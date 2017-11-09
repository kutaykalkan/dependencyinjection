using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Data;
using SkyStem.ART.Service.Utility;
using System.Xml;
using SkyStem.ART.Service.Data;
using System.Data.SqlClient;
using System.IO;
using SkyStem.ART.Service.Model;
using SkyStem.ART.Client.Model.CompanyDatabase;

namespace SkyStem.ART.Service.APP.BLL
{
    public class MatchingDataImport
    {
        #region "Public Attributes"

        public List<MatchingSourceDataImportHdrInfo> oMatchingSourceDataImportHdrInfoList;

        #endregion

        #region "Private Attributes"
        private CompanyUserInfo CompanyUserInfo;
        #endregion

        public MatchingDataImport(CompanyUserInfo oCompanyUserInfo)
        {
            this.CompanyUserInfo = oCompanyUserInfo;
        }

        #region "Public Methods"
        public bool IsProcessingRequiredForMatchingDataImport()
        {
            bool isProcessingRequired;
            try
            {
                oMatchingSourceDataImportHdrInfoList = MatchingHelper.GetMatchingSourceDataImportHdrForProcessing(this.CompanyUserInfo);
                isProcessingRequired = (oMatchingSourceDataImportHdrInfoList != null);
            }
            catch (Exception ex)
            {
                Helper.LogError(ex.Message, this.CompanyUserInfo);
                oMatchingSourceDataImportHdrInfoList = null;
                isProcessingRequired = false;
            }
            return isProcessingRequired;
        }

        public void ProcessMatchingDataImport()
        {

            //Process each record one by one
            foreach (MatchingSourceDataImportHdrInfo oMatchingSourceDataImportInfo in this.oMatchingSourceDataImportHdrInfoList)
            {
                DataTable oDataTableExcel = null;
                DataTable oDataTableAccount = null;
                try
                {
                    //process excel file to generate xml for file
                    oDataTableExcel = this.GetDataTableFromMatchingSourceData(oMatchingSourceDataImportInfo);
                    if (oDataTableExcel != null && oDataTableExcel.Rows.Count > 0)
                    {

                        oMatchingSourceDataImportInfo.RecordsImported = oDataTableExcel.Rows.Count;

                        MatchingSourceDataInfo oMatchingSourceData = new MatchingSourceDataInfo();
                        oMatchingSourceData.MatchingSourceDataImportID = oMatchingSourceDataImportInfo.MatchingSourceDataImportID;

                        //Generate XML from DataTable and populate respective property
                        oMatchingSourceData.XMLData = MatchingHelper.GenerateXMLFromDataTable(oDataTableExcel, false);
                        Helper.LogInfo("5. XML data generated from DataTable.", this.CompanyUserInfo);

                        //Generate Schema XML from DataTable and populate respective property
                        oMatchingSourceData.TableXMLSchema = MatchingHelper.GenerateSchemaXMLFromDataTable(oDataTableExcel);

                        oMatchingSourceDataImportInfo.MatchingSourceData = oMatchingSourceData;

                        Helper.LogInfo("6. Schema XML data generated from DataTable.", this.CompanyUserInfo);
                    }
                    else
                        throw new Exception("Proper datatable could not be generated from MatchingSourceData");

                    if (oMatchingSourceDataImportInfo.MatchingSourceTypeID == (short)Enums.DataImportType.GLTBS)
                    {
                        //Process Accounts and generate xml for individual accounts.
                        oDataTableAccount = this.GetAccountsDataTableForGLTBS(oMatchingSourceDataImportInfo, oDataTableExcel);

                        oMatchingSourceDataImportInfo.AccountDataTable = oDataTableAccount;
                    }
                    oMatchingSourceDataImportInfo.DateRevised = DateTime.Now;
                    oMatchingSourceDataImportInfo.RevisedBy = oMatchingSourceDataImportInfo.AddedBy;
                }
                catch (Exception ex)
                {

                    Helper.LogError(ex.Message, this.CompanyUserInfo);
                    oMatchingSourceDataImportInfo.HasError = true;
                    oMatchingSourceDataImportInfo.RecordsImported = 0;
                    MatchingSourceDataInfo oMatchingSourceData = new MatchingSourceDataInfo();
                    oMatchingSourceData.MatchingSourceDataImportID = oMatchingSourceDataImportInfo.MatchingSourceDataImportID;
                    oMatchingSourceData.XMLData = null;
                    oMatchingSourceData.TableXMLSchema = null;

                    oMatchingSourceDataImportInfo.MatchingSourceData = oMatchingSourceData;
                    oMatchingSourceDataImportInfo.DateRevised = DateTime.Now;
                    oMatchingSourceDataImportInfo.RevisedBy = oMatchingSourceDataImportInfo.AddedBy;
                    if (String.IsNullOrEmpty(oMatchingSourceDataImportInfo.Message))
                        oMatchingSourceDataImportInfo.Message = ex.Message;
                }
            }
            //Save all objects to database
            MatchingHelper.SaveProcessedMatchingSourceDataImportList(oMatchingSourceDataImportHdrInfoList, this.CompanyUserInfo);
        }

