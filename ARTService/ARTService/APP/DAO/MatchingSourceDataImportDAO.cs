using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using System.Xml;
using SkyStem.ART.Service.Model;
using System.Data.SqlClient;
using System.Data;
using SkyStem.ART.Service.Utility;
using SkyStem.ART.Service.Data;
using SkyStem.ART.Client.Model.CompanyDatabase;


namespace SkyStem.ART.Service.APP.DAO
{
    public class MatchingSourceDataImportDAO : AbstractDAO
    {
        #region "Column Names"
        public static readonly string COLUMN_MATCHINGSOURCEDATAIMPORTID = "MatchingSourceDataImportID";
        public static readonly string COLUMN_MATCHINGSOURCENAME = "MatchingSourceName";
        public static readonly string COLUMN_FILENAME = "FileName";
        public static readonly string COLUMN_PHYSICALPATH = "PhysicalPath";
        public static readonly string COLUMN_FILESIZE = "FileSize";
        public static readonly string COLUMN_MATCHINGSOURCETYPEID = "MatchingSourceTypeID";
        public static readonly string COLUMN_RECPERIODID = "RecPeriodID";
        public static readonly string COLUMN_DATAIMPORTSTATUSID = "DataImportStatusID";

        public static readonly string COLUMN_RECORDSIMPORTED = "RecordsImported";
        public static readonly string COLUMN_RECITEMCREATEDCOUNT = "RecItemCreatedCount";
        public static readonly string COLUMN_ISFORCECOMMIT = "IsForceCommit";
        public static readonly string COLUMN_FORCECOMMITDATE = "ForceCommitDate";
        public static readonly string COLUMN_USERID = "UserID";
        public static readonly string COLUMN_ROLEID = "RoleID";
        public static readonly string COLUMN_LANGUAGEID = "LanguageID";
        public static readonly string COLUMN_MESSAGE = "Message";
        public static readonly string COLUMN_ISACTIVE = "IsActive";
        public static readonly string COLUMN_DATEADDED = "DateAdded";
        public static readonly string COLUMN_ADDEDBY = "AddedBy";
        public static readonly string COLUMN_DATEREVISED = "DateRevised";
        public static readonly string COLUMN_REVISEDBY = "RevisedBy";

        public static readonly string COLUMN_COMPANYID = "CompanyID";

        public static readonly string COLUMN_KEYFIELDS = "KeyFields";

        public static readonly string COLUMN_MATCHINGSOURCECOLUMNXML = "MatchingSourceColumnXML";
        public static readonly string COLUMN_MATCHINGSOURCECOLUMNS = "MatchingSourceColumns";
        public static readonly string COLUMN_ACCOUNT_UNIQUE_SUBSET_KEYS = "AccountUniqueSubSetKeys";
        #endregion

        public MatchingSourceDataImportDAO(CompanyUserInfo oCompanyUserInfo)
            : base(oCompanyUserInfo)
        {
        }

        #region "Public Methods"
        public List<MatchingSourceDataImportHdrInfo> GetMatchingSourceDataImportHdrForProcessing()
        {
            SqlConnection oConnection = null;
            SqlCommand oCommand = null;
            SqlDataReader reader = null;
            SqlTransaction oTrans = null;
            List<MatchingSourceDataImportHdrInfo> oListMatchingSourceDataImportHdr = null;
            try
            {
                oConnection = this.GetConnection();

                oCommand = this.GetIsProcessingRequiredForMatchingDataImportCommand();
                oCommand.Connection = oConnection;
                oCommand.Connection.Open();
                oTrans = oConnection.BeginTransaction();
                oCommand.Transaction = oTrans;
                reader = oCommand.ExecuteReader();
                oListMatchingSourceDataImportHdr = this.MapAllObjects(reader);
                reader.Close();
                oTrans.Commit();
                oTrans.Dispose();
                oTrans = null;
                return oListMatchingSourceDataImportHdr;
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
                if (oConnection != null && oConnection.State != ConnectionState.Closed)
                    oConnection.Close();
            }
        }

