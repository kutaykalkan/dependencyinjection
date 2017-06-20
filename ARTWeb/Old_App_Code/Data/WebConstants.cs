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
using SkyStem.ART.Client.Data;

namespace SkyStem.ART.Web.Data
{

    /// <summary>
    /// Summary description for WebConstants
    /// </summary>
    public class WebConstants
    {
        public WebConstants()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public const int LABEL_ID_YES = 1252;
        public const int LABEL_ID_NO = 1251;

        public const string ATTRIBUTE_LABEL_ID = "LabelID";

        public const string CULTURE_SPECIFIC_JS = "CultureSpecificJSFormats";
        public const decimal NULL_DECIMAL = Decimal.MinValue;
        public const string COMPANY_NOT_SPECIFIED = "Company ID Not Specified";
        public const string DATASOURCE_NOT_SPECIFIED = "DataSource Not Specified";
        public const string SELECT_ONE = "-2";
        public const string ART_TEMPLATE = "-1";
        public const string YES = "1";
        public const string NO = "2";
        public const string CREATE_NEW = "-1111";
        public const string SELECT_ALL = "0";
        public const string ACCOUNT_ENTITY_SEPARATOR = " - ";
        public const string ALL = "0";
        public const string HYPHEN = "-";
        public const string NOT_AVAILABLE = "N/A";
        public const string CURRENCY_LABEL_DEMO = "USD ";
        public const string FORMAT_BRACKET = "({0})";
        public const int INT_FOR_DECIMAL_VALUE_TEXTBOX = 3;
        public const int POPUP_WIDTH = 800;
        public const int POPUP_HEIGHT = 480;
        public const string RPT_PRM_YES = "1";
        public const string RPT_PRM_NO = "0";
        public const string RPT_PRM_ALL = "2";

        public const string ACCT_FLTR_YES = "True";
        public const string ACCT_FLTR_NO = "False";
        public const string ACCT_FLTR_ALL = "All";
        public const int MAX_FILE_NAME_LENGTH_FOR_ATTACHMENT = 150;
        public const int MAX_LENGTH_FOR_ATTRIBUTE_VALUE = 2000;

        //Error message to be shown to user when no selection is made on grid.
        public const int NO_SELECTION_ERROR_MESSAGE = 5000046;
        public const int CONFIRM_FOR_DELETE_DATAIMPORT_ITEM = 1934;
        //Error seperator in case of colective error message
        public const string MANDATORYFIELSDNAMESEPERATOR = ",";
        public const string ORGHIERARCHYVALUESEPERATOR = "^";
        //public const string RPTCRITERIAKEYVALUESEPERATOR = ",";
        //public const string ACCTFLRTKEYVALUESEPERATOR = ", ";
        public const string ACCT_FLRT_DISPLAY_VALUE_SEPERATOR = " - ";

        public const string MATCHING_SHEETNAME = "DATA";
        public const string GLDATA_SHEETNAME = "DATA";
        public const string ACCOUNTATTRIBUTE_SHEETNAME = "DATA";
        public const string SUBLEDGER_SHEETNAME = "DATA";
        public const int CONFIRM_FOR_CLOSE_RECPERIOD_AND_START_CERTIFICATION = 2217;
        public const int CONFIRM_FOR_RE_PROCESS_SRA = 2266;

        public const string DATA_TYPE_MAPPING = "DataTypeMapping";
        public const int CONFIRM_FOR_RE_OPEN = 2398;
        public const string MAPPINGUPLOAD_SHEETNAME = "DATA";
        public const string CURRENT_REC_PERIOD = "Current";
        public const string CURRENT_REC_PERIOD_INDEX = "-1";

        public const string USERUPLOAD_SHEETNAME = "DATA";
        public const int CONFIRM_FOR_RE_SET = 2670;
        public const int TASK_DUE_DAYS = 30;
        public const string SELECT_COLUMN = "-2";
        public const string USERNAMESEPERATOR = ", ";

        #region Show/Hide DataImport Rows
        public const string HIDE_SHOW_ROWS = "HideShowRows";
        #endregion

        #region JavascriptEventsConstants

        public const string ONCLICK = "onclick";
        public const string ONBLUR = "onblur";

        #endregion

        #region MasterPageControldID

        public const string LABEL_ERRORMESSAGE_ID = "lblErrorMessage";

        #endregion

        #region ControlProperties

        public const string VERTICALALIGN = "verticalAlign";
        public const string WIDTH = "Width";
        public const string VISIBILITY = "Visibility";

        #endregion

        #region ControlPropertyValue

        public const string VERTIVALALIGN_TOP = "top";
        public const string VISIBILITY_HIDDEN = "hidden";

        #endregion

        public const string REVISED_BY_FIELD_FOR_REVIEW_NOTE_DELETION = "RevisedByFieldForReviewNoteDeletion";

        public const int PRINT_POPUP_WIDTH = 800;
        public const int PRINT_POPUP_HEIGHT = 550;

        public const string CSS_CLASS_RECONCILED_ROW = "ReconciledRow";

    }

    public class RecFormButtonCommandName
    {
        public const string SAVE = "Save";
        public const string CANCEL = "Cancel";
        public const string SIGNOFF = "Signoff";
        public const string EDIT_REC = "EditRec";
        public const string APPROVE = "Approve";
        public const string DENY = "Deny";
        public const string ACCEPT = "Accept";
        public const string REJECT = "Reject";
        public const string REMOVE_SIGN_OFF = "RemoveSignOff";
    }

    public class VerbiagePlaceHolder
    {
        public const string USERNAME = "#Name#";
        public const string PERIODENDDATE = "#PeriodEndDate#";
        public const string ROLENAME = "#Role#";
    }

    public class URLConstants
    {

        public const string URL_TEMPLATEID_1 = "~/Pages/TemplateBankAccountForm.aspx";
        public const string URL_TEMPLATEID_2 = "~/Pages/TemplateDerivedCalculationForm.aspx";
        public const string URL_TEMPLATEID_3 = "~/Pages/TemplateAccruableItemsForm.aspx";
        public const string URL_TEMPLATEID_4 = "~/Pages/TemplateAmortizableAccountsForm.aspx";
        public const string URL_TEMPLATEID_5 = "~/Pages/TemplateItemizedListForm.aspx";
        public const string URL_TEMPLATEID_6 = "~/Pages/TemplateSubledgerForm.aspx";

        public const string URL_ITEMINPUT_BANKFEE = "~/Pages/GLAdjustments.aspx";
        public const string URL_ITEMINPUT_AMORTIZABLE = "~/Pages/ItemInputAmortizableTemplate.aspx";
        public const string URL_ITEMINPUT_ACCRUABLE = "~/Pages/ItemInputAccurable.aspx";
        public const string URL_ITEMINPUT_ACCRUABE_RECURRING = "~/Pages/ItemInputAccurableRecurring.aspx";
        public const string URL_ITEMINPUT_WRITEOFF = "~/Pages/ItemInputWriteOff.aspx";
        public const string URL_ITEMINPUT_UNEXPLAINEDVARIANCE = "~/Pages/UnexplainedVariance.aspx";
        public const string URL_ITEMINPUT_UNEXPLAINEDVARIANCE_HISTORY = "~/Pages/UnExplainedVarianceHistory.aspx";
        public const string URL_ITEMINPUT_DOCUMENT = "~/Pages/NotAvailable.aspx";
        public const string URL_ITEMINPUT_COMMENT = "~/Pages/ReviewNotes.aspx";
        public const string URL_AUDIT_TRAIL = "~/Pages/AuditTrail.aspx";
        public const string URL_RECONCILIATION_ARCHIVES = "~/Pages/ReconciliationArchives.aspx";

        public const string URL_IMPORT_RECURRING = "~/Pages/RecurringImportUII.aspx";
        public const string URL_IMPORT_BANKFEE = "~/Pages/OSDepositBankNsfImport.aspx";
        public const string URL_IMPORT_RECCONTROLCHECKLIST = "~/Pages/RecControlCheckListImport.aspx";

        public const string URL_CERTIFICATION_HOME = "~/Pages/CertificationHome.aspx";
        public const string URL_MATCHING_WIZARD = "~/Pages/Matching/MatchingWizard.aspx";
        public const string URL_MATCHING_RESULT = "~/Pages/Matching/MatchingResults.aspx";
        public const string URL_MATCHING_RESULT_CREATE_REC_ITEMS = "~/Pages/Matching/CreateRecItem.aspx";
        public const string URL_MATCHING_SOURCE_DATAIMPORT = "~/Pages/Matching/MatchSourceDataImport.aspx";
        public const string URL_MATCHING_SOURCE_DATAIMPORT_STATUS = "~/Pages/Matching/MatchingSourceDataImportStatus.aspx";
        public const string URL_MATCHING_VIEW_MATCH_SET = "~/Pages/Matching/ViewMatchSet.aspx";
        public const string URL_MATCHSET_STATUS_MESSAGES = "~/Pages/Matching/MatchsetStatusMessages.aspx";
        public const string URL_MATCHING_RESULT_CLOSE_REC_ITEMS = "~/Pages/Matching/CloseRecItem.aspx";

        public const string URL_IMAGE = "~/App_Themes/SkyStemBlueBrown/Images/";
        public const string URL_IMAGE_DELETE = URL_IMAGE + "Delete.gif";
        public const string URL_IMAGE_CANCEL = URL_IMAGE + "Cancel.gif";
        public const string URL_IMAGE_EDIT = URL_IMAGE + "Edit.gif";
        public const string URL_IMAGE_UPDATE = URL_IMAGE + "Save.gif";
        public const string URL_IMAGE_ATTACHDOC = URL_IMAGE + "FileIcon.gif";
        public const string URL_IMAGE_SCHEDULE = URL_IMAGE + "FileIcon.gif";
        public const string URL_IMAGE_COPY = URL_IMAGE + "Copy.gif";
        public const string URL_IMAGE_COPY_AND_CLOSE = URL_IMAGE + "CopyAndClose.gif";

