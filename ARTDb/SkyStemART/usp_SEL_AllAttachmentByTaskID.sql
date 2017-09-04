
CREATE PROCEDURE [dbo].[usp_SEL_AllAttachmentByTaskID] 
(
	@TaskID BIGINT
	,@TaskTypeID SMALLINT 
	,@RecPeriodID INT
	, @UserID INT
	, @RoleID INT
)
AS

/*------------------------------------------------------------------------------
	AUTHOR:			Vinay
	DATE CREATED:	06/09/2017
	PURPOSE:		Get All Attachments of task in given rec period
-------------------------------------------------------------------------------
	MODIFIED		AUTHOR				DESCRIPTION
	DATE
-------------------------------------------------------------------------------
-------------------------------------------------------------------------------
	SAMPLE CALL
-------------------------------------------------------------------------------*/
BEGIN

	DECLARE @FilterCriteriaTable udt_FilterCriteriaTableType 
	DECLARE @TaskCategoryID SMALLINT = NULL
	, @IsShowHidden BIT = 1
	, @LCID INT = 1033
	, @DefaultLCID INT = 1033
	
	
	IF EXISTS (SELECT 1 FROM tempdb.dbo.sysobjects WHERE ID = OBJECT_ID('tempdb..#tmpFilteredAccessableTasks'))
	BEGIN
		DROP TABLE #tmpFilteredAccessableTasks
	END	
	
	CREATE TABLE #tmpFilteredAccessableTasks
	(
		TaskID BIGINT
		, [TaskDetailID]		BIGINT
		, [TaskDescription]		NVARCHAR(500)
		, [TaskName]			NVARCHAR(500)
		, [TaskNumber]			NVARCHAR(500)	
		, [TaskDueDate]			SMALLDATETIME
		, [TaskDetailRecPeriodID] INT
		, [RecPeriodID]			INT
		, [AddedBy]				NVARCHAR(128)
		, [DateAdded]			SMALLDATETIME
		, [TaskTypeID]			SMALLINT
		, [TaskRecurrenceTypeID] SMALLINT
		, [TaskRecurrenceTypeLabelID] INT
		, [TaskListID]			INT
		, [TaskListName]		NVARCHAR(500)
		, [TaskSubListID]		INT
		, [TaskSubListName]		NVARCHAR(500)
		, [AssigneeID]			INT
		, [AssigneeFirstName]	NVARCHAR(128)		
		, [AssigneeLastName]	NVARCHAR(128)	
		, [ReviewerID]			INT
		, [ReviewerFirstName]	NVARCHAR(128)		
		, [ReviewerLastName]	NVARCHAR(128)	
		, [ApproverID]			INT		
		, [ApproverFirstName]	NVARCHAR(128)	
		, [ApproverLastName]	NVARCHAR(128)
		, [AssigneeDueDate]		SMALLDATETIME
		, [ReviewerDueDate]		SMALLDATETIME
		, [TaskDueDays]			INT		
		, [TaskCompletionDate]	SMALLDATETIME
		, [TaskStatusID]		SMALLINT
		, [TaskStatusLabelID]   INT
		, [TaskStatusDate]		SMALLDATETIME
		, [Comment]				NVARCHAR(500)
		, [AddedByUserID]		INT
		, [TaskDetailAddedByFirstName] NVARCHAR(128)
		, [TaskDetailAddedByLastName] NVARCHAR(128)
		, [RevisedByUserID]		INT
		, [TaskDetaiRevisedByFirstName] NVARCHAR(128)
		, [TaskDetaiRevisedByLastName] NVARCHAR(128)
		, [TaskDetailDateRevised] SMALLDATETIME
		, [CreationDocCount]	INT
		, [CompletionDocCount]	INT
		, AccountID BIGINT
		, AccountNameLabelID INT
		, AccountNumber NVARCHAR(20)
		, AccountTypeID SMALLINT
		, AccountTypeLabelID  INT
		, FSCaptionID INT
		, FSCaptionLabelID INT
		, GeographyObjectID INT
		, Key2LabelID INT
		, Key3LabelID INT
		, Key4LabelID INT
		, Key5LabelID INT
		, Key6LabelID INT
		, Key7LabelID INT
		, Key8LabelID INT
		, Key9LabelID INT
		, Key2 NVARCHAR(500)
		, Key3 NVARCHAR(500)
		, Key4 NVARCHAR(500)
		, Key5 NVARCHAR(500)
		, Key6 NVARCHAR(500)
		, Key7 NVARCHAR(500)
		, Key8 NVARCHAR(500)
		, Key9 NVARCHAR(500)
		, IsDeleted BIT
		, CustomField1 NVARCHAR(100)
		, CustomField2 NVARCHAR(100)
	)

		
	INSERT INTO #tmpFilteredAccessableTasks 
	(
		TaskID 
		, TaskDetailID 
		, TaskDescription 
		, TaskNumber
		, TaskName 	
		, TaskDetailRecPeriodID 
		, RecPeriodID 
		, AddedBy 
		, DateAdded 
		, TaskTypeID 
		, TaskDueDate 
		, TaskRecurrenceTypeID 
		, TaskRecurrenceTypeLabelID 
		, TaskListID 
		, TaskListName 
		, TaskSubListID 
		, TaskSubListName 
		, AssigneeID 
		, AssigneeFirstName 
		, AssigneeLastName 
		, ReviewerID
		, ReviewerFirstName
		, ReviewerLastName
		, ApproverID 
		, ApproverFirstName 
		, ApproverLastName 
		, AssigneeDueDate 
		, ReviewerDueDate 
		, TaskDueDays
		, TaskCompletionDate 
		, TaskStatusID 	
		, TaskStatusDate
		, Comment 
		, AddedByUserID 
		, TaskDetailAddedByFirstName
		, TaskDetailAddedByLastName
		, RevisedByUserID
		, TaskDetaiRevisedByFirstName
		, TaskDetaiRevisedByLastName
		, TaskDetailDateRevised
		, CreationDocCount
		, CompletionDocCount
		, AccountID
		, AccountNameLabelID 
		, AccountNumber
		, AccountTypeID
		, AccountTypeLabelID 
		, FSCaptionID 
		, FSCaptionLabelID 
		, GeographyObjectID 
		, Key2LabelID 
		, Key3LabelID 
		, Key4LabelID 
		, Key5LabelID 
		, Key6LabelID 
		, Key7LabelID
		, Key8LabelID 
		, Key9LabelID
		, Key2
		, Key3
		, Key4
		, Key5
		, Key6
		, Key7
		, Key8
		, Key9 
		, IsDeleted 
		, CustomField1 
		, CustomField2
	)
	EXEC [TaskMaster].[usp_GET_AccessableTaskByUserID] 
		@UserID =@UserID
		, @RoleID =@RoleID
		, @RecPeriodID = @RecPeriodID
		, @TaskTypeID = @TaskTypeID
		, @FilterCriteriaTable = @FilterCriteriaTable
		, @TaskCategoryID = @TaskCategoryID
		, @IsShowHidden = @IsShowHidden
		, @LCID = @LCID
		, @DefaultLCID = @DefaultLCID
		
