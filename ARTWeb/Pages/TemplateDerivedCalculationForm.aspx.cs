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
using SkyStem.Library.Controls.TelerikWebControls;

public partial class Pages_TemplateDerivedCalculationForm : PageBaseRecForm
{

    #region Variables & Constants
    public bool _IsRefreshData = false;
    bool _IsGLDataIDChanged = false;
    const ARTEnums.ReconciliationItemTemplate eReconciliationItemTemplateThisPage = ARTEnums.ReconciliationItemTemplate.DerivedCalculationForm;
    const bool IS_SUPPORTING_DETAIL_ENTRY_ON_TEMPLATE = true;// tells whether sum for SUPPORTING_DETAIL section will come from ItemInput grid or just on entry on the recForm itself
    WebEnums.ARTPages _ARTPages = WebEnums.ARTPages.AccountViewer;
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
            oRecPeriodMasterPageBase.PageTitleLabeID = 1099;

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
            {
                Session[SessionConstants.REC_ITEM_TEMPLATE_ID] = eReconciliationItemTemplateThisPage;
                OnPageLoad();
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
    protected void Page_PreRender(object sender, EventArgs e)
    {
        try
        {
            Helper.SetBreadcrumbsForRecForms(this, _ARTPages, 1099);
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

    void imgRecWriteOff_Click(object sender, ImageClickEventArgs e)
    {
        uctlItemInputWriteOff.IsExpanded = true;
        this.LoadRecItemsItemWriteOff();
        uctlItemInputWriteOff.ContentVisibility = true;
    }
    void imgUnexplainedVariance_Click(object sender, ImageClickEventArgs e)
    {
        this.uctlUnexplainedVariance.IsExpanded = true;
        this.LoadUnexplainedVariance();
        this.uctlUnexplainedVariance.ContentVisibility = true;

        // this.uctlUnexplainedVariance.ContentWidth = Convert.ToInt32(100);

    }
    void imgGLAdjustmentsOther_Click(object sender, ImageClickEventArgs e)
    {
        uctlGLAdjustmentsOther.IsExpanded = true;
        this.LoadRecItemsGLAdjustment(uctlGLAdjustmentsOther, WebEnums.RecCategory.SupportingDetail, WebEnums.RecCategoryType.DerivedCalculation_SupportingDetail_OtherDetails);
        uctlGLAdjustmentsOther.ContentVisibility = true;

    }
    void imgRecControlCheckList_Click(object sender, ImageClickEventArgs e)
    {
        ucRecControlCheckList.IsExpanded = true;
        LoadRecControlCheckList();
        ucRecControlCheckList.ContentVisibility = true;
    }
    void imgViewBankFee_Click(object sender, ImageClickEventArgs e)
    {

        GLAdjustments1.IsExpanded = true;
        this.LoadRecItemsGLAdjustment(GLAdjustments1, WebEnums.RecCategory.GLAdjustments, WebEnums.RecCategoryType.DerivedCalculation_GLAdjustments_Total);
        GLAdjustments1.ContentVisibility = true;

    }
    void imgViewTimingDifference_Click(object sender, ImageClickEventArgs e)
    {
        uctlTimmingDifference.IsExpanded = true;
        this.LoadRecItemsGLAdjustment(uctlTimmingDifference, WebEnums.RecCategory.TimingDifference, WebEnums.RecCategoryType.DerivedCalculation_TimingDifference_Total);
        uctlTimmingDifference.ContentVisibility = true;
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
                ReloadUserControls();

                // RecHelper.SetAccountTaskCount(lblPendingTaskStatus, ucRecFormAccountTaskGrid.TaskCountPending, lblCompletedTaskStatus, ucRecFormAccountTaskGrid.TaskCountCompleted);

                pnlRecForm.Enabled = false;
                if (GLDataID != null && GLDataID != 0)
                {
                    pnlRecForm.Enabled = true;
                    //this.ucRecFormButtons.EnableDisableButtons();
                    //decimal? exchangeRateBaseToReporting = CacheHelper.GetExchangeRate(this.CurrentBCCY, SessionHelper.ReportingCurrencyCode, SessionHelper.CurrentReconciliationPeriodID);
                    //decimal? exchangeRateReportingToBase = CacheHelper.GetExchangeRate(SessionHelper.ReportingCurrencyCode, this.CurrentBCCY, SessionHelper.CurrentReconciliationPeriodID);



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
                    //    , lblReconciledBalanceRC, lblReconciledBalanceBC
                    //    , lblTotalRecWriteOffRC, lblTotalRecWriteOffBC
                    //    , lblTotalUnExplainedVarianceRC, lblTotalUnExplainedVarianceBC, !string.IsNullOrEmpty(CurrentBCCY));

                }
            }
            //if (GLDataID != null && GLDataID != 0)
            //{
            //    MasterPageBase ompage = (MasterPageBase)this.Master.Master;
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
    private void ReloadUserControls()
    {

        // ReLoad the user controls that are already expanded
        RecHelper.ReloadUserControls(this.GLDataHdrInfo, uctlGLAdjustmentsOther, uctlTimmingDifference, uctlItemInputWriteOff, uctlUnexplainedVariance, ucEditQualityScore, GLAdjustments1, ucRecControlCheckList, ucRecFormAccountTaskGrid);
    }

    private void SetNewGLDataID()
    {
        // Since the GL Data ID would have changed, reset on all controls to refresh them if already expanded
        //uctlGLAdjustments.GLDataHdrInfo = this.GLDataHdrInfo;
        uctlTimmingDifference.GLDataHdrInfo = this.GLDataHdrInfo;
        uctlGLAdjustmentsOther.GLDataHdrInfo = this.GLDataHdrInfo;
        uctlItemInputWriteOff.GLDataHdrInfo = this.GLDataHdrInfo;
        uctlUnexplainedVariance.GLDataHdrInfo = this.GLDataHdrInfo;
        ucEditQualityScore.GLDataHdrInfo = this.GLDataHdrInfo;
        GLAdjustments1.GLDataHdrInfo = this.GLDataHdrInfo;
        ucRecFormAccountTaskGrid.GLDataHdrInfo = this.GLDataHdrInfo;
        ucRecControlCheckList.GLDataHdrInfo = this.GLDataHdrInfo;
    }
    private void SetEditMode()
    {
        uctlTimmingDifference.EditMode = this.EditMode;
        uctlGLAdjustmentsOther.EditMode = this.EditMode;
        uctlItemInputWriteOff.EditMode = this.EditMode;
        GLAdjustments1.EditMode = this.EditMode;
    }


    private void OnPageLoad()
    {
        Helper.SetPageTitle(this, 1099);
        Helper.HideMessage(this);

        RegisterToggleControl();
        ManagePackageFeatures();


        string pageID = Request.QueryString[QueryStringConstants.REFERRER_PAGE_ID];
        _ARTPages = (WebEnums.ARTPages)System.Enum.Parse(typeof(WebEnums.ARTPages), pageID);

        if (!string.IsNullOrEmpty(Request.QueryString[QueryStringConstants.GLDATA_ID]))
        {
            long? _gLDataID = Convert.ToInt64(Request.QueryString[QueryStringConstants.GLDATA_ID]);
            this.GLDataHdrInfo = Helper.GetGLDataHdrInfo(_gLDataID);
        }
        Helper.ValidateRecTemplateForGLDataID(this, this.GLDataHdrInfo, eReconciliationItemTemplateThisPage, _ARTPages);
        EditMode = Helper.GetFormMode(WebEnums.ARTPages.TemplateDerivedCalc, this.GLDataHdrInfo);
        if (this.GLDataHdrInfo != null)
        {
            lblPeriodEndDate.Text = string.Format(WebConstants.FORMAT_BRACKET, Helper.GetDisplayDate(SessionHelper.CurrentReconciliationPeriodEndDate));

            if (GLRecStatusID.HasValue)
            {
                //WebEnums.FormMode eFormMode = Helper.GetFormMode(WebEnums.ARTPages.TemplateBankAccount, (WebEnums.ReconciliationStatus)GLRecStatusID.Value, this.IsSRA);
                ////WebEnums.FormMode eFormMode = Helper.GetFormMode(WebEnums.ARTPages.TemplateDerivedCalc, this.GLDataHdrInfo);
                if (EditMode == WebEnums.FormMode.Edit)
                {
                    if (!string.IsNullOrEmpty(CurrentBCCY))
                    {
                        txtBankBalanceBC.Enabled = true;
                        cvBankBalanceBC.Enabled = true;
                    }
                    txtBankBalanceRC.Enabled = true;
                    cvBankBalanceRC.Enabled = true;
                    //txtBasisForCalculationExplanation.Enabled = true;
                    Helper.SetExTextBoxDisplayMode(txtBasisForCalculationExplanation, false);
                }
                else
                {
                    txtBankBalanceBC.Enabled = false;
                    txtBankBalanceRC.Enabled = false;
                    cvBankBalanceBC.Enabled = false;
                    cvBankBalanceRC.Enabled = false;
                    //txtBasisForCalculationExplanation.Enabled = false;
                    Helper.SetExTextBoxDisplayMode(txtBasisForCalculationExplanation, true);

                }
                // Apoorv: hack by using hidden control
                if (txtBasisForCalculationExplanation.Text == "")
                {
                    txtBasisForCalculationExplanation.Text = hdnBasisForCalculationExplanation.Value;
                }
            }




            //SetCapabilityFlags(SessionHelper.CurrentReconciliationPeriodID);
            //if (!IsPostBack)
            //{
            //SetURL(queryString);

            //if (this.GLDataHdrInfo != null && this.GLDataHdrInfo.NetAccountID.HasValue && this.GLDataHdrInfo.NetAccountID > 0)
            //{
            //    trAccountTask.Visible = false;
            //}

            SetURL();
            SetAllLabels();
            //}

            //Set properties for ucAccountInfo cntrol
            this.ucAccountInfo.GLDataID = this.GLDataID.HasValue ? this.GLDataID.Value : 0;
            this.ucAccountInfo.AccountID = AccountID.HasValue ? AccountID.Value : 0;
            this.ucAccountInfo.NetAccountID = NetAccountID.HasValue ? NetAccountID.Value : 0;

            //Set properties for DocumentUpload cntrol
            SetDocumentUploadURL();

            //Set properties for ButtonsControl cntrol
            SetButtonsControlProperties();

            // Set the Master Page Properties for GL Data ID
            RecHelper.SetRecStatusBarPropertiesForRecForm(this, GLDataID);

            setEntityNameLabelIDForGLAdjustments();

            // Display Non-Editbale Message
            Helper.ShowNonEditableMessage(this, GLDataHdrInfo);
            this.ucRecControlCheckList.GLDataHdrInfo = this.GLDataHdrInfo;
            this.ucRecControlCheckList.EditMode = Helper.GetFormModeForRecControlCheckList(this.GLDataHdrInfo);
        }

        SetPageSettings();
        //Added By Prafull for NetAccountComposition PopUp 
        //******************************************************************************************************************************
        if (NetAccountID == 0)
        {
            imgShowNetAccountComposition.Visible = false;
        }
        else
        {
            imgShowNetAccountComposition.Visible = true;
            imgShowNetAccountComposition.ToolTip = LanguageUtil.GetValue(2128);
            imgShowNetAccountComposition.OnClientClick = "javascript:OpenRadWindowForHyperlink('" + Page.ResolveClientUrl("~/Pages/PopupNetAccountComposition.aspx?" + QueryStringConstants.NETACCOUNT_ID + "=" + NetAccountID + "&" + QueryStringConstants.REC_PERIOD_ID + "=" + SessionHelper.CurrentReconciliationPeriodID.Value) + "', 350, 500, '" + imgShowNetAccountComposition.ClientID + "');";
        }
        //******************************************************************************************************************************
        this.ucRecFormButtons.EnableDisableButtons();


        decimal? exchangeRateBaseToReporting = CacheHelper.GetExchangeRate(this.CurrentBCCY, SessionHelper.ReportingCurrencyCode, SessionHelper.CurrentReconciliationPeriodID);
        decimal? exchangeRateReportingToBase = CacheHelper.GetExchangeRate(SessionHelper.ReportingCurrencyCode, this.CurrentBCCY, SessionHelper.CurrentReconciliationPeriodID);

        if (!string.IsNullOrEmpty(CurrentBCCY) && exchangeRateBaseToReporting == null)
        {
            throw new ARTException(5000106);
        }

        if (!string.IsNullOrEmpty(CurrentBCCY) && exchangeRateReportingToBase == null)
        {
            throw new ARTException(5000107);
        }

        cvBankBalanceBC.ErrorMessage = Helper.GetErrorMessage(WebEnums.FieldType.InvalidNumericField, 1436, 1493);
        cvBankBalanceRC.ErrorMessage = Helper.GetErrorMessage(WebEnums.FieldType.InvalidNumericField, 1436, 1424);

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
            , lblReconciledBalanceRC, lblReconciledBalanceBC
            , lblTotalRecWriteOffRC, lblTotalRecWriteOffBC
            , lblTotalUnExplainedVarianceRC, lblTotalUnExplainedVarianceBC, !string.IsNullOrEmpty(CurrentBCCY), hdnBankBalanceRC, hdnBankBalanceBC);

        RecHelper.RenderJSForOldValuesForRecalculateBalances(this, txtBankBalanceBC, txtBankBalanceRC);
        RecHelper.ShowHideReviewNotesAndQualityScore(trReviewNotes, trQualityScore, trRecControlCheckList);
        ucRecFormAccountTaskGrid.RegisterClientScripts();
    }
    private void setEntityNameLabelIDForGLAdjustments()
    {
        //if (ExLabel5.LabelID.ToString() != "1656")
        //    uctlGLAdjustments.EntityNameLabelID = ExLabel5.LabelID;
        //else
        //    uctlGLAdjustments.EntityNameLabelID = lblGLAdjustments.LabelID;

        //if (ExLabel1.LabelID.ToString() != "1656")
        //    uctlTimmingDifference.EntityNameLabelID = ExLabel1.LabelID;
        //else
        //    uctlTimmingDifference.EntityNameLabelID = lblTimmingDifference.LabelID;

        //if (ExLabel6.LabelID.ToString() != "1656")
        //    uctlGLAdjustmentsOther.EntityNameLabelID = ExLabel6.LabelID;
        //else
        //    uctlGLAdjustmentsOther.EntityNameLabelID = lblDerivedCalculation.LabelID;
    }


    private void RegisterToggleControl()
    {
        //Register toggle control with User Control
        //uctlGLAdjustments.RegisterToggleControl(imgViewBankFee);
        GLAdjustments1.RegisterToggleControl(imgViewBankFee);

        uctlTimmingDifference.RegisterToggleControl(imgViewTimingDifference);
        uctlItemInputWriteOff.RegisterToggleControl(imgRecWriteOff);
        uctlUnexplainedVariance.RegisterToggleControl(imgUnexplainedVariance);
        uctlGLAdjustmentsOther.RegisterToggleControl(imgGLAdjustmentsOther);
        ucEditQualityScore.RegisterToggleControl(imgQualityScore);
        ucRecFormAccountTaskGrid.RegisterToggleControl(imgAccountTask);
        ucRecControlCheckList.RegisterToggleControl(imgRecControlCheckList);
        imgQualityScore.Click += new ImageClickEventHandler(imgQualityScore_Click);

    }

    private void RegisterToggleControlEventHandler()
    {
        imgViewBankFee.Click += new ImageClickEventHandler(imgViewBankFee_Click);
        imgGLAdjustmentsOther.Click += new ImageClickEventHandler(imgGLAdjustmentsOther_Click);
        imgViewTimingDifference.Click += new ImageClickEventHandler(imgViewTimingDifference_Click);
        imgRecWriteOff.Click += new ImageClickEventHandler(imgRecWriteOff_Click);
        imgUnexplainedVariance.Click += new ImageClickEventHandler(imgUnexplainedVariance_Click);
        imgAccountTask.Click += new ImageClickEventHandler(imgAccountTask_Click);
        imgRecControlCheckList.Click += new ImageClickEventHandler(imgRecControlCheckList_Click);
    }

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
            oGLDataClient.UpdateGLDataForRemoveAccountSignOff(oAccountIDCollection, oNetAccountIDCollection, SessionHelper.CurrentReconciliationPeriodID, SessionHelper.CurrentUserLoginID, DateTime.Now, Helper.GetAppUserInfo());
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
        //GLDataHdrInfo oGLDataHdrInfo = Helper.GetGLDataHdrInfoToSaveOnTemplateForm(eReconciliationStatus, string commandName, out isSignOff, out isFormDataToBeSaved);
        GLDataHdrInfo oGLDataHdrInfo = Helper.GetGLDataHdrInfoToSaveOnTemplateForm(commandName, IsSRA, ucEditQualityScore.IsExpanded, ucRecControlCheckList.IsExpanded, out eNewReconciliationStatus, out isSignOff, out isFormDataToBeSaved);
        oGLDataHdrInfo.GLDataID = GLDataID;

        DerivedCalculationSupportingDetailInfo oDerivedCalculationSupportingDetailInfo = null;
        oDerivedCalculationSupportingDetailInfo = new DerivedCalculationSupportingDetailInfo();

        if (txtBankBalanceBC.Text.Trim() != "")
        {
            oDerivedCalculationSupportingDetailInfo.BaseCurrencyBalance = Convert.ToDecimal(txtBankBalanceBC.Text);
        }

        if (txtBankBalanceRC.Text.Trim() != "")
        {
            oDerivedCalculationSupportingDetailInfo.ReportingCurrencyBalance = Convert.ToDecimal(txtBankBalanceRC.Text);
        }

        if (txtBasisForCalculationExplanation.Text.Trim() != "")
        {
            oDerivedCalculationSupportingDetailInfo.BasisForDerivedCalculation = txtBasisForCalculationExplanation.Text;
        }

        oDerivedCalculationSupportingDetailInfo.RevisedBy = SessionHelper.CurrentUserLoginID;
        oDerivedCalculationSupportingDetailInfo.DateRevised = DateTime.Now;
        oDerivedCalculationSupportingDetailInfo.GLDataID = GLDataID;

        short? reconciliationCategoryTypeIDForSupportingDetail = (short?)WebEnums.RecCategoryType.DerivedCalculation_SupportingDetail_OtherDetails;
        oGLDataHdrInfo.GLDataQualityScoreInfoList = ucEditQualityScore.GetData();
        oGLDataHdrInfo.GLDataRecControlCheckListInfoList = ucRecControlCheckList.GetGLDataRecControlCheckListInfoList();
        IGLData oGLDataClient = RemotingHelper.GetGLDataObject();
        oGLDataClient.SaveReconciliationForm(GLDataID, oGLDataHdrInfo, isFormDataToBeSaved, isSignOff, IS_SUPPORTING_DETAIL_ENTRY_ON_TEMPLATE, false, oDerivedCalculationSupportingDetailInfo, reconciliationCategoryTypeIDForSupportingDetail, Helper.GetAppUserInfo());

        //Raise Alert for deny/Rejected command        
        Helper.RaiseAlertFromReconciliationTemplates(commandName, AccountID, NetAccountID);

        if (commandName == RecFormButtonCommandName.SIGNOFF

            //|| commandName == RecFormButtonCommandName.SAVE
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
    private void SetURL()
    {
        string queryString = "?" + QueryStringConstants.ACCOUNT_ID + "=" + AccountID
           + "&" + QueryStringConstants.NETACCOUNT_ID + "=" + NetAccountID
            + "&" + QueryStringConstants.GLDATA_ID + "=" + GLDataID
            + "&" + QueryStringConstants.REC_STATUS_ID + "=" + GLRecStatusID
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

        //mk  hlGLAdjustment.NavigateUrl = URLConstants.URL_ITEMINPUT_BANKFEE + queryString + (short)WebEnums.RecCategoryType.DerivedCalculation_GLAdjustments_Total + "&" + QueryStringConstants.REC_CATEGORY_ID + "=" + (short)WebEnums.RecCategory.GLAdjustments;
        //mk  hlTimingDifference.NavigateUrl = URLConstants.URL_ITEMINPUT_BANKFEE + queryString + (short)WebEnums.RecCategoryType.DerivedCalculation_TimingDifference_Total + "&" + QueryStringConstants.REC_CATEGORY_ID + "=" + (short)WebEnums.RecCategory.TimingDifference;
        //mk hlSupportingDetail.NavigateUrl = URLConstants.URL_ITEMINPUT_BANKFEE + queryString + (short)WebEnums.RecCategoryType.DerivedCalculation_SupportingDetail_OtherDetails + "&" + QueryStringConstants.REC_CATEGORY_ID + "=" + (short)WebEnums.RecCategory.SupportingDetail;

        hlImportGLAdjustment.NavigateUrl = URLConstants.URL_IMPORT_BANKFEE + queryString + (short)WebEnums.RecCategoryType.DerivedCalculation_GLAdjustments_Total + "&" + QueryStringConstants.REC_CATEGORY_ID + "=" + (short)WebEnums.RecCategory.GLAdjustments;
        hlImportTimingDifference.NavigateUrl = URLConstants.URL_IMPORT_BANKFEE + queryString + (short)WebEnums.RecCategoryType.DerivedCalculation_TimingDifference_Total + "&" + QueryStringConstants.REC_CATEGORY_ID + "=" + (short)WebEnums.RecCategory.TimingDifference;
        hlImportSupportingDetail.NavigateUrl = URLConstants.URL_IMPORT_BANKFEE + queryString + (short)WebEnums.RecCategoryType.DerivedCalculation_SupportingDetail_OtherDetails + "&" + QueryStringConstants.REC_CATEGORY_ID + "=" + (short)WebEnums.RecCategory.SupportingDetail;

        string queryStringForRCCLImport = "?" + QueryStringConstants.ACCOUNT_ID + "=" + AccountID
       + "&" + QueryStringConstants.NETACCOUNT_ID + "=" + NetAccountID
        + "&" + QueryStringConstants.GLDATA_ID + "=" + GLDataID;
        hlImportRecControlCheckList.NavigateUrl = URLConstants.URL_IMPORT_RECCONTROLCHECKLIST + queryString;

        //mk hlRecWriteOff.NavigateUrl = URLConstants.URL_ITEMINPUT_WRITEOFF + queryString + (short)WebEnums.RecCategoryType.DerivedCalculation_RecWriteoffOn_WriteoffOn + "&" + QueryStringConstants.REC_CATEGORY_ID + "=" + (short)WebEnums.RecCategory.VariancesWriteOffOn;
        //mkhlUnexplainedVariance.NavigateUrl = URLConstants.URL_ITEMINPUT_UNEXPLAINEDVARIANCE + queryString + (short)WebEnums.RecCategoryType.DerivedCalculation_RecWriteoffOn_UnexpVar + "&" + QueryStringConstants.REC_CATEGORY_ID + "=" + (short)WebEnums.RecCategory.VariancesWriteOffOn;
        hlUnexplainedVarianceHistory.NavigateUrl = URLConstants.URL_ITEMINPUT_UNEXPLAINEDVARIANCE_HISTORY + queryString + (short)WebEnums.RecCategoryType.DerivedCalculation_RecWriteoffOn_UnexpVar + "&" + QueryStringConstants.REC_CATEGORY_ID + "=" + (short)WebEnums.RecCategory.VariancesWriteOffOn;

        hlReviewNotes.NavigateUrl = URLConstants.URL_ITEMINPUT_COMMENT + queryString + (short)WebEnums.RecCategoryType.DerivedCalculation_ReviewNotes + "&" + QueryStringConstants.REC_CATEGORY_ID + "=" + (short)WebEnums.RecCategory.ReviewNotes;
        hlAuditTrail.NavigateUrl = URLConstants.URL_AUDIT_TRAIL + queryString + (short)WebEnums.RecCategoryType.DerivedCalculation_ReviewNotes + "&" + QueryStringConstants.REC_CATEGORY_ID + "=" + (short)WebEnums.RecCategory.ReviewNotes;

        RegisterToggleControlEventHandler();
        ucAccountMatching.SetURL(AccountID, NetAccountID, GLDataID, Request.Url.AbsoluteUri);
    }

    private void SetButtonsControlProperties()
    {
        this.ucRecFormButtons.GLDataHdrInfo = this.GLDataHdrInfo;
        this.ucRecFormButtons.CurrentUserRole = SessionHelper.CurrentRoleID.Value;
        this.ucRecFormButtons.ReconciliationStatusID = GLRecStatusID;
        this.ucRecFormButtons.eARTPages = WebEnums.ARTPages.TemplateDerivedCalc;
    }

    private void SetDocumentUploadURL()
    {
        if (GLDataID == null || GLDataID == 0 ||
          GLRecStatusID == null || GLRecStatusID == 0)
            this.hlDocument.NavigateUrl = ScriptHelper.GetJSForEmptyURL();
        else
        {
            string windowName;
            this.hlDocument.NavigateUrl = "javascript:OpenRadWindowForHyperlink('" + Helper.SetDocumentUploadURL(GLDataID, this.AccountID, this.NetAccountID, this.IsSRA, Request.Url.ToString(), out windowName) + "', " + WebConstants.POPUP_HEIGHT + " , " + WebConstants.POPUP_WIDTH + ");";
            //this.ucDocumentUpload.WindowName = windowName;
        }
    }

    private void SetDefaulValueForLabelTotal()
    {
        decimal? defValue = null;
        if (!String.IsNullOrEmpty(this.CurrentBCCY))
            defValue = 0;

        lblTotalGLAdjustmentsBC.Text = Helper.GetDisplayDecimalValue(defValue);
        lblTotalGLAdjustmentsRC.Text = Helper.GetDisplayDecimalValue(0);
        lblTotalTimingDifferenceBC.Text = Helper.GetDisplayDecimalValue(defValue);
        lblTotalTimingDifferenceRC.Text = Helper.GetDisplayDecimalValue(0);
        lblTotalSupportingDetailBC.Text = Helper.GetDisplayDecimalValue(defValue);
        lblTotalSupportingDetailRC.Text = Helper.GetDisplayDecimalValue(0);
        lblTotalRecWriteOffRC.Text = Helper.GetDisplayDecimalValue(0);
        lblSystemScore.Text = Helper.GetDisplayIntegerValue(0);
        lblUserScore.Text = Helper.GetDisplayIntegerValue(0);
        //lblRecControlTotalValue.Text = Helper.GetDisplayIntegerValue(0);
        //lblRecControlCompletedValue.Text = Helper.GetDisplayIntegerValue(0);


        if (!IsPostBack || _IsGLDataIDChanged) // _IsRefreshData
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

        lblReconciledBalanceBC.Text = Helper.GetDisplayDecimalValue(null);
        lblReconciledBalanceRC.Text = Helper.GetDisplayDecimalValue(0);
        lblTotalRecWriteOffBC.Text = Helper.GetDisplayDecimalValue(defValue);

        lblTotalUnExplainedVarianceBC.Text = Helper.GetDisplayDecimalValue(null);
        lblTotalUnExplainedVarianceRC.Text = Helper.GetDisplayDecimalValue(0);
        lblCountAttachedDocument.Text = string.Format(WebConstants.FORMAT_BRACKET, "0");
        lblCountReviewNotes.Text = string.Format(WebConstants.FORMAT_BRACKET, "0");

        if (GLDataID == null || GLDataID == 0)
            txtBasisForCalculationExplanation.Text = "";

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

        IGLData oGLDataClient = RemotingHelper.GetGLDataObject();
        // List<GLDataHdrInfo> oGLDataHdrInfoCollection = oGLDataClient.SelectGLDataHdrByGLDataID(GLDataID, Helper.GetAppUserInfo());
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

        decimal? glAdjustmentBC = 0M;
        decimal? glAdjustmentRC = 0M;

        decimal? glTimingDiffBC = 0M;
        decimal? glTimingDiffRC = 0M;

        decimal? glSupportingDetailBC = 0M;
        decimal? glSupportingDetailRC = 0M;
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
                    //switch (oGLReconciliationItemInputInfo.ReconciliationCategoryTypeID.Value)
                    switch (eReconciliationCategoryType)
                    {
                        case WebEnums.RecCategoryType.DerivedCalculation_GLAdjustments_Total:
                            glAdjustmentBC = oGLReconciliationItemInputInfo.AmountBaseCurrency;
                            glAdjustmentRC = oGLReconciliationItemInputInfo.AmountReportingCurrency;

                            lblTotalGLAdjustmentsBC.Text = Helper.GetDisplayBaseCurrencyValue(this.CurrentBCCY, oGLReconciliationItemInputInfo.AmountBaseCurrency);
                            lblTotalGLAdjustmentsRC.Text = Helper.GetDisplayDecimalValue(oGLReconciliationItemInputInfo.AmountReportingCurrency);
                            break;
                        case WebEnums.RecCategoryType.DerivedCalculation_TimingDifference_Total:
                            glTimingDiffBC = oGLReconciliationItemInputInfo.AmountBaseCurrency;
                            glTimingDiffRC = oGLReconciliationItemInputInfo.AmountReportingCurrency;

                            lblTotalTimingDifferenceBC.Text = Helper.GetDisplayBaseCurrencyValue(this.CurrentBCCY, oGLReconciliationItemInputInfo.AmountBaseCurrency);
                            lblTotalTimingDifferenceRC.Text = Helper.GetDisplayDecimalValue(oGLReconciliationItemInputInfo.AmountReportingCurrency);
                            break;
                        case WebEnums.RecCategoryType.DerivedCalculation_SupportingDetail_OtherDetails:
                            //TODO: check if this label show the just SUM(other detail) or SUM( SupportingDetail section)   
                            glSupportingDetailBC = oGLReconciliationItemInputInfo.AmountBaseCurrency;
                            glSupportingDetailRC = oGLReconciliationItemInputInfo.AmountReportingCurrency;

                            lblTotalSupportingDetailBC.Text = Helper.GetDisplayBaseCurrencyValue(this.CurrentBCCY, oGLReconciliationItemInputInfo.AmountBaseCurrency);
                            lblTotalSupportingDetailRC.Text = Helper.GetDisplayDecimalValue(oGLReconciliationItemInputInfo.AmountReportingCurrency);
                            break;
                    }
                }
            }

            IGLData oGLDataClient = RemotingHelper.GetGLDataObject();
            // List<GLDataHdrInfo> oGLDataHdrInfoCollection = oGLDataClient.SelectGLDataHdrByGLDataID(GLDataID, Helper.GetAppUserInfo());
            List<GLDataHdrInfo> oGLDataHdrInfoCollection = new List<GLDataHdrInfo>();
            oGLDataHdrInfoCollection.Add(this.GLDataHdrInfo);
            if (oGLDataHdrInfoCollection != null && oGLDataHdrInfoCollection.Count > 0)
            {
                //this._BaseCurrencyCode = oGLDataHdrInfoCollection[0].BaseCurrencyCode;
                lblGLBalanceBC.Text = Helper.GetDisplayCurrencyValue(oGLDataHdrInfoCollection[0].BaseCurrencyCode, oGLDataHdrInfoCollection[0].GLBalanceBaseCurrency);
                lblGLBalanceRC.Text = Helper.GetDisplayReportingCurrencyValue(oGLDataHdrInfoCollection[0].GLBalanceReportingCurrency);
                // Set in Hidden
                hdnGLBalanceBC.Value = Helper.GetDisplayDecimalValue(oGLDataHdrInfoCollection[0].GLBalanceBaseCurrency);
                hdnGLBalanceRC.Value = Helper.GetDisplayDecimalValue(oGLDataHdrInfoCollection[0].GLBalanceReportingCurrency);

                glAdjustmentAndTimingDiffBC = glAdjustmentBC + glTimingDiffBC;
                glAdjustmentAndTimingDiffRC = glAdjustmentRC + glTimingDiffRC;

                hdnGlAdjustmentAndTimingDiffBC.Value = Helper.GetDisplayDecimalValue(glAdjustmentAndTimingDiffBC);
                hdnGlAdjustmentAndTimingDiffRC.Value = Helper.GetDisplayDecimalValue(glAdjustmentAndTimingDiffRC);
                hdnSupportingDetailBC.Value = Helper.GetDisplayDecimalValue(glSupportingDetailBC);
                hdnSupportingDetailRC.Value = Helper.GetDisplayDecimalValue(glSupportingDetailRC);

                lblReconciledBalanceBC.Text = Helper.GetDisplayDecimalValue(oGLDataHdrInfoCollection[0].ReconciliationBalanceBaseCurrency);
                if (oGLDataHdrInfoCollection[0].ReconciliationBalanceReportingCurrency.HasValue)
                    lblReconciledBalanceRC.Text = Helper.GetDisplayDecimalValue(oGLDataHdrInfoCollection[0].ReconciliationBalanceReportingCurrency);
                if (oGLDataHdrInfoCollection[0].WriteOnOffAmountBaseCurrency.HasValue)
                    lblTotalRecWriteOffBC.Text = Helper.GetDisplayDecimalValue(oGLDataHdrInfoCollection[0].WriteOnOffAmountBaseCurrency);
                if (oGLDataHdrInfoCollection[0].WriteOnOffAmountReportingCurrency.HasValue)
                    lblTotalRecWriteOffRC.Text = Helper.GetDisplayDecimalValue(oGLDataHdrInfoCollection[0].WriteOnOffAmountReportingCurrency);

                //extra section
                decimal? SupportingDetailDerivedBC = 0M;
                decimal? SupportingDetailDerivedRC = 0M;
                if ((!IsPostBack || _IsGLDataIDChanged) && oGLDataHdrInfoCollection != null && oGLDataHdrInfoCollection.Count > 0)
                {
                    List<DerivedCalculationSupportingDetailInfo> oDerivedCalculationSupportingDetailInfoCollection = oGLDataClient.SelectAllDerivedCalculationSupportingDetailInfoByGLDataID(GLDataID, Helper.GetAppUserInfo());
                    if (oDerivedCalculationSupportingDetailInfoCollection != null && oDerivedCalculationSupportingDetailInfoCollection.Count > 0 && oDerivedCalculationSupportingDetailInfoCollection[0] != null)
                    {
                        SupportingDetailDerivedBC = oDerivedCalculationSupportingDetailInfoCollection[0].BaseCurrencyBalance;
                        SupportingDetailDerivedRC = oDerivedCalculationSupportingDetailInfoCollection[0].ReportingCurrencyBalance;
                        txtBankBalanceBC.Text = Helper.GetDecimalValueForTextBox(SupportingDetailDerivedBC, WebConstants.INT_FOR_DECIMAL_VALUE_TEXTBOX);
                        txtBankBalanceRC.Text = Helper.GetDecimalValueForTextBox(SupportingDetailDerivedRC, WebConstants.INT_FOR_DECIMAL_VALUE_TEXTBOX);
                        txtBasisForCalculationExplanation.Text = oDerivedCalculationSupportingDetailInfoCollection[0].BasisForDerivedCalculation;
                        hdnBasisForCalculationExplanation.Value = txtBasisForCalculationExplanation.Text;
                    }
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
                        txtBankBalanceRC.Text = Helper.GetDecimalValueForTextBox(RecHelper.ConvertCurrency(this.CurrentBCCY, SessionHelper.ReportingCurrencyCode, SessionHelper.CurrentReconciliationPeriodID, SupportingDetailDerivedBC), WebConstants.INT_FOR_DECIMAL_VALUE_TEXTBOX);
                    }
                    if (txtBankBalanceRC.Text.Trim() != "" && String.IsNullOrEmpty(txtBankBalanceBC.Text))
                    {
                        txtBankBalanceBC.Text = Helper.GetDecimalValueForTextBox(RecHelper.ConvertCurrency(SessionHelper.ReportingCurrencyCode, this.CurrentBCCY, SessionHelper.CurrentReconciliationPeriodID, SupportingDetailDerivedRC), WebConstants.INT_FOR_DECIMAL_VALUE_TEXTBOX);
                    }
                    if (!string.IsNullOrEmpty(txtBankBalanceBC.Text))
                        SupportingDetailDerivedBC = Convert.ToDecimal(txtBankBalanceBC.Text);
                    if (!string.IsNullOrEmpty(txtBankBalanceRC.Text))
                        SupportingDetailDerivedRC = Convert.ToDecimal(txtBankBalanceRC.Text);
                }

                oGLDataHdrInfoCollection[0].UnexplainedVarianceBaseCurrency = RecHelper.CaclculateUnexplainedVariance(
                    oGLDataHdrInfoCollection[0].GLBalanceBaseCurrency,
                    glAdjustmentBC,
                    glTimingDiffBC,
                    glSupportingDetailBC + SupportingDetailDerivedBC,
                    oGLDataHdrInfoCollection[0].WriteOnOffAmountBaseCurrency);
                oGLDataHdrInfoCollection[0].UnexplainedVarianceReportingCurrency = RecHelper.CaclculateUnexplainedVariance(
                    oGLDataHdrInfoCollection[0].GLBalanceReportingCurrency,
                    glAdjustmentRC,
                    glTimingDiffRC,
                    glSupportingDetailRC + SupportingDetailDerivedRC,
                    oGLDataHdrInfoCollection[0].WriteOnOffAmountReportingCurrency);

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
        GLAdjustments1.ApplyFilter();
        uctlTimmingDifference.ApplyFilter();
        uctlGLAdjustmentsOther.ApplyFilter();
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
            //this.ucEditQualityScore.NetAccountID = NetAccountID;
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

    void LoadAccountTask()
    {
        if (this.GLDataHdrInfo != null && trAccountTask.Visible)
        {
            ucRecFormAccountTaskGrid.IsRefreshData = true;
            ucRecFormAccountTaskGrid.GLDataHdrInfo = this.GLDataHdrInfo;
            ucRecFormAccountTaskGrid.RegisterClientScripts();
            this.ucRecFormAccountTaskGrid.LoadData();
            ucRecFormAccountTaskGrid.Visible = true;
        }
        else
        {
            ucRecFormAccountTaskGrid.Visible = false;
        }

        RecHelper.SetAccountTaskCount(lblPendingTaskStatus, ucRecFormAccountTaskGrid.TaskCountPending, lblCompletedTaskStatus, ucRecFormAccountTaskGrid.TaskCountCompleted);
    }
    void LoadUnexplainedVariance()
    {
        if (!this.uctlUnexplainedVariance.Visible)
        {
            //this.uctlUnexplainedVariance.AccountID = AccountID;
            //this.uctlUnexplainedVariance.NetAccountID = NetAccountID;
            //this.uctlUnexplainedVariance.GLDataID = GLDataID;
            //if (this.IsSRA.HasValue && this.IsSRA.Value)
            //    this.uctlUnexplainedVariance.IsSRA = true;
            //else
            //    this.uctlUnexplainedVariance.IsSRA = false;
            this.uctlUnexplainedVariance.GLDataHdrInfo = this.GLDataHdrInfo;
            this.uctlUnexplainedVariance.IsRefreshData = true;
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
    void LoadRecItemsItemWriteOff()
    {
        if (!this.uctlItemInputWriteOff.Visible)
        {
            this.uctlItemInputWriteOff.IsRefreshData = true;
            this.uctlItemInputWriteOff.GLDataHdrInfo = this.GLDataHdrInfo;
            uctlItemInputWriteOff.EditMode = this.EditMode;
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



    #endregion

}//end of class