        public void UpdateMatchingSourceDataImportHDR(SqlConnection oConnection, long matchingSourceDataImportID
                            , string errorMessageToSave, string dataImportStatus, int recordsImported
                            , string revisedBy, DateTime dateRevised)
        {
            SqlTransaction oTransaction = null;
            SqlCommand oCommand = null;

            try
            {
                if (oConnection.State != ConnectionState.Open)
                    oConnection.Open();
                oTransaction = oConnection.BeginTransaction();
                oCommand = GetMatchingSourceDataImportHDRUpdateCommand(matchingSourceDataImportID, errorMessageToSave, dataImportStatus
                    , recordsImported, revisedBy, dateRevised);
                oCommand.Connection = oConnection;
                oCommand.Transaction = oTransaction;

                oCommand.ExecuteNonQuery();
                oTransaction.Commit();
                oTransaction.Dispose();
                oTransaction = null;
                Helper.LogInfo(" MatchingSourceDataImportHdr Updated successfully for MatchingSourceDataImportID: ." + matchingSourceDataImportID.ToString(), this.CompanyUserInfo );
            }
            catch (Exception ex)
            {
                if (oTransaction != null)
                    oTransaction.Rollback();
                Helper.LogError("Error:: Error while updating MatchingSourceDataImportHDR", this.CompanyUserInfo);
                Helper.LogError(ex, this.CompanyUserInfo);
            }
        }

        public void SaveMatchingSourceDataObjectList(List<MatchingSourceDataImportHdrInfo> oMatchingSourceDataImportHdrList)
        {
            foreach (MatchingSourceDataImportHdrInfo oMatchingSourceDataImportHdrInfo in oMatchingSourceDataImportHdrList)
            {
                this.SaveMatchingSourceDataObject(oMatchingSourceDataImportHdrInfo);
            }
        }

        public void SaveMatchingSourceDataObject(MatchingSourceDataImportHdrInfo oMatchingSourceDataImportHdr)
        {
            SqlConnection oConnection = null;
            SqlCommand oCommand = null;
            SqlTransaction oTrans = null;
            string errorMessageFromSqlServer = "";
            string errorMessageToSave = "";
            string dataImportStatus = "";
            int recordsImported = -1;

            try
            {
                oConnection = this.GetConnection();
                oConnection.Open();
                if (oMatchingSourceDataImportHdr.HasError.HasValue && !oMatchingSourceDataImportHdr.HasError.Value)
                {
                    oTrans = oConnection.BeginTransaction();
                    if (oMatchingSourceDataImportHdr.MatchingSourceTypeID.Value == (short)Enums.DataImportType.GLTBS)
                    {
                        Helper.LogInfo("Start Data transfer to sql server.", this.CompanyUserInfo );
                        this.TransferDataToSqlServer(oMatchingSourceDataImportHdr, oConnection, oTrans);
                        Helper.LogInfo("Data transfer to sql server complete.", this.CompanyUserInfo);
                    }
                    if (oConnection.State != ConnectionState.Open)
                        oConnection.Open();

                    oCommand = this.GetMatchingSourceDataObjectCommand(oMatchingSourceDataImportHdr);
                    oCommand.Connection = oConnection;
                    oCommand.Transaction = oTrans;

                    int retVal = oCommand.ExecuteNonQuery();

                    string xmlReturnString = oCommand.Parameters["@ReturnValue"].Value.ToString();

                    ReturnValue oRetVal = ReturnValue.DeSerialize(xmlReturnString);

                    errorMessageFromSqlServer = oRetVal.ErrorMessageForServiceToLog;
                    errorMessageToSave = oRetVal.ErrorMessageToSave;
                    dataImportStatus = oRetVal.ImportStatus;
                    recordsImported = oRetVal.RecordsImported.Value;

                    Helper.LogInfo("8. Data Processing Complete.", this.CompanyUserInfo );
                    Helper.LogInfo(" - Status: " + dataImportStatus, this.CompanyUserInfo);
                    Helper.LogInfo(" - Message: " + errorMessageFromSqlServer, this.CompanyUserInfo);
                    Helper.LogInfo(" - Records Imported: " + recordsImported.ToString(), this.CompanyUserInfo);

                    //Raise exception if dataImportStatus = "FAIL". This exception message will be logged into logfile
                    if (oRetVal.ImportStatus == DataImportStatus.DATAIMPORTFAIL || oRetVal.ImportStatus == DataImportStatus.DATAIMPORTSEVEREWARNING)
                        throw new Exception(errorMessageToSave);
                    else
                    {
                        oTrans.Commit();
                        oTrans.Dispose();
                        oTrans = null;
                    }

                }
                else
                {
                    errorMessageFromSqlServer = "";
                    errorMessageToSave = oMatchingSourceDataImportHdr.Message;
                    dataImportStatus = DataImportStatus.DATAIMPORTFAIL;
                    recordsImported = 0;
                }
            }
            catch (Exception ex)
            {
                if (oTrans != null)
                    oTrans.Rollback();
                Helper.LogError(ex.Message, this.CompanyUserInfo);
            }
            finally
            {
                try
                {
                    this.UpdateMatchingSourceDataImportHDR(oConnection, oMatchingSourceDataImportHdr.MatchingSourceDataImportID.Value
                        , errorMessageToSave, dataImportStatus, recordsImported, oMatchingSourceDataImportHdr.AddedBy, oMatchingSourceDataImportHdr.DateRevised);
                }
                catch (Exception ex)
                {
                    Helper.LogError(ex.Message, this.CompanyUserInfo);
                }
                if (oConnection != null && oConnection.State != ConnectionState.Closed)
                {
                    oConnection.Dispose();
                }
            }

        }
        #endregion



