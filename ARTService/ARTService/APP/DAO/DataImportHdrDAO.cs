using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SkyStem.ART.Service.Model;
using System.Data.SqlClient;
using SkyStem.ART.Service.Utility;
using System.Data;
using SkyStem.ART.Service.Data;
using SkyStem.ART.Client.Model.CompanyDatabase;
using ClientModel = SkyStem.ART.Client.Model;

namespace SkyStem.ART.Service.APP.DAO
{
    public class DataImportHdrDAO : AbstractDAO
    {
        #region "Column Names"

        public static readonly string COLUMN_COMPANY_ID = "CompanyID";
        public static readonly string COLUMN_DATAIMPORT_ID = "DataImportID";
        public static readonly string COLUMN_RECONCILIATION_PERIOD_ID = "ReconciliationPeriodID";
        public static readonly string COLUMN_DATAIMPORT_TYPE_ID = "DataImportTypeID";
        public static readonly string COLUMN_PHYSICAL_PATH = "PhysicalPath";
        public static readonly string COLUMN_PERIOD_END_DATE = "PeriodEndDate";
        public static readonly string COLUMN_KEY_FIELDS = "KeyFields";
        public static readonly string COLUMN_DATA_IMPORT_STATUS_ID = "DataImportStatusID";
        public static readonly string COLUMN_IS_FORCE_COMMIT = "IsForceCommit";
        public static readonly string COLUMN_WARNING_REASON_ID = "WarningReasonID";

        public static readonly string COLUMN_NOTIFICATION_SUCCESS_EMAIL_IDs = "NotifySuccessEmailIDs";
        public static readonly string COLUMN_NOTIFICATION_SUCCESS_USER_EMALI_IDs = "NotifySuccessUserEmailIDs";
        public static readonly string COLUMN_NOTIFICATION_FAILURE_EMAIL_IDs = "NotifyFailureEmailIds";
        public static readonly string COLUMN_NOTIFICATION_FAILURE_USER_EMAIL_IDs = "NotifyFailureUserEmailIds";

        public static readonly string COLUMN_ADDED_BY_USER_EMAIL_ID = "AddedByUserEmailID";
        public static readonly string COLUMN_LANGUAGE_ID = "LanguageID";
        public static readonly string COLUMN_DEFAULT_LANGUAGE_ID = "DefaultLanguageID";
        public static readonly string COLUMN_DATAIMPORT_NAME = "DataImportName";
        //public static readonly string COLUMN_USER_LANGUAGE_ID = "UserLanguageID";

        public static readonly string COLUMN_ISACTIVE = "IsActive";
        public static readonly string COLUMN_DATEADDED = "DateAdded";
        public static readonly string COLUMN_ADDEDBY = "AddedBy";
        public static readonly string COLUMN_DATEREVISED = "DateRevised";
        public static readonly string COLUMN_REVISEDBY = "RevisedBy";
        public static readonly string COLUMN_ACCOUNT_UNIQUE_SUBSET_KEYS = "AccountUniqueSubSetKeys";
        public static readonly string COLUMN_IS_MULTIVERSION_UPLOAD = "IsMultiVersionUpload";
        public static readonly string COLUMN_IS_DATA_TRANSFERED = "IsDataTransfered";
        public static readonly string ROLE_ID = "RoleID";
        public static readonly string USER_ID = "UserID";
        public static readonly string IMPORT_TEMPLATE_ID = "ImportTemplateID"; 


        #endregion

        public List<ClientModel.LogInfo> LogInfoCache { get; set; }

        public DataImportHdrDAO(CompanyUserInfo oCompanyUserInfo)
            : base(oCompanyUserInfo)
        {
        }


        public void UpdateDataImportHDR(DataImportHdrInfo oDataImportHdrInfo)
        {
            SqlConnection oConnection = null;
            SqlTransaction oTransaction = null;
            SqlCommand oCommand = null;

            try
            {
                oConnection = this.GetConnection();
                oConnection.Open();
                oTransaction = oConnection.BeginTransaction();
                oCommand = GetDataImportHDRUpdateCommand(oDataImportHdrInfo);
                oCommand.Connection = oConnection;
                oCommand.Transaction = oTransaction;

                oCommand.ExecuteNonQuery();
                oTransaction.Commit();
                oTransaction.Dispose();
                oTransaction = null;
                Helper.LogInfoToCache("DataImportHdr Updated successfully for DataImportID: " + oDataImportHdrInfo.DataImportID.ToString(), this.LogInfoCache);
            }
            catch (Exception ex)
            {
                if (oTransaction != null)
                    oTransaction.Rollback();
                throw ex;
            }
            finally
            {
                if (oConnection != null && oConnection.State != ConnectionState.Closed)
                    oConnection.Close();
            }
        }

