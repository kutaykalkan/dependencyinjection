using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using SkyStem.ART.Service.Data;
using SkyStem.ART.Service.Model;
using SkyStem.ART.Service.APP.DAO;
using System.IO;
using System.Data.OleDb;
using SkyStem.ART.Client.Model.CompanyDatabase;

namespace SkyStem.ART.Service.Utility
{
    public class MatchingHelper
    {
        #region "Public Methods"
        public static MatchSetHdrInfo GetMatchSetHdrForProcessing(Enums.MatchingStatus toBeProcessed, Enums.MatchingStatus inProgress, CompanyUserInfo oCompanyUserInfo)
        {
            MatchSetHdrDAO oMatchEngineDAO = new MatchSetHdrDAO(oCompanyUserInfo);
            return oMatchEngineDAO.GetMatchSetHdrForProcessing(toBeProcessed, inProgress);
        }

        public static bool IsMatchingRequired(MatchSetHdrInfo oMatchSetHdr, Enums.MatchingStatus toBeProcessed, Enums.MatchingStatus inProgress, CompanyUserInfo oCompanyUserInfo)
        {
            MatchSetHdrDAO oMatchEngineDAO = new MatchSetHdrDAO(oCompanyUserInfo);
            oMatchSetHdr = oMatchEngineDAO.GetMatchSetHdrForProcessing(toBeProcessed, inProgress);

            return (oMatchSetHdr != null);
        }

        public static List<MatchSetSubSetCombinationInfo> GetMatchSetSubSetCombinationForMatchSetID(long matchSetHdrID, CompanyUserInfo oCompanyUserInfo)
        {
            MatchSetSubSetCombinationDAO oMatchSetSubsetCombinationDAO = new MatchSetSubSetCombinationDAO(oCompanyUserInfo);
            List<MatchSetSubSetCombinationInfo> oMatchSetSubSetCombinationInfoList = oMatchSetSubsetCombinationDAO.GetMatchSetSubSetCombinationForMatchSetID(matchSetHdrID);


            //Get all ColumnConfiguration objects for all MatchSetSubSetCombination
            MatchingConfigurationDAO oMatchingConfigDAO = new MatchingConfigurationDAO(oCompanyUserInfo);
            List<MatchingConfigurationInfo> oMatchingConfigList = oMatchingConfigDAO.GetMatchingConfigurationForMatchSetID(matchSetHdrID);

            //Associate related MatchingConfigurations with each MatchSetSubSetCombination
            foreach (MatchSetSubSetCombinationInfo obj in oMatchSetSubSetCombinationInfoList)
            {
                long matchSetSubSetCombinationID = obj.MatchSetSubSetCombinationID.Value;
                List<MatchingConfigurationInfo> configList = oMatchingConfigList.FindAll(r => r.MatchSetSubSetCombinationID.Value == matchSetSubSetCombinationID);
                obj.MatchingConfigurationCollection = configList;
                configList = null;
            }
            oMatchingConfigList = null;

            return oMatchSetSubSetCombinationInfoList;
        }

        public static DataTable GetDataTableFromXmlString(string xmlString)
        {
            StringReader sr = new StringReader(xmlString);
            DataSet ds = new DataSet();
            ds.ReadXml(sr);
            return ds.Tables[0];
        }

        public static bool SaveMatchSetResultsForMatchSetID(long MatchSetID, List<MatchSetResultInfo> oListMatchSetResultInfo
            , string AddedBy, DateTime DateAdded, CompanyUserInfo oCompanyUserInfo)
        {
            MatchSetHdrDAO oMatchSet = new MatchSetHdrDAO(oCompanyUserInfo);
            MatchSetResultDAO oMatchSetResult = new MatchSetResultDAO(oCompanyUserInfo);
            DataTable oDTMatchSetResult = MatchingHelper.GetMatchSetResultDataTableFromInfoList(oListMatchSetResultInfo);
            int retval = oMatchSetResult.SaveMatchSetResultInfoList(oDTMatchSetResult, AddedBy, DateAdded, oCompanyUserInfo);
            return true;
        }


        public static bool UpdateMatchSetHdrStatus(MatchSetHdrInfo oMatchSetHdrInfo, CompanyUserInfo oCompanyUserInfo)
        {
            MatchSetHdrDAO oMatchSet = new MatchSetHdrDAO(oCompanyUserInfo);
            int retval = 0;
            retval = oMatchSet.UpdateMatchSetHdrStatus(oMatchSetHdrInfo, DateTime.Now);
            if (retval <= 0)
            {
                Helper.LogError(@"MatchSetHdr could not be updated. Error: " + oMatchSetHdrInfo.MatchSetID.Value.ToString(), oCompanyUserInfo);
            }
            return true;
        }
        #endregion

