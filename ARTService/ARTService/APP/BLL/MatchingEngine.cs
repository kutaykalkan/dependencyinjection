using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SkyStem.ART.Service.Data;
using SkyStem.ART.Service.Model;
using SkyStem.ART.Service.Utility;
using System.Data;
using System.IO;
using SkyStem.ART.Service.APP.DAO;
using System.Data.SqlClient;
using SkyStem.ART.Client.Model.CompanyDatabase;

namespace SkyStem.ART.Service.APP.BLL
{
    public class MatchingEngine
    {
        private MatchSetHdrInfo oMatchSetHdrInfo;
        private CompanyUserInfo CompanyUserInfo;

        public MatchingEngine(CompanyUserInfo oCompanyUserInfo)
        {
            this.CompanyUserInfo = oCompanyUserInfo;
        }

        #region "Public Methods"
        //This methods checks in db if and MatchSetHdr require processing, if so, initializes passed object of MatSetHdrInfo
        public bool IsMatchingRequired()
        {
            try
            {
                oMatchSetHdrInfo = MatchingHelper.GetMatchSetHdrForProcessing(Enums.MatchingStatus.ToBeProcessed, Enums.MatchingStatus.InProgress, this.CompanyUserInfo);
            }
            catch (Exception ex)
            {
                oMatchSetHdrInfo = null;
                Helper.LogError(@"Error in IsMatchingRequired: " + ex.Message, this.CompanyUserInfo);
            }
            return (oMatchSetHdrInfo != null);
        }

        //this function process MatchedSetHdrInfo object for matching
        public void ProcessMatching()
        {

            StringBuilder oSBErrors = new StringBuilder();

            if (oMatchSetHdrInfo != null)
            {
                try
                {
                    Helper.LogInfo(@"Starting matching process for MatchSetHdr : " + oMatchSetHdrInfo.MatchSetID.ToString(), this.CompanyUserInfo);
                    /*Data retrival for matching
                     * Get all subset combinations for this MatchSetHdrID
                     * Get all subsets information (xml data, columns)
                     * Get all match and partial-match keys for combinations
                     * Get all matching rules for each combinations
                     * 
                     */
                    List<MatchSetSubSetCombinationInfo> oMatchSetSubSetCombinationList = MatchingHelper.GetMatchSetSubSetCombinationForMatchSetID(oMatchSetHdrInfo.MatchSetID.Value, this.CompanyUserInfo);
                    List<MatchSetResultInfo> oListMatchSetResultInfo = new List<MatchSetResultInfo>();
                    //Process each combination one by one
                    foreach (MatchSetSubSetCombinationInfo oSubSetCombination in oMatchSetSubSetCombinationList)
                    {
                        oListMatchSetResultInfo.Add(ProcessSubSetCombination(oSubSetCombination));
                    }
                    //Save matched, partial match, unmatched recordsets in DB
                    MatchingHelper.SaveMatchSetResultsForMatchSetID(oMatchSetHdrInfo.MatchSetID.Value, oListMatchSetResultInfo, oMatchSetHdrInfo.AddedBy, DateTime.Now, this.CompanyUserInfo);

                    //Mark the record as processed
                    Helper.LogInfo(@"Matching process ended for MatchSetHdr : " + oMatchSetHdrInfo.MatchSetID.ToString(), this.CompanyUserInfo);
                }
                catch (Exception ex)
                {
                    Helper.LogError(@"Error in ProcessMatching: " + ex.Message, this.CompanyUserInfo);
                    //Error while Matching process. 
                    oSBErrors.Append(Helper.GetSinglePhrase(5000301,
                    ServiceConstants.DEFAULTBUSINESSENTITYID,
                    oMatchSetHdrInfo.UserLanguageID,
                    ServiceConstants.DEFAULTLANGUAGEID, this.CompanyUserInfo));
                    //Prepare error message that will be saved in database
                }
                finally
                {
                    //update MatchSet Hdr
                    if (!oSBErrors.ToString().Equals(string.Empty))
                    {
                        oMatchSetHdrInfo.Message = oSBErrors.ToString();
                        oMatchSetHdrInfo.MatchingStatusID = (short)Enums.MatchingStatus.Error;
                    }
                    else
                    {
                        oMatchSetHdrInfo.Message = Helper.GetSinglePhrase(2360, ServiceConstants.DEFAULTBUSINESSENTITYID, oMatchSetHdrInfo.UserLanguageID, ServiceConstants.DEFAULTLANGUAGEID, this.CompanyUserInfo);
                        oMatchSetHdrInfo.MatchingStatusID = (short)Enums.MatchingStatus.Success;
                    }

                    try
                    {
                        Helper.LogInfo(@"Updating MatchSetHdr for MatchSetID: " + oMatchSetHdrInfo.MatchSetID.Value.ToString(), this.CompanyUserInfo);
                        MatchingHelper.UpdateMatchSetHdrStatus(oMatchSetHdrInfo, this.CompanyUserInfo);
                        Helper.LogError(@"MatchSetHdr Updated for MatchSetID: " + oMatchSetHdrInfo.MatchSetID.Value.ToString(), this.CompanyUserInfo);
                    }
                    catch (Exception ex)
                    {
                        Helper.LogError(@"MatchSetHdr could not be updated. Error: " + oMatchSetHdrInfo.MatchSetID.GetValueOrDefault().ToString() + " " + ex.Message, this.CompanyUserInfo);
                    }

                    //Send Mail
                    try
                    {
                        Helper.LogInfo(@"Sending Email for MatchSetID: " + oMatchSetHdrInfo.MatchSetID.Value.ToString(), this.CompanyUserInfo);
                        MailHelper.SendEmailToUserByMatchingResult(oMatchSetHdrInfo, this.CompanyUserInfo);
                        Helper.LogInfo(@"Email Send for MatchSetID: " + oMatchSetHdrInfo.MatchSetID.Value.ToString(), this.CompanyUserInfo);
                    }
                    catch (Exception ex)
                    {
                        Helper.LogError(@"MatchSetHdr could not be updated. Error: " + oMatchSetHdrInfo.MatchSetID.Value.ToString() + " " + ex.Message, this.CompanyUserInfo);
                    }
                }
            }
        }
        #endregion

