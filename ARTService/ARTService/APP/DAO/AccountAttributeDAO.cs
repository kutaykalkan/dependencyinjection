using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using SkyStem.ART.Service.Data;
using SkyStem.ART.Service.Utility;
using SkyStem.ART.Service.Model;
using SkyStem.ART.Client.Model.CompanyDatabase;

namespace SkyStem.ART.Service.APP.DAO
{
    public class AccountAttributeDAO : DataImportHdrDAO
    {
        public AccountAttributeDAO(CompanyUserInfo oCompanyUserInfo)
            : base(oCompanyUserInfo)
        {

        }

        public AccountAttributeDataImportInfo GetAccountAttributeDataImportForProcessing(DateTime dateRevised)
        {
            AccountAttributeDataImportInfo oEntity = new AccountAttributeDataImportInfo();
            if (this.IsDataImportProcessingRequired(Enums.DataImportType.AccountAttributeList))
            {
                this.GetDataImportForAttributeProcessing(oEntity, Enums.DataImportType.AccountAttributeList, dateRevised);
            }
            return oEntity;
        }

        private void GetDataImportForAttributeProcessing(AccountAttributeDataImportInfo oEntity, Enums.DataImportType dataImportType, DateTime dateRevised)
        {
            SqlConnection oConn = null;
            SqlTransaction oTrans = null;
            SqlCommand oCmd = null;
            SqlDataReader reader = null;
            try
            {
                oConn = this.GetConnection();
                oCmd = this.GetAccountAttributeDataImportProcessingCommand(dataImportType, dateRevised);
                oCmd.Connection = oConn;
                oCmd.Connection.Open();
                oTrans = oConn.BeginTransaction();
                oCmd.Transaction = oTrans;
                reader = oCmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    this.MapObject(reader, oEntity);
                }

                if (reader != null && !reader.IsClosed)
                    reader.Close();

                oTrans.Commit();
                oTrans.Dispose();
                oTrans = null;

                string errorDescrp = oCmd.Parameters["@errorMessageForServiceToLog"].Value.ToString();
                int retVal = Convert.ToInt32(oCmd.Parameters["@returnValue"].Value);
                if (retVal < 0)
                    throw new Exception(errorDescrp);

            }
            catch (Exception ex)
            {
                if (oTrans != null)
                    oTrans.Rollback();
                throw ex;
            }
            finally
            {
                if (reader != null && !reader.IsClosed)
                    reader.Close();

                if (oConn != null && oConn.State == ConnectionState.Open)
                    oConn.Close();
            }
        }


