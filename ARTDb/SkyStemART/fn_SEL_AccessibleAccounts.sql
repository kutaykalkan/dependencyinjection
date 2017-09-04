
/****** Object:  UserDefinedFunction [dbo].[fn_SEL_AccessibleAccounts]    Script Date: 7/13/2017 7:22:02 AM ******/
DROP FUNCTION [dbo].[fn_SEL_AccessibleAccounts]
GO

/****** Object:  UserDefinedFunction [dbo].[fn_SEL_AccessibleAccounts]    Script Date: 7/13/2017 7:22:02 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE FUNCTION [dbo].[fn_SEL_AccessibleAccounts]
(	
	@UserID INT, 
	@RoleID SMALLINT,
	@RecPeriodID INT
)
RETURNS @AccessibleAccounts TABLE(AccountID BIGINT)
AS

/*------------------------------------------------------------------------------
	AUTHOR		:	Apoorv Gupta
	DATE CREATED:	04/20/2011
	PURPOSE		:	Get All the Accounts Accessible for the User + Role
-------------------------------------------------------------------------------
	MODIFIED		AUTHOR				DESCRIPTION
	DATE
-------------------------------------------------------------------------------
	9/28/2011		Vinay			Modified to ignore account which don't have active key info
	3/27/2012		Vinay			Added roles for Backup Owners
	5/16/2012		Vinay			Commented code for Audit Role, GL Data handling is done in reconcilable accessible accounts
	6/1/2012		Vinay			Geography Object optimization
	01/09/2102		Manish			Adding WITH(NOLOCK)
	01/14/2014		Vinay			Accounts should not be visibile to A/BA when dual level review is off
-------------------------------------------------------------------------------
	SAMPLE CALL
	SELECT * FROM dbo.fn_SEL_AccessibleAccounts(89, 3, 214)
-------------------------------------------------------------------------------*/
BEGIN
	/*

	Check based on ROle
	If Role = SkyStem Admin / System Admin / CEO/CFO
		Return All Accounts as available in the System

	If Role = P / R / A
		Returns Accounts assigned to them in the Rec Period

	If Role = BA / Controller / Executive / AM / FM
		Return Accounts as assigned to them thru a combination of Geo + Direct Account Association

	*/


	DECLARE @RecPeriodEndDate DATETIME 
	DECLARE @CompanyID INT

	DECLARE @GeographyObjectAssignedToTheUser TABLE(KeyLabelID INT, KeySize INT)
	DECLARE @CountGeographyObjects INT
	DECLARE @CapabilityIDForDualLevelReview SMALLINT = CONVERT(SMALLINT, dbo.fn_GET_AppSettingValue('CapabilityIDForDualLevelReview'))
	DECLARE @IsDualLevelReviewActivated BIT
	DECLARE @IsAllAccountSelected BIT
		,@MaxID INT
		,@MinID INT
		,@LocalUserID INT
		,@LocalRoleID SMALLINT
	DECLARE @tblUserRoles TABLE (ID INT IDENTITY, UserID INT, RoleID SMALLINT)

	SELECT 
		 @RecPeriodEndDate = R.PeriodEndDate
		,@CompanyID = R.CompanyID
	FROM
		ReconciliationPeriod R WITH(NOLOCK)
	WHERE 
		R.ReconciliationPeriodID = @RecPeriodID
		
	SELECT
		@IsDualLevelReviewActivated = IsActivated
	FROM
		dbo.fn_SEL_CompanyCapabilityStatus(@RecPeriodEndDate, @CompanyID) CCS
	WHERE
		CCS.CapabilityID = @CapabilityIDForDualLevelReview 

	-- ADD User ID and Role ID
	INSERT INTO @tblUserRoles (UserID, RoleID) VALUES (@UserID, @RoleID)

	INSERT INTO @tblUserRoles (UserID, RoleID)
		SELECT ChildUserID, ChildRoleID
		FROM
			dbo.UserAssociationByUserRole UAR
		WHERE
			UAR.UserID = @UserID
			AND UAR.RoleID = @RoleID
			AND IsActive = 1

	SELECT @MinID = MIN(ID)
		,@MaxID = MAX (ID)
	FROM @tblUserRoles

	WHILE (@MinID <= @MaxID)
	BEGIN
		SELECT @LocalUserID = UserID
			,@LocalRoleID = RoleID
		FROM
			@tblUserRoles
		WHERE
			ID = @MinID

		SELECT
			@IsAllAccountSelected = IsAllAccount
		FROM
			dbo.UserAllAccount
		WHERE
			IsActive = 1 AND UserID = @LocalUserID AND RoleID = @LocalRoleID

		IF	@LocalRoleID = 1 -- Hard Coded for Skystem Admin
			OR @LocalRoleID = 2 -- Hard Coded for System Admin
			OR @LocalRoleID = 11 -- Hard Coded for CEO/CFO
			OR @IsAllAccountSelected = 1
		BEGIN
			INSERT INTO 
				@AccessibleAccounts
				(
					AccountID
				)
			SELECT 
				A.AccountID
			FROM 
				vw_Select_AccountKeyInfo A
			WHERE 
				CompanyID = @CompanyID
			
			BREAK
		END
		ELSE 
		BEGIN
			IF	@LocalRoleID = 3 -- Hard Coded for Preparer
				OR @LocalRoleID = 4 -- Hard Coded for Reviewer
				OR (@LocalRoleID = 5 AND @IsDualLevelReviewActivated = 1) -- Hard Coded for Approver
				OR @LocalRoleID = 14 -- Hard Coded for Backup Preparer
				OR @LocalRoleID = 15 -- Hard Coded for Backup Reviewer
				OR (@LocalRoleID = 16 AND @IsDualLevelReviewActivated = 1) -- Hard Coded for Backup Approver
			BEGIN
				DECLARE @AccountAttributeID SMALLINT

				-- Get Account Attribute ID for Role ID
				--9		Preparer
				--10	Reviewer
				--11	Approver			
				--17	Backup Preparer
				--18	Backup Reviewer
				--19	Backup Approver			
				SELECT 
						@AccountAttributeID = CASE @LocalRoleID
													WHEN 3 THEN 9
													WHEN 4 THEN 10
													WHEN 5 THEN 11
													WHEN 14 THEN 17
													WHEN 15 THEN 18
													WHEN 16 THEN 19
												END
			

				INSERT INTO
						@AccessibleAccounts
						(
							AccountID
						)
				SELECT
						AAV.AccountID
				FROM
						dbo.fn_SEL_AccountAttributeValue(@RecPeriodEndDate, @CompanyID) AAV
				INNER JOIN vw_Select_AccountKeyInfo A
						ON AAV.AccountID = A.AccountID
				WHERE
						AAV.AccountAttributeID = @AccountAttributeID
						AND CAST(AAV.Value AS INT) = @LocalUserID
				EXCEPT
				SELECT 
					AccountID
				FROM @AccessibleAccounts
			END
			ELSE
			BEGIN
				-- For Roles BA / Controller / Executive / AM / FM
				-- Data to be picked from UserGeoObject + UserAccount
				-- Selects all accounts which are accessible to the given user
				-- Doing a UNION will ensure there are not duplicate Account IDs
				INSERT INTO 
					@AccessibleAccounts
					(
						AccountID
					)
					(
						SELECT
							AKI.AccountID
						FROM 
							dbo.UserGeographyObject UGO
						INNER JOIN dbo.GeographyObjectHdr GOH1  WITH(NOLOCK)
							ON UGO.GeographyObjectID = GOH1.GeographyObjectID
						INNER JOIN dbo.GeographyObjectHdr GOH2  WITH(NOLOCK)
							ON GOH2.GeographyObjectKey LIKE GOH1.GeographyObjectKey +'%' 
						INNER JOIN dbo.vw_Select_AccountKeyInfo AKI
							ON GOH2.GeographyObjectID = AKI.GeographyObjectID
						WHERE
							GOH1.CompanyID = @CompanyID
							AND GOH2.CompanyID = @CompanyID
							AND AKI.CompanyID = @CompanyID
							AND UGO.UserID = @LocalUserID
							AND UGO.RoleID = @LocalRoleID
							AND GOH1.IsActive = 1
							AND GOH2.IsActive = 1
							AND UGO.IsActive = 1					

						UNION
					
						SELECT 
							UA.AccountID
						FROM
							UserAccount UA
						INNER JOIN vw_Select_AccountKeyInfo A
								ON UA.AccountID = A.AccountID
						WHERE
							UA.UserID = @LocalUserID
							AND UA.RoleID = @LocalRoleID 
							AND UA.IsActive = 1
					)
					EXCEPT
					SELECT 
						AccountID
					FROM @AccessibleAccounts
			END
		END
		SELECT @MinID = @MinID + 1
	END
	RETURN 
END

GO


