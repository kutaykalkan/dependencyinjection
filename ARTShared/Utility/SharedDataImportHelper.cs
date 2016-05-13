using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.OleDb;
using System.Data;
using System.IO;
using System.Text;
using SkyStem.ART.Web.Utility;
using SkyStem.ART.Client.IServices;
using SkyStem.ART.Client.Exception;
using SkyStem.ART.Client.Data;
using SkyStem.ART.Client.Model;
using SkyStem.Language.LanguageUtility;
using SkyStem.ART.Shared.Data;
using SkyStem.ART.Shared.Utility;
using SkyStem.Language.LanguageUtility.Classes;


namespace SkyStem.ART.Web.Utility
{
    /// <summary>
    /// Summary description for DataImportHelper
    /// </summary>
    public class SharedDataImportHelper
    {

        public SharedDataImportHelper()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        /// <summary>
        /// Create company folder name by companyId. 
        /// </summary>
        /// <param name="companyID">id of company</param>
        public static void CreateCompanyFolderForDataImport(int companyID)
        {
            //read base folder path from web config.
            string baseFolderPath = GetBaseFolder();

            if (baseFolderPath != "")
            {
                if (!baseFolderPath.EndsWith("\\"))
                {
                    baseFolderPath += "\\";
                }

                if (!Directory.Exists(baseFolderPath + companyID.ToString()))
                {
                    Directory.CreateDirectory(baseFolderPath + companyID.ToString());
                }
            }
        }

        /// <summary>
        /// Gets folder name for import type as per company id
        /// </summary>
        /// <param name="companyID">id of the company</param>
        /// <param name="importType">import type</param>
        /// <returns>folder name</returns>
        public static string GetFolderForImport(int companyID, short importType)
        {
            // There will be a Base folder(path from web config). 
            // Within Base Folder, there will be folders 
            // for each company as per CompanyId at the time of company creation.
            // If CompanyID folder exists, Create another folder by the name of Import Type.
            // Else create a folder by the name of companyID and within it, ImportType
            // Base folder + companyId + Import type

            // CURRENT_COMPANY_ID
            string baseFolderPath = GetBaseFolderForCompany(companyID);
            string importFolder = @"";

            //Check if import folder within company folder exists or not.
            //if not, Create import folder within company folder
            importFolder = baseFolderPath + @"\" + GetImportTypeName(importType, null, null);

            if (!Directory.Exists(importFolder))
                Directory.CreateDirectory(importFolder);

            return importFolder + @"\";
        }

        /// <summary>
        /// Gets folder name for import type as per company id
        /// </summary>
        /// <param name="companyID">id of the company</param>
        /// <param name="importType">import type</param>
        /// <returns>folder name</returns>
        public static string GetBaseFolder()
        {
            // There will be a Base folder(path from web config). 
            string baseFolderPath = @"";//read base folder path from web config.

            //Read base folder name and physical path
            baseFolderPath = SharedAppSettingHelper.GetAppSettingValue(SharedAppSettingConstants.BASE_FOLDER_FOR_FILES);
            if (baseFolderPath == null)
                throw new ARTException(5000039);

            //if folder for Files is not created, then create it
            if (!Directory.Exists(baseFolderPath))
            {
                Directory.CreateDirectory(baseFolderPath);
            }

            return baseFolderPath + @"\";
        }



        /// <summary>
        /// Gets folder name for Temporary Files
        /// </summary>
        /// <param name="companyID">id of the company</param>
        /// <param name="importType">import type</param>
        /// <returns>folder name</returns>
        public static string GetFolderForTemporaryFilesForExport()
        {
            // There will be a Base folder(path from web config). 
            // Within Base Folder, there will be a temp folders for exported files used for Email

            // CURRENT_COMPANY_ID
            string baseFolderPath = GetBaseFolder();
            string tempFolder = SharedAppSettingHelper.GetAppSettingValue(SharedAppSettingConstants.TEMP_FOLDER_FOR_EXPORT_FILES);

            //Check if import folder within company folder exists or not.
            //if not, Create import folder within company folder
            tempFolder = baseFolderPath + @"\" + tempFolder;

            if (!Directory.Exists(tempFolder))
                Directory.CreateDirectory(tempFolder);

            tempFolder += @"\";
            return tempFolder;
        }


