
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using SkyStem.ART.Client.Model;
using SkyStem.ART.Web.Utility;
using SkyStem.ART.Web.Classes;
using SkyStem.ART.Client.IServices;
using SkyStem.ART.Client.Exception;
using SkyStem.ART.Client.Data;
using Telerik.Web.UI;
using SkyStem.ART.Web.Data;
using AjaxControlToolkit;
using System.Text;
using System.Data;
using System.IO;
using SkyStem.ART.Client.Model.BulkExportExcel;
using SkyStem.ART.Client.IServices.BulkExportToExcel;
using System.Threading;
using SkyStem.Language.LanguageUtility;
using SkyStem.Library.Controls.WebControls;
using SkyStem.Library.Controls.TelerikWebControls;

public partial class Pages_AccountProfileMassAndBulkUpdate : PageBaseRecPeriod
{
    #region Variables & Constants
    bool _IsRiskRatingEnabled;
    bool _IsZeroBalanceEnabled;
    bool _IsKeyAccountEnabled;
    bool _IsNetAccountEnabled;
    bool _IsReconcilableEnabled;
    bool _IsDualLevelReviewEnabled;
    bool _IsDueDateByAccountEnabled;
    bool _IsMassUpdateClicked;
    #region Constants

    private const string COLUMN_NAME_ID = "ID";
    private const string COLUMN_NAME_NETACCOUNTID = "NetAccountID";
    private const string GRID_ONROWSELECTED_EVENT_VALUE = "EnableUserInputControls";//"ValidateUserInputForBulkUpdate";
    private const string GRID_ONROWDESELECTED_EVENT_VALUE = "DisableUserInputControls";//"DevalidateUserInputForBulkUpdate";
    private const string GRID_ONROWCREATED_EVENT_VALUE = "OnRowCreated";
    private const string DEFAULT_STRING_FOR_SEARCH = "No Records found";
    private const string GRID_ONROWSELECTING_EVENT_VALUE = "RowSelecting";

    #endregion
    #endregion

    #region Properties
    private List<CompanyCapabilityInfo> _CompanyCapabilityInfoCollection = null;
    List<AccountReconciliationPeriodInfo> _AccountReconciliationPeriodInfoCollection = null;
    #endregion

    #region Delegates & Events
    public delegate void ExportExcelAsyncDelegate(ExRadGrid exportGrid);
    #endregion

    #region Page Events
    /// <summary>
    /// Initializes controls value and variables on page load
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        Helper.RegisterPostBackToControls(this, ucSkyStemARTGridMassUpdate.Grid);

        ucSkyStemARTGridBulkUpdate.HideColumnAccountMassBuklupd = true;

