using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data.Sql;
using System.Data;
using SkyStem.ART.Service.DAO;
using SkyStem.ART.Service.Data;
using SkyStem.ART.Service.Utility;
using System.Data.OleDb;
using System.Xml.Serialization;
using System.IO;
using System.Xml;
using SkyStem.ART.Service.Model;
using SkyStem.ART.Client.Model.CompanyDatabase;

namespace SkyStem.ART.Service.APP.BLL
{
    public class AccountDataImport
    {
        #region "Private Attributes"
        //private static int companyID;
        //private static int dataImportID;
        //private static int recPeriod;
        //private static short dataImportTypeID;
        //private static string physicalPath = "";
        //private static DateTime PeriodEndDate;
        //private static string AddedBy = "";
        //private static string keyFields = "";
        //private static short dataImportStatusID;
        //private static bool isForceCommit;
        //private static string notifySuccessEmailIds = "";
        //private static string notifySuccessUserEmailIds = "";
        //private static string notifyFailureEmailIds = "";
        //private static string notifyFailureUserEmailIds = "";
        //private static string errorMessageFromSqlServer = "";
        //private static string errorMessageToSave = "";
        //private static string dataImportStatus = "";
        //private static int recordsImported = 0;
        //private static string successEmailIds = "";
        //private static string failureEmailIds = "";
        //private static string warningEmailIds = "";
        //private static DateTime? dateAdded;      
        //private static string profileName;
        //private static short? warningReasonID;
        //private static int languageID;
        //private static int defaultLanguageID;
        //private static bool isAlertRaised;
        //private static string overridenAcctEmailID;
        //private static List<UserAccountInfo> oUserAccountInfoCollection;
        private AccountDataImportInfo oAccountDataImportInfo;
        private CompanyUserInfo CompanyUserInfo;

        #endregion

        public AccountDataImport(CompanyUserInfo oCompanyUserInfo)
        {
            this.CompanyUserInfo = oCompanyUserInfo;
        }
        #region "Public Methods"
        public bool IsProcessingRequiredForAccountDataImport()
        {

            bool isProcessingRequired = false;
            try
            {
                oAccountDataImportInfo = DataImportHelper.GetAccountDataImportInfoForProcessing(DateTime.Now, this.CompanyUserInfo);
                if (oAccountDataImportInfo.DataImportID > 0)
                {
                    isProcessingRequired = true;
                    Helper.LogInfo(@"Account DataImport required for DataImportID: " + oAccountDataImportInfo.DataImportID.ToString(), this.CompanyUserInfo);
                }
                else
                {
                    isProcessingRequired = false;
                    Helper.LogInfo(@"No Data Available for Account DataImport.", this.CompanyUserInfo);
                }
            }
            catch (Exception ex)
            {
                this.oAccountDataImportInfo = null;
                isProcessingRequired = false;
                Helper.LogError(@"Error in IsProcessingRequiredForGLDataImport: " + ex.Message, this.CompanyUserInfo);
            }
            return isProcessingRequired;
            #region "Old Code"
            //InitializePrivateAttributes();
            //SqlCommand oCommand = null;
            //SqlConnection oConnection = null;
            //SqlDataReader reader = null;
            //SqlTransaction oTransaction = null;
            //bool isProcessingRequired = false;
            //try
            //{
            //    oCommand = GetIsProcessingRequiredCommand();
            //    oConnection = Helper.CreateConnection();
            //    oConnection.Open();
            //    oTransaction = oConnection.BeginTransaction();
            //    oCommand.Connection = oConnection;
            //    oCommand.Transaction = oTransaction;
            //    reader = oCommand.ExecuteReader();
            //    if (reader.HasRows)
            //    {
            //        PopulatePrivateAttributes(reader);
            //        isProcessingRequired = true;
            //        Helper.LogInfo(@"Account DataImport required for DataImportID: " + dataImportID.ToString());
            //    }
            //    else
            //    {
            //        Helper.LogInfo(@"No Data Available for Account DataImport.");
            //        isProcessingRequired = false;
            //    }
            //    reader.Close();
            //    oTransaction.Commit();
            //    oTransaction.Dispose();
            //    oTransaction = null;
            //    return isProcessingRequired;
            //}
            //catch (Exception ex)
            //{
            //    if (reader != null && !reader.IsClosed)
            //        reader.Close();
            //    if (oTransaction != null)
            //        oTransaction.Rollback();
            //    throw ex;
            //}
            //finally
            //{
            //    if (reader != null && !reader.IsClosed)
            //        reader.Close();
            //    if (oConnection != null && oConnection.State != ConnectionState.Closed)
            //        oConnection.Close();
            //}
            #endregion
        }

