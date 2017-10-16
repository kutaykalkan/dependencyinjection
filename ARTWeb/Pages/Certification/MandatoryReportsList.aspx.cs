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
using SkyStem.Library.Controls.WebControls;
using SkyStem.ART.Client.Model;
using System.Collections.Generic;
using SkyStem.ART.Client.IServices;
using SkyStem.ART.Web.Data;
using SkyStem.ART.Web.Classes;
using System.Text;

public partial class Pages_MandatoryReportsList : PageBaseRecPeriod
{
    List<ReportMstInfo> oReportMstInfoCollection;

    protected void Page_Init(object sender, EventArgs e)
    {
        MasterPageBase ompage = (MasterPageBase)this.Master.Master;
        ompage.ReconciliationPeriodChangedEventHandler += new EventHandler(ompage_ReconciliationPeriodChangedEventHandler);

    }
    protected void Page_Load(object sender, EventArgs e)
    {
        Helper.SetPageTitle(this, 1016);
        if( ! Page.IsPostBack )
           LoadData();
      
    }
    private void LoadData()
    {
        bool isShowContent = Helper.ShowHideContentOnCertificationPages(this, WebEnums.ARTPages.MandatoryReportsList);
        if (isShowContent == true)
        {
            //int? companyID = 0;
            short? roleID = 0;
            int? userID = 0;
            int? recPeriodID = 0;
            //companyID = SessionHelper.CurrentCompanyID;
            roleID = SessionHelper.CurrentRoleID;
            userID = SessionHelper.CurrentUserID;
            recPeriodID = SessionHelper.CurrentReconciliationPeriodID;
            IReport oReport = RemotingHelper.GetReportObject();
            oReportMstInfoCollection = oReport.SelectMandatoryReportByRoleID(roleID, userID, recPeriodID, Helper.GetAppUserInfo());
            List<ReportMstInfo> oReprotTypeList = new List<ReportMstInfo>();

            if (oReportMstInfoCollection.Count > 0)
            {
                oReprotTypeList.Add(oReportMstInfoCollection[0]);
                for (int i = 1; i <= oReportMstInfoCollection.Count - 1; i++)
                {
                    if (oReportMstInfoCollection[i - 1].ReportTypeLabelID != oReportMstInfoCollection[i].ReportTypeLabelID)
                        oReprotTypeList.Add(oReportMstInfoCollection[i]);
                }
                rptMandatoryReportGroup.DataSource = oReprotTypeList;
                rptMandatoryReportGroup.DataBind();
            }
            else
            {
                rptMandatoryReportGroup.DataSource = null;
                rptMandatoryReportGroup.DataBind();
            }
        }

        Helper.ShowExportToolbarOnCertificationPages(this, false);
    
    }


