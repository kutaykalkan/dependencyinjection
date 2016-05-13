using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SkyStem.Library.Controls.TelerikWebControls;
using SkyStem.ART.Web.Classes;
using System.Xml;
using Telerik.Web.UI;
using System.Collections;
using SkyStem.ART.Client.Model.Matching;
using SkyStem.ART.Web.Data;
using SkyStem.ART.Web.Utility;
using System.Data;
using SkyStem.ART.Client.Exception;
using SkyStem.Language.LanguageUtility;
using SkyStem.Library.Controls.WebControls;
using SkyStem.Library.Controls.TelerikWebControls.Data;

namespace SkyStem.ART.Web.UserControls
{
    public partial class Matched : UserControlBase
    {

        public delegate bool UnmatchHandler(DataSet sourceDataSet, DataRow[] dataRows);
        public event UnmatchHandler OnUnmatchClick;
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
        public DataSet MatchedDataSet
        {
            get { return (DataSet)Session[SessionConstants.MATCHING_MATCH_SET_RESULT_MATCHED_SOURCE]; }
            set { Session[SessionConstants.MATCHING_MATCH_SET_RESULT_MATCHED_SOURCE] = value; }
        }

        public DataSet MatchedWorkspaceDataSet
        {
            get { return (DataSet)Session[SessionConstants.MATCHING_MATCH_SET_RESULT_MATCHED_WORKSPACE_SOURCE]; }
            set { Session[SessionConstants.MATCHING_MATCH_SET_RESULT_MATCHED_WORKSPACE_SOURCE] = value; }
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
                    rgMatched.MasterTableView.DataKeyNames = MatchingHelper.GetDataKeyNamesForMatched();
                    rgWorkspace.MasterTableView.DataKeyNames = MatchingHelper.GetDataKeyNamesForMatched();
                    MatchingHelper.CreateRecItemPairIdColumn(rgMatched);
                    MatchingHelper.CreateRecItemNetValueColumn(rgMatched);
                    MatchingHelper.CreateGridPairColumns(rgMatched, MatchingConfigurationInfoList, WebEnums.MatchingResultType.Matched);
                    MatchingHelper.CreateRecItemPairIdColumn(rgWorkspace);
                    MatchingHelper.CreateRecItemNetValueColumn(rgWorkspace);
                    MatchingHelper.CreateGridPairColumns(rgWorkspace, MatchingConfigurationInfoList, WebEnums.MatchingResultType.Matched);
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
            Helper.ClearDynamicColumns(rgMatched);
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
        }

        /// <summary>
        /// Load XML data into DataSet
        /// </summary>
        private void LoadDataSet()
        {
            if (CurrentMatchSetResultWorkspaceInfo != null)
            {
                Helper.LoadXmlToDataSet(MatchedDataSet, CurrentMatchSetResultWorkspaceInfo.MatchData);
                Helper.LoadXmlToDataSet(MatchedWorkspaceDataSet, CurrentMatchSetResultWorkspaceInfo.WorkspaceMatchData);
            }
            else
            {
                Helper.LoadXmlToDataSet(MatchedDataSet, string.Empty);
                Helper.LoadXmlToDataSet(MatchedWorkspaceDataSet, string.Empty);
            }
        }

