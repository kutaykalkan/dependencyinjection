using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using SkyStem.Library.Controls.WebControls;
using SkyStem.ART.Web.Data;
using SkyStem.ART.Web.Classes;
using System.Text;
using SkyStem.Language.LanguageUtility;
using SkyStem.ART.Web.Classes.UserControl;
using Telerik.Web.UI;
using SkyStem.ART.Client.Model;
using SkyStem.ART.Client.Data;
using System.Collections.Generic;
using SkyStem.ART.Shared.Utility;


namespace SkyStem.ART.Web.Utility
{

    /// <summary>
    /// Summary description for RecHelper
    /// </summary>
    public class RecHelper
    {
        public RecHelper()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public static decimal? RecalculateCurrentAmount(int? recPeriodID, DateTime? recPeriodEndDate
            , HiddenField hdnPrevRecPeriodEndDate, HiddenField hdnPreviousGLDataRecurringItemScheduleID
            , TextBox txtOriginalAmountLCCY, ExLabel lblCurrentAmountRCCYValue
            , DateTime? scheduleBeginDate, DateTime? scheduleEndDate
            , string lccyCode, string rccyCode)
        {
            // formula
            /*
             *  No Of Days in Scedule = Schedule End Date - Schedule Begin Date
             * If Record created in the Current Rec Period
             *  then Start Date = Schedule Begin Date
             *       End Date = Rec Period End Date
             * Else
             *  then Start Date = Previous Rec Period End Date + 1
             *       End Date = Current Rec Period End Date

             * No Of Days Consumed in Rec Period = End Date - Start Date
             * Daily Amt = Schedule Amt RCCY / No Of Days in Scedule
             * Current Amt = Daily Amt * No Of Days Consumed in Rec Period
             */
            DateTime? prevRecPeriodEndDate = null;
            long? previousGLDataRecurringItemScheduleID = null;

            decimal? originalAmtLCCY = null;
            decimal? currentAmtRCCY = null;
            decimal? currentAmtLCCY = null;
            decimal number;

            if (lccyCode != WebConstants.SELECT_ONE)
            {
                decimal? exchangeRateLCCYToRCCY = CacheHelper.GetExchangeRate(lccyCode, rccyCode, recPeriodID);

                if (!string.IsNullOrEmpty(txtOriginalAmountLCCY.Text)
                    && Decimal.TryParse(txtOriginalAmountLCCY.Text, out number)
                    && exchangeRateLCCYToRCCY != null)
                {
                    // Original Amt - LCCY , BCCY , RCCY
                    // Calculate in LCCY and then convert to BCCY and RCCY
                    originalAmtLCCY = Convert.ToDecimal(txtOriginalAmountLCCY.Text);
                    originalAmtLCCY = Math.Round(originalAmtLCCY.Value, TestConstant.DECIMAL_PLACES_FOR_MATH_ROUND);

                    decimal noOfDaysInSchedule = Helper.GetDaysBetweenDateRanges(scheduleBeginDate, scheduleEndDate);
                    decimal? noOfDaysConsumedInCurrentRecPeriod = null;

                    if (!string.IsNullOrEmpty(hdnPreviousGLDataRecurringItemScheduleID.Value))
                    {
                        previousGLDataRecurringItemScheduleID = Convert.ToInt64(hdnPreviousGLDataRecurringItemScheduleID.Value);
                    }

                    // Get No of Days Consumed in Current Rec Period
                    if (previousGLDataRecurringItemScheduleID == null)
                    {
                        noOfDaysConsumedInCurrentRecPeriod = Helper.GetDaysBetweenDateRanges(scheduleBeginDate, recPeriodEndDate);
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(hdnPrevRecPeriodEndDate.Value))
                        {
                            DateTime dt;
                            DateTime.TryParse(hdnPrevRecPeriodEndDate.Value, out dt);
                            prevRecPeriodEndDate = dt;
                        }
                        noOfDaysConsumedInCurrentRecPeriod = Helper.GetDaysBetweenDateRanges(prevRecPeriodEndDate, recPeriodEndDate) - 1;
                    }

                    if (noOfDaysInSchedule > 0 && noOfDaysConsumedInCurrentRecPeriod > 0)
                    {
                        currentAmtLCCY = (originalAmtLCCY / noOfDaysInSchedule) * noOfDaysConsumedInCurrentRecPeriod;
                        currentAmtLCCY = Math.Round(currentAmtLCCY.Value, TestConstant.DECIMAL_PLACES_FOR_MATH_ROUND);

                        currentAmtRCCY = currentAmtLCCY * exchangeRateLCCYToRCCY;
                        currentAmtRCCY = Math.Round(currentAmtRCCY.Value, TestConstant.DECIMAL_PLACES_FOR_MATH_ROUND);
                    }
                }
            }

            lblCurrentAmountRCCYValue.Text = Helper.GetDisplayDecimalValue(currentAmtRCCY);
            return currentAmtRCCY;
        }

