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
using SkyStem.ART.Client.Exception;
using SkyStem.Language.LanguageUtility;
using SkyStem.Library.Controls.TelerikWebControls.Data;
using SkyStem.Library.Controls.WebControls;


namespace SkyStem.ART.Web.UserControls
{
    public partial class PartialMatched : UserControlBase
    {

        public delegate bool UnmatchHandler(DataSet sourceDataSet, DataRow[] dataRows);
        public delegate bool MatchHandler(DataSet sourceDataSet, DataRow[] dataRows);
        public event UnmatchHandler OnUnmatchClick;
        public event MatchHandler OnMatchClick;
        public event EventHandler OnSaveRequired;
        protected string SelectionMsg = "";
        private Int64? _MatchSetSubSetCombinationID;

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
        /// Matching Source Data Set
        /// </summary>
        public DataSet PartialMatchedDataSet
        {
            get { return (DataSet)Session[SessionConstants.MATCHING_MATCH_SET_RESULT_PARTIAL_MATCHED_SOURCE]; }
            set { Session[SessionConstants.MATCHING_MATCH_SET_RESULT_PARTIAL_MATCHED_SOURCE] = value; }
        }

        public DataSet PartialMatchedWorkspaceDataSet
        {
            get { return (DataSet)Session[SessionConstants.MATCHING_MATCH_SET_RESULT_PARTIAL_MATCHED_WORKSPACE_SOURCE]; }
            set { Session[SessionConstants.MATCHING_MATCH_SET_RESULT_PARTIAL_MATCHED_WORKSPACE_SOURCE] = value; }
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

        protected void Page_Load(object sender, EventArgs e)
        {
            CreateColumns();
            SelectionMsg = LanguageUtil.GetValue(2013);
        }

        /// <summary>
        /// Create Grid Columns
        /// </summary>
        private void CreateColumns()
        {
            try
            {
                if (MatchingConfigurationInfoList != null)
                {
                    rgPartialMatched.MasterTableView.DataKeyNames = MatchingHelper.GetDataKeyNamesForMatched();
                    rgWorkspace.MasterTableView.DataKeyNames = MatchingHelper.GetDataKeyNamesForMatched();
                    MatchingHelper.CreateRecItemPairIdColumn(rgPartialMatched);
                    MatchingHelper.CreateRecItemNetValueColumn(rgPartialMatched);
                    MatchingHelper.CreateGridPairColumns(rgPartialMatched, MatchingConfigurationInfoList, WebEnums.MatchingResultType.PartialMatched);
                    MatchingHelper.CreateRecItemPairIdColumn(rgWorkspace);
                    MatchingHelper.CreateRecItemNetValueColumn(rgWorkspace);
                    MatchingHelper.CreateGridPairColumns(rgWorkspace, MatchingConfigurationInfoList, WebEnums.MatchingResultType.PartialMatched);
                }
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
            Helper.ClearDynamicColumns(rgPartialMatched);
            Helper.ClearDynamicColumns(rgWorkspace);
            LoadDataSet();
            if (bindDataRequired)
                BindGrids();
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
            btnUnMatch.Enabled = isEditMode;
            btnMatch.Enabled = isEditMode;
        }

        /// <summary>
        /// Load XML data into DataSet
        /// </summary>
        private void LoadDataSet()
        {
            if (CurrentMatchSetResultWorkspaceInfo != null)
            {
                Helper.LoadXmlToDataSet(PartialMatchedDataSet, CurrentMatchSetResultWorkspaceInfo.PartialMatchData);
                Helper.LoadXmlToDataSet(PartialMatchedWorkspaceDataSet, CurrentMatchSetResultWorkspaceInfo.WorkspacePartialMatchData);
            }
            else
            {
                Helper.LoadXmlToDataSet(PartialMatchedDataSet, string.Empty);
                Helper.LoadXmlToDataSet(PartialMatchedWorkspaceDataSet, string.Empty);
            }
        }

        /// <summary>
        /// Bind the Grids
        /// </summary>
        public void BindGrids()
        {
            CreateColumns();
            BindPartialMatchedGrid(0);
            BindPartialMatchedWorkspaceGrid(0);
            // To bind Label for net amount calculation
            DataTable tblPartialMatchedDataSetSource1 = Helper.FindDataTable(PartialMatchedDataSet, MatchSetResultConstants.MATCH_SET_RESULT_TABLE_NAME_SOURCE1);
            DataTable tblPartialMatchedWorkspaceDataSetSource1 = Helper.FindDataTable(PartialMatchedWorkspaceDataSet, MatchSetResultConstants.MATCH_SET_RESULT_TABLE_NAME_SOURCE1);

            DataTable tblPartialMatchedDataSetSource2 = Helper.FindDataTable(PartialMatchedDataSet, MatchSetResultConstants.MATCH_SET_RESULT_TABLE_NAME_SOURCE2);
            DataTable tblPartialMatchedWorkspaceDataSetSource2 = Helper.FindDataTable(PartialMatchedWorkspaceDataSet, MatchSetResultConstants.MATCH_SET_RESULT_TABLE_NAME_SOURCE2);

            DataTable dtCombinedSource1 = Helper.MergeTables(tblPartialMatchedDataSetSource1, tblPartialMatchedWorkspaceDataSetSource1);
            DataTable dtCombinedSource2 = Helper.MergeTables(tblPartialMatchedDataSetSource2, tblPartialMatchedWorkspaceDataSetSource2);

            string AmountColumnName = MatchingHelper.GetAmountColumnName(MatchingConfigurationInfoList);
            decimal? TotalAmountSource1 = 0;
            decimal? TotalAmountSource2 = 0;
            decimal? netAmount = 0;
            MatchSetSubSetCombinationInfoForNetAmountCalculation oMatchSetSubSetCombinationInfoForNetAmountCalculation = MatchingHelper.GetMatchSetSubSetCombinationForNetAmountCalculationByID(MatchSetSubSetCombinationID);
            if (!string.IsNullOrEmpty(AmountColumnName))
            {
                TotalAmountSource1 = dtCombinedSource1.AsEnumerable().Sum(x => x.Field<decimal?>(AmountColumnName));
                TotalAmountSource2 = dtCombinedSource2.AsEnumerable().Sum(x => x.Field<decimal?>(AmountColumnName));
                netAmount = (decimal?)(TotalAmountSource1 - TotalAmountSource2);
            }
            lblSourceNamesWithNetValue.Text = oMatchSetSubSetCombinationInfoForNetAmountCalculation.Source1Name + ": " + Helper.GetDisplayDecimalValue(TotalAmountSource1) + " | " + oMatchSetSubSetCombinationInfoForNetAmountCalculation.Source2Name + ": " + Helper.GetDisplayDecimalValue(TotalAmountSource2) + " | " + LanguageUtil.GetValue(2026) + ": " + Helper.GetDisplayDecimalValue(netAmount);
        }
        private void BindPartialMatchedGrid(int pageIndex)
        {
            DataTable Dt = Helper.FindDataTable(PartialMatchedDataSet, MatchSetResultConstants.MATCH_SET_RESULT_TABLE_NAME_PAIR);
            rgPartialMatched.DataSource = MatchingHelper.CalculatePairNetValue(PartialMatchedDataSet, Dt, MatchingHelper.GetAmountColumnName(MatchingConfigurationInfoList));
            rgPartialMatched.VirtualItemCount = Dt.Rows.Count;
            rgPartialMatched.MasterTableView.CurrentPageIndex = pageIndex;
            rgPartialMatched.DataBind();
        }
        private void BindPartialMatchedWorkspaceGrid(int pageIndex)
        {
            DataTable Dt = Helper.FindDataTable(PartialMatchedWorkspaceDataSet, MatchSetResultConstants.MATCH_SET_RESULT_TABLE_NAME_PAIR);
            rgWorkspace.DataSource = MatchingHelper.CalculatePairNetValue(PartialMatchedWorkspaceDataSet, Dt, MatchingHelper.GetAmountColumnName(MatchingConfigurationInfoList));
            rgWorkspace.VirtualItemCount = Dt.Rows.Count;
            rgWorkspace.MasterTableView.CurrentPageIndex = pageIndex;
            rgWorkspace.DataBind();
        }

        protected void btnAddToWorkspace_OnClick(object sender, EventArgs e)
        {
            try
            {
                string pairIDStr = MatchingHelper.GetSelectedPairIDStr(rgPartialMatched);
                if (pairIDStr != string.Empty)
                {
                    MatchingHelper.MovePairs(PartialMatchedDataSet, PartialMatchedWorkspaceDataSet, pairIDStr);
                    if (OnSaveRequired != null)
                        OnSaveRequired(this, new EventArgs());
                }
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
                string pairIDStr = MatchingHelper.GetSelectedPairIDStr(rgWorkspace);
                if (pairIDStr != string.Empty)
                {
                    MatchingHelper.MovePairs(PartialMatchedWorkspaceDataSet, PartialMatchedDataSet, pairIDStr);
                    if (OnSaveRequired != null)
                        OnSaveRequired(this, new EventArgs());
                }
                BindGrids();
            }
            catch (ARTException ex)
            {
                Helper.ShowErrorMessageFromUserControl(this, ex);
            }
        }

        protected void btnUnMatch_OnClick(object sender, EventArgs e)
        {
            try
            {
                DataRow[] drPairRows = MatchingHelper.GetSelectedPairRows(rgWorkspace, PartialMatchedWorkspaceDataSet);
                if (OnUnmatchClick != null && drPairRows != null && drPairRows.Length > 0)
                {
                    bool bResult = OnUnmatchClick.Invoke(PartialMatchedWorkspaceDataSet, drPairRows);
                    if (bResult && OnSaveRequired != null)
                        OnSaveRequired(this, new EventArgs());

                }
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
                DataRow[] drPairRows = MatchingHelper.GetSelectedPairRows(rgWorkspace, PartialMatchedWorkspaceDataSet);
                if (OnMatchClick != null && drPairRows != null && drPairRows.Length > 0)
                {
                    bool bResult = OnMatchClick.Invoke(PartialMatchedWorkspaceDataSet, drPairRows);
                    if (bResult && OnSaveRequired != null)
                        OnSaveRequired(this, new EventArgs());
                }
                BindGrids();
            }
            catch (ARTException ex)
            {
                Helper.ShowErrorMessageFromUserControl(this, ex);
            }
        }

        protected void rgPartialMatched_OnNeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            rgPartialMatched.DataSource = Helper.FindDataTable(PartialMatchedDataSet, MatchSetResultConstants.MATCH_SET_RESULT_TABLE_NAME_PAIR);
        }

        protected void rgWorkspace_OnNeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            rgWorkspace.DataSource = Helper.FindDataTable(PartialMatchedWorkspaceDataSet, MatchSetResultConstants.MATCH_SET_RESULT_TABLE_NAME_PAIR);
        }

