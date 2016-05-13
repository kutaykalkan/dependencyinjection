using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using SkyStem.Library.Controls.WebControls;
using SkyStem.ART.Web.Utility;
using SkyStem.ART.Web.Classes;
using SkyStem.ART.Client.IServices;
using SkyStem.ART.Client.Model;
using SkyStem.ART.Web.Data;
using SkyStem.Language.LanguageUtility;
using Telerik.Web.UI;
using SkyStem.ART.Client.Exception;
using System.Text;
using SkyStem.ART.Client.Data;
using System.Collections;
using SkyStem.ART.Shared.Data;
using SkyStem.Library.Controls.TelerikWebControls.Data;
using SkyStem.ART.Client.Params.RecItemUpload;
using SkyStem.Language.LanguageUtility.Classes;

public partial class Pages_RecurringImportUII : PageBaseRecPeriod
{
    bool isExportPDF;
    bool isExportExcel;
    long? _AccountID = 0;
    long? _GLDataID = 0;
    short? _RecCategoryTypeID = 0;
    short? _RecCategoryID = 0;
    bool _IsMultiCurrencyEnabled = false;
    private decimal _ExchangeRateBaseCurrency = 1;
    private decimal _ExchangeRateReportingCurrency = 1;
    private bool? _IsSRA = false;
    bool isError = false;
    long? _NetAccountID;
    short? recCategoryTypeID;
    //BCCY Changes
    private string CurrentBCCY
    {
        get
        {
            if (ViewState[ViewStateConstants.CURRENT_ACCT_BCCY] == null)
            {
                ViewState[ViewStateConstants.CURRENT_ACCT_BCCY] = Helper.GetCurrentAccountBCCY(this._GLDataID);
            }
            return ViewState[ViewStateConstants.CURRENT_ACCT_BCCY].ToString();
        }
        set
        {
            ViewState[ViewStateConstants.CURRENT_ACCT_BCCY] = value;
        }

    }

    private GLDataHdrInfo _GLDataHdrInfo = null;
    //List<ExchangeRateInfo> oExchangeRateInfoCollection;

    #region "Public Properties"
    public string ReturnUrl
    {
        get
        {
            return ViewState["returnUrl"].ToString();
        }
        set
        {
            ViewState["returnUrl"] = value;
        }
    }
    public GLDataHdrInfo GLDataHdrInfo
    {
        get
        {
            if (_GLDataHdrInfo == null)
                _GLDataHdrInfo = (GLDataHdrInfo)ViewState[ViewStateConstants.CURRENT_GLDATAHDRINFO];
            return _GLDataHdrInfo;
        }
        set
        {
            _GLDataHdrInfo = value;
            ViewState[ViewStateConstants.CURRENT_GLDATAHDRINFO] = value;
        }
    }
    #endregion

    #region "Page Methods"

    protected void Page_Init(object sender, EventArgs e)
    {
        MasterPageBase oMasterPageBase = (MasterPageBase)this.Master.Master;
        oMasterPageBase.ReconciliationPeriodChangedEventHandler += new EventHandler(oMasterPageBase_ReconciliationPeriodChangedEventHandler);
        ScriptManager scriptManager = oMasterPageBase.GetScriptManager();
        scriptManager.RegisterPostBackControl(btnPreview);
        scriptManager.RegisterPostBackControl(btnImportAll);
        scriptManager.RegisterPostBackControl(btnUpload);
    }

    void oMasterPageBase_ReconciliationPeriodChangedEventHandler(object sender, EventArgs e)
    {
        this.GLDataHdrInfo = null;
        SetPrivateVariables();
        PopulateItemsOnPage();
        EnableDisableControls();
        RefreshDataImportStatusGrid(true);
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        int importSourceSectionLabelID = 2068;
        //TODO: VINAY Uncomment In Next Release v1.6
        //if (!String.IsNullOrEmpty(Request.QueryString[QueryStringConstants.IMPORT_SOURCE_PAGE_SECTION_LABEL_ID]))
        //    importSourceSectionLabelID = Int32.Parse(Request.QueryString[QueryStringConstants.IMPORT_SOURCE_PAGE_SECTION_LABEL_ID]);
        int lastPagePhreaseID = Helper.GetPageTitlePhraseID(this.GetPreviousPageName());
        if (lastPagePhreaseID != -1)
        {
            Helper.SetBreadcrumbs(this, 1071, 1187, lastPagePhreaseID, importSourceSectionLabelID);
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            SetPrivateVariables();
            PopulateItemsOnPage();

            if (Request.UrlReferrer == null)
            {
                //Handle the case where the page is requested directly
                //throw new Exception("This page has been called without a referring page");
            }
            else
            {
                if (!IsPostBack)
                {
                    string isRedirectedFromStatusPage = Convert.ToString(Request.QueryString[QueryStringConstants.IS_REDIRECTED_FROM_STATUSPAGE]);

                    if (string.IsNullOrEmpty(isRedirectedFromStatusPage))
                    {
                        ReturnUrl = Request.UrlReferrer.PathAndQuery;
                        Session["RedirectUrlForImportPage"] = ReturnUrl;
                    }
                    else
                    {
                        string confirmStatusMessageID = Convert.ToString(Request.QueryString[QueryStringConstants.CONFIRMATION_MESSAGE_FROM_STATUSPAGE]);
                        ReturnUrl = Convert.ToString(Session["RedirectUrlForImportPage"]);

                        if (!string.IsNullOrEmpty(confirmStatusMessageID) && Convert.ToInt32(confirmStatusMessageID) == 3)
                        {
                            //ViewState["DataTable"] = Session["DataTableForImport"];
                            //ImportAllUploadData();
                            MasterPageBase oMasterPageBase = (MasterPageBase)this.Master.Master;
                            oMasterPageBase.ShowConfirmationMessage(1784);
                        }
                        else if (!string.IsNullOrEmpty(confirmStatusMessageID) && Convert.ToInt32(confirmStatusMessageID) == 2)
                        {
                            MasterPageBase oMasterPageBase = (MasterPageBase)this.Master.Master;
                            oMasterPageBase.ShowConfirmationMessage(2399);
                        }
                    }

                    //Setting allowed file extensions.
                    this.radFileUpload.AllowedFileExtensions = DataImportHelper.GetAllowedFileExtensions();

                    //Set allowed file size in bytes
                    this.radFileUpload.MaxFileSize = DataImportHelper.GetAllowedMaximumFileSize(SessionHelper.CurrentCompanyID.Value);
                    //setting error messages to be shown at runtime
                    this.cvFileUpload.Attributes.Add("fileNameErrorMessage", LanguageUtil.GetValue(5000035));
                    this.cvFileUpload.Attributes.Add("fileExtensionErrorMessage", LanguageUtil.GetValue(5000036));

                    //Hide all Preview Grids
                    pnlImportGrid.Visible = false;
                    rgImportList.Visible = false;
                }

                //Rec Category Type ID
                if (Request.QueryString[QueryStringConstants.REC_CATEGORY_TYPE_ID] != null)
                {
                    recCategoryTypeID = Convert.ToInt16(Request.QueryString[QueryStringConstants.REC_CATEGORY_TYPE_ID]);
                }
            }
        }
        catch (Exception ex)
        {
            Helper.ShowErrorMessage(this, ex);
        }
        EnableDisableControls();
    }
    #endregion

    #region "Control event Handlers"
    protected void btnPreview_Click(object sender, EventArgs e)
    {
        if (radFileUpload.UploadedFiles.Count > 0)
        {
            string filePath = string.Empty;
            string fileName = string.Empty;
            string targetFolder = string.Empty;
            StringBuilder oSBErrors = null;
            short importType = -1;
            DataTable dt = null;
            //DataTable dtPeriodEndDates;
            try
            {
                UploadedFile validFile = radFileUpload.UploadedFiles[0];
                ImportFileAttributes ImportFile;
                importType = (short)ARTEnums.DataImportType.ScheduleRecItems;
                //Get folder path and name as per companyid and import type
                targetFolder = SharedDataImportHelper.GetFolderForImport(SessionHelper.CurrentCompanyID.Value, importType);
                MultilingualAttributeInfo oMultilingualAttributeInfo;
                oMultilingualAttributeInfo = LanguageHelper.GetMultilingualAttributeInfo(SessionHelper.CurrentCompanyID, SessionHelper.GetUserLanguage());
                fileName = SharedDataImportHelper.GetFileName(validFile.GetNameWithoutExtension(), validFile.GetExtension(), importType, SessionHelper.CurrentReconciliationPeriodEndDate, oMultilingualAttributeInfo);
                filePath = Path.Combine(targetFolder, fileName);


                //save file
                validFile.SaveAs(filePath, true);

                //Save file attributes, they will be used at the time of importing file records.
                ImportFile.FileOriginalName = validFile.GetName();
                ImportFile.FileModifiedName = fileName;
                ImportFile.FilePhysicalPath = filePath;
                ImportFile.FileSize = validFile.ContentLength;

                decimal? dataStorageCapacity;
                decimal? currentUsage;

                DataImportHelper.GetCompanyDataStorageCapacityAndCurrentUsage(out dataStorageCapacity, out currentUsage);

                if (((decimal)(ImportFile.FileSize) / (decimal)(1024 * 1024)) > (dataStorageCapacity - currentUsage))
                {
                    string exceptionMessage = string.Format(Helper.GetLabelIDValue(5000181), (dataStorageCapacity - currentUsage), dataStorageCapacity);
                    throw new Exception(exceptionMessage);
                }
                else
                {
                    ViewState[ViewStateConstants.IMPORTFILEATTRIBUTES] = ImportFile;
                    //Once file is saved, get records in a DataTable.
                    //Show records in grid.
                    oSBErrors = new StringBuilder();
                    dt = DataImportHelper.GetDataTableFromExcel(filePath, validFile.GetExtension(), importType, oSBErrors);
                    if (dt.Select("IsDuplicate = true").Length > 0)//if duplicate, show message for duplicates
                        Helper.FormatAndShowErrorMessage(this.Page.Master.FindControl("lblErrorMessage") as ExLabel, Helper.GetLabelIDValue(5000043));
                    {
                        rgImportList.PageSize = Convert.ToInt32(hdnNewPageSize.Value);
                        rgImportList.VirtualItemCount = dt.Rows.Count;
                        ViewState["DataTable"] = dt;
                        this.ShowGridAndImportButtons(dt);
                    }
                }
            }
            catch (ARTException ex)
            {
                switch (ex.ExceptionPhraseID)
                {
                    //Invalid File. All Mandatory fields not present
                    //Save import and failure message to database
                    case 5000037:
                        InsertDataImportHdrWithFailureMsg(Helper.GetLabelIDValue(5000037), importType, (short)WebEnums.DataImportStatus.Failure);
                        ViewState.Remove(ViewStateConstants.IMPORTFILEATTRIBUTES);
                        break;
                    //Invalid Data. 
                    //Save import and failure message to database
                    case 5000047:
                        InsertDataImportHdrWithFailureMsg(oSBErrors.ToString(), importType, (short)WebEnums.DataImportStatus.Failure);
                        ViewState.Remove(ViewStateConstants.IMPORTFILEATTRIBUTES);
                        break;
                }
                Helper.ShowErrorMessage(this, ex);
            }
            catch (Exception ex)
            {
                this.DeleteFile(filePath);
                ViewState.Remove(ViewStateConstants.IMPORTFILEATTRIBUTES);
                Helper.ShowErrorMessage(this, ex);
            }
            finally
            {
                this.radFileUpload.UploadedFiles.Clear();
            }
        }
        else
        {
            pnlImportGrid.Visible = false;
        }
    }

