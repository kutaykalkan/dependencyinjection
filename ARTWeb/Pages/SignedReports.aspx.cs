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
using SkyStem.ART.Web.Classes;
using SkyStem.ART.Web.Utility;
using SkyStem.ART.Web.Data;
using System.Collections.Generic;
using SkyStem.ART.Client.Model;
using SkyStem.ART.Client.IServices;
using SkyStem.Library.Controls.WebControls;
using Telerik.Web.UI;
using SkyStem.ART.Client.Exception;
using SkyStem.Library.Controls.TelerikWebControls.Data;

public partial class Pages_SignedReports : PageBaseRecPeriod
{
    short? _reportID = 0;
    string _reportSectionIDFromURL = "";
    bool isExportPDF;
    bool isExportExcel;
    const short MenuIDForSignedReoprt = 68;
    protected void Page_Init(object sender, EventArgs e)
    {

        MasterPageBase ompage = (MasterPageBase)this.Master;
        ompage.ReconciliationPeriodChangedEventHandler += new EventHandler(ompage_ReconciliationPeriodChangedEventHandler);


    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //List<MenuMstInfo> oAllMenuMstInfoCollection = SessionHelper.GetUserMenu();
        MenuMstInfo oMenuMstInfo = SessionHelper.GetUserMenu().Find(c => c.MenuID == MenuIDForSignedReoprt);
        if (oMenuMstInfo == null)
            Helper.RedirectToHomePage();
        Helper.SetPageTitle(this, 2805);
        Helper.SetBreadcrumbs(this, 1073, 2805);
        rgMain.ClientSettings.Selecting.AllowRowSelect = true;
        if (!this.IsPostBack)
        {
            isExportPDF = false;
            isExportExcel = false;
            this.BindDataToGrid();
        }
    }