        protected void rgPartialMatched_ItemDataBound(object sender, GridItemEventArgs e)
        {
            try
            {
                if (e.Item is GridDataItem)
                {
                    GridDataItem itm = (GridDataItem)e.Item;
                    int S1Rows = 0;
                    int S2Rows = 0;
                    int MaxRowsInPair = Convert.ToInt32(AppSettingHelper.GetAppSettingValue(AppSettingConstants.DISPLAY_RECORDS_LIMIT_IN_A_MATCHING_PAIR));
                    int PairRowCount = MatchingHelper.GetRowsCount(itm, out  S1Rows, out  S2Rows);
                    DataRowView dr = (DataRowView)e.Item.DataItem;
                    ExHyperLink hlViewData = (ExHyperLink)e.Item.FindControl("hlViewData");
                    hlViewData.NavigateUrl = "javascript:OpenRadWindowForHyperlink('ViewPairData.aspx?" + QueryStringConstants.MATCH_SET_RESULT_COLUMN_NAME_PAIR_ID + "=" + dr[MatchSetResultConstants.MATCH_SET_RESULT_COLUMN_NAME_PAIR_ID] + "&" + QueryStringConstants.MATCHING_RESULT_TYPE + "=" + WebEnums.MatchingResultType.PartialMatched + "&" + QueryStringConstants.IS_FROM_WORK_SPACE + "=0" + "', 550, 900, '" + hlViewData.ClientID + "');";
                    if (PairRowCount > MaxRowsInPair)
                        hlViewData.Visible = true;
                    else
                        hlViewData.Visible = false;
                }
            }
            catch (ARTException ex)
            {
                Helper.ShowErrorMessageFromUserControl(this, ex);
            }
        }