    protected void btnImportAll_Click(object sender, EventArgs e)
    {
        if (radFileUpload.UploadedFiles.Count > 0)
        {
            string filePath = string.Empty;
            string fileName = string.Empty;
            string targetFolder = string.Empty;
            StringBuilder oSBErrors = null;
            short importType = -1;
            DataTable dt = null;
            try
            {
                UploadedFile validFile = radFileUpload.UploadedFiles[0];
                ImportFileAttributes ImportFile;
                importType = (short)ARTEnums.DataImportType.ScheduleRecItems;
                //Get folder path and name as per companyid and import type
                targetFolder = SharedDataImportHelper.GetFolderForImport(SessionHelper.CurrentCompanyID.Value, importType);
                MultilingualAttributeInfo oMultilingualAttributeInfo;
                oMultilingualAttributeInfo = LanguageHelper.GetMultilingualAttributeInfo(SessionHelper.CurrentCompanyID, SessionHelper.GetUserLanguage());
                fileName = SharedDataImportHelper.GetFileName(validFile.GetNameWithoutExtension(), validFile.GetExtension(), importType, SessionHelper.CurrentReconciliationPeriodEndDate, oMultilingualAttributeInfo);
                filePath = Path.Combine(targetFolder, fileName);

                //save file
                validFile.SaveAs(filePath, true);

                //Save file attributes, they will be used at the time of importing file records.
                ImportFile.FileOriginalName = validFile.GetName();
                ImportFile.FileModifiedName = fileName;
                ImportFile.FilePhysicalPath = filePath;
                ImportFile.FileSize = validFile.ContentLength;

                decimal? dataStorageCapacity;
                decimal? currentUsage;

                DataImportHelper.GetCompanyDataStorageCapacityAndCurrentUsage(out dataStorageCapacity, out currentUsage);

                if (((decimal)(ImportFile.FileSize) / (decimal)(1024 * 1024)) > (dataStorageCapacity - currentUsage))
                {
                    string exceptionMessage = string.Format(Helper.GetLabelIDValue(5000181), (dataStorageCapacity - currentUsage), dataStorageCapacity);
                    throw new Exception(exceptionMessage);
                }
                else
                {
                    ViewState[ViewStateConstants.IMPORTFILEATTRIBUTES] = ImportFile;
                    //Once file is saved, get records in a DataTable.
                    //Show records in grid.
                    oSBErrors = new StringBuilder();
                    dt = DataImportHelper.GetDataTableFromExcel(filePath, validFile.GetExtension(), importType, oSBErrors);
                    if (dt.Select("IsDuplicate = true").Length > 0)//if duplicate, show message for duplicates
                        Helper.FormatAndShowErrorMessage(this.Page.Master.FindControl("lblErrorMessage") as ExLabel, Helper.GetLabelIDValue(5000043));
                    {
                        rgImportList.PageSize = Convert.ToInt32(hdnNewPageSize.Value);
                        rgImportList.VirtualItemCount = dt.Rows.Count;
                        //ViewState["DataTable"] = dt;
                        //ImportAllUploadData();

                        Session["RecurringDataImportHeaderInfo"] = GetDataImportHdrDetails();

                        DataTable dtValidData = GetValidRows(dt);
                        if (dtValidData.Select("IsValidRow = false").Count() > 0)
                        {
                            Session["DataTableForImport"] = dtValidData;
                            Response.Redirect(GetUrlForRecItemImportStatusPage());
                        }
                        else
                        {
                            ViewState["DataTable"] = dtValidData;
                            ImportAllUploadData();
                        }
                    }
                }
            }
            catch (ARTException ex)
            {
                switch (ex.ExceptionPhraseID)
                {
                    //Invalid File. All Mandatory fields not present
                    //Save import and failure message to database
                    case 5000037:
                        InsertDataImportHdrWithFailureMsg(Helper.GetLabelIDValue(5000037), importType, (short)WebEnums.DataImportStatus.Failure);
                        ViewState.Remove(ViewStateConstants.IMPORTFILEATTRIBUTES);
                        break;
                    //Invalid Data. 
                    //Save import and failure message to database
                    case 5000047:
                        InsertDataImportHdrWithFailureMsg(oSBErrors.ToString(), importType, (short)WebEnums.DataImportStatus.Failure);
                        ViewState.Remove(ViewStateConstants.IMPORTFILEATTRIBUTES);
                        break;
                }
                Helper.ShowErrorMessage(this, ex);
            }
            catch (Exception ex)
            {
                this.DeleteFile(filePath);
                ViewState.Remove(ViewStateConstants.IMPORTFILEATTRIBUTES);
                Helper.ShowErrorMessage(this, ex);
            }
            finally
            {
                this.radFileUpload.UploadedFiles.Clear();
            }
        }
        else
        {
            pnlImportGrid.Visible = false;
        }
    }

    protected void btnUpload_Click(object sender, EventArgs e)
    {
        if (radFileUpload.UploadedFiles.Count > 0)
        {
            string filePath = string.Empty;
            string fileName = string.Empty;
            string targetFolder = string.Empty;
            StringBuilder oSBErrors = null;
            short importType = -1;
            try
            {
                UploadedFile validFile = radFileUpload.UploadedFiles[0];
                ImportFileAttributes ImportFile;
                importType = (short)ARTEnums.DataImportType.ScheduleRecItems;
                //Get folder path and name as per companyid and import type
                targetFolder = SharedDataImportHelper.GetFolderForImport(SessionHelper.CurrentCompanyID.Value, importType);
                MultilingualAttributeInfo oMultilingualAttributeInfo;
                oMultilingualAttributeInfo = LanguageHelper.GetMultilingualAttributeInfo(SessionHelper.CurrentCompanyID, SessionHelper.GetUserLanguage());
                fileName = SharedDataImportHelper.GetFileName(validFile.GetNameWithoutExtension(), validFile.GetExtension(), importType, SessionHelper.CurrentReconciliationPeriodEndDate, oMultilingualAttributeInfo);
                filePath = Path.Combine(targetFolder, fileName);


                //save file
                validFile.SaveAs(filePath, true);

                //Save file attributes, they will be used at the time of importing file records.
                ImportFile.FileOriginalName = validFile.GetName();
                ImportFile.FileModifiedName = fileName;
                ImportFile.FilePhysicalPath = filePath;
                ImportFile.FileSize = validFile.ContentLength;

                decimal? dataStorageCapacity;
                decimal? currentUsage;

                DataImportHelper.GetCompanyDataStorageCapacityAndCurrentUsage(out dataStorageCapacity, out currentUsage);

                if (((decimal)(ImportFile.FileSize) / (decimal)(1024 * 1024)) > (dataStorageCapacity - currentUsage))
                {
                    string exceptionMessage = string.Format(Helper.GetLabelIDValue(5000181), (dataStorageCapacity - currentUsage), dataStorageCapacity);
                    throw new Exception(exceptionMessage);
                }
                else
                {
                    ViewState[ViewStateConstants.IMPORTFILEATTRIBUTES] = ImportFile;
                    string SheetName;
                    SheetName = DataImportHelper.GetSheetName(ARTEnums.DataImportType.ScheduleRecItems, null);
                    if (!SharedDataImportHelper.IsDataSheetPresent(filePath, validFile.GetExtension(), SheetName))
                        throw new Exception(String.Format(Helper.GetLabelIDValue(5000256), SheetName));

                    string errorMessage;
                    if (DataImportHelper.IsAllMandatoryColumnsPresentForDataImport(ARTEnums.DataImportType.ScheduleRecItems, filePath, validFile.GetExtension(),null,SheetName, out errorMessage))
                    {
                        Session["RecurringDataImportHeaderInfo"] = GetDataImportHdrDetails();
                        SubmitFileForProcessing();
                        RefreshDataImportStatusGrid(true);
                    }
                    else
                    {
                        throw new Exception(errorMessage);
                    }
                }
            }
            catch (ARTException ex)
            {
                switch (ex.ExceptionPhraseID)
                {
                    //Invalid File. All Mandatory fields not present
                    //Save import and failure message to database
                    case 5000037:
                        InsertDataImportHdrWithFailureMsg(Helper.GetLabelIDValue(5000037), importType, (short)WebEnums.DataImportStatus.Failure);
                        ViewState.Remove(ViewStateConstants.IMPORTFILEATTRIBUTES);
                        break;
                    //Invalid Data. 
                    //Save import and failure message to database
                    case 5000047:
                        InsertDataImportHdrWithFailureMsg(oSBErrors.ToString(), importType, (short)WebEnums.DataImportStatus.Failure);
                        ViewState.Remove(ViewStateConstants.IMPORTFILEATTRIBUTES);
                        break;
                }
                Helper.ShowErrorMessage(this, ex);
            }
            catch (Exception ex)
            {
                this.DeleteFile(filePath);
                ViewState.Remove(ViewStateConstants.IMPORTFILEATTRIBUTES);
                Helper.ShowErrorMessage(this, ex);
            }
            finally
            {
                this.radFileUpload.UploadedFiles.Clear();
            }
        }
        else
        {
            pnlImportGrid.Visible = false;
        }
    }

    private DataTable GetValidRows(DataTable dt)
    {
        DataTable dtValidTable = ValidateForMandatoryFields(dt);
        return dtValidTable;
    }