        #region "Private Methods"
        private MatchSetResultInfo ProcessSubSetCombination(MatchSetSubSetCombinationInfo oSubSetCombination)
        {
            DataTable oDTSource1;
            DataTable oDTSource2;
            DataTable oDTFullMatchPair;
            DataTable oDTPartialMatchPair;
            DataColumn[] keys;
            MatchSetResultInfo oMatchSetResultInfo;
            List<MatchingConfigurationInfo> oConfigList;

            Dictionary<int, DisplayToSourceColumnMapping> oDictColMapping;
            DataTable oDTDisplay;

            oDTFullMatchPair = new DataTable();
            DataColumn oDCPairID = new DataColumn(Pair.COLUMN_PAIRID, typeof(System.Int32));
            oDTFullMatchPair.Columns.Add(oDCPairID);

            oDTPartialMatchPair = oDTFullMatchPair.Clone();

            //Get data table out of Source1 xml and Source2 xml
            oMatchSetResultInfo = new MatchSetResultInfo();

            oDTSource1 = MatchingHelper.GetDataTableFromXmlString(oSubSetCombination.Source1.SubSetData);
            oDTSource2 = MatchingHelper.GetDataTableFromXmlString(oSubSetCombination.Source2.SubSetData); ;
            oMatchSetResultInfo.MatchSetSubSetCombinationID = oSubSetCombination.MatchSetSubSetCombinationID.Value;

            //Set primary key for DataTable to make Select() fast. Primary key column is ExcelRowNumber
            keys = new DataColumn[1];
            if (oDTSource2.Columns.Contains(AddedGLTBSImportFields.EXCELROWNUMBER))
            {
                DataColumn dc = oDTSource2.Columns[AddedGLTBSImportFields.EXCELROWNUMBER];
                keys[0] = dc;
                oDTSource2.PrimaryKey = keys;
            }
            keys = new DataColumn[1];
            if (oDTSource1.Columns.Contains(AddedGLTBSImportFields.EXCELROWNUMBER))
            {
                DataColumn dc = oDTSource1.Columns[AddedGLTBSImportFields.EXCELROWNUMBER];
                keys[0] = dc;
                oDTSource1.PrimaryKey = keys;
            }

            //Creates additional fields in Source1 and Source2 data tables.
            //Additional fields are FullMatchPairID Int, PartialMatchPairID Int
            this.AddFieldsToDataSources(oDTSource1, oDTSource2);

            //Get Configuration collection between sources 
            oConfigList = oSubSetCombination.MatchingConfigurationCollection;

            //Process full Matching
            this.ProcessFullMatching(oDTSource1, oDTSource2, oDTFullMatchPair, oConfigList);

            //Process Partial Matching
            this.ProcessPartialMatching(oDTSource1, oDTSource2, oDTPartialMatchPair, oConfigList);

            //Produce dataTable as per Display columns
            oDTDisplay = this.GetDisplayDataTable(oConfigList);

            oDictColMapping = new Dictionary<int, DisplayToSourceColumnMapping>();

            //Map display datatable with configuration and store information in dictionary
            this.MapDisplayDataTableWithColumnConfig(oDTDisplay, oConfigList, oDTSource1, oDTSource2, oDictColMapping);

            //Get Matched Set XML
            oMatchSetResultInfo.MatchData = this.GetFullMatchXML(oDTDisplay, oDTSource1, oDTSource2, oDTFullMatchPair, oDictColMapping, oSubSetCombination);

            //Get Partial Match XML
            oMatchSetResultInfo.PartialMatchData = this.GetPartialMatchXML(oDTDisplay, oDTSource1, oDTSource2, oDTPartialMatchPair, oDictColMapping, oSubSetCombination);

            //Get UnMatched XML
            oMatchSetResultInfo.UnmatchData = this.GetUnMatchXML(oDTDisplay, oDTSource1, oDTSource2, oDictColMapping, oSubSetCombination);

            //Get Schema
            DataSet oDS = this.GetResultDataSet(oDTDisplay, oDTFullMatchPair.Clone());
            oMatchSetResultInfo.ResultSchema = MatchingHelper.GenerateSchemaXMLFromDataSet(oDS);

            return oMatchSetResultInfo;
        }

        private DataSet GetResultDataSet(DataTable oDTDisplay, DataTable oDTPair)
        {
            DataSet ds;
            DataTable oDTDisplaySource1;
            DataTable oDTDisplaySource2;

            ds = new DataSet();
            oDTDisplaySource1 = oDTDisplay.Clone();
            oDTDisplaySource2 = oDTDisplay.Clone();

            oDTDisplaySource1.TableName = "Source1";
            oDTDisplaySource2.TableName = "Source2";
            oDTPair.TableName = "Pair";

            ds.Tables.Add(oDTDisplaySource1);
            ds.Tables.Add(oDTDisplaySource2);
            ds.Tables.Add(oDTPair);

            DataColumn oDCPair = oDTPair.Columns[Pair.COLUMN_PAIRID];
            DataColumn oDCPairSource1 = oDTDisplaySource1.Columns[AddedDisplayColumns.COLUMN_PAIRID];
            DataColumn oDCPairSource2 = oDTDisplaySource2.Columns[AddedDisplayColumns.COLUMN_PAIRID];
            DataRelation oDataRelPairSource1 = new DataRelation("PairSource1", oDCPair, oDCPairSource1);
            DataRelation oDataRelPairSource2 = new DataRelation("PairSource2", oDCPair, oDCPairSource2);
            ds.Relations.Add(oDataRelPairSource1);
            ds.Relations.Add(oDataRelPairSource2);

            return ds;
        }

        #region "UnMatched Data"
        private string GetUnMatchXML(DataTable oDTDisplay, DataTable oDTSource1, DataTable oDTSource2
            , Dictionary<int, DisplayToSourceColumnMapping> oDictColMapping, MatchSetSubSetCombinationInfo oSubSetCombination)
        {
            DataSet ds;
            DataTable oDTDisplaySource1;
            DataTable oDTDisplaySource2;

            ds = new DataSet();
            oDTDisplaySource1 = oDTDisplay.Clone();
            oDTDisplaySource2 = oDTDisplay.Clone();

            oDTDisplaySource1.TableName = "Source1";
            oDTDisplaySource2.TableName = "Source2";

            this.PopulateDisplayData1ForUnMatch(oDTDisplaySource1, oDTSource1, oDictColMapping, oSubSetCombination.Source1);
            this.PopulateDisplayData2ForUnMatch(oDTDisplaySource2, oDTSource2, oDictColMapping, oSubSetCombination.Source2);

            ds.Tables.Add(oDTDisplaySource1);
            ds.Tables.Add(oDTDisplaySource2);

            return MatchingHelper.GenerateXMLFromDataSet(ds);
        }

        private void PopulateDisplayData1ForUnMatch(DataTable oDTDisplay, DataTable oDTSource1
            , Dictionary<int, DisplayToSourceColumnMapping> oDictColMapping, MatchSetMatchingSourceDataImportInfo oSource1)
        {
            for (int r = 0; r < oDTSource1.Rows.Count; r++)
            {
                DataRow drSource1 = oDTSource1.Rows[r];
                if (drSource1.IsNull(AddedFieldsForMatching.COLUMN_FULLMATCHPAIRID) && drSource1.IsNull(AddedFieldsForMatching.COLUMN_PARTIALMATCHPAIRID))//only for Matched rows
                {
                    DataRow drDisplay = oDTDisplay.NewRow();
                    foreach (KeyValuePair<int, DisplayToSourceColumnMapping> keyValue in oDictColMapping)
                    {
                        int displayColumnOrdinal = keyValue.Key;
                        if (keyValue.Value.source1ColumnOrdinal.HasValue)
                            drDisplay[displayColumnOrdinal] = drSource1[keyValue.Value.source1ColumnOrdinal.Value];
                    }
                    if (oDTSource1.Columns.Contains(AddedDisplayColumns.COLUMN_EXCELROWNUMBER))
                        drDisplay[AddedDisplayColumns.COLUMN_EXCELROWNUMBER] = drSource1[AddedDisplayColumns.COLUMN_EXCELROWNUMBER];

                    drDisplay[AddedDisplayColumns.COLUMN_MATCHSET_MATCHING_SOURCE_DATAIMPORTID] = oSource1.MatchSetMatchingSourceDataImportID.Value;
                    drDisplay[AddedDisplayColumns.COLUMN_MATCHING_SOURCE_DATAIMPORTID] = oSource1.MatchingSourceDataImportID.Value;
                    drDisplay[AddedDisplayColumns.COLUMN_SUBSET_NAME] = oSource1.SubSetName;
                    drDisplay[AddedDisplayColumns.COLUMN_IS_AUTOMATIC_MATCH] = true;

                    oDTDisplay.Rows.Add(drDisplay);
                }
            }
        }

