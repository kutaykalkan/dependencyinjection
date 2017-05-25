
ALTER PROCEDURE [dbo].[usp_GET_DataImportInfo]  
(
	@DataImportID INT  
)
AS  
BEGIN  
/*------------------------------------------------------------------------------  
 AUTHOR:   Apoorv Gupta  
 DATE CREATED: 04/12/2010  
 PURPOSE:  Select the Data Import Info  
     - Hdr Record + Status + Type  
     - Failre Message  
-------------------------------------------------------------------------------  
 MODIFIED			AUTHOR			  DESCRIPTION  
 DATE  
 11/22/2011			Manoj Kumar			Get added by and email id also for sending main on rjecting the import by other user
-------------------------------------------------------------------------------  
21-Feb-2011			Prafull				Additional Column Selection(DataImportID,PhysicalPath ) 
9/1/2012			Manoj				Add NOLOCK
02/27/2015			Vinay				Get Data Import Message Details
03/30/2015			Vinay				Import message category and account changes
04/13/2015			Vinay				Bug Fixed
04/15/2015			Vinay				Taken into separate sp
-------------------------------------------------------------------------------  
 SAMPLE CALL  
 EXEC usp_GET_DataImportInfo 108  
-------------------------------------------------------------------------------*/  
SET NOCOUNT ON  
  
	SELECT 
		DI.DataImportID	  
		,DI.DataImportName  
		,DI.RecordsImported  
		,DI.DateAdded  
		,DI.FileName
		,DI.PhysicalPath  
		,DI.FileSize  
		,DI.ForceCommitDate  
		,DI.DataImportTypeID  
		,DIT.DataImportTypeLabelID  
		,DI.DataImportStatusID  
		,DIS.DataImportStatusLabelID  
		,DIFM.DataImportFailureMessageID  
		,DIFM.FailureMessage
		,DI.AddedBy
		,U.EmailID 
		,DI.RoleID 
	FROM  
		DataImportHdr DI  WITH(NOLOCK) 
	INNER JOIN DataImportTypeMst DIT    
		ON DI.DataImportTypeID = DIT.DataImportTypeID  
	INNER JOIN DataImportStatusMst DIS  
		ON DI.DataImportStatusID = DIS.DataImportStatusID  
	INNER JOIN UserHdr U WITH(NOLOCK)
		ON DI.AddedBy=U.LoginID
	LEFT OUTER JOIN DataImportFailureMessage DIFM WITH(NOLOCK)  
		ON DI.DataImportID = DIFM.DataImportID   
	WHERE   
		DI.DataImportID = @DataImportID  
  
END 