        public static void RecalculateRecItemScheduleAmount(TextBox txtOriginalAmountLCCY, ExCalendar calScheduleBeginDate, ExCalendar calScheduleEndDate,
            ExLabel lblOriginalAmountRCCYValue, ExLabel lblCurrentAmountRCCYValue, ExLabel lblTotalAccruedAmountRCCYValue, ExLabel lblToBeAccruedAmountRCCYValue
            , string lccyCode, string bccyCode, string rccyCode, int? recPeriodID, DateTime? recPeriodEndDate
            , HiddenField hdnPrevRecPeriodEndDate, HiddenField hdnPreviousGLDataRecurringItemScheduleID
            , out decimal? originalAmtBCCY
            , out decimal? totalAccruedAmountLCCY, out decimal? totalAccruedAmountBCCY
            , out decimal? toBeAccruedAmountLCCY, out decimal? toBeAccruedAmountBCCY)
        {
            decimal? totalAccruedAmountRCCY = null;
            decimal? toBeAccruedAmountRCCY = null;
            decimal? originalAmtLCCY = null;
            decimal? originalAmtRCCY = null;
            decimal? currentAmtRCCY = null;
            decimal number;

            originalAmtBCCY = null;
            totalAccruedAmountLCCY = null;
            totalAccruedAmountBCCY = null;
            toBeAccruedAmountLCCY = null;
            toBeAccruedAmountBCCY = null;

            if (lccyCode != WebConstants.SELECT_ONE)
            {
                decimal? exchangeRateLCCYToRCCY = CacheHelper.GetExchangeRate(lccyCode, rccyCode, recPeriodID);
                decimal? exchangeRateLCCYToBCCY = CacheHelper.GetExchangeRate(lccyCode, bccyCode, recPeriodID);

                if (!string.IsNullOrEmpty(txtOriginalAmountLCCY.Text)
                    && Decimal.TryParse(txtOriginalAmountLCCY.Text, out number)
                    && exchangeRateLCCYToRCCY != null)
                {
                    // Original Amt - LCCY , BCCY , RCCY
                    // Calculate in LCCY and then convert to BCCY and RCCY
                    originalAmtLCCY = Convert.ToDecimal(txtOriginalAmountLCCY.Text);
                    originalAmtLCCY = Math.Round(originalAmtLCCY.Value, TestConstant.DECIMAL_PLACES_FOR_MATH_ROUND);

                    if (exchangeRateLCCYToBCCY != null)
                    {
                        originalAmtBCCY = originalAmtLCCY * exchangeRateLCCYToBCCY;
                        originalAmtBCCY = Math.Round(originalAmtBCCY.Value, TestConstant.DECIMAL_PLACES_FOR_MATH_ROUND);
                    }

                    originalAmtRCCY = originalAmtLCCY * exchangeRateLCCYToRCCY;
                    originalAmtRCCY = Math.Round(originalAmtRCCY.Value, TestConstant.DECIMAL_PLACES_FOR_MATH_ROUND);

                    DateTime scheduleBeginDate;
                    DateTime scheduleEndDate;
                    if (DateTime.TryParse(calScheduleBeginDate.Text, out scheduleBeginDate) && DateTime.TryParse(calScheduleEndDate.Text, out scheduleEndDate))
                    {
                        currentAmtRCCY = RecHelper.RecalculateCurrentAmount(recPeriodID, recPeriodEndDate
                            , hdnPrevRecPeriodEndDate, hdnPreviousGLDataRecurringItemScheduleID
                            , txtOriginalAmountLCCY, lblCurrentAmountRCCYValue, scheduleBeginDate, scheduleEndDate
                            , lccyCode, rccyCode);

                        decimal noOfDaysInSchedule = Helper.GetDaysBetweenDateRanges(scheduleBeginDate, scheduleEndDate);
                        decimal noOfDaysConsumedTillCurrentRecPeriod = Helper.GetDaysBetweenDateRanges(scheduleBeginDate, recPeriodEndDate);

                        if (noOfDaysInSchedule > 0 && noOfDaysConsumedTillCurrentRecPeriod > 0)
                        {
                            // Total Accrued Amt - LCCY / BCCY / RCCY
                            // Calculate in LCCY and then convert to BCCY and RCCY
                            totalAccruedAmountLCCY = (originalAmtLCCY / noOfDaysInSchedule) * noOfDaysConsumedTillCurrentRecPeriod;
                            totalAccruedAmountLCCY = Math.Round(totalAccruedAmountLCCY.Value, TestConstant.DECIMAL_PLACES_FOR_MATH_ROUND);

                            totalAccruedAmountRCCY = totalAccruedAmountLCCY * exchangeRateLCCYToRCCY;
                            totalAccruedAmountRCCY = Math.Round(totalAccruedAmountRCCY.Value, TestConstant.DECIMAL_PLACES_FOR_MATH_ROUND);

                            // To Be Accrued Amt or Balance - LCCY / BCCY / RCCY
                            // Calculate in LCCY and then convert to BCCY and RCCY
                            toBeAccruedAmountLCCY = originalAmtLCCY - totalAccruedAmountLCCY;
                            toBeAccruedAmountLCCY = Math.Round(toBeAccruedAmountLCCY.Value, TestConstant.DECIMAL_PLACES_FOR_MATH_ROUND);

                            toBeAccruedAmountRCCY = toBeAccruedAmountLCCY * exchangeRateLCCYToRCCY;
                            toBeAccruedAmountRCCY = Math.Round(toBeAccruedAmountRCCY.Value, TestConstant.DECIMAL_PLACES_FOR_MATH_ROUND);

                            // BCCY Calcualtion
                            if (exchangeRateLCCYToBCCY != null)
                            {
                                totalAccruedAmountBCCY = totalAccruedAmountLCCY * exchangeRateLCCYToBCCY;
                                totalAccruedAmountBCCY = Math.Round(totalAccruedAmountBCCY.Value, TestConstant.DECIMAL_PLACES_FOR_MATH_ROUND);

                                toBeAccruedAmountBCCY = toBeAccruedAmountLCCY * exchangeRateLCCYToBCCY;
                                toBeAccruedAmountBCCY = Math.Round(toBeAccruedAmountBCCY.Value, TestConstant.DECIMAL_PLACES_FOR_MATH_ROUND);
                            }

                        }
                    }
                }
            }

            lblOriginalAmountRCCYValue.Text = Helper.GetDisplayDecimalValue(originalAmtRCCY);
            lblTotalAccruedAmountRCCYValue.Text = Helper.GetDisplayDecimalValue(totalAccruedAmountRCCY);
            lblToBeAccruedAmountRCCYValue.Text = Helper.GetDisplayDecimalValue(toBeAccruedAmountRCCY);

            lblCurrentAmountRCCYValue.Text = Helper.GetDisplayDecimalValue(currentAmtRCCY);
        }

