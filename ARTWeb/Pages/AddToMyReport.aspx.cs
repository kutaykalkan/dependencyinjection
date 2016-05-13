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
using SkyStem.ART.Client.Model;
using SkyStem.ART.Web.Data;
using System.Collections.Generic;
using SkyStem.ART.Client.IServices;

public partial class Pages_AddToMyReport : PopupPageBase
{
    #region Variables & Constants
    #endregion

    #region Properties
    #endregion

    #region Delegates & Events
    #endregion

    #region Page Events
    protected void Page_Load(object sender, EventArgs e)
    {
        PopupHelper.SetPageTitle(this, 1850);
        if (!Page.IsPostBack)
        {
            ReportMstInfo oReportInfo;
            oReportInfo = (ReportMstInfo)Session[SessionConstants.REPORT_INFO_OBJECT];

            IReport oReportClient = RemotingHelper.GetReportObject();
            int noOfSavedMyReport = oReportClient.NoOfSavedMyReportByReportID(oReportInfo.ReportID, Helper.GetAppUserInfo());
            noOfSavedMyReport = noOfSavedMyReport + 1;

            this.txtName.Text = oReportInfo.Report + "_" + noOfSavedMyReport;

            this.lblReportName.LabelID = oReportInfo.ReportLabelID.Value;
        }

    }
    #endregion

    #region Grid Events
    #endregion

    #region Other Events
    protected void btnOk_Click(object sender, EventArgs e)
    {
        ReportMstInfo oReportInfo;
        oReportInfo = (ReportMstInfo)Session[SessionConstants.REPORT_INFO_OBJECT];

        Dictionary<string, string> oRptCriteriaCollection = null;
        if (Session[SessionConstants.REPORT_CRITERIA] != null)
        {
            oRptCriteriaCollection = (Dictionary<string, string>)Session[SessionConstants.REPORT_CRITERIA];

        }



        List<UserMyReportSavedReportParameterInfo> oUserMyReportSavedReportParameterInfoCollection = new List<UserMyReportSavedReportParameterInfo>();
        foreach (KeyValuePair<string, string> kvp in oRptCriteriaCollection)
        {
            string keyName = kvp.Key;
            string keyValue = kvp.Value;
            short rptParamKeyId = ReportHelper.GetRptParamKeyIDForParamKey(keyName);
            UserMyReportSavedReportParameterInfo oUserMyReportSavedReportParameterInfo = new UserMyReportSavedReportParameterInfo();
            oUserMyReportSavedReportParameterInfo.ReportParameterID = rptParamKeyId;
            oUserMyReportSavedReportParameterInfo.ParameterValue = keyValue;
            oUserMyReportSavedReportParameterInfoCollection.Add(oUserMyReportSavedReportParameterInfo);
        }

        IReport oReportClient = RemotingHelper.GetReportObject();
        oReportClient.InsertMyReport(SessionHelper.CurrentRoleID, SessionHelper.CurrentUserID, oReportInfo, this.txtName.Text, oUserMyReportSavedReportParameterInfoCollection, Helper.GetAppUserInfo());

        this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "return ClosePopupWithoutRefreshParentPage", ScriptHelper.GetJSForPopupClose());



    }
    #endregion

    #region Validation Control Events
    #endregion

    #region Private Methods
    #endregion

    #region Other Methods
    #endregion

}
