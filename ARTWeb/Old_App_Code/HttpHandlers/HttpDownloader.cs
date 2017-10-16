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
using SkyStem.ART.Client.Data;
using SkyStem.ART.Client.Params;
using SkyStem.ART.Client.Params.Matching;
using SkyStem.ART.Client.Model.Matching;
using SkyStem.ART.Client.Model.BulkExportExcel;
using System.Threading;

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
            catch (ThreadAbortException)
            { }
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
                    case WebEnums.HandlerActionType.DownloadRecsAndAttachmentsFile:
                        DownloadRecAndAttachments(context);
                        break;
                    case WebEnums.HandlerActionType.DownloadDataImportFile:
                        DownloadDataImportFile(context);
                        break;
                    case WebEnums.HandlerActionType.DownloadMatchingImportFile:
                        DownloadMatchingDataImportFile(context);
                        break;
                    case WebEnums.HandlerActionType.DownloadGLAttachmentFile:
                        DownloadGLAttachmentFile(context);
                        break;
                    case WebEnums.HandlerActionType.DownloadTaskAttachmentFile:
                        DownloadTaskAttachmentFile(context);
                        break;
                    case WebEnums.HandlerActionType.DownloadDataImportTemplateFile:
                        DownloadDataImportTemplateFile(context);
                        break;
                    case WebEnums.HandlerActionType.DownloadRequestFile:
                        DownloadRequestFile(context);
                        break;
                    case WebEnums.HandlerActionType.DownloadCompanyLogo:
                        DownloadCompanyLogo(context);
                        break;
                    case WebEnums.HandlerActionType.DownloadDirect:
                        DownloadDirect(context);
                        break;
                }
            }
        }

        protected void DownloadDirect(HttpContext context)
        {
            if (context.Session[SessionConstants.DIRECT_DOWNLOAD_FILE] != null)
            {
                string filePath = Convert.ToString(context.Session[SessionConstants.DIRECT_DOWNLOAD_FILE]);
                if (filePath == null)
                    throw new ARTException(5000206);
                DownloadFile(filePath, context);
            }
        }
        protected void DownloadRecAndAttachments(HttpContext context)
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
                long? glDataID = Convert.ToInt64(context.Request.QueryString[QueryStringConstants.GLDATA_ID]);
                long? taskID = Convert.ToInt64(context.Request.QueryString[QueryStringConstants.TASK_ID]);
                DataImportParamInfo oDataImportParamInfo = new DataImportParamInfo()
                {
                    DataImportID = dataImportID,
                    DataImportTypeID = dataImportTypeID,
                    GLDataID = glDataID,
                    TaskID = taskID
                };
                IDataImport oDataImport = RemotingHelper.GetDataImportObject();
                DataImportHdrInfo oDataImportHdrInfo = oDataImport.GetAccessibleDataImportInfo(oDataImportParamInfo, Helper.GetAppUserInfo());
                if (oDataImportHdrInfo == null)
                    throw new ARTException(5000206);
                DownloadFile(oDataImportHdrInfo.PhysicalPath, context);
            }
        }

        protected void DownloadMatchingDataImportFile(HttpContext context)
        {
            if (!string.IsNullOrEmpty(context.Request.QueryString[QueryStringConstants.MATCHING_SOURCE_DATA_IMPORT_ID]))
            {
                int userID = SessionHelper.CurrentUserID.GetValueOrDefault();
                short roleID = SessionHelper.CurrentRoleID.GetValueOrDefault();
                int? matchingSourceDataImportID = Convert.ToInt32(context.Request.QueryString[QueryStringConstants.MATCHING_SOURCE_DATA_IMPORT_ID]);
                short? matchingSourceTypeID = Convert.ToInt16(context.Request.QueryString[QueryStringConstants.MATCHING_SOURCE_TYPE_ID]);
                long? glDataID = Convert.ToInt64(context.Request.QueryString[QueryStringConstants.GLDATA_ID]);
                MatchingParamInfo oMatchingParamInfo = new MatchingParamInfo()
                {
                    MatchingSourceDataImportID = matchingSourceDataImportID,
                    MatchingSourceTypeID = matchingSourceTypeID,
                    GLDataID = glDataID
                };
                IMatching oMatching = RemotingHelper.GetMatchingObject();
                MatchingSourceDataImportHdrInfo oMatchingSourceDataImportHdrInfo = oMatching.GetMatchingSourceDataImportInfo(oMatchingParamInfo, Helper.GetAppUserInfo());
                if (oMatchingSourceDataImportHdrInfo == null)
                    throw new ARTException(5000206);
                DownloadFile(oMatchingSourceDataImportHdrInfo.PhysicalPath, context);
            }
        }

        protected void DownloadCompanyLogo(HttpContext context)
        {
            ICompany oCompanyClient = RemotingHelper.GetCompanyObject();
            string logoPath = oCompanyClient.GetCompanyLogo(SessionHelper.CurrentCompanyID, Helper.GetAppUserInfo());
            if (!string.IsNullOrEmpty(logoPath))
            {
                DownloadFile(logoPath, context);
            }
        }

        protected void DownloadRequestFile(HttpContext context)
        {
            if (!string.IsNullOrEmpty(context.Request.QueryString[QueryStringConstants.REQUEST_ID]))
            {
                int userID = SessionHelper.CurrentUserID.GetValueOrDefault();
                short roleID = SessionHelper.CurrentRoleID.GetValueOrDefault();
                int companyID = SessionHelper.CurrentCompanyID.GetValueOrDefault();
                int? requestID = Convert.ToInt32(context.Request.QueryString[QueryStringConstants.REQUEST_ID]);
                short? requestTypeID = Convert.ToInt16(context.Request.QueryString[QueryStringConstants.REQUEST_TYPE_ID]);
                BulkExportToExcelInfo oBulkExportToExcelInfo = null;
                List<BulkExportToExcelInfo> oBulkExportToExcelInfoLst = RequestHelper.GetRequests(new List<short> { requestTypeID.GetValueOrDefault() });
                if (oBulkExportToExcelInfoLst != null && oBulkExportToExcelInfoLst.Count > 0)
                {
                    oBulkExportToExcelInfo = oBulkExportToExcelInfoLst.Find(T => T.RequestID == requestID);
                }
                if (oBulkExportToExcelInfo == null)
                    throw new ARTException(5000206);
                DownloadFile(oBulkExportToExcelInfo.PhysicalPath, context);
            }
        }
        protected void DownloadDataImportTemplateFile(HttpContext context)
        {
            if (!string.IsNullOrEmpty(context.Request.QueryString[QueryStringConstants.IMPORT_TEMPLATE_ID]))
            {
                int userID = SessionHelper.CurrentUserID.GetValueOrDefault();
                short roleID = SessionHelper.CurrentRoleID.GetValueOrDefault();
                int companyID = SessionHelper.CurrentCompanyID.GetValueOrDefault();
                int? importTemplateID = Convert.ToInt32(context.Request.QueryString[QueryStringConstants.IMPORT_TEMPLATE_ID]);
                short? dataImportTypeID = Convert.ToInt16(context.Request.QueryString[QueryStringConstants.DATA_IMPORT_TYPE_ID]);
                ImportTemplateHdrInfo oImportTemplateHdrInfo = null;
                List<ImportTemplateHdrInfo> oImportTemplateInfoLst = DataImportTemplateHelper.GetAllTemplateImport(companyID, userID, roleID);
                if (oImportTemplateInfoLst != null && oImportTemplateInfoLst.Count > 0)
                {
                    oImportTemplateHdrInfo = oImportTemplateInfoLst.Find(T => T.ImportTemplateID == importTemplateID);
                }
                if (oImportTemplateHdrInfo == null)
                    throw new ARTException(5000206);
                DownloadFile(oImportTemplateHdrInfo.PhysicalPath, context);
            }
        }
        protected void DownloadGLAttachmentFile(HttpContext context)
        {
            if (!string.IsNullOrEmpty(context.Request.QueryString[QueryStringConstants.GENERIC_ID])
                && !string.IsNullOrEmpty(context.Request.QueryString[QueryStringConstants.GLDATA_ID]))
            {
                int userID = SessionHelper.CurrentUserID.GetValueOrDefault();
                short roleID = SessionHelper.CurrentRoleID.GetValueOrDefault();
                long? glDataID = Convert.ToInt64(context.Request.QueryString[QueryStringConstants.GLDATA_ID]);
                int recPeriodID = SessionHelper.CurrentReconciliationPeriodID.GetValueOrDefault();
                long? recordID = Convert.ToInt32(context.Request.QueryString[QueryStringConstants.RECORD_ID]);
                int? recordTypeID = Convert.ToInt16(context.Request.QueryString[QueryStringConstants.RECORD_TYPE_ID]);
                long? id = Convert.ToInt32(context.Request.QueryString[QueryStringConstants.GENERIC_ID]);
                List<AttachmentInfo> oAttachmentInfoList = null;
                AttachmentInfo oAttachmentInfo = null;
                if (recordID.GetValueOrDefault() > 0 && recordTypeID.GetValueOrDefault() > 0)
                {
                    AppUserInfo oAppUserInfo = Helper.GetAppUserInfo();
                    IGLData oGLData = RemotingHelper.GetGLDataObject();
                    if (oGLData.CheckGLPermissions(glDataID, userID, roleID, oAppUserInfo))
                    {
                        IAttachment oAttachment = RemotingHelper.GetAttachmentObject();
                        oAttachmentInfoList = oAttachment.GetAttachment(recordID.Value, recordTypeID.Value, recPeriodID, oAppUserInfo);
                    }
                }
                else
                {
                    oAttachmentInfoList = (List<AttachmentInfo>)context.Session[SessionConstants.ATTACHMENTS];
                }
                if (oAttachmentInfoList != null && oAttachmentInfoList.Count > 0)
                {
                    oAttachmentInfo = oAttachmentInfoList.Find(T => T.AttachmentID == id);
                }
                if (oAttachmentInfo == null)
                    throw new ARTException(5000206);
                DownloadFile(oAttachmentInfo.PhysicalPath, context);
            }
        }

        protected void DownloadTaskAttachmentFile(HttpContext context)
        {
            if (!string.IsNullOrEmpty(context.Request.QueryString[QueryStringConstants.GENERIC_ID])
                && !string.IsNullOrEmpty(context.Request.QueryString[QueryStringConstants.TASK_ID]))
            {
                int userID = SessionHelper.CurrentUserID.GetValueOrDefault();
                short roleID = SessionHelper.CurrentRoleID.GetValueOrDefault();
                long? taskID = Convert.ToInt64(context.Request.QueryString[QueryStringConstants.TASK_ID]);
                short? taskType = Convert.ToInt16(context.Request.QueryString[QueryStringConstants.TASK_TYPE_ID]);
                int recPeriodID = SessionHelper.CurrentReconciliationPeriodID.GetValueOrDefault();
                long? recordID = Convert.ToInt32(context.Request.QueryString[QueryStringConstants.RECORD_ID]);
                int? recordTypeID = Convert.ToInt16(context.Request.QueryString[QueryStringConstants.RECORD_TYPE_ID]);
                long? id = Convert.ToInt32(context.Request.QueryString[QueryStringConstants.GENERIC_ID]);
                List<AttachmentInfo> oAttachmentInfoList = null;
                AttachmentInfo oAttachmentInfo = null;
                if (recordID.GetValueOrDefault() > 0 && recordTypeID.GetValueOrDefault() > 0)
                {
                    IAttachment oAttachment = RemotingHelper.GetAttachmentObject();
                    oAttachmentInfoList = oAttachment.GetAllAttachmentForTask(taskID, (ARTEnums.TaskType)taskType, Helper.GetAppUserInfo());
                }
                else
                {
                    oAttachmentInfoList = (List<AttachmentInfo>)context.Session[SessionConstants.TASK_MASTER_ATTACHMENT];
                }
                if (oAttachmentInfoList != null && oAttachmentInfoList.Count > 0)
                {
                    oAttachmentInfo = oAttachmentInfoList.Find(T => T.AttachmentID == id);
                }
                if (oAttachmentInfo == null)
                    throw new ARTException(5000206);
                DownloadFile(oAttachmentInfo.PhysicalPath, context);
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