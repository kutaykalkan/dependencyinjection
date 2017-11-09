using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SkyStem.ART.Service.Data;
using SkyStem.ART.Service.Utility;
using System.Data.SqlClient;
using System.Data;
using SkyStem.ART.Client.Model.CompanyDatabase;

namespace SkyStem.ART.Service.APP.BLL
{
    public class MaterialityAndSRA
    {
        private SqlConnection oConnection = null;
        private SqlTransaction oTransaction = null;
        private SqlDataReader reader = null;
        public int RecPeriodID;

        public static string errorMessageFromSqlServer = "";
        private static int recordsImported = 0;
        private CompanyUserInfo CompanyUserInfo;

        public MaterialityAndSRA(CompanyUserInfo oCompanyUserInfo)
        {
            this.CompanyUserInfo = oCompanyUserInfo;
        }

        public void SetMaterialityAndSRAStaus()
        {
            if (oConnection != null)
                oConnection = Helper.CreateConnection(this.CompanyUserInfo);
            if (oConnection != null && oConnection.State != ConnectionState.Open)
                oConnection.Open();
            try
            {
                if (this.RecPeriodID > 0)
                {
                    //Process GLDataHDR for recPeriod
                    ProcessMaterialityAndSRAByCompanyIDRecPeriodID();
                    //Update CompanySetting for GLDataProcessingStatusID field
                    UpdateReconciliationPeriodForGLDataProcessingStatusID();
                }

            }
            catch (Exception ex)
            {
                Helper.LogError(ex, this.CompanyUserInfo);
            }
            finally
            {
                if (oConnection != null && oConnection.State != ConnectionState.Closed)
                    oConnection.Close();
            }
        }