    private DataTable ValidateForMandatoryFields(DataTable dtImport)
    {
        DataTable dtTempValidTable = new DataTable();
        dtTempValidTable = dtImport.Clone();
        DataRow[] drValidRows = dtImport.Select();
        if (drValidRows != null)
        {
            dtTempValidTable = drValidRows.CopyToDataTable();
        }

        dtTempValidTable.Columns.Add("ErrorMessage");

        DateTime scheduleBeginDate = DateTime.MinValue;
        DateTime Opendate = DateTime.MinValue;
        DateTime scheduleEndDate = DateTime.MinValue;
        string openDateMandatory = "";
        string scheduleBeginDateDateMandatory = "";
        string scheduleEndDateMandatory = "";
        string invalidOpenDate = "";
        string invalidscheduleBeginDate = "";
        string invalidscheduleEndDate = "";
        string compareOpenDateWithCurrentDate = "";
        string CompareOpenDateAndScheduleEndDates = "";
        string CompareRecPeriodEndDateWithScheduleDates = "";
        if (recCategoryTypeID.HasValue)
        {
            if (recCategoryTypeID == (short)WebEnums.RecCategoryType.Accrual_SupportingDetail_RecurringAccrualSchedule)
            {
                //Accrual Recurring
                openDateMandatory = Helper.GetErrorMessage(WebEnums.FieldType.MandatoryField, 1657);
                scheduleBeginDateDateMandatory = Helper.GetErrorMessage(WebEnums.FieldType.MandatoryField, 1667);
                scheduleEndDateMandatory = Helper.GetErrorMessage(WebEnums.FieldType.MandatoryField, 1668);

                invalidOpenDate = Helper.GetErrorMessage(WebEnums.FieldType.InvalidDate, 1657);
                invalidscheduleBeginDate = Helper.GetErrorMessage(WebEnums.FieldType.InvalidDate, 1667);
                invalidscheduleEndDate = Helper.GetErrorMessage(WebEnums.FieldType.InvalidDate, 1668);

                compareOpenDateWithCurrentDate = Helper.GetErrorMessage(WebEnums.FieldType.DateCompareField, 1657, 2062);
                CompareOpenDateAndScheduleEndDates = Helper.GetErrorMessage(WebEnums.FieldType.DateCompareField, 1657, 1668);
                CompareRecPeriodEndDateWithScheduleDates = LanguageUtil.GetValue(2060);
            }
            else
            {
                //Amortizable
                openDateMandatory = Helper.GetErrorMessage(WebEnums.FieldType.MandatoryField, 1657);
                scheduleBeginDateDateMandatory = Helper.GetErrorMessage(WebEnums.FieldType.MandatoryField, 1670);
                scheduleEndDateMandatory = Helper.GetErrorMessage(WebEnums.FieldType.MandatoryField, 1671);

                invalidOpenDate = Helper.GetErrorMessage(WebEnums.FieldType.InvalidDate, 1657);
                invalidscheduleBeginDate = Helper.GetErrorMessage(WebEnums.FieldType.InvalidDate, 1670);
                invalidscheduleEndDate = Helper.GetErrorMessage(WebEnums.FieldType.InvalidDate, 1671);

                compareOpenDateWithCurrentDate = Helper.GetErrorMessage(WebEnums.FieldType.DateCompareField, 1657, 2062);
                CompareOpenDateAndScheduleEndDates = Helper.GetErrorMessage(WebEnums.FieldType.DateCompareField, 1657, 1671);
                CompareRecPeriodEndDateWithScheduleDates = LanguageUtil.GetValue(2069);
            }
        }

        string errorFormat = Helper.GetLabelIDValue(2495);
        StringBuilder oSBError;
        int rowCount = 0;
        foreach (DataRow dr in dtTempValidTable.Rows)
        {
            oSBError = new StringBuilder();
            rowCount += 1;
            //oSBError.Append("Error in Row" + rowCount + ":");
            //oSBError.Append("<br/>");

            if (dr["Open Date"] == System.DBNull.Value)
            {
                dr["IsValidRow"] = "false";
                //oSBError.Append(string.Format(errorFormat, openDateMandatory));
                oSBError.Append(openDateMandatory);
                oSBError.Append("<br/>");
            }
            else
            {
                //Invalid Open Date
                if (DateTime.TryParse(dr["Open Date"].ToString(), out Opendate))
                {
                    //Open date should be less then current date
                    if (Opendate > Convert.ToDateTime(DateTime.Now.ToShortDateString()))
                    {
                        dr["IsValidRow"] = "false";
                        //oSBError.Append(string.Format(errorFormat, compareOpenDateWithCurrentDate));
                        oSBError.Append(compareOpenDateWithCurrentDate);
                        oSBError.Append("<br/>");
                    }
                }
                else
                {
                    dr["IsValidRow"] = "false";
                    //oSBError.Append(string.Format(errorFormat, invalidOpenDate));
                    oSBError.Append(invalidOpenDate);
                    oSBError.Append("<br/>");
                }
            }

            //Schedule Begin Date
            if (dr["Schedule Begin Date"] == System.DBNull.Value)
            {
                dr["IsValidRow"] = "false";
                //oSBError.Append(string.Format(errorFormat, scheduleBeginDateDateMandatory));
                oSBError.Append(scheduleBeginDateDateMandatory);
                oSBError.Append("<br/>");
            }
            else
            {
                //Invalid Date
                if (DateTime.TryParse(dr["Schedule Begin Date"].ToString(), out scheduleBeginDate))
                {
                }
                else
                {
                    dr["IsValidRow"] = "false";
                    //oSBError.Append(string.Format(errorFormat, invalidscheduleBeginDate));
                    oSBError.Append(invalidscheduleBeginDate);
                    oSBError.Append("<br/>");
                }
            }

            //Schedule End Date
            if (dr["Schedule End Date"] == System.DBNull.Value)
            {
                dr["IsValidRow"] = "false";
                oSBError.Append(scheduleEndDateMandatory);
                oSBError.Append("<br/>");
            }
            else
            {
                //Invalid Date
                if (DateTime.TryParse(dr["Schedule End Date"].ToString(), out scheduleEndDate))
                {
                    //ScheduleEndDate should be greated that OpenDate
                    if ((scheduleEndDate != DateTime.MinValue) && (Opendate != DateTime.MinValue) && scheduleEndDate < Opendate)
                    {
                        dr["IsValidRow"] = "false";
                        oSBError.Append(CompareOpenDateAndScheduleEndDates);
                        oSBError.Append("<br/>");

                    }
                }
                else
                {
                    dr["IsValidRow"] = "false";
                    oSBError.Append(invalidscheduleEndDate);
                    oSBError.Append("<br/>");
                }
            }

            if ((scheduleEndDate != DateTime.MinValue) && (scheduleEndDate < SessionHelper.CurrentReconciliationPeriodEndDate.Value))
            {
                dr["IsValidRow"] = "false";
                oSBError.Append(CompareRecPeriodEndDateWithScheduleDates);
                oSBError.Append("<br/>");

            }
            if ((scheduleBeginDate != DateTime.MinValue) && (scheduleBeginDate > SessionHelper.CurrentReconciliationPeriodEndDate.Value))
            {
                dr["IsValidRow"] = "false";
                oSBError.Append(CompareRecPeriodEndDateWithScheduleDates);
                oSBError.Append("<br/>");
            }


            //Description
            string description = Convert.ToString(dr["Description"]);
            if (!string.IsNullOrEmpty(description))
            {
            }
            else
            {
                dr["IsValidRow"] = "false";
                oSBError.Append(string.Format(errorFormat, Helper.GetLabelIDValue(1408)));
                oSBError.Append("<br/>");
            }

            //ScheduleName
            string scheduleName = Convert.ToString(dr["ScheduleName"]);
            if (!string.IsNullOrEmpty(scheduleName))
            {
            }
            else
            {
                dr["IsValidRow"] = "false";
                oSBError.Append(string.Format(errorFormat, Helper.GetLabelIDValue(2052)));
                oSBError.Append("<br/>");
            }

            //Lccy Code
            string localCurrencyCode = Convert.ToString(dr["L-CCY Code"]);
            if (!string.IsNullOrEmpty(localCurrencyCode))
            {
                try
                {
                    if (localCurrencyCode.Length > 3)
                        throw new ARTException(5000348);
                    try
                    {
                        //if (localCurrencyCode.Length > 3)
                        //    throw new ARTException(5000348);
                        if (this._IsMultiCurrencyEnabled)
                        {
                            if (!String.IsNullOrEmpty(this.CurrentBCCY))
                            {
                                //ExchangeRateInfo oBccyExchangeRateInfo = this.oExchangeRateInfoCollection.Find(r => r.FromCurrencyCode == localCurrencyCode && r.ToCurrencyCode == this.CurrentBCCY);
                                //if (oBccyExchangeRateInfo == null)
                                //    throw new ARTException(5000098);
                                try
                                {
                                    decimal? exRateBCCY = CacheHelper.GetExchangeRate(localCurrencyCode, this.CurrentBCCY, SessionHelper.CurrentReconciliationPeriodID);
                                    if (exRateBCCY == null)
                                        throw new ARTException(5000098);
                                }
                                catch (Exception)
                                {
                                    throw new ARTException(5000098);
                                }
                            }
                        }
                        else
                        {
                            if (!SessionHelper.ReportingCurrencyCode.ToLower().Equals(localCurrencyCode.ToLower())) //reporting currency and local currency from excel import must be same
                                throw new ARTException(5000348);

                        }

                    }
                    catch (ARTException art_ex)
                    {
                        dr["IsValidRow"] = "false";
                        oSBError.Append(Helper.GetLabelIDValue(art_ex.ExceptionPhraseID));
                        oSBError.Append("<br/>");
                    }
                    catch (Exception)
                    {
                        dr["IsValidRow"] = "false";
                        oSBError.Append(Helper.GetLabelIDValue(5000098));
                        oSBError.Append("<br/>");
                    }

                    try
                    {
                        if (this._IsMultiCurrencyEnabled)
                        {
                            //ExchangeRateInfo oRCCYExchangeRateInfo = this.oExchangeRateInfoCollection.Find(r => r.FromCurrencyCode == localCurrencyCode && r.ToCurrencyCode == SessionHelper.ReportingCurrencyCode);
                            //if (oRCCYExchangeRateInfo == null)
                            //    throw new ARTException(5000099);
                            try
                            {
                                decimal? exRateRCCY = CacheHelper.GetExchangeRate(localCurrencyCode, SessionHelper.ReportingCurrencyCode, SessionHelper.CurrentReconciliationPeriodID);
                                if (exRateRCCY == null)
                                    throw new ARTException(5000099);
                            }
                            catch (Exception)
                            {
                                throw new ARTException(5000099);
                            }

                        }
                        //else
                        //{
                        //    if (!SessionHelper.ReportingCurrencyCode.ToLower().Equals(localCurrencyCode.ToLower())) //reporting currency and local currency from excel import must be same
                        //        throw new ARTException(5000348);

                        //}
                    }
                    catch (ARTException art_ex)
                    {
                        dr["IsValidRow"] = "false";
                        oSBError.Append(Helper.GetLabelIDValue(art_ex.ExceptionPhraseID));
                        oSBError.Append("<br/>");
                    }
                    catch (Exception)
                    {
                        dr["IsValidRow"] = "false";
                        oSBError.Append(Helper.GetLabelIDValue(5000099));
                        oSBError.Append("<br/>");
                    }


                    //IUtility oUtilityClient = RemotingHelper.GetUtilityObject();

                    //try
                    //{
                    //    //Bccy Changes
                    //    //decimal baseCurrencyExRate = oUtilityClient.GetCurrencyExchangeRate(SessionHelper.CurrentReconciliationPeriodID.Value
                    //    //    , localCurrencyCode, SessionHelper.BaseCurrencyCode, this._IsMultiCurrencyEnabled);

                    //    decimal baseCurrencyExRate = 1;
                    //    if (!string.IsNullOrEmpty(this.CurrentBCCY))
                    //        baseCurrencyExRate = oUtilityClient.GetCurrencyExchangeRate(SessionHelper.CurrentReconciliationPeriodID.Value
                    //            , localCurrencyCode, this.CurrentBCCY, this._IsMultiCurrencyEnabled);

                    //}
                    //catch (Exception ex)
                    //{
                    //    dr["IsValidRow"] = "false";
                    //    oSBError.Append(Helper.GetLabelIDValue(5000098));
                    //    oSBError.Append("<br/>");
                    //}

                    //try
                    //{
                    //    decimal ReportingCurrencyExRate = oUtilityClient.GetCurrencyExchangeRate(SessionHelper.CurrentReconciliationPeriodID.Value
                    //        , localCurrencyCode, SessionHelper.ReportingCurrencyCode, this._IsMultiCurrencyEnabled);

                    //}
                    //catch (Exception ex)
                    //{
                    //    dr["IsValidRow"] = "false";
                    //    oSBError.Append(Helper.GetLabelIDValue(5000099));
                    //    oSBError.Append("<br/>");
                    //}
                }
                catch (ARTException art_ex)
                {
                    dr["IsValidRow"] = "false";
                    oSBError.Append(Helper.GetLabelIDValue(art_ex.ExceptionPhraseID));
                    oSBError.Append("<br/>");
                }
            }
            else
            {
                dr["IsValidRow"] = "false";
                oSBError.Append(string.Format(errorFormat, Helper.GetLabelIDValue(1773)));
                oSBError.Append("<br/>");
            }

            //Schedule Amount
            decimal amountLocalCurrency = Convert.ToDecimal(dr["Schedule Amount"]);
            if (amountLocalCurrency != 0)
            {
            }
            else
            {
                dr["IsValidRow"] = "false";
                oSBError.Append(string.Format(errorFormat, Helper.GetLabelIDValue(1675)));
                oSBError.Append("<br/>");
            }

            dr["ErrorMessage"] = oSBError.ToString();
        }


        return dtTempValidTable;
    }

    private string GetUrlForRecItemImportStatusPage()
    {
        return "RecItemImportStatusMessage.aspx";
    }

    private void ImportAllUploadData()
    {
        List<GLDataRecurringItemScheduleInfo> oGLDataRecurringItemScheduleInfoCollection = new List<GLDataRecurringItemScheduleInfo>();
        GLDataRecurringItemScheduleInfo oGLDataRecurringItemScheduleInfo = null;
        DataTable dtTempValidTable = (DataTable)ViewState["DataTable"];

        DataTable dtUpload = new DataTable();
        dtUpload = dtTempValidTable.Clone();
        DataRow[] drValidRows = dtTempValidTable.Select("IsValidRow = true");
        if (drValidRows != null)
        {
            dtUpload = drValidRows.CopyToDataTable();
        }


        DataImportHdrInfo oDataImportHrdInfo = null;
        if (Session["RecurringDataImportHeaderInfo"] != null)
        {
            oDataImportHrdInfo = (DataImportHdrInfo)Session["RecurringDataImportHeaderInfo"];
        }
        else
        {
            oDataImportHrdInfo = GetDataImportHdrDetails();
        }

        foreach (DataRow rowpload in dtUpload.Rows)
        {
            oGLDataRecurringItemScheduleInfo = GetGLDataRecItemScheduleDetailForUploadAll(rowpload);
            oGLDataRecurringItemScheduleInfoCollection.Add(oGLDataRecurringItemScheduleInfo);
        }

        IDataImport oDataImportClient = RemotingHelper.GetDataImportObject();
        int rowsAffected;

        oDataImportClient.InsertDataImportGLDataRecItemSchedule(oDataImportHrdInfo, oGLDataRecurringItemScheduleInfoCollection, Helper.GetLabelIDValue((int)WebEnums.DataImportStatusLabelID.Success), out rowsAffected, Helper.GetAppUserInfo());

        Helper.ShowConfirmationMessage(this, Helper.GetLabelIDValue(1608));

        pnlImportGrid.Visible = false;
    }

