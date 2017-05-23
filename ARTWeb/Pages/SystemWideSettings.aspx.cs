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
using SkyStem.ART.Client.IServices;
using SkyStem.ART.Client.Model;
using SkyStem.ART.Web.Utility;
using System.Collections.Generic;
using SkyStem.Library.Controls.WebControls;
using Telerik.Web.UI;
using SkyStem.ART.Web.Data;
using System.Text;
using SkyStem.Language.LanguageUtility;
using SkyStem.ART.Client.Exception;
using SkyStem.ART.Client.Data;
//TODO: usp_Test_SEL_IncompleteRequirementToMarkOpen
//TODO: Error message of custom validation (server side for dates on holiday and if capability and dataimport for markCheckBox ) are not coming by .ShowErrorMessage() yet. only one error at a time shows.
//certfication due date--> lockdowndate or close date when allow lockdown checked.( close date will always be avaible)
//usp_SEL_IncompleteRequirementToMarkOpen
public partial class Pages_SystemWideSettings : PageBaseRecPeriod
{

    #region Variables & Constants
    int? _companyID = 0;
    int? _currentReconciliationPeriodIDInDB = null;
    DateTime? _currentRecPeriodEndDateInDB = null;
    bool? _isCertificationActivated = false;
    static readonly string cvHoliday_RecPeriodID = "RecPeriodID";
    const int POPUP_WIDTH = 630;
    const int POPUP_HEIGHT = 480;
    #endregion

    #region Properties
    List<HolidayCalendarInfo> oHolidayCalendarInfoCollection = null;
    IList<WeekDayMstInfo> oWeekDayMstInfoCollection = null;
    IList<CompanyWeekDayInfo> oCompanyWorkWeekInfoCollectionForFincncialyear = null;
    public bool IsRefreshData
    {
        get
        {
            if (hdIsRefreshData.Value == "1")
                return true;
            else
                return false;
        }
        set
        {
            if (value == true)
                hdIsRefreshData.Value = "1";
            else
                hdIsRefreshData.Value = "0";
        }
    }
    #endregion

    #region Delegates & Events
    #endregion