        protected void rgWorkspace_ItemDataBound(object sender, GridItemEventArgs e)
        {
            try
            {
                if (e.Item is GridDataItem)
                {
                    GridDataItem itm = (GridDataItem)e.Item;
                    int S1Rows = 0;
                    int S2Rows = 0;
                    int MaxRowsInPair = Convert.ToInt32(AppSettingHelper.GetAppSettingValue(AppSettingConstants.DISPLAY_RECORDS_LIMIT_IN_A_MATCHING_PAIR));
                    int PairRowCount = MatchingHelper.GetRowsCount(itm, out  S1Rows, out  S2Rows);
                    DataRowView dr = (DataRowView)e.Item.DataItem;
                    ExHyperLink hlViewData = (ExHyperLink)e.Item.FindControl("hlViewData");
                    hlViewData.NavigateUrl = "javascript:OpenRadWindowForHyperlink('ViewPairData.aspx?" + QueryStringConstants.MATCH_SET_RESULT_COLUMN_NAME_PAIR_ID + "=" + dr[MatchSetResultConstants.MATCH_SET_RESULT_COLUMN_NAME_PAIR_ID] + "&" + QueryStringConstants.MATCHING_RESULT_TYPE + "=" + WebEnums.MatchingResultType.PartialMatched + "&" + QueryStringConstants.IS_FROM_WORK_SPACE + "=1" + "', 550, 900, '" + hlViewData.ClientID + "');";
                    if (PairRowCount > MaxRowsInPair)
                        hlViewData.Visible = true;
                    else
                        hlViewData.Visible = false;
                }
            }
            catch (ARTException ex)
            {
                Helper.ShowErrorMessageFromUserControl(this, ex);
            }
        }
        protected void rgPartialMatched_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            BindPartialMatchedGrid(e.NewPageIndex);
        }
       