    protected void rptMandatoryReportGroup_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        try
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                ReportMstInfo oReportMstInfo = (ReportMstInfo)e.Item.DataItem;
                ExLabel lblReportGroup = (ExLabel)e.Item.FindControl("lblReportGroup");
                lblReportGroup.LabelID = oReportMstInfo.ReportTypeLabelID.Value;
                Repeater rptReport = (Repeater)e.Item.FindControl("rptMandatoryReport");
                rptReport.ItemDataBound += rptStandardReport_ItemDataBound;
                rptReport.DataSource = getAllRepotsByReportType(oReportMstInfo.ReportTypeLabelID.Value);
                rptReport.DataBind();
            }
        }
        catch (Exception ex)
        {
            Helper.ShowErrorMessage((PageBase)this.Page, ex);
        }
    }
    private List<ReportMstInfo> getAllRepotsByReportType(int ReportTypeLabelID)
    {
        List<ReportMstInfo> oReprotTypeList = new List<ReportMstInfo>();
        for (int i = 0; i <= oReportMstInfoCollection.Count - 1; i++)
        {
            if (oReportMstInfoCollection[i].ReportTypeLabelID == ReportTypeLabelID)
                oReprotTypeList.Add(oReportMstInfoCollection[i]);
        }
        return oReprotTypeList;
    }



    protected void SendToReport(object sender, CommandEventArgs e)
    {
        if (e.CommandName.Equals("SendToReportCommand"))
        {
            DateTime dt = new DateTime();
            string[] commandArgsArr = e.CommandArgument.ToString().Split('~');
            short? reportID = Convert.ToInt16(commandArgsArr[0]);
            ReportMstInfo oReportInfo = ReportHelper.GetReportInfoByReportID(reportID, SessionHelper.GetUserLanguage()
                                             , AppSettingHelper.GetDefaultBusinessEntityID(), AppSettingHelper.GetDefaultLanguageID());
            Session[SessionConstants.REPORT_INFO_OBJECT] = oReportInfo;
            string url = oReportInfo.ReportUrl;
            url = ReportHelper.AddCommonQueryStringParameter(url);
            if (DateTime.TryParse(commandArgsArr[2].ToString(), out dt))
            {
                oReportInfo.SignOffDate = dt;
                url = url + "&" + QueryStringConstants.REPORT_TYPE + "=" + ((short)WebEnums.ReportType.MandatoryReportSignedOff).ToString();
            }
            else
                url = url + "&" + QueryStringConstants.REPORT_TYPE + "=" + ((short)WebEnums.ReportType.MandatoryReport).ToString();
            url = url + "&" + QueryStringConstants.MANDATORY_REPORT_ID + "=" + commandArgsArr[1];

            //Response.Redirect(url);
            SessionHelper.RedirectToUrl(url);
            return;
        }
    }

    public void rptStandardReport_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        try
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                string delimeter = "~";
                StringBuilder oSbArgs = new StringBuilder();
                string signOffDate = "";
                ReportMstInfo oReportMstInfo = (ReportMstInfo)e.Item.DataItem;
                ExLinkButton lbtnReportName = (ExLinkButton)e.Item.FindControl("lbtnReportName");
                lbtnReportName.LabelID = oReportMstInfo.ReportLabelID.Value;
                System.DateTime? SignOffDate = null;
                SignOffDate = oReportMstInfo.SignOffDate;
                ExLabel lblYN = (ExLabel)e.Item.FindControl("lblYN");
                ExLabel lblSignOffDate = (ExLabel)e.Item.FindControl("lblSignOffDate");
                if (SignOffDate == null)
                {
                    lblYN.Text = Helper.GetDisplayStringValue(null);
                    lblSignOffDate.Text = Helper.GetDisplayStringValue(null); 
                    signOffDate = "";
                }
                else
                {
                    lblYN.Text = ReportHelper.SetYesNoCodeBasedOnBool(true); ; 
                    lblSignOffDate.Text = Helper.GetDisplayDateTime(SignOffDate);
                    signOffDate = Helper.GetDisplayDate(SignOffDate);

                }
                ExLabel lblReportDescription = (ExLabel)e.Item.FindControl("lblReportDescription");
                lblReportDescription.LabelID = oReportMstInfo.DescriptionLabelID.Value;
                oSbArgs.Append(oReportMstInfo.ReportID.Value.ToString());
                oSbArgs.Append(delimeter);
                oSbArgs.Append(oReportMstInfo.ReportRoleMandatoryReportID.Value.ToString());
                oSbArgs.Append(delimeter);
                oSbArgs.Append(signOffDate);
                lbtnReportName.CommandArgument = oSbArgs.ToString();
                //lbtnReportName.CommandArgument = oReportMstInfo.ReportID.Value.ToString() + delimeter + oReportMstInfo.ReportRoleMandatoryReportID.Value.ToString() + delimeter+ ;
            }
        }
        catch (Exception ex)
        {
            Helper.ShowErrorMessage((PageBase)this.Page, ex);
        }
    }
    protected void Page_PreRender(object sender, EventArgs e)
    {
        Helper.SetBreadcrumbs(this, 1072, 1464, 1016);
    }
    public override string GetMenuKey()
    {
        return "CertificationStatus";
    }

    protected void ReportChanged(object sender, EventArgs e)
    {
        //string reportID = Convert.ToInt32(e).ToString();
        //Response.Redirect("~/Pages/MandatoryReportSignOff.aspx?ReportID=" + reportID);
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
    }

    protected void ompage_ReconciliationPeriodChangedEventHandler(object sender, EventArgs e)
    {
        if (IsPostBack)
        {
            LoadData();
        }
    }

}//end of class
