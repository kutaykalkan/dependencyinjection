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
using SkyStem.ART.Web.Utility;
using SkyStem.ART.Web.Data;
using System.Text;
using SkyStem.ART.Client.IServices;
using SkyStem.ART.Client.Model;
using System.Collections.Generic;
using SkyStem.Language.LanguageUtility;
using SkyStem.ART.Web.UserControls;
using SkyStem.ART.Client.Data;
using SkyStem.ART.Client.Exception;
using SkyStem.Library.Controls.WebControls;
using SkyStem.Library.Controls.TelerikWebControls;
using SkyStem.ART.Client.Model.RecControlCheckList;


public partial class Pages_TemplateBankAccountForm : PageBaseRecForm
{

    #region Variables & Constants
    const ARTEnums.ReconciliationItemTemplate eReconciliationItemTemplateThisPage = ARTEnums.ReconciliationItemTemplate.BankForm;
    const bool IS_SUPPORTING_DETAIL_ENTRY_ON_TEMPLATE = true;// tells whether sum for SUPPORTING_DETAIL section will come from ItemInput grid or just on entry on the recForm itself
    const int POPUP_WIDTH = 800;
    const int POPUP_HEIGHT = 480;
    WebEnums.ARTPages _ARTPages = WebEnums.ARTPages.AccountViewer;
    public bool _IsRefreshData = false;
    bool _IsGLDataIDChanged = false;
    #endregion

    #region Properties
    private WebEnums.FormMode EditMode
    {
        get
        {
            return (WebEnums.FormMode)ViewState["EditMode"];
        }
        set
        {
            ViewState["EditMode"] = value;
        }
    }
    private int? PreviousNetAccountId { get; set; }



    #endregion

    #region Delegates & Events
    #endregion