        public static void ShowHideReviewNotesAndQualityScore(HtmlTableRow trReviewNotes, HtmlTableRow trQualityScore, HtmlTableRow trRecControlCheckList)
        {
          
            if (SessionHelper.CurrentRoleID == (short)ARTEnums.UserRole.AUDIT)
            {
                List<CompanyAttributeConfigInfo> oAttributeConfigInfo = AttributeConfigHelper.GetCompanyAttributeConfigInfoList(false, WebEnums.AttributeSetType.RoleConfig);
                CompanyAttributeConfigInfo oReviewNotesConfig = oAttributeConfigInfo.Find(c => c.AttributeID == (short)ARTEnums.AttributeList.NotSeeReviewNoteSection);
                CompanyAttributeConfigInfo oQualitySectionConfig = oAttributeConfigInfo.Find(c => c.AttributeID == (short)ARTEnums.AttributeList.NotSeeQSSection);
                CompanyAttributeConfigInfo oNotSeeRecControlChecklist = oAttributeConfigInfo.Find(c => c.AttributeID == (short)ARTEnums.AttributeList.NotSeeRecControlChecklist);

                //bool isReviewNotesConfigSelected = (bool)oReviewNotesConfig.IsEnabled;
                //bool isQualityScoreConfigSelected = (bool)oQualitySectionConfig.IsEnabled;
                //trReviewNotes.Visible = !isReviewNotesConfigSelected;
                //trQualityScore.Visible = !isQualityScoreConfigSelected;
                if (oReviewNotesConfig.IsEnabled.HasValue)
                    trReviewNotes.Visible = !oReviewNotesConfig.IsEnabled.Value;
                if (oQualitySectionConfig.IsEnabled.HasValue)
                    trQualityScore.Visible = !oQualitySectionConfig.IsEnabled.Value;
                if (oNotSeeRecControlChecklist.IsEnabled.HasValue)
                    trRecControlCheckList.Visible = !oNotSeeRecControlChecklist.IsEnabled.Value;
                
            }

        }

        public static void RecalculateRecItemAmount(TextBox txtAmountLCCY
            , string lccyCode, string bccyCode, string rccyCode
            , decimal? exchangeRateLCCYToBCCY, decimal? exchangeRateLCCYToRCCY
            , out decimal? amountBCCY, out decimal? amountRCCY)
        {
            decimal? amountLCCY = null;
            decimal number;

            amountBCCY = null;
            amountRCCY = null;

            if (lccyCode != WebConstants.SELECT_ONE)
            {
                if (!string.IsNullOrEmpty(txtAmountLCCY.Text)
                    && Decimal.TryParse(txtAmountLCCY.Text, out number)
                    && exchangeRateLCCYToRCCY != null)
                {
                    amountLCCY = number;
                    amountLCCY = Math.Round(amountLCCY.Value, TestConstant.DECIMAL_PLACES_FOR_MATH_ROUND);

                    if (exchangeRateLCCYToBCCY != null)
                    {
                        exchangeRateLCCYToBCCY = Math.Round(exchangeRateLCCYToBCCY.Value, TestConstant.DECIMAL_PLACES_FOR_MATH_ROUND);
                        amountBCCY = amountLCCY * exchangeRateLCCYToBCCY;
                        amountBCCY = Math.Round(amountBCCY.Value, TestConstant.DECIMAL_PLACES_FOR_MATH_ROUND);
                    }

                    if (exchangeRateLCCYToRCCY != null)
                    {
                        exchangeRateLCCYToRCCY = Math.Round(exchangeRateLCCYToRCCY.Value, TestConstant.DECIMAL_PLACES_FOR_MATH_ROUND);
                        amountRCCY = amountLCCY * exchangeRateLCCYToRCCY;
                        amountRCCY = Math.Round(amountRCCY.Value, TestConstant.DECIMAL_PLACES_FOR_MATH_ROUND);
                    }
                }
            }
        }

        public static void RecalculateRecItemAmount(TextBox txtAmountLCCY
            , string lccyCode, string bccyCode, string rccyCode, int? recPeriodID
            , out decimal? amountBCCY, out decimal? amountRCCY)
        {
            decimal number;

            amountBCCY = null;
            amountRCCY = null;

            if (lccyCode != WebConstants.SELECT_ONE)
            {
                decimal? exchangeRateLCCYToRCCY = CacheHelper.GetExchangeRate(lccyCode, rccyCode, recPeriodID);
                decimal? exchangeRateLCCYToBCCY = CacheHelper.GetExchangeRate(lccyCode, bccyCode, recPeriodID);

                if (!string.IsNullOrEmpty(txtAmountLCCY.Text)
                    && Decimal.TryParse(txtAmountLCCY.Text, out number)
                    && exchangeRateLCCYToRCCY != null)
                {
                    RecalculateRecItemAmount(txtAmountLCCY, lccyCode, bccyCode, rccyCode, exchangeRateLCCYToBCCY, exchangeRateLCCYToRCCY, out amountBCCY, out amountRCCY);
                }
            }
        }