        public void UpdateDataTransferStatus(DataImportHdrInfo oDataImportHdrInfo, SqlConnection oConnection, SqlTransaction oTransaction)
        {
            SqlCommand oCommand = null;
            oCommand = GetUpdateDataTransferStatusCommand(oDataImportHdrInfo);
            oCommand.Connection = oConnection;
            oCommand.Transaction = oTransaction;

            oCommand.ExecuteNonQuery();
            Helper.LogInfoToCache("DataImportHdr Data Transfer Status Flag Updated for DataImportID: " + oDataImportHdrInfo.DataImportID.ToString(), this.LogInfoCache);
        }

        public void UpdateDataTransferStatus(DataImportHdrInfo oDataImportHdrInfo)
        {
            using (SqlConnection cnn = GetConnection())
            {
                cnn.Open();
                using (SqlCommand oCommand = GetUpdateDataTransferStatusCommand(oDataImportHdrInfo))
                {
                    oCommand.Connection = cnn;
                    oCommand.ExecuteNonQuery();
                }
            }
            Helper.LogInfoToCache("DataImportHdr Data Transfer Status Flag Updated for DataImportID: " + oDataImportHdrInfo.DataImportID.ToString(), this.LogInfoCache);
        }

        public void MapObject(SqlDataReader reader, DataImportHdrInfo oEntity)
        {
            //DataImportHdrInfo oEntity = new DataImportHdrInfo();
            int ordinal = -1;
            //CompanyID
            try
            {
                ordinal = reader.GetOrdinal(COLUMN_COMPANY_ID);
                if (!reader.IsDBNull(ordinal))
                    oEntity.CompanyID = reader.GetInt32(ordinal);
            }
            catch (IndexOutOfRangeException) { }
            catch (Exception) { }

            //DataImportID
            try
            {
                ordinal = reader.GetOrdinal(COLUMN_DATAIMPORT_ID);
                if (!reader.IsDBNull(ordinal))
                    oEntity.DataImportID = reader.GetInt32(ordinal);
            }
            catch (IndexOutOfRangeException) { }
            catch (Exception) { }

            //Rec Period ID
            try
            {
                ordinal = reader.GetOrdinal(COLUMN_RECONCILIATION_PERIOD_ID);
                if (!reader.IsDBNull(ordinal))
                    oEntity.RecPeriodID = reader.GetInt32(ordinal);
            }
            catch (IndexOutOfRangeException) { }
            catch (Exception) { }

            //Data Import Type ID
            try
            {
                ordinal = reader.GetOrdinal(COLUMN_DATAIMPORT_TYPE_ID);
                if (!reader.IsDBNull(ordinal))
                    oEntity.DataImportTypeID = reader.GetInt16(ordinal);
            }
            catch (IndexOutOfRangeException) { }
            catch (Exception) { }

            //Physical Path
            try
            {
                ordinal = reader.GetOrdinal(COLUMN_PHYSICAL_PATH);
                if (!reader.IsDBNull(ordinal))
                    oEntity.PhysicalPath = reader.GetString(ordinal);
            }
            catch (IndexOutOfRangeException) { }
            catch (Exception) { }

            //Period End Date
            try
            {
                ordinal = reader.GetOrdinal(COLUMN_PERIOD_END_DATE);
                if (!reader.IsDBNull(ordinal))
                    oEntity.PeriodEndDate = reader.GetDateTime(ordinal);
            }
            catch (IndexOutOfRangeException) { }
            catch (Exception) { }

            //KEY FIELDS
            try
            {
                ordinal = reader.GetOrdinal(COLUMN_KEY_FIELDS);
                if (!reader.IsDBNull(ordinal))
                    oEntity.KeyFields = reader.GetString(ordinal);
            }
            catch (IndexOutOfRangeException) { }
            catch (Exception) { }

            //Data Import Status ID
            try
            {
                ordinal = reader.GetOrdinal(COLUMN_DATA_IMPORT_STATUS_ID);
                if (!reader.IsDBNull(ordinal))
                    oEntity.DataImportStatusID = reader.GetInt16(ordinal);
            }
            catch (IndexOutOfRangeException) { }
            catch (Exception) { }

            //IsForceCommit
            try
            {
                ordinal = reader.GetOrdinal(COLUMN_IS_FORCE_COMMIT);
                if (!reader.IsDBNull(ordinal))
                    oEntity.IsForceCommit = reader.GetBoolean(ordinal);
            }
            catch (IndexOutOfRangeException) { }
            catch (Exception) { }

            //Notify Success EmailIDs
            try
            {
                ordinal = reader.GetOrdinal(COLUMN_NOTIFICATION_SUCCESS_EMAIL_IDs);
                if (!reader.IsDBNull(ordinal))
                    oEntity.NotifySuccessEmailIds = reader.GetString(ordinal);
            }
            catch (IndexOutOfRangeException) { }
            catch (Exception) { }

            //Notify Success User EmailIDs
            try
            {
                ordinal = reader.GetOrdinal(COLUMN_NOTIFICATION_SUCCESS_USER_EMALI_IDs);
                if (!reader.IsDBNull(ordinal))
                    oEntity.NotifySuccessUserEmailIds = reader.GetString(ordinal);
            }
            catch (IndexOutOfRangeException) { }
            catch (Exception) { }

            //Notify failure EmailIDs
            try
            {
                ordinal = reader.GetOrdinal(COLUMN_NOTIFICATION_FAILURE_EMAIL_IDs);
                if (!reader.IsDBNull(ordinal))
                    oEntity.NotifyFailureEmailIds = reader.GetString(ordinal);
            }
            catch (IndexOutOfRangeException) { }
            catch (Exception) { }

            //Notify failure User EmailIDs
            try
            {
                ordinal = reader.GetOrdinal(COLUMN_NOTIFICATION_FAILURE_USER_EMAIL_IDs);
                if (!reader.IsDBNull(ordinal))
                    oEntity.NotifyFailureUserEmailIds = reader.GetString(ordinal);
            }
            catch (IndexOutOfRangeException) { }
            catch (Exception) { }


            //Warning EmailID
            try
            {
                ordinal = reader.GetOrdinal(COLUMN_ADDED_BY_USER_EMAIL_ID);
                if (!reader.IsDBNull(ordinal))
                    oEntity.WarningEmailIds = reader.GetString(ordinal);
            }
            catch (IndexOutOfRangeException) { }
            catch (Exception) { }

            //LanguageID
            try
            {
                ordinal = reader.GetOrdinal(COLUMN_LANGUAGE_ID);
                if (!reader.IsDBNull(ordinal))
                    oEntity.LanguageID = reader.GetInt32(ordinal);
            }
            catch (IndexOutOfRangeException) { }
            catch (Exception) { }

            //Default Language ID
            try
            {
                ordinal = reader.GetOrdinal(COLUMN_DEFAULT_LANGUAGE_ID);
                //if (!reader.IsDBNull(ordinal))
                oEntity.DefaultLanguageID = ServiceConstants.DEFAULTLANGUAGEID;
            }
            catch (IndexOutOfRangeException) { }
            catch (Exception) { }



            //Profile Name
            try
            {
                ordinal = reader.GetOrdinal(COLUMN_DATAIMPORT_NAME);
                if (!reader.IsDBNull(ordinal))
                    oEntity.ProfileName = reader.GetString(ordinal);
            }
            catch (IndexOutOfRangeException) { }
            catch (Exception) { }

            //IsActive
            try
            {
                ordinal = reader.GetOrdinal(COLUMN_ISACTIVE);
                if (!reader.IsDBNull(ordinal))
                    oEntity.IsActive = reader.GetBoolean(ordinal);
            }
            catch (IndexOutOfRangeException) { }
            catch (Exception) { }

            //DATEADDED
            try
            {
                ordinal = reader.GetOrdinal(COLUMN_DATEADDED);
                if (!reader.IsDBNull(ordinal))
                    oEntity.DateAdded = reader.GetDateTime(ordinal);
            }
            catch (IndexOutOfRangeException) { }
            catch (Exception) { }


            try
            {
                ordinal = reader.GetOrdinal(COLUMN_ADDEDBY);
                if (!reader.IsDBNull(ordinal))
                    oEntity.AddedBy = reader.GetString(ordinal);
            }
            catch (IndexOutOfRangeException) { }
            catch (Exception) { }

            //Warning ReasonID
            try
            {
                ordinal = reader.GetOrdinal(COLUMN_WARNING_REASON_ID);
                if (!reader.IsDBNull(ordinal))
                    oEntity.WarningReasonID = reader.GetInt16(ordinal);
            }
            catch (IndexOutOfRangeException) { }
            catch (Exception) { }


            try
            {
                ordinal = reader.GetOrdinal(COLUMN_ACCOUNT_UNIQUE_SUBSET_KEYS);
                if (!reader.IsDBNull(ordinal))
                    oEntity.AccountUniqueSubSetKeys = reader.GetString(ordinal);
            }
            catch (IndexOutOfRangeException) { }
            catch (Exception) { }

            try
            {
                ordinal = reader.GetOrdinal(COLUMN_ACCOUNT_UNIQUE_SUBSET_KEYS);
                if (!reader.IsDBNull(ordinal))
                    oEntity.AccountUniqueSubSetKeys = reader.GetString(ordinal);
            }
            catch (IndexOutOfRangeException) { }
            catch (Exception) { }

            try
            {
                ordinal = reader.GetOrdinal(COLUMN_IS_MULTIVERSION_UPLOAD);
                if (!reader.IsDBNull(ordinal))
                    oEntity.IsMultiVersionUpload = reader.GetBoolean(ordinal);
            }
            catch (IndexOutOfRangeException) { }
            catch (Exception) { }

            //try
            //{
            //    ordinal = reader.GetOrdinal(COLUMN_DATAIMPORT_ID);
            //    if (!reader.IsDBNull(ordinal))
            //        oEntity.DataImportID = reader.GetInt32(ordinal);
            //}
            //catch (IndexOutOfRangeException) { }
            //catch (Exception) { }
            try
            {
                ordinal = reader.GetOrdinal(COLUMN_IS_DATA_TRANSFERED);
                if (!reader.IsDBNull(ordinal))
                    oEntity.IsDataTransfered = reader.GetBoolean(ordinal);
            }
            catch (IndexOutOfRangeException) { }
            catch (Exception) { }

            //UserID
            try
            {
                ordinal = reader.GetOrdinal(USER_ID);
                if (!reader.IsDBNull(ordinal))
                    oEntity.UserID = reader.GetInt32(ordinal);
            }
            catch (IndexOutOfRangeException) { }
            catch (Exception) { }


            //RoleID
            try
            {
                ordinal = reader.GetOrdinal(ROLE_ID);
                if (!reader.IsDBNull(ordinal))
                    oEntity.RoleID = reader.GetInt16(ordinal);
            }
            catch (IndexOutOfRangeException) { }
            catch (Exception) { }

            //ImportTemplateID
            try
            {
                ordinal = reader.GetOrdinal(IMPORT_TEMPLATE_ID);
                if (!reader.IsDBNull(ordinal))
                    oEntity.ImportTemplateID = reader.GetInt32(ordinal);
            }
            catch (IndexOutOfRangeException) { }
            catch (Exception) { }

             

        }