        #region "Private Methods"
        private static DataTable GetMatchSetResultDataTableFromInfoList(List<MatchSetResultInfo> oListMatchSetResultInfo)
        {
            DataTable oDTResult = new DataTable();
            oDTResult.Columns.Add("MatchSetSubSetCombinationID", typeof(System.Int64));
            oDTResult.Columns.Add("ResultSchema", typeof(System.String));
            oDTResult.Columns.Add("MatchData", typeof(System.String));
            oDTResult.Columns.Add("PartialMatchData", typeof(System.String));
            oDTResult.Columns.Add("UnMatchData", typeof(System.String));


            foreach (MatchSetResultInfo oMatchResult in oListMatchSetResultInfo)
            {
                DataRow dr = oDTResult.NewRow();
                dr["MatchSetSubSetCombinationID"] = oMatchResult.MatchSetSubSetCombinationID.Value;
                dr["ResultSchema"] = oMatchResult.ResultSchema;
                dr["MatchData"] = oMatchResult.MatchData;
                dr["PartialMatchData"] = oMatchResult.PartialMatchData;
                dr["UnMatchData"] = oMatchResult.UnmatchData;
                oDTResult.Rows.Add(dr);
                //oDTResult.Rows.Add(oMatchResult.MatchSetSubSetCombinationID
                //    , oMatchResult.ResultSchema
                //    , oMatchResult.MatchData
                //    , oMatchResult.PartialMatchData
                //    , oMatchResult.UnmatchData);
            }
            return oDTResult;
        }
        #endregion


        #region "Matching DataImport"
        public static List<MatchingSourceDataImportHdrInfo> GetMatchingSourceDataImportHdrForProcessing(CompanyUserInfo oCompanyUserInfo)
        {
            MatchingSourceDataImportDAO oMatchingSourceDataImportDAO = new MatchingSourceDataImportDAO(oCompanyUserInfo);
            return oMatchingSourceDataImportDAO.GetMatchingSourceDataImportHdrForProcessing();
        }

        //public static void UpdateMatchingSourceDataImportHDR(SqlConnection oConnection, long matchingSourceDataImportID
        //                    , string errorMessageToSave, string dataImportStatus, int recordsImported)
        //{
        //    MatchingSourceDataImportDAO oMatchingSourceDataImportDAO = new MatchingSourceDataImportDAO();
        //    oMatchingSourceDataImportDAO.UpdateMatchingSourceDataImportHDR(oConnection, matchingSourceDataImportID, errorMessageToSave
        //                    , dataImportStatus, recordsImported);
        //}

        public static string GetColumnNamesFromColumnsCollection(List<MatchingSourceColumnInfo> oListMatchingSourceColumn)
        {
            StringBuilder oSBColumns = new StringBuilder();
            foreach (MatchingSourceColumnInfo oMatchingSourceColumn in oListMatchingSourceColumn)
            {
                if (!String.IsNullOrEmpty(oSBColumns.ToString()))
                {
                    oSBColumns.Append(", ");
                }
                oSBColumns.Append("[");
                oSBColumns.Append(oMatchingSourceColumn.ColumnName);
                oSBColumns.Append("]");


            }
            return oSBColumns.ToString();
        }

        public static void AddRowNumberColumnInDataTable(DataTable oDataTable)
        {
            DataColumn dataColumn = new DataColumn(AddedGLTBSImportFields.EXCELROWNUMBER, Type.GetType("System.Int64"));
            oDataTable.Columns.Add(dataColumn);
            DataColumn dc = new DataColumn(AddedGLTBSImportFields.ISRECITEMCREATED, Type.GetType("System.Boolean"));
            oDataTable.Columns.Add(dc);
            Int64 index = 1;
            foreach (DataRow dr in oDataTable.Rows)
            {
                index++;
                dr[AddedGLTBSImportFields.EXCELROWNUMBER] = index;
                dr[AddedGLTBSImportFields.ISRECITEMCREATED] = false;

            }
        }

