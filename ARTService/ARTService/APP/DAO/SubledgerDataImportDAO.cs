using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SkyStem.ART.Service.Model;
using SkyStem.ART.Service.Data;
using System.Data.SqlClient;
using SkyStem.ART.Service.Utility;
using System.Data;
using SkyStem.ART.Client.Model.CompanyDatabase;
using SkyStem.ART.Client.Model;

namespace SkyStem.ART.Service.APP.DAO
{
    public class SubledgerDataImportDAO : DataImportHdrDAO
    {
        public SubledgerDataImportDAO(CompanyUserInfo oCompanyUserInfo)
            : base(oCompanyUserInfo)
        {

        }

        public SubledgerDataImportInfo GetSubledgerDataImportForProcessing(DateTime dateRevised)
        {
            SubledgerDataImportInfo oEntity = new SubledgerDataImportInfo();
            if (this.IsDataImportProcessingRequired(Enums.DataImportType.SubledgerData))
            {
                this.GetDataImportForProcessing(oEntity, Enums.DataImportType.SubledgerData, dateRevised);
            }
            return oEntity;
        }

        public void CopySubledgerDataToSqlServer(DataTable oExcelData, SubledgerDataImportInfo oSubledgerDataImportInfo, SqlConnection oConnection, SqlTransaction oTransaction)
        {
            DateTime dateAdded = DateTime.Now.Date;
            using (SqlBulkCopy oSqlBlkCopy = new SqlBulkCopy(oConnection, SqlBulkCopyOptions.Default, oTransaction))
            {
                Helper.LogInfoToCache("5. Mapping fields from source to destination.", this.LogInfoCache);

                oSqlBlkCopy.DestinationTableName = "GLDataTransit";
                Helper.AddSqlBulkCopyMapping(oExcelData, oSqlBlkCopy, AddedGLDataImportFields.DATAIMPORTID, GLDataImportTransitFields.DATAIMPORTID);
                Helper.AddSqlBulkCopyMapping(oExcelData, oSqlBlkCopy, AddedGLDataImportFields.EXCELROWNUMBER, GLDataImportTransitFields.EXCELROWNUMBER);
                Helper.AddSqlBulkCopyMapping(oExcelData, oSqlBlkCopy, AddedGLDataImportFields.RECPERIODENDDATE, GLDataImportTransitFields.PERIODENDDATE);              
                Helper.AddSqlBulkCopyMapping(oExcelData, oSqlBlkCopy, GLDataImportFields.GLACCOUNTNUMBER, GLDataImportTransitFields.GLACCOUNTNUMBER);
                Helper.AddSqlBulkCopyMapping(oExcelData, oSqlBlkCopy, GLDataImportFields.GLACCOUNTNAME, GLDataImportTransitFields.GLACCOUNTNAME);
                Helper.AddSqlBulkCopyMapping(oExcelData, oSqlBlkCopy, GLDataImportFields.FSCAPTION, GLDataImportTransitFields.FSCAPTION);
                Helper.AddSqlBulkCopyMapping(oExcelData, oSqlBlkCopy, GLDataImportFields.ACCOUNTTYPE, GLDataImportTransitFields.ACCOUNTTYPE);
                Helper.AddSqlBulkCopyMapping(oExcelData, oSqlBlkCopy, GLDataImportFields.ISPROFITANDLOSS, GLDataImportTransitFields.ISPROFITANDLOSS);
                Helper.AddSqlBulkCopyMapping(oExcelData, oSqlBlkCopy, GLDataImportFields.BCCYCODE, GLDataImportTransitFields.BCCYCODE);
                Helper.AddSqlBulkCopyMapping(oExcelData, oSqlBlkCopy, GLDataImportFields.BALANCEBCCY, GLDataImportTransitFields.BALANCEBCCY);
                Helper.AddSqlBulkCopyMapping(oExcelData, oSqlBlkCopy, GLDataImportFields.BALANCERCCY, GLDataImportTransitFields.BALANCERCCY);
                Helper.AddSqlBulkCopyMapping(oExcelData, oSqlBlkCopy, GLDataImportFields.RCCYCODE, GLDataImportTransitFields.RCCYCODE);
                


                //oSqlBlkCopy.ColumnMappings.Add(AddedGLDataImportFields.DATAIMPORTID, GLDataImportTransitFields.DATAIMPORTID);
                //oSqlBlkCopy.ColumnMappings.Add(AddedGLDataImportFields.EXCELROWNUMBER, GLDataImportTransitFields.EXCELROWNUMBER);
                //oSqlBlkCopy.ColumnMappings.Add(AddedGLDataImportFields.RECPERIODENDDATE, GLDataImportTransitFields.PERIODENDDATE);

                //if (oExcelData.Columns.Contains(GLDataImportFields.GLACCOUNTNUMBER))
                //    oSqlBlkCopy.ColumnMappings.Add(GLDataImportFields.GLACCOUNTNUMBER, GLDataImportTransitFields.GLACCOUNTNUMBER);

                //if (oExcelData.Columns.Contains(GLDataImportFields.GLACCOUNTNAME))
                //    oSqlBlkCopy.ColumnMappings.Add(GLDataImportFields.GLACCOUNTNAME, GLDataImportTransitFields.GLACCOUNTNAME);

                //if (oExcelData.Columns.Contains(GLDataImportFields.FSCAPTION))
                //    oSqlBlkCopy.ColumnMappings.Add(GLDataImportFields.FSCAPTION, GLDataImportTransitFields.FSCAPTION);

                //if (oExcelData.Columns.Contains(GLDataImportFields.ACCOUNTTYPE))
                //    oSqlBlkCopy.ColumnMappings.Add(GLDataImportFields.ACCOUNTTYPE, GLDataImportTransitFields.ACCOUNTTYPE);

                //if (oExcelData.Columns.Contains(GLDataImportFields.ISPROFITANDLOSS))
                //    oSqlBlkCopy.ColumnMappings.Add(GLDataImportFields.ISPROFITANDLOSS, GLDataImportTransitFields.ISPROFITANDLOSS);

                //oSqlBlkCopy.ColumnMappings.Add(GLDataImportFields.BCCYCODE, GLDataImportTransitFields.BCCYCODE);

                //oSqlBlkCopy.ColumnMappings.Add(GLDataImportFields.BALANCEBCCY, GLDataImportTransitFields.BALANCEBCCY);

                //oSqlBlkCopy.ColumnMappings.Add(GLDataImportFields.BALANCERCCY, GLDataImportTransitFields.BALANCERCCY);

                //oSqlBlkCopy.ColumnMappings.Add(GLDataImportFields.RCCYCODE, GLDataImportTransitFields.RCCYCODE);


                string[] arrKeyFields = oSubledgerDataImportInfo.KeyFields.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                for (int k = 0; k < arrKeyFields.Length; k++)
                {
                    string sourceField = arrKeyFields[k].ToString();
                    string targetField = "Key" + (k + 2).ToString();
                    //if (oExcelData.Columns.Contains(sourceField))
                    //    oSqlBlkCopy.ColumnMappings.Add(sourceField, targetField);
                    Helper.AddSqlBulkCopyMapping(oExcelData, oSqlBlkCopy, sourceField, targetField); 
                }

                Helper.LogInfoToCache("6. Transfering data to sql server destination table.", this.LogInfoCache);

                oSqlBlkCopy.WriteToServer(oExcelData);
                Helper.LogInfoToCache("7. Processing Transfered data in sql server.", this.LogInfoCache);
                //Mark Data Transfer Flag
                DataImportHdrDAO oDataImportHdrDAO = new DataImportHdrDAO(this.CompanyUserInfo);
                oSubledgerDataImportInfo.IsDataTransfered = true;
                oDataImportHdrDAO.UpdateDataTransferStatus(oSubledgerDataImportInfo, oConnection, oTransaction);
            }
        }

