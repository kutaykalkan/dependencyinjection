using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SkyStem.ART.Web.Classes.UserControl;
using SkyStem.ART.Client.Data;
using System.Data;
using SkyStem.ART.Web.Utility;
using SkyStem.ART.Client.Model.Matching;
using SkyStem.ART.Web.Data;
using SkyStem.ART.Client.Params.Matching;
using SkyStem.ART.Client.IServices;
using Telerik.Web.UI;
using System.IO;
using SkyStem.Library.Controls.TelerikWebControls;
using SkyStem.Library.Controls.TelerikWebControls.Data;
using System.Text;
using SkyStem.Language.LanguageUtility;
using SkyStem.ART.Web.Classes;
using SkyStem.ART.Client.Model;

namespace SkyStem.ART.Web.UserControls.Matching.Wizard
{
    public partial class MatchingSourceFilter : UserControlMatchingWizardBase
    {
        #region Properties And Variable
        protected string SelectionMsg = "";
        protected string SelectionMsgForAddtn = "";
        protected string RemoveMsg = "";
        protected string RemoveAllMsg = "";
        protected string qscMatchingSourceDataImportID = "";
        List<MatchingSourceDataImportHdrInfo> _MatchingSourceDataImportHdrInfoList = null;

        private List<MatchingSourceDataImportHdrInfo> MatchingSourceDataImportHdrInfoList
        {
            get
            {
                if (CurrentMatchSetHdrInfo != null)
                {
                    _MatchingSourceDataImportHdrInfoList = CurrentMatchSetHdrInfo.MatchingSourceDataImportHdrInfoList;
                }
                return _MatchingSourceDataImportHdrInfoList;
            }
        }


        List<MatchingSourceColumnInfo> _MatchingSourceColumnInfo = null;
        #endregion

        protected void Page_Init(object sender, EventArgs e)
        {
            SelectionMsg = LanguageUtil.GetValue(5000260);
            SelectionMsgForAddtn = LanguageUtil.GetValue(2013);
            RemoveMsg = LanguageUtil.GetValue(1781);
            RemoveAllMsg = LanguageUtil.GetValue(1781);
            qscMatchingSourceDataImportID = QueryStringConstants.MATCHING_SOURCE_DATA_IMPORT_ID;
            MatchingWizardStepType = ARTEnums.MatchingWizardSteps.MatchingSourceFilter;
            WizardStepID = (int)ARTEnums.WizardSteps.MatchingSourceFilter;
            rfvSubsetName.ErrorMessage = Helper.GetErrorMessage(WebEnums.FieldType.MandatoryField, 5000284);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            GridHelper.SetRecordCount(rgMappingColumns);
            GridHelper.SetRecordCount(rgSubSetData);

        }

        public override void RefreshData()
        {
            GridFilterData();
        }

        public override void LoadData()
        {
            //Added by Abhishek
            // SessionHelper.ClearGridDynamicFilterDataFromSession();
            SessionHelper.ClearDynamicFilterData(GetGridClientIDKey(rgMappingColumns));
            PageBase oPageBase = (PageBase)this.Page;
            Helper.ShowInputRequirementSection(oPageBase, 2345, 2346);
            uscMatchSetInfo.PopulateData(CurrentMatchSetHdrInfo);
            BindDataSourceDDL();
            hdnSelectedDataImportID.Value = ddlDataSource.SelectedItem.Value;
            PopulateGridData();
            if (this.IsEditMode)
                BindGrid();

            BindSubSetGrid();

        }

        protected void BindDataSourceDDL()
        {
            ddlDataSource.DataSource = MatchingSourceDataImportHdrInfoList;
            ddlDataSource.DataTextField = "MatchingSourceName";
            ddlDataSource.DataValueField = "MatchingSourceDataImportID";
            ddlDataSource.DataBind();
        }

        protected void ddlDataSource_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Added by Abhishek
            //SessionHelper.ClearGridDynamicFilterDataFromSession();
            SessionHelper.ClearDynamicFilterData(GetGridClientIDKey(rgMappingColumns));
            UpdateSessionData();
            hdnSelectedDataImportID.Value = ddlDataSource.SelectedItem.Value;
            PopulateGridData();

            if (this.IsEditMode)
                BindGrid();

            BindSubSetGrid();
        }

        /// <summary>
        /// Populate Grid Data add in Session for Filter/Sub set Grid 
        /// </summary>
        protected void PopulateGridData()
        {
            string stringSubSetXML = "";
            string stringXMLData = "";

            DataTable oSubSetDT = null;
            DataTable oOriginalDT = null;

            if (_MatchingSourceColumnInfo == null)
                _MatchingSourceColumnInfo = GetColumns();

            MatchingSourceDataImportHdrInfo oMatchingSourceDataImportHdrInfo = GetSelectedMatchingSourceDataImportHdrInfo();
            if (oMatchingSourceDataImportHdrInfo != null)
            {
                //**** Get Sub set Data and Sub Set Name from selected Match Set Matching Source Data Import
                if (oMatchingSourceDataImportHdrInfo.MatchSetMatchingSourceSubSetData != null)
                {
                    stringSubSetXML = oMatchingSourceDataImportHdrInfo.MatchSetMatchingSourceSubSetData.SubSetData;
                    txtSubSetName.Text = oMatchingSourceDataImportHdrInfo.MatchSetMatchingSourceSubSetData.SubSetName;
                }
                //**** Get Sub set Data and Sub Set Name from selected Match Set Matching Source Data Import

                //**** Get Sub set Data and Sub Set Name from selected Match Set Matching Source Data Import
                if (oMatchingSourceDataImportHdrInfo.MatchingSourceData != null)
                {
                    stringXMLData = oMatchingSourceDataImportHdrInfo.MatchingSourceData.Data;
                }
                //**** Get Sub set Data and Sub Set Name from selected Match Set Matching Source Data Import
            }

            //**** Get XmlData to DataTable
            if (stringXMLData != "")
                oOriginalDT = Helper.GetXmlToDataTable(stringXMLData, _MatchingSourceColumnInfo, oMatchingSourceDataImportHdrInfo.MatchingSourceTypeName);

            if (oOriginalDT == null)
                oOriginalDT = new DataTable();


            //**** Get SubSetXml to DataTable
            if (stringSubSetXML != "")
                oSubSetDT = Helper.GetXmlToDataTable(stringSubSetXML);

            if (oSubSetDT == null)
                oSubSetDT = new DataTable();

            UpdateFilterColumn(oOriginalDT, "");
            UpdateFilterColumn(oSubSetDT, "");

            Session[SessionConstants.MATCHING_SOURCE_FILTER_DATA] = oOriginalDT;
            Session[SessionConstants.MATCHING_SOURCE_SUBSET_DATA] = oSubSetDT;
        }

