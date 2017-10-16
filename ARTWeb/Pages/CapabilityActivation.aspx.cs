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

using SkyStem.ART.Client.IServices;
using SkyStem.ART.Client.Model;
using SkyStem.ART.Web.Classes;
using SkyStem.ART.Web.Data;
using SkyStem.Library.Controls.WebControls;
using SkyStem.ART.Web.Utility;
using System.Text;
using SkyStem.ART.Client.Exception;
using SkyStem.Language.LanguageUtility;
using SkyStem.ART.Client.Data;

//TODO: also consider start and end date for capabilities in DB, show riskrating based on default frequencies in DB if nothing selected yet. 
//TODO: #if DEMO is not working so all submit_click() methods (in child controls are commented as of now. change that. 
//TODO: reports and FS Captions are not using labelIDs and transfer method now.

// usp_UPD_MarkToBeReprocessed
// usp_INS_CompanyCapabilityActivationTableValue
// usp_INS_MaterialityCapability
// usp_INS_MandatoryReportCapability
//(usp_INS_RoleMandatoryReportSetByRoleID)
// usp_INS_RiskRatingCapability
//(usp_INS_RiskRatingReconciliationPeriodByRiskRatingIDAndReconciliationFrequencyID)
// usp_INS_CompanyUnexplainedVarianceThreshold

//usp_GET_MaterialityTypeByRecPeriodID
//usp_SEL_UnexplainedVarianceThresholdByRecPeriodID
//usp_SEL_FSCaptionMaterialityByRecPeriodID
//usp_SEL_RoleReportByRoleID (no range)
//usp_SEL_MandatoryReportByRecPeriodID
//usp_SEL_RiskRatingReconciliationPeriodByRiskRatingIDAndRecPeriodID
//usp_SEL_RiskRatingReconciliationFrequencyByRecPeriodID

public partial class Pages_CapabilityActivation : PageBaseRecPeriod
{
    #region Variables & Constants
    bool? _isMaterialityYesChecked = null;
    bool? _isRiskRatingYesChecked = null;
    bool? _isKeyAccountYesChecked = null;
    bool? _isDualReviewYesChecked = null;
    bool? _isMandatoryReportSignoffYesChecked = null;
    bool? _isZeroBalanceAccountYesChecked = null;
    bool? _isMultiCurrencyYesChecked = null;
    bool? _isReopenRecOnCCYreloadYesChecked = null;
    bool? _isCertificationActivationYesChecked = null;
    bool? _isNetAccountYesChecked = null;
    bool? _isCeoCfoCertificationYesChecked = null;
    bool? _isAllowDeletionOfReviewNotesYesChecked = null;
    bool? _isJournalEntryWriteOffOnApproverYesChecked = null;
    bool? _isDueDateByAccountYesChecked = null;
    bool? _isRecControlChecklistYesChecked = null;
    bool? _isDualReviewYesCheckedFromUI = null;
    bool? _isDueDateByAccountYesCheckedFromUI = null;
    bool? _isMaterialityForwarded = null;
    bool? _isRiskRatingForwarded = null;
    bool? _isKeyAccountForwarded = null;
    bool? _isDualReviewForwarded = null;
    bool? _isMandatoryReportSignoffForwarded = null;
    bool? _isZeroBalanceAccountForwarded = null;
    bool? _isMultiCurrencyForwarded = null;
    bool? _isReopenRecOnCCYreloadForwarded = null;
    bool? _isCertificationActivationForwarded = null;
    bool? _isNetAccountForwarded = null;
    bool? _isCeoCfoCertificationForwarded = null;
    bool? _isAllowDeletionOfReviewNotesForwarded = null;
    bool? _isCompanyUnexplainedVarianceThresholdForwarded = null;
    bool? _isJournalEntryWriteOffOnApproverForwarded = null;
    bool? _isDueDateByAccountForwarded = null;
    bool? _isRecControlChecklistForwarded = null;

    int? _companyID = 0;

    #endregion

    #region Properties
    IUtility oUtilityClient;
    ICompany oCompanyClient;
    #endregion

    #region Delegates & Events
    #endregion

    #region Page Events
    protected void Page_Init(object sender, EventArgs e)
    {
        try
        {
            MasterPageBase oMPage = (MasterPageBase)this.Master;
            oMPage.ReconciliationPeriodChangedEventHandler += new EventHandler(oMPage_ReconciliationPeriodChangedEventHandler);
            //oMPage.CompanyChangedEventHandler += new EventHandler(oMPage_CompanyChangedEventHandler);
            ucCapabilityActivationDualLevelReview.DualLevelReviewSelectionChanged += new EventHandler(HandleDualReviewYesNoChecked);
            this.SetErrorMessages();
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
        OnPageLoad(false);
    }
    #endregion

    #region Grid Events
    #endregion

    #region Other Events
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                bool IsAnythingChanged = SaveAll();
                HttpContext.Current.Session.Remove(SessionConstants.CURRENT_CAPABILITY_COLLECTION);
                //// Also clear the Capability Collection Rec Period Data since 
                SessionHelper.ClearRecPeriodCompanyCapabilityData();
                if (IsAnythingChanged)
                {
                    string url = "~/Pages/Home.aspx?" + QueryStringConstants.CONFIRMATION_MESSAGE_LABEL_ID + "=1650";
                    //Response.Redirect(url);
                    SessionHelper.RedirectToUrl(url);
                    return;
                }
               // else
              //  {
                    //throw new Exception(LanguageUtil.GetValue(2877));
              //  }
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

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Helper.RedirectToHomePage();
    }

