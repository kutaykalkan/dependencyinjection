
ALTER PROCEDURE [dbo].[usp_SAV_AutoSaveAttributeValue]  
(  
	@udtAutoSaveAttributeValue udt_AutoSaveAttributeValue READONLY
	,@DateRevised smalldatetime
	,@RevisedBy NVARCHAR(128)
)  
AS 
/*------------------------------------------------------------------------------
	AUTHOR:			
	DATE CREATED:	
	PURPOSE:		Auto Save User Selection
-------------------------------------------------------------------------------
	MODIFIED		AUTHOR			DESCRIPTION
	DATE
-------------------------------------------------------------------------------
-------------------------------------------------------------------------------*/ 
BEGIN
	SET NOCOUNT ON
	
	UPDATE ASA
	SET
		ASA.ReferenceID = ASAI.ReferenceID
		, ASA.Value = ASAI.Value
		, ASA.RevisedBy = @RevisedBy
		, ASA.DateRevised = @DateRevised
		, ASA.IsActive = ASAI.IsActive
	FROM
		dbo.AutoSaveAttributeValue ASA
	INNER JOIN
		@udtAutoSaveAttributeValue ASAI
		ON ASA.AutoSaveAttributeValueID = ASAI.AutoSaveAttributeValueID
			AND ASA.AutoSaveAttributeID = ASAI.AutoSaveAttributeID
			AND ASA.UserID = ASAI.UserID
			AND IsNull(ASA.RoleID,0) = IsNull(ASAI.RoleID,0)

	INSERT INTO dbo.AutoSaveAttributeValue
		(
			AutoSaveAttributeID
			,UserID
			,RoleID
			,ReferenceID
			,Value
			,IsActive
			,DateAdded
			,AddedBy
		)
		SELECT
			AutoSaveAttributeID
			,UserID
			,RoleID
			,ReferenceID
			,Value
			,IsActive
			,@DateRevised
			,@RevisedBy
		FROM
			@udtAutoSaveAttributeValue
		WHERE
			AutoSaveAttributeValueID IS NULL
END
