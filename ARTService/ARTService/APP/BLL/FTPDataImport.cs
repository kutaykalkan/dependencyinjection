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
using SkyStem.ART.Web.Utility;
using SkyStem.Language.LanguageUtility.Classes;
using SkyStem.ART.Client.Data;
using ART.Integration.Utility;
using SkyStem.Language.LanguageUtility;

namespace SkyStem.ART.Service.APP.BLL
{
    /// <summary>
    /// 
    /// </summary>
    /// 


    public class FTPDataImport
    {
        List<ClientModel.UserFTPConfigurationInfo> _objUserFTPConfigurationInfo;
        CompanyUserInfo CompanyUserInfo;
        public FTPDataImport(CompanyUserInfo oCompanyUserInfo)
        {
            this.CompanyUserInfo = oCompanyUserInfo;
        }

        internal bool IsProcessingRequiredForFTPDataImport()
        {
            bool isFTPDataImportRequired = false;
            try
            {
                List<ClientModel.UserFTPConfigurationInfo> objActivatedFTPUserList = DataImportHelper.GetFTPUsers(this.CompanyUserInfo);
                if (objActivatedFTPUserList != null && objActivatedFTPUserList.Count > 0)
                {
                    _objUserFTPConfigurationInfo = objActivatedFTPUserList;
                    isFTPDataImportRequired = true;
                }
            }
            catch (Exception ex)
            {
                isFTPDataImportRequired = false;
                _objUserFTPConfigurationInfo = null;
                Helper.LogError(@"Error in IsProcessingRequiredForExportToExcel: " + ex.Message, this.CompanyUserInfo);

            }
            return isFTPDataImportRequired;
        }