        public static void RecalculateRecItemAmount(TextBox txtAmountLCCY, ExLabel lblAmountBCCYValue, ExLabel lblAmountRCCYValue
            , string lccyCode, string bccyCode, string rccyCode, int? recPeriodID)
        {
            decimal? amountBCCY = null;
            decimal? amountRCCY = null;

            RecHelper.RecalculateRecItemAmount(txtAmountLCCY, lccyCode, bccyCode, rccyCode, recPeriodID, out amountBCCY, out amountRCCY);
            lblAmountBCCYValue.Text = Helper.GetDisplayDecimalValue(amountBCCY);
            lblAmountRCCYValue.Text = Helper.GetDisplayDecimalValue(amountRCCY);
        }

        public static void RecalculateRecItemAmount(TextBox txtAmountLCCY, ExLabel lblAmountBCCYValue, ExLabel lblAmountRCCYValue
            , ExLabel lblOverriddenExRateBCCYValue, ExLabel lblOverriddenExRateRCCYValue
            , string lccyCode, string bccyCode, string rccyCode, decimal? ExRateLCCYtoBCCY, decimal? ExRateLCCYtoRCCY)
        {
            decimal? amountBCCY = null;
            decimal? amountRCCY = null;

            RecHelper.RecalculateRecItemAmount(txtAmountLCCY, lccyCode, bccyCode, rccyCode, ExRateLCCYtoBCCY, ExRateLCCYtoRCCY, out amountBCCY, out amountRCCY);
            lblAmountBCCYValue.Text = Helper.GetDisplayDecimalValue(amountBCCY);
            lblAmountRCCYValue.Text = Helper.GetDisplayDecimalValue(amountRCCY);
            lblOverriddenExRateBCCYValue.Text = Helper.GetDisplayExchangeRateValue(ExRateLCCYtoBCCY);
            lblOverriddenExRateRCCYValue.Text = Helper.GetDisplayExchangeRateValue(ExRateLCCYtoRCCY);
        }

        public static void SetErrorMessagesForValidationControls(RequiredFieldValidator rfvAmount, RequiredFieldValidator rfvLocalCurrency
            , RequiredFieldValidator rfvOpenTransDate, RequiredFieldValidator rfvResolutionDate
            , CustomValidator cstVldAmount, CustomValidator cvOpenDate, CustomValidator cvResolutionDate
            , CustomValidator cvCompareOpenDateWithCurrentDate, CustomValidator cvResolutionDateCompareWithCurrentDate
            , CustomValidator cvResolutionDateCompareWithOpenDate
            , int amountLabelID, int currencyLabelID, int openDateLabelID, int closeDateLabelUD)
        {
            int currentDateLabelID = 2062;

            // Required Fields
            rfvAmount.ErrorMessage = Helper.GetErrorMessage(WebEnums.FieldType.MandatoryField, amountLabelID);

            rfvLocalCurrency.InitialValue = WebConstants.SELECT_ONE;
            rfvLocalCurrency.ErrorMessage = Helper.GetErrorMessage(WebEnums.FieldType.MandatoryField, currencyLabelID);

            rfvOpenTransDate.ErrorMessage = Helper.GetErrorMessage(WebEnums.FieldType.MandatoryField, openDateLabelID);

            // Invalid Values
            cstVldAmount.ErrorMessage = Helper.GetErrorMessage(WebEnums.FieldType.InvalidNumericField, amountLabelID);
            cvOpenDate.ErrorMessage = Helper.GetErrorMessage(WebEnums.FieldType.InvalidDate, openDateLabelID);

            cvCompareOpenDateWithCurrentDate.ErrorMessage = Helper.GetErrorMessage(WebEnums.FieldType.DateCompareField, openDateLabelID, currentDateLabelID);

            SetErrorMessageForCloseDateValidationControls(rfvResolutionDate, cvResolutionDate, cvResolutionDateCompareWithCurrentDate, cvResolutionDateCompareWithOpenDate, openDateLabelID, closeDateLabelUD, currentDateLabelID);
        }

        public static void SetErrorMessageForCloseDateValidationControls(RequiredFieldValidator rfvResolutionDate
            , CustomValidator cvResolutionDate, CustomValidator cvResolutionDateCompareWithCurrentDate
            , CustomValidator cvResolutionDateCompareWithOpenDate, int openDateLabelID, int closeDateLabelUD, int currentDateLabelID)
        {
            // Required Fields
            rfvResolutionDate.ErrorMessage = Helper.GetErrorMessage(WebEnums.FieldType.MandatoryField, closeDateLabelUD);

            // Invalid Values
            cvResolutionDate.ErrorMessage = Helper.GetErrorMessage(WebEnums.FieldType.InvalidDate, closeDateLabelUD);

            cvResolutionDateCompareWithCurrentDate.ErrorMessage = Helper.GetErrorMessage(WebEnums.FieldType.DateCompareField, closeDateLabelUD, currentDateLabelID);
            cvResolutionDateCompareWithOpenDate.ErrorMessage = Helper.GetErrorMessage(WebEnums.FieldType.DateCompareFieldGreaterThan, closeDateLabelUD, openDateLabelID);
        }



        /// <summary>
        /// Force Reload of those User Controls which are already expanded
        /// </summary>
        /// <param name="oUserControlRecItemBaseCollection"></param>
        public static void ReloadUserControls(GLDataHdrInfo oGLDataHdrInfo, params UserControlRecItemBase[] oUserControlRecItemBaseCollection)
        {
            for (int i = 0; i < oUserControlRecItemBaseCollection.Length; i++)
            {
                oUserControlRecItemBaseCollection[i].IsRefreshData = true;
                if (oUserControlRecItemBaseCollection[i].IsExpanded)
                {
                    if (oGLDataHdrInfo == null || oGLDataHdrInfo.GLDataID == null || oGLDataHdrInfo.GLDataID == 0)
                    {
                        oUserControlRecItemBaseCollection[i].IsExpanded = false;
                        oUserControlRecItemBaseCollection[i].ExpandCollapse();
                    }
                    oUserControlRecItemBaseCollection[i].GLDataHdrInfo = oGLDataHdrInfo;
                    oUserControlRecItemBaseCollection[i].LoadData();
                }
            }
        }

