using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SkyStem.ART.Web.Classes.UserControl;
using SkyStem.Language.LanguageUtility;
using SkyStem.ART.Web.Data;
using SkyStem.ART.Client.Model;
using SkyStem.ART.Web.Utility;
using SkyStem.ART.Client.IServices;
using System.IO;
using SkyStem.ART.Client.Exception;
using SkyStem.ART.Web.Classes;
using SkyStem.ART.Client.Data;


public partial class UserControls_RecForm_RecStatusBar : UserControlRecStatusBarBase
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
    }
    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        lblUnexpVar.Text = string.Format("{0} ({1}):", LanguageUtil.GetValue(1799), LanguageUtil.GetValue(1424));
        lblReconciledBalance.Text = string.Format("{0} ({1}):", LanguageUtil.GetValue(1385), LanguageUtil.GetValue(1424));

        tdRecStatus.Visible = this.ShowRecStatus;
        pnlReconciledBalance.Visible = this.ShowReconciledBalance;
        tdUnexpVar.Visible = this.ShowUnexpVar;
        pnlDueDate1.Visible = false;
        tdDueDate2.Visible = false;
        tdExportButtons.Visible = this.ShowExportButton;
        tdQualityScore.Visible = this.ShowQualityScore;
        lblCertificationStartDate.Visible = Helper.GetFeatureCapabilityModeForCurrentRecPeriod(WebEnums.Feature.Certification, ARTEnums.Capability.CertificationActivation) == WebEnums.FeatureCapabilityMode.Visible;
        lblCertificationStartDateValue.Visible = lblCertificationStartDate.Visible;

        if (SessionHelper.CurrentRoleID == (short)WebEnums.UserRole.AUDIT)
        {
            List<CompanyAttributeConfigInfo> roleConfigInfo = AttributeConfigHelper.GetCompanyAttributeConfigInfoList(false, WebEnums.AttributeSetType.RoleConfig);
            if (roleConfigInfo != null && roleConfigInfo.Count > 0)
            {
                bool isQualitySectionSelected = false;
                CompanyAttributeConfigInfo isQualitySection = (CompanyAttributeConfigInfo)roleConfigInfo.Find(c => c.AttributeID == (int)ARTEnums.AttributeList.NotSeeQSSection);
                if (isQualitySection != null)
                {
                    if (isQualitySection.IsEnabled.HasValue)
                    {
                        if ((bool)isQualitySection.IsEnabled)
                        {
                            isQualitySectionSelected = true;
                        }
                    }
                }
                tdQualityScore.Visible = !isQualitySectionSelected;
            }
        }
        if (this.ShowExportButton)
        {
            string urlPrint = GetUrlForPrint();
            hlPrint.NavigateUrl = "javascript:OpenPrintWindow('" + urlPrint + "', " + WebConstants.PRINT_POPUP_HEIGHT + ", " + WebConstants.PRINT_POPUP_WIDTH + ");";

            string urlPdf = GetUrlForPDF();
            hlPDF.NavigateUrl = "javascript:OpenPrintWindow('" + urlPdf + "', " + WebConstants.PRINT_POPUP_HEIGHT + ", " + WebConstants.PRINT_POPUP_WIDTH + ");";


            string urlEmail = GetUrlForEmail();
            hlEmail.NavigateUrl = "javascript:OpenPrintWindow('" + urlEmail + "', " + WebConstants.PRINT_POPUP_HEIGHT + ", " + WebConstants.PRINT_POPUP_WIDTH + ");";

            string urlDownloadAttachments = GetUrlForDownloadAttachments();
            lnkBtnDownloadAttachments.Attributes.Add("onclick", "javascript:{$get('" + ifDownloader.ClientID + "').src='" + urlDownloadAttachments + "'; return false;}");
        }
        if (this.GLDataID != null)
        {
            // Get the GL Data and Display 
            IGLData oGLDataClient = RemotingHelper.GetGLDataObject();
            GLDataHdrInfo oGLDataHdrInfo = oGLDataClient.GetLiteGLDataInfoByGLDataID(this.GLDataID, Helper.GetAppUserInfo());

            lblRecStatus.Text = Helper.GetDisplayStringValue(LanguageUtil.GetValue(oGLDataHdrInfo.ReconciliationStatusLabelID.Value));

            if (pnlReconciledBalance.Visible)
            {
                lblReconciledBalanceValue.Text = Helper.GetDisplayReportingCurrencyValue(oGLDataHdrInfo.ReconciliationBalanceReportingCurrency);
            }

            if (tdUnexpVar.Visible)
            {
                lblUnexpVarValue.Text = Helper.GetDisplayReportingCurrencyValue(oGLDataHdrInfo.UnexplainedVarianceReportingCurrency);
            }

            if (this.ShowDueDates)
            {
                if (SessionHelper.CurrentRoleEnum == WebEnums.UserRole.PREPARER
                    || SessionHelper.CurrentRoleEnum == WebEnums.UserRole.REVIEWER
                    || SessionHelper.CurrentRoleEnum == WebEnums.UserRole.APPROVER)
                {
                    pnlDueDate1.Visible = true;
                    tdDueDate2.Visible = true;
                    DateTime? dtDueDate = null;
                    ReconciliationPeriodInfo oReconciliationPeriodInfo = Helper.GetRecPeriodInfo(SessionHelper.CurrentReconciliationPeriodID);

                    switch (SessionHelper.CurrentRoleEnum)
                    {
                        case WebEnums.UserRole.PREPARER:
                            dtDueDate = oReconciliationPeriodInfo.PreparerDueDate;
                            break;

                        case WebEnums.UserRole.REVIEWER:
                            dtDueDate = oReconciliationPeriodInfo.ReviewerDueDate;
                            break;

                        case WebEnums.UserRole.APPROVER:
                            dtDueDate = oReconciliationPeriodInfo.ApproverDueDate;
                            break;
                    }
                    lblAccountDueDateValue.Text = Helper.GetDisplayDate(dtDueDate);
                    lblCertificationStartDateValue.Text = Helper.GetDisplayDate(oReconciliationPeriodInfo.CertificationStartDate);
                }
            }
            if (this.ShowQualityScore)
            {
                ucDisplayQualityScore.SetGLDataQualityScoreCount(QualityScoreHelper.GetGLDataQualityScoreCount(GLDataID));
            }
        }
    }
    #endregion

    #region Grid Events
    #endregion

    #region Other Events
    #endregion

    #region Validation Control Events
    #endregion

    #region Private Methods
    private string GetUrlForPrint()
    {
        string url = "";
        url = "PopupForRecFormPrint.aspx?IsPrint=1" + "&" + GetCommonQueryStringParameters(url);
        return url;
    }

    private string GetUrlForPDF()
    {
        string url = "";
        url = "PopupForRecFormPrint.aspx?IsPDF=1" + "&" + GetCommonQueryStringParameters(url);
        return url;
    }

    private string GetUrlForEmail()
    {
        string url = "";
        url = "PopupForRecFormPrint.aspx?IsEmail=1" + "&" + GetCommonQueryStringParameters(url);
        return url;
    }

    private string GetUrlForDownloadAttachments()
    {
        string url = "";
        url = string.Format("Downloader?{0}={1}&", QueryStringConstants.HANDLER_ACTION, (Int32)WebEnums.HandlerActionType.DownloadRecAttachments);
        url += GetCommonQueryStringParameters(url);
        return url;
    }

    private string GetCommonQueryStringParameters(string url)
    {
        url = QueryStringConstants.PAGE_TITLE_ID + "=" + this.PageTitleLabeID
            + "&" + QueryStringConstants.ACCOUNT_ID + "=" + this.AccountID.GetValueOrDefault()
            + "&" + QueryStringConstants.NETACCOUNT_ID + "=" + this.NetAccountID.GetValueOrDefault()
            + "&" + QueryStringConstants.GLDATA_ID + "=" + this.GLDataID
            + "&" + QueryStringConstants.REC_TEMPLATE_ID + "=" + ((short?)((ARTEnums.ReconciliationItemTemplate)Session[SessionConstants.REC_ITEM_TEMPLATE_ID])).GetValueOrDefault();
        return url;
    }
    #endregion

    #region Other Methods
    protected void imgBtnExportToPDF_Click(object sender, EventArgs e)
    {
        ReportMstInfo oReportInfo = (ReportMstInfo)Session[SessionConstants.REPORT_INFO_OBJECT];
        string pageTitle = LanguageUtil.GetValue(oReportInfo.ReportLabelID.Value);
        string fileName = pageTitle + ".pdf";
        fileName = ExportHelper.RemoveInvalidFileNameChars(fileName);

        string url = GetUrlForPDF();
        TextWriter oTextWriter = new StringWriter();
        Server.Execute(url, oTextWriter);
        ExportHelper.GeneratePDFAndRender(pageTitle, fileName, oTextWriter.ToString(), true, true);
    }
    #endregion

}
