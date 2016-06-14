using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace SkyStem.ART.Web.Data
{

    /// <summary>
    /// Summary description for WebEnums
    /// </summary>
    public class WebEnums
    {
        public WebEnums()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public enum FieldType
        {
            MandatoryField = 1,
            DateCompareField,
            FSCaptionMandatoryField,
            DateCompareWithRecOrCurrentDateField,
            DateComparePerRecDateField,
            DateFormatField,
            InvalidNumericField,
            InvalidDate,
            DateCompareFieldGreaterThan,
            CompareBetweenField
        }

        public enum AttributeSetType
        {
            RoleConfig = 1,
            AuditRoleFilter = 2
        }

        public enum UserRole
        {
            None = 0,
            SKYSTEM_ADMIN = 1,
            SYSTEM_ADMIN = 2,
            PREPARER = 3,
            REVIEWER = 4,
            APPROVER = 5,
            BUSINESS_ADMIN = 6,
            FINANCIAL_MANAGER = 7,
            ACCOUNT_MANAGER = 8,
            CONTROLLER = 9,
            EXECUTIVE = 10,
            CEO_CFO = 11,
            JE_PREPARER = 12,
            JE_WRITEONOFF_APPROVER = 13,
            BACKUP_PREPARER = 14,
            BACKUP_REVIEWER = 15,
            BACKUP_APPROVER = 16,
            AUDIT = 17,
            TASK_OWNER = 18,
            USER_ADMIN = 19
        };

        public enum RiskRating
        {
            HIGH = 1,
            MEDIUM = 2,
            LOW = 3,
            CUSTOM = 4,
        }

        public enum ARTPages
        {
            Home = 1,
            UserSearch,
            EditUser,
            CompanyList,
            CreateUser,
            AccountProfileUpdate,
            AccountOwnership,
            AccountBulkUpdate,
            AccountMassUpdate,
            AccountViewer,
            GLAdjustments,
            GLAdjustmentsBulkClose,
            EditRecItemInputs,
            ReviewNotes,
            EditItemReviewNote,
            ItemInputWriteOnOff,
            ItemInputAccruable,
            UnexplainedVariance,

            TemplateBankAccount,
            TemplateDerivedCalc,
            TemplateAccruable,
            TemplateAmortizable,
            TemplateItemized,
            TemplateSubledger,

            EditItemAccrubleRecurring,
            NonRecurringImportUII,
            OSDepositBankNsfImport,
            RecFormButtons,
            DocumentUpload,
            CreateCompany,

            MandatoryReportSignOff,
            CertificationBalances,
            CertificationException,
            CertificationAccount,
            CertificationStatus,

            MandatoryReportsList,

            SystemReconciledAccounts,
            ItemInputAmortizableIndividual,

            ViewMatchSets,
            MatchingDataImport,
            DataTypeMapping,
            MatchingDataImportStatus,
            MatchingWizard,
            MatchSetResults,
            CreateRecItem,
            EditQualityScore,
            DashboardPreferences,
            ReportViewer,

            TaskViewer,
            ErrorPage,
            ChangePassword,

            RequestStatus,
            RecBinders

        }

        public enum MaterialType
        {
            MATERIALTYPEID_FSCAPTION = 1,
            MATERIALTYPEID_COMPANYWIDE = 2


        }

        public enum AlertType
        {
            ACCOUNT_TYPE = 1,
            EXCEPTION_TYPE
        }

        public enum Reports
        {
            UNUSUAL_BALANCES = 1,
            OPEN_ITEM = 2,
            EXCEPTION_STATUS = 3,
            ACCOUNT_STATUS = 4,
            CERTIFICATION_TRACKING = 5,
            RECONCILIATION_STATUS_COUNT_BY_USER_ROLE = 6,
            ACCOUNT_OWNERSHIP = 7,
            USER_ACCESS = 8,
            UNASSIGNED_ACCOUNTS = 9,
            INCOMPLETE_ACCOUNT_ATTRIBUTE = 10,
            DELINQUENT_ACCOUNTS_REPORT = 11,
            QUALITY_SCORE_REPORT = 12,
            COMPLETION_DATE_REPORT = 13,
            REVIEW_NOTES_REPORT = 14,
            NEW_ACCOUNT_REPORT = 15,
            ACCOUNT_ATTRIBUTE_CHANGE_REPORT = 16,
            TASK_COMPLETION_REPORT = 17
        }

        public enum ReportSection
        {
            STANDARD_REPORT = 1,
            MY_REPORT = 2
        }

        public enum DataImportStatus
        {
            Success = 1,
            Failure = 2,
            Warning = 3,
            Processing = 4,
            Submitted = 5,
            Draft = 6,
            Reject = 7
        }

        public enum ReconciliationStatus
        {
            Prepared = 1,
            InProgress,
            PendingReview,
            PendingModificationPreparer,
            Reviewed,
            PendingApproval,
            Approved,
            NotStarted,
            SysReconciled,
            Reconciled,
            PendingModificationReviewer
        }

        public enum DataImportStatusLabelID
        {
            Success = 1050,
            Error = 1051,
            Warning = 1546,
            Processing = 1620,
            Submitted = 1730
        }
        /// <summary>
        /// Enum for data upload rules label ids
        /// </summary>
        public enum DataImportTypeLabelID
        {
            GLDataLabelID = 1979,
            CurrencyExchangeRateDataLabelID = 1795,
            SubledgerDataLabelID = 1979,
            AccountAttributeListLabelID = 1599,
            HolidayCalendarLabelID = 1796,
            PeriodEndDatesLabelID = 1797,
            SubledgerSourceLabelID = 1798,
            GLTBSDataLabelID = 2368,
            AccountUploadLabelID = 1979,
            MultilingualUploadLabelID = 2475,
            UserUploadLabelID = 2523,
            ScheduleRecItemUploadLabelID = 2741,
            TaskUploadLabelID = 2747,
            RecControlChecklistLabelID = 2828
        }
        public enum RecPeriodStatus
        {
            NotStarted = 1,
            Open = 2,
            InProgress = 3,
            Closed = 4,
            Skipped = 5,
            OpeningInProgress = 6
        }

        public enum DataImportAvailableStatus
        {
            NotAvailable = 1,
            ToBeProcessed,
            Processing,
            Available
        }

        public enum GeographyClass
        {
            Company = 1,
            Key2,
            Key3,
            Key4,
            Key5,
            Key6,
            Key7,
            Key8,
            Key9,
            Key10
        }

        public enum RecCategory
        {
            GLAdjustments = 1,
            TimingDifference = 2,
            SupportingDetail = 3,
            VariancesWriteOffOn = 4,
            ReviewNotes = 5
        }

        public enum RecCategoryType
        {

            //Template_Category_CategoryType
            Amortizable_GLAdjustments_Total = 1,
            Amortizable_TimingDifference_Total = 2,
            Amortizable_SupportingDetail_RecurringAmortizableSchedule = 3,
            Amortizable_RecWriteoffOn_WriteoffOn = 4,
            Amortizable_RecWriteoffOn_UnexpVar = 5,
            Amortizable_SupportingDetail_IndividualAmortizableDetail = 40,

            DerivedCalculation_GLAdjustments_Total = 6,
            DerivedCalculation_TimingDifference_Total = 7,
            DerivedCalculation_SupportingDetail_OtherDetails = 8,
            DerivedCalculation_RecWriteoffOn_WriteoffOn = 9,
            DerivedCalculation_RecWriteoffOn_UnexpVar = 10,

            Subledger_GLAdjustments_Total = 11,
            Subledger_TimingDifference_Total = 12,
            Subledger_SupportingDetail_OtherDetails = 41,
            Subledger_RecWriteoffOn_WriteoffOn = 13,
            Subledger_RecWriteoffOn_UnexpVar = 14,

            Accrual_GLAdjustments_Total = 15,
            Accrual_TimingDifference_Total = 16,
            Accrual_SupportingDetail_IndividualAccrualDetail = 17,
            Accrual_SupportingDetail_RecurringAccrualSchedule = 18,//TODO: do we need this?
            Accrual_RecWriteoffOn_WriteoffOn = 19,
            Accrual_RecWriteoffOn_UnexpVar = 20,

            ItemizedList_GLAdjustments_Total = 21,
            ItemizedList_TimingDifference_Total = 22,
            ItemizedList_SupportingDetail_SupportingDetailList = 23,
            ItemizedList_RecWriteoffOn_WriteoffOn = 24,
            ItemizedList_RecWriteoffOn_UnexpVar = 25,

            BankAccount_GLAdjustments_BankFees = 26,
            BankAccount_GLAdjustments_NSFFees = 27,
            BankAccount_GLAdjustments_Other = 28,
            BankAccount_TimingDifference_DepositsInTransit = 29,
            BankAccount_TimingDifference_OutstandingChecks = 30,
            BankAccount_TimingDifference_Other = 31,
            BankAccount_RecWriteoffOn_WriteoffOn = 32,
            BankAccount_RecWriteoffOn_UnexpVar = 33,//=33

            Amortizable_ReviewNotes = 34,
            DerivedCalculation_ReviewNotes = 35,
            Subledger_ReviewNotes = 36,
            Accrual_ReviewNotes = 37,
            ItemizedList_ReviewNotes = 38,
            BankAccount_ReviewNotes = 39
        }

        public enum WriteOnOff
        {
            WriteOn = 1,
            WriteOff,
        }

        // for reference for attachments,(which kind of data they are attached with)
        public enum RecordType
        {
            GLDataHdr = 1,
            GLReconciliationItemInput = 2,
            ScheduleRecItem = 3,
            TaskCreation = 4,
            TaskComplition = 5,
            GLDataReviewNote = 8
        }

        public enum CertificationType
        {
            None = 0,
            MandatoryReportSignOff = 1,
            CertificationBalances,
            ExceptionCertification,
            Certification
        }

        #region AccountPageEnum

        public enum AccountPages
        {
            AccountProfileUpdate = 1,
            AccountOwnership,
            AccountBulkUpdate,
            AccountMassUpdate,
            AccountViewer,
            NetAccount,
            AccountInformation
        }

        #endregion

        public enum FormMode
        {
            None = 0,
            Edit,
            ReadOnly
        }

        public enum ReturnCodeStatusMarkOpen
        {
            GLDataNoSuccessUpload = 11,
            GLDataToBeProcessed,
            GLDataProcessing,
            GLDataWarning,

            CapabilityAllNotMarked = 21,
            CapabilityUnexpVarNotSet,
            CapabilityDetailNotComplete,

            ExchangeRateUploadNotAvailable = 31,

            SubledgerSourceNotUploaded = 41,
            SubledgerDataNotAvailable = 42, // 2 cases, if no successful uplaod, or for all subledger accounts rows are not there 
            SubledgerDataNotAvailableForAllSubledgerAccount = 43,
            SubledgerDataWarning = 44,

            ToBeReprocessed = 51,

            PreparerDueDateNotSet = 61,
            ReviewerDueDateNotSet = 62,
            ApproverDueDateNotSet = 63,
            CertificationStartDateNotSet = 64,
            CertificationLockDownDateNotSet = 65,
            ReconciliationCloseDateNotSet = 66,


            AttribteTemplateNotSetForAll = 71,
            AttribteOwnershipNotSetForAll = 72,

            NoAccountForThisPeriod = 81,
            ReconciliationFrequencyNotSet = 82,

            SRARulesNotSet = 91,
            CertificationVerbiageNotSet = 92,
            CompanyAlertNotSet = 93,
            FYNotSet = 94,

            JEConfigurationNotSet = 95,
            JEWriteOffOnApproversNotSet = 96,

            CurrencyExchangeRateNotAvailable = 97,

            DueDateIsNotAvailableForReconcilableAccounts = 98,
            DayTypeIsNotSetUp = 99



        }

        public enum ReportParameter
        {
            Entity = 1,
            Account = 2,
            Reason = 3,
            Period = 4,
            Key = 6,
            RiskRating = 7,
            Material = 8,
            User = 10,
            Role = 11,
            Preparer = 12,
            OpenItemClassification = 13,
            OpenDate = 14,
            CloseDate = 15,
            Aging = 16,
            RecStatus = 18,
            TypeOfException = 19,
            FinancialYear = 20,
            RangeOfScore = 21,
            ChecklistItem = 22,
            UserScore = 23,
            SystemScore = 24,
            CreationDate = 25,
            ChangeDate = 26,
            AsOnDate = 27,
            TaskStatus = 28,
            TaskListName = 29,
            TaskType = 30,
            DisplayColumn = 31
        }

        public enum ThresholdType
        {
            Days = 1,
            Percentage,
            Fixed
        }

        public enum AlertResponseType
        {
            Once = 1,
            Recurring,
            Mix
        }

        public enum ReportArchiveTypeMst
        {
            SignOff = 1,
            Archive
        }
        public enum Alert
        {
            AutoRemoveSignoff = 1,
            AccountReconciliationHasBeenRejected_Denied,
            DelinquentAccounts,
            WarningOfImpendingDelinquency,
            OpenPeriodForReconciliation,
            PeriodHasClosed,
            CertificationProcessHasOpened,
            AlertUserAboutRoleAndAccountChanges,
            CertificationStartDateIsApproachingInXDays,
            YouHaveXAccountsPendingModification,
            YouHaveXAccountsPendingReview,
            YouHaveXAccountsPendingApproval,
            YouHaveXAccountsWhichHaveAttributesThatHaveChanged,
            UploadHasFailed,
            HolidayCalendarNotSetUp,
            PeriodInformationNotSetUp,
            GLUpload_AccountInformationNotAvailable,
            CertificationLockdownDateIsMissingForXPeriods,
            NewAccountsAvailable,
            XAccountsDoNotHaveAttributesSetUp,
            CompanySubscriptionIsAboutToExpireInXDays,
            CompanyDataStorageCapacityIsAboutToFinishSoon_AnotherXMbsLeft,
            UserCreationLicenseIsAboutToExpireSoon_OnlyXNewUsersCanBeCreated,
            RecCloseDateMissing,
            AlertToNotifyBackUpOwners = 25,
            YouHaveXTaskAssigned,
            YoyHaveXTaskForApproval = 29,
            YoyHaveXTaskForReview = 34

        }

        public enum ReportType
        {
            MandatoryReport = 1,
            StandardReport,
            ArchivedReport,
            UserSavedReport,
            MandatoryReportSignedOff,
            UserSavedReportChangedParams
        }



        public enum ReportParameterKeyMst
        {
            Key2 = 1,
            Key3,
            Key4,
            Key5,
            Key6,
            Key7,
            Key8,
            Key9,
            FromAccount,
            ToAccount,
            Reason,
            Period,
            KeyAccount,
            RiskRating,
            MaterialAccount,
            User,
            Role,
            PreparerUser,
            PreparerRole,
            OpenItemClassification,
            FromOpenDate,
            ToOpenDate,
            FromCloseDate,
            ToCloseDate,
            Aging,
            RecStatus,
            TypeOfException

        }

        #region AppSetting
        public enum AppSetting
        {
            MaxFileUploadSize = 1
        }

        #endregion

        public enum Operator
        {
            Equals = 1,
            Between,
            Contains,
            GreaterThan,
            LessThan,
            GreaterThanEqualTo,
            LessThanEqualTo,
            NotEqualTo,
            Matches
        }

        public enum StaticAccountField
        {
            ShowException = -1001, // Sentinel Value added by Apoorv
            ShowExceptionNetAccounts = -1002, // Sentinel Value added by Apoorv
            AccountNumber = 11,
            AccountName,
            ReconciliationStatus,
            CertificationStatus,
            GLBalance,
            RecBalance,
            UnexplainedVar,
            WriteOnOff,
            Materiality,
            ZeroBalance,
            KeyAccount,
            RiskRating,
            Preparer,
            Reviewer,
            Approver,
            AccountType,
            FSCaption,
            NetAccountName = 29,
            BackupPreparer = 30,
            BackupReviewer = 31,
            BackupApprover = 32,
            PreparerDueDate = 57,
            ReviewerDueDate = 58,
            ApproverDueDate = 59,
            TMStatus = 60
        }

        /// <summary>
        /// SingnatureEnum is used to identify that mail is sent by User/System.
        /// </summary>
        public enum SignatureEnum
        {
            SendByUser,
            SendBySystemAdmin,
            SendBySkyStemSystem
        }

        public enum Feature
        {
            MultiCurrency = 1,
            DualLevelReview = 2,
            MandatoryReportSignOff = 3,
            KeyAccount = 4,
            Reports = 5,
            DefaultNoOfUsers = 6,
            DefaultDataStorageForTheLifetime = 7,
            JournalEntry = 8,
            MatchingEntry = 9,
            ChangeManagement = 10,
            Analytics = 11,
            KeyChanges = 12,
            AccountOwnershipBackup = 13,
            SharedAccounts = 14,
            SuperCompany = 15,
            MultiVersionGL = 16,
            InternalAudit = 17,
            FSVariance = 18,
            PandLVariance = 19,
            Personalization = 20,
            Certification = 21,
            ExternalAudit = 22,
            CEO_CFOCertification = 23,
            QualityScore = 24,
            MappingUpload = 25,
            AuditRole = 26,
            UserUpload = 27,
            TaskMaster = 28,
            Reconciliation = 29,
            DueDateByAccount = 30,
            MultiDocumentUpload = 31,
            UserAdmin = 32,
            SignedReports = 33,
            DownloadAllRecs = 34,
            ERecBinders = 35,
            DownloadAllAttachments = 36,
            RecControlChecklist = 37
        }

        public enum FeatureCapabilityMode
        {
            Visible = 1,
            Hidden,
            Disable
        }

        public enum RecStatusBarPageType
        {
            RecForm = 1,
            RecFormPrint,
            OtherPages
        }

        public enum WizardType
        {
            Matching = 1,

        };

        public enum DataType
        {
            Integer = 1,
            String,
            Boolean,
            Decimal,
            DataTime
        };

        public enum BindingModes
        {
            DataTableBinding = 1,
            XmlBinding = 2
        };

        public enum RecItemColumns
        {
            Date = 1,
            Description = 2,
            LCCYCode = 3,
            AmountLCCY = 4,
            RefNo = 5,
            ScheduleBeginDate = 6,
            ScheduleEndDate = 7,
            ScheduleName = 8,
            RecItemNumber = 9

        };

        public enum MatchingStatus
        {
            Draft = 1,
            ToBeProcessed = 2,
            InProgress = 3,
            Success = 4,
            Warning = 5,
            SevereWarning = 6,
            Error = 7
        };
        public enum MatchingResultType
        {
            Matched = 1,
            PartialMatched = 2
        };

        public enum AccountKeyMapping
        {
            Company = 1,
            FSCaption = 2,
            AccountType = 3,
            Key2 = 4,
            Key3 = 5,
            Key4 = 6,
            Key5 = 7,
            Key6 = 8,
            Key7 = 9,
            Key8 = 10,
            Key9 = 11,
            AccountNumber = 12
        }

        public enum TaskColumnForFilter
        {
            TaskNumber = 33,
            TaskName = 34,
            Description = 35,
            TaskList = 37,
            AttachmentCount = 40,
            StartDate = 36,
            DueDate = 42,
            TaskDuration = 43,
            RecurrenceType = 41,
            AssignedTo = 38,
            Approver = 61,
            Reviewer = 39,
            ApproverComment = 46,
            ApprovalDuration = 47,
            CompletionDate = 48,
            CompletionDocs = 49,
            CompletionComment = 50,
            CreatedBy = 51,
            TaskStatus = 52,
            AssigneeDueDate = 56,
            TaskReviewerDueDate = 62,
            CustomField1 = 64,
            CustomField2 = 65,
            TaskSubList = 63,
        }
        public enum DualLevelReviewType
        {
            CompanyWideDualLevelReview = 1,
            DualLevelReviewbyAccount = 2
        };

        public enum HandlerActionType
        {
            DownloadRecAttachments = 1
        }
        public enum DownloadMode
        {
            attachment = 1,
            inline = 2
        }
        public enum TaskCustomField
        {
            CustomField1 = 1,
            CustomField2 = 2
        }
    }//end of class
}//end of namespace