        public static DataTable CreateTypedDataTable(List<MatchingSourceColumnInfo> oListMatchingSourceColumn)
        {
            DataTable dt = new DataTable();

            foreach (MatchingSourceColumnInfo oMatchingSourceColumn in oListMatchingSourceColumn)
            {
                if (oMatchingSourceColumn.Ordinal.HasValue)
                {
                    string columnName = oMatchingSourceColumn.ColumnName;
                    DataColumn col = new DataColumn(columnName);
                    col.ExtendedProperties.Add("ColumnOrdinal", oMatchingSourceColumn.Ordinal);
                    col.ExtendedProperties.Add("ColumnNameInReader", oMatchingSourceColumn.ColumnNameInReader);

                    //Set Data Type of column
                    switch (oMatchingSourceColumn.DataTypeID)
                    {
                        case (short)Enums.DataType.Boolean:
                            col.DataType = System.Type.GetType("System.Boolean");
                            break;
                        case (short)Enums.DataType.DataTime:
                            col.DataType = System.Type.GetType("System.DateTime");
                            break;
                        case (short)Enums.DataType.Decimal:
                            col.DataType = System.Type.GetType("System.Decimal");
                            break;
                        case (short)Enums.DataType.Integer:
                            col.DataType = System.Type.GetType("System.Int32");
                            break;
                        case (short)Enums.DataType.String:
                            col.DataType = System.Type.GetType("System.String");
                            break;
                    }
                    dt.Columns.Add(col);
                    col = null;
                }
            }

            //Add a column for Excel row number and Rec Item Created
            DataColumn dataColumn = new DataColumn(AddedGLTBSImportFields.EXCELROWNUMBER, Type.GetType("System.Int64"));
            dt.Columns.Add(dataColumn);
            DataColumn dc = new DataColumn(AddedGLTBSImportFields.ISRECITEMCREATED, Type.GetType("System.Boolean"));
            dt.Columns.Add(dc);

            return dt;
        }

        public static OleDbDataReader GetDataReaderFromExcel(OleDbConnection oConnExcelFile, string allColumnNames, string sheetName, CompanyUserInfo oCompanyUserInfo)
        {
            try
            {
                string query = "SELECT " + allColumnNames + " FROM [" + sheetName + "$]";
                OleDbCommand oCommandExcel = new OleDbCommand(query, oConnExcelFile);
                return oCommandExcel.ExecuteReader();
            }
            catch (Exception ex)
            {
                Helper.LogError(ex, oCompanyUserInfo);
                throw new Exception("Error while reading data from excel file");
            }
        }

        public static DataTable GetExcelFileSchema(OleDbConnection oConnExcelFile, string sheetName, CompanyUserInfo oCompanyUserInfo)
        {
            DataTable oExcelFileSchemaDataTable = null;
            DataTable oExcelSchema = null;
            try
            {
                if (oConnExcelFile.State != ConnectionState.Open)
                    oConnExcelFile.Open();
                oExcelSchema = oConnExcelFile.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                DataRow[] dr = oExcelSchema.Select("TABLE_NAME = '" + sheetName + "$'");
                if (dr.Length > 0)
                {
                    string[] restrictions = { null, null, sheetName + "$", null };
                    oExcelFileSchemaDataTable = oConnExcelFile.GetSchema("Columns", restrictions);
                    DataView dvColumns = new DataView(oExcelFileSchemaDataTable);
                    dvColumns.Sort = "ORDINAL_POSITION";
                    return dvColumns.ToTable();
                }
                else
                {
                    string errMsg = "Data sheet with name " + sheetName + " not found";
                    Helper.LogError(errMsg, oCompanyUserInfo);
                    throw new Exception(errMsg);
                }
            }
            catch (Exception ex)
            {
                Helper.LogError(ex, oCompanyUserInfo);
                throw new Exception("Error while establishing connection to Excel file.");
            }
        }

        //sets ordinal of Source column as per datareader returned from excel
        public static void SetOrdinalForSourceColumns(List<MatchingSourceColumnInfo> oListMatchingSourceColumn, OleDbDataReader oExcelDataReader, CompanyUserInfo oCompanyUserInfo)
        {
            if (oExcelDataReader.HasRows)
            {
                //Get first excel rows which contains all field Names.
                oExcelDataReader.Read();
                for (int i = 0; i < oExcelDataReader.FieldCount; i++)
                {
                    string columnName = oExcelDataReader[i].ToString();
                    //Map column in columnCollection with Reader and save its ordinal
                    foreach (MatchingSourceColumnInfo oMatchingColumn in oListMatchingSourceColumn)
                    {
                        if (columnName == oMatchingColumn.ColumnName)
                        {
                            oMatchingColumn.Ordinal = i;
                            break;
                        }
                    }
                }

            }
            else
            {
                Helper.LogError("MatchingHelper.SetOrdinalForSourceColumns: No rows found on data reader", oCompanyUserInfo);
                throw new Exception("No Columns found");
            }
        }

