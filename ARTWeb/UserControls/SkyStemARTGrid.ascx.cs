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
using Telerik.Web.UI;
using SkyStem.ART.Client.Model.Base;
using SkyStem.Library.Controls.WebControls;
using SkyStem.ART.Client.IServices;
using SkyStem.ART.Web.Utility;
using SkyStem.ART.Client.Model;
using System.Collections.Generic;
using SkyStem.ART.Web.Data;
using SkyStem.Language.LanguageUtility;
using SkyStem.Library.Controls.TelerikWebControls;
using SkyStem.Library.Controls.TelerikWebControls.Common;
using SkyStem.ART.Client.Data;
using System.Text;
using SkyStem.Library.Controls.TelerikWebControls.Data;
using SkyStem.ART.Web.Classes;

[ParseChildren(ChildrenAsProperties = true)]
public partial class UserControls_SkyStemARTGrid : UserControlBase
{
    bool isExportPDF;
    bool isExportExcel;
    public static bool isFetchAllRecords = false;

    const int GRID_COLUMN_INDEX_STATUS_IMAGE = 0;
    const int GRID_COLUMN_INDEX_SELECT = 1;
    const int GRID_COLUMN_INDEX_KEY_START = 3;


    //public delegate GridItemEventHandler OnGridItemDataBound(object sender, GridItemEventArgs e);
    public event GridItemEventHandler GridItemDataBound;
    public event GridCommandEventHandler GridCommand;
    public event GridDetailTableDataBindEventHandler GridDetailTableDataBind;
    //public event GridItemEventHandler GridItemCreated;

    //public properties
    public ExRadGrid RgAccount
    {
        get { return rgAccount; }
    }

    // Delegate to handle paging in grid
    public delegate object Grid_NeedDataSource(int count);
    public event Grid_NeedDataSource Grid_NeedDataSourceEventHandler;


    //*****Delegate to handle Page Index Change Event************************************************
    public delegate void Grid_PageIndexChanged();
    public event Grid_PageIndexChanged PageIndexChangedEvent;
    //***********************************************************************************************

    //*****Delegate to handle CheckBoxStatus while Sorting*******************************************
    public delegate void Grid_ColumnSorting();
    public event Grid_ColumnSorting GridColumnSortingEvent;

    //Delegate to handle clear filter in grid
    public delegate void Grid_ClearFilter();
    public event Grid_ClearFilter Grid_ClearFilterEventHandler;

    public delegate void Grid_Minimize();
    public event Grid_Minimize Grid_MinimizeEventHandler;

    public delegate void Grid_Maximize();
    public event Grid_Maximize Grid_MaximizeEventHandler;

    public delegate void GridRefresh();
    public event GridRefresh GridRefreshEvent;

    public delegate void GridItemCreated(object sender, GridItemEventArgs e, bool isExportPDF, bool isExportExcel);
    public event GridItemCreated GridItemCreatedEvent;

    private object RaiseGrid_NeedDataSourceEventHandler(int count)
    {
        if (Grid_NeedDataSourceEventHandler != null)
        {
            return Grid_NeedDataSourceEventHandler(count);
        }

        return null;
    }


    private void RaiseGrid_ClearFilterEventHandler()
    {
        if (Grid_ClearFilterEventHandler != null)
        {
            Grid_ClearFilterEventHandler();
        }
    }

    private void RaiseGrid_Minimize()
    {

        if (Grid_MinimizeEventHandler != null)
        {
            Grid_MinimizeEventHandler();
        }
    }
    private void RaiseGrid_Maximize()
    {
        if (Grid_MaximizeEventHandler != null)
        {
            Grid_MaximizeEventHandler();
        }

    }

    private void RaiseGrid_PageIndexChangedEventHandler()
    {
        if (PageIndexChangedEvent != null)
        {
            PageIndexChangedEvent();
        }

    }

    private void RaiseGrid_ColumnSortingEventHandler()
    {
        if (GridColumnSortingEvent != null)
        {
            GridColumnSortingEvent();
        }

    }


    private void RaiseGridRefreshEvent()
    {
        if (GridRefreshEvent != null)
        {
            GridRefreshEvent();
        }

    }

    private void RaiseGridItemCreated(object sender, GridItemEventArgs e, bool isExportPDF, bool isExportExcel)
    {
        if (GridItemCreatedEvent != null)
        {
            GridItemCreatedEvent(sender, e, isExportPDF, isExportExcel);
        }

    }


    /// <summary>
    /// Grid's -> Master Table View Column Collection
    /// </summary>

    #region Properties
    private GridGroupByExpression _GridGroupByExpression = null;
    public GridGroupByExpression GridGroupByExpression
    {
        get { return _GridGroupByExpression; }
        set { _GridGroupByExpression = value; }
    }
    private GridGroupByExpression _TaskListGridGroupByExpression = null;
    public GridGroupByExpression TaskListGridGroupByExpression
    {
        get { return _TaskListGridGroupByExpression; }
        set { _TaskListGridGroupByExpression = value; }
    }

    private GridGroupByExpression _TaskSubListGridGroupByExpression = null;
    public GridGroupByExpression TaskSubListGridGroupByExpression
    {
        get { return _TaskSubListGridGroupByExpression; }
        set { _TaskSubListGridGroupByExpression = value; }
    }

