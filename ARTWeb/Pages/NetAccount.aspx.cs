using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SkyStem.ART.Web.Utility;
using SkyStem.ART.Web.Classes;
using SkyStem.ART.Client.Model;
using SkyStem.Library.Controls.WebControls;
using SkyStem.Library.Controls.TelerikWebControls;
using SkyStem.Library.Controls.TelerikWebControls.Common;
using System.Data.SqlClient;
using SkyStem.ART.Web.Data;
using SkyStem.Language.LanguageUtility;
using SkyStem.ART.Client.IServices;
using Telerik.Web.UI;
using SkyStem.ART.Client.Data;
using SkyStem.ART.Client.Exception;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.ServiceModel;
using System.Collections;
using SkyStem.ART.Client.Params;


public partial class Pages_NetAccount : PageBaseRecPeriod
{
    #region Variables & Constants
    static bool _isCapabilityNetAccount = false;
    #endregion

    #region Properties
    public NetAccountHdrInfo SelectedNetAccountHdrInfo
    {
        get
        {
            if (NetAccountHdrInfoList != null && ddlNetAccount.SelectedValue != WebConstants.CREATE_NEW
                && ddlNetAccount.SelectedValue != WebConstants.SELECT_ONE)
                return NetAccountHdrInfoList.Find
                    (obj => obj.NetAccountID == Convert.ToInt32(ddlNetAccount.SelectedValue));
            else return null;
        }
    }

    public List<NetAccountHdrInfo> NetAccountHdrInfoList
    {
        get { return (List<NetAccountHdrInfo>)Session[SessionConstants.NET_ACCOUNT_LIST]; }
        set { Session[SessionConstants.NET_ACCOUNT_LIST] = value; }
    }

    public AccountHdrInfo AccountHdrInfoToComapare
    {
        get
        {
            if (AccountHdrInfoListAdded != null && AccountHdrInfoListAdded.Count > 0)
                return AccountHdrInfoListAdded[0];
            else
                return null;
        }
    }

    public List<AccountHdrInfo> AccountHdrInfoListAdded
    {
        get { return (List<AccountHdrInfo>)Session[SessionConstants.NET_ACCOUNT_ASSOCIATION]; }
        set { Session[SessionConstants.NET_ACCOUNT_ASSOCIATION] = value; }
    }

    public List<AccountHdrInfo> AccountHdrInfoListSearchResults
    {
        get { return (List<AccountHdrInfo>)Session[SessionConstants.SEARCH_RESULTS_NETACCOUNT_ASSOCIATION]; }
        set { Session[SessionConstants.SEARCH_RESULTS_NETACCOUNT_ASSOCIATION] = value; }
    }
    #endregion

    #region Delegates & Events
    #endregion

    #region Page Events
    protected void Page_Init(object sender, EventArgs e)
    {
        MasterPageBase ompage = (MasterPageBase)this.Master;
        ompage.ReconciliationPeriodChangedEventHandler += new EventHandler(ompage_ReconciliationPeriodChangedEventHandler);

        ucSkyStemARTGridAccountsAdded.GridItemDataBound += new Telerik.Web.UI.GridItemEventHandler(ucNetAcSkyStemARTGrid_GridItemDataBound);
        ucSkyStemARTGridAccountSearchResult.GridItemDataBound += new GridItemEventHandler(ucSkyStemARTGridAccountSearchResult_GridItemDataBound);
        ucSkyStemARTGridAccountSearchResult.Grid_NeedDataSourceEventHandler += new UserControls_SkyStemARTGrid.Grid_NeedDataSource(ucSkyStemARTGrid_Grid_NeedDataSourceEventHandler);
        ucSkyStemARTGridAccountsAdded.Grid_NeedDataSourceEventHandler += new UserControls_SkyStemARTGrid.Grid_NeedDataSource(ucSkyStemARTGridAccountsAdded_Grid_NeedDataSourceEventHandler);

        ucAccountSearchControl.SearchClickEventHandler += new UserControls_AccountSearchControl.ShowSearchResults(ucAccountSearchControl_SearchClickEventHandler);
        ucAccountSearchControl.PnlMassAndBulk.Visible = false;
        ucAccountSearchControl.ShowMissing.Visible = false;
        ucAccountSearchControl.PnlSearchAndMail.Visible = false;
        ucAccountSearchControl.ShowDueDaysRow = true;
        ucAccountSearchControl.ParentPage = WebEnums.AccountPages.NetAccount;
        ucAccountSearchControl.ExcludeNetAccount = true;

    }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            Helper.SetPageTitle(this, 1358);
            Helper.HideMessage(this);
            SetCapabilityFlags(SessionHelper.CurrentReconciliationPeriodID);
            if (!IsPostBack && (ddlNetAccount.SelectedValue == WebConstants.SELECT_ONE || ddlNetAccount.SelectedValue == ""))
            {
                rowCreateNewNetAc.Visible = false;
                pnlNetAccountAttributes.Visible = false;
                pnlNetAccountGrid.Visible = false;
                btnDeleteNetAccount.Visible = false;
            }
            string[] oAccountID = { "AccountID" };
            ucSkyStemARTGridAccountSearchResult.Grid.MasterTableView.DataKeyNames = oAccountID;
            ucSkyStemARTGridAccountsAdded.Grid.MasterTableView.DataKeyNames = oAccountID;
            ucSkyStemARTGridAccountsAdded.CompanyID = SessionHelper.CurrentCompanyID;
            ucSkyStemARTGridAccountSearchResult.CompanyID = SessionHelper.CurrentCompanyID;

            SetErrorMessagesForValidationControls();

            ScriptHelper.AddJSForConfirmDelete(btnRemoveAccount);
            ScriptHelper.AddJSForConfirmDelete(btnDeleteNetAccount, 5000331);
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