    private void SubmitFileForProcessing()
    {
        DataImportHdrInfo oDataImportHrdInfo = null;
        if (Session["RecurringDataImportHeaderInfo"] != null)
        {
            oDataImportHrdInfo = (DataImportHdrInfo)Session["RecurringDataImportHeaderInfo"];
        }
        else
        {
            oDataImportHrdInfo = GetDataImportHdrDetails();
        }
        oDataImportHrdInfo.DataImportStatusID = (short)WebEnums.DataImportStatus.Submitted;
        oDataImportHrdInfo.RecordsImported = 0;
        IDataImport oDataImportClient = RemotingHelper.GetDataImportObject();
        int rowsAffected;

        oDataImportClient.InsertDataImportGLDataRecItemSchedule(oDataImportHrdInfo, null, Helper.GetLabelIDValue((int)WebEnums.DataImportStatusLabelID.Submitted), out rowsAffected, Helper.GetAppUserInfo());

        Helper.ShowConfirmationMessage(this, Helper.GetLabelIDValue(2742));
    }

    private GLDataRecurringItemScheduleInfo GetGLDataRecItemScheduleDetailForUploadAll(DataRow rowUpload)
    {
        GLDataRecurringItemScheduleInfo oGLDataRecurringItemScheduleInfo = new GLDataRecurringItemScheduleInfo();
        UserHdrInfo oUserHdrInfo = SessionHelper.GetCurrentUser();
        IUtility oUtilityClient = RemotingHelper.GetUtilityObject();

        oGLDataRecurringItemScheduleInfo.AddedBy = oUserHdrInfo.LoginID;
        oGLDataRecurringItemScheduleInfo.AddedByUserID = oUserHdrInfo.UserID;

        decimal lblScheduleAmountvalue = Convert.ToDecimal(rowUpload["Schedule Amount"]);
        oGLDataRecurringItemScheduleInfo.LocalCurrencyCode = Convert.ToString(rowUpload["L-CCY Code"]);


        oGLDataRecurringItemScheduleInfo.ScheduleBeginDate = Convert.ToDateTime(rowUpload["Schedule Begin Date"]);

        oGLDataRecurringItemScheduleInfo.OpenDate = Convert.ToDateTime(rowUpload["Open Date"]);

        oGLDataRecurringItemScheduleInfo.ScheduleEndDate = Convert.ToDateTime(rowUpload["Schedule End Date"]);
        oGLDataRecurringItemScheduleInfo.ScheduleAmount = lblScheduleAmountvalue;
        //SetExchangeRates(oGLDataRecurringItemScheduleInfo.LocalCurrencyCode);

        string localCurrencyCode = (string)rowUpload["L-CCY Code"];
        decimal? baseCurrencyExRate = null;
        decimal reportingCurrencyExRate = 1;

        if (!string.IsNullOrEmpty(localCurrencyCode))
        {
            if (this._IsMultiCurrencyEnabled)
            {
                if (!String.IsNullOrEmpty(this.CurrentBCCY))
                {
                    //ExchangeRateInfo bccyExchangeRateInfo = this.oExchangeRateInfoCollection.Find(r => r.FromCurrencyCode == localCurrencyCode && r.ToCurrencyCode == this.CurrentBCCY);
                    //if (bccyExchangeRateInfo != null)
                    //    baseCurrencyExRate = bccyExchangeRateInfo.ExchangeRate.Value;
                    decimal? exRateBCCY = CacheHelper.GetExchangeRate(localCurrencyCode, this.CurrentBCCY, SessionHelper.CurrentReconciliationPeriodID);
                    if (exRateBCCY.HasValue)
                        baseCurrencyExRate = exRateBCCY.Value;
                }
                else
                    baseCurrencyExRate = null;

                //ExchangeRateInfo rccyExchangeRateInfo = this.oExchangeRateInfoCollection.Find(r => r.FromCurrencyCode == localCurrencyCode && r.ToCurrencyCode == SessionHelper.ReportingCurrencyCode);
                //if (rccyExchangeRateInfo != null)
                //    reportingCurrencyExRate = rccyExchangeRateInfo.ExchangeRate.Value;
                decimal? exRateRCCY = CacheHelper.GetExchangeRate(localCurrencyCode, SessionHelper.ReportingCurrencyCode, SessionHelper.CurrentReconciliationPeriodID);
                if (exRateRCCY.HasValue)
                    reportingCurrencyExRate = exRateRCCY.Value;
            }
            else
            {
                if (!String.IsNullOrEmpty(this.CurrentBCCY))
                {
                    baseCurrencyExRate = 1;
                }
            }
            //try
            //{
            //    if (!string.IsNullOrEmpty(this.CurrentBCCY))
            //        baseCurrencyExRate = oUtilityClient.GetCurrencyExchangeRate(SessionHelper.CurrentReconciliationPeriodID.Value
            //            , localCurrencyCode, this.CurrentBCCY, this._IsMultiCurrencyEnabled);
            //}
            //catch (Exception ex)
            //{
            //}

            //try
            //{
            //    reportingCurrencyExRate = oUtilityClient.GetCurrencyExchangeRate(SessionHelper.CurrentReconciliationPeriodID.Value
            //        , localCurrencyCode, SessionHelper.ReportingCurrencyCode, this._IsMultiCurrencyEnabled);
            //}
            //catch (Exception ex)
            //{
            //}
        }

        //if (!this._NetAccountID.HasValue || this._NetAccountID.Value == 0)
        if (baseCurrencyExRate.HasValue)
            oGLDataRecurringItemScheduleInfo.ScheduleAmountBaseCurrency = Math.Round(lblScheduleAmountvalue * baseCurrencyExRate.Value, 2);

        decimal reportingCurrencyExchangeRate = reportingCurrencyExRate;
        oGLDataRecurringItemScheduleInfo.ScheduleAmountReportingCurrency = Math.Round(lblScheduleAmountvalue * reportingCurrencyExchangeRate, 2);

        decimal noOfDaysInSchedule = oGLDataRecurringItemScheduleInfo.ScheduleEndDate.Value.Subtract(oGLDataRecurringItemScheduleInfo.ScheduleBeginDate.Value).Days + 1;
        decimal noOfDaysPassed = SessionHelper.CurrentReconciliationPeriodEndDate.Value.Subtract(oGLDataRecurringItemScheduleInfo.ScheduleBeginDate.Value).Days + 1;
        decimal recPeriodAmountLCCY = 0;

        if (noOfDaysInSchedule > 0 && noOfDaysPassed > 0)
        {
            recPeriodAmountLCCY = oGLDataRecurringItemScheduleInfo.ScheduleAmount.Value * (noOfDaysPassed / noOfDaysInSchedule);
        }
        else { }

        oGLDataRecurringItemScheduleInfo.RecPeriodAmountLocalCurrency = Math.Round(recPeriodAmountLCCY, TestConstant.DECIMAL_PLACES_FOR_MATH_ROUND);

        if (!String.IsNullOrEmpty(this.CurrentBCCY))
            oGLDataRecurringItemScheduleInfo.RecPeriodAmountBaseCurrency = Math.Round(recPeriodAmountLCCY * baseCurrencyExRate.Value, TestConstant.DECIMAL_PLACES_FOR_MATH_ROUND);

        oGLDataRecurringItemScheduleInfo.RecPeriodAmountReportingCurrency = Math.Round(recPeriodAmountLCCY * reportingCurrencyExchangeRate, TestConstant.DECIMAL_PLACES_FOR_MATH_ROUND);

        oGLDataRecurringItemScheduleInfo.BalanceLocalCurrency = Math.Round((oGLDataRecurringItemScheduleInfo.ScheduleAmount.Value - recPeriodAmountLCCY), TestConstant.DECIMAL_PLACES_FOR_MATH_ROUND);
        if (!String.IsNullOrEmpty(this.CurrentBCCY))
            oGLDataRecurringItemScheduleInfo.BalanceBaseCurrency = Math.Round(oGLDataRecurringItemScheduleInfo.BalanceLocalCurrency.Value * baseCurrencyExRate.Value, TestConstant.DECIMAL_PLACES_FOR_MATH_ROUND);

        oGLDataRecurringItemScheduleInfo.BalanceReportingCurrency = Math.Round(oGLDataRecurringItemScheduleInfo.BalanceLocalCurrency.Value * reportingCurrencyExchangeRate, TestConstant.DECIMAL_PLACES_FOR_MATH_ROUND);

        oGLDataRecurringItemScheduleInfo.Comments = Convert.ToString(rowUpload["Description"]);
        oGLDataRecurringItemScheduleInfo.DateAdded = DateTime.Now;
        oGLDataRecurringItemScheduleInfo.GLDataID = this._GLDataID;
        oGLDataRecurringItemScheduleInfo.ScheduleName = Convert.ToString(rowUpload["ScheduleName"]);
        oGLDataRecurringItemScheduleInfo.IsActive = true;
        if (Request.QueryString[QueryStringConstants.REC_CATEGORY_TYPE_ID] != null)
            oGLDataRecurringItemScheduleInfo.ReconciliationCategoryTypeID = _RecCategoryTypeID;

        oGLDataRecurringItemScheduleInfo.CalculationFrequencyID = (short)ARTEnums.CalculationFrequency.DailyInterval;
        oGLDataRecurringItemScheduleInfo.TotalIntervals = null;
        oGLDataRecurringItemScheduleInfo.CurrentInterval = null;
        oGLDataRecurringItemScheduleInfo.ReconciliationCategoryTypeID = this._RecCategoryTypeID;
        oGLDataRecurringItemScheduleInfo.RecCategoryTypeID = this._RecCategoryTypeID;
        oGLDataRecurringItemScheduleInfo.RecordSourceTypeID = (short)ARTEnums.RecordSourceType.DataImport;
        return oGLDataRecurringItemScheduleInfo;
    }

    protected void rgImportList_PageIndexChanged(object source, GridPageChangedEventArgs e)
    {
        Sel.Value = string.Empty;
    }