        public void CopyAccountAttributeToSqlServer(DataTable dtExcelData, AccountAttributeDataImportInfo oAcctAttrDataImportInfo, SqlConnection oConnection, SqlTransaction oTransaction)
        {
            DateTime dateAdded = DateTime.Now.Date;
            using (SqlBulkCopy oSqlBlkCopy = new SqlBulkCopy(oConnection, SqlBulkCopyOptions.Default, oTransaction))
            {
                Helper.LogInfoToCache("5. Mapping fields from source to destination.", this.LogInfoCache);
                oSqlBlkCopy.DestinationTableName = "AccountAttributeTransit";

                if (dtExcelData.Columns.Contains(AddedAccountAttributeImportFields.DATAIMPORTID))
                    oSqlBlkCopy.ColumnMappings.Add(AddedAccountAttributeImportFields.DATAIMPORTID, AccountAttributeDataImportTransitFields.DATAIMPORTID);

                if (dtExcelData.Columns.Contains(AddedAccountAttributeImportFields.EXCELROWNUMBER))
                    oSqlBlkCopy.ColumnMappings.Add(AddedAccountAttributeImportFields.EXCELROWNUMBER, AccountAttributeDataImportTransitFields.EXCELROWNUMBER);

                if (dtExcelData.Columns.Contains(AccountAttributeDataImportFields.COMPANY))
                    oSqlBlkCopy.ColumnMappings.Add(AccountAttributeDataImportFields.COMPANY, AccountAttributeDataImportTransitFields.COMPANY);

                if (dtExcelData.Columns.Contains(AccountAttributeDataImportFields.GLACCOUNTNUMBER))
                    oSqlBlkCopy.ColumnMappings.Add(AccountAttributeDataImportFields.GLACCOUNTNUMBER, AccountAttributeDataImportTransitFields.GLACCOUNTNUMBER);

                if (dtExcelData.Columns.Contains(AccountAttributeDataImportFields.GLACCOUNTNAME))
                    oSqlBlkCopy.ColumnMappings.Add(AccountAttributeDataImportFields.GLACCOUNTNAME, AccountAttributeDataImportTransitFields.GLACCOUNTNAME);

                if (dtExcelData.Columns.Contains(AccountAttributeDataImportFields.FSCAPTION))
                    oSqlBlkCopy.ColumnMappings.Add(AccountAttributeDataImportFields.FSCAPTION, AccountAttributeDataImportTransitFields.FSCAPTION);

                if (dtExcelData.Columns.Contains(AccountAttributeDataImportFields.ACCOUNTTYPE))
                    oSqlBlkCopy.ColumnMappings.Add(AccountAttributeDataImportFields.ACCOUNTTYPE, AccountAttributeDataImportTransitFields.ACCOUNTTYPE);

                if (!String.IsNullOrEmpty(oAcctAttrDataImportInfo.KeyFields))
                {
                    string[] arrKeyFields = oAcctAttrDataImportInfo.KeyFields.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    for (int k = 0; k < arrKeyFields.Length; k++)
                    {
                        string sourceField = arrKeyFields[k].ToString();
                        string targetField = "Key" + (k + 2).ToString();
                        if (dtExcelData.Columns.Contains(sourceField))
                            oSqlBlkCopy.ColumnMappings.Add(sourceField, targetField);
                    }
                }

                if (oAcctAttrDataImportInfo.IsRiskRatingFieldAvailable)
                    oSqlBlkCopy.ColumnMappings.Add(AccountAttributeDataImportFields.RISKRATING, AccountAttributeDataImportTransitFields.RISKRATING);

                if (oAcctAttrDataImportInfo.IsRecTemplateFieldAvailable)
                    oSqlBlkCopy.ColumnMappings.Add(AccountAttributeDataImportFields.RECONCILIATIONTEMPLATE, AccountAttributeDataImportTransitFields.RECONCILIATIONTEMPLATE);

                if (oAcctAttrDataImportInfo.IsKeyAccountFieldAvailable)
                    oSqlBlkCopy.ColumnMappings.Add(AccountAttributeDataImportFields.ISKEYACCOUNT, AccountAttributeDataImportTransitFields.ISKEYACCOUNT);

                if (oAcctAttrDataImportInfo.IsZeroBalanceFieldAvailable)
                    oSqlBlkCopy.ColumnMappings.Add(AccountAttributeDataImportFields.ISZEROBALANCEACCOUNT, AccountAttributeDataImportTransitFields.ISZEROBALANCEACCOUNT);

                if (oAcctAttrDataImportInfo.IsSubledgerSourceFieldAvailable)
                    oSqlBlkCopy.ColumnMappings.Add(AccountAttributeDataImportFields.SUBLEDGERSOURCE, AccountAttributeDataImportTransitFields.SUBLEDGERSOURCE);

                if (oAcctAttrDataImportInfo.IsRecPolicyFieldAvailable)
                    oSqlBlkCopy.ColumnMappings.Add(AccountAttributeDataImportFields.RECONCILIATIONPOLICY, AccountAttributeDataImportTransitFields.RECONCILIATIONPOLICY);

                if (oAcctAttrDataImportInfo.IsNatureOfAcctFieldAvailable)
                    oSqlBlkCopy.ColumnMappings.Add(AccountAttributeDataImportFields.NATUREOFACCOUNT, AccountAttributeDataImportTransitFields.NATUREOFACCOUNT);

                if (oAcctAttrDataImportInfo.IsRecProcedureFieldAvailable)
                    oSqlBlkCopy.ColumnMappings.Add(AccountAttributeDataImportFields.RECONCILIATIONPROCEDURE, AccountAttributeDataImportTransitFields.RECONCILIATIONPROCEDURE);

                if (oAcctAttrDataImportInfo.IsPreparerFieldAvailable)
                    oSqlBlkCopy.ColumnMappings.Add(AccountAttributeDataImportFields.PREPARER, AccountAttributeDataImportTransitFields.PREPARER);

                if (oAcctAttrDataImportInfo.IsReviewerFieldAvailable)
                    oSqlBlkCopy.ColumnMappings.Add(AccountAttributeDataImportFields.REVIEWER, AccountAttributeDataImportTransitFields.REVIEWER);

                if (oAcctAttrDataImportInfo.IsApproverFieldAvailable)
                    oSqlBlkCopy.ColumnMappings.Add(AccountAttributeDataImportFields.APPROVER, AccountAttributeDataImportTransitFields.APPROVER);

                if (oAcctAttrDataImportInfo.IsBackupPreparerFieldAvailable)
                    oSqlBlkCopy.ColumnMappings.Add(AccountAttributeDataImportFields.BACKUPPREPARER, AccountAttributeDataImportTransitFields.BACKUPPREPARER);

                if (oAcctAttrDataImportInfo.IsBackupReviewerFieldAvailable)
                    oSqlBlkCopy.ColumnMappings.Add(AccountAttributeDataImportFields.BACKUPREVIEWER, AccountAttributeDataImportTransitFields.BACKUPREVIEWER);

                if (oAcctAttrDataImportInfo.IsBackupApproverFieldAvailable)
                    oSqlBlkCopy.ColumnMappings.Add(AccountAttributeDataImportFields.BACKUPAPPROVER, AccountAttributeDataImportTransitFields.BACKUPAPPROVER);

                if (oAcctAttrDataImportInfo.IsReconcilableFieldAvailable)
                    oSqlBlkCopy.ColumnMappings.Add(AccountAttributeDataImportFields.RECONCILABLE, AccountAttributeDataImportTransitFields.RECONCILABLE);

                if (oAcctAttrDataImportInfo.IsPreparerDueDaysFieldAvailable)
                    oSqlBlkCopy.ColumnMappings.Add(AccountAttributeDataImportFields.PREPARERDUEDAYS, AccountAttributeDataImportTransitFields.PREPARERDUEDAYS);

                if (oAcctAttrDataImportInfo.IsReviewerDueDaysFieldAvailable)
                    oSqlBlkCopy.ColumnMappings.Add(AccountAttributeDataImportFields.REVIEWERDUEDAYS, AccountAttributeDataImportTransitFields.REVIEWERDUEDAYS);

                if (oAcctAttrDataImportInfo.IsApproverDueDaysFieldAvailable)
                    oSqlBlkCopy.ColumnMappings.Add(AccountAttributeDataImportFields.APPROVERDUEDAYS, AccountAttributeDataImportTransitFields.APPROVERDUEDAYS);

                //if (oAcctAttrDataImportInfo.IsDayTypeFieldAvailable)
                //    oSqlBlkCopy.ColumnMappings.Add(AccountAttributeDataImportFields.DAYTYPE, AccountAttributeDataImportTransitFields.DAYTYPE);

                Helper.LogInfoToCache("6. Transfering data to sql server destination table.", this.LogInfoCache);

                oSqlBlkCopy.WriteToServer(dtExcelData);
                Helper.LogInfoToCache("7. Processing Transfered data in sql server.", this.LogInfoCache);
            }
        }