        public void ProcessImportedSubledgerData(SubledgerDataImportInfo oSubledgerDataImportInfo, SqlConnection oConnection, SqlTransaction oTransaction)
        {
            SqlCommand oCommand = GetSubledgerDataProcessingCommand(oSubledgerDataImportInfo);
            oCommand.Connection = oConnection;
            oCommand.Transaction = oTransaction;
            try
            {
                oCommand.ExecuteNonQuery();
            }
            finally
            {
                oSubledgerDataImportInfo.ErrorMessageFromSqlServer = oCommand.Parameters["@errorMessageForServiceToLog"].Value.ToString(); ;
                oSubledgerDataImportInfo.ErrorMessageToSave = oCommand.Parameters["@errorMessageToSave"].Value.ToString();
                oSubledgerDataImportInfo.DataImportStatus = oCommand.Parameters["@importStatus"].Value.ToString();
                oSubledgerDataImportInfo.RecordsImported = Convert.ToInt32(oCommand.Parameters["@recordsImported"].Value.ToString());
                if (oCommand.Parameters["@retWarningReasonID"].Value == DBNull.Value)
                {
                    oSubledgerDataImportInfo.WarningReasonID = null;
                }
                else
                {
                    oSubledgerDataImportInfo.WarningReasonID = Convert.ToInt16(oCommand.Parameters["@retWarningReasonID"].Value.ToString());
                }
            }
        }

