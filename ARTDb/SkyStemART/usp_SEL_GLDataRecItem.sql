
ALTER PROCEDURE [dbo].[usp_SEL_GLDataRecItem]
	
	@GLDataID BIGINT,
	@RecPeriodID INT,
	@RecCategoryTypeID SMALLINT,
	@GLReconciliationItemInputRecordTypeID SMALLINT,
	@AccountTemplateAttributeID SMALLINT

AS
/*------------------------------------------------------------------------------
	AUTHOR:			Rajiv Ranjan
	DATE CREATED:	03/15/2010
	PURPOSE:		Get All Rec Item input for the given GL Data and Rec Category type
-------------------------------------------------------------------------------
	MODIFIED		AUTHOR				DESCRIPTION
	DATE
	5/12/2011		Manoj Kumar			Add Rec Item #
	5/12/2011		Manoj Kumar			Add Rec Item #
	31/5/2011		Harsh Kuntail		Query Optimization, removed group by clause
	19/08/2011		Prafull Pandey		Additional Join to fetch MatchSetRef#
	01/11/2011		Vinay				Added Custom Exchange Rate fields
	02/14/2012		Abhishek			Added UserName field
	05-June-2012	Santosh				Added Status Column for reports
	09/01/2012		Santosh				With NoLock added
	21/03/2013		Manoj				Add PhysicalPath in select list
	01/29/2014		Manoj				Add rec item Comments logic
-------------------------------------------------------------------------------
exec [dbo].[usp_SEL_GLDataRecItem] 6, 3, 17, 2, 2
-------------------------------------------------------------------------------*/

SELECT 
		GRI.[GLDataRecItemID]
		, GRI.[GLDataID]
		, GRI.[ReconciliationCategoryID]
		, GRI.[ReconciliationCategoryTypeID]
		, GRI.[DataImportID]
		, GRI.[IsAttachmentAvailable]
		, GRI.[Amount]
		, GRI.[LocalCurrencyCode]
		, GRI.[AmountBaseCurrency]
		, GRI.[AmountReportingCurrency]
		, GRI.[OpenDate]
		, GRI.[CloseDate]
		, GRI.[JournalEntryRef]
		, GRI.[Comments]
		, GRI.[CloseComments]
		, GRI.[DateAdded]
		, GRI.[AddedBy]
		, GRI.[RecItemNumber] 
		, GRI.[PreviousGLDataRecItemID]
		, GRI.[OriginalGLDataRecItemID]
		,CASE WHEN GRI.CloseDate IS NULL Then 'Open'
			 Else 'Closed' 
			 END as 'Status'
		, AttachmentCount = ISNULL((SELECT SUM(1) FROM [dbo].[Attachment] A WITH (NOLOCK)
									WHERE GRI.GLDataRecItemID = A.RecordID 
									AND A.RecordTypeID = @GLReconciliationItemInputRecordTypeID 
									AND A.IsActive = 1),0)
		, IsForwardedItem = (CASE 
							   WHEN GRI.PreviousGLDataRecItemID IS NULL
							   THEN CAST (0 AS bit)
							   ELSE CAST (1 AS bit) 
							   END)
		, MSH.MatchSetRef
		, MSH.MatchSetID
		, MSC.MatchSetSubSetCombinationID
		, GRI.ExRateLCCYtoBCCY 
		, GRI.ExRateLCCYtoRCCY
		, UH.FirstName + ' ' + UH.LastName As UserName
		, DI.PhysicalPath
		, IsCommentAvailable  =(CASE 
							   WHEN  ISNULL((SELECT SUM(1) FROM [dbo].[GLDataRecItemComment] GLRC WITH (NOLOCK)
									WHERE GRI.GLDataRecItemID = GLRC.RecItemID
									AND GLRC.RecordTypeID = 2 
									AND GLRC.IsActive = 1),0) > 0
							   THEN CAST (1 AS bit)
							   ELSE CAST (0 AS bit) 
							   END)
		, DI.DataImportTypeID
								
	FROM 
		[dbo].[GLDataRecItem] GRI  WITH (NOLOCK)
		INNER JOIN [dbo].[GLDataHdr] GDH WITH (NOLOCK)
			ON GRI.GLDataID = GDH.GLDataID
		INNER JOIN UserHdr UH WITH (NOLOCK)
			ON GRI.AddedByUserID = UH.UserID
			
		--*******Additional Join to fetch MatchSetRef No. 
		LEFT OUTER JOIN Matching.MatchSetSubSetCombination MSC
			 ON (GRI.RecordSourceID=MSC.MatchSetSubSetCombinationID 
			     AND ISNULL(GRI.RecordSourceTypeID,0)=2)   
		LEFT OUTER JOIN Matching.MatchSetMatchingSourceDataImport  MSM
			 ON MSC.MatchSetMatchingSourceDataImport1ID=MSM.MatchSetMatchingSourceDataImportID
		LEFT OUTER JOIN  Matching.MatchSetHdr MSH WITH (NOLOCK)
			 ON MSM.MatchSetID=MSH.MatchSetID
		LEFT OUTER JOIN DataImportHdr DI
			ON GRI.DataImportID= DI.DataImportID		
	WHERE 
		GRI.IsActive = 1
		AND GDH.GLDataID = @GLDataID
		AND GDH.ReconciliationPeriodID  = @RecPeriodID
		AND ReconciliationCategoryTypeID = @RecCategoryTypeID
		
		