        public void ProcessImportedAccountAttribute(AccountAttributeDataImportInfo oAcctAttrDataImportInfo, SqlConnection oConnection, SqlTransaction oTransaction)
        {
            SqlCommand oCommand = GetAccountAttributeProcessingCommand(oAcctAttrDataImportInfo);
            oCommand.Connection = oConnection;
            oCommand.Transaction = oTransaction;
            try
            {
                oCommand.ExecuteNonQuery();
            }
            finally
            {
                if (oCommand.Parameters["@errorMessageForServiceToLog"].Value != DBNull.Value)
                    oAcctAttrDataImportInfo.ErrorMessageFromSqlServer = oCommand.Parameters["@errorMessageForServiceToLog"].Value.ToString();
                if (oCommand.Parameters["@errorMessageToSave"].Value != DBNull.Value)
                    oAcctAttrDataImportInfo.ErrorMessageToSave = oCommand.Parameters["@errorMessageToSave"].Value.ToString();
                if (oCommand.Parameters["@importStatus"].Value != DBNull.Value)
                    oAcctAttrDataImportInfo.DataImportStatus = oCommand.Parameters["@importStatus"].Value.ToString();
                if (oCommand.Parameters["@recordsImported"].Value != DBNull.Value)
                {
                    int RecordsImported;
                    Int32.TryParse(oCommand.Parameters["@recordsImported"].Value.ToString(), out RecordsImported);
                    oAcctAttrDataImportInfo.RecordsImported = RecordsImported;
                }
                if (oCommand.Parameters["@retWarningReasonID"].Value != DBNull.Value)
                {
                    short WarningReasonID;
                    Int16.TryParse(oCommand.Parameters["@retWarningReasonID"].Value.ToString(), out WarningReasonID);
                    oAcctAttrDataImportInfo.WarningReasonID = WarningReasonID;
                }
                Helper.LogInfoToCache("8. Data Processing Complete.", this.LogInfoCache);
                Helper.LogInfoToCache(" - Status: " + oAcctAttrDataImportInfo.DataImportStatus, this.LogInfoCache);
                Helper.LogInfoToCache(" - Message: " + oAcctAttrDataImportInfo.ErrorMessageFromSqlServer, this.LogInfoCache);
                Helper.LogInfoToCache(" - Records Imported: " + oAcctAttrDataImportInfo.RecordsImported.ToString(), this.LogInfoCache);
                Helper.LogInfoToCache(" - Warning Reason ID: " + oAcctAttrDataImportInfo.WarningReasonID.ToString(), this.LogInfoCache);

                //Raise exception if dataImportStatus = "FAIL". This exception message will be logged into logfile
                if (oAcctAttrDataImportInfo.DataImportStatus == "FAIL")
                    throw new Exception(oAcctAttrDataImportInfo.ErrorMessageToSave);
            }
        }

