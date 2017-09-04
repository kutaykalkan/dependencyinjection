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
using SkyStem.Language.LanguageUtility;
using ExpertPdf.HtmlToPdf;
using SkyStem.ART.Client.Exception;
using System.IO;
using Microsoft.Reporting.WebForms;
using SkyStem.ART.Client.Data;

public partial class Pages_PopupForRecFormPrint : PopupPageBase
{
    Int32? _PageTitelID = null;
    Int64? _AccountID = null;
    Int32? _NetAccountID = null;
    long? _GLDataID = null;

    bool _IsDetailedForm = false;
    bool _IsPDF = false;
    bool _IsEmail = false;

    protected void Page_Load(object sender, EventArgs e)
    {
        GetQueryStringValues();
        if (!Page.IsPostBack)
        {
            SetPageTitle();
            tempUrlreffer.Text = Request.UrlReferrer.ToString();
        }
    }

    private void GetQueryStringValues()
    {
        if (!string.IsNullOrEmpty(Request.QueryString[QueryStringConstants.PAGE_TITLE_ID]))
            _PageTitelID = Convert.ToInt32(Request.QueryString[QueryStringConstants.PAGE_TITLE_ID]);

        if (!string.IsNullOrEmpty(Request.QueryString[QueryStringConstants.GLDATA_ID]))
            _GLDataID = Convert.ToInt64(Request.QueryString[QueryStringConstants.GLDATA_ID]);

        if (!string.IsNullOrEmpty(Request.QueryString[QueryStringConstants.ACCOUNT_ID]))
            _AccountID = Convert.ToInt64(Request.QueryString[QueryStringConstants.ACCOUNT_ID]);

        if (!string.IsNullOrEmpty(Request.QueryString[QueryStringConstants.NETACCOUNT_ID]))
            _NetAccountID = Convert.ToInt32(Request.QueryString[QueryStringConstants.NETACCOUNT_ID]);

        if (Request.QueryString["IsPDF"] != null)
            _IsPDF = Convert.ToBoolean(Convert.ToInt16(Request.QueryString["IsPDF"]));

        if (Request.QueryString["IsEmail"] != null)
            _IsEmail = Convert.ToBoolean(Convert.ToInt16(Request.QueryString["IsEmail"]));
    }

    void SetPageTitle()
    {
        if (_IsPDF)
        {
            PopupHelper.SetPageTitle(this, 1405);
        }
        else if (_IsEmail)
        {
            PopupHelper.SetPageTitle(this, 1311);
        }
        else
        {
            PopupHelper.SetPageTitle(this, 1406);
        }
    }

    protected void btnOk_Click(object sender, EventArgs e)
    {
        string accountDetails = Helper.GetAccountDisplayString(_AccountID, _NetAccountID);
        string fileName = ExportHelper.GenerateTempFileName(accountDetails, ".pdf");

        byte[] oByteArray = GeneratePDF(accountDetails);

        if (_IsPDF)
        {
            DownloadPDFAsAttachment(fileName, oByteArray);
            this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "return ClosePopupWithoutRefreshParentPage", ScriptHelper.GetJSForPopupClose());
        }
        else if (_IsEmail)
        {
            DisplayEmailPopup(fileName, accountDetails, oByteArray);
        }
        else
        {
            DisplayPDFInline(fileName, oByteArray);
        }
    }

    private void DisplayEmailPopup(string fileName, string accountDetails, byte[] oByteArray)
    {
        string filePath = SSRSReportsHelper.SaveByteStreamAndReturnFilePath(fileName, oByteArray);

        Session[SessionConstants.REC_FORM_PRINT_PAGE_URL] = GetUrlForPdf();
        string url = "EmailPopupForRecForm.aspx?";
        url = url + "&" + QueryStringConstants.PAGE_TITLE_ID + "=" + Server.UrlEncode(Request.QueryString[QueryStringConstants.PAGE_TITLE_ID])
            + "&" + QueryStringConstants.EMAIL_INFO_SPECIFIC + "=" + Server.UrlEncode(GetPdfFooter(accountDetails))
            + "&" + QueryStringConstants.GLDATA_ID + "=" + _GLDataID.GetValueOrDefault().ToString()
            + "&" + QueryStringConstants.FILE_PATH + "=" + Server.UrlEncode(filePath);
        Response.Redirect(url);
    }

    private string GetUrlForPdf()
    {
        string url = "";
        url = tempUrlreffer.Text;
        int InsertIndex = url.IndexOf("?") - 5;
        url = url.Insert(InsertIndex, "Print");

        url = url.Insert(url.LastIndexOf("/"), "/RecFormPrint");
        url = url.Substring(url.LastIndexOf("Pages") + 6);
        url.Insert(0, "../");
        url = url + "&" + QueryStringConstants.PAGE_TITLE_ID + "=" + _PageTitelID.ToString();
        if (optSummary.Checked)
        {
            url = url + "&" + QueryStringConstants.IS_SUMMARY + "=1";
            _IsDetailedForm = false;
        }
        else
        {
            url = url + "&" + QueryStringConstants.IS_SUMMARY + "=0";
            _IsDetailedForm = true;
        }
        return url;
    }

    private byte[] GeneratePDF(string accountDetails)
    {
        byte[] oByteArray = null;
        try
        {
            string pageFooter = GetPdfFooter(accountDetails);
            string pageTitle = LanguageUtil.GetValue(_PageTitelID.Value);

            string url = GetUrlForPdf();
            if (_IsDetailedForm)
            {
                oByteArray = SSRSReportsHelper.GeneratePDFBytes(_GLDataID, _NetAccountID, _AccountID, GetReconciliationTemplateID());
            }
            else
            {
                TextWriter oTextWriter = new StringWriter();
                Server.Execute(url, oTextWriter);
                oByteArray = ExportHelper.GeneratePDFBytes(pageTitle, pageFooter, oTextWriter.ToString(), true);
            }
        }
        catch (ARTException ex)
        {
            PopupHelper.ShowErrorMessage(this, ex);
        }
        catch (Exception ex)
        {
            Helper.LogException(ex);
            PopupHelper.ShowErrorMessage(this, ex);
        }
        return oByteArray;
    }

    protected String GetPdfFooter(string AccountDetails)
    {
        AccountDetails = string.Format("{0}. {1}: {2}", AccountDetails, LanguageUtil.GetValue(1420), Helper.GetDisplayDate(SessionHelper.CurrentReconciliationPeriodEndDate));
        return AccountDetails;
    }

    public void DownloadPDFAsAttachment(string fileName, byte[] oByteCollection)
    {
        ExportHelper.SendPDFByteStreamForDownload(fileName, oByteCollection,true);
    }

    public void DisplayPDFInline(string fileName, byte[] oByteCollection)
    {
        string filePath = SSRSReportsHelper.SaveByteStreamAndReturnFilePath(fileName, oByteCollection);
        Session[SessionConstants.RECFORM_PRINT_PDF_FILE] = filePath;
        string downloadURL = this.ResolveClientUrl("~/pages/DocumentViewer.aspx?" + QueryStringConstants.DOWNLOAD_MODE + "=" + WebEnums.DownloadMode.inline);
        Response.Redirect(downloadURL);
    }

    public short GetReconciliationTemplateID()
    {
        return ((short?)((ARTEnums.ReconciliationItemTemplate)Session[SessionConstants.REC_ITEM_TEMPLATE_ID])).GetValueOrDefault();
    }
}
