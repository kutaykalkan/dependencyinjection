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

using System.Collections.Generic;
using SkyStem.ART.Client.Model;
using SkyStem.ART.Web.Utility;
using SkyStem.Library.Controls.WebControls;
using SkyStem.ART.Web.Data;
using SkyStem.ART.Web.Classes;
using SkyStem.ART.Client.IServices;
using SkyStem.Language.LanguageUtility;
using SkyStem.ART.Client.Data;
using SkyStem.ART.Client.Exception;
using Telerik.Web.UI;
using SkyStem.Library.Controls.TelerikWebControls;
using SkyStem.ART.Client.Model.RecControlCheckList;

public partial class Pages_AccountViewer : PageBaseRecPeriod
{

    #region Variables & Constants
    const int GRID_COLUMN_INDEX_KEY_START = 0;

    private int _CompanyID;
    private short _RoleID;
    private int _ReconciliationPeriodID;
    private int _UserID;
    private bool _IsDualReviewEnabled;
    private bool _IsMaterialityEnabled;
    public bool _IsZeroBalanceEnabled = false;
    public bool _IsKeyAccountEnabled = false;
    public bool _IsRiskRatingEnabled = false;
    public bool _IsNetAccountEnabled = false;
    public bool _IsMultiCurrencyEnabled = false;
    public bool? _IsClearFilter;
    public int countDisabledCheckboxes = 0;
    string sessionKey = null;
    WebEnums.RecPeriodStatus eRecPeriodStatus;
    bool IsCertificationStarted = false;

    #endregion

    #region Properties
    List<GLDataHdrInfo> oGLDataHdrInfoCollection = null;
    PageSettings oPageSettings;

    #endregion

    #region Delegates & Events
    #endregion

    #region Page Events
    protected void Page_Init(object sender, EventArgs e)
    {
        this.PageID = WebEnums.ARTPages.AccountViewer;
        MasterPageBase ompage = (MasterPageBase)this.Master;
        ompage.ReconciliationPeriodChangedEventHandler += new EventHandler(ompage_ReconciliationPeriodChangedEventHandler);

        ucSkyStemARTGrid.Grid_NeedDataSourceEventHandler += new UserControls_SkyStemARTGrid.Grid_NeedDataSource(ucSkyStemARTGrid_Grid_NeedDataSourceEventHandler);
        ucSkyStemARTGrid.Grid_ClearFilterEventHandler += new UserControls_SkyStemARTGrid.Grid_ClearFilter(ucSkyStemARTGrid_Grid_ClearFilterEventHandler);
        ucSkyStemARTGrid.Grid.PagerStyle.PagerTextFormat = "Change page: {4}";
        ucSkyStemARTGrid.Grid.PreRender += new EventHandler(ucSkyStemARTGrid_Grid_PreRender);

        //Binding the PageIndex change Event to its handler
        ucSkyStemARTGrid.PageIndexChangedEvent += new UserControls_SkyStemARTGrid.Grid_PageIndexChanged(ucSkyStemARTGrid_PageIndexChangedEvent);
        //Binding the SortColumn Event to its handler
        ucSkyStemARTGrid.GridColumnSortingEvent += new UserControls_SkyStemARTGrid.Grid_ColumnSorting(ucSkyStemARTGrid_GridColumnSortingEvent);
        ucSkyStemARTGrid.GridRefreshEvent += new UserControls_SkyStemARTGrid.GridRefresh(ucSkyStemARTGrid_GridRefreshEvent);
        this.PageSettingLoadedEvent += new PageSettingLoaded(Pages_AccountViewer_PageSettingLoadedEvent);
        this.NeedPageSettingEvent += new NeedPageSetting(Pages_AccountViewer_NeedPageSettingEvent);

        ScriptManager.GetCurrent(this).EnablePartialRendering = false;
    }
    protected void Page_Load(object sender, EventArgs e)
    {

        // Check for Rec Period
        if (SessionHelper.CurrentReconciliationPeriodID == null)
        {
            Helper.RedirectToErrorPage(5000061, true);
        }
        Helper.SetPageTitle(this, 1187);
        ucSkyStemARTGrid.BasePageTitle = 1187;
        ucSkyStemARTGrid.HideColumnAccountMassBuklupd = false;
        sessionKey = SessionHelper.GetSessionKeyForGridFilter(ucSkyStemARTGrid.GridType);
        eRecPeriodStatus = CurrentRecProcessStatus.Value;
        IsCertificationStarted = CertificationHelper.IsCertificationStarted();
        //ucSkyStemARTGrid.Grid.VirtualItemCount = 15;
        SetPrivateVariableValue();
        if (this.IsPostBack && this.txtIsPostbackFromPopupScreen.Value == "1")
        {
            // repopulate grid as coming from Grid Filter Screens
            ompage_ReconciliationPeriodChangedEventHandler(null, null);
            this.txtIsPostbackFromPopupScreen.Value = "0";
        }
        ARTEnums.UserRole eCurrentRole = (ARTEnums.UserRole)System.Enum.Parse(typeof(ARTEnums.UserRole), this._RoleID.ToString());
        if (eCurrentRole == ARTEnums.UserRole.PREPARER || eCurrentRole == ARTEnums.UserRole.BACKUP_PREPARER)
            btnAccept.LabelID = 1377;
        else
            btnAccept.LabelID = 1481;
        //this.btnReopen.Visible = AccountViewerHelper.ShowHideReopenAccountbtn(_RoleID);

        //this.btnAccept.Enabled = false;
        //this.btnSubmit.Enabled = false;
        //this.btnReopen.Enabled = false;
        //this.btnReset.Enabled = false;

    }
    protected void Page_PreRender(object sender, EventArgs e)
    {
        RePopulateCheckBoxStates();
        ARTEnums.UserRole eCurrentRole = (ARTEnums.UserRole)System.Enum.Parse(typeof(ARTEnums.UserRole), this._RoleID.ToString());
        //for user other then PRA and Backups the submmit button shoud not be there
        if (!(eCurrentRole == ARTEnums.UserRole.PREPARER || eCurrentRole == ARTEnums.UserRole.BACKUP_PREPARER || eCurrentRole == ARTEnums.UserRole.REVIEWER || eCurrentRole == ARTEnums.UserRole.BACKUP_REVIEWER || eCurrentRole == ARTEnums.UserRole.APPROVER || eCurrentRole == ARTEnums.UserRole.BACKUP_APPROVER))
            ShowHideButtons(false);
        ShowHideDownloadAllRecsBtn();
        //btnCreateBinders.Visible = Helper.IsFeatureActivated(WebEnums.Feature.ERecBinders, SessionHelper.CurrentReconciliationPeriodID);
        //btnDownloadAll.Visible = Helper.IsFeatureActivated(WebEnums.Feature.DownloadAllRecs, SessionHelper.CurrentReconciliationPeriodID);
        //btnDownloadSelected.Visible = btnDownloadAll.Visible;
        if (
             CurrentRecProcessStatus.Value == WebEnums.RecPeriodStatus.Closed
            || CurrentRecProcessStatus.Value == WebEnums.RecPeriodStatus.NotStarted
            || CurrentRecProcessStatus.Value == WebEnums.RecPeriodStatus.Skipped
            || CertificationHelper.IsCertificationStarted()
            )
            ShowHideButtons(false);
    }
    PageSettings Pages_AccountViewer_NeedPageSettingEvent()
    {
        PageSettings oPageSettings = PageSettingHelper.GetPageSettings(WebEnums.ARTPages.AccountViewer);
        if (oPageSettings.GridSettingValues != null && oPageSettings.GridSettingValues.ContainsKey(ucSkyStemARTGrid.Grid.ClientID))
        {
            oPageSettings.GridSettingValues.Remove(ucSkyStemARTGrid.Grid.ClientID);
        }
        GridSettings oGridSettings = PageSettingHelper.GetGridSettings(ucSkyStemARTGrid.Grid, ARTEnums.Grid.AccountViewer);
        oPageSettings.GridSettingValues.Add(ucSkyStemARTGrid.Grid.ClientID, oGridSettings);
        oPageSettings.ShowSRAAsWell = chkShowSRAAsWell.Checked;
        return oPageSettings;
    }
    void Pages_AccountViewer_PageSettingLoadedEvent()
    {
        oPageSettings = this.PageSettings;
        if (oPageSettings != null && oPageSettings.GridSettingValues != null && oPageSettings.GridSettingValues.ContainsKey(ucSkyStemARTGrid.Grid.ClientID))
        {
            int? PageSize;
            PageSize = oPageSettings.GridSettingValues[ucSkyStemARTGrid.Grid.ClientID].PageSize;
            if (PageSize.HasValue)
            {
                ucSkyStemARTGrid.CustomPageSize = PageSize.Value.ToString();
            }
            PageSettingHelper.SetGridSettins(ucSkyStemARTGrid.Grid, oPageSettings.GridSettingValues[ucSkyStemARTGrid.Grid.ClientID]);

            if (sessionKey == null)
                sessionKey = SessionHelper.GetSessionKeyForGridFilter(ucSkyStemARTGrid.GridType);

            if (Session[sessionKey] == null && oPageSettings.GridSettingValues[ucSkyStemARTGrid.Grid.ClientID].oFilterCriteriaCollection != null && oPageSettings.GridSettingValues[ucSkyStemARTGrid.Grid.ClientID].oFilterCriteriaCollection.Count > 0)
                Session[sessionKey] = oPageSettings.GridSettingValues[ucSkyStemARTGrid.Grid.ClientID].oFilterCriteriaCollection;

        }
        if (oPageSettings != null)
        {
            chkShowSRAAsWell.Checked = oPageSettings.ShowSRAAsWell;
        }

    }
    #endregion