        #region "Methods for get command"
        private SqlCommand GetAccountAttributeDataImportProcessingCommand(Enums.DataImportType dataImportType, DateTime dateRevised)
        {
            SqlCommand oCommand = this.CreateCommand();
            oCommand.CommandType = CommandType.StoredProcedure;
            oCommand.CommandText = "usp_GET_GLDataImportForProcessing";

            SqlParameterCollection cmdParamCollection = oCommand.Parameters;

            SqlParameter paramDataImportTypeId = new SqlParameter("@dataImportTypeId", dataImportType);
            SqlParameter paramDataImportStatusId = new SqlParameter("@dataImportStatusID", Enums.DataImportStatus.ToBeProcessed);
            SqlParameter paramProcessingDataImportStatusId = new SqlParameter("@processingDataImportStatusID", Enums.DataImportStatus.Processing);
            SqlParameter paramWarningDataImportStatusId = new SqlParameter("@warningDataImportStatusID", Enums.DataImportStatus.Warning);
            SqlParameter paramDateRevised = new SqlParameter("@dateRevised", Convert.ToDateTime(Helper.GetDateTime()));

            SqlParameter paramErrorMessageSRV = new SqlParameter("@errorMessageForServiceToLog", SqlDbType.VarChar, 8000);
            paramErrorMessageSRV.Direction = ParameterDirection.Output;

            SqlParameter paramReturnValue = new SqlParameter("@returnValue", SqlDbType.Int);
            paramReturnValue.Direction = ParameterDirection.ReturnValue;

            cmdParamCollection.Add(paramDataImportTypeId);
            cmdParamCollection.Add(paramDataImportStatusId);
            cmdParamCollection.Add(paramProcessingDataImportStatusId);
            cmdParamCollection.Add(paramWarningDataImportStatusId);
            cmdParamCollection.Add(paramDateRevised);
            cmdParamCollection.Add(paramErrorMessageSRV);
            cmdParamCollection.Add(paramReturnValue);

            return oCommand;
        }

