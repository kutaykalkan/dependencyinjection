using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SkyStem.ART.Client.Model;
using SkyStem.ART.Shared.Data;

namespace SkyStem.ART.Shared.Utility
{
    public class SharedRecItemHelper
    {
        private SharedRecItemHelper()
        {
        }

        /// <summary>
        /// Recalculates the schedule.
        /// </summary>
        /// <param name="originalAmt">The original amt.</param>
        /// <param name="scheduleBeginDate">The schedule begin date.</param>
        /// <param name="scheduleEndDate">The schedule end date.</param>
        public static void RecalculateScheduleDaily(List<GLDataRecurringItemScheduleIntervalDetailInfo> ScheduleIntervalDetails, DateTime? startPeriodEndDate, DateTime? currentRecPeriodEndDate, decimal? originalAmt, DateTime? scheduleBeginDate, DateTime? scheduleEndDate)
        {
            if (ScheduleIntervalDetails != null && ScheduleIntervalDetails.Count > 0 &&
                originalAmt.HasValue && Math.Abs(originalAmt.Value) > 0 && scheduleEndDate.HasValue && scheduleEndDate.HasValue)
            {
                //OriginalAmount = originalAmt.Value;
                // Get used balance upto previous period
                decimal? usedBalance = GetUsedBalanceUptoPreviousPeriod(ScheduleIntervalDetails, startPeriodEndDate, currentRecPeriodEndDate);
                // If there is balance to distribute
                if (Math.Abs(originalAmt.GetValueOrDefault()) > Math.Abs(usedBalance.GetValueOrDefault()))
                {
                    // Calculate Adjusted Balance
                    decimal adjustedBalance = originalAmt.GetValueOrDefault() - usedBalance.GetValueOrDefault();
                    decimal dailyAmt = 0;
                    decimal periodAmt = 0;
                    // Get Just Previous Period End Date
                    DateTime? prevDate = GetPreviousPeriodEndDate(ScheduleIntervalDetails, currentRecPeriodEndDate);
                    DateTime? startDT = scheduleBeginDate;
                    int adjustedDays = 0;
                    int periodDays = 0;
                    // If schedule is created in Previous Periods then adjust the start date for calculation
                    if (prevDate.HasValue && prevDate > startDT)
                        startDT = prevDate.Value.AddDays(1);
                    // Calculate adjusted days
                    adjustedDays = 1 + (scheduleEndDate.Value - startDT.Value).Days;
                    if (Math.Abs(adjustedBalance) > 0 && adjustedDays > 0)
                    {
                        // Calculate daily amount
                        dailyAmt = Math.Round(adjustedBalance / adjustedDays, DecimalConstants.DECIMAL_PLACES_FOR_MATH_ROUND);
                        foreach (GLDataRecurringItemScheduleIntervalDetailInfo oItem in ScheduleIntervalDetails)
                        {
                            // Calculate only for current and future periods
                            if (oItem.PeriodEndDate >= currentRecPeriodEndDate)
                            {
                                // Initialize with zero
                                oItem.IntervalAmount = 0;
                                // calculate period days
                                periodDays = (oItem.PeriodEndDate.Value - startDT.Value).Days + 1;
                                // calculate period amount
                                periodAmt = Math.Round(periodDays * dailyAmt, 2);
                                // if adjusted balance is greater than period amount
                                if (Math.Abs(adjustedBalance) >= Math.Abs(periodAmt) && oItem.PeriodEndDate < scheduleEndDate)
                                {
                                    // assign period amount
                                    oItem.IntervalAmount = periodAmt;
                                    // adjust remaining balance
                                    adjustedBalance = adjustedBalance - periodAmt;
                                }
                                else
                                {
                                    // assign anything left balance
                                    oItem.IntervalAmount = adjustedBalance;
                                    // make the balance to 0
                                    adjustedBalance = 0;
                                }
                                // adjust start date for next period
                                startDT = oItem.PeriodEndDate.Value.AddDays(1);
                            }
                            oItem.SystemIntervalAmount = oItem.IntervalAmount;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Recalculates the schedule.
        /// </summary>
        /// <param name="originalAmt">The original amt.</param>
        /// <param name="totalIntervals">The total intervals.</param>
        /// <param name="startInterval">The start interval.</param>
        /// <param name="currentInterval">The current interval.</param>
        /// <param name="prevRecPeriodID">The prev rec period ID.</param>
        public static void RecalculateScheduleOther(List<GLDataRecurringItemScheduleIntervalDetailInfo> ScheduleIntervalDetails, DateTime? currentPeriodEndDate, decimal? originalAmt, int? totalIntervals, int? startInterval, int? currentInterval, int? prevRecPeriodID)
        {
            if (ScheduleIntervalDetails != null && ScheduleIntervalDetails.Count > 0 && originalAmt.HasValue && Math.Abs(originalAmt.Value) > 0
                && totalIntervals.HasValue && Math.Abs(totalIntervals.Value) > 0
                && ((currentInterval.HasValue && Math.Abs(currentInterval.Value) > 0)
                    || (startInterval.HasValue && Math.Abs(startInterval.Value) > 0)
                    ))
            {
                //OriginalAmount = originalAmt.Value;
                //StartIntervalRecPeriodID = startInterval;
                DateTime? StartPeriodEndDate = GetStartIntervalDateTime(ScheduleIntervalDetails, startInterval);
                DateTime? prevPeriodEndDate = GetPreviousPeriodEndDate(ScheduleIntervalDetails, currentPeriodEndDate);
                // Get Used Balance
                decimal? usedBalance = GetUsedBalanceUptoPreviousPeriod(ScheduleIntervalDetails, StartPeriodEndDate, currentPeriodEndDate);
                // if there is anything to distribute
                if (Math.Abs(originalAmt.GetValueOrDefault()) > Math.Abs(usedBalance.GetValueOrDefault()))
                {
                    // Calculate adjusted balance
                    decimal adjustedBalance = originalAmt.GetValueOrDefault() - usedBalance.GetValueOrDefault();
                    // Get used Intervals
                    int? usedIntervals = 0;
                    if (prevPeriodEndDate.GetValueOrDefault() >= StartPeriodEndDate.GetValueOrDefault())
                        usedIntervals = GetUsedIntervalsUptoPrevioudPeriod(currentInterval, prevRecPeriodID);
                    // Calculate adjusted intervals
                    int adjustedTotalIntervals = totalIntervals.GetValueOrDefault() - usedIntervals.GetValueOrDefault();
                    // If there is balance and intervals to distribute
                    if (Math.Abs(adjustedBalance) > 0 && adjustedTotalIntervals > 0)
                    {
                        // Calculate interval amount
                        decimal intervalAmt = Math.Round(adjustedBalance / adjustedTotalIntervals, DecimalConstants.DECIMAL_PLACES_FOR_MATH_ROUND);
                        decimal periodAmt = 0;
                        foreach (GLDataRecurringItemScheduleIntervalDetailInfo oItem in ScheduleIntervalDetails)
                        {
                            // Calculate only for current and future periods
                            if (oItem.PeriodEndDate >= currentPeriodEndDate)
                            {
                                // Initialize with zero
                                oItem.IntervalAmount = 0;

                                if (oItem.PeriodEndDate >= StartPeriodEndDate.GetValueOrDefault())
                                {
                                    if (oItem.PeriodEndDate == StartPeriodEndDate.GetValueOrDefault())
                                        currentInterval = 1;
                                    // If schedule is created in current rec period and rec period in consideration is also current then only use current interval
                                    if (!prevRecPeriodID.HasValue && oItem.PeriodEndDate == currentPeriodEndDate)
                                    {
                                        periodAmt = intervalAmt * currentInterval.Value;
                                        // Added By Manoj:- Bug Fixed  adjust the remaining balance
                                        adjustedTotalIntervals = adjustedTotalIntervals - (currentInterval.Value - 1);
                                    }
                                    else
                                        periodAmt = intervalAmt;

                                    // if adjusted balance is greater than period amount
                                    if (Math.Abs(adjustedBalance) >= Math.Abs(periodAmt))
                                    {
                                        // assgin period amount 
                                        oItem.IntervalAmount = periodAmt;
                                        // adjust the balance
                                        adjustedBalance = adjustedBalance - periodAmt;
                                        adjustedTotalIntervals--;
                                        // Add remaining amount to last interval
                                        if (adjustedTotalIntervals == 0)
                                        {
                                            oItem.IntervalAmount += adjustedBalance;
                                            adjustedBalance = 0;
                                        }
                                    }
                                    else
                                    {
                                        // assign anything left amount
                                        oItem.IntervalAmount = adjustedBalance;
                                        // make the adjusted balance to zero
                                        adjustedBalance = 0;
                                    }
                                }
                            }
                            oItem.SystemIntervalAmount = oItem.IntervalAmount;
                        }
                    }
                }
                //BindData();
            }
        }

        /// <summary>
        /// Gets the used balance upto previous period.
        /// </summary>
        /// <returns></returns>
        public static decimal? GetUsedBalanceUptoPreviousPeriod(List<GLDataRecurringItemScheduleIntervalDetailInfo> ScheduleIntervalDetails, DateTime? StartPeriodEndDate, DateTime? CurrentRecPeriodEndDate)
        {
            decimal? usedBalance = 0;
            if (ScheduleIntervalDetails != null)
            {
                foreach (GLDataRecurringItemScheduleIntervalDetailInfo oItem in ScheduleIntervalDetails)
                {
                    if (oItem.PeriodEndDate >= StartPeriodEndDate.GetValueOrDefault()
                        && oItem.PeriodEndDate < CurrentRecPeriodEndDate)
                        usedBalance += oItem.IntervalAmount;
                }
            }
            return usedBalance;
        }

        public static DateTime? GetStartIntervalDateTime(List<GLDataRecurringItemScheduleIntervalDetailInfo> ScheduleIntervalDetails, int? startInterval)
        {
            DateTime? startPeriodEndDate = null;
            if (startInterval.GetValueOrDefault() > 0)
            {
                foreach (GLDataRecurringItemScheduleIntervalDetailInfo item in ScheduleIntervalDetails)
                {
                    if (item.ReconciliationPeriodID == startInterval)
                        startPeriodEndDate = item.PeriodEndDate;
                }
            }
            return startPeriodEndDate;
        }

        public static int? GetUsedIntervalsUptoPrevioudPeriod(int? currentInterval, int? prevRecPeriodID)
        {
            int? usedIntervals = null;
            // If new schedule or future schedule then nothing is consumed
            if (!prevRecPeriodID.HasValue)
                return usedIntervals = 0;
            else
            {
                // If Current is 3 then 2 is used
                usedIntervals = currentInterval - 1;
            }
            return usedIntervals;
        }

        /// <summary>
        /// Gets the previous period end date.
        /// </summary>
        /// <returns></returns>
        public static DateTime? GetPreviousPeriodEndDate(List<GLDataRecurringItemScheduleIntervalDetailInfo> ScheduleIntervalDetails, DateTime? currentPeriodEndDate)
        {
            DateTime? prevDate = null;
            if (ScheduleIntervalDetails != null)
            {
                foreach (GLDataRecurringItemScheduleIntervalDetailInfo oItem in ScheduleIntervalDetails)
                {
                    if (oItem.PeriodEndDate < currentPeriodEndDate)
                    {
                        if (!prevDate.HasValue || prevDate < oItem.PeriodEndDate)
                            prevDate = oItem.PeriodEndDate;
                    }
                }
            }
            return prevDate;
        }

        public static void GetTotals(List<GLDataRecurringItemScheduleIntervalDetailInfo> ScheduleIntervalDetails, DateTime? currentPeriodEndDate
            , out decimal GrandTotalAmount, out decimal CurrentConsumedAmount, out decimal TotalConsumedAmount)
        {
            GrandTotalAmount = 0;
            CurrentConsumedAmount = 0;
            TotalConsumedAmount = 0;
            foreach (GLDataRecurringItemScheduleIntervalDetailInfo oItem in ScheduleIntervalDetails)
            {
                GrandTotalAmount += oItem.IntervalAmount.GetValueOrDefault();

                if (oItem.PeriodEndDate == currentPeriodEndDate)
                    CurrentConsumedAmount = oItem.IntervalAmount.GetValueOrDefault();

                if (oItem.PeriodEndDate <= currentPeriodEndDate)
                    TotalConsumedAmount += oItem.IntervalAmount.GetValueOrDefault();
            }
        }

        /// <summary>
        /// Converts the amount.
        /// </summary>
        /// <param name="amount">The amount.</param>
        /// <param name="rate">The rate.</param>
        /// <returns></returns>
        public static decimal? ConvertAmount(decimal? amount, decimal? rate)
        {
            decimal? value = null;
            if (amount.HasValue && rate.HasValue)
            {
                rate = Math.Round(rate.Value, 4);
                value = Math.Round(amount.Value * rate.Value, DecimalConstants.DECIMAL_PLACES_FOR_EXCHANGE_RATE_ROUND);
            }
            return value;
        }

        public static decimal? GetExchangeRate(List<ExchangeRateInfo> oExchangeRateInfoList, string fromCurrencyCode, string toCurrecyCode)
        {
            decimal exchangeRate = 0M;
            // this will happen in case of Net Account
            if (string.IsNullOrEmpty(fromCurrencyCode)
                || string.IsNullOrEmpty(toCurrecyCode))
                return null;

            // if Codes are same
            if (fromCurrencyCode == toCurrecyCode)
            {
                exchangeRate = 1M;
                return exchangeRate;
            }

            fromCurrencyCode = fromCurrencyCode.Trim().ToLower();
            toCurrecyCode = toCurrecyCode.Trim().ToLower();
            if (oExchangeRateInfoList != null && oExchangeRateInfoList.Count > 0)
            {
                ExchangeRateInfo oExchangeRateInfo = oExchangeRateInfoList.FirstOrDefault(T => T.FromCurrencyCode.Trim().ToLower() == fromCurrencyCode && T.ToCurrencyCode.Trim().ToLower() == toCurrecyCode);
                if (oExchangeRateInfo != null)
                    exchangeRate = oExchangeRateInfo.ExchangeRate.Value;
            }
            return exchangeRate;
        }

        /// <summary>
        /// Recalculates the rec item schedule amount.
        /// </summary>
        /// <param name="oGLDataRecurringItemScheduleInfo">The o GL data recurring item schedule info.</param>
        /// <param name="bccyCode">The bccy code.</param>
        /// <param name="netAccountID">The net account ID.</param>
        public static void RecalculateRecItemScheduleAmount(GLDataRecurringItemScheduleInfo oGLDataRecurringItemScheduleInfo, string bccyCode, string rccyCode, decimal? exRateLccyToBccy, decimal? exRateLccyToRccy)
        {
            if (oGLDataRecurringItemScheduleInfo != null)
            {
                //if (netAccountID.GetValueOrDefault() == 0)
                //{
                if (oGLDataRecurringItemScheduleInfo.ExRateLCCYtoBCCY.HasValue)
                {
                    exRateLccyToBccy = oGLDataRecurringItemScheduleInfo.ExRateLCCYtoBCCY.Value;
                }
                oGLDataRecurringItemScheduleInfo.ScheduleAmountBaseCurrency = ConvertAmount(oGLDataRecurringItemScheduleInfo.ScheduleAmount, exRateLccyToBccy);
                oGLDataRecurringItemScheduleInfo.RecPeriodAmountBaseCurrency = ConvertAmount(oGLDataRecurringItemScheduleInfo.RecPeriodAmountLocalCurrency, exRateLccyToBccy);
                oGLDataRecurringItemScheduleInfo.BalanceBaseCurrency = ConvertAmount(oGLDataRecurringItemScheduleInfo.BalanceLocalCurrency, exRateLccyToBccy);
                //}
                if (oGLDataRecurringItemScheduleInfo.ExRateLCCYtoRCCY.HasValue)
                {
                    exRateLccyToRccy = oGLDataRecurringItemScheduleInfo.ExRateLCCYtoRCCY.Value;
                }
                oGLDataRecurringItemScheduleInfo.ScheduleAmountReportingCurrency = ConvertAmount(oGLDataRecurringItemScheduleInfo.ScheduleAmount, exRateLccyToRccy);
                oGLDataRecurringItemScheduleInfo.RecPeriodAmountReportingCurrency = ConvertAmount(oGLDataRecurringItemScheduleInfo.RecPeriodAmountLocalCurrency, exRateLccyToRccy);
                oGLDataRecurringItemScheduleInfo.BalanceReportingCurrency = ConvertAmount(oGLDataRecurringItemScheduleInfo.BalanceLocalCurrency, exRateLccyToRccy);
            }
        }
    }
}
