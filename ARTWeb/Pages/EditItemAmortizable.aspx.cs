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
using SkyStem.ART.Web.Data;
using SkyStem.ART.Web.Utility;
using SkyStem.Language.LanguageUtility;
using SkyStem.ART.Client.IServices;
using SkyStem.ART.Client.Model;
using System.Collections.Generic;
using SkyStem.ART.Client.Data;
using System.Text;
using Telerik.Web.UI;
using SkyStem.Library.Controls.WebControls;
using SkyStem.Library.Controls.TelerikWebControls;
using SkyStem.ART.Shared.Utility;

public partial class Pages_EditItemAmortizable : PopupPageBaseRecItem
{
    #region Variables & Constants
    private long _GLDataRecurringItemScheduleID;
    private bool _IsMultiCurrencyEnabled;
    #endregion


    #region Properties

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
            //PopupHelper.SetPageTitle(this, 1525);
            PopupHelper.ShowInputRequirementSection(this, 2043, 2069, 2350, 2255, 2435);
            //this.RecCategoryType = Convert.ToInt16(Request.QueryString[QueryStringConstants.REC_CATEGORY_TYPE_ID].ToString());
            //_RecCategory = Convert.ToInt16(Request.QueryString[QueryStringConstants.REC_CATEGORY_ID].ToString());
            int[] oLableIdCollection = new int[0];
            PopupHelper.SetPageTitle(this, this.RecCategory.Value, this.RecCategoryType.Value, oLableIdCollection);

            SetCompanyCabalityInfo();
            GetQueryStringValues();
            SetErrorMessagesForValidationControls();

            if (!IsPostBack)
            {
                lblOriginalAmountRCCYCode.Text = SessionHelper.ReportingCurrencyCode;
                lblTotalAmortizedAmountRCCYCode.Text = SessionHelper.ReportingCurrencyCode;
                lblToBeAmortizedAmountRCCYCode.Text = SessionHelper.ReportingCurrencyCode;
                lblCurrentAmountRCCYCode.Text = SessionHelper.ReportingCurrencyCode;
                lblOverriddenExRateBCCYCode.Text = this.CurrentBCCY;
                lblOverriddenExRateRCCYCode.Text = SessionHelper.ReportingCurrencyCode;

                PopulateItemsOnPage();
            }

            SetExchangeRates();
            this.lblInputFormRecPeriodValue.Text = Helper.GetDisplayDate(SessionHelper.CurrentReconciliationPeriodEndDate);
            //lblBCCYCode.Text = SessionHelper.BaseCurrencyCode;
            //lblRCCYCode.Text = SessionHelper.ReportingCurrencyCode;
            ucAccountHierarchyDetailPopup.AccountID = this.AccountID;
            ucAccountHierarchyDetailPopup.NetAccountID = this.NetAccountID;
            hlDocument.NavigateUrl = "javascript:OpenRadWindowFromRadWindow('" + SetDocumentUploadURL() + "', '480', '800');";

            ucExchangeRate.BCCYCode = this.CurrentBCCY;
            ucExchangeRate.RCCYCode = SessionHelper.ReportingCurrencyCode;
            optDailyInterval.InputAttributes.Add("onclick", "EnableDisableIntervalControls();");
            optOtherInterval.InputAttributes.Add("onclick", "EnableDisableIntervalControls();");
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
                //else if (this._IsForwardedItem)
                //{
                //    SaveCloseDateForForwardedItems();
                //}
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
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        SessionHelper.ClearSession(SessionConstants.ATTACHMENTS);
        string script = PopupHelper.GetScriptForClosingRadWindow();

