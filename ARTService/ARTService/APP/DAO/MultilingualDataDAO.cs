using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SkyStem.ART.Service.Model;
using SkyStem.ART.Service.Data;
using System.Data.SqlClient;
using System.Data;
using SkyStem.ART.Service.Utility;
using System.Xml.Serialization;
using System.IO;
using System.Xml;
using SkyStem.ART.Service.APP.BLL;
using SkyStem.ART.Client.Model.CompanyDatabase;

namespace SkyStem.ART.Service.APP.DAO
{
    public class MultilingualDataDAO : DataImportHdrDAO
    {
        public static readonly string COLUMN_FROM_LANGUAGE_ID = "FromLanguageID";
        public static readonly string COLUMN_TO_LANGUAGE_ID = "ToLanguageID";

        public MultilingualDataDAO(CompanyUserInfo oCompanyUserInfo)
            : base(oCompanyUserInfo)
        {

        }

        #region "Public Methods"
        public MultilingualDataImportHdrInfo GetMultilingualDataImportForProcessing(DateTime dateRevised)
        {
            MultilingualDataImportHdrInfo oEntity = new MultilingualDataImportHdrInfo();
            if (this.IsDataImportProcessingRequired(Enums.DataImportType.MultilingualUpload))
            {
                this.GetMultilingualDataImportForProcessing(oEntity, Enums.DataImportType.MultilingualUpload, dateRevised);
            }
            return oEntity;
        }

        public void GetMultilingualDataImportForProcessing(MultilingualDataImportHdrInfo oEntity, Enums.DataImportType dataImportType
            , DateTime dateRevised)
        {
            SqlConnection oConn = null;
            SqlTransaction oTrans = null;
            SqlCommand oCmd = null;
            SqlDataReader reader = null;
            try
            {
                oConn = this.GetConnection();
                oCmd = this.GetMultilingualDataImportForProcessingComand(dataImportType, dateRevised);
                oCmd.Connection = oConn;
                oCmd.Connection.Open();
                oTrans = oConn.BeginTransaction();
                oCmd.Transaction = oTrans;
                reader = oCmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    this.CustomMapObject(reader, oEntity);
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

        public void CustomMapObject(SqlDataReader reader, MultilingualDataImportHdrInfo oEntity)
        {
            base.MapObject(reader, oEntity);
            int ordinal = -1;
            //From LanguageID
            try
            {
                ordinal = reader.GetOrdinal(COLUMN_FROM_LANGUAGE_ID);
                if (!reader.IsDBNull(ordinal))
                    oEntity.FromLanguageID = reader.GetInt32(ordinal);
            }
            catch (IndexOutOfRangeException) { }
            catch (Exception) { }
            //To LanguageID
            try
            {
                ordinal = reader.GetOrdinal(COLUMN_TO_LANGUAGE_ID);
                if (!reader.IsDBNull(ordinal))
                    oEntity.ToLanguageID = reader.GetInt32(ordinal);
            }
            catch (IndexOutOfRangeException) { }
            catch (Exception) { }
        }

        private SqlCommand GetMultilingualDataImportForProcessingComand(Enums.DataImportType dataImportType, DateTime dateRevised)
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

        #endregion
    }
}
