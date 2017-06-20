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
using SkyStem.ART.Shared.Utility;

public partial class Pages_TemplateSubledgerForm : PageBaseRecForm
{
    #region Variables & Constants
    const ARTEnums.ReconciliationItemTemplate eReconciliationItemTemplateThisPage = ARTEnums.ReconciliationItemTemplate.Subledgerform;
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
            oRecPeriodMasterPageBase.PageTitleLabeID = 1100;

            MasterPageBase ompage = (MasterPageBase)this.Master.Master;
            ompage.ReconciliationPeriodChangedEventHandler += new EventHandler(ompage_ReconciliationPeriodChangedEventHandler);
            //ompage.ReconciliationPeriodChangedEventHandler += new EventHandler(uctlGLAdjustments.ompage_ReconciliationPeriodChangedEventHandler);
            //ompage.ReconciliationPeriodChangedEventHandler += new EventHandler(uctlTimmingDifference.ompage_ReconciliationPeriodChangedEventHandler);
            //ompage.ReconciliationPeriodChangedEventHandler += new EventHandler(uctlItemInputWriteOff.ompage_ReconciliationPeriodChangedEventHandler);
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
            Helper.SetBreadcrumbsForRecForms(this, _ARTPages, 1100);
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

    void imgSupportingDetailsOther_Click(object sender, ImageClickEventArgs e)
    {
        uctlSupportingDetailOther.IsExpanded = true;
        this.LoadRecItemsGLAdjustment(uctlSupportingDetailOther, WebEnums.RecCategory.SupportingDetail, WebEnums.RecCategoryType.Subledger_SupportingDetail_OtherDetails);
        uctlSupportingDetailOther.ContentVisibility = true;
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
        // this.uctlUnexplainedVariance.ContentWidth = Convert.ToInt32(100);

    }

    void imgRecWriteOff_Click(object sender, ImageClickEventArgs e)
    {
        uctlItemInputWriteOff.IsExpanded = true;
        this.LoadRecItemsItemWriteOff();
        uctlItemInputWriteOff.ContentVisibility = true;
    }


    void imgViewTimingDifference_Click(object sender, ImageClickEventArgs e)
    {
        uctlTimmingDifference.IsExpanded = true;
        this.LoadRecItemsGLAdjustment(uctlTimmingDifference, WebEnums.RecCategory.TimingDifference, WebEnums.RecCategoryType.Subledger_TimingDifference_Total);
        uctlTimmingDifference.ContentVisibility = true;
    }

