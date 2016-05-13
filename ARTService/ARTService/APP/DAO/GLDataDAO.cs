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

namespace SkyStem.ART.Service.APP.DAO
{
    public class GLDataDAO : DataImportHdrDAO
    {
        public GLDataDAO(CompanyUserInfo oCompanyUserInfo)
            : base(oCompanyUserInfo)
        {
        }
        #region "Public Methods"
        public GLDataImportInfo GetGLDataImportForProcessing(DateTime dateRevised)
        {
            GLDataImportInfo oEntity = new GLDataImportInfo();
            if (this.IsDataImportProcessingRequired(Enums.DataImportType.GLData))
            {
                this.GetDataImportForProcessing(oEntity, Enums.DataImportType.GLData, dateRevised);
            }
            return oEntity;
        }

        //Copy information in Data Table to Destination Table in Sql Server
        public void CopyGLDataFromExcelToSqlServer(DataTable dtExcelData, GLDataImportInfo oGLDataImportInfo, SqlConnection oConnection, SqlTransaction oTransaction)
        {
            DateTime dateAdded = DateTime.Now.Date;
            using (SqlBulkCopy oSqlBlkCopy = new SqlBulkCopy(oConnection, SqlBulkCopyOptions.Default, oTransaction))
            {
                Helper.LogInfoToCache("5. Mapping fields from source to destination.", this.LogInfoCache);
                //Mapping for Static Fields 
                oSqlBlkCopy.DestinationTableName = "GLDataTransit";
                Helper.AddSqlBulkCopyMapping(dtExcelData, oSqlBlkCopy, AddedGLDataImportFields.DATAIMPORTID, GLDataImportTransitFields.DATAIMPORTID);                
                Helper.AddSqlBulkCopyMapping(dtExcelData, oSqlBlkCopy, AddedGLDataImportFields.EXCELROWNUMBER, GLDataImportTransitFields.EXCELROWNUMBER);               
                Helper.AddSqlBulkCopyMapping(dtExcelData, oSqlBlkCopy, AddedGLDataImportFields.RECPERIODENDDATE, GLDataImportTransitFields.PERIODENDDATE);             
                Helper.AddSqlBulkCopyMapping(dtExcelData, oSqlBlkCopy, GLDataImportFields.COMPANY, GLDataImportTransitFields.COMPANY);            
                Helper.AddSqlBulkCopyMapping(dtExcelData, oSqlBlkCopy, GLDataImportFields.GLACCOUNTNUMBER, GLDataImportTransitFields.GLACCOUNTNUMBER);               
                Helper.AddSqlBulkCopyMapping(dtExcelData, oSqlBlkCopy, GLDataImportFields.GLACCOUNTNAME, GLDataImportTransitFields.GLACCOUNTNAME);               
                Helper.AddSqlBulkCopyMapping(dtExcelData, oSqlBlkCopy, GLDataImportFields.FSCAPTION, GLDataImportTransitFields.FSCAPTION);              
                Helper.AddSqlBulkCopyMapping(dtExcelData, oSqlBlkCopy, GLDataImportFields.ACCOUNTTYPE, GLDataImportTransitFields.ACCOUNTTYPE);                
                Helper.AddSqlBulkCopyMapping(dtExcelData, oSqlBlkCopy, GLDataImportFields.ISPROFITANDLOSS, GLDataImportTransitFields.ISPROFITANDLOSS);               
                Helper.AddSqlBulkCopyMapping(dtExcelData, oSqlBlkCopy, GLDataImportFields.BCCYCODE, GLDataImportTransitFields.BCCYCODE);              
                Helper.AddSqlBulkCopyMapping(dtExcelData, oSqlBlkCopy, GLDataImportFields.BALANCEBCCY, GLDataImportTransitFields.BALANCEBCCY);              
                Helper.AddSqlBulkCopyMapping(dtExcelData, oSqlBlkCopy, GLDataImportFields.BALANCERCCY, GLDataImportTransitFields.BALANCERCCY);               
                Helper.AddSqlBulkCopyMapping(dtExcelData, oSqlBlkCopy, GLDataImportFields.RCCYCODE, GLDataImportTransitFields.RCCYCODE);
                

                //Mapping for Key Fields
                string[] arrKeyFields = oGLDataImportInfo.KeyFields.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                for (int k = 0; k < arrKeyFields.Length; k++)
                {
                    string sourceField = arrKeyFields[k].ToString();
                    string targetField = "Key" + (k + 2).ToString();
                    Helper.AddSqlBulkCopyMapping(dtExcelData, oSqlBlkCopy, sourceField, targetField);                    
                }

                Helper.LogInfoToCache("6. Transfering data to sql server destination table.", this.LogInfoCache);

                //Write Data to Sql Server destination Table
                oSqlBlkCopy.WriteToServer(dtExcelData);

                Helper.LogInfoToCache("7. Processing Transfered data in sql server.", this.LogInfoCache);

                //Mark Data Transfer Flag
                oGLDataImportInfo.IsDataTransfered = true;
                this.UpdateDataTransferStatus(oGLDataImportInfo, oConnection, oTransaction);
            }
        }