    protected void ompage_ReconciliationPeriodChangedEventHandler(object sender, EventArgs e)
    {
        try
        {
            this.BindDataToGrid();
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

    protected void rgMain_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {
        if (e.Item.ItemType == Telerik.Web.UI.GridItemType.Item ||
            e.Item.ItemType == Telerik.Web.UI.GridItemType.AlternatingItem)
        {
            ReportArchiveInfo oRptArchiveInfo = (ReportArchiveInfo)e.Item.DataItem;
            string commandArgs = e.Item.ItemIndex.ToString();
            ExLinkButton lnkbtnArchiveDate = (ExLinkButton)e.Item.FindControl("lnkbtnArchiveDate");
            ExLinkButton lnkbtnActionPerformed = (ExLinkButton)e.Item.FindControl("lnkbtnActionPerformed");
            ExLinkButton lnkbtnComments = (ExLinkButton)e.Item.FindControl("lnkbtnComments");
            ExLinkButton lnkbtnReportName = (ExLinkButton)e.Item.FindControl("lnkbtnReportName");
            ExLinkButton lnkbtnSignedBy = (ExLinkButton)e.Item.FindControl("lnkbtnSignedBy");

            ParameterViewer pv = (ParameterViewer)e.Item.FindControl("ucParamViewer");

            ExLabel lblArchiveDate = (ExLabel)e.Item.FindControl("lblArchiveDate");
            ExLabel lblReportName = (ExLabel)e.Item.FindControl("lblReportName");
            ExLabel lblSignedBy = (ExLabel)e.Item.FindControl("lblSignedBy");
            ExLabel lblActionPerformed = (ExLabel)e.Item.FindControl("lblActionPerformed");
            ExLabel lblComments = (ExLabel)e.Item.FindControl("lblComments");
            ExLabel lblParamViewer = (ExLabel)e.Item.FindControl("lblParamViewer");

            lnkbtnReportName.Text = Helper.GetDisplayStringValue(oRptArchiveInfo.ReportName);
            lnkbtnReportName.CommandArgument = commandArgs;
            lblSignedBy.Text = lnkbtnReportName.Text;

            lnkbtnSignedBy.Text = Helper.GetDisplayStringValue(oRptArchiveInfo.SignedBy);
            lnkbtnSignedBy.CommandArgument = commandArgs;
            lblReportName.Text = lnkbtnSignedBy.Text;

            lnkbtnArchiveDate.Text = Helper.GetDisplayDateTime(oRptArchiveInfo.ReportCreateDateTime);
            lnkbtnArchiveDate.CommandArgument = commandArgs;
            lblArchiveDate.Text = lnkbtnArchiveDate.Text;

            lnkbtnActionPerformed.Text = Helper.GetDisplayStringValue(oRptArchiveInfo.ArchiveType);
            lnkbtnActionPerformed.CommandArgument = commandArgs;
            lblActionPerformed.Text = lnkbtnActionPerformed.Text;

            lnkbtnComments.Text = Helper.GetDisplayStringValue(oRptArchiveInfo.Comments);
            lnkbtnComments.CommandArgument = commandArgs;
            lblComments.Text = lnkbtnComments.Text;

            pv.ParamDataTable = ReportHelper.GetDataTableForParamViewer(oRptArchiveInfo);
            lblParamViewer.Text = pv.ParamDataTableHtml;
            pv.CommandArgs = commandArgs;
        }
    }
    protected void linkParamClick(object sender, CommandEventArgs e)
    {
        this.ShowReport(Convert.ToInt32(e.CommandArgument.ToString()));
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        string url = "ReportHome.aspx?" + QueryStringConstants.REPORT_SECTION_ID + "=" + _reportSectionIDFromURL;
        Response.Redirect(url);
    }
    protected void LinkButton_Command(Object sender, CommandEventArgs e)
    {
        this.ShowReport(Convert.ToInt32(e.CommandArgument.ToString()));
    }
    public override string GetMenuKey()
    {
        return "";
    }
    private void BindDataToGrid()
    {
        List<ReportArchiveInfo> oRptArchiveInfo = GetReportsActivity();
        this.rgMain.DataSource = oRptArchiveInfo;
        rgMain.DataBind();
    }

    private void ShowReport(int itemIndex)
    {
        try
        {
            GridDataItem item = rgMain.MasterTableView.Items[itemIndex];
            IList<ReportArchiveParameterInfo> ArchiveRptParamCollection = (IList<ReportArchiveParameterInfo>)item.GetDataKeyValue("ReportArchiveParameterByRptArchiveID");

            int rptId = Convert.ToInt32(item.GetDataKeyValue("ReportArchiveID").ToString());
            string reportUrl = item.GetDataKeyValue("ReportUrl").ToString();
            _reportID = Convert.ToInt16(item.GetDataKeyValue("ReportID").ToString());
            ReportMstInfo oRptMstInfo = ReportHelper.GetReportInfoByReportID(_reportID, SessionHelper.GetUserLanguage(), AppSettingHelper.GetDefaultBusinessEntityID(), AppSettingHelper.GetDefaultLanguageID());
            ReportArchiveInfo oRptArchiveInfo = ReportHelper.GetReportArchivedDataByReportArchiveID(rptId);
            Dictionary<string, string> oRptcriteria = ReportHelper.GetReportCriteriaFromParamInfoCollection(ArchiveRptParamCollection);

            oRptMstInfo.DateAdded = oRptArchiveInfo.ReportCreateDateTime;
            Session[SessionConstants.REPORT_INFO_OBJECT] = oRptMstInfo;
            Session[SessionConstants.REPORT_ARCHIVED_DATA] = oRptArchiveInfo;
            Session[SessionConstants.REPORT_CRITERIA] = oRptcriteria;

            reportUrl = ReportHelper.AddCommonQueryStringParameter(reportUrl);
            reportUrl = reportUrl + "&" + QueryStringConstants.REPORT_TYPE + "=" + ((short)WebEnums.ReportType.ArchivedReport).ToString();
            reportUrl = reportUrl + "&" + QueryStringConstants.REPORT_SECTION_ID + "=" + Request.QueryString[QueryStringConstants.REPORT_SECTION_ID];
            reportUrl = reportUrl + "&" + QueryStringConstants.IS_REPORT_ACTIVITY + "=1";
            Response.Redirect(reportUrl);
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

    protected void rgMain_ItemCreated(object sender, GridItemEventArgs e)
    {
        GridHelper.SetStylesForExportGrid(e, isExportPDF, isExportExcel);
    }
    protected void rgMain_OnItemCommand(object sender, GridCommandEventArgs e)
    {
        if (e.CommandName == TelerikConstants.GridExportToPDFCommandName)
        {
            isExportPDF = true;
            EnableColumnsForImport(true);
            GridHelper.ExportGridToPDF(rgMain, 2805);

        }
        if (e.CommandName == TelerikConstants.GridExportToExcelCommandName)
        {
            isExportExcel = true;
            EnableColumnsForImport(true);
            GridHelper.ExportGridToExcel(rgMain, 2805);
        }

    }
    protected void rgMain_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    {
        try
        {

            List<ReportArchiveInfo> oRptArchiveInfo = GetReportsActivity();
            this.rgMain.DataSource = oRptArchiveInfo;
            // rgMain.DataBind();
        }
        catch (Exception ex)
        {
            Helper.ShowErrorMessage(this, ex);
        }
    }

    private List<ReportArchiveInfo> GetReportsActivity()
    {
        List<ReportArchiveInfo> oRptArchiveInfoList = null;
        if (SessionHelper.CurrentReconciliationPeriodID.HasValue && SessionHelper.CurrentRoleID.HasValue)
        {
            IReportArchive oReportArchiveClient = RemotingHelper.GetReportArchiveObject();
            oRptArchiveInfoList = oReportArchiveClient.GetReportsActivity(
                 SessionHelper.CurrentReconciliationPeriodID.Value
                , SessionHelper.GetUserLanguage()
                , AppSettingHelper.GetDefaultLanguageID()
                , SessionHelper.GetBusinessEntityID()
                , SessionHelper.CurrentRoleID.Value
                , Helper.GetAppUserInfo());
        }
        return oRptArchiveInfoList;
    }

    private void EnableColumnsForImport(bool flag)
    {
        rgMain.MasterTableView.Columns.FindByUniqueName("ArchiveDate").Visible = !flag;
        rgMain.MasterTableView.Columns.FindByUniqueName("ArchiveDateForExport").Visible = flag;
        rgMain.MasterTableView.Columns.FindByUniqueName("ReportName").Visible = !flag;
        rgMain.MasterTableView.Columns.FindByUniqueName("ReportNameForExport").Visible = flag;
        rgMain.MasterTableView.Columns.FindByUniqueName("SignedBy").Visible = !flag;
        rgMain.MasterTableView.Columns.FindByUniqueName("SignedByForExport").Visible = flag;
        rgMain.MasterTableView.Columns.FindByUniqueName("ActionPerformed").Visible = !flag;
        rgMain.MasterTableView.Columns.FindByUniqueName("ActionPerformedForExport").Visible = flag;
        rgMain.MasterTableView.Columns.FindByUniqueName("Comments").Visible = !flag;
        rgMain.MasterTableView.Columns.FindByUniqueName("CommentsForExport").Visible = flag;
        rgMain.MasterTableView.Columns.FindByUniqueName("ParamViewer").Visible = !flag;
        rgMain.MasterTableView.Columns.FindByUniqueName("ParamViewerForExport").Visible = flag;

    }





}//end of class