        //sets ordinal of Source column as per datareader returned from excel
        public static void SetColumnsMapping(List<MatchingSourceColumnInfo> oListMatchingSourceColumn, OleDbDataReader oExcelDataReader, CompanyUserInfo oCompanyUserInfo)
        {
            if (oExcelDataReader.HasRows)
            {
                //Get first excel rows which contains all field Names.
                oExcelDataReader.Read();
                foreach (MatchingSourceColumnInfo oMatchingColumn in oListMatchingSourceColumn)
                {
                    for (int i = 0; i < oExcelDataReader.FieldCount; i++)
                    {
                        string columnName = oExcelDataReader[i].ToString().Trim();
                        if (columnName == oMatchingColumn.ColumnName.Trim())
                        {
                            oMatchingColumn.Ordinal = i;
                            oMatchingColumn.ColumnNameInReader = oExcelDataReader.GetName(i);
                            break;
                        }
                    }
                }
            }
            else
            {
                Helper.LogError("MatchingHelper.SetOrdinalForSourceColumns: No rows found on data reader", oCompanyUserInfo);
                throw new Exception("No Columns found");
            }
        }

        //Populate Data Table and Validate data type as mentioned by user
        public static void PopulateAndValidateDataTableFromExcel(DataTable oDataTableExcel, OleDbDataReader oDataReaderExcel, StringBuilder oSbErrorMessage, string invalidDataMsg, int languageID)
        {
            int excelRowNumber = 1;

            //Now, reader is pointing at the end of first row.
            while (oDataReaderExcel.Read())
            {
                DataRow dr = oDataTableExcel.NewRow();
                excelRowNumber++;

                dr[AddedGLTBSImportFields.EXCELROWNUMBER] = excelRowNumber;
                dr[AddedGLTBSImportFields.ISRECITEMCREATED] = false;
                bool isBlankRow = true;
                foreach (DataColumn dc in oDataTableExcel.Columns)
                {
                    if (dc.ExtendedProperties.ContainsKey("ColumnNameInReader"))
                    {
                        string ColumnNameInReader = dc.ExtendedProperties["ColumnNameInReader"].ToString();
                        int dataTableColumnOrdinal = oDataReaderExcel.GetOrdinal(ColumnNameInReader);

                        if (!oDataReaderExcel.IsDBNull(dataTableColumnOrdinal))
                        {
                            isBlankRow = false;
                            //Read Column Value
                            string columnValue = oDataReaderExcel.GetString(dataTableColumnOrdinal).Trim();
                            try
                            {
                                switch (dc.DataType.FullName)
                                {
                                    case "System.String":
                                        dr[dc.ColumnName] = columnValue;
                                        break;
                                    case "System.Boolean":
                                        dr[dc] = Boolean.Parse(columnValue);
                                        break;
                                    case "System.Decimal":
                                        dr[dc] = Decimal.Parse(columnValue, System.Globalization.NumberStyles.Number);
                                        break;
                                    case "System.Int32":
                                        dr[dc] = Int32.Parse(columnValue, System.Globalization.NumberStyles.Integer);
                                        break;
                                    case "System.DateTime":
                                         DateTime tmpDate = DateTime.MinValue;
                                         if (Helper.IsValidDateTime(columnValue, languageID, out tmpDate) && tmpDate != DateTime.MinValue)
                                             dr[dc] = tmpDate;
                                         else
                                             throw new Exception();
                                        break;
                                }
                            }
                            catch (InvalidDataException dataException)
                            {
                                oSbErrorMessage.Append(String.Format(invalidDataMsg, dc.ColumnName, excelRowNumber));
                                oSbErrorMessage.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
                            }
                            catch (Exception ex)
                            {
                                oSbErrorMessage.Append(String.Format(invalidDataMsg, dc.ColumnName, excelRowNumber));
                                oSbErrorMessage.Append(ServiceConstants.ERRORMESSAGESEPERATOR);
                            }
                        }
                    }
                }
                if (!isBlankRow)
                    oDataTableExcel.Rows.Add(dr);
            }
        }

