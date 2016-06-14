using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.OleDb;
using System.Data;
using System.IO;
using Telerik.Web.UI;
using System.Text;
using SkyStem.ART.Web.Utility;
using SkyStem.ART.Web.Data;
using SkyStem.ART.Client.IServices;
using SkyStem.ART.Client.Exception;
using SkyStem.ART.Client.Data;
using SkyStem.ART.Client.Model;
using SkyStem.Language.LanguageUtility;
using SkyStem.ART.Shared.Data;
using SkyStem.ART.Shared.Utility;


namespace SkyStem.ART.Web.Utility
{
    /// <summary>
    /// Summary description for DataImportHelper
    /// </summary>
    public class DataImportHelper
    {

        public DataImportHelper()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        //TODO: Method below needs to be removed once all the pages recerencing this method 
        // comes under development.
        public static DataTable ImportToGrid(string filePath, string fileExtension)
        {

            string conStr = "";
            OleDbConnection oConnectionExcel = null;
            OleDbCommand oCommandExcel = null;
            OleDbDataAdapter oAdapter = null;
            DataSet ds = null;
            DataTable dt = null;
            switch (fileExtension)
            {
                case ".xls": //Excel 97-03 
                    conStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties='Excel 8.0;HDR={1}'";
                    break;
                case ".xlsx": //Excel 07 
                    conStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties='Excel 8.0;HDR={1}'";
                    break;
            }
            try
            {
                conStr = String.Format(conStr, filePath, false);
                oConnectionExcel = new OleDbConnection(conStr);
                oCommandExcel = new OleDbCommand();
                oAdapter = new OleDbDataAdapter();
                dt = new DataTable();
                ds = new DataSet();
                oCommandExcel.Connection = oConnectionExcel;
                //Get the name of First Sheet 
                oConnectionExcel.Open();
                DataTable dtExcelSchema = oConnectionExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                string SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
                oCommandExcel.CommandText = "SELECT * From [" + SheetName + "]";
                oAdapter.SelectCommand = oCommandExcel;
                oAdapter.Fill(ds);
                dt = ds.Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if ((oConnectionExcel != null) && (oConnectionExcel.State == ConnectionState.Open))
                {
                    oConnectionExcel.Close();
                }
            }
            return dt;
        }

        /// <summary>
        /// Formulates new filename for imported file.
        /// </summary>
        /// <param name="validFile">File object</param>
        /// <param name="importType">import type</param>
        /// <returns>new file name</returns>
        //public static string GetFileName(UploadedFile validFile, short importType)
        //{

        //    StringBuilder oSb = new StringBuilder();
        //    switch (importType)
        //    {
        //        case (short)ARTEnums.DataImportType.HolidayCalendar:
        //            // <<FileName>>_<<UploadDateTime>>
        //            oSb.Append(validFile.GetNameWithoutExtension());
        //            oSb.Append("_");
        //            oSb.Append(Helper.GetDisplayDateTime(DateTime.Now));
        //            oSb.Append(validFile.GetExtension());
        //            break;
        //        case (short)ARTEnums.DataImportType.PeriodEndDates:
        //            // <<FileName>>_<<UploadDateTime>>
        //            oSb.Append(validFile.GetNameWithoutExtension());
        //            oSb.Append("_");
        //            oSb.Append(Helper.GetDisplayDateTime(DateTime.Now));
        //            oSb.Append(validFile.GetExtension());
        //            break;
        //        default:
        //            // <<FileName>>_<<recPeriod>>_<<UploadDateTime>>
        //            oSb.Append(validFile.GetNameWithoutExtension());
        //            oSb.Append("_");
        //            oSb.Append(Helper.GetDisplayDate(SessionHelper.CurrentReconciliationPeriodEndDate));
        //            oSb.Append("_");
        //            oSb.Append(Helper.GetDisplayDateTime(DateTime.Now));
        //            oSb.Append(validFile.GetExtension());
        //            break;
        //    }
        //    foreach (char ch in Path.GetInvalidFileNameChars())
        //    {
        //        oSb.Replace(ch.ToString(), "");
        //    }
        //    oSb.Replace(" ", "");
        //    return oSb.ToString();
        //}

        /// <summary>
        /// Create company folder name by companyId. 
        /// </summary>
        /// <param name="companyID">id of company</param>
        //public static void CreateCompanyFolderForDataImport(int companyID)
        //{
        //    //read base folder path from web config.
        //    string baseFolderPath = DataImportHelper.GetBaseFolder();

        //    if (baseFolderPath != "")
        //    {
        //        if (!baseFolderPath.EndsWith("\\"))
        //        {
        //            baseFolderPath += "\\";
        //        }

        //        if (!Directory.Exists(baseFolderPath + companyID.ToString()))
        //        {
        //            Directory.CreateDirectory(baseFolderPath + companyID.ToString());
        //        }
        //    }
        //}

        /// <summary>
        /// Gets folder name for import type as per company id
        /// </summary>
        /// <param name="companyID">id of the company</param>
        /// <param name="importType">import type</param>
        /// <returns>folder name</returns>
        //public static string GetFolderForImport(int companyID, short importType)
        //{
        //    // There will be a Base folder(path from web config). 
        //    // Within Base Folder, there will be folders 
        //    // for each company as per CompanyId at the time of company creation.
        //    // If CompanyID folder exists, Create another folder by the name of Import Type.
        //    // Else create a folder by the name of companyID and within it, ImportType
        //    // Base folder + companyId + Import type

        //    // CURRENT_COMPANY_ID
        //    string baseFolderPath = GetBaseFolderForCompany(companyID);
        //    string importFolder = @"";

        //    //Check if import folder within company folder exists or not.
        //    //if not, Create import folder within company folder
        //    importFolder = baseFolderPath + @"\" + DataImportHelper.GetImportTypeName(importType);

        //    if (!Directory.Exists(importFolder))
        //        Directory.CreateDirectory(importFolder);

        //    return importFolder + @"\";
        //}

        /// <summary>
        /// Gets folder name for import type as per company id
        /// </summary>
        /// <param name="companyID">id of the company</param>
        /// <param name="importType">import type</param>
        /// <returns>folder name</returns>
        //public static string GetBaseFolder()
        //{
        //    // There will be a Base folder(path from web config). 
        //    string baseFolderPath = @"";//read base folder path from web config.

        //    //Read base folder name and physical path
        //    baseFolderPath = AppSettingHelper.GetAppSettingValue(AppSettingConstants.BASE_FOLDER_FOR_FILES);
        //    if (baseFolderPath == null)
        //        throw new ARTException(5000039);

        //    //if folder for Files is not created, then create it
        //    if (!Directory.Exists(baseFolderPath))
        //    {
        //        Directory.CreateDirectory(baseFolderPath);
        //    }

        //    return baseFolderPath + @"\";
        //}



        /// <summary>
        /// Gets folder name for Temporary Files
        /// </summary>
        /// <param name="companyID">id of the company</param>
        /// <param name="importType">import type</param>
        /// <returns>folder name</returns>
        //public static string GetFolderForTemporaryFilesForExport()
        //{
        //    // There will be a Base folder(path from web config). 
        //    // Within Base Folder, there will be a temp folders for exported files used for Email

        //    // CURRENT_COMPANY_ID
        //    string baseFolderPath = SharedDataImportHelper.GetBaseFolder();
        //    string tempFolder = AppSettingHelper.GetAppSettingValue(AppSettingConstants.TEMP_FOLDER_FOR_EXPORT_FILES);

        //    //Check if import folder within company folder exists or not.
        //    //if not, Create import folder within company folder
        //    tempFolder = baseFolderPath + @"\" + tempFolder;

        //    if (!Directory.Exists(tempFolder))
        //        Directory.CreateDirectory(tempFolder);

        //    tempFolder += @"\";
        //    return tempFolder;
        //}


        /// <summary>
        /// Gets folder name for import type as per company id
        /// </summary>
        /// <param name="companyID">id of the company</param>
        /// <param name="importType">import type</param>
        /// <returns>folder name</returns>
        //public static string GetBaseFolderForCompany(int companyID)
        //{
        //    // There will be a Base folder(path from web config). 
        //    // Within Base Folder, there will be folders 
        //    // for each company as per CompanyId at the time of company creation.
        //    // If CompanyID folder exists, Create another folder by the name of Import Type.
        //    // Else create a folder by the name of companyID and within it, ImportType
        //    // Base folder + companyId + Import type

        //    // COMPANY_ID
        //    string baseFolderPath = @"";//read base folder path from web config.

        //    //Read base folder name and physical path
        //    baseFolderPath = DataImportHelper.GetBaseFolder();

        //    if (!baseFolderPath.EndsWith("\\"))
        //    {
        //        baseFolderPath += "\\";
        //    }

        //    baseFolderPath += companyID.ToString();

        //    //if folder for company is not created at "Create Company", create it.
        //    if (!Directory.Exists(baseFolderPath))
        //        CreateCompanyFolderForDataImport(companyID);

        //    return baseFolderPath + @"\";
        //}


