USE [SkyStemART]
GO

/****** Object:  StoredProcedure [dbo].[usp_SEL_AllRoleByCompanyID]    Script Date: 7/4/2017 11:19:44 AM ******/
DROP PROCEDURE [dbo].[usp_SEL_AllRoleByCompanyID]
GO

/****** Object:  StoredProcedure [dbo].[usp_SEL_AllRoleByCompanyID]    Script Date: 7/4/2017 11:19:44 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[usp_SEL_AllRoleByCompanyID]
(
	@CompanyID INT
	,@PeriodEndDate DATETIME
)
AS

/*------------------------------------------------------------------------------
	AUTHOR:			Devendra Kumar
	DATE CREATED:	12/27/2010
	PURPOSE:		Get All Roles by Company ID .
-------------------------------------------------------------------------------
	MODIFIED		AUTHOR				DESCRIPTION
	DATE
-------------------------------------------------------------------------------
	01/06/15		Vinay				Rec Period set logic added
-------------------------------------------------------------------------------
	SAMPLE CALL 
	EXEC usp_SEL_AllRoleByCompanyID 157
-------------------------------------------------------------------------------*/

BEGIN
	DECLARE @CompanyRecPeriodSetID INT
	-- When periods have not been loaded
	IF (@PeriodEndDate IS NULL)
	BEGIN
		SELECT 
			R.RoleID,
			R.RoleLabelID,
			R.IsVisibleForAccountAssociationByUserRole
		FROM 
			dbo.PackageRole P
		INNER JOIN dbo.RoleMst R 
			ON P.RoleID = R.RoleID
		INNER JOIN dbo.CompanyHdr C 
			ON C.PackageID = P.PackageID
		WHERE 
			C.CompanyID = @CompanyID 
			AND C.IsActive = 1 AND R.IsActive = 1
	END
	ELSE -- When periods are loaded and set must be available
	BEGIN
		SELECT
			@CompanyRecPeriodSetID = CompanyRecPeriodSetID
		FROM
			dbo.fn_GET_CompanyRecPeriodSetID(@PeriodEndDate, @CompanyID, 6) -- 6 = Set Type for Company Feature
			
		SELECT 
			R.RoleID,
			R.RoleLabelID,
			R.IsVisibleForAccountAssociationByUserRole
		FROM 
			dbo.CompanyRole C
		INNER JOIN dbo.RoleMst R 
			ON R.RoleID = C.RoleID
		WHERE 
			C.CompanyID = @CompanyID
			AND R.IsActive = 1
			AND C.CompanyRecPeriodSetID = @CompanyRecPeriodSetID
	END
END

GO