        public void ProcessAccountDataImport()
        {
            try
            {
                if (oAccountDataImportInfo.IsDataTransfered)
                {
                    ProcessImportedAccountData();
                }
                else
                {
                    ExtractTransferAndProcessData();
                }
            }

            catch (Exception ex)
            {
                DataImportHelper.ResetAccountDataHdrObject(oAccountDataImportInfo, ex);
                Helper.LogError(ex, this.CompanyUserInfo);
            }
            finally
            {
                try
                {
                    DataImportHelper.UpdateDataImportHDR(oAccountDataImportInfo, this.CompanyUserInfo);
                }
                catch (Exception ex)
                {
                    Helper.LogError("Error while updating DataImportHDR - ", this.CompanyUserInfo);
                    Helper.LogError(ex, this.CompanyUserInfo);
                }
                try
                {
                    oAccountDataImportInfo.SuccessEmailIDs = DataImportHelper.GetEmailIDWithSeprator(oAccountDataImportInfo.NotifySuccessEmailIds) + DataImportHelper.GetEmailIDWithSeprator( oAccountDataImportInfo.NotifySuccessUserEmailIds) + DataImportHelper.GetEmailIDWithSeprator(oAccountDataImportInfo.WarningEmailIds) ;
                    oAccountDataImportInfo.FailureEmailIDs = DataImportHelper.GetEmailIDWithSeprator(oAccountDataImportInfo.NotifyFailureEmailIds) + DataImportHelper.GetEmailIDWithSeprator( oAccountDataImportInfo.NotifyFailureUserEmailIds) + DataImportHelper.GetEmailIDWithSeprator(oAccountDataImportInfo.WarningEmailIds) ;
                    DataImportHelper.SendMailToUsers(oAccountDataImportInfo, this.CompanyUserInfo);
                }
                catch (Exception ex)
                {
                    Helper.LogError("Error while sending mail - ", this.CompanyUserInfo);
                    Helper.LogError(ex, this.CompanyUserInfo);
                }
            }

            #region "Old Code"
            //SqlConnection oConnection = null;
            //SqlTransaction oTransaction = null;
            //try
            //{
            //    oConnection = Helper.CreateConnection();
            //    oConnection.Open();
            //    oTransaction = oConnection.BeginTransaction();         
            //    if (isForceCommit)
            //        ProcessImportedAccountData(oConnection, oTransaction);
            //    else
            //        GetDatafromExcel(oConnection, oTransaction);
            //    oTransaction.Commit();
            //    oTransaction.Dispose();
            //    oTransaction = null;
            //    if (isAlertRaised)
            //        Alert.GetUserListAndSendMail(dataImportID, companyID);
            //    Helper.LogInfo(" Account Data Imported to sql server successfully");
            //}
            //catch (Exception ex)
            //{

            //    if (oTransaction != null)
            //    {
            //        oTransaction.Rollback();
            //        if (dataImportStatus == DataImportStatus.DATAIMPORTSEVEREWARNING)
            //        {
            //            if (string.IsNullOrEmpty(warningEmailIds))
            //                warningEmailIds = overridenAcctEmailID;
            //            else
            //                warningEmailIds = warningEmailIds + ';' + overridenAcctEmailID;

            //            dataImportStatus = DataImportStatus.DATAIMPORTWARNING;
            //        }
            //        else
            //        {
            //            if (dataImportStatus == "")
            //                dataImportStatus = DataImportStatus.DATAIMPORTFAIL;
            //            if (errorMessageToSave == "")
            //                errorMessageToSave = ex.Message;
            //        }
            //    }
            //    Helper.LogError(ex);
            //}
            //finally
            //{

            //    Helper.UpdateDataImportHDR(oConnection, dataImportID, errorMessageToSave, dataImportStatus, recordsImported, warningReasonID);
            //    if (oConnection != null && oConnection.State != ConnectionState.Closed)
            //        oConnection.Dispose();
            //    try
            //    {
            //        //MailHelper.SendEmailToUserByDataImportStatusMultiVersion(dataImportStatus, successEmailIds
            //        //, failureEmailIds, warningEmailIds, dataImportTypeID, recordsImported, profileName, errorMessageToSave
            //        //, ServiceConstants.DEFAULTBUSINESSENTITYID, languageID, ServiceConstants.DEFAULTLANGUAGEID
            //        //, dateAdded, oUserAccountInfoCollection);

            //     MailHelper.SendEmailToUserByDataImportStatus(dataImportStatus, successEmailIds
            //        , failureEmailIds, warningEmailIds, dataImportTypeID, recordsImported, profileName
            //        , errorMessageToSave, ServiceConstants.DEFAULTBUSINESSENTITYID, languageID, ServiceConstants.DEFAULTLANGUAGEID
            //        , dateAdded);
            //    }
            //    catch (Exception ex)
            //    {
            //        Helper.LogError("Error:: Could not send mail: " + ex.Message);
            //    }
            //}
            #endregion
        }

