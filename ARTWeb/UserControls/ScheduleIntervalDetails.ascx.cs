using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using SkyStem.Library.Controls.WebControls;
using SkyStem.ART.Client.Model;
using SkyStem.ART.Web.Utility;
using SkyStem.ART.Client.IServices;
using SkyStem.ART.Web.Data;
using SkyStem.Language.LanguageUtility;
using SkyStem.ART.Web.Classes;
using SkyStem.ART.Client.Data;
using SkyStem.ART.Shared.Utility;

namespace SkyStem.ART.Web.UserControls
{
    public partial class UserControls_ScheduleIntervalDetails : UserControlBase
    {
        #region Properties and Variables

        private string lblTotalAmountClientID;
        private string lblUnderOverAmtClientID;
        public decimal? consumedAmount = 0;
        public short RecCategoryType
        {
            get { return Convert.ToInt16(ViewState["RecCategoryType"]); }
            set { ViewState["RecCategoryType"] = value; }
        }
        public decimal OriginalAmount
        {
            get { return Convert.ToDecimal(ViewState["OriginalAmount"]); }
            set { ViewState["OriginalAmount"] = value; }
        }
        public DateTime? StartPeriodEndDate
        {
            get { return Convert.ToDateTime(ViewState["StartPeriodEndDate"]); }
            set { ViewState["StartPeriodEndDate"] = value; }
        }

        public string GridClientID
        {
            get { return rgScheduleIntervalDetails.ClientID; }
        }

        public string Mode { get; set; }

        public int? StartIntervalRecPeriodID { get; set; }
        /// <summary>
        /// Total of All Rec Periods
        /// </summary>
        public decimal GrandTotalAmount { get; set; }
        /// <summary>
        /// Consumed in Current Period Only
        /// </summary>
        public decimal CurrentConsumedAmount { get; set; }
        /// <summary>
        /// Consumed upto Current Period
        /// </summary>
        public decimal TotalConsumedAmount { get; set; }

        public List<GLDataRecurringItemScheduleIntervalDetailInfo> ScheduleIntervalDetails
        {
            get { return (List<GLDataRecurringItemScheduleIntervalDetailInfo>)Session[SessionConstants.CURRENT_GLDATA_ITEM_SCHEDULE_INTERVAL_DETAIL_INFO]; }
            set { Session[SessionConstants.CURRENT_GLDATA_ITEM_SCHEDULE_INTERVAL_DETAIL_INFO] = value; }
        }

        /// <summary>
        /// Java Script function name to call when amount is edited in grid
        /// </summary>
        protected string _OnScheduleAmountChanged = string.Empty;
        public string OnScheduleAmountChanged
        {
            get { return _OnScheduleAmountChanged; }
            set { _OnScheduleAmountChanged = value; }
        }

        #endregion

        #region Grid Events

