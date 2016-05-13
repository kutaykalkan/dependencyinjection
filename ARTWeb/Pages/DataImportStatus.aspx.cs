using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SkyStem.ART.Client.IServices;
using SkyStem.ART.Client.Model;
using SkyStem.Library.Controls.WebControls;
using SkyStem.ART.Web.Utility;
using SkyStem.ART.Web.Classes;
using SkyStem.ART.Web.Data;
using Telerik.Web.UI;
using SkyStem.Language.LanguageUtility;
using SkyStem.ART.Client.Exception;
using SkyStem.Library.Controls.TelerikWebControls.Data;
using SkyStem.ART.Client.Data;

public partial class Pages_DataImportStatus : PageBaseCompany
{
    #region Variables & Constants
    bool isExportPDF;
    bool isExportExcel;
    #endregion

    #region Properties
    public string DataImportTypeIDForGL
    {
        get
        {
            return ((short)ARTEnums.DataImportType.GLData).ToString();
        }
    }
    public string DataImportTypeIDForAccountUpload
    {
        get
        {
            return ((short)ARTEnums.DataImportType.AccountUpload).ToString();
        }
    }
    public string DataImportTypeIDForRecControlChecklist
    {
        get
        {
            return ((short)ARTEnums.DataImportType.RecControlChecklist).ToString();
        }
    }
    public string CurrentRoleID
    {
        get
        {
            return SessionHelper.CurrentRoleID.ToString();
        }
    }
    public string DataImportSuccessTypeID
    {
        get
        {
            return ((short)WebEnums.DataImportStatus.Success).ToString();
        }
    }
    #endregion

    #region Delegates & Events
    #endregion

    #region Page Events
    protected void Page_Init(object sender, EventArgs e)
    {
        MasterPageBase oMaster = (MasterPageBase)this.Master;
        oMaster.ReconciliationPeriodChangedEventHandler += new EventHandler(oMaster_ReconciliationPeriodChangedEventHandler);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        Helper.SetPageTitle(this, 1219);


        //Commented By Prafull on 20-Jan-2010
        Helper.RegisterPostBackToControls(this, rgRecAndHolidayDataImport);
        GridHelper.SetRecordCount(rgRecAndHolidayDataImport);
        GridHelper.SetRecordCount(rgDataImport);
        if (!Page.IsPostBack)
        {
            isExportPDF = false;
            isExportExcel = false;



            if ((SessionHelper.CurrentRoleID == (short)WebEnums.UserRole.SYSTEM_ADMIN))
            {

                lblGridHeading.Text = LanguageUtil.GetValue(1056) + " / " + LanguageUtil.GetValue(1420) + " / " + LanguageUtil.GetValue(1058);
            }
            else if ((SessionHelper.CurrentRoleID == (short)WebEnums.UserRole.BUSINESS_ADMIN))
            {
                lblGridHeading.Text = LanguageUtil.GetValue(1058);

            }


            // Add Default Sort as Import Data, Desc
            GridSortExpression oGridSortExpression = new GridSortExpression();
            oGridSortExpression.FieldName = "DateAdded";
            oGridSortExpression.SortOrder = GridSortOrder.Descending;
            rgDataImport.MasterTableView.SortExpressions.AddSortExpression(oGridSortExpression);

            // Add Default Sort as Import Data, Desc
            rgRecAndHolidayDataImport.MasterTableView.SortExpressions.AddSortExpression(oGridSortExpression);
            SessionHelper.ClearSearchResultsFromSession();


        }
        Sel.Value = "";


        if ((SessionHelper.CurrentRoleID == (short)WebEnums.UserRole.SYSTEM_ADMIN) || (SessionHelper.CurrentRoleID == (short)WebEnums.UserRole.BUSINESS_ADMIN))
        {

            this.btnDeleteDataImport.Visible = true;
            this.btnNewDataImport.Visible = true;
        }
        else
        {
            this.btnNewDataImport.Visible = false;
            this.btnDeleteDataImport.Visible = false;
        }
        if ((SessionHelper.CurrentRoleID == (short)WebEnums.UserRole.SYSTEM_ADMIN))
        {
            //pnlRecAndHolidayDataImport.Visible = true;
            pnlBusinessAdminDDL.Visible = true;
        }
        else
        {
            //pnlRecAndHolidayDataImport.Visible = false;
            pnlBusinessAdminDDL.Visible = false;
        }

        if (!Page.IsPostBack)
            ShowHideButttons();

    }

