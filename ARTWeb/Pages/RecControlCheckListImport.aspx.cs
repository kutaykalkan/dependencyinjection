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
using System.Text;
using Telerik.Web.UI;
using SkyStem.Language.LanguageUtility;
using SkyStem.ART.Client.Data;
using SkyStem.ART.Client.Exception;
using SkyStem.ART.Client.Model.RecControlCheckList;
using SkyStem.Language.LanguageUtility.Classes;

public partial class Pages_RecControlCheckListImport : PageBaseRecPeriod
{

    long? AccountID = null;
    int? NetAccountID = null;
    long? GLDataID;

    bool isError = false;
    private GLDataHdrInfo _GLDataHdrInfo;
    #region "Private Properties"
    private GLDataHdrInfo GLDataHdrInfo
    {
        get
        {
            if (this._GLDataHdrInfo == null) 
            {
                if (ViewState[ViewStateConstants.CURRENT_GLDATAHDRINFO] == null)
                {
                    if (Request.QueryString[QueryStringConstants.GLDATA_ID] != null)
                    {
                        long? glDataID = Convert.ToInt64(Request.QueryString[QueryStringConstants.GLDATA_ID]);
                        this._GLDataHdrInfo = Helper.GetGLDataHdrInfo(glDataID);
                    }
                    ViewState[ViewStateConstants.CURRENT_GLDATAHDRINFO] = this._GLDataHdrInfo;
                }
                else
                {
                    this._GLDataHdrInfo = (GLDataHdrInfo)ViewState[ViewStateConstants.CURRENT_GLDATAHDRINFO];
                }
            }
            return this._GLDataHdrInfo;
        }
    }
    #endregion
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
    #endregion
    #region "Page Methods"
    protected void Page_Init(object sender, EventArgs e)
    {
        MasterPageBase oMasterPageBase = (MasterPageBase)this.Master.Master;
        ScriptManager scriptManager = oMasterPageBase.GetScriptManager();
        scriptManager.RegisterPostBackControl(btnPreview);
        scriptManager.RegisterPostBackControl(btnImportAll);
    }
    protected void Page_PreRender(object sender, EventArgs e)
    {
        int importSourceSectionLabelID = 2833;
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
                        ViewState["DataTable"] = Session["DataTableForImport"];
                        ImportAllUploadData();
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
            }

        }
        catch (Exception ex)
        {
            Helper.ShowErrorMessage(this, ex);
        }
        if (GLDataHdrInfo != null)
        {
            if (Helper.GetFormModeForRecControlCheckList(this.GLDataHdrInfo) == WebEnums.FormMode.Edit && (SessionHelper.CurrentRoleID == (short)ARTEnums.UserRole.PREPARER || SessionHelper.CurrentRoleID == (short)ARTEnums.UserRole.BACKUP_PREPARER))
            {
                txtImportName.Enabled = true;
                radFileUpload.Enabled = true;
                btnCancel.Enabled = true;
                btnPreview.Enabled = true;
                btnImportAll.Enabled = true;
            }
            else
            {
                txtImportName.Enabled = false;
                radFileUpload.Enabled = false;
                btnCancel.Enabled = false;
                btnPreview.Enabled = false;
                btnImportAll.Enabled = false;
            }           
        }
    }
    #endregion

    #region "Control Event Handlers"
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
                importType = (short)ARTEnums.DataImportType.RecControlChecklistAccount;
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
                        rgRecControlChecklist.PageSize = Convert.ToInt32(hdnNewPageSize.Value);
                        rgRecControlChecklist.VirtualItemCount = dt.Rows.Count;
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
                //Helper.FormatAndShowErrorMessage(this.Page.Master.FindControl("lblErrorMessage") as ExLabel, ex);
                Helper.ShowErrorMessage(this, ex);
            }
            catch (Exception ex)
            {
                this.DeleteFile(filePath);
                ViewState.Remove(ViewStateConstants.IMPORTFILEATTRIBUTES);
                //Helper.FormatAndShowErrorMessage(this.Page.Master.FindControl("lblErrorMessage") as ExLabel, ex);
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
    /// <summary>
    /// Deletes file from specified path.
    /// </summary>
    /// <param name="filePath">file physical path</param>
    /// <param name="exceptionMessage"></param>
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
    /// <summary>
    /// 
    /// </summary>
    /// <param name="source"></param>
    /// <param name="e"></param>
    protected void rgRecControlChecklist_PageIndexChanged(object source, GridPageChangedEventArgs e)
    {
        Sel.Value = string.Empty;
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        pnlImportGrid.Visible = false;
    }
    protected void btnPageCancel_Click(object sender, EventArgs e)
    {
        //Response.Redirect(ReturnUrl, true);
        SessionHelper.RedirectToUrl(ReturnUrl);
        return;
    }
    protected void btnImport_Click(object sender, EventArgs e)
    {
        try
        {
            this.ImportSelectedRecControlChecklist();
            MasterPageBase oMasterPageBase = (MasterPageBase)this.Master.Master;
            oMasterPageBase.ShowConfirmationMessage(1608);

            pnlImportGrid.Visible = false;
            Reset();
        }
        catch (Exception ex)
        {
            Helper.ShowErrorMessage(this, ex);
        }
    }
    private void Reset()
    {
        txtImportName.Text = string.Empty;
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
                importType = (short)ARTEnums.DataImportType.RecControlChecklistAccount;
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
                        DataTable dtValidData = GetValidRows(dt);
                        if (dtValidData.Select("IsValidRow = false").Count() > 0)
                        {
                            Session["DataTableForImport"] = dtValidData;
                            //Response.Redirect(GetUrlForRecItemImportStatusPage());
                            SessionHelper.RedirectToUrl(GetUrlForRecItemImportStatusPage());
                            return;
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
                //Helper.FormatAndShowErrorMessage(this.Page.Master.FindControl("lblErrorMessage") as ExLabel, ex);
                Helper.ShowErrorMessage(this, ex);
            }
            catch (Exception ex)
            {
                this.DeleteFile(filePath);
                ViewState.Remove(ViewStateConstants.IMPORTFILEATTRIBUTES);
                //Helper.FormatAndShowErrorMessage(this.Page.Master.FindControl("lblErrorMessage") as ExLabel, ex);
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
    private string GetUrlForRecItemImportStatusPage()
    {
        return "RecItemImportStatusMessage.aspx";
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

        string errorFormat = Helper.GetLabelIDValue(2495);
        StringBuilder oSBError;
        int rowCount = 0;

        foreach (DataRow dr in dtTempValidTable.Rows)
        {
            oSBError = new StringBuilder();
            rowCount += 1;

            string description = dr["Description"].ToString();
            if (string.IsNullOrEmpty(description))
            {
                dr["IsValidRow"] = "false";
                oSBError.Append(string.Format(errorFormat, Helper.GetLabelIDValue(1408)));
            }
            dr["ErrorMessage"] = oSBError.ToString();
        }


        return dtTempValidTable;
    }
    private void ImportAllUploadData()
    {

        DataTable dtTempValidTable = (DataTable)ViewState["DataTable"];
        DataTable dtUploadData = new DataTable();
        dtUploadData = dtTempValidTable.Clone();
        DataRow[] drValidRows = dtTempValidTable.Select("IsValidRow = true");
        if (drValidRows != null && drValidRows.Count() > 0)
        {
            dtUploadData = drValidRows.CopyToDataTable();
        }
        List<RecControlCheckListInfo> oRecControlCheckListInfoList = new List<RecControlCheckListInfo>();
        foreach (DataRow rowUpload in dtUploadData.Rows)
        {

            string Description = (string)rowUpload["Description"];
            RecControlCheckListInfo oRecControlCheckListInfo = new RecControlCheckListInfo();
            oRecControlCheckListInfo.Description = Description;
            oRecControlCheckListInfo.DescriptionLabelID = (int)LanguageUtil.InsertPhrase(Description, null, 1, (int)SessionHelper.CurrentCompanyID, SessionHelper.GetUserLanguage(), 4, null);
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
        ImportRecControlChecklist(oRecControlCheckListInfoList);

        MasterPageBase oMasterPageBase = (MasterPageBase)this.Master.Master;
        oMasterPageBase.ShowConfirmationMessage(1608);

        pnlImportGrid.Visible = false;
        Reset();
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
    #endregion

    #region "Private Methods"
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
        if (dataImportTypeID == (short)ARTEnums.DataImportType.RecControlChecklistAccount)
            oDataImportHrdInfo.ReconciliationPeriodID = SessionHelper.CurrentReconciliationPeriodID;
        oDataImportHrdInfo.IsActive = true;
        oDataImportHrdInfo.DateAdded = DateTime.Now;
        oDataImportHrdInfo.AddedBy = SessionHelper.GetCurrentUser().LoginID;

        oDataImport.InsertDataImportWithFailureMsg(oDataImportHrdInfo, failureMsg, Helper.GetAppUserInfo());

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
        hlOpenExcelFile.NavigateUrl = "~/Templates/RecControlChecklist.xlsx";
        Helper.SetPageTitle(this, 2833);
        Helper.ShowInputRequirementSection(this, 1647);
    }
    private void SetPrivateVariables()
    {
        if (!string.IsNullOrEmpty(Request.QueryString[QueryStringConstants.ACCOUNT_ID]))
        {
            AccountID = Convert.ToInt64(Request.QueryString[QueryStringConstants.ACCOUNT_ID]);
        }
        if (!string.IsNullOrEmpty(Request.QueryString[QueryStringConstants.NETACCOUNT_ID]))
        {
            NetAccountID = Convert.ToInt32(Request.QueryString[QueryStringConstants.NETACCOUNT_ID]);
        }
        if (!string.IsNullOrEmpty(Request.QueryString[QueryStringConstants.GLDATA_ID]))
        {
            GLDataID = Convert.ToInt64(Request.QueryString[QueryStringConstants.GLDATA_ID]);
        }


        // Set the Master Page Properties for GL Data ID
        RecHelper.SetRecStatusBarPropertiesForOtherPages(this, this.GLDataHdrInfo.GLDataID);
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
        Sel.Value = string.Empty;
        pnlImportGrid.Visible = true;
        //DataView dv = dt.DefaultView;
        //dv.RowFilter = "IsDuplicate = false";
        this.rgRecControlChecklist.DataSource = dt;
        this.rgRecControlChecklist.DataBind();
        GridColumn oGridColumn = this.rgRecControlChecklist.Columns.FindByUniqueNameSafe("Error");

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

    public override string GetMenuKey()
    {
        return "AccountViewer";
    }
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
            if (rgRecControlChecklist.AllowCustomPaging)
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
            //int defaultItemCount = Convert.ToInt32(AppSettingHelper.GetAppSettingValue(AppSettingConstants.DEFAULT_CHUNK_SIZE));
            int pageIndex = rgRecControlChecklist.CurrentPageIndex;
            int pageSize = Convert.ToInt32(hdnNewPageSize.Value);
            int count;
            int defaultItemCount = Helper.GetDefaultChunkSize(pageSize);
            count = ((((pageIndex + 1) * pageSize) / defaultItemCount) + 1) * defaultItemCount;

            object obj = ViewState["DataTable"];
            if (obj != null)
            {
                DataTable objectCollection = (DataTable)obj;
                if (objectCollection.Rows.Count % defaultItemCount == 0)
                    rgRecControlChecklist.VirtualItemCount = objectCollection.Rows.Count + 1;
                else
                    rgRecControlChecklist.VirtualItemCount = objectCollection.Rows.Count;
                rgRecControlChecklist.MasterTableView.DataSource = objectCollection;
            }
        }
        catch (Exception ex)
        {
            Helper.ShowErrorMessage(this, ex);
        }

    }
    private void ImportSelectedRecControlChecklist()
    {
        GridDataItem[] selectedItems = this.rgRecControlChecklist.MasterTableView.GetSelectedItems();
        List<RecControlCheckListInfo> oRecControlCheckListInfoList = new List<RecControlCheckListInfo>();
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
        ImportRecControlChecklist(oRecControlCheckListInfoList);
    }
    private int ImportRecControlChecklist(List<RecControlCheckListInfo> oRecControlCheckListInfoList)
    {
        int rowAffected = 0;
        DataImportHdrInfo oDataImportHrdInfo = new DataImportHdrInfo();
        this.GetDataImportHdrInfo(oDataImportHrdInfo, (short)ARTEnums.DataImportType.RecControlChecklist, oRecControlCheckListInfoList.Count);
        try
        {

            oDataImportHrdInfo.RoleID = SessionHelper.CurrentRoleID;
            RecControlCheckListHelper.InsertDataImportRecControlChecklistAccount(oDataImportHrdInfo, oRecControlCheckListInfoList, AccountID, NetAccountID,GLDataID, out rowAffected);
            //SendMailOnSucess(oDataImportHrdInfo, rowAffected);
        }
        catch (ARTException ex)
        {
            //SendMailOnFailure(oDataImportHrdInfo, ex.Message);
            throw ex;
        }
        catch (Exception ex)
        {
            //SendMailOnFailure(oDataImportHrdInfo, ex.Message);
            throw ex;
        }
        return rowAffected;
    }
    private void GetDataImportHdrInfo(DataImportHdrInfo oDataImportHrdInfo, short dataImportTypeID, int recordsImported)
    {
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
        oDataImportHrdInfo.DataImportStatusID = (short)WebEnums.DataImportStatus.Success;
        oDataImportHrdInfo.RecordsImported = recordsImported;
        if (dataImportTypeID == (short)ARTEnums.DataImportType.RecControlChecklist)
            oDataImportHrdInfo.ReconciliationPeriodID = SessionHelper.CurrentReconciliationPeriodID;

        oDataImportHrdInfo.IsActive = true;
        oDataImportHrdInfo.DateAdded = DateTime.Now;
        oDataImportHrdInfo.AddedBy = SessionHelper.GetCurrentUser().LoginID;

    }
}
