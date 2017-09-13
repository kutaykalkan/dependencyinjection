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
using System.IO;
using SkyStem.ART.Client.Exception;
using SkyStem.ART.Web.Utility;
using SkyStem.ART.Web.Data;
using SkyStem.Language.LanguageUtility;
using SkyStem.ART.Web.Classes;

public partial class Pages_DownloadAttachment : System.Web.UI.Page
{
    #region Variables & Constants
    #endregion
    #region Properties
    #endregion
    #region Delegates & Events
    #endregion
    #region Page Events

    protected void Page_Init(object sender, EventArgs e)
    {
        // Check for Http Referrer
        Helper.CheckReferrer();

        // Check for Session
        SessionHelper.CheckSessionForUser();
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!HttpContext.Current.User.Identity.IsAuthenticated)
            throw new AccessViolationException("User is not authenticated and is attempting to download a file.");

        /* No download allowed using this page
        // Put user code to initialize the page here
        //string strDocPath = Helper.GetFolderForAttachment(SessionHelper.CurrentCompanyID.Value, SessionHelper.CurrentReconciliationPeriodID.Value);
        string ContentType;
        string strResponsePath;
        string filePhysicalPath = "";
        //string strRequestedDoc = HttpContext.Current.Server.UrlDecode(Request.QueryString[QueryStringConstants.FILE_PATH]);
        string strRequestedDoc = Request.QueryString[QueryStringConstants.FILE_PATH];
        WebEnums.DownloadMode eDownloadMode = WebEnums.DownloadMode.attachment;
        if (!string.IsNullOrEmpty(Request.QueryString[QueryStringConstants.DOWNLOAD_MODE]))
        {
            eDownloadMode = (WebEnums.DownloadMode)Enum.Parse(typeof(WebEnums.DownloadMode), Request.QueryString[QueryStringConstants.DOWNLOAD_MODE]);
        }
        //filePhysicalPath = strDocPath + "/" + strRequestedDoc;
        filePhysicalPath = strRequestedDoc;
        if (strRequestedDoc != null)
        {
            string basePath = SharedDataImportHelper.GetBaseFolder();
            if (!filePhysicalPath.Contains(":") && !filePhysicalPath.Contains(basePath))
            {
                filePhysicalPath = basePath + Path.DirectorySeparatorChar + filePhysicalPath;
            }
            FileInfo objFileInfo = new FileInfo(filePhysicalPath);

            ContentType = ExportHelper.GetContentType(objFileInfo.Extension);

            if (objFileInfo.Exists)
            {
                strResponsePath = objFileInfo.FullName;
                // if the file for the requested media exists on the disk
                // send the headers to the client's browser

                if (eDownloadMode == WebEnums.DownloadMode.inline)
                {
                    Response.AddHeader("Content-Disposition", "inline; filename=" + ExportHelper.GetOriginalFileName(objFileInfo.Name));
                    Response.AddHeader("Content-Length", objFileInfo.Length.ToString());
                    Response.ContentType = ContentType;
                    Response.WriteFile(strResponsePath);
                }
                else
                {
                    Response.AddHeader("Content-Disposition", "attachment; filename=" + ExportHelper.GetOriginalFileName(objFileInfo.Name));
                    Response.AddHeader("Content-Length", objFileInfo.Length.ToString());
                    Response.ContentType = ContentType;
                    Response.TransmitFile(strResponsePath);
                }
                Response.End();
            }
            else
            {
                string responseScript = string.Empty;
                responseScript += "<script language='javascript'>";
                responseScript += "alert('" + LanguageUtil.GetValue(5000206) + "');";
                //if (!string.IsNullOrEmpty(Request.QueryString[QueryStringConstants.FROM_PAGE]) && Convert.ToInt16(Request.QueryString[QueryStringConstants.FROM_PAGE]) == (short)WebEnums.ARTPages.RequestStatus)
                //    responseScript += "document.location.href='RequestStatus.aspx';";
                //else
                //    responseScript += "document.location.href='DataImportStatus.aspx';";
                responseScript += "document.location.href='" + HttpContext.Current.Request.UrlReferrer + "';";
                responseScript += "</script>";
                Response.Write(responseScript);
                //throw new ARTSystemException(5000030);
            }

        }
        else
            throw new ARTSystemException(5000030);
        */
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