        #endregion

        #region "Private Methods"

        //private static void InitializePrivateAttributes()
        //{
        //    companyID = -1;
        //    dataImportID = 0;
        //    recPeriod = -1;
        //    dataImportTypeID = -1;
        //    physicalPath = "";
        //    PeriodEndDate = DateTime.MinValue;
        //    AddedBy = "";
        //    keyFields = "";
        //    dataImportStatusID = -1;
        //    isForceCommit = false;
        //    notifySuccessEmailIds = "";
        //    notifySuccessUserEmailIds = "";
        //    notifyFailureEmailIds = "";
        //    notifyFailureUserEmailIds = "";
        //    errorMessageFromSqlServer = "";
        //    errorMessageToSave = "";
        //    dataImportStatus = "";
        //    recordsImported = 0;
        //    successEmailIds = "";
        //    failureEmailIds = "";
        //    warningEmailIds = "";
        //    dateAdded = DateTime.Now;         
        //    profileName = "";
        //    warningReasonID = null;
        //    languageID = -1;
        //    defaultLanguageID = 1033;
        //    overridenAcctEmailID = "";
        //    isAlertRaised = false;
        //    overridenAcctEmailID = "";

        //}


        //private static void PopulatePrivateAttributes(SqlDataReader reader)
        //{
        //    if (reader.HasRows)
        //    {
        //        reader.Read();
        //        Helper.LogInfo("1.Reading Data");
        //        companyID = (int)reader["CompanyID"];
        //        dataImportID = (int)reader["DataImportID"];
        //        recPeriod = (int)reader["ReconciliationPeriodID"];
        //        dataImportTypeID = (short)reader["DataImportTypeID"];
        //        physicalPath = reader["PhysicalPath"].ToString();
        //        PeriodEndDate = (DateTime)reader["PeriodEndDate"];
        //        keyFields = reader["KeyFields"].ToString();
        //        AddedBy = reader["AddedBy"].ToString();
        //        dataImportStatusID = (short)reader["DataImportStatusID"];
        //        isForceCommit = (bool)reader["IsForceCommit"];
        //        notifySuccessEmailIds = reader["NotifySuccessEmailIDs"].ToString();
        //        notifySuccessUserEmailIds = reader["NotifySuccessUserEmailIDs"].ToString();
        //        notifyFailureEmailIds = reader["notifyFailureEmailIds"].ToString();
        //        notifyFailureUserEmailIds = reader["notifyFailureUserEmailIds"].ToString();
        //        successEmailIds = notifySuccessEmailIds + notifySuccessUserEmailIds;
        //        failureEmailIds = notifyFailureEmailIds + notifyFailureUserEmailIds;
        //        warningEmailIds = reader["AddedByUserEmailID"].ToString();
        //        dateAdded = Convert.ToDateTime(reader["DateAdded"]);
        //        profileName = reader["DataImportName"].ToString();

        //        short warningReason;
        //        if (short.TryParse(reader["WarningReasonID"].ToString(), out warningReason))
        //            warningReasonID = warningReason;