        #region "Private Methods"

        private List<T> DeSerializeList<T>(string listXML, string rootNode)
        {
            XmlSerializer xmlSerial = null;
            StringReader strReader = null;
            XmlTextReader txtReader = null;
            try
            {
                if (string.IsNullOrEmpty(rootNode))
                {
                    xmlSerial = new XmlSerializer(typeof(List<T>));
                }
                else
                {
                    XmlAttributeOverrides overrideAttr = new XmlAttributeOverrides();
                    XmlAttributes attr = new XmlAttributes();
                    attr.XmlRoot = new XmlRootAttribute(rootNode);
                    overrideAttr.Add(typeof(List<T>), attr);
                    xmlSerial = new XmlSerializer(typeof(List<T>), overrideAttr);

                }
                strReader = new StringReader(listXML);
                txtReader = new XmlTextReader(strReader);
                return (List<T>)xmlSerial.Deserialize(txtReader);
            }
            finally
            {
                if (txtReader != null)
                    txtReader.Close();
                if (strReader != null)
                    strReader.Close();
            }
        }

        private SqlCommand GetIsProcessingRequiredForMatchingDataImportCommand()
        {
            SqlCommand oCommand = this.CreateCommand();
            oCommand.CommandType = CommandType.StoredProcedure;
            oCommand.CommandText = "Matching.usp_SVC_SEL_MatchingDataImportForProcessing";

            SqlParameterCollection cmdParamCollection = oCommand.Parameters;

            SqlParameter paramDataImportStatusId = new SqlParameter("@dataImportStatusID", Enums.DataImportStatus.ToBeProcessed);
            SqlParameter paramWarningDataImportStatusId = new SqlParameter("@warningDataImportStatusID", Enums.DataImportStatus.Warning);
            SqlParameter paramProcessingDataImportStatusId = new SqlParameter("@processingDataImportStatusID", Enums.DataImportStatus.Processing);
            SqlParameter paramDateRevised = new SqlParameter("@dateRevised", Convert.ToDateTime(Helper.GetDateTime()));
            SqlParameter paramErrorMessageSRV = new SqlParameter("@errorMessageForServiceToLog", SqlDbType.VarChar, 8000);
            paramErrorMessageSRV.Direction = ParameterDirection.Output;

            SqlParameter paramReturnValue = new SqlParameter("@returnValue", SqlDbType.Int);
            paramReturnValue.Direction = ParameterDirection.ReturnValue;

            cmdParamCollection.Add(paramDataImportStatusId);
            cmdParamCollection.Add(paramProcessingDataImportStatusId);
            cmdParamCollection.Add(paramWarningDataImportStatusId);
            cmdParamCollection.Add(paramDateRevised);
            cmdParamCollection.Add(paramErrorMessageSRV);
            cmdParamCollection.Add(paramReturnValue);

            return oCommand;
        }

