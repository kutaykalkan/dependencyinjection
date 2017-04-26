




ALTER PROCEDURE [dbo].[usp_SEL_GLDataUnexplainedVarianceHistory] 
	@GLDataID Int

AS

/*------------------------------------------------------------------------------
	AUTHOR:			Siddharth Parashar
	DATE CREATED:	03/23/2010
	PURPOSE:		Get all the Unexplained Vraiance History for the GL Data ID
-------------------------------------------------------------------------------
	MODIFIED		AUTHOR				DESCRIPTION
	DATE
	2/25/2011		Vinay			Get Data for last 12 reconciled periods from 
									variance history if not available then from GL Data
    2/2/2012	   Abhishek			Fixed issue that comments, Entered By and Date 
									was not getting displayed	
	6/28/2012		Vinay			Bug Fixed, It was showing same description for all past 
									records	also net account handling was missing																
-------------------------------------------------------------------------------
	09/01/2012		Santosh			With NoLock added
-------------------------------------------------------------------------------
	
-------------------------------------------------------------------------------*/

SET NOCOUNT ON

DECLARE @AccountID BIGINT
	,@NetAccountID INT
	,@ReconciliationPeriodID INT
	,@PeriodEndDate DATETIME
	
DECLARE @PrevRecPeriods TABLE
(	ReconciliationPeriodID INT
	,PeriodEndDate DateTime
	,ReportingCurrencyCode CHAR(3)
)	
	
SELECT 
	@AccountID = AGL.AccountID
	,@NetAccountID = AGL.NetAccountID
	,@ReconciliationPeriodID = AGL.ReconciliationPeriodID 
	,@PeriodEndDate = RP.PeriodEndDate
FROM dbo.vw_Select_ActiveGLData AGL 
INNER JOIN dbo.ReconciliationPeriod RP WITH (NOLOCK)
	ON AGL.ReconciliationPeriodID = RP.ReconciliationPeriodID
WHERE GLDataID=@GLDataID

INSERT INTO @PrevRecPeriods
	SELECT 
		TOP 12 ARPF.ReconciliationPeriodID
		,RP.PeriodEndDate
		,RP.ReportingCurrencyCode
	FROM dbo.AccountReconciliationPeriodFinal ARPF
	INNER JOIN dbo.ReconciliationPeriod RP WITH (NOLOCK)
		ON ARPF.ReconciliationPeriodID = RP.ReconciliationPeriodID
	WHERE ARPF.AccountID = @AccountID
		AND RP.PeriodEndDate < @PeriodEndDate
		AND RP.ReconciliationPeriodStatusID!=5
	ORDER BY RP.PeriodEndDate DESC
		
SELECT GLUV.DateAdded
	,CASE 
		WHEN GLUV.AmountReportingCurrency IS NOT NULL THEN GLUV.AmountReportingCurrency
		ELSE AGL.UnexplainedVarianceReportingCurrency
	END AS AmountReportingCurrency
	,GLUV.AddedBy
	,GLUV.Comments
	,PRP.PeriodEndDate
	,PRP.ReportingCurrencyCode
	,UH.FirstName						AS AddedByFirstName  
    ,UH.LastName						AS AddedByLastName  
FROM 
	@PrevRecPeriods PRP
INNER JOIN vw_Select_ActiveGLData AGL 
	ON PRP.ReconciliationPeriodID = AGL.ReconciliationPeriodID
		AND (AGL.AccountID = @AccountID OR AGL.NetAccountID = @NetAccountID)
LEFT JOIN GLDataUnexplainedVariance GLUV 
	ON AGL.GLDataID = GLUV.GLDataID
LEFT JOIN UserHdr UH  WITH (NOLOCK)
	ON GLUV.AddedByUserID=UH.UserID
WHERE GLUV.IsActive =1	

