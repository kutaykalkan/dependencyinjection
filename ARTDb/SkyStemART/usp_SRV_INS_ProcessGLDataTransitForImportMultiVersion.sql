
CREATE PROCEDURE [dbo].[usp_SRV_INS_ProcessGLDataTransitForImportMultiVersion]  
 (
	@companyID int,
	@dataImportID int,
	@addedBy varchar(128),
	@dateAdded smalldatetime,
	@isForceCommit bit,
	@warningReasonID smallint,
	@ReturnValue nvarchar(max) OUTPUT
) 
 
AS 
/*------------------------------------------------------------------------------
	AUTHOR:			Harsh Kuntail
	DATE CREATED:	05/18/2011
	PURPOSE:		Process GLDataTransit and Update relevant tables in case of MultiVersion GL Upload
-------------------------------------------------------------------------------
	MODIFIED		AUTHOR				DESCRIPTION
	DATE
	11/21/2011		Manoj Kumar			Replace Account# by AccountNumber
	11/25/2011		Manoj Kumar			Put SRA Processing on account list and IsAccountUpdate check
	01/05/2012		Manoj Kumar			Fixed Bug#8507 -Cannot insert the value NULL into column 'ID', table '@NewAccountsTbl'; column does
										not allow nulls. INSERT fails.
	01/09/2012		Manoj Kumar			Put Materiality Processing on GLDATA ID list and IsLocked check
	03/01/2012		Manoj Kumar			IsLocked check	
	03/03/2012		Harsh				Delete copied over items where rec is reverted from reconciled to inprogress
	04/12/2012		Vinay				Now New Accounts are allowed in Multiversion
	07/06/2012		Manoj				Add IsActive user check in sending mail logic
	07/10/2012		Manoj Kumar			Process risk rating and call RecalculateBalances
	13/06/2012		Harsh				Warning blocks moved
	07/25/2012		Vinay				Call new sp for reconcilablity processing
	09/01/2012		Vinay				Optimization Changes
	09/04/2012		Vinay				Bug Fixed
	09/19/2012		Vinay				Use framework function to get notification email
	11/30/2012		Vinay				Is Profit and Loss column changes
	02/27/2013		manoj				Moved Reprocess Account Reconcilability logic to usp_SRV_UPD_GLDataHdrForMultiversionGL sp 	
	03/15/2013		Vinay				Parameter added in a sp call	
	05/21/2013		Vinay				Duplicate Messages removed	
	07/16/2013		Manoj				Bug Fix about updating Account name	
	10/16/2013		Vinay				Action Type and Change Source ID changes
	12/09/2014		Vinay				Bug Fixed
	03/18/2015		Vinay				Data Import Message changes
	04/07/2015		Manoj				Move PRA mail sending block
	04/08/2015		Manoj				Add UserID, RoleID in select list
	04/20/2015		Manoj				BugFixed : UAT issue balance change mail should be go in user language
	04/28/2015		Manoj				Bug Fixed : dulplicate account creation
	06/04/2015		Manoj				Bug Fixed : SRA rule# 5 issue
	06/30/2015		Manoj				Bug Fixed : Net value issue
-------------------------------------------------------------------------------
  
declare @p9 nvarchar(max)
set @p9=NULL
EXEC usp_SRV_INS_ProcessGLDataTransitForImportMultiVersion
 @companyID=246,
 @dataImportID=1219,
 @addedBy=N'SA@MKCL.com',
 @dateAdded='2011-07-21 18:08:00',
 @isForceCommit=1,@maxSumThreshold=1.1234,
 @minSumThreshold=-1.1234,
 @warningReasonID=1,
 @ReturnValue=@p9 OUTPUT
select @p9
-------------------------------------------------------------------------------
Tables Affected:
All Currency codes must be the same

	1. Check for Data Type (PeriodEndDate, BCCY Amouont,RCCY Amount) 
		--> usp_SRV_CHK_ValidateGLDataTransitForDataType

	2. Check for Period End Date 
		--> usp_SRV_CHK_ValidatePeriodEndDate

	3. Complete missing information based upon unique keys
		--> usp_SVC_UPD_ProcessGLDataTransitForAccountSubSetKey

	4. Check for Account Type
		--> usp_SRV_CHK_ValidateGLDataTransitForAccountType

	5. Check for valid currency codes
		-- All rec period must be same, Currency codes must be the same
		-- If multicurrency is off, rccy and bccy must be the same
		--> [dbo].[usp_SRV_CHK_ValidateAccountRCCYBCCY]

	6.	Check for valid Is Profit and Loss field
		--> usp_SRV_CHK_ValidateGLDataTransitForIsProfitAndLoss	

	7. Not null or empty fields (PeriodEndDate, AccountNumber,AccountName,FSCaption
			, AccountType,RCCY Code,RCCY Amount, BCCY Code, BCCY Amouont,Key Fields)
		--> usp_SRV_CHK_ValidateGLDataTransitForNullOrEmpty

	8. Uniqueness (AccountNumber + Account Name + FS Caption + Account Type + Key Fields) within data to import
		--> usp_SRV_CHK_ValidateGLDataTransitForDuplicates

	9. Check for not net to zero
		--> usp_SRV_CHK_ValidateGLDataTransitForSum

	10.Check for new accounts with multiversion 
		-->EXEC [dbo].[usp_SRV_CHK_ValidateGLDataTransitForNewAccounts] @companyID, @dataImportID, @languageID, @businessEntityID, @defaultLanguageID, @RecPeriodID
	
	11. Check for changes in existing accounts
		--> usp_SRV_CHK_ValidateGLDataTransitForExistingAccountsMultiversion

	12. Check for Business Admin Accessibility
		--> usp_SRV_CHK_ValidateAccountAccessibilityForBA

--------------------------------------------------------------------------------	

-------------------------------------------------------------------------------*/
BEGIN 
		DECLARE @errorMessageForServiceToLog VARCHAR(max) 
		DECLARE @errorMessageToSave NVARCHAR(MAX) 
		DECLARE @importStatus VARCHAR(15) 
		DECLARE @recordsImported INT 
		DECLARE @AccountHdrRecordsInserted INT = 0
		DECLARE @FSCaptionHdrRecordsInserted INT = 0
		DECLARE @GeographyObjectHdrRecordsInserted INT = 0
		DECLARE @AccountHdrRecordsUpdated INT = 0
		DECLARE @FSCaptionHdrRecordsUpdated INT = 0
		DECLARE @GeographyObjectHdrRecordsUpdated INT = 0
		DECLARE @retWarningReasonID SMALLINT 
		DECLARE @isAlertRaised BIT 
		DECLARE @OverrideUserEmailIds VARCHAR(MAX)		
		DECLARE @errMsg NVARCHAR(MAX)
		DECLARE @glBalanceReportingCurrencySum DECIMAL(18,4)
		DECLARE @returnVal INT
		DECLARE @rowsInserted INT
		DECLARE @error VARCHAR(MAX)
		Declare @validationErrorMsg VARCHAR(MAX)
		Declare @validationWarningMsg VARCHAR(MAX)
		DECLARE @RecPeriodDate SMALLDATETIME
		DECLARE @RecPeriodID INT
		Declare @errorState INT
		DECLARE @languageID INT
		DECLARE @businessEntityID INT
		DECLARE @defaultLanguageID INT
		DECLARE @DefaultErrorMessage NVARCHAR(MAX)
		DECLARE @AccountTable udt_BigIntIDTableType
		DECLARE @RoleID SMALLINT, @UserID INT, @DataImportTypeID SMALLINT
		DECLARE @IsDualReviewOn BIT
		DECLARE @GlBalanceReportingNetValue DECIMAL(18,4)		
		DECLARE @tblAccounts TABLE ( AccountInfo VARCHAR(MAX),Email VARCHAR(200), RoleID SMALLINT, UserID INT, DefaultLanguageID INT)
		DECLARE @tblGLDataID  AS udt_BigIntIDTableType
		DECLARE @maxSumThreshold decimal(18,4),@minSumThreshold decimal(18,4)
		DECLARE @NetAccountTable AS udt_IntIDTableType		
		DECLARE @ApplicationID INT = CONVERT(INT,dbo.fn_GET_AppSettingValue('ApplicationID'))
		DECLARE @DefaultBusinessEntityID INT = CONVERT(INT,dbo.fn_GET_AppSettingValue('DefaultBusinessEntityID'))
		DECLARE @UserInputLabelTypeID INT = CONVERT(INT,dbo.fn_GET_AppSettingValue('UserInputLabelTypeID'))
		
		-- BEGIN SP Profiling Variables
		DECLARE @EnableSPProfiling BIT = CONVERT(BIT,dbo.fn_GET_AppSettingValue('EnableSPProfiling'))
		DECLARE @tblProfile Table(SPNAME NVARCHAR(200), StartDateTime DATETIME2, EndDateTime DATETIME2)
		DECLARE @StartDateTime DATETIME2
		-- END SP Profiling Variables
		DECLARE @ChangeSourceIDGLDataLoad SMALLINT = CONVERT(INT,dbo.fn_GET_ChangeSourceID('ChangeSourceIDGLDataLoad'))
		DECLARE @ActionTypeIDGLDataLoad SMALLINT = CONVERT(INT,dbo.fn_GET_ActionTypeID('ActionTypeIDGLDataLoad'))
		DECLARE @WarningCount INT = 0
			,@ErrorCount INT = 0
			,@MappedKeyCount INT = 0


		-- Update Company ID in GL Data Transit (TODO: Should be done from service)
		UPDATE 
			dbo.GLDataTransit					
		SET
			CompanyID = @CompanyID
		WHERE
			DataImportID = @dataImportID
					
		SET @DefaultErrorMessage = 'Error message not available'
		SET @validationErrorMsg = 'Validation Fail'
		SET @validationWarningMsg = 'Validation Warning'
		SET @recordsImported = 0
		SET @businessEntityID = @companyID
		SET @defaultLanguageID = 1033
		
		SELECT 
			@RecPeriodDate = RP.PeriodEndDate
			,@RecPeriodID = RP.ReconciliationPeriodID
			,@languageID = DI.LanguageID
			,@UserID = UH.UserID
			,@RoleID = DI.RoleID 
			,@DataImportTypeID = DI.DataImportTypeID
		FROM
			[dbo].[ReconciliationPeriod] RP
		INNER JOIN [dbo].[DataImportHdr] DI 
			ON RP.ReconciliationPeriodID = DI.ReconciliationPeriodID 
		INNER JOIN
			dbo.UserHdr UH
			ON DI.AddedBy = UH.LoginID
		WHERE
			DI.DataImportID = @dataImportID
			AND RP.CompanyID = @companyID 
			AND RP.IsActive = 1 
			
		SELECT
			@MappedKeyCount = COUNT(1)
		FROM
			[MappingUpload].[fn_SEL_CompanyAccountMappingKey] (@RecPeriodDate , @companyID, 1) F

		SET @maxSumThreshold = dbo.fn_GET_ZeroThresholdByRecPeriodID  (@RecPeriodID )
		SET @minSumThreshold = -1 * @maxSumThreshold
		
		--  ******Get Dual Review FROM CompanyCapability table and set the variable @IsDualReviewOn 
		SELECT @IsDualReviewOn=dbo.fn_GET_IsCompanyCapabilityActivated(@RecPeriodID,4) -- 4 is capabilty ID For Dual Level Review
   					
		BEGIN TRY

			IF (@isForceCommit = 0)
			BEGIN
				-- Step1: Data Type Check
				--1. Validation for DataType
				IF (@EnableSPProfiling = 1)
					SET @StartDateTime = GETDATE()
				exec [dbo].[usp_SRV_CHK_ValidateGLDataTransitForDataType]  
					@companyID = @companyID
					,@dataImportID = @dataImportID
					,@languageID = @languageID
					,@businessEntityID = @businessEntityID
					,@defaultLanguageID = @defaultLanguageID
					,@dateAdded = @dateAdded
				IF (@EnableSPProfiling = 1)
					INSERT INTO @tblProfile VALUES('[dbo].[usp_SRV_CHK_ValidateGLDataTransitForDataType]', @StartDateTime, GETDATE())
				--IF exists (Select GLDataTransitID from [dbo].[GLDataTransit] where IsValidRow = 0 and DataImportID = @dataImportID )				
				--	RaisError(@validationErrorMsg, 16, 1)

				--2. Validation for Period End Date
				IF (@EnableSPProfiling = 1)
					SET @StartDateTime = GETDATE()
				exec @returnVal = [dbo].[usp_SRV_CHK_ValidatePeriodEndDate] 
					@companyID = @companyID
					,@dataImportID = @dataImportID
					,@languageID = @languageID
					,@BusinessEntityID = @businessEntityID
					,@defaultLanguageID = @defaultLanguageID
					,@dateAdded = @dateAdded
					,@errorMessage = @errMsg output
				IF (@EnableSPProfiling = 1)
					INSERT INTO @tblProfile VALUES('[dbo].[usp_SRV_CHK_ValidatePeriodEndDate]', @StartDateTime, GETDATE())
				--IF (@errMsg is not null)
				--	RaisError(@validationErrorMsg, 16, 10)

				IF(@MappedKeyCount > 0)
				BEGIN
					--3: Complete Missing Information
					IF (@EnableSPProfiling = 1)
						SET @StartDateTime = GETDATE()
					EXEC [MappingUpload].[usp_SVC_UPD_ProcessGLDataTransitForAccountSubSetKey] 	
						@CompanyID = @companyID
						,@DataImportID = @dataImportID 
						,@IsForceCommit = @IsForceCommit
						,@WarningReasonID = @warningReasonID 	
						,@DateAdded = @DateAdded
					IF (@EnableSPProfiling = 1)
						INSERT INTO @tblProfile VALUES('[MappingUpload].[usp_SVC_UPD_ProcessGLDataTransitForAccountSubSetKey]', @StartDateTime, GETDATE())
					--If exists(Select G.GLDataTransitID From GLDataTransit G Where G.DataImportID = @dataImportID AND IsValidRow = 0)
					--	RaisError(@validationErrorMsg,16,21)
				END

				--4.Check for Valid AccountType 
				IF (@EnableSPProfiling = 1)
					SET @StartDateTime = GETDATE()
				EXEC [dbo].[usp_SRV_CHK_ValidateGLDataTransitForAccountType] 
					 @CompanyID = @companyID
					,@DataImportID = @dataImportID 
					,@languageID = @languageID
					,@BusinessEntityID = @businessEntityID
					,@defaultLanguageID = @defaultLanguageID
					,@dateAdded = @dateAdded
				IF (@EnableSPProfiling = 1)
					INSERT INTO @tblProfile VALUES('[dbo].[usp_SRV_CHK_ValidateGLDataTransitForAccountType]', @StartDateTime, GETDATE())
				--IF exists (Select GLDataTransitID from [dbo].[GLDataTransit] where IsValidRow = 0 and DataImportID = @dataImportID )
				--	RaisError(@validationErrorMsg, 16, 4)

				--5. Validation for Curreny Codes. New Addition
				IF (@EnableSPProfiling = 1)
					SET @StartDateTime = GETDATE()
				exec @returnVal = [dbo].[usp_SRV_CHK_ValidateAccountRCCYBCCY]  
					 @CompanyID = @companyID
					,@DataImportID = @dataImportID 
					,@languageID = @languageID
					,@BusinessEntityID = @businessEntityID
					,@defaultLanguageID = @defaultLanguageID
					,@dateAdded = @dateAdded
					,@errorMessage = @errMsg output
				IF (@EnableSPProfiling = 1)
					INSERT INTO @tblProfile VALUES('[dbo].[usp_SRV_CHK_ValidateAccountRCCYBCCY]', @StartDateTime, GETDATE())
				--IF (@errMsg is not null)
				--	RaisError(@validationErrorMsg, 16, 11)

				--6. Validation for Profit and Loss Column
				IF (@EnableSPProfiling = 1)
					SET @StartDateTime = GETDATE()
				EXEC [dbo].[usp_SRV_CHK_ValidateGLDataTransitForIsProfitAndLoss] 
					 @CompanyID = @companyID
					,@DataImportID = @dataImportID 
					,@LanguageID = @languageID
					,@BusinessEntityID = @businessEntityID
					,@defaultLanguageID = @defaultLanguageID
					,@dateAdded = @dateAdded
					,@DefaultErrorMessage = @DefaultErrorMessage
				IF (@EnableSPProfiling = 1)
					INSERT INTO @tblProfile VALUES('[dbo].[usp_SRV_CHK_ValidateGLDataTransitForIsProfitAndLoss]', @StartDateTime, GETDATE())
				--IF exists (Select GLDataTransitID from [dbo].[GLDataTransit] where IsValidRow = 0 and DataImportID = @dataImportID )				
				--	RaisError(@validationErrorMsg, 16, 22)

				--7. Validation for mandatory fields should not be null or empty
				IF (@EnableSPProfiling = 1)
					SET @StartDateTime = GETDATE()
				EXEC [dbo].[usp_SRV_CHK_ValidateGLDataTransitForNullOrEmpty] 
					 @CompanyID = @companyID
					,@DataImportID = @dataImportID 
					,@LanguageID = @languageID
					,@BusinessEntityID = @businessEntityID
					,@defaultLanguageID = @defaultLanguageID
					,@dateAdded = @dateAdded
				IF (@EnableSPProfiling = 1)
					INSERT INTO @tblProfile VALUES('[dbo].[usp_SRV_CHK_ValidateGLDataTransitForNullOrEmpty]', @StartDateTime, GETDATE())
				--IF exists (Select GLDataTransitID from [dbo].[GLDataTransit] where IsValidRow = 0 and DataImportID = @dataImportID )
				--	RaisError(@validationErrorMsg, 16, 3)

				IF (@MappedKeyCount = 0)
				BEGIN
					--8. Validation of 	Uniqueness when mapping keys are not defined
					IF (@EnableSPProfiling = 1)
						SET @StartDateTime = GETDATE()
					EXEC [dbo].[usp_SRV_CHK_ValidateGLDataTransitForDuplicates] 
						 @CompanyID = @companyID
						,@DataImportID = @dataImportID 
						,@LanguageID = @languageID
						,@BusinessEntityID = @businessEntityID
						,@defaultLanguageID = @defaultLanguageID
						,@dateAdded = @dateAdded
					IF (@EnableSPProfiling = 1)
						INSERT INTO @tblProfile VALUES('[dbo].[usp_SRV_CHK_ValidateGLDataTransitForDuplicates]', @StartDateTime, GETDATE())
					--IF exists (Select GLDataTransitID from [dbo].[GLDataTransit] where IsValidRow = 0 and DataImportID = @dataImportID )
					--	RaisError(@validationErrorMsg, 16, 2)				
				END
				--9. Check for Sum = 0
				IF (@EnableSPProfiling = 1)
					SET @StartDateTime = GETDATE()
				exec [dbo].[usp_SRV_CHK_ValidateGLDataTransitForSum]  
					@CompanyID = @companyID
					,@DataImportID = @dataImportID
					, @MinSumThreshold = @minSumThreshold
					, @MaxSumThreshold = @maxSumThreshold
					, @LanguageID = @languageID
					, @BusinessEntityID = @businessEntityID
					, @DefaultLanguageID = @defaultLanguageID
					, @DateAdded = @dateAdded
					, @IsForceCommit = @IsForceCommit
					, @RetVal = @glBalanceReportingCurrencySum out
				IF (@EnableSPProfiling = 1)
					INSERT INTO @tblProfile VALUES('[dbo].[usp_SRV_CHK_ValidateGLDataTransitForSum]', @StartDateTime, GETDATE())

			  -- This is required to update Account info
				IF (@MappedKeyCount = 0)
				BEGIN
					-- Update Label ID for existing phrases
					IF (@EnableSPProfiling = 1)
						SET @StartDateTime = GETDATE()
					EXEC [dbo].[usp_SRV_UPD_UpdateLabelsGLDataTransit]  
						@DataImportID = @dataImportID
						,@LanguageID = @languageID
						,@BusinessEntityID = @businessEntityID
						,@DefaultLanguageID = @defaultLanguageID
						,@DefaultBusinessEntityID = @DefaultBusinessEntityID
					IF (@EnableSPProfiling = 1)
						INSERT INTO @tblProfile VALUES('[dbo].[usp_SRV_UPD_UpdateLabelsGLDataTransit]', @StartDateTime, GETDATE())
					--This is required to update Account info
					IF (@EnableSPProfiling = 1)
						SET @StartDateTime = GETDATE()
					EXEC [dbo].[usp_SRV_UPD_GLDataTransitForAccountInfo] @companyID, @dataImportID, @languageID, @businessEntityID, @defaultLanguageID
					IF (@EnableSPProfiling = 1)
						INSERT INTO @tblProfile VALUES('[dbo].[usp_SRV_UPD_GLDataTransitForAccountInfo]', @StartDateTime, GETDATE())
				END

				--10. Check for new accounts
				IF (@EnableSPProfiling = 1)
					SET @StartDateTime = GETDATE()
				EXEC [dbo].[usp_SRV_CHK_ValidateGLDataTransitForNewAccounts] 
					@companyID
					,@dataImportID
					,@languageID
					,@businessEntityID
					,@defaultLanguageID
					,@RecPeriodID
					,@dateAdded = @dateAdded
				IF (@EnableSPProfiling = 1)
					INSERT INTO @tblProfile VALUES('[dbo].[usp_SRV_CHK_ValidateGLDataTransitForNewAccounts]', @StartDateTime, GETDATE())
				--IF exists (SELECT GLDataTransitID FROM [dbo].[GLDataTransit] WHERE IsValidRow = 0 and DataImportID = @dataImportID )
				--	RAISERROR(@validationWarningMsg, 16, 3)

				-- 11. Check for changes in existing accounts
				IF (@EnableSPProfiling = 1)
					SET @StartDateTime = GETDATE()
				EXEC [dbo].[usp_SRV_CHK_ValidateGLDataTransitForExistingAccountsMultiversion] 
					@companyID = @companyID
					,@dataImportID = @dataImportID
					,@RecPeriodID = @RecPeriodID
					,@PeriodEndDate = @RecPeriodDate
					,@languageID = @languageID
					,@businessEntityID = @businessEntityID
					,@defaultLanguageID = @defaultLanguageID
					,@DateAdded = @dateAdded
				IF (@EnableSPProfiling = 1)
					INSERT INTO @tblProfile VALUES('[dbo].[usp_SRV_CHK_ValidateGLDataTransitForExistingAccountsMultiversion]', @StartDateTime, GETDATE())

				-- 11.1. Check for changes in existing locked accounts
				IF (@EnableSPProfiling = 1)
					SET @StartDateTime = GETDATE()
				EXEC [dbo].[usp_SRV_CHK_ValidateGLDataTransitForLockedAccountsMultiversion] 
					@companyID = @companyID
					,@dataImportID = @dataImportID
					,@RecPeriodID = @RecPeriodID
					,@PeriodEndDate = @RecPeriodDate
					,@languageID = @languageID
					,@businessEntityID = @businessEntityID
					,@defaultLanguageID = @defaultLanguageID
					,@DateAdded = @dateAdded
				IF (@EnableSPProfiling = 1)
					INSERT INTO @tblProfile VALUES('[dbo].[usp_SRV_CHK_ValidateGLDataTransitForLockedAccountsMultiversion]', @StartDateTime, GETDATE())

				--12. Check for Business Admin Accessibility
				--Check if DataImport is done by BA, if so, check account accessibility
				--Error if any of the uploaded Accounts are found inAccessible
				IF (@RoleID = 6)--HardCoded for Business Admin
				BEGIN
					IF (@EnableSPProfiling = 1)
						SET @StartDateTime = GETDATE()
					exec [dbo].[usp_SRV_CHK_ValidateAccountAccessibilityForBA] 
						 @CompanyID = @companyID
						,@DataImportID = @dataImportID 
						,@LanguageID = @languageID
						,@BusinessEntityID = @businessEntityID
						,@defaultLanguageID = @defaultLanguageID
						,@dateAdded = @dateAdded
						,@addedBy = @addedBy
					IF (@EnableSPProfiling = 1)
						INSERT INTO @tblProfile VALUES('[dbo].[usp_SRV_CHK_ValidateAccountAccessibilityForBA]', @StartDateTime, GETDATE())
					--IF exists (SELECT GLDataTransitID FROM [dbo].[GLDataTransit] WHERE IsValidRow = 0 and DataImportID = @dataImportID )
					--	RAISERROR(@validationErrorMsg, 16, 12)
				END
				--13. Delete warning rows which have errors
				EXEC [dbo].[usp_DEL_DataImportMessageDetailWarningsWithError]
					@DataImportID = @DataImportID

				SELECT 
					@ErrorCount = CASE WHEN DIW.DataImportMessageTypeID = 3 THEN @ErrorCount + 1 ELSE @ErrorCount END
					,@WarningCount = CASE WHEN DIW.DataImportMessageTypeID = 2 AND IsNull(DIW.IsEnabled, 1) = 1 THEN @WarningCount + 1 ELSE @WarningCount END
				FROM
					dbo.DataImportMessageDetail DIMD
				INNER JOIN fn_SEL_DataImportWarningPreferences(@CompanyID, @DataImportTypeID, @UserID, @RoleID) DIW
					ON DIMD.DataImportMessageID = DIW.DataImportMessageID
				WHERE 
					DataImportID = @dataImportID AND IsActive = 1

				IF (@ErrorCount > 0)
				BEGIN
					RaisError(@validationErrorMsg, 16, 100)
				END
				IF (@ErrorCount = 0 AND @WarningCount > 0)
				BEGIN					
					RaisError(@validationWarningMsg, 16, 200)
				END
			END

			IF((@ErrorCount = 0 AND @WarningCount = 0) OR (@isForceCommit = 1))
			BEGIN	
			
				----Get email id of user whose uploaded accounts are getting overriden
				IF EXISTS (SELECT GLDataTransitID 
							FROM 
								[dbo].[GLDataTransit] GDT
							INNER JOIN [dbo].DataImportMessageDetail DIMD
								ON GDT.DataImportID = DIMD.DataImportID AND GDT.ExcelRowNumber = DIMD.ExcelRowNumber 
							WHERE GDT.DataImportID = @dataImportID AND DIMD.DataImportMessageID = 15)
				BEGIN					
					INSERT INTO @tblAccounts
						(	
							AccountInfo
							,Email
							,RoleID	
							,UserID	
							,DefaultLanguageID
						)
						SELECT DISTINCT
							GLT.ErrorMessage
							, UN.EmailID
							, UN.RoleID
							, UN.UserID
							, UN.DefaultLanguageID
						FROM 
							GLDataTransit GLT 
							INNER JOIN GLDataHdr GL 
								ON GLT.AccountID = GL.AccountID
							INNER JOIN dbo.fn_SEL_UsersForNotificationPRA(@companyID, @RecPeriodDate) UN
								ON GLT.AccountID = UN.AccountID 
							INNER JOIN dbo.fn_SEL_UserRolesForNotificationPRA(@RecPeriodID) UR
								ON UN.RoleID = UR.RoleID
						WHERE 
							GLT.DataImportID = @dataImportID 
							AND GLT.IsAccountUpdate = 1 
							AND GL.IsActive = 1 
							AND GL.ReconciliationPeriodID = @RecPeriodID
					--This is required for sending the emails
					SELECT * FROM @tblAccounts ORDER BY Email
				END
				
				--Insert/retrive Labels for Geography Object Hdr
				IF (@EnableSPProfiling = 1)
					SET @StartDateTime = GETDATE()
				EXEC [dbo].[usp_SRV_INS_AccountGeographyLabels]  
					@DataImportID = @dataImportID
					,@ApplicationID = @ApplicationID
					,@LanguageID = @languageID
					,@BusinessEntityID = @businessEntityID
					,@DefaultLanguageID = @defaultLanguageID
					,@DefaultBusinessEntityID = @DefaultBusinessEntityID
					,@LabelTypeID = @UserInputLabelTypeID
				IF (@EnableSPProfiling = 1)
					INSERT INTO @tblProfile VALUES('[dbo].[usp_SRV_INS_AccountGeographyLabels]', @StartDateTime, GETDATE())

				-- This is required to update Account info
				IF (@EnableSPProfiling = 1)
					SET @StartDateTime = GETDATE()
				EXEC [dbo].[usp_SRV_UPD_GLDataTransitForAccountInfo] @companyID, @dataImportID, @languageID, @businessEntityID, @defaultLanguageID
				IF (@EnableSPProfiling = 1)
					INSERT INTO @tblProfile VALUES('[dbo].[usp_SRV_UPD_GLDataTransitForAccountInfo]', @StartDateTime, GETDATE())
			
				--1. Insert into FSCaptionHDR
				IF (@EnableSPProfiling = 1)
					SET @StartDateTime = GETDATE()
				EXEC [dbo].[usp_SRV_INS_FSCaptionHdr_New] @companyID, @dataImportID, @addedBy, @dateAdded, @languageID, @businessEntityID, @defaultLanguageID , @FSCaptionHdrRecordsInserted OUTPUT, @FSCaptionHdrRecordsUpdated OUTPUT
				IF (@EnableSPProfiling = 1)
					INSERT INTO @tblProfile VALUES('[dbo].[usp_SRV_INS_FSCaptionHdr_New]', @StartDateTime, GETDATE())
				
				--2. Insert into GeographyObjectHdr
				IF (@EnableSPProfiling = 1)
					SET @StartDateTime = GETDATE()
				EXEC [dbo].[usp_SRV_INS_GeographyObjectHdr_New] @companyID, @dataImportID, @addedBy, @dateAdded, @languageID, @businessEntityID, @defaultLanguageID , @GeographyObjectHdrRecordsInserted OUTPUT, @GeographyObjectHdrRecordsUpdated OUTPUT
				IF (@EnableSPProfiling = 1)
					INSERT INTO @tblProfile VALUES('[dbo].[usp_SRV_INS_GeographyObjectHdr_New]', @StartDateTime, GETDATE())
				
				--3. Insert AccountHDR
				IF (@EnableSPProfiling = 1)
					SET @StartDateTime = GETDATE()
				EXEC [dbo].[usp_SRV_INS_AccountHdr_New] @companyID, @dataImportID, @addedBy, @dateAdded, @languageID, @businessEntityID, @defaultLanguageID, @isAlertRaised OUTPUT, @AccountHdrRecordsInserted OUTPUT, @AccountHdrRecordsUpdated OUTPUT
				IF (@EnableSPProfiling = 1)
					INSERT INTO @tblProfile VALUES('[dbo].[usp_SRV_INS_AccountHdr_New]', @StartDateTime, GETDATE())
			
				---- Check accounts are getting overriden and Update GLDataTransit For GLDataID For overriden accounts
				
				--Update GLDataHDR for FOR GL balance
				IF (@EnableSPProfiling = 1)
					SET @StartDateTime = GETDATE()
				EXEC [dbo].[usp_SRV_UPD_GLDataHdrForMultiversionGL] 
					@DataImportID = @dataImportID
					,@RecPeriodID = @RecPeriodID
					,@CompanyID = @companyID
					,@AddedBy = @addedBy
					,@DateAdded = @dateAdded
					,@ChangeSourceIDSRA = @ChangeSourceIDGLDataLoad
					,@ActionTypeID = @ActionTypeIDGLDataLoad
					,@RowsAffected = @rowsInserted OUTPUT
				IF (@EnableSPProfiling = 1)
					INSERT INTO @tblProfile VALUES('[dbo].[usp_SRV_UPD_GLDataHdrForMultiversionGL]', @StartDateTime, GETDATE())
						
				-- Prepare list of GLDataID which are not locked
				INSERT INTO @tblGLDataID 
				(
					ID
				)
				SELECT 
					G.GLDataID
				FROM 
					[dbo].[GLDataHdr] G
				LEFT JOIN dbo.fn_SEL_LockedAccounts(@companyID, @RecPeriodDate) LA
					ON G.AccountID = LA.AccountID
				WHERE 
					G.DataImportID = @dataImportID AND G.ReconciliationPeriodID = @RecPeriodID
					AND (LA.IsLocked IS NULL OR LA.IsLocked = 0)
				
				
			
				DECLARE @udt_GLDataID udt_BigIntIDTableType
				INSERT INTO @udt_GLDataID (ID)
				SELECT
					GL.GLDataID
				FROM
					GLDataHdr GL
				LEFT JOIN dbo.fn_SEL_LockedAccounts(@companyID, @RecPeriodDate) LA
					ON GL.AccountID = LA.AccountID
				WHERE
					GL.DataImportID = @dataImportID 
					AND (LA.IsLocked IS NULL OR LA.IsLocked = 0)
					AND GL.ReconciliationStatusID = 2 -- InProgress
				
				--Delete all copied over rec items for GLDataID affected by Multi Version Upload
				IF (@EnableSPProfiling = 1)
					SET @StartDateTime = GETDATE()
				EXEC [dbo].[usp_DEL_CopiedOverRecItems] 
					@GLDataIDTable = @udt_GLDataID
					,@DateRevised = @dateAdded 
					,@RevisedBy = @addedBy 
					,@ChangeSourceIDSRA = @ChangeSourceIDGLDataLoad
					,@ActionTypeID = @ActionTypeIDGLDataLoad
				IF (@EnableSPProfiling = 1)
					INSERT INTO @tblProfile VALUES('[dbo].[usp_DEL_CopiedOverRecItems]', @StartDateTime, GETDATE())
				
				--Recalculate Balances on all accounts affected by this data import			
				IF (@EnableSPProfiling = 1)
					SET @StartDateTime = GETDATE()
				EXEC [dbo].[usp_UPD_RecalculateBalances]  
						@udtGLdataID = @tblGLDataID
						, @RevisedBy = @addedBy
						, @DateRevised = @dateAdded
						, @FlagConsiderSubledgerCalculation = 1
				IF (@EnableSPProfiling = 1)
					INSERT INTO @tblProfile VALUES('[dbo].[usp_UPD_RecalculateBalances]', @StartDateTime, GETDATE())


				IF (@EnableSPProfiling = 1)
					SET @StartDateTime = GETDATE()
				EXEC [dbo].[usp_UPD_MaterialityAndSRAWrapper] 
					@udtGLDataID = @tblGLDataID
					,@CompanyID = @CompanyID
					,@RecPeriodID = @RecPeriodID
					,@ProcessForMateriality = 1
					,@ProcessForSRA = 1
					,@dateRevised = @dateAdded
					,@RevisedBy = @addedBy
					,@ChangeSourceIDSRA = @ChangeSourceIDGLDataLoad
					,@ActionTypeID = @ActionTypeIDGLDataLoad
					,@ProcessGLDataIDListOnly = 1
				
				IF (@EnableSPProfiling = 1)
					INSERT INTO @tblProfile VALUES('[dbo].[usp_UPD_MaterialityAndSRAWrapper]', @StartDateTime, GETDATE())
			
				-- All Updates are done successfully.			
				-- Get Net Value if  Updates are done successfully  
				IF (@isForceCommit = 1)
				BEGIN
					IF (@EnableSPProfiling = 1)
						SET @StartDateTime = GETDATE()
					exec [dbo].[usp_SRV_CHK_ValidateGLDataTransitForSum]  
						@CompanyID = @companyID
						,@DataImportID = @dataImportID
						, @MinSumThreshold = @minSumThreshold
						, @MaxSumThreshold = @maxSumThreshold
						, @LanguageID = @languageID
						, @BusinessEntityID = @businessEntityID
						, @DefaultLanguageID = @defaultLanguageID
						, @DateAdded = @dateAdded
						, @IsForceCommit = @IsForceCommit
						, @RetVal = @glBalanceReportingCurrencySum out
					IF (@EnableSPProfiling = 1)
						INSERT INTO @tblProfile VALUES('[dbo].[usp_SRV_CHK_ValidateGLDataTransitForSum]', @StartDateTime, GETDATE())
				END
				UPDATE
					[dbo].[DataImportHdr]
				SET
					NetValue=@glBalanceReportingCurrencySum
				WHERE
					DataImportID = @dataImportID 
			
					---Delete all GLData Items FROM GLDataTransit Table 
				Delete From [dbo].[GLDataTransit] WHERE DataImportID = @dataImportID 				
				SET @importStatus = 'SUCCESS'
				SET @errorMessageForServiceToLog = 'Data Imported Successfully'
				SET @errorMessageToSave = ISNULL((SELECT [dbo].fn_GetPhraseForDefaultBuinsessEntityID (1743 ,@languageID,@defaultLanguageID)),@DefaultErrorMessage)
				SET @recordsImported = @rowsInserted
			END			
		END TRY
		
		BEGIN CATCH
			SET @errorState = ERROR_STATE()
			SET @error = ERROR_MESSAGE()
			IF (@error = @validationErrorMsg )
			BEGIN
				If (@errorState = 100)
				BEGIN
					Set @importStatus = 'ERROR'
					SELECT @errorMessageToSave = ISNULL(@errorMessageToSave + ', ','') + ErrorMessage 
					FROM [dbo].[GLDataTransit] 
					WHERE IsValidRow = 0 and ErrorMessage is not null and DataImportID = @dataImportID
					
					SET @errorMessageForServiceToLog = @errorMessageToSave 
					SET @recordsImported = 0
				End
				ELSE
				BEGIN
					SET @importStatus = 'FAIL'

					SELECT @errorMessageToSave = ISNULL(@errorMessageToSave + ', ','') + ErrorMessage 
					FROM [dbo].[GLDataTransit] 
					WHERE IsValidRow = 0 and ErrorMessage is not null and DataImportID = @dataImportID
					
					SET @errorMessageForServiceToLog = @errorMessageToSave 
					SET @recordsImported = 0
				End
			End
			ELSE IF(@error = @validationWarningMsg )
			BEGIN
				IF (@errorState = 200)
				BEGIN
					SET @importStatus = 'WARNING'
					SELECT 
						@errorMessageToSave = ISNULL(@errorMessageToSave + ', ','') + IsNull(G.WarningMessage, G.ErrorMessage)  
					FROM 
						[dbo].[GLDataTransit] G 
					WHERE 
						DataImportID = @dataImportID
						AND G.RowHasWarning = 1
				
					SET @errorMessageForServiceToLog = @errorMessageToSave 
					SET @recordsImported = 0
				END			
				ELSE
				BEGIN
					SET @importStatus = 'WARNING'

					SELECT @errorMessageToSave = ISNULL(@errorMessageToSave + ', ','') + ErrorMessage 
					FROM [dbo].[GLDataTransit] 
					WHERE IsValidRow = 0 and ErrorMessage is not null and DataImportID = @dataImportID
					
					SET @errorMessageForServiceToLog = @errorMessageToSave 
					SET @recordsImported = 0
				End
			END
			ELSE
			BEGIN
				SET @importStatus = 'FAIL'
 				--SET @errorMessageToSave = 'Error while uploading Data. Data could not be uploaded' 
 				SET @errorMessageToSave = ISNULL((SELECT [dbo].fn_GetPhraseForDefaultBuinsessEntityID (5000164 ,@languageID,@defaultLanguageID)),@DefaultErrorMessage)
				EXECUTE [dbo].[usp_GetExecutionError] @errorMessageForServiceToLog out
				SET @recordsImported = 0
			End
			
		END CATCH
		
		DECLARE @ProfileData XML = (SELECT * FROM @tblProfile 
									FOR XML RAW('SPCall'), ELEMENTS, ROOT('SPProfileRoot'))
		--Return Values
		SET @ReturnValue = (
							SELECT 
								@errorMessageForServiceToLog AS 'ErrorMessageForServiceToLog'
								, @errorMessageToSave AS 'ErrorMessageToSave'
								, @importStatus AS 'ImportStatus'
								, @recordsImported AS 'RecordsImported'
								, @retWarningReasonID AS 'WarningReasonID'
								, @isAlertRaised AS 'IsAlertRaised'
								, @OverrideUserEmailIds AS 'OverridenEmailID'
								, @ProfileData AS 'ProfilingData'
							For XML Path (''), Root	('ReturnValue')
							)
		
END
