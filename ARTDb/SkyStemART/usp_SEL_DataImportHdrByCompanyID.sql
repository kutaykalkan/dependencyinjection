

  
    
  
ALTER PROCEDURE [dbo].[usp_SEL_DataImportHdrByCompanyID]   
  @CompanyID int
 ,@UserID int 
 ,@RoleID int
 
AS  
/*------------------------------------------------------------------------------  
 AUTHOR:   Apoorv  
 DATE CREATED: 18/02/2010  
 PURPOSE:  Get Data Import Status based on the Company, Inlcude  
     - Holiday Calendar  
     - Period End Dates  
     - Subledger Source  
-------------------------------------------------------------------------------  
 MODIFIED		AUTHOR			DESCRIPTION  
 DATE  
 4/20/2011		Manoj Kumar	    making role and userID based
 09/13/2011		Manoj Kumar		For SA All System Admin data will Be Show
 04/11/2012		Manoj kumar		bug fix Remove ambiguity error of DataImportID  
 06/14/2012		Vinay			With NoLock added 
 09/01/2012		Santosh			With NoLock added

-------------------------------------------------------------------------------  
	Exec usp_SEL_DataImportHdrByCompanyID 189,480,2
-------------------------------------------------------------------------------*/  
  
        
IF @RoleID = 2 -- For SA All System Admin data will Be Show

BEGIN

	SELECT    
	   DI.DataImportID  
	  ,DataImportName  
	  ,DI.CompanyID  
	  ,DI.DataImportTypeID  
	  ,DIT.DataImportTypeLabelID  
	  ,DI.DataImportStatusID  
	  ,DIS.DataImportStatusLabelID 
	  ,DI.[FileName]  
	  ,DI.PhysicalPath 
	  ,DI.NetValue
	  ,RecordsImported  
	  ,DI.DateAdded  
	  ,DI.AddedBy 
	  ,DI.RoleID
	  
	FROM   
	  DataImportHdr DI WITH(NOLOCK)
	  INNER JOIN DataImportStatusMst DIS  
	   ON DI.DataImportStatusID = DIS.DataImportStatusID  
	  INNER JOIN DataImportTypeMst DIT  
	   ON DI.DataImportTypeID = DIT.DataImportTypeID 
	  --INNER JOIN UserHdr UH 
	  -- ON DI.AddedBy = UH.LoginID    
	WHERE    
	  DI.CompanyID = @CompanyID 
	  AND  DI.RoleID = @RoleID 
	 -- AND UH.UserID  = @UserID    
	  AND DI.IsActive = 1  
	  AND (DI.DataImportTypeID = 5   
		OR DI.DataImportTypeID = 6  
		OR DI.DataImportTypeID = 7) 
END
ELSE -- For BA 
BEGIN
	SELECT    
	   DI.DataImportID  
	  ,DataImportName  
	  ,DI.CompanyID  
	  ,DI.DataImportTypeID  
	  ,DIT.DataImportTypeLabelID  
	  ,DI.DataImportStatusID  
	  ,DIS.DataImportStatusLabelID 
	  ,DI.[FileName]  
	  ,DI.PhysicalPath 
	  ,DI.NetValue
	  ,RecordsImported  
	  ,DI.DateAdded  
	  ,DI.AddedBy 
	  ,DI.RoleID
	  
	FROM   
	  DataImportHdr DI WITH(NOLOCK) 
	  INNER JOIN DataImportStatusMst DIS  
	   ON DI.DataImportStatusID = DIS.DataImportStatusID  
	  INNER JOIN DataImportTypeMst DIT  
	   ON DI.DataImportTypeID = DIT.DataImportTypeID 
	  INNER JOIN UserHdr UH WITH(NOLOCK) 
	   ON DI.AddedBy = UH.LoginID    
	WHERE    
	  DI.CompanyID = @CompanyID 
	  AND  DI.RoleID = @RoleID 
	  AND UH.UserID  = @UserID    
	  AND DI.IsActive = 1  
	  AND (DI.DataImportTypeID = 5   
		OR DI.DataImportTypeID = 6  
		OR DI.DataImportTypeID = 7) 
END
  

 

