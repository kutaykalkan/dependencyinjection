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
using SkyStem.ART.Client.Model;
using System.Collections.Generic;
using SkyStem.ART.Client.Model.Report;
using SkyStem.Language.LanguageUtility;
using SkyStem.ART.Client.IServices;
using System.IO;
using ExpertPdf.HtmlToPdf;
using SkyStem.ART.Client.Exception;


public partial class Pages_ReportSignOffArchive : PopupPageBase
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
        lblReportPeriodValue.Text = Request.QueryString[QueryStringConstants.REC_PERIODC_END_DATE];

        string reportArchiveType = this.Request.QueryString[QueryStringConstants.REPORT_ARCHIVE_TYPE];

        this.optArchive.Checked = (reportArchiveType == WebEnums.ReportArchiveTypeMst.Archive.ToString());
        this.optSignOff.Checked = (reportArchiveType == WebEnums.ReportArchiveTypeMst.SignOff.ToString());

        if (reportArchiveType == WebEnums.ReportArchiveTypeMst.Archive.ToString())
        {
            PopupHelper.SetPageTitle(this, 1583);
            optSignOff.Enabled = false;
        }
        else
        {
            PopupHelper.SetPageTitle(this, 1377);
            optArchive.Enabled = false;
        }
    }
    #endregion

    #region Grid Events
    #endregion

    #region Other Events
    protected void btnOk_Click(object sender, EventArgs e)
    {
        try
        {
            //Get report info object from session
            ReportMstInfo oReportInfo = (ReportMstInfo)Session[SessionConstants.REPORT_INFO_OBJECT];

            int? _ReportRoleMandatoryReportID = null;
            if (!string.IsNullOrEmpty(Request.QueryString[QueryStringConstants.MANDATORY_REPORT_ID]))
            {
                _ReportRoleMandatoryReportID = Convert.ToInt32(Request.QueryString[QueryStringConstants.MANDATORY_REPORT_ID]);
            }

            if (_ReportRoleMandatoryReportID.HasValue && _ReportRoleMandatoryReportID.Value > 0)
            {
                SaveMandatoryReportSignOffInDB(_ReportRoleMandatoryReportID.Value);
            }
            else
            {
                //Get report parameter dictionary from session
                Dictionary<string, string> oReportCriteria = (Dictionary<string, string>)Session[SessionConstants.REPORT_CRITERIA];

                //Determine ReportSaveActionType
                short reportArchiveType = -1;
                if (this.optSignOff.Checked)
                    reportArchiveType = (short)WebEnums.ReportArchiveTypeMst.SignOff;
                if (this.optArchive.Checked)
                    reportArchiveType = (short)WebEnums.ReportArchiveTypeMst.Archive;

                //determine rec periodID as selected in master page dropdown
                int currentRecPeriodID = SessionHelper.CurrentReconciliationPeriodID.Value;

                //get current userid
                int currentUserID = SessionHelper.CurrentUserID.Value;

                //get roleid
                short currentUserRoleID = SessionHelper.CurrentRoleID.Value;

                //get date time when report was prepared
                DateTime reportCreateTime = (DateTime)Session[SessionConstants.REPORT_CREATE_DATETIME];

                //get report data as binary serialized
                //List<UnusualBalancesReportInfo> rptDataObjectCollection = (List<UnusualBalancesReportInfo>)Session[SessionConstants.REPORT_DATA];

                //Create an object of ReportArchiveInfo
                ReportArchiveInfo oRptArchiveInfo = new ReportArchiveInfo();
                oRptArchiveInfo.ReportID = oReportInfo.ReportID.Value;
                oRptArchiveInfo.ReconciliationPeriodID = currentRecPeriodID;
                oRptArchiveInfo.UserID = currentUserID;
                oRptArchiveInfo.RoleID = currentUserRoleID;
                oRptArchiveInfo.ReportArchiveTypeID = reportArchiveType;
                oRptArchiveInfo.Comments = this.txtComments.Text;
                oRptArchiveInfo.EmailList = this.txtSharedWith.Text;
                oRptArchiveInfo.IsActive = true;
                oRptArchiveInfo.DateAdded = DateTime.Now;


                oRptArchiveInfo.ReportData = ReportHelper.GetBinarySerializedReportData(Session[ReportHelper.GetSessionConstantByReportID(oReportInfo.ReportID.Value)]);
                oRptArchiveInfo.AddedBy = SessionHelper.CurrentUserLoginID;
                oRptArchiveInfo.ReportCreateDateTime = reportCreateTime;
                //Create List of ReportArchiveParameter
                List<ReportArchiveParameterInfo> oRptArchiveParameterCollection = new List<ReportArchiveParameterInfo>();
                foreach (KeyValuePair<string, string> kvp in oReportCriteria)
                {
                    string keyName = kvp.Key;
                    string keyValue = kvp.Value;
                    short rptParamKeyId = ReportHelper.GetRptParamKeyIDForParamKey(keyName);
                    ReportArchiveParameterInfo oRptArchiveParamInfo = new ReportArchiveParameterInfo();
                    oRptArchiveParamInfo.ReportParameterKeyID = rptParamKeyId;
                    oRptArchiveParamInfo.ParameterValue = keyValue;
                    oRptArchiveParameterCollection.Add(oRptArchiveParamInfo);
                }
                ReportHelper.SaveArchivedReport(oRptArchiveInfo, oRptArchiveParameterCollection);
            }

            SendMail(oReportInfo);

            if (_ReportRoleMandatoryReportID.HasValue && _ReportRoleMandatoryReportID.Value > 0)
            {
                this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "ClosePopupAndRefreshParentPage", ScriptHelper.GetJSForClosePopupAndSubmitParentPage());
            }
            else
            {
                Label1.Text = "<script type='text/javascript'> GetRadWindow().Close();</" + "script>";
            }
        }
        catch (ARTException ex)
        {
            PopupHelper.ShowErrorMessage(this, ex);
        }
        catch (Exception ex)
        {
            PopupHelper.ShowErrorMessage(this, ex);
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "ClosePopupAndRefreshParentPage", ScriptHelper.GetJSForClosePopupAndSubmitParentPage(true));
    }
    #endregion

    #region Validation Control Events
    #endregion

    #region Private Methods
    private void SendMail(ReportMstInfo oReportInfo)
    {
        if (this.txtSharedWith.Text != "")
        {
            try
            {
                string mailBody = string.Empty; // LanguageUtil.GetValue(1931); // comment the code , not to show msg body.
                string fromEmailAddress = AppSettingHelper.GetAppSettingValue(AppSettingConstants.EMAIL_FROM_DEFAULT);//SessionHelper.GetCurrentUser().EmailID;
                string subject = oReportInfo.Report;

                // Send Attachment
                string pageTitle = LanguageUtil.GetValue(oReportInfo.ReportLabelID.Value);
                string fileName = pageTitle + System.Guid.NewGuid().ToString() + ".pdf";

                string url = Page.ResolveClientUrl(oReportInfo.ReportPrintUrl)
                    + "?" + QueryStringConstants.REC_PERIODC_END_DATE + "=" + lblReportPeriodValue.Text
                    + "&" + QueryStringConstants.SHOW_COMMENTS + "=" + (string.IsNullOrEmpty(txtComments.Text) ? "false" : "true")
                    + "&" + QueryStringConstants.COMMENTS + "=" + Server.UrlEncode(txtComments.Text);

                TextWriter oTextWriter = new StringWriter();
                Server.Execute(url, oTextWriter);
                string htmlToConvert = oTextWriter.ToString();
                ExportHelper.GeneratePDFAndSendMail(htmlToConvert, mailBody, this.txtSharedWith.Text, fromEmailAddress, subject, pageTitle, fileName);
            }
            catch
            {
                ARTException oARTException = new ARTException(5000177);
                PopupHelper.ShowErrorMessage(this, oARTException);
                throw;
            }
        }
    }
    private void SaveMandatoryReportSignOffInDB(int reportRoleMandatoryReportID)
    {
        MandatoryReportSignOffInfo oMandatoryReportSignOffInfo = new MandatoryReportSignOffInfo();
        oMandatoryReportSignOffInfo.CertificationTypeID = (short)WebEnums.CertificationType.MandatoryReportSignOff;
        oMandatoryReportSignOffInfo.Comments = txtComments.Text;
        oMandatoryReportSignOffInfo.UserID = SessionHelper.CurrentUserID;
        oMandatoryReportSignOffInfo.RoleID = SessionHelper.CurrentRoleID;
        oMandatoryReportSignOffInfo.ReconciliationPeriodID = SessionHelper.CurrentReconciliationPeriodID;
        oMandatoryReportSignOffInfo.SignOffDate = DateTime.Now;
        oMandatoryReportSignOffInfo.ReportRoleMandatoryReportID = reportRoleMandatoryReportID;//TODO://_ReportRoleMandatoryReportID;
        //oMandatoryReportSignOffInfo.UserName = Helper.GetUserFullName();
        ICertification oCertificationClient = RemotingHelper.GetCertificationObject();
        oCertificationClient.SaveMandatoryReportSignoff(oMandatoryReportSignOffInfo, Helper.GetAppUserInfo());

        //Commented by Vinay as Now Review notes should be deleted when period is closed
        //Delete review notes if certification is completed
        //IGLData oGLDataClient = RemotingHelper.GetGLDataObject();
        //if (Helper.IsCapabilityActivatedForCurrentRecPeriod(ARTEnums.Capability.CEOCFOCertification, false) == false
        //    && Helper.IsCapabilityActivatedForCurrentRecPeriod(ARTEnums.Capability.CertificationActivation, false) == false)
        //{
        //    oGLDataClient.DeleteReviewNotesAfterCertification(SessionHelper.CurrentReconciliationPeriodID.Value, WebConstants.REVISED_BY_FIELD_FOR_REVIEW_NOTE_DELETION, DateTime.Now);
        //}
    }
    #endregion

    #region Other Methods
    #endregion

}