        private SqlCommand GetMatchingSourceDataImportHDRUpdateCommand(long matchingSourceDataImportID,
                                string errorMessageToSave, string dataImportStatus, int recordsImported
                                , string revisedBy, DateTime dateRevised)
        {
            SqlCommand oCommand = this.CreateCommand();
            oCommand.CommandType = CommandType.StoredProcedure;
            oCommand.CommandText = "Matching.usp_UPD_MatchingSourceDataImportHdr";

            SqlParameterCollection cmdParamCollection = oCommand.Parameters;
            SqlParameter paramDataImportID = new SqlParameter("@matchingSourceDataImportID", matchingSourceDataImportID);

            SqlParameter paramErrorMessageToSave = new SqlParameter("@errorMessageToSave", SqlDbType.NVarChar, -1);
            paramErrorMessageToSave.Value = errorMessageToSave;

            SqlParameter paramImportStatus = new SqlParameter("@dataImportStatusID", SqlDbType.SmallInt);

            SqlParameter paramRecordsImported = new SqlParameter("@recordsImported", SqlDbType.Int);
            paramRecordsImported.Value = recordsImported;

            SqlParameter paramRevisedBy = new SqlParameter("@RevisedBy", revisedBy);
            SqlParameter paramDateRevised = new SqlParameter("@dateRevised", dateRevised);


            if (dataImportStatus == "FAIL")
                paramImportStatus.Value = (short)Enums.DataImportStatus.Failure;
            if (dataImportStatus == "WARNING")
                paramImportStatus.Value = (short)Enums.DataImportStatus.Warning;
            if (dataImportStatus == "SUCCESS")
                paramImportStatus.Value = (short)Enums.DataImportStatus.Success;


            cmdParamCollection.Add(paramDataImportID);
            cmdParamCollection.Add(paramErrorMessageToSave);
            cmdParamCollection.Add(paramImportStatus);
            cmdParamCollection.Add(paramRecordsImported);
            cmdParamCollection.Add(paramRevisedBy);
            cmdParamCollection.Add(paramDateRevised);

            return oCommand;
        }


        private SqlCommand GetMatchingSourceDataObjectCommand(MatchingSourceDataImportHdrInfo oMatchingSourceDataImportHdr)
        {
            SqlCommand oCommand = this.CreateCommand();
            oCommand.CommandType = CommandType.StoredProcedure;
            oCommand.CommandText = "Matching.usp_SVC_SAV_MatchingSourceDataImport";

            SqlParameterCollection cmdParamCollection = oCommand.Parameters;
            SqlParameter paramDataImportID = new SqlParameter("@MatchingSourceDataImportID", oMatchingSourceDataImportHdr.MatchingSourceDataImportID);
            SqlParameter paramRecordsImported = new SqlParameter("@MatchingSourceRecordsImported", oMatchingSourceDataImportHdr.RecordsImported.Value);

            SqlParameter paramMatchingSourceXML = new SqlParameter("@MatchingSourceXML", oMatchingSourceDataImportHdr.MatchingSourceData.XMLData);
            SqlParameter paramMatchingSourceSchemaXML = new SqlParameter("@MatchingSourceSchemaXML", oMatchingSourceDataImportHdr.MatchingSourceData.TableXMLSchema);

            SqlParameter paramRevisedBy = new SqlParameter("@RevisedBy", oMatchingSourceDataImportHdr.RevisedBy);
            SqlParameter paramDateRevised = new SqlParameter("@DateRevised", oMatchingSourceDataImportHdr.DateRevised);

            SqlParameter paramReturnValue = new SqlParameter("@ReturnValue", SqlDbType.NVarChar, -1);
            paramReturnValue.Direction = ParameterDirection.Output;

            cmdParamCollection.Add(paramDataImportID);
            cmdParamCollection.Add(paramRecordsImported);
            cmdParamCollection.Add(paramMatchingSourceXML);
            cmdParamCollection.Add(paramMatchingSourceSchemaXML);
            cmdParamCollection.Add(paramRevisedBy);
            cmdParamCollection.Add(paramDateRevised);
            cmdParamCollection.Add(paramReturnValue);

            return oCommand;
        }

