using SkyStem.ART.Client.Data;
using SkyStem.ART.Client.Exception;
using SkyStem.ART.Client.IServices;
using SkyStem.ART.Client.Model;
using SkyStem.ART.Client.Model.Matching;
using SkyStem.ART.Shared.Data;
using SkyStem.ART.Shared.Utility;
using SkyStem.ART.Web.Classes;
using SkyStem.ART.Web.Data;
using SkyStem.ART.Web.Utility;
using SkyStem.Language.LanguageUtility;
using SkyStem.Language.LanguageUtility.Classes;
using SkyStem.Library.Controls.TelerikWebControls.Data;
using SkyStem.Library.Controls.WebControls;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

public partial class Pages_CreateDataImportTemplate : PageBaseCompany
{

    #region Variables & Constants
    private bool selectOption = true;
    bool isExportPDF;
    bool isExportExcel;
    #endregion

    #region Properties
    #endregion

    #region Delegates & Events
    #endregion

    #region Page Events
    protected void Page_Init(object sender, EventArgs e)
    {
        SetImportTypeDDl();
        MasterPageBase ompage = (MasterPageBase)this.Master;
        ompage.ReconciliationPeriodChangedEventHandler += new EventHandler(ompage_ReconciliationPeriodChangedEventHandler);
        this.SetErrorMessage();
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                isExportPDF = false;
                isExportExcel = false;
                Helper.SetPageTitle(this, 2859);
                Helper.ShowInputRequirementSection(this, 1202);
                BindGrid();

                //Setting allowed file extensions.
                this.RadFileUpload.AllowedFileExtensions = DataImportHelper.GetAllowedFileExtensions();

                //Set allowed file size in bytes
                this.RadFileUpload.MaxFileSize = DataImportHelper.GetAllowedMaximumFileSize(SessionHelper.CurrentCompanyID.Value);

                //setting error messages to be shown at runtime
                this.cvFileUpload.Attributes.Add("fileNameErrorMessage", LanguageUtil.GetValue(5000035));
                this.cvFileUpload.Attributes.Add("fileExtensionErrorMessage", LanguageUtil.GetValue(5000036));
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

    #endregion

    #region Grid Events
    protected void ucSkyStemARTGrid_ItemCreated(object sender, GridItemEventArgs e)
    {
        GridHelper.SetStylesForExportGrid(e, isExportPDF, isExportExcel);
        if (e.Item is GridCommandItem)
        {
            ImageButton ibExportToExcel = (e.Item as GridCommandItem).FindControl(TelerikConstants.GRID_ID_EXPORT_TO_EXCEL_ICON) as ImageButton;
            Helper.RegisterPostBackToControls(this, ibExportToExcel);

        }
        if (e.Item is GridCommandItem)
        {
            ImageButton ibExportToExcel = (e.Item as GridCommandItem).FindControl(TelerikConstants.GRID_ID_EXPORT_TO_PDF_ICON) as ImageButton;
            Helper.RegisterPostBackToControls(this, ibExportToExcel);

        }
    }
    protected void ucSkyStemARTGrid_ItemDataBound(object sender, GridItemEventArgs e)
    {
        if (e.Item.ItemType == GridItemType.Header)
        {
            ucSkyStemARTGrid.MasterTableView.Columns.FindByUniqueName("CheckboxSelectColumn").Visible = this.selectOption;
        }
        if (e.Item.ItemType == GridItemType.Item || e.Item.ItemType == GridItemType.AlternatingItem)
        {
            ImportTemplateHdrInfo oImportTemplateInfo = (ImportTemplateHdrInfo)e.Item.DataItem;
            ExLabel lblTemplateName = (ExLabel)e.Item.FindControl("lblTemplateName");
            ExLabel lblLanguage = (ExLabel)e.Item.FindControl("lblLanguage");
            ExLabel lblSheetName = (ExLabel)e.Item.FindControl("lblSheetName");
            ExLabel lblFileName = (ExLabel)e.Item.FindControl("lblFileName");
            ExLabel lblCreatedBy = (ExLabel)e.Item.FindControl("lblCreatedBy");
            ExLabel lblDateCreated = (ExLabel)e.Item.FindControl("lblDateCreated");
            ExLabel lblDataImportType = (ExLabel)e.Item.FindControl("lblDataImportType");
            ImageButton imgbtnEdit = (ImageButton)e.Item.FindControl("imgbtnEdit");
            ImageButton imgbtnViewRecord = (ImageButton)e.Item.FindControl("imgbtnViewRecord");
            ExImageButton imgFileType = (ExImageButton)e.Item.FindControl("imgFileType");
            CheckBox chkbox = (CheckBox)(e.Item as GridDataItem)["CheckboxSelectColumn"].Controls[0];
            ExImage imgSuccess = (ExImage)e.Item.FindControl("imgSuccess");
            ExLabel lblDateRevised = (ExLabel)e.Item.FindControl("lblDateRevised");
            ExLabel lblRevisedBy = (ExLabel)e.Item.FindControl("lblRevisedBy");

            lblTemplateName.Text = Convert.ToString(oImportTemplateInfo.TemplateName);
            lblLanguage.Text = Convert.ToString(oImportTemplateInfo.LanguageName);
            lblSheetName.Text = Convert.ToString(oImportTemplateInfo.SheetName);
            lblFileName.Text = Convert.ToString(oImportTemplateInfo.TemplateFileName);
            lblCreatedBy.Text = Convert.ToString(oImportTemplateInfo.AddedBy);
            lblDateCreated.Text = Helper.GetDisplayDate(oImportTemplateInfo.DateAdded);
            lblDataImportType.Text = Convert.ToString(oImportTemplateInfo.DataImportType);
            lblDateRevised.Text = Helper.GetDisplayDate(oImportTemplateInfo.DateRevised);
            lblRevisedBy.Text = oImportTemplateInfo.RevisedBy;

            //string url = "DownloadAttachment.aspx?" + QueryStringConstants.FILE_PATH + "=" + Server.UrlEncode(SharedHelper.GetDisplayFilePath(oImportTemplateInfo.PhysicalPath));
            //imgFileType.OnClientClick = "document.location.href = '" + url + "';return false;";
            string url = string.Format("Downloader?{0}={1}&", QueryStringConstants.HANDLER_ACTION, (Int32)WebEnums.HandlerActionType.DownloadDataImportTemplateFile);
            url += "&" + QueryStringConstants.IMPORT_TEMPLATE_ID + "=" + oImportTemplateInfo.ImportTemplateID.GetValueOrDefault()
            + "&" + QueryStringConstants.DATA_IMPORT_TYPE_ID + "=" + oImportTemplateInfo.DataImportTypeID.GetValueOrDefault();

            imgFileType.Attributes.Add("onclick", "javascript:{$get('" + ifDownloader.ClientID + "').src='" + url + "'; return false;}");


            if (oImportTemplateInfo.DataImportTypeLabelID == 1052 || oImportTemplateInfo.DataImportTypeLabelID == 1054)
            {
                imgbtnEdit.Visible = true;
            }

            if (oImportTemplateInfo.DataImportTemplateID > 0)
            {
                chkbox.Enabled = false;
                e.Item.SelectableMode = GridItemSelectableMode.None;
                imgbtnEdit.Visible = false;
                imgbtnViewRecord.Visible = true;
            }
            else
            {
                imgbtnEdit.Visible = true;
                imgbtnViewRecord.Visible = false;
            }

            if (oImportTemplateInfo.NumberOfMappedColumns.HasValue && oImportTemplateInfo.NumberOfMappedColumns.Value > 0)
            {
                imgSuccess.Visible = true;
            }
            else
            {
                imgSuccess.Visible = false;
            }
        }
    }
    protected void ucSkyStemARTGrid_ItemCommand(object sender, GridCommandEventArgs e)
    {
        if (e.CommandName == "EDIT")
        {
            Int32 ImportTemplateID = Convert.ToInt32((ucSkyStemARTGrid.MasterTableView.DataKeyValues[e.Item.ItemIndex][ucSkyStemARTGrid.MasterTableView.DataKeyNames[0]]).ToString());
            Response.Redirect("~/Pages/TemplateColumnMapping.aspx?TemplateId=" + ImportTemplateID);
        }
        if (e.CommandName == "VIEW")
        {
            Int32 ImportTemplateID = Convert.ToInt32((ucSkyStemARTGrid.MasterTableView.DataKeyValues[e.Item.ItemIndex][ucSkyStemARTGrid.MasterTableView.DataKeyNames[0]]).ToString());
            string EditMode = "EditMode";
            Response.Redirect("~/Pages/TemplateColumnMapping.aspx?TemplateId=" + ImportTemplateID + "&View=" + EditMode);
        }
        if (e.CommandName == TelerikConstants.GridExportToPDFCommandName)
        {
            isExportPDF = true;
            ucSkyStemARTGrid.MasterTableView.Columns.FindByUniqueName("CheckboxSelectColumn").Visible = false;
            GridHelper.ExportGridToPDF(ucSkyStemARTGrid, 2859);
        }
        if (e.CommandName == TelerikConstants.GridExportToExcelCommandName)
        {
            isExportExcel = true;
            ucSkyStemARTGrid.MasterTableView.Columns.FindByUniqueName("CheckboxSelectColumn").Visible = false;
            GridHelper.ExportGridToExcel(ucSkyStemARTGrid, 2859);
        }
    }
    protected void ucSkyStemARTGrid_PageIndexChanged(object sender, GridPageChangedEventArgs e)
    {
        ucSkyStemARTGrid.CurrentPageIndex = e.NewPageIndex;
        BindGrid();
    }
    #endregion

    #region Other Events
    protected void btnImport_Click(object sender, EventArgs e)
    {
        MasterPageBase oMasterPageBase = (MasterPageBase)this.Master;
        oMasterPageBase.HideMessage();
        if (Page.IsValid)
        {
            short? keyCount = null;
            IDataImport oDataImport = RemotingHelper.GetDataImportObject();
            keyCount = oDataImport.isKeyMappingDoneByCompanyID(SessionHelper.CurrentCompanyID.Value, Helper.GetAppUserInfo());
            if (keyCount.HasValue && keyCount.Value > 0)
            {
                if (RadFileUpload.UploadedFiles.Count > 0)
                {
                    ImportTemplateHdrInfo oImportTemplateInfo = new ImportTemplateHdrInfo();
                    string filePath = string.Empty;
                    string fileName = string.Empty;
                    string targetFolder = string.Empty;
                    StringBuilder oSBErrors = null;
                    short importType = -1;
                    DataTable dt = null;
                    int result = 0;
                    int companyID = SessionHelper.CurrentCompanyID.Value;
                    if (SessionHelper.CurrentRoleID == (short)ARTEnums.UserRole.SKYSTEM_ADMIN)
                        companyID = 0;
                    try
                    {
                        UploadedFile validFile = RadFileUpload.UploadedFiles[0];
                        ImportFileAttributes ImportFile;
                        importType = Convert.ToInt16(this.ddlDataImportType.SelectedValue);

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

                        if (SessionHelper.CurrentRoleID != (short)ARTEnums.UserRole.SKYSTEM_ADMIN && ((decimal)(ImportFile.FileSize) / (decimal)(1024 * 1024)) > (dataStorageCapacity - currentUsage))
                        {
                            string exceptionMessage = string.Format(Helper.GetLabelIDValue(5000181), (dataStorageCapacity - currentUsage), dataStorageCapacity);
                            throw new Exception(exceptionMessage);
                        }
                        else
                        {
                            oImportTemplateInfo.TemplateName = txtTemplateName.Text;
                            oImportTemplateInfo.SheetName = txtSheetName.Text;
                            oImportTemplateInfo.LanguageID = Convert.ToInt32(ddlLanguage.SelectedValue);
                            oImportTemplateInfo.DataImportTypeID = Convert.ToInt16(ddlDataImportType.SelectedValue);
                            oImportTemplateInfo.TemplateFileName = ImportFile.FileOriginalName;
                            oImportTemplateInfo.PhysicalPath = ImportFile.FilePhysicalPath;
                            oImportTemplateInfo.AddedBy = SessionHelper.CurrentUserLoginID;
                            oImportTemplateInfo.DateAdded = DateTime.Now;
                            oImportTemplateInfo.CompanyID = SessionHelper.CurrentCompanyID.Value;
                            oImportTemplateInfo.UserID = SessionHelper.CurrentUserID.Value;
                            oImportTemplateInfo.RoleId = SessionHelper.CurrentRoleID.Value;
                            ViewState[ViewStateConstants.IMPORTFILEATTRIBUTES] = ImportFile;

                            //Once file is saved, get records in a DataTable.
                            oSBErrors = new StringBuilder();
                            switch (importType)
                            {
                                case (short)ARTEnums.DataImportType.GLData:
                                    #region "GLData Upload"
                                    if (validFile.GetExtension().ToLower() == FileExtensions.csv)
                                        dt = ExcelHelper.GetDelimitedFileColumnsDataTable(filePath);
                                    else
                                        dt = ExcelHelper.GetExcelFileSchemaGL(filePath, validFile.GetExtension(), oImportTemplateInfo.SheetName);
                                    result = DataImportTemplateHelper.SaveImportTemplate(oImportTemplateInfo, dt);
                                    if (result > 0)
                                        Response.Redirect("~/Pages/TemplateColumnMapping.aspx?TemplateId=" + result);
                                    #endregion
                                    break;
                                case (short)ARTEnums.DataImportType.SubledgerData:
                                    #region "Subledgar Data Upload"
                                    if (validFile.GetExtension().ToLower() == FileExtensions.csv)
                                        dt = ExcelHelper.GetDelimitedFileColumnsDataTable(filePath);
                                    else
                                        dt = ExcelHelper.GetExcelFileSchemaGL(filePath, validFile.GetExtension(), oImportTemplateInfo.SheetName);
                                    result = DataImportTemplateHelper.SaveImportTemplate(oImportTemplateInfo, dt);
                                    if (result > 0)
                                        Response.Redirect("~/Pages/TemplateColumnMapping.aspx?TemplateId=" + result);
                                    #endregion
                                    break;

                            }
                        }
                    }
                    catch (HttpException exHttp)
                    {
                        if (ViewState[ViewStateConstants.IMPORTFILEATTRIBUTES] != null)
                            ViewState.Remove(ViewStateConstants.IMPORTFILEATTRIBUTES);
                        Helper.LogException(exHttp);
                    }
                    catch (ARTException ex)
                    {
                        switch (ex.ExceptionPhraseID)
                        {
                            //Invalid File. All Mandatory fields not present
                            //Save import and failure message to database
                            case 5000037:
                                //InsertDataImportHdrWithFailureMsg(Helper.GetLabelIDValue(5000037), importType, (short)WebEnums.DataImportStatus.Failure);
                                ViewState.Remove(ViewStateConstants.IMPORTFILEATTRIBUTES);
                                break;
                            //Invalid Data. 
                            //Save import and failure message to database
                            case 5000047:
                                ViewState.Remove(ViewStateConstants.IMPORTFILEATTRIBUTES);
                                break;
                        }
                        Helper.LogException(ex);
                        Helper.ShowErrorMessage(this, ex);
                    }
                    catch (Exception ex)
                    {

                        if (ViewState[ViewStateConstants.IMPORTFILEATTRIBUTES] != null)
                            ViewState.Remove(ViewStateConstants.IMPORTFILEATTRIBUTES);
                        Helper.LogException(ex);
                        Helper.ShowErrorMessage(this, ex);
                    }
                    finally
                    {
                        this.RadFileUpload.UploadedFiles.Clear();
                        txtSheetName.Text = "";
                        txtTemplateName.Text = "";
                        ddlDataImportType.SelectedValue = "-2";
                        ddlLanguage.SelectedValue = "-2";
                    }
                }
            }
            else
            {
                oMasterPageBase.ShowErrorMessage(5000407);
            }
        }
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        List<int> oSelectedIDList = GridSelectedItems();
        if (oSelectedIDList.Count > 0)
        {
            DataTable dt = WebPartHelper.CreateDataTable(oSelectedIDList);
            ImportTemplateHdrInfo oImportTemplateInfo = new ImportTemplateHdrInfo();
            oImportTemplateInfo.RevisedBy = SessionHelper.CurrentUserLoginID;
            oImportTemplateInfo.DateRevised = DateTime.Now;
            DataImportTemplateHelper.DeleteMappingData(dt, oImportTemplateInfo);
            BindGrid();
        }
        else
        {
            MasterPageBase oMasterPageBase = (MasterPageBase)this.Master;
            oMasterPageBase.ShowErrorMessage(2013);
        }
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
        }

    }
    #endregion