        public static string GetJSParametersForRecalculateRecItemScheduleAmount(TextBox txtOriginalAmountLCCY
            , ExCalendar calScheduleBeginDate, ExCalendar calScheduleEndDate, ExLabel lblOriginalAmountRCCYValue
            , ExLabel lblCurrentAmountRCCYValue, ExLabel lblTotalAccruedAmountRCCYValue, ExLabel lblToBeAccruedAmountRCCYValue
            , HiddenField hdnPrevRecPeriodEndDate, HiddenField hdnPrevGLDataRecurringItemScheduleID
            , string lccyCode, string bccyCode, string rccyCode, int? recPeriodID, DateTime? recPeriodEndDate)
        {
            string periodEndDate = Helper.GetDisplayDateForCalendar(recPeriodEndDate);
            decimal? exchangeRateLCCYToRCCY = null;
            decimal? exchangeRateLCCYToBCCY = null;

            if (lccyCode != WebConstants.SELECT_ONE)
            {
                exchangeRateLCCYToRCCY = CacheHelper.GetExchangeRate(lccyCode, rccyCode, recPeriodID);
                exchangeRateLCCYToBCCY = CacheHelper.GetExchangeRate(lccyCode, bccyCode, recPeriodID);
            }

            StringBuilder oJSFunctionCall = new StringBuilder();

            oJSFunctionCall.Append("'");
            oJSFunctionCall.Append(txtOriginalAmountLCCY.ClientID);
            oJSFunctionCall.Append("', '");
            oJSFunctionCall.Append(calScheduleBeginDate.ClientID);
            oJSFunctionCall.Append("', '");
            oJSFunctionCall.Append(calScheduleEndDate.ClientID);
            oJSFunctionCall.Append("', '");
            oJSFunctionCall.Append(lblOriginalAmountRCCYValue.ClientID);
            oJSFunctionCall.Append("', '");
            oJSFunctionCall.Append(lblCurrentAmountRCCYValue.ClientID);
            oJSFunctionCall.Append("', '");
            oJSFunctionCall.Append(lblTotalAccruedAmountRCCYValue.ClientID);
            oJSFunctionCall.Append("', '");
            oJSFunctionCall.Append(lblToBeAccruedAmountRCCYValue.ClientID);
            oJSFunctionCall.Append("', '");
            oJSFunctionCall.Append(lccyCode);
            oJSFunctionCall.Append("', '");
            if (exchangeRateLCCYToBCCY != null)
            {
                oJSFunctionCall.Append(exchangeRateLCCYToBCCY);
            }
            else
            {
                oJSFunctionCall.Append(0);
            }
            oJSFunctionCall.Append("', '");
            oJSFunctionCall.Append(exchangeRateLCCYToRCCY);
            oJSFunctionCall.Append("', '");
            oJSFunctionCall.Append(periodEndDate);
            oJSFunctionCall.Append("', ");
            oJSFunctionCall.Append(TestConstant.DECIMAL_PLACES_FOR_MATH_ROUND);
            oJSFunctionCall.Append(", '");
            oJSFunctionCall.Append(hdnPrevRecPeriodEndDate.ClientID);
            oJSFunctionCall.Append("', '");
            oJSFunctionCall.Append(hdnPrevGLDataRecurringItemScheduleID.ClientID);
            oJSFunctionCall.Append("', ");
            oJSFunctionCall.Append(TestConstant.DECIMAL_PLACES_FOR_EXCHANGE_RATE_ROUND);

            return oJSFunctionCall.ToString(); ;
        }

        public static void RenderJSForOldValuesForRecalculateBalances(PageBase oPage, TextBox txtBankBalanceBC, TextBox txtBankBalanceRC)
        {
            StringBuilder oScript = new StringBuilder();
            oScript.Append("OldBankBalancBC = '");
            if (string.IsNullOrEmpty(txtBankBalanceBC.Text))
            {
                oScript.Append("0");
            }
            else
            {
                oScript.Append(Helper.GetDecimalValueForTextBox(Convert.ToDecimal(txtBankBalanceBC.Text), TestConstant.DECIMAL_PLACES_FOR_MATH_ROUND));
            }
            oScript.Append("';");
            oScript.Append(System.Environment.NewLine);
            oScript.Append("OldBankBalancRC = '");
            if (string.IsNullOrEmpty(txtBankBalanceRC.Text))
            {
                oScript.Append("0");
            }
            else
            {
                oScript.Append(Helper.GetDecimalValueForTextBox(Convert.ToDecimal(txtBankBalanceRC.Text), TestConstant.DECIMAL_PLACES_FOR_MATH_ROUND));
            }
            oScript.Append("';");

            if (!oPage.ClientScript.IsClientScriptBlockRegistered(oPage.GetType(), "PopulateVariableValues"))
            {
                oPage.ClientScript.RegisterClientScriptBlock(oPage.GetType(), "PopulateVariableValues", oScript.ToString(), true);
            }
        }


