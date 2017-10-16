using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using SkyStem.ART.Web.Utility;
using Telerik.Web.UI;
using SkyStem.ART.Client.Model;
using System.Collections.Generic;
using SkyStem.ART.Client.IServices;
using SkyStem.ART.Client.Exception;
using SkyStem.Library.Controls.WebControls;
using SkyStem.Library.Controls.TelerikWebControls;
using SkyStem.ART.Web.Data;
using SkyStem.ART.Web.Classes;
using SkyStem.Library.Controls.TelerikWebControls.Data;
using SkyStem.ART.Client.Data;
using System.Threading.Tasks;


public partial class UserControls_Dashboard_ReconciliationStatusByFSCaption : UserControlWebPartBase
{
    bool isExportPDF;
    bool isExportExcel;

    protected void Page_Init(object sender, EventArgs e)
    {
        Page oPage = (Page)this.Parent.Page;
        MasterPageBase ompage = (MasterPageBase)oPage.Master;
        ompage.ReconciliationPeriodChangedEventHandler += new EventHandler(ompage_ReconciliationPeriodChangedEventHandler);

    }


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            isExportPDF = false;
            isExportExcel = false;
            this.LoadData = true;
            BindReconciliationStatusDropdown();
        }
    }


    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (this.LoadData.Value && this.Visible)
        {
            OnPageLoad();
        }
    }

    protected void ompage_ReconciliationPeriodChangedEventHandler(object sender, EventArgs e)
    {
        this.LoadData = true;
    }


    private void SetDefaultSortExpression()
    {
        // Get the Review Notes
        if (!Page.IsPostBack)
        {
            // Add Default Sort as Date Revised, Desc
            GridSortExpression oGridSortExpression = new GridSortExpression();
            oGridSortExpression.FieldName = "FSCaption";
            oGridSortExpression.SortOrder = GridSortOrder.Ascending;
            rgReconciliationStatusByFSCaption.MasterTableView.SortExpressions.AddSortExpression(oGridSortExpression);
        }
    }

    #region "Grid Events"
    protected void rgReconciliationStatusByFSCaption_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    {
        LoadGridData();
    }

    public override IAsyncResult GetDataAsync()
    {
        IDashboard oDashboardClient = RemotingHelper.GetDashboardObject();
        return DashboardHelper.GetDataForReconciliationStatusByFSCaptionAsync(SessionHelper.CurrentUserID, SessionHelper.CurrentRoleID, SessionHelper.CurrentReconciliationPeriodID, Helper.GetAppUserInfo());
    }

    public override void DataLoaded(IAsyncResult result)
    {
        Task<List<ReconciliationStatusFSCaptionInfo>> oTask = (Task<List<ReconciliationStatusFSCaptionInfo>>)result;
        if (oTask.IsCompleted)
        {
            LoadGridData(oTask.Result);
        }
    }
    private void LoadGridData()
    {
        List<ReconciliationStatusFSCaptionInfo> oReconciliationStatusFSCaptionInfoCollection = null;
        oReconciliationStatusFSCaptionInfoCollection = DashboardHelper.GetDataForReconciliationStatusByFSCaption(SessionHelper.CurrentUserID, SessionHelper.CurrentRoleID, SessionHelper.CurrentReconciliationPeriodID, Helper.GetAppUserInfo());
    }
    private void LoadGridData(List<ReconciliationStatusFSCaptionInfo> oReconciliationStatusFSCaptionInfoCollection)
    {
        try
        {
            if (oReconciliationStatusFSCaptionInfoCollection != null)
            {
                LanguageHelper.TranslateLabelReconciliationStatusFSCaptionInfo(oReconciliationStatusFSCaptionInfoCollection);
                Session[SessionConstants.SEARCH_RESULTS_RECONCILIATION_STATUS_FSCAPTION] = oReconciliationStatusFSCaptionInfoCollection;
                rgReconciliationStatusByFSCaption.DataSource = oReconciliationStatusFSCaptionInfoCollection;
                // Sort the Data
                GridHelper.SortDataSource(rgReconciliationStatusByFSCaption.MasterTableView);
            }
        }
        catch (ARTException ex)
        {
            WebPartHelper.ShowErrorMessage(tblMessage, tblContent, lblMessage, ex);
        }
        catch (Exception ex)
        {
            WebPartHelper.ShowErrorMessage(tblMessage, tblContent, lblMessage, ex);
        }
    }

    protected void rgReconciliationStatusByFSCaption_ItemDataBound(object sender, GridItemEventArgs e)
    {
        if (e.Item.ItemType == GridItemType.Item
            || e.Item.ItemType == GridItemType.AlternatingItem)
        {
            ReconciliationStatusFSCaptionInfo oReconciliationStatusFSCaptionInfo = (ReconciliationStatusFSCaptionInfo)e.Item.DataItem;

            ExLinkButton lbtnFSCaption = (ExLinkButton)e.Item.FindControl("lbtnFSCaption");
            ExLinkButton lbtnTotal = (ExLinkButton)e.Item.FindControl("lbtnTotal");

            ExLinkButton lbtnPrepared = (ExLinkButton)e.Item.FindControl("lbtnPrepared");
            ExLinkButton lbtnInProgress = (ExLinkButton)e.Item.FindControl("lbtnInProgress");
            ExLinkButton lbtnPendingReview = (ExLinkButton)e.Item.FindControl("lbtnPendingReview");
            ExLinkButton lbtnPendingModificationPreparer = (ExLinkButton)e.Item.FindControl("lbtnPendingModificationPreparer");
            ExLinkButton lbtnReviewed = (ExLinkButton)e.Item.FindControl("lbtnReviewed");
            ExLinkButton lbtnPendingApproval = (ExLinkButton)e.Item.FindControl("lbtnPendingApproval");
            ExLinkButton lbtnApproved = (ExLinkButton)e.Item.FindControl("lbtnApproved");
            ExLinkButton lbtnNotstarted = (ExLinkButton)e.Item.FindControl("lbtnNotstarted");
            ExLinkButton lbtnSysReconciled = (ExLinkButton)e.Item.FindControl("lbtnSysReconciled");
            ExLinkButton lbtnReconciled = (ExLinkButton)e.Item.FindControl("lbtnReconciled");
            ExLinkButton lbtnPendingModificationReviewer = (ExLinkButton)e.Item.FindControl("lbtnPendingModificationReviewer");




            ExLabel lFSCaption = (ExLabel)e.Item.FindControl("lFSCaption");
            ExLabel lTotal = (ExLabel)e.Item.FindControl("lTotal");

            ExLabel lPrepared = (ExLabel)e.Item.FindControl("lPrepared");
            ExLabel lInProgress = (ExLabel)e.Item.FindControl("lInProgress");
            ExLabel lPendingReview = (ExLabel)e.Item.FindControl("lPendingReview");
            ExLabel lPendingModificationPreparer = (ExLabel)e.Item.FindControl("lPendingModificationPreparer");
            ExLabel lReviewed = (ExLabel)e.Item.FindControl("lReviewed");
            ExLabel lPendingApproval = (ExLabel)e.Item.FindControl("lPendingApproval");
            ExLabel lApproved = (ExLabel)e.Item.FindControl("lApproved");
            ExLabel lNotstarted = (ExLabel)e.Item.FindControl("lNotstarted");
            ExLabel lSysReconciled = (ExLabel)e.Item.FindControl("lSysReconciled");
            ExLabel lReconciled = (ExLabel)e.Item.FindControl("lReconciled");
            ExLabel lPendingModificationReviewer = (ExLabel)e.Item.FindControl("lPendingModificationReviewer");




            lbtnFSCaption.Text = Helper.GetDisplayStringValue(oReconciliationStatusFSCaptionInfo.FSCaption);
            lbtnTotal.Text = Helper.GetDisplayIntegerValue(oReconciliationStatusFSCaptionInfo.TotalCount);

            lbtnPrepared.Text = Helper.GetDisplayIntegerValue(oReconciliationStatusFSCaptionInfo.Prepared);
            lbtnInProgress.Text = Helper.GetDisplayIntegerValue(oReconciliationStatusFSCaptionInfo.InProgress);
            lbtnPendingReview.Text = Helper.GetDisplayIntegerValue(oReconciliationStatusFSCaptionInfo.PendingReview);
            lbtnPendingModificationPreparer.Text = Helper.GetDisplayIntegerValue(oReconciliationStatusFSCaptionInfo.PendingModificationPreparer);
            lbtnReviewed.Text = Helper.GetDisplayIntegerValue(oReconciliationStatusFSCaptionInfo.Reviewed);
            lbtnPendingApproval.Text = Helper.GetDisplayIntegerValue(oReconciliationStatusFSCaptionInfo.PendingApproval);
            lbtnApproved.Text = Helper.GetDisplayIntegerValue(oReconciliationStatusFSCaptionInfo.Approved);
            lbtnNotstarted.Text = Helper.GetDisplayIntegerValue(oReconciliationStatusFSCaptionInfo.Notstarted);
            lbtnSysReconciled.Text = Helper.GetDisplayIntegerValue(oReconciliationStatusFSCaptionInfo.SysReconciled);
            lbtnReconciled.Text = Helper.GetDisplayIntegerValue(oReconciliationStatusFSCaptionInfo.Reconciled);
            lbtnPendingModificationReviewer.Text = Helper.GetDisplayIntegerValue(oReconciliationStatusFSCaptionInfo.PendingModificationReviewer);



            lFSCaption.Text = Helper.GetDisplayStringValue(oReconciliationStatusFSCaptionInfo.FSCaption);
            lTotal.Text = Helper.GetDisplayIntegerValue(oReconciliationStatusFSCaptionInfo.TotalCount);
            lPrepared.Text = Helper.GetDisplayIntegerValue(oReconciliationStatusFSCaptionInfo.Prepared);
            lInProgress.Text = Helper.GetDisplayIntegerValue(oReconciliationStatusFSCaptionInfo.InProgress);
            lPendingReview.Text = Helper.GetDisplayIntegerValue(oReconciliationStatusFSCaptionInfo.PendingReview);
            lPendingModificationPreparer.Text = Helper.GetDisplayIntegerValue(oReconciliationStatusFSCaptionInfo.PendingModificationPreparer);
            lReviewed.Text = Helper.GetDisplayIntegerValue(oReconciliationStatusFSCaptionInfo.Reviewed);
            lPendingApproval.Text = Helper.GetDisplayIntegerValue(oReconciliationStatusFSCaptionInfo.PendingApproval);
            lApproved.Text = Helper.GetDisplayIntegerValue(oReconciliationStatusFSCaptionInfo.Approved);
            lNotstarted.Text = Helper.GetDisplayIntegerValue(oReconciliationStatusFSCaptionInfo.Notstarted);
            lSysReconciled.Text = Helper.GetDisplayIntegerValue(oReconciliationStatusFSCaptionInfo.SysReconciled);
            lReconciled.Text = Helper.GetDisplayIntegerValue(oReconciliationStatusFSCaptionInfo.Reconciled);
            lPendingModificationReviewer.Text = Helper.GetDisplayIntegerValue(oReconciliationStatusFSCaptionInfo.PendingModificationReviewer);



            //// Set the Drilldown Url
            //string url = GetDrilldownUrl();
            lbtnFSCaption.CommandArgument = oReconciliationStatusFSCaptionInfo.FSCaption + ",0";
            lbtnTotal.CommandArgument = oReconciliationStatusFSCaptionInfo.FSCaption + ",0";


            lbtnPrepared.CommandArgument = oReconciliationStatusFSCaptionInfo.FSCaption + "," + ((short)WebEnums.ReconciliationStatus.Prepared).ToString();
            lbtnInProgress.CommandArgument = oReconciliationStatusFSCaptionInfo.FSCaption + "," + ((short)WebEnums.ReconciliationStatus.InProgress).ToString();
            lbtnPendingReview.CommandArgument = oReconciliationStatusFSCaptionInfo.FSCaption + "," + ((short)WebEnums.ReconciliationStatus.PendingReview).ToString();
            lbtnPendingModificationPreparer.CommandArgument = oReconciliationStatusFSCaptionInfo.FSCaption + "," + ((short)WebEnums.ReconciliationStatus.PendingModificationPreparer).ToString();
            lbtnReviewed.CommandArgument = oReconciliationStatusFSCaptionInfo.FSCaption + "," + ((short)WebEnums.ReconciliationStatus.Reviewed).ToString();
            lbtnPendingApproval.CommandArgument = oReconciliationStatusFSCaptionInfo.FSCaption + "," + ((short)WebEnums.ReconciliationStatus.PendingApproval).ToString();
            lbtnApproved.CommandArgument = oReconciliationStatusFSCaptionInfo.FSCaption + "," + ((short)WebEnums.ReconciliationStatus.Approved).ToString();
            lbtnNotstarted.CommandArgument = oReconciliationStatusFSCaptionInfo.FSCaption + "," + ((short)WebEnums.ReconciliationStatus.NotStarted).ToString();
            lbtnSysReconciled.CommandArgument = oReconciliationStatusFSCaptionInfo.FSCaption + "," + ((short)WebEnums.ReconciliationStatus.SysReconciled).ToString();
            lbtnReconciled.CommandArgument = oReconciliationStatusFSCaptionInfo.FSCaption + "," + ((short)WebEnums.ReconciliationStatus.Reconciled).ToString();
            lbtnPendingModificationReviewer.CommandArgument = oReconciliationStatusFSCaptionInfo.FSCaption + "," + ((short)WebEnums.ReconciliationStatus.PendingModificationReviewer).ToString();

        }
    }

    protected void rgReconciliationStatusByFSCaption_SortCommand(object source, GridSortCommandEventArgs e)
    {
        GridHelper.HandleSortCommand(e);
        rgReconciliationStatusByFSCaption.Rebind();
    }

    #endregion

    private string GetDrilldownUrl()
    {
        string url = "~/Pages/AccountViewer.aspx?";
        return url;
    }

    #region ReconciliationStatus Dropdown

    private void BindReconciliationStatusDropdown()
    {
        ListControlHelper.BindReconciliationStatusDropdown(ddlReconciliationStatus);

    }

    protected void ddlReconciliationStatus_SelectedIndexChanged(object sender, EventArgs e)
    {

        isExportPDF = false;
        isExportExcel = false;
        int SelectedStatus = System.Convert.ToInt32(ddlReconciliationStatus.SelectedValue);

        ShowHideGridColumnsBasedOnRecStatus(SelectedStatus);
        rgReconciliationStatusByFSCaption.DataSource = (List<ReconciliationStatusFSCaptionInfo>)Session[SessionConstants.SEARCH_RESULTS_RECONCILIATION_STATUS_FSCAPTION];
        rgReconciliationStatusByFSCaption.DataBind();
    }



    void ShowHideGridColumnsBasedOnRecStatus(int SelectedStatus)
    {
        if (SelectedStatus == 0)
        {
            ShowColumnByCondition("Prepared", 1, 1);
            ShowColumnByCondition("InProgress", 2, 2);
            ShowColumnByCondition("PendingReview", 3, 3);
            ShowColumnByCondition("PendingModificationPreparer", 4, 4);
            ShowColumnByCondition("Reviewed", 5, 5);
            ShowColumnByCondition("PendingApproval", 6, 6);
            ShowColumnByCondition("Approved", 7, 7);
            ShowColumnByCondition("Notstarted", 8, 8);
            ShowColumnByCondition("SysReconciled", 9, 9);
            ShowColumnByCondition("Reconciled", 10, 10);
            ShowColumnByCondition("PendingModificationReviewer", 11, 11);
        }
        else
        {

            ShowColumnByCondition("Prepared", SelectedStatus, 1);
            ShowColumnByCondition("InProgress", SelectedStatus, 2);
            ShowColumnByCondition("PendingReview", SelectedStatus, 3);
            ShowColumnByCondition("PendingModificationPreparer", SelectedStatus, 4);
            ShowColumnByCondition("Reviewed", SelectedStatus, 5);
            ShowColumnByCondition("PendingApproval", SelectedStatus, 6);
            ShowColumnByCondition("Approved", SelectedStatus, 7);
            ShowColumnByCondition("Notstarted", SelectedStatus, 8);
            ShowColumnByCondition("SysReconciled", SelectedStatus, 9);
            ShowColumnByCondition("Reconciled", SelectedStatus, 10);
            ShowColumnByCondition("PendingModificationReviewer", SelectedStatus, 11);
        }

    }

    void ShowHideGridDataColumnsBasedOnRecStatus(int SelectedStatus)
    {
        if (SelectedStatus == 0)
        {
            ShowColumnByCondition("PreparedData", 1, 1);
            ShowColumnByCondition("InProgressData", 2, 2);
            ShowColumnByCondition("PendingReviewData", 3, 3);
            ShowColumnByCondition("PendingModificationPreparerData", 4, 4);
            ShowColumnByCondition("ReviewedData", 5, 5);
            ShowColumnByCondition("PendingApprovalData", 6, 6);
            ShowColumnByCondition("ApprovedData", 7, 7);
            ShowColumnByCondition("NotstartedData", 8, 8);
            ShowColumnByCondition("SysReconciledData", 9, 9);
            ShowColumnByCondition("ReconciledData", 10, 10);
            ShowColumnByCondition("PendingModificationReviewerData", 11, 11);
        }
        else
        {

            ShowColumnByCondition("PreparedData", SelectedStatus, 1);
            ShowColumnByCondition("InProgressData", SelectedStatus, 2);
            ShowColumnByCondition("PendingReviewData", SelectedStatus, 3);
            ShowColumnByCondition("PendingModificationPreparerData", SelectedStatus, 4);
            ShowColumnByCondition("ReviewedData", SelectedStatus, 5);
            ShowColumnByCondition("PendingApprovalData", SelectedStatus, 6);
            ShowColumnByCondition("ApprovedData", SelectedStatus, 7);
            ShowColumnByCondition("NotstartedData", SelectedStatus, 8);
            ShowColumnByCondition("SysReconciledData", SelectedStatus, 9);
            ShowColumnByCondition("ReconciledData", SelectedStatus, 10);
            ShowColumnByCondition("PendingModificationReviewerData", SelectedStatus, 11);
        }

    }

    void ShowColumnByCondition(string columnName, int SelectedStatus, int matchingCondition)
    {

        GridColumn oGridColumn = rgReconciliationStatusByFSCaption.Columns.FindByUniqueNameSafe(columnName);
        if (oGridColumn != null)
        {
            oGridColumn.Visible = (SelectedStatus == matchingCondition ? true : false);
        }

    }

    #endregion


    protected void rgReconciliationStatusByFSCaption_ItemCreated(object sender, GridItemEventArgs e)
    {
        GridHelper.RegisterPDFAndExcelForPostback(e, isExportPDF, isExportExcel, this.Page);

        GridHelper.SetStylesForExportGrid(e, isExportPDF, isExportExcel);
    }

    protected void rgReconciliationStatusByFSCaption_OnItemCommand(object sender, GridCommandEventArgs e)
    {
        WebEnums.FeatureCapabilityMode eMode;

        eMode = Helper.GetFeatureCapabilityModeForCurrentRecPeriod(WebEnums.Feature.DualLevelReview, ARTEnums.Capability.DualLevelReview);

        if (eMode == WebEnums.FeatureCapabilityMode.Visible)
        {
            rgReconciliationStatusByFSCaption.MasterTableView.Columns.FindByUniqueName("PendingApprovalData").Visible = true;
            rgReconciliationStatusByFSCaption.MasterTableView.Columns.FindByUniqueName("PendingModificationReviewerData").Visible = true;
            rgReconciliationStatusByFSCaption.MasterTableView.Columns.FindByUniqueName("ApprovedData").Visible = true;

            rgReconciliationStatusByFSCaption.MasterTableView.Columns.FindByUniqueName("PendingApproval").Visible = true;
            rgReconciliationStatusByFSCaption.MasterTableView.Columns.FindByUniqueName("PendingModificationReviewer").Visible = true;
            rgReconciliationStatusByFSCaption.MasterTableView.Columns.FindByUniqueName("Approved").Visible = true;
        }
        else
        {
            rgReconciliationStatusByFSCaption.MasterTableView.Columns.FindByUniqueName("PendingApprovalData").Visible = false;
            rgReconciliationStatusByFSCaption.MasterTableView.Columns.FindByUniqueName("PendingModificationReviewerData").Visible = false;
            rgReconciliationStatusByFSCaption.MasterTableView.Columns.FindByUniqueName("ApprovedData").Visible = false;

            rgReconciliationStatusByFSCaption.MasterTableView.Columns.FindByUniqueName("PendingApproval").Visible = false;
            rgReconciliationStatusByFSCaption.MasterTableView.Columns.FindByUniqueName("PendingModificationReviewer").Visible = false;
            rgReconciliationStatusByFSCaption.MasterTableView.Columns.FindByUniqueName("Approved").Visible = false;
        }

        if (e.CommandName == TelerikConstants.GridExportToPDFCommandName)
        {
            LoadGridData();
            isExportPDF = true;
            rgReconciliationStatusByFSCaption.MasterTableView.Columns.FindByUniqueName("FSCaption").Visible = false;
            rgReconciliationStatusByFSCaption.MasterTableView.Columns.FindByUniqueName("Total").Visible = false;
            rgReconciliationStatusByFSCaption.MasterTableView.Columns.FindByUniqueName("Notstarted").Visible = false;
            rgReconciliationStatusByFSCaption.MasterTableView.Columns.FindByUniqueName("InProgress").Visible = false;
            rgReconciliationStatusByFSCaption.MasterTableView.Columns.FindByUniqueName("Prepared").Visible = false;
            rgReconciliationStatusByFSCaption.MasterTableView.Columns.FindByUniqueName("PendingReview").Visible = false;
            rgReconciliationStatusByFSCaption.MasterTableView.Columns.FindByUniqueName("PendingModificationPreparer").Visible = false;
            rgReconciliationStatusByFSCaption.MasterTableView.Columns.FindByUniqueName("Reviewed").Visible = false;
            rgReconciliationStatusByFSCaption.MasterTableView.Columns.FindByUniqueName("PendingApproval").Visible = false;
            rgReconciliationStatusByFSCaption.MasterTableView.Columns.FindByUniqueName("PendingModificationReviewer").Visible = false;
            rgReconciliationStatusByFSCaption.MasterTableView.Columns.FindByUniqueName("Approved").Visible = false;
            rgReconciliationStatusByFSCaption.MasterTableView.Columns.FindByUniqueName("Reconciled").Visible = false;
            rgReconciliationStatusByFSCaption.MasterTableView.Columns.FindByUniqueName("SysReconciled").Visible = false;


            rgReconciliationStatusByFSCaption.MasterTableView.Columns.FindByUniqueName("FSCaptionData").Visible = true;
            rgReconciliationStatusByFSCaption.MasterTableView.Columns.FindByUniqueName("TotalData").Visible = true;
            rgReconciliationStatusByFSCaption.MasterTableView.Columns.FindByUniqueName("NotstartedData").Visible = true;
            rgReconciliationStatusByFSCaption.MasterTableView.Columns.FindByUniqueName("InProgressData").Visible = true;
            rgReconciliationStatusByFSCaption.MasterTableView.Columns.FindByUniqueName("PreparedData").Visible = true;
            rgReconciliationStatusByFSCaption.MasterTableView.Columns.FindByUniqueName("PendingReviewData").Visible = true;
            rgReconciliationStatusByFSCaption.MasterTableView.Columns.FindByUniqueName("PendingModificationPreparerData").Visible = true;
            rgReconciliationStatusByFSCaption.MasterTableView.Columns.FindByUniqueName("ReviewedData").Visible = true;
            rgReconciliationStatusByFSCaption.MasterTableView.Columns.FindByUniqueName("PendingApprovalData").Visible = true;
            rgReconciliationStatusByFSCaption.MasterTableView.Columns.FindByUniqueName("PendingModificationReviewerData").Visible = true;
            rgReconciliationStatusByFSCaption.MasterTableView.Columns.FindByUniqueName("ApprovedData").Visible = true;
            rgReconciliationStatusByFSCaption.MasterTableView.Columns.FindByUniqueName("ReconciledData").Visible = true;
            rgReconciliationStatusByFSCaption.MasterTableView.Columns.FindByUniqueName("SysReconciledData").Visible = true;

            //if (eMode == WebEnums.FeatureCapabilityMode.Visible)
            //{
            //    rgReconciliationStatusByFSCaption.MasterTableView.Columns.FindByUniqueName("PendingApprovalData").Visible = true;
            //    rgReconciliationStatusByFSCaption.MasterTableView.Columns.FindByUniqueName("PendingModificationReviewerData").Visible = true;
            //    rgReconciliationStatusByFSCaption.MasterTableView.Columns.FindByUniqueName("ApprovedData").Visible = true;

            //    //rgReconciliationStatusByFSCaption.MasterTableView.Columns.FindByUniqueName("PendingApproval").Visible = true;
            //    //rgReconciliationStatusByFSCaption.MasterTableView.Columns.FindByUniqueName("PendingModificationReviewer").Visible = true;
            //    //rgReconciliationStatusByFSCaption.MasterTableView.Columns.FindByUniqueName("Approved").Visible = true;
            //}
            //else
            //{
            //    rgReconciliationStatusByFSCaption.MasterTableView.Columns.FindByUniqueName("PendingApprovalData").Visible = true;
            //    rgReconciliationStatusByFSCaption.MasterTableView.Columns.FindByUniqueName("PendingModificationReviewerData").Visible = false;
            //    rgReconciliationStatusByFSCaption.MasterTableView.Columns.FindByUniqueName("ApprovedData").Visible = false;

            //    //rgReconciliationStatusByFSCaption.MasterTableView.Columns.FindByUniqueName("PendingApproval").Visible = false;
            //    //rgReconciliationStatusByFSCaption.MasterTableView.Columns.FindByUniqueName("PendingModificationReviewer").Visible = false;
            //    //rgReconciliationStatusByFSCaption.MasterTableView.Columns.FindByUniqueName("Approved").Visible = false;
            //}

            int SelectedStatus = System.Convert.ToInt32(ddlReconciliationStatus.SelectedValue);

            ShowHideGridDataColumnsBasedOnRecStatus(SelectedStatus);

            GridHelper.ExportGridToPDF(rgReconciliationStatusByFSCaption, 1464);

        }
        if (e.CommandName == TelerikConstants.GridExportToExcelCommandName)
        {
            LoadGridData();
            isExportExcel = true;
            rgReconciliationStatusByFSCaption.MasterTableView.Columns.FindByUniqueName("FSCaption").Visible = false;
            rgReconciliationStatusByFSCaption.MasterTableView.Columns.FindByUniqueName("Total").Visible = false;
            rgReconciliationStatusByFSCaption.MasterTableView.Columns.FindByUniqueName("Notstarted").Visible = false;
            rgReconciliationStatusByFSCaption.MasterTableView.Columns.FindByUniqueName("InProgress").Visible = false;
            rgReconciliationStatusByFSCaption.MasterTableView.Columns.FindByUniqueName("Prepared").Visible = false;
            rgReconciliationStatusByFSCaption.MasterTableView.Columns.FindByUniqueName("PendingReview").Visible = false;
            rgReconciliationStatusByFSCaption.MasterTableView.Columns.FindByUniqueName("PendingModificationPreparer").Visible = false;
            rgReconciliationStatusByFSCaption.MasterTableView.Columns.FindByUniqueName("Reviewed").Visible = false;
            rgReconciliationStatusByFSCaption.MasterTableView.Columns.FindByUniqueName("PendingApproval").Visible = false;
            rgReconciliationStatusByFSCaption.MasterTableView.Columns.FindByUniqueName("PendingModificationReviewer").Visible = false;
            rgReconciliationStatusByFSCaption.MasterTableView.Columns.FindByUniqueName("Approved").Visible = false;
            rgReconciliationStatusByFSCaption.MasterTableView.Columns.FindByUniqueName("Reconciled").Visible = false;
            rgReconciliationStatusByFSCaption.MasterTableView.Columns.FindByUniqueName("SysReconciled").Visible = false;


            rgReconciliationStatusByFSCaption.MasterTableView.Columns.FindByUniqueName("FSCaptionData").Visible = true;
            rgReconciliationStatusByFSCaption.MasterTableView.Columns.FindByUniqueName("TotalData").Visible = true;
            rgReconciliationStatusByFSCaption.MasterTableView.Columns.FindByUniqueName("NotstartedData").Visible = true;
            rgReconciliationStatusByFSCaption.MasterTableView.Columns.FindByUniqueName("InProgressData").Visible = true;
            rgReconciliationStatusByFSCaption.MasterTableView.Columns.FindByUniqueName("PreparedData").Visible = true;
            rgReconciliationStatusByFSCaption.MasterTableView.Columns.FindByUniqueName("PendingReviewData").Visible = true;
            rgReconciliationStatusByFSCaption.MasterTableView.Columns.FindByUniqueName("PendingModificationPreparerData").Visible = true;
            rgReconciliationStatusByFSCaption.MasterTableView.Columns.FindByUniqueName("ReviewedData").Visible = true;
            rgReconciliationStatusByFSCaption.MasterTableView.Columns.FindByUniqueName("PendingApprovalData").Visible = true;
            rgReconciliationStatusByFSCaption.MasterTableView.Columns.FindByUniqueName("PendingModificationReviewerData").Visible = true;
            rgReconciliationStatusByFSCaption.MasterTableView.Columns.FindByUniqueName("ApprovedData").Visible = true;
            rgReconciliationStatusByFSCaption.MasterTableView.Columns.FindByUniqueName("ReconciledData").Visible = true;
            rgReconciliationStatusByFSCaption.MasterTableView.Columns.FindByUniqueName("SysReconciledData").Visible = true;

            //if (eMode == WebEnums.FeatureCapabilityMode.Visible)
            //{
            //    rgReconciliationStatusByFSCaption.MasterTableView.Columns.FindByUniqueName("PendingApprovalData").Visible = true;
            //    rgReconciliationStatusByFSCaption.MasterTableView.Columns.FindByUniqueName("PendingModificationReviewerData").Visible = true;
            //    rgReconciliationStatusByFSCaption.MasterTableView.Columns.FindByUniqueName("ApprovedData").Visible = true;

            //    rgReconciliationStatusByFSCaption.MasterTableView.Columns.FindByUniqueName("PendingApproval").Visible = true;
            //    rgReconciliationStatusByFSCaption.MasterTableView.Columns.FindByUniqueName("PendingModificationReviewer").Visible = true;
            //    rgReconciliationStatusByFSCaption.MasterTableView.Columns.FindByUniqueName("Approved").Visible = true;
            //}
            //else
            //{
            //    rgReconciliationStatusByFSCaption.MasterTableView.Columns.FindByUniqueName("PendingApprovalData").Visible = false;
            //    rgReconciliationStatusByFSCaption.MasterTableView.Columns.FindByUniqueName("PendingModificationReviewerData").Visible = false;
            //    rgReconciliationStatusByFSCaption.MasterTableView.Columns.FindByUniqueName("ApprovedData").Visible = false;

            //    rgReconciliationStatusByFSCaption.MasterTableView.Columns.FindByUniqueName("PendingApproval").Visible = false;
            //    rgReconciliationStatusByFSCaption.MasterTableView.Columns.FindByUniqueName("PendingModificationReviewer").Visible = false;
            //    rgReconciliationStatusByFSCaption.MasterTableView.Columns.FindByUniqueName("Approved").Visible = false;
            //}

            int SelectedStatus = System.Convert.ToInt32(ddlReconciliationStatus.SelectedValue);

            ShowHideGridDataColumnsBasedOnRecStatus(SelectedStatus);

            GridHelper.ExportGridToExcel(rgReconciliationStatusByFSCaption, 1464);

        }

    }

    protected void SendToAccountViewer(object sender, CommandEventArgs e)
    {
        ARTEnums.Grid eGrid = ARTEnums.Grid.AccountViewer;
        string CommandArgumentVal = e.CommandArgument.ToString();
        string[] ArrFscatoipnIDAndStatusID = CommandArgumentVal.Split(',');

        short columnID = (short)WebEnums.StaticAccountField.ReconciliationStatus;
        short operatorID = (short)WebEnums.Operator.Matches;
        string value = ArrFscatoipnIDAndStatusID[1];
        SessionHelper.ClearGridFilterDataFromSession(eGrid);
        if (value != "0")
            AccountFilterHelper.AddCriteriaToSessionByDashBoardRecStatus(columnID, operatorID, value, eGrid);

        operatorID = (short)WebEnums.Operator.Contains;
        columnID = (short)WebEnums.StaticAccountField.FSCaption;
        value = ArrFscatoipnIDAndStatusID[0];

        AccountFilterHelper.AddCriteriaToSessionByDashBoardFSCaption(columnID, operatorID, value, eGrid);

        PageSettings oPageSettings = PageSettingHelper.GetPageSettings(WebEnums.ARTPages.AccountViewer);
        oPageSettings.ShowSRAAsWell = true;
        PageSettingHelper.SavePageSettings(WebEnums.ARTPages.AccountViewer, oPageSettings);

        string url = "~/Pages/AccountViewer.aspx?" + QueryStringConstants.IS_SRA + "=1";
        //Response.Redirect(url);
        SessionHelper.RedirectToUrl(url);
        return;
    }

    private void OnPageLoad()
    {
        rgReconciliationStatusByFSCaption.NeedDataSource += new Telerik.Web.UI.GridNeedDataSourceEventHandler(rgReconciliationStatusByFSCaption_NeedDataSource);


        // Set default Sorting
        SetDefaultSortExpression();
        rgReconciliationStatusByFSCaption.Rebind();
    }


    protected void rgReconciliationStatusByFSCaption_PreRender(object sender, EventArgs e)
    {
         WebEnums.FeatureCapabilityMode eMode = Helper.GetFeatureCapabilityMode(WebEnums.Feature.DualLevelReview, ARTEnums.Capability.DualLevelReview, SessionHelper.CurrentReconciliationPeriodID);
         if (eMode == WebEnums.FeatureCapabilityMode.Hidden || eMode == WebEnums.FeatureCapabilityMode.Disable)
         {
             foreach (GridColumn col in rgReconciliationStatusByFSCaption.MasterTableView.Columns)
             {
                 if (col.UniqueName == "ApprovedData" || col.UniqueName == "PendingModificationReviewerData" 
                     || col.UniqueName == "PendingApprovalData" || col.UniqueName == "PendingApproval" 
                     || col.UniqueName == "PendingModificationReviewer" || col.UniqueName == "Approved")
                 {
                     col.Visible = false;
                 }
             }
         }

    }




}