    #region Grid Events
    void ucSkyStemARTGrid_Grid_PreRender(object sender, EventArgs e)
    {
        foreach (GridDataItem oItem in ucSkyStemARTGrid.Grid.MasterTableView.Items)
        {
            int? netAccountID = Convert.ToInt32(oItem.GetDataKeyValue("NetAccountID"));
            if (netAccountID.GetValueOrDefault() > 0 && oItem.HasChildItems)
            {
                GridTableView oDetailTableView = oItem.ChildItem.NestedTableViews[0];
                GridHelper.ShowHideColumns(GRID_COLUMN_INDEX_KEY_START, oDetailTableView, this._CompanyID, ARTEnums.Grid.AccountViewer, ucSkyStemARTGrid.Grid.AllowCustomization);
            }
        }
    }
    protected void ucSkyStemARTGrid_GridItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {
        try
        {
            if (e.Item.ItemType == GridItemType.Header)
            {
                if (e.Item.OwnerTableView.Name == "SkyStemARTGridView")
                    AccountViewerHelper.ShowFilterIcon(e, ucSkyStemARTGrid.GridType);
            }
            if (e.Item.ItemType == Telerik.Web.UI.GridItemType.Item || e.Item.ItemType == Telerik.Web.UI.GridItemType.AlternatingItem)
            {
                GLDataHdrInfo oGLDataHdrInfo = (GLDataHdrInfo)e.Item.DataItem;

                AccountViewerHelper.BindCommonFields(e, oGLDataHdrInfo);
                List<GLDataHdrInfo> temp = (List<GLDataHdrInfo>)ucSkyStemARTGrid.DataSource;
                //TODO : Patching code for fix Checkbox selection button visibility.
                ARTEnums.UserRole eCurrentRole = (ARTEnums.UserRole)System.Enum.Parse(typeof(ARTEnums.UserRole), this._RoleID.ToString());
                switch (e.Item.OwnerTableView.Name)
                {
                    case "NetAccountDetails":
                        // Apoorv: Hack, because the BindCommonFields method above will set it to "Net Account"
                        Helper.SetTextForHyperlink(e.Item, "hlAccountNumber", oGLDataHdrInfo.AccountNumber);
                        break;
                    default:
                        AccountViewerHelper.BindMasterGridFields(e, oGLDataHdrInfo, eRecPeriodStatus, Sel, ARTEnums.Grid.AccountViewer, WebEnums.ARTPages.AccountViewer, ref countDisabledCheckboxes);
                        switch (AccountViewerHelper.GetFormMode(eRecPeriodStatus))
                        {
                            case WebEnums.FormMode.Edit:
                                // Commented by manoj all buttons will visible all time    
                                //if (eCurrentRole == ARTEnums.UserRole.PREPARER)
                                //{
                                //    //Checking here to confirm that submit button should activate when Rec Status is Reviewed.
                                //    if (oGLDataHdrInfo.ReconciliationStatusID == (short)SkyStem.ART.Web.Data.WebEnums.ReconciliationStatus.Prepared)
                                //    {
                                //        btnSubmit.Visible = true;
                                //    }
                                //    if (oGLDataHdrInfo.ReconciliationStatusID == (short)SkyStem.ART.Web.Data.WebEnums.ReconciliationStatus.PendingModificationPreparer)
                                //    {
                                //        btnAccept.Visible = true;
                                //    }
                                //}
                                //else if (eCurrentRole == ARTEnums.UserRole.REVIEWER)
                                //{
                                //    //Checking here to confirm that submit button should activate when Rec Status is Reviewed.
                                //    if (oGLDataHdrInfo.ReconciliationStatusID == (short)SkyStem.ART.Web.Data.WebEnums.ReconciliationStatus.Reviewed)
                                //    {
                                //        btnSubmit.Visible = true;
                                //    }
                                //    if (oGLDataHdrInfo.ReconciliationStatusID == (short)SkyStem.ART.Web.Data.WebEnums.ReconciliationStatus.PendingReview || oGLDataHdrInfo.ReconciliationStatusID == (short)SkyStem.ART.Web.Data.WebEnums.ReconciliationStatus.PendingModificationReviewer)
                                //    {
                                //        btnAccept.Visible = true;
                                //    }
                                //}
                                //else if (eCurrentRole == ARTEnums.UserRole.APPROVER)
                                //{
                                //    //Checking here to confirm that submit button should activate when Rec Status is Reviewed.
                                //    if (oGLDataHdrInfo.ReconciliationStatusID == (short)SkyStem.ART.Web.Data.WebEnums.ReconciliationStatus.Approved)
                                //    {
                                //        btnSubmit.Visible = true;
                                //    }
                                //    if (oGLDataHdrInfo.ReconciliationStatusID == (short)SkyStem.ART.Web.Data.WebEnums.ReconciliationStatus.PendingApproval)
                                //    {
                                //        btnAccept.Visible = true;
                                //    }
                                //}
                                //else 
                                if (eCurrentRole == ARTEnums.UserRole.SYSTEM_ADMIN)
                                {
                                    ShowHideButtons(false);
                                    this.btnReopen.Visible = true;
                                    this.btnReset.Visible = true;
                                    //Checking here to confirm that Reopen button should activate when Rec Status is Reconciled or SysReconciled .
                                    //if (oGLDataHdrInfo.ReconciliationStatusID == (short)SkyStem.ART.Web.Data.WebEnums.ReconciliationStatus.Reconciled || oGLDataHdrInfo.ReconciliationStatusID == (short)SkyStem.ART.Web.Data.WebEnums.ReconciliationStatus.SysReconciled)
                                    //{
                                    //    this.btnReopen.Visible = AccountViewerHelper.ShowHideReopenAccountbtn(_RoleID, eRecPeriodStatus, IsCertificationStarted);
                                    //}
                                    //if (oGLDataHdrInfo.ReconciliationStatusID == (short)SkyStem.ART.Web.Data.WebEnums.ReconciliationStatus.Approved || oGLDataHdrInfo.ReconciliationStatusID == (short)SkyStem.ART.Web.Data.WebEnums.ReconciliationStatus.PendingApproval
                                    //    || oGLDataHdrInfo.ReconciliationStatusID == (short)SkyStem.ART.Web.Data.WebEnums.ReconciliationStatus.InProgress || oGLDataHdrInfo.ReconciliationStatusID == (short)SkyStem.ART.Web.Data.WebEnums.ReconciliationStatus.PendingModificationPreparer
                                    //    || oGLDataHdrInfo.ReconciliationStatusID == (short)SkyStem.ART.Web.Data.WebEnums.ReconciliationStatus.PendingModificationReviewer || oGLDataHdrInfo.ReconciliationStatusID == (short)SkyStem.ART.Web.Data.WebEnums.ReconciliationStatus.PendingReview
                                    //    || oGLDataHdrInfo.ReconciliationStatusID == (short)SkyStem.ART.Web.Data.WebEnums.ReconciliationStatus.Reviewed || (oGLDataHdrInfo.IsSystemReconcilied.HasValue && oGLDataHdrInfo.IsSystemReconcilied == false && oGLDataHdrInfo.ReconciliationStatusID == (short)SkyStem.ART.Web.Data.WebEnums.ReconciliationStatus.Prepared)
                                    //    || oGLDataHdrInfo.ReconciliationStatusID == (short)SkyStem.ART.Web.Data.WebEnums.ReconciliationStatus.Reconciled || oGLDataHdrInfo.ReconciliationStatusID == (short)SkyStem.ART.Web.Data.WebEnums.ReconciliationStatus.SysReconciled

                                    //    )
                                    //{
                                    //    this.btnReset.Visible = AccountViewerHelper.ShowHideReopenAccountbtn(_RoleID, eRecPeriodStatus, IsCertificationStarted);
                                    //}
                                }
                                else if (eCurrentRole == ARTEnums.UserRole.PREPARER || eCurrentRole == ARTEnums.UserRole.REVIEWER || eCurrentRole == ARTEnums.UserRole.APPROVER ||
                                    eCurrentRole == ARTEnums.UserRole.BACKUP_PREPARER || eCurrentRole == ARTEnums.UserRole.BACKUP_REVIEWER || eCurrentRole == ARTEnums.UserRole.BACKUP_APPROVER)
                                {
                                    this.btnReopen.Visible = false;
                                    this.btnReset.Visible = false;
                                    ShowHideButtons(true);
                                }
                                else if (eCurrentRole == ARTEnums.UserRole.AUDIT)
                                {
                                    this.btnReopen.Visible = false;
                                    this.btnReset.Visible = false;
                                    ShowHideButtons(true);
                                }
                                ucSkyStemARTGrid.ShowSelectCheckBoxColum = true;
                                break;
                        }
                        if ((e.Item as GridDataItem)["ID"] != null)
                        {
                            (e.Item as GridDataItem)["ID"].Text = oGLDataHdrInfo.AccountID.ToString();
                        }

                        if ((e.Item as GridDataItem)["NetAccountID"] != null)
                        {
                            (e.Item as GridDataItem)["NetAccountID"].Text = oGLDataHdrInfo.NetAccountID.ToString();
                        }
                        // If the Row is not for Net Account then hide the Expand Collapse
                        if (oGLDataHdrInfo.NetAccountID == null || oGLDataHdrInfo.NetAccountID <= 0)
                        {
                            GridDataItem oItem = (GridDataItem)e.Item;
                            oItem["ExpandColumn"].Controls[0].Visible = false;
                        }
                        break;
                }

            }
        }
        catch (ARTException ex)
        {
            Helper.ShowErrorMessage(this, ex);
        }
        catch (Exception ex)
        {
            Helper.ShowErrorMessage(this, ex);
        }
    }
    protected object ucSkyStemARTGrid_Grid_NeedDataSourceEventHandler(int count)
    {
        List<GLDataHdrInfo> oGLDataHdrInfoCollection = null;
        try
        {
            oGLDataHdrInfoCollection = (List<GLDataHdrInfo>)HttpContext.Current.Session[SessionConstants.SEARCH_RESULTS_ACCOUNT_VIEWER];

            // int defaultItemCount = Convert.ToInt32(AppSettingHelper.GetAppSettingValue(AppSettingConstants.DEFAULT_CHUNK_SIZE));
            int defaultItemCount = Helper.GetDefaultChunkSize(ucSkyStemARTGrid.Grid.PageSize);

            if (oGLDataHdrInfoCollection == null || oGLDataHdrInfoCollection.Count <= count - defaultItemCount || count == 0)
            {
                IGLData oGLDataClient = RemotingHelper.GetGLDataObject();
                string sortExpression = "NetAccountID";
                string sortDirection = "ASC";

                if (ucSkyStemARTGrid.Grid.MasterTableView.SortExpressions != null && ucSkyStemARTGrid.Grid.MasterTableView.SortExpressions.Count > 0)
                {
                    sortExpression = ucSkyStemARTGrid.Grid.MasterTableView.SortExpressions[0].FieldName;
                    sortDirection = ucSkyStemARTGrid.Grid.MasterTableView.SortExpressions[0].SortOrderAsString();
                }

                List<FilterCriteria> oFilterCriteriaCollection = (List<FilterCriteria>)Session[sessionKey];

                oGLDataHdrInfoCollection = oGLDataClient.SelectGLDataAndAccountInfoByUserID(oFilterCriteriaCollection,
                this._ReconciliationPeriodID, this._CompanyID, this._UserID, this._RoleID, this._IsDualReviewEnabled, this._IsMaterialityEnabled,
                (short)ARTEnums.AccountAttribute.Preparer, (short)ARTEnums.AccountAttribute.Reviewer, (short)ARTEnums.AccountAttribute.Approver,
                (short)ARTEnums.UserRole.PREPARER, (short)ARTEnums.UserRole.REVIEWER, (short)ARTEnums.UserRole.APPROVER,
                (short)ARTEnums.UserRole.SYSTEM_ADMIN, (short)ARTEnums.UserRole.CEO_CFO, (short)ARTEnums.UserRole.SKYSTEM_ADMIN
                , chkShowSRAAsWell.Checked, count, Helper.GetAccountAttributeIDCollection(WebEnums.AccountPages.AccountViewer),
                SessionHelper.GetUserLanguage(), SessionHelper.GetBusinessEntityID(), AppSettingHelper.GetDefaultLanguageID(),
                sortExpression, sortDirection, Helper.GetAppUserInfo());

                HttpContext.Current.Session[SessionConstants.SEARCH_RESULTS_ACCOUNT_VIEWER] = oGLDataHdrInfoCollection;
            }

        }
        catch (ARTException ex)
        {
            Helper.ShowErrorMessage(this, ex);
        }
        catch (Exception ex)
        {
            Helper.ShowErrorMessage(this, ex);
        }