        /// <summary>
        /// Get Selected Matching Source Data Import Hdr Info from Session
        /// </summary>
        /// <returns></returns>
        protected MatchingSourceDataImportHdrInfo GetSelectedMatchingSourceDataImportHdrInfo()
        {
            MatchingSourceDataImportHdrInfo oMatchingSourceDataImportHdrInfo = new MatchingSourceDataImportHdrInfo();

            long MatchingSourceDataImportID = 0;
            long.TryParse(hdnSelectedDataImportID.Value.ToString(), out MatchingSourceDataImportID);

            if (MatchingSourceDataImportHdrInfoList != null && MatchingSourceDataImportHdrInfoList.Count > 0)
                oMatchingSourceDataImportHdrInfo = MatchingSourceDataImportHdrInfoList.Find(p => p.MatchingSourceDataImportID == MatchingSourceDataImportID);

            return oMatchingSourceDataImportHdrInfo;
        }

        /// <summary>
        /// Add Filter Column to Data Table and Update Filter Text
        /// </summary>
        /// <param name="oDt"></param>
        /// <param name="strFilter"></param>
        private void UpdateFilterColumn(DataTable oDt, string strFilter)
        {
            if (oDt != null)
            {
                String Filter = LanguageUtil.GetValue(2853);
                DataColumn dc = null;
                if (!oDt.Columns.Contains(Filter))
                    dc = oDt.Columns.Add(Filter);

                foreach (DataRow oDr in oDt.Rows)
                {
                    if (oDr[Filter].ToString() == "")
                        oDr[Filter] = strFilter;
                }
            }
        }

        /// <summary>
        /// Get Selected Matching Source Column List from Session 
        /// </summary>
        /// <returns></returns>
        private List<MatchingSourceColumnInfo> GetColumns()
        {
            List<MatchingSourceColumnInfo> oMatchingSourceColumnInfo = new List<MatchingSourceColumnInfo>();
            MatchingSourceDataImportHdrInfo oMatchingSourceDataImportHdrInfo = GetSelectedMatchingSourceDataImportHdrInfo();
            if (oMatchingSourceDataImportHdrInfo != null)
                oMatchingSourceColumnInfo = oMatchingSourceDataImportHdrInfo.MatchingSourceColumnList;
            return oMatchingSourceColumnInfo;
        }

        protected void btnAddtoSubSet_Click(object sender, EventArgs e)
        {
            long MatchingSourceDataImportID = Convert.ToInt64(ddlDataSource.SelectedItem.Value);
            AddToSubSet(true);
            //UpdateSessionData(MatchingSourceDataImportID);
            rgSubSetData.Rebind();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            long MatchingSourceDataImportID = Convert.ToInt64(ddlDataSource.SelectedItem.Value);
            AddToSubSet(false);
            //UpdateSessionData(MatchingSourceDataImportID);
            rgSubSetData.Rebind();
        }