        #endregion

        #region "Private Methods"
        //Returns DataTable after processing MatchingSourceData
        private DataTable GetDataTableFromMatchingSourceData(MatchingSourceDataImportHdrInfo oMatchingSourceDataImportInfo)
        {

            OleDbConnection oConnectionExcel = null;
            string allColumnNames = "";
            string excelFilePhysicalPath = "";
            DataTable oDataTableExcel = null;
            OleDbDataReader oDataReaderExcel = null;
            DataTable dtGLDataSchema = null;
            StringBuilder oSBError = null;

            int languageID = oMatchingSourceDataImportInfo.LanguageID.HasValue ? oMatchingSourceDataImportInfo.LanguageID.Value : ServiceConstants.DEFAULTLANGUAGEID;
            excelFilePhysicalPath = oMatchingSourceDataImportInfo.PhysicalPath;
            string invalidDataMsg = Helper.GetInvalidDataErrorMessage(ServiceConstants.DEFAULTBUSINESSENTITYID, languageID, ServiceConstants.DEFAULTLANGUAGEID, this.CompanyUserInfo);

            Helper.LogInfo("3. Start Reading Excel file: " + oMatchingSourceDataImportInfo.PhysicalPath, this.CompanyUserInfo);
            try
            {
                //Get data from excel file
                oConnectionExcel = Helper.GetExcelFileConnection(excelFilePhysicalPath, false);
                oConnectionExcel.Open();

                // Get Excel File Schema - gets the list of Columns names as per Excel
                dtGLDataSchema = MatchingHelper.GetExcelFileSchema(oConnectionExcel, ServiceConstants.GLDATA_SHEETNAME, this.CompanyUserInfo);

                //Get All columns from excel schema
                allColumnNames = Helper.GetColumnNames(dtGLDataSchema);

                //Get Excel file reader
                oDataReaderExcel = MatchingHelper.GetDataReaderFromExcel(oConnectionExcel, allColumnNames, ServiceConstants.MATCHING_SHEETNAME, this.CompanyUserInfo);

                //Set ordinal position of Source Columns as returned by excel data reader
                //MatchingHelper.SetOrdinalForSourceColumns(oMatchingSourceDataImportInfo.MatchingSourceColumns, oDataReaderExcel);
                MatchingHelper.SetColumnsMapping(oMatchingSourceDataImportInfo.MatchingSourceColumns, oDataReaderExcel, this.CompanyUserInfo);

                //Generate DataTable as per SourceColumns
                oDataTableExcel = MatchingHelper.CreateTypedDataTable(oMatchingSourceDataImportInfo.MatchingSourceColumns);

                if (oMatchingSourceDataImportInfo.MatchingSourceTypeID.Value == (short)Enums.DataImportType.GLTBS)
                    oDataTableExcel.TableName = ServiceConstants.MATCHING_GLTBS_DATATABLE_NAME;
                else
                    oDataTableExcel.TableName = ServiceConstants.MATCHING_NBF_DATATABLE_NAME;

                //Fill DataTableExcel from DataReaderExcel
                oSBError = new StringBuilder();
                Helper.LogInfo("Reading excel file and populating Data Table", this.CompanyUserInfo);
                MatchingHelper.PopulateAndValidateDataTableFromExcel(oDataTableExcel, oDataReaderExcel, oSBError, invalidDataMsg, languageID);

                oDataReaderExcel.Close();
                oConnectionExcel.Close();
                oConnectionExcel.Dispose();
                Helper.LogInfo("4. Reading Excel file complete.", this.CompanyUserInfo);

                //If there are any errors while validating data, save the error in parent object, else generate XML and save it into object
                if (!String.IsNullOrEmpty(oSBError.ToString()))
                {
                    oMatchingSourceDataImportInfo.HasError = true;
                    oMatchingSourceDataImportInfo.Message = oSBError.ToString();
                    Helper.LogError("File: " + oMatchingSourceDataImportInfo.PhysicalPath + " processed with errors: " + oSBError.ToString(), this.CompanyUserInfo);
                    throw new Exception("Error while populating and validating excel file");
                }
                else
                {
                    oMatchingSourceDataImportInfo.HasError = false;
                }
                return oDataTableExcel;

            }
            finally
            {
                if (oDataReaderExcel != null && !oDataReaderExcel.IsClosed)
                {
                    oDataReaderExcel.Close();
                }
                if (oConnectionExcel != null && oConnectionExcel.State != ConnectionState.Closed)
                {
                    oConnectionExcel.Close();
                    oConnectionExcel.Dispose();
                }
            }
        }