        private void PopulateDisplayData2ForUnMatch(DataTable oDTDisplay, DataTable oDTSource2
            , Dictionary<int, DisplayToSourceColumnMapping> oDictColMapping, MatchSetMatchingSourceDataImportInfo oSource2)
        {
            for (int r = 0; r < oDTSource2.Rows.Count; r++)
            {
                DataRow drSource2 = oDTSource2.Rows[r];
                if (drSource2.IsNull(AddedFieldsForMatching.COLUMN_FULLMATCHPAIRID) && drSource2.IsNull(AddedFieldsForMatching.COLUMN_PARTIALMATCHPAIRID))//only for Matched rows
                {
                    DataRow drDisplay = oDTDisplay.NewRow();
                    foreach (KeyValuePair<int, DisplayToSourceColumnMapping> keyValue in oDictColMapping)
                    {
                        int displayColumnOrdinal = keyValue.Key;
                        if (keyValue.Value.source2ColumnOrdinal.HasValue)
                            drDisplay[displayColumnOrdinal] = drSource2[keyValue.Value.source2ColumnOrdinal.Value];
                    }
                    if (oDTSource2.Columns.Contains(AddedDisplayColumns.COLUMN_EXCELROWNUMBER))
                        drDisplay[AddedDisplayColumns.COLUMN_EXCELROWNUMBER] = drSource2[AddedDisplayColumns.COLUMN_EXCELROWNUMBER];

                    drDisplay[AddedDisplayColumns.COLUMN_MATCHSET_MATCHING_SOURCE_DATAIMPORTID] = oSource2.MatchSetMatchingSourceDataImportID.Value;
                    drDisplay[AddedDisplayColumns.COLUMN_MATCHING_SOURCE_DATAIMPORTID] = oSource2.MatchingSourceDataImportID.Value;
                    drDisplay[AddedDisplayColumns.COLUMN_SUBSET_NAME] = oSource2.SubSetName;
                    drDisplay[AddedDisplayColumns.COLUMN_IS_AUTOMATIC_MATCH] = true;
                    oDTDisplay.Rows.Add(drDisplay);
                }
            }
        }
        #endregion

        #region "Partial Match"
        private string GetPartialMatchXML(DataTable oDTDisplay, DataTable oDTSource1, DataTable oDTSource2, DataTable oDTPair
            , Dictionary<int, DisplayToSourceColumnMapping> oDictColMapping, MatchSetSubSetCombinationInfo oSubSetCombination)
        {
            DataSet oDS = this.GetResultDataSet(oDTDisplay, oDTPair);
            this.PopulateDisplayData1ForPartialMatch(oDS.Tables["Source1"], oDTSource1, oDictColMapping, oSubSetCombination.Source1);
            this.PopulateDisplayData2ForPartialMatch(oDS.Tables["Source2"], oDTSource2, oDictColMapping, oSubSetCombination.Source2);


            return MatchingHelper.GenerateXMLFromDataSet(oDS);
        }

        private void PopulateDisplayData1ForPartialMatch(DataTable oDTDisplay, DataTable oDTSource1
            , Dictionary<int, DisplayToSourceColumnMapping> oDictColMapping, MatchSetMatchingSourceDataImportInfo oSource1)
        {
            for (int r = 0; r < oDTSource1.Rows.Count; r++)
            {
                DataRow drSource1 = oDTSource1.Rows[r];
                if (drSource1.IsNull(AddedFieldsForMatching.COLUMN_FULLMATCHPAIRID) && !drSource1.IsNull(AddedFieldsForMatching.COLUMN_PARTIALMATCHPAIRID))//only for Matched rows
                {
                    DataRow drDisplay = oDTDisplay.NewRow();
                    foreach (KeyValuePair<int, DisplayToSourceColumnMapping> keyValue in oDictColMapping)
                    {
                        int displayColumnOrdinal = keyValue.Key;
                        if (keyValue.Value.source1ColumnOrdinal.HasValue)
                            drDisplay[displayColumnOrdinal] = drSource1[keyValue.Value.source1ColumnOrdinal.Value];
                    }
                    if (oDTSource1.Columns.Contains(AddedFieldsForMatching.COLUMN_PARTIALMATCHPAIRID))
                        drDisplay[AddedDisplayColumns.COLUMN_PAIRID] = drSource1[AddedFieldsForMatching.COLUMN_PARTIALMATCHPAIRID];

                    if (oDTSource1.Columns.Contains(AddedDisplayColumns.COLUMN_EXCELROWNUMBER))
                        drDisplay[AddedDisplayColumns.COLUMN_EXCELROWNUMBER] = drSource1[AddedDisplayColumns.COLUMN_EXCELROWNUMBER];

                    drDisplay[AddedDisplayColumns.COLUMN_MATCHSET_MATCHING_SOURCE_DATAIMPORTID] = oSource1.MatchSetMatchingSourceDataImportID.Value;
                    drDisplay[AddedDisplayColumns.COLUMN_MATCHING_SOURCE_DATAIMPORTID] = oSource1.MatchingSourceDataImportID.Value;
                    drDisplay[AddedDisplayColumns.COLUMN_SUBSET_NAME] = oSource1.SubSetName;
                    drDisplay[AddedDisplayColumns.COLUMN_IS_AUTOMATIC_MATCH] = true;
                    oDTDisplay.Rows.Add(drDisplay);
                }
            }
        }

        private void PopulateDisplayData2ForPartialMatch(DataTable oDTDisplay, DataTable oDTSource2
            , Dictionary<int, DisplayToSourceColumnMapping> oDictColMapping, MatchSetMatchingSourceDataImportInfo oSource2)
        {
            for (int r = 0; r < oDTSource2.Rows.Count; r++)
            {
                DataRow drSource2 = oDTSource2.Rows[r];
                if (drSource2.IsNull(AddedFieldsForMatching.COLUMN_FULLMATCHPAIRID) && !drSource2.IsNull(AddedFieldsForMatching.COLUMN_PARTIALMATCHPAIRID))//only for Matched rows
                {
                    DataRow drDisplay = oDTDisplay.NewRow();
                    foreach (KeyValuePair<int, DisplayToSourceColumnMapping> keyValue in oDictColMapping)
                    {
                        int displayColumnOrdinal = keyValue.Key;
                        if (keyValue.Value.source2ColumnOrdinal.HasValue)
                            drDisplay[displayColumnOrdinal] = drSource2[keyValue.Value.source2ColumnOrdinal.Value];
                    }
                    if (oDTSource2.Columns.Contains(AddedFieldsForMatching.COLUMN_PARTIALMATCHPAIRID))
                        drDisplay[AddedDisplayColumns.COLUMN_PAIRID] = drSource2[AddedFieldsForMatching.COLUMN_PARTIALMATCHPAIRID];

                    if (oDTSource2.Columns.Contains(AddedDisplayColumns.COLUMN_EXCELROWNUMBER))
                        drDisplay[AddedDisplayColumns.COLUMN_EXCELROWNUMBER] = drSource2[AddedDisplayColumns.COLUMN_EXCELROWNUMBER];

                    drDisplay[AddedDisplayColumns.COLUMN_MATCHSET_MATCHING_SOURCE_DATAIMPORTID] = oSource2.MatchSetMatchingSourceDataImportID.Value;
                    drDisplay[AddedDisplayColumns.COLUMN_MATCHING_SOURCE_DATAIMPORTID] = oSource2.MatchingSourceDataImportID.Value;
                    drDisplay[AddedDisplayColumns.COLUMN_SUBSET_NAME] = oSource2.SubSetName;
                    drDisplay[AddedDisplayColumns.COLUMN_IS_AUTOMATIC_MATCH] = true;

                    oDTDisplay.Rows.Add(drDisplay);
                }
            }
        }