        //        int langID;
        //        if (int.TryParse(reader["LanguageID"].ToString(), out langID))
        //            languageID = langID;
        //        defaultLanguageID = ServiceConstants.DEFAULTLANGUAGEID;
        //        Helper.LogInfo("2. DataImport Information retrived for Company" + companyID.ToString());
        //    }
        //}

        private void ProcessImportedAccountData()
        {
            DataImportHelper.ProcessTransferedAccountData(oAccountDataImportInfo, this.CompanyUserInfo);
        }

        private void ExtractTransferAndProcessData()
        {
            OleDbConnection oConnectionExcel = null;
            DataTable dtExcelData = null;
            DataTable dtAccountDataSchema = null;
            string allFieldName = "";

            Helper.LogInfo("3. Start Reading Excel file: " + oAccountDataImportInfo.PhysicalPath, this.CompanyUserInfo);
            try
            {
                oConnectionExcel = Helper.GetExcelFileConnection(oAccountDataImportInfo.PhysicalPath);
                oConnectionExcel.Open();
                // Get Excel File Schema - gets the list of Columns names as per Excel
                dtAccountDataSchema = Helper.GetExcelFileSchema(oConnectionExcel, ServiceConstants.ACCOUNTDATA_SHEETNAME, this.CompanyUserInfo);
                // Loop and create a , separated list of columns available in Excel
                allFieldName = Helper.GetColumnNames(dtAccountDataSchema);
                // read the data based on above Column List
                dtExcelData = Helper.GetDataTableFromExcel(oConnectionExcel, allFieldName, ServiceConstants.ACCOUNTDATA_SHEETNAME, this.CompanyUserInfo );

                if (ValidateSchemaForAccountData(dtExcelData, out allFieldName))
                {
                    oConnectionExcel.Close();
                    oConnectionExcel.Dispose();
                    dtExcelData.Rows.RemoveAt(0); // Remove Hdr Row
                    Helper.LogInfo("4. Reading Excel file complete.", this.CompanyUserInfo);
                    AddDataImportIDToDataTable(dtExcelData);
                    ValidateDataLength(dtExcelData);
                    //CopyGLDataToSqlServer(dtExcelData, oConnection, oTransaction);
                    DataImportHelper.TransferAndProcessAccountData(dtExcelData, oAccountDataImportInfo, this.CompanyUserInfo);
                }
            }
            finally
            {
                if (oConnectionExcel != null && oConnectionExcel.State != ConnectionState.Closed)
                    oConnectionExcel.Dispose();
            }
        }

        //private static void GetDatafromExcel(SqlConnection oConnection, SqlTransaction oTransaction)
        //{
        //    OleDbConnection oConnectionExcel = null;
        //    DataTable dtExcelData = null;
        //    DataTable dtAccountDataSchema = null;
        //    string allFieldName = "";

        //    Helper.LogInfo("3. Start Reading Excel file: " + physicalPath);
        //    try
        //    {
        //        oConnectionExcel = Helper.GetExcelFileConnection(physicalPath);
        //        oConnectionExcel.Open();
        //        // Get Excel File Schema - gets the list of Columns names as per Excel
        //        dtAccountDataSchema = Helper.GetExcelFileSchema(oConnectionExcel, ServiceConstants.ACCOUNTDATA_SHEETNAME);
        //        // Loop and create a , separated list of columns available in Excel
        //        allFieldName = Helper.GetColumnNames(dtAccountDataSchema);
        //        // read the data based on above Column List
        //        dtExcelData = Helper.GetDataTableFromExcel(oConnectionExcel, allFieldName, ServiceConstants.ACCOUNTDATA_SHEETNAME);

        //        if (ValidateSchemaForAccountData(dtExcelData, out allFieldName))
        //        {
        //            oConnectionExcel.Close();
        //            oConnectionExcel.Dispose();
        //            dtExcelData.Rows.RemoveAt(0); // Remove Hdr Row
        //            Helper.LogInfo("4. Reading Excel file complete.");
        //            AddDataImportIDToDataTable(dtExcelData);
        //            ValidateDataLength(dtExcelData);
        //            CopyGLDataToSqlServer(dtExcelData, oConnection, oTransaction);
        //        }
        //    }
        //    finally
        //    {
        //        if (oConnectionExcel != null && oConnectionExcel.State != ConnectionState.Closed)
        //            oConnectionExcel.Dispose();
        //    }