    [PersistenceMode(PersistenceMode.InnerProperty)]
    public GridColumnCollection SkyStemGridColumnCollection
    {
        get
        {
            return rgAccount.MasterTableView.Columns;
        }
    }

    [PersistenceMode(PersistenceMode.InnerProperty)]
    public GridTableViewCollection DetailTables
    {
        get
        {
            return rgAccount.MasterTableView.DetailTables;
        }
    }

    [PersistenceMode(PersistenceMode.InnerProperty)]
    public GridSelfHierarchySettings SelfHierarchySettings
    {
        get
        {
            return rgAccount.MasterTableView.SelfHierarchySettings;
        }
    }

    [PersistenceMode(PersistenceMode.InnerProperty)]
    public GridClientSettings ClientSettings
    {
        get
        {
            return rgAccount.ClientSettings;
        }
    }
    [PersistenceMode(PersistenceMode.InnerProperty)]
    public GridTableView SkyStemGridMasterTableView
    {
        get
        {
            return rgAccount.MasterTableView;
        }
    }

    private bool selectOption;
    public bool ShowSelectCheckBoxColum
    {

        set
        {
            selectOption = value;
            if (selectOption)
            {
                rgAccount.Columns[GRID_COLUMN_INDEX_SELECT].Visible = true;
            }
            else
            {
                rgAccount.Columns[GRID_COLUMN_INDEX_SELECT].Visible = false;
            }


        }
    }

    private bool _ShowSerialNumberColumn = false;
    public bool ShowSerialNumberColumn
    {
        set
        {
            _ShowSerialNumberColumn = value;
        }
        get
        {
            return _ShowSerialNumberColumn;
        }
    }

    private bool _ShowTaskStatusImage = false;
    public bool ShowTaskStatusImage
    {
        set
        {
            _ShowTaskStatusImage = value;


        }
        get
        {
            return _ShowTaskStatusImage;
        }
    }

    public bool ShowFooter
    {
        get { return rgAccount.ShowFooter; }
        set { rgAccount.ShowFooter = value; }
    }

    private bool isCustomPaging = false;
    public bool CustomPaging
    {
        get
        {
            return isCustomPaging;
        }

        set
        {
            isCustomPaging = value;


        }
    }
    public string CustomPageSize
    {
        get
        {
            return this.hdnNewPageSize.Value;
        }
        set
        {
            this.hdnNewPageSize.Value = value;
        }
    }

    public bool ShowStatusImageColumn
    {
        set
        {
            selectOption = value;
            if (selectOption)
            {
                rgAccount.Columns[GRID_COLUMN_INDEX_STATUS_IMAGE].Visible = true;
            }
            else
            {
                rgAccount.Columns[GRID_COLUMN_INDEX_STATUS_IMAGE].Visible = false;
            }
        }
    }

    private bool IsHideColumnAccountMassBuklupd = false;
    public bool HideColumnAccountMassBuklupd
    {
        get
        {
            return IsHideColumnAccountMassBuklupd;
        }

        set
        {
            IsHideColumnAccountMassBuklupd = value;


        }
    }


    /// <summary>
    /// The contained control "RadGrid"
    /// </summary>
    public ExRadGrid Grid
    {
        get
        {
            return rgAccount;
        }
    }

    private int? _CompanyID = null;

    /// <summary>
    /// Company ID to Pick Organizational Hierarchy
    /// </summary>
    public int? CompanyID
    {
        get { return _CompanyID; }
        set { _CompanyID = value; }
    }

    /// <summary>
    /// Data to be shown on UI
    /// </summary>.
    /// 
    public object DataSource
    {
        get
        {
            return rgAccount.DataSource;
        }
        set
        {
            rgAccount.DataSource = value;
        }
    }


    /// <summary>
    /// Property to Set the Type of Grid
    /// This helps in Personalization
    /// </summary>

    //public ARTEnums.Grid GridType
    //{
    //    get
    //    {
    //        ARTEnums.Grid eGrid = ARTEnums.Grid.None;
    //        if (ViewState[ViewStateConstants.GRID_TYPE] != null)
    //        {
    //            eGrid = (ARTEnums.Grid)System.Enum.Parse(typeof(ARTEnums.Grid), ViewState[ViewStateConstants.GRID_TYPE].ToString(), true);
    //        }
    //        return eGrid;
    //    }
    //    set
    //    {
    //        ViewState[ViewStateConstants.GRID_TYPE] = value;
    //    }
    //}

    private ARTEnums.Grid _GridType = ARTEnums.Grid.None;
    public ARTEnums.Grid GridType
    {
        get
        {
            return _GridType;
        }
        set
        {
            _GridType = value;
        }
    }


    private int _BasePageTitle = 0;

    /// <summary>
    /// Base Page Title 
    /// </summary>
    public int BasePageTitle
    {
        get { return _BasePageTitle; }
        set { _BasePageTitle = value; }
    }

    private List<string> _TemplateColumnNameToHideList = null;

    /// <summary>
    /// Base Page Title 
    /// </summary>
    public List<string> TemplateColumnNameToHideList
    {
        get { return _TemplateColumnNameToHideList; }
        set { _TemplateColumnNameToHideList = value; }
    }