        //generates XML String for Partial Match Set
        private void ProcessPartialMatching_ToBeRemoved(DataTable oDTSource1, DataTable oDTSource2, string filterExpressionSource1, string filterExpressionSource2, DataTable oDTPair)
        {
            string matchingFilterExpression1 = "";
            string matchingFilterExpression2 = "";

            //Get matching rows and mark them with pair id
            int pairID = 1;
            for (int i = 0; i < oDTSource1.Rows.Count; i++)
            {
                DataRow drSource1 = oDTSource1.Rows[i];
                if (drSource1.IsNull(AddedFieldsForMatching.COLUMN_FULLMATCHPAIRID) && drSource1.IsNull(AddedFieldsForMatching.COLUMN_PARTIALMATCHPAIRID))
                {
                    try
                    {
                        matchingFilterExpression1 = string.Format(filterExpressionSource1, oDTSource1.Rows[i].ItemArray);
                        matchingFilterExpression2 = string.Format(filterExpressionSource2, oDTSource1.Rows[i].ItemArray);
                        DataRow[] matchingRowsFromSource1 = null;
                        DataRow[] matchingRowsFromSource2 = null;
                        try
                        {
                            //Apply filter expression on Source1 from Source1
                            matchingRowsFromSource1 = oDTSource1.Select(matchingFilterExpression1);

                            //Apply filter expression on Source2 from Source1
                            matchingRowsFromSource2 = oDTSource2.Select(matchingFilterExpression2);
                        }
                        catch (Exception ex)
                        {
                            Helper.LogError("Error while applying filter in Partial Match in Row" + i.ToString() + " " + ex.Message, this.CompanyUserInfo);
                        }
                        if (matchingRowsFromSource2.Length > 0)//If matching is found
                        {
                            //Mark source2 rows with pairId
                            foreach (DataRow dr in matchingRowsFromSource2)
                            {
                                dr[AddedFieldsForMatching.COLUMN_PARTIALMATCHPAIRID] = pairID;
                            }

                            //Mark source1 rows with pairId
                            foreach (DataRow dr in matchingRowsFromSource1)
                            {
                                dr[AddedFieldsForMatching.COLUMN_PARTIALMATCHPAIRID] = pairID;
                            }

                            //Create an entry in Partial-Pair table
                            DataRow drPair = oDTPair.NewRow();
                            drPair[Pair.COLUMN_PAIRID] = pairID;
                            oDTPair.Rows.Add(drPair);
                            pairID++;
                        }
                    }
                    catch (ArgumentNullException ex1)
                    {
                        Helper.LogError("Error while partial matching in Source1 row:" + i.ToString() + " " + ex1.Message, this.CompanyUserInfo);
                    }
                    catch (FormatException ex2)
                    {
                        Helper.LogError("Error while partial matching in Source1 row:" + i.ToString() + " " + ex2.Message, this.CompanyUserInfo);
                    }
                    catch (Exception ex)
                    {
                        Helper.LogError("Error while partial matching in Source1 row:" + i.ToString() + " " + ex.Message, this.CompanyUserInfo);
                    }
                }
            }
        }

        private void ProcessPartialMatching(DataTable oDTSource1, DataTable oDTSource2, DataTable oDTPair, List<MatchingConfigurationInfo> oConfigList)
        {
            string matchingFilterExpression1 = "";
            string matchingFilterExpression2 = "";

            //Get partial key configuration list
            List<MatchingConfigurationInfo> oPartialMatchConfigList = oConfigList.FindAll(r => r.IsPartialMatching.GetValueOrDefault());
            if (oPartialMatchConfigList != null && oPartialMatchConfigList.Count > 0)//Processing will happen if there are some partial keys defined
            {
                //Get matching rows and mark them with pair id
                int pairID = 1;
                for (int i = 0; i < oDTSource1.Rows.Count; i++)
                {
                    DataRow drSource1 = oDTSource1.Rows[i];
                    if (drSource1.IsNull(AddedFieldsForMatching.COLUMN_FULLMATCHPAIRID) && drSource1.IsNull(AddedFieldsForMatching.COLUMN_PARTIALMATCHPAIRID))
                    {
                        try
                        {
                            matchingFilterExpression1 = this.GetFilterExpressionFromConfigList(oPartialMatchConfigList, drSource1, Enums.MatchingSourceType.Source1, Enums.MatchingType.partialMatch);
                            matchingFilterExpression2 = this.GetFilterExpressionFromConfigList(oPartialMatchConfigList, drSource1, Enums.MatchingSourceType.Source2, Enums.MatchingType.partialMatch);

                            DataRow[] matchingRowsFromSource1 = null;
                            DataRow[] matchingRowsFromSource2 = null;
                            try
                            {
                                //Apply filter expression on Source1 from Source1
                                matchingRowsFromSource1 = oDTSource1.Select(matchingFilterExpression1);

                                //Apply filter expression on Source2 from Source1
                                matchingRowsFromSource2 = oDTSource2.Select(matchingFilterExpression2);
                            }
                            catch (Exception ex)
                            {
                                Helper.LogError("Error while applying filter in Partial Match in Row" + i.ToString() + " " + ex.Message, this.CompanyUserInfo );
                                throw ex;
                            }
                            if (matchingRowsFromSource2.Length > 0)//If matching is found
                            {
                                //Mark source2 rows with pairId
                                foreach (DataRow dr in matchingRowsFromSource2)
                                {
                                    dr[AddedFieldsForMatching.COLUMN_PARTIALMATCHPAIRID] = pairID;
                                }

                                //Mark source1 rows with pairId
                                foreach (DataRow dr in matchingRowsFromSource1)
                                {
                                    dr[AddedFieldsForMatching.COLUMN_PARTIALMATCHPAIRID] = pairID;
                                }

                                //Create an entry in Partial-Pair table
                                DataRow drPair = oDTPair.NewRow();
                                drPair[Pair.COLUMN_PAIRID] = pairID;
                                oDTPair.Rows.Add(drPair);
                                pairID++;
                            }
                        }
                        catch (ArgumentNullException ex1)
                        {
                            Helper.LogError("Error while partial matching in Source1 row:" + i.ToString() + " " + ex1.Message, this.CompanyUserInfo);
                            throw ex1;
                        }
                        catch (FormatException ex2)
                        {
                            Helper.LogError("Error while partial matching in Source1 row:" + i.ToString() + " " + ex2.Message, this.CompanyUserInfo);
                            throw ex2;
                        }
                        catch (Exception ex)
                        {
                            Helper.LogError("Error while partial matching in Source1 row:" + i.ToString() + " " + ex.Message, this.CompanyUserInfo);
                            throw ex;
                        }
                    }
                }
            }
        }
        #endregion

        #region "Full match"
        private string GetFullMatchXML(DataTable oDTDisplay, DataTable oDTSource1, DataTable oDTSource2, DataTable oDTPair
            , Dictionary<int, DisplayToSourceColumnMapping> oDictColMapping, MatchSetSubSetCombinationInfo oSubSetCombination)
        {
            DataSet oDS = this.GetResultDataSet(oDTDisplay, oDTPair);

            this.PopulateDisplayData1ForFullMatch(oDS.Tables["Source1"], oDTSource1, oDictColMapping, oSubSetCombination.Source1);
            this.PopulateDisplayData2ForFullMatch(oDS.Tables["Source2"], oDTSource2, oDictColMapping, oSubSetCombination.Source2);

            return MatchingHelper.GenerateXMLFromDataSet(oDS);

        }

