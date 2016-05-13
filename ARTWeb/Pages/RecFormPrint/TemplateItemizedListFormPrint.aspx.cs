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
using SkyStem.ART.Web.Utility;
using SkyStem.ART.Web.Classes;
using SkyStem.ART.Web.Data;
using SkyStem.ART.Client.IServices;
using System.Collections.Generic;
using SkyStem.ART.Client.Model;
using System.Text;
using SkyStem.Language.LanguageUtility;
using SkyStem.ART.Web.UserControls;
using SkyStem.ART.Client.Data;
using SkyStem.ART.Client.Exception;
using SkyStem.Library.Controls.WebControls;

public partial class Pages_TemplateItemizedListFormPrint : PageBaseRecForm
{
    const ARTEnums.ReconciliationItemTemplate eReconciliationItemTemplateThisPage = ARTEnums.ReconciliationItemTemplate.ItemizedListForm;
    const bool IS_SUPPORTING_DETAIL_ENTRY_ON_TEMPLATE = false;// tells whether sum for SUPPORTING_DETAIL section will come from ItemInput grid or just on entry on the recForm itself
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

        setEntityNameLabelIDForGLAdjustments();

        RecHelper.SetRecStatusBarPropertiesForRecFormPrint(this, GLDataID);
        ManagePackageFeatures();
        //ShowTaskStatus();
    }
    private void setEntityNameLabelIDForGLAdjustments()
    {
        if (lblHeaderTotal.LabelID.ToString() != "1656")
            uctlGLAdjustment.EntityNameLabelID = lblHeaderTotal.LabelID;
        else
            uctlGLAdjustment.EntityNameLabelID = lblGLAdjustments.LabelID;

        if (ExLabel1.LabelID.ToString() != "1656")
            uctlTimingDifference.EntityNameLabelID = ExLabel1.LabelID;
        else
            uctlTimingDifference.EntityNameLabelID = lblTimmingDifference.LabelID;

        if (ExLabel12.LabelID.ToString() != "1656")
            uctlSupportingDetail.EntityNameLabelID = ExLabel12.LabelID;
        else
            uctlSupportingDetail.EntityNameLabelID = lblDerivedCalculation.LabelID;



    }


    public override string GetMenuKey()
    {
        return Helper.GetMenuKeyForRecForms(_ARTPages);
    }




    private void LoadDetails()
    {
        imgGLAdjustment_Click(null, null);
        imgTimingDifference_Click(null, null);
        imgSupportingDetail_Click(null, null);
        imgRecWriteOff_Click(null, null);
        imgUnexplainedVariance_Click(null, null);
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
            this.uctlUnexplainedVariance.RecCategoryTypeID = (short)WebEnums.RecCategoryType.ItemizedList_RecWriteoffOn_UnexpVar;
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

    void imgRecWriteOff_Click(object sender, ImageClickEventArgs e)
    {
        uctlRecWriteOff.IsExpanded = true;
        uctlRecWriteOff.IsRegisterPDFAndExcelForPostback = false;
        this.LoadRecItemsItemWriteOff();
        uctlRecWriteOff.ContentVisibility = true;
    }

    void LoadRecItemsItemWriteOff()
    {
        if (!this.uctlRecWriteOff.Visible)
        {
            this.uctlRecWriteOff.GLDataHdrInfo = this.GLDataHdrInfo;
            this.uctlRecWriteOff.RecCategoryTypeID = (short)WebEnums.RecCategoryType.ItemizedList_RecWriteoffOn_WriteoffOn;
            if (this.uctlRecWriteOff.RecCategoryTypeID != null)
            {
                this.uctlRecWriteOff.RecCategoryID = (short)WebEnums.RecCategory.VariancesWriteOffOn;
            }
            this.uctlRecWriteOff.LoadData();
            this.uctlRecWriteOff.Visible = true;
        }
        else
        {
            this.uctlRecWriteOff.Visible = false;
        }
    }

    void imgSupportingDetail_Click(object sender, ImageClickEventArgs e)
    {
        uctlSupportingDetail.IsExpanded = true;
        //uctlSupportingDetail.IsRegisterPDFAndExcelForPostback = false;
        this.LoadRecItemsGLAdjustment(uctlSupportingDetail, WebEnums.RecCategory.SupportingDetail, WebEnums.RecCategoryType.ItemizedList_SupportingDetail_SupportingDetailList);
        uctlSupportingDetail.ContentVisibility = true;
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

    void imgTimingDifference_Click(object sender, ImageClickEventArgs e)
    {
        uctlTimingDifference.IsExpanded = true;
        //uctlTimingDifference.IsRegisterPDFAndExcelForPostback = false;
        this.LoadRecItemsGLAdjustment(uctlTimingDifference, WebEnums.RecCategory.TimingDifference, WebEnums.RecCategoryType.ItemizedList_TimingDifference_Total);
        uctlTimingDifference.ContentVisibility = true;
    }

    void imgGLAdjustment_Click(object sender, ImageClickEventArgs e)
    {
        uctlGLAdjustment.IsExpanded = true;
        //uctlGLAdjustment.IsRegisterPDFAndExcelForPostback = false;
        this.LoadRecItemsGLAdjustment(uctlGLAdjustment, WebEnums.RecCategory.GLAdjustments, WebEnums.RecCategoryType.ItemizedList_GLAdjustments_Total);
        uctlGLAdjustment.ContentVisibility = true;
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
        IGLDataRecItem oGLRecItemInputClient = RemotingHelper.GetGLDataRecItemObject();
        List<GLDataRecItemInfo> oGLReconciliationItemInputInfoCollection = oGLRecItemInputClient.GetTotalByReconciliationCategoryTypeID(GLDataID, Helper.GetAppUserInfo());

        int recPeriodID = SessionHelper.CurrentReconciliationPeriodID.GetValueOrDefault();

        SetDefaulValueForLabelTotal();
        foreach (GLDataRecItemInfo oGLReconciliationItemInputInfo in oGLReconciliationItemInputInfoCollection)
        {
            WebEnums.RecCategoryType eReconciliationCategoryType = (WebEnums.RecCategoryType)oGLReconciliationItemInputInfo.ReconciliationCategoryTypeID;
            if ((Int32)eReconciliationCategoryType > 0)
            {
                //switch (oGLReconciliationItemInputInfo.ReconciliationCategoryTypeID.Value)
                switch (eReconciliationCategoryType)
                {
                    case WebEnums.RecCategoryType.ItemizedList_GLAdjustments_Total:
                        lblTotalGLAdjustmentsBC.Text = Helper.GetDisplayDecimalValue(oGLReconciliationItemInputInfo.AmountBaseCurrency);
                        lblTotalGLAdjustmentsRC.Text = Helper.GetDisplayDecimalValue(oGLReconciliationItemInputInfo.AmountReportingCurrency);
                        break;
                    case WebEnums.RecCategoryType.ItemizedList_TimingDifference_Total:
                        lblTotalTimingDifferenceBC.Text = Helper.GetDisplayDecimalValue(oGLReconciliationItemInputInfo.AmountBaseCurrency);
                        lblTotalTimingDifferenceRC.Text = Helper.GetDisplayDecimalValue(oGLReconciliationItemInputInfo.AmountReportingCurrency);
                        break;
                    case WebEnums.RecCategoryType.ItemizedList_SupportingDetail_SupportingDetailList:
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
        uctlGLAdjustment.EditMode = WebEnums.FormMode.ReadOnly;
        uctlTimingDifference.EditMode = WebEnums.FormMode.ReadOnly;
        uctlSupportingDetail.EditMode = WebEnums.FormMode.ReadOnly;
    }
    private void ShowTaskStatus()
    {
        //if (NetAccountID != null && NetAccountID.Value > 0)
        //    trTaskMaster.Visible = false;
        //else
        trTaskMaster.Visible = true;
    }
}//end of class
