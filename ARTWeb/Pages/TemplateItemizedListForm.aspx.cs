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
using SkyStem.Library.Controls.TelerikWebControls;

public partial class Pages_TemplateItemizedListForm : PageBaseRecForm
{
    #region Variables & Constants
    const ARTEnums.ReconciliationItemTemplate eReconciliationItemTemplateThisPage = ARTEnums.ReconciliationItemTemplate.ItemizedListForm;
    const bool IS_SUPPORTING_DETAIL_ENTRY_ON_TEMPLATE = false;// tells whether sum for SUPPORTING_DETAIL section will come from ItemInput grid or just on entry on the recForm itself
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
            oRecPeriodMasterPageBase.PageTitleLabeID = 1103;

            MasterPageBase ompage = (MasterPageBase)this.Master.Master;
            ompage.ReconciliationPeriodChangedEventHandler += new EventHandler(ompage_ReconciliationPeriodChangedEventHandler);
            //ompage.ReconciliationPeriodChangedEventHandler += new EventHandler(uctlGLAdjustment.ompage_ReconciliationPeriodChangedEventHandler);
            //ompage.ReconciliationPeriodChangedEventHandler += new EventHandler(uctlTimingDifference.ompage_ReconciliationPeriodChangedEventHandler);
            //ompage.ReconciliationPeriodChangedEventHandler += new EventHandler(uctlSupportingDetail.ompage_ReconciliationPeriodChangedEventHandler);
            //ompage.ReconciliationPeriodChangedEventHandler += new EventHandler(uctlRecWriteOff.ompage_ReconciliationPeriodChangedEventHandler);
            //ompage.ReconciliationPeriodChangedEventHandler += new EventHandler(uctlUnexplainedVariance.ompage_ReconciliationPeriodChangedEventHandler);

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
            Helper.SetBreadcrumbsForRecForms(this, _ARTPages, 1103);

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


    void imgUnexplainedVariance_Click(object sender, ImageClickEventArgs e)
    {
        this.uctlUnexplainedVariance.IsExpanded = true;
        this.LoadUnexplainedVariance();
        this.uctlUnexplainedVariance.ContentVisibility = true;
    }