        public const string URL_TASK_VIEWER = "~/Pages/TaskMaster/TaskViewer.aspx";
        public const string URL_LOGOUT = "~/Logout.aspx";
        public const string URL_CHANGE_PASSWORD = "~/Pages/ChangePassword.aspx";
        public const string CREATE_TASK_URL = "~/Pages/TaskMaster/CreateTask.aspx";
        public const string EDIT_TASK_LIST_URL = "~/Pages/TaskMaster/EditTaskList.aspx";
        public const string EDIT_TASK_SUB_LIST_URL = "~/Pages/TaskMaster/EditTaskList.aspx";


        public const string IMPORT_GENERAL_TASK_URL = "~/Pages/TaskMaster/GeneralTaskImport.aspx";
        public const string IMPORT_ACCOUNT_TASK_URL = "~/Pages/TaskMaster/AccountTaskImport.aspx";

        public const string URL_BULK_COMPLETE_TASK = "~/Pages/TaskMaster/BulkCompleteTasks.aspx";
        //public const string URL_DOWNLOAD_ATTACHMENT = "~/Pages/DownloadAttachment.aspx";
        public const string URL_RECFREQUENCY = "~/Pages/PopupRecFrequency.aspx";
        public const string URL_TASK_ATTACHMENT = "~/Pages/TaskMaster/TaskAttachment.aspx";
        public const string URL_TASK_VIEW_COMMENTS = "~/Pages/TaskMaster/ViewTaskComments.aspx";
        public const string URL_TASK_FILTER_ICON = "<img src='../../App_Themes/SkyStemBlueBrown/Images/FilterIcon.gif' border='0' />";
        public const string URL_USER_LOCKDOWN_DETAIL = "~/Pages/PopupUserLockdownDetail.aspx";
    }

    public class QueryStringConstants
    {
        public const string CERTIFICATION_TYPE_ID = "certtp";
        public const string PAGE_TITLE_ID = "pgttlid";
        public const string EXPANDED = "ex";
        public const string GENERIC_ID = "id";
        public const string CONFIRMATION_MESSAGE_LABEL_ID = "msg";
        public const string IS_REDIRECTED_FROM_STATUSPAGE = "IsRedirectedFromStatusPage";
        public const string CONFIRMATION_MESSAGE_FROM_STATUSPAGE = "ConfirmMessageFromStatusPage";
        public const string CONFIRMATION_MESSAGE_ENTITY_ID = "ent";
        public const string ERROR_MESSAGE_LABEL_ID = "err";
        public const string ERROR_MESSAGE_SYSTEM = "sys";
        public const string COMPANY_ID = "cmp";
        public const string User_ID = "Ur_ID";
        public const string ACCOUNT_ID = "Acct_ID";
        public const string ACCOUNT_INFO = "Acct_Info";
        public const string EMAIL_INFO_SPECIFIC = "Email_Info_Specific";
        public const string NETACCOUNT_ID = "NetAcct_ID";
        public const string RISKRATING_ID = "RiskRating_ID";
        public const string GLDATA_ID = "GLData_ID";
        public const string ROLE_ID = "Role_ID";
        public const string FROM_PAGE = "pg";
        public const string SHOW_SEARCH_RESULTS = "sch";
        public const string REPORT_ID = "Report_ID";
        public const string REPORT_SECTION_ID = "ReportSection_ID";
        public const string IS_REPORT_ACTIVITY = "IsReportActivity";
        public const string DATA_IMPORT_ID = "dtimpid";
        public const string DATA_IMPORT_TYPE_ID = "dtimptypeid";
        public const string IMPORT_TEMPLATE_ID = "importtemplateid";
        public const string Reconciliation_TemplateID = "templateID";
        public const string SHOW_USER_CREATED = "createuser";
        public const string PREVIOUS_PAGE_FROM_QUERYSTRING = "Prv_Page";
        public const string RECORD_ID = "RecordID";
        public const string RECORD_TYPE_ID = "RecordTypeID";
        public const string REC_CATEGORY_TYPE_ID = "RecCategoryTypeID";
        public const string MODE = "Mode";
        public const string INSERT = "Insert";
        public const string READ_ONLY = "ReadOnly";
        public const string EDIT = "Edit";
        public const string GL_RECONCILIATION_ITEM_INPUT_ID = "GLReconciliationItemInputID";
        public const string GL_RECONCILIATION_ITEM_ID = "GLReconciliationItemID";
        public const string IS_FORWARDED_ITEM = "IsForwardedItem";
        public const string GRID_TYPE = "gt";
        public const string REVIEW_NOTE_ID = "rvn";
        public const string REC_STATUS_ID = "recst";
        public const string SUCESS = "SUCESS";
        public const string FAILURE = "FAILURE";
        public const string FROMPAGE = "FROMPAGE";
        public const string FROMPOPUP = "FROMPOPUP";
        public const string ITEM_COUNT_REQUIRED = "ItemCountRequired";
        public const string Login_ID = "Login_ID";
        public const string FILE_PATH = "flpth";
        public const string DOWNLOAD_MODE = "downloadMode";
        public const string REC_PERIOD_CONTAINER_ID = "RecPeriodContainerID";
        public const string REC_PERIOD_ID_COLLECTION = "RecPeriodIDCollection";
        public const string HL_REC_FREQUENCY_ID = "hlRecFrequencyID";
        public const string IS_SRA = "IsSRA";
        public const string REPORT_ARCHIVE_TYPE = "ReportArchiveType";
        public const string REPORT_ARCHIVEID = "ReportArchiveID";
        public const string MANDATORY_REPORT_ID = "MandatoryReportID";
        //public const string IS_MANDATORY_REPORT_SIGNEDOFF_DONE = "IsMandatoryReportSignedoffDone";
        public const string IS_REPORT = "rpt";
        public const string MY_REPORT_ID = "ReportMyReport";
        public const string REPORT_TYPE = "ReportType";
        public const string REFERRER_PAGE_ID = "ReferrerPageID";
        public const string REC_CATEGORY_ID = "RecCategoryID";
        public const string REC_PERIODC_END_DATE = "dt";
        public const string SHOW_COMMENTS = "scmt";
        public const string COMMENTS = "cmt";
        public const string REPORT_SESSIONCLEAR = "RptSClr";
        public const string CLEAR_FILTER = "ClearFilter";
        public const string IS_SUMMARY = "IsSummary";
        public const string POSTBACK_CONTROL_ID = "pbcid";
        public const string REFFERAL_PAGE_AC_SRA = "IsSRAAcc";
        public const string POSTBACK_PACKAGEID = "PackageId";
        public const string POSTBACK_ISCUSTOMIZED_PACKAGE = "IsCustomerMizedPackage";
        public const string REC_PERIOD_ID = "RecPeriodID";
        public const string REC_TEMPLATE_ID = "RecTemplateID";

        public const string PARENT_HIDDEN_FIELD = "parentHiddenField";
        public const string IMPORT_SOURCE_PAGE_SECTION_LABEL_ID = "ImportSourceSectionLabelID";

        public const string MATCHING_SOURCE_NAME = "matchingSourceName";
        public const string MATCHING_SOURCE_DATA_IMPORT_ID = "matchingSourceDataImportID";
        public const string MATCHING_SOURCE_TYPE_ID = "matchingSourceTypeID";
        public const string MATCH_SET_ID = "matchSetID";
        public const string MATCHING_STATUS_ID = "matchingStatusID";
        public const string MATCHING_TYPE_ID = "matchingTypeID";
        public const string ADDEDBY_USER_ID = "addedByUserID";
        public const string MATCHSET_SUBSET_COMBINATION_ID = "matchSetSubsetCombinationID";
        public const string MATCH_SET_RESULT_COLUMN_NAME_PAIR_ID = "MatchSetResultColumnNamePairID";
        public const string MATCHING_RESULT_TYPE = "MatchingResultType";
        public const string IS_FROM_WORK_SPACE = "IsFromWorkSpace";
        public const string GRID_DYNAMIC_FILTER_SESSION_KEY = "griddynamicfiltersessionkey";
        public const string EXCEL_ROW_NUMBER = "ExcelRowNumber";
        public const string MATCH_SET_MATCHING_SOURCE_DATA_IMPORT_ID = "MatchSetMatchingSourceDataImportId";

        public const string EXCHANGE_RATE_LCCY_TO_BCCY = "ExRateLccyToBccy";
        public const string EXCHANGE_RATE_LCCY_TO_RCCY = "ExRateLccyToRccy";
        public const string IS_FOR_POPUP = "IsForPopup";
        public const string PAGE_ID = "PageID";
        public const string CURRENT_BCCY = "CurrentBccy";
        public const string SESSION_ID = "SESSION_ID";
        public const string FILE_NAME = "FILE_NAME";
        public const string TASK_DETAIL_REVIEW_NOTE_ID = "taskDetailReviewNoteID";
        public const string TASK_ID = "taskID";
        public const string TASK_DETAIL_ID = "taskDetailID";
        public const string TASK_TYPE_ID = "taskTypeID";
        public const string TASK_ACTION_TYPE_ID = "taskActionTypeID";
        public const string TASK_LIST_ID = "tasklistID";
        public const string TASK_LIST_NAME = "taskListName";
        public const string TASK_SUB_LIST_ID = "tasksublistID";
        public const string TASK_SUB_LIST_NAME = "tasksubListName";
        public const string SELECTED_USER_ID = "selectedUserID";
        public const string ACTIVE_TAB_INDEX = "ActiveTabIndex";
        public const string TASK_COMPLETION_STATUS_ID = "TaskCompletionStatusID";
        public const string TASK_RECURRENCE_TYPE = "TaskRecurrenceType";
        public const string IS_REDIRECTED_FROM_RECURRING_IMPORT = "IsRedirectedFromRecurringImport";
        public const string IS_REDIRECTED_FROM_TASK_IMOPRT = "IsRedirectedFromTaskImoprt";
        public const string REC_ITEM_NUMBER = "RecItemNumber";
        public const string HANDLER_ACTION = "HandlerAction";
        public const string GLDATA_REC_CONTROL_CHECKLIST_ID = "GLDataRecControlCheckListID";
        public const string REC_CONTROL_CHECK_LIST_ID = "RecControlCheckListID";
        public const string TASK_USER_ROLE_ID = "TaskUserRoleID";
        public const string TASK_LIST_LEVEL = "TasklistLevel";
        public const string LOGOUT_MESSAGE = "SessionExpired";

