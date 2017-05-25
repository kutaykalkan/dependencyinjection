using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SkyStem.ART.Web.Classes;
using SkyStem.ART.Web.Utility;
using SkyStem.ART.Web.Data;
using SkyStem.ART.Client.Model;
using SkyStem.ART.Client.IServices;
using SkyStem.ART.Client.Exception;
using System.Text;
using SkyStem.Language.LanguageUtility;
using System.Data;
using SkyStem.ART.Client.Data;

public partial class Pages_RecItemImportStatusMessage : PageBaseCompany
{
    int? _DataImportID = null;
    DataImportHdrInfo oDataImportHdrInfo = null;
    public string _ReferrerUrl
    {
        get
        {
            object objReferrerUrl = ViewState["ReferrerUrl"];
            if (objReferrerUrl == null)
                return String.Empty;

            return (string)objReferrerUrl;
        }

        set
        {
            ViewState["ReferrerUrl"] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        Helper.SetPageTitle(this, 2497);
        Helper.GetUserFullName();

        try
        {
            _DataImportID = Convert.ToInt32(Request.QueryString[QueryStringConstants.DATA_IMPORT_ID]);



            if (!Page.IsPostBack)
            {
                _ReferrerUrl = Request.UrlReferrer.AbsoluteUri;

                if (!string.IsNullOrEmpty(Request.QueryString[QueryStringConstants.IS_REDIRECTED_FROM_TASK_IMOPRT]))
                    ExLabel2.LabelID = 2748;
                else
                    ExLabel2.LabelID = 2499;

                LoadData();
            }
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

    private void LoadData()
    {
        if (!string.IsNullOrEmpty(Request.QueryString[QueryStringConstants.IS_REDIRECTED_FROM_RECURRING_IMPORT]) || !string.IsNullOrEmpty(Request.QueryString[QueryStringConstants.IS_REDIRECTED_FROM_TASK_IMOPRT]))
        {
            DataImportHdrInfo _oDataImportHdrInfo = GetDataImportHdrInfo();
            if (_oDataImportHdrInfo != null)
                LoadImportStatusPnl(_oDataImportHdrInfo);
        }
        else
        {
            DataTable dtImportData = (DataTable)(Session["DataTableForImport"]);
            StringBuilder sbError = new StringBuilder();
            int rowIndex = 0;
            foreach (DataRow drImport in dtImportData.Rows)
            {
                rowIndex = rowIndex + 1;
                if (!string.IsNullOrEmpty(Convert.ToString(drImport["ErrorMessage"])))
                {
                    sbError.Append("Error in Row " + rowIndex + ": ");
                    sbError.Append(Convert.ToString(drImport["ErrorMessage"]));
                }
            }
            lblFailureMessages.Text = sbError.ToString();
        }
    }

    private DataImportHdrInfo GetDataImportHdrInfo()
    {
        DataImportHdrInfo _oDataImportHdrInfo = null;

        if (ViewState["DataImportHdrInfo"] == null)
        {
            IDataImport oDataImportClient = RemotingHelper.GetDataImportObject();
            _oDataImportHdrInfo = oDataImportClient.GetDataImportInfo(_DataImportID, Helper.GetAppUserInfo());
            ViewState["DataImportHdrInfo"] = _oDataImportHdrInfo;

        }
        else
            _oDataImportHdrInfo = (DataImportHdrInfo)ViewState["DataImportHdrInfo"];
        return _oDataImportHdrInfo;

    }
    private void LoadImportStatusPnl(DataImportHdrInfo oDataImportHdrInfo)
    {

        WebEnums.DataImportStatus eDataImportStatus = (WebEnums.DataImportStatus)System.Enum.Parse(typeof(WebEnums.DataImportStatus), oDataImportHdrInfo.DataImportStatusID.Value.ToString());

        imgSuccess.Visible = false;
        pnlWarning.Visible = false;
        pnlFailureMessages.Visible = false;
        btnYes.Visible = false;
        btnReject.Visible = false;



        switch (eDataImportStatus)
        {
            case WebEnums.DataImportStatus.Success:
                LoadSuccessPanel(oDataImportHdrInfo);
                break;

            case WebEnums.DataImportStatus.Failure:
                pnlFailureMessages.Visible = true;
                imgFailure.Visible = true;
                lblStatusHeading.LabelID = 1051;
                lblMessage.LabelID = 1623;
                if (oDataImportHdrInfo != null && oDataImportHdrInfo.DataImportFailureMessageInfo != null)
                    lblFailureMessages.Text = FormatFailureMessage(oDataImportHdrInfo.DataImportFailureMessageInfo.FailureMessage);
                break;


            case WebEnums.DataImportStatus.Warning:
                /* If Force commit, is already enabled and Status = Warning
                 * - Means User has already put the file for Force Commit 
                 * - Show a similar message
                 */
                LoadWarningPanel(oDataImportHdrInfo);
                break;

            case WebEnums.DataImportStatus.Processing:
                imgProcessing.Visible = true;
                lblStatusHeading.LabelID = 1620;
                lblMessage.LabelID = 1619;
                break;

            case WebEnums.DataImportStatus.Submitted:
                imgToBeProcessed.Visible = true;
                lblStatusHeading.LabelID = 1730;
                lblMessage.LabelID = 1783;
                break;
            case WebEnums.DataImportStatus.Reject:
                imgReject.Visible = true;
                lblStatusHeading.LabelID = 2400;
                lblMessage.LabelID = 2400;
                break;
        }
    }

    private void LoadSuccessPanel(DataImportHdrInfo oDataImportHdrInfo)
    {
        imgSuccess.Visible = true;
        lblStatusHeading.LabelID = 1050;
        lblMessage.LabelID = 1618;
    }

    private void LoadWarningPanel(DataImportHdrInfo oDataImportHdrInfo)
    {
        pnlFailureMessages.Visible = true;
        lblMessage.Visible = false;
        imgWarning.Visible = true;
        lblStatusHeading.LabelID = 1546;
        lblFailureMessages.Text = FormatFailureMessage(oDataImportHdrInfo.DataImportFailureMessageInfo.FailureMessage);

        if (!oDataImportHdrInfo.IsForceCommit.HasValue)
        {
            btnYes.Visible = true;
            btnReject.Visible = true;
        }
        else
        {
            // lblConfirmUpload.LabelID = 1784;
        }

    }

    private string FormatFailureMessage(string msg)
    {
        msg = msg.Replace(" , ", "<br>");
        msg = msg.Replace(" ,", "<br>");
        msg = msg.Replace(",", "<br>");
        return msg;
    }

    public override string GetMenuKey()
    {
        return "DataImportStatus";
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request.QueryString[QueryStringConstants.IS_REDIRECTED_FROM_TASK_IMOPRT]))
        {
            string url = _ReferrerUrl;
            Response.Redirect(url);
        }
        else
        {
            if (_ReferrerUrl.Contains(QueryStringConstants.IS_REDIRECTED_FROM_STATUSPAGE))
            {
                string tempReferrerUrl = _ReferrerUrl;
                int indexStatusMessage = tempReferrerUrl.IndexOf(QueryStringConstants.IS_REDIRECTED_FROM_STATUSPAGE);
                _ReferrerUrl = _ReferrerUrl.Substring(0, indexStatusMessage - 1);
            }
            string url = _ReferrerUrl;
            if (!url.Contains("?"))
            {
                url += "&" + QueryStringConstants.IS_REDIRECTED_FROM_STATUSPAGE + "=2399";
                url += "&" + QueryStringConstants.CONFIRMATION_MESSAGE_FROM_STATUSPAGE + "=1";
            }
            Response.Redirect(url);
        }
    }

    protected void btnYes_Click(object sender, EventArgs e)
    {
        try
        {
            if (!string.IsNullOrEmpty(Request.QueryString[QueryStringConstants.IS_REDIRECTED_FROM_RECURRING_IMPORT]))
            {
                UpdateDataImportStatus((short)WebEnums.DataImportStatus.Submitted);
            }
            if (!string.IsNullOrEmpty(Request.QueryString[QueryStringConstants.IS_REDIRECTED_FROM_TASK_IMOPRT]))
            {
                UpdateDataImportStatus((short)WebEnums.DataImportStatus.Submitted);
                string url = _ReferrerUrl;
                if (!url.Contains("?"))
                {
                    url += "?" + QueryStringConstants.IS_REDIRECTED_FROM_STATUSPAGE + "=1784";
                    url += "&" + QueryStringConstants.CONFIRMATION_MESSAGE_FROM_STATUSPAGE + "=3";
                }
                Response.Redirect(url);
            }
            else
            {

                if (_ReferrerUrl.Contains(QueryStringConstants.IS_REDIRECTED_FROM_STATUSPAGE))
                {
                    string tempReferrerUrl = _ReferrerUrl;
                    int indexStatusMessage = tempReferrerUrl.IndexOf(QueryStringConstants.IS_REDIRECTED_FROM_STATUSPAGE);
                    _ReferrerUrl = _ReferrerUrl.Substring(0, indexStatusMessage - 1);
                }
                string url = _ReferrerUrl;
                if (!url.Contains("?"))
                {
                    url += "&" + QueryStringConstants.IS_REDIRECTED_FROM_STATUSPAGE + "=1784";
                    url += "&" + QueryStringConstants.CONFIRMATION_MESSAGE_FROM_STATUSPAGE + "=3";
                }
                Response.Redirect(url);
            }
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

    private void UpdateDataImportStatus(short DataImportStatusID)
    {
        DataImportHdrInfo oDataImportHdrInfo = new DataImportHdrInfo();
        oDataImportHdrInfo.DataImportID = _DataImportID;
        oDataImportHdrInfo.DataImportStatusID = DataImportStatusID;
        oDataImportHdrInfo.IsForceCommit = true;
        oDataImportHdrInfo.ForceCommitDate = DateTime.Now;
        oDataImportHdrInfo.DateRevised = DateTime.Now;
        oDataImportHdrInfo.RevisedBy = SessionHelper.CurrentUserLoginID;

        IDataImport oDataImportClient = RemotingHelper.GetDataImportObject();
        oDataImportClient.UpdateDataImportForForceCommit(oDataImportHdrInfo, Helper.GetAppUserInfo());

    }

    protected void btnReject_Click(object sender, EventArgs e)
    {
        try
        {
            if (!string.IsNullOrEmpty(Request.QueryString[QueryStringConstants.IS_REDIRECTED_FROM_RECURRING_IMPORT]))
            {
                UpdateDataImportStatus((short)WebEnums.DataImportStatus.Reject);
            }
            if (!string.IsNullOrEmpty(Request.QueryString[QueryStringConstants.IS_REDIRECTED_FROM_TASK_IMOPRT]))
            {
                UpdateDataImportStatus((short)WebEnums.DataImportStatus.Reject);
                string url = _ReferrerUrl;
                if (!url.Contains("?"))
                {
                    url += "?" + QueryStringConstants.IS_REDIRECTED_FROM_STATUSPAGE + "=1784";
                    url += "&" + QueryStringConstants.CONFIRMATION_MESSAGE_FROM_STATUSPAGE + "=3";
                }
                Response.Redirect(url);
            }
            else
            {

                if (_ReferrerUrl.Contains(QueryStringConstants.IS_REDIRECTED_FROM_STATUSPAGE))
                {
                    string tempReferrerUrl = _ReferrerUrl;
                    int indexStatusMessage = tempReferrerUrl.IndexOf(QueryStringConstants.IS_REDIRECTED_FROM_STATUSPAGE);
                    _ReferrerUrl = _ReferrerUrl.Substring(0, indexStatusMessage - 1);
                }
                string url = _ReferrerUrl;
                if (!url.Contains("?"))
                {
                    url += "&" + QueryStringConstants.IS_REDIRECTED_FROM_STATUSPAGE + "=2399";
                    url += "&" + QueryStringConstants.CONFIRMATION_MESSAGE_FROM_STATUSPAGE + "=2";
                }
                Response.Redirect(url);
            }
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


    private string GetUrlForBankImportPage()
    {
        return "OsDepositBankNsfImport.aspx";
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        Helper.SetBreadcrumbs(this, 2498, 2497);
    }

    private void SendMailToUser(string msg, string Subject)
    {
        try
        {

            StringBuilder oMailBody = new StringBuilder();
            oMailBody.Append(msg);
            //oMailBody.Append(string.Format(LanguageUtil.GetValue(2456), LanguageUtil.GetValue(DataImportTypeLabelID), Helper.GetUserFullName (SessionHelper .CurrentUserID)));
            oMailBody.Append("<br>");
            oMailBody.Append("<br>");

            string fromAddress = AppSettingHelper.GetAppSettingValue(AppSettingConstants.EMAIL_FROM_DEFAULT);
            if (SessionHelper.CurrentRoleEnum == ARTEnums.UserRole.SKYSTEM_ADMIN)
            {
                oMailBody.Append("<br/>" + MailHelper.GetEmailSignature(WebEnums.SignatureEnum.SendBySkyStemSystem, fromAddress));
            }
            else
            {
                oMailBody.Append("<br/>" + MailHelper.GetEmailSignature(WebEnums.SignatureEnum.SendBySystemAdmin, fromAddress));
            }

            string mailSubject = Subject;

            string toAddress = oDataImportHdrInfo.EmailID;
            MailHelper.SendEmail(fromAddress, toAddress, mailSubject, oMailBody.ToString());
        }
        catch (Exception ex)
        {
            Helper.FormatAndShowErrorMessage(null, ex);
        }
    }
}