        public static void ShowRecStatusBar(RecPeriodMasterPageBase oRecPeriodMasterPageBase, UserControlRecStatusBarBase ucRecStatusBar)
        {
            switch (oRecPeriodMasterPageBase.RecStatusBar)
            {
                case WebEnums.RecStatusBarPageType.RecForm:
                    RecHelper.ShowRecStatusBar(oRecPeriodMasterPageBase, ucRecStatusBar, true, false, false, true, true, true);
                    break;

                case WebEnums.RecStatusBarPageType.RecFormPrint:
                    RecHelper.ShowRecStatusBar(oRecPeriodMasterPageBase, ucRecStatusBar, true, false, false, true, false, true);
                    break;

                case WebEnums.RecStatusBarPageType.OtherPages:
                    RecHelper.ShowRecStatusBar(oRecPeriodMasterPageBase, ucRecStatusBar, true, true, true, false, false, false);
                    break;
            }
        }

        private static void ShowRecStatusBar(RecPeriodMasterPageBase oRecPeriodMasterPageBase, UserControlRecStatusBarBase ucRecStatusBar, bool bShowRecStatus, bool bShowReconciledBalance, bool bShowUnexpVar, bool bShowDueDates, bool bShowExportButtons, bool bShowQualityScore)
        {
            ucRecStatusBar.AccountID = oRecPeriodMasterPageBase.AccountID;
            ucRecStatusBar.NetAccountID = oRecPeriodMasterPageBase.NetAccountID;
            ucRecStatusBar.GLDataID = oRecPeriodMasterPageBase.GLDataID;
            ucRecStatusBar.PageTitleLabeID = oRecPeriodMasterPageBase.PageTitleLabeID;

            ucRecStatusBar.ShowRecStatus = bShowRecStatus;
            ucRecStatusBar.ShowReconciledBalance = bShowReconciledBalance;
            ucRecStatusBar.ShowUnexpVar = bShowUnexpVar;
            ucRecStatusBar.ShowDueDates = bShowDueDates;
            ucRecStatusBar.ShowExportButton = bShowExportButtons;
            ucRecStatusBar.ShowQualityScore = bShowQualityScore;
        }

        public static void SetRecStatusBarPropertiesForRecForm(PageBase oPageBase, long? glDataID)
        {
            RecHelper.SetRecStatusBarProperties(oPageBase, glDataID, WebEnums.RecStatusBarPageType.RecForm);
        }

        public static void SetRecStatusBarPropertiesForRecFormPrint(PageBase oPageBase, long? glDataID)
        {
            RecHelper.SetRecStatusBarProperties(oPageBase, glDataID, WebEnums.RecStatusBarPageType.RecFormPrint);
        }

        public static void SetRecStatusBarPropertiesForOtherPages(PageBase oPageBase, long? glDataID)
        {
            RecHelper.SetRecStatusBarProperties(oPageBase, glDataID, WebEnums.RecStatusBarPageType.OtherPages);
        }

        private static void SetRecStatusBarProperties(PageBase oPageBase, long? glDataID, WebEnums.RecStatusBarPageType eRecStatusBarPageType)
        {
            RecPeriodMasterPageBase oRecPeriodMasterPageBase;
            oRecPeriodMasterPageBase = (RecPeriodMasterPageBase)oPageBase.Master;
            oRecPeriodMasterPageBase.GLDataID = glDataID;
            oRecPeriodMasterPageBase.RecStatusBar = eRecStatusBarPageType;
        }

        public static void RefreshRecForm(UserControlRecItemBase oUserControlRecItemBase)
        {
            PageBaseRecForm oPageBaseRecForm = (PageBaseRecForm)oUserControlRecItemBase.Page;
            oPageBaseRecForm.RefreshPage(null, null);
        }

        public static void ReloadRecPeriodsOnMasterPage(UserControlRecItemBase oUserControlRecItemBase)
        {
            if (oUserControlRecItemBase.IsRefreshData)
            {
                MasterPageBase oMasterPageBase = (MasterPageBase)oUserControlRecItemBase.Page.Master.Master;
                // Reload the Rec Periods and also the Status / Countdown
                oMasterPageBase.ReloadRecPeriods(false);
            }
        }