        public const string REQUEST_ID = "requestID";
        public const string REQUEST_TYPE_ID = "requestTypeID";
    }

    public class SessionConstants
    {
        public const string SESSION_KEY_PREFIX = ARTConstants.APP_NAME + "SessionKey_";
        // Use prefix SESSION_KEY_MASTER_DATA_PREFIX if data should be cleaned when langauge is changed by user
        public const string SESSION_KEY_MASTER_DATA_PREFIX = SESSION_KEY_PREFIX + "MasterData_";
        public const string ALL_ROLES = SESSION_KEY_PREFIX + "AllRoles";

        public const string CURRENT_COMPANY_HDR_INFO = "CurrentCompanyHdrInfo";


        public const string ALL_COMPANIES_LITE_OBJECT = SESSION_KEY_MASTER_DATA_PREFIX + "AllCompaniesLiteObject";
        public const string BUSINESS_ENTITY_ID = "BusinessEntityID";
        public const string CURRENT_COMPANY_NAME = "CurrentCompanyName";
        public const string CURRENT_LANGUAGE = "CurrentLanguage";

        public const string ROLES_LIST = SESSION_KEY_PREFIX + "RolesList";
        public const string USER_MENUS = SESSION_KEY_PREFIX + "UserMenus";
        public const string USER_INFO = SESSION_KEY_PREFIX + "UserInfo";
        public const string USER_ROLE = SESSION_KEY_MASTER_DATA_PREFIX + "UserRole";
        public const string CURRENT_ROLE_ID = SESSION_KEY_PREFIX + "CurrentRoleID";
        public const string CURRENT_COMPANY_ID = SESSION_KEY_PREFIX + "CurrentCompanyID";
        public const string CURRENT_COMPANY_DATABASE_EXISTS = SESSION_KEY_PREFIX + "CurrentCompanyDatabaseExists";
        public const string CURRENT_USER_ID = SESSION_KEY_PREFIX + "CurrentUserID";

        public const string CURRENT_FINANCIAL_YEAR_ID = SESSION_KEY_PREFIX + "CurrentFinancialYearID";
        public const string CURRENT_COMPANY_SETTINGS = ARTConstants.CACHE_KEY_PREFIX + "CurrentCompanySettings";

        //public const string CURRENT_RECONCILIATION_PREIOD_ID = SESSION_KEY_PREFIX + "CurrentReconciliationPeriodID";
        //public const string CURRENT_RECONCILIATION_PREIOD_END_DATE = SESSION_KEY_PREFIX + "CurrentReconciliationPeriodEndDate";
        public const string CURRENT_RECONCILIATION_PREIOD_INFO = SESSION_KEY_PREFIX + "CurrentReconciliationPeriodInfo";
        public const string CURRENT_CAPABILITY_COLLECTION = SESSION_KEY_PREFIX + "CurrentCompanyCapabilityCollection";
        public const string ALL_CAPABILITY_LIST = SESSION_KEY_MASTER_DATA_PREFIX + "AllCapabilityList";
        //public const string CAPABILITY_COLLECTION = SESSION_KEY_PREFIX + "CompanyCapabilityCollection";
        public const string REC_PERIOD_CAPABILITY_COLLECTION = SESSION_KEY_PREFIX + "RecPeriodCapabilityCollection";

        public const string ALL_MATERIALITYTYPE_LIST = SESSION_KEY_MASTER_DATA_PREFIX + "MaterialityTypeList";
        public const string ALL_RISKRATING_LIST = SESSION_KEY_MASTER_DATA_PREFIX + "RiskRatingList";
        public const string ALL_RECONCILIATIONFREQUENCY_LIST = SESSION_KEY_MASTER_DATA_PREFIX + "ReconciliationFrequencyList";
        public const string ALL_DATAIMPORTTYPE_LIST = SESSION_KEY_MASTER_DATA_PREFIX + "DataImportTypeList";
        public const string ALL_WEEKDAYS_LIST = SESSION_KEY_MASTER_DATA_PREFIX + "WeekDaysList";
        public const string ALL_ROLE_CONFIGURATIONS = SESSION_KEY_MASTER_DATA_PREFIX + "RoleConfigList";
        public const string ALL_MAPPINGUPLOADKEY_LIST = SESSION_KEY_MASTER_DATA_PREFIX + "MappingUploadkeyList";
        public const string ALL_DUALLEVELREVIEWTYPE_LIST = SESSION_KEY_MASTER_DATA_PREFIX + "DualLevelReviewTypeList";
        public const string ALL_DUE_DAYS_BASIS_LIST = SESSION_KEY_MASTER_DATA_PREFIX + "DueDaysBasisList";
        public const string ALL_COMPANY_ALERT_LIST = SESSION_KEY_MASTER_DATA_PREFIX + "CompanyAlertList";
        public const string ALL_RCCL_VALIDATION_TYPE_LIST = SESSION_KEY_MASTER_DATA_PREFIX + "AllRCCLValidationTypeList";
        public const string ALL_DAYS_TYPE_LIST = SESSION_KEY_MASTER_DATA_PREFIX + "DaysTypeList";
        public const string ALL_TASK_CUSTOM_FIELD_INFO_LIST = SESSION_KEY_MASTER_DATA_PREFIX + "AllTaskCustomFieldInfo";

        //TODO: to be removed when connected to database.
        public const string TEMPGLDATAUNEXPECTEDVARIANCE_LIST = SESSION_KEY_PREFIX + "UnexplainedVarianceInfoCollection";
        public const string TEMPGLDATAWRITEOFF_LIST = SESSION_KEY_PREFIX + "WriteOffInfoCollection";
        public const string ACCOUNTENTITY_DISPLAY = SESSION_KEY_PREFIX + "AccountEntityToDisplay";
        public const string ORGANIZATIONAL_HIERARCHY_KEYS = SESSION_KEY_MASTER_DATA_PREFIX + "OrganizationalHierarchyKeys";
        public const string TEMPREVIEW_LIST = SESSION_KEY_PREFIX + "ReviewNotesInfoCollection";
        public const string REC_PERIOD_STATUS_INFO = SESSION_KEY_PREFIX + "RecPeriodStatus";
        //public const string REC_PERIOD_STATUS_ENUM = SESSION_KEY_PREFIX + "RecProcessStatusEnum";
        //public const string REC_PROCESS_STATUS_LABEL_ID = SESSION_KEY_PREFIX + "RecProcessStatusLabelID";
        public const string ALL_ACCOUNT_TYPE_MST_INFO = SESSION_KEY_MASTER_DATA_PREFIX + "AllAccountTypeMstInfo";
        public const string ALL_RECONCILIATION_TEMPLATE_MST_INFO = SESSION_KEY_MASTER_DATA_PREFIX + "AllReconciliationTemplateMstInfo";
        public const string ALL_RISK_RATING_MST_INFO = SESSION_KEY_MASTER_DATA_PREFIX + "AllRiskRatingMstInfo";
        public const string ALL_PREPARER_NAME_AND_ID = SESSION_KEY_PREFIX + "AllPreparerNameandId";
        public const string ALL_REVIEWER_NAME_AND_ID = SESSION_KEY_PREFIX + "AllReviewerNameandId";
        public const string ALL_APPROVER_NAME_AND_ID = SESSION_KEY_PREFIX + "AllApproverNameandId";
        public const string ALL_GEOGRAPHY_STRUCTURE_BY_COMPANYID = SESSION_KEY_PREFIX + "AllGeographyStructureByCompanyID";
        public const string ACCOUNT_SEARCH_CRITERIA = SESSION_KEY_PREFIX + "AccountSearchCriteria";
        //public const string User_Search_Result = SESSION_KEY_PREFIX + "UserSearchResult";
        public const string USER_ACCOUNT_ASSOCIATION = SESSION_KEY_PREFIX + "UserAccountAssociation";
        public const string SEARCH_ACCOUNT_ASSOCIATION = SESSION_KEY_PREFIX + "SearchAccountAssociation";
        public const string USER_INSERUPDATE_ACCOUNTASSOCIATION = SESSION_KEY_PREFIX + "UserInsertUpdateAccountAssociation";

        public const string NET_ACCOUNT_ASSOCIATION = SESSION_KEY_PREFIX + "NetAccountAssociation";
        public const string NET_ACCOUNT_LIST = SESSION_KEY_PREFIX + "NetAccountList";
        //public const string DELTE_NETACCOUNT_ASSOCIATION = SESSION_KEY_PREFIX + "DeleteNetAccountAssociation";

        public const string BASE_CURRENCY_CODE = SESSION_KEY_PREFIX + "BaseCurrencyCode";
        public const string REPORTING_CURRENCY_CODE = SESSION_KEY_PREFIX + "ReportingCurrencyCode";
        public const string SEARCH_USER_PARAMATERES = SESSION_KEY_PREFIX + "SearchUserParameters";
        public const string SEARCH_USER_PARAMATERES_ROLE = SESSION_KEY_PREFIX + "SearchUserParametersRole";
        public const string USER_COLLECTION = SESSION_KEY_PREFIX + "UserCollection";
        public const string GEOGRAPHY_STRUCTURE_COLLECTION = SESSION_KEY_MASTER_DATA_PREFIX + "GeographyStructure";