    private string _CustomGridApplyFilterOnClientClick;
    public string CustomGridApplyFilterOnClientClick
    {
        get
        {
            return _CustomGridApplyFilterOnClientClick;
        }
        set
        {
            _CustomGridApplyFilterOnClientClick = value;
            this.rgAccount.GridApplyFilterOnClientClick = value;
        }
    }
    private bool _BindSkipedRecords = false;
    public bool BindSkipedRecords
    {
        set
        {
            _BindSkipedRecords = value;
        }
        get
        {
            return _BindSkipedRecords;
        }
    }

    #endregion





    protected void Page_Load(object sender, EventArgs e)
    {
        //rgAccount.ItemCommand += new GridCommandEventHandler(rgAccount_ItemCommand);
        //rgAccount.ColumnCreated += new GridColumnCreatedEventHandler(rgAccount_ColumnCreated);
        rgAccount.NeedDataSource += new GridNeedDataSourceEventHandler(rgAccount_NeedDataSource);
        rgAccount.DetailTableDataBind += new GridDetailTableDataBindEventHandler(rgAccount_DetailTableDataBind);
        if (rgAccount.AllowCustomization)
            GetGridCustomizationScript();

        if (!Page.IsPostBack)
        {
            isExportPDF = false;
            isExportExcel = false;
            isFetchAllRecords = false;

        }
        //rgAccount.MasterTableView.Columns.FindByUniqueName("TaskStatusImage").Visible = _ShowTaskStatusImage;
    }

    #region "Grid Events"
    void rgAccount_DetailTableDataBind(object source, GridDetailTableDataBindEventArgs e)
    {
        // Raise Event for Page to Handle it
        if (GridDetailTableDataBind != null)
            GridDetailTableDataBind(source, e);
    }

    public void rgAccount_ItemCommand(object source, GridCommandEventArgs e)
    {
        if (GridCommand != null)
        {
            GridCommand(source, e);
        }

        if (e.CommandName == TelerikConstants.GridExportToPDFCommandName)
        {
            isExportPDF = true;
            if (TemplateColumnNameToHideList != null && TemplateColumnNameToHideList.Count > 0)
            {
                foreach (string oTemplateColumnName in TemplateColumnNameToHideList)
                {
                    rgAccount.MasterTableView.Columns.FindByUniqueName(oTemplateColumnName).Visible = false;
                }
            }

            if (IsHideColumnAccountMassBuklupd)
                showHideColumnAccountBulkUpd();

            if (rgAccount.AllowCustomPaging)
            {
                bindAllRecords();
                rgAccount.MasterTableView.Columns.FindByUniqueName("CheckboxSelectColumn").Display = false;
                rgAccount.MasterTableView.Columns.FindByUniqueName("CheckboxSelectColumn").Visible = false;
                rgAccount.MasterTableView.Columns.FindByUniqueName("ImageColumn").Display = false;
                rgAccount.MasterTableView.Columns.FindByUniqueName("ImageColumn").Visible = false;
                GridColumn oGridColumn = null;
                oGridColumn = rgAccount.MasterTableView.Columns.FindByUniqueNameSafe("Edit");
                if (oGridColumn != null)
                    oGridColumn.Display = false;
                oGridColumn = rgAccount.MasterTableView.Columns.FindByUniqueNameSafe("CommentIcon");
                if (oGridColumn != null)
                    oGridColumn.Display = false;
                //if (BindSkipedRecords) Commented by manoj : As Per Discussion with Vinay this grid should have LandScape PDF and Excel
                GridHelper.ExportGridToPDFLandScape(rgAccount, ExportHelper.RemoveInvalidFileNameChars(LanguageUtil.GetValue(this.BasePageTitle)), true);
                //else
                //    GridHelper.ExportGridToPDF(rgAccount, ExportHelper.RemoveInvalidFileNameChars(LanguageUtil.GetValue(this.BasePageTitle)));
                unSetBindAllRecords();
            }
            else
            {
                rgAccount.MasterTableView.Columns.FindByUniqueName("CheckboxSelectColumn").Display = false;
                rgAccount.MasterTableView.Columns.FindByUniqueName("CheckboxSelectColumn").Visible = false;
                rgAccount.MasterTableView.Columns.FindByUniqueName("ImageColumn").Display = false;
                rgAccount.MasterTableView.Columns.FindByUniqueName("ImageColumn").Visible = false;
                GridColumn oGridColumn = null;
                oGridColumn = rgAccount.MasterTableView.Columns.FindByUniqueNameSafe("Edit");
                if (oGridColumn != null)
                    oGridColumn.Display = false;
                oGridColumn = rgAccount.MasterTableView.Columns.FindByUniqueNameSafe("CommentIcon");
                if (oGridColumn != null)
                    oGridColumn.Display = false;
                //if (BindSkipedRecords)
                GridHelper.ExportGridToPDFLandScape(rgAccount, ExportHelper.RemoveInvalidFileNameChars(LanguageUtil.GetValue(this.BasePageTitle)), true);
                //else
                //    GridHelper.ExportGridToPDF(rgAccount, ExportHelper.RemoveInvalidFileNameChars(LanguageUtil.GetValue(this.BasePageTitle)));

            }


        }
        if (e.CommandName == TelerikConstants.GridExportToExcelCommandName)
        {
            ViewState["CurrentPageIndex"] = rgAccount.CurrentPageIndex;
            if (TemplateColumnNameToHideList != null && TemplateColumnNameToHideList.Count > 0)
            {
                foreach (string oTemplateColumnName in TemplateColumnNameToHideList)
                {
                    rgAccount.MasterTableView.Columns.FindByUniqueName(oTemplateColumnName).Visible = false;
                }
            }

            if (IsHideColumnAccountMassBuklupd)
                showHideColumnAccountBulkUpd();
            isExportExcel = true;
            // Hide The Template Column which are not required 
            int x = rgAccount.MasterTableView.DetailTables.Count;
            if (rgAccount.AllowCustomPaging)
            {
                bindAllRecords();
                rgAccount.MasterTableView.Columns.FindByUniqueName("CheckboxSelectColumn").Display = false;
                rgAccount.MasterTableView.Columns.FindByUniqueName("CheckboxSelectColumn").Visible = false;
                rgAccount.MasterTableView.Columns.FindByUniqueName("ImageColumn").Display = false;
                rgAccount.MasterTableView.Columns.FindByUniqueName("ImageColumn").Visible = false;
                GridHelper.ExportGridToExcel(rgAccount, this.BasePageTitle);
                unSetBindAllRecords();
            }
            else
            {
                rgAccount.MasterTableView.Columns.FindByUniqueName("CheckboxSelectColumn").Display = false;
                rgAccount.MasterTableView.Columns.FindByUniqueName("CheckboxSelectColumn").Visible = false;
                rgAccount.MasterTableView.Columns.FindByUniqueName("ImageColumn").Display = false;
                rgAccount.MasterTableView.Columns.FindByUniqueName("ImageColumn").Visible = false;
                GridHelper.ExportGridToExcel(rgAccount, this.BasePageTitle);
            }
        }

        if (e.CommandName == TelerikConstants.GridClearFilterCommandName)
        {
            RaiseGrid_ClearFilterEventHandler();
        }
        if (e.CommandName == TelerikConstants.GridMinimizeCommandName)
        {
            RaiseGrid_Minimize();
        }
        if (e.CommandName == TelerikConstants.GridMaximizeCommandName)
        {
            RaiseGrid_Maximize();
        }
        if (e.CommandName == TelerikConstants.GridRefreshCommandName)
        {
            RaiseGridRefreshEvent();
        }
    }