    #region Private Methods
    private void SetErrorMessage()
    {
        this.txtTemplateName.ErrorMessage = Helper.GetErrorMessage(WebEnums.FieldType.MandatoryField, this.lblTemplateName.LabelID);
        this.txtSheetName.ErrorMessage = Helper.GetErrorMessage(WebEnums.FieldType.MandatoryField, this.lblSheetName.LabelID);
        rfvImportType.ErrorMessage = Helper.GetErrorMessage(WebEnums.FieldType.MandatoryField, lblLanguage.LabelID);
        rfvImportType.InitialValue = WebConstants.SELECT_ONE;
        rfvData.ErrorMessage = Helper.GetErrorMessage(WebEnums.FieldType.MandatoryField, lblDataImportType.LabelID);
        rfvData.InitialValue = WebConstants.SELECT_ONE;
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

    private void BindGrid()
    {
        List<ImportTemplateHdrInfo> oImportTemplateInfoLst = new List<ImportTemplateHdrInfo>();
        oImportTemplateInfoLst = DataImportTemplateHelper.GetAllTemplateImport(SessionHelper.CurrentCompanyID.Value, SessionHelper.CurrentUserID.Value, SessionHelper.CurrentRoleID.Value);
        if (oImportTemplateInfoLst.Count > 0)
        {
            ucSkyStemARTGrid.DataSource = oImportTemplateInfoLst;
            ucSkyStemARTGrid.DataBind();
        }
        else
        {
            ucSkyStemARTGrid.DataSource = oImportTemplateInfoLst;
            ucSkyStemARTGrid.DataBind();
            btnDelete.Visible = false;
        }
    }

    private List<int> GridSelectedItems()
    {
        List<int> oSelectedIDList = new List<int>();
        int ImportListID;
        foreach (GridDataItem item in ucSkyStemARTGrid.SelectedItems)
        {
            CheckBox chkSelectItem = (CheckBox)(item)["CheckboxSelectColumn"].Controls[0];
            if (chkSelectItem != null && chkSelectItem.Checked)
            {
                ImportListID = Convert.ToInt32(item.GetDataKeyValue("ImportTemplateID"));
                oSelectedIDList.Add(ImportListID);
            }
        }
        return oSelectedIDList;
    }
    #endregion

    #region Other Methods
    protected void ompage_ReconciliationPeriodChangedEventHandler(object sender, EventArgs e)
    {
        ListControlHelper.BindLanguageDropdown(ddlLanguage, true, false, true);
        MasterPageBase oMasterPageBase = (MasterPageBase)this.Master;
        oMasterPageBase.HideMessage();
    }
    protected void SetImportTypeDDl()
    {
        try
        {
            IList<DataImportTypeMstInfo> objDataImportTypeMstInfolist = SessionHelper.GetAllDataImportType();
            objDataImportTypeMstInfolist = objDataImportTypeMstInfolist.Where(x => x.DataImportTypeID == 1 || x.DataImportTypeID == 3).ToList();
            if (objDataImportTypeMstInfolist != null)
            {
                ddlDataImportType.DataSource = objDataImportTypeMstInfolist;
                ddlDataImportType.DataValueField = "DataImportTypeID";
                ddlDataImportType.DataTextField = "Name";
                ddlDataImportType.DataBind();
            }
            ListControlHelper.AddListItemForSelectOne(ddlDataImportType);
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
    public override string GetMenuKey()
    {
        return "CreateDataImportTemplate";
    }

    #endregion


}