using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SkyStem.ART.Service.Data
{
    public class ServiceConstants
    {
        public const Int16 DEFAULT_INTERVAL = 10; // default internval is 1 hr = 60 mins
        public const string SERVICE_PROCESSING_USER_ID = "SystemUserServiceProcessing";
        public const string SERVICE_NAME_REC_PERIOD = "Rec Period Status Processing Service";
        public const string SERVICE_NAME_DATA_PROCESSING = "Data Processing Service";
        public const string SERVICE_NAME_ALERT_PROCESSING = "Alert Processing Service";
        public const string SERVICE_NAME_MATCHING_FILE_PROCESSING = "Matching File Processing Service";
        public const string SERVICE_NAME_MATCHING_ENGINE_PROCESSING = "Matching Engine Processing Service";
        public const string SERVICE_NAME_MULTILINGUAL_UPLOAD_PROCESSING = "Multilingual Upload Processing Service";
        public const string SERVICE_NAME_EXPORTTOEXCEL_PROCESSING = "Export To Excel Processing Service";
        public const string SERVICE_NAME_USERUPLOAD_PROCESSING = "User Upload Processing Service";
        public const string SERVICE_NAME_TASKUPLOAD_PROCESSING = "Task Upload Processing Service";
        public const string SERVICE_NAME_COMPANY_CREATION_PROCESSING = "Company Creation Service";
        public const string SERVICE_NAME_ACCOUNT_RECONCILABILITY_PROCESSING = "Account Reconcilability Processing Service";
        public const string SERVICE_NAME_INDEX_RECREATION_SERVICE = "Index Recreation Service";
        public const string SERVICE_NAME_REC_ITEM_IMPORT_SERVICE = "Rec Item Import Service";
        public const string SERVICE_NAME_CLEAR_COMPANY_CACHE_SERVICE = "Clear Company Cache Service";
        public const string SERVICE_NAME_FTPDATAIMPORT_PROCESSING = "FTP DataImport Processing Service";
        public const string SERVICE_NAME_THREAD_CHECK_PROCESSING = "Thread Check Service";
        public const string HYPHEN = "-";
        public const string GLDATA = "GLData";
        public const string SUBLEDGER = "Subledger Data";
        public const string CURRENCYLOAD = "Currency Load";
        public const string ACCOUNT_ATTRIBUTE_DATA = "Account Attribute Data";
        public const string DEFAULTSERVICELOGFILEPATH = @"C:\";
        public const string ERRORMESSAGESEPERATOR = ",";
        public const int DEFAULTLANGUAGEID = 1033;
        public const int DEFAULTBUSINESSENTITYID = 0;
        public const string LOGGER_NAME = "SkyStemARTServiceLogger";
        public const string LOGGER_NAME_SERVICE_TIME_STAMP = "ServiceTimeStampLogger";
        public const string MATCHING_SHEETNAME = "DATA";
        public const string GLDATA_SHEETNAME = "DATA";//"GLData$";
        public const string ACCOUNTATTRIBUTE_SHEETNAME = "DATA";//"AccountAttributeData$";
        public const string SUBLEDGER_SHEETNAME = "DATA";//"SubledgerData$";
        public const string MATCHING_RULESXMLTYPE = "Rule";
        public const string MATCHING_RULESLISTXMLTYPS = "RuleList";
        public const string MATCHING_SOURCE_COLUMNXML = "Column";
        public const string MATCHING_SOURCE_COLUMN_LIST_XML = "ColumnList";
        public const string MATCHING_GLTBS_DATATABLE_NAME = "GLTBS";
        public const string MATCHING_NBF_DATATABLE_NAME = "NBF";
        public const string MATCHING_ACCOUNT_DATATABLE_NAME = "Account";
        public const string MATCHING_CHILD_ACCOUNT_DATATABLE_NAME = "ChildAccounts";
        public const string MATCHING_FULLMATCHPAIR_DATATABLE_NAME = "FullMatchPair";
        public const string MATCHING_PARTIALMATCHPAIR_DATATABLE_NAME = "PartialMatchPair";
        public const string ACCOUNTDATA_SHEETNAME = "DATA";//"GLData$";
        public const string ACCOUNTDATA = "Account Data";
        public const string EXPORTSHEETNAME = "ExportData";
        public const string USERDATA_SHEETNAME = "DATA";//"SubledgerData$"
        public const int LENGTH_GENERATED_PASSWORD = 10;
        public const string USER_UPLOAD_DATA = "UserUpload Data";
        public const string TASK_IMPORT_SHEETNAME = "DATA";//"SubledgerData$"
        public const string SCHEDULE_REC_ITEM_UPLOAD_DATA = "Schedule Rec Item Data";
        public const string ART_TEMPLATE = "-1";
    }

    public class LoggingConstants
    {
        public const string SERVICE_INITIALIZE_TEXT = "{0} -> Initialize Component at: {1}";
        public const string SERVICE_START_TEXT = "{0} -> Service Starting at: {1}";
        public const string SERVICE_STOP_TEXT = "{0} -> Service Starting at: {1}";
        public const string SERVICE_PROCESSING_BEGINS_TEXT = "{0} -> Data Processing Begins at: {1}";
        public const string SERVICE_PROCESSING_ENDS_TEXT = "{0} -> Data Processing Ends at: {1}";
        public const string SERVICE_TIME_STAMP_TEXT = "BEGIN: {0}, END: {1}, TOTAL: {2}, Service: {3}";
        public const string SERVICE_THREAD_ERROR_TEXT = "SERVICE THREAD: {0}, ERROR MSG: {1}, STACK TRACE: {2}";
        public const string SERVICE_RESTART_TEXT = "RESTARTING SERVICE {0} at: {1}";
    }

    public class AppSettingConstants
    {
        #region "Email Constants"
        public const string EMAIL_SMTP_SERVER = "SmtpServer";
        public const string EMAIL_SMTP_PORT = "SmtpPort";
        public const string EMAIL_USER_NAME = "UserName";
        public const string EMAIL_PASSWORD = "password";
        public const string EMAIL_USE_TEST_ACCOUNT = "UseTestAccount";
        public const string EMAIL_ENABLE_SSL = "EnableSSL";
        public const string EMAIL_FROM_DEFAULT = "defaultEmailFromCompany";
        public const string EMAIL_SYSTEM_URL = "SystemURL";
        #endregion

        public const string TIMER_INTERVAL_DATA_PROCESSING = "DataProcessingTimerIntervalInMins";
        public const string TIMER_INTERVAL_REC_PERIOD_STATUS_PROCESSING = "RecPeriodStatusProcessingTimerIntervalInMins";
        public const string TIMER_INTERVAL_ALERT_PROCESSING = "AlertProcessingTimerIntervalInMins";
        public const string TIMER_INTERVAL_MATCHING_FILE_PROCESSING = "MatchingFileProcessingTimerIntervalInMins";
        public const string TIMER_INTERVAL_MATCHING_ENGINE_PROCESSING = "MatchingEnginePcessingTimerIntervalInMins";

        public const string TIMER_INTERVAL_MULTILINGUAL_UPLOAD_PROCESSING = "MultilingualUploadProcessingTimerIntervalInMins";
        public const string TIMER_INTERVAL_EXPORTTOEXCEL_PROCESSING = "ExportToExcelProcessingTimerIntervalInMins";
        public const string TIMER_INTERVAL_USERUPLOAD_PROCESSING = "UserUploadProcessingTimerIntervalInMins";
        public const string TIMER_INTERVAL_COMPANY_CREATION_PROCESSING = "CompanyCreationTimerIntervalInMins";
        public const string TIMER_INTERVAL_ACCOUNT_RECONCILABILITY_PROCESSING = "AccountReconcilabilityProcesssingTimerIntervalInMins";
        public const string TIMER_INTERVAL_TASKUPLOAD_PROCESSING = "TaskUploadProcessingTimerIntervalInMins";
        public const string TIMER_INTERVAL_INDEX_RECREATION_SERVICE_PROCESSING = "IndexRecreationServiceProcesssingTimerIntervalInMins";
        public const string TIMER_INTERVAL_REC_ITEM_IMPORT_PROCESSING = "RecItemImportProcessingTimerIntervalInMins";
        public const string TIMER_INTERVAL_CLEAR_COMPANY_CACHE_PROCESSING = "ClearCompanyCacheTimerIntervalInMins";
        public const string TIMER_INTERVAL_FTPDATAIMPORT_PROCESSING = "FTPDataImportProcessingTimerIntervalInMins";
        public const string TIMER_INTERVAL_THREAD_CHECK_PROCESSING = "ThreadcheckTimerIntervalInMins";
        public const string THREAD_CHECK_TIMEOUT = "ThreadcheckTimeoutInMins";
        public const string TIMER_INTERVAL_CURRENCY_IMPORT_PROCESSING = "CurrencyImportProcessingTimerIntervalInMins";

        public const string BASE_FOLDER_FOR_FILES = "BaseFolderForFiles";
        public const string FOLDER_FOR_DOWNLOAD_REQUESTS = "FolderForDownloadRequests";

        public const string EXPORT_FILE_PATH = "ExportToExcelFilePath";
    }

    public struct DataImportStatus
    {
        public const string DATAIMPORTFAIL = "FAIL";
        public const string DATAIMPORTSUCCESS = "SUCCESS";
        public const string DATAIMPORTWARNING = "WARNING";
        public const string DATAIMPORTSEVEREWARNING = "SEVEREWARNING";
        public const string DATAIMPORTERRORS = "ERROR";
    }

    public struct AccountDataImportFields
    {

        public const string COMPANY = "Company";
        public const string GLACCOUNTNUMBER = "GL Account #";
        public const string GLACCOUNTNAME = "GL Account Name";
        public const string FSCAPTION = "FS Caption";
        public const string ACCOUNTTYPE = "Account Type";
        public const string ISPROFITANDLOSS = "Is P&L";

    }

    public struct GLDataImportFields
    {

        public const string PERIODENDDATE = "Period End Date";
        public const string COMPANY = AccountDataImportFields.COMPANY;
        public const string GLACCOUNTNUMBER = AccountDataImportFields.GLACCOUNTNUMBER;
        public const string GLACCOUNTNAME = AccountDataImportFields.GLACCOUNTNAME;
        public const string FSCAPTION = AccountDataImportFields.FSCAPTION;
        public const string ACCOUNTTYPE = AccountDataImportFields.ACCOUNTTYPE;
        public const string ISPROFITANDLOSS = AccountDataImportFields.ISPROFITANDLOSS;
        public const string BCCYCODE = "Base CCY Code";
        public const string BALANCEBCCY = "Balance in Base CCY";
        public const string RCCYCODE = "Reporting CCY Code";
        public const string BALANCERCCY = "Balance in Reporting CCY";

        public const short GLDATAIMPORTMANDATORYFIELDCOUNT = 11;
        public const short ACCOUNTDATAIMPORTMANDATORYFIELDCOUNT = 6;
        //public const string[] MANDATORYGLDATAIMPORTFIELDS = {"Period End Date", "Company", "GL Account #"
        //, "GL Account Name", "FS Caption", "Account Type"
        //, "Base CCY Code", "Balance in Base CCY"
        //, "Balance in Reporting CCY","Reporting CCY Code"};
    }

    public struct SubledgerDataImportFields
    {

        public const string PERIODENDDATE = "Period End Date";
        public const string GLACCOUNTNUMBER = AccountDataImportFields.GLACCOUNTNUMBER;
        public const string GLACCOUNTNAME = AccountDataImportFields.GLACCOUNTNAME;
        public const string FSCAPTION = AccountDataImportFields.FSCAPTION;
        public const string ACCOUNTTYPE = AccountDataImportFields.ACCOUNTTYPE;
        public const string ISPROFITANDLOSS = AccountDataImportFields.ISPROFITANDLOSS;
        public const string BCCYCODE = "Base CCY Code";
        public const string BALANCEBCCY = "Balance in Base CCY";
        public const string RCCYCODE = "Reporting CCY Code";
        public const string BALANCERCCY = "Balance in Reporting CCY";

        public const short SUBLEDGERDATAIMPORTMANDATORYFIELDCOUNT = 10;

    }

    public struct AccountAttributeDataImportFields
    {
        public const string COMPANY = AccountDataImportFields.COMPANY;

        public const string GLACCOUNTNUMBER = AccountDataImportFields.GLACCOUNTNUMBER;
        public const string GLACCOUNTNAME = AccountDataImportFields.GLACCOUNTNAME;
        public const string FSCAPTION = AccountDataImportFields.FSCAPTION;
        public const string ACCOUNTTYPE = AccountDataImportFields.ACCOUNTTYPE;

        public const string RISKRATING = "Risk Rating";
        public const string RECONCILIATIONTEMPLATE = "Rec Form";
        public const string ISKEYACCOUNT = "Key Account";
        public const string ISZEROBALANCEACCOUNT = "Zero Balance";

        public const string SUBLEDGERSOURCE = "Subledger Source";
        public const string RECONCILIATIONPOLICY = "Policy";
        public const string NATUREOFACCOUNT = "Nature of Account";
        public const string RECONCILIATIONPROCEDURE = "Reconciliation Procedure";

        //Account ownership fields
        public const string PREPARER = "Preparer";
        public const string REVIEWER = "Reviewer";
        public const string APPROVER = "Approver";
        public const string BACKUPPREPARER = "Backup Preparer";
        public const string BACKUPREVIEWER = "Backup Reviewer";
        public const string BACKUPAPPROVER = "Backup Approver";

        public const string RECONCILABLE = "Reconcilable";
        public const string PREPARERDUEDAYS = "Preparer Due Days";
        public const string REVIEWERDUEDAYS = "Reviewer Due Days";
        public const string APPROVERDUEDAYS = "Approver Due Days";
        //public const string DAYTYPE = "Day Type";
    }
    public struct AccountAttributeDataImportTransitFields
    {
        public const string DATAIMPORTID = "DataImportID";
        public const string EXCELROWNUMBER = "ExcelRowNumber";
        public const string COMPANY = "Company";
        public const string GLACCOUNTNUMBER = "AccountNumber";
        public const string GLACCOUNTNAME = "AccountName";
        public const string FSCAPTION = "FSCaption";
        public const string ACCOUNTTYPE = "AccountType";

        public const string RISKRATING = "RiskRating";
        public const string RECONCILIATIONTEMPLATE = "RecFormTemplate";
        public const string ISKEYACCOUNT = "IsKeyAccount";
        public const string ISZEROBALANCEACCOUNT = "IsZeroBalance";

        public const string SUBLEDGERSOURCE = "SubledgerSource";
        public const string RECONCILIATIONPOLICY = "AccountPolicy";
        public const string NATUREOFACCOUNT = "NatureOfAccount";
        public const string RECONCILIATIONPROCEDURE = "ReconciliationProcedure";

        //public const string PREPARER = "PreparerEmailID";
        //public const string REVIEWER = "ReviewerEmailID";
        //public const string APPROVER = "ApproverEmailID";
        public const string PREPARER = "PreparerUniqueID";
        public const string REVIEWER = "ReviewerUniqueID";
        public const string APPROVER = "ApproverUniqueID";

        public const string BACKUPPREPARER = "BackupPreparerUniqueID";
        public const string BACKUPREVIEWER = "BackupReviewerUniqueID";
        public const string BACKUPAPPROVER = "BackupApproverUniqueID";

        public const string RECONCILABLE = "Reconcilable";

        public const string PREPARERDUEDAYS = "PreparerDueDays";
        public const string REVIEWERDUEDAYS = "ReviewerDueDays";
        public const string APPROVERDUEDAYS = "ApproverDueDays";
        //public const string DAYTYPE = "DayType";
    }

    public struct GLDataImportTransitFields
    {
        public const string DATAIMPORTID = "DataImportID";
        public const string EXCELROWNUMBER = "ExcelRowNumber";
        public const string PERIODENDDATE = "PeriodEndDate";
        public const string COMPANY = "Company";
        public const string GLACCOUNTNUMBER = "AccountNumber";
        public const string GLACCOUNTNAME = "AccountName";
        public const string FSCAPTION = "FSCaption";
        public const string ACCOUNTTYPE = "AccountType";
        public const string ISPROFITANDLOSS = "IsProfitAndLoss";
        public const string BCCYCODE = "BaseCurrencyCode";
        public const string BALANCEBCCY = "GLBalanceBaseCurrency";
        public const string RCCYCODE = "ReportingCurrencyCode";
        public const string BALANCERCCY = "GLBalanceReportingCurrency";

        public const short GLDATAIMPORTTRANSITFIELDCOUNT = 13;
        //public const string[] MANDATORYGLDATAIMPORTFIELDS = {"Period End Date", "Company", "GL Account #"
        //, "GL Account Name", "FS Caption", "Account Type"
        //, "Base CCY Code", "Balance in Base CCY"
        //, "Balance in Reporting CCY","Reporting CCY Code"};
    }

    public struct AddedGLDataImportFields
    {
        public const string DATAIMPORTID = "DataImportID";
        public const string EXCELROWNUMBER = "ExcelRowNumber";
        public const string RECPERIODENDDATE = "RecPeriodEndDate";

    }

    public struct AddedAccountAttributeImportFields
    {
        public const string DATAIMPORTID = "DataImportID";
        public const string EXCELROWNUMBER = "ExcelRowNumber";

    }

    public struct GLTBSDataImportFields
    {
        public const string COMPANY = AccountDataImportFields.COMPANY;
        public const string GLACCOUNTNUMBER = AccountDataImportFields.GLACCOUNTNUMBER;
        public const string GLACCOUNTNAME = AccountDataImportFields.GLACCOUNTNAME;
        public const string FSCAPTION = AccountDataImportFields.FSCAPTION;
        public const string ACCOUNTTYPE = AccountDataImportFields.ACCOUNTTYPE;
    }
    public struct AddedGLTBSAccountFields
    {
        //Harsh
        public const string MATCHING_SOURCE_DATAIMPORT_ID = "__MatchingSourceDataImportID";
        public const string EXCEL_ROW_NUMBER_COLLECTION = "__ExcelRowNumberCollection";
        public const string ACCOUNT_ID = "__AccountID";
        public const string RECORDS_IMPORTED = "__RecordsImported";
        public const string MATCHING_SOURCE_ACCOUNT_DATA = "__MatchingSourceAccountData";
    }
    public struct AddedGLTBSImportFields
    {
        public const string ISRECITEMCREATED = "__IsRecItemCreated";
        public const string EXCELROWNUMBER = "__ExcelRowNumber";


    }

    //STRUCTURE OF MATCHINGSOURCEDATATRANSIT SQL TABLE
    public struct MatchingSourceDataTransitFields
    {
        public const string MATCHING_SOURCE_DATAIMPORTID = "MatchingSourceDataImportID";
        public const string ACCOUNTID = "AccountID";
        public const string FS_CAPTION = "FSCaption";
        public const string ACCOUNT_TYPE = "AccountType";
        public const string ACCOUNT_NUMBER = "Account#";
        public const string KEY2 = "Key2";
        public const string KEY3 = "Key3";
        public const string KEY4 = "Key4";
        public const string KEY5 = "Key5";
        public const string KEY6 = "Key6";
        public const string KEY7 = "Key7";
        public const string KEY8 = "Key8";
        public const string KEY9 = "Key9";
        public const string KEY = "Key";
        public const string MATCHING_SOURCE_ACCOUNT_XML = "MatchingSourceAccountXML";
        public const string RECORDSIMPORTED = "RecordImported";
        public const string EXCEL_ROW_NUMBER_COLLECTION = "ExcelRowNumberCollection";
        public const string ISVALID = "IsValidRow";
        public const string ERROR_MESSAGE = "ErrorMessage";
    }

    public struct AddedFieldsForMatching
    {
        public const string COLUMN_ISFULLMATCH = "__IsFullMatch";
        public const string COLUMN_ISPARTIALMATCH = "__IsPartialMatch";
        public const string COLUMN_FULLMATCHPAIRID = "__FullMatchPairID";
        public const string COLUMN_PARTIALMATCHPAIRID = "__PartialMatchPairID";
    }

    public struct Pair
    {
        public const string COLUMN_PAIRID = "__PairID";
    }

    public struct AddedDisplayColumns
    {
        public const string COLUMN_PAIRID = "__PairID";
        public const string COLUMN_EXCELROWNUMBER = "__ExcelRowNumber";
        public const string COLUMN_MATCHSET_MATCHING_SOURCE_DATAIMPORTID = "__MatchSetMatchingSourceDataImportID";
        public const string COLUMN_SUBSET_NAME = "__DataSourceName";
        public const string COLUMN_REC_ITEM_NUMBER = "__RecItemNumber";
        public const string COLUMN_IS_AUTOMATIC_MATCH = "__IsAutomaticMatch";
        public const string COLUMN_MATCHING_SOURCE_DATAIMPORTID = "__MatchingSourceDataImportID";
    }
    public struct GeographyClassName
    {
        public const string KEY1 = "Company";
        public const int KEY1LABELID = 1059;
        public const string KEY2 = "Key2";
        public const int KEY2LABELID = 1060;
        public const string KEY3 = "Key3";
        public const int KEY3LABELID = 1061;
        public const string KEY4 = "Key4";
        public const int KEY4LABELID = 1062;
        public const string KEY5 = "Key5";
        public const int KEY5LABELID = 1063;
        public const string KEY6 = "Key6";
        public const int KEY6LABELID = 1064;
        public const string KEY7 = "Key7";
        public const int KEY7LABELID = 1065;
        public const string KEY8 = "Key8";
        public const int KEY8LABELID = 1066;
        public const string KEY9 = "Key9";
        public const int KEY9LABELID = 1067;
        public const string KEY10 = "Key10";
        public const int KEY10LABELID = 1068;

    }
}