    protected void rgAccount_PdfExporting(object source, GridPdfExportingArgs e)
    {
        ExportHelper.GeneratePDFAndRender(ExportHelper.RemoveInvalidFileNameChars(LanguageUtil.GetValue(this.BasePageTitle)),
           ExportHelper.RemoveInvalidFileNameChars(LanguageUtil.GetValue(this.BasePageTitle)), e.RawHTML, false, false);
    }

    public void showHideColumnAccountBulkUpd()
    {
        GridColumn oGridColumnRiskRatingLbl = rgAccount.MasterTableView.Columns.FindByUniqueName("RiskRatingLbl");
        if (oGridColumnRiskRatingLbl != null)
        {
            oGridColumnRiskRatingLbl.Visible = true;
        }

        GridColumn oGridColumnReconciliationTemplateExport = rgAccount.MasterTableView.Columns.FindByUniqueName("ReconciliationTemplateExport");
        if (oGridColumnReconciliationTemplateExport != null)
        {
            oGridColumnReconciliationTemplateExport.Visible = true;
        }


        GridColumn oGridColumnSubledgerExport = rgAccount.MasterTableView.Columns.FindByUniqueName("SubledgerExport");
        if (oGridColumnSubledgerExport != null)
        {
            oGridColumnSubledgerExport.Visible = true;
        }

        GridColumn oGridColumnRiskRating = rgAccount.MasterTableView.Columns.FindByUniqueName("RiskRating");
        if (oGridColumnRiskRating != null)
        {
            oGridColumnRiskRating.Visible = false;


        }
        GridColumn oGridColumnReconciliationTemplate = rgAccount.MasterTableView.Columns.FindByUniqueName("ReconciliationTemplate");
        if (oGridColumnReconciliationTemplate != null)
        {
            oGridColumnReconciliationTemplate.Visible = false;
        }
        GridColumn oGridColumnSubledger = rgAccount.MasterTableView.Columns.FindByUniqueName("Subledger");
        if (oGridColumnSubledger != null)
        {
            oGridColumnSubledger.Visible = false;
        }

        GridColumn oGridColumnPreparerDueDaysExport = rgAccount.MasterTableView.Columns.FindByUniqueName("PreparerDueDaysExport");
        if (oGridColumnPreparerDueDaysExport != null)
        {
            oGridColumnPreparerDueDaysExport.Visible = true;
        }
        GridColumn oGridColumnReviewerDueDaysExport = rgAccount.MasterTableView.Columns.FindByUniqueName("ReviewerDueDaysExport");
        if (oGridColumnReviewerDueDaysExport != null)
        {
            oGridColumnReviewerDueDaysExport.Visible = true;
        }
        GridColumn oGridColumnApproverDueDaysExport = rgAccount.MasterTableView.Columns.FindByUniqueName("ApproverDueDaysExport");
        if (oGridColumnApproverDueDaysExport != null)
        {
            oGridColumnApproverDueDaysExport.Visible = true;
        }

        GridColumn oGridColumnPreparerDueDays = rgAccount.MasterTableView.Columns.FindByUniqueName("PreparerDueDays");
        if (oGridColumnPreparerDueDays != null)
        {
            oGridColumnPreparerDueDays.Visible = false;
        }
        GridColumn oGridColumnReviewerDueDays = rgAccount.MasterTableView.Columns.FindByUniqueName("ReviewerDueDays");
        if (oGridColumnReviewerDueDays != null)
        {
            oGridColumnReviewerDueDays.Visible = false;
        }
        GridColumn oGridColumnApproverDueDays = rgAccount.MasterTableView.Columns.FindByUniqueName("ApproverDueDays");
        if (oGridColumnApproverDueDays != null)
        {
            oGridColumnApproverDueDays.Visible = false;
        }

    }

