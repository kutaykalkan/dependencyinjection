using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data.OleDb;
using System.Data;
using System.Data.SqlClient;
using SkyStem.ART.Client.IServices;
using SkyStem.ART.Client.Model;
using SkyStem.ART.Web.Classes;
using System.Collections;
using SkyStem.ART.Web.Utility;
using SkyStem.Library.Controls.WebControls;
using Telerik.Web.UI;
using Telerik.Web.UI.Upload;
using SkyStem.ART.Web.Data;
using System.Text;
using SkyStem.Library.Controls.TelerikWebControls;
using SkyStem.ART.Client.Exception;
using SkyStem.Language.LanguageUtility;
using SkyStem.ART.Client.Model.Matching;
using SkyStem.ART.Client.Data;
using SkyStem.ART.Client.Model.RecControlCheckList;
using SkyStem.ART.Shared.Data;
using SkyStem.Language.LanguageUtility.Classes;
using System.Globalization;



public partial class Pages_DataImport : PageBaseCompany
{
    #region Variables & Constants
    const string POPUP_PAGE = "AddUserMailOnDataImport.aspx?";
    const int POPUP_WIDTH = 800;
    const int POPUP_HEIGHT = 480;
    string _PopupUrl = string.Empty;
    string _PopupUrlFailure = string.Empty;
    private DateTime? _openPeriod;
    #endregion

    #region Properties
    #endregion

    #region Delegates & Events
    #endregion

    #region Page Events
    protected void Page_Init(object sender, EventArgs e)
    {
        MasterPageBase ompage = (MasterPageBase)this.Master;
        ompage.ReconciliationPeriodChangedEventHandler += new EventHandler(ompage_ReconciliationPeriodChangedEventHandler);
        this.SetErrorMessage();
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            Helper.SetPageTitle(this, 1506);

            txtUserNameSucess.Attributes.Add("readonly", "readonly");
            txtUserNameFailure.Attributes.Add("readonly", "readonly");
            Helper.ShowInputRequirementSection(this, 1604, 1603, 1605, 1606);
            //rgPeriodEndDate.ClientSettings.Selecting.AllowRowSelect = true;
            //rgHolidayCal.ClientSettings.Selecting.AllowRowSelect = true;
            rgCurrency.ClientSettings.Selecting.AllowRowSelect = true;
            rgSubLedger.ClientSettings.Selecting.AllowRowSelect = true;


            _PopupUrl = POPUP_PAGE + QueryStringConstants.SUCESS + "=1";
            _PopupUrlFailure = POPUP_PAGE + QueryStringConstants.FAILURE + "=2";

            imgUserNameSucess.Attributes.Add("onclick", "return OpenRadWindow('" + _PopupUrl + "', " + POPUP_HEIGHT + " , " + POPUP_WIDTH + ");return false");
            imgUserNameFailure.Attributes.Add("onclick", "return OpenRadWindow('" + _PopupUrlFailure + "', " + POPUP_HEIGHT + " , " + POPUP_WIDTH + ");return false");


            if (!Page.IsPostBack)
            {
                //Setting allowed file extensions.
                this.RadFileUpload.AllowedFileExtensions = DataImportHelper.GetAllowedFileExtensions();

                //Set allowed file size in bytes
                this.RadFileUpload.MaxFileSize = DataImportHelper.GetAllowedMaximumFileSize(SessionHelper.CurrentCompanyID.Value);

                //setting error messages to be shown at runtime
                this.cvFileUpload.Attributes.Add("fileNameErrorMessage", LanguageUtil.GetValue(5000035));
                this.cvFileUpload.Attributes.Add("fileExtensionErrorMessage", LanguageUtil.GetValue(5000036));

                //Hide all Preview Grids
                this.HideAllGrids();

            }
        }