        public void ProcessImportedMultiversionSubledgerData(SubledgerDataImportInfo oSubledgerDataImportInfo, SqlConnection oConnection, SqlTransaction oTransaction)
        {
            SqlCommand oCommand = GetSubledgerDataProcessingCommand(oSubledgerDataImportInfo);
            SqlDataReader oDataReader = null;
            oCommand.Connection = oConnection;
            oCommand.Transaction = oTransaction;

            try
            {
                oDataReader = oCommand.ExecuteReader();
                oSubledgerDataImportInfo.UserAccountInfoCollection = new List<UserAccountInfo>();
                while (oDataReader.Read())
                {
                    MapUserAccountInfoObject(oDataReader, oSubledgerDataImportInfo.UserAccountInfoCollection);
                }
            }
            finally
            {
                if (oDataReader != null && !oDataReader.IsClosed)
                    oDataReader.Close();
                oSubledgerDataImportInfo.ErrorMessageFromSqlServer = oCommand.Parameters["@errorMessageForServiceToLog"].Value.ToString(); ;
                oSubledgerDataImportInfo.ErrorMessageToSave = oCommand.Parameters["@errorMessageToSave"].Value.ToString();
                oSubledgerDataImportInfo.DataImportStatus = oCommand.Parameters["@importStatus"].Value.ToString();
                oSubledgerDataImportInfo.RecordsImported = Convert.ToInt32(oCommand.Parameters["@recordsImported"].Value.ToString());
                if (oCommand.Parameters["@retWarningReasonID"].Value == DBNull.Value)
                {
                    oSubledgerDataImportInfo.WarningReasonID = null;
                }
                else
                {
                    oSubledgerDataImportInfo.WarningReasonID = Convert.ToInt16(oCommand.Parameters["@retWarningReasonID"].Value.ToString());
                }
            }
        }