        //}

        private bool ValidateSchemaForAccountData(DataTable dtSchema, out string ListOfFields)
        {
            ListOfFields = "";
            string[] arryMandatoryFields = Helper.GetAccountDataImportMandatoryFields();
            string[] arrKeyFields = this.oAccountDataImportInfo.KeyFields.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            string[] arrAllFields = new string[arryMandatoryFields.Length + arrKeyFields.Length];
            arryMandatoryFields.CopyTo(arrAllFields, 0);
            arrKeyFields.CopyTo(arrAllFields, arryMandatoryFields.Length);
            string columnName = "";
            int? columnIndex = null;
            string fieldName = null;
            bool columnFound = false;
            for (int i = 0; i < arrAllFields.Length; i++)
            {
                columnIndex = null;
                fieldName = arrAllFields[i].ToString().Trim();
                columnFound = false;

                for (int j = 0; j < dtSchema.Columns.Count; j++)
                {
                    columnName = dtSchema.Rows[0][j].ToString().Trim();
                    if (columnName == fieldName)
                    {
                        columnFound = true;
                        if (ListOfFields == "")
                            ListOfFields = ListOfFields + "[" + columnName + "]" + " AS [" + fieldName + "]";
                        else
                            ListOfFields = ListOfFields + " , " + "[" + columnName + "]" + " AS [" + fieldName + "]";

                        columnIndex = j;
                        break;
                    }
                }

                if (columnIndex != null)
                {
                    dtSchema.Columns[columnIndex.Value].ColumnName = fieldName;
                }
                if (!columnFound)
                {
                    throw new Exception("Mandatory columns not present: " + fieldName);
                }
            }
            return true;
        }

        private void AddDataImportIDToDataTable(DataTable dtExcelData)
        {

            DataColumn dl = new DataColumn(AddedGLDataImportFields.DATAIMPORTID, typeof(System.Int32));
            DataColumn dlRowNumber = new DataColumn(AddedGLDataImportFields.EXCELROWNUMBER, typeof(System.Int32));
            //dtExcelData.Columns.Add(AddedGLDataImportFields.RECPERIODENDDATE, typeof(System.String));
            dtExcelData.Columns.Add(dl);
            dtExcelData.Columns.Add(dlRowNumber);
            DateTime dtPeriodEndDate = new DateTime();
            for (int x = 0; x < dtExcelData.Rows.Count; x++)
            {
                dtExcelData.Rows[x][AddedGLDataImportFields.DATAIMPORTID] = this.oAccountDataImportInfo.DataImportID;
                dtExcelData.Rows[x][AddedGLDataImportFields.EXCELROWNUMBER] = x + 2;
                //if (DateTime.TryParse(dtExcelData.Rows[x][GLDataImportFields.PERIODENDDATE].ToString(), out dtPeriodEndDate))
                //    dtExcelData.Rows[x][AddedGLDataImportFields.RECPERIODENDDATE] = dtPeriodEndDate.ToShortDateString();

            }

        }

