CREATE PROCEDURE [dbo].[usp_SEL_UserAllAccounts]  
(  
	@UserID INT
	,@RoleID SMALLINT
)  
AS 
/*------------------------------------------------------------------------------
	AUTHOR:			
	DATE CREATED:	
	PURPOSE:		Select User Association All Accounts Flag
-------------------------------------------------------------------------------
	MODIFIED		AUTHOR			DESCRIPTION
	DATE
-------------------------------------------------------------------------------
-------------------------------------------------------------------------------*/ 
BEGIN
	SET NOCOUNT ON
		DECLARE @IsAllAccount BIT = 0

		SELECT
			@IsAllAccount = IsAllAccount
		FROM
			dbo.UserAllAccount UA
		WHERE
			UA.UserID = @UserID
			AND UA.RoleID = @RoleID

		SELECT @IsAllAccount

END

