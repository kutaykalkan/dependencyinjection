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

    public partial class UserControls_GLDataRecItemGrid : UserControlRecItemBase
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
                return rgGLDataRecItems;
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
                rgGLDataRecItems.AllowExportToExcel = _AllowExportToExcel;
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
                rgGLDataRecItems.AllowExportToPDF = _AllowExportToPDF;
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
                rgGLDataRecItems.AllowCustomFilter = _AllowCustomFilter;
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
                rgGLDataRecItems.AllowCustomPaging = _AllowCustomPaging;
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
                    rgGLDataRecItems.ClientSettings.ClientEvents.OnRowDeselected = "rgGLDataRecItemSelected";
                    rgGLDataRecItems.ClientSettings.ClientEvents.OnRowSelected = "rgGLDataRecItemSelected";
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
                return rgGLDataRecItems.MasterTableView.Columns;
            }
        }
        [PersistenceMode(PersistenceMode.InnerProperty)]
        public GridClientSettings ClientSettings
        {
            get
            {
                return rgGLDataRecItems.ClientSettings;
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
        /// Show Comments Colum
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
        /// <summary>
        /// GLDataRecItems Grid ShowFooter
        /// </summary>
        public bool ShowFooter
        {
            get { return rgGLDataRecItems.ShowFooter; }
            set { rgGLDataRecItems.ShowFooter = value; }
        }
        /// <summary>
        /// GLDataRecItems Grid Data Table
        /// </summary>
        private DataTable GLDataRecItemsGridDataTable
        {
            get
            {
                if (GLDataRecItemsGridFilteredDataTable != null)
                    return GLDataRecItemsGridFilteredDataTable;
                return (DataTable)Session[GetUniqueSessionKey()];
            }
            set { Session[GetUniqueSessionKey()] = value; }
        }
        /// <summary>
        /// GLAdjustment Grid Filtered Data Table
        /// </summary>
        private DataTable GLDataRecItemsGridFilteredDataTable
        {
            get { return (DataTable)Session[GetFilteredDataUniqueSessionKey()]; }
            set { Session[GetFilteredDataUniqueSessionKey()] = value; }
        }
        protected override void OnInit(EventArgs e)
        {
            Helper.SetGridImageButtonProperties(this.rgGLDataRecItems.MasterTableView.Columns);
            string url;
            url = Page.ResolveClientUrl("~/Pages/GridDynamicFilter.aspx");
            if (_IsOnPage)
                url = url + "?griddynamicfiltersessionkey=" + rgGLDataRecItems.ClientID;
            else
                url = url + "?griddynamicfiltersessionkey=" + rgGLDataRecItems.ClientID + "&IsForPopup=1";

            rgGLDataRecItems.GridApplyFilterOnClientClick = string.Format("ucLoadGridApplyFilterPage('{0}');return false;", url);
            base.OnInit(e);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                isExportPDF = false;
                isExportExcel = false;
                Session[rgGLDataRecItems.ClientID + "NewPageSize"] = "10";
                CreateFilterInfoForGLDataRecItemsGrid();
            }
            GetGridApplyFilterScript();
            if (_AllowDisplayCurrencyPnl)
                GetGridSelectedScript();
            if (_ShowCloseDateColum)
                rgGLDataRecItems.EntityNameLabelID = 1873;
            else
                rgGLDataRecItems.EntityNameLabelID = 1872;
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
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), rgGLDataRecItems.ClientID + "Testing", osb.ToString(), true);
        }
        /// <summary>
        /// Get Grid Selected Script
        /// </summary>
        private void GetGridSelectedScript()
        {
            StringBuilder osb = new StringBuilder();
            osb.Append(@"function rgGLDataRecItemSelected(sender, args) {");
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
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), rgGLDataRecItems.ClientID + "GridSelectedScript", osb.ToString(), true);
        }
        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (_AllowSelectionPersist)
                RePopulateCheckBoxStates();
        }
        /// <summary>
        /// Bind GLDataRecItems Grid
        /// </summary>
        private void BindrgGLDataRecItemsGrid(int PageIndex)
        {
            if (GLDataRecItemsGridDataTable != null)
            {
                rgGLDataRecItems.DataSource = GLDataRecItemsGridDataTable;
                rgGLDataRecItems.VirtualItemCount = GLDataRecItemsGridDataTable.Rows.Count;
                rgGLDataRecItems.DataBind();
                MangeColumnsForExport(false);
            }
        }
        /// <summary>
        /// Bind Load Grid Data 
        /// </summary>
        public void LoadGridData()
        {
            BindrgGLDataRecItemsGrid(0);
        }
        /// <summary>
        /// Selected GLDataRecItemIDList  List<long>
        /// </summary>
        public List<long> SelectedGLDataRecItemIDs()
        {
            List<long> oSelectedGLDataRecItemIDList = null;
            if (_AllowSelectionPersist)
            {
                SaveCheckBoxStates();
                if (Session[getCheckedItemsSessionKey()] != null)
                    oSelectedGLDataRecItemIDList = (List<long>)Session[getCheckedItemsSessionKey()];
            }
            else
                oSelectedGLDataRecItemIDList = GridSelectedItems();
            return oSelectedGLDataRecItemIDList;
        }
        /// <summary>
        /// Selected GLDataRecItemInfo Collection
        /// </summary>
        /// 
        public List<GLDataRecItemInfo> SelectedGLDataRecItemInfoCollection()
        {
            List<GLDataRecItemInfo> oGLDataRecItemInfoCollection = null;
            List<GLDataRecItemInfo> oAllGLDataRecItemInfoCollection = (List<GLDataRecItemInfo>)Session[GetUniqueSessionKey() + "List"];
            List<long> oSelectedGLDataRecItemIDList = SelectedGLDataRecItemIDs();
            if (oAllGLDataRecItemInfoCollection != null && oSelectedGLDataRecItemIDList != null)
                oGLDataRecItemInfoCollection = (from oGLDataRecItemInfo in oAllGLDataRecItemInfoCollection
                                                join SelectedGLDataRecItemID in oSelectedGLDataRecItemIDList on oGLDataRecItemInfo.GLDataRecItemID equals SelectedGLDataRecItemID
                                                select oGLDataRecItemInfo).ToList();
            return oGLDataRecItemInfoCollection;
        }
        /// <summary>
        /// Selected GLDataRecItemID on Current Page
        /// </summary>
        /// 
        private List<long> GridSelectedItems()
        {
            List<long> oSelectedGLDataRecItemIDList = new List<long>(); ;
            long GLDataRecItemID;
            foreach (GridDataItem item in rgGLDataRecItems.SelectedItems)
            {
                GLDataRecItemID = Convert.ToInt64(item.GetDataKeyValue("GLDataRecItemID"));
                oSelectedGLDataRecItemIDList.Add(GLDataRecItemID);
            }
            return oSelectedGLDataRecItemIDList;
        }
        /// <summary>
        /// Bind Load Grid Data 
        /// </summary>
        private string GetGridClientIDKey(ExRadGrid Rg)
        {
            return Rg.ClientID;
        }
        /// <summary>
        /// Create FilterInfo For GLDataRecItems Grid
        /// </summary>
        private void CreateFilterInfoForGLDataRecItemsGrid()
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
            oFilterInfo.ColumnName = "AttachmentCount";
            oFilterInfo.DisplayColumnName = Helper.GetLabelIDValue(2056);
            oFilterInfo.DataType = (short)WebEnums.DataType.Integer;
            oFilterInfoList.Add(oFilterInfo);

            oFilterInfo = new FilterInfo();
            oFilterInfo.ColumnID = 7;
            oFilterInfo.ColumnName = "RecItemNumber";
            oFilterInfo.DisplayColumnName = Helper.GetLabelIDValue(2118);
            oFilterInfo.DataType = (short)WebEnums.DataType.String;
            oFilterInfoList.Add(oFilterInfo);

            oFilterInfo = new FilterInfo();
            oFilterInfo.ColumnID = 8;
            oFilterInfo.ColumnName = "MatchSetRefNumber";
            oFilterInfo.DisplayColumnName = Helper.GetLabelIDValue(2276);
            oFilterInfo.DataType = (short)WebEnums.DataType.String;
            oFilterInfoList.Add(oFilterInfo);

            if (_ShowCloseDateColum)
            {
                oFilterInfo = new FilterInfo();
                oFilterInfo.ColumnID = 9;
                oFilterInfo.ColumnName = "CloseDate";
                oFilterInfo.DisplayColumnName = Helper.GetLabelIDValue(1411);
                oFilterInfo.DataType = (short)WebEnums.DataType.DataTime;
                oFilterInfoList.Add(oFilterInfo);
            }

            SessionHelper.SetDynamicFilterColumns(oFilterInfoList, GetGridClientIDKey(rgGLDataRecItems));

        }
        private string GetUniqueSessionKey()
        {
            return GetGridClientIDKey(rgGLDataRecItems) + SessionConstants.GLADJUSTMENT_GRID_DATA;
        }
        private string GetFilteredDataUniqueSessionKey()
        {
            return GetGridClientIDKey(rgGLDataRecItems) + SessionConstants.GLADJUSTMENT_GRID_DATA_FILTERED;
        }
        /// <summary>
        /// Apply Filter GLDataRecItems Grid
        /// </summary>
        public void ApplyFilterGLDataRecItemsGrid()
        {
            ClearCheckedItemViewState();
            ClearTotals();
            string whereClause = SessionHelper.GetDynamicFilterResultWhereClause(GetGridClientIDKey(rgGLDataRecItems));
            GLDataRecItemsGridFilteredDataTable = null;
            if (!string.IsNullOrEmpty(whereClause) && GLDataRecItemsGridDataTable != null && GLDataRecItemsGridDataTable.Rows.Count > 0)
            {
                DataRow[] filterResult = GLDataRecItemsGridDataTable.Select(whereClause);
                if (filterResult != null && filterResult.Length > 0)
                    GLDataRecItemsGridFilteredDataTable = filterResult.CopyToDataTable();
                else
                    GLDataRecItemsGridFilteredDataTable = GLDataRecItemsGridDataTable.Clone();
            }
            BindrgGLDataRecItemsGrid(0);
        }
        /// <summary>
        /// Set GLDataRecItem Grid Data
        /// </summary>
        /// 
        public void SetGLDataRecItemGridData(List<GLDataRecItemInfo> oGLDataRecItemInfoCollection)
        {
            ClearGLDataRecItemGridData();
            if (oGLDataRecItemInfoCollection != null)
            {
                Session[GetUniqueSessionKey() + "List"] = oGLDataRecItemInfoCollection;
                GLDataRecItemsGridDataTable = MatchingHelper.ListToDataTable(oGLDataRecItemInfoCollection);
                ClearCheckedItemViewState();
            }
        }
        /// <summary>
        /// Clear GLDataRecItem Grid Data
        /// </summary>
        /// 
        private void ClearGLDataRecItemGridData()
        {
            GLDataRecItemsGridDataTable = null;
            GLDataRecItemsGridFilteredDataTable = null;
            Session[GetUniqueSessionKey() + "List"] = null;
        }
        #region GLDataRecItems Grid Events
        protected void rgGLDataRecItems_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {

            rgGLDataRecItems.DataSource = GLDataRecItemsGridDataTable;
            if (GLDataRecItemsGridDataTable != null)
                rgGLDataRecItems.VirtualItemCount = GLDataRecItemsGridDataTable.Rows.Count;

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
        int? refNo = null;
        protected void rgGLDataRecItems_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {

            if (e.Item.ItemType == GridItemType.Header)
            {
                rgGLDataRecItems.MasterTableView.Columns.FindByUniqueName("Comments").Visible = _ShowDescriptionColum;
                rgGLDataRecItems.MasterTableView.Columns.FindByUniqueName("CloseDate").Visible = _ShowCloseDateColum;
                rgGLDataRecItems.MasterTableView.Columns.FindByUniqueName("AttachmentCount").Visible = _ShowDocsColumn;
                rgGLDataRecItems.MasterTableView.Columns.FindByUniqueName("CheckboxSelectColumn").Visible = this.selectOption;
                if (!_IsOnPage)
                    SessionHelper.ShowGridFilterIcon((PopupPageBase)this.Page, rgGLDataRecItems, e, GetGridClientIDKey(rgGLDataRecItems));
                ClearTotals();
            }
            if (e.Item.ItemType == Telerik.Web.UI.GridItemType.Item || e.Item.ItemType == Telerik.Web.UI.GridItemType.AlternatingItem)
            {

                GridDataItem oGridDataItem = e.Item as GridDataItem;
                DataRow dr = ((DataRowView)oGridDataItem.DataItem).Row;

                bool IsForwardedItem;
                bool.TryParse(dr["IsForwardedItem"].ToString(), out IsForwardedItem);

                ExLabel lblDescription = (ExLabel)e.Item.FindControl("lblDescription");
                if (isExportExcel || isExportPDF)
                    lblDescription.Text = Convert.ToString(dr["Comments"]);
                else
                    Helper.SetTextAndTooltipValue(lblDescription, Convert.ToString(dr["Comments"]));
                ExLabel lblAmount = (ExLabel)e.Item.FindControl("lblAmount");
                lblAmount.Text = Helper.GetDisplayCurrencyValue(Convert.ToString(dr["LocalCurrencyCode"]), Convert.ToDecimal(dr["Amount"]));
                ExLabel lblAmountImport = (ExLabel)e.Item.FindControl("lblAmountImport");
                lblAmountImport.Text = Helper.GetDisplayDecimalValue(Convert.ToDecimal(dr["Amount"]));

                ExLabel lblLocalCurrencyCode = (ExLabel)e.Item.FindControl("lblLocalCurrencyCode");
                lblLocalCurrencyCode.Text = Helper.GetDisplayStringValue(Convert.ToString(dr["LocalCurrencyCode"]));

                decimal AmountBaseCurrency;
                decimal.TryParse(Convert.ToString(dr["AmountBaseCurrency"]), out AmountBaseCurrency);
                ExLabel lblAmountBaseCurrency = (ExLabel)e.Item.FindControl("lblAmountBaseCurrency");
                lblAmountBaseCurrency.Text = Helper.GetDisplayBaseCurrencyValue(this.CurrentBCCY, AmountBaseCurrency);
                ExLabel lblAmountReportingCurrency = (ExLabel)e.Item.FindControl("lblAmountReportingCurrency");
                lblAmountReportingCurrency.Text = Helper.GetDisplayDecimalValue(Convert.ToDecimal(dr["AmountReportingCurrency"]));
                ExLabel lblOpenDate = (ExLabel)e.Item.FindControl("lblOpenDate");
                lblOpenDate.Text = Helper.GetDisplayDate(Convert.ToDateTime(dr["OpenDate"]));
                ExLabel lblDateForImport = (ExLabel)e.Item.FindControl("lblDateForImport");
                lblDateForImport.Text = Helper.GetDisplayDate(Convert.ToDateTime(dr["OpenDate"]));

                DateTime? closeDate = null;
                if (_ShowCloseDateColum)
                {
                    ExLabel lblCloseDate = (ExLabel)e.Item.FindControl("lblCloseDate");
                    closeDate = Convert.ToDateTime(dr["CloseDate"]);
                    lblCloseDate.Text = Helper.GetDisplayDate(closeDate);
                }
                if (_ShowDocsColumn)
                {
                    ExHyperLink hlAttachmentCount = (ExHyperLink)e.Item.FindControl("hlAttachmentCount");
                    hlAttachmentCount.Text = Helper.GetDisplayIntegerValue(dr.GetInt32Value("AttachmentCount"));
                    string windowName;
                    hlAttachmentCount.NavigateUrl = "javascript:OpenRadWindowForHyperlink('" + Helper.SetDocumentUploadURLForRecItemInput(this.GLDataID, dr.GetInt32Value("GLDataRecItemID"), this.AccountID, this.NetAccountID, true, Request.Url.ToString(), out windowName, IsForwardedItem, WebEnums.RecordType.GLReconciliationItemInput) + "', " + POPUP_HEIGHT + " , " + POPUP_WIDTH + ");";
                }
                ExLabel lblRecItemNumber = (ExLabel)e.Item.FindControl("lblRecItemNumber");
                lblRecItemNumber.Text = Helper.GetDisplayStringValue(dr["RecItemNumber"].ToString());
                ExLabel lblAging = (ExLabel)e.Item.FindControl("lblAging");
                lblAging.Text = Helper.GetDisplayIntegerValue(Helper.GetDaysBetweenDateRanges(Convert.ToDateTime(dr["OpenDate"]), (closeDate.GetValueOrDefault() == default(DateTime)) ? DateTime.Now : closeDate));
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

                ExLabel lblRecItemComments = (ExLabel)e.Item.FindControl("lblRecItemComments");
                if (lblRecItemComments != null)
                {
                    long GLDataRecItemID = Convert.ToInt64(dr["GLDataRecItemID"]);
                    IGLDataRecItem oGLDataRecItemClient = RemotingHelper.GetGLDataRecItemObject();
                    List<RecItemCommentInfo> RecItemCommentInfoList;
                    RecItemCommentInfoList = oGLDataRecItemClient.GetRecItemCommentList(GLDataRecItemID, 2, Helper.GetAppUserInfo());
                    lblRecItemComments.Text = Helper.GetRecItemComments(RecItemCommentInfoList);
                }
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
            // Raise Event for Page to Handle it
            //if (Grid_ItemDataBoundEventHandler != null)
            //    RaiseGrid_ItemDataBoundEventHandler(sender, e);
            if (GridItemDataBound != null)
                GridItemDataBound(sender, e);

        }
        private void MangeColumnsForExport(bool IsVisible)
        {
            GridColumn oGridDeleteColumn = null;
            oGridDeleteColumn = rgGLDataRecItems.MasterTableView.Columns.FindByUniqueNameSafe("LCCYCode");
            if (oGridDeleteColumn != null)
            {
                oGridDeleteColumn.Visible = IsVisible;
            }
            oGridDeleteColumn = rgGLDataRecItems.MasterTableView.Columns.FindByUniqueNameSafe("AmountImport");
            if (oGridDeleteColumn != null)
            {
                oGridDeleteColumn.Visible = IsVisible;
            }
            oGridDeleteColumn = rgGLDataRecItems.MasterTableView.Columns.FindByUniqueNameSafe("DateForImport");
            if (oGridDeleteColumn != null)
            {
                oGridDeleteColumn.Visible = IsVisible;
            }
            oGridDeleteColumn = rgGLDataRecItems.MasterTableView.Columns.FindByUniqueNameSafe("RefNo");
            if (oGridDeleteColumn != null)
            {
                oGridDeleteColumn.Visible = IsVisible;
            }
            oGridDeleteColumn = rgGLDataRecItems.MasterTableView.Columns.FindByUniqueNameSafe("Amount");
            if (oGridDeleteColumn != null)
            {
                oGridDeleteColumn.Visible = !IsVisible;
            }
            oGridDeleteColumn = rgGLDataRecItems.MasterTableView.Columns.FindByUniqueNameSafe("OpenDate");
            if (oGridDeleteColumn != null)
            {
                oGridDeleteColumn.Visible = !IsVisible;
            }
            oGridDeleteColumn = rgGLDataRecItems.MasterTableView.Columns.FindByUniqueNameSafe("RecItemCommentForImport");
            if (oGridDeleteColumn != null)
            {
                oGridDeleteColumn.Visible = IsVisible;
            }

        }
        protected void rgGLDataRecItems_ItemCommand(object source, GridCommandEventArgs e)
        {
            // Raise Event for Page to Handle it       
            if (GridCommand != null)
            {
                GridCommand(source, e);
            }
            if (e.CommandName == TelerikConstants.GridClearFilterCommandName)
            {
                SessionHelper.ClearDynamicFilterData(GetGridClientIDKey(rgGLDataRecItems));
                ApplyFilterGLDataRecItemsGrid();
            }
            if (e.CommandName == TelerikConstants.GridExportToPDFCommandName)
            {
                isExportPDF = true;
                rgGLDataRecItems.MasterTableView.Columns.FindByUniqueName("CheckboxSelectColumn").Display = false;
                GridColumn oGridDeleteColumn = null;
                oGridDeleteColumn = rgGLDataRecItems.MasterTableView.Columns.FindByUniqueNameSafe("DeleteColumn");
                if (oGridDeleteColumn != null)
                {
                    oGridDeleteColumn.Visible = false;
                }
                GridColumn oGridShowInputFormColumn = null;
                oGridShowInputFormColumn = rgGLDataRecItems.MasterTableView.Columns.FindByUniqueNameSafe("ShowInputForm");
                if (oGridShowInputFormColumn != null)
                {
                    oGridShowInputFormColumn.Visible = false;
                }
                MangeColumnsForExport(true);
                GridHelper.ExportGridToPDF(rgGLDataRecItems, this.BasePageTitleLabelID);
            }
            if (e.CommandName == TelerikConstants.GridExportToExcelCommandName)
            {
                isExportExcel = true;
                GridColumn oGridDeleteColumn = null;
                oGridDeleteColumn = rgGLDataRecItems.MasterTableView.Columns.FindByUniqueNameSafe("DeleteColumn");
                if (oGridDeleteColumn != null)
                {
                    oGridDeleteColumn.Visible = false;
                }
                GridColumn oGridShowInputFormColumn = null;
                oGridShowInputFormColumn = rgGLDataRecItems.MasterTableView.Columns.FindByUniqueNameSafe("ShowInputForm");
                if (oGridShowInputFormColumn != null)
                {
                    oGridShowInputFormColumn.Visible = false;
                }
                rgGLDataRecItems.MasterTableView.Columns.FindByUniqueName("CheckboxSelectColumn").Display = false;
                MangeColumnsForExport(true);
                GridHelper.ExportGridToExcel(rgGLDataRecItems, this.BasePageTitleLabelID);
                // MangeColumnsForExport(false);
            }
        }
        protected void rgGLDataRecItems_PdfExporting(object source, GridPdfExportingArgs e)
        {
            ExportHelper.GeneratePDFAndRender(ExportHelper.RemoveInvalidFileNameChars(LanguageUtil.GetValue(this.BasePageTitleLabelID)),
            ExportHelper.RemoveInvalidFileNameChars(LanguageUtil.GetValue(this.BasePageTitleLabelID)), e.RawHTML, false, false);
        }
        protected void rgGLDataRecItems_ItemCreated(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridPagerItem)
            {
                GridPagerItem gridPager = e.Item as GridPagerItem;
                DropDownList oRadComboBox = (DropDownList)gridPager.FindControl("ddlPageSize");
                if (rgGLDataRecItems.AllowCustomPaging)
                {
                    GridHelper.BindPageSizeGrid(oRadComboBox);
                    if (Session[GetGridClientIDKey(rgGLDataRecItems) + "NewPageSize"] != null)
                        oRadComboBox.SelectedValue = Session[GetGridClientIDKey(rgGLDataRecItems) + "NewPageSize"].ToString();
                    oRadComboBox.Attributes.Add("onChange", "return ddlPageSize_SelectedIndexChanged('" + oRadComboBox.ClientID + "' , '" + rgGLDataRecItems.ClientID + "');");
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
        protected void rgGLDataRecItems_PageSizeChanged(object source, GridPageSizeChangedEventArgs e)
        {
            if (!(isExportPDF || isExportExcel))
                Session[GetGridClientIDKey(rgGLDataRecItems) + "NewPageSize"] = e.NewPageSize.ToString();
        }
        protected void rgGLDataRecItems_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            if (_AllowSelectionPersist)
                SaveCheckBoxStates();
            BindrgGLDataRecItemsGrid(e.NewPageIndex);
            ClearTotals();
        }
        #endregion
        #region GLDataRecItems Grid Paging Events

        /// <summary>
        /// get Checked Items SessionKey
        /// </summary>   
        private string getCheckedItemsSessionKey()
        {
            return SessionConstants.CHECKED_ITEMS + rgGLDataRecItems.ClientID;
        }
        /// <summary>
        /// Save Check Box States
        /// </summary>  
        private void SaveCheckBoxStates()
        {
            List<long> oSelectedGLDataRecItemIDList;
            if (Session[getCheckedItemsSessionKey()] != null)
                oSelectedGLDataRecItemIDList = (List<long>)Session[getCheckedItemsSessionKey()];
            else
                oSelectedGLDataRecItemIDList = new List<long>();
            long GLDataRecItemID;
            foreach (GridDataItem item in rgGLDataRecItems.Items)
            {
                GLDataRecItemID = Convert.ToInt64(item.GetDataKeyValue("GLDataRecItemID"));
                if (item.Selected == true)
                {
                    if (oSelectedGLDataRecItemIDList != null && !oSelectedGLDataRecItemIDList.Contains(GLDataRecItemID))
                        oSelectedGLDataRecItemIDList.Add(GLDataRecItemID);
                }
                else
                {
                    if (oSelectedGLDataRecItemIDList != null && oSelectedGLDataRecItemIDList.Contains(GLDataRecItemID))
                        oSelectedGLDataRecItemIDList.Remove(GLDataRecItemID);
                }

            }
            if (oSelectedGLDataRecItemIDList != null && oSelectedGLDataRecItemIDList.Count > 0)
            {
                Session[getCheckedItemsSessionKey()] = oSelectedGLDataRecItemIDList;
            }

        }
        /// <summary>
        /// RePopulate CheckBox States
        /// </summary> 
        private void RePopulateCheckBoxStates()
        {

            List<long> oSelectedGLDataRecItemIDList = null;
            if (Session[getCheckedItemsSessionKey()] != null)
                oSelectedGLDataRecItemIDList = (List<long>)Session[getCheckedItemsSessionKey()];
            if (oSelectedGLDataRecItemIDList != null && oSelectedGLDataRecItemIDList.Count > 0)
            {
                long GLDataRecItemID;
                foreach (GridDataItem item in rgGLDataRecItems.Items)
                {
                    GLDataRecItemID = Convert.ToInt64(item.GetDataKeyValue("GLDataRecItemID"));
                    if (oSelectedGLDataRecItemIDList != null && oSelectedGLDataRecItemIDList.Contains(GLDataRecItemID))
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