        private SqlCommand GetAccountAttributeProcessingCommand(AccountAttributeDataImportInfo oAcctAttrDataImportInfo)
        {
            SqlCommand oCommand = this.CreateCommand();
            oCommand.CommandType = CommandType.StoredProcedure;
            oCommand.CommandText = "usp_SRV_INS_ProcessAccountAttributeTransit";
            SqlParameterCollection cmdParamCollectionImport = oCommand.Parameters;

            SqlParameter paramCompanyID = new SqlParameter("@companyID", oAcctAttrDataImportInfo.CompanyID);
            SqlParameter paramDataImportID = new SqlParameter("@dataImportID", oAcctAttrDataImportInfo.DataImportID);
            SqlParameter paramAddedBy = new SqlParameter("@addedBy", oAcctAttrDataImportInfo.AddedBy);
            SqlParameter paramDateAdded = new SqlParameter("@dateAdded", oAcctAttrDataImportInfo.DateAdded);
            SqlParameter paramIsForceCommit = new SqlParameter("@isForceCommit", oAcctAttrDataImportInfo.IsForceCommit);
            short WarningReasonID;
            if (oAcctAttrDataImportInfo.WarningReasonID.HasValue && oAcctAttrDataImportInfo.WarningReasonID.Value > 0)
                WarningReasonID = oAcctAttrDataImportInfo.WarningReasonID.Value;
            else
                WarningReasonID = 0;
            SqlParameter paramWarningReasonID = new SqlParameter("@warningReasonID", WarningReasonID);

            SqlParameter paramErrorMessageSRV = new SqlParameter("@errorMessageForServiceToLog", SqlDbType.VarChar, -1);
            paramErrorMessageSRV.Direction = ParameterDirection.Output;

            SqlParameter paramErrorMessageToSave = new SqlParameter("@errorMessageToSave", SqlDbType.NVarChar, -1);
            paramErrorMessageToSave.Direction = ParameterDirection.Output;

            SqlParameter paramImportStatus = new SqlParameter("@importStatus", SqlDbType.VarChar, 15);
            paramImportStatus.Direction = ParameterDirection.Output;

            SqlParameter paramRecordsImported = new SqlParameter("@recordsImported", SqlDbType.Int);
            paramRecordsImported.Direction = ParameterDirection.Output;

            SqlParameter paramRetWarningReasonID = new SqlParameter("@retWarningReasonID", SqlDbType.Int);
            paramRetWarningReasonID.Direction = ParameterDirection.Output;


            cmdParamCollectionImport.Add(paramCompanyID);
            cmdParamCollectionImport.Add(paramDataImportID);
            cmdParamCollectionImport.Add(paramAddedBy);
            cmdParamCollectionImport.Add(paramDateAdded);
            cmdParamCollectionImport.Add(paramIsForceCommit);
            cmdParamCollectionImport.Add(paramWarningReasonID);


            //Get output parameters
            cmdParamCollectionImport.Add(paramErrorMessageSRV);
            cmdParamCollectionImport.Add(paramErrorMessageToSave);
            cmdParamCollectionImport.Add(paramImportStatus);
            cmdParamCollectionImport.Add(paramRecordsImported);
            cmdParamCollectionImport.Add(paramRetWarningReasonID);


            return oCommand;
        }
        #endregion
    }
}
