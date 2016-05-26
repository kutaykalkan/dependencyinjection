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

        protected void ProcessHandlerActions(int handlerAction, HttpContext context)
        {
            try
            {
                if (handlerAction > 0)
                {
                    WebEnums.HandlerActionType eHandlerAction = (WebEnums.HandlerActionType)handlerAction;
                    switch (eHandlerAction)
                    {
                        case WebEnums.HandlerActionType.DownloadRecAttachments:
                            DownloadAttachments(context);
                            break;
                    }
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

        #endregion
    }
}