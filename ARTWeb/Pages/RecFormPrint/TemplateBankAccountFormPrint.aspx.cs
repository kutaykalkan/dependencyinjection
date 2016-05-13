using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SkyStem.ART.Client.Data;
using SkyStem.ART.Web.Data;
using SkyStem.ART.Web.Classes;
using SkyStem.ART.Web.Utility;
using SkyStem.ART.Client.Exception;
using SkyStem.ART.Web.UserControls;
using SkyStem.ART.Client.IServices;
using SkyStem.ART.Client.Model;
using SkyStem.Library.Controls.WebControls;

public partial class Pages_RecFormPrint_TemplateBankAccountFormPrint : PageBaseRecForm
{
    const ARTEnums.ReconciliationItemTemplate eReconciliationItemTemplateThisPage = ARTEnums.ReconciliationItemTemplate.BankForm;
    const bool IS_SUPPORTING_DETAIL_ENTRY_ON_TEMPLATE = true;// tells whether sum for SUPPORTING_DETAIL section will come from ItemInput grid or just on entry on the recForm itself
    //bool _isMultiCurrencyActivated = false;
    //bool _isCapabilityDualReview = false;
    const string POPUP_PAGE = "DocumentUpload.aspx?";
    const int POPUP_WIDTH = 800;
    const int POPUP_HEIGHT = 480;
    bool? _IsSummary = true;
    WebEnums.ARTPages _ARTPages = WebEnums.ARTPages.AccountViewer;
    protected void Page_Load(object sender, EventArgs e)
    {

        try
        {
            OnPageLoad();
            SetEditMode();
            if (!_IsSummary.Value)
                LoadDetails();
            RecHelper.ShowHideReviewNotesAndQualityScore(trReviewNotes, trQualityScore, trRecControlCheckList);
        }
        catch (ARTException ex)
        {
            Helper.LogException(ex);
            throw;
        }
        catch (Exception ex)
        {
            Helper.LogException(ex);
            throw;
        }
    }

    private void LoadDetails()
    {
        imgViewBankFee_Click(null, null);
        imgViewNSFFee_Click(null, null);
        imgViewOtherInGLAdjustments_Click(null, null);
        imgViewDepositInTransit_Click(null, null);
        imgViewOutstandingChecks_Click(null, null);
        imgViewOtherInTimingDifference_Click(null, null);
        imgViewRecWriteOff_Click(null, null);
        imgViewUnexplainedVariance_Click(null, null);

    }