    #region Page Events
    protected void Page_Init(object sender, EventArgs e)
    {
        try
        {
            RecPeriodMasterPageBase oRecPeriodMasterPageBase = (RecPeriodMasterPageBase)this.Master;
            oRecPeriodMasterPageBase.PageTitleLabeID = 1355;
            MasterPageBase ompage = (MasterPageBase)this.Master.Master;
            ompage.ReconciliationPeriodChangedEventHandler += new EventHandler(ompage_ReconciliationPeriodChangedEventHandler);
            ucRecFormButtons.eventButtonClick += new UserControls_RecFormButtons.ButtonClick(ucRecFormButtons_eventButtonClick);
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
    protected void Page_Load(object sender, EventArgs e)
    {

        try
        {
            Session[SessionConstants.REC_ITEM_TEMPLATE_ID] = eReconciliationItemTemplateThisPage;
            OnPageLoad();
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
    protected void Page_PreRender(object sender, EventArgs e)
    {
        try
        {
            Helper.SetBreadcrumbsForRecForms(this, _ARTPages, 1355);
            //*******Added By Prafull on 24-Feb-2011
            SetAttributesForSignOffButton();
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
    #endregion

    #region Other Events
    void imgAccountTask_Click(object sender, ImageClickEventArgs e)
    {
        ucRecFormAccountTaskGrid.IsExpanded = true;
        LoadAccountTask();
        ucRecFormAccountTaskGrid.ContentVisibility = true;
    }

    void imgQualityScore_Click(object sender, ImageClickEventArgs e)
    {
        ucEditQualityScore.IsExpanded = true;
        LoadQualityScore();
        ucEditQualityScore.ContentVisibility = true;
    }
    void imgRecControlCheckList_Click(object sender, ImageClickEventArgs e)
    {
        ucRecControlCheckList.IsExpanded = true;
        LoadRecControlCheckList();
        ucRecControlCheckList.ContentVisibility = true;
    }

    void imgViewUnexplainedVariance_Click(object sender, ImageClickEventArgs e)
    {
        this.uctlUnexplainedVariance.IsExpanded = true;
        this.LoadUnexplainedVariance();
        this.uctlUnexplainedVariance.ContentVisibility = true;
    }

    void imgViewRecWriteOff_Click(object sender, ImageClickEventArgs e)
    {
        uctlItemInputWriteOff.IsExpanded = true;
        this.LoadRecItemsItemWriteOff();
        uctlItemInputWriteOff.ContentVisibility = true;
    }

    void imgViewOtherInTimingDifference_Click(object sender, ImageClickEventArgs e)
    {
        uctlOtherInTimingDifference.IsExpanded = true;
        this.LoadRecItemsGLAdjustment(uctlOtherInTimingDifference, WebEnums.RecCategory.TimingDifference, WebEnums.RecCategoryType.BankAccount_TimingDifference_Other);
        uctlOtherInTimingDifference.ContentVisibility = true;
    }

    void imgViewOutstandingChecks_Click(object sender, ImageClickEventArgs e)
    {
        uctlOutstandingChecks.IsExpanded = true;
        this.LoadRecItemsGLAdjustment(uctlOutstandingChecks, WebEnums.RecCategory.TimingDifference, WebEnums.RecCategoryType.BankAccount_TimingDifference_OutstandingChecks);
        uctlOutstandingChecks.ContentVisibility = true;
    }

    void imgViewDepositInTransit_Click(object sender, ImageClickEventArgs e)
    {
        uctlDepositInTransit.IsExpanded = true;
        this.LoadRecItemsGLAdjustment(uctlDepositInTransit, WebEnums.RecCategory.TimingDifference, WebEnums.RecCategoryType.BankAccount_TimingDifference_DepositsInTransit);
        uctlDepositInTransit.ContentVisibility = true;
    }

    void imgViewOtherInGLAdjustments_Click(object sender, ImageClickEventArgs e)
    {
        uctlOtherInGLAdjustments.IsExpanded = true;
        this.LoadRecItemsGLAdjustment(uctlOtherInGLAdjustments, WebEnums.RecCategory.GLAdjustments, WebEnums.RecCategoryType.BankAccount_GLAdjustments_Other);
        uctlOtherInGLAdjustments.ContentVisibility = true;
    }

    void imgViewNSFFee_Click(object sender, ImageClickEventArgs e)
    {
        uctlNSFFee.IsExpanded = true;
        this.LoadRecItemsGLAdjustment(uctlNSFFee, WebEnums.RecCategory.GLAdjustments, WebEnums.RecCategoryType.BankAccount_GLAdjustments_NSFFees);
        uctlNSFFee.ContentVisibility = true;
    }

    void imgViewBankFee_Click(object sender, ImageClickEventArgs e)
    {
        uctlBankFee.IsExpanded = true;
        this.LoadRecItemsGLAdjustment(uctlBankFee, WebEnums.RecCategory.GLAdjustments, WebEnums.RecCategoryType.BankAccount_GLAdjustments_BankFees);
        uctlBankFee.ContentVisibility = true;
 
    }

    private void ucRecFormButtons_eventButtonClick(string commandName)
    {
        try
        {
            if (commandName == RecFormButtonCommandName.REMOVE_SIGN_OFF)
            {
                RemoveSignOffFromSRA();
            }
            else
            {
                if (trRecControlCheckList.Visible == false)
                {
                    SaveOnButtonClick(commandName);
                }
                else
                {
                    if (commandName == RecFormButtonCommandName.SAVE || commandName == RecFormButtonCommandName.EDIT_REC || commandName == RecFormButtonCommandName.REJECT)
                    {
                        SaveOnButtonClick(commandName);
                    }
                    else
                    {
                        ICompany oCompanyClient = RemotingHelper.GetCompanyObject();
                        IList<CompanySettingInfo> oCompanySettingInfoCollection = oCompanyClient.SelectCompanyRCCLValidationType(SessionHelper.CurrentReconciliationPeriodID, Helper.GetAppUserInfo());
                        if (oCompanySettingInfoCollection != null && oCompanySettingInfoCollection.Count > 0 && oCompanySettingInfoCollection[0].RCCValidationTypeID.HasValue)
                        {
                            if (oCompanySettingInfoCollection[0].RCCValidationTypeID.Value == RCCValidationType.Exclude_SRAs_from_RCC_requirement && this.IsSRA == true)
                            {
                                SaveOnButtonClick(commandName);
                            }
                            else
                            {
                                if (SessionHelper.CurrentRoleID == (short)ARTEnums.UserRole.PREPARER || SessionHelper.CurrentRoleID == (short)ARTEnums.UserRole.BACKUP_PREPARER)
                                {
                                    if (Convert.ToString(lblRecControlTotalValue.Text) == Convert.ToString(lblRecControlCompletedValue.Text))
                                    {
                                        SaveOnButtonClick(commandName);
                                    }
                                    else
                                    {
                                        // Show Error Message
                                        Helper.ShowErrorMessage(this, LanguageUtil.GetValue(2854));
                                    }
                                }
                                else if (SessionHelper.CurrentRoleID == (short)ARTEnums.UserRole.REVIEWER || SessionHelper.CurrentRoleID == (short)ARTEnums.UserRole.BACKUP_REVIEWER)
                                {
                                    if (Convert.ToString(lblRecControlTotalValue.Text) == Convert.ToString(hdReviewCount.Value))
                                    {
                                        SaveOnButtonClick(commandName);
                                    }
                                    else
                                    {
                                        // Show Error Message
                                        Helper.ShowErrorMessage(this, LanguageUtil.GetValue(2854));
                                    }
                                }
                                else
                                {
                                    SaveOnButtonClick(commandName);
                                }
                            }
                        }
                        else
                        {
                            if (SessionHelper.CurrentRoleID == (short)ARTEnums.UserRole.PREPARER || SessionHelper.CurrentRoleID == (short)ARTEnums.UserRole.BACKUP_PREPARER)
                            {
                                if (Convert.ToString(lblRecControlTotalValue.Text) == Convert.ToString(lblRecControlCompletedValue.Text))
                                {
                                    SaveOnButtonClick(commandName);
                                }
                                else
                                {
                                    // Show Error Message
                                    Helper.ShowErrorMessage(this, LanguageUtil.GetValue(2854));
                                }
                            }
                            else if (SessionHelper.CurrentRoleID == (short)ARTEnums.UserRole.REVIEWER || SessionHelper.CurrentRoleID == (short)ARTEnums.UserRole.BACKUP_REVIEWER)
                            {
                                if (Convert.ToString(lblRecControlTotalValue.Text) == Convert.ToString(hdReviewCount.Value))
                                {
                                    SaveOnButtonClick(commandName);
                                }
                                else
                                {
                                    // Show Error Message
                                    Helper.ShowErrorMessage(this, LanguageUtil.GetValue(2854));
                                }
                            }
                            else
                            {
                                SaveOnButtonClick(commandName);
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
    }
    protected void ompage_ReconciliationPeriodChangedEventHandler(object sender, EventArgs e)
    {
        try
        {
            if (IsPostBack)
            {
                _IsRefreshData = true;
                if (this.GLDataHdrInfo != null)
                    PreviousNetAccountId = this.GLDataHdrInfo.NetAccountID;
                OnPageLoad();
                Helper.ValidateRecTemplateForAccountAndNetAccount(this, this.GLDataHdrInfo, PreviousNetAccountId);
                _IsRefreshData = false;
                // Since the GL Data ID would have changed due to change of Rec Period
                // reset on all controls to refresh them if already expanded
                SetNewGLDataID();
                SetEditMode();
                // Reload Usercontrols
                ReloadUserControls();

                //RecHelper.SetAccountTaskCount(lblPendingTaskStatus, ucRecFormAccountTaskGrid.TaskCountPending, lblCompletedTaskStatus, ucRecFormAccountTaskGrid.TaskCountCompleted);

                pnlRecForm.Enabled = false;
                if (GLDataID != null && GLDataID != 0)
                {
                    pnlRecForm.Enabled = true;
                    //this.ucRecFormButtons.EnableDisableButtons();
                    //decimal? exchangeRateBaseToReporting = CacheHelper.GetExchangeRate(CurrentBCCY, SessionHelper.ReportingCurrencyCode, SessionHelper.CurrentReconciliationPeriodID);
                    //decimal? exchangeRateReportingToBase = CacheHelper.GetExchangeRate(SessionHelper.ReportingCurrencyCode, CurrentBCCY, SessionHelper.CurrentReconciliationPeriodID);
                    // Enable Only when it is not net account
                    //if (!string.IsNullOrEmpty(CurrentBCCY) && exchangeRateBaseToReporting == null)
                    //{
                    //    throw new ARTException(5000106);
                    //}

                    //if (!string.IsNullOrEmpty(CurrentBCCY) && exchangeRateReportingToBase == null)
                    //{
                    //    throw new ARTException(5000107);
                    //}

                    //if (!string.IsNullOrEmpty(CurrentBCCY))
                    //{
                    //    Helper.SetAttributeForInputTextBoxesToCalculateValueInOtherCurrecy(txtBankBalanceBC
                    //        , txtBankBalanceRC, txtBankBalanceBC, txtBankBalanceRC, exchangeRateBaseToReporting.GetValueOrDefault()
                    //        , hdnGLBalanceBC, hdnGLBalanceRC
                    //        , hdnGlAdjustmentAndTimingDiffBC, hdnGlAdjustmentAndTimingDiffRC
                    //        , lblReconciledBalanceBC, lblReconciledBalanceRC
                    //        , lblTotalRecWriteOffBC, lblTotalRecWriteOffRC
                    //        , lblTotalUnExplainedVarianceBC, lblTotalUnExplainedVarianceRC, !string.IsNullOrEmpty(CurrentBCCY));
                    //}
                    //else
                    //    txtBankBalanceBC.Enabled = false;

                    //Helper.SetAttributeForInputTextBoxesToCalculateValueInOtherCurrecy(txtBankBalanceRC
                    //    , txtBankBalanceBC, txtBankBalanceBC, txtBankBalanceRC, exchangeRateReportingToBase.GetValueOrDefault()
                    //    , hdnGLBalanceBC, hdnGLBalanceRC
                    //    , hdnGlAdjustmentAndTimingDiffBC, hdnGlAdjustmentAndTimingDiffRC
                    //    , lblReconciledBalanceBC, lblReconciledBalanceRC
                    //    , lblTotalRecWriteOffBC, lblTotalRecWriteOffRC
                    //    , lblTotalUnExplainedVarianceBC, lblTotalUnExplainedVarianceRC, !string.IsNullOrEmpty(CurrentBCCY));
                }
            }
            //if (GLDataID != null && GLDataID != 0)
            //{
            //    Helper.HideMessage(this);
            //}

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
    private void OnPageLoad()
    {

        Helper.SetPageTitle(this, 1355);
        Helper.HideMessage(this);

        RegisterToggleControl();
        ManagePackageFeatures();

        if (!string.IsNullOrEmpty(Request.QueryString[QueryStringConstants.GLDATA_ID]))
        {
            long? _gLDataID = Convert.ToInt64(Request.QueryString[QueryStringConstants.GLDATA_ID]);
            this.GLDataHdrInfo = Helper.GetGLDataHdrInfo(_gLDataID);
        }

        string pageID = Request.QueryString[QueryStringConstants.REFERRER_PAGE_ID];
        _ARTPages = (WebEnums.ARTPages)System.Enum.Parse(typeof(WebEnums.ARTPages), pageID);

        Helper.ValidateRecTemplateForGLDataID(this, this.GLDataHdrInfo, eReconciliationItemTemplateThisPage, _ARTPages);
        EditMode = Helper.GetFormMode(WebEnums.ARTPages.TemplateBankAccount, this.GLDataHdrInfo);
        if (this.GLDataHdrInfo != null)
        {
            lblPeriodEndDate.Text = string.Format(WebConstants.FORMAT_BRACKET, Helper.GetDisplayDate(SessionHelper.CurrentReconciliationPeriodEndDate));

            //WebEnums.FormMode eFormMode = Helper.GetFormMode(WebEnums.ARTPages.TemplateBankAccount, GLRecStatus, this.IsSRA);
            //WebEnums.FormMode eFormMode = Helper.GetFormMode(WebEnums.ARTPages.TemplateBankAccount, this.GLDataHdrInfo);
            if (EditMode == WebEnums.FormMode.Edit)
            {
                // Enable Only when it is not net account
                if (!string.IsNullOrEmpty(CurrentBCCY))
                {
                    txtBankBalanceBC.Enabled = true;
                    cvBankBalanceBC.Enabled = true;
                }
                txtBankBalanceRC.Enabled = true;
                cvBankBalanceRC.Enabled = true;
            }
            else
            {
                txtBankBalanceBC.Enabled = false;
                txtBankBalanceRC.Enabled = false;
                cvBankBalanceBC.Enabled = false;
                cvBankBalanceRC.Enabled = false;
            }

            //if (this.GLDataHdrInfo != null && this.GLDataHdrInfo.NetAccountID.HasValue && this.GLDataHdrInfo.NetAccountID > 0)
            //{
            //    trAccountTask.Visible = false;
            //}

            SetURL();
            SetAllLabels();

            //Set properties for ucAccountInfo cntrol
            this.ucAccountInfo.GLDataID = GLDataID.HasValue ? GLDataID.Value : 0;
            this.ucAccountInfo.AccountID = AccountID.HasValue ? AccountID.Value : 0;
            this.ucAccountInfo.NetAccountID = this.NetAccountID.HasValue ? this.NetAccountID.Value : 0;


            //Set properties for DocumentUpload cntrol
            SetDocumentUploadURL();

            //Set properties for ButtonsControl cntrol
            SetButtonsControlProperties();

            // Set the Master Page Properties for GL Data ID
            RecHelper.SetRecStatusBarPropertiesForRecForm(this, GLDataID);

            // Set the Entity Name LabelID For GLAdjustments
            setEntityNameLabelIDForGLAdjustments();

            // Display Non-Editbale Message
            Helper.ShowNonEditableMessage(this, GLDataHdrInfo);
            this.ucRecControlCheckList.GLDataHdrInfo = this.GLDataHdrInfo;
            this.ucRecControlCheckList.EditMode = Helper.GetFormModeForRecControlCheckList(this.GLDataHdrInfo);
        }

        SetPageSettings();
        //Added By Prafull for NetAccountComposition PopUp 
        //******************************************************************************************************************************
        if (this.NetAccountID.GetValueOrDefault() == 0)
        {
            imgShowNetAccountComposition.Visible = false;
        }
        else
        {
            imgShowNetAccountComposition.Visible = true;
            imgShowNetAccountComposition.ToolTip = LanguageUtil.GetValue(2128);
            imgShowNetAccountComposition.OnClientClick = "javascript:OpenRadWindowForHyperlink('" + Page.ResolveClientUrl("~/Pages/PopupNetAccountComposition.aspx?" + QueryStringConstants.NETACCOUNT_ID + "=" + this.NetAccountID + "&" + QueryStringConstants.REC_PERIOD_ID + "=" + SessionHelper.CurrentReconciliationPeriodID.Value) + "', 350, 500, '" + imgShowNetAccountComposition.ClientID + "');";
        }
        //******************************************************************************************************************************



        string _PopupUrl = string.Empty;
        _PopupUrl = SetDocumentUploadURL();
        hlDocument.NavigateUrl = "javascript:OpenRadWindowForHyperlink('" + _PopupUrl + "', " + WebConstants.POPUP_HEIGHT + " , " + WebConstants.POPUP_WIDTH + ");";

        this.ucRecFormButtons.EnableDisableButtons();

        decimal? exchangeRateBaseToReporting = CacheHelper.GetExchangeRate(CurrentBCCY, SessionHelper.ReportingCurrencyCode, SessionHelper.CurrentReconciliationPeriodID);
        decimal? exchangeRateReportingToBase = CacheHelper.GetExchangeRate(SessionHelper.ReportingCurrencyCode, CurrentBCCY, SessionHelper.CurrentReconciliationPeriodID);

        if (!string.IsNullOrEmpty(CurrentBCCY) && exchangeRateBaseToReporting == null)
        {
            throw new ARTException(5000106);
        }

        if (!string.IsNullOrEmpty(CurrentBCCY) && exchangeRateReportingToBase == null)
        {
            throw new ARTException(5000107);
        }

        cvBankBalanceBC.ErrorMessage = Helper.GetErrorMessage(WebEnums.FieldType.InvalidNumericField, 1386, 1493);
        cvBankBalanceRC.ErrorMessage = Helper.GetErrorMessage(WebEnums.FieldType.InvalidNumericField, 1386, 1424);
        // Enable Only when it is not net account
        if (!string.IsNullOrEmpty(CurrentBCCY))
        {
            Helper.SetAttributeForInputTextBoxesToCalculateValueInOtherCurrecy(txtBankBalanceBC
                , txtBankBalanceRC, txtBankBalanceBC, txtBankBalanceRC, exchangeRateBaseToReporting.GetValueOrDefault()
                , hdnGLBalanceBC, hdnGLBalanceRC
                , hdnGlAdjustmentAndTimingDiffBC, hdnGlAdjustmentAndTimingDiffRC
                , hdnSupportingDetailBC, hdnSupportingDetailRC
                , lblReconciledBalanceBC, lblReconciledBalanceRC
                , lblTotalRecWriteOffBC, lblTotalRecWriteOffRC
                , lblTotalUnExplainedVarianceBC, lblTotalUnExplainedVarianceRC, !string.IsNullOrEmpty(CurrentBCCY), hdnBankBalanceRC, hdnBankBalanceBC);
        }
        else
            txtBankBalanceBC.Enabled = false;
        Helper.SetAttributeForInputTextBoxesToCalculateValueInOtherCurrecy(txtBankBalanceRC
            , txtBankBalanceBC, txtBankBalanceBC, txtBankBalanceRC, exchangeRateReportingToBase.GetValueOrDefault()
            , hdnGLBalanceBC, hdnGLBalanceRC
            , hdnGlAdjustmentAndTimingDiffBC, hdnGlAdjustmentAndTimingDiffRC
            , hdnSupportingDetailBC, hdnSupportingDetailRC
            , lblReconciledBalanceBC, lblReconciledBalanceRC
            , lblTotalRecWriteOffBC, lblTotalRecWriteOffRC
            , lblTotalUnExplainedVarianceBC, lblTotalUnExplainedVarianceRC, !string.IsNullOrEmpty(CurrentBCCY), hdnBankBalanceRC, hdnBankBalanceBC);

        RecHelper.RenderJSForOldValuesForRecalculateBalances(this, txtBankBalanceBC, txtBankBalanceRC);


        // HandleRefreshOnPopupclose();
        RecHelper.ShowHideReviewNotesAndQualityScore(trReviewNotes, trQualityScore, trRecControlCheckList);

        ucRecFormAccountTaskGrid.RegisterClientScripts();

        AutoExpandSections();

    }

    private void AutoExpandSections()
    {
        List<AutoSaveAttributeValueInfo> oAutoSaveAttributeList = Helper.GetAutoSaveAttributeValues();
        if (oAutoSaveAttributeList != null && oAutoSaveAttributeList.Count > 0)
        {
            foreach(AutoSaveAttributeValueInfo item in oAutoSaveAttributeList)
            {
                switch ((ARTEnums.AutoSaveAttribute)item.AutoSaveAttributeID)
                {
                    case ARTEnums.AutoSaveAttribute.BankFormAdjustmentsBankFees:
                        if (uctlBankFee.AutoSaveAttributeID.HasValue && Convert.ToBoolean(item.Value))
                        {
                            imgViewBankFee_Click(imgViewBankFee, null);
                        }
                        break;
                    case ARTEnums.AutoSaveAttribute.BankFormAdjustmentsNSFFees:
                        if (uctlNSFFee.AutoSaveAttributeID.HasValue && Convert.ToBoolean(item.Value))
                        {
                            imgViewNSFFee_Click(imgViewNSFFee, null);
                        }
                        break;
                    case ARTEnums.AutoSaveAttribute.BankFormAdjustmentsOther:
                        if (uctlOtherInGLAdjustments.AutoSaveAttributeID.HasValue && Convert.ToBoolean(item.Value))
                        {
                            imgViewOtherInGLAdjustments_Click(imgViewOtherInGLAdjustments, null);
                        }
                        break;
                    case ARTEnums.AutoSaveAttribute.BankFormTimingDifferenceDepositsInTransit:
                        if (uctlDepositInTransit.AutoSaveAttributeID.HasValue && Convert.ToBoolean(item.Value))
                        {
                            imgViewDepositInTransit_Click(imgViewDepositInTransit, null);
                        }
                        break;
                    case ARTEnums.AutoSaveAttribute.BankFormTimingDifferenceOutstandingChecks:
                        if (uctlOutstandingChecks.AutoSaveAttributeID.HasValue && Convert.ToBoolean(item.Value))
                        {
                            imgViewOutstandingChecks_Click(imgViewOutstandingChecks, null);
                        }
                        break;
                    case ARTEnums.AutoSaveAttribute.BankFormTimingDifferenceOther:
                        if (uctlOtherInTimingDifference.AutoSaveAttributeID.HasValue && Convert.ToBoolean(item.Value))
                        {
                            imgViewOtherInTimingDifference_Click(imgViewOtherInTimingDifference, null);
                        }
                        break;
                    case ARTEnums.AutoSaveAttribute.BankFormReconciliationWriteOffsOns:
                        if (uctlItemInputWriteOff.AutoSaveAttributeID.HasValue && Convert.ToBoolean(item.Value))
                        {
                            imgViewRecWriteOff_Click(imgViewRecWriteOff, null);
                        }
                        break;
                    case ARTEnums.AutoSaveAttribute.BankFormUnexpVar:
                        if (uctlUnexplainedVariance.AutoSaveAttributeID.HasValue && Convert.ToBoolean(item.Value))
                        {
                            imgViewUnexplainedVariance_Click(imgViewUnexplainedVariance, null);
                        }
                        break;
                    case ARTEnums.AutoSaveAttribute.BankFromQualityScore:
                        if (ucEditQualityScore.AutoSaveAttributeID.HasValue && Convert.ToBoolean(item.Value))
                        {
                            imgQualityScore_Click(imgQualityScore, null);
                        }
                        break;
                    case ARTEnums.AutoSaveAttribute.BankFormRCCStatus:
                        if (ucRecControlCheckList.AutoSaveAttributeID.HasValue && Convert.ToBoolean(item.Value))
                        {
                            imgRecControlCheckList_Click(imgRecControlCheckList, null);
                        }
                        break;
                    case ARTEnums.AutoSaveAttribute.BankFormTaskStatus:
                        if (ucRecFormAccountTaskGrid.AutoSaveAttributeID.HasValue && Convert.ToBoolean(item.Value))
                        {
                            imgAccountTask_Click(imgAccountTask, null);
                        }
                        break;
                }
            }
        }
    }
    private void setEntityNameLabelIDForGLAdjustments()
    {
        if (lblBankFee.LabelID.ToString() != "1656")
            uctlBankFee.EntityNameLabelID = lblBankFee.LabelID;
        else
            uctlBankFee.EntityNameLabelID = lblGLAdjustments.LabelID;

        if (lblNSFFees.LabelID.ToString() != "1656")
            uctlNSFFee.EntityNameLabelID = lblNSFFees.LabelID;
        else
            uctlNSFFee.EntityNameLabelID = lblGLAdjustments.LabelID;

        if (lblOtherInGLAdjustments.LabelID.ToString() != "1656")
            uctlOtherInGLAdjustments.EntityNameLabelID = lblOtherInGLAdjustments.LabelID;
        else
            uctlOtherInGLAdjustments.EntityNameLabelID = lblGLAdjustments.LabelID;

        if (lblDepositInTransit.LabelID.ToString() != "1656")
            uctlDepositInTransit.EntityNameLabelID = lblDepositInTransit.LabelID;
        else
            uctlDepositInTransit.EntityNameLabelID = lblGLAdjustments.LabelID;

        if (lblOutstandingChecks.LabelID.ToString() != "1656")
            uctlOutstandingChecks.EntityNameLabelID = lblOutstandingChecks.LabelID;
        else
            uctlOutstandingChecks.EntityNameLabelID = lblGLAdjustments.LabelID;


        if (ExLabel2.LabelID.ToString() != "1656")
            uctlOtherInTimingDifference.EntityNameLabelID = ExLabel2.LabelID;
        else
            uctlOtherInTimingDifference.EntityNameLabelID = lblGLAdjustments.LabelID;
    }

    private void RegisterToggleControl()
    {
        //Register toggle control with User Control
        uctlBankFee.RegisterToggleControl(imgViewBankFee);
        uctlNSFFee.RegisterToggleControl(imgViewNSFFee);
        uctlOtherInGLAdjustments.RegisterToggleControl(imgViewOtherInGLAdjustments);
        uctlDepositInTransit.RegisterToggleControl(imgViewDepositInTransit);
        uctlOutstandingChecks.RegisterToggleControl(imgViewOutstandingChecks);
        uctlOtherInTimingDifference.RegisterToggleControl(imgViewOtherInTimingDifference);
        uctlItemInputWriteOff.RegisterToggleControl(imgViewRecWriteOff);
        uctlUnexplainedVariance.RegisterToggleControl(imgViewUnexplainedVariance);
        ucEditQualityScore.RegisterToggleControl(imgQualityScore);
        ucRecFormAccountTaskGrid.RegisterToggleControl(imgAccountTask);
        ucRecControlCheckList.RegisterToggleControl(imgRecControlCheckList);
    }
    /// <summary>
    /// RemoveSignOffFromSRA() is used to make SRA account to normal account and refresh the page. 
    /// </summary>
    private void RemoveSignOffFromSRA()
    {
        try
        {
            List<long> oAccountIDCollection = new List<long>();
            List<int> oNetAccountIDCollection = new List<int>();
            if (AccountID != 0)
                oAccountIDCollection.Add(Convert.ToInt64(AccountID));
            else
                oNetAccountIDCollection.Add(Convert.ToInt32(NetAccountID));
            IGLData oGLDataClient = RemotingHelper.GetGLDataObject();
            oGLDataClient.UpdateGLDataForRemoveAccountSignOff(oAccountIDCollection, oNetAccountIDCollection, SessionHelper.CurrentReconciliationPeriodID,
                                            SessionHelper.CurrentUserLoginID, DateTime.Now, Helper.GetAppUserInfo());
            string path = Request.Url.PathAndQuery;
            path = path.Replace("IsSRA=1", "IsSRA=0");
            Response.Redirect(path, false);
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

    private void SaveOnButtonClick(string commandName)
    {

        bool isSignOff = false;
        bool isFormDataToBeSaved = true;
        WebEnums.ReconciliationStatus eNewReconciliationStatus;

        GLDataHdrInfo oGLDataHdrInfo = Helper.GetGLDataHdrInfoToSaveOnTemplateForm(commandName, IsSRA, ucEditQualityScore.IsExpanded, ucRecControlCheckList.IsExpanded, out eNewReconciliationStatus, out isSignOff, out isFormDataToBeSaved);

        oGLDataHdrInfo.GLDataID = GLDataID;

        if (txtBankBalanceBC.Text.Trim() != "")
        {
            oGLDataHdrInfo.SupportingDetailBalanceBaseCurrency = Convert.ToDecimal(txtBankBalanceBC.Text);
        }
        if (txtBankBalanceRC.Text.Trim() != "")
        {
            oGLDataHdrInfo.SupportingDetailBalanceReportingCurrency = Convert.ToDecimal(txtBankBalanceRC.Text);
        }

        short? reconciliationCategoryTypeIDForSupportingDetail = null;
        oGLDataHdrInfo.GLDataQualityScoreInfoList = ucEditQualityScore.GetData();
        oGLDataHdrInfo.GLDataRecControlCheckListInfoList = ucRecControlCheckList.GetGLDataRecControlCheckListInfoList();
        IGLData oGLDataClient = RemotingHelper.GetGLDataObject();
        oGLDataClient.SaveReconciliationForm(GLDataID, oGLDataHdrInfo, isFormDataToBeSaved, isSignOff, IS_SUPPORTING_DETAIL_ENTRY_ON_TEMPLATE, true, null, reconciliationCategoryTypeIDForSupportingDetail, Helper.GetAppUserInfo());

        //Raise Alert for deny/Rejected command
        Helper.RaiseAlertFromReconciliationTemplates(commandName, AccountID, this.NetAccountID);

        if (commandName == RecFormButtonCommandName.SIGNOFF
            || commandName == RecFormButtonCommandName.CANCEL
            || commandName == RecFormButtonCommandName.ACCEPT
            || commandName == RecFormButtonCommandName.REJECT
            || commandName == RecFormButtonCommandName.APPROVE
            || commandName == RecFormButtonCommandName.DENY
            )
        {
            HttpContext.Current.Response.Redirect(Helper.GetRedirectURLForTemplatePages(this.IsSRA, _ARTPages));
        }
        //Reload the page(refresh)
        ompage_ReconciliationPeriodChangedEventHandler(null, null);

        // ReLoad the user controls that are already expanded
        ReloadUserControls();
        if (ucRecControlCheckList.IsExpanded)
        {
            imgRecControlCheckList_Click(null, null);
        }

        // Show Confirmation Message
        Helper.ShowConfirmationMessage(this, Helper.GetSuccessMessage(1366));

    }
    private void ReloadUserControls()
    {
        // ReLoad the user controls that are already expanded
        RecHelper.ReloadUserControls(this.GLDataHdrInfo, uctlBankFee, uctlNSFFee, uctlOtherInGLAdjustments, uctlDepositInTransit, uctlOutstandingChecks, uctlOtherInTimingDifference, uctlItemInputWriteOff, uctlUnexplainedVariance, ucEditQualityScore, ucRecControlCheckList, ucRecFormAccountTaskGrid);
    }

    private void SetNewGLDataID()
    {
        // Since the GL Data ID would have changed, reset on all controls to refresh them if already expanded
        uctlBankFee.GLDataHdrInfo = this.GLDataHdrInfo;
        uctlNSFFee.GLDataHdrInfo = this.GLDataHdrInfo;
        uctlOtherInGLAdjustments.GLDataHdrInfo = this.GLDataHdrInfo;
        uctlDepositInTransit.GLDataHdrInfo = this.GLDataHdrInfo;
        uctlOutstandingChecks.GLDataHdrInfo = this.GLDataHdrInfo;
        uctlOtherInTimingDifference.GLDataHdrInfo = this.GLDataHdrInfo;
        uctlItemInputWriteOff.GLDataHdrInfo = this.GLDataHdrInfo;
        uctlUnexplainedVariance.GLDataHdrInfo = this.GLDataHdrInfo;
        ucEditQualityScore.GLDataHdrInfo = this.GLDataHdrInfo;
        ucRecFormAccountTaskGrid.GLDataHdrInfo = this.GLDataHdrInfo;
        ucRecControlCheckList.GLDataHdrInfo = this.GLDataHdrInfo;
    }
    private void SetEditMode()
    {
        uctlBankFee.EditMode = this.EditMode;
        uctlNSFFee.EditMode = this.EditMode;
        uctlOtherInGLAdjustments.EditMode = this.EditMode;
        uctlDepositInTransit.EditMode = this.EditMode;
        uctlOutstandingChecks.EditMode = this.EditMode;
        uctlOtherInTimingDifference.EditMode = this.EditMode;
        uctlItemInputWriteOff.EditMode = this.EditMode;

    }
    private void SetURL()
    {
        string queryString = "?" + QueryStringConstants.ACCOUNT_ID + "=" + AccountID
           + "&" + QueryStringConstants.NETACCOUNT_ID + "=" + this.NetAccountID
            + "&" + QueryStringConstants.GLDATA_ID + "=" + GLDataID
            + "&" + QueryStringConstants.REC_STATUS_ID + "=" + this.GLRecStatusID
            + "&" + QueryStringConstants.REFERRER_PAGE_ID + "=" + _ARTPages;

        if (this.IsSRA.HasValue && this.IsSRA.Value)
        {
            queryString += "&" + QueryStringConstants.IS_SRA + "=1";
        }
        else
        {
            queryString += "&" + QueryStringConstants.IS_SRA + "=0";
        }
        queryString += "&" + QueryStringConstants.REC_CATEGORY_TYPE_ID + "=";

        hlImportBankFees.NavigateUrl = URLConstants.URL_IMPORT_BANKFEE + queryString + (short)WebEnums.RecCategoryType.BankAccount_GLAdjustments_BankFees + "&" + QueryStringConstants.REC_CATEGORY_ID + "=" + (short)WebEnums.RecCategory.GLAdjustments;
        hlImportNSFFees.NavigateUrl = URLConstants.URL_IMPORT_BANKFEE + queryString + (short)WebEnums.RecCategoryType.BankAccount_GLAdjustments_NSFFees + "&" + QueryStringConstants.REC_CATEGORY_ID + "=" + (short)WebEnums.RecCategory.GLAdjustments;
        hlImportOtherInGLAdjustments.NavigateUrl = URLConstants.URL_IMPORT_BANKFEE + queryString + (short)WebEnums.RecCategoryType.BankAccount_GLAdjustments_Other + "&" + QueryStringConstants.REC_CATEGORY_ID + "=" + (short)WebEnums.RecCategory.GLAdjustments;

        hlImportDepositInTransit.NavigateUrl = URLConstants.URL_IMPORT_BANKFEE + queryString + (short)WebEnums.RecCategoryType.BankAccount_TimingDifference_DepositsInTransit + "&" + QueryStringConstants.REC_CATEGORY_ID + "=" + (short)WebEnums.RecCategory.TimingDifference;
        hlImportOutstandingChecks.NavigateUrl = URLConstants.URL_IMPORT_BANKFEE + queryString + (short)WebEnums.RecCategoryType.BankAccount_TimingDifference_OutstandingChecks + "&" + QueryStringConstants.REC_CATEGORY_ID + "=" + (short)WebEnums.RecCategory.TimingDifference;
        hlImportOtherInTimingDifference.NavigateUrl = URLConstants.URL_IMPORT_BANKFEE + queryString + (short)WebEnums.RecCategoryType.BankAccount_TimingDifference_Other + "&" + QueryStringConstants.REC_CATEGORY_ID + "=" + (short)WebEnums.RecCategory.TimingDifference;

        hlUnexplainedVarianceHistory.NavigateUrl = URLConstants.URL_ITEMINPUT_UNEXPLAINEDVARIANCE_HISTORY + queryString + (short)WebEnums.RecCategoryType.BankAccount_RecWriteoffOn_UnexpVar + "&" + QueryStringConstants.REC_CATEGORY_ID + "=" + (short)WebEnums.RecCategory.VariancesWriteOffOn;

        hlReviewNotes.NavigateUrl = URLConstants.URL_ITEMINPUT_COMMENT + queryString + (short)WebEnums.RecCategoryType.BankAccount_ReviewNotes + "&" + QueryStringConstants.REC_CATEGORY_ID + "=" + (short)WebEnums.RecCategory.ReviewNotes;
        hlAuditTrail.NavigateUrl = URLConstants.URL_AUDIT_TRAIL + queryString + (short)WebEnums.RecCategoryType.BankAccount_ReviewNotes + "&" + QueryStringConstants.REC_CATEGORY_ID + "=" + (short)WebEnums.RecCategory.ReviewNotes;
        string queryStringForRCCLImport = "?" + QueryStringConstants.ACCOUNT_ID + "=" + AccountID
              + "&" + QueryStringConstants.NETACCOUNT_ID + "=" + NetAccountID
               + "&" + QueryStringConstants.GLDATA_ID + "=" + GLDataID;
        hlImportRecControlCheckList.NavigateUrl = URLConstants.URL_IMPORT_RECCONTROLCHECKLIST + queryString;
        RegisterToggleControlEventHandler();
        ucAccountMatching.SetURL(AccountID, this.NetAccountID, GLDataID, Request.Url.AbsoluteUri);

    }


    private void RegisterToggleControlEventHandler()
    {
        imgViewBankFee.Click += new ImageClickEventHandler(imgViewBankFee_Click);
        imgViewNSFFee.Click += new ImageClickEventHandler(imgViewNSFFee_Click);
        imgViewOtherInGLAdjustments.Click += new ImageClickEventHandler(imgViewOtherInGLAdjustments_Click);
        imgViewDepositInTransit.Click += new ImageClickEventHandler(imgViewDepositInTransit_Click);
        imgViewOutstandingChecks.Click += new ImageClickEventHandler(imgViewOutstandingChecks_Click);
        imgViewOtherInTimingDifference.Click += new ImageClickEventHandler(imgViewOtherInTimingDifference_Click);
        imgViewRecWriteOff.Click += new ImageClickEventHandler(imgViewRecWriteOff_Click);
        imgViewUnexplainedVariance.Click += new ImageClickEventHandler(imgViewUnexplainedVariance_Click);
        imgQualityScore.Click += new ImageClickEventHandler(imgQualityScore_Click);
        imgAccountTask.Click += new ImageClickEventHandler(imgAccountTask_Click);
        imgRecControlCheckList.Click += new ImageClickEventHandler(imgRecControlCheckList_Click);
    }
    private void SetButtonsControlProperties()
    {
        this.ucRecFormButtons.GLDataHdrInfo = this.GLDataHdrInfo;
        this.ucRecFormButtons.CurrentUserRole = SessionHelper.CurrentRoleID.Value;
        this.ucRecFormButtons.ReconciliationStatusID = (short)this.GLRecStatus;
        this.ucRecFormButtons.eARTPages = WebEnums.ARTPages.TemplateBankAccount;
    }
    private void SetDefaulValueForLabelTotal()
    {
        string defaultText = Helper.GetDisplayDecimalValue(0);
        decimal? defValue = null;

        if (!String.IsNullOrEmpty(this.CurrentBCCY))
            defValue = 0;
        lblBankFeeBC.Text = Helper.GetDisplayDecimalValue(defValue);
        lblNSFFeesBC.Text = Helper.GetDisplayDecimalValue(defValue);
        lblOtherInGLAdjustmentsBC.Text = Helper.GetDisplayDecimalValue(defValue);
        lblDepositInTransitBC.Text = Helper.GetDisplayDecimalValue(defValue);
        lblOutstandingChecksBC.Text = Helper.GetDisplayDecimalValue(defValue);
        lblOtherInTimingDifferenceBC.Text = Helper.GetDisplayDecimalValue(defValue);
        lblTotalRecWriteOffBC.Text = Helper.GetDisplayDecimalValue(defValue);



        lblBankFeeRC.Text = defaultText;
        lblNSFFeesRC.Text = defaultText;
        lblOtherInGLAdjustmentsRC.Text = defaultText;
        lblDepositInTransitRC.Text = defaultText;
        lblOutstandingChecksRC.Text = defaultText;
        lblOtherInTimingDifferenceRC.Text = defaultText;
        lblTotalRecWriteOffRC.Text = defaultText;
        lblSystemScore.Text = defaultText;
        lblUserScore.Text = defaultText;



        if (!IsPostBack || _IsGLDataIDChanged)  //_IsRefreshData
        {
            txtBankBalanceBC.Text = Helper.GetDecimalValueForTextBox(null, WebConstants.INT_FOR_DECIMAL_VALUE_TEXTBOX);
            txtBankBalanceRC.Text = Helper.GetDecimalValueForTextBox(null, WebConstants.INT_FOR_DECIMAL_VALUE_TEXTBOX);
        }
        if (!Page.IsPostBack)
        {
            lblRecControlTotalValue.Text = Helper.GetDisplayIntegerValue(0);
            lblRecControlCompletedValue.Text = Helper.GetDisplayIntegerValue(0);
            hdReviewCount.Value = Helper.GetDisplayIntegerValue(0);
        }
        if (hdCompetedCount.Value != "")
        {
            lblRecControlCompletedValue.Text = hdCompetedCount.Value;
        }

        lblReconciledBalanceRC.Text = defaultText;
        lblTotalUnExplainedVarianceBC.Text = Helper.GetDisplayDecimalValue(null);
        lblTotalUnExplainedVarianceRC.Text = defaultText;
        lblCountAttachedDocument.Text = string.Format(WebConstants.FORMAT_BRACKET, "0");
        lblCountReviewNotes.Text = string.Format(WebConstants.FORMAT_BRACKET, "0");

    }
    private void SetAttributesForSignOffButton()
    {

        decimal UnExplainedVarianceBC = 0.0M;
        decimal UnExplainedVarianceRC = 0.0M;
        hdnPrevUnExpBalanceBC.Value = Helper.GetDecimalValueForTextBox(0, 2);
        hdnPrevUnExpBalanceRC.Value = Helper.GetDecimalValueForTextBox(0, 2);
        RemoveAttributeForSignOff();
        AddAttributeForSignOff();

        if (GLDataID == null || GLDataID == 0)
            return;
        //IGLData oGLDataClient = RemotingHelper.GetGLDataObject();
        //List<GLDataHdrInfo> oGLDataHdrInfoCollection = oGLDataClient.SelectGLDataHdrByGLDataID(GLDataID, Helper.GetAppUserInfo());
        List<GLDataHdrInfo> oGLDataHdrInfoCollection = new List<GLDataHdrInfo>();
        oGLDataHdrInfoCollection.Add(this.GLDataHdrInfo);
        if (oGLDataHdrInfoCollection != null && oGLDataHdrInfoCollection.Count > 0)
        {
            UnExplainedVarianceBC = Math.Round(oGLDataHdrInfoCollection[0].UnexplainedVarianceBaseCurrency ?? 0, 2);
            UnExplainedVarianceRC = Math.Round(oGLDataHdrInfoCollection[0].UnexplainedVarianceReportingCurrency ?? 0, 2);
            if (Math.Abs(UnExplainedVarianceBC) > 0 || Math.Abs(UnExplainedVarianceRC) > 0)
            {
                IUnexplainedVariance oUnExpectedVarianceClient = RemotingHelper.GetUnexplainedVarianceObject();
                List<GLDataUnexplainedVarianceInfo> oGLDataUnexplainedVarianceInfoCollection = oUnExpectedVarianceClient.GetGLDataUnexplainedVarianceInfoCollectionByGLDataID(GLDataID, Helper.GetAppUserInfo());

                if (oGLDataUnexplainedVarianceInfoCollection != null)
                {
                    if (oGLDataUnexplainedVarianceInfoCollection.Count == 0)
                    {
                        //AddAttributeForSignOff();
                    }
                    else if (oGLDataUnexplainedVarianceInfoCollection.Count > 0)
                    {
                        hdnPrevUnExpBalanceBC.Value = Helper.GetDecimalValueForTextBox(oGLDataUnexplainedVarianceInfoCollection[0].AmountBaseCurrency, 2);
                        hdnPrevUnExpBalanceRC.Value = Helper.GetDecimalValueForTextBox(oGLDataUnexplainedVarianceInfoCollection[0].AmountReportingCurrency, 2);
                        //if (Math.Abs(Math.Round(oGLDataUnexplainedVarianceInfoCollection[0].AmountBaseCurrency ?? 0, 2)) != Math.Abs(UnExplainedVarianceBC))
                        //{
                        //    AddAttributeForSignOff();
                        //}
                    }
                }
            }
        }
    }

    private void AddAttributeForSignOff()
    {

        ExButton btnSignoff = (ExButton)this.ucRecFormButtons.FindControl("btnSignoff");
        btnSignoff.Attributes.Add("onclick", "return ConfirmUnExplainedVariance('" + LanguageUtil.GetValue(2057) + "');");

    }

    private void RemoveAttributeForSignOff()
    {

        ExButton btnSignoff = (ExButton)this.ucRecFormButtons.FindControl("btnSignoff");
        btnSignoff.Attributes.Remove("onclick");

    }
    private void HandleRefreshOnPopupclose()
    {

        //MasterPage oMasterPage=(MasterPage)this.Master;
        // MasterPageBase oMasterPageBase = (MasterPageBase)oMasterPage.Master;
        //  // Reload the Rec Periods and also the Status / Countdown
        // oMasterPageBase.ReloadRecPeriods();

    }
    private void ManagePackageFeatures()
    {
        if (Helper.IsFeatureActivated(WebEnums.Feature.QualityScore, SessionHelper.CurrentReconciliationPeriodID))
            trQualityScore.Visible = true;
        else
            trQualityScore.Visible = false;

        if (Helper.IsFeatureActivated(WebEnums.Feature.TaskMaster, SessionHelper.CurrentReconciliationPeriodID))
            trAccountTask.Visible = true;
        else
            trAccountTask.Visible = false;

        if (Helper.GetFeatureCapabilityMode(WebEnums.Feature.RecControlChecklist, ARTEnums.Capability.RecControlChecklist, SessionHelper.CurrentReconciliationPeriodID) == WebEnums.FeatureCapabilityMode.Visible)
            trRecControlCheckList.Visible = true;
        else
            trRecControlCheckList.Visible = false;
    }

    /// <summary>
    /// Sets the page settings.
    /// </summary>
    private void SetPageSettings()
    {
        MasterPages_RecProcessMasterPage oMasterPageBase = (MasterPages_RecProcessMasterPage)this.Master;
        MasterPageSettings oMasterPageSettings = new MasterPageSettings();
        oMasterPageSettings.EditMode = this.EditMode; ;
        oMasterPageBase.SetMasterPageSettings(oMasterPageSettings);
    }

    #endregion

    #region Other Methods

    protected void SetAllLabels()
    {
        decimal? glAdjustmentAndTimingDiffBC = 0M;
        decimal? glAdjustmentAndTimingDiffRC = 0M;


        decimal? glAdjustmentBankFeeBC = 0M;
        decimal? glAdjustmentBankFeeRC = 0M;

        decimal? glAdjustmentNSFFeesBC = 0M;
        decimal? glAdjustmentNSFFeesRC = 0M;

        decimal? glAdjustmentOtherInGLAdjustmentsBC = 0M;
        decimal? glAdjustmentOtherInGLAdjustmentsRC = 0M;

        decimal? glAdjustmentDepositInTransitBC = 0M;
        decimal? glAdjustmentDepositInTransitRC = 0M;

        decimal? glAdjustmentOutstandingChecksBC = 0M;
        decimal? glAdjustmentOutstandingChecksRC = 0M;

        decimal? glAdjustmentOtherInTimingDifferenceBC = 0M;
        decimal? glAdjustmentOtherInTimingDifferenceRC = 0M;

        SetDefaulValueForLabelTotal();

        if (GLDataID != null && GLDataID != 0)
        {
            IGLDataRecItem oGLRecItemInputClient = RemotingHelper.GetGLDataRecItemObject();
            List<GLDataRecItemInfo> oGLReconciliationItemInputInfoCollection = oGLRecItemInputClient.GetTotalByReconciliationCategoryTypeID(GLDataID, Helper.GetAppUserInfo());

            foreach (GLDataRecItemInfo oGLReconciliationItemInputInfo in oGLReconciliationItemInputInfoCollection)
            {
                WebEnums.RecCategoryType eReconciliationCategoryType = (WebEnums.RecCategoryType)oGLReconciliationItemInputInfo.ReconciliationCategoryTypeID;
                if ((Int32)eReconciliationCategoryType > 0)
                {
                    switch (eReconciliationCategoryType)
                    {
                        case WebEnums.RecCategoryType.BankAccount_GLAdjustments_BankFees:
                            lblBankFeeBC.Text = Helper.GetDisplayBaseCurrencyValue(this.CurrentBCCY, oGLReconciliationItemInputInfo.AmountBaseCurrency);
                            //lblBankFeeBC.Text = Helper.GetDisplayDecimalValue(oGLReconciliationItemInputInfo.AmountBaseCurrency);
                            lblBankFeeRC.Text = Helper.GetDisplayDecimalValue(oGLReconciliationItemInputInfo.AmountReportingCurrency);

                            glAdjustmentBankFeeBC = oGLReconciliationItemInputInfo.AmountBaseCurrency;
                            glAdjustmentBankFeeRC = oGLReconciliationItemInputInfo.AmountReportingCurrency;
                            break;

                        case WebEnums.RecCategoryType.BankAccount_GLAdjustments_NSFFees:
                            //lblNSFFeesBC.Text = Helper.GetDisplayDecimalValue(oGLReconciliationItemInputInfo.AmountBaseCurrency);
                            lblNSFFeesBC.Text = Helper.GetDisplayBaseCurrencyValue(this.CurrentBCCY, oGLReconciliationItemInputInfo.AmountBaseCurrency);
                            lblNSFFeesRC.Text = Helper.GetDisplayDecimalValue(oGLReconciliationItemInputInfo.AmountReportingCurrency);

                            glAdjustmentNSFFeesBC = oGLReconciliationItemInputInfo.AmountBaseCurrency;
                            glAdjustmentNSFFeesRC = oGLReconciliationItemInputInfo.AmountReportingCurrency;
                            break;

                        case WebEnums.RecCategoryType.BankAccount_GLAdjustments_Other:
                            //lblOtherInGLAdjustmentsBC.Text = Helper.GetDisplayDecimalValue(oGLReconciliationItemInputInfo.AmountBaseCurrency);
                            lblOtherInGLAdjustmentsBC.Text = Helper.GetDisplayBaseCurrencyValue(this.CurrentBCCY, oGLReconciliationItemInputInfo.AmountBaseCurrency);
                            lblOtherInGLAdjustmentsRC.Text = Helper.GetDisplayDecimalValue(oGLReconciliationItemInputInfo.AmountReportingCurrency);

                            glAdjustmentOtherInGLAdjustmentsBC = oGLReconciliationItemInputInfo.AmountBaseCurrency;
                            glAdjustmentOtherInGLAdjustmentsRC = oGLReconciliationItemInputInfo.AmountReportingCurrency;
                            break;

                        case WebEnums.RecCategoryType.BankAccount_TimingDifference_DepositsInTransit:
                            //lblDepositInTransitBC.Text = Helper.GetDisplayDecimalValue(oGLReconciliationItemInputInfo.AmountBaseCurrency);
                            lblDepositInTransitBC.Text = Helper.GetDisplayBaseCurrencyValue(this.CurrentBCCY, oGLReconciliationItemInputInfo.AmountBaseCurrency);
                            lblDepositInTransitRC.Text = Helper.GetDisplayDecimalValue(oGLReconciliationItemInputInfo.AmountReportingCurrency);

                            glAdjustmentDepositInTransitBC = oGLReconciliationItemInputInfo.AmountBaseCurrency;
                            glAdjustmentDepositInTransitRC = oGLReconciliationItemInputInfo.AmountReportingCurrency;
                            break;

                        case WebEnums.RecCategoryType.BankAccount_TimingDifference_OutstandingChecks:
                            //lblOutstandingChecksBC.Text = Helper.GetDisplayDecimalValue(oGLReconciliationItemInputInfo.AmountBaseCurrency);
                            lblOutstandingChecksBC.Text = Helper.GetDisplayBaseCurrencyValue(this.CurrentBCCY, oGLReconciliationItemInputInfo.AmountBaseCurrency);
                            lblOutstandingChecksRC.Text = Helper.GetDisplayDecimalValue(oGLReconciliationItemInputInfo.AmountReportingCurrency);

                            glAdjustmentOutstandingChecksBC = oGLReconciliationItemInputInfo.AmountBaseCurrency;
                            glAdjustmentOutstandingChecksRC = oGLReconciliationItemInputInfo.AmountReportingCurrency;
                            break;

                        case WebEnums.RecCategoryType.BankAccount_TimingDifference_Other:
                            //lblOtherInTimingDifferenceBC.Text = Helper.GetDisplayDecimalValue(oGLReconciliationItemInputInfo.AmountBaseCurrency);
                            lblOtherInTimingDifferenceBC.Text = Helper.GetDisplayBaseCurrencyValue(this.CurrentBCCY, oGLReconciliationItemInputInfo.AmountBaseCurrency);
                            lblOtherInTimingDifferenceRC.Text = Helper.GetDisplayDecimalValue(oGLReconciliationItemInputInfo.AmountReportingCurrency);

                            glAdjustmentOtherInTimingDifferenceBC = oGLReconciliationItemInputInfo.AmountBaseCurrency;
                            glAdjustmentOtherInTimingDifferenceRC = oGLReconciliationItemInputInfo.AmountReportingCurrency;
                            break;
                    }
                }

            }


            IGLData oGLDataClient = RemotingHelper.GetGLDataObject();

            //List<GLDataHdrInfo> oGLDataHdrInfoCollection = oGLDataClient.SelectGLDataHdrByGLDataID(GLDataID, Helper.GetAppUserInfo());
            List<GLDataHdrInfo> oGLDataHdrInfoCollection = new List<GLDataHdrInfo>();
            oGLDataHdrInfoCollection.Add(this.GLDataHdrInfo);
            if (oGLDataHdrInfoCollection != null && oGLDataHdrInfoCollection.Count > 0)
            {
                //this.CurrentBCCY = oGLDataHdrInfoCollection[0].BaseCurrencyCode;
                lblGLBalanceBC.Text = Helper.GetDisplayCurrencyValue(this.CurrentBCCY, oGLDataHdrInfoCollection[0].GLBalanceBaseCurrency);
                lblGLBalanceRC.Text = Helper.GetDisplayReportingCurrencyValue(oGLDataHdrInfoCollection[0].GLBalanceReportingCurrency);
                // Set in Hidden
                hdnGLBalanceBC.Value = Helper.GetDisplayCurrencyValue(this.CurrentBCCY, oGLDataHdrInfoCollection[0].GLBalanceBaseCurrency);
                hdnGLBalanceRC.Value = Helper.GetDisplayDecimalValue(oGLDataHdrInfoCollection[0].GLBalanceReportingCurrency);

                //********  Formula Reconciled Balance=  (Bank Fees) + (NSF Fees)+ Others + (DIT) + OS Checks+ Others ******
                glAdjustmentAndTimingDiffBC =
                          glAdjustmentBankFeeBC
                        + glAdjustmentNSFFeesBC
                        + glAdjustmentOtherInGLAdjustmentsBC
                        + glAdjustmentDepositInTransitBC
                        + glAdjustmentOutstandingChecksBC
                        + glAdjustmentOtherInTimingDifferenceBC;

                glAdjustmentAndTimingDiffRC =
                          glAdjustmentBankFeeRC
                        + glAdjustmentNSFFeesRC
                        + glAdjustmentOtherInGLAdjustmentsRC
                        + glAdjustmentDepositInTransitRC
                        + glAdjustmentOutstandingChecksRC
                        + glAdjustmentOtherInTimingDifferenceRC;

                hdnGlAdjustmentAndTimingDiffBC.Value = Helper.GetDisplayBaseCurrencyValue(this.CurrentBCCY, glAdjustmentAndTimingDiffBC);
                hdnGlAdjustmentAndTimingDiffRC.Value = Helper.GetDisplayDecimalValue(glAdjustmentAndTimingDiffRC);

                lblReconciledBalanceBC.Text = Helper.GetDisplayBaseCurrencyValue(this.CurrentBCCY, oGLDataHdrInfoCollection[0].ReconciliationBalanceBaseCurrency);
                if (oGLDataHdrInfoCollection[0].ReconciliationBalanceReportingCurrency.HasValue)
                    lblReconciledBalanceRC.Text = Helper.GetDisplayDecimalValue(oGLDataHdrInfoCollection[0].ReconciliationBalanceReportingCurrency);
                if (oGLDataHdrInfoCollection[0].WriteOnOffAmountBaseCurrency.HasValue)
                    lblTotalRecWriteOffBC.Text = Helper.GetDisplayBaseCurrencyValue(this.CurrentBCCY, oGLDataHdrInfoCollection[0].WriteOnOffAmountBaseCurrency);
                if (oGLDataHdrInfoCollection[0].WriteOnOffAmountReportingCurrency.HasValue)
                    lblTotalRecWriteOffRC.Text = Helper.GetDisplayDecimalValue(oGLDataHdrInfoCollection[0].WriteOnOffAmountReportingCurrency);

                decimal? SupportingDetailBankBC = 0M;
                decimal? SupportingDetailBankRC = 0M;
                if ((!IsPostBack || _IsGLDataIDChanged) && oGLDataHdrInfoCollection != null && oGLDataHdrInfoCollection.Count > 0)
                {
                    SupportingDetailBankBC = oGLDataHdrInfoCollection[0].SupportingDetailBalanceBaseCurrency;
                    SupportingDetailBankRC = oGLDataHdrInfoCollection[0].SupportingDetailBalanceReportingCurrency;
                    txtBankBalanceBC.Text = Helper.GetDecimalValueForTextBox(SupportingDetailBankBC, WebConstants.INT_FOR_DECIMAL_VALUE_TEXTBOX);
                    txtBankBalanceRC.Text = Helper.GetDecimalValueForTextBox(SupportingDetailBankRC, WebConstants.INT_FOR_DECIMAL_VALUE_TEXTBOX);

                    ucRecFormAccountTaskGrid.GLDataHdrInfo = this.GLDataHdrInfo;
                    LoadAccountTask();
                }
                else
                {
                    if (!String.IsNullOrEmpty(hdnBankBalanceRC.Value))
                        txtBankBalanceRC.Text = Helper.GetDecimalValueForTextBox(Decimal.Parse(hdnBankBalanceRC.Value), 2);
                    if (!String.IsNullOrEmpty(hdnBankBalanceBC.Value))
                        txtBankBalanceBC.Text = Helper.GetDecimalValueForTextBox(Decimal.Parse(hdnBankBalanceBC.Value), 2);
                    if (txtBankBalanceBC.Text.Trim() != "" && String.IsNullOrEmpty(txtBankBalanceRC.Text))
                    {
                        txtBankBalanceRC.Text = Helper.GetDecimalValueForTextBox(RecHelper.ConvertCurrency(this.CurrentBCCY, SessionHelper.ReportingCurrencyCode, SessionHelper.CurrentReconciliationPeriodID, SupportingDetailBankBC), WebConstants.INT_FOR_DECIMAL_VALUE_TEXTBOX);
                    }
                    if (txtBankBalanceRC.Text.Trim() != "" && String.IsNullOrEmpty(txtBankBalanceBC.Text))
                    {
                        txtBankBalanceBC.Text = Helper.GetDecimalValueForTextBox(RecHelper.ConvertCurrency(SessionHelper.ReportingCurrencyCode, this.CurrentBCCY, SessionHelper.CurrentReconciliationPeriodID, SupportingDetailBankRC), WebConstants.INT_FOR_DECIMAL_VALUE_TEXTBOX);
                    }
                    if (!string.IsNullOrEmpty(txtBankBalanceBC.Text))
                        SupportingDetailBankBC = Convert.ToDecimal(txtBankBalanceBC.Text);
                    if (!string.IsNullOrEmpty(txtBankBalanceRC.Text))
                        SupportingDetailBankRC = Convert.ToDecimal(txtBankBalanceRC.Text);
                }

                oGLDataHdrInfoCollection[0].UnexplainedVarianceBaseCurrency = RecHelper.CaclculateUnexplainedVariance(
                    oGLDataHdrInfoCollection[0].GLBalanceBaseCurrency,
                    glAdjustmentBankFeeBC + glAdjustmentNSFFeesBC + glAdjustmentOtherInGLAdjustmentsBC,
                    glAdjustmentDepositInTransitBC + glAdjustmentOutstandingChecksBC + glAdjustmentOtherInTimingDifferenceBC,
                    SupportingDetailBankBC,
                    oGLDataHdrInfoCollection[0].WriteOnOffAmountBaseCurrency);
                oGLDataHdrInfoCollection[0].UnexplainedVarianceReportingCurrency = RecHelper.CaclculateUnexplainedVariance(
                    oGLDataHdrInfoCollection[0].GLBalanceReportingCurrency,
                    glAdjustmentBankFeeRC + glAdjustmentNSFFeesRC + glAdjustmentOtherInGLAdjustmentsRC,
                    glAdjustmentDepositInTransitRC + glAdjustmentOutstandingChecksRC + glAdjustmentOtherInTimingDifferenceRC,
                    SupportingDetailBankRC,
                    oGLDataHdrInfoCollection[0].WriteOnOffAmountReportingCurrency);

                if (oGLDataHdrInfoCollection[0].UnexplainedVarianceBaseCurrency.HasValue)
                    lblTotalUnExplainedVarianceBC.Text = Helper.GetDisplayDecimalValue(oGLDataHdrInfoCollection[0].UnexplainedVarianceBaseCurrency);
                if (oGLDataHdrInfoCollection[0].UnexplainedVarianceReportingCurrency.HasValue)
                    lblTotalUnExplainedVarianceRC.Text = Helper.GetDisplayDecimalValue(oGLDataHdrInfoCollection[0].UnexplainedVarianceReportingCurrency);


                //GetCountDocumentByGLDataID
                int? countAttachedDocument = oGLDataClient.GetCountAttachedDocumentByGLDataID(GLDataID, Helper.GetAppUserInfo());
                if (countAttachedDocument.HasValue)
                {
                    string textCountAttachedDocument = countAttachedDocument.Value.ToString();
                    lblCountAttachedDocument.Text = string.Format(WebConstants.FORMAT_BRACKET, textCountAttachedDocument);
                }

                int? countGLReviewNote = oGLDataClient.GetCountGLReviewNoteByGLDataID(GLDataID, Helper.GetAppUserInfo());
                if (countGLReviewNote.HasValue)
                {
                    string textCountGLReviewNote = countGLReviewNote.Value.ToString();
                    lblCountReviewNotes.Text = string.Format(WebConstants.FORMAT_BRACKET, textCountGLReviewNote);
                }

                // Added By Santosh for GlVersionButton 
                //Set properties for ucGLVersionButton Control
                ucGLVersionButton.IsVersionAvailable = (bool)oGLDataHdrInfoCollection[0].IsVersionAvailable;
                ucGLVersionButton.GLDataID = this.GLDataHdrInfo.GLDataID;
                ucEditQualityScore.GLDataHdrInfo = this.GLDataHdrInfo;
                Dictionary<ARTEnums.QualityScoreType, Int32?> dictGLDataQualityScoreCount = QualityScoreHelper.GetGLDataQualityScoreCount(GLDataID);
                if (dictGLDataQualityScoreCount != null)
                {
                    if (dictGLDataQualityScoreCount.ContainsKey(ARTEnums.QualityScoreType.SystemScore))
                        lblSystemScoreValue.Text = Helper.GetDisplayIntegerValue(dictGLDataQualityScoreCount[ARTEnums.QualityScoreType.SystemScore]);
                    if (dictGLDataQualityScoreCount.ContainsKey(ARTEnums.QualityScoreType.UserScore))
                        lblUserScoreValue.Text = Helper.GetDisplayIntegerValue(dictGLDataQualityScoreCount[ARTEnums.QualityScoreType.UserScore]);
                }
                if (ucEditQualityScore.IsExpanded)
                    ucEditQualityScore.LoadData();



                //if (ucRecFormAccountTaskGrid.LoadGridData || _IsGLDataIDChanged)
                //{
                //    ucRecFormAccountTaskGrid.LoadGridData = true;
                //    ucRecFormAccountTaskGrid.GLDataHdrInfo = this.GLDataHdrInfo;
                //    LoadAccountTask();
                //}


                if (ucRecControlCheckList.IsExpanded)
                    ucRecControlCheckList.LoadData();
                string OldTotalValue = lblRecControlTotalValue.Text;
                int old;
                int total;
                RecControlCheckListHelper.SetRecControlCheckListCounts(GLDataID, lblRecControlTotalValue, lblRecControlCompletedValue, hdReviewCount);
                if (int.TryParse(OldTotalValue, out old) == int.TryParse(lblRecControlTotalValue.Text, out total))
                {
                    if (old <= total)
                    {
                        if (hdCompetedCount.Value != "")
                        {
                            lblRecControlCompletedValue.Text = hdCompetedCount.Value;
                        }
                        else if (hdReviwed.Value != "")
                        {
                            hdReviewCount.Value = hdReviwed.Value;
                        }
                    }
                    else
                    {
                        hdCompetedCount.Value = "";
                        hdReviewCount.Value = "";
                    }
                }
                else
                {
                    hdCompetedCount.Value = "";
                    hdReviewCount.Value = "";
                }
            }

        }
        var taskGridData = TaskMasterHelper.GetAccessableTaskByAccountIDs(AccountID, NetAccountID);
        if (taskGridData != null && taskGridData.Count > 0)
        {
            int TaskCountCompleted = taskGridData.FindAll(t => t.TaskStatusID == (short)ARTEnums.TaskStatus.Completed).Count;
            int TaskCountPending = (taskGridData.Count - TaskCountCompleted);
            RecHelper.SetAccountTaskCount(lblPendingTaskStatus, TaskCountPending, lblCompletedTaskStatus, TaskCountCompleted);
        }
    }

    public override string GetMenuKey()
    {
        return Helper.GetMenuKeyForRecForms(_ARTPages);
    }
    public string SetDocumentUploadURL()
    {
        if (GLDataID == null || GLDataID == 0 ||
            GLRecStatusID == null || GLRecStatusID == 0)
            return ScriptHelper.GetJSForEmptyURL();
        else
        {
            string windowName;
            WebEnums.ReconciliationStatus eReconciliationStatus = (WebEnums.ReconciliationStatus)Enum.Parse(typeof(WebEnums.ReconciliationStatus), this.GLRecStatusID.ToString());
            //if (Helper.GetFormMode(WebEnums.ARTPages.TemplateBankAccount, this.GLDataHdrInfo) == WebEnums.FormMode.Edit)
            if (EditMode == WebEnums.FormMode.Edit)
            {
                return Helper.SetDocumentUploadURLfrombankAccountForm((short)WebEnums.FormMode.Edit, GLDataID, this.AccountID, this.NetAccountID, this.IsSRA, Request.Url.ToString(), out windowName);

            }

            else
            {
                return Helper.SetDocumentUploadURLfrombankAccountForm((short)WebEnums.FormMode.ReadOnly, GLDataID, this.AccountID, this.NetAccountID, this.IsSRA, Request.Url.ToString(), out windowName);
            }
        }
    }
    public override void OnGLDataIDChanged()
    {
        base.OnGLDataIDChanged();
        _IsGLDataIDChanged = true;
    }
    public override void RefreshPage(object sender, RefreshEventArgs args)
    {
        base.RefreshPage(sender, args);
        //Reload the page(refresh)
        ompage_ReconciliationPeriodChangedEventHandler(null, null);
        uctlBankFee.ApplyFilter();
        uctlNSFFee.ApplyFilter();
        uctlOtherInGLAdjustments.ApplyFilter();
        uctlOtherInTimingDifference.ApplyFilter();
        uctlDepositInTransit.ApplyFilter();
        uctlOutstandingChecks.ApplyFilter();
        uctlItemInputWriteOff.ApplyFilter();
        MasterPageBase oMasterPageBase = (MasterPageBase)this.Master.Master;
        // Reload the Rec Periods and also the Status / Countdown
        oMasterPageBase.ReloadRecPeriods(false);
    }
    void LoadQualityScore()
    {
        if (!ucEditQualityScore.Visible)
        {
            //this.ucEditQualityScore.AccountID = this.AccountID;
            //this.ucEditQualityScore.NetAccountID = this.NetAccountID;
            //this.ucEditQualityScore.GLDataID = GLDataID;
            //if (this.IsSRA.HasValue && this.IsSRA.Value)
            //    this.ucEditQualityScore.IsSRA = true;
            //else
            //    this.ucEditQualityScore.IsSRA = false;
            this.ucEditQualityScore.GLDataHdrInfo = this.GLDataHdrInfo;
            ucEditQualityScore.IsRefreshData = true;
            this.ucEditQualityScore.LoadData();
            this.ucEditQualityScore.Visible = true;
        }
        else
        {
            this.ucEditQualityScore.Visible = false;
        }
    }
    void LoadRecControlCheckList()
    {
        if (!ucRecControlCheckList.Visible)
        {
            this.ucRecControlCheckList.IsRefreshData = true;
            this.ucRecControlCheckList.GLDataHdrInfo = this.GLDataHdrInfo;
            this.ucRecControlCheckList.LoadData();
            this.ucRecControlCheckList.Visible = true;
        }
        else
        {
            this.ucRecControlCheckList.Visible = false;
        }
    }


    void LoadAccountTask()
    {
        //if (!ucRecFormAccountTaskGrid.Visible)
        //{
        if (this.GLDataHdrInfo != null && trAccountTask.Visible)
        {
            ucRecFormAccountTaskGrid.IsRefreshData = true;
            ucRecFormAccountTaskGrid.GLDataHdrInfo = this.GLDataHdrInfo;
            //ucRecFormAccountTaskGrid.RegisterClientScripts();
            this.ucRecFormAccountTaskGrid.LoadData();
            ucRecFormAccountTaskGrid.Visible = true;
        }
        //}
        else
        {
            ucRecFormAccountTaskGrid.Visible = false;
        }
        RecHelper.SetAccountTaskCount(lblPendingTaskStatus, ucRecFormAccountTaskGrid.TaskCountPending, lblCompletedTaskStatus, ucRecFormAccountTaskGrid.TaskCountCompleted);
        //lblPendingTaskStatus.Text = Helper.GetDisplayIntegerValueWithBracket(ucRecFormAccountTaskGrid.TaskCountPending);
        //lblCompletedTaskStatus.Text = Helper.GetDisplayIntegerValueWithBracket(ucRecFormAccountTaskGrid.TaskCountCompleted);
    }

    void LoadRecItemsGLAdjustment(UserControls_GLAdjustments ucGLAdjustments, WebEnums.RecCategory? enumRecCategory, WebEnums.RecCategoryType enumRecCategoryType)
    {
        ucGLAdjustments.IsRefreshData = true;
        ucGLAdjustments.GLDataHdrInfo = this.GLDataHdrInfo;
        ucGLAdjustments.EditMode = this.EditMode;
        ucGLAdjustments.RecCategoryTypeID = (short)enumRecCategoryType;
        if (ucGLAdjustments.RecCategoryTypeID != null)
        {
            ucGLAdjustments.RecCategoryID = (short)enumRecCategory;
        }
        ucGLAdjustments.LoadData();
    }

    void LoadRecItemsItemWriteOff()
    {
        if (!this.uctlItemInputWriteOff.Visible)
        {
            this.uctlItemInputWriteOff.IsRefreshData = true;
            this.uctlItemInputWriteOff.GLDataHdrInfo = this.GLDataHdrInfo;
            this.uctlItemInputWriteOff.EditMode = this.EditMode;
            this.uctlItemInputWriteOff.RecCategoryTypeID = (short)WebEnums.RecCategoryType.BankAccount_RecWriteoffOn_WriteoffOn;
            if (this.uctlItemInputWriteOff.RecCategoryTypeID != null)
            {
                this.uctlItemInputWriteOff.RecCategoryID = (short)WebEnums.RecCategory.VariancesWriteOffOn;
            }
            this.uctlItemInputWriteOff.LoadData();
            this.uctlItemInputWriteOff.Visible = true;
        }
        else
        {
            this.uctlItemInputWriteOff.Visible = false;
        }
    }

    void LoadUnexplainedVariance()
    {
        if (!this.uctlUnexplainedVariance.Visible)
        {
            //this.uctlUnexplainedVariance.AccountID = AccountID;
            //this.uctlUnexplainedVariance.NetAccountID = this.NetAccountID;
            //this.uctlUnexplainedVariance.GLDataID = GLDataID;
            //if (this.IsSRA.HasValue && this.IsSRA.Value)
            //    this.uctlUnexplainedVariance.IsSRA = true;
            //else
            //    this.uctlUnexplainedVariance.IsSRA = false;
            this.uctlUnexplainedVariance.IsRefreshData = true;
            this.uctlUnexplainedVariance.GLDataHdrInfo = this.GLDataHdrInfo;
            this.uctlUnexplainedVariance.RecCategoryTypeID = (short)WebEnums.RecCategoryType.Accrual_RecWriteoffOn_UnexpVar;
            if (this.uctlUnexplainedVariance.RecCategoryTypeID != null)
            {
                this.uctlUnexplainedVariance.RecCategoryID = (short)WebEnums.RecCategory.VariancesWriteOffOn;
            }
            this.uctlUnexplainedVariance.LoadData();
            this.uctlUnexplainedVariance.Visible = true;
        }
        else
        {
            this.uctlUnexplainedVariance.Visible = false;
        }
    }

    #endregion

}

