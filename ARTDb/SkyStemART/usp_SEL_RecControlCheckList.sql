
ALTER PROCEDURE [dbo].[usp_SEL_RecControlCheckList]
@GLDataID BIGINT           
,@RecPeriodID INT

AS    

/*------------------------------------------------------------------------------
	AUTHOR				Manoj Kumar
	Creation Date		09/16/2014
	Purpose:			Get RecControl CheckList By GlDataIDs 
-------------------------------------------------------------------------------
	MODIFIED		AUTHOR				DESCRIPTION
	DATE
-------------------------------------------------------------------------------
	10/22/2014		Manoj				Bug Fixed
	11/05/2014		Manoj				UAT Change- remove rec control chacklist of reconciliation status basis
	11/12/2014		manoj				UAT Change- bug Fixed
	11/13/2014		manoj				UAT Change- make call for single gldata id
	11/20/2014		Manoj				UAT Changes- bug fixed
	02/04/2015		Girish				Column Name Change (CompletedRecStatus,ReviewedRecStatus)
	02/16/2015		Manoj				SRA Changes
			
-------------------------------------------------------------------------------

-------------------------------------------------------------------------------*/ 
BEGIN
	DECLARE @CompanyID INT,
			@PeriodEndDate DATETIME			
	DECLARE @ReconciliationStatusIDForPrepared SMALLINT = 1
	DECLARE @ReconciliationStatusIDForInProgress SMALLINT = 2
	DECLARE @ReconciliationStatusIDForPendModPreparer SMALLINT = 4
	DECLARE @ReconciliationStatusIDForNotStarted SMALLINT = 8
	DECLARE @ReconciliationStatusIDForGL SMALLINT
	DECLARE @IsSystemReconcilied BIT

	SELECT
		@CompanyID= RP.CompanyID
		,@PeriodEndDate= RP.PeriodEndDate 
	FROM 
		ReconciliationPeriod RP
	WHERE
		RP.ReconciliationPeriodID= @RecPeriodID
		
	SELECT 		
		 @ReconciliationStatusIDForGL=AGL.ReconciliationStatusID
		 ,@IsSystemReconcilied=AGL.IsSystemReconcilied		      
	FROM 
		dbo.vw_Select_ActiveGLData AGL	
	WHERE
		AGL.GLDataID= @GLDataID
		AND AGL.ReconciliationPeriodID= @RecPeriodID
		
	;WITH  CTE_AGL AS
	(
	SELECT 
		
		AGL.AccountID
		 ,AGL.NetAccountID
		 ,AGL.GLDataID		      
	FROM 
		dbo.vw_Select_ActiveGLData AGL			
	WHERE
		AGL.GLDataID= @GLDataID
		AND AGL.ReconciliationPeriodID= @RecPeriodID	
	
	)
	
	
	, CTE_RCCLAccount AS
	(
	
	SELECT 
		 RCCL.RecControlChecklistID
		 ,RCCL.DescriptionLabelID
		 ,RCCL.CheckListNumber
		 ,RCCL.RoleID
		 ,RCCL.StartRecPeriodID
		 ,RCCL.EndRecPeriodID
		 ,RCCL.DataImportID
		 ,RCCL.AddedByUserID
		 ,RCCL.DateAdded
		 ,RCCL.AddedBy
		 ,RCCLA.AccountID
		 ,RCCLA.NetAccountID
		 ,DIH.PhysicalPath
		 ,DIH.DataImportTypeID			       
	FROM 
		dbo.fn_SEL_RecControlChecklist(@CompanyID,@PeriodEndDate) RCCL
		LEFT OUTER JOIN DataImportHdr DIH
			ON RCCL.DataImportID= DIH.DataImportID  
		INNER JOIN dbo.fn_SEL_RecControlChecklistAccount(@CompanyID,@PeriodEndDate) RCCLA 
			ON RCCL.RecControlChecklistID= RCCLA.RecControlChecklistID
		INNER JOIN CTE_AGL 
			ON(RCCLA.AccountID=CTE_AGL.AccountID OR RCCLA.NetAccountID= CTE_AGL.NetAccountID)
	WHERE
		RCCLA.IsActive = 1
		AND RCCL.IsActive =1 
	
	)
	, CTE_RCCL AS
	(
	SELECT 
		 RCCL.RecControlChecklistID
		 ,RCCL.CheckListNumber
		 ,RCCL.DescriptionLabelID
		 ,RCCL.RoleID
		 ,RCCL.StartRecPeriodID
		 ,RCCL.EndRecPeriodID
		 ,RCCL.DataImportID
		 ,RCCL.AddedByUserID
		 ,RCCL.DateAdded
		 ,RCCL.AddedBy
		 ,RCCLA.AccountID
		 ,RCCLA.NetAccountID
		 ,DIH.PhysicalPath		
		 ,DIH.DataImportTypeID	      
	FROM 
		dbo.fn_SEL_RecControlChecklist(@CompanyID,@PeriodEndDate) RCCL 
		LEFT OUTER JOIN dbo.DataImportHdr DIH
			ON RCCL.DataImportID= DIH.DataImportID 			 
		LEFT OUTER JOIN dbo.fn_SEL_RecControlChecklistAccount(@CompanyID,@PeriodEndDate) RCCLA 
			ON RCCL.RecControlChecklistID = RCCLA.RecControlChecklistID AND RCCLA.IsActive = 1
	WHERE 
		RCCLA.RecControlChecklistID IS NULL 
		AND RCCL.IsActive = 1
	)
	
	, CTE_GLRCCL_Comment AS
	(
		SELECT	 DISTINCT	
			GLRCCL.GLDataRecControlCheckListID		
			 , CASE WHEN GLRCCLC.GLDataRecControlCheckListID IS NOT NULL
					THEN CONVERT(BIT ,1)
					ELSE CONVERT(BIT ,0)
				END AS IsCommentAvailable		      
		FROM 
			dbo.vw_Select_ActiveGLData AGL			
			LEFT JOIN dbo.GLDataRecControlCheckList GLRCCL
				ON GLRCCL.GLDataID= @GLDataID  AND  GLRCCL.IsActive = 1
			LEFT OUTER JOIN dbo.GLDataRecControlCheckListComment GLRCCLC
				ON GLRCCL.GLDataRecControlCheckListID= GLRCCLC.GLDataRecControlCheckListID AND GLRCCLC.IsActive=1
		WHERE
			AGL.GLDataID= @GLDataID
			AND AGL.ReconciliationPeriodID= @RecPeriodID	
	)
	 
	, CTE_ActiveGL AS
	(
	SELECT 
		 GLRCCL.RecControlChecklistID		
		 ,AGL.AccountID
		 ,AGL.NetAccountID
		 ,AGL.GLDataID
		 ,AGL.ReconciliationStatusID
		 ,AGL.IsSystemReconcilied	
		 ,GLRCCL.GLDataRecControlCheckListID
		 ,GLRCCL.CompletedRecStatus
		 ,GLRCCL.CompletedBy
		 ,GLRCCL.ReviewedRecStatus
		 ,GLRCCL.ReviewedBy	 
		 ,GLRCCLC.IsCommentAvailable		      
	FROM 
		dbo.vw_Select_ActiveGLData AGL		
		LEFT JOIN dbo.GLDataRecControlCheckList GLRCCL
			ON GLRCCL.GLDataID= @GLDataID AND  GLRCCL.IsActive = 1
		LEFT OUTER JOIN CTE_GLRCCL_Comment GLRCCLC
			ON GLRCCL.GLDataRecControlCheckListID= GLRCCLC.GLDataRecControlCheckListID 
	WHERE
		AGL.GLDataID= @GLDataID
		AND AGL.ReconciliationPeriodID= @RecPeriodID	
	
	)
	
	
	
	SELECT 
		 RCCLAccount.RecControlChecklistID
		 ,RCCLAccount.DescriptionLabelID
		 ,RCCLAccount.CheckListNumber
		 ,RCCLAccount.RoleID
		 ,RCCLAccount.StartRecPeriodID
		 ,RCCLAccount.EndRecPeriodID
		 ,RCCLAccount.DataImportID
		 ,RCCLAccount.AddedByUserID	
		 ,RCCLAccount.DateAdded
		 ,RCCLAccount.AddedBy
		 ,RCCLAccount.PhysicalPath		 
		 ,RCCLAccount.DataImportTypeID
		 ,GLRCCL.AccountID
		 ,GLRCCL.NetAccountID		 
		 ,GLRCCL.GLDataRecControlCheckListID
		 ,GLRCCL.CompletedRecStatus
		 ,GLRCCL.CompletedBy
		 ,GLRCCL.ReviewedRecStatus
		 ,GLRCCL.ReviewedBy	
		 ,GLRCCL.IsCommentAvailable	   	       
	FROM  
		CTE_RCCLAccount RCCLAccount 
		LEFT OUTER  JOIN CTE_ActiveGL GLRCCL
			ON RCCLAccount.RecControlChecklistID= GLRCCL.RecControlCheckListID 
			AND( RCCLAccount.AccountID= GLRCCL.AccountID OR RCCLAccount.NetAccountID= GLRCCL.NetAccountID)
			
