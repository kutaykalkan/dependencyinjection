using System;
using System.Collections;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using SkyStem.ART.Web.Classes;
using SkyStem.ART.Web.Utility;
using SkyStem.ART.Client.IServices;
using SkyStem.ART.Client.Exception;
using SkyStem.ART.Web.Data;
using SkyStem.ART.Client.Model;
using SkyStem.ART.Client.Model.Matching;
using System.Collections.Generic;
using SkyStem.Library.Controls.TelerikWebControls;
using SkyStem.Library.Controls.WebControls;
using Telerik.Web.UI;
using SkyStem.ART.Client.Data;
using SkyStem.Language.LanguageUtility;
using System.Text;
using SkyStem.Library.Controls.TelerikWebControls.Data;
public partial class Pages_Matching_CloseRecItem : PageBaseMatching
{
    long? _AccountID = 0;
    long? _GLDataID = 0;
    long? _MatchSetID = null;
    bool _IsMultiCurrencyEnabled = false;
    ARTEnums.MatchingType _MatchingType = ARTEnums.MatchingType.AccountMatching;
    MatchSetSubSetCombinationInfo _oMatchSetSubSetCombinationInfo;
    short _GLReconciliationItemInputRecordTypeID = 0;
    protected void Page_Init(object sender, EventArgs e)
    {
        MasterPageBase ompage = (MasterPageBase)this.Master.Master;
        ompage.ReconciliationPeriodChangedEventHandler += new EventHandler(ompage_ReconciliationPeriodChangedEventHandler);
        ucGLDataRecurringScheduleItemsGrid.GridItemDataBound += new GridItemEventHandler(ucGLDataRecurringScheduleItemsGrid_GridItemDataBound);
    }
    public void ompage_ReconciliationPeriodChangedEventHandler(object sender, EventArgs e)
    {
        PageLoad();
        GridHelper.SetRecordCount(rgMatchSetResult);
        GridHelper.SetRecordCount(ucGLDataRecItemGrid.Grid);
        GridHelper.SetRecordCount(ucGLDataRecurringScheduleItemsGrid.Grid);
        GridHelper.SetRecordCount(ucGLDataWriteOnOffGrid.Grid);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        SetPrivateVariables();
    }
    public bool IsMultiCurrencyActivated
    {
        get { return (bool)ViewState["IsMultiCurrencyActivated"]; }
        set { ViewState["IsMultiCurrencyActivated"] = value; }
    }
    private void PageLoad()
    {
        try
        {
            SetPrivateVariables();
            if (!Page.IsPostBack)
            {
                IsMultiCurrencyActivated = Helper.IsCapabilityActivatedForCurrentRecPeriod(ARTEnums.Capability.MultiCurrency);
                ucGLDataRecItemGrid.IsMultiCurrencyActivated = IsMultiCurrencyActivated;
                ucGLDataWriteOnOffGrid.IsMultiCurrencyActivated = IsMultiCurrencyActivated;
                ucGLDataRecurringScheduleItemsGrid.IsMultiCurrencyActivated = IsMultiCurrencyActivated;
                ViewState["NewPageSize"] = "10";
                ViewState["PageSizeResult"] = "10";
                MatchSetHdrInfo oMatchSetHdrInfo = MatchingHelper.GetMatchSetResults(_MatchSetID, _GLDataID, SessionHelper.CurrentReconciliationPeriodID, true);
                WebEnums.ReconciliationStatus? eRecStatus = (WebEnums.ReconciliationStatus?)Helper.GetReconciliationStatusByGLDataID(_GLDataID);
                this.FormMode = MatchingHelper.GetFormModeForMatching(WebEnums.ARTPages.CreateRecItem, _MatchingType, eRecStatus, this.GLDataID, oMatchSetHdrInfo);
                if (_oMatchSetSubSetCombinationInfo != null)
                    lblDataSourceName.Text = _oMatchSetSubSetCombinationInfo.MatchSetSubSetCombinationName;
                //this.FormMode = WebEnums.FormMode.Edit;
                Helper.SetPageTitle(this, 2471);
                Helper.SetBreadcrumbs(this, 2234, 2471);
                Helper.ShowInputRequirementSection(this, 2472);
                BindRecCategoryDDL();
                BindRecCategoryTypeDDL(Convert.ToInt32(ddlRecCategory.SelectedValue));
            }
        }
        catch (Exception ex)
        {
            Helper.ShowErrorMessage(this, ex);
        }
    }
    void ucGLDataRecurringScheduleItemsGrid_GridItemDataBound(object sender, GridItemEventArgs e)
    {
        short ArrSelectedControlID = 0;
        string[] ArrSelected = ddlRecCategoryType.SelectedValue.Split('~');
        if (ArrSelected.Length > 0)
            short.TryParse(ArrSelected[1], out ArrSelectedControlID);
        if (ArrSelectedControlID == (short)WebEnums.RecCategoryType.Amortizable_SupportingDetail_RecurringAmortizableSchedule)
        {
            ucGLDataRecurringScheduleItemsGrid.SetAmortizableGridHeaders(e);
        }
        else
        {
            ucGLDataRecurringScheduleItemsGrid.SetAccruableGridHeaders(e);
        }
    }
    /// <summary>
    /// Set Private Variables
    /// </summary>
    private void SetPrivateVariables()
    {
        if (Request.QueryString[QueryStringConstants.GLDATA_ID] != null)
            _GLDataID = Convert.ToInt64(Request.QueryString[QueryStringConstants.GLDATA_ID]);
        this._IsMultiCurrencyEnabled = Helper.IsCapabilityActivatedForCurrentRecPeriod(ARTEnums.Capability.MultiCurrency);
        _oMatchSetSubSetCombinationInfo = SessionHelper.CurrentMatchSetSubSetCombinationInfo;
        if (!String.IsNullOrEmpty(Request.QueryString[QueryStringConstants.MATCH_SET_ID]))
        {
            _MatchSetID = Convert.ToInt64(Request.QueryString[QueryStringConstants.MATCH_SET_ID]);
        }
        if (!String.IsNullOrEmpty(Request.QueryString[QueryStringConstants.ACCOUNT_ID]))
        {
            _AccountID = Convert.ToInt64(Request.QueryString[QueryStringConstants.ACCOUNT_ID]);
        }
        MasterPageBase oMasterPageBase = (MasterPageBase)this.Master.Master;
        MasterPageSettings oMasterPageSettings = new MasterPageSettings();
        oMasterPageSettings.EnableRoleSelection = false;
        oMasterPageSettings.EnableRecPeriodSelection = false;
        oMasterPageBase.SetMasterPageSettings(oMasterPageSettings);
    }
    public override string GetMenuKey()
    {
        return "CloseRecItem";
    }
    protected void ddlRecCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindRecCategoryTypeDDL(Convert.ToInt32(ddlRecCategory.SelectedValue));
        PanelScrollBars.Visible = false;
        ClearData();
        btnCloseRecItem.Enabled = false;
        btnSave1.Enabled = false;
    }
    protected void ddlRecCategoryType_SelectedIndexChanged(object sender, EventArgs e)
    {
        ViewState["NewPageSize"] = "10";
        PanelScrollBars.Visible = true;
        ClearData();
        string ArrSelectedControlID = "0";
        string[] ArrSelected = ddlRecCategoryType.SelectedValue.Split('~');
        if (ArrSelected.Length > 0)
            ArrSelectedControlID = ArrSelected[0];
        BindMatchSetResultItems();
        //  18 is the Recurring Accrual Schedule Category TypeID  AND 3 is the Recurring Amortizable Schedule Category TypeID
        if (ArrSelectedControlID == ((short)ARTEnums.RecItemControl.ItemInputAmortizableTemplate).ToString() || ArrSelectedControlID == ((short)ARTEnums.RecItemControl.ItemInputAccurableRecurring).ToString())
        {
            Sel.Value = "";
            BindRecurringScheduleItemsGrid();
            pnlRecurringScheduleItems.Visible = true;
            pnlGLAdjustments.Visible = false;
            pnlWriteOffOnItems.Visible = false;
            btnCloseRecItem.Enabled = true;
            btnSave1.Enabled = true;
        }
        else if (ArrSelectedControlID == ((short)ARTEnums.RecItemControl.ItemInputWriteOff).ToString())//  4 is the category Id for Variances/ Write offs
        {
            Sel.Value = "";
            BindWriteOffOnItemsGrid();
            pnlWriteOffOnItems.Visible = true;
            pnlGLAdjustments.Visible = false;
            pnlRecurringScheduleItems.Visible = false;
            btnCloseRecItem.Enabled = true;
            btnSave1.Enabled = true;
        }
        else if (ArrSelectedControlID == WebConstants.SELECT_ONE)
        {
            Sel.Value = "";
            ClearData();
            pnlGLAdjustments.Visible = false;
            pnlWriteOffOnItems.Visible = false;
            pnlRecurringScheduleItems.Visible = false;
            btnCloseRecItem.Enabled = false;
            btnSave1.Enabled = false;
        }
        else
        {
            Sel.Value = "";
            BindGLAdjustmentGrid();
            pnlGLAdjustments.Visible = true;
            pnlWriteOffOnItems.Visible = false;
            pnlRecurringScheduleItems.Visible = false;
            btnCloseRecItem.Enabled = true;
            btnSave1.Enabled = true;
        }
        // EnableDisableControls(this.FormMode);
    }
    /// <summary>
    /// clear GridI tems
    /// </summary>
    private void clearGridItems(ExRadGrid rg)
    {
        rg.DataSource = null;
        rg.DataBind();
    }
    /// <summary>
    /// Get Reconciliation Category List
    /// </summary>
    private List<ReconciliationCategoryMstInfo> GetReconciliationCategoryList()
    {

        List<ReconciliationCategoryMstInfo> oReconciliationCategoryMstInfoCollection = null;
        if (Session[this.AccountID.ToString()] == null)
        {
            int? RecTemplateID = null;
            IGLDataRecItem oGLDataRecItemClient = RemotingHelper.GetGLDataRecItemObject();
            oReconciliationCategoryMstInfoCollection = oGLDataRecItemClient.GetAllReconciliationCategory(SessionHelper.CurrentReconciliationPeriodID, this.AccountID, ref  RecTemplateID, Helper.GetAppUserInfo());
            Session[this.AccountID.ToString()] = oReconciliationCategoryMstInfoCollection;
            if (RecTemplateID.HasValue)
                hdnRecTemplateID.Value = RecTemplateID.Value.ToString();
        }
        else
        {
            oReconciliationCategoryMstInfoCollection = (List<ReconciliationCategoryMstInfo>)Session[this.AccountID.ToString()];
        }

        return oReconciliationCategoryMstInfoCollection;
    }
    /// <summary>
    /// Bind RecCategory Dropdown list
    /// </summary>
    private void BindRecCategoryDDL()
    {
        List<short> tempReconciliationCategoryID = new List<short>();
        ddlRecCategoryType.Items.Clear();
        List<ReconciliationCategoryMstInfo> oReconciliationCategoryMstInfoCollection = GetReconciliationCategoryList();
        for (int i = 0; i < oReconciliationCategoryMstInfoCollection.Count; i++)
        {
            if (!tempReconciliationCategoryID.Contains(oReconciliationCategoryMstInfoCollection[i].ReconciliationCategoryID.Value))
            {
                tempReconciliationCategoryID.Add(oReconciliationCategoryMstInfoCollection[i].ReconciliationCategoryID.Value);
                ddlRecCategory.Items.Add(new ListItem(LanguageUtil.GetValue(oReconciliationCategoryMstInfoCollection[i].ReconciliationCategoryLabelID.Value), oReconciliationCategoryMstInfoCollection[i].ReconciliationCategoryID.ToString()));
            }
        }
        ListControlHelper.AddListItemForSelectOne(ddlRecCategory);
    }
    /// <summary>
    /// Bind RecCategoryType Dropdown list
    /// </summary>
    private void BindRecCategoryTypeDDL(int ReconciliationCategoryID)
    {
        ddlRecCategoryType.Items.Clear();
        List<ReconciliationCategoryMstInfo> oReconciliationCategoryMstInfoCollection = GetReconciliationCategoryList();
        List<ReconciliationCategoryMstInfo> oReconciliationCategoryTypeCollection = null;
        oReconciliationCategoryTypeCollection = oReconciliationCategoryMstInfoCollection.FindAll(o => o.ReconciliationCategoryID.Value == ReconciliationCategoryID).ToList();
        for (int i = 0; i < oReconciliationCategoryTypeCollection.Count; i++)
        {
            //if(oReconciliationCategoryTypeCollection[i].ReconciliationCategoryTypeID.Value == 18) 
            // ddlRecCategoryType.Items.Add(new ListItem(LanguageUtil.GetValue(oReconciliationCategoryTypeCollection[i].ReconciliationCategoryTypeLabelID.Value), oReconciliationCategoryTypeCollection[i].RecItemControlID.ToString() + "~3" ));
            //else
            ddlRecCategoryType.Items.Add(new ListItem(LanguageUtil.GetValue(oReconciliationCategoryTypeCollection[i].ReconciliationCategoryTypeLabelID.Value), oReconciliationCategoryTypeCollection[i].RecItemControlID.ToString() + "~" + oReconciliationCategoryTypeCollection[i].ReconciliationCategoryTypeID.ToString()));
        }
        ListControlHelper.AddListItemForSelectOne(ddlRecCategoryType);
    }
    #region MatchSetResultItems
    /// <summary>
    /// Bind  Result Grid Items
    /// </summary>
    private void BindMatchSetResultItems()
    {
        CreateFilterInfo();
        CreateColumns();
        rgMatchSetResultBindGrid(0);
    }
    #endregion
    protected void btnCloseRecItem_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            string ArrSelectedControlID = "0";
            string[] ArrSelected = ddlRecCategoryType.SelectedValue.Split('~');
            if (ArrSelected.Length > 0)
                ArrSelectedControlID = ArrSelected[0];
            //  18 is the Recurring Accrual Schedule Category TypeID  AND 3 is the Recurring Amortizable Schedule Category TypeID
            if (ArrSelectedControlID == ((short)ARTEnums.RecItemControl.ItemInputAmortizableTemplate).ToString() || ArrSelectedControlID == ((short)ARTEnums.RecItemControl.ItemInputAccurableRecurring).ToString())
            {
                CloseRecurringScheduleItems();
            }
            else if (ArrSelectedControlID == ((short)ARTEnums.RecItemControl.ItemInputWriteOff).ToString())
            {
                CloseWriteOffOnRecItems();
            }
            else
            {
                CloseNormalRecItems();
            }
            if (Session[SessionConstants.PARENT_PAGE_URL] != null)
            {
                String Url = Session[SessionConstants.PARENT_PAGE_URL].ToString();
                Response.Redirect(Url, true);
            }
        }
    }
    protected void btnBackToWorkspace_Click(object sender, EventArgs e)
    {
        String Url = Request.Url.AbsoluteUri;
        string NewUrl;
        NewUrl = Url.Replace("CloseRecItem", "MatchingResults");
        Response.Redirect(NewUrl);
    }
    public void EnableDisableControls(WebEnums.FormMode eFormMode)
    {
        bool isEditMode = eFormMode == WebEnums.FormMode.Edit;
        btnCloseRecItem.Enabled = isEditMode;
        btnSave1.Enabled = isEditMode;
    }
    protected void btnSave1_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            string ArrSelectedControlID = "0";
            string[] ArrSelected = ddlRecCategoryType.SelectedValue.Split('~');
            if (ArrSelected.Length > 0)
                ArrSelectedControlID = ArrSelected[0];
            //  18 is the Recurring Accrual Schedule Category TypeID  AND 3 is the Recurring Amortizable Schedule Category TypeID
            if (ArrSelectedControlID == ((short)ARTEnums.RecItemControl.ItemInputAmortizableTemplate).ToString() || ArrSelectedControlID == ((short)ARTEnums.RecItemControl.ItemInputAccurableRecurring).ToString())
            {
                CloseRecurringScheduleItems();
            }
            else if (ArrSelectedControlID == ((short)ARTEnums.RecItemControl.ItemInputWriteOff).ToString())
            {
                CloseWriteOffOnRecItems();
            }
            else
            {
                CloseNormalRecItems();
            }
        }
    }
    /// <summary>
    /// Clear All data from grids
    /// </summary>
    public void ClearData()
    {
        SessionHelper.ClearDynamicFilterData("rgMatchSetResult");
        clearGridItems(rgMatchSetResult);
        MatchSetResultCloseRecItemFilteredDataTable = null;
    }
    #region GLAdjustmentClose
    /// <summary>
    /// Bind GLAdjustment Grid
    /// </summary>
    private void BindGLAdjustmentGrid()
    {
        this._GLReconciliationItemInputRecordTypeID = (short)WebEnums.RecordType.GLReconciliationItemInput;
        short SelectedCategoryTypeID = 0;
        string[] ArrSelected = ddlRecCategoryType.SelectedValue.Split('~');
        if (ArrSelected.Length > 0)
            short.TryParse(ArrSelected[1], out SelectedCategoryTypeID);
        List<GLDataRecItemInfo> _GLRecItemInfoCollection = null;
        IGLDataRecItem oGLRecItemInput = RemotingHelper.GetGLDataRecItemObject();
        _GLRecItemInfoCollection = oGLRecItemInput.GetRecItem(this._GLDataID.Value, SessionHelper.CurrentReconciliationPeriodID.Value, SelectedCategoryTypeID, this._GLReconciliationItemInputRecordTypeID, (short)ARTEnums.AccountAttribute.ReconciliationTemplate, Helper.GetAppUserInfo());
        List<GLDataRecItemInfo> oGLReconciliationItemInputInfoCollection = (from recItem in _GLRecItemInfoCollection
                                                                            where recItem.CloseDate == null
                                                                            select recItem).ToList();
        ucGLDataRecItemGrid.SetGLDataRecItemGridData(oGLReconciliationItemInputInfoCollection);
        ucGLDataRecItemGrid.LoadGridData();
    }
    /// <summary>
    /// Close GLAdjustment Items
    /// </summary>
    private void CloseNormalRecItems()
    {
        try
        {
            DateTime ClosetDate;
            DateTime.TryParse(calResolutionDate.Text, out ClosetDate);
            List<GLDataRecItemInfo> oGLDataRecItemInfoCollection;
            List<GLDataRecItemInfo> oGLDataRecItemInfoCollectionForSave;
            oGLDataRecItemInfoCollection = new List<GLDataRecItemInfo>();
            GLDataRecItemInfo oGLDataRecItemInfo = null;
            foreach (GridDataItem item in rgMatchSetResult.MasterTableView.GetSelectedItems())
            {
                oGLDataRecItemInfo = new GLDataRecItemInfo();
                oGLDataRecItemInfo.ExcelRowNumber = Convert.ToInt64(item.OwnerTableView.DataKeyValues[item.ItemIndex]["__ExcelRowNumber"]);
                oGLDataRecItemInfo.MatchSetMatchingSourceDataImportID = Convert.ToInt64(item.OwnerTableView.DataKeyValues[item.ItemIndex]["__MatchSetMatchingSourceDataImportID"]);
                if (_oMatchSetSubSetCombinationInfo != null)
                    oGLDataRecItemInfo.MatchSetSubSetCombinationID = _oMatchSetSubSetCombinationInfo.MatchSetSubSetCombinationID;
                oGLDataRecItemInfoCollection.Add(oGLDataRecItemInfo);
            }
            oGLDataRecItemInfoCollectionForSave = GetGLDataRecItemInfoCollectionForSave(oGLDataRecItemInfoCollection);
            IDataImport oDataImportClient = RemotingHelper.GetDataImportObject();
            int rowsAffected;
            MasterPageBase oMasterPageBase = (MasterPageBase)this.Master.Master;
            if (oGLDataRecItemInfoCollectionForSave.Count > 0)
            {
                oDataImportClient.CloseMatchingGLDataRecItem(oGLDataRecItemInfoCollectionForSave, ClosetDate, out rowsAffected, Helper.GetAppUserInfo());
                oMasterPageBase.ShowConfirmationMessage(2469);
                DataTable tblRecItem = (DataTable)Session[SessionConstants.MATCHING_MATCH_SET_RESULT_CREATE_REC_ITEM_DATA];
                UpdateSessionTable(oGLDataRecItemInfoCollectionForSave, tblRecItem);
                if (MatchSetResultCloseRecItemFilteredDataTable != null)
                    UpdateSessionTable(oGLDataRecItemInfoCollectionForSave, MatchSetResultCloseRecItemFilteredDataTable);
                BindGLAdjustmentGrid();
                rgMatchSetResultBindGrid(rgMatchSetResult.CurrentPageIndex);
            }
            else
            {
                oMasterPageBase.ShowConfirmationMessage(2013);
                rgMatchSetResultBindGrid(rgMatchSetResult.CurrentPageIndex);
            }
        }
        catch (Exception ex)
        {
            Helper.ShowErrorMessage(this, ex);
        }
    }
    /// <summary>
    ///Update Session Table for GLAdjustment Items
    /// </summary>
    private void UpdateSessionTable(List<GLDataRecItemInfo> oGLDataRecItemInfoCollection, DataTable tblRecItem)
    {
        for (int i = 0; i < oGLDataRecItemInfoCollection.Count; i++)
        {
            long excelRowNumber;
            long matchSetMatchingSourceDataImportID;
            string filterString = MatchSetResultConstants.MATCH_SET_RESULT_COLUMN_NAME_EXCEL_ROW_NUMBER + "={0} AND " +
                MatchSetResultConstants.MATCH_SET_RESULT_COLUMN_NAME_MATCH_SET_MATCHING_SOURCE_DATA_IMPORT_ID + "={1}";
            DataRow[] dr = null;
            if (long.TryParse(oGLDataRecItemInfoCollection[i].ExcelRowNumber.ToString(), out excelRowNumber) &&
                long.TryParse(oGLDataRecItemInfoCollection[i].MatchSetMatchingSourceDataImportID.ToString(), out matchSetMatchingSourceDataImportID))
            {
                dr = tblRecItem.Select(string.Format(filterString, excelRowNumber, matchSetMatchingSourceDataImportID));
                if (dr.Length > 0)
                {
                    //dr[0][MatchSetResultConstants.MATCH_SET_RESULT_COLUMN_NAME_REC_ITEM_NUMBER] = oGLDataRecItemInfoCollection[i].RecItemNumber;
                    if (oGLDataRecItemInfoCollection[i].CloseDate.HasValue)
                        dr[0][MatchSetResultConstants.MATCH_SET_RESULT_COLUMN_NAME_CLOSE_DATE] = oGLDataRecItemInfoCollection[i].CloseDate;
                }
            }
        }

    }
    /// <summary>
    /// Get GLDataRecItemInfo Collection For Saving
    /// </summary>
    private List<GLDataRecItemInfo> GetGLDataRecItemInfoCollectionForSave(List<GLDataRecItemInfo> oSourceListCollection)
    {
        List<GLDataRecItemInfo> oGLDataRecItemInfoCollectionForSave;
        oGLDataRecItemInfoCollectionForSave = new List<GLDataRecItemInfo>();
        short SelectedCategoryTypeID = 0;
        string[] ArrSelected = ddlRecCategoryType.SelectedValue.Split('~');
        if (ArrSelected.Length > 0)
            SelectedCategoryTypeID = Convert.ToInt16(ArrSelected[1]);
        List<GLDataRecItemInfo> oSelectedGLDataRecItemInfoCollection = ucGLDataRecItemGrid.SelectedGLDataRecItemInfoCollection();
        for (int j = 0; j < oSelectedGLDataRecItemInfoCollection.Count; j++)
        {
            for (int i = 0; i < oSourceListCollection.Count; i++)
            {
                GLDataRecItemInfo oGLDataRecItemInfo = new GLDataRecItemInfo();
                oGLDataRecItemInfo.ExcelRowNumber = oSourceListCollection[i].ExcelRowNumber;
                oGLDataRecItemInfo.MatchSetMatchingSourceDataImportID = oSourceListCollection[i].MatchSetMatchingSourceDataImportID;
                oGLDataRecItemInfo.MatchSetSubSetCombinationID = oSourceListCollection[i].MatchSetSubSetCombinationID;
                //oGLDataRecItemInfo.CloseDate = Convert.ToDateTime(calResolutionDate.Text);
                oGLDataRecItemInfo.GLDataRecItemID = oSelectedGLDataRecItemInfoCollection[j].GLDataRecItemID;
                oGLDataRecItemInfo.GLDataID = this._GLDataID;
                oGLDataRecItemInfo.ReconciliationCategoryID = Convert.ToInt16(ddlRecCategory.SelectedValue);
                oGLDataRecItemInfo.ReconciliationCategoryTypeID = SelectedCategoryTypeID;
                oGLDataRecItemInfo.RecItemNumber = oSelectedGLDataRecItemInfoCollection[j].RecItemNumber;
                oGLDataRecItemInfo.RevisedBy = SessionHelper.CurrentUserLoginID;
                oGLDataRecItemInfo.DateRevised = DateTime.Now;
                oGLDataRecItemInfo.GLDataRecItemTypeID = (short)ARTEnums.GLDataRecItemType.GLDataRecItem;
                oGLDataRecItemInfoCollectionForSave.Add(oGLDataRecItemInfo);
            }
        }

        return oGLDataRecItemInfoCollectionForSave;

    }
    #endregion
    #region WriteOffOnItemsClose
    /// <summary>
    /// Bind WriteOffOn Items Grid
    /// </summary>
    private void BindWriteOffOnItemsGrid()
    {
        this._GLReconciliationItemInputRecordTypeID = (short)WebEnums.RecordType.GLReconciliationItemInput;
        short SelectedCategoryTypeID = 0;
        string[] ArrSelected = ddlRecCategoryType.SelectedValue.Split('~');
        if (ArrSelected.Length > 0)
            short.TryParse(ArrSelected[1], out SelectedCategoryTypeID);
        List<GLDataWriteOnOffInfo> _GLDataWriteOnOffInfoCollection = null;
        IGLDataWriteOnOff oGLDataWriteOnOffClient = RemotingHelper.GetGLDataWriteOnOffObject();
        _GLDataWriteOnOffInfoCollection = oGLDataWriteOnOffClient.GetGLDataWriteOnOffInfoCollectionByGLDataID(_GLDataID, (short)ARTEnums.AccountAttribute.ReconciliationTemplate,Helper.GetAppUserInfo());
        List<GLDataWriteOnOffInfo> oOpenItemsGLDataWriteOnOffInfoCollection = (from recItem in _GLDataWriteOnOffInfoCollection
                                                                               where recItem.CloseDate == null
                                                                               select recItem).ToList();
        ucGLDataWriteOnOffGrid.SetGLDataWriteOnOffGridData(oOpenItemsGLDataWriteOnOffInfoCollection);
        ucGLDataWriteOnOffGrid.LoadGridData();
    }
    /// <summary>
    /// Close WriteOffOn RecItems
    /// </summary>
    private void CloseWriteOffOnRecItems()
    {
        try
        {
            DateTime ClosetDate;
            DateTime.TryParse(calResolutionDate.Text, out ClosetDate);
            List<GLDataWriteOnOffInfo> oGLDataWriteOnOffInfoCollection;
            List<GLDataWriteOnOffInfo> oGLDataWriteOnOffInfoCollectionForSave;
            oGLDataWriteOnOffInfoCollection = new List<GLDataWriteOnOffInfo>();
            GLDataWriteOnOffInfo oGLDataWriteOnOffInfo = null;
            foreach (GridDataItem item in rgMatchSetResult.MasterTableView.GetSelectedItems())
            {
                oGLDataWriteOnOffInfo = new GLDataWriteOnOffInfo();
                oGLDataWriteOnOffInfo.ExcelRowNumber = Convert.ToInt64(item.OwnerTableView.DataKeyValues[item.ItemIndex]["__ExcelRowNumber"]);
                oGLDataWriteOnOffInfo.MatchSetMatchingSourceDataImportID = Convert.ToInt64(item.OwnerTableView.DataKeyValues[item.ItemIndex]["__MatchSetMatchingSourceDataImportID"]);
                if (_oMatchSetSubSetCombinationInfo != null)
                    oGLDataWriteOnOffInfo.MatchSetSubSetCombinationID = _oMatchSetSubSetCombinationInfo.MatchSetSubSetCombinationID;
                oGLDataWriteOnOffInfoCollection.Add(oGLDataWriteOnOffInfo);
            }
            oGLDataWriteOnOffInfoCollectionForSave = GetGLDataWriteOnOffInfoCollectionForSave(oGLDataWriteOnOffInfoCollection);
            IDataImport oDataImportClient = RemotingHelper.GetDataImportObject();
            int rowsAffected;
            MasterPageBase oMasterPageBase = (MasterPageBase)this.Master.Master;
            if (oGLDataWriteOnOffInfoCollectionForSave.Count > 0)
            {
                oDataImportClient.CloseMatchingGLDataWriteOnOffItem(oGLDataWriteOnOffInfoCollectionForSave, ClosetDate, out rowsAffected, Helper.GetAppUserInfo());
                oMasterPageBase.ShowConfirmationMessage(2469);
                DataTable tblRecItem = (DataTable)Session[SessionConstants.MATCHING_MATCH_SET_RESULT_CREATE_REC_ITEM_DATA];
                UpdateSessionTableForWriteOffOn(oGLDataWriteOnOffInfoCollectionForSave, tblRecItem);
                if (MatchSetResultCloseRecItemFilteredDataTable != null)
                    UpdateSessionTableForWriteOffOn(oGLDataWriteOnOffInfoCollectionForSave, MatchSetResultCloseRecItemFilteredDataTable);
                BindWriteOffOnItemsGrid();
                rgMatchSetResultBindGrid(rgMatchSetResult.CurrentPageIndex);
            }
            else
            {
                oMasterPageBase.ShowConfirmationMessage(2013);
                rgMatchSetResultBindGrid(rgMatchSetResult.CurrentPageIndex);
            }
        }
        catch (Exception ex)
        {
            Helper.ShowErrorMessage(this, ex);
        }
    }
    /// <summary>
    ///Update Session Table for WriteOffOn
    /// </summary>
    private void UpdateSessionTableForWriteOffOn(List<GLDataWriteOnOffInfo> oGLDataWriteOnOffInfoCollection, DataTable tblRecItem)
    {
        for (int i = 0; i < oGLDataWriteOnOffInfoCollection.Count; i++)
        {
            long excelRowNumber;
            long matchSetMatchingSourceDataImportID;
            string filterString = MatchSetResultConstants.MATCH_SET_RESULT_COLUMN_NAME_EXCEL_ROW_NUMBER + "={0} AND " +
                MatchSetResultConstants.MATCH_SET_RESULT_COLUMN_NAME_MATCH_SET_MATCHING_SOURCE_DATA_IMPORT_ID + "={1}";
            DataRow[] dr = null;
            if (long.TryParse(oGLDataWriteOnOffInfoCollection[i].ExcelRowNumber.ToString(), out excelRowNumber) &&
                long.TryParse(oGLDataWriteOnOffInfoCollection[i].MatchSetMatchingSourceDataImportID.ToString(), out matchSetMatchingSourceDataImportID))
            {
                dr = tblRecItem.Select(string.Format(filterString, excelRowNumber, matchSetMatchingSourceDataImportID));
                if (dr.Length > 0)
                {
                    // dr[0][MatchSetResultConstants.MATCH_SET_RESULT_COLUMN_NAME_REC_ITEM_NUMBER] = oGLDataWriteOnOffInfoCollection[i].RecItemNumber;
                    if (oGLDataWriteOnOffInfoCollection[i].CloseDate.HasValue)
                        dr[0][MatchSetResultConstants.MATCH_SET_RESULT_COLUMN_NAME_CLOSE_DATE] = oGLDataWriteOnOffInfoCollection[i].CloseDate;
                }
            }
        }

    }
    /// <summary>
    /// Get GLDataWriteOnOffInfo Collection For Saving
    /// </summary>
    private List<GLDataWriteOnOffInfo> GetGLDataWriteOnOffInfoCollectionForSave(List<GLDataWriteOnOffInfo> oSourceListCollection)
    {
        List<GLDataWriteOnOffInfo> oGLDataWriteOnOffInfoCollectionForSave;
        oGLDataWriteOnOffInfoCollectionForSave = new List<GLDataWriteOnOffInfo>();
        short SelectedCategoryTypeID = 0;
        string[] ArrSelected = ddlRecCategoryType.SelectedValue.Split('~');
        if (ArrSelected.Length > 0)
            SelectedCategoryTypeID = Convert.ToInt16(ArrSelected[1]);
        List<GLDataWriteOnOffInfo> oGLDataWriteOnOffInfoCollection;
        oGLDataWriteOnOffInfoCollection = ucGLDataWriteOnOffGrid.SelectedGLDataWriteOnOffInfoCollection();
        for (int j = 0; j < oGLDataWriteOnOffInfoCollection.Count; j++)
        {
            for (int i = 0; i < oSourceListCollection.Count; i++)
            {
                GLDataWriteOnOffInfo oGLDataWriteOnOffInfo = new GLDataWriteOnOffInfo();
                oGLDataWriteOnOffInfo.ExcelRowNumber = oSourceListCollection[i].ExcelRowNumber;
                oGLDataWriteOnOffInfo.MatchSetMatchingSourceDataImportID = oSourceListCollection[i].MatchSetMatchingSourceDataImportID;
                oGLDataWriteOnOffInfo.MatchSetSubSetCombinationID = oSourceListCollection[i].MatchSetSubSetCombinationID;
                oGLDataWriteOnOffInfo.GLDataWriteOnOffID = oGLDataWriteOnOffInfoCollection[j].GLDataWriteOnOffID;
                oGLDataWriteOnOffInfo.GLDataID = this._GLDataID;
                oGLDataWriteOnOffInfo.ReconciliationCategoryID = Convert.ToInt16(ddlRecCategory.SelectedValue);
                oGLDataWriteOnOffInfo.ReconciliationCategoryTypeID = SelectedCategoryTypeID;
                oGLDataWriteOnOffInfo.RecItemNumber = oGLDataWriteOnOffInfoCollection[j].RecItemNumber;
                oGLDataWriteOnOffInfo.RevisedBy = SessionHelper.CurrentUserLoginID;
                oGLDataWriteOnOffInfo.DateRevised = DateTime.Now;
                oGLDataWriteOnOffInfo.GLDataRecItemTypeID = (short)ARTEnums.GLDataRecItemType.GLDataWriteOnOff;
                oGLDataWriteOnOffInfoCollectionForSave.Add(oGLDataWriteOnOffInfo);
            }
        }
        return oGLDataWriteOnOffInfoCollectionForSave;
    }
    #endregion
    #region RecurringScheduleItemsClose
    /// <summary>
    /// Bind RecurringSchedule Items Grid
    /// </summary>
    private void BindRecurringScheduleItemsGrid()
    {
        this._GLReconciliationItemInputRecordTypeID = (short)WebEnums.RecordType.GLReconciliationItemInput;
        short SelectedCategoryTypeID = 0;
        string[] ArrSelected = ddlRecCategoryType.SelectedValue.Split('~');
        if (ArrSelected.Length > 0)
            short.TryParse(ArrSelected[1], out SelectedCategoryTypeID);
        List<GLDataRecurringItemScheduleInfo> _GLDataRecurringItemScheduleInfoCollection = null;
        IGLDataRecItemSchedule oGLDataRecItemScheduleClient = RemotingHelper.GetGLDataRecItemScheduleObject();
        _GLDataRecurringItemScheduleInfoCollection = oGLDataRecItemScheduleClient.GetGLDataRecurringItemSchedule(_GLDataID, Helper.GetAppUserInfo());
        List<GLDataRecurringItemScheduleInfo> oOpenItemsGLDataRecurringItemScheduleInfoCollection = (from recItem in _GLDataRecurringItemScheduleInfoCollection
                                                                                                     where recItem.CloseDate == null
                                                                                                     select recItem).ToList();
        ucGLDataRecurringScheduleItemsGrid.SetGLDataRecurringItemScheduleItemGridData(oOpenItemsGLDataRecurringItemScheduleInfoCollection);
        ucGLDataRecurringScheduleItemsGrid.LoadGridData();
    }
    /// <summary>
    /// Close RecurringSchedule Items
    /// </summary>
    private void CloseRecurringScheduleItems()
    {
        try
        {
            DateTime ClosetDate;
            DateTime.TryParse(calResolutionDate.Text, out ClosetDate);
            List<GLDataRecurringItemScheduleInfo> oGLDataRecurringItemScheduleInfoCollection;
            List<GLDataRecurringItemScheduleInfo> oGLDataRecurringItemScheduleInfoCollectionForSave;
            oGLDataRecurringItemScheduleInfoCollection = new List<GLDataRecurringItemScheduleInfo>();
            GLDataRecurringItemScheduleInfo oGLDataRecurringItemScheduleInfo = null;
            foreach (GridDataItem item in rgMatchSetResult.MasterTableView.GetSelectedItems())
            {
                oGLDataRecurringItemScheduleInfo = new GLDataRecurringItemScheduleInfo();
                oGLDataRecurringItemScheduleInfo.ExcelRowNumber = Convert.ToInt64(item.OwnerTableView.DataKeyValues[item.ItemIndex]["__ExcelRowNumber"]);
                oGLDataRecurringItemScheduleInfo.MatchSetMatchingSourceDataImportID = Convert.ToInt64(item.OwnerTableView.DataKeyValues[item.ItemIndex]["__MatchSetMatchingSourceDataImportID"]);
                if (_oMatchSetSubSetCombinationInfo != null)
                    oGLDataRecurringItemScheduleInfo.MatchSetSubSetCombinationID = _oMatchSetSubSetCombinationInfo.MatchSetSubSetCombinationID;
                oGLDataRecurringItemScheduleInfoCollection.Add(oGLDataRecurringItemScheduleInfo);
            }
            oGLDataRecurringItemScheduleInfoCollectionForSave = GetGLDataRecurringItemScheduleInfoCollectionForSave(oGLDataRecurringItemScheduleInfoCollection);

            IDataImport oDataImportClient = RemotingHelper.GetDataImportObject();
            int rowsAffected;
            MasterPageBase oMasterPageBase = (MasterPageBase)this.Master.Master;

            if (oGLDataRecurringItemScheduleInfoCollectionForSave.Count > 0)
            {
                oDataImportClient.CloseMatchingRecurringScheduleItems(oGLDataRecurringItemScheduleInfoCollectionForSave, ClosetDate, out rowsAffected, Helper.GetAppUserInfo());
                oMasterPageBase.ShowConfirmationMessage(2469);
                DataTable tblRecItem = (DataTable)Session[SessionConstants.MATCHING_MATCH_SET_RESULT_CREATE_REC_ITEM_DATA];
                UpdateSessionTableForRecurringScheduleItems(oGLDataRecurringItemScheduleInfoCollectionForSave, tblRecItem);
                if (MatchSetResultCloseRecItemFilteredDataTable != null)
                    UpdateSessionTableForRecurringScheduleItems(oGLDataRecurringItemScheduleInfoCollectionForSave, MatchSetResultCloseRecItemFilteredDataTable);
                BindRecurringScheduleItemsGrid();
                rgMatchSetResultBindGrid(rgMatchSetResult.CurrentPageIndex);
            }
            else
            {
                oMasterPageBase.ShowConfirmationMessage(2013);
                rgMatchSetResultBindGrid(rgMatchSetResult.CurrentPageIndex);
            }
        }
        catch (Exception ex)
        {
            Helper.ShowErrorMessage(this, ex);
        }
    }
    /// <summary>
    ///Update Session Table for RecurringSchedule Items
    /// </summary>
    private void UpdateSessionTableForRecurringScheduleItems(List<GLDataRecurringItemScheduleInfo> oGLDataRecurringItemScheduleInfoCollection, DataTable tblRecItem)
    {
        for (int i = 0; i < oGLDataRecurringItemScheduleInfoCollection.Count; i++)
        {

            long excelRowNumber;
            long matchSetMatchingSourceDataImportID;
            string filterString = MatchSetResultConstants.MATCH_SET_RESULT_COLUMN_NAME_EXCEL_ROW_NUMBER + "={0} AND " +
                MatchSetResultConstants.MATCH_SET_RESULT_COLUMN_NAME_MATCH_SET_MATCHING_SOURCE_DATA_IMPORT_ID + "={1}";
            DataRow[] dr = null;
            if (long.TryParse(oGLDataRecurringItemScheduleInfoCollection[i].ExcelRowNumber.ToString(), out excelRowNumber) &&
                long.TryParse(oGLDataRecurringItemScheduleInfoCollection[i].MatchSetMatchingSourceDataImportID.ToString(), out matchSetMatchingSourceDataImportID))
            {
                dr = tblRecItem.Select(string.Format(filterString, excelRowNumber, matchSetMatchingSourceDataImportID));
                if (dr.Length > 0)
                {
                    // dr[0][MatchSetResultConstants.MATCH_SET_RESULT_COLUMN_NAME_REC_ITEM_NUMBER] = oGLDataWriteOnOffInfoCollection[i].RecItemNumber;
                    if (oGLDataRecurringItemScheduleInfoCollection[i].CloseDate.HasValue)
                        dr[0][MatchSetResultConstants.MATCH_SET_RESULT_COLUMN_NAME_CLOSE_DATE] = oGLDataRecurringItemScheduleInfoCollection[i].CloseDate;
                }
            }
        }

    }
    /// <summary>
    /// Get GLDataRecurringItemScheduleInfo Collection For Saving
    /// </summary>
    private List<GLDataRecurringItemScheduleInfo> GetGLDataRecurringItemScheduleInfoCollectionForSave(List<GLDataRecurringItemScheduleInfo> oSourceListCollection)
    {
        List<GLDataRecurringItemScheduleInfo> oGLDataRecurringItemScheduleInfoCollectionForSave;
        oGLDataRecurringItemScheduleInfoCollectionForSave = new List<GLDataRecurringItemScheduleInfo>();
        short SelectedCategoryTypeID = 0;
        string[] ArrSelected = ddlRecCategoryType.SelectedValue.Split('~');
        if (ArrSelected.Length > 0)
            SelectedCategoryTypeID = Convert.ToInt16(ArrSelected[1]);
        List<GLDataRecurringItemScheduleInfo> oGLDataRecurringItemScheduleInfoCollection;
        oGLDataRecurringItemScheduleInfoCollection = ucGLDataRecurringScheduleItemsGrid.SelectedGLDataRecurringItemScheduleInfoCollection();
        for (int j = 0; j < oGLDataRecurringItemScheduleInfoCollection.Count; j++)
        {
            for (int i = 0; i < oSourceListCollection.Count; i++)
            {
                GLDataRecurringItemScheduleInfo oGLDataRecurringItemScheduleInfo = new GLDataRecurringItemScheduleInfo();
                oGLDataRecurringItemScheduleInfo.ExcelRowNumber = oSourceListCollection[i].ExcelRowNumber;
                oGLDataRecurringItemScheduleInfo.MatchSetMatchingSourceDataImportID = oSourceListCollection[i].MatchSetMatchingSourceDataImportID;
                oGLDataRecurringItemScheduleInfo.MatchSetSubSetCombinationID = oSourceListCollection[i].MatchSetSubSetCombinationID;
                oGLDataRecurringItemScheduleInfo.GLDataRecurringItemScheduleID = oGLDataRecurringItemScheduleInfoCollection[j].GLDataRecurringItemScheduleID;
                oGLDataRecurringItemScheduleInfo.GLDataID = this._GLDataID;
                oGLDataRecurringItemScheduleInfo.RecCategoryTypeID = Convert.ToInt16(ddlRecCategory.SelectedValue);
                oGLDataRecurringItemScheduleInfo.ReconciliationCategoryTypeID = SelectedCategoryTypeID;
                oGLDataRecurringItemScheduleInfo.RecItemNumber = oGLDataRecurringItemScheduleInfoCollection[j].RecItemNumber;
                oGLDataRecurringItemScheduleInfo.RevisedBy = SessionHelper.CurrentUserLoginID;
                oGLDataRecurringItemScheduleInfo.DateRevised = DateTime.Now;
                oGLDataRecurringItemScheduleInfo.GLDataRecItemTypeID = (short)ARTEnums.GLDataRecItemType.GLDataRecurringItemSchedule;
                oGLDataRecurringItemScheduleInfoCollectionForSave.Add(oGLDataRecurringItemScheduleInfo);
            }
        }
        return oGLDataRecurringItemScheduleInfoCollectionForSave;
    }
    #endregion
    #region  ResultDetails
    /// <summary>
    /// Matching match set result create rec item data table
    /// </summary>
    public DataTable MatchSetResultCloseRecItemDataTable
    {
        get
        {
            if (MatchSetResultCloseRecItemFilteredDataTable != null)
                return MatchSetResultCloseRecItemFilteredDataTable;
            return (DataTable)Session[SessionConstants.MATCHING_MATCH_SET_RESULT_CREATE_REC_ITEM_DATA];
        }
    }
    /// <summary>
    ///Matching match set result Filtered Data Table
    /// </summary>
    public DataTable MatchSetResultCloseRecItemFilteredDataTable
    {
        get { return (DataTable)Session[SessionConstants.MATCHING_MATCH_SET_RESULT_CLOSE_REC_ITEM_DATA_FILTERED]; }
        set { Session[SessionConstants.MATCHING_MATCH_SET_RESULT_CLOSE_REC_ITEM_DATA_FILTERED] = value; }
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
    /// Create rgMatchSetResult Grid Columns
    /// </summary>
    private void CreateColumns()
    {
        try
        {
            if (MatchingConfigurationInfoList != null)
            {
                rgMatchSetResult.MasterTableView.DataKeyNames = MatchingHelper.GetDataKeyNamesForUnMatched();
                MatchingHelper.CreateGridColumns(rgMatchSetResult, MatchingConfigurationInfoList);
                MatchingHelper.CreateMatchingSourceNameColumn(rgMatchSetResult);
                MatchingHelper.CreateRecItemNumberColumn(rgMatchSetResult);
                MatchingHelper.CreateCloseDateColumn(rgMatchSetResult);
            }
        }
        catch (ARTException ex)
        {
            Helper.ShowErrorMessage(this, ex);
        }
    }
    /// <summary>
    /// Bind the  rgMatchSetResult Grid
    /// </summary>
    public void rgMatchSetResultBindGrid(int Pageindex)
    {
        try
        {
            rgMatchSetResultBindGridNeeddataSource(Pageindex);
            rgMatchSetResult.DataBind();
        }
        catch (Exception ex)
        {
            Helper.ShowErrorMessage(this, ex);
        }
    }
    /// <summary>
    /// Bind the  rgMatchSetResult Grid for NeeddataSource
    /// </summary>
    public void rgMatchSetResultBindGridNeeddataSource(int Pageindex)
    {
        try
        {
            CreateColumns();
            rgMatchSetResult.DataSource = MatchSetResultCloseRecItemDataTable;
            rgMatchSetResult.MasterTableView.CurrentPageIndex = Pageindex;
            if (MatchSetResultCloseRecItemDataTable != null)
                rgMatchSetResult.VirtualItemCount = MatchSetResultCloseRecItemDataTable.Rows.Count;
        }
        catch (Exception ex)
        {
            Helper.ShowErrorMessage(this, ex);
        }
    }
    /// <summary>
    /// Creates the filter info for rgMatchSetResult.
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
                        oFilterInfo.ColumnName = oMatchingConfigurationInfo.DisplayColumnName;
                        oFilterInfo.DataType = oMatchingConfigurationInfo.DataTypeID;
                        oFilterInfoList.Add(oFilterInfo);
                    }
                }
                oFilterInfo = new FilterInfo();
                oFilterInfo.ColumnID = MatchSetResultConstants.MATCH_SET_RESULT_COLUMN_ID_DATA_SOURCE_NAME;
                oFilterInfo.ColumnName = MatchSetResultConstants.MATCH_SET_RESULT_COLUMN_NAME_DATA_SOURCE_NAME;
                oFilterInfo.DataType = (short)WebEnums.DataType.String;
                oFilterInfoList.Add(oFilterInfo);
                SessionHelper.SetDynamicFilterColumns(oFilterInfoList, "rgMatchSetResult");
            }
        }
        catch (Exception ex)
        {
            Helper.ShowErrorMessage(this, ex);
        }
    }
    /// <summary>
    /// Applies the filter for rgMatchSetResult.
    /// </summary>
    public void ApplyFilter()
    {
        try
        {
            string whereClause = SessionHelper.GetDynamicFilterResultWhereClause("rgMatchSetResult");
            MatchSetResultCloseRecItemFilteredDataTable = null;
            if (!string.IsNullOrEmpty(whereClause) && MatchSetResultCloseRecItemDataTable != null && MatchSetResultCloseRecItemDataTable.Rows.Count > 0)
            {
                DataRow[] filterResult = MatchSetResultCloseRecItemDataTable.Select(whereClause);
                if (filterResult != null && filterResult.Length > 0)
                    MatchSetResultCloseRecItemFilteredDataTable = filterResult.CopyToDataTable();
                else
                    MatchSetResultCloseRecItemFilteredDataTable = MatchSetResultCloseRecItemDataTable.Clone();
            }
            rgMatchSetResultBindGrid(rgMatchSetResult.CurrentPageIndex);
        }
        catch (Exception ex)
        {
            Helper.ShowErrorMessage(this, ex);
        }
    }
    public void rgMatchSetResult_ItemCommand(object source, GridCommandEventArgs e)
    {
        if (e.CommandName == TelerikConstants.GridClearFilterCommandName)
        {
            SessionHelper.ClearDynamicFilterData("rgMatchSetResult");
            ApplyFilter();
        }
    }
    public void rgMatchSetResult_OnItemDataBound(object sender, GridItemEventArgs e)
    {
        if (e.Item.ItemType == GridItemType.Header)
            SessionHelper.ShowGridFilterIcon((PageBase)this.Page, rgMatchSetResult, e, "rgMatchSetResult");
        if (e.Item.ItemType == GridItemType.Item || e.Item.ItemType == GridItemType.AlternatingItem)
        {
            GridDataItem oGridDataItem = e.Item as GridDataItem;
            if (oGridDataItem != null)
            {
                CheckBox checkBox = (CheckBox)(oGridDataItem)["CheckboxSelectColumn"].Controls[0];
                DataRow dr = ((DataRowView)oGridDataItem.DataItem).Row;
                string recItemNumber = Convert.ToString(dr[MatchSetResultConstants.MATCH_SET_RESULT_COLUMN_NAME_REC_ITEM_NUMBER]);
                string CloseDate = Convert.ToString(dr[MatchSetResultConstants.MATCH_SET_RESULT_COLUMN_NAME_CLOSE_DATE]);
                checkBox.Enabled = (String.IsNullOrEmpty(recItemNumber) && String.IsNullOrEmpty(CloseDate));
            }
        }
    }
    protected void rgMatchSetResult_OnNeedDataSource(object sender, GridNeedDataSourceEventArgs e)
    {
        rgMatchSetResultBindGridNeeddataSource(rgMatchSetResult.CurrentPageIndex);
    }
    /// <summary>
    /// refresh page to Applies the filter for Grids.
    /// </summary>
    public override void RefreshPage(object sender, RefreshEventArgs args)
    {
        base.RefreshPage(sender, args);
        this.ApplyFilter();
        ucGLDataRecItemGrid.ApplyFilterGLDataRecItemsGrid();
        ucGLDataWriteOnOffGrid.ApplyFilterGLDataWriteOnOffsGrid();
        ucGLDataRecurringScheduleItemsGrid.ApplyFilterGLDataRecurringScheduleItemsGrid();
    }
    #endregion
    #region Grids Paging Events
  
    protected void rgMatchSetResult_PageSizeChanged(object source, GridPageSizeChangedEventArgs e)
    {
        ViewState["PageSizeResult"] = e.NewPageSize.ToString();
    }
    protected void rgMatchSetResult_ItemCreated(object sender, GridItemEventArgs e)
    {
        if (e.Item is GridPagerItem)
        {
            GridPagerItem gridPager = e.Item as GridPagerItem;
            DropDownList oRadComboBox = (DropDownList)gridPager.FindControl("ddlPageSize");
            if (rgMatchSetResult.AllowCustomPaging)
            {
                GridHelper.BindPageSizeGrid(oRadComboBox);
                oRadComboBox.SelectedValue = ViewState["PageSizeResult"].ToString();
                oRadComboBox.Attributes.Add("onChange", "return ddlPageSize_SelectedIndexChanged('" + oRadComboBox.ClientID + "' , '" + rgMatchSetResult.ClientID + "');");
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
    #endregion
}
