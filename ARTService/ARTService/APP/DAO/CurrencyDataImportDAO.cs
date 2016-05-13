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
using SkyStem.ART.Client.Model;
using SkyStem.ART.Shared.Data;

namespace SkyStem.ART.Service.APP.DAO
{
    public class CurrencyDataImportDAO : DataImportHdrDAO
    {
        public CurrencyDataImportDAO(CompanyUserInfo oCompanyUserInfo)
            : base(oCompanyUserInfo)
        {
        }
        #region "Public Methods"
        public CurrencyDataImportInfo GetCurrencyDataImportForProcessing(DateTime dateRevised)
        {
            CurrencyDataImportInfo oEntity = new CurrencyDataImportInfo();
            if (this.IsDataImportProcessingRequired(Enums.DataImportType.CurrencyExchangeRateData))
            {
                this.GetDataImportForProcessing(oEntity, Enums.DataImportType.CurrencyExchangeRateData, dateRevised);
            }
            return oEntity;
        }

        //Copy information in Data Table to Destination Table in Sql Server
        public void CopyCurrencyDataFromExcelToSqlServer(DataTable dtExcelData, CurrencyDataImportInfo oCurrencyDataImportInfo, SqlConnection oConnection, SqlTransaction oTransaction)
        {
            DateTime dateAdded = DateTime.Now.Date;
            using (SqlBulkCopy oSqlBlkCopy = new SqlBulkCopy(oConnection, SqlBulkCopyOptions.Default, oTransaction))
            {
                Helper.LogInfoToCache("5. Mapping fields from source to destination.", this.LogInfoCache);
                //Mapping for Static Fields 
                oSqlBlkCopy.DestinationTableName = "ExchangeRateTransit";
                Helper.AddSqlBulkCopyMapping(dtExcelData, oSqlBlkCopy, CurrencyExchangeUploadConstants.AddedFields.COMPANYID, CurrencyExchangeUploadConstants.TransitFields.COMPANYID);
                Helper.AddSqlBulkCopyMapping(dtExcelData, oSqlBlkCopy, CurrencyExchangeUploadConstants.AddedFields.DATAIMPORTID, CurrencyExchangeUploadConstants.TransitFields.DATAIMPORTID);
                Helper.AddSqlBulkCopyMapping(dtExcelData, oSqlBlkCopy, CurrencyExchangeUploadConstants.AddedFields.EXCELROWNUMBER, CurrencyExchangeUploadConstants.TransitFields.EXCELROWNUMBER);
                Helper.AddSqlBulkCopyMapping(dtExcelData, oSqlBlkCopy, CurrencyExchangeUploadConstants.UploadFields.PERIODENDDATE, CurrencyExchangeUploadConstants.TransitFields.PERIODENDDATE);
                Helper.AddSqlBulkCopyMapping(dtExcelData, oSqlBlkCopy, CurrencyExchangeUploadConstants.UploadFields.FROMCURRENCYCODE, CurrencyExchangeUploadConstants.TransitFields.FROMCURRENCYCODE);
                Helper.AddSqlBulkCopyMapping(dtExcelData, oSqlBlkCopy, CurrencyExchangeUploadConstants.UploadFields.TOCURRENCYCODE, CurrencyExchangeUploadConstants.TransitFields.TOCURRENCYCODE);
                Helper.AddSqlBulkCopyMapping(dtExcelData, oSqlBlkCopy, CurrencyExchangeUploadConstants.UploadFields.RATE, CurrencyExchangeUploadConstants.TransitFields.RATE);

                Helper.LogInfoToCache("6. Transfering data to sql server destination table.", this.LogInfoCache);

                //Write Data to Sql Server destination Table
                oSqlBlkCopy.WriteToServer(dtExcelData);

                Helper.LogInfoToCache("7. Processing Transfered data in sql server.", this.LogInfoCache);

                //Mark Data Transfer Flag
                oCurrencyDataImportInfo.IsDataTransfered = true;
                this.UpdateDataTransferStatus(oCurrencyDataImportInfo, oConnection, oTransaction);
            }
        }