        private void ValidateDataLength(DataTable dtExcelData)
        {
            StringBuilder oSBError = new StringBuilder();
            string msg = Helper.GetDataLengthErrorMessage(ServiceConstants.DEFAULTBUSINESSENTITYID, this.oAccountDataImportInfo.LanguageID, this.oAccountDataImportInfo.DefaultLanguageID, this.CompanyUserInfo);
            string invalidDataMsg = Helper.GetInvalidDataErrorMessage(ServiceConstants.DEFAULTBUSINESSENTITYID, this.oAccountDataImportInfo.LanguageID, this.oAccountDataImportInfo.DefaultLanguageID, this.CompanyUserInfo);

            for (int x = 0; x < dtExcelData.Rows.Count; x++)
            {
                DataRow dr = dtExcelData.Rows[x];
                string excelRowNumber = dr[AddedGLDataImportFields.EXCELROWNUMBER].ToString();
                if (dr[AccountDataImportFields.FSCAPTION] != DBNull.Value)
                    dr[AccountDataImportFields.FSCAPTION] = dr[AccountDataImportFields.FSCAPTION].ToString().Trim();
                if (dr[AccountDataImportFields.FSCAPTION].ToString().Length > (int)Enums.DataImportFieldsMaxLength.FSCaption)
                {
                    oSBError.Append(String.Format(msg, AccountDataImportFields.FSCAPTION, excelRowNumber, ((int)Enums.DataImportFieldsMaxLength.FSCaption).ToString()));
                    oSBError.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
                }

                if (dr[AccountDataImportFields.GLACCOUNTNAME] != DBNull.Value)
                    dr[AccountDataImportFields.GLACCOUNTNAME] = dr[AccountDataImportFields.GLACCOUNTNAME].ToString().Trim();
                if (dr[AccountDataImportFields.GLACCOUNTNAME].ToString().Length > (int)Enums.DataImportFieldsMaxLength.AccountName)
                {
                    oSBError.Append(String.Format(msg, AccountDataImportFields.GLACCOUNTNAME, excelRowNumber, ((int)Enums.DataImportFieldsMaxLength.AccountName).ToString()));
                    oSBError.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
                }

                if (dr[AccountDataImportFields.GLACCOUNTNUMBER] != DBNull.Value)
                    dr[AccountDataImportFields.GLACCOUNTNUMBER] = dr[AccountDataImportFields.GLACCOUNTNUMBER].ToString().Trim();
                if (dr[AccountDataImportFields.GLACCOUNTNUMBER].ToString().Length > (int)Enums.DataImportFieldsMaxLength.AccountNumber)
                {
                    oSBError.Append(String.Format(msg, AccountDataImportFields.GLACCOUNTNUMBER, excelRowNumber, ((int)Enums.DataImportFieldsMaxLength.AccountNumber).ToString()));
                    oSBError.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
                }

                if (dr[AccountDataImportFields.ISPROFITANDLOSS] != DBNull.Value)
                    dr[AccountDataImportFields.ISPROFITANDLOSS] = dr[AccountDataImportFields.ISPROFITANDLOSS].ToString().Trim();
                if (dr[AccountDataImportFields.ISPROFITANDLOSS].ToString().Length > (int)Enums.DataImportFieldsMaxLength.IsProfitAndLoss)
                {
                    oSBError.Append(String.Format(msg, AccountDataImportFields.ISPROFITANDLOSS, excelRowNumber, ((int)Enums.DataImportFieldsMaxLength.IsProfitAndLoss).ToString()));
                    oSBError.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
                }
                //Keyfields
                string[] arrKeyFields = this.oAccountDataImportInfo.KeyFields.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                for (int k = 0; k < arrKeyFields.Length; k++)
                {
                    string sourceField = arrKeyFields[k].ToString();
                    if (dtExcelData.Columns.Contains(sourceField))
                    {
                        if (dr[sourceField] != DBNull.Value)
                            dr[sourceField] = dr[sourceField].ToString().Trim();
                        if (dr[sourceField].ToString().Length > (int)Enums.DataImportFieldsMaxLength.KeyFields)
                        {
                            oSBError.Append(String.Format(msg, sourceField, excelRowNumber, ((int)Enums.DataImportFieldsMaxLength.KeyFields).ToString()));
                            oSBError.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
                        }
                    }

                }

            }
            if (!oSBError.ToString().Equals(String.Empty))
                throw new Exception(oSBError.ToString());
        }