    private void OnPageLoad()
    {
        if (Request.QueryString[QueryStringConstants.IS_SUMMARY] != null)
            _IsSummary = Convert.ToBoolean(Convert.ToInt16(Request.QueryString[QueryStringConstants.IS_SUMMARY]));


        string pageID = Request.QueryString[QueryStringConstants.REFERRER_PAGE_ID];
        _ARTPages = (WebEnums.ARTPages)System.Enum.Parse(typeof(WebEnums.ARTPages), pageID);

        if (!string.IsNullOrEmpty(Request.QueryString[QueryStringConstants.GLDATA_ID]))
        {
            long? _gLDataID = Convert.ToInt64(Request.QueryString[QueryStringConstants.GLDATA_ID]);
            this.GLDataHdrInfo = Helper.GetGLDataHdrInfo(_gLDataID);
        }
        //string queryString = "?" + QueryStringConstants.ACCOUNT_ID + "=" + _accountID + "&" + QueryStringConstants.GLDATA_ID + "=" + _gLDataID;
        lblPeriodEndDate.Text = string.Format(WebConstants.FORMAT_BRACKET, Helper.GetDisplayDate(SessionHelper.CurrentReconciliationPeriodEndDate));

        SetAllLabels();
        ManagePackageFeatures();
       // ShowTaskStatus();

        //RegisterToggleControl();

        setEntityNameLabelIDForGLAdjustments();

        RecHelper.SetRecStatusBarPropertiesForRecFormPrint(this, GLDataID);
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

    //private void RegisterToggleControl()
    //{
    //    //Register toggle control with User Control
    //    uctlBankFee.RegisterToggleControl(imgViewBankFee);
    //    uctlNSFFee.RegisterToggleControl(imgViewNSFFee);
    //    uctlOtherInGLAdjustments.RegisterToggleControl(imgViewOtherInGLAdjustments);
    //    uctlDepositInTransit.RegisterToggleControl(imgViewDepositInTransit);
    //    uctlOutstandingChecks.RegisterToggleControl(imgViewOutstandingChecks);
    //    uctlOtherInTimingDifference.RegisterToggleControl(imgViewOtherInTimingDifference);
    //    uctlItemInputWriteOff.RegisterToggleControl(imgViewRecWriteOff);
    //    uctlUnexplainedVariance.RegisterToggleControl(imgViewUnexplainedVariance);
    //}

    public override string GetMenuKey()
    {
        return Helper.GetMenuKeyForRecForms(_ARTPages);
    }


    void imgViewUnexplainedVariance_Click(object sender, ImageClickEventArgs e)
    {
        this.uctlUnexplainedVariance.IsExpanded = true;
        uctlUnexplainedVariance.IsRegisterPDFAndExcelForPostback = false;
        this.LoadUnexplainedVariance();
        this.uctlUnexplainedVariance.ContentVisibility = true;
    }

    void imgViewRecWriteOff_Click(object sender, ImageClickEventArgs e)
    {
        uctlItemInputWriteOff.IsExpanded = true;
        uctlItemInputWriteOff.IsRegisterPDFAndExcelForPostback = false;
        this.LoadRecItemsItemWriteOff();
        uctlItemInputWriteOff.ContentVisibility = true;
    }

    void imgViewOtherInTimingDifference_Click(object sender, ImageClickEventArgs e)
    {
        uctlOtherInTimingDifference.IsExpanded = true;
        //uctlOtherInTimingDifference.IsRegisterPDFAndExcelForPostback = false;
        //RecFormHelper.LoadRecItemsGLAdjustment(uctlOtherInTimingDifference, WebEnums.RecCategory.TimingDifference, WebEnums.RecCategoryType.BankAccount_TimingDifference_Other, _accountID, _netAccountID, _IsSRA);
        this.LoadRecItemsGLAdjustment(uctlOtherInTimingDifference, WebEnums.RecCategory.TimingDifference, WebEnums.RecCategoryType.BankAccount_TimingDifference_Other);
        uctlOtherInTimingDifference.ContentVisibility = true;
    }

    void imgViewOutstandingChecks_Click(object sender, ImageClickEventArgs e)
    {
        uctlOutstandingChecks.IsExpanded = true;
        //uctlOutstandingChecks.IsRegisterPDFAndExcelForPostback = false;
        this.LoadRecItemsGLAdjustment(uctlOutstandingChecks, WebEnums.RecCategory.TimingDifference, WebEnums.RecCategoryType.BankAccount_TimingDifference_OutstandingChecks);
        uctlOutstandingChecks.ContentVisibility = true;
    }

    void imgViewDepositInTransit_Click(object sender, ImageClickEventArgs e)
    {
        uctlDepositInTransit.IsExpanded = true;
        //uctlDepositInTransit.IsRegisterPDFAndExcelForPostback = false;
        this.LoadRecItemsGLAdjustment(uctlDepositInTransit, WebEnums.RecCategory.TimingDifference, WebEnums.RecCategoryType.BankAccount_TimingDifference_DepositsInTransit);
        uctlDepositInTransit.ContentVisibility = true;
    }

    void imgViewOtherInGLAdjustments_Click(object sender, ImageClickEventArgs e)
    {
        uctlOtherInGLAdjustments.IsExpanded = true;
        //uctlOtherInGLAdjustments.IsRegisterPDFAndExcelForPostback = false;
        this.LoadRecItemsGLAdjustment(uctlOtherInGLAdjustments, WebEnums.RecCategory.GLAdjustments, WebEnums.RecCategoryType.BankAccount_GLAdjustments_Other);
        uctlOtherInGLAdjustments.ContentVisibility = true;
    }

    void imgViewNSFFee_Click(object sender, ImageClickEventArgs e)
    {
        uctlNSFFee.IsExpanded = true;
        //uctlNSFFee.IsRegisterPDFAndExcelForPostback = false;
        this.LoadRecItemsGLAdjustment(uctlNSFFee, WebEnums.RecCategory.GLAdjustments, WebEnums.RecCategoryType.BankAccount_GLAdjustments_NSFFees);
        uctlNSFFee.ContentVisibility = true;
    }

    void imgViewBankFee_Click(object sender, ImageClickEventArgs e)
    {
        uctlBankFee.IsExpanded = true;
        //uctlBankFee.IsRegisterPDFAndExcelForPostback = false;
        this.LoadRecItemsGLAdjustment(uctlBankFee, WebEnums.RecCategory.GLAdjustments, WebEnums.RecCategoryType.BankAccount_GLAdjustments_BankFees);
        uctlBankFee.ContentVisibility = true;

    }

    void LoadRecItemsGLAdjustment(UserControls_GLAdjustments ucGLAdjustments, WebEnums.RecCategory? enumRecCategory, WebEnums.RecCategoryType enumRecCategoryType)
    {
        ucGLAdjustments.GLDataHdrInfo = this.GLDataHdrInfo;
        ucGLAdjustments.RecCategoryTypeID = (short)enumRecCategoryType;
        if (ucGLAdjustments.RecCategoryTypeID != null)
        {
            ucGLAdjustments.RecCategoryID = (short)enumRecCategory;
        }
        ucGLAdjustments.PopulateGrids();
    }

    void LoadRecItemsItemWriteOff()
    {

        if (!this.uctlItemInputWriteOff.Visible)
        {
            this.uctlItemInputWriteOff.GLDataHdrInfo = this.GLDataHdrInfo;
            this.uctlItemInputWriteOff.RecCategoryTypeID = (short)WebEnums.RecCategoryType.Accrual_RecWriteoffOn_WriteoffOn;
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

    private void SetDefaulValueForLabelTotal()
    {
        string defaultText = Helper.GetDisplayDecimalValue(0);

        lblBankFeeBC.Text = defaultText;
        lblBankFeeRC.Text = defaultText;
        lblNSFFeesBC.Text = defaultText;
        lblNSFFeesRC.Text = defaultText;
        lblOtherInGLAdjustmentsBC.Text = defaultText;
        lblOtherInGLAdjustmentsRC.Text = defaultText;
        lblDepositInTransitBC.Text = defaultText;
        lblDepositInTransitRC.Text = defaultText;
        lblOutstandingChecksBC.Text = defaultText;
        lblOutstandingChecksRC.Text = defaultText;
        lblOtherInTimingDifferenceBC.Text = defaultText;
        lblOtherInTimingDifferenceRC.Text = defaultText;
    }

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
        int recPeriodID = SessionHelper.CurrentReconciliationPeriodID.GetValueOrDefault();

        IGLDataRecItem oGLRecItemInputClient = RemotingHelper.GetGLDataRecItemObject();
        List<GLDataRecItemInfo> oGLReconciliationItemInputInfoCollection = oGLRecItemInputClient.GetTotalByReconciliationCategoryTypeID(GLDataID, Helper.GetAppUserInfo());

        SetDefaulValueForLabelTotal();
        foreach (GLDataRecItemInfo oGLReconciliationItemInputInfo in oGLReconciliationItemInputInfoCollection)
        {
            WebEnums.RecCategoryType eReconciliationCategoryType = (WebEnums.RecCategoryType)oGLReconciliationItemInputInfo.ReconciliationCategoryTypeID;
            if ((Int32)eReconciliationCategoryType > 0)
            {
                switch (eReconciliationCategoryType)
                {
                    case WebEnums.RecCategoryType.BankAccount_GLAdjustments_BankFees:
                        lblBankFeeBC.Text = Helper.GetDisplayDecimalValue(oGLReconciliationItemInputInfo.AmountBaseCurrency);
                        lblBankFeeRC.Text = Helper.GetDisplayDecimalValue(oGLReconciliationItemInputInfo.AmountReportingCurrency);

                        glAdjustmentBankFeeBC = oGLReconciliationItemInputInfo.AmountBaseCurrency;
                        glAdjustmentBankFeeRC = oGLReconciliationItemInputInfo.AmountReportingCurrency;
                        break;

                    case WebEnums.RecCategoryType.BankAccount_GLAdjustments_NSFFees:
                        lblNSFFeesBC.Text = Helper.GetDisplayDecimalValue(oGLReconciliationItemInputInfo.AmountBaseCurrency);
                        lblNSFFeesRC.Text = Helper.GetDisplayDecimalValue(oGLReconciliationItemInputInfo.AmountReportingCurrency);

                        glAdjustmentNSFFeesBC = oGLReconciliationItemInputInfo.AmountBaseCurrency;
                        glAdjustmentNSFFeesRC = oGLReconciliationItemInputInfo.AmountReportingCurrency;
                        break;

                    case WebEnums.RecCategoryType.BankAccount_GLAdjustments_Other:
                        lblOtherInGLAdjustmentsBC.Text = Helper.GetDisplayDecimalValue(oGLReconciliationItemInputInfo.AmountBaseCurrency);
                        lblOtherInGLAdjustmentsRC.Text = Helper.GetDisplayDecimalValue(oGLReconciliationItemInputInfo.AmountReportingCurrency);

                        glAdjustmentOtherInGLAdjustmentsBC = oGLReconciliationItemInputInfo.AmountBaseCurrency;
                        glAdjustmentOtherInGLAdjustmentsRC = oGLReconciliationItemInputInfo.AmountReportingCurrency;
                        break;

                    case WebEnums.RecCategoryType.BankAccount_TimingDifference_DepositsInTransit:
                        lblDepositInTransitBC.Text = Helper.GetDisplayDecimalValue(oGLReconciliationItemInputInfo.AmountBaseCurrency);
                        lblDepositInTransitRC.Text = Helper.GetDisplayDecimalValue(oGLReconciliationItemInputInfo.AmountReportingCurrency);

                        glAdjustmentDepositInTransitBC = oGLReconciliationItemInputInfo.AmountBaseCurrency;
                        glAdjustmentDepositInTransitRC = oGLReconciliationItemInputInfo.AmountReportingCurrency;
                        break;

                    case WebEnums.RecCategoryType.BankAccount_TimingDifference_OutstandingChecks:
                        lblOutstandingChecksBC.Text = Helper.GetDisplayDecimalValue(oGLReconciliationItemInputInfo.AmountBaseCurrency);
                        lblOutstandingChecksRC.Text = Helper.GetDisplayDecimalValue(oGLReconciliationItemInputInfo.AmountReportingCurrency);

                        glAdjustmentOutstandingChecksBC = oGLReconciliationItemInputInfo.AmountBaseCurrency;
                        glAdjustmentOutstandingChecksRC = oGLReconciliationItemInputInfo.AmountReportingCurrency;
                        break;

                    case WebEnums.RecCategoryType.BankAccount_TimingDifference_Other:
                        lblOtherInTimingDifferenceBC.Text = Helper.GetDisplayDecimalValue(oGLReconciliationItemInputInfo.AmountBaseCurrency);
                        lblOtherInTimingDifferenceRC.Text = Helper.GetDisplayDecimalValue(oGLReconciliationItemInputInfo.AmountReportingCurrency);

                        glAdjustmentOtherInTimingDifferenceBC = oGLReconciliationItemInputInfo.AmountBaseCurrency;
                        glAdjustmentOtherInTimingDifferenceRC = oGLReconciliationItemInputInfo.AmountReportingCurrency;
                        break;
                }
            }
        }

        IGLData oGLDataClient = RemotingHelper.GetGLDataObject();
        List<GLDataHdrInfo> oGLDataHdrInfoCollection = oGLDataClient.SelectGLDataHdrByGLDataID(GLDataID, Helper.GetAppUserInfo());
        if (oGLDataHdrInfoCollection != null && oGLDataHdrInfoCollection.Count > 0)
        {
            lblGLBalanceBC.Text = Helper.GetDisplayCurrencyValue(oGLDataHdrInfoCollection[0].BaseCurrencyCode, oGLDataHdrInfoCollection[0].GLBalanceBaseCurrency);
            lblGLBalanceRC.Text = Helper.GetDisplayReportingCurrencyValue(oGLDataHdrInfoCollection[0].GLBalanceReportingCurrency);
            // Set in Hidden
            hdnGLBalanceBC.Value = Helper.GetDisplayDecimalValue(oGLDataHdrInfoCollection[0].GLBalanceBaseCurrency);
            hdnGLBalanceRC.Value = Helper.GetDisplayDecimalValue(oGLDataHdrInfoCollection[0].GLBalanceReportingCurrency);

            //********  Formula Reconciled Balance=  - (Bank Fees)- (NSF Fees)+ Others - (DIT) + OS Checks+ Others + Bank Balance ******

            glAdjustmentAndTimingDiffBC =
                    -glAdjustmentBankFeeBC
                    - glAdjustmentNSFFeesBC
                    + glAdjustmentOtherInGLAdjustmentsBC
                    - glAdjustmentDepositInTransitBC
                    + glAdjustmentOutstandingChecksBC
                    + glAdjustmentOtherInTimingDifferenceBC;

            glAdjustmentAndTimingDiffRC =
                    -glAdjustmentBankFeeRC
                    - glAdjustmentNSFFeesRC
                    + glAdjustmentOtherInGLAdjustmentsRC
                    - glAdjustmentDepositInTransitRC
                    + glAdjustmentOutstandingChecksRC
                    + glAdjustmentOtherInTimingDifferenceRC;

            lblReconciledBalanceBC.Text = Helper.GetDisplayDecimalValue(oGLDataHdrInfoCollection[0].ReconciliationBalanceBaseCurrency);
            lblReconciledBalanceRC.Text = Helper.GetDisplayDecimalValue(oGLDataHdrInfoCollection[0].ReconciliationBalanceReportingCurrency);

            lblTotalRecWriteOffBC.Text = Helper.GetDisplayDecimalValue(oGLDataHdrInfoCollection[0].WriteOnOffAmountBaseCurrency);
            lblTotalRecWriteOffRC.Text = Helper.GetDisplayDecimalValue(oGLDataHdrInfoCollection[0].WriteOnOffAmountReportingCurrency);


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

            lblTotalUnExplainedVarianceBC.Text = Helper.GetDisplayDecimalValue(oGLDataHdrInfoCollection[0].UnexplainedVarianceBaseCurrency);
            lblTotalUnExplainedVarianceRC.Text = Helper.GetDisplayDecimalValue(oGLDataHdrInfoCollection[0].UnexplainedVarianceReportingCurrency);

            lblBankBalanceBC.Text = Helper.GetDisplayDecimalValue(oGLDataHdrInfoCollection[0].SupportingDetailBalanceBaseCurrency);
            lblBankBalanceRC.Text = Helper.GetDisplayDecimalValue(oGLDataHdrInfoCollection[0].SupportingDetailBalanceReportingCurrency);
        }

        //ucEditQualityScore.GLDataID = _gLDataID;
        Dictionary<ARTEnums.QualityScoreType, Int32?> dictGLDataQualityScoreCount = QualityScoreHelper.GetGLDataQualityScoreCount(GLDataID);
        if (dictGLDataQualityScoreCount != null)
        {
            if (dictGLDataQualityScoreCount.ContainsKey(ARTEnums.QualityScoreType.SystemScore))
                lblSystemScoreValue.Text = Helper.GetDisplayIntegerValue(dictGLDataQualityScoreCount[ARTEnums.QualityScoreType.SystemScore]);
            if (dictGLDataQualityScoreCount.ContainsKey(ARTEnums.QualityScoreType.UserScore))
                lblUserScoreValue.Text = Helper.GetDisplayIntegerValue(dictGLDataQualityScoreCount[ARTEnums.QualityScoreType.UserScore]);
        }
        RecControlCheckListHelper.SetRecControlCheckListCounts(GLDataID, lblRecControlTotalValue, lblRecControlCompletedValue, hdReviewCount);

        List<long> AccountIDs = new List<long>();
        if (AccountID.GetValueOrDefault() > 0)
        {
            AccountIDs.Add(AccountID.Value);
        }
        else if (NetAccountID.GetValueOrDefault() > 0)
        {
            AccountIDs = NetAccountHelper.GetAllConstituentAccounts(NetAccountID.Value, recPeriodID);
        }
        int? TaskCountCompleted = null;
        int? TaskCountPending = null;
        var taskGridData = TaskMasterHelper.GetAccessableTaskByAccountIDs(SessionHelper.CurrentUserID.GetValueOrDefault(), SessionHelper.CurrentRoleID.GetValueOrDefault(), SessionHelper.CurrentReconciliationPeriodID.GetValueOrDefault(), AccountIDs, null, false);

        if (taskGridData != null && taskGridData.Count > 0)
        {
            TaskCountCompleted = taskGridData.FindAll(t => t.TaskStatusID == (short)ARTEnums.TaskStatus.Completed).Count;
            TaskCountPending = (taskGridData.Count - TaskCountCompleted);
        }
        lblCompleatedTaskValue.Text = Helper.GetDisplayIntegerValue(TaskCountCompleted);
        lblPendingTaskValue.Text = Helper.GetDisplayIntegerValue(TaskCountPending);
    }

    private void ManagePackageFeatures()
    {
        if (Helper.IsFeatureActivated(WebEnums.Feature.QualityScore, SessionHelper.CurrentReconciliationPeriodID))
            trQualityScore.Visible = true;
        else
            trQualityScore.Visible = false;
        if (Helper.GetFeatureCapabilityMode(WebEnums.Feature.RecControlChecklist, ARTEnums.Capability.RecControlChecklist, SessionHelper.CurrentReconciliationPeriodID) == WebEnums.FeatureCapabilityMode.Visible)
            trRecControlCheckList.Visible = true;
        else
            trRecControlCheckList.Visible = false;
    }

    private void SetEditMode()
    {
        uctlBankFee.EditMode = WebEnums.FormMode.ReadOnly;
        uctlNSFFee.EditMode = WebEnums.FormMode.ReadOnly;
        uctlOtherInGLAdjustments.EditMode = WebEnums.FormMode.ReadOnly;
        uctlDepositInTransit.EditMode = WebEnums.FormMode.ReadOnly;
        uctlOutstandingChecks.EditMode = WebEnums.FormMode.ReadOnly;
        uctlOtherInTimingDifference.EditMode = WebEnums.FormMode.ReadOnly;
    }
    private void ShowTaskStatus()
    {
        //if (NetAccountID != null && NetAccountID.Value > 0)
        //    trTaskMaster.Visible = false;
        //else
        trTaskMaster.Visible = true;
    }
}
