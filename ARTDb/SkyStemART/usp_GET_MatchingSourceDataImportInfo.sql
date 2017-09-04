

ALTER PROCEDURE [Matching].[usp_GET_MatchingSourceDataImportInfo]  
 @MatchingSourceDataImportID BIGINT  
AS  
BEGIN  
/*------------------------------------------------------------------------------  
 AUTHOR:   Santosh Kumar  
 DATE CREATED: 14-Jun-2011
 PURPOSE:  Select the Mathing Data Source Import Info  
		   - Hdr Record + Status + Type - Message  
-------------------------------------------------------------------------------  
 MODIFIED		AUTHOR				 DESCRIPTION  
 DATE  
-------------------------------------------------------------------------------  
9/1/2012		Manoj				Add NOLOCK

-------------------------------------------------------------------------------  
 SAMPLE CALL  
 EXEC Matching.usp_GET_MatchingSourceDataImportInfo  13
-------------------------------------------------------------------------------*/  
SET NOCOUNT ON  
  
	SELECT 
		MADI.MatchingSourceDataImportID
		,MADI.MatchingSourceName
		,MADI.RecordsImported
		,MADI.DateAdded
		,MADI.FileName
		,MADI.PhysicalPath
		,MADI.FileSize
		,MADI.ForceCommitDate
		,MADI.MatchingSourceTypeID
		,DIT.DataImportTypeLabelID 
		,MADI.DataImportStatusID
		,DIS.DataImportStatusLabelID 
		,MADI.Message
		,MADI.UserID
		,MADI.RoleID
	FROM 
		Matching.MatchingSourceDataImportHdr MADI WITH(NOLOCK)
		INNER JOIN DataImportTypeMst DIT    
		ON MADI.MatchingSourceTypeID = DIT.DataImportTypeID    
		INNER JOIN DataImportStatusMst DIS    
		ON MADI.DataImportStatusID = DIS.DataImportStatusID 
	WHERE 
		MADI.MatchingSourceDataImportID=@MatchingSourceDataImportID
  
END 

