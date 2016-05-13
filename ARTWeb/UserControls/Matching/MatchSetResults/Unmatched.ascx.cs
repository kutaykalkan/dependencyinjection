using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SkyStem.ART.Web.Classes;
using System.Data;
using SkyStem.ART.Client.Model.Matching;
using SkyStem.ART.Web.Data;
using SkyStem.ART.Web.Utility;
using Telerik.Web.UI;
using SkyStem.ART.Client.Data;
using SkyStem.ART.Client.Exception;
using SkyStem.ART.Client.Model;
using SkyStem.Library.Controls.TelerikWebControls.Data;
using SkyStem.Language.LanguageUtility;
using SkyStem.Library.Controls.WebControls;
using SkyStem.Library.Controls.TelerikWebControls;

namespace SkyStem.ART.Web.UserControls
{
    public partial class Unmatched : UserControlBase
    {
        public delegate bool MatchHandler(DataTable tblSource1, DataRow[] dataRowsSource1, DataTable tblSource2, DataRow[] dataRowsSource2);
        public delegate void CreateRecItemHandler(DataTable tblRecItem);
        public delegate void CloseRecItemHandler(DataTable tblRecItem);
        public event MatchHandler OnMatchClick;
        public event EventHandler OnSaveRequired;
        public event CreateRecItemHandler OnCreateRecItemClick;
        public event CloseRecItemHandler OnCloseRecItemHandler;
        bool isExportPDF;
        bool isExportExcel;
        protected string SelectionMsg = "";
        protected string SelectionMsgMatch = "";
        protected string SelectionMsgCreateRecItem = "";
        ARTEnums.MatchingType _MatchingType;
        private Int64? _MatchSetSubSetCombinationID;
        private Int64? _GlDataID;
        private Int64? _AccountID;
        /// <summary>
        /// Matching Type
        /// </summary>
        public ARTEnums.MatchingType MatchingType
        {
            get { return (ARTEnums.MatchingType)ViewState["UnmatchedMatchingType"]; }
            set
            {
                if (ViewState["UnmatchedMatchingType"] == null)
                {
                    _MatchingType = value;
                    ViewState["UnmatchedMatchingType"] = _MatchingType;
                }

                if (MatchingType == ARTEnums.MatchingType.AccountMatching)
                {
                    btnCreateRecItem.Visible = true;
                    btnCloseRecItem.Visible = true;
                    pnlUsedRecordsContent.Visible = true;
                }
                else
                {
                    btnCreateRecItem.Visible = false;
                    btnCloseRecItem.Visible = false;
                    pnlUsedRecordsContent.Visible = true;
                }
            }
        }
        public Int64? MatchSetSubSetCombinationID
        {
            get
            {
                if (_MatchSetSubSetCombinationID.HasValue)
                    return _MatchSetSubSetCombinationID;
                else
                {
                    _MatchSetSubSetCombinationID = Convert.ToInt64(ViewState["MatchSetSubSetCombinationID"]);
                    return _MatchSetSubSetCombinationID;
                }

            }
            set
            {
                _MatchSetSubSetCombinationID = value;
                ViewState["MatchSetSubSetCombinationID"] = _MatchSetSubSetCombinationID;

            }
        }
        public Int64? AccountID
        {
            get
            {
                if (_AccountID.HasValue)
                    return _AccountID;
                else
                {
                    if (!String.IsNullOrEmpty(Request.QueryString[QueryStringConstants.ACCOUNT_ID]))
                    {
                        _AccountID = Convert.ToInt64(Request.QueryString[QueryStringConstants.ACCOUNT_ID]);
                    }
                    return _AccountID;
                }

            }
            set
            {
                _AccountID = value;

            }
        }
        public Int64? GlDataID
        {
            get
            {
                if (_GlDataID.HasValue)
                    return _GlDataID;
                else
                {
                    if (!String.IsNullOrEmpty(Request.QueryString[QueryStringConstants.GLDATA_ID]))
                    {
                        _GlDataID = Convert.ToInt64(Request.QueryString[QueryStringConstants.GLDATA_ID]);
                    }
                    return _GlDataID;
                }

            }
            set
            {
                _GlDataID = value;

            }
        }
        /// <summary>
        /// List of Columns
        /// </summary>
        private List<MatchingConfigurationInfo> MatchingConfigurationInfoList
        {
            get
            {
                if (SessionHelper.CurrentMatchSetSubSetCombinationInfo != null)
                    return SessionHelper.CurrentMatchSetSubSetCombinationInfo.MatchingConfigurationInfoList;
                return null;
            }
        }
        /// <summary>
        /// Result Data for Current Match Set Sub Set Combination
        /// </summary>
        private MatchSetResultWorkspaceInfo CurrentMatchSetResultWorkspaceInfo
        {
            get
            {
                if (SessionHelper.CurrentMatchSetSubSetCombinationInfo != null)
                    return SessionHelper.CurrentMatchSetSubSetCombinationInfo.MatchSetResultWorkspaceInfo;
                return null;
            }
        }
        /// <summary>
        /// Gets the current match set GL data rec item info list.
        /// </summary>
        private List<MatchSetGLDataRecItemInfo> CurrentGLDataRecItemInfoListSource1
        {
            get
            {
                if (SessionHelper.CurrentMatchSetSubSetCombinationInfo != null)
                    return SessionHelper.CurrentMatchSetSubSetCombinationInfo.GLDataRecItemInfoListSource1;
                return null;
            }
        }
        /// <summary>
        /// Gets the current match set GL data rec item info list.
        /// </summary>
        private List<MatchSetGLDataRecItemInfo> CurrentGLDataRecItemInfoListSource2
        {
            get
            {
                if (SessionHelper.CurrentMatchSetSubSetCombinationInfo != null)
                    return SessionHelper.CurrentMatchSetSubSetCombinationInfo.GLDataRecItemInfoListSource2;
                return null;
            }
        }
        /// <summary>
        /// Matching Source Data Set
        /// </summary>
        public DataSet UnmatchedDataSet
        {
            get { return (DataSet)Session[SessionConstants.MATCHING_MATCH_SET_RESULT_UNMATCHED_SOURCE]; }
            set { Session[SessionConstants.MATCHING_MATCH_SET_RESULT_UNMATCHED_SOURCE] = value; }
        }
        /// <summary>
        /// Un Matched Combined Data Table
        /// </summary>
        public DataTable UnmatchedCombinedDataTable
        {
            get
            {
                if (UnmatchedCombinedFilteredDataTable != null)
                    return UnmatchedCombinedFilteredDataTable;
                return (DataTable)Session[SessionConstants.MATCHING_MATCH_SET_RESULT_UNMATCHED_SOURCE_COMBINED];
            }
            set { Session[SessionConstants.MATCHING_MATCH_SET_RESULT_UNMATCHED_SOURCE_COMBINED] = value; }
        }
        /// <summary>k
        /// Used Records Combined Data Table
        /// </summary>
        public DataTable UsedRecordsCombinedDataTable
        {
            get
            {
                return (DataTable)ViewState[SessionConstants.MATCHING_MATCH_SET_RESULT_USED_RECORDS_COMBINED];
            }
            set { ViewState[SessionConstants.MATCHING_MATCH_SET_RESULT_USED_RECORDS_COMBINED] = value; }
        }
        /// <summary>
        /// Un Matched Combined Filtered Data Table
        /// </summary>
        public DataTable UnmatchedCombinedFilteredDataTable
        {
            get { return (DataTable)Session[SessionConstants.MATCHING_MATCH_SET_RESULT_UNMATCHED_SOURCE_COMBINED_FILTERED]; }
            set { Session[SessionConstants.MATCHING_MATCH_SET_RESULT_UNMATCHED_SOURCE_COMBINED_FILTERED] = value; }
        }
        /// <summary>
        /// Work Space Data Set
        /// </summary>
        public DataSet UnmatchedWorkspaceDataSet
        {
            get { return (DataSet)Session[SessionConstants.MATCHING_MATCH_SET_RESULT_UNMATCHED_WORKSPACE_SOURCE]; }
            set { Session[SessionConstants.MATCHING_MATCH_SET_RESULT_UNMATCHED_WORKSPACE_SOURCE] = value; }
        }
        protected override void OnInit(EventArgs e)
        {
            //rgWorkspaceSource1.ClientSettings.ClientEvents.OnRowDeselected = "rgWorkspaceSource1ItemSelected";
            //rgWorkspaceSource1.ClientSettings.ClientEvents.OnRowSelected = "rgWorkspaceSource1ItemSelected";
            //rgWorkspaceSource2.ClientSettings.ClientEvents.OnRowDeselected = "rgWorkspaceSource2ItemSelected";
            //rgWorkspaceSource2.ClientSettings.ClientEvents.OnRowSelected = "rgWorkspaceSource2ItemSelected";
            base.OnInit(e);
        }
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                isExportPDF = false;
                isExportExcel = false;
                ////ClearCheckedItemSession();
            }
            CreateColumns();
            SelectionMsg = LanguageUtil.GetValue(2013);
            SelectionMsgMatch = LanguageUtil.GetValue(2357);
            SelectionMsgCreateRecItem = LanguageUtil.GetValue(2375);
            MatchSetSubSetCombinationInfo oMatchSetSubSetCombinationInfo = (MatchSetSubSetCombinationInfo)SessionHelper.CurrentMatchSetSubSetCombinationInfo;
            GridHelper.SetRecordCount(rgUsedRecords);
        }
        ////private void ClearCheckedItemSession()
        ////{
        ////    Session[MatchingHelper.getCheckedItemsSessionKey(rgUnmatched)] = null;
        ////}
        /// <summary>
        /// Create Grid Columns
        /// </summary>
        private void CreateColumns()
        {
            try
            {
                if (MatchingConfigurationInfoList != null)
                {
                    rgUnmatched.MasterTableView.DataKeyNames = MatchingHelper.GetDataKeyNamesForUnMatched();
                    rgWorkspaceSource1.MasterTableView.DataKeyNames = MatchingHelper.GetDataKeyNamesForUnMatched();
                    rgWorkspaceSource2.MasterTableView.DataKeyNames = MatchingHelper.GetDataKeyNamesForUnMatched();
                    string[] ClientDataKeyNames = new string[1];
                    ClientDataKeyNames = MatchingHelper.GetClientDataKeyNamesForUnMatched(MatchingConfigurationInfoList);
                    if (ClientDataKeyNames != null)
                    {
                        rgWorkspaceSource1.ClientSettings.ClientEvents.OnRowDeselected = "rgWorkspaceSource1ItemSelected";
                        rgWorkspaceSource1.ClientSettings.ClientEvents.OnRowSelected = "rgWorkspaceSource1ItemSelected";
                        rgWorkspaceSource2.ClientSettings.ClientEvents.OnRowDeselected = "rgWorkspaceSource2ItemSelected";
                        rgWorkspaceSource2.ClientSettings.ClientEvents.OnRowSelected = "rgWorkspaceSource2ItemSelected";

                        rgWorkspaceSource1.MasterTableView.ClientDataKeyNames = ClientDataKeyNames;
                        rgWorkspaceSource2.MasterTableView.ClientDataKeyNames = ClientDataKeyNames;
                        hdnAmountColumnDataKeyname.Value = ClientDataKeyNames[0];
                        pnlWS1Total.Visible = true;
                        pnlWS2Total.Visible = true;
                    }
                    else
                    {
                        pnlWS1Total.Visible = false;
                        pnlWS2Total.Visible = false;
                    }
                    MatchingHelper.CreateGridColumns(rgUnmatched, MatchingConfigurationInfoList);
                    MatchingHelper.CreateMatchingSourceNameColumn(rgUnmatched);
                    MatchingHelper.CreateRecItemNumberColumn(rgWorkspaceSource1);
                    MatchingHelper.CreateRecItemNumberColumn(rgWorkspaceSource2);
                    MatchingHelper.CreateGridColumns(rgWorkspaceSource1, MatchingConfigurationInfoList);
                    MatchingHelper.CreateMatchingSourceNameColumn(rgWorkspaceSource1);
                    MatchingHelper.CreateGridColumns(rgWorkspaceSource2, MatchingConfigurationInfoList);
                    MatchingHelper.CreateMatchingSourceNameColumn(rgWorkspaceSource2);
                    if (MatchingType == ARTEnums.MatchingType.AccountMatching)
                    {
                        rgWorkspaceSource1.MasterTableView.DataKeyNames = MatchingHelper.GetDataKeyNamesForUnMatched();
                        rgWorkspaceSource1.MasterTableView.DataKeyNames = MatchingHelper.GetDataKeyNamesForUnMatched();
                        rgUsedRecords.MasterTableView.DataKeyNames = MatchingHelper.GetDataKeyNamesForUnMatched();
                        MatchingHelper.CreateRecItemNumberColumn(rgUsedRecords);
                        MatchingHelper.CreateGridColumns(rgUsedRecords, MatchingConfigurationInfoList);
                    }

                }
            }
            catch (ARTException ex)
            {
                Helper.ShowErrorMessageFromUserControl(this, ex);
            }
        }
        /// <summary>
        /// Creates the combined source.
        /// </summary>
        public void CreateCombinedSource()
        {

            try
            {
                DataTable tblSource1 = Helper.FindDataTable(UnmatchedDataSet, MatchSetResultConstants.MATCH_SET_RESULT_TABLE_NAME_SOURCE1);
                DataTable tblSource2 = Helper.FindDataTable(UnmatchedDataSet, MatchSetResultConstants.MATCH_SET_RESULT_TABLE_NAME_SOURCE2);
                UnmatchedCombinedDataTable = Helper.MergeTables(tblSource1, tblSource2);
            }
            catch (ARTException ex)
            {
                Helper.ShowErrorMessageFromUserControl(this, ex);
            }
        }

        /// <summary>
        /// Creates the combined source.
        /// </summary>
        public void CreateUsedRecordCombinedSource()
        {

            try
            {
                DataTable tblSource1 = GetUsedRecordsDataTable(Helper.FindDataTable(UnmatchedWorkspaceDataSet, MatchSetResultConstants.MATCH_SET_RESULT_TABLE_NAME_SOURCE1));
                DataTable tblSource2 = GetUsedRecordsDataTable(Helper.FindDataTable(UnmatchedWorkspaceDataSet, MatchSetResultConstants.MATCH_SET_RESULT_TABLE_NAME_SOURCE2));
                UsedRecordsCombinedDataTable = Helper.MergeTables(tblSource1, tblSource2);
            }
            catch (ARTException ex)
            {
                Helper.ShowErrorMessageFromUserControl(this, ex);
            }
        }
        /// <summary>
        /// Pupulate Data
        /// </summary>
        public void PopulateData(WebEnums.FormMode eFormMode, bool bindDataRequired)
        {
            Helper.ClearDynamicColumns(rgUnmatched);
            Helper.ClearDynamicColumns(rgWorkspaceSource1);
            Helper.ClearDynamicColumns(rgWorkspaceSource2);
            LoadDataSet();
            CreateCombinedSource();
            CreateUsedRecordCombinedSource();
            ApplyFilter();
            if (bindDataRequired)
                BindGrids();
            CreateFilterInfo();
            EnableDisableControls(eFormMode);
        }
        /// <summary>
        /// Enables the disable controls.
        /// </summary>
        /// <param name="eFormMode">The e form mode.</param>
        public void EnableDisableControls(WebEnums.FormMode eFormMode)
        {
            bool isEditMode = eFormMode == WebEnums.FormMode.Edit;
            btnAddToWorkspace.Enabled = isEditMode;
            btnRemoveFromWorkspace.Enabled = isEditMode;
            btnMatch.Enabled = isEditMode;
            btnCloseRecItem.Enabled = isEditMode;
            if (isEditMode)
                btnCreateRecItem.LabelID = 2327;
            else
                btnCreateRecItem.LabelID = 2369;
        }
        /// <summary>
        /// Load XML data into DataSet
        /// </summary>
        private void LoadDataSet()
        {
            try
            {
                if (CurrentMatchSetResultWorkspaceInfo != null)
                {
                    Helper.LoadXmlToDataSet(UnmatchedDataSet, CurrentMatchSetResultWorkspaceInfo.UnmatchData);
                    Helper.LoadXmlToDataSet(UnmatchedWorkspaceDataSet, CurrentMatchSetResultWorkspaceInfo.WorkspaceUnmatchData);
                    UpdateRecItemNumber();
                }
                else
                {
                    Helper.LoadXmlToDataSet(UnmatchedDataSet, string.Empty);
                    Helper.LoadXmlToDataSet(UnmatchedWorkspaceDataSet, string.Empty);
                }
            }
            catch (ARTException ex)
            {
                Helper.ShowErrorMessageFromUserControl(this, ex);
                Helper.LoadXmlToDataSet(UnmatchedDataSet, string.Empty);
                Helper.LoadXmlToDataSet(UnmatchedWorkspaceDataSet, string.Empty);
            }
        }
        /// <summary>
        /// Updates the rec item number.
        /// </summary>
        public void UpdateRecItemNumber()
        {
            MatchingHelper.UpdateRecItemNumber(UnmatchedWorkspaceDataSet, CurrentGLDataRecItemInfoListSource1, CurrentGLDataRecItemInfoListSource2);
        }
        /// <summary>
        /// Bind the Grids
        /// </summary>
        public void BindGrids()
        {
            try
            {
                CreateColumns();
                rgUnmatched.DataSource = UnmatchedCombinedDataTable;
                rgUnmatched.MasterTableView.CurrentPageIndex = 0;
                rgUnmatched.VirtualItemCount = UnmatchedCombinedDataTable.Rows.Count;
                rgUnmatched.DataBind();
                DataTable DtWorkspaceSource1 = GetUnUsedRecordsDataTable(Helper.FindDataTable(UnmatchedWorkspaceDataSet, MatchSetResultConstants.MATCH_SET_RESULT_TABLE_NAME_SOURCE1));
                rgWorkspaceSource1.DataSource = DtWorkspaceSource1;
                rgWorkspaceSource1.MasterTableView.CurrentPageIndex = 0;
                rgWorkspaceSource1.VirtualItemCount=DtWorkspaceSource1.Rows.Count;
                rgWorkspaceSource1.DataBind();
                DataTable DtWorkspaceSource2 = GetUnUsedRecordsDataTable(Helper.FindDataTable(UnmatchedWorkspaceDataSet, MatchSetResultConstants.MATCH_SET_RESULT_TABLE_NAME_SOURCE2));
                rgWorkspaceSource2.DataSource = DtWorkspaceSource2;
                rgWorkspaceSource2.MasterTableView.CurrentPageIndex = 0;
                rgWorkspaceSource2.VirtualItemCount = DtWorkspaceSource2.Rows.Count;
                rgWorkspaceSource2.DataBind();
                if (MatchingType == ARTEnums.MatchingType.AccountMatching)
                {
                    rgUsedRecords.DataSource = UsedRecordsCombinedDataTable;
                    rgUsedRecords.MasterTableView.CurrentPageIndex = 0;
                    rgUsedRecords.DataBind();
                }
                // To bind Label for net amount calculation
                DataTable tblUnmatchedDataSetSource1 = Helper.FindDataTable(UnmatchedDataSet, MatchSetResultConstants.MATCH_SET_RESULT_TABLE_NAME_SOURCE1);
                DataTable tblUnmatchedWorkspaceDataSetSource1 = GetUnUsedRecordsDataTable(Helper.FindDataTable(UnmatchedWorkspaceDataSet, MatchSetResultConstants.MATCH_SET_RESULT_TABLE_NAME_SOURCE1));

                DataTable tblUnmatchedDataSetSource2 = Helper.FindDataTable(UnmatchedDataSet, MatchSetResultConstants.MATCH_SET_RESULT_TABLE_NAME_SOURCE2);
                DataTable tblUnmatchedWorkspaceDataSetSource2 = GetUnUsedRecordsDataTable(Helper.FindDataTable(UnmatchedWorkspaceDataSet, MatchSetResultConstants.MATCH_SET_RESULT_TABLE_NAME_SOURCE2));

                DataTable dtCombinedSource1 = Helper.MergeTables(tblUnmatchedDataSetSource1, tblUnmatchedWorkspaceDataSetSource1);
                DataTable dtCombinedSource2 = Helper.MergeTables(tblUnmatchedDataSetSource2, tblUnmatchedWorkspaceDataSetSource2);

                string AmountColumnName = MatchingHelper.GetAmountColumnName(MatchingConfigurationInfoList);
                decimal? TotalAmountSource1 = 0;
                decimal? TotalAmountSource2 = 0;
                decimal? netAmount = 0;
                MatchSetSubSetCombinationInfoForNetAmountCalculation oMatchSetSubSetCombinationInfoForNetAmountCalculation = MatchingHelper.GetMatchSetSubSetCombinationForNetAmountCalculationByID(MatchSetSubSetCombinationID);
                if (!string.IsNullOrEmpty(AmountColumnName))
                {
                    decimal tmpVal;
                    TotalAmountSource1 = dtCombinedSource1.AsEnumerable().Sum(x => (decimal.TryParse(x[AmountColumnName].ToString(), out tmpVal) ? tmpVal : 0));
                    TotalAmountSource2 = dtCombinedSource2.AsEnumerable().Sum(x => (decimal.TryParse(x[AmountColumnName].ToString(), out tmpVal) ? tmpVal : 0));
                    //TotalAmountSource1 = dtCombinedSource1.AsEnumerable().Sum(x => x.Field<decimal?>(AmountColumnName));
                    //TotalAmountSource2 = dtCombinedSource2.AsEnumerable().Sum(x => x.Field<decimal?>(AmountColumnName));
                    netAmount = (decimal?)(TotalAmountSource1 - TotalAmountSource2);
                }
                lblSourceNamesWithNetValue.Text = oMatchSetSubSetCombinationInfoForNetAmountCalculation.Source1Name + ": " + Helper.GetDisplayDecimalValue(TotalAmountSource1) + " | " + oMatchSetSubSetCombinationInfoForNetAmountCalculation.Source2Name + ": " + Helper.GetDisplayDecimalValue(TotalAmountSource2) + " | " + LanguageUtil.GetValue(2026) + ": " + Helper.GetDisplayDecimalValue(netAmount);
                GridHelper.SetRecordCount(rgUsedRecords);
            }
            catch (Exception ex)
            {
                Helper.ShowErrorMessageFromUserControl(this, ex);
            }
        }
        /// <summary>
        /// Creates the filter info.
        /// </summary>
        private void CreateFilterInfo()
        {
            try
            {
                List<FilterInfo> oFilterInfoList = new List<FilterInfo>();
                FilterInfo oFilterInfo = null;
                if (MatchingConfigurationInfoList != null && MatchingConfigurationInfoList.Count > 0)
                {
                    foreach (MatchingConfigurationInfo oMatchingConfigurationInfo in MatchingConfigurationInfoList)
                    {
                        if (oMatchingConfigurationInfo.IsDisplayedColumn.GetValueOrDefault()
                            && oMatchingConfigurationInfo.MatchingConfigurationID.HasValue
                            && !string.IsNullOrEmpty(oMatchingConfigurationInfo.DisplayColumnName))
                        {
                            oFilterInfo = new FilterInfo();
                            oFilterInfo.ColumnID = oMatchingConfigurationInfo.MatchingConfigurationID;
                            oFilterInfo.ColumnName = GridHelper.RemoveSpecialChars(oMatchingConfigurationInfo.DisplayColumnName);
                            oFilterInfo.DisplayColumnName = oMatchingConfigurationInfo.DisplayColumnName;
                            oFilterInfo.DataType = oMatchingConfigurationInfo.DataTypeID;
                            oFilterInfoList.Add(oFilterInfo);
                        }
                    }
                    oFilterInfo = new FilterInfo();
                    oFilterInfo.ColumnID = MatchSetResultConstants.MATCH_SET_RESULT_COLUMN_ID_DATA_SOURCE_NAME;
                    oFilterInfo.ColumnName = MatchSetResultConstants.MATCH_SET_RESULT_COLUMN_NAME_DATA_SOURCE_NAME;
                    oFilterInfo.DisplayColumnName = MatchSetResultConstants.MATCH_SET_RESULT_COLUMN_NAME_DATA_SOURCE_NAME;
                    oFilterInfo.DataType = (short)WebEnums.DataType.String;
                    oFilterInfoList.Add(oFilterInfo);
                    SessionHelper.SetDynamicFilterColumns(oFilterInfoList, GetGridClientIDKey(rgUnmatched));
                    //SessionHelper.SetDynamicFilterColumns(oFilterInfoList, rgUnmatched.ClientID);
                }
            }
            catch (Exception ex)
            {
                Helper.ShowErrorMessageFromUserControl(this, ex);
            }
        }
        /// <summary>
        /// Get Grid Client key
        /// </summary>
        private string GetGridClientIDKey(ExRadGrid Rg)
        {
            return Rg.ClientID;
        }
        /// <summary>
        /// Applies the filter.
        /// </summary>
        public void ApplyFilter()
        {
            try
            {
                string whereClause = SessionHelper.GetDynamicFilterResultWhereClause(GetGridClientIDKey(rgUnmatched));
                UnmatchedCombinedFilteredDataTable = null;
                if (!string.IsNullOrEmpty(whereClause) && UnmatchedCombinedDataTable != null && UnmatchedCombinedDataTable.Rows.Count > 0)
                {
                    DataRow[] filterResult = UnmatchedCombinedDataTable.Select(GridHelper.GetWithSpecialChars(whereClause));
                    if (filterResult != null && filterResult.Length > 0)
                        UnmatchedCombinedFilteredDataTable = filterResult.CopyToDataTable();
                    else
                        UnmatchedCombinedFilteredDataTable = UnmatchedCombinedDataTable.Clone();
                }
                BindGrids();
            }
            catch (Exception ex)
            {
                Helper.ShowErrorMessageFromUserControl(this, ex);
            }
        }
        public void RebindGrids()
        {
            rgUnmatched.Rebind();
            rgWorkspaceSource1.Rebind();
            rgWorkspaceSource2.Rebind();
            if (MatchingType == ARTEnums.MatchingType.AccountMatching)
            {
                rgUsedRecords.Rebind();
            }
        }
        public void rgUnmatched_ItemCommand(object source, GridCommandEventArgs e)
        {
            if (e.CommandName == TelerikConstants.GridClearFilterCommandName)
            {
                SessionHelper.ClearDynamicFilterData(GetGridClientIDKey(rgUnmatched));
                ApplyFilter();
            }

            if (e.CommandName == TelerikConstants.GridExportToPDFCommandName)
            {
                rgUnmatched.DataSource = UnmatchedCombinedDataTable;
                isExportPDF = true;
                rgUnmatched.MasterTableView.Columns.FindByUniqueName("CheckboxSelectColumn").Visible = false;
                //GridHelper.ExportGridToPDF(rgUnmatched, "Unmatched Records");
                BindGrids();
            }
            if (e.CommandName == TelerikConstants.GridExportToExcelCommandName)
            {
                rgUnmatched.AllowPaging = false;
                rgUnmatched.DataSource = UnmatchedCombinedDataTable;
                isExportExcel = true;
                rgUnmatched.MasterTableView.Columns.FindByUniqueName("CheckboxSelectColumn").Visible = false;
                GridHelper.ExportGridToExcel(rgUnmatched, "Unmatched Records");
                BindGrids();
            }

        }
        protected void rgUnmatched_ItemCreated(object sender, GridItemEventArgs e)
        {
            GridHelper.RegisterPDFAndExcelForPostback(e, isExportPDF, isExportExcel, this.Page);
            GridHelper.SetStylesForExportGrid(e, isExportPDF, isExportExcel);
            if (e.Item is GridPagerItem)
            {
                GridPagerItem gridPager = e.Item as GridPagerItem;
                DropDownList oRadComboBox = (DropDownList)gridPager.FindControl("ddlPageSize");
                if (rgUnmatched.AllowCustomPaging)
                {
                    GridHelper.BindPageSizeGrid(oRadComboBox);
                    if (Session[rgUnmatched.ClientID + "NewPageSize"] != null)
                        oRadComboBox.SelectedValue = Session[rgUnmatched.ClientID + "NewPageSize"].ToString();
                    oRadComboBox.Attributes.Add("onChange", "return ddlPageSize_SelectedIndexChanged('" + oRadComboBox.ClientID + "' , '" + rgUnmatched.ClientID + "');");
                    oRadComboBox.Visible = true;
                }
                else
                {
                    Control pnlPageSizeDDL = gridPager.FindControl("pnlPageSizeDDL");
                    pnlPageSizeDDL.Visible = false;
                }

                Control numericPagerControl = gridPager.GetNumericPager();
                Control placeHolder = gridPager.FindControl("NumericPagerPlaceHolder");
                placeHolder.Controls.Add(numericPagerControl);
            }

        }
        public void rgWorkspaceSource1_ItemCommand(object source, GridCommandEventArgs e)
        {
            if (e.CommandName == TelerikConstants.GridExportToPDFCommandName)
            {
                isExportPDF = true;
                rgWorkspaceSource1.MasterTableView.Columns.FindByUniqueName("CheckboxSelectColumn").Visible = false;
                GridHelper.ExportGridToPDF(rgWorkspaceSource1, "UnmatchedWorkspaceSource1");
                BindGrids();
            }
            if (e.CommandName == TelerikConstants.GridExportToExcelCommandName)
            {
                isExportExcel = true;
                rgWorkspaceSource1.AllowPaging = false;
                rgWorkspaceSource1.MasterTableView.Columns.FindByUniqueName("CheckboxSelectColumn").Visible = false;
                GridHelper.ExportGridToExcel(rgWorkspaceSource1, "UnmatchedWorkspaceSource1");
                BindGrids();
            }
        }
        protected void rgWorkspaceSource1_ItemCreated(object sender, GridItemEventArgs e)
        {
            GridHelper.RegisterPDFAndExcelForPostback(e, isExportPDF, isExportExcel, this.Page);
            GridHelper.SetStylesForExportGrid(e, isExportPDF, isExportExcel);
            if (e.Item is GridPagerItem)
            {
                GridPagerItem gridPager = e.Item as GridPagerItem;
                DropDownList oRadComboBox = (DropDownList)gridPager.FindControl("ddlPageSize");
                if (rgWorkspaceSource1.AllowCustomPaging)
                {
                    GridHelper.BindPageSizeGrid(oRadComboBox);
                    if (Session[rgWorkspaceSource1.ClientID + "NewPageSize"] != null)
                        oRadComboBox.SelectedValue = Session[rgWorkspaceSource1.ClientID + "NewPageSize"].ToString();
                    oRadComboBox.Attributes.Add("onChange", "return ddlPageSize_SelectedIndexChanged('" + oRadComboBox.ClientID + "' , '" + rgWorkspaceSource1.ClientID + "');");
                    oRadComboBox.Visible = true;
                }
                else
                {
                    Control pnlPageSizeDDL = gridPager.FindControl("pnlPageSizeDDL");
                    pnlPageSizeDDL.Visible = false;
                }

                Control numericPagerControl = gridPager.GetNumericPager();
                Control placeHolder = gridPager.FindControl("NumericPagerPlaceHolder");
                placeHolder.Controls.Add(numericPagerControl);
            }
        }
        public void rgWorkspaceSource2_ItemCommand(object source, GridCommandEventArgs e)
        {
            if (e.CommandName == TelerikConstants.GridExportToPDFCommandName)
            {
                isExportPDF = true;
                rgWorkspaceSource2.MasterTableView.Columns.FindByUniqueName("CheckboxSelectColumn").Visible = false;
                GridHelper.ExportGridToPDF(rgWorkspaceSource2, "UnmatchedWorkspaceSource2");
                BindGrids();
            }
            if (e.CommandName == TelerikConstants.GridExportToExcelCommandName)
            {
                isExportExcel = true;
                rgWorkspaceSource2.AllowPaging = false;
                rgWorkspaceSource2.MasterTableView.Columns.FindByUniqueName("CheckboxSelectColumn").Visible = false;
                GridHelper.ExportGridToExcel(rgWorkspaceSource2, "UnmatchedWorkspaceSource2");
                BindGrids();
            }
        }
        protected void rgWorkspaceSource2_ItemCreated(object sender, GridItemEventArgs e)
        {
            GridHelper.RegisterPDFAndExcelForPostback(e, isExportPDF, isExportExcel, this.Page);
            GridHelper.SetStylesForExportGrid(e, isExportPDF, isExportExcel);
            if (e.Item is GridPagerItem)
            {
                GridPagerItem gridPager = e.Item as GridPagerItem;
                DropDownList oRadComboBox = (DropDownList)gridPager.FindControl("ddlPageSize");
                if (rgWorkspaceSource2.AllowCustomPaging)
                {
                    GridHelper.BindPageSizeGrid(oRadComboBox);
                    if (Session[rgWorkspaceSource2.ClientID + "NewPageSize"] != null)
                        oRadComboBox.SelectedValue = Session[rgWorkspaceSource2.ClientID + "NewPageSize"].ToString();
                    oRadComboBox.Attributes.Add("onChange", "return ddlPageSize_SelectedIndexChanged('" + oRadComboBox.ClientID + "' , '" + rgWorkspaceSource2.ClientID + "');");
                    oRadComboBox.Visible = true;
                }
                else
                {
                    Control pnlPageSizeDDL = gridPager.FindControl("pnlPageSizeDDL");
                    pnlPageSizeDDL.Visible = false;
                }

                Control numericPagerControl = gridPager.GetNumericPager();
                Control placeHolder = gridPager.FindControl("NumericPagerPlaceHolder");
                placeHolder.Controls.Add(numericPagerControl);
            }
        }
        public void rgUnmatched_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item.ItemType == GridItemType.Header)
                SessionHelper.ShowGridFilterIcon((PageBase)this.Page, rgUnmatched, e, GetGridClientIDKey(rgUnmatched));
        }
        protected void btnAddToWorkspace_OnClick(object sender, EventArgs e)
        {
            try
            {
                DataTable tblSource1 = Helper.FindDataTable(UnmatchedDataSet, MatchSetResultConstants.MATCH_SET_RESULT_TABLE_NAME_SOURCE1);
                DataTable tblTarget1 = Helper.FindDataTable(UnmatchedWorkspaceDataSet, MatchSetResultConstants.MATCH_SET_RESULT_TABLE_NAME_SOURCE1);
                DataRow[] drSource1 = MatchingHelper.GetSelectedUnmatchedRows(rgUnmatched, tblSource1);

                Helper.MoveDataRows(tblSource1, tblTarget1, drSource1);

                DataTable tblSource2 = Helper.FindDataTable(UnmatchedDataSet, MatchSetResultConstants.MATCH_SET_RESULT_TABLE_NAME_SOURCE2);
                DataTable tblTarget2 = Helper.FindDataTable(UnmatchedWorkspaceDataSet, MatchSetResultConstants.MATCH_SET_RESULT_TABLE_NAME_SOURCE2);
                DataRow[] drSource2 = MatchingHelper.GetSelectedUnmatchedRows(rgUnmatched, tblSource2);

                Helper.MoveDataRows(tblSource2, tblTarget2, drSource2);

                if (OnSaveRequired != null)
                    OnSaveRequired.Invoke(this, new EventArgs());
                CreateCombinedSource();
                UpdateRecItemNumber();
                ApplyFilter();
                BindGrids();
            }
            catch (ARTException ex)
            {
                Helper.ShowErrorMessageFromUserControl(this, ex);
            }
        }
        protected void btnRemoveFromWorkspace_OnClick(object sender, EventArgs e)
        {
            try
            {
                DataTable tblSource1 = Helper.FindDataTable(UnmatchedWorkspaceDataSet, MatchSetResultConstants.MATCH_SET_RESULT_TABLE_NAME_SOURCE1);
                DataTable tblTarget1 = Helper.FindDataTable(UnmatchedDataSet, MatchSetResultConstants.MATCH_SET_RESULT_TABLE_NAME_SOURCE1);
                DataRow[] drSource1 = MatchingHelper.GetSelectedUnmatchedRows(rgWorkspaceSource1, tblSource1);

                Helper.MoveDataRows(tblSource1, tblTarget1, drSource1);

                DataTable tblSource2 = Helper.FindDataTable(UnmatchedWorkspaceDataSet, MatchSetResultConstants.MATCH_SET_RESULT_TABLE_NAME_SOURCE2);
                DataTable tblTarget2 = Helper.FindDataTable(UnmatchedDataSet, MatchSetResultConstants.MATCH_SET_RESULT_TABLE_NAME_SOURCE2);
                DataRow[] drSource2 = MatchingHelper.GetSelectedUnmatchedRows(rgWorkspaceSource2, tblSource2);

                Helper.MoveDataRows(tblSource2, tblTarget2, drSource2);

                if (OnSaveRequired != null)
                    OnSaveRequired.Invoke(this, new EventArgs());

                CreateCombinedSource();
                ApplyFilter();
                BindGrids();
            }
            catch (ARTException ex)
            {
                Helper.ShowErrorMessageFromUserControl(this, ex);
            }
        }
        protected void btnMatch_OnClick(object sender, EventArgs e)
        {
            try
            {
                DataTable tblSource1 = Helper.FindDataTable(UnmatchedWorkspaceDataSet, MatchSetResultConstants.MATCH_SET_RESULT_TABLE_NAME_SOURCE1);
                DataTable tblSource2 = Helper.FindDataTable(UnmatchedWorkspaceDataSet, MatchSetResultConstants.MATCH_SET_RESULT_TABLE_NAME_SOURCE2);
                DataRow[] dataRowsSource1 = MatchingHelper.GetSelectedUnmatchedRows(rgWorkspaceSource1, tblSource1);
                DataRow[] dataRowsSource2 = MatchingHelper.GetSelectedUnmatchedRows(rgWorkspaceSource2, tblSource2);
                if (OnMatchClick != null && dataRowsSource1.Length > 0 && dataRowsSource1.Length > 0)
                {
                    bool bResult = OnMatchClick.Invoke(tblSource1, dataRowsSource1, tblSource2, dataRowsSource2);
                    if (bResult && OnSaveRequired != null)
                        OnSaveRequired.Invoke(this, new EventArgs());
                }
                BindGrids();
            }
            catch (ARTException ex)
            {
                Helper.ShowErrorMessageFromUserControl(this, ex);
            }
        }
        protected void btnCreateRecItem_OnClick(object sender, EventArgs e)
        {
            try
            {
                DataTable tblSource1 = Helper.FindDataTable(UnmatchedWorkspaceDataSet, MatchSetResultConstants.MATCH_SET_RESULT_TABLE_NAME_SOURCE1);
                DataTable tblSource2 = Helper.FindDataTable(UnmatchedWorkspaceDataSet, MatchSetResultConstants.MATCH_SET_RESULT_TABLE_NAME_SOURCE2);
                DataTable tblRecItem = Helper.MergeTables(tblSource1, tblSource2);
                if (tblRecItem != null && tblRecItem.Rows.Count > 0 && OnCreateRecItemClick != null)
                {
                    OnCreateRecItemClick.Invoke(tblRecItem);
                    return;
                }
                BindGrids();
            }
            catch (ARTException ex)
            {
                Helper.ShowErrorMessageFromUserControl(this, ex);
            }
        }
        protected void btnCloseRecItem_OnClick(object sender, EventArgs e)
        {
            try
            {
                DataTable tblSource1 = Helper.FindDataTable(UnmatchedWorkspaceDataSet, MatchSetResultConstants.MATCH_SET_RESULT_TABLE_NAME_SOURCE1);
                DataTable tblSource2 = Helper.FindDataTable(UnmatchedWorkspaceDataSet, MatchSetResultConstants.MATCH_SET_RESULT_TABLE_NAME_SOURCE2);
                DataTable tblRecItem = Helper.MergeTables(tblSource1, tblSource2);
                if (tblRecItem != null && tblRecItem.Rows.Count > 0 && OnCloseRecItemHandler != null)
                {
                    OnCloseRecItemHandler.Invoke(tblRecItem);
                    return;
                }
                BindGrids();
            }
            catch (ARTException ex)
            {
                Helper.ShowErrorMessageFromUserControl(this, ex);
            }
        }
        protected void rgUnmatched_OnNeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            rgUnmatched.DataSource = UnmatchedCombinedDataTable;
        }
        protected void rgWorkspaceSource1_OnNeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            rgWorkspaceSource1.DataSource = GetUnUsedRecordsDataTable(Helper.FindDataTable(UnmatchedWorkspaceDataSet, MatchSetResultConstants.MATCH_SET_RESULT_TABLE_NAME_SOURCE1));
        }
        protected void rgWorkspaceSource2_OnNeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            rgWorkspaceSource2.DataSource = GetUnUsedRecordsDataTable(Helper.FindDataTable(UnmatchedWorkspaceDataSet, MatchSetResultConstants.MATCH_SET_RESULT_TABLE_NAME_SOURCE2));
        }
        protected void rgUsedRecords_OnNeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            rgUsedRecords.DataSource = UsedRecordsCombinedDataTable;
        }
        protected void rgWorkspaceSource1_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item.ItemType == GridItemType.Item || e.Item.ItemType == GridItemType.AlternatingItem)
            {
                GridDataItem oGridDataItem = e.Item as GridDataItem;
                if (oGridDataItem != null)
                {
                    CheckBox checkBox = (CheckBox)(oGridDataItem)["CheckboxSelectColumn"].Controls[0];
                    DataRow dr = ((DataRowView)oGridDataItem.DataItem).Row;
                    string recItemNumber = Convert.ToString(dr[MatchSetResultConstants.MATCH_SET_RESULT_COLUMN_NAME_REC_ITEM_NUMBER]);
                    checkBox.Enabled = String.IsNullOrEmpty(recItemNumber);
                }
            }
        }
        protected void rgWorkspaceSource2_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item.ItemType == GridItemType.Item || e.Item.ItemType == GridItemType.AlternatingItem)
            {
                GridDataItem oGridDataItem = e.Item as GridDataItem;
                if (oGridDataItem != null)
                {
                    CheckBox checkBox = (CheckBox)(oGridDataItem)["CheckboxSelectColumn"].Controls[0];
                    DataRow dr = ((DataRowView)oGridDataItem.DataItem).Row;
                    string recItemNumber = Convert.ToString(dr[MatchSetResultConstants.MATCH_SET_RESULT_COLUMN_NAME_REC_ITEM_NUMBER]);
                    checkBox.Enabled = String.IsNullOrEmpty(recItemNumber);
                }
            }
        }
        protected void rgUsedRecords_OnItemDataBound(object sender, GridItemEventArgs e)
        {


            if (e.Item.ItemType == GridItemType.Item || e.Item.ItemType == GridItemType.AlternatingItem)
            {
                GridDataItem oGridDataItem = e.Item as GridDataItem;
                if (oGridDataItem != null)
                {

                    DataRow dr = ((DataRowView)oGridDataItem.DataItem).Row;
                    ExHyperLink hlViewData = (ExHyperLink)e.Item.FindControl("hlCloseDate");
                    long excelRowNumber;
                    long matchSetMatchingSourceDataImportID;
                    long.TryParse(dr[MatchSetResultConstants.MATCH_SET_RESULT_COLUMN_NAME_EXCEL_ROW_NUMBER].ToString(), out excelRowNumber);
                    long.TryParse(dr[MatchSetResultConstants.MATCH_SET_RESULT_COLUMN_NAME_MATCH_SET_MATCHING_SOURCE_DATA_IMPORT_ID].ToString(), out matchSetMatchingSourceDataImportID);
                    if (!String.IsNullOrEmpty(Convert.ToString(dr[MatchSetResultConstants.MATCH_SET_RESULT_COLUMN_NAME_CLOSE_DATE])))
                    {
                        hlViewData.Text = Helper.GetDisplayDate(Convert.ToDateTime(dr[MatchSetResultConstants.MATCH_SET_RESULT_COLUMN_NAME_CLOSE_DATE]));
                        hlViewData.NavigateUrl = "javascript:OpenRadWindowForHyperlink('ViewRecItems.aspx?" + QueryStringConstants.MATCHSET_SUBSET_COMBINATION_ID + "=" + this.MatchSetSubSetCombinationID + "&" + QueryStringConstants.EXCEL_ROW_NUMBER + "=" + excelRowNumber + "&" + QueryStringConstants.ACCOUNT_ID + "=" + this.AccountID + "&" + QueryStringConstants.GLDATA_ID + "=" + this.GlDataID + "&" + QueryStringConstants.MATCH_SET_MATCHING_SOURCE_DATA_IMPORT_ID + "=" + matchSetMatchingSourceDataImportID + "', 450, 800, '" + hlViewData.ClientID + "');";
                    }
                    else
                        hlViewData.Text = Helper.GetDisplayDate(null);
                }
            }
        }
        private DataTable GetUsedRecordsDataTable(DataTable oSourceDataTable)
        {
            DataTable UsedRecordDataTable = oSourceDataTable.Clone();
            string filterString = MatchSetResultConstants.MATCH_SET_RESULT_COLUMN_NAME_REC_ITEM_NUMBER + "<> '' OR " + MatchSetResultConstants.MATCH_SET_RESULT_COLUMN_NAME_REC_ITEM_NUMBER + " IS NOT NULL" + " OR " + MatchSetResultConstants.MATCH_SET_RESULT_COLUMN_NAME_CLOSE_DATE + "<> '' OR " + MatchSetResultConstants.MATCH_SET_RESULT_COLUMN_NAME_CLOSE_DATE + " IS NOT NULL";
            DataRow[] UsedRecords = null;
            UsedRecords = oSourceDataTable.Select(filterString);
            if (UsedRecords != null && UsedRecords.Length > 0)
                UsedRecordDataTable = UsedRecords.CopyToDataTable();
            return UsedRecordDataTable;
        }
        private DataTable GetUnUsedRecordsDataTable(DataTable oSourceDataTable)
        {
            DataTable UsedRecordDataTable = oSourceDataTable.Clone();
            string filterString = "(" + MatchSetResultConstants.MATCH_SET_RESULT_COLUMN_NAME_REC_ITEM_NUMBER + "= '' OR " + MatchSetResultConstants.MATCH_SET_RESULT_COLUMN_NAME_REC_ITEM_NUMBER + " IS NULL" + ") AND ( " + MatchSetResultConstants.MATCH_SET_RESULT_COLUMN_NAME_CLOSE_DATE + "= '' OR " + MatchSetResultConstants.MATCH_SET_RESULT_COLUMN_NAME_CLOSE_DATE + " IS NULL )"; ;
            DataRow[] UsedRecords = null;
            UsedRecords = oSourceDataTable.Select(filterString);
            if (UsedRecords != null && UsedRecords.Length > 0)
                UsedRecordDataTable = UsedRecords.CopyToDataTable();
            return UsedRecordDataTable;
        }

        protected void rgUnmatched_PageSizeChanged(object source, GridPageSizeChangedEventArgs e)
        {
            Session[rgUnmatched.ClientID + "NewPageSize"] = e.NewPageSize.ToString();
            if (UnmatchedCombinedDataTable != null)
            rgUnmatched.VirtualItemCount = UnmatchedCombinedDataTable.Rows.Count;
        }
        protected void rgWorkspaceSource1_PageSizeChanged(object source, GridPageSizeChangedEventArgs e)
        {
            Session[rgWorkspaceSource1.ClientID + "NewPageSize"] = e.NewPageSize.ToString();
            DataTable Dt = GetUnUsedRecordsDataTable(Helper.FindDataTable(UnmatchedWorkspaceDataSet, MatchSetResultConstants.MATCH_SET_RESULT_TABLE_NAME_SOURCE1));
            if (Dt != null)
                rgWorkspaceSource1.VirtualItemCount = Dt.Rows.Count;
        }
        protected void rgWorkspaceSource2_PageSizeChanged(object source, GridPageSizeChangedEventArgs e)
        {
            Session[rgWorkspaceSource2.ClientID + "NewPageSize"] = e.NewPageSize.ToString();
            DataTable Dt = GetUnUsedRecordsDataTable(Helper.FindDataTable(UnmatchedWorkspaceDataSet, MatchSetResultConstants.MATCH_SET_RESULT_TABLE_NAME_SOURCE2));
            if (Dt != null)
                rgWorkspaceSource2.VirtualItemCount = Dt.Rows.Count;
        }
        //protected void rgUnmatched_PageIndexChanged(object source, GridPageChangedEventArgs e)
        //{
        //    MatchingHelper.SaveCheckBoxStates(rgUnmatched, UnmatchedCombinedDataTable);
        //}
        //protected void Page_PreRender(object sender, EventArgs e)
        //{

        //    MatchingHelper.RePopulateCheckBoxStates(rgUnmatched, UnmatchedCombinedDataTable);
        //}

    }
}