        //Process imported ExchangeRate
        public void ProcessImportedMultiVersionCurrencyData(CurrencyDataImportInfo oCurrencyDataImportInfo, SqlConnection oConnection, SqlTransaction oTransaction)
        {
            string xmlReturnString;
            SqlDataReader oDataReader = null;
            List<UserAccountInfo> oUserAccountInfoCollection;
            SqlCommand oCommand = this.GetProcessImportedMultiVersionCurrencyDataCommand(oCurrencyDataImportInfo);
            if (oConnection.State != ConnectionState.Open)
                oConnection.Open();
            oCommand.Connection = oConnection;
            oCommand.Transaction = oTransaction;
            //  oCommand.ExecuteNonQuery();
            try
            {
                Helper.LogInfoToCache(@"Begin: Multiversion CurrencyData Processing SP for DataImportID: " + oCurrencyDataImportInfo.DataImportID.ToString(), this.LogInfoCache);
                oDataReader = oCommand.ExecuteReader();
                Helper.LogInfoToCache(@"End: Multiversion CurrencyData Processing SP for DataImportID: " + oCurrencyDataImportInfo.DataImportID.ToString(), this.LogInfoCache);
                oUserAccountInfoCollection = new List<UserAccountInfo>();

                if (oDataReader.NextResult()) // Ignore Copy Item result
                {
                    while (oDataReader.Read())
                    {
                        this.MapUserAccountInfoObject(oDataReader, oUserAccountInfoCollection);
                        //this.MapAccountAttributeInfo(reader, oAccountStatusReportInfo);
                    }
                }
                oCurrencyDataImportInfo.UserAccountInfoCollection = oUserAccountInfoCollection;
            }
            finally
            {
                if (oDataReader != null && !oDataReader.IsClosed)
                    oDataReader.Close();

                Helper.LogInfoToCache(@"Read Return Value from Multiversion ExchangeRate Processing SP for DataImportID: " + oCurrencyDataImportInfo.DataImportID.ToString(), this.LogInfoCache);
                if (oCommand.Parameters["@ReturnValue"].Value != null)
                {
                    xmlReturnString = oCommand.Parameters["@ReturnValue"].Value.ToString();

                    //Deserialize returned info
                    ReturnValue oRetVal = DataImportHelper.DeSerializeReturnValue(xmlReturnString);

                    oCurrencyDataImportInfo.IsAlertRaised = false;

                    if (oRetVal != null)
                    {
                        oCurrencyDataImportInfo.ErrorMessageFromSqlServer = oRetVal.ErrorMessageForServiceToLog;
                        oCurrencyDataImportInfo.ErrorMessageToSave = oRetVal.ErrorMessageToSave;
                        oCurrencyDataImportInfo.DataImportStatus = oRetVal.ImportStatus;
                        oCurrencyDataImportInfo.ProfilingData = oRetVal.ProfilingData;

                        if (oRetVal.IsAlertRaised.HasValue)
                        {
                            oCurrencyDataImportInfo.IsAlertRaised = oRetVal.IsAlertRaised.Value;
                        }
                        if (oRetVal.WarningReasonID.HasValue)
                        {
                            oCurrencyDataImportInfo.WarningReasonID = oRetVal.WarningReasonID.Value;
                        }

                        if (oRetVal.RecordsImported.HasValue)
                        {
                            oCurrencyDataImportInfo.RecordsImported = oRetVal.RecordsImported.Value;
                        }
                        else
                        {
                            oCurrencyDataImportInfo.RecordsImported = 0;
                        }
                        oCurrencyDataImportInfo.OverridenAcctEmailID = oRetVal.OverridenEmailID;
                    }
                }
            }
        }
        #endregion

        #region "Private Methods"

        private SqlCommand GetProcessImportedMultiVersionCurrencyDataCommand(CurrencyDataImportInfo oCurrencyDataImportInfo)
        {
            SqlCommand oCommand = this.CreateCommand();
            oCommand.CommandType = CommandType.StoredProcedure;
            oCommand.CommandText = "usp_SRV_INS_ProcessExchangeRateTransitForImportMultiVersion";
            this.AddParamsToCommandForCurrencyDataImportProcessing(oCurrencyDataImportInfo, oCommand);
            return oCommand;
        }