    protected void rgAccount_PreRender(object sender, EventArgs e)
    {
        rgAccount.GridLines = GridLines.None;
        if (isExportPDF)
        {
            GridItem headerItem = rgAccount.MasterTableView.GetItems(GridItemType.Header)[0];
            foreach (TableCell cell in headerItem.Cells)
            {
                cell.Style["text-align"] = "left";
            }

            GridItem[] dataItems = rgAccount.MasterTableView.GetItems(GridItemType.Item);
            foreach (GridItem item in dataItems)
            {
                foreach (TableCell cell in item.Cells)
                {
                    cell.Style[HtmlTextWriterStyle.TextAlign] = "left";
                }
            }

            GridItem[] dataAltItems = rgAccount.MasterTableView.GetItems(GridItemType.AlternatingItem);
            foreach (GridItem item in dataAltItems)
            {
                foreach (TableCell cell in item.Cells)
                {
                    cell.Style[HtmlTextWriterStyle.TextAlign] = "left";
                }
            }

            GridItem[] dataNestItems = rgAccount.MasterTableView.GetItems(GridItemType.NestedView);
            foreach (GridItem item in dataNestItems)
            {
                foreach (TableCell cell in item.Cells)
                {
                    cell.Style[HtmlTextWriterStyle.TextAlign] = "left";
                }
            }
        }
    }

    public void bindAllRecords()
    {
        object obj;
        //Vinay: Commented as all means all records, right now it was working only once
        //if (!isFetchAllRecords)
        //{
        obj = RaiseGrid_NeedDataSourceEventHandler(0);
        isFetchAllRecords = true;
        //}
        //else
        //{
        //    obj = RaiseGrid_NeedDataSourceEventHandler(10);
        //}
        if (obj != null)
        {
            IList objectCollection = (IList)obj;
            rgAccount.VirtualItemCount = objectCollection.Count;
            rgAccount.DataSource = objectCollection;
        }

    }
    public void unSetBindAllRecords()
    {
        rgAccount.CurrentPageIndex = Convert.ToInt32(ViewState["CurrentPageIndex"]);
        int defaultItemCount = Helper.GetDefaultChunkSize(rgAccount.PageSize);
        //int defaultItemCount = Convert.ToInt32(AppSettingHelper.GetAppSettingValue(AppSettingConstants.DEFAULT_CHUNK_SIZE));
        int pageIndex = rgAccount.CurrentPageIndex;
        int pageSize = rgAccount.PageSize;
        int count = (pageIndex / pageSize + 1) * defaultItemCount;

        object obj = RaiseGrid_NeedDataSourceEventHandler(count);
        if (obj != null)
        {
            IList objectCollection = (IList)obj;
            if (objectCollection.Count % defaultItemCount == 0)
                rgAccount.VirtualItemCount = objectCollection.Count + 1;
            else
                rgAccount.VirtualItemCount = objectCollection.Count;

            rgAccount.DataSource = objectCollection;
        }

    }

