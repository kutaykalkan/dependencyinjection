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
using SkyStem.ART.Web.Classes;
using SkyStem.ART.Web.Data;
using SkyStem.ART.Client.Model;
using SkyStem.Library.Controls.WebControls;
using SkyStem.ART.Client.IServices;
using System.Collections.Generic;
using Telerik.Web.UI;

public partial class Pages_ReportSaved : PageBaseCompany
{
    short? _reportID = 0;
    string _reportSectionIDMyReport = "2";
    string _reportSectionIDFromURL = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        Helper.SetPageTitle(this, 1609);
        Helper.SetBreadcrumbs(this, 1073, 1077, 1609);
        rgMain.ClientSettings.Selecting.AllowRowSelect = true;
        if (!IsPostBack)
        {
            BindRadGrid();
        }
    }


    protected void rgMain_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {
        if (e.Item.ItemType == Telerik.Web.UI.GridItemType.Item ||
            e.Item.ItemType == Telerik.Web.UI.GridItemType.AlternatingItem)
        {
            UserMyReportSavedReportInfo oReportSavedInfo = (UserMyReportSavedReportInfo)e.Item.DataItem;

            ExLinkButton lbtnReportName = (ExLinkButton)e.Item.FindControl("lbtnReportName");
            ExLinkButton lbtnSavedReportName = (ExLinkButton)e.Item.FindControl("lbtnSavedReportName");
            ExLinkButton lbtnParameter = (ExLinkButton)e.Item.FindControl("lbtnParameter");
            ExLinkButton lbtnDateSaved = (ExLinkButton)e.Item.FindControl("lbtnDateSaved");
            ExLinkButton lbtnSavedBy = (ExLinkButton)e.Item.FindControl("lbtnSavedBy");

            ParameterViewer pv = (ParameterViewer)e.Item.FindControl("ucParamViewer");
            pv.ParamDataTable = ReportHelper.GetDataTableForSavedRptParamViewer(oReportSavedInfo.UserMyReportSavedReportParameterByUserMyReportSavedReportID);
            pv.CommandArgs = e.Item.ItemIndex.ToString();
            lbtnReportName.Text = oReportSavedInfo.ReportName;
            lbtnSavedReportName.Text = oReportSavedInfo.UserMyReportSavedReportName;
            lbtnDateSaved.Text = oReportSavedInfo.DateAdded.ToString();
            IUser oIUser = RemotingHelper.GetUserObject();
            UserHdrInfo oUserHdrInfo = oIUser.GetUserDetail(oReportSavedInfo.UserID, Helper.GetAppUserInfo());
            lbtnSavedBy.Text = oUserHdrInfo.FirstName + "  " + oUserHdrInfo.LastName;
            lbtnReportName.CommandArgument = e.Item.ItemIndex.ToString();
            lbtnSavedReportName.CommandArgument = e.Item.ItemIndex.ToString();
            lbtnDateSaved.CommandArgument = e.Item.ItemIndex.ToString();
            lbtnSavedBy.CommandArgument = e.Item.ItemIndex.ToString();

        }
    }


    protected void SendToReport(object sender, CommandEventArgs e)
    {
        if (e.CommandName.Equals("SendToReportCommand"))
        {
            int itemIndex = Convert.ToInt32(e.CommandArgument.ToString());
            GridDataItem item = rgMain.MasterTableView.Items[itemIndex];
            string savedReportID = item.GetDataKeyValue("UserMyReportSavedReportID").ToString();
            string savedReportName = item.GetDataKeyValue("UserMyReportSavedReportName").ToString();
            string savedReportDateAdded = item.GetDataKeyValue("DateAdded").ToString();
            SendToRunReport(savedReportID, savedReportName, savedReportDateAdded);

        }

    }

    private void CreateAndSaveRptParameterDictionayInSession(List<UserMyReportSavedReportParameterInfo> oSavedReportParameterInfoCollection)
    {
        Dictionary<string, string> oReportCriteria = new Dictionary<string, string>();
        for (int i = 0; i < oSavedReportParameterInfoCollection.Count; i++)
        {


            if ((short)WebEnums.ReportParameterKeyMst.Period == (short)oSavedReportParameterInfoCollection[i].ReportParameterID)
            {
                if (oSavedReportParameterInfoCollection[i].ParameterValue == WebConstants.CURRENT_REC_PERIOD_INDEX)
                {
                    oReportCriteria.Add(ReportHelper.GetRptParamKeyNameForParamKeyID((short)oSavedReportParameterInfoCollection[i].ReportParameterID), WebConstants.CURRENT_REC_PERIOD_INDEX);
                }
                else
                {
                    oReportCriteria.Add(ReportHelper.GetRptParamKeyNameForParamKeyID((short)oSavedReportParameterInfoCollection[i].ReportParameterID), SessionHelper.CurrentReconciliationPeriodID.ToString());
                }
            }
            else
            {
                oReportCriteria.Add(ReportHelper.GetRptParamKeyNameForParamKeyID((short)oSavedReportParameterInfoCollection[i].ReportParameterID), oSavedReportParameterInfoCollection[i].ParameterValue);
            }

        }
        Session[SessionConstants.REPORT_CRITERIA] = oReportCriteria;
    }

    private void BindRadGrid()
    {

        if (Request.QueryString[QueryStringConstants.REPORT_ID] != null)
            _reportID = Convert.ToInt16(Request.QueryString[QueryStringConstants.REPORT_ID]);
        IReport oReportClient = RemotingHelper.GetReportObject();
        List<UserMyReportSavedReportInfo> oUserMyReportSavedReportInfoCollection = oReportClient.GetSavedReportData(
             SessionHelper.CurrentRoleID
            , SessionHelper.CurrentUserID
            , _reportID
            , SessionHelper.GetUserLanguage()
            , AppSettingHelper.GetDefaultLanguageID()
            , SessionHelper.GetBusinessEntityID()
            , Helper.GetAppUserInfo()
            );
        rgMain.DataSource = oUserMyReportSavedReportInfoCollection;
        rgMain.DataBind();


    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        List<Int64> oUserMyReportIDCollection = new List<long>();
        foreach (GridDataItem item in rgMain.SelectedItems)
        {
            long UserMyReportID = Convert.ToInt64(item.GetDataKeyValue("UserMyReportID"));
            oUserMyReportIDCollection.Add(UserMyReportID);
        }
        IReport oReportClient = RemotingHelper.GetReportObject();
        bool result = oReportClient.DeleteSavedReportData(oUserMyReportIDCollection, Helper.GetAppUserInfo());

        BindRadGrid();
        rgMain.Rebind();
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        //as always coming from MyReport Section
        _reportSectionIDFromURL = _reportSectionIDMyReport;
        string url = "ReportHome.aspx?" + QueryStringConstants.REPORT_SECTION_ID + "=" + _reportSectionIDFromURL;
        //Response.Redirect(url);
        SessionHelper.RedirectToUrl(url);
        return;
    }

    public override string GetMenuKey()
    {
        return "";
    }

    void SendToRunReport(string savedReportID, string savedReportName, string savedReportDateAdded)
    {
        if (Request.QueryString[QueryStringConstants.REPORT_ID] != null)
            _reportID = Convert.ToInt16(Request.QueryString[QueryStringConstants.REPORT_ID]);
        ReportMstInfo oReportInfo = ReportHelper.GetReportInfoByReportID(_reportID, SessionHelper.GetUserLanguage()
                                                 , AppSettingHelper.GetDefaultBusinessEntityID(), AppSettingHelper.GetDefaultLanguageID());
        oReportInfo.DateAdded = Convert.ToDateTime(savedReportDateAdded);
        Session[SessionConstants.REPORT_INFO_OBJECT] = oReportInfo;

        Session[SessionConstants.USER_MY_REPORT_SAVED_REPORT_NAME] = savedReportName;
        IReport oReportClient = RemotingHelper.GetReportObject();
        List<UserMyReportSavedReportParameterInfo> oUserMyReportSavedReportParameterInfoCollection = new List<UserMyReportSavedReportParameterInfo>();
        oUserMyReportSavedReportParameterInfoCollection = (List<UserMyReportSavedReportParameterInfo>)oReportClient.GetAllParametersByMySavedReportID(Convert.ToInt32(savedReportID), Helper.GetAppUserInfo());
        CreateAndSaveRptParameterDictionayInSession(oUserMyReportSavedReportParameterInfoCollection);
        string url = string.Empty;
        //if (_reportID == 2)
        //{
        //    url = oReportInfo.ReportUrl.Replace("OpenItemsReport", "OpenItemReportForCurrentRecPeriod");
        //}
        //else
        //{
            url = oReportInfo.ReportUrl;
        //}
        url = ReportHelper.AddCommonQueryStringParameter(url);
        url = url + "&" + QueryStringConstants.MY_REPORT_ID + "=1";
        url = url + "&" + QueryStringConstants.REPORT_TYPE + "=" + ((short)WebEnums.ReportType.UserSavedReport).ToString();
        url = url + "&" + QueryStringConstants.REPORT_SECTION_ID + "=" + Request.QueryString[QueryStringConstants.REPORT_SECTION_ID];
        //Response.Redirect(url);
        SessionHelper.RedirectToUrl(url);
        return;
    }


    protected void linkParamClick(object sender, CommandEventArgs e)
    {
        int itemIndex = Convert.ToInt32(e.CommandArgument.ToString());
        GridDataItem item = rgMain.MasterTableView.Items[itemIndex];
        string savedReportID = item.GetDataKeyValue("UserMyReportSavedReportID").ToString();
        string savedReportName = item.GetDataKeyValue("UserMyReportSavedReportName").ToString();
        string savedReportDateAdded = item.GetDataKeyValue("DateAdded").ToString();
        SendToRunReport(savedReportID, savedReportName, savedReportDateAdded);

    }
}//end of class