    protected void rgImportList_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {
        if (e.Item.ItemType == Telerik.Web.UI.GridItemType.Item ||
            e.Item.ItemType == Telerik.Web.UI.GridItemType.AlternatingItem)
        {
            ExLabel lblScheduleName = (ExLabel)e.Item.FindControl("lblScheduleName");
            ExLabel lblScheduleBeginDate = (ExLabel)e.Item.FindControl("lblScheduleBeginDate");
            ExLabel lblOpenDate = (ExLabel)e.Item.FindControl("lblOpenDate");
            ExLabel lblSceduleEndDate = (ExLabel)e.Item.FindControl("lblSceduleEndDate");
            ExLabel lblDescription = (ExLabel)e.Item.FindControl("lblDescription");
            ExLabel lblLocalCurrencyCode = (ExLabel)e.Item.FindControl("lblLocalCurrencyCode");
            ExLabel ScheduleAmount = (ExLabel)e.Item.FindControl("lblScheduleAmount");
            ExLabel lblRefNo = (ExLabel)e.Item.FindControl("lblRefNo");
            ExLabel lblError = (ExLabel)e.Item.FindControl("lblError");
            //CheckBox checkBox = (CheckBox)(e.Item as GridDataItem)["CheckboxSelectColumn"].Controls[0];

            string openDateMandatory = "";
            string scheduleBeginDateDateMandatory = "";
            string scheduleEndDateMandatory = "";
            string invalidOpenDate = "";
            string invalidscheduleBeginDate = "";
            string invalidscheduleEndDate = "";
            string compareOpenDateWithCurrentDate = "";
            string CompareOpenDateAndScheduleEndDates = "";
            string CompareRecPeriodEndDateWithScheduleDates = "";
            if (recCategoryTypeID.HasValue)
            {
                if (recCategoryTypeID == (short)WebEnums.RecCategoryType.Accrual_SupportingDetail_RecurringAccrualSchedule)
                {
                    //Accrual Recurring
                    openDateMandatory = Helper.GetErrorMessage(WebEnums.FieldType.MandatoryField, 1657);
                    scheduleBeginDateDateMandatory = Helper.GetErrorMessage(WebEnums.FieldType.MandatoryField, 1667);
                    scheduleEndDateMandatory = Helper.GetErrorMessage(WebEnums.FieldType.MandatoryField, 1668);

                    invalidOpenDate = Helper.GetErrorMessage(WebEnums.FieldType.InvalidDate, 1657);
                    invalidscheduleBeginDate = Helper.GetErrorMessage(WebEnums.FieldType.InvalidDate, 1667);
                    invalidscheduleEndDate = Helper.GetErrorMessage(WebEnums.FieldType.InvalidDate, 1668);

                    compareOpenDateWithCurrentDate = Helper.GetErrorMessage(WebEnums.FieldType.DateCompareField, 1657, 2062);
                    CompareOpenDateAndScheduleEndDates = Helper.GetErrorMessage(WebEnums.FieldType.DateCompareField, 1657, 1668);
                    CompareRecPeriodEndDateWithScheduleDates = LanguageUtil.GetValue(2060);
                }
                else
                {
                    //Amortizable
                    openDateMandatory = Helper.GetErrorMessage(WebEnums.FieldType.MandatoryField, 1657);
                    scheduleBeginDateDateMandatory = Helper.GetErrorMessage(WebEnums.FieldType.MandatoryField, 1670);
                    scheduleEndDateMandatory = Helper.GetErrorMessage(WebEnums.FieldType.MandatoryField, 1671);

                    invalidOpenDate = Helper.GetErrorMessage(WebEnums.FieldType.InvalidDate, 1657);
                    invalidscheduleBeginDate = Helper.GetErrorMessage(WebEnums.FieldType.InvalidDate, 1670);
                    invalidscheduleEndDate = Helper.GetErrorMessage(WebEnums.FieldType.InvalidDate, 1671);

                    compareOpenDateWithCurrentDate = Helper.GetErrorMessage(WebEnums.FieldType.DateCompareField, 1657, 2062);
                    CompareOpenDateAndScheduleEndDates = Helper.GetErrorMessage(WebEnums.FieldType.DateCompareField, 1657, 1671);
                    CompareRecPeriodEndDateWithScheduleDates = LanguageUtil.GetValue(2069);
                }
            }


            StringBuilder oSBError = new StringBuilder();
            string errorFormat = Helper.GetLabelIDValue(1774);

            DataRow dr = ((DataRowView)e.Item.DataItem).Row;

            DateTime scheduleBeginDate = DateTime.MinValue;
            DateTime Opendate = DateTime.MinValue;
            DateTime scheduleEndDate = DateTime.MinValue;


            //Open Date
            //Mandatory
            if (dr["Open Date"] == System.DBNull.Value)
            {
                isError = true;
                //oSBError.Append(string.Format(errorFormat, openDateMandatory));
                oSBError.Append(openDateMandatory);
                oSBError.Append("<br/>");
            }
            else
            {
                //Invalid Open Date
                if (DateTime.TryParse(dr["Open Date"].ToString(), out Opendate))
                {
                    lblOpenDate.Text = Helper.GetDisplayDate(Opendate);
                    //Open date should be less then current date
                    if (Opendate > Convert.ToDateTime(DateTime.Now.ToShortDateString()))
                    {
                        isError = true;
                        //oSBError.Append(string.Format(errorFormat, compareOpenDateWithCurrentDate));
                        oSBError.Append(compareOpenDateWithCurrentDate);
                        oSBError.Append("<br/>");
                    }
                }
                else
                {
                    isError = true;

                    //oSBError.Append(string.Format(errorFormat, invalidOpenDate));
                    oSBError.Append(invalidOpenDate);
                    oSBError.Append("<br/>");
                }
            }

            //Schedule Begin Date
            if (dr["Schedule Begin Date"] == System.DBNull.Value)
            {
                isError = true;
                //oSBError.Append(string.Format(errorFormat, scheduleBeginDateDateMandatory));
                oSBError.Append(scheduleBeginDateDateMandatory);
                oSBError.Append("<br/>");
            }
            else
            {
                //Invalid Date
                if (DateTime.TryParse(dr["Schedule Begin Date"].ToString(), out scheduleBeginDate))
                {
                    lblScheduleBeginDate.Text = Helper.GetDisplayDate(scheduleBeginDate);

                }
                else
                {
                    isError = true;
                    //oSBError.Append(string.Format(errorFormat, invalidscheduleBeginDate));
                    oSBError.Append(invalidscheduleBeginDate);
                    oSBError.Append("<br/>");
                }
                lblScheduleBeginDate.Text = Helper.GetDisplayDate(scheduleBeginDate);
            }

            //Schedule End Date
            if (dr["Schedule End Date"] == System.DBNull.Value)
            {
                isError = true;
                //oSBError.Append(string.Format(errorFormat, scheduleEndDateMandatory));
                oSBError.Append(scheduleEndDateMandatory);
                oSBError.Append("<br/>");
            }
            else
            {
                //Invalid Date
                if (DateTime.TryParse(dr["Schedule End Date"].ToString(), out scheduleEndDate))
                {
                    lblSceduleEndDate.Text = Helper.GetDisplayDate(scheduleEndDate);
                    //ScheduleEndDate should be greated that OpenDate
                    if ((scheduleEndDate != DateTime.MinValue) && (Opendate != DateTime.MinValue) && scheduleEndDate < Opendate)
                    {
                        isError = true;
                        oSBError.Append(CompareOpenDateAndScheduleEndDates);
                        oSBError.Append("<br/>");

                    }
                }
                else
                {
                    isError = true;
                    //oSBError.Append(string.Format(errorFormat, invalidscheduleEndDate));
                    oSBError.Append(invalidscheduleEndDate);
                    oSBError.Append("<br/>");
                }
                lblSceduleEndDate.Text = Helper.GetDisplayDate(scheduleEndDate);
            }

            if ((scheduleEndDate != DateTime.MinValue) && (scheduleEndDate < SessionHelper.CurrentReconciliationPeriodEndDate.Value))
            {
                isError = true;
                //oSBError.Append(string.Format(errorFormat, CompareRecPeriodEndDateWithScheduleDates));
                oSBError.Append(CompareRecPeriodEndDateWithScheduleDates);
                oSBError.Append("<br/>");

            }
            if ((scheduleBeginDate != DateTime.MinValue) && (scheduleBeginDate > SessionHelper.CurrentReconciliationPeriodEndDate.Value))
            {
                isError = true;
                //oSBError.Append(string.Format(errorFormat, CompareRecPeriodEndDateWithScheduleDates));
                oSBError.Append(CompareRecPeriodEndDateWithScheduleDates);
                oSBError.Append("<br/>");
            }


            //Description
            string description = (string)DataBinder.Eval(e.Item.DataItem, "Description");
            if (!string.IsNullOrEmpty(description))
            {
                lblDescription.Text = (string)DataBinder.Eval(e.Item.DataItem, "Description");
            }
            else
            {
                isError = true;
                oSBError.Append(string.Format(errorFormat, Helper.GetLabelIDValue(1408)));
                oSBError.Append("<br/>");
            }

            //ScheduleName
            string scheduleName = (string)DataBinder.Eval(e.Item.DataItem, "ScheduleName");
            if (!string.IsNullOrEmpty(scheduleName))
            {
                lblScheduleName.Text = (string)DataBinder.Eval(e.Item.DataItem, "ScheduleName");
            }
            else
            {
                isError = true;
                oSBError.Append(string.Format(errorFormat, Helper.GetLabelIDValue(2052)));
                oSBError.Append("<br/>");
            }

            //Lccy Code
            string localCurrencyCode = (string)DataBinder.Eval(e.Item.DataItem, "L-CCY Code");
            if (!string.IsNullOrEmpty(localCurrencyCode))
            {
                lblLocalCurrencyCode.Text = localCurrencyCode;
                IUtility oUtilityClient = RemotingHelper.GetUtilityObject();
                try
                {
                    if (localCurrencyCode.Length > 3)
                        throw new ARTException(5000348);
                    try
                    {
                        //if (localCurrencyCode.Length > 3)
                        //    throw new ARTException(5000348);
                        decimal? baseCurrencyExRate = null;
                        if (this._IsMultiCurrencyEnabled)
                        {
                            if (!String.IsNullOrEmpty(this.CurrentBCCY))
                            {
                                //ExchangeRateInfo oBCCYExchangeRateInfo = this.oExchangeRateInfoCollection.Find(r => r.FromCurrencyCode == localCurrencyCode && r.ToCurrencyCode == this.CurrentBCCY);
                                //if (oBCCYExchangeRateInfo != null)
                                //    baseCurrencyExRate = oBCCYExchangeRateInfo.ExchangeRate.Value;
                                //else
                                //{
                                //    throw new ARTException(5000098);
                                //}
                                try
                                {
                                    decimal? exRateBCCY = CacheHelper.GetExchangeRate(localCurrencyCode, this.CurrentBCCY, SessionHelper.CurrentReconciliationPeriodID);
                                    if (exRateBCCY.HasValue)
                                        baseCurrencyExRate = exRateBCCY.Value;
                                    else
                                    {
                                        throw new ARTException(5000098);
                                    }
                                }
                                catch (Exception)
                                {
                                    throw new ARTException(5000098);
                                }
                            }
                        }
                        else
                        {
                            if (!SessionHelper.ReportingCurrencyCode.ToLower().Equals(localCurrencyCode.ToLower())) //reporting currency and local currency from excel import must be same
                                throw new ARTException(5000348);
                            if (!String.IsNullOrEmpty(this.CurrentBCCY))
                                baseCurrencyExRate = 1;
                        }
                        (e.Item as GridDataItem)["BaseCurrencyExchangeRate"].Text = baseCurrencyExRate.ToString();
                    }
                    catch (ARTException art_ex)
                    {
                        isError = true;
                        oSBError.Append(Helper.GetLabelIDValue(art_ex.ExceptionPhraseID));
                        oSBError.Append("<br/>");
                    }
                    catch (Exception)
                    {
                        isError = true;
                        oSBError.Append(Helper.GetLabelIDValue(5000098));
                        oSBError.Append("<br/>");
                    }


                    try
                    {
                        decimal reportingCurrencyExRate = 1;
                        if (this._IsMultiCurrencyEnabled)
                        {
                            //ExchangeRateInfo oBCCYExchangeRateInfo = this.oExchangeRateInfoCollection.Find(r => r.FromCurrencyCode == localCurrencyCode && r.ToCurrencyCode == SessionHelper.ReportingCurrencyCode);
                            //if (oBCCYExchangeRateInfo != null)
                            //    reportingCurrencyExRate = oBCCYExchangeRateInfo.ExchangeRate.Value;
                            //else
                            //    throw new ARTException(5000099);
                            try
                            {
                                decimal? exRateRCCY = CacheHelper.GetExchangeRate(localCurrencyCode, SessionHelper.ReportingCurrencyCode, SessionHelper.CurrentReconciliationPeriodID);
                                if (exRateRCCY.HasValue)
                                    reportingCurrencyExRate = exRateRCCY.Value;
                                else
                                    throw new ARTException(5000099);
                            }
                            catch (Exception)
                            {
                                throw new ARTException(5000099);
                            }
                        }
                        //else
                        //{
                        //    if (!SessionHelper.ReportingCurrencyCode.ToLower().Equals(localCurrencyCode.ToLower())) //reporting currency and local currency from excel import must be same
                        //        throw new ARTException(5000348);

                        //}
                        (e.Item as GridDataItem)["ReportingCurrencyExchangeRate"].Text = reportingCurrencyExRate.ToString();
                    }
                    catch (ARTException art_ex)
                    {
                        isError = true;
                        oSBError.Append(Helper.GetLabelIDValue(art_ex.ExceptionPhraseID));
                        oSBError.Append("<br/>");
                    }
                    catch (Exception)
                    {
                        isError = true;
                        oSBError.Append(Helper.GetLabelIDValue(5000099));
                        oSBError.Append("<br/>");
                    }

                    //try
                    //{
                    //    //Bccy Changes
                    //    //decimal baseCurrencyExRate = oUtilityClient.GetCurrencyExchangeRate(SessionHelper.CurrentReconciliationPeriodID.Value
                    //    //    , localCurrencyCode, SessionHelper.BaseCurrencyCode, this._IsMultiCurrencyEnabled);

                    //    decimal baseCurrencyExRate = 1;
                    //    if (!string.IsNullOrEmpty(this.CurrentBCCY))
                    //        baseCurrencyExRate = oUtilityClient.GetCurrencyExchangeRate(SessionHelper.CurrentReconciliationPeriodID.Value
                    //            , localCurrencyCode, this.CurrentBCCY, this._IsMultiCurrencyEnabled);

                    //    (e.Item as GridDataItem)["BaseCurrencyExchangeRate"].Text = baseCurrencyExRate.ToString();
                    //}
                    //catch (Exception ex)
                    //{
                    //    isError = true;
                    //    oSBError.Append(Helper.GetLabelIDValue(5000098));
                    //    oSBError.Append("<br/>");
                    //}

                    //try
                    //{
                    //    decimal ReportingCurrencyExRate = oUtilityClient.GetCurrencyExchangeRate(SessionHelper.CurrentReconciliationPeriodID.Value
                    //        , localCurrencyCode, SessionHelper.ReportingCurrencyCode, this._IsMultiCurrencyEnabled);

                    //    (e.Item as GridDataItem)["ReportingCurrencyExchangeRate"].Text = ReportingCurrencyExRate.ToString();
                    //}
                    //catch (Exception ex)
                    //{
                    //    isError = true;
                    //    oSBError.Append(Helper.GetLabelIDValue(5000099));
                    //    oSBError.Append("<br/>");
                    //}
                }
                catch (ARTException art_ex)
                {
                    isError = true;
                    oSBError.Append(Helper.GetLabelIDValue(art_ex.ExceptionPhraseID));
                    oSBError.Append("<br/>");
                }
            }
            else
            {
                isError = true;
                oSBError.Append(string.Format(errorFormat, Helper.GetLabelIDValue(1773)));
                oSBError.Append("<br/>");
            }

            //Schedule Amount
            decimal amountLocalCurrency = (decimal)DataBinder.Eval(e.Item.DataItem, "Schedule Amount");
            if (amountLocalCurrency != 0)
            {
                ScheduleAmount.Text = Helper.GetDisplayDecimalValue(amountLocalCurrency);
            }
            else
            {
                isError = true;
                oSBError.Append(string.Format(errorFormat, Helper.GetLabelIDValue(1675)));
                oSBError.Append("<br/>");
            }

            //Ref No
            string refNumber = (string)DataBinder.Eval(e.Item.DataItem, "Ref No");
            if (!string.IsNullOrEmpty(refNumber))
            {
                lblRefNo.Text = refNumber;
            }

            lblError.Text = oSBError.ToString();

            if (!string.IsNullOrEmpty(oSBError.ToString()))
            {
                //checkBox.Enabled = false;
                Sel.Value += e.Item.ItemIndex.ToString() + ":";
            }
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        //rowHide.Visible = false;
        rgImportList.MasterTableView.DataSource = null;
        rgImportList.DataBind();

    }

    protected void btnPageCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(ReturnUrl, true);
    }