        /// <summary>
        /// Gets folder name for import type as per company id
        /// </summary>
        /// <param name="companyID">id of the company</param>
        /// <param name="importType">import type</param>
        /// <returns>folder name</returns>
        public static string GetBaseFolderForCompany(int companyID)
        {
            // There will be a Base folder(path from web config). 
            // Within Base Folder, there will be folders 
            // for each company as per CompanyId at the time of company creation.
            // If CompanyID folder exists, Create another folder by the name of Import Type.
            // Else create a folder by the name of companyID and within it, ImportType
            // Base folder + companyId + Import type

            // COMPANY_ID
            string baseFolderPath = @"";//read base folder path from web config.

            //Read base folder name and physical path
            baseFolderPath = GetBaseFolder();

            if (!baseFolderPath.EndsWith("\\"))
            {
                baseFolderPath += "\\";
            }

            baseFolderPath += companyID.ToString();

            //if folder for company is not created at "Create Company", create it.
            if (!Directory.Exists(baseFolderPath))
                CreateCompanyFolderForDataImport(companyID);

            return baseFolderPath + @"\";
        }

        public static string GetImportTypeName(short importType, DateTime? ReconciliationPeriodEndDate, int? CurrentReconciliationPeriodID)
        {
            string importTypeName = string.Empty;
            try
            {
                switch ((ARTEnums.DataImportType)importType)
                {
                    case ARTEnums.DataImportType.HolidayCalendar:
                        importTypeName = DataImportTypeName.HOLIDAYCALENDAR;
                        break;
                    case ARTEnums.DataImportType.PeriodEndDates:
                        importTypeName = DataImportTypeName.PERIODENDDATE;
                        break;
                    case ARTEnums.DataImportType.GLData:
                        importTypeName = DataImportTypeName.GLDATA;
                        break;
                    case ARTEnums.DataImportType.AccountAttributeList:
                        importTypeName = DataImportTypeName.ACCOUNTATTRIBUTELIST;
                        break;
                    case ARTEnums.DataImportType.CurrencyExchangeRateData:
                        importTypeName = DataImportTypeName.CURRENCYEXCHANGERATE;
                        break;
                    case ARTEnums.DataImportType.SubledgerData:
                        importTypeName = DataImportTypeName.SUBLEDGERDATA;
                        break;
                    case ARTEnums.DataImportType.SubledgerSource:
                        importTypeName = DataImportTypeName.SUBLEDGERSOURCE;
                        break;
                    case ARTEnums.DataImportType.RecItems:
                        if (ReconciliationPeriodEndDate.HasValue)
                            importTypeName = ReconciliationPeriodEndDate.Value.ToString("ddMMyyyy");
                        break;
                    case ARTEnums.DataImportType.CompanyLogo:
                        importTypeName = DataImportTypeName.COMPANYLOGO;
                        break;
                    case ARTEnums.DataImportType.GLTBS:
                        if (CurrentReconciliationPeriodID.HasValue)
                            importTypeName = @"Matching\" + CurrentReconciliationPeriodID.Value.ToString() + DataImportTypeName.GLTBS;
                        break;
                    case ARTEnums.DataImportType.NBF:
                        importTypeName = DataImportTypeName.NBF;
                        break;
                    case ARTEnums.DataImportType.AccountUpload:
                        importTypeName = DataImportTypeName.MAPPINGUPLOAD;
                        break;
                    case ARTEnums.DataImportType.MultilingualUpload:
                        importTypeName = DataImportTypeName.MAPPINGUPLOAD;
                        break;
                    case ARTEnums.DataImportType.UserUpload:
                        importTypeName = DataImportTypeName.USERUPLOAD;
                        break;
                    case ARTEnums.DataImportType.GeneralTaskImport:
                        importTypeName = DataImportTypeName.TASKIMPORT;
                        break;
                }
                return importTypeName;
            }
            catch (ARTException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Formulates new filename for imported file.
        /// </summary>
        /// <param name="validFile">File object</param>
        /// <param name="importType">import type</param>
        /// <returns>new file name</returns>
        public static string GetFileName(string FileNameWithoutExtension, string FileExtension, short importType, DateTime? RecPeriodEndDate, MultilingualAttributeInfo oMultilingualAttributeInfo)
        {

            StringBuilder oSb = new StringBuilder();
            switch ((ARTEnums.DataImportType)importType)
            {
                case ARTEnums.DataImportType.HolidayCalendar:
                    // <<FileName>>_<<UploadDateTime>>
                    oSb.Append(FileNameWithoutExtension);
                    oSb.Append("_");
                    oSb.Append(SharedHelper.GetDisplayDateTime(DateTime.Now, oMultilingualAttributeInfo));
                    oSb.Append(FileExtension);
                    break;
                case ARTEnums.DataImportType.PeriodEndDates:
                    // <<FileName>>_<<UploadDateTime>>
                    oSb.Append(FileNameWithoutExtension);
                    oSb.Append("_");
                    oSb.Append(SharedHelper.GetDisplayDateTime(DateTime.Now, oMultilingualAttributeInfo));
                    oSb.Append(FileExtension);
                    break;
                default:
                    // <<FileName>>_<<recPeriod>>_<<UploadDateTime>>
                    oSb.Append(FileNameWithoutExtension);
                    oSb.Append("_");
                    oSb.Append(SharedHelper.GetDisplayDate(RecPeriodEndDate, oMultilingualAttributeInfo));
                    oSb.Append("_");
                    oSb.Append(SharedHelper.GetDisplayDateTime(DateTime.Now, oMultilingualAttributeInfo));
                    oSb.Append(FileExtension);
                    break;
            }
            foreach (char ch in Path.GetInvalidFileNameChars())
            {
                oSb.Replace(ch.ToString(), "");
            }
            oSb.Replace(" ", "");
            return oSb.ToString();
        }
        public static bool IsDataSheetPresent(string filePath, string fileExtension, string SheetName)
        {    
            DataTable dtSchema = GetExcelFileSchema(filePath, fileExtension);         
            return !(FindSheetIndex(dtSchema, SheetName) < 0);
        }      
        private static DataTable GetExcelFileSchema(string filePath, string fileExtension)
        {
            DataTable dtSchema = null;
            OleDbConnection oConnectionExcel = null;
            try
            {
                oConnectionExcel = ExcelHelper.GetConnectionForExcelFile(filePath, fileExtension, true);
                oConnectionExcel.Open();
                dtSchema = oConnectionExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            }
            finally
            {
                if (oConnectionExcel != null && oConnectionExcel.State != ConnectionState.Closed)
                    oConnectionExcel.Dispose();
            }
            return dtSchema;
        }
        public static short FindSheetIndex(DataTable dtSchema, string sheetName)
        {
            short sheetIndex = -1;
            string columnName = "";
            for (short i = 0; i < dtSchema.Rows.Count; i++)
            {
                columnName = dtSchema.Rows[i]["TABLE_NAME"].ToString().Replace("'", "");
                if (columnName.ToUpper() == sheetName.ToUpper() + "$")
                {
                    sheetIndex = i;
                    break;
                }
            }
            return sheetIndex;
        }


        #region "File Helper Methods"
        public static void SaveFileFromStream(string fileFullPath, Stream stream)
        {
            using (System.IO.FileStream output = new System.IO.FileStream(fileFullPath, FileMode.Create))
            {
                stream.CopyTo(output);
            }
        }
        public static string GetFileNameWithoutExtension(string FileFullName)
        {
            if (FileFullName != null && FileFullName.Contains('.'))
                return FileFullName.Substring(0, FileFullName.LastIndexOf('.'));
            else
                return FileFullName;
        }
        public static string GetFileExtension(string FileFullName)
        {
            if (FileFullName != null && FileFullName.Contains('.'))
                return FileFullName.Substring(FileFullName.LastIndexOf('.'));
            else
                return null;
        }
        public static void CopyFile(string OriginalFilePath, string newFilePath)
        {
            string path = OriginalFilePath;
            string path2 = newFilePath;
            try
            {
                // Copy the file.
                if (!File.Exists(path2))
                    File.Copy(path, path2, false);
                else
                    throw new ARTException(5000379);
            }
            catch (Exception)
            {
                throw new ARTException(5000379);
            }

        }
        public static long GetFileSize(string filePath)
        {
            FileInfo file = new FileInfo(filePath);
            return file.Length;
        }
        public static string GetFTPLoginID(string ARTUserLoginID)
        {
            return ARTUserLoginID.Replace("@", "_");
        }
        public static string GetFTPDataImportFolderName(UserFTPConfigurationInfo oUserFTPConfigurationInfo)
        {
            string FTPDataImportFolderName = string.Empty;

            switch ((ARTEnums.DataImportType)oUserFTPConfigurationInfo.DataImportTypeID.GetValueOrDefault())
            {
                case ARTEnums.DataImportType.GLData:
                    FTPDataImportFolderName = FTPDataImportConstants.FTPDataImportFolderName.GL_DATA;
                    break;
                case ARTEnums.DataImportType.SubledgerData:
                    FTPDataImportFolderName = FTPDataImportConstants.FTPDataImportFolderName.SUBLEDGER_DATA;
                    break;
            }
            return FTPDataImportFolderName;

        }
        #endregion

        #region File Helper Functions
        private static string GetBaseFolderForFTPUser(string loginID, string companyAlias)
        {
            string homeDir = SharedAppSettingHelper.GetAppSettingValue(SharedAppSettingConstants.BASE_FOLDER_FOR_FTP_UPLOAD);           
            homeDir += Path.DirectorySeparatorChar + loginID;
            homeDir += Path.DirectorySeparatorChar + companyAlias;
            return homeDir;
        }
        public static string GetFirstFile(string loginID, string companyAlias, string folderName, string extFilter)
        {
            string folderPath = GetBaseFolderForFTPUser(loginID, companyAlias);
            folderPath += Path.DirectorySeparatorChar + folderName;
            if (string.IsNullOrEmpty(extFilter))
                extFilter = "*.*";
            string[] ext = extFilter.Split('|');
            if (Directory.Exists(folderPath))
            {
                IEnumerable<string> ienumFiles = Directory.EnumerateFiles(folderPath);
                foreach (string fileName in ienumFiles)
                {
                    FileInfo oFileInfo = new FileInfo(fileName);
                    if (Array.Exists(ext, T => T.ToLower() == "*" + oFileInfo.Extension.ToLower()))
                        return oFileInfo.Name;
                }
            }
            return null;
        }
        public static Stream GetFileStream(string loginID, string companyAlias, string folderName, string fileName)
        {
            string filePath = GetBaseFolderForFTPUser(loginID, companyAlias);
            filePath += Path.DirectorySeparatorChar + folderName;
            if (Directory.Exists(filePath) && !string.IsNullOrEmpty(fileName))
            {
                filePath += Path.DirectorySeparatorChar + fileName;
                FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                return fs;
            }
            return null;
        }

        public static bool DeleteFile(string loginID, string companyAlias, string folderName, string fileName)
        {
            string filePath = GetBaseFolderForFTPUser(loginID, companyAlias);
            filePath += Path.DirectorySeparatorChar + folderName;
            if (Directory.Exists(filePath) && !string.IsNullOrEmpty(fileName))
            {
                filePath += Path.DirectorySeparatorChar + fileName;
                File.Delete(filePath);
            }
            return false;
        }
        #endregion

    }
}
