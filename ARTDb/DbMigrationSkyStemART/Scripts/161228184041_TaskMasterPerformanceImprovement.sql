SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'TaskMaster.[fn_SEL_TaskOwnersByRecPeriodID]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'
ALTER FUNCTION TaskMaster.[fn_SEL_TaskOwnersByRecPeriodID]
(	
	@RecPeriodID INT
)
RETURNS @TaskOwners TABLE
						(
							TaskID BIGINT
							, AssigneeUserName VARCHAR(2000)
							,ReviewerUserName VARCHAR(2000)
							, ApproverUserName VARCHAR(2000)
							, NotifyUserName VARCHAR(2000)
						 )
AS

/*------------------------------------------------------------------------------
	AUTHOR:			Manoj Kumar
	DATE CREATED:	08/07/2015
	PURPOSE:		Get All Assignee/Reviewer/Approvers/Notify for tasks in the given Recperiod
-------------------------------------------------------------------------------
	MODIFIED		AUTHOR				DESCRIPTION
	DATE
-------------------------------------------------------------------------------

-------------------------------------------------------------------------------
	SAMPLE CALL
	 SELECT * From [TaskMaster].[fn_SEL_TaskOwnersByRecPeriodID](2196) 
-------------------------------------------------------------------------------

-------------------------------------------------------------------------------*/
BEGIN

DECLARE @PeriodEndDate SMALLDATETIME
DECLARE @CompanyID INT
DECLARE @TaskIDTbl AS udt_BigIntIDTableType


	SELECT
		@PeriodEndDate = RP.PeriodEndDate
		, @CompanyID = RP.CompanyID 
	FROM
		ReconciliationPeriod RP
	WHERE
		RP.ReconciliationPeriodID = @RecPeriodID

	DECLARE @TaskAttributeValue AS TABLE(
		TaskAttributeRecPeriodSetID INT,
		TaskID BIGINT, 	
		TaskAttributeID SMALLINT,
		StartRecPeriodID INT,	
		EndRecPeriodID INT,
		ReferenceID BIGINT,
		Value NVARCHAR(500),
		StartRecPeriodEndDate DATETIME,
		EndRecPeriodEndDate DATETIME
	)

	INSERT INTO @TaskAttributeValue 
	SELECT * FROM TaskMaster.fn_SEL_TaskAttributeValue(@PeriodEndDate,@CompanyID)

	INSERT INTO @TaskIDTbl
	SELECT 
		TA.TaskID
	FROM	
		@TaskAttributeValue TA
	WHERE
	TA.TaskAttributeID = 1

	; WITH CTEUser AS
	(
		SELECT 
			UH.UserID 
			,UH.[FirstName] + '' '' + UH.[LastName] AS UserName			
		FROM 
			dbo.UserHdr UH
		WHERE
			UH.CompanyID = @CompanyID	
	)

	INSERT INTO @TaskOwners
	 SELECT
	 T.ID
	 ,	SUBSTRING( 
					(SELECT 
						''; '' + UH.UserName 
					FROM 
						CTEUser UH
						INNER JOIN @TaskAttributeValue TA 
						ON UH.UserID= TA.ReferenceID
					WHERE
						TA.TaskID= T.ID 						
						AND TA.TaskAttributeID = 4 -- Assigned To												
					FOR XML PATH('''')
					)
			,2,200000 ) AS AssigneeUserName

	 ,	SUBSTRING( 
					(SELECT 
						''; '' + UH.UserName  
					FROM 
						CTEUser UH
						INNER JOIN @TaskAttributeValue TA 
						ON UH.UserID= TA.ReferenceID
					WHERE
						TA.TaskID= T.ID 
						--AND UH.CompanyID = @CompanyID
						AND TA.TaskAttributeID = 5 --Reviewer												
					FOR XML PATH('''')
					)
		,2,200000 ) AS ReviewerUserName

	 , SUBSTRING( 
					(SELECT 
						''; '' + UH.UserName
					FROM 
						CTEUser UH
						INNER JOIN @TaskAttributeValue TA 
						ON UH.UserID= TA.ReferenceID
					WHERE
						TA.TaskID= T.ID 
						--AND UH.CompanyID = @CompanyID
						AND TA.TaskAttributeID = 24 --Approver												
					FOR XML PATH('''')
					)
		,2,200000 ) AS ApproverUserName

	 , SUBSTRING( 
					(SELECT 
						''; '' + UH.UserName  
					FROM 
						CTEUser UH
						INNER JOIN @TaskAttributeValue	TA
						ON UH.UserID= TA.ReferenceID
					WHERE
						TA.TaskID= T.ID 						
						AND TA.TaskAttributeID = 6 --Notify												
					FOR XML PATH('''')
					)
		,2,200000 ) AS NotifyUserName
  FROM
		@TaskIDTbl T
	RETURN
END

' 
END

GO