        public static void SetMatchSetRefNumberUrlForGLDataRecItem(GridItemEventArgs e, GLDataRecItemInfo oGLReconciliationItemInputInfo, long? AccountID, long? NetAccountID, long? GLDataID)
        {
            ExHyperLink hlMatchSetRefNumber = (ExHyperLink)e.Item.FindControl("hlMatchSetRefNumber");
            hlMatchSetRefNumber.Text = Helper.GetDisplayStringValue(oGLReconciliationItemInputInfo.MatchSetRefNumber);

            if (!(bool)oGLReconciliationItemInputInfo.IsForwardedItem)
            {
                if (!string.IsNullOrEmpty(oGLReconciliationItemInputInfo.MatchSetRefNumber))
                {
                    hlMatchSetRefNumber.Enabled = true;
                    string queryString = "?" + QueryStringConstants.ACCOUNT_ID + "=" + AccountID
                              + "&" + QueryStringConstants.NETACCOUNT_ID + "=" + NetAccountID
                             + "&" + QueryStringConstants.GLDATA_ID + "=" + GLDataID
                             + "&" + QueryStringConstants.MATCH_SET_ID + "=" + oGLReconciliationItemInputInfo.MatchSetID
                             + "&" + QueryStringConstants.MATCHSET_SUBSET_COMBINATION_ID + "=" + oGLReconciliationItemInputInfo.MatchSetSubSetCombinationID
                              + "&" + QueryStringConstants.MATCHING_TYPE_ID + "=" + (int)ARTEnums.MatchingType.AccountMatching;
                    hlMatchSetRefNumber.NavigateUrl = URLConstants.URL_MATCHING_VIEW_MATCH_SET + queryString;
                }
                else
                {
                    hlMatchSetRefNumber.Enabled = false;
                }
            }
            else
            {
                hlMatchSetRefNumber.Enabled = false;
            }
        }
        public static void SetMatchSetRefNumberUrlForGLDataRecurringSchedule(GridItemEventArgs e, GLDataRecurringItemScheduleInfo oGLDataRecurringItemScheduleInfo, long? AccountID, long? NetAccountID, long? GLDataID)
        {
            ExHyperLink hlMatchSetRefNumber = (ExHyperLink)e.Item.FindControl("hlMatchSetRefNumber");
            hlMatchSetRefNumber.Text = Helper.GetDisplayStringValue(oGLDataRecurringItemScheduleInfo.MatchSetRefNumber);
            bool _IsForwardedItem = !(oGLDataRecurringItemScheduleInfo.CreatedInRecPeriodID == SessionHelper.CurrentReconciliationPeriodID);

            if (!_IsForwardedItem)
            {
                if (!string.IsNullOrEmpty(oGLDataRecurringItemScheduleInfo.MatchSetRefNumber))
                {
                    hlMatchSetRefNumber.Enabled = true;
                    string queryString = "?" + QueryStringConstants.ACCOUNT_ID + "=" + AccountID
                              + "&" + QueryStringConstants.NETACCOUNT_ID + "=" + NetAccountID
                             + "&" + QueryStringConstants.GLDATA_ID + "=" + GLDataID
                             + "&" + QueryStringConstants.MATCH_SET_ID + "=" + oGLDataRecurringItemScheduleInfo.MatchSetID
                             + "&" + QueryStringConstants.MATCHSET_SUBSET_COMBINATION_ID + "=" + oGLDataRecurringItemScheduleInfo.MatchSetSubSetCombinationID
                              + "&" + QueryStringConstants.MATCHING_TYPE_ID + "=" + (int)ARTEnums.MatchingType.AccountMatching;
                    hlMatchSetRefNumber.NavigateUrl = URLConstants.URL_MATCHING_VIEW_MATCH_SET + queryString;
                }
                else
                {
                    hlMatchSetRefNumber.Enabled = false;
                }
            }
            else
            {
                hlMatchSetRefNumber.Enabled = false;

            }
        }

        public static void SetMatchSetRefNumberUrlForGLDataWriteOnOff(GridItemEventArgs e, GLDataWriteOnOffInfo oGLDataWriteOnOffInfo, long? AccountID, long? NetAccountID, long? GLDataID)
        {
            ExHyperLink hlMatchSetRefNumber = (ExHyperLink)e.Item.FindControl("hlMatchSetRefNumber");
            hlMatchSetRefNumber.Text = Helper.GetDisplayStringValue(oGLDataWriteOnOffInfo.MatchSetRefNumber);
            if (!string.IsNullOrEmpty(oGLDataWriteOnOffInfo.MatchSetRefNumber))
            {
                hlMatchSetRefNumber.Enabled = true;
                string queryString = "?" + QueryStringConstants.ACCOUNT_ID + "=" + AccountID
                          + "&" + QueryStringConstants.NETACCOUNT_ID + "=" + NetAccountID
                         + "&" + QueryStringConstants.GLDATA_ID + "=" + GLDataID
                         + "&" + QueryStringConstants.MATCH_SET_ID + "=" + oGLDataWriteOnOffInfo.MatchSetID
                         + "&" + QueryStringConstants.MATCHSET_SUBSET_COMBINATION_ID + "=" + oGLDataWriteOnOffInfo.MatchSetSubSetCombinationID
                          + "&" + QueryStringConstants.MATCHING_TYPE_ID + "=" + (int)ARTEnums.MatchingType.AccountMatching;
                hlMatchSetRefNumber.NavigateUrl = URLConstants.URL_MATCHING_VIEW_MATCH_SET + queryString;
            }
            else
            {
                hlMatchSetRefNumber.Enabled = false;
            }
        }


