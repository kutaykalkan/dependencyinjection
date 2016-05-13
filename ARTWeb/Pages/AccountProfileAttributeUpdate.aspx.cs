using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SkyStem.ART.Web.Utility;
using SkyStem.ART.Web.Data;
using SkyStem.ART.Client.Model;
using SkyStem.Language.LanguageUtility;
using Telerik.Web.UI;
using SkyStem.ART.Client.Model.Base;
using SkyStem.Library.Controls.WebControls;
using SkyStem.ART.Web.Classes;
using System;
using SkyStem.ART.Client.IServices;
using System.Collections.Generic;
using SkyStem.ART.Client.Exception;
using SkyStem.ART.Client.Data;

public partial class Pages_AccountProfileAttributeUpdate : PageBaseRecPeriod
{
    #region Variables & Constants
    long _AccountID = 0;
    int? _NetAccountID = null;
    bool _IsRiskRatingEnabled;
    bool _IsZeroBalanceEnabled;
    bool _IsKeyAccountEnabled;
    bool _IsDualReviewEnabled;
    bool _IsMaterialityEnabled;
    bool _IsNetAccountEnabled;
    bool _IsReconcilableEnabled;
    bool _IsDueDateByAccountEnabled;
    bool _IsDualLevelReviewByAccountEnabled;
    #endregion

    #region Properties
    private List<CompanyCapabilityInfo> _CompanyCapabilityInfoCollection = null;
    List<AccountReconciliationPeriodInfo> _AccountReconciliationPeriodInfoCollection = null;
    AccountHdrInfo _AccountHdrInfo = null;
    #endregion

    #region Delegates & Events
    /// <summary>
    /// Reconciliation period changed event handler. Also works as page load.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public void ompage_ReconciliationPeriodChangedEventHandler(object sender, EventArgs e)
    {
        try
        {
            OnPageLoad();
            WebEnums.FormMode mode = Helper.GetFormModeForAccountPages();
            if (this._AccountHdrInfo != null && this._AccountHdrInfo.NetAccountID.HasValue && this._AccountHdrInfo.NetAccountID.Value > 0)
            {
                mode = WebEnums.FormMode.ReadOnly;
            }
            ucRecFrequencySelection.URL = Helper.GetUrlForRecFrequency(this._AccountID, txtRecPeriodIDContainer.ClientID, txtRecPeriodIDContainer.Value, mode);

            PopulateItemsOnPage();
            DisableControls();
            DisableNetAccountAttributes();
        }
        catch (Exception ex)
        {
            Helper.ShowErrorMessage(this, ex);
        }
    }
    #endregion

    #region Page Events
    protected void Page_Init(object sender, EventArgs e)
    {
        MasterPageBase ompage = (MasterPageBase)this.Master;
        ompage.ReconciliationPeriodChangedEventHandler += new EventHandler(ompage_ReconciliationPeriodChangedEventHandler);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        OnPageLoad();

    }

    protected void Page_PreRender(Object source, EventArgs e)
    {

        DropDownList ddlRiskratiingtoStForHyperLink = (DropDownList)ddlRiskRating.FindControl("ddlRiskRating");
        ExHyperLink hlRecFrequency = (ExHyperLink)ucRiskRatingSelection.FindControl("hlRecFrequency");
        ddlRiskratiingtoStForHyperLink.Attributes.Add("onchange", "SetURLForRiskRating('" + ddlRiskratiingtoStForHyperLink.ClientID + "', '" + hlRecFrequency.ClientID + "')");
        if (ddlRiskRating.SelectedValue != "" && ddlRiskRating.SelectedValue != WebConstants.SELECT_ONE)
        {
            hlRecFrequency.Attributes.CssStyle.Add("visibility", "visible");
        }
        else
            hlRecFrequency.Attributes.CssStyle.Add("visibility", "hidden");
        if (!this._IsRiskRatingEnabled)
        {
            lblRiskRating.LabelID = 1427;
            ddlRiskRating.Visible = false;
            ddlRiskRating.ValidatorEnable = false;
            ucRecFrequencySelection.Visible = true;
            ucRiskRatingSelection.Visible = false;
        }
        else
        {
            lblRiskRating.LabelID = 1013;
            ddlRiskRating.Visible = true;
            ddlRiskRating.ValidatorEnable = true;
            ucRecFrequencySelection.Visible = false;
            ucRiskRatingSelection.Visible = true;
        }
    }
    #endregion

    #region Grid Events
    #endregion

