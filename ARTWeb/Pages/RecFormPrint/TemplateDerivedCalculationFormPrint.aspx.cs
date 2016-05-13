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
using System.Collections.Generic;
using SkyStem.ART.Client.Model;
using SkyStem.Language.LanguageUtility;
using SkyStem.ART.Web.UserControls;
using SkyStem.ART.Client.Data;
using SkyStem.ART.Client.Exception;
using SkyStem.Library.Controls.WebControls;
public partial class Pages_TemplateDerivedCalculationFormPrint : PageBaseRecForm
{
    const ARTEnums.ReconciliationItemTemplate eReconciliationItemTemplateThisPage = ARTEnums.ReconciliationItemTemplate.DerivedCalculationForm;
    const bool IS_SUPPORTING_DETAIL_ENTRY_ON_TEMPLATE = true;// tells whether sum for SUPPORTING_DETAIL section will come from ItemInput grid or just on entry on the recForm itself
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
            Helper.ShowErrorMessage(this, ex);
        }
        catch (Exception ex)
        {
            Helper.ShowErrorMessage(this, ex);
        }
    }
    private void LoadDetails()
    {
        imgViewBankFee_Click(null, null);
        imgGLAdjustmentsOther_Click(null, null);
        imgViewTimingDifference_Click(null, null);
        imgRecWriteOff_Click(null, null);
        imgUnexplainedVariance_Click(null, null);
    }

    private void OnPageLoad()
    {
        //Helper.SetPageTitle(this, 1099);
        if (Request.QueryString[QueryStringConstants.IS_SUMMARY] != null)
            _IsSummary = Convert.ToBoolean(Convert.ToInt16(Request.QueryString[QueryStringConstants.IS_SUMMARY]));

        string pageID = Request.QueryString[QueryStringConstants.REFERRER_PAGE_ID];
        _ARTPages = (WebEnums.ARTPages)System.Enum.Parse(typeof(WebEnums.ARTPages), pageID);
        if (!string.IsNullOrEmpty(Request.QueryString[QueryStringConstants.GLDATA_ID]))
        {
            long? _gLDataID = Convert.ToInt64(Request.QueryString[QueryStringConstants.GLDATA_ID]);
            this.GLDataHdrInfo = Helper.GetGLDataHdrInfo(_gLDataID);
        }

        lblPeriodEndDate.Text = string.Format(WebConstants.FORMAT_BRACKET, Helper.GetDisplayDate(SessionHelper.CurrentReconciliationPeriodEndDate));

        if (this.GLDataHdrInfo != null)
        {
            WebEnums.FormMode eFormMode = Helper.GetFormMode(WebEnums.ARTPages.TemplateBankAccount, this.GLDataHdrInfo);
            if (eFormMode == WebEnums.FormMode.Edit)
            {
                txtBankBalanceBC.Enabled = true;
                txtBankBalanceRC.Enabled = true;
                txtBasisForCalculationExplanation.Enabled = true;
            }
            else
            {
                txtBankBalanceBC.Enabled = false;
                txtBankBalanceRC.Enabled = false;
                txtBasisForCalculationExplanation.Enabled = false;
            }
        }
        SetAllLabels();

        setEntityNameLabelIDForGLAdjustments();

        RecHelper.SetRecStatusBarPropertiesForRecFormPrint(this, GLDataID);
        ManagePackageFeatures();
        //ShowTaskStatus();
    }
    private void setEntityNameLabelIDForGLAdjustments()
    {
        if (ExLabel5.LabelID.ToString() != "1656")
            uctlGLAdjustments.EntityNameLabelID = ExLabel5.LabelID;
        else
            uctlGLAdjustments.EntityNameLabelID = lblGLAdjustments.LabelID;

        if (ExLabel1.LabelID.ToString() != "1656")
            uctlTimmingDifference.EntityNameLabelID = ExLabel1.LabelID;
        else
            uctlTimmingDifference.EntityNameLabelID = lblTimmingDifference.LabelID;

        if (ExLabel6.LabelID.ToString() != "1656")
            uctlGLAdjustmentsOther.EntityNameLabelID = ExLabel6.LabelID;
        else
            uctlGLAdjustmentsOther.EntityNameLabelID = lblDerivedCalculation.LabelID;



    }
    private void RegisterToggleControl()
    {

    }
    void imgRecWriteOff_Click(object sender, ImageClickEventArgs e)
    {
        uctlItemInputWriteOff.IsExpanded = true;
        uctlItemInputWriteOff.IsRegisterPDFAndExcelForPostback = false;
        this.LoadRecItemsItemWriteOff();
        uctlItemInputWriteOff.ContentVisibility = true;
    }
    void imgUnexplainedVariance_Click(object sender, ImageClickEventArgs e)
    {
        this.uctlUnexplainedVariance.IsExpanded = true;
        uctlUnexplainedVariance.IsRegisterPDFAndExcelForPostback = false;
        this.LoadUnexplainedVariance();
        this.uctlUnexplainedVariance.ContentVisibility = true;
    }
    void LoadUnexplainedVariance()
    {
        if (!this.uctlUnexplainedVariance.Visible)
        {
            this.uctlUnexplainedVariance.GLDataHdrInfo = this.GLDataHdrInfo;
            this.uctlUnexplainedVariance.RecCategoryTypeID = (short)WebEnums.RecCategoryType.DerivedCalculation_RecWriteoffOn_WriteoffOn;
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
    void imgGLAdjustmentsOther_Click(object sender, ImageClickEventArgs e)
    {
        uctlGLAdjustmentsOther.IsExpanded = true;
        //uctlGLAdjustmentsOther.IsRegisterPDFAndExcelForPostback = false;
        this.LoadRecItemsGLAdjustment(uctlGLAdjustmentsOther, WebEnums.RecCategory.GLAdjustments, WebEnums.RecCategoryType.DerivedCalculation_SupportingDetail_OtherDetails);
        uctlGLAdjustmentsOther.ContentVisibility = true;
    }
    void imgViewBankFee_Click(object sender, ImageClickEventArgs e)
    {
        uctlGLAdjustments.IsExpanded = true;
        //uctlGLAdjustments.IsRegisterPDFAndExcelForPostback = false;
        this.LoadRecItemsGLAdjustment(uctlGLAdjustments, WebEnums.RecCategory.GLAdjustments, WebEnums.RecCategoryType.DerivedCalculation_GLAdjustments_Total);
        uctlGLAdjustments.ContentVisibility = true;

    }
    void imgViewTimingDifference_Click(object sender, ImageClickEventArgs e)
    {
        uctlTimmingDifference.IsExpanded = true;
        //uctlTimmingDifference.IsRegisterPDFAndExcelForPostback = false;
        this.LoadRecItemsGLAdjustment(uctlTimmingDifference, WebEnums.RecCategory.TimingDifference, WebEnums.RecCategoryType.DerivedCalculation_TimingDifference_Total);
        uctlTimmingDifference.ContentVisibility = true;
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
            this.uctlItemInputWriteOff.RecCategoryTypeID = (short)WebEnums.RecCategoryType.DerivedCalculation_RecWriteoffOn_WriteoffOn;
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
    public override string GetMenuKey()
    {
        return Helper.GetMenuKeyForRecForms(_ARTPages);
    }
    private void SetDefaulValueForLabelTotal()
    {
        lblTotalGLAdjustmentsBC.Text = Helper.GetDisplayDecimalValue(0);
        lblTotalGLAdjustmentsRC.Text = Helper.GetDisplayDecimalValue(0);
        lblTotalTimingDifferenceBC.Text = Helper.GetDisplayDecimalValue(0);
        lblTotalTimingDifferenceRC.Text = Helper.GetDisplayDecimalValue(0);
        lblTotalSupportingDetailBC.Text = Helper.GetDisplayDecimalValue(0);
        lblTotalSupportingDetailRC.Text = Helper.GetDisplayDecimalValue(0);
    }
    protected void SetAllLabels()
    {
        decimal? glAdjustmentAndTimingDiffBC = 0M;
        decimal? glAdjustmentAndTimingDiffRC = 0M;
        decimal? glAdjustmentBC = 0M;
        decimal? glAdjustmentRC = 0M;
        decimal? glTimingDiffBC = 0M;
        decimal? glTimingDiffRC = 0M;
        decimal? glSupportingDetailBC = 0M;
        decimal? glSupportingDetailRC = 0M;
        int recPeriodID = SessionHelper.CurrentReconciliationPeriodID.GetValueOrDefault();
        IGLDataRecItem oGLRecItemInputClient = RemotingHelper.GetGLDataRecItemObject();
        List<GLDataRecItemInfo> oGLReconciliationItemInputInfoCollection = oGLRecItemInputClient.GetTotalByReconciliationCategoryTypeID(GLDataID, Helper.GetAppUserInfo());
        SetDefaulValueForLabelTotal();
        foreach (GLDataRecItemInfo oGLReconciliationItemInputInfo in oGLReconciliationItemInputInfoCollection)
        {
            WebEnums.RecCategoryType eReconciliationCategoryType = (WebEnums.RecCategoryType)oGLReconciliationItemInputInfo.ReconciliationCategoryTypeID;
            if ((Int32)eReconciliationCategoryType > 0)
            {
                //switch (oGLReconciliationItemInputInfo.ReconciliationCategoryTypeID.Value)
                switch (eReconciliationCategoryType)
                {
                    case WebEnums.RecCategoryType.DerivedCalculation_GLAdjustments_Total:
                        glAdjustmentBC = oGLReconciliationItemInputInfo.AmountBaseCurrency;
                        glAdjustmentRC = oGLReconciliationItemInputInfo.AmountReportingCurrency;

                        lblTotalGLAdjustmentsBC.Text = Helper.GetDisplayDecimalValue(oGLReconciliationItemInputInfo.AmountBaseCurrency);
                        lblTotalGLAdjustmentsRC.Text = Helper.GetDisplayDecimalValue(oGLReconciliationItemInputInfo.AmountReportingCurrency);
                        break;
                    case WebEnums.RecCategoryType.DerivedCalculation_TimingDifference_Total:
                        glTimingDiffBC = oGLReconciliationItemInputInfo.AmountBaseCurrency;
                        glTimingDiffRC = oGLReconciliationItemInputInfo.AmountReportingCurrency;

                        lblTotalTimingDifferenceBC.Text = Helper.GetDisplayDecimalValue(oGLReconciliationItemInputInfo.AmountBaseCurrency);
                        lblTotalTimingDifferenceRC.Text = Helper.GetDisplayDecimalValue(oGLReconciliationItemInputInfo.AmountReportingCurrency);
                        break;
                    case WebEnums.RecCategoryType.DerivedCalculation_SupportingDetail_OtherDetails:
                        //TODO: check if this label show the just SUM(other detail) or SUM( SupportingDetail section)   
                        glSupportingDetailBC = oGLReconciliationItemInputInfo.AmountBaseCurrency;
                        glSupportingDetailBC = oGLReconciliationItemInputInfo.AmountReportingCurrency;

                        lblTotalSupportingDetailBC.Text = Helper.GetDisplayDecimalValue(oGLReconciliationItemInputInfo.AmountBaseCurrency);
                        lblTotalSupportingDetailRC.Text = Helper.GetDisplayDecimalValue(oGLReconciliationItemInputInfo.AmountReportingCurrency);
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

            glAdjustmentAndTimingDiffBC = -glAdjustmentBC + glTimingDiffBC + glSupportingDetailBC;
            glAdjustmentAndTimingDiffRC = -glAdjustmentRC + glTimingDiffRC + glSupportingDetailRC;

            hdnGlAdjustmentAndTimingDiffBC.Value = Helper.GetDisplayDecimalValue(glAdjustmentAndTimingDiffBC);
            hdnGlAdjustmentAndTimingDiffRC.Value = Helper.GetDisplayDecimalValue(glAdjustmentAndTimingDiffRC);

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
        }

        //extra section
        if (!IsPostBack)
        {
            List<DerivedCalculationSupportingDetailInfo> oDerivedCalculationSupportingDetailInfoCollection = oGLDataClient.SelectAllDerivedCalculationSupportingDetailInfoByGLDataID(GLDataID, Helper.GetAppUserInfo());
            if (oDerivedCalculationSupportingDetailInfoCollection != null && oDerivedCalculationSupportingDetailInfoCollection.Count > 0 && oDerivedCalculationSupportingDetailInfoCollection[0] != null)
            {
                txtBankBalanceBC.Text = Helper.GetDecimalValueForTextBox(oDerivedCalculationSupportingDetailInfoCollection[0].BaseCurrencyBalance, WebConstants.INT_FOR_DECIMAL_VALUE_TEXTBOX);
                txtBankBalanceRC.Text = Helper.GetDecimalValueForTextBox(oDerivedCalculationSupportingDetailInfoCollection[0].ReportingCurrencyBalance, WebConstants.INT_FOR_DECIMAL_VALUE_TEXTBOX);
                txtBasisForCalculationExplanation.Text = oDerivedCalculationSupportingDetailInfoCollection[0].BasisForDerivedCalculation;
            }
        }

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
        uctlGLAdjustments.EditMode = WebEnums.FormMode.ReadOnly;
        uctlTimmingDifference.EditMode = WebEnums.FormMode.ReadOnly;
        uctlGLAdjustmentsOther.EditMode = WebEnums.FormMode.ReadOnly;
    }
    private void ShowTaskStatus()
    {
        //if (NetAccountID != null && NetAccountID.Value > 0)
        //    trTaskMaster.Visible = false;
        //else
        trTaskMaster.Visible = true;
    }
}