    protected void rgAccount_ItemDataBound(object sender, GridItemEventArgs e)
    {
        if (e.Item.ItemType == GridItemType.Item || e.Item.ItemType == GridItemType.AlternatingItem)
        {
            OrganizationalHierarchyInfo oOrganizationalHierarchyInfo = (OrganizationalHierarchyInfo)e.Item.DataItem;

            ExHyperLink hlKey2 = (ExHyperLink)e.Item.FindControl("hlKey2");
            ExHyperLink hlKey3 = (ExHyperLink)e.Item.FindControl("hlKey3");
            ExHyperLink hlKey4 = (ExHyperLink)e.Item.FindControl("hlKey4");
            ExHyperLink hlKey5 = (ExHyperLink)e.Item.FindControl("hlKey5");
            ExHyperLink hlKey6 = (ExHyperLink)e.Item.FindControl("hlKey6");
            ExHyperLink hlKey7 = (ExHyperLink)e.Item.FindControl("hlKey7");
            ExHyperLink hlKey8 = (ExHyperLink)e.Item.FindControl("hlKey8");
            ExHyperLink hlKey9 = (ExHyperLink)e.Item.FindControl("hlKey9");
            ExHyperLink hlFSCaption = (ExHyperLink)e.Item.FindControl("hlFSCaption");
            ExHyperLink hlAccountType = (ExHyperLink)e.Item.FindControl("hlAccountType");

            if (hlKey2 != null)
            {
                hlKey2.Text = oOrganizationalHierarchyInfo.Key2;
            }

            if (hlKey3 != null)
            {
                hlKey3.Text = oOrganizationalHierarchyInfo.Key3;
            }

            if (hlKey4 != null)
            {
                hlKey4.Text = oOrganizationalHierarchyInfo.Key4;
            }

            if (hlKey5 != null)
            {
                hlKey5.Text = oOrganizationalHierarchyInfo.Key5;
            }

            if (hlKey6 != null)
            {
                hlKey6.Text = oOrganizationalHierarchyInfo.Key6;
            }

            if (hlKey7 != null)
            {
                hlKey7.Text = oOrganizationalHierarchyInfo.Key7;
            }

            if (hlKey8 != null)
            {
                hlKey8.Text = oOrganizationalHierarchyInfo.Key8;
            }

            if (hlKey9 != null)
            {
                hlKey9.Text = oOrganizationalHierarchyInfo.Key9;
            }

            if (hlFSCaption != null)
            {
                hlFSCaption.Text = oOrganizationalHierarchyInfo.FSCaption;
            }

            if (hlAccountType != null)
            {
                if (oOrganizationalHierarchyInfo.AccountType != "0")
                    hlAccountType.Text = oOrganizationalHierarchyInfo.AccountType;
                else
                    hlAccountType.Text = "-";
            }
            ExHyperLink hlCompletedStatus = (ExHyperLink)e.Item.FindControl("hlCompletedStatus");
            ExHyperLink hlPendingStatus = (ExHyperLink)e.Item.FindControl("hlPendingStatus");
            ExHyperLink hlOverDueStatus = (ExHyperLink)e.Item.FindControl("hlOverDueStatus");
            ExHyperLink hlHiddenStatus = (ExHyperLink)e.Item.FindControl("hlHiddenStatus");
            ExHyperLink hlDeletedStatus = (ExHyperLink)e.Item.FindControl("hlDeletedStatus");

            if (hlCompletedStatus != null)
                hlCompletedStatus.Visible = ShowTaskStatusImage;
            if (hlPendingStatus != null)
                hlPendingStatus.Visible = ShowTaskStatusImage;
            if (hlOverDueStatus != null)
                hlOverDueStatus.Visible = ShowTaskStatusImage;
            if (hlHiddenStatus != null)
                hlHiddenStatus.Visible = ShowTaskStatusImage;
            if (hlDeletedStatus != null)
                hlDeletedStatus.Visible = ShowTaskStatusImage;

        }
        // Raise Event for Page to Handle it
        if (GridItemDataBound != null)
            GridItemDataBound(sender, e);
    }

    //protected void rgAccount_ColumnCreated(object sender, GridColumnCreatedEventArgs e)
    //{
    //    if (e.Column is GridGroupSplitterColumn)
    //    {
    //        GridGroupSplitterColumn gc = (GridGroupSplitterColumn)e.Column;
    //        gc.ExpandImageUrl = "~/App_Themes/SkyStemBlueBrown/Images/ExpandRow.gif";
    //        gc.CollapseImageUrl = "~/App_Themes/SkyStemBlueBrown/Images/CollapseRow.gif";
    //    }
    //}

    //public void rgAccount_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
    //{
    //    if (!e.IsFromDetailTable)
    //    {
    //        int defaultItemCount = Convert.ToInt32(AppSettingHelper.GetAppSettingValue(AppSettingConstants.DEFAULT_CHUNK_SIZE));
    //        int pageIndex = rgAccount.CurrentPageIndex;
    //        //int pageSize = rgAccount.PageSize;
    //        int pageSize = Convert.ToInt32(hdnNewPageSize.Value);
    //        int count;
    //        //if(pageIndex==0)
    //        //     count = (((1 * pageSize) / defaultItemCount) + 1) * defaultItemCount;
    //        //else
    //        count = ((((pageIndex + 1) * pageSize) / defaultItemCount) + 1) * defaultItemCount;

    //        object obj = RaiseGrid_NeedDataSourceEventHandler(count);
    //        if (obj != null)
    //        {
    //            IList objectCollection = (IList)obj;
    //            if (objectCollection.Count % defaultItemCount == 0)
    //                rgAccount.VirtualItemCount = objectCollection.Count + 1;
    //            else
    //                rgAccount.VirtualItemCount = objectCollection.Count;

    //            rgAccount.DataSource = objectCollection;
    //        }
    //    }
    //}
    public void rgAccount_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
    {
        if (!e.IsFromDetailTable)
        {
            if (BindSkipedRecords)
            {

                List<TaskHdrInfo> oDataSource = (List<TaskHdrInfo>)RaiseGrid_NeedDataSourceEventHandler(0);
                if (oDataSource != null)
                    rgAccount.VirtualItemCount = oDataSource.Count;
                if (!(isExportPDF || isExportExcel) && oDataSource != null)
                {
                    object objSkiped = oDataSource.Skip(rgAccount.CurrentPageIndex * rgAccount.PageSize).Take(rgAccount.PageSize).ToList();
                    rgAccount.DataSource = objSkiped;
                }
                else
                {
                    rgAccount.DataSource = oDataSource;
                }

            }
            else
            {
                // int defaultItemCount = Convert.ToInt32(AppSettingHelper.GetAppSettingValue(AppSettingConstants.DEFAULT_CHUNK_SIZE));

                int pageIndex = rgAccount.CurrentPageIndex;
                //int pageSize = rgAccount.PageSize;
                int pageSize = Convert.ToInt32(hdnNewPageSize.Value);
                int defaultItemCount = Helper.GetDefaultChunkSize(pageSize);
                int count;
                //if(pageIndex==0)
                //     count = (((1 * pageSize) / defaultItemCount) + 1) * defaultItemCount;
                //else
                count = ((((pageIndex + 1) * pageSize) / defaultItemCount) + 1) * defaultItemCount;

                object obj = RaiseGrid_NeedDataSourceEventHandler(count);
                if (obj != null)
                {
                    IList objectCollection = (IList)obj;
                    if (objectCollection.Count % defaultItemCount == 0)
                        rgAccount.VirtualItemCount = objectCollection.Count + 1;
                    else
                        rgAccount.VirtualItemCount = objectCollection.Count;

                    rgAccount.DataSource = objectCollection;
                }
            }

        }
    }


