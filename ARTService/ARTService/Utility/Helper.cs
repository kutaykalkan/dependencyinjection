using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using System.Configuration;
using System.IO;
using System.Data.OleDb;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data;
using SkyStem.ART.Service.Log;
using SkyStem.ART.Service.Data;
using SkyStem.ART.Service.DAO;
using SkyStem.ART.Service.APP.BLL;
using System.Xml.Serialization;
using SkyStem.ART.Service.Model;
using SkyStem.Language.LanguageUtility.Classes;
using SkyStem.ART.Shared.Utility;
using System.Security.Cryptography;
using System.Globalization;
using SkyStem.ART.Client.Model;
using SkyStem.ART.Client.Model.CompanyDatabase;
using SkyStem.ART.Client.IServices;
using SkyStem.ART.Client.Data;

using SkyStem.Language.LanguageUtility;
using SkyStem.ART.Client.Model.Base;
using ServiceModel = SkyStem.ART.Service.Model;
using SharedUtility = SkyStem.ART.Shared.Utility;

namespace SkyStem.ART.Service.Utility
{
    public class Helper
    {
        public const string COMPANY_NOT_SPECIFIED = "Company ID Not Specified";

        static Helper()
        {
            log4net.Config.XmlConfigurator.Configure();
        }


        /// <summary>
        /// Reads key from appsettings section of config file and returns its value.
        /// </summary>
        /// <param name="keyName">key name</param>
        /// <returns>value as string</returns>
        public static string GetAppSettingFromKey(string keyName)
        {
            string appsettingValue = "";
            appsettingValue = ConfigurationManager.AppSettings[keyName];
            return appsettingValue;
        }
        /// <summary>
        /// Return connection sting on the basis of file extension
        /// </summary>
        /// <param name="excelFileFullPath">full physical file path</param>
        /// <returns>connection string</returns>
        public static string GetConnectionStringForExcelFile(string excelFileFullPath, bool bReadHeaders)
        {
            string connStr = "";
            FileInfo excelFile = new FileInfo(excelFileFullPath);
            switch (excelFile.Extension)
            {
                case ".xls":
                    connStr = Helper.GetAppSettingFromKey("xls");
                    break;
                case ".xlsx":
                    connStr = Helper.GetAppSettingFromKey("xlsx");
                    break;
            }
            if (bReadHeaders)
            {
                connStr = string.Format(connStr, excelFile.FullName, "YES");
            }
            else
            {
                connStr = string.Format(connStr, excelFile.FullName, "NO");
            }
            return connStr;
        }

        public static OleDbConnection GetExcelFileConnection(string excelFileFullPath)
        {
            return GetExcelFileConnection(excelFileFullPath, false);
        }

        public static OleDbConnection GetExcelFileConnection(string excelFileFullPath, bool bReadHeaders)
        {
            string conStrexcel = GetConnectionStringForExcelFile(excelFileFullPath, bReadHeaders);
            OleDbConnection oConnExcelFile = new OleDbConnection(conStrexcel);
            return oConnExcelFile;
        }


        public static DataTable GetExcelFileSchema(OleDbConnection oConnExcelFile, string sheetName, CompanyUserInfo oCompanyUserInfo)
        {
            DataTable oExcelFileSchemaDataTable = null;
            DataTable oExcelSchema = null;
            try
            {
                if (oConnExcelFile.State != ConnectionState.Open)
                    oConnExcelFile.Open();
                oExcelSchema = oConnExcelFile.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                if (sheetName.Contains(" "))
                {
                    string[] restrictions = { null, null, "'" + sheetName + "$'", null };
                    oExcelFileSchemaDataTable = oConnExcelFile.GetSchema("Columns", restrictions);
                }
                else
                {
                    string[] restrictions = { null, null, sheetName + "$", null };
                    oExcelFileSchemaDataTable = oConnExcelFile.GetSchema("Columns", restrictions);
                }
                return oExcelFileSchemaDataTable;
            }
            catch (Exception ex)
            {
                LogError(ex, oCompanyUserInfo);
                throw new Exception("Error while establishing connection to Excel file.");
            }
        }

        public static DataTable GetExcelFileSchema(string excelFileFullPath, CompanyUserInfo oCompanyUserInfo)
        {
            DataTable oExcelFileSchemaDataTable = null;
            DataTable oExcelSchema = null;
            OleDbConnection oConnExcelFile = null;
            try
            {
                oConnExcelFile = GetExcelFileConnection(excelFileFullPath);
                oConnExcelFile.Open();
                oExcelSchema = oConnExcelFile.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                string sheetName = oExcelSchema.Rows[0]["TABLE_NAME"].ToString();
                string[] restrictions = { null, null, sheetName, null };
                oExcelFileSchemaDataTable = oConnExcelFile.GetSchema("Columns", restrictions);
                return oExcelFileSchemaDataTable;
            }
            catch (Exception ex)
            {
                LogError(ex, oCompanyUserInfo);
                throw new Exception("Error while establishing connection to file : " + excelFileFullPath);
            }
            finally
            {
                if (oConnExcelFile != null && oConnExcelFile.State != ConnectionState.Closed)
                    oConnExcelFile.Close();
            }
        }

        public static DataTable GetDataTableFromExcel(OleDbConnection oConnExcelFile, string fieldNames, string sheetName, CompanyUserInfo oCompanyUserInfo)
        {
            DataTable oExcelDataTable;
            OleDbDataReader oReaderExcel = null;
            string query = string.Empty;
            try
            {
                query = "SELECT " + fieldNames + " FROM [" + sheetName + "$]";
                OleDbCommand oCommandExcel = new OleDbCommand(query, oConnExcelFile);
                oReaderExcel = oCommandExcel.ExecuteReader();
                oExcelDataTable = new DataTable();
                oExcelDataTable.Load(oReaderExcel);
                oReaderExcel.Close();
                return oExcelDataTable;
            }
            catch (Exception ex)
            {
                LogError("Query = " + query, oCompanyUserInfo);
                LogError(ex, oCompanyUserInfo);
                throw new Exception("Error while reading data from excel file");
            }
            finally
            {
                if (oReaderExcel != null && !oReaderExcel.IsClosed)
                    oReaderExcel.Close();
            }
        }

        public static DataTable GetDataTableFromExcel(string fullExcelFilePath, string sheetName, CompanyUserInfo oCompanyUserInfo)
        {
            OleDbConnection oConnectionExcel = null;
            DataTable dtSchema = null;
            string allFieldName = "";

            try
            {
                oConnectionExcel = Helper.GetExcelFileConnection(fullExcelFilePath);
                oConnectionExcel.Open();

                // Get Excel File Schema - gets the list of Columns names as per Excel
                dtSchema = Helper.GetExcelFileSchema(oConnectionExcel, sheetName, oCompanyUserInfo);

                // Loop and create a comma separated list of columns available in Excel
                allFieldName = Helper.GetColumnNames(dtSchema);

                // read the data based on above Column List
                return Helper.GetDataTableFromExcel(oConnectionExcel, allFieldName, sheetName, oCompanyUserInfo);
            }
            finally
            {
                oConnectionExcel.Close();
                oConnectionExcel.Dispose();
            }
        }



        internal static string GetDateTime()
        {
            return DateTime.Now.ToString();
        }

        internal static int GetTimerInterval(string appSettingKey)
        {
            int interval = ServiceConstants.DEFAULT_INTERVAL;
            string timerInterval = Helper.GetAppSettingFromKey(appSettingKey);
            if (!string.IsNullOrEmpty(timerInterval))
            {
                interval = Convert.ToInt32(timerInterval);
            }
            return interval;
        }

        public static void UpdateDataImportHDR(SqlConnection oConnection, int dataImportID
                             , string errorMessageToSave, string dataImportStatus, int recordsImported
                             , short? warningReasonID, CompanyUserInfo oCompanyUserInfo)
        {
            SqlTransaction oTransaction = null;
            SqlCommand oCommand = null;

            try
            {
                if (oConnection.State != ConnectionState.Open)
                    oConnection.Open();
                oTransaction = oConnection.BeginTransaction();
                oCommand = GetDataImportHDRUpdateCommand(dataImportID, errorMessageToSave, dataImportStatus, recordsImported, warningReasonID, oCompanyUserInfo);
                oCommand.Connection = oConnection;
                oCommand.Transaction = oTransaction;

                oCommand.ExecuteNonQuery();
                oTransaction.Commit();
                oTransaction.Dispose();
                oTransaction = null;
                Helper.LogError("DataImportHdr Updated successfully for DataImportID: ." + dataImportID.ToString(), oCompanyUserInfo);
            }
            catch (Exception ex)
            {
                if (oTransaction != null)
                    oTransaction.Rollback();
                LogError("Error:: Error while updating DataImportHDR", oCompanyUserInfo);
                LogError(ex, oCompanyUserInfo);
            }
        }
        public static DataTable GetPhrases(DataTable dtLabelIDs, int businessEntityID
                              , int languageID, int defaultLanguageID, SqlConnection oConnection, CompanyUserInfo oCompanyUserInfo)
        {

            if (oConnection.State != ConnectionState.Open)
                oConnection.Open();
            SqlCommand oCommand = GetPhraseCommand(dtLabelIDs, businessEntityID, languageID, defaultLanguageID, oCompanyUserInfo);
            oCommand.Connection = oConnection;
            DataTable dt = new DataTable();
            dt.Load(oCommand.ExecuteReader());
            return dt;

        }

        private static SqlCommand GetPhraseCommand(DataTable dtLabelIDs, int businessEntityID
                                                            , int languageID, int defaultLanguageID, CompanyUserInfo oCompanyUserInfo)
        {
            SqlCommand oCommand = CreateCommand(oCompanyUserInfo);
            oCommand.CommandType = CommandType.StoredProcedure;
            oCommand.CommandText = "usp_GET_PhraseForLabelIDs";

            SqlParameterCollection cmdParamCollection = oCommand.Parameters;

            SqlParameter paramDataTableLabelID = new SqlParameter("@udtLabelID", dtLabelIDs);
            SqlParameter paramBusinessentityID = new SqlParameter("@businessEntityID", businessEntityID);
            SqlParameter paramLanguageID = new SqlParameter("@languageID", languageID);
            SqlParameter paramDefaultLanguageID = new SqlParameter("@defalutLanguageID", defaultLanguageID);

            cmdParamCollection.Add(paramDataTableLabelID);
            cmdParamCollection.Add(paramBusinessentityID);
            cmdParamCollection.Add(paramLanguageID);
            cmdParamCollection.Add(paramDefaultLanguageID);

            return oCommand;
        }
        public static string GetSinglePhrase(int LabelID, int businessEntityID
                              , int languageID, int defaultLanguageID, CompanyUserInfo oCompanyUserInfo)
        {
            string phrase = "";
            SqlConnection oConnection = null;
            SqlCommand oCommand = null;
            try
            {
                oConnection = CreateConnection(oCompanyUserInfo);
                oCommand = GetSinglePhraseCommand(LabelID, businessEntityID, languageID, defaultLanguageID, oCompanyUserInfo);
                oCommand.Connection = oConnection;
                oCommand.Connection.Open();
                oCommand.ExecuteNonQuery();
                phrase = oCommand.Parameters["@Phrase"].Value.ToString();
            }
            finally
            {
                if (oConnection != null && oConnection.State == ConnectionState.Open)
                    oConnection.Close();
            }
            return phrase;


        }
        private static SqlCommand GetSinglePhraseCommand(int LabelID, int businessEntityID
                                                            , int languageID, int defaultLanguageID, CompanyUserInfo oCompanyUserInfo)
        {
            SqlCommand oCommand = CreateCommand(oCompanyUserInfo);
            oCommand.CommandType = CommandType.StoredProcedure;
            oCommand.CommandText = "usp_GET_PhraseForLabelID";

            SqlParameterCollection cmdParamCollection = oCommand.Parameters;

            SqlParameter paramLabelID = new SqlParameter("@LableID", LabelID);
            SqlParameter paramBusinessentityID = new SqlParameter("@businessEntityID", businessEntityID);
            SqlParameter paramLanguageID = new SqlParameter("@languageID", languageID);
            SqlParameter paramDefaultLanguageID = new SqlParameter("@defalutLanguageID", defaultLanguageID);
            SqlParameter paramPhrase = new SqlParameter("@Phrase", SqlDbType.NVarChar, 2000);
            paramPhrase.Direction = ParameterDirection.Output;

            cmdParamCollection.Add(paramLabelID);
            cmdParamCollection.Add(paramBusinessentityID);
            cmdParamCollection.Add(paramLanguageID);
            cmdParamCollection.Add(paramDefaultLanguageID);
            cmdParamCollection.Add(paramPhrase);

            return oCommand;
        }



