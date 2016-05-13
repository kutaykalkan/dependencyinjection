using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SkyStem.ART.Service.Data
{
    public class Enums
    {
        public enum DataImportStatus
        {
            Success = 1,
            Failure,
            Warning,
            Processing,
            ToBeProcessed
        }
        public enum GLDataProcessingStatus
        {
            Processing = 1,
            ToBeProcessed,
            Processed
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
            GLTBS = 11,
            NBF = 12,
            AccountDataImport = 13,
            MultilingualUpload = 14,
            UserUpload = 15,
            GeneralTaskImport = 16

        }
        public enum DataImportFieldsMaxLength
        {
            FSCaption = 200,
            IsProfitAndLoss = 100,
            KeyFields = 100,
            HolidayName = 100,
            SubLedgerName = 100,
            AccountName = 250,
            AccountNumber = 20,
            AccountDescription = 2000,
            AccountPolicyURL = 2000,
            ReconciliationProcedure = 2000,
            Comments = 1000,

            AccountType = 100,
            RiskRating = 100,
            RecTemplate = 100,
            IsKeyAccount = 100,
            IsZeroBalance = 100,
            NatureOfAccount = 2000,

            //Account Ownership fields
            Preparer = 128,
            Reviewer = 128,
            Approver = 128
            //DayType = 100
        }
        public enum WarningReason
        {
            SumNotZero = 1,
            DuplicateAccounts = 2,
            NewAccounts = 3
        }

        public enum Capability
        {
            //MaterialitySelection = 1,
            RiskRating = 2,
            KeyAccount = 3,
            DualLevelReview = 4,
            //MandatoryReportSignoff,
            ZeroBalanceAccount = 6,
            //    ,
            //MultiCurrency,
            //CertificationActivation,
            //NetAccount,
            //CEOCFOCertification,
            //AllowDeletionOfReviewNotes
            DueDateByAccount = 13
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

        public enum RecPeriodStatus
        {
            NotStarted = 1,
            Open,
            InProgress,
            Closed,
            Skipped
        }

        public enum DataType
        {
            Integer = 1,
            String,
            Boolean,
            Decimal,
            DataTime
        }

        #region "Matching"
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
            fullMatch = 1,
            partialMatch = 2,
            unMatched = 3,
            common = 4
        }

        public enum OperatorType
        {
            Equals = 1,
            Between = 2,
            Contains = 3,
            Greaterthan = 4,
            Lessthan = 5,
            Greaterthanequalto = 6,
            Lessthanequalto = 7,
            Notequalto = 8,
            Matches = 9
        }

        public enum ThresholdType
        {
            Days = 1,
            Percentage = 2,
            Fixed = 3
        }

        public enum MatchingSourceType
        {
            Source1 = 1,
            Source2 = 2
        }
        #endregion

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

        public enum UserRoles
        {
            SystemAdmin = 2,
            Preparer = 3,
            Reviewer = 4,
            Approver = 5,
            BusinessAdmin = 6,
            FinancialManager = 7,
            AccountManager = 8,
            Controller = 9,
            Executive = 10,
            CEOCFO = 11,
            JEPreparer = 12,
            JEWriteOffOnApprover = 13,
            BackupPreparer = 14,
            BackupReviewer = 15,
            BackupApprover = 16,
            Audit = 17
        }
        public enum UserUploadFieldMaxLength
        {
            FirstName = 50,
            LastName = 50,
            EmailID = 50,
            LoginID = 50,
            DefaultRole = 50
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
        #region "Task Upload"
        public enum TaskUploadFieldMaxLength
        {
            TaskListName = 100,
            TaskName = 500,
            TaskAssignee = 128,
            RecurrenceType = 50
        }

        public enum TaskRecurrenceType
        {
            NoRecurrence = 1,
            EveryRecPeriod = 2,
            Custom = 3
        }

        #endregion
        #region DataImportMessage

        public enum DataImportMessage
        {
            NotNetTo0 = 1,
            DuplicateAccountsExist = 2,
            WillCreateNewAccount = 3,
            WillUpdatExistingAccount = 4,
            PartOfAnotherNetAccount = 5,
            AccountWithDifferentAttribute = 6,
            ChangeRecStatus = 7,
            MoreThenOneValueForPeriodEndDate = 8,
            MoreThenOneRCCYValues = 9,
            InvalidValueForIsPL = 10,
            NoDataForMandatoryField = 11,
            InvalidValueForAccountType = 12,
            InvalidValue = 13,
            DuplicateRowsFound = 14,
            BalanceOrCurrencyCodeHaveChanged = 15,
            DifferentRCCYValueForSamePeriod = 16,
            MoreThenOneBCCYValues = 17,
            BCCYAndRCCYValuesCannotBeDifferent = 18,
            ExchangeRateNotAvailable = 19,
            AccountDataMismatch = 20,
            InputReconciliationPeriodNotPresent = 21,
            InputReconciliationPeriodMismatch = 22,
            DataImportedSuccessfully = 23,
            AccountNotAccessible = 24,
            InvalidValueForFSCaption = 25,
            AccountDoesNotExists = 26,
            SubledgerAlreadyUploaded = 27,
            DataLengthExceeded = 28,
            MandatoryFieldsNotPresent = 29,
            ColumnsForNewAccountCreationNotPresent = 30,
            SubledgerBalanceORCurrencyCodehaveChanged = 31,
            AccountExistsButDataMismatchWithExisting = 32,
            InvalidSubledgerAccounts = 33,
            ExchangeRateHaveChanged = 34,
            ExchangeRatePresentInPreviousMissingInCurrentPeriod = 35,
            ReverseConversionNotPresent = 36,
            WorkStartedInSubsequentPeriods = 37,
            PartialConversionProvidedForLCCY = 38,
            InvalidDataLength = 39
        }
        #endregion

    }
}