        //public const string GLDATA_UPLOADFILE_PHYSICALPATH = SESSION_KEY_PREFIX + "GLDataUploadFilePhysicalPath";
        //public const string GLDATA_IMPORTNAME = SESSION_KEY_PREFIX + "DataImportName";
        public const string GLDATA_INFO = SESSION_KEY_PREFIX + "GLDataImportInfo";

        public const string ATTACHMENTS = SESSION_KEY_PREFIX + "Attachments";


        public const string STANDARD_REPORT_LIST_BY_ROLE = SESSION_KEY_PREFIX + "StandardReportListByRole";

        public const string DASHBOARDS_BY_ROLE = SESSION_KEY_PREFIX + "DashbaordsByRole";
        public const string EXCEPTION_TYPES = SESSION_KEY_MASTER_DATA_PREFIX + "ExceptionTypes";
        public const string AGING_TYPES = SESSION_KEY_MASTER_DATA_PREFIX + "AgingTypes";
        public const string OPENITEMCLASSIFICATION_TYPES = SESSION_KEY_MASTER_DATA_PREFIX + "OpenItemClassificationsTypes";
        public const string USER_ROLES = SESSION_KEY_MASTER_DATA_PREFIX + "UserRoles";
        public const string REASON_TYPES = SESSION_KEY_MASTER_DATA_PREFIX + "ReasonTypes";
        public const string RECSTATUS_TYPES = SESSION_KEY_MASTER_DATA_PREFIX + "RecStatusTypes";
        public const string RISKRATING_TYPES = SESSION_KEY_PREFIX + "RiskRatingTypes";
        public const string QUALITY_SCORE_STATUSES = SESSION_KEY_MASTER_DATA_PREFIX + "QualityScoreStatuses";
        public const string SYSTEM_LOCKDOWN_REASONS = SESSION_KEY_MASTER_DATA_PREFIX + "SystemLockdownReasons";
        public const string REPORT_COLUMNS = SESSION_KEY_MASTER_DATA_PREFIX + "ReportColumns";
        //Report Session Constants"
        public const string REPORT_INFO_OBJECT = SESSION_KEY_PREFIX + "ReportInfoObject";
        public const string REPORT_CRITERIA = SESSION_KEY_PREFIX + "ReportCriteria";
        public const string REPORT_CREATE_DATETIME = SESSION_KEY_PREFIX + "ReportCreateTime";
        public const string REPORT_DATA = SESSION_KEY_PREFIX + "ReportData";
        public const string USER_MY_REPORT_SAVED_REPORT_NAME = SESSION_KEY_PREFIX + "UserMyReportSavedReportName";
        public const string REPORT_DATA_ACCOUNT_OWNERSHIP = SESSION_KEY_PREFIX + "ReportDataAccountOwnership";
        public const string REPORT_DATA_ACCOUNT_STATUS = SESSION_KEY_PREFIX + "ReportDataAccountStatus";
        public const string REPORT_DATA_CERTIFICATION_TRACKING = SESSION_KEY_PREFIX + "ReportDataCertificationTracking";
        public const string REPORT_DATA_DELINQUENT_ACCOUNT = SESSION_KEY_PREFIX + "ReportDataDelinquentAccount";
        public const string REPORT_DATA_EXCEPTION_STATUS = SESSION_KEY_PREFIX + "ReportDataExceptionStatus";
        public const string REPORT_DATA_INCOMPLETE_ACCOUNT_ATTRIBUTE = SESSION_KEY_PREFIX + "ReportDataIncompleteAccountAttribute";
        public const string REPORT_DATA_OPEN_ITEM = SESSION_KEY_PREFIX + "ReportDataOpenItems";
        public const string REPORT_DATA_REC_STATUS_COUNT = SESSION_KEY_PREFIX + "ReportDataReconciliationStatusCount";
        public const string REPORT_DATA_UNASSIGNED_ACCOUNT = SESSION_KEY_PREFIX + "ReportDataUnassignedAccounts";
        public const string REPORT_DATA_UNUSUAL_BALANCE = SESSION_KEY_PREFIX + "ReportDataUnusualBalances";
        public const string REPORT_DATA_QUALITYSCORE_ITEM = SESSION_KEY_PREFIX + "ReportDataQualityScore";
        public const string REPORT_DATA_QUALITYSCORE_ITEM_PRINT = SESSION_KEY_PREFIX + "ReportDataQualityScoreForPrint";
        public const string REPORT_DATA_REVIEW_NOTES = SESSION_KEY_PREFIX + "ReportDataReviewNotes";
        public const string REPORT_DATA_COMPLETION_DATE = SESSION_KEY_PREFIX + "ReportDataCompletionDate";
        public const string REPORT_DATA_NEW_ACCOUNT = SESSION_KEY_PREFIX + "ReportDataNewAccount";
        public const string REPORT_DATA_ACCOUNT_ATTRIBUTE_CHANGE = SESSION_KEY_PREFIX + "ReportDataAccountAttributeChange";
        public const string REPORT_DATA_TASK_COMPLETION_REPORT = SESSION_KEY_PREFIX + "ReportDataTaskCompletionReport";

        public const string REPORT_ARCHIVED_DATA = SESSION_KEY_PREFIX + "ReportArchivedData";
        public const string REPORT_ARCHIVED_INFO_OBJECT = SESSION_KEY_PREFIX + "ReportArchivedInfoObject";

        public const string CERTIFICATION_BALANCES_DATA = SESSION_KEY_PREFIX + "CertificationBalancesData";
        public const string CERTIFICATION_EXCEPTION_DATA = SESSION_KEY_PREFIX + "CertificationExceptionData";

        public const string CERTIFICATION_STATUS_TYPE = SESSION_KEY_MASTER_DATA_PREFIX + "CertificationStatusType";
        public const string REC_FORM_PRINT_PAGE_URL = SESSION_KEY_PREFIX + "RecFormPrintpageUrl";
        public const string ACCOUNT_CERTIFICATION_STATUS_INFO_OBJECT = SESSION_KEY_PREFIX + "AccountCertificationStatusInfo";

        public const string CURRENT_GLDATA_ITEM_SCHEDULE_INFO = SESSION_KEY_PREFIX + "CurrentGLDataItemScheduleInfo";
        public const string CURRENT_GLDATA_ITEM_SCHEDULE_INTERVAL_DETAIL_INFO = SESSION_KEY_PREFIX + "CurrentGLDataItemScheduleIntervalDetailInfo";

        public const string RANGEOFSCORES_TYPES = SESSION_KEY_MASTER_DATA_PREFIX + "RangeOfScoreTypes";
        public const string QUALITYSCORECHECKLIST_TYPES = SESSION_KEY_MASTER_DATA_PREFIX + "QualityScoreChecklistTypes";
        public const string EXPORT_TO_EXCEL_DATA_TABLE = SESSION_KEY_PREFIX + "ExportToExcelDataTable";
        public const string PAGE_SETTINGS = SESSION_KEY_PREFIX + "PageSettings";
        public const string TASK_COMPLETION_STATUS_ID = SESSION_KEY_PREFIX + "TaskCompletionStatusID";
        public const string COMPANY_SUBLEDGER_SOURCES = SESSION_KEY_PREFIX + "CompanySubledgerSources";


        #region "Search Results Constants"
        public const string SEARCH_RESULTS_PREFIX = SESSION_KEY_PREFIX + "SearchResults_";
        public const string SEARCH_RESULTS_DATA_IMPORT_COMPANY = SEARCH_RESULTS_PREFIX + "DataImportByCompany";
        public const string SEARCH_RESULTS_DATA_IMPORT_REC_PERIOD = SEARCH_RESULTS_PREFIX + "DataImportByRecPeriod";
        public const string SEARCH_RESULTS_ACCOUNT_VIEWER = SEARCH_RESULTS_PREFIX + "AccountViewer";
        public const string SEARCH_RESULTS_ACCOUNT = SEARCH_RESULTS_PREFIX + "AccountSearchResult";
        public const string SEARCH_RESULTS_REVIEW_NOTES = SEARCH_RESULTS_PREFIX + "ReviewNotesByGLDataID";
        public const string SEARCH_RESULTS_NETACCOUNT_ASSOCIATION = SEARCH_RESULTS_PREFIX + "NetAccountAssociation";
        public const string SEARCH_RESULTS_AUDIT_TRAIL = SEARCH_RESULTS_PREFIX + "AuditTrailByGLDataID";
        public const string SEARCH_RESULTS_ACCOUNT_VIEWER_SRA = SEARCH_RESULTS_PREFIX + "AccountViewerSRA";
        public const string SEARCH_RESULTS_RECONCILIATION_STATUS_FSCAPTION = SEARCH_RESULTS_PREFIX + "ReconciliationStatusByFSCaption";
        public const string TASK_ACCOUNT_LIST = SESSION_KEY_PREFIX + "TaskAccountList";
        public const string SEARCH_RESULTS_ACCOUNTS = "SearchResultsAccounts";
        public const string SEARCH_RESULTS_DATA_IMPORT_SCHEDULE_REC_ITEM = SEARCH_RESULTS_PREFIX + "DataImportScheduleRecItem";

        #endregion

        #region "Grid Customization Constants"
        public const string GRID_CUSTOMIZATION_PREFIX = SESSION_KEY_PREFIX + "GridCustomization_";
        public const string GRID_CUSTOMIZATION_GRID_TYPE = GRID_CUSTOMIZATION_PREFIX + "GridType_";
        #endregion

        #region "Grid Filter Constants"