    protected void SkyStemARTGrid_SortCommand(object source, GridSortCommandEventArgs e)
    {
        //Added by Prafull on 25-Jan-2011 to retain CheckBox Status
        RaiseGrid_ColumnSortingEventHandler();

        GridHelper.HandleSortCommand(e);
        SessionHelper.ClearSession(SessionConstants.SEARCH_RESULTS_ACCOUNT);
        SessionHelper.ClearSession(SessionConstants.SEARCH_RESULTS_ACCOUNT_VIEWER);
        rgAccount.Rebind();



    }
    #endregion

    /// <summary>
    /// Show FSCaption and Account Type Cols
    /// </summary>
    public void ShowFSCaptionAndAccountType()
    {
        GridColumn oGridColumn = rgAccount.Columns.FindByUniqueNameSafe("FSCaption");
        if (oGridColumn != null)
        {
            // Check for Capability
            oGridColumn.Visible = true;
        }

        oGridColumn = rgAccount.Columns.FindByUniqueNameSafe("AccountType");
        if (oGridColumn != null)
        {
            // Check for Capability
            oGridColumn.Visible = true;
        }

    }

    public void ShowHideColumns()
    {
        GridHelper.ShowHideColumns(GRID_COLUMN_INDEX_KEY_START, rgAccount.MasterTableView, this.CompanyID, this.GridType, rgAccount.AllowCustomization);

    }
    public void ApplyGridGroupByExpression()
    {
        if (_GridGroupByExpression != null)
        {
            rgAccount.MasterTableView.GroupByExpressions.Add(_GridGroupByExpression);
            rgAccount.GroupHeaderItemStyle.CssClass = "groupRadGrid";
            rgAccount.GroupingSettings.CollapseTooltip = String.Format("{0}...", LanguageUtil.GetValue(1908));
            rgAccount.GroupingSettings.ExpandTooltip = String.Format("{0}...", LanguageUtil.GetValue(1260));
        }
        if (_TaskListGridGroupByExpression != null)
        {
            rgAccount.MasterTableView.GroupByExpressions.Add(_TaskListGridGroupByExpression);
            rgAccount.GroupHeaderItemStyle.CssClass = "groupRadGrid";
            rgAccount.GroupingSettings.CollapseTooltip = String.Format("{0}...", LanguageUtil.GetValue(1908));
            rgAccount.GroupingSettings.ExpandTooltip = String.Format("{0}...", LanguageUtil.GetValue(1260));
        }
        if (_TaskSubListGridGroupByExpression != null)
        {
            rgAccount.MasterTableView.GroupByExpressions.Add(_TaskSubListGridGroupByExpression);
            rgAccount.GroupHeaderItemStyle.CssClass = "groupRadGrid";
            rgAccount.GroupingSettings.CollapseTooltip = String.Format("{0}...", LanguageUtil.GetValue(1908));
            rgAccount.GroupingSettings.ExpandTooltip = String.Format("{0}...", LanguageUtil.GetValue(1260));
        }
    }

    public void BindGrid()
    {
        if (_CompanyID == null)
            throw new Exception(WebConstants.COMPANY_NOT_SPECIFIED);
        ShowHideColumns();
        ApplyGridGroupByExpression();
        if (this.ShowSerialNumberColumn)
        {
            GridColumn oGridColumn = rgAccount.Columns.FindByUniqueNameSafe("SerialNumberColumn");
            if (oGridColumn != null)
            {
                oGridColumn.Visible = true;
            }
        }
        rgAccount.DataBind();
    }


    //private static void HandleSortCommand(GridSortCommandEventArgs e)
    //{
    //    // Add Default Sort as Import Data, Desc
    //    GridSortExpression oGridSortExpression = new GridSortExpression();

    //    if (e.NewSortOrder == GridSortOrder.None)
    //    {
    //        oGridSortExpression.SortOrder = GridSortOrder.Ascending;
    //    }
    //    else
    //    {
    //        oGridSortExpression.SortOrder = e.NewSortOrder;
    //    }
    //    oGridSortExpression.FieldName = e.SortExpression;
    //    e.Item.OwnerTableView.SortExpressions.AddSortExpression(oGridSortExpression);
    //    e.Item.OwnerTableView.CurrentPageIndex = 0; // send to first page
    //    e.Canceled = true;
    //}