    private void SaveOnButtonClick(string commandName)
    {

        bool isSignOff = false;
        bool isFormDataToBeSaved = true;
        WebEnums.ReconciliationStatus eNewReconciliationStatus;
        GLDataHdrInfo oGLDataHdrInfo = Helper.GetGLDataHdrInfoToSaveOnTemplateForm(commandName, IsSRA, ucEditQualityScore.IsExpanded, ucRecControlCheckList.IsExpanded, out eNewReconciliationStatus, out isSignOff, out isFormDataToBeSaved);
        oGLDataHdrInfo.GLDataID = GLDataID;
        short? reconciliationCategoryTypeIDForSupportingDetail = (short?)WebEnums.RecCategoryType.ItemizedList_SupportingDetail_SupportingDetailList;
        oGLDataHdrInfo.GLDataQualityScoreInfoList = ucEditQualityScore.GetData();
        oGLDataHdrInfo.GLDataRecControlCheckListInfoList = ucRecControlCheckList.GetGLDataRecControlCheckListInfoList();
        IGLData oGLDataClient = RemotingHelper.GetGLDataObject();
        oGLDataClient.SaveReconciliationForm(GLDataID, oGLDataHdrInfo, isFormDataToBeSaved, isSignOff, IS_SUPPORTING_DETAIL_ENTRY_ON_TEMPLATE, false, null, reconciliationCategoryTypeIDForSupportingDetail, Helper.GetAppUserInfo());

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
            //HttpContext.Current.Response.Redirect(Helper.GetRedirectURLForTemplatePages(this.IsSRA, _ARTPages));
            SessionHelper.RedirectToUrl(Helper.GetRedirectURLForTemplatePages(this.IsSRA, _ARTPages));
            return;
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

    protected void ompage_ReconciliationPeriodChangedEventHandler(object sender, EventArgs e)
    {
        try
        {
            if (IsPostBack)
            {
                if (this.GLDataHdrInfo != null)
                    PreviousNetAccountId = this.GLDataHdrInfo.NetAccountID;
                OnPageLoad();
                Helper.ValidateRecTemplateForAccountAndNetAccount(this, this.GLDataHdrInfo, PreviousNetAccountId);
                // Since the GL Data ID would have changed due to change of Rec Period
                // reset on all controls to refresh them if already expanded
                SetNewGLDataID();
                SetEditMode();
                ReloadUserControls();

                //RecHelper.SetAccountTaskCount(lblPendingTaskStatus, ucRecFormAccountTaskGrid.TaskCountPending, lblCompletedTaskStatus, ucRecFormAccountTaskGrid.TaskCountCompleted);

                pnlRecForm.Enabled = false;
                if (GLDataID != null && GLDataID != 0)
                {
                    pnlRecForm.Enabled = true;

                    this.ucRecFormButtons.EnableDisableButtons();

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

    void imgSupportingDetail_Click(object sender, ImageClickEventArgs e)
    {
        uctlSupportingDetail.IsExpanded = true;
        this.LoadRecItemsGLAdjustment(uctlSupportingDetail, WebEnums.RecCategory.SupportingDetail, WebEnums.RecCategoryType.ItemizedList_SupportingDetail_SupportingDetailList);
        uctlSupportingDetail.ContentVisibility = true;
    }
    void imgTimingDifference_Click(object sender, ImageClickEventArgs e)
    {
        uctlTimingDifference.IsExpanded = true;
        this.LoadRecItemsGLAdjustment(uctlTimingDifference, WebEnums.RecCategory.TimingDifference, WebEnums.RecCategoryType.ItemizedList_TimingDifference_Total);
        uctlTimingDifference.ContentVisibility = true;
    }

    void imgGLAdjustment_Click(object sender, ImageClickEventArgs e)
    {
        uctlGLAdjustment.IsExpanded = true;
        this.LoadRecItemsGLAdjustment(uctlGLAdjustment, WebEnums.RecCategory.GLAdjustments, WebEnums.RecCategoryType.ItemizedList_GLAdjustments_Total);
        uctlGLAdjustment.ContentVisibility = true;
    }

    #endregion

    #region Validation Control Events
    #endregion

    #region Private Methods
    private void ReloadUserControls()
    {
        // ReLoad the user controls that are already expanded
        RecHelper.ReloadUserControls(this.GLDataHdrInfo, uctlGLAdjustment, uctlTimingDifference, uctlSupportingDetail, uctlRecWriteOff, uctlUnexplainedVariance, ucEditQualityScore, ucRecControlCheckList, ucRecFormAccountTaskGrid);
    }

    private void SetNewGLDataID()
    {
        // Since the GL Data ID would have changed, reset on all controls to refresh them if already expanded
        uctlGLAdjustment.GLDataHdrInfo = GLDataHdrInfo;
        uctlTimingDifference.GLDataHdrInfo = GLDataHdrInfo;
        uctlSupportingDetail.GLDataHdrInfo = GLDataHdrInfo;
        uctlRecWriteOff.GLDataHdrInfo = GLDataHdrInfo;
        uctlUnexplainedVariance.GLDataHdrInfo = GLDataHdrInfo;
        ucEditQualityScore.GLDataHdrInfo = GLDataHdrInfo;
        ucRecFormAccountTaskGrid.GLDataHdrInfo = this.GLDataHdrInfo;
        ucRecControlCheckList.GLDataHdrInfo = this.GLDataHdrInfo;
    }
    private void SetEditMode()
    {
        uctlGLAdjustment.EditMode = this.EditMode;
        uctlTimingDifference.EditMode = this.EditMode;
        uctlSupportingDetail.EditMode = this.EditMode;
        uctlRecWriteOff.EditMode = this.EditMode;
    }

    private void OnPageLoad()
    {
        Helper.SetPageTitle(this, 1103);
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
        EditMode = Helper.GetFormMode(WebEnums.ARTPages.TemplateItemized, this.GLDataHdrInfo);
        SetEditMode();
        Helper.ValidateRecTemplateForGLDataID(this, this.GLDataHdrInfo, eReconciliationItemTemplateThisPage, _ARTPages);
        if (this.GLDataHdrInfo != null)
        {

            lblPeriodEndDate.Text = string.Format(WebConstants.FORMAT_BRACKET, Helper.GetDisplayDate(SessionHelper.CurrentReconciliationPeriodEndDate));

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
            this.ucAccountInfo.GLDataID = GLDataID.HasValue ? GLDataID.Value : 0;
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

        this.ucRecFormButtons.EnableDisableButtons();
        RecHelper.ShowHideReviewNotesAndQualityScore(trReviewNotes, trQualityScore, trRecControlCheckList);
        ucRecFormAccountTaskGrid.RegisterClientScripts();
        if (!Page.IsPostBack)
            AutoExpandSections();
    }

    private void AutoExpandSections()
    {
        List<AutoSaveAttributeValueInfo> oAutoSaveAttributeList = Helper.GetAutoSaveAttributeValues();
        if (oAutoSaveAttributeList != null && oAutoSaveAttributeList.Count > 0)
        {
            foreach (AutoSaveAttributeValueInfo item in oAutoSaveAttributeList)
            {
                switch ((ARTEnums.AutoSaveAttribute)item.AutoSaveAttributeID)
                {
                    case ARTEnums.AutoSaveAttribute.ItemizedListFormAdjustmentsTotal:
                        if (uctlGLAdjustment.AutoSaveAttributeID.HasValue && Convert.ToBoolean(item.Value))
                        {
                            imgGLAdjustment_Click(imgGLAdjustment, null);
                        }
                        break;
                    case ARTEnums.AutoSaveAttribute.ItemizedListFormTimingDifferenceTotal:
                        if (uctlTimingDifference.AutoSaveAttributeID.HasValue && Convert.ToBoolean(item.Value))
                        {
                            imgTimingDifference_Click(imgTimingDifference, null);
                        }
                        break;
                    case ARTEnums.AutoSaveAttribute.ItemizedListFormSupportingDetailTotal:
                        if (uctlSupportingDetail.AutoSaveAttributeID.HasValue && Convert.ToBoolean(item.Value))
                        {
                            imgSupportingDetail_Click(imgSupportingDetail, null);
                        }
                        break;
                    case ARTEnums.AutoSaveAttribute.ItemizedListFormReconciliationWriteOffsOns:
                        if (uctlRecWriteOff.AutoSaveAttributeID.HasValue && Convert.ToBoolean(item.Value))
                        {
                            imgRecWriteOff_Click(imgRecWriteOff, null);
                        }
                        break;
                    case ARTEnums.AutoSaveAttribute.ItemizedListFormUnexpVar:
                        if (uctlUnexplainedVariance.AutoSaveAttributeID.HasValue && Convert.ToBoolean(item.Value))
                        {
                            imgUnexplainedVariance_Click(imgUnexplainedVariance, null);
                        }
                        break;
                    case ARTEnums.AutoSaveAttribute.ItemizedListFormQualityScore:
                        if (ucEditQualityScore.AutoSaveAttributeID.HasValue && Convert.ToBoolean(item.Value))
                        {
                            imgQualityScore_Click(imgQualityScore, null);
                        }
                        break;
                    case ARTEnums.AutoSaveAttribute.ItemizedListFormRCCStatus:
                        if (ucRecControlCheckList.AutoSaveAttributeID.HasValue && Convert.ToBoolean(item.Value))
                        {
                            imgRecControlCheckList_Click(imgRecControlCheckList, null);
                        }
                        break;
                    case ARTEnums.AutoSaveAttribute.ItemizedListFormTaskStatus:
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
            //Response.Redirect(path, false);
            SessionHelper.RedirectToUrl(path);
            return;
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

        //hlGLAdjustment.NavigateUrl = URLConstants.URL_ITEMINPUT_BANKFEE + queryString + (short)WebEnums.RecCategoryType.ItemizedList_GLAdjustments_Total + "&" + QueryStringConstants.REC_CATEGORY_ID + "=" + (short)WebEnums.RecCategory.GLAdjustments;
        //hlTimingDifference.NavigateUrl = URLConstants.URL_ITEMINPUT_BANKFEE + queryString + (short)WebEnums.RecCategoryType.ItemizedList_TimingDifference_Total + "&" + QueryStringConstants.REC_CATEGORY_ID + "=" + (short)WebEnums.RecCategory.TimingDifference ;
        // hlSupportingDetail.NavigateUrl = URLConstants.URL_ITEMINPUT_BANKFEE + queryString + (short)WebEnums.RecCategoryType.ItemizedList_SupportingDetail_SupportingDetailList + "&" + QueryStringConstants.REC_CATEGORY_ID + "=" + (short)WebEnums.RecCategory.SupportingDetail ;

        hlImportGLAdjustment.NavigateUrl = URLConstants.URL_IMPORT_BANKFEE + queryString + (short)WebEnums.RecCategoryType.ItemizedList_GLAdjustments_Total + "&" + QueryStringConstants.REC_CATEGORY_ID + "=" + (short)WebEnums.RecCategory.GLAdjustments;
        hlImportTimingDifference.NavigateUrl = URLConstants.URL_IMPORT_BANKFEE + queryString + (short)WebEnums.RecCategoryType.ItemizedList_TimingDifference_Total + "&" + QueryStringConstants.REC_CATEGORY_ID + "=" + (short)WebEnums.RecCategory.TimingDifference;
        hlImportSupportingDetail.NavigateUrl = URLConstants.URL_IMPORT_BANKFEE + queryString + (short)WebEnums.RecCategoryType.ItemizedList_SupportingDetail_SupportingDetailList + "&" + QueryStringConstants.REC_CATEGORY_ID + "=" + (short)WebEnums.RecCategory.SupportingDetail;
        string queryStringForRCCLImport = "?" + QueryStringConstants.ACCOUNT_ID + "=" + AccountID
      + "&" + QueryStringConstants.NETACCOUNT_ID + "=" + NetAccountID
       + "&" + QueryStringConstants.GLDATA_ID + "=" + GLDataID;
        hlImportRecControlCheckList.NavigateUrl = URLConstants.URL_IMPORT_RECCONTROLCHECKLIST + queryString;

        //hlRecWriteOff.NavigateUrl = URLConstants.URL_ITEMINPUT_WRITEOFF + queryString + (short)WebEnums.RecCategoryType.ItemizedList_RecWriteoffOn_WriteoffOn + "&" + QueryStringConstants.REC_CATEGORY_ID + "=" + (short)WebEnums.RecCategory.VariancesWriteOffOn ;
        //hlUnexplainedVariance.NavigateUrl = URLConstants.URL_ITEMINPUT_UNEXPLAINEDVARIANCE + queryString + (short)WebEnums.RecCategoryType.ItemizedList_RecWriteoffOn_UnexpVar + "&" + QueryStringConstants.REC_CATEGORY_ID + "=" + (short)WebEnums.RecCategory.VariancesWriteOffOn;
        hlUnexplainedVarianceHistory.NavigateUrl = URLConstants.URL_ITEMINPUT_UNEXPLAINEDVARIANCE_HISTORY + queryString + (short)WebEnums.RecCategoryType.ItemizedList_RecWriteoffOn_UnexpVar + "&" + QueryStringConstants.REC_CATEGORY_ID + "=" + (short)WebEnums.RecCategory.VariancesWriteOffOn;

        hlReviewNotes.NavigateUrl = URLConstants.URL_ITEMINPUT_COMMENT + queryString + (short)WebEnums.RecCategoryType.ItemizedList_ReviewNotes + "&" + QueryStringConstants.REC_CATEGORY_ID + "=" + (short)WebEnums.RecCategory.ReviewNotes;
        hlAuditTrail.NavigateUrl = URLConstants.URL_AUDIT_TRAIL + queryString + (short)WebEnums.RecCategoryType.ItemizedList_ReviewNotes + "&" + QueryStringConstants.REC_CATEGORY_ID + "=" + (short)WebEnums.RecCategory.ReviewNotes;

        RegisterToggleControlEventHandler();
        ucAccountMatching.SetURL(AccountID, NetAccountID, GLDataID, Request.Url.AbsoluteUri);
    }

    private void RegisterToggleControl()
    {
        uctlGLAdjustment.RegisterToggleControl(imgGLAdjustment);
        uctlTimingDifference.RegisterToggleControl(imgTimingDifference);
        uctlSupportingDetail.RegisterToggleControl(imgSupportingDetail);
        uctlRecWriteOff.RegisterToggleControl(imgRecWriteOff);
        uctlUnexplainedVariance.RegisterToggleControl(imgUnexplainedVariance);
        ucEditQualityScore.RegisterToggleControl(imgQualityScore);
        ucRecFormAccountTaskGrid.RegisterToggleControl(imgAccountTask);
        ucRecControlCheckList.RegisterToggleControl(imgRecControlCheckList);
    }

    private void RegisterToggleControlEventHandler()
    {
        imgGLAdjustment.Click += new ImageClickEventHandler(imgGLAdjustment_Click);
        imgTimingDifference.Click += new ImageClickEventHandler(imgTimingDifference_Click);
        imgSupportingDetail.Click += new ImageClickEventHandler(imgSupportingDetail_Click);
        imgRecWriteOff.Click += new ImageClickEventHandler(imgRecWriteOff_Click);
        imgUnexplainedVariance.Click += new ImageClickEventHandler(imgUnexplainedVariance_Click);
        imgQualityScore.Click += new ImageClickEventHandler(imgQualityScore_Click);
        imgAccountTask.Click += new ImageClickEventHandler(imgAccountTask_Click);
        imgRecControlCheckList.Click += new ImageClickEventHandler(imgRecControlCheckList_Click);
    }

    private void SetButtonsControlProperties()
    {
        this.ucRecFormButtons.GLDataHdrInfo = this.GLDataHdrInfo;
        this.ucRecFormButtons.CurrentUserRole = SessionHelper.CurrentRoleID.Value;
        this.ucRecFormButtons.ReconciliationStatusID = GLRecStatusID;
        this.ucRecFormButtons.eARTPages = WebEnums.ARTPages.TemplateItemized;
    }

    private void SetDocumentUploadURL()
    {
        string windowName;
        if (GLDataID == null || GLDataID == 0 ||
          GLRecStatusID == null || GLRecStatusID == 0)
            this.hlDocument.NavigateUrl = ScriptHelper.GetJSForEmptyURL();
        else
        {

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
        lblSystemScore.Text = Helper.GetDisplayIntegerValue(0);
        lblUserScore.Text = Helper.GetDisplayIntegerValue(0);
        //lblRecControlTotalValue.Text = Helper.GetDisplayIntegerValue(0);
        //lblRecControlCompletedValue.Text = Helper.GetDisplayIntegerValue(0);

        lblReconciledBalanceBC.Text = Helper.GetDisplayDecimalValue(null);
        lblReconciledBalanceRC.Text = Helper.GetDisplayDecimalValue(0);
        lblTotalRecWriteOffBC.Text = Helper.GetDisplayDecimalValue(defValue);
        lblTotalRecWriteOffRC.Text = Helper.GetDisplayDecimalValue(0);
        lblTotalUnExplainedVarianceBC.Text = Helper.GetDisplayDecimalValue(null);
        lblTotalUnExplainedVarianceRC.Text = Helper.GetDisplayDecimalValue(0);
        lblCountAttachedDocument.Text = string.Format(WebConstants.FORMAT_BRACKET, "0");
        lblCountReviewNotes.Text = string.Format(WebConstants.FORMAT_BRACKET, "0");

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

    }
    private void SetAttributesForSignOffButton()
    {
        decimal UnExplainedVarianceBC = 0.0M;
        decimal UnExplainedVarianceRC = 0.0M;
        RemoveAttributeForSignOff();

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
                        AddAttributeForSignOff();
                    }
                    else if (oGLDataUnexplainedVarianceInfoCollection.Count > 0)
                    {
                        if (Math.Abs(Math.Round(oGLDataUnexplainedVarianceInfoCollection[0].AmountBaseCurrency ?? 0, 2)) != Math.Abs(UnExplainedVarianceBC)
                           || Math.Abs(Math.Round(oGLDataUnexplainedVarianceInfoCollection[0].AmountReportingCurrency ?? 0, 2)) != Math.Abs(UnExplainedVarianceRC))
                        {
                            AddAttributeForSignOff();
                        }

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
                        case WebEnums.RecCategoryType.ItemizedList_GLAdjustments_Total:
                            lblTotalGLAdjustmentsBC.Text = Helper.GetDisplayBaseCurrencyValue(this.CurrentBCCY, oGLReconciliationItemInputInfo.AmountBaseCurrency);
                            lblTotalGLAdjustmentsRC.Text = Helper.GetDisplayDecimalValue(oGLReconciliationItemInputInfo.AmountReportingCurrency);
                            break;
                        case WebEnums.RecCategoryType.ItemizedList_TimingDifference_Total:
                            lblTotalTimingDifferenceBC.Text = Helper.GetDisplayBaseCurrencyValue(this.CurrentBCCY, oGLReconciliationItemInputInfo.AmountBaseCurrency);
                            lblTotalTimingDifferenceRC.Text = Helper.GetDisplayDecimalValue(oGLReconciliationItemInputInfo.AmountReportingCurrency);
                            break;
                        case WebEnums.RecCategoryType.ItemizedList_SupportingDetail_SupportingDetailList:
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
                lblGLBalanceBC.Text = Helper.GetDisplayCurrencyValue(oGLDataHdrInfoCollection[0].BaseCurrencyCode, oGLDataHdrInfoCollection[0].GLBalanceBaseCurrency);
                lblGLBalanceRC.Text = Helper.GetDisplayReportingCurrencyValue(oGLDataHdrInfoCollection[0].GLBalanceReportingCurrency);

                lblReconciledBalanceBC.Text = Helper.GetDisplayDecimalValue(oGLDataHdrInfoCollection[0].ReconciliationBalanceBaseCurrency);
                if (oGLDataHdrInfoCollection[0].ReconciliationBalanceReportingCurrency.HasValue)
                    lblReconciledBalanceRC.Text = Helper.GetDisplayDecimalValue(oGLDataHdrInfoCollection[0].ReconciliationBalanceReportingCurrency);
                if (oGLDataHdrInfoCollection[0].WriteOnOffAmountBaseCurrency.HasValue)
                    lblTotalRecWriteOffBC.Text = Helper.GetDisplayDecimalValue(oGLDataHdrInfoCollection[0].WriteOnOffAmountBaseCurrency);
                if (oGLDataHdrInfoCollection[0].WriteOnOffAmountReportingCurrency.HasValue)
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
                if (oGLDataHdrInfoCollection[0].UnexplainedVarianceReportingCurrency.HasValue)
                    lblTotalUnExplainedVarianceRC.Text = Helper.GetDisplayDecimalValue(oGLDataHdrInfoCollection[0].UnexplainedVarianceReportingCurrency);

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

                if (!IsPostBack && oGLDataHdrInfoCollection != null && oGLDataHdrInfoCollection.Count > 0)
                {
                    ucRecFormAccountTaskGrid.GLDataHdrInfo = this.GLDataHdrInfo;
                    LoadAccountTask();
                }
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




    public override void RefreshPage(object sender, RefreshEventArgs args)
    {
        base.RefreshPage(sender, args);
        //Reload the page(refresh)
        ompage_ReconciliationPeriodChangedEventHandler(null, null);
        uctlGLAdjustment.ApplyFilter();
        uctlTimingDifference.ApplyFilter();
        uctlSupportingDetail.ApplyFilter();
        uctlRecWriteOff.ApplyFilter();
        MasterPageBase oMasterPageBase = (MasterPageBase)this.Master.Master;
        // Reload the Rec Periods and also the Status / Countdown
        oMasterPageBase.ReloadRecPeriods(false);
    }
    public override string GetMenuKey()
    {
        return Helper.GetMenuKeyForRecForms(_ARTPages);
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
            this.ucEditQualityScore.IsRefreshData = true;
            this.ucEditQualityScore.GLDataHdrInfo = this.GLDataHdrInfo;
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
        if (this.GLDataHdrInfo != null && trAccountTask.Visible)
        {
            ucRecFormAccountTaskGrid.GLDataHdrInfo = this.GLDataHdrInfo;
            ucRecFormAccountTaskGrid.IsRefreshData = true;
            ucRecFormAccountTaskGrid.RegisterClientScripts();
            this.ucRecFormAccountTaskGrid.LoadData();
            ucRecFormAccountTaskGrid.Visible = true;
        }
        else
        {
            ucRecFormAccountTaskGrid.Visible = false;
        }

        RecHelper.SetAccountTaskCount(lblPendingTaskStatus, ucRecFormAccountTaskGrid.TaskCountPending, lblCompletedTaskStatus, ucRecFormAccountTaskGrid.TaskCountCompleted);

        //lblPendingTaskStatus.Text = Helper.GetDisplayIntegerValueWithBracket(ucRecFormAccountTaskGrid.TaskCountPending);
        //lblCompletedTaskStatus.Text = Helper.GetDisplayIntegerValueWithBracket(ucRecFormAccountTaskGrid.TaskCountCompleted);
    }

    void LoadUnexplainedVariance()
    {
        if (!this.uctlUnexplainedVariance.Visible)
        {
            //this.uctlUnexplainedVariance.AccountID = this.AccountID;
            //this.uctlUnexplainedVariance.NetAccountID = NetAccountID;
            //this.uctlUnexplainedVariance.GLDataID = GLDataID;
            //if (this.IsSRA.HasValue && this.IsSRA.Value)
            //    this.uctlUnexplainedVariance.IsSRA = true;
            //else
            //    this.uctlUnexplainedVariance.IsSRA = false;
            this.uctlUnexplainedVariance.IsRefreshData = true;
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
        this.LoadRecItemsItemWriteOff();
        uctlRecWriteOff.ContentVisibility = true;
    }

    void LoadRecItemsItemWriteOff()
    {
        if (!this.uctlRecWriteOff.Visible)
        {
            uctlRecWriteOff.IsRefreshData = true;
            uctlRecWriteOff.EditMode = this.EditMode;
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
    void LoadRecItemsGLAdjustment(UserControls_GLAdjustments ucGLAdjustments, WebEnums.RecCategory? enumRecCategory, WebEnums.RecCategoryType enumRecCategoryType)
    {
        ucGLAdjustments.EditMode = this.EditMode;
        ucGLAdjustments.IsRefreshData = true;
        ucGLAdjustments.GLDataHdrInfo = this.GLDataHdrInfo;
        ucGLAdjustments.RecCategoryTypeID = (short)enumRecCategoryType;
        if (ucGLAdjustments.RecCategoryTypeID != null)
        {
            ucGLAdjustments.RecCategoryID = (short)enumRecCategory;
        }
        ucGLAdjustments.LoadData();
    }
    #endregion

}
