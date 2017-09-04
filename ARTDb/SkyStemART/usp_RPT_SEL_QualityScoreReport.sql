
ALTER PROCEDURE [dbo].[usp_RPT_SEL_QualityScoreReport]
 @tblKeyValue udt_EntityKeyValue READONLY, 
 @RequesterUserID INT,
 @RequesterRoleID SMALLINT,   
 @ReconciliationPeriodID INT,
 @CompanyID SMALLINT,    
 @FromAccountNumber VARCHAR(20),  
 @ToAccountNumber VARCHAR(20),
 @IsKeyAccount BIT,  
 @IsMaterialAccount BIT,   
 @RiskRatingIDs VARCHAR(128), 
 @KeyAccountAttributeId SMALLINT,  
 @RiskRatingAttributeId SMALLINT,    
 @LCID INT,  
 @BusinessEntityID INT,  
 @DefaultLCID INT, 
 @ExcludeNetAccount BIT,
 @IsRequesterUserIDToBeConsideredForPermission BIT, 
 @IsZeroBalanceAccountEnabled BIT ,
 @QualityScoreIDs VARCHAR(MAX) 
AS  
  
/*------------------------------------------------------------------------------  
 AUTHOR:   Abhishek Singh 
 DATE CREATED: 12/01/2011  
 PURPOSE:  Quality Score Report 
 displaying Account hierarchy/ Score/ checklist items #/Period/
-------------------------------------------------------------------------------  
 MODIFIED		AUTHOR			 DESCRIPTION  
 DATE 
 05/17/2012		Manoj Kumar		 modify sp for performance issue
 01/09/2102		Manish				Adding WITH(NOLOCK)
 09/26/2012		Vinay			Production Bug Fixed
 01/24/2013		Manoj			Add QualityScoreDescLabelID
 05/26/2015		Manoj				- use  Filter Value Separator for serching
-------------------------------------------------------------------------------*/  
BEGIN
		SET NOCOUNT ON;  
		DECLARE @AccountHdr AccountHdrType
		DECLARE @DefaultBusinessEntityID INT = 0
		DECLARE @GLDataIDsTable udt_BigIntIDTableType
		DECLARE @tblGLDataIDWithKey TABLE(ID BIGINT IDENTITY, GLDataID BIGINT) 
		DECLARE @BatchSize INT = 15000
			, @MinID BIGINT, @MaxID BIGINT
			, @StartID BIGINT, @EndID BIGINT

		DECLARE @RecPeriodEndDate DATETIME
		DECLARE  @QualityScoreIDsTable TABLE ( QualityScoreID NVARCHAR(10)) 
		DECLARE @FilterValueSeparator CHAR(1) = dbo.fn_Get_AppSettingValue ('FilterValueSeparator')
		CREATE TABLE #QSResult
		(
			GLDataID BIGINT				
			,SystemQualityScoreStatusID INT
			,UserQualityScoreStatusID INT
			,SystemScore SMALLINT
			,UserScore SMALLINT
			,AccountNumber VARCHAR(20)			
			,AccountNameLabelID INT
			,NetAccountLabelID INT
			,NetAccountID INT				
			,FSCaptionID INT	
			,QualityScoreNumber NVARCHAR(10)
			,[Description] NVARCHAR(500)
			,QualityScoreDescLabelID INT
			,[QualityScoreID] INT
			, Comments NVARCHAR(500)			
			, Key2LabelID INT
			, Key3LabelID INT
			, Key4LabelID INT
			, Key5LabelID INT
			, Key6LabelID INT
			, Key7LabelID INT
			, Key8LabelID INT
			, Key9LabelID INT
		)

		
		INSERT INTO	
			@AccountHdr
		SELECT
			AAUR.AccountID
		FROM
			[dbo].[fn_SEL_ReconcilableAccessibleRegularAccounts](@RequesterUserID, @RequesterRoleID, @ReconciliationPeriodID) AAUR
			
		
		CREATE TABLE #AccountListAfterFiltering (AccountID BIGINT);
		INSERT INTO 
			#AccountListAfterFiltering  
		EXEC  usp_RPT_SEL_AccountIDsByFilters 
			 @AccountHdr,
			 @tblKeyValue ,
			 @RequesterUserID ,
			 @RequesterRoleID ,  
			 @ReconciliationPeriodID ,  
			 @CompanyID ,  
			 @FromAccountNumber ,  
			 @ToAccountNumber ,			   
			 @IsKeyAccount ,  
			 @IsMaterialAccount ,  
			 @RiskRatingIDs ,
			 NULL,--@ReconciliationStatusIDs ,			 
			 @KeyAccountAttributeId ,  
			 @RiskRatingAttributeId ,			   
			 @LCID ,  
			 @BusinessEntityID ,  
			 @DefaultLCID ,			 
			 @ExcludeNetAccount ,
			 @IsRequesterUserIDToBeConsideredForPermission 

		CREATE TABLE #AccountListAfterFilteringWithNetAccount (NetAccountID INT); 
		INSERT INTO #AccountListAfterFilteringWithNetAccount  
		EXEC  usp_RPT_SEL_AccountListAfterFilteringWithNetAccount  
			 @tblKeyValue ,
			 @RequesterUserID ,
			 @RequesterRoleID ,  
			 @ReconciliationPeriodID ,  
			 @CompanyID ,  
			 @FromAccountNumber ,  
			 @ToAccountNumber ,			   
			 @IsKeyAccount ,  
			 @IsMaterialAccount ,  
			 @RiskRatingIDs ,
			  NULL,			 
			 @KeyAccountAttributeId ,  
			 @RiskRatingAttributeId ,			   
			 @LCID ,  
			 @BusinessEntityID ,  
			 @DefaultLCID ,			 
			 @ExcludeNetAccount ,
			 @IsRequesterUserIDToBeConsideredForPermission
		
		-- Get Reconciliation period END date  
		SELECT 
			@RecPeriodEndDate =  RP.PeriodEndDate    
		FROM 
			ReconciliationPeriod RP   WITH(NOLOCK)
		WHERE 
			RP.ReconciliationPeriodID = @ReconciliationPeriodID
			
	
		INSERT INTO @tblGLDataIDWithKey -- @GLDataIDsTable 
			SELECT 
				GLD.GLDataID 
			FROM 
				#AccountListAfterFiltering A  
				INNER JOIN [dbo].[vw_Select_AccountKeyInfo] vw
				ON A.AccountID= vw.AccountID 
				INNER JOIN --TODO: what if no row ( then perhaps we wont count it in exception)
				vw_Select_AccountGLData GLD
				ON vw.AccountID = GLD.AccountID
				AND GLD.ReconciliationPeriodID = @ReconciliationPeriodID
		
		INSERT INTO @tblGLDataIDWithKey -- @GLDataIDsTable 
			SELECT 
				 GLD.GLDataID 
			FROM
				dbo.fn_SEL_ReconcilableAccessibleNetAccounts(@RequesterUserID, @RequesterRoleID, @ReconciliationPeriodID ) A
				INNER Join #AccountListAfterFilteringWithNetAccount AF
				ON AF.NetAccountID=A.NetAccountID 
				INNER JOIN vw_Select_AccountGLData GLD
				ON A.NetAccountID = GLD.NetAccountID AND GLD.ReconciliationPeriodID = @ReconciliationPeriodID 

		--Select * from #AccountListAfterFilteringWithNetAccount

		
		INSERT INTO @QualityScoreIDsTable( QualityScoreID)
			SELECT
				T.StringID
			FROM dbo.fn_ConvertStringToTable (@QualityScoreIDs,@FilterValueSeparator) T 
 
		SELECT @MinID = MIN (ID), @MaxID = MAX (ID) FROM @tblGLDataIDWithKey
		SELECT @StartID = @MinID, @EndID = @MinID + @BatchSize -1
		IF (@EndID > @MaxID)
			SELECT @EndID = @MaxID 
		WHILE @StartID <= @EndID
		BEGIN
		print @StartID
		print GETDATE()
		INSERT INTO @GLDataIDsTable SELECT GLDataID FROM @tblGLDataIDWithKey WHERE ID >= @StartID AND ID <= @EndID

		INSERT INTO #QSResult
		SELECT 
			CQS.GLDataID				
			,SYSQSST.QualityScoreStatusLabelID AS SystemQualityScoreStatusID
			,USERQSST.QualityScoreStatusLabelID AS UserQualityScoreStatusID
			,CASE WHEN CQS.SystemQualityScoreStatusID = 2 THEN 1 END SystemScore
			,	CASE WHEN CQS.UserQualityScoreStatusID IS NULL THEN
				CASE WHEN CQS.SystemQualityScoreStatusID = 2 THEN 1 END
				ELSE 
				CASE WHEN CQS.UserQualityScoreStatusID = 2 THEN 1 END
				END AS UserScore
			,AGD.AccountNumber AS AccountNumber				
			,AGD.AccountNameLabelID
			,NA.NetAccountLabelID 
			,NA.NetAccountID 				
			,AGD.FSCaptionID	
			,QSMST.QualityScoreNumber
			,QSMST.[Description]
			,QSMST.[DescriptionLabelID]AS QualityScoreDescLabelID
			,QSMST.[QualityScoreID]
			,GQS.Comments			
			, vw.Key2LabelID
			, vw.Key3LabelID
			, vw.Key4LabelID
			, vw.Key5LabelID
			, vw.Key6LabelID
			, vw.Key7LabelID
			, vw.Key8LabelID
			, vw.Key9LabelID
		FROM 
			QualityScore.fn_SEL_CalculateQualityScores(@ReconciliationPeriodID,@GLDataIDsTable,0) CQS
			LEFT OUTER JOIN vw_Select_AccountGLData AGD
				ON CQS.GLDataID = AGD.GLDataID
			LEFT OUTER JOIN vw_Select_AccountKeyInfo vw
				ON AGD.AccountID = vw.AccountID
			LEFT OUTER JOIN NetAccountHdr NA
				ON AGD.NetAccountID = NA.NetAccountID
			INNER JOIN QualityScore.QualityScoreMst QSMST
				ON CQS.QualityScoreID = QSMST.QualityScoreID
			LEFT OUTER JOIN QualityScore.QualityScoreStatusMst SYSQSST
				ON CQS.SystemQualityScoreStatusID = SYSQSST.QualityScoreStatusID
			LEFT OUTER JOIN QualityScore.QualityScoreStatusMst USERQSST
				ON CQS.UserQualityScoreStatusID = USERQSST.QualityScoreStatusID
			INNER JOIN @QualityScoreIDsTable QSID 
				ON CQS.QualityScoreID = QSID.QualityScoreID
			LEFT OUTER JOIN QualityScore.GLDataQualityScore GQS
				ON CQS.GLDataID = GQS.GLDataID AND CQS.CompanyQualityScoreID = GQS.CompanyQualityScoreID
		WHERE IsNull(GQS.IsActive,1) = 1
		ORDER BY AGD.AccountNumber, QSMST.SortOrder
		
		SELECT @StartID = @EndID + 1
		SELECT @EndID = @StartID + @BatchSize - 1
		IF (@EndID > @MaxID)
			SELECT @EndID = @MaxID 
		END
		print @EndID
		print GETDATE()

		SELECT * FROM #QSResult
DROP TABLE #AccountListAfterFiltering
DROP TABLE #AccountListAfterFilteringWithNetAccount
DROP TABLE #QSResult

END
