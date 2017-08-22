using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SkyStem.ART.Shared.Data
{
    public class SharedConstants
    {
        private SharedConstants()
        {
        }
        public const string HYPHEN = "-";
        public const int LENGTH_GENERATED_PASSWORD = 10;
    }

    public struct DecimalConstants
    {
        public const Int32 DECIMAL_PLACES_FOR_TEXTBOX = 2;
        public const Int32 DECIMAL_PLACES_FOR_MATH_ROUND = 2;
        public const Int32 DECIMAL_PLACES_FOR_EXCHANGE_RATE_ROUND = 4;
        public const decimal NULL_DECIMAL = Decimal.MinValue;
    }
    public struct FileExtensions
    {
        public const string xlsx = ".xlsx";
        public const string xls = ".xls";
        public const string csv = ".csv";
        public const string AllowedExtensions = "*.xlsx|*.xls|*.csv";
    }

    public struct DataImportTypeName
    {
        public const string HOLIDAYCALENDAR = "HolidayCalendar";
        public const string PERIODENDDATE = "PeriodEndDate";
        public const string GLDATA = "GLData";
        public const string SUBLEDGERSOURCE = "SubLedgerSource";
        public const string SUBLEDGERDATA = "SubLedgerData";
        public const string CURRENCYEXCHANGERATE = "CurrencyExchangeRate";
        public const string ACCOUNTATTRIBUTELIST = "AccountAttributeLIst";
        public const string COMPANYLOGO = "CompanyLogo";
        public const string GLTBS = "GLTBS";
        public const string NBF = "NBF";
        public const string MAPPINGUPLOAD = "AccountUpload";
        public const string MULTILINGUALUPLOAD = "MultilingualUpload";
        public const string USERUPLOAD = "UserUpload";
        public const string TASKIMPORT = "TaskImport";
    }
    public class FTPDataImportConstants
    {
        public struct FTPDataImportFolderName
        {
            public const string GL_DATA = "GLData";
            public const string SUBLEDGER_DATA = "SubledgerData";          
        }
    }

    public class MultilingualUploadConstants
    {
        private MultilingualUploadConstants()
        {
        }

        public const string SheetName = "DATA";
        public const string TemplateName = "MultilingualUpload";
        public const string TemplateExt = "xls";
        public const int MendatoryFieldCount = 3;
        public struct Fields
        {
            public const string LabelID = "Label ID";
            public const string FromLanguage = "From Language";
            public const string ToLanguage = "To Language";
        }
    }

    public class UserUploadConstants
    {
        private UserUploadConstants()
        {
        }
        public const string SheetName = "DATA";
        public struct AddedFields
        {
            public const string DATAIMPORTID = "DATAIMPORTID";
            public const string EXCELROWNUMBER = "EXCELROWNUMBER";
            public const string PASSWORD = "PASSWORD";
            public const string UNHASHED_PASSWORD = "UNHASHEDPASSWORD";
        }
        public struct UploadFields
        {
            public const string FIRSTNAME = "First Name";
            public const string LASTTNAME = "Last Name";
            public const string LOGINID = "Login ID";
            public const string EMAILID = "Email ID";
            public const string SYSADMIN = "System Admin";
            public const string PREPARER = "Preparer";
            public const string REVIEWER = "Reviewer";
            public const string APPROVER = "Approver";
            public const string BUSINESSADMIN = "Business Admin";
            public const string FINANCIALMANAGER = "Financial Manager";
            public const string ACCOUNTMANAGER = "Account Manager";
            public const string CONTROLLER = "Controller";
            public const string EXECUTIVE = "Executive";
            public const string CEOCFO = "CEO/CFO";
            public const string BACKUPPREPARER = "Backup Preparer";
            public const string BACKUPREVIEWER = "Backup Reviewer";
            public const string BACKUPAPPROVER = "Backup Approver";
            public const string AUDIT = "Audit";
            public const string TASKOWNER = "Task Owner";
            public const string USERADMIN = "User Admin";
            public const string DEFAULTROLE = "Default Role";

        }
        public struct UserUploadTransitFields
        {
            public const string DATAIMPORTID = "DATAIMPORTID";
            public const string EXCELROWNUMBER = "EXCELROWNUMBER";
            public const string FIRSTNAME = "FirstName";
            public const string LASTNAME = "LastName";
            public const string LOGINID = "LoginID";
            public const string EMAILID = "EmailID";
            public const string DEFAULTROLE = "DefaultRole";
            public const string SYSADMIN = "SystemAdmin";
            public const string PREPARER = "Preparer";
            public const string REVIEWER = "Reviewer";
            public const string APPROVER = "Approver";
            public const string BUSINESSADMIN = "BusinessAdmin";
            public const string FINANCIALMANAGER = "FinancialManager";
            public const string ACCOUNTMANAGER = "AccountManager";
            public const string CONTROLLER = "Controller";
            public const string EXECUTIVE = "Executive";
            public const string CEOCFO = "CEOCFO";
            public const string BACKUPPREPARER = "BackupPreparer";
            public const string BACKUPREVIEWER = "BackupReviewer";
            public const string BACKUPAPPROVER = "BackupApprover";
            public const string AUDIT = "Audit";
            public const string TASKOWNER = "TaskOwner";
            public const string USERADMIN = "UserAdmin";
            public const string PASSWORD = "PASSWORD";
            public const string UNHASHED_PASSWORD = "UNHASHED_PASSWORD";
        }
    }

    public class SharedAppSettingConstants
    {
        private SharedAppSettingConstants()
        {
        }
        public const string APPLICATION_ID = "ApplicationID";
        public const string DEFAULT_LANGUAGE_ID = "DefaultLanguageID";
        public const string DEFAULT_BUSINESS_ENTITY_ID = "DefaultBusinessEntityID";
        public const string APP_KEY_START_LABEL_ID = "StartLabelID";
        public const string SKYSTEMART_BASE_DATABASE_PATH = "SkyStemARTBaseDatabasePath";
        public const string IS_EMAIL_ID_UNIQUE_CHECK_REQUIRED = "IsEmailIDUniqueCheckRequired";
        public const string EMAIL_USE_TEST_ACCOUNT = "UseTestAccount";

        public const string BASE_FOLDER_FOR_FILES = "BaseFolderForFiles";
        public const string TEMP_FOLDER_FOR_EXPORT_FILES = "TempFolderForExportFiles";
        public const string BASE_FOLDER_FOR_FTP_UPLOAD = "BaseFolderForFTPUpload";
       
    }

    public class ConnectionStringConstants
    {
        public const string CONNECTION_STRING_SKYSTEMART = "connectionString";
        public const string CONNECTION_STRING_SKYSTEMART_CORE = "connectionStringCore";
        public const string CONNECTION_STRING_SKYSTEMART_BASE = "connectionStringBase";
        public const string CONNECTION_STRING_SKYSTEMART_SPECIFIC = "connectionStringSpecific";
        public const string CONNECTION_STRING_SKYSTEMART_CREATE_COMPANY = "connectionStringCreateCompany";
    }

    public class TaskUploadConstants
    {
        private TaskUploadConstants()
        {
        }
        public const string SheetName = "DATA";
        public const int MendatoryFieldCount = 13;
        public struct AddedFields
        {
            public const string DATAIMPORTID = "DATAIMPORTID";
            public const string EXCELROWNUMBER = "EXCELROWNUMBER";
            public const string ASSIGNEDTOUSERID = "AssignedToUserID";
            public const string REVIEWERUSERID = "ReviewerUserID";
            public const string APPROVERUSERID = "ApproverUserID";
            public const string NOTIFYUSERID = "NotifyUserID";
            public const string RECURRENCETYPEID = "RecurrenceTypeID";
            public const string ISVALIDROW = "IsValidRow";
            public const string RECURRENCEPERIODID = "RecurrencePeriodID";
            public const string TASKDUEDAYSBASISID = "TaskDueDaysBasisID";
            public const string ASSIGNEEDUEDAYSBASISID = "AssigneeDueDaysBasisID";
            public const string REVIEWERDUEDAYSBASISID = "ReviewerDueDaysBasisID";
            public const string DAYTYPEID = "DayTypeID";

        }
        public struct TaskUploadFields
        {
            public const string TASKLISTNAME = "Task List Name";
            public const string TASKSUBLISTNAME = "Task SubList Name";
            public const string TASKNAME = "Task Name";
            public const string CUSTOMFIELD1 = "Custom Field 1";
            public const string CUSTOMFIELD2 = "Custom Field 2";    
            public const string DESCRIPTION = "Description";
            public const string ASSIGNEDTO = "Assigned To";
            public const string REVIEWER = "Reviewer";            
            public const string APPROVER = "Approver";
            public const string NOTIFY = "Notify";
            public const string RECURRENCETYPE = "Recurrence Type";
            public const string RECURRENCEFREQUENCY = "Recurrence Frequency";
            public const string PERIODNUMBER = "Period #";
            public const string TASKDUEDATE = "Task Due Date";
            public const string REVIEWERDUEDATE = "Reviewer Due Date";           
            public const string ASSIGNEEDUEDATE = "Assignee Due Date";
            public const string ASSIGNEEDUEDAYS = "Assignee Due Days";         
            public const string TASKDUEDAYS = "Task Due Days";
            public const string TASKDUEDAYSBASIS = "Task Due Days Basis";
            public const string TASKDUEDAYSSKIPNUMBER = "Task Due Days Skip Number";
            public const string ASSIGNEEDUEDAYSBASIS = "Assignee Due Days Basis";
            public const string ASSIGNEEDUEDAYSSKIPNUMBER = "Assignee Due Days Skip Number";
            public const string REVIEWERDUEDAYS = "Reviewer Due Days";
            public const string REVIEWERDUEDAYSBASIS = "Reviewer Due Days Basis";
            public const string REVIEWERDUEDAYSSKIPNUMBER = "Reviewer Due Days Skip Number";
            public const string DAYTYPE = "Day Type";

        }

        public struct DataLength
        {
            public const int TaskListName = 100;
            public const int TaskSubListName = 100;
            public const int CustomField = 100;                   
            public const int RecurrenceType = 50;
            public const int UserLoginID = 250;
            public const int TaskAttributeValue = 500;           
        }
       
    }

    public class CurrencyExchangeUploadConstants
    {
        private CurrencyExchangeUploadConstants()
        {
        }
        public const string SheetName = "DATA";
        public const int MendatoryFieldCount = 4;
        public struct AddedFields
        {
            public const string DATAIMPORTID = "DATAIMPORTID";
            public const string EXCELROWNUMBER = "EXCELROWNUMBER";
            public const string COMPANYID = "COMPANYID";
            public const string ISVALIDROW = "IsValidRow";
            public const string ISDUPLICATE = "IsDuplicate";
            public const string ISERROR = "IsError";
        }
        public struct UploadFields
        {
            public const string PERIODENDDATE = "Period End Date";
            public const string FROMCURRENCYCODE = "From Currency Code";
            public const string TOCURRENCYCODE = "To Currency Code";
            public const string RATE = "Rate";
        }

        public struct TransitFields
        {
            public const string DATAIMPORTID = "DataImportID";
            public const string EXCELROWNUMBER = "ExcelRowNumber";
            public const string COMPANYID = "CompanyID";
            public const string PERIODENDDATE = "PeriodEndDate";
            public const string FROMCURRENCYCODE = "FromCurrencyCode";
            public const string TOCURRENCYCODE = "ToCurrencyCode";
            public const string RATE = "ExchangeRate";
        }

        public struct DataLength
        {
            public const int FROMCURRENCYCODE = 3;
            public const int TOCURRENCYCODE = 3;
        }
    }
    public class RecControlChecklistUploadConstants
    {
        private RecControlChecklistUploadConstants()
        {
        }
        public const string SheetName = "DATA";
        public const int MendatoryFieldCount = 1;
        public struct AddedFields
        {
            public const string DATAIMPORTID = "DATAIMPORTID";
            public const string EXCELROWNUMBER = "EXCELROWNUMBER";
            public const string ISVALIDROW = "IsValidRow";
            public const string ISDUPLICATE = "IsDuplicate";
        }
        public struct UploadFields
        {
            public const string DESCRIPTION = "Description";
        }

        public struct DataLength
        {
            public const int DESCRIPTION = 1000;
        }
    }

    public class ScheduleRecItemUploadConstants
    {
        private ScheduleRecItemUploadConstants()
        {
        }
        public const string SheetName = "DATA";
        public const int MendatoryFieldCount = 11;
        public struct AddedFields
        {
            public const string DATAIMPORTID = "DATAIMPORTID";
            public const string EXCELROWNUMBER = "EXCELROWNUMBER";
            public const string STARTINTERVALRECPERIODID = "StartIntervalRecPeriodID";
            public const string IGNOREINCALCULATION = "IgnoreInCalculation";
            public const string CALCULATIONFREQUENCYID = "CalculationFrequencyID";
            public const string EXRATELCCYTOBCCY = "ExRateLCCYToBCCY";
            public const string EXRATELCCYTORCCY = "ExRateLCCYToRCCY";
            public const string ISVALIDROW = "IsValidRow";
            public const string TotalInterval = "Total Interval";
            public const string CurrentInterval = "Current Interval";
        }
        public struct Fields
        {
            public const string RefNo = "Ref No";
            public const string ScheduleName = "Schedule Name";
            public const string Description = "Description";
            public const string OriginalAmount = "Original Amount";
            public const string LCCYCode = "L-CCY Code";
            public const string OpenDate = "Open Date";
            public const string BeginScheduleOn = "Begin Schedule On";
            public const string IncludeOnBeginDate = "Include On Begin Date";
            public const string ScheduleBeginDate = "Schedule Begin Date";
            public const string ScheduleEndDate = "Schedule End Date";
            public const string CalculationFrequency = "Calculation Frequency";
            //public const string TotalInterval = "Total Interval";
            //public const string CurrentInterval = "Current Interval";
        }
        public struct DataLength
        {
            public const int ScheduleName = 50;
            public const int LCCYCode = 3;
        }
    }

    public class SearchAndEmailConstants
    {
        public const string HeaderTableName = "RequestHdr";
        public const string DetailTableName = "RequestDetail";
        public struct HeaderFields
        {
            public const string FILENAME = "FileName";
        }
    }

    public class DownloadAllRecsConstants
    {
        public const string HeaderTableName = "RequestHdr";
        public struct HeaderFields
        {
            public const string FILENAME = "FileName";
            public const string RECPERIODID = "RecPeriodID";
            public const string LANGUAGEID = "LanguageID";
            public const string COMPANYID = "CompanyID";
            public const string DEFAULTLANGUAGEID = "DefaultLanguageID";
            public const string USERID = "UserID";
            public const string ROLEID = "RoleID";
            public const string ISDUALREVIEWENABLED = "IsDualReviewEnabled";
            public const string ISCERTIFICATIONENABLED = "IsCertificationEnabled";
            public const string ISMATERIALITYENABLED = "IsMaterialityEnabled";
            public const string CERTIFICATIONTYPEID = "CertificationTypeId";
            public const string PREPARERATTRIBUTEID = "PreparerAttributeId";
            public const string REVIEWERATTRIBUTEID = "ReviewerAttributeID";
            public const string APPROVERATTRIBUTEID = "ApproverAttributeID";
            public const string ISQUALITYSCOREENABLED = "IsQualityScoreEnabled";
            public const string ISREVIEWNOTESENABLED = "IsReviewNotesEnabled";
            public const string LANGUAGEINFO = "languageInfo";
        }

        public const string DetailTableName = "RequestDetail";
        public struct DetailFields
        {
            public const string GLDATAID = "GLDataID";
            public const string ACCOUNTID = "AccountID";
            public const string NETACCOUNTID = "NetAccountID";
            public const string RECONCILIATIONTEMPLATEID = "ReconciliationTemplateID";
            public const string ACCOUNTNAME = "AccountName";
        }
    }

    public class DataImportMessageConstants
    {
        private DataImportMessageConstants()
        {
        }

        public const string TableName = "MessageData";
        public struct Fields
        {
            public const string ImportFieldID = "ImportFieldID";
            public const string ImportField = "ImportField";
            public const string MessageLabelID = "MessageLabelID";
            public const string Message = "Message";
            public const string Allowed = "Allowed";
            public const string Actual = "Actual";
        }
        public struct Attributes
        {
            public const string IsLabelID = "IsLabelID";
            public const string IsVisible = "IsVisible";
            public const string HeaderLabelID = "HeaderLabelID";
            public const string LabelFieldName = "LabelFieldName";
        }
      
      
    }

    public class ExportToExcelReportConstants
    {
        public const string HeaderTableName = "RequestHdr";
        public struct HeaderFields
        {
            public const string FILENAME = "FileName";
            public const string RECPERIODID = "RecPeriodID";
            public const string LANGUAGEID = "LanguageID";
            public const string COMPANYID = "CompanyID";
            public const string DEFAULTLANGUAGEID = "DefaultLanguageID";
            public const string USERID = "UserID";
            public const string ROLEID = "RoleID";
        }
        public const string DetailTableName = "RequestDetail";
        public struct DetailFields
        {
            public const string RECPERIODID = "ReconciliationPeriodID";
            public const string FROMACCOUNTNUMBER = "FromAccountNumber";
            public const string TOACCOUNTNUMBER = "ToAccountNumber";
            public const string ISMATERIALACCOUNT = "IsMaterialAccount";
            public const string RISKRATINGIDS = "RiskRatingIDs";
            public const string ISKEYCCOUNT = "IsKeyccount";
            public const string FROMOPENDATE = "FromOpenDate";
            public const string TOOPENDATE = "ToOpenDate";
            public const string AGINGIDS = "AgingIDs";
            public const string OPENITEMCLASSIFICATIONIDS = "OpenItemClassificationIDs";
            public const string QUALITYSCORERANGE = "QualityScoreRange";
            public const string QUALITYSCOREIDS = "QualityScoreIDs";
            public const string FROMSYSTEMSCORE = "FromSystemScore";
            public const string FROMUSERSCORE = "FromUserScore";
            public const string TOSYSTEMSCORE = "ToSystemScore";
            public const string TOUSERSCORE = "ToUserScore";
        }
        public const string EntitySearchTableName = "EntitySearch";
    }
}