    void imgViewGLAdjustment_Click(object sender, ImageClickEventArgs e)
    {
        uctlGLAdjustments.IsExpanded = true;
        this.LoadRecItemsGLAdjustment(uctlGLAdjustments, WebEnums.RecCategory.GLAdjustments, WebEnums.RecCategoryType.Subledger_GLAdjustments_Total);
        uctlGLAdjustments.ContentVisibility = true;
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
                if (this.GLDataHdrInfo != null)
                    PreviousNetAccountId = this.GLDataHdrInfo.NetAccountID;
                OnPageLoad();
                Helper.ValidateRecTemplateForAccountAndNetAccount(this, this.GLDataHdrInfo, PreviousNetAccountId);
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
    #endregion

    #region Validation Control Events
    #endregion

    #region Private Methods
    private void ShowSubledgerLoad()
    {

        SubledgerDataInfo oSubledgerDataInfo;
        ISubledger oSubledgerClient = RemotingHelper.GetSubledgerObject();
        ShowHideFileDownloadIcon(0, null);
        if (NetAccountID > 0)
        {
            oSubledgerDataInfo = oSubledgerClient.GetSubledgerDataImportIDByNetAccountIDRecPeriodID(NetAccountID, SessionHelper.CurrentReconciliationPeriodID, Helper.GetAppUserInfo());
            if (oSubledgerDataInfo != null && oSubledgerDataInfo.DataImportID.HasValue)
                ShowHideFileDownloadIcon(oSubledgerDataInfo.DataImportID.Value, oSubledgerDataInfo.DataImportTypeID);
        }
        else
        {
            oSubledgerDataInfo = oSubledgerClient.GetSubledgerDataInfoByAccountIDRecPeriodID(AccountID, SessionHelper.CurrentReconciliationPeriodID, Helper.GetAppUserInfo());
            if (oSubledgerDataInfo != null)
            {
                if (oSubledgerDataInfo.DataImportID.HasValue)
                    ShowHideFileDownloadIcon(oSubledgerDataInfo.DataImportID.Value, oSubledgerDataInfo.DataImportTypeID);
            }
        }
    }

    private void ShowHideFileDownloadIcon(int? DataImportID, short? DataImportTypeID)
    {
        if (DataImportID > 0)
        {
            //DataImportHdrInfo oDataImportHdr = null;
            //IDataImport oDataImportClient = RemotingHelper.GetDataImportObject();
            //oDataImportHdr = oDataImportClient.GetDataImportInfo(DataImportID, Helper.GetAppUserInfo());
            //if (oDataImportHdr != null)
            //{
            //string url = "DownloadAttachment.aspx?" + QueryStringConstants.FILE_PATH + "=" + Server.UrlEncode(SharedHelper.GetDisplayFilePath(PhysicalPath));
            //imgFileDownload.OnClientClick = "document.location.href = '" + url + "';return false;";
            string url = string.Format("Downloader?{0}={1}&", QueryStringConstants.HANDLER_ACTION, (Int32)WebEnums.HandlerActionType.DownloadDataImportFile);
            url += "&" + QueryStringConstants.DATA_IMPORT_ID + "=" + DataImportID.GetValueOrDefault()
            + "&" + QueryStringConstants.DATA_IMPORT_TYPE_ID + "=" + DataImportTypeID.GetValueOrDefault()
            + "&" + QueryStringConstants.GLDATA_ID + "=" + this.GLDataID.GetValueOrDefault();
            imgFileDownload.Attributes.Add("onclick", "javascript:{$get('" + ifDownloader.ClientID + "').src='" + url + "'; return false;}");
            imgFileDownload.ToolTip = LanguageUtil.GetValue(2051);
            imgFileDownload.Visible = true;
            //}
        }
        else
        {
            imgFileDownload.Visible = false;
        }
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
        GLDataHdrInfo oGLDataHdrInfo = Helper.GetGLDataHdrInfoToSaveOnTemplateForm(commandName, IsSRA, ucEditQualityScore.IsExpanded, ucRecControlCheckList.IsExpanded, out eNewReconciliationStatus, out isSignOff, out isFormDataToBeSaved);
        oGLDataHdrInfo.GLDataID = GLDataID;
        short? reconciliationCategoryTypeIDForSupportingDetail = null;
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
        RecHelper.ReloadUserControls(this.GLDataHdrInfo, uctlGLAdjustments, uctlTimmingDifference, uctlSupportingDetailOther, uctlItemInputWriteOff, uctlUnexplainedVariance, ucEditQualityScore, ucRecControlCheckList, ucRecFormAccountTaskGrid);
    }


    private void SetNewGLDataID()
    {
        // Since the GL Data ID would have changed, reset on all controls to refresh them if already expanded
        uctlGLAdjustments.GLDataHdrInfo = this.GLDataHdrInfo;
        uctlTimmingDifference.GLDataHdrInfo = this.GLDataHdrInfo;
        uctlSupportingDetailOther.GLDataHdrInfo = this.GLDataHdrInfo;
        uctlItemInputWriteOff.GLDataHdrInfo = this.GLDataHdrInfo;
        uctlUnexplainedVariance.GLDataHdrInfo = this.GLDataHdrInfo;
        ucEditQualityScore.GLDataHdrInfo = this.GLDataHdrInfo;
        ucRecFormAccountTaskGrid.GLDataHdrInfo = this.GLDataHdrInfo;
        ucRecControlCheckList.GLDataHdrInfo = this.GLDataHdrInfo;
    }
    private void SetEditMode()
    {
        uctlGLAdjustments.EditMode = this.EditMode;
        uctlTimmingDifference.EditMode = this.EditMode;
        uctlItemInputWriteOff.EditMode = this.EditMode;
    }

    private void OnPageLoad()
    {
        RegisterToggleControl();
        ManagePackageFeatures();

        Helper.SetPageTitle(this, 1100);
        Helper.HideMessage(this);

        if (!string.IsNullOrEmpty(Request.QueryString[QueryStringConstants.GLDATA_ID]))
        {
            long? _gLDataID = Convert.ToInt64(Request.QueryString[QueryStringConstants.GLDATA_ID]);
            this.GLDataHdrInfo = Helper.GetGLDataHdrInfo(_gLDataID);
        }

        string pageID = Request.QueryString[QueryStringConstants.REFERRER_PAGE_ID];
        _ARTPages = (WebEnums.ARTPages)System.Enum.Parse(typeof(WebEnums.ARTPages), pageID);

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

            ShowSubledgerLoad();
            // Display Non-Editbale Message
            Helper.ShowNonEditableMessage(this, GLDataHdrInfo);
            this.ucRecControlCheckList.GLDataHdrInfo = this.GLDataHdrInfo;
            this.ucRecControlCheckList.EditMode = Helper.GetFormModeForRecControlCheckList(this.GLDataHdrInfo);

        }
        EditMode = Helper.GetFormMode(WebEnums.ARTPages.TemplateSubledger, this.GLDataHdrInfo);
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
        RecHelper.ShowHideReviewNotesAndQualityScore(trReviewNotes, trQualityScore, trRecControlCheckList);
        ucRecFormAccountTaskGrid.RegisterClientScripts();

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

        uctlSupportingDetailOther.EntityNameLabelID = lblSupportingDetailOther.LabelID;
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

        hlImportGLAdjustment.NavigateUrl = URLConstants.URL_IMPORT_BANKFEE + queryString + (short)WebEnums.RecCategoryType.Subledger_GLAdjustments_Total + "&" + QueryStringConstants.REC_CATEGORY_ID + "=" + (short)WebEnums.RecCategory.GLAdjustments;
        hlImportTimingDifference.NavigateUrl = URLConstants.URL_IMPORT_BANKFEE + queryString + (short)WebEnums.RecCategoryType.Subledger_TimingDifference_Total + "&" + QueryStringConstants.REC_CATEGORY_ID + "=" + (short)WebEnums.RecCategory.TimingDifference;
        hlImportSupportingDetailOther.NavigateUrl = URLConstants.URL_IMPORT_BANKFEE + queryString + (short)WebEnums.RecCategoryType.Subledger_SupportingDetail_OtherDetails + "&" + QueryStringConstants.REC_CATEGORY_ID + "=" + (short)WebEnums.RecCategory.SupportingDetail;

        string queryStringForRCCLImport = "?" + QueryStringConstants.ACCOUNT_ID + "=" + AccountID
      + "&" + QueryStringConstants.NETACCOUNT_ID + "=" + NetAccountID
       + "&" + QueryStringConstants.GLDATA_ID + "=" + GLDataID;
        hlImportRecControlCheckList.NavigateUrl = URLConstants.URL_IMPORT_RECCONTROLCHECKLIST + queryString;

        hlUnexplainedVarianceHistory.NavigateUrl = URLConstants.URL_ITEMINPUT_UNEXPLAINEDVARIANCE_HISTORY + queryString + (short)WebEnums.RecCategoryType.Subledger_RecWriteoffOn_UnexpVar + "&" + QueryStringConstants.REC_CATEGORY_ID + "=" + (short)WebEnums.RecCategory.VariancesWriteOffOn;

        hlReviewNotes.NavigateUrl = URLConstants.URL_ITEMINPUT_COMMENT + queryString + (short)WebEnums.RecCategoryType.Subledger_ReviewNotes + "&" + QueryStringConstants.REC_CATEGORY_ID + "=" + (short)WebEnums.RecCategory.ReviewNotes;
        hlAuditTrail.NavigateUrl = URLConstants.URL_AUDIT_TRAIL + queryString + (short)WebEnums.RecCategoryType.Subledger_ReviewNotes + "&" + QueryStringConstants.REC_CATEGORY_ID + "=" + (short)WebEnums.RecCategory.ReviewNotes;

        RegisterToggleControlEventHandler();
        ucAccountMatching.SetURL(AccountID, NetAccountID, GLDataID, Request.Url.AbsoluteUri);
    }

