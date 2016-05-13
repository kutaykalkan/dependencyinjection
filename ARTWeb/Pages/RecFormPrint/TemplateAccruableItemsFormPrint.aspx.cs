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

public partial class Pages_RecFormPrint_TemplateAccruableItemsFormPrint : PageBaseRecForm
{
    const ARTEnums.ReconciliationItemTemplate eReconciliationItemTemplateThisPage = ARTEnums.ReconciliationItemTemplate.AccrualForm;
    const bool IS_SUPPORTING_DETAIL_ENTRY_ON_TEMPLATE = false;// tells whether sum for SUPPORTING_DETAIL section will come from ItemInput grid or just on entry on the recForm itself
    //bool _isMultiCurrencyActivated = false;
    //bool _isCapabilityDualReview = false;
    bool? _IsSummary = true;
    WebEnums.ARTPages _ARTPages = WebEnums.ARTPages.AccountViewer;
    protected void Page_Init(object sender, EventArgs e)
    {
        try
        {

        }
        catch (ARTException ex)
        {
            //Helper.ShowErrorMessage(this, ex);
            Helper.LogException(ex);
            throw;
        }
        catch (Exception ex)
        {
            //Helper.ShowErrorMessage(this, ex);
            Helper.LogException(ex);
            throw;
        }

    }

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
            //Helper.ShowErrorMessage(this, ex);
            Helper.LogException(ex);
            throw;
        }
        catch (Exception ex)
        {
            //Helper.ShowErrorMessage(this, ex);
            Helper.LogException(ex);
            throw;
        }
    }

    private void LoadDetails()
    {
        //imgViewGLAdjustment_Click(null, null);
        //imgViewTimingDifference_Click(null, null);
        //imgViewIndividualAccrualDetail_Click(null, null);
        //imgViewRecurringAccrualSchedule_Click(null, null);
        //imgRecWriteOff_Click(null, null);
        //imgUnexplainedVariance_Click(null, null);

    }

    protected void ompage_ReconciliationPeriodChangedEventHandler(object sender, EventArgs e)
    {
        try
        {
            if (IsPostBack)
            {
                OnPageLoad();

            }
            if (GLDataID != 0)
            {
                MasterPageBase ompage = (MasterPageBase)this.Master.Master;
                Helper.HideMessage(this);
            }
        }
        catch (ARTException ex)
        {
            //Helper.ShowErrorMessage(this, ex);
            Helper.LogException(ex);
            throw;
        }
        catch (Exception ex)
        {
            //Helper.ShowErrorMessage(this, ex);
            Helper.LogException(ex);
            throw;
        }
    }

    private void OnPageLoad()
    {
        //RegisterToggleControl();

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

        SetURL();
        SetAllLabels();

        // Set the Master Page Properties for GL Data ID
        RecHelper.SetRecStatusBarPropertiesForRecFormPrint(this, GLDataID);

        // Set the Entity Name LabelID For GLAdjustments
        setEntityNameLabelIDForGLAdjustments();
        ManagePackageFeatures();
        //ShowTaskStatus();
    }

    private void setEntityNameLabelIDForGLAdjustments()
    {
        if (lblHeaderTotal.LabelID.ToString() != "1656")
            uctlGLAdjustments.EntityNameLabelID = lblHeaderTotal.LabelID;
        else
            uctlGLAdjustments.EntityNameLabelID = lblGLAdjustments.LabelID;

        if (ExLabel1.LabelID.ToString() != "1656")
            uctlTimmingDifference.EntityNameLabelID = ExLabel1.LabelID;
        else
            uctlTimmingDifference.EntityNameLabelID = lblTimmingDifference.LabelID;
    }

    //private void RegisterToggleControl()
    //{
    //    uctlGLAdjustments.RegisterToggleControl(imgViewGLAdjustment);
    //    uctlTimmingDifference.RegisterToggleControl(imgViewTimingDifference);
    //    uctlItemInputAccurable.RegisterToggleControl(imgViewIndividualAccrualDetail);
    //    uctlItemInputAccurableRecurring.RegisterToggleControl(imgViewRecurringAccrualSchedule);
    //    uctlItemInputWriteOff.RegisterToggleControl(imgRecWriteOff);
    //    uctlUnexplainedVariance.RegisterToggleControl(imgUnexplainedVariance);
    //}


    public override string GetMenuKey()
    {
        return Helper.GetMenuKeyForRecForms(_ARTPages);
    }


    //private void SetURL(string queryString)
    private void SetURL()
    {
        string queryString = "?" + QueryStringConstants.ACCOUNT_ID + "=" + this.AccountID
           + "&" + QueryStringConstants.NETACCOUNT_ID + "=" + this.NetAccountID
            + "&" + QueryStringConstants.GLDATA_ID + "=" + this.GLDataID
            + "&" + QueryStringConstants.REC_STATUS_ID + "=" + this.GLRecStatusID;

        if (this.IsSRA.HasValue && this.IsSRA.Value)
        {
            queryString += "&" + QueryStringConstants.IS_SRA + "=1";
        }
        else
        {
            queryString += "&" + QueryStringConstants.IS_SRA + "=0";
        }
        queryString += "&" + QueryStringConstants.REC_CATEGORY_TYPE_ID + "=";


        //hlGLAdjustment.NavigateUrl = URLConstants.URL_ITEMINPUT_BANKFEE + queryString + (short)WebEnums.RecCategoryType.Accrual_GLAdjustments_Total + "&" + QueryStringConstants.REC_CATEGORY_ID + "=" + (short)WebEnums.RecCategory.GLAdjustments;
        //hlTimingDifference.NavigateUrl = URLConstants.URL_ITEMINPUT_BANKFEE + queryString + (short)WebEnums.RecCategoryType.Accrual_TimingDifference_Total + "&" + QueryStringConstants.REC_CATEGORY_ID + "=" + (short)WebEnums.RecCategory.TimingDifference ;

        //hlSupportingDetail.NavigateUrl = URLConstants.URL_ITEMINPUT_AMORTIZABLE + queryString + (short)WebEnums.RecCategoryType.BankAccount_GLAdjustments_BankFees ;

        //hlImportGLAdjustment.NavigateUrl = URLConstants.URL_IMPORT_BANKFEE + queryString + (short)WebEnums.RecCategoryType.Accrual_GLAdjustments_Total + "&" + QueryStringConstants.REC_CATEGORY_ID + "=" + (short)WebEnums.RecCategory.GLAdjustments;
        //hlImportTimingDifference.NavigateUrl = URLConstants.URL_IMPORT_BANKFEE + queryString + (short)WebEnums.RecCategoryType.Accrual_TimingDifference_Total + "&" + QueryStringConstants.REC_CATEGORY_ID + "=" + (short)WebEnums.RecCategory.TimingDifference;
        //hlImportSupportingDetail.NavigateUrl = URLConstants.URL_IMPORT_BANKFEE + queryString + (short)WebEnums.RecCategoryType.BankAccount_GLAdjustments_BankFees ;

        //hlIndividualAccrualDetail.NavigateUrl = URLConstants.URL_ITEMINPUT_ACCRUABLE + queryString + (short)WebEnums.RecCategoryType.Accrual_SupportingDetail_IndividualAccrualDetail + "&" + QueryStringConstants.REC_CATEGORY_ID + "=" + (short)WebEnums.RecCategory.SupportingDetail ;
        //hlRecurringAccrualSchedule.NavigateUrl = URLConstants.URL_ITEMINPUT_ACCRUABE_RECURRING + queryString + (short)WebEnums.RecCategoryType.Accrual_SupportingDetail_RecurringAccrualSchedule + "&" + QueryStringConstants.REC_CATEGORY_ID + "=" + (short)WebEnums.RecCategory.SupportingDetail ;

        //hlImportIndividualAccrualDetail.NavigateUrl = URLConstants.URL_IMPORT_BANKFEE + queryString + (short)WebEnums.RecCategoryType.Accrual_SupportingDetail_IndividualAccrualDetail + "&" + QueryStringConstants.REC_CATEGORY_ID + "=" + (short)WebEnums.RecCategory.SupportingDetail;
        //hlImportRecurringAccrualSchedule.NavigateUrl = URLConstants.URL_IMPORT_RECURRING + queryString + (short)WebEnums.RecCategoryType.Accrual_SupportingDetail_RecurringAccrualSchedule + "&" + QueryStringConstants.REC_CATEGORY_ID + "=" + (short)WebEnums.RecCategory.SupportingDetail;

        //hlRecWriteOff.NavigateUrl = URLConstants.URL_ITEMINPUT_WRITEOFF + queryString + (short)WebEnums.RecCategoryType.Accrual_RecWriteoffOn_WriteoffOn + "&" + QueryStringConstants.REC_CATEGORY_ID + "=" + (short)WebEnums.RecCategory.VariancesWriteOffOn ;
        //hlUnexplainedVariance.NavigateUrl = URLConstants.URL_ITEMINPUT_UNEXPLAINEDVARIANCE + queryString + (short)WebEnums.RecCategoryType.Accrual_RecWriteoffOn_UnexpVar + "&" + QueryStringConstants.REC_CATEGORY_ID + "=" + (short)WebEnums.RecCategory.VariancesWriteOffOn;
        //hlUnexplainedVarianceHistory.NavigateUrl = URLConstants.URL_ITEMINPUT_UNEXPLAINEDVARIANCE_HISTORY + queryString + (short)WebEnums.RecCategoryType.Accrual_RecWriteoffOn_UnexpVar + "&" + QueryStringConstants.REC_CATEGORY_ID + "=" + (short)WebEnums.RecCategory.VariancesWriteOffOn;
        //hlReviewNotes.NavigateUrl = URLConstants.URL_ITEMINPUT_COMMENT + queryString + (short)WebEnums.RecCategoryType.Accrual_ReviewNotes + "&" + QueryStringConstants.REC_CATEGORY_ID + "=" + (short)WebEnums.RecCategory.ReviewNotes;
        //hlAuditTrail.NavigateUrl = URLConstants.URL_AUDIT_TRAIL + queryString + (short)WebEnums.RecCategoryType.Accrual_ReviewNotes + "&" + QueryStringConstants.REC_CATEGORY_ID + "=" + (short)WebEnums.RecCategory.ReviewNotes;

        //RegisterToggleControlEventHandler();

    }



    //private void RegisterToggleControlEventHandler()
    //{
    //    imgViewGLAdjustment.Click += new ImageClickEventHandler(imgViewGLAdjustment_Click);
    //    imgViewTimingDifference.Click += new ImageClickEventHandler(imgViewTimingDifference_Click);
    //    imgViewIndividualAccrualDetail.Click += new ImageClickEventHandler(imgViewIndividualAccrualDetail_Click);
    //    imgViewRecurringAccrualSchedule.Click += new ImageClickEventHandler(imgViewRecurringAccrualSchedule_Click);
    //    imgRecWriteOff.Click += new ImageClickEventHandler(imgRecWriteOff_Click);
    //    imgUnexplainedVariance.Click += new ImageClickEventHandler(imgUnexplainedVariance_Click);
    //}

    void imgUnexplainedVariance_Click(object sender, ImageClickEventArgs e)
    {
        this.uctlUnexplainedVariance.IsExpanded = true;
        uctlUnexplainedVariance.IsRegisterPDFAndExcelForPostback = false;
        this.LoadUnexplainedVariance();
        this.uctlUnexplainedVariance.ContentVisibility = true;
        // this.uctlUnexplainedVariance.ContentWidth = Convert.ToInt32(100);

    }

    void imgRecWriteOff_Click(object sender, ImageClickEventArgs e)
    {
        uctlItemInputWriteOff.IsExpanded = true;
        uctlItemInputWriteOff.IsRegisterPDFAndExcelForPostback = false;
        this.LoadRecItemsItemWriteOff();
        uctlItemInputWriteOff.ContentVisibility = true;
    }

    void imgViewRecurringAccrualSchedule_Click(object sender, ImageClickEventArgs e)
    {
        uctlItemInputAccurableRecurring.IsExpanded = true;
        uctlItemInputAccurableRecurring.IsRegisterPDFAndExcelForPostback = false;
        this.LoadRecItemsItemInputAccurableRecurring();
        uctlItemInputAccurableRecurring.ContentVisibility = true;
    }

    void imgViewIndividualAccrualDetail_Click(object sender, ImageClickEventArgs e)
    {

        uctlItemInputAccurable.IsExpanded = true;
        uctlItemInputAccurable.IsRegisterPDFAndExcelForPostback = false;
        this.LoadRecItemsItemInputAccurable();
        uctlItemInputAccurable.ContentVisibility = true;

    }

    void imgViewTimingDifference_Click(object sender, ImageClickEventArgs e)
    {
        uctlTimmingDifference.IsExpanded = true;
        //uctlTimmingDifference.IsRegisterPDFAndExcelForPostback = false;
        this.LoadRecItemsGLAdjustment(uctlTimmingDifference, WebEnums.RecCategory.TimingDifference, WebEnums.RecCategoryType.Accrual_TimingDifference_Total);
        uctlTimmingDifference.ContentVisibility = true;
    }

    void imgViewGLAdjustment_Click(object sender, ImageClickEventArgs e)
    {
        uctlGLAdjustments.EntityNameLabelID = 1080;
        uctlGLAdjustments.IsExpanded = true;
        //uctlGLAdjustments.IsRegisterPDFAndExcelForPostback = false;
        this.LoadRecItemsGLAdjustment(uctlGLAdjustments, WebEnums.RecCategory.GLAdjustments, WebEnums.RecCategoryType.Accrual_GLAdjustments_Total);
        uctlGLAdjustments.ContentVisibility = true;

    }

    void LoadRecItemsGLAdjustment(UserControls_GLAdjustments ucGLAdjustments, WebEnums.RecCategory? enumRecCategory, WebEnums.RecCategoryType enumRecCategoryType)
    {
        ucGLAdjustments.GLDataHdrInfo = this.GLDataHdrInfo;
        ucGLAdjustments.RecCategoryTypeID = (short)enumRecCategoryType;
        if (ucGLAdjustments.RecCategoryTypeID != null)
        {
            ucGLAdjustments.RecCategoryID = (short)enumRecCategory; // (short)WebEnums.RecCategory.GLAdjustments;
        }
        //ucGLAdjustments.LoadData();
        ucGLAdjustments.PopulateGrids();
    }

    void LoadRecItemsItemInputAccurable()
    {
        this.uctlItemInputAccurable.GLDataHdrInfo = this.GLDataHdrInfo;
        this.uctlItemInputAccurable.RecCategoryTypeID = (short)WebEnums.RecCategoryType.Accrual_SupportingDetail_IndividualAccrualDetail;
        if (this.uctlItemInputAccurable.RecCategoryTypeID != null)
        {
            this.uctlItemInputAccurable.RecCategoryID = (short)WebEnums.RecCategory.SupportingDetail;
        }
        this.uctlItemInputAccurable.LoadData();


        //this.uctlItemInputAccurable.PopulateGrids();
        //this.uctlItemInputAccurable.Visible = true;

    }

    void LoadRecItemsItemInputAccurableRecurring()
    {
        this.uctlItemInputAccurableRecurring.GLDataHdrInfo = this.GLDataHdrInfo;
        this.uctlItemInputAccurableRecurring.RecCategoryTypeID = (short)WebEnums.RecCategoryType.Accrual_SupportingDetail_RecurringAccrualSchedule;
        if (this.uctlItemInputAccurableRecurring.RecCategoryTypeID != null)
        {
            this.uctlItemInputAccurableRecurring.RecCategoryID = (short)WebEnums.RecCategory.SupportingDetail;
        }
        this.uctlItemInputAccurableRecurring.LoadData();
        this.uctlItemInputAccurableRecurring.Visible = true;
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
        lblTotalGLAdjustmentsBC.Text = Helper.GetDisplayDecimalValue(0);
        lblTotalGLAdjustmentsRC.Text = Helper.GetDisplayDecimalValue(0);
        lblTotalTimingDifferenceBC.Text = Helper.GetDisplayDecimalValue(0);
        lblTotalTimingDifferenceRC.Text = Helper.GetDisplayDecimalValue(0);
        lblTotalSupportingDetailBC.Text = Helper.GetDisplayDecimalValue(0);
        lblTotalSupportingDetailRC.Text = Helper.GetDisplayDecimalValue(0);

    }

    protected void SetAllLabels()
    {
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
                    case WebEnums.RecCategoryType.Accrual_GLAdjustments_Total:
                        lblTotalGLAdjustmentsBC.Text = Helper.GetDisplayDecimalValue(oGLReconciliationItemInputInfo.AmountBaseCurrency);
                        lblTotalGLAdjustmentsRC.Text = Helper.GetDisplayDecimalValue(oGLReconciliationItemInputInfo.AmountReportingCurrency);
                        break;
                    case WebEnums.RecCategoryType.Accrual_TimingDifference_Total:
                        lblTotalTimingDifferenceBC.Text = Helper.GetDisplayDecimalValue(oGLReconciliationItemInputInfo.AmountBaseCurrency);
                        lblTotalTimingDifferenceRC.Text = Helper.GetDisplayDecimalValue(oGLReconciliationItemInputInfo.AmountReportingCurrency);
                        break;
                    //TODO: check how to do this
                    //case WebEnums.RecCategoryType.Accrual_SupportingDetail_IndividualAccrualDetail:
                    //    lblTotalSupportingDetailBC.Text = Helper.GetDisplayDecimalValue(oGLReconciliationItemInputInfo.AmountInBaseCurrency);
                    //    lblTotalSupportingDetailRC.Text = Helper.GetDisplayDecimalValue(oGLReconciliationItemInputInfo.AmountInReportingCurrency);
                    //    break;
                    case WebEnums.RecCategoryType.Accrual_SupportingDetail_IndividualAccrualDetail:
                        lblIndividualAccrualDetailBC.Text = Helper.GetDisplayDecimalValue(oGLReconciliationItemInputInfo.AmountBaseCurrency);
                        lblIndividualAccrualDetailRC.Text = Helper.GetDisplayDecimalValue(oGLReconciliationItemInputInfo.AmountReportingCurrency);
                        break;
                    case WebEnums.RecCategoryType.Accrual_SupportingDetail_RecurringAccrualSchedule:
                        lblRecurringAccrualScheduleBC.Text = Helper.GetDisplayDecimalValue(oGLReconciliationItemInputInfo.AmountBaseCurrency);
                        lblRecurringAccrualScheduleRC.Text = Helper.GetDisplayDecimalValue(oGLReconciliationItemInputInfo.AmountReportingCurrency);
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

            lblTotalSupportingDetailBC.Text = Helper.GetDisplayDecimalValue(oGLDataHdrInfoCollection[0].SupportingDetailBalanceBaseCurrency);
            lblTotalSupportingDetailRC.Text = Helper.GetDisplayDecimalValue(oGLDataHdrInfoCollection[0].SupportingDetailBalanceReportingCurrency);



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
    }
    private void ShowTaskStatus()
    {
        //if (NetAccountID != null && NetAccountID.Value > 0)
        //    trTaskMaster.Visible = false;
        //else
        trTaskMaster.Visible = true;
    }
}