        private MatchingSourceDataImportHdrInfo MapObject(SqlDataReader reader)
        {
            MatchingSourceDataImportHdrInfo oEntity = new MatchingSourceDataImportHdrInfo();
            int ordinal = -1;
            if (reader.HasRows)
            {
                try
                {
                    ordinal = reader.GetOrdinal(COLUMN_MATCHINGSOURCEDATAIMPORTID);
                    if (!reader.IsDBNull(ordinal))
                        oEntity.MatchingSourceDataImportID = reader.GetInt64(ordinal);
                }
                catch (IndexOutOfRangeException) { }
                catch (Exception) { }

                try
                {
                    ordinal = reader.GetOrdinal(COLUMN_MATCHINGSOURCENAME);
                    if (!reader.IsDBNull(ordinal))
                        oEntity.MatchingSourceName = reader.GetString(ordinal);
                }
                catch (IndexOutOfRangeException) { }
                catch (Exception) { }

                try
                {
                    ordinal = reader.GetOrdinal(COLUMN_FILENAME);
                    if (!reader.IsDBNull(ordinal))
                        oEntity.FileName = reader.GetString(ordinal);
                }
                catch (IndexOutOfRangeException) { }
                catch (Exception) { }

                try
                {
                    ordinal = reader.GetOrdinal(COLUMN_PHYSICALPATH);
                    if (!reader.IsDBNull(ordinal))
                        oEntity.PhysicalPath = reader.GetString(ordinal);
                }
                catch (IndexOutOfRangeException) { }
                catch (Exception) { }

                try
                {
                    ordinal = reader.GetOrdinal(COLUMN_FILESIZE);
                    if (!reader.IsDBNull(ordinal))
                        oEntity.FileSize = reader.GetDecimal(ordinal);
                }
                catch (IndexOutOfRangeException) { }
                catch (Exception) { }

                try
                {
                    ordinal = reader.GetOrdinal(COLUMN_MATCHINGSOURCETYPEID);
                    if (!reader.IsDBNull(ordinal))
                        oEntity.MatchingSourceTypeID = reader.GetInt16(ordinal);
                }
                catch (IndexOutOfRangeException) { }
                catch (Exception) { }

                try
                {
                    ordinal = reader.GetOrdinal(COLUMN_RECPERIODID);
                    if (!reader.IsDBNull(ordinal))
                        oEntity.RecPeriodID = reader.GetInt32(ordinal);
                }
                catch (IndexOutOfRangeException) { }
                catch (Exception) { }

                try
                {
                    ordinal = reader.GetOrdinal(COLUMN_DATAIMPORTSTATUSID);
                    if (!reader.IsDBNull(ordinal))
                        oEntity.DataImportStatusID = reader.GetInt16(ordinal);
                }
                catch (IndexOutOfRangeException) { }
                catch (Exception) { }

                try
                {
                    ordinal = reader.GetOrdinal(COLUMN_RECORDSIMPORTED);
                    if (!reader.IsDBNull(ordinal))
                        oEntity.RecordsImported = reader.GetInt32(ordinal);
                }
                catch (IndexOutOfRangeException) { }
                catch (Exception) { }

                try
                {
                    ordinal = reader.GetOrdinal(COLUMN_RECITEMCREATEDCOUNT);
                    if (!reader.IsDBNull(ordinal))
                        oEntity.RecItemCreatedCount = reader.GetInt32(ordinal);
                }
                catch (IndexOutOfRangeException) { }
                catch (Exception) { }

                try
                {
                    ordinal = reader.GetOrdinal(COLUMN_ISFORCECOMMIT);
                    if (!reader.IsDBNull(ordinal))
                        oEntity.IsForceCommit = reader.GetBoolean(ordinal);
                }
                catch (IndexOutOfRangeException) { }
                catch (Exception) { }

                try
                {
                    ordinal = reader.GetOrdinal(COLUMN_FORCECOMMITDATE);
                    if (!reader.IsDBNull(ordinal))
                        oEntity.ForceCommitDate = reader.GetDateTime(ordinal);
                }
                catch (IndexOutOfRangeException) { }
                catch (Exception) { }

                try
                {
                    ordinal = reader.GetOrdinal(COLUMN_USERID);
                    if (!reader.IsDBNull(ordinal))
                        oEntity.UserID = reader.GetInt32(ordinal);
                }
                catch (IndexOutOfRangeException) { }
                catch (Exception) { }

                try
                {
                    ordinal = reader.GetOrdinal(COLUMN_ROLEID);
                    if (!reader.IsDBNull(ordinal))
                        oEntity.RoleID = reader.GetInt16(ordinal);
                }
                catch (IndexOutOfRangeException) { }
                catch (Exception) { }

                try
                {
                    ordinal = reader.GetOrdinal(COLUMN_LANGUAGEID);
                    if (!reader.IsDBNull(ordinal))
                        oEntity.LanguageID = reader.GetInt32(ordinal);
                }
                catch (IndexOutOfRangeException) { }
                catch (Exception) { }
                try
                {
                    ordinal = reader.GetOrdinal(COLUMN_MESSAGE);
                    if (!reader.IsDBNull(ordinal))
                        oEntity.Message = reader.GetString(ordinal);
                }
                catch (IndexOutOfRangeException) { }
                catch (Exception) { }

                try
                {
                    ordinal = reader.GetOrdinal(COLUMN_ISACTIVE);
                    if (!reader.IsDBNull(ordinal))
                        oEntity.IsActive = reader.GetBoolean(ordinal);
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
                    ordinal = reader.GetOrdinal(COLUMN_REVISEDBY);
                    if (!reader.IsDBNull(ordinal))
                        oEntity.RevisedBy = reader.GetString(ordinal);
                }
                catch (IndexOutOfRangeException) { }
                catch (Exception) { }

                try
                {
                    ordinal = reader.GetOrdinal(COLUMN_DATEREVISED);
                    if (!reader.IsDBNull(ordinal))
                        oEntity.DateRevised = reader.GetDateTime(ordinal);
                }
                catch (IndexOutOfRangeException) { }
                catch (Exception) { }

                try
                {
                    ordinal = reader.GetOrdinal(COLUMN_MATCHINGSOURCECOLUMNXML);
                    if (!reader.IsDBNull(ordinal))
                        oEntity.MatchingSourceColumnXML = reader.GetString(ordinal);
                }
                catch (IndexOutOfRangeException) { }
                catch (Exception) { }

                try
                {
                    oEntity.MatchingSourceColumns = this.DeSerializeList<MatchingSourceColumnInfo>(oEntity.MatchingSourceColumnXML, ServiceConstants.MATCHING_SOURCE_COLUMN_LIST_XML);
                }
                catch (IndexOutOfRangeException) { }
                catch (Exception) { }

                try
                {
                    ordinal = reader.GetOrdinal(COLUMN_COMPANYID);
                    if (!reader.IsDBNull(ordinal))
                        oEntity.CompanyID = reader.GetInt32(ordinal);
                }
                catch (IndexOutOfRangeException) { }
                catch (Exception) { }

                try
                {
                    ordinal = reader.GetOrdinal(COLUMN_KEYFIELDS);
                    if (!reader.IsDBNull(ordinal))
                        oEntity.KeyFields = reader.GetString(ordinal);
                }
                catch (IndexOutOfRangeException) { }
                catch (Exception) { }

                //Account
                try
                {
                    ordinal = reader.GetOrdinal(COLUMN_ACCOUNT_UNIQUE_SUBSET_KEYS);
                    if (!reader.IsDBNull(ordinal))
                        oEntity.AccountUniqueSubSetKeys = reader.GetString(ordinal);
                }
                catch (IndexOutOfRangeException) { }
                catch (Exception) { }
            }
            return oEntity;

        }

