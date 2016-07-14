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
using SkyStem.ART.Web.Data;
using SkyStem.ART.Web.Utility;
using SkyStem.Language.LanguageUtility;
using SkyStem.ART.Client.IServices;
using SkyStem.ART.Client.Model;
using System.Collections.Generic;
using SkyStem.ART.Web.Classes;
using SkyStem.ART.Client.Data;
using SkyStem.Library.Controls.WebControls;
using System.Text;
using SkyStem.Library.Controls.TelerikWebControls;
using Telerik.Web.UI;
using SkyStem.ART.Shared.Utility;

public partial class Pages_EditItemAccrubleRecurring : PopupPageBaseRecItem
{
    #region Variables & Constants

    private long _GLDataRecurringItemScheduleID;
    private bool _IsMultiCurrencyEnabled;
    private string _recPeriodsAll;
    #endregion

    #region Properties

    protected string RecPeriodsAll { get { return _recPeriodsAll; } }

    private decimal? ExRateLCCYtoBCCY
    {
        get
        {
            decimal exRate;
            if (Decimal.TryParse(hdnExRateLCCYtoBCCY.Value, out exRate) && exRate != 0)
                return exRate;
            return null;
        }
        set { hdnExRateLCCYtoBCCY.Value = value.GetValueOrDefault().ToString(); }
    }

    private decimal? ExRateLCCYtoRCCY
    {
        get
        {
            decimal exRate;
            if (Decimal.TryParse(hdnExRateLCCYtoRCCY.Value, out exRate) && exRate != 0)
                return exRate;
            return null;
        }
        set { hdnExRateLCCYtoRCCY.Value = value.GetValueOrDefault().ToString(); }
    }

    private bool IsExchangeRateOverridden
    {
        get
        {
            if (hdnIsExchangeRateOverridden.Value == "1")
                return true;
            return false;
        }
        set
        {
            if (value)
                hdnIsExchangeRateOverridden.Value = "1";
            else
                hdnIsExchangeRateOverridden.Value = "0";
        }
    }

    private GLDataRecurringItemScheduleInfo CurrentGLDataRecurringItemScheduleInfo
    {
        get { return (GLDataRecurringItemScheduleInfo)Session[SessionConstants.CURRENT_GLDATA_ITEM_SCHEDULE_INFO]; }
        set { Session[SessionConstants.CURRENT_GLDATA_ITEM_SCHEDULE_INFO] = value; }
    }
    #endregion

    #region Delegates & Events
    #endregion

    #region Page Events
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            int[] oLableIdCollection = new int[0];
            PopupHelper.SetPageTitle(this, this.RecCategory.Value, this.RecCategoryType.Value, oLableIdCollection);
            SetLabels(this.RecCategoryType.Value);

            if (!string.IsNullOrEmpty(hdnStartInterval.Value))
            {
                ddlStartInterval.SelectedValue = hdnStartInterval.Value;
                hdnStartInterval.Value = string.Empty;
            }

            SetCompanyCabalityInfo();
            GetQueryStringValues();
            SetErrorMessagesForValidationControls();

            if (!IsPostBack)
            {
                lblOriginalAmountRCCYCode.Text = SessionHelper.ReportingCurrencyCode;
                lblTotalAccruedAmountRCCYCode.Text = SessionHelper.ReportingCurrencyCode;
                lblToBeAccruedAmountRCCYCode.Text = SessionHelper.ReportingCurrencyCode;
                lblCurrentAmountRCCYCode.Text = SessionHelper.ReportingCurrencyCode;
                lblOverriddenExRateBCCYCode.Text = this.CurrentBCCY;
                lblOverriddenExRateRCCYCode.Text = SessionHelper.ReportingCurrencyCode;

                PopulateItemsOnPage();

            }