        /// <summary>
        /// Add Filter Grid Data to Subset Grid data
        /// </summary>
        private void AddToSubSet(bool selectAll)
        {
            String Filter = LanguageUtil.GetValue(2853);
            DataTable odtSubSetData = new DataTable();
            DataTable odtFilterData = new DataTable();

            if (_MatchingSourceColumnInfo == null)
                _MatchingSourceColumnInfo = GetColumns();

            if (selectAll)
            {
                if (Session[SessionConstants.MATCHING_SOURCE_FILTER_DATA] != null)
                    odtFilterData = (DataTable)Session[SessionConstants.MATCHING_SOURCE_FILTER_DATA];
            }
            else
            {
                DataTable odt = new DataTable();
                if (Session[SessionConstants.MATCHING_SOURCE_FILTER_DATA] != null)
                    odt = (DataTable)Helper.DeepClone((DataTable)Session[SessionConstants.MATCHING_SOURCE_FILTER_DATA]);

                odtFilterData = odt.Clone();
                GridItemCollection selectedItems = rgMappingColumns.SelectedItems;
                foreach (GridDataItem item in selectedItems)
                {
                    DataRow Odr1 = odt.Select(AddedGLTBSImportFields.EXCELROWNUMBER + "='" + item.GetDataKeyValue(AddedGLTBSImportFields.EXCELROWNUMBER) + "'").SingleOrDefault();
                    if (string.IsNullOrEmpty(Odr1[Filter].ToString()))
                        Odr1[Filter] = "-";
                    odtFilterData.ImportRow(Odr1);
                }
            }

            if (Session[SessionConstants.MATCHING_SOURCE_SUBSET_DATA] != null)
                odtSubSetData = (DataTable)Session[SessionConstants.MATCHING_SOURCE_SUBSET_DATA];


            if (odtSubSetData.Rows.Count == 0)
                odtSubSetData = odtFilterData.Clone();

            if (odtSubSetData.AsEnumerable().Select(row => (string)row[Filter].ToString()).Count() == odtSubSetData.Rows.Count)
                odtSubSetData.Rows.Clear();

            //*** Add New Filtered Rows in Grid Data Source
            IEnumerable<string> oSubSetIE = odtSubSetData.AsEnumerable().Select(row => (string)row[AddedGLTBSImportFields.EXCELROWNUMBER].ToString());
            IEnumerable<string> oFilterIE = odtFilterData.AsEnumerable().Select(row => (string)row[AddedGLTBSImportFields.EXCELROWNUMBER].ToString());
            IEnumerable<string> oNewFilterIE = oFilterIE.Except(oSubSetIE);
            foreach (string str in oNewFilterIE)
            {
                DataRow oSubSetDR = odtSubSetData.NewRow();
                DataRow oFilterDR = odtFilterData.Select(AddedGLTBSImportFields.EXCELROWNUMBER + "='" + str + "'").SingleOrDefault();
                for (int i = 0; i < _MatchingSourceColumnInfo.Count; i++)
                {
                    if (odtSubSetData.Columns.Contains(_MatchingSourceColumnInfo[i].ColumnName) && !string.IsNullOrEmpty(oFilterDR[_MatchingSourceColumnInfo[i].ColumnName].ToString()))
                    {
                        if (_MatchingSourceColumnInfo[i].DataTypeID == (short)WebEnums.DataType.Boolean)
                        {
                            oSubSetDR[_MatchingSourceColumnInfo[i].ColumnName] = oFilterDR[_MatchingSourceColumnInfo[i].ColumnName].ToString();
                        }
                        if (_MatchingSourceColumnInfo[i].DataTypeID == (short)WebEnums.DataType.DataTime)
                        {
                            oSubSetDR[_MatchingSourceColumnInfo[i].ColumnName] = Helper.GetDisplayDate(Convert.ToDateTime(oFilterDR[_MatchingSourceColumnInfo[i].ColumnName].ToString()));
                        }
                        if (_MatchingSourceColumnInfo[i].DataTypeID == (short)WebEnums.DataType.String)
                        {
                            oSubSetDR[_MatchingSourceColumnInfo[i].ColumnName] = oFilterDR[_MatchingSourceColumnInfo[i].ColumnName].ToString();
                        }
                        if (_MatchingSourceColumnInfo[i].DataTypeID == (short)WebEnums.DataType.Integer)
                        {
                            oSubSetDR[_MatchingSourceColumnInfo[i].ColumnName] = Convert.ToInt64(oFilterDR[_MatchingSourceColumnInfo[i].ColumnName].ToString());
                        }
                        if (_MatchingSourceColumnInfo[i].DataTypeID == (short)WebEnums.DataType.Decimal)
                        {
                            oSubSetDR[_MatchingSourceColumnInfo[i].ColumnName] = Convert.ToDecimal(oFilterDR[_MatchingSourceColumnInfo[i].ColumnName].ToString());
                        }
                    }
                }
                oSubSetDR[AddedGLTBSImportFields.EXCELROWNUMBER] = oFilterDR[AddedGLTBSImportFields.EXCELROWNUMBER];
                oSubSetDR[Filter] = oFilterDR[Filter];
                odtSubSetData.Rows.InsertAt(oSubSetDR, 0);
            }
            Session[SessionConstants.MATCHING_SOURCE_SUBSET_DATA] = odtSubSetData;
            //*** Add New Filter Row in Grid Data Source
        }

        /// <summary>
        /// Update Session data MatchingSourceDataImportHdrInfo
        /// </summary>
        protected void UpdateSessionData()
        {
            if (CurrentMatchSetHdrInfo != null && this.IsEditMode)
            {
                MatchingSourceDataImportHdrInfo oMatchingSourceDataImportHdrInfo = GetSelectedMatchingSourceDataImportHdrInfo();

                if (oMatchingSourceDataImportHdrInfo != null && oMatchingSourceDataImportHdrInfo.MatchSetMatchingSourceSubSetData != null)
                {
                    MatchSetMatchingSourceDataImportInfo oMatchSetMatchingSourceDataImportInfo = null;
                    oMatchSetMatchingSourceDataImportInfo = oMatchingSourceDataImportHdrInfo.MatchSetMatchingSourceSubSetData;

                    string XMLData = "";
                    DataTable orgDataTable = new DataTable();

                    if (Session[SessionConstants.MATCHING_SOURCE_SUBSET_DATA] != null)
                        orgDataTable = (DataTable)Session[SessionConstants.MATCHING_SOURCE_SUBSET_DATA];

                    XMLData = Helper.GetDataTableToXMLString(orgDataTable);
                    oMatchSetMatchingSourceDataImportInfo.SubSetName = txtSubSetName.Text;
                    oMatchSetMatchingSourceDataImportInfo.SubSetData = XMLData;
                }
            }
        }

        //*** End

