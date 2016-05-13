using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SkyStem.ART.Service.Model;
using SkyStem.ART.Service.Data;
using System.Data.SqlClient;
using System.Data;
using SkyStem.ART.Service.Utility;
using SkyStem.ART.Client.Model.CompanyDatabase;
using SkyStem.ART.Client.Model;
using System.Transactions;

namespace SkyStem.ART.Service.APP.DAO
{
    public class ScheduleRecItemImportDAO : DataImportHdrDAO
    {

        public static readonly string COLUMN_GLDATA_ID = "GLDataID";
        public static readonly string COLUMN_RECONCILIATION_CATEGORY_ID = "ReconciliationCategoryID";
        public static readonly string COLUMN_RECONCILIATION_CATEGORY_TYPE_ID = "ReconciliationCategoryTypeID";
        public static readonly string COLUMN_REPORTING_CURRENCY_CODE = "ReportingCurrencyCode";
        public static readonly string COLUMN_BASE_CURRENCY_CODE = "BaseCurrencyCode";


        public ScheduleRecItemImportDAO(CompanyUserInfo oCompanyUserInfo)
            : base(oCompanyUserInfo)
        {

        }

        #region "Public Methods"
        public ScheduleRecItemImportInfo GetScheduleRecItemImportForProcessing(DateTime dateRevised)
        {
            ScheduleRecItemImportInfo oEntity = new ScheduleRecItemImportInfo();
            if (this.IsDataImportProcessingRequired(Enums.DataImportType.ScheduleRecItems))
            {
                this.GetScheduleRecItemImportForProcessing(oEntity, Enums.DataImportType.ScheduleRecItems, dateRevised);
            }
            return oEntity;
        }

        public void GetScheduleRecItemImportForProcessing(ScheduleRecItemImportInfo oEntity, Enums.DataImportType dataImportType
            , DateTime dateRevised)
        {
            using (SqlConnection oConn = this.GetConnection())
            {
                oConn.Open();
                using (SqlTransaction oTran = oConn.BeginTransaction())
                {
                    using (SqlCommand oCmd = this.GetScheduleRecItemImportForProcessingComand(dataImportType, dateRevised))
                    {
                        oCmd.Connection = oConn;
                        oCmd.Transaction = oTran;
                        SqlDataReader reader = oCmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            reader.Read();
                            this.CustomMapObject(reader, oEntity);
                        }
                        if (reader.NextResult())
                        {
                            oEntity.GLDataRecurringItemScheduleIntervalDetailInfoList = new List<GLDataRecurringItemScheduleIntervalDetailInfo>();
                            while (reader.Read())
                            {
                                oEntity.GLDataRecurringItemScheduleIntervalDetailInfoList.Add(this.MapScheduleIntervalDetailObject(reader));
                            }
                        }
                        if (reader != null && !reader.IsClosed)
                            reader.Close(); 
                        oTran.Commit();
                        string errorDescrp = oCmd.Parameters["@errorMessageForServiceToLog"].Value.ToString();
                        int retVal = Convert.ToInt32(oCmd.Parameters["@returnValue"].Value);
                        if (retVal < 0)
                            throw new Exception(errorDescrp);
                    }
                }
            }
        }

        public void CustomMapObject(SqlDataReader reader, ScheduleRecItemImportInfo oEntity)
        {
            base.MapObject(reader, oEntity);
            int ordinal = -1;
            //GLData ID
            try
            {
                ordinal = reader.GetOrdinal(COLUMN_GLDATA_ID);
                if (!reader.IsDBNull(ordinal))
                    oEntity.GLDataID = reader.GetInt64(ordinal);
            }
            catch (IndexOutOfRangeException) { }
            catch (Exception) { }
            //Rec Category ID
            try
            {
                ordinal = reader.GetOrdinal(COLUMN_RECONCILIATION_CATEGORY_ID);
                if (!reader.IsDBNull(ordinal))
                    oEntity.ReconciliationCategoryID = reader.GetInt16(ordinal);
            }
            catch (IndexOutOfRangeException) { }
            catch (Exception) { }
            //Rec Category Type ID
            try
            {
                ordinal = reader.GetOrdinal(COLUMN_RECONCILIATION_CATEGORY_TYPE_ID);
                if (!reader.IsDBNull(ordinal))
                    oEntity.ReconciliationCategoryTypeID = reader.GetInt16(ordinal);
            }
            catch (IndexOutOfRangeException) { }
            catch (Exception) { }
            //Reporting Currency Code
            try
            {
                ordinal = reader.GetOrdinal(COLUMN_REPORTING_CURRENCY_CODE);
                if (!reader.IsDBNull(ordinal))
                    oEntity.ReportingCurrencyCode = reader.GetString(ordinal);
            }
            catch (IndexOutOfRangeException) { }
            catch (Exception) { }
            //Base Currency Code
            try
            {
                ordinal = reader.GetOrdinal(COLUMN_BASE_CURRENCY_CODE);
                if (!reader.IsDBNull(ordinal))
                    oEntity.BaseCurrencyCode = reader.GetString(ordinal);
            }
            catch (IndexOutOfRangeException) { }
            catch (Exception) { }
        }

        public GLDataRecurringItemScheduleIntervalDetailInfo MapScheduleIntervalDetailObject(SqlDataReader reader)
        {
            GLDataRecurringItemScheduleIntervalDetailInfo oEntity = new GLDataRecurringItemScheduleIntervalDetailInfo();
            int ordinal = -1;
            //Reconciliation Period ID
            try
            {
                ordinal = reader.GetOrdinal(COLUMN_RECONCILIATION_PERIOD_ID);
                if (!reader.IsDBNull(ordinal))
                    oEntity.ReconciliationPeriodID = reader.GetInt32(ordinal);
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
            return oEntity;
        }

        private SqlCommand GetScheduleRecItemImportForProcessingComand(Enums.DataImportType dataImportType, DateTime dateRevised)
        {
            SqlCommand oCommand = this.CreateCommand();
            oCommand.CommandType = CommandType.StoredProcedure;
            oCommand.CommandText = "usp_GET_RecItemImportForProcessing";

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

        #endregion
    }
}