        public const string GRID_FILTER_CRITERIA = SESSION_KEY_PREFIX + "GridFilterCriteria_";

        public const string GRID_DYNAMIC_FILTER_CRITERIA = SESSION_KEY_PREFIX + "GridDynamicFilterCriteria_";

        public const string GRID_DYNAMIC_FILTER_INFO = SESSION_KEY_PREFIX + "GridDynamicFilterInfo_";

        #endregion

        #region Package

        public const string ALL_PACKAGE_LIST = SESSION_KEY_MASTER_DATA_PREFIX + "AllPackageList";

        #endregion

        #region FilterClauseForGrid
        public const string GRID_FILTER_CLAUSE = SESSION_KEY_PREFIX + "GridFilterClause_";

        #endregion


        public const string SESSION_KEY_JE_WRITE_OFF_ON_APPROVER = SESSION_KEY_MASTER_DATA_PREFIX + "JEWriteOffOnApprover";


        public const string MATCH_SET = SESSION_KEY_PREFIX + "MatchSet";


        // #endregion



        //Matching Session Constants
        public const string MATCH_SOURCE_TYPE = SESSION_KEY_MASTER_DATA_PREFIX + "MatchSourceType";
        public const string MATCHING_SOURCE_DATA = SESSION_KEY_PREFIX + "MatchingSourceData";
        public const string MATCHING_SOURCE_COLUMN = SESSION_KEY_PREFIX + "MatchingSourceColumn";

        public const string MATCHING_SOURCE_INFO_LIST_NBF = SESSION_KEY_PREFIX + "MatchingSourceInfoListNBF";
        public const string MATCHING_SOURCE_INFO_LIST_GLTBS = SESSION_KEY_PREFIX + "MatchingSourceInfoListGLTBS";

        public const string ALL_DATA_TYPE = SESSION_KEY_MASTER_DATA_PREFIX + "AllDataType";
        public const string ALL_KEY_FIELDS = SESSION_KEY_MASTER_DATA_PREFIX + "ALLKeyFields";

        public const string MATCHING_SOURCE_FILTER_DATA = SESSION_KEY_PREFIX + "MatchingSourceFilterData";
        public const string MATCHING_SOURCE_SUBSET_DATA = SESSION_KEY_PREFIX + "MatchingSourceSubSetData";

        public const string MATCHING_RULE_SETUP = SESSION_KEY_PREFIX + "MatchingRuleSetup";

        public const string MATCHING_CONFIGURATION_DATA = SESSION_KEY_PREFIX + "MatchingConfigurationData";
        public const string MATCHING_MATCH_SET_SUB_SET_COMBINATION_INFO_LIST = SESSION_KEY_PREFIX + "MatchingMatchSetSubSetCombinationInfoList";
        public const string MATCHING_CURRENT_MATCH_SET_SUB_SET_COMBINATION_INFO = SESSION_KEY_PREFIX + "MatchingCurrentMatchSetSubSetCombinationInfo";

        public const string MATCHING_MATCH_SET_RESULT_MATCHED_SOURCE = SESSION_KEY_PREFIX + "MatchingMatchSetResultDataMatchedSource";
        public const string MATCHING_MATCH_SET_RESULT_MATCHED_WORKSPACE_SOURCE = SESSION_KEY_PREFIX + "MatchingMatchSetResultDataMatchedWorkspaceSource";

        public const string MATCHING_MATCH_SET_RESULT_PARTIAL_MATCHED_SOURCE = SESSION_KEY_PREFIX + "MatchingMatchSetResultDataPartialMatchedSource";
        public const string MATCHING_MATCH_SET_RESULT_PARTIAL_MATCHED_WORKSPACE_SOURCE = SESSION_KEY_PREFIX + "MatchingMatchSetResultDataPartialMatchedWorkspaceSource";

        public const string MATCHING_MATCH_SET_RESULT_UNMATCHED_SOURCE = SESSION_KEY_PREFIX + "MatchingMatchSetResultDataUnmatchedSource";
        public const string MATCHING_MATCH_SET_RESULT_UNMATCHED_WORKSPACE_SOURCE = SESSION_KEY_PREFIX + "MatchingMatchSetResultDataUnmatchedWorkspaceSource";
        public const string MATCHING_MATCH_SET_RESULT_UNMATCHED_SOURCE_COMBINED = SESSION_KEY_PREFIX + "MatchingMatchSetResultDataUnmatchedSourceCombined";
        public const string MATCHING_MATCH_SET_RESULT_UNMATCHED_SOURCE_COMBINED_FILTERED = SESSION_KEY_PREFIX + "MatchingMatchSetResultDataUnmatchedSourceCombinedFiltered";

        public const string MATCHING_MATCH_SET_RESULT_CREATE_REC_ITEM_DATA = SESSION_KEY_PREFIX + "MatchingMatchSetResultCreateRecItemData";

        public const string MATCHING_MATCH_KEY_ARGUMENT = "Matched";

        public const string MATCHING_PARTIALMATCH_KEY_ARGUMENT = "PartialMatched";
        public const string PARENT_PAGE_URL = SESSION_KEY_PREFIX + "ParentPageURL";

        public const string REC_ITEM_TEMPLATE_ID = "RecTemplateIdReports";
        public const string MATCHING_MATCH_SET_RESULT_CLOSE_REC_ITEM_DATA_FILTERED = SESSION_KEY_PREFIX + "MatchingMatchSetResultCreateRecitemDataFiltered";
        public const string GLADJUSTMENT_GRID_DATA = SESSION_KEY_PREFIX + "gladjustmentgriddata";
        public const string GLADJUSTMENT_GRID_DATA_FILTERED = SESSION_KEY_PREFIX + "gladjustmentgriddatafiltered";
        public const string MATCHING_MATCH_SET_RESULT_USED_RECORDS_COMBINED = SESSION_KEY_PREFIX + "MatchingMatchSetResultUsedRecordsCombined";
        public const string CHECKED_ITEMS = SESSION_KEY_PREFIX + "CheckedItems";
        public const string WRITE_OFF_ON_GRID_DATA = SESSION_KEY_PREFIX + "writeoffongriddata";
        public const string WRITE_OFF_ON_GRID_DATA_FILTERED = SESSION_KEY_PREFIX + "writeoffongriddatafiltered";
        public const string RECURRING_SCHEDULE_ITEMS_GRID_DATA = SESSION_KEY_PREFIX + "recurringscheduleitemsgriddata";
        public const string RECURRING_SCHEDULE_ITEMS_GRID_DATA_FILTERED = SESSION_KEY_PREFIX + "recurringscheduleitemsgriddatafiltered";
        public const string GENERAL_TASK_GRID_DATA = SESSION_KEY_PREFIX + "generaltaskgriddata";
        public const string GENERAL_TASK_GRID_DATA_FILTERED = SESSION_KEY_PREFIX + "generaltaskgriddatafiltered";
        public const string ACCOUNT_TASK_GRID_DATA = SESSION_KEY_PREFIX + "accounttaskgriddata";



        #region Quality Score

        public const string QUALITY_SCORE_COMPANT_QUALITY_METRIC_LIST = SESSION_KEY_PREFIX + "QualityScoreCompanyQualityMetricList";
        public const string QUALITY_SCORE_GLDATA_QUALITY_METRIC_LIST = SESSION_KEY_PREFIX + "QualityScoreGLDataQualityMetricList";

        #endregion
        #region RecControl CheckList
        public const string REC_CONTROL_CHECK_LIST_GLDATA = SESSION_KEY_PREFIX + "RecControlCheckListGldata";
        #endregion

        #region Role Config

        public const string ROLE_CONFIG_COMPANY_ROLE_CONFIG_LIST = SESSION_KEY_PREFIX + "RoleConfigCompanyRoleConfigList";
        public const string ROLE_CONFIG_COMPANY_ROLE_CONFIG_FILTER_LIST = SESSION_KEY_PREFIX + "RoleConfigCompanyRoleConfigFilterList";

        #endregion

        #region mapping Upload

        public const string MAPPING_UPLOAD_LIST = SESSION_KEY_PREFIX + "MappingUploadList";

        #endregion

        public const string DATE_TIME = SESSION_KEY_PREFIX + "datetime";

        #region Task Master

        public const string TASK_MASTER_ATTACHMENT = SESSION_KEY_PREFIX + "taskmasterattachment";
        public const string TASK_CATEGORY = SESSION_KEY_MASTER_DATA_PREFIX + "TaskCategory";
        public const string TASK_STATUS = SESSION_KEY_MASTER_DATA_PREFIX + "TaskStatus";
        public const string ALL_TASK_TYPE = SESSION_KEY_MASTER_DATA_PREFIX + "AllTaskType";
        #endregion
        public const string CURRENT_DUAL_LEVEL_REVIEW_TYPE = SESSION_KEY_MASTER_DATA_PREFIX + "DualLevelReviewType";

        public const string ALL_OPERATOR_LIST = SESSION_KEY_MASTER_DATA_PREFIX + " AllOperatorList";

        public const string ALL_FIELD_LIST = SESSION_KEY_MASTER_DATA_PREFIX + " AllFieldList";

        public const string ALL_DATAIMPORTMESSAGE_LIST = SESSION_KEY_MASTER_DATA_PREFIX + " AllDataImportMessageList";

        public const string ALL_FTPSERVER_LIST= SESSION_KEY_MASTER_DATA_PREFIX + "AllFTPServerList";

        public const string RECFORM_PRINT_PDF_FILE = SESSION_KEY_PREFIX + "RecFormPrintPDFFile";
        public const string DIRECT_DOWNLOAD_FILE = SESSION_KEY_PREFIX + "DirectDownload";

    }