        /// <summary>
        /// Save Match Set Matching Source Data Import
        /// </summary>
        /// <returns></returns>
        public override bool SaveData()
        {
            UpdateSessionData();
            MatchingParamInfo oMatchingParamInfo = new MatchingParamInfo();
            //**** Add GLTBS/NBF MatchSetMatchingSourceDataImportInfo
            //**** to MatchingParamInfo for Saving
            foreach (MatchingSourceDataImportHdrInfo oMatchingSourceDataImportHdrInfo in CurrentMatchSetHdrInfo.MatchingSourceDataImportHdrInfoList)
            {
                if (oMatchingParamInfo.oMatchSetMatchingSourceDataImportInfoList == null)
                    oMatchingParamInfo.oMatchSetMatchingSourceDataImportInfoList = new List<MatchSetMatchingSourceDataImportInfo>();

                oMatchingParamInfo.oMatchSetMatchingSourceDataImportInfoList.Add(oMatchingSourceDataImportHdrInfo.MatchSetMatchingSourceSubSetData);
            }
            //****End

            //**** Add MatchSetSubSetCombinationInfo
            //**** to MatchingParamInfo for Saving
            List<MatchSetSubSetCombinationInfo> oMatchSetSubSetCombinationInfoCollection = GetCombination();
            oMatchingParamInfo.oMatchSetSubSetCombinationInfoList = oMatchSetSubSetCombinationInfoCollection;
            //**** End

            //*** Save Data
            MatchingHelper.UpdateMatchSetMatchingSourceDataImportInfo(oMatchingParamInfo);
            //*** End

            ReloadMatchingSubSetCombinationData();

            return true;
        }

        private void ReloadMatchingSubSetCombinationData()
        {
            if (CurrentMatchSetHdrInfo != null)
            {
                CurrentMatchSetHdrInfo.MatchSetSubSetCombinationInfoCollection = MatchingHelper.GetAllMatchSetSubSetCombination(CurrentMatchSetHdrInfo.MatchSetID, GLDataID, SessionHelper.CurrentReconciliationPeriodID, null);
            }
        }


        /// <summary>
        /// Create/Update Subset Combination
        /// </summary>
        /// <returns></returns>
        protected List<MatchSetSubSetCombinationInfo> GetCombination()
        {
            List<MatchSetSubSetCombinationInfo> oMatchSetSubSetCombinationInfoList = null;
            try
            {
                if (CurrentMatchSetHdrInfo != null)
                {
                    oMatchSetSubSetCombinationInfoList = new List<MatchSetSubSetCombinationInfo>();
                    oMatchSetSubSetCombinationInfoList = CreateCombination();
                    if (CurrentMatchSetHdrInfo.MatchSetSubSetCombinationInfoCollection != null && CurrentMatchSetHdrInfo.MatchSetSubSetCombinationInfoCollection.Count > 0)
                    {
                        //oMatchSetSubSetCombinationInfo = CurrentMatchSetHdrInfo.MatchSetSubSetCombinationInfoCollection;
                        if (oMatchSetSubSetCombinationInfoList != null && oMatchSetSubSetCombinationInfoList.Count > 0)
                        {
                            foreach (MatchSetSubSetCombinationInfo oMatchSetSubSetCombinationInfo in CurrentMatchSetHdrInfo.MatchSetSubSetCombinationInfoCollection)
                            {
                                MatchSetSubSetCombinationInfo oMatchSetSubSetCombinationData = oMatchSetSubSetCombinationInfoList.Find(P => P.MatchSetMatchingSourceDataImport1ID == oMatchSetSubSetCombinationInfo.MatchSetMatchingSourceDataImport1ID && P.MatchSetMatchingSourceDataImport2ID == oMatchSetSubSetCombinationInfo.MatchSetMatchingSourceDataImport2ID);
                                if (oMatchSetSubSetCombinationData != null)
                                    oMatchSetSubSetCombinationInfo.MatchSetSubSetCombinationName = oMatchSetSubSetCombinationData.MatchSetSubSetCombinationName;
                            }
                        }
                        oMatchSetSubSetCombinationInfoList = null;
                        oMatchSetSubSetCombinationInfoList = CurrentMatchSetHdrInfo.MatchSetSubSetCombinationInfoCollection;
                    }
                }
            }
            catch (Exception ex)
            {
                Helper.ShowErrorMessageFromUserControl(this, ex);
            }
            return oMatchSetSubSetCombinationInfoList;
        }