        private void AddParamsToCommandForCurrencyDataImportProcessing(CurrencyDataImportInfo oCurrencyDataImportInfo, SqlCommand oCommand)
        {
            SqlParameterCollection cmdParamCollectionImport = oCommand.Parameters;

            SqlParameter paramCompanyID = new SqlParameter("@companyID", oCurrencyDataImportInfo.CompanyID);
            SqlParameter paramDataImportID = new SqlParameter("@dataImportID", oCurrencyDataImportInfo.DataImportID);
            SqlParameter paramAddedBy = new SqlParameter("@addedBy", oCurrencyDataImportInfo.AddedBy);
            SqlParameter paramDateAdded = new SqlParameter("@dateAdded", oCurrencyDataImportInfo.DateAdded);

            SqlParameter paramIsForceCommit = new SqlParameter("@isForceCommit", oCurrencyDataImportInfo.IsForceCommit);

            SqlParameter paramWarningReasonID = new SqlParameter();
            paramWarningReasonID.ParameterName = "@warningReasonID";
            if (oCurrencyDataImportInfo.WarningReasonID.HasValue)
                paramWarningReasonID.Value = oCurrencyDataImportInfo.WarningReasonID;
            else
                paramWarningReasonID.Value = DBNull.Value;

            SqlParameter paramReturnValue = new SqlParameter("@ReturnValue", SqlDbType.NVarChar, -1);
            paramReturnValue.Direction = ParameterDirection.Output;

            cmdParamCollectionImport.Add(paramCompanyID);
            cmdParamCollectionImport.Add(paramDataImportID);
            cmdParamCollectionImport.Add(paramAddedBy);
            cmdParamCollectionImport.Add(paramDateAdded);
            cmdParamCollectionImport.Add(paramIsForceCommit);
            cmdParamCollectionImport.Add(paramWarningReasonID);
            cmdParamCollectionImport.Add(paramReturnValue);
        }

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
        #endregion

        #region InsertWarning
        public void InsertDataImportWarning(CurrencyDataImportInfo oCurrencyDataImportInfo, SqlConnection oConnection, SqlTransaction oTransaction)
        {
            if (oCurrencyDataImportInfo.DataImportMessageDetailInfoList != null && oCurrencyDataImportInfo.DataImportMessageDetailInfoList.Count > 0)
            {
                SqlCommand oCommand = this.GetInsertWarningCommand(oCurrencyDataImportInfo);
                oCommand.Connection = oConnection;
                oCommand.Transaction = oTransaction;
                Helper.LogInfoToCache(@"Begin: Insert Warning Command for DataImportID: " + oCurrencyDataImportInfo.DataImportID.ToString(), this.LogInfoCache);
                oCommand.ExecuteNonQuery();
                Helper.LogInfoToCache(@"End: Insert Warning Command for DataImportID: " + oCurrencyDataImportInfo.DataImportID.ToString(), this.LogInfoCache);
                // Clear Warning
                oCurrencyDataImportInfo.DataImportMessageDetailInfoList = null;
            }
        }
        private SqlCommand GetInsertWarningCommand(CurrencyDataImportInfo oCurrencyDataImportInfo)
        {
            SqlCommand oCommand = this.CreateCommand();
            oCommand.CommandType = CommandType.StoredProcedure;
            oCommand.CommandText = "[dbo].[usp_INS_DataImportMessageDetail]";
            SqlParameterCollection cmdParamCollection = oCommand.Parameters;
            SqlParameter paramDataImportID = new SqlParameter("@dataImportID", oCurrencyDataImportInfo.DataImportID);
            SqlParameter paramDateAdded = new SqlParameter("@dateAdded", oCurrencyDataImportInfo.DateAdded);
            SqlParameter paramDataImportStatusMessage = new SqlParameter();
            paramDataImportStatusMessage.ParameterName = "@DataImportMessageDetailType";
            if (oCurrencyDataImportInfo.DataImportMessageDetailInfoList != null && oCurrencyDataImportInfo.DataImportMessageDetailInfoList.Count > 0)
                paramDataImportStatusMessage.Value = DataImportHelper.ConvertDataImportStatusMessageToDataTable(oCurrencyDataImportInfo.DataImportMessageDetailInfoList);
            cmdParamCollection.Add(paramDataImportStatusMessage);
            cmdParamCollection.Add(paramDataImportID);
            cmdParamCollection.Add(paramDateAdded);

            return oCommand;
        }
        #endregion
    }
}
