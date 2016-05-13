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

public partial class Pages_ReportActivity : PageBaseCompany
{
    short? _reportID = 0;
   string _reportSectionIDFromURL = "";
    //string parametersString_REPORT = "<br/>Currency: Reporting Currency <br/>ISKeyAccount: Yes <br/>RiskRating: High<br/> Period: 2009-10-01<br/> IsMaterial: Yes<br/> Reason: <br/> Entity: <br/> Account: 0051000000 , 0101000000 , 0103000000 , 4221000000 ";

    protected void Page_Load(object sender, EventArgs e)
    {


        Helper.SetPageTitle(this, 1616);
        ReportHelper.SetReportsBreadCrumb(this);
        rgMain.ClientSettings.Selecting.AllowRowSelect = true;

        if (Request.QueryString[QueryStringConstants.REPORT_ID] != null)
            _reportID = Convert.ToInt16(Request.QueryString[QueryStringConstants.REPORT_ID]);

        if (Request.QueryString[QueryStringConstants.REPORT_SECTION_ID] != null)
        {
            _reportSectionIDFromURL = Request.QueryString[QueryStringConstants.REPORT_SECTION_ID];
        }
        if (!this.IsPostBack)
        {
            this.BindDataToGrid(_reportID.Value);
        }



        //*******Show Page Header Labels : By Prafull, 04-Mar-2011 
        ReportMstInfo oRptMstInfo = ReportHelper.GetReportInfoByReportID(_reportID, SessionHelper.GetUserLanguage(), AppSettingHelper.GetDefaultBusinessEntityID(), AppSettingHelper.GetDefaultLanguageID());
        PopulateReportHeader(oRptMstInfo);



        //IReport oReportClient = RemotingHelper.GetReportObject();
        //List<ReportActivityInfo> oReportActivityInfoCollection = oReportClient.GetReportActivityData(_reportID);
        //rgMain.DataSource = oReportActivityInfoCollection;
        //rgMain.DataBind();
    }

    protected void rgMain_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {
        if (e.Item.ItemType == Telerik.Web.UI.GridItemType.Item ||
            e.Item.ItemType == Telerik.Web.UI.GridItemType.AlternatingItem)
        {
            //ReportActivityInfo oReportActivityInfo = (ReportActivityInfo)e.Item.DataItem;
            ReportArchiveInfo oRptArchiveInfo = (ReportArchiveInfo)e.Item.DataItem;
            string commandArgs = e.Item.ItemIndex.ToString(); //oRptArchiveInfo.ReportArchiveID.Value.ToString();

            ExLinkButton lnkbtnArchiveDate = (ExLinkButton)e.Item.FindControl("lnkbtnArchiveDate");
            ExLinkButton lnkbtnActionPerformed = (ExLinkButton)e.Item.FindControl("lnkbtnActionPerformed");
            ExLinkButton lnkbtnComments = (ExLinkButton)e.Item.FindControl("lnkbtnComments");
            ParameterViewer pv = (ParameterViewer)e.Item.FindControl("ucParamViewer");

            lnkbtnArchiveDate.Text = Helper.GetDisplayDateTime(oRptArchiveInfo.ReportCreateDateTime);
            lnkbtnArchiveDate.CommandArgument = commandArgs;
            
            lnkbtnActionPerformed.Text =oRptArchiveInfo.ArchiveType;
            lnkbtnActionPerformed.CommandArgument = commandArgs;

            lnkbtnComments.Text = oRptArchiveInfo.Comments;
            lnkbtnComments.CommandArgument = commandArgs;

            pv.ParamDataTable = ReportHelper.GetDataTableForParamViewer(oRptArchiveInfo);
            //pv.SavedReportID = oRptArchiveInfo.ReportArchiveID.Value.ToString();
            //pv.SavedReportName = "";
            pv.CommandArgs = commandArgs;
         //   string url = oRptArchiveInfo.ReportUrl + "?" + QueryStringConstants.REPORT_ID + "=" + _reportID;
        }
    }
    protected void linkParamClick(object sender, CommandEventArgs e)
    {
       this.ShowReport (Convert.ToInt32 (e.CommandArgument.ToString ()));
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
    private void BindDataToGrid(short reportID)
    {
        IReportArchive oReportArchiveClient = RemotingHelper.GetReportArchiveObject();
        List<ReportArchiveInfo> oRptArchiveInfo = oReportArchiveClient.GetRptActivityByReportIDUserIDRoleIDRecPeriodID(
            reportID
            , SessionHelper.CurrentUserID.Value
            , SessionHelper.CurrentRoleID.Value
            , SessionHelper.CurrentReconciliationPeriodID.Value
            , SessionHelper.GetUserLanguage()
            , AppSettingHelper.GetDefaultLanguageID()
            , SessionHelper.GetBusinessEntityID()
            , Helper.GetAppUserInfo());
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

            //Set session for report into to show report header on master page
            ReportMstInfo oRptMstInfo = ReportHelper.GetReportInfoByReportID(_reportID, SessionHelper.GetUserLanguage(), AppSettingHelper.GetDefaultBusinessEntityID(), AppSettingHelper.GetDefaultLanguageID());
            ReportArchiveInfo oRptArchiveInfo = ReportHelper.GetReportArchivedDataByReportArchiveID(rptId);
            Dictionary<string, string> oRptcriteria = ReportHelper.GetReportCriteriaFromParamInfoCollection(ArchiveRptParamCollection);

            oRptMstInfo.DateAdded = oRptArchiveInfo.ReportCreateDateTime;
            Session[SessionConstants.REPORT_INFO_OBJECT] = oRptMstInfo;
            Session[SessionConstants.REPORT_ARCHIVED_DATA] = oRptArchiveInfo;
            Session[SessionConstants.REPORT_CRITERIA] = oRptcriteria;
            
            reportUrl = ReportHelper.AddCommonQueryStringParameter(reportUrl);
            reportUrl = reportUrl + "&" + QueryStringConstants.REPORT_TYPE + "=" + ((short)WebEnums.ReportType.ArchivedReport).ToString () ;
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


    private void PopulateReportHeader(ReportMstInfo oReportInfo)
    {
        this.lblReportName.LabelID = oReportInfo.ReportLabelID.Value;
        this.lblCompanyName.Text = Helper.GetCompanyName();
        this.lblReportDescription.LabelID= oReportInfo.DescriptionLabelID.Value;
    }





    protected void rgMain_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
    {
        IReportArchive oReportArchiveClient = RemotingHelper.GetReportArchiveObject();
        List<ReportArchiveInfo> oRptArchiveInfo = oReportArchiveClient.GetRptActivityByReportIDUserIDRoleIDRecPeriodID(
            _reportID.Value
            , SessionHelper.CurrentUserID.Value
            , SessionHelper.CurrentRoleID.Value
            , SessionHelper.CurrentReconciliationPeriodID.Value
            , SessionHelper.GetUserLanguage()
            , AppSettingHelper.GetDefaultLanguageID()
            , SessionHelper.GetBusinessEntityID()
            , Helper.GetAppUserInfo());
        this.rgMain.DataSource = oRptArchiveInfo;
    }
}//end of class