        //Process imported GLData
        public void ProcessImportedGLData(GLDataImportInfo oGLDataImportInfo, SqlConnection oConnection, SqlTransaction oTransaction)
        {
            string xmlReturnString;
            SqlCommand oCommand = this.GetProcessImportedGLDataCommand(oGLDataImportInfo);
            oCommand.Connection = oConnection;
            oCommand.Transaction = oTransaction;
            try
            {
                Helper.LogInfoToCache(@"Begin: GLData Processing SP for DataImportID: " + oGLDataImportInfo.DataImportID.ToString(), this.LogInfoCache);
                oCommand.ExecuteNonQuery();
                Helper.LogInfoToCache(@"End: GLData Processing SP for DataImportID: " + oGLDataImportInfo.DataImportID.ToString(), this.LogInfoCache);
            }
            finally
            {
                //Get Return Value from Sql Server
                Helper.LogInfoToCache(@"Read Return Value from GLData Processing SP for DataImportID: " + oGLDataImportInfo.DataImportID.ToString(), this.LogInfoCache);
                if (oCommand.Parameters["@ReturnValue"].Value != null)
                {
                    xmlReturnString = oCommand.Parameters["@ReturnValue"].Value.ToString();

                    //Deserialize returned info
                    ReturnValue oRetVal = DataImportHelper.DeSerializeReturnValue(xmlReturnString);

                    oGLDataImportInfo.IsAlertRaised = false;

                    if (oRetVal != null)
                    {
                        oGLDataImportInfo.ErrorMessageFromSqlServer = oRetVal.ErrorMessageForServiceToLog;
                        oGLDataImportInfo.ErrorMessageToSave = oRetVal.ErrorMessageToSave;
                        oGLDataImportInfo.DataImportStatus = oRetVal.ImportStatus;
                        oGLDataImportInfo.ProfilingData = oRetVal.ProfilingData;

                        if (oRetVal.IsAlertRaised.HasValue)
                        {
                            oGLDataImportInfo.IsAlertRaised = oRetVal.IsAlertRaised.Value;
                        }
                        if (oRetVal.WarningReasonID.HasValue)
                        {
                            oGLDataImportInfo.WarningReasonID = oRetVal.WarningReasonID.Value;
                        }

                        if (oRetVal.RecordsImported.HasValue)
                        {
                            oGLDataImportInfo.RecordsImported = oRetVal.RecordsImported.Value;
                        }
                        else
                        {
                            oGLDataImportInfo.RecordsImported = 0;
                        }
                        oGLDataImportInfo.OverridenAcctEmailID = oRetVal.OverridenEmailID;
                    }
                }
            }
        }

