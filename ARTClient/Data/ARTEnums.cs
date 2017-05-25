using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SkyStem.ART.Client.Data
{
    public class ARTEnums
    {
        public enum ReconciliationItemTemplate
        {
            None = 0,
            BankForm = 1,
            DerivedCalculationForm = 2,
            Subledgerform = 3,
            AccrualForm = 4,
            AmortizableBalanceForm = 5,
            ItemizedListForm = 6
        }

        public enum Status
        {
            All = 0,
            Active = 1,
            Inactive = 2,

        }

        public enum AccountAttribute
        {
            Description = 1,
            //AccountType,
            ReconciliationTemplate = 2,
            IsKeyAccount = 3,
            IsZeroBalanceAccount = 4,
            RiskRating = 5,
            SubledgerSource = 6,
            AccountPolicyURL = 7,
            ReconciliationProcedure = 8,
            Preparer = 9,
            Reviewer = 10,
            Approver = 11,
            NetAccount = 12,
            IsExcludeOwnershipForZBA = 13,
            IsActive = 14,
            RecFrequency = 15,
            IsReconcilable = 16,
            BackupPreparer = 17,
            BackupReviewer = 18,
            BackupApprover = 19,
            PreparerDueDays = 20,
            ReviewerDueDays = 21,
            ApproverDueDays = 22
            //DayType = 23
        }

        public enum Capability
        {
            MaterialitySelection = 1,
            RiskRating = 2,
            KeyAccount = 3,
            DualLevelReview = 4,
            MandatoryReportSignoff = 5,
            ZeroBalanceAccount = 6,
            MultiCurrency = 7,
            CertificationActivation = 8,
            NetAccount = 9,
            CEOCFOCertification = 10,
            AllowDeletionOfReviewNotes = 11,
            JournalEntryWriteOffOnApprover = 12,
            DueDateByAccount = 13,
            RecControlChecklist = 14,
            ReopenRecOnCCYreload = 15
        }

        public enum Grid
        {
            None = 0,
            AccountViewer = 1,
            AccountViewerSRA,
            AccountOwnership,
            AccountProfileMassAndBulkUpdate,
            AccountTasks = 5,
            GeneralTaskPending = 6,
            GeneralTaskCompleted = 7,
            AccountTaskPending = 8,
            AccountTaskCompleted = 9,
            DataImportStatusAccountMessage = 10
        }

        public enum DataImportFieldsMaxLength
        {
            FSCaption = 200,
            KeyFields = 100,
            HolidayName = 100,
            SubLedgerName = 100,
            AccountName = 100,
            AccountNumber = 20,
            AccountDescription = 2000,
            AccountPolicyURL = 2000,
            ReconciliationProcedure = 2000,
            Comments = 1000
        }

        public enum GLDataProcessingStatus
        {
            Processing = 1,
            ToBeProcessed,
            Processed
        }

        public enum MatchingWizardSteps
        {
            MatchingSourceSelection = 2198,
            MatchingSourceFilter = 2199,
            MatchingConfiguration = 2200,
            DisplayColumnSelection = 2201,
            RecItemColumnMapping = 2202,
            PreviewConfirm = 2275
        }
        public enum MatchingSourceType
        {
            GLTBS = 11,
            NBF = 12
        }
        public enum GLDataRecItemType
        {
            GLDataRecItem = 1,
            GLDataRecurringItemSchedule = 2,
            GLDataWriteOnOff = 3
        }

        public enum RecordSourceType
        {
            DataImport = 1,
            Matching = 2,
            UI = 3,
            FTP = 4
        }

        public enum MatchingStatus
        {
            Draft = 1,
            ToBeProcessed = 2,
            InProgress = 3,
            Success = 4,
            Warning = 5,
            SevereWarning = 6,
            Error = 7
        }

        public enum MatchingType
        {
            AccountMatching = 1,
            DataMatching = 2
        }

        public enum CalculationFrequency
        {
            DailyInterval = 1,
            OtherInterval = 2
        }
        public enum WizardSteps
        {
            MatchingSourceSelection = 1,
            MatchingSourceFilter = 2,
            MatchingConfiguration = 3,
            DisplayColumnSelection = 4,
            RecItemColumnMapping = 5,
            PreviewConfirm = 6
        }

        public enum RecItemControl
        {
            GLAdjustments = 1,
            ItemInputAccurable = 2,
            ItemInputAccurableRecurring = 3,
            ItemInputAmortizableIndividual = 4,
            ItemInputAmortizableTemplate = 5,
            ItemInputWriteOff = 6,
            UnexplainedVariance = 6
        }

        public enum QualityScoreStatus
        {
            Yes = 1,
            No = 2
        }

        public enum YesNo
        {
            Yes = 1,
            No = 2
        }

        public enum YesNoLabel
        {
            Yes = 1252,
            No = 1251
        }

        public enum QualityScoreType
        {
            SystemScore = 1,
            UserScore = 2
        }

        public enum QualityScoreItem
        {
            UnexpVarThreshold = 1,
            SupportingDocumentation = 2,
            GLAdjustmentsAging = 3,
            WriteOffOnAging = 4,
            TimingDiffAging = 5,
            PreparerDueDate = 6,
            ReviewerDueDate = 7,
            ApproverDueDate = 8,
            RiskRating = 9,
            NoWriteOffItem = 10,
            NoCapabilityChanges = 11
        }

        public enum SystemLockdownReason
        {
            UploadDataProcessingRequired = 1,
            ReconcilablityCalcuationRequired = 2,
            SRAProcessingRequired = 3
        }

        public enum TaskType
        {
            GeneralTask = 1,
            AccountTask = 2
        }

        public enum TaskStatus
        {
            NotStarted = 1,
            PendReview = 2,
            PendModAssignee = 3,
            Completed = 4,
            InProgress = 5,
            Deleted = 6,
            PendModReviewer = 7,
            PendApproval = 8
        }

        public enum TaskStatusLabelID
        {
            NotStarted = 1475,
            PendReview = 1091,
            PendModAssignee = 2558,
            Completed = 2559,
            InProgress = 1090,
            Deleted = 2646,
            PendModReviewer = 1756,
            PendApproval = 1094
        }

        public enum TaskCompletionStatus
        {
            Pending = 1,
            Overdue = 2,
            Completed = 3
        }

        public enum TaskRecurrenceType
        {
            NoRecurrence = 1,
            EveryRecPeriod = 2,
            Custom = 3,
            Quarterly = 4,
            Annually = 5,
            MultipleRecPeriod = 6
        }

        public enum TaskAttribute
        {
            TaskName = 1,
            TaskListID = 2,
            Description = 3,
            AssignedTo = 4,
            Reviewer = 5,
            Notify = 6,
            AttachedDocuments = 7,
            RecurrenceType = 8,
            RecurrenceFrequency = 9,
            TaskStartDate = 10,
            TaskDueDate = 11,
            AssigneeDueDate = 12,
            ReviewerDueDate = 13,
            AssigneeDueDays = 14,
            ReviewerDueDays = 15,
            TaskAccount = 16,
            TaskDueDays = 17,
            IsDeleted = 18,
            RecurrencePeriodNumber = 19,
            TaskDueDaysBasis = 20,
            AssigneeDueDaysBasis = 21,
            TaskDueDaysBasisSkipNumber = 22,
            AssigneeDueDaysBasisSkipNumber = 23,
            Approver = 24,
            ReviewerDueDaysBasis = 25,
            ReviewerDueDaysBasisSkipNumber = 26,
            CustomField1 = 27,
            CustomField2 = 28,
            TaskSubListID = 29,
            DaysType = 30
        }

        public enum TaskActionType
        {
            Add = 1,
            BulkEdit = 2,
            Remove = 3,
            Approve = 4,  // task Approver
            Reject = 5,
            Complete = 6,// task assignee
            Reopen = 7,
            Review = 8, // task reviewer
            RemoveDeleted = 9,
            Save=10
        }

        public enum TaskCategory
        {
            All = 0,
            MyTask = 1,
            SelfTask = 2,
            CreatedTask = 3
        }

        public enum RecordType
        {
            GLDataHdr = 1,
            GLReconciliationItemInput = 2,
            ScheduleRecItem = 3,
            TaskCreation = 4,
            TaskComplition = 5
        }
        public enum Package
        {
            Basic = 1,
            Standard = 2,
            Enterprise = 3,
            TaskMaster = 4
        }
        public enum ActionType
        {
            CreateRecItem = 1,
            CloseRecItem = 2,
            GLDataLoad = 3,
            SubLedgerLoad = 4,
            ForceReprocessSRAFromUI = 5,
            ReprocessSRAfromService = 6,
            CalculateReconciliability = 7,
            AccountAttributeLoad = 8,
            DeleteNetAccount = 9,
            ChangeNetAccountComposition = 10,
            AccountAttributeChangeFromUI = 11,
            ReopenReconciliationFromUI = 12,
            ResetReconciliationFromUI = 13,
            UpdateSystemWideSettings = 14,
            CloseRecAndCertStart = 15,
            ForceCloseRecPeriodFromUI = 16,
            UpdateRecFromUI = 17,
            CloseRecPeriodFromService = 18,
            ReopenRecPeriodFromUI = 25,

        }

        public enum ChangeSource
        {
            NetAccountCompositionChange = 1,
            AccountReconcilabilityChange = 2,
            GLStatusChange = 3,
            RecPeriodStatusChange = 4,
            AccountCreation = 5,
            GLDataLoad = 6,
            SubLedgerLoad = 7,
            ZBAAttributeChanged = 8,
            RecTemplateChanged = 9,
            ReprocessSRA = 10,
            AccountAttributeLoad = 11,
            DeleteNetAccount = 12,
            ReopenReconciliation = 13,
            ResetReconciliation = 14,
            ProcessReconcilabilityFlag = 15,
            ReopenRecPeriodFromUI = 18,
        }
        public enum DueDaysBasis
        {
            PeriodEndDate = 1,
            MonthEndDate = 2
        }
        public enum DaysType
        {
            CalendarDays = 1,
            BusinessDays = 2
        }
        public enum FileType
        {
            Permanent = 1,
            Temporary = 2
        }
        public enum UserStatus
        {
            All = 0,
            Activated = 2,
            Deactivated = 3,
        }

        public enum AttributeList
        {
            ReconciledAccountsInOpenPeriod = 1,
            ReconciledAccountsInClosedPeriod = 2,
            NotSeeReviewNoteSection = 3,
            NotSeeQSSection = 4,
            ViewReportActivity = 5,
            ViewPeriodsOnlyInRange = 6,
            ViewPeriodsInRangeFrom = 7,
            ViewPeriodsInRangeTo = 8,
            NotSeeRecControlChecklist = 9
        }

        public enum AutoSaveAttribute
        {
            AutoSaveFinancialYearSelection = 1,
            AutoSaveRecPeriodSelection = 2
        }

        public enum RequestType
        {
            ExportToExcel = 1,
            DownloadAllRecFormsDetailed = 2,
            CreateBinders = 3,
            DownloadSelectedRecFormsDetailed = 4,
        }
        public enum RequestStatus
        {
            Submitted = 1,
            Processing = 2,
            Success = 3,
            Error = 4
        }

        public enum DataImportMessageType
        {
            Success = 1,
            Warning = 2,
            Error = 3
        }

        public enum DataImportMessageCategory
        {
            AccountMessages = 1,
            OtherMessages = 2,
        }

        public enum DataImportFields
        {
            Company = 1,
            PeriodEndDate = 2,
            FSCaption = 3,
            AccountType = 4,
            IsProfitAndLoss = 5,
            AccountNumber = 6,
            AccountName = 7,
            BaseCCYCode = 8,
            BalanceInBaseCCY = 9,
            ReportingCCYCode = 10,
            BalanceInReportingCCY = 11,
            Key2 = 12,
            Key3 = 13,
            Key4 = 14,
            Key5 = 15,
            Key6 = 16,
            Key7 = 17,
            Key8 = 18,
            Key9 = 19
        }
        public enum ActivationStatus
        {
            All = 0,
            Activated = 1,
            Deactivated = 2
        }

        //TODO: Get LabelID from database and cache
        public enum ActivationStatusLabelID
        {
            Activated = 2797,
            Deactivated = 2798
        }

        public enum ActivationStatusType
        {
            UserActivationStatus = 1,
            FTPActivationStatus = 2
        }
        /// <summary>
        /// Enum for Data Import Type
        /// </summary>
        public enum DataImportType
        {
            GLData = 1,
            CurrencyExchangeRateData = 2,
            SubledgerData = 3,
            AccountAttributeList = 4,
            HolidayCalendar = 5,
            PeriodEndDates = 6,
            SubledgerSource = 7,
            RecItems = 8,
            ScheduleRecItems = 9,
            CompanyLogo = 10,
            GLTBS = 11,
            NBF = 12,
            AccountUpload = 13,
            MultilingualUpload = 14,
            UserUpload = 15,
            GeneralTaskImport = 16,
            RecControlChecklist = 17,
            RecControlChecklistAccount = 18
        }

        public enum CapabilityAttribute
        {
            MultiCurrencyReopenRecOnCCYReload = 1
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
    }
}