    protected void btnImport_Click(object sender, EventArgs e)
    {
        try
        {
            List<GLDataRecurringItemScheduleInfo> oGLDataRecurringItemScheduleInfoCollection = new List<GLDataRecurringItemScheduleInfo>();
            GLDataRecurringItemScheduleInfo oGLDataRecurringItemScheduleInfo = null;

            DataImportHdrInfo oDataImportHrdInfo = GetDataImportHdrDetails();

            foreach (GridDataItem item in rgImportList.SelectedItems)
            {
                oGLDataRecurringItemScheduleInfo = GetGLDataRecItemScheduleDetail(item);
                oGLDataRecurringItemScheduleInfoCollection.Add(oGLDataRecurringItemScheduleInfo);
            }

            IDataImport oDataImportClient = RemotingHelper.GetDataImportObject();
            int rowsAffected;

            oDataImportClient.InsertDataImportGLDataRecItemSchedule(oDataImportHrdInfo, oGLDataRecurringItemScheduleInfoCollection, Helper.GetLabelIDValue((int)WebEnums.DataImportStatusLabelID.Success), out rowsAffected, Helper.GetAppUserInfo());

            Helper.ShowConfirmationMessage(this, Helper.GetLabelIDValue(1608));

            pnlImportGrid.Visible = false;
        }
        catch (Exception ex)
        {
            Helper.ShowErrorMessage(this, ex);
        }
    }

    protected void cvFileUpload_ServerValidate(object source, ServerValidateEventArgs args)
    {
        if (this.radFileUpload.InvalidFiles.Count > 0)
        {
            //Set default error message
            this.cvFileUpload.ErrorMessage = LanguageUtil.GetValue(5000037);

            //Check if there is enough space for this company to save this file
            UploadedFile invalidFile = radFileUpload.InvalidFiles[0];
            if (invalidFile.ContentLength > DataImportHelper.GetAvailableDataStorageByCompanyId(SessionHelper.CurrentCompanyID.Value))
            {
                this.cvFileUpload.ErrorMessage = LanguageUtil.GetValue(5000034);
                //throw new ARTException(5000034);//File size exceeds available space
            }
            //File size exceeds maximum file upload size.
            if (invalidFile.ContentLength > DataImportHelper.GetAllowedMaximumFileSize(SessionHelper.CurrentCompanyID.Value))
            {
                this.cvFileUpload.ErrorMessage = LanguageUtil.GetValue(5000038);//File size is more than maximum allowed file size
            }
            //file extension validation 
            string[] arr = DataImportHelper.GetAllowedFileExtensions();
            string invalidFileExtension = invalidFile.GetExtension();
            bool valid = false;
            foreach (string str in arr)
            {
                if (str == invalidFileExtension)
                {
                    valid = true;
                    break;
                }
            }
            if (!valid)
                this.cvFileUpload.ErrorMessage = LanguageUtil.GetValue(5000036);//Invalid file extension 
            args.IsValid = false;

            //disable import and cancel buttons and remove grid
            pnlImportGrid.Visible = false;
        }

    }

    protected void Import_To_Grid(string FilePath, string Extension)
    {
        DataTable dt = new DataTable();

        dt = DataImportHelper.ImportToGrid(FilePath, Extension);
        rgImportList.MasterTableView.DataSource = dt;
        rgImportList.DataBind();

        GridColumn oGridColumn = this.rgImportList.Columns.FindByUniqueNameSafe("Error");

        if (isError == false)
        {
            if (oGridColumn != null)
                oGridColumn.Visible = false;
            //this.rgImportList.DataBind();
        }
        else
        {
            if (oGridColumn != null)
                oGridColumn.Visible = true;
        }
    }
    #endregion


    #region "Private Methods"

    private void EnableDisableControls()
    {
        if (Helper.GetFormMode(WebEnums.ARTPages.OSDepositBankNsfImport, this.GLDataHdrInfo) == WebEnums.FormMode.Edit)
        {
            txtImportName.Enabled = true;
            radFileUpload.Enabled = true;
            btnUpload.Enabled = true;
        }
        else
        {
            txtImportName.Enabled = false;
            radFileUpload.Enabled = false;
            btnCancel.Enabled = false;
            btnPreview.Enabled = false;
            btnImportAll.Enabled = false;
            btnUpload.Enabled = false;
        }
    }

    private void InsertDataImportHdrWithFailureMsg(string failureMsg, short dataImportTypeID, short dataImportStatusID)
    {
        IDataImport oDataImport = RemotingHelper.GetDataImportObject();
        DataImportHdrInfo oDataImportHrdInfo = new DataImportHdrInfo();
        ImportFileAttributes file = new ImportFileAttributes();
        if (ViewState[ViewStateConstants.IMPORTFILEATTRIBUTES] != null)
        {
            file = (ImportFileAttributes)ViewState[ViewStateConstants.IMPORTFILEATTRIBUTES];
            ViewState.Remove(ViewStateConstants.IMPORTFILEATTRIBUTES);
        }
        oDataImportHrdInfo.DataImportName = this.txtImportName.Text;
        oDataImportHrdInfo.FileName = file.FileOriginalName;
        oDataImportHrdInfo.PhysicalPath = file.FilePhysicalPath;
        oDataImportHrdInfo.FileSize = file.FileSize;
        oDataImportHrdInfo.CompanyID = SessionHelper.CurrentCompanyID;
        oDataImportHrdInfo.DataImportTypeID = dataImportTypeID;
        oDataImportHrdInfo.DataImportStatusID = dataImportStatusID;
        oDataImportHrdInfo.RecordsImported = 0;
        if (dataImportTypeID == (short)ARTEnums.DataImportType.GLData)
            oDataImportHrdInfo.ReconciliationPeriodID = SessionHelper.CurrentReconciliationPeriodID;
        oDataImportHrdInfo.IsActive = true;
        oDataImportHrdInfo.DateAdded = DateTime.Now;
        oDataImportHrdInfo.AddedBy = SessionHelper.GetCurrentUser().LoginID;
        oDataImportHrdInfo.ReconciliationPeriodID = SessionHelper.CurrentReconciliationPeriodID;
        oDataImportHrdInfo.RoleID = SessionHelper.CurrentRoleID;
        oDataImportHrdInfo.LanguageID = SessionHelper.GetUserLanguage();

        oDataImportHrdInfo.DataImportRecItemUploadInfo = new DataImportRecItemUploadInfo();
        oDataImportHrdInfo.DataImportRecItemUploadInfo.GLDataID = this._GLDataID;
        oDataImportHrdInfo.DataImportRecItemUploadInfo.ReconciliationCategoryID = this._RecCategoryID;
        oDataImportHrdInfo.DataImportRecItemUploadInfo.ReconciliationCategoryTypeID = this._RecCategoryTypeID;

        oDataImport.InsertDataImportWithFailureMsg(oDataImportHrdInfo, failureMsg, Helper.GetAppUserInfo());
    }

    private DataImportHdrInfo GetDataImportHdrDetails()
    {
        DataImportHdrInfo oDataImportHrdInfo = new DataImportHdrInfo();
        ImportFileAttributes file = new ImportFileAttributes();
        if (ViewState[ViewStateConstants.IMPORTFILEATTRIBUTES] != null)
        {
            file = (ImportFileAttributes)ViewState[ViewStateConstants.IMPORTFILEATTRIBUTES];
            ViewState.Remove(ViewStateConstants.IMPORTFILEATTRIBUTES);
        }
        oDataImportHrdInfo.DataImportName = this.txtImportName.Text;
        oDataImportHrdInfo.FileName = file.FileOriginalName;
        oDataImportHrdInfo.PhysicalPath = file.FilePhysicalPath;
        oDataImportHrdInfo.FileSize = file.FileSize;
        oDataImportHrdInfo.CompanyID = SessionHelper.CurrentCompanyID;
        oDataImportHrdInfo.DataImportTypeID = (short)ARTEnums.DataImportType.ScheduleRecItems;
        oDataImportHrdInfo.DataImportStatusID = (short)WebEnums.DataImportStatus.Success;
        oDataImportHrdInfo.RecordsImported = rgImportList.SelectedItems.Count;
        oDataImportHrdInfo.IsActive = true;
        oDataImportHrdInfo.DateAdded = DateTime.Now;
        oDataImportHrdInfo.AddedBy = SessionHelper.GetCurrentUser().LoginID;
        oDataImportHrdInfo.ReconciliationPeriodID = SessionHelper.CurrentReconciliationPeriodID;
        oDataImportHrdInfo.RoleID = SessionHelper.CurrentRoleID;
        oDataImportHrdInfo.LanguageID = SessionHelper.GetUserLanguage();

        oDataImportHrdInfo.DataImportRecItemUploadInfo = new DataImportRecItemUploadInfo();
        oDataImportHrdInfo.DataImportRecItemUploadInfo.GLDataID = this._GLDataID;
        oDataImportHrdInfo.DataImportRecItemUploadInfo.ReconciliationCategoryID = this._RecCategoryID;
        oDataImportHrdInfo.DataImportRecItemUploadInfo.ReconciliationCategoryTypeID = this._RecCategoryTypeID;

        return oDataImportHrdInfo;
    }

