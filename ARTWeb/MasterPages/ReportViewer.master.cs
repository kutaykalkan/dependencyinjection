using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using SkyStem.ART.Web.Classes;
using SkyStem.ART.Web.Data;
using SkyStem.ART.Client.Model;
using SkyStem.Language.LanguageUtility;
using System.Text;
using SkyStem.ART.Web.Utility;
using System.Collections.Generic;
using SkyStem.ART.Client.IServices;
using SkyStem.Library.Controls.TelerikWebControls;
using ExpertPdf.HtmlToPdf;
using System.IO;
using SkyStem.ART.Client.Data;

public partial class MasterPages_ReportViewer : ReportViewerMasterPageBase
{
    int? _ReportRoleMandatoryReportID = null;

    protected void Page_Init(object sender, EventArgs e)
    {
        Session[SessionConstants.DATE_TIME] = DateTime.Now.ToString();
        if (Session[SessionConstants.REPORT_INFO_OBJECT] != null)
        {
            ReportMstInfo oReportInfo = (ReportMstInfo)Session[SessionConstants.REPORT_INFO_OBJECT];
            Session[SessionConstants.REPORT_CREATE_DATETIME] = DateTime.Now;

            lblAdditionalComments.Visible = false;
            lblAdditionalCommentsValue.Visible = false;

            if (Request.QueryString[QueryStringConstants.REPORT_TYPE] != null)
            {
                short reportType = Convert.ToInt16(Request.QueryString[QueryStringConstants.REPORT_TYPE]);

                switch (reportType)
                {
                    //Standard Report OR MyReport + Changed Params
                    case (short)WebEnums.ReportType.StandardReport:
                        this.RunReportWithReportInfoAndParamCollection(oReportInfo);
                        break;
                    //My Reports
                    case (short)WebEnums.ReportType.UserSavedReport:
                        this.RunReportWithReportInfoAndParamCollection(oReportInfo);
                        break;
                    //Report Activity for Archived Report and signed off
                    case (short)WebEnums.ReportType.ArchivedReport:
                        this.RunArchivedReport();
                        break;
                    //MandatoryReport
                    case (short)WebEnums.ReportType.MandatoryReport:
                        this.RunMandatoryReport();
                        break;
                    //Mandatory already SignedOFF
                    case (short)WebEnums.ReportType.MandatoryReportSignedOff:
                        this.RunMandatoryReport();
                        break;
                }
                ReportHelper.EnableDisableReportOptions(reportType, this.btnArchive, this.btnSignOff, this.btnSave, this.hlkChangeParam);
            }
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        MasterPageBase oMasterPageBase = (MasterPageBase)this.Master;
        oMasterPageBase.RegisterPostBackToControls(imgBtnExportToPDF);

        hlkChangeParam.ToolTip = LanguageUtil.GetValue(2039);

        // ToDo: Apoorv - Change the implementation below by using a method instead of hard-code
        DropDownList ddlRecPeriod = (DropDownList)oMasterPageBase.FindControl("ddlReconciliationPeriod");
        if (ddlRecPeriod != null)
            ddlRecPeriod.Enabled = false;

    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        imgBtnExportToPDF.Attributes.Add("onclick", "ResetInnerHTML('" + hdnInnerHTML.ClientID + "');");

        string qMandatoryReport = "";
        if (_ReportRoleMandatoryReportID.HasValue && _ReportRoleMandatoryReportID.Value > 0)
        {
            qMandatoryReport = QueryStringConstants.MANDATORY_REPORT_ID + "=" + _ReportRoleMandatoryReportID.Value + "&";
        }
        //string _PopupUrl = "/ARTWeb/Pages/ReportSignOffArchive.aspx?"
        string _PopupUrl = Page.ResolveClientUrl("~/Pages/ReportSignOffArchive.aspx?")
            + QueryStringConstants.REC_PERIODC_END_DATE + "=" + ucReportInfo.RecPeriodEndDateText
            + "&" + qMandatoryReport + QueryStringConstants.REPORT_ARCHIVE_TYPE + "=";

        btnArchive.Attributes.Add("onclick", "javascript:return OpenRadWindow('" + _PopupUrl + WebEnums.ReportArchiveTypeMst.Archive.ToString() + "', " + 370 + " , " + 450 + ");");
        btnSignOff.Attributes.Add("onclick", "javascript:return OpenRadWindow('" + _PopupUrl + WebEnums.ReportArchiveTypeMst.SignOff.ToString() + "', " + 370 + " , " + 450 + ");");

        //string _PopupUrlSave = "/ARTWeb/Pages/AddToMyReport.aspx";
        string _PopupUrlSave = Page.ResolveClientUrl("~/Pages/AddToMyReport.aspx");
        btnSave.Attributes.Add("onclick", "javascript:return OpenRadWindow('" + _PopupUrlSave + "', " + 200 + " , " + 500 + ");");

        string url = GetUrlForPDF();

        hlPrint.NavigateUrl = "javascript:OpenPrintWindow('" + url + "', " + WebConstants.PRINT_POPUP_HEIGHT + ", " + WebConstants.PRINT_POPUP_WIDTH + ");";
        ReportMstInfo oReportInfo = (ReportMstInfo)Session[SessionConstants.REPORT_INFO_OBJECT];
        string urlEmailPopUp = Page.ResolveClientUrl("~/Pages/EmailPopupForRecForm.aspx") + "?"
                  + QueryStringConstants.EMAIL_INFO_SPECIFIC + "=" + "Report" + "&" + QueryStringConstants.PAGE_ID + "=" + (short)WebEnums.ARTPages.ReportViewer;

        hlEmail.NavigateUrl = "javascript:OpenRadWindowForHyperlink('" + urlEmailPopUp + "', 400, 500);";
        HandleFormMode();
    }

    private void HandleFormMode()
    {
        if (SessionHelper.CurrentRoleEnum == ARTEnums.UserRole.AUDIT)
        {
            btnArchive.Enabled = false;
            btnSignOff.Enabled = false;
            btnSave.Enabled = false;
        }
    }

    private string GetUrlForPDF()
    {
        string url = "";
        ReportMstInfo oReportInfo = (ReportMstInfo)Session[SessionConstants.REPORT_INFO_OBJECT];
        url = Page.ResolveClientUrl(oReportInfo.ReportPrintUrl)
            + "?" + QueryStringConstants.REC_PERIODC_END_DATE + "=" + ucReportInfo.RecPeriodEndDateText
            + "&" + QueryStringConstants.SHOW_COMMENTS + "=" + (lblAdditionalCommentsValue.Visible == true ? "true" : "false");

        return url;
    }

    protected void imgBtnExportToPDF_Click(object sender, EventArgs e)
    {
        ReportMstInfo oReportInfo = (ReportMstInfo)Session[SessionConstants.REPORT_INFO_OBJECT];
        string pageTitle = LanguageUtil.GetValue(oReportInfo.ReportLabelID.Value);
        string fileName = pageTitle + ".pdf";
        fileName = ExportHelper.RemoveInvalidFileNameChars(fileName);

        string url = GetUrlForPDF();
        TextWriter oTextWriter = new StringWriter();
        Server.Execute(url, oTextWriter);
        ExportHelper.GeneratePDFAndRender(pageTitle, fileName, oTextWriter.ToString(), true, true, true);
    }

    private string GetNavigateURL(ReportMstInfo oReportInfo)
    {
        StringBuilder oSbNavigateUrl = new StringBuilder();
        oSbNavigateUrl.Append("~/Pages/ReportParameter.aspx");
        oSbNavigateUrl.Append("?");
        oSbNavigateUrl.Append(QueryStringConstants.REPORT_ID);
        oSbNavigateUrl.Append("=");
        oSbNavigateUrl.Append(oReportInfo.ReportID.Value.ToString());
        oSbNavigateUrl.Append("&");
        oSbNavigateUrl.Append(QueryStringConstants.REPORT_SECTION_ID);
        oSbNavigateUrl.Append("=");
        oSbNavigateUrl.Append(Request.QueryString[QueryStringConstants.REPORT_SECTION_ID]);
        return oSbNavigateUrl.ToString();
    }

    #region "Private Methods"
    private void RunReportWithReportInfoAndParamCollection(ReportMstInfo oReportInfo)
    {
        Dictionary<string, string> oRptCriteria = (Dictionary<string, string>)Session[SessionConstants.REPORT_CRITERIA];
        string recPeriodId = ReportHelper.GetCriteriaForCriteriaKey(oRptCriteria, ReportCriteriaKeyName.RPTCRITERIAKEYNAME_RECPERIOD);
        if (recPeriodId == WebConstants.CURRENT_REC_PERIOD_INDEX)
        {
            ucReportInfo.lblRecPeriodEndDate.Text = WebConstants.CURRENT_REC_PERIOD;
        }
        else
        {
            ucReportInfo.RecPeriodEndDate = this.GetRecPeriodDateTimeFromCriteriaCollection(oRptCriteria);
        }
        if (!this.IsPostBack)
        {
            this.hlkChangeParam.Text = LanguageUtil.GetValue(1870);
            this.hlkChangeParam.NavigateUrl = GetNavigateURL(oReportInfo);
        }
    }

    private void HandleFormModeForCertification()
    {
        WebEnums.FormMode eFormMode = Helper.GetFormModeForCertification(WebEnums.ARTPages.MandatoryReportSignOff);
        if (eFormMode == WebEnums.FormMode.ReadOnly)
        {
            btnSignOff.Enabled = false;
        }
        //else
        //    if (eFormMode == WebEnums.FormMode.Edit)
        //    {
        //        btnSignOff.Enabled = true ;
        //    }
    }

    private void RunMandatoryReport()
    {
        if (!string.IsNullOrEmpty(Request.QueryString[QueryStringConstants.MANDATORY_REPORT_ID]))
        {
            this._ReportRoleMandatoryReportID = Convert.ToInt32(Request.QueryString[QueryStringConstants.MANDATORY_REPORT_ID]);

            if (Session[SessionConstants.REPORT_INFO_OBJECT] != null)
            {
                ReportMstInfo oReportInfo = (ReportMstInfo)Session[SessionConstants.REPORT_INFO_OBJECT];
                Dictionary<string, string> oRptCriteria = GetDefaulParametersForMandatoryReport(oReportInfo.ReportID.Value);
                Session[SessionConstants.REPORT_CRITERIA] = oRptCriteria;
                ucReportInfo.RecPeriodEndDate = this.GetRecPeriodDateTimeFromCriteriaCollection(oRptCriteria);

                //if (oReportInfo.SignOffDate.HasValue)
                //{
                //    btnSignOff.Enabled  = false;
                //}

                ICertification oCertificationClient = RemotingHelper.GetCertificationObject();
                List<MandatoryReportSignOffInfo> oMandatoryReportSignOffInfoCollection = oCertificationClient.GetMandatoryReportSignOff(_ReportRoleMandatoryReportID, SessionHelper.CurrentUserID, SessionHelper.CurrentReconciliationPeriodID,Helper.GetAppUserInfo());
                if (oMandatoryReportSignOffInfoCollection != null && oMandatoryReportSignOffInfoCollection.Count > 0)
                {
                    //txtAdditionalComments.Text = oMandatoryReportSignOffInfoCollection[0].Comments;
                    lblAdditionalCommentsValue.Text = oMandatoryReportSignOffInfoCollection[0].Comments;
                    // Store into Session for use on Print / PDF Page
                    oReportInfo.Comments = lblAdditionalCommentsValue.Text;

                    if (oMandatoryReportSignOffInfoCollection[0].SignOffDate.HasValue)
                    {
                        hlkChangeParam.Enabled = false;
                        btnSave.Enabled = false;
                        btnArchive.Enabled = false;
                        btnSignOff.Enabled = false;


                        lblAdditionalComments.Visible = true;
                        lblAdditionalCommentsValue.Visible = true;
                        //txtAdditionalComments.Visible = false;

                        string userName = Helper.GetDisplayUserFullName(oMandatoryReportSignOffInfoCollection[0].FirstName, oMandatoryReportSignOffInfoCollection[0].LastName);
                        MakeSignatureVisible(oMandatoryReportSignOffInfoCollection[0].SignOffDate.Value, userName);
                    }
                    else
                    {
                        hlkChangeParam.Enabled = false;
                        btnSave.Enabled = false;
                        btnArchive.Enabled = false;
                        btnSignOff.Enabled = true;

                        lblAdditionalComments.Visible = false;
                        lblAdditionalCommentsValue.Visible = false;
                        //txtAdditionalComments.Visible = true;

                        ucSignature.Visible = false;
                    }
                }
                else
                {
                    hlkChangeParam.Enabled = false;
                    btnSave.Enabled = false;
                    btnArchive.Enabled = false;
                    btnSignOff.Enabled = true;

                    lblAdditionalComments.Visible = false;
                    lblAdditionalCommentsValue.Visible = false;
                    //txtAdditionalComments.Visible = true;

                    ucSignature.Visible = false;
                }

            }
            if (Helper.DisablePageBasedOnRecPeriodStatusForCertification())
            {
                btnSignOff.Enabled = false;
            }
            HandleFormModeForCertification();
        }
    }
    private void RunArchivedReport()
    {
        ReportArchiveInfo oRptArchiveInfo = (ReportArchiveInfo)Session[SessionConstants.REPORT_ARCHIVED_DATA];
        short reportArchiveType = -1;
        if (oRptArchiveInfo.ReportArchiveTypeID.HasValue)
            reportArchiveType = oRptArchiveInfo.ReportArchiveTypeID.Value;

        if (reportArchiveType == (short)WebEnums.ReportArchiveTypeMst.Archive)
            this.btnArchive.Enabled = false;
        if (reportArchiveType == (short)WebEnums.ReportArchiveTypeMst.SignOff)
            this.btnSignOff.Enabled = false;

        ucReportInfo.RecPeriodEndDate = oRptArchiveInfo.PeriodEndDate;
        ucReportInfo.ReportDateTime = oRptArchiveInfo.ReportCreateDateTime;

        Session[SessionConstants.REPORT_CREATE_DATETIME] = oRptArchiveInfo.ReportCreateDateTime.Value;
        //this.hlkChangeParam.Enabled = false;

        lblAdditionalCommentsValue.Text = oRptArchiveInfo.Comments;

        ReportMstInfo oReportInfo = (ReportMstInfo)Session[SessionConstants.REPORT_INFO_OBJECT];
        // Store into Session for use on Print / PDF Page
        oReportInfo.Comments = lblAdditionalCommentsValue.Text;

        lblAdditionalComments.Visible = true;
        lblAdditionalCommentsValue.Visible = true;
    }

    private Dictionary<string, string> GetDefaulParametersForMandatoryReport(short reportID)
    {
        Dictionary<string, string> _oCriteriaCollection = new Dictionary<string, string>();

        List<short> oRoleCollection = ReportHelper.GetPermittedRolesByReportID(reportID, SessionHelper.CurrentRoleID.Value, SessionHelper.CurrentReconciliationPeriodID);
        ListItemCollection oListItemUserCollection = ReportHelper.GetListItemCollectionForUser(oRoleCollection, SessionHelper.CurrentReconciliationPeriodID.Value.ToString());
        StringBuilder strRoleList = new StringBuilder();
        StringBuilder strUserList = new StringBuilder();
        if (oRoleCollection != null && oRoleCollection.Count > 0)
        {
            foreach (short roleID in oRoleCollection)
            {
                strRoleList.Append(roleID + ReportHelper.FilterValueSeparator);
            }
            strRoleList.Remove(strRoleList.Length - 1, 1);
        }
        if (oListItemUserCollection != null && oListItemUserCollection.Count > 0)
        {
            foreach (ListItem oListItem in oListItemUserCollection)
            {
                strUserList.Append(oListItem.Value + ReportHelper.FilterValueSeparator);
            }
            strUserList.Remove(strUserList.Length - 1, 1);
        }
        _oCriteriaCollection.Add(ReportCriteriaKeyName.RPTCRITERIAKEYNAME_USER, strUserList.ToString());
        _oCriteriaCollection.Add(ReportCriteriaKeyName.RPTCRITERIAKEYNAME_ROLE, strRoleList.ToString());
        _oCriteriaCollection.Add(ReportCriteriaKeyName.RPTCRITERIAKEYNAME_RECPERIOD, SessionHelper.CurrentReconciliationPeriodID.Value.ToString());

        string prmList = "";
        prmList = DefaultParameterList(ReportHelper.GetListItemCollectionForReason());
        _oCriteriaCollection.Add(ReportCriteriaKeyName.RPTCRITERIAKEYNAME_REASON, prmList);
        prmList = DefaultParameterList(ReportHelper.GetListItemCollectionForOpenItemClassification());
        _oCriteriaCollection.Add(ReportCriteriaKeyName.RPTCRITERIAKEYNAME_OPENITEMCLASSIFICATION, prmList);
        prmList = DefaultParameterList(ReportHelper.GetListItemCollectionForAging());
        _oCriteriaCollection.Add(ReportCriteriaKeyName.RPTCRITERIAKEYNAME_AGING, prmList);
        prmList = DefaultParameterList(ReportHelper.GetListItemCollectionForRecStatus());
        _oCriteriaCollection.Add(ReportCriteriaKeyName.RPTCRITERIAKEYNAME_RECSTATUS, prmList);
        prmList = DefaultParameterList(ReportHelper.GetListItemCollectionForExceptionType());
        _oCriteriaCollection.Add(ReportCriteriaKeyName.RPTCRITERIAKEYNAME_TYPEOFEXCEPTION, prmList);

        //And all others, if null is not allowed
        return _oCriteriaCollection;
    }

    private string DefaultParameterList(ListItemCollection oLstCollection)
    {
        StringBuilder sb = new StringBuilder();
        foreach (ListItem li in oLstCollection)
        {
            if (!string.IsNullOrEmpty(li.Value))
            {
                sb.Append(ReportHelper.FilterValueSeparator + li.Value);
            }
        }
        sb.Remove(0, 1);
        return sb.ToString();
    }


    private DateTime? GetRecPeriodDateTimeFromCriteriaCollection(Dictionary<string, string> oRptCriteria)
    {
        string recPeriodId = ReportHelper.GetCriteriaForCriteriaKey(oRptCriteria, ReportCriteriaKeyName.RPTCRITERIAKEYNAME_RECPERIOD);
        DateTime? recPeriodEndDate = null;
        if (!string.IsNullOrEmpty(recPeriodId) && recPeriodId != WebConstants.CURRENT_REC_PERIOD)
        {
            List<ReconciliationPeriodInfo> oRecPeriodCollection = CacheHelper.GetAllReconciliationPeriods(null);
            ReconciliationPeriodInfo oRecPeriod = oRecPeriodCollection.Find(r => r.ReconciliationPeriodID.Value == Convert.ToInt32(recPeriodId));
            if (oRecPeriod != null)
                recPeriodEndDate = oRecPeriod.PeriodEndDate;
        }
        return recPeriodEndDate;
    }
    #endregion

    private void MakeSignatureVisible(DateTime? signatureDate, string userName)
    {
        ucSignature.Visible = true;
        CertificationHelper.ShowHideSignature(ucSignature, signatureDate, userName);
        //btnSignOff.Visible = false;
    }

}//end of class