        private void PopulateDisplayData1ForFullMatch(DataTable oDTDisplay, DataTable oDTSource1
            , Dictionary<int, DisplayToSourceColumnMapping> oDictColMapping, MatchSetMatchingSourceDataImportInfo oSource1)
        {
            for (int r = 0; r < oDTSource1.Rows.Count; r++)
            {
                DataRow drSource1 = oDTSource1.Rows[r];
                if (!drSource1.IsNull(AddedFieldsForMatching.COLUMN_FULLMATCHPAIRID))//only for Matched rows
                {
                    DataRow drDisplay = oDTDisplay.NewRow();
                    foreach (KeyValuePair<int, DisplayToSourceColumnMapping> keyValue in oDictColMapping)
                    {
                        int displayColumnOrdinal = keyValue.Key;
                        if (keyValue.Value.source1ColumnOrdinal.HasValue)
                            drDisplay[displayColumnOrdinal] = drSource1[keyValue.Value.source1ColumnOrdinal.Value];
                    }
                    if (oDTSource1.Columns.Contains(AddedFieldsForMatching.COLUMN_FULLMATCHPAIRID))
                        drDisplay[AddedDisplayColumns.COLUMN_PAIRID] = drSource1[AddedFieldsForMatching.COLUMN_FULLMATCHPAIRID];

                    if (oDTSource1.Columns.Contains(AddedDisplayColumns.COLUMN_EXCELROWNUMBER))
                        drDisplay[AddedDisplayColumns.COLUMN_EXCELROWNUMBER] = drSource1[AddedDisplayColumns.COLUMN_EXCELROWNUMBER];

                    drDisplay[AddedDisplayColumns.COLUMN_MATCHSET_MATCHING_SOURCE_DATAIMPORTID] = oSource1.MatchSetMatchingSourceDataImportID.Value;
                    drDisplay[AddedDisplayColumns.COLUMN_MATCHING_SOURCE_DATAIMPORTID] = oSource1.MatchingSourceDataImportID.Value;
                    drDisplay[AddedDisplayColumns.COLUMN_SUBSET_NAME] = oSource1.SubSetName;
                    drDisplay[AddedDisplayColumns.COLUMN_IS_AUTOMATIC_MATCH] = true;

                    oDTDisplay.Rows.Add(drDisplay);
                }
            }
        }

        private void PopulateDisplayData2ForFullMatch(DataTable oDTDisplay, DataTable oDTSource2
            , Dictionary<int, DisplayToSourceColumnMapping> oDictColMapping, MatchSetMatchingSourceDataImportInfo oSource2)
        {
            for (int r = 0; r < oDTSource2.Rows.Count; r++)
            {
                DataRow drSource2 = oDTSource2.Rows[r];
                if (!drSource2.IsNull(AddedFieldsForMatching.COLUMN_FULLMATCHPAIRID))//only for Matched rows
                {
                    DataRow drDisplay = oDTDisplay.NewRow();
                    foreach (KeyValuePair<int, DisplayToSourceColumnMapping> keyValue in oDictColMapping)
                    {
                        int displayColumnOrdinal = keyValue.Key;
                        if (keyValue.Value.source2ColumnOrdinal.HasValue)
                            drDisplay[displayColumnOrdinal] = drSource2[keyValue.Value.source2ColumnOrdinal.Value];
                    }
                    if (oDTSource2.Columns.Contains(AddedFieldsForMatching.COLUMN_FULLMATCHPAIRID))
                        drDisplay[AddedDisplayColumns.COLUMN_PAIRID] = drSource2[AddedFieldsForMatching.COLUMN_FULLMATCHPAIRID];

                    if (oDTSource2.Columns.Contains(AddedDisplayColumns.COLUMN_EXCELROWNUMBER))
                        drDisplay[AddedDisplayColumns.COLUMN_EXCELROWNUMBER] = drSource2[AddedDisplayColumns.COLUMN_EXCELROWNUMBER];

                    drDisplay[AddedDisplayColumns.COLUMN_MATCHSET_MATCHING_SOURCE_DATAIMPORTID] = oSource2.MatchSetMatchingSourceDataImportID.Value;
                    drDisplay[AddedDisplayColumns.COLUMN_MATCHING_SOURCE_DATAIMPORTID] = oSource2.MatchingSourceDataImportID.Value;
                    drDisplay[AddedDisplayColumns.COLUMN_SUBSET_NAME] = oSource2.SubSetName;
                    drDisplay[AddedDisplayColumns.COLUMN_IS_AUTOMATIC_MATCH] = true;

                    oDTDisplay.Rows.Add(drDisplay);
                }
            }
        }

        //Generates XML string for Matched Set
        private void ProcessFullMatching_ToBeRemoved(DataTable oDTSource1, DataTable oDTSource2, string filterExpressionSource1, string filterExpressionSource2, DataTable oDTPair)
        {
            string matchingFilterExpression1 = "";
            string matchingFilterExpression2 = "";

            //Get matching rows and mark them with pair id
            int pairID = 1;
            for (int i = 0; i < oDTSource1.Rows.Count; i++)
            {

                DataRow drSource1 = oDTSource1.Rows[i];
                if (drSource1.IsNull(AddedFieldsForMatching.COLUMN_FULLMATCHPAIRID))
                {
                    try
                    {
                        matchingFilterExpression1 = string.Format(filterExpressionSource1, oDTSource1.Rows[i].ItemArray);
                        matchingFilterExpression2 = string.Format(filterExpressionSource2, oDTSource1.Rows[i].ItemArray);
                        DataRow[] matchingRowsFromSource1 = null;
                        DataRow[] matchingRowsFromSource2 = null;

                        try
                        {
                            //Apply filter expression on Source1 from Source1
                            matchingRowsFromSource1 = oDTSource1.Select(matchingFilterExpression1);

                            //Apply filter expression on Source2 from Source1
                            matchingRowsFromSource2 = oDTSource2.Select(matchingFilterExpression2);
                        }
                        catch (Exception ex)
                        {
                            Helper.LogError("Error while applying filter in Full Match in Row" + i.ToString() + " " + ex.Message, this.CompanyUserInfo);
                        }
                        if (matchingRowsFromSource2.Length > 0)//If matching is found
                        {
                            //Mark source2 rows with pairId
                            foreach (DataRow dr in matchingRowsFromSource2)
                            {
                                dr[AddedFieldsForMatching.COLUMN_FULLMATCHPAIRID] = pairID;
                            }

                            //Mark source1 rows with pairId
                            foreach (DataRow dr in matchingRowsFromSource1)
                            {
                                dr[AddedFieldsForMatching.COLUMN_FULLMATCHPAIRID] = pairID;
                            }

                            //Create an entry in Pair table
                            DataRow drPair = oDTPair.NewRow();
                            drPair[Pair.COLUMN_PAIRID] = pairID;
                            oDTPair.Rows.Add(drPair);

                            pairID++;
                        }

                    }
                    catch (ArgumentNullException ex1)
                    {
                        Helper.LogError("Error while Full matching in Source1 row:" + i.ToString() + " " + ex1.Message, this.CompanyUserInfo);
                    }
                    catch (FormatException ex2)
                    {
                        Helper.LogError("Error while Full matching in Source1 row:" + i.ToString() + " " + ex2.Message, this.CompanyUserInfo);
                    }
                    catch (Exception ex)
                    {
                        Helper.LogError("Error while Full Matching row:" + i.ToString() + " " + ex.Message, this.CompanyUserInfo);

                    }
                }
            }
        }