        /// <summary>
        /// Bind the Grids
        /// </summary>
        public void BindGrids()
        {
            CreateColumns();
            BindMatchedWorkspaceGrid(0);
            BindMatchedGrid(0);
            // To bind Label for net amount calculation
            DataTable tblMatchedDataSetSource1 = Helper.FindDataTable(MatchedDataSet, MatchSetResultConstants.MATCH_SET_RESULT_TABLE_NAME_SOURCE1);
            DataTable tblMatchedWorkspaceDataSetSource1 = Helper.FindDataTable(MatchedWorkspaceDataSet, MatchSetResultConstants.MATCH_SET_RESULT_TABLE_NAME_SOURCE1);

            DataTable tblMatchedDataSetSource2 = Helper.FindDataTable(MatchedDataSet, MatchSetResultConstants.MATCH_SET_RESULT_TABLE_NAME_SOURCE2);
            DataTable tblMatchedWorkspaceDataSetSource2 = Helper.FindDataTable(MatchedWorkspaceDataSet, MatchSetResultConstants.MATCH_SET_RESULT_TABLE_NAME_SOURCE2);

            DataTable dtCombinedSource1 = Helper.MergeTables(tblMatchedDataSetSource1, tblMatchedWorkspaceDataSetSource1);
            DataTable dtCombinedSource2 = Helper.MergeTables(tblMatchedDataSetSource2, tblMatchedWorkspaceDataSetSource2);

            string AmountColumnName = MatchingHelper.GetAmountColumnName(MatchingConfigurationInfoList);
            decimal? TotalAmountSource1 = 0;
            decimal? TotalAmountSource2 = 0;
            decimal? netAmount = 0;
            MatchSetSubSetCombinationInfoForNetAmountCalculation oMatchSetSubSetCombinationInfoForNetAmountCalculation = MatchingHelper.GetMatchSetSubSetCombinationForNetAmountCalculationByID(MatchSetSubSetCombinationID);
            if (!string.IsNullOrEmpty(AmountColumnName))
            {
                decimal tmpVal;
               // y = groupedTable.Sum(o => (o[amountColumn] != System.DBNull.Value && decimal.TryParse(o[amountColumn].ToString(), out tmpVal) ? tmpVal : 0))
               // TotalAmountSource1 = dtCombinedSource1.AsEnumerable().Sum(x => x.Field<decimal?>(AmountColumnName));
                //TotalAmountSource2 = dtCombinedSource2.AsEnumerable().Sum(x => x.Field<decimal?>(AmountColumnName));
                  TotalAmountSource1 = dtCombinedSource1.AsEnumerable().Sum(x => (decimal.TryParse(x[AmountColumnName].ToString(), out tmpVal) ? tmpVal : 0));
                  TotalAmountSource2 = dtCombinedSource2.AsEnumerable().Sum(x => (decimal.TryParse(x[AmountColumnName].ToString(), out tmpVal) ? tmpVal : 0));
                netAmount = (decimal?)(TotalAmountSource1 - TotalAmountSource2);
            }
            lblSourceNamesWithNetValue.Text = oMatchSetSubSetCombinationInfoForNetAmountCalculation.Source1Name + ": " + Helper.GetDisplayDecimalValue(TotalAmountSource1) + " | " + oMatchSetSubSetCombinationInfoForNetAmountCalculation.Source2Name + ": " + Helper.GetDisplayDecimalValue(TotalAmountSource2) + " | " + LanguageUtil.GetValue(2026) + ": " + Helper.GetDisplayDecimalValue(netAmount);
        }
        private void BindMatchedGrid(int pageIndex)
        {
            DataTable Dt = Helper.FindDataTable(MatchedDataSet, MatchSetResultConstants.MATCH_SET_RESULT_TABLE_NAME_PAIR);
            rgMatched.DataSource = MatchingHelper.CalculatePairNetValue(MatchedDataSet, Dt, MatchingHelper.GetAmountColumnName(MatchingConfigurationInfoList));
            rgMatched.VirtualItemCount = Dt.Rows.Count;
            rgMatched.MasterTableView.CurrentPageIndex = pageIndex;
            rgMatched.DataBind();
        }
        private void BindMatchedWorkspaceGrid(int pageIndex)
        {
            DataTable Dt = Helper.FindDataTable(MatchedWorkspaceDataSet, MatchSetResultConstants.MATCH_SET_RESULT_TABLE_NAME_PAIR);
            rgWorkspace.DataSource = MatchingHelper.CalculatePairNetValue(MatchedWorkspaceDataSet, Dt, MatchingHelper.GetAmountColumnName(MatchingConfigurationInfoList));
            rgWorkspace.VirtualItemCount = Dt.Rows.Count;
            rgWorkspace.MasterTableView.CurrentPageIndex = pageIndex;
            rgWorkspace.DataBind();
        }

