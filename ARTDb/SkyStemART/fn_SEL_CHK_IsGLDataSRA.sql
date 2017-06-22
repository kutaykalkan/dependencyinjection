
ALTER FUNCTION [dbo].[fn_SEL_CHK_IsGLDataSRA]
(	
	@companyID int,
	@recPeriodID int, 
	@NewGLDataIDTbl  udt_BigIntIDTableType   READONLY
)  
RETURNS @GLDataSRATbl TABLE
(
	GLDataID BIGINT
	,SRARuleID SMALLINT
	,IsSRA BIT
)

 
AS  
/*------------------------------------------------------------------------------  
	AUTHOR    Manoj Kumar  
	Creation Date  01/06/2012  
-------------------------------------------------------------------------------  
	MODIFIED		AUTHOR				DESCRIPTION  
	DATE  
-------------------------------------------------------------------------------  
	06/04/2015		Manoj				Bug Fixed Same rule issue
	07/22/2015		Vinay				Production Issue fixed
	08/14/2015		Vinay				Added new subledger rule
-------------------------------------------------------------------------------  
 
------------------------------------------------------------------------------*/   

BEGIN 

	DECLARE @preparedReconciliationStatusID SMALLINT=1-- 1 is the ReconciliationStatusID for Prepared
	DECLARE @notStartedReconciliationStatusID SMALLINT=8-- 8 is the ReconciliationStatusID for Not Started
	DECLARE @CompanyUnexplainedVarianceThreshold DECIMAL(18,4)
	DECLARE @RecPeriodEndDate SMALLDATETIME	
	DECLARE @ZerobalanceRule SMALLINT
	DECLARE @SubledgerRule SMALLINT
	DECLARE @NetAccountsBelowUexpVarRule SMALLINT
	DECLARE @AccountbelowUnexpVar SMALLINT
	DECLARE @MaterialityRule SMALLINT
	DECLARE @GLBalanceChangeRule SMALLINT
	DECLARE @RecTemplateIDForSubLedger SMALLINT = CONVERT(SMALLINT, dbo.fn_GET_AppSettingValue('RecTemplateIDForSubLedger'))	
		,@AccountAttributeIDForRecTemplate SMALLINT = CONVERT(SMALLINT, dbo.fn_GET_AppSettingValue('AccountAttributeIDForRecTemplate'))	
		,@AccountAttributeIDForZBA SMALLINT = CONVERT(INT, dbo.fn_GET_AppSettingValue('AccountAttributeIDForZBA'))
		
	DECLARE @tblSRARules TABLE (RecordID INT IDENTITY, RuleID SMALLINT)
	DECLARE @minSumThreshold DECIMAL (18,4) ,@maxSumThreshold DECIMAL (18,4)
		SET @maxSumThreshold = dbo.fn_GET_ZeroThresholdByRecPeriodID  (@RecPeriodID )
			SET @minSumThreshold = -1 * @maxSumThreshold

	DECLARE @TmpGLDataHdrForNetAccount AS Table(
		 GLDataID bigint       
		, NetAccountID int		  
	)

	DECLARE @TmpGLDataHdrForNormalAccount AS Table(
		 GLDataID bigint       
		, AccountID bigint
	)

	DECLARE @tblAccountsInNetAccount AS Table(
		AccountID BIGINT
		,NetAccountID INT
	)

	DECLARE @tblUpdatedNetAccounts AS Table(
		NetAccountID INT
	)
	--Get PeriodEndDate for this rec period
	SELECT
		@RecPeriodEndDate= PeriodEndDate 
	FROM 
		ReconciliationPeriod
	WHERE
		ReconciliationPeriodID = @recPeriodID	
			
	--Get all the Accounts which are part of some net Account for this rec period
	INSERT INTO
		 @tblAccountsInNetAccount(AccountID, NetAccountID)
	SELECT 
		AccountID 
		,CONVERT(INT, Value) 
	FROM 
		[dbo].[fn_SEL_AccountAttributeValue] (@RecPeriodEndDate, @companyID )
	WHERE 
		AccountAttributeID = 12 
		AND Value IS NOT NULL
	--Get all SRARules for this rec period
	INSERT INTO 
		@tblSRARules
	SELECT
		*
	FROM fn_GET_SRARulesByCompanyIDRecPeriodEndDate(@companyID, @RecPeriodEndDate)
	--Set @ZerobalanceRule 
	SET @ZerobalanceRule = (SELECT RuleID FROM @tblSRARules Where RuleID = 1)
	--Set @SubledgerRule
	SET @SubledgerRule = (SELECT RuleID FROM @tblSRARules Where RuleID = 2)
	--Set @NetAccountsBelowUexpVarRule
	SET @NetAccountsBelowUexpVarRule = (SELECT RuleID FROM @tblSRARules WHERE RuleID = 3)
	--Set @MaterialityRule 
	SET @MaterialityRule = (SELECT RuleID FROM @tblSRARules WHERE RuleID = 4)
	--Set @GLBalanceChangeRule 
	SET @GLBalanceChangeRule = (SELECT RuleID FROM @tblSRARules WHERE RuleID = 5)	
	-- Get Unexplained Variance threshold value for this rec period
	SELECT  
		@CompanyUnexplainedVarianceThreshold = cuvt.CompanyUnexplainedVarianceThreshold
	FROM 
		[dbo].[CompanyUnexplainedVarianceThreshold] cuvt
		INNER JOIN [dbo].[ReconciliationPeriod] rpStart
		ON cuvt.StartReconciliationPeriodID = rpStart.ReconciliationPeriodID
		LEFT JOIN [dbo].[ReconciliationPeriod] rpEnd
		ON cuvt.EndReconciliationPeriodID = rpEnd.ReconciliationPeriodID
	WHERE  
		cuvt.[CompanyID] = @companyID 
		AND @RecPeriodEndDate >= rpStart.PeriodEndDate 
		AND ((@RecPeriodEndDate < rpEnd.PeriodEndDate) OR (EndReconciliationPeriodID IS NULL))	
								
	IF((SELECT COUNT(ID) FROM @NewGLDataIDTbl) > 0) 
	BEGIN

	
		--- GET NetAccountID for GLDataID`s IF any account is net account
			;WITH cteLockedNetAccounts AS
			(
				SELECT DISTINCT NetAccountID, IsLocked
				FROM
					dbo.fn_SEL_LockedAccounts(@companyID, @RecPeriodEndDate)
			), cteNetAccountAAV AS
			(
				SELECT DISTINCT AAV.Value
				FROM
					dbo.fn_SEL_AccountAttributeValue(@RecPeriodEndDate, @companyID) AAV
				WHERE
					AAV. AccountAttributeID =12 --  AccountAttributeID for net Account
			)
			INSERT INTO @TmpGLDataHdrForNetAccount 
			(
				GLDataID 
				,NetAccountID 
			)
			SELECT
				 DISTINCT GLH.GLDataID
				,AAV.Value
			FROM 
				cteNetAccountAAV AAV
				INNER JOIN dbo.GLDataHdr GLH
					ON CONVERT(NVARCHAR(20),GLH.NetAccountID) = AAV.Value
				INNER JOIN @NewGLDataIDTbl TblGLDataID 
					ON GLH.GLDataID  = TblGLDataID.ID 
				INNER JOIN dbo.NetAccountHdr NAH
					ON GLH.NetAccountID = NAH.NetAccountID 
				LEFT JOIN cteLockedNetAccounts LA
					ON GLH.NetAccountID = LA.NetAccountID				        
			WHERE
				--AAV. AccountAttributeID =12 --  AccountAttributeID for net Account
				--AND GLH.IsLocked = 0 --ommit locked accounts in net acct
				(LA.IsLocked = 0 OR LA.IsLocked IS NULL) --ommit locked accounts in net acct
				AND GLH.ReconciliationPeriodID = @recPeriodID
				AND GLH.AccountID IS NULL --ommit normal accounts 					
				AND NAH.IsActive = 1
				AND NAH.CompanyID = @companyID 
		--- GET Normal AccountID for GLDataID for which SRA Should be Processed					
			INSERT INTO @TmpGLDataHdrForNormalAccount 
			(
				GLDataID 
				, AccountID 				   
			)
			SELECT
				GLH.GLDataID
				,GLH.AccountID
			FROM  GLDataHdr GLH				
				INNER JOIN @NewGLDataIDTbl TblGLDataID 
					ON GLH.GLDataID  = TblGLDataID.ID 
				INNER JOIN [dbo].[vw_Select_ActiveAccounts] AH 
					ON GLH.AccountID = AH.AccountID--this will ommit Net Accounts as AccountID is null for Net Accounts  
				LEFT JOIN dbo.fn_SEL_LockedAccounts(@companyID, @RecPeriodEndDate) LA
					ON GLH.AccountID = LA.AccountID				        
			WHERE
				GLH.ReconciliationPeriodID = @recPeriodID
				AND AH.FSCaptionID IS NOT NULL -- ommit P&L Accounts
				AND AH.AccountTypeID IS NOT NULL -- ommit P&L Accounts
				AND AH.AccountID NOT IN (Select AccountID from @tblAccountsInNetAccount)--ommit accounts in net acct
				-- AND GLH.IsLocked = 0 --ommit locked accounts in net acct
				AND (LA.IsLocked = 0 OR LA.IsLocked IS NULL) --ommit locked accounts
	END
	
	IF (@CompanyUnexplainedVarianceThreshold is not null)
		BEGIN 

			;WITH cteRuleRegularAccount AS
			( 
				SELECT
					GLD.GLDataID
					,CASE					
						WHEN @GLBalanceChangeRule IS NOT NULL AND (ABS(UnexplainedVarianceReportingCurrency) < ABS(@CompanyUnexplainedVarianceThreshold))
							AND IsNull(GLOO.OpenItemCount,0) = 0 AND IsNull(GLB.IsAccountSRAbyGLBalance,0) = 1 THEN @GLBalanceChangeRule 
						WHEN SUBUNEXP.IsSubLedgerLoadedWithUnexpVar = 1 THEN NULL
						WHEN IsNull(GLO.OpenItemCount,0) > 0 THEN NULL
						WHEN @ZerobalanceRule IS NOT NULL AND IsNull(ZBA.IsAccountSRAbyZBA,0) = 1 THEN @ZerobalanceRule 
						WHEN @MaterialityRule IS NOT NULL AND (IsMaterial = 0) THEN @MaterialityRule
						WHEN @SubledgerRule IS NOT NULL AND ISNULL(SUB.IsAccountSRAbySubLedger,0) = 1 Then @SubledgerRule 
						--WHEN	@AccountbelowUnexpVar IS NOT NULL AND (UnexplainedVarianceReportingCurrency  < @CompanyUnexplainedVarianceThreshold) then @AccountbelowUnexpVar
					ELSE 
						null 
					END AS SystemReconciliationRuleID
				FROM 
					[dbo].[GLDataHdr] GLD
				INNER JOIN @TmpGLDataHdrForNormalAccount TblNormalAccount 
					ON GLD.GLDataID  = TblNormalAccount.GLDataID			
				LEFT JOIN dbo.fn_SEL_GLDataOpenRecItemCountExceptSupportingDetails(@recPeriodID) GLOO
					ON GLD.GLDataID = GLOO.GLDataID
				LEFT JOIN dbo.fn_SEL_IsAccountSRAbyGLBalanceRule(@companyID, @recPeriodID, @RecPeriodEndDate) GLB
					ON GLD.GLDataID = GLB.GLDataID
				LEFT JOIN dbo.fn_SEL_GLDataOpenRecItemCount(@recPeriodID) GLO
					ON GLD.GLDataID = GLO.GLDataID
				LEFT JOIN dbo.fn_SEL_IsAccountSRAbyZBARule(@companyID, @RecPeriodID, @RecPeriodEndDate, @maxSumThreshold, @minSumThreshold, @AccountAttributeIDForZBA) ZBA
					ON GLD.GLDataID = ZBA.GLDataID
				LEFT JOIN dbo.fn_SEL_IsAccountSRAbySubLedgerRule(@companyID, @RecPeriodID, @RecPeriodEndDate, @CompanyUnexplainedVarianceThreshold, @AccountAttributeIDForRecTemplate, @RecTemplateIDForSubLedger) SUB
					ON GLD.GLDataID = SUB.GLDataID
				LEFT JOIN dbo.fn_SEL_IsAccountSubLedgerLoadedWithUnexpVar(@companyID, @RecPeriodID, @RecPeriodEndDate, @CompanyUnexplainedVarianceThreshold, @AccountAttributeIDForRecTemplate, @RecTemplateIDForSubLedger) SUBUNEXP
					ON GLD.GLDataID = SUBUNEXP.GLDataID
			), cteSRARegularAccount AS
			(
				SELECT 
					RRA.GLDataID
					,RRA.SystemReconciliationRuleID
					,CASE WHEN RRA.SystemReconciliationRuleID IS NULL THEN 0 ELSE 1 END As IsSystemReconciled
				FROM
					cteRuleRegularAccount RRA
			)
			-- Update for GL Data Hdr Items for NON - Net Accounts only
			INSERT INTO @GLDataSRATbl
				(
					GLDataID
					,SRARuleID
					,IsSRA
				)
				SELECT
					RRA.GLDataID	
					,RRA.SystemReconciliationRuleID			
					,RRA.IsSystemReconciled	
				FROM 
					cteSRARegularAccount RRA 
			

			-- Update for GL Data Hdr Items for Net Accounts only
			;WITH cteRuleNetAccount AS
			(	SELECT
					G.GLDataID 
					,CASE 						
						WHEN @GLBalanceChangeRule IS NOT NULL AND (ABS(UnexplainedVarianceReportingCurrency) < ABS(@CompanyUnexplainedVarianceThreshold)) 
							AND IsNull(GLOO.OpenItemCount,0) = 0 AND IsNull(GLB.IsNetAccountSRAbyGLBalance,0) = 1 THEN @GLBalanceChangeRule 
						WHEN SUBUNEXP.IsSubLedgerLoadedWithUnexpVar = 1 THEN NULL
						WHEN IsNull(GLO.OpenItemCount,0) > 0 THEN NULL
						WHEN @ZerobalanceRule IS NOT NULL AND IsNull(ZBA.IsNetAccountSRAbyZBA,0) = 1 THEN @ZerobalanceRule 
						WHEN @MaterialityRule IS NOT NULL AND (IsMaterial = 0) THEN @MaterialityRule
						WHEN @SubledgerRule IS NOT NULL AND ISNULL(SUB.IsNetAccountSRAbySubLedger,0) = 1 Then @SubledgerRule 
						WHEN @NetAccountsBelowUexpVarRule IS NOT NULL AND (ABS(UnexplainedVarianceReportingCurrency) < ABS(@CompanyUnexplainedVarianceThreshold)) then @NetAccountsBelowUexpVarRule 
					ELSE 
						null 
					END AS SystemReconciliationRuleID
				FROM 
					[dbo].[GLDataHdr] G
				INNER JOIN @TmpGLDataHdrForNetAccount NAH
					ON G.GLDataID = NAH.GLDataID 			
				LEFT JOIN dbo.fn_SEL_GLDataOpenRecItemCountExceptSupportingDetails(@recPeriodID) GLOO
					ON G.GLDataID = GLOO.GLDataID
				LEFT JOIN dbo.fn_SEL_IsNetAccountSRAbyGLBalanceRule(@companyID, @recPeriodID, @RecPeriodEndDate) GLB
					ON G.GLDataID = GLB.GLDataID
				LEFT JOIN dbo.fn_SEL_GLDataOpenRecItemCount(@recPeriodID) GLO
					ON G.GLDataID = GLO.GLDataID
				LEFT JOIN dbo.fn_SEL_IsNetAccountSRAbyZBARule(@companyID, @RecPeriodID, @RecPeriodEndDate, @maxSumThreshold, @minSumThreshold, @AccountAttributeIDForZBA) ZBA
					ON G.GLDataID = ZBA.GLDataID
				LEFT JOIN dbo.fn_SEL_IsNetAccountSRAbySubLedgerRule(@companyID, @RecPeriodID, @RecPeriodEndDate, @CompanyUnexplainedVarianceThreshold, @RecTemplateIDForSubLedger) SUB
					ON G.GLDataID = SUB.GLDataID
				LEFT JOIN dbo.fn_SEL_IsNetAccountSubLedgerLoadedWithUnexpVar(@companyID, @RecPeriodID, @RecPeriodEndDate, @CompanyUnexplainedVarianceThreshold, @RecTemplateIDForSubLedger) SUBUNEXP
					ON G.GLDataID = SUBUNEXP.GLDataID
			), cteSRANetAccount AS
			(
				SELECT 
					RNA.GLDataID
					,RNA.SystemReconciliationRuleID
					,CASE WHEN RNA.SystemReconciliationRuleID IS NULL THEN 0 ELSE 1 END As IsSystemReconciled
				FROM
					cteRuleNetAccount RNA
			)
			INSERT INTO @GLDataSRATbl
			(
				GLDataID
				,SRARuleID
				,IsSRA
			)
			SELECT
				RNA.GLDataID
				, RNA.SystemReconciliationRuleID
				, RNA.IsSystemReconciled	
			FROM 
				 cteSRANetAccount RNA
				
		
	END
	RETURN 
END  