        protected List<MatchSetSubSetCombinationInfo> CreateCombination()
        {
            List<MatchSetSubSetCombinationInfo> oMatchSetSubSetCombinationInfo = null;
            try
            {
                if (CurrentMatchSetHdrInfo != null)
                {
                    oMatchSetSubSetCombinationInfo = new List<MatchSetSubSetCombinationInfo>();
                    List<MatchSetMatchingSourceDataImportInfo> oMatchSetMatchingSourceDataImportNBFInfo = new List<MatchSetMatchingSourceDataImportInfo>();
                    MatchSetMatchingSourceDataImportInfo oMatchSetMatchingSourceDataImportGLTBSInfo = new MatchSetMatchingSourceDataImportInfo();

                    foreach (MatchingSourceDataImportHdrInfo oMatchingSourceDataImportHdrInfo in CurrentMatchSetHdrInfo.MatchingSourceDataImportHdrInfoList)
                    {
                        if (oMatchingSourceDataImportHdrInfo.MatchingSourceTypeID == (short)ARTEnums.MatchingSourceType.GLTBS)
                            oMatchSetMatchingSourceDataImportGLTBSInfo = oMatchingSourceDataImportHdrInfo.MatchSetMatchingSourceSubSetData;
                        else
                            oMatchSetMatchingSourceDataImportNBFInfo.Add(oMatchingSourceDataImportHdrInfo.MatchSetMatchingSourceSubSetData);

                    }
                    string MatchSetSubSetCombinationName = LanguageUtil.GetValue(2314);

                    if (oMatchSetMatchingSourceDataImportGLTBSInfo != null && oMatchSetMatchingSourceDataImportGLTBSInfo.MatchSetMatchingSourceDataImportID != null)
                    {
                        string MatchingSource1Name = CurrentMatchSetHdrInfo.MatchingSourceDataImportHdrInfoList.Find(P => P.MatchingSourceDataImportID == oMatchSetMatchingSourceDataImportGLTBSInfo.MatchingSourceDataImportID).MatchingSourceName;
                        foreach (MatchSetMatchingSourceDataImportInfo oMsMsdi in oMatchSetMatchingSourceDataImportNBFInfo)
                        {
                            string MatchingSource2Name = CurrentMatchSetHdrInfo.MatchingSourceDataImportHdrInfoList.Find(P => P.MatchingSourceDataImportID == oMsMsdi.MatchingSourceDataImportID).MatchingSourceName;
                            MatchSetSubSetCombinationInfo oMatchSetSubSetCombination = new MatchSetSubSetCombinationInfo();
                            oMatchSetSubSetCombination.MatchSetSubSetCombinationID = 0;
                            oMatchSetSubSetCombination.MatchSetMatchingSourceDataImport1ID = oMatchSetMatchingSourceDataImportGLTBSInfo.MatchSetMatchingSourceDataImportID;
                            oMatchSetSubSetCombination.MatchSetMatchingSourceDataImport2ID = oMsMsdi.MatchSetMatchingSourceDataImportID;
                            oMatchSetSubSetCombination.MatchSetSubSetCombinationName = String.Format(MatchSetSubSetCombinationName, MatchingSource1Name, oMatchSetMatchingSourceDataImportGLTBSInfo.SubSetName, MatchingSource2Name, oMsMsdi.SubSetName);
                            oMatchSetSubSetCombination.IsConfigurationComplete = false;
                            oMatchSetSubSetCombination.AddedBy = SessionHelper.CurrentUserLoginID;
                            oMatchSetSubSetCombination.DateAdded = DateTime.Now;
                            oMatchSetSubSetCombination.IsActive = true;
                            oMatchSetSubSetCombinationInfo.Add(oMatchSetSubSetCombination);
                        }
                    }
                    else
                    {
                        for (int i = 0; i < oMatchSetMatchingSourceDataImportNBFInfo.Count; i++)
                        {
                            string MatchingSource1Name = CurrentMatchSetHdrInfo.MatchingSourceDataImportHdrInfoList.Find(P => P.MatchingSourceDataImportID == oMatchSetMatchingSourceDataImportNBFInfo[i].MatchingSourceDataImportID).MatchingSourceName;
                            for (int j = i + 1; j < oMatchSetMatchingSourceDataImportNBFInfo.Count; j++)
                            {
                                string MatchingSource2Name = CurrentMatchSetHdrInfo.MatchingSourceDataImportHdrInfoList.Find(P => P.MatchingSourceDataImportID == oMatchSetMatchingSourceDataImportNBFInfo[j].MatchingSourceDataImportID).MatchingSourceName;
                                MatchSetSubSetCombinationInfo oMatchSetSubSetCombination = new MatchSetSubSetCombinationInfo();
                                oMatchSetSubSetCombination.MatchSetSubSetCombinationID = 0;
                                oMatchSetSubSetCombination.MatchSetMatchingSourceDataImport1ID = oMatchSetMatchingSourceDataImportNBFInfo[i].MatchSetMatchingSourceDataImportID;
                                oMatchSetSubSetCombination.MatchSetMatchingSourceDataImport2ID = oMatchSetMatchingSourceDataImportNBFInfo[j].MatchSetMatchingSourceDataImportID;
                                oMatchSetSubSetCombination.MatchSetSubSetCombinationName = String.Format(MatchSetSubSetCombinationName, MatchingSource1Name, oMatchSetMatchingSourceDataImportNBFInfo[i].SubSetName, MatchingSource2Name, oMatchSetMatchingSourceDataImportNBFInfo[j].SubSetName);
                                oMatchSetSubSetCombination.IsConfigurationComplete = false;
                                oMatchSetSubSetCombination.AddedBy = SessionHelper.CurrentUserLoginID;
                                oMatchSetSubSetCombination.DateAdded = DateTime.Now;
                                oMatchSetSubSetCombination.IsActive = true;
                                oMatchSetSubSetCombinationInfo.Add(oMatchSetSubSetCombination);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Helper.ShowErrorMessageFromUserControl(this, ex);
            }
            return oMatchSetSubSetCombinationInfo;
        }

        public override void SetControlStatePostLoad()
        {
            base.SetControlStatePostLoad();
            GridPnl.Visible = this.IsEditMode;
            btnAddtoSubSet.Enabled = this.IsEditMode;
            btnRemove.Enabled = this.IsEditMode;
            btnRemoveAll.Enabled = this.IsEditMode;
            txtSubSetName.Enabled = this.IsEditMode;

            btnSubsetRemove.Enabled = this.IsEditMode;
            btnSubsetRemoveAll.Enabled = this.IsEditMode;

            GridColumn oGridColumn = rgMappingColumns.MasterTableView.Columns.FindByUniqueNameSafe("CheckboxSelectColumn");
            if (oGridColumn != null)
                oGridColumn.Display = this.IsEditMode;

            GridColumn oSubSetGridColumn = rgSubSetData.MasterTableView.Columns.FindByUniqueNameSafe("CheckboxSelectColumn");
            if (oSubSetGridColumn != null)
                oSubSetGridColumn.Display = this.IsEditMode;
        }

        /// <summary>
        /// Can Load Step or not
        /// </summary>
        /// <returns></returns>
        public override bool CanLoadStep()
        {
            if (CurrentMatchSetHdrInfo != null)
            {
                if (CurrentMatchSetHdrInfo.MatchingSourceDataImportHdrInfoList != null && CurrentMatchSetHdrInfo.MatchingSourceDataImportHdrInfoList.Count > 0)
                    return true;
            }
            return false;
        }

        #region Filter Grid
        public void rgMappingColumns_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            DataTable oDTFilterData = new DataTable();
            if (Session[SessionConstants.MATCHING_SOURCE_FILTER_DATA] != null)
                oDTFilterData = (DataTable)Session[SessionConstants.MATCHING_SOURCE_FILTER_DATA];

            rgMappingColumns.DataSource = oDTFilterData;
        }
        public void rgMappingColumns_ItemCommand(object source, GridCommandEventArgs e)
        {
            if (e.CommandName == TelerikConstants.GridClearFilterCommandName)
                GridClearFilter();
        }
        protected void rgMappingColumns_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            try
            {
                String Filter = LanguageUtil.GetValue(2853);
                if (e.Item.ItemType == GridItemType.Header)
                    SessionHelper.ShowGridFilterIcon((PageBase)this.Page, rgMappingColumns, e, GetGridClientIDKey(rgMappingColumns));

                if (e.Item.ItemType == Telerik.Web.UI.GridItemType.Item || e.Item.ItemType == Telerik.Web.UI.GridItemType.AlternatingItem)
                {
                    DataRow row = ((System.Data.DataRowView)(e.Item.DataItem)).Row;

                    if (_MatchingSourceColumnInfo == null)
                        _MatchingSourceColumnInfo = GetColumns();

                    if (_MatchingSourceColumnInfo != null && _MatchingSourceColumnInfo.Count > 0)
                    {
                        for (int i = 0; i < _MatchingSourceColumnInfo.Count; i++)
                        {
                            if ((e.Item as GridDataItem)[_MatchingSourceColumnInfo[i].ColumnName] != null && row[_MatchingSourceColumnInfo[i].ColumnName].ToString() != "")
                            {
                                if (_MatchingSourceColumnInfo[i].DataTypeID == (short)WebEnums.DataType.Boolean)
                                {
                                    (e.Item as GridDataItem)[_MatchingSourceColumnInfo[i].ColumnName].Text = row[_MatchingSourceColumnInfo[i].ColumnName].ToString();
                                }
                                if (_MatchingSourceColumnInfo[i].DataTypeID == (short)WebEnums.DataType.DataTime)
                                {
                                    (e.Item as GridDataItem)[_MatchingSourceColumnInfo[i].ColumnName].Text = Helper.GetDisplayDate(Convert.ToDateTime(row[_MatchingSourceColumnInfo[i].ColumnName].ToString()));
                                }
                                if (_MatchingSourceColumnInfo[i].DataTypeID == (short)WebEnums.DataType.String)
                                {
                                    (e.Item as GridDataItem)[_MatchingSourceColumnInfo[i].ColumnName].Text = row[_MatchingSourceColumnInfo[i].ColumnName].ToString();
                                }
                                if (_MatchingSourceColumnInfo[i].DataTypeID == (short)WebEnums.DataType.Integer)
                                {
                                    (e.Item as GridDataItem)[_MatchingSourceColumnInfo[i].ColumnName].Text = Helper.GetDisplayIntegerValueWitoutComma(Convert.ToInt64(row[_MatchingSourceColumnInfo[i].ColumnName].ToString()));
                                }
                                if (_MatchingSourceColumnInfo[i].DataTypeID == (short)WebEnums.DataType.Decimal)
                                {
                                    (e.Item as GridDataItem)[_MatchingSourceColumnInfo[i].ColumnName].Text = Helper.GetDisplayDecimalValue(Convert.ToDecimal(row[_MatchingSourceColumnInfo[i].ColumnName].ToString()));
                                }
                            }
                        }
                        if ((e.Item as GridDataItem)[Filter] != null)
                            (e.Item as GridDataItem)[Filter].Text = row[Filter].ToString();

                    }
                }
            }
            catch (Exception)
            {
            }
        }
        protected void btnRemove_Click(object sender, EventArgs e)
        {
            DataTable oDt = new DataTable();
            if (Session[SessionConstants.MATCHING_SOURCE_FILTER_DATA] != null)
                oDt = (DataTable)Session[SessionConstants.MATCHING_SOURCE_FILTER_DATA];

            foreach (GridDataItem item in rgMappingColumns.SelectedItems)
            {
                DataRow[] odr = oDt.Select(AddedGLTBSImportFields.EXCELROWNUMBER + "='" + item.GetDataKeyValue(AddedGLTBSImportFields.EXCELROWNUMBER) + "'");
                oDt.Rows.Remove(odr[0]);
            }
            rgMappingColumns.Rebind();

        }
        protected void btnRemoveAll_Click(object sender, EventArgs e)
        {
            DataTable oDt = new DataTable();
            if (Session[SessionConstants.MATCHING_SOURCE_FILTER_DATA] != null)
                oDt = (DataTable)Session[SessionConstants.MATCHING_SOURCE_FILTER_DATA];

            oDt.Rows.Clear();
            rgMappingColumns.Rebind();
        }
        /// <summary>
        /// Bind Filter Grid
        /// </summary>
        protected void BindGrid()
        {
            try
            {
                MapFilterInfo();
                rgMappingColumns.MasterTableView.SortExpressions.Clear();
                rgMappingColumns.CurrentPageIndex = 0;
                DataTable oDt = new DataTable();
                if (Session[SessionConstants.MATCHING_SOURCE_FILTER_DATA] != null)
                    oDt = (DataTable)Session[SessionConstants.MATCHING_SOURCE_FILTER_DATA];
                rgMappingColumns.DataSource = oDt;
                rgMappingColumns.DataBind();
            }
            catch (Exception ex)
            {
                Helper.ShowErrorMessageFromUserControl(this, ex);
            }
        }

        protected void rgMappingColumns_ColumnCreated(object sender, Telerik.Web.UI.GridColumnCreatedEventArgs e)
        {
            if (e.Column.UniqueName == AddedGLTBSImportFields.EXCELROWNUMBER)
            {
                e.Column.Visible = false;
            }
            if (e.Column.UniqueName == AddedGLTBSImportFields.ISRECITEMCREATED)
            {
                e.Column.Visible = false;
            }
        }
        protected void rgMappingColumns_SortCommand(object source, GridSortCommandEventArgs e)
        {
            GridHelper.HandleSortCommand(e);
            rgMappingColumns.Rebind();
        }
        /// <summary>
        /// Clear Filtered data and Rebind grid
        /// </summary>
        protected void GridClearFilter()
        {
            DataTable oDt = new DataTable();
            if (Session[SessionConstants.MATCHING_SOURCE_FILTER_DATA] != null)
                oDt = (DataTable)Session[SessionConstants.MATCHING_SOURCE_FILTER_DATA];

            DataTable OriginalDT = new DataTable();

            MatchingSourceDataImportHdrInfo oMatchingSourceDataImportHdrInfo = GetSelectedMatchingSourceDataImportHdrInfo();
            if (oMatchingSourceDataImportHdrInfo.MatchingSourceData != null && oMatchingSourceDataImportHdrInfo.MatchingSourceData.Data != "")
                OriginalDT = Helper.GetXmlToDataTable(oMatchingSourceDataImportHdrInfo.MatchingSourceData.Data);

            if (OriginalDT != null && OriginalDT.Rows.Count > 0)
            {
                UpdateFilterColumn(OriginalDT, "");
                Session[SessionConstants.MATCHING_SOURCE_FILTER_DATA] = OriginalDT;
            }
            rgMappingColumns.Rebind();

            //Added by Abhishek
            //SessionHelper.ClearGridDynamicFilterDataFromSession();
            SessionHelper.ClearDynamicFilterData(GetGridClientIDKey(rgMappingColumns));
        }

        /// <summary>
        /// Filter data and Rebind grid
        /// </summary>
        protected void GridFilterData()
        {
            rgMappingColumns.CurrentPageIndex = 0;
            string strWhere = "";
            string strFilter = "";

            DataTable OriginalDT = new DataTable();

            if (_MatchingSourceColumnInfo == null)
                _MatchingSourceColumnInfo = GetColumns();

            MatchingSourceDataImportHdrInfo oMatchingSourceDataImportHdrInfo = GetSelectedMatchingSourceDataImportHdrInfo();
            if (oMatchingSourceDataImportHdrInfo.MatchingSourceData != null && oMatchingSourceDataImportHdrInfo.MatchingSourceData.Data != "")
                OriginalDT = Helper.GetXmlToDataTable(oMatchingSourceDataImportHdrInfo.MatchingSourceData.Data, _MatchingSourceColumnInfo, oMatchingSourceDataImportHdrInfo.MatchingSourceTypeName);

            if (OriginalDT != null && OriginalDT.Rows.Count > 0)
            {
                //List<string> oFilter = new List<string>();
                //if (Session[SessionConstants.GRID_FILTER_CLAUSE] != null)
                //{
                // oFilter = (List<string>)Session[SessionConstants.GRID_FILTER_CLAUSE];
                //strWhere = oFilter[1].ToString();
                //strFilter = oFilter[0].ToString();                  
                strWhere = SessionHelper.GetDynamicFilterResultWhereClause(GetGridClientIDKey(rgMappingColumns));
                strFilter = SessionHelper.GetDynamicFilterResultString(GetGridClientIDKey(rgMappingColumns));
                //}
                if (strWhere != "")
                {
                    //strWhere = "[Txn Date] >= #01/02/2011# AND [Txn Date] <= #01/05/2011#  ";
                    //*** Select Data from Data table using Filter
                    DataRow[] oDrs = OriginalDT.Select(strWhere);
                    if (oDrs != null && oDrs.Count() > 0)
                    {
                        OriginalDT = null;
                        OriginalDT = oDrs.CopyToDataTable();
                    }
                    else
                    {
                        OriginalDT.Rows.Clear();
                    }
                    UpdateFilterColumn(OriginalDT, strFilter);
                    //*** End Select Data from Data table using Filter
                }
                Session[SessionConstants.MATCHING_SOURCE_FILTER_DATA] = OriginalDT;
            }
            rgMappingColumns.Rebind();
        }
        #endregion

        #region SubSetData Grid
        protected void btnSubsetRemove_Click(object sender, EventArgs e)
        {
            DataTable oDTSubSetData = new DataTable();
            if (Session[SessionConstants.MATCHING_SOURCE_SUBSET_DATA] != null)
                oDTSubSetData = (DataTable)Session[SessionConstants.MATCHING_SOURCE_SUBSET_DATA];

            foreach (GridDataItem item in rgSubSetData.SelectedItems)
            {
                DataRow[] odr = oDTSubSetData.Select(AddedGLTBSImportFields.EXCELROWNUMBER + "='" + item.GetDataKeyValue(AddedGLTBSImportFields.EXCELROWNUMBER) + "'");
                oDTSubSetData.Rows.Remove(odr[0]);
            }
            rgSubSetData.Rebind();
        }
        protected void btnSubsetRemoveAll_Click(object sender, EventArgs e)
        {
            DataTable oDTSubSetData = new DataTable();
            if (Session[SessionConstants.MATCHING_SOURCE_SUBSET_DATA] != null)
                oDTSubSetData = (DataTable)Session[SessionConstants.MATCHING_SOURCE_SUBSET_DATA];

            oDTSubSetData.Rows.Clear();
            rgSubSetData.Rebind();
        }

        protected void rgSubSetData_ColumnCreated(object sender, Telerik.Web.UI.GridColumnCreatedEventArgs e)
        {
            if (e.Column.UniqueName == AddedGLTBSImportFields.EXCELROWNUMBER)
            {
                e.Column.Visible = false;
            }
            if (e.Column.UniqueName == AddedGLTBSImportFields.ISRECITEMCREATED)
            {
                e.Column.Visible = false;
            }
        }
        protected void rgSubSetData_SortCommand(object source, GridSortCommandEventArgs e)
        {
            GridHelper.HandleSortCommand(e);
            rgSubSetData.Rebind();
        }
        protected void rgSubSetData_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == Telerik.Web.UI.GridItemType.Item || e.Item.ItemType == Telerik.Web.UI.GridItemType.AlternatingItem)
                {
                    String Filter = LanguageUtil.GetValue(2853);

                    DataRow row = ((System.Data.DataRowView)(e.Item.DataItem)).Row;

                    if (_MatchingSourceColumnInfo == null)
                        _MatchingSourceColumnInfo = GetColumns();

                    if (_MatchingSourceColumnInfo != null && _MatchingSourceColumnInfo.Count > 0)
                    {
                        for (int i = 0; i < _MatchingSourceColumnInfo.Count; i++)
                        {
                            if ((e.Item as GridDataItem)[_MatchingSourceColumnInfo[i].ColumnName] != null && row[_MatchingSourceColumnInfo[i].ColumnName].ToString() != "")
                            {
                                if (_MatchingSourceColumnInfo[i].DataTypeID == (short)WebEnums.DataType.Boolean)
                                {
                                    (e.Item as GridDataItem)[_MatchingSourceColumnInfo[i].ColumnName].Text = row[_MatchingSourceColumnInfo[i].ColumnName].ToString();
                                }
                                if (_MatchingSourceColumnInfo[i].DataTypeID == (short)WebEnums.DataType.DataTime)
                                {
                                    (e.Item as GridDataItem)[_MatchingSourceColumnInfo[i].ColumnName].Text = Helper.GetDisplayDate(Convert.ToDateTime(row[_MatchingSourceColumnInfo[i].ColumnName].ToString()));
                                }
                                if (_MatchingSourceColumnInfo[i].DataTypeID == (short)WebEnums.DataType.String)
                                {
                                    (e.Item as GridDataItem)[_MatchingSourceColumnInfo[i].ColumnName].Text = row[_MatchingSourceColumnInfo[i].ColumnName].ToString();
                                }
                                if (_MatchingSourceColumnInfo[i].DataTypeID == (short)WebEnums.DataType.Integer)
                                {
                                    (e.Item as GridDataItem)[_MatchingSourceColumnInfo[i].ColumnName].Text = Helper.GetDisplayIntegerValueWitoutComma(Convert.ToInt64(row[_MatchingSourceColumnInfo[i].ColumnName].ToString()));
                                }
                                if (_MatchingSourceColumnInfo[i].DataTypeID == (short)WebEnums.DataType.Decimal)
                                {
                                    (e.Item as GridDataItem)[_MatchingSourceColumnInfo[i].ColumnName].Text = Helper.GetDisplayDecimalValue(Convert.ToDecimal(row[_MatchingSourceColumnInfo[i].ColumnName].ToString()));
                                }
                            }
                        }
                        if ((e.Item as GridDataItem)[Filter] != null)
                            (e.Item as GridDataItem)[Filter].Text = row[Filter].ToString();

                    }
                }
            }
            catch (Exception)
            {
            }
        }
        public void rgSubSetData_ItemCommand(object source, GridCommandEventArgs e)
        {

        }
        public void rgSubSetData_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            DataTable oDTSubSetData = new DataTable();
            if (Session[SessionConstants.MATCHING_SOURCE_SUBSET_DATA] != null)
                oDTSubSetData = (DataTable)Session[SessionConstants.MATCHING_SOURCE_SUBSET_DATA];

            rgSubSetData.DataSource = oDTSubSetData;
        }
        /// <summary>
        /// Bind Subset Grid
        /// </summary>
        protected void BindSubSetGrid()
        {
            try
            {
                rgSubSetData.CurrentPageIndex = 0;
                rgSubSetData.MasterTableView.SortExpressions.Clear();
                DataTable oDt = new DataTable();
                if (Session[SessionConstants.MATCHING_SOURCE_SUBSET_DATA] != null)
                    oDt = (DataTable)Session[SessionConstants.MATCHING_SOURCE_SUBSET_DATA];
                rgSubSetData.DataSource = oDt;
                rgSubSetData.DataBind();
            }
            catch (Exception ex)
            {
                Helper.ShowErrorMessageFromUserControl(this, ex);
            }
        }
        #endregion
        /// <summary>
        /// Get Grid Client key
        /// </summary>
        private string GetGridClientIDKey(ExRadGrid Rg)
        {
            return Rg.ClientID;
        }

        private void MapFilterInfo()
        {
            try
            {
                if (_MatchingSourceColumnInfo == null)
                    _MatchingSourceColumnInfo = GetColumns();
                List<FilterInfo> tempList = new List<FilterInfo>();
                FilterInfo oFilterInfo = null;
                if (_MatchingSourceColumnInfo != null)
                {
                    foreach (MatchingSourceColumnInfo info in _MatchingSourceColumnInfo)
                    {
                        oFilterInfo = new FilterInfo();
                        oFilterInfo.ColumnID = (short)info.MatchingSourceColumnID;
                        oFilterInfo.ColumnName = info.ColumnName;
                        oFilterInfo.DisplayColumnName = info.ColumnName;
                        oFilterInfo.DataType = (short)info.DataTypeID;
                        tempList.Add(oFilterInfo);
                    }
                }
                SessionHelper.SetDynamicFilterColumns(tempList, GetGridClientIDKey(rgMappingColumns));
            }
            catch (Exception ex)
            {
                Helper.ShowErrorMessageFromUserControl(this, ex);
            }
        }
    }
}