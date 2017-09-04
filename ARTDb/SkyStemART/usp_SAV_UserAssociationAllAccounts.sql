CREATE PROCEDURE [dbo].[usp_SAV_UserAssociationAllAccounts]        
(        
	@IsAllAccounts BIT,        
	@UserID int,  
	@RoleID smallint     
)        
        
AS    
/*------------------------------------------------------------------------------      
 AUTHOR:   Vinay     
 DATE CREATED: 07/10/2017      
 PURPOSE:  Save all account permission by user role    
-------------------------------------------------------------------------------      
 MODIFIED  AUTHOR    DESCRIPTION      
 DATE      
-------------------------------------------------------------------------------      
-------------------------------------------------------------------------------      
--SAMPLE EXECUTION SCript      
     
-------------------------------------------------------------------------------*/    
BEGIN    
    SET NOCOUNT ON    

	IF ((SELECT Count(1) FROM dbo.UserAllAccount 
		WHERE UserID = @UserID
		AND RoleID = @RoleID)<1)
	BEGIN
		INSERT INTO dbo.UserAllAccount
			(
				UserID
				,RoleID
				,IsAllAccount
				,IsActive
			)
		SELECT
			@UserID
			,@RoleID
			,@IsAllAccounts
			,1
	END
	ELSE
	BEGIN
		UPDATE UA SET
			IsAllAccount = @IsAllAccounts
			,IsActive = @IsAllAccounts
		FROM
			dbo.UserAllAccount UA
		WHERE
			UA.UserID = @UserID
			AND UA.RoleID = @RoleID
	END
  
END 