    #region Other Events
    /// <summary>
    /// Hanldes cancel button click event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCancel_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("AccountProfileUpdate.aspx?ShowGrid=true");
    }

    /// <summary>
    /// Hanldes save button click event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSave_OnClick(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                //ValidateInput();
                // ******commented by Prafull on 08-Mar-2011 
                //if (this._AccountHdrInfo != null && this._AccountHdrInfo.NetAccountID.HasValue && this._AccountHdrInfo.NetAccountID.Value > 0)
                //{
                //    throw new ARTException(5000044);
                //}

                List<AccountAttributeWarningInfo> oAccountIDNetAccountIDCollection = new List<AccountAttributeWarningInfo>();
                AccountAttributeWarningInfo oAccountAttributeWarningInfo = new AccountAttributeWarningInfo();
                oAccountAttributeWarningInfo.AccountID = this._AccountID;
                if (this._NetAccountID.HasValue)
                    oAccountAttributeWarningInfo.NetAccountID = this._NetAccountID;
                oAccountIDNetAccountIDCollection.Add(oAccountAttributeWarningInfo);

                AccountHdrInfo oAccountHdrInfo = this.GetAccountInformation();

                List<AccountHdrInfo> oAccountHdrInfoCollection = new List<AccountHdrInfo>();
                oAccountHdrInfoCollection.Add(oAccountHdrInfo);

                if (!this._IsRiskRatingEnabled)
                {
                    oAccountHdrInfo.RecPeriodIDCollection = this.GetSelectedRecPeriods();
                }
                ValidateIsWarningOccur(oAccountIDNetAccountIDCollection, oAccountHdrInfoCollection);
                if (hdnConfirm.Value == "Yes")
                {
                    IAccount oAccountClient = RemotingHelper.GetAccountObject();
                    bool result = oAccountClient.SaveAccountProfile(oAccountHdrInfoCollection, SessionHelper.CurrentCompanyID.Value, SessionHelper.CurrentReconciliationPeriodID.Value, SessionHelper.GetCurrentUser().LoginID, DateTime.Now, (short)ARTEnums.ActionType.AccountAttributeChangeFromUI, Helper.GetAppUserInfo());

                    RaiseAlertIfAttributesHaveChanged(oAccountHdrInfo);
                    RaiseAlertIfOwnershipChanged(oAccountHdrInfo);
                    int LabelID = 1597;
                    hdnConfirm.Value = "";
                    Response.Redirect("AccountProfileUpdate.aspx?" + QueryStringConstants.CONFIRMATION_MESSAGE_LABEL_ID + "=" + LabelID.ToString());
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
    protected void ctrySelectBox_DropDownUserSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (ddlApprover.SelectedValue == "-2")
        {
            rfvApproverDueDays.Enabled = false;
        }
        else
        {
            rfvApproverDueDays.Enabled = true;
        }
    }

    #endregion

    #region Validation Control Events
    protected void cvPreparerDueDays_OnServerValidate(object source, ServerValidateEventArgs args)
    {
        int preparerDueDays = 0;
        if (!Int32.TryParse(txtPreparerDueDays.Text, out preparerDueDays) || preparerDueDays == 0)
        {
            cvPreparerDueDays.ErrorMessage = LanguageUtil.GetValue(5000378);
            args.IsValid = false;
        }
    }

    protected void cvReviewerDueDays_OnServerValidate(object source, ServerValidateEventArgs args)
    {
        int preparerDueDays = 0;
        int reviewerDueDays = 0;
        if (!Int32.TryParse(txtReviewerDueDays.Text, out reviewerDueDays) || reviewerDueDays == 0)
        {
            cvReviewerDueDays.ErrorMessage = LanguageUtil.GetValue(5000378);
            args.IsValid = false;
        }
        else if (Int32.TryParse(txtPreparerDueDays.Text, out preparerDueDays))
        {
            if (reviewerDueDays < preparerDueDays)
            {
                cvReviewerDueDays.ErrorMessage = Helper.GetErrorMessage(WebEnums.FieldType.DateCompareFieldGreaterThan, lblReviewerDueDays.LabelID, lblPreparerDueDays.LabelID);
                args.IsValid = false;
            }
        }
    }

    protected void cvApproverDueDays_OnServerValidate(object source, ServerValidateEventArgs args)
    {
        int reviewerDueDays = 0;
        int approverDueDays = 0;
        if (!Int32.TryParse(txtApproverDueDays.Text, out approverDueDays) || approverDueDays == 0)
        {
            cvApproverDueDays.ErrorMessage = LanguageUtil.GetValue(5000378);
            args.IsValid = false;
        }
        else if (Int32.TryParse(txtReviewerDueDays.Text, out reviewerDueDays))
        {
            if (approverDueDays < reviewerDueDays)
            {
                cvApproverDueDays.ErrorMessage = Helper.GetErrorMessage(WebEnums.FieldType.DateCompareFieldGreaterThan, lblApproverDueDays.LabelID, lblReviewerDueDays.LabelID);
                args.IsValid = false;
            }

            else if (ddlApprover.SelectedValue == "-2")
            {
                ddlApprover.ValidatorEnable = true;
            }
        }

    }
    #endregion

    #region Private Methods
    private void OnPageLoad()
    {
        SetPageSettings();
        if (Request.QueryString[QueryStringConstants.ACCOUNT_ID] != null)
            this._AccountID = Convert.ToInt64(Request.QueryString[QueryStringConstants.ACCOUNT_ID]);
        //if (Request.QueryString[QueryStringConstants.NETACCOUNT_ID] != null)
        //    this._NetAccountID = Convert.ToInt32(Request.QueryString[QueryStringConstants.NETACCOUNT_ID]);

        ucAccountHierarchyDetail.AccountID = this._AccountID;

        this._CompanyCapabilityInfoCollection = SessionHelper.GetCompanyCapabilityCollectionForCurrentRecPeriod();
        SetCapabilityInfo();
        SetErrorMessagesForValidationControls();

        IAccount oAccountClient = RemotingHelper.GetAccountObject();
        this._AccountReconciliationPeriodInfoCollection = oAccountClient.SelectAccountRecPeriodByAccountID(this._AccountID, Helper.GetAppUserInfo());
        this._AccountHdrInfo = oAccountClient.GetAccountHdrInfoByAccountID(this._AccountID, SessionHelper.CurrentCompanyID.Value, SessionHelper.CurrentReconciliationPeriodID.Value, Helper.GetAppUserInfo());
        this._NetAccountID = this._AccountHdrInfo.NetAccountID;
        List<AccountHdrInfo> oAccountHdrInfoCollection = new List<AccountHdrInfo>();
        oAccountHdrInfoCollection.Add(this._AccountHdrInfo);
        oAccountHdrInfoCollection = LanguageHelper.TranslateLabelsAccountHdr(oAccountHdrInfoCollection);

        if (!IsPostBack)
        {
            this._AccountReconciliationPeriodInfoCollection = oAccountClient.SelectAccountRecPeriodByAccountID(Convert.ToInt32(this._AccountID), Helper.GetAppUserInfo());
            foreach (AccountReconciliationPeriodInfo oRecPeriodInfo in this._AccountReconciliationPeriodInfoCollection)
            {
                txtRecPeriodIDContainer.Value += oRecPeriodInfo.ReconciliationPeriodID.Value.ToString() + ";";
            }

        }

        ddlBackupPreparer.ValidatorEnable = false;
        ddlBackupReviewer.ValidatorEnable = false;
        ddlBackupApprover.ValidatorEnable = false;
        string ApproverBackupApproverClientID = ddlApprover.ClientID + "," + ddlBackupApprover.ClientID;
        cvApproverBackupApprover.Attributes.Add("ApproverBackupApproverClientID", ApproverBackupApproverClientID);
        cvApproverBackupApprover.ErrorMessage = Helper.GetLabelIDValue(5000328);
    }
    /// <summary>
    /// Initializes Items e.g. dropdowns on page
    /// </summary>
    private void PopulateItemsOnPage()
    {
        try
        {
            ucDescription.EditorControl.Content = _AccountHdrInfo.Description;
            if (_AccountHdrInfo.DescriptionLabelID != null)
            {
                ucDescription.EditorControl.Attributes.Add(WebConstants.ATTRIBUTE_LABEL_ID, _AccountHdrInfo.DescriptionLabelID.Value.ToString());
            }

            ucAccountPolicyURL.EditorControl.Content = _AccountHdrInfo.AccountPolicyUrl;
            if (_AccountHdrInfo.AccountPolicyUrlLabelID != null)
            {
                ucAccountPolicyURL.EditorControl.Attributes.Add(WebConstants.ATTRIBUTE_LABEL_ID, _AccountHdrInfo.AccountPolicyUrlLabelID.Value.ToString());
            }

            ucReconciliationProcedure.EditorControl.Content = _AccountHdrInfo.ReconciliationProcedure;
            if (_AccountHdrInfo.ReconciliationProcedureLabelID != null)
            {
                ucReconciliationProcedure.EditorControl.Attributes.Add(WebConstants.ATTRIBUTE_LABEL_ID, _AccountHdrInfo.ReconciliationProcedureLabelID.Value.ToString());
            }

            lblReportingCurrencyValue.Text = SessionHelper.ReportingCurrencyCode;
            //lblBaseCurrencyvalue.Text = SessionHelper.BaseCurrencyCode;
            //BCCY Changes
            lblBaseCurrencyvalue.Text = Helper.GetDisplayBaseCurrencyCode(this._AccountHdrInfo.BaseCurrencyCode);

            if (this._IsMaterialityEnabled)
            {
                IAccount oAccountClient = RemotingHelper.GetAccountObject();
                bool? isMaterial = oAccountClient.GetAccountMateriality(SessionHelper.CurrentCompanyID.Value,
                                                                        this._AccountID,
                                                                        SessionHelper.CurrentReconciliationPeriodID.Value,
                                                                        this._IsMaterialityEnabled, Helper.GetAppUserInfo());

                if (isMaterial == null)
                {
                    lblAcMaterialityValue.Text = "-";
                    imgGLDataUnAvailable.Visible = true;
                    imgGLDataUnAvailable.AlternateText = LanguageUtil.GetValue(1734);
                }
                else if (isMaterial.Value)
                {
                    lblAcMaterialityValue.LabelID = 1252;
                    imgGLDataUnAvailable.Visible = false;
                }
                else
                {
                    lblAcMaterialityValue.LabelID = 1251;
                    imgGLDataUnAvailable.Visible = false;
                }
            }
            else
            {
                lblAcMateriality.Enabled = false;
            }
            if (_AccountHdrInfo.AccountTypeID.HasValue && _AccountHdrInfo.AccountTypeID.Value > 0)
                lblAccountTypeValue.Text = _AccountHdrInfo.AccountType;
            else
                lblAccountTypeValue.Text = "-";

            //Populate Subledger drop down            
            if (_AccountHdrInfo.SubLedgerSourceID.HasValue && _AccountHdrInfo.SubLedgerSourceID.Value > 0)
                ddlSubledgerSource.SelectedValue = _AccountHdrInfo.SubLedgerSourceID.Value.ToString();
            else
                ddlSubledgerSource.SelectedValue = WebConstants.SELECT_ONE;

            //Populate Reconciliation template dropdown
            if (_AccountHdrInfo.ReconciliationTemplateID.HasValue && _AccountHdrInfo.ReconciliationTemplateID.Value > 0)
                ddlReconciliationTemplate.SelectedValue = _AccountHdrInfo.ReconciliationTemplateID.Value.ToString();
            else
                ddlReconciliationTemplate.SelectedValue = WebConstants.SELECT_ONE;

            if (this._IsRiskRatingEnabled)
            {
                //Populate Risk Rating Dropdown
                if (_AccountHdrInfo.RiskRatingID.HasValue && _AccountHdrInfo.RiskRatingID.Value > 0)
                {
                    ddlRiskRating.SelectedValue = _AccountHdrInfo.RiskRatingID.Value.ToString();
                    ucRiskRatingSelection.RiskRatingRecPeriodID = _AccountHdrInfo.RiskRatingID.Value;
                }
                else
                    ddlRiskRating.SelectedValue = WebConstants.SELECT_ONE;
            }

            //Populate Preparer dropdown
            if (_AccountHdrInfo.PreparerUserID.HasValue && _AccountHdrInfo.PreparerUserID.Value > 0)
                ddlPreparer.SelectedValue = _AccountHdrInfo.PreparerUserID.Value.ToString();
            else
                ddlPreparer.SelectedValue = WebConstants.SELECT_ONE;

            //Populate Reviewer dropdown
            if (_AccountHdrInfo.ReviewerUserID.HasValue && _AccountHdrInfo.ReviewerUserID.Value > 0)
                ddlReviewer.SelectedValue = _AccountHdrInfo.ReviewerUserID.Value.ToString();
            else
                ddlReviewer.SelectedValue = WebConstants.SELECT_ONE;

            //Populate Backup Preparer dropdown
            if (_AccountHdrInfo.BackupPreparerUserID.HasValue && _AccountHdrInfo.BackupPreparerUserID.Value > 0)
                ddlBackupPreparer.SelectedValue = _AccountHdrInfo.BackupPreparerUserID.Value.ToString();
            else
                ddlBackupPreparer.SelectedValue = WebConstants.SELECT_ONE;

            //Populate Backup Reviewer dropdown
            if (_AccountHdrInfo.BackupReviewerUserID.HasValue && _AccountHdrInfo.BackupReviewerUserID.Value > 0)
                ddlBackupReviewer.SelectedValue = _AccountHdrInfo.BackupReviewerUserID.Value.ToString();
            else
                ddlBackupReviewer.SelectedValue = WebConstants.SELECT_ONE;

            if (this._IsDualReviewEnabled)
            {
                //Populate Approver dropdown
                if (_AccountHdrInfo.ApproverUserID.HasValue && _AccountHdrInfo.ApproverUserID.Value > 0)
                    ddlApprover.SelectedValue = _AccountHdrInfo.ApproverUserID.Value.ToString();
                else
                    ddlApprover.SelectedValue = WebConstants.SELECT_ONE;

                //Populate Backup Approver dropdown
                if (_AccountHdrInfo.BackupApproverUserID.HasValue && _AccountHdrInfo.BackupApproverUserID.Value > 0)
                    ddlBackupApprover.SelectedValue = _AccountHdrInfo.BackupApproverUserID.Value.ToString();
                else
                    ddlBackupApprover.SelectedValue = WebConstants.SELECT_ONE;
            }

            if (this._IsKeyAccountEnabled)
            {
                if (!this._AccountHdrInfo.IsIsKeyAccountNull && this._AccountHdrInfo.IsKeyAccount.HasValue && this._AccountHdrInfo.IsKeyAccount.Value == true)
                {
                    optIsKeyAccountYes.Checked = true;
                }
                else if (!this._AccountHdrInfo.IsIsKeyAccountNull && this._AccountHdrInfo.IsKeyAccount.HasValue && this._AccountHdrInfo.IsKeyAccount.Value == false)
                {
                    optIsKeyAccountNo.Checked = true;
                }
            }

            if (this._IsZeroBalanceEnabled)
            {
                if (!this._AccountHdrInfo.IsIsZeroBalanceNull && this._AccountHdrInfo.IsZeroBalance.HasValue && this._AccountHdrInfo.IsZeroBalance.Value == true)
                {
                    optZeroBalanceAccountYes.Checked = true;
                }
                else if (!this._AccountHdrInfo.IsIsZeroBalanceNull && this._AccountHdrInfo.IsZeroBalance.HasValue && this._AccountHdrInfo.IsZeroBalance.Value == false)
                {
                    optZeroBalanceAccountNo.Checked = true;
                }
            }

            lblNetAccountValue.Text = Helper.GetDisplayStringValue(null);
            if (this._IsNetAccountEnabled)
            {
                if (this._AccountHdrInfo.NetAccountID.HasValue && this._AccountHdrInfo.NetAccountLabelID.HasValue)
                    lblNetAccountValue.Text = Helper.GetDisplayStringValue(LanguageUtil.GetValue(this._AccountHdrInfo.NetAccountLabelID.Value));
            }

            lblCreationDateValue.Text = Helper.GetDisplayDate(this._AccountHdrInfo.CreationPeriodEndDate);

            //Reconcilable
            if (this._IsReconcilableEnabled && this._AccountHdrInfo.IsReconcilable.HasValue)
            {
                if (this._AccountHdrInfo.IsReconcilable.Value)
                {
                    this.optIsReconcilableYes.Checked = true;
                }
                else
                {
                    this.optIsReconcilableNo.Checked = true;
                }
            }
            //Due Date By Account
            this.txtPreparerDueDays.Text = string.Empty;
            this.txtReviewerDueDays.Text = string.Empty;
            this.txtApproverDueDays.Text = string.Empty;

            if (this._IsDueDateByAccountEnabled)
            {
                this.txtPreparerDueDays.Text = Helper.GetDisplayIntegerValueForTextBox(this._AccountHdrInfo.PreparerDueDays);
                this.txtReviewerDueDays.Text = Helper.GetDisplayIntegerValueForTextBox(this._AccountHdrInfo.ReviewerDueDays);
                this.txtApproverDueDays.Text = Helper.GetDisplayIntegerValueForTextBox(this._AccountHdrInfo.ApproverDueDays);
                //if (this._AccountHdrInfo.DayTypeID.GetValueOrDefault() > 0)
                //    ddlDayType.SelectedValue = this._AccountHdrInfo.DayTypeID.ToString();
                //else
                //    ddlDayType.SelectedValue = WebConstants.SELECT_ONE;
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

    private void SetCapabilityInfo()
    {
        // Set Default Values
        this._IsRiskRatingEnabled = false;
        this._IsZeroBalanceEnabled = false;
        this._IsKeyAccountEnabled = false;
        this._IsDualReviewEnabled = false;
        this._IsMaterialityEnabled = false;
        this._IsNetAccountEnabled = false;
        this._IsDueDateByAccountEnabled = false;
        this._IsReconcilableEnabled = true;//Reconcilable
        this._IsDualLevelReviewByAccountEnabled = false;

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
                        if (Helper.GetFeatureCapabilityMode(WebEnums.Feature.KeyAccount, ARTEnums.Capability.KeyAccount, SessionHelper.CurrentReconciliationPeriodID) == WebEnums.FeatureCapabilityMode.Visible)
                            //if (oCompanyCapabilityInfo.IsActivated.HasValue && oCompanyCapabilityInfo.IsActivated.Value)
                            //{
                            this._IsKeyAccountEnabled = true;
                        //}
                        break;

                    case ARTEnums.Capability.DualLevelReview:
                        if (Helper.GetFeatureCapabilityMode(WebEnums.Feature.DualLevelReview, ARTEnums.Capability.DualLevelReview, SessionHelper.CurrentReconciliationPeriodID) == WebEnums.FeatureCapabilityMode.Visible)
                        {
                            // if (oCompanyCapabilityInfo.IsActivated.HasValue && oCompanyCapabilityInfo.IsActivated.Value)
                            // {
                            this._IsDualReviewEnabled = true;
                            this._IsDualLevelReviewByAccountEnabled = Helper.IsDualLevelReviewByAccountActivated();
                            // }
                        }
                        break;

                    case ARTEnums.Capability.MaterialitySelection:
                        if (oCompanyCapabilityInfo.IsActivated.HasValue && oCompanyCapabilityInfo.IsActivated.Value)
                        {
                            this._IsMaterialityEnabled = true;
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
                }
            }
        }
    }

    private void DisableControls()
    {
        if (!this._IsRiskRatingEnabled)
        {
            lblRiskRating.LabelID = 1427;
            ddlRiskRating.Visible = false;
            ddlRiskRating.ValidatorEnable = false;
            ucRecFrequencySelection.Visible = true;
            ucRiskRatingSelection.Visible = false;
        }
        else
        {
            lblRiskRating.LabelID = 1013;
            ddlRiskRating.Visible = true;
            ddlRiskRating.ValidatorEnable = true;
            ucRecFrequencySelection.Visible = false;
            ucRiskRatingSelection.Visible = true;
        }

        if (!this._IsKeyAccountEnabled)
        {
            optIsKeyAccountNo.Enabled = false;
            optIsKeyAccountYes.Enabled = false;
        }

        if (!this._IsZeroBalanceEnabled)
        {
            optZeroBalanceAccountNo.Enabled = false;
            optZeroBalanceAccountYes.Enabled = false;
        }

        txtPreparerDueDays.Enabled = this._IsDueDateByAccountEnabled;
        rfvPreparerDueDays.Enabled = this._IsDueDateByAccountEnabled;
        cvPreparerDueDays.Enabled = this._IsDueDateByAccountEnabled;
        txtReviewerDueDays.Enabled = this._IsDueDateByAccountEnabled;
        rfvReviewerDueDays.Enabled = this._IsDueDateByAccountEnabled;
        cvReviewerDueDays.Enabled = this._IsDueDateByAccountEnabled;
        txtApproverDueDays.Enabled = this._IsDueDateByAccountEnabled && this._IsDualReviewEnabled;
        rfvApproverDueDays.Enabled = this._IsDueDateByAccountEnabled && this._IsDualReviewEnabled && !this._IsDualLevelReviewByAccountEnabled;
        cvApproverDueDays.Enabled = this._IsDueDateByAccountEnabled && this._IsDualReviewEnabled;
        //ddlDayType.Enabled = this._IsDueDateByAccountEnabled && this._IsDualReviewEnabled;
        //ddlDayType.ValidatorEnable = this._IsDueDateByAccountEnabled && this._IsDualReviewEnabled;
        if (!this._IsDualReviewEnabled)
        {
            ddlApprover.Enabled = false;
            //  ddlApprover.ValidatorEnable = false;
            ddlBackupApprover.Enabled = false;
            ddlBackupApprover.ValidatorEnable = false;
        }
        else
        {
            ddlApprover.Enabled = true;
            //ddlApprover.ValidatorEnable = true;
            ddlBackupApprover.Enabled = true;
            ddlBackupApprover.ValidatorEnable = false;
        }
        ddlApprover.ValidatorEnable = this._IsDueDateByAccountEnabled && this._IsDualReviewEnabled && !this._IsDualLevelReviewByAccountEnabled;
        //  ddlBackupApprover.ValidatorEnable = this._IsDueDateByAccountEnabled && this._IsDualReviewEnabled && !this._IsDualLevelReviewByAccountEnabled;
        //Reconcilable
        this.optIsReconcilableYes.Enabled = this._IsReconcilableEnabled;
        this.optIsReconcilableNo.Enabled = this._IsReconcilableEnabled;
        if (Helper.IsFeatureActivated(WebEnums.Feature.AccountOwnershipBackup, SessionHelper.CurrentReconciliationPeriodID))
        {
            trBackupPreparerLabel.Visible = true;
            trBackupReviewerDropdown.Visible = true;
            trBackupReviewerLabel.Visible = true;
            trBackupReviewerDropdown.Visible = true;
            trBackupApproverLabel.Visible = true;
            trBackupApproverDropdown.Visible = true;
        }
        else
        {
            trBackupPreparerLabel.Visible = false;
            trBackupReviewerDropdown.Visible = false;
            trBackupReviewerLabel.Visible = false;
            trBackupReviewerDropdown.Visible = false;
            trBackupApproverLabel.Visible = false;
            trBackupApproverDropdown.Visible = false;
        }

        if (Helper.IsFeatureActivated(WebEnums.Feature.DueDateByAccount, SessionHelper.CurrentReconciliationPeriodID))
        {
            trPreparerDueDays.Visible = true;
            trApproverDueDays.Visible = true;
        }
        else
        {
            trPreparerDueDays.Visible = false;
            trApproverDueDays.Visible = false;
        }

        DisableControlsForCurrentRecPeriodStatus();
    }

    private void DisableControlsForCurrentRecPeriodStatus()
    {
        if (_AccountHdrInfo.IsLocked.GetValueOrDefault()
            || CurrentRecProcessStatus.Value == WebEnums.RecPeriodStatus.Closed
            || CurrentRecProcessStatus.Value == WebEnums.RecPeriodStatus.Skipped
            || CertificationHelper.IsCertificationStarted())
        {
            btnSave.Visible = false;
            ddlRiskRating.Enabled = false;
            optIsKeyAccountNo.Enabled = false;
            optIsKeyAccountYes.Enabled = false;
            optZeroBalanceAccountNo.Enabled = false;
            optZeroBalanceAccountYes.Enabled = false;
            this.optIsReconcilableYes.Enabled = false;//Reconcilable
            this.optIsReconcilableNo.Enabled = false;//Reconcilable
            ddlApprover.Enabled = false;
            ucAccountPolicyURL.EditorControl.EditModes = EditModes.Preview;
            ucDescription.EditorControl.EditModes = EditModes.Preview;
            ucReconciliationProcedure.EditorControl.EditModes = EditModes.Preview;
            ddlSubledgerSource.Enabled = false;
            ddlReviewer.Enabled = false;
            ddlReconciliationTemplate.Enabled = false;
            ddlPreparer.Enabled = false;
            ddlBackupPreparer.Enabled = false;
            ddlBackupReviewer.Enabled = false;
            ddlBackupApprover.Enabled = false;
            txtPreparerDueDays.Enabled = false;
            txtReviewerDueDays.Enabled = false;
            txtApproverDueDays.Enabled = false;
            //ddlDayType.Enabled = false;
        }
        else
        {
            ddlReconciliationTemplate.Enabled = true;
            btnSave.Visible = true;

            if (this._IsRiskRatingEnabled)
                ddlRiskRating.Enabled = true;
            if (this._IsKeyAccountEnabled)
            {
                optIsKeyAccountNo.Enabled = true;
                optIsKeyAccountYes.Enabled = true;
            }
            if (this._IsZeroBalanceEnabled)
            {
                optZeroBalanceAccountNo.Enabled = true;
                optZeroBalanceAccountYes.Enabled = true;
            }
            this.optIsReconcilableYes.Enabled = true;//Reconcilable
            this.optIsReconcilableNo.Enabled = true;//Reconcilable

            if (this._IsDualReviewEnabled)
                ddlApprover.Enabled = true;
            ucAccountPolicyURL.EditorControl.EditModes = EditModes.Design;
            ucDescription.EditorControl.EditModes = EditModes.Design;
            ucReconciliationProcedure.EditorControl.EditModes = EditModes.Design;
            ddlSubledgerSource.Enabled = true;
            ddlReviewer.Enabled = true;
            ddlPreparer.Enabled = true;

            if (this._IsDueDateByAccountEnabled)
            {
                if (this._AccountHdrInfo.ReconciliationStatusID == (short)WebEnums.ReconciliationStatus.Reconciled
                    || this._AccountHdrInfo.ReconciliationStatusID == (short)WebEnums.ReconciliationStatus.SysReconciled)
                {
                    txtPreparerDueDays.Enabled = false;
                    txtReviewerDueDays.Enabled = false;
                    txtApproverDueDays.Enabled = false;
                    //ddlDayType.Enabled = false;
                }
                else
                {
                    txtPreparerDueDays.Enabled = true;
                    txtReviewerDueDays.Enabled = true;
                    if (this._IsDualReviewEnabled)
                        txtApproverDueDays.Enabled = true;
                    //ddlDayType.Enabled = true;
                }
            }
        }
    }
    private void SetErrorMessagesForValidationControls()
    {
        vldKeyAccount.ErrorMessage = LanguageUtil.GetValue(5000063);
        vldZeroBalance.ErrorMessage = LanguageUtil.GetValue(5000064);
        vldSubledgerSource.ErrorMessage = LanguageUtil.GetValue(5000065);
        vldRecFrequency.ErrorMessage = LanguageUtil.GetValue(5000052);
        rfvPreparerDueDays.ErrorMessage = Helper.GetErrorMessage(WebEnums.FieldType.MandatoryField, lblPreparerDueDays.LabelID);
        rfvReviewerDueDays.ErrorMessage = Helper.GetErrorMessage(WebEnums.FieldType.MandatoryField, lblReviewerDueDays.LabelID);
        rfvApproverDueDays.ErrorMessage = Helper.GetErrorMessage(WebEnums.FieldType.MandatoryField, lblApproverDueDays.LabelID);
    }

    private void RaiseAlertIfOwnershipChanged(AccountHdrInfo oAccountHdrInfo)
    {
        bool isAttributesMismatch = false;
        List<int> removedPreparerIDCollection = new List<int>();
        List<int> assignedPreparerIDCollection = new List<int>();

        Dictionary<int, List<AccountHdrInfo>> removedPreparerAccounts = new Dictionary<int, List<AccountHdrInfo>>();
        Dictionary<int, List<AccountHdrInfo>> assignedPreparerAccounts = new Dictionary<int, List<AccountHdrInfo>>();

        List<int> removedReviewerIDCollection = new List<int>();
        List<int> assignedReviewerIDCollection = new List<int>();

        Dictionary<int, List<AccountHdrInfo>> removedReviewerAccounts = new Dictionary<int, List<AccountHdrInfo>>();
        Dictionary<int, List<AccountHdrInfo>> assignedReviewerAccounts = new Dictionary<int, List<AccountHdrInfo>>();

        List<int> removedApproverIDCollection = new List<int>();
        List<int> assignedApproverIDCollection = new List<int>();

        Dictionary<int, List<AccountHdrInfo>> removedApproverAccounts = new Dictionary<int, List<AccountHdrInfo>>();
        Dictionary<int, List<AccountHdrInfo>> assignedApproverAccounts = new Dictionary<int, List<AccountHdrInfo>>();

        List<int> removedBackupPreparerIDCollection = new List<int>();
        List<int> assignedBackupPreparerIDCollection = new List<int>();

        Dictionary<int, List<AccountHdrInfo>> removedBackupPreparerAccounts = new Dictionary<int, List<AccountHdrInfo>>();
        Dictionary<int, List<AccountHdrInfo>> assignedBackupPreparerAccounts = new Dictionary<int, List<AccountHdrInfo>>();

        List<int> removedBackupReviewerIDCollection = new List<int>();
        List<int> assignedBackupReviewerIDCollection = new List<int>();

        Dictionary<int, List<AccountHdrInfo>> removedBackupReviewerAccounts = new Dictionary<int, List<AccountHdrInfo>>();
        Dictionary<int, List<AccountHdrInfo>> assignedBackupReviewerAccounts = new Dictionary<int, List<AccountHdrInfo>>();

        List<int> removedBackupApproverIDCollection = new List<int>();
        List<int> assignedBackupApproverIDCollection = new List<int>();

        Dictionary<int, List<AccountHdrInfo>> removedBackupApproverAccounts = new Dictionary<int, List<AccountHdrInfo>>();
        Dictionary<int, List<AccountHdrInfo>> assignedBackupApproverAccounts = new Dictionary<int, List<AccountHdrInfo>>();

        List<long> oAccountIDCollection = new List<long>();
        oAccountIDCollection.Add(oAccountHdrInfo.AccountID.Value);


        if (oAccountHdrInfo.PreparerUserID != this._AccountHdrInfo.PreparerUserID)
        {
            isAttributesMismatch = true;
            if (this._AccountHdrInfo.PreparerUserID.HasValue && this._AccountHdrInfo.PreparerUserID.Value > 0)
            {
                removedPreparerIDCollection.Add(this._AccountHdrInfo.PreparerUserID.Value);
                AddUserAccountToDictionary(removedPreparerAccounts, _AccountHdrInfo.PreparerUserID.Value, _AccountHdrInfo);
            }
            if (oAccountHdrInfo.PreparerUserID.HasValue && oAccountHdrInfo.PreparerUserID.Value > 0)
            {
                assignedPreparerIDCollection.Add(oAccountHdrInfo.PreparerUserID.Value);
                AddUserAccountToDictionary(assignedPreparerAccounts, oAccountHdrInfo.PreparerUserID.Value, _AccountHdrInfo);
            }
        }

        if (this._IsDualReviewEnabled && oAccountHdrInfo.ApproverUserID != this._AccountHdrInfo.ApproverUserID)
        {
            isAttributesMismatch = true;
            if (this._AccountHdrInfo.ApproverUserID.HasValue && this._AccountHdrInfo.ApproverUserID.Value > 0)
            {
                removedApproverIDCollection.Add(this._AccountHdrInfo.ApproverUserID.Value);
                AddUserAccountToDictionary(removedApproverAccounts, _AccountHdrInfo.ApproverUserID.Value, _AccountHdrInfo);
            }
            if (oAccountHdrInfo.ApproverUserID.HasValue && oAccountHdrInfo.ApproverUserID.Value > 0)
            {
                assignedApproverIDCollection.Add(oAccountHdrInfo.ApproverUserID.Value);
                AddUserAccountToDictionary(assignedApproverAccounts, oAccountHdrInfo.ApproverUserID.Value, _AccountHdrInfo);
            }
        }

        if (oAccountHdrInfo.ReviewerUserID != this._AccountHdrInfo.ReviewerUserID)
        {
            isAttributesMismatch = true;
            if (this._AccountHdrInfo.ReviewerUserID.HasValue && this._AccountHdrInfo.ReviewerUserID.Value > 0)
            {
                removedReviewerIDCollection.Add(this._AccountHdrInfo.ReviewerUserID.Value);
                AddUserAccountToDictionary(removedReviewerAccounts, _AccountHdrInfo.ReviewerUserID.Value, _AccountHdrInfo);
            }
            if (oAccountHdrInfo.ReviewerUserID.HasValue && oAccountHdrInfo.ReviewerUserID.Value > 0)
            {
                assignedReviewerIDCollection.Add(oAccountHdrInfo.ReviewerUserID.Value);
                AddUserAccountToDictionary(assignedReviewerAccounts, oAccountHdrInfo.ReviewerUserID.Value, _AccountHdrInfo);
            }
        }

        #region Backup Section
        if (oAccountHdrInfo.BackupPreparerUserID != this._AccountHdrInfo.BackupPreparerUserID)
        {
            isAttributesMismatch = true;
            if (this._AccountHdrInfo.BackupPreparerUserID.HasValue && this._AccountHdrInfo.BackupPreparerUserID.Value > 0)
            {
                removedBackupPreparerIDCollection.Add(this._AccountHdrInfo.BackupPreparerUserID.Value);
                AddUserAccountToDictionary(removedBackupPreparerAccounts, _AccountHdrInfo.BackupPreparerUserID.Value, _AccountHdrInfo);
            }
            if (oAccountHdrInfo.BackupPreparerUserID.HasValue && oAccountHdrInfo.BackupPreparerUserID.Value > 0)
            {
                assignedBackupPreparerIDCollection.Add(oAccountHdrInfo.BackupPreparerUserID.Value);
                AddUserAccountToDictionary(assignedBackupPreparerAccounts, oAccountHdrInfo.BackupPreparerUserID.Value, _AccountHdrInfo);
            }
        }

        if (this._IsDualReviewEnabled && oAccountHdrInfo.BackupApproverUserID != this._AccountHdrInfo.BackupApproverUserID)
        {
            isAttributesMismatch = true;
            if (this._AccountHdrInfo.BackupApproverUserID.HasValue && this._AccountHdrInfo.BackupApproverUserID.Value > 0)
            {
                removedBackupApproverIDCollection.Add(this._AccountHdrInfo.BackupApproverUserID.Value);
                AddUserAccountToDictionary(removedBackupApproverAccounts, _AccountHdrInfo.BackupApproverUserID.Value, _AccountHdrInfo);
            }
            if (oAccountHdrInfo.BackupApproverUserID.HasValue && oAccountHdrInfo.BackupApproverUserID.Value > 0)
            {
                assignedBackupApproverIDCollection.Add(oAccountHdrInfo.BackupApproverUserID.Value);
                AddUserAccountToDictionary(assignedBackupApproverAccounts, oAccountHdrInfo.BackupApproverUserID.Value, _AccountHdrInfo);
            }
        }

        if (oAccountHdrInfo.BackupReviewerUserID != this._AccountHdrInfo.BackupReviewerUserID)
        {
            isAttributesMismatch = true;
            if (this._AccountHdrInfo.BackupReviewerUserID.HasValue && this._AccountHdrInfo.BackupReviewerUserID.Value > 0)
            {
                removedBackupReviewerIDCollection.Add(this._AccountHdrInfo.BackupReviewerUserID.Value);
                AddUserAccountToDictionary(removedBackupReviewerAccounts, _AccountHdrInfo.BackupReviewerUserID.Value, _AccountHdrInfo);
            }
            if (oAccountHdrInfo.BackupReviewerUserID.HasValue && oAccountHdrInfo.BackupReviewerUserID.Value > 0)
            {
                assignedBackupReviewerIDCollection.Add(oAccountHdrInfo.BackupReviewerUserID.Value);
                AddUserAccountToDictionary(assignedBackupReviewerAccounts, oAccountHdrInfo.BackupReviewerUserID.Value, _AccountHdrInfo);
            }
        }

        // Remove InActive USERS
        short CurrentRoleID = SessionHelper.CurrentRoleID.Value;
        CurrentRoleID = 2;// fetch all record 
        IUser oUserClient = RemotingHelper.GetUserObject();
        List<UserHdrInfo> oInActiveUserCollection = oUserClient.SearchUser(null, null, null, 0, null, false, SessionHelper.CurrentCompanyID,
            SessionHelper.CurrentUserID.Value, CurrentRoleID, SessionHelper.CurrentReconciliationPeriodID,
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

        #endregion

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

    private void RaiseAlertIfAttributesHaveChanged(AccountHdrInfo oAccountHdrInfo)
    {
        bool isAttributesMismatch = false;

        if (oAccountHdrInfo.AccountPolicyUrl != this._AccountHdrInfo.AccountPolicyUrl
            || oAccountHdrInfo.Description != this._AccountHdrInfo.Description
            || (oAccountHdrInfo.IsKeyAccount != null && (oAccountHdrInfo.IsKeyAccount != this._AccountHdrInfo.IsKeyAccount))
            || (oAccountHdrInfo.IsZeroBalance != null && (oAccountHdrInfo.IsZeroBalance != this._AccountHdrInfo.IsZeroBalance))
            || oAccountHdrInfo.NatureOfAccount != this._AccountHdrInfo.NatureOfAccount
            || oAccountHdrInfo.ReconciliationProcedure != this._AccountHdrInfo.ReconciliationProcedure
            || (oAccountHdrInfo.ReconciliationTemplateID != null && (oAccountHdrInfo.ReconciliationTemplateID != this._AccountHdrInfo.ReconciliationTemplateID))
            || (oAccountHdrInfo.SubLedgerSourceID != null && (oAccountHdrInfo.SubLedgerSourceID != this._AccountHdrInfo.SubLedgerSourceID))
            || (this._IsRiskRatingEnabled && oAccountHdrInfo.SubLedgerSourceID != null && (oAccountHdrInfo.RiskRatingID != this._AccountHdrInfo.RiskRatingID))
            || (this._IsReconcilableEnabled && (oAccountHdrInfo.IsReconcilable != this._AccountHdrInfo.IsReconcilable))
            || (this._IsDueDateByAccountEnabled && (oAccountHdrInfo.PreparerDueDays != this._AccountHdrInfo.PreparerDueDays))
            || (this._IsDueDateByAccountEnabled && (oAccountHdrInfo.ReviewerDueDays != this._AccountHdrInfo.ReviewerDueDays))
            || (this._IsDueDateByAccountEnabled && (oAccountHdrInfo.ApproverDueDays != this._AccountHdrInfo.ApproverDueDays)))
        {
            isAttributesMismatch = true;
        }
        else if (!this._IsRiskRatingEnabled)
        {
            foreach (int recPeriodID in oAccountHdrInfo.RecPeriodIDCollection)
            {
                AccountReconciliationPeriodInfo oAccountReconciliationPeriodInfo = this._AccountReconciliationPeriodInfoCollection.Find(AR => AR.ReconciliationPeriodID == recPeriodID);

                if (oAccountReconciliationPeriodInfo == null)
                {
                    isAttributesMismatch = true;
                    break;
                }
            }
        }

        if (isAttributesMismatch)
        {
            List<long> oAccountIDCollection = new List<long>();
            oAccountIDCollection.Add(oAccountHdrInfo.AccountID.Value);

            //Raise Alert with account detail
            List<AccountHdrInfo> oListAccountHdrInfo = new List<AccountHdrInfo>();
            oListAccountHdrInfo.Add(this._AccountHdrInfo);
            oListAccountHdrInfo = LanguageHelper.TranslateLabelsAccountHdr(oListAccountHdrInfo);
            AlertHelper.RaiseAlert(WebEnums.Alert.YouHaveXAccountsWhichHaveAttributesThatHaveChanged,
                SessionHelper.CurrentReconciliationPeriodID, SessionHelper.CurrentReconciliationPeriodEndDate, oAccountIDCollection,
                null, SessionHelper.CurrentRoleID.Value, oListAccountHdrInfo);
        }
    }

    private List<int> GetSelectedRecPeriods()
    {
        List<int> oRecPeriodIdCollection = new List<int>();
        string[] recPeriodIDCollection = txtRecPeriodIDContainer.Value.Split(';');

        foreach (string recPeriodID in recPeriodIDCollection)
        {
            if (!string.IsNullOrEmpty(recPeriodID))
                oRecPeriodIdCollection.Add(Convert.ToInt32(recPeriodID));
        }

        return oRecPeriodIdCollection;
    }

    private AccountHdrInfo GetAccountInformation()
    {
        int? lableID = null;
        AccountHdrInfo oAccountHdrInfo = new AccountHdrInfo
        {
            AccountID = this._AccountID,
            AccountPolicyUrl = this.ucAccountPolicyURL.EditorControl.Content,
            //AccountTypeID = Convert.ToInt16(ddlAccountType.SelectedValue),
            Description = this.ucDescription.EditorControl.Content,
            ReconciliationProcedure = this.ucReconciliationProcedure.EditorControl.Content,

        };

        // Check if either labelid is 0 or label text changed from earlier value than create new label instead of modify older one
        lableID = Convert.ToInt32(ucAccountPolicyURL.EditorControl.Attributes[WebConstants.ATTRIBUTE_LABEL_ID]);
        if (lableID != null && lableID.HasValue && !LanguageUtil.GetValue(lableID.Value).Equals(this.ucAccountPolicyURL.EditorControl.Content, StringComparison.InvariantCultureIgnoreCase))
            lableID = 0;
        oAccountHdrInfo.AccountPolicyUrlLabelID = LanguageUtil.InsertPhrase(oAccountHdrInfo.AccountPolicyUrl, null, AppSettingHelper.GetApplicationID(), SessionHelper.CurrentCompanyID.Value, SessionHelper.GetUserLanguage(), 4, lableID);

        lableID = Convert.ToInt32(ucDescription.EditorControl.Attributes[WebConstants.ATTRIBUTE_LABEL_ID]);
        if (lableID != null && lableID.HasValue && !LanguageUtil.GetValue(lableID.Value).Equals(this.ucDescription.EditorControl.Content, StringComparison.InvariantCultureIgnoreCase))
            lableID = 0;
        oAccountHdrInfo.DescriptionLabelID = LanguageUtil.InsertPhrase(oAccountHdrInfo.Description, null, AppSettingHelper.GetApplicationID(), SessionHelper.CurrentCompanyID.Value, SessionHelper.GetUserLanguage(), 4, lableID);

        lableID = Convert.ToInt32(ucReconciliationProcedure.EditorControl.Attributes[WebConstants.ATTRIBUTE_LABEL_ID]);
        if (lableID != null && lableID.HasValue && !LanguageUtil.GetValue(lableID.Value).Equals(this.ucReconciliationProcedure.EditorControl.Content, StringComparison.InvariantCultureIgnoreCase))
            lableID = 0;
        oAccountHdrInfo.ReconciliationProcedureLabelID = LanguageUtil.InsertPhrase(oAccountHdrInfo.ReconciliationProcedure, null, AppSettingHelper.GetApplicationID(), SessionHelper.CurrentCompanyID.Value, SessionHelper.GetUserLanguage(), 4, lableID);


        if (this.ddlPreparer.SelectedValue != WebConstants.SELECT_ONE)
        {
            oAccountHdrInfo.PreparerUserID = Convert.ToInt32(ddlPreparer.SelectedValue);
        }
        else
        {
            oAccountHdrInfo.PreparerUserID = null;
        }


        if (this.ddlReviewer.SelectedValue != WebConstants.SELECT_ONE)
        {
            oAccountHdrInfo.ReviewerUserID = Convert.ToInt32(ddlReviewer.SelectedValue);
        }

        else
        {
            oAccountHdrInfo.ReviewerUserID = null;
        }

        #region BackupRoles

        if (this.ddlBackupPreparer.SelectedValue != WebConstants.SELECT_ONE)
        {
            oAccountHdrInfo.BackupPreparerUserID = Convert.ToInt32(ddlBackupPreparer.SelectedValue);
        }
        else
        {
            oAccountHdrInfo.BackupPreparerUserID = null;
        }


        if (this.ddlBackupReviewer.SelectedValue != WebConstants.SELECT_ONE)
        {
            oAccountHdrInfo.BackupReviewerUserID = Convert.ToInt32(ddlBackupReviewer.SelectedValue);
        }

        else
        {
            oAccountHdrInfo.BackupReviewerUserID = null;

        }

        #endregion

        if (this._IsDualReviewEnabled)
        {

            if (this.ddlApprover.SelectedValue != WebConstants.SELECT_ONE)
            {
                oAccountHdrInfo.ApproverUserID = Convert.ToInt32(ddlApprover.SelectedValue);
            }
            else
            {
                oAccountHdrInfo.ApproverUserID = null;
            }

            if (this.ddlBackupApprover.SelectedValue != WebConstants.SELECT_ONE)
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



        if (this.ddlReconciliationTemplate.SelectedValue != WebConstants.SELECT_ONE)
        {
            oAccountHdrInfo.ReconciliationTemplateID = Convert.ToInt16(ddlReconciliationTemplate.SelectedValue);
        }

        else
        {
            oAccountHdrInfo.ReconciliationTemplateID = null;

        }

        if (this._IsKeyAccountEnabled)
        {
            if (optIsKeyAccountYes.Checked)
            {
                oAccountHdrInfo.IsKeyAccount = true;
            }
            else if (optIsKeyAccountNo.Checked)
            {
                oAccountHdrInfo.IsKeyAccount = false;

            }
            else
            {
                oAccountHdrInfo.IsKeyAccount = null;

            }

        }
        else
        {
            oAccountHdrInfo.IsKeyAccount = null;
        }

        if (this._IsRiskRatingEnabled)
        {
            if (this.ddlRiskRating.SelectedValue != WebConstants.SELECT_ONE)
            {
                oAccountHdrInfo.RiskRatingID = Convert.ToInt16(ddlRiskRating.SelectedValue);
            }
            else
            {
                oAccountHdrInfo.RiskRatingID = null;

            }
        }
        else
        {
            oAccountHdrInfo.RiskRatingID = null;
        }

        if (this._IsZeroBalanceEnabled)
        {
            if (optZeroBalanceAccountYes.Checked)
            {
                oAccountHdrInfo.IsZeroBalance = true;

            }
            else if (optZeroBalanceAccountNo.Checked)
            {
                oAccountHdrInfo.IsZeroBalance = false;

            }
            else
            {
                oAccountHdrInfo.IsZeroBalance = null;

            }

        }
        else
        {
            oAccountHdrInfo.IsZeroBalance = null;
        }

        if (this.ddlSubledgerSource.SelectedValue != WebConstants.SELECT_ONE)
        {
            oAccountHdrInfo.SubLedgerSourceID = Convert.ToInt32(ddlSubledgerSource.SelectedValue);
        }
        else
        {
            oAccountHdrInfo.SubLedgerSourceID = null;
        }

        if (this._IsDualReviewEnabled)
        {

            if (oAccountHdrInfo.PreparerUserID != null && oAccountHdrInfo.ReviewerUserID != null && oAccountHdrInfo.ApproverUserID != null)
            {

                if (!ValidatePRAsAndBackupPRAs(oAccountHdrInfo))
                {
                    throw new ARTException(5000086);
                }
            }
        }
        else
        {
            if (oAccountHdrInfo.PreparerUserID != null && oAccountHdrInfo.ReviewerUserID != null)
            {
                if (!ValidatePRAsAndBackupPRAs(oAccountHdrInfo))
                {
                    throw new ARTException(5000086);
                }
            }
        }

        //Reconcilable
        if (this._IsReconcilableEnabled)
        {
            if (this.optIsReconcilableYes.Checked)
            {
                oAccountHdrInfo.IsReconcilable = true;
            }
            else if (this.optIsReconcilableNo.Checked)
            {
                oAccountHdrInfo.IsReconcilable = false;
            }
        }

        // Due Date by Account
        if (!this._IsDueDateByAccountEnabled)
        {
            oAccountHdrInfo.PreparerDueDays = null;
            oAccountHdrInfo.ReviewerDueDays = null;
            oAccountHdrInfo.ApproverDueDays = null;
            //oAccountHdrInfo.DayType = null;
        }
        else
        {
            oAccountHdrInfo.PreparerDueDays = Int32.Parse(txtPreparerDueDays.Text);
            oAccountHdrInfo.ReviewerDueDays = Int32.Parse(txtReviewerDueDays.Text);
            if (this._IsDualReviewEnabled && !string.IsNullOrEmpty(txtApproverDueDays.Text))
                oAccountHdrInfo.ApproverDueDays = Int32.Parse(txtApproverDueDays.Text);
            else
                oAccountHdrInfo.ApproverDueDays = null;
            //if (!string.IsNullOrEmpty(ddlDayType.SelectedValue) && ddlDayType.SelectedValue != WebConstants.SELECT_ONE)
            //    oAccountHdrInfo.DayTypeID = Int16.Parse(ddlDayType.SelectedValue);
        }

        return oAccountHdrInfo;
    }

    private bool ValidatePRAsAndBackupPRAs(AccountHdrInfo oAccountHdr)
    {
        List<int> accountOwnershipAttributes = new List<int>();

        //Preparer
        if (oAccountHdr.PreparerUserID != null)
        {
            if (accountOwnershipAttributes.Contains((int)oAccountHdr.PreparerUserID))
            {
                return false;
            }
            else
            {
                accountOwnershipAttributes.Add((int)oAccountHdr.PreparerUserID);
            }
        }

        //Reviewer
        if (oAccountHdr.ReviewerUserID != null)
        {
            if (accountOwnershipAttributes.Contains((int)oAccountHdr.ReviewerUserID))
            {
                return false;
            }
            else
            {
                accountOwnershipAttributes.Add((int)oAccountHdr.ReviewerUserID);
            }
        }

        //Approver
        if (oAccountHdr.ApproverUserID != null)
        {
            if (accountOwnershipAttributes.Contains((int)oAccountHdr.ApproverUserID))
            {
                return false;
            }
            else
            {
                accountOwnershipAttributes.Add((int)oAccountHdr.ApproverUserID);
            }
        }

        //Backup Preparer
        if (oAccountHdr.BackupPreparerUserID != null)
        {
            if (accountOwnershipAttributes.Contains((int)oAccountHdr.BackupPreparerUserID))
            {
                return false;
            }
            else
            {
                accountOwnershipAttributes.Add((int)oAccountHdr.BackupPreparerUserID);
            }
        }

        //Backup Reviewer
        if (oAccountHdr.BackupReviewerUserID != null)
        {
            if (accountOwnershipAttributes.Contains((int)oAccountHdr.BackupReviewerUserID))
            {
                return false;
            }
            else
            {
                accountOwnershipAttributes.Add((int)oAccountHdr.BackupReviewerUserID);
            }
        }

        //Backup Approver
        if (oAccountHdr.BackupApproverUserID != null)
        {
            if (accountOwnershipAttributes.Contains((int)oAccountHdr.BackupApproverUserID))
            {
                return false;
            }
            else
            {
                accountOwnershipAttributes.Add((int)oAccountHdr.BackupApproverUserID);
            }
        }

        return true;
    }

    private void ValidateInput()
    {
        string errorMessage = null;
        if (this._IsKeyAccountEnabled)
        {
            if (optIsKeyAccountNo.Checked == false && optIsKeyAccountYes.Checked == false)
            {
                errorMessage = LanguageUtil.GetValue(5000063);
            }
        }

        if (this._IsZeroBalanceEnabled)
        {
            if (optZeroBalanceAccountNo.Checked == false && optZeroBalanceAccountYes.Checked == false)
            {
                if (errorMessage == null)
                {
                    errorMessage = LanguageUtil.GetValue(5000064);
                }
                else
                {
                    errorMessage = errorMessage + "\r\n<li>" + LanguageUtil.GetValue(5000064);
                }
            }
        }

        if (Convert.ToInt32(ddlReconciliationTemplate.SelectedValue) == (int)ARTEnums.ReconciliationItemTemplate.Subledgerform)
        {
            if (ddlSubledgerSource.SelectedValue == WebConstants.SELECT_ONE)
            {
                if (errorMessage == null)
                {
                    errorMessage = LanguageUtil.GetValue(5000065);
                }
                else
                {
                    errorMessage = errorMessage + "\r\n<li>" + LanguageUtil.GetValue(5000065);
                }
            }
        }

        if (errorMessage != null)
        {
            throw new Exception(errorMessage);
        }
    }

    private void DisableNetAccountAttributes()
    {
        if (this._AccountHdrInfo != null && this._AccountHdrInfo.NetAccountID.HasValue && this._AccountHdrInfo.NetAccountID.Value > 0)
        {
            optIsKeyAccountNo.Enabled = false;
            optIsKeyAccountYes.Enabled = false;
            optZeroBalanceAccountNo.Enabled = false;
            optZeroBalanceAccountYes.Enabled = false;
            optIsReconcilableYes.Enabled = false;
            optIsReconcilableNo.Enabled = false;


            if (ddlRiskRating.Enabled == true)
            {
                ddlRiskRating.ValidatorEnable = false;
                ddlRiskRating.Enabled = false;
            }

            if (ddlPreparer.Enabled == true)
            {
                ddlPreparer.ValidatorEnable = false;
                ddlPreparer.Enabled = false;
            }

            if (ddlBackupPreparer.Enabled == true)
            {
                ddlBackupPreparer.ValidatorEnable = false;
                ddlBackupPreparer.Enabled = false;
            }

            if (ddlReviewer.Enabled == true)
            {
                ddlReviewer.ValidatorEnable = false;
                ddlReviewer.Enabled = false;
            }

            if (ddlBackupReviewer.Enabled == true)
            {
                ddlBackupReviewer.ValidatorEnable = false;
                ddlBackupReviewer.Enabled = false;
            }

            if (ddlApprover.Enabled == true)
            {
                ddlApprover.ValidatorEnable = false;
                ddlApprover.Enabled = false;
            }

            if (ddlBackupApprover.Enabled == true)
            {
                ddlBackupApprover.ValidatorEnable = false;
                ddlBackupApprover.Enabled = false;
            }

            if (ddlReconciliationTemplate.Enabled == true)
            {
                ddlReconciliationTemplate.ValidatorEnable = false;
                ddlReconciliationTemplate.Enabled = false;
            }

            if (txtPreparerDueDays.Enabled == true)
            {
                txtPreparerDueDays.Enabled = false;
            }

            if (txtReviewerDueDays.Enabled == true)
            {
                txtReviewerDueDays.Enabled = false;
            }

            if (txtApproverDueDays.Enabled == true)
            {
                txtApproverDueDays.Enabled = false;
            }
            //if (ddlDayType.Enabled == true)
            //{
            //    ddlDayType.Enabled = false;
            //    ddlDayType.ValidatorEnable = false;
            //}
        }
    }

    /// <summary>
    /// Sets the page settings.
    /// </summary>
    private void SetPageSettings()
    {
        Helper.SetPageTitle(this, 1507);
        MasterPageBase oMasterPageBase = (MasterPageBase)this.Master;
        MasterPageSettings oMasterPageSettings = new MasterPageSettings();
        oMasterPageSettings.HideValidationSummary = true;
        oMasterPageBase.SetMasterPageSettings(oMasterPageSettings);
    }

    #endregion

    #region Other Methods
    /// <summary>
    /// Gets menu key
    /// </summary>
    /// <returns>menu key</returns>
    public override string GetMenuKey()
    {
        return "AccountProfile";
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
                    msg = LanguageUtil.GetValue(1546) + ": " + LanguageUtil.GetValue(2726);
                }
                if (oAccountAttributeWarningInfo.LossOfWorkWarning.Value)
                {
                    if (msg == null)
                    {
                        msg = LanguageUtil.GetValue(1546) + ": " + LanguageUtil.GetValue(2727);
                    }
                    else
                    {
                        msg = msg + "\\n" + LanguageUtil.GetValue(1546) + ": " + LanguageUtil.GetValue(2727);
                    }
                }
                if (msg != null)
                    msg = msg + "\\n" + LanguageUtil.GetValue(2221);

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "newWindow1", "ConfirmNetAccount('" + btnSave.ClientID + "','" + hdnConfirm.ClientID + "','" + msg + "');", true);
            }
            else
            {
                hdnConfirm.Value = "Yes";
            }
        }
    }
    #endregion

    protected void txtApproverDueDays_TextChanged(object sender, EventArgs e)
    {
        if (txtApproverDueDays.Text != "")
        {
            if (ddlApprover.SelectedValue == "-2")
            {
                ddlApprover.ValidatorEnable = true;
            }
            else
            {
                ddlApprover.ValidatorEnable = false;
            }
        }
        else
        {
            if (ddlApprover.SelectedValue != "-2")
            {
                ddlApprover.ValidatorEnable = false;
                if (txtApproverDueDays.Text == "")
                {
                    rfvApproverDueDays.Enabled = true;
                }
            }
            else
            {
                ddlApprover.ValidatorEnable = false;
                rfvApproverDueDays.Enabled = false;
            }
        }
    }
}