        //private static void CopyGLDataToSqlServer(DataTable dtExcelData, SqlConnection oConnection, SqlTransaction oTransaction)
        //{
        //    DateTime dateAdded = DateTime.Now.Date;
        //    if (oConnection.State != ConnectionState.Open)
        //        oConnection.Open();
        //    using (SqlBulkCopy oSqlBlkCopy = new SqlBulkCopy(oConnection, SqlBulkCopyOptions.Default, oTransaction))
        //    {
        //        Helper.LogInfo("5. Mapping fields from source to destination.");
        //        oSqlBlkCopy.DestinationTableName = "GLDataTransit";
        //        oSqlBlkCopy.ColumnMappings.Add(AddedGLDataImportFields.DATAIMPORTID, GLDataImportTransitFields.DATAIMPORTID);
        //        oSqlBlkCopy.ColumnMappings.Add(AddedGLDataImportFields.EXCELROWNUMBER, GLDataImportTransitFields.EXCELROWNUMBER);
        //        //oSqlBlkCopy.ColumnMappings.Add(AddedGLDataImportFields.RECPERIODENDDATE, GLDataImportTransitFields.PERIODENDDATE);
        //        oSqlBlkCopy.ColumnMappings.Add(GLDataImportFields.COMPANY, GLDataImportTransitFields.COMPANY);
        //        oSqlBlkCopy.ColumnMappings.Add(GLDataImportFields.GLACCOUNTNUMBER, GLDataImportTransitFields.GLACCOUNTNUMBER);
        //        oSqlBlkCopy.ColumnMappings.Add(GLDataImportFields.GLACCOUNTNAME, GLDataImportTransitFields.GLACCOUNTNAME);
        //        oSqlBlkCopy.ColumnMappings.Add(GLDataImportFields.FSCAPTION, GLDataImportTransitFields.FSCAPTION);
        //        oSqlBlkCopy.ColumnMappings.Add(GLDataImportFields.ACCOUNTTYPE, GLDataImportTransitFields.ACCOUNTTYPE);
        //        oSqlBlkCopy.ColumnMappings.Add(GLDataImportFields.ISPROFITANDLOSS, GLDataImportTransitFields.ISPROFITANDLOSS);
        //        string[] arrKeyFields = keyFields.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
        //        for (int k = 0; k < arrKeyFields.Length; k++)
        //        {
        //            string sourceField = arrKeyFields[k].ToString();
        //            string targetField = "Key" + (k + 2).ToString();
        //            oSqlBlkCopy.ColumnMappings.Add(sourceField, targetField);
        //        }

        //        Helper.LogInfo("6. Transfering data to sql server destination table.");

        //        oSqlBlkCopy.WriteToServer(dtExcelData);
        //        Helper.LogInfo("7. Processing Transfered data in sql server.");
        //        ProcessImportedAccountData(oConnection, oTransaction);

        //        Helper.LogInfo("7. Data Transfer complete");
        //    }
        //}
        //private static void ProcessImportedAccountData(SqlConnection oConnection, SqlTransaction oTransaction)
        //{

        //    string xmlReturnString;
        //    XmlSerializer xmlSerial = null;
        //    StringReader strReader = null;
        //    XmlTextReader txtReader = null;
        //    ReturnValue oRetVal = null;
        //    SqlCommand oCommand = null;
        //    SqlDataReader oDataReader = null;
        //    try
        //    {

        //        oCommand = GetAccountDataProcessingCommand();
        //        if (oConnection.State != ConnectionState.Open)
        //            oConnection.Open();
        //        oCommand.Connection = oConnection;
        //        oCommand.Transaction = oTransaction;
        //        oCommand.ExecuteNonQuery();
        //        xmlReturnString = oCommand.Parameters["@ReturnValue"].Value.ToString();

        //        xmlSerial = new XmlSerializer(typeof(ReturnValue));
        //        strReader = new StringReader(xmlReturnString);
        //        txtReader = new XmlTextReader(strReader);
        //        oRetVal = (ReturnValue)xmlSerial.Deserialize(txtReader);
        //        isAlertRaised = false;

        //        if (oRetVal != null)
        //        {
        //            errorMessageFromSqlServer = oRetVal.ErrorMessageForServiceToLog;
        //            errorMessageToSave = oRetVal.ErrorMessageToSave;
        //            dataImportStatus = oRetVal.ImportStatus;

        //            if (oRetVal.IsAlertRaised.HasValue)
        //            {
        //                isAlertRaised = oRetVal.IsAlertRaised.Value;
        //                //if (isAlertRaised)
        //                //    Alert.GetUserListAndSendMail(dataImportID, companyID);
        //            } 
        //            if (oRetVal.RecordsImported.HasValue)
        //            {
        //                recordsImported = oRetVal.RecordsImported.Value;
        //            }
        //            else
        //            {
        //                recordsImported = 0;
        //            }
        //            if (oRetVal.WarningReasonID.HasValue)
        //            {
        //                warningReasonID = oRetVal.WarningReasonID.Value;
        //            }
        //        }