--Select * from #tmpFilteredAccessableTasks	
	
	SELECT 
		A.[AttachmentID]
		, A.[FileName]
		, A.[PhysicalPath]		
		, A.[DocumentName]
		, A.[RecordID]
		, A.[FileSize]
		, A.[RecordTypeID]		
	FROM 
		[Attachment] A WITH (NOLOCK)
	WHERE
		A.RecordID = @TaskID AND A.RecordTypeID = 4 -- RecordTypeID 4 for Task Creation
		AND A.IsActive = 1
		
UNION -- Get Account Task Complition Attachments
		
	SELECT 
		A.[AttachmentID]
		, A.[FileName]
		, A.[PhysicalPath]		
		, A.[DocumentName]
		, A.[RecordID]
		, A.[FileSize]
		, A.[RecordTypeID]		
	FROM 
		[Attachment] A WITH (NOLOCK)
		INNER JOIN #tmpFilteredAccessableTasks TblAcocountTask
		ON A.RecordID= TblAcocountTask.TaskDetailID AND A.RecordTypeID = 5 -- RecordTypeID 5 for Task Complition
	WHERE 
		A.IsActive = 1

	IF EXISTS (SELECT 1 FROM tempdb.dbo.sysobjects WHERE ID = OBJECT_ID('tempdb..#tmpFilteredAccessableTasks'))
	BEGIN
		DROP TABLE #tmpFilteredAccessableTasks
	END	
		
END