        //Process imported GLData
        public void ProcessImportedMultiVersionGLData(GLDataImportInfo oGLDataImportInfo, SqlConnection oConnection, SqlTransaction oTransaction)
        {
            string xmlReturnString;
            SqlDataReader oDataReader = null;
            List<UserAccountInfo> oUserAccountInfoCollection;
            SqlCommand oCommand = this.GetProcessImportedMultiVersionGLDataCommand(oGLDataImportInfo);
            if (oConnection.State != ConnectionState.Open)
                oConnection.Open();
            oCommand.Connection = oConnection;
            oCommand.Transaction = oTransaction;
            //  oCommand.ExecuteNonQuery();
            try
            {
                Helper.LogInfoToCache(@"Begin: Multiversion GLData Processing SP for DataImportID: " + oGLDataImportInfo.DataImportID.ToString(), this.LogInfoCache);
                oDataReader = oCommand.ExecuteReader();
                Helper.LogInfoToCache(@"End: Multiversion GLData Processing SP for DataImportID: " + oGLDataImportInfo.DataImportID.ToString(), this.LogInfoCache);
                oUserAccountInfoCollection = new List<UserAccountInfo>();
                while (oDataReader.Read())
                {
                    this.MapUserAccountInfoObject(oDataReader, oUserAccountInfoCollection);
                    //this.MapAccountAttributeInfo(reader, oAccountStatusReportInfo);
                }
                oGLDataImportInfo.UserAccountInfoCollection = oUserAccountInfoCollection;
            }
            finally
            {
                if (oDataReader != null && !oDataReader.IsClosed)
                    oDataReader.Close();

                Helper.LogInfoToCache(@"Read Return Value from Multiversion GLData Processing SP for DataImportID: " + oGLDataImportInfo.DataImportID.ToString(), this.LogInfoCache);
                if (oCommand.Parameters["@ReturnValue"].Value != null)
                {
                    xmlReturnString = oCommand.Parameters["@ReturnValue"].Value.ToString();

                    //Deserialize returned info
                    ReturnValue oRetVal = DataImportHelper.DeSerializeReturnValue(xmlReturnString);

                    oGLDataImportInfo.IsAlertRaised = false;

                    if (oRetVal != null)
                    {
                        oGLDataImportInfo.ErrorMessageFromSqlServer = oRetVal.ErrorMessageForServiceToLog;
                        oGLDataImportInfo.ErrorMessageToSave = oRetVal.ErrorMessageToSave;
                        oGLDataImportInfo.DataImportStatus = oRetVal.ImportStatus;
                        oGLDataImportInfo.ProfilingData = oRetVal.ProfilingData;

                        if (oRetVal.IsAlertRaised.HasValue)
                        {
                            oGLDataImportInfo.IsAlertRaised = oRetVal.IsAlertRaised.Value;
                        }
                        if (oRetVal.WarningReasonID.HasValue)
                        {
                            oGLDataImportInfo.WarningReasonID = oRetVal.WarningReasonID.Value;
                        }

                        if (oRetVal.RecordsImported.HasValue)
                        {
                            oGLDataImportInfo.RecordsImported = oRetVal.RecordsImported.Value;
                        }
                        else
                        {
                            oGLDataImportInfo.RecordsImported = 0;
                        }
                        oGLDataImportInfo.OverridenAcctEmailID = oRetVal.OverridenEmailID;
                    }
                }
            }
        }
        #endregion


        #region "Private Methods"
        private SqlCommand GetProcessImportedGLDataCommand(GLDataImportInfo oGLDataImportInfo)
        {
            SqlCommand oCommand = this.CreateCommand();
            oCommand.CommandType = CommandType.StoredProcedure;
            oCommand.CommandText = "usp_SRV_INS_ProcessGLDataTransitForImport";
            this.AddParamsToCommandForGLDataImportProcessing(oGLDataImportInfo, oCommand);
            return oCommand;
        }