        #region "Command Methods"
        private SqlCommand GetIsProcessingRequiredCommand()
        {
            SqlCommand oCommand = this.CreateCommand();
            oCommand.CommandType = CommandType.StoredProcedure;
            oCommand.CommandText = "usp_GET_SubledgerDataImportForProcessing";
            SqlParameterCollection cmdParamCollection = oCommand.Parameters;

            SqlParameter paramDataImportTypeId = new SqlParameter("@dataImportTypeId", Enums.DataImportType.SubledgerData);
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

        private SqlCommand GetSubledgerDataProcessingCommand(SubledgerDataImportInfo oSubledgerDataImportInfo)
        {
            SqlCommand oCommand = this.CreateCommand();
            oCommand.CommandType = CommandType.StoredProcedure;
            oCommand.CommandText = "usp_SRV_INS_ProcessSubledgerTransit";

            SqlParameterCollection cmdParamCollectionImport = oCommand.Parameters;

            SqlParameter paramCompanyID = new SqlParameter("@companyID", oSubledgerDataImportInfo.CompanyID);
            SqlParameter paramDataImportID = new SqlParameter("@dataImportID", oSubledgerDataImportInfo.DataImportID);
            SqlParameter paramAddedBy = new SqlParameter("@addedBy", oSubledgerDataImportInfo.AddedBy);
            SqlParameter paramDateAdded = new SqlParameter("@dateAdded", oSubledgerDataImportInfo.DateAdded);
            SqlParameter paramIsForceCommit = new SqlParameter("@isForceCommit", oSubledgerDataImportInfo.IsForceCommit);


            //****Added By Prafull on 17-May-2011

            SqlParameter paramRecStatusNotStartedID = new SqlParameter("@recStatusNotStartedID", (Int16)Enums.ReconciliationStatus.NotStarted);
            SqlParameter paramRecStatusInProgressID = new SqlParameter("@recStatusInProgressID", (Int16)Enums.ReconciliationStatus.InProgress);

            //*** Added By Prafull on 31-Aug-2011
            SqlParameter paramWarningReasonID = new SqlParameter();
            paramWarningReasonID.ParameterName = "@warningReasonID";
            if (oSubledgerDataImportInfo.WarningReasonID.HasValue)
                paramWarningReasonID.Value = oSubledgerDataImportInfo.WarningReasonID.Value;
            else
                paramWarningReasonID.Value = DBNull.Value;




            SqlParameter paramErrorMessageSRV = new SqlParameter("@errorMessageForServiceToLog", SqlDbType.VarChar, -1);
            paramErrorMessageSRV.Direction = ParameterDirection.Output;

            SqlParameter paramErrorMessageToSave = new SqlParameter("@errorMessageToSave", SqlDbType.NVarChar, -1);
            paramErrorMessageToSave.Direction = ParameterDirection.Output;

            SqlParameter paramImportStatus = new SqlParameter("@importStatus", SqlDbType.VarChar, 15);
            paramImportStatus.Direction = ParameterDirection.Output;

            SqlParameter paramRecordsImported = new SqlParameter("@recordsImported", SqlDbType.Int);
            paramRecordsImported.Direction = ParameterDirection.Output;

            SqlParameter paramRetWarningReasonID = new SqlParameter("@retWarningReasonID", SqlDbType.SmallInt);
            paramRetWarningReasonID.Direction = ParameterDirection.Output;



            cmdParamCollectionImport.Add(paramCompanyID);
            cmdParamCollectionImport.Add(paramDataImportID);
            cmdParamCollectionImport.Add(paramAddedBy);
            cmdParamCollectionImport.Add(paramDateAdded);
            cmdParamCollectionImport.Add(paramIsForceCommit);

            //****Added By Prafull on 17-May-2011
            cmdParamCollectionImport.Add(paramRecStatusNotStartedID);
            cmdParamCollectionImport.Add(paramRecStatusInProgressID);
            //*** Added By Prafull on 31-Aug-2011
            cmdParamCollectionImport.Add(paramWarningReasonID);

            //Get output parameters
            cmdParamCollectionImport.Add(paramErrorMessageSRV);
            cmdParamCollectionImport.Add(paramErrorMessageToSave);
            cmdParamCollectionImport.Add(paramImportStatus);
            cmdParamCollectionImport.Add(paramRecordsImported);
            //****Added By Prafull on 17-May-2011
            cmdParamCollectionImport.Add(paramRetWarningReasonID);

            return oCommand;
        }
        #endregion

        private void MapUserAccountInfoObject(SqlDataReader reader, List<UserAccountInfo> oUserAccountInfoCollection)
        {
            UserAccountInfo oUserAccountInfo;
            string EmailID = String.Empty;
            short RoleID = 0;
            int userID = 0;
            try
            {
                int ordinal = reader.GetOrdinal("Email");
                if (!reader.IsDBNull(ordinal))
                    EmailID = ((string)(reader.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = reader.GetOrdinal("RoleID");
                if (!reader.IsDBNull(ordinal))
                {
                    RoleID = ((short)(reader.GetValue(ordinal)));

                }
            }
            catch (Exception) { }

            try
            {
                int ordinal = reader.GetOrdinal("UserID");
                if (!reader.IsDBNull(ordinal))
                {
                    userID = ((int)(reader.GetValue(ordinal)));

                }
            }
            catch (Exception) { }


            oUserAccountInfo = (from o in oUserAccountInfoCollection
                                where o.EmailID == EmailID && o.RoleID.Value == RoleID && o.UserID.Value == userID
                                select o).FirstOrDefault();
            if (oUserAccountInfo == null)
            {
                oUserAccountInfo = new UserAccountInfo();
                oUserAccountInfo.EmailID = EmailID;
                oUserAccountInfo.RoleID = RoleID;
                oUserAccountInfo.UserID = userID;
                oUserAccountInfoCollection.Add(oUserAccountInfo);

            }

            try
            {
                int ordinal = reader.GetOrdinal("AccountInfo");
                if (!reader.IsDBNull(ordinal))
                {
                    string AcInfo = ((string)(reader.GetValue(ordinal)));
                    oUserAccountInfo.AccountInfoCollection.Add(AcInfo);
                }
            }
            catch (Exception) { }

            try
            {
                int ordinal = reader.GetOrdinal("DefaultLanguageID");
                if (!reader.IsDBNull(ordinal))
                {
                    oUserAccountInfo.DefaultLanguageID = ((int)(reader.GetValue(ordinal)));

                }
            }
            catch (Exception) { }

            

        }
    }
}