    protected void Page_PreRender(Object sender, EventArgs e)
    {



    }
    #endregion

    #region Grid Events
    #region Data Import Grid By Rec Period ID
    protected void rgDataImport_ItemCreated(object sender, GridItemEventArgs e)
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
    protected void rgDataImport_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {
        if (e.Item.ItemType == Telerik.Web.UI.GridItemType.Item ||
            e.Item.ItemType == Telerik.Web.UI.GridItemType.AlternatingItem)
        {
            BindCommonFields(e);
            GridDataItem item = (GridDataItem)e.Item;
            CheckBox checkBox = (CheckBox)item["CheckboxSelectColumn"].Controls[0];
            DataImportHdrInfo oDataImportHdrInfo = (DataImportHdrInfo)e.Item.DataItem;


            // ********Not to be deleted. To be implemented later(Show/Hide rows)
            //*************  Image Button To ShowHideRows
            //ExImageButton imgbtnShowRows = (ExImageButton)e.Item.FindControl("imgbtnShowRows");
            //ExImageButton imgbtnHideRows = (ExImageButton)e.Item.FindControl("imgbtnHideRows");
            //imgbtnShowRows.ToolTip = LanguageUtil.GetValue(2108);
            //imgbtnHideRows.ToolTip = LanguageUtil.GetValue(2107);
            //imgbtnShowRows.Visible = (Boolean)oDataImportHdrInfo.IsHidden;
            //imgbtnHideRows.Visible = !((Boolean)oDataImportHdrInfo.IsHidden);



            // Net value
            ExHyperLink hlNetValue = (ExHyperLink)e.Item.FindControl("hlNetValue");
            hlNetValue.Text = Helper.GetDisplayReportingCurrencyValue(oDataImportHdrInfo.NetValue);

            // Show Data for Force Commit
            ExHyperLink hlForceCommitDetails = (ExHyperLink)e.Item.FindControl("hlForceCommitDetails");
            hlForceCommitDetails.Text = Helper.GetDisplayDateTime(oDataImportHdrInfo.ForceCommitDate);

            ExImage imgMultiVirsionIcon = (ExImage)e.Item.FindControl("imgMultiVirsionIcon");
            if (oDataImportHdrInfo.IsMultiVersionUpload.HasValue)
            {
                imgMultiVirsionIcon.Visible = oDataImportHdrInfo.IsMultiVersionUpload.Value;
            }
            else
            {
                imgMultiVirsionIcon.Visible = false;
            }
            ExImage imgFTPUpload = (ExImage)e.Item.FindControl("imgFTPUpload");
            if (oDataImportHdrInfo.RecordSourceTypeID.HasValue && oDataImportHdrInfo.RecordSourceTypeID.Value == (short) ARTEnums.RecordSourceType.FTP)
            {
                imgFTPUpload.Visible = true;
            }
            else
            {
                imgFTPUpload.Visible = false;
            }
            

            // Set URLs
            string url = GetUrlForDataImportStatusPage(oDataImportHdrInfo);
            hlNetValue.NavigateUrl = url;
            hlForceCommitDetails.NavigateUrl = url;

            DataImportHdrInfo obj = (DataImportHdrInfo)e.Item.DataItem;

            short status = obj.DataImportStatusID.Value;
            short importType = obj.DataImportTypeID.Value;

            if (checkBox != null)
            {
                if ((importType == (short)ARTEnums.DataImportType.GLData || importType == (short)ARTEnums.DataImportType.AccountUpload) && status == (short)WebEnums.DataImportStatus.Success)
                {
                    if (SessionHelper.CurrentUserLoginID == oDataImportHdrInfo.AddedBy && (short)CurrentRecProcessStatus.Value == (short)WebEnums.RecPeriodStatus.NotStarted)
                    {
                        checkBox.Enabled = true;
                        item.SelectableMode = GridItemSelectableMode.ServerAndClientSide;
                    }
                    else
                    {
                        checkBox.Enabled = false;
                        Sel.Value += e.Item.ItemIndex.ToString() + ":";
                        item.SelectableMode = GridItemSelectableMode.None;
                    }
                }
                else if (importType == (short)ARTEnums.DataImportType.RecControlChecklist && SessionHelper.CurrentRoleID == oDataImportHdrInfo.RoleID && ((short)CurrentRecProcessStatus.Value == (short)WebEnums.RecPeriodStatus.NotStarted || (short)CurrentRecProcessStatus.Value == (short)WebEnums.RecPeriodStatus.Open))
                {
                    checkBox.Enabled = true;
                    item.SelectableMode = GridItemSelectableMode.ServerAndClientSide;
                }
                else
                {
                    checkBox.Enabled = false;
                    Sel.Value += e.Item.ItemIndex.ToString() + ":";
                    item.SelectableMode = GridItemSelectableMode.None;
                }
            }
        }
    }
    protected void rgDataImport_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
    {
        rgDataImportLoadData(SessionHelper.CurrentUserID, false);
    }
    protected void rgDataImport_SortCommand(object source, GridSortCommandEventArgs e)
    {
        GridHelper.HandleSortCommand(e);
        rgDataImport.Rebind();
    }
    protected void rgDataImport_ItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == TelerikConstants.GridExportToPDFCommandName)
            {
                isExportPDF = true;
                rgDataImport.MasterTableView.Columns.FindByUniqueName("imgStatus").Visible = false;
                rgDataImport.MasterTableView.Columns.FindByUniqueName("CheckboxSelectColumn").Visible = false;
                GridHelper.ExportGridToPDF(rgDataImport, 1219);

            }
            if (e.CommandName == TelerikConstants.GridExportToExcelCommandName)
            {
                isExportExcel = true;
                rgDataImport.MasterTableView.Columns.FindByUniqueName("imgStatus").Visible = false;
                rgDataImport.MasterTableView.Columns.FindByUniqueName("CheckboxSelectColumn").Visible = false;
                GridHelper.ExportGridToExcel(rgDataImport, 1219);
            }
            if (e.CommandName == TelerikConstants.GridRefreshCommandName)
            {
                Session[SessionConstants.SEARCH_RESULTS_DATA_IMPORT_REC_PERIOD] = null;
                rgDataImport.Rebind();
            }

            // ********Not to be deleted. To be implemented later(Show/Hide rows)
            //if (e.CommandName == WebConstants.HIDE_SHOW_ROWS)
            //{
            //    GridDataItem oGridDataItem = (GridDataItem)e.Item;
            //    int dataImportID = Convert.ToInt32(oGridDataItem["DataImportID"].Text);
            //    bool isHidden = !(Convert.ToBoolean(oGridDataItem["IsHidden"].Text));
            //    IDataImport oDataImportClient = RemotingHelper.GetDataImportObject();
            //    oDataImportClient.UpdateDataImportHiddenStatusByDataImportID(dataImportID, isHidden);

            //    SessionHelper.ClearSearchResultsFromSession(SessionConstants.SEARCH_RESULTS_DATA_IMPORT_COMPANY);
            //    rgDataImport.Rebind();

            //}


        }
        catch (Exception ex)
        {
            Helper.ShowErrorMessage(this, ex);
        }
    }

    // ********Not to be deleted. To be implemented later(Show/Hide rows)
    //protected void chkShowHiddenGLDataImport_OnCheckedChanged(object Sender, EventArgs e)
    //{
    //    SessionHelper.ClearSearchResultsFromSession(SessionConstants.SEARCH_RESULTS_DATA_IMPORT_COMPANY);
    //    rgDataImport.Rebind();

    //}

    #endregion

    #region Data Import Grid By Company ID
    protected void rgRecAndHolidayDataImport_ItemCreated(object sender, GridItemEventArgs e)
    {
        GridHelper.SetStylesForExportGrid(e, isExportPDF, isExportExcel);
    }
    protected void rgRecAndHolidayDataImport_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {
        if (e.Item.ItemType == Telerik.Web.UI.GridItemType.Item ||
            e.Item.ItemType == Telerik.Web.UI.GridItemType.AlternatingItem)
        {
            BindCommonFields(e);
        }

    }
    protected void rgRecAndHolidayDataImport_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    {
        LoadrgRecAndHolidayData(SessionHelper.CurrentUserID, false);

    }

    protected void rgRecAndHolidayDataImport_SortCommand(object source, GridSortCommandEventArgs e)
    {
        GridHelper.HandleSortCommand(e);
        rgRecAndHolidayDataImport.Rebind();
    }
    protected void rgRecAndHolidayDataImport_ItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == TelerikConstants.GridExportToPDFCommandName)
            {
                isExportPDF = true;
                rgRecAndHolidayDataImport.MasterTableView.Columns.FindByUniqueName("imgStatus").Visible = false;
                GridHelper.ExportGridToPDF(rgRecAndHolidayDataImport, 1219);

            }
            if (e.CommandName == TelerikConstants.GridExportToExcelCommandName)
            {
                isExportExcel = true;
                rgRecAndHolidayDataImport.MasterTableView.Columns.FindByUniqueName("imgStatus").Visible = false;
                GridHelper.ExportGridToExcel(rgRecAndHolidayDataImport, 1219);
            }


        }
        catch (Exception ex)
        {
            Helper.ShowErrorMessage(this, ex);
        }
    }

    #endregion
    #endregion

    #region Other Events
    protected void btnNewDataImport_Click(object sender, EventArgs e)
    {
        Response.Redirect("DataImport.aspx");
    }
    protected void btnDeleteDataImport_Click(object sender, EventArgs e)
    {
        MasterPageBase oMasterPageBase = (MasterPageBase)this.Master;
        oMasterPageBase.HideMessage();
        List<int> oDataImportIDCollection = new List<int>();
        IDataImport oDataImportClient = null;
        int? rowsAffected = null;
        try
        {
            GridDataItem[] dataItemCollection = this.rgDataImport.MasterTableView.GetSelectedItems();
            if (dataItemCollection.Length > 0)
            {
                int dataImportID;
                foreach (GridDataItem item in dataItemCollection)
                {
                    if (Int32.TryParse(item.GetDataKeyValue("DataImportID").ToString(), out dataImportID))
                    {

                        if ((Convert.ToInt16(item.GetDataKeyValue("DataImportTypeID")) == (short)ARTEnums.DataImportType.GLData || Convert.ToInt16(item.GetDataKeyValue("DataImportTypeID")) == (short)ARTEnums.DataImportType.AccountUpload)
                            && Convert.ToInt16(item.GetDataKeyValue("DataImportStatusID")) == (short)WebEnums.DataImportStatus.Success && (short)CurrentRecProcessStatus.Value == (short)WebEnums.RecPeriodStatus.NotStarted)
                        {

                            oDataImportIDCollection.Add(dataImportID);
                        }
                        if (Convert.ToInt16(item.GetDataKeyValue("DataImportTypeID")) == (short)ARTEnums.DataImportType.RecControlChecklist && Convert.ToInt16(item.GetDataKeyValue("DataImportStatusID")) == (short)WebEnums.DataImportStatus.Success
                            && ((short)CurrentRecProcessStatus.Value == (short)WebEnums.RecPeriodStatus.NotStarted || (short)CurrentRecProcessStatus.Value == (short)WebEnums.RecPeriodStatus.Open))
                        {

                            oDataImportIDCollection.Add(dataImportID);
                        }
                    }
                }
                oDataImportClient = RemotingHelper.GetDataImportObject();

                rowsAffected = oDataImportClient.DeleteDataImportByDataImportIDs(oDataImportIDCollection
                    , SessionHelper.CurrentCompanyID.Value, SessionHelper.CurrentReconciliationPeriodID.Value
                    , SessionHelper.CurrentUserLoginID, DateTime.Now, (short)WebEnums.RecPeriodStatus.NotStarted, Helper.GetAppUserInfo());
                if (rowsAffected > 0)
                {
                    SessionHelper.ClearSession(SessionConstants.SEARCH_RESULTS_DATA_IMPORT_REC_PERIOD);
                    this.rgDataImport.Rebind();
                    ShowHideButttons();
                    oMasterPageBase.ShowConfirmationMessage(1938);
                }
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
    protected void ddlBA_SelectedIndexChanged(object sender, EventArgs e)
    {

        LoadrgRecAndHolidayData(Convert.ToInt32(this.ddlBA.SelectedValue), true);
        rgDataImportLoadData(Convert.ToInt32(this.ddlBA.SelectedValue), true);

        if (this.ddlBA.SelectedValue == "-2")
        {

            lblGridHeading.Text = LanguageUtil.GetValue(1056) + " / " + LanguageUtil.GetValue(1420) + " / " + LanguageUtil.GetValue(1058);
        }
        else
        {
            lblGridHeading.Text = LanguageUtil.GetValue(1058);
        }
    }
    void oMaster_ReconciliationPeriodChangedEventHandler(object sender, EventArgs e)
    {
        // Rebind the Data Import Status Grid
        Sel.Value = "";
        ShowHideButttons();
        SetBADDl();
        Session.Remove(SessionConstants.SEARCH_RESULTS_DATA_IMPORT_REC_PERIOD);
        rgDataImport.Rebind();
        //rgDataImportLoadData(null, true);
        MasterPageBase oMasterPageBase = (MasterPageBase)this.Master;
        oMasterPageBase.HideMessage();
    }
    #endregion

    #region Validation Control Events
    #endregion

    #region Private Methods
    private void rgDataImportLoadData(int? UserID, bool FromDDL)
    {
        try
        {

            List<DataImportHdrInfo> oDataImportHdrInfoCollection = null;
            IDataImport oDataImportClient = RemotingHelper.GetDataImportObject();

            // Get All Data Imports done in the Current Rec Period
            if (SessionHelper.CurrentReconciliationPeriodID != null || SessionHelper.CurrentRoleID == (short)WebEnums.UserRole.SKYSTEM_ADMIN)
            {
                //bool showHiddenRows = chkShowHiddenGLDataImport.Checked;
                int? recPeriodID = null;
                if (SessionHelper.CurrentRoleID != (short)WebEnums.UserRole.SKYSTEM_ADMIN)
                    recPeriodID = SessionHelper.CurrentReconciliationPeriodID;
                bool showHiddenRows = true;
                if (FromDDL && UserID != -2)
                    oDataImportHdrInfoCollection = oDataImportClient.GetDataImportStatusByUserID(recPeriodID, showHiddenRows, UserID, (short)WebEnums.UserRole.BUSINESS_ADMIN, Helper.GetAppUserInfo());
                else
                    oDataImportHdrInfoCollection = oDataImportClient.GetDataImportStatusByUserID(recPeriodID, showHiddenRows, SessionHelper.CurrentUserID, SessionHelper.CurrentRoleID, Helper.GetAppUserInfo());

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
            if (FromDDL)
                rgDataImport.DataBind();


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

    private void LoadrgRecAndHolidayData(int? UserID, bool FromDDL)
    {

        try
        {
            // Check in Session Object
            //List<DataImportHdrInfo> oRecHolidayDataImportHdrInfoCollection = (List<DataImportHdrInfo>)Session[SessionConstants.SEARCH_RESULTS_DATA_IMPORT_COMPANY];
            List<DataImportHdrInfo> oRecHolidayDataImportHdrInfoCollection = null;
            // Get All Data Imports done For Rec Period and Holiday Calendar
            IDataImport oDataImportClient = RemotingHelper.GetDataImportObject();


            if (FromDDL && UserID != -2)
                oRecHolidayDataImportHdrInfoCollection = oDataImportClient.GetDataImportStatusByCompanyID(SessionHelper.CurrentCompanyID, UserID, (short)WebEnums.UserRole.BUSINESS_ADMIN, Helper.GetAppUserInfo());
            else
                oRecHolidayDataImportHdrInfoCollection = oDataImportClient.GetDataImportStatusByCompanyID(SessionHelper.CurrentCompanyID, SessionHelper.CurrentUserID, SessionHelper.CurrentRoleID, Helper.GetAppUserInfo());

            //oRecHolidayDataImportHdrInfoCollection = oDataImportClient.GetDataImportStatusByCompanyID(SessionHelper.CurrentCompanyID);

            // Get the Values for Status and Type
            for (int i = 0; i < oRecHolidayDataImportHdrInfoCollection.Count; i++)
            {
                oRecHolidayDataImportHdrInfoCollection[i].DataImportStatus = LanguageUtil.GetValue(oRecHolidayDataImportHdrInfoCollection[i].DataImportStatusLabelID.Value);
                oRecHolidayDataImportHdrInfoCollection[i].DataImportType = LanguageUtil.GetValue(oRecHolidayDataImportHdrInfoCollection[i].DataImportTypeLabelID.Value);
            }

            //Session[SessionConstants.SEARCH_RESULTS_DATA_IMPORT_COMPANY] = oRecHolidayDataImportHdrInfoCollection;
            //SessionHelper.ClearSearchResultsFromSession(SessionConstants.SEARCH_RESULTS_DATA_IMPORT_COMPANY, SessionConstants.SEARCH_RESULTS_DATA_IMPORT_REC_PERIOD);

            // Sort the Data
            rgRecAndHolidayDataImport.MasterTableView.DataSource = oRecHolidayDataImportHdrInfoCollection;
            if (FromDDL)
                rgRecAndHolidayDataImport.DataBind();
            GridHelper.SortDataSource(rgRecAndHolidayDataImport.MasterTableView);
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

    private void ShowHideButttons()
    {
        if (SessionHelper.CurrentReconciliationPeriodID != null)
        {
            GridColumn selectColumn = this.rgDataImport.Columns.FindByUniqueNameSafe("CheckboxSelectColumn");
            if (selectColumn != null)
            {
                if (!DataImportHelper.IsDeletePermittedByRecPeriodStatus())
                {
                    selectColumn.Visible = false;
                    this.btnDeleteDataImport.Enabled = false;
                    this.btnNewDataImport.Enabled = true;
                }
                else
                {
                    selectColumn.Visible = true;
                    this.btnDeleteDataImport.Enabled = true;
                    this.btnNewDataImport.Enabled = true;
                    //foreach (GridDataItem gdItem in this.rgDataImport.MasterTableView.Items)
                    //{
                    //    if ((Convert.ToInt16(gdItem.GetDataKeyValue("DataImportTypeID")) == (short)ARTEnums.DataImportType.GLData || Convert.ToInt16(gdItem.GetDataKeyValue("DataImportTypeID")) == (short)ARTEnums.DataImportType.AccountUpload || Convert.ToInt16(gdItem.GetDataKeyValue("DataImportTypeID")) == (short)ARTEnums.DataImportType.RecControlChecklist)
                    //         && Convert.ToInt16(gdItem.GetDataKeyValue("DataImportStatusID")) == (short)WebEnums.DataImportStatus.Success)
                    //    {

                    //        this.btnDeleteDataImport.Enabled = true;
                    //        break;
                    //    }
                    //}
                }
            }
        }
        else
            this.btnDeleteDataImport.Enabled = false;
    }
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
        ExHyperLink hlTemplateName = (ExHyperLink)e.Item.FindControl("hlTemplateName");

        lblAddedBy.Text = oDataImportHdrInfo.AddedBy;
        if (!string.IsNullOrEmpty(oDataImportHdrInfo.TemplateName))
        {
            hlTemplateName.Text = oDataImportHdrInfo.TemplateName;
        }
        else
        {
            hlTemplateName.Text = LanguageUtil.GetValue(2866);
        }
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
        SetHyperlinkForStatusPage(oDataImportHdrInfo, hlProfileName, hlDate, hlImportType, hlRecordsAffected, hlStatus, hlFileName, hlTemplateName);
    }

    private void SetHyperlinkForStatusPage(DataImportHdrInfo oDataImportHdrInfo, ExHyperLink hlProfileName,
        ExHyperLink hlDate, ExHyperLink hlImportType, ExHyperLink hlRecordsAffected, ExHyperLink hlStatus, ExHyperLink hlFileName, ExHyperLink hlTemplateName)
    {
        string url = GetUrlForDataImportStatusPage(oDataImportHdrInfo);
        hlStatus.NavigateUrl = url;
        hlProfileName.NavigateUrl = url;
        hlDate.NavigateUrl = url;
        hlImportType.NavigateUrl = url;
        hlRecordsAffected.NavigateUrl = url;
        hlFileName.NavigateUrl = url;
        hlTemplateName.NavigateUrl = url;
    }

    private static string GetUrlForDataImportStatusPage(DataImportHdrInfo oDataImportHdrInfo)
    {
        string url = "DataImportStatusMessages.aspx?" + QueryStringConstants.DATA_IMPORT_ID + "=" + oDataImportHdrInfo.DataImportID.ToString();
        return url;
    }


    #endregion

    #region Other Methods
    protected void SetBADDl()
    {
        try
        {
            List<UserHdrInfo> oUserHdrInfoCollection = null;
            oUserHdrInfoCollection = CacheHelper.SelectAllBusinessAdminForCurrentCompany();
            ddlBA.DataSource = oUserHdrInfoCollection;
            ddlBA.DataTextField = "Name";
            ddlBA.DataValueField = "UserID";
            ddlBA.DataBind();
            ListControlHelper.AddListItemForSelectOne(ddlBA);
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
        return "DataImportStatus";
    }
    #endregion

}