        //    }
        //    finally
        //    {
        //        if (txtReader != null)
        //            txtReader.Close();
        //        if (strReader != null)
        //            strReader.Close();
        //    }
        //    Helper.LogInfo("8. Data Processing Complete.");
        //    Helper.LogInfo(" - Status: " + dataImportStatus);
        //    Helper.LogInfo(" - Message: " + errorMessageFromSqlServer);
        //    Helper.LogInfo(" - Records Imported: " + recordsImported.ToString());

        //    if (dataImportStatus == DataImportStatus.DATAIMPORTFAIL)
        //        throw new Exception(errorMessageToSave);

        //}

        #endregion

        //#region "Command Methods"
        //private static SqlCommand GetIsProcessingRequiredCommand()
        //{
        //    SqlCommand oCommand = Helper.CreateCommand();
        //    oCommand.CommandType = CommandType.StoredProcedure;
        //    oCommand.CommandText = "usp_GET_AccountDataImportForProcessing";
        //    SqlParameterCollection cmdParamCollection = oCommand.Parameters;
        //    SqlParameter paramDataImportTypeId = new SqlParameter("@dataImportTypeId", Enums.DataImportType.AccountDataImport);
        //    SqlParameter paramDataImportStatusId = new SqlParameter("@dataImportStatusID", Enums.DataImportStatus.ToBeProcessed);
        //    SqlParameter paramProcessingDataImportStatusId = new SqlParameter("@processingDataImportStatusID", Enums.DataImportStatus.Processing);
        //    SqlParameter paramWarningDataImportStatusId = new SqlParameter("@warningDataImportStatusID", Enums.DataImportStatus.Warning);
        //    SqlParameter paramDateRevised = new SqlParameter("@dateRevised", Convert.ToDateTime(Helper.GetDateTime()));
        //    SqlParameter paramErrorMessageSRV = new SqlParameter("@errorMessageForServiceToLog", SqlDbType.VarChar, 8000);
        //    paramErrorMessageSRV.Direction = ParameterDirection.Output;
        //    SqlParameter paramReturnValue = new SqlParameter("@returnValue", SqlDbType.Int);
        //    paramReturnValue.Direction = ParameterDirection.ReturnValue;
        //    cmdParamCollection.Add(paramDataImportTypeId);
        //    cmdParamCollection.Add(paramDataImportStatusId);
        //    cmdParamCollection.Add(paramProcessingDataImportStatusId);
        //    cmdParamCollection.Add(paramWarningDataImportStatusId);
        //    cmdParamCollection.Add(paramDateRevised);
        //    cmdParamCollection.Add(paramErrorMessageSRV);
        //    cmdParamCollection.Add(paramReturnValue);
        //    return oCommand;
        //}

        //private static SqlCommand GetAccountDataProcessingCommand()
        //{
        //    SqlCommand oCommand = Helper.CreateCommand();
        //    oCommand.CommandType = CommandType.StoredProcedure;
        //    oCommand.CommandText = "usp_SVC_INS_ProcessGLDataTransitForAccountDataImport";
        //    SqlParameterCollection cmdParamCollectionImport = oCommand.Parameters;
        //    SqlParameter paramCompanyID = new SqlParameter("@companyID", companyID);
        //    SqlParameter paramDataImportID = new SqlParameter("@dataImportID", dataImportID);
        //    SqlParameter paramAddedBy = new SqlParameter("@addedBy", AddedBy);
        //    SqlParameter paramDateAdded = new SqlParameter("@dateAdded", dateAdded);
        //    SqlParameter paramReturnValue = new SqlParameter("@ReturnValue", SqlDbType.NVarChar, -1);
        //    paramReturnValue.Direction = ParameterDirection.Output;
        //    cmdParamCollectionImport.Add(paramCompanyID);
        //    cmdParamCollectionImport.Add(paramDataImportID);
        //    cmdParamCollectionImport.Add(paramAddedBy);
        //    cmdParamCollectionImport.Add(paramDateAdded);        
        //    cmdParamCollectionImport.Add(paramReturnValue);
        //    return oCommand;
        //}
        //#endregion
    }
}