    public class AppSettingConstants
    {
        public const string CACHE_REFRESH_RATE = "CacheRefreshRate";
        public const string LOGGER_NAME = ARTConstants.LOGGER_NAME;
        public const string APPLICATION_ID = "ApplicationID";
        public const string DEFAULT_LANGUAGE_ID = "DefaultLanguageID";
        public const string DEFAULT_BUSINESS_ENTITY_ID = "DefaultBusinessEntityID";
        public const string ASYNC_POSTBACK_TIMEOUT = "AsyncPostBackTimeout";

        public const string DEFAULT_MAX_DOC_SIZE = "DefaultMaxDocSize";
        public const int RISKRATINGFREQUENCYID_CUSTOM = 4;


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

        public const string DEFAULT_CHUNK_SIZE = "DefaultChunkSize";
        public const string DEFAULT_PAGE_SIZE = "DefaultPageSize";
        public const string MAX_RECORD_SIZE_FOR_NONPAGED_GRIDS = "MaxRecordSizeForNonPagedGrid";

        #region "Data Import folder"
        public const string BASE_FOLDER_FOR_FILES = "BaseFolderForFiles";
        public const string DEFAULTDATAIMPORTFILESIZE = "DefaultDataImportFileSize";
        public const string ALLOWEDFILEEXTENSIONS = "AllowedFileExtensions";
        public const string FOLDER_FOR_IMPORT_TEMPLATES = "FolderForImportTemplates";
        #endregion

        public const string FOLDER_FOR_ATTACHMENTS = "FolderForAttachments";
        public const string FOLDER_FOR_DOWNLOAD_REQUESTS = "FolderForDownloadRequests";
        public const string TEMP_FOLDER_FOR_EXPORT_FILES = "TempFolderForExportFiles";
        public const string TEMP_FOLDER_FOR_COMPANY_LOGO = "TempFolderForCompanyLogo";


        #region "Image URL"
        public const string IMAGE_URL_EDIT_ITEM_POPUP = "EditItemPopupImageURL";
        public const string IMAGE_URL_READ_ONLY_ITEM_POPUP = "ReadOnlyItemPopupImageURL";
        #endregion


        public const string BASE_SYSTEM_URL_FOR_PDF = "BaseSystemURLForPDF";
        public const string HTML_2_PDF_LICENSE_KEY = "Html2PdfLicenseKey";

        public const string USE_TEST_LCID = "UseTestLCID";

        public const string MATCHING_DATA_IMPORT_ROW = "MatchingDataImportRow";
        public const string DISPLAY_RECORDS_LIMIT_IN_A_MATCHING_PAIR = "DisplayRecordsLimitInAMatchingPair";

        #region QualityScore
        public const string QUALITY_SCORE_THRESHOLD = "QualityScoreThreshold";
        #endregion

        #region Session Timeout
        public const string SESSION_TIMEOUT_WARNING_INTERVAL_IN_MINUTES = "SessionTimeoutWarningIntervalInMinutes";
        public const string DISPLAY_SESSION_TIMEOUT_WARNING = "DisplaySessionTimeoutWarning";
        #endregion

        #region FilterSettings
        public const string ACCOUNT_FILTER_VALUE_SEPARATOR = "AccountFilterValueSeparator";
        public const string FILTER_VALUE_SEPARATOR = "FilterValueSeparator";
        #endregion

        #region Start Timer Elapsed Default Time in Mins
        public const string TIMER_INTERVAL_DATA_PROCESSING = "DataProcessingTimerIntervalInMins";
        #endregion

        #region Support Site Settings

        public const string SUPPORT_SITE_BASE_URL = "SupportSiteBaseURL";
        public const string SUPPORT_SITE_PRIVATE_KEY = "SupportSitePrivateKey";

        #endregion
    }

    public class CacheConstants
    {
        public const string ALL_COMPANIES_LITE_OBJECT = ARTConstants.CACHE_KEY_PREFIX + "AllCompaniesLiteObject";
        public const string COMPANY_INFO = ARTConstants.CACHE_KEY_PREFIX + "CompanyInfo";
        public const string ROLE_ID_LIST = ARTConstants.CACHE_KEY_PREFIX + "RoleIDList";
        public const string COMPANY_STRUCTURE = ARTConstants.CACHE_KEY_PREFIX + "CompanyStructure";
        public const string ALL_MATERIALITYTYPE_LIST = ARTConstants.CACHE_KEY_PREFIX + "MaterialityTypeList";
        public const string ALL_RISKRATING_LIST = ARTConstants.CACHE_KEY_PREFIX + "RiskRatingList";
        public const string ALL_RECONCILIATIONFREQUENCY_LIST = ARTConstants.CACHE_KEY_PREFIX + "ReconciliationFrequencyList";
        public const string ALL_ROLES = ARTConstants.CACHE_KEY_PREFIX + "AllRoles";
        public const string ORGANIZATIONAL_HIERARCHY_KEYS = ARTConstants.CACHE_KEY_PREFIX + "OrganizationalHierarchyKeys";
        public const string ALL_ACCOUNT_TYPE_MST_INFO = ARTConstants.CACHE_KEY_PREFIX + "AllAccountTypeMstInfo";
        public const string ALL_RECONCILIATION_TEMPLATE_MST_INFO = ARTConstants.CACHE_KEY_PREFIX + "AllReconciliationTemplateMstInfo";
        public const string ALL_RISK_RATING_MST_INFO = ARTConstants.CACHE_KEY_PREFIX + "AllRiskRatingMstInfo";
        public const string ALL_GEOGRAPHY_STRUCTURE_BY_COMPANYID = ARTConstants.CACHE_KEY_PREFIX + "AllGeographyStructureByCompanyID";
        public const string ALL_APPROVERS_FOR_CURRENT_COMPANY = ARTConstants.CACHE_KEY_PREFIX + "AllApproversForCurrentCompany";
        public const string ALL_REVIEWERS_FOR_CURRENT_COMPANY = ARTConstants.CACHE_KEY_PREFIX + "AllReviewersForCurrentCompany";
        public const string ALL_BUSINESSADMIN_FOR_CURRENT_COMPANY = ARTConstants.CACHE_KEY_PREFIX + "AllBusinessAdminForCurrentCompany";
        public const string ALL_PREPARERS_FOR_CURRENT_COMPANY = ARTConstants.CACHE_KEY_PREFIX + "AllPreparersForCurrentCompany";
        public const string ALL_BACKUP_PREPARERS_FOR_CURRENT_COMPANY = ARTConstants.CACHE_KEY_PREFIX + "AllBackupPreparersForCurrentCompany";
        public const string ALL_RECONCILIATION_PERIODS = ARTConstants.CACHE_KEY_PREFIX + "AllRecPeriods";
        public const string ALL_RECONCILIATION_PERIODS_BASED_ON_FY = ARTConstants.CACHE_KEY_PREFIX + "AllRecPeriodsBasedOnFY";
        public const string ALL_DATAIMPORTTYPE_LIST = ARTConstants.CACHE_KEY_PREFIX + "DataImportTypeList";
        public const string ALL_REC_PERIOD_STATUSES = ARTConstants.CACHE_KEY_PREFIX + "AllRecPeriodStatuses";
        public const string ALL_OPEN_ITEM_CLASSIFICATIONS = ARTConstants.CACHE_KEY_PREFIX + "AllOpenItemClassifications";
        public const string EXCHANGE_RATE_FROM_BASE_CURRENCY_TO_REPORTING_CURRENCY = ARTConstants.CACHE_KEY_PREFIX + "ExchangeRateFromBaseCurrencyToReportingCurrency";
        public const string EXCHANGE_RATE_FROM_REPORTING_CURRENCY_TO_BASE_CURRENCY = ARTConstants.CACHE_KEY_PREFIX + "ExchangeRateFromReportingCurrencyToBaseCurrency";
        public const string ALL_REASONS = ARTConstants.CACHE_KEY_PREFIX + "AllReasons";
        public const string ALL_REPORT_PARAMETER_KEY = ARTConstants.CACHE_KEY_PREFIX + "AllReportParameterKeys";
        public const string ALL_AGINGCATEGORIES = ARTConstants.CACHE_KEY_PREFIX + "AllAgingCategories";
        public const string ALL_COMPANY_ALERTS = ARTConstants.CACHE_KEY_PREFIX + "CompanyAlerts";
        public const string ALL_REC_STATUS = ARTConstants.CACHE_KEY_PREFIX + "AllRecStatus";
        public const string EXCEPTION_TYPES = ARTConstants.CACHE_KEY_PREFIX + "ExceptionTypes";
        public const string ALL_CERTIFICATION_STATUS_TYPE = ARTConstants.CACHE_KEY_PREFIX + "CertificationStatusType";
        public const string ALL_FINANCIAL_YEAR_LIST = ARTConstants.CACHE_KEY_PREFIX + "AllFinancialYearList";
        public const string ALL_WEEKDAYS_LIST = ARTConstants.CACHE_KEY_PREFIX + "WeekDaysList";
        public const string ALL_PACKAGE_LIST = ARTConstants.CACHE_KEY_PREFIX + "AllPackageList";
        public const string COMPANY_FEATURES = ARTConstants.CACHE_KEY_PREFIX + "CompanyFeatures";
        public const string ALL_ROLE_LIST = ARTConstants.CACHE_KEY_PREFIX + "AllRoleList";
        public const string EXCHANGE_RATE = ARTConstants.CACHE_KEY_PREFIX + "ExchangeRateData";
        public const string MATCH_SET = ARTConstants.CACHE_KEY_PREFIX + "MatchSet";
        public const string ALL_ROLE_CONFIG_LIST = ARTConstants.CACHE_KEY_PREFIX + "RoleConfigList";
        public const string MATCH_SOURCE_TYPE = ARTConstants.CACHE_KEY_PREFIX + "MatchSourceType";
        public const string ALL_SYSTEM_LOCKDOWN_REASONS = ARTConstants.CACHE_KEY_PREFIX + "AllSystemLockdownReasons";
        public const string ALL_DUALLEVELREVIEWTYPE_LIST = ARTConstants.CACHE_KEY_PREFIX + "DualLevelReviewTypeList";