    private GLDataRecurringItemScheduleInfo GetGLDataRecItemScheduleDetail(GridDataItem item)
    {
        GLDataRecurringItemScheduleInfo oGLDataRecurringItemScheduleInfo = new GLDataRecurringItemScheduleInfo();
        UserHdrInfo oUserHdrInfo = SessionHelper.GetCurrentUser();
        IUtility oUtilityClient = RemotingHelper.GetUtilityObject();

        oGLDataRecurringItemScheduleInfo.AddedBy = oUserHdrInfo.LoginID;
        oGLDataRecurringItemScheduleInfo.AddedByUserID = oUserHdrInfo.UserID;

        ExLabel lblScheduleAmount = (ExLabel)item.FindControl("lblScheduleAmount");
        decimal lblScheduleAmountvalue = Convert.ToDecimal(lblScheduleAmount.Text);
        ExLabel lblLocalCurrencyCode = (ExLabel)item.FindControl("lblLocalCurrencyCode");
        oGLDataRecurringItemScheduleInfo.LocalCurrencyCode = lblLocalCurrencyCode.Text;

        ExLabel lblScheduleBeginDate = (ExLabel)item.FindControl("lblScheduleBeginDate");
        oGLDataRecurringItemScheduleInfo.ScheduleBeginDate = Convert.ToDateTime(lblScheduleBeginDate.Text);

        ExLabel lblOpenDate = (ExLabel)item.FindControl("lblOpenDate");
        oGLDataRecurringItemScheduleInfo.OpenDate = Convert.ToDateTime(lblOpenDate.Text);

        ExLabel lblSceduleEndDate = (ExLabel)item.FindControl("lblSceduleEndDate");
        oGLDataRecurringItemScheduleInfo.ScheduleEndDate = Convert.ToDateTime(lblSceduleEndDate.Text);
        oGLDataRecurringItemScheduleInfo.ScheduleAmount = lblScheduleAmountvalue;

        decimal baseCurrencyExchangeRate = 1;
        if (!String.IsNullOrEmpty(this.CurrentBCCY))
        {
            baseCurrencyExchangeRate = Convert.ToDecimal(item["BaseCurrencyExchangeRate"].Text);
            oGLDataRecurringItemScheduleInfo.ScheduleAmountBaseCurrency = Math.Round(lblScheduleAmountvalue * baseCurrencyExchangeRate, 2);
        }

        decimal reportingCurrencyExchangeRate = Convert.ToDecimal(item["ReportingCurrencyExchangeRate"].Text);
        oGLDataRecurringItemScheduleInfo.ScheduleAmountReportingCurrency = Math.Round(lblScheduleAmountvalue * reportingCurrencyExchangeRate, 2);

        decimal noOfDaysInSchedule = oGLDataRecurringItemScheduleInfo.ScheduleEndDate.Value.Subtract(oGLDataRecurringItemScheduleInfo.ScheduleBeginDate.Value).Days + 1;
        decimal noOfDaysPassed = SessionHelper.CurrentReconciliationPeriodEndDate.Value.Subtract(oGLDataRecurringItemScheduleInfo.ScheduleBeginDate.Value).Days + 1;
        decimal recPeriodAmountLCCY = 0;

        if (noOfDaysInSchedule > 0 && noOfDaysPassed > 0)
        {
            recPeriodAmountLCCY = oGLDataRecurringItemScheduleInfo.ScheduleAmount.Value * (noOfDaysPassed / noOfDaysInSchedule);
        }
        else // if noOfDaysPassed=0 ?
        {
            //recPeriodAmountLCCY = 0;
            //Raise Error message etc
        }

        oGLDataRecurringItemScheduleInfo.RecPeriodAmountLocalCurrency = recPeriodAmountLCCY;

        if (!String.IsNullOrEmpty(this.CurrentBCCY))
            oGLDataRecurringItemScheduleInfo.RecPeriodAmountBaseCurrency = recPeriodAmountLCCY * baseCurrencyExchangeRate;

        oGLDataRecurringItemScheduleInfo.RecPeriodAmountReportingCurrency = recPeriodAmountLCCY * reportingCurrencyExchangeRate;

        oGLDataRecurringItemScheduleInfo.BalanceLocalCurrency = Math.Round((oGLDataRecurringItemScheduleInfo.ScheduleAmount.Value - recPeriodAmountLCCY), TestConstant.DECIMAL_PLACES_FOR_MATH_ROUND);
        if (!String.IsNullOrEmpty(this.CurrentBCCY))
            oGLDataRecurringItemScheduleInfo.BalanceBaseCurrency = Math.Round(oGLDataRecurringItemScheduleInfo.BalanceLocalCurrency.Value * baseCurrencyExchangeRate, TestConstant.DECIMAL_PLACES_FOR_MATH_ROUND);

        oGLDataRecurringItemScheduleInfo.BalanceReportingCurrency = Math.Round(oGLDataRecurringItemScheduleInfo.BalanceLocalCurrency.Value * reportingCurrencyExchangeRate, TestConstant.DECIMAL_PLACES_FOR_MATH_ROUND);

        ExLabel description = (ExLabel)item.FindControl("lblDescription");
        oGLDataRecurringItemScheduleInfo.Comments = description.Text;
        oGLDataRecurringItemScheduleInfo.DateAdded = DateTime.Now;
        oGLDataRecurringItemScheduleInfo.GLDataID = this._GLDataID;
        ExLabel lblScheduleName = (ExLabel)item.FindControl("lblScheduleName");
        oGLDataRecurringItemScheduleInfo.ScheduleName = lblScheduleName.Text;
        oGLDataRecurringItemScheduleInfo.IsActive = true;
        if (Request.QueryString[QueryStringConstants.REC_CATEGORY_TYPE_ID] != null)
            oGLDataRecurringItemScheduleInfo.ReconciliationCategoryTypeID = _RecCategoryTypeID;

        oGLDataRecurringItemScheduleInfo.CalculationFrequencyID = (short)ARTEnums.CalculationFrequency.DailyInterval;
        oGLDataRecurringItemScheduleInfo.TotalIntervals = null;
        oGLDataRecurringItemScheduleInfo.CurrentInterval = null;
        oGLDataRecurringItemScheduleInfo.ReconciliationCategoryTypeID = this._RecCategoryTypeID;
        oGLDataRecurringItemScheduleInfo.RecCategoryTypeID = this.recCategoryTypeID;
        oGLDataRecurringItemScheduleInfo.RecordSourceTypeID = (short)ARTEnums.RecordSourceType.DataImport;
        return oGLDataRecurringItemScheduleInfo;
    }

    private string GetPreviousPageName()
    {
        string PName = "";
        if (Request.UrlReferrer != null)
        {
            PName = Request.UrlReferrer.Segments[Request.UrlReferrer.Segments.Length - 1];
        }
        return PName;
    }

    private void PopulateItemsOnPage()
    {
        hlOpenExcelFile.NavigateUrl = "~/Templates/RecurringImportUI.xlsx";
        this.lblRules.LabelID = (int)WebEnums.DataImportTypeLabelID.ScheduleRecItemUploadLabelID;
        Helper.SetPageTitle(this, 1506);
        Helper.ShowInputRequirementSection(this, 1641, 1647);
    }

    private void SetPrivateVariables()
    {
        if (this.GLDataHdrInfo == null)
        {
            if (!string.IsNullOrEmpty(Request.QueryString[QueryStringConstants.GLDATA_ID]))
            {
                long? _gLDataID = Convert.ToInt64(Request.QueryString[QueryStringConstants.GLDATA_ID]);
                this.GLDataHdrInfo = Helper.GetGLDataHdrInfo(_gLDataID);
            }
        }
        this._AccountID = this.GLDataHdrInfo.AccountID;
        this._NetAccountID = this.GLDataHdrInfo.NetAccountID;
        this._GLDataID = this.GLDataHdrInfo.GLDataID;
        this._IsSRA = this.GLDataHdrInfo.IsSystemReconcilied.GetValueOrDefault();


        if (Request.QueryString[QueryStringConstants.REC_CATEGORY_TYPE_ID] != null)
            _RecCategoryTypeID = Convert.ToInt16(Request.QueryString[QueryStringConstants.REC_CATEGORY_TYPE_ID]);

        if (Request.QueryString[QueryStringConstants.REC_CATEGORY_ID] != null)
            _RecCategoryID = Convert.ToInt16(Request.QueryString[QueryStringConstants.REC_CATEGORY_ID]);

        this._IsMultiCurrencyEnabled = Helper.IsCapabilityActivatedForCurrentRecPeriod(ARTEnums.Capability.MultiCurrency);

        // Set the Master Page Properties for GL Data ID
        RecHelper.SetRecStatusBarPropertiesForOtherPages(this, this._GLDataID);
    }