        /// <summary>
        /// Gets available data storage for company
        /// </summary>
        /// <param name="companyID">id of the company</param>
        /// <returns>available storage capacity</returns>
        public static long GetAvailableDataStorageByCompanyId(int companyID)
        {
            long availableStorage = 0;
            try
            {
                IDataImport oDataImport = RemotingHelper.GetDataImportObject();
                availableStorage = oDataImport.GetAvailableFileStorageSpaceByCompanyID(companyID, Helper.GetAppUserInfo());
                return availableStorage;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Returns alowed file extensiond
        /// </summary>
        /// <returns>string array of allowed file extensions</returns>
        public static string[] GetAllowedFileExtensions()
        {
            string allowedFileExtensions = AppSettingHelper.GetAppSettingValue(AppSettingConstants.ALLOWEDFILEEXTENSIONS);
            string[] text = allowedFileExtensions.Split(new Char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            return text;
            //return new string[] { ".xls", ".xlsx" };
        }

        /// <summary>
        /// Returns maximum allowed file size as per company id
        /// </summary>
        /// <param name="companyID">id of company</param>
        /// <returns>size of file in bytes</returns>
        public static int GetAllowedMaximumFileSize(int companyID)
        {

            int maxFileSize = 0;
            decimal? tempFileSize;
            try
            {
                IDataImport oDataImport = RemotingHelper.GetDataImportObject();
                tempFileSize = oDataImport.GetMaxFileSizeByCompanyID(companyID, Helper.GetAppUserInfo());
                if (!tempFileSize.HasValue)
                {
                    //get default value from web.config
                    maxFileSize = Convert.ToInt32(AppSettingHelper.GetAppSettingValue(AppSettingConstants.DEFAULTDATAIMPORTFILESIZE));
                }
                else
                {
                    maxFileSize = Convert.ToInt32(tempFileSize.Value * 1024 * 1024);
                }
                return maxFileSize;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="importType"></param>
        /// <returns></returns>
        /// 
        public static int GetAllowedMaximumFileSizeInt(int companyID)
        {

            int maxFileSize = 0;

            try
            {
                IDataImport oDataImport = RemotingHelper.GetDataImportObject();
                maxFileSize = oDataImport.GetMaxFileSizeByCompanyIDInt(companyID, Helper.GetAppUserInfo());
                if (maxFileSize <= 0) //Manoj : Get Default Value if MaxFileSize value is NULL in DB 
                {
                    maxFileSize = Convert.ToInt32(AppSettingHelper.GetAppSettingValue(AppSettingConstants.DEFAULT_MAX_DOC_SIZE));
                }
            }
            catch (Exception)
            {
            }
            return maxFileSize;
        }

        //public static string GetImportTypeName(short importType)
        //{
        //    string importTypeName = string.Empty;
        //    try
        //    {
        //        switch (importType)
        //        {
        //            case (short)Enums.DataImportType.HolidayCalendar:
        //                importTypeName = DataImportTypeName.HOLIDAYCALENDAR;
        //                break;
        //            case (short)ARTEnums.DataImportType.PeriodEndDates:
        //                importTypeName = DataImportTypeName.PERIODENDDATE;
        //                break;
        //            case (short)ARTEnums.DataImportType.GLData:
        //                importTypeName = DataImportTypeName.GLDATA;
        //                break;
        //            case (short)ARTEnums.DataImportType.AccountAttributeList:
        //                importTypeName = DataImportTypeName.ACCOUNTATTRIBUTELIST;
        //                break;
        //            case (short)ARTEnums.DataImportType.CurrencyExchangeRateData:
        //                importTypeName = DataImportTypeName.CURRENCYEXCHANGERATE;
        //                break;
        //            case (short)ARTEnums.DataImportType.SubledgerData:
        //                importTypeName = DataImportTypeName.SUBLEDGERDATA;
        //                break;
        //            case (short)ARTEnums.DataImportType.SubledgerSource:
        //                importTypeName = DataImportTypeName.SUBLEDGERSOURCE;
        //                break;
        //            case (short)ARTEnums.DataImportType.RecItems:
        //                importTypeName = SessionHelper.CurrentReconciliationPeriodEndDate.Value.ToString("ddMMyyyy");
        //                break;
        //            case (short)ARTEnums.DataImportType.CompanyLogo:
        //                importTypeName = DataImportTypeName.COMPANYLOGO;
        //                break;
        //            case (short)ARTEnums.DataImportType.GLTBS:
        //                importTypeName = @"Matching\" + SessionHelper.CurrentReconciliationPeriodID.Value.ToString() + DataImportTypeName.GLTBS;
        //                break;
        //            case (short)ARTEnums.DataImportType.NBF:
        //                importTypeName = DataImportTypeName.NBF;
        //                break;
        //            case (short)ARTEnums.DataImportType.AccountUpload:
        //                importTypeName = DataImportTypeName.MAPPINGUPLOAD;
        //                break;
        //            case (short)ARTEnums.DataImportType.MultilingualUpload:
        //                importTypeName = DataImportTypeName.MAPPINGUPLOAD;
        //                break;
        //            case (short)ARTEnums.DataImportType.UserUpload:
        //                importTypeName = DataImportTypeName.USERUPLOAD;
        //                break;
        //            case (short)ARTEnums.DataImportType.GeneralTaskImport:
        //                importTypeName = DataImportTypeName.TASKIMPORT;
        //                break;
        //        }
        //        return importTypeName;
        //    }
        //    catch (ARTException ex)
        //    {
        //        throw ex;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        /// <summary>
        /// returns datatable from sheet1 of excel file as per FilePath
        /// </summary>
        /// <param name="FilePath">full path of excel file</param>
        /// <param name="Extension">extension of excel file</param>
        /// <returns>datatable from sheet1 of excel file</returns>
        public static DataTable GetDataTableFromExcel(string filePath, string fileExtension
            , short importType, StringBuilder oSBErrors)
        {

            OleDbCommand oCommandExcel = null;
            OleDbConnection oConnectionExcel = null;
            OleDbDataAdapter oDataAdapterExcel = null;
            DataSet dsExcel = null;
            DataTable dtExcel = null;
            IDataReader reader = null;

            try
            {
                oConnectionExcel = GetConnectionForExcelFile(filePath, fileExtension, true);
                oCommandExcel = new OleDbCommand();
                oDataAdapterExcel = new OleDbDataAdapter();
                dsExcel = new DataSet();
                oCommandExcel.Connection = oConnectionExcel;
                //Get the name of First Sheet 
                oConnectionExcel.Open();
                DataTable dtExcelSchema = oConnectionExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                string sheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
                string[] restrictions = { null, null, sheetName, null };
                DataTable mandatoryColumns = oConnectionExcel.GetSchema("Columns", restrictions);
                //scan mandatoryColumns datatable to make sure that all mandatory fields are there in excel
                string fieldList = "";

                //check for mandatory fields
                if (!AllMandatoryFieldsPresent(importType, mandatoryColumns, out fieldList))
                {
                    oSBErrors.Append(Helper.GetLabelIDValue(5000037));
                    throw new ARTException(5000037);//Invalid File. All Mandatory fields not present.
                }
                oCommandExcel.CommandText = "SELECT " + fieldList + " From [" + sheetName + "]";
                reader = oCommandExcel.ExecuteReader(CommandBehavior.CloseConnection);

                dtExcel = GetDataTableFromReader(reader, importType, oSBErrors);

                reader.Close();


                //Check for invalid data
                if (!IsValidDataTable(dtExcel) && importType != (short)ARTEnums.DataImportType.RecItems && importType != (short)ARTEnums.DataImportType.GeneralTaskImport)
                {
                    throw new ARTException(5000047);//Invalid Data
                }
                string DataLengtherrors = string.Empty;
                //Check for duplicate rows, data type
                switch (importType)
                {
                    case (short)ARTEnums.DataImportType.HolidayCalendar:
                        //MarkDuplicateRows(dtExcel, "Date", "Holiday Name");
                        MarkDuplicateRows(dtExcel, "Date");
                        DataLengtherrors = ValidateDataLengthForHolidayName(dtExcel);
                        if (!DataLengtherrors.Equals(string.Empty))
                        {
                            throw new Exception(DataLengtherrors);
                        }
                        break;
                    case (short)ARTEnums.DataImportType.PeriodEndDates:
                        MarkDuplicateRows(dtExcel, "Period #", "Period end date");
                        break;
                    case (short)ARTEnums.DataImportType.CurrencyExchangeRateData:
                        // MarkDuplicateRowsForCurrency(dtExcel, "Period", "From Currency Code", "To Currency Code", "Rate");
                        MarkDuplicateRowsForCurrency(dtExcel, CurrencyExchangeUploadConstants.UploadFields.PERIODENDDATE,
                            CurrencyExchangeUploadConstants.UploadFields.FROMCURRENCYCODE,
                            CurrencyExchangeUploadConstants.UploadFields.TOCURRENCYCODE);
                        MarkErrorRowsForCurrency(dtExcel);
                        DataLengtherrors = ValidateDataLengthForCurrency(dtExcel);
                        if (!DataLengtherrors.Equals(string.Empty))
                        {
                            throw new Exception(DataLengtherrors);
                        }
                        break;

                    case (short)ARTEnums.DataImportType.SubledgerSource:
                        MarkDuplicateRowsForCurrency(dtExcel, "Subledger Source Name");

                        MarkErrorRowsForSubledgerSource(dtExcel, "Subledger Source Name");
                        DataLengtherrors = ValidateDataLengthForSubLedger(dtExcel);
                        if (!DataLengtherrors.Equals(string.Empty))
                        {
                            throw new Exception(DataLengtherrors);
                        }
                        break;

                    case (short)ARTEnums.DataImportType.RecItems:
                        MarkDuplicateRowsForCurrency(dtExcel, "Date", "Description", "L-CCY Code", "AmountLocalCurrency", "RefNo");
                        break;
                    case (short)ARTEnums.DataImportType.ScheduleRecItems:
                        //MarkDuplicateRowsForCurrency(dtExcel, "Schedule Begin Date", "Schedule Amount", "Schedule Amount B-CCY", "Schedule Amount R-CCY", "L-CCY Code", "Ref No", "Description", "Schedule End Date", "Open Date");
                        MarkDuplicateRowsForCurrency(dtExcel, "Schedule Begin Date", "Schedule Amount", "L-CCY Code", "Ref No", "Description", "Schedule End Date", "Open Date", "ScheduleName");
                        break;
                    case (short)ARTEnums.DataImportType.GeneralTaskImport:
                        MarkDuplicateRowsForCurrency(dtExcel, "Task List Name", "Task Name", "Description", "Task Due Date", "TaskAssignee", "TaskApprover", "Assignee Due Date");
                        break;
                    case (short)ARTEnums.DataImportType.RecControlChecklist:
                        string[] rccColumns = GetRecControlChecklistMandatoryFields();
                        MarkDuplicateRows(dtExcel, rccColumns);
                        DataLengtherrors = ValidateDataLengthForRecControlChecklist(dtExcel);
                        if (!DataLengtherrors.Equals(string.Empty))
                        {
                            throw new Exception(DataLengtherrors);
                        }
                        break;
                    case (short)ARTEnums.DataImportType.RecControlChecklistAccount:
                        string[] rccColumnsa = GetRecControlChecklistMandatoryFields();
                        MarkDuplicateRows(dtExcel, rccColumnsa);
                        DataLengtherrors = ValidateDataLengthForRecControlChecklist(dtExcel);
                        if (!DataLengtherrors.Equals(string.Empty))
                        {
                            throw new Exception(DataLengtherrors);
                        }
                        break;
                }
                return dtExcel;
            }
            finally
            {
                if (oConnectionExcel != null && oConnectionExcel.State != ConnectionState.Closed)
                {
                    oConnectionExcel.Dispose();
                }
            }
        }

        private static string ValidateDataLengthForSubLedger(DataTable dtExcelData)
        {
            StringBuilder oSBError = new StringBuilder();
            string msg = DataImportHelper.GetDataLengthErrorMessage();
            for (int x = 0; x < dtExcelData.Rows.Count; x++)
            {
                DataRow dr = dtExcelData.Rows[x];
                string ExcelRowNumber = (x + 2).ToString();
                if (dr["Subledger Source Name"].ToString().Length > (int)ARTEnums.DataImportFieldsMaxLength.SubLedgerName)
                {
                    oSBError.Append(String.Format(msg, "Subledger Source Name", ExcelRowNumber, (int)ARTEnums.DataImportFieldsMaxLength.SubLedgerName));
                    oSBError.Append(" | ");
                }
            }
            return oSBError.ToString();
        }

        private static string ValidateDataLengthForCurrency(DataTable dtExcelData)
        {
            StringBuilder oSBError = new StringBuilder();
            string msg = DataImportHelper.GetFixedDataLengthErrorMessage();
            for (int x = 0; x < dtExcelData.Rows.Count; x++)
            {
                DataRow dr = dtExcelData.Rows[x];
                string ExcelRowNumber = (x + 2).ToString();
                if (dr[CurrencyExchangeUploadConstants.UploadFields.FROMCURRENCYCODE].ToString().Trim().Length != (int)CurrencyExchangeUploadConstants.DataLength.FROMCURRENCYCODE)
                {
                    oSBError.Append(String.Format(msg, CurrencyExchangeUploadConstants.UploadFields.FROMCURRENCYCODE, ExcelRowNumber, (int)CurrencyExchangeUploadConstants.DataLength.FROMCURRENCYCODE));
                    oSBError.Append(" | ");
                }
                if (dr[CurrencyExchangeUploadConstants.UploadFields.TOCURRENCYCODE].ToString().Trim().Length != (int)CurrencyExchangeUploadConstants.DataLength.TOCURRENCYCODE)
                {
                    oSBError.Append(String.Format(msg, CurrencyExchangeUploadConstants.UploadFields.TOCURRENCYCODE, ExcelRowNumber, (int)CurrencyExchangeUploadConstants.DataLength.TOCURRENCYCODE));
                    oSBError.Append(" | ");
                }
            }
            string errmsg = oSBError.ToString();
            if (errmsg.Length > 3)
                return errmsg.Substring(0, errmsg.Length - 3);
            return errmsg;
        }

        private static string ValidateDataLengthForRecControlChecklist(DataTable dtExcelData)
        {
            StringBuilder oSBError = new StringBuilder();
            string msg = DataImportHelper.GetDataLengthErrorMessage();
            for (int x = 0; x < dtExcelData.Rows.Count; x++)
            {
                DataRow dr = dtExcelData.Rows[x];
                string ExcelRowNumber = (x + 2).ToString();
                if (dr[RecControlChecklistUploadConstants.UploadFields.DESCRIPTION].ToString().Length > RecControlChecklistUploadConstants.DataLength.DESCRIPTION)
                {
                    oSBError.Append(String.Format(msg, RecControlChecklistUploadConstants.UploadFields.DESCRIPTION, ExcelRowNumber, RecControlChecklistUploadConstants.DataLength.DESCRIPTION));
                    oSBError.Append("|");
                }
            }
            return oSBError.ToString();
        }
        private static string ValidateDataLengthForHolidayName(DataTable dtExcelData)
        {
            StringBuilder oSBError = new StringBuilder();
            string msg = DataImportHelper.GetDataLengthErrorMessage();
            for (int x = 0; x < dtExcelData.Rows.Count; x++)
            {
                DataRow dr = dtExcelData.Rows[x];
                string ExcelRowNumber = (x + 2).ToString();
                if (dr["Holiday Name"].ToString().Length > (int)ARTEnums.DataImportFieldsMaxLength.HolidayName)
                {
                    oSBError.Append(String.Format(msg, "Holiday Name", ExcelRowNumber, (int)ARTEnums.DataImportFieldsMaxLength.HolidayName));
                    oSBError.Append("|");
                }
            }
            return oSBError.ToString();
        }

        public static string GetDataLengthErrorMessage()
        {
            //TODO: Use labelID
            //return "Field: {0}; Row: {1} Data cannot be more than {2} characters";
            //1827 
            return Helper.GetLabelIDValue(1827);
        }
        public static string GetFixedDataLengthErrorMessage()
        {
            return LanguageUtil.GetValue(5000423);
        }        
        /// <summary>
        /// This function determins if 
        /// </summary>
        /// <param name="importType"></param>
        /// <param name="dt"></param>
        /// <param name="ListOfFields"></param>
        /// <returns></returns>
        private static bool AllMandatoryFieldsPresent(short importType, DataTable dt, out string ListOfFields)
        {
            bool allMandatoryFieldsPresent = false;
            try
            {
                //TODO: Need to define a way where we can remove this hardcoded field names.
                //Column names for each data import.
                //Mandatory fields for each data import.
                //Unique key columns for each import.
                string[] fieldArray = null;
                switch (importType)
                {
                    case (short)ARTEnums.DataImportType.HolidayCalendar:
                        fieldArray = new string[] { "[Date]", "[Holiday Name]" };
                        break;
                    case (short)ARTEnums.DataImportType.PeriodEndDates:
                        fieldArray = new string[] { "[Period #]", "[Period end date]" };
                        break;
                    case (short)ARTEnums.DataImportType.CurrencyExchangeRateData:
                        fieldArray = new string[] { "[Period End Date]", "[From Currency Code]", "[To Currency Code]", "[Rate]" };
                        break;
                    case (short)ARTEnums.DataImportType.SubledgerSource:
                        fieldArray = new string[] { "[Subledger Source Name]" };
                        break;
                    case (short)ARTEnums.DataImportType.RecItems:
                        fieldArray = new string[] { "[Date]", "[Description]", "[L-CCY Code]", "[Amount L-CCY]", "[Ref No]" };
                        break;

                    case (short)ARTEnums.DataImportType.ScheduleRecItems:
                        //fieldArray = new string[] { "[Schedule Begin Date]", "[Schedule Amount]", "[L-CCY Code]", "[Ref No]", "[Description]", "[Schedule End Date]", "[Open Date]", "[ScheduleName]" };
                        fieldArray = new string[] { "[Ref No]", "[Schedule Name]", "[Description]", "[Original Amount]", "[L-CCY Code]", "[Open Date]", "[Begin Schedule On]", "[Don't Show on Rec Form]", "[Schedule Begin Date]", "[Schedule End Date]", "[Calculation Frequency]" };
                        break;
                    case (short)ARTEnums.DataImportType.GeneralTaskImport:
                        fieldArray = new string[] { "[Task List Name]", "[Task Name]", "[Description]", "[Task Due Date]", "[TaskAssignee]", "[TaskApprover]", "[Assignee Due Date]" };
                        break;
                    case (short)ARTEnums.DataImportType.RecControlChecklist:
                        fieldArray = GetRecControlChecklistMandatoryFields();
                        break;
                    case (short)ARTEnums.DataImportType.RecControlChecklistAccount:
                        fieldArray = GetRecControlChecklistMandatoryFields();
                        break;
                }
                foreach (string fieldName in fieldArray)
                {
                    allMandatoryFieldsPresent = false;
                    string newFieldName = string.Empty;
                    foreach (DataRow dr in dt.Rows)
                    {
                        newFieldName = "[" + dr["Column_Name"].ToString().Trim() + "]";
                        if (fieldName == newFieldName || "[" + fieldName + "]" == newFieldName)
                        {
                            allMandatoryFieldsPresent = true;
                            break;
                        }
                    }
                    if (!allMandatoryFieldsPresent)
                        break;
                }
                ListOfFields = string.Join(",", fieldArray);
                return allMandatoryFieldsPresent;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// This function finds out if datatable has duplicate rows or not
        /// </summary>
        /// <param name="dt">dattable to be evaluated for duplicate rows</param>
        /// <param name="columnNames">column names on wihich uniqueness needs to be determinded</param>
        /// <returns>true if duplicate rows found , else false</returns>
        /// 
        private static void MarkDuplicateRowsForCurrency(DataTable dt, params string[] columnNames)
        {

            //bool duplicateRowsPresent = false;
            //DataTable UniqueRowsDataTable = dt.DefaultView.ToTable(true, columnNames);
            //duplicateRowsPresent = (dt.Rows.Count != UniqueRowsDataTable.Rows.Count);
            //return duplicateRowsPresent;
            int rowcount = dt.Rows.Count;
            for (int i = 0; i < rowcount; i++)
            {
                DataRow sourceRow = dt.Rows[i];
                bool isValid = (bool)sourceRow["IsValidRow"];
                bool isDuplicate = (bool)sourceRow["IsDuplicate"];
                if (isValid && !isDuplicate)
                {
                    for (int j = i + 1; j < rowcount; j++)
                    {
                        DataRow targetRow = dt.Rows[j];
                        if ((bool)targetRow["IsValidRow"] && !(bool)targetRow["IsDuplicate"])
                        {
                            bool flag = true;
                            for (int x = 0; x < columnNames.Length; x++)
                            {
                                string colName = columnNames[x].ToString();
                                if (!sourceRow[colName].Equals(targetRow[colName]))
                                {
                                    flag = false;
                                    break;
                                }
                            }
                            targetRow["IsDuplicate"] = flag;
                        }
                    }
                }
            }
        }

        private static void MarkErrorRowsForSubledgerSource(DataTable dt, params string[] columnNames)
        {

            //Fetch All Sulledger  Source For the Company

            List<SubledgerSourceInfo> oSubledgerSourceInfoCollection = null;
            ICompany oICompany = RemotingHelper.GetCompanyObject();
            oSubledgerSourceInfoCollection = oICompany.SelectAllSubledgerSourceByCompanyID(SessionHelper.CurrentCompanyID, Helper.GetAppUserInfo());

            string SubledgerSourceName = "";

            int rowcount = dt.Rows.Count;
            string colName = columnNames[0].ToString();
            for (int i = 0; i < rowcount; i++)
            {
                DataRow sourceRow = dt.Rows[i];

                SubledgerSourceName = (from o in oSubledgerSourceInfoCollection
                                       where o.SubledgerSource.Equals(sourceRow[colName])
                                       select o.SubledgerSource).FirstOrDefault();
                if (SubledgerSourceName == null)
                {
                    sourceRow["IsError"] = false;

                }
                else
                {
                    sourceRow["IsError"] = true;

                }

            }
        }

        private static void MarkErrorRowsForCurrency(DataTable dt)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow sourceRow = dt.Rows[i];
                for (int j = i + 1; j < dt.Rows.Count; j++)
                {
                    DataRow destRow = dt.Rows[j];
                    if (sourceRow[CurrencyExchangeUploadConstants.UploadFields.FROMCURRENCYCODE].ToString().Trim() ==
                        destRow[CurrencyExchangeUploadConstants.UploadFields.TOCURRENCYCODE].ToString().Trim()
                        && sourceRow[CurrencyExchangeUploadConstants.UploadFields.TOCURRENCYCODE].ToString().Trim() ==
                        destRow[CurrencyExchangeUploadConstants.UploadFields.FROMCURRENCYCODE].ToString().Trim())
                    {
                        sourceRow["IsError"] = false;
                        destRow["IsError"] = false;
                        break;
                    }
                }
                if (sourceRow["IsError"] == DBNull.Value)
                    sourceRow["IsError"] = true;
            }
        }

        private static void MarkDuplicateRows(DataTable dt, params string[] columnNames)
        {

            //bool duplicateRowsPresent = false;
            //DataTable UniqueRowsDataTable = dt.DefaultView.ToTable(true, columnNames);
            //duplicateRowsPresent = (dt.Rows.Count != UniqueRowsDataTable.Rows.Count);
            //return duplicateRowsPresent;
            int rowcount = dt.Rows.Count;
            for (int i = 0; i < rowcount; i++)
            {
                DataRow sourceRow = dt.Rows[i];
                bool isValid = (bool)sourceRow["IsValidRow"];
                bool isDuplicate = (bool)sourceRow["IsDuplicate"];
                if (isValid && !isDuplicate)
                {
                    for (int j = i + 1; j < rowcount; j++)
                    {
                        DataRow targetRow = dt.Rows[j];
                        if ((bool)targetRow["IsValidRow"] && !(bool)targetRow["IsDuplicate"])
                        {
                            for (int x = 0; x < columnNames.Length; x++)
                            {
                                string colName = columnNames[x].ToString();
                                if (sourceRow[colName].Equals(targetRow[colName]))
                                {
                                    targetRow["IsDuplicate"] = true;
                                }
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Get DataTable from DataReader as per Import Type and StringBuilder containing errors in rows
        /// </summary>
        /// <param name="reader">DataReader to be converted to dataTable</param>
        /// <param name="importType">Import Type</param>
        /// <param name="oSBErrors">Stringbuilder for errors</param>
        /// <returns>DataTable</returns>
        private static DataTable GetDataTableFromReader(IDataReader reader, short importType, StringBuilder oSBErrors)
        {
            DataTable genericDT = null;

            switch ((ARTEnums.DataImportType)importType)
            {
                case ARTEnums.DataImportType.HolidayCalendar:
                    genericDT = GetHolidayCalendarDataTableFromReader(reader, oSBErrors);
                    break;
                case ARTEnums.DataImportType.PeriodEndDates:
                    genericDT = GetPeriodEndDateDataTableFromReader(reader, oSBErrors);
                    break;
                case ARTEnums.DataImportType.CurrencyExchangeRateData:
                    genericDT = GetExchangeRateTableFromReader(reader, oSBErrors);
                    break;
                case ARTEnums.DataImportType.SubledgerSource:
                    genericDT = GetSubledgerTableFromReader(reader, oSBErrors);
                    break;
                case ARTEnums.DataImportType.RecItems:
                    genericDT = GetRecItemsDataTableFromReader(reader, oSBErrors);
                    break;
                case ARTEnums.DataImportType.ScheduleRecItems:
                    genericDT = GetScheduleRecItemsDataTableFromReader(reader, oSBErrors);
                    break;
                case ARTEnums.DataImportType.GeneralTaskImport:
                    genericDT = GetGeneralTaskDataTableFromReader(reader, oSBErrors);
                    break;
                case ARTEnums.DataImportType.RecControlChecklist:
                    genericDT = GetRecControlChecklistDataTableFromReader(reader, oSBErrors);
                    break;
                case ARTEnums.DataImportType.RecControlChecklistAccount:
                    genericDT = GetRecControlChecklistDataTableFromReader(reader, oSBErrors);
                    break;
            }
            return genericDT;
        }

        /// <summary>
        /// Get DataTable for Rec items as per reader, get errors into StringBuilder
        /// </summary>
        /// <param name="reader">DataReader</param>
        /// <param name="oSBErrors">StringBuilder for errors</param>
        /// <returns>DataTable</returns>
        private static DataTable GetRecItemsDataTableFromReader(IDataReader reader, StringBuilder oSBErrors)
        {
            DataTable recItemsDT = new DataTable();
            DataColumn Date = new DataColumn("Date", Type.GetType("System.DateTime"));
            DataColumn Description = new DataColumn("Description", Type.GetType("System.String"));
            DataColumn LocalCurrencyCode = new DataColumn("L-CCY Code", Type.GetType("System.String"));
            DataColumn AmountLocalCurrency = new DataColumn("AmountLocalCurrency", Type.GetType("System.Decimal"));
            DataColumn RefNo = new DataColumn("RefNo", Type.GetType("System.String"));
            DataColumn IsValid = new DataColumn("IsValidRow", Type.GetType("System.Boolean"));
            DataColumn IsDuplicate = new DataColumn("IsDuplicate", Type.GetType("System.Boolean"));


            recItemsDT.Columns.Add(Date);
            recItemsDT.Columns.Add(Description);
            recItemsDT.Columns.Add(LocalCurrencyCode);
            recItemsDT.Columns.Add(AmountLocalCurrency);
            recItemsDT.Columns.Add(RefNo);
            recItemsDT.Columns.Add(IsValid);
            recItemsDT.Columns.Add(IsDuplicate);
            int count = 1;
            string errorFormat = Helper.GetLabelIDValue(1732);//"Invalid value for {0} column in row {1}"
            while (reader.Read())
            {
                DataRow dr = recItemsDT.NewRow();
                DateTime date;
                bool isValid = false;
                if (DateTime.TryParse(reader["Date"].ToString(), out date))
                {
                    dr["Date"] = date;
                    isValid = true;
                }
                else
                {
                    dr["Date"] = default(DateTime);
                    isValid = false;
                    oSBErrors.Append(String.Format(errorFormat, "Date", count.ToString()));
                    oSBErrors.Append(Environment.NewLine);
                }

                if (reader["Description"].ToString() != string.Empty)
                {
                    dr["Description"] = reader["Description"];
                }
                else
                {
                    dr["Description"] = string.Empty;
                    isValid = false;
                    oSBErrors.Append(String.Format(errorFormat, "Description", count.ToString()));
                    oSBErrors.Append(Environment.NewLine);
                }

                if (reader["L-CCY Code"].ToString() != string.Empty)
                {
                    dr["L-CCY Code"] = reader["L-CCY Code"];
                }
                else
                {
                    dr["L-CCY Code"] = string.Empty;
                    isValid = false;
                    oSBErrors.Append(String.Format(errorFormat, "L-CCY Code", count.ToString()));
                    oSBErrors.Append(Environment.NewLine);
                }

                decimal amount;
                if (Decimal.TryParse((reader["Amount L-CCY"].ToString()), out amount))
                {
                    dr["AmountLocalCurrency"] = amount;
                }
                else
                {
                    dr["AmountLocalCurrency"] = 0;
                    isValid = false;
                    oSBErrors.Append(String.Format(errorFormat, "AmountLocalCurrency", count.ToString()));
                    oSBErrors.Append(Environment.NewLine);
                }

                if (reader["Ref No"].ToString() != string.Empty)
                {
                    dr["RefNo"] = reader["Ref No"];
                }
                else
                {
                    dr["RefNo"] = string.Empty;
                }


                dr["IsValidRow"] = isValid;
                dr["IsDuplicate"] = false;
                recItemsDT.Rows.Add(dr);
                count++;
            }
            return recItemsDT;
        }

        private static DataTable GetScheduleRecItemsDataTableFromReader(IDataReader reader, StringBuilder oSBErrors)
        {
            DataTable recItemsDT = new DataTable();
            DataColumn ScheduleBeginDate = new DataColumn("Schedule Begin Date", Type.GetType("System.DateTime"));
            DataColumn ScheduleAmount = new DataColumn("Schedule Amount", Type.GetType("System.Decimal"));
            DataColumn AmountLCCY = new DataColumn("L-CCY Code", Type.GetType("System.String"));
            DataColumn Comments = new DataColumn("Description", Type.GetType("System.String"));
            DataColumn RefNo = new DataColumn("Ref No", Type.GetType("System.String"));
            DataColumn IsValid = new DataColumn("IsValidRow", Type.GetType("System.Boolean"));
            DataColumn IsDuplicate = new DataColumn("IsDuplicate", Type.GetType("System.Boolean"));
            DataColumn ScheduleEndDate = new DataColumn("Schedule End Date", Type.GetType("System.DateTime"));
            DataColumn OpenDate = new DataColumn("Open Date", Type.GetType("System.DateTime"));
            DataColumn ScheduleName = new DataColumn("ScheduleName", Type.GetType("System.String"));


            recItemsDT.Columns.Add(OpenDate);
            recItemsDT.Columns.Add(ScheduleBeginDate);
            recItemsDT.Columns.Add(ScheduleEndDate);
            recItemsDT.Columns.Add(ScheduleAmount);
            recItemsDT.Columns.Add(AmountLCCY);
            recItemsDT.Columns.Add(Comments);
            recItemsDT.Columns.Add(RefNo);
            recItemsDT.Columns.Add(IsValid);
            recItemsDT.Columns.Add(IsDuplicate);
            recItemsDT.Columns.Add(ScheduleName);

            int count = 1;
            string errorFormat = Helper.GetLabelIDValue(1732);//"Invalid value for {0} column in row {1}"
            while (reader.Read())
            {
                DataRow dr = recItemsDT.NewRow();
                DateTime date;
                bool isValid = true;
                if (DateTime.TryParse(reader["Open Date"].ToString(), out date))
                {
                    dr["Open Date"] = date;
                    isValid = true;
                }
                //else
                //{
                //    dr["Open Date"] = default(DateTime);
                //    isValid = false;
                //    oSBErrors.Append(String.Format(errorFormat, "Open Date", count.ToString()));
                //    oSBErrors.Append(Environment.NewLine);
                //}

                if (DateTime.TryParse(reader["Schedule Begin Date"].ToString(), out date))
                {
                    dr["Schedule Begin Date"] = date;
                    isValid = true;
                }
                //else
                //{
                //    dr["Schedule Begin Date"] = default(DateTime);
                //    isValid = false;
                //    oSBErrors.Append(String.Format(errorFormat, "Schedule Begin Date", count.ToString()));
                //    oSBErrors.Append(Environment.NewLine);
                //}
                decimal amount;
                if (Decimal.TryParse((reader["Schedule Amount"].ToString()), out amount))
                {
                    dr["Schedule Amount"] = amount;
                }
                else
                {
                    dr["Schedule Amount"] = string.Empty;
                    isValid = false;
                    oSBErrors.Append(String.Format(errorFormat, "Schedule Amount", count.ToString()));
                    oSBErrors.Append(Environment.NewLine);
                }

                if (reader["L-CCY Code"].ToString() != string.Empty)
                {
                    dr["L-CCY Code"] = reader["L-CCY Code"];
                }
                else
                {
                    dr["L-CCY Code"] = string.Empty;
                    isValid = false;
                    oSBErrors.Append(String.Format(errorFormat, "L-CCY Code", count.ToString()));
                    oSBErrors.Append(Environment.NewLine);
                }

                if (reader["Description"].ToString() != string.Empty)
                {
                    dr["Description"] = reader["Description"];
                }
                else
                {
                    dr["Description"] = string.Empty;
                }

                if (reader["Ref No"].ToString() != string.Empty)
                {
                    dr["Ref No"] = reader["Ref No"];
                }
                else
                {
                    dr["Ref No"] = string.Empty;
                }

                if (DateTime.TryParse(reader["Schedule End Date"].ToString(), out date))
                {
                    dr["Schedule End Date"] = date;
                    isValid = true;
                }
                //else
                //{
                //    dr["Schedule End Date"] = default(DateTime);
                //    isValid = false;
                //    oSBErrors.Append(String.Format(errorFormat, "Schedule End Date", count.ToString()));
                //    oSBErrors.Append(Environment.NewLine);
                //}
                if (reader["ScheduleName"].ToString() != string.Empty)
                {
                    dr["ScheduleName"] = reader["ScheduleName"];
                }
                else
                {
                    dr["ScheduleName"] = string.Empty;
                }


                dr["IsValidRow"] = isValid;
                dr["IsDuplicate"] = false;
                recItemsDT.Rows.Add(dr);
                count++;
            }
            return recItemsDT;
        }

        private static DataTable GetExchangeRateTableFromReader(IDataReader reader, StringBuilder oSBErrors)
        {
            DataTable CurrencyDT = new DataTable();
            DataColumn CurrencyPeriod = new DataColumn(CurrencyExchangeUploadConstants.UploadFields.PERIODENDDATE, Type.GetType("System.DateTime"));
            DataColumn CurrencyFrom = new DataColumn(CurrencyExchangeUploadConstants.UploadFields.FROMCURRENCYCODE, Type.GetType("System.String"));
            DataColumn CurrencyTo = new DataColumn(CurrencyExchangeUploadConstants.UploadFields.TOCURRENCYCODE, Type.GetType("System.String"));
            DataColumn CurrencyRate = new DataColumn(CurrencyExchangeUploadConstants.UploadFields.RATE, Type.GetType("System.Decimal"));

            DataColumn IsValid = new DataColumn(CurrencyExchangeUploadConstants.AddedFields.ISVALIDROW, Type.GetType("System.Boolean"));
            //DataColumn Description = new DataColumn("Description", Type.GetType("System.String"));
            DataColumn IsDuplicate = new DataColumn(CurrencyExchangeUploadConstants.AddedFields.ISDUPLICATE, Type.GetType("System.Boolean"));
            DataColumn IsError = new DataColumn(CurrencyExchangeUploadConstants.AddedFields.ISERROR, Type.GetType("System.Boolean"));

            CurrencyDT.Columns.Add(CurrencyPeriod);
            CurrencyDT.Columns.Add(CurrencyFrom);
            CurrencyDT.Columns.Add(CurrencyTo);
            CurrencyDT.Columns.Add(CurrencyRate);
            CurrencyDT.Columns.Add(IsValid);
            //periodEndDateDT.Columns.Add(Description);
            CurrencyDT.Columns.Add(IsDuplicate);
            CurrencyDT.Columns.Add(IsError);

            int count = 1;
            string errorFormat = Helper.GetLabelIDValue(1732);//"Invalid value for {0} column in row {1}"
            while (reader.Read())
            {
                DataRow dr = CurrencyDT.NewRow();
                DateTime period;
                decimal CurrencyExchangeRate;

                bool isValid = false;
                if (DateTime.TryParse(reader[CurrencyExchangeUploadConstants.UploadFields.PERIODENDDATE].ToString(), out period))
                {
                    dr[CurrencyExchangeUploadConstants.UploadFields.PERIODENDDATE] = period;
                    isValid = true;
                }
                else
                {
                    dr[CurrencyExchangeUploadConstants.UploadFields.PERIODENDDATE] = default(DateTime);
                    isValid = false;
                    oSBErrors.Append(String.Format(errorFormat, CurrencyExchangeUploadConstants.UploadFields.PERIODENDDATE, count.ToString()));
                    oSBErrors.Append(Environment.NewLine);
                }

                if (decimal.TryParse(reader[CurrencyExchangeUploadConstants.UploadFields.RATE].ToString(), out CurrencyExchangeRate))
                {
                    dr[CurrencyExchangeUploadConstants.UploadFields.RATE] = CurrencyExchangeRate;
                    isValid = true;
                }
                else
                {
                    isValid = false;
                    //description = "Invalid value for [Period end date] Column. ";
                    oSBErrors.Append(String.Format(errorFormat, CurrencyExchangeUploadConstants.UploadFields.RATE, count.ToString()));
                    oSBErrors.Append(Environment.NewLine);
                }
                if (reader[CurrencyExchangeUploadConstants.UploadFields.FROMCURRENCYCODE].ToString() != string.Empty)
                {
                    dr[CurrencyExchangeUploadConstants.UploadFields.FROMCURRENCYCODE] = reader[CurrencyExchangeUploadConstants.UploadFields.FROMCURRENCYCODE];
                }
                else
                {
                    isValid = false;
                    oSBErrors.Append(String.Format(errorFormat, CurrencyExchangeUploadConstants.UploadFields.FROMCURRENCYCODE, count.ToString()));
                    oSBErrors.Append(Environment.NewLine);
                }

                if (reader[CurrencyExchangeUploadConstants.UploadFields.TOCURRENCYCODE].ToString() != string.Empty)
                {
                    dr[CurrencyExchangeUploadConstants.UploadFields.TOCURRENCYCODE] = reader[CurrencyExchangeUploadConstants.UploadFields.TOCURRENCYCODE];
                }
                else
                {
                    isValid = false;
                    oSBErrors.Append(String.Format(errorFormat, CurrencyExchangeUploadConstants.UploadFields.TOCURRENCYCODE, count.ToString()));
                    oSBErrors.Append(Environment.NewLine);
                }

                dr[CurrencyExchangeUploadConstants.AddedFields.ISVALIDROW] = isValid;
                dr[CurrencyExchangeUploadConstants.AddedFields.ISDUPLICATE] = false;
                CurrencyDT.Rows.Add(dr);
                count++;
            }
            return CurrencyDT;
        }

        private static DataTable GetSubledgerTableFromReader(IDataReader reader, StringBuilder oSBErrors)
        {
            DataTable SubledgerDT = new DataTable();
            DataColumn SubledgerSource = new DataColumn("Subledger Source Name", Type.GetType("System.String"));
            DataColumn IsValid = new DataColumn("IsValidRow", Type.GetType("System.Boolean"));
            //DataColumn Description = new DataColumn("Description", Type.GetType("System.String"));
            DataColumn IsDuplicate = new DataColumn("IsDuplicate", Type.GetType("System.Boolean"));
            DataColumn IsError = new DataColumn("IsError", Type.GetType("System.Boolean"));

            SubledgerDT.Columns.Add(SubledgerSource);

            SubledgerDT.Columns.Add(IsValid);
            //periodEndDateDT.Columns.Add(Description);
            SubledgerDT.Columns.Add(IsDuplicate);
            SubledgerDT.Columns.Add(IsError);

            int count = 1;
            string errorFormat = Helper.GetLabelIDValue(1732);//"Invalid value for {0} column in row {1}"
            while (reader.Read())
            {
                DataRow dr = SubledgerDT.NewRow();
                bool isValid = false;

                if (reader["Subledger Source Name"].ToString() != string.Empty)
                {
                    dr["Subledger Source Name"] = reader["Subledger Source Name"];
                    isValid = true;
                }
                else
                {
                    isValid = false;
                    oSBErrors.Append(String.Format(errorFormat, "Subledger Source Name", count.ToString()));
                    oSBErrors.Append(Environment.NewLine);
                }



                dr["IsValidRow"] = isValid;
                dr["IsDuplicate"] = false;
                dr["IsError"] = false;
                SubledgerDT.Rows.Add(dr);
                count++;
            }
            return SubledgerDT;
        }

        /// <summary>
        /// Get DataTable for HolidayCalendar as per reader, get errors into StringBuilder
        /// </summary>
        /// <param name="reader">DataReader</param>
        /// <param name="oSBErrors">StringBuilder for errors</param>
        /// <returns>DataTable</returns>
        private static DataTable GetHolidayCalendarDataTableFromReader(IDataReader reader, StringBuilder oSBErrors)
        {
            DataTable holidayCalendarDT = new DataTable();
            DataColumn Date = new DataColumn("Date", Type.GetType("System.DateTime"));
            DataColumn HolidayName = new DataColumn("Holiday Name", Type.GetType("System.String"));
            DataColumn IsValid = new DataColumn("IsValidRow", Type.GetType("System.Boolean"));
            DataColumn IsDuplicate = new DataColumn("IsDuplicate", Type.GetType("System.Boolean"));
            //DataColumn Description = new DataColumn("Description", Type.GetType("System.String"));

            holidayCalendarDT.Columns.Add(Date);
            holidayCalendarDT.Columns.Add(HolidayName);
            holidayCalendarDT.Columns.Add(IsValid);
            holidayCalendarDT.Columns.Add(IsDuplicate);
            int count = 1;
            string errorFormat = Helper.GetLabelIDValue(1732);//"Invalid value for {0} column in row {1}"
            while (reader.Read())
            {
                DataRow dr = holidayCalendarDT.NewRow();
                DateTime holidayDate;
                bool isValid = false;
                if (DateTime.TryParse(reader["Date"].ToString(), out holidayDate))
                {
                    dr["Date"] = holidayDate;
                    isValid = true;
                }
                else
                {
                    dr["Date"] = default(DateTime);
                    isValid = false;
                    oSBErrors.Append(String.Format(errorFormat, "Date", count.ToString()));
                    oSBErrors.Append(Environment.NewLine);
                }

                if (reader["Holiday Name"].ToString() != string.Empty)
                {
                    dr["Holiday Name"] = reader["Holiday Name"];
                }
                else
                {
                    isValid = false;
                    oSBErrors.Append(String.Format(errorFormat, "Holiday Name", count.ToString()));
                    oSBErrors.Append(Environment.NewLine);
                }
                dr["IsValidRow"] = isValid;
                dr["IsDuplicate"] = false;
                holidayCalendarDT.Rows.Add(dr);
                count++;
            }
            return holidayCalendarDT;
        }

        /// <summary>
        /// Get DataTable for RecPeriod as per reader, get errors into StringBuilder
        /// </summary>
        /// <param name="reader">DataReader</param>
        /// <param name="oSBErrors">StringBuilder for errors</param>
        /// <returns>DataTable</returns>
        private static DataTable GetPeriodEndDateDataTableFromReader(IDataReader reader, StringBuilder oSBErrors)
        {
            DataTable periodEndDateDT = new DataTable();
            DataColumn PeriodNum = new DataColumn("Period #", Type.GetType("System.Int32"));
            DataColumn PeriodEndDate = new DataColumn("Period end date", Type.GetType("System.DateTime"));
            DataColumn IsValid = new DataColumn("IsValidRow", Type.GetType("System.Boolean"));
            //DataColumn Description = new DataColumn("Description", Type.GetType("System.String"));
            DataColumn IsDuplicate = new DataColumn("IsDuplicate", Type.GetType("System.Boolean"));

            periodEndDateDT.Columns.Add(PeriodNum);
            periodEndDateDT.Columns.Add(PeriodEndDate);
            periodEndDateDT.Columns.Add(IsValid);
            //periodEndDateDT.Columns.Add(Description);
            periodEndDateDT.Columns.Add(IsDuplicate);

            int count = 1;
            string errorFormat = Helper.GetLabelIDValue(1732);//"Invalid value for {0} column in row {1}"
            while (reader.Read())
            {
                DataRow dr = periodEndDateDT.NewRow();
                DateTime periodEndDate;
                int periodNum;
                bool isValid = false;
                if (DateTime.TryParse(reader["Period end date"].ToString(), out periodEndDate))
                {
                    dr["Period end date"] = periodEndDate;
                    isValid = true;
                }
                else
                {
                    dr["Period end date"] = default(DateTime);
                    isValid = false;
                    //description = "Invalid value for [Period end date] Column. ";
                    oSBErrors.Append(String.Format(errorFormat, "Period end date", count.ToString()));
                    oSBErrors.Append(Environment.NewLine);
                }

                if (Int32.TryParse(reader["Period #"].ToString(), out periodNum))
                {
                    dr["Period #"] = periodNum;
                }
                else
                {
                    isValid = false;
                    //description = description + " Invalid value for [Period #] Column";
                    oSBErrors.Append(String.Format(errorFormat, "Period #", count.ToString()));
                    oSBErrors.Append(Environment.NewLine);
                }

                dr["IsValidRow"] = isValid;
                dr["IsDuplicate"] = false;
                periodEndDateDT.Rows.Add(dr);
                count++;
            }
            return periodEndDateDT;
        }

        /// <summary>
        /// Get DataTable for Rec Contorl Checklist as per reader, get errors into StringBuilder
        /// </summary>
        /// <param name="reader">DataReader</param>
        /// <param name="oSBErrors">StringBuilder for errors</param>
        /// <returns>DataTable</returns>
        private static DataTable GetRecControlChecklistDataTableFromReader(IDataReader reader, StringBuilder oSBErrors)
        {
            DataTable rccDT = new DataTable();
            DataColumn Description = new DataColumn(RecControlChecklistUploadConstants.UploadFields.DESCRIPTION, Type.GetType("System.String"));
            DataColumn IsValid = new DataColumn(RecControlChecklistUploadConstants.AddedFields.ISVALIDROW, Type.GetType("System.Boolean"));
            DataColumn IsDuplicate = new DataColumn(RecControlChecklistUploadConstants.AddedFields.ISDUPLICATE, Type.GetType("System.Boolean"));

            rccDT.Columns.Add(Description);
            rccDT.Columns.Add(IsValid);
            rccDT.Columns.Add(IsDuplicate);

            int count = 1;
            string errorFormat = Helper.GetLabelIDValue(1732);//"Invalid value for {0} column in row {1}"
            while (reader.Read())
            {
                DataRow dr = rccDT.NewRow();
                string description = reader[RecControlChecklistUploadConstants.UploadFields.DESCRIPTION].ToString();
                bool isValid = false;
                if (!string.IsNullOrEmpty(description))
                {
                    dr[RecControlChecklistUploadConstants.UploadFields.DESCRIPTION] = description;
                    isValid = true;
                }
                else
                {
                    dr[RecControlChecklistUploadConstants.UploadFields.DESCRIPTION] = string.Empty;
                    isValid = false;
                    //description = "Invalid value for [Period end date] Column. ";
                    oSBErrors.Append(String.Format(errorFormat, RecControlChecklistUploadConstants.UploadFields.DESCRIPTION, count.ToString()));
                    oSBErrors.Append(Environment.NewLine);
                }
                dr[RecControlChecklistUploadConstants.AddedFields.ISVALIDROW] = isValid;
                dr[RecControlChecklistUploadConstants.AddedFields.ISDUPLICATE] = false;
                rccDT.Rows.Add(dr);
                count++;
            }
            return rccDT;
        }

        private static bool IsValidDataTable(DataTable dt)
        {
            bool isValidData = true;
            int invalidRows = dt.Select("IsValidRow = false").Length;
            if (invalidRows > 0)
                isValidData = false;
            return isValidData;
        }

        #region "GL Data Upload"

        /// <summary>
        /// Reads excel file and returns schema for its sheet as specified in parameter "sheetIndex"
        /// </summary>
        /// <param name="filePath">Physical path for file to be read</param>
        /// <param name="fileExtension">File extension</param>
        /// <param name="sheetIndex">zero base index for sheet</param>
        /// <returns>DataTable</returns>
        public static DataTable GetSchemaDataTableForExcelFile(string filePath, string fileExtension, short sheetIndex)
        {
            DataTable dtExcelSchema = null;
            OleDbConnection oConnectionExcel = null;
            string sheetName = "";
            DataTable dtAvailableColumns = null;
            try
            {
                oConnectionExcel = GetConnectionForExcelFile(filePath, fileExtension, true);
                oConnectionExcel.Open();
                dtExcelSchema = oConnectionExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                if (dtExcelSchema.Rows.Count > sheetIndex)
                {
                    sheetName = dtExcelSchema.Rows[sheetIndex]["TABLE_NAME"].ToString();
                    string[] restrictions = { null, null, sheetName, null };
                    dtAvailableColumns = oConnectionExcel.GetSchema("Columns", restrictions);
                }
                DataView dvColumns = new DataView(dtAvailableColumns);
                dvColumns.Sort = "ORDINAL_POSITION";
                return dvColumns.ToTable();
            }

            finally
            {
                if (oConnectionExcel != null && oConnectionExcel.State != ConnectionState.Closed)
                    oConnectionExcel.Dispose();
            }
        }

        //Get columns of sheet (which contains upload data) from excel file
        public static DataTable GetSchemaDataTableForExcelFile(string filePath, string fileExtension, string sheetName)
        {
            DataTable dtExcelSchema = null;
            OleDbConnection oConnectionExcel = null;
            DataTable dtAvailableColumns = null;
            short sheetIndex = -1;

            try
            {
                oConnectionExcel = GetConnectionForExcelFile(filePath, fileExtension, true);
                oConnectionExcel.Open();
                dtExcelSchema = oConnectionExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                sheetIndex = SharedDataImportHelper.FindSheetIndex(dtExcelSchema, sheetName);
                if (sheetIndex < 0)
                {
                    throw new Exception("SheetName not found");
                }
                if (dtExcelSchema.Rows.Count > sheetIndex)
                {
                    sheetName = dtExcelSchema.Rows[sheetIndex]["TABLE_NAME"].ToString();
                    string[] restrictions = { null, null, sheetName, null };
                    dtAvailableColumns = oConnectionExcel.GetSchema("Columns", restrictions);
                }
                DataView dvColumns = new DataView(dtAvailableColumns);
                dvColumns.Sort = "ORDINAL_POSITION";
                return dvColumns.ToTable();
            }
            finally
            {
                if (oConnectionExcel != null && oConnectionExcel.State != ConnectionState.Closed)
                    oConnectionExcel.Dispose();
            }
        }

        //Get columns of sheet (which contains upload data) from excel file
        public static DataTable GetSchemaDataTableForExcelFile(string filePath, string fileExtension, string sheetName, bool readHeader)
        {
            DataTable dtExcelSchema = null;
            OleDbConnection oConnectionExcel = null;
            // DataTable dtAvailableColumns = null;
            short sheetIndex = -1;
            // string allColumnName = "";
            string query = string.Empty;
            DataTable dt = new DataTable();
            OleDbDataReader oReaderExcel = null;
            try
            {
                oConnectionExcel = GetConnectionForExcelFile(filePath, fileExtension, readHeader);
                oConnectionExcel.Open();
                dtExcelSchema = oConnectionExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                sheetIndex = SharedDataImportHelper.FindSheetIndex(dtExcelSchema, sheetName);
                if (sheetIndex < 0)
                {
                    throw new Exception("SheetName not found");
                }
                if (dtExcelSchema.Rows.Count > sheetIndex)
                {
                    sheetName = dtExcelSchema.Rows[sheetIndex]["TABLE_NAME"].ToString();
                    //string[] restrictions = { null, null, sheetName, null };
                    //dtAvailableColumns = oConnectionExcel.GetSchema("Columns", restrictions);
                    //DataView dvColumns = new DataView(dtAvailableColumns);
                    //dvColumns.Sort = "ORDINAL_POSITION";

                    //allColumnName = DataImportHelper.GetColumnNames(dvColumns.ToTable());

                    //dtExcelSchema = null;

                    dt.Columns.Add("COLUMN_NAME", typeof(System.String));

                    query = "SELECT TOP 1 * FROM [" + sheetName + "]";
                    OleDbCommand oCommandExcel = new OleDbCommand(query, oConnectionExcel);
                    oReaderExcel = oCommandExcel.ExecuteReader();

                    if (oReaderExcel.HasRows)
                    {
                        oReaderExcel.Read();
                        for (int f = 0; f < oReaderExcel.FieldCount; f++)
                        {
                            if (!oReaderExcel.IsDBNull(f))
                            {
                                DataRow dr = dt.NewRow();
                                dr[0] = oReaderExcel.GetString(f).Replace("\n", " ").Trim();
                                dt.Rows.Add(dr);
                            }
                        }
                    }
                }
                return dt;
            }
            finally
            {
                if (oReaderExcel != null && !oReaderExcel.IsClosed)
                    oReaderExcel.Close();
                if (oConnectionExcel != null && oConnectionExcel.State != ConnectionState.Closed)
                    oConnectionExcel.Dispose();
            }
        }

        private static string GetColumnNames(DataTable dtGLDataSchema)
        {
            string ListOfFields = "";
            foreach (DataRow dr in dtGLDataSchema.Rows)
            {
                if (ListOfFields == "")
                    ListOfFields = ListOfFields + "[" + dr["Column_Name"].ToString() + "]";
                else
                    ListOfFields = ListOfFields + " , " + "[" + dr["Column_Name"].ToString().Trim() + "]";
            }
            return ListOfFields;
        }
        #endregion

        private static OleDbConnection GetConnectionForExcelFile(string filePath, string fileExtension, bool readHeader)
        {
            OleDbConnection oConnectionExcel = null;
            string conStr = "";
            if (readHeader)
                conStr = GetConnectionString(filePath, fileExtension, "YES");
            else
                conStr = GetConnectionString(filePath, fileExtension, "NO;IMEX=1");
            oConnectionExcel = new OleDbConnection(conStr);
            return oConnectionExcel;
        }

        public static string GetConnectionString(string filePath, string fileExtension, string readHeader)
        {
            string conStr = "";
            //switch (fileExtension)
            //{
            //    case ".xls": //Excel 97-03 
            //        conStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties='Excel 8.0;HDR={1}'";
            //        break;
            //    case ".xlsx": //Excel 07 
            //        conStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties='Excel 8.0;HDR={1}'";
            //        break;
            //}
            switch (fileExtension.ToLower())
            {
                case ".xls":
                    conStr = AppSettingHelper.GetAppSettingValue("xls");
                    break;
                case ".xlsx":
                    conStr = AppSettingHelper.GetAppSettingValue("xlsx");
                    break;
            }
            conStr = String.Format(conStr, filePath, readHeader);
            return conStr;
        }

        public static string[] GetGLDataImportMandatoryFields()
        {
            //{"Period End Date", "Company", "GL Account #"
            //, "GL Account Name", "FS Caption", "Account Type"
            //, "Base CCY Code", "Balance in Base CCY"
            //, "Balance in Reporting CCY","Reporting CCY Code"};
            string[] mandatoryFields = new string[GLDataImportFields.GLDATAIMPORTMANDATORYFIELDCOUNT];
            mandatoryFields[0] = GLDataImportFields.PERIODENDDATE;
            mandatoryFields[1] = GLDataImportFields.COMPANY;
            mandatoryFields[2] = GLDataImportFields.GLACCOUNTNUMBER;
            mandatoryFields[3] = GLDataImportFields.GLACCOUNTNAME;
            mandatoryFields[4] = GLDataImportFields.FSCAPTION;
            mandatoryFields[5] = GLDataImportFields.ACCOUNTTYPE;
            mandatoryFields[6] = GLDataImportFields.ISPROFITANDLOSS;
            mandatoryFields[7] = GLDataImportFields.BCCYCODE;
            mandatoryFields[8] = GLDataImportFields.BALANCEBCCY;
            mandatoryFields[9] = GLDataImportFields.BALANCERCCY;
            mandatoryFields[10] = GLDataImportFields.RCCYCODE;

            return mandatoryFields;
        }
        public static string[] GetMappingUploadImportMandatoryFields()
        {
            string[] mandatoryFields = new string[GLDataImportFields.MAPPINGUPLOADIMPORTMANDATORYFIELDCOUNT];
            //mandatoryFields[0] = GLDataImportFields.PERIODENDDATE;
            mandatoryFields[0] = GLDataImportFields.COMPANY;
            mandatoryFields[1] = GLDataImportFields.GLACCOUNTNUMBER;
            mandatoryFields[2] = GLDataImportFields.GLACCOUNTNAME;
            mandatoryFields[3] = GLDataImportFields.FSCAPTION;
            mandatoryFields[4] = GLDataImportFields.ACCOUNTTYPE;
            mandatoryFields[5] = AccountImportFields.ISPROFITANDLOSS;
            //mandatoryFields[6] = GLDataImportFields.BCCYCODE;
            //mandatoryFields[7] = GLDataImportFields.BALANCEBCCY;
            //mandatoryFields[8] = GLDataImportFields.BALANCERCCY;
            //mandatoryFields[9] = GLDataImportFields.RCCYCODE;
            return mandatoryFields;
           
        }

        public static string[] GetUserUploadImportMandatoryFields()
        {
            string[] mandatoryFields = new string[GLDataImportFields.USERUPLOADMANDATORYFIELDCOUNT];
            mandatoryFields[0] = GLDataImportFields.FIRSTNAME;
            mandatoryFields[1] = GLDataImportFields.LASTTNAME;
            mandatoryFields[2] = GLDataImportFields.LOGINID;
            mandatoryFields[3] = GLDataImportFields.EMAILID;
            mandatoryFields[4] = GLDataImportFields.DEFAULTROLE;

            return mandatoryFields;
        }

        //Company,FScaption,AccountType,Account#, AccountName are the only mandatory fields for Attribute load. Other Attribute fields are not mandatory
        public static string[] GetAcctAttrLoadMandatoryFields()
        {
            string[] mandatoryFields = new string[AccountAttributeLoadImportFields.ACCOUNTATTRIBUTEDATAIMPORTMANDATORYFIELDCOUNT];
            mandatoryFields[0] = AccountAttributeLoadImportFields.COMPANY;
            mandatoryFields[1] = AccountAttributeLoadImportFields.FSCAPTION;
            mandatoryFields[2] = AccountAttributeLoadImportFields.ACCOUNTTYPE;
            mandatoryFields[3] = AccountAttributeLoadImportFields.GLACCOUNTNUMBER;
            mandatoryFields[4] = AccountAttributeLoadImportFields.GLACCOUNTNAME;

            //mandatoryFields[5] = AccountAttributeLoadImportFields.ISKEYACCOUNT;
            //mandatoryFields[6] = AccountAttributeLoadImportFields.ISZEROBALANCEACCOUNT;
            //mandatoryFields[7] = AccountAttributeLoadImportFields.NATUREOFACCOUNT;
            //mandatoryFields[8] = AccountAttributeLoadImportFields.RECONCILIATIONPOLICY;
            //mandatoryFields[9] = AccountAttributeLoadImportFields.RECONCILIATIONPROCEDURE;
            //mandatoryFields[10] = AccountAttributeLoadImportFields.RECONCILIATIONTEMPLATE;
            //mandatoryFields[11] = AccountAttributeLoadImportFields.RISKRATING;
            //mandatoryFields[12] = AccountAttributeLoadImportFields.SUBLEDGERSOURCE;
            return mandatoryFields;

        }

        public static string[] GetSubledgerDataLoadMandatoryFields()
        {
            string[] mandatoryFields = new string[SubLedgerDataImportFields.MANDATORYFIELDCOUNT];
            mandatoryFields[0] = SubLedgerDataImportFields.PERIODENDDATE;
            mandatoryFields[1] = SubLedgerDataImportFields.GLACCOUNTNUMBER;
            mandatoryFields[2] = SubLedgerDataImportFields.GLACCOUNTNAME;
            mandatoryFields[3] = SubLedgerDataImportFields.FSCAPTION;
            mandatoryFields[4] = SubLedgerDataImportFields.ACCOUNTTYPE;
            mandatoryFields[5] = SubLedgerDataImportFields.ISPROFITANDLOSS;
            mandatoryFields[6] = SubLedgerDataImportFields.BCCYCODE;
            mandatoryFields[7] = SubLedgerDataImportFields.BALANCEBCCY;
            mandatoryFields[8] = SubLedgerDataImportFields.BALANCERCCY;
            mandatoryFields[9] = SubLedgerDataImportFields.RCCYCODE;

            return mandatoryFields;

        }

        //Added by Santosh for Matching GLTBS Data Import
        public static string[] GetGLTBSDataLoadMandatoryFields()
        {

            string[] KeyFields = SessionHelper.GetKeyFieldsByCompanyID().Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            string[] mandatoryFields = new string[KeyFields.Length + 5];
            //mandatoryFields[0] = GLDataImportFields.COMPANY;
            mandatoryFields[0] = GLDataImportFields.GLACCOUNTNUMBER;
            mandatoryFields[1] = GLDataImportFields.GLACCOUNTNAME;
            mandatoryFields[2] = GLDataImportFields.FSCAPTION;
            mandatoryFields[3] = GLDataImportFields.ACCOUNTTYPE;
            mandatoryFields[4] = GLDataImportFields.ISPROFITANDLOSS;
            int index = 5;
            for (int i = 0; i < KeyFields.Length; i++)
            {
                mandatoryFields[index] = KeyFields[i];
                index++;
            }
            return mandatoryFields;
        }
        //END
        public static string[] GetMultilingualUploadMandatoryFields()
        {
            string[] mandatoryFields = new string[MultilingualUploadConstants.MendatoryFieldCount];
            mandatoryFields[0] = MultilingualUploadConstants.Fields.LabelID;
            mandatoryFields[1] = MultilingualUploadConstants.Fields.FromLanguage;
            mandatoryFields[2] = MultilingualUploadConstants.Fields.ToLanguage;
            return mandatoryFields;
        }

        public static string[] GetScheduleRecItemImportMandatoryFields()
        {
            string[] mandatoryFields = new string[ScheduleRecItemUploadConstants.MendatoryFieldCount];
            mandatoryFields[0] = ScheduleRecItemUploadConstants.Fields.RefNo;
            mandatoryFields[1] = ScheduleRecItemUploadConstants.Fields.ScheduleName;
            mandatoryFields[2] = ScheduleRecItemUploadConstants.Fields.Description;
            mandatoryFields[3] = ScheduleRecItemUploadConstants.Fields.OriginalAmount;
            mandatoryFields[4] = ScheduleRecItemUploadConstants.Fields.LCCYCode;
            mandatoryFields[5] = ScheduleRecItemUploadConstants.Fields.OpenDate;
            mandatoryFields[6] = ScheduleRecItemUploadConstants.Fields.BeginScheduleOn;
            mandatoryFields[7] = ScheduleRecItemUploadConstants.Fields.IncludeOnBeginDate;
            mandatoryFields[8] = ScheduleRecItemUploadConstants.Fields.ScheduleBeginDate;
            mandatoryFields[9] = ScheduleRecItemUploadConstants.Fields.ScheduleEndDate;
            mandatoryFields[10] = ScheduleRecItemUploadConstants.Fields.CalculationFrequency;
            //mandatoryFields[11] = ScheduleRecItemUploadConstants.Fields.TotalInterval;
            //mandatoryFields[12] = ScheduleRecItemUploadConstants.Fields.CurrentInterval;
            return mandatoryFields;
        }

        public static string[] GetGeneralTaskImportMandatoryFields()
        {
            string[] mandatoryFields = new string[TaskUploadConstants.MendatoryFieldCount];
            mandatoryFields[0] = TaskUploadConstants.TaskUploadFields.TASKLISTNAME;
            mandatoryFields[1] = TaskUploadConstants.TaskUploadFields.TASKNAME;
            mandatoryFields[2] = TaskUploadConstants.TaskUploadFields.DESCRIPTION;
            mandatoryFields[3] = TaskUploadConstants.TaskUploadFields.ASSIGNEDTO;
            mandatoryFields[4] = TaskUploadConstants.TaskUploadFields.APPROVER;
            mandatoryFields[5] = TaskUploadConstants.TaskUploadFields.NOTIFY;
            mandatoryFields[6] = TaskUploadConstants.TaskUploadFields.RECURRENCETYPE;
            mandatoryFields[7] = TaskUploadConstants.TaskUploadFields.RECURRENCEFREQUENCY;
            mandatoryFields[8] = TaskUploadConstants.TaskUploadFields.PERIODNUMBER;
            mandatoryFields[9] = TaskUploadConstants.TaskUploadFields.ASSIGNEEDUEDATE;
            mandatoryFields[10] = TaskUploadConstants.TaskUploadFields.TASKDUEDATE;
            mandatoryFields[11] = TaskUploadConstants.TaskUploadFields.ASSIGNEEDUEDAYS;
            mandatoryFields[12] = TaskUploadConstants.TaskUploadFields.TASKDUEDAYS;
            return mandatoryFields;
        }

        public static string[] GetRecControlChecklistMandatoryFields()
        {
            string[] mandatoryFields = new string[RecControlChecklistUploadConstants.MendatoryFieldCount];
            mandatoryFields[0] = RecControlChecklistUploadConstants.UploadFields.DESCRIPTION;
            return mandatoryFields;
        }

        public static string[] GetCurrencyExchangeMandatoryFields()
        {
            string[] mandatoryFields = new string[CurrencyExchangeUploadConstants.MendatoryFieldCount];
            mandatoryFields[0] = CurrencyExchangeUploadConstants.UploadFields.PERIODENDDATE;
            mandatoryFields[1] = CurrencyExchangeUploadConstants.UploadFields.FROMCURRENCYCODE;
            mandatoryFields[2] = CurrencyExchangeUploadConstants.UploadFields.TOCURRENCYCODE;
            mandatoryFields[3] = CurrencyExchangeUploadConstants.UploadFields.RATE;
            return mandatoryFields;
        }

        //Check if sheet with specified index has some keys or not
        public static bool IsKeyFieldAvailable(string fullFileName, string fileExtension, short sheetIndex)
        {

            DataTable dtSchema = GetSchemaDataTableForExcelFile(fullFileName, fileExtension, sheetIndex);
            string[] arryMandatoryFields = GetGLDataImportMandatoryFields();
            return (dtSchema.Rows.Count > arryMandatoryFields.Length);

        }

        //check if sheet with specified name has some keys or not
        public static bool IsKeyFieldAvailable(string fullFileName, string fileExtension, string sheetName, ARTEnums.DataImportType dataImportType)
        {
            DataTable dtSchema;
            if (fileExtension.ToLower() == FileExtensions.csv)
                dtSchema = ExcelHelper.GetDelimitedFileSchema(fullFileName);
            else
                dtSchema = GetSchemaDataTableForExcelFile(fullFileName, fileExtension, sheetName);

            string[] arryMandatoryFields = null;
            switch (dataImportType)
            {
                case ARTEnums.DataImportType.GLData:
                    arryMandatoryFields = GetGLDataImportMandatoryFields();
                    break;
                case ARTEnums.DataImportType.AccountUpload:
                    arryMandatoryFields = GetMappingUploadImportMandatoryFields();
                    break;
            }
            return (dtSchema.Rows.Count > arryMandatoryFields.Length);


        }

        public static bool IsAllMandatoryColumnsPresentForDataImport(ARTEnums.DataImportType dataImportType, string fullFileName, string fileExtension, int? ImportTemplateID, string ImportsheetName, out string errorMessage)
        {
            errorMessage = "";
            DataTable dtSchema = null;
            StringBuilder oSbError = null;
            string[] arryMandatoryFields = null;
            string sheetName = "";
            try
            {
                //on the basis of dataImportType, get sheetName and array of mandatory fields 
                switch (dataImportType)
                {
                    case ARTEnums.DataImportType.GLData:
                        sheetName = ImportsheetName;
                        List<string> tmp;
                        List<string> tmpMandatoryFieldList = GetGLDataImportAllMandatoryFields(SessionHelper.CurrentCompanyID.Value, SessionHelper.CurrentReconciliationPeriodID.Value);//GetGLDataImportMandatoryFields();
                        if (ImportTemplateID.HasValue && ImportTemplateID.Value != Convert.ToInt32(WebConstants.ART_TEMPLATE))
                            tmp = GetImportTemplateMandatoryFields(SessionHelper.CurrentCompanyID, ImportTemplateID, tmpMandatoryFieldList);
                        else
                            tmp = tmpMandatoryFieldList;
                        arryMandatoryFields = tmp.ToArray<string>();
                        break;

                    case ARTEnums.DataImportType.SubledgerData:
                        sheetName = ImportsheetName;
                        List<string> tmpSubLedger;
                        List<string> tmpMandatorySubLedgerFieldList = GetSubledgerDataImportAllMandatoryFields(SessionHelper.CurrentCompanyID.Value, SessionHelper.CurrentReconciliationPeriodID.Value);//GetSubledgerDataLoadMandatoryFields();
                        if (ImportTemplateID.HasValue && ImportTemplateID.Value != Convert.ToInt32(WebConstants.ART_TEMPLATE))
                            tmpSubLedger = GetImportTemplateMandatoryFields(SessionHelper.CurrentCompanyID, ImportTemplateID, tmpMandatorySubLedgerFieldList);
                        else
                            tmpSubLedger = tmpMandatorySubLedgerFieldList;
                        arryMandatoryFields = tmpSubLedger.ToArray<string>();
                        break;

                    case ARTEnums.DataImportType.AccountAttributeList:
                        sheetName = WebConstants.ACCOUNTATTRIBUTE_SHEETNAME;
                        //arryMandatoryFields = GetAcctAttrLoadMandatoryFields();
                        List<string> tmpAccountAttr = GetAccountAttrDataImportAllMandatoryFields(SessionHelper.CurrentCompanyID.Value, SessionHelper.CurrentReconciliationPeriodID.Value);
                        arryMandatoryFields = tmpAccountAttr.ToArray<string>();
                        break;

                    //Added by Santosh for Matching GLTBS Mandatory Column
                    case ARTEnums.DataImportType.GLTBS:
                        sheetName = WebConstants.MATCHING_SHEETNAME;
                        List<string> tmpGLTBS = GetGLTBSDataImportAllMandatoryFields(SessionHelper.CurrentCompanyID.Value, SessionHelper.CurrentReconciliationPeriodID.Value);
                        arryMandatoryFields = tmpGLTBS.ToArray<string>();
                        break;
                    //End
                    case ARTEnums.DataImportType.AccountUpload:
                        sheetName = WebConstants.MAPPINGUPLOAD_SHEETNAME;
                        arryMandatoryFields = GetMappingUploadImportMandatoryFields();
                        break;

                    case ARTEnums.DataImportType.MultilingualUpload:
                        sheetName = MultilingualUploadConstants.SheetName;
                        arryMandatoryFields = GetMultilingualUploadMandatoryFields();
                        break;

                    case ARTEnums.DataImportType.UserUpload:
                        sheetName = WebConstants.USERUPLOAD_SHEETNAME;
                        arryMandatoryFields = GetUserUploadImportMandatoryFields();
                        break;

                    case ARTEnums.DataImportType.ScheduleRecItems:
                        sheetName = ScheduleRecItemUploadConstants.SheetName;
                        arryMandatoryFields = GetScheduleRecItemImportMandatoryFields();
                        break;
                    case ARTEnums.DataImportType.GeneralTaskImport:
                        sheetName = TaskUploadConstants.SheetName;
                        arryMandatoryFields = GetGeneralTaskImportMandatoryFields();
                        break;
                    case ARTEnums.DataImportType.RecControlChecklist:
                        sheetName = RecControlChecklistUploadConstants.SheetName;
                        arryMandatoryFields = GetRecControlChecklistMandatoryFields();
                        break;
                    case ARTEnums.DataImportType.CurrencyExchangeRateData:
                        sheetName = CurrencyExchangeUploadConstants.SheetName;
                        arryMandatoryFields = GetCurrencyExchangeMandatoryFields();
                        break;
                }
                //dtSchema = GetSchemaDataTableForExcelFile(fullFileName, fileExtension, sheetIndex);
                if (fileExtension.ToLower() == FileExtensions.csv)
                    dtSchema = ExcelHelper.GetDelimitedFileSchema(fullFileName);
                else
                    dtSchema = GetSchemaDataTableForExcelFile(fullFileName, fileExtension, sheetName, false);
                oSbError = new StringBuilder();
                string errorPhrase = LanguageUtil.GetValue(5000165);

                //find out if all mandatory fields are present or not
                for (int i = 0; i < arryMandatoryFields.Length; i++)
                {
                    string fieldName = arryMandatoryFields[i].ToString();
                    bool columnFound = false;
                    foreach (DataRow dr in dtSchema.Rows)
                    {
                        if (dr["Column_Name"].ToString().Trim().ToUpper() == fieldName.ToUpper())
                        {
                            columnFound = true;
                            break;
                        }
                    }
                    if (!columnFound)
                    {
                        //throw new Exception("Mandatory columns not present: " + fieldName);
                        if (!oSbError.ToString().Equals(string.Empty))
                            oSbError.Append(WebConstants.MANDATORYFIELSDNAMESEPERATOR);
                        oSbError.Append(fieldName);

                    }
                }
                errorMessage = String.Format(errorPhrase, oSbError.ToString());
            }
            catch (Exception ex)
            {
                Helper.LogException(ex);
                oSbError = new StringBuilder();
                oSbError.Append(LanguageUtil.GetValue(5000071));
                errorMessage = oSbError.ToString();
                //throw new ARTException(5000071);//Error while reading uploaded file.
            }

            return (oSbError.ToString().Equals(string.Empty));
        }

        public static bool IsDeletePermittedByRecPeriodStatus()
        {
            ReconciliationPeriodStatusMstInfo oReconciliationPeriodStatusMstInfo = SessionHelper.GetRecPeriodStatus();
            bool isDeletable = false;
            switch (oReconciliationPeriodStatusMstInfo.ReconciliationPeriodStatusID.Value)
            {
                case (short)WebEnums.RecPeriodStatus.NotStarted:
                case (short)WebEnums.RecPeriodStatus.Open:
                    isDeletable = true;
                    break;

                default:
                    isDeletable = false;
                    break;
            }
            return isDeletable;
        }

        public static void GetCompanyDataStorageCapacityAndCurrentUsage(out decimal? dataStorageCapacity, out decimal? currentUsage)
        {
            dataStorageCapacity = 0;
            currentUsage = 0;

            ICompany oCompanyClient = RemotingHelper.GetCompanyObject();
            oCompanyClient.GetCompanyDataStorageCapacityAndCurrentUsage(SessionHelper.CurrentCompanyID.Value, out dataStorageCapacity, out currentUsage, Helper.GetAppUserInfo());
        }

        /// <summary>
        /// Gets folder name as per company id for Attachment upload. 
        /// </summary>
        /// <param name="companyID">id of the company</param>
        /// <param name="importType">import type</param>
        /// <returns>folder name</returns>
        public static string GetFolderForAttachment(int companyID, int recPeriodID)
        {
            // There will be a Base folder(path from web config). 
            // Within Base Folder, there will be folders 
            // for each company as per CompanyId at the time of company creation.
            // If CompanyID folder does not exist create a folder by the name of companyID 
            // Base folder + companyId 

            string baseFolderPath = @"";//read base folder path from web config.
            string companyFolder = @"";

            //Read base folder name and physical path
            baseFolderPath = SharedDataImportHelper.GetBaseFolderForCompany(companyID);

            companyFolder = baseFolderPath + @"\" + AppSettingHelper.GetAppSettingValue(AppSettingConstants.FOLDER_FOR_ATTACHMENTS);

            if (!Directory.Exists(companyFolder))
            {
                Directory.CreateDirectory(companyFolder);
            }

            companyFolder = companyFolder + @"\" + recPeriodID.ToString();
            if (!Directory.Exists(companyFolder))
            {
                Directory.CreateDirectory(companyFolder);
            }

            return companyFolder + @"\"; ;
        }

        public static bool IsGLDataUploaded(int recPeriodID, ARTEnums.DataImportType dataImportTypeID, WebEnums.DataImportStatus dataImportStatusID)
        {
            IDataImport oDataImportClient = RemotingHelper.GetDataImportObject();
            return oDataImportClient.IsGLDataUploaded(recPeriodID, (byte)dataImportTypeID, (byte)dataImportStatusID, Helper.GetAppUserInfo());
        }

        
      

      


        public static void DeleteFile(string filePath)
        {
            //Delete the saved file
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }

        //Returns list of static account fields
        public static List<string> GetAccountStaticFields()
        {
            List<string> fieldList = new List<string>();
            fieldList.Add(AccountImportFields.FSCAPTION);
            fieldList.Add(AccountImportFields.ACCOUNTTYPE);
            fieldList.Add(AccountImportFields.GLACCOUNTNAME);
            fieldList.Add(AccountImportFields.GLACCOUNTNUMBER);

            return fieldList;
        }

        //Returns list of Account key fields
        public static List<string> GetAccountKeyFields(int companyID)
        {
            IUtility oClient = RemotingHelper.GetUtilityObject();
            string keyFields = oClient.GetKeyFieldsByCompanyID(companyID, Helper.GetAppUserInfo());
            return keyFields.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList<string>();
        }

        //Returns list of Account Unique Subset keys
        public static List<string> GetAccountUniqueSubsetFields(int companyID, int recPeriodID)
        {
            IUtility oClient = RemotingHelper.GetUtilityObject();
            string keyFields = oClient.GetAccountUniqueSubsetKeys(companyID, recPeriodID, Helper.GetAppUserInfo());

            string[] arryAccountUniqueKeys = keyFields.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            return arryAccountUniqueKeys.ToList<string>();
        }

        //Returns Account fields (if mapping is done then unique subset keys, else static fields + key fields)
        public static List<string> GetAccountFields(int companyID, int recPeriodID)
        {
            List<string> fieldList = new List<string>();
            List<string> uniqueSubSetFielsList = GetAccountUniqueSubsetFields(companyID, recPeriodID);
            if (uniqueSubSetFielsList.Count > 0)
            {
                //Add Account Unique Subset Keys
                fieldList.AddRange(uniqueSubSetFielsList);
            }
            else
            {
                fieldList.AddRange(GetAllPossibleAccountFields(companyID));
            }

            return fieldList;
        }

        //Returns Account fields (if mapping is done then unique subset keys, else static fields + key fields)
        public static List<string> GetAccountFields(int companyID, int recPeriodID, bool IsColumnsOptional)
        {
            List<string> fieldList = new List<string>();
            List<string> uniqueSubSetFielsList = GetAccountUniqueSubsetFields(companyID, recPeriodID);
            if (uniqueSubSetFielsList.Count > 0)
            {
                //Add Account Unique Subset Keys
                fieldList.AddRange(uniqueSubSetFielsList);
            }
            else
            {
                fieldList.AddRange(GetAllPossibleAccountFields(companyID));
                fieldList.AddRange(GetAllAccountCreationMendatoryFields());
            }

            return fieldList;
        }

        //Returns all possible Account Fields (tatic fields + key fields)
        public static List<string> GetAllPossibleAccountFields(int companyID)
        {
            List<string> fieldList = new List<string>();
            //Add Account Static Fields
            fieldList.AddRange(GetAccountStaticFields());
            //Add mapped key fields
            fieldList.AddRange(GetAccountKeyFields(companyID));

            return fieldList;
        }

        //Returns GLdata import mandatory fields other than accountfields
        public static List<string> GetGLDataImportMandatoryFields_New()
        {
            List<string> fieldList = new List<string>();
            fieldList.Add(GLDataImportFields.PERIODENDDATE);
            // fieldList.Add(GLDataImportFields.COMPANY);
            fieldList.Add(GLDataImportFields.BCCYCODE);
            fieldList.Add(GLDataImportFields.BALANCEBCCY);
            fieldList.Add(GLDataImportFields.BALANCERCCY);
            fieldList.Add(GLDataImportFields.RCCYCODE);
            return fieldList;
        }

        public static List<string> GetAllAccountCreationMendatoryFields()
        {
            List<string> fieldList = new List<string>();
            fieldList.Add(AccountImportFields.ISPROFITANDLOSS);
            return fieldList;
        }

        //Returns all possible fields in GLDataImport
        public static List<string> GetAllPossibleGLDataImportFields(int companyID, int recPeriodID)
        {
            List<string> fieldList = new List<string>();
            fieldList.AddRange(GetGLDataImportMandatoryFields_New());//Period, Company, Currency fields
            fieldList.AddRange(GetAllPossibleAccountFields(companyID));
            return fieldList;
        }

        //Returns list of all mandatory fields for GLDataImport
        public static List<string> GetGLDataImportAllMandatoryFields(int companyID, int recPeriodID)
        {
            List<string> fieldList = new List<string>();

            fieldList.AddRange(GetGLDataImportMandatoryFields_New());
            fieldList.AddRange(GetAccountFields(companyID, recPeriodID, true));
            //fieldList.AddRange(GetAllAccountCreationMendatoryFields());
            return fieldList;
        }

        //Returns a list of all possible mandatory fields for SubLedger data Import
        public static List<string> GetSubledgerDataImportAllMandatoryFields(int companyID, int recPeriodID)
        {
            List<string> fieldList = new List<string>();
            //Add fields which must be present in any condition
            fieldList.Add(SubLedgerDataImportFields.PERIODENDDATE);
            fieldList.Add(SubLedgerDataImportFields.BCCYCODE);
            fieldList.Add(SubLedgerDataImportFields.BALANCEBCCY);
            fieldList.Add(SubLedgerDataImportFields.BALANCERCCY);
            fieldList.Add(SubLedgerDataImportFields.RCCYCODE);

            fieldList.AddRange(GetAccountFields(companyID, recPeriodID, true));
            //fieldList.AddRange(GetAllAccountCreationMendatoryFields());
            return fieldList;
        }

        //Returns a list of all possible mandatory fields for Account Attribute data Import
        public static List<string> GetAccountAttrDataImportAllMandatoryFields(int companyID, int recPeriodID)
        {
            List<string> fieldList = new List<string>();

            //Add fields which must be present in any condition
            fieldList.Add(AccountAttributeLoadImportFields.COMPANY);

            fieldList.AddRange(GetAccountFields(companyID, recPeriodID));

            return fieldList;
        }

        //Returns a list of all possible mandatory fields for GLTBS data Import
        public static List<string> GetGLTBSDataImportAllMandatoryFields(int companyID, int recPeriodID)
        {
            List<string> fieldList = new List<string>();

            fieldList.AddRange(GetAccountFields(companyID, recPeriodID));
            fieldList.AddRange(GetAllAccountCreationMendatoryFields());

            return fieldList;
        }

        /// <summary>
        /// Get General Task data table From data reader
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="oSBErrors"></param>
        /// <returns></returns>
        private static DataTable GetGeneralTaskDataTableFromReader(IDataReader reader, StringBuilder oSBErrors)
        {
            DataTable generalTasksDT = new DataTable();
            DataColumn TaskListName = new DataColumn("Task List Name", Type.GetType("System.String"));
            DataColumn TaskName = new DataColumn("Task Name", Type.GetType("System.String"));
            DataColumn Description = new DataColumn("Description", Type.GetType("System.String"));
            DataColumn TaskDueDate = new DataColumn("Task Due Date", Type.GetType("System.DateTime"));
            DataColumn TaskAssignee = new DataColumn("TaskAssignee", Type.GetType("System.String"));
            DataColumn TaskApprover = new DataColumn("TaskApprover", Type.GetType("System.String"));
            DataColumn AssigneeDueDate = new DataColumn("Assignee Due Date", Type.GetType("System.DateTime"));

            DataColumn IsValid = new DataColumn("IsValidRow", Type.GetType("System.Boolean"));
            DataColumn IsDuplicate = new DataColumn("IsDuplicate", Type.GetType("System.Boolean"));

            generalTasksDT.Columns.Add(TaskListName);
            generalTasksDT.Columns.Add(TaskName);
            generalTasksDT.Columns.Add(Description);
            generalTasksDT.Columns.Add(TaskDueDate);
            generalTasksDT.Columns.Add(TaskAssignee);
            generalTasksDT.Columns.Add(TaskApprover);
            generalTasksDT.Columns.Add(AssigneeDueDate);

            generalTasksDT.Columns.Add(IsValid);
            generalTasksDT.Columns.Add(IsDuplicate);
            int count = 2;
            string errorFormat = Helper.GetLabelIDValue(1732);//"Invalid value for {0} column in row {1}"
            List<UserHdrInfo> oListUserHdrInfo = CacheHelper.SelectAllUsersForCurrentCompany(); //get All user for current company

            while (reader.Read())
            {
                DataRow dr = generalTasksDT.NewRow();
                DateTime date;
                List<DateTime> TaskDateList = new List<DateTime>();
                bool isValid = true;

                if (reader["Task List Name"].ToString() != string.Empty)
                {
                    dr["Task List Name"] = reader["Task List Name"];
                }
                else
                {
                    dr["Task List Name"] = string.Empty;
                    isValid = false;
                    oSBErrors.Append(String.Format(errorFormat, "Task List Name", count.ToString()));
                    oSBErrors.Append(Environment.NewLine);

                }

                if (reader["Task Name"].ToString() != string.Empty)
                {
                    dr["Task Name"] = reader["Task Name"];
                }
                else
                {
                    dr["Task Name"] = string.Empty;
                    isValid = false;
                    oSBErrors.Append(String.Format(errorFormat, "Task Name", count.ToString()));
                    oSBErrors.Append(Environment.NewLine);

                }

                if (reader["Description"].ToString() != string.Empty)
                {
                    dr["Description"] = reader["Description"];
                }
                string Asignee = reader["TaskAssignee"].ToString();
                if (Asignee != string.Empty)
                {
                    UserHdrInfo oUser = oListUserHdrInfo.Where(user => user.LoginID.ToLower() == Asignee.ToLower()).FirstOrDefault();
                    dr["TaskAssignee"] = Asignee;
                    if (oUser == null)
                    {
                        isValid = false;
                        oSBErrors.Append(String.Format(errorFormat, "TaskAssignee", count.ToString()));
                        oSBErrors.Append(Environment.NewLine);

                    }
                }
                else
                {
                    dr["TaskAssignee"] = string.Empty;
                }

                string Approver = reader["TaskApprover"].ToString();
                if (Approver != string.Empty)
                {
                    UserHdrInfo oUser = oListUserHdrInfo.Where(user => user.LoginID.ToLower() == Approver.ToLower()).FirstOrDefault();
                    dr["TaskApprover"] = Approver;
                    if (oUser == null)
                    {
                        isValid = false;
                        oSBErrors.Append(String.Format(errorFormat, "TaskApprover", count.ToString()));
                        oSBErrors.Append(Environment.NewLine);

                    }
                }
                else
                {
                    dr["TaskApprover"] = string.Empty;
                }

                if (!string.IsNullOrEmpty(Asignee) && !string.IsNullOrEmpty(Approver))
                {
                    if (Asignee.ToLower().Equals(Approver.ToLower()))
                    {
                        isValid = false;
                        oSBErrors.Append(string.Format(Helper.GetLabelIDValue(5000354), Helper.GetLabelIDValue(2564), Helper.GetLabelIDValue(1132), LanguageUtil.GetValue(2525)));
                        oSBErrors.Append("<br/>");
                    }
                }
                if (Approver != string.Empty)
                {
                    if (DateTime.TryParse(reader["Assignee Due Date"].ToString(), out date))
                    {
                        dr["Assignee Due Date"] = date;
                        if (date < Convert.ToDateTime(DateTime.Now))
                        {
                            isValid = false;
                            oSBErrors.Append(String.Format(errorFormat, "Assignee Due Date", count.ToString()));
                            oSBErrors.Append(Environment.NewLine);
                        }
                        else
                            TaskDateList.Add(date);

                    }
                    else
                    {
                        dr["Assignee Due Date"] = reader["Assignee Due Date"];
                        isValid = false;
                        oSBErrors.Append(String.Format(errorFormat, "Assignee Due Date", count.ToString()));
                        oSBErrors.Append(Environment.NewLine);

                    }
                }
                if (DateTime.TryParse(reader["Task Due Date"].ToString(), out date))
                {
                    dr["Task Due Date"] = date;
                    if (date < Convert.ToDateTime(DateTime.Today))
                    {
                        isValid = false;
                        oSBErrors.Append(String.Format(errorFormat, "Task Due Date", count.ToString()));
                        oSBErrors.Append(Environment.NewLine);
                    }
                    else
                        TaskDateList.Add(date);

                }
                else
                {
                    dr["Task Due Date"] = reader["Task Due Date"];
                    isValid = false;
                    oSBErrors.Append(String.Format(errorFormat, "Task Due Date", count.ToString()));
                    oSBErrors.Append(Environment.NewLine);

                }

                if (Helper.DatesInOrder(TaskDateList))
                {
                }
                else
                {
                    string errorMessage = LanguageUtil.GetValue(5000342);
                    isValid = false;
                    oSBErrors.Append(string.Format(errorMessage, Helper.GetLabelIDValue(2582)));
                    oSBErrors.Append("<br/>");
                }

                dr["IsValidRow"] = isValid;
                dr["IsDuplicate"] = false;
                generalTasksDT.Rows.Add(dr);
                count++;
            }
            return generalTasksDT;
        }

        public static void GetCompanyDataStorageCapacityAndCurrentUsage(int companyID, out decimal? dataStorageCapacity, out decimal? currentUsage)
        {
            dataStorageCapacity = 0;
            currentUsage = 0;

            ICompany oCompanyClient = RemotingHelper.GetCompanyObject();
            oCompanyClient.GetCompanyDataStorageCapacityAndCurrentUsage(companyID, out dataStorageCapacity, out currentUsage, Helper.GetAppUserInfo());
        }

        public static void UpdateCompanyDataStorageCapacityAndCurrentUsage(int CurrentCompanyID, decimal FileSizeInMB, string RevisedBy, DateTime DateRevised)
        {
            ICompany oCompanyClient = RemotingHelper.GetCompanyObject();
            oCompanyClient.UpdateCompanyDataStorageCapacityAndCurrentUsage(CurrentCompanyID, FileSizeInMB, RevisedBy, DateRevised, Helper.GetAppUserInfo());
        }
        public static List<ImportTemplateHdrInfo> GetAllDataImportTemplate(short? DataImportType)
        {
            List<ImportTemplateHdrInfo> oAllImportTemplateHdrList = new List<ImportTemplateHdrInfo>();
            short? keyCount = null;
            IDataImport oDataImport = RemotingHelper.GetDataImportObject();
            keyCount = oDataImport.isKeyMappingDoneByCompanyID(SessionHelper.CurrentCompanyID.Value, Helper.GetAppUserInfo());
            if (keyCount.HasValue && DataImportType.HasValue && DataImportType.Value != Convert.ToInt32(WebConstants.SELECT_ONE))
            {
                oAllImportTemplateHdrList = oDataImport.GetAllTemplateImport(SessionHelper.CurrentCompanyID.Value, SessionHelper.CurrentUserID.Value, SessionHelper.CurrentRoleID.Value, Helper.GetAppUserInfo());
            }
            var oImportTemplateHdrList = oAllImportTemplateHdrList.FindAll(obj => obj.DataImportTypeID.Value == DataImportType.Value && obj.NumberOfMappedColumns.GetValueOrDefault() > 0);
            return oImportTemplateHdrList;

        }
        public static List<string> GetImportTemplateMandatoryFields(int? companyID, int? ImportTemplateID, List<string> MandatoryFieldList)
        {
            IUtility oClient = RemotingHelper.GetUtilityObject();
            List<string> fieldList = new List<string>();
            fieldList.AddRange(oClient.GetImportTemplateMandatoryFields(companyID, ImportTemplateID, MandatoryFieldList, Helper.GetAppUserInfo()));
            return fieldList;
        }


        /// <summary>
        /// Gets folder name for import templates type as per company id
        /// </summary>
        /// <param name="companyID">id of the company</param>
        /// <param name="importType">import type</param>
        /// <returns>folder name</returns>
        public static string GetFolderForImportTemplates(int companyID, short importType)
        {
            // There will be a Base folder(path from web config). 
            // Within Base Folder, there will be folders 
            // for each company as per CompanyId at the time of company creation.
            // If CompanyID folder exists, Create another folder by the name of Import Type.
            // Else create a folder by the name of companyID and within it, ImportType
            // Base folder + companyId + Import type

            // CURRENT_COMPANY_ID
            string baseFolderPath = SharedDataImportHelper.GetBaseFolderForCompany(companyID);

            string ImportFolderName = AppSettingHelper.GetAppSettingValue(AppSettingConstants.FOLDER_FOR_IMPORT_TEMPLATES);
            if (ImportFolderName == null)
            {
                throw new ARTException(5000398);
            }
            else
            {
                baseFolderPath = baseFolderPath + @"\" + ImportFolderName;
                string importFolder = @"";

                //Check if import folder within company folder exists or not.
                //if not, Create import folder within company folder
                importFolder = baseFolderPath + @"\" + SharedDataImportHelper.GetImportTypeName(importType,SessionHelper.CurrentReconciliationPeriodEndDate, SessionHelper.CurrentReconciliationPeriodID);

                if (!Directory.Exists(importFolder))
                    Directory.CreateDirectory(importFolder);

                return importFolder + @"\";
            }
        }
        public static string GetSheetName(ARTEnums.DataImportType dataImportType, int? ImportTemplateID)
        {
            string sheetName = "";
            switch (dataImportType)
            {
                case ARTEnums.DataImportType.GLData:
                    if (ImportTemplateID.HasValue && ImportTemplateID != Convert.ToInt32(WebConstants.ART_TEMPLATE))
                    {
                        IDataImport oDataImport = RemotingHelper.GetDataImportObject();
                        sheetName = oDataImport.GetImportTemplateSheetName(ImportTemplateID.Value, Helper.GetAppUserInfo());
                    }
                    else
                        sheetName = WebConstants.GLDATA_SHEETNAME;
                    break;
                case ARTEnums.DataImportType.SubledgerData:
                    if (ImportTemplateID.HasValue && ImportTemplateID != Convert.ToInt32(WebConstants.ART_TEMPLATE))
                    {
                        IDataImport oDataImport = RemotingHelper.GetDataImportObject();
                        sheetName = oDataImport.GetImportTemplateSheetName(ImportTemplateID.Value, Helper.GetAppUserInfo());
                    }
                    else
                        sheetName = WebConstants.SUBLEDGER_SHEETNAME;
                    break;
                case ARTEnums.DataImportType.AccountAttributeList:
                    sheetName = WebConstants.ACCOUNTATTRIBUTE_SHEETNAME;
                    break;
                case ARTEnums.DataImportType.GLTBS:
                    sheetName = WebConstants.MATCHING_SHEETNAME;
                    break;
                case ARTEnums.DataImportType.NBF:
                    sheetName = WebConstants.MATCHING_SHEETNAME;
                    break;
                case ARTEnums.DataImportType.AccountUpload:
                    sheetName = WebConstants.MAPPINGUPLOAD_SHEETNAME;
                    break;
                case ARTEnums.DataImportType.MultilingualUpload:
                    sheetName = MultilingualUploadConstants.SheetName;
                    break;
                case ARTEnums.DataImportType.ScheduleRecItems:
                    sheetName = ScheduleRecItemUploadConstants.SheetName;
                    break;
                case ARTEnums.DataImportType.GeneralTaskImport:
                    sheetName = TaskUploadConstants.SheetName;
                    break;
                case ARTEnums.DataImportType.CurrencyExchangeRateData:
                    sheetName = CurrencyExchangeUploadConstants.SheetName;
                    break;
            }
            return sheetName;

        }

     
    }
}