        protected void rgPartialMatched_ItemCreated(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridPagerItem)
            {
                GridPagerItem gridPager = e.Item as GridPagerItem;
                DropDownList oRadComboBox = (DropDownList)gridPager.FindControl("ddlPageSize");
                if (rgPartialMatched.AllowCustomPaging)
                {
                   GridHelper.BindPageSizeGrid(oRadComboBox);
                    if (Session[rgPartialMatched.ClientID + "NewPageSize"] != null)
                        oRadComboBox.SelectedValue = Session[rgPartialMatched.ClientID + "NewPageSize"].ToString();
                    oRadComboBox.Attributes.Add("onChange", "return ddlPageSize_SelectedIndexChanged('" + oRadComboBox.ClientID + "' , '" + rgPartialMatched.ClientID + "');");
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
        protected void rgPartialMatched_PageSizeChanged(object source, GridPageSizeChangedEventArgs e)
        {
            Session[rgPartialMatched.ClientID + "NewPageSize"] = e.NewPageSize.ToString();
        }
        protected void rgWorkspace_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            BindPartialMatchedWorkspaceGrid(e.NewPageIndex);
        }
        protected void rgWorkspace_ItemCreated(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridPagerItem)
            {
                GridPagerItem gridPager = e.Item as GridPagerItem;
                DropDownList oRadComboBox = (DropDownList)gridPager.FindControl("ddlPageSize");
                if (rgWorkspace.AllowCustomPaging)
                {
                   GridHelper.BindPageSizeGrid(oRadComboBox);
                    if (Session[rgWorkspace.ClientID + "NewPageSize"] != null)
                        oRadComboBox.SelectedValue = Session[rgWorkspace.ClientID + "NewPageSize"].ToString();
                    oRadComboBox.Attributes.Add("onChange", "return ddlPageSize_SelectedIndexChanged('" + oRadComboBox.ClientID + "' , '" + rgWorkspace.ClientID + "');");
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
        protected void rgWorkspace_PageSizeChanged(object source, GridPageSizeChangedEventArgs e)
        {
            Session[rgWorkspace.ClientID + "NewPageSize"] = e.NewPageSize.ToString();
        }
        public void rgPartialMatched_ItemCommand(object source, GridCommandEventArgs e)
        {
            if (e.CommandName == TelerikConstants.GridExportToExcelCommandName)
            {
                try
                {
                    DataTable Dt = Helper.FindDataTable(PartialMatchedDataSet, MatchSetResultConstants.MATCH_SET_RESULT_TABLE_NAME_PAIR);
                    MatchingHelper.CalculatePairNetValue(PartialMatchedDataSet, Dt, MatchingHelper.GetAmountColumnName(MatchingConfigurationInfoList));
                    DataTable tblSource1 = Helper.FindDataTable(PartialMatchedDataSet, MatchSetResultConstants.MATCH_SET_RESULT_TABLE_NAME_SOURCE1);
                    DataTable tblSource2 = Helper.FindDataTable(PartialMatchedDataSet, MatchSetResultConstants.MATCH_SET_RESULT_TABLE_NAME_SOURCE2);
                    DataTable CombinedDataTable = Helper.MergeTables(tblSource1, tblSource2);
                    MatchingHelper.UpdateNetValue(Dt, CombinedDataTable);
                    DataTable PartialMatchedDataTable = MatchingHelper.ManageColumnsForExporToExcel(CombinedDataTable, MatchingConfigurationInfoList);
                    Session[SessionConstants.EXPORT_TO_EXCEL_DATA_TABLE] = PartialMatchedDataTable;
                    Response.Redirect(string.Format("downloader?{0}={1}&{2}={3}", QueryStringConstants.SESSION_ID, SessionConstants.EXPORT_TO_EXCEL_DATA_TABLE, QueryStringConstants.FILE_NAME, "PartialMatched"));
                }
                catch (ARTException ex)
                {
                    Helper.ShowErrorMessageFromUserControl(this, ex);
                }
            }
        }
        public void rgWorkspace_ItemCommand(object source, GridCommandEventArgs e)
        {
            if (e.CommandName == TelerikConstants.GridExportToExcelCommandName)
            {
                try
                {

                    DataTable Dt = Helper.FindDataTable(PartialMatchedWorkspaceDataSet, MatchSetResultConstants.MATCH_SET_RESULT_TABLE_NAME_PAIR);
                    DataTable tblSource1 = Helper.FindDataTable(PartialMatchedWorkspaceDataSet, MatchSetResultConstants.MATCH_SET_RESULT_TABLE_NAME_SOURCE1);
                    DataTable tblSource2 = Helper.FindDataTable(PartialMatchedWorkspaceDataSet, MatchSetResultConstants.MATCH_SET_RESULT_TABLE_NAME_SOURCE2);
                    DataTable CombinedDataTable = Helper.MergeTables(tblSource1, tblSource2);
                    MatchingHelper.UpdateNetValue(Dt, CombinedDataTable);
                    DataTable dt = MatchingHelper.ManageColumnsForExporToExcel(CombinedDataTable, MatchingConfigurationInfoList);
                    Session[SessionConstants.EXPORT_TO_EXCEL_DATA_TABLE] = dt;
                    Response.Redirect(string.Format("downloader?{0}={1}&{2}={3}", QueryStringConstants.SESSION_ID, SessionConstants.EXPORT_TO_EXCEL_DATA_TABLE, QueryStringConstants.FILE_NAME, "PartialMatchedWorkspace"));
                }
                catch (ARTException ex)
                {
                    Helper.ShowErrorMessageFromUserControl(this, ex);
                }
            }
        }


    }
}