        public void GetDataImportForProcessing(DataImportHdrInfo oEntity, Enums.DataImportType dataImportType, DateTime dateRevised)
        {
            SqlConnection oConn = null;
            SqlTransaction oTrans = null;
            SqlCommand oCmd = null;
            SqlDataReader reader = null;
            try
            {
                oConn = this.GetConnection();
                oCmd = this.GetDataImportForProcessingComand(dataImportType, dateRevised);
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

        public bool IsDataImportProcessingRequired(Enums.DataImportType dataImportType)
        {
            using (SqlConnection oConn = this.GetConnection())
            {
                oConn.Open();
                using (SqlCommand oCmd = this.GetIsDataImportProcessingRequiredCommand(dataImportType))
                {
                    oCmd.Connection = oConn;
                    int retVal = Convert.ToInt32(oCmd.ExecuteScalar());
                    oConn.Close();
                    if (retVal > 0)
                        return true;
                }
            }
            return false;
        }

        #region "Private Methods"
        private SqlCommand GetDataImportHDRUpdateCommand(DataImportHdrInfo oDataImportInfo)
        {
            SqlCommand oCommand = this.CreateCommand();
            oCommand.CommandType = CommandType.StoredProcedure;
            oCommand.CommandText = "usp_UPD_DataImportHdr";

            SqlParameterCollection cmdParamCollection = oCommand.Parameters;
            SqlParameter paramDataImportID = new SqlParameter("@dataImportID", oDataImportInfo.DataImportID);
            SqlParameter paramErrorMessageToSave = new SqlParameter("@errorMessageToSave", SqlDbType.NVarChar, -1);
            SqlParameter paramImportStatus = new SqlParameter("@dataImportStatusID", SqlDbType.SmallInt);
            SqlParameter paramRecordsImported = new SqlParameter("@recordsImported", SqlDbType.Int);
            SqlParameter paramWarningReasonID = new SqlParameter("@warningReasonID", SqlDbType.SmallInt);
            SqlParameter paramDataDeletionRequired = new SqlParameter("@IsDataDeletionRequired", SqlDbType.Bit);
            SqlParameter paramProfilingDataToSave = new SqlParameter("@ProfilingData", SqlDbType.Xml);
            SqlParameter paramDataImportStatusMessage = new SqlParameter();
            paramDataImportStatusMessage.ParameterName = "@udtDataImportMessageDetailType";

            if (oDataImportInfo.ErrorMessageToSave != null)
                paramErrorMessageToSave.Value = oDataImportInfo.ErrorMessageToSave;
            else
                paramErrorMessageToSave.Value = string.Empty;

            if (oDataImportInfo.DataImportStatus == DataImportStatus.DATAIMPORTFAIL 
                || oDataImportInfo.DataImportStatus == DataImportStatus.DATAIMPORTSEVEREWARNING
                || oDataImportInfo.DataImportStatus == DataImportStatus.DATAIMPORTERRORS)
                paramImportStatus.Value = (short)Enums.DataImportStatus.Failure;

            if (oDataImportInfo.DataImportStatus == DataImportStatus.DATAIMPORTWARNING)
                paramImportStatus.Value = (short)Enums.DataImportStatus.Warning;

            if (oDataImportInfo.DataImportStatus == DataImportStatus.DATAIMPORTSUCCESS)
                paramImportStatus.Value = (short)Enums.DataImportStatus.Success;
            paramRecordsImported.Value = oDataImportInfo.RecordsImported;

            if (oDataImportInfo.WarningReasonID.HasValue)
                paramWarningReasonID.Value = oDataImportInfo.WarningReasonID.Value;
            else
                paramWarningReasonID.Value = DBNull.Value;
            paramDataDeletionRequired.Value = oDataImportInfo.IsDataDeletionRequired;

            if (string.IsNullOrEmpty(oDataImportInfo.ProfilingData))
                paramProfilingDataToSave.Value = DBNull.Value;
            else
                paramProfilingDataToSave.Value = oDataImportInfo.ProfilingData;

            if (oDataImportInfo.DataImportMessageDetailInfoList != null && oDataImportInfo.DataImportMessageDetailInfoList.Count > 0)
                paramDataImportStatusMessage.Value = DataImportHelper.ConvertDataImportStatusMessageToDataTable(oDataImportInfo.DataImportMessageDetailInfoList);

            cmdParamCollection.Add(paramDataImportID);
            cmdParamCollection.Add(paramErrorMessageToSave);
            cmdParamCollection.Add(paramImportStatus);
            cmdParamCollection.Add(paramRecordsImported);
            cmdParamCollection.Add(paramWarningReasonID);
            cmdParamCollection.Add(paramDataDeletionRequired);
            cmdParamCollection.Add(paramProfilingDataToSave);
            cmdParamCollection.Add(paramDataImportStatusMessage);
            return oCommand;
        }
        private SqlCommand GetDataImportForProcessingComand(Enums.DataImportType dataImportType, DateTime dateRevised)
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
        private SqlCommand GetUpdateDataTransferStatusCommand(DataImportHdrInfo oDataImportInfo)
        {
            SqlCommand oCommand = this.CreateCommand();
            oCommand.CommandType = CommandType.StoredProcedure;
            oCommand.CommandText = "usp_UPD_DataImportHdrDataTransferStatus";

            SqlParameterCollection cmdParamCollection = oCommand.Parameters;
            SqlParameter paramDataImportID = new SqlParameter("@DataImportID", oDataImportInfo.DataImportID);
            SqlParameter paramDataTransfered = new SqlParameter("@IsDataTransfered", SqlDbType.Bit);

            paramDataTransfered.Value = oDataImportInfo.IsDataTransfered;

            cmdParamCollection.Add(paramDataImportID);
            cmdParamCollection.Add(paramDataTransfered);
            return oCommand;
        }
        private SqlCommand GetIsDataImportProcessingRequiredCommand(Enums.DataImportType dataImportType)
        {
            SqlCommand oCommand = this.CreateCommand();
            oCommand.CommandType = CommandType.StoredProcedure;
            oCommand.CommandText = "usp_GET_IsDataImportProcessingRequired";

            SqlParameterCollection cmdParamCollection = oCommand.Parameters;

            SqlParameter paramDataImportTypeId = new SqlParameter("@dataImportTypeId", dataImportType);
            SqlParameter paramDataImportStatusId = new SqlParameter("@dataImportStatusID", Enums.DataImportStatus.ToBeProcessed);
            SqlParameter paramWarningDataImportStatusId = new SqlParameter("@warningDataImportStatusID", Enums.DataImportStatus.Warning);

            cmdParamCollection.Add(paramDataImportTypeId);
            cmdParamCollection.Add(paramDataImportStatusId);
            cmdParamCollection.Add(paramWarningDataImportStatusId);
            return oCommand;
        }
        #endregion
    }
}