            SetExchangeRates();
            this.lblInputFormRecPeriodValue.Text = Helper.GetDisplayDate(SessionHelper.CurrentReconciliationPeriodEndDate);
            ucAccountHierarchyDetailPopup.AccountID = this.AccountID.Value;
            ucAccountHierarchyDetailPopup.NetAccountID = this.NetAccountID;
            hlDocument.NavigateUrl = "javascript:OpenRadWindowFromRadWindow('" + SetDocumentUploadURL() + "', '480', '800');";
            calScheduleBeginDate.JSCallbackFunction = "CalculateTotalAndCurrentInterval();";
            calScheduleBeginDate.Attributes.Add("onblur", "CalculateTotalAndCurrentInterval();");
            calScheduleEndDate.JSCallbackFunction = "CalculateTotalAndCurrentInterval();";
            calScheduleEndDate.Attributes.Add("onblur", "CalculateTotalAndCurrentInterval();");
            ucExchangeRate.BCCYCode = this.CurrentBCCY;
            ucExchangeRate.RCCYCode = SessionHelper.ReportingCurrencyCode;
            optDailyInterval.InputAttributes.Add("onclick", "EnableDisableIntervalControls();");
            optOtherInterval.InputAttributes.Add("onclick", "EnableDisableIntervalControls();CalculateTotalAndCurrentInterval();");
            ddlStartInterval.Attributes.Add("onchange", "InitScheduleBeginDate();EnableDisableIntervalControls();");
            SetModeForFormView();
        }
        catch (Exception ex)
        {
            PopupHelper.ShowErrorMessage(this, ex);
        }
    }
    #endregion

    #region Grid Events
    #endregion

    #region Other Events
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                IGLDataRecItemSchedule oGLDataRecItemScheduleClient = RemotingHelper.GetGLDataRecItemScheduleObject();

                if (this.Mode == QueryStringConstants.INSERT)
                {
                    GLDataRecurringItemScheduleInfo oGLDataRecurringItemScheduleInfo = this.GetGLReconciliationItemInputInfo();
                    List<AttachmentInfo> oAttachmentInfoCollection = (List<AttachmentInfo>)Session[SessionConstants.ATTACHMENTS];

                    oGLDataRecItemScheduleClient.InsertGLDataRecurringItemSchedule(oGLDataRecurringItemScheduleInfo, SessionHelper.CurrentReconciliationPeriodID.Value, oAttachmentInfoCollection, Helper.GetAppUserInfo());
                    SessionHelper.ClearSession(SessionConstants.ATTACHMENTS);
                }

                else if (this.Mode == QueryStringConstants.EDIT)
                {
                    GLDataRecurringItemScheduleInfo oGLDataRecurringItemScheduleInfo = this.GetGLReconciliationItemInputInfo();
                    if (!string.IsNullOrEmpty(calCloseDate.Text))
                    {
                        oGLDataRecurringItemScheduleInfo.CloseDate = Convert.ToDateTime(calCloseDate.Text);
                    }
                    oGLDataRecItemScheduleClient.UpdateGLDataRecurringItemSchedule(oGLDataRecurringItemScheduleInfo, (short)ARTEnums.AccountAttribute.ReconciliationTemplate, Helper.GetAppUserInfo());
                }

                if (!String.IsNullOrEmpty(this.ParentHiddenField))
                {
                    this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "SetHiddenFieldStatus", ScriptHelper.GetJSToSetParentWindowElementValue(this.ParentHiddenField, "1")); // 1 means Reload data of GridVieww
                }
                this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "ClosePopupAndRefreshParentPage", ScriptHelper.GetJSForClosePopupAndSubmitParentPage());

            }

            else
            {
                Page.Validate();
            }

        }
        catch (Exception ex)
        {
            PopupHelper.ShowErrorMessage(this, ex);
        }
    }
    protected void btnOvereideExchangeRate_Click(object sender, EventArgs e)
    {
        SetExchangeRateAndRecalculateAmount();
    }


    protected void btnRecalculateSchedule_Click(object sender, EventArgs e)
    {
        decimal amt;
        Decimal.TryParse(txtOriginalAmountLCCY.Text, out amt);
        if (optDailyInterval.Checked)
        {
            DateTime startDT, endDT;
            if (this.Mode == QueryStringConstants.EDIT && CurrentGLDataRecurringItemScheduleInfo.ScheduleBeginDate.GetValueOrDefault() < SessionHelper.CurrentReconciliationPeriodEndDate)
                DateTime.TryParse(lblScheduleBeginDateValue.Text, out startDT);
            else
                DateTime.TryParse(calScheduleBeginDate.Text, out startDT);
            DateTime.TryParse(calScheduleEndDate.Text, out endDT);
            ucScheduleIntervalDetails.RecalculateSchedule(amt, startDT, endDT);
            //RecalculateScheduleDaysWise();
        }
        else
        {
            int totalIntervals, startInterval, currentInterval;
            if (this.Mode == QueryStringConstants.EDIT)
                Int32.TryParse(txtCurrentInterval.Text, out currentInterval);
            else
                Int32.TryParse(txtCurrentInterval.Text, out currentInterval);
            Int32.TryParse(ddlStartInterval.SelectedValue, out startInterval);
            Int32.TryParse(txtTotalIntervals.Text, out totalIntervals);
            if (startInterval > 0)
                ucScheduleIntervalDetails.RecalculateSchedule(amt, totalIntervals, startInterval, currentInterval, CurrentGLDataRecurringItemScheduleInfo.PrevRecPeriodID);
            else
                ucScheduleIntervalDetails.RecalculateSchedule(amt, totalIntervals, null, currentInterval, CurrentGLDataRecurringItemScheduleInfo.PrevRecPeriodID);
            //RecalculateScheduleIntervalwise();
        }
        CurrentGLDataRecurringItemScheduleInfo.ScheduleAmount = amt;
        if (IsExchangeRateOverridden)
        {
            if (ExRateLCCYtoBCCY.GetValueOrDefault() != 0)
                CurrentGLDataRecurringItemScheduleInfo.ExRateLCCYtoBCCY = ExRateLCCYtoBCCY;
            if (ExRateLCCYtoRCCY.GetValueOrDefault() != 0)
                CurrentGLDataRecurringItemScheduleInfo.ExRateLCCYtoRCCY = ExRateLCCYtoRCCY;
        }
        else
        {
            CurrentGLDataRecurringItemScheduleInfo.ExRateLCCYtoBCCY = null;
            CurrentGLDataRecurringItemScheduleInfo.ExRateLCCYtoRCCY = null;
        }
        UpdateRCCYAmountAndLabels();
    }
    protected void ddlLocalCurrency_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            PopupHelper.HideErrorMessage(this);
            IsExchangeRateOverridden = false;
            if (ddlLocalCurrency.SelectedValue == WebConstants.SELECT_ONE)
            {
                this.ExRateLCCYtoBCCY = null;
                this.ExRateLCCYtoRCCY = null;
            }
            SetExchangeRateAndRecalculateAmount();
        }
        catch (Exception ex)
        {
            PopupHelper.ShowErrorMessage(this, ex);
        }
    }

    protected void ddlStartInterval_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            PopupHelper.HideErrorMessage(this);
            if (Convert.ToInt32(ddlStartInterval.SelectedValue) > 0)
            {
                if (ddlStartInterval.SelectedValue == SessionHelper.CurrentReconciliationPeriodID.ToString())
                    txtCurrentInterval.Text = "1";
                else
                    txtCurrentInterval.Text = "";
                calScheduleBeginDate.Text = ddlStartInterval.SelectedItem.Text;
            }
            SetExchangeRateAndRecalculateAmount();
        }
        catch (Exception ex)
        {
            PopupHelper.ShowErrorMessage(this, ex);
        }
    }

    #endregion

    #region Validation Control Events
    protected void cvOriginalAmount_ServerValidate(object source, ServerValidateEventArgs args)
    {
        if (this.Mode == QueryStringConstants.EDIT)
        {
            // check if Schedule is not created in current period
            long OriginalGLDataRecurringItemScheduleID;
            OriginalGLDataRecurringItemScheduleID = Convert.ToInt64(this.hdnOriginalGLDataRecurringItemScheduleID.Value);

            decimal newOrigAmt;
            Decimal.TryParse(txtOriginalAmountLCCY.Text, out newOrigAmt);

            decimal prevBalance = ucScheduleIntervalDetails.GetUsedBalanceUptoPreviousPeriod().GetValueOrDefault();

            if (OriginalGLDataRecurringItemScheduleID > 0)
            {

                if (Math.Abs(newOrigAmt) < Math.Abs(prevBalance))
                {
                    args.IsValid = false;
                    cvOriginalAmount.ErrorMessage = string.Format(LanguageUtil.GetValue(5000108), LanguageUtil.GetValue(2432) + " (" + Helper.GetDisplayDecimalValue(prevBalance) + ")");
                }
            }
        }
    }

    protected void cvCompareScheduleBeginDateWithScheduleEndDate_OnServerValidate(object source, ServerValidateEventArgs args)
    {
        DateTime beginDate;
        DateTime endDate;
        DateTime.TryParse(calScheduleBeginDate.Text, out beginDate);
        DateTime.TryParse(calScheduleEndDate.Text, out endDate);
        if (!string.IsNullOrEmpty(calScheduleBeginDate.Text) && !string.IsNullOrEmpty(calScheduleEndDate.Text))
        {
            if (endDate < beginDate)
                args.IsValid = false;
        }
        else if (!(string.IsNullOrEmpty(calScheduleBeginDate.Text) && string.IsNullOrEmpty(calScheduleEndDate.Text)))
        {
            args.IsValid = false;
        }
    }

    protected void cvCompareRecPeriodEndDateWithScheduleDates_OnServerValidate(object source, ServerValidateEventArgs args)
    {
        if (ddlStartInterval.SelectedValue == null || Convert.ToInt32(ddlStartInterval.SelectedValue) <= 0)
        {
            DateTime periodDate;
            DateTime beginDate;
            DateTime endDate;
            DateTime.TryParse(lblInputFormRecPeriodValue.Text, out periodDate);
            DateTime.TryParse(calScheduleBeginDate.Text, out beginDate);
            DateTime.TryParse(calScheduleEndDate.Text, out endDate);
            if (!string.IsNullOrEmpty(calScheduleBeginDate.Text) && !string.IsNullOrEmpty(calScheduleEndDate.Text))
            {
                if (periodDate < beginDate || periodDate > endDate)
                    args.IsValid = false;
            }
        }
    }

    protected void cvStartInterval_ServerValidate(object source, ServerValidateEventArgs args)
    {
        DateTime startIntervalPeriodDate;
        DateTime beginDate;
        DateTime endDate;
        DateTime.TryParse(ddlStartInterval.SelectedItem.Text, out startIntervalPeriodDate);
        DateTime.TryParse(calScheduleBeginDate.Text, out beginDate);
        DateTime.TryParse(calScheduleEndDate.Text, out endDate);
        if (ddlStartInterval.SelectedValue != null && Convert.ToInt32(ddlStartInterval.SelectedValue) > 0
            && !string.IsNullOrEmpty(calScheduleBeginDate.Text) && !string.IsNullOrEmpty(calScheduleEndDate.Text))
        {
            if (startIntervalPeriodDate < beginDate || startIntervalPeriodDate > endDate)
                args.IsValid = false;
        }
    }
    #endregion

    #region Private Methods
    private void SetLabels(short RecCategoryType)
    {
        if ((short)WebEnums.RecCategoryType.Accrual_SupportingDetail_RecurringAccrualSchedule == RecCategoryType)
        {
            PopupHelper.ShowInputRequirementSection(this, 2050, 2060, 2350, 2255, 2436, 1710);
            lblScheduleName.LabelID = 1666;
            lblScheduleBeginDate.LabelID = 1667;
            lblScheduleEndDate.LabelID = 1668;
            ExLabl14.LabelID = 2047;
            ExLabel3.LabelID = 2046;
            lblStartInterval.LabelID = 2733;
        }
        else
        {
            PopupHelper.ShowInputRequirementSection(this, 2043, 2069, 2350, 2255, 2435, 1710);
            lblScheduleName.LabelID = 1669;
            lblScheduleBeginDate.LabelID = 1670;
            lblScheduleEndDate.LabelID = 1671;
            ExLabl14.LabelID = 2070;
            ExLabel3.LabelID = 2071;
            lblStartInterval.LabelID = 2732;
        }
    }



    private void SetErrorMessagesForValidationControls()
    {
        // Validate Amount
        cvOriginalAmount.ErrorMessage = Helper.GetErrorMessage(WebEnums.FieldType.InvalidNumericField, lblOriginalAmountLCCY.LabelID);

        // Mandatory Field Validations
        rfvLocalCurrency.InitialValue = WebConstants.SELECT_ONE;
        rfvLocalCurrency.ErrorMessage = Helper.GetErrorMessage(WebEnums.FieldType.MandatoryField, this.lblLocalCurrencyCode.LabelID);
        rfvOriginalAmount.ErrorMessage = Helper.GetErrorMessage(WebEnums.FieldType.MandatoryField, this.lblOriginalAmountLCCY.LabelID);
        rfvOpenDate.ErrorMessage = Helper.GetErrorMessage(WebEnums.FieldType.MandatoryField, this.lblOpenDate.LabelID);
        rfvScheduleBeginDate.ErrorMessage = Helper.GetErrorMessage(WebEnums.FieldType.MandatoryField, this.lblScheduleBeginDate.LabelID);
        rfvScheduleEndDate.ErrorMessage = Helper.GetErrorMessage(WebEnums.FieldType.MandatoryField, this.lblScheduleEndDate.LabelID);
        txtScheduleName.ErrorMessage = Helper.GetErrorMessage(WebEnums.FieldType.MandatoryField, this.lblScheduleName.LabelID);

        // Validations for Valid Date
        cvOpenDate.ErrorMessage = Helper.GetErrorMessage(WebEnums.FieldType.InvalidDate, lblOpenDate.LabelID);
        cvCloseDate.ErrorMessage = Helper.GetErrorMessage(WebEnums.FieldType.InvalidDate, lblCloseDate.LabelID);
        cvScheduleBeginDate.ErrorMessage = Helper.GetErrorMessage(WebEnums.FieldType.InvalidDate, lblScheduleBeginDate.LabelID); ;
        cvScheduleEndDate.ErrorMessage = Helper.GetErrorMessage(WebEnums.FieldType.InvalidDate, lblScheduleEndDate.LabelID); ; ;

        // Date Comparison Validations
        cvCompareOpenDateWithCurrentDate.ErrorMessage = Helper.GetErrorMessage(WebEnums.FieldType.DateCompareField, this.lblOpenDate.LabelID, 2062);
        cvCompareCloseDateWithCurrentDate.ErrorMessage = Helper.GetErrorMessage(WebEnums.FieldType.DateCompareField, this.lblCloseDate.LabelID, 2062);
        cvCompareCloseDateWithOpenDate.ErrorMessage = Helper.GetErrorMessage(WebEnums.FieldType.DateCompareFieldGreaterThan, this.lblCloseDate.LabelID, this.lblOpenDate.LabelID);

        cvCompareScheduleBeginDateWithScheduleEndDate.ErrorMessage = Helper.GetErrorMessage(WebEnums.FieldType.DateCompareField, this.lblScheduleBeginDate.LabelID, this.lblScheduleEndDate.LabelID);
        cvCompareOpenDateWithScheduleEndDate.ErrorMessage = Helper.GetErrorMessage(WebEnums.FieldType.DateCompareField, this.lblOpenDate.LabelID, this.lblScheduleEndDate.LabelID);
        cvStartInterval.ErrorMessage = Helper.GetErrorMessage(WebEnums.FieldType.CompareBetweenField, this.lblStartInterval.LabelID, this.lblScheduleBeginDate.LabelID, this.lblScheduleEndDate.LabelID);
        if (this.RecCategoryType == (short)WebEnums.RecCategoryType.Accrual_SupportingDetail_RecurringAccrualSchedule)
        {
            cvCompareRecPeriodEndDateWithScheduleDates.ErrorMessage = LanguageUtil.GetValue(2060);
        }
        else
        {
            cvCompareRecPeriodEndDateWithScheduleDates.ErrorMessage = LanguageUtil.GetValue(2069);
        }
    }

    private void SetExchangeRates()
    {
        if (ddlLocalCurrency.SelectedValue != WebConstants.SELECT_ONE && (this.Mode == QueryStringConstants.EDIT || this.Mode == QueryStringConstants.INSERT))
        {
            ucExchangeRate.LCCYCode = ddlLocalCurrency.SelectedValue;
        }
        else
            ucExchangeRate.LCCYCode = lblLocalCurrencyCodeValue.Text;

        if (!IsExchangeRateOverridden)
        {
            this.ExRateLCCYtoBCCY = CacheHelper.GetExchangeRate(ucExchangeRate.LCCYCode, this.CurrentBCCY, SessionHelper.CurrentReconciliationPeriodID);
            this.ExRateLCCYtoRCCY = CacheHelper.GetExchangeRate(ucExchangeRate.LCCYCode, SessionHelper.ReportingCurrencyCode, SessionHelper.CurrentReconciliationPeriodID);
        }
        if (!CurrentGLDataRecurringItemScheduleInfo.PrevRecPeriodID.HasValue)
            CurrentGLDataRecurringItemScheduleInfo.LocalCurrencyCode = ucExchangeRate.LCCYCode;

        hlOverrideExchangeRate.NavigateUrl = "javascript:{OpenRadWindowFromRadWindow('" + SetOverrideExchangeRateURL() + "', '300', '350');}";
        lblOverriddenExRateBCCYValue.Text = Helper.GetDisplayExchangeRateValue(ExRateLCCYtoBCCY);
        lblOverriddenExRateRCCYValue.Text = Helper.GetDisplayExchangeRateValue(ExRateLCCYtoRCCY);

        if (_IsMultiCurrencyEnabled && IsExchangeRateOverridden)
            pnlOverriddenExRate.Visible = true;
        else
            pnlOverriddenExRate.Visible = false;
    }

    private void PopulateItemsOnPage()
    {
        IGLData oGLDataClient = RemotingHelper.GetGLDataObject();
        DateTime? prevRecPeriodEndDate = null;

        if (this.Mode == QueryStringConstants.EDIT
            || this.Mode == QueryStringConstants.READ_ONLY)
        {
            IGLDataRecItemSchedule oGLDataRecItemScheduleClient = RemotingHelper.GetGLDataRecItemScheduleObject();
            CurrentGLDataRecurringItemScheduleInfo = oGLDataRecItemScheduleClient.GetGLDataRecurringItemScheduleInfo(_GLDataRecurringItemScheduleID, Helper.GetAppUserInfo());

            if (CurrentGLDataRecurringItemScheduleInfo != null)
            {
                if (CurrentGLDataRecurringItemScheduleInfo.OriginalGLDataRecurringItemScheduleID.HasValue)
                    hdnOriginalGLDataRecurringItemScheduleID.Value = CurrentGLDataRecurringItemScheduleInfo.OriginalGLDataRecurringItemScheduleID.Value.ToString();
                else
                    hdnOriginalGLDataRecurringItemScheduleID.Value = "0";


                lblEnteredByValue.Text = CurrentGLDataRecurringItemScheduleInfo.AddedByFirstName + " " + CurrentGLDataRecurringItemScheduleInfo.AddedByLastName;
                lblDateAddedValue.Text = Helper.GetDisplayDate(CurrentGLDataRecurringItemScheduleInfo.DateAdded);

                txtOriginalAmountLCCY.Text = Helper.GetDecimalValueForTextBox(CurrentGLDataRecurringItemScheduleInfo.ScheduleAmount, TestConstant.DECIMAL_PLACES_FOR_TEXTBOX);
                txtComments.Text = CurrentGLDataRecurringItemScheduleInfo.Comments;
                calCloseDate.Text = Helper.GetDisplayDateForCalendar(CurrentGLDataRecurringItemScheduleInfo.CloseDate);

                //Populate Labels
                lblOriginalAmountLCCYValue.Text = Helper.GetDecimalValueForTextBox(CurrentGLDataRecurringItemScheduleInfo.ScheduleAmount, TestConstant.DECIMAL_PLACES_FOR_TEXTBOX);
                lblCommentsValue.Text = CurrentGLDataRecurringItemScheduleInfo.Comments;
                lblCloseDateValue.Text = Helper.GetDisplayDate(CurrentGLDataRecurringItemScheduleInfo.CloseDate);

                lblScheduleBeginDateValue.Text = Helper.GetDisplayDate(CurrentGLDataRecurringItemScheduleInfo.ScheduleBeginDate);
                lblScheduleEndDateValue.Text = Helper.GetDisplayDate(CurrentGLDataRecurringItemScheduleInfo.ScheduleEndDate);
                lblOpenDateValue.Text = Helper.GetDisplayDate(CurrentGLDataRecurringItemScheduleInfo.OpenDate);

                calOpenDate.Text = Helper.GetDisplayDateForCalendar(CurrentGLDataRecurringItemScheduleInfo.OpenDate);
                calScheduleBeginDate.Text = Helper.GetDisplayDateForCalendar(CurrentGLDataRecurringItemScheduleInfo.ScheduleBeginDate);
                calScheduleEndDate.Text = Helper.GetDisplayDateForCalendar(CurrentGLDataRecurringItemScheduleInfo.ScheduleEndDate);

                txtScheduleName.Text = CurrentGLDataRecurringItemScheduleInfo.ScheduleName;
                lblScheduleNameValue.Text = CurrentGLDataRecurringItemScheduleInfo.ScheduleName;

                hdnTotalAccruedAmountLCCYValue.Value = CurrentGLDataRecurringItemScheduleInfo.RecPeriodAmountLocalCurrency.GetValueOrDefault().ToString();
                hdnTotalAccruedAmountRCCYValue.Value = CurrentGLDataRecurringItemScheduleInfo.RecPeriodAmountReportingCurrency.GetValueOrDefault().ToString();

                lblOriginalAmountRCCYValue.Text = Helper.GetDisplayDecimalValue(CurrentGLDataRecurringItemScheduleInfo.ScheduleAmountReportingCurrency);

                lblTotalAccruedAmountRCCYValue.Text = Helper.GetDisplayDecimalValue(CurrentGLDataRecurringItemScheduleInfo.RecPeriodAmountReportingCurrency);

                lblToBeAccruedAmountRCCYValue.Text = Helper.GetDisplayDecimalValue(CurrentGLDataRecurringItemScheduleInfo.BalanceReportingCurrency);
                lblRecItemNumberValue.Text = Helper.GetDisplayStringValue(CurrentGLDataRecurringItemScheduleInfo.RecItemNumber);
                lblMatchSetRefNoValue.Text = Helper.GetDisplayStringValue(CurrentGLDataRecurringItemScheduleInfo.MatchSetRefNumber);

                chkDontShowOnRecForm.Checked = CurrentGLDataRecurringItemScheduleInfo.IgnoreInCalculation.GetValueOrDefault();

                //Calculation FrequencyId and Intervals details
                if (CurrentGLDataRecurringItemScheduleInfo.TotalIntervals != null)
                {
                    txtTotalIntervals.Text = Helper.GetDisplayIntegerValue(CurrentGLDataRecurringItemScheduleInfo.TotalIntervals);
                    lblTotalIntervalValue.Text = Helper.GetDisplayIntegerValue(CurrentGLDataRecurringItemScheduleInfo.TotalIntervals);
                }

                if (CurrentGLDataRecurringItemScheduleInfo.CurrentInterval != null)
                {
                    txtCurrentInterval.Text = Helper.GetDisplayIntegerValue(CurrentGLDataRecurringItemScheduleInfo.CurrentInterval);
                    lblCurrentIntervalValue.Text = Helper.GetDisplayIntegerValue(CurrentGLDataRecurringItemScheduleInfo.CurrentInterval);
                }
                if (CurrentGLDataRecurringItemScheduleInfo.CalculationFrequencyID != null)
                {
                    short calculationfrequencyID = (short)CurrentGLDataRecurringItemScheduleInfo.CalculationFrequencyID;
                    optDailyInterval.Checked = (calculationfrequencyID == (short)ARTEnums.CalculationFrequency.DailyInterval ? true : false);
                    optOtherInterval.Checked = (calculationfrequencyID == (short)ARTEnums.CalculationFrequency.OtherInterval ? true : false);
                    txtTotalIntervals.Enabled = (calculationfrequencyID == (short)ARTEnums.CalculationFrequency.OtherInterval ? true : false);
                    txtCurrentInterval.Enabled = (calculationfrequencyID == (short)ARTEnums.CalculationFrequency.OtherInterval ? true : false);
                }

                // Get PRev Rec Period End Date
                if (CurrentGLDataRecurringItemScheduleInfo.PrevRecPeriodID != null)
                {
                    ReconciliationPeriodInfo oReconciliationPeriodInfo = Helper.GetRecPeriodInfo(CurrentGLDataRecurringItemScheduleInfo.PrevRecPeriodID);

                    if (oReconciliationPeriodInfo != null)
                    {
                        prevRecPeriodEndDate = oReconciliationPeriodInfo.PeriodEndDate;
                        hdnPrevRecPeriodEndDate.Value = Helper.GetDisplayDate(oReconciliationPeriodInfo.PeriodEndDate);
                    }
                }

                if (CurrentGLDataRecurringItemScheduleInfo.PreviousGLDataRecurringItemScheduleID != null)
                {
                    hdnPrevGLDataRecurringItemScheduleID.Value = CurrentGLDataRecurringItemScheduleInfo.PreviousGLDataRecurringItemScheduleID.ToString();
                }
                //For Edit Mode, Set the OriginalAmount properties of user control
                ucScheduleIntervalDetails.OriginalAmount = (decimal)CurrentGLDataRecurringItemScheduleInfo.ScheduleAmount;
                if (this.CurrentGLDataRecurringItemScheduleInfo.ExRateLCCYtoBCCY.HasValue)
                {
                    lblOverriddenExRateBCCYValue.Text = Helper.GetDisplayExchangeRateValue(this.CurrentGLDataRecurringItemScheduleInfo.ExRateLCCYtoBCCY);
                    this.ExRateLCCYtoBCCY = this.CurrentGLDataRecurringItemScheduleInfo.ExRateLCCYtoBCCY;
                }
                if (this.CurrentGLDataRecurringItemScheduleInfo.ExRateLCCYtoRCCY.HasValue)
                {
                    lblOverriddenExRateRCCYValue.Text = Helper.GetDisplayExchangeRateValue(this.CurrentGLDataRecurringItemScheduleInfo.ExRateLCCYtoRCCY);
                    this.ExRateLCCYtoRCCY = this.CurrentGLDataRecurringItemScheduleInfo.ExRateLCCYtoRCCY;
                    IsExchangeRateOverridden = true;
                }
                // Get Start Rec Period End Date
                ucScheduleIntervalDetails.StartIntervalRecPeriodID = this.CurrentGLDataRecurringItemScheduleInfo.StartIntervalRecPeriodID;
                if (CurrentGLDataRecurringItemScheduleInfo.StartIntervalRecPeriodID != null)
                {
                    ReconciliationPeriodInfo oReconciliationPeriodInfo = Helper.GetRecPeriodInfo(CurrentGLDataRecurringItemScheduleInfo.StartIntervalRecPeriodID);

                    if (oReconciliationPeriodInfo != null)
                    {
                        lblStartIntervalRecPeriodDateValue.Text = Helper.GetDisplayDate(oReconciliationPeriodInfo.PeriodEndDate);
                    }
                }
            }
        }
        else
        {
            UserHdrInfo oUserHdrInfo = SessionHelper.GetCurrentUser();
            lblEnteredByValue.Text = oUserHdrInfo.FirstName + " " + oUserHdrInfo.LastName;
            lblDateAddedValue.Text = Helper.GetDisplayDate(DateTime.Today);
            CurrentGLDataRecurringItemScheduleInfo = new GLDataRecurringItemScheduleInfo();
            CurrentGLDataRecurringItemScheduleInfo.GLDataRecurringItemScheduleIntervalDetailInfoList = new List<GLDataRecurringItemScheduleIntervalDetailInfo>();
            // Schedule Item Financial Year needs to be ignored
            ICompany oCompanyClient = RemotingHelper.GetCompanyObject();
            List<ReconciliationPeriodInfo> oReconciliationPeriodInfoList = (List<ReconciliationPeriodInfo>)oCompanyClient.SelectAllReconciliationPeriodByCompanyID(SessionHelper.CurrentCompanyID.Value, null, Helper.GetAppUserInfo());//CacheHelper.GetAllReconciliationPeriods();
            foreach (ReconciliationPeriodInfo oReconciliationPeriodInfo in oReconciliationPeriodInfoList)
            {
                GLDataRecurringItemScheduleIntervalDetailInfo oGLDataRecurringItemScheduleIntervalDetailInfo = null;
                if (oReconciliationPeriodInfo.PeriodEndDate >= SessionHelper.CurrentReconciliationPeriodEndDate &&
                    (WebEnums.RecPeriodStatus)oReconciliationPeriodInfo.ReconciliationPeriodStatusID != WebEnums.RecPeriodStatus.Closed &&
                    (WebEnums.RecPeriodStatus)oReconciliationPeriodInfo.ReconciliationPeriodStatusID != WebEnums.RecPeriodStatus.Skipped)
                {
                    oGLDataRecurringItemScheduleIntervalDetailInfo = new GLDataRecurringItemScheduleIntervalDetailInfo();
                    oGLDataRecurringItemScheduleIntervalDetailInfo.ReconciliationPeriodID = oReconciliationPeriodInfo.ReconciliationPeriodID;
                    oGLDataRecurringItemScheduleIntervalDetailInfo.PeriodEndDate = oReconciliationPeriodInfo.PeriodEndDate;

                    CurrentGLDataRecurringItemScheduleInfo.GLDataRecurringItemScheduleIntervalDetailInfoList.Add(oGLDataRecurringItemScheduleIntervalDetailInfo);
                }
            }
            // Set the  OriginalAmount properties of user control
            ucScheduleIntervalDetails.OriginalAmount = 0;
        }

        ucScheduleIntervalDetails.RecCategoryType = this.RecCategoryType.Value;

        ucScheduleIntervalDetails.ScheduleIntervalDetails = CurrentGLDataRecurringItemScheduleInfo.GLDataRecurringItemScheduleIntervalDetailInfoList;
        ucScheduleIntervalDetails.BindData();

        ListControlHelper.BindLocalCurrencyDropDown(ddlLocalCurrency, this.GLDataID.Value, this._IsMultiCurrencyEnabled);
        if (this._IsMultiCurrencyEnabled && this.Mode != QueryStringConstants.EDIT)
        {
            ListControlHelper.AddListItemForCCY(ddlLocalCurrency);
        }
        if (CurrentGLDataRecurringItemScheduleInfo != null && this._IsMultiCurrencyEnabled && this.Mode == QueryStringConstants.EDIT)
        {
            if (this.ddlLocalCurrency.Items.FindByValue(CurrentGLDataRecurringItemScheduleInfo.LocalCurrencyCode) != null)
                this.ddlLocalCurrency.SelectedValue = CurrentGLDataRecurringItemScheduleInfo.LocalCurrencyCode;
        }

        if (CurrentGLDataRecurringItemScheduleInfo != null)
        {
            lblLocalCurrencyCodeValue.Text = CurrentGLDataRecurringItemScheduleInfo.LocalCurrencyCode;
        }
        BindStartInterval();
        UpdateRCCYAmountAndLabels();
    }

    private void BindStartInterval()
    {
        if (CurrentGLDataRecurringItemScheduleInfo != null)
        {
            List<ReconciliationPeriodInfo> oRecPeriodInfoList = (List<ReconciliationPeriodInfo>)Helper.DeepClone(CacheHelper.GetAllReconciliationPeriods(null));

            SetRecPeriodsAll(oRecPeriodInfoList);

            for (int i = oRecPeriodInfoList.Count - 1; i >= 0; i--)
            {                
                if (oRecPeriodInfoList[i].PeriodEndDate < SessionHelper.CurrentReconciliationPeriodEndDate)
                    oRecPeriodInfoList.RemoveAt(i);
            }

            this.ddlStartInterval.DataSource = oRecPeriodInfoList;
            this.ddlStartInterval.DataTextField = "PeriodEndDate";
            this.ddlStartInterval.DataValueField = "ReconciliationPeriodID";
            this.ddlStartInterval.DataTextFormatString = "{0:d}";
            this.ddlStartInterval.DataBind();
            ListControlHelper.AddListItemForSelectOne(ddlStartInterval);
            if (this.Mode == QueryStringConstants.EDIT)
            {
                if (this.ddlStartInterval.Items.FindByValue(CurrentGLDataRecurringItemScheduleInfo.StartIntervalRecPeriodID.ToString()) != null)
                    this.ddlStartInterval.SelectedValue = CurrentGLDataRecurringItemScheduleInfo.StartIntervalRecPeriodID.ToString();
            }
        }
    }

    private void SetRecPeriodsAll(List<ReconciliationPeriodInfo> oRecPeriodInfoList)
    {
        var jss = new System.Web.Script.Serialization.JavaScriptSerializer();
        _recPeriodsAll = jss.Serialize(from item in oRecPeriodInfoList
            where item.PeriodEndDate != null
            select ((DateTime) item.PeriodEndDate).ToShortDateString());
    }

    private void SetCompanyCabalityInfo()
    {
        this._IsMultiCurrencyEnabled = Helper.IsCapabilityActivatedForRecPeriodID(ARTEnums.Capability.MultiCurrency, SessionHelper.CurrentReconciliationPeriodID.Value, true);
    }

    private void SetModeForFormView()
    {

        bool isReadOnlyMode = (this.Mode == QueryStringConstants.READ_ONLY);
        bool isEditMode = (this.Mode == QueryStringConstants.INSERT || this.Mode == QueryStringConstants.EDIT);
        bool isOnlyEdit = (this.Mode == QueryStringConstants.EDIT);
        bool isStartIntervalSelected = Convert.ToInt32(ddlStartInterval.SelectedValue) > 0;

        trCloseDate.Visible = this.IsForwardedItem.Value || isOnlyEdit;
        trCloseDateBlankRow.Visible = this.IsForwardedItem.Value || isOnlyEdit;

        txtCurrentInterval.Enabled = optOtherInterval.Checked && !isStartIntervalSelected;
        txtTotalIntervals.Enabled = optOtherInterval.Checked && isEditMode;

        rfvTotalIntervals.Enabled = optOtherInterval.Checked;
        cmpvTotalIntervals.Enabled = optOtherInterval.Checked;
        cvTotalIntervals.Enabled = optOtherInterval.Checked;
        cvStartInterval.Enabled = optOtherInterval.Checked;

        rfvCurrentInterval.Enabled = optOtherInterval.Checked && !isStartIntervalSelected;
        cmpvCurrentInterval.Enabled = optOtherInterval.Checked && !isStartIntervalSelected;
        cvCurrentInterval.Enabled = optOtherInterval.Checked && !isStartIntervalSelected;
        //revCurrentIntervals.Enabled = optOtherInterval.Checked && !isStartIntervalSelected;

        calScheduleBeginDate.Enabled = isEditMode && !isStartIntervalSelected;
        calScheduleEndDate.Enabled = isEditMode;
        chkDontShowOnRecForm.Enabled = isEditMode;
        if (optOtherInterval.Checked)
        {
            //calScheduleBeginDate.Attributes.Add("disabled", "disabled");
            //calScheduleEndDate.Attributes.Add("disabled", "disabled");
        }
        if (optDailyInterval.Checked)
        {
            ddlStartInterval.Attributes.Add("disabled", "disabled");
        }
        rfvScheduleBeginDate.Enabled = isEditMode && optDailyInterval.Checked;
        rfvScheduleEndDate.Enabled = isEditMode && optDailyInterval.Checked;

        lblCloseDateValue.Visible = isReadOnlyMode;
        calCloseDate.Visible = (this.IsForwardedItem.Value && isEditMode) || (isOnlyEdit && isEditMode);

        lblCommentsValue.Visible = isReadOnlyMode;
        txtComments.Visible = isEditMode;
        lblLocalCurrencyCodeValue.Visible = isReadOnlyMode || this.IsForwardedItem.Value;
        ddlLocalCurrency.Visible = isEditMode && !this.IsForwardedItem.Value;
        hlOverrideExchangeRate.Visible = isEditMode && !this.IsForwardedItem.Value && _IsMultiCurrencyEnabled;
        optDailyInterval.Enabled = isEditMode && !this.IsForwardedItem.Value;
        optOtherInterval.Enabled = isEditMode && !this.IsForwardedItem.Value;

        if (optOtherInterval.Checked)
            trOtherInterval.Style.Add(HtmlTextWriterStyle.Display, "");
        else
            trOtherInterval.Style.Add(HtmlTextWriterStyle.Display, "none");

        if (isEditMode)
            txtTotalIntervals.Style.Add(HtmlTextWriterStyle.Display, "inline");
        else
            txtTotalIntervals.Style.Add(HtmlTextWriterStyle.Display, "none");
        lblTotalIntervalValue.Visible = isReadOnlyMode;

        if (isEditMode
            && (!this.IsForwardedItem.Value
                || ucScheduleIntervalDetails.StartPeriodEndDate >= SessionHelper.CurrentReconciliationPeriodEndDate)
            )
        {
            ddlStartInterval.Style.Add(HtmlTextWriterStyle.Display, "inline");
            txtCurrentInterval.Style.Add(HtmlTextWriterStyle.Display, "inline");
            lblStartIntervalRecPeriodDateValue.Visible = false;
            lblCurrentIntervalValue.Visible = false;
            calScheduleBeginDate.Visible = true;
            lblScheduleBeginDateValue.Visible = false;
        }
        else
        {
            ddlStartInterval.Style.Add(HtmlTextWriterStyle.Display, "none");
            txtCurrentInterval.Style.Add(HtmlTextWriterStyle.Display, "none");
            lblStartIntervalRecPeriodDateValue.Visible = true;
            lblCurrentIntervalValue.Visible = true;
            calScheduleBeginDate.Visible = false;
            lblScheduleBeginDateValue.Visible = true;
        }

        cvCloseDate.Enabled = this.IsForwardedItem.Value || isOnlyEdit;
        cvCompareCloseDateWithOpenDate.Enabled = this.IsForwardedItem.Value || isOnlyEdit;
        cvCompareCloseDateWithCurrentDate.Enabled = this.IsForwardedItem.Value || isOnlyEdit;

        lblOriginalAmountLCCYValue.Visible = isReadOnlyMode;
        txtOriginalAmountLCCY.Visible = isEditMode;

        lblOpenDateValue.Visible = isReadOnlyMode;
        calOpenDate.Visible = isEditMode;

        rfvOpenDate.Enabled = !this.IsForwardedItem.Value;
        rfvOriginalAmount.Enabled = isEditMode;
        cvOriginalAmount.Enabled = isEditMode;

        btnUpdate.Visible = isEditMode;
        btnRecalculateSchedule.Enabled = isEditMode;

        calScheduleEndDate.Visible = isEditMode;
        lblScheduleEndDateValue.Visible = isReadOnlyMode;

        txtScheduleName.Visible = isEditMode;
        lblScheduleNameValue.Visible = isReadOnlyMode;

        if (this._IsMultiCurrencyEnabled)
        {
            trExchangeRate.Visible = true;
            trExchangeRateBlankRow.Visible = true;
        }
        else
        {
            trExchangeRate.Visible = false;
            trExchangeRateBlankRow.Visible = false;
        }
    }

    private void GetQueryStringValues()
    {
        if (Request.QueryString[QueryStringConstants.GL_RECONCILIATION_ITEM_INPUT_ID] != null)
            this._GLDataRecurringItemScheduleID = Convert.ToInt32(Request.QueryString[QueryStringConstants.GL_RECONCILIATION_ITEM_INPUT_ID]);
        ucScheduleIntervalDetails.Mode = this.Mode;
    }
    private GLDataRecurringItemScheduleInfo GetGLReconciliationItemInputInfo()
    {
        // Only In case of new Schedule
        if (CurrentGLDataRecurringItemScheduleInfo.GLDataRecurringItemScheduleID.GetValueOrDefault() == 0)
        {
            CurrentGLDataRecurringItemScheduleInfo.GLDataID = this.GLDataID.Value;
            CurrentGLDataRecurringItemScheduleInfo.DateAdded = DateTime.Now;
            CurrentGLDataRecurringItemScheduleInfo.AddedBy = SessionHelper.CurrentUserLoginID;
            CurrentGLDataRecurringItemScheduleInfo.AddedByUserID = SessionHelper.CurrentUserID;
        }
        else
        {
            // Audit Fields
            CurrentGLDataRecurringItemScheduleInfo.RevisedBy = SessionHelper.CurrentUserLoginID;
            CurrentGLDataRecurringItemScheduleInfo.DateRevised = DateTime.Now;
        }
        // Do not update in Carry Forward Period
        if (!CurrentGLDataRecurringItemScheduleInfo.PrevRecPeriodID.HasValue)
        {
            if (!string.IsNullOrEmpty(calScheduleBeginDate.Text))
                CurrentGLDataRecurringItemScheduleInfo.ScheduleBeginDate = Convert.ToDateTime(calScheduleBeginDate.Text);
            else
                CurrentGLDataRecurringItemScheduleInfo.ScheduleBeginDate = null;
            CurrentGLDataRecurringItemScheduleInfo.LocalCurrencyCode = ddlLocalCurrency.SelectedValue;
            CurrentGLDataRecurringItemScheduleInfo.CalculationFrequencyID = (optDailyInterval.Checked ? (short)ARTEnums.CalculationFrequency.DailyInterval : (short)ARTEnums.CalculationFrequency.OtherInterval);
        }

        // Set Calculation Frequency & Other Intervals
        int? startInterval = Convert.ToInt32(ddlStartInterval.SelectedValue);
        if (startInterval.GetValueOrDefault() < 1)
            startInterval = null;
        if (CurrentGLDataRecurringItemScheduleInfo.CalculationFrequencyID == (short)ARTEnums.CalculationFrequency.OtherInterval)
        {
            int currentInterval;
            int.TryParse(txtCurrentInterval.Text, out currentInterval);
            if (currentInterval > 0)
                CurrentGLDataRecurringItemScheduleInfo.CurrentInterval = currentInterval;
            else
                CurrentGLDataRecurringItemScheduleInfo.CurrentInterval = null;
            CurrentGLDataRecurringItemScheduleInfo.StartIntervalRecPeriodID = startInterval;
        }
        else
        {
            CurrentGLDataRecurringItemScheduleInfo.CurrentInterval = null;
            CurrentGLDataRecurringItemScheduleInfo.StartIntervalRecPeriodID = null;
        }

        if (CurrentGLDataRecurringItemScheduleInfo.CalculationFrequencyID == (short)ARTEnums.CalculationFrequency.OtherInterval)
        {
            int totalInterval;
            int.TryParse(txtTotalIntervals.Text, out totalInterval);
            CurrentGLDataRecurringItemScheduleInfo.TotalIntervals = totalInterval;
        }
        else
        {
            CurrentGLDataRecurringItemScheduleInfo.TotalIntervals = null;
        }

        CurrentGLDataRecurringItemScheduleInfo.ReconciliationCategoryTypeID = this.RecCategoryType.Value;
        CurrentGLDataRecurringItemScheduleInfo.RecCategoryTypeID = this.RecCategoryType.Value;
        CurrentGLDataRecurringItemScheduleInfo.RecordSourceTypeID = (short)ARTEnums.RecordSourceType.UI;
        CurrentGLDataRecurringItemScheduleInfo.RecordSourceID = null;
        CurrentGLDataRecurringItemScheduleInfo.ScheduleAmount = Convert.ToDecimal(txtOriginalAmountLCCY.Text);
        CurrentGLDataRecurringItemScheduleInfo.IsActive = true;
        if (!string.IsNullOrEmpty(calScheduleEndDate.Text))
            CurrentGLDataRecurringItemScheduleInfo.ScheduleEndDate = Convert.ToDateTime(calScheduleEndDate.Text);
        else
            CurrentGLDataRecurringItemScheduleInfo.ScheduleEndDate = null;
        CurrentGLDataRecurringItemScheduleInfo.ScheduleName = txtScheduleName.Text;
        CurrentGLDataRecurringItemScheduleInfo.Comments = txtComments.Text;
        CurrentGLDataRecurringItemScheduleInfo.OpenDate = Convert.ToDateTime(calOpenDate.Text);
        if (IsExchangeRateOverridden)
        {
            if (this.ExRateLCCYtoBCCY.GetValueOrDefault() != 0)
                CurrentGLDataRecurringItemScheduleInfo.ExRateLCCYtoBCCY = this.ExRateLCCYtoBCCY;
            if (this.ExRateLCCYtoRCCY.GetValueOrDefault() != 0)
                CurrentGLDataRecurringItemScheduleInfo.ExRateLCCYtoRCCY = this.ExRateLCCYtoRCCY;
        }
        else
        {
            CurrentGLDataRecurringItemScheduleInfo.ExRateLCCYtoBCCY = null;
            CurrentGLDataRecurringItemScheduleInfo.ExRateLCCYtoRCCY = null;
        }
        CurrentGLDataRecurringItemScheduleInfo.IgnoreInCalculation = chkDontShowOnRecForm.Checked;
        SetExchangeRates();

        CurrentGLDataRecurringItemScheduleInfo.GLDataRecurringItemScheduleIntervalDetailInfoList = ucScheduleIntervalDetails.UpdateIntervalDetailsToSource();
        UpdateRCCYAmountAndLabels();
        return CurrentGLDataRecurringItemScheduleInfo;
    }
    private void UpdateRCCYAmountAndLabels()
    {
        ucScheduleIntervalDetails.UpdateTotals();
        CurrentGLDataRecurringItemScheduleInfo.RecPeriodAmountLocalCurrency = ucScheduleIntervalDetails.TotalConsumedAmount;
        CurrentGLDataRecurringItemScheduleInfo.BalanceLocalCurrency = CurrentGLDataRecurringItemScheduleInfo.ScheduleAmount - ucScheduleIntervalDetails.TotalConsumedAmount;
        RecHelper.RecalculateRecItemScheduleAmount(CurrentGLDataRecurringItemScheduleInfo, this.CurrentBCCY, this.NetAccountID);

        lblOriginalAmountRCCYValue.Text = Helper.GetDisplayDecimalValue(CurrentGLDataRecurringItemScheduleInfo.ScheduleAmountReportingCurrency);
        lblTotalAccruedAmountRCCYValue.Text = Helper.GetDisplayDecimalValue(CurrentGLDataRecurringItemScheduleInfo.RecPeriodAmountReportingCurrency);
        lblToBeAccruedAmountRCCYValue.Text = Helper.GetDisplayDecimalValue(CurrentGLDataRecurringItemScheduleInfo.BalanceReportingCurrency);

        decimal? currentRCCY = null;
        if (CurrentGLDataRecurringItemScheduleInfo.ExRateLCCYtoRCCY.HasValue)
            currentRCCY = SharedRecItemHelper.ConvertAmount(ucScheduleIntervalDetails.CurrentConsumedAmount, CurrentGLDataRecurringItemScheduleInfo.ExRateLCCYtoRCCY);
        else
            currentRCCY = RecHelper.ConvertCurrency(CurrentGLDataRecurringItemScheduleInfo.LocalCurrencyCode, SessionHelper.ReportingCurrencyCode, SessionHelper.CurrentReconciliationPeriodID, ucScheduleIntervalDetails.CurrentConsumedAmount);
        lblCurrentAmountRCCYValue.Text = Helper.GetDisplayDecimalValue(currentRCCY);
        lblOverriddenExRateBCCYValue.Text = Helper.GetDisplayExchangeRateValue(ExRateLCCYtoBCCY);
        lblOverriddenExRateRCCYValue.Text = Helper.GetDisplayExchangeRateValue(ExRateLCCYtoRCCY);
    }
    #endregion

    #region Other Methods
    protected void SetExchangeRateAndRecalculateAmount()
    {
        SetExchangeRates();
        CurrentGLDataRecurringItemScheduleInfo.LocalCurrencyCode = ddlLocalCurrency.SelectedValue;
        btnRecalculateSchedule_Click(null, null);
    }
    public string SetOverrideExchangeRateURL()
    {
        return Page.ResolveUrl(Helper.GetOverrideExchangeRateURLForRecItemInput(this.AccountID, this.NetAccountID, this.ExRateLCCYtoBCCY, this.ExRateLCCYtoRCCY));
    }

    public string SetDocumentUploadURL()
    {
        string windowName;
        string url = Helper.SetDocumentUploadURLForRecItemInput(this.GLDataID, this._GLDataRecurringItemScheduleID, this.AccountID, this.NetAccountID, (this.Mode == QueryStringConstants.READ_ONLY), Request.Url.ToString(), out windowName, this.IsForwardedItem.Value, WebEnums.RecordType.ScheduleRecItem);
        return url;
    }

    #endregion

  
}
