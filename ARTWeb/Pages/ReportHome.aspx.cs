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
using Telerik.Web.UI;
using SkyStem.Library.Controls.WebControls;
using SkyStem.ART.Client.Model;
using System.Collections.Generic;
using SkyStem.ART.Client.IServices;
using SkyStem.Language.LanguageUtility;

public partial class Pages_ReportHome : PageBaseRecPeriod
{
    string _reportSectionIDStandardReport = "1";
    string _reportSectionIDMyReport = "2";

    string _reportSectionID = "";
    List<ReportMstInfo> oMyReportMstInfoCollection = null;
    List<ReportMstInfo> oStandardReportMstInfoCollection = null;

    protected void Page_Init(object sender, EventArgs e)
    {
        MasterPageBase ompage = (MasterPageBase)this.Master;
        ompage.ReconciliationPeriodChangedEventHandler += new EventHandler(ompage_ReconciliationPeriodChangedEventHandler);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        Helper.SetPageTitle(this, 1631);
        Helper.SetBreadcrumbs(this, 1073);

        if (Request.QueryString[QueryStringConstants.REPORT_SECTION_ID] != null)
            _reportSectionID = Request.QueryString[QueryStringConstants.REPORT_SECTION_ID];

        if (!IsPostBack)
        {
            if (_reportSectionID == _reportSectionIDStandardReport)
            {
                cpeMyReport.Collapsed = true;
                cpeStandardReport.Collapsed = false;
            }
            else if (_reportSectionID == _reportSectionIDMyReport)
            {
                cpeMyReport.Collapsed = false;
                cpeStandardReport.Collapsed = true;
            }
        }

    }

    void ompage_ReconciliationPeriodChangedEventHandler(object sender, EventArgs e)
    {
        BindStandardReportsGroup();

        //myreport 
        BindMyReportsGroup();
    }



    private void BindStandardReportsGroup()
    {
        //Standard report 
        IReport oReport = RemotingHelper.GetReportObject();
        oStandardReportMstInfoCollection = oReport.SelectAllReportByRoleID(SessionHelper.CurrentRoleID, SessionHelper.CurrentReconciliationPeriodID, SessionHelper.CurrentCompanyID, Helper.GetAppUserInfo());

        List<ReportMstInfo> oReprotTypeList = new List<ReportMstInfo>();
        if (oStandardReportMstInfoCollection.Count > 0)
        {
            oReprotTypeList.Add(oStandardReportMstInfoCollection[0]);
            for (int i = 1; i <= oStandardReportMstInfoCollection.Count - 1; i++)
            {
                if (oStandardReportMstInfoCollection[i - 1].ReportTypeLabelID != oStandardReportMstInfoCollection[i].ReportTypeLabelID)
                    oReprotTypeList.Add(oStandardReportMstInfoCollection[i]);
            }
            rptStandardReportGroup.DataSource = oReprotTypeList;
            rptStandardReportGroup.DataBind();
        }
    }

    public void BindMyReportsGroup()
    {
        IReport oReport = RemotingHelper.GetReportObject();
        oMyReportMstInfoCollection = oReport.SelectAllMyReportByRoleID(SessionHelper.CurrentRoleID, SessionHelper.CurrentUserID, SessionHelper.CurrentReconciliationPeriodID, Helper.GetAppUserInfo());
        List<ReportMstInfo> oMyReprotTypeList = new List<ReportMstInfo>();

        if (oMyReportMstInfoCollection.Count > 0)
        {
            oMyReprotTypeList.Add(oMyReportMstInfoCollection[0]);
            for (int i = 1; i <= oMyReportMstInfoCollection.Count - 1; i++)
            {
                if (oMyReportMstInfoCollection[i - 1].ReportTypeLabelID != oMyReportMstInfoCollection[i].ReportTypeLabelID)
                {
                    oMyReprotTypeList.Add(oMyReportMstInfoCollection[i]);
                }
            }

        }

        rptMyReportGroup.DataSource = oMyReprotTypeList;
        rptMyReportGroup.DataBind();
    }

    public override string GetMenuKey()
    {
        //return "ReportHome";
        if (_reportSectionID == "1")
            return "StandardReports";
        else
            return "MyReports";
    }

    protected void rptStandardReportGroup_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        try
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                ReportMstInfo oReportMstInfo = (ReportMstInfo)e.Item.DataItem;
                ExLabel lblReportGroup = (ExLabel)e.Item.FindControl("lblReportGroup");