        public const string ALL_DATA_TYPE = ARTConstants.CACHE_KEY_PREFIX + "AllDataType";
        public const string ALL_DEPENDENT_STEPS = ARTConstants.CACHE_KEY_PREFIX + "AllDependentSteps";

        public const string ALL_QUALITY_SCORE_STATUS_TYPE = ARTConstants.CACHE_KEY_PREFIX + "AllQualityScoreStatues";

        public const string ALL_MAPPINGUPLOADKEY_LIST = ARTConstants.CACHE_KEY_PREFIX + "MappingUploadKeyList";
        public const string ALL_CAPABILITIES_LIST = ARTConstants.CACHE_KEY_PREFIX + "MappingUploadKeyList";

        public const string ALL_RANGEOFSCORECATEGORIES = ARTConstants.CACHE_KEY_PREFIX + "AllRangeOfScoreCategories";
        public const string ALL_CHECKLISTCATEGORIES = ARTConstants.CACHE_KEY_PREFIX + "AllCheckListCategories";

        public const string ALL_BACKUP_APPROVERS_FOR_CURRENT_COMPANY = ARTConstants.CACHE_KEY_PREFIX + "AllBackupApproversForCurrentCompany";
        public const string ALL_BACKUP_REVIEWERS_FOR_CURRENT_COMPANY = ARTConstants.CACHE_KEY_PREFIX + "AllBackupReviewersForCurrentCompany";

        public const string ALL_USERS_FOR_CURRENT_COMPANY = ARTConstants.CACHE_KEY_PREFIX + "AllUsersForCurrentCompany";
        public const string TASK_STATUS_IDS_BY_TASK_ACTION_TYPE_ID = ARTConstants.CACHE_KEY_PREFIX + "TaskStatusIDsByTaskActionTypeID";
        public const string TASK_CATEGORY = ARTConstants.CACHE_KEY_PREFIX + "TaskCategory";
        public const string TASK_STATUS = ARTConstants.CACHE_KEY_PREFIX + "TaskStatus";
        public const string ALL_TASK_TYPE = ARTConstants.CACHE_KEY_PREFIX + "AllTaskType";
        public const string REPORT_COLUMNS = ARTConstants.CACHE_KEY_PREFIX + "ReportColumns";
        public const string ALL_DUE_DAYS_BASIS_TYPE_LIST = ARTConstants.CACHE_KEY_PREFIX + "DueDaysBasisType";
        public const string ALL_DAYS_TYPE_LIST = ARTConstants.CACHE_KEY_PREFIX + "DueType";

        public const string ALL_OPERATOR_TYPE = ARTConstants.CACHE_KEY_PREFIX + "ALLOPERATORTYPE";

        public const string ALL_IMPORTFIELD_LIST = ARTConstants.CACHE_KEY_PREFIX + "ALLIMPORTFIELDLIST";

        public const string ALL_DATAIMPORTMESSAGE_LIST = ARTConstants.CACHE_KEY_PREFIX + "ALLDATAIMPORTMESSAGELIST";

        public const string ALL_FTPSERVER_LIST = ARTConstants.CACHE_KEY_PREFIX + "AllFTPServerList";

    }

    public struct ViewStateConstants
    {
        public const string IMPORTFILEATTRIBUTES = ARTConstants.CACHE_KEY_PREFIX + "ImportFileAttributes";
        public const string GRID_TYPE = "SkyStemARTGridType";
        public const string PATH_AND_QUERY = "PathAndQuery";
        public const string GENERAL_ALERTS = "GeneralAlerts";
        public const string SYSTEM_ADMIN_ALERTS = "SystemAdminAlerts";
        public const string OTHER_ALERTS = "OtherAlerts";
        public const string VIEWSTATE_KEY_FIRST_TIME_LOAD = "IsFirstTimeLoad";
        public const string CURRENT_ACCT_BCCY = "CurrentAccountBCCY";
        public const string CURRENT_GLDATAHDRINFO = "CurrentGLDataHdrInfo";
        public const string ENTITY_NAME_LABEL_ID = "EntityNameLabelID";
        public const string REC_CATEGORY_TYPE_ID = "RecCategoryTypeID";
        public const string REC_CATEGORY_ID = "RecCategoryID";
        public const string ACCESSIBLE_ACCOUNT_LIST = "AccessibleAccountList";
        public const string SEARCH_RESULTS_TASK_ACCOUNTS = "SearchResultsTaskAccounts";
        public const string REVIEWNOTE_DETAIL = "ReviewNoteDetailList";
        public const string ACTIVE_TAB_INDEX = "ActiveTabIndex";
        public const string REC_ITEM_COMMENT_LIST = "RecItemCommentList";
        public const string PRA_LIST = "PRAList";
        public const string REPORTSACTIVITY_LIST = "ReportsActivityList";
        public const string REC_CONTROL_CHECK_LIST_COMMENT_LIST = "RecControlCheckListCommentList";
        public const string REC_CONTROL_CHECK_LIST_INFO = "RecControlCheckListInfo";
        public const string RISK_RATING_CURRENT_DB_VAL = "RiskRatingCurrentDBVal";
        public const string RISK_RATING_RECPERIODS_CURRENT_DB_VAL = "RiskRatingRecPeriodsCurrentDBVal";
        public const string DUAL_LEVEL_REVIEW_TYPE_CURRENT_DB_VAL = "DualLevelReviewTypeCurrentDBVal";
        public const string RCCL_VALIDATION_TYPE_CURRENT_DB_VAL = "RCCLValidationTypeCurrentDBVal";
        public const string Is_RCCL_YES_CHECKED_CURRENT = "IsRCCLYesCheckedCurrent";
        public const string ROLE_MANDATORY_REPORT_CURRENT_DB_VAL = "RoleMandatoryReportCurrentDBVal";
        public const string MATERIALITY_TYPE_CURRENT_DB_VAL = "MaterialityTypeCurrentDBVal";
        public const string FSCAPTION_MATERIALITY_CURRENT_DB_VAL = "FSCaptionMaterialityCurrentDBVal";
        public const string TASK_CUSTOM_FIELDS_CURRENT_DB_VAL = "TaskCustomFieldsCurrentDBVal";
        public const string DAY_TYPE_CURRENT_DB_VAL = "DayTypeCurrentDBVal";

    }

 

    /// <summary>
    /// Structure defining Company key for Geography Class and Structure
    /// </summary>
    public struct GeographyClassCompanyKey
    {
        public const short GEOGRAPHYCLASSID = 1;
        public const string GEOGRAPHYCLASSNAME = "Company";
        public const int GEOGRAPHYCLASSLABELID = 1059;
        public const string GEOGRAPHYSTRUCTURE = "Company";
        public const int GEOGRAPHYSTRUCTURELABELID = 1229;
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

    public struct ReportCriteriaKeyName
    {
        public const string RPTCRITERIAKEYNAME_KEY2 = "Key2";
        public const string RPTCRITERIAKEYNAME_KEY3 = "Key3";
        public const string RPTCRITERIAKEYNAME_KEY4 = "Key4";
        public const string RPTCRITERIAKEYNAME_KEY5 = "Key5";
        public const string RPTCRITERIAKEYNAME_KEY6 = "Key6";
        public const string RPTCRITERIAKEYNAME_KEY7 = "Key7";
        public const string RPTCRITERIAKEYNAME_KEY8 = "Key8";
        public const string RPTCRITERIAKEYNAME_KEY9 = "Key9";

        public const string RPTCRITERIAKEYNAME_FINANCIAL_YEAR = "FinancialYear";
        public const string RPTCRITERIAKEYNAME_RECPERIOD = "Period";
        public const string RPTCRITERIAKEYNAME_ISKEYACCOUNT = "KeyAccount";
        public const string RPTCRITERIAKEYNAME_REASON = "Reason";
        public const string RPTCRITERIAKEYNAME_ISMATERIALACCOUNT = "MaterialAccount";
        public const string RPTCRITERIAKEYNAME_RISKRATING = "RiskRating";
        public const string RPTCRITERIAKEYNAME_ORGHIERARCHY = "Entity";
        public const string RPTCRITERIAKEYNAME_FROMACCOUNT = "FromAccount";
        public const string RPTCRITERIAKEYNAME_TOACCOUNT = "ToAccount";

        public const string RPTCRITERIAKEYNAME_FROMSYSTEMSCORE = "FromSystemScore";
        public const string RPTCRITERIAKEYNAME_TOSYSTEMSCORE = "ToSystemScore";

        public const string RPTCRITERIAKEYNAME_FROMUSERSCORE = "FromUserScore";
        public const string RPTCRITERIAKEYNAME_TOUSERSCORE = "ToUserScore";

        public const string RPTCRITERIAKEYNAME_USER = "User";
        public const string RPTCRITERIAKEYNAME_ROLE = "Role";

        public const string RPTCRITERIAKEYNAME_PREPARERUSER = "PreparerUser";
        public const string RPTCRITERIAKEYNAME_PREPARERROLE = "PreparerRole";

        public const string RPTCRITERIAKEYNAME_PREPARER = "Preparer";
        public const string RPTCRITERIAKEYNAME_OPENITEMCLASSIFICATION = "OpenItemClassification";
        public const string RPTCRITERIAKEYNAME_FROMOPENDATE = "FromOpenDate";
        public const string RPTCRITERIAKEYNAME_TOOPENDATE = "ToOpenDate";

        public const string RPTCRITERIAKEYNAME_FROMCLOSEDATE = "FromCloseDate";
        public const string RPTCRITERIAKEYNAME_TOCLOSEDATE = "ToCloseDate";