    #region Grid Events
    #region NetAccountSkyStemGrid
    void ucNetAcSkyStemARTGrid_GridItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {
        try
        {
            if (e.Item.ItemType == Telerik.Web.UI.GridItemType.Item || e.Item.ItemType == Telerik.Web.UI.GridItemType.AlternatingItem)
            {
                AccountHdrInfo oAccountHdrInfo = (AccountHdrInfo)e.Item.DataItem;
                AccountViewerHelper.BindCommonFields(e, oAccountHdrInfo, "AddedGrid");

                //HACK: Vinay - Override Account# as It may come as "net account"
                Helper.SetTextForLabel(e.Item, "lblAccountNumberAddedGrid", oAccountHdrInfo.AccountNumber);

                if (!Helper.IsCapabilityActivatedForCurrentRecPeriod(ARTEnums.Capability.RiskRating))
                {
                    UserControls_PopupRecFrequency ucPopupRecFrequency = (UserControls_PopupRecFrequency)e.Item.FindControl("ucPopupRecFrequencyAddedGrid");
                    ucPopupRecFrequency.AccountID = oAccountHdrInfo.AccountID.Value;
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
        try
        {
            int totalCount = 0;
            if (AccountHdrInfoListSearchResults != null)
                totalCount = AccountHdrInfoListSearchResults.Count;

            if (AccountHdrInfoListAdded != null)
                totalCount += AccountHdrInfoListAdded.Count;
            //int defaultItemCount = Convert.ToInt32(AppSettingHelper.GetAppSettingValue(AppSettingConstants.DEFAULT_CHUNK_SIZE));
            int defaultItemCount = Helper.GetDefaultChunkSize(ucSkyStemARTGridAccountSearchResult.Grid.PageSize);

            if (totalCount <= count - defaultItemCount)
            {
                AccountSearchCriteria oAccountSearchCriteria = (AccountSearchCriteria)HttpContext.Current.Session[SessionConstants.ACCOUNT_SEARCH_CRITERIA];

                if (oAccountSearchCriteria != null)
                {
                    if (ucSkyStemARTGridAccountSearchResult.Grid.MasterTableView.SortExpressions != null && ucSkyStemARTGridAccountSearchResult.Grid.MasterTableView.SortExpressions.Count > 0)
                    {
                        GridHelper.GetSortExpressionAndDirection(oAccountSearchCriteria, ucSkyStemARTGridAccountSearchResult.Grid.MasterTableView);
                    }

                    oAccountSearchCriteria.Count = count;
                    AccountHdrInfoListSearchResults = NetAccountHelper.SearchAccounts(oAccountSearchCriteria);

                    if (AccountHdrInfoListAdded != null && AccountHdrInfoListAdded.Count > 0 && AccountHdrInfoListSearchResults != null && AccountHdrInfoListSearchResults.Count > 0)
                    {
                        foreach (AccountHdrInfo oAccountHdrInfo in AccountHdrInfoListAdded)
                        {
                            for (int i = AccountHdrInfoListSearchResults.Count - 1; i < AccountHdrInfoListSearchResults.Count; i--)
                            {
                                if (AccountHdrInfoListSearchResults[i].AccountID == oAccountHdrInfo.AccountID)
                                    AccountHdrInfoListSearchResults.Remove(AccountHdrInfoListSearchResults[i]);
                            }
                        }
                    }
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

        return AccountHdrInfoListSearchResults;
    }
    #endregion

    #region GridAccountSearchResult
    void ucSkyStemARTGridAccountSearchResult_GridItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {
        try
        {
            if (e.Item.ItemType == Telerik.Web.UI.GridItemType.Item || e.Item.ItemType == Telerik.Web.UI.GridItemType.AlternatingItem)
            {
                AccountHdrInfo oAccountHdrInfo = (AccountHdrInfo)e.Item.DataItem;

                AccountViewerHelper.BindCommonFields(e, oAccountHdrInfo);


                //HACK: Vinay - Override Account# as It may come as "net account"
                Helper.SetTextForLabel(e.Item, "lblAccountNumber", oAccountHdrInfo.AccountNumber);
                Helper.SetTextForLabel(e.Item, "lblCreationDate", Helper.GetDisplayDate(oAccountHdrInfo.CreationPeriodEndDate));


                if (!Helper.IsCapabilityActivatedForCurrentRecPeriod(ARTEnums.Capability.RiskRating))
                {
                    UserControls_PopupRecFrequency ucPopupRecFrequency = (UserControls_PopupRecFrequency)e.Item.FindControl("ucPopupRecFrequency");
                    ucPopupRecFrequency.AccountID = oAccountHdrInfo.AccountID.Value;
                }

                //set style for those row for which which NetAccount attributes has same value as of
                //any of the row in top grid.Any of the row => that the row in top grid would have 
                //all the net NetAccount attribute same, already.
                CheckBox checkBox = (CheckBox)(e.Item as GridDataItem)["CheckboxSelectColumn"].Controls[0];
                checkBox.Enabled = false;
                // Only accounts that are in Not Started Stated, Not Is Locked and Have All required attriutes set can be added
                if ((!oAccountHdrInfo.ReconciliationStatusID.HasValue
                    || oAccountHdrInfo.ReconciliationStatusID.GetValueOrDefault() == (Int16)WebEnums.ReconciliationStatus.NotStarted
                    || (oAccountHdrInfo.IsSystemReconcilied.GetValueOrDefault()
                        && oAccountHdrInfo.ReconciliationStatusID.GetValueOrDefault() == (Int16)WebEnums.ReconciliationStatus.Prepared
                       )
                    )
                    && !oAccountHdrInfo.IsLocked.GetValueOrDefault()
                    && NetAccountHelper.IsAccountAttributesValidForNetAccount(oAccountHdrInfo)
                    && oAccountHdrInfo.CreationPeriodEndDate.HasValue && SessionHelper.CurrentReconciliationPeriodEndDate.HasValue && oAccountHdrInfo.CreationPeriodEndDate.Value <= SessionHelper.CurrentReconciliationPeriodEndDate.Value)
                {
                    if (AccountHdrInfoToComapare == null)
                        checkBox.Enabled = true;
                    else if (NetAccountHelper.IsAccountAttributesMatchForNetAccount(AccountHdrInfoToComapare, oAccountHdrInfo))
                    {
                        checkBox.Enabled = true;
                        e.Item.CssClass = "NetAccountAttributesSame";
                    }
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
    #endregion

    #region ARTGridAccountsAdded
    protected object ucSkyStemARTGridAccountsAdded_Grid_NeedDataSourceEventHandler(int count)
    {
        try
        {
            if (AccountHdrInfoListAdded == null || AccountHdrInfoListAdded.Count == 0)
            {
                AccountHdrInfoListAdded = NetAccountHelper.GetNetAccountAssociatedAccounts((int)SessionHelper.CurrentReconciliationPeriodID, Convert.ToInt32(ddlNetAccount.SelectedValue));
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

        return AccountHdrInfoListAdded;
    }
    #endregion

    #endregion

    #region Other Events
    protected void btnAdd_OnClick(object sender, EventArgs e)
    {
        AddAccount();
    }

    protected void btnSave_OnClick(object sender, EventArgs e)
    {
        if (AccountHdrInfoListAdded != null && AccountHdrInfoListAdded.Count > 0)
        {
            if (NetAccountHelper.IsAllNetAccountOwnersActive(AccountHdrInfoListAdded))
            {
                SaveNetAccount(false);
            }
            else
            {
                MasterPageBase oMasterPageBase = (MasterPageBase)this.Master;
                oMasterPageBase.ShowErrorMessage(5000345);
            }
        }
        else
        {
            MasterPageBase oMasterPageBase = (MasterPageBase)this.Master;
            oMasterPageBase.ShowErrorMessage(5000045);
        }

    }

    protected void btnRemoveAccount_OnClick(object sender, EventArgs e)
    {
        try
        {
            RemoveAccountFromNetAccount();
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

    protected void btnDeleteNetAccount_Click(object sender, EventArgs e)
    {
        try
        {
            DeleteNetAccount();

            //refresh Net Account Drop Down List
            PopulateNetAccountDropDown();
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

    protected void ddlNetAccount_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            LoadNetAccount();
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

    void ucAccountSearchControl_SearchClickEventHandler(List<AccountHdrInfo> oAccountHdrInfoCollection)
    {
        SearchAccounts(oAccountHdrInfoCollection);
    }

    public void ompage_ReconciliationPeriodChangedEventHandler(object sender, EventArgs e)
    {
        try
        {
            InitPage();
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
    #endregion

    #region Private Methods
    private void SaveNetAccount(bool IsEditMode)
    {
        try
        {
            bool isValidPage = false;
            try
            {
                isValidPage = Page.IsValid;
            }
            catch (Exception)
            { isValidPage = false; }
            if (isValidPage || IsEditMode)
            {
                bool bCreateMode = false;
                if (ddlNetAccount.SelectedValue == WebConstants.CREATE_NEW)
                    bCreateMode = true;
                if (bCreateMode)
                {
                    if (txtNetAcName.Text == "")
                        throw new ARTException(1776);
                    else if (IsNetAccountDuplicate(txtNetAcName.Text.Trim()))
                        throw new ARTException(1777);
                }
                UserHdrInfo objUserHdrInfo = SessionHelper.GetCurrentUser();
                NetAccountHdrInfo objNetAccountHdrInfo = null;
                if (bCreateMode)
                {
                    objNetAccountHdrInfo = new NetAccountHdrInfo();
                    int labelIDValue = (int)LanguageUtil.InsertPhrase(txtNetAcName.Text, null, 1, (int)SessionHelper.CurrentCompanyID, SessionHelper.GetUserLanguage(), 4, null);
                    objNetAccountHdrInfo.NetAccountLabelID = labelIDValue;
                    objNetAccountHdrInfo.NetAccount = txtNetAcName.Text;
                    objNetAccountHdrInfo.DateAdded = DateTime.Now;
                    objNetAccountHdrInfo.IsActive = true;
                    objNetAccountHdrInfo.CompanyID = SessionHelper.CurrentCompanyID;

                    objNetAccountHdrInfo.AddedBy = objUserHdrInfo.LoginID;
                    objNetAccountHdrInfo.Description = txtNetAcName.Text;
                    objNetAccountHdrInfo.IsAccountSelectionChanged = true;
                }
                else
                    objNetAccountHdrInfo = SelectedNetAccountHdrInfo;

                objNetAccountHdrInfo.NetAccountAttributeValueInfoList = this.GetNetAccountAttributeValueInfoCollection();

                int? iNetAccountId = NetAccountHelper.UpdateNetAccount(objNetAccountHdrInfo, AccountHdrInfoListAdded, (int)SessionHelper.CurrentCompanyID, (int)SessionHelper.CurrentReconciliationPeriodID, (DateTime)SessionHelper.CurrentReconciliationPeriodEndDate, SessionHelper.CurrentUserLoginID);

                // Update Net Account ID
                if (bCreateMode)
                {
                    objNetAccountHdrInfo.NetAccountID = iNetAccountId;
                }

                // Refresh Dropdown and reselect
                if (bCreateMode)
                {
                    //Refresh Dropdown
                    PopulateNetAccountDropDown();
                    ddlNetAccount.SelectedValue = objNetAccountHdrInfo.NetAccountID.ToString();
                    // Raise Drop Selection Change Event
                    ddlNetAccount_OnSelectedIndexChanged(null, null);

                }
                if (AccountHdrInfoListAdded != null)
                {
                    pnlNetAccountGrid.Visible = true;
                }
                SelectedNetAccountHdrInfo.IsAccountSelectionChanged = false;
                string msg = string.Format(LanguageUtil.GetValue(1762));
                Helper.ShowConfirmationMessage(this, msg);
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

    private void LoadNetAccount()
    {
        ClearControls();
        if (SelectedNetAccountHdrInfo != null)
        {
            txtNetAcName.Text = ddlNetAccount.SelectedItem.ToString();
            lblNetAccountName.Text = ddlNetAccount.SelectedItem.ToString();
            SelectedNetAccountHdrInfo.IsAccountSelectionChanged = false;

            this.LoadNetAccountAttributesValue();

            AccountHdrInfoListAdded = NetAccountHelper.GetNetAccountAssociatedAccounts(Convert.ToInt32(ddlNetAccount.SelectedValue), (int)SessionHelper.CurrentReconciliationPeriodID);
            BindAccountAddedGrid();
        }
        SetControlState();
    }

    protected void LoadNetAccountAttributesValue()
    {
        //Get Net Account Attributes Value 1. Account Policy Url 2.Reconciliation Procedure
        //3. Account Description
        List<NetAccountAttributeValueInfo> oNetAccountAttributeValueInfoList = NetAccountHelper.GetNetAccountAttributeValues(Convert.ToInt32(ddlNetAccount.SelectedValue), (int)SessionHelper.CurrentCompanyID, (int)SessionHelper.CurrentReconciliationPeriodID);
        oNetAccountAttributeValueInfoList.ForEach(obj =>
        {
            ARTEnums.AccountAttribute enumAccountAttribute = (ARTEnums.AccountAttribute)obj.AccountAttributeID;
            switch (enumAccountAttribute)
            {
                case ARTEnums.AccountAttribute.AccountPolicyURL:
                    ucAccountPolicyURL.EditorControl.Content = LanguageUtil.GetValue(obj.ValueLabelID);
                    ucAccountPolicyURL.EditorControl.Attributes.Add(WebConstants.ATTRIBUTE_LABEL_ID, obj.ValueLabelID.ToString());
                    break;
                case ARTEnums.AccountAttribute.ReconciliationProcedure:
                    ucReconciliationProcedure.EditorControl.Content = LanguageUtil.GetValue(obj.ValueLabelID);
                    ucReconciliationProcedure.EditorControl.Attributes.Add(WebConstants.ATTRIBUTE_LABEL_ID, obj.ValueLabelID.ToString());
                    break;
                case ARTEnums.AccountAttribute.Description:
                    ucDescription.EditorControl.Content = LanguageUtil.GetValue(obj.ValueLabelID);
                    ucDescription.EditorControl.Attributes.Add(WebConstants.ATTRIBUTE_LABEL_ID, obj.ValueLabelID.ToString());
                    break;
            }
        }
        );
    }

    private void PopulateNetAccountDropDown()
    {
        NetAccountSearchParamInfo oNetAccountSearchParamInfo = new NetAccountSearchParamInfo();
        Helper.FillCommonServiceParams(oNetAccountSearchParamInfo);
        NetAccountHdrInfoList = NetAccountHelper.GetNetAccounts(oNetAccountSearchParamInfo);
        if (NetAccountHdrInfoList != null && NetAccountHdrInfoList.Count > 0)
        {
            NetAccountHdrInfoList.ForEach(obj =>
            {
                obj.NetAccountLabelText = Helper.GetLabelIDValue((int)obj.NetAccountLabelID);
            }
            );
        }
        //NetAccountHdrInfoList = NetAccountHdrInfoList.OrderBy(obj => obj.NetAccountLabelText).ToList();
        BindNetAccountDropDown();
    }

    private void BindNetAccountDropDown()
    {
        ddlNetAccount.DataSource = NetAccountHdrInfoList;
        ddlNetAccount.DataTextField = "NetAccountLabelText";
        ddlNetAccount.DataValueField = "NetAccountID";
        ddlNetAccount.DataBind();

        ddlNetAccount.Items.Remove(new ListItem { Value = WebConstants.SELECT_ONE, Text = LanguageUtil.GetValue(1343) });
        ListControlHelper.AddListItemForSelectOne(ddlNetAccount);

        AddCreateNewItem();
    }

    private void DeleteNetAccount()
    {
        try
        {
            if (ddlNetAccount.SelectedValue != WebConstants.CREATE_NEW || ddlNetAccount.SelectedValue == WebConstants.SELECT_ONE
                && SessionHelper.CurrentReconciliationPeriodID != null
                && SessionHelper.CurrentCompanyID != null)
            {
                NetAccountParamInfo oNetAccountParamInfo = CreateNetAccountParamInfo();
                oNetAccountParamInfo.AccountHdrInfoList = AccountHdrInfoListAdded;
                oNetAccountParamInfo.DateRevised = DateTime.Now;
                int? iCount = NetAccountHelper.DeleteNetAccount(oNetAccountParamInfo);
                InitPage();
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

    private NetAccountParamInfo CreateNetAccountParamInfo()
    {
        NetAccountParamInfo oNetAccountParamInfo = new NetAccountParamInfo();
        oNetAccountParamInfo.NetAccountID = Convert.ToInt32(ddlNetAccount.SelectedValue);
        oNetAccountParamInfo.CompanyID = SessionHelper.CurrentCompanyID;
        oNetAccountParamInfo.RecPeriodID = SessionHelper.CurrentReconciliationPeriodID;
        oNetAccountParamInfo.UserLoginID = SessionHelper.GetCurrentUser().LoginID;
        oNetAccountParamInfo.RecPeriodEndDate = SessionHelper.CurrentReconciliationPeriodEndDate;
        return oNetAccountParamInfo;
    }

    private void SearchAccounts(List<AccountHdrInfo> oAccountHdrInfoCollection)
    {
        try
        {
            //AccountSearchCriteria oAccountSearchCriteria = this.GetSearchCriteria();

            AccountHdrInfoListSearchResults = oAccountHdrInfoCollection;
            int defaultItemCount = Helper.GetDefaultChunkSize(ucSkyStemARTGridAccountSearchResult.Grid.PageSize);

            ucSkyStemARTGridAccountSearchResult.Visible = true;
            ucSkyStemARTGridAccountSearchResult.ShowSelectCheckBoxColum = true;
            btnAdd.Visible = true;

            if (AccountHdrInfoListAdded != null && AccountHdrInfoListAdded.Count > 0)
            {
                AccountHdrInfoListSearchResults.RemoveAll(obj => AccountHdrInfoListAdded.Exists(objr => objr.AccountID == obj.AccountID) == true);
            }

            if (AccountHdrInfoListSearchResults.Count == defaultItemCount)
            {
                ucSkyStemARTGridAccountSearchResult.Grid.VirtualItemCount = AccountHdrInfoListSearchResults.Count + 1;
            }
            else
            {
                ucSkyStemARTGridAccountSearchResult.Grid.VirtualItemCount = AccountHdrInfoListSearchResults.Count;
            }

            ucSkyStemARTGridAccountSearchResult.DataSource = AccountHdrInfoListSearchResults;
            ucSkyStemARTGridAccountSearchResult.BindGrid();
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

    private void RemoveAccountFromNetAccount()
    {
        List<AccountHdrInfo> oRemoveNetAccountHdrInfoCollection = new List<AccountHdrInfo>();

        foreach (GridDataItem item in ucSkyStemARTGridAccountsAdded.Grid.SelectedItems)
        {
            int accountID = int.Parse(item.GetDataKeyValue("AccountID").ToString());
            if (AccountHdrInfoListAdded != null && AccountHdrInfoListAdded.Count > 0)
            {
                oRemoveNetAccountHdrInfoCollection.Add(AccountHdrInfoListAdded.Find(T => T.AccountID == accountID));
            }
        }

        //if rows selected in top grid
        if (oRemoveNetAccountHdrInfoCollection.Count > 0)
        {
            //Remove from Database
            if (ddlNetAccount.SelectedValue != WebConstants.CREATE_NEW && ddlNetAccount.SelectedValue != WebConstants.SELECT_ONE)
            {
                NetAccountParamInfo oNetAccountParamInfo = CreateNetAccountParamInfo();
                oNetAccountParamInfo.AccountHdrInfoList = oRemoveNetAccountHdrInfoCollection;
                oNetAccountParamInfo.DateRevised = DateTime.Now;
                NetAccountHelper.RemoveAccountFromNetAccount(oNetAccountParamInfo);
            }

            //remove selected row from top grid
            AccountHdrInfoListAdded.RemoveAll(obj => oRemoveNetAccountHdrInfoCollection.Exists(objr => objr.AccountID == obj.AccountID) == true);

            BindGrids();
        }

        else
        {
            // pnlNetAccountGrid.Visible = false;
            throw new ARTException(5000186);
        }
    }
    private void BindAccountAddedGrid()
    {
        ucSkyStemARTGridAccountsAdded.DataSource = AccountHdrInfoListAdded;
        ucSkyStemARTGridAccountsAdded.BindGrid();
    }

    private void BindAccountSearchGrid()
    {
        ucSkyStemARTGridAccountSearchResult.DataSource = AccountHdrInfoListSearchResults;
        ucSkyStemARTGridAccountSearchResult.BindGrid();
    }
    private void AddAccount()
    {
        try
        {
            AccountHdrInfo oAccountHdrInfoToComapare = AccountHdrInfoToComapare;
            // if no records in AccountHdrAdded grid
            if (oAccountHdrInfoToComapare == null)
            {
                if (AccountHdrInfoListSearchResults != null && AccountHdrInfoListSearchResults.Count > 0)
                {
                    if (ucSkyStemARTGridAccountSearchResult.Grid.SelectedIndexes.Count > 0)
                    {
                        GridDataItem gItem = (GridDataItem)ucSkyStemARTGridAccountSearchResult.Grid.SelectedItems[0];
                        int accountID = int.Parse(gItem.GetDataKeyValue("AccountID").ToString());
                        oAccountHdrInfoToComapare = AccountHdrInfoListSearchResults.Find(r => r.AccountID == accountID);
                    }
                }
            }

            List<int> lstSelectedAccountId = new List<int>();
            bool IsSelectedItemAttributeSame = true;
            bool IsSelectedItemsAttributeContainValidValue = true;
            //check whether attribute of selected Account are same or not 
            foreach (GridDataItem item in ucSkyStemARTGridAccountSearchResult.Grid.SelectedItems)
            {
                int accountID = int.Parse(item.GetDataKeyValue("AccountID").ToString());
                //AccountHdrInfo oAccountHdrInfo = oAccountClient.GetAccountHdrInfoByAccountID(accountID, SessionHelper.CurrentCompanyID.Value, SessionHelper.CurrentReconciliationPeriodID.Value);
                AccountHdrInfo oAccountHdrInfo = AccountHdrInfoListSearchResults.Find(r => r.AccountID == accountID);
                if (NetAccountHelper.IsAccountAttributesValidForNetAccount(oAccountHdrInfo))
                {
                    if (accountID != oAccountHdrInfoToComapare.AccountID)
                    {
                        if (!NetAccountHelper.IsAccountAttributesMatchForNetAccount(oAccountHdrInfoToComapare, oAccountHdrInfo))
                        {
                            IsSelectedItemAttributeSame = false;
                            break;
                        }
                    }
                    lstSelectedAccountId.Add(accountID);
                }
                else
                {
                    IsSelectedItemsAttributeContainValidValue = false;
                    break;
                }
            }
            if (!IsSelectedItemsAttributeContainValidValue)
            {
                MasterPageBase oMasterPageBase = (MasterPageBase)this.Master;
                oMasterPageBase.ShowErrorMessage(5000224);
            }
            List<long> SelActIDlist = new List<long>();

            List<long> AccountsHaveDiffrentAttributeValueInFuture=null;
            if (lstSelectedAccountId != null && lstSelectedAccountId.Count > 0)
            {
                for (int i = 0; i < lstSelectedAccountId.Count; i++)
                {
                    SelActIDlist.Add(Convert.ToInt64(lstSelectedAccountId[i]));
                }
                AccountsHaveDiffrentAttributeValueInFuture = NetAccountHelper.GetAccountsHaveDiffrentAttributeValueInFuture(SelActIDlist);
              
            }
            if (AccountsHaveDiffrentAttributeValueInFuture != null && AccountsHaveDiffrentAttributeValueInFuture.Count > 0)
            {
                MasterPageBase oMasterPageBase = (MasterPageBase)this.Master;
                oMasterPageBase.ShowErrorMessage(5000416);
            }
            else
            {

                if (IsSelectedItemAttributeSame) //If all the selected account's Attributes are same
                {
                    if (AccountHdrInfoListAdded == null)
                        AccountHdrInfoListAdded = new List<AccountHdrInfo>();

                    //add selected row from bottom grid to top grid
                    AccountHdrInfoListAdded.AddRange
                        (
                        AccountHdrInfoListSearchResults.FindAll
                       (obj => lstSelectedAccountId.Contains((int)obj.AccountID) == true)
                        );

                    //remove selected row from bottom grid
                    AccountHdrInfoListSearchResults.RemoveAll(obj => lstSelectedAccountId.Contains((int)obj.AccountID) == true);
                    BindGrids();
                    pnlNetAccountGrid.Visible = true;
                    ucSkyStemARTGridAccountsAdded.Visible = true;
                    btnSave.Visible = true;
                    btnRemoveAccount.Visible = true;
                    if (lstSelectedAccountId.Count > 0 && SelectedNetAccountHdrInfo != null)
                    {
                        SelectedNetAccountHdrInfo.IsAccountSelectionChanged = true;
                    }
                }
                else
                {
                    MasterPageBase oMasterPageBase = (MasterPageBase)this.Master;
                    oMasterPageBase.ShowErrorMessage(5000225);
                }

                if (ddlNetAccount.SelectedValue != WebConstants.CREATE_NEW)
                {
                    if (NetAccountHelper.IsAllNetAccountOwnersActive(AccountHdrInfoListAdded))
                    {
                        SaveNetAccount(true);
                    }
                    else
                    {
                        MasterPageBase oMasterPageBase = (MasterPageBase)this.Master;
                        oMasterPageBase.ShowErrorMessage(5000345);
                    }
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

    private void ClearGrids()
    {
        AccountHdrInfoListAdded = null;
        AccountHdrInfoListSearchResults = null;
        BindGrids();
    }

    private void BindGrids()
    {
        ucSkyStemARTGridAccountSearchResult.DataSource = AccountHdrInfoListSearchResults;
        ucSkyStemARTGridAccountSearchResult.BindGrid();
        ucSkyStemARTGridAccountsAdded.DataSource = AccountHdrInfoListAdded;
        ucSkyStemARTGridAccountsAdded.BindGrid();
    }

    protected void ChangeControlStateOnSelectedIndexChange()
    {
        bool periodLocked = false;
        if (CertificationHelper.IsCertificationStarted()
            || (CurrentRecProcessStatus.Value != WebEnums.RecPeriodStatus.NotStarted
            && CurrentRecProcessStatus.Value != WebEnums.RecPeriodStatus.Open
            && CurrentRecProcessStatus.Value != WebEnums.RecPeriodStatus.InProgress))
            periodLocked = true;

        bool createMode = false;
        if (ddlNetAccount.SelectedValue == WebConstants.CREATE_NEW)
            createMode = true;

        bool viewMode = false;
        if (SelectedNetAccountHdrInfo != null)
            viewMode = true;

        bool editMode = false;
        // Not Locked, Is Null (GL not available) or In Not Started State
        if (viewMode && SessionHelper.CurrentRoleEnum == ARTEnums.UserRole.SYSTEM_ADMIN
                && !SelectedNetAccountHdrInfo.IsLocked.GetValueOrDefault()
                && (!SelectedNetAccountHdrInfo.ReconciliationStatusID.HasValue // GL not available
                    || SelectedNetAccountHdrInfo.ReconciliationStatusID.GetValueOrDefault() == (short)WebEnums.ReconciliationStatus.NotStarted // Not Started
                    || (SelectedNetAccountHdrInfo.ReconciliationStatusID.GetValueOrDefault() == (short)WebEnums.ReconciliationStatus.Prepared && SelectedNetAccountHdrInfo.IsSystemReconciled.GetValueOrDefault()) // SRA and Prepared
                    )
            //&& SelectedNetAccountHdrInfo.AddedBy == SessionHelper.CurrentUserLoginID                
            )
            editMode = true;

        btnDeleteNetAccount.Visible = (!periodLocked && editMode);
        rowCreateNewNetAc.Visible = (createMode || editMode);
        txtNetAcName.Visible = createMode;
        lblNetAccountName.Visible = viewMode || editMode;
        pnlNetAccountAttributes.Visible = (createMode || editMode || viewMode);
        cpeAccountSearch.Collapsed = !(createMode || editMode);
        pnlSearchGrid.Visible = (!periodLocked && (createMode || editMode));

        // DO NOT REMOVE: (VINAY) This is needed to set here for renedering the Editor Control 
        // properly after post back from Rec Period change drop down.
        ucAccountPolicyURL.EditorControl.EnableAjaxSkinRendering = true;
        ucDescription.EditorControl.EnableAjaxSkinRendering = true;
        ucReconciliationProcedure.EditorControl.EnableAjaxSkinRendering = true;

        EditModes editorDisplayMode = EditModes.Preview;
        if (!periodLocked && (createMode || editMode))
            editorDisplayMode = EditModes.Design;
        ucAccountPolicyURL.EditorControl.EditModes = editorDisplayMode;
        ucReconciliationProcedure.EditorControl.EditModes = editorDisplayMode;
        ucDescription.EditorControl.EditModes = editorDisplayMode;

        pnlNetAccountGrid.Visible = editMode || viewMode;
        ucSkyStemARTGridAccountsAdded.ShowSelectCheckBoxColum = !periodLocked;
        ucSkyStemARTGridAccountsAdded.Visible = editMode || viewMode;
        btnRemoveAccount.Visible = (!periodLocked && editMode);
        btnSave.Visible = (!periodLocked && editMode);

        ucSkyStemARTGridAccountSearchResult.Visible = false;
        btnAdd.Visible = false;
    }

    public override string GetMenuKey()
    {
        return "CreateNetAccount";
    }

    private AccountSearchCriteria GetSearchCriteria()
    {
        var oAccountSearchCriteria = HttpContext.Current.Session[SessionConstants.ACCOUNT_SEARCH_CRITERIA] as AccountSearchCriteria;
        oAccountSearchCriteria.PageID = (int)WebEnums.AccountPages.NetAccount;
        oAccountSearchCriteria.ExcludeNetAccount = true;

        //oAccountSearchCriteria.FromAccountNumber = txtAcNumber.Text;
        //oAccountSearchCriteria.ToAccountNumber = txtAcNumber.Text;
        //oAccountSearchCriteria.RiskRatingID = Convert.ToInt32(ucDDLRiskRating.SelectedValue);
        //if (optIsKeyAccountYes.Checked)
        //    oAccountSearchCriteria.IsKeyccount = true;
        //else if (optIsKeyAccountNo.Checked)
        //    oAccountSearchCriteria.IsKeyccount = false;
        //else
        //    oAccountSearchCriteria.IsKeyccount = null;

        //oAccountSearchCriteria.FSCaption = txtFsCaption.Text;
        //oAccountSearchCriteria.AccountName = ExTextBox1.Text;
        //oAccountSearchCriteria.Count = Convert.ToInt32(AppSettingHelper.GetAppSettingValue(AppSettingConstants.DEFAULT_CHUNK_SIZE));
        //oAccountSearchCriteria.ReconciliationPeriodID = (int)SessionHelper.CurrentReconciliationPeriodID;
        //oAccountSearchCriteria.BusinessEntityID = SessionHelper.GetBusinessEntityID();
        //oAccountSearchCriteria.LCID = SessionHelper.GetUserLanguage();
        //oAccountSearchCriteria.DefaultLanguageID = AppSettingHelper.GetDefaultLanguageID();
        //oAccountSearchCriteria.IsShowOnlyAccountMissingAttributes = false;

        //oAccountSearchCriteria.CompanyID = SessionHelper.CurrentCompanyID.Value;
        //oAccountSearchCriteria.ExcludeNetAccount = true;
        //oAccountSearchCriteria.PageID = (int)WebEnums.AccountPages.NetAccount;
        //oAccountSearchCriteria.UserID = SessionHelper.CurrentUserID.Value;
        //oAccountSearchCriteria.UserRoleID = SessionHelper.CurrentRoleID.Value;
        //oAccountSearchCriteria.IsDualReviewEnabled = Helper.IsCapabilityActivatedForCurrentRecPeriod(ARTEnums.Capability.DualLevelReview);
        //oAccountSearchCriteria.IsKeyAccountEnabled = Helper.IsCapabilityActivatedForCurrentRecPeriod(ARTEnums.Capability.KeyAccount);
        //oAccountSearchCriteria.IsZeroBalanceAccountEnabled = Helper.IsCapabilityActivatedForCurrentRecPeriod(ARTEnums.Capability.ZeroBalanceAccount);
        //oAccountSearchCriteria.IsRiskRatingEnabled = Helper.IsCapabilityActivatedForCurrentRecPeriod(ARTEnums.Capability.RiskRating);



        ////User cannot search Un-Reconcilable Accounts while creating Net Account.
        //oAccountSearchCriteria.IsReconcilableEnabled = true;
        //oAccountSearchCriteria.IsReconcilable = true;

        return oAccountSearchCriteria;
    }

    private List<NetAccountAttributeValueInfo> GetNetAccountAttributeValueInfoCollection()
    {
        int? lableID = null;
        UserHdrInfo objUserHdrInfo = SessionHelper.GetCurrentUser();
        NetAccountAttributeValueInfo oNetAccountAttributeValueInfo;
        // add NetAccountAttribute list here
        List<NetAccountAttributeValueInfo> lstNetAccountAttributeValueInfo = new List<NetAccountAttributeValueInfo>();
        //add AccountPolicyURL attribute to list
        oNetAccountAttributeValueInfo = new NetAccountAttributeValueInfo();
        oNetAccountAttributeValueInfo.AccountAttributeID = (int)ARTEnums.AccountAttribute.AccountPolicyURL;
        oNetAccountAttributeValueInfo.Value = ucAccountPolicyURL.EditorControl.Text;
        //?? check what it return in case phrase already exist.
        // Check if either labelid is 0 or label text changed from earlier value than create new label instead of modify older one
        lableID = Convert.ToInt32(ucAccountPolicyURL.EditorControl.Attributes[WebConstants.ATTRIBUTE_LABEL_ID]);
        if (lableID != null && lableID.HasValue && !LanguageUtil.GetValue(lableID.Value).Equals(this.ucAccountPolicyURL.EditorControl.Content, StringComparison.InvariantCultureIgnoreCase))
            lableID = 0;
        oNetAccountAttributeValueInfo.ValueLabelID = (int)LanguageUtil.InsertPhrase(ucAccountPolicyURL.EditorControl.Text, null, AppSettingHelper.GetApplicationID(), (int)SessionHelper.CurrentCompanyID, SessionHelper.GetUserLanguage(), 4, lableID);

        lstNetAccountAttributeValueInfo.Add(oNetAccountAttributeValueInfo);

        //add ReconciliationProcedure attribute to list
        oNetAccountAttributeValueInfo = new NetAccountAttributeValueInfo();
        oNetAccountAttributeValueInfo.AccountAttributeID = (int)ARTEnums.AccountAttribute.ReconciliationProcedure;
        oNetAccountAttributeValueInfo.Value = ucReconciliationProcedure.EditorControl.Text;

        lableID = Convert.ToInt32(ucReconciliationProcedure.EditorControl.Attributes[WebConstants.ATTRIBUTE_LABEL_ID]);
        if (lableID != null && lableID.HasValue && !LanguageUtil.GetValue(lableID.Value).Equals(this.ucReconciliationProcedure.EditorControl.Content, StringComparison.InvariantCultureIgnoreCase))
            lableID = 0;
        oNetAccountAttributeValueInfo.ValueLabelID = (int)LanguageUtil.InsertPhrase(ucReconciliationProcedure.EditorControl.Text, null, AppSettingHelper.GetApplicationID(), (int)SessionHelper.CurrentCompanyID, SessionHelper.GetUserLanguage(), 4, lableID);

        lstNetAccountAttributeValueInfo.Add(oNetAccountAttributeValueInfo);

        //add Description attribute to list
        oNetAccountAttributeValueInfo = new NetAccountAttributeValueInfo();
        oNetAccountAttributeValueInfo.AccountAttributeID = (int)ARTEnums.AccountAttribute.Description;
        oNetAccountAttributeValueInfo.Value = ucDescription.EditorControl.Text;

        lableID = Convert.ToInt32(ucDescription.EditorControl.Attributes[WebConstants.ATTRIBUTE_LABEL_ID]);
        if (lableID != null && lableID.HasValue && !LanguageUtil.GetValue(lableID.Value).Equals(this.ucDescription.EditorControl.Content, StringComparison.InvariantCultureIgnoreCase))
            lableID = 0;
        oNetAccountAttributeValueInfo.ValueLabelID = (int)LanguageUtil.InsertPhrase(ucDescription.EditorControl.Text, null, AppSettingHelper.GetApplicationID(), (int)SessionHelper.CurrentCompanyID, SessionHelper.GetUserLanguage(), 4, lableID);

        lstNetAccountAttributeValueInfo.Add(oNetAccountAttributeValueInfo);

        //add IsActive attribute to list
        oNetAccountAttributeValueInfo = new NetAccountAttributeValueInfo();
        oNetAccountAttributeValueInfo.AccountAttributeID = (int)ARTEnums.AccountAttribute.IsActive;
        oNetAccountAttributeValueInfo.Value = "1";

        lstNetAccountAttributeValueInfo.Add(oNetAccountAttributeValueInfo);

        return lstNetAccountAttributeValueInfo;
    }

    private bool IsNetAccountDuplicate(string strNetAccountName)
    {
        return NetAccountHelper.IsNetAccountDuplicate(strNetAccountName, (int)SessionHelper.CurrentCompanyID, (int)SessionHelper.CurrentReconciliationPeriodID);
    }

    /// <summary>
    /// Set Capability Flags
    /// </summary>
    /// <param name="recPeriodID"></param>
    private void SetCapabilityFlags(int? recPeriodID)
    {
        _isCapabilityNetAccount = Helper.IsCapabilityActivatedForRecPeriodID(ARTEnums.Capability.NetAccount, (int)recPeriodID, false);
    }

    /// <summary>
    /// Init Page
    /// </summary>
    private void InitPage()
    {
        ClearControls();
        SetCapabilityFlags(SessionHelper.CurrentReconciliationPeriodID);
        if (_isCapabilityNetAccount)
        {
            PopulateNetAccountDropDown();
        }
        SetControlState();
    }

    /// <summary>
    /// Clear Controls
    /// </summary>
    private void ClearControls()
    {
        txtNetAcName.Text = string.Empty;
        ucAccountPolicyURL.EditorControl.Content = string.Empty;
        ucReconciliationProcedure.EditorControl.Content = string.Empty;
        ucDescription.EditorControl.Content = string.Empty;
        ClearGrids();
    }

    /// <summary>
    /// Set Control State
    /// </summary>
    private void SetControlState()
    {
        pnlContent.Visible = _isCapabilityNetAccount;
        pnlCapabilityNotActivatedMsg.Visible = !_isCapabilityNetAccount;
        ChangeControlStateOnSelectedIndexChange();
    }

    /// <summary>
    /// Add Create New Item
    /// </summary>
    private void AddCreateNewItem()
    {
        if (_isCapabilityNetAccount)
        {
            if ((CurrentRecProcessStatus.Value == WebEnums.RecPeriodStatus.NotStarted
                || CurrentRecProcessStatus.Value == WebEnums.RecPeriodStatus.Open
                || CurrentRecProcessStatus.Value == WebEnums.RecPeriodStatus.InProgress)
                && !CertificationHelper.IsCertificationStarted())
            {
                ddlNetAccount.Items.Remove(new ListItem { Value = WebConstants.CREATE_NEW, Text = LanguageUtil.GetValue(1622) });
                ListControlHelper.AddListItemForCreateNew(ddlNetAccount, 1);
            }
            else
            {
                ddlNetAccount.Items.Remove(new ListItem { Value = WebConstants.CREATE_NEW, Text = LanguageUtil.GetValue(1622) });
            }
        }
    }

    private void SetErrorMessagesForValidationControls()
    {
        this.txtNetAcName.ErrorMessage = Helper.GetErrorMessage(WebEnums.FieldType.MandatoryField, this.lblNetAcName.LabelID);
    }
    #endregion

    #region Other Methods
    /// <summary>
    /// This method is used to auto populate FS Caption text box based on the basis of 
    /// the prefix text typed in the text box
    /// </summary>
    /// <param name="prefixText">The text which was typed in the text box</param>
    /// <param name="count">Number of results to be returned</param>
    /// <returns>List of FS Caption</returns>
    [System.Web.Services.WebMethod()]
    [System.Web.Script.Services.ScriptMethod()]
    public static string[] AutoCompleteFSCaption(string prefixText, int count)
    {
        string[] oFSCaptionCollection = null;
        try
        {
            if (SessionHelper.CurrentCompanyID.HasValue)
            {
                int companyId = SessionHelper.CurrentCompanyID.Value;
                IFSCaption oFSCaptionClient = RemotingHelper.GetFSCaptioneObject();
                oFSCaptionCollection = oFSCaptionClient.SelectFSCaptionByCompanyIDAndPrefixText(companyId, prefixText, count
                    , SessionHelper.GetUserLanguage(), SessionHelper.GetBusinessEntityID(), AppSettingHelper.GetDefaultLanguageID(), Helper.GetAppUserInfo());

                if (oFSCaptionCollection == null || oFSCaptionCollection.Length == 0)
                {
                    oFSCaptionCollection = new string[] { "No Records Found" };
                }
            }
        }
        catch (ARTException ex)
        {
            Helper.ShowErrorMessage(null, ex);
            throw ex;
        }
        catch (Exception ex)
        {
            Helper.ShowErrorMessage(null, ex);
            throw ex;
        }

        return oFSCaptionCollection;
    }


    /// <summary>
    /// This method is used to auto populate User Name text box based on the basis of 
    /// the prefix text typed in the text box
    /// </summary>
    /// <param name="prefixText">The text which was typed in the text box</param>
    /// <param name="count">Number of results to be returned</param>
    /// <returns>List of User Names</returns>
    [System.Web.Services.WebMethod()]
    [System.Web.Script.Services.ScriptMethod()]
    public static List<string> AutoCompleteUserName(string prefixText, int count)
    {
        List<string> UserNames = new List<string>();

        try
        {
            if (SessionHelper.CurrentCompanyID.HasValue)
            {
                int companyId = SessionHelper.CurrentCompanyID.Value;
                IUser oUserClient = RemotingHelper.GetUserObject();
                List<UserHdrInfo> oUserHdrInfoList = oUserClient.SelectActiveUserHdrInfoByCompanyIdAndPrefixText(companyId, prefixText, count, Helper.GetAppUserInfo());
                for (int i = 0; i < oUserHdrInfoList.Count; i++)
                {
                    string item = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(oUserHdrInfoList[i].Name.ToString(), oUserHdrInfoList[i].UserID.ToString());
                    UserNames.Add(item);
                }
                if (oUserHdrInfoList == null || oUserHdrInfoList.Count == 0)
                {
                    string item = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem("No Records Found", "0");
                    UserNames.Add(item);
                }
            }
        }
        catch (ARTException ex)
        {
            PopupHelper.ShowErrorMessage(null, ex);
            throw ex;
        }
        catch (Exception ex)
        {
            PopupHelper.ShowErrorMessage(null, ex);
            throw ex;
        }

        return UserNames;
    }
    #endregion


}