    private void DeleteFile(string filePath)
    {
        //Delete the saved file
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
            if (ViewState[ViewStateConstants.IMPORTFILEATTRIBUTES] != null)
                ViewState.Remove(ViewStateConstants.IMPORTFILEATTRIBUTES);
        }
        //Send exception to user interface
    }

    private void ShowGridAndImportButtons(DataTable dt)
    {
        rgImportList.Visible = true;
        Sel.Value = string.Empty;
        pnlImportGrid.Visible = true;
        this.rgImportList.DataSource = dt;
        this.rgImportList.DataBind();

        GridColumn oGridColumn = this.rgImportList.Columns.FindByUniqueNameSafe("Error");

        if (isError == false)
        {
            if (oGridColumn != null)
                oGridColumn.Visible = false;
        }
        else
        {
            if (oGridColumn != null)
                oGridColumn.Visible = true;
        }
    }

    private void SetExchangeRates(string LocalCurrencyCode)
    {
        IUtility oUtilityClient = RemotingHelper.GetUtilityObject();
        this._ExchangeRateBaseCurrency = 1;
        if (string.IsNullOrEmpty(this.CurrentBCCY))
        {
            this._ExchangeRateBaseCurrency = oUtilityClient.GetCurrencyExchangeRate(SessionHelper.CurrentReconciliationPeriodID.Value
                , LocalCurrencyCode, this.CurrentBCCY, this._IsMultiCurrencyEnabled, Helper.GetAppUserInfo());
        }


        this._ExchangeRateReportingCurrency = oUtilityClient.GetCurrencyExchangeRate(SessionHelper.CurrentReconciliationPeriodID.Value
            , LocalCurrencyCode, SessionHelper.ReportingCurrencyCode, this._IsMultiCurrencyEnabled, Helper.GetAppUserInfo());
    }

    #endregion

    #region "Public Methods"
    public override string GetMenuKey()
    {
        return "NonRecurringImportUII";
    }
    #endregion
    protected void rgImportList_PageSizeChanged(object source, GridPageSizeChangedEventArgs e)
    {
        hdnNewPageSize.Value = e.NewPageSize.ToString();
    }

    protected void rgImportList_ItemCreated(object sender, GridItemEventArgs e)
    {
        if (e.Item is GridPagerItem)
        {
            GridPagerItem gridPager = e.Item as GridPagerItem;
            DropDownList oRadComboBox = (DropDownList)gridPager.FindControl("ddlPageSize");
            if (rgImportList.AllowCustomPaging)
            {
                GridHelper.BindPageSizeGrid(oRadComboBox);
                oRadComboBox.SelectedValue = hdnNewPageSize.Value.ToString();
                oRadComboBox.Attributes.Add("onChange", "return ddlPageSize_SelectedIndexChanged('" + oRadComboBox.ClientID + "');");
                oRadComboBox.Visible = true;
            }
            else
            {
                Control pnlPageSizeDDL = gridPager.FindControl("pnlPageSizeDDL");
                pnlPageSizeDDL.Visible = false;
            }
            Control numericPagerControl = gridPager.GetNumericPager();
            Control placeHolder = gridPager.FindControl("NumericPagerPlaceHolder");
            placeHolder.Controls.Add(numericPagerControl);
        }
    }

    protected void rgImportList_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    {
        try
        {
            int pageIndex = rgImportList.CurrentPageIndex;
            int pageSize = Convert.ToInt32(hdnNewPageSize.Value);
            int defaultItemCount = Helper.GetDefaultChunkSize(pageSize);
            int count;

            count = ((((pageIndex + 1) * pageSize) / defaultItemCount) + 1) * defaultItemCount;

            object obj = ViewState["DataTable"];
            if (obj != null)
            {
                DataTable objectCollection = (DataTable)obj;
                if (objectCollection.Rows.Count % defaultItemCount == 0)
                    rgImportList.VirtualItemCount = objectCollection.Rows.Count + 1;
                else
                    rgImportList.VirtualItemCount = objectCollection.Rows.Count;
                rgImportList.MasterTableView.DataSource = objectCollection;
            }
        }
        catch (Exception ex)
        {
            Helper.ShowErrorMessage(this, ex);
        }
    }

    #region "Data Import Grid By Rec Period ID"
    protected void rgDataImport_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
    {
        RefreshDataImportStatusGrid(false);
    }

    private void RefreshDataImportStatusGrid(bool IsRebind)
    {
        try
        {
            List<DataImportHdrInfo> oDataImportHdrInfoCollection = null;
            IDataImport oDataImportClient = RemotingHelper.GetDataImportObject();

            // Get All Data Imports done in the Current Rec Period
            if (SessionHelper.CurrentReconciliationPeriodID != null)
            {
                //bool showHiddenRows = chkShowHiddenGLDataImport.Checked;
                int? recPeriodID = SessionHelper.CurrentReconciliationPeriodID;

                RecItemUploadParamInfo oRecItemUploadParamInfo = new RecItemUploadParamInfo();
                Helper.FillCommonServiceParams(oRecItemUploadParamInfo);
                oRecItemUploadParamInfo.RecCategoryID = _RecCategoryID;
                oRecItemUploadParamInfo.RecCategoryTypeID = _RecCategoryTypeID;
                oRecItemUploadParamInfo.GLDataID = _GLDataID;
                oDataImportHdrInfoCollection = oDataImportClient.GetRecItemDataImportStatus(oRecItemUploadParamInfo, Helper.GetAppUserInfo());

                // Get the Values for Status and Type
                for (int i = 0; i < oDataImportHdrInfoCollection.Count; i++)
                {
                    oDataImportHdrInfoCollection[i].DataImportStatus = LanguageUtil.GetValue(oDataImportHdrInfoCollection[i].DataImportStatusLabelID.Value);
                    oDataImportHdrInfoCollection[i].DataImportType = LanguageUtil.GetValue(oDataImportHdrInfoCollection[i].DataImportTypeLabelID.Value);
                }
            }
            else
            {
                oDataImportHdrInfoCollection = new List<DataImportHdrInfo>();
            }

            var query = from d in oDataImportHdrInfoCollection
                        where d.AddedBy == SessionHelper.CurrentUserLoginID
                        select d;
            foreach (var d in query)
            {
                d.IsRecordOwner = true;
            }
            rgDataImport.MasterTableView.DataSource = oDataImportHdrInfoCollection;
            if (IsRebind)
                rgDataImport.Rebind();

            // Sort the Data
            GridHelper.SortDataSource(rgDataImport.MasterTableView);
        }
        catch (ARTException ex)
        {
            Helper.ShowErrorMessage(this, ex);
        }
        catch (Exception ex)
        {
            Helper.ShowErrorMessage(this, ex);
        }
    }

    protected void rgDataImport_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {
        if (e.Item.ItemType == Telerik.Web.UI.GridItemType.Item ||
            e.Item.ItemType == Telerik.Web.UI.GridItemType.AlternatingItem)
        {
            BindCommonFields(e);
            GridDataItem item = (GridDataItem)e.Item;
            DataImportHdrInfo oDataImportHdrInfo = (DataImportHdrInfo)e.Item.DataItem;

            // Show Data for Force Commit
            ExHyperLink hlForceCommitDetails = (ExHyperLink)e.Item.FindControl("hlForceCommitDetails");
            hlForceCommitDetails.Text = Helper.GetDisplayDateTime(oDataImportHdrInfo.ForceCommitDate);

            // Set URLs
            string url = GetUrlForDataImportStatusPage(oDataImportHdrInfo);
            hlForceCommitDetails.NavigateUrl = url;

            DataImportHdrInfo obj = (DataImportHdrInfo)e.Item.DataItem;

            short status = obj.DataImportStatusID.Value;
            short importType = obj.DataImportTypeID.Value;
        }
    }

    protected void rgDataImport_SortCommand(object source, GridSortCommandEventArgs e)
    {
        GridHelper.HandleSortCommand(e);
        rgDataImport.Rebind();
    }

    protected void rgDataImport_ItemCreated(object sender, GridItemEventArgs e)
    {
        GridHelper.SetStylesForExportGrid(e, isExportPDF, isExportExcel);
        if (e.Item is GridCommandItem)
        {
            ImageButton ibExportToExcel = (e.Item as GridCommandItem).FindControl(TelerikConstants.GRID_ID_EXPORT_TO_EXCEL_ICON) as ImageButton;
            if (ibExportToExcel != null)
            {
                ibExportToExcel.CausesValidation = false;
                Helper.RegisterPostBackToControls(this, ibExportToExcel);
            }
        }
        if (e.Item is GridCommandItem)
        {
            ImageButton ibExportToExcel = (e.Item as GridCommandItem).FindControl(TelerikConstants.GRID_ID_EXPORT_TO_PDF_ICON) as ImageButton;
            if (ibExportToExcel != null)
            {
                ibExportToExcel.CausesValidation = false;
                Helper.RegisterPostBackToControls(this, ibExportToExcel);
            }
        }
        if (e.Item is GridCommandItem)
        {
            ImageButton ibRefresh = (e.Item as GridCommandItem).FindControl(TelerikConstants.GRID_ID_REFRESH_ICON) as ImageButton;
            if (ibRefresh != null)
            {
                ibRefresh.CausesValidation = false;
                Helper.RegisterPostBackToControls(this, ibRefresh);
            }
        }
    }

    protected void rgDataImport_ItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == TelerikConstants.GridExportToPDFCommandName)
            {
                isExportPDF = true;
                rgDataImport.MasterTableView.Columns.FindByUniqueName("imgStatus").Visible = false;
                //rgDataImport.MasterTableView.Columns.FindByUniqueName("CheckboxSelectColumn").Visible = false;
                GridHelper.ExportGridToPDF(rgDataImport, 1219);
            }
            if (e.CommandName == TelerikConstants.GridExportToExcelCommandName)
            {
                isExportExcel = true;
                rgDataImport.MasterTableView.Columns.FindByUniqueName("imgStatus").Visible = false;
                //rgDataImport.MasterTableView.Columns.FindByUniqueName("CheckboxSelectColumn").Visible = false;
                GridHelper.ExportGridToExcel(rgDataImport, 1219);
            }
            if (e.CommandName == TelerikConstants.GridRefreshCommandName)
            {
                Session[SessionConstants.SEARCH_RESULTS_DATA_IMPORT_SCHEDULE_REC_ITEM] = null;
                rgDataImport.Rebind();
            }
        }
        catch (Exception ex)
        {
            Helper.ShowErrorMessage(this, ex);
        }
    }
    #endregion
    private void BindCommonFields(Telerik.Web.UI.GridItemEventArgs e)
    {
        DataImportHdrInfo oDataImportHdrInfo = (DataImportHdrInfo)e.Item.DataItem;

        ExHyperLink hlProfileName = (ExHyperLink)e.Item.FindControl("hlProfileName");
        ExHyperLink hlDate = (ExHyperLink)e.Item.FindControl("hlDate");
        ExHyperLink hlImportType = (ExHyperLink)e.Item.FindControl("hlImportType");
        ExHyperLink hlStatus = (ExHyperLink)e.Item.FindControl("hlStatus");
        ExHyperLink hlRecordsAffected = (ExHyperLink)e.Item.FindControl("hlRecordsAffected");
        ExHyperLink hlFileName = (ExHyperLink)e.Item.FindControl("hlFileName");
        ExImageButton imgFileType = (ExImageButton)e.Item.FindControl("imgFileType");
        ExLabel lblAddedBy = (ExLabel)e.Item.FindControl("lblAddedBy");
        lblAddedBy.Text = oDataImportHdrInfo.AddedBy;

        if (!string.IsNullOrEmpty(oDataImportHdrInfo.DataImportName))
        {
            hlProfileName.Text = oDataImportHdrInfo.DataImportName;
        }
        hlDate.Text = Helper.GetDisplayDateTime(oDataImportHdrInfo.DateAdded);
        hlImportType.Text = oDataImportHdrInfo.DataImportType;
        hlStatus.Text = oDataImportHdrInfo.DataImportStatus;
        hlRecordsAffected.Text = Helper.GetDisplayIntegerValue(oDataImportHdrInfo.RecordsImported);
        hlFileName.Text = oDataImportHdrInfo.FileName;

        string url = "DownloadAttachment.aspx?" + QueryStringConstants.FILE_PATH + "=" + Server.UrlEncode(oDataImportHdrInfo.PhysicalPath);
        imgFileType.OnClientClick = "document.location.href = '" + url + "';return false;";

        //// Icons
        WebEnums.DataImportStatus eDataImportStatus = (WebEnums.DataImportStatus)System.Enum.Parse(typeof(WebEnums.DataImportStatus), oDataImportHdrInfo.DataImportStatusID.Value.ToString());

        switch (eDataImportStatus)
        {
            case WebEnums.DataImportStatus.Success:
                ExImage imgSuccess = (ExImage)e.Item.FindControl("imgSuccess");
                imgSuccess.Visible = true;
                break;

            case WebEnums.DataImportStatus.Failure:
                ExImage imgFailure = (ExImage)e.Item.FindControl("imgFailure");
                imgFailure.Visible = true;
                break;

            case WebEnums.DataImportStatus.Warning:
                ExImage imgWarning = (ExImage)e.Item.FindControl("imgWarning");
                imgWarning.Visible = true;
                break;

            case WebEnums.DataImportStatus.Processing:
                ExImage imgProcessing = (ExImage)e.Item.FindControl("imgProcessing");
                imgProcessing.Visible = true;
                break;

            case WebEnums.DataImportStatus.Submitted:
                ExImage imgToBeProcessed = (ExImage)e.Item.FindControl("imgToBeProcessed");
                imgToBeProcessed.Visible = true;
                break;
            case WebEnums.DataImportStatus.Reject:
                ExImage imgReject = (ExImage)e.Item.FindControl("imgReject");
                imgReject.Visible = true;
                break;

        }
        // Set the Hyperlink for Next Page
        SetHyperlinkForStatusPage(oDataImportHdrInfo, hlProfileName, hlDate, hlImportType, hlRecordsAffected, hlStatus, hlFileName);
    }

    private void SetHyperlinkForStatusPage(DataImportHdrInfo oDataImportHdrInfo, ExHyperLink hlProfileName,
        ExHyperLink hlDate, ExHyperLink hlImportType, ExHyperLink hlRecordsAffected, ExHyperLink hlStatus, ExHyperLink hlFileName)
    {
        string url = GetUrlForDataImportStatusPage(oDataImportHdrInfo);
        hlStatus.NavigateUrl = url;
        hlProfileName.NavigateUrl = url;
        hlDate.NavigateUrl = url;
        hlImportType.NavigateUrl = url;
        hlRecordsAffected.NavigateUrl = url;
        hlFileName.NavigateUrl = url;
    }

    private static string GetUrlForDataImportStatusPage(DataImportHdrInfo oDataImportHdrInfo)
    {
        string url = "RecItemImportStatusMessage.aspx?" + QueryStringConstants.DATA_IMPORT_ID + "=" + oDataImportHdrInfo.DataImportID.ToString() + "&" + QueryStringConstants.IS_REDIRECTED_FROM_RECURRING_IMPORT + "=" + 1;
        return url;
    }
}
