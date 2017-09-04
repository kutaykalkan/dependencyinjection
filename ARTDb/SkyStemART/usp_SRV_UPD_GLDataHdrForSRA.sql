
ALTER PROCEDURE [dbo].[usp_SRV_UPD_GLDataHdrForSRA]  
(
	@companyID int,
	@recPeriodID int, 
	@maxSumThreshold decimal(18,4),
	@minSumThreshold decimal(18,4),
	@dateRevised smalldatetime,
	@NewGLDataIDTbl  udt_BigIntIDTableType   READONLY,
	@revisedBy NVARCHAR(128),
	@ChangeSourceIDSRA SMALLINT,
	@ActionTypeID SMALLINT,
	@ProcessGLDataIDListOnly BIT
)  
 
AS  
/*------------------------------------------------------------------------------  
	AUTHOR    Manoj Kumar  
	Creation Date  01/06/2012  
-------------------------------------------------------------------------------  
	MODIFIED		AUTHOR				DESCRIPTION  
	DATE  
-------------------------------------------------------------------------------  
	01/03/2011		Apoorv Gupta		Used View for Active Accounts - vw_Select_ActiveAccounts
	2/11/2011		Harsh Kuntail		Added Revised By, DateRevised when update is happening
	7/27/2011		Manoj Kumar			Update ReconciliationStatusID = CASE
																		 WHEN SystemReconciliationRuleID IS NOT NULL THEN prepared ReconciliationStatusID
																		 WHEN SystemReconciliationRuleID IS NULL AND GLDataHdr.IsSystemReconcilied = 1 THEN NotStarted ReconciliationStatusID
																		 WHEN SystemReconciliationRuleID IS NULL AND GLDataHdr.IsSystemReconcilied = 0 THEN GLDataHdr.ReconciliationStatusID
																	   ELSE
																		 NotStarted ReconciliationStatusID
																	   END 
	11/5/2011		Harsh Kuntail		Isnull check on IsSystemReconciled field, ReconciliationStatusDate
	11/25/2011		Manoj Kumar			Put SRA Processing on account list	
	02/06/2012		Manoj Kumar			Add Net Accounts BelowUexpVar Rule
	02/07/2012		Manoj Kumar			DISTINCT Gldata id for net accounts	
	06/14/2012		Vinay				Optimization Changes
	09/02/2012		Vinay				Net Account SRA Fix.														   
	10/04/2012		Vinay				RevisedBy Parameter added
	10/06/2012		Vinay				Bug Fixed #9279
	11/29/2012		Vinay				New SRA Rule added
	06/06/2013		Vinay				Function used for IsLocked instead of flag
	10/11/2013		Vinay				Change Source ID Parameter added
	11/21/2014		Vinay				Production Issue fix (Re-SRA should not happen if new rule is same as previous rule)
	12/09/2014		Vinay				Bug Fixed
	05/26/2015		Vinay				Stop SRA when RCC is enabled and not complete
	06/25/2015		Manoj				Copy Temporary Active Attachments
	08/14/2015		Vinay				Added new subledger rule
-------------------------------------------------------------------------------  
 Table Affected:
		1. GL Data HDR
Usage Example
	exec [dbo].[usp_SRV_UPD_GLDataHdrForSRA] 1, 13,'nancy@demo1.com' ,'2010-03-12 16:13:00'
	, '2009-11-24 00:00:00.000',3,0.1234, -0.1234
 
	exec [dbo].[usp_SRV_UPD_GLDataHdrForSRA] 1, 13,'nancy@demo1.com' ,'2010-03-12 16:13:00'
	, '2009-01-20 19:50:00.000',1,2.1234, -0.1234
	
------------------------------------------------------------------------------*/   

BEGIN 

	DECLARE @companyIDLocal int,
		@recPeriodIDLocal int, 
		@maxSumThresholdLocal decimal(18,4),
		@minSumThresholdLocal decimal(18,4),
		@dateRevisedLocal smalldatetime,
--		@NewGLDataIDTblLocal  udt_BigIntIDTableType,
		@revisedByLocal NVARCHAR(128),
		@ChangeSourceIDSRALocal SMALLINT,
		@ActionTypeIDLocal SMALLINT,
		@ProcessGLDataIDListOnlyLocal BIT

	SELECT @companyIDLocal = @companyID
		,@recPeriodIDLocal = @recPeriodID
		,@maxSumThresholdLocal = @maxSumThreshold
		,@minSumThresholdLocal = @minSumThreshold
		,@dateRevisedLocal = @dateRevised
		,@revisedByLocal = @revisedBy
		,@ChangeSourceIDSRALocal = @ChangeSourceIDSRA
		,@ActionTypeIDLocal = @ActionTypeID
		,@ProcessGLDataIDListOnlyLocal = @ProcessGLDataIDListOnly

