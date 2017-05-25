using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using SkyStem.ART.Client.Exception;
using SkyStem.ART.Client.IServices;
using SkyStem.ART.Client.Model;
using SkyStem.ART.Web.Data;
using SkyStem.ART.Web.Utility;
using SkyStem.Language.LanguageUtility;
using System.IO;

namespace SkyStem.ART.Web.Utility
{

    /// <summary>
    /// Summary description for HttpDownloader
    /// </summary>
    public class HttpDownloader : IHttpHandler, IRequiresSessionState
    {
        #region IHttpHandler Members

        public bool IsReusable
        {
            get { return false; }
        }

        public void ProcessRequest(HttpContext context)
        {
            try
            {
                if (!HttpContext.Current.User.Identity.IsAuthenticated)
                    throw new AccessViolationException("User is not authenticated and is attempting to download a file.");

                string sessionID = context.Request.QueryString[QueryStringConstants.SESSION_ID];
                string filename = context.Request.QueryString[QueryStringConstants.FILE_NAME];
                var data = context.Session[sessionID] as System.Data.DataTable;
                if (data != null)
                {
                    MatchingHelper.ExportToExcel(data, filename);
                }
                if (!string.IsNullOrEmpty(context.Request.QueryString[QueryStringConstants.HANDLER_ACTION]))
                {
                    int handlerAction;
                    if (Int32.TryParse(context.Request.QueryString[QueryStringConstants.HANDLER_ACTION], out handlerAction))
                        ProcessHandlerActions(handlerAction, context);
                }
            }
            catch (ARTException ex)
            {
                string errMsg = LanguageUtil.GetValue(ex.ExceptionPhraseID);
                Helper.LogInfo(errMsg);
                context.Response.Write(ScriptHelper.GetJSForDisplayErrorMessage(errMsg));
            }
            catch (Exception ex)
            {
                Helper.LogException(ex);
                context.Response.Write(ScriptHelper.GetJSForDisplayErrorMessage(ex.Message));
            }
            finally
            {
                context.Response.End();
            }
        }

        protected void ProcessHandlerActions(int handlerAction, HttpContext context)
        {
            if (handlerAction > 0)
            {
                WebEnums.HandlerActionType eHandlerAction = (WebEnums.HandlerActionType)handlerAction;
                switch (eHandlerAction)
                {
                    case WebEnums.HandlerActionType.DownloadRecAttachments:
                        DownloadAttachments(context);
                        break;
                    case WebEnums.HandlerActionType.DownloadDataImportFile:
                        DownloadDataImportFile(context);
                        break;
                }
            }
        }

        protected void DownloadAttachments(HttpContext context)
        {
            if (!string.IsNullOrEmpty(context.Request.QueryString[QueryStringConstants.GLDATA_ID]))
            {
                int userID = SessionHelper.CurrentUserID.GetValueOrDefault();
                short roleID = SessionHelper.CurrentRoleID.GetValueOrDefault();
                long? glDataID = Convert.ToInt64(context.Request.QueryString[QueryStringConstants.GLDATA_ID]);
                long? accountID = Convert.ToInt64(context.Request.QueryString[QueryStringConstants.ACCOUNT_ID]);
                Int32? netAccountID = Convert.ToInt32(context.Request.QueryString[QueryStringConstants.NETACCOUNT_ID]);
                short recTemplateID = Convert.ToInt16(context.Request.QueryString[QueryStringConstants.REC_TEMPLATE_ID]);
                string archiveFileName = ExportHelper.GenerateTempFilePath(LanguageUtil.GetValue(2809), "zip");
                // Add Detailed PDF
                byte[] oByteArray = SSRSReportsHelper.GeneratePDFBytes(glDataID, netAccountID, accountID, recTemplateID);
                string accountDetail = Helper.GetAccountDisplayString(accountID, netAccountID);
                string fileName = ExportHelper.GenerateTempFileName(accountDetail, "pdf");
                string filePath = SSRSReportsHelper.SaveByteStreamAndReturnFilePath(fileName, oByteArray);
                List<string> strFiles = new List<string>();
                strFiles.Add(filePath);
                // Add Attachments
                IAttachment oAttachmentClient = RemotingHelper.GetAttachmentObject();
                List<AttachmentInfo> oAttachmentInfoList = oAttachmentClient.GetAllAttachmentForGL(glDataID, userID, roleID, Helper.GetAppUserInfo());
                if (oAttachmentInfoList != null && oAttachmentInfoList.Count > 0)
                {
                    oAttachmentInfoList.ForEach(T => strFiles.Add(T.PhysicalPath));
                }
                ExportHelper.CreateZipFromFiles(archiveFileName, strFiles);
                ExportHelper.DownloadAttachment(archiveFileName, ExportHelper.GetOriginalFileName(archiveFileName), true);
            }
        }