        private void ProcessFullMatching(DataTable oDTSource1, DataTable oDTSource2, DataTable oDTPair, List<MatchingConfigurationInfo> oConfigList)
        {
            string matchingFilterExpression1 = "";
            string matchingFilterExpression2 = "";

            //Get partial key configuration list
            List<MatchingConfigurationInfo> oFullMatchConfigList = oConfigList.FindAll(r => r.IsMatching.GetValueOrDefault());

            //Get matching rows and mark them with pair id
            int pairID = 1;
            if (oFullMatchConfigList != null && oFullMatchConfigList.Count > 0)
            {
                for (int i = 0; i < oDTSource1.Rows.Count; i++)
                {

                    DataRow drSource1 = oDTSource1.Rows[i];
                    if (drSource1.IsNull(AddedFieldsForMatching.COLUMN_FULLMATCHPAIRID))
                    {
                        try
                        {
                            //matchingFilterExpression1 = string.Format(filterExpressionSource1, oDTSource1.Rows[i].ItemArray);
                            //matchingFilterExpression2 = string.Format(filterExpressionSource2, oDTSource1.Rows[i].ItemArray);
                            matchingFilterExpression1 = this.GetFilterExpressionFromConfigList(oFullMatchConfigList, drSource1, Enums.MatchingSourceType.Source1, Enums.MatchingType.fullMatch);
                            matchingFilterExpression2 = this.GetFilterExpressionFromConfigList(oFullMatchConfigList, drSource1, Enums.MatchingSourceType.Source2, Enums.MatchingType.fullMatch);

                            DataRow[] matchingRowsFromSource1 = null;
                            DataRow[] matchingRowsFromSource2 = null;

                            try
                            {
                                //Apply filter expression on Source1 from Source1
                                matchingRowsFromSource1 = oDTSource1.Select(matchingFilterExpression1);

                                //Apply filter expression on Source2 from Source1
                                matchingRowsFromSource2 = oDTSource2.Select(matchingFilterExpression2);
                            }
                            catch (Exception ex)
                            {
                                Helper.LogError("Error while applying filter in Full Match in Row" + i.ToString() + " " + ex.Message, this.CompanyUserInfo);
                                throw ex;
                            }
                            if (matchingRowsFromSource2.Length > 0)//If matching is found
                            {
                                //Mark source2 rows with pairId
                                foreach (DataRow dr in matchingRowsFromSource2)
                                {
                                    dr[AddedFieldsForMatching.COLUMN_FULLMATCHPAIRID] = pairID;
                                }

                                //Mark source1 rows with pairId
                                foreach (DataRow dr in matchingRowsFromSource1)
                                {
                                    dr[AddedFieldsForMatching.COLUMN_FULLMATCHPAIRID] = pairID;
                                }

                                //Create an entry in Pair table
                                DataRow drPair = oDTPair.NewRow();
                                drPair[Pair.COLUMN_PAIRID] = pairID;
                                oDTPair.Rows.Add(drPair);

                                pairID++;
                            }

                        }
                        catch (ArgumentNullException ex1)
                        {
                            Helper.LogError("Error while Full matching in Source1 row:" + i.ToString() + " " + ex1.Message, this.CompanyUserInfo);
                            throw ex1;
                        }
                        catch (FormatException ex2)
                        {
                            Helper.LogError("Error while Full matching in Source1 row:" + i.ToString() + " " + ex2.Message, this.CompanyUserInfo);
                            throw ex2;
                        }
                        catch (Exception ex)
                        {
                            Helper.LogError("Error while Full Matching row:" + i.ToString() + " " + ex.Message, this.CompanyUserInfo);
                            throw ex;
                        }
                    }
                }
            }
        }
        #endregion

        #region "Display Data Structure"
        //Stores mapping between DisplayDataTable, Column-Configuration, Source1, Source2 into a dictionary
        private void MapDisplayDataTableWithColumnConfig(DataTable oDTDisplay, List<MatchingConfigurationInfo> oConfigList
            , DataTable oDTSource1, DataTable oDTSource2, Dictionary<int, DisplayToSourceColumnMapping> oDictColMapping)
        {
            foreach (MatchingConfigurationInfo oConfig in oConfigList)
            {
                if (oConfig.IsDisplayColumn.HasValue && oConfig.IsDisplayColumn.Value)
                {
                    //find this column in display datatable
                    DataColumn oDc = oDTDisplay.Columns[oConfig.DisplayColumnName];

                    if (oDc != null)
                    {
                        DisplayToSourceColumnMapping oColMap = new DisplayToSourceColumnMapping();
                        oColMap.displayColumnName = oDc.ColumnName;
                        if (oConfig.MatchingSource1ColumnID.HasValue)
                        {
                            oColMap.source1ColumnID = oConfig.MatchingSource1ColumnID.Value;
                            oColMap.source1ColumnName = oConfig.MatchingSource1ColumnName;
                            DataColumn oDCSource1 = oDTSource1.Columns[oConfig.MatchingSource1ColumnName];
                            if (oDCSource1 != null)
                                oColMap.source1ColumnOrdinal = oDCSource1.Ordinal;

                        }
                        if (oConfig.MatchingSource2ColumnID.HasValue)
                        {
                            oColMap.source2ColumnID = oConfig.MatchingSource2ColumnID.Value;
                            oColMap.source2ColumnName = oConfig.MatchingSource2ColumnName;
                            DataColumn oDCSource2 = oDTSource2.Columns[oConfig.MatchingSource2ColumnName];
                            if (oDCSource2 != null)
                                oColMap.source2ColumnOrdinal = oDCSource2.Ordinal;
                        }
                        oDictColMapping.Add(oDc.Ordinal, oColMap);
                    }
                }
            }
        }

        //Returns a DataTable as per DisplayColumns from Configuration List
        private DataTable GetDisplayDataTable(List<MatchingConfigurationInfo> oConfigList)
        {
            DataTable oDTDisplay = new DataTable();
            try
            {
                foreach (MatchingConfigurationInfo oConfig in oConfigList)
                {
                    if (oConfig.IsDisplayColumn.HasValue && oConfig.IsDisplayColumn.Value)
                    {
                        DataColumn oDC = new DataColumn();
                        oDC.ColumnName = oConfig.DisplayColumnName;
                        switch (oConfig.DataTypeID.Value)
                        {
                            case (short)Enums.DataType.Boolean:
                                oDC.DataType = typeof(System.Boolean);
                                break;
                            case (short)Enums.DataType.DataTime:
                                oDC.DataType = typeof(System.DateTime);
                                break;
                            case (short)Enums.DataType.Decimal:
                                oDC.DataType = typeof(System.Decimal);
                                break;
                            case (short)Enums.DataType.Integer:
                                oDC.DataType = typeof(System.Int32);
                                break;
                            case (short)Enums.DataType.String:
                                oDC.DataType = typeof(System.String);
                                break;
                            default:
                                oDC.DataType = typeof(System.String);
                                break;

                        }
                        oDTDisplay.Columns.Add(oDC);

                    }
                }
                //Add PairID column in Display DataTable
                oDTDisplay.Columns.Add(AddedDisplayColumns.COLUMN_PAIRID, typeof(System.Int32));
                oDTDisplay.Columns.Add(AddedDisplayColumns.COLUMN_EXCELROWNUMBER, typeof(System.Int32));
                oDTDisplay.Columns.Add(AddedDisplayColumns.COLUMN_MATCHSET_MATCHING_SOURCE_DATAIMPORTID, typeof(System.Int64));
                oDTDisplay.Columns.Add(AddedDisplayColumns.COLUMN_SUBSET_NAME, typeof(System.String));
                oDTDisplay.Columns.Add(AddedDisplayColumns.COLUMN_REC_ITEM_NUMBER, typeof(System.String));
                oDTDisplay.Columns.Add(AddedDisplayColumns.COLUMN_IS_AUTOMATIC_MATCH, typeof(System.Boolean));
                oDTDisplay.Columns.Add(AddedDisplayColumns.COLUMN_MATCHING_SOURCE_DATAIMPORTID, typeof(System.Int64));
            }
            catch (Exception ex)
            {
                Helper.LogError("Error while creating display column DataTable for SubSetID:" + oConfigList[0].MatchSetSubSetCombinationID + " " + ex.Message, this.CompanyUserInfo);
                throw ex;
            }
            return oDTDisplay;
        }
        #endregion