        protected void btnAddToWorkspace_OnClick(object sender, EventArgs e)
        {
            try
            {
                string pairIDStr = MatchingHelper.GetSelectedPairIDStr(rgMatched);
                if (pairIDStr != string.Empty)
                {
                    MatchingHelper.MovePairs(MatchedDataSet, MatchedWorkspaceDataSet, pairIDStr);
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
                    MatchingHelper.MovePairs(MatchedWorkspaceDataSet, MatchedDataSet, pairIDStr);
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
                DataRow[] drPairRows = MatchingHelper.GetSelectedPairRows(rgWorkspace, MatchedWorkspaceDataSet);
                if (OnUnmatchClick != null && drPairRows != null && drPairRows.Length > 0)
                {
                    bool bResult = OnUnmatchClick.Invoke(MatchedWorkspaceDataSet, drPairRows);
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

        protected void rgMatched_OnNeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            rgMatched.DataSource = Helper.FindDataTable(MatchedDataSet, MatchSetResultConstants.MATCH_SET_RESULT_TABLE_NAME_PAIR);
        }

        protected void rgMatched_ItemDataBound(object sender, GridItemEventArgs e)
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
                    hlViewData.NavigateUrl = "javascript:OpenRadWindowForHyperlink('ViewPairData.aspx?" + QueryStringConstants.MATCH_SET_RESULT_COLUMN_NAME_PAIR_ID + "=" + dr[MatchSetResultConstants.MATCH_SET_RESULT_COLUMN_NAME_PAIR_ID] + "&" + QueryStringConstants.MATCHING_RESULT_TYPE + "=" + WebEnums.MatchingResultType.Matched + "&" + QueryStringConstants.IS_FROM_WORK_SPACE + "=0" + "', 550, 900, '" + hlViewData.ClientID + "');";
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
                    //e.Item.Cells[rgWorkspace.Columns.FindByUniqueName(MatchSetResultConstants.MATCH_SET_RESULT_COLUMN_NAME_PAIR_ID).OrderIndex].Text =
                    //Helper.GetDisplayIntegerValue(Convert.ToInt64(dr[MatchSetResultConstants.MATCH_SET_RESULT_COLUMN_NAME_PAIR_ID]));
                    hlViewData.NavigateUrl = "javascript:OpenRadWindowForHyperlink('ViewPairData.aspx?" + QueryStringConstants.MATCH_SET_RESULT_COLUMN_NAME_PAIR_ID + "=" + dr[MatchSetResultConstants.MATCH_SET_RESULT_COLUMN_NAME_PAIR_ID] + "&" + QueryStringConstants.MATCHING_RESULT_TYPE + "=" + WebEnums.MatchingResultType.Matched + "&" + QueryStringConstants.IS_FROM_WORK_SPACE + "=1" + "', 550, 900, '" + hlViewData.ClientID + "');";
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

        protected void rgWorkspace_OnNeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            rgWorkspace.DataSource = Helper.FindDataTable(MatchedWorkspaceDataSet, MatchSetResultConstants.MATCH_SET_RESULT_TABLE_NAME_PAIR);
        }
        protected void rgMatched_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            BindMatchedGrid(e.NewPageIndex);
        }

        protected void rgMatched_ItemCreated(object sender, GridItemEventArgs e)
        {

            if (e.Item is GridPagerItem)
            {
                GridPagerItem gridPager = e.Item as GridPagerItem;
                DropDownList oRadComboBox = (DropDownList)gridPager.FindControl("ddlPageSize");
                if (rgMatched.AllowCustomPaging)
                {
                    GridHelper.BindPageSizeGrid(oRadComboBox);
                    if (Session[rgMatched.ClientID + "NewPageSize"] != null)
                        oRadComboBox.SelectedValue = Session[rgMatched.ClientID + "NewPageSize"].ToString();
                    oRadComboBox.Attributes.Add("onChange", "return ddlPageSize_SelectedIndexChanged('" + oRadComboBox.ClientID + "' , '" + rgMatched.ClientID + "');");
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
        protected void rgMatched_PageSizeChanged(object source, GridPageSizeChangedEventArgs e)
        {
            Session[rgMatched.ClientID + "NewPageSize"] = e.NewPageSize.ToString();
        }
        protected void rgWorkspace_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            BindMatchedWorkspaceGrid(e.NewPageIndex);
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
        public void rgMatched_ItemCommand(object source, GridCommandEventArgs e)
        {
            if (e.CommandName == TelerikConstants.GridExportToExcelCommandName)
            {
                try
                {
                    DataTable Dt = Helper.FindDataTable(MatchedDataSet, MatchSetResultConstants.MATCH_SET_RESULT_TABLE_NAME_PAIR);
                    MatchingHelper.CalculatePairNetValue(MatchedDataSet, Dt, MatchingHelper.GetAmountColumnName(MatchingConfigurationInfoList));
                    DataTable tblSource1 = Helper.FindDataTable(MatchedDataSet, MatchSetResultConstants.MATCH_SET_RESULT_TABLE_NAME_SOURCE1);
                    DataTable tblSource2 = Helper.FindDataTable(MatchedDataSet, MatchSetResultConstants.MATCH_SET_RESULT_TABLE_NAME_SOURCE2);
                    DataTable matchedCombinedDataTable = Helper.MergeTables(tblSource1, tblSource2);
                    MatchingHelper.UpdateNetValue(Dt, matchedCombinedDataTable);
                    DataTable matchedDataTable = MatchingHelper.ManageColumnsForExporToExcel(matchedCombinedDataTable, MatchingConfigurationInfoList);
                    Session[SessionConstants.EXPORT_TO_EXCEL_DATA_TABLE] = matchedDataTable;
                    Response.Redirect(string.Format("downloader?{0}={1}&{2}={3}", QueryStringConstants.SESSION_ID, SessionConstants.EXPORT_TO_EXCEL_DATA_TABLE, QueryStringConstants.FILE_NAME, "Matched"));
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
                    DataTable Dt = Helper.FindDataTable(MatchedWorkspaceDataSet, MatchSetResultConstants.MATCH_SET_RESULT_TABLE_NAME_PAIR);
                    DataTable tblSource1 = Helper.FindDataTable(MatchedWorkspaceDataSet, MatchSetResultConstants.MATCH_SET_RESULT_TABLE_NAME_SOURCE1);
                    DataTable tblSource2 = Helper.FindDataTable(MatchedWorkspaceDataSet, MatchSetResultConstants.MATCH_SET_RESULT_TABLE_NAME_SOURCE2);
                    DataTable CombinedDataTable = Helper.MergeTables(tblSource1, tblSource2);
                    MatchingHelper.UpdateNetValue(Dt, CombinedDataTable);
                    DataTable dt = MatchingHelper.ManageColumnsForExporToExcel(CombinedDataTable, MatchingConfigurationInfoList);
                    Session[SessionConstants.EXPORT_TO_EXCEL_DATA_TABLE] = dt;
                    Response.Redirect(string.Format("downloader?{0}={1}&{2}={3}", QueryStringConstants.SESSION_ID, SessionConstants.EXPORT_TO_EXCEL_DATA_TABLE, QueryStringConstants.FILE_NAME, "MatchedWorkspace"));
                }
                catch (ARTException ex)
                {
                    Helper.ShowErrorMessageFromUserControl(this, ex);
                }
            }
        }


    }
}