        private List<MatchingSourceDataImportHdrInfo> MapAllObjects(SqlDataReader reader)
        {
            List<MatchingSourceDataImportHdrInfo> oListMatchingSourceDataImportHdrInfo = null;
            if (reader.HasRows)
            {
                oListMatchingSourceDataImportHdrInfo = new List<MatchingSourceDataImportHdrInfo>();
                while (reader.Read())
                {
                    oListMatchingSourceDataImportHdrInfo.Add(this.MapObject(reader));
                }
            }
            return oListMatchingSourceDataImportHdrInfo;
        }

        //Transfers data from data table to sql server as per mapped fields
        private void TransferDataToSqlServer(MatchingSourceDataImportHdrInfo oMatchingSourceDataImportHdr, SqlConnection oConnection, SqlTransaction oTrans)
        {
            using (SqlBulkCopy oSqlBlkCopy = new SqlBulkCopy(oConnection, SqlBulkCopyOptions.Default, oTrans))
            {
                Helper.LogInfo("Mapping fields from source to destination.", this.CompanyUserInfo);

                oSqlBlkCopy.DestinationTableName = "Matching.MatchingSourceAccountTransit";

                //Map known fields
                oSqlBlkCopy.ColumnMappings.Add(AddedGLTBSAccountFields.MATCHING_SOURCE_DATAIMPORT_ID, MatchingSourceDataTransitFields.MATCHING_SOURCE_DATAIMPORTID);

                if (oMatchingSourceDataImportHdr.AccountDataTable.Columns.Contains(GLDataImportFields.FSCAPTION))
                    oSqlBlkCopy.ColumnMappings.Add(GLDataImportFields.FSCAPTION, MatchingSourceDataTransitFields.FS_CAPTION);

                if (oMatchingSourceDataImportHdr.AccountDataTable.Columns.Contains(GLDataImportFields.ACCOUNTTYPE))
                    oSqlBlkCopy.ColumnMappings.Add(GLDataImportFields.ACCOUNTTYPE, MatchingSourceDataTransitFields.ACCOUNT_TYPE);

                if (oMatchingSourceDataImportHdr.AccountDataTable.Columns.Contains(GLDataImportFields.GLACCOUNTNUMBER))
                    oSqlBlkCopy.ColumnMappings.Add(GLDataImportFields.GLACCOUNTNUMBER, MatchingSourceDataTransitFields.ACCOUNT_NUMBER);

                oSqlBlkCopy.ColumnMappings.Add(AddedGLTBSAccountFields.MATCHING_SOURCE_ACCOUNT_DATA, MatchingSourceDataTransitFields.MATCHING_SOURCE_ACCOUNT_XML);
                oSqlBlkCopy.ColumnMappings.Add(AddedGLTBSAccountFields.RECORDS_IMPORTED, MatchingSourceDataTransitFields.RECORDSIMPORTED);
                oSqlBlkCopy.ColumnMappings.Add(AddedGLTBSAccountFields.EXCEL_ROW_NUMBER_COLLECTION, MatchingSourceDataTransitFields.EXCEL_ROW_NUMBER_COLLECTION);

                //Map key fields
                string[] arrKeyFields = oMatchingSourceDataImportHdr.KeyFields.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                for (int k = 0; k < arrKeyFields.Length; k++)
                {
                    string sourceField = arrKeyFields[k].ToString();
                    string targetField = MatchingSourceDataTransitFields.KEY + (k + 2).ToString();
                    if (oMatchingSourceDataImportHdr.AccountDataTable.Columns.Contains(sourceField))
                        oSqlBlkCopy.ColumnMappings.Add(sourceField, targetField);
                }

                Helper.LogInfo("Transfering data to sql server destination table.", this.CompanyUserInfo);

                oSqlBlkCopy.WriteToServer(oMatchingSourceDataImportHdr.AccountDataTable);

                Helper.LogInfo("Transfering data to sql server complete", this.CompanyUserInfo);
            }
        }