        #region "Filter Expression"
        private string GetValidString(string strValue)
        {
            StringBuilder oSB = new StringBuilder(strValue);
            oSB.Replace("[", "[[]");//replacing wild card characters
            oSB.Replace("*", "[*]");//replacing wild card characters
            oSB.Replace("'", "''");//replacing "'" with "''"
            return oSB.ToString();
        }

        private string GetDefaultFilterExpressionFromConfig(MatchingConfigurationInfo oConfig, DataRow drSource1, Enums.MatchingSourceType sourceType)
        {
            StringBuilder oSBFilterExpression = new StringBuilder();
            short dataTypeID = oConfig.DataTypeID.Value;

            string columnToBeCompared = sourceType == Enums.MatchingSourceType.Source1 ? oConfig.MatchingSource1ColumnName : oConfig.MatchingSource2ColumnName;

            string dataType = "";
            switch (dataTypeID)
            {
                case (short)Enums.DataType.Boolean:
                    dataType = "System.Boolean";
                    bool boolDataValue;
                    if (Boolean.TryParse(drSource1[oConfig.MatchingSource1ColumnName].ToString(), out boolDataValue))
                    {
                        oSBFilterExpression.Append("Convert([");
                        oSBFilterExpression.Append(columnToBeCompared);
                        oSBFilterExpression.Append("]");
                        oSBFilterExpression.Append(",'");
                        oSBFilterExpression.Append(dataType);
                        oSBFilterExpression.Append("')");
                        oSBFilterExpression.Append(" = ");
                        oSBFilterExpression.Append(boolDataValue);
                    }
                    break;
                case (short)Enums.DataType.DataTime:
                    dataType = "System.DateTime";
                    DateTime dtDataValue;
                    if (DateTime.TryParse(drSource1[oConfig.MatchingSource1ColumnName].ToString(), out dtDataValue))
                    {
                        oSBFilterExpression.Append("Convert([");
                        oSBFilterExpression.Append(columnToBeCompared);
                        oSBFilterExpression.Append("]");
                        oSBFilterExpression.Append(",'");
                        oSBFilterExpression.Append(dataType);
                        oSBFilterExpression.Append("')");
                        oSBFilterExpression.Append(" = #");
                        oSBFilterExpression.Append(dtDataValue);
                        oSBFilterExpression.Append("#");
                    }
                    break;
                case (short)Enums.DataType.Decimal:
                    dataType = "System.Decimal";
                    decimal dcDataValue;
                    if (decimal.TryParse(drSource1[oConfig.MatchingSource1ColumnName].ToString(), out dcDataValue))
                    {
                        oSBFilterExpression.Append("Convert([");
                        oSBFilterExpression.Append(columnToBeCompared);
                        oSBFilterExpression.Append("]");
                        oSBFilterExpression.Append(",'");
                        oSBFilterExpression.Append(dataType);
                        oSBFilterExpression.Append("')");
                        oSBFilterExpression.Append(" = ");
                        oSBFilterExpression.Append(dcDataValue);
                    }
                    break;
                case (short)Enums.DataType.Integer:
                    dataType = "System.Int32";
                    int intDataValue;
                    if (int.TryParse(drSource1[oConfig.MatchingSource1ColumnName].ToString(), out intDataValue))
                    {
                        oSBFilterExpression.Append("Convert([");
                        oSBFilterExpression.Append(columnToBeCompared);
                        oSBFilterExpression.Append("]");
                        oSBFilterExpression.Append(",'");
                        oSBFilterExpression.Append(dataType);
                        oSBFilterExpression.Append("')");
                        oSBFilterExpression.Append(" = ");
                        oSBFilterExpression.Append(intDataValue);
                    }
                    break;
                case (short)Enums.DataType.String:
                    string strDataValue = this.GetValidString(drSource1[oConfig.MatchingSource1ColumnName].ToString());
                    oSBFilterExpression.Append("[");
                    oSBFilterExpression.Append(columnToBeCompared);
                    oSBFilterExpression.Append("]");
                    oSBFilterExpression.Append(" LIKE '");
                    oSBFilterExpression.Append(strDataValue);
                    oSBFilterExpression.Append("'");
                    break;
            }

            return oSBFilterExpression.ToString();
        }

        private string GetFilterExpressionFromRuleList(MatchingConfigurationInfo oConfig, DataRow drSource1, Enums.MatchingSourceType sourceType)
        {
            StringBuilder oSBFilterExpression = new StringBuilder();
            string baseColumnName = oConfig.MatchingSource1ColumnName;

            string columnToBeCompared = sourceType == Enums.MatchingSourceType.Source1 ? oConfig.MatchingSource1ColumnName : oConfig.MatchingSource2ColumnName;

            short dataTypeID = oConfig.DataTypeID.Value;
            foreach (MatchingConfigurationRuleInfo oConfigRule in oConfig.MatchingConfigurationRuleCollection)
            {
                if (!oSBFilterExpression.ToString().Equals(string.Empty))
                    oSBFilterExpression.Append(" AND ");
                oSBFilterExpression.Append(this.GetFilterExpressionFromRule(oConfigRule, drSource1, baseColumnName, columnToBeCompared, dataTypeID));
            }
            return oSBFilterExpression.ToString();
        }

