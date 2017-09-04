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
using SkyStem.ART.Shared.Utility;

namespace SkyStem.ART.Web.UserControls
{

    public partial class UserControls_GLDataRecurringScheduleItemsGrid : UserControlRecItemBase
    {
        const int POPUP_WIDTH = 630;
        const int POPUP_HEIGHT = 480;

        bool isExportPDF;
        bool isExportExcel;
        private int _BasePageTitleLabelID = 0;
        const int GRID_COLUMN_INDEX_SELECT = 0;
        //public delegate GridItemEventHandler 
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
                return rgGLDataRecurringScheduleItems;
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
                rgGLDataRecurringScheduleItems.AllowExportToExcel = _AllowExportToExcel;
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
                rgGLDataRecurringScheduleItems.AllowExportToPDF = _AllowExportToPDF;
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
                rgGLDataRecurringScheduleItems.AllowCustomFilter = _AllowCustomFilter;
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
                rgGLDataRecurringScheduleItems.AllowCustomPaging = _AllowCustomPaging;
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
                    rgGLDataRecurringScheduleItems.ClientSettings.ClientEvents.OnRowDeselected = "rgGLDataRecurringScheduleItemselected";
                    rgGLDataRecurringScheduleItems.ClientSettings.ClientEvents.OnRowSelected = "rgGLDataRecurringScheduleItemselected";
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
                return rgGLDataRecurringScheduleItems.MasterTableView.Columns;
            }
        }
        [PersistenceMode(PersistenceMode.InnerProperty)]
        public GridClientSettings ClientSettings
        {
            get
            {
                return rgGLDataRecurringScheduleItems.ClientSettings;
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
        /// Show Docs Column
        /// </summary>
        private bool _ShowDocsColumn = true;
        public bool ShowDocsColumn
        {
            get { return _ShowDocsColumn; }
            set { _ShowDocsColumn = value; }
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
        /// GLDataRecurringScheduleItems Grid ShowFooter
        /// </summary>
        public bool ShowFooter
        {
            get { return rgGLDataRecurringScheduleItems.ShowFooter; }
            set { rgGLDataRecurringScheduleItems.ShowFooter = value; }
        }
        /// <summary>
        /// GLDataRecurringScheduleItems Grid Data Table
        /// </summary>
        private DataTable GLDataRecurringScheduleItemsGridDataTable
        {
            get
            {
                if (GLDataRecurringScheduleItemsGridFilteredDataTable != null)
                    return GLDataRecurringScheduleItemsGridFilteredDataTable;
                return (DataTable)Session[GetUniqueSessionKey()];
            }
            set { Session[GetUniqueSessionKey()] = value; }
        }
        /// <summary>
        /// GLAdjustment Grid Filtered Data Table
        /// </summary>
        private DataTable GLDataRecurringScheduleItemsGridFilteredDataTable
        {
            get { return (DataTable)Session[GetFilteredDataUniqueSessionKey()]; }
            set { Session[GetFilteredDataUniqueSessionKey()] = value; }
        }
        protected override void OnInit(EventArgs e)
        {
            Helper.SetGridImageButtonProperties(this.rgGLDataRecurringScheduleItems.MasterTableView.Columns);
            string url;
            url = Page.ResolveClientUrl("~/Pages/GridDynamicFilter.aspx");
            if (_IsOnPage)
                url = url + "?griddynamicfiltersessionkey=" + rgGLDataRecurringScheduleItems.ClientID;
            else
                url = url + "?griddynamicfiltersessionkey=" + rgGLDataRecurringScheduleItems.ClientID + "&IsForPopup=1";
            rgGLDataRecurringScheduleItems.GridApplyFilterOnClientClick = string.Format("ucLoadGridApplyFilterPage('{0}');return false;", url);
            base.OnInit(e);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                isExportPDF = false;
                isExportExcel = false;
                Session[rgGLDataRecurringScheduleItems.ClientID + "NewPageSize"] = "10";
            }
            GetGridApplyFilterScript();
            if (_AllowDisplayCurrencyPnl)
                GetGridSelectedScript();
            if (_ShowCloseDateColum)
                rgGLDataRecurringScheduleItems.EntityNameLabelID = 1873;
            else
                rgGLDataRecurringScheduleItems.EntityNameLabelID = 1872;
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
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), rgGLDataRecurringScheduleItems.ClientID + "Testing", osb.ToString(), true);
        }
        /// <summary>
        /// Get Grid Selected Script
        /// </summary>
        private void GetGridSelectedScript()
        {
            StringBuilder osb = new StringBuilder();
            osb.Append(@"function rgGLDataRecurringScheduleItemselected(sender, args) {");
            osb.Append(@"var baseCurrencyValue = 0;");
            osb.Append(@"var reportingCurrencyValue = 0;");
            osb.Append(@" var masterTable = sender.get_masterTableView();");
            osb.Append(@" var selectedRows = masterTable.get_selectedItems();");
            osb.Append(@" for (var i = 0; i < selectedRows.length; i++) {");
            osb.Append(@" var row = selectedRows[i];");
            if (!String.IsNullOrEmpty(this.CurrentBCCY))
                osb.Append(@"  baseCurrencyValue = baseCurrencyValue + Round(row.getDataKeyValue('ScheduleAmountBaseCurrency'), 2);");
            osb.Append(@" reportingCurrencyValue = reportingCurrencyValue + Round(row.getDataKeyValue('ScheduleAmountReportingCurrency'), 2);");
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
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), rgGLDataRecurringScheduleItems.ClientID + "GridSelectedScript", osb.ToString(), true);
        }
        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (_AllowSelectionPersist)
                RePopulateCheckBoxStates();
        }
        /// <summary>
        /// Bind GLDataRecurringScheduleItems Grid
        /// </summary>
        private void BindrgGLDataRecurringScheduleItemsGrid(int PageIndex)
        {
            if (GLDataRecurringScheduleItemsGridDataTable != null)
            {
                rgGLDataRecurringScheduleItems.DataSource = GLDataRecurringScheduleItemsGridDataTable;
                rgGLDataRecurringScheduleItems.VirtualItemCount = GLDataRecurringScheduleItemsGridDataTable.Rows.Count;
                rgGLDataRecurringScheduleItems.DataBind();
                MangeColumnsForExport(false);
            }
        }
        /// <summary>
        /// Bind Load Grid Data 
        /// </summary>
        public void LoadGridData()
        {
            BindrgGLDataRecurringScheduleItemsGrid(0);
        }
        /// <summary>
        /// Selected GLDataRecurringItemScheduleIDList  List
        /// </summary>
        public List<long> SelectedGLDataRecurringItemScheduleIDs()
        {
            List<long> oSelectedGLDataRecurringItemScheduleIDList = null;
            if (_AllowSelectionPersist)
            {
                SaveCheckBoxStates();
                if (Session[getCheckedItemsSessionKey()] != null)
                    oSelectedGLDataRecurringItemScheduleIDList = (List<long>)Session[getCheckedItemsSessionKey()];
            }
            else
                oSelectedGLDataRecurringItemScheduleIDList = GridSelectedItems();
            return oSelectedGLDataRecurringItemScheduleIDList;
        }
        /// <summary>
        /// Selected GLDataRecurringItemScheduleInfo Collection
        /// </summary>
        /// 
        public List<GLDataRecurringItemScheduleInfo> SelectedGLDataRecurringItemScheduleInfoCollection()
        {
            List<GLDataRecurringItemScheduleInfo> oGLDataRecurringItemScheduleInfoCollection = null;
            List<GLDataRecurringItemScheduleInfo> oAllGLDataRecurringItemScheduleInfoCollection = (List<GLDataRecurringItemScheduleInfo>)Session[GetUniqueSessionKey() + "List"];
            List<long> oSelectedGLDataRecurringItemScheduleIDList = SelectedGLDataRecurringItemScheduleIDs();
            if (oAllGLDataRecurringItemScheduleInfoCollection != null && oSelectedGLDataRecurringItemScheduleIDList != null)
                oGLDataRecurringItemScheduleInfoCollection = (from oGLDataRecurringItemScheduleInfo in oAllGLDataRecurringItemScheduleInfoCollection
                                                              join SelectedGLDataRecurringItemScheduleID in oSelectedGLDataRecurringItemScheduleIDList on oGLDataRecurringItemScheduleInfo.GLDataRecurringItemScheduleID equals SelectedGLDataRecurringItemScheduleID
                                                              select oGLDataRecurringItemScheduleInfo).ToList();
            return oGLDataRecurringItemScheduleInfoCollection;
        }
        /// <summary>
        /// Selected GLDataRecurringItemScheduleID on Current Page
        /// </summary>
        /// 
        private List<long> GridSelectedItems()
        {
            List<long> oSelectedGLDataRecurringItemScheduleIDList = null;
            oSelectedGLDataRecurringItemScheduleIDList = new List<long>();
            long GLDataRecurringItemScheduleID;
            foreach (GridDataItem item in rgGLDataRecurringScheduleItems.SelectedItems)
            {

                GLDataRecurringItemScheduleID = Convert.ToInt64(item.GetDataKeyValue("GLDataRecurringItemScheduleID"));
                oSelectedGLDataRecurringItemScheduleIDList.Add(GLDataRecurringItemScheduleID);
            }
            return oSelectedGLDataRecurringItemScheduleIDList;
        }
        /// <summary>
        /// Bind Load Grid Data 
        /// </summary>
        private string GetGridClientIDKey(ExRadGrid Rg)
        {
            return Rg.ClientID;
        }
        /// <summary>
        /// Create FilterInfo For GLDataRecurringScheduleItems Grid
        /// </summary>
        private void CreateFilterInfoForGLDataRecurringScheduleItemsGrid(bool IsAmortizable)
        {
            List<FilterInfo> oFilterInfoList = new List<FilterInfo>();
            FilterInfo oFilterInfo = null;

            oFilterInfo = new FilterInfo();
            oFilterInfo.ColumnID = 1;
            oFilterInfo.ColumnName = "ScheduleAmount";
            oFilterInfo.DisplayColumnName = Helper.GetLabelIDValue(1700) + " (" + Helper.GetLabelIDValue(1409) + ")";
            oFilterInfo.DataType = (short)WebEnums.DataType.Decimal;
            oFilterInfoList.Add(oFilterInfo);

            oFilterInfo = new FilterInfo();
            oFilterInfo.ColumnID = 2;
            oFilterInfo.ColumnName = "ScheduleAmountReportingCurrency";
            oFilterInfo.DisplayColumnName = Helper.GetLabelIDValue(1700) + " (" + SessionHelper.ReportingCurrencyCode + ")";
            oFilterInfo.DataType = (short)WebEnums.DataType.Decimal;
            oFilterInfoList.Add(oFilterInfo);

            oFilterInfo = new FilterInfo();
            oFilterInfo.ColumnID = 3;
            oFilterInfo.ColumnName = "RecPeriodAmountReportingCurrency";
            if (IsAmortizable)
                oFilterInfo.DisplayColumnName = Helper.GetLabelIDValue(2054) + " (" + SessionHelper.ReportingCurrencyCode + ")";
            else
                oFilterInfo.DisplayColumnName = Helper.GetLabelIDValue(2058) + " (" + SessionHelper.ReportingCurrencyCode + ")";

            oFilterInfo.DataType = (short)WebEnums.DataType.Decimal;
            oFilterInfoList.Add(oFilterInfo);

            oFilterInfo = new FilterInfo();
            oFilterInfo.ColumnID = 4;
            oFilterInfo.ColumnName = "BalanceReportingCurrency";
            if (IsAmortizable)
                oFilterInfo.DisplayColumnName = Helper.GetLabelIDValue(2055) + " (" + SessionHelper.ReportingCurrencyCode + ")";
            else
                oFilterInfo.DisplayColumnName = Helper.GetLabelIDValue(2061) + " (" + SessionHelper.ReportingCurrencyCode + ")";
            oFilterInfo.DataType = (short)WebEnums.DataType.Decimal;
            oFilterInfoList.Add(oFilterInfo);

            if (_ShowCloseDateColum)
            {
                oFilterInfo = new FilterInfo();
                oFilterInfo.ColumnID = 10;
                oFilterInfo.ColumnName = "CloseDate";
                oFilterInfo.DisplayColumnName = Helper.GetLabelIDValue(1411);
                oFilterInfo.DataType = (short)WebEnums.DataType.DataTime;
                oFilterInfoList.Add(oFilterInfo);
            }

            oFilterInfo = new FilterInfo();
            oFilterInfo.ColumnID = 5;
            oFilterInfo.ColumnName = "ScheduleBeginDate";
            oFilterInfo.DisplayColumnName = Helper.GetLabelIDValue(2053);
            oFilterInfo.DataType = (short)WebEnums.DataType.DataTime;
            oFilterInfoList.Add(oFilterInfo);

            oFilterInfo = new FilterInfo();
            oFilterInfo.ColumnID = 6;
            oFilterInfo.ColumnName = "ScheduleEndDate";
            oFilterInfo.DisplayColumnName = Helper.GetLabelIDValue(1450);
            oFilterInfo.DataType = (short)WebEnums.DataType.DataTime;
            oFilterInfoList.Add(oFilterInfo);

            oFilterInfo = new FilterInfo();
            oFilterInfo.ColumnID = 7;
            oFilterInfo.ColumnName = "AttachmentCount";
            oFilterInfo.DisplayColumnName = Helper.GetLabelIDValue(2056);
            oFilterInfo.DataType = (short)WebEnums.DataType.Integer;
            oFilterInfoList.Add(oFilterInfo);

            oFilterInfo = new FilterInfo();
            oFilterInfo.ColumnID = 8;
            oFilterInfo.ColumnName = "RecItemNumber";
            oFilterInfo.DisplayColumnName = Helper.GetLabelIDValue(2118);
            oFilterInfo.DataType = (short)WebEnums.DataType.String;
            oFilterInfoList.Add(oFilterInfo);

            oFilterInfo = new FilterInfo();
            oFilterInfo.ColumnID = 9;
            oFilterInfo.ColumnName = "ScheduleName";
            oFilterInfo.DisplayColumnName = Helper.GetLabelIDValue(2052);
            oFilterInfo.DataType = (short)WebEnums.DataType.String;
            oFilterInfoList.Add(oFilterInfo);


            SessionHelper.SetDynamicFilterColumns(oFilterInfoList, GetGridClientIDKey(rgGLDataRecurringScheduleItems));

        }
        private string GetUniqueSessionKey()
        {
            return GetGridClientIDKey(rgGLDataRecurringScheduleItems) + SessionConstants.RECURRING_SCHEDULE_ITEMS_GRID_DATA;
        }
        private string GetFilteredDataUniqueSessionKey()
        {
            return GetGridClientIDKey(rgGLDataRecurringScheduleItems) + SessionConstants.RECURRING_SCHEDULE_ITEMS_GRID_DATA_FILTERED;
        }
        /// <summary>
        /// Apply Filter GLDataRecurringScheduleItems Grid
        /// </summary>
        public void ApplyFilterGLDataRecurringScheduleItemsGrid()
        {
            ClearCheckedItemViewState();
            ClearTotals();
            string whereClause = SessionHelper.GetDynamicFilterResultWhereClause(GetGridClientIDKey(rgGLDataRecurringScheduleItems));
            GLDataRecurringScheduleItemsGridFilteredDataTable = null;
            if (!string.IsNullOrEmpty(whereClause) && GLDataRecurringScheduleItemsGridDataTable != null && GLDataRecurringScheduleItemsGridDataTable.Rows.Count > 0)
            {
                DataRow[] filterResult = GLDataRecurringScheduleItemsGridDataTable.Select(whereClause);
                if (filterResult != null && filterResult.Length > 0)
                    GLDataRecurringScheduleItemsGridFilteredDataTable = filterResult.CopyToDataTable();
                else
                    GLDataRecurringScheduleItemsGridFilteredDataTable = GLDataRecurringScheduleItemsGridDataTable.Clone();
            }
            BindrgGLDataRecurringScheduleItemsGrid(0);
        }
        /// <summary>
        /// Set GLDataRecurringItemScheduleItem Grid Data
        /// </summary>
        /// 
        public void SetGLDataRecurringItemScheduleItemGridData(List<GLDataRecurringItemScheduleInfo> oGLDataRecurringItemScheduleInfoCollection)
        {
            ClearGLDataRecurringItemScheduleItemGridData();
            if (oGLDataRecurringItemScheduleInfoCollection != null)
            {

                Session[GetUniqueSessionKey() + "List"] = oGLDataRecurringItemScheduleInfoCollection;
                GLDataRecurringScheduleItemsGridDataTable = MatchingHelper.ListToDataTable(oGLDataRecurringItemScheduleInfoCollection);
                ClearCheckedItemViewState();
            }
        }
        /// <summary>
        /// Clear GLDataRecurringItemScheduleItem Grid Data
        /// </summary>
        /// 
        private void ClearGLDataRecurringItemScheduleItemGridData()
        {
            GLDataRecurringScheduleItemsGridDataTable = null;
            GLDataRecurringScheduleItemsGridFilteredDataTable = null;
            Session[GetUniqueSessionKey() + "List"] = null;
        }
        #region GLDataRecurringScheduleItems Grid Events
        protected void rgGLDataRecurringScheduleItems_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {

            rgGLDataRecurringScheduleItems.DataSource = GLDataRecurringScheduleItemsGridDataTable;
            if (GLDataRecurringScheduleItemsGridDataTable != null)
                rgGLDataRecurringScheduleItems.VirtualItemCount = GLDataRecurringScheduleItemsGridDataTable.Rows.Count;

        }
        private decimal? _TotalOriginalAmountRCCY;
        private decimal? _TotalRecPeriodAmountRCCY;
        private decimal? _TotalBalanceRCCY;
        int? refNo = null;
        private void ClearTotals()
        {
            _TotalOriginalAmountRCCY = null;
            _TotalRecPeriodAmountRCCY = null;
            _TotalBalanceRCCY = null;
        }
        protected void rgGLDataRecurringScheduleItems_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item.ItemType == GridItemType.Header)
            {
                _TotalOriginalAmountRCCY = 0.00M;
                _TotalRecPeriodAmountRCCY = 0.00M;
                _TotalBalanceRCCY = 0.00M;
                rgGLDataRecurringScheduleItems.MasterTableView.Columns.FindByUniqueName("CloseDate").Visible = _ShowCloseDateColum;
                rgGLDataRecurringScheduleItems.MasterTableView.Columns.FindByUniqueName("AttachmentCount").Visible = _ShowDocsColumn;
                rgGLDataRecurringScheduleItems.MasterTableView.Columns.FindByUniqueName("CheckboxSelectColumn").Visible = this.selectOption;
            }
            if (e.Item.ItemType == Telerik.Web.UI.GridItemType.Item || e.Item.ItemType == Telerik.Web.UI.GridItemType.AlternatingItem)
            {
                GridDataItem oGridDataItem = e.Item as GridDataItem;
                DataRow dr = ((DataRowView)oGridDataItem.DataItem).Row;
                int CreatedInRecPeriodID;
                int.TryParse(dr["CreatedInRecPeriodID"].ToString(), out CreatedInRecPeriodID);
                bool IsForwardedItem = !(CreatedInRecPeriodID == SessionHelper.CurrentReconciliationPeriodID);

                //Original Amount LCCY
                ExLabel lblScheduleAmountLCCY = (ExLabel)e.Item.FindControl("lblScheduleAmountLCCY");
                lblScheduleAmountLCCY.Text = Helper.GetCurrencyValue(dr.GetDecimalValue("ScheduleAmount"), dr.GetStringValue("LocalCurrencyCode"));
                //Open Date
                ExLabel lblOpenDate = (ExLabel)e.Item.FindControl("lblOpenDate");
                lblOpenDate.Text = Helper.GetDisplayDate(dr.GetDateValue("OpenDate"));
                //Schedule Name
                ExLabel lblScheduleName = (ExLabel)e.Item.FindControl("lblScheduleName");
                lblScheduleName.Text = Helper.GetDisplayStringValue(dr.GetStringValue("ScheduleName"));
                //Schedule Begin Date
                ExLabel lblScheduleBeginDate = (ExLabel)e.Item.FindControl("lblScheduleBeginDate");
                lblScheduleBeginDate.Text = Helper.GetDisplayDate(dr.GetDateValue("ScheduleBeginDate"));
                //Schedule End Date
                ExLabel lblScheduleEndDate = (ExLabel)e.Item.FindControl("lblScheduleEndDate");
                lblScheduleEndDate.Text = Helper.GetDisplayDate(dr.GetDateValue("ScheduleEndDate"));
                if (_ShowCloseDateColum)
                {
                    //CloseDate
                    ExLabel lblCloseDate = (ExLabel)e.Item.FindControl("lblCloseDate");
                    lblCloseDate.Text = Helper.GetDisplayDate(dr.GetDateValue("CloseDate"));
                }
                //Original Amount RCCY
                ExLabel lblOriginalAmountRCCY = (ExLabel)e.Item.FindControl("lblOriginalAmountRCCY");
                lblOriginalAmountRCCY.Text = Helper.GetDisplayDecimalValue(dr.GetDecimalValue("ScheduleAmountReportingCurrency"));
                if (dr.GetDecimalValue("ScheduleAmountReportingCurrency") != null)
                    this._TotalOriginalAmountRCCY = this._TotalOriginalAmountRCCY + dr.GetDecimalValue("ScheduleAmountReportingCurrency");
                //Total Amortized Amount 
                ExLabel lblConsumedAmountRCCY = (ExLabel)e.Item.FindControl("lblConsumedAmountRCCY");
                lblConsumedAmountRCCY.Text = Helper.GetDisplayDecimalValue(dr.GetDecimalValue("RecPeriodAmountReportingCurrency"));
                if (dr.GetDecimalValue("RecPeriodAmountReportingCurrency") != null)
                    this._TotalRecPeriodAmountRCCY = this._TotalRecPeriodAmountRCCY + dr.GetDecimalValue("RecPeriodAmountReportingCurrency");
                //Remaining Amortizable Amount RCCY

                //CrrentRecPeriodAmount 
                ExLabel lblCrrentRecPeriodAmountRCCY = (ExLabel)e.Item.FindControl("lblCrrentRecPeriodAmountRCCY");

                decimal? currentRCCY = null;
                if (dr.GetDecimalValue("ExRateLCCYtoRCCY") != null)
                    currentRCCY = SharedRecItemHelper.ConvertAmount(dr.GetDecimalValue("CrrentRecPeriodAmount"), dr.GetDecimalValue("ExRateLCCYtoRCCY"));
                else
                    currentRCCY = RecHelper.ConvertCurrency(dr.GetStringValue("LocalCurrencyCode"), SessionHelper.ReportingCurrencyCode, SessionHelper.CurrentReconciliationPeriodID, dr.GetDecimalValue("CrrentRecPeriodAmount"));
                lblCrrentRecPeriodAmountRCCY.Text = Helper.GetDisplayDecimalValue(currentRCCY);

                ExLabel lblBalanceRCCY = (ExLabel)e.Item.FindControl("lblBalanceRCCY");
                ExHyperLink hlIgnoreInCalculationImg = (ExHyperLink)e.Item.FindControl("hlIgnoreInCalculationImg");
                if (dr.GetDecimalValue("IgnoreInCalculation") != null && Convert.ToBoolean(dr.GetDecimalValue("IgnoreInCalculation")) == true)
                {
                    lblBalanceRCCY.Text = Helper.GetDisplayDecimalValue(0);
                    //e.Item.CssClass = "IgnoreInCalculation";                  
                    lblBalanceRCCY.CssClass = "Red11Arial";
                    lblBalanceRCCY.ToolTipLabelID = 2744;
                }
                else
                {
                    lblBalanceRCCY.Text = Helper.GetDisplayDecimalValue(dr.GetDecimalValue("BalanceReportingCurrency"));
                    if (dr.GetDecimalValue("BalanceReportingCurrency") != null)
                        this._TotalBalanceRCCY = this._TotalBalanceRCCY + Convert.ToDecimal(dr.GetDecimalValue("BalanceReportingCurrency"));
                }

                //Docs
                if (_ShowDocsColumn)
                {
                    ExHyperLink hlAttachmentCount = (ExHyperLink)e.Item.FindControl("hlAttachmentCount");
                    hlAttachmentCount.Text = Helper.GetDisplayIntegerValue(dr.GetInt32Value("AttachmentCount"));
                    string windowName;
                    hlAttachmentCount.NavigateUrl = "javascript:OpenRadWindowForHyperlink('" + Helper.SetDocumentUploadURLForRecItemInput(this.GLDataID, dr.GetInt32Value("GLDataRecurringItemScheduleID"), this.AccountID, this.NetAccountID, true, Request.Url.ToString(), out windowName, IsForwardedItem, WebEnums.RecordType.ScheduleRecItem) + "', " + POPUP_HEIGHT + " , " + POPUP_WIDTH + ");";
                }

                ExLabel lblRecItemNumber = (ExLabel)e.Item.FindControl("lblRecItemNumber");
                lblRecItemNumber.Text = Helper.GetDisplayStringValue(dr.GetStringValue("RecItemNumber"));
                ExLabel lblRefNo = (ExLabel)e.Item.FindControl("lblRefNo");
                if (refNo.HasValue)
                {
                    refNo += 1;
                    lblRefNo.Text = refNo.Value.ToString();
                }
                else
                {
                    refNo = 1;
                    lblRefNo.Text = refNo.Value.ToString();
                }

                ExLabel lblAmountImport = (ExLabel)e.Item.FindControl("lblAmountImport");
                lblAmountImport.Text = Helper.GetDisplayDecimalValue(Convert.ToDecimal(dr["ScheduleAmount"]));

                ExLabel lblLocalCurrencyCode = (ExLabel)e.Item.FindControl("lblLocalCurrencyCode");
                lblLocalCurrencyCode.Text = Helper.GetDisplayStringValue(Convert.ToString(dr["LocalCurrencyCode"]));
                ExLabel lblDescription = (ExLabel)e.Item.FindControl("lblDescription");
                lblDescription.Text = Helper.GetDisplayStringValue(dr.GetStringValue("Comments"));

                ExLabel lblRecItemComments = (ExLabel)e.Item.FindControl("lblRecItemComments");
                if (lblRecItemComments != null)
                {
                    long GLDataRecurringItemScheduleID = Convert.ToInt64(dr["GLDataRecurringItemScheduleID"]);
                    IGLDataRecItem oGLDataRecItemClient = RemotingHelper.GetGLDataRecItemObject();
                    List<RecItemCommentInfo> RecItemCommentInfoList;
                    RecItemCommentInfoList = oGLDataRecItemClient.GetRecItemCommentList(GLDataRecurringItemScheduleID, 3, Helper.GetAppUserInfo());
                    lblRecItemComments.Text = Helper.GetRecItemComments(RecItemCommentInfoList);
                }

            }
            if (e.Item.ItemType == GridItemType.Footer)
            {
                //Original Amount RCCY
                ExLabel lblOriginalAmountRCCYTotalValue = (ExLabel)e.Item.FindControl("lblOriginalAmountRCCYTotalValue");
                lblOriginalAmountRCCYTotalValue.Text = Helper.GetDisplayDecimalValue(this._TotalOriginalAmountRCCY);
                //Total Amortized Amount
                ExLabel lblConsumedAmountRCCYTotalValue = (ExLabel)e.Item.FindControl("lblConsumedAmountRCCYTotalValue");
                lblConsumedAmountRCCYTotalValue.Text = Helper.GetDisplayDecimalValue(this._TotalRecPeriodAmountRCCY);
                //Remaining Amortizable Amount RCCY
                ExLabel lblBalanceRCCYTotalValue = (ExLabel)e.Item.FindControl("lblBalanceRCCYTotalValue");
                lblBalanceRCCYTotalValue.Text = Helper.GetDisplayDecimalValue(this._TotalBalanceRCCY);
            }
            // Raise Event for Page to Handle it 
            if (GridItemDataBound != null)
                GridItemDataBound(sender, e);
        }
        public void SetAmortizableGridHeaders(Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item.ItemType == GridItemType.Header)
            {
                List<FilterInfo> oFilterInfoCollection = SessionHelper.GetDynamicFilterColumns(GetGridClientIDKey(rgGLDataRecurringScheduleItems));
                if (oFilterInfoCollection == null)
                    CreateFilterInfoForGLDataRecurringScheduleItemsGrid(true);

                Control oControlRecPeriodAmountReportingCurrency = new Control();
                Control oControlBalanceReportingCurrency = new Control();
                Control oControlOriginalAmountRCCY = new Control();
                Control oControlOriginalAmountLCCY = new Control();
                Control oControlCrrentRecPeriodAmountLCCY = new Control();
                GridHeaderItem headerItem = e.Item as GridHeaderItem;
                oControlRecPeriodAmountReportingCurrency = (headerItem)["RecPeriodAmountReportingCurrency"].Controls[0];
                oControlBalanceReportingCurrency = (headerItem)["BalanceReportingCurrency"].Controls[0];
                oControlOriginalAmountRCCY = (headerItem)["ScheduleAmountReportingCurrency"].Controls[0];
                oControlOriginalAmountLCCY = (headerItem)["ScheduleAmount"].Controls[0];
                oControlCrrentRecPeriodAmountLCCY = (headerItem)["CrrentRecPeriodAmountReportingCurrency"].Controls[0];
                if (oControlRecPeriodAmountReportingCurrency is LinkButton)
                {
                    ((LinkButton)oControlRecPeriodAmountReportingCurrency).Text = Helper.GetLabelIDValue(2054) + " (" + SessionHelper.ReportingCurrencyCode + ")";
                }
                else
                {
                    if (oControlRecPeriodAmountReportingCurrency is LiteralControl)
                    {
                        ((LiteralControl)oControlRecPeriodAmountReportingCurrency).Text = Helper.GetLabelIDValue(2054) + " (" + SessionHelper.ReportingCurrencyCode + ")";
                    }
                }
                if (oControlBalanceReportingCurrency is LinkButton)
                {
                    ((LinkButton)oControlBalanceReportingCurrency).Text = Helper.GetLabelIDValue(2055) + " (" + SessionHelper.ReportingCurrencyCode + ")";
                }
                else
                {
                    if (oControlBalanceReportingCurrency is LiteralControl)
                    {
                        ((LiteralControl)oControlBalanceReportingCurrency).Text = Helper.GetLabelIDValue(2055) + " (" + SessionHelper.ReportingCurrencyCode + ")";
                    }
                }
                if (oControlOriginalAmountRCCY is LinkButton)
                    ((LinkButton)oControlOriginalAmountRCCY).Text = Helper.GetLabelIDValue(1700) + " (" + SessionHelper.ReportingCurrencyCode + ")";
                else
                {
                    if (oControlOriginalAmountRCCY is LiteralControl)
                        ((LiteralControl)oControlOriginalAmountRCCY).Text = Helper.GetLabelIDValue(1700) + " (" + SessionHelper.ReportingCurrencyCode + ")";

                }

                if (oControlOriginalAmountLCCY is LinkButton)
                {
                    ((LinkButton)oControlOriginalAmountLCCY).Text = Helper.GetLabelIDValue(1700) + " (" + Helper.GetLabelIDValue(1409) + ")";
                }
                else
                {
                    if (oControlOriginalAmountLCCY is LiteralControl)
                    {
                        ((LiteralControl)oControlOriginalAmountLCCY).Text = Helper.GetLabelIDValue(1700) + " (" + Helper.GetLabelIDValue(1409) + ")";
                    }
                }

                if (oControlCrrentRecPeriodAmountLCCY is LinkButton)
                {
                    ((LinkButton)oControlCrrentRecPeriodAmountLCCY).Text = Helper.GetLabelIDValue(2700) + " (" + SessionHelper.ReportingCurrencyCode + ")";
                }
                else
                {
                    if (oControlCrrentRecPeriodAmountLCCY is LiteralControl)
                    {
                        ((LiteralControl)oControlCrrentRecPeriodAmountLCCY).Text = Helper.GetLabelIDValue(2700) + " (" + SessionHelper.ReportingCurrencyCode + ")";
                    }
                }

                if (!_IsOnPage)
                    //   SessionHelper.ShowGridFilterIcon((PageBase)this.Page, rgGLDataRecurringScheduleItems, e, GetGridClientIDKey(rgGLDataRecurringScheduleItems));
                    //else
                    SessionHelper.ShowGridFilterIcon((PopupPageBase)this.Page, rgGLDataRecurringScheduleItems, e, GetGridClientIDKey(rgGLDataRecurringScheduleItems));
            }
        }
        public void SetAccruableGridHeaders(Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item.ItemType == GridItemType.Header)
            {
                List<FilterInfo> oFilterInfoCollection = SessionHelper.GetDynamicFilterColumns(GetGridClientIDKey(rgGLDataRecurringScheduleItems));
                if (oFilterInfoCollection == null)
                    CreateFilterInfoForGLDataRecurringScheduleItemsGrid(false);
                Control oControlRecPeriodAmountReportingCurrency = new Control();
                Control oControlBalanceReportingCurrency = new Control();
                Control oControlOriginalAmountRCCY = new Control();
                Control oControlOriginalAmountLCCY = new Control();
                Control oControlCrrentRecPeriodAmountLCCY = new Control();
                GridHeaderItem headerItem = e.Item as GridHeaderItem;
                oControlRecPeriodAmountReportingCurrency = (headerItem)["RecPeriodAmountReportingCurrency"].Controls[0];
                oControlBalanceReportingCurrency = (headerItem)["BalanceReportingCurrency"].Controls[0];
                oControlOriginalAmountRCCY = (headerItem)["ScheduleAmountReportingCurrency"].Controls[0];
                oControlOriginalAmountLCCY = (headerItem)["ScheduleAmount"].Controls[0];
                oControlCrrentRecPeriodAmountLCCY = (headerItem)["CrrentRecPeriodAmountReportingCurrency"].Controls[0];
                if (oControlRecPeriodAmountReportingCurrency is LinkButton)
                {
                    ((LinkButton)oControlRecPeriodAmountReportingCurrency).Text = Helper.GetLabelIDValue(2058) + " (" + SessionHelper.ReportingCurrencyCode + ")";
                }
                else
                {
                    if (oControlRecPeriodAmountReportingCurrency is LiteralControl)
                    {
                        ((LiteralControl)oControlRecPeriodAmountReportingCurrency).Text = Helper.GetLabelIDValue(2058) + " (" + SessionHelper.ReportingCurrencyCode + ")";
                    }
                }
                if (oControlBalanceReportingCurrency is LinkButton)
                {
                    ((LinkButton)oControlBalanceReportingCurrency).Text = Helper.GetLabelIDValue(2061) + " (" + SessionHelper.ReportingCurrencyCode + ")";
                }
                else
                {
                    if (oControlBalanceReportingCurrency is LiteralControl)
                    {
                        ((LiteralControl)oControlBalanceReportingCurrency).Text = Helper.GetLabelIDValue(2061) + " (" + SessionHelper.ReportingCurrencyCode + ")";
                    }
                }
                if (oControlOriginalAmountRCCY is LinkButton)
                    ((LinkButton)oControlOriginalAmountRCCY).Text = Helper.GetLabelIDValue(1700) + " (" + SessionHelper.ReportingCurrencyCode + ")";
                else
                {
                    if (oControlOriginalAmountRCCY is LiteralControl)
                        ((LiteralControl)oControlOriginalAmountRCCY).Text = Helper.GetLabelIDValue(1700) + " (" + SessionHelper.ReportingCurrencyCode + ")";
                }
                if (oControlOriginalAmountLCCY is LinkButton)
                {
                    ((LinkButton)oControlOriginalAmountLCCY).Text = Helper.GetLabelIDValue(1700) + " (" + Helper.GetLabelIDValue(1409) + ")";
                }
                else
                {
                    if (oControlOriginalAmountLCCY is LiteralControl)
                    {
                        ((LiteralControl)oControlOriginalAmountLCCY).Text = Helper.GetLabelIDValue(1700) + " (" + Helper.GetLabelIDValue(1409) + ")";
                    }
                }
                if (oControlCrrentRecPeriodAmountLCCY is LinkButton)
                {
                    ((LinkButton)oControlCrrentRecPeriodAmountLCCY).Text = Helper.GetLabelIDValue(2700) + " (" + SessionHelper.ReportingCurrencyCode + ")";
                }
                else
                {
                    if (oControlCrrentRecPeriodAmountLCCY is LiteralControl)
                    {
                        ((LiteralControl)oControlCrrentRecPeriodAmountLCCY).Text = Helper.GetLabelIDValue(2700) + " (" + SessionHelper.ReportingCurrencyCode + ")";
                    }
                }
                if (!_IsOnPage)
                    //    SessionHelper.ShowGridFilterIcon((PageBase)this.Page, rgGLDataRecurringScheduleItems, e, GetGridClientIDKey(rgGLDataRecurringScheduleItems));
                    ////else
                    SessionHelper.ShowGridFilterIcon((PopupPageBase)this.Page, rgGLDataRecurringScheduleItems, e, GetGridClientIDKey(rgGLDataRecurringScheduleItems));
            }
        }
        protected void rgGLDataRecurringScheduleItems_ItemCommand(object source, GridCommandEventArgs e)
        {
            // Raise Event for Page to Handle it
            if (GridCommand != null)
            {
                GridCommand(source, e);
            }
            if (e.CommandName == TelerikConstants.GridClearFilterCommandName)
            {
                SessionHelper.ClearDynamicFilterData(GetGridClientIDKey(rgGLDataRecurringScheduleItems));
                ApplyFilterGLDataRecurringScheduleItemsGrid();
            }
            if (e.CommandName == TelerikConstants.GridExportToPDFCommandName)
            {
                isExportPDF = true;
                rgGLDataRecurringScheduleItems.MasterTableView.Columns.FindByUniqueName("CheckboxSelectColumn").Display = false;
                GridColumn oGridDeleteColumn = null;
                oGridDeleteColumn = rgGLDataRecurringScheduleItems.MasterTableView.Columns.FindByUniqueNameSafe("DeleteColumn");
                if (oGridDeleteColumn != null)
                {
                    oGridDeleteColumn.Visible = false;
                }
                GridColumn oGridShowInputFormColumn = null;
                oGridShowInputFormColumn = rgGLDataRecurringScheduleItems.MasterTableView.Columns.FindByUniqueNameSafe("ShowInputForm");
                if (oGridShowInputFormColumn != null)
                {
                    oGridShowInputFormColumn.Visible = false;
                }
                MangeColumnsForExport(true);
                GridHelper.ExportGridToPDF(rgGLDataRecurringScheduleItems, this.BasePageTitleLabelID);
            }
            if (e.CommandName == TelerikConstants.GridExportToExcelCommandName)
            {
                isExportExcel = true;
                GridColumn oGridDeleteColumn = null;
                oGridDeleteColumn = rgGLDataRecurringScheduleItems.MasterTableView.Columns.FindByUniqueNameSafe("DeleteColumn");
                if (oGridDeleteColumn != null)
                {
                    oGridDeleteColumn.Visible = false;
                }
                GridColumn oGridShowInputFormColumn = null;
                oGridShowInputFormColumn = rgGLDataRecurringScheduleItems.MasterTableView.Columns.FindByUniqueNameSafe("ShowInputForm");
                if (oGridShowInputFormColumn != null)
                {
                    oGridShowInputFormColumn.Visible = false;
                }
                rgGLDataRecurringScheduleItems.MasterTableView.Columns.FindByUniqueName("CheckboxSelectColumn").Display = false;
                MangeColumnsForExport(true);
                GridHelper.ExportGridToExcel(rgGLDataRecurringScheduleItems, this.BasePageTitleLabelID);
            }
        }
        protected void rgGLDataRecurringScheduleItems_PdfExporting(object source, GridPdfExportingArgs e)
        {
            ExportHelper.GeneratePDFAndRender(ExportHelper.RemoveInvalidFileNameChars(LanguageUtil.GetValue(this.BasePageTitleLabelID)),
            ExportHelper.RemoveInvalidFileNameChars(LanguageUtil.GetValue(this.BasePageTitleLabelID)), e.RawHTML, false, false);
        }
        protected void rgGLDataRecurringScheduleItems_ItemCreated(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridPagerItem)
            {
                GridPagerItem gridPager = e.Item as GridPagerItem;
                DropDownList oRadComboBox = (DropDownList)gridPager.FindControl("ddlPageSize");
                if (rgGLDataRecurringScheduleItems.AllowCustomPaging)
                {
                    GridHelper.BindPageSizeGrid(oRadComboBox);
                    if (Session[GetGridClientIDKey(rgGLDataRecurringScheduleItems) + "NewPageSize"] != null)
                        oRadComboBox.SelectedValue = Session[GetGridClientIDKey(rgGLDataRecurringScheduleItems) + "NewPageSize"].ToString();
                    oRadComboBox.Attributes.Add("onChange", "return ddlPageSize_SelectedIndexChanged('" + oRadComboBox.ClientID + "' , '" + rgGLDataRecurringScheduleItems.ClientID + "');");
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
        protected void rgGLDataRecurringScheduleItems_PageSizeChanged(object source, GridPageSizeChangedEventArgs e)
        {
            if (!(isExportPDF || isExportExcel))
                Session[GetGridClientIDKey(rgGLDataRecurringScheduleItems) + "NewPageSize"] = e.NewPageSize.ToString();
        }
        protected void rgGLDataRecurringScheduleItems_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            if (_AllowSelectionPersist)
                SaveCheckBoxStates();
            BindrgGLDataRecurringScheduleItemsGrid(e.NewPageIndex);
            ClearTotals();
        }
        #endregion
        #region GLDataRecurringScheduleItems Grid Paging Events

        /// <summary>
        /// get Checked Items SessionKey
        /// </summary>   
        private string getCheckedItemsSessionKey()
        {
            return SessionConstants.CHECKED_ITEMS + rgGLDataRecurringScheduleItems.ClientID;
        }
        /// <summary>
        /// Save Check Box States
        /// </summary>  
        private void SaveCheckBoxStates()
        {
            List<long> oSelectedGLDataRecurringItemScheduleIDList;
            if (Session[getCheckedItemsSessionKey()] != null)
                oSelectedGLDataRecurringItemScheduleIDList = (List<long>)Session[getCheckedItemsSessionKey()];
            else
                oSelectedGLDataRecurringItemScheduleIDList = new List<long>();
            long GLDataRecurringItemScheduleID;
            foreach (GridDataItem item in rgGLDataRecurringScheduleItems.Items)
            {
                GLDataRecurringItemScheduleID = Convert.ToInt64(item.GetDataKeyValue("GLDataRecurringItemScheduleID"));
                if (item.Selected == true)
                {
                    if (oSelectedGLDataRecurringItemScheduleIDList != null && !oSelectedGLDataRecurringItemScheduleIDList.Contains(GLDataRecurringItemScheduleID))
                        oSelectedGLDataRecurringItemScheduleIDList.Add(GLDataRecurringItemScheduleID);
                }
                else
                {
                    if (oSelectedGLDataRecurringItemScheduleIDList != null && oSelectedGLDataRecurringItemScheduleIDList.Contains(GLDataRecurringItemScheduleID))
                        oSelectedGLDataRecurringItemScheduleIDList.Remove(GLDataRecurringItemScheduleID);
                }

            }
            if (oSelectedGLDataRecurringItemScheduleIDList != null && oSelectedGLDataRecurringItemScheduleIDList.Count > 0)
            {
                Session[getCheckedItemsSessionKey()] = oSelectedGLDataRecurringItemScheduleIDList;
            }

        }
        /// <summary>
        /// RePopulate CheckBox States
        /// </summary> 
        private void RePopulateCheckBoxStates()
        {

            List<long> oSelectedGLDataRecurringItemScheduleIDList = null;
            if (Session[getCheckedItemsSessionKey()] != null)
                oSelectedGLDataRecurringItemScheduleIDList = (List<long>)Session[getCheckedItemsSessionKey()];
            if (oSelectedGLDataRecurringItemScheduleIDList != null && oSelectedGLDataRecurringItemScheduleIDList.Count > 0)
            {
                long GLDataRecurringItemScheduleID;
                foreach (GridDataItem item in rgGLDataRecurringScheduleItems.Items)
                {
                    GLDataRecurringItemScheduleID = Convert.ToInt64(item.GetDataKeyValue("GLDataRecurringItemScheduleID"));
                    if (oSelectedGLDataRecurringItemScheduleIDList != null && oSelectedGLDataRecurringItemScheduleIDList.Contains(GLDataRecurringItemScheduleID))
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
        private void MangeColumnsForExport(bool IsVisible)
        {
            GridColumn oGridDeleteColumn = null;
            oGridDeleteColumn = rgGLDataRecurringScheduleItems.MasterTableView.Columns.FindByUniqueNameSafe("LCCYCode");
            if (oGridDeleteColumn != null)
            {
                oGridDeleteColumn.Visible = IsVisible;
            }
            oGridDeleteColumn = rgGLDataRecurringScheduleItems.MasterTableView.Columns.FindByUniqueNameSafe("AmountImport");
            if (oGridDeleteColumn != null)
            {
                oGridDeleteColumn.Visible = IsVisible;
            }
            oGridDeleteColumn = rgGLDataRecurringScheduleItems.MasterTableView.Columns.FindByUniqueNameSafe("Comments");
            if (oGridDeleteColumn != null)
            {
                oGridDeleteColumn.Visible = IsVisible;
            }
            oGridDeleteColumn = rgGLDataRecurringScheduleItems.MasterTableView.Columns.FindByUniqueNameSafe("RefNo");
            if (oGridDeleteColumn != null)
            {
                oGridDeleteColumn.Visible = IsVisible;
            }
            oGridDeleteColumn = rgGLDataRecurringScheduleItems.MasterTableView.Columns.FindByUniqueNameSafe("ScheduleAmount");
            if (oGridDeleteColumn != null)
            {
                oGridDeleteColumn.Visible = !IsVisible;
            }
            oGridDeleteColumn = rgGLDataRecurringScheduleItems.MasterTableView.Columns.FindByUniqueNameSafe("RecItemCommentForImport");
            if (oGridDeleteColumn != null)
            {
                oGridDeleteColumn.Visible = IsVisible;
            }


        }

        #endregion

    }
}