        //Returns DataTable containing xml for each Account
        private DataTable GetAccountsDataTableForGLTBS(MatchingSourceDataImportHdrInfo oMatchingSourceDataImportInfo, DataTable oDataTableExcel)
        {
            DataTable oDataTableAccounts = null;
            try
            {
                if (oMatchingSourceDataImportInfo.MatchingSourceTypeID.Value == (short)Enums.DataImportType.GLTBS)
                {
                    DataSet ds = new DataSet();

                    //get all column names required to be created in Accounts Parent Table
                    //string[] allAccountFields = this.GetColumnForGLTBS(oMatchingSourceDataImportInfo.KeyFields);
                    string[] allAccountFields = this.GetColumnForGLTBS(oMatchingSourceDataImportInfo, oDataTableExcel.Columns);

                    //Get all distinct account mandatory and key fields from excel sheet data into a DataTable named Accounts
                    oDataTableAccounts = this.PrepareAccountsDataTable(oDataTableExcel, allAccountFields);

                    //Add both tables to DataSet and define relationship between them
                    DataRelation oDataRelation = this.DefineRelationship(oDataTableAccounts, oDataTableExcel, ds, allAccountFields);

                    //Add extra fields in Accounts table
                    oDataTableAccounts.Columns.Add(AddedGLTBSAccountFields.ACCOUNT_ID, System.Type.GetType("System.Int64"));
                    oDataTableAccounts.Columns.Add(AddedGLTBSAccountFields.MATCHING_SOURCE_ACCOUNT_DATA, System.Type.GetType("System.String"));
                    oDataTableAccounts.Columns.Add(AddedGLTBSAccountFields.MATCHING_SOURCE_DATAIMPORT_ID, System.Type.GetType("System.Int64"));
                    oDataTableAccounts.Columns.Add(AddedGLTBSAccountFields.RECORDS_IMPORTED, System.Type.GetType("System.Int32"));
                    oDataTableAccounts.Columns.Add(AddedGLTBSAccountFields.EXCEL_ROW_NUMBER_COLLECTION, System.Type.GetType("System.String"));

                    foreach (DataRow oParentDataRow in oDataTableAccounts.Rows)
                    {
                        DataRow[] oChildRowCollection = oParentDataRow.GetChildRows(oDataRelation);
                        if (oChildRowCollection != null && oChildRowCollection.Length > 0)
                        {
                            DataTable oDTChild = oChildRowCollection.CopyToDataTable();
                            oDTChild.TableName = ServiceConstants.MATCHING_CHILD_ACCOUNT_DATATABLE_NAME;
                            oParentDataRow[AddedGLTBSAccountFields.MATCHING_SOURCE_ACCOUNT_DATA] = MatchingHelper.GenerateXMLFromDataTable(oDTChild, false);
                            oParentDataRow[AddedGLTBSAccountFields.MATCHING_SOURCE_DATAIMPORT_ID] = oMatchingSourceDataImportInfo.MatchingSourceDataImportID.Value;
                            oParentDataRow[AddedGLTBSAccountFields.RECORDS_IMPORTED] = oDTChild.Rows.Count;
                            oParentDataRow[AddedGLTBSAccountFields.EXCEL_ROW_NUMBER_COLLECTION] = this.GetExcelRowCollectionString(oDTChild);
                            oDTChild = null;
                        }
                        else
                            throw new Exception("Error while processing file for Accounts. No Matching rows found.");
                    }
                    ////Rename key columns back to original keys
                    //string[] keyColumns = oMatchingSourceDataImportInfo.KeyFields.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    //int counter = 2;
                    //for (int i = 0; i < keyColumns.Length; i++)
                    //{

                    //    if (oDataTableAccounts.Columns.Contains (keyColumns[i]))
                    //    {
                    //        oDataTableAccounts.Columns[keyColumns[i]].ColumnName = "Key" + counter.ToString();
                    //    }
                    //    counter++;
                    //}
                }
                return oDataTableAccounts;
            }
            catch (Exception ex)
            {
                Helper.LogError("Error while processing GLTBS for Accounts: " + oMatchingSourceDataImportInfo.FileName + "Error: " + ex.Message, this.CompanyUserInfo);
                throw ex;
            }
        }