        //private List<MatchingSourceColumnInfo> DeSerializeColumnList(string columnsXml, string rootNode)
        //{
        //    XmlSerializer xmlSerial = null;
        //    StringReader strReader = null;
        //    XmlTextReader txtReader = null;
        //    try
        //    {
        //        if (string.IsNullOrEmpty(rootNode))
        //        {
        //            xmlSerial = new XmlSerializer(typeof(List<MatchingSourceColumnInfo>));
        //        }
        //        else
        //        {
        //            XmlAttributeOverrides overrideAttr = new XmlAttributeOverrides();
        //            XmlAttributes attr = new XmlAttributes();
        //            attr.XmlRoot = new XmlRootAttribute(rootNode);
        //            overrideAttr.Add(typeof(List<MatchingSourceColumnInfo>), attr);
        //            xmlSerial = new XmlSerializer(typeof(List<MatchingSourceColumnInfo>), overrideAttr);

        //        }
        //        strReader = new StringReader(columnsXml);
        //        txtReader = new XmlTextReader(strReader);
        //        return (List<MatchingSourceColumnInfo>)xmlSerial.Deserialize(txtReader);
        //    }
        //    finally
        //    {
        //        if (txtReader != null)
        //            txtReader.Close();
        //        if (strReader != null)
        //            strReader.Close();
        //    }
        //}
        #endregion

    }
}