        catch (ARTException ex)
        {
            //ExLabel lblErrorMessage = this.Page.Master.FindControl("lblErrorMessage") as ExLabel;
            //Helper.FormatAndShowErrorMessage(lblErrorMessage, ex);
            Helper.ShowErrorMessage(this, ex);
        }
        catch (Exception ex)
        {
            //ExLabel lblErrorMessage = this.Page.Master.FindControl("lblErrorMessage") as ExLabel;
            //Helper.FormatAndShowErrorMessage(lblErrorMessage, ex);
            Helper.ShowErrorMessage(this, ex);
        }
    }
    protected void Page_PreRender(Object source, EventArgs e)
    {
        this.UpdateImportTypeRulesInfo();
    }
    #endregion

    #region Grid Events
    #region Period
    protected void rgPeriodEndDate_ItemDataBound(object sender, GridItemEventArgs e)
    {
        if (e.Item.ItemType == GridItemType.Item || e.Item.ItemType == GridItemType.AlternatingItem)
        {
            GridDataItem item = (GridDataItem)e.Item;
            CheckBox checkBox = (CheckBox)item["CheckboxSelectColumn"].Controls[0];
            ExLabel lblPeriodEndDate = (ExLabel)item.FindControl("lblPeriodEndDate");
            //ExLabel lblDescription = (ExLabel)item.FindControl("lblDescription");
            DateTime periodEndDate = Convert.ToDateTime(lblPeriodEndDate.Text);
            if (this._openPeriod.HasValue)
            {
                if (periodEndDate <= this._openPeriod.Value)
                {
                    checkBox.Enabled = false;
                    Sel.Value += e.Item.ItemIndex.ToString() + ":";
                    //if (lblDescription != null)
                    //{
                    //    lblDescription.Text = Helper.GetLabelIDValue(1728);//"Invalid Period, Cannot be imported";
                    //}
                }
            }
        }
    }
    #endregion
    #region HolidayCal
    protected void rgHolidayCal_ItemDataBound(object sender, GridItemEventArgs e)
    {
        if (e.Item.ItemType == GridItemType.Item || e.Item.ItemType == GridItemType.AlternatingItem)
        {
            GridDataItem item = (GridDataItem)e.Item;
            CheckBox checkBox = (CheckBox)item["CheckboxSelectColumn"].Controls[0];
            bool isValid = false;
            if (Boolean.TryParse(DataBinder.Eval(e.Item.DataItem, "IsValidRow").ToString(), out isValid))
            {
                if (!isValid)
                {
                    checkBox.Enabled = false;
                    Sel.Value += e.Item.ItemIndex.ToString() + ":";
                }
            }
        }
    }
    #endregion
    #region Currency
    protected void rgCurrency_ItemDataBound(object sender, GridItemEventArgs e)
    {
        if (e.Item.ItemType == GridItemType.Item || e.Item.ItemType == GridItemType.AlternatingItem)
        {
            GridDataItem item = (GridDataItem)e.Item;
            CheckBox checkBox = (CheckBox)item["CheckboxSelectColumn"].Controls[0];
            ExLabel lblCurrencyError = (ExLabel)e.Item.FindControl("lblCurrencyError");
            bool isValid = false;
            if (Boolean.TryParse(DataBinder.Eval(e.Item.DataItem, "IsValidRow").ToString(), out isValid))
            {
                if (!isValid)
                {
                    checkBox.Enabled = false;
                    Sel.Value += e.Item.ItemIndex.ToString() + ":";
                }
            }

            bool isError = false;
            if (Boolean.TryParse(DataBinder.Eval(e.Item.DataItem, "IsError").ToString(), out isError))
            {
                if (isError)
                {
                    checkBox.Enabled = false;
                    lblCurrencyError.LabelID = 5000420;
                    Sel.Value += e.Item.ItemIndex.ToString() + ":";
                }
            }

            DataRowView drv = (DataRowView)e.Item.DataItem;

            ExLabel lblPeriod = (ExLabel)e.Item.FindControl("lblPeriod");
            lblPeriod.Text = Helper.GetDisplayDate(Convert.ToDateTime(drv[0]));
        }
    }

    #endregion
    #region SubLedger
    protected void rgSubLedger_ItemDataBound(object sender, GridItemEventArgs e)
    {
        if (e.Item.ItemType == GridItemType.Item || e.Item.ItemType == GridItemType.AlternatingItem)
        {
            GridDataItem item = (GridDataItem)e.Item;
            CheckBox checkBox = (CheckBox)item["CheckboxSelectColumn"].Controls[0];
            ExLabel lblSubledgerSourceError = (ExLabel)e.Item.FindControl("lblSubledgerSourceError");
            bool isValid = false;

            if (Boolean.TryParse(DataBinder.Eval(e.Item.DataItem, "IsValidRow").ToString(), out isValid))
            {
                if (!isValid)
                {
                    checkBox.Enabled = false;
                    //Sel.Value += e.Item.ItemIndex.ToString() + ":";
                }
            }

            bool isError = false;
            if (Boolean.TryParse(DataBinder.Eval(e.Item.DataItem, "IsError").ToString(), out isError))
            {
                if (isError)
                {
                    checkBox.Enabled = false;
                    lblSubledgerSourceError.LabelID = 5000245;
                    Sel.Value += e.Item.ItemIndex.ToString() + ":";
                }
            }
        }
    }
    #endregion

    #region RecControlChecklist
    protected void rgRecControlChecklist_ItemDataBound(object sender, GridItemEventArgs e)
    {
        if (e.Item.ItemType == GridItemType.Item || e.Item.ItemType == GridItemType.AlternatingItem)
        {
            GridDataItem item = (GridDataItem)e.Item;
            CheckBox checkBox = (CheckBox)item["CheckboxSelectColumn"].Controls[0];
            ExLabel lblRecControlChecklistError = (ExLabel)e.Item.FindControl("lblRecControlChecklistError");
            bool isValid = false;

            if (Boolean.TryParse(DataBinder.Eval(e.Item.DataItem, "IsValidRow").ToString(), out isValid))
            {
                if (!isValid)
                {
                    checkBox.Enabled = false;
                    //Sel.Value += e.Item.ItemIndex.ToString() + ":";
                }
            }
        }
    }
    #endregion


    #endregion

    #region Other Events
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        MasterPageBase oMasterPageBase = (MasterPageBase)this.Master;
        oMasterPageBase.HideMessage();
        ClearDataImport();
        if (Page.IsValid)
        {
            if (RadFileUpload.UploadedFiles.Count > 0)
            {
                string filePath = string.Empty;
                string fileName = string.Empty;
                string targetFolder = string.Empty;
                WebEnums.RecPeriodStatus currectRecPeriodStatus = SessionHelper.CurrentRecProcessStatusEnum;
                StringBuilder oSBErrors = null;
                short importType = -1;
                DataTable dt = null;
                string errorMessage = "";
                int companyID = SessionHelper.CurrentCompanyID.Value;
                int tempImportTemplateID;
                int? ImportTemplateID;
                if (trDataImportTemaplate.Visible && int.TryParse(ddlDataimportTemplate.SelectedValue, out tempImportTemplateID))
                {
                    if (tempImportTemplateID == Convert.ToInt32(WebConstants.ART_TEMPLATE))
                        ImportTemplateID = null;
                    else
                        ImportTemplateID = tempImportTemplateID;
                }
                else
                    ImportTemplateID = null;

                if (SessionHelper.CurrentRoleID == (short)WebEnums.UserRole.SKYSTEM_ADMIN)
                    companyID = 0;
                //DataTable dtPeriodEndDates;
                try
                {
                    UploadedFile validFile = RadFileUpload.UploadedFiles[0];
                    ImportFileAttributes ImportFile;
                    importType = Convert.ToInt16(this.ddlImportType.SelectedValue);
                    //Get folder path and name as per companyid and import type
                    targetFolder = SharedDataImportHelper.GetFolderForImport(companyID, importType);

                    MultilingualAttributeInfo oMultilingualAttributeInfo;
                    oMultilingualAttributeInfo = LanguageHelper.GetMultilingualAttributeInfo(SessionHelper.CurrentCompanyID, SessionHelper.GetUserLanguage());
                    fileName = SharedDataImportHelper.GetFileName(validFile.GetNameWithoutExtension(), validFile.GetExtension(), importType, SessionHelper.CurrentReconciliationPeriodEndDate, oMultilingualAttributeInfo);
                    filePath = Path.Combine(targetFolder, fileName);

                    IDataImport oDataImport = null;
                    short? keyCount = null;
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

                    if (SessionHelper.CurrentRoleID != (short)WebEnums.UserRole.SKYSTEM_ADMIN && ((decimal)(ImportFile.FileSize) / (decimal)(1024 * 1024)) > (dataStorageCapacity - currentUsage))
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
                        string SheetName = string.Empty;
                        switch (importType)
                        {
                            case (short)ARTEnums.DataImportType.GLTBS:
                                #region "GLTBS Upload"
                                List<MatchingSourceDataImportHdrInfo> oMatchingSourceDataImportHdrInfoList = new List<MatchingSourceDataImportHdrInfo>();
                                if (MatchingHelper.ValidateFileForMatchingDataImport(filePath, validFile.GetName(), validFile.GetExtension(), ARTEnums.DataImportType.GLTBS, out errorMessage))
                                {
                                    MatchingSourceDataImportHdrInfo oMatchingSourceDataImportHdrInfo = new MatchingSourceDataImportHdrInfo();
                                    oMatchingSourceDataImportHdrInfo.MatchingSourceName = this.txtProfileName.Text;
                                    oMatchingSourceDataImportHdrInfo.FileName = validFile.GetName();
                                    oMatchingSourceDataImportHdrInfo.PhysicalPath = filePath;
                                    oMatchingSourceDataImportHdrInfo.FileSize = validFile.ContentLength;
                                    oMatchingSourceDataImportHdrInfo.MatchingSourceTypeID = importType;
                                    oMatchingSourceDataImportHdrInfo.DataImportStatusID = (short)WebEnums.DataImportStatus.Draft;
                                    oMatchingSourceDataImportHdrInfo.RecPeriodID = SessionHelper.CurrentReconciliationPeriodID;
                                    oMatchingSourceDataImportHdrInfo.UserID = SessionHelper.CurrentUserID;
                                    oMatchingSourceDataImportHdrInfo.RoleID = SessionHelper.CurrentRoleID;
                                    oMatchingSourceDataImportHdrInfo.LanguageID = SessionHelper.GetUserLanguage();
                                    oMatchingSourceDataImportHdrInfo.DateAdded = DateTime.Now;
                                    oMatchingSourceDataImportHdrInfo.AddedBy = SessionHelper.CurrentUserLoginID;
                                    oMatchingSourceDataImportHdrInfo.IsActive = true;
                                    oMatchingSourceDataImportHdrInfoList.Add(oMatchingSourceDataImportHdrInfo);

                                    oMatchingSourceDataImportHdrInfoList = MatchingHelper.SaveMatchingSource(SessionHelper.CurrentCompanyID, oMatchingSourceDataImportHdrInfoList);

                                    if (oMatchingSourceDataImportHdrInfoList.Count > 0)
                                    {
                                        Session[SessionConstants.MATCHING_SOURCE_DATA] = oMatchingSourceDataImportHdrInfoList;
                                        Response.Redirect("~/Pages/Matching/MatchSourceDataTypeMapping.aspx", false);
                                    }
                                }
                                else
                                    throw new Exception(errorMessage);
                                #endregion
                                break;
                            case (short)ARTEnums.DataImportType.HolidayCalendar:
                                #region "Holiday Calendar"
                                dt = DataImportHelper.GetDataTableFromExcel(filePath, validFile.GetExtension(), importType, oSBErrors);
                                if (dt.Select("IsDuplicate = true").Length > 0)//if duplicate, show message for duplicates
                                    Helper.FormatAndShowErrorMessage(this.Page.Master.FindControl("lblErrorMessage") as ExLabel, Helper.GetLabelIDValue(5000043));
                                this.ShowHideGrids(importType, dt);
                                #endregion
                                break;
                            case (short)ARTEnums.DataImportType.PeriodEndDates:
                                #region "Period End Date Upload"
                                dt = DataImportHelper.GetDataTableFromExcel(filePath, validFile.GetExtension(), importType, oSBErrors);
                                if (dt.Select("IsDuplicate = true").Length > 0)//if duplicate, show message for duplicates
                                    Helper.FormatAndShowErrorMessage(this.Page.Master.FindControl("lblErrorMessage") as ExLabel, Helper.GetLabelIDValue(5000043));
                                this.ShowHideGrids(importType, dt);
                                #endregion
                                break;
                            case (short)ARTEnums.DataImportType.SubledgerSource:
                                #region "Subledger Source"
                                dt = DataImportHelper.GetDataTableFromExcel(filePath, validFile.GetExtension(), importType, oSBErrors);
                                if (dt.Select("IsDuplicate = true").Length > 0)//if duplicate, show message for duplicates
                                    Helper.FormatAndShowErrorMessage(this.Page.Master.FindControl("lblErrorMessage") as ExLabel, Helper.GetLabelIDValue(5000043));
                                this.ShowHideGrids(importType, dt);
                                #endregion
                                break;
                            case (short)ARTEnums.DataImportType.CurrencyExchangeRateData:
                                #region "Currency Exchange Rate & Period Not Started"
                                //Check for mandatory sheet name                             
                                SheetName = DataImportHelper.GetSheetName(ARTEnums.DataImportType.CurrencyExchangeRateData, ImportTemplateID);
                                if (!SharedDataImportHelper.IsDataSheetPresent(filePath, validFile.GetExtension(), SheetName))
                                    throw new Exception(String.Format(Helper.GetLabelIDValue(5000256), SheetName));
                                if (DataImportHelper.IsAllMandatoryColumnsPresentForDataImport(ARTEnums.DataImportType.CurrencyExchangeRateData, filePath, validFile.GetExtension(), ImportTemplateID, SheetName, out errorMessage))
                                {
                                    if (CurrentRecProcessStatus == WebEnums.RecPeriodStatus.NotStarted)
                                    {
                                        dt = DataImportHelper.GetDataTableFromExcel(filePath, validFile.GetExtension(), importType, oSBErrors);
                                        if (dt.Select("IsDuplicate = true").Length > 0)//if duplicate, show message for duplicates
                                            Helper.FormatAndShowErrorMessage(this.Page.Master.FindControl("lblErrorMessage") as ExLabel, Helper.GetLabelIDValue(5000043));
                                        this.ShowHideGrids(importType, dt);
                                    }
                                    else if (CurrentRecProcessStatus == WebEnums.RecPeriodStatus.Open || currectRecPeriodStatus == WebEnums.RecPeriodStatus.InProgress)
                                    {
                                        // Create a Data Import Info
                                        DataImportHdrInfo oDataImportHrdInfo = new DataImportHdrInfo();
                                        ImportFileAttributes file = new ImportFileAttributes();
                                        if (ViewState[ViewStateConstants.IMPORTFILEATTRIBUTES] != null)
                                        {
                                            file = (ImportFileAttributes)ViewState[ViewStateConstants.IMPORTFILEATTRIBUTES];
                                        }
                                        oDataImportHrdInfo.DataImportName = this.txtProfileName.Text;
                                        oDataImportHrdInfo.FileName = file.FileOriginalName;
                                        oDataImportHrdInfo.PhysicalPath = file.FilePhysicalPath;
                                        oDataImportHrdInfo.IsMultiVersionUpload = true;
                                        oDataImportHrdInfo.DataImportTypeID = (short)ARTEnums.DataImportType.CurrencyExchangeRateData;
                                        Session[SessionConstants.GLDATA_INFO] = oDataImportHrdInfo;
                                        oDataImport = RemotingHelper.GetDataImportObject();
                                        this.InsertDataImportHdrWithFailureMsg(Helper.GetLabelIDValue((int)WebEnums.DataImportStatusLabelID.Submitted)
                                                , (short)ARTEnums.DataImportType.CurrencyExchangeRateData
                                                , (short)WebEnums.DataImportStatus.Submitted, ImportTemplateID);
                                        CacheHelper.ClearExchangeRateByRecPeriodID(SessionHelper.CurrentReconciliationPeriodID.GetValueOrDefault());
                                        oMasterPageBase.ShowConfirmationMessage(1565);
                                    }
                                }
                                else
                                {
                                    //Helper.FormatAndShowErrorMessage(this.Page.Master.FindControl("lblErrorMessage") as ExLabel, errorMessage);
                                    throw new Exception(errorMessage);
                                }
                                #endregion
                                break;
                            case (short)ARTEnums.DataImportType.GLData:

                                #region "GLData Upload"
                                //Check for mandatory sheet name
                                if (validFile.GetExtension().ToLower() != FileExtensions.csv)
                                {
                                    SheetName = DataImportHelper.GetSheetName(ARTEnums.DataImportType.GLData, ImportTemplateID);
                                    if (!SharedDataImportHelper.IsDataSheetPresent(filePath, validFile.GetExtension(), SheetName))
                                        throw new Exception(String.Format(Helper.GetLabelIDValue(5000256), SheetName));
                                }

                                if (DataImportHelper.IsAllMandatoryColumnsPresentForDataImport(ARTEnums.DataImportType.GLData, filePath, validFile.GetExtension(), ImportTemplateID, SheetName, out errorMessage))
                                {
                                    // Create a Data Import Info
                                    DataImportHdrInfo oDataImportHrdInfo = new DataImportHdrInfo();
                                    ImportFileAttributes file = new ImportFileAttributes();
                                    if (ViewState[ViewStateConstants.IMPORTFILEATTRIBUTES] != null)
                                    {
                                        file = (ImportFileAttributes)ViewState[ViewStateConstants.IMPORTFILEATTRIBUTES];
                                    }
                                    oDataImportHrdInfo.DataImportName = this.txtProfileName.Text;
                                    oDataImportHrdInfo.FileName = file.FileOriginalName;
                                    oDataImportHrdInfo.PhysicalPath = file.FilePhysicalPath;
                                    oDataImportHrdInfo.ImportTemplateID = ImportTemplateID;

                                    //****** for multivirsionGL
                                    if (CurrentRecProcessStatus.Value == WebEnums.RecPeriodStatus.InProgress || CurrentRecProcessStatus.Value == WebEnums.RecPeriodStatus.Open)
                                        oDataImportHrdInfo.IsMultiVersionUpload = true;
                                    else
                                        oDataImportHrdInfo.IsMultiVersionUpload = false;
                                    oDataImportHrdInfo.DataImportTypeID = (short)ARTEnums.DataImportType.GLData;
                                    Session[SessionConstants.GLDATA_INFO] = oDataImportHrdInfo;

                                    oDataImport = RemotingHelper.GetDataImportObject();
                                    keyCount = oDataImport.isKeyMappingDoneByCompanyID(SessionHelper.CurrentCompanyID.Value, Helper.GetAppUserInfo());

                                    //**********By Prafull on 26-Apr-2011
                                    //**********Check whether any Account is associated for him in case the Business Admin is uploading the GLData file
                                    if (SessionHelper.CurrentRoleID == (short)WebEnums.UserRole.BUSINESS_ADMIN)
                                    {
                                        CheckWhetherAnyAccountAssociatedForGivenUser(oDataImport);

                                    }
                                    //***************************************************************************************************


                                    if (!keyCount.HasValue)
                                    {
                                        if (DataImportHelper.IsKeyFieldAvailable(filePath, validFile.GetExtension(), WebConstants.GLDATA_SHEETNAME, ARTEnums.DataImportType.GLData))
                                            Response.Redirect("~/Pages/DataImportGLMapping.aspx", false);
                                        else
                                        {
                                            this.InsertGLDataImportHdrWithFailureMsgWithKeyMapping(Helper.GetLabelIDValue((int)WebEnums.DataImportStatusLabelID.Submitted)
                                            , (short)WebEnums.DataImportStatus.Submitted, 0, ARTEnums.DataImportType.GLData, ImportTemplateID);
                                            oMasterPageBase.ShowConfirmationMessage(1565);
                                        }
                                    }

                                    else
                                    {
                                        this.InsertDataImportHdrWithFailureMsg(Helper.GetLabelIDValue((int)WebEnums.DataImportStatusLabelID.Submitted)
                                                , (short)ARTEnums.DataImportType.GLData
                                                , (short)WebEnums.DataImportStatus.Submitted, ImportTemplateID);
                                        oMasterPageBase.ShowConfirmationMessage(1565);
                                    }
                                }
                                else
                                {
                                    //Helper.FormatAndShowErrorMessage(this.Page.Master.FindControl("lblErrorMessage") as ExLabel, errorMessage);
                                    throw new Exception(errorMessage);
                                }
                                #endregion
                                break;
                            case (short)ARTEnums.DataImportType.SubledgerData:

                                #region "Subledgar Data Upload"
                                //Check for mandatory sheet name
                                if (validFile.GetExtension().ToLower() != FileExtensions.csv)
                                {
                                    SheetName = DataImportHelper.GetSheetName(ARTEnums.DataImportType.SubledgerData, ImportTemplateID);
                                    if (!SharedDataImportHelper.IsDataSheetPresent(filePath, validFile.GetExtension(), SheetName))
                                        throw new Exception(String.Format(Helper.GetLabelIDValue(5000256), SheetName));
                                }
                                //Session[SessionConstants.GLDATA_UPLOADFILE_PHYSICALPATH] = filePath;
                                if (DataImportHelper.IsGLDataUploaded(SessionHelper.CurrentReconciliationPeriodID.Value, ARTEnums.DataImportType.GLData, WebEnums.DataImportStatus.Success))
                                {
                                    if (DataImportHelper.IsAllMandatoryColumnsPresentForDataImport(ARTEnums.DataImportType.SubledgerData, filePath, validFile.GetExtension(), ImportTemplateID, SheetName, out errorMessage))
                                    {
                                        oDataImport = RemotingHelper.GetDataImportObject();
                                        keyCount = oDataImport.isKeyMappingDoneByCompanyID(SessionHelper.CurrentCompanyID.Value, Helper.GetAppUserInfo());
                                        if (keyCount.HasValue)
                                        {
                                            this.InsertDataImportHdrWithFailureMsg(Helper.GetLabelIDValue((int)WebEnums.DataImportStatusLabelID.Submitted)
                                                , (short)ARTEnums.DataImportType.SubledgerData
                                                , (short)WebEnums.DataImportStatus.Submitted, ImportTemplateID);
                                            oMasterPageBase.ShowConfirmationMessage(1565);
                                        }
                                        else
                                            throw new ARTException(5000047);
                                    }
                                    else
                                    {
                                        throw new Exception(errorMessage);
                                    }
                                }
                                else
                                {
                                    //Helper.FormatAndShowErrorMessage(this.Page.Master.FindControl("lblErrorMessage") as ExLabel, LanguageUtil.GetValue (5000210));
                                    throw new Exception(LanguageUtil.GetValue(5000210));
                                }
                                #endregion
                                break;
                            case (short)ARTEnums.DataImportType.AccountAttributeList:

                                #region "Account Attribute Upload"
                                //Check for mandatory sheet name
                                SheetName = DataImportHelper.GetSheetName(ARTEnums.DataImportType.AccountAttributeList, ImportTemplateID);
                                if (!SharedDataImportHelper.IsDataSheetPresent(filePath, validFile.GetExtension(), SheetName))
                                    throw new Exception(String.Format(Helper.GetLabelIDValue(5000256), SheetName));

                                if (DataImportHelper.IsAllMandatoryColumnsPresentForDataImport(ARTEnums.DataImportType.AccountAttributeList, filePath, validFile.GetExtension(), ImportTemplateID, SheetName, out errorMessage))
                                {
                                    oDataImport = RemotingHelper.GetDataImportObject();
                                    keyCount = oDataImport.isKeyMappingDoneByCompanyID(SessionHelper.CurrentCompanyID.Value, Helper.GetAppUserInfo());
                                    if (keyCount.HasValue)
                                    {
                                        this.InsertDataImportHdrWithFailureMsg(Helper.GetLabelIDValue((int)WebEnums.DataImportStatusLabelID.Submitted)
                                            , (short)ARTEnums.DataImportType.AccountAttributeList
                                            , (short)WebEnums.DataImportStatus.Submitted, ImportTemplateID);
                                        oMasterPageBase.ShowConfirmationMessage(1565);
                                    }
                                    else
                                        throw new ARTException(5000215);//Bug#6722
                                }
                                else
                                    Helper.FormatAndShowErrorMessage(this.Page.Master.FindControl("lblErrorMessage") as ExLabel, errorMessage);
                                #endregion
                                break;
                            case (short)ARTEnums.DataImportType.AccountUpload:

                                #region "Account Upload"
                                //Check for mandatory sheet name                             
                                SheetName = DataImportHelper.GetSheetName(ARTEnums.DataImportType.AccountUpload, ImportTemplateID);
                                if (!SharedDataImportHelper.IsDataSheetPresent(filePath, validFile.GetExtension(), SheetName))
                                    throw new Exception(String.Format(Helper.GetLabelIDValue(5000256), SheetName));
                                if (DataImportHelper.IsAllMandatoryColumnsPresentForDataImport(ARTEnums.DataImportType.AccountUpload, filePath, validFile.GetExtension(), ImportTemplateID, SheetName, out errorMessage))
                                {
                                    // Create a Data Import Info
                                    DataImportHdrInfo oDataImportHrdInfo = new DataImportHdrInfo();
                                    ImportFileAttributes file = new ImportFileAttributes();
                                    if (ViewState[ViewStateConstants.IMPORTFILEATTRIBUTES] != null)
                                    {
                                        file = (ImportFileAttributes)ViewState[ViewStateConstants.IMPORTFILEATTRIBUTES];
                                    }
                                    oDataImportHrdInfo.DataImportName = this.txtProfileName.Text;
                                    oDataImportHrdInfo.FileName = file.FileOriginalName;
                                    oDataImportHrdInfo.PhysicalPath = file.FilePhysicalPath;
                                    oDataImportHrdInfo.IsMultiVersionUpload = false;
                                    oDataImportHrdInfo.DataImportTypeID = (short)ARTEnums.DataImportType.AccountUpload;
                                    Session[SessionConstants.GLDATA_INFO] = oDataImportHrdInfo;
                                    oDataImport = RemotingHelper.GetDataImportObject();
                                    keyCount = oDataImport.isKeyMappingDoneByCompanyID(SessionHelper.CurrentCompanyID.Value, Helper.GetAppUserInfo());
                                    if (!keyCount.HasValue)
                                    {
                                        if (DataImportHelper.IsKeyFieldAvailable(filePath, validFile.GetExtension(), WebConstants.MAPPINGUPLOAD_SHEETNAME, ARTEnums.DataImportType.AccountUpload))
                                            Response.Redirect("~/Pages/DataImportGLMapping.aspx", false);
                                        else
                                        {
                                            this.InsertGLDataImportHdrWithFailureMsgWithKeyMapping(Helper.GetLabelIDValue((int)WebEnums.DataImportStatusLabelID.Submitted)
                                            , (short)WebEnums.DataImportStatus.Submitted, 0, ARTEnums.DataImportType.AccountUpload, ImportTemplateID);
                                            oMasterPageBase.ShowConfirmationMessage(1565);
                                        }
                                    }

                                    else
                                    {
                                        this.InsertDataImportHdrWithFailureMsg(Helper.GetLabelIDValue((int)WebEnums.DataImportStatusLabelID.Submitted)
                                                , (short)ARTEnums.DataImportType.AccountUpload
                                                , (short)WebEnums.DataImportStatus.Submitted, ImportTemplateID);
                                        oMasterPageBase.ShowConfirmationMessage(1565);
                                    }
                                }
                                else
                                {
                                    //Helper.FormatAndShowErrorMessage(this.Page.Master.FindControl("lblErrorMessage") as ExLabel, errorMessage);
                                    throw new Exception(errorMessage);
                                }
                                #endregion
                                break;
                            case (short)ARTEnums.DataImportType.MultilingualUpload:
                                #region "Multilingual Upload"
                                //Check for mandatory sheet name

                                SheetName = DataImportHelper.GetSheetName(ARTEnums.DataImportType.MultilingualUpload, ImportTemplateID);
                                if (!SharedDataImportHelper.IsDataSheetPresent(filePath, validFile.GetExtension(), SheetName))
                                    throw new Exception(String.Format(Helper.GetLabelIDValue(5000256), SheetName));
                                if (DataImportHelper.IsAllMandatoryColumnsPresentForDataImport(ARTEnums.DataImportType.MultilingualUpload, filePath, validFile.GetExtension(), ImportTemplateID, SheetName, out errorMessage))
                                {
                                    // Create a Data Import Info
                                    DataImportHdrInfo oDataImportHrdInfo = new DataImportHdrInfo();
                                    ImportFileAttributes file = new ImportFileAttributes();
                                    if (ViewState[ViewStateConstants.IMPORTFILEATTRIBUTES] != null)
                                    {
                                        file = (ImportFileAttributes)ViewState[ViewStateConstants.IMPORTFILEATTRIBUTES];
                                    }
                                    oDataImportHrdInfo.DataImportName = this.txtProfileName.Text;
                                    oDataImportHrdInfo.FileName = file.FileOriginalName;
                                    oDataImportHrdInfo.PhysicalPath = file.FilePhysicalPath;
                                    oDataImportHrdInfo.IsMultiVersionUpload = false;
                                    oDataImportHrdInfo.DataImportTypeID = (short)ARTEnums.DataImportType.MultilingualUpload;
                                    Session[SessionConstants.GLDATA_INFO] = oDataImportHrdInfo;
                                    oDataImport = RemotingHelper.GetDataImportObject();
                                    this.InsertDataImportHdrWithFailureMsg(Helper.GetLabelIDValue((int)WebEnums.DataImportStatusLabelID.Submitted)
                                            , (short)ARTEnums.DataImportType.MultilingualUpload
                                            , (short)WebEnums.DataImportStatus.Submitted, ImportTemplateID);
                                    oMasterPageBase.ShowConfirmationMessage(1565);
                                }
                                else
                                {
                                    //Helper.FormatAndShowErrorMessage(this.Page.Master.FindControl("lblErrorMessage") as ExLabel, errorMessage);
                                    throw new Exception(errorMessage);
                                }
                                #endregion
                                break;
                            case (short)ARTEnums.DataImportType.UserUpload:
                                #region User Upload
                                if (DataImportHelper.IsAllMandatoryColumnsPresentForDataImport(ARTEnums.DataImportType.UserUpload, filePath, validFile.GetExtension(), ImportTemplateID, null, out errorMessage))
                                {
                                    this.InsertDataImportHdrWithFailureMsg(Helper.GetLabelIDValue((int)WebEnums.DataImportStatusLabelID.Submitted)
                                                , (short)ARTEnums.DataImportType.UserUpload
                                                , (short)WebEnums.DataImportStatus.Submitted, ImportTemplateID);
                                    oMasterPageBase.ShowConfirmationMessage(1565);
                                }
                                else
                                {
                                    //Helper.FormatAndShowErrorMessage(this.Page.Master.FindControl("lblErrorMessage") as ExLabel, errorMessage);
                                    throw new Exception(errorMessage);
                                }
                                #endregion
                                break;
                            case (short)ARTEnums.DataImportType.RecControlChecklist:
                                #region Rec Control Checklist Upload
                                dt = DataImportHelper.GetDataTableFromExcel(filePath, validFile.GetExtension(), importType, oSBErrors);
                                if (dt.Select("IsDuplicate = true").Length > 0)//if duplicate, show message for duplicates
                                    Helper.FormatAndShowErrorMessage(this.Page.Master.FindControl("lblErrorMessage") as ExLabel, Helper.GetLabelIDValue(5000043));
                                this.ShowHideGrids(importType, dt);
                                //if (DataImportHelper.IsAllMandatoryColumnsPresentForDataImport(ARTEnums.DataImportType.RecControlChecklist, filePath, validFile.GetExtension(), out errorMessage))
                                //{
                                //    this.InsertDataImportHdrWithFailureMsg(Helper.GetLabelIDValue((int)WebEnums.DataImportStatusLabelID.Submitted)
                                //                , (short)ARTEnums.DataImportType.RecControlChecklist
                                //                , (short)WebEnums.DataImportStatus.Submitted);
                                //    oMasterPageBase.ShowConfirmationMessage(1565);
                                //}
                                //else
                                //{
                                //    //Helper.FormatAndShowErrorMessage(this.Page.Master.FindControl("lblErrorMessage") as ExLabel, errorMessage);
                                //    throw new Exception(errorMessage);
                                //}
                                #endregion
                                break;
                        }
                    }
                }
                catch (HttpException exHttp)
                {
                    this.DeleteFile(filePath);
                    if (ViewState[ViewStateConstants.IMPORTFILEATTRIBUTES] != null)
                        ViewState.Remove(ViewStateConstants.IMPORTFILEATTRIBUTES);
                    //Helper.FormatAndShowErrorMessage(this.Page.Master.FindControl("lblErrorMessage") as ExLabel, ex);
                    Helper.LogException(exHttp);
                }
                catch (ARTException ex)
                {
                    switch (ex.ExceptionPhraseID)
                    {
                        //Invalid File. All Mandatory fields not present
                        //Save import and failure message to database
                        case 5000037:
                            InsertDataImportHdrWithFailureMsg(Helper.GetLabelIDValue(5000037), importType, (short)WebEnums.DataImportStatus.Failure, ImportTemplateID);
                            ViewState.Remove(ViewStateConstants.IMPORTFILEATTRIBUTES);
                            break;
                        //Invalid Data. 
                        //Save import and failure message to database
                        case 5000047:
                            InsertDataImportHdrWithFailureMsg(oSBErrors.ToString(), importType, (short)WebEnums.DataImportStatus.Failure, ImportTemplateID);
                            ViewState.Remove(ViewStateConstants.IMPORTFILEATTRIBUTES);
                            break;
                    }
                    //Helper.FormatAndShowErrorMessage(this.Page.Master.FindControl("lblErrorMessage") as ExLabel, ex);
                    Helper.LogException(ex);
                    Helper.ShowErrorMessage(this, ex);
                }
                catch (Exception ex)
                {
                    this.DeleteFile(filePath);
                    if (ViewState[ViewStateConstants.IMPORTFILEATTRIBUTES] != null)
                        ViewState.Remove(ViewStateConstants.IMPORTFILEATTRIBUTES);
                    //Helper.FormatAndShowErrorMessage(this.Page.Master.FindControl("lblErrorMessage") as ExLabel, ex);
                    Helper.LogException(ex);
                    Helper.ShowErrorMessage(this, ex);
                }
                finally
                {
                    this.RadFileUpload.UploadedFiles.Clear();
                }
            }
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        trButtons.Visible = false;
        rowDataImportGrid.Visible = false;
        ClearDataImport();

        //rowPeriodEndDate.Visible = false;
    }

    protected void btnImport_Click(object sender, EventArgs e)
    {
        try
        {
            MasterPageBase oMasterPageBase = (MasterPageBase)this.Master;
            Sel.Value = "";
            bool flag = false;
            switch ((ARTEnums.DataImportType)Convert.ToInt16(this.ddlImportType.SelectedValue))
            {
                case ARTEnums.DataImportType.HolidayCalendar:
                    this.ImportHolidayCalendar();
                    flag = true;
                    break;
                case ARTEnums.DataImportType.CurrencyExchangeRateData:
                    this.ImportCurrency();
                    flag = true;
                    break;
                case ARTEnums.DataImportType.PeriodEndDates:
                    this.ImportPeriodEndDate();
                    flag = true;

                    /* Uploaded Successfully, 
                     * Now, reload Master Page Dropdown
                     * 
                     */
                    oMasterPageBase.ReloadRecPeriods();
                    break;
                case ARTEnums.DataImportType.SubledgerSource:
                    this.ImportSubledgerSource();
                    flag = true;
                    break;
                case ARTEnums.DataImportType.RecControlChecklist:
                    this.ImportRecControlChecklist();
                    flag = true;
                    break;
            }
            if (flag)
            {
                trButtons.Visible = false;
                rowDataImportGrid.Visible = false;
                oMasterPageBase.ShowConfirmationMessage(1608);
                clearcontrolsAfterUpload();
            }
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

    protected void ddlImportType_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.HideAllGrids();
        if (ViewState[ViewStateConstants.IMPORTFILEATTRIBUTES] != null)
        {
            ImportFileAttributes file = (ImportFileAttributes)ViewState[ViewStateConstants.IMPORTFILEATTRIBUTES];
            this.DeleteFile(file.FilePhysicalPath);
            ViewState.Remove(ViewStateConstants.IMPORTFILEATTRIBUTES);
        }
        short? importType = Convert.ToInt16(this.ddlImportType.SelectedValue);
        if (importType.HasValue)
        {
            //trDataImportTemaplate.Visible = true;
            ListControlHelper.BindDataimportTemplate(ddlDataimportTemplate, importType);
        }
        //else
        //{
        //    trDataImportTemaplate.Visible = false;
        //}
        MasterPageBase oMasterPageBase = (MasterPageBase)this.Master;
        oMasterPageBase.HideMessage();

    }

    protected void ompage_ReconciliationPeriodChangedEventHandler(object sender, EventArgs e)
    {
        IGLDataRecItem oGLRecItemInput = RemotingHelper.GetGLDataRecItemObject();
        SetImportTypeDDl();
        //if (SkyStem.ART.Web.Utility.SessionHelper.CurrentReconciliationPeriodID.HasValue)
        //    recPeriodID = SkyStem.ART.Web.Utility.SessionHelper.CurrentReconciliationPeriodID.Value;
        ListControlHelper.BindLanguageDropdown(ddlFromLanguage, true, false, true);
        ListControlHelper.BindLanguageDropdown(ddlToLanguage, false, false, true);
        short? importType = Convert.ToInt16(this.ddlImportType.SelectedValue);
        if (importType.HasValue)
        {
            //trDataImportTemaplate.Visible = true;
            ListControlHelper.BindDataimportTemplate(ddlDataimportTemplate, importType);
        }
        MasterPageBase oMasterPageBase = (MasterPageBase)this.Master;
        oMasterPageBase.HideMessage();
    }
    #endregion

    #region Validation Control Events
    protected void cvFileUpload_ServerValidate(object source, ServerValidateEventArgs args)
    {
        if (this.RadFileUpload.InvalidFiles.Count > 0)
        {
            //Set default error message
            this.cvFileUpload.ErrorMessage = LanguageUtil.GetValue(5000037);

            //Check if there is enough space for this company to save this file
            UploadedFile invalidFile = RadFileUpload.InvalidFiles[0];
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
            trButtons.Visible = false;
            rowDataImportGrid.Visible = false;
        }

    }
    #endregion

    #region Private Methods
    /// <summary>
    /// Removes duplicate rows based on "duplicateRowsPresent" param, hides other grids
    /// ,shows releveant grid based on "dataImportTypeID" param after binding it to datasource
    /// based on "dt" param
    /// </summary>
    /// <param name="dataImportTypeID">DataImporty Type ID</param>
    /// <param name="dt">Datatable for datasource of grid</param>
    /// <param name="duplicateRowsPresent">if duplicate rows present or not</param>
    private void ShowHideGrids(short dataImportTypeID, DataTable dt)
    {
        this.HideAllGrids();
        DataView dv = dt.DefaultView;
        switch ((ARTEnums.DataImportType)dataImportTypeID)
        {
            case ARTEnums.DataImportType.HolidayCalendar:
                rowDataImportGrid.Visible = true;
                trButtons.Visible = true;
                this.pnlHolidayCal.Visible = true;
                dv.RowFilter = "IsDuplicate = false";
                this.rgHolidayCal.DataSource = dv;
                this.rgHolidayCal.DataBind();
                break;

            case ARTEnums.DataImportType.CurrencyExchangeRateData:
                rowDataImportGrid.Visible = true;
                trButtons.Visible = true;
                this.pnlCurrency.Visible = true;
                dv.RowFilter = "IsDuplicate = false";
                this.rgCurrency.DataSource = dv;
                this.rgCurrency.DataBind();
                break;

            case ARTEnums.DataImportType.PeriodEndDates:
                IReconciliationPeriod oRecPeriod = RemotingHelper.GetReconciliationPeriodObject();
                ReconciliationPeriodInfo oReconciliationPeriodInfo = oRecPeriod.GetMaxCurrentPeriodByCompanyId(SessionHelper.CurrentCompanyID, Helper.GetAppUserInfo());
                this._openPeriod = oReconciliationPeriodInfo.PeriodEndDate;
                rowDataImportGrid.Visible = true;
                trButtons.Visible = true;
                this.pnlPeriodEndDate.Visible = true;
                dv.RowFilter = "IsDuplicate = false";
                this.rgPeriodEndDate.DataSource = dv;
                this.rgPeriodEndDate.DataBind();
                break;

            case ARTEnums.DataImportType.SubledgerSource:
                rowDataImportGrid.Visible = true;
                trButtons.Visible = true;
                this.pnlSubLedger.Visible = true;
                dv.RowFilter = "IsDuplicate = false";
                this.rgSubLedger.DataSource = dt;
                this.rgSubLedger.DataBind();
                break;

            case ARTEnums.DataImportType.RecControlChecklist:
                rowDataImportGrid.Visible = true;
                trButtons.Visible = true;
                this.pnlRecControlChecklist.Visible = true;
                dv.RowFilter = "IsDuplicate = false";
                this.rgRecControlChecklist.DataSource = dt;
                this.rgRecControlChecklist.DataBind();
                break;
        }

    }

    /// <summary>
    /// Insert records for holiday calendar to database
    /// Table affected: DataIMportHDR, HolidayCalendar, DataImportFailureMessage
    /// </summary>
    private void ImportHolidayCalendar()
    {
        GridDataItem[] selectedItems = this.rgHolidayCal.MasterTableView.GetSelectedItems();
        IDataImport oDataImport = RemotingHelper.GetDataImportObject();
        DataImportHdrInfo oDataImportHrdInfo = new DataImportHdrInfo();
        List<HolidayCalendarInfo> oHolidayCalendarInfoCollection = new List<HolidayCalendarInfo>();

        this.GetDataImportHdrInfo(oDataImportHrdInfo, (short)ARTEnums.DataImportType.HolidayCalendar, selectedItems.Length);

        #region
        foreach (GridDataItem item in selectedItems)
        {
            ExLabel lblDate = (ExLabel)item.FindControl("lblDate");
            ExLabel lblHolidayName = (ExLabel)item.FindControl("lblHolidayName");

            HolidayCalendarInfo oHolidayCalendar = new HolidayCalendarInfo();
            oHolidayCalendar.HolidayName = lblHolidayName.Text;
            oHolidayCalendar.HolidayDate = Convert.ToDateTime(lblDate.Text);
            //TODO: Needs to be modified
            oHolidayCalendar.HolidayNameLabelID = LanguageUtil.InsertPhrase(lblHolidayName.Text, null
                , AppSettingHelper.GetApplicationID(), SessionHelper.CurrentCompanyID.Value
                , SessionHelper.GetUserLanguage(), 4, null);
            //GeographyObjectId is currently CompanyID
            oHolidayCalendar.GeographyObjectID = SessionHelper.CurrentCompanyID;
            oHolidayCalendar.AddedBy = SessionHelper.GetCurrentUser().LoginID;
            oHolidayCalendar.DateAdded = DateTime.Now;
            oHolidayCalendar.IsActive = true;

            oHolidayCalendarInfoCollection.Add(oHolidayCalendar);
            oHolidayCalendar = null;
        }
        #endregion

        try
        {
            int rowAffected = 0;
            oDataImportHrdInfo.RoleID = SessionHelper.CurrentRoleID;
            oDataImport.InsertDataImportHolidayCalendar(oDataImportHrdInfo, oHolidayCalendarInfoCollection, Helper.GetLabelIDValue((int)WebEnums.DataImportStatusLabelID.Success), out rowAffected, Helper.GetAppUserInfo());
            SendMailOnSucess(oDataImportHrdInfo, rowAffected);
        }
        catch (ARTException ex)
        {
            SendMailOnFailure(oDataImportHrdInfo, ex.Message);
            throw ex;
            //Helper.ShowErrorMessage(this, ex);

        }
        catch (Exception ex)
        {
            SendMailOnFailure(oDataImportHrdInfo, ex.Message);
            throw ex;
            //Helper.ShowErrorMessage(this, ex);
        }


    }

    /// <summary>
    /// Insert records for Period End Date to database
    /// Table affected: DataIMportHDR, ReconciliationPeriod, DataImportFailureMessage
    /// </summary>
    private void ImportPeriodEndDate()
    {
        GridDataItem[] selectedItems = this.rgPeriodEndDate.MasterTableView.GetSelectedItems();
        IDataImport oDataImport = RemotingHelper.GetDataImportObject();
        DataImportHdrInfo oDataImportHrdInfo = new DataImportHdrInfo();
        List<ReconciliationPeriodInfo> oRecPeriodInfoCollection = new List<ReconciliationPeriodInfo>();

        this.GetDataImportHdrInfo(oDataImportHrdInfo, (short)ARTEnums.DataImportType.PeriodEndDates, selectedItems.Length);

        #region
        foreach (GridDataItem item in selectedItems)
        {
            ExLabel lblReconciliationPeriodNumber = (ExLabel)item.FindControl("lblReconciliationPeriodNumber");
            ExLabel lblPeriodEnddate = (ExLabel)item.FindControl("lblPeriodEnddate");

            ReconciliationPeriodInfo oRecPeriodInfo = new ReconciliationPeriodInfo();
            oRecPeriodInfo.CompanyID = SessionHelper.CurrentCompanyID;
            oRecPeriodInfo.PeriodNumber = Convert.ToInt16(lblReconciliationPeriodNumber.Text);
            oRecPeriodInfo.PeriodEndDate = Convert.ToDateTime(lblPeriodEnddate.Text);
            oRecPeriodInfo.ReconciliationPeriodStatusID = (short)WebEnums.RecPeriodStatus.NotStarted;
            oRecPeriodInfo.IsActive = true;
            oRecPeriodInfo.DateAdded = DateTime.Now;
            oRecPeriodInfo.AddedBy = SessionHelper.GetCurrentUser().LoginID;

            oRecPeriodInfoCollection.Add(oRecPeriodInfo);
            oRecPeriodInfo = null;
        }
        #endregion

        try
        {
            int rowAffected = 0;
            oDataImportHrdInfo.RoleID = SessionHelper.CurrentRoleID;
            oDataImport.InsertDataImportRecPeriod(oDataImportHrdInfo, oRecPeriodInfoCollection, Helper.GetLabelIDValue((int)WebEnums.DataImportStatusLabelID.Success), SessionHelper.CurrentReconciliationPeriodEndDate, out rowAffected, Helper.GetAppUserInfo());
            SendMailOnSucess(oDataImportHrdInfo, rowAffected);
        }
        catch (ARTException ex)
        {
            SendMailOnFailure(oDataImportHrdInfo, ex.Message);
            throw ex;
            //Helper.ShowErrorMessage(this, ex);

        }
        catch (Exception ex)
        {
            SendMailOnFailure(oDataImportHrdInfo, ex.Message);
            throw ex;
            //Helper.ShowErrorMessage(this, ex);
        }

        //oDataImport.InsertDataImportRecPeriod(oDataImportHrdInfo, oRecPeriodInfoCollection
        //  , Helper.GetLabelIDValue((int)WebEnums.DataImportStatusLabelID.Success ));

    }

    private void ImportCurrency()
    {
        GridDataItem[] selectedItems = this.rgCurrency.MasterTableView.GetSelectedItems();
        IDataImport oDataImport = RemotingHelper.GetDataImportObject();
        DataImportHdrInfo oDataImportHrdInfo = new DataImportHdrInfo();
        List<ExchangeRateInfo> oExchangeRateInfoCollection = new List<ExchangeRateInfo>();
        List<CurrencyCodeInfo> oCurrencyCodeInfoCollection = new List<CurrencyCodeInfo>();

        this.GetDataImportHdrInfo(oDataImportHrdInfo, (short)ARTEnums.DataImportType.CurrencyExchangeRateData, selectedItems.Length);

        int recPeriod = 0;

        #region
        foreach (GridDataItem item in selectedItems)
        {
            ExLabel lblPeriod = (ExLabel)item.FindControl("lblPeriod");
            ExLabel lblFromCurrency = (ExLabel)item.FindControl("lblFromCurrency");
            ExLabel lblToCurrency = (ExLabel)item.FindControl("lblToCurrency");
            ExLabel lblRate = (ExLabel)item.FindControl("lblRate");
            recPeriod = CheckRecPeriodandGetRecID(lblPeriod.Text);
            ExchangeRateInfo oExchangeRateInfo = new ExchangeRateInfo();
            oExchangeRateInfo.ReconciliationPeriodID = recPeriod;
            oExchangeRateInfo.FromCurrencyCode = lblFromCurrency.Text;
            oExchangeRateInfo.ToCurrencyCode = lblToCurrency.Text;
            oExchangeRateInfo.ExchangeRate = Convert.ToDecimal(lblRate.Text);
            //TODO: Needs to be modified
            //oHolidayCalendar.HolidayNameLabelID = LanguageUtil.InsertPhrase(lblHolidayName.Text, null
            //    , AppSettingHelper.GetApplicationID(), SessionHelper.CurrentCompanyID.Value
            //    , SessionHelper.GetUserLanguage(), 4);
            //GeographyObjectId is currently CompanyID
            //oHolidayCalendar.GeographyObjectID = SessionHelper.CurrentCompanyID;
            oExchangeRateInfo.AddedBy = SessionHelper.GetCurrentUser().LoginID;
            oExchangeRateInfo.DateAdded = DateTime.Now;
            oExchangeRateInfo.IsActive = true;
            oExchangeRateInfoCollection.Add(oExchangeRateInfo);
            oExchangeRateInfo = null;

            CurrencyCodeInfo oCurrencyCodeInfo = new CurrencyCodeInfo();
            oCurrencyCodeInfo.ComapnyID = SessionHelper.CurrentCompanyID;
            oCurrencyCodeInfo.CurrencyCode = lblFromCurrency.Text;
            oCurrencyCodeInfo.AddedBy = SessionHelper.GetCurrentUser().LoginID;
            oCurrencyCodeInfo.DateAdded = DateTime.Now;
            oCurrencyCodeInfo.IsActive = true;
            oCurrencyCodeInfoCollection.Add(oCurrencyCodeInfo);
            oCurrencyCodeInfo = null;
        }
        #endregion
        try
        {
            if (recPeriod > 0)
            {
                oDataImportHrdInfo.ReconciliationPeriodID = recPeriod;
                int rowAffected = 0;
                oDataImportHrdInfo.RoleID = SessionHelper.CurrentRoleID;
                oDataImport.InsertDataImportExchangeRate(oDataImportHrdInfo, oExchangeRateInfoCollection, Helper.GetLabelIDValue(1050), oCurrencyCodeInfoCollection, out rowAffected, Helper.GetAppUserInfo());
                // Clear Cache for Exchange Rate based on the Rec Period
                CacheHelper.ClearExchangeRateByRecPeriodID(recPeriod);
                SendMailOnSucess(oDataImportHrdInfo, rowAffected);
            }
            else
            {
                throw new ARTException(5000076);
                //MasterPageBase oMasterPageBase = (MasterPageBase)this.Master;
                // oMasterPageBase.ShowErrorMessage(5000076);

            }
        }

        catch (ARTException ex)
        {
            SendMailOnFailure(oDataImportHrdInfo, ex.Message);
            throw ex;
            //Helper.ShowErrorMessage(this, ex);

        }
        catch (Exception ex)
        {
            SendMailOnFailure(oDataImportHrdInfo, ex.Message);
            throw ex;
            //Helper.ShowErrorMessage(this, ex);
        }

    }

    private void GetDataImportHdrInfo(DataImportHdrInfo oDataImportHrdInfo, short dataImportTypeID, int recordsImported)
    {
        ImportFileAttributes file = new ImportFileAttributes();
        if (ViewState[ViewStateConstants.IMPORTFILEATTRIBUTES] != null)
        {
            file = (ImportFileAttributes)ViewState[ViewStateConstants.IMPORTFILEATTRIBUTES];
            ViewState.Remove(ViewStateConstants.IMPORTFILEATTRIBUTES);
        }


        oDataImportHrdInfo.DataImportName = this.txtProfileName.Text;
        oDataImportHrdInfo.FileName = file.FileOriginalName;
        oDataImportHrdInfo.PhysicalPath = file.FilePhysicalPath;
        oDataImportHrdInfo.FileSize = file.FileSize;
        oDataImportHrdInfo.CompanyID = SessionHelper.CurrentCompanyID;
        oDataImportHrdInfo.DataImportTypeID = dataImportTypeID;
        oDataImportHrdInfo.DataImportStatusID = (short)WebEnums.DataImportStatus.Success;
        oDataImportHrdInfo.RecordsImported = recordsImported;
        if (dataImportTypeID == (short)ARTEnums.DataImportType.RecControlChecklist)
            oDataImportHrdInfo.ReconciliationPeriodID = SessionHelper.CurrentReconciliationPeriodID;

        oDataImportHrdInfo.IsActive = true;
        oDataImportHrdInfo.DateAdded = DateTime.Now;
        oDataImportHrdInfo.AddedBy = SessionHelper.GetCurrentUser().LoginID;
        SetDataImportHDR(oDataImportHrdInfo);
    }

    private int CheckRecPeriodandGetRecID(string PeriodEndDate)
    {

        List<ReconciliationPeriodInfo> oReconciliationPeriodInfoCollection = CacheHelper.GetAllReconciliationPeriods(null);
        foreach (ReconciliationPeriodInfo objReconciliationPeriodInfo in oReconciliationPeriodInfoCollection)
        {
            if (objReconciliationPeriodInfo.PeriodEndDate == Convert.ToDateTime(PeriodEndDate))
            {
                if (objReconciliationPeriodInfo.ReconciliationPeriodID == SessionHelper.CurrentReconciliationPeriodID)
                {
                    return Convert.ToInt32(objReconciliationPeriodInfo.ReconciliationPeriodID);
                }
                else
                {
                    return 0;
                }
            }
        }
        return 0;
    }

    private void ImportSubledgerSource()
    {
        GridDataItem[] selectedItems = this.rgSubLedger.MasterTableView.GetSelectedItems();
        IDataImport oDataImport = RemotingHelper.GetDataImportObject();
        DataImportHdrInfo oDataImportHrdInfo = new DataImportHdrInfo();
        List<SubledgerSourceInfo> oSubledgerSourceInfoCollection = new List<SubledgerSourceInfo>();

        this.GetDataImportHdrInfo(oDataImportHrdInfo, (short)ARTEnums.DataImportType.SubledgerSource, selectedItems.Length);

        #region
        foreach (GridDataItem item in selectedItems)
        {
            ExLabel lblSubledgerSourceName = (ExLabel)item.FindControl("lblSubledgerSourceName");
            SubledgerSourceInfo oSubledgerSourceInfo = new SubledgerSourceInfo();
            oSubledgerSourceInfo.CompanyID = SessionHelper.CurrentCompanyID;
            oSubledgerSourceInfo.SubledgerSource = lblSubledgerSourceName.Text;
            oSubledgerSourceInfo.SubledgerSourceLabelID = (int)LanguageUtil.InsertPhrase(lblSubledgerSourceName.Text, null, 1, (int)SessionHelper.CurrentCompanyID, SessionHelper.GetUserLanguage(), 4, null);

            //TODO: Needs to be modified
            //oHolidayCalendar.HolidayNameLabelID = LanguageUtil.InsertPhrase(lblHolidayName.Text, null
            //    , AppSettingHelper.GetApplicationID(), SessionHelper.CurrentCompanyID.Value
            //    , SessionHelper.GetUserLanguage(), 4);
            //GeographyObjectId is currently CompanyID
            //oHolidayCalendar.GeographyObjectID = SessionHelper.CurrentCompanyID;
            oSubledgerSourceInfo.CompanyID = SessionHelper.CurrentCompanyID;
            oSubledgerSourceInfo.AddedBy = SessionHelper.GetCurrentUser().LoginID;
            oSubledgerSourceInfo.DateAdded = DateTime.Now;
            oSubledgerSourceInfo.IsActive = true;

            oSubledgerSourceInfoCollection.Add(oSubledgerSourceInfo);
            oSubledgerSourceInfo = null;
        }
        #endregion
        try
        {
            int rowAffected = 0;
            oDataImportHrdInfo.RoleID = SessionHelper.CurrentRoleID;
            oDataImport.InsertDataImportSubledgerSourceRate(oDataImportHrdInfo, oSubledgerSourceInfoCollection, Helper.GetLabelIDValue(1050), out rowAffected, Helper.GetAppUserInfo());
            SendMailOnSucess(oDataImportHrdInfo, rowAffected);
            SessionHelper.ClearAllSubLedgerSources();
        }
        catch (ARTException ex)
        {
            SendMailOnFailure(oDataImportHrdInfo, ex.Message);
            throw ex;
            //Helper.ShowErrorMessage(this, ex);

        }
        catch (Exception ex)
        {
            SendMailOnFailure(oDataImportHrdInfo, ex.Message);
            throw ex;
            //Helper.ShowErrorMessage(this, ex);
        }
    }


    private void ImportRecControlChecklist()
    {
        GridDataItem[] selectedItems = this.rgRecControlChecklist.MasterTableView.GetSelectedItems();
        DataImportHdrInfo oDataImportHrdInfo = new DataImportHdrInfo();
        List<RecControlCheckListInfo> oRecControlCheckListInfoList = new List<RecControlCheckListInfo>();

        this.GetDataImportHdrInfo(oDataImportHrdInfo, (short)ARTEnums.DataImportType.RecControlChecklist, selectedItems.Length);

        #region
        foreach (GridDataItem item in selectedItems)
        {
            ExLabel lblDescription = (ExLabel)item.FindControl("lblDescription");
            RecControlCheckListInfo oRecControlCheckListInfo = new RecControlCheckListInfo();
            oRecControlCheckListInfo.Description = lblDescription.Text;
            oRecControlCheckListInfo.DescriptionLabelID = (int)LanguageUtil.InsertPhrase(lblDescription.Text, null, 1, (int)SessionHelper.CurrentCompanyID, SessionHelper.GetUserLanguage(), 4, null);

            oRecControlCheckListInfo.CompanyID = SessionHelper.CurrentCompanyID;
            oRecControlCheckListInfo.AddedBy = SessionHelper.CurrentUserLoginID;
            oRecControlCheckListInfo.DateAdded = DateTime.Now;
            oRecControlCheckListInfo.AddedByUserID = SessionHelper.CurrentUserID;
            oRecControlCheckListInfo.RoleID = SessionHelper.CurrentRoleID;
            oRecControlCheckListInfo.StartRecPeriodID = SessionHelper.CurrentReconciliationPeriodID;
            oRecControlCheckListInfo.EndRecPeriodID = null;
            oRecControlCheckListInfo.IsActive = true;

            oRecControlCheckListInfoList.Add(oRecControlCheckListInfo);
            oRecControlCheckListInfo = null;
        }
        #endregion
        try
        {
            int rowAffected = 0;
            oDataImportHrdInfo.RoleID = SessionHelper.CurrentRoleID;
            RecControlCheckListHelper.InsertDataImportRecControlChecklist(oDataImportHrdInfo, oRecControlCheckListInfoList, out rowAffected);
            SendMailOnSucess(oDataImportHrdInfo, rowAffected);
        }
        catch (ARTException ex)
        {
            SendMailOnFailure(oDataImportHrdInfo, ex.Message);
            throw ex;
        }
        catch (Exception ex)
        {
            SendMailOnFailure(oDataImportHrdInfo, ex.Message);
            throw ex;
        }
    }


    /// <summary>
    /// Returns url for template as per import type
    /// </summary>
    /// <param name="importType"></param>
    /// <returns>url of import type template</returns>
    private string GetImportTypeTemplateUrl(short importType)
    {
        string url = "";
        switch (importType)
        {
            case (short)ARTEnums.DataImportType.PeriodEndDates:
                url = "~/Templates/PeriodEndDates.xlsx";
                break;
            case (short)ARTEnums.DataImportType.GLData:
                url = "~/Templates/GLDataUpload.xlsx";
                break;
            case (short)ARTEnums.DataImportType.CurrencyExchangeRateData:
                url = "~/Templates/CurrencyExchange.xlsx";
                break;
            case (short)ARTEnums.DataImportType.HolidayCalendar:
                url = "~/Templates/HolidayUpload.xlsx";
                break;
            case (short)ARTEnums.DataImportType.SubledgerSource:
                url = "~/Templates/SubledgerSource.xlsx";
                break;
            case (short)ARTEnums.DataImportType.SubledgerData:
                url = "~/Templates/Subledger.xlsx";
                break;
            case (short)ARTEnums.DataImportType.AccountAttributeList:
                url = "~/Templates/AccountsAttributes.xlsx";
                break;
            case (short)ARTEnums.DataImportType.GLTBS:
                url = "~/Templates/GLTBSDataUpload.xlsx";
                break;
            case (short)ARTEnums.DataImportType.AccountUpload:
                url = "~/Templates/AccountsUpload.xlsx";
                break;
            case (short)ARTEnums.DataImportType.MultilingualUpload:
                url = "javascript:OpenRadWindowForHyperlink('" + Page.ResolveClientUrl("~/Pages/Multilingual/DownloadMultilingualData.aspx") + "',350,400)";
                break;
            case (short)ARTEnums.DataImportType.UserUpload:
                url = "~/Templates/UserUpload.xlsx";
                break;
            case (short)ARTEnums.DataImportType.RecControlChecklist:
                url = "~/Templates/RecControlChecklist.xlsx";
                break;
        }
        return url;
    }

    /// <summary>
    /// Deletes file from specified path.
    /// </summary>
    /// <param name="filePath">file physical path</param>
    /// <param name="exceptionMessage"></param>
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

    /// <summary>
    /// Insert records for failure message to database
    /// Table affected: DataIMportHDR, DataImportFailureMessage
    /// </summary>
    private void InsertDataImportHdrWithFailureMsg(string failureMsg, short dataImportTypeID, short dataImportStatusID, int? ImportTemplateID)
    {
        IDataImport oDataImport = RemotingHelper.GetDataImportObject();
        DataImportHdrInfo oDataImportHrdInfo = new DataImportHdrInfo();
        ImportFileAttributes file = new ImportFileAttributes();
        if (ViewState[ViewStateConstants.IMPORTFILEATTRIBUTES] != null)
        {
            file = (ImportFileAttributes)ViewState[ViewStateConstants.IMPORTFILEATTRIBUTES];
            ViewState.Remove(ViewStateConstants.IMPORTFILEATTRIBUTES);
        }
        oDataImportHrdInfo.ImportTemplateID = ImportTemplateID;
        oDataImportHrdInfo.RoleID = SessionHelper.CurrentRoleID;
        oDataImportHrdInfo.DataImportName = this.txtProfileName.Text;
        oDataImportHrdInfo.FileName = file.FileOriginalName;
        oDataImportHrdInfo.PhysicalPath = file.FilePhysicalPath;
        oDataImportHrdInfo.FileSize = file.FileSize;
        if (SessionHelper.CurrentRoleID == (short)WebEnums.UserRole.SKYSTEM_ADMIN)
        {
            oDataImportHrdInfo.CompanyID = null;
            oDataImportHrdInfo.ReconciliationPeriodID = null;
        }
        else
        {
            oDataImportHrdInfo.CompanyID = SessionHelper.CurrentCompanyID;
            oDataImportHrdInfo.ReconciliationPeriodID = SessionHelper.CurrentReconciliationPeriodID;
        }
        oDataImportHrdInfo.DataImportTypeID = dataImportTypeID;
        oDataImportHrdInfo.DataImportStatusID = dataImportStatusID;
        oDataImportHrdInfo.RecordsImported = 0;
        if (dataImportTypeID == (short)ARTEnums.DataImportType.GLData
            || dataImportTypeID == (short)ARTEnums.DataImportType.SubledgerData
            || dataImportTypeID == (short)ARTEnums.DataImportType.AccountAttributeList
            || dataImportTypeID == (short)ARTEnums.DataImportType.CurrencyExchangeRateData)
        {

            if (CurrentRecProcessStatus.Value == WebEnums.RecPeriodStatus.InProgress || CurrentRecProcessStatus.Value == WebEnums.RecPeriodStatus.Open)
            {
                oDataImportHrdInfo.IsMultiVersionUpload = true;
                oDataImportHrdInfo.SystemLockdownInfo = Helper.GetSystemLockdownInfo(ARTEnums.SystemLockdownReason.UploadDataProcessingRequired);
            }
            else
                oDataImportHrdInfo.IsMultiVersionUpload = false;
        }
        else
        {
            oDataImportHrdInfo.IsMultiVersionUpload = false;
        }
        if (dataImportTypeID == (short)ARTEnums.DataImportType.MultilingualUpload)
        {
            DataImportMultilingualUploadInfo oDataImportMultilingualUploadInfo = new DataImportMultilingualUploadInfo();
            oDataImportMultilingualUploadInfo.FromLanguageID = Convert.ToInt32(ddlFromLanguage.SelectedValue);
            oDataImportMultilingualUploadInfo.ToLanguageID = Convert.ToInt32(ddlToLanguage.SelectedValue);
            oDataImportHrdInfo.DataImportMultilingualUploadInfo = oDataImportMultilingualUploadInfo;
        }
        oDataImportHrdInfo.IsActive = true;
        oDataImportHrdInfo.DateAdded = DateTime.Now;
        oDataImportHrdInfo.AddedBy = SessionHelper.GetCurrentUser().LoginID;
        oDataImportHrdInfo.LanguageID = SessionHelper.GetUserLanguage();
        SetDataImportHDR(oDataImportHrdInfo);
        oDataImport.InsertDataImportWithFailureMsg(oDataImportHrdInfo, failureMsg, Helper.GetAppUserInfo());
    }

    private void InsertGLDataImportHdrWithFailureMsgWithKeyMapping(string failureMsg, short dataImportStatusID, short keyMappingCount, ARTEnums.DataImportType importType, int? ImportTemplateID)
    {
        IDataImport oDataImport = RemotingHelper.GetDataImportObject();
        DataImportHdrInfo oDataImportHrdInfo = new DataImportHdrInfo();
        ImportFileAttributes file = new ImportFileAttributes();
        if (ViewState[ViewStateConstants.IMPORTFILEATTRIBUTES] != null)
        {
            file = (ImportFileAttributes)ViewState[ViewStateConstants.IMPORTFILEATTRIBUTES];
            ViewState.Remove(ViewStateConstants.IMPORTFILEATTRIBUTES);
        }
        oDataImportHrdInfo.ReconciliationPeriodID = SessionHelper.CurrentReconciliationPeriodID;
        switch (importType)
        {

            case ARTEnums.DataImportType.GLData:
                oDataImportHrdInfo.DataImportTypeID = (short)ARTEnums.DataImportType.GLData;

                //****** for multivirsion Load
                if (CurrentRecProcessStatus.Value == WebEnums.RecPeriodStatus.InProgress || CurrentRecProcessStatus.Value == WebEnums.RecPeriodStatus.Open)
                    oDataImportHrdInfo.IsMultiVersionUpload = true;
                else
                    oDataImportHrdInfo.IsMultiVersionUpload = false;
                break;

            case ARTEnums.DataImportType.AccountUpload:
                oDataImportHrdInfo.DataImportTypeID = (short)ARTEnums.DataImportType.AccountUpload;
                break;
        }
        oDataImportHrdInfo.ImportTemplateID = ImportTemplateID;
        oDataImportHrdInfo.SystemLockdownInfo = Helper.GetSystemLockdownInfo(ARTEnums.SystemLockdownReason.UploadDataProcessingRequired);
        oDataImportHrdInfo.RoleID = SessionHelper.CurrentRoleID;
        oDataImportHrdInfo.DataImportName = this.txtProfileName.Text;
        oDataImportHrdInfo.FileName = file.FileOriginalName;
        oDataImportHrdInfo.PhysicalPath = file.FilePhysicalPath;
        oDataImportHrdInfo.FileSize = file.FileSize;
        oDataImportHrdInfo.CompanyID = SessionHelper.CurrentCompanyID;
        oDataImportHrdInfo.DataImportStatusID = dataImportStatusID;
        oDataImportHrdInfo.RecordsImported = 0;
        oDataImportHrdInfo.IsActive = true;
        oDataImportHrdInfo.DateAdded = DateTime.Now;
        oDataImportHrdInfo.AddedBy = SessionHelper.GetCurrentUser().LoginID;
        oDataImportHrdInfo.LanguageID = SessionHelper.GetUserLanguage();
        SetDataImportHDR(oDataImportHrdInfo);

        oDataImport.InsertDataImportWithFailureMsgAndKeyCount(oDataImportHrdInfo, failureMsg, keyMappingCount, Helper.GetAppUserInfo());
    }

    /// <summary>
    /// Hides all preview grids
    /// </summary>
    private void HideAllGrids()
    {
        this.rgHolidayCal.DataSource = null;
        this.pnlHolidayCal.Visible = false;

        this.pnlCurrency.Visible = false;
        this.rgCurrency.DataSource = null;

        this.pnlPeriodEndDate.Visible = false;
        this.rgPeriodEndDate.DataSource = null;

        this.pnlSubLedger.Visible = false;
        this.rgSubLedger.DataSource = null;

        rowDataImportGrid.Visible = false;
        trButtons.Visible = false;
    }

    /// <summary>
    /// Update Upload Rules Information
    /// </summary>
    private void UpdateImportTypeRulesInfo()
    {
        short importType = Convert.ToInt16(this.ddlImportType.SelectedValue);
        hlOpenExcelFile.NavigateUrl = this.GetImportTypeTemplateUrl(importType);
        pnlLanguageSelection.Visible = false;
        switch (importType)
        {
            case (short)ARTEnums.DataImportType.AccountAttributeList:
                this.lblRules.LabelID = (int)WebEnums.DataImportTypeLabelID.AccountAttributeListLabelID;
                break;
            case (short)ARTEnums.DataImportType.CurrencyExchangeRateData:
                this.lblRules.LabelID = (int)WebEnums.DataImportTypeLabelID.CurrencyExchangeRateDataLabelID;
                break;
            case (short)ARTEnums.DataImportType.GLData:
                this.lblRules.LabelID = (int)WebEnums.DataImportTypeLabelID.GLDataLabelID;
                break;
            case (short)ARTEnums.DataImportType.HolidayCalendar:
                this.lblRules.LabelID = (int)WebEnums.DataImportTypeLabelID.HolidayCalendarLabelID;
                break;
            case (short)ARTEnums.DataImportType.PeriodEndDates:
                this.lblRules.LabelID = (int)WebEnums.DataImportTypeLabelID.PeriodEndDatesLabelID;
                break;
            case (short)ARTEnums.DataImportType.SubledgerData:
                this.lblRules.LabelID = (int)WebEnums.DataImportTypeLabelID.SubledgerDataLabelID;
                break;
            case (short)ARTEnums.DataImportType.SubledgerSource:
                this.lblRules.LabelID = (int)WebEnums.DataImportTypeLabelID.SubledgerSourceLabelID;
                break;
            case (short)ARTEnums.DataImportType.GLTBS:
                this.lblRules.LabelID = (int)WebEnums.DataImportTypeLabelID.GLTBSDataLabelID;
                break;
            case (short)ARTEnums.DataImportType.AccountUpload:
                this.lblRules.LabelID = (int)WebEnums.DataImportTypeLabelID.AccountUploadLabelID;
                break;
            case (short)ARTEnums.DataImportType.MultilingualUpload:
                this.lblRules.LabelID = (int)WebEnums.DataImportTypeLabelID.MultilingualUploadLabelID;
                pnlLanguageSelection.Visible = true;
                break;
            case (short)ARTEnums.DataImportType.UserUpload:
                this.lblRules.LabelID = (int)WebEnums.DataImportTypeLabelID.UserUploadLabelID;
                break;
            case (short)ARTEnums.DataImportType.RecControlChecklist:
                this.lblRules.LabelID = (int)WebEnums.DataImportTypeLabelID.RecControlChecklistLabelID;
                break;
        }
    }

    /// <summary>
    /// Set errormessages for required fields
    /// </summary>
    private void SetErrorMessage()
    {
        this.txtProfileName.ErrorMessage = Helper.GetErrorMessage(WebEnums.FieldType.MandatoryField, this.lblProfileName.LabelID);
        rfvImportType.ErrorMessage = Helper.GetErrorMessage(WebEnums.FieldType.MandatoryField, lblImportType.LabelID);
        rfvImportType.InitialValue = WebConstants.SELECT_ONE;
        rfvFromLanguage.ErrorMessage = Helper.GetErrorMessage(WebEnums.FieldType.MandatoryField, lblFromLanguage.LabelID);
        rfvFromLanguage.InitialValue = WebConstants.SELECT_ONE;
        rfvToLanguage.ErrorMessage = Helper.GetErrorMessage(WebEnums.FieldType.MandatoryField, lblToLanguage.LabelID);
        rfvToLanguage.InitialValue = WebConstants.SELECT_ONE;
        rfvDataimportTemplate.ErrorMessage = Helper.GetErrorMessage(WebEnums.FieldType.MandatoryField, 2870);
        rfvDataimportTemplate.InitialValue = WebConstants.SELECT_ONE;
    }

    private void SendMailToUserOnSucess(string mailID, int rowsAffected)
    {
        try
        {
            //AppSettingHelper.GetAppSettingValue(AppSettingConstants.EMAIL_PASSWORD);
            StringBuilder oMailBody = new StringBuilder();
            oMailBody.Append(string.Format("{0} {1}", ddlImportType.SelectedItem, LanguageUtil.GetValue(1744)));
            oMailBody.Append("<br>");
            oMailBody.Append(string.Format("{0}: {1} ", LanguageUtil.GetValue(1308), txtProfileName.Text));
            oMailBody.Append("<br>");
            oMailBody.Append(string.Format("{0}:{1} ", LanguageUtil.GetValue(1745), rowsAffected));
            oMailBody.Append("<br>");
            oMailBody.Append(string.Format("{0}: {1} ", LanguageUtil.GetValue(1399), Helper.GetDisplayDate(DateTime.Now)));
            string fromAddress = AppSettingHelper.GetAppSettingValue(AppSettingConstants.EMAIL_FROM_DEFAULT);
            oMailBody.Append("<br/>" + MailHelper.GetEmailSignature(WebEnums.SignatureEnum.SendBySystemAdmin, fromAddress));
            //oMailBody.Append(AppSettingHelper.GetAppSettingValue(AppSettingConstants.EMAIL_SYSTEM_URL));
            string mailSubject = string.Format("{0}", LanguageUtil.GetValue(1743));

            string toAddress = mailID;
            MailHelper.SendEmail(fromAddress, toAddress, mailSubject, oMailBody.ToString());
        }
        catch (Exception ex)
        {
            Helper.FormatAndShowErrorMessage(null, ex);
        }
    }

    private void SendMailToUserOnFailure(string mailID, string errorMessage)
    {
        try
        {
            string fromAddress = AppSettingHelper.GetAppSettingValue(AppSettingConstants.EMAIL_FROM_DEFAULT);
            //AppSettingHelper.GetAppSettingValue(AppSettingConstants.EMAIL_PASSWORD);
            StringBuilder oMailBody = new StringBuilder();
            oMailBody.Append(string.Format("{0} {1}", ddlImportType.SelectedItem, LanguageUtil.GetValue(1753)));
            oMailBody.Append("<br>");
            oMailBody.Append(string.Format("{0}: {1} ", LanguageUtil.GetValue(1308), txtProfileName.Text));
            oMailBody.Append("<br>");
            oMailBody.Append(string.Format("{0}:{1} ", LanguageUtil.GetValue(1399), Helper.GetDisplayDate(DateTime.Now)));
            oMailBody.Append("<br>");
            oMailBody.Append(string.Format("{0}: {1} ", LanguageUtil.GetValue(1051), errorMessage));
            oMailBody.Append("<br/>" + MailHelper.GetEmailSignature(WebEnums.SignatureEnum.SendBySystemAdmin, fromAddress));

            //oMailBody.Append(AppSettingHelper.GetAppSettingValue(AppSettingConstants.EMAIL_SYSTEM_URL));
            string mailSubject = string.Format("{0}", LanguageUtil.GetValue(1752));



            string toAddress = mailID;
            MailHelper.SendEmail(fromAddress, toAddress, mailSubject, oMailBody.ToString());
        }
        catch (Exception ex)
        {
            Helper.FormatAndShowErrorMessage(null, ex);
        }
    }

    private ReconciliationPeriodInfo GetCurrentReconciliationPeriodInfo()
    {

        List<ReconciliationPeriodInfo> oReconciliationPeriodInfoCollection = CacheHelper.GetAllReconciliationPeriods(null);
        foreach (ReconciliationPeriodInfo objReconciliationPeriodInfo in oReconciliationPeriodInfoCollection)
        {
            if (objReconciliationPeriodInfo.ReconciliationPeriodID == SessionHelper.CurrentReconciliationPeriodID)
            {
                return objReconciliationPeriodInfo;
            }

        }
        return null;
    }

    /// <summary>
    /// Remove Import Types based upon Rec Period Status
    /// </summary>
    /// <param name="oDataImportTypeMstInfoList"></param>
    private void RemoveImportTypesBasedUponRecPeriodStatus(IList<DataImportTypeMstInfo> oDataImportTypeMstInfoList)
    {
        if (oDataImportTypeMstInfoList != null && oDataImportTypeMstInfoList.Count > 0)
        {
            for (int i = oDataImportTypeMstInfoList.Count - 1; i >= 0; i--)
            {
                DataImportTypeMstInfo oDataImportTypeMstInfo = oDataImportTypeMstInfoList[i];
                ARTEnums.DataImportType eDataImportType = (ARTEnums.DataImportType)oDataImportTypeMstInfo.DataImportTypeID;
                switch (eDataImportType)
                {
                    case ARTEnums.DataImportType.AccountUpload:
                        if (SessionHelper.CurrentReconciliationPeriodID == null
                            || CurrentRecProcessStatus.Value != WebEnums.RecPeriodStatus.NotStarted)
                            oDataImportTypeMstInfoList.Remove(oDataImportTypeMstInfo);
                        break;
                    case ARTEnums.DataImportType.CurrencyExchangeRateData:
                    case ARTEnums.DataImportType.GLData:
                    case ARTEnums.DataImportType.SubledgerData:
                    case ARTEnums.DataImportType.AccountAttributeList:
                    case ARTEnums.DataImportType.GLTBS:
                    case ARTEnums.DataImportType.RecControlChecklist:
                        if (SessionHelper.CurrentReconciliationPeriodID == null
                            || CurrentRecProcessStatus.Value == WebEnums.RecPeriodStatus.Closed
                            || CurrentRecProcessStatus.Value == WebEnums.RecPeriodStatus.Skipped)
                        {
                            oDataImportTypeMstInfoList.Remove(oDataImportTypeMstInfo);
                        }
                        break;
                        //case ARTEnums.DataImportType.UserUpload:
                        //    if (SessionHelper.CurrentReconciliationPeriodID == null
                        //        || SessionHelper.CurrentRecProcessStatusEnum != WebEnums.RecPeriodStatus.NotStarted)
                        //        oDataImportTypeMstInfoList.Remove(oDataImportTypeMstInfo);
                        //break;
                }
            }
        }
    }
    /// <summary>
    /// Remove Data Import Types based upon Certification Status
    /// </summary>
    /// <param name="oDataImportTypeMstInfoList"></param>
    private void RemoveImportTypesBasedUponCertification(IList<DataImportTypeMstInfo> oDataImportTypeMstInfoList)
    {
        bool isCertificationStarted = CertificationHelper.IsCertificationStarted();
        if (oDataImportTypeMstInfoList != null && oDataImportTypeMstInfoList.Count > 0)
        {
            for (int i = oDataImportTypeMstInfoList.Count - 1; i >= 0; i--)
            {
                DataImportTypeMstInfo oDataImportTypeMstInfo = oDataImportTypeMstInfoList[i];
                ARTEnums.DataImportType eDataImportType = (ARTEnums.DataImportType)oDataImportTypeMstInfo.DataImportTypeID;
                switch (eDataImportType)
                {
                    case ARTEnums.DataImportType.CurrencyExchangeRateData:
                    case ARTEnums.DataImportType.GLData:
                    case ARTEnums.DataImportType.SubledgerData:
                    case ARTEnums.DataImportType.AccountAttributeList:
                    case ARTEnums.DataImportType.RecControlChecklist:
                        if (isCertificationStarted)
                        {
                            oDataImportTypeMstInfoList.Remove(oDataImportTypeMstInfo);
                        }
                        break;
                }
            }
        }
    }
    /// <summary>
    /// Remove data import types based upon capability
    /// </summary>
    /// <param name="oDataImportTypeMstInfoList"></param>
    private void RemoveImportTypesBasedUponCapability(IList<DataImportTypeMstInfo> oDataImportTypeMstInfoList)
    {
        if (oDataImportTypeMstInfoList != null && oDataImportTypeMstInfoList.Count > 0)
        {
            for (int i = oDataImportTypeMstInfoList.Count - 1; i >= 0; i--)
            {
                DataImportTypeMstInfo oDataImportTypeMstInfo = oDataImportTypeMstInfoList[i];
                ARTEnums.DataImportType eDataImportType = (ARTEnums.DataImportType)oDataImportTypeMstInfo.DataImportTypeID;
                switch (eDataImportType)
                {
                    case ARTEnums.DataImportType.GLData:
                    case ARTEnums.DataImportType.SubledgerData:
                        if (CurrentRecProcessStatus.Value != WebEnums.RecPeriodStatus.NotStarted
                            && !Helper.IsFeatureActivated(WebEnums.Feature.MultiVersionGL, SessionHelper.CurrentReconciliationPeriodID))
                        {
                            oDataImportTypeMstInfoList.Remove(oDataImportTypeMstInfo);
                        }
                        break;
                    case ARTEnums.DataImportType.GLTBS:
                        if (!Helper.IsFeatureActivated(WebEnums.Feature.MatchingEntry, SessionHelper.CurrentReconciliationPeriodID))
                        {
                            oDataImportTypeMstInfoList.Remove(oDataImportTypeMstInfo);
                        }
                        break;
                    case ARTEnums.DataImportType.AccountUpload:
                        if (!Helper.IsFeatureActivated(WebEnums.Feature.MappingUpload, SessionHelper.CurrentReconciliationPeriodID))
                        {
                            oDataImportTypeMstInfoList.Remove(oDataImportTypeMstInfo);
                        }
                        break;
                    case ARTEnums.DataImportType.UserUpload:
                        if (!Helper.IsFeatureActivated(WebEnums.Feature.UserUpload, SessionHelper.CurrentReconciliationPeriodID))
                        {
                            oDataImportTypeMstInfoList.Remove(oDataImportTypeMstInfo);
                        }
                        break;
                    case ARTEnums.DataImportType.CurrencyExchangeRateData:
                        if (!Helper.IsFeatureActivated(WebEnums.Feature.MultiCurrency, SessionHelper.CurrentReconciliationPeriodID))
                        {
                            oDataImportTypeMstInfoList.Remove(oDataImportTypeMstInfo);
                        }
                        break;
                    case ARTEnums.DataImportType.RecControlChecklist:
                        if (Helper.GetFeatureCapabilityMode(WebEnums.Feature.RecControlChecklist, ARTEnums.Capability.RecControlChecklist, SessionHelper.CurrentReconciliationPeriodID) != WebEnums.FeatureCapabilityMode.Visible)
                        {
                            oDataImportTypeMstInfoList.Remove(oDataImportTypeMstInfo);
                        }
                        break;
                }
            }
        }
    }
    /// <summary>
    /// Remove Data Import Types based upon Role
    /// </summary>
    /// <param name="oDataImportTypeMstInfoList"></param>
    private void RemoveImportTypesBasedUponRole(IList<DataImportTypeMstInfo> oDataImportTypeMstInfoList)
    {
        if (oDataImportTypeMstInfoList != null && oDataImportTypeMstInfoList.Count > 0)
        {
            for (int i = oDataImportTypeMstInfoList.Count - 1; i >= 0; i--)
            {
                DataImportTypeMstInfo oDataImportTypeMstInfo = oDataImportTypeMstInfoList[i];
                ARTEnums.DataImportType eDataImportType = (ARTEnums.DataImportType)oDataImportTypeMstInfo.DataImportTypeID;
                switch (eDataImportType)
                {
                    case ARTEnums.DataImportType.MultilingualUpload:
                        if (SessionHelper.CurrentRoleEnum != WebEnums.UserRole.SKYSTEM_ADMIN)
                        {
                            oDataImportTypeMstInfoList.Remove(oDataImportTypeMstInfo);
                        }
                        break;
                }
            }
        }
    }

    /// <summary>
    /// Remove data import types based upon TaskMaster Package
    /// </summary>
    /// <param name="oDataImportTypeMstInfoList"></param>
    private void RemoveImportTypesBasedUponTaskMasterPackage(IList<DataImportTypeMstInfo> oDataImportTypeMstInfoList)
    {
        if (oDataImportTypeMstInfoList != null && oDataImportTypeMstInfoList.Count > 0)
        {
            for (int i = oDataImportTypeMstInfoList.Count - 1; i >= 0; i--)
            {
                DataImportTypeMstInfo oDataImportTypeMstInfo = oDataImportTypeMstInfoList[i];
                ARTEnums.DataImportType eDataImportType = (ARTEnums.DataImportType)oDataImportTypeMstInfo.DataImportTypeID;
                switch (eDataImportType)
                {
                    case ARTEnums.DataImportType.GLData:
                        if (CurrentRecProcessStatus.Value != WebEnums.RecPeriodStatus.NotStarted
                          && !Helper.IsFeatureActivated(WebEnums.Feature.MultiVersionGL, SessionHelper.CurrentReconciliationPeriodID))
                        {
                            oDataImportTypeMstInfoList.Remove(oDataImportTypeMstInfo);
                        }
                        break;
                    case ARTEnums.DataImportType.AccountUpload:
                        if (!Helper.IsFeatureActivated(WebEnums.Feature.MappingUpload, SessionHelper.CurrentReconciliationPeriodID))
                        {
                            oDataImportTypeMstInfoList.Remove(oDataImportTypeMstInfo);
                        }
                        break;
                    case ARTEnums.DataImportType.CurrencyExchangeRateData:
                        if (!Helper.IsFeatureActivated(WebEnums.Feature.MultiCurrency, SessionHelper.CurrentReconciliationPeriodID))
                        {
                            oDataImportTypeMstInfoList.Remove(oDataImportTypeMstInfo);
                        }
                        break;
                    case ARTEnums.DataImportType.SubledgerData:
                    case ARTEnums.DataImportType.GLTBS:
                    case ARTEnums.DataImportType.AccountAttributeList:
                    case ARTEnums.DataImportType.MultilingualUpload:
                    case ARTEnums.DataImportType.SubledgerSource:
                    case ARTEnums.DataImportType.UserUpload:
                    case ARTEnums.DataImportType.RecControlChecklist:
                        oDataImportTypeMstInfoList.Remove(oDataImportTypeMstInfo);
                        break;
                }
            }
        }
    }
    #endregion

    #region Other Methods
    protected void ClearDataImport()
    {
        Sel.Value = "";
        // If file is saved and operation have been canceled, delete this file.
        if (ViewState[ViewStateConstants.IMPORTFILEATTRIBUTES] != null)
        {
            ImportFileAttributes fileAttributes = (ImportFileAttributes)ViewState[ViewStateConstants.IMPORTFILEATTRIBUTES];
            string filePath = fileAttributes.FilePhysicalPath;
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
                ViewState.Remove(ViewStateConstants.IMPORTFILEATTRIBUTES);
            }
        }

    }

    protected void SetImportTypeDDl()
    {
        try
        {
            IList<DataImportTypeMstInfo> objDataImportTypeMstInfolist = SessionHelper.GetAllDataImportType();
            IList<DataImportTypeMstInfo> oDataImportTypeMstInfoCollection = null;

            oDataImportTypeMstInfoCollection = (IList<DataImportTypeMstInfo>)Helper.DeepClone(objDataImportTypeMstInfolist);

            RemoveImportTypesBasedUponRecPeriodStatus(oDataImportTypeMstInfoCollection);
            RemoveImportTypesBasedUponCertification(oDataImportTypeMstInfoCollection);
            if (SessionHelper.CurrentReconciliationPeriodID.HasValue)
                RemoveImportTypesBasedUponCapability(oDataImportTypeMstInfoCollection);
            RemoveImportTypesBasedUponRole(oDataImportTypeMstInfoCollection);
            CompanyHdrInfo oCompanyHdrInfo = Helper.GetCompanyInfoLiteObject(SessionHelper.CurrentCompanyID);
            if (oCompanyHdrInfo != null && oCompanyHdrInfo.PackageID.HasValue)
            {
                short PackageID = oCompanyHdrInfo.PackageID.Value;
                if (PackageID == (short)ARTEnums.Package.TaskMaster)
                {
                    RemoveImportTypesBasedUponTaskMasterPackage(oDataImportTypeMstInfoCollection);
                }
            }
            ddlImportType.DataSource = oDataImportTypeMstInfoCollection;
            ddlImportType.DataValueField = "DataImportTypeID";
            ddlImportType.DataTextField = "Name";
            ddlImportType.DataBind();
            ListControlHelper.AddListItemForSelectOne(ddlImportType);

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

    protected void clearcontrolsAfterUpload()
    {
        txtEmailFailure.Text = "";
        txtEmailSucess.Text = "";
        txtProfileName.Text = "";
        txtUserNameFailure.Text = "";
        txtUserNameSucess.Text = "";

    }
    public override string GetMenuKey()
    {
        return "DataImport";
    }

    public void SetDataImportHDR(DataImportHdrInfo objDataImportHdrInfo)
    {
        if (txtEmailFailure.Text != "")
            objDataImportHdrInfo.NotifyFailureEmailIDs = txtEmailFailure.Text;
        if (txtUserNameFailure.Text != "")
            objDataImportHdrInfo.NotifyFailureUserEmailIDs = txtUserNameFailure.Text;
        if (txtEmailSucess.Text != "")
            objDataImportHdrInfo.NotifySuccessEmailIDs = txtEmailSucess.Text;

        if (txtUserNameSucess.Text != "")
            objDataImportHdrInfo.NotifySuccessUserEmailIDs = txtUserNameSucess.Text;
    }
    void SendMailOnSucess(DataImportHdrInfo oDataImportHdrInfo, int rowAffected)
    {

        if (oDataImportHdrInfo.NotifySuccessEmailIDs != "" && oDataImportHdrInfo.NotifySuccessEmailIDs != null)
        {
            SendMailToUserOnSucess(oDataImportHdrInfo.NotifySuccessEmailIDs, rowAffected);
        }
        if (oDataImportHdrInfo.NotifySuccessUserEmailIDs != "" && oDataImportHdrInfo.NotifySuccessUserEmailIDs != null)
        {
            SendMailToUserOnSucess(oDataImportHdrInfo.NotifySuccessUserEmailIDs, rowAffected);
        }
    }

    void SendMailOnFailure(DataImportHdrInfo oDataImportHdrInfo, string errorMessage)
    {
        if (oDataImportHdrInfo.NotifyFailureEmailIDs != "" && oDataImportHdrInfo.NotifyFailureEmailIDs != null)
        {
            SendMailToUserOnFailure(oDataImportHdrInfo.NotifyFailureEmailIDs, errorMessage);
        }
        if (oDataImportHdrInfo.NotifyFailureUserEmailIDs != "" && oDataImportHdrInfo.NotifyFailureUserEmailIDs != null)
        {
            SendMailToUserOnFailure(oDataImportHdrInfo.NotifyFailureUserEmailIDs, errorMessage);
        }
    }
    void CheckWhetherAnyAccountAssociatedForGivenUser(IDataImport oDataImport)
    {
        bool isAnyAccountAssigned = false;
        isAnyAccountAssigned = oDataImport.IsAnyAccountAssigned(SessionHelper.CurrentRoleID, SessionHelper.CurrentUserID, SessionHelper.CurrentReconciliationPeriodID, Helper.GetAppUserInfo());
        if (isAnyAccountAssigned == false)
        {
            throw new Exception(LanguageUtil.GetValue(5000247));
        }
    }
    #endregion

}
