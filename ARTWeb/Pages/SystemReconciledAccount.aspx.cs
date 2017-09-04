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
using SkyStem.ART.Client.Model;
using System.Collections.Generic;
using SkyStem.ART.Web.Utility;
using SkyStem.Library.Controls.WebControls;
using SkyStem.ART.Web.Classes;
using SkyStem.ART.Client.IServices;
using SkyStem.ART.Web.Data;
using SkyStem.ART.Client.Data;
using Telerik.Web.UI;
using SkyStem.ART.Client.Exception;
using SkyStem.Language.LanguageUtility;
using SkyStem.Library.Controls.TelerikWebControls;

public partial class Pages_SystemReconciledAccount : PageBaseRecPeriod
{
    const int GRID_COLUMN_INDEX_KEY_START = 0;
    private int _CompanyID;
    private short _RoleID;
    private int _ReconciliationPeriodID;
    private int _UserID;
    private bool _IsDualReviewEnabled;
    private bool _IsMaterialityEnabled;
    public bool _IsZeroBalanceEnabled = false;
    public bool _IsKeyAccountEnabled;
    public bool _IsRiskRatingEnabled = false;
    public bool _IsNetAccountEnabled = false;
    public bool _IsMultiCurrencyEnabled = false;
    private bool? _IsClearFilter;
    public int countDisabledCheckboxes = 0;
    string sessionKey = null;
    WebEnums.RecPeriodStatus eRecPeriodStatus;
    bool IsCertificationStarted = false;
    protected void Page_Init(object sender, EventArgs e)
    {
        MasterPageBase ompage = (MasterPageBase)this.Master;
        ompage.ReconciliationPeriodChangedEventHandler += new EventHandler(ompage_ReconciliationPeriodChangedEventHandler);

        ucSkyStemARTGrid.Grid_NeedDataSourceEventHandler += new UserControls_SkyStemARTGrid.Grid_NeedDataSource(ucSkyStemARTGrid_Grid_NeedDataSourceEventHandler);
        ucSkyStemARTGrid.Grid.PagerStyle.PagerTextFormat = "Change page: {4}";

        
        ScriptManager.GetCurrent(this).EnablePartialRendering = false;
    }
    protected void Page_Load(object sender, EventArgs e)
    {

        //Helper.RegisterPostBackToControls(this, ucSkyStemARTGrid.Grid);
     
        try
        {
            // Check for Rec Period
            if (SessionHelper.CurrentReconciliationPeriodID == null)
            {
                Helper.RedirectToErrorPage(5000061, true);
            }
            Helper.SetPageTitle(this, 1075);
            ucSkyStemARTGrid.BasePageTitle = 1075;
            eRecPeriodStatus = CurrentRecProcessStatus.Value;
            IsCertificationStarted = CertificationHelper.IsCertificationStarted();
            sessionKey = SessionHelper.GetSessionKeyForGridFilter(ucSkyStemARTGrid.GridType);

            //Binding the PageIndex change Event to its handler
            ucSkyStemARTGrid.PageIndexChangedEvent += new UserControls_SkyStemARTGrid.Grid_PageIndexChanged(ucSkyStemARTGrid_PageIndexChangedEvent);
            //Binding the SortColumn Event to its handler
            ucSkyStemARTGrid.GridColumnSortingEvent += new UserControls_SkyStemARTGrid.Grid_ColumnSorting(ucSkyStemARTGrid_GridColumnSortingEvent);

            if (CurrentRecProcessStatus.Value == WebEnums.RecPeriodStatus.Skipped)
            {
                pnlErrorMessageForSkippedRecPeriod.Visible = true;
                pnlAcctViewer.Visible = false;
            }
            else
            {
                pnlErrorMessageForSkippedRecPeriod.Visible = false;
                pnlAcctViewer.Visible = true;
                SetPrivateVariableValue();
                PopulateOtherItemsOnPage();
            }
            //this.btnReopen.Visible = AccountViewerHelper.ShowHideReopenAccountbtn(_RoleID);

        }
        catch (ARTException ex)
        {
            Helper.ShowErrorMessage(this, ex);
        }
        catch (Exception ex)
        {
            Helper.ShowErrorMessage(this, ex);
        }

        //PopulateSRAGrid();        
    }
    protected void ompage_ReconciliationPeriodChangedEventHandler(object sender, EventArgs e)
    {
        try
        {
            string sessionKey = SessionHelper.GetSessionKeyForGridCustomization(ARTEnums.Grid.AccountViewerSRA);
            SessionHelper.ClearSession(sessionKey);
            Helper.SetPageTitle(this, 1075);
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
                PopulateSRAGrid();
                PopulateOtherItemsOnPage();
            }
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
    private void PopulateOtherItemsOnPage()
    {
        PopulateBaseAndReportingCurrency();

        PopulateDueDate();
    }
    private void PopulateBaseAndReportingCurrency()
    {
        lblReportingCurrencyValue.Text = SessionHelper.ReportingCurrencyCode;
    }
    private void PopulateDueDate()
    {
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
    private void SetPrivateVariableValue()
    {
        if (!string.IsNullOrEmpty(Request.QueryString[QueryStringConstants.CLEAR_FILTER]))
            this._IsClearFilter = Convert.ToBoolean(Convert.ToInt16(Request.QueryString[QueryStringConstants.CLEAR_FILTER]));

        if (!IsPostBack && this._IsClearFilter == true)
            SessionHelper.ClearGridFilterDataFromSession(ucSkyStemARTGrid.GridType);

        this._CompanyID = SessionHelper.CurrentCompanyID.Value;
        this._RoleID = SessionHelper.CurrentRoleID.Value;
        this._ReconciliationPeriodID = SessionHelper.CurrentReconciliationPeriodID.Value;
        UserHdrInfo oUserHdrInfo = SessionHelper.GetCurrentUser();
        this._UserID = oUserHdrInfo.UserID.Value;

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
    private void PopulateSRAGrid()
    {
        Sel.Value = string.Empty;
        IGLData oGLDataClient = RemotingHelper.GetGLDataObject();
        //List<GLDataHdrInfo> oGLDataHdrInfoCollection = GetGLDataHdrInfoDemoDataForSRA();
        int? companyID = SessionHelper.CurrentCompanyID;
        short? currentRoleID = SessionHelper.CurrentRoleID;
        int? reconciliationPeriodID = SessionHelper.CurrentReconciliationPeriodID;
        UserHdrInfo oUserHdrInfo = SessionHelper.GetCurrentUser();
        int? userID = oUserHdrInfo.UserID;
        //int defaultItemCount = Convert.ToInt32(AppSettingHelper.GetAppSettingValue(AppSettingConstants.DEFAULT_CHUNK_SIZE));
        int defaultItemCount = Helper.GetDefaultChunkSize(ucSkyStemARTGrid.Grid.PageSize);

        List<FilterCriteria> oFilterCriteriaCollection = (List<FilterCriteria>)Session[sessionKey];

        IList<GLDataHdrInfo> oGLDataHdrInfoCollection = oGLDataClient.SelectGLDataAndAccountInfoByUserIDForSRA(
                                                                        oFilterCriteriaCollection
                                                                       , reconciliationPeriodID.Value
                                                                       , companyID.Value
                                                                       , userID.Value
                                                                       , SessionHelper.CurrentRoleID.Value
                                                                       , Helper.IsCapabilityActivatedForCurrentRecPeriod(ARTEnums.Capability.DualLevelReview)
                                                                       , Helper.IsCapabilityActivatedForCurrentRecPeriod(ARTEnums.Capability.MaterialitySelection)
                                                                       , (short)ARTEnums.AccountAttribute.Preparer
                                                                       , (short)ARTEnums.AccountAttribute.Reviewer
                                                                       , (short)ARTEnums.AccountAttribute.Approver
                                                                       , (short)ARTEnums.UserRole.PREPARER
                                                                       , (short)ARTEnums.UserRole.REVIEWER
                                                                       , (short)ARTEnums.UserRole.APPROVER
                                                                       , (short)ARTEnums.UserRole.SYSTEM_ADMIN
                                                                       , (short)ARTEnums.UserRole.CEO_CFO
                                                                       , (short)ARTEnums.UserRole.SKYSTEM_ADMIN
                                                                       , defaultItemCount
                                                                       , Helper.GetAccountAttributeIDCollection(WebEnums.AccountPages.AccountViewer)
                                                                       , SessionHelper.GetUserLanguage()
                                                                       , SessionHelper.GetBusinessEntityID()
                                                                       , AppSettingHelper.GetDefaultLanguageID()
                                                                       , "AccountID"
                                                                       , "ASC"
                                                                       , Helper.GetAppUserInfo());

        ucSkyStemARTGrid.ShowStatusImageColumn = true;
        ucSkyStemARTGrid.Grid.AllowPaging = true;
        ucSkyStemARTGrid.Grid.ClientSettings.ClientEvents.OnRowSelecting = "Selecting";

        if (oGLDataHdrInfoCollection.Count < defaultItemCount)
        {
            ucSkyStemARTGrid.Grid.VirtualItemCount = oGLDataHdrInfoCollection.Count;
        }
        else
        {
            ucSkyStemARTGrid.Grid.VirtualItemCount = oGLDataHdrInfoCollection.Count + 1;
        }
        ARTEnums.UserRole eCurrentRole = (ARTEnums.UserRole)System.Enum.Parse(typeof(ARTEnums.UserRole), this._RoleID.ToString());
        if (eCurrentRole == ARTEnums.UserRole.SYSTEM_ADMIN)
        {
            ShowHideButtons(false);

        }
        else if (eCurrentRole == ARTEnums.UserRole.PREPARER || eCurrentRole == ARTEnums.UserRole.REVIEWER || eCurrentRole == ARTEnums.UserRole.APPROVER)
        {
            this.btnReopen.Visible = false;
            ShowHideButtons(true);
        }
        else
        {
            ShowHideButtons(false);
        }
        this.btnReopen.Visible = false;
        this.btnReset.Visible = false;
        ucSkyStemARTGrid.ShowSelectCheckBoxColum = false;
        countDisabledCheckboxes = 0;
        ucSkyStemARTGrid.CompanyID = companyID;
        ucSkyStemARTGrid.Grid.CurrentPageIndex = 0;
        ucSkyStemARTGrid.DataSource = oGLDataHdrInfoCollection;
        ucSkyStemARTGrid.BindGrid();
        HttpContext.Current.Session[SessionConstants.SEARCH_RESULTS_ACCOUNT_VIEWER_SRA] = oGLDataHdrInfoCollection;
        //this.SetVisibilityOfControlsForRole(oGLDataHdrInfoCollection);
    }
    private void SetVisibilityOfControlsForRole(IList<GLDataHdrInfo> oGLDataHdrInfoCollection)
    {
        ARTEnums.UserRole eCurrentRole = (ARTEnums.UserRole)System.Enum.Parse(typeof(ARTEnums.UserRole), this._RoleID.ToString());

        switch (AccountViewerHelper.GetFormMode(eRecPeriodStatus))
        {
            case WebEnums.FormMode.Edit:
                //if (eCurrentRole == ARTEnums.UserRole.PREPARER)
                //{
                //    if (
                //        (oGLDataHdrInfoCollection.Count < ucSkyStemARTGrid.Grid.PageSize
                //        && countDisabledCheckboxes < oGLDataHdrInfoCollection.Count)
                //    ||
                //        (oGLDataHdrInfoCollection.Count >= ucSkyStemARTGrid.Grid.PageSize
                //         && countDisabledCheckboxes < ucSkyStemARTGrid.Grid.PageSize)
                //    )
                //    {
                //        btnRemoveSignOff.Visible = true;
                //    }
                //}

                //if (
                //        (oGLDataHdrInfoCollection.Count < ucSkyStemARTGrid.Grid.PageSize
                //        && countDisabledCheckboxes < oGLDataHdrInfoCollection.Count)
                //    ||
                //        (oGLDataHdrInfoCollection.Count >= ucSkyStemARTGrid.Grid.PageSize
                //         && countDisabledCheckboxes < ucSkyStemARTGrid.Grid.PageSize)
                //    )
                //{
                //    btnSubmit.Visible = true;
                //}
                //else
                //    btnSubmit.Visible = false;
                ucSkyStemARTGrid.ShowSelectCheckBoxColum = true;
                break;

            default:

                break;
        }

    }
    public override string GetMenuKey()
    {
        return "SystemReconciledAccounts";
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

                    GridHelper.ShowHideColumns(GRID_COLUMN_INDEX_KEY_START, e.DetailTableView, SessionHelper.CurrentCompanyID, ARTEnums.Grid.AccountViewer, ucSkyStemARTGrid.Grid.AllowCustomization);
                    //int columnIndex = GridHelper.ShowHideColumnsBasedOnOrganizationalHierarchy(GRID_COLUMN_INDEX_KEY_START, e.DetailTableView, this._CompanyID);

                    //HandleGridCustomization(e, columnIndex);

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
    /// <summary>
    /// ShowHideButtons() is used to show/hide all buttons on page. 
    /// </summary>
    /// <param name="isShow"></param>
    private void ShowHideButtons(bool isShow)
    {
        ARTEnums.UserRole eCurrentRole = (ARTEnums.UserRole)System.Enum.Parse(typeof(ARTEnums.UserRole), this._RoleID.ToString());
        if (eCurrentRole == ARTEnums.UserRole.PREPARER || eCurrentRole == ARTEnums.UserRole.BACKUP_PREPARER)
        {
            btnAccept.LabelID = 1377;
            btnRemoveSignOff.Visible = isShow;
        }
        else
        {
            btnAccept.LabelID = 1481;
            btnRemoveSignOff.Visible = false;
        }
        btnSubmit.Visible = isShow;
        btnAccept.Visible = isShow;
    }
    protected void ucSkyStemARTGrid_GridItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {
        try
        {
            if (e.Item.ItemType == Telerik.Web.UI.GridItemType.Item || e.Item.ItemType == Telerik.Web.UI.GridItemType.AlternatingItem)
            {
                GLDataHdrInfo oGLDataHdrInfo = (GLDataHdrInfo)e.Item.DataItem;

                AccountViewerHelper.BindCommonFields(e, oGLDataHdrInfo);

                switch (e.Item.OwnerTableView.Name)
                {
                    case "NetAccountDetails":
                        // Apoorv: Hack, because the BindCommonFields method above will set it to "Net Account"
                        Helper.SetTextForHyperlink(e.Item, "hlAccountNumber", oGLDataHdrInfo.AccountNumber);
                        break;

                    default:
                        AccountViewerHelper.BindMasterGridFields(e, oGLDataHdrInfo, eRecPeriodStatus, Sel, ARTEnums.Grid.AccountViewerSRA, WebEnums.ARTPages.SystemReconciledAccounts, ref countDisabledCheckboxes);

                        //TODO : Patching code for fix Checkbox selection button visibility.

                        ARTEnums.UserRole eCurrentRole = (ARTEnums.UserRole)System.Enum.Parse(typeof(ARTEnums.UserRole), this._RoleID.ToString());

                        switch (AccountViewerHelper.GetFormMode(eRecPeriodStatus))
                        {
                            case WebEnums.FormMode.Edit:
                                //if (eCurrentRole == ARTEnums.UserRole.PREPARER)
                                //{
                                //    //Checking here to confirm that submit button should activate when Rec Status is Reviewed.
                                //    if (oGLDataHdrInfo.ReconciliationStatusID == (short)SkyStem.ART.Web.Data.WebEnums.ReconciliationStatus.Prepared)
                                //    {
                                //        btnRemoveSignOff.Visible = true;
                                //        btnSubmit.Visible = true;
                                //        // ShowHideButtons(true);
                                //    }
                                //}
                                //else if (eCurrentRole == ARTEnums.UserRole.REVIEWER)
                                //{
                                //    //Checking here to confirm that submit button should activate when Rec Status is Reviewed.
                                //    if (oGLDataHdrInfo.ReconciliationStatusID == (short)SkyStem.ART.Web.Data.WebEnums.ReconciliationStatus.Reviewed)
                                //    {
                                //        btnSubmit.Visible = true;
                                //    }
                                //}
                                //else if (eCurrentRole == ARTEnums.UserRole.APPROVER)
                                //{
                                //    //Checking here to confirm that submit button should activate when Rec Status is approved.
                                //    if (oGLDataHdrInfo.ReconciliationStatusID == (short)SkyStem.ART.Web.Data.WebEnums.ReconciliationStatus.Approved)
                                //    {
                                //        btnSubmit.Visible = true;
                                //    }
                                //}
                                //else if (eCurrentRole == ARTEnums.UserRole.SYSTEM_ADMIN)
                                //{
                                //    //Checking here to confirm that Reopen button should activate when Rec Status is Reconciled or SysReconciled .
                                //    if (oGLDataHdrInfo.ReconciliationStatusID == (short)SkyStem.ART.Web.Data.WebEnums.ReconciliationStatus.Reconciled || oGLDataHdrInfo.ReconciliationStatusID == (short)SkyStem.ART.Web.Data.WebEnums.ReconciliationStatus.SysReconciled)
                                //    {
                                //        btnReopen.Visible = true;
                                //    }
                                //}
                                if (eCurrentRole == ARTEnums.UserRole.SYSTEM_ADMIN)
                                {
                                    ShowHideButtons(false);
                                    this.btnReopen.Visible = true;
                                    this.btnReset.Visible = true;
                                    //Checking here to confirm that Reopen button should activate when Rec Status is Reconciled or SysReconciled .
                                    //if (oGLDataHdrInfo.ReconciliationStatusID == (short)SkyStem.ART.Web.Data.WebEnums.ReconciliationStatus.SysReconciled)
                                    //{
                                    //    this.btnReopen.Visible = AccountViewerHelper.ShowHideReopenAccountbtn(_RoleID, eRecPeriodStatus, IsCertificationStarted);
                                    //}
                                    //if (oGLDataHdrInfo.ReconciliationStatusID == (short)SkyStem.ART.Web.Data.WebEnums.ReconciliationStatus.Approved || oGLDataHdrInfo.ReconciliationStatusID == (short)SkyStem.ART.Web.Data.WebEnums.ReconciliationStatus.PendingApproval
                                    //    || oGLDataHdrInfo.ReconciliationStatusID == (short)SkyStem.ART.Web.Data.WebEnums.ReconciliationStatus.PendingModificationPreparer || oGLDataHdrInfo.ReconciliationStatusID == (short)SkyStem.ART.Web.Data.WebEnums.ReconciliationStatus.Reviewed
                                    //    || oGLDataHdrInfo.ReconciliationStatusID == (short)SkyStem.ART.Web.Data.WebEnums.ReconciliationStatus.PendingModificationReviewer || oGLDataHdrInfo.ReconciliationStatusID == (short)SkyStem.ART.Web.Data.WebEnums.ReconciliationStatus.PendingReview
                                    //    || oGLDataHdrInfo.ReconciliationStatusID == (short)SkyStem.ART.Web.Data.WebEnums.ReconciliationStatus.SysReconciled

                                    //    )
                                    //{
                                    //    this.btnReset.Visible = AccountViewerHelper.ShowHideReopenAccountbtn(_RoleID, eRecPeriodStatus, IsCertificationStarted);
                                    //}
                                }
                                else if (eCurrentRole == ARTEnums.UserRole.PREPARER || eCurrentRole == ARTEnums.UserRole.REVIEWER || eCurrentRole == ARTEnums.UserRole.APPROVER ||
                                    eCurrentRole == ARTEnums.UserRole.BACKUP_PREPARER || eCurrentRole == ARTEnums.UserRole.BACKUP_REVIEWER || eCurrentRole == ARTEnums.UserRole.BACKUP_APPROVER)
                                {
                                    this.btnReopen.Visible = false;
                                    ShowHideButtons(true);
                                }
                                else
                                {
                                    this.btnReopen.Visible = false;
                                    ShowHideButtons(false);
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
                            result = oGLDataClient.SaveGLDataReconciliationStatus(oGLDataIDCollection, this._ReconciliationPeriodID, (short)WebEnums.ReconciliationStatus.PendingApproval, SessionHelper.CurrentUserLoginID, DateTime.Now, (short)ARTEnums.ActionType.UpdateRecFromUI, (short)ARTEnums.ChangeSource.GLStatusChange, Helper.GetAppUserInfo());
                            AlertHelper.RaiseAlert(WebEnums.Alert.YouHaveXAccountsPendingApproval,
                                SessionHelper.CurrentReconciliationPeriodID, SessionHelper.CurrentReconciliationPeriodEndDate, 
                                oAccountIDCollection, oNetAccountIDCollection, this._RoleID, null);
                        }
                        else
                        {
                            result = oGLDataClient.SaveGLDataReconciliationStatus(oGLDataIDCollection, this._ReconciliationPeriodID, (short)WebEnums.ReconciliationStatus.SysReconciled, SessionHelper.CurrentUserLoginID, DateTime.Now, (short)ARTEnums.ActionType.UpdateRecFromUI, (short)ARTEnums.ChangeSource.GLStatusChange, Helper.GetAppUserInfo());
                        }
                    }
                }
                else if (this._RoleID == (short)ARTEnums.UserRole.APPROVER || this._RoleID == (short)ARTEnums.UserRole.BACKUP_APPROVER)
                {
                    oGLDataIDCollection = GetGLDataIDCollection((short)WebEnums.ReconciliationStatus.Approved);
                    if (oGLDataIDCollection.Count > 0)
                    {
                        result = oGLDataClient.SaveGLDataReconciliationStatus(oGLDataIDCollection, this._ReconciliationPeriodID, (short)WebEnums.ReconciliationStatus.SysReconciled, SessionHelper.CurrentUserLoginID, DateTime.Now, (short)ARTEnums.ActionType.UpdateRecFromUI, (short)ARTEnums.ChangeSource.GLStatusChange, Helper.GetAppUserInfo());
                    }
                }
                if (result)
                    ClearCheckedItemViewState();
                MasterPageBase oMasterPageBase = (MasterPageBase)this.Master;
                oMasterPageBase.ShowConfirmationMessage(1740);
                PopulateSRAGrid();
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
    protected void btnRemoveSignOff_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                List<long> oAccountIDCollection = new List<long>();
                List<int> oNetAccountIDCollection = new List<int>();
                List<long> oGLDataIDCollection = new List<long>();
                IGLData oGLDataClient = RemotingHelper.GetGLDataObject();
                if (this._RoleID == (short)ARTEnums.UserRole.PREPARER || this._RoleID == (short)ARTEnums.UserRole.BACKUP_PREPARER)
                {
                    oGLDataIDCollection = GetGLDataIDCollection((short)WebEnums.ReconciliationStatus.Prepared);
                    if (oGLDataIDCollection.Count > 0)
                    {
                        GetSelectedAccountIdCollection(oGLDataIDCollection, out  oAccountIDCollection, out  oNetAccountIDCollection);
                        oGLDataClient.UpdateGLDataForRemoveAccountSignOff(oAccountIDCollection, oNetAccountIDCollection, SessionHelper.CurrentReconciliationPeriodID, SessionHelper.CurrentUserLoginID, DateTime.Now, Helper.GetAppUserInfo());
                    }
                }
                PopulateSRAGrid();
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
    /// <summary>
    /// Handles user controls Need data source event
    /// </summary>
    /// <param name="count">Number of items needed to bind the grid</param>
    /// <returns>object</returns>
    protected object ucSkyStemARTGrid_Grid_NeedDataSourceEventHandler(int count)
    {
        List<GLDataHdrInfo> oGLDataHdrInfoCollection = null;
        try
        {
            oGLDataHdrInfoCollection = (List<GLDataHdrInfo>)HttpContext.Current.Session[SessionConstants.SEARCH_RESULTS_ACCOUNT_VIEWER_SRA];

            //int defaultItemCount = Convert.ToInt32(AppSettingHelper.GetAppSettingValue(AppSettingConstants.DEFAULT_CHUNK_SIZE));
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

                oGLDataHdrInfoCollection = oGLDataClient.SelectGLDataAndAccountInfoByUserIDForSRA(oFilterCriteriaCollection,
                this._ReconciliationPeriodID, this._CompanyID, this._UserID, this._RoleID, this._IsDualReviewEnabled, this._IsMaterialityEnabled,
                (short)ARTEnums.AccountAttribute.Preparer, (short)ARTEnums.AccountAttribute.Reviewer, (short)ARTEnums.AccountAttribute.Approver,
                (short)ARTEnums.UserRole.PREPARER, (short)ARTEnums.UserRole.REVIEWER, (short)ARTEnums.UserRole.APPROVER,
                (short)ARTEnums.UserRole.SYSTEM_ADMIN, (short)ARTEnums.UserRole.CEO_CFO, (short)ARTEnums.UserRole.SKYSTEM_ADMIN
                , count, Helper.GetAccountAttributeIDCollection(WebEnums.AccountPages.AccountViewer),
                SessionHelper.GetUserLanguage(), SessionHelper.GetBusinessEntityID(), AppSettingHelper.GetDefaultLanguageID(),
                sortExpression, sortDirection, Helper.GetAppUserInfo());

                HttpContext.Current.Session[SessionConstants.SEARCH_RESULTS_ACCOUNT_VIEWER_SRA] = oGLDataHdrInfoCollection;
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

    }
    protected void ucSkyStemARTGrid_PageIndexChangedEvent()
    {
        Sel.Value = string.Empty;
        SaveCheckBoxStates();
        this.btnReopen.Visible = false;
    }
    void ucSkyStemARTGrid_GridColumnSortingEvent()
    {
        SaveCheckBoxStates();
    }
    protected void Page_PreRender(object sender, EventArgs e)
    {
        RePopulateCheckBoxStates();

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
    protected void btnReopen_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            List<long> oGLDataIDCollection = new List<long>();
            oGLDataIDCollection = GetGLDataIDCollection((short)WebEnums.ReconciliationStatus.SysReconciled);
            if (oGLDataIDCollection.Count > 0)
            {
                IGLData oGLDataClient = RemotingHelper.GetGLDataObject();
                oGLDataClient.UpdateReOpenAccount(oGLDataIDCollection, SessionHelper.CurrentUserLoginID, DateTime.Now, (short)ARTEnums.ActionType.ReopenReconciliationFromUI, (short)ARTEnums.ChangeSource.ReopenReconciliation, Helper.GetAppUserInfo());
                MasterPageBase oMasterPageBase = (MasterPageBase)this.Master;
                oMasterPageBase.ShowConfirmationMessage(1740);
                PopulateSRAGrid();
            }
            MyHiddenField.Value = "";
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
                if (this._RoleID == (short)ARTEnums.UserRole.PREPARER || this._RoleID == (short)ARTEnums.UserRole.BACKUP_PREPARER)
                {
                    oGLDataIDCollection = GetGLDataIDCollection((short)WebEnums.ReconciliationStatus.PendingModificationPreparer);
                    if (oGLDataIDCollection.Count > 0)
                    {
                        result = oGLDataClient.SaveGLDataReconciliationStatus(oGLDataIDCollection, this._ReconciliationPeriodID, (short)WebEnums.ReconciliationStatus.Prepared, SessionHelper.CurrentUserLoginID, DateTime.Now, (short)ARTEnums.ActionType.UpdateRecFromUI, (short)ARTEnums.ChangeSource.GLStatusChange, Helper.GetAppUserInfo());
                    }
                }
                else if (this._RoleID == (short)ARTEnums.UserRole.REVIEWER || this._RoleID == (short)ARTEnums.UserRole.BACKUP_REVIEWER)
                {
                    oGLDataIDCollection = GetGLDataIDCollection((short)WebEnums.ReconciliationStatus.PendingReview);
                    oGLDataIDCollection.InsertRange(oGLDataIDCollection.Count, GetGLDataIDCollection((short)WebEnums.ReconciliationStatus.PendingModificationReviewer));
                    if (oGLDataIDCollection.Count > 0)
                    {
                        result = oGLDataClient.SaveGLDataReconciliationStatus(oGLDataIDCollection, this._ReconciliationPeriodID, (short)WebEnums.ReconciliationStatus.Reviewed, SessionHelper.CurrentUserLoginID, DateTime.Now, (short)ARTEnums.ActionType.UpdateRecFromUI, (short)ARTEnums.ChangeSource.GLStatusChange, Helper.GetAppUserInfo());
                    }
                }
                else if (this._RoleID == (short)ARTEnums.UserRole.APPROVER || this._RoleID == (short)ARTEnums.UserRole.BACKUP_APPROVER)
                {
                    oGLDataIDCollection = GetGLDataIDCollection((short)WebEnums.ReconciliationStatus.PendingApproval);
                    if (oGLDataIDCollection.Count > 0)
                    {
                        result = oGLDataClient.SaveGLDataReconciliationStatus(oGLDataIDCollection, this._ReconciliationPeriodID, (short)WebEnums.ReconciliationStatus.Approved, SessionHelper.CurrentUserLoginID, DateTime.Now, (short)ARTEnums.ActionType.UpdateRecFromUI, (short)ARTEnums.ChangeSource.GLStatusChange, Helper.GetAppUserInfo());
                    }
                }
                if (result)
                    ClearCheckedItemViewState();
                MasterPageBase oMasterPageBase = (MasterPageBase)this.Master;
                oMasterPageBase.ShowConfirmationMessage(1740);
                PopulateSRAGrid();
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
                oGLDataIDCollection.InsertRange(oGLDataIDCollection.Count, GetGLDataIDCollection((short)WebEnums.ReconciliationStatus.SysReconciled));
                ErrorMessage = string.Format(LanguageUtil.GetValue(5000323), LanguageUtil.GetValue(1097));
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
    protected void cvRemoveSignOff_ServerValidate(object source, ServerValidateEventArgs args)
    {
        try
        {
            // Update in ViewState             
            SaveCheckBoxStates();
            String ErrorMessage = "";
            List<long> oGLDataIDCollection = new List<long>();
            if (this._RoleID == (short)ARTEnums.UserRole.PREPARER || this._RoleID == (short)ARTEnums.UserRole.BACKUP_PREPARER)
            {
                oGLDataIDCollection.InsertRange(oGLDataIDCollection.Count, GetGLDataIDCollection((short)WebEnums.ReconciliationStatus.Prepared));
                ErrorMessage = string.Format(LanguageUtil.GetValue(5000323), LanguageUtil.GetValue(1089));
            }
            //else
            //{
            //    ErrorMessage = string.Format(LanguageUtil.GetValue(5000323), LanguageUtil.GetValue(1089));
            //}
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

    protected void btnReset_Click(object sender, EventArgs e)
    {
        // Moved the logic to RadAjaxManager handler after getting confirmation

        if (Page.IsValid)
        {
            List<long> oGLDataIDCollection = new List<long>();
            List<short> ReconciliationStatusIDList = new List<short>();
            ReconciliationStatusIDList.Add((short)WebEnums.ReconciliationStatus.Approved);
            ReconciliationStatusIDList.Add((short)WebEnums.ReconciliationStatus.PendingApproval);
            ReconciliationStatusIDList.Add((short)WebEnums.ReconciliationStatus.PendingModificationPreparer);
            ReconciliationStatusIDList.Add((short)WebEnums.ReconciliationStatus.PendingModificationReviewer);
            ReconciliationStatusIDList.Add((short)WebEnums.ReconciliationStatus.PendingReview);
            ReconciliationStatusIDList.Add((short)WebEnums.ReconciliationStatus.Reviewed);
            ReconciliationStatusIDList.Add((short)WebEnums.ReconciliationStatus.PendingReview);
            ReconciliationStatusIDList.Add((short)WebEnums.ReconciliationStatus.SysReconciled);

            oGLDataIDCollection = GetGLDataIDCollection(ReconciliationStatusIDList);
            if (oGLDataIDCollection.Count > 0)
            {
                IGLData oGLDataClient = RemotingHelper.GetGLDataObject();
                oGLDataClient.UpdateReSetAccount(oGLDataIDCollection, SessionHelper.CurrentUserLoginID, DateTime.Now, (short)ARTEnums.ActionType.ResetReconciliationFromUI, (short)ARTEnums.ChangeSource.ResetReconciliation, Helper.GetAppUserInfo());
                MasterPageBase oMasterPageBase = (MasterPageBase)this.Master;
                oMasterPageBase.ShowConfirmationMessage(2673);
                PopulateSRAGrid();
            }
            MyHiddenField.Value = "";
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
                ReconciliationStatusIDList.Add((short)WebEnums.ReconciliationStatus.Approved);
                ReconciliationStatusIDList.Add((short)WebEnums.ReconciliationStatus.PendingApproval);
                ReconciliationStatusIDList.Add((short)WebEnums.ReconciliationStatus.PendingModificationPreparer);
                ReconciliationStatusIDList.Add((short)WebEnums.ReconciliationStatus.PendingModificationReviewer);
                ReconciliationStatusIDList.Add((short)WebEnums.ReconciliationStatus.PendingReview);
                ReconciliationStatusIDList.Add((short)WebEnums.ReconciliationStatus.Reviewed);
                ReconciliationStatusIDList.Add((short)WebEnums.ReconciliationStatus.PendingReview);
                ReconciliationStatusIDList.Add((short)WebEnums.ReconciliationStatus.SysReconciled);

                oGLDataIDCollection = GetGLDataIDCollection(ReconciliationStatusIDList);
                ErrorMessage = LanguageUtil.GetValue(2672);
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

}//end of class