    #region Page Events
    protected void Page_Init(object sender, EventArgs e)
    {
        try
        {
            getDocumentMaximumSizeLimit();
            this.SetErrorMessages();
            Helper.ShowInputRequirementSection(this, 5000118, 5000237, 2766);
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
            Helper.SetPageTitle(this, 1225);
            Helper.HideMessage(this);
            trErrorAndWarnings.Visible = false;
            if (!IsPostBack)
            {
                this.Page.SetFocus(this.ddlCurrentRecPeriod);

                /* 1. Bind FY
                 * 2. Bind Rec Periods based on FY
                 * 3. Bind Rec Period Grid based on FY
                 */
                CallEveryTime();
                CallOnlyWhenFirstTime();
            }
            //  HandleRefreshForceClose();
            HandleRefreshReopen();
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
    protected void rgSystemWideSettings_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {
        WebEnums.FeatureCapabilityMode eFeatureCapabilityModeDualReview = WebEnums.FeatureCapabilityMode.Hidden;
        WebEnums.FeatureCapabilityMode eFeatureCapabilityModeCertification = WebEnums.FeatureCapabilityMode.Hidden;
        WebEnums.FeatureCapabilityMode eFeatureCapabilityModeDueDateByAccount = WebEnums.FeatureCapabilityMode.Hidden;

        if (e.Item.ItemType == Telerik.Web.UI.GridItemType.Item ||
           e.Item.ItemType == Telerik.Web.UI.GridItemType.AlternatingItem)
        {
            //if ((e.Item is GridEditFormItem) && (e.Item.IsInEditMode))
            ExLabel lblRecPeriod = e.Item.FindControl("lblRecPeriod") as ExLabel;
            ExCalendar calPrepareDueDate = e.Item.FindControl("calPrepareDueDate") as ExCalendar;
            ExCalendar calReviewerDueDate = e.Item.FindControl("calReviewerDueDate") as ExCalendar;
            ExCalendar calApproverDueDate = e.Item.FindControl("calApproverDueDate") as ExCalendar;
            ExCalendar calCloseOrLockDownDate = e.Item.FindControl("calCloseOrLockDownDate") as ExCalendar;
            ExCalendar calCertificationStartDate = e.Item.FindControl("calCertificationStartDate") as ExCalendar;
            ExCheckBox cbAllowCertificationLockDown = e.Item.FindControl("cbAllowCertificationLockDown") as ExCheckBox;

            ExCustomValidator cvPrepareDueDateHoliday = e.Item.FindControl("cvPrepareDueDateHoliday") as ExCustomValidator;
            ExCustomValidator cvReviewerDueDateHoliday = e.Item.FindControl("cvReviewerDueDateHoliday") as ExCustomValidator;
            ExCustomValidator cvApproverDueDateHoliday = e.Item.FindControl("cvApproverDueDateHoliday") as ExCustomValidator;
            ExCustomValidator cvCertificationStartDateHoliday = e.Item.FindControl("cvCertificationStartDateHoliday") as ExCustomValidator;
            ExCustomValidator cvCloseOrLockDownDateHoliday = e.Item.FindControl("cvCloseOrLockDownDateHoliday") as ExCustomValidator;

            CustomValidator cvAllDueDateAdjacent = e.Item.FindControl("cvAllDueDateAdjacent") as CustomValidator;

            CustomValidator cvComparePrepareDueDateWithCurrentDate = e.Item.FindControl("cvComparePrepareDueDateWithCurrentDate") as CustomValidator;
            CustomValidator cvCompareReviewerDueDateWithCurrentDate = e.Item.FindControl("cvCompareReviewerDueDateWithCurrentDate") as CustomValidator;
            CustomValidator cvCompareApproverDueDateWithCurrentDate = e.Item.FindControl("cvCompareApproverDueDateWithCurrentDate") as CustomValidator;
            CustomValidator cvCompareCertificationStartDateWithCurrentDate = e.Item.FindControl("cvCompareCertificationStartDateWithCurrentDate") as CustomValidator;
            CustomValidator cvCompareCloseOrLockDownDateWithCurrentDate = e.Item.FindControl("cvCompareCloseOrLockDownDateWithCurrentDate") as CustomValidator;
            CustomValidator cvcalCloseOrLockDownDate = e.Item.FindControl("cvcalCloseOrLockDownDate") as CustomValidator;



            RequiredFieldValidator rfvPrepareDueDate = e.Item.FindControl("rfvPrepareDueDate") as RequiredFieldValidator;
            RequiredFieldValidator rfvReviewerDueDate = e.Item.FindControl("rfvReviewerDueDate") as RequiredFieldValidator;
            RequiredFieldValidator rfvApproverDueDate = e.Item.FindControl("rfvApproverDueDate") as RequiredFieldValidator;
            RequiredFieldValidator rfvCertificationStartDate = e.Item.FindControl("rfvCertificationStartDate") as RequiredFieldValidator;
            RequiredFieldValidator rfvCloseOrLockDownDate = e.Item.FindControl("rfvCloseOrLockDownDate") as RequiredFieldValidator;

            ExLabel lblRecPeriodStatus = e.Item.FindControl("lblRecPeriodStatus") as ExLabel;
            ExHyperLink hlRecPeriodHistory = e.Item.FindControl("hlRecPeriodHistory") as ExHyperLink;

            ReconciliationPeriodInfo oReconciliationPeriodInfo = new ReconciliationPeriodInfo();
            oReconciliationPeriodInfo = (ReconciliationPeriodInfo)e.Item.DataItem;



            if (hlRecPeriodHistory != null)
            {
                string windowName;
                hlRecPeriodHistory.NavigateUrl = "javascript:OpenRadWindowForHyperlink('" + Helper.SetURLForRecPeriodHistory(oReconciliationPeriodInfo.ReconciliationPeriodID, out windowName) + "', " + POPUP_HEIGHT + " , " + POPUP_WIDTH + ");";
            }

            if (oReconciliationPeriodInfo.ReconciliationPeriodID.HasValue)
            {
                cvPrepareDueDateHoliday.Attributes.Add(cvHoliday_RecPeriodID, oReconciliationPeriodInfo.ReconciliationPeriodID.ToString());
                cvReviewerDueDateHoliday.Attributes.Add(cvHoliday_RecPeriodID, oReconciliationPeriodInfo.ReconciliationPeriodID.ToString());
                cvApproverDueDateHoliday.Attributes.Add(cvHoliday_RecPeriodID, oReconciliationPeriodInfo.ReconciliationPeriodID.ToString());
                cvCertificationStartDateHoliday.Attributes.Add(cvHoliday_RecPeriodID, oReconciliationPeriodInfo.ReconciliationPeriodID.ToString());
                cvCloseOrLockDownDateHoliday.Attributes.Add(cvHoliday_RecPeriodID, oReconciliationPeriodInfo.ReconciliationPeriodID.ToString());
            }

            calPrepareDueDate.Text = Helper.GetDisplayDateForCalendar(oReconciliationPeriodInfo.PreparerDueDate);
            calReviewerDueDate.Text = Helper.GetDisplayDateForCalendar(oReconciliationPeriodInfo.ReviewerDueDate);

            eFeatureCapabilityModeDualReview = Helper.GetFeatureCapabilityMode(WebEnums.Feature.DualLevelReview, ARTEnums.Capability.DualLevelReview, oReconciliationPeriodInfo.ReconciliationPeriodID);
            eFeatureCapabilityModeCertification = Helper.GetFeatureCapabilityMode(WebEnums.Feature.Certification, ARTEnums.Capability.CertificationActivation, oReconciliationPeriodInfo.ReconciliationPeriodID);
            eFeatureCapabilityModeDueDateByAccount = Helper.GetFeatureCapabilityMode(WebEnums.Feature.DueDateByAccount, ARTEnums.Capability.DueDateByAccount, oReconciliationPeriodInfo.ReconciliationPeriodID);


            switch (eFeatureCapabilityModeDualReview)
            {
                case WebEnums.FeatureCapabilityMode.Visible:
                    calApproverDueDate.Text = Helper.GetDisplayDateForCalendar(oReconciliationPeriodInfo.ApproverDueDate);
                    break;

                case WebEnums.FeatureCapabilityMode.Hidden:
                    calApproverDueDate.Visible = false;
                    break;

                case WebEnums.FeatureCapabilityMode.Disable:
                    calApproverDueDate.Enabled = false;
                    break;
            }

            calCloseOrLockDownDate.Text = Helper.GetDisplayDateForCalendar(oReconciliationPeriodInfo.ReconciliationCloseDate);
            switch (eFeatureCapabilityModeCertification)
            {
                case WebEnums.FeatureCapabilityMode.Visible:
                    calCertificationStartDate.Text = Helper.GetDisplayDateForCalendar(oReconciliationPeriodInfo.CertificationStartDate);
                    if (oReconciliationPeriodInfo.AllowCertificationLockdown.HasValue && oReconciliationPeriodInfo.AllowCertificationLockdown.Value == true)
                    {
                        // Show Cert Lockdown Date
                        cbAllowCertificationLockDown.Checked = true;
                        calCloseOrLockDownDate.Text = Helper.GetDisplayDateForCalendar(oReconciliationPeriodInfo.CertificationLockDownDate);

                    }
                    else
                    {

                        cbAllowCertificationLockDown.Checked = false;
                    }
                    break;

                case WebEnums.FeatureCapabilityMode.Hidden:
                    calCertificationStartDate.Visible = false;
                    cbAllowCertificationLockDown.Visible = false;
                    break;

                case WebEnums.FeatureCapabilityMode.Disable:
                    calCertificationStartDate.Enabled = false;
                    cbAllowCertificationLockDown.Enabled = false;
                    break;
            }


            string clientIDPreparerDueDate = "";
            string clientIDReviewerDueDate = "";
            string clientIDApproverDueDate = "";
            string clientIDCertificationStartDate = "";
            string clientIDCloseOrLockDownDate = "";

            clientIDPreparerDueDate = calPrepareDueDate.ClientID;
            clientIDReviewerDueDate = calReviewerDueDate.ClientID;
            clientIDApproverDueDate = calApproverDueDate.ClientID;
            clientIDCertificationStartDate = calCertificationStartDate.ClientID;
            clientIDCloseOrLockDownDate = calCloseOrLockDownDate.ClientID;


            string controlListForAdjacentToCompare = clientIDPreparerDueDate + "," +
                      clientIDReviewerDueDate + "," +
                      clientIDApproverDueDate + "," +
                      clientIDCertificationStartDate + "," +
                      clientIDCloseOrLockDownDate;

            AddPropertyForAdjacentDateCheck(cvAllDueDateAdjacent, controlListForAdjacentToCompare
                                            , Helper.GetDisplayDate(oReconciliationPeriodInfo.PeriodEndDate), e.Item.ItemIndex);

            SetCompareValidatorProperty(clientIDPreparerDueDate, clientIDReviewerDueDate, clientIDApproverDueDate,
                clientIDCertificationStartDate, clientIDCloseOrLockDownDate, oReconciliationPeriodInfo,
                cvComparePrepareDueDateWithCurrentDate, cvCompareReviewerDueDateWithCurrentDate,
                cvCompareApproverDueDateWithCurrentDate, cvCompareCertificationStartDateWithCurrentDate,
                cvCompareCloseOrLockDownDateWithCurrentDate, eFeatureCapabilityModeDualReview, eFeatureCapabilityModeCertification, cvcalCloseOrLockDownDate, e.Item.ItemIndex);

            //New logic as per multiple open periods. Logis below has been commented
            if (!EnableDisableCalendarsBasedOnRecPeriodStatus(oReconciliationPeriodInfo.ReconciliationPeriodStatusID))
            {
                // e.Item.Enabled = false;

                calPrepareDueDate.Enabled = false;
                calReviewerDueDate.Enabled = false;
                calApproverDueDate.Enabled = false;
                calCloseOrLockDownDate.Enabled = false;
                calCertificationStartDate.Enabled = false;
                cbAllowCertificationLockDown.Enabled = false;
                hlRecPeriodHistory.Enabled = true;




                cvPrepareDueDateHoliday.Visible = false;
                cvReviewerDueDateHoliday.Visible = false;
                cvApproverDueDateHoliday.Visible = false;
                cvCertificationStartDateHoliday.Visible = false;
                cvCloseOrLockDownDateHoliday.Visible = false;

                cvAllDueDateAdjacent.Visible = false;

                cvComparePrepareDueDateWithCurrentDate.Visible = false;
                cvCompareReviewerDueDateWithCurrentDate.Visible = false;
                cvCompareApproverDueDateWithCurrentDate.Visible = false;
                cvCompareCertificationStartDateWithCurrentDate.Visible = false;
                cvCompareCloseOrLockDownDateWithCurrentDate.Visible = false;
            }

            //if (!EnableDisableCalendarsBasedOnRecPeriodStatus(oReconciliationPeriodInfo.ReconciliationPeriodID, oReconciliationPeriodInfo.PeriodEndDate.Value))
            //{
            //    e.Item.Enabled = false;

            //    cvPrepareDueDateHoliday.Visible = false;
            //    cvReviewerDueDateHoliday.Visible = false;
            //    cvApproverDueDateHoliday.Visible = false;
            //    cvCertificationStartDateHoliday.Visible = false;
            //    cvCloseOrLockDownDateHoliday.Visible = false;

            //    cvAllDueDateAdjacent.Visible = false;

            //    cvComparePrepareDueDateWithCurrentDate.Visible = false;
            //    cvCompareReviewerDueDateWithCurrentDate.Visible = false;
            //    cvCompareApproverDueDateWithCurrentDate.Visible = false;
            //    cvCompareCertificationStartDateWithCurrentDate.Visible = false;
            //    cvCompareCloseOrLockDownDateWithCurrentDate.Visible = false;


            //    //TODO: other validations too
            //}

            //Disable the controls for editing where due date is passed
            //IReconciliationPeriod oReconciliationPeriodClient = RemotingHelper.GetReconciliationPeriodObject();
            //ReconciliationPeriodStatusMstInfo oReconciliationPeriodStatusMstInfo = oReconciliationPeriodClient.GetRecPeriodStatus(oReconciliationPeriodInfo.ReconciliationPeriodID);
            //WebEnums.RecPeriodStatus eRecPeriodStatus = (WebEnums.RecPeriodStatus)oReconciliationPeriodStatusMstInfo.ReconciliationPeriodStatusID;

            WebEnums.RecPeriodStatus eRecPeriodStatus = (WebEnums.RecPeriodStatus)oReconciliationPeriodInfo.ReconciliationPeriodStatusID;
            if (eRecPeriodStatus == WebEnums.RecPeriodStatus.InProgress || eRecPeriodStatus == WebEnums.RecPeriodStatus.Open)
            {
                rfvPrepareDueDate.ErrorMessage = string.Format(LanguageUtil.GetValue(5000349), LanguageUtil.GetValue(1417));
                rfvReviewerDueDate.ErrorMessage = string.Format(LanguageUtil.GetValue(5000349), LanguageUtil.GetValue(1418));
                rfvApproverDueDate.ErrorMessage = string.Format(LanguageUtil.GetValue(5000349), LanguageUtil.GetValue(1738));
                rfvCertificationStartDate.ErrorMessage = string.Format(LanguageUtil.GetValue(5000349), LanguageUtil.GetValue(1453));
                rfvCloseOrLockDownDate.ErrorMessage = string.Format(LanguageUtil.GetValue(5000349), LanguageUtil.GetValue(1419));

                rfvPrepareDueDate.Enabled = true;
                rfvReviewerDueDate.Enabled = true;
                rfvCloseOrLockDownDate.Enabled = true;
                if (eFeatureCapabilityModeDualReview == WebEnums.FeatureCapabilityMode.Visible)
                    rfvApproverDueDate.Enabled = true;
                if (eFeatureCapabilityModeCertification == WebEnums.FeatureCapabilityMode.Visible)
                    rfvCertificationStartDate.Enabled = true;
                DisableValidationForPassedDates(oReconciliationPeriodInfo.PreparerDueDate, calPrepareDueDate, cvComparePrepareDueDateWithCurrentDate);
                DisableValidationForPassedDates(oReconciliationPeriodInfo.ReviewerDueDate, calReviewerDueDate, cvCompareReviewerDueDateWithCurrentDate);
                DisableValidationForPassedDates(oReconciliationPeriodInfo.CertificationStartDate, calCertificationStartDate, cvCompareCertificationStartDateWithCurrentDate);
                // Disable Due to  IsStopRecAndStartCert flag
                if (oReconciliationPeriodInfo.IsStopRecAndStartCert.Value)
                {
                    calPrepareDueDate.Enabled = false;
                    calReviewerDueDate.Enabled = false;
                    calCertificationStartDate.Enabled = false;
                }

                if (eFeatureCapabilityModeDualReview == WebEnums.FeatureCapabilityMode.Visible)
                {
                    DisableValidationForPassedDates(oReconciliationPeriodInfo.ApproverDueDate, calApproverDueDate, cvCompareApproverDueDateWithCurrentDate);

                    if (oReconciliationPeriodInfo.IsStopRecAndStartCert.Value)
                        calApproverDueDate.Enabled = false;
                }
                if (eFeatureCapabilityModeCertification == WebEnums.FeatureCapabilityMode.Visible)
                {
                    if (oReconciliationPeriodInfo.AllowCertificationLockdown.HasValue && oReconciliationPeriodInfo.AllowCertificationLockdown.Value == true)
                    {
                        DisableValidationForPassedDates(oReconciliationPeriodInfo.CertificationLockDownDate, calCloseOrLockDownDate, cvCompareCloseOrLockDownDateWithCurrentDate);
                    }
                    else
                    {
                        DisableValidationForPassedDates(oReconciliationPeriodInfo.ReconciliationCloseDate, calCloseOrLockDownDate, cvCompareCloseOrLockDownDateWithCurrentDate);

                    }
                }
            }

            if (oReconciliationPeriodInfo.ReconciliationPeriodStatusID.HasValue && oReconciliationPeriodInfo.ReconciliationPeriodStatusID.Value == (short)WebEnums.RecPeriodStatus.Closed)
                btnReopenPeriod.Visible = true;

            lblRecPeriod.Text = Helper.GetDisplayDate(oReconciliationPeriodInfo.PeriodEndDate);
            lblRecPeriodStatus.Text = Helper.GetDisplayStringValue(oReconciliationPeriodInfo.ReconciliationPeriodStatus);
            switch (eFeatureCapabilityModeDueDateByAccount)
            {
                case WebEnums.FeatureCapabilityMode.Visible:
                    bool IsEnable = false;
                    calPrepareDueDate.Text = "";
                    calPrepareDueDate.Enabled = IsEnable;
                    calReviewerDueDate.Text = "";
                    calReviewerDueDate.Enabled = IsEnable;
                    calApproverDueDate.Text = "";
                    calApproverDueDate.Enabled = IsEnable;
                    rfvPrepareDueDate.Enabled = IsEnable;
                    rfvReviewerDueDate.Enabled = IsEnable;
                    rfvApproverDueDate.Enabled = IsEnable;
                    cvPrepareDueDateHoliday.Enabled = IsEnable;
                    cvReviewerDueDateHoliday.Enabled = IsEnable;
                    cvApproverDueDateHoliday.Enabled = IsEnable;
                    cvComparePrepareDueDateWithCurrentDate.Enabled = IsEnable;
                    cvCompareReviewerDueDateWithCurrentDate.Enabled = IsEnable;
                    cvCompareApproverDueDateWithCurrentDate.Enabled = IsEnable;
                    // cvAllDueDateAdjacent.Enabled = IsEnable;


                    break;
            }
        }
    }
    #endregion

    #region Other Events
    protected void btnSubmit_Click(object sender, EventArgs e)
    {

        try
        {
            if (Page.IsValid)
            {
                bool isPeriodMarkedOpen = false;

                //Save in Database
                isPeriodMarkedOpen = SaveInDB();

                //Clear cache so that latest information is loaded
                string cacheKey = CacheHelper.GetCacheKeyForCompanyData(CacheConstants.ALL_RECONCILIATION_PERIODS_BASED_ON_FY);
                CacheHelper.ClearCache(cacheKey);
                SessionHelper.ClearSession(SessionConstants.CURRENT_RECONCILIATION_PREIOD_INFO);
                SessionHelper.ClearSession(SessionConstants.REC_PERIOD_STATUS_INFO);

                //set values for currentRecPeriod and currentRecPeriodEndDate
                CallEveryTime();


                int selectedRecPeriodID = Convert.ToInt32(ddlCurrentRecPeriod.SelectedItem.Value);

                // Reload the Rec Periods and also the Status / Countdown
                MasterPageBase oMasterPageBase = (MasterPageBase)this.Master;
                oMasterPageBase.ShowConfirmationMessage(1969);
                oMasterPageBase.ReloadRecPeriods();

                //ReBind Rec Period Dropdown
                BindRecPeriodDDL();

                // Update the Dropdown Status
                SetCurrentRecPeriod(selectedRecPeriodID);
                EnableDisableDDLBasedOnRecPeriodStatus(selectedRecPeriodID);
                ShowCBStatusBasedOnRecPeriodStatus(selectedRecPeriodID);

                //Rebind grid as status of some rec period might have been changed.
                BindSystemWideSettingsRadGrid();
            }
            else
            {
                //Helper.ShowInputRequirementSection(this, 1604);
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
        try
        {
            Helper.RedirectToHomePage();
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

    protected void btnCloseRecCertStart_Click(object sender, EventArgs e)
    {


        int rowsAffected = 0;
        DateTime? RevisedTime = DateTime.Now;
        int selectedRecPeriodID = Convert.ToInt32(ddlCurrentRecPeriod.SelectedItem.Value);
        IReconciliationPeriod oReconciliationPeriodClient = RemotingHelper.GetReconciliationPeriodObject();
        IsRefreshData = false;
        rowsAffected = oReconciliationPeriodClient.MarkRecPeriodReconciledAndStartCertification(selectedRecPeriodID, RevisedTime, SessionHelper.CurrentUserLoginID, (short)ARTEnums.ActionType.CloseRecAndCertStart, (short)ARTEnums.ChangeSource.RecPeriodStatusChange, Helper.GetAppUserInfo());
        if (rowsAffected > 0)
        {
            //Clear Recperiod liast in cache 
            string cacheKey = CacheHelper.GetCacheKeyForCompanyData(CacheConstants.ALL_RECONCILIATION_PERIODS_BASED_ON_FY);
            CacheHelper.ClearCache(cacheKey);
            btnCloseRecCertStart.Visible = false;

        }
        MasterPageBase oMasterPageBase = (MasterPageBase)this.Master;
        // Reload the Rec Periods and also the Status / Countdown
        oMasterPageBase.ReloadRecPeriods();
        //if (rowsAffected > 0)
        //{
        //    // Raise Alert for Open Period for Reconciliation
        //    AlertHelper.RaiseAlert(WebEnums.Alert.PeriodHasClosed, null, null, SessionHelper.CurrentRoleID.Value);
        //}
    }

    protected void ddlFinancialYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            HandleFinancialYearChange();
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

    protected void ddlCurrentRecPeriod_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            HandleRecPeriodChange();
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
    protected void ddlCurrentRecPeriod_DataBound(object sender, EventArgs e)
    {
        for (int i = 0; i < ddlCurrentRecPeriod.Items.Count; i++)
        {
            Helper.GetDisplayDate(Convert.ToDateTime(ddlCurrentRecPeriod.Items[i].Text));
        }
    }

    #endregion

    #region Validation Control Events
    protected void cvIsOnHoliday_ServerValidate(object source, ServerValidateEventArgs args)
    {
        try
        {
            ExCustomValidator cv = (ExCustomValidator)source;

            string rowRecPeriodID = cv.Attributes[cvHoliday_RecPeriodID];//cv.ToolTip;


            Telerik.Web.UI.GridDataItem gdi = (Telerik.Web.UI.GridDataItem)cv.NamingContainer;
            ExCalendar cal = (ExCalendar)gdi.FindControl(cv.ControlToValidate);
            if (IsDateOnHoliday(cal.Text, Convert.ToInt32(rowRecPeriodID)))
            {
                //cv.ErrorMessage = LanguageUtil.GetValue(cv.LabelID);

                MasterPageBase oMasterPageBase = (MasterPageBase)this.Master;
                oMasterPageBase.ShowErrorMessage(cv.LabelID);

                //Helper.ShowInputRequirementSection(this, cv.LabelID,1604);
                args.IsValid = false;
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

    protected void cvCurrentRecPeriod_ServerValidate(object source, ServerValidateEventArgs args)
    {
        try
        {
            if (int.Parse(ddlCurrentRecPeriod.SelectedItem.Value) <= 0)//"select one"
            {
                args.IsValid = false;
                Helper.ShowErrorMessage(this, LanguageUtil.GetValue(5000117));
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

    protected void cvValidateDatesTopDown_OnServerValidate(object sender, ServerValidateEventArgs args)
    {
        List<DateTime> preparerDueDateList = new List<DateTime>();
        List<DateTime> reviewerDueDateList = new List<DateTime>();
        List<DateTime> approverDueDateList = new List<DateTime>();
        List<DateTime> certStartDueDateList = new List<DateTime>();
        List<DateTime> recCloseOrCertLockDownDateList = new List<DateTime>();

        string errorMessage = LanguageUtil.GetValue(5000342);

        foreach (GridDataItem gdi in rgSystemWideSettings.Items)
        {
            ExCalendar calPrepareDueDate = gdi.FindControl("calPrepareDueDate") as ExCalendar;
            ExCalendar calReviewerDueDate = gdi.FindControl("calReviewerDueDate") as ExCalendar;
            ExCalendar calApproverDueDate = gdi.FindControl("calApproverDueDate") as ExCalendar;
            ExCalendar calCertificationStartDate = gdi.FindControl("calCertificationStartDate") as ExCalendar;
            ExCalendar calCloseOrLockDownDate = gdi.FindControl("calCloseOrLockDownDate") as ExCalendar;

            if (calPrepareDueDate != null && !String.IsNullOrEmpty(calPrepareDueDate.Text))
            {
                DateTime dtPreparerDueDate = new DateTime();
                if (DateTime.TryParse(calPrepareDueDate.Text, out dtPreparerDueDate))
                {
                    preparerDueDateList.Add(Convert.ToDateTime(dtPreparerDueDate));
                }
            }

            if (calReviewerDueDate != null && !String.IsNullOrEmpty(calReviewerDueDate.Text))
            {
                DateTime dtReviewerDueDate = new DateTime();
                if (DateTime.TryParse(calReviewerDueDate.Text, out dtReviewerDueDate))
                {
                    reviewerDueDateList.Add(Convert.ToDateTime(dtReviewerDueDate));
                }
            }
            if (calApproverDueDate != null && !String.IsNullOrEmpty(calApproverDueDate.Text))
            {
                DateTime dtApproverDueDate = new DateTime();
                if (DateTime.TryParse(calApproverDueDate.Text, out dtApproverDueDate))
                {
                    approverDueDateList.Add(Convert.ToDateTime(dtApproverDueDate));
                }
            }
            if (calCertificationStartDate != null && !String.IsNullOrEmpty(calCertificationStartDate.Text))
            {
                DateTime dtCertStartDate = new DateTime();
                if (DateTime.TryParse(calCertificationStartDate.Text, out dtCertStartDate))
                {
                    certStartDueDateList.Add(Convert.ToDateTime(dtCertStartDate));
                }
            }
            if (calCloseOrLockDownDate != null && !String.IsNullOrEmpty(calCloseOrLockDownDate.Text))
            {
                DateTime dtCloseRecOrCertLockDownDate = new DateTime();
                if (DateTime.TryParse(calCloseOrLockDownDate.Text, out dtCloseRecOrCertLockDownDate))
                {
                    recCloseOrCertLockDownDateList.Add(Convert.ToDateTime(dtCloseRecOrCertLockDownDate));
                }
            }


        }

        if (!Helper.DatesInOrder(preparerDueDateList))
        {
            args.IsValid = false;
            Helper.ShowErrorMessage(this, cvValidateDatesTopDown.Attributes["PreparerDueDateErrorMessage"]);
        }

        if (!Helper.DatesInOrder(reviewerDueDateList))
        {
            args.IsValid = false;
            Helper.ShowErrorMessage(this, cvValidateDatesTopDown.Attributes["ReviewerDueDateErrorMessage"]);
        }
        if (!Helper.DatesInOrder(approverDueDateList))
        {
            args.IsValid = false;
            Helper.ShowErrorMessage(this, cvValidateDatesTopDown.Attributes["ApproverDueDateErrorMessage"]);

        }
        if (!Helper.DatesInOrder(certStartDueDateList))
        {
            args.IsValid = false;
            Helper.ShowErrorMessage(this, cvValidateDatesTopDown.Attributes["CertStartDateErrorMessage"]);
        }
        if (!Helper.DatesInOrder(recCloseOrCertLockDownDateList))
        {
            args.IsValid = false;
            Helper.ShowErrorMessage(this, cvValidateDatesTopDown.Attributes["RecCloseDateErrorMessage"]);
        }
    }

    protected void cvAllDueDateAdjacent_OnServerValidate(object sender, ServerValidateEventArgs args)
    {
        CustomValidator cv = (CustomValidator)sender;
        int itemindex = Convert.ToInt32((cv).Attributes["itemIndex"]);
        GridDataItem gdi = rgSystemWideSettings.Items[itemindex];

        ExCalendar calPrepareDueDate = gdi.FindControl("calPrepareDueDate") as ExCalendar;
        ExCalendar calReviewerDueDate = gdi.FindControl("calReviewerDueDate") as ExCalendar;
        ExCalendar calApproverDueDate = gdi.FindControl("calApproverDueDate") as ExCalendar;
        ExCalendar calCertificationStartDate = gdi.FindControl("calCertificationStartDate") as ExCalendar;
        ExCalendar calCloseOrLockDownDate = gdi.FindControl("calCloseOrLockDownDate") as ExCalendar;

        List<DateTime> dueDateList = new List<DateTime>();

        if (calPrepareDueDate != null && !String.IsNullOrEmpty(calPrepareDueDate.Text))
        {
            DateTime dtPreparerDueDate = new DateTime();
            if (DateTime.TryParse(calPrepareDueDate.Text, out dtPreparerDueDate))
                dueDateList.Add(Convert.ToDateTime(dtPreparerDueDate));
        }

        if (calReviewerDueDate != null && !String.IsNullOrEmpty(calReviewerDueDate.Text))
        {
            DateTime dtReviewerDueDate = new DateTime();
            if (DateTime.TryParse(calReviewerDueDate.Text, out dtReviewerDueDate))
                dueDateList.Add(Convert.ToDateTime(dtReviewerDueDate));
        }
        if (calApproverDueDate != null && !String.IsNullOrEmpty(calApproverDueDate.Text))
        {
            DateTime dtApproverDueDate = new DateTime();
            if (DateTime.TryParse(calApproverDueDate.Text, out dtApproverDueDate))
                dueDateList.Add(Convert.ToDateTime(dtApproverDueDate));
        }
        if (calCertificationStartDate != null && !String.IsNullOrEmpty(calCertificationStartDate.Text))
        {
            DateTime dtCertStartDate = new DateTime();
            if (DateTime.TryParse(calCertificationStartDate.Text, out dtCertStartDate))
                dueDateList.Add(Convert.ToDateTime(dtCertStartDate));
        }
        if (calCloseOrLockDownDate != null && !String.IsNullOrEmpty(calCloseOrLockDownDate.Text))
        {
            DateTime dtCloseRecOrCertLockDownDate = new DateTime();
            if (DateTime.TryParse(calCloseOrLockDownDate.Text, out dtCloseRecOrCertLockDownDate))
                dueDateList.Add(Convert.ToDateTime(dtCloseRecOrCertLockDownDate));
        }
        if (!Helper.DatesInOrder(dueDateList))
        {
            args.IsValid = false;
            Helper.ShowErrorMessage(this, cv.ErrorMessage);
        }
    }

    protected void cvCompareDateWithCurrentDate_OnServerValidate(object sender, ServerValidateEventArgs args)
    {
        CustomValidator cv = (CustomValidator)sender;
        int itemindex = Convert.ToInt32((cv).Attributes["itemIndex"]);
        GridDataItem gdi = rgSystemWideSettings.Items[itemindex];

        ExCalendar calPrepareDueDate = gdi.FindControl("calPrepareDueDate") as ExCalendar;
        ExCalendar calReviewerDueDate = gdi.FindControl("calReviewerDueDate") as ExCalendar;
        ExCalendar calApproverDueDate = gdi.FindControl("calApproverDueDate") as ExCalendar;
        ExCalendar calCertificationStartDate = gdi.FindControl("calCertificationStartDate") as ExCalendar;
        ExCalendar calCloseOrLockDownDate = gdi.FindControl("calCloseOrLockDownDate") as ExCalendar;
        DateTime compareDate = new DateTime();
        DateTime.TryParse(cv.Attributes["dateToCompare"], out compareDate);

        List<DateTime> pastDateList = new List<DateTime>();

        if (calPrepareDueDate != null && calPrepareDueDate.Enabled && !String.IsNullOrEmpty(calPrepareDueDate.Text))
        {
            DateTime dtPreparerDueDate = new DateTime();
            if (DateTime.TryParse(calPrepareDueDate.Text, out dtPreparerDueDate))
            {
                if (compareDate > dtPreparerDueDate)
                    pastDateList.Add(Convert.ToDateTime(dtPreparerDueDate));
            }
        }

        if (calReviewerDueDate != null && calReviewerDueDate.Enabled && !String.IsNullOrEmpty(calReviewerDueDate.Text))
        {
            DateTime dtReviewerDueDate = new DateTime();
            if (DateTime.TryParse(calReviewerDueDate.Text, out dtReviewerDueDate))
            {
                if (compareDate > dtReviewerDueDate)
                    pastDateList.Add(Convert.ToDateTime(dtReviewerDueDate));
            }
        }
        if (calApproverDueDate != null && calApproverDueDate.Enabled && !String.IsNullOrEmpty(calApproverDueDate.Text)) // Ignore disabled
        {
            DateTime dtApproverDueDate = new DateTime();
            if (DateTime.TryParse(calApproverDueDate.Text, out dtApproverDueDate))
            {
                if (compareDate > dtApproverDueDate)
                    pastDateList.Add(Convert.ToDateTime(dtApproverDueDate));
            }
        }
        if (calCertificationStartDate != null && calCertificationStartDate.Enabled && !String.IsNullOrEmpty(calCertificationStartDate.Text)) // Ignore disabled
        {
            DateTime dtCertStartDate = new DateTime();
            if (DateTime.TryParse(calCertificationStartDate.Text, out dtCertStartDate))
            {
                if (compareDate > dtCertStartDate)
                    pastDateList.Add(Convert.ToDateTime(dtCertStartDate));
            }
        }
        if (calCloseOrLockDownDate != null && calCloseOrLockDownDate.Enabled && !String.IsNullOrEmpty(calCloseOrLockDownDate.Text)) // Ignore disabled
        {
            DateTime dtCloseRecOrCertLockDownDate = new DateTime();
            if (DateTime.TryParse(calCloseOrLockDownDate.Text, out dtCloseRecOrCertLockDownDate))
            {
                if (compareDate > dtCloseRecOrCertLockDownDate)
                    pastDateList.Add(Convert.ToDateTime(dtCloseRecOrCertLockDownDate));
            }
        }
        if (pastDateList.Count > 0)
        {
            args.IsValid = false;
            Helper.ShowErrorMessage(this, cv.ErrorMessage);
        }
    }

    protected void cvMaximumDocumentSize_OnServerValidate(object source, ServerValidateEventArgs args)
    {
        try
        {
            decimal inputValue;
            decimal.TryParse(txtMaximumDocumentSize.Text, out inputValue);
            if (string.IsNullOrEmpty(txtMaximumDocumentSize.Text.Trim()))
            {
                args.IsValid = false;
                Helper.ShowErrorMessage(this, rfvMaximumDocumentSize.ErrorMessage);
            }
            else if (inputValue <= 0 || inputValue > decimal.Parse(hdMaximumDocumentSize.Value))
            {
                args.IsValid = false;
                Helper.ShowErrorMessage(this, cvMaximumDocumentSize.ErrorMessage);
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

    protected void cvIsPeriodMarkedOpen_Validate(object source, ServerValidateEventArgs args)
    {
        try
        {
            if (cbIsPeriodMarkedOpen.Checked == true)
            {
                int selectedRecPeriodID = Convert.ToInt32(ddlCurrentRecPeriod.SelectedItem.Value);
                IReconciliationPeriod oReconciliationPeriodClient = RemotingHelper.GetReconciliationPeriodObject();
                List<int> oIncompleteRequirementCollection = oReconciliationPeriodClient.GetIncompleteRequirementToMarkOpen(selectedRecPeriodID, Helper.GetAppUserInfo());
                if (oIncompleteRequirementCollection != null && oIncompleteRequirementCollection.Count > 0)
                {
                    int[] oLabelIDCollection;
                    bool showOpenMarkCB = false;
                    oLabelIDCollection = GetLableIDToShowMarkOpenStatus(oIncompleteRequirementCollection, out showOpenMarkCB);
                    if (oLabelIDCollection.Length > 0)
                    {
                        trErrorAndWarnings.Visible = true;
                        ucErrorAndWarnings.Datasource = oLabelIDCollection;
                        if (!showOpenMarkCB)
                        {
                            ucErrorAndWarnings.IsErrorOrWarning = true;
                        }
                        else
                        {
                            ucErrorAndWarnings.IsErrorOrWarning = false;
                        }

                    }
                    if (!showOpenMarkCB)
                    {
                        cvIsPeriodMarkedOpen.ErrorMessage = LanguageUtil.GetValue(5000141);
                        args.IsValid = false;
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
    #endregion

    #region Private Methods
    private bool? IsAllAccountsReconciled(int recPeriodID)
    {
        int userID = SessionHelper.CurrentUserID.Value;
        short roleID = SessionHelper.CurrentRoleID.Value;
        IGLData oGLDataClient = RemotingHelper.GetGLDataObject();
        bool? isAllAccountsReconciled = oGLDataClient.GetIsAllAccountsReconciledForUserAndRole(userID, roleID, recPeriodID, Helper.GetAppUserInfo());
        return isAllAccountsReconciled;
    }

    private bool? IsMinimumRecPeriodExist(int recPeriodID)
    {
        bool? IsMinRecPeriodExist = null;
        IReconciliationPeriod oReconciliationPeriodClient = RemotingHelper.GetReconciliationPeriodObject();
        IsMinRecPeriodExist = oReconciliationPeriodClient.GetIsMinimumRecPeriodExist(recPeriodID, Helper.GetAppUserInfo());
        return IsMinRecPeriodExist;
    }
    private void HandleRefreshForceClose()
    {
        //if (hdIsRefreshData.Value == "1")
        //{
        //    MasterPageBase oMasterPageBase = (MasterPageBase)this.Master;
        //    // Reload the Rec Periods and also the Status / Countdown
        //    oMasterPageBase.ReloadRecPeriods();
        //    EnableDisableDDLBasedOnRecPeriodStatus(Convert.ToInt32(ddlCurrentRecPeriod.SelectedValue));
        //    CallEveryTime();
        //    BindSystemWideSettingsRadGrid();
        //    hdIsRefreshData.Value = "0";
        //}
    }

    private void HandleRefreshReopen()
    {
        if (hdIsRefreshData.Value == "1")
        {
            string cacheKey = CacheHelper.GetCacheKeyForCompanyData(CacheConstants.ALL_RECONCILIATION_PERIODS_BASED_ON_FY);
            CacheHelper.ClearCache(cacheKey);
            MasterPageBase oMasterPageBase = (MasterPageBase)this.Master;
            // Reload the Rec Periods and also the Status / Countdown
            oMasterPageBase.ReloadRecPeriods();
            CallEveryTime();
            BindSystemWideSettingsRadGrid();
            hdIsRefreshData.Value = "0";
            ICompany oCompanyClient = RemotingHelper.GetCompanyObject();
            List<ReconciliationPeriodInfo> oReconciliationPeriodInfoCollectionFromDB = (List<ReconciliationPeriodInfo>)oCompanyClient.SelectAllReconciliationPeriodByCompanyID(SessionHelper.CurrentCompanyID.Value, null, Helper.GetAppUserInfo());
            if (oReconciliationPeriodInfoCollectionFromDB != null)
            {
                var ReconciliationPeriodInfo = oReconciliationPeriodInfoCollectionFromDB.Find(obj => obj.ReconciliationPeriodStatusID.GetValueOrDefault() == (short)WebEnums.RecPeriodStatus.Closed);
                if (ReconciliationPeriodInfo == null)
                    btnReopenPeriod.Visible = false;
            }
        }
    }

    public override void RefreshPage(object sender, RefreshEventArgs args)
    {
        base.RefreshPage(sender, args);
        string cacheKey = CacheHelper.GetCacheKeyForCompanyData(CacheConstants.ALL_RECONCILIATION_PERIODS_BASED_ON_FY);
        CacheHelper.ClearCache(cacheKey);
        MasterPageBase oMasterPageBase = (MasterPageBase)this.Master;
        // Reload the Rec Periods and also the Status / Countdown
        oMasterPageBase.ReloadRecPeriods();
        CallEveryTime();
        BindSystemWideSettingsRadGrid();
        ddlCurrentRecPeriod_SelectedIndexChanged(null, null);
    }

    private void HandleFinancialYearChange()
    {
        /* 
         * 1. Rebind Rec Period
         * 2. Rebind Rec Period Grids
         */
        BindRecPeriodDDL();
        SetCurrentRecPeriodInDDL();
        HandleRecPeriodChange();
        BindSystemWideSettingsRadGrid();

    }

    private void HandleRecPeriodChange()
    {
        trErrorAndWarnings.Visible = false;
        int selectedRecPeriodID = Convert.ToInt32(ddlCurrentRecPeriod.SelectedItem.Value);

        hdnCurrentRecPeriodSelectedValueTemporary.Value = ddlCurrentRecPeriod.SelectedValue;
        if (selectedRecPeriodID > 0)
        {
            EnableDisableDDLBasedOnRecPeriodStatus(selectedRecPeriodID);
            ShowCBStatusBasedOnRecPeriodStatus(selectedRecPeriodID);
        }
        else
        {
            ddlCurrentRecPeriod.Enabled = true;
            ShowCBStatusBasedOnRecPeriodStatus(selectedRecPeriodID);
            HideMarkOpenCBAndValidatorAndShowStatus(5000089, false);// no period is current period
        }
    }

    //private bool IsToDisableCalander(DateTime? dateTime, Control c1, Control c2, Control c3)
    private bool IsToDisableCalander(DateTime? dateTime)
    {
        bool boolReturn = false;
        if (dateTime.HasValue)
        {
            if (dateTime.Value.Date < DateTime.Now.Date)
            {
                boolReturn = true;
            }
        }
        return boolReturn;
    }

    private void DisableValidationForPassedDates(DateTime? dateTime, ExCalendar oCalendar, CustomValidator oCustomValidator)
    {
        if (IsToDisableCalander(dateTime))
        {
            oCalendar.Enabled = false;
            oCustomValidator.Enabled = false;
        }
    }

    public override string GetMenuKey()
    {
        return "SystemWideSettings";
    }

    private void BindRecPeriodDDL()
    {
        int? financialYearID = null;
        if (ddlFinancialYear.SelectedItem != null
            && ddlFinancialYear.SelectedItem.Value != WebConstants.SELECT_ONE)
        {
            financialYearID = Convert.ToInt32(ddlFinancialYear.SelectedItem.Value);
        }

        IList<ReconciliationPeriodInfo> oReconciliationPeriodInfoCollectionFromDB;
        // Fetch from DB
        ICompany oCompanyClient = RemotingHelper.GetCompanyObject();
        oReconciliationPeriodInfoCollectionFromDB = (List<ReconciliationPeriodInfo>)oCompanyClient.SelectAllReconciliationPeriodByCompanyID(SessionHelper.CurrentCompanyID.Value, financialYearID, Helper.GetAppUserInfo());

        //IList<ReconciliationPeriodInfo> oReconciliationPeriodInfoCollectionWithCondition = new List<ReconciliationPeriodInfo>();
        //if (_currentRecPeriodEndDateInDB.HasValue)
        //{
        //    foreach (ReconciliationPeriodInfo item in oReconciliationPeriodInfoCollectionFromDB)
        //    {
        //        if (item.PeriodEndDate >= _currentRecPeriodEndDateInDB) //TODO: check if we need to do .shortDateTime() to match
        //        {
        //            oReconciliationPeriodInfoCollectionWithCondition.Add(item);
        //        }
        //    }
        //}
        //else
        //{
        //    oReconciliationPeriodInfoCollectionWithCondition = oReconciliationPeriodInfoCollectionFromDB;
        //}

        //ddlCurrentRecPeriod.DataSource = oReconciliationPeriodInfoCollectionWithCondition;
        StringBuilder oSB = new StringBuilder();
        foreach (ReconciliationPeriodInfo oRecPeriod in oReconciliationPeriodInfoCollectionFromDB)
        {
            if (!String.IsNullOrEmpty(oSB.ToString()))
                oSB.Append(",");
            oSB.Append(oRecPeriod.ReconciliationPeriodID.Value.ToString());
            oSB.Append("~");
            oSB.Append(oRecPeriod.PeriodEndDate.Value.ToShortDateString());
            oSB.Append("~");
            oSB.Append(oRecPeriod.ReconciliationPeriodStatusID.Value.ToString());
        }
        //RecPeriodID~PeriodEndDate~RecPeriodStatusID
        ddlCurrentRecPeriod.DataSource = oReconciliationPeriodInfoCollectionFromDB;
        ddlCurrentRecPeriod.DataTextField = "PeriodEndDate";
        ddlCurrentRecPeriod.DataValueField = "ReconciliationPeriodID";
        ddlCurrentRecPeriod.DataTextFormatString = "{0:d}";
        ddlCurrentRecPeriod.DataBind();
        ddlCurrentRecPeriod.Attributes.Add("RecPeriods", oSB.ToString());
    }

    private void SetCurrentRecPeriodInDDL()
    {
        ListControlHelper.AddListItemForSelectOne(ddlCurrentRecPeriod);
        if (_currentReconciliationPeriodIDInDB != null)
        {
            SetCurrentRecPeriod(_currentReconciliationPeriodIDInDB);
        }
        //hdnCurrentRecPeriodSelectedValue.Value = ddlCurrentRecPeriod.SelectedIndex.ToString();
        hdnCurrentRecPeriodSelectedValue.Value = ddlCurrentRecPeriod.SelectedValue;
        hdnCurrentRecPeriodSelectedValueTemporary.Value = ddlCurrentRecPeriod.SelectedValue;

        //Commented by Harsh for MOP. Not Needed anymore. Can be deleted
        //if (_currentReconciliationPeriodIDInDB != null)
        //{
        //    ReconciliationPeriodStatusMstInfo oReconciliationPeriodStatusMstInfo = Helper.GetRecPeriodStatusByRecPeriodID(_currentReconciliationPeriodIDInDB);
        //    WebEnums.RecPeriodStatus eRecPeriodStatus = (WebEnums.RecPeriodStatus)oReconciliationPeriodStatusMstInfo.ReconciliationPeriodStatusID;
        //    if (eRecPeriodStatus == WebEnums.RecPeriodStatus.Closed)
        //    {
        //        hdnCurrentRecPeriodStatusToBeSkipped.Value = "0";
        //    }
        //    else
        //    {
        //        hdnCurrentRecPeriodStatusToBeSkipped.Value = "1";
        //    }
        //}
        //else
        //{
        //    hdnCurrentRecPeriodStatusToBeSkipped.Value = "0";
        //}
    }

    //ToDo: on master page also there is a method SetCurrentRecPeriod(), we can move to helper class.
    private void SetCurrentRecPeriod(int? recPeriodID)
    {
        if (recPeriodID != null)
        {
            ListItem oListItem = ddlCurrentRecPeriod.Items.FindByValue(recPeriodID.ToString());
            if (oListItem != null)
            {
                ddlCurrentRecPeriod.SelectedItem.Selected = false;
                oListItem.Selected = true;
            }
        }
    }

    private void EnableDisableDDLBasedOnRecPeriodStatus(int? recPeriodID)
    {
        //TODO: get Info for the selecetd period on page DDL not on master page DDL 
        if (recPeriodID.HasValue)
        {
            IReconciliationPeriod oReconciliationPeriodClient = RemotingHelper.GetReconciliationPeriodObject();
            ReconciliationPeriodStatusMstInfo oReconciliationPeriodStatusMstInfo = oReconciliationPeriodClient.GetRecPeriodStatus(recPeriodID, Helper.GetAppUserInfo());
            if (oReconciliationPeriodStatusMstInfo != null && oReconciliationPeriodStatusMstInfo.ReconciliationPeriodStatusID.HasValue)
            {
                WebEnums.RecPeriodStatus eRecPeriodStatus = (WebEnums.RecPeriodStatus)oReconciliationPeriodStatusMstInfo.ReconciliationPeriodStatusID;
                if ((Int32)eRecPeriodStatus > 0)
                {
                    switch (eRecPeriodStatus)
                    {
                        case WebEnums.RecPeriodStatus.Open:
                        case WebEnums.RecPeriodStatus.InProgress:
                        case WebEnums.RecPeriodStatus.OpeningInProgress:
                            {
                                //TODO: uncomment following line- ddlCurrentRecPeriod.Enabled = false;
                                //Multiple Open Periods
                                //ddlCurrentRecPeriod.Enabled = false;
                                //ddlFinancialYear.Enabled = false;
                                //Commented by Vinay upon request from client
                                //btnForceClose.Visible = true;
                                //btnCloseRecCertStart.Visible = true;
                                DisableCloseRecCertStartBtn();
                                break;
                            }
                        case WebEnums.RecPeriodStatus.NotStarted:
                        case WebEnums.RecPeriodStatus.Closed:
                        case WebEnums.RecPeriodStatus.Skipped:
                            {
                                //Multiple Open Periods
                                //ddlCurrentRecPeriod.Enabled = true;
                                //ddlFinancialYear.Enabled = true;
                                btnForceClose.Visible = false;
                                btnCloseRecCertStart.Visible = false;
                                break;
                            }
                    }
                }
            }
        }
        else // no period is current period
        {
            //ddlCurrentRecPeriod.Enabled = true;
        }
    }

    private void ShowCBStatusBasedOnRecPeriodStatus(int? recPeriodID)
    {
        trErrorAndWarnings.Visible = false;
        if (recPeriodID.HasValue)
        {
            if (recPeriodID > 0)
            {
                IReconciliationPeriod oReconciliationPeriodClient = RemotingHelper.GetReconciliationPeriodObject();
                ReconciliationPeriodStatusMstInfo oReconciliationPeriodStatusMstInfo = oReconciliationPeriodClient.GetRecPeriodStatus(recPeriodID, Helper.GetAppUserInfo());
                WebEnums.RecPeriodStatus eRecPeriodStatus = (WebEnums.RecPeriodStatus)oReconciliationPeriodStatusMstInfo.ReconciliationPeriodStatusID;

                List<int> oIncompleteRequirementCollection = oReconciliationPeriodClient.GetIncompleteRequirementToMarkOpen(recPeriodID, Helper.GetAppUserInfo());
                if ((Int32)eRecPeriodStatus > 0)
                {

                    switch (eRecPeriodStatus)
                    {
                        case WebEnums.RecPeriodStatus.Open:
                            HideMarkOpenCBAndValidatorAndShowStatus(1714, false);
                            break;
                        case WebEnums.RecPeriodStatus.InProgress:
                            HideMarkOpenCBAndValidatorAndShowStatus(1090, false);
                            break;
                        case WebEnums.RecPeriodStatus.OpeningInProgress:
                            HideMarkOpenCBAndValidatorAndShowStatus(2634, false);
                            break;
                        case WebEnums.RecPeriodStatus.NotStarted:
                            if (oIncompleteRequirementCollection != null && oIncompleteRequirementCollection.Count > 0)
                            {

                                int[] oLabelIDCollection;
                                //TODO: show CB when subledger error etc, make common method
                                bool showOpenMarkCB = false;
                                oLabelIDCollection = GetLableIDToShowMarkOpenStatus(oIncompleteRequirementCollection, out showOpenMarkCB);
                                HideMarkOpenCBAndValidatorAndShowStatus(1475, showOpenMarkCB);//1475== notstarted
                                if (oLabelIDCollection.Length > 0)
                                {
                                    trErrorAndWarnings.Visible = true;
                                    ucErrorAndWarnings.Datasource = oLabelIDCollection;
                                    if (!showOpenMarkCB)
                                    {
                                        ucErrorAndWarnings.IsErrorOrWarning = true;
                                    }
                                    else
                                    {
                                        ucErrorAndWarnings.IsErrorOrWarning = false;
                                    }
                                }

                                HideMarkOpenCBAndValidatorAndShowStatus(1475, showOpenMarkCB);

                            }
                            else
                            {
                                HideMarkOpenCBAndValidatorAndShowStatus(1475, true);
                            }
                            break;
                        case WebEnums.RecPeriodStatus.Closed:
                            HideMarkOpenCBAndValidatorAndShowStatus(1715, false);
                            break;
                        case WebEnums.RecPeriodStatus.Skipped:
                            HideMarkOpenCBAndValidatorAndShowStatus(1729, false);
                            break;
                    }


                }
                else
                {
                    //TODO:error
                }
            }
            else// no period is current period
            {
                HideMarkOpenCBAndValidatorAndShowStatus(5000089, false);
            }
        }
        else// no period is current period
        {
            HideMarkOpenCBAndValidatorAndShowStatus(5000089, false);
        }
    }

    private bool IsDateOnHoliday(string date, int rowRecPeriodID)
    {
        DateTime dateTime = Convert.ToDateTime(date);
        bool isDateOnHoliday = false;
        short? selectedDateID = 0;
        selectedDateID = (short)dateTime.DayOfWeek;

        oWeekDayMstInfoCollection = SessionHelper.GetAllWeekDays();
        IList<CompanyWeekDayInfo> oCompanyWorkWeekInfoCollection = null;
        oCompanyWorkWeekInfoCollection = SetCompanyWorkWeekByRecPeriodID(Convert.ToInt32(rowRecPeriodID));


        short? WeekDayID = null;
        if (oCompanyWorkWeekInfoCollection != null)
            WeekDayID = (from oCompanyWorkWeekInfo in oCompanyWorkWeekInfoCollection
                         where oCompanyWorkWeekInfo.WeekDayID ==
                         ((from oWeekDayMstInfo in this.oWeekDayMstInfoCollection
                           where oWeekDayMstInfo.WeekDayNumber == selectedDateID
                           select oWeekDayMstInfo.WeekDayID).FirstOrDefault())
                         select oCompanyWorkWeekInfo.WeekDayID).FirstOrDefault();

        if (WeekDayID != null)
        {


            if (oHolidayCalendarInfoCollection == null)
            {
                IHolidayCalendar oHolidayCalendarClient = RemotingHelper.GetHolidayCalendarObject();
                oHolidayCalendarInfoCollection = oHolidayCalendarClient.GetHolidayCalendarByCompanyID(SessionHelper.CurrentCompanyID.Value, Helper.GetAppUserInfo());
            }
            foreach (HolidayCalendarInfo oHolidayCalendarInfo in oHolidayCalendarInfoCollection)
            {
                //if (oHolidayCalendarInfo.HolidayDate.HasValue && (oHolidayCalendarInfo.HolidayDate.Value.ToShortDateString() == date))
                if (oHolidayCalendarInfo.HolidayDate.HasValue && (oHolidayCalendarInfo.HolidayDate.Value.ToShortDateString() == dateTime.ToShortDateString()))
                {
                    isDateOnHoliday = true;
                    return isDateOnHoliday;
                }
            }
        }
        else
        {
            isDateOnHoliday = true;
            return isDateOnHoliday;

        }

        return isDateOnHoliday;
    }
    private DateTime? SetDateInInfoFromCalendar(string textCalendar)
    {
        if (textCalendar != string.Empty)
        {
            return Convert.ToDateTime(textCalendar);
        }
        else
            return null;

    }

    private void SetMaximumDocumentSizeTB()
    {
        ICompany oCompanyClient = RemotingHelper.GetCompanyObject();
        IList<CompanySettingInfo> oCompanySettingInfoCollection = oCompanyClient.SelectAllCompanySettingByCompanyID(_companyID, Helper.GetAppUserInfo());
        CompanySettingInfo oCompanySettingInfo = oCompanySettingInfoCollection[0];
        if (oCompanySettingInfo.MaximumDocumentUploadSize.HasValue)
        {
            txtMaximumDocumentSize.Text = Helper.GetDecimalValueForTextBox(oCompanySettingInfo.MaximumDocumentUploadSize, TestConstant.DECIMAL_PLACES_FOR_TEXTBOX);
        }
        else //default
        {
            txtMaximumDocumentSize.Text = Helper.GetDecimalValueForTextBox(Convert.ToDecimal(AppSettingHelper.GetAppSettingValue(AppSettingConstants.DEFAULT_MAX_DOC_SIZE)), TestConstant.DECIMAL_PLACES_FOR_TEXTBOX);
        }
    }

    private bool SaveInDB()
    {
        int? selectedFinancialyearID = null;
        bool isPeriodMarkedOpen;
        List<ReconciliationPeriodInfo> oReconciliationPeriodInfoCollection = GetSystemWideSettingDatesFromUC();
        int selectedRecPeriodID = Convert.ToInt32(ddlCurrentRecPeriod.SelectedItem.Value);
        DateTime selectedPeriodEndDate = Convert.ToDateTime(ddlCurrentRecPeriod.SelectedItem.Text);

        // Check for Select One first, then set as Current FY
        if (ddlFinancialYear.SelectedItem.Value != WebConstants.SELECT_ONE)
        {
            selectedFinancialyearID = Convert.ToInt32(ddlFinancialYear.SelectedItem.Value);
        }
        if (cbIsPeriodMarkedOpen.Visible)
            isPeriodMarkedOpen = cbIsPeriodMarkedOpen.Checked;
        else
            isPeriodMarkedOpen = false;


        CompanySettingInfo oCompanySettingInfo = new CompanySettingInfo();
        oCompanySettingInfo.CurrentReconciliationPeriodID = selectedRecPeriodID;
        oCompanySettingInfo.CurrentFinancialYearID = selectedFinancialyearID;
        oCompanySettingInfo.MaximumDocumentUploadSize = decimal.Parse(txtMaximumDocumentSize.Text);
        oCompanySettingInfo.CompanyID = SessionHelper.CurrentCompanyID;
        oCompanySettingInfo.DateAdded = DateTime.Now;
        oCompanySettingInfo.AddedBy = SessionHelper.CurrentUserLoginID;

        ICompany oCompanyClient = RemotingHelper.GetCompanyObject();
        oCompanyClient.UpdateSystemWideSetting(SessionHelper.CurrentCompanyID, selectedRecPeriodID, isPeriodMarkedOpen, DateTime.Now, SessionHelper.CurrentUserLoginID, (short)ARTEnums.ActionType.UpdateSystemWideSettings, (short)ARTEnums.ChangeSource.RecPeriodStatusChange, oReconciliationPeriodInfoCollection, oCompanySettingInfo, Helper.GetAppUserInfo());

        if (isPeriodMarkedOpen)
        {
            // Raise Alert for Open Period for Reconciliation
            AlertHelper.RaiseAlert(WebEnums.Alert.OpenPeriodForReconciliation, selectedRecPeriodID, selectedPeriodEndDate, null, null, SessionHelper.CurrentRoleID.Value, null);
        }

        return isPeriodMarkedOpen;
    }

    private void SetCompareValidatorProperty(string clientIDPreparerDueDate, string clientIDReviewerDueDate, string clientIDApproverDueDate,
        string clientIDCertificationStartDate, string clientIDCloseOrLockDownDate, ReconciliationPeriodInfo oReconciliationPeriodInfo,
        CustomValidator cvComparePrepareDueDateWithCurrentDate, CustomValidator cvCompareReviewerDueDateWithCurrentDate,
        CustomValidator cvCompareApproverDueDateWithCurrentDate, CustomValidator cvCompareCertificationStartDateWithCurrentDate,
        CustomValidator cvCompareCloseOrLockDownDateWithCurrentDate,
        WebEnums.FeatureCapabilityMode eFeatureCapabilityModeDualReview, WebEnums.FeatureCapabilityMode eFeatureCapabilityModeCertification, 
        CustomValidator cvcalCloseOrLockDownDate, int itemIndex)
    {
        string textDateToCompare = "";
        string textPeriodEndDate = Helper.GetDisplayDate(oReconciliationPeriodInfo.PeriodEndDate);
        string textCurrentDate = DateTime.Now.ToShortDateString();
        if (oReconciliationPeriodInfo.PeriodEndDate.Value > DateTime.Now)
        {
            textDateToCompare = textPeriodEndDate;
        }
        else
        {
            textDateToCompare = textCurrentDate;
        }

        cvComparePrepareDueDateWithCurrentDate.Attributes.Add("dateToCompare", textDateToCompare);
        cvComparePrepareDueDateWithCurrentDate.Attributes.Add("itemIndex", itemIndex.ToString());
        cvCompareReviewerDueDateWithCurrentDate.Attributes.Add("dateToCompare", textDateToCompare);
        cvCompareReviewerDueDateWithCurrentDate.Attributes.Add("itemIndex", itemIndex.ToString());
        cvCompareApproverDueDateWithCurrentDate.Attributes.Add("dateToCompare", textDateToCompare);
        cvCompareApproverDueDateWithCurrentDate.Attributes.Add("itemIndex", itemIndex.ToString());
        cvCompareCertificationStartDateWithCurrentDate.Attributes.Add("dateToCompare", textDateToCompare);
        cvCompareCertificationStartDateWithCurrentDate.Attributes.Add("itemIndex", itemIndex.ToString());
        cvCompareCloseOrLockDownDateWithCurrentDate.Attributes.Add("dateToCompare", textDateToCompare);
        cvCompareCloseOrLockDownDateWithCurrentDate.Attributes.Add("itemIndex", itemIndex.ToString());

        cvComparePrepareDueDateWithCurrentDate.ErrorMessage = Helper.GetErrorMessageForSystemWide(WebEnums.FieldType.DateCompareWithRecOrCurrentDateField, textPeriodEndDate, 1417);
        cvCompareReviewerDueDateWithCurrentDate.ErrorMessage = Helper.GetErrorMessageForSystemWide(WebEnums.FieldType.DateCompareWithRecOrCurrentDateField, textPeriodEndDate, 1418);
        cvCompareApproverDueDateWithCurrentDate.ErrorMessage = Helper.GetErrorMessageForSystemWide(WebEnums.FieldType.DateCompareWithRecOrCurrentDateField, textPeriodEndDate, 1738);
        cvCompareCertificationStartDateWithCurrentDate.ErrorMessage = Helper.GetErrorMessageForSystemWide(WebEnums.FieldType.DateCompareWithRecOrCurrentDateField, textPeriodEndDate, 1453);
        cvCompareCloseOrLockDownDateWithCurrentDate.ErrorMessage = Helper.GetErrorMessageForSystemWide(WebEnums.FieldType.DateCompareWithRecOrCurrentDateField, textPeriodEndDate, 1419);
        cvcalCloseOrLockDownDate.ErrorMessage = Helper.GetErrorMessage(WebEnums.FieldType.InvalidDate, 1419);
        string controlToComparePRA = "";
        if (eFeatureCapabilityModeDualReview == WebEnums.FeatureCapabilityMode.Visible)
        {
            controlToComparePRA = clientIDApproverDueDate;
        }
        else
        {
            controlToComparePRA = clientIDReviewerDueDate;
            cvCompareApproverDueDateWithCurrentDate.Visible = false;
        }

        if (eFeatureCapabilityModeCertification != WebEnums.FeatureCapabilityMode.Visible)
        {
            cvCompareCertificationStartDateWithCurrentDate.Visible = false;
        }
    }

    private void AddPropertyForAdjacentDateCheck(
        CustomValidator cvAllDueDateAdjacent
       , string controlListForAdjacentToCompare
        , string textPeriodEndDate
        , int itemIndex)
    {

        cvAllDueDateAdjacent.Attributes.Add("controlListForAdjacentToCompare", controlListForAdjacentToCompare);
        cvAllDueDateAdjacent.ErrorMessage = string.Format(LanguageUtil.GetValue(5000178), textPeriodEndDate);
        cvAllDueDateAdjacent.Attributes.Add("itemIndex", itemIndex.ToString());
    }

    private void HideMarkOpenCBAndValidatorAndShowStatus(int labelID, bool showOpenMarkCB)
    {
        if (showOpenMarkCB)
        {
            cbIsPeriodMarkedOpen.Visible = true;
            cvIsPeriodMarkedOpen.Visible = true;
        }
        else
        {
            cbIsPeriodMarkedOpen.Checked = false;
            cbIsPeriodMarkedOpen.Visible = false;
            cvIsPeriodMarkedOpen.Visible = false;
        }
        lblPeriodStatus.Visible = true;
        lblPeriodStatus.LabelID = labelID;
    }

    //return bool value indicating if row is editabl on the basis of Rec Period Status ID
    private bool EnableDisableCalendarsBasedOnRecPeriodStatus(short? recPeriodStatusID)
    {
        bool isCalendarActive = true;
        if (recPeriodStatusID.HasValue)
        {
            switch (recPeriodStatusID.Value)
            {
                case (short)WebEnums.RecPeriodStatus.NotStarted:
                    isCalendarActive = true;
                    break;
                case (short)WebEnums.RecPeriodStatus.Open:
                    isCalendarActive = true;
                    break;
                case (short)WebEnums.RecPeriodStatus.InProgress:
                    isCalendarActive = true;
                    break;
                case (short)WebEnums.RecPeriodStatus.OpeningInProgress:
                    isCalendarActive = false;
                    break;
                case (short)WebEnums.RecPeriodStatus.Closed:
                    isCalendarActive = false;
                    break;
                case (short)WebEnums.RecPeriodStatus.Skipped:
                    isCalendarActive = false;
                    break;
            }
        }
        return isCalendarActive;

    }

    //obsolete. Can be removed
    private bool EnableDisableCalendarsBasedOnRecPeriodStatus(int? recPeriodID, DateTime recPeriodEndDate)
    {
        bool isCalendarActive = false;
        if (_currentRecPeriodEndDateInDB.HasValue)
        {

            if (recPeriodEndDate.Date < _currentRecPeriodEndDateInDB.Value.Date)
            {
                isCalendarActive = false;
            }
            else if (recPeriodEndDate.Date > _currentRecPeriodEndDateInDB.Value.Date)
            {
                isCalendarActive = true;
            }
            else if (recPeriodEndDate.Date == _currentRecPeriodEndDateInDB.Value.Date)
            {
                //TODO: get Info for the selecetd period on page DDL not on master page DDL 
                ReconciliationPeriodStatusMstInfo oReconciliationPeriodStatusMstInfo = Helper.GetRecPeriodStatusByRecPeriodID(recPeriodID);
                WebEnums.RecPeriodStatus eRecPeriodStatus = (WebEnums.RecPeriodStatus)oReconciliationPeriodStatusMstInfo.ReconciliationPeriodStatusID;
                if ((Int32)eRecPeriodStatus > 0)
                {
                    switch (eRecPeriodStatus)
                    {
                        case WebEnums.RecPeriodStatus.NotStarted:
                        case WebEnums.RecPeriodStatus.Open:
                        case WebEnums.RecPeriodStatus.InProgress:
                            isCalendarActive = true;
                            break;
                        case WebEnums.RecPeriodStatus.Closed:
                        case WebEnums.RecPeriodStatus.Skipped:
                            isCalendarActive = false;
                            break;
                    }
                }
            }
        }
        else
        {
            isCalendarActive = true;
        }
        return isCalendarActive;
    }

    private void CallOnlyWhenFirstTime()
    {
        SetMaximumDocumentSizeTB();

        if (_currentRecPeriodEndDateInDB.HasValue)
        {
            hdnCurrentRecPeriodEndDate.Value = _currentRecPeriodEndDateInDB.Value.ToShortDateString();
        }

        ListControlHelper.BindFinancialYearDropdown(ddlFinancialYear, true);
        //if (SessionHelper.CurrentFinancialYearID != null)
        //{
        //    ddlFinancialYear.SelectedValue = SessionHelper.CurrentFinancialYearID.Value.ToString();
        //}
        ReconciliationPeriodInfo oCurrentMaxPeriod = Helper.GetMaxReconciliationPeriodInfo();
        if (oCurrentMaxPeriod.FinancialYearID != null)
        {
            ddlFinancialYear.SelectedValue = oCurrentMaxPeriod.FinancialYearID.Value.ToString();
        }
        HandleFinancialYearChange();
    }

    private void CallEveryTime()
    {
        _companyID = SessionHelper.CurrentCompanyID;
        //Commented for MOP. Can be deleted
        //IReconciliationPeriod oReconciliationPeriodClient = RemotingHelper.GetReconciliationPeriodObject();
        //_currentRecPeriodEndDateInDB = oReconciliationPeriodClient.GetCurrentPeriodByCompanyId(SessionHelper.CurrentCompanyID.Value);

        //ICompany oCompanyClient = RemotingHelper.GetCompanyObject();
        //CompanySettingInfo oCompanySettingInfo = oCompanyClient.GetCurrentReconciliationPeriod(SessionHelper.CurrentCompanyID);

        //_currentReconciliationPeriodIDInDB = oCompanySettingInfo.CurrentReconciliationPeriodID;
        ReconciliationPeriodInfo oCurrentMaxPeriod = Helper.GetMaxReconciliationPeriodInfo();
        if (oCurrentMaxPeriod != null)
        {
            _currentReconciliationPeriodIDInDB = oCurrentMaxPeriod.ReconciliationPeriodID;
            _currentRecPeriodEndDateInDB = oCurrentMaxPeriod.PeriodEndDate;
        }

    }

    private void SetErrorMessages()
    {
        rfvMaximumDocumentSize.ErrorMessage = Helper.GetErrorMessage(WebEnums.FieldType.MandatoryField, this.lblMaximumDocumentSize.LabelID);
        //cvMaximumDocumentSize.ErrorMessage = LanguageUtil.GetValue(5000189);
        cvMaximumDocumentSize.ErrorMessage = string.Format(LanguageUtil.GetValue(5000188), hdMaximumDocumentSize.Value);
        //cvCurrentRecPeriod.ErrorMessage = LanguageUtil.GetValue(5000117);

        string errorMessage = LanguageUtil.GetValue(5000342);
        cvValidateDatesTopDown.Attributes.Add("PreparerDueDateErrorMessage", string.Format(errorMessage, LanguageUtil.GetValue(1417)));
        cvValidateDatesTopDown.Attributes.Add("ReviewerDueDateErrorMessage", string.Format(errorMessage, LanguageUtil.GetValue(1418)));
        cvValidateDatesTopDown.Attributes.Add("ApproverDueDateErrorMessage", string.Format(errorMessage, LanguageUtil.GetValue(1738)));
        cvValidateDatesTopDown.Attributes.Add("CertStartDateErrorMessage", string.Format(errorMessage, LanguageUtil.GetValue(1453)));
        cvValidateDatesTopDown.Attributes.Add("RecCloseDateErrorMessage", string.Format(errorMessage, LanguageUtil.GetValue(1419)));
        cvValidateDatesTopDown.Attributes.Add("AllDueDateErrorMessage", string.Format(errorMessage, LanguageUtil.GetValue(2976)));


    }

    private int[] GetLableIDToShowMarkOpenStatus(List<int> oIncompleteRequirementCollection, out bool showOpenMarkCB)
    {
        //int[] oLabelIDCollection = new ;
        List<int> oLabelIDCollection = new List<int>();
        showOpenMarkCB = true;

        if (IsErrorInCollection(oIncompleteRequirementCollection, WebEnums.ReturnCodeStatusMarkOpen.GLDataNoSuccessUpload))
        {
            showOpenMarkCB = false;
            oLabelIDCollection.Add(5000075);
        }

        if (IsErrorInCollection(oIncompleteRequirementCollection, WebEnums.ReturnCodeStatusMarkOpen.GLDataToBeProcessed))
        {
            showOpenMarkCB = false;
            oLabelIDCollection.Add(5000073);
        }
        if (IsErrorInCollection(oIncompleteRequirementCollection, WebEnums.ReturnCodeStatusMarkOpen.GLDataProcessing))
        {
            showOpenMarkCB = false;
            oLabelIDCollection.Add(5000074);
        }
        if (IsErrorInCollection(oIncompleteRequirementCollection, WebEnums.ReturnCodeStatusMarkOpen.GLDataWarning))
        {
            showOpenMarkCB = false;
            oLabelIDCollection.Add(5000309);
        }
        if (IsErrorInCollection(oIncompleteRequirementCollection, WebEnums.ReturnCodeStatusMarkOpen.CapabilityAllNotMarked))
        {
            showOpenMarkCB = false;
            oLabelIDCollection.Add(5000110);
        }
        if (IsErrorInCollection(oIncompleteRequirementCollection, WebEnums.ReturnCodeStatusMarkOpen.CapabilityDetailNotComplete))
        {
            showOpenMarkCB = false;
            oLabelIDCollection.Add(5000111);
        }
        if (IsErrorInCollection(oIncompleteRequirementCollection, WebEnums.ReturnCodeStatusMarkOpen.CapabilityUnexpVarNotSet))
        {
            showOpenMarkCB = false;
            oLabelIDCollection.Add(5000111);
        }
        //if (IsErrorInCollection(oIncompleteRequirementCollection, WebEnums.ReturnCodeStatusMarkOpen.ExchangeRateUploadNotAvailable))
        //{
        //    showOpenMarkCB = false;
        //    oLabelIDCollection.Add(5000112);
        //}
        if (IsErrorInCollection(oIncompleteRequirementCollection, WebEnums.ReturnCodeStatusMarkOpen.ToBeReprocessed))
        {
            showOpenMarkCB = false;
            oLabelIDCollection.Add(5000119);
        }

        if (IsErrorInCollection(oIncompleteRequirementCollection, WebEnums.ReturnCodeStatusMarkOpen.PreparerDueDateNotSet))
        {
            showOpenMarkCB = false;
            oLabelIDCollection.Add(5000125);
        }
        if (IsErrorInCollection(oIncompleteRequirementCollection, WebEnums.ReturnCodeStatusMarkOpen.ReviewerDueDateNotSet))
        {
            showOpenMarkCB = false;
            oLabelIDCollection.Add(5000126);
        }
        if (IsErrorInCollection(oIncompleteRequirementCollection, WebEnums.ReturnCodeStatusMarkOpen.ApproverDueDateNotSet))
        {
            showOpenMarkCB = false;
            oLabelIDCollection.Add(5000127);
        }
        if (IsErrorInCollection(oIncompleteRequirementCollection, WebEnums.ReturnCodeStatusMarkOpen.CertificationStartDateNotSet))
        {
            showOpenMarkCB = false;
            oLabelIDCollection.Add(5000128);
        }
        if (IsErrorInCollection(oIncompleteRequirementCollection, WebEnums.ReturnCodeStatusMarkOpen.ReconciliationCloseDateNotSet))
        {
            showOpenMarkCB = false;
            oLabelIDCollection.Add(5000129);
        }
        if (IsErrorInCollection(oIncompleteRequirementCollection, WebEnums.ReturnCodeStatusMarkOpen.CertificationLockDownDateNotSet))
        {
            showOpenMarkCB = false;
            oLabelIDCollection.Add(5000130);
        }

        if (IsErrorInCollection(oIncompleteRequirementCollection, WebEnums.ReturnCodeStatusMarkOpen.SubledgerDataWarning))
        {
            showOpenMarkCB = false;
            oLabelIDCollection.Add(5000310);
        }
        if (IsErrorInCollection(oIncompleteRequirementCollection, WebEnums.ReturnCodeStatusMarkOpen.DayTypeIsNotSetUp))
        {
            showOpenMarkCB = false;
            oLabelIDCollection.Add(5000417);
        }

        //commented by Prafull on 01-Sep-2011 To change subledeger load requirement message from error to  warning 
        //if (IsErrorInCollection(oIncompleteRequirementCollection, WebEnums.ReturnCodeStatusMarkOpen.SubledgerDataNotAvailableForAllSubledgerAccount))
        //{
        //    showOpenMarkCB = false;
        //    oLabelIDCollection.Add(5000115);
        //}
        //if (IsErrorInCollection(oIncompleteRequirementCollection, WebEnums.ReturnCodeStatusMarkOpen.SubledgerDataNotAvailable))
        //{
        //    showOpenMarkCB = false;
        //    oLabelIDCollection.Add(5000114);
        //}
        if (showOpenMarkCB == true)//to separate the errors and warnings
        {

            if (IsErrorInCollection(oIncompleteRequirementCollection, WebEnums.ReturnCodeStatusMarkOpen.AttribteTemplateNotSetForAll))
            {
                oLabelIDCollection.Add(5000131);
            }
            if (IsErrorInCollection(oIncompleteRequirementCollection, WebEnums.ReturnCodeStatusMarkOpen.AttribteOwnershipNotSetForAll))
            {
                oLabelIDCollection.Add(5000132);
            }
            if (IsErrorInCollection(oIncompleteRequirementCollection, WebEnums.ReturnCodeStatusMarkOpen.SubledgerSourceNotUploaded))
            {
                oLabelIDCollection.Add(5000113);
            }
            if (IsErrorInCollection(oIncompleteRequirementCollection, WebEnums.ReturnCodeStatusMarkOpen.NoAccountForThisPeriod))
            {
                oLabelIDCollection.Add(5000167);
            }
            if (IsErrorInCollection(oIncompleteRequirementCollection, WebEnums.ReturnCodeStatusMarkOpen.ReconciliationFrequencyNotSet))
            {
                oLabelIDCollection.Add(5000172);
            }
            if (IsErrorInCollection(oIncompleteRequirementCollection, WebEnums.ReturnCodeStatusMarkOpen.ExchangeRateUploadNotAvailable))
            {
                oLabelIDCollection.Add(5000112);
            }

            //**added by Prafull on 01-Sep-2011 To change subledeger load requirement message from error to  warning 
            if (IsErrorInCollection(oIncompleteRequirementCollection, WebEnums.ReturnCodeStatusMarkOpen.SubledgerDataNotAvailableForAllSubledgerAccount))
            {
                oLabelIDCollection.Add(5000115);
            }
            if (IsErrorInCollection(oIncompleteRequirementCollection, WebEnums.ReturnCodeStatusMarkOpen.SubledgerDataNotAvailable))
            {
                oLabelIDCollection.Add(5000114);
            }


            //JE Configuration for GL Tool columns not done
            if (IsErrorInCollection(oIncompleteRequirementCollection, WebEnums.ReturnCodeStatusMarkOpen.JEConfigurationNotSet))
            {
                oLabelIDCollection.Add(5000222);
            }
            //JE Write Off / On Approvers not set
            if (IsErrorInCollection(oIncompleteRequirementCollection, WebEnums.ReturnCodeStatusMarkOpen.JEWriteOffOnApproversNotSet))
            {
                oLabelIDCollection.Add(5000223);
            }
            //No SRA rules set for this period - Warning
            if (IsErrorInCollection(oIncompleteRequirementCollection, WebEnums.ReturnCodeStatusMarkOpen.SRARulesNotSet))
            {
                oLabelIDCollection.Add(5000219);
            }
            //Some of the Certification Verbiage not set for this period - Warning
            if (IsErrorInCollection(oIncompleteRequirementCollection, WebEnums.ReturnCodeStatusMarkOpen.CertificationVerbiageNotSet))
            {
                oLabelIDCollection.Add(5000220);
            }
            //Alert not set for this period - Warning
            if (IsErrorInCollection(oIncompleteRequirementCollection, WebEnums.ReturnCodeStatusMarkOpen.CompanyAlertNotSet))
            {
                oLabelIDCollection.Add(5000221);
            }
            //Alert not set for this period - Warning
            if (IsErrorInCollection(oIncompleteRequirementCollection, WebEnums.ReturnCodeStatusMarkOpen.FYNotSet))
            {
                oLabelIDCollection.Add(5000251);
            }
            if (IsErrorInCollection(oIncompleteRequirementCollection, WebEnums.ReturnCodeStatusMarkOpen.CurrencyExchangeRateNotAvailable))
            {
                oLabelIDCollection.Add(5000327);
            }
            if (IsErrorInCollection(oIncompleteRequirementCollection, WebEnums.ReturnCodeStatusMarkOpen.DueDateIsNotAvailableForReconcilableAccounts))
            {
                oLabelIDCollection.Add(2759);
            }
        }

        return oLabelIDCollection.ToArray();

    }

    private bool IsErrorInCollection(List<int> oIncompleteRequirementCollection, WebEnums.ReturnCodeStatusMarkOpen oReturnCodeStatusMarkOpen)
    {
        List<int> oIncompleteRequirementCollectionTemp = null;
        oIncompleteRequirementCollectionTemp = oIncompleteRequirementCollection.Where(recItem => recItem == (int)oReturnCodeStatusMarkOpen).ToList();
        if (oIncompleteRequirementCollectionTemp != null && oIncompleteRequirementCollectionTemp.Count > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private string SetDateFromCalendar(ExCalendar cal)
    {
        if (cal != null && cal.Text != "")
        {
            return cal.Text;
        }
        else return "";
    }

    private void getDocumentMaximumSizeLimit()
    {
        try
        {
            IAppSetting oAppSettingClient = RemotingHelper.GetAppSettingObject();
            AppSettingsInfo oAppSettingsInfo = oAppSettingClient.SelectAppSettingByAppSettingID(Convert.ToInt32(WebEnums.AppSetting.MaxFileUploadSize), Helper.GetAppUserInfo());
            hdMaximumDocumentSize.Value = Convert.ToDouble(oAppSettingsInfo.AppSettingValue).ToString();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void btnForceClose_Click(object sender, EventArgs e)
    {
        //var url = "CloseRecPeriodPopup.aspx";
        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "newWindow", "OpenRadWindow('" + url + "','280','500');", true);
        //Int16? RecPeriodStatus = (Int16?)WebEnums.RecPeriodStatus.Closed;
        DateTime? RevisedTime = DateTime.Now;

        int selectedRecPeriodID = Convert.ToInt32(ddlCurrentRecPeriod.SelectedItem.Value);
        DateTime selectedPeriodEndDate = Convert.ToDateTime(ddlCurrentRecPeriod.SelectedItem.Text);
        WebEnums.FeatureCapabilityMode eFeatureCapabilityMode = Helper.GetFeatureCapabilityMode(WebEnums.Feature.Certification, ARTEnums.Capability.CertificationActivation, selectedRecPeriodID);
        if (eFeatureCapabilityMode == WebEnums.FeatureCapabilityMode.Visible)
        {
            _isCertificationActivated = true;

        }
        else
        {
            _isCertificationActivated = false;
        }
        IReconciliationPeriod oReconciliationPeriodClient = RemotingHelper.GetReconciliationPeriodObject();
        AccountCertificationStatusInfo oAccountCertificationStatusInfo = oReconciliationPeriodClient.GetAccountAndCertificationStatus(selectedRecPeriodID, SessionHelper.CurrentUserID, SessionHelper.CurrentCompanyID, _isCertificationActivated.Value, SessionHelper.CurrentRoleID, Helper.GetAppUserInfo());
        Session[SessionConstants.ACCOUNT_CERTIFICATION_STATUS_INFO_OBJECT] = oAccountCertificationStatusInfo;
        if (_isCertificationActivated.Value)
        {
            // Commented by manoj:- popup will come in every time for confirmation
            //if (oAccountCertificationStatusInfo.oAccountStatusInfoCollection.Count == 0 && oAccountCertificationStatusInfo.oCertificationStatusInfo.UnCertifiedAccountsCount == 0)
            //{

            //    IsRefreshData = false;
            //    rowsAffected = oReconciliationPeriodClient.CloseRecPeriodByRecPeriodIdAndComanyID(selectedRecPeriodID, RecPeriodStatus, RevisedTime, SessionHelper.CurrentUserLoginID);
            //    MasterPageBase oMasterPageBase = (MasterPageBase)this.Master;
            //    // Reload the Rec Periods and also the Status / Countdown
            //    oMasterPageBase.ReloadRecPeriods();
            //    if (rowsAffected > 0)
            //    {
            //        // Raise Alert for Open Period for Reconciliation
            //        AlertHelper.RaiseAlert(WebEnums.Alert.PeriodHasClosed, null, null, SessionHelper.CurrentRoleID.Value);
            //    }
            //}
            //else
            //{
            IsRefreshData = true;

            string url = "CloseRecPeriodPopup.aspx?";
            url += "&" + QueryStringConstants.PARENT_HIDDEN_FIELD + "=" + hdIsRefreshData.ClientID;
            url += "&" + QueryStringConstants.REC_PERIOD_ID + "=" + selectedRecPeriodID;
            url += "&" + QueryStringConstants.REC_PERIODC_END_DATE + "=" + selectedPeriodEndDate;

            //var url = "CloseRecPeriodPopup.aspx";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "newWindow", "OpenRadWindow('" + url + "','320','500');", true);
            //}
        }
        else
        {
            //if (oAccountCertificationStatusInfo.oAccountStatusInfoCollection.Count == 0)
            //{

            //    IsRefreshData = false;
            //    rowsAffected = oReconciliationPeriodClient.CloseRecPeriodByRecPeriodIdAndComanyID(selectedRecPeriodID, RecPeriodStatus, RevisedTime, SessionHelper.CurrentUserLoginID);
            //    MasterPageBase oMasterPageBase = (MasterPageBase)this.Master;
            //    // Reload the Rec Periods and also the Status / Countdown
            //    oMasterPageBase.ReloadRecPeriods();
            //    if (rowsAffected > 0)
            //    {
            //        // Raise Alert for Open Period for Reconciliation
            //        AlertHelper.RaiseAlert(WebEnums.Alert.PeriodHasClosed, null, null, SessionHelper.CurrentRoleID.Value);
            //    }
            //}
            //else
            //{
            IsRefreshData = true;

            string url = "CloseRecPeriodPopup.aspx?";
            url += "&" + QueryStringConstants.PARENT_HIDDEN_FIELD + "=" + hdIsRefreshData.ClientID;
            url += "&" + QueryStringConstants.REC_PERIOD_ID + "=" + selectedRecPeriodID;
            url += "&" + QueryStringConstants.REC_PERIODC_END_DATE + "=" + selectedPeriodEndDate;

            //var url = "CloseRecPeriodPopup.aspx";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "newWindow", "OpenRadWindow('" + url + "','320','500');", true);
            //}


        }
    }

    protected void btnReopenPeriod_Click(object sender, EventArgs e)
    {
        IsRefreshData = true;
        string url = "ReOpenRecPeriodPopup.aspx?";
        url += "&" + QueryStringConstants.PARENT_HIDDEN_FIELD + "=" + hdIsRefreshData.ClientID;
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "newWindow", "OpenRadWindow('" + url + "','320','400');", true);
    }

    private IList<CompanyWeekDayInfo> SetCompanyWorkWeekByRecPeriodID(int RecPeriodID)
    {
        int? financialYearID = null;
        if (ddlFinancialYear.SelectedItem != null && ddlFinancialYear.SelectedItem.Value != WebConstants.SELECT_ONE)
        {
            financialYearID = Convert.ToInt32(ddlFinancialYear.SelectedItem.Value);
        }
        if (ViewState["CompanyWorkWeekInfoCollectionForFincncialyear"] == null)
        {
            // Fetch from DB
            ICompany oCompanyClient = RemotingHelper.GetCompanyObject();
            oCompanyWorkWeekInfoCollectionForFincncialyear = (List<CompanyWeekDayInfo>)oCompanyClient.SelectAllWorkWeekByFinancialYearIDAndCompanyID(SessionHelper.CurrentCompanyID.Value, financialYearID, Helper.GetAppUserInfo());
            ViewState["CompanyWorkWeekInfoCollectionForFincncialyear"] = oCompanyWorkWeekInfoCollectionForFincncialyear;
        }
        else
        {
            oCompanyWorkWeekInfoCollectionForFincncialyear = (IList<CompanyWeekDayInfo>)ViewState["CompanyWorkWeekInfoCollectionForFincncialyear"];
        }
        var oCompanyWeekDayInfocollection =
                        (from o in oCompanyWorkWeekInfoCollectionForFincncialyear
                         where o.RecPeriodID.Value == RecPeriodID
                         select o).ToList<CompanyWeekDayInfo>();
        return oCompanyWeekDayInfocollection;


    }

    private void DisableCloseRecCertStartBtn()
    {

        int selectedRecPeriodID = Convert.ToInt32(ddlCurrentRecPeriod.SelectedItem.Value);
        WebEnums.FeatureCapabilityMode eFeatureCapabilityMode = Helper.GetFeatureCapabilityMode(WebEnums.Feature.Certification, ARTEnums.Capability.CertificationActivation, selectedRecPeriodID);
        if (eFeatureCapabilityMode == WebEnums.FeatureCapabilityMode.Visible)
        {

            IReconciliationPeriod oReconciliationPeriodClient = RemotingHelper.GetReconciliationPeriodObject();
            //Commented by Vinay upon request from client
            //btnCloseRecCertStart.Visible = !(oReconciliationPeriodClient.GetIsStopRecAndStartCertFlag(selectedRecPeriodID));
            bool? IsTotalAccountsReconciled = IsAllAccountsReconciled(selectedRecPeriodID);
            if (IsTotalAccountsReconciled.HasValue)
            {
                if (IsTotalAccountsReconciled.Value)
                    btnCloseRecCertStart.Visible = false;
            }

        }
        else
        {
            btnCloseRecCertStart.Visible = false;
        }

        bool? IsMinRecPeriodExist = IsMinimumRecPeriodExist(selectedRecPeriodID);
        if (IsMinRecPeriodExist.HasValue)
        {
            if (IsMinRecPeriodExist.Value)
            {
                btnCloseRecCertStart.Visible = false;
                btnForceClose.Visible = false;
            }
        }

    }

    #endregion

    #region Other Methods
    protected void BindSystemWideSettingsRadGrid()
    {
        int? financialYearID = null;
        List<ReconciliationPeriodInfo> oReconciliationPeriodInfoCollection = null;
        ICompany oCompanyClient = RemotingHelper.GetCompanyObject();

        if (ddlFinancialYear.SelectedItem != null
            && ddlFinancialYear.SelectedItem.Value != WebConstants.SELECT_ONE)
        {
            financialYearID = Convert.ToInt32(ddlFinancialYear.SelectedItem.Value);
        }

        oReconciliationPeriodInfoCollection = (List<ReconciliationPeriodInfo>)oCompanyClient.SelectAllReconciliationPeriodByCompanyID(SessionHelper.CurrentCompanyID.Value, financialYearID, Helper.GetAppUserInfo());
        foreach (ReconciliationPeriodInfo oRecPeriodInfo in oReconciliationPeriodInfoCollection)
        {

            ReconciliationPeriodStatusMstInfo oRecPeriodStatusInfo = Helper.GetRecPeriodStatusByRecPeriodID(oRecPeriodInfo.ReconciliationPeriodID);
            oRecPeriodInfo.ReconciliationPeriodStatus = LanguageUtil.GetValue((int)oRecPeriodStatusInfo.ReconciliationPeriodStatusLabelID);

        }

        rgSystemWideSettings.DataSource = oReconciliationPeriodInfoCollection;
        rgSystemWideSettings.DataBind();
    }

    public List<ReconciliationPeriodInfo> GetSystemWideSettingDatesFromUC()
    {
        List<ReconciliationPeriodInfo> oReconciliationPeriodInfoCollection = new List<ReconciliationPeriodInfo>();
        for (int i = 0; i < rgSystemWideSettings.Items.Count; i++)
        {
            //TODO: may need to change it based on simple date comparison
            if (rgSystemWideSettings.Items[i].Enabled == true)
            {
                ReconciliationPeriodInfo oReconciliationPeriodInfo = new ReconciliationPeriodInfo();
                string s = rgSystemWideSettings.MasterTableView.DataKeyValues[i]["ReconciliationPeriodID"].ToString();
                oReconciliationPeriodInfo.ReconciliationPeriodID = Convert.ToInt32(s);

                ExCalendar calPrepareDueDate = rgSystemWideSettings.Items[i].FindControl("calPrepareDueDate") as ExCalendar;
                ExCalendar calReviewerDueDate = rgSystemWideSettings.Items[i].FindControl("calReviewerDueDate") as ExCalendar;
                ExCalendar calApproverDueDate = rgSystemWideSettings.Items[i].FindControl("calApproverDueDate") as ExCalendar;
                ExCalendar calCloseOrLockDownDate = rgSystemWideSettings.Items[i].FindControl("calCloseOrLockDownDate") as ExCalendar;
                ExCalendar calCertificationStartDate = rgSystemWideSettings.Items[i].FindControl("calCertificationStartDate") as ExCalendar;
                ExCheckBox cbAllowCertificationLockDown = rgSystemWideSettings.Items[i].FindControl("cbAllowCertificationLockDown") as ExCheckBox;

                oReconciliationPeriodInfo.PreparerDueDate = SetDateInInfoFromCalendar(calPrepareDueDate.Text);
                oReconciliationPeriodInfo.ReviewerDueDate = SetDateInInfoFromCalendar(calReviewerDueDate.Text);
                if (calApproverDueDate != null)
                {
                    oReconciliationPeriodInfo.ApproverDueDate = SetDateInInfoFromCalendar(calApproverDueDate.Text);
                }

                WebEnums.FeatureCapabilityMode eFeatureCapabilityModeCertification = Helper.GetFeatureCapabilityMode(WebEnums.Feature.Certification, ARTEnums.Capability.CertificationActivation, oReconciliationPeriodInfo.ReconciliationPeriodID);
                if (eFeatureCapabilityModeCertification == WebEnums.FeatureCapabilityMode.Visible)
                {
                    oReconciliationPeriodInfo.CertificationStartDate = SetDateInInfoFromCalendar(calCertificationStartDate.Text);
                    if (cbAllowCertificationLockDown.Checked)
                    {
                        oReconciliationPeriodInfo.AllowCertificationLockdown = true;
                        oReconciliationPeriodInfo.CertificationLockDownDate = SetDateInInfoFromCalendar(calCloseOrLockDownDate.Text);
                    }
                    else
                    {
                        oReconciliationPeriodInfo.AllowCertificationLockdown = false;
                        oReconciliationPeriodInfo.ReconciliationCloseDate = SetDateInInfoFromCalendar(calCloseOrLockDownDate.Text);
                    }
                }
                else
                {
                    oReconciliationPeriodInfo.ReconciliationCloseDate = SetDateInInfoFromCalendar(calCloseOrLockDownDate.Text);
                }
                oReconciliationPeriodInfoCollection.Add(oReconciliationPeriodInfo);
            }
        }
        return oReconciliationPeriodInfoCollection;
    }
    #endregion

}//end of class