                lblReportGroup.LabelID = oReportMstInfo.ReportTypeLabelID.Value;
                Repeater rptReport = (Repeater)e.Item.FindControl("rptStandardReport");
                rptReport.ItemDataBound += rptStandardReport_ItemDataBound;
                rptReport.DataSource = GetAllStandardReportsByReportType(oReportMstInfo.ReportTypeLabelID.Value);
                rptReport.DataBind();
            }
        }
        catch (Exception ex)
        {
            Helper.ShowErrorMessage((PageBase)this.Page, ex);
        }
    }

    private List<ReportMstInfo> GetAllStandardReportsByReportType(int ReportTypeLabelID)
    {
        List<ReportMstInfo> oReportMstInfoCollection = (List<ReportMstInfo>)oStandardReportMstInfoCollection.FindAll(o => o.ReportTypeLabelID.Value == ReportTypeLabelID);
        if (ReportTypeLabelID == 1106)
        {
            CompanyHdrInfo compHdrInfo = SessionHelper.GetCurrentCompanyHdrInfo();
            if (compHdrInfo.PackageID == 1)
            {
                oReportMstInfoCollection.RemoveAll(o => o.ReportID == 12);
            }
        }
        return oReportMstInfoCollection;
    }

    public void rptStandardReport_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        try
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                ReportMstInfo oReportMstInfo = (ReportMstInfo)e.Item.DataItem;

                ExHyperLink hlReport = (ExHyperLink)e.Item.FindControl("hlReport");
                ExHyperLink hlActivity = (ExHyperLink)e.Item.FindControl("hlActivity");
                ExLabel lblReportDescription = (ExLabel)e.Item.FindControl("lblReportDescription");

                lblReportDescription.LabelID = oReportMstInfo.DescriptionLabelID.Value;

                string urlStdReport = "ReportParameter.aspx?" + QueryStringConstants.REPORT_ID + "=" + oReportMstInfo.ReportID + "&" + QueryStringConstants.REPORT_SECTION_ID + "=" + _reportSectionIDStandardReport + "&" + QueryStringConstants.REPORT_SESSIONCLEAR + "=" + "1";
                string urlReportActivityStdReport = "ReportActivity.aspx?" + QueryStringConstants.REPORT_ID + "=" + oReportMstInfo.ReportID + "&" + QueryStringConstants.REPORT_SECTION_ID + "=" + _reportSectionIDStandardReport + "&" + QueryStringConstants.IS_REPORT_ACTIVITY + "=1";

                hlReport.LabelID = oReportMstInfo.ReportLabelID.Value;
                hlReport.NavigateUrl = urlStdReport;

                hlActivity.NavigateUrl = urlReportActivityStdReport;
            }
        }
        catch (Exception ex)
        {
            Helper.ShowErrorMessage((PageBase)this.Page, ex);
        }
    }


    protected void rptMyReportGroup_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        try
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                ReportMstInfo oReportMstInfo = (ReportMstInfo)e.Item.DataItem;
                ExLabel lblReportGroup = (ExLabel)e.Item.FindControl("lblMyReportGroup");
                lblReportGroup.LabelID = oReportMstInfo.ReportTypeLabelID.Value;

                Repeater rptReport = (Repeater)e.Item.FindControl("rptMyReport");
                rptReport.ItemDataBound += rptMyReport_ItemDataBound;
                rptReport.DataSource = getAllMyRepotsByReportType(oReportMstInfo.ReportTypeLabelID.Value);
                rptReport.DataBind();
            }
        }
        catch (Exception ex)
        {
            Helper.ShowErrorMessage((PageBase)this.Page, ex);
        }
    }

    public void rptMyReport_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        try
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                ReportMstInfo oReportMstInfo = (ReportMstInfo)e.Item.DataItem;
                ExHyperLink hlReport = (ExHyperLink)e.Item.FindControl("hlMyReport");
                //hlReport.Text = oReportMstInfo.Report;
                hlReport.LabelID = oReportMstInfo.ReportLabelID.Value;
                string urlStdReport = "ReportSaved.aspx?" + QueryStringConstants.REPORT_ID + "=" + oReportMstInfo.ReportID + "&" + QueryStringConstants.REPORT_SECTION_ID + "=" + _reportSectionIDMyReport; ;
                hlReport.NavigateUrl = urlStdReport;
                ExHyperLink hlActivity = (ExHyperLink)e.Item.FindControl("hlMyActivity");
                string urlReportActivityStdReport = "ReportActivity.aspx?" + QueryStringConstants.REPORT_ID + "=" + oReportMstInfo.ReportID + "&" + QueryStringConstants.REPORT_SECTION_ID + "=" + _reportSectionIDMyReport + "&" + QueryStringConstants.IS_REPORT_ACTIVITY + "=1";
                hlActivity.NavigateUrl = urlReportActivityStdReport;
                ExLabel lblReportDescription = (ExLabel)e.Item.FindControl("lblMyReportDescription");
                lblReportDescription.LabelID = oReportMstInfo.DescriptionLabelID.Value;
                ExImageButton imgBtnDeleteReport = (ExImageButton)e.Item.FindControl("imgBtnDeleteReport");
                imgBtnDeleteReport.ImageUrl = URLConstants.URL_IMAGE_DELETE;
                imgBtnDeleteReport.CommandArgument = oReportMstInfo.ReportID.ToString();
                imgBtnDeleteReport.CommandName = "DeleteMyReport";
                imgBtnDeleteReport.OnClientClick = "return ConfirmDelete('" + LanguageUtil.GetValue(1781) + "');";
                imgBtnDeleteReport.ToolTip = LanguageUtil.GetValue(1564);




            }
        }
        catch (Exception ex)
        {
            Helper.ShowErrorMessage((PageBase)this.Page, ex);
        }
    }

    public void DeleteMyReport(object sender, CommandEventArgs e)
    {
        if (e.CommandName == "DeleteMyReport")
        {
            short? ReportID = Convert.ToInt16(e.CommandArgument);
            IReport oReport = RemotingHelper.GetReportObject();
            bool isDeleted = oReport.DeleteMyReportByReportID(SessionHelper.CurrentRoleID, SessionHelper.CurrentUserID, ReportID, Helper.GetAppUserInfo());

            if (isDeleted)
            {
                BindMyReportsGroup();
            }
        }

    }

    private List<ReportMstInfo> getAllMyRepotsByReportType(int ReportTypeLabelID)
    {
        List<ReportMstInfo> oReportMstInfoCollection = (List<ReportMstInfo>)oMyReportMstInfoCollection.FindAll(o => o.ReportTypeLabelID.Value == ReportTypeLabelID);
        return oReportMstInfoCollection;
    }

}//end of class