    private void RegisterToggleControl()
    {
        uctlGLAdjustments.RegisterToggleControl(imgViewGLAdjustment);
        uctlTimmingDifference.RegisterToggleControl(imgViewTimingDifference);
        uctlSupportingDetailOther.RegisterToggleControl(imgSupportingDetailsOther);
        uctlItemInputWriteOff.RegisterToggleControl(imgRecWriteOff);
        uctlUnexplainedVariance.RegisterToggleControl(imgUnexplainedVariance);
        ucEditQualityScore.RegisterToggleControl(imgQualityScore);
        ucRecFormAccountTaskGrid.RegisterToggleControl(imgAccountTask);
        ucRecControlCheckList.RegisterToggleControl(imgRecControlCheckList);
    }

    private void RegisterToggleControlEventHandler()
    {
        imgViewGLAdjustment.Click += new ImageClickEventHandler(imgViewGLAdjustment_Click);
        imgViewTimingDifference.Click += new ImageClickEventHandler(imgViewTimingDifference_Click);
        imgSupportingDetailsOther.Click += new ImageClickEventHandler(imgSupportingDetailsOther_Click);
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
        this.ucRecFormButtons.eARTPages = WebEnums.ARTPages.TemplateSubledger;
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
        lblTotalTimingDifferenceBC.Text = Helper.GetDisplayDecimalValue(defValue);
        lblTotalSupportingDetailBC.Text = Helper.GetDisplayDecimalValue(defValue);

        lblTotalGLAdjustmentsRC.Text = Helper.GetDisplayDecimalValue(0);
        lblTotalTimingDifferenceRC.Text = Helper.GetDisplayDecimalValue(0);
        lblTotalSupportingDetailRC.Text = Helper.GetDisplayDecimalValue(0);
        lblSystemScore.Text = Helper.GetDisplayIntegerValue(0);
        lblUserScore.Text = Helper.GetDisplayIntegerValue(0);
        //lblRecControlTotalValue.Text = Helper.GetDisplayIntegerValue(0);
        //lblRecControlCompletedValue.Text = Helper.GetDisplayIntegerValue(0);
        //hdReviewCount.Value = Helper.GetDisplayIntegerValue(0);

        lblSupportingDetailSubledgerBC.Text = Helper.GetDisplayDecimalValue(defValue);
        lblSupportingDetailSubledgerRC.Text = Helper.GetDisplayDecimalValue(defValue);
        lblSupportingDetailOtherBC.Text = Helper.GetDisplayDecimalValue(defValue);
        lblSupportingDetailOtherRC.Text = Helper.GetDisplayDecimalValue(defValue);

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
                        case WebEnums.RecCategoryType.Subledger_GLAdjustments_Total:
                            lblTotalGLAdjustmentsBC.Text = Helper.GetDisplayBaseCurrencyValue(this.CurrentBCCY, oGLReconciliationItemInputInfo.AmountBaseCurrency);
                            lblTotalGLAdjustmentsRC.Text = Helper.GetDisplayDecimalValue(oGLReconciliationItemInputInfo.AmountReportingCurrency);
                            break;
                        case WebEnums.RecCategoryType.Subledger_TimingDifference_Total:
                            lblTotalTimingDifferenceBC.Text = Helper.GetDisplayBaseCurrencyValue(this.CurrentBCCY, oGLReconciliationItemInputInfo.AmountBaseCurrency);
                            lblTotalTimingDifferenceRC.Text = Helper.GetDisplayDecimalValue(oGLReconciliationItemInputInfo.AmountReportingCurrency);
                            break;
                        case WebEnums.RecCategoryType.Subledger_SupportingDetail_OtherDetails:
                            lblSupportingDetailOtherBC.Text = Helper.GetDisplayBaseCurrencyValue(this.CurrentBCCY, oGLReconciliationItemInputInfo.AmountBaseCurrency);
                            lblSupportingDetailOtherRC.Text = Helper.GetDisplayDecimalValue(oGLReconciliationItemInputInfo.AmountReportingCurrency);
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

                lblSupportingDetailSubledgerBC.Text = Helper.GetDisplayBaseCurrencyValue(oGLDataHdrInfoCollection[0].BaseCurrencyCode, oGLDataHdrInfoCollection[0].SubledgerBalanceBCCY);
                lblSupportingDetailSubledgerRC.Text = Helper.GetDisplayDecimalValue(oGLDataHdrInfoCollection[0].SubledgerBalanceRCCY);
                if (oGLDataHdrInfoCollection[0].WriteOnOffAmountBaseCurrency.HasValue)
                    lblTotalRecWriteOffBC.Text = Helper.GetDisplayDecimalValue(oGLDataHdrInfoCollection[0].WriteOnOffAmountBaseCurrency);
                if (oGLDataHdrInfoCollection[0].WriteOnOffAmountReportingCurrency.HasValue)
                    lblTotalRecWriteOffRC.Text = Helper.GetDisplayDecimalValue(oGLDataHdrInfoCollection[0].WriteOnOffAmountReportingCurrency);

                lblTotalSupportingDetailBC.Text = Helper.GetDisplayDecimalValue(oGLDataHdrInfoCollection[0].SupportingDetailBalanceBaseCurrency);
                if (oGLDataHdrInfoCollection[0].SupportingDetailBalanceReportingCurrency.HasValue)
                    lblTotalSupportingDetailRC.Text = Helper.GetDisplayDecimalValue(oGLDataHdrInfoCollection[0].SupportingDetailBalanceReportingCurrency);

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

                //Added By Prafull for Multiversion Subledger 
                ucSubledgerVersion.IsVersionAvailable = (bool)oGLDataHdrInfoCollection[0].IsSubledgerVersionAvailable;
                ucSubledgerVersion.GLDataID = GLDataID;
                if (ucSubledgerVersion.IsVersionAvailable == true)
                {
                    ExHyperLink hlSubledgerVersion = (ExHyperLink)ucSubledgerVersion.FindControl("hlSubledgerVersion");
                    if (hlSubledgerVersion != null)
                    {
                        hlSubledgerVersion.ToolTip = LanguageUtil.GetValue(2365);
                    }
                }
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
    #endregion

    #region Other Methods
    public override void RefreshPage(object sender, RefreshEventArgs args)
    {
        base.RefreshPage(sender, args);
        //Reload the page(refresh)
        ompage_ReconciliationPeriodChangedEventHandler(null, null);
        uctlGLAdjustments.ApplyFilter();
        uctlTimmingDifference.ApplyFilter();
        uctlSupportingDetailOther.ApplyFilter();
        uctlItemInputWriteOff.ApplyFilter();
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
    void LoadRecItemsGLAdjustment(UserControls_GLAdjustments ucGLAdjustments, WebEnums.RecCategory? enumRecCategory, WebEnums.RecCategoryType enumRecCategoryType)
    {
        ucGLAdjustments.IsRefreshData = true;
        ucGLAdjustments.EditMode = this.EditMode;
        ucGLAdjustments.GLDataHdrInfo = this.GLDataHdrInfo;
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
            uctlItemInputWriteOff.IsRefreshData = true;
            uctlItemInputWriteOff.EditMode = this.EditMode;
            this.uctlItemInputWriteOff.GLDataHdrInfo = this.GLDataHdrInfo;
            this.uctlItemInputWriteOff.RecCategoryTypeID = (short)WebEnums.RecCategoryType.Subledger_RecWriteoffOn_WriteoffOn;
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

        //lblPendingTaskStatus.Text = Helper.GetDisplayIntegerValueWithBracket(ucRecFormAccountTaskGrid.TaskCountPending);
        //lblCompletedTaskStatus.Text = Helper.GetDisplayIntegerValueWithBracket(ucRecFormAccountTaskGrid.TaskCountCompleted);
    }


    void LoadUnexplainedVariance()
    {
        if (!this.uctlUnexplainedVariance.Visible)
        {
            this.uctlUnexplainedVariance.IsRefreshData = true;
            this.uctlUnexplainedVariance.GLDataHdrInfo = this.GLDataHdrInfo;
            this.uctlUnexplainedVariance.RecCategoryTypeID = (short)WebEnums.RecCategoryType.Subledger_RecWriteoffOn_UnexpVar;
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

}//end of class