        private static SqlCommand GetDataImportHDRUpdateCommand(int dataImportID, string errorMessageToSave
                                    , string dataImportStatus, int recordsImported, short? warningReasonID, CompanyUserInfo oCompanyUserInfo)
        {
            SqlCommand oCommand = CreateCommand(oCompanyUserInfo);
            oCommand.CommandType = CommandType.StoredProcedure;
            oCommand.CommandText = "usp_UPD_DataImportHdr";

            SqlParameterCollection cmdParamCollection = oCommand.Parameters;
            SqlParameter paramDataImportID = new SqlParameter("@dataImportID", dataImportID);
            SqlParameter paramErrorMessageToSave = new SqlParameter("@errorMessageToSave", SqlDbType.NVarChar, -1);
            SqlParameter paramImportStatus = new SqlParameter("@dataImportStatusID", SqlDbType.SmallInt);
            SqlParameter paramRecordsImported = new SqlParameter("@recordsImported", SqlDbType.Int);
            SqlParameter paramWarningReasonID = new SqlParameter("@warningReasonID", SqlDbType.SmallInt);
            SqlParameter paramDataDeletionRequired = new SqlParameter("@IsDataDeletionRequired", SqlDbType.Bit);
            SqlParameter paramProfilingDataToSave = new SqlParameter("@ProfilingData", SqlDbType.Xml);

            paramErrorMessageToSave.Value = errorMessageToSave;
            if (dataImportStatus == "FAIL")
                paramImportStatus.Value = (short)Enums.DataImportStatus.Failure;
            if (dataImportStatus == "WARNING")
                paramImportStatus.Value = (short)Enums.DataImportStatus.Warning;
            if (dataImportStatus == "SUCCESS")
                paramImportStatus.Value = (short)Enums.DataImportStatus.Success;
            paramRecordsImported.Value = recordsImported;
            if (dataImportStatus != "WARNING")
                paramDataDeletionRequired.Value = true;
            else
                paramDataDeletionRequired.Value = false;

            if (warningReasonID.HasValue)
                paramWarningReasonID.Value = warningReasonID.Value;
            else
                paramWarningReasonID.Value = DBNull.Value;
            //if (string.IsNullOrEmpty(oDataImportInfo.ProfilingData))
            paramProfilingDataToSave.Value = DBNull.Value;
            //else
            //    paramProfilingDataToSave.Value = oDataImportInfo.ProfilingData;

            cmdParamCollection.Add(paramDataImportID);
            cmdParamCollection.Add(paramErrorMessageToSave);
            cmdParamCollection.Add(paramImportStatus);
            cmdParamCollection.Add(paramRecordsImported);
            cmdParamCollection.Add(paramWarningReasonID);
            cmdParamCollection.Add(paramDataDeletionRequired);
            cmdParamCollection.Add(paramProfilingDataToSave);
            return oCommand;
        }

        public static string GetDisplayDate(DateTime? oDate)
        {
            if (oDate == null || oDate == default(DateTime))
            {
                return ServiceConstants.HYPHEN;
            }
            else
            {
                // Todo: This would change to have Multi-lingual Phrase
                return oDate.Value.ToShortDateString();
            }
        }

        public static string FormatFailureMessage(string msg)
        {
            msg = msg.Replace(" , ", "<br>");
            msg = msg.Replace(" ,", "<br>");
            msg = msg.Replace(",", "<br>");
            return msg;
        }

        public static string GetSinglePhraseValue(int LabelID, int businessEntityID, int languageID, int defaultLanguageID, CompanyUserInfo oCompanyUserInfo)
        {
            return GetSinglePhrase(LabelID, businessEntityID, languageID, defaultLanguageID, oCompanyUserInfo);
        }
        public static string GetDataLengthErrorMessage(int businessEntityID, int languageID, int defaultLanguageID, CompanyUserInfo oCompanyUserInfo)
        {
            //TODO: Use labelID


            return GetSinglePhrase(1827, businessEntityID, languageID, defaultLanguageID, oCompanyUserInfo);
            //return "Field: {0}; Row: {1} Data cannot be more than {2} characters";
        }

        public static string GetMandatoryFieldErrorMessage(int businessEntityID, int languageID, int defaultLanguageID, CompanyUserInfo oCompanyUserInfo)
        {
            return GetSinglePhrase(5000372, businessEntityID, languageID, defaultLanguageID, oCompanyUserInfo);
            // No Data For Mandatory Field: {0}; Row: {1}  
        }

        public static string GetLessThanErrorMessage(int businessEntityID, int languageID, int defaultLanguageID, CompanyUserInfo oCompanyUserInfo)
        {
            return GetSinglePhrase(5000007, businessEntityID, languageID, defaultLanguageID, oCompanyUserInfo);
            // {0} has to be less than or equal to {1}   
        }

        public static string GetInvalidDataErrorMessage(int businessEntityID, int languageID, int defaultLanguageID, CompanyUserInfo oCompanyUserInfo)
        {
            return GetSinglePhrase(5000255, businessEntityID, languageID, defaultLanguageID, oCompanyUserInfo);//Field: {0}; Row: {1} Invalid Data
        }
        public static string GetDataInDisabledCapabilityColumnErrorMessage(int businessEntityID, int languageID, int defaultLanguageID, CompanyUserInfo oCompanyUserInfo)
        {
            //TODO: Use labelID
            //return "Field: {0}; Row: {1} There should not be any data for this column as corrosponding capability is not enabled";
            return GetSinglePhrase(1955, businessEntityID, languageID, defaultLanguageID, oCompanyUserInfo);
        }

        public static string GetDataWarningMessage(int businessEntityID, int languageID, int defaultLanguageID, CompanyUserInfo oCompanyUserInfo)
        {
            return GetSinglePhrase(5000246, businessEntityID, languageID, defaultLanguageID, oCompanyUserInfo);//Warning in Row# {0}. {1} 
        }

        public static List<string> GetGLDataImportMandatoryFields()
        {
            List<string> mandatoryFieldList = new List<string>();
            mandatoryFieldList.Add(GLDataImportFields.PERIODENDDATE);
            mandatoryFieldList.Add(GLDataImportFields.COMPANY);
            mandatoryFieldList.Add(GLDataImportFields.GLACCOUNTNUMBER);
            mandatoryFieldList.Add(GLDataImportFields.GLACCOUNTNAME);
            mandatoryFieldList.Add(GLDataImportFields.FSCAPTION);
            mandatoryFieldList.Add(GLDataImportFields.ACCOUNTTYPE);
            mandatoryFieldList.Add(GLDataImportFields.BCCYCODE);
            mandatoryFieldList.Add(GLDataImportFields.BALANCEBCCY);
            mandatoryFieldList.Add(GLDataImportFields.BALANCERCCY);
            mandatoryFieldList.Add(GLDataImportFields.RCCYCODE);

            return mandatoryFieldList;
        }

        public static string[] GetAccountDataImportMandatoryFields()
        {

            string[] mandatoryFields = new string[GLDataImportFields.ACCOUNTDATAIMPORTMANDATORYFIELDCOUNT];
            mandatoryFields[0] = GLDataImportFields.COMPANY;
            mandatoryFields[1] = GLDataImportFields.GLACCOUNTNUMBER;
            mandatoryFields[2] = GLDataImportFields.GLACCOUNTNAME;
            mandatoryFields[3] = GLDataImportFields.FSCAPTION;
            mandatoryFields[4] = GLDataImportFields.ACCOUNTTYPE;
            mandatoryFields[5] = GLDataImportFields.ISPROFITANDLOSS;
            return mandatoryFields;
        }

        public static string[] GetSubLedgerImportMandatoryFields()
        {
            string[] mandatoryFields = new string[SubledgerDataImportFields.SUBLEDGERDATAIMPORTMANDATORYFIELDCOUNT];
            mandatoryFields[0] = SubledgerDataImportFields.PERIODENDDATE;
            mandatoryFields[1] = SubledgerDataImportFields.GLACCOUNTNUMBER;
            mandatoryFields[2] = SubledgerDataImportFields.GLACCOUNTNAME;
            mandatoryFields[3] = SubledgerDataImportFields.FSCAPTION;
            mandatoryFields[4] = SubledgerDataImportFields.ACCOUNTTYPE;
            mandatoryFields[5] = SubledgerDataImportFields.BCCYCODE;
            mandatoryFields[6] = SubledgerDataImportFields.BALANCEBCCY;
            mandatoryFields[7] = SubledgerDataImportFields.BALANCERCCY;
            mandatoryFields[8] = SubledgerDataImportFields.RCCYCODE;

            return mandatoryFields;
        }