/*Old Logic
SELECT GRI.[GLDataRecItemID]
      , GRI.[GLDataID]
      , GRI.[ReconciliationCategoryID]
      , GRI.[ReconciliationCategoryTypeID]
      , GRI.[DataImportID]
      , GRI.[IsAttachmentAvailable]
      , GRI.[Amount]
      , GRI.[LocalCurrencyCode]
      , GRI.[AmountBaseCurrency]
      , GRI.[AmountReportingCurrency]
      , GRI.[OpenDate]
      , GRI.[CloseDate]
      , GRI.[JournalEntryRef]
      , GRI.[Comments]
      , GRI.[CloseComments]
      , GRI.[DateAdded]
      , GRI.[AddedBy]
      , GRI.[RecItemNumber] 
      , COUNT(A.AttachmentID) As AttachmentCount
      , CASE 
           WHEN GRI.PreviousGLDataRecItemID IS NULL
           THEN CAST (0 AS bit)
           ELSE CAST (1 AS bit) 
           END As IsForwardedItem
	  , UH.FirstName + ' ' + UH.LastName As UserName	               
  FROM [dbo].[GLDataRecItem] GRI  
  LEFT OUTER JOIN [dbo].[Attachment] A 
	ON GRI.GLDataRecItemID = A.RecordID 
		AND A.IsActive = 1
  INNER JOIN [dbo].[GLDataHdr] GDH
	ON GRI.GLDataID = GDH.GLDataID
  INNER JOIN UserHdr UH
	ON GRI.AddedByUserID = UH.UserID
  WHERE 
  GRI.IsActive = 1
  AND GDH.GLDataID = @GLDataID
  AND GDH.ReconciliationPeriodID  = @RecPeriodID
  AND ReconciliationCategoryTypeID = @RecCategoryTypeID
  AND (A.RecordTypeID = @GLReconciliationItemInputRecordTypeID
  OR A.RecordTypeID IS NULL)
  GROUP BY GRI.[GLDataRecItemID]
      , GRI.[GLDataID]
      , GRI.[ReconciliationCategoryID]
      , GRI.[ReconciliationCategoryTypeID]
      , GRI.[DataImportID]
      , GRI.[IsAttachmentAvailable]
      , GRI.[Amount]
      , GRI.[LocalCurrencyCode]
      , GRI.[AmountBaseCurrency]
      , GRI.[AmountReportingCurrency]
      , GRI.[OpenDate]
      , GRI.[CloseDate]
      , GRI.[JournalEntryRef]
      , GRI.[Comments]
      , GRI.[CloseComments]
      , GRI.[DateAdded]
      , GRI.[AddedBy]
      , GRI.[PreviousGLDataRecItemID]
      , GRI.[RecItemNumber]
      , UH.[FirstName] + ' ' + UH.[LastName]
*/      
