using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using ARTExportToExcelApp.APP.DAO;
using SkyStem.ART.Service.Model;
using SkyStem.ART.Service.Utility;
using System.IO;
using System.Xml;
using SkyStem.ART.Service.Data;
using SkyStem.ART.Client.Model.CompanyDatabase;
using SkyStem.ART.Shared.Utility;
using Microsoft.Reporting.WinForms;
using SkyStem.ART.Service.DAO;
using SkyStem.ART.Shared.Data;
using System.IO.Compression;
using SkyStem.ART.Client.IServices;
using ClientModel = SkyStem.ART.Client.Model;

namespace SkyStem.ART.Service.APP.BLL
{
    /// <summary>
    /// 
    /// </summary>
    public class ExportToExcel
    {
        List<ExportToExcelInfo> _objExportInfo = null;
        CompanyUserInfo CompanyUserInfo;

        public ExportToExcel(CompanyUserInfo oCompanyUserInfo)
        {
            this.CompanyUserInfo = oCompanyUserInfo;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<ExportToExcelInfo> GetDataToExport()
        {
            ExportToExcelDAO objExportDAO = new ExportToExcelDAO(this.CompanyUserInfo);
            return objExportDAO.GetDataForExport();
        }

        public void InsertAttachmentDetails(ExportToExcelParamInfo exportParamInfo)
        {
            ExportToExcelDAO objExportToExcelDAO = new ExportToExcelDAO(this.CompanyUserInfo);
            objExportToExcelDAO.InsertAttachmentData(exportParamInfo, this.CompanyUserInfo);
        }

        public bool IsProcessingRequiredForRequests()
        {
            bool isExportRequired = false;
            try
            {
                ExportToExcelDAO objExportDAO = new ExportToExcelDAO(this.CompanyUserInfo);
                List<ExportToExcelInfo> objExport = objExportDAO.GetDataForExport();
                if (objExport != null && objExport.Count > 0)
                {
                    _objExportInfo = objExport;
                    isExportRequired = true;
                }
            }
            catch (Exception ex)
            {
                isExportRequired = false;
                _objExportInfo = null;
                Helper.LogError(@"Error in IsProcessingRequiredForExportToExcel: " + ex.Message, this.CompanyUserInfo);

            }

            return isExportRequired;
        }

        public void ProcessRequests()
        {
            List<ExportToExcelInfo> objExportDataInfoList = _objExportInfo;

            //Convert DataTable to Excel
            //ExportToExcelHelper objExportHelper = new ExportToExcelHelper();
            foreach (ExportToExcelInfo objExportInfo in objExportDataInfoList)
            {
                List<ClientModel.LogInfo> oLogInfoCache = new List<ClientModel.LogInfo>();
                try
                {
                    DataSet ds = new DataSet();
                    ExcelHelper.LoadXmlToDataSet(ds, objExportInfo.Data);
                    switch (objExportInfo.RequestTypeID)
                    {
                        case 1:
                            ProcessExportToExcel(ds, objExportInfo, oLogInfoCache);
                            break;
                        case 2:
                        case 3:
                        case 4:
                            ProcessDownloadAllRecs(ds, objExportInfo, oLogInfoCache);
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Helper.LogErrorToCache(ex, oLogInfoCache);
                }
                finally
                {
                    try
                    {
                        UpdateRequestStatus(objExportInfo);
                        Helper.LogListViaService(oLogInfoCache, null, this.CompanyUserInfo);
                    }
                    catch (Exception ex)
                    {
                        Helper.LogError(ex, this.CompanyUserInfo);
                    }
                }
            }
        }

        private void ProcessExportToExcel(DataSet ds, ExportToExcelInfo objExportInfo, List<ClientModel.LogInfo> oLogInfoCache)
        {
            DataTable dtRequestHdr = ds.Tables[SearchAndEmailConstants.HeaderTableName];
            DataTable dtExportDataInfo = ds.Tables[SearchAndEmailConstants.DetailTableName];

            if (dtRequestHdr != null && dtRequestHdr.Rows.Count > 0 && dtExportDataInfo != null & dtExportDataInfo.Rows.Count > 0)
            {
                string baseFolder = DataImportHelper.GetFolderForDownloadRequests(objExportInfo.CompanyID.Value, objExportInfo.RecPeriodID.Value);
                string filePath = baseFolder + Helper.ReplaceSpecialChars(dtRequestHdr.Rows[0][SearchAndEmailConstants.HeaderFields.FILENAME].ToString()) + ".xls";
                ExportToExcelUtility.CreateExcelFile(dtExportDataInfo, objExportInfo, filePath, ServiceConstants.EXPORTSHEETNAME, oLogInfoCache, this.CompanyUserInfo);
                SendRequestStatusEmail(objExportInfo, true, oLogInfoCache);
            }
        }

        /// <summary>
        /// Process Download All Recs Request
        /// </summary>
        /// <param name="ds"></param>
        /// <param name="oExportToExcelInfo"></param>
        /// <returns></returns>
        private void ProcessDownloadAllRecs(DataSet ds, ExportToExcelInfo oExportToExcelInfo, List<ClientModel.LogInfo> oLogInfoCache)
        {
            try
            {
                DataTable dtRequestHdr = ds.Tables[DownloadAllRecsConstants.HeaderTableName];
                DataTable dtRequestDtl = ds.Tables[DownloadAllRecsConstants.DetailTableName];

                if (dtRequestHdr != null && dtRequestHdr.Rows.Count > 0 && dtRequestDtl != null && dtRequestDtl.Rows.Count > 0)
                {
                    DataRow drHeader = dtRequestHdr.Rows[0];
                    string basefolderName = DataImportHelper.GetFolderForDownloadRequests(oExportToExcelInfo.CompanyID.Value, oExportToExcelInfo.RecPeriodID.Value);
                    string fileName = Helper.ReplaceSpecialChars(drHeader[DownloadAllRecsConstants.HeaderFields.FILENAME].ToString());
                    string zipPath = basefolderName + @"\" + fileName + ".zip";
                    using (ZipArchive oZipArchive = ZipFile.Open(zipPath, ZipArchiveMode.Create))
                    {
                            foreach (DataRow dr in dtRequestDtl.Rows)
                            {
                                try
                                {
                                    string accountName = Helper.ReplaceSpecialChars(dr[DownloadAllRecsConstants.DetailFields.ACCOUNTNAME].ToString());
                                    byte[] pdfBiteStream = CreateReportPDF(drHeader, dr);

                                    ZipArchiveEntry oZipArchiveEntry = oZipArchive.CreateEntry(accountName + @"\" + accountName + ".pdf");

                                    using (BinaryWriter oBinaryWriter = new BinaryWriter(oZipArchiveEntry.Open()))
                                    {
                                        oBinaryWriter.Write(pdfBiteStream);
                                        oBinaryWriter.Flush();
                                    }

                                    Int64? glDataID = Convert.ToInt64(dr[DownloadAllRecsConstants.DetailFields.GLDATAID]);
                                    IAttachment oAttachmentClient = RemotingHelper.GetAttachmentObject();
                                    ClientModel.AppUserInfo oAppUserInfo = new ClientModel.AppUserInfo();
                                    oAppUserInfo.CompanyID = oExportToExcelInfo.CompanyID;
                                    oAppUserInfo.RecPeriodID = oExportToExcelInfo.RecPeriodID;
                                    List<ClientModel.AttachmentInfo> oAttachmentInfoList = oAttachmentClient.GetAllAttachmentForGL(glDataID, oExportToExcelInfo.UserID, oExportToExcelInfo.RoleID, oAppUserInfo);
                                    if (oAttachmentInfoList != null && oAttachmentInfoList.Count > 0)
                                    {
                                        oAttachmentInfoList.ForEach(T => oZipArchive.CreateEntryFromFile(T.PhysicalPath, accountName + @"\" + GetFileName(T.PhysicalPath)));
                                    }
                                }
                                catch (Exception ex)
                                {
                                    Helper.LogInfoToCache(ex, oLogInfoCache);
                                }
                            }
                    }
                    oExportToExcelInfo.OutputFile = zipPath;
                    oExportToExcelInfo.OutputFileSize = Helper.GetFileSize(zipPath);
                }
            }
            catch (Exception ex)
            {
                Helper.LogInfoToCache(ex, oLogInfoCache);
            }
            if (oLogInfoCache.Count == 0)
            {
                oExportToExcelInfo.IsRequestSuccessFull = true;
            }
            else
            {
                oExportToExcelInfo.OutputFile = null;
                oExportToExcelInfo.IsRequestSuccessFull = false;
            }
            SendRequestStatusEmail(oExportToExcelInfo, false, oLogInfoCache);
        }

        #region Create PDF

        /// <summary>
        /// Create Rec Form PDF
        /// </summary>
        /// <param name="drHeader"></param>
        /// <param name="drDetail"></param>
        /// <returns></returns>
        private byte[] CreateReportPDF(DataRow drHeader, DataRow drDetail)
        {
            byte[] oByteCollection = null;
            using (ReportViewer rvReportViewer = new ReportViewer())
            {
                rvReportViewer.ServerReport.ReportServerUrl = new Uri(Shared.Utility.SharedAppSettingHelper.GetAppSettingValue("ReportUri"));
                rvReportViewer.ServerReport.ReportPath = Shared.Utility.SharedAppSettingHelper.GetAppSettingValue("ReportPath");

                List<ReportParameter> reportParameters = new List<ReportParameter>();

                reportParameters = SetReportParameters(reportParameters, drHeader, drDetail);
                rvReportViewer.ServerReport.SetParameters(reportParameters);

                Warning[] warnings;
                string[] streamids;
                string mimeType;
                string encoding;
                string extension;

                oByteCollection = rvReportViewer.ServerReport.Render(
                "PDF", null, out mimeType, out encoding, out extension,
                out streamids, out warnings);
            }
            return oByteCollection;
        }

        /// <summary>
        /// Set Report Parameters
        /// </summary>
        /// <param name="reportParams"></param>
        /// <param name="drHeader"></param>
        /// <param name="drDetail"></param>
        /// <returns></returns>
        private List<ReportParameter> SetReportParameters(List<ReportParameter> reportParams, DataRow drHeader, DataRow drDetail)
        {

            ReportParameter recPeriodID = new ReportParameter("RecPeriodID", drHeader[DownloadAllRecsConstants.HeaderFields.RECPERIODID].ToString());
            reportParams.Add(recPeriodID);

            ReportParameter languageID = new ReportParameter("LANGUAGEID", drHeader[DownloadAllRecsConstants.HeaderFields.LANGUAGEID].ToString());
            reportParams.Add(languageID);

            ReportParameter companyID = new ReportParameter("COMPANYID", drHeader[DownloadAllRecsConstants.HeaderFields.COMPANYID].ToString());
            reportParams.Add(companyID);

            ReportParameter defaultLanguageID = new ReportParameter("DEFAULTLANGUAGEID", drHeader[DownloadAllRecsConstants.HeaderFields.DEFAULTLANGUAGEID].ToString());
            reportParams.Add(defaultLanguageID);

            ReportParameter userID = new ReportParameter("UserID", drHeader[DownloadAllRecsConstants.HeaderFields.USERID].ToString());
            reportParams.Add(userID);

            ReportParameter roleID = new ReportParameter("RoleID", drHeader[DownloadAllRecsConstants.HeaderFields.ROLEID].ToString());
            reportParams.Add(roleID);

            ReportParameter isDualReviewEnabled = new ReportParameter("IsDualReviewEnabled", drHeader[DownloadAllRecsConstants.HeaderFields.ISDUALREVIEWENABLED].ToString());
            reportParams.Add(isDualReviewEnabled);

            ReportParameter isCertificationEnabled = new ReportParameter("IsCertificationEnabled", drHeader[DownloadAllRecsConstants.HeaderFields.ISCERTIFICATIONENABLED].ToString());
            reportParams.Add(isCertificationEnabled);

            ReportParameter isMaterialityEnabled = new ReportParameter("IsMaterialityEnabled", drHeader[DownloadAllRecsConstants.HeaderFields.ISMATERIALITYENABLED].ToString());
            reportParams.Add(isMaterialityEnabled);

            ReportParameter certificationTypeId = new ReportParameter("CertificationTypeId", drHeader[DownloadAllRecsConstants.HeaderFields.CERTIFICATIONTYPEID].ToString());
            reportParams.Add(certificationTypeId);

            ReportParameter preparerAttributeId = new ReportParameter("PreparerAttributeId", drHeader[DownloadAllRecsConstants.HeaderFields.PREPARERATTRIBUTEID].ToString());
            reportParams.Add(preparerAttributeId);

            ReportParameter reviewerAttributeID = new ReportParameter("ReviewerAttributeID", drHeader[DownloadAllRecsConstants.HeaderFields.REVIEWERATTRIBUTEID].ToString());
            reportParams.Add(reviewerAttributeID);

            ReportParameter approverAttributeID = new ReportParameter("ApproverAttributeID", drHeader[DownloadAllRecsConstants.HeaderFields.APPROVERATTRIBUTEID].ToString());
            reportParams.Add(approverAttributeID);

            ReportParameter isQualityScoreEnabled = new ReportParameter("IsQualityScoreEnabled", drHeader[DownloadAllRecsConstants.HeaderFields.ISQUALITYSCOREENABLED].ToString());
            reportParams.Add(isQualityScoreEnabled);

            ReportParameter IsReviewNotesEnabled = new ReportParameter("IsReviewNotesEnabled", drHeader[DownloadAllRecsConstants.HeaderFields.ISREVIEWNOTESENABLED].ToString());
            reportParams.Add(IsReviewNotesEnabled);

            ReportParameter languageInfo = new ReportParameter("languageInfo", drHeader[DownloadAllRecsConstants.HeaderFields.LANGUAGEINFO].ToString());
            reportParams.Add(languageInfo);

            ReportParameter glDataID = new ReportParameter("GLdataID", drDetail[DownloadAllRecsConstants.DetailFields.GLDATAID].ToString());
            reportParams.Add(glDataID);

            ReportParameter reconciliationTemplateID = new ReportParameter("ReconciliationTemplateID", drDetail[DownloadAllRecsConstants.DetailFields.RECONCILIATIONTEMPLATEID].ToString());
            reportParams.Add(reconciliationTemplateID);

            string netAccountIDValue = "0";
            if (drDetail[DownloadAllRecsConstants.DetailFields.NETACCOUNTID] != DBNull.Value)
                netAccountIDValue = drDetail[DownloadAllRecsConstants.DetailFields.NETACCOUNTID].ToString();
            ReportParameter netAccountID = new ReportParameter("NetAccountID", netAccountIDValue);
            reportParams.Add(netAccountID);

            ReportParameter isNetAccount = new ReportParameter("isNetAccount", netAccountIDValue == "0" ? "true" : "false");
            reportParams.Add(isNetAccount);

            string accountIDValue = "0";
            if (drDetail[DownloadAllRecsConstants.DetailFields.ACCOUNTID] != DBNull.Value)
                accountIDValue = drDetail[DownloadAllRecsConstants.DetailFields.ACCOUNTID].ToString();
            ReportParameter accId = new ReportParameter("AccountID", accountIDValue);
            reportParams.Add(accId);

            DataAccess oDataAccess = new DataAccess(this.CompanyUserInfo);
            ReportParameter connString = new ReportParameter("ConnectionString", oDataAccess.GetConnectionString());
            reportParams.Add(connString);

            return reportParams;
        }

        #endregion

        #region Common Functions

        /// <summary>
        /// Get the file name from full path and remove timestamp/suffix added by the application during upload
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string GetOriginalFileName(string fileName)
        {
            string actFileName = fileName;
            int fileNameStartIndex = fileName.LastIndexOf(@"\") + 1;
            int fileNameEndIndex = fileName.LastIndexOf("_");
            if (fileNameStartIndex >= 0 && fileNameEndIndex >= 0 && fileNameStartIndex < fileNameEndIndex)
            {
                actFileName = fileName.Substring(fileNameStartIndex, fileNameEndIndex - fileNameStartIndex);
                actFileName += fileName.Substring(fileName.LastIndexOf("."));
            }
            return actFileName;
        }

        /// <summary>
        /// Get the file name from full path 
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string GetFileName(string fileName)
        {
            string actFileName = fileName;
            int fileNameStartIndex = fileName.LastIndexOf(@"\") + 1;
            if (fileNameStartIndex >= 0)
            {
                actFileName = fileName.Substring(fileNameStartIndex);
            }
            return actFileName;
        }

        /// <summary>
        /// Send Email
        /// </summary>
        /// <param name="objExportToExcel"></param>
        /// <param name="oLogInfoCache"></param>
        public static void SendRequestStatusEmail(ExportToExcelInfo objExportToExcel, bool sendAttachments, List<ClientModel.LogInfo> oLogInfoCache)
        {
            try
            {
                //Mail settings
                string smtpServer = Helper.GetAppSettingFromKey(AppSettingConstants.EMAIL_SMTP_SERVER);
                //smtpPort from web.config
                string smtpPort = Helper.GetAppSettingFromKey(AppSettingConstants.EMAIL_SMTP_PORT);
                //Network Credentials from web.config
                string userName = Helper.GetAppSettingFromKey(AppSettingConstants.EMAIL_USER_NAME);
                string pwd = Helper.GetAppSettingFromKey(AppSettingConstants.EMAIL_PASSWORD);
                //From Address from config
                string fromAddress = Helper.GetAppSettingFromKey(AppSettingConstants.EMAIL_FROM_DEFAULT);

                bool bEnableSSL = false;
                if (Helper.GetAppSettingFromKey(AppSettingConstants.EMAIL_ENABLE_SSL) != null)
                {
                    bEnableSSL = Convert.ToBoolean(Helper.GetAppSettingFromKey(AppSettingConstants.EMAIL_ENABLE_SSL));
                }

                List<string> fileAttachments = new List<string>();
                if (sendAttachments && !string.IsNullOrEmpty(objExportToExcel.OutputFile))
                    fileAttachments.Add(objExportToExcel.OutputFile);

                objExportToExcel.IsEmailSuccessFull = SkyStem.ART.Shared.Utility.MailHelper.SendMail(fromAddress, objExportToExcel.ToEmailID, objExportToExcel.EmailSubject,
                objExportToExcel.EmailBody, fileAttachments, smtpServer, Convert.ToInt32(smtpPort), bEnableSSL, userName, pwd);
            }
            catch (Exception ex)
            {
                objExportToExcel.IsEmailSuccessFull = false;
                Helper.LogInfoToCache(ex, oLogInfoCache);
            }
        }

        /// <summary>
        /// Update Request Status
        /// </summary>
        /// <param name="oExportToExcelInfo"></param>
        private void UpdateRequestStatus(ExportToExcelInfo oExportToExcelInfo)
        {
            ExportToExcelParamInfo oExportToExcelParamInfo = CreateInsertParamsForAttachment(oExportToExcelInfo);
            InsertAttachmentDetails(oExportToExcelParamInfo);
        }

        /// <summary>
        /// Create Param Info for Update Request Status
        /// </summary>
        /// <param name="oExportToExcelInfo"></param>
        /// <returns></returns>
        private ExportToExcelParamInfo CreateInsertParamsForAttachment(ExportToExcelInfo oExportToExcelInfo)
        {
            ExportToExcelParamInfo oExportToExcelParamInfo = new ExportToExcelParamInfo();
            oExportToExcelParamInfo.RequestID = oExportToExcelInfo.RequestID;
            oExportToExcelParamInfo.CompanyID = oExportToExcelInfo.CompanyID;
            oExportToExcelParamInfo.RevisedBy = oExportToExcelInfo.AddedBy;
            oExportToExcelParamInfo.DateRevised = DateTime.Now;
            oExportToExcelParamInfo.RequestErrorCodeID = GetRequestErrorCodeID(oExportToExcelInfo);
            if (!oExportToExcelParamInfo.RequestErrorCodeID.HasValue || oExportToExcelParamInfo.RequestErrorCodeID == 2)
            {
                oExportToExcelParamInfo.FileName = GetOriginalFileName(oExportToExcelInfo.OutputFile);
                oExportToExcelParamInfo.FileSize = oExportToExcelInfo.OutputFileSize;
                oExportToExcelParamInfo.IsActive = true;
                oExportToExcelParamInfo.IsFileDeleted = false;
                oExportToExcelParamInfo.PhysicalPath = oExportToExcelInfo.OutputFile;
                oExportToExcelParamInfo.IsMailSendingErrorFlag = oExportToExcelInfo.IsEmailSuccessFull;
                oExportToExcelParamInfo.IsRequestSuccessFull = oExportToExcelInfo.IsRequestSuccessFull;
                oExportToExcelParamInfo.RequestStatusID = GetRequestStatusID(oExportToExcelInfo);
            }
            else
            {
                oExportToExcelParamInfo.RequestStatusID = 4;
                if (!string.IsNullOrEmpty(oExportToExcelParamInfo.FileName))
                    File.Delete(oExportToExcelParamInfo.FileName);
            }
            return oExportToExcelParamInfo;
        }

        private Int16? GetRequestStatusID(ExportToExcelInfo oExportToExcelInfo)
        {
            Int16? statusID = null;

            if (oExportToExcelInfo.IsRequestSuccessFull.GetValueOrDefault())
                statusID = 3; //Success
            else
                statusID = 4;  //Error
            return statusID;
        }

        private Int16? GetRequestErrorCodeID(ExportToExcelInfo oExportToExcelInfo)
        {
            decimal? dataStorageCapacity;
            decimal? currentUsage;
            Helper.GetCompanyDataStorageCapacityAndCurrentUsage(oExportToExcelInfo.CompanyID.Value, out dataStorageCapacity, out currentUsage);
            Int16? errorCodeID = null;
            if (!oExportToExcelInfo.IsRequestSuccessFull.Value)
                errorCodeID = 1; //Error while processing request
            else if (oExportToExcelInfo.IsPartOfStorageSpace.GetValueOrDefault() && oExportToExcelInfo.OutputFileSize > (dataStorageCapacity - currentUsage))
                errorCodeID = 3;  //Error while sending email
            else if (!oExportToExcelInfo.IsEmailSuccessFull.Value)
                errorCodeID = 2;  //Error while sending email
            return errorCodeID;
        }

        #endregion
    }
}