        protected void DownloadDataImportFile(HttpContext context)
        {
            if (!string.IsNullOrEmpty(context.Request.QueryString[QueryStringConstants.DATA_IMPORT_ID]))
            {
                int userID = SessionHelper.CurrentUserID.GetValueOrDefault();
                short roleID = SessionHelper.CurrentRoleID.GetValueOrDefault();
                int? dataImportID = Convert.ToInt32(context.Request.QueryString[QueryStringConstants.DATA_IMPORT_ID]);
                short? dataImportTypeID = Convert.ToInt16(context.Request.QueryString[QueryStringConstants.DATA_IMPORT_TYPE_ID]);
                IDataImport oDataImport = RemotingHelper.GetDataImportObject();
                DataImportHdrInfo oDataImportHdrInfo = oDataImport.GetDataImportInfo(dataImportID, Helper.GetAppUserInfo());
                if (oDataImportHdrInfo == null)
                    throw new ARTException(5000206);
                DownloadFile(oDataImportHdrInfo.PhysicalPath, context);
            }
        }

        private void DownloadFile(string strRequestedDoc, HttpContext context)
        {
            // Put user code to initialize the page here
            //string strDocPath = Helper.GetFolderForAttachment(SessionHelper.CurrentCompanyID.Value, SessionHelper.CurrentReconciliationPeriodID.Value);
            string ContentType;
            string strResponsePath;
            string filePhysicalPath = "";
            //string strRequestedDoc = HttpContext.Current.Server.UrlDecode(Request.QueryString[QueryStringConstants.FILE_PATH]);
            WebEnums.DownloadMode eDownloadMode = WebEnums.DownloadMode.attachment;
            if (!string.IsNullOrEmpty(context.Request.QueryString[QueryStringConstants.DOWNLOAD_MODE]))
            {
                eDownloadMode = (WebEnums.DownloadMode)Enum.Parse(typeof(WebEnums.DownloadMode), context.Request.QueryString[QueryStringConstants.DOWNLOAD_MODE]);
            }
            //filePhysicalPath = strDocPath + "/" + strRequestedDoc;
            filePhysicalPath = strRequestedDoc;
            if (strRequestedDoc != null)
            {
                FileInfo objFileInfo = new FileInfo(filePhysicalPath);

                ContentType = ExportHelper.GetContentType(objFileInfo.Extension);

                if (objFileInfo.Exists)
                {
                    strResponsePath = objFileInfo.FullName;
                    // if the file for the requested media exists on the disk
                    // send the headers to the client's browser

                    if (eDownloadMode == WebEnums.DownloadMode.inline)
                    {
                        context.Response.AddHeader("Content-Disposition", "inline; filename=" + ExportHelper.GetOriginalFileName(objFileInfo.Name));
                        context.Response.AddHeader("Content-Length", objFileInfo.Length.ToString());
                        context.Response.ContentType = ContentType;
                        context.Response.WriteFile(strResponsePath);
                    }
                    else
                    {
                        context.Response.AddHeader("Content-Disposition", "attachment; filename=" + ExportHelper.GetOriginalFileName(objFileInfo.Name));
                        context.Response.AddHeader("Content-Length", objFileInfo.Length.ToString());
                        context.Response.ContentType = ContentType;
                        context.Response.TransmitFile(strResponsePath);
                    }
                    context.Response.End();
                }
                else
                {
                    string responseScript = string.Empty;
                    responseScript += "<script language='javascript'>";
                    responseScript += "alert('" + LanguageUtil.GetValue(5000206) + "');";
                    responseScript += "document.location.href='" + HttpContext.Current.Request.UrlReferrer + "';";
                    responseScript += "</script>";
                    context.Response.Write(responseScript);
                }

            }
            else
                throw new ARTSystemException(5000030);
        }
        #endregion
    }
}