        //Save collection to database
        public static void SaveProcessedMatchingSourceDataImportList(List<MatchingSourceDataImportHdrInfo> oMatchingSourceDataImportHdrList, CompanyUserInfo oCompanyUserInfo)
        {
            MatchingSourceDataImportDAO oMatchingSourceDataImportDAO = new MatchingSourceDataImportDAO(oCompanyUserInfo);
            oMatchingSourceDataImportDAO.SaveMatchingSourceDataObjectList(oMatchingSourceDataImportHdrList);
        }

        public static DataTable GetAccountsDataTable(DataTable oDTAccount, CompanyUserInfo oCompanyUserInfo)
        {
            MatchingSourceAccountInfo oMatchingSourceAccount = new MatchingSourceAccountInfo();
            MatchingSourceAccountDAO oMatchingSourceAccountDAO = new MatchingSourceAccountDAO(oCompanyUserInfo);
            return oMatchingSourceAccountDAO.GetAccountIDs(oDTAccount);
        }

        public static string GenerateXMLFromDataTable(DataTable oDataTable, bool writeHierarchy)
        {
            StringWriter writer = null;
            try
            {
                writer = new StringWriter();
                oDataTable.WriteXml(writer, writeHierarchy);
                return writer.ToString();
            }
            finally
            {
                if (writer != null)
                    writer.Dispose();
            }
        }

        public static string GenerateSchemaXMLFromDataTable(DataTable oDataTable)
        {
            StringWriter writer = null;
            try
            {
                writer = new StringWriter();
                oDataTable.WriteXmlSchema(writer, false);
                return writer.ToString();
            }
            finally
            {
                if (writer != null)
                    writer.Dispose();
            }
        }

        //Generates xml string of schema for Data Table
        public static string GenerateSchemaXMLFromDataSet(DataSet oDataSet)
        {
            StringWriter writer = null;
            try
            {
                writer = new StringWriter();
                oDataSet.WriteXmlSchema(writer);
                return writer.ToString();
            }
            finally
            {
                if (writer != null)
                    writer.Dispose();
            }
        }

        //Generates xml string for DataTables in DataSet
        public static string GenerateXMLFromDataSet(DataSet oDataSet)
        {
            StringWriter writer = null;
            try
            {
                writer = new StringWriter();
                oDataSet.WriteXml(writer);
                return writer.ToString();
            }
            finally
            {
                if (writer != null)
                    writer.Dispose();
            }
        }

        //Get a list of static fields
        public static List<string> GetAccountStaticFields()
        {
            List<string> colList = DataImportHelper.GetAccountStaticFields();
            colList.AddRange(DataImportHelper.GetAllAccountCreationMendatoryFields());
            return colList;
        }

        //Get a list of Mapper keys for Account hierarchy
        public static List<string> GetAccountKeyFields(MatchingSourceDataImportHdrInfo oEntity)
        {
            return oEntity.KeyFields.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList<string>();
        }

        //Get a list of Account unique subset keys
        public static List<string> GetAccountUniqueSubsetFields(MatchingSourceDataImportHdrInfo oEntity)
        {
            List<string> fieldList = new List<string>();
            if (!String.IsNullOrEmpty(oEntity.AccountUniqueSubSetKeys))
            {
                string[] arryAccountUniqueKeys = oEntity.AccountUniqueSubSetKeys.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                fieldList = arryAccountUniqueKeys.ToList<string>();
            }
            return fieldList;
        }

        public static List<string> GetAccountMandatoryFields(MatchingSourceDataImportHdrInfo oEntity)
        {
            List<string> fieldList = new List<string>();

            if (!String.IsNullOrEmpty(oEntity.AccountUniqueSubSetKeys))
            {
                fieldList.AddRange(GetAccountUniqueSubsetFields(oEntity));
            }
            else
            {
                fieldList.AddRange(GetAccountStaticFields());

                //Get Account Key fields as used by this company
                if (!String.IsNullOrEmpty(oEntity.KeyFields))
                    fieldList.AddRange(GetAccountKeyFields(oEntity));
            }
            return fieldList;
        }

        //Get all possible Account Fields
        public static List<string> GetAllPossibleAccountFields(MatchingSourceDataImportHdrInfo oEntity)
        {
            List<string> fieldList = new List<string>();

            fieldList.AddRange(GetAccountStaticFields());

            //Get Account Key fields as used by this company
            if (!String.IsNullOrEmpty(oEntity.KeyFields))
                fieldList.AddRange(GetAccountKeyFields(oEntity));

            return fieldList;
        }
        #endregion
    }
}
