using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SkyStem.ART.Web.Utility;
using SkyStem.ART.Web.Classes;
using SkyStem.Library.Controls.WebControls;
using System.Data;
using SkyStem.ART.Client.Model;
using SkyStem.ART.Client.IServices;
using SkyStem.Language.LanguageUtility;
using Telerik.Web.UI;
using SkyStem.ART.Web.Data;
using SkyStem.ART.Client.Exception;
using SkyStem.ART.Client.Data;
using System.Web.UI.HtmlControls;
using SkyStem.Library.Controls.TelerikWebControls.Data;
using SkyStem.Library.Controls.TelerikWebControls;
using System.Text;

public partial class Pages_AccountOwnership : PageBaseRecPeriod
{
    #region Variable & Constant
    bool _IsDualReviewEnabled;
    bool _IsDualLevelReviewByAccountEnabled;
    #region Constants

    private const string GRID_ONROWSELECTED_EVENT_VALUE = "ValidateUserInput";
    private const string GRID_ONROWDESELECTED_EVENT_VALUE = "DevalidateUserInput";
    private const string GRID_ONROWCREATED_EVENT_VALUE = "OnRowCreated";
    private const string LABEL_ACCOUNT_NUMBER = "lblAccountNumber";
    private const string LABEL_NET_ACCOUNT = "lblNetAccount";
    private const string USERCONTROL_PREPARER = "ucPreparer";
    private const string USERCONTROL_REVIEWER = "ucReviewer";
    private const string USERCONTROL_APPROVER = "ucApprover";
    private const string MENU_KEY = "AccountOwnership";
    private const string DEFAULT_STRING_FOR_SEARCH = "No Records found";
    private const string COLUMN_NAME_ID = "ID";
    private const string COLUMN_NAME_NETACCOUNTID = "NetAccountID";
    private const string BLANK_TEXT_HYPHEN = "-";
    bool _IsMassUpdateClicked;

    #endregion
    #endregion

    #region Properties Declarations
    #region Private
    private List<CompanyCapabilityInfo> _CompanyCapabilityInfoCollection = null;
    #endregion
    #region Public
    public static short? SelectedAttributeID
    {
        get;
        set;
    }
    #endregion
    #endregion

    #region Page Events Handler
    protected void Page_Init(object sender, EventArgs e)
    {
        MasterPageBase ompage = (MasterPageBase)this.Master;
        ompage.ReconciliationPeriodChangedEventHandler += new EventHandler(ompage_ReconciliationPeriodChangedEventHandler);
    }

    /// <summary>
    /// Initializes controls value on page load
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            setErrorMessage();
            ScriptManager.GetCurrent(Page).RegisterPostBackControl(btnSave);
            ScriptManager.GetCurrent(Page).RegisterPostBackControl(btnCancel);
            Helper.ShowInputRequirementSection(this, 2496);
            this._IsMassUpdateClicked = pnlMassUpdate.Visible;
            //Helper.IsCapabilityActivatedForCurrentRecPeriod(
            //Helper.GetFeatureCapabilityMode(
            //btnSearchMassUpdate,btnSearchBulkUpdate
            ucAccountSearchControl.PnlSearchAndMail.Visible = true;
            //ExButton btnSearchAndMail = ((ExButton)ucAccountSearchControl.FindControl("btnSearchAndMail"));
            //if (btnSearchAndMail != null)
            //{
            //    btnSearchAndMail.Visible = true;
            //}


            GridHelper.ShowHideColumnsBasedOnFeatureCapability(ucSkyStemARTGrid.RgAccount.CreateTableView());
            Helper.SetPageTitle(this, 1212);
            ucAccountSearchControl.PnlMassAndBulk.Visible = true;
            ucAccountSearchControl.PnlSearch.Visible = false;
            ucAccountSearchControl.ChkBoxlabelID = 1705;
            ucAccountSearchControl.ShowMissingBackupOwners = true;

            ucAccountSearchControl.SearchAndMailClickEventHandler += new UserControls_AccountSearchControl.SearchAndMail(ucAccountSearchControl_SearchAndMailClickEventHandler);
            // ucAccountSearchControl.SearchClickEventHandler += new UserControls_AccountSearchControl.ShowSearchResults(ucAccountSearchControl_SearchClickEventHandler);
            ucAccountSearchControl.BulkUpdateClickEventHandler += new UserControls_AccountSearchControl.ShowSearchResults(ucAccountSearchControl_BulkUpdateClickEventHandler);
            ucAccountSearchControl.MassUpdateClickEventHandler += new UserControls_AccountSearchControl.ShowSearchResults(ucAccountSearchControl_MassUpdateClickEventHandler);
            ucAccountSearchControl.ParentPage = WebEnums.AccountPages.AccountOwnership;
            ucSkyStemARTGrid.GridItemDataBound += new Telerik.Web.UI.GridItemEventHandler(ucSkyStemARTGrid_GridItemDataBound);
            ucSkyStemARTGrid.GridCommand += new Telerik.Web.UI.GridCommandEventHandler(ucSkyStemARTGrid_GridItemCommand);

            ucSkyStemARTGrid.Grid_NeedDataSourceEventHandler += new UserControls_SkyStemARTGrid.Grid_NeedDataSource(ucSkyStemARTGrid_Grid_NeedDataSourceEventHandler);
            SkyStemARTGridMassUpdate.Grid_NeedDataSourceEventHandler += new UserControls_SkyStemARTGrid.Grid_NeedDataSource(SkyStemARTGridMassUpdate_Grid_NeedDataSourceEventHandler);
            ucSkyStemARTGrid.Grid.ClientSettings.ClientEvents.OnRowSelected = GRID_ONROWSELECTED_EVENT_VALUE;
            ucSkyStemARTGrid.Grid.ClientSettings.ClientEvents.OnRowDeselected = GRID_ONROWDESELECTED_EVENT_VALUE;
            ucSkyStemARTGrid.Grid.ClientSettings.ClientEvents.OnRowCreated = GRID_ONROWCREATED_EVENT_VALUE;
            ucSkyStemARTGrid.Grid.EntityNameLabelID = 1071;

            //ucSkyStemARTGrid.Grid.SortCommand += new GridSortCommandEventHandler(ucSkyStemARTGrid_SortCommand);


            this._CompanyCapabilityInfoCollection = SessionHelper.GetCompanyCapabilityCollectionForCurrentRecPeriod();
            this._IsDualReviewEnabled = (from capability in _CompanyCapabilityInfoCollection
                                         where capability.CapabilityID.HasValue
                                         && capability.IsActivated.HasValue
                                         && capability.CapabilityID.Value == (short)ARTEnums.Capability.DualLevelReview
                                         select capability.IsActivated.Value).FirstOrDefault();
            this._IsDualLevelReviewByAccountEnabled = Helper.IsDualLevelReviewByAccountActivated();



            // if (this._IsDualReviewEnabled == false)
            if (Helper.GetFeatureCapabilityMode(WebEnums.Feature.DualLevelReview, ARTEnums.Capability.DualLevelReview, SessionHelper.CurrentReconciliationPeriodID) == WebEnums.FeatureCapabilityMode.Hidden)
            {
                GridColumn oGridColumn = ucSkyStemARTGrid.Grid.Columns.FindByUniqueNameSafe("Approver");
                if (oGridColumn != null)
                {
                    oGridColumn.Visible = false;
                }
            }

            GridColumn oGridColumnBkpPreparer = ucSkyStemARTGrid.Grid.Columns.FindByUniqueNameSafe("BackupPreparer");
            GridColumn oGridColumnBkpReviewer = ucSkyStemARTGrid.Grid.Columns.FindByUniqueNameSafe("BackupReviewer");
            GridColumn oGridColumnBkpApprover = ucSkyStemARTGrid.Grid.Columns.FindByUniqueNameSafe("BackupApprover");