        protected void rgScheduleIntervalDetails_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item.ItemType == GridItemType.Header)
            {
                consumedAmount = 0;
            }
            else if (e.Item.ItemType == GridItemType.Footer)
            {
                decimal? underOverAmount = OriginalAmount - consumedAmount.GetValueOrDefault();
                decimal absDiff = Math.Abs(OriginalAmount) - Math.Abs(consumedAmount.GetValueOrDefault());
                ExLabel lblOverUnderAmount = (ExLabel)e.Item.FindControl("lblOverUnderAmount");
                ExLabel lblUnderOverAmountValue = (ExLabel)e.Item.FindControl("lblUnderOverAmountValue");
                ExLabel lblTotalAmountValue = (ExLabel)e.Item.FindControl("lblTotalAmountValue");
               
                
                lblTotalAmountClientID = lblTotalAmountValue.ClientID;
                lblUnderOverAmtClientID = lblUnderOverAmountValue.ClientID;
                if (this.RecCategoryType == (short)WebEnums.RecCategoryType.Amortizable_SupportingDetail_RecurringAmortizableSchedule)
                {
                    lblOverUnderAmount.LabelID = 2354;
                    lblUnderOverAmountValue.Text = Helper.GetDecimalValueForTextBox(underOverAmount, TestConstant.DECIMAL_PLACES_FOR_TEXTBOX);
                    if (absDiff > 0)
                        lblUnderOverAmountValue.Text = "(" + lblUnderOverAmountValue.Text + ")";
                }
                else if (this.RecCategoryType == (short)WebEnums.RecCategoryType.Accrual_SupportingDetail_RecurringAccrualSchedule)
                {
                    lblOverUnderAmount.LabelID = 2355;
                    lblUnderOverAmountValue.Text = Helper.GetDecimalValueForTextBox(underOverAmount, TestConstant.DECIMAL_PLACES_FOR_TEXTBOX);
                    if (absDiff < 0)
                        lblUnderOverAmountValue.Text = "(" + lblUnderOverAmountValue.Text + ")";
                }
                lblTotalAmountValue.Text = Helper.GetDisplayDecimalValue(GrandTotalAmount);
            }