        try
        {
            ucAccountSearchControl.PnlSearch.Visible = false;
            ucAccountSearchControl.ChkBoxlabelID = 1706;

            ucAccountSearchControl.PnlSearchAndMail.Visible = true;
            ucAccountSearchControl.ShowDueDaysRow = true;
            //ExButton btnSearchAndMail =  ((ExButton)ucAccountSearchControl.FindControl("btnSearchAndMail"));
            //if (btnSearchAndMail != null)
            //{
            //    btnSearchAndMail.Visible = true;
            //}

            
            ucAccountSearchControl.SearchAndMailClickEventHandler += new UserControls_AccountSearchControl.SearchAndMail(ucAccountSearchControl_SearchAndMailClickEventHandler);
            ucAccountSearchControl.BulkUpdateClickEventHandler += new UserControls_AccountSearchControl.ShowSearchResults(ucAccountSearchControl_BulkUpdateClickEventHandler);
            ucAccountSearchControl.MassUpdateClickEventHandler += new UserControls_AccountSearchControl.ShowSearchResults(ucAccountSearchControl_MassUpdateClickEventHandler);
            ucSkyStemARTGridBulkUpdate.GridItemDataBound += new Telerik.Web.UI.GridItemEventHandler(ucSkyStemARTGridBulkUpdate_GridItemDataBound);
            ucSkyStemARTGridMassUpdate.GridItemDataBound += new Telerik.Web.UI.GridItemEventHandler(ucSkyStemARTGridMassUpdate_GridItemDataBound);
            ucSkyStemARTGridMassUpdate.Grid_NeedDataSourceEventHandler += new UserControls_SkyStemARTGrid.Grid_NeedDataSource(ucSkyStemARTGridMassUpdate_Grid_NeedDataSourceEventHandler);
            ucSkyStemARTGridBulkUpdate.Grid.AllowPaging = false;
            ucSkyStemARTGridMassUpdate.Grid.AllowPaging = false;
            ucSkyStemARTGridBulkUpdate.Grid.ClientSettings.ClientEvents.OnRowSelected = GRID_ONROWSELECTED_EVENT_VALUE;
            ucSkyStemARTGridBulkUpdate.Grid.ClientSettings.ClientEvents.OnRowDeselected = GRID_ONROWDESELECTED_EVENT_VALUE;
            ucSkyStemARTGridBulkUpdate.Grid.ClientSettings.ClientEvents.OnRowCreated = GRID_ONROWCREATED_EVENT_VALUE;
            ucSkyStemARTGridBulkUpdate.Grid.ClientSettings.ClientEvents.OnRowSelecting = GRID_ONROWSELECTING_EVENT_VALUE;
            ucSkyStemARTGridMassUpdate.Grid.ClientSettings.ClientEvents.OnRowSelecting = GRID_ONROWSELECTING_EVENT_VALUE;
            ucSkyStemARTGridBulkUpdate.Grid.EntityNameLabelID = 1071;
            ucSkyStemARTGridMassUpdate.Grid.EntityNameLabelID = 1071;
            ucSkyStemARTGridBulkUpdate.Grid_NeedDataSourceEventHandler += new UserControls_SkyStemARTGrid.Grid_NeedDataSource(ucSkyStemARTGridBulkUpdate_Grid_NeedDataSourceEventHandler);

            MasterPageBase ompage = (MasterPageBase)this.Master;
            ompage.ReconciliationPeriodChangedEventHandler += new EventHandler(ompage_ReconciliationPeriodChangedEventHandler);

            this._CompanyCapabilityInfoCollection = SessionHelper.GetCompanyCapabilityCollectionForCurrentRecPeriod();
            this._IsMassUpdateClicked = pnlMassUpdate.Visible;
            SetCapabilityInfo();
            PopulateAttributeDropdown();

            Helper.SetPageTitle(this, 1214);
            Helper.ShowInputRequirementSection(this, 1594, 1595, 2496);

            if (CurrentRecProcessStatus.Value == WebEnums.RecPeriodStatus.Closed
                || CurrentRecProcessStatus.Value == WebEnums.RecPeriodStatus.Skipped)
            {
                lblAttribute.Visible = false;
                ddlAccountAttribute.Visible = false;
            }
            else
            {
                lblAttribute.Visible = true;
                ddlAccountAttribute.Visible = true;
            }

            ucRecFrequencySelectionMass.URL = Helper.GetUrlForRecFrequency(null, txtRecPriodIDContainer.ClientID, string.Empty, Helper.GetFormModeForAccountPages());

            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "RecPeriodStatus", "<script Language='javascript'> var recPeriodStatus = '" + CurrentRecProcessStatus.Value.ToString() + "'; var hdnBulkUpdateModeID = '" + hdnBulkUpdateMode.ClientID + "' </script>");
            ucAccountSearchControl.ParentPage = WebEnums.AccountPages.AccountProfileUpdate;
        }
        catch (Exception ex)
        {
            Helper.ShowErrorMessage(this, ex);
        }
    }
    protected void Page_PreRender(Object source, EventArgs e)
    {


        DropDownList ddlRiskratiingtoStForHyperLink = (DropDownList)ddlRiskRating.FindControl("ddlRiskRating");
        ExHyperLink hlRecFrequency = (ExHyperLink)ucRiskRatingPeriod.FindControl("hlRecFrequency");
        ddlRiskratiingtoStForHyperLink.Attributes.Add("onchange", "SetURLForRiskRating('" + ddlRiskratiingtoStForHyperLink.ClientID + "', '" + hlRecFrequency.ClientID + "')");
        if (ddlRiskRating.SelectedValue != "" && ddlRiskRating.SelectedValue != WebConstants.SELECT_ONE)
        {
            hlRecFrequency.Attributes.CssStyle.Add("visibility", "visible");
        }
        else
            hlRecFrequency.Attributes.CssStyle.Add("visibility", "hidden");



    }
    #endregion

    #region Grid Events

    #region "Bulk Update"

    /// <summary>
    /// Handles rad grids item data bound event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    void ucSkyStemARTGridBulkUpdate_GridItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {
        try
        {
            if (e.Item.ItemType == Telerik.Web.UI.GridItemType.Item || e.Item.ItemType == Telerik.Web.UI.GridItemType.AlternatingItem)
            {
                StringBuilder oSBhlkControls = new StringBuilder();
                StringBuilder oSBchkControls = new StringBuilder();
                StringBuilder oSBddlControls = new StringBuilder();
                StringBuilder oSBtxtDueDaysControls = new StringBuilder();
                StringBuilder oSBhdnControls = new StringBuilder();
                StringBuilder oSBvldControls = new StringBuilder();

                AccountHdrInfo oAccountHdrInfo = (AccountHdrInfo)e.Item.DataItem;
                IAccount oAccountClient = RemotingHelper.GetAccountObject();
                this._AccountReconciliationPeriodInfoCollection = oAccountClient.SelectAccountRecPeriodByAccountID(Convert.ToInt32(oAccountHdrInfo.AccountID.Value), Helper.GetAppUserInfo());

                HiddenField hdnIsReconciled = (HiddenField)e.Item.FindControl("hdnIsReconciled");
                this.AddToStringBuilder(oSBhdnControls, hdnIsReconciled.ClientID);
                hdnIsReconciled.Value = "";
                if (oAccountHdrInfo.ReconciliationStatusID == (short)WebEnums.ReconciliationStatus.Reconciled
                    || oAccountHdrInfo.ReconciliationStatusID == (short)WebEnums.ReconciliationStatus.SysReconciled)
                    hdnIsReconciled.Value = "1";
                ExLabel lblAccountNumber = (ExLabel)e.Item.FindControl("lblAccountNumber");
                lblAccountNumber.Text = Helper.GetDisplayStringValue(oAccountHdrInfo.AccountNumber);
                ExLabel lblAccountName = (ExLabel)e.Item.FindControl("lblAccountName");
                lblAccountName.Text = Helper.GetDisplayStringValue(oAccountHdrInfo.AccountName);
                ExLabel lblNetAccount = (ExLabel)e.Item.FindControl("lblNetAccount");
                lblNetAccount.Text = Helper.GetDisplayStringValue(oAccountHdrInfo.NetAccount);

                //IsKeyAccount
                ExCheckBox chkKeyAccount = (ExCheckBox)e.Item.FindControl("chkKeyAccount");
                if (oAccountHdrInfo.IsKeyAccount.HasValue)
                {
                    this.AddToStringBuilder(oSBchkControls, "chkKeyAccount=" + chkKeyAccount.ClientID);
                    chkKeyAccount.Checked = oAccountHdrInfo.IsKeyAccount.Value;
                    chkKeyAccount.InputAttributes.Add("disabled", "disabled");//Disable IsKeyAccount checkbox at client side
                }

                //IsZeroBalance
                ExCheckBox chkZeroBalanceAccount = (ExCheckBox)e.Item.FindControl("chkZeroBalanceAccount");
                if (oAccountHdrInfo.IsZeroBalance.HasValue)
                {
                    this.AddToStringBuilder(oSBchkControls, "chkZeroBalance=" + chkZeroBalanceAccount.ClientID);
                    chkZeroBalanceAccount.Checked = oAccountHdrInfo.IsZeroBalance.Value;
                    chkZeroBalanceAccount.InputAttributes.Add("disabled", "disabled");//Disable IsZeroBalance checkbox as client side
                }

                //Reconcilable
                ExCheckBox chkIsReconcilable = (ExCheckBox)e.Item.FindControl("chkIsReconcilable");
                if (chkIsReconcilable != null && this._IsReconcilableEnabled)
                {
                    this.AddToStringBuilder(oSBchkControls, "chkReconcilable=" + chkIsReconcilable.ClientID);
                    chkIsReconcilable.Checked = oAccountHdrInfo.IsReconcilable.Value;
                    chkIsReconcilable.InputAttributes.Add("disabled", "disabled");//Disable IsReconcilable checkbox as client side
                }

                //RiskRating OR RecFrequency
                if (this._IsRiskRatingEnabled)
                {
                    UserControls_RiskRatingDropDown oUserControls_RiskRatingDropDown = (UserControls_RiskRatingDropDown)e.Item.FindControl("ddlRiskRating");
                    UserControls_PopupRecFrequency oUserControls_PopupRecFrequency = (UserControls_PopupRecFrequency)e.Item.FindControl("ucriskRatingBulkUpdate");
                    //DropDownList ddlRiskratiingtoStForHyperLink = (DropDownList)oUserControls_RiskRatingDropDown.FindControl("ddlRiskRating");
                    DropDownList ddlRiskratiingtoStForHyperLink = oUserControls_RiskRatingDropDown.DropDown;
                    ExHyperLink hlRecFrequency = (ExHyperLink)oUserControls_PopupRecFrequency.FindControl("hlRecFrequency");
                    ddlRiskratiingtoStForHyperLink.Attributes.Add("onchange", "SetURLForRiskRating('" + ddlRiskratiingtoStForHyperLink.ClientID + "', '" + hlRecFrequency.ClientID + "')");

                    ExLabel lblRiskRatingExport = (ExLabel)e.Item.FindControl("lblRiskRatingExport");
                    if (oAccountHdrInfo.RiskRatingID.HasValue && oAccountHdrInfo.RiskRatingID.Value > 0)
                    {
                        oUserControls_RiskRatingDropDown.SelectedValue = oAccountHdrInfo.RiskRatingID.Value.ToString();
                        lblRiskRatingExport.Text = oUserControls_RiskRatingDropDown.SelectedItem.Text;
                        oUserControls_PopupRecFrequency.RiskRatingRecPeriodID = oAccountHdrInfo.RiskRatingID.Value;
                    }
                    else
                    {
                        hlRecFrequency.Attributes.CssStyle.Add("visibility", "hidden");
                        lblRiskRatingExport.Text = "-";
                    }

                    //if (SessionHelper.CurrentRecProcessStatusEnum == WebEnums.RecPeriodStatus.Closed
                    //|| SessionHelper.CurrentRecProcessStatusEnum == WebEnums.RecPeriodStatus.InProgress
                    //|| SessionHelper.CurrentRecProcessStatusEnum == WebEnums.RecPeriodStatus.Skipped)
                    //{
                    this.AddToStringBuilder(oSBhlkControls, "hlRecFrequency=" + hlRecFrequency.ClientID);
                    this.AddToStringBuilder(oSBddlControls, "ddlRiskRating=" + ddlRiskratiingtoStForHyperLink.ClientID);
                    this.AddToStringBuilder(oSBvldControls, "vldRiskRating=" + oUserControls_RiskRatingDropDown.ReqFldValidator.ClientID);

                    oUserControls_RiskRatingDropDown.DropDown.Attributes.Add("disabled", "disabled");
                    ///}
                }
                else
                {
                    UserControls_PopupRecFrequencySelection ucRecFrequencySelection = (UserControls_PopupRecFrequencySelection)e.Item.FindControl("ucRecFrequencySelection");
                    HtmlInputText oRecPeriodContainer = (HtmlInputText)e.Item.FindControl("txtRecPeriodsContainer");

                    if (string.IsNullOrEmpty(oRecPeriodContainer.Value))
                    {
                        string[] recPeriodNumbers = (from accRecPeriod in this._AccountReconciliationPeriodInfoCollection
                                                     where accRecPeriod.AccountID == oAccountHdrInfo.AccountID
                                                     select accRecPeriod.ReconciliationPeriodID.ToString() + ";").ToArray();
                        Array.ForEach(recPeriodNumbers, rec => oRecPeriodContainer.Value += rec);
                    }
                    string url = Helper.GetUrlForRecFrequency(oAccountHdrInfo.AccountID, oRecPeriodContainer.ClientID, oRecPeriodContainer.Value, WebEnums.FormMode.ReadOnly);
                    ucRecFrequencySelection.URL = url;

                    this.AddToStringBuilder(oSBhlkControls, "hlRecFrequencySelection=" + ucRecFrequencySelection.HyperLink.ClientID);
                }

                //Client Select Column
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
                    }
                }

                //Subledger Source
                UserControls_SubledgerSourceDropDown oUserControls_SubledgerSourceDropDown = (UserControls_SubledgerSourceDropDown)e.Item.FindControl("ddlSubledger");
                ExLabel lblSubledgerExport = (ExLabel)e.Item.FindControl("lblSubledgerExport");
                if (lblSubledgerExport != null)
                {
                    if (oAccountHdrInfo.SubLedgerSourceID.HasValue && oAccountHdrInfo.SubLedgerSourceID.Value > 0)
                    {
                        oUserControls_SubledgerSourceDropDown.SelectedValue = oAccountHdrInfo.SubLedgerSourceID.Value.ToString();
                        lblSubledgerExport.Text = oUserControls_SubledgerSourceDropDown.SelectedItem.Text;
                    }
                    else
                    {
                        lblSubledgerExport.Text = "-";
                    }
                    this.AddToStringBuilder(oSBddlControls, "ddlSubLedgerSource=" + oUserControls_SubledgerSourceDropDown.DropDown.ClientID);
                    this.AddToStringBuilder(oSBvldControls, "vldSubLedgerSource=" + oUserControls_SubledgerSourceDropDown.ReqFldValidator.ClientID);
                    oUserControls_SubledgerSourceDropDown.DropDown.Attributes.Add("disabled", "disabled");

                }

                UserControls_ReconciliationTemplateDropDown oUserControls_ReconciliationTemplateDropDown = (UserControls_ReconciliationTemplateDropDown)e.Item.FindControl("ddlReconciliationTemplate");
                if (oUserControls_ReconciliationTemplateDropDown != null)
                {
                    ExLabel lblReconciliationTemplateExport = (ExLabel)e.Item.FindControl("lblReconciliationTemplateExport");
                    string onSelectedIndexChanged = "return OnSelectedIndexChange('" + oUserControls_ReconciliationTemplateDropDown.ClientID + "','" + oUserControls_SubledgerSourceDropDown.ValidatorClientID + "','" + checkBox.ClientID + "');";
                    oUserControls_ReconciliationTemplateDropDown.AddAttributes = new KeyValuePair<string, string>("onchange", onSelectedIndexChanged);

                    if (oAccountHdrInfo.ReconciliationTemplateID.HasValue && oAccountHdrInfo.ReconciliationTemplateID.Value > 0)
                    {
                        oUserControls_ReconciliationTemplateDropDown.SelectedValue = oAccountHdrInfo.ReconciliationTemplateID.Value.ToString();
                        lblReconciliationTemplateExport.Text = oUserControls_ReconciliationTemplateDropDown.SelectedItem.Text;
                    }
                    else
                    {
                        lblReconciliationTemplateExport.Text = "-";
                    }
                    this.AddToStringBuilder(oSBddlControls, "ddlRecTemplate=" + oUserControls_ReconciliationTemplateDropDown.DropDown.ClientID);
                    this.AddToStringBuilder(oSBvldControls, "vldRecTemplate=" + oUserControls_ReconciliationTemplateDropDown.ReqFldValidator.ClientID);
                    oUserControls_ReconciliationTemplateDropDown.DropDown.Attributes.Add("disabled", "disabled");
                }

                //Preparer Due Days
                TextBox txtPreparerDueDays = (TextBox)e.Item.FindControl("txtPreparerDueDays");
                ExLabel lblPreparerDueDaysExport = (ExLabel)e.Item.FindControl("lblPreparerDueDaysExport");
                RequiredFieldValidator rfvPreparerDueDays = (RequiredFieldValidator)e.Item.FindControl("rfvPreparerDueDays");
                CustomValidator cvPreparerDueDays = (CustomValidator)e.Item.FindControl("cvPreparerDueDays");
                this.AddToStringBuilder(oSBtxtDueDaysControls, "txtPreparerDueDays=" + txtPreparerDueDays.ClientID);
                this.AddToStringBuilder(oSBvldControls, "rfvPreparerDueDays=" + rfvPreparerDueDays.ClientID);
                this.AddToStringBuilder(oSBvldControls, "cvPreparerDueDays=" + cvPreparerDueDays.ClientID);
                txtPreparerDueDays.Text = Helper.GetDisplayIntegerValueForTextBox(oAccountHdrInfo.PreparerDueDays);
                txtPreparerDueDays.Attributes.Add("disabled", "disabled");//Disable textbox at client side
                rfvPreparerDueDays.ErrorMessage = Helper.GetErrorMessage(WebEnums.FieldType.MandatoryField, 2752);
                cvPreparerDueDays.ErrorMessage = LanguageUtil.GetValue(5000377);
                lblPreparerDueDaysExport.Text = Helper.GetDisplayIntegerValue(oAccountHdrInfo.PreparerDueDays); 

                //Reviewer Due Days
                TextBox txtReviewerDueDays = (TextBox)e.Item.FindControl("txtReviewerDueDays");
                ExLabel lblReviewerDueDaysExport = (ExLabel)e.Item.FindControl("lblReviewerDueDaysExport");
                RequiredFieldValidator rfvReviewerDueDays = (RequiredFieldValidator)e.Item.FindControl("rfvReviewerDueDays");
                CustomValidator cvReviewerDueDays = (CustomValidator)e.Item.FindControl("cvReviewerDueDays");
                this.AddToStringBuilder(oSBtxtDueDaysControls, "txtReviewerDueDays=" + txtReviewerDueDays.ClientID);
                this.AddToStringBuilder(oSBvldControls, "rfvReviewerDueDays=" + rfvReviewerDueDays.ClientID);
                this.AddToStringBuilder(oSBvldControls, "cvReviewerDueDays=" + cvReviewerDueDays.ClientID);
                txtReviewerDueDays.Text = Helper.GetDisplayIntegerValueForTextBox(oAccountHdrInfo.ReviewerDueDays);
                txtReviewerDueDays.Attributes.Add("disabled", "disabled");//Disable textbox at client side
                rfvReviewerDueDays.ErrorMessage = Helper.GetErrorMessage(WebEnums.FieldType.MandatoryField, 2753);
                cvReviewerDueDays.ErrorMessage = LanguageUtil.GetValue(5000377);
                lblReviewerDueDaysExport.Text = Helper.GetDisplayIntegerValue(oAccountHdrInfo.ReviewerDueDays);

                //Approver Due Days
                TextBox txtApproverDueDays = (TextBox)e.Item.FindControl("txtApproverDueDays");
                ExLabel lblApproverDueDaysExport = (ExLabel)e.Item.FindControl("lblApproverDueDaysExport");
                RequiredFieldValidator rfvApproverDueDays = (RequiredFieldValidator)e.Item.FindControl("rfvApproverDueDays");
                CustomValidator cvApproverDueDays = (CustomValidator)e.Item.FindControl("cvApproverDueDays");
                this.AddToStringBuilder(oSBtxtDueDaysControls, "txtApproverDueDays=" + txtApproverDueDays.ClientID);
                this.AddToStringBuilder(oSBvldControls, "rfvApproverDueDays=" + rfvApproverDueDays.ClientID);
                this.AddToStringBuilder(oSBvldControls, "cvApproverDueDays=" + cvApproverDueDays.ClientID);
                txtApproverDueDays.Text = Helper.GetDisplayIntegerValueForTextBox(oAccountHdrInfo.ApproverDueDays);
                lblApproverDueDaysExport.Text = Helper.GetDisplayIntegerValue(oAccountHdrInfo.ApproverDueDays);
                txtApproverDueDays.Attributes.Add("disabled", "disabled");//Disable textbox at client side
                if (Helper.IsDualLevelReviewByAccountActivated())
                {
                    txtApproverDueDays.Attributes.Add("IsOptional", "1");
                    rfvApproverDueDays.Attributes.Add("IsOptional", "1");
                }
                rfvApproverDueDays.ErrorMessage = Helper.GetErrorMessage(WebEnums.FieldType.MandatoryField, 2754);
                cvApproverDueDays.ErrorMessage = LanguageUtil.GetValue(5000377);

                //UserControls_DayTypeDropDown oUserControls_DayTypeDropDown = (UserControls_DayTypeDropDown)e.Item.FindControl("ddlDayType");
                //if (oUserControls_DayTypeDropDown != null)
                //{
                //    ExLabel lblDayTypeExport = (ExLabel)e.Item.FindControl("lblDayTypeExport");
                //    if (oAccountHdrInfo.DayTypeID.HasValue && oAccountHdrInfo.DayTypeID.Value > 0)
                //    {
                //        oUserControls_DayTypeDropDown.SelectedValue = oAccountHdrInfo.DayTypeID.Value.ToString();
                //        lblDayTypeExport.Text = oUserControls_DayTypeDropDown.SelectedItem.Text;
                //    }
                //    else
                //    {
                //        lblDayTypeExport.Text = "-";
                //    }
                //    this.AddToStringBuilder(oSBddlControls, "ddlDayType=" + oUserControls_DayTypeDropDown.DropDown.ClientID);
                //    this.AddToStringBuilder(oSBvldControls, "vldDayType=" + oUserControls_DayTypeDropDown.ReqFldValidator.ClientID);
                //    oUserControls_DayTypeDropDown.DropDown.Attributes.Add("disabled", "disabled");
                //}
                e.Item.Attributes.Add("hlkControls", oSBhlkControls.ToString());//hyperlink controls
                e.Item.Attributes.Add("chkControls", oSBchkControls.ToString());//checkbox controls
                e.Item.Attributes.Add("ddlControls", oSBddlControls.ToString());//dropdownlist controls
                e.Item.Attributes.Add("txtDueDaysControls", oSBtxtDueDaysControls.ToString());//Textbox Due Date controls
                e.Item.Attributes.Add("hdnControls", oSBhdnControls.ToString());//Hidden controls
                e.Item.Attributes.Add("validatorControls", oSBvldControls.ToString());//Validation controls
                cvPreparerDueDays.Attributes.Add("txtDueDaysControls", oSBtxtDueDaysControls.ToString());//Due Days controls
                cvReviewerDueDays.Attributes.Add("txtDueDaysControls", oSBtxtDueDaysControls.ToString());//Due Days controls
                cvApproverDueDays.Attributes.Add("txtDueDaysControls", oSBtxtDueDaysControls.ToString());//Due Days controls

                //}
                //else if (SessionHelper.CurrentRecProcessStatusEnum == WebEnums.RecPeriodStatus.Open)
                //{

                //}
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

    protected object ucSkyStemARTGridBulkUpdate_Grid_NeedDataSourceEventHandler(int count)
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


    #endregion

    #region "Mass Update "
    /// <summary>
    /// Handles rad grids item data bound event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    void ucSkyStemARTGridMassUpdate_GridItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {
        try
        {

            if (e.Item.ItemType == Telerik.Web.UI.GridItemType.Item || e.Item.ItemType == Telerik.Web.UI.GridItemType.AlternatingItem)
            {
                AccountHdrInfo oAccountHdrInfo = (AccountHdrInfo)e.Item.DataItem;
                IAccount oAccountClient = RemotingHelper.GetAccountObject();
                this._AccountReconciliationPeriodInfoCollection = oAccountClient.SelectAccountRecPeriodByAccountID(Convert.ToInt32(oAccountHdrInfo.AccountID.Value), Helper.GetAppUserInfo());

                ExLabel lblAccountNumber = (ExLabel)e.Item.FindControl("lblAccountNumber");
                lblAccountNumber.Text = Helper.GetDisplayStringValue(oAccountHdrInfo.AccountNumber);

                ExLabel lblAccountName = (ExLabel)e.Item.FindControl("lblAccountName");
                lblAccountName.Text = Helper.GetDisplayStringValue(oAccountHdrInfo.AccountName);

                ExLabel lblNetAccount = (ExLabel)e.Item.FindControl("lblNetAccount");
                if (!string.IsNullOrEmpty(oAccountHdrInfo.NetAccount))
                {
                    lblNetAccount.Text = Helper.GetDisplayStringValue(oAccountHdrInfo.NetAccount);
                }
                else
                {
                    lblNetAccount.Text = "-";
                }

                if (this._IsKeyAccountEnabled)
                {
                    ExLabel lblKeyAccount = (ExLabel)e.Item.FindControl("lblKeyAccount");
                    lblKeyAccount.Text = Helper.GetDisplayStringValue(oAccountHdrInfo.KeyAccount);
                }

                if (this._IsZeroBalanceEnabled)
                {
                    ExLabel lblZeroBalanceAccount = (ExLabel)e.Item.FindControl("lblZeroBalanceAccount");
                    lblZeroBalanceAccount.Text = Helper.GetDisplayStringValue(oAccountHdrInfo.ZeroBalance);
                }
                if (this._IsReconcilableEnabled)
                {
                    ExLabel lblIsReconcilable = (ExLabel)e.Item.FindControl("lblIsReconcilable");
                    lblIsReconcilable.Text = Helper.GetDisplayStringValue(oAccountHdrInfo.Reconcilable);
                }
                if (this._IsRiskRatingEnabled)
                {
                    ExLabel lblRiskRating = (ExLabel)e.Item.FindControl("lblRiskRating");
                    lblRiskRating.Text = Helper.GetDisplayStringValue(oAccountHdrInfo.RiskRating);
                }
                else
                {
                    UserControls_PopupRecFrequency ucPopupRecFrequency = (UserControls_PopupRecFrequency)e.Item.FindControl("ucPopupRecFrequency");
                    ucPopupRecFrequency.AccountID = oAccountHdrInfo.AccountID.Value;
                }

                ExLabel lblReconciliationTemplate = (ExLabel)e.Item.FindControl("lblReconciliationTemplate");
                lblReconciliationTemplate.Text = Helper.GetDisplayStringValue(oAccountHdrInfo.ReconciliationTemplate);

                ExLabel lblPreparerDueDays = (ExLabel)e.Item.FindControl("lblPreparerDueDays");
                lblPreparerDueDays.Text = Helper.GetDisplayIntegerValue(oAccountHdrInfo.PreparerDueDays);

                ExLabel lblReviewerDueDays = (ExLabel)e.Item.FindControl("lblReviewerDueDays");
                lblReviewerDueDays.Text = Helper.GetDisplayIntegerValue(oAccountHdrInfo.ReviewerDueDays);

                ExLabel lblApproverDueDays = (ExLabel)e.Item.FindControl("lblApproverDueDays");
                lblApproverDueDays.Text = Helper.GetDisplayIntegerValue(oAccountHdrInfo.ApproverDueDays);

                //ExLabel lblDayType = (ExLabel)e.Item.FindControl("lblDayType");
                //lblDayType.Text = Helper.GetDisplayStringValue(oAccountHdrInfo.DayType);

                if ((e.Item as GridDataItem)[COLUMN_NAME_ID] != null)
                {
                    (e.Item as GridDataItem)[COLUMN_NAME_ID].Text = oAccountHdrInfo.AccountID.ToString();
                }

                if ((e.Item as GridDataItem)[COLUMN_NAME_NETACCOUNTID] != null)
                {
                    (e.Item as GridDataItem)[COLUMN_NAME_NETACCOUNTID].Text = oAccountHdrInfo.NetAccountID.ToString();
                }
                //Client Select Column
                CheckBox checkBox = (CheckBox)(e.Item as GridDataItem)["CheckboxSelectColumn"].Controls[0];
                if (!oAccountHdrInfo.IsLocked.GetValueOrDefault()
                    && !IsLockedDueToDueDaysAttributes(oAccountHdrInfo)
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
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Helper.ShowErrorMessage(this, ex);
        }
    }


    protected object ucSkyStemARTGridMassUpdate_Grid_NeedDataSourceEventHandler(int count)
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


    #endregion
    #endregion

    #region Other Events
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
    /// Handles save button click event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                bool result = false;
                IAccount oAccountClient = RemotingHelper.GetAccountObject();
                List<AccountAttributeWarningInfo> oAccountIDNetAccountIDCollection = new List<AccountAttributeWarningInfo>();
                List<AccountHdrInfo> oAccountHdrInfoCollectionNew = new List<AccountHdrInfo>();
                List<int> oNetAccountIDCollection = new List<int>();
                List<long> oAccountIDCollection = new List<long>();

                if (this._IsMassUpdateClicked)
                {
                    foreach (GridDataItem item in ucSkyStemARTGridMassUpdate.Grid.SelectedItems)
                    {

                        string accountId = item[COLUMN_NAME_ID].Text;
                        string netAccountId = item[COLUMN_NAME_NETACCOUNTID].Text;
                        AccountAttributeWarningInfo oAccountAttributeWarningInfo = new AccountAttributeWarningInfo();
                        AccountHdrInfo oAccountHdrInfo = new AccountHdrInfo();
                        oAccountAttributeWarningInfo.AccountID = Convert.ToInt64(accountId);
                        oAccountHdrInfo.AccountID = Convert.ToInt64(accountId);
                        if (!string.IsNullOrEmpty(netAccountId) && Convert.ToInt32(netAccountId) > 0)
                        {
                            oNetAccountIDCollection.Add(Convert.ToInt32(netAccountId));
                            oAccountAttributeWarningInfo.NetAccountID = Convert.ToInt32(netAccountId);
                        }
                        oAccountIDCollection.Add(Convert.ToInt64(accountId));
                        oAccountIDNetAccountIDCollection.Add(oAccountAttributeWarningInfo);
                        oAccountHdrInfoCollectionNew.Add(oAccountHdrInfo);
                    }

                    //Validate input for Net Accounts
                    ValidateNetAccountsForConstituentAccountCount(oNetAccountIDCollection, oAccountIDCollection);

                    if (ddlAccountAttribute.SelectedValue != "0")
                    {
                        // Get Selected Account Attribute value
                        ARTEnums.AccountAttribute oAccountAttribute = (ARTEnums.AccountAttribute)Enum.Parse(typeof(ARTEnums.AccountAttribute), ddlAccountAttribute.SelectedValue);
                        string AttributeValue = GetAttributeValue(oAccountAttribute, oAccountHdrInfoCollectionNew);
                        //Validate selected attribute for their value
                        ValidateAttributeValue(oAccountAttribute);
                        // Save values 
                        ValidateIsWarningOccur(oAccountIDNetAccountIDCollection, oAccountHdrInfoCollectionNew);

                        if (hdnConfirm.Value == "Yes")
                            result = oAccountClient.SaveAccountMassUpdate(oAccountIDCollection, SessionHelper.CurrentCompanyID.Value,
                                SessionHelper.CurrentReconciliationPeriodID.Value, oAccountAttribute, AttributeValue,

                                SessionHelper.GetCurrentUser().LoginID, DateTime.Now, (short)ARTEnums.ActionType.AccountAttributeChangeFromUI, Helper.GetAppUserInfo());


                    }
                    else //Selected value is Rec Frequency
                    {
                        result = ValidateAndSaveRecFrequency(oAccountIDCollection, oAccountIDNetAccountIDCollection);
                    }


                    if (hdnConfirm.Value == "Yes")
                    {
                        List<AccountHdrInfo> oAccountHdrInfoCollection = (List<AccountHdrInfo>)HttpContext.Current.Session[SessionConstants.SEARCH_RESULTS_ACCOUNT];
                        List<AccountHdrInfo> oSelectedAccountHdrInfoList = (from oAccountHdrInfo in oAccountHdrInfoCollection
                                                                            join oAccountID in oAccountIDCollection on oAccountHdrInfo.AccountID equals oAccountID
                                                                            select oAccountHdrInfo).ToList();


                        AlertHelper.RaiseAlert(WebEnums.Alert.YouHaveXAccountsWhichHaveAttributesThatHaveChanged,
                            SessionHelper.CurrentReconciliationPeriodID, SessionHelper.CurrentReconciliationPeriodEndDate, oAccountIDCollection,
                            null, SessionHelper.CurrentRoleID.Value, oSelectedAccountHdrInfoList);

                        //Fire search again for mass update.
                        this.ucAccountSearchControl.btnSearchMassUpdate_Click(null, null);

                        //showing MassUpdateGrid.
                        HidePanels(this._IsMassUpdateClicked);
                    }
                }
                else
                {
                    foreach (GridDataItem item in ucSkyStemARTGridBulkUpdate.Grid.SelectedItems)
                    {
                        string accountId = item[COLUMN_NAME_ID].Text;
                        string netAccountId = item[COLUMN_NAME_NETACCOUNTID].Text;
                        AccountAttributeWarningInfo oAccountAttributeWarningInfo = new AccountAttributeWarningInfo();
                        oAccountAttributeWarningInfo.AccountID = Convert.ToInt64(accountId);
                        if (!string.IsNullOrEmpty(netAccountId) && Convert.ToInt32(netAccountId) > 0)
                        {
                            oNetAccountIDCollection.Add(Convert.ToInt32(netAccountId));
                            oAccountAttributeWarningInfo.NetAccountID = Convert.ToInt32(netAccountId);
                        }
                        oAccountIDCollection.Add(Convert.ToInt64(accountId));
                        oAccountIDNetAccountIDCollection.Add(oAccountAttributeWarningInfo);
                    }

                    //Validate input for Net Accounts
                    ValidateNetAccountsForConstituentAccountCount(oNetAccountIDCollection, oAccountIDCollection);

                    List<AccountHdrInfo> oAccountHdrInfoCollection = this.GetSelectedAccountInformation();
                    //Validate input for Net Accounts
                    ValidateNetAccountsForBulkUpdate(oNetAccountIDCollection, oAccountHdrInfoCollection);

                    ValidateIsWarningOccur(oAccountIDNetAccountIDCollection, oAccountHdrInfoCollection);
                    if (hdnConfirm.Value == "Yes")
                    {
                        result = oAccountClient.SaveAccountProfile(oAccountHdrInfoCollection, SessionHelper.CurrentCompanyID.Value, SessionHelper.CurrentReconciliationPeriodID.Value, SessionHelper.GetCurrentUser().LoginID, DateTime.Now, (short)ARTEnums.ActionType.AccountAttributeChangeFromUI, Helper.GetAppUserInfo());


                        //List<long> oAccountIDCollection = (from acc in oAccountHdrInfoCollection select acc.AccountID.Value).ToList();
                        AlertHelper.RaiseAlert(WebEnums.Alert.YouHaveXAccountsWhichHaveAttributesThatHaveChanged,
                            SessionHelper.CurrentReconciliationPeriodID, SessionHelper.CurrentReconciliationPeriodEndDate,
                            oAccountIDCollection, null, SessionHelper.CurrentRoleID.Value, oAccountHdrInfoCollection);
                        //Show BulkUpdateGrid.
                        HidePanels();
                    }
                }
                //HidePanels();
                if (hdnConfirm.Value == "Yes")
                {
                    MasterPageBase oMasterPageBase = (MasterPageBase)this.Master;
                    oMasterPageBase.ShowConfirmationMessage(1597);
                    hdnConfirm.Value = "";
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

    protected void ddlAccountAttribute_SelectedIndexChangedHandler(object sender, EventArgs e)
    {
        if (ddlAccountAttribute.SelectedValue != "0")
        {
            ARTEnums.AccountAttribute oAccountAttribute = (ARTEnums.AccountAttribute)Enum.Parse(typeof(ARTEnums.AccountAttribute), ddlAccountAttribute.SelectedValue);
            switch (oAccountAttribute)
            {
                case ARTEnums.AccountAttribute.IsKeyAccount:
                case ARTEnums.AccountAttribute.IsZeroBalanceAccount:
                case ARTEnums.AccountAttribute.IsReconcilable:
                    optIsNo.Visible = true;
                    optIsYes.Visible = true;
                    ddlRiskRating.Visible = false;
                    ucRecFrequencySelectionMass.Visible = false;
                    ucRiskRatingPeriod.Visible = false;
                    txtDueDays.Visible = false;
                    //ddlDayType.Visible = false;
                    Page.SetFocus(optIsNo);
                    break;

                case ARTEnums.AccountAttribute.RiskRating:
                    ddlRiskRating.Visible = true;
                    ucRiskRatingPeriod.Visible = true;
                    optIsNo.Visible = false;
                    optIsYes.Visible = false;
                    ucRecFrequencySelectionMass.Visible = false;
                    txtDueDays.Visible = false;
                    //ddlDayType.Visible = false;
                    Page.SetFocus(ddlRiskRating);
                    break;
                case ARTEnums.AccountAttribute.PreparerDueDays:
                case ARTEnums.AccountAttribute.ReviewerDueDays:
                case ARTEnums.AccountAttribute.ApproverDueDays:
                    txtDueDays.Visible = true;
                    ddlRiskRating.Visible = false;
                    ucRiskRatingPeriod.Visible = false;
                    optIsNo.Visible = false;
                    optIsYes.Visible = false;
                    ucRecFrequencySelectionMass.Visible = false;
                    //ddlDayType.Visible = false;
                    Page.SetFocus(ddlRiskRating);
                    break;
                //case ARTEnums.AccountAttribute.DayType:
                //    ddlDayType.Visible = true;
                //    ddlRiskRating.Visible = false;
                //    ucRiskRatingPeriod.Visible = false;
                //    optIsNo.Visible = false;
                //    optIsYes.Visible = false;
                //    ucRecFrequencySelectionMass.Visible = false;
                //    txtDueDays.Visible = false;
                //    Page.SetFocus(ddlDayType);
                //    break;
                default:
                    ddlRiskRating.Visible = false;
                    ucRiskRatingPeriod.Visible = false;
                    optIsNo.Visible = false;
                    optIsYes.Visible = false;
                    ucRecFrequencySelectionMass.Visible = false;
                    txtDueDays.Visible = false;
                    //ddlDayType.Visible = false;
                    Page.SetFocus(ddlAccountAttribute);
                    break;
            }
        }
        else
        {
            ddlRiskRating.Visible = false;
            ucRiskRatingPeriod.Visible = false;
            optIsNo.Visible = false;
            optIsYes.Visible = false;
            txtDueDays.Visible = false;
            ucRecFrequencySelectionMass.Visible = true;
            //ddlDayType.Visible = false;
            Page.SetFocus(ddlAccountAttribute);
        }

        optIsNo.Checked = false;
        optIsYes.Checked = false;
        ddlRiskRating.SelectedValue = WebConstants.SELECT_ONE;
        //ddlDayType.SelectedValue = WebConstants.SELECT_ONE;
        txtDueDays.Text = "";
    }

    public void ucAccountSearchControl_MassUpdateClickEventHandler(List<AccountHdrInfo> oAccountHdrInfoCollection)
    {
        bool isCertificationStarted = CertificationHelper.IsCertificationStarted();
        pnlMassUpdate.Visible = true;
        pnlBulkUpdate.Visible = false;
        MasterPageBase oMasterPageBase = (MasterPageBase)this.Master;
        oMasterPageBase.HideMessage();

        ucSkyStemARTGridMassUpdate.CompanyID = SessionHelper.GetCurrentUser().CompanyID;
        ucSkyStemARTGridMassUpdate.DataSource = oAccountHdrInfoCollection;
        if (CurrentRecProcessStatus.Value == WebEnums.RecPeriodStatus.Closed
            || CurrentRecProcessStatus.Value == WebEnums.RecPeriodStatus.Skipped
            || isCertificationStarted)
        {
            ucSkyStemARTGridMassUpdate.ShowSelectCheckBoxColum = false;
        }
        else
        {
            ucSkyStemARTGridMassUpdate.ShowSelectCheckBoxColum = true;
        }

        //ucSkyStemARTGridMassUpdate.Grid.Columns[21].Visible = true;
        // will need in actual scenerio
        GridColumn oGridColumn = ucSkyStemARTGridMassUpdate.Grid.Columns.FindByUniqueNameSafe("ZeroBalance");
        if (!this._IsZeroBalanceEnabled)
        {
            if (oGridColumn != null)
            {
                oGridColumn.Visible = false;
            }
        }
        else
        {
            if (oGridColumn != null)
            {
                oGridColumn.Visible = true;
            }
        }
        //ucSkyStemARTGridMassUpdate.Grid.Columns[17].Visible = false;
        GridColumn oGridColumnKeyAccount = ucSkyStemARTGridMassUpdate.Grid.Columns.FindByUniqueNameSafe("KeyAccount");
        if (!this._IsKeyAccountEnabled)
        {
            if (oGridColumnKeyAccount != null)
            {
                oGridColumnKeyAccount.Visible = false;
            }
        }
        else
        {
            if (oGridColumnKeyAccount != null)
            {
                oGridColumnKeyAccount.Visible = true;
            }
        }
        //ucSkyStemARTGridMassUpdate.Grid.Columns[18].Visible = false;
        GridColumn oGridColumnRecFrequency = ucSkyStemARTGridMassUpdate.Grid.Columns.FindByUniqueNameSafe("RecFrequency");
        GridColumn oGridColumnRiskRating = ucSkyStemARTGridMassUpdate.Grid.Columns.FindByUniqueNameSafe("RiskRating");
        if (this._IsRiskRatingEnabled)
        {
            if (oGridColumnRecFrequency != null)
            {
                oGridColumnRecFrequency.Visible = false;
            }
            if (oGridColumnRiskRating != null)
            {
                oGridColumnRiskRating.Visible = true;
            }
        }
        //   ucSkyStemARTGridMassUpdate.Grid.Columns[21].Visible = false;
        else
        {
            if (oGridColumnRecFrequency != null)
            {
                oGridColumnRecFrequency.Visible = true;
            }
            if (oGridColumnRiskRating != null)
            {
                oGridColumnRiskRating.Visible = false;
            }
        }
        //   ucSkyStemARTGridMassUpdate.Grid.Columns[19].Visible = false;


        //******** Show/Hide Net Account Column based on the capability : By  Prafull on 03-Mar-2011
        GridColumn oGridColumnNetAccount = ucSkyStemARTGridMassUpdate.Grid.Columns.FindByUniqueNameSafe("NetAccount");
        if (oGridColumnNetAccount != null)
        {
            if (this._IsNetAccountEnabled)
            {
                oGridColumnNetAccount.Visible = true;
            }
            else
            {
                oGridColumnNetAccount.Visible = false;
            }
        }

        GridColumn oGridColumnPreparerDueDays = ucSkyStemARTGridMassUpdate.Grid.Columns.FindByUniqueNameSafe("PreparerDueDays");
        if (oGridColumnPreparerDueDays != null)
        {
            oGridColumnPreparerDueDays.Visible = this._IsDueDateByAccountEnabled;
        }
        GridColumn oGridColumnReviewerDueDays = ucSkyStemARTGridMassUpdate.Grid.Columns.FindByUniqueNameSafe("ReviewerDueDays");
        if (oGridColumnReviewerDueDays != null)
        {
            oGridColumnReviewerDueDays.Visible = this._IsDueDateByAccountEnabled;
        }
        GridColumn oGridColumnApproverDueDays = ucSkyStemARTGridMassUpdate.Grid.Columns.FindByUniqueNameSafe("ApproverDueDays");
        if (oGridColumnApproverDueDays != null)
        {
            oGridColumnApproverDueDays.Visible = this._IsDueDateByAccountEnabled && this._IsDualLevelReviewEnabled;
        }
        GridColumn oGridColumnPreparerDueDaysExport = ucSkyStemARTGridMassUpdate.Grid.Columns.FindByUniqueNameSafe("PreparerDueDaysExport");
        if (oGridColumnPreparerDueDaysExport != null)
        {
            oGridColumnPreparerDueDaysExport.Visible = this._IsDueDateByAccountEnabled;
        }
        GridColumn oGridColumnReviewerDueDaysExport = ucSkyStemARTGridMassUpdate.Grid.Columns.FindByUniqueNameSafe("ReviewerDueDaysExport");
        if (oGridColumnReviewerDueDaysExport != null)
        {
            oGridColumnReviewerDueDaysExport.Visible = this._IsDueDateByAccountEnabled;
        }
        GridColumn oGridColumnApproverDueDaysExport = ucSkyStemARTGridMassUpdate.Grid.Columns.FindByUniqueNameSafe("ApproverDueDaysExport");
        if (oGridColumnApproverDueDaysExport != null)
        {
            oGridColumnApproverDueDaysExport.Visible = this._IsDueDateByAccountEnabled && this._IsDualLevelReviewEnabled;
        }

        //GridColumn oGridColumnDayType = ucSkyStemARTGridMassUpdate.Grid.Columns.FindByUniqueNameSafe("DayType");
        //if (oGridColumnDayType != null)
        //{
        //    oGridColumnDayType.Visible = this._IsDueDateByAccountEnabled && this._IsDualLevelReviewEnabled;
        //}

        ucSkyStemARTGridMassUpdate.BindGrid();
        ucSkyStemARTGridMassUpdate.DataBind();

        btnCancel.Visible = true;

        if (CurrentRecProcessStatus.Value == WebEnums.RecPeriodStatus.Closed
            || CurrentRecProcessStatus.Value == WebEnums.RecPeriodStatus.Skipped
            || isCertificationStarted
            || oAccountHdrInfoCollection == null || oAccountHdrInfoCollection.Count == 0)
        {
            btnSave.Visible = false;
            ddlAccountAttribute.Visible = false;
            lblAttribute.Visible = false;
        }
        else
        {
            btnSave.Visible = true;
            ddlAccountAttribute.Visible = true;
            lblAttribute.Visible = true;
        }

        lblGridTitle.LabelID = 1648;
        ddlAccountAttribute.SelectedValue = WebConstants.SELECT_ONE;
        this.ddlAccountAttribute_SelectedIndexChangedHandler(null, null);
    }

    void ucAccountSearchControl_SearchAndMailClickEventHandler(AccountSearchCriteria oAccountSearchCriteria)
    {
        try
        {
            MasterPageBase oMasterPageBase = (MasterPageBase)this.Master;
            RequestHelper objRequestHelper = new RequestHelper();
            oAccountSearchCriteria.PageID = (int)WebEnums.AccountPages.AccountMassUpdate;
            int searchResultCount = objRequestHelper.StartSaveBulkExport(ucSkyStemARTGridBulkUpdate.Grid, ARTEnums.Grid.AccountProfileMassAndBulkUpdate, false, oAccountSearchCriteria);
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
    /// Handles user controls Bulk click event
    /// </summary>
    /// <param name="oAccountHdrInfoCollection">List of accounts</param>
    public void ucAccountSearchControl_BulkUpdateClickEventHandler(List<AccountHdrInfo> oAccountHdrInfoCollection)
    {
        try
        {
            bool isCertificationStarted = CertificationHelper.IsCertificationStarted();
            pnlMassUpdate.Visible = false;
            pnlBulkUpdate.Visible = true;
            MasterPageBase oMasterPageBase = (MasterPageBase)this.Master;
            oMasterPageBase.HideMessage();

            GridColumn oGridColumn = ucSkyStemARTGridBulkUpdate.Grid.Columns.FindByUniqueNameSafe("ZeroBalance");
            if (!this._IsZeroBalanceEnabled)
            {
                if (oGridColumn != null)
                {
                    oGridColumn.Visible = false;
                }
            }
            else
            {
                if (oGridColumn != null)
                {
                    oGridColumn.Visible = true;
                }
            }
            //ucSkyStemARTGridMassUpdate.Grid.Columns[17].Visible = false;
            GridColumn oGridColumnKeyAccount = ucSkyStemARTGridBulkUpdate.Grid.Columns.FindByUniqueNameSafe("KeyAccount");
            if (!this._IsKeyAccountEnabled)
            {
                if (oGridColumnKeyAccount != null)
                {
                    oGridColumnKeyAccount.Visible = false;
                }
            }
            else
            {
                if (oGridColumnKeyAccount != null)
                {
                    oGridColumnKeyAccount.Visible = true;
                }
            }
            //ucSkyStemARTGridMassUpdate.Grid.Columns[18].Visible = false;
            GridColumn oGridColumnRecFrequency = ucSkyStemARTGridBulkUpdate.Grid.Columns.FindByUniqueNameSafe("RecFrequency");
            GridColumn oGridColumnRiskRating = ucSkyStemARTGridBulkUpdate.Grid.Columns.FindByUniqueNameSafe("RiskRating");
            if (this._IsRiskRatingEnabled)
            {
                if (oGridColumnRecFrequency != null)
                {
                    oGridColumnRecFrequency.Visible = false;
                }
                if (oGridColumnRiskRating != null)
                {
                    oGridColumnRiskRating.Visible = true;
                }
            }
            //   ucSkyStemARTGridMassUpdate.Grid.Columns[21].Visible = false;
            else
            {
                if (oGridColumnRecFrequency != null)
                {
                    oGridColumnRecFrequency.Visible = true;
                }
                if (oGridColumnRiskRating != null)
                {
                    oGridColumnRiskRating.Visible = false;
                }
            }
            //******** Show/Hide Net Account Column based on the capability : By  Prafull on 03-Mar-2011
            GridColumn oGridColumnNetAccount = ucSkyStemARTGridBulkUpdate.Grid.Columns.FindByUniqueNameSafe("NetAccount");
            if (oGridColumnNetAccount != null)
            {
                if (this._IsNetAccountEnabled)
                {
                    oGridColumnNetAccount.Visible = true;
                }
                else
                {
                    oGridColumnNetAccount.Visible = false;
                }
            }

            GridColumn oGridColumnPreparerDueDays = ucSkyStemARTGridBulkUpdate.Grid.Columns.FindByUniqueNameSafe("PreparerDueDays");
            if (oGridColumnPreparerDueDays != null)
            {
                oGridColumnPreparerDueDays.Visible = this._IsDueDateByAccountEnabled;
            }
            GridColumn oGridColumnReviewerDueDays = ucSkyStemARTGridBulkUpdate.Grid.Columns.FindByUniqueNameSafe("ReviewerDueDays");
            if (oGridColumnReviewerDueDays != null)
            {
                oGridColumnReviewerDueDays.Visible = this._IsDueDateByAccountEnabled;
            }
            GridColumn oGridColumnApproverDueDays = ucSkyStemARTGridBulkUpdate.Grid.Columns.FindByUniqueNameSafe("ApproverDueDays");
            if (oGridColumnApproverDueDays != null)
            {
                oGridColumnApproverDueDays.Visible = this._IsDueDateByAccountEnabled && this._IsDualLevelReviewEnabled;
            }
            //GridColumn oGridColumnDayType = ucSkyStemARTGridBulkUpdate.Grid.Columns.FindByUniqueNameSafe("DayType");
            //if (oGridColumnDayType != null)
            //{
            //    oGridColumnDayType.Visible = this._IsDueDateByAccountEnabled && this._IsDualLevelReviewEnabled;
            //}

            btnCancel.Visible = true;

            if (CurrentRecProcessStatus.Value == WebEnums.RecPeriodStatus.Closed
                || CurrentRecProcessStatus.Value == WebEnums.RecPeriodStatus.Skipped
                || isCertificationStarted)
            {
                btnSave.Visible = false;
            }
            else
            {
                btnSave.Visible = true;
            }
            lblGridTitle.LabelID = 1649;

            ucSkyStemARTGridBulkUpdate.CompanyID = SessionHelper.GetCurrentUser().CompanyID;

            if (CurrentRecProcessStatus.Value == WebEnums.RecPeriodStatus.Closed
                || CurrentRecProcessStatus.Value == WebEnums.RecPeriodStatus.Skipped
                || isCertificationStarted)
            {
                ucSkyStemARTGridBulkUpdate.ShowSelectCheckBoxColum = false;
                hdnBulkUpdateMode.Value = "ReadOnly";
                Page.ClientScript.RegisterHiddenField("abcAp", "1");
                Page.ClientScript.RegisterHiddenField("abcApPage", "1");
            }
            else
            {
                ucSkyStemARTGridBulkUpdate.ShowSelectCheckBoxColum = true;
                hdnBulkUpdateMode.Value = "Edit";
                Page.ClientScript.RegisterHiddenField("abcAp", "2");
                Page.ClientScript.RegisterHiddenField("abcApPage", "2");
            }
            ucSkyStemARTGridBulkUpdate.DataSource = oAccountHdrInfoCollection;
            ucSkyStemARTGridBulkUpdate.BindGrid();
            ucSkyStemARTGridBulkUpdate.DataBind();
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

    private bool IsLockedDueToDueDaysAttributes(AccountHdrInfo oAccountHdrInfo)
    {
        bool isLocked = false;
        if (ddlAccountAttribute.SelectedValue != "0")
        {
            ARTEnums.AccountAttribute oAccountAttribute = (ARTEnums.AccountAttribute)Enum.Parse(typeof(ARTEnums.AccountAttribute), ddlAccountAttribute.SelectedValue);
            switch (oAccountAttribute)
            {
                case ARTEnums.AccountAttribute.PreparerDueDays:
                case ARTEnums.AccountAttribute.ReviewerDueDays:
                case ARTEnums.AccountAttribute.ApproverDueDays:
                    if (oAccountHdrInfo.ReconciliationStatusID == (short)WebEnums.ReconciliationStatus.Reconciled
                        || oAccountHdrInfo.ReconciliationStatusID == (short)WebEnums.ReconciliationStatus.SysReconciled)
                        isLocked = true;
                    break;
            }
        }
        return isLocked;
    }


    //Adds a string to StringBuilder.If it already contains some value, new values is added with "," 
    private void AddToStringBuilder(StringBuilder oSb, string strAppend)
    {
        if (!String.IsNullOrEmpty(oSb.ToString()))
            oSb.Append(",");
        oSb.Append(strAppend);
    }

    private void SetCapabilityInfo()
    {

        this._IsReconcilableEnabled = true;

        foreach (CompanyCapabilityInfo oCompanyCapabilityInfo in this._CompanyCapabilityInfoCollection)
        {
            if (oCompanyCapabilityInfo.CapabilityID.HasValue)
            {
                ARTEnums.Capability oCapability = (ARTEnums.Capability)Enum.Parse(typeof(ARTEnums.Capability), oCompanyCapabilityInfo.CapabilityID.Value.ToString());

                switch (oCapability)
                {
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
                    case ARTEnums.Capability.KeyAccount:
                        if (Helper.GetFeatureCapabilityMode(WebEnums.Feature.KeyAccount, ARTEnums.Capability.KeyAccount, SessionHelper.CurrentReconciliationPeriodID) != WebEnums.FeatureCapabilityMode.Hidden)
                        {
                            //do we need this now
                            if (oCompanyCapabilityInfo.IsActivated.HasValue && oCompanyCapabilityInfo.IsActivated.Value)
                            {
                                this._IsKeyAccountEnabled = true;
                            }
                        }
                        break;


                    case ARTEnums.Capability.NetAccount:
                        if (oCompanyCapabilityInfo.IsActivated.HasValue && oCompanyCapabilityInfo.IsActivated.Value)
                        {
                            this._IsNetAccountEnabled = true;
                        }
                        break;

                    case ARTEnums.Capability.DueDateByAccount:
                        if (oCompanyCapabilityInfo.IsActivated.HasValue && oCompanyCapabilityInfo.IsActivated.Value)
                        {
                            this._IsDueDateByAccountEnabled = true;
                        }
                        break;

                    case ARTEnums.Capability.DualLevelReview:
                        if (oCompanyCapabilityInfo.IsActivated.HasValue && oCompanyCapabilityInfo.IsActivated.Value)
                        {
                            this._IsDualLevelReviewEnabled = true;
                        }
                        break;
                }
            }
        }
    }

    private void PopulateAttributeDropdown()
    {
        IUtility oUtilityClient = RemotingHelper.GetUtilityObject();
        List<ARTEnums.AccountAttribute> oAccountAttributeCollection = new List<ARTEnums.AccountAttribute>();
        //oAccountAttributeCollection.Add(ARTEnums.AccountAttribute.AccountType);

        if (this._IsKeyAccountEnabled)
        {
            oAccountAttributeCollection.Add(ARTEnums.AccountAttribute.IsKeyAccount);
        }

        if (this._IsZeroBalanceEnabled)
        {
            oAccountAttributeCollection.Add(ARTEnums.AccountAttribute.IsZeroBalanceAccount);
        }

        if (this._IsRiskRatingEnabled)
        {
            oAccountAttributeCollection.Add(ARTEnums.AccountAttribute.RiskRating);
        }
        //Reconcilable
        if (this._IsReconcilableEnabled)
        {
            oAccountAttributeCollection.Add(ARTEnums.AccountAttribute.IsReconcilable);
        }

        //List<AccountAttributeMstInfo> oAccountAttributeMstInfoCollection = oUtilityClient.SelectAccountAttributeMstForMassUpdate(oAccountAttributeCollection);
        List<AccountAttributeMstInfo> oAccountAttributeMstInfoCollection = oUtilityClient.SelectAccountAttributeMstForMassUpdate(SessionHelper.CurrentCompanyID, SessionHelper.CurrentReconciliationPeriodID, Helper.GetAppUserInfo());
        oAccountAttributeMstInfoCollection = LanguageHelper.TranslateLabelAccountAttribute(oAccountAttributeMstInfoCollection);

        int selectedIndex = ddlAccountAttribute.SelectedIndex;
        ddlAccountAttribute.DataSource = oAccountAttributeMstInfoCollection;
        ddlAccountAttribute.DataTextField = "Name";
        ddlAccountAttribute.DataValueField = "AccountAttributeID";
        ddlAccountAttribute.DataBind();

        ListControlHelper.AddListItemForSelectOne(ddlAccountAttribute);

        if (!this._IsRiskRatingEnabled)
        {
            ListItem oListItem = new ListItem(LanguageUtil.GetValue(1427), "0");
            ddlAccountAttribute.Items.Add(oListItem);
        }

        ddlAccountAttribute.SelectedIndex = selectedIndex;
    }

    private List<AccountHdrInfo> GetSelectedAccountInformation()
    {
        List<AccountHdrInfo> oAccountHdrInfoCollection = (List<AccountHdrInfo>)HttpContext.Current.Session[SessionConstants.SEARCH_RESULTS_ACCOUNT];
        List<AccountHdrInfo> oAccountHdrInfoCollectionNew = new List<AccountHdrInfo>();

        foreach (GridDataItem item in ucSkyStemARTGridBulkUpdate.Grid.SelectedItems)
        {
            string accountId = item[COLUMN_NAME_ID].Text;
            string netAccountId = item[COLUMN_NAME_NETACCOUNTID].Text;
            //if (!string.IsNullOrEmpty(netAccountId) && Convert.ToInt32(netAccountId) > 0)
            //{
            //    throw new ARTException(5000044);
            //}

            AccountHdrInfo oAccountHdrInfo = (from accHdr in oAccountHdrInfoCollection where accHdr.AccountID.ToString() == accountId select accHdr).FirstOrDefault();

            if (this._IsZeroBalanceEnabled)
            {
                ExCheckBox chkZeroBalance = (ExCheckBox)item.FindControl("chkZeroBalanceAccount");
                if (chkZeroBalance.Checked)
                {
                    oAccountHdrInfo.IsZeroBalance = true;
                }
                else
                {
                    oAccountHdrInfo.IsZeroBalance = false;
                }
            }
            else
            {
                oAccountHdrInfo.IsZeroBalance = null;
            }

            if (this._IsKeyAccountEnabled)
            {
                ExCheckBox chkkeyAccount = (ExCheckBox)item.FindControl("chkKeyAccount");
                if (chkkeyAccount.Checked)
                {
                    oAccountHdrInfo.IsKeyAccount = true;
                }
                else
                {
                    oAccountHdrInfo.IsKeyAccount = false;
                }
            }
            else
            {
                oAccountHdrInfo.IsKeyAccount = null;
            }

            if (this._IsRiskRatingEnabled)
            {
                UserControls_RiskRatingDropDown ddlRiskRating = (UserControls_RiskRatingDropDown)item.FindControl("ddlRiskRating");
                oAccountHdrInfo.RiskRatingID = Convert.ToInt16(ddlRiskRating.SelectedValue);
            }
            else
            {
                oAccountHdrInfo.RiskRatingID = null;
                HtmlInputText txtRecPeriodConatiner = (HtmlInputText)item.FindControl("txtRecPeriodsContainer");
                string recPeriodIds = txtRecPeriodConatiner.Value;
                string[] recPeriodIdCollection = recPeriodIds.Split(';');

                List<int> intList = new List<int>();
                foreach (string recPeriod in recPeriodIdCollection)
                {
                    if (!string.IsNullOrEmpty(recPeriod))
                    {
                        intList.Add(Convert.ToInt32(recPeriod));
                    }
                }
                if (intList.Count == 0)
                {
                    throw new ARTException(5000052);
                }
                oAccountHdrInfo.RecPeriodIDCollection = intList;
            }
            //Reconcilable
            if (this._IsReconcilableEnabled)
            {
                ExCheckBox chkkeyAccount = (ExCheckBox)item.FindControl("chkIsReconcilable");
                if (chkkeyAccount != null)
                {
                    oAccountHdrInfo.IsReconcilable = chkkeyAccount.Checked;
                }
            }
            UserControls_ReconciliationTemplateDropDown ddlReconciliationTemplate = (UserControls_ReconciliationTemplateDropDown)item.FindControl("ddlReconciliationTemplate");
            oAccountHdrInfo.ReconciliationTemplateID = Convert.ToInt16(ddlReconciliationTemplate.SelectedValue);

            UserControls_SubledgerSourceDropDown ddlSubledgerSource = (UserControls_SubledgerSourceDropDown)item.FindControl("ddlSubledger");
            if (ddlSubledgerSource.SelectedValue != WebConstants.SELECT_ONE)
            {
                oAccountHdrInfo.SubLedgerSourceID = Convert.ToInt32(ddlSubledgerSource.SelectedValue);
            }
            else
            {
                oAccountHdrInfo.SubLedgerSourceID = null;
            }

            //UserControls_AccountTypeDropDown ddlAccountType = (UserControls_AccountTypeDropDown)item.FindControl("ddlAccountType");
            //oAccountHdrInfo.AccountTypeID = Convert.ToInt16(ddlAccountType.SelectedValue);

            //Commented by Vinay as original values should be preserved
            //oAccountHdrInfo.Description = null;
            //oAccountHdrInfo.AccountPolicyUrl = null;
            //oAccountHdrInfo.ApproverUserID = null;
            //oAccountHdrInfo.PreparerUserID = null;
            //oAccountHdrInfo.ReconciliationProcedure = null;
            //oAccountHdrInfo.ReviewerUserID = null;

            //Preparer Due Days
            if (this._IsDueDateByAccountEnabled)
            {
                TextBox txtPreparerDueDays = (TextBox)item.FindControl("txtPreparerDueDays");
                if (txtPreparerDueDays != null)
                {
                    oAccountHdrInfo.PreparerDueDays = Convert.ToInt32(txtPreparerDueDays.Text);
                }
                TextBox txtReviewerDueDays = (TextBox)item.FindControl("txtReviewerDueDays");
                if (txtReviewerDueDays != null)
                {
                    oAccountHdrInfo.ReviewerDueDays = Convert.ToInt32(txtReviewerDueDays.Text);
                }
                TextBox txtApproverDueDays = (TextBox)item.FindControl("txtApproverDueDays");
                if (txtApproverDueDays != null && !string.IsNullOrEmpty(txtApproverDueDays.Text) && this._IsDualLevelReviewEnabled)
                {
                    oAccountHdrInfo.ApproverDueDays = Convert.ToInt32(txtApproverDueDays.Text);
                }
                else
                    oAccountHdrInfo.ApproverDueDays = null;
                //UserControls_DayTypeDropDown ddlDayType = (UserControls_DayTypeDropDown)item.FindControl("ddlDayType");
                //oAccountHdrInfo.DayTypeID = Convert.ToInt16(ddlDayType.SelectedValue);
            }

            oAccountHdrInfoCollectionNew.Add(oAccountHdrInfo);
        }
        return oAccountHdrInfoCollectionNew;
    }

    private bool ValidateAndSaveRecFrequency(List<long> oAccountIDCollection, List<AccountAttributeWarningInfo> oAccountIDNetAccountIDCollection)
    {
        if (string.IsNullOrEmpty(txtRecPriodIDContainer.Value))
        {
            throw new ARTException(5000052);
        }

        List<int> oRecPeriodIdCollection = new List<int>();
        string[] strRecPeriodIDCollection = txtRecPriodIDContainer.Value.Split(';');

        foreach (string recPeriodId in strRecPeriodIDCollection)
        {
            if (!string.IsNullOrEmpty(recPeriodId))
            {
                oRecPeriodIdCollection.Add(Convert.ToInt32(recPeriodId));
            }
        }


        bool result = false;
        ValidateIsWarningOccur(oAccountIDNetAccountIDCollection, new List<AccountHdrInfo>());
        // Save values
        if (hdnConfirm.Value == "Yes")
        {
            IAccount oAccountClient = RemotingHelper.GetAccountObject();
            result = oAccountClient.SaveAccountRecFrequecy(oAccountIDCollection, oRecPeriodIdCollection,
                SessionHelper.CurrentCompanyID.Value, SessionHelper.CurrentReconciliationPeriodID.Value, Helper.GetAppUserInfo());
        }

        return result;
    }

    private string GetAttributeValue(ARTEnums.AccountAttribute oAccountAttribute, List<AccountHdrInfo> oAccountHdrInfoCollectionNew)
    {
        string attributeValue = null;

        switch (oAccountAttribute)
        {
            case ARTEnums.AccountAttribute.IsKeyAccount:
                if (optIsNo.Checked)
                {
                    attributeValue = "false";
                }
                else
                {
                    attributeValue = "true";
                }
                break;
            case ARTEnums.AccountAttribute.IsZeroBalanceAccount:
                if (optIsNo.Checked)
                {
                    attributeValue = "false";
                    oAccountHdrInfoCollectionNew.ForEach(o => o.IsZeroBalance = false);
                }
                else
                {
                    attributeValue = "true";
                    oAccountHdrInfoCollectionNew.ForEach(o => o.IsZeroBalance = true);
                }
                break;
            case ARTEnums.AccountAttribute.IsReconcilable:
                if (optIsNo.Checked)
                {
                    attributeValue = "false";
                    oAccountHdrInfoCollectionNew.ForEach(o => o.IsReconcilable = false);
                }
                else
                {
                    attributeValue = "true";
                    oAccountHdrInfoCollectionNew.ForEach(o => o.IsReconcilable = true);
                }
                break;

            //case ARTEnums.AccountAttribute.AccountType:
            //    attributeValue = ddlAccountType.SelectedValue;
            //    break;

            case ARTEnums.AccountAttribute.RiskRating:
                attributeValue = ddlRiskRating.SelectedValue;
                oAccountHdrInfoCollectionNew.ForEach(o => o.RiskRatingID = Convert.ToInt16(attributeValue));
                break;
            //case ARTEnums.AccountAttribute.DayType:
            //    attributeValue = ddlDayType.SelectedValue;
            //    oAccountHdrInfoCollectionNew.ForEach(o => o.DayTypeID = Convert.ToInt16(attributeValue));
            //    break;
            case ARTEnums.AccountAttribute.PreparerDueDays:
            case ARTEnums.AccountAttribute.ReviewerDueDays:
            case ARTEnums.AccountAttribute.ApproverDueDays:
                attributeValue = txtDueDays.Text;
                break;
        }

        return attributeValue;
    }

    private void ValidateAttributeValue(ARTEnums.AccountAttribute oAccountAttribute)
    {
        switch (oAccountAttribute)
        {
            case ARTEnums.AccountAttribute.IsKeyAccount:
            case ARTEnums.AccountAttribute.IsZeroBalanceAccount:
            case ARTEnums.AccountAttribute.IsReconcilable:
                if (!optIsNo.Checked && !optIsYes.Checked)
                {
                    throw new ARTException(5000049);
                }
                break;

            //case ARTEnums.AccountAttribute.AccountType:
            //    if (ddlAccountType.SelectedValue == WebConstants.SELECT_ONE)
            //    {
            //        throw new ARTException(5000050);
            //    }
            //    break;

            case ARTEnums.AccountAttribute.RiskRating:
                if (ddlRiskRating.SelectedValue == WebConstants.SELECT_ONE)
                {
                    throw new ARTException(5000051);
                }
                break;
            //case ARTEnums.AccountAttribute.DayType:
            //    if (ddlDayType.SelectedValue == WebConstants.SELECT_ONE)
            //    {
            //        throw new ARTException(5000414);
            //    }
            //    break;
            case ARTEnums.AccountAttribute.PreparerDueDays:
            case ARTEnums.AccountAttribute.ReviewerDueDays:
            case ARTEnums.AccountAttribute.ApproverDueDays:
                ValidateDueDaysForMassUpdate(oAccountAttribute);
                break;
            default:
                throw new ARTException(5000053);
        }
    }

    private void ValidateDueDaysForMassUpdate(ARTEnums.AccountAttribute oAccountAttribute)
    {
        if (string.IsNullOrEmpty(txtDueDays.Text))
            throw new ARTException(5000376);
        int dueDays = 0;
        if (!Int32.TryParse(txtDueDays.Text, out dueDays) || dueDays == 0)
        {
            throw new ARTException(5000378);
        }
        foreach (GridDataItem item in ucSkyStemARTGridMassUpdate.Grid.SelectedItems)
        {
            int? PreparerDueDays = (int?)item.GetDataKeyValue("PreparerDueDays");
            int? ReviewerDueDays = (int?)item.GetDataKeyValue("ReviewerDueDays");
            int? ApproverDueDays = (int?)item.GetDataKeyValue("ApproverDueDays");
            if (oAccountAttribute == ARTEnums.AccountAttribute.PreparerDueDays)
            {
                if ((ReviewerDueDays.HasValue && dueDays > ReviewerDueDays.Value)
                    || (ApproverDueDays.HasValue && dueDays > ApproverDueDays.Value))
                {
                    throw new ARTException(5000377);
                }
            }
            if (oAccountAttribute == ARTEnums.AccountAttribute.ReviewerDueDays)
            {
                if (PreparerDueDays.GetValueOrDefault() == 0
                    || (dueDays < PreparerDueDays.GetValueOrDefault())
                    || (ApproverDueDays.HasValue && dueDays > ApproverDueDays.Value))
                {
                    throw new ARTException(5000377);
                }
            }
            if (oAccountAttribute == ARTEnums.AccountAttribute.ApproverDueDays)
            {
                if (PreparerDueDays.GetValueOrDefault() == 0
                    || ReviewerDueDays.GetValueOrDefault() == 0
                    || (dueDays < PreparerDueDays.GetValueOrDefault())
                    || (dueDays < ReviewerDueDays.GetValueOrDefault()))
                {
                    throw new ARTException(5000377);
                }
            }
        }
    }

    private void ValidateNetAccountsForConstituentAccountCount(List<int> oNetAccountIDList, List<long> oAccountIDList)
    {
        List<int> netAccountIdList = (from netAcc in oNetAccountIDList
                                      select netAcc).Distinct().ToList();

        IAccount oAccountClient = RemotingHelper.GetAccountObject();

        foreach (int netAccountId in netAccountIdList)
        {
            //int count = (from netAcc in oNetAccountIDCollection
            //             where netAcc == netAccountId
            //             select netAcc).Count();
            //List<AccountHdrInfo> oNetAccountCollectionInDB = oAccountClient.GetNetAccountHdrInfoCollectionByNetID(SessionHelper.CurrentReconciliationPeriodID.Value, netAccountId);
            //if (count != oNetAccountCollectionInDB.Count)
            if (!NetAccountHelper.IsAllConstituentAccountsSelected(netAccountId, SessionHelper.CurrentReconciliationPeriodID.Value, oAccountIDList))
            {
                throw new ARTException(5000048);
            }
        }
    }


    private void ValidateNetAccountsForBulkUpdate(List<int> oNetAccountIDList, List<AccountHdrInfo> oAccountHdrInfoList)
    {
        List<int> netAccountIdCollection = (from netAcc in oNetAccountIDList
                                            select netAcc).Distinct().ToList();

        foreach (int netAccountId in netAccountIdCollection)
        {
            if (netAccountId > 0)
            {
                List<AccountHdrInfo> oAccountHdrInfoListForNetAccount = oAccountHdrInfoList.FindAll(T => T.NetAccountID.GetValueOrDefault() == netAccountId);
                if (oAccountHdrInfoListForNetAccount != null && oAccountHdrInfoListForNetAccount.Count > 1)
                {
                    AccountHdrInfo oAccountHdrInfoFirst = oAccountHdrInfoListForNetAccount[0];
                    for (int j = 1; j < oAccountHdrInfoListForNetAccount.Count; j++)
                    {
                        if (!NetAccountHelper.IsAccountAttributesMatchForNetAccount(oAccountHdrInfoFirst, oAccountHdrInfoListForNetAccount[j]))
                        {
                            throw new ARTException(5000044);
                        }
                    }
                }
            }
        }
    }

    /// <summary>
    /// Hides panel
    /// </summary>
    private void HidePanels()
    {
        btnCancel.Visible = false;
        btnSave.Visible = false;
        pnlBulkUpdate.Visible = false;
        pnlMassUpdate.Visible = false;
    }

    /// <summary>
    /// Hides panel based on Mass/Bulk search.
    /// </summary>
    private void HidePanels(bool isMassUpdate)
    {
        if (isMassUpdate)
        {
            pnlMassUpdate.Visible = true;
            pnlBulkUpdate.Visible = false;
        }
        else
        {
            pnlMassUpdate.Visible = false;
            pnlBulkUpdate.Visible = true;
        }

    }
    #endregion

    #region Other Methods
    /// <summary>
    /// Gets menu key
    /// </summary>
    /// <returns>Menu key</returns>
    public override string GetMenuKey()
    {
        return "AccountProfileMassAndBulkUpdate";
    }
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
                msg = msg + "\\n" + LanguageUtil.GetValue(2221);

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "newWindow", "ConfirmNetAccount('" + btnSave.ClientID + "','" + hdnConfirm.ClientID + "','" + msg + "');", true);
            }
            else
            {
                hdnConfirm.Value = "Yes";
            }
        }
    }

    public void ompage_ReconciliationPeriodChangedEventHandler(object sender, EventArgs e)
    {
        ucAccountSearchControl.ReloadControl();
        HidePanels();
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


    #endregion

}


