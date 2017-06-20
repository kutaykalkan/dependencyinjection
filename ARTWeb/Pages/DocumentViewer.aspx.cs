using SkyStem.ART.Shared.Utility;
using SkyStem.ART.Web.Classes;
using SkyStem.ART.Web.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_DocumentViewer : PopupPageBase
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
        string strRequestedDoc = null;
        //if (!string.IsNullOrEmpty(Request.QueryString[QueryStringConstants.FILE_PATH]))
        //    strRequestedDoc = Request.QueryString[QueryStringConstants.FILE_PATH];
        if (Session[SessionConstants.RECFORM_PRINT_PDF_FILE] != null)
            strRequestedDoc = (string)Session[SessionConstants.RECFORM_PRINT_PDF_FILE];
        if (strRequestedDoc != null)
        {
            // string downloadUrl = this.ResolveUrl("~/Pages/DownloadAttachment.aspx?" + QueryStringConstants.FILE_PATH + "=" + Server.UrlEncode(SharedHelper.GetDisplayFilePath(strRequestedDoc)) + "&" + QueryStringConstants.DOWNLOAD_MODE + "=" + WebEnums.DownloadMode.inline);
            Session[SessionConstants.DIRECT_DOWNLOAD_FILE] = strRequestedDoc;
            string downloadUrl = string.Format("Downloader?{0}={1}&", QueryStringConstants.HANDLER_ACTION, (Int32)WebEnums.HandlerActionType.DownloadDirect) + "&" + QueryStringConstants.DOWNLOAD_MODE + "=" + WebEnums.DownloadMode.inline;
            //string strPrint = "javascript:OpenPrintRadWindowFromRadWindow('" + downloadUrl + "', " + WebConstants.PRINT_POPUP_HEIGHT + ", " + WebConstants.PRINT_POPUP_WIDTH + ");";
            //if (!Page.ClientScript.IsStartupScriptRegistered("OpenDocumentViewer"))
            //    Page.ClientScript.RegisterStartupScript(Page.GetType(), "OpenDocumentViewer", strPrint, true);
            rwDocument.NavigateUrl = downloadUrl;
            rwDocument.Width = WebConstants.PRINT_POPUP_WIDTH;
            rwDocument.Height = WebConstants.PRINT_POPUP_HEIGHT;
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
    #endregion
    #region Other Methods
    #endregion




}