        //Returns a string containing all excel row ids for an account
        private string GetExcelRowCollectionString(DataTable oDataTableAccount)
        {
            StringBuilder oSBExcelRowNumber = new StringBuilder();
            for (int i = 0; i < oDataTableAccount.Rows.Count; i++)
            {
                if (oSBExcelRowNumber.ToString() != "")
                    oSBExcelRowNumber.Append(",");
                oSBExcelRowNumber.Append(oDataTableAccount.Rows[i][AddedGLTBSImportFields.EXCELROWNUMBER].ToString());
            }
            return oSBExcelRowNumber.ToString();
        }

        //Prepares a DataTable containing unique combinations of Account keys from excel file dataTable as per string array passed
        private DataTable PrepareAccountsDataTable(DataTable oDataTableExcel, string[] allColumnNames)
        {
            DataTable oDTAccounts = oDataTableExcel.DefaultView.ToTable(true, allColumnNames);
            oDTAccounts.TableName = ServiceConstants.MATCHING_ACCOUNT_DATATABLE_NAME;
            return oDTAccounts;
        }

        //Defines relationship between Accounts and DataTable from excel, adds them to dataSet and defined relationship between them
        //based on commonColumns string array
        private DataRelation DefineRelationship(DataTable oDTAccounts, DataTable oDataTableExcel, DataSet ds, string[] commonColumnNames)
        {
            List<DataColumn> oParentDataColumnList = new List<DataColumn>();
            List<DataColumn> oChildDataColumnList = new List<DataColumn>();
            for (int x = 0; x < commonColumnNames.Length; x++)
            {
                DataColumn oDCParent = oDTAccounts.Columns[commonColumnNames[x]];
                DataColumn oDCChild = oDataTableExcel.Columns[commonColumnNames[x]];
                if (oDCParent != null && oDCChild != null)
                {
                    oParentDataColumnList.Add(oDCParent);
                    oChildDataColumnList.Add(oDCChild);
                }
            }
            ds.Tables.Add(oDTAccounts);
            ds.Tables.Add(oDataTableExcel);

            oDTAccounts.TableName = "Accounts";

            DataRelation dr = new DataRelation("AccountDetail", oParentDataColumnList.ToArray(), oChildDataColumnList.ToArray(), false);
            ds.Relations.Add(dr);

            return dr;
        }

        //obsolete method, need to be removed
        private string[] GetColumnForGLTBS(string keyFields)
        {
            string[] ColumnNames = new string[] 
                {GLDataImportFields.FSCAPTION
                ,GLDataImportFields.ACCOUNTTYPE
                ,GLDataImportFields.GLACCOUNTNUMBER};
            string[] arrKeyFields = keyFields.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            string[] ColumnNames1 = new string[ColumnNames.Length + arrKeyFields.Length];
            ColumnNames.CopyTo(ColumnNames1, 0);
            arrKeyFields.CopyTo(ColumnNames1, ColumnNames.Length);
            return ColumnNames1;
        }

        //Gets a list of all possible account fields, check field existance in data column collection and returns an array for account fields
        private string[] GetColumnForGLTBS(MatchingSourceDataImportHdrInfo oMatchingSourceDataImportInfo, DataColumnCollection columnCollectionFromExcel)
        {
            List<string> allPossibleAccountFieldList = MatchingHelper.GetAllPossibleAccountFields(oMatchingSourceDataImportInfo);
            List<string> AccountFieldList = new List<string>();

            if (allPossibleAccountFieldList != null && allPossibleAccountFieldList.Count > 0)
            {
                foreach (string accountField in allPossibleAccountFieldList)
                {
                    if (columnCollectionFromExcel.Contains(accountField))
                        AccountFieldList.Add(accountField);
                }
            }
            return AccountFieldList.ToArray();
        }

        #endregion
    }
}