        private SqlCommand GetProcessImportedMultiVersionGLDataCommand(GLDataImportInfo oGLDataImportInfo)
        {
            SqlCommand oCommand = this.CreateCommand();
            oCommand.CommandType = CommandType.StoredProcedure;
            oCommand.CommandText = "usp_SRV_INS_ProcessGLDataTransitForImportMultiVersion";
            this.AddParamsToCommandForGLDataImportProcessing(oGLDataImportInfo, oCommand);
            return oCommand;
        }

        private void AddParamsToCommandForGLDataImportProcessing(GLDataImportInfo oGLDataImportInfo, SqlCommand oCommand)
        {
            SqlParameterCollection cmdParamCollectionImport = oCommand.Parameters;

            SqlParameter paramCompanyID = new SqlParameter("@companyID", oGLDataImportInfo.CompanyID);
            SqlParameter paramDataImportID = new SqlParameter("@dataImportID", oGLDataImportInfo.DataImportID);
            SqlParameter paramAddedBy = new SqlParameter("@addedBy", oGLDataImportInfo.AddedBy);
            SqlParameter paramDateAdded = new SqlParameter("@dateAdded", oGLDataImportInfo.DateAdded);

            SqlParameter paramIsForceCommit = new SqlParameter("@isForceCommit", oGLDataImportInfo.IsForceCommit);



            SqlParameter paramWarningReasonID = new SqlParameter();
            paramWarningReasonID.ParameterName = "@warningReasonID";
            if (oGLDataImportInfo.WarningReasonID.HasValue)
                paramWarningReasonID.Value = oGLDataImportInfo.WarningReasonID;
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
        public void InsertDataImportWarning(GLDataImportInfo oGLDataImportInfo, SqlConnection oConnection, SqlTransaction oTransaction)
        {
            if (oGLDataImportInfo.DataImportMessageDetailInfoList != null && oGLDataImportInfo.DataImportMessageDetailInfoList.Count > 0)
            {
                SqlCommand oCommand = this.GetInsertWarningCommand(oGLDataImportInfo);
                oCommand.Connection = oConnection;
                oCommand.Transaction = oTransaction;
                Helper.LogInfoToCache(@"Begin: Insert Warning Command for DataImportID: " + oGLDataImportInfo.DataImportID.ToString(), this.LogInfoCache);
                oCommand.ExecuteNonQuery();
                Helper.LogInfoToCache(@"End: Insert Warning Command for DataImportID: " + oGLDataImportInfo.DataImportID.ToString(), this.LogInfoCache);
                // Clear Warning
                oGLDataImportInfo.DataImportMessageDetailInfoList = null;
            }
        }
        private SqlCommand GetInsertWarningCommand(GLDataImportInfo oGLDataImportInfo)
        {
            SqlCommand oCommand = this.CreateCommand();
            oCommand.CommandType = CommandType.StoredProcedure;
            oCommand.CommandText = "[dbo].[usp_INS_DataImportMessageDetail]";
            SqlParameterCollection cmdParamCollection = oCommand.Parameters;
            SqlParameter paramDataImportID = new SqlParameter("@dataImportID", oGLDataImportInfo.DataImportID);
            SqlParameter paramDateAdded = new SqlParameter("@dateAdded", oGLDataImportInfo.DateAdded);
            SqlParameter paramDataImportStatusMessage = new SqlParameter();
            paramDataImportStatusMessage.ParameterName = "@DataImportMessageDetailType";
            if (oGLDataImportInfo.DataImportMessageDetailInfoList != null && oGLDataImportInfo.DataImportMessageDetailInfoList.Count > 0)
                paramDataImportStatusMessage.Value = DataImportHelper.ConvertDataImportStatusMessageToDataTable(oGLDataImportInfo.DataImportMessageDetailInfoList);
            cmdParamCollection.Add(paramDataImportStatusMessage);
            cmdParamCollection.Add(paramDataImportID);
            cmdParamCollection.Add(paramDateAdded);         
          
            return oCommand;
        }
        #endregion

    }

}