        public const string RPTCRITERIAKEYNAME_FROMDATECREATED = "FromDateCreated";
        public const string RPTCRITERIAKEYNAME_TODATECREATED = "ToDateCreated";

        public const string RPTCRITERIAKEYNAME_FROMCHANGEDATE = "FromChangeDate";
        public const string RPTCRITERIAKEYNAME_TOCHANGEDATE = "ToChangeDate";

        public const string RPTCRITERIAKEYNAME_AGING = "Aging";
        public const string RPTCRITERIAKEYNAME_RECSTATUS = "RecStatus";
        public const string RPTCRITERIAKEYNAME_TYPEOFEXCEPTION = "TypeOfException";

        public const string RPTCRITERIAKEYNAME_RANGEOFSCORE = "RangeOfScore";
        public const string RPTCRITERIAKEYNAME_CHECKLISTITEM = "CheckListItem";
        public const string RPTCRITERIAKEYNAME_ASONDATE = "AsOnDate";

        public const string RPTCRITERIAKEYNAME_TASKSTATUS = "TaskStatus";
        public const string RPTCRITERIAKEYNAME_TASKLISTNAME = "TaskListName";
        public const string RPTCRITERIAKEYNAME_TASKTYPE = "TaskType";

        public const string RPTCRITERIAKEYNAME_DISPLAYCOLUMN = "DisplayColumn";

    }

    public struct TestConstant
    {
        public const Int32 DECIMAL_PLACES_FOR_TEXTBOX = 2;
        public const Int32 DECIMAL_PLACES_FOR_MATH_ROUND = 4;
        public const Int32 DECIMAL_PLACES_FOR_EXCHANGE_RATE_ROUND = 4;
    }

    public struct AccountImportFields
    {
        public const string FSCAPTION = "FS Caption";
        public const string ACCOUNTTYPE = "Account Type";
        public const string GLACCOUNTNUMBER = "GL Account #";
        public const string GLACCOUNTNAME = "GL Account Name";
        public const string ISPROFITANDLOSS = "Is P&L";
    }

    public struct GLDataImportFields
    {
        //public const string[] MANDATORYGLDATAIMPORTFIELDS = {"Period End Date", "Company", "GL Account #"
        //, "GL Account Name", "FS Caption", "Account Type"
        //, "Base CCY Code", "Balance in Base CCY"
        //, "Balance in Reporting CCY","Reporting CCY Code"};
        public const string PERIODENDDATE = "Period End Date";
        public const string COMPANY = "Company";
        public const string GLACCOUNTNUMBER = AccountImportFields.GLACCOUNTNUMBER;
        public const string GLACCOUNTNAME = AccountImportFields.GLACCOUNTNAME;
        public const string FSCAPTION = AccountImportFields.FSCAPTION;
        public const string ACCOUNTTYPE = AccountImportFields.ACCOUNTTYPE;
        public const string ISPROFITANDLOSS = AccountImportFields.ISPROFITANDLOSS;
        public const string BCCYCODE = "Base CCY Code";
        public const string BALANCEBCCY = "Balance in Base CCY";
        public const string RCCYCODE = "Reporting CCY Code";
        public const string BALANCERCCY = "Balance in Reporting CCY";

        public const short GLDATAIMPORTMANDATORYFIELDCOUNT = 11;
        public const short MAPPINGUPLOADIMPORTMANDATORYFIELDCOUNT = 6;
        public const short USERUPLOADMANDATORYFIELDCOUNT = 5;

        public const string FIRSTNAME = "First Name";
        public const string LASTTNAME = "Last Name";
        public const string LOGINID = "Login ID";
        public const string EMAILID = "Email ID";
        public const string DEFAULTROLE = "Default Role";

    }

    public struct AccountAttributeLoadImportFields
    {
        public const string COMPANY = "Company";
        public const string FSCAPTION = AccountImportFields.FSCAPTION;
        public const string ACCOUNTTYPE = AccountImportFields.ACCOUNTTYPE;
        public const string GLACCOUNTNUMBER = AccountImportFields.GLACCOUNTNUMBER;
        public const string GLACCOUNTNAME = AccountImportFields.GLACCOUNTNAME;

        public const short ACCOUNTATTRIBUTEDATAIMPORTMANDATORYFIELDCOUNT = 5;
    }

    public struct SubLedgerDataImportFields
    {
        //public const string[] MANDATORYGLDATAIMPORTFIELDS = {"Period End Date", "Company", "GL Account #"
        //, "GL Account Name", "FS Caption", "Account Type"
        //, "Base CCY Code", "Balance in Base CCY"
        //, "Balance in Reporting CCY","Reporting CCY Code"};
        public const string PERIODENDDATE = "Period End Date";
        public const string GLACCOUNTNUMBER = AccountImportFields.GLACCOUNTNUMBER;
        public const string GLACCOUNTNAME = AccountImportFields.GLACCOUNTNAME;
        public const string FSCAPTION = AccountImportFields.FSCAPTION;
        public const string ACCOUNTTYPE = AccountImportFields.ACCOUNTTYPE;
        public const string ISPROFITANDLOSS = AccountImportFields.ISPROFITANDLOSS;
        public const string BCCYCODE = "Base CCY Code";
        public const string BALANCEBCCY = "Balance in Base CCY";
        public const string RCCYCODE = "Reporting CCY Code";
        public const string BALANCERCCY = "Balance in Reporting CCY";

        public const short MANDATORYFIELDCOUNT = 10;

    }

    public struct AddedGLTBSImportFields
    {
        public const string ISRECITEMCREATED = "__IsRecItemCreated";
        public const string EXCELROWNUMBER = "__ExcelRowNumber";
    }

    public struct MatchSetResultConstants
    {
        public const string MATCH_SET_RESULT_XPATH_BINDGRID = "//MatchSetResult/Pair";
        public const string MATCH_SET_RESULT_XPATH_SOURCEROWS = "./MatchSetSource[@ID={0}]/Row/{1}";
        public const string MATCH_SET_RESULT_XPATH_VALUE = "./{0}";

        public const string MATCH_SET_RESULT_TABLE_NAME_PAIR = "Pair";
        public const string MATCH_SET_RESULT_TABLE_NAME_SOURCE1 = "Source1";
        public const string MATCH_SET_RESULT_TABLE_NAME_SOURCE2 = "Source2";
        public const string MATCH_SET_RESULT_RELATION_NAME_SOURCE1 = "PairSource1";
        public const string MATCH_SET_RESULT_RELATION_NAME_SOURCE2 = "PairSource2";

        public const string MATCH_SET_RESULT_COLUMN_NAME_PAIR_ID = "__PairID";
        public const string MATCH_SET_RESULT_COLUMN_NAME_DATA_SOURCE_NAME = "__DataSourceName";
        public const string MATCH_SET_RESULT_COLUMN_NAME_EXCEL_ROW_NUMBER = "__ExcelRowNumber";
        public const string MATCH_SET_RESULT_COLUMN_NAME_MATCH_SET_MATCHING_SOURCE_DATA_IMPORT_ID = "__MatchSetMatchingSourceDataImportID";
        public const string MATCH_SET_RESULT_COLUMN_NAME_MATCHING_SOURCE_DATA_IMPORT_ID = "__MatchingSourceDataImportID";
        public const string MATCH_SET_RESULT_COLUMN_NAME_REC_ITEM_NUMBER = "__RecItemNumber";
        public const string MATCH_SET_RESULT_COLUMN_NAME_CLOSE_DATE = "__CloseDate";
        public const string MATCH_SET_RESULT_COLUMN_NAME_NET_VALUE = "__NetValue";


        public const long MATCH_SET_RESULT_COLUMN_ID_DATA_SOURCE_NAME = -1;
        public const long MATCH_SET_RESULT_COLUMN_ID_REC_ITEM_NUMBER = -2;

        public const long MATCH_SET_RESULT_COLUMN_ID_PAIR_ID = -2;
        public const long MATCH_SET_RESULT_COLUMN_ID_CLOSE_DATE = -3;

    }

    public class CookieConstants
    {
        public const string USER_LANGUAGE = "Cookie_UserLanguage";
    }

    public struct ToolBarCommmand
    {
        public const string TOOLBAR_COMMAND_ADD = "Add";
        public const string TOOLBAR_COMMAND_BULKEDIT = "BulkEdit";
        public const string TOOLBAR_COMMAND_DELETE = "Delete";
        public const string TOOLBAR_COMMAND_APPROVE = "Approve";
        public const string TOOLBAR_COMMAND_REJECT = "Reject";
        public const string TOOLBAR_COMMAND_DONE = "Done";
        public const string TOOLBAR_COMMAND_REVIEW = "Review";
        public const string TOOLBAR_COMMAND_REOPEN = "Reopen";

        public const string TOOLBAR_COMMAND_IMPORT = "Import";
    }
    public struct TaskViewerConstants
    {
        public const string TOOLBAR_COMMAND_ADD_ATTRIBUTE_NAME_GENERAL_TASK = "AddGeneralTaskFunctionName";
        public const string TOOLBAR_COMMAND_ADD_ON_CLIENT_CLICKED_GENERAL_TASK = "OnClientAddButtonClickedGeneralTask";
        public const string TOOLBAR_COMMAND_ADD_ON_CLIENT_CLICKED_ACCOUNT_TASK = "OnClientAddButtonClickedAccountTask";
        public const string TOOLBAR_COMMAND_ADD_ATTRIBUTE_NAME_ACCOUNT_TASK = "AddAccountTaskFunctionName";
    }

    public struct RecControlCheckListStatus
    {
        public const String Yes = "1252";
        public const String No = "1251";
        public const String NA = "2858";

    }

    public struct RCCValidationType
    {
        public const int Carry_forward_RCC_answers_for_SRA = 1;
        public const int Exclude_SRAs_from_RCC_requirement = 2;
    }
}