        public static List<string> GetAccountAttributeImportMandatoryFields()
        {
            List<string> mandatoryFields = new List<string>();

            mandatoryFields.Add(AccountAttributeDataImportFields.COMPANY);
            mandatoryFields.Add(AccountAttributeDataImportFields.GLACCOUNTNUMBER);
            mandatoryFields.Add(AccountAttributeDataImportFields.GLACCOUNTNAME);
            mandatoryFields.Add(AccountAttributeDataImportFields.FSCAPTION);
            mandatoryFields.Add(AccountAttributeDataImportFields.ACCOUNTTYPE);

            return mandatoryFields;
        }
        public static List<string> GetAccountAttributeImportAttributeFields()
        {
            List<string> attributeFields = new List<string>();

            attributeFields.Add(AccountAttributeDataImportFields.RISKRATING);
            attributeFields.Add(AccountAttributeDataImportFields.RECONCILIATIONTEMPLATE);
            attributeFields.Add(AccountAttributeDataImportFields.ISKEYACCOUNT);
            attributeFields.Add(AccountAttributeDataImportFields.ISZEROBALANCEACCOUNT);
            attributeFields.Add(AccountAttributeDataImportFields.SUBLEDGERSOURCE);
            attributeFields.Add(AccountAttributeDataImportFields.RECONCILIATIONPOLICY);
            attributeFields.Add(AccountAttributeDataImportFields.NATUREOFACCOUNT);
            attributeFields.Add(AccountAttributeDataImportFields.RECONCILIATIONPROCEDURE);
            attributeFields.Add(AccountAttributeDataImportFields.PREPARER);
            attributeFields.Add(AccountAttributeDataImportFields.REVIEWER);
            attributeFields.Add(AccountAttributeDataImportFields.APPROVER);
            attributeFields.Add(AccountAttributeDataImportFields.RECONCILABLE);
            return attributeFields;
        }
        //public static DataTable ConvertAlertCollectionToDataTable(List<Alert> oAlertCollection)
        //{
        //    DataTable dt = null;
        //    if (oAlertCollection != null && oAlertCollection.Count > 0)
        //    {
        //        dt = new DataTable("udt_BigIntIDTableType");
        //        DataColumn dc = dt.Columns.Add("ID");
        //        DataRow dr;
        //        for (int i = 0; i < oAlertCollection.Count; i++)
        //        {
        //            dr = dt.NewRow();
        //            dr[0] = oAlertCollection[i].CompanyAlertDetailUserID;
        //            dt.Rows.Add(dr);
        //        }
        //    }
        //    return dt;
        //}