    protected void Page_PreRender(object sender, EventArgs e)
    {
        string scriptKey = "LoadGridCustomization";

        string url = "~/Pages/GridCustomization.aspx?" + QueryStringConstants.GRID_TYPE + "=" + this.GridType.ToString("d");
        string urlGridCustomization = ResolveClientUrlPath(url);
        // Render JS to Open the grid Customization Window, 
        StringBuilder script = new StringBuilder();
        ScriptHelper.AddJSStartTag(script);

        ////////script.Append("function LoadGridCustomizationPage()");
        ////////script.Append(System.Environment.NewLine);
        ////////script.Append("{");
        ////////script.Append(System.Environment.NewLine);
        ////////script.Append("OpenRadWindowForHyperlink('" + urlGridCustomization + "', 480, 600);");
        ////////script.Append(System.Environment.NewLine);
        //script.Append("var oWindow = window.radopen('GridCustomization.aspx?" + QueryStringConstants.GRID_TYPE + "=" + this.GridType.ToString("d") + "', 'GridCustomize');");
        //script.Append(System.Environment.NewLine);
        //script.Append("oWindow.SetSize(600, 480); ");
        //script.Append(System.Environment.NewLine);
        //script.Append("oWindow.Center(); ");
        //script.Append(System.Environment.NewLine);
        ////////script.Append("}");
        ////////script.Append(System.Environment.NewLine);

        string urlGridApplyFilter = "GridApplyFilter.aspx?" + QueryStringConstants.GRID_TYPE + "=" + this.GridType.ToString("d");
        script.Append("function LoadGridApplyFilterPage()");
        script.Append(System.Environment.NewLine);
        script.Append("{");
        script.Append(System.Environment.NewLine);
        script.Append("OpenRadWindowForHyperlink('" + urlGridApplyFilter + "', 480, 600);");
        script.Append(System.Environment.NewLine);
        //script.Append("var oWindow = window.radopen('GridApplyFilter.aspx?" + QueryStringConstants.GRID_TYPE + "=" + this.GridType.ToString("d") + "', 'GridApplyFilter');");
        //script.Append(System.Environment.NewLine);
        //script.Append("oWindow.SetSize(600, 480); ");
        //script.Append(System.Environment.NewLine);
        //script.Append("oWindow.Center(); ");
        //script.Append(System.Environment.NewLine);
        script.Append("}");
        script.Append(System.Environment.NewLine);

        ScriptHelper.AddJSEndTag(script);
        if (!this.Page.ClientScript.IsStartupScriptRegistered(this.GetType(), scriptKey))
        {
            this.Page.ClientScript.RegisterStartupScript(this.GetType(), scriptKey, script.ToString());
        }
    }
    protected override void OnInit(EventArgs e)
    {
        string url = "~/Pages/GridCustomization.aspx?" + QueryStringConstants.GRID_TYPE + "=" + this.GridType.ToString("d");
        string urlGridCustomization = ResolveClientUrlPath(url);
        rgAccount.GridCustomizeOnClientClick = string.Format("LoadGridCustomizationPage('{0}');return false;", urlGridCustomization);
        base.OnInit(e);
    }
    private void GetGridCustomizationScript()
    {
        StringBuilder osb = new StringBuilder();
        osb.Append(@" function LoadGridCustomizationPage(id){");
        //osb.Append(@" alert(id);");
        osb.Append(@" OpenRadWindowForHyperlink(id, 480, 800);");
        osb.Append(@"return false;}");
        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), rgAccount.ClientID + "Testing", osb.ToString(), true);
    }
    protected void rgAccount_ItemCreated(object sender, GridItemEventArgs e)
    {
        if (e.Item is GridPagerItem)
        {
            GridPagerItem gridPager = e.Item as GridPagerItem;
            DropDownList oRadComboBox = (DropDownList)gridPager.FindControl("ddlPageSize");
            if (rgAccount.AllowCustomPaging)
            {

                //oRadComboBox.Items.Add(new ListItem("10", "10"));
                //oRadComboBox.Items.Add(new ListItem("50", "50"));
                //oRadComboBox.Items.Add(new ListItem("100", "100"));
                //oRadComboBox.Items.Add(new ListItem("200", "200"));
                //oRadComboBox.Items.Add(new ListItem("300", "300"));
                //oRadComboBox.Items.Add(new ListItem("400", "400"));
                //oRadComboBox.Items.Add(new ListItem("500", "500"));
                //oRadComboBox.Items.Add(new RadComboBoxItem("Select All", rgAccount.VirtualItemCount.ToString()));
                GridHelper.BindPageSizeGrid(oRadComboBox);
                oRadComboBox.SelectedValue = hdnNewPageSize.Value.ToString();
                //oRadComboBox.Attributes.Add("onChange", "return ddlPageSize_SelectedIndexChanged('" + oRadComboBox.ClientID + "');");
                oRadComboBox.Attributes.Add("onChange", "return ddlPageSize_SelectedIndexChanged('" + oRadComboBox.ClientID + "' , '" + rgAccount.ClientID + "');");


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
        RaiseGridItemCreated(sender, e, isExportPDF, isExportExcel);

    }


    protected void rgAccount_PageIndexChanged(object source, GridPageChangedEventArgs e)
    {
        rgAccount.CurrentPageIndex = e.NewPageIndex;
        RaiseGrid_PageIndexChangedEventHandler();

    }
    protected void rgAccount_PageSizeChanged(object source, GridPageSizeChangedEventArgs e)
    {
        hdnNewPageSize.Value = e.NewPageSize.ToString();
        //if (hdnNewPageSize.Value == rgAccount.VirtualItemCount.ToString())
        //{
        //    isFetchAllRecords = false;
        //    bindAllRecords();
        //    hdnNewPageSize.Value = rgAccount.VirtualItemCount.ToString();

        //}

    }

    public string ResolveClientUrlPath(string relativeUrl)
    {
        string url;
        url = Page.ResolveClientUrl(relativeUrl);
        return url;
    }



}//end of class