--	INSERT INTO @NewGLDataIDTblLocal (ID) SELECT ID FROM @NewGLDataIDTbl

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

	DECLARE @RCCFeatureID SMALLINT = 37
	DECLARE @RCCCapabilityID SMALLINT = 14
	DECLARE @RCCValidationTypeIDFORExcludeSRA SMALLINT = 2

	DECLARE @IsRCCCapabilityEnabled SMALLINT = dbo.fn_GET_IsFeatureCapabilityActivated(@CompanyIDLocal, @RCCFeatureID,@RCCCapabilityID, @RecPeriodIDLocal)
	DECLARE @RCCCValidationType SMALLINT
	SELECT @RCCCValidationType = RCCV.RCCValidationTypeID FROM [dbo].[fn_GET_RCCValidationType](@RecPeriodIDLocal) RCCV	
		
	DECLARE @tblSRARules TABLE (RecordID INT IDENTITY, RuleID SMALLINT)

	IF EXISTS (SELECT 1 FROM tempdb.dbo.sysobjects WHERE ID = OBJECT_ID('tempdb..#TmpInputGLDataID'))
	BEGIN
		DROP TABLE #TmpInputGLDataID
	END	
	CREATE TABLE #TmpInputGLDataID(
		 ID bigint       
	)

	INSERT INTO #TmpInputGLDataID (ID) SELECT ID FROM @NewGLDataIDTbl

	IF EXISTS (SELECT 1 FROM tempdb.dbo.sysobjects WHERE ID = OBJECT_ID('tempdb..#TmpGLDataHdrForNetAccount'))
	BEGIN
		DROP TABLE #TmpGLDataHdrForNetAccount
	END	
	CREATE TABLE #TmpGLDataHdrForNetAccount(
		 GLDataID bigint       
		, NetAccountID int		  
	)

	IF EXISTS (SELECT 1 FROM tempdb.dbo.sysobjects WHERE ID = OBJECT_ID('tempdb..#TmpGLDataHdrForNormalAccount'))
	BEGIN
		DROP TABLE #TmpGLDataHdrForNormalAccount
	END	
	CREATE TABLE #TmpGLDataHdrForNormalAccount(
		 GLDataID bigint       
		, AccountID bigint
	)

	IF EXISTS (SELECT 1 FROM tempdb.dbo.sysobjects WHERE ID = OBJECT_ID('tempdb..#tblAccountsInNetAccount'))
	BEGIN
		DROP TABLE #tblAccountsInNetAccount
	END	
	CREATE TABLE #tblAccountsInNetAccount(
		AccountID BIGINT
		,NetAccountID INT
	)

	IF EXISTS (SELECT 1 FROM tempdb.dbo.sysobjects WHERE ID = OBJECT_ID('tempdb..#tblUpdatedNetAccounts'))
	BEGIN
		DROP TABLE #tblUpdatedNetAccounts
	END	
	CREATE TABLE #tblUpdatedNetAccounts(
		NetAccountID INT
	)

	--update GL Data HDR for SRA
	/*
	An Account is SRA for a period if
	1. If it has zero balance
	2. Subledger matching accounts where variance is below unexplained variance threshold
	3. Net Accounts where variance is below unexplained variance threshold
	4. All Accounts with balances below unexplained variance threshold
	Select * from GL Data HDR where GLBalanceReportingCurrency = 0
	
	*/
	--Get PeriodEndDate for this rec period
	SELECT
		@RecPeriodEndDate= PeriodEndDate 
	FROM 
		ReconciliationPeriod
	WHERE
		ReconciliationPeriodID = @recPeriodIDLocal	
			
	--Get all the Accounts which are part of some net Account for this rec period
	INSERT INTO
		 #tblAccountsInNetAccount(AccountID, NetAccountID)
	SELECT 
		AccountID 
		,CONVERT(INT, Value) 
	FROM 
		[dbo].[fn_SEL_AccountAttributeValue] (@RecPeriodEndDate, @companyIDLocal )
	WHERE 
		AccountAttributeID = 12 
		AND Value IS NOT NULL
	--Get all SRARules for this rec period
	INSERT INTO 
		@tblSRARules
	SELECT
		*
	FROM fn_GET_SRARulesByCompanyIDRecPeriodEndDate(@companyIDLocal, @RecPeriodEndDate)
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
		cuvt.[CompanyID] = @companyIDLocal 
		AND @RecPeriodEndDate >= rpStart.PeriodEndDate 
		AND ((@RecPeriodEndDate < rpEnd.PeriodEndDate) OR (EndReconciliationPeriodID IS NULL))	
								
	IF(@ProcessGLDataIDListOnlyLocal = 1 OR (SELECT COUNT(ID) FROM #TmpInputGLDataID) > 0) 
	BEGIN
		--- GET NetAccountID for GLDataID`s IF any account is net account
			;WITH cteLockedNetAccounts AS
			(
				SELECT DISTINCT NetAccountID, IsLocked
				FROM
					dbo.fn_SEL_LockedAccounts(@companyIDLocal, @RecPeriodEndDate)
			), cteNetAccountAAV AS
			(
				SELECT DISTINCT AAV.Value
				FROM
					dbo.fn_SEL_AccountAttributeValue(@RecPeriodEndDate, @companyIDLocal) AAV
				WHERE
					AAV. AccountAttributeID =12 --  AccountAttributeID for net Account
			)
			INSERT INTO #TmpGLDataHdrForNetAccount 
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
				INNER JOIN #TmpInputGLDataID TblGLDataID 
					ON GLH.GLDataID  = TblGLDataID.ID 
				INNER JOIN dbo.NetAccountHdr NAH
					ON GLH.NetAccountID = NAH.NetAccountID 
				LEFT JOIN cteLockedNetAccounts LA
					ON GLH.NetAccountID = LA.NetAccountID				        
			WHERE
				--AAV. AccountAttributeID =12 --  AccountAttributeID for net Account
				--AND GLH.IsLocked = 0 --ommit locked accounts in net acct
				(LA.IsLocked = 0 OR LA.IsLocked IS NULL) --ommit locked accounts in net acct
				AND GLH.ReconciliationPeriodID = @recPeriodIDLocal
				AND GLH.AccountID IS NULL --ommit normal accounts 					
				AND NAH.IsActive = 1
				AND NAH.CompanyID = @companyIDLocal 
		--- GET Normal AccountID for GLDataID for which SRA Should be Processed					
			INSERT INTO #TmpGLDataHdrForNormalAccount 
			(
				GLDataID 
				, AccountID 				   
			)
			SELECT
				GLH.GLDataID
				,GLH.AccountID
			FROM  GLDataHdr GLH				
				INNER JOIN #TmpInputGLDataID TblGLDataID 
					ON GLH.GLDataID  = TblGLDataID.ID 
				INNER JOIN [dbo].[vw_Select_ActiveAccounts] AH 
					ON GLH.AccountID = AH.AccountID--this will ommit Net Accounts as AccountID is null for Net Accounts  
				LEFT JOIN dbo.fn_SEL_LockedAccounts(@companyIDLocal, @RecPeriodEndDate) LA
					ON GLH.AccountID = LA.AccountID				        
			WHERE
				GLH.ReconciliationPeriodID = @recPeriodIDLocal
				AND AH.FSCaptionID IS NOT NULL -- ommit P&L Accounts
				AND AH.AccountTypeID IS NOT NULL -- ommit P&L Accounts
				AND AH.AccountID NOT IN (Select AccountID from #tblAccountsInNetAccount)--ommit accounts in net acct
				-- AND GLH.IsLocked = 0 --ommit locked accounts in net acct
				AND (LA.IsLocked = 0 OR LA.IsLocked IS NULL) --ommit locked accounts
	END
	ELSE		
	BEGIN
		--- GET ALL NetAccountID for GLDataID`s for this recperiod
			;WITH cteLockedNetAccounts AS
			(
				SELECT DISTINCT NetAccountID, IsLocked
				FROM
					dbo.fn_SEL_LockedAccounts(@companyIDLocal, @RecPeriodEndDate)
			), cteNetAccountAAV AS
			(
				SELECT DISTINCT AAV.Value
				FROM
					dbo.fn_SEL_AccountAttributeValue(@RecPeriodEndDate, @companyIDLocal) AAV
				WHERE
					AAV. AccountAttributeID =12 --  AccountAttributeID for net Account
			)
			INSERT INTO #TmpGLDataHdrForNetAccount 
			(
				GLDataID 
				,NetAccountID 
			)
			SELECT
				DISTINCT GLH.GLDataID
				,AAV.Value
			FROM 
				cteNetAccountAAV AAV
				INNER JOIN GLDataHdr GLH
					ON CONVERT(NVARCHAR(20),GLH.NetAccountID) = AAV.Value
				INNER JOIN NetAccountHdr NAH
					ON GLH.NetAccountID = NAH.NetAccountID         
				LEFT JOIN cteLockedNetAccounts LA
					ON GLH.NetAccountID = LA.NetAccountID				        
			WHERE
				--AAV. AccountAttributeID =12 --  AccountAttributeID for net Account
				--AND GLH.IsLocked = 0 --ommit locked accounts in net acct
				(LA.IsLocked = 0 OR LA.IsLocked IS NULL) --ommit locked accounts in net acct
				AND GLH.ReconciliationPeriodID = @recPeriodIDLocal
				AND GLH.AccountID IS NULL	--ommit normal accounts 				
				AND NAH.IsActive = 1
				AND NAH.CompanyID = @companyIDLocal 
		--- GET ALL Reconcilable AccountID for for this period					
			INSERT INTO #TmpGLDataHdrForNormalAccount 
			(
				GLDataID 
				, AccountID 				   
			)
			SELECT
				GLH.GLDataID
				,GLH.AccountID
			FROM  GLDataHdr GLH				
				--INNER JOIN AccountReconciliationPeriodFinal  TblARPF
				--ON GLH.AccountID  = TblARPF.AccountID
				INNER JOIN [dbo].[vw_Select_ActiveAccounts] AH 
					ON GLH.AccountID = AH.AccountID--this will ommit Net Accounts as AccountID is null for Net Accounts  
				LEFT JOIN dbo.fn_SEL_LockedAccounts(@companyIDLocal, @RecPeriodEndDate) LA
					ON GLH.AccountID = LA.AccountID				        
			WHERE
				GLH.ReconciliationPeriodID = @recPeriodIDLocal
				AND AH.FSCaptionID IS NOT NULL -- ommit P&L Accounts
				AND AH.AccountTypeID IS NOT NULL -- ommit P&L Accounts
				AND AH.AccountID NOT IN (Select AccountID from #tblAccountsInNetAccount)--ommit accounts in net acct
				AND (LA.IsLocked = 0 OR LA.IsLocked IS NULL) --ommit locked accounts 
				--AND GLH.IsLocked = 0 --ommit locked accounts in net acct
				--AND TblARPF.ReconciliationPeriodID = @recPeriodID
	END

	--*****************************************BEGIN RCC Copy logic ************************************************************

	DECLARE  @UDT_InputInfo AS udt_BigInt_Int_NullableIDTableType 
	DECLARE @udt_tblSourceDestination udt_CopyOpenItemSourceDestinationTableType

	INSERT INTO  @UDT_InputInfo
		SELECT 
			DISTINCT 
			Tbl.AccountID
			,NULL
		FROM
			 #TmpGLDataHdrForNormalAccount Tbl
	UNION ALL

		SELECT
			DISTINCT 
			NULL
			, Tbl.NetAccountID
		FROM #TmpGLDataHdrForNetAccount Tbl


	INSERT INTO @udt_tblSourceDestination
	(
		[InputAccountID]
		,[InputNetAccountID]
		,[InputGLDataID] 
		,[SourceGLDataID] 
		,[SourceRecPeriodID] 		
		,[SourceRecTemplate] 
		,[DestinationGLDataID] 
		,[DestinationRecPeriodID] 		
	 )
		SELECT 
			Tbl.AccountID
			, NULL
			,Tbl.GLDataID
			,S.GLDataID
			,S.RecPeriodID
			,S.AccountTemplate 		
			,Tbl.GLDataID  
			,GL.ReconciliationPeriodID 		
		 FROM  #TmpGLDataHdrForNormalAccount Tbl
				INNER JOIN GLDataHdr GL
				ON GL.GLDataID= Tbl.GLDataID
				LEFT OUTER JOIN [dbo].[fn_GET_SourceInfoToCopyOpenItem] (@UDT_InputInfo, 2) S			
					ON  (Tbl.AccountID = S.AccountID AND S.NetAccountID IS NULL)
	UNION ALL

		SELECT 
			NUll
			, Tbl.NetAccountID
			,Tbl.GLDataID
			,S.GLDataID
			,S.RecPeriodID
			,S.AccountTemplate 		
			,Tbl.GLDataID  
			,GL.ReconciliationPeriodID 		
		 FROM  #TmpGLDataHdrForNetAccount Tbl
				INNER JOIN GLDataHdr GL
				ON GL.GLDataID= Tbl.GLDataID
				LEFT OUTER JOIN [dbo].[fn_GET_SourceInfoToCopyOpenItem] (@UDT_InputInfo, 2) S			
					ON  (Tbl.NetAccountID = S.NetAccountID AND S.AccountID IS NULL)


	
	--6. Copy RecControlCheckList	
		EXEC [dbo].[usp_INS_CopyRecControlCheckList]
		@companyID = @CompanyIDLocal
		,@UDT_SourceDestinationGLDataID = @udt_tblSourceDestination 
		,@ActionTypeID = @ActionTypeIDLocal 		
		,@CarryForwardDateTime = @DateRevisedLocal
		,@CarryForwardBy = @RevisedByLocal
		,@RecPeriodID =@recPeriodIDLocal


	--*****************************************END RCC Copy logic ************************************************************







	IF (@CompanyUnexplainedVarianceThreshold is not null)
		BEGIN 
			;WITH cteRuleRegularAccount AS
			( 
				SELECT
					GLD.GLDataID
					,CASE	
						WHEN @IsRCCCapabilityEnabled = 1 AND IsNull(@RCCCValidationType,0) <> @RCCValidationTypeIDFORExcludeSRA AND RCC.IncompleteRCCCount > 0 THEN NULL
						WHEN SUBUNEXP.IsSubLedgerLoadedWithUnexpVar = 1 THEN NULL
						WHEN @GLBalanceChangeRule IS NOT NULL AND (ABS(UnexplainedVarianceReportingCurrency) < ABS(@CompanyUnexplainedVarianceThreshold)) 
							AND IsNull(GLOO.OpenItemCount,0) = 0 AND IsNull(GLB.IsAccountSRAbyGLBalance,0) = 1 THEN @GLBalanceChangeRule 
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
				INNER JOIN #TmpGLDataHdrForNormalAccount TblNormalAccount 
					ON GLD.GLDataID  = TblNormalAccount.GLDataID
				LEFT JOIN dbo.fn_SEL_GLDataIncompleteRCCCount(@recPeriodIDLocal) RCC
					ON GLD.GLDataID = RCC.GLDataID	
				LEFT JOIN dbo.fn_SEL_GLDataOpenRecItemCountExceptSupportingDetails(@recPeriodIDLocal) GLOO
					ON GLD.GLDataID = GLOO.GLDataID
				LEFT JOIN dbo.fn_SEL_IsAccountSRAbyGLBalanceRule(@companyIDLocal, @recPeriodIDLocal, @RecPeriodEndDate) GLB
					ON GLD.GLDataID = GLB.GLDataID
				LEFT JOIN dbo.fn_SEL_GLDataOpenRecItemCount(@recPeriodIDLocal) GLO
					ON GLD.GLDataID = GLO.GLDataID
				LEFT JOIN dbo.fn_SEL_IsAccountSRAbyZBARule(@companyIDLocal, @RecPeriodIDLocal, @RecPeriodEndDate, @maxSumThresholdLocal, @minSumThresholdLocal, @AccountAttributeIDForZBA) ZBA
					ON GLD.GLDataID = ZBA.GLDataID
				LEFT JOIN dbo.fn_SEL_IsAccountSRAbySubLedgerRule(@companyIDLocal, @RecPeriodIDLocal, @RecPeriodEndDate, @CompanyUnexplainedVarianceThreshold, @AccountAttributeIDForRecTemplate, @RecTemplateIDForSubLedger) SUB
					ON GLD.GLDataID = SUB.GLDataID
				LEFT JOIN dbo.fn_SEL_IsAccountSubLedgerLoadedWithUnexpVar(@companyIDLocal, @RecPeriodIDLocal, @RecPeriodEndDate, @CompanyUnexplainedVarianceThreshold, @AccountAttributeIDForRecTemplate, @RecTemplateIDForSubLedger) SUBUNEXP
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
			UPDATE 
				GLD
			SET 
				GLD.SystemReconciliationRuleID = RRA.SystemReconciliationRuleID
				, GLD.IsSystemReconcilied = RRA.IsSystemReconciled
				, GLD.ReconciliationStatusID = CASE
												 WHEN RRA.SystemReconciliationRuleID IS NOT NULL THEN @preparedReconciliationStatusID
												 WHEN RRA.SystemReconciliationRuleID IS NULL AND ISNULL(GLD.IsSystemReconcilied,0) = 1 THEN @notStartedReconciliationStatusID
												 WHEN RRA.SystemReconciliationRuleID IS NULL AND ISNULL(GLD.IsSystemReconcilied,0) = 0 THEN GLD.ReconciliationStatusID
											   ELSE
												 @notStartedReconciliationStatusID
											   END
				, GLD.ReconciliationStatusDate = @dateRevisedLocal							    
				, GLD.DateRevised = @dateRevisedLocal 
				, GLD.RevisedBy = @revisedByLocal
				, GLD.ChangeSourceIDSRA = @ChangeSourceIDSRALocal
				, GLD.ActionTypeID = @ActionTypeIDLocal
			FROM 
				[dbo].[GLDataHdr] GLD
			INNER JOIN cteSRARegularAccount RRA 
				ON GLD.GLDataID  = RRA.GLDataID
			WHERE GLD.ReconciliationPeriodID = @recPeriodIDLocal 
				AND (IsNull(GLD.SystemReconciliationRuleID,0) <> IsNull(RRA.SystemReconciliationRuleID,0)
					OR IsNull(GLD.IsSystemReconcilied,0) <> IsNull(RRA.IsSystemReconciled,0))

			-- Update for GL Data Hdr Items for Net Accounts only
			;WITH cteRuleNetAccount AS
			(	SELECT
					G.GLDataID 
					,CASE 
						WHEN @IsRCCCapabilityEnabled = 1 AND IsNull(@RCCCValidationType,0) <> @RCCValidationTypeIDFORExcludeSRA AND RCC.IncompleteRCCCount > 0 THEN NULL
						WHEN SUBUNEXP.IsSubLedgerLoadedWithUnexpVar = 1 THEN NULL
						WHEN @GLBalanceChangeRule IS NOT NULL AND (ABS(UnexplainedVarianceReportingCurrency) < ABS(@CompanyUnexplainedVarianceThreshold))
							AND IsNull(GLOO.OpenItemCount,0) = 0 AND IsNull(GLB.IsNetAccountSRAbyGLBalance,0) = 1 THEN @GLBalanceChangeRule 
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
				INNER JOIN #TmpGLDataHdrForNetAccount NAH
					ON G.GLDataID = NAH.GLDataID
				LEFT JOIN dbo.fn_SEL_GLDataIncompleteRCCCount(@recPeriodIDLocal) RCC
					ON G.GLDataID = RCC.GLDataID	
				LEFT JOIN dbo.fn_SEL_GLDataOpenRecItemCountExceptSupportingDetails(@recPeriodIDLocal) GLOO
					ON G.GLDataID = GLOO.GLDataID
				LEFT JOIN dbo.fn_SEL_IsNetAccountSRAbyGLBalanceRule(@companyIDLocal, @recPeriodIDLocal, @RecPeriodEndDate) GLB
					ON G.GLDataID = GLB.GLDataID
				LEFT JOIN dbo.fn_SEL_GLDataOpenRecItemCount(@recPeriodIDLocal) GLO
					ON G.GLDataID = GLO.GLDataID
				LEFT JOIN dbo.fn_SEL_IsNetAccountSRAbyZBARule(@companyIDLocal, @RecPeriodIDLocal, @RecPeriodEndDate, @maxSumThresholdLocal, @minSumThresholdLocal, @AccountAttributeIDForZBA) ZBA
					ON G.GLDataID = ZBA.GLDataID
				LEFT JOIN dbo.fn_SEL_IsNetAccountSRAbySubLedgerRule(@companyIDLocal, @RecPeriodIDLocal, @RecPeriodEndDate, @CompanyUnexplainedVarianceThreshold, @RecTemplateIDForSubLedger) SUB
					ON G.GLDataID = SUB.GLDataID
				LEFT JOIN dbo.fn_SEL_IsNetAccountSubLedgerLoadedWithUnexpVar(@companyIDLocal, @RecPeriodIDLocal, @RecPeriodEndDate, @CompanyUnexplainedVarianceThreshold, @RecTemplateIDForSubLedger) SUBUNEXP
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
			UPDATE 
				G 
			SET 
				G.SystemReconciliationRuleID = RNA.SystemReconciliationRuleID
				, G.IsSystemReconcilied = RNA.IsSystemReconciled
				, G.ReconciliationStatusID = CASE
												 WHEN RNA.SystemReconciliationRuleID IS NOT NULL THEN @preparedReconciliationStatusID
												 WHEN RNA.SystemReconciliationRuleID IS NULL AND ISNULL(G.IsSystemReconcilied,0) = 1 THEN @notStartedReconciliationStatusID
												 WHEN RNA.SystemReconciliationRuleID IS NULL AND ISNULL(G.IsSystemReconcilied,0) = 0 THEN G.ReconciliationStatusID
											 ELSE
											  @notStartedReconciliationStatusID
											 END
				, G.ReconciliationStatusDate = 	@dateRevisedLocal						  
				, G.DateRevised = @dateRevisedLocal 
				, G.RevisedBy = @revisedByLocal
				, G.ChangeSourceIDSRA = @ChangeSourceIDSRALocal
				, G.ActionTypeID = @ActionTypeIDLocal
			OUTPUT inserted.NetAccountID INTO #tblUpdatedNetAccounts
			FROM 
				[dbo].[GLDataHdr] G
			INNER JOIN cteSRANetAccount RNA
				ON G.GLDataID = RNA.GLDataID
			WHERE G.ReconciliationPeriodID = @recPeriodIDLocal 
				AND (IsNull(G.SystemReconciliationRuleID,0) <> IsNull(RNA.SystemReconciliationRuleID,0)
					OR IsNull(G.IsSystemReconcilied,0) <> IsNull(RNA.IsSystemReconciled,0))
				
			-- Mark Constituent accounts of net accounts to not started
			UPDATE 
				G 
			SET 
				 G.IsSystemReconcilied = 0
				, G.SystemReconciliationRuleID = NULL
				, G.ReconciliationStatusID = @notStartedReconciliationStatusID
				, G.ReconciliationStatusDate = 	@dateRevisedLocal						  
				, G.DateRevised = @dateRevisedLocal 
				, G.RevisedBy = @revisedByLocal
				, G.ChangeSourceIDSRA = @ChangeSourceIDSRALocal
				, G.ActionTypeID = @ActionTypeIDLocal
			FROM 
				[dbo].[GLDataHdr] G
			INNER JOIN 
				#tblAccountsInNetAccount A
				ON G.AccountID = A.AccountID 
			INNER JOIN
				#tblUpdatedNetAccounts NA
				ON A.NetAccountID = NA.NetAccountID			
			WHERE G.ReconciliationPeriodID = @recPeriodIDLocal
		END
-- _Copy Temporary Active Attachments
		EXEC [dbo].[usp_INS_CopyTemporaryActiveAttachment_MOP]
			@RecPeriodID = @recPeriodIDLocal
			, @ActionTypeID = @ActionTypeIDLocal
			, @CopyDateTime = @dateRevisedLocal
			, @CopiedBy = @revisedByLocal


	IF EXISTS (SELECT 1 FROM tempdb.dbo.sysobjects WHERE ID = OBJECT_ID('tempdb..#TmpGLDataHdrForNetAccount'))
	BEGIN
		DROP TABLE #TmpGLDataHdrForNetAccount
	END	
	IF EXISTS (SELECT 1 FROM tempdb.dbo.sysobjects WHERE ID = OBJECT_ID('tempdb..#TmpGLDataHdrForNormalAccount'))
	BEGIN
		DROP TABLE #TmpGLDataHdrForNormalAccount
	END	
	IF EXISTS (SELECT 1 FROM tempdb.dbo.sysobjects WHERE ID = OBJECT_ID('tempdb..#tblAccountsInNetAccount'))
	BEGIN
		DROP TABLE #tblAccountsInNetAccount
	END	
	IF EXISTS (SELECT 1 FROM tempdb.dbo.sysobjects WHERE ID = OBJECT_ID('tempdb..#tblUpdatedNetAccounts'))
	BEGIN
		DROP TABLE #tblUpdatedNetAccounts
	END	
	IF EXISTS (SELECT 1 FROM tempdb.dbo.sysobjects WHERE ID = OBJECT_ID('tempdb..#TmpInputGLDataID'))
	BEGIN
		DROP TABLE #TmpInputGLDataID
	END	
END