        public static bool IsFieldPresent(DataTable dtSchema, string fieldName)
        {
            foreach (DataRow dr in dtSchema.Rows)
            {
                if (dr["Column_Name"] != null)
                {
                    if (dr["Column_Name"].ToString().Trim() == fieldName)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        internal static string GetColumnNames(DataTable dtGLDataSchema)
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

        public static string GetErrorMessage(int businessEntityID, int languageID, int defaultLanguageID, CompanyUserInfo oCompanyUserInfo)
        {
            return GetSinglePhrase(5000190, businessEntityID, languageID, defaultLanguageID, oCompanyUserInfo);
            //Error in Row# {0}, Column '{1}'. {2} 
        }
        #region Matching

        public static void UpdateMatchingSourceDataImportHDR(SqlConnection oConnection, long matchingSourceDataImportID
                            , string errorMessageToSave, string dataImportStatus, int recordsImported, CompanyUserInfo oCompanyUserInfo)
        {
            SqlTransaction oTransaction = null;
            SqlCommand oCommand = null;

            try
            {
                if (oConnection.State != ConnectionState.Open)
                    oConnection.Open();
                oTransaction = oConnection.BeginTransaction();
                oCommand = GetMatchingSourceDataImportHDRUpdateCommand(matchingSourceDataImportID, errorMessageToSave, dataImportStatus, recordsImported, oCompanyUserInfo);
                oCommand.Connection = oConnection;
                oCommand.Transaction = oTransaction;

                oCommand.ExecuteNonQuery();
                oTransaction.Commit();
                oTransaction.Dispose();
                oTransaction = null;
                Helper.LogError(" MatchingSourceDataImportHdr Updated successfully for MatchingSourceDataImportID: ." + matchingSourceDataImportID.ToString(), oCompanyUserInfo);
            }
            catch (Exception ex)
            {
                if (oTransaction != null)
                    oTransaction.Rollback();
                LogError("Error:: Error while updating MatchingSourceDataImportHDR", oCompanyUserInfo);
                LogError(ex, oCompanyUserInfo);
            }
        }

        private static SqlCommand GetMatchingSourceDataImportHDRUpdateCommand(long matchingSourceDataImportID,
                                string errorMessageToSave,
                                string dataImportStatus, int recordsImported, CompanyUserInfo oCompanyUserInfo)
        {
            SqlCommand oCommand = CreateCommand(oCompanyUserInfo);
            oCommand.CommandType = CommandType.StoredProcedure;
            oCommand.CommandText = "Matching.usp_UPD_MatchingSourceDataImportHdr";

            SqlParameterCollection cmdParamCollection = oCommand.Parameters;
            SqlParameter paramDataImportID = new SqlParameter("@matchingSourceDataImportID", matchingSourceDataImportID);
            SqlParameter paramErrorMessageToSave = new SqlParameter("@errorMessageToSave", SqlDbType.NVarChar, -1);
            SqlParameter paramImportStatus = new SqlParameter("@dataImportStatusID", SqlDbType.SmallInt);
            SqlParameter paramRecordsImported = new SqlParameter("@recordsImported", SqlDbType.Int);

            paramErrorMessageToSave.Value = errorMessageToSave;
            if (dataImportStatus == "FAIL")
                paramImportStatus.Value = (short)Enums.DataImportStatus.Failure;
            if (dataImportStatus == "WARNING")
                paramImportStatus.Value = (short)Enums.DataImportStatus.Warning;
            if (dataImportStatus == "SUCCESS")
                paramImportStatus.Value = (short)Enums.DataImportStatus.Success;
            paramRecordsImported.Value = recordsImported;

            cmdParamCollection.Add(paramDataImportID);
            cmdParamCollection.Add(paramErrorMessageToSave);
            cmdParamCollection.Add(paramImportStatus);
            cmdParamCollection.Add(paramRecordsImported);
            return oCommand;
        }




        #endregion



        public static bool IsFieldPresent(DataColumnCollection columnsFromExcelData, string fieldName)
        {
            foreach (DataColumn dc in columnsFromExcelData)
            {
                if (!string.IsNullOrEmpty(dc.ColumnName) && !string.IsNullOrEmpty(fieldName) && TrimAndMakeLower(dc.ColumnName) == TrimAndMakeLower(fieldName))
                {
                    return true;
                }
            }
            return false;
        }
        public static DataColumn GetDataTableColumn(DataColumnCollection columnsFromExcelData, string fieldName)
        {
            foreach (DataColumn dc in columnsFromExcelData)
            {
                if (!string.IsNullOrEmpty(dc.ColumnName) && !string.IsNullOrEmpty(fieldName) && TrimAndMakeLower(dc.ColumnName) == TrimAndMakeLower(fieldName))
                {
                    return dc;
                }
            }
            return null;
        }
        public static string TrimAndMakeLower(string fieldName)
        {
            if (!string.IsNullOrEmpty(fieldName))
                return fieldName.Trim().ToLower();
            else
                return fieldName;
        }

        public static void AddSqlBulkCopyMapping(DataTable dtExcelData, SqlBulkCopy oSqlBlkCopy, string DataImportFieldName, string TransitFieldName)
        {
            DataColumn dc = GetDataTableColumn(dtExcelData.Columns, DataImportFieldName);
            if (dc != null)
                oSqlBlkCopy.ColumnMappings.Add(dc.ColumnName, TransitFieldName);
        }

        public static MultilingualAttributeInfo GetMultilingualAttributeInfo(int languageID, int companyID)
        {
            MultilingualAttributeInfo oMultilingualAttributeInfo = new MultilingualAttributeInfo();
            oMultilingualAttributeInfo.ApplicationID = SharedAppSettingHelper.GetApplicationID();
            oMultilingualAttributeInfo.LanguageID = languageID;
            oMultilingualAttributeInfo.BusinessEntityID = companyID;
            oMultilingualAttributeInfo.DefaultBusinessEntityID = SharedAppSettingHelper.GetDefaultBusinessEntityID();
            oMultilingualAttributeInfo.DefaultLanguageID = SharedAppSettingHelper.GetDefaultLanguageID();
            return oMultilingualAttributeInfo;
        }

        #region "Password Methods"
        public static string CreateRandomPassword(int passwordLength, string loginID, Random rand)
        {
            string passwordString = string.Empty;
            try
            {
                passwordString = SharedHelper.CreateRandomPassword(passwordLength, loginID, rand);
            }
            catch (Exception ex)
            {
                //Do something
                LogError(ex, null);
            }
            return passwordString;
        }

        public static string GetHashedPassword(string passwordTyped)
        {
            string stringResult = "";
            try
            {
                SHA1 sha = new SHA1CryptoServiceProvider();// hashed length = 28
                //MD5 sha = new MD5CryptoServiceProvider();// hashed length = 24
                //SHA512Managed sha = new SHA512Managed();
                byte[] result = sha.ComputeHash(System.Text.Encoding.UTF8.GetBytes(passwordTyped));
                stringResult = Convert.ToBase64String(result);
            }
            catch (CryptographicException ce)
            {
                throw ce;
                //Do work
            }
            return stringResult;
        }
        #endregion

        #region "Logging"

        public static void LogError(string message, CompanyUserInfo oCompanyUserInfo)
        {
#if EVENT_LOG
                        EventLogger.CreateEventLog();
                        EventLogger.WriteLogEntry(message);
#elif FILE_LOG
                        ServiceLog.AddToFile(message);
#elif LOG4NET
            // do some code here
            //log4net.ILog oLogger = log4net.LogManager.GetLogger(ServiceConstants.LOGGER_NAME);
            //oLogger.Error(message);
            LogErrorViaService(message, oCompanyUserInfo);
#endif
        }

        public static void LogError(Exception oException, CompanyUserInfo oCompanyUserInfo)
        {
#if EVENT_LOG
                        // Add Code here
#elif FILE_LOG
                        ServiceLog.AddToFile(oException);
#elif LOG4NET
            // do some code here
            //log4net.ILog oLogger = log4net.LogManager.GetLogger(ServiceConstants.LOGGER_NAME);
            //oLogger.Error("", oException);
            LogErrorViaService(oException, oCompanyUserInfo);
#endif
        }

        public static void LogError(ServiceModel.DataImportHdrInfo oDataImportHdrInfo, string msg, Exception oException, CompanyUserInfo oCompanyUserInfo)
        {
#if EVENT_LOG
                        // Add Code here
#elif FILE_LOG
                        ServiceLog.AddToFile(oException);
#elif LOG4NET
            // do some code here
            //log4net.ILog oLogger = log4net.LogManager.GetLogger(ServiceConstants.LOGGER_NAME);
            //oLogger.Error("", oException);
            LogErrorViaService(oDataImportHdrInfo, msg, oException, oCompanyUserInfo);
#endif
        }

        public static void LogInfo(string message, CompanyUserInfo oCompanyUserInfo)
        {
#if EVENT_LOG
                        EventLogger.CreateEventLog();
                        EventLogger.WriteLogEntry(message);
#elif FILE_LOG
                        ServiceLog.AddToFile(message);
#elif LOG4NET
            // do some code here
            //log4net.ILog oLogger = log4net.LogManager.GetLogger(ServiceConstants.LOGGER_NAME);
            //oLogger.Info(message);
            LogInfoViaService(message, oCompanyUserInfo);
#endif
        }

        public static void LogInfoViaService(string message, CompanyUserInfo oCompanyUserInfo)
        {
            try
            {
                IUtility oUtilityClient = RemotingHelper.GetUtilityObject();
                LogInfo oLog = CreateLogInfo(message, ARTConstants.LOG_INFO);
                AppUserInfo oAppUser = Helper.GetAppUserFromCompanyUserInfo(oCompanyUserInfo);
                oUtilityClient.LogInfo(oLog, oAppUser);
            }
            catch { }
        }

        public static void LogInfoViaService(Exception ex, CompanyUserInfo oCompanyUserInfo)
        {
            try
            {
                IUtility oUtilityClient = RemotingHelper.GetUtilityObject();
                LogInfo oLog = CreateLogInfo(ex, ARTConstants.LOG_INFO);
                AppUserInfo oAppUser = Helper.GetAppUserFromCompanyUserInfo(oCompanyUserInfo);
                oUtilityClient.LogInfo(oLog, oAppUser);
            }
            catch { }
        }

        public static List<LogInfo> LogInfoToCache(string message, List<LogInfo> oLogInfoCache)
        {
            if (oLogInfoCache == null)
                oLogInfoCache = new List<LogInfo>();
            LogInfo oLog = CreateLogInfo(message, ARTConstants.LOG_INFO);
            oLogInfoCache.Add(oLog);
            return oLogInfoCache;
        }

        public static List<LogInfo> LogInfoToCache(Exception ex, List<LogInfo> oLogInfoCache)
        {
            if (oLogInfoCache == null)
                oLogInfoCache = new List<LogInfo>();
            LogInfo oLog = CreateLogInfo(ex, ARTConstants.LOG_INFO);
            oLogInfoCache.Add(oLog);
            return oLogInfoCache;
        }

        public static void LogErrorViaService(string message, CompanyUserInfo oCompanyUserInfo)
        {
            try
            {
                IUtility oUtilityClient = RemotingHelper.GetUtilityObject();
                LogInfo oLog = CreateLogInfo(message, ARTConstants.LOG_ERROR);
                AppUserInfo oAppUser = Helper.GetAppUserFromCompanyUserInfo(oCompanyUserInfo);
                oUtilityClient.LogError(oLog, oAppUser);
            }
            catch { }
        }

        public static void LogErrorViaService(Exception ex, CompanyUserInfo oCompanyUserInfo)
        {
            try
            {
                IUtility oUtilityClient = RemotingHelper.GetUtilityObject();
                LogInfo oLog = CreateLogInfo(ex, ARTConstants.LOG_ERROR);
                AppUserInfo oAppUser = Helper.GetAppUserFromCompanyUserInfo(oCompanyUserInfo);
                oUtilityClient.LogError(oLog, oAppUser);
            }
            catch { }
        }

        public static void LogErrorViaService(ServiceModel.DataImportHdrInfo oDataImportHdrInfo, string msg, Exception ex, CompanyUserInfo oCompanyUserInfo)
        {
            try
            {
                IUtility oUtilityClient = RemotingHelper.GetUtilityObject();
                LogInfo oLog = CreateLogInfo(ex, ARTConstants.LOG_ERROR);
                oLog.Message = msg + oLog.Message;
                AppUserInfo oAppUser = Helper.GetAppUserFromCompanyUserInfo(oCompanyUserInfo);
                if (oDataImportHdrInfo != null)
                {
                    oLog.DataImportID = oDataImportHdrInfo.DataImportID;
                    oAppUser.RecPeriodID = oDataImportHdrInfo.RecPeriodID;
                    oAppUser.LoginID = oDataImportHdrInfo.AddedBy;
                }
                oUtilityClient.LogError(oLog, oAppUser);
            }
            catch { }
        }

        public static void LogListViaService(List<LogInfo> oLogInfoCache, int? dataImportID, CompanyUserInfo oCompanyUserInfo)
        {
            try
            {
                if (oLogInfoCache != null && oLogInfoCache.Count > 0)
                {
                    IUtility oUtilityClient = RemotingHelper.GetUtilityObject();
                    AppUserInfo oAppUser = Helper.GetAppUserFromCompanyUserInfo(oCompanyUserInfo);
                    bool bError = false;
                    // Check whether there is a error or not
                    foreach (LogInfo oLog in oLogInfoCache)
                    {
                        if (oLog.LogLevel == ARTConstants.LOG_ERROR)
                        {
                            bError = true;
                            break;
                        }
                    }
                    foreach (LogInfo oLog in oLogInfoCache)
                    {
                        // If there is a error escalate the level of all the info
                        oLog.DataImportID = dataImportID.GetValueOrDefault();
                        if (bError)
                            oLog.LogLevel = ARTConstants.LOG_ERROR;
                        switch (oLog.LogLevel)
                        {
                            case ARTConstants.LOG_ERROR:
                                oUtilityClient.LogError(oLog, oAppUser);
                                break;
                            default:
                                oUtilityClient.LogInfo(oLog, oAppUser);
                                break;
                        }
                    }
                }
            }
            catch { }
        }

        public static List<LogInfo> LogErrorToCache(string message, List<LogInfo> oLogInfoCache)
        {
            if (oLogInfoCache == null)
                oLogInfoCache = new List<LogInfo>();
            LogInfo oLog = CreateLogInfo(message, ARTConstants.LOG_ERROR);
            oLogInfoCache.Add(oLog);
            return oLogInfoCache;
        }

        public static List<LogInfo> LogErrorToCache(Exception ex, List<LogInfo> oLogInfoCache)
        {
            if (oLogInfoCache == null)
                oLogInfoCache = new List<LogInfo>();
            LogInfo oLog = CreateLogInfo(ex, ARTConstants.LOG_ERROR);
            oLogInfoCache.Add(oLog);
            return oLogInfoCache;
        }

        private static LogInfo CreateLogInfo(string message, string logLevel)
        {
            LogInfo oLog = new LogInfo();
            oLog.Message = message;
            oLog.LogDate = DateTime.Now;
            oLog.LogLevel = logLevel;
            oLog.Source = ARTConstants.WINDOWS_SERVICE_SOURCE_NAME;
            return oLog;
        }

        private static LogInfo CreateLogInfo(Exception ex, string logLevel)
        {
            LogInfo oLog = new LogInfo();
            oLog.Message = ex.Message;
            oLog.StackTrace = ex.StackTrace;
            oLog.LogDate = DateTime.Now;
            oLog.LogLevel = logLevel;
            oLog.Source = ARTConstants.WINDOWS_SERVICE_SOURCE_NAME;
            return oLog;
        }

        public static void LogServiceTimeStampInfo(string msg)
        {
#if EVENT_LOG
                        EventLogger.CreateEventLog();
                        EventLogger.WriteLogEntry(msg);
#elif FILE_LOG
                        ServiceLog.AddToFile(msg);
#elif LOG4NET
            // do some code here
            log4net.ILog oLogger = log4net.LogManager.GetLogger(ServiceConstants.LOGGER_NAME_SERVICE_TIME_STAMP);
            oLogger.Info(msg);
#endif
        }

        public static void LogServiceTimeStampError(string msg)
        {
#if EVENT_LOG
                        EventLogger.CreateEventLog();
                        EventLogger.WriteLogEntry(msg);
#elif FILE_LOG
                        ServiceLog.AddToFile(msg);
#elif LOG4NET
            // do some code here
            log4net.ILog oLogger = log4net.LogManager.GetLogger(ServiceConstants.LOGGER_NAME_SERVICE_TIME_STAMP);
            oLogger.Error(msg);
#endif
        }

        #endregion

        public static bool IsValidDecimal(string val, int lcid, out decimal num)
        {
            CultureInfo oCultureInfo = new CultureInfo(lcid);
            return decimal.TryParse(val, System.Globalization.NumberStyles.Number, oCultureInfo.NumberFormat, out num);
        }

        public static bool IsValidDateTime(string val, int lcid, out DateTime dt)
        {
            CultureInfo oCultureInfo = new CultureInfo(lcid);
            return DateTime.TryParse(val, oCultureInfo.DateTimeFormat, System.Globalization.DateTimeStyles.AssumeLocal, out dt);
        }

        public static bool IsValidInt32(string val, int lcid, out Int32 num)
        {
            CultureInfo oCultureInfo = new CultureInfo(lcid);
            return Int32.TryParse(val, System.Globalization.NumberStyles.Integer, oCultureInfo.NumberFormat, out num);
        }

        #region DB Helper functions
        public static SqlCommand CreateCommand(CompanyUserInfo oCompanyUserInfo)
        {
            DataAccess da = new DataAccess(oCompanyUserInfo);
            return da.CreateCommand();
        }
        //public static SqlConnection CreateConnection()
        //{
        //    DataAccess da = new DataAccess();
        //    return da.GetConnection();
        //}
        public static SqlConnection CreateConnection(CompanyUserInfo oCompanyUserInfo)
        {
            DataAccess da = new DataAccess(oCompanyUserInfo);
            return da.GetConnection();
        }

        #endregion

        public static AppUserInfo GetAppUserFromCompanyUserInfo(CompanyUserInfo oCompanyUserInfo)
        {
            AppUserInfo oAppUser = new AppUserInfo();
            try
            {
                oAppUser.CompanyID = oCompanyUserInfo.CompanyID;
                oAppUser.UserID = oCompanyUserInfo.UserID;
                DataAccess oDA = new DataAccess(oCompanyUserInfo);
                oAppUser.ConnectionString = oDA.GetConnectionString();
            }
            catch (Exception ex)
            {
            }
            return oAppUser;
        }

        public static void GetAccountDetailsForMail(StringBuilder AccountDetails, List<AccountHdrInfo> oListAccountHdrInfoForUser, int? languageID, int? CompanyID, string AccountNumber, string AccountName, MultilingualAttributeInfo oMultilingualAttributeInfo)
        {
            if (oListAccountHdrInfoForUser != null && oListAccountHdrInfoForUser.Count > 0)
            {

                // Get the Organizational Hierarchy based on Company ID
                List<GeographyStructureHdrInfo> oGeographyStructureHdrInfoCollection = GetOrganizationalHierarchy(CompanyID);

                //Translate
                for (int i = 0; i < oGeographyStructureHdrInfoCollection.Count; i++)
                {
                    oGeographyStructureHdrInfoCollection[i].GeographyStructure = LanguageUtil.GetValue(oGeographyStructureHdrInfoCollection[i].GeographyStructureLabelID.Value, oMultilingualAttributeInfo);
                }

                string netAccount = LanguageUtil.GetValue(1257, oMultilingualAttributeInfo);
                string NewAccountString = LanguageUtil.GetValue(2810, oMultilingualAttributeInfo);
                string ModifiedAccountString = LanguageUtil.GetValue(2811, oMultilingualAttributeInfo);

                foreach (AccountHdrInfo oAccountHdrInfo in oListAccountHdrInfoForUser)
                {
                    TranslateLabelsAccountHdr(oAccountHdrInfo, oMultilingualAttributeInfo);
                }

                AccountDetails.Append("<TABLE border=1 cellpadding=0 cellspacing=0>");
                AccountDetails.Append("<TR>");


                for (int i = 1; i < oGeographyStructureHdrInfoCollection.Count; i++)
                {

                    AccountDetails.Append("<TH>");
                    AccountDetails.Append(oGeographyStructureHdrInfoCollection[i].GeographyStructure);
                    AccountDetails.Append("</TH>");

                }
                // Account Number
                AccountDetails.Append("<TH>");
                AccountDetails.Append(AccountNumber); //1357
                AccountDetails.Append("</TH>");

                // Account Name
                AccountDetails.Append("<TH>");
                AccountDetails.Append(AccountName); //1346
                AccountDetails.Append("</TH>");

                //Change Type
                if (oListAccountHdrInfoForUser.Count > 0 && oListAccountHdrInfoForUser[0].ChangeTypeLabelID.HasValue)
                {
                    AccountDetails.Append("<TH>");
                    AccountDetails.Append(LanguageUtil.GetValue(2812, oMultilingualAttributeInfo)); //2812
                    AccountDetails.Append("</TH>");
                }

                AccountDetails.Append("</TR>");
                foreach (AccountHdrInfo oAccountHdrInfo in oListAccountHdrInfoForUser)
                {
                    AccountDetails.Append("<TR>");
                    for (int i = 1; i < oGeographyStructureHdrInfoCollection.Count; i++)
                    {

                        string GeoClassName = "";
                        switch (oGeographyStructureHdrInfoCollection[i].GeographyClassID)
                        {
                            case (short)Enums.GeographyClass.Company:
                                GeoClassName = GeographyClassName.KEY1;
                                break;
                            case (short)Enums.GeographyClass.Key2:
                                GeoClassName = GeographyClassName.KEY2;
                                break;
                            case (short)Enums.GeographyClass.Key3:
                                GeoClassName = GeographyClassName.KEY3;
                                break;
                            case (short)Enums.GeographyClass.Key4:
                                GeoClassName = GeographyClassName.KEY4;
                                break;
                            case (short)Enums.GeographyClass.Key5:
                                GeoClassName = GeographyClassName.KEY5;
                                break;
                            case (short)Enums.GeographyClass.Key6:
                                GeoClassName = GeographyClassName.KEY6;
                                break;
                            case (short)Enums.GeographyClass.Key7:
                                GeoClassName = GeographyClassName.KEY7;
                                break;
                            case (short)Enums.GeographyClass.Key8:
                                GeoClassName = GeographyClassName.KEY8;
                                break;
                            case (short)Enums.GeographyClass.Key9:
                                GeoClassName = GeographyClassName.KEY9;
                                break;
                        }
                        AccountDetails.Append("<TD>");
                        AccountDetails.Append(oAccountHdrInfo.GetType().GetProperty(GeoClassName).GetValue(oAccountHdrInfo, null).ToString());
                        AccountDetails.Append("</TD>");

                    }
                    // Account Number
                    AccountDetails.Append("<TD>");
                    if (oAccountHdrInfo.AccountID != null && oAccountHdrInfo.AccountID > 0)
                        AccountDetails.Append(oAccountHdrInfo.AccountNumber);
                    else
                        AccountDetails.Append(netAccount);
                    AccountDetails.Append("</TD>");

                    // Account Name
                    AccountDetails.Append("<TD>");
                    AccountDetails.Append(oAccountHdrInfo.AccountName);
                    AccountDetails.Append("</TD>");

                    // Change Type
                    if (oAccountHdrInfo.ChangeTypeLabelID.HasValue)
                    {
                        AccountDetails.Append("<TD>");
                        if (oAccountHdrInfo.ChangeTypeLabelID.Value == 2810)
                            AccountDetails.Append(NewAccountString);
                        else
                            AccountDetails.Append(ModifiedAccountString);
                        AccountDetails.Append("</TD>");
                    }

                    AccountDetails.Append("</TR>");
                }

                AccountDetails.Append("</TABLE>");
            }
        }
        public static void GetAccountDetailsForAlertMail(StringBuilder AccountDetails, List<AccountHdrInfo> oListAccountHdrInfoForUser,
            int? languageID, int? CompanyID, string AccountNumber, string AccountName,
            bool isDateColumnRequired, bool isNumberColumnRequired,
            MultilingualAttributeInfo oMultilingualAttributeInfo, string NumberofDaysHeading, string DueDateHeading)
        {
            if (oListAccountHdrInfoForUser != null && oListAccountHdrInfoForUser.Count > 0)
            {

                // Get the Organizational Hierarchy based on Company ID
                List<GeographyStructureHdrInfo> oGeographyStructureHdrInfoCollection = GetOrganizationalHierarchy(CompanyID);

                //Translate
                for (int i = 0; i < oGeographyStructureHdrInfoCollection.Count; i++)
                {
                    oGeographyStructureHdrInfoCollection[i].GeographyStructure = LanguageUtil.GetValue(oGeographyStructureHdrInfoCollection[i].GeographyStructureLabelID.Value, oMultilingualAttributeInfo);
                }

                string netAccount = LanguageUtil.GetValue(1257, oMultilingualAttributeInfo);

                foreach (AccountHdrInfo oAccountHdrInfo in oListAccountHdrInfoForUser)
                {
                    TranslateLabelsAccountHdr(oAccountHdrInfo, oMultilingualAttributeInfo);
                }

                AccountDetails.Append("<TABLE border=1 cellpadding=0 cellspacing=0>");
                AccountDetails.Append("<TR>");

                // FSCaption 
                AccountDetails.Append("<TH>");
                AccountDetails.Append(LanguageUtil.GetValue(1337, oMultilingualAttributeInfo)); //1337 
                AccountDetails.Append("</TH>");

                // Account Type
                AccountDetails.Append("<TH>");
                AccountDetails.Append(LanguageUtil.GetValue(1363, oMultilingualAttributeInfo)); //1363 
                AccountDetails.Append("</TH>");

                for (int i = 1; i < oGeographyStructureHdrInfoCollection.Count; i++)
                {

                    AccountDetails.Append("<TH>");
                    AccountDetails.Append(oGeographyStructureHdrInfoCollection[i].GeographyStructure);
                    AccountDetails.Append("</TH>");

                }
                // Account Number
                AccountDetails.Append("<TH>");
                AccountDetails.Append(AccountNumber); //1357
                AccountDetails.Append("</TH>");

                // Account Name
                AccountDetails.Append("<TH>");
                AccountDetails.Append(AccountName); //1346
                AccountDetails.Append("</TH>");

                if (isDateColumnRequired)
                {
                    // Due Date
                    AccountDetails.Append("<TH>");
                    AccountDetails.Append(DueDateHeading);
                    AccountDetails.Append("</TH>");
                }

                if (isNumberColumnRequired)
                {
                    // Number of Days
                    AccountDetails.Append("<TH>");
                    AccountDetails.Append(NumberofDaysHeading); //1300 
                    AccountDetails.Append("</TH>");
                }



                AccountDetails.Append("</TR>");
                foreach (AccountHdrInfo oAccountHdrInfo in oListAccountHdrInfoForUser)
                {
                    AccountDetails.Append("<TR>");

                    // FSCaption
                    AccountDetails.Append("<TD>");
                    if (oAccountHdrInfo.FSCaptionLabelID.HasValue)
                        AccountDetails.Append(LanguageUtil.GetValue(oAccountHdrInfo.FSCaptionLabelID.Value, oMultilingualAttributeInfo));
                    else
                        AccountDetails.Append(" ");
                    AccountDetails.Append("</TD>");

                    // Account Type
                    AccountDetails.Append("<TD>");
                    if (oAccountHdrInfo.AccountTypeLabelID.HasValue)
                        AccountDetails.Append(LanguageUtil.GetValue(oAccountHdrInfo.AccountTypeLabelID.Value, oMultilingualAttributeInfo));
                    else
                        AccountDetails.Append(" ");
                    AccountDetails.Append("</TD>");


                    for (int i = 1; i < oGeographyStructureHdrInfoCollection.Count; i++)
                    {

                        string GeoClassName = "";
                        switch (oGeographyStructureHdrInfoCollection[i].GeographyClassID)
                        {
                            case (short)Enums.GeographyClass.Company:
                                GeoClassName = GeographyClassName.KEY1;
                                break;
                            case (short)Enums.GeographyClass.Key2:
                                GeoClassName = GeographyClassName.KEY2;
                                break;
                            case (short)Enums.GeographyClass.Key3:
                                GeoClassName = GeographyClassName.KEY3;
                                break;
                            case (short)Enums.GeographyClass.Key4:
                                GeoClassName = GeographyClassName.KEY4;
                                break;
                            case (short)Enums.GeographyClass.Key5:
                                GeoClassName = GeographyClassName.KEY5;
                                break;
                            case (short)Enums.GeographyClass.Key6:
                                GeoClassName = GeographyClassName.KEY6;
                                break;
                            case (short)Enums.GeographyClass.Key7:
                                GeoClassName = GeographyClassName.KEY7;
                                break;
                            case (short)Enums.GeographyClass.Key8:
                                GeoClassName = GeographyClassName.KEY8;
                                break;
                            case (short)Enums.GeographyClass.Key9:
                                GeoClassName = GeographyClassName.KEY9;
                                break;
                        }
                        AccountDetails.Append("<TD>");
                        AccountDetails.Append(oAccountHdrInfo.GetType().GetProperty(GeoClassName).GetValue(oAccountHdrInfo, null).ToString());
                        AccountDetails.Append("</TD>");

                    }
                    // Account Number
                    AccountDetails.Append("<TD>");
                    if (oAccountHdrInfo.AccountID != null && oAccountHdrInfo.AccountID > 0)
                        AccountDetails.Append(oAccountHdrInfo.AccountNumber);
                    else
                        AccountDetails.Append(netAccount);
                    AccountDetails.Append("</TD>");

                    // Account Name
                    AccountDetails.Append("<TD>");
                    AccountDetails.Append(oAccountHdrInfo.AccountName);
                    AccountDetails.Append("</TD>");

                    if (isDateColumnRequired)
                    {
                        // Due Date
                        AccountDetails.Append("<TD>");
                        AccountDetails.Append(SharedHelper.GetDisplayDate(oAccountHdrInfo.DateValue, oMultilingualAttributeInfo));
                        AccountDetails.Append("</TD>");
                    }

                    if (isNumberColumnRequired)
                    {
                        // Number of days
                        AccountDetails.Append("<TD>");
                        AccountDetails.Append(oAccountHdrInfo.NumberValue);
                        AccountDetails.Append("</TD>");
                    }

                    AccountDetails.Append("</TR>");
                }

                AccountDetails.Append("</TABLE>");
            }
        }
        public static void GetTaskDetailsForAlertMail(StringBuilder TaskDetails, List<TaskHdrInfo> oListTaskHdrInfoForUser,
            int? languageID, int? CompanyID,
            bool isDateColumnRequired, bool isNumberColumnRequired,
            MultilingualAttributeInfo oMultilingualAttributeInfo, string NumberofDaysHeading, string DueDateHeading)
        {
            if (oListTaskHdrInfoForUser != null && oListTaskHdrInfoForUser.Count > 0)
            {

                string netAccount = LanguageUtil.GetValue(1257, oMultilingualAttributeInfo);

                foreach (TaskHdrInfo oTaskHdrInfo in oListTaskHdrInfoForUser)
                {
                    TranslateLabelsTaskHdr(oTaskHdrInfo, oMultilingualAttributeInfo);
                }

                TaskDetails.Append(SharedUtility.MailHelper.GetBeginTableHTML());
                TaskDetails.Append(SharedUtility.MailHelper.GetBeginHaderRowHTML());

                // Task List Name 
                TaskDetails.Append("<TH>");
                TaskDetails.Append(LanguageUtil.GetValue(2584, oMultilingualAttributeInfo)); //2584 
                TaskDetails.Append("</TH>");

                //Task#
                TaskDetails.Append("<TH>");
                TaskDetails.Append(LanguageUtil.GetValue(2544, oMultilingualAttributeInfo)); // 2544  
                TaskDetails.Append("</TH>");

                //Task Name
                TaskDetails.Append("<TH>");
                TaskDetails.Append(LanguageUtil.GetValue(2545, oMultilingualAttributeInfo)); // 2545  
                TaskDetails.Append("</TH>");


                // Task Status
                TaskDetails.Append("<TH>");
                TaskDetails.Append(LanguageUtil.GetValue(2576, oMultilingualAttributeInfo)); //2576
                TaskDetails.Append("</TH>");

                // Task Due Date 
                TaskDetails.Append("<TH>");
                TaskDetails.Append(LanguageUtil.GetValue(2566, oMultilingualAttributeInfo));  //2566
                TaskDetails.Append("</TH>");

                // Assignee Due Date 
                TaskDetails.Append("<TH>");
                TaskDetails.Append(LanguageUtil.GetValue(2567, oMultilingualAttributeInfo));  //2567
                TaskDetails.Append("</TH>");

                // Reviewer Due Date 
                TaskDetails.Append("<TH>");
                TaskDetails.Append(LanguageUtil.GetValue(1418, oMultilingualAttributeInfo));  //1418
                TaskDetails.Append("</TH>");


                // Description
                TaskDetails.Append("<TH>");
                TaskDetails.Append(LanguageUtil.GetValue(1408, oMultilingualAttributeInfo)); //1408 
                TaskDetails.Append("</TH>");

                if (isDateColumnRequired)
                {
                    // Due Date
                    TaskDetails.Append("<TH>");
                    TaskDetails.Append(DueDateHeading);
                    TaskDetails.Append("</TH>");
                }

                if (isNumberColumnRequired)
                {
                    // Number of Days
                    TaskDetails.Append("<TH>");
                    TaskDetails.Append(NumberofDaysHeading); //1300 
                    TaskDetails.Append("</TH>");
                }



                TaskDetails.Append("</TR>");
                foreach (TaskHdrInfo oTaskHdrInfo in oListTaskHdrInfoForUser)
                {
                    TaskDetails.Append("<TR>");

                    // Task List Name
                    TaskDetails.Append("<TD>");
                    if (oTaskHdrInfo.TaskListName != null)
                        TaskDetails.Append(oTaskHdrInfo.TaskListName);
                    else
                        TaskDetails.Append(" ");
                    TaskDetails.Append("</TD>");

                    // Task#
                    TaskDetails.Append("<TD>");
                    if (oTaskHdrInfo.TaskNumber != null)
                        TaskDetails.Append(oTaskHdrInfo.TaskNumber);
                    else
                        TaskDetails.Append(" ");
                    TaskDetails.Append("</TD>");

                    // Task Name
                    TaskDetails.Append("<TD>");
                    if (oTaskHdrInfo.TaskName != null)
                        TaskDetails.Append(oTaskHdrInfo.TaskName);
                    else
                        TaskDetails.Append(" ");
                    TaskDetails.Append("</TD>");

                    // Task Status
                    TaskDetails.Append("<TD>");
                    if (oTaskHdrInfo.TaskStatus != null)
                        TaskDetails.Append(oTaskHdrInfo.TaskStatus);
                    else
                        TaskDetails.Append(" ");
                    TaskDetails.Append("</TD>");

                    //  Task Due Date 
                    DateTime tmpTaskDueDate;
                    TaskDetails.Append("<TD>");
                    if (oTaskHdrInfo.TaskDueDate.HasValue && Helper.IsValidDateTime(oTaskHdrInfo.TaskDueDate.Value.ToString(), languageID.Value, out tmpTaskDueDate))
                        TaskDetails.Append(SharedHelper.GetDisplayDate(tmpTaskDueDate, oMultilingualAttributeInfo));
                    else
                        TaskDetails.Append(" ");
                    TaskDetails.Append("</TD>");

                    //  Assignee Due Date 
                    DateTime tmpAssigneeDueDate;
                    TaskDetails.Append("<TD>");
                    if (oTaskHdrInfo.AssigneeDueDate.HasValue && Helper.IsValidDateTime(oTaskHdrInfo.AssigneeDueDate.Value.ToString(), languageID.Value, out tmpAssigneeDueDate))
                        TaskDetails.Append(SharedHelper.GetDisplayDate(tmpAssigneeDueDate, oMultilingualAttributeInfo));
                    else
                        TaskDetails.Append(" ");
                    TaskDetails.Append("</TD>");

                    //  Reviewer Due Date 
                    DateTime tmpReviewerDueDate;
                    TaskDetails.Append("<TD>");
                    if (oTaskHdrInfo.ReviewerDueDate.HasValue && Helper.IsValidDateTime(oTaskHdrInfo.ReviewerDueDate.Value.ToString(), languageID.Value, out tmpReviewerDueDate))
                        TaskDetails.Append(SharedHelper.GetDisplayDate(tmpReviewerDueDate, oMultilingualAttributeInfo));
                    else
                        TaskDetails.Append(" ");
                    TaskDetails.Append("</TD>");

                    // Description
                    TaskDetails.Append("<TD>");
                    if (oTaskHdrInfo.TaskDescription != null)
                        TaskDetails.Append(oTaskHdrInfo.TaskDescription);
                    else
                        TaskDetails.Append(" ");
                    TaskDetails.Append("</TD>");

                    if (isDateColumnRequired)
                    {
                        // Due Date
                        TaskDetails.Append("<TD>");
                        if (oTaskHdrInfo.DateValue.HasValue)
                            TaskDetails.Append(SharedHelper.GetDisplayDate(oTaskHdrInfo.DateValue, oMultilingualAttributeInfo));
                        else
                            TaskDetails.Append(" ");
                        TaskDetails.Append("</TD>");
                    }

                    if (isNumberColumnRequired)
                    {
                        // Number of days
                        TaskDetails.Append("<TD>");
                        if (oTaskHdrInfo.DateValue.HasValue)
                            TaskDetails.Append(oTaskHdrInfo.NumberValue);
                        else
                            TaskDetails.Append(" ");
                        TaskDetails.Append("</TD>");
                    }


                    TaskDetails.Append("</TR>");
                }

                TaskDetails.Append("</TABLE>");
            }
        }
        public static List<GeographyStructureHdrInfo> GetOrganizationalHierarchy(int? CompanyID)
        {
            if (CompanyID == null)
            {
                throw new Exception(COMPANY_NOT_SPECIFIED);
            }
            // Get from DB and Add to Dictionary
            ICompany oCompanyClient = RemotingHelper.GetCompanyObject();
            AppUserInfo oAppUserInfo = new AppUserInfo();
            oAppUserInfo.CompanyID = CompanyID;
            List<GeographyStructureHdrInfo> oGeographyStructureHdrInfoCollection = oCompanyClient.GetOrganizationalHierarchy(CompanyID, oAppUserInfo);
            return oGeographyStructureHdrInfoCollection;
        }

        private static void TranslateLabelsAccountHdr(AccountHdrInfo oAccountHdrInfo, MultilingualAttributeInfo oMultilingualAttributeInfo)
        {
            TranslateLabelOrganizationalHierarchyInfo(oAccountHdrInfo, oMultilingualAttributeInfo);

            if (oAccountHdrInfo.AccountNameLabelID.HasValue)
            {
                oAccountHdrInfo.AccountName = LanguageUtil.GetValue(oAccountHdrInfo.AccountNameLabelID.Value, oMultilingualAttributeInfo);
            }

            if (oAccountHdrInfo.DescriptionLabelID.HasValue && oAccountHdrInfo.DescriptionLabelID.Value > 0)
            {
                oAccountHdrInfo.Description = LanguageUtil.GetValue(oAccountHdrInfo.DescriptionLabelID.Value, oMultilingualAttributeInfo);
            }

        }
        private static void TranslateLabelsTaskHdr(TaskHdrInfo oTaskHdrInfo, MultilingualAttributeInfo oMultilingualAttributeInfo)
        {

            if (oTaskHdrInfo.TaskStatusLabelID.HasValue)
            {
                oTaskHdrInfo.TaskStatus = LanguageUtil.GetValue(oTaskHdrInfo.TaskStatusLabelID.Value, oMultilingualAttributeInfo);
            }

        }

        public static void TranslateLabelOrganizationalHierarchyInfo(OrganizationalHierarchyInfo oOrganizationalHierarchyInfo, MultilingualAttributeInfo oMultilingualAttributeInfo)
        {
            if (oOrganizationalHierarchyInfo.FSCaptionLabelID.HasValue && oOrganizationalHierarchyInfo.FSCaptionLabelID.Value > 0)
            {
                oOrganizationalHierarchyInfo.FSCaption = LanguageUtil.GetValue(oOrganizationalHierarchyInfo.FSCaptionLabelID.Value, oMultilingualAttributeInfo).Trim();
            }

            if (oOrganizationalHierarchyInfo.Key1LabelID.HasValue && oOrganizationalHierarchyInfo.Key1LabelID.Value > 0)
            {
                oOrganizationalHierarchyInfo.Key1 = LanguageUtil.GetValue(oOrganizationalHierarchyInfo.Key1LabelID.Value, oMultilingualAttributeInfo).Trim();
            }

            if (oOrganizationalHierarchyInfo.Key2LabelID.HasValue && oOrganizationalHierarchyInfo.Key2LabelID.Value > 0)
            {
                oOrganizationalHierarchyInfo.Key2 = LanguageUtil.GetValue(oOrganizationalHierarchyInfo.Key2LabelID.Value, oMultilingualAttributeInfo).Trim();
            }

            if (oOrganizationalHierarchyInfo.Key3LabelID.HasValue && oOrganizationalHierarchyInfo.Key3LabelID.Value > 0)
            {
                oOrganizationalHierarchyInfo.Key3 = LanguageUtil.GetValue(oOrganizationalHierarchyInfo.Key3LabelID.Value, oMultilingualAttributeInfo).Trim();
            }

            if (oOrganizationalHierarchyInfo.Key4LabelID.HasValue && oOrganizationalHierarchyInfo.Key4LabelID.Value > 0)
            {
                oOrganizationalHierarchyInfo.Key4 = LanguageUtil.GetValue(oOrganizationalHierarchyInfo.Key4LabelID.Value, oMultilingualAttributeInfo).Trim();
            }

            if (oOrganizationalHierarchyInfo.Key5LabelID.HasValue && oOrganizationalHierarchyInfo.Key5LabelID.Value > 0)
            {
                oOrganizationalHierarchyInfo.Key5 = LanguageUtil.GetValue(oOrganizationalHierarchyInfo.Key5LabelID.Value, oMultilingualAttributeInfo).Trim();
            }

            if (oOrganizationalHierarchyInfo.Key6LabelID.HasValue && oOrganizationalHierarchyInfo.Key6LabelID.Value > 0)
            {
                oOrganizationalHierarchyInfo.Key6 = LanguageUtil.GetValue(oOrganizationalHierarchyInfo.Key6LabelID.Value, oMultilingualAttributeInfo).Trim();
            }

            if (oOrganizationalHierarchyInfo.Key7LabelID.HasValue && oOrganizationalHierarchyInfo.Key7LabelID.Value > 0)
            {
                oOrganizationalHierarchyInfo.Key7 = LanguageUtil.GetValue(oOrganizationalHierarchyInfo.Key7LabelID.Value, oMultilingualAttributeInfo).Trim();
            }

            if (oOrganizationalHierarchyInfo.Key8LabelID.HasValue && oOrganizationalHierarchyInfo.Key8LabelID.Value > 0)
            {
                oOrganizationalHierarchyInfo.Key8 = LanguageUtil.GetValue(oOrganizationalHierarchyInfo.Key8LabelID.Value, oMultilingualAttributeInfo).Trim();
            }

            if (oOrganizationalHierarchyInfo.Key9LabelID.HasValue && oOrganizationalHierarchyInfo.Key9LabelID.Value > 0)
            {
                oOrganizationalHierarchyInfo.Key9 = LanguageUtil.GetValue(oOrganizationalHierarchyInfo.Key9LabelID.Value, oMultilingualAttributeInfo).Trim();
            }
        }

        public static string GetUserIDForServiceProcessing()
        {
            return ServiceConstants.SERVICE_PROCESSING_USER_ID;
        }

        public static decimal GetFileSize(string fileName)
        {
            if (!string.IsNullOrEmpty(fileName) && File.Exists(fileName))
            {
                FileInfo oFileInfo = new FileInfo(fileName);
                return (decimal)oFileInfo.Length / (1024 * 1024);
            }
            return 0M;
        }

        public static void GetCompanyDataStorageCapacityAndCurrentUsage(int companyID, out decimal? dataStorageCapacity, out decimal? currentUsage)
        {
            dataStorageCapacity = 0;
            currentUsage = 0;

            AppUserInfo oAppUserInfo = new AppUserInfo();
            oAppUserInfo.CompanyID = companyID;
            ICompany oCompanyClient = RemotingHelper.GetCompanyObject();
            oCompanyClient.GetCompanyDataStorageCapacityAndCurrentUsage(companyID, out dataStorageCapacity, out currentUsage, oAppUserInfo);
        }

        public static string ReplaceSpecialChars(string fileName)
        {
            char[] invalidChars = System.IO.Path.GetInvalidFileNameChars();
            foreach (char c in invalidChars)
                fileName = fileName.Replace(c, '-');
            return fileName;
        }

        public static bool IsAlertConfiguredByTheCompany(short alertID, TaskImportInfo oTaskImportInfo, out int? companyAlertID)
        {
            AppUserInfo oAppUserInfo = new AppUserInfo();
            oAppUserInfo.CompanyID = oTaskImportInfo.CompanyID;
            IAlert oAlertClient = RemotingHelper.GetAlertObject();
            List<CompanyAlertInfo> oCompanyAlertInfoCollection = oAlertClient.SelectComapnyAlertByCompanyIDAndRecPeriodID(oTaskImportInfo.CompanyID, oTaskImportInfo.RecPeriodID, oAppUserInfo);
            CompanyAlertInfo oCompanyAlertInfo = oCompanyAlertInfoCollection.Find(a => a.AlertID == alertID);
            if (oCompanyAlertInfo != null)
            {
                companyAlertID = oCompanyAlertInfo.CompanyAlertID;
                return true;
            }
            companyAlertID = null;
            return false;
        }

        public static void RaiseAlertForAssignedTask(List<int> assignedAssignedToIDCollection, List<int> assignedReviewerIDCollection, List<int> assignedApproverIDCollection, List<int> assignedNotifyIDCollection, List<TaskHdrInfo> oTaskHdrInfoList, TaskImportInfo oTaskImportInfo, CompanyUserInfo oCompanyUserInfo)
        {
            short AlertID = 26;
            int? companyAlertID;
            string replacement;
            int? alertLabelID;
            int recPeriodID = oTaskImportInfo.RecPeriodID;
            AppUserInfo oAppUserInfo = new AppUserInfo();
            oAppUserInfo.CompanyID = oTaskImportInfo.CompanyID;
            bool isAlertSubscribed = Helper.IsAlertConfiguredByTheCompany(AlertID, oTaskImportInfo, out companyAlertID);
            if (isAlertSubscribed)
            {
                IAlert oAlertClient = RemotingHelper.GetAlertObject();
                alertLabelID = oAlertClient.GetAlertDescriptionAndReplacementString(AlertID, recPeriodID, null, out replacement, oAppUserInfo);
                List<CompanyAlertInfo> oCompanyAlertInfoCollection = oAlertClient.SelectComapnyAlertByCompanyIDAndRecPeriodID(oTaskImportInfo.CompanyID, oTaskImportInfo.RecPeriodID, oAppUserInfo);
                short? noOfHours = oCompanyAlertInfoCollection.Find(CA => CA.CompanyAlertID == companyAlertID).NoOfHours;
                IUser oUserClient = RemotingHelper.GetUserObject();
                List<UserHdrInfo> oAllUserHdrInfoCollection = oUserClient.SelectAllUserHdrInfoByCompanyIDAndRoleID(oTaskImportInfo.CompanyID, 0, oAppUserInfo);
                List<UserHdrInfo> oassignedAssignedToIDCollection = GetUsersDetails(oAllUserHdrInfoCollection, assignedAssignedToIDCollection);
                List<UserHdrInfo> oassignedReviewerIDCollection = GetUsersDetails(oAllUserHdrInfoCollection, assignedReviewerIDCollection);
                List<UserHdrInfo> oassignedApproverIDCollection = GetUsersDetails(oAllUserHdrInfoCollection, assignedApproverIDCollection);
                List<UserHdrInfo> oassignedNotifyIDCollection = GetUsersDetails(oAllUserHdrInfoCollection, assignedNotifyIDCollection);
                foreach (var item in oTaskHdrInfoList)
                {
                    item.AssignedTo = GetUsersDetails(oAllUserHdrInfoCollection, item.AssignedTo);
                    item.Reviewer = GetUsersDetails(oAllUserHdrInfoCollection, item.Reviewer);
                    item.Approver = GetUsersDetails(oAllUserHdrInfoCollection, item.Approver);
                    item.Notify = GetUsersDetails(oAllUserHdrInfoCollection, item.Notify);
                }

                //raise alert
                if (oassignedAssignedToIDCollection != null && oassignedAssignedToIDCollection.Count > 0)
                {
                    RaiseTaskAlertForUsers(alertLabelID, 1654, 1130, companyAlertID, noOfHours, recPeriodID, oassignedAssignedToIDCollection, (short)Enums.UserRoles.Preparer, oTaskHdrInfoList, oTaskImportInfo, oCompanyUserInfo);
                }

                if (oassignedReviewerIDCollection != null && oassignedReviewerIDCollection.Count > 0)
                {
                    RaiseTaskAlertForUsers(alertLabelID, 1654, 1131, companyAlertID, noOfHours, recPeriodID, oassignedReviewerIDCollection, (short)Enums.UserRoles.Reviewer, oTaskHdrInfoList, oTaskImportInfo, oCompanyUserInfo);
                }

                if (oassignedApproverIDCollection != null && oassignedApproverIDCollection.Count > 0)
                {
                    RaiseTaskAlertForUsers(alertLabelID, 1654, 1132, companyAlertID, noOfHours, recPeriodID, oassignedApproverIDCollection, (short)Enums.UserRoles.Approver, oTaskHdrInfoList, oTaskImportInfo, oCompanyUserInfo);
                }
                //alert to notify user
                if (oassignedNotifyIDCollection != null && oassignedNotifyIDCollection.Count > 0)
                {
                    RaiseTaskAlertForUsers(alertLabelID, 1654, 1312, companyAlertID, noOfHours, recPeriodID, oassignedNotifyIDCollection, null, oTaskHdrInfoList, oTaskImportInfo, oCompanyUserInfo);
                }

            }
        }
        private static void RaiseTaskAlertForUsers(int? alertLabelID, int actionLabelID, int roleLabelID, int? companyAlertID, short? noOfHours, int recPeriodID, List<UserHdrInfo> oUserHdrInfoCollection, short? roleID, List<TaskHdrInfo> oTaskHdrInfoList, TaskImportInfo oTaskImportInfo, CompanyUserInfo oCompanyUserInfo)
        {
            AppUserInfo oAppUserInfo = new AppUserInfo();
            oAppUserInfo.CompanyID = oTaskImportInfo.CompanyID;
            IAlert oAlertClient = RemotingHelper.GetAlertObject();
            string alertDescription = string.Empty;
            List<CompanyAlertDetailInfo> oCompanyAlertDetailInfoCollection = GetTaskAlertDetailInfoCollection(companyAlertID, null, alertLabelID, null, noOfHours, recPeriodID, oUserHdrInfoCollection, roleID, actionLabelID, roleLabelID, oTaskImportInfo);
            oAlertClient.InsertCompanyAlertDetail(oCompanyAlertDetailInfoCollection, oAppUserInfo);
            SendMailToAllUsers(null, alertLabelID, null, oUserHdrInfoCollection, oTaskHdrInfoList, actionLabelID, roleLabelID, oTaskImportInfo, oCompanyUserInfo);

        }
        private static List<CompanyAlertDetailInfo> GetTaskAlertDetailInfoCollection(int? companyAlertID, string alertDescription, int? alertLabelID, string replacement, short? noOfHours, int recPeriodID, List<UserHdrInfo> oUserHdrInfoCollection, short? roleID, int actionLabelID, int roleLabelID, TaskImportInfo oTaskImportInfo)
        {
            List<CompanyAlertDetailInfo> oCompanyAlertDetailInfoCollection = new List<CompanyAlertDetailInfo>();
            CompanyAlertDetailInfo oCompanyAlertDetailInfo = null;
            string userLoginID = oTaskImportInfo.AddedBy;
            DateTime updateTime = DateTime.Now;

            foreach (UserHdrInfo oUserHdrInfo in oUserHdrInfoCollection)
            {
                MultilingualAttributeInfo oMultilingualAttributeInfo;
                if (oUserHdrInfo.DefaultLanguageID.HasValue)
                    oMultilingualAttributeInfo = Helper.GetMultilingualAttributeInfo(oUserHdrInfo.DefaultLanguageID.Value, oTaskImportInfo.CompanyID);
                else
                    oMultilingualAttributeInfo = Helper.GetMultilingualAttributeInfo(1033, oTaskImportInfo.CompanyID);
                if (alertLabelID.HasValue)
                {
                    if (actionLabelID > 0 && roleLabelID > 0)
                    {
                        alertDescription = GetAlertDescription(alertLabelID, actionLabelID, roleLabelID, oMultilingualAttributeInfo);
                    }
                    else if (!string.IsNullOrEmpty(oUserHdrInfo.AlertReplacement))
                    {
                        alertDescription = GetAlertDescription(alertLabelID, oUserHdrInfo.AlertReplacement, oMultilingualAttributeInfo);
                    }
                    else
                    {
                        alertDescription = GetAlertDescription(alertLabelID, replacement, oMultilingualAttributeInfo);
                    }
                }


                oCompanyAlertDetailInfo = new CompanyAlertDetailInfo();
                oCompanyAlertDetailInfo.AddedBy = userLoginID;
                oCompanyAlertDetailInfo.AlertDescription = alertDescription;
                if (noOfHours.HasValue && noOfHours.Value > 0)
                {
                    oCompanyAlertDetailInfo.AlertExpectedDateTime = updateTime.AddHours(noOfHours.Value);
                }
                oCompanyAlertDetailInfo.CompanyAlertID = companyAlertID;
                oCompanyAlertDetailInfo.DateAdded = updateTime;
                oCompanyAlertDetailInfo.IsActive = true;
                oCompanyAlertDetailInfo.ReconciliationPeriodID = recPeriodID;

                oCompanyAlertDetailInfo.RoleID = roleID;

                oCompanyAlertDetailInfo.UserID = oUserHdrInfo.UserID;

                oCompanyAlertDetailInfoCollection.Add(oCompanyAlertDetailInfo);
            }
            return oCompanyAlertDetailInfoCollection;
        }
        public static string GetAlertDescription(int? alertLabelID, string replacement, MultilingualAttributeInfo oMultilingualAttributeInfo)
        {
            string alertDescription = string.Format(LanguageUtil.GetValue(alertLabelID.Value, oMultilingualAttributeInfo), replacement);

            return alertDescription;
        }
        private static string GetAlertDescription(int? alertLabelID, int actionLabelID, int roleLabelID, MultilingualAttributeInfo oMultilingualAttributeInfo)
        {
            string alertDescription = string.Format(LanguageUtil.GetValue(alertLabelID.Value, oMultilingualAttributeInfo), LanguageUtil.GetValue(actionLabelID, oMultilingualAttributeInfo), LanguageUtil.GetValue(roleLabelID, oMultilingualAttributeInfo));

            return alertDescription;
        }
        private static List<UserHdrInfo> GetUsersDetails(List<UserHdrInfo> oAllUserHdrInfoCollection, List<int> UserIDCollection)
        {
            List<UserHdrInfo> oUserHdrInfoCollection = new List<UserHdrInfo>();
            UserHdrInfo oUserHdrInfo = null;

            foreach (int userID in UserIDCollection)
            {
                oUserHdrInfo = oAllUserHdrInfoCollection.Find(UH => UH.UserID == userID);
                oUserHdrInfoCollection.Add(oUserHdrInfo);
            }

            return oUserHdrInfoCollection;
        }
        private static List<UserHdrInfo> GetUsersDetails(List<UserHdrInfo> oAllUserHdrInfoCollection, List<UserHdrInfo> UserIDCollection)
        {
            List<UserHdrInfo> oUserHdrInfoCollection = new List<UserHdrInfo>();
            UserHdrInfo oUserHdrInfo = null;
            if (UserIDCollection != null && UserIDCollection.Count > 0)
            {
                foreach (UserHdrInfo obj in UserIDCollection)
                {
                    oUserHdrInfo = oAllUserHdrInfoCollection.Find(UH => UH.UserID.GetValueOrDefault() == obj.UserID.GetValueOrDefault());
                    oUserHdrInfoCollection.Add(oUserHdrInfo);
                }
            }
            return oUserHdrInfoCollection;
        }

        private static void SendMailToAllUsers(string alertDescription, int? alertLabelID, string replacement, List<UserHdrInfo> oUserHdrInfoCollection, List<TaskHdrInfo> oTaskHdrInfoList, int actionLabelID, int roleLabelID, TaskImportInfo oTaskImportInfo, CompanyUserInfo oCompanyUserInfo)
        {
            AppUserInfo oAppUserInfo = new AppUserInfo();
            oAppUserInfo.CompanyID = oTaskImportInfo.CompanyID;
            string fromEmailAddress = Helper.GetAppSettingFromKey("defaultEmailFromCompany");
            foreach (UserHdrInfo oUserHdrInfo in oUserHdrInfoCollection)
            {

                MultilingualAttributeInfo oMultilingualAttributeInfo;
                if (oUserHdrInfo.DefaultLanguageID.HasValue)
                    oMultilingualAttributeInfo = Helper.GetMultilingualAttributeInfo(oUserHdrInfo.DefaultLanguageID.Value, oTaskImportInfo.CompanyID);
                else
                    oMultilingualAttributeInfo = Helper.GetMultilingualAttributeInfo(1033, oTaskImportInfo.CompanyID);

                if (alertLabelID.HasValue)
                {
                    if (actionLabelID > 0 && roleLabelID > 0)
                    {
                        alertDescription = GetAlertDescription(alertLabelID, actionLabelID, roleLabelID, oMultilingualAttributeInfo);
                    }
                    else if (!string.IsNullOrEmpty(oUserHdrInfo.AlertReplacement))
                    {
                        alertDescription = GetAlertDescription(alertLabelID, oUserHdrInfo.AlertReplacement, oMultilingualAttributeInfo);
                    }
                    else
                    {
                        alertDescription = GetAlertDescription(alertLabelID, replacement, oMultilingualAttributeInfo);
                    }
                }

                StringBuilder mailBody = new StringBuilder();
                mailBody.Append(LanguageUtil.GetValue(1845, oMultilingualAttributeInfo));
                mailBody.Append(" ");
                mailBody.Append(oUserHdrInfo.FirstName);
                mailBody.Append("<br>");
                mailBody.Append(alertDescription);
                mailBody.Append("<br>");

                //To add Task Details
                if (oTaskHdrInfoList != null && oTaskHdrInfoList.Count > 0)
                {
                    mailBody.Append("<br>");
                    StringBuilder TaskDetails = new StringBuilder();
                    List<TaskHdrInfo> oListTaskHdrInfoForUser = null;
                    switch (roleLabelID)
                    {
                        case 1130: // AssignedTo
                            oListTaskHdrInfoForUser = oTaskHdrInfoList.FindAll(obj => (obj.AssignedTo != null && obj.AssignedTo.Exists(obj1 => obj1.UserID == oUserHdrInfo.UserID)));
                            break;
                        case 1131: // Reviewer
                            oListTaskHdrInfoForUser = oTaskHdrInfoList.FindAll(obj => (obj.Reviewer != null && obj.Reviewer.Exists(obj1 => obj1.UserID == oUserHdrInfo.UserID)));
                            break;
                        case 1132: //Approver
                            oListTaskHdrInfoForUser = oTaskHdrInfoList.FindAll(obj => (obj.Approver != null && obj.Approver.Exists(obj1 => obj1.UserID == oUserHdrInfo.UserID)));
                            break;
                        case 1312: //Notify                            
                            oListTaskHdrInfoForUser = oTaskHdrInfoList.FindAll(obj => (obj.Notify != null && obj.Notify.Exists(obj1 => obj1.UserID == oUserHdrInfo.UserID)));
                            break;
                    }
                    GetTaskDetailsForMail(TaskDetails, oListTaskHdrInfoForUser, oMultilingualAttributeInfo);
                    mailBody.Append(TaskDetails.ToString());
                }

                mailBody.Append(MailHelper.GetEmailSignature(Enums.SignatureEnum.SendBySystemAdmin, fromEmailAddress, null, oMultilingualAttributeInfo, null));
                MailHelper.SendEmail(fromEmailAddress, oUserHdrInfo.EmailID, alertDescription, mailBody.ToString(), oCompanyUserInfo);
            }
        }

        private static void GetTaskDetailsForMail(StringBuilder TaskDetails, List<TaskHdrInfo> oListTaskHdrInfoForUser, MultilingualAttributeInfo oMultilingualAttributeInfo)
        {
            if (oListTaskHdrInfoForUser != null && oListTaskHdrInfoForUser.Count > 0)
            {

                List<TaskHdrInfo> oListAccountTask = oListTaskHdrInfoForUser.FindAll(obj => (obj.TaskAccount != null));

                TaskDetails.Append(SharedUtility.MailHelper.GetBeginTableHTML());
                TaskDetails.Append(SharedUtility.MailHelper.GetBeginHaderRowHTML());

                //Task#
                TaskDetails.Append("<TH>");
                TaskDetails.Append(LanguageUtil.GetValue(2544, oMultilingualAttributeInfo));
                TaskDetails.Append("</TH>");

                //TaskName
                TaskDetails.Append("<TH>");
                TaskDetails.Append(LanguageUtil.GetValue(2545, oMultilingualAttributeInfo));
                TaskDetails.Append("</TH>");

                //TaskDesc
                TaskDetails.Append("<TH>");
                TaskDetails.Append(LanguageUtil.GetValue(1408, oMultilingualAttributeInfo));
                TaskDetails.Append("</TH>");

                // Due Date
                TaskDetails.Append("<TH>");
                TaskDetails.Append(LanguageUtil.GetValue(1499, oMultilingualAttributeInfo));
                TaskDetails.Append("</TH>");

                // Task Owner
                TaskDetails.Append("<TH>");
                TaskDetails.Append(LanguageUtil.GetValue(2550, oMultilingualAttributeInfo));
                TaskDetails.Append("</TH>");

                // Task Reviewer
                TaskDetails.Append("<TH>");
                TaskDetails.Append(LanguageUtil.GetValue(1131, oMultilingualAttributeInfo));
                TaskDetails.Append("</TH>");

                // Task Approver
                TaskDetails.Append("<TH>");
                TaskDetails.Append(LanguageUtil.GetValue(1132, oMultilingualAttributeInfo));
                TaskDetails.Append("</TH>");

                TaskDetails.Append("</TR>");
                foreach (TaskHdrInfo oTaskHdrInfo in oListTaskHdrInfoForUser)
                {
                    //Task#
                    TaskDetails.Append("<TR>");
                    TaskDetails.Append("<TD>");
                    TaskDetails.Append(oTaskHdrInfo.TaskNumber);
                    TaskDetails.Append("</TD>");


                    // Task Nuame
                    TaskDetails.Append("<TD>");
                    TaskDetails.Append(oTaskHdrInfo.TaskName);
                    TaskDetails.Append("</TD>");

                    //TaskDesc
                    TaskDetails.Append("<TD>");
                    TaskDetails.Append(oTaskHdrInfo.TaskDescription);
                    TaskDetails.Append("</TD>");

                    // Due Date
                    TaskDetails.Append("<TD>");
                    TaskDetails.Append(Helper.GetDisplayDate(oTaskHdrInfo.TaskDueDate));
                    TaskDetails.Append("</TD>");

                    // Task Owner
                    TaskDetails.Append("<TD>");
                    TaskDetails.Append(Helper.GetDisplayTaskUserName(oTaskHdrInfo.AssignedTo));
                    TaskDetails.Append("</TD>");

                    // Task Reviewer
                    TaskDetails.Append("<TD>");
                    TaskDetails.Append(Helper.GetDisplayTaskUserName(oTaskHdrInfo.Reviewer));
                    TaskDetails.Append("</TD>");

                    // Task Approver
                    TaskDetails.Append("<TD>");
                    TaskDetails.Append(Helper.GetDisplayTaskUserName(oTaskHdrInfo.Approver));
                    TaskDetails.Append("</TD>");


                    TaskDetails.Append("</TR>");
                }

                TaskDetails.Append("</TABLE>");
            }
        }
        public static string GetDisplayTaskUserName(List<UserHdrInfo> oUserHdrInfoList)
        {
            string ReturnVal = null;
            if (oUserHdrInfoList != null && oUserHdrInfoList.Count > 0)
            {
                foreach (var oUserHdrInfo in oUserHdrInfoList)
                {
                    if (string.IsNullOrEmpty(ReturnVal))
                        ReturnVal = oUserHdrInfo.Name;
                    else
                        ReturnVal = ReturnVal + "," + oUserHdrInfo.Name;
                }
            }
            return ReturnVal;
        }
    }
}