        return oGLDataHdrInfoCollection;
    }
    protected void ucSkyStemARTGrid_GridDetailTableDataBind(object source, GridDetailTableDataBindEventArgs e)
    {
        try
        {
            GridDataItem oGridItem = e.DetailTableView.ParentItem;
            int? netAccountID = Convert.ToInt32(oGridItem.GetDataKeyValue("NetAccountID"));

            if (netAccountID > 0)
            {
                e.DetailTableView.Visible = true;
                if (oGridItem.HasChildItems)
                    oGridItem.ChildItem.Visible = true;
            }
            else
            {
                e.DetailTableView.Visible = false;
                if (oGridItem.HasChildItems)
                    oGridItem.ChildItem.Visible = false;
            }
            switch (e.DetailTableView.Name)
            {
                case "NetAccountDetails":
                    /*
                     * 1. Show Hide Columns, based on Org Hierarchy
                     * 2. Show Hide Columns, based on Grid Prefernce
                     * 3. Fetch the Data for NetAccounts
                     * 4. Load Data Source
                     */

                    GridHelper.ShowHideColumns(GRID_COLUMN_INDEX_KEY_START, e.DetailTableView, this._CompanyID, ARTEnums.Grid.AccountViewer, ucSkyStemARTGrid.Grid.AllowCustomization);

                    e.DetailTableView.DataSource = AccountViewerHelper.GetAccountInfoForNetAccount(netAccountID);
                    break;
            }
        }
        catch (ARTException ex)
        {
            Helper.ShowErrorMessage(this, ex);
        }
        catch (Exception ex)
        {
            Helper.ShowErrorMessage(this, ex);
        }
    }
    protected void ucSkyStemARTGrid_PageIndexChangedEvent()
    {
        Sel.Value = string.Empty;
        SaveCheckBoxStates();
        // ShowHideButtons(true);
        this.btnReopen.Visible = false;
        this.btnReset.Visible = false;

    }
    void ucSkyStemARTGrid_GridColumnSortingEvent()
    {
        SaveCheckBoxStates();

    }
    void ucSkyStemARTGrid_GridRefreshEvent()
    {
        PopulateAccountsGrid();
    }
    protected void ucSkyStemARTGrid_Grid_ClearFilterEventHandler()
    {
        SessionHelper.ClearGridFilterDataFromSession(ucSkyStemARTGrid.GridType);
        this.SavePageSettings();
        PopulateAccountsGrid();
    }
    #endregion

    #region Other Events
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                List<long> oAccountIDCollection = new List<long>();
                List<int> oNetAccountIDCollection = new List<int>();