            if (Helper.IsFeatureActivated(WebEnums.Feature.AccountOwnershipBackup, SessionHelper.CurrentReconciliationPeriodID))
            {
                oGridColumnBkpPreparer.Visible = true;
                oGridColumnBkpReviewer.Visible = true;
                if (Helper.IsCapabilityActivatedForCurrentRecPeriod(ARTEnums.Capability.DualLevelReview))
                {
                    oGridColumnBkpApprover.Visible = true;
                }
                else
                {
                    oGridColumnBkpApprover.Visible = false;
                }
            }
            else
            {
                oGridColumnBkpPreparer.Visible = false;
                oGridColumnBkpReviewer.Visible = false;
                oGridColumnBkpApprover.Visible = false;
            }
        }
        catch (Exception ex)
        {
            Helper.ShowErrorMessage(this, ex);
        }
    }

    #endregion

    #region Grid Events SkyStemARTGrid
    /// <summary>
    /// Handles rad grids item data bound event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ucSkyStemARTGrid_GridItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {
        try
        {
            ListItem selectOne = new ListItem(LanguageUtil.GetValue(1343), WebConstants.SELECT_ONE);
            if (e.Item.ItemType == Telerik.Web.UI.GridItemType.Item || e.Item.ItemType == Telerik.Web.UI.GridItemType.AlternatingItem)
            {
                AccountHdrInfo oAccountHdrInfo = (AccountHdrInfo)e.Item.DataItem;

                ExLabel lblAccountNumber = (ExLabel)e.Item.FindControl(LABEL_ACCOUNT_NUMBER);
                lblAccountNumber.Text = HttpUtility.HtmlEncode(oAccountHdrInfo.AccountNumber);

                ExLabel lblAccountName = (ExLabel)e.Item.FindControl("lblAccountName");
                lblAccountName.Text = HttpUtility.HtmlEncode(oAccountHdrInfo.AccountName);

                ExCheckBox chkExcludeOwnershipForZBA = (ExCheckBox)e.Item.FindControl("chkExcludeOwnershipForZBA");
                HtmlInputText txtExcludeOwbershipValue = (HtmlInputText)e.Item.FindControl("txtExcludeOwnershipValue");
                chkExcludeOwnershipForZBA.Enabled = false;

                if (oAccountHdrInfo.AccountGLBalance.HasValue && oAccountHdrInfo.AccountGLBalance.Value == 0M
                    && oAccountHdrInfo.IsZeroBalance.HasValue && oAccountHdrInfo.IsZeroBalance.Value)
                {
                    chkExcludeOwnershipForZBA.Visible = true;
                }
                else
                {
                    chkExcludeOwnershipForZBA.Visible = false;
                }

                if (oAccountHdrInfo.IsExcludeOwnershipForZBA.HasValue && oAccountHdrInfo.IsExcludeOwnershipForZBA.Value)
                {
                    chkExcludeOwnershipForZBA.Checked = true;
                }

                ExLabel lblNetAccount = (ExLabel)e.Item.FindControl(LABEL_NET_ACCOUNT);

                if (!string.IsNullOrEmpty(oAccountHdrInfo.NetAccount))
                {
                    lblNetAccount.Text = HttpUtility.HtmlEncode(oAccountHdrInfo.NetAccount);
                }
                else
                {
                    lblNetAccount.Text = BLANK_TEXT_HYPHEN;
                }

                UserControls_UserDropDown ddlPreparer = (UserControls_UserDropDown)e.Item.FindControl(USERCONTROL_PREPARER);
                UserControls_UserDropDown ddlBackUpPreparer = (UserControls_UserDropDown)e.Item.FindControl("ucBackupPreparer");
                ddlBackUpPreparer.Enabled = false;

                UserControls_UserDropDown ddlBackUpApprover = (UserControls_UserDropDown)e.Item.FindControl("ucBackupApprover");
                ddlBackUpApprover.Enabled = false;

                UserControls_UserDropDown ddlReviewer = (UserControls_UserDropDown)e.Item.FindControl(USERCONTROL_REVIEWER);
                UserControls_UserDropDown ddlBackupReviewer = (UserControls_UserDropDown)e.Item.FindControl("ucBackupReviewer");
                ddlBackupReviewer.Enabled = false;

                //UserControls_UserDropDown ddlBackupApprover = (UserControls_UserDropDown)e.Item.FindControl("ucBackupApprover");
                //ddlBackupApprover.Enabled = false;

                CustomValidator vldComparePreparerAndReviewer = (CustomValidator)e.Item.FindControl("vldComparePreparerAndReviewer");
                vldComparePreparerAndReviewer.ErrorMessage = Helper.GetLabelIDValue(5000086);
                //if (SessionHelper.CurrentRecProcessStatusEnum == WebEnums.RecPeriodStatus.Closed
                //    || SessionHelper.CurrentRecProcessStatusEnum == WebEnums.RecPeriodStatus.InProgress
                //    || SessionHelper.CurrentRecProcessStatusEnum == WebEnums.RecPeriodStatus.Skipped)
                //{
                ddlPreparer.Enabled = false;
                ddlReviewer.Enabled = false;
                //}
                CheckBox checkBox = (CheckBox)(e.Item as GridDataItem)["CheckboxSelectColumn"].Controls[0];
                if (!oAccountHdrInfo.IsLocked.GetValueOrDefault()
                    && (CurrentRecProcessStatus.Value == WebEnums.RecPeriodStatus.NotStarted
                        || CurrentRecProcessStatus.Value == WebEnums.RecPeriodStatus.Open
                        || CurrentRecProcessStatus.Value == WebEnums.RecPeriodStatus.InProgress
                    ))
                {
                    if (checkBox != null)
                    {
                        checkBox.Enabled = true;
                    }
                }
                else
                {
                    if (checkBox != null)
                    {
                        checkBox.Enabled = false;
                        Sel.Value += e.Item.ItemIndex.ToString() + ":";
                    }
                }
                //string onSelectedIndexChanged = "return OnReviewerSelectedIndexChange('" + ddlPreparer.ClientID + "','" + ddlReviewer.ClientID + "','" + vldComparePreparerAndReviewer.ClientID + "','" + checkBox.ClientID + "','" + WebConstants.SELECT_ONE + "');";
                //ddlReviewer.AddAttributes = new KeyValuePair<string, string>("onchange", onSelectedIndexChanged);
                ExLabel lblPreparerExport = (ExLabel)e.Item.FindControl("lblPreparerExport");
                if (oAccountHdrInfo.PreparerUserID.HasValue && oAccountHdrInfo.PreparerUserID.Value > 0)
                {
                    ddlPreparer.SelectedValue = oAccountHdrInfo.PreparerUserID.Value.ToString();
                    lblPreparerExport.Text = HttpUtility.HtmlEncode(ddlPreparer.SelectedItem.Text);
                }
                else
                {
                    lblPreparerExport.Text = "-";
                }

                ExLabel lblReviewerExport = (ExLabel)e.Item.FindControl("lblReviewerExport");
                if (oAccountHdrInfo.ReviewerUserID.HasValue && oAccountHdrInfo.ReviewerUserID.Value > 0)
                {
                    ddlReviewer.SelectedValue = oAccountHdrInfo.ReviewerUserID.Value.ToString();
                    lblReviewerExport.Text = HttpUtility.HtmlEncode(ddlReviewer.SelectedItem.Text);
                }
                else
                {
                    lblReviewerExport.Text = "-";
                }

                #region BackupRoles Section

                ExLabel lblBackupPreparerExport = (ExLabel)e.Item.FindControl("lblBackupPreparerExport");
                if (oAccountHdrInfo.BackupPreparerUserID.HasValue && oAccountHdrInfo.BackupPreparerUserID.Value > 0)
                {
                    ddlBackUpPreparer.SelectedValue = oAccountHdrInfo.BackupPreparerUserID.Value.ToString();
                    lblBackupPreparerExport.Text = HttpUtility.HtmlEncode(ddlBackUpPreparer.SelectedItem.Text);
                }
                else
                {
                    lblBackupPreparerExport.Text = "-";
                }

                ExLabel lblBackupReviewerExport = (ExLabel)e.Item.FindControl("lblBackupReviewerExport");
                if (oAccountHdrInfo.BackupReviewerUserID.HasValue && oAccountHdrInfo.BackupReviewerUserID.Value > 0)
                {
                    ddlBackupReviewer.SelectedValue = oAccountHdrInfo.BackupReviewerUserID.Value.ToString();
                    lblBackupReviewerExport.Text = HttpUtility.HtmlEncode(ddlBackupReviewer.SelectedItem.Text);
                }
                else
                {
                    lblBackupReviewerExport.Text = "-";
                }

                #endregion

                UserControls_UserDropDown ddlApprover = (UserControls_UserDropDown)e.Item.FindControl(USERCONTROL_APPROVER);
                ddlApprover.Enabled = false;
                if (Helper.IsDualLevelReviewByAccountActivated())
                    ddlApprover.AddAttributes = new KeyValuePair<string, string>("IsOptional", "1");

                CustomValidator vldCompareApproverAndReviewer = (CustomValidator)e.Item.FindControl("vldCompareApproverAndReviewer");
                CustomValidator vldComparePreparerAndApprover = (CustomValidator)e.Item.FindControl("vldComparePreparerAndApprover");

                CustomValidator vldCompareBackupPreparer = (CustomValidator)e.Item.FindControl("vldCompareBackupPreparer");
                CustomValidator vldCompareBackupReviewer = (CustomValidator)e.Item.FindControl("vldCompareBackupReviewer");
                CustomValidator vldCompareBackupApprover = (CustomValidator)e.Item.FindControl("vldCompareBackupApprover");

                if (vldComparePreparerAndApprover != null)
                    vldComparePreparerAndApprover.ErrorMessage = Helper.GetLabelIDValue(5000086);
                if (vldCompareApproverAndReviewer != null)
                    vldCompareApproverAndReviewer.ErrorMessage = Helper.GetLabelIDValue(5000086);
                if (vldCompareBackupPreparer != null)
                    vldCompareBackupPreparer.ErrorMessage = Helper.GetLabelIDValue(5000086);
                if (vldCompareBackupReviewer != null)
                    vldCompareBackupReviewer.ErrorMessage = Helper.GetLabelIDValue(5000086);
                if (vldCompareBackupApprover != null)
                    vldCompareBackupApprover.ErrorMessage = Helper.GetLabelIDValue(5000086);


                string onBackupPreparerSelectedIndexChanged = "return ValidatePRAForAccountOwnership('" + ddlPreparer.ClientID + "','" + ddlReviewer.ClientID + "','" + ddlApprover.ClientID + "','"
                      + ddlBackUpPreparer.ClientID + "','" + ddlBackupReviewer.ClientID + "','" + ddlBackUpApprover.ClientID + "','"
                      + vldComparePreparerAndReviewer.ClientID + "', '"
                      + vldComparePreparerAndApprover.ClientID + "','"
                      + vldCompareApproverAndReviewer.ClientID + "', '"
                      + vldCompareBackupPreparer.ClientID + "', '"
                      + vldCompareBackupReviewer.ClientID + "', '"
                      + vldCompareBackupApprover.ClientID + "', '"
                      + checkBox.ClientID + "','" + WebConstants.SELECT_ONE + "');";

                string onPreparerSelectedIndexChanged = "return OnPreparerSelectedIndexChange('" + ddlPreparer.ClientID + "','" + ddlReviewer.ClientID + "','" + ddlApprover.ClientID + "','"
                       + vldComparePreparerAndReviewer.ClientID + "', '" + vldComparePreparerAndApprover.ClientID + "','" + vldCompareApproverAndReviewer.ClientID + "', '" + checkBox.ClientID + "','" + WebConstants.SELECT_ONE + "');";
                //ddlReviewer.AddAttributes = new KeyValuePair<string, string>("onchange", onPreparerSelectedIndexChanged);
                ddlReviewer.AddAttributes = new KeyValuePair<string, string>("onchange", onBackupPreparerSelectedIndexChanged);



                if (ddlBackUpPreparer != null)
                {
                    ddlBackUpPreparer.AddAttributes = new KeyValuePair<string, string>("onchange", onBackupPreparerSelectedIndexChanged);
                }
                if (ddlBackupReviewer != null)
                {
                    ddlBackupReviewer.AddAttributes = new KeyValuePair<string, string>("onchange", onBackupPreparerSelectedIndexChanged);
                }

                if (this._IsDualReviewEnabled)
                {
                    //ddlPreparer.AddAttributes = new KeyValuePair<string, string>("onchange", onPreparerSelectedIndexChanged);
                    ddlPreparer.AddAttributes = new KeyValuePair<string, string>("onchange", onBackupPreparerSelectedIndexChanged);
                    if (ddlBackupReviewer != null)
                    {
                        ddlBackUpApprover.AddAttributes = new KeyValuePair<string, string>("onchange", onBackupPreparerSelectedIndexChanged);
                    }
                    string onApproverSelectedIndexChanged = "return OnApproverSelectedIndexChange('" + ddlPreparer.ClientID + "','" + ddlReviewer.ClientID + "','" + ddlApprover.ClientID + "','"
                        + vldComparePreparerAndApprover.ClientID + "','" + vldCompareApproverAndReviewer.ClientID + "', '" + checkBox.ClientID + "','" + WebConstants.SELECT_ONE + "');";
                    //ddlApprover.AddAttributes = new KeyValuePair<string, string>("onchange", onPreparerSelectedIndexChanged);
                    ddlApprover.AddAttributes = new KeyValuePair<string, string>("onchange", onBackupPreparerSelectedIndexChanged);

                    string onReviewerSelectedIndexChanged = "return OnApproverSelectedIndexChange('" + ddlPreparer.ClientID + "','" + ddlReviewer.ClientID + "','" + ddlApprover.ClientID + "','"
                        + vldComparePreparerAndApprover.ClientID + "','" + vldCompareApproverAndReviewer.ClientID + "', '" + checkBox.ClientID + "','" + WebConstants.SELECT_ONE + "');";
                    // ddlApprover.AddAttributes = new KeyValuePair<string, string>("onchange", onPreparerSelectedIndexChanged);
                    ddlApprover.AddAttributes = new KeyValuePair<string, string>("onchange", onBackupPreparerSelectedIndexChanged);

                    chkExcludeOwnershipForZBA.Attributes.Add(WebConstants.ONCLICK, "ExcludeOwnershipValidationChecks('" + chkExcludeOwnershipForZBA.ClientID
                                                                                                                    + "', '" + ddlPreparer.ValidatorClientID
                                                                                                                    + "', '" + ddlReviewer.ValidatorClientID
                                                                                                                    + "', '" + vldComparePreparerAndReviewer.ClientID
                                                                                                                    + "', '" + ddlApprover.ValidatorClientID
                                                                                                                    + "', '" + vldCompareApproverAndReviewer.ClientID
                                                                                                                    + "', '" + vldComparePreparerAndApprover.ClientID
                                                                                                                    + "', '" + txtExcludeOwbershipValue.ClientID
                                                                                                                    + "');");

                    ExLabel lblApproverExport = (ExLabel)e.Item.FindControl("lblApproverExport");
                    ExLabel lblBackupApproverExport = (ExLabel)e.Item.FindControl("lblBackupApproverExport");

                    if (CurrentRecProcessStatus.Value == WebEnums.RecPeriodStatus.Closed
                    || CurrentRecProcessStatus.Value == WebEnums.RecPeriodStatus.InProgress
                        || CurrentRecProcessStatus.Value == WebEnums.RecPeriodStatus.Skipped)
                    {
                        if (oAccountHdrInfo.ApproverUserID.HasValue && oAccountHdrInfo.ApproverUserID.Value > 0)
                        {
                            ddlApprover.SelectedValue = oAccountHdrInfo.ApproverUserID.Value.ToString();
                            lblApproverExport.Text = HttpUtility.HtmlEncode(ddlApprover.SelectedItem.Text);
                        }
                        else
                        {
                            lblApproverExport.Text = "-";
                        }

                        //Backup Approver
                        if (oAccountHdrInfo.BackupApproverUserID.HasValue && oAccountHdrInfo.BackupApproverUserID.Value > 0)
                        {
                            ddlBackUpApprover.SelectedValue = oAccountHdrInfo.BackupApproverUserID.Value.ToString();
                            lblBackupApproverExport.Text = HttpUtility.HtmlEncode(ddlBackUpApprover.SelectedItem.Text);
                        }
                        else
                        {
                            lblBackupApproverExport.Text = "-";
                        }

                    }
                    else
                    {
                        //ddlApprover.Items.Remove(selectOne);
                        //ddlApprover.Items.Insert(0, selectOne);
                        if (oAccountHdrInfo.ApproverUserID.HasValue && oAccountHdrInfo.ApproverUserID.Value > 0)
                        {
                            ddlApprover.SelectedValue = oAccountHdrInfo.ApproverUserID.Value.ToString();
                            lblApproverExport.Text = HttpUtility.HtmlEncode(ddlApprover.SelectedItem.Text);
                        }
                        else
                        {
                            lblApproverExport.Text = "-";
                        }

                        //Backup Approver
                        if (oAccountHdrInfo.BackupApproverUserID.HasValue && oAccountHdrInfo.BackupApproverUserID.Value > 0)
                        {
                            ddlBackUpApprover.SelectedValue = oAccountHdrInfo.BackupApproverUserID.Value.ToString();
                            lblBackupApproverExport.Text = HttpUtility.HtmlEncode(ddlBackUpApprover.SelectedItem.Text);
                        }
                        else
                        {
                            lblBackupApproverExport.Text = "-";
                        }

                    }
                }
                else
                {
                    //string onPreparerSelectedIndexChanged = "return OnPreparerSelectedIndexChange('" + ddlPreparer.ClientID + "','" + ddlReviewer.ClientID + "','" + string.Empty + "','"
                    //   + vldComparePreparerAndReviewer.ClientID + "', '" + string.Empty + "','" + string.Empty + "', '" + checkBox.ClientID + "');";
                    //ddlPreparer.AddAttributes = new KeyValuePair<string, string>("onchange", onPreparerSelectedIndexChanged);
                    ddlPreparer.AddAttributes = new KeyValuePair<string, string>("onchange", onBackupPreparerSelectedIndexChanged);

                    chkExcludeOwnershipForZBA.Attributes.Add(WebConstants.ONCLICK, "ExcludeOwnershipValidationChecks('" + chkExcludeOwnershipForZBA.ClientID
                                                                                                                    + "', '" + ddlPreparer.ValidatorClientID
                                                                                                                    + "', '" + ddlReviewer.ValidatorClientID
                                                                                                                    + "', '" + vldComparePreparerAndReviewer.ClientID
                                                                                                                    + "', '" + null
                                                                                                                    + "', '" + null
                                                                                                                    + "', '" + null
                                                                                                                    + "', '" + txtExcludeOwbershipValue.ClientID
                                                                                                                    + "');");
                }



                if ((e.Item as GridDataItem)[COLUMN_NAME_ID] != null)
                {
                    (e.Item as GridDataItem)[COLUMN_NAME_ID].Text = oAccountHdrInfo.AccountID.ToString();
                }

                if ((e.Item as GridDataItem)[COLUMN_NAME_NETACCOUNTID] != null)
                {
                    (e.Item as GridDataItem)[COLUMN_NAME_NETACCOUNTID].Text = oAccountHdrInfo.NetAccountID.ToString();
                }
                CustomValidator cvApproverBackupApprover = (CustomValidator)e.Item.FindControl("cvApproverBackupApprover");
                string ApproverBackupApproverClientID = ddlApprover.ClientID + "," + ddlBackUpApprover.ClientID;
                cvApproverBackupApprover.Attributes.Add("ApproverBackupApproverClientID", ApproverBackupApproverClientID);
                cvApproverBackupApprover.ErrorMessage = Helper.GetLabelIDValue(5000328);
            }
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
        List<AccountHdrInfo> oAccountHdrInfoCollection = null;
        try
        {
            oAccountHdrInfoCollection = (List<AccountHdrInfo>)HttpContext.Current.Session[SessionConstants.SEARCH_RESULTS_ACCOUNT];

            if (oAccountHdrInfoCollection == null || oAccountHdrInfoCollection.Count == 0)
            {
                AccountSearchCriteria oAccountSearchCriteria = (AccountSearchCriteria)HttpContext.Current.Session[SessionConstants.ACCOUNT_SEARCH_CRITERIA];
                IAccount oAccountClient = RemotingHelper.GetAccountObject();
                oAccountHdrInfoCollection = oAccountClient.SearchAccount(oAccountSearchCriteria, Helper.GetAppUserInfo());
                oAccountHdrInfoCollection = LanguageHelper.TranslateLabelsAccountHdr(oAccountHdrInfoCollection);
                HttpContext.Current.Session[SessionConstants.SEARCH_RESULTS_ACCOUNT] = oAccountHdrInfoCollection;
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

        return oAccountHdrInfoCollection;
    }
    public void ucSkyStemARTGrid_GridItemCommand(object source, GridCommandEventArgs e)
    {

        if (e.CommandName.Equals("GridExportToPDF") || e.CommandName.Equals("GridExportToExcel"))
        {
            GridColumn oGridColumnPreparerExport = ucSkyStemARTGrid.Grid.MasterTableView.Columns.FindByUniqueName("PreparerExport");
            GridColumn oGridColumnPreparer = ucSkyStemARTGrid.Grid.MasterTableView.Columns.FindByUniqueName("Preparer");
            if (oGridColumnPreparerExport != null)
                oGridColumnPreparerExport.Visible = true;

            if (oGridColumnPreparer != null)
                oGridColumnPreparer.Visible = false;

            GridColumn oGridColumnReviewerExport = ucSkyStemARTGrid.Grid.MasterTableView.Columns.FindByUniqueName("ReviewerExport");
            GridColumn oGridColumnReviewer = ucSkyStemARTGrid.Grid.MasterTableView.Columns.FindByUniqueName("Reviewer");
            if (oGridColumnReviewerExport != null)
                oGridColumnReviewerExport.Visible = true;

            if (oGridColumnReviewer != null)
                oGridColumnReviewer.Visible = false;

            GridColumn oGridColumnBackupPreparerExport = ucSkyStemARTGrid.Grid.MasterTableView.Columns.FindByUniqueName("BackupPreparerExport");
            GridColumn oGridColumnBackupPreparer = ucSkyStemARTGrid.Grid.MasterTableView.Columns.FindByUniqueName("BackupPreparer");
            if (oGridColumnBackupPreparerExport != null)
                oGridColumnBackupPreparerExport.Visible = true;

            if (oGridColumnBackupPreparer != null)
                oGridColumnBackupPreparer.Visible = false;

            GridColumn oGridColumnBackupReviewerExport = ucSkyStemARTGrid.Grid.MasterTableView.Columns.FindByUniqueName("BackupReviewerExport");
            GridColumn oGridColumnBackupReviewer = ucSkyStemARTGrid.Grid.MasterTableView.Columns.FindByUniqueName("BackupReviewer");
            if (oGridColumnBackupReviewerExport != null)
                oGridColumnBackupReviewerExport.Visible = true;

            if (oGridColumnBackupReviewer != null)
                oGridColumnBackupReviewer.Visible = false;

            if (Helper.GetFeatureCapabilityMode(WebEnums.Feature.DualLevelReview, ARTEnums.Capability.DualLevelReview, SessionHelper.CurrentReconciliationPeriodID) == WebEnums.FeatureCapabilityMode.Visible)
            {
                GridColumn oGridColumnApproverExport = ucSkyStemARTGrid.Grid.MasterTableView.Columns.FindByUniqueName("ApproverExport");
                GridColumn oGridColumnApprover = ucSkyStemARTGrid.Grid.MasterTableView.Columns.FindByUniqueName("Approver");
                if (oGridColumnApproverExport != null)
                    oGridColumnApproverExport.Visible = true;

                if (oGridColumnApprover != null)
                    oGridColumnApprover.Visible = false;

                GridColumn oGridColumnBackupApproverExport = ucSkyStemARTGrid.Grid.MasterTableView.Columns.FindByUniqueName("BackupApproverExport");
                GridColumn oGridColumnBackupApprover = ucSkyStemARTGrid.Grid.MasterTableView.Columns.FindByUniqueName("BackupApprover");
                if (oGridColumnBackupApproverExport != null)
                    oGridColumnBackupApproverExport.Visible = true;

                if (oGridColumnBackupApprover != null)
                    oGridColumnBackupApprover.Visible = false;
            }


        }

    }
    #endregion

    #region Grid Events SkyStemARTGridMassUpdate
    /// <summary>
    /// Handles user controls Need data source event
    /// </summary>
    /// <param name="count">Number of items needed to bind the grid</param>
    /// <returns>object</returns>
    protected object SkyStemARTGridMassUpdate_Grid_NeedDataSourceEventHandler(int count)
    {
        List<AccountHdrInfo> oAccountHdrInfoCollection = null;
        try
        {
            oAccountHdrInfoCollection = (List<AccountHdrInfo>)HttpContext.Current.Session[SessionConstants.SEARCH_RESULTS_ACCOUNT];

            if (oAccountHdrInfoCollection == null || oAccountHdrInfoCollection.Count == 0)
            {
                AccountSearchCriteria oAccountSearchCriteria = (AccountSearchCriteria)HttpContext.Current.Session[SessionConstants.ACCOUNT_SEARCH_CRITERIA];
                IAccount oAccountClient = RemotingHelper.GetAccountObject();
                oAccountHdrInfoCollection = oAccountClient.SearchAccount(oAccountSearchCriteria, Helper.GetAppUserInfo());
                oAccountHdrInfoCollection = LanguageHelper.TranslateLabelsAccountHdr(oAccountHdrInfoCollection);
                HttpContext.Current.Session[SessionConstants.SEARCH_RESULTS_ACCOUNT] = oAccountHdrInfoCollection;
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

        return oAccountHdrInfoCollection;
    }

    /// <summary>
    /// Handles rad grids item data bound event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void SkyStemARTGridMassUpdate_GridItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {
        try
        {
            if (e.Item.ItemType == Telerik.Web.UI.GridItemType.Item || e.Item.ItemType == Telerik.Web.UI.GridItemType.AlternatingItem)
            {
                AccountHdrInfo oAccountHdrInfo = (AccountHdrInfo)e.Item.DataItem;

                ExLabel lblAccountNumber = (ExLabel)e.Item.FindControl("lblAccountNumberMass");
                lblAccountNumber.Text = HttpUtility.HtmlEncode(oAccountHdrInfo.AccountNumber);

                ExLabel lblAccountName = (ExLabel)e.Item.FindControl("lblAccountNameMass");
                lblAccountName.Text = HttpUtility.HtmlEncode(oAccountHdrInfo.AccountName);

                ExLabel lblNetAccount = (ExLabel)e.Item.FindControl("lblNetAccountMass");

                if (!string.IsNullOrEmpty(oAccountHdrInfo.NetAccount))
                {
                    lblNetAccount.Text = HttpUtility.HtmlEncode(oAccountHdrInfo.NetAccount);
                }
                else
                {
                    lblNetAccount.Text = BLANK_TEXT_HYPHEN;
                }
                CheckBox checkBox = (CheckBox)(e.Item as GridDataItem)["CheckboxSelectColumn"].Controls[0];
                if (!oAccountHdrInfo.IsLocked.GetValueOrDefault()
                    && (CurrentRecProcessStatus.Value == WebEnums.RecPeriodStatus.NotStarted
                        || CurrentRecProcessStatus.Value == WebEnums.RecPeriodStatus.Open
                        || CurrentRecProcessStatus.Value == WebEnums.RecPeriodStatus.InProgress
                    ))
                {
                    if (checkBox != null)
                    {
                        checkBox.Enabled = true;
                    }
                }
                else
                {
                    if (checkBox != null)
                    {
                        checkBox.Enabled = false;
                        Sel.Value += e.Item.ItemIndex.ToString() + ":";
                    }
                }
                //string onSelectedIndexChanged = "return OnReviewerSelectedIndexChange('" + ddlPreparer.ClientID + "','" + ddlReviewer.ClientID + "','" + vldComparePreparerAndReviewer.ClientID + "','" + checkBox.ClientID + "','" + WebConstants.SELECT_ONE + "');";
                //ddlReviewer.AddAttributes = new KeyValuePair<string, string>("onchange", onSelectedIndexChanged);
                ExLabel lblPreparer = (ExLabel)e.Item.FindControl("lblPreparer");
                lblPreparer.Text = Helper.GetDisplayStringValue(oAccountHdrInfo.PreparerFullName);
                ExLabel lblReviewer = (ExLabel)e.Item.FindControl("lblReviewer");
                lblReviewer.Text = Helper.GetDisplayStringValue(oAccountHdrInfo.ReviewerFullName);
                #region BackupRoles Section

                ExLabel lblBackupPreparer = (ExLabel)e.Item.FindControl("lblBackupPreparer");
                lblBackupPreparer.Text = Helper.GetDisplayStringValue(oAccountHdrInfo.BackupPreparerFullName);
                ExLabel lblBackupReviewer = (ExLabel)e.Item.FindControl("lblBackupReviewer");
                lblBackupReviewer.Text = Helper.GetDisplayStringValue(oAccountHdrInfo.BackupReviewerFullName);
                #endregion

                if (this._IsDualReviewEnabled)
                {
                    ExLabel lblApprover = (ExLabel)e.Item.FindControl("lblApprover");
                    ExLabel lblBackupApprover = (ExLabel)e.Item.FindControl("lblBackupApprover");
                    lblApprover.Text = Helper.GetDisplayStringValue(oAccountHdrInfo.ApproverFullName);
                    //Backup Approver
                    lblBackupApprover.Text = Helper.GetDisplayStringValue(oAccountHdrInfo.BackupApproverFullName);
                }
                if ((e.Item as GridDataItem)[COLUMN_NAME_ID] != null)
                {
                    (e.Item as GridDataItem)[COLUMN_NAME_ID].Text = oAccountHdrInfo.AccountID.ToString();
                }

                if ((e.Item as GridDataItem)[COLUMN_NAME_NETACCOUNTID] != null)
                {
                    (e.Item as GridDataItem)[COLUMN_NAME_NETACCOUNTID].Text = oAccountHdrInfo.NetAccountID.ToString();
                }

            }
        }
        catch (Exception ex)
        {
            Helper.ShowErrorMessage(this, ex);
        }
    }
    #endregion

    #region Other Events
    /// <summary>
    /// Handles save button click event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (IsValid)
            {
                if (this._IsMassUpdateClicked)
                {
                    List<AccountHdrInfo> oAccountHdrInfoCollection = (List<AccountHdrInfo>)HttpContext.Current.Session[SessionConstants.SEARCH_RESULTS_ACCOUNT];
                    List<AccountHdrInfo> oAccountHdrInfoCollectionNew = new List<AccountHdrInfo>();
                    List<AccountAttributeWarningInfo> oAccountIDNetAccountIDCollection = new List<AccountAttributeWarningInfo>();

                    List<long> accountIDList = new List<long>();
                    List<int> netAccountIDList = new List<int>();
                    foreach (GridDataItem item in SkyStemARTGridMassUpdate.Grid.MasterTableView.GetSelectedItems())
                    {
                        long accountId;
                        int netAccountId;
                        if (int.TryParse(item[COLUMN_NAME_NETACCOUNTID].Text, out netAccountId) && netAccountIDList.FindIndex(T => T == netAccountId) < 0)
                            netAccountIDList.Add(netAccountId);
                        if (long.TryParse(item[COLUMN_NAME_ID].Text, out accountId))
                            accountIDList.Add(accountId);
                        AccountAttributeWarningInfo oAccountAttributeWarningInfo = new AccountAttributeWarningInfo();
                        oAccountAttributeWarningInfo.AccountID = accountId;
                        oAccountAttributeWarningInfo.NetAccountID = netAccountId;
                        oAccountIDNetAccountIDCollection.Add(oAccountAttributeWarningInfo);

                        AccountHdrInfo oOldAccountHdrInfo = (from accHdr in oAccountHdrInfoCollection where accHdr.AccountID == accountId select accHdr).FirstOrDefault();
                        AccountHdrInfo oAccountHdrInfo = (AccountHdrInfo)Helper.DeepClone(oOldAccountHdrInfo);

                        //if (ucPreparer.Visible)
                        //{
                        //    if (Convert.ToInt32(ucPreparer.SelectedValue) > Convert.ToInt32(WebConstants.SELECT_ONE))
                        //    {
                        //        oAccountHdrInfo.PreparerUserID = Convert.ToInt32(ucPreparer.SelectedValue);
                        //    }
                        //    else
                        //    {
                        //        oAccountHdrInfo.PreparerUserID = null;
                        //    }
                        //}
                        //if (ucReviewer.Visible)
                        //{
                        //    if (Convert.ToInt32(ucReviewer.SelectedValue) > Convert.ToInt32(WebConstants.SELECT_ONE))
                        //    {
                        //        oAccountHdrInfo.ReviewerUserID = Convert.ToInt32(ucReviewer.SelectedValue);
                        //    }
                        //    else
                        //    {
                        //        oAccountHdrInfo.ReviewerUserID = null;
                        //    }
                        //}

                        //if (ucBackupPreparer.Visible)
                        //{
                        //    if (Convert.ToInt32(ucBackupPreparer.SelectedValue) > Convert.ToInt32(WebConstants.SELECT_ONE))
                        //    {
                        //        oAccountHdrInfo.BackupPreparerUserID = Convert.ToInt32(ucBackupPreparer.SelectedValue);
                        //    }
                        //    else
                        //    {
                        //        oAccountHdrInfo.BackupPreparerUserID = null;
                        //    }
                        //}
                        //if (ucBackupReviewer.Visible)
                        //{
                        //    if (Convert.ToInt32(ucBackupReviewer.SelectedValue) > Convert.ToInt32(WebConstants.SELECT_ONE))
                        //    {
                        //        oAccountHdrInfo.BackupReviewerUserID = Convert.ToInt32(ucBackupReviewer.SelectedValue);
                        //    }
                        //    else
                        //    {
                        //        oAccountHdrInfo.BackupReviewerUserID = null;
                        //    }
                        //}                      

                        //if (this._IsDualReviewEnabled)
                        //{
                        //    if (ucApprover.Visible)
                        //    {
                        //        if (Convert.ToInt32(ucApprover.SelectedValue) > Convert.ToInt32(WebConstants.SELECT_ONE))
                        //        {
                        //            oAccountHdrInfo.ApproverUserID = Convert.ToInt32(ucApprover.SelectedValue);
                        //        }
                        //        else
                        //        {
                        //            oAccountHdrInfo.ApproverUserID = null;
                        //        }
                        //    }
                        //    if (ucBackupApprover.Visible)
                        //    {
                        //        if (Convert.ToInt32(ucBackupApprover.SelectedValue) > Convert.ToInt32(WebConstants.SELECT_ONE))
                        //        {
                        //            oAccountHdrInfo.BackupApproverUserID = Convert.ToInt32(ucBackupApprover.SelectedValue);
                        //        }
                        //        else
                        //        {
                        //            oAccountHdrInfo.BackupApproverUserID = null;
                        //        }
                        //    }
                        //}
                        //else
                        //{
                        //    oAccountHdrInfo.ApproverUserID = null;
                        //    oAccountHdrInfo.BackupApproverUserID = null;
                        //}


                        if (Convert.ToInt16(ddlAccountAttribute.SelectedValue) == Convert.ToInt16(ARTEnums.UserRole.PREPARER))
                        {
                            int userid;
                            if (!string.IsNullOrEmpty(hdnOwner.Value) && int.TryParse(hdnOwner.Value, out userid) && userid > 0)
                                oAccountHdrInfo.PreparerUserID = Convert.ToInt32(userid);
                            else
                            {
                                oAccountHdrInfo.PreparerUserID = null;
                            }
                        }
                        if (Convert.ToInt16(ddlAccountAttribute.SelectedValue) == Convert.ToInt16(ARTEnums.UserRole.REVIEWER))
                        {
                            int userid;
                            if (!string.IsNullOrEmpty(hdnOwner.Value) && int.TryParse(hdnOwner.Value, out userid) && userid > 0)
                                oAccountHdrInfo.ReviewerUserID = Convert.ToInt32(userid);
                            else
                            {
                                oAccountHdrInfo.ReviewerUserID = null;
                            }
                        }
                        if (Convert.ToInt16(ddlAccountAttribute.SelectedValue) == Convert.ToInt16(ARTEnums.UserRole.BACKUP_PREPARER))
                        {
                            int userid;
                            if (!string.IsNullOrEmpty(hdnOwner.Value) && int.TryParse(hdnOwner.Value, out userid) && userid > 0)
                                oAccountHdrInfo.BackupPreparerUserID = Convert.ToInt32(userid);
                            else
                            {
                                oAccountHdrInfo.BackupPreparerUserID = null;
                            }
                        }
                        if (Convert.ToInt16(ddlAccountAttribute.SelectedValue) == Convert.ToInt16(ARTEnums.UserRole.BACKUP_REVIEWER))
                        {
                            int userid;
                            if (!string.IsNullOrEmpty(hdnOwner.Value) && int.TryParse(hdnOwner.Value, out userid) && userid > 0)
                                oAccountHdrInfo.BackupReviewerUserID = Convert.ToInt32(userid);
                            else
                            {
                                oAccountHdrInfo.BackupReviewerUserID = null;
                            }
                        }

                        if (this._IsDualReviewEnabled)
                        {
                            if (Convert.ToInt16(ddlAccountAttribute.SelectedValue) == Convert.ToInt16(ARTEnums.UserRole.APPROVER))
                            {
                                int userid;
                                if (!string.IsNullOrEmpty(hdnOwner.Value) && int.TryParse(hdnOwner.Value, out userid) && userid > 0)
                                    oAccountHdrInfo.ApproverUserID = Convert.ToInt32(userid);
                                else
                                {
                                    oAccountHdrInfo.ApproverUserID = null;
                                }
                            }
                            if (Convert.ToInt16(ddlAccountAttribute.SelectedValue) == Convert.ToInt16(ARTEnums.UserRole.BACKUP_APPROVER))
                            {
                                int userid;
                                if (!string.IsNullOrEmpty(hdnOwner.Value) && int.TryParse(hdnOwner.Value, out userid) && userid > 0)
                                    oAccountHdrInfo.BackupApproverUserID = Convert.ToInt32(userid);
                                else
                                {
                                    oAccountHdrInfo.BackupApproverUserID = null;
                                }
                            }
                        }
                        else
                        {
                            oAccountHdrInfo.ApproverUserID = null;
                            oAccountHdrInfo.BackupApproverUserID = null;
                        }
                        oAccountHdrInfoCollectionNew.Add(oAccountHdrInfo);
                    }
                    ValidateIsBlankOwnerWarningOccur();
                    if (hdnConfirm_BlankOwner.Value == "Yes")
                    {
                        ValidatePRALogicForOwnershipMassUpdate(oAccountHdrInfoCollectionNew);

                        foreach (int netAccountID in netAccountIDList)
                        {
                            if (!NetAccountHelper.IsAllConstituentAccountsSelected(netAccountID, SessionHelper.CurrentReconciliationPeriodID.Value, accountIDList))
                            {
                                throw new ARTException(5000048);
                            }
                            ValidateNetAccountsForOwnershipUpdate(netAccountID, oAccountHdrInfoCollectionNew);
                        }
                        ValidateIsWarningOccur(oAccountIDNetAccountIDCollection, new List<AccountHdrInfo>());
                    }
                    if (hdnConfirm.Value == "Yes" && hdnConfirm_BlankOwner.Value == "Yes")
                    {
                        IAccount oAccountClient = RemotingHelper.GetAccountObject();
                        bool result = oAccountClient.SaveAccountOwnerships(
                            oAccountHdrInfoCollectionNew
                            , SessionHelper.CurrentCompanyID.Value
                            , SessionHelper.CurrentReconciliationPeriodID.Value
                            , _IsDualReviewEnabled
                            , SessionHelper.GetCurrentUser().LoginID
                            , DateTime.Now
                            , (short)ARTEnums.UserRole.PREPARER
                            , (short)ARTEnums.UserRole.REVIEWER
                            , (short)ARTEnums.UserRole.APPROVER
                            , (short)ARTEnums.UserRole.BACKUP_PREPARER
                            , (short)ARTEnums.UserRole.BACKUP_REVIEWER
                            , (short)ARTEnums.UserRole.BACKUP_APPROVER
                            , (short)ARTEnums.ActionType.AccountAttributeChangeFromUI
                            , Helper.GetAppUserInfo());

                        RaiseAlertIfOwnershipChanged(oAccountHdrInfoCollectionNew, oAccountHdrInfoCollection);

                        if (result)
                        {
                            MasterPageBase oMasterPageBase = (MasterPageBase)this.Master;
                            oMasterPageBase.ShowConfirmationMessage(1597);
                            HidePanels(this._IsMassUpdateClicked);
                            AccountSearchCriteria oAccountSearchCriteria = (AccountSearchCriteria)HttpContext.Current.Session[SessionConstants.ACCOUNT_SEARCH_CRITERIA];
                            oAccountHdrInfoCollection = oAccountClient.SearchAccount(oAccountSearchCriteria, Helper.GetAppUserInfo());
                            oAccountHdrInfoCollection = LanguageHelper.TranslateLabelsAccountHdr(oAccountHdrInfoCollection);
                            HttpContext.Current.Session[SessionConstants.SEARCH_RESULTS_ACCOUNT] = oAccountHdrInfoCollection;
                            SkyStemARTGridMassUpdate.DataSource = oAccountHdrInfoCollection;
                            SkyStemARTGridMassUpdate.DataBind();
                            hdnConfirm.Value = "";
                            hdnConfirm_BlankOwner.Value = "";
                            hdnOwner.Value = "";
                            txtOwner.Text = "";
                        }
                        else
                        {
                            SkyStemARTGridMassUpdate.DataSource = oAccountHdrInfoCollection;
                            SkyStemARTGridMassUpdate.DataBind();
                            hdnConfirm.Value = "";
                            hdnConfirm_BlankOwner.Value = "";
                            hdnOwner.Value = "";
                            txtOwner.Text = "";
                            throw new ARTException(5000045);
                        }

                    }
                }
                else
                {
                    List<AccountHdrInfo> oAccountHdrInfoCollection = (List<AccountHdrInfo>)HttpContext.Current.Session[SessionConstants.SEARCH_RESULTS_ACCOUNT];
                    List<AccountHdrInfo> oAccountHdrInfoCollectionNew = new List<AccountHdrInfo>();
                    List<AccountAttributeWarningInfo> oAccountIDNetAccountIDCollection = new List<AccountAttributeWarningInfo>();

                    List<long> accountIDList = new List<long>();
                    List<int> netAccountIDList = new List<int>();
                    foreach (GridDataItem item in ucSkyStemARTGrid.Grid.MasterTableView.GetSelectedItems())
                    {
                        long accountId;
                        int netAccountId;
                        if (int.TryParse(item[COLUMN_NAME_NETACCOUNTID].Text, out netAccountId) && netAccountIDList.FindIndex(T => T == netAccountId) < 0)
                            netAccountIDList.Add(netAccountId);
                        if (long.TryParse(item[COLUMN_NAME_ID].Text, out accountId))
                            accountIDList.Add(accountId);
                        //if (!string.IsNullOrEmpty(netAccountId) && Convert.ToInt32(netAccountId) > 0)
                        //{
                        //    throw new ARTException(5000044);
                        //}
                        AccountAttributeWarningInfo oAccountAttributeWarningInfo = new AccountAttributeWarningInfo();
                        oAccountAttributeWarningInfo.AccountID = accountId;
                        oAccountAttributeWarningInfo.NetAccountID = netAccountId;
                        oAccountIDNetAccountIDCollection.Add(oAccountAttributeWarningInfo);

                        AccountHdrInfo oOldAccountHdrInfo = (from accHdr in oAccountHdrInfoCollection where accHdr.AccountID == accountId select accHdr).FirstOrDefault();

                        //AccountHdrInfo oAccountHdrInfo = Helper.CloneAccountOwnershipInfo(oOldAccountHdrInfo);
                        // Required to compare net account attributes
                        AccountHdrInfo oAccountHdrInfo = (AccountHdrInfo)Helper.DeepClone(oOldAccountHdrInfo);

                        UserControls_UserDropDown ddlPreparer = (UserControls_UserDropDown)item.FindControl(USERCONTROL_PREPARER);
                        UserControls_UserDropDown ddlReviewer = (UserControls_UserDropDown)item.FindControl(USERCONTROL_REVIEWER);
                        UserControls_UserDropDown ddlApprover = (UserControls_UserDropDown)item.FindControl(USERCONTROL_APPROVER);

                        UserControls_UserDropDown ddlBackupPreparer = (UserControls_UserDropDown)item.FindControl("ucBackupPreparer");
                        UserControls_UserDropDown ddlBackupReviewer = (UserControls_UserDropDown)item.FindControl("ucBackupReviewer");
                        UserControls_UserDropDown ddlBackupApprover = (UserControls_UserDropDown)item.FindControl("ucBackupApprover");

                        HtmlInputText txtExcludeOwbershipValue = (HtmlInputText)item.FindControl("txtExcludeOwnershipValue");

                        if (Convert.ToInt32(ddlPreparer.SelectedValue) > Convert.ToInt32(WebConstants.SELECT_ONE))
                        {
                            oAccountHdrInfo.PreparerUserID = Convert.ToInt32(ddlPreparer.SelectedValue);
                        }
                        else
                        {
                            oAccountHdrInfo.PreparerUserID = null;
                        }

                        if (Convert.ToInt32(ddlReviewer.SelectedValue) > Convert.ToInt32(WebConstants.SELECT_ONE))
                        {
                            oAccountHdrInfo.ReviewerUserID = Convert.ToInt32(ddlReviewer.SelectedValue);
                        }
                        else
                        {
                            oAccountHdrInfo.ReviewerUserID = null;
                        }


                        if (Convert.ToInt32(ddlBackupPreparer.SelectedValue) > Convert.ToInt32(WebConstants.SELECT_ONE))
                        {
                            oAccountHdrInfo.BackupPreparerUserID = Convert.ToInt32(ddlBackupPreparer.SelectedValue);
                        }
                        else
                        {
                            oAccountHdrInfo.BackupPreparerUserID = null;
                        }

                        if (Convert.ToInt32(ddlBackupReviewer.SelectedValue) > Convert.ToInt32(WebConstants.SELECT_ONE))
                        {
                            oAccountHdrInfo.BackupReviewerUserID = Convert.ToInt32(ddlBackupReviewer.SelectedValue);
                        }
                        else
                        {
                            oAccountHdrInfo.BackupReviewerUserID = null;
                        }

                        //if (oAccountHdrInfo.PreparerUserID == oAccountHdrInfo.ReviewerUserID)
                        //{
                        //    throw new ARTException(5000086);
                        //}

                        if (this._IsDualReviewEnabled)
                        {
                            if (Convert.ToInt32(ddlApprover.SelectedValue) > Convert.ToInt32(WebConstants.SELECT_ONE))
                            {
                                oAccountHdrInfo.ApproverUserID = Convert.ToInt32(ddlApprover.SelectedValue);
                            }
                            else
                            {
                                oAccountHdrInfo.ApproverUserID = null;
                            }

                            if (Convert.ToInt32(ddlBackupApprover.SelectedValue) > Convert.ToInt32(WebConstants.SELECT_ONE))
                            {
                                oAccountHdrInfo.BackupApproverUserID = Convert.ToInt32(ddlBackupApprover.SelectedValue);
                            }
                            else
                            {
                                oAccountHdrInfo.BackupApproverUserID = null;
                            }
                        }
                        else
                        {
                            oAccountHdrInfo.ApproverUserID = null;
                            oAccountHdrInfo.BackupApproverUserID = null;
                        }

                        if (txtExcludeOwbershipValue != null && !string.IsNullOrEmpty(txtExcludeOwbershipValue.Value))
                        {
                            oAccountHdrInfo.IsExcludeOwnershipForZBA = Convert.ToBoolean(txtExcludeOwbershipValue.Value);
                        }

                        oAccountHdrInfoCollectionNew.Add(oAccountHdrInfo);
                    }

                    foreach (int netAccountID in netAccountIDList)
                    {
                        if (!NetAccountHelper.IsAllConstituentAccountsSelected(netAccountID, SessionHelper.CurrentReconciliationPeriodID.Value, accountIDList))
                        {
                            throw new ARTException(5000048);
                        }
                        ValidateNetAccountsForOwnershipUpdate(netAccountID, oAccountHdrInfoCollectionNew);
                    }
                    ValidateIsWarningOccur(oAccountIDNetAccountIDCollection, new List<AccountHdrInfo>());
                    if (hdnConfirm.Value == "Yes")
                    {
                        IAccount oAccountClient = RemotingHelper.GetAccountObject();
                        bool result = oAccountClient.SaveAccountOwnerships(
                            oAccountHdrInfoCollectionNew
                            , SessionHelper.CurrentCompanyID.Value
                            , SessionHelper.CurrentReconciliationPeriodID.Value
                            , _IsDualReviewEnabled
                            , SessionHelper.GetCurrentUser().LoginID
                            , DateTime.Now
                            , (short)ARTEnums.UserRole.PREPARER
                            , (short)ARTEnums.UserRole.REVIEWER
                            , (short)ARTEnums.UserRole.APPROVER
                            , (short)ARTEnums.UserRole.BACKUP_PREPARER
                            , (short)ARTEnums.UserRole.BACKUP_REVIEWER
                            , (short)ARTEnums.UserRole.BACKUP_APPROVER
                            , (short)ARTEnums.ActionType.AccountAttributeChangeFromUI
                            , Helper.GetAppUserInfo());

                        RaiseAlertIfOwnershipChanged(oAccountHdrInfoCollectionNew, oAccountHdrInfoCollection);

                        if (result)
                        {
                            MasterPageBase oMasterPageBase = (MasterPageBase)this.Master;
                            oMasterPageBase.ShowConfirmationMessage(1597);
                            HidePanels();
                            btnSave.Visible = false;
                            btnReset.Visible = false;
                            btnCancel.Visible = false;
                        }
                        else
                        {
                            ucSkyStemARTGrid.DataSource = oAccountHdrInfoCollection;
                            ucSkyStemARTGrid.DataBind();
                            throw new ARTException(5000045);
                        }
                        hdnConfirm.Value = "";
                    }
                }
            }
            else
            {
                Page.Validate();
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
    /// Handles cancel button click event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        HidePanels();
    }

    /// <summary>
    /// Handles reset button click event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnReset_Click(object sender, EventArgs e)
    {
        try
        {
            List<AccountHdrInfo> oAccountHdrInfoCollection = (List<AccountHdrInfo>)HttpContext.Current.Session[SessionConstants.SEARCH_RESULTS_ACCOUNT];
            List<AccountHdrInfo> oAccountHdrInfoCollectionNew = new List<AccountHdrInfo>();
            List<AccountAttributeWarningInfo> oAccountIDNetAccountIDCollection = new List<AccountAttributeWarningInfo>();
            List<long> accountIDList = new List<long>();
            List<int> netAccountIDList = new List<int>();
            foreach (GridDataItem item in ucSkyStemARTGrid.Grid.MasterTableView.GetSelectedItems())
            {
                long accountId;
                int netAccountId;
                if (int.TryParse(item[COLUMN_NAME_NETACCOUNTID].Text, out netAccountId) && netAccountIDList.FindIndex(T => T == netAccountId) < 0)
                    netAccountIDList.Add(netAccountId);
                if (long.TryParse(item[COLUMN_NAME_ID].Text, out accountId))
                    accountIDList.Add(accountId);
                AccountHdrInfo oOldAccountHdrInfo = (from accHdr in oAccountHdrInfoCollection where accHdr.AccountID == accountId select accHdr).FirstOrDefault();
                AccountHdrInfo oAccountHdrInfo = (AccountHdrInfo)Helper.DeepClone(oOldAccountHdrInfo);

                oAccountHdrInfo.PreparerUserID = null;
                oAccountHdrInfo.ReviewerUserID = null;
                oAccountHdrInfo.BackupPreparerUserID = null;
                oAccountHdrInfo.BackupReviewerUserID = null;
                oAccountHdrInfo.ApproverUserID = null;
                oAccountHdrInfo.BackupApproverUserID = null;
                oAccountHdrInfoCollectionNew.Add(oAccountHdrInfo);
                AccountAttributeWarningInfo oAccountAttributeWarningInfo = new AccountAttributeWarningInfo();
                oAccountAttributeWarningInfo.AccountID = accountId;
                oAccountAttributeWarningInfo.NetAccountID = netAccountId;
                oAccountIDNetAccountIDCollection.Add(oAccountAttributeWarningInfo);
            }

            foreach (int netAccountID in netAccountIDList)
            {
                if (!NetAccountHelper.IsAllConstituentAccountsSelected(netAccountID, SessionHelper.CurrentReconciliationPeriodID.Value, accountIDList))
                {
                    throw new ARTException(5000048);
                }
                ValidateNetAccountsForOwnershipUpdate(netAccountID, oAccountHdrInfoCollectionNew);
            }

            ValidateIsWarningOccur(oAccountIDNetAccountIDCollection, new List<AccountHdrInfo>());
            if (hdnConfirm.Value == "Yes")
            {
                IAccount oAccountClient = RemotingHelper.GetAccountObject();
                bool result = oAccountClient.SaveAccountOwnerships(
                    oAccountHdrInfoCollectionNew
                    , SessionHelper.CurrentCompanyID.Value
                    , SessionHelper.CurrentReconciliationPeriodID.Value
                    , _IsDualReviewEnabled
                    , SessionHelper.GetCurrentUser().LoginID
                    , DateTime.Now
                    , (short)ARTEnums.UserRole.PREPARER
                    , (short)ARTEnums.UserRole.REVIEWER
                    , (short)ARTEnums.UserRole.APPROVER
                    , (short)ARTEnums.UserRole.BACKUP_PREPARER
                    , (short)ARTEnums.UserRole.BACKUP_REVIEWER
                    , (short)ARTEnums.UserRole.BACKUP_APPROVER
                    , (short)ARTEnums.ActionType.AccountAttributeChangeFromUI
                    , Helper.GetAppUserInfo());

                if (result)
                {
                    RaiseAlertIfOwnershipChanged(oAccountHdrInfoCollectionNew, oAccountHdrInfoCollection);
                    MasterPageBase oMasterPageBase = (MasterPageBase)this.Master;
                    oMasterPageBase.ShowConfirmationMessage(1597);
                    HidePanels();
                }
                else
                {
                    ucSkyStemARTGrid.DataSource = oAccountHdrInfoCollection;
                    ucSkyStemARTGrid.DataBind();
                    throw new ARTException(5000045);
                }
                hdnConfirm.Value = "";
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

    protected void ddlAccountAttribute_SelectedIndexChangedHandler(object sender, EventArgs e)
    {
        SelectedAttributeID = Convert.ToInt16(ddlAccountAttribute.SelectedValue);
        hdnOwner.Value = "";
        txtOwner.Text = "";
        hdnConfirm_BlankOwner.Value = "";
        Page.SetFocus(ddlAccountAttribute);
        //ShowHideOwnersDDL();
    }

    #endregion

    #region Validation Control Events
    public void ValidateIsWarningOccur(List<AccountAttributeWarningInfo> oAccountIDNetAccountIDCollection, List<AccountHdrInfo> oAccountHdrInfoCollection)
    {
        if (hdnConfirm.Value != "Yes")
        {
            IAccount oAccountClient = RemotingHelper.GetAccountObject();
            AccountAttributeWarningInfo oAccountAttributeWarningInfo = oAccountClient.GetAccountAttributeChangeWarning(oAccountIDNetAccountIDCollection, oAccountHdrInfoCollection, SessionHelper.CurrentReconciliationPeriodID.Value, Helper.GetAppUserInfo());
            if (oAccountAttributeWarningInfo.FutureNetAccountWarning.Value || oAccountAttributeWarningInfo.LossOfWorkWarning.Value)
            {
                string msg = null;
                if (oAccountAttributeWarningInfo.FutureNetAccountWarning.Value)
                {
                    msg = LanguageUtil.GetValue(1546) + ": " + LanguageUtil.GetValue(2728);
                }
                if (oAccountAttributeWarningInfo.LossOfWorkWarning.Value)
                {
                    if (msg == null)
                    {
                        msg = LanguageUtil.GetValue(1546) + ": " + LanguageUtil.GetValue(2729);
                    }
                    else
                    {
                        msg = msg + "\\n" + LanguageUtil.GetValue(1546) + ": " + LanguageUtil.GetValue(2729);
                    }
                }
                if (msg != null)
                    msg = msg + "\\n" + LanguageUtil.GetValue(2221);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "newWindow", "ConfirmNetAccount('" + btnSave.ClientID + "','" + hdnConfirm.ClientID + "','" + msg + "');", true);
            }
            else
            {
                hdnConfirm.Value = "Yes";
            }
        }
    }
    public void ValidateIsBlankOwnerWarningOccur()
    {

        if (hdnConfirm_BlankOwner.Value != "Yes")
        {
            int userid = 0;
            int.TryParse(hdnOwner.Value, out userid);
            if (string.IsNullOrEmpty(txtOwner.Text) || string.IsNullOrEmpty(hdnOwner.Value) || userid <= 0)
            {
                string msg = null;
                msg = LanguageUtil.GetValue(2771);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "newWindow2", "ConfirmBlankOwner('" + btnSave.ClientID + "','" + hdnConfirm_BlankOwner.ClientID + "','" + msg + "');", true);
            }
            else
            {
                hdnConfirm_BlankOwner.Value = "Yes";
            }
        }
    }
    private void ValidateNetAccountsForOwnershipUpdate(int netAccountID, List<AccountHdrInfo> oAccountHdrInfoList)
    {
        if (netAccountID > 0)
        {
            List<AccountHdrInfo> oAccountHdrInfoListForNetAccount = oAccountHdrInfoList.FindAll(T => T.NetAccountID.GetValueOrDefault() == netAccountID);
            if (oAccountHdrInfoListForNetAccount != null && oAccountHdrInfoListForNetAccount.Count > 1)
            {
                AccountHdrInfo oAccountHdrInfoFirst = oAccountHdrInfoListForNetAccount[0];
                for (int j = 1; j < oAccountHdrInfoListForNetAccount.Count; j++)
                {
                    if (!NetAccountHelper.IsAccountOwnershipAttributesMatchForNetAccount(oAccountHdrInfoFirst, oAccountHdrInfoListForNetAccount[j]))
                    {
                        throw new ARTException(5000044);
                    }
                }
            }
        }
    }
    private void ValidatePRALogicForOwnershipMassUpdate(List<AccountHdrInfo> oAccountHdrInfoList)
    {
        if (oAccountHdrInfoList.Count > 0)
        {
            for (int i = 0; i < oAccountHdrInfoList.Count; i++)
            {
                List<int> oOwnerIDList = new List<int>();
                if (oAccountHdrInfoList[i].ReviewerUserID != null && oAccountHdrInfoList[i].PreparerUserID == null)
                {
                    throw new ARTException(2774);
                }
                if (oAccountHdrInfoList[i].ApproverUserID != null && oAccountHdrInfoList[i].ReviewerUserID == null)
                {
                    throw new ARTException(2775);
                }
                if ((oAccountHdrInfoList[i].PreparerUserID == null && oAccountHdrInfoList[i].BackupPreparerUserID != null)
                    || (oAccountHdrInfoList[i].ReviewerUserID == null && oAccountHdrInfoList[i].BackupReviewerUserID != null)
                    )
                {
                    throw new ARTException(5000328);
                }
                if (oAccountHdrInfoList[i].PreparerUserID.HasValue)
                    oOwnerIDList.Add(oAccountHdrInfoList[i].PreparerUserID.Value);
                if (oAccountHdrInfoList[i].BackupPreparerUserID.HasValue)
                    oOwnerIDList.Add(oAccountHdrInfoList[i].BackupPreparerUserID.Value);
                if (oAccountHdrInfoList[i].ReviewerUserID.HasValue)
                    oOwnerIDList.Add(oAccountHdrInfoList[i].ReviewerUserID.Value);
                if (oAccountHdrInfoList[i].BackupReviewerUserID.HasValue)
                    oOwnerIDList.Add(oAccountHdrInfoList[i].BackupReviewerUserID.Value);

                //if (oAccountHdrInfoList[i].PreparerUserID != null && oAccountHdrInfoList[i].ReviewerUserID != null && oAccountHdrInfoList[i].PreparerUserID == oAccountHdrInfoList[i].ReviewerUserID)
                //{
                //    throw new ARTException(5000086);
                //}
                //if (oAccountHdrInfoList[i].BackupPreparerUserID != null && oAccountHdrInfoList[i].BackupReviewerUserID != null && oAccountHdrInfoList[i].BackupPreparerUserID == oAccountHdrInfoList[i].BackupReviewerUserID)
                //{
                //    throw new ARTException(5000086);
                //}
                //if (oAccountHdrInfoList[i].BackupPreparerUserID != null
                //        || oAccountHdrInfoList[i].BackupReviewerUserID != null
                //        || oAccountHdrInfoList[i].BackupApproverUserID != null
                //        || oAccountHdrInfoList[i].PreparerUserID == oAccountHdrInfoList[i].BackupPreparerUserID || oAccountHdrInfoList[i].PreparerUserID == oAccountHdrInfoList[i].BackupReviewerUserID || oAccountHdrInfoList[i].PreparerUserID == oAccountHdrInfoList[i].BackupApproverUserID
                //           && oAccountHdrInfoList[i].ReviewerUserID == oAccountHdrInfoList[i].BackupPreparerUserID && oAccountHdrInfoList[i].ReviewerUserID == oAccountHdrInfoList[i].BackupReviewerUserID && oAccountHdrInfoList[i].ReviewerUserID == oAccountHdrInfoList[i].BackupApproverUserID
                //            && oAccountHdrInfoList[i].ApproverUserID == oAccountHdrInfoList[i].BackupPreparerUserID && oAccountHdrInfoList[i].ApproverUserID == oAccountHdrInfoList[i].BackupReviewerUserID && oAccountHdrInfoList[i].ApproverUserID == oAccountHdrInfoList[i].BackupApproverUserID)
                //{
                //    throw new ARTException(5000086);
                //}
                if (this._IsDualReviewEnabled)
                {

                    if ((oAccountHdrInfoList[i].ApproverUserID == null && oAccountHdrInfoList[i].BackupApproverUserID != null))
                    {
                        throw new ARTException(5000328);
                    }
                    //if (
                    //    oAccountHdrInfoList[i].ApproverUserID != null
                    //    && ((oAccountHdrInfoList[i].ReviewerUserID == oAccountHdrInfoList[i].ApproverUserID)
                    //        || (oAccountHdrInfoList[i].PreparerUserID == oAccountHdrInfoList[i].ApproverUserID))
                    //    )
                    //{
                    //    throw new ARTException(5000086);
                    //}
                    //if (oAccountHdrInfoList[i].BackupPreparerUserID != null && oAccountHdrInfoList[i].BackupApproverUserID != null && oAccountHdrInfoList[i].BackupPreparerUserID == oAccountHdrInfoList[i].BackupApproverUserID)
                    //{
                    //    throw new ARTException(5000086);
                    //}
                    //if (oAccountHdrInfoList[i].BackupReviewerUserID != null && oAccountHdrInfoList[i].BackupApproverUserID != null && oAccountHdrInfoList[i].BackupReviewerUserID == oAccountHdrInfoList[i].BackupApproverUserID)
                    //{
                    //    throw new ARTException(5000086);
                    //}
                    if (oAccountHdrInfoList[i].ApproverUserID.HasValue)
                        oOwnerIDList.Add(oAccountHdrInfoList[i].ApproverUserID.Value);
                    if (oAccountHdrInfoList[i].BackupApproverUserID.HasValue)
                        oOwnerIDList.Add(oAccountHdrInfoList[i].BackupApproverUserID.Value);
                }
                if (oOwnerIDList != null && oOwnerIDList.Count != oOwnerIDList.Distinct().ToList().Count)
                    throw new ARTException(5000086);
            }
        }
    }
    #endregion

    #region Private Method
    private void setErrorMessage()
    {
        rfvAccountAttribute.ErrorMessage = LanguageUtil.GetValue(5000053);
    }

    /// <summary>
    /// Hide panel
    /// </summary>
    private void HidePanels()
    {
        pnlAccounts.Visible = false;
        pnlMassUpdate.Visible = false;
        btnSave.Visible = false;
        btnReset.Visible = false;
        btnCancel.Visible = false;
    }
    private void HidePanels(bool IsMassUpdateClicked)
    {
        if (IsMassUpdateClicked)
        {
            pnlAccounts.Visible = false;
            pnlMassUpdate.Visible = true;
        }
        else
        {
            pnlAccounts.Visible = true;
            pnlMassUpdate.Visible = false;
        }
    }

    private void RaiseAlertIfOwnershipChanged(List<AccountHdrInfo> oAccountHdrInfoCollectionNew, List<AccountHdrInfo> oAccountHdrInfoCollection)
    {
        bool isAttributesMismatch = false;
        List<int> removedPreparerIDCollection = new List<int>();
        List<int> assignedPreparerIDCollection = new List<int>();

        Dictionary<int, List<AccountHdrInfo>> removedPreparerAccounts = new Dictionary<int, List<AccountHdrInfo>>();
        Dictionary<int, List<AccountHdrInfo>> assignedPreparerAccounts = new Dictionary<int, List<AccountHdrInfo>>();

        List<int> removedBackupPreparerIDCollection = new List<int>();
        List<int> assignedBackupPreparerIDCollection = new List<int>();

        Dictionary<int, List<AccountHdrInfo>> removedBackupPreparerAccounts = new Dictionary<int, List<AccountHdrInfo>>();
        Dictionary<int, List<AccountHdrInfo>> assignedBackupPreparerAccounts = new Dictionary<int, List<AccountHdrInfo>>();

        List<int> removedReviewerIDCollection = new List<int>();
        List<int> assignedReviewerIDCollection = new List<int>();

        Dictionary<int, List<AccountHdrInfo>> removedReviewerAccounts = new Dictionary<int, List<AccountHdrInfo>>();
        Dictionary<int, List<AccountHdrInfo>> assignedReviewerAccounts = new Dictionary<int, List<AccountHdrInfo>>();

        List<int> removedBackupReviewerIDCollection = new List<int>();
        List<int> assignedBackupReviewerIDCollection = new List<int>();

        Dictionary<int, List<AccountHdrInfo>> removedBackupReviewerAccounts = new Dictionary<int, List<AccountHdrInfo>>();
        Dictionary<int, List<AccountHdrInfo>> assignedBackupReviewerAccounts = new Dictionary<int, List<AccountHdrInfo>>();

        List<int> removedApproverIDCollection = new List<int>();
        List<int> assignedApproverIDCollection = new List<int>();

        Dictionary<int, List<AccountHdrInfo>> removedApproverAccounts = new Dictionary<int, List<AccountHdrInfo>>();
        Dictionary<int, List<AccountHdrInfo>> assignedApproverAccounts = new Dictionary<int, List<AccountHdrInfo>>();

        List<int> removedBackupApproverIDCollection = new List<int>();
        List<int> assignedBackupApproverIDCollection = new List<int>();

        Dictionary<int, List<AccountHdrInfo>> removedBackupApproverAccounts = new Dictionary<int, List<AccountHdrInfo>>();
        Dictionary<int, List<AccountHdrInfo>> assignedBackupApproverAccounts = new Dictionary<int, List<AccountHdrInfo>>();

        List<long> oAccountIDCollection = (from acc in oAccountHdrInfoCollectionNew select acc.AccountID.Value).ToList();


        foreach (AccountHdrInfo oNewAccountHdrInfo in oAccountHdrInfoCollectionNew)
        {
            AccountHdrInfo oOldAccountHdrInfo = (from acc in oAccountHdrInfoCollection where acc.AccountID == oNewAccountHdrInfo.AccountID select acc).FirstOrDefault();

            if (oNewAccountHdrInfo.PreparerUserID != oOldAccountHdrInfo.PreparerUserID)
            {
                isAttributesMismatch = true;
                if (oOldAccountHdrInfo.PreparerUserID.HasValue && oOldAccountHdrInfo.PreparerUserID.Value > 0)
                {
                    int removedPreparerID = (from remPrep in removedPreparerIDCollection where remPrep == oOldAccountHdrInfo.PreparerUserID.Value select remPrep).FirstOrDefault();
                    if (removedPreparerID == 0)
                        removedPreparerIDCollection.Add(oOldAccountHdrInfo.PreparerUserID.Value);
                    AddUserAccountToDictionary(removedPreparerAccounts, oOldAccountHdrInfo.PreparerUserID.Value, oNewAccountHdrInfo);
                }

                if (oNewAccountHdrInfo.PreparerUserID.HasValue && oNewAccountHdrInfo.PreparerUserID.Value > 0)
                {
                    int assignedPreparerID = (from remPrep in assignedPreparerIDCollection where remPrep == oNewAccountHdrInfo.PreparerUserID.Value select remPrep).FirstOrDefault();
                    if (assignedPreparerID == 0)
                        assignedPreparerIDCollection.Add(oNewAccountHdrInfo.PreparerUserID.Value);
                    AddUserAccountToDictionary(assignedPreparerAccounts, oNewAccountHdrInfo.PreparerUserID.Value, oNewAccountHdrInfo);
                }
            }

            if (this._IsDualReviewEnabled && oNewAccountHdrInfo.ApproverUserID != oOldAccountHdrInfo.ApproverUserID)
            {
                isAttributesMismatch = true;
                if (oOldAccountHdrInfo.ApproverUserID.HasValue && oOldAccountHdrInfo.ApproverUserID.Value > 0)
                {
                    int removedApproverID = (from remPrep in removedApproverIDCollection where remPrep == oOldAccountHdrInfo.ApproverUserID.Value select remPrep).FirstOrDefault();
                    if (removedApproverID == 0)
                        removedApproverIDCollection.Add(oOldAccountHdrInfo.ApproverUserID.Value);
                    AddUserAccountToDictionary(removedApproverAccounts, oOldAccountHdrInfo.ApproverUserID.Value, oNewAccountHdrInfo);
                }

                if (oNewAccountHdrInfo.ApproverUserID.HasValue && oNewAccountHdrInfo.ApproverUserID.Value > 0)
                {
                    int assignedApproverID = (from remPrep in assignedApproverIDCollection where remPrep == oNewAccountHdrInfo.ApproverUserID.Value select remPrep).FirstOrDefault();
                    if (assignedApproverID == 0)
                        assignedApproverIDCollection.Add(oNewAccountHdrInfo.ApproverUserID.Value);
                    AddUserAccountToDictionary(assignedApproverAccounts, oNewAccountHdrInfo.ApproverUserID.Value, oNewAccountHdrInfo);
                }
            }

            if (oNewAccountHdrInfo.ReviewerUserID != oOldAccountHdrInfo.ReviewerUserID)
            {
                isAttributesMismatch = true;
                if (oOldAccountHdrInfo.ReviewerUserID.HasValue && oOldAccountHdrInfo.ReviewerUserID.Value > 0)
                {
                    int removedReviewerID = (from remPrep in removedReviewerIDCollection where remPrep == oOldAccountHdrInfo.ReviewerUserID.Value select remPrep).FirstOrDefault();
                    if (removedReviewerID == 0)
                        removedReviewerIDCollection.Add(oOldAccountHdrInfo.ReviewerUserID.Value);
                    AddUserAccountToDictionary(removedReviewerAccounts, oOldAccountHdrInfo.ReviewerUserID.Value, oNewAccountHdrInfo);
                }

                if (oNewAccountHdrInfo.ReviewerUserID.HasValue && oNewAccountHdrInfo.ReviewerUserID.Value > 0)
                {
                    int assignedReviewerID = (from remPrep in assignedReviewerIDCollection where remPrep == oNewAccountHdrInfo.ReviewerUserID.Value select remPrep).FirstOrDefault();
                    if (assignedReviewerID == 0)
                        assignedReviewerIDCollection.Add(oNewAccountHdrInfo.ReviewerUserID.Value);
                    AddUserAccountToDictionary(assignedReviewerAccounts, oNewAccountHdrInfo.ReviewerUserID.Value, oNewAccountHdrInfo);
                }
            }

            #region Backup Roles

            if (Helper.IsFeatureActivated(WebEnums.Feature.AccountOwnershipBackup, SessionHelper.CurrentReconciliationPeriodID))
            {
                //Backup Preparer
                if (oNewAccountHdrInfo.BackupPreparerUserID != oOldAccountHdrInfo.BackupPreparerUserID)
                {
                    isAttributesMismatch = true;
                    if (oOldAccountHdrInfo.BackupPreparerUserID.HasValue && oOldAccountHdrInfo.BackupPreparerUserID.Value > 0)
                    {
                        int removedBackupPreparerID = (from remBackupPrep in removedBackupPreparerIDCollection where remBackupPrep == oOldAccountHdrInfo.BackupPreparerUserID.Value select remBackupPrep).FirstOrDefault();
                        if (removedBackupPreparerID == 0)
                            removedBackupPreparerIDCollection.Add(oOldAccountHdrInfo.BackupPreparerUserID.Value);
                        AddUserAccountToDictionary(removedBackupPreparerAccounts, oOldAccountHdrInfo.BackupPreparerUserID.Value, oNewAccountHdrInfo);
                    }

                    if (oNewAccountHdrInfo.BackupPreparerUserID.HasValue && oNewAccountHdrInfo.BackupPreparerUserID.Value > 0)
                    {
                        int assignedBackupPreparerID = (from remBackupPrep in assignedBackupPreparerIDCollection where remBackupPrep == oNewAccountHdrInfo.BackupPreparerUserID.Value select remBackupPrep).FirstOrDefault();
                        if (assignedBackupPreparerID == 0)
                            assignedBackupPreparerIDCollection.Add(oNewAccountHdrInfo.BackupPreparerUserID.Value);
                        AddUserAccountToDictionary(assignedBackupPreparerAccounts, oNewAccountHdrInfo.BackupPreparerUserID.Value, oNewAccountHdrInfo);
                    }
                }

                //Backup Reviewer
                if (oNewAccountHdrInfo.BackupReviewerUserID != oOldAccountHdrInfo.BackupReviewerUserID)
                {
                    isAttributesMismatch = true;
                    if (oOldAccountHdrInfo.BackupReviewerUserID.HasValue && oOldAccountHdrInfo.BackupReviewerUserID.Value > 0)
                    {
                        int removedBackupReviewerID = (from remBackupRev in removedBackupReviewerIDCollection where remBackupRev == oOldAccountHdrInfo.BackupReviewerUserID.Value select remBackupRev).FirstOrDefault();
                        if (removedBackupReviewerID == 0)
                            removedBackupReviewerIDCollection.Add(oOldAccountHdrInfo.BackupReviewerUserID.Value);
                        AddUserAccountToDictionary(removedBackupReviewerAccounts, oOldAccountHdrInfo.BackupReviewerUserID.Value, oNewAccountHdrInfo);
                    }

                    if (oNewAccountHdrInfo.BackupReviewerUserID.HasValue && oNewAccountHdrInfo.BackupReviewerUserID.Value > 0)
                    {
                        int assignedBackupReviewerID = (from remBackupRev in assignedBackupReviewerIDCollection where remBackupRev == oNewAccountHdrInfo.BackupReviewerUserID.Value select remBackupRev).FirstOrDefault();
                        if (assignedBackupReviewerID == 0)
                            assignedBackupReviewerIDCollection.Add(oNewAccountHdrInfo.BackupReviewerUserID.Value);
                        AddUserAccountToDictionary(assignedBackupReviewerAccounts, oNewAccountHdrInfo.BackupReviewerUserID.Value, oNewAccountHdrInfo);
                    }
                }

                //Backup Approver
                if (this._IsDualReviewEnabled && oNewAccountHdrInfo.BackupApproverUserID != oOldAccountHdrInfo.BackupApproverUserID)
                {
                    isAttributesMismatch = true;
                    if (oOldAccountHdrInfo.BackupApproverUserID.HasValue && oOldAccountHdrInfo.BackupApproverUserID.Value > 0)
                    {
                        int removedBackupApproverID = (from remPrep in removedBackupApproverIDCollection where remPrep == oOldAccountHdrInfo.BackupApproverUserID.Value select remPrep).FirstOrDefault();
                        if (removedBackupApproverID == 0)
                            removedBackupApproverIDCollection.Add(oOldAccountHdrInfo.BackupApproverUserID.Value);
                        AddUserAccountToDictionary(removedBackupApproverAccounts, oOldAccountHdrInfo.BackupApproverUserID.Value, oNewAccountHdrInfo);
                    }

                    if (oNewAccountHdrInfo.BackupApproverUserID.HasValue && oNewAccountHdrInfo.BackupApproverUserID.Value > 0)
                    {
                        int assignedBackupApproverID = (from remPrep in assignedBackupApproverIDCollection where remPrep == oNewAccountHdrInfo.BackupApproverUserID.Value select remPrep).FirstOrDefault();
                        if (assignedBackupApproverID == 0)
                            assignedBackupApproverIDCollection.Add(oNewAccountHdrInfo.BackupApproverUserID.Value);
                        AddUserAccountToDictionary(assignedBackupApproverAccounts, oNewAccountHdrInfo.BackupApproverUserID.Value, oNewAccountHdrInfo);
                    }
                }
            }

            #endregion
        }

        // Remove InActive USERS
        short CurrentRoleID = SessionHelper.CurrentRoleID.Value;
        CurrentRoleID = 2;// fetch all record 
        IUser oUserClient = RemotingHelper.GetUserObject();
        List<UserHdrInfo> oInActiveUserCollection = oUserClient.SearchUser(null, null, null, 0, null, false, 
            SessionHelper.CurrentCompanyID, SessionHelper.CurrentUserID.Value, CurrentRoleID, SessionHelper.CurrentReconciliationPeriodID, 
            SessionHelper.CurrentReconciliationPeriodEndDate, null, null, false, 2, (short)ARTEnums.ActivationStatusType.UserActivationStatus, null, Helper.GetAppUserInfo());
        for (int i = 0; i < oInActiveUserCollection.Count; i++)
        {
            int InActiveUserID = oInActiveUserCollection[i].UserID.Value;

            removedPreparerIDCollection.Remove(InActiveUserID);
            assignedPreparerIDCollection.Remove(InActiveUserID);

            removedPreparerAccounts.Remove(InActiveUserID);
            assignedPreparerAccounts.Remove(InActiveUserID);

            removedBackupPreparerIDCollection.Remove(InActiveUserID);
            assignedBackupPreparerIDCollection.Remove(InActiveUserID);

            removedBackupPreparerAccounts.Remove(InActiveUserID);
            assignedBackupPreparerAccounts.Remove(InActiveUserID);

            removedReviewerIDCollection.Remove(InActiveUserID);
            assignedReviewerIDCollection.Remove(InActiveUserID);

            removedReviewerAccounts.Remove(InActiveUserID);
            assignedReviewerAccounts.Remove(InActiveUserID);

            removedBackupReviewerIDCollection.Remove(InActiveUserID);
            assignedBackupReviewerIDCollection.Remove(InActiveUserID);

            removedBackupReviewerAccounts.Remove(InActiveUserID);
            assignedBackupReviewerAccounts.Remove(InActiveUserID);

            removedApproverIDCollection.Remove(InActiveUserID);
            assignedApproverIDCollection.Remove(InActiveUserID);

            removedApproverAccounts.Remove(InActiveUserID);
            assignedApproverAccounts.Remove(InActiveUserID);

            removedBackupApproverIDCollection.Remove(InActiveUserID);
            assignedBackupApproverIDCollection.Remove(InActiveUserID);

            removedBackupApproverAccounts.Remove(InActiveUserID);
            assignedBackupApproverAccounts.Remove(InActiveUserID);
        }

        if (isAttributesMismatch)
        {
            AlertHelper.RaiseAlertForAlertUserAboutRoleAndAccountChangesAlert(oAccountIDCollection, removedPreparerIDCollection, removedReviewerIDCollection,
                removedApproverIDCollection, assignedPreparerIDCollection, assignedReviewerIDCollection, assignedApproverIDCollection,
                removedBackupPreparerIDCollection, assignedBackupPreparerIDCollection, removedBackupReviewerIDCollection, assignedBackupReviewerIDCollection,
                removedBackupApproverIDCollection, assignedBackupApproverIDCollection, removedPreparerAccounts, removedReviewerAccounts,
                removedApproverAccounts, assignedPreparerAccounts, assignedReviewerAccounts, assignedApproverAccounts,
                removedBackupPreparerAccounts, assignedBackupPreparerAccounts, removedBackupReviewerAccounts, assignedBackupReviewerAccounts,
                removedBackupApproverAccounts, assignedBackupApproverAccounts);
        }
    }
    private void AddUserAccountToDictionary(Dictionary<int, List<AccountHdrInfo>> dictUserAccount, int userID, AccountHdrInfo oAccountHdrInfo)
    {
        List<AccountHdrInfo> accountIDList = null;
        if (dictUserAccount.ContainsKey(userID))
        {
            accountIDList = dictUserAccount[userID];
        }
        else
        {
            accountIDList = new List<AccountHdrInfo>();
        }
        accountIDList.Add(oAccountHdrInfo);
        dictUserAccount[userID] = accountIDList;
    }
    private void PopulateAttributeDropdown()
    {
        List<AccountAttributeMstInfo> oAccountAttributeMstInfoCollection = new List<AccountAttributeMstInfo>();
        AccountAttributeMstInfo oAccountAttributeMstInfo = null;

        oAccountAttributeMstInfo = new AccountAttributeMstInfo();
        oAccountAttributeMstInfo.AccountAttributeID = (short)ARTEnums.UserRole.PREPARER;
        oAccountAttributeMstInfo.Name = LanguageUtil.GetValue(1130);
        oAccountAttributeMstInfoCollection.Add(oAccountAttributeMstInfo);

        oAccountAttributeMstInfo = new AccountAttributeMstInfo();
        oAccountAttributeMstInfo.AccountAttributeID = (short)ARTEnums.UserRole.REVIEWER;
        oAccountAttributeMstInfo.Name = LanguageUtil.GetValue(1131);
        oAccountAttributeMstInfoCollection.Add(oAccountAttributeMstInfo);

        if (Helper.IsFeatureActivated(WebEnums.Feature.AccountOwnershipBackup, SessionHelper.CurrentReconciliationPeriodID))
        {
            oAccountAttributeMstInfo = new AccountAttributeMstInfo();
            oAccountAttributeMstInfo.AccountAttributeID = (short)ARTEnums.UserRole.BACKUP_PREPARER;
            oAccountAttributeMstInfo.Name = LanguageUtil.GetValue(2501);
            oAccountAttributeMstInfoCollection.Add(oAccountAttributeMstInfo);

            oAccountAttributeMstInfo = new AccountAttributeMstInfo();
            oAccountAttributeMstInfo.AccountAttributeID = (short)ARTEnums.UserRole.BACKUP_REVIEWER;
            oAccountAttributeMstInfo.Name = LanguageUtil.GetValue(2502);
            oAccountAttributeMstInfoCollection.Add(oAccountAttributeMstInfo);

        }
        if (this._IsDualReviewEnabled)
        {
            oAccountAttributeMstInfo = new AccountAttributeMstInfo();
            oAccountAttributeMstInfo.AccountAttributeID = (short)ARTEnums.UserRole.APPROVER;
            oAccountAttributeMstInfo.Name = LanguageUtil.GetValue(1132);
            oAccountAttributeMstInfoCollection.Add(oAccountAttributeMstInfo);

            if (Helper.IsFeatureActivated(WebEnums.Feature.AccountOwnershipBackup, SessionHelper.CurrentReconciliationPeriodID))
            {
                oAccountAttributeMstInfo = new AccountAttributeMstInfo();
                oAccountAttributeMstInfo.AccountAttributeID = (short)ARTEnums.UserRole.BACKUP_APPROVER;
                oAccountAttributeMstInfo.Name = LanguageUtil.GetValue(2503);
                oAccountAttributeMstInfoCollection.Add(oAccountAttributeMstInfo);
            }
        }


        ////List<AccountAttributeMstInfo> oAccountAttributeMstInfoCollection = oUtilityClient.SelectAccountAttributeMstForMassUpdate(oAccountAttributeCollection);
        //List<AccountAttributeMstInfo> oAccountAttributeMstInfoCollection = oUtilityClient.SelectAccountAttributeMstForMassUpdate(SessionHelper.CurrentCompanyID, SessionHelper.CurrentReconciliationPeriodID, Helper.GetAppUserInfo());
        //oAccountAttributeMstInfoCollection = LanguageHelper.TranslateLabelAccountAttribute(oAccountAttributeMstInfoCollection);

        int selectedIndex = ddlAccountAttribute.SelectedIndex;
        ddlAccountAttribute.DataSource = oAccountAttributeMstInfoCollection.OrderBy(o => o.AccountAttributeID);
        ddlAccountAttribute.DataTextField = "Name";
        ddlAccountAttribute.DataValueField = "AccountAttributeID";
        ddlAccountAttribute.DataBind();

        ListControlHelper.AddListItemForSelectOne(ddlAccountAttribute);

        ddlAccountAttribute.SelectedIndex = selectedIndex;
    }
    #endregion

    #region Other Method
    protected void ucAccountSearchControl_SearchAndMailClickEventHandler(AccountSearchCriteria oAccountSearchCriteria)
    {
        try
        {
            MasterPageBase oMasterPageBase = (MasterPageBase)this.Master;
            RequestHelper objRequestHelper = new RequestHelper();
            oAccountSearchCriteria.PageID = (int)WebEnums.AccountPages.AccountOwnership;
            int searchResultCount = objRequestHelper.StartSaveBulkExport(ucSkyStemARTGrid.RgAccount, ARTEnums.Grid.AccountOwnership, false, oAccountSearchCriteria);
            string confirmationMessage = string.Empty;
            if (searchResultCount == 0)
            {
                confirmationMessage = LanguageUtil.GetValue(2488);
            }
            else
            {
                string messageExport = LanguageUtil.GetValue(2489);
                messageExport = string.Format(messageExport, searchResultCount.ToString());
                confirmationMessage = messageExport;
            }
            oMasterPageBase.ShowConfirmationMessage(confirmationMessage);

        }
        catch (Exception ex)
        {
            Helper.ShowErrorMessage(this, ex);
        }
    }

    /// <summary>
    /// Handles user controls Search click event
    /// </summary>
    /// <param name="oAccountHdrInfoCollection">List of accounts</param>
    protected void ucAccountSearchControl_BulkUpdateClickEventHandler(List<AccountHdrInfo> oAccountHdrInfoCollection)
    {
        try
        {
            HttpContext.Current.Session[SessionConstants.SEARCH_RESULTS_ACCOUNT] = oAccountHdrInfoCollection;
            Sel.Value = string.Empty;
            ucSkyStemARTGrid.Grid.ClientSettings.ClientEvents.OnRowSelecting = "Selecting";
            MasterPageBase oMasterPageBase = (MasterPageBase)this.Master;
            oMasterPageBase.HideMessage();

            int maxSize = Convert.ToInt32(AppSettingHelper.GetAppSettingValue(AppSettingConstants.MAX_RECORD_SIZE_FOR_NONPAGED_GRIDS));

            if (oAccountHdrInfoCollection.Count > maxSize)
            {
                throw new ARTException(5000087);
            }

            ucSkyStemARTGrid.Grid.AllowPaging = false;
            ucSkyStemARTGrid.CompanyID = SessionHelper.CurrentCompanyID;
            ucSkyStemARTGrid.DataSource = oAccountHdrInfoCollection;

            if (CurrentRecProcessStatus.Value == WebEnums.RecPeriodStatus.NotStarted
                    || CurrentRecProcessStatus.Value == WebEnums.RecPeriodStatus.InProgress
                    || CurrentRecProcessStatus.Value == WebEnums.RecPeriodStatus.Open)
            {
                ucSkyStemARTGrid.ShowSelectCheckBoxColum = true;
            }
            else
            {
                ucSkyStemARTGrid.ShowSelectCheckBoxColum = false;
            }
            ucSkyStemARTGrid.BindGrid();
            ucSkyStemARTGrid.DataBind();

            pnlAccounts.Visible = true;
            pnlMassUpdate.Visible = false;

            if (CurrentRecProcessStatus.Value == WebEnums.RecPeriodStatus.Closed
                || CurrentRecProcessStatus.Value == WebEnums.RecPeriodStatus.Skipped
                || CertificationHelper.IsCertificationStarted())
            {
                btnSave.Visible = false;
                ucSkyStemARTGrid.ShowSelectCheckBoxColum = false;
                btnReset.Visible = false;
            }
            else
            {
                btnSave.Visible = true;
                btnReset.Visible = true;
            }
            btnCancel.Visible = true;

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
    /// Gets menu key
    /// </summary>
    /// <returns>Menu key</returns>
    public override string GetMenuKey()
    {
        return MENU_KEY;
    }

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
                IFSCaption oFSCaption = RemotingHelper.GetFSCaptioneObject();
                oFSCaptionCollection = oFSCaption.SelectFSCaptionByCompanyIDAndPrefixText(companyId, prefixText, count
                    , SessionHelper.GetUserLanguage(), SessionHelper.GetBusinessEntityID(), AppSettingHelper.GetDefaultLanguageID(), Helper.GetAppUserInfo());

                if (oFSCaptionCollection == null || oFSCaptionCollection.Length == 0)
                {
                    oFSCaptionCollection = new string[] { DEFAULT_STRING_FOR_SEARCH };
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
    public static string[] AutoCompleteUserName(string prefixText, int count)
    {
        string[] oUserNameCollection = null;

        try
        {
            if (SessionHelper.CurrentCompanyID.HasValue)
            {
                int companyId = SessionHelper.CurrentCompanyID.Value;
                IUser oUser = RemotingHelper.GetUserObject();
                oUserNameCollection = oUser.SelectActiveUserNamesByCompanyIdAndPrefixText(companyId, prefixText, count, Helper.GetAppUserInfo());

                if (oUserNameCollection == null || oUserNameCollection.Length == 0)
                {
                    oUserNameCollection = new string[] { DEFAULT_STRING_FOR_SEARCH };
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

        return oUserNameCollection;
    }
    public void ompage_ReconciliationPeriodChangedEventHandler(object sender, EventArgs e)
    {
        HidePanels();
        ucAccountSearchControl.ReloadControl();
    }
    public void ucAccountSearchControl_MassUpdateClickEventHandler(List<AccountHdrInfo> oAccountHdrInfoCollection)
    {
        try
        {
            HttpContext.Current.Session[SessionConstants.SEARCH_RESULTS_ACCOUNT] = oAccountHdrInfoCollection;
            PopulateAttributeDropdown();
            Sel.Value = string.Empty;
            SkyStemARTGridMassUpdate.Grid.ClientSettings.ClientEvents.OnRowSelecting = "Selecting";
            MasterPageBase oMasterPageBase = (MasterPageBase)this.Master;
            oMasterPageBase.HideMessage();

            int maxSize = Convert.ToInt32(AppSettingHelper.GetAppSettingValue(AppSettingConstants.MAX_RECORD_SIZE_FOR_NONPAGED_GRIDS));

            if (oAccountHdrInfoCollection.Count > maxSize)
            {
                throw new ARTException(5000087);
            }

            SkyStemARTGridMassUpdate.Grid.AllowPaging = false;
            SkyStemARTGridMassUpdate.CompanyID = SessionHelper.CurrentCompanyID;
            SkyStemARTGridMassUpdate.DataSource = oAccountHdrInfoCollection;

            if (CurrentRecProcessStatus.Value == WebEnums.RecPeriodStatus.NotStarted
                    || CurrentRecProcessStatus.Value == WebEnums.RecPeriodStatus.InProgress
                    || CurrentRecProcessStatus.Value == WebEnums.RecPeriodStatus.Open)
            {
                SkyStemARTGridMassUpdate.ShowSelectCheckBoxColum = true;
            }
            else
            {
                SkyStemARTGridMassUpdate.ShowSelectCheckBoxColum = false;
            }
            SkyStemARTGridMassUpdate.BindGrid();
            SkyStemARTGridMassUpdate.DataBind();

            pnlAccounts.Visible = false;
            pnlMassUpdate.Visible = true;

            if (CurrentRecProcessStatus == WebEnums.RecPeriodStatus.Closed
                || CurrentRecProcessStatus == WebEnums.RecPeriodStatus.Skipped
                || CertificationHelper.IsCertificationStarted())
            {
                btnSave.Visible = false;
                SkyStemARTGridMassUpdate.ShowSelectCheckBoxColum = false;
                btnReset.Visible = false;
            }
            else
            {
                btnSave.Visible = true;
                btnReset.Visible = false;
            }
            btnCancel.Visible = true;
            // ShowHideOwnersDDL();
            hdnOwner.Value = "";
            txtOwner.Text = "";
            hdnConfirm_BlankOwner.Value = "";

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
    /// This method is used to auto populate User Name text box based on the basis of 
    /// the prefix text typed in the text box
    /// </summary>
    /// <param name="prefixText">The text which was typed in the text box</param>
    /// <param name="count">Number of results to be returned</param>
    /// <returns>List of User Names</returns>
    [System.Web.Services.WebMethod()]
    [System.Web.Script.Services.ScriptMethod()]
    public static List<string> AutoCompleteOwnerName(string prefixText, int count)
    {
        List<string> UserNames = new List<string>();

        try
        {
            if (SessionHelper.CurrentCompanyID.HasValue && SelectedAttributeID.HasValue && SelectedAttributeID.GetValueOrDefault() > 0)
            {
                int companyId = SessionHelper.CurrentCompanyID.Value;
                IUser oUserClient = RemotingHelper.GetUserObject();

                List<UserHdrInfo> oUserHdrInfoList = oUserClient.SelectActiveUserHdrInfoByCompanyRoleAndPrefixText(companyId, SelectedAttributeID.GetValueOrDefault(), prefixText, count, Helper.GetAppUserInfo());
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
    public string ResolveClientUrlPath(string relativeUrl)
    {
        string url;
        url = Page.ResolveClientUrl(relativeUrl);
        return url;
    }
    #endregion

}