        /// <summary>
        /// Recalculates the rec item schedule amount.
        /// </summary>
        /// <param name="oGLDataRecurringItemScheduleInfo">The o GL data recurring item schedule info.</param>
        /// <param name="bccyCode">The bccy code.</param>
        /// <param name="netAccountID">The net account ID.</param>
        public static void RecalculateRecItemScheduleAmount(GLDataRecurringItemScheduleInfo oGLDataRecurringItemScheduleInfo, string bccyCode, int? netAccountID)
        {
            if (oGLDataRecurringItemScheduleInfo != null)
            {
                decimal? exRateLccyToBccy = CacheHelper.GetExchangeRate(oGLDataRecurringItemScheduleInfo.LocalCurrencyCode, bccyCode, SessionHelper.CurrentReconciliationPeriodID);
                decimal? exRateLccyToRccy = CacheHelper.GetExchangeRate(oGLDataRecurringItemScheduleInfo.LocalCurrencyCode, SessionHelper.ReportingCurrencyCode, SessionHelper.CurrentReconciliationPeriodID);
                SharedRecItemHelper.RecalculateRecItemScheduleAmount(oGLDataRecurringItemScheduleInfo, bccyCode, SessionHelper.ReportingCurrencyCode, exRateLccyToBccy, exRateLccyToRccy); 
                //if (netAccountID.GetValueOrDefault() == 0)
                //{
                //if (oGLDataRecurringItemScheduleInfo.ExRateLCCYtoBCCY.HasValue)
                //{
                //    oGLDataRecurringItemScheduleInfo.ScheduleAmountBaseCurrency = ConvertAmount(oGLDataRecurringItemScheduleInfo.ScheduleAmount, oGLDataRecurringItemScheduleInfo.ExRateLCCYtoBCCY);
                //    oGLDataRecurringItemScheduleInfo.RecPeriodAmountBaseCurrency = ConvertAmount(oGLDataRecurringItemScheduleInfo.RecPeriodAmountLocalCurrency, oGLDataRecurringItemScheduleInfo.ExRateLCCYtoBCCY);
                //    oGLDataRecurringItemScheduleInfo.BalanceBaseCurrency = ConvertAmount(oGLDataRecurringItemScheduleInfo.BalanceLocalCurrency, oGLDataRecurringItemScheduleInfo.ExRateLCCYtoBCCY);
                //}
                //else
                //{
                //    oGLDataRecurringItemScheduleInfo.ScheduleAmountBaseCurrency = ConvertCurrency(oGLDataRecurringItemScheduleInfo.LocalCurrencyCode, bccyCode, SessionHelper.CurrentReconciliationPeriodID, oGLDataRecurringItemScheduleInfo.ScheduleAmount);
                //    oGLDataRecurringItemScheduleInfo.RecPeriodAmountBaseCurrency = ConvertCurrency(oGLDataRecurringItemScheduleInfo.LocalCurrencyCode, bccyCode, SessionHelper.CurrentReconciliationPeriodID, oGLDataRecurringItemScheduleInfo.RecPeriodAmountLocalCurrency);
                //    oGLDataRecurringItemScheduleInfo.BalanceBaseCurrency = ConvertCurrency(oGLDataRecurringItemScheduleInfo.LocalCurrencyCode, bccyCode, SessionHelper.CurrentReconciliationPeriodID, oGLDataRecurringItemScheduleInfo.BalanceLocalCurrency);
                //}
                ////}
                //if (oGLDataRecurringItemScheduleInfo.ExRateLCCYtoRCCY.HasValue)
                //{
                //    oGLDataRecurringItemScheduleInfo.ScheduleAmountReportingCurrency = ConvertAmount(oGLDataRecurringItemScheduleInfo.ScheduleAmount, oGLDataRecurringItemScheduleInfo.ExRateLCCYtoRCCY);
                //    oGLDataRecurringItemScheduleInfo.RecPeriodAmountReportingCurrency = ConvertAmount(oGLDataRecurringItemScheduleInfo.RecPeriodAmountLocalCurrency, oGLDataRecurringItemScheduleInfo.ExRateLCCYtoRCCY);
                //    oGLDataRecurringItemScheduleInfo.BalanceReportingCurrency = ConvertAmount(oGLDataRecurringItemScheduleInfo.BalanceLocalCurrency, oGLDataRecurringItemScheduleInfo.ExRateLCCYtoRCCY);
                //}
                //else
                //{
                //    oGLDataRecurringItemScheduleInfo.ScheduleAmountReportingCurrency = ConvertCurrency(oGLDataRecurringItemScheduleInfo.LocalCurrencyCode, SessionHelper.ReportingCurrencyCode, SessionHelper.CurrentReconciliationPeriodID, oGLDataRecurringItemScheduleInfo.ScheduleAmount);
                //    oGLDataRecurringItemScheduleInfo.RecPeriodAmountReportingCurrency = ConvertCurrency(oGLDataRecurringItemScheduleInfo.LocalCurrencyCode, SessionHelper.ReportingCurrencyCode, SessionHelper.CurrentReconciliationPeriodID, oGLDataRecurringItemScheduleInfo.RecPeriodAmountLocalCurrency);
                //    oGLDataRecurringItemScheduleInfo.BalanceReportingCurrency = ConvertCurrency(oGLDataRecurringItemScheduleInfo.LocalCurrencyCode, SessionHelper.ReportingCurrencyCode, SessionHelper.CurrentReconciliationPeriodID, oGLDataRecurringItemScheduleInfo.BalanceLocalCurrency);
                //}
            }
        }

        /// <summary>
        /// Converts the currency.
        /// </summary>
        /// <param name="fromCurrencyCode">From currency code.</param>
        /// <param name="toCurrencyCode">To currency code.</param>
        /// <param name="recPeriodID">The rec period ID.</param>
        /// <param name="amount">The amount.</param>
        /// <returns></returns>
        public static decimal? ConvertCurrency(string fromCurrencyCode, string toCurrencyCode, int? recPeriodID, decimal? amount)
        {
            decimal? rate = CacheHelper.GetExchangeRate(fromCurrencyCode, toCurrencyCode, recPeriodID);
            return SharedRecItemHelper.ConvertAmount(amount, rate);
        }

        /// <summary>
        /// Caclculate Unexplained Variance
        /// </summary>
        /// <param name="glBalance"></param>
        /// <param name="adjustments"></param>
        /// <param name="timingDiff"></param>
        /// <param name="supportingDtl"></param>
        /// <param name="writeOnOff"></param>
        /// <returns></returns>
        public static decimal? CaclculateUnexplainedVariance(decimal? glBalance, decimal? adjustments, decimal? timingDiff, decimal? supportingDtl, decimal? writeOnOff)
        {
            return glBalance.GetValueOrDefault() + adjustments.GetValueOrDefault() + timingDiff.GetValueOrDefault() - supportingDtl.GetValueOrDefault() + writeOnOff.GetValueOrDefault();
        }


        public static void SetAccountTaskCount(ExLabel llblPending, int? pendingCount, ExLabel lblCompleted, int? completedCount)
        {
            if (llblPending != null)
            {
                llblPending.Text = Helper.GetDisplayIntegerValueWithBracket(pendingCount, false);
            }
            if (lblCompleted != null)
            {
                lblCompleted.Text = Helper.GetDisplayIntegerValueWithBracket(completedCount, false);
            }
        }

    }
}