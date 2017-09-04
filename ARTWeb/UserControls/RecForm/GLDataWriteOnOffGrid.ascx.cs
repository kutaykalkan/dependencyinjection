using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using SkyStem.ART.Web.Classes;
using SkyStem.Library.Controls.TelerikWebControls;
using SkyStem.Library.Controls.TelerikWebControls.Grid;
using Telerik.Web.UI;
using SkyStem.ART.Client.IServices;
using SkyStem.ART.Client.Model;
using System.Collections.Generic;
using SkyStem.Language.LanguageUtility;
using SkyStem.ART.Web.Utility;
using SkyStem.Library.Controls.WebControls;
using SkyStem.ART.Web.Data;
using SkyStem.ART.Web.UserControls;
using System.Text;
using SkyStem.ART.Client.Data;
using SkyStem.ART.Client.Exception;
using SkyStem.Library.Controls.TelerikWebControls.Data;


namespace SkyStem.ART.Web.UserControls
{

    public partial class UserControls_GLDataWriteOnOffGrid : UserControlRecItemBase
    {
        bool isExportPDF;
        bool isExportExcel;
        private int _BasePageTitleLabelID = 0;
        public event GridCommandEventHandler GridCommand;
        public event GridItemEventHandler GridItemDataBound;
        /// <summary>
        /// Base Page Title 
        /// </summary>
        public int BasePageTitleLabelID
        {
            get { return _BasePageTitleLabelID; }
            set { _BasePageTitleLabelID = value; }
        }
        /// <summary>
        /// The contained control "RadGrid"
        /// </summary>
        public ExRadGrid Grid
        {
            get
            {
                return rgGLDataWriteOnOffItems;
            }
        }
        /// <summary>
        /// Allow Export To Excel
        /// </summary>
        private bool _AllowExportToExcel = false;
        public bool AllowExportToExcel
        {
            get { return _AllowExportToExcel; }
            set
            {
                _AllowExportToExcel = value;
                rgGLDataWriteOnOffItems.AllowExportToExcel = _AllowExportToExcel;
            }

        }
        /// <summary>
        /// Allow Export To Excel
        /// </summary>
        private bool _AllowExportToPDF = false;
        public bool AllowExportToPDF
        {
            get { return _AllowExportToPDF; }
            set
            {
                _AllowExportToPDF = value;
                rgGLDataWriteOnOffItems.AllowExportToPDF = _AllowExportToPDF;
            }
        }
        /// <summary>
        /// Allow  Custom Filter
        /// </summary>
        private bool _AllowCustomFilter = false;
        public bool AllowCustomFilter
        {
            get { return _AllowCustomFilter; }
            set
            {
                _AllowCustomFilter = value;
                rgGLDataWriteOnOffItems.AllowCustomFilter = _AllowCustomFilter;
            }
        }
        /// <summary>
        /// Allow  Custom Paging
        /// </summary>
        private bool _AllowCustomPaging = false;
        public bool AllowCustomPaging
        {
            get { return _AllowCustomPaging; }
            set
            {
                _AllowCustomPaging = value;
                rgGLDataWriteOnOffItems.AllowCustomPaging = _AllowCustomPaging;
            }
        }
        /// <summary>
        /// Allow  Custom Paging
        /// </summary>
        private bool _AllowSelectionPersist = false;
        public bool AllowSelectionPersist
        {
            get { return _AllowSelectionPersist; }
            set
            {
                _AllowSelectionPersist = value;
            }
        }
        /// <summary>
        /// Is On Page 
        /// </summary>
        private bool _IsOnPage = true;
        public bool IsOnPage
        {
            get { return _IsOnPage; }
            set
            { _IsOnPage = value; }
        }
        /// <summary>
        /// Allow  Custom Paging
        /// </summary>
        private bool _AllowDisplayCurrencyPnl = false;
        public bool AllowDisplayCurrencyPnl
        {
            get { return _AllowDisplayCurrencyPnl; }
            set
            {
                _AllowDisplayCurrencyPnl = value;
                if (_AllowDisplayCurrencyPnl)
                {
                    rgGLDataWriteOnOffItems.ClientSettings.ClientEvents.OnRowDeselected = "rgGLDataWriteOnOffItemSelected";
                    rgGLDataWriteOnOffItems.ClientSettings.ClientEvents.OnRowSelected = "rgGLDataWriteOnOffItemSelected";
                }
            }
        }
        /// <summary>
        /// Grid's -> Master Table View Column Collection
        /// </summary>
        [PersistenceMode(PersistenceMode.InnerProperty)]
        public GridColumnCollection ColumnCollection
        {
            get
            {
                return rgGLDataWriteOnOffItems.MasterTableView.Columns;
            }
        }
        [PersistenceMode(PersistenceMode.InnerProperty)]
        public GridClientSettings ClientSettings
        {
            get
            {
                return rgGLDataWriteOnOffItems.ClientSettings;
            }
        }
        /// <summary>
        /// Show Select CheckBox Colum
        /// </summary>
        private bool selectOption = true;
        public bool ShowSelectCheckBoxColum
        {
            get { return selectOption; }
            set { selectOption = value; }
        }
        /// <summary>
        /// Show  CloseDate  Colum
        /// </summary>
        private bool _ShowCloseDateColum = true;
        public bool ShowCloseDateColum
        {
            get { return _ShowCloseDateColum; }
            set { _ShowCloseDateColum = value; }
        }
        /// <summary>
        /// Show Description Colum
        /// </summary>
        private bool _ShowDescriptionColum = true;
        public bool ShowDescriptionColum
        {
            get { return _ShowDescriptionColum; }
            set { _ShowDescriptionColum = value; }
        }
        /// <summary>
        /// IsMultiCurrencyActivated
        /// </summary>
        public bool IsMultiCurrencyActivated
        {
            get
            {
                if (ViewState["IsMultiCurrencyActivated"] != null)
                    return (bool)ViewState["IsMultiCurrencyActivated"];
                else
                    return false;
            }
            set { ViewState["IsMultiCurrencyActivated"] = value; }
        }
        /// <summary>
        /// GLDataWriteOnOffs Grid ShowFooter
        /// </summary>
        public bool ShowFooter
        {
            get { return rgGLDataWriteOnOffItems.ShowFooter; }
            set { rgGLDataWriteOnOffItems.ShowFooter = value; }
        }
        /// <summary>
        /// GLDataWriteOnOffs Grid Data Table
        /// </summary>
        private DataTable GLDataWriteOnOffItemsGridDataTable
        {
            get
            {
                if (GLDataWriteOnOffItemsGridFilteredDataTable != null)
                    return GLDataWriteOnOffItemsGridFilteredDataTable;
                return (DataTable)Session[GetUniqueSessionKey()];
            }
            set { Session[GetUniqueSessionKey()] = value; }
        }
        /// <summary>
        /// GLAdjustment Grid Filtered Data Table
        /// </summary>
        private DataTable GLDataWriteOnOffItemsGridFilteredDataTable
        {
            get { return (DataTable)Session[GetFilteredDataUniqueSessionKey()]; }
            set { Session[GetFilteredDataUniqueSessionKey()] = value; }
        }
        protected override void OnInit(EventArgs e)
        {
            Helper.SetGridImageButtonProperties(this.rgGLDataWriteOnOffItems.MasterTableView.Columns);
            string url;
            url = Page.ResolveClientUrl("~/Pages/GridDynamicFilter.aspx");
            if (_IsOnPage)
                url = url + "?griddynamicfiltersessionkey=" + rgGLDataWriteOnOffItems.ClientID;
            else
                url = url + "?griddynamicfiltersessionkey=" + rgGLDataWriteOnOffItems.ClientID + "&IsForPopup=1";

            rgGLDataWriteOnOffItems.GridApplyFilterOnClientClick = string.Format("ucLoadGridApplyFilterPage('{0}');return false;", url);
            base.OnInit(e);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                isExportPDF = false;
                isExportExcel = false;
                Session[rgGLDataWriteOnOffItems.ClientID + "NewPageSize"] = "10";
                CreateFilterInfoForGLDataWriteOnOffsGrid();
            }
            GetGridApplyFilterScript();
            if (_AllowDisplayCurrencyPnl)
                GetGridSelectedScript();
            if (_ShowCloseDateColum)
                rgGLDataWriteOnOffItems.EntityNameLabelID = 1873;
            else
                rgGLDataWriteOnOffItems.EntityNameLabelID = 1872;
            pnlCurrency.Visible = _AllowDisplayCurrencyPnl;
        }
        /// <summary>
        /// Get Grid Apply Filter Script
        /// </summary>
        /// 
        private void GetGridApplyFilterScript()
        {
            StringBuilder osb = new StringBuilder();
            osb.Append(@" function ucLoadGridApplyFilterPage(id){");
            //osb.Append(@" alert(id);");
            osb.Append(@" var urlGridApplyFilter = '../GridDynamicFilter.aspx?griddynamicfiltersessionkey=' + id ;");
            if (_IsOnPage)
                osb.Append(@" OpenRadWindowForHyperlink(id, 480, 800);");
            else
                osb.Append(@" OpenRadWindowFromRadWindow(id, 480, 800);");
            osb.Append(@"return false;}");
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), rgGLDataWriteOnOffItems.ClientID + "Testing", osb.ToString(), true);
        }
        /// <summary>
        /// Get Grid Selected Script
        /// </summary>
        private void GetGridSelectedScript()
        {
            StringBuilder osb = new StringBuilder();
            osb.Append(@"function rgGLDataWriteOnOffItemSelected(sender, args) {");
            osb.Append(@"var baseCurrencyValue = 0;");
            osb.Append(@"var reportingCurrencyValue = 0;");
            osb.Append(@" var masterTable = sender.get_masterTableView();");
            osb.Append(@" var selectedRows = masterTable.get_selectedItems();");
            osb.Append(@" for (var i = 0; i < selectedRows.length; i++) {");
            osb.Append(@" var row = selectedRows[i];");
            if (!String.IsNullOrEmpty(this.CurrentBCCY))
                osb.Append(@"  baseCurrencyValue = baseCurrencyValue + Round(row.getDataKeyValue('AmountBaseCurrency'), 2);");
            osb.Append(@" reportingCurrencyValue = reportingCurrencyValue + Round(row.getDataKeyValue('AmountReportingCurrency'), 2);");
            //osb.Append(@" alert(baseCurrencyValue);");          
            osb.Append(@" }");
            osb.Append(@" var lblBaseCurrency = document.getElementById('" + this.lblBaseCurrencyValue.ClientID + "');");
            osb.Append(@" var lblReportingCurrency = document.getElementById('" + this.lblReportingCurrencyValue.ClientID + "');");
            if (!String.IsNullOrEmpty(this.CurrentBCCY))
                osb.Append(@" lblBaseCurrency.firstChild.data = GetDisplayDecimalValue(baseCurrencyValue, 2);");
            else
            {
                osb.Append(@" lblBaseCurrency.firstChild.data = '-';");
            }
            osb.Append(@" lblReportingCurrency.firstChild.data = GetDisplayDecimalValue(reportingCurrencyValue, 2);");
            osb.Append(@" }");
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), rgGLDataWriteOnOffItems.ClientID + "GridSelectedScript", osb.ToString(), true);
        }
        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (_AllowSelectionPersist)
                RePopulateCheckBoxStates();
        }
        /// <summary>
        /// Bind GLDataWriteOnOffs Grid
        /// </summary>
        private void BindrgGLDataWriteOnOffItemsGrid(int PageIndex)
        {
            if (GLDataWriteOnOffItemsGridDataTable != null)
            {
                rgGLDataWriteOnOffItems.DataSource = GLDataWriteOnOffItemsGridDataTable;
                rgGLDataWriteOnOffItems.VirtualItemCount = GLDataWriteOnOffItemsGridDataTable.Rows.Count;
                rgGLDataWriteOnOffItems.DataBind();
            }
        }
        /// <summary>
        /// Bind Load Grid Data 
        /// </summary>
        public void LoadGridData()
        {
            BindrgGLDataWriteOnOffItemsGrid(0);
        }
        /// <summary>
        /// Selected GLDataWriteOnOffIDList  List<long>
        /// </summary>
        public List<long> SelectedGLDataWriteOnOffIDs()
        {
            List<long> oSelectedGLDataWriteOnOffIDList = null;
            if (_AllowSelectionPersist)
            {
                SaveCheckBoxStates();
                if (Session[getCheckedItemsSessionKey()] != null)
                    oSelectedGLDataWriteOnOffIDList = (List<long>)Session[getCheckedItemsSessionKey()];
            }
            else
                oSelectedGLDataWriteOnOffIDList = GridSelectedItems();
            return oSelectedGLDataWriteOnOffIDList;
        }
        /// <summary>
        /// Selected GLDataWriteOnOffInfo Collection
        /// </summary>
        /// 
        public List<GLDataWriteOnOffInfo> SelectedGLDataWriteOnOffInfoCollection()
        {
            List<GLDataWriteOnOffInfo> oGLDataWriteOnOffInfoCollection = null;
            List<GLDataWriteOnOffInfo> oAllGLDataWriteOnOffInfoCollection = (List<GLDataWriteOnOffInfo>)Session[GetUniqueSessionKey() + "List"];
            List<long> oSelectedGLDataWriteOnOffIDList = SelectedGLDataWriteOnOffIDs();
            if (oAllGLDataWriteOnOffInfoCollection != null && oSelectedGLDataWriteOnOffIDList != null)
                oGLDataWriteOnOffInfoCollection = (from oGLDataWriteOnOffInfo in oAllGLDataWriteOnOffInfoCollection
                                                   join SelectedGLDataWriteOnOffID in oSelectedGLDataWriteOnOffIDList on oGLDataWriteOnOffInfo.GLDataWriteOnOffID equals SelectedGLDataWriteOnOffID
                                                   select oGLDataWriteOnOffInfo).ToList();
            return oGLDataWriteOnOffInfoCollection;
        }
        /// <summary>
        /// Selected GLDataWriteOnOffID on Current Page
        /// </summary>
        /// 
        private List<long> GridSelectedItems()
        {
            List<long> oSelectedGLDataWriteOnOffIDList = null;
            oSelectedGLDataWriteOnOffIDList = new List<long>();
            long GLDataWriteOnOffID;
            foreach (GridDataItem item in rgGLDataWriteOnOffItems.SelectedItems)
            {
                GLDataWriteOnOffID = Convert.ToInt64(item.GetDataKeyValue("GLDataWriteOnOffID"));
                oSelectedGLDataWriteOnOffIDList.Add(GLDataWriteOnOffID);
            }
            return oSelectedGLDataWriteOnOffIDList;
        }
        /// <summary>
        /// Bind Load Grid Data 
        /// </summary>
        private string GetGridClientIDKey(ExRadGrid Rg)
        {
            return Rg.ClientID;
        }
        /// <summary>
        /// Create FilterInfo For GLDataWriteOnOff Grid
        /// </summary>
        private void CreateFilterInfoForGLDataWriteOnOffsGrid()
        {
            List<FilterInfo> oFilterInfoList = new List<FilterInfo>();
            FilterInfo oFilterInfo = null;

            oFilterInfo = new FilterInfo();
            oFilterInfo.ColumnID = 1;
            oFilterInfo.ColumnName = "Comments";
            oFilterInfo.DisplayColumnName = Helper.GetLabelIDValue(1408);
            oFilterInfo.DataType = (short)WebEnums.DataType.String;
            oFilterInfoList.Add(oFilterInfo);

            oFilterInfo = new FilterInfo();
            oFilterInfo.ColumnID = 2;
            oFilterInfo.ColumnName = "Amount";
            oFilterInfo.DisplayColumnName = Helper.GetLabelIDValue(1675);
            oFilterInfo.DataType = (short)WebEnums.DataType.Decimal;
            oFilterInfoList.Add(oFilterInfo);

            oFilterInfo = new FilterInfo();
            oFilterInfo.ColumnID = 3;
            oFilterInfo.ColumnName = "AmountReportingCurrency";
            oFilterInfo.DataType = (short)WebEnums.DataType.Decimal;
            oFilterInfo.DisplayColumnName = Helper.GetLabelIDValue(1674);
            oFilterInfoList.Add(oFilterInfo);

            oFilterInfo = new FilterInfo();
            oFilterInfo.ColumnID = 4;
            oFilterInfo.ColumnName = "AmountBaseCurrency";
            oFilterInfo.DataType = (short)WebEnums.DataType.Decimal;
            oFilterInfo.DisplayColumnName = Helper.GetLabelIDValue(1673);
            oFilterInfoList.Add(oFilterInfo);

            oFilterInfo = new FilterInfo();
            oFilterInfo.ColumnID = 5;
            oFilterInfo.ColumnName = "OpenDate";
            oFilterInfo.DisplayColumnName = Helper.GetLabelIDValue(1511);
            oFilterInfo.DataType = (short)WebEnums.DataType.DataTime;
            oFilterInfoList.Add(oFilterInfo);

            oFilterInfo = new FilterInfo();
            oFilterInfo.ColumnID = 6;
            oFilterInfo.ColumnName = "RecItemNumber";
            oFilterInfo.DisplayColumnName = Helper.GetLabelIDValue(2120);
            oFilterInfo.DataType = (short)WebEnums.DataType.String;
            oFilterInfoList.Add(oFilterInfo);

            oFilterInfo = new FilterInfo();
            oFilterInfo.ColumnID = 7;
            oFilterInfo.ColumnName = "MatchSetRefNumber";
            oFilterInfo.DisplayColumnName = Helper.GetLabelIDValue(2276);
            oFilterInfo.DataType = (short)WebEnums.DataType.String;
            oFilterInfoList.Add(oFilterInfo);

            if (_ShowCloseDateColum)
            {
                oFilterInfo = new FilterInfo();
                oFilterInfo.ColumnID = 8;
                oFilterInfo.ColumnName = "CloseDate";
                oFilterInfo.DisplayColumnName = Helper.GetLabelIDValue(1411);
                oFilterInfo.DataType = (short)WebEnums.DataType.DataTime;
                oFilterInfoList.Add(oFilterInfo);
            }
            oFilterInfo = new FilterInfo();
            oFilterInfo.ColumnID = 9;
            oFilterInfo.ColumnName = "UserName";
            oFilterInfo.DisplayColumnName = Helper.GetLabelIDValue(1508);
            oFilterInfo.DataType = (short)WebEnums.DataType.String;
            oFilterInfoList.Add(oFilterInfo);

            SessionHelper.SetDynamicFilterColumns(oFilterInfoList, GetGridClientIDKey(rgGLDataWriteOnOffItems));

        }
        private string GetUniqueSessionKey()
        {
            return GetGridClientIDKey(rgGLDataWriteOnOffItems) + SessionConstants.WRITE_OFF_ON_GRID_DATA;
        }
        private string GetFilteredDataUniqueSessionKey()
        {
            return GetGridClientIDKey(rgGLDataWriteOnOffItems) + SessionConstants.WRITE_OFF_ON_GRID_DATA_FILTERED;
        }
        /// <summary>
        /// Apply Filter GLDataWriteOnOffs Grid
        /// </summary>
        public void ApplyFilterGLDataWriteOnOffsGrid()
        {
            ClearCheckedItemViewState();
            ClearTotals();
            string whereClause = SessionHelper.GetDynamicFilterResultWhereClause(GetGridClientIDKey(rgGLDataWriteOnOffItems));
            GLDataWriteOnOffItemsGridFilteredDataTable = null;
            if (!string.IsNullOrEmpty(whereClause) && GLDataWriteOnOffItemsGridDataTable != null && GLDataWriteOnOffItemsGridDataTable.Rows.Count > 0)
            {
                DataRow[] filterResult = GLDataWriteOnOffItemsGridDataTable.Select(whereClause);
                if (filterResult != null && filterResult.Length > 0)
                    GLDataWriteOnOffItemsGridFilteredDataTable = filterResult.CopyToDataTable();
                else
                    GLDataWriteOnOffItemsGridFilteredDataTable = GLDataWriteOnOffItemsGridDataTable.Clone();
            }
            BindrgGLDataWriteOnOffItemsGrid(0);
        }
        /// <summary>
        /// Set GLDataWriteOnOff Grid Data
        /// </summary>
        /// 
        public void SetGLDataWriteOnOffGridData(List<GLDataWriteOnOffInfo> oGLDataWriteOnOffInfoCollection)
        {
            ClearGLDataWriteOnOffGridData();
            if (oGLDataWriteOnOffInfoCollection != null)
            {
                Session[GetUniqueSessionKey() + "List"] = oGLDataWriteOnOffInfoCollection;
                GLDataWriteOnOffItemsGridDataTable = MatchingHelper.ListToDataTable(oGLDataWriteOnOffInfoCollection);
                ClearCheckedItemViewState();
            }
        }
        /// <summary>
        /// Clear GLDataWriteOnOff Grid Data
        /// </summary>
        /// 
        private void ClearGLDataWriteOnOffGridData()
        {
            GLDataWriteOnOffItemsGridDataTable = null;
            GLDataWriteOnOffItemsGridFilteredDataTable = null;
            Session[GetUniqueSessionKey() + "List"] = null;
        }
        #region GLDataWriteOnOffs Grid Events
        protected void rgGLDataWriteOnOffItems_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {

            rgGLDataWriteOnOffItems.DataSource = GLDataWriteOnOffItemsGridDataTable;
            if (GLDataWriteOnOffItemsGridDataTable != null)
                rgGLDataWriteOnOffItems.VirtualItemCount = GLDataWriteOnOffItemsGridDataTable.Rows.Count;

        }
        Decimal? TotalAmount = null;
        Decimal? BaseCurrencyTotal = null;
        Decimal? ReportingCurrencyTotal = null;
        private void ClearTotals()
        {
            TotalAmount = null;
            BaseCurrencyTotal = null;
            ReportingCurrencyTotal = null;
        }
        protected void rgGLDataWriteOnOffItems_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {

            if (e.Item.ItemType == GridItemType.Header)
            {
                rgGLDataWriteOnOffItems.MasterTableView.Columns.FindByUniqueName("Description").Visible = _ShowDescriptionColum;
                rgGLDataWriteOnOffItems.MasterTableView.Columns.FindByUniqueName("CloseDate").Visible = _ShowCloseDateColum;
                rgGLDataWriteOnOffItems.MasterTableView.Columns.FindByUniqueName("CheckboxSelectColumn").Visible = this.selectOption;
                if (!_IsOnPage)
                    //    SessionHelper.ShowGridFilterIcon((PageBase)this.Page, rgGLDataWriteOnOffItems, e, GetGridClientIDKey(rgGLDataWriteOnOffItems));
                    ////else
                    SessionHelper.ShowGridFilterIcon((PopupPageBase)this.Page, rgGLDataWriteOnOffItems, e, GetGridClientIDKey(rgGLDataWriteOnOffItems));
            }
            if (e.Item.ItemType == Telerik.Web.UI.GridItemType.Item || e.Item.ItemType == Telerik.Web.UI.GridItemType.AlternatingItem)
            {


                GridDataItem oGridDataItem = e.Item as GridDataItem;
                DataRow dr = ((DataRowView)oGridDataItem.DataItem).Row;
                ExLabel lblDescription = (ExLabel)e.Item.FindControl("lblDescription");
                if (isExportExcel || isExportPDF)
                    lblDescription.Text = Convert.ToString(dr["Comments"]);
                else
                    Helper.SetTextAndTooltipValue(lblDescription, Convert.ToString(dr["Comments"]));
                ExLabel lblAmount = (ExLabel)e.Item.FindControl("lblAmount");
                lblAmount.Text = Helper.GetDisplayCurrencyValue(Convert.ToString(dr["LocalCurrencyCode"]), Convert.ToDecimal(dr["Amount"]));
                decimal AmountBaseCurrency;
                decimal.TryParse(Convert.ToString(dr["AmountBaseCurrency"]), out AmountBaseCurrency);
                ExLabel lblAmountBaseCurrency = (ExLabel)e.Item.FindControl("lblAmountBaseCurrency");
                //lblAmountBaseCurrency.Text = Helper.GetDisplayDecimalValue(AmountBaseCurrency);
                lblAmountBaseCurrency.Text = Helper.GetDisplayBaseCurrencyValue(this.CurrentBCCY, AmountBaseCurrency);
                ExLabel lblAmountReportingCurrency = (ExLabel)e.Item.FindControl("lblAmountReportingCurrency");
                lblAmountReportingCurrency.Text = Helper.GetDisplayDecimalValue(Convert.ToDecimal(dr["AmountReportingCurrency"]));
                ExLabel lblOpenDate = (ExLabel)e.Item.FindControl("lblOpenDate");
                lblOpenDate.Text = Helper.GetDisplayDate(Convert.ToDateTime(dr["OpenDate"]));
                DateTime? closeDate = null;
                if (_ShowCloseDateColum)
                {
                    ExLabel lblCloseDate = (ExLabel)e.Item.FindControl("lblCloseDate");
                    closeDate = Convert.ToDateTime(dr["CloseDate"]);
                    lblCloseDate.Text = Helper.GetDisplayDate(closeDate);
                }
                ExLabel lblRecItemNumber = (ExLabel)e.Item.FindControl("lblRecItemNumber");
                lblRecItemNumber.Text = Helper.GetDisplayStringValue(dr["RecItemNumber"].ToString());
                ExLabel lblUserName = (ExLabel)e.Item.FindControl("lblUserName");
                lblUserName.Text = Helper.GetDisplayStringValue(dr["UserName"].ToString());

                ExLabel lblAging = (ExLabel)e.Item.FindControl("lblAging");
                lblAging.Text = Helper.GetDisplayIntegerValue(Helper.GetDaysBetweenDateRanges(Convert.ToDateTime(dr["OpenDate"]), (closeDate.GetValueOrDefault() == default(DateTime)) ? DateTime.Now : closeDate));
                if (IsMultiCurrencyActivated)
                {
                    if (TotalAmount.HasValue)
                        TotalAmount += Convert.ToDecimal(dr["Amount"]);
                    else
                        TotalAmount = Convert.ToDecimal(dr["Amount"]);
                }
                if (BaseCurrencyTotal.HasValue)
                    BaseCurrencyTotal += AmountBaseCurrency;
                else
                    BaseCurrencyTotal = AmountBaseCurrency;
                if (ReportingCurrencyTotal.HasValue)
                    ReportingCurrencyTotal += Convert.ToDecimal(dr["AmountReportingCurrency"]);
                else
                    ReportingCurrencyTotal = Convert.ToDecimal(dr["AmountReportingCurrency"]);
            }
            if (e.Item.ItemType == GridItemType.Footer)
            {
                if (IsMultiCurrencyActivated)
                {
                    ExLabel lblLocalCurrencyTotal = (ExLabel)e.Item.FindControl("lblLocalCurrencyTotal");
                    lblLocalCurrencyTotal.Text = Helper.GetDisplayDecimalValue(TotalAmount);
                }
                ExLabel lblBaseCurrencyTotal = (ExLabel)e.Item.FindControl("lblBaseCurrencyTotal");
                lblBaseCurrencyTotal.Text = Helper.GetDisplayBaseCurrencyValue(this.CurrentBCCY, BaseCurrencyTotal);
                ExLabel lblReportingCurrencyTotal = (ExLabel)e.Item.FindControl("lblReportingCurrencyTotal");
                lblReportingCurrencyTotal.Text = Helper.GetDisplayDecimalValue(ReportingCurrencyTotal);

            }
            if (GridItemDataBound != null)
                GridItemDataBound(sender, e);

        }
        protected void rgGLDataWriteOnOffItems_ItemCommand(object source, GridCommandEventArgs e)
        {
            // Raise Event for Page to Handle it
            if (GridCommand != null)
            {
                GridCommand(source, e);
            }
            if (e.CommandName == TelerikConstants.GridClearFilterCommandName)
            {
                SessionHelper.ClearDynamicFilterData(GetGridClientIDKey(rgGLDataWriteOnOffItems));
                ApplyFilterGLDataWriteOnOffsGrid();
            }
            if (e.CommandName == TelerikConstants.GridExportToPDFCommandName)
            {
                isExportPDF = true;
                rgGLDataWriteOnOffItems.MasterTableView.Columns.FindByUniqueName("CheckboxSelectColumn").Display = false;
                GridColumn oGridDeleteColumn = null;
                oGridDeleteColumn = rgGLDataWriteOnOffItems.MasterTableView.Columns.FindByUniqueNameSafe("DeleteColumn");
                if (oGridDeleteColumn != null)
                {
                    oGridDeleteColumn.Visible = false;
                }
                GridColumn oGridShowInputFormColumn = null;
                oGridShowInputFormColumn = rgGLDataWriteOnOffItems.MasterTableView.Columns.FindByUniqueNameSafe("ShowInputForm");
                if (oGridShowInputFormColumn != null)
                {
                    oGridShowInputFormColumn.Visible = false;
                }
                GridHelper.ExportGridToPDF(rgGLDataWriteOnOffItems, this.BasePageTitleLabelID);
            }
            if (e.CommandName == TelerikConstants.GridExportToExcelCommandName)
            {
                isExportExcel = true;
                GridColumn oGridDeleteColumn = null;
                oGridDeleteColumn = rgGLDataWriteOnOffItems.MasterTableView.Columns.FindByUniqueNameSafe("DeleteColumn");
                if (oGridDeleteColumn != null)
                {
                    oGridDeleteColumn.Visible = false;
                }
                GridColumn oGridShowInputFormColumn = null;
                oGridShowInputFormColumn = rgGLDataWriteOnOffItems.MasterTableView.Columns.FindByUniqueNameSafe("ShowInputForm");
                if (oGridShowInputFormColumn != null)
                {
                    oGridShowInputFormColumn.Visible = false;
                }
                rgGLDataWriteOnOffItems.MasterTableView.Columns.FindByUniqueName("CheckboxSelectColumn").Display = false;
                GridHelper.ExportGridToExcel(rgGLDataWriteOnOffItems, this.BasePageTitleLabelID);
            }
        }
        protected void rgGLDataWriteOnOffItems_PdfExporting(object source, GridPdfExportingArgs e)
        {
            ExportHelper.GeneratePDFAndRender(ExportHelper.RemoveInvalidFileNameChars(LanguageUtil.GetValue(this.BasePageTitleLabelID)),
            ExportHelper.RemoveInvalidFileNameChars(LanguageUtil.GetValue(this.BasePageTitleLabelID)), e.RawHTML, false, false);
        }
        protected void rgGLDataWriteOnOffItems_ItemCreated(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridPagerItem)
            {
                GridPagerItem gridPager = e.Item as GridPagerItem;
                DropDownList oRadComboBox = (DropDownList)gridPager.FindControl("ddlPageSize");
                if (rgGLDataWriteOnOffItems.AllowCustomPaging)
                {
                    GridHelper.BindPageSizeGrid(oRadComboBox);
                    if (Session[GetGridClientIDKey(rgGLDataWriteOnOffItems) + "NewPageSize"] != null)
                        oRadComboBox.SelectedValue = Session[GetGridClientIDKey(rgGLDataWriteOnOffItems) + "NewPageSize"].ToString();
                    oRadComboBox.Attributes.Add("onChange", "return ddlPageSize_SelectedIndexChanged('" + oRadComboBox.ClientID + "' , '" + rgGLDataWriteOnOffItems.ClientID + "');");
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
            GridHelper.RegisterPDFAndExcelForPostback(e, isExportPDF, isExportExcel, this.Page);
            GridHelper.SetStylesForExportGrid(e, isExportPDF, isExportExcel);
        }
        protected void rgGLDataWriteOnOffItems_PageSizeChanged(object source, GridPageSizeChangedEventArgs e)
        {
            if (!(isExportPDF || isExportExcel))
                Session[GetGridClientIDKey(rgGLDataWriteOnOffItems) + "NewPageSize"] = e.NewPageSize.ToString();
        }
        protected void rgGLDataWriteOnOffItems_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            if (_AllowSelectionPersist)
                SaveCheckBoxStates();
            BindrgGLDataWriteOnOffItemsGrid(e.NewPageIndex);
            ClearTotals();
        }
        #endregion
        #region GLDataWriteOnOffs Grid Paging Events

        /// <summary>
        /// get Checked Items SessionKey
        /// </summary>   
        private string getCheckedItemsSessionKey()
        {
            return SessionConstants.CHECKED_ITEMS + rgGLDataWriteOnOffItems.ClientID;
        }
        /// <summary>
        /// Save Check Box States
        /// </summary>  
        private void SaveCheckBoxStates()
        {
            List<long> oSelectedGLDataWriteOnOffIDList;
            if (Session[getCheckedItemsSessionKey()] != null)
                oSelectedGLDataWriteOnOffIDList = (List<long>)Session[getCheckedItemsSessionKey()];
            else
                oSelectedGLDataWriteOnOffIDList = new List<long>();
            long GLDataWriteOnOffID;
            foreach (GridDataItem item in rgGLDataWriteOnOffItems.Items)
            {
                GLDataWriteOnOffID = Convert.ToInt64(item.GetDataKeyValue("GLDataWriteOnOffID"));
                if (item.Selected == true)
                {
                    if (oSelectedGLDataWriteOnOffIDList != null && !oSelectedGLDataWriteOnOffIDList.Contains(GLDataWriteOnOffID))
                        oSelectedGLDataWriteOnOffIDList.Add(GLDataWriteOnOffID);
                }
                else
                {
                    if (oSelectedGLDataWriteOnOffIDList != null && oSelectedGLDataWriteOnOffIDList.Contains(GLDataWriteOnOffID))
                        oSelectedGLDataWriteOnOffIDList.Remove(GLDataWriteOnOffID);
                }

            }
            if (oSelectedGLDataWriteOnOffIDList != null && oSelectedGLDataWriteOnOffIDList.Count > 0)
            {
                Session[getCheckedItemsSessionKey()] = oSelectedGLDataWriteOnOffIDList;
            }

        }
        /// <summary>
        /// RePopulate CheckBox States
        /// </summary> 
        private void RePopulateCheckBoxStates()
        {

            List<long> oSelectedGLDataWriteOnOffIDList = null;
            if (Session[getCheckedItemsSessionKey()] != null)
                oSelectedGLDataWriteOnOffIDList = (List<long>)Session[getCheckedItemsSessionKey()];
            if (oSelectedGLDataWriteOnOffIDList != null && oSelectedGLDataWriteOnOffIDList.Count > 0)
            {
                long GLDataWriteOnOffID;
                foreach (GridDataItem item in rgGLDataWriteOnOffItems.Items)
                {
                    GLDataWriteOnOffID = Convert.ToInt64(item.GetDataKeyValue("GLDataWriteOnOffID"));
                    if (oSelectedGLDataWriteOnOffIDList != null && oSelectedGLDataWriteOnOffIDList.Contains(GLDataWriteOnOffID))
                    {
                        item.Selected = true;
                    }
                }
            }
        }
        private void ClearCheckedItemViewState()
        {
            Session[getCheckedItemsSessionKey()] = null;
        }
        #endregion
    }
}