        internal void ProcessFTPDataImport()
        {
            if (_objUserFTPConfigurationInfo != null)
            {
                foreach (ClientModel.UserFTPConfigurationInfo objUserFTPConfigurationInfo in _objUserFTPConfigurationInfo)
                {
                    List<ClientModel.LogInfo> oLogInfoCache = new List<ClientModel.LogInfo>();
                    try
                    {
                        ProcessFTPDataImport(objUserFTPConfigurationInfo, oLogInfoCache);
                    }
                    catch (Exception ex)
                    {
                        Helper.LogErrorToCache(ex, oLogInfoCache);
                    }
                    finally
                    {
                        try
                        {
                            Helper.LogListViaService(oLogInfoCache, null, this.CompanyUserInfo);
                        }
                        catch (Exception ex)
                        {
                            Helper.LogError(ex, this.CompanyUserInfo);
                        }
                    }
                    if (objUserFTPConfigurationInfo.IsProcessFTPDataImportSuccessFull.HasValue && objUserFTPConfigurationInfo.IsProcessFTPDataImportSuccessFull.Value == false)
                    {
                        SendMailToUserAboutFTPDataImportFail(objUserFTPConfigurationInfo);
                    }

                }
            }
        }
        private void ProcessFTPDataImport(ClientModel.UserFTPConfigurationInfo oUserFTPConfigurationInfo, List<ClientModel.LogInfo> oLogInfoCache)
        {
            string FTPDataImportFolderName = SharedDataImportHelper.GetFTPDataImportFolderName(oUserFTPConfigurationInfo);
            string ProfileName = GetProfileName(oUserFTPConfigurationInfo);
            // Get File from FTP
            string FTPDataImportFileName = GetFTPDataImportFileName(oUserFTPConfigurationInfo, FTPDataImportFolderName, oLogInfoCache);
            ClientModel.ReconciliationPeriodInfo oReconciliationPeriodInfo = null;
            oUserFTPConfigurationInfo.FTPFileName = FTPDataImportFileName;
            try
            {
                ClientModel.DataImportHdrInfo oDataImportHrdInfo = new ClientModel.DataImportHdrInfo();
                if (!string.IsNullOrEmpty(FTPDataImportFileName) && ProfileName != null)
                {

                    Stream FileSteam = GetFileStream(oUserFTPConfigurationInfo, FTPDataImportFolderName, FTPDataImportFileName);
                    if (FileSteam != null)
                    {

                        string filePath;
                        string TemporaryFilePath = string.Empty;

                        oReconciliationPeriodInfo = GetReconciliationPeriodInfo(oUserFTPConfigurationInfo, FileSteam, FTPDataImportFileName, out TemporaryFilePath, oLogInfoCache);


                        if (oReconciliationPeriodInfo != null && !string.IsNullOrEmpty(TemporaryFilePath))
                        {
                            filePath = GetFilePath(oUserFTPConfigurationInfo, FTPDataImportFileName, oReconciliationPeriodInfo.PeriodEndDate, oDataImportHrdInfo, oLogInfoCache);
                            if (!string.IsNullOrEmpty(filePath))
                            {
                                SharedDataImportHelper.CopyFile(TemporaryFilePath, filePath);
                                oDataImportHrdInfo.ReconciliationPeriodID = oReconciliationPeriodInfo.ReconciliationPeriodID;
                                if (oReconciliationPeriodInfo.ReconciliationPeriodStatusID == 2 || oReconciliationPeriodInfo.ReconciliationPeriodStatusID == 3)// To Do: User Enum for Checking
                                {
                                    oDataImportHrdInfo.IsMultiVersionUpload = true;
                                    oDataImportHrdInfo.SystemLockdownInfo = DataImportHelper.GetSystemLockdownInfo(ARTEnums.SystemLockdownReason.UploadDataProcessingRequired, oUserFTPConfigurationInfo, oReconciliationPeriodInfo.ReconciliationPeriodID);
                                }
                                else
                                    oDataImportHrdInfo.IsMultiVersionUpload = false;

                                oDataImportHrdInfo.DataImportTypeID = oUserFTPConfigurationInfo.DataImportTypeID;
                                oDataImportHrdInfo.ImportTemplateID = oUserFTPConfigurationInfo.ImportTemplateID;
                                oDataImportHrdInfo.RoleID = oUserFTPConfigurationInfo.FTPUploadRoleID;
                                oDataImportHrdInfo.DataImportName = ProfileName;
                                oDataImportHrdInfo.DataImportStatusID = 5;//   Submitted 
                                oDataImportHrdInfo.RecordsImported = 0;
                                oDataImportHrdInfo.CompanyID = oUserFTPConfigurationInfo.CompanyID;
                                oDataImportHrdInfo.IsActive = true;
                                oDataImportHrdInfo.DateAdded = DateTime.Now;
                                oDataImportHrdInfo.AddedBy = oUserFTPConfigurationInfo.LoginID;
                                oDataImportHrdInfo.LanguageID = oUserFTPConfigurationInfo.DefaultLanguageID;
                                oDataImportHrdInfo.FileSize = Convert.ToDecimal(SharedDataImportHelper.GetFileSize(filePath));
                                oDataImportHrdInfo.RecordSourceTypeID = (short)ARTEnums.RecordSourceType.FTP;

                            }
                        }
                        else
                        {
                            oUserFTPConfigurationInfo.IsProcessFTPDataImportSuccessFull = false;
                            oUserFTPConfigurationInfo.ErrorMessageLabelID = 2931;
                        }
                        // Create DataImportHdr Record
                        if (!string.IsNullOrEmpty(oDataImportHrdInfo.FileName) && oDataImportHrdInfo.FileSize.HasValue && oDataImportHrdInfo.FileSize.Value > 0)
                        {
                            try
                            {
                                DataImportHelper.InsertDataImportHdr(oUserFTPConfigurationInfo, oDataImportHrdInfo);
                            }
                            catch (Exception ex)
                            {
                                oUserFTPConfigurationInfo.IsProcessFTPDataImportSuccessFull = false;
                                oUserFTPConfigurationInfo.ErrorMessageLabelID = 5000042;
                                oUserFTPConfigurationInfo.ExceptionMessage = ex.Message;
                                throw ex;
                            }

                            // Delete File From FTP Location

                            DeleteFileFromFTPLocation(oUserFTPConfigurationInfo, FTPDataImportFolderName, FTPDataImportFileName);
                            //Process Data Import File
                            ProcessDataImportFile(oUserFTPConfigurationInfo, oLogInfoCache);
                            oUserFTPConfigurationInfo.IsProcessFTPDataImportSuccessFull = true;
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Helper.LogInfoToCache(ex, oLogInfoCache);
            }
            if (oUserFTPConfigurationInfo.IsProcessFTPDataImportSuccessFull.HasValue && oUserFTPConfigurationInfo.IsProcessFTPDataImportSuccessFull.Value == false)
            {
                // Delete File From FTP Location
                DeleteFileFromFTPLocation(oUserFTPConfigurationInfo, FTPDataImportFolderName, FTPDataImportFileName);
            }

        }
        private void ProcessDataImportFile(ClientModel.UserFTPConfigurationInfo oUserFTPConfigurationInfo, List<ClientModel.LogInfo> oLogInfoCache)
        {
            try
            {
                switch ((Enums.DataImportType)oUserFTPConfigurationInfo.DataImportTypeID.GetValueOrDefault())
                {
                    case Enums.DataImportType.GLData:
                        GLDataImport oGLDataImport = new GLDataImport(this.CompanyUserInfo);
                        if (oGLDataImport.IsProcessingRequiredForGLDataImport())
                        {
                            oGLDataImport.ProcessGLDataImport();
                        }
                        break;
                    case Enums.DataImportType.SubledgerData:
                        SubledgerDataImport oSubledgerDataImport = new SubledgerDataImport(this.CompanyUserInfo);
                        if (oSubledgerDataImport.IsProcessingRequiredForSubledgerDataImport())
                        {
                            oSubledgerDataImport.ProcessSubledgerDataImport();
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                Helper.LogInfoToCache(ex, oLogInfoCache);
            }

        }
        private ClientModel.ReconciliationPeriodInfo GetReconciliationPeriodInfo(ClientModel.UserFTPConfigurationInfo oUserFTPConfigurationInfo, Stream FileSteam, string FTPDataImportFileName, out string TemporaryFilePath, List<ClientModel.LogInfo> oLogInfoCache)
        {
            MultilingualAttributeInfo oMultilingualAttributeInfo = Helper.GetMultilingualAttributeInfo(oUserFTPConfigurationInfo.DefaultLanguageID.GetValueOrDefault(), oUserFTPConfigurationInfo.CompanyID.GetValueOrDefault());
            ClientModel.ReconciliationPeriodInfo oReconciliationPeriodInfo = null;
            string fileExtension = SharedDataImportHelper.GetFileExtension(FTPDataImportFileName);
            string fileNameWithoutExtension = SharedDataImportHelper.GetFileNameWithoutExtension(FTPDataImportFileName);
            string FTPTempFileName = GetTempFileName(fileNameWithoutExtension, fileExtension, oMultilingualAttributeInfo, oUserFTPConfigurationInfo.CompanyID); ;
            TemporaryFilePath = GetTemporaryFilePath(oUserFTPConfigurationInfo, FTPTempFileName, oLogInfoCache);

            try
            {
                DateTime? RecPeriodEndDate = null;
                List<SkyStem.ART.Client.Model.ImportTemplateFieldMappingInfo> oImportTemplateFieldMappingInfoList = null;
                DataTable dtExcelData = null;
                if (FileSteam != null)
                {
                    Enums.DataImportType oDataImportTypeEnum = (Enums.DataImportType)oUserFTPConfigurationInfo.DataImportTypeID;
                    if (!string.IsNullOrEmpty(TemporaryFilePath))
                    {
                        SaveFileFromStream(TemporaryFilePath, FileSteam, FTPTempFileName, oUserFTPConfigurationInfo);
                        if (DataImportHelper.GetKeyCount(oUserFTPConfigurationInfo).HasValue)
                        {
                            string SheetName = DataImportHelper.GetSheetName(oDataImportTypeEnum, oUserFTPConfigurationInfo.ImportTemplateID, oUserFTPConfigurationInfo.CompanyID);
                            if (IsDataSheetPresent(TemporaryFilePath, SheetName))
                            {
                                List<string> MandatoryFileldListPED = new List<string>();
                                DataTable TempdtExcelData = DataImportHelper.GetGLDataImportDataTableFromExcel(TemporaryFilePath, SheetName, this.CompanyUserInfo);
                                if (oUserFTPConfigurationInfo.ImportTemplateID.HasValue && oUserFTPConfigurationInfo.ImportTemplateID.Value != Convert.ToInt32(ServiceConstants.ART_TEMPLATE))
                                {
                                    oImportTemplateFieldMappingInfoList = DataImportHelper.GetImportTemplateFieldMappingInfoList(oUserFTPConfigurationInfo.ImportTemplateID, oUserFTPConfigurationInfo.CompanyID.Value);

                                    if (oImportTemplateFieldMappingInfoList != null && oImportTemplateFieldMappingInfoList.Count > 0)
                                    {
                                        var Item = oImportTemplateFieldMappingInfoList.Find(obj => obj.ImportFieldID.GetValueOrDefault() == (int)ARTEnums.DataImportFields.PeriodEndDate);
                                        if (Item != null)
                                            MandatoryFileldListPED.Add(Item.ImportTemplateField);
                                    }
                                }
                                else
                                {
                                    MandatoryFileldListPED.Add(GLDataImportFields.PERIODENDDATE);
                                }
                                string MandatoryColumnsNotPrasentListPED;
                                if (IsAllMandatoryColumnsPresentForDataImport(TempdtExcelData, MandatoryFileldListPED, out MandatoryColumnsNotPrasentListPED, false, oImportTemplateFieldMappingInfoList))
                                {
                                    if (oUserFTPConfigurationInfo.ImportTemplateID.HasValue && oUserFTPConfigurationInfo.ImportTemplateID.Value != Convert.ToInt32(ServiceConstants.ART_TEMPLATE))
                                    {
                                        dtExcelData = DataImportHelper.RenameTemplateColumnNameToARTColumns(TempdtExcelData, oImportTemplateFieldMappingInfoList);
                                    }
                                    else
                                    {
                                        dtExcelData = TempdtExcelData;
                                    }
                                }
                                else
                                {
                                    oUserFTPConfigurationInfo.ErrorMessage = String.Format(LanguageUtil.GetValue(5000165, oMultilingualAttributeInfo), MandatoryColumnsNotPrasentListPED);
                                    oUserFTPConfigurationInfo.IsProcessFTPDataImportSuccessFull = false;
                                    throw new Exception("All Mandatory Columns Not Present");
                                }


                                if (dtExcelData != null && dtExcelData.Rows.Count > 0)
                                {
                                    DataRow dr = dtExcelData.Rows[0];
                                    DateTime periodEndDate;
                                    if (Helper.IsValidDateTime(dr[GLDataImportFields.PERIODENDDATE].ToString(), oUserFTPConfigurationInfo.DefaultLanguageID.Value, out periodEndDate))
                                    {
                                        RecPeriodEndDate = periodEndDate;
                                    }
                                    else
                                    {
                                        oUserFTPConfigurationInfo.ErrorMessageLabelID = 2933;
                                        oUserFTPConfigurationInfo.IsProcessFTPDataImportSuccessFull = false;
                                        throw new Exception("Invalid Period End Date.");
                                    }
                                }
                                else
                                {
                                    oUserFTPConfigurationInfo.ErrorMessageLabelID = 2931;
                                    oUserFTPConfigurationInfo.IsProcessFTPDataImportSuccessFull = false;
                                    throw new Exception("Error while reading the file from FTP.");
                                }
                                if (RecPeriodEndDate.HasValue)
                                    oReconciliationPeriodInfo = DataImportHelper.GetReconciliationPeriodInfo(RecPeriodEndDate, oUserFTPConfigurationInfo.CompanyID);

                                if (oReconciliationPeriodInfo == null || !oReconciliationPeriodInfo.ReconciliationPeriodID.HasValue)
                                {
                                    oUserFTPConfigurationInfo.ErrorMessageLabelID = 5000156;
                                    oUserFTPConfigurationInfo.IsProcessFTPDataImportSuccessFull = false;
                                    throw new Exception("Input Reconciliation Period not present ");
                                }
                                else
                                {
                                    List<string> MandatoryFileldList = null;
                                    bool IsGetImportTemplateFieldList;

                                    List<string> MandatoryFileldListTemp = GetAllMandatoryFields(oUserFTPConfigurationInfo, oReconciliationPeriodInfo.ReconciliationPeriodID.Value);
                                    if (oUserFTPConfigurationInfo.ImportTemplateID.HasValue && oUserFTPConfigurationInfo.ImportTemplateID.Value != Convert.ToInt32(ServiceConstants.ART_TEMPLATE))
                                    {
                                        MandatoryFileldList = new List<string>();
                                        foreach (var item in MandatoryFileldListTemp)
                                        {
                                            MandatoryFileldList.Add(oImportTemplateFieldMappingInfoList.Find(obj => obj.ImportTemplateField == item).ImportField);
                                        }
                                        IsGetImportTemplateFieldList = true;
                                    }
                                    else
                                    {
                                        MandatoryFileldList = MandatoryFileldListTemp;
                                        IsGetImportTemplateFieldList = false;
                                    }

                                    string MandatoryColumnsNotPrasentList;

                                    if (!IsAllMandatoryColumnsPresentForDataImport(dtExcelData, MandatoryFileldList, out MandatoryColumnsNotPrasentList, IsGetImportTemplateFieldList, oImportTemplateFieldMappingInfoList))
                                    {

                                        oUserFTPConfigurationInfo.ErrorMessage = String.Format(LanguageUtil.GetValue(5000165, oMultilingualAttributeInfo), MandatoryColumnsNotPrasentList);
                                        oUserFTPConfigurationInfo.IsProcessFTPDataImportSuccessFull = false;
                                        throw new Exception("All Mandatory Columns Not Present");
                                    }
                                }
                            }
                            else
                            {

                                oUserFTPConfigurationInfo.ErrorMessage = String.Format(LanguageUtil.GetValue(5000256, oMultilingualAttributeInfo), SheetName);
                                oUserFTPConfigurationInfo.IsProcessFTPDataImportSuccessFull = false;
                                throw new Exception("Upload does not contain mandatory sheet");
                            }
                        }
                        else
                        {
                            oUserFTPConfigurationInfo.ErrorMessageLabelID = 2930;
                            oUserFTPConfigurationInfo.IsProcessFTPDataImportSuccessFull = false;
                            throw new Exception("First GL Load must be uploaded through application.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                if (!(oUserFTPConfigurationInfo.ErrorMessageLabelID.HasValue || ! string.IsNullOrEmpty(oUserFTPConfigurationInfo.ErrorMessage)))
                {
                    oUserFTPConfigurationInfo.ErrorMessageLabelID = 2931;
                    oUserFTPConfigurationInfo.IsProcessFTPDataImportSuccessFull = false;
                }
                throw ex;
            }
            finally
            {
                FileSteam.Dispose();
            }
            return oReconciliationPeriodInfo;
        }
        public void SaveFileFromStream(string fileFullPath, Stream stream, string FTPTempFileName, ClientModel.UserFTPConfigurationInfo oUserFTPConfigurationInfo)
        {
            try
            {
                SharedDataImportHelper.SaveFileFromStream(fileFullPath, stream);
                oUserFTPConfigurationInfo.FTPTempFileName = FTPTempFileName;
                oUserFTPConfigurationInfo.FTPFilePath = fileFullPath;
            }
            catch (Exception Ex)
            {

                oUserFTPConfigurationInfo.IsProcessFTPDataImportSuccessFull = false;
                oUserFTPConfigurationInfo.ErrorMessageLabelID = 2931;
                oUserFTPConfigurationInfo.ExceptionMessage = Ex.Message;
                throw Ex;
            }

        }
        private bool IsDataSheetPresent(string TemporaryFilePath, string SheetName)
        {
            FileInfo file = new FileInfo(TemporaryFilePath);
            if (file.Extension.ToLower() == FileExtensions.csv)
            {
                return true;
            }
            else
            {
                if (!SharedDataImportHelper.IsDataSheetPresent(TemporaryFilePath, file.Extension, SheetName))
                    return false;
                else
                    return true;
            }

        }
        private string GetFilePath(ClientModel.UserFTPConfigurationInfo oUserFTPConfigurationInfo, string FTPDataImportFileName, DateTime? RecPeriodEndDate, ClientModel.DataImportHdrInfo oDataImportHrdInfo, List<ClientModel.LogInfo> oLogInfoCache)
        {
            string filePath = string.Empty;
            try
            {

                string fileName = string.Empty;
                string targetFolder = string.Empty;
                targetFolder = SharedDataImportHelper.GetFolderForImport(oUserFTPConfigurationInfo.CompanyID.GetValueOrDefault(), oUserFTPConfigurationInfo.DataImportTypeID.GetValueOrDefault());
                string fileExtension = SharedDataImportHelper.GetFileExtension(FTPDataImportFileName);
                string fileNameWithoutExtension = SharedDataImportHelper.GetFileNameWithoutExtension(FTPDataImportFileName);
                MultilingualAttributeInfo oMultilingualAttributeInfo;
                oMultilingualAttributeInfo = Helper.GetMultilingualAttributeInfo(oUserFTPConfigurationInfo.CompanyID.GetValueOrDefault(), oUserFTPConfigurationInfo.DefaultLanguageID.GetValueOrDefault());
                fileName = SharedDataImportHelper.GetFileName(fileNameWithoutExtension, fileExtension, oUserFTPConfigurationInfo.DataImportTypeID.GetValueOrDefault(), RecPeriodEndDate, oMultilingualAttributeInfo);

                filePath = Path.Combine(targetFolder, fileName);
                if (!string.IsNullOrEmpty(filePath))
                {
                    //oDataImportHrdInfo.FileName = fileName;
                    oDataImportHrdInfo.FileName = FTPDataImportFileName;
                    oDataImportHrdInfo.PhysicalPath = filePath;
                }
            }
            catch (Exception ex)
            {
                Helper.LogInfoToCache(ex, oLogInfoCache);
            }
            return filePath;
        }
        private string GetTempFileName(string FileNameWithoutExtension, string FileExtension, MultilingualAttributeInfo oMultilingualAttributeInfo, int? CompanyID)
        {
            StringBuilder oSb = new StringBuilder();
            oSb.Append(FileNameWithoutExtension);
            oSb.Append("_");
            if (CompanyID.HasValue)
                oSb.Append(CompanyID.Value.ToString());
            oSb.Append(SharedHelper.GetDisplayDateTime(DateTime.Now, oMultilingualAttributeInfo));
            oSb.Append(FileExtension);
            foreach (char ch in Path.GetInvalidFileNameChars())
            {
                oSb.Replace(ch.ToString(), "");
            }
            return oSb.ToString();
        }
        private string GetTemporaryFilePath(ClientModel.UserFTPConfigurationInfo oUserFTPConfigurationInfo, string FTPTempFileName, List<ClientModel.LogInfo> oLogInfoCache)
        {

            string filePath = string.Empty;
            try
            {
                filePath = SharedDataImportHelper.GetFolderForTemporaryFilesForExport() + FTPTempFileName;
            }
            catch (Exception ex)
            {
                Helper.LogInfoToCache(ex, oLogInfoCache);
            }
            return filePath;
        }
        private string GetFTPDataImportFileName(ClientModel.UserFTPConfigurationInfo oUserFTPConfigurationInfo, string FTPDataImportFolderName, List<ClientModel.LogInfo> oLogInfoCache)
        {
            string FTPDataImportFileName = string.Empty;
            try
            {
                string FTPUserLoginID = SharedDataImportHelper.GetFTPLoginID(oUserFTPConfigurationInfo.FTPLoginID);
                FTPDataImportFileName = SharedDataImportHelper.GetFirstFile(FTPUserLoginID, oUserFTPConfigurationInfo.CompanyAlias, FTPDataImportFolderName, FileExtensions.AllowedExtensions);
                // FTPDataImportFileName = IntegrationUtil.GetFirstFile(FTPUserLoginID, FTPDataImportFolderName, FileExtensions.AllowedExtensions);               
                //FTPDataImportFileName = GetFristFileName(@"D:\Test\FTP_Cust_GL.xlsx");

            }
            catch (Exception ex)
            {
                Helper.LogInfoToCache(ex, oLogInfoCache);

            }
            return FTPDataImportFileName;
        }
        private Stream GetFileStream(ClientModel.UserFTPConfigurationInfo oUserFTPConfigurationInfo, string FTPDataImportFolderName, string FTPFileName)
        {
            Stream oFTPFileStream = null;
            try
            {
                oFTPFileStream = SharedDataImportHelper.GetFileStream(SharedDataImportHelper.GetFTPLoginID(oUserFTPConfigurationInfo.FTPLoginID), oUserFTPConfigurationInfo.CompanyAlias, FTPDataImportFolderName, FTPFileName);
                // return TestStream(@"D:\Test\FTP_Cust_GL.xlsx");
            }
            catch (Exception Ex)
            {
                oUserFTPConfigurationInfo.IsProcessFTPDataImportSuccessFull = false;
                oUserFTPConfigurationInfo.ErrorMessageLabelID = 2931;
                oUserFTPConfigurationInfo.ExceptionMessage = Ex.Message;
                throw Ex;
            }
            return oFTPFileStream;
        }
        private bool DeleteFileFromFTPLocation(ClientModel.UserFTPConfigurationInfo oUserFTPConfigurationInfo, string FTPDataImportFolderName, string FTPFileName)
        {
            try
            {
                return SharedDataImportHelper.DeleteFile(SharedDataImportHelper.GetFTPLoginID(oUserFTPConfigurationInfo.FTPLoginID), oUserFTPConfigurationInfo.CompanyAlias, FTPDataImportFolderName, FTPFileName);
            }
            catch (Exception Ex)
            {
                oUserFTPConfigurationInfo.IsProcessFTPDataImportSuccessFull = false;
                oUserFTPConfigurationInfo.ErrorMessageLabelID = 2932;
                oUserFTPConfigurationInfo.ExceptionMessage = Ex.Message;
                throw Ex;
            }
        }
        //private Stream TestStream(string Path)
        //{
        //    // Stream fs = File.OpenRead(@"D:\Test\FTP_Cust_GL.xlsx");
        //    Stream fs = File.OpenRead(Path);
        //    return fs;
        //}
        //private string GetFristFileName(string Path)
        //{
        //    return "FTP_Cust_GL.xlsx";
        //}
        private string GetProfileName(ClientModel.UserFTPConfigurationInfo oUserFTPConfigurationInfo)
        {
            StringBuilder oSb = new StringBuilder();
            if (!string.IsNullOrEmpty(oUserFTPConfigurationInfo.ProfileName))
            {
                MultilingualAttributeInfo oMultilingualAttributeInfo;
                oMultilingualAttributeInfo = Helper.GetMultilingualAttributeInfo(oUserFTPConfigurationInfo.CompanyID.GetValueOrDefault(), oUserFTPConfigurationInfo.DefaultLanguageID.GetValueOrDefault());
                oSb.Append(oUserFTPConfigurationInfo.ProfileName);
                oSb.Append("_");
                oSb.Append(SharedHelper.GetDisplayDateTime(DateTime.Now, oMultilingualAttributeInfo));
                foreach (char ch in Path.GetInvalidFileNameChars())
                {
                    oSb.Replace(ch.ToString(), "");
                }
                return oSb.ToString();
            }
            else
                return null;
        }
        private void SendMailToUserAboutFTPDataImportFail(ClientModel.UserFTPConfigurationInfo oUserFTPConfigurationInfo)
        {
            string fromEmailId = Helper.GetAppSettingFromKey("defaultEmailFromCompany");
            Helper.LogInfo(@"Begin: Sending FTP Upload Status Emails", this.CompanyUserInfo);
            StringBuilder oStringBuilder = new StringBuilder();
            MultilingualAttributeInfo oMultilingualAttributeInfo = Helper.GetMultilingualAttributeInfo(oUserFTPConfigurationInfo.DefaultLanguageID.GetValueOrDefault(), oUserFTPConfigurationInfo.CompanyID.GetValueOrDefault());
            string MailSubject = LanguageUtil.GetValue(2934, oMultilingualAttributeInfo);
            oStringBuilder.Append(LanguageUtil.GetValue(1845, oMultilingualAttributeInfo));
            oStringBuilder.Append("&nbsp;");
            oStringBuilder.Append(oUserFTPConfigurationInfo.FirstName);
            oStringBuilder.Append("&nbsp;");
            oStringBuilder.Append(oUserFTPConfigurationInfo.LastName);
            oStringBuilder.Append(",");
            try
            {
                oStringBuilder.Append("<br>");
                oStringBuilder.Append("<br>");
                if (!string.IsNullOrEmpty(oUserFTPConfigurationInfo.ErrorMessage))
                    oStringBuilder.Append(oUserFTPConfigurationInfo.ErrorMessage);
                else
                    oStringBuilder.Append(LanguageUtil.GetValue(oUserFTPConfigurationInfo.ErrorMessageLabelID.GetValueOrDefault(), oMultilingualAttributeInfo));

                oStringBuilder.Append("<br>");
                oStringBuilder.Append("<br>");
                oStringBuilder.Append(string.Format("{0}: ", LanguageUtil.GetValue(1524, oMultilingualAttributeInfo)));
                oStringBuilder.Append(oUserFTPConfigurationInfo.ProfileName);

                if (!string.IsNullOrEmpty(oUserFTPConfigurationInfo.FTPFileName))
                {
                    oStringBuilder.Append("<br>");
                    oStringBuilder.Append("<br>");
                    oStringBuilder.Append(string.Format("{0}: ", LanguageUtil.GetValue(2027, oMultilingualAttributeInfo)));
                    oStringBuilder.Append(oUserFTPConfigurationInfo.FTPFileName);
                }
                List<String> oFilePathCollection = new List<string>();
                if (!string.IsNullOrEmpty(oUserFTPConfigurationInfo.FTPFilePath))
                    oFilePathCollection.Add(oUserFTPConfigurationInfo.FTPFilePath);
                SkyStem.ART.Service.Utility.MailHelper.SendEmail(fromEmailId, oUserFTPConfigurationInfo.EmailID, MailSubject, oStringBuilder.ToString(), oFilePathCollection, this.CompanyUserInfo);

            }
            catch (Exception ex)
            {
                Helper.LogError(ex, this.CompanyUserInfo);
            }
            Helper.LogInfo(@"End: Sending FTP Upload Status Emails", this.CompanyUserInfo);
        }

        public bool IsAllMandatoryColumnsPresentForDataImport(DataTable dt, List<string> MandatoryFileldList, out string MandatoryColumnsNotPrasentList, bool GetImportTemplateFieldList, List<SkyStem.ART.Client.Model.ImportTemplateFieldMappingInfo> oImportTemplateFieldMappingInfoList)
        {
            bool IsAllMandatoryColumnsPresent = true;
            string currentColumnName = string.Empty;
            MandatoryColumnsNotPrasentList = string.Empty;
            string[] TblColumns = null;
            TblColumns = (from dc in dt.Columns.Cast<DataColumn>()
                          select dc.ColumnName.ToUpper().Trim()).ToArray();
            if (TblColumns != null && TblColumns.Length > 0)
            {
                foreach (var item in MandatoryFileldList)
                {

                    if (!TblColumns.Contains(item.ToUpper()))
                    {
                        IsAllMandatoryColumnsPresent = false;
                        if (string.IsNullOrEmpty(MandatoryColumnsNotPrasentList))
                        {
                            if (GetImportTemplateFieldList)
                                MandatoryColumnsNotPrasentList = GetImportTemplateField(oImportTemplateFieldMappingInfoList, item);
                            else
                                MandatoryColumnsNotPrasentList = item;
                        }
                        else
                        {
                            if (GetImportTemplateFieldList)
                                MandatoryColumnsNotPrasentList = MandatoryColumnsNotPrasentList + " , " + GetImportTemplateField(oImportTemplateFieldMappingInfoList, item);
                            else
                                MandatoryColumnsNotPrasentList = MandatoryColumnsNotPrasentList + " , " + item;
                        }
                    }
                }
            }
            return IsAllMandatoryColumnsPresent;
        }
        string GetImportTemplateField(List<SkyStem.ART.Client.Model.ImportTemplateFieldMappingInfo> oImportTemplateFieldMappingInfoList, string ImportField)
        {
            return oImportTemplateFieldMappingInfoList.Find(obj => obj.ImportField != null && obj.ImportField.ToLower() == ImportField.ToLower()).ImportTemplateField;
        }
        private List<string> GetAllMandatoryFields(ClientModel.UserFTPConfigurationInfo oUserFTPConfigurationInfo, int recPeriodID)
        {
            List<string> tmp;
            tmp = DataImportHelper.GetAllMandatoryFields(oUserFTPConfigurationInfo.CompanyID, oUserFTPConfigurationInfo.ImportTemplateID, recPeriodID);
            return tmp;
        }




    }
}