                IGLData oGLDataClient = RemotingHelper.GetGLDataObject();
                bool result = false;
                List<long> oGLDataIDCollection = new List<long>();
                if (this._RoleID == (short)ARTEnums.UserRole.PREPARER || this._RoleID == (short)ARTEnums.UserRole.BACKUP_PREPARER)
                {
                    oGLDataIDCollection = GetGLDataIDCollection((short)WebEnums.ReconciliationStatus.Prepared);
                    if (oGLDataIDCollection.Count > 0)
                    {
                        GetSelectedAccountIdCollection(oGLDataIDCollection, out  oAccountIDCollection, out  oNetAccountIDCollection);
                        //result = oGLDataClient.SaveGLDataReconciliationStatus(oAccountIDCollection, oNetAccountIDCollection, this._ReconciliationPeriodID, (short)WebEnums.ReconciliationStatus.PendingReview, SessionHelper.CurrentUserLoginID, DateTime.Now);
                        result = oGLDataClient.SaveGLDataReconciliationStatus(oGLDataIDCollection, this._ReconciliationPeriodID, (short)WebEnums.ReconciliationStatus.PendingReview, SessionHelper.CurrentUserLoginID, DateTime.Now, (short)ARTEnums.ActionType.UpdateRecFromUI, (short)ARTEnums.ChangeSource.GLStatusChange, Helper.GetAppUserInfo());
                        AlertHelper.RaiseAlert(WebEnums.Alert.YouHaveXAccountsPendingReview,
                            SessionHelper.CurrentReconciliationPeriodID, SessionHelper.CurrentReconciliationPeriodEndDate,
                            oAccountIDCollection, oNetAccountIDCollection, this._RoleID, null);
                    }
                }
                else if (this._RoleID == (short)ARTEnums.UserRole.REVIEWER || this._RoleID == (short)ARTEnums.UserRole.BACKUP_REVIEWER)
                {
                    oGLDataIDCollection = GetGLDataIDCollection((short)WebEnums.ReconciliationStatus.Reviewed);
                    if (oGLDataIDCollection.Count > 0)
                    {
                        GetSelectedAccountIdCollection(oGLDataIDCollection, out  oAccountIDCollection, out  oNetAccountIDCollection);
                        if (this._IsDualReviewEnabled)
                        {
                            //result = oGLDataClient.SaveGLDataReconciliationStatus(oAccountIDCollection, oNetAccountIDCollection, this._ReconciliationPeriodID, (short)WebEnums.ReconciliationStatus.PendingApproval, SessionHelper.CurrentUserLoginID, DateTime.Now);
                            result = oGLDataClient.SaveGLDataReconciliationStatus(oGLDataIDCollection, this._ReconciliationPeriodID, (short)WebEnums.ReconciliationStatus.PendingApproval, SessionHelper.CurrentUserLoginID, DateTime.Now, (short)ARTEnums.ActionType.UpdateRecFromUI, (short)ARTEnums.ChangeSource.GLStatusChange, Helper.GetAppUserInfo());
                            AlertHelper.RaiseAlert(WebEnums.Alert.YouHaveXAccountsPendingApproval,
                                SessionHelper.CurrentReconciliationPeriodID, SessionHelper.CurrentReconciliationPeriodEndDate,
                                oAccountIDCollection, oNetAccountIDCollection, this._RoleID, null);
                        }
                        else
                        {
                            //result = oGLDataClient.SaveGLDataReconciliationStatus(oAccountIDCollection, oNetAccountIDCollection, this._ReconciliationPeriodID, (short)WebEnums.ReconciliationStatus.Reconciled, SessionHelper.CurrentUserLoginID, DateTime.Now);
                            result = oGLDataClient.SaveGLDataReconciliationStatus(oGLDataIDCollection, this._ReconciliationPeriodID, (short)WebEnums.ReconciliationStatus.Reconciled, SessionHelper.CurrentUserLoginID, DateTime.Now, (short)ARTEnums.ActionType.UpdateRecFromUI, (short)ARTEnums.ChangeSource.GLStatusChange, Helper.GetAppUserInfo());
                        }
                    }
                }
                else if (this._RoleID == (short)ARTEnums.UserRole.APPROVER || this._RoleID == (short)ARTEnums.UserRole.BACKUP_APPROVER)
                {
                    oGLDataIDCollection = GetGLDataIDCollection((short)WebEnums.ReconciliationStatus.Approved);
                    if (oGLDataIDCollection.Count > 0)
                    {
                        //result = oGLDataClient.SaveGLDataReconciliationStatus(oAccountIDCollection, oNetAccountIDCollection, this._ReconciliationPeriodID, (short)WebEnums.ReconciliationStatus.Reconciled, SessionHelper.CurrentUserLoginID, DateTime.Now);
                        result = oGLDataClient.SaveGLDataReconciliationStatus(oGLDataIDCollection, this._ReconciliationPeriodID, (short)WebEnums.ReconciliationStatus.Reconciled, SessionHelper.CurrentUserLoginID, DateTime.Now, (short)ARTEnums.ActionType.UpdateRecFromUI, (short)ARTEnums.ChangeSource.GLStatusChange, Helper.GetAppUserInfo());
                    }
                }
                if (result)
                    ClearCheckedItemViewState();
                MasterPageBase oMasterPageBase = (MasterPageBase)this.Master;
                oMasterPageBase.ShowConfirmationMessage(1740);
                PopulateAccountsGrid();
                PopulatePercentCompleteLabel();
                CertificationHelper.NotifyUsersToStartCertification();
                MyHiddenField.Value = "";
            }
        }
        catch (ARTException ex)
        {
            Helper.ShowErrorMessage(this, ex);
        }
        catch (Exception ex)
        {
            Helper.ShowErrorMessage(this, ex);
        }
    }
    protected void btnAccept_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {

                IGLData oGLDataClient = RemotingHelper.GetGLDataObject();
                bool result = false;
                List<long> oGLDataIDCollection = new List<long>();
                if (this._RoleID == (short)ARTEnums.UserRole.PREPARER
                    || this._RoleID == (short)ARTEnums.UserRole.BACKUP_PREPARER)
                {
                    oGLDataIDCollection = GetGLDataIDCollection((short)WebEnums.ReconciliationStatus.PendingModificationPreparer);
                    if (oGLDataIDCollection.Count > 0)
                    {
                        result = oGLDataClient.SaveGLDataReconciliationStatus(oGLDataIDCollection, this._ReconciliationPeriodID, (short)WebEnums.ReconciliationStatus.Prepared, SessionHelper.CurrentUserLoginID, DateTime.Now, (short)ARTEnums.ActionType.UpdateRecFromUI, (short)ARTEnums.ChangeSource.GLStatusChange, Helper.GetAppUserInfo());

                    }
                }
                else if (this._RoleID == (short)ARTEnums.UserRole.REVIEWER
                    || this._RoleID == (short)ARTEnums.UserRole.BACKUP_REVIEWER)
                {
                    oGLDataIDCollection = GetGLDataIDCollection((short)WebEnums.ReconciliationStatus.PendingReview);
                    oGLDataIDCollection.InsertRange(oGLDataIDCollection.Count, GetGLDataIDCollection((short)WebEnums.ReconciliationStatus.PendingModificationReviewer));
                    if (oGLDataIDCollection.Count > 0)
                    {
                        result = oGLDataClient.SaveGLDataReconciliationStatus(oGLDataIDCollection, this._ReconciliationPeriodID, (short)WebEnums.ReconciliationStatus.Reviewed, SessionHelper.CurrentUserLoginID, DateTime.Now, (short)ARTEnums.ActionType.UpdateRecFromUI, (short)ARTEnums.ChangeSource.GLStatusChange, Helper.GetAppUserInfo());

                    }
                }
                else if (this._RoleID == (short)ARTEnums.UserRole.APPROVER
                    || this._RoleID == (short)ARTEnums.UserRole.BACKUP_APPROVER)
                {
                    oGLDataIDCollection = GetGLDataIDCollection((short)WebEnums.ReconciliationStatus.PendingApproval);
                    if (oGLDataIDCollection.Count > 0)
                    {
                        result = oGLDataClient.SaveGLDataReconciliationStatus(oGLDataIDCollection, this._ReconciliationPeriodID, (short)WebEnums.ReconciliationStatus.Approved, SessionHelper.CurrentUserLoginID, DateTime.Now, (short)ARTEnums.ActionType.UpdateRecFromUI, (short)ARTEnums.ChangeSource.GLStatusChange, Helper.GetAppUserInfo());
                    }
                }
                if (result == true)
                {
                    ClearCheckedItemViewState();
                    MasterPageBase oMasterPageBase = (MasterPageBase)this.Master;
                    oMasterPageBase.ShowConfirmationMessage(1740);
                    PopulateAccountsGrid();
                    PopulatePercentCompleteLabel();
                    MyHiddenField.Value = "";
                }
                else
                {
                    ClearCheckedItemViewState();
                    MasterPageBase oMasterPageBase = (MasterPageBase)this.Master;
                    Helper.ShowErrorMessageWithNoBullet(this, LanguageUtil.GetValue(2854));
                    PopulateAccountsGrid();
                    PopulatePercentCompleteLabel();
                    MyHiddenField.Value = "";
                }
            }
        }
        catch (ARTException ex)
        {
            Helper.ShowErrorMessage(this, ex);
        }
        catch (Exception ex)
        {
            Helper.ShowErrorMessage(this, ex);
        }
    }
    protected void btnReopen_Click(object sender, EventArgs e)
    {
        // Moved the logic to RadAjaxManager handler after getting confirmation

        if (Page.IsValid)
        {
            List<long> oGLDataIDCollection = new List<long>();
            oGLDataIDCollection = GetGLDataIDCollection((short)WebEnums.ReconciliationStatus.Reconciled);
            if (this.chkShowSRAAsWell.Checked)
                oGLDataIDCollection.InsertRange(oGLDataIDCollection.Count, GetGLDataIDCollection((short)WebEnums.ReconciliationStatus.SysReconciled));
            if (oGLDataIDCollection.Count > 0)
            {
                IGLData oGLDataClient = RemotingHelper.GetGLDataObject();
                oGLDataClient.UpdateReOpenAccount(oGLDataIDCollection, SessionHelper.CurrentUserLoginID, DateTime.Now, (short)ARTEnums.ActionType.ReopenReconciliationFromUI, (short)ARTEnums.ChangeSource.ReopenReconciliation, Helper.GetAppUserInfo());
                MasterPageBase oMasterPageBase = (MasterPageBase)this.Master;
                oMasterPageBase.ShowConfirmationMessage(1740);
                PopulateAccountsGrid();
                PopulatePercentCompleteLabel();
            }
            MyHiddenField.Value = "";
        }
    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        // Moved the logic to RadAjaxManager handler after getting confirmation

        if (Page.IsValid)
        {
            List<long> oGLDataIDCollection = new List<long>();
            List<short> ReconciliationStatusIDList = new List<short>();
            ReconciliationStatusIDList.Add((short)WebEnums.ReconciliationStatus.Reconciled);
            ReconciliationStatusIDList.Add((short)WebEnums.ReconciliationStatus.InProgress);
            ReconciliationStatusIDList.Add((short)WebEnums.ReconciliationStatus.Approved);
            ReconciliationStatusIDList.Add((short)WebEnums.ReconciliationStatus.PendingApproval);
            ReconciliationStatusIDList.Add((short)WebEnums.ReconciliationStatus.PendingModificationPreparer);
            ReconciliationStatusIDList.Add((short)WebEnums.ReconciliationStatus.PendingModificationReviewer);
            ReconciliationStatusIDList.Add((short)WebEnums.ReconciliationStatus.PendingReview);
            ReconciliationStatusIDList.Add((short)WebEnums.ReconciliationStatus.Reviewed);
            ReconciliationStatusIDList.Add((short)WebEnums.ReconciliationStatus.Prepared);
            ReconciliationStatusIDList.Add((short)WebEnums.ReconciliationStatus.PendingReview);
            if (this.chkShowSRAAsWell.Checked)
                ReconciliationStatusIDList.Add((short)WebEnums.ReconciliationStatus.SysReconciled);

            oGLDataIDCollection = GetGLDataIDCollection(ReconciliationStatusIDList);
            if (oGLDataIDCollection.Count > 0)
            {
                IGLData oGLDataClient = RemotingHelper.GetGLDataObject();
                oGLDataClient.UpdateReSetAccount(oGLDataIDCollection, SessionHelper.CurrentUserLoginID, DateTime.Now, (short)ARTEnums.ActionType.ResetReconciliationFromUI, (short)ARTEnums.ChangeSource.ResetReconciliation, Helper.GetAppUserInfo());
                MasterPageBase oMasterPageBase = (MasterPageBase)this.Master;
                oMasterPageBase.ShowConfirmationMessage(2673);
                PopulateAccountsGrid();
                PopulatePercentCompleteLabel();
            }
            MyHiddenField.Value = "";
        }
    }

    protected void btnDownloadSelected_Click(object sender, EventArgs e)
    {
        List<GLDataHdrInfo> oGLDataHdrInfoList = null;
        try
        {
            if (Page.IsValid)
            {
                oGLDataHdrInfoList = GetSelectedGLDataHdrInfo();
                RequestHelper.SaveRequest(ARTEnums.RequestType.DownloadSelectedRecFormsDetailed, ARTEnums.Grid.AccountViewer, RequestHelper.CreateDataSetForDownloadAllRequest(oGLDataHdrInfoList));
                Helper.ShowConfirmationMessage(this, LanguageUtil.GetValue(2815));
                PopulateAccountsGrid();
                PopulatePercentCompleteLabel();
                MyHiddenField.Value = "";

            }
        }
        catch (ARTException ex)
        {
            Helper.ShowErrorMessage(this, ex);
        }
        catch (Exception ex)
        {
            Helper.ShowErrorMessage(this, ex);
        }
    }

    protected void btnDownloadAll_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                IGLData oGLDataClient = RemotingHelper.GetGLDataObject();
                bool isSRA = chkShowSRAAsWell.Checked;
                List<FilterCriteria> oFilterCriteriaCollection = null;//(List<FilterCriteria>)Session[sessionKey];

                List<GLDataHdrInfo> oGLDataHdrInfoList = oGLDataClient.SelectGLDataAndAccountInfoByUserID(oFilterCriteriaCollection,
                            this._ReconciliationPeriodID, this._CompanyID, this._UserID, this._RoleID, this._IsDualReviewEnabled, this._IsMaterialityEnabled,
                            (short)ARTEnums.AccountAttribute.Preparer, (short)ARTEnums.AccountAttribute.Reviewer, (short)ARTEnums.AccountAttribute.Approver,
                            (short)ARTEnums.UserRole.PREPARER, (short)ARTEnums.UserRole.REVIEWER, (short)ARTEnums.UserRole.APPROVER,
                            (short)ARTEnums.UserRole.SYSTEM_ADMIN, (short)ARTEnums.UserRole.CEO_CFO, (short)ARTEnums.UserRole.SKYSTEM_ADMIN
                            , isSRA, null, Helper.GetAccountAttributeIDCollection(WebEnums.AccountPages.AccountViewer),
                            SessionHelper.GetUserLanguage(), SessionHelper.GetBusinessEntityID(), AppSettingHelper.GetDefaultLanguageID(),
                            "NetAccountID", "ASC", Helper.GetAppUserInfo());

                if (oGLDataHdrInfoList != null && oGLDataHdrInfoList.Count > 0)
                {
                    RequestHelper.SaveRequest(ARTEnums.RequestType.DownloadAllRecFormsDetailed, ARTEnums.Grid.AccountViewer, RequestHelper.CreateDataSetForDownloadAllRequest(oGLDataHdrInfoList));
                    Helper.ShowConfirmationMessage(this, LanguageUtil.GetValue(2815));
                }
                else
                    Helper.ShowConfirmationMessage(this, LanguageUtil.GetValue(5000116));
            }
        }
        catch (ARTException ex)
        {
            Helper.ShowErrorMessage(this, ex);
        }
        catch (Exception ex)
        {
            Helper.ShowErrorMessage(this, ex);
        }
    }

    protected void btnCreateBinders_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                IGLData oGLDataClient = RemotingHelper.GetGLDataObject();
                bool isSRA = chkShowSRAAsWell.Checked;
                List<FilterCriteria> oFilterCriteriaCollection = null;//(List<FilterCriteria>)Session[sessionKey];

                List<GLDataHdrInfo> oGLDataHdrInfoList = oGLDataClient.SelectGLDataAndAccountInfoByUserID(oFilterCriteriaCollection,
                            this._ReconciliationPeriodID, this._CompanyID, this._UserID, this._RoleID, this._IsDualReviewEnabled, this._IsMaterialityEnabled,
                            (short)ARTEnums.AccountAttribute.Preparer, (short)ARTEnums.AccountAttribute.Reviewer, (short)ARTEnums.AccountAttribute.Approver,
                            (short)ARTEnums.UserRole.PREPARER, (short)ARTEnums.UserRole.REVIEWER, (short)ARTEnums.UserRole.APPROVER,
                            (short)ARTEnums.UserRole.SYSTEM_ADMIN, (short)ARTEnums.UserRole.CEO_CFO, (short)ARTEnums.UserRole.SKYSTEM_ADMIN
                            , isSRA, null, Helper.GetAccountAttributeIDCollection(WebEnums.AccountPages.AccountViewer),
                            SessionHelper.GetUserLanguage(), SessionHelper.GetBusinessEntityID(), AppSettingHelper.GetDefaultLanguageID(),
                            "NetAccountID", "ASC", Helper.GetAppUserInfo());
                if (oGLDataHdrInfoList != null && oGLDataHdrInfoList.Count > 0)
                {
                    RequestHelper.SaveRequest(ARTEnums.RequestType.CreateBinders, ARTEnums.Grid.AccountViewer, RequestHelper.CreateDataSetForDownloadAllRequest(oGLDataHdrInfoList));
                    Helper.ShowConfirmationMessage(this, LanguageUtil.GetValue(2815));
                }
                else
                    Helper.ShowConfirmationMessage(this, LanguageUtil.GetValue(5000116));

            }
        }
        catch (ARTException ex)
        {
            Helper.ShowErrorMessage(this, ex);
        }
        catch (Exception ex)
        {
            Helper.ShowErrorMessage(this, ex);
        }
    }
    protected void chkShowSRAAsWell_OnCheckedChanged(object sender, EventArgs e)
    {
        this.SavePageSettings();
        PopulateAccountsGrid();
        PopulatePercentCompleteLabel();
    }
    protected void ompage_ReconciliationPeriodChangedEventHandler(object sender, EventArgs e)
    {
        try
        {
            string sessionKey = SessionHelper.GetSessionKeyForGridCustomization(ARTEnums.Grid.AccountViewer);
            SessionHelper.ClearSession(sessionKey);
            eRecPeriodStatus = CurrentRecProcessStatus.Value;
            IsCertificationStarted = CertificationHelper.IsCertificationStarted();
            if (eRecPeriodStatus == WebEnums.RecPeriodStatus.Skipped)
            {
                pnlErrorMessageForSkippedRecPeriod.Visible = true;
                pnlAcctViewer.Visible = false;
            }
            else
            {
                pnlErrorMessageForSkippedRecPeriod.Visible = false;
                pnlAcctViewer.Visible = true;
                SetPrivateVariableValue();
                PopulateAccountsGrid();
                PopulateOtherItemsOnPage();
            }
            Helper.HideMessage(this);
            MyHiddenField.Value = "";

        }
        catch (ARTException ex)
        {
            Helper.ShowErrorMessage(this, ex);
        }
        catch (Exception ex)
        {
            Helper.ShowErrorMessage(this, ex);
        }
    }

    #endregion

    #region Validation Control Events
    protected void cvAccept_ServerValidate(object source, ServerValidateEventArgs args)
    {
        try
        {
            // Update in ViewState             
            SaveCheckBoxStates();
            String ErrorMessage = "";
            List<long> oGLDataIDCollection = new List<long>();
            if (this._RoleID == (short)ARTEnums.UserRole.PREPARER || this._RoleID == (short)ARTEnums.UserRole.BACKUP_PREPARER)
            {
                oGLDataIDCollection = GetGLDataIDCollection((short)WebEnums.ReconciliationStatus.PendingModificationPreparer);
                ErrorMessage = string.Format(LanguageUtil.GetValue(5000323), LanguageUtil.GetValue(1755));
            }
            else if (this._RoleID == (short)ARTEnums.UserRole.REVIEWER || this._RoleID == (short)ARTEnums.UserRole.BACKUP_REVIEWER)
            {
                oGLDataIDCollection = GetGLDataIDCollection((short)WebEnums.ReconciliationStatus.PendingReview);
                oGLDataIDCollection.InsertRange(oGLDataIDCollection.Count, GetGLDataIDCollection((short)WebEnums.ReconciliationStatus.PendingModificationReviewer));
                ErrorMessage = string.Format(LanguageUtil.GetValue(5000323), LanguageUtil.GetValue(1091));
            }
            else if (this._RoleID == (short)ARTEnums.UserRole.APPROVER || this._RoleID == (short)ARTEnums.UserRole.BACKUP_APPROVER)
            {
                oGLDataIDCollection = GetGLDataIDCollection((short)WebEnums.ReconciliationStatus.PendingApproval);
                ErrorMessage = string.Format(LanguageUtil.GetValue(5000323), LanguageUtil.GetValue(1094));
            }

            if (
             CurrentRecProcessStatus.Value == WebEnums.RecPeriodStatus.Closed
            || CurrentRecProcessStatus.Value == WebEnums.RecPeriodStatus.NotStarted
            || CurrentRecProcessStatus.Value == WebEnums.RecPeriodStatus.Skipped
            || CertificationHelper.IsCertificationStarted()
            )
            {
                args.IsValid = false;
                Helper.ShowErrorMessage(this, ErrorMessage);
            }

            if (oGLDataIDCollection.Count <= 0)//"there is no record"
            {
                args.IsValid = false;
                Helper.ShowErrorMessage(this, ErrorMessage);
                buttonShowHideOnserverValidate();
            }

        }
        catch (ARTException ex)
        {
            Helper.ShowErrorMessage(this, ex);
        }
        catch (Exception ex)
        {
            Helper.ShowErrorMessage(this, ex);
        }
    }
    protected void cvSubmit_ServerValidate(object source, ServerValidateEventArgs args)
    {
        try
        {
            // Update in ViewState             
            SaveCheckBoxStates();
            String ErrorMessage = "";
            List<long> oGLDataIDCollection = new List<long>();
            if (this._RoleID == (short)ARTEnums.UserRole.PREPARER || this._RoleID == (short)ARTEnums.UserRole.BACKUP_PREPARER)
            {
                oGLDataIDCollection = GetGLDataIDCollection((short)WebEnums.ReconciliationStatus.Prepared);
                ErrorMessage = string.Format(LanguageUtil.GetValue(5000323), LanguageUtil.GetValue(1089));
            }
            else if (this._RoleID == (short)ARTEnums.UserRole.REVIEWER || this._RoleID == (short)ARTEnums.UserRole.BACKUP_REVIEWER)
            {
                oGLDataIDCollection = GetGLDataIDCollection((short)WebEnums.ReconciliationStatus.Reviewed);
                ErrorMessage = string.Format(LanguageUtil.GetValue(5000323), LanguageUtil.GetValue(1093));

            }
            else if (this._RoleID == (short)ARTEnums.UserRole.APPROVER || this._RoleID == (short)ARTEnums.UserRole.BACKUP_APPROVER)
            {
                oGLDataIDCollection = GetGLDataIDCollection((short)WebEnums.ReconciliationStatus.Approved);
                ErrorMessage = string.Format(LanguageUtil.GetValue(5000323), LanguageUtil.GetValue(1095));
            }
            if (
              CurrentRecProcessStatus.Value == WebEnums.RecPeriodStatus.Closed
             || CurrentRecProcessStatus.Value == WebEnums.RecPeriodStatus.NotStarted
             || CurrentRecProcessStatus.Value == WebEnums.RecPeriodStatus.Skipped
             || CertificationHelper.IsCertificationStarted()
             )
            {
                args.IsValid = false;
                Helper.ShowErrorMessage(this, ErrorMessage);
            }

            if (oGLDataIDCollection.Count <= 0)//"there is no record"
            {
                args.IsValid = false;
                Helper.ShowErrorMessage(this, ErrorMessage);
                buttonShowHideOnserverValidate();
            }
        }
        catch (ARTException ex)
        {
            Helper.ShowErrorMessage(this, ex);
        }
        catch (Exception ex)
        {
            Helper.ShowErrorMessage(this, ex);
        }
    }

    protected void cvReopen_ServerValidate(object source, ServerValidateEventArgs args)
    {
        try
        {
            // Update in ViewState             
            SaveCheckBoxStates();
            String ErrorMessage = "";
            List<long> oGLDataIDCollection = new List<long>();
            if (this._RoleID == (short)ARTEnums.UserRole.SYSTEM_ADMIN)
            {
                oGLDataIDCollection = GetGLDataIDCollection((short)WebEnums.ReconciliationStatus.Reconciled);
                if (this.chkShowSRAAsWell.Checked)
                    oGLDataIDCollection.InsertRange(oGLDataIDCollection.Count, GetGLDataIDCollection((short)WebEnums.ReconciliationStatus.SysReconciled));
                ErrorMessage = string.Format(LanguageUtil.GetValue(5000323), LanguageUtil.GetValue(2671));
            }

            if (
                  CurrentRecProcessStatus.Value == WebEnums.RecPeriodStatus.Closed
                 || CurrentRecProcessStatus.Value == WebEnums.RecPeriodStatus.NotStarted
                 || CurrentRecProcessStatus.Value == WebEnums.RecPeriodStatus.Skipped
                 || CertificationHelper.IsCertificationStarted()
             )
            {
                args.IsValid = false;
                Helper.ShowErrorMessage(this, ErrorMessage);
            }

            if (oGLDataIDCollection.Count <= 0)//"there is no record"
            {
                args.IsValid = false;
                Helper.ShowErrorMessage(this, ErrorMessage);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ConfirmationReopen", "InitiateAjaxRequest('Confirm');", true);
            }
        }
        catch (ARTException ex)
        {
            Helper.ShowErrorMessage(this, ex);
        }
        catch (Exception ex)
        {
            Helper.ShowErrorMessage(this, ex);
        }
    }
    protected void cvReSet_ServerValidate(object source, ServerValidateEventArgs args)
    {
        try
        {
            //Update in ViewState             
            SaveCheckBoxStates();
            String ErrorMessage = "";
            List<long> oGLDataIDCollection = new List<long>();
            if (this._RoleID == (short)ARTEnums.UserRole.SYSTEM_ADMIN)
            {

                List<short> ReconciliationStatusIDList = new List<short>();
                ReconciliationStatusIDList.Add((short)WebEnums.ReconciliationStatus.Reconciled);
                ReconciliationStatusIDList.Add((short)WebEnums.ReconciliationStatus.InProgress);
                ReconciliationStatusIDList.Add((short)WebEnums.ReconciliationStatus.Approved);
                ReconciliationStatusIDList.Add((short)WebEnums.ReconciliationStatus.PendingApproval);
                ReconciliationStatusIDList.Add((short)WebEnums.ReconciliationStatus.PendingModificationPreparer);
                ReconciliationStatusIDList.Add((short)WebEnums.ReconciliationStatus.PendingModificationReviewer);
                ReconciliationStatusIDList.Add((short)WebEnums.ReconciliationStatus.PendingReview);
                ReconciliationStatusIDList.Add((short)WebEnums.ReconciliationStatus.Reviewed);
                ReconciliationStatusIDList.Add((short)WebEnums.ReconciliationStatus.Prepared);
                ReconciliationStatusIDList.Add((short)WebEnums.ReconciliationStatus.PendingReview);
                if (this.chkShowSRAAsWell.Checked)
                    ReconciliationStatusIDList.Add((short)WebEnums.ReconciliationStatus.SysReconciled);

                oGLDataIDCollection = GetGLDataIDCollection(ReconciliationStatusIDList);
                ErrorMessage = LanguageUtil.GetValue(2672);
            }
            if (
               CurrentRecProcessStatus.Value == WebEnums.RecPeriodStatus.Closed
              || CurrentRecProcessStatus.Value == WebEnums.RecPeriodStatus.NotStarted
              || CurrentRecProcessStatus.Value == WebEnums.RecPeriodStatus.Skipped
              || CertificationHelper.IsCertificationStarted()
              )
            {
                args.IsValid = false;
                Helper.ShowErrorMessage(this, ErrorMessage);
            }
            if (oGLDataIDCollection.Count <= 0)//"there is no record"
            {
                args.IsValid = false;
                Helper.ShowErrorMessage(this, ErrorMessage);
            }

        }
        catch (ARTException ex)
        {
            Helper.ShowErrorMessage(this, ex);
        }
        catch (Exception ex)
        {
            Helper.ShowErrorMessage(this, ex);
        }
    }

    protected void cvDownloadSelected_ServerValidate(object source, ServerValidateEventArgs args)
    {
        try
        {
            // Update in ViewState             
            SaveCheckBoxStates();
            String ErrorMessage = "";
            List<long> oGLDataIDCollection = (List<long>)ViewState["CHECKED_ITEMS"];
            if (oGLDataIDCollection == null || oGLDataIDCollection.Count <= 0)//"there is no record"
            {
                ErrorMessage = LanguageUtil.GetValue(2013);
                args.IsValid = false;
                cvDownloadSelected.ErrorMessage = ErrorMessage;
                Helper.ShowErrorMessage(this, ErrorMessage);
                buttonShowHideOnserverValidate();
            }

        }
        catch (ARTException ex)
        {
            Helper.ShowErrorMessage(this, ex);
        }
        catch (Exception ex)
        {
            Helper.ShowErrorMessage(this, ex);
        }
    }

    #endregion

    #region Private Methods
    private void PopulateOtherItemsOnPage()
    {
        PopulateBaseAndReportingCurrency();

        PopulateDueDate();

        PopulatePercentCompleteLabel();
    }

    private void PopulateBaseAndReportingCurrency()
    {
        lblReportingCurrencyValue.Text = SessionHelper.ReportingCurrencyCode;
    }

    private void PopulateDueDate()
    {
        if (SessionHelper.CurrentReconciliationPeriodID.HasValue)
            if (Helper.IsDueDatesByAccountConfiuredForRecPeriodID(SessionHelper.CurrentReconciliationPeriodID.Value))
            {
                lblDueDate.Visible = false;
                lblDueDateValue.Visible = false;
            }
            else
            {
                lblDueDate.Visible = true;
                lblDueDateValue.Visible = true;

                IReconciliationPeriod oReconciliationPeriodClient = RemotingHelper.GetReconciliationPeriodObject();
                DateTime? dueDate = oReconciliationPeriodClient.GetDueDateByUserRoleID(this._RoleID, (short)ARTEnums.UserRole.PREPARER,
                    (short)ARTEnums.UserRole.REVIEWER, (short)ARTEnums.UserRole.APPROVER, this._ReconciliationPeriodID, Helper.GetAppUserInfo());

                if (SessionHelper.CurrentRoleID == (short)ARTEnums.UserRole.PREPARER
                    || SessionHelper.CurrentRoleID == (short)ARTEnums.UserRole.REVIEWER
                    || SessionHelper.CurrentRoleID == (short)ARTEnums.UserRole.APPROVER)
                {
                    if (dueDate.HasValue)
                    {
                        lblDueDateValue.Text = Helper.GetDisplayDate(dueDate);
                    }
                    else
                    {
                        lblDueDateValue.Text = "-";
                    }
                }
                else
                {
                    lblDueDateValue.Text = "-";
                }
            }
    }

    private void PopulatePercentCompleteLabel()
    {
        IGLData oGLDataClient = RemotingHelper.GetGLDataObject();
        bool isSRA = chkShowSRAAsWell.Checked;
        AccountCountInfo oAccountCountInfo = oGLDataClient.GetTotalAndCompletedAccountCount(
            this._ReconciliationPeriodID
            , this._CompanyID
            , this._UserID
            , this._RoleID
            , this._IsDualReviewEnabled
            , (short)ARTEnums.AccountAttribute.Preparer
            , (short)ARTEnums.AccountAttribute.Reviewer
            , (short)ARTEnums.AccountAttribute.Approver
            , (short)ARTEnums.UserRole.PREPARER
            , (short)ARTEnums.UserRole.REVIEWER
            , (short)ARTEnums.UserRole.APPROVER
            , (short)ARTEnums.UserRole.SYSTEM_ADMIN
            , (short)ARTEnums.UserRole.CEO_CFO
            , (short)ARTEnums.UserRole.SKYSTEM_ADMIN
            , (short)WebEnums.ReconciliationStatus.SysReconciled
            , isSRA
            , Helper.GetAppUserInfo());

        decimal? accountCompletePercentage = 0;

        if (oAccountCountInfo.TotalAccounts != 0)
        {
            accountCompletePercentage = ((oAccountCountInfo.TotalCompletedAccounts * 1.0M) / oAccountCountInfo.TotalAccounts) * 100;
        }

        lblPercentCompleteValue.Text = string.Format("{0}: {1}% (= {2} / {3})", LanguageUtil.GetValue(1430), Helper.GetDisplayDecimalValue(accountCompletePercentage), Helper.GetDisplayIntegerValue(oAccountCountInfo.TotalCompletedAccounts), Helper.GetDisplayIntegerValue(oAccountCountInfo.TotalAccounts));

        tdPercentComplete.Style.Add("background-color", "#a6d3f7");
        tdPercentComplete.Style.Add("width", Math.Ceiling(accountCompletePercentage.Value) + "%");
        tdPercentNotComplete.Style.Add("width", Math.Floor(100 - accountCompletePercentage.Value) + "%");
    }

    private void PopulateAccountsGrid()
    {
        this.LoadPageSettings();
        Sel.Value = string.Empty;
        IGLData oGLDataClient = RemotingHelper.GetGLDataObject();
        bool isSRA = chkShowSRAAsWell.Checked;
        //TODO: Change false to actual check box selected value when check box for include SRA's is included
        //int defaultItemCount = Convert.ToInt32(AppSettingHelper.GetAppSettingValue(AppSettingConstants.DEFAULT_CHUNK_SIZE));
        int defaultItemCount = Helper.GetDefaultChunkSize(ucSkyStemARTGrid.Grid.PageSize);
        ARTEnums.UserRole eCurrentRole = (ARTEnums.UserRole)System.Enum.Parse(typeof(ARTEnums.UserRole), this._RoleID.ToString());
        ucSkyStemARTGrid.Grid.AllowPaging = true;
        List<FilterCriteria> oFilterCriteriaCollection = (List<FilterCriteria>)Session[sessionKey];
        //Added for checkBox State
        ClearCheckedItemViewState();
        oGLDataHdrInfoCollection = oGLDataClient.SelectGLDataAndAccountInfoByUserID(oFilterCriteriaCollection,
                    this._ReconciliationPeriodID, this._CompanyID, this._UserID, this._RoleID, this._IsDualReviewEnabled, this._IsMaterialityEnabled,
                    (short)ARTEnums.AccountAttribute.Preparer, (short)ARTEnums.AccountAttribute.Reviewer, (short)ARTEnums.AccountAttribute.Approver,
                    (short)ARTEnums.UserRole.PREPARER, (short)ARTEnums.UserRole.REVIEWER, (short)ARTEnums.UserRole.APPROVER,
                    (short)ARTEnums.UserRole.SYSTEM_ADMIN, (short)ARTEnums.UserRole.CEO_CFO, (short)ARTEnums.UserRole.SKYSTEM_ADMIN
                    , isSRA, defaultItemCount, Helper.GetAccountAttributeIDCollection(WebEnums.AccountPages.AccountViewer),
                    SessionHelper.GetUserLanguage(), SessionHelper.GetBusinessEntityID(), AppSettingHelper.GetDefaultLanguageID(),
                    "NetAccountID", "ASC", Helper.GetAppUserInfo());



        if (oGLDataHdrInfoCollection.Count < defaultItemCount)
        {
            ucSkyStemARTGrid.Grid.VirtualItemCount = oGLDataHdrInfoCollection.Count;
        }
        else
        {
            ucSkyStemARTGrid.Grid.VirtualItemCount = oGLDataHdrInfoCollection.Count + 1;
        }

        //oGLDataHdrInfoCollection = LanguageHelper.TranslateLabelsGLData(oGLDataHdrInfoCollection);
        HttpContext.Current.Session[SessionConstants.SEARCH_RESULTS_ACCOUNT_VIEWER] = oGLDataHdrInfoCollection;

        countDisabledCheckboxes = 0;
        ucSkyStemARTGrid.Grid.EntityNameLabelID = 1187;

        ucSkyStemARTGrid.Grid.ClientSettings.ClientEvents.OnRowSelecting = "Selecting";

        ucSkyStemARTGrid.ShowStatusImageColumn = true;
        ucSkyStemARTGrid.CompanyID = this._CompanyID;
        ShowHideButtons(true);
        this.btnReopen.Visible = false;
        this.btnReset.Visible = false;
        ucSkyStemARTGrid.ShowSelectCheckBoxColum = true;
        ucSkyStemARTGrid.DataSource = oGLDataHdrInfoCollection;
        ucSkyStemARTGrid.BindGrid();

        //switch (AccountViewerHelper.GetFormMode())
        //{
        //    case WebEnums.FormMode.Edit:
        //        if (
        //                (oGLDataHdrInfoCollection.Count < ucSkyStemARTGrid.Grid.PageSize
        //                && countDisabledCheckboxes < oGLDataHdrInfoCollection.Count)
        //            ||
        //                (oGLDataHdrInfoCollection.Count >= ucSkyStemARTGrid.Grid.PageSize
        //                 && countDisabledCheckboxes < ucSkyStemARTGrid.Grid.PageSize)
        //            )
        //            btnSubmit.Visible = true;
        //        else
        //            btnSubmit.Visible = false;
        //        ucSkyStemARTGrid.ShowSelectCheckBoxColum = true;
        //        break;
        //    default:
        //        btnSubmit.Visible = false;
        //        ucSkyStemARTGrid.ShowSelectCheckBoxColum = false;
        //        break;
        //}
    }

    private void SetPrivateVariableValue()
    {
        if (SessionHelper.CurrentCompanyID.HasValue)
            this._CompanyID = SessionHelper.CurrentCompanyID.Value;
        if (SessionHelper.CurrentRoleID.HasValue)
            this._RoleID = SessionHelper.CurrentRoleID.Value;
        if (SessionHelper.CurrentReconciliationPeriodID.HasValue)
            this._ReconciliationPeriodID = SessionHelper.CurrentReconciliationPeriodID.Value;
        UserHdrInfo oUserHdrInfo = SessionHelper.GetCurrentUser();
        if (oUserHdrInfo != null)
            this._UserID = oUserHdrInfo.UserID.Value;

        //if (!string.IsNullOrEmpty(Request.QueryString[QueryStringConstants.IS_SRA]))
        //    this._IsSRA = Convert.ToBoolean(Convert.ToInt16(Request.QueryString[QueryStringConstants.IS_SRA]));

        if (!string.IsNullOrEmpty(Request.QueryString[QueryStringConstants.CLEAR_FILTER]))
            this._IsClearFilter = Convert.ToBoolean(Convert.ToInt16(Request.QueryString[QueryStringConstants.CLEAR_FILTER]));

        //if (!IsPostBack && this._IsSRA == true)
        //    chkShowSRAAsWell.Checked = true;

        //if (!IsPostBack && this._IsClearFilter == true)
        //    SessionHelper.ClearGridFilterDataFromSession(ucSkyStemARTGrid.GridType);

        List<CompanyCapabilityInfo> oCompanyCapabilityInfoCollection = SessionHelper.GetCompanyCapabilityCollectionForCurrentRecPeriod();
        SetCapabilityInfo(oCompanyCapabilityInfoCollection);
    }
    private void SetCapabilityInfo(List<CompanyCapabilityInfo> oCompanyCapabilityInfoCollection)
    {
        foreach (CompanyCapabilityInfo oCompanyCapabilityInfo in oCompanyCapabilityInfoCollection)
        {
            if (oCompanyCapabilityInfo.CapabilityID.HasValue)
            {
                ARTEnums.Capability oCapability = (ARTEnums.Capability)Enum.Parse(typeof(ARTEnums.Capability), oCompanyCapabilityInfo.CapabilityID.Value.ToString());

                switch (oCapability)
                {
                    case ARTEnums.Capability.DualLevelReview:
                        if (oCompanyCapabilityInfo.IsActivated.HasValue && oCompanyCapabilityInfo.IsActivated.Value)
                        {
                            this._IsDualReviewEnabled = true;
                        }
                        break;

                    case ARTEnums.Capability.MaterialitySelection:
                        if (oCompanyCapabilityInfo.IsActivated.HasValue && oCompanyCapabilityInfo.IsActivated.Value)
                        {
                            this._IsMaterialityEnabled = true;
                        }
                        break;

                    case ARTEnums.Capability.KeyAccount:
                        if (oCompanyCapabilityInfo.IsActivated.HasValue && oCompanyCapabilityInfo.IsActivated.Value)
                        {
                            this._IsKeyAccountEnabled = true;
                        }
                        break;

                    case ARTEnums.Capability.NetAccount:
                        if (oCompanyCapabilityInfo.IsActivated.HasValue && oCompanyCapabilityInfo.IsActivated.Value)
                        {
                            this._IsNetAccountEnabled = true;
                        }
                        break;

                    case ARTEnums.Capability.RiskRating:
                        if (oCompanyCapabilityInfo.IsActivated.HasValue && oCompanyCapabilityInfo.IsActivated.Value)
                        {
                            this._IsRiskRatingEnabled = true;
                        }
                        break;

                    case ARTEnums.Capability.ZeroBalanceAccount:
                        if (oCompanyCapabilityInfo.IsActivated.HasValue && oCompanyCapabilityInfo.IsActivated.Value)
                        {
                            this._IsZeroBalanceEnabled = true;
                        }
                        break;

                    case ARTEnums.Capability.MultiCurrency:
                        if (oCompanyCapabilityInfo.IsActivated.HasValue && oCompanyCapabilityInfo.IsActivated.Value)
                        {
                            this._IsMultiCurrencyEnabled = true;
                        }
                        break;
                }
            }
        }
    }
    /// <summary>
    /// ShowHideButtons() is used to show/hide all buttons on page. 
    /// </summary>
    /// <param name="isShow"></param>
    private void ShowHideButtons(bool isShow)
    {
        btnSubmit.Visible = isShow;
        btnAccept.Visible = isShow;
    }
    private List<GLDataHdrInfo> GetSelectedGLDataHdrInfo()
    {
        List<GLDataHdrInfo> oGLDataHdrInfoListSelected = null;
        List<GLDataHdrInfo> oGLDataHdrInfoList = (List<GLDataHdrInfo>)HttpContext.Current.Session[SessionConstants.SEARCH_RESULTS_ACCOUNT_VIEWER];
        List<long> oGLDataIDCollection = (List<long>)ViewState["CHECKED_ITEMS"];
        if (oGLDataIDCollection != null && oGLDataIDCollection.Count > 0)
            oGLDataHdrInfoListSelected = (from obj in oGLDataHdrInfoList
                                          join ID in oGLDataIDCollection on obj.GLDataID equals ID
                                          select obj).ToList();
        return oGLDataHdrInfoListSelected;
    }

    private List<long> GetGLDataIDCollection(short ReconciliationStatusID)
    {
        List<long> oGLDataIDCollection = new List<long>();
        Dictionary<long, short> oReconciliationStatusIDCollection = null;
        oReconciliationStatusIDCollection = (Dictionary<long, short>)ViewState["CHECKED_RECONCILIATIONSTATUSID"];
        if (oReconciliationStatusIDCollection != null && oReconciliationStatusIDCollection.Count > 0)
            oGLDataIDCollection = (from obj in oReconciliationStatusIDCollection
                                   where obj.Value == ReconciliationStatusID
                                   select obj.Key).ToList();
        return oGLDataIDCollection;

    }
    private List<long> GetGLDataIDCollection(List<short> ReconciliationStatusIDList)
    {
        List<long> oGLDataIDCollection = new List<long>();
        Dictionary<long, short> oReconciliationStatusIDCollection = null;
        oReconciliationStatusIDCollection = (Dictionary<long, short>)ViewState["CHECKED_RECONCILIATIONSTATUSID"];
        if (oReconciliationStatusIDCollection != null && oReconciliationStatusIDCollection.Count > 0)
            oGLDataIDCollection = (from obj in oReconciliationStatusIDCollection
                                   join ReconciliationStatusID in ReconciliationStatusIDList on obj.Value equals ReconciliationStatusID
                                   select obj.Key).ToList();
        return oGLDataIDCollection;

    }

    private void GetSelectedAccountIdCollection(List<long> oGLDataIDCollection, out List<long> oActIDCollection, out  List<int> oNetActIDCollection)
    {
        List<long> oAccountIDCollection = new List<long>();
        List<int> oNetAccountIDCollection = new List<int>();
        foreach (GridDataItem item in ucSkyStemARTGrid.Grid.SelectedItems)
        {
            string accountId = item["ID"].Text;
            string netAccountId = item["NetAccountID"].Text;
            long glDataID = Convert.ToInt64(item.GetDataKeyValue("GLDataID"));
            if (oGLDataIDCollection.Contains(glDataID))
            {
                if (!string.IsNullOrEmpty(accountId))
                {
                    oAccountIDCollection.Add(Convert.ToInt64(accountId));
                }
                else if (!string.IsNullOrEmpty(netAccountId))
                {
                    oNetAccountIDCollection.Add(Convert.ToInt32(netAccountId));
                }
            }
        }
        oActIDCollection = oAccountIDCollection;
        oNetActIDCollection = oNetAccountIDCollection;

    }
    private void SaveCheckBoxStates()
    {
        List<long> checkBoxStatusList = null;
        long glDataID = -1;
        short ReconciliationStatusID = 0;
        Dictionary<long, short> oReconciliationStatusIDCollection = null;
        ExRadGrid grdData = (ExRadGrid)ucSkyStemARTGrid.Grid;
        foreach (GridDataItem item in grdData.Items)
        {
            glDataID = Convert.ToInt64(item.GetDataKeyValue("GLDataID"));
            ReconciliationStatusID = Convert.ToInt16(item.GetDataKeyValue("ReconciliationStatusID"));
            bool result = false;
            if (item.Selected == true)
            {
                result = true;
            }
            // Check in the ViewState
            if (ViewState["CHECKED_ITEMS"] != null)
                checkBoxStatusList = (List<long>)ViewState["CHECKED_ITEMS"];
            else
                checkBoxStatusList = new List<long>();
            // Check in the ViewState
            if (ViewState["CHECKED_RECONCILIATIONSTATUSID"] != null)
                oReconciliationStatusIDCollection = (Dictionary<long, short>)ViewState["CHECKED_RECONCILIATIONSTATUSID"];
            else
                oReconciliationStatusIDCollection = new Dictionary<long, short>();
            if (result)
            {
                if (!checkBoxStatusList.Contains(glDataID))
                {
                    checkBoxStatusList.Add(glDataID);
                    oReconciliationStatusIDCollection.Add(glDataID, ReconciliationStatusID);
                    ViewState["CHECKED_ITEMS"] = checkBoxStatusList;
                    ViewState["CHECKED_RECONCILIATIONSTATUSID"] = oReconciliationStatusIDCollection;
                }
            }
            else
            {
                checkBoxStatusList.Remove(glDataID);
                oReconciliationStatusIDCollection.Remove(glDataID);
            }
        }
        if (checkBoxStatusList != null && checkBoxStatusList.Count > 0)
        {
            ViewState["CHECKED_ITEMS"] = checkBoxStatusList;
        }
        if (oReconciliationStatusIDCollection != null && oReconciliationStatusIDCollection.Count > 0)
        {
            ViewState["CHECKED_RECONCILIATIONSTATUSID"] = oReconciliationStatusIDCollection;
        }
    }

    private void RePopulateCheckBoxStates()
    {
        List<long> checkBoxStatusList = null;
        if (ViewState["CHECKED_ITEMS"] != null)
        {
            checkBoxStatusList = (List<long>)ViewState["CHECKED_ITEMS"];
        }
        if (checkBoxStatusList != null && checkBoxStatusList.Count > 0)
        {

            ExRadGrid grdData = ucSkyStemARTGrid.Grid;
            foreach (GridDataItem item in grdData.Items)
            {
                long glDataID = Convert.ToInt64(item.GetDataKeyValue("GLDataID"));
                if (checkBoxStatusList.Contains(glDataID))
                {
                    item.Selected = true;
                }

            }
        }
    }
    private void ShowHideDownloadAllRecsBtn()
    {

        //ARTEnums.UserRole eCurrentRole = (ARTEnums.UserRole)System.Enum.Parse(typeof(ARTEnums.UserRole), this._RoleID.ToString());
        //if (eCurrentRole == ARTEnums.UserRole.SYSTEM_ADMIN)
        //{
        if (SessionHelper.CurrentReconciliationPeriodID.HasValue && Helper.IsFeatureActivated(WebEnums.Feature.DownloadAllRecs, SessionHelper.CurrentReconciliationPeriodID))
        {
            btnDownloadSelected.Visible = true;
            btnDownloadAll.Visible = true;
            cvDownloadSelected.Enabled = true;
        }
        else
        {
            btnDownloadSelected.Visible = false;
            btnDownloadAll.Visible = false;
            cvDownloadSelected.Enabled = false;
        }
        if (SessionHelper.CurrentReconciliationPeriodID.HasValue && Helper.IsFeatureActivated(WebEnums.Feature.ERecBinders, SessionHelper.CurrentReconciliationPeriodID))
            btnCreateBinders.Visible = true;
        else
            btnCreateBinders.Visible = false;

        //}
        //else
        //{
        //    btnDownloadSelected.Visible = false;
        //    btnDownloadAll.Visible = false;          
        //    cvDownloadSelected.Enabled = false;
        //    btnCreateBinders.Visible = false;
        //}

    }


    #endregion

    #region Other Methods
    protected void buttonShowHideOnserverValidate()
    {
        ARTEnums.UserRole eCurrentRole = (ARTEnums.UserRole)System.Enum.Parse(typeof(ARTEnums.UserRole), this._RoleID.ToString());
        if (eCurrentRole == ARTEnums.UserRole.SYSTEM_ADMIN)
        {
            ShowHideButtons(false);

        }
        else if (eCurrentRole == ARTEnums.UserRole.PREPARER || eCurrentRole == ARTEnums.UserRole.REVIEWER || eCurrentRole == ARTEnums.UserRole.APPROVER ||
                            eCurrentRole == ARTEnums.UserRole.BACKUP_PREPARER || eCurrentRole == ARTEnums.UserRole.BACKUP_REVIEWER || eCurrentRole == ARTEnums.UserRole.BACKUP_APPROVER)
        {

            ShowHideButtons(true);
        }
    }
    public override string GetMenuKey()
    {
        return "AccountViewer";
    }
    [System.Web.Services.WebMethod()]
    [System.Web.Script.Services.ScriptMethod()]
    public static void InsertUserGLDataFlag(int userID, string userLoginID, long glDataID)
    {
        UserGLDataFlagInfo oUserGLDataFlagInfo = new UserGLDataFlagInfo();
        oUserGLDataFlagInfo.AddedBy = userLoginID;
        oUserGLDataFlagInfo.DateAdded = DateTime.Now;
        oUserGLDataFlagInfo.GLDataID = glDataID;
        oUserGLDataFlagInfo.IsActive = true;
        oUserGLDataFlagInfo.UserID = userID;

        IGLData oGLDataClient = RemotingHelper.GetGLDataObject();
        oGLDataClient.InsertUserGLDataFlag(oUserGLDataFlagInfo, Helper.GetAppUserInfo());

        //added By PRafull on 03-Mar-2011
        List<GLDataHdrInfo> oGLDataHdrInfoCollection = (List<GLDataHdrInfo>)HttpContext.Current.Session[SessionConstants.SEARCH_RESULTS_ACCOUNT_VIEWER];
        if (oGLDataHdrInfoCollection != null && oGLDataHdrInfoCollection.Count > 0)
        {
            foreach (GLDataHdrInfo oGLDataHdrInfo in oGLDataHdrInfoCollection)
            {
                if (oGLDataHdrInfo.GLDataID == glDataID)
                {
                    oGLDataHdrInfo.IsFlagged = true;
                    HttpContext.Current.Session[SessionConstants.SEARCH_RESULTS_ACCOUNT_VIEWER] = oGLDataHdrInfoCollection;
                    break;
                }
            }
        }

    }

    [System.Web.Services.WebMethod()]
    [System.Web.Script.Services.ScriptMethod()]
    public static void DeleteUserGLDataFlag(int userID, string userLoginID, long glDataID)
    {
        UserGLDataFlagInfo oUserGLDataFlagInfo = new UserGLDataFlagInfo();
        oUserGLDataFlagInfo.RevisedBy = userLoginID;
        oUserGLDataFlagInfo.DateRevised = DateTime.Now;
        oUserGLDataFlagInfo.GLDataID = glDataID;
        oUserGLDataFlagInfo.IsActive = false;
        oUserGLDataFlagInfo.UserID = userID;

        IGLData oGLDataClient = RemotingHelper.GetGLDataObject();
        oGLDataClient.DeleteUserGLDataFlag(oUserGLDataFlagInfo, Helper.GetAppUserInfo());

        //added By PRafull on 03-Mar-2011
        List<GLDataHdrInfo> oGLDataHdrInfoCollection = (List<GLDataHdrInfo>)HttpContext.Current.Session[SessionConstants.SEARCH_RESULTS_ACCOUNT_VIEWER];
        if (oGLDataHdrInfoCollection != null && oGLDataHdrInfoCollection.Count > 0)
        {
            foreach (GLDataHdrInfo oGLDataHdrInfo in oGLDataHdrInfoCollection)
            {
                if (oGLDataHdrInfo.GLDataID == glDataID)
                {
                    oGLDataHdrInfo.IsFlagged = false;
                    HttpContext.Current.Session[SessionConstants.SEARCH_RESULTS_ACCOUNT_VIEWER] = oGLDataHdrInfoCollection;
                    break;
                }
            }
        }

    }

    void ClearCheckedItemViewState()
    {
        ViewState["CHECKED_ITEMS"] = null;
        ViewState["CHECKED_RECONCILIATIONSTATUSID"] = null;

    }

    public override void RefreshPage(object sender, RefreshEventArgs args)
    {
        base.RefreshPage(sender, args);
        // repopulate grid as coming from Grid Personalization 
        ompage_ReconciliationPeriodChangedEventHandler(null, null);
    }
    #endregion

}
