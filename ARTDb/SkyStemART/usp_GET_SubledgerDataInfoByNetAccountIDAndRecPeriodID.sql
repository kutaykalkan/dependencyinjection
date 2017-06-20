
  
ALTER PROCEDURE [dbo].[usp_GET_SubledgerDataInfoByNetAccountIDAndRecPeriodID]  
 @NetAccountID BIGINT,  
 @RecPeriodID INT  
AS  
BEGIN  
/*------------------------------------------------------------------------------  
 AUTHOR:   Manoj Kumar
 DATE CREATED: 09/30/2011
 PURPOSE:  Get the SubledgerDataInfo By netAccountId and RecPeriodID
-------------------------------------------------------------------------------  
 MODIFIED  AUTHOR    DESCRIPTION  
 DATE   
 -------------------------------------------------------------------------  
 9/1/2012		Manoj      				 Add NOLOCK
 3/21/2013		Manoj					 use inner join with DataImportHdr

-------------------------------------------------------------------------------  
 SAMPLE CALL  
  Exec  usp_GET_SubledgerDataInfoByNetAccountIDAndRecPeriodID  70,468
-------------------------------------------------------------------------------*/  
SET NOCOUNT ON  
	DECLARE @Tbl As udt_intIDTableType 
	INSERT INTO @Tbl VALUES(@NetAccountID)
	
	SELECT  TOP 1   
		SD.SubledgerDataID		
		,DIH.DataImportID
		,DIH.PhysicalPath  	
		,DIH.DataImportTypeID	
	FROM
		 SubledgerData SD  WITH(NOLOCK)
		 INNER JOIN DataImportHdr DIH
		 ON SD.DataImportID= DIH.DataImportID	
	WHERE
		SD.AccountID IN (SELECT AccountID  
					  FROM fn_SEL_ConstituentAccountsForNetAccountsByRecPeriodID(@Tbl,@RecPeriodID)) 
		AND SD.RecPeriodID=@RecPeriodID 
		AND SD.IsActive=1
	ORDER BY SD.DataImportID DESC    

   
END  
  
  