UNION
	SELECT 
		 RCCL.RecControlChecklistID
		 ,RCCL.DescriptionLabelID
		 ,RCCL.CheckListNumber
		 ,RCCL.RoleID
		 ,RCCL.StartRecPeriodID
		 ,RCCL.EndRecPeriodID		
		 ,RCCL.DataImportID
		 ,RCCL.AddedByUserID
		 ,RCCL.DateAdded
		 ,RCCL.AddedBy
		 ,RCCL.PhysicalPath	
		 ,RCCL.DataImportTypeID
		 ,GLRCCL.AccountID
		 ,GLRCCL.NetAccountID		 
		 ,GLRCCL.GLDataRecControlCheckListID
		 ,GLRCCL.CompletedRecStatus
		 ,GLRCCL.CompletedBy
		 ,GLRCCL.ReviewedRecStatus
		 ,GLRCCL.ReviewedBy	 
		 ,GLRCCL.IsCommentAvailable	       
	FROM 
		CTE_RCCL RCCL		
		LEFT JOIN CTE_ActiveGL GLRCCL
			ON  RCCL.RecControlCheckListID = GLRCCL.RecControlChecklistID 
	WHERE		
			
		@ReconciliationStatusIDForGL = @ReconciliationStatusIDForInProgress
		OR @ReconciliationStatusIDForGL = @ReconciliationStatusIDForPendModPreparer
		OR @ReconciliationStatusIDForGL = @ReconciliationStatusIDForNotStarted 						
		OR( @ReconciliationStatusIDForGL = @ReconciliationStatusIDForPrepared  AND @IsSystemReconcilied = 1)
		OR RCCL.RecControlCheckListID = GLRCCL.RecControlChecklistID 
	 	
		
		
END