        ClientScript.RegisterClientScriptBlock(this.GetType(), "CloseWindow", script);
    }

    protected void btnRecalculateSchedule_Click(object sender, EventArgs e)
    {
        decimal amt;
        Decimal.TryParse(txtOriginalAmountLCCY.Text, out amt);
        if (optDailyInterval.Checked)
        {
            DateTime startDT, endDT;
            if (this.Mode == QueryStringConstants.EDIT)
                DateTime.TryParse(lblScheduleBeginDateValue.Text, out startDT);
            else
                DateTime.TryParse(calScheduleBeginDate.Text, out startDT);
            DateTime.TryParse(calScheduleEndDate.Text, out endDT);
            ucScheduleIntervalDetails.RecalculateSchedule(amt, startDT, endDT);
            //RecalculateScheduleDaysWise();
        }
        else
        {
            int totalIntervals, currentInterval;
            if (this.Mode == QueryStringConstants.EDIT)
                Int32.TryParse(txtCurrentInterval.Text, out currentInterval);
            else
                Int32.TryParse(txtCurrentInterval.Text, out currentInterval);
            Int32.TryParse(txtTotalIntervals.Text, out totalIntervals);
            ucScheduleIntervalDetails.RecalculateSchedule(amt, totalIntervals, 0, currentInterval, CurrentGLDataRecurringItemScheduleInfo.PrevRecPeriodID);
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

    #endregion

    #region Validation Control Events
    protected void cvOriginalAmount_ServerValidate(object source, ServerValidateEventArgs args)
    {
        try
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
                    if (Math.Abs(ucScheduleIntervalDetails.OriginalAmount) < Math.Abs(prevBalance))
                    {
                        //MasterPageBase oMasterPageBase = (MasterPageBase)this.Master;
                        //oMasterPageBase.ShowErrorMessage(cv.LabelID);
                        args.IsValid = false;
                        cvOriginalAmount.ErrorMessage = string.Format(LanguageUtil.GetValue(5000108), LanguageUtil.GetValue(2433) + " (" + Helper.GetDisplayDecimalValue(prevBalance) + ")");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            PopupHelper.ShowErrorMessage(this, ex);
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
    protected void cvLocalCurrency_ServerValidate(object source, ServerValidateEventArgs args)
    {
        if (ddlLocalCurrency.SelectedItem.Value == WebConstants.SELECT_ONE)//"select one"
        {
            args.IsValid = false;
        }
    }
    #endregion

    #region Private Methods
    private void SetErrorMessagesForValidationControls()
    {
        // Validate Amount
        cvOriginalAmount.ErrorMessage = LanguageUtil.GetValue(5000093);

        // Mandatory Field Validations
        rfvLocalCurrency.InitialValue = WebConstants.SELECT_ONE;
        rfvLocalCurrency.ErrorMessage = Helper.GetErrorMessage(WebEnums.FieldType.MandatoryField, this.lblLocalCurrencyCode.LabelID);
        rfvOriginalAmount.ErrorMessage = Helper.GetErrorMessage(WebEnums.FieldType.MandatoryField, this.lblOriginalAmountLCCY.LabelID);
        rfvOpenDate.ErrorMessage = Helper.GetErrorMessage(WebEnums.FieldType.MandatoryField, this.lblOpenDate.LabelID);
        rfvScheduleBeginDate.ErrorMessage = Helper.GetErrorMessage(WebEnums.FieldType.MandatoryField, this.lblScheduleBeginDate.LabelID);
        rfvScheduleEndDate.ErrorMessage = Helper.GetErrorMessage(WebEnums.FieldType.MandatoryField, this.lblScheduleEndDate.LabelID);
        this.txtScheduleName.ErrorMessage = Helper.GetErrorMessage(WebEnums.FieldType.MandatoryField, this.lblScheduleName.LabelID);

        // Validations for Valid Date
        cvOpenDate.ErrorMessage = Helper.GetErrorMessage(WebEnums.FieldType.InvalidDate, lblOpenDate.LabelID);
        cvCloseDate.ErrorMessage = Helper.GetErrorMessage(WebEnums.FieldType.InvalidDate, lblCloseDate.LabelID);
        cvScheduleBeginDate.ErrorMessage = Helper.GetErrorMessage(WebEnums.FieldType.InvalidDate, lblScheduleBeginDate.LabelID);
        cvScheduleEndDate.ErrorMessage = Helper.GetErrorMessage(WebEnums.FieldType.InvalidDate, lblScheduleEndDate.LabelID);

        // Date Comparison Validations
        cvCompareOpenDateWithCurrentDate.ErrorMessage = Helper.GetErrorMessage(WebEnums.FieldType.DateCompareField, this.lblOpenDate.LabelID, 2062);
        cvCompareCloseDateWithCurrentDate.ErrorMessage = Helper.GetErrorMessage(WebEnums.FieldType.DateCompareField, this.lblCloseDate.LabelID, 2062);
        cvCompareCloseDateWithOpenDate.ErrorMessage = Helper.GetErrorMessage(WebEnums.FieldType.DateCompareFieldGreaterThan, this.lblCloseDate.LabelID, this.lblOpenDate.LabelID);

        cvCompareScheduleBeginDateWithScheduleEndDate.ErrorMessage = Helper.GetErrorMessage(WebEnums.FieldType.DateCompareField, this.lblScheduleEndDate.LabelID, this.lblScheduleBeginDate.LabelID);
        cvCompareOpenDateWithScheduleEndDate.ErrorMessage = Helper.GetErrorMessage(WebEnums.FieldType.DateCompareField, this.lblOpenDate.LabelID, this.lblScheduleEndDate.LabelID);
        if (this.RecCategoryType == (short)WebEnums.RecCategoryType.Accrual_SupportingDetail_RecurringAccrualSchedule)
            cvCompareRecPeriodEndDateWithScheduleDates.ErrorMessage = LanguageUtil.GetValue(2060);
        else
            cvCompareRecPeriodEndDateWithScheduleDates.ErrorMessage = LanguageUtil.GetValue(2069);
    }

    private void SetExchangeRates()
    {
        if (ddlLocalCurrency.SelectedValue != WebConstants.SELECT_ONE)
        {
            ucExchangeRate.LCCYCode = ddlLocalCurrency.SelectedValue;
            if (!IsExchangeRateOverridden)
            {
                this.ExRateLCCYtoBCCY = CacheHelper.GetExchangeRate(ddlLocalCurrency.SelectedValue, this.CurrentBCCY, SessionHelper.CurrentReconciliationPeriodID);
                this.ExRateLCCYtoRCCY = CacheHelper.GetExchangeRate(ddlLocalCurrency.SelectedValue, SessionHelper.ReportingCurrencyCode, SessionHelper.CurrentReconciliationPeriodID);
            }
            if (!CurrentGLDataRecurringItemScheduleInfo.PrevRecPeriodID.HasValue)
                CurrentGLDataRecurringItemScheduleInfo.LocalCurrencyCode = ucExchangeRate.LCCYCode;
        }
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

        if (this.Mode == QueryStringConstants.EDIT || this.Mode == QueryStringConstants.READ_ONLY)
        {
            IGLDataRecItemSchedule oGLDataRecItemScheduleClient = RemotingHelper.GetGLDataRecItemScheduleObject();
            CurrentGLDataRecurringItemScheduleInfo = oGLDataRecItemScheduleClient.GetGLDataRecurringItemScheduleInfo(_GLDataRecurringItemScheduleID, Helper.GetAppUserInfo());
            if (this._GLDataRecurringItemScheduleID > 0)
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
                hdnTotalAmortizedAmountRCCYValue.Value = CurrentGLDataRecurringItemScheduleInfo.RecPeriodAmountReportingCurrency.Value.ToString();

                lblOriginalAmountRCCYValue.Text = Helper.GetDisplayDecimalValue(CurrentGLDataRecurringItemScheduleInfo.ScheduleAmountReportingCurrency);
                lblTotalAmortizedAmountRCCYValue.Text = Helper.GetDisplayDecimalValue(CurrentGLDataRecurringItemScheduleInfo.RecPeriodAmountReportingCurrency);
                lblToBeAmortizedAmountRCCYValue.Text = Helper.GetDisplayDecimalValue(CurrentGLDataRecurringItemScheduleInfo.BalanceReportingCurrency);
                lblRecItemNumberValue.Text = Helper.GetDisplayStringValue(CurrentGLDataRecurringItemScheduleInfo.RecItemNumber);
                lblMatchSetRefNoValue.Text = Helper.GetDisplayStringValue(CurrentGLDataRecurringItemScheduleInfo.MatchSetRefNumber);

                //Calculation FrequencyId and Intervals details
                if (CurrentGLDataRecurringItemScheduleInfo.TotalIntervals != null)
                {
                    txtTotalIntervals.Text = Helper.GetDisplayIntegerValue(CurrentGLDataRecurringItemScheduleInfo.TotalIntervals.Value);
                }

                if (CurrentGLDataRecurringItemScheduleInfo.CurrentInterval != null)
                {
                    txtCurrentInterval.Text = Helper.GetDisplayIntegerValue(CurrentGLDataRecurringItemScheduleInfo.CurrentInterval.Value);
                    lblCurrentIntervalValue.Text = Helper.GetDisplayIntegerValue(CurrentGLDataRecurringItemScheduleInfo.CurrentInterval.Value);
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

        ucScheduleIntervalDetails.RecCategoryType = (short)this.RecCategoryType;
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
        UpdateRCCYAmountAndLabels();
    }

    private void SetCompanyCabalityInfo()
    {
        List<CompanyCapabilityInfo> CompanyCapabilityInfoCollection = SessionHelper.GetCompanyCapabilityCollectionForCurrentRecPeriod();
        this._IsMultiCurrencyEnabled = (from capability in CompanyCapabilityInfoCollection
                                        where capability.CapabilityID.HasValue
                                        && capability.IsActivated.HasValue
                                        && capability.CapabilityID.Value == (short)ARTEnums.Capability.MultiCurrency
                                        select capability.IsActivated.Value).FirstOrDefault();
    }

    private void SetModeForFormView()
    {

        bool isReadOnlyMode = (this.Mode == QueryStringConstants.READ_ONLY);
        bool isEditMode = (this.Mode == QueryStringConstants.INSERT || this.Mode == QueryStringConstants.EDIT);
        bool isOnlyEdit = (this.Mode == QueryStringConstants.EDIT);

        trCloseDate.Visible = this.IsForwardedItem.Value || isOnlyEdit;
        trCloseDateBlankRow.Visible = this.IsForwardedItem.Value || isOnlyEdit;

        txtCurrentInterval.Enabled = optOtherInterval.Checked;
        txtTotalIntervals.Enabled = optOtherInterval.Checked;

        rfvTotalIntervals.Enabled = optOtherInterval.Checked;
        cmpvTotalIntervals.Enabled = optOtherInterval.Checked;
        cvTotalIntervals.Enabled = optOtherInterval.Checked;

        rfvCurrentInterval.Enabled = optOtherInterval.Checked;
        cmpvCurrentInterval.Enabled = optOtherInterval.Checked;
        cvCurrentInterval.Enabled = optOtherInterval.Checked;
        //revCurrentIntervals.Enabled = optOtherInterval.Checked;

        calScheduleBeginDate.Enabled = isEditMode;
        calScheduleEndDate.Enabled = isEditMode;

        if (optOtherInterval.Checked)
        {
            //calScheduleBeginDate.Attributes.Add("disabled", "disabled");
            //calScheduleEndDate.Attributes.Add("disabled", "disabled");
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
        if (isEditMode && !this.IsForwardedItem.Value)
            txtCurrentInterval.Style.Add(HtmlTextWriterStyle.Display, "inline");
        else
            txtCurrentInterval.Style.Add(HtmlTextWriterStyle.Display, "none");
        lblCurrentIntervalValue.Visible = isReadOnlyMode || this.IsForwardedItem.Value;
        calScheduleBeginDate.Visible = isEditMode && !this.IsForwardedItem.Value;
        lblScheduleBeginDateValue.Visible = isReadOnlyMode || this.IsForwardedItem.Value;

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
        //if (!string.IsNullOrEmpty(Request.QueryString[QueryStringConstants.ACCOUNT_ID]))
        //    this.AccountID = Convert.ToInt32(Request.QueryString[QueryStringConstants.ACCOUNT_ID]);

        //if (!string.IsNullOrEmpty(Request.QueryString[QueryStringConstants.NETACCOUNT_ID]))
        //    this.NetAccountID = Convert.ToInt32(Request.QueryString[QueryStringConstants.NETACCOUNT_ID]);

        //if (Request.QueryString[QueryStringConstants.GLDATA_ID] != null)
        //    this.GLDataID = Convert.ToInt32(Request.QueryString[QueryStringConstants.GLDATA_ID]);

        //if (Request.QueryString[QueryStringConstants.REC_CATEGORY_TYPE_ID] != null)
        //    this.RecCategoryType = Convert.ToInt16(Request.QueryString[QueryStringConstants.REC_CATEGORY_TYPE_ID]);

        //if (Request.QueryString[QueryStringConstants.MODE] != null)
        //    this.Mode = Request.QueryString[QueryStringConstants.MODE];

        if (Request.QueryString[QueryStringConstants.GL_RECONCILIATION_ITEM_INPUT_ID] != null)
            this._GLDataRecurringItemScheduleID = Convert.ToInt32(Request.QueryString[QueryStringConstants.GL_RECONCILIATION_ITEM_INPUT_ID]);

        //if (Request.QueryString[QueryStringConstants.IS_FORWARDED_ITEM] != null)
        //    this._IsForwardedItem = Convert.ToBoolean(Convert.ToInt32(Request.QueryString[QueryStringConstants.IS_FORWARDED_ITEM]));

        //if (Request.QueryString[QueryStringConstants.PARENT_HIDDEN_FIELD] != null)
        //    this._ParentHiddenField = Request.QueryString[QueryStringConstants.PARENT_HIDDEN_FIELD];
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
            // Set Calculation Frequency & Other Intervals
            if (CurrentGLDataRecurringItemScheduleInfo.CalculationFrequencyID == (short)ARTEnums.CalculationFrequency.OtherInterval)
            {
                int currentInterval;
                int.TryParse(txtCurrentInterval.Text, out currentInterval);
                CurrentGLDataRecurringItemScheduleInfo.CurrentInterval = currentInterval;
            }
            else
            {
                CurrentGLDataRecurringItemScheduleInfo.CurrentInterval = null;
            }
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

        CurrentGLDataRecurringItemScheduleInfo.ReconciliationCategoryTypeID = this.RecCategoryType;
        CurrentGLDataRecurringItemScheduleInfo.RecCategoryTypeID = this.RecCategoryType;
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
        lblTotalAmortizedAmountRCCYValue.Text = Helper.GetDisplayDecimalValue(CurrentGLDataRecurringItemScheduleInfo.RecPeriodAmountReportingCurrency);
        lblToBeAmortizedAmountRCCYValue.Text = Helper.GetDisplayDecimalValue(CurrentGLDataRecurringItemScheduleInfo.BalanceReportingCurrency);

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

    protected void btnOvereideExchangeRate_Click(object sender, EventArgs e)
    {
        SetExchangeRateAndRecalculateAmount();
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