    protected void oMPage_ReconciliationPeriodChangedEventHandler(object sender, EventArgs e)
    {
        try
        {
            OnPageLoad(true);
            //EnableDisablePageBasedOnRecPeriodStatus();

            //_companyID = SessionHelper.CurrentCompanyID;
            //IList<CompanyCapabilityInfo> oCompanyCapabilityInfoCollection = oCompanyClient.SelectAllCompanyCapabilityByReconciliationID(SessionHelper.CurrentReconciliationPeriodID);
            //IList<CompanyCapabilityInfo> oCompanyCapabilityInfoCollection = SessionHelper.GetCompanyCapabilityCollectionForCurrentRecPeriod();
            //MakeYseNoSelectionFromDB2(oCompanyCapabilityInfoCollection);

            //SetTBCompanyUnexplainedVarianceThreshold();
            //lblReportingCurrencyValue.Text = SessionHelper.ReportingCurrencyCode;
            //SetCarryforwardedStatusCompanyUnexplainedVarianceThreshold();
            //Helper.SetCarryforwardedStatus(imgUnexplainedVarianceThresholdForwardYes, imgUnexplainedVarianceThresholdForwardNo, _isCompanyUnexplainedVarianceThresholdForwarded);

            //_isDualReviewYesCheckedFromUI = Helper.SetSetFlagBasedOnYesNoRadioButtons(optDualReviewYes, optDualReviewNo);
            //_isDualReviewYesCheckedFromUI = ucCapabilityActivationDualLevelReview.IsDualLevelReviewYesCheckedCurrent;
            //if (_isDualReviewYesCheckedFromUI.HasValue)
            //{
            //    ucCapabilityMandatoryReportSignoff.IsDualReviewYesChecked = _isDualReviewYesCheckedFromUI.Value;
            //}

            ucCapabilityRiskRatingAll.ChangedEventHandler();
            ucCapabilityMandatoryReportSignoff.ChangedEventHandler();
            ucCapabilityMateriality.ChangedEventHandler();
            ucCapabilityActivationDualLevelReview.ChangedEventHandler();
            ucCapabilityDueDateByAccount.ChangedEventHandler();
            CapabilityActivationRCCL.ChangedEventHandler();
            ucCapabilityMultiCurrency.ChangedEventHandler();
            upnlMain.Update();
            Helper.HideMessage(this);
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
    private void OnPageLoad(bool bRecPeriodChange)
    {
        try
        {
            oUtilityClient = RemotingHelper.GetUtilityObject();
            oCompanyClient = RemotingHelper.GetCompanyObject();
            Helper.SetPageTitle(this, 1224);
            _companyID = SessionHelper.CurrentCompanyID;
            IList<CompanyCapabilityInfo> oCompanyCapabilityInfoCollection = SessionHelper.GetCompanyCapabilityCollectionForCurrentRecPeriod();
            if (!IsPostBack || bRecPeriodChange)
            {
                Helper.ShowInputRequirementSection(this, 1640, 2025, 2031, 2072);
                ShowHideControlsBasedOnCapability();
                EnableDisablePageBasedOnRecPeriodStatus();

                //IList<CompanyCapabilityInfo> oCompanyCapabilityInfoCollection = oCompanyClient.SelectAllCompanyCapabilityByReconciliationID(SessionHelper.CurrentReconciliationPeriodID);

                MakeYseNoSelectionFromDB2(oCompanyCapabilityInfoCollection);
                SetTBCompanyUnexplainedVarianceThreshold();
                lblReportingCurrencyValue.Text = SessionHelper.ReportingCurrencyCode;
                SetCarryforwardedStatusCompanyUnexplainedVarianceThreshold();
                Helper.SetCarryforwardedStatus(imgUnexplainedVarianceThresholdForwardYes, imgUnexplainedVarianceThresholdForwardNo, _isCompanyUnexplainedVarianceThresholdForwarded);
            }
            else
            {
                //IList<CompanyCapabilityInfo> oCompanyCapabilityInfoCollection = oCompanyClient.SelectAllCompanyCapabilityByReconciliationID(SessionHelper.CurrentReconciliationPeriodID);
                DecideYseNoSelectionFromDB(oCompanyCapabilityInfoCollection);

                //SetTBCompanyUnexplainedVarianceThreshold();
                SetCarryforwardedStatusCompanyUnexplainedVarianceThreshold();
                //Helper.SetCarryforwardedStatus(imgUnexplainedVarianceThresholdForwardYes, imgUnexplainedVarianceThresholdForwardNo, _isCompanyUnexplainedVarianceThresholdForwarded);
            }
            //   _isDualReviewYesCheckedFromUI = Helper.SetSetFlagBasedOnYesNoRadioButtons(optDualReviewYes, optDualReviewNo);
            _isDualReviewYesCheckedFromUI = ucCapabilityActivationDualLevelReview.IsDualLevelReviewYesCheckedCurrent;
            if (_isDualReviewYesCheckedFromUI.HasValue)
            {
                ucCapabilityMandatoryReportSignoff.IsDualReviewYesChecked = _isDualReviewYesCheckedFromUI.Value;
            }
            _isDueDateByAccountYesCheckedFromUI = ucCapabilityDueDateByAccount.IsDueDateByAccountYesCheckedCurrent;
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

    private void ShowHideControlsBasedOnCapability()
    {
        trMultiCurrency.Visible = Helper.IsFeatureActivated(WebEnums.Feature.MultiCurrency, SessionHelper.CurrentReconciliationPeriodID);
        trMultiCurrencyBlankRow.Visible = trMultiCurrency.Visible;

        //trReopenRecOnCCYreload.Visible = Helper.IsFeatureActivated(WebEnums.Feature.MultiCurrency, SessionHelper.CurrentReconciliationPeriodID);
        //trReopenRecOnCCYreloadBlankRow.Visible = trReopenRecOnCCYreload.Visible;

        trDualReview.Visible = Helper.IsFeatureActivated(WebEnums.Feature.DualLevelReview, SessionHelper.CurrentReconciliationPeriodID);
        trDualReviewBlankRow.Visible = trDualReview.Visible;

        trCEOCert.Visible = Helper.IsFeatureActivated(WebEnums.Feature.CEO_CFOCertification, SessionHelper.CurrentReconciliationPeriodID);
        trCEOCertBlankRow.Visible = trCEOCert.Visible;

        trCertActication.Visible = Helper.IsFeatureActivated(WebEnums.Feature.Certification, SessionHelper.CurrentReconciliationPeriodID);
        trRecomCertActivation.Visible = trCertActication.Visible;
        trCertActicationBlankRow.Visible = trCertActication.Visible;

        trMandatoryReportSignoff.Visible = Helper.IsFeatureActivated(WebEnums.Feature.MandatoryReportSignOff, SessionHelper.CurrentReconciliationPeriodID);
        trMandatoryReportSignoffBlankRow.Visible = trMandatoryReportSignoff.Visible;

        trKeyAccount.Visible = Helper.IsFeatureActivated(WebEnums.Feature.KeyAccount, SessionHelper.CurrentReconciliationPeriodID);
        trKeyAccountBlankRow.Visible = trKeyAccount.Visible;

        trJE.Visible = Helper.IsFeatureActivated(WebEnums.Feature.JournalEntry, SessionHelper.CurrentReconciliationPeriodID);
        trJEBlankRow.Visible = trJE.Visible;

        trDueDateByAccount.Visible = Helper.IsFeatureActivated(WebEnums.Feature.DueDateByAccount, SessionHelper.CurrentReconciliationPeriodID);
        trDueDateByAccountBlankRow.Visible = trDueDateByAccount.Visible;

        trRCCL.Visible = Helper.IsFeatureActivated(WebEnums.Feature.RecControlChecklist, SessionHelper.CurrentReconciliationPeriodID);
        trRCCLBlankRow.Visible = trRCCL.Visible;
    }
    private void SetErrorMessages()
    {
        //this.cvMultiCurrency.ErrorMessage = Helper.GetErrorMessage(WebEnums.FieldType.MandatoryField, this.lblMultiCurrency.LabelID);
        //this.cvReopenRecOnCCYreload.ErrorMessage = Helper.GetErrorMessage(WebEnums.FieldType.MandatoryField, this.lblReopenRecOnCCYreload.LabelID);
        rfvUnexpVarThreshold.ErrorMessage = Helper.GetErrorMessage(WebEnums.FieldType.MandatoryField, this.lblUnexpVarThreshold.LabelID);
        cvUnexpVarThreshold.ErrorMessage = LanguageUtil.GetValue(2492);        
    }
    private void SetTBCompanyUnexplainedVarianceThreshold()
    {
        oCompanyClient = RemotingHelper.GetCompanyObject();
        IList<CompanyUnexplainedVarianceThresholdInfo> oCompanyUnexplainedVarianceThresholdInfoCollection = oCompanyClient.GetUnexplainedVarianceThresholdByRecPeriodID(SessionHelper.CurrentReconciliationPeriodID, Helper.GetAppUserInfo());
        if (oCompanyUnexplainedVarianceThresholdInfoCollection != null && oCompanyUnexplainedVarianceThresholdInfoCollection.Count > 0)
        {
            txtUnexpVarThreshold.Text = Helper.GetDecimalValueForTextBox(oCompanyUnexplainedVarianceThresholdInfoCollection[0].CompanyUnexplainedVarianceThreshold, TestConstant.DECIMAL_PLACES_FOR_TEXTBOX);
            //_isCompanyUnexplainedVarianceThresholdForwarded = oCompanyUnexplainedVarianceThresholdInfoCollection[0].IsCarryForwardedFromPreviousRecPeriod;           
        }
        else
        {
            //TODO: put default value
            txtUnexpVarThreshold.Text = "";
        }
        hdnUnexpVarThresholdVal.Value = txtUnexpVarThreshold.Text;
    }

    private void EnableDisablePageBasedOnRecPeriodStatus()
    {
        ReconciliationPeriodStatusMstInfo oReconciliationPeriodStatusMstInfo = SessionHelper.GetRecPeriodStatus();
        WebEnums.RecPeriodStatus eRecPeriodStatus = (WebEnums.RecPeriodStatus)oReconciliationPeriodStatusMstInfo.ReconciliationPeriodStatusID;
        bool bEnable = false;
        bool isPeriodInProgress = false;
        bool isCertificationStarted = CertificationHelper.IsCertificationStarted();
        bool isPartialEditMode = false;
        if ((Int32)eRecPeriodStatus > 0)
        {
            switch (eRecPeriodStatus)
            {
                case WebEnums.RecPeriodStatus.NotStarted:
                    //pnlPage.Enabled = true;//TODO: do it with style or property as it is giving some javascrip error when recPeriod DDL changs.
                    //btnSubmit.Enabled = true;
                    bEnable = true;
                    break;
                case WebEnums.RecPeriodStatus.InProgress:
                case WebEnums.RecPeriodStatus.Open:
                    isPeriodInProgress = true;
                    break;
                case WebEnums.RecPeriodStatus.Skipped:
                case WebEnums.RecPeriodStatus.Closed:
                    //pnlPage.Enabled = false;
                    //btnSubmit.Enabled = false;
                    bEnable = false;
                    break;
            }
            isPartialEditMode = isPeriodInProgress && !isCertificationStarted;
            pnlMultiCurrency.Enabled = bEnable;
            //pnlReopenRecOnCCYreload.Enabled = bEnable;
            pnlDualReview.Enabled = bEnable;
            pnlRCCL.Enabled = bEnable;
            pnlMateriality.Enabled = !isCertificationStarted && (bEnable || isPeriodInProgress);
            //ucCapabilityMateriality.IsPartialEditMode = isPartialEditMode;
            ucCapabilityMateriality.IsPartialEditMode = isPartialEditMode;
            pnlRiskRating.Enabled = bEnable;
            pnlMendatoryReportSignOff.Enabled = bEnable;
            pnlKeyAccount.Enabled = bEnable;
            pnlNetAccount.Enabled = bEnable;
            pnlZBA.Enabled = bEnable;
            pnlCertification.Enabled = bEnable;
            pnlCEOCert.Enabled = bEnable;
            pnlReviewNotes.Enabled = bEnable;
            pnlUnexpVar.Enabled = bEnable;
            pnlJE.Enabled = bEnable;
            pnlReportingCurrency.Enabled = bEnable;
            btnSubmit.Enabled = !isCertificationStarted && (bEnable || isPeriodInProgress);
            pnlDueDateByAccount.Enabled = bEnable;
            // pnlRecControlChecklist.Enabled = bEnable;
        }
    }

    #endregion

    #region Other Methods
    protected void DecideYseNoSelectionFromDB(IList<CompanyCapabilityInfo> oCompanyCapabilityInfoCollection)
    {
        InitiateFlagsWithNullValue();

        foreach (CompanyCapabilityInfo oCompanyCapabilityInfo in oCompanyCapabilityInfoCollection)
        {
            ARTEnums.Capability eCapability = (ARTEnums.Capability)oCompanyCapabilityInfo.CapabilityID;

            switch (eCapability)
            {
                case ARTEnums.Capability.MaterialitySelection://Materiality Selection
                    _isMaterialityYesChecked = oCompanyCapabilityInfo.IsActivated;
                    _isMaterialityForwarded = oCompanyCapabilityInfo.IsCarryForwardedFromPreviousRecPeriod;
                    break;
                case ARTEnums.Capability.RiskRating://Risk Rating
                    _isRiskRatingYesChecked = oCompanyCapabilityInfo.IsActivated;
                    _isRiskRatingForwarded = oCompanyCapabilityInfo.IsCarryForwardedFromPreviousRecPeriod;
                    break;
                case ARTEnums.Capability.KeyAccount://Key Account
                    _isKeyAccountYesChecked = oCompanyCapabilityInfo.IsActivated;
                    _isKeyAccountForwarded = oCompanyCapabilityInfo.IsCarryForwardedFromPreviousRecPeriod;
                    break;
                case ARTEnums.Capability.DualLevelReview://Dual Level Review
                    _isDualReviewYesChecked = oCompanyCapabilityInfo.IsActivated;
                    _isDualReviewForwarded = oCompanyCapabilityInfo.IsCarryForwardedFromPreviousRecPeriod;
                    break;
                case ARTEnums.Capability.MandatoryReportSignoff://Mandatory Report Sign off
                    _isMandatoryReportSignoffYesChecked = oCompanyCapabilityInfo.IsActivated;
                    _isMandatoryReportSignoffForwarded = oCompanyCapabilityInfo.IsCarryForwardedFromPreviousRecPeriod;
                    break;
                case ARTEnums.Capability.ZeroBalanceAccount://Zero Balance Accounts Selection
                    _isZeroBalanceAccountYesChecked = oCompanyCapabilityInfo.IsActivated;
                    _isZeroBalanceAccountForwarded = oCompanyCapabilityInfo.IsCarryForwardedFromPreviousRecPeriod;
                    break;
                case ARTEnums.Capability.MultiCurrency://Multi-currency capability
                    _isMultiCurrencyYesChecked = oCompanyCapabilityInfo.IsActivated;
                    _isMultiCurrencyForwarded = oCompanyCapabilityInfo.IsCarryForwardedFromPreviousRecPeriod;
                    ucCapabilityMultiCurrency.CurrencyCapabilityInfo = oCompanyCapabilityInfo;
                    break;
                case ARTEnums.Capability.ReopenRecOnCCYreload://Recopen Rec on CCY reload capability
                    _isReopenRecOnCCYreloadYesChecked = oCompanyCapabilityInfo.IsActivated;
                    _isReopenRecOnCCYreloadForwarded = oCompanyCapabilityInfo.IsCarryForwardedFromPreviousRecPeriod;
                    break;
                case ARTEnums.Capability.CertificationActivation://Certification Activation
                    _isCertificationActivationYesChecked = oCompanyCapabilityInfo.IsActivated;
                    _isCertificationActivationForwarded = oCompanyCapabilityInfo.IsCarryForwardedFromPreviousRecPeriod;
                    break;
                //case 9://Certification Lock Down
                //    optCertificationActivationYes
                //    break;
                case ARTEnums.Capability.NetAccount://Net Account
                    _isNetAccountYesChecked = oCompanyCapabilityInfo.IsActivated;
                    _isNetAccountForwarded = oCompanyCapabilityInfo.IsCarryForwardedFromPreviousRecPeriod;
                    break;
                case ARTEnums.Capability.CEOCFOCertification://CEO/CFO Certification Activation
                    _isCeoCfoCertificationYesChecked = oCompanyCapabilityInfo.IsActivated;
                    _isCeoCfoCertificationForwarded = oCompanyCapabilityInfo.IsCarryForwardedFromPreviousRecPeriod;
                    break;
                case ARTEnums.Capability.AllowDeletionOfReviewNotes://AllowDeletionOfReviewNotes
                    _isAllowDeletionOfReviewNotesYesChecked = oCompanyCapabilityInfo.IsActivated;
                    _isAllowDeletionOfReviewNotesForwarded = oCompanyCapabilityInfo.IsCarryForwardedFromPreviousRecPeriod;
                    break;
                case ARTEnums.Capability.JournalEntryWriteOffOnApprover:
                    _isJournalEntryWriteOffOnApproverYesChecked = oCompanyCapabilityInfo.IsActivated;
                    _isJournalEntryWriteOffOnApproverForwarded = oCompanyCapabilityInfo.IsCarryForwardedFromPreviousRecPeriod;
                    break;
                case ARTEnums.Capability.DueDateByAccount://Due Dates By Account Selection
                    _isDueDateByAccountYesChecked = oCompanyCapabilityInfo.IsActivated;
                    _isDueDateByAccountForwarded = oCompanyCapabilityInfo.IsCarryForwardedFromPreviousRecPeriod;
                    break;
                case ARTEnums.Capability.RecControlChecklist://Rec Control Checklist Selection
                    _isRecControlChecklistYesChecked = oCompanyCapabilityInfo.IsActivated;
                    _isRecControlChecklistForwarded = oCompanyCapabilityInfo.IsCarryForwardedFromPreviousRecPeriod;
                    break;
            }
        }

        SetCarryforwardedStatusAllCapability();
    }

    protected void MakeYseNoSelectionFromDB2(IList<CompanyCapabilityInfo> oCompanyCapabilityInfoCollection)
    {
        DecideYseNoSelectionFromDB(oCompanyCapabilityInfoCollection);
        ucCapabilityMateriality.IsMaterialityYesChecked = _isMaterialityYesChecked;
        ucCapabilityRiskRatingAll.IsRiskRatingYesChecked = _isRiskRatingYesChecked;
        Helper.SetYesNoRadioButtons(optKeyAccountYes, optKeyAccountNo, _isKeyAccountYesChecked);
        ucCapabilityActivationDualLevelReview.IsDualLevelReviewYesChecked = _isDualReviewYesChecked;
        ucCapabilityDueDateByAccount.IsDueDateByAccountYesChecked = _isDueDateByAccountYesChecked;
        // Helper.SetYesNoRadioButtons(optDualReviewYes, optDualReviewNo, _isDualReviewYesChecked);
        ucCapabilityMandatoryReportSignoff.IsMandatoryReportSignoffYesChecked = _isMandatoryReportSignoffYesChecked;
        Helper.SetYesNoRadioButtons(optZeroBalanceAccountYes, optZeroBalanceAccountNo, _isZeroBalanceAccountYesChecked);
        ucCapabilityMultiCurrency.IsMultiCurrencyYesChecked = _isMultiCurrencyYesChecked;
        //Helper.SetYesNoRadioButtons(optMultiCurrencyYes, optMultiCurrencyNo, _isMultiCurrencyYesChecked);
        //Helper.SetYesNoRadioButtons(optReopenRecOnCCYreloadYes, optReopenRecOnCCYreloadNo, _isReopenRecOnCCYreloadYesChecked);
        Helper.SetYesNoRadioButtons(optCertificationActivationYes, optCertificationActivationNo, _isCertificationActivationYesChecked);
        Helper.SetYesNoRadioButtons(optNetAccountYes, optNetAccountNo, _isNetAccountYesChecked);
        Helper.SetYesNoRadioButtons(optCeoCfoCertificationYes, optCeoCfoCertificationNo, _isCeoCfoCertificationYesChecked);
        //Helper.SetYesNoRadioButtons(optAllowDeletionOfReviewNotesYes, optAllowDeletionOfReviewNotesNo, _isAllowDeletionOfReviewNotesForwarded);
        Helper.SetYesNoRadioButtons(optAllowDeletionOfReviewNotesYes, optAllowDeletionOfReviewNotesNo, _isAllowDeletionOfReviewNotesYesChecked);
        Helper.SetYesNoRadioButtons(optJEWriteOffOnApproverYes, optJEWriteOffOnApproverNo, _isJournalEntryWriteOffOnApproverYesChecked);
        //Helper.SetYesNoRadioButtons(OptDueDateByAccountYes, OptDueDateByAccountNo, _isDueDateByAccountYesChecked);
        //Helper.SetYesNoRadioButtons(optRecControlChecklistYes, optRecControlChecklistNo, _isRecControlChecklistYesChecked);
        CapabilityActivationRCCL.IsRCCLYesChecked = _isRecControlChecklistYesChecked;
    }

    protected List<CompanyCapabilityInfo> GetCapabilitySelectionFromUI(out bool IsAnythingChanged)
    {
        //TODO: Use enum instead of 1,2,3 etc
        CompanyCapabilityInfo oCompanyCapabilityInfo = null;
        List<CompanyCapabilityInfo> oCompanyCapabilityInfoCollection = new List<CompanyCapabilityInfo>();
        IsAnythingChanged = false;
        oCompanyCapabilityInfo = new CompanyCapabilityInfo();
        oCompanyCapabilityInfo.CapabilityID = (short)ARTEnums.Capability.MaterialitySelection;
        oCompanyCapabilityInfo.IsActivated = ucCapabilityMateriality.IsMaterialityYesCheckedCurrent;//could be true false or null for 3 states
        if (oCompanyCapabilityInfo.IsActivated.HasValue)
        {
            if (!_isMaterialityYesChecked.HasValue || (_isMaterialityYesChecked.HasValue && _isMaterialityYesChecked.Value != oCompanyCapabilityInfo.IsActivated.Value))
                IsAnythingChanged = true;
            oCompanyCapabilityInfoCollection.Add(oCompanyCapabilityInfo);

        }
        oCompanyCapabilityInfo = new CompanyCapabilityInfo();
        oCompanyCapabilityInfo.CapabilityID = (short)ARTEnums.Capability.RiskRating;
        oCompanyCapabilityInfo.IsActivated = ucCapabilityRiskRatingAll.IsRiskRatingYesChecked;//could be true false or null for 3 states
        if (oCompanyCapabilityInfo.IsActivated.HasValue)
        {
            if (!_isRiskRatingYesChecked.HasValue || (_isRiskRatingYesChecked.HasValue && _isRiskRatingYesChecked.Value != oCompanyCapabilityInfo.IsActivated.Value))
                IsAnythingChanged = true;
            oCompanyCapabilityInfoCollection.Add(oCompanyCapabilityInfo);
        }

        oCompanyCapabilityInfo = new CompanyCapabilityInfo();
        oCompanyCapabilityInfo.CapabilityID = (short)ARTEnums.Capability.KeyAccount;
        oCompanyCapabilityInfo.IsActivated = Helper.SetSetFlagBasedOnYesNoRadioButtons(optKeyAccountYes, optKeyAccountNo);
        if (oCompanyCapabilityInfo.IsActivated.HasValue)
        {
            if (trKeyAccount.Visible && (!_isKeyAccountYesChecked.HasValue || (_isKeyAccountYesChecked.HasValue && _isKeyAccountYesChecked.Value != oCompanyCapabilityInfo.IsActivated.Value)))
                IsAnythingChanged = true;
            oCompanyCapabilityInfoCollection.Add(oCompanyCapabilityInfo);
        }

        oCompanyCapabilityInfo = new CompanyCapabilityInfo();
        oCompanyCapabilityInfo.CapabilityID = (short)ARTEnums.Capability.DualLevelReview;
        // oCompanyCapabilityInfo.IsActivated = Helper.SetSetFlagBasedOnYesNoRadioButtons(optDualReviewYes, optDualReviewNo);
        oCompanyCapabilityInfo.IsActivated = ucCapabilityActivationDualLevelReview.IsDualLevelReviewYesCheckedCurrent;
        if (oCompanyCapabilityInfo.IsActivated.HasValue)
        {
            if (trDualReview.Visible && (!_isDualReviewYesChecked.HasValue || (_isDualReviewYesChecked.HasValue && _isDualReviewYesChecked.Value != oCompanyCapabilityInfo.IsActivated.Value)))
                IsAnythingChanged = true;
            oCompanyCapabilityInfoCollection.Add(oCompanyCapabilityInfo);
        }


        oCompanyCapabilityInfo = new CompanyCapabilityInfo();
        oCompanyCapabilityInfo.CapabilityID = (short)ARTEnums.Capability.MandatoryReportSignoff;
        oCompanyCapabilityInfo.IsActivated = ucCapabilityMandatoryReportSignoff.IsMandatoryReportSignoffYesCheckedCurrent;//could be true false or null for 3 states
        if (oCompanyCapabilityInfo.IsActivated.HasValue)
        {
            if (trMandatoryReportSignoff.Visible && (!_isMandatoryReportSignoffYesChecked.HasValue || (_isMandatoryReportSignoffYesChecked.HasValue && _isMandatoryReportSignoffYesChecked.Value != oCompanyCapabilityInfo.IsActivated.Value)))
                IsAnythingChanged = true;
            oCompanyCapabilityInfoCollection.Add(oCompanyCapabilityInfo);
        }


        oCompanyCapabilityInfo = new CompanyCapabilityInfo();
        oCompanyCapabilityInfo.CapabilityID = (short)ARTEnums.Capability.ZeroBalanceAccount;
        oCompanyCapabilityInfo.IsActivated = Helper.SetSetFlagBasedOnYesNoRadioButtons(optZeroBalanceAccountYes, optZeroBalanceAccountNo);
        if (oCompanyCapabilityInfo.IsActivated.HasValue)
        {
            if (!_isZeroBalanceAccountYesChecked.HasValue || (_isZeroBalanceAccountYesChecked.HasValue && _isZeroBalanceAccountYesChecked.Value != oCompanyCapabilityInfo.IsActivated.Value))
                IsAnythingChanged = true;
            oCompanyCapabilityInfoCollection.Add(oCompanyCapabilityInfo);
        }


        oCompanyCapabilityInfo = ucCapabilityMultiCurrency.GetCompanyCapabilityInfoToSave();
        //oCompanyCapabilityInfo.CapabilityID = (short)ARTEnums.Capability.MultiCurrency;
        //oCompanyCapabilityInfo.IsActivated = ucCapabilityMultiCurrency.IsMultiCurrencyYesCheckedCurrent; //Helper.SetSetFlagBasedOnYesNoRadioButtons(optMultiCurrencyYes, optMultiCurrencyNo);
        if (oCompanyCapabilityInfo.IsActivated.HasValue)
        {
            if (trMultiCurrency.Visible && ucCapabilityMultiCurrency.IsValueChanged())
                IsAnythingChanged = true;
            oCompanyCapabilityInfoCollection.Add(oCompanyCapabilityInfo);
        }

        //oCompanyCapabilityInfo = new CompanyCapabilityInfo();
        //oCompanyCapabilityInfo.CapabilityID = (short)ARTEnums.Capability.ReopenRecOnCCYreload;
        //oCompanyCapabilityInfo.IsActivated = Helper.SetSetFlagBasedOnYesNoRadioButtons(optReopenRecOnCCYreloadYes, optReopenRecOnCCYreloadNo);
        //if (oCompanyCapabilityInfo.IsActivated.HasValue)
        //{
        //    if (trReopenRecOnCCYreload.Visible && (!_isReopenRecOnCCYreloadYesChecked.HasValue || (_isReopenRecOnCCYreloadYesChecked.HasValue && _isReopenRecOnCCYreloadYesChecked.Value != oCompanyCapabilityInfo.IsActivated.Value)))
        //        IsAnythingChanged = true;
        //    oCompanyCapabilityInfoCollection.Add(oCompanyCapabilityInfo);
        //}

        oCompanyCapabilityInfo = new CompanyCapabilityInfo();
        oCompanyCapabilityInfo.CapabilityID = (short)ARTEnums.Capability.CertificationActivation;
        oCompanyCapabilityInfo.IsActivated = Helper.SetSetFlagBasedOnYesNoRadioButtons(optCertificationActivationYes, optCertificationActivationNo);
        if (oCompanyCapabilityInfo.IsActivated.HasValue)
        {
            if (trCertActication.Visible && (!_isCertificationActivationYesChecked.HasValue || (_isCertificationActivationYesChecked.HasValue && _isCertificationActivationYesChecked.Value != oCompanyCapabilityInfo.IsActivated.Value)))
                IsAnythingChanged = true;
            oCompanyCapabilityInfoCollection.Add(oCompanyCapabilityInfo);
        }

        oCompanyCapabilityInfo = new CompanyCapabilityInfo();
        oCompanyCapabilityInfo.CapabilityID = (short)ARTEnums.Capability.NetAccount;
        oCompanyCapabilityInfo.IsActivated = Helper.SetSetFlagBasedOnYesNoRadioButtons(optNetAccountYes, optNetAccountNo);
        if (oCompanyCapabilityInfo.IsActivated.HasValue)
        {
            if (!_isNetAccountYesChecked.HasValue || (_isNetAccountYesChecked.HasValue && _isNetAccountYesChecked.Value != oCompanyCapabilityInfo.IsActivated.Value))
                IsAnythingChanged = true;
            oCompanyCapabilityInfoCollection.Add(oCompanyCapabilityInfo);
        }

        oCompanyCapabilityInfo = new CompanyCapabilityInfo();
        oCompanyCapabilityInfo.CapabilityID = (short)ARTEnums.Capability.CEOCFOCertification;
        oCompanyCapabilityInfo.IsActivated = Helper.SetSetFlagBasedOnYesNoRadioButtons(optCeoCfoCertificationYes, optCeoCfoCertificationNo);
        if (oCompanyCapabilityInfo.IsActivated.HasValue)
        {
            if (trCEOCert.Visible && (!_isCeoCfoCertificationYesChecked.HasValue || (_isCeoCfoCertificationYesChecked.HasValue && _isCeoCfoCertificationYesChecked.Value != oCompanyCapabilityInfo.IsActivated.Value)))
                IsAnythingChanged = true;
            oCompanyCapabilityInfoCollection.Add(oCompanyCapabilityInfo);
        }

        oCompanyCapabilityInfo = new CompanyCapabilityInfo();
        oCompanyCapabilityInfo.CapabilityID = (short)ARTEnums.Capability.AllowDeletionOfReviewNotes;
        oCompanyCapabilityInfo.IsActivated = Helper.SetSetFlagBasedOnYesNoRadioButtons(optAllowDeletionOfReviewNotesYes, optAllowDeletionOfReviewNotesNo);
        if (oCompanyCapabilityInfo.IsActivated.HasValue)
        {
            if (!_isAllowDeletionOfReviewNotesYesChecked.HasValue || (_isAllowDeletionOfReviewNotesYesChecked.HasValue && _isAllowDeletionOfReviewNotesYesChecked.Value != oCompanyCapabilityInfo.IsActivated.Value))
                IsAnythingChanged = true;
            oCompanyCapabilityInfoCollection.Add(oCompanyCapabilityInfo);
        }

        oCompanyCapabilityInfo = new CompanyCapabilityInfo();
        oCompanyCapabilityInfo.CapabilityID = (short)ARTEnums.Capability.JournalEntryWriteOffOnApprover;
        oCompanyCapabilityInfo.IsActivated = Helper.SetSetFlagBasedOnYesNoRadioButtons(optJEWriteOffOnApproverYes, optJEWriteOffOnApproverNo);
        if (oCompanyCapabilityInfo.IsActivated.HasValue)
        {
            if (trJE.Visible && (!_isJournalEntryWriteOffOnApproverYesChecked.HasValue || (_isJournalEntryWriteOffOnApproverYesChecked.HasValue && _isJournalEntryWriteOffOnApproverYesChecked.Value != oCompanyCapabilityInfo.IsActivated.Value)))
                IsAnythingChanged = true;
            oCompanyCapabilityInfoCollection.Add(oCompanyCapabilityInfo);
        }

        oCompanyCapabilityInfo = new CompanyCapabilityInfo();
        oCompanyCapabilityInfo.CapabilityID = (short)ARTEnums.Capability.DueDateByAccount;
        oCompanyCapabilityInfo.IsActivated = ucCapabilityDueDateByAccount.IsDueDateByAccountYesCheckedCurrent;//Helper.SetSetFlagBasedOnYesNoRadioButtons(OptDueDateByAccountYes, OptDueDateByAccountNo);
        if (oCompanyCapabilityInfo.IsActivated.HasValue)
        {
            if (trDueDateByAccount.Visible && (!_isDueDateByAccountYesChecked.HasValue || (_isDueDateByAccountYesChecked.HasValue && _isDueDateByAccountYesChecked.Value != oCompanyCapabilityInfo.IsActivated.Value)))
                IsAnythingChanged = true;
            oCompanyCapabilityInfoCollection.Add(oCompanyCapabilityInfo);
        }

        oCompanyCapabilityInfo = new CompanyCapabilityInfo();
        oCompanyCapabilityInfo.CapabilityID = (short)ARTEnums.Capability.RecControlChecklist;
        //oCompanyCapabilityInfo.IsActivated = Helper.SetSetFlagBasedOnYesNoRadioButtons(optRecControlChecklistYes, optRecControlChecklistNo);
        oCompanyCapabilityInfo.IsActivated = CapabilityActivationRCCL.IsRCCLYesCheckedCurrent;
        if (oCompanyCapabilityInfo.IsActivated.HasValue)
        {
            if (trRCCL.Visible && (!_isRecControlChecklistYesChecked.HasValue || (_isRecControlChecklistYesChecked.HasValue && _isRecControlChecklistYesChecked.Value != oCompanyCapabilityInfo.IsActivated.Value)))
                IsAnythingChanged = true;
            oCompanyCapabilityInfoCollection.Add(oCompanyCapabilityInfo);
        }

        return oCompanyCapabilityInfoCollection;
    }

    protected void HandleDualReviewYesNoChecked(object sender, EventArgs e)
    {
        try
        {
            //ToDO: do we need to check for null case or NONE case
            // _isDualReviewYesCheckedFromUI = Helper.SetSetFlagBasedOnYesNoRadioButtons(optDualReviewYes, optDualReviewNo);
            _isDualReviewYesCheckedFromUI = ucCapabilityActivationDualLevelReview.IsDualLevelReviewYesCheckedCurrent;
            if (_isDualReviewYesCheckedFromUI.HasValue)
            {
                ucCapabilityMandatoryReportSignoff.IsDualReviewYesChecked = _isDualReviewYesCheckedFromUI.Value;
            }
            else
                ucCapabilityMandatoryReportSignoff.IsDualReviewYesChecked = false;
            ucCapabilityMandatoryReportSignoff.IsMandatoryReportSignoffYesChecked = _isMandatoryReportSignoffYesChecked;

            ucCapabilityMandatoryReportSignoff.ChangedEventHandler();
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

    protected bool SaveAll()
    {
        bool IsAnythingChanged;
        bool isDualReviewYesCheckedFromUI = false;
        bool? isMaterialityYesCheckedFromUI = null;
        bool isRiskRatingYesCheckedFromUI = false;
        bool isMandatoryReportSignoffYesCheckedFromUI = false;

        //Revision fields for CompanySetting
        DateTime dateRevised = DateTime.Now;
        string revisedBy = SessionHelper.CurrentUserLoginID;

        //if (optDualReviewYes.Checked == true)
        //{
        //    _isDualReviewYesChecked = true;
        //    ucCapabilityMandatoryReportSignoff.IsDualReviewYesChecked = _isDualReviewYesChecked.Value;
        //    isDualReviewYesCheckedFromUI = true;
        //}

        if (ucCapabilityActivationDualLevelReview.IsDualLevelReviewYesCheckedCurrent.HasValue && ucCapabilityActivationDualLevelReview.IsDualLevelReviewYesCheckedCurrent.Value)
        {
            _isDualReviewYesChecked = true;
            ucCapabilityMandatoryReportSignoff.IsDualReviewYesChecked = _isDualReviewYesChecked.Value;
            isDualReviewYesCheckedFromUI = true;
        }
        bool IsDualReviewTypeToBeSaved = false;
        if (isDualReviewYesCheckedFromUI && ucCapabilityActivationDualLevelReview.IsValueChanged())
            IsDualReviewTypeToBeSaved = true;
        bool IsDayTypeToBeSaved = false;
        if (ucCapabilityDueDateByAccount.IsValueChanged())
            IsDayTypeToBeSaved = true;

        bool IsRCCLValidationTypeToBeSaved = false;
        CompanySettingInfo oCompanySettingInfoRCCL = null;
        if (CapabilityActivationRCCL.IsRCCLYesCheckedCurrent.HasValue && CapabilityActivationRCCL.IsRCCLYesCheckedCurrent.Value && CapabilityActivationRCCL.IsValueChanged())
        {
            IsRCCLValidationTypeToBeSaved = true;
            oCompanySettingInfoRCCL = CapabilityActivationRCCL.GetCompanySettingObjectToBeSavedFromUC();
        }

        if (ucCapabilityMateriality.IsMaterialityYesCheckedCurrent.HasValue)
            isMaterialityYesCheckedFromUI = ucCapabilityMateriality.IsMaterialityYesCheckedCurrent;

        if (ucCapabilityRiskRatingAll.IsRiskRatingYesChecked.HasValue)
            isRiskRatingYesCheckedFromUI = ucCapabilityRiskRatingAll.IsRiskRatingYesChecked.Value;

        if (ucCapabilityMandatoryReportSignoff.IsMandatoryReportSignoffYesCheckedCurrent.HasValue && ucCapabilityMandatoryReportSignoff.IsMandatoryReportSignoffYesCheckedCurrent.Value)
            isMandatoryReportSignoffYesCheckedFromUI = ucCapabilityMandatoryReportSignoff.IsMandatoryReportSignoffYesCheckedCurrent.Value;

        List<CompanyCapabilityInfo> oCompanyCapabilityInfoCollection = GetCapabilitySelectionFromUI(out IsAnythingChanged);

        CompanySettingInfo oCompanySettingInfo = ucCapabilityMateriality.GetCompanySettingObjectToBeSavedFromUC();
        
        CompanySettingInfo oDualLevelReviewCompanySetting = ucCapabilityActivationDualLevelReview.GetCompanySettingObjectToBeSavedFromUC();
        oCompanySettingInfo.DualLevelReviewTypeID = oDualLevelReviewCompanySetting.DualLevelReviewTypeID;

        CompanySettingInfo oDueDateByAccountCompanySetting = ucCapabilityDueDateByAccount.GetCompanySettingObjectToBeSavedFromUC();
        oCompanySettingInfo.DayTypeID = oDueDateByAccountCompanySetting.DayTypeID;

        IList<FSCaptionMaterialityInfo> oFSCaptionMaterialityInfoCollection = ucCapabilityMateriality.GetFSCaptionMaterialityObjectToBeSavedFromUC();

        IList<RoleMandatoryReportInfo> oRoleMandatoryReportInfoCollection = null;
        if (trMandatoryReportSignoff.Visible && ucCapabilityMandatoryReportSignoff.IsValueChanged())
            oRoleMandatoryReportInfoCollection = ucCapabilityMandatoryReportSignoff.GetMandatoryReportObjectToBeSavedFromUC();

        IList<RiskRatingReconciliationFrequencyInfo> oRiskRatingReconciliationFrequencyInfoCollection = ucCapabilityRiskRatingAll.GetRiskRatingReconciliationFrequencyObjectToBeSavedFromUC();
        IList<RiskRatingReconciliationPeriodInfo> oRiskRatingReconciliationPeriodInfoCollection = ucCapabilityRiskRatingAll.GetRiskRatingReconciliationPeriodObjectToBeSavedFromUC();
        bool IsRiskRatingValueChanged = ucCapabilityRiskRatingAll.IsValueChanged();

        IList<ReconciliationPeriodInfo> oReconciliationPeriodInfoCollection = null;
        decimal? newUnexplainedVarianceThreshold = null;
        CompanyUnexplainedVarianceThresholdInfo oCompanyUnexplainedVarianceThresholdInfo = new CompanyUnexplainedVarianceThresholdInfo();
        oCompanyUnexplainedVarianceThresholdInfo.CompanyID = SessionHelper.CurrentCompanyID;
        oCompanyUnexplainedVarianceThresholdInfo.CompanyUnexplainedVarianceThreshold = decimal.Parse(txtUnexpVarThreshold.Text);
        newUnexplainedVarianceThreshold = oCompanyUnexplainedVarianceThresholdInfo.CompanyUnexplainedVarianceThreshold;
        if (oCompanySettingInfo != null)
        {
            if (IsRCCLValidationTypeToBeSaved && oCompanySettingInfoRCCL != null)
                oCompanySettingInfo.RCCValidationTypeID = oCompanySettingInfoRCCL.RCCValidationTypeID;
        }
        else
        {
            if (IsRCCLValidationTypeToBeSaved && oCompanySettingInfoRCCL != null)
                oCompanySettingInfo = oCompanySettingInfoRCCL;
        }
        decimal? oldUnexplainedVarianceThreshold = null;
        if (!string.IsNullOrEmpty(hdnUnexpVarThresholdVal.Value))
            oldUnexplainedVarianceThreshold = decimal.Parse(hdnUnexpVarThresholdVal.Value);

        bool IsMaterialityToBeSaved = ucCapabilityMateriality.IsValueChanged();

        if (
            (trRCCL.Visible && IsRCCLValidationTypeToBeSaved)
            || IsDualReviewTypeToBeSaved
            || IsDayTypeToBeSaved
            || IsRiskRatingValueChanged
            || (trMandatoryReportSignoff.Visible && ucCapabilityMandatoryReportSignoff.IsValueChanged())
            || oldUnexplainedVarianceThreshold.GetValueOrDefault() != newUnexplainedVarianceThreshold.GetValueOrDefault()
            || IsMaterialityToBeSaved)
            IsAnythingChanged = true;

        oCompanyClient.SaveAllCompanyCapability(SessionHelper.CurrentReconciliationPeriodID
            , isDualReviewYesCheckedFromUI
            , IsDualReviewTypeToBeSaved
            , isMaterialityYesCheckedFromUI
            , isRiskRatingYesCheckedFromUI
            , isMandatoryReportSignoffYesCheckedFromUI
            , IsRCCLValidationTypeToBeSaved
            , IsMaterialityToBeSaved
            , oCompanyCapabilityInfoCollection
            , oCompanySettingInfo
            , oFSCaptionMaterialityInfoCollection
            , oRoleMandatoryReportInfoCollection
            , oRiskRatingReconciliationFrequencyInfoCollection
            , oRiskRatingReconciliationPeriodInfoCollection
            , IsRiskRatingValueChanged
            , oReconciliationPeriodInfoCollection
            , oCompanyUnexplainedVarianceThresholdInfo
            , IsDayTypeToBeSaved
            , dateRevised
            , revisedBy
            , Helper.GetAppUserInfo()
            );


        Session.Remove(SessionHelper.GetSessionKeyForDualLevelReviewType());

        return IsAnythingChanged;

    }

    public void SetCarryforwardedStatusAllCapability()
    {
        ucCapabilityMateriality.IsMaterialityForwarded = _isMaterialityForwarded;
        ucCapabilityRiskRatingAll.IsRiskRatingForwarded = _isRiskRatingForwarded;
        ucCapabilityMandatoryReportSignoff.IsMandatoryReportSignoffForwarded = _isMandatoryReportSignoffForwarded;
        //Certification Activation also

        Helper.SetCarryforwardedStatus(imgStatusKeyAccountForwardYes, imgStatusKeyAccountForwardNo, _isKeyAccountForwarded);
        ucCapabilityActivationDualLevelReview.IsDualLevelReviewForwarded = _isDualReviewForwarded;
        //Helper.SetCarryforwardedStatus(imgStatusDualReviewForwardYes, imgStatusDualReviewForwardNo, _isDualReviewForwarded);
        Helper.SetCarryforwardedStatus(imgStatusZeroBalanceAccountForwardYes, imgStatusZeroBalanceAccountForwardNo, _isZeroBalanceAccountForwarded);
        ucCapabilityMultiCurrency.IsMultiCurrencyForwarded = _isMultiCurrencyForwarded;
        //Helper.SetCarryforwardedStatus(imgStatusMultiCurrencyForwardYes, imgStatusMultiCurrencyForwardNo, _isMultiCurrencyForwarded);
        //Helper.SetCarryforwardedStatus(imgStatusReopenRecOnCCYreloadForwardYes, imgStatusReopenRecOnCCYreloadForwardNo, _isReopenRecOnCCYreloadForwarded);
        Helper.SetCarryforwardedStatus(imgStatusNetAccountForwardYes, imgStatusNetAccountForwardNo, _isNetAccountForwarded);
        Helper.SetCarryforwardedStatus(imgStatusCertificationActivationForwardYes, imgStatusCertificationActivationForwardNo, _isCertificationActivationForwarded);
        Helper.SetCarryforwardedStatus(imgStatusCeoCfoCertificationForwardYes, imgStatusCeoCfoCertificationForwardNo, _isCeoCfoCertificationForwarded);
        Helper.SetCarryforwardedStatus(imgAllowDeletionOfReviewNotesForwardYes, imgAllowDeletionOfReviewNotesForwardNo, _isAllowDeletionOfReviewNotesForwarded);
        Helper.SetCarryforwardedStatus(ImgJE1, ImgJE2, _isJournalEntryWriteOffOnApproverForwarded);
        //Helper.SetCarryforwardedStatus(imgStatusDueDateByAccountYes, imgStatusDueDateByAccountNo, _isDueDateByAccountForwarded);
        ucCapabilityDueDateByAccount.IsDueDateByAccountForwarded = _isDueDateByAccountForwarded;
        CapabilityActivationRCCL.IsRCCLForwarded = _isRecControlChecklistForwarded;
        // Helper.SetCarryforwardedStatus(imgRecControlChecklistForwardYes, imgRecControlChecklistForwardNo, _isRecControlChecklistForwarded);


        //SetCarryforwardedStatusCompanyUnexplainedVarianceThreshold();
        //Helper.SetCarryforwardedStatus(imgUnexplainedVarianceThresholdForwardYes, imgUnexplainedVarianceThresholdForwardNo, _isCompanyUnexplainedVarianceThresholdForwarded);
    }
    public void SetCarryforwardedStatusCompanyUnexplainedVarianceThreshold()
    {
        _isCompanyUnexplainedVarianceThresholdForwarded = null;
        oCompanyClient = RemotingHelper.GetCompanyObject();
        IList<CompanyUnexplainedVarianceThresholdInfo> oCompanyUnexplainedVarianceThresholdInfoCollection = oCompanyClient.GetUnexplainedVarianceThresholdByRecPeriodID(SessionHelper.CurrentReconciliationPeriodID, Helper.GetAppUserInfo());
        if (oCompanyUnexplainedVarianceThresholdInfoCollection != null && oCompanyUnexplainedVarianceThresholdInfoCollection.Count > 0)
        {
            _isCompanyUnexplainedVarianceThresholdForwarded = oCompanyUnexplainedVarianceThresholdInfoCollection[0].IsCarryForwardedFromPreviousRecPeriod;
        }
        //Helper.SetCarryforwardedStatus(imgUnexplainedVarianceThresholdForwardYes, imgUnexplainedVarianceThresholdForwardNo, _isCompanyUnexplainedVarianceThresholdForwarded);
    }

    public void InitiateFlagsWithNullValue()
    {
        _isMaterialityYesChecked = null;
        _isRiskRatingYesChecked = null;
        _isKeyAccountYesChecked = null;
        _isDualReviewYesChecked = null;
        _isMandatoryReportSignoffYesChecked = null;
        _isZeroBalanceAccountYesChecked = null;
        _isMultiCurrencyYesChecked = null;
        _isReopenRecOnCCYreloadYesChecked = null;
        _isCertificationActivationYesChecked = null;
        _isNetAccountYesChecked = null;
        _isCeoCfoCertificationYesChecked = null;
        _isAllowDeletionOfReviewNotesYesChecked = null;
        _isJournalEntryWriteOffOnApproverYesChecked = null;
        _isRecControlChecklistYesChecked = null;

        _isMaterialityForwarded = null;
        _isRiskRatingForwarded = null;
        _isKeyAccountForwarded = null;
        _isDualReviewForwarded = null;
        _isMandatoryReportSignoffForwarded = null;
        _isZeroBalanceAccountForwarded = null;
        _isMultiCurrencyForwarded = null;
        _isReopenRecOnCCYreloadForwarded = null;
        _isCertificationActivationForwarded = null;
        _isNetAccountForwarded = null;
        _isCeoCfoCertificationForwarded = null;
        _isAllowDeletionOfReviewNotesForwarded = null;
        _isJournalEntryWriteOffOnApproverForwarded = null;
        _isDueDateByAccountForwarded = null;
        _isRecControlChecklistForwarded = null;

        //_isCompanyUnexplainedVarianceThresholdForwarded = null;
    }

    public override string GetMenuKey()
    {
        return "CapabilitiesActivation";
    }

    protected void cvValidateChanges_ServerValidate(object source, ServerValidateEventArgs args)
    {
        try
        {
            CustomValidator cv = (CustomValidator)source;
            bool IsAnythingChanged;
            List<CompanyCapabilityInfo> oCompanyCapabilityInfoCollection = GetCapabilitySelectionFromUI(out IsAnythingChanged);
            decimal? newUnexplainedVarianceThreshold = null;
            newUnexplainedVarianceThreshold = decimal.Parse(txtUnexpVarThreshold.Text);
            decimal? oldUnexplainedVarianceThreshold = null;
            if (!string.IsNullOrEmpty(hdnUnexpVarThresholdVal.Value))
                oldUnexplainedVarianceThreshold = decimal.Parse(hdnUnexpVarThresholdVal.Value);

            bool IsMaterialityToBeSaved = ucCapabilityMateriality.IsValueChanged();

            if (
                (trRCCL.Visible && CapabilityActivationRCCL.IsValueChanged())
                || ucCapabilityActivationDualLevelReview.IsValueChanged()
                || ucCapabilityRiskRatingAll.IsValueChanged()
                || (trMandatoryReportSignoff.Visible && ucCapabilityMandatoryReportSignoff.IsValueChanged())
                || oldUnexplainedVarianceThreshold.GetValueOrDefault() != newUnexplainedVarianceThreshold.GetValueOrDefault()
                || ucCapabilityMateriality.IsValueChanged()
                || ucCapabilityDueDateByAccount.IsValueChanged()
                || ucCapabilityMultiCurrency.IsValueChanged()
                )
                IsAnythingChanged = true;

            if (!IsAnythingChanged)
            {
                //cvValidateChanges.ErrorMessage = LanguageUtil.GetValue(2877);
                string ErrorMessage = LanguageUtil.GetValue(2877);
                args.IsValid = false;
                Helper.ShowErrorMessage(this, ErrorMessage);               
            }

        }
        catch (ARTException ex)
        {
            Helper.ShowErrorMessage((PageBase)this.Page, ex);
        }
        catch (Exception ex)
        {
            Helper.ShowErrorMessage((PageBase)this.Page, ex);
        }
    }

    #endregion


}