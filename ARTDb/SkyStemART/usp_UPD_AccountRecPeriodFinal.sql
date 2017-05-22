


  
  
  
ALTER PROCEDURE [dbo].[usp_UPD_AccountRecPeriodFinal]  
(  
	@AccountTable udt_BigIntIDTableType READONLY
	,@RecPeriodTable udt_IntIDTableType READONLY
	,@CompanyID INT
	,@RecPeriodID INT
	,@ChangeSourceIDSRA SMALLINT
	,@ActionTypeID SMALLINT
)  
AS 
/*------------------------------------------------------------------------------
	AUTHOR:			Rajiv Ranjan
	DATE CREATED:	01/28/2010
	PURPOSE:		Update Number of Accounts with a single value for an attribute
-------------------------------------------------------------------------------
	MODIFIED		AUTHOR				DESCRIPTION
	DATE
-------------------------------------------------------------------------------
	11th Nov 11		Harsh				Reconcilable Attribute
	3/26/2012		Vinay				1. Delete Rec Items for removed accounts because of reconcilability change
										2. Copy Rec Items for added accounts because of reconcilability change
										3. Do not create reconcilability in older periods (less than account creation period)
	5/9/2012		Vinay				Bug Fixed #9168		
	8/20/2012		Vinay				Bug fixed, Call only when there are accounts affected			
	10/04/2012		Vinay				Revised by changes					
	10/16/2013		Vinay				Change Source and Action Type changes		
	01/28/2014		Vinay				Bug Fixed	
	02/10/2014		Vinay				Calculate and Insert Due Dates	
	29/05/2014		Manoj				Increase Error Message length				
-------------------------------------------------------------------------------*/ 
BEGIN

	DECLARE @RecPeriodEndDate DATETIME;
	DECLARE @FutureRecPeriodIDs TABLE(RecPeriodId INT);
	--DECLARE @tblNonReconcilable TABLE (AccountID BIGINT, RecPeriodID INT)--Reconcilable Attribute
	DECLARE @ReconcilableAccountAttributeID SMALLINT = 16--Reconcilable Attribute
	DECLARE @tblAccountAttr udt_SmallintIDTableType 
	DECLARE @tblAccountRecPeriodAction TABLE (AccountID BIGINT, RecPeriodID INT, UpdateAction TINYINT)	
	DECLARE @tblAccountsDeleted udt_BigIntIDTableType
	DECLARE @tblAccountsAdded udt_BigIntIDTableType
	DECLARE @tblGLDataAdded udt_BigIntIDTableType
	DECLARE @tblGLDataDeleted udt_BigIntIDTableType
	DECLARE @SQLJobUserID NVARCHAR(128) = (SELECT dbo.fn_GET_AppSettingValue('SQLJobUserIDForAccountReconcilability'))
	DECLARE @DateRevised DATETIME = GETDATE()
	DECLARE @CurrencyExchangeRateErrMsg NVARCHAR(MAX)
	DECLARE @udt_InputAccountAndNetAccount udt_BigInt_Int_NullableIDTableType
	DECLARE @RecPeriodStatusID SMALLINT
	DECLARE @DeleteAction TINYINT = 1
		,@AddAction TINYINT = 2
		,@DeletedCount INT = 0
		,@AddedCount INT = 0

	IF EXISTS (SELECT 1 FROM tempdb.dbo.sysobjects WHERE ID = OBJECT_ID('tempdb..#TmptblNonReconcilable'))
	BEGIN
		DROP TABLE #TmptblNonReconcilable
	END	
	CREATE TABLE #TmptblNonReconcilable (AccountID BIGINT, RecPeriodID INT)
	IF EXISTS (SELECT 1 FROM tempdb.dbo.sysobjects WHERE ID = OBJECT_ID('tempdb..#TmptblAccountRecPeriod'))
	BEGIN
		DROP TABLE #TmptblAccountRecPeriod
	END	
	CREATE TABLE #TmptblAccountRecPeriod (AccountID BIGINT, RecPeriodID INT, PeriodEndDate DATETIME)
	
	-- Get Reconciliation period end date
	SELECT 
			@RecPeriodEndDate =  RP.PeriodEndDate
			,@RecPeriodStatusID = RP.ReconciliationPeriodStatusID
	FROM 
			ReconciliationPeriod RP 
	WHERE 
			RP.ReconciliationPeriodID = @RecPeriodID;
	
	-- Get All Future Rec Periods
	INSERT INTO 
			@FutureRecPeriodIDs
			(
				RecPeriodId
			)
	SELECT 
			RP.ReconciliationPeriodID
	FROM  
			ReconciliationPeriod RP
	WHERE 
			RP.CompanyID = @CompanyID
			AND RP.IsActive = 1
			AND RP.PeriodEndDate >= @RecPeriodEndDate;
	
		
	--Reconcilable Attribute: Get all Non Reconcilable Accounts from future Rec Periods on the basis of Reconcilable Attribute
	INSERT INTO @tblAccountAttr(ID)
	VALUES (@ReconcilableAccountAttributeID)
	
	INSERT INTO #TmptblNonReconcilable (AccountID, RecPeriodID)
	SELECT 
		AAV.AccountID
		, AAV.RecPeriodID 
	FROM
		[dbo].[fn_SEL_AcctAttr] (@CompanyID, @AccountTable, @RecPeriodTable, @tblAccountAttr) AAV
	WHERE
		CONVERT(BIT, AAV.Value) = 0 	

	
	-- Delete All Future Account Rec Period values for all accounts 
	DELETE 
			ARP
			OUTPUT deleted.AccountID, deleted.ReconciliationPeriodID, @DeleteAction INTO @tblAccountRecPeriodAction
	FROM 
			AccountReconciliationPeriodFinal ARP
	INNER JOIN 
			@FutureRecPeriodIDs FRP
			ON ARP.ReconciliationPeriodID = FRP.RecPeriodId
	INNER JOIN 
			@AccountTable AT
			ON ARP.AccountID = AT.ID
	LEFT OUTER JOIN 
			@RecPeriodTable RP
			ON ARP.ReconciliationPeriodID = RP.ID
	WHERE 
			RP.ID IS NULL;
	

	--Delete all the accounts for Future Rec Periods For Which Reconcilable Attribute is Set to False
	DELETE
		ARP
		OUTPUT deleted.AccountID, deleted.ReconciliationPeriodID, @DeleteAction INTO @tblAccountRecPeriodAction
	FROM
		AccountReconciliationPeriodFinal ARP
		INNER JOIN #TmptblNonReconcilable AAV
			ON ARP.AccountID = AAV.AccountID 
			AND ARP.ReconciliationPeriodID = AAV.RecPeriodID 	
	
	INSERT INTO #TmptblAccountRecPeriod
		(
			AccountID
			,RecPeriodID
			,PeriodEndDate
		)
		SELECT 
			AT.ID AS AccountID
			, RP.ID As RecPeriodID
			, RP2.PeriodEndDate
		FROM 
			@AccountTable AT
		INNER JOIN vw_Select_ActiveAccounts AA
			ON AT.ID = AA.AccountID
		INNER JOIN DataImportHdr DI
			ON AA.DataImportID = DI.DataImportID
		INNER JOIN ReconciliationPeriod RP1
			ON DI.ReconciliationPeriodID = RP1.ReconciliationPeriodID
		CROSS APPLY
			(
				@RecPeriodTable RP
				INNER JOIN
					ReconciliationPeriod RP2
					ON RP.ID = RP2.ReconciliationPeriodID
			)
		WHERE
			RP2.PeriodEndDate >= RP1.PeriodEndDate
			AND RP2.PeriodEndDate >= @RecPeriodEndDate		
	
	-- INSERT records for the Reconcilable rec periods. Only those accounts will be inserted which are reconcilable as per reconcilable Attribute
	INSERT INTO 
		AccountReconciliationPeriodFinal
		(
			AccountID
			,ReconciliationPeriodID
			,PreparerDueDate
			,ReviewerDueDate
			,ApproverDueDate
		)
		OUTPUT inserted.AccountID, inserted.ReconciliationPeriodID, @AddAction INTO @tblAccountRecPeriodAction
	SELECT 
		AR.AccountID
		,AR.RecPeriodID
		,CASE WHEN AAVD.PreparerDueDays IS NOT NULL 
			THEN DATEADD(d, Convert(INT, AAVD.PreparerDueDays), AR.PeriodEndDate) 
			ELSE NULL
		END
		,CASE WHEN AAVD.ReviewerDueDays IS NOT NULL 
			THEN DATEADD(d, Convert(INT, AAVD.ReviewerDueDays), AR.PeriodEndDate) 
			ELSE NULL
		END
		,CASE WHEN AAVD.ApproverDueDays IS NOT NULL 
			THEN DATEADD(d, Convert(INT, AAVD.ApproverDueDays), AR.PeriodEndDate) 
			ELSE NULL
		END		
	FROM #TmptblAccountRecPeriod AR
	LEFT OUTER JOIN
		dbo.fn_SEL_AccountAttributeValuePIVOT(@CompanyID, @RecPeriodEndDate) AAVD
		ON AR.AccountID = AAVD.AccountID
	LEFT OUTER JOIN	
		AccountReconciliationPeriodFinal ARP
		ON ARP.AccountID = AR.AccountID
		AND ARP.ReconciliationPeriodID = AR.RecPeriodID
	LEFT OUTER JOIN 
		#TmptblNonReconcilable AAV
		ON AAV.AccountID = AR.AccountID 
		AND AAV.RecPeriodID = AR.RecPeriodID 		
	WHERE 
		ARP.AccountReconciliationPeriodFinalID IS NULL
		AND (AAV.AccountID IS NULL AND AAV.RecPeriodID IS NULL)

	-- Open Period Changes		
	IF (@RecPeriodStatusID = 2 OR @RecPeriodStatusID = 3) -- Open or In-Progress		
	BEGIN	
	-- Delete Rec Items for the records being removed
		INSERT INTO
			@tblAccountsDeleted
		SELECT
			DISTINCT AccountID
		FROM
			@tblAccountRecPeriodAction
		WHERE
			UpdateAction = @DeleteAction AND RecPeriodID = @RecPeriodID
			
		SELECT 
			@DeletedCount = COUNT(1) 
		FROM 
			@tblAccountsDeleted
			
		IF (@DeletedCount > 0)
		BEGIN
			INSERT INTO
				@tblGLDataDeleted
				SELECT
					GLDataID
				FROM
					dbo.fn_SEL_GLDataForAccounts(@tblAccountsDeleted, @RecPeriodID)
					
			IF ((SELECT COUNT(1) FROM @tblGLDataDeleted)>0)
			BEGIN
				EXEC dbo.usp_DEL_AllRecItems    
					@GLDataIDTable = @tblGLDataDeleted
					,@DateRevised = @DateRevised
					,@RevisedBy = @SQLJobUserID
					,@ChangeSourceIDSRA = @ChangeSourceIDSRA
					,@ActionTypeID = @ActionTypeID
					
				-- Mark Status to Not Started
				EXEC dbo.usp_UPD_GLDataReconciliationStatus  
					@GLDataIDTable = @tblGLDataDeleted
					,@RecPeriodID = @RecPeriodID
					,@ReconciliationStatusID = 8 -- Not Started
					,@RevisedBy = @SQLJobUserID
					,@DateRevised = @DateRevised
					,@ChangeSourceIDSRA = @ChangeSourceIDSRA
					,@ActionTypeID = @ActionTypeID
			END
		END
	-- Call Copy Item Engine for records being added

		INSERT INTO
			@tblAccountsAdded
		SELECT
			DISTINCT AccountID
		FROM
			@tblAccountRecPeriodAction
		WHERE
			UpdateAction = @AddAction AND RecPeriodID = @RecPeriodID

		SELECT 
			@AddedCount = COUNT(1) 
		FROM 
			@tblAccountsAdded

		IF (@AddedCount > 0)
		BEGIN	
			INSERT INTO @udt_InputAccountAndNetAccount 
			(
				ID1 
				,ID2
			)
			SELECT
				AccountID
				,NetAccountID
			FROM
				dbo.fn_SEL_GLDataForAccounts(@tblAccountsAdded, @RecPeriodID)
				  
			EXEC [dbo].[usp_INS_CopyOpenItemsEngine]   
				@CompanyID = @CompanyID
				,@UDT_InputInfo = @udt_InputAccountAndNetAccount
				,@InputRecPeriodID = @RecPeriodID
				,@OnlyCheckForExchangeRate = 0
				,@ProcessForMateriality = 0
				,@ProcessForSRA = 0
				,@ProcessForAttachment = 1
				,@SourceRecPeriodID = null
				,@DestinationRecPeriodID  = @RecPeriodID
				,@RevisedBy = @SQLJobUserID
				,@ChangeSourceIDSRA = @ChangeSourceIDSRA
				,@ActionTypeID = @ActionTypeID
				,@currencyExchangeRateErrorMessage = @CurrencyExchangeRateErrMsg OUT

			INSERT INTO 
				@tblGLDataAdded 
				SELECT
					GLDataID
				FROM
					dbo.fn_SEL_GLDataForAccounts(@tblAccountsAdded, @RecPeriodID)

			IF ((SELECT COUNT(1) FROM @tblGLDataAdded)>0)
			BEGIN
				EXEC [dbo].[usp_UPD_MaterialityAndSRAWrapper] 
					@udtGLDataID = @tblGLDataAdded
					,@CompanyID = @CompanyID
					,@RecPeriodID = @RecPeriodID
					,@DateRevised = @DateRevised
					,@ProcessForMateriality = 1
					,@ProcessForSRA = 1
					,@RevisedBy = @SQLJobUserID
					,@ChangeSourceIDSRA = @ChangeSourceIDSRA
					,@ActionTypeID = @ActionTypeID
					,@ProcessGLDataIDListOnly = 1
			END
		END
	END		
	IF EXISTS (SELECT 1 FROM tempdb.dbo.sysobjects WHERE ID = OBJECT_ID('tempdb..#TmptblNonReconcilable'))
	BEGIN
		DROP TABLE #TmptblNonReconcilable
	END	
	IF EXISTS (SELECT 1 FROM tempdb.dbo.sysobjects WHERE ID = OBJECT_ID('tempdb..#TmptblAccountRecPeriod'))
	BEGIN
		DROP TABLE #TmptblAccountRecPeriod
	END		
END

