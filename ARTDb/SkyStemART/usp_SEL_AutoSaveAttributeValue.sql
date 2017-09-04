
ALTER PROCEDURE [dbo].[usp_SEL_AutoSaveAttributeValue]  
(  
	@UserID INT
	,@RoleID SMALLINT
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
	
		SELECT
			AutoSaveAttributeValueID
			,AutoSaveAttributeID
			,UserID
			,RoleID
			,ReferenceID
			,Value
			,IsActive
			,DateAdded
			,AddedBy
		FROM
			dbo.AutoSaveAttributeValue
		WHERE
			UserID = @UserID
			AND (RoleID IS NULL OR RoleID = @RoleID)
END