        private string GetFilterExpressionFromRule(MatchingConfigurationRuleInfo oConfigRule, DataRow drSource1, string baseColumnName, string columnToBeCompared, short dataTypeID)
        {
            StringBuilder oSBFilterExpression = new StringBuilder();
            string dataType = "";
            switch (dataTypeID)
            {
                case (short)Enums.DataType.Boolean:
                    dataType = "System.Boolean";
                    bool boolDataValue;
                    if (Boolean.TryParse(drSource1[baseColumnName].ToString(), out boolDataValue))
                    {
                        oSBFilterExpression.Append("Convert([");
                        oSBFilterExpression.Append(columnToBeCompared);
                        oSBFilterExpression.Append("]");
                        oSBFilterExpression.Append(",'");
                        oSBFilterExpression.Append(dataType);
                        oSBFilterExpression.Append("')");
                        oSBFilterExpression.Append(" = ");
                        oSBFilterExpression.Append(boolDataValue);
                    }
                    break;
                case (short)Enums.DataType.DataTime:
                    dataType = "System.DateTime";
                    DateTime dtDataValue;
                    if (DateTime.TryParse(drSource1[baseColumnName].ToString(), out dtDataValue))
                    {
                        //apply rule on value, there can be only +- days, no percentage
                        double upperBound = Convert.ToDouble(oConfigRule.UpperBound.Value);
                        double lowerBound = Convert.ToDouble(oConfigRule.UpperBound.Value) * -1;

                        if (oConfigRule.OperatorID.Value == (short)Enums.OperatorType.Between)
                        {
                            oSBFilterExpression.Append(" ( Convert([");
                            oSBFilterExpression.Append(columnToBeCompared);
                            oSBFilterExpression.Append("]");
                            oSBFilterExpression.Append(",'");
                            oSBFilterExpression.Append(dataType);
                            oSBFilterExpression.Append("')");
                            oSBFilterExpression.Append(" >= #");
                            oSBFilterExpression.Append(dtDataValue.AddDays(lowerBound));
                            oSBFilterExpression.Append("#");

                            oSBFilterExpression.Append(" AND ");

                            oSBFilterExpression.Append("Convert([");
                            oSBFilterExpression.Append(columnToBeCompared);
                            oSBFilterExpression.Append("]");
                            oSBFilterExpression.Append(",'");
                            oSBFilterExpression.Append(dataType);
                            oSBFilterExpression.Append("')");
                            oSBFilterExpression.Append(" <= #");
                            oSBFilterExpression.Append(dtDataValue.AddDays(upperBound));
                            oSBFilterExpression.Append("#");
                            oSBFilterExpression.Append(" ) ");
                        }


                    }
                    break;
                case (short)Enums.DataType.String:
                    //string strDataValue = this.GetValidString(drSource1[baseColumnName].ToString());
                    string strDataValue = this.GetValidString(oConfigRule.Keywords);
                    switch (oConfigRule.OperatorID.Value)
                    {
                        case (short)Enums.OperatorType.Contains:
                            oSBFilterExpression.Append("[");
                            oSBFilterExpression.Append(columnToBeCompared);
                            oSBFilterExpression.Append("]");
                            oSBFilterExpression.Append(" LIKE '*");
                            oSBFilterExpression.Append(strDataValue);
                            oSBFilterExpression.Append("*'");
                            break;
                        case (short)Enums.OperatorType.Equals:
                            oSBFilterExpression.Append("[");
                            oSBFilterExpression.Append(columnToBeCompared);
                            oSBFilterExpression.Append("]");
                            oSBFilterExpression.Append(" LIKE '");
                            oSBFilterExpression.Append(strDataValue);
                            oSBFilterExpression.Append("'");
                            break;
                    }
                    break;
                case (short)Enums.DataType.Decimal:
                    decimal dcDataValue;
                    dataType = "System.Decimal";
                    decimal dcLowerBound = 0;
                    decimal dcUpperBound = 0;
                    if (decimal.TryParse(drSource1[baseColumnName].ToString(), out dcDataValue))
                    {
                        switch (oConfigRule.OperatorID.Value)
                        {
                            case (short)Enums.OperatorType.Between:
                                if (oConfigRule.ThresholdTypeID.Value == (short)Enums.ThresholdType.Fixed)
                                {
                                    dcLowerBound = dcDataValue - oConfigRule.LowerBound.Value;
                                    dcUpperBound = dcDataValue + oConfigRule.UpperBound.Value;
                                }
                                if (oConfigRule.ThresholdTypeID.Value == (short)Enums.ThresholdType.Percentage)
                                {
                                    dcLowerBound = dcDataValue - (oConfigRule.LowerBound.Value * dcDataValue) / 100;
                                    dcUpperBound = dcDataValue + (oConfigRule.UpperBound.Value * dcDataValue) / 100;
                                }
                                oSBFilterExpression.Append(" (");
                                oSBFilterExpression.Append("Convert([");
                                oSBFilterExpression.Append(columnToBeCompared);
                                oSBFilterExpression.Append("]");
                                oSBFilterExpression.Append(",'");
                                oSBFilterExpression.Append(dataType);
                                oSBFilterExpression.Append("')");
                                oSBFilterExpression.Append(" >= ");
                                oSBFilterExpression.Append(dcLowerBound);

                                oSBFilterExpression.Append(" AND ");

                                oSBFilterExpression.Append("Convert([");
                                oSBFilterExpression.Append(columnToBeCompared);
                                oSBFilterExpression.Append("]");
                                oSBFilterExpression.Append(",'");
                                oSBFilterExpression.Append(dataType);
                                oSBFilterExpression.Append("')");
                                oSBFilterExpression.Append(" <= ");
                                oSBFilterExpression.Append(dcUpperBound);
                                oSBFilterExpression.Append(") ");
                                break;
                        }
                    }
                    break;
            }

            return oSBFilterExpression.ToString();
        }

        private string GetFilterExpressionFromConfig(MatchingConfigurationInfo oConfig, DataRow drSource1, Enums.MatchingSourceType sourceType)
        {
            StringBuilder oSBFilterExpression = new StringBuilder();
            List<MatchingConfigurationRuleInfo> oMatchingConfigRuleList = oConfig.MatchingConfigurationRuleCollection;
            if (oMatchingConfigRuleList == null)
            {
                oSBFilterExpression.Append(this.GetDefaultFilterExpressionFromConfig(oConfig, drSource1, sourceType));
            }
            else
            {
                oSBFilterExpression.Append(this.GetFilterExpressionFromRuleList(oConfig, drSource1, sourceType));
            }

            return oSBFilterExpression.ToString();
        }

        private string GetFilterExpressionFromConfigList(List<MatchingConfigurationInfo> oConfigList, DataRow drSource1, Enums.MatchingSourceType sourceType, Enums.MatchingType matchingType)
        {
            StringBuilder oSBMainFilterExpression = new StringBuilder();
            switch (matchingType)
            {
                case Enums.MatchingType.fullMatch:
                    //Condition to aviod already fully Matched rows.
                    oSBMainFilterExpression.Append("IsNull([");
                    oSBMainFilterExpression.Append(AddedFieldsForMatching.COLUMN_FULLMATCHPAIRID);
                    oSBMainFilterExpression.Append("],-1)= -1");
                    break;
                case Enums.MatchingType.partialMatch:
                    //Condition to avoid already fully-matched and Partially-matched rows
                    oSBMainFilterExpression.Append("IsNull([");
                    oSBMainFilterExpression.Append(AddedFieldsForMatching.COLUMN_FULLMATCHPAIRID);
                    oSBMainFilterExpression.Append("],-1)= -1");
                    oSBMainFilterExpression.Append(" AND ");
                    oSBMainFilterExpression.Append("IsNull([");
                    oSBMainFilterExpression.Append(AddedFieldsForMatching.COLUMN_PARTIALMATCHPAIRID);
                    oSBMainFilterExpression.Append("],-1)= -1");
                    break;
            }
            foreach (MatchingConfigurationInfo oConfig in oConfigList)
            {
                StringBuilder oSBConfigFilterExpression = new StringBuilder();
                //Get filter expression based on current configuration
                oSBConfigFilterExpression.Append(this.GetFilterExpressionFromConfig(oConfig, drSource1, sourceType));
                if (!oSBConfigFilterExpression.ToString().Equals(string.Empty))
                {
                    oSBMainFilterExpression.Append(" AND ");
                    oSBMainFilterExpression.Append(oSBConfigFilterExpression.ToString());
                }
            }
            return oSBMainFilterExpression.ToString();
        }


        #endregion

        //Add extra fields to Data Sources
        private void AddFieldsToDataSources(DataTable oDTSource1, DataTable oDTSource2)
        {
            //Creates additional fields in Source1 and Source2 data tables.
            //Additional fields FullMatchPairID Int, PartialMatchPairID Int


            oDTSource1.Columns.Add(AddedFieldsForMatching.COLUMN_FULLMATCHPAIRID, typeof(System.Int32));
            oDTSource1.Columns.Add(AddedFieldsForMatching.COLUMN_PARTIALMATCHPAIRID, typeof(System.Int32));

            oDTSource2.Columns.Add(AddedFieldsForMatching.COLUMN_FULLMATCHPAIRID, typeof(System.Int32));
            oDTSource2.Columns.Add(AddedFieldsForMatching.COLUMN_PARTIALMATCHPAIRID, typeof(System.Int32));
        }
        #endregion
    }

    public struct DisplayToSourceColumnMapping
    {
        public string source1ColumnName { get; set; }
        public string source2ColumnName { get; set; }
        public long? source1ColumnID { get; set; }
        public long? source2ColumnID { get; set; }
        public int? source1ColumnOrdinal { get; set; }
        public int? source2ColumnOrdinal { get; set; }
        public string displayColumnName { get; set; }
    }
}