        public bool IsProcessingRequiredForMaterialityAndSRA()
        {
            bool isProcessingRequired = false;
            try
            {
                this.RecPeriodID = 0;
                errorMessageFromSqlServer = "";
                if (oConnection == null)
                    oConnection = Helper.CreateConnection(this.CompanyUserInfo);
                if (oConnection != null && oConnection.State != ConnectionState.Open)
                    oConnection.Open();
                oTransaction = oConnection.BeginTransaction();
                SqlCommand oCommand = GetCommandForMaterialAndSRAFlags();
                oCommand.Connection = oConnection;
                oCommand.Transaction = oTransaction;
                oCommand.ExecuteNonQuery();

                errorMessageFromSqlServer = oCommand.Parameters["@errorMessageForServiceToLog"].Value.ToString();
                RecPeriodID = (int)oCommand.Parameters["@returnValue"].Value;

                switch (this.RecPeriodID)
                {
                    case 0:
                        Helper.LogInfo(@"No Data Available for processing of Materiality and SRA.", this.CompanyUserInfo);
                        isProcessingRequired = false;
                        break;
                    case -1:
                        throw new Exception(errorMessageFromSqlServer);
                    default:
                        Helper.LogInfo(@"Data needs to be processed for updating Material 
                        and SRA Flage for RecPeriodID: " + this.RecPeriodID.ToString(), this.CompanyUserInfo);
                        isProcessingRequired = true;
                        break;
                }
                oTransaction.Commit();
                oTransaction.Dispose();
                oTransaction = null;
                oConnection.Close();
                return isProcessingRequired;
            }
            catch (Exception ex)
            {
                if (oTransaction != null)
                    oTransaction.Rollback();
                Helper.LogError(@"Error while reading data from RecPeriod for Materiality and SRA Update.", this.CompanyUserInfo);
                throw ex;
            }
            finally
            {
                if (oConnection != null && oConnection.State != ConnectionState.Closed)
                    oConnection.Close();
            }
        }

        private void ProcessMaterialityAndSRAByCompanyIDRecPeriodID()
        {
            try
            {
                if (oConnection != null && oConnection.State != ConnectionState.Open)
                    oConnection.Open();
                oTransaction = null;
                oTransaction = oConnection.BeginTransaction();
                SqlCommand oCommandUpdate = GetUpdateCommandForMaterialAndSRAFlags();
                oCommandUpdate.Connection = oConnection;
                oCommandUpdate.Transaction = oTransaction;
                oCommandUpdate.ExecuteNonQuery();

                errorMessageFromSqlServer = oCommandUpdate.Parameters["@errorMessageForServiceToLog"].Value.ToString();
                recordsImported = Convert.ToInt32(oCommandUpdate.Parameters["@recordsUpdated"].Value.ToString());

                if (recordsImported < 0)
                    throw new Exception(errorMessageFromSqlServer);
                else
                    Helper.LogInfo("GLData Records updated for Materiality and SRA. ", this.CompanyUserInfo);
                oTransaction.Commit();
                oTransaction.Dispose();
                oTransaction = null;
            }
            catch (Exception ex)
            {
                if (oTransaction != null)
                    oTransaction.Rollback();
                Helper.LogError(@"Error while updating Materiality and SRA for RecPeriodID: " + RecPeriodID.ToString(), this.CompanyUserInfo);
                throw ex;

            }
        }

        private void UpdateReconciliationPeriodForGLDataProcessingStatusID()
        {
            try
            {
                if (oConnection != null && oConnection.State != ConnectionState.Open)
                    oConnection.Open();
                oTransaction = null;
                oTransaction = oConnection.BeginTransaction();
                SqlCommand oCommandUpdate = GetUpdateCommandForGLDataProcessingStatusID();
                oCommandUpdate.Connection = oConnection;
                oCommandUpdate.Transaction = oTransaction;
                oCommandUpdate.ExecuteNonQuery();
                errorMessageFromSqlServer = oCommandUpdate.Parameters["@errorMessageForServiceToLog"].Value.ToString();
                int returnValue = Convert.ToInt32(oCommandUpdate.Parameters["@returnValue"].Value.ToString());

                if (returnValue < 0)
                    throw new Exception(errorMessageFromSqlServer);
                else
                    Helper.LogInfo(@"ReconciliationPeriod record with RecPeriodID: " + RecPeriodID.ToString() + " updated successfully.", this.CompanyUserInfo);
                oTransaction.Commit();
                oTransaction.Dispose();
                oTransaction = null;
            }
            catch (Exception ex)
            {
                if (oTransaction != null)
                    oTransaction.Rollback();
                Helper.LogError(@"Error while updating GLDataProcessingStatusID for record: " + RecPeriodID.ToString(), this.CompanyUserInfo );
                throw ex;
            }
        }

        #region "Get Commands Methods"

        private SqlCommand GetCommandForMaterialAndSRAFlags()
        {
            SqlCommand oCommand = Helper.CreateCommand(this.CompanyUserInfo);
            oCommand.CommandType = CommandType.StoredProcedure;
            oCommand.CommandText = "usp_GET_RecPeriodIDForGLDataProcessing";

            SqlParameterCollection cmdParamCollection = oCommand.Parameters;

            SqlParameter paramGLDataToBeProcessedStatusID = new SqlParameter("@glDataToBeProcessedStatusID", (short)Enums.GLDataProcessingStatus.ToBeProcessed);
            SqlParameter paramGLDataProcessingStatusID = new SqlParameter("@glDataProcessingStatusID", (short)Enums.GLDataProcessingStatus.Processing);

            SqlParameter paramErrorMessageSRV = new SqlParameter("@errorMessageForServiceToLog", SqlDbType.VarChar, 8000);
            paramErrorMessageSRV.Direction = ParameterDirection.Output;

            SqlParameter paramRecPeriodID = new SqlParameter("@returnValue", SqlDbType.Int);
            paramRecPeriodID.Direction = ParameterDirection.ReturnValue;

            cmdParamCollection.Add(paramGLDataToBeProcessedStatusID);
            cmdParamCollection.Add(paramGLDataProcessingStatusID);
            cmdParamCollection.Add(paramErrorMessageSRV);
            cmdParamCollection.Add(paramRecPeriodID);
            return oCommand;

        }

        private SqlCommand GetUpdateCommandForMaterialAndSRAFlags()
        {
            SqlCommand oCommand = Helper.CreateCommand(this.CompanyUserInfo);
            oCommand.CommandType = CommandType.StoredProcedure;
            oCommand.CommandText = "usp_UPD_GLDataHdrForUpdateMaterialAndSRA";

            SqlParameterCollection cmdParamCollection = oCommand.Parameters;
            SqlParameter paramCompanySettingID = new SqlParameter("@RecPeriodID", this.RecPeriodID);

            SqlParameter paramErrorMessageSRV = new SqlParameter("@errorMessageForServiceToLog", SqlDbType.VarChar, 8000);
            paramErrorMessageSRV.Direction = ParameterDirection.Output;

            SqlParameter paramRecordsUpdated = new SqlParameter("@recordsUpdated", SqlDbType.Int);
            paramRecordsUpdated.Direction = ParameterDirection.ReturnValue;

            cmdParamCollection.Add(paramCompanySettingID);
            cmdParamCollection.Add(paramErrorMessageSRV);
            cmdParamCollection.Add(paramRecordsUpdated);

            return oCommand;
        }

        private SqlCommand GetUpdateCommandForGLDataProcessingStatusID()
        {
            SqlCommand oCommand = Helper.CreateCommand(this.CompanyUserInfo);
            oCommand.CommandType = CommandType.StoredProcedure;
            oCommand.CommandText = "usp_UPD_ReconciliationPeriodForGLDataProcessingStatusID";

            SqlParameterCollection cmdParamCollection = oCommand.Parameters;

            SqlParameter paramCompanyID = new SqlParameter("@RecPeriodID", RecPeriodID);

            SqlParameter paramProcessedStatusID = new SqlParameter("@processedStatusID", (short)Enums.GLDataProcessingStatus.Processed);

            SqlParameter paramErrorMessageSRV = new SqlParameter("@errorMessageForServiceToLog", SqlDbType.VarChar, 8000);
            paramErrorMessageSRV.Direction = ParameterDirection.Output;

            SqlParameter paramReturnValue = new SqlParameter("@returnValue", SqlDbType.Int);
            paramReturnValue.Direction = ParameterDirection.ReturnValue;

            cmdParamCollection.Add(paramCompanyID);
            cmdParamCollection.Add(paramProcessedStatusID);
            cmdParamCollection.Add(paramErrorMessageSRV);
            cmdParamCollection.Add(paramReturnValue);
            return oCommand;
        }
        #endregion
    }
}