            if (e.Item.ItemType == GridItemType.Item || e.Item.ItemType == GridItemType.AlternatingItem)
            {
                ExLabel lblRecPeriodEndDate = (ExLabel)e.Item.FindControl("lblRecPeriodEndDate");
                ExTextBox txtIntervalAmount = (ExTextBox)e.Item.FindControl("txtIntervalAmount");
                ExLabel lblRecPeriodID = (ExLabel)e.Item.FindControl("lblRecPeriodID");
                HiddenField hdnRecPeriodCompareResult = (HiddenField)e.Item.FindControl("hdnRecPeriodCompareResult");
                HiddenField hdnSystemIntervalAmount = (HiddenField)e.Item.FindControl("hdnSystemIntervalAmount");
                ExLabel lblSystemIntervalAmount = (ExLabel)e.Item.FindControl("lblSystemIntervalAmount");

                //txtIntervalAmount.TextBox.Attributes.Add(WebConstants.ONBLUR, "UpdateTotals(" + OriginalAmount + "," + RecCategoryType + "," + TestConstant.DECIMAL_PLACES_FOR_TEXTBOX + ");");

                GLDataRecurringItemScheduleIntervalDetailInfo oGLDataRecurringItemScheduleIntervalDetailInfo = (GLDataRecurringItemScheduleIntervalDetailInfo)e.Item.DataItem;
                lblRecPeriodEndDate.Text = Helper.GetDisplayDate(oGLDataRecurringItemScheduleIntervalDetailInfo.PeriodEndDate);
                txtIntervalAmount.Text = Helper.GetDecimalValueForTextBox(oGLDataRecurringItemScheduleIntervalDetailInfo.IntervalAmount, TestConstant.DECIMAL_PLACES_FOR_TEXTBOX);
                lblRecPeriodID.Text = oGLDataRecurringItemScheduleIntervalDetailInfo.ReconciliationPeriodID.ToString();
                hdnSystemIntervalAmount.Value = Helper.GetDecimalValueForTextBox(oGLDataRecurringItemScheduleIntervalDetailInfo.SystemIntervalAmount, TestConstant.DECIMAL_PLACES_FOR_TEXTBOX);
                lblSystemIntervalAmount.Text = Helper.GetDecimalValueForTextBox(oGLDataRecurringItemScheduleIntervalDetailInfo.SystemIntervalAmount, TestConstant.DECIMAL_PLACES_FOR_TEXTBOX);
                ReconciliationPeriodInfo oReconciliationPeriodInfo = null;

                if (oGLDataRecurringItemScheduleIntervalDetailInfo.IsDisabled == true || this.Mode == QueryStringConstants.READ_ONLY)
                {
                    txtIntervalAmount.Enabled = false;
                }
                else
                {
                    IReconciliationPeriod oReconciliationPeriodClient = RemotingHelper.GetReconciliationPeriodObject();
                    oReconciliationPeriodInfo = oReconciliationPeriodClient.GetReconciliationPeriodInfoByRecPeriodID(oGLDataRecurringItemScheduleIntervalDetailInfo.ReconciliationPeriodID,null,SessionHelper.CurrentCompanyID, Helper.GetAppUserInfo());
                    if (oReconciliationPeriodInfo.PeriodEndDate < SessionHelper.CurrentReconciliationPeriodEndDate
                        || oReconciliationPeriodInfo.PeriodEndDate < StartPeriodEndDate
                        || (WebEnums.RecPeriodStatus)oReconciliationPeriodInfo.ReconciliationPeriodStatusID == WebEnums.RecPeriodStatus.Closed
                        || (WebEnums.RecPeriodStatus)oReconciliationPeriodInfo.ReconciliationPeriodStatusID == WebEnums.RecPeriodStatus.Skipped)
                    {
                        txtIntervalAmount.Enabled = false;
                    }
                }
                hdnRecPeriodCompareResult.Value = GetRecPeriodCompareResult(oGLDataRecurringItemScheduleIntervalDetailInfo.PeriodEndDate).ToString();

                decimal intervalAmount;
                decimal.TryParse(txtIntervalAmount.Text, out intervalAmount);
                consumedAmount += intervalAmount;
                decimal syatemIntervalAmount;
                decimal.TryParse(hdnSystemIntervalAmount.Value, out syatemIntervalAmount);
                if (intervalAmount != syatemIntervalAmount)
                    txtIntervalAmount.TextBox.ForeColor = System.Drawing.Color.Red;
                else
                    txtIntervalAmount.TextBox.ForeColor = System.Drawing.Color.Black;

            }
        }

        protected void rgScheduleIntervalDetails_PreRender(object sender, EventArgs e)
        {
            foreach (GridDataItem dataItem in rgScheduleIntervalDetails.MasterTableView.Items)
            {
                ExTextBox oExTextBox = dataItem["IntervalAmount"].FindControl("txtIntervalAmount") as ExTextBox;
                HiddenField hdnSystemIntervalAmount = dataItem["IntervalAmount"].FindControl("hdnSystemIntervalAmount") as HiddenField;

                oExTextBox.TextBox.Attributes.Add("onblur", "UpdateTotals(" + OriginalAmount.ToString() + "," + RecCategoryType.ToString() + ","
                    + ((int)TestConstant.DECIMAL_PLACES_FOR_TEXTBOX).ToString() + ",'" + oExTextBox.TextBox.ClientID
                    + "','" + lblTotalAmountClientID + "','" + lblUnderOverAmtClientID + "','" + hdnSystemIntervalAmount.ClientID + "')");
                //oExTextBox.TextBox.Attributes.Add("onfocus", "StoreInitialValue('" + oExTextBox.TextBox.ClientID + "')");
            }
        }

        #endregion

        #region Custom Functions

        /// <summary>
        /// Binds the data.
        /// </summary>
        public void BindData()
        {
            StartPeriodEndDate = SharedRecItemHelper.GetStartIntervalDateTime(ScheduleIntervalDetails, StartIntervalRecPeriodID);
            UpdateTotals();
            rgScheduleIntervalDetails.DataSource = ScheduleIntervalDetails;
            rgScheduleIntervalDetails.DataBind();
        }

        /// <summary>
        /// Recalculates the schedule.
        /// </summary>
        /// <param name="originalAmt">The original amt.</param>
        /// <param name="totalIntervals">The total intervals.</param>
        /// <param name="startInterval">The start interval.</param>
        /// <param name="currentInterval">The current interval.</param>
        /// <param name="prevRecPeriodID">The prev rec period ID.</param>
        public void RecalculateSchedule(decimal? originalAmt, int? totalIntervals, int? startInterval, int? currentInterval, int? prevRecPeriodID)
        {
            if (ScheduleIntervalDetails != null && ScheduleIntervalDetails.Count > 0 && originalAmt.HasValue && Math.Abs(originalAmt.Value) > 0
                && totalIntervals.HasValue && Math.Abs(totalIntervals.Value) > 0
                && ((currentInterval.HasValue && Math.Abs(currentInterval.Value) > 0)
                    || (startInterval.HasValue && Math.Abs(startInterval.Value) > 0)
                    ))
            {
                OriginalAmount = originalAmt.Value;
                StartIntervalRecPeriodID = startInterval;
                StartPeriodEndDate = SharedRecItemHelper.GetStartIntervalDateTime(ScheduleIntervalDetails, startInterval);
                SharedRecItemHelper.RecalculateScheduleOther(ScheduleIntervalDetails, SessionHelper.CurrentReconciliationPeriodEndDate, originalAmt, totalIntervals, startInterval, currentInterval, prevRecPeriodID);
                //DateTime? prevPeriodEndDate = GetPreviousPeriodEndDate();
                //// Get Used Balance
                //decimal? usedBalance = GetUsedBalanceUptoPreviousPeriod();
                //// if there is anything to distribute
                //if (Math.Abs(originalAmt.GetValueOrDefault()) > Math.Abs(usedBalance.GetValueOrDefault()))
                //{
                //    // Calculate adjusted balance
                //    decimal adjustedBalance = originalAmt.GetValueOrDefault() - usedBalance.GetValueOrDefault();
                //    // Get used Intervals
                //    int? usedIntervals = 0;
                //    if (prevPeriodEndDate >= StartPeriodEndDate)
                //        usedIntervals = GetUsedIntervalsUptoPrevioudPeriod(currentInterval, prevRecPeriodID);
                //    // Calculate adjusted intervals
                //    int adjustedTotalIntervals = totalIntervals.GetValueOrDefault() - usedIntervals.GetValueOrDefault();
                //    // If there is balance and intervals to distribute
                //    if (Math.Abs(adjustedBalance) > 0 && adjustedTotalIntervals > 0)
                //    {
                //        // Calculate interval amount
                //        decimal intervalAmt = Math.Round(adjustedBalance / adjustedTotalIntervals, TestConstant.DECIMAL_PLACES_FOR_MATH_ROUND);
                //        decimal periodAmt = 0;
                //        foreach (GLDataRecurringItemScheduleIntervalDetailInfo oItem in ScheduleIntervalDetails)
                //        {
                //            // Calculate only for current and future periods
                //            if (oItem.PeriodEndDate >= SessionHelper.CurrentReconciliationPeriodEndDate)
                //            {
                //                // Initialize with zero
                //                oItem.IntervalAmount = 0;

                //                if (oItem.PeriodEndDate >= StartPeriodEndDate)
                //                {
                //                    if (oItem.PeriodEndDate == StartPeriodEndDate)
                //                        currentInterval = 1;
                //                    // If schedule is created in current rec period and rec period in consideration is also current then only use current interval
                //                    if (!prevRecPeriodID.HasValue && oItem.PeriodEndDate == SessionHelper.CurrentReconciliationPeriodEndDate)
                //                    {
                //                        periodAmt = intervalAmt * currentInterval.Value;
                //                        // Added By Manoj:- Bug Fixed  adjust the remaining balance
                //                        adjustedTotalIntervals = adjustedTotalIntervals - (currentInterval.Value-1);
                //                    }
                //                    else
                //                        periodAmt = intervalAmt;

                //                    // if adjusted balance is greater than period amount
                //                    if (Math.Abs(adjustedBalance) >= Math.Abs(periodAmt))
                //                    {
                //                        // assgin period amount 
                //                        oItem.IntervalAmount = periodAmt;
                //                        // adjust the balance
                //                        adjustedBalance = adjustedBalance - periodAmt;
                //                        adjustedTotalIntervals--;
                //                        // Add remaining amount to last interval
                //                        if (adjustedTotalIntervals == 0)
                //                        {
                //                            oItem.IntervalAmount += adjustedBalance;
                //                            adjustedBalance = 0;
                //                        }
                //                    }
                //                    else
                //                    {
                //                        // assign anything left amount
                //                        oItem.IntervalAmount = adjustedBalance;
                //                        // make the adjusted balance to zero
                //                        adjustedBalance = 0;
                //                    }
                //                }
                //            }
                //            oItem.SystemIntervalAmount = oItem.IntervalAmount;
                //        }
                //    }
                //}
                BindData();
            }
        }

        /// <summary>
        /// Recalculates the schedule.
        /// </summary>
        /// <param name="originalAmt">The original amt.</param>
        /// <param name="scheduleBeginDate">The schedule begin date.</param>
        /// <param name="scheduleEndDate">The schedule end date.</param>
        public void RecalculateSchedule(decimal? originalAmt, DateTime? scheduleBeginDate, DateTime? scheduleEndDate)
        {
            if (ScheduleIntervalDetails != null && ScheduleIntervalDetails.Count > 0 &&
                originalAmt.HasValue && Math.Abs(originalAmt.Value) > 0 && scheduleEndDate.HasValue && scheduleEndDate.HasValue)
            {
                OriginalAmount = originalAmt.Value;
                SharedRecItemHelper.RecalculateScheduleDaily(ScheduleIntervalDetails, StartPeriodEndDate, SessionHelper.CurrentReconciliationPeriodEndDate, originalAmt, scheduleBeginDate, scheduleEndDate);
                // Get used balance upto previous period
                //decimal? usedBalance = GetUsedBalanceUptoPreviousPeriod();
                // If there is balance to distribute
                //if (Math.Abs(originalAmt.GetValueOrDefault()) > Math.Abs(usedBalance.GetValueOrDefault()))
                //{
                //    // Calculate Adjusted Balance
                //    decimal adjustedBalance = originalAmt.GetValueOrDefault() - usedBalance.GetValueOrDefault();
                //    decimal dailyAmt = 0;
                //    decimal periodAmt = 0;
                //    // Get Just Previous Period End Date
                //    DateTime? prevDate = GetPreviousPeriodEndDate();
                //    DateTime? startDT = scheduleBeginDate;
                //    int adjustedDays = 0;
                //    int periodDays = 0;
                //    // If schedule is created in Previous Periods then adjust the start date for calculation
                //    if (prevDate.HasValue && prevDate > startDT)
                //        startDT = prevDate.Value.AddDays(1);
                //    // Calculate adjusted days
                //    adjustedDays = 1 + (scheduleEndDate.Value - startDT.Value).Days;
                //    if (Math.Abs(adjustedBalance) > 0 && adjustedDays > 0)
                //    {
                //        // Calculate daily amount
                //        dailyAmt = Math.Round(adjustedBalance / adjustedDays, TestConstant.DECIMAL_PLACES_FOR_MATH_ROUND);
                //        foreach (GLDataRecurringItemScheduleIntervalDetailInfo oItem in ScheduleIntervalDetails)
                //        {
                //            // Calculate only for current and future periods
                //            if (oItem.PeriodEndDate >= SessionHelper.CurrentReconciliationPeriodEndDate)
                //            {
                //                // Initialize with zero
                //                oItem.IntervalAmount = 0;
                //                // calculate period days
                //                periodDays = (oItem.PeriodEndDate.Value - startDT.Value).Days + 1;
                //                // calculate period amount
                //                periodAmt = Math.Round(periodDays * dailyAmt, 2);
                //                // if adjusted balance is greater than period amount
                //                if (Math.Abs(adjustedBalance) >= Math.Abs(periodAmt))
                //                {
                //                    // assign period amount
                //                    oItem.IntervalAmount = periodAmt;
                //                    // adjust remaining balance
                //                    adjustedBalance = adjustedBalance - periodAmt;
                //                }
                //                else
                //                {
                //                    // assign anything left balance
                //                    oItem.IntervalAmount = adjustedBalance;
                //                    // make the balance to 0
                //                    adjustedBalance = 0;
                //                }
                //                // adjust start date for next period
                //                startDT = oItem.PeriodEndDate.Value.AddDays(1);
                //            }
                //            oItem.SystemIntervalAmount = oItem.IntervalAmount;
                //        }
                //    }
                //}
                BindData();
            }
        }


        public decimal? GetUsedBalanceUptoPreviousPeriod()
        {
            return SharedRecItemHelper.GetUsedBalanceUptoPreviousPeriod(ScheduleIntervalDetails, StartPeriodEndDate, SessionHelper.CurrentReconciliationPeriodEndDate);
        }

        /// <summary>
        /// -1 : When Rec Period End Date is Lesser than current Rec Period end date
        ///  0 : Rec Period End date is equal to current rec period end date
        ///  1 : Rec Period End Date is greater than current rec period end date
        /// </summary>
        /// <param name="recPeriodEndDate"></param>
        /// <returns></returns>
        public short GetRecPeriodCompareResult(DateTime? recPeriodEndDate)
        {
            if (recPeriodEndDate.Value < SessionHelper.CurrentReconciliationPeriodEndDate)
                return -1;
            else if (recPeriodEndDate.Value == SessionHelper.CurrentReconciliationPeriodEndDate)
                return 0;
            else
                return 1;
        }

        /// <summary>
        /// Updates the interval details from text boxes to source.
        /// </summary>
        /// <returns></returns>
        public List<GLDataRecurringItemScheduleIntervalDetailInfo> UpdateIntervalDetailsToSource()
        {
            foreach (GridDataItem item in rgScheduleIntervalDetails.MasterTableView.Items)
            {
                int recPeriodID = (int)item.GetDataKeyValue("ReconciliationPeriodID");
                GLDataRecurringItemScheduleIntervalDetailInfo oGLDataRecurringItemScheduleIntervalDetailInfo = ScheduleIntervalDetails.Find(T => T.ReconciliationPeriodID == recPeriodID);

                if (oGLDataRecurringItemScheduleIntervalDetailInfo != null)
                {
                    ExTextBox txtIntervalAmount = (ExTextBox)item.FindControl("txtIntervalAmount");

                    HiddenField hdnSystemIntervalAmount = (HiddenField)item.FindControl("hdnSystemIntervalAmount");
                    Decimal systemIntervalAmount;
                    Decimal.TryParse(hdnSystemIntervalAmount.Value, out systemIntervalAmount);
                    Decimal intervalAmount;
                    Decimal.TryParse(txtIntervalAmount.Text, out intervalAmount);
                    oGLDataRecurringItemScheduleIntervalDetailInfo.IntervalAmount = intervalAmount;
                    oGLDataRecurringItemScheduleIntervalDetailInfo.SystemIntervalAmount = systemIntervalAmount;
                }
            }
            UpdateTotals();
            return ScheduleIntervalDetails;
        }

        /// <summary>
        /// Updates the totals.
        /// </summary>
        public void UpdateTotals()
        {
            decimal GrandTotAmount = 0;
            decimal CurrConsumedAmount = 0;
            decimal TotConsumedAmount = 0;
            SharedRecItemHelper.GetTotals(ScheduleIntervalDetails, SessionHelper.CurrentReconciliationPeriodEndDate,
                 out GrandTotAmount, out CurrConsumedAmount, out TotConsumedAmount);
            GrandTotalAmount = GrandTotAmount;
            CurrentConsumedAmount = CurrConsumedAmount;
            TotalConsumedAmount = TotConsumedAmount;
        }

        #endregion

    }
}