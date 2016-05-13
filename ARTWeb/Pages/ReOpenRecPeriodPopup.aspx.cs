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
using SkyStem.ART.Client.Model;
using SkyStem.ART.Web.Data;
using System.Collections.Generic;
using SkyStem.ART.Client.IServices;
using SkyStem.ART.Client.Data;
using SkyStem.Language.LanguageUtility;

public partial class Pages_ReOpenRecPeriodPopup : PopupPageBase
{
    #region Variables & Constants
    string _ParentHiddenField = null;

    private ReconciliationPeriodInfo oReconciliationPeriodInfo
    {
        get
        {
            return (ReconciliationPeriodInfo)ViewState["oReconciliationPeriodInfo"];
        }
        set { ViewState["oReconciliationPeriodInfo"] = value; }
    }
    List<HolidayCalendarInfo> oHolidayCalendarInfoCollection = null;
    IList<WeekDayMstInfo> oWeekDayMstInfoCollection = null;
    IList<CompanyWeekDayInfo> oCompanyWorkWeekInfoCollectionForFincncialyear = null;
    int? financialYearID = null;
    #endregion

    #region Properties
    #endregion

    #region Delegates & Events
    #endregion

    #region Page Events
    protected void Page_Load(object sender, EventArgs e)
    {
        PopupHelper.SetPageTitle(this, 2982);
        if (!Page.IsPostBack)
        {
            SetErrorMessage();
            LoadData();
        }
    }
    #endregion

    #region Grid Events
    #endregion

    #region Other Events
    protected void btnOk_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            DateTime CalDate;
            if (oReconciliationPeriodInfo !=null && DateTime.TryParse(calCloseOrLockDownDate.Text, out CalDate))
            {
                oReconciliationPeriodInfo.ReconciliationCloseDate = CalDate;
                oReconciliationPeriodInfo.RevisedBy = SessionHelper.CurrentUserLoginID;
                oReconciliationPeriodInfo.DateRevised = System.DateTime.Now;
                IReconciliationPeriod oReconciliationPeriodClient = RemotingHelper.GetReconciliationPeriodObject();
                int rowsAffected = oReconciliationPeriodClient.ReOpenRecPeriod(oReconciliationPeriodInfo, (short)ARTEnums.ActionType.ReopenRecPeriodFromUI, (short)ARTEnums.ChangeSource.ReopenRecPeriodFromUI, Helper.GetAppUserInfo());
                //Clear cache so that latest information is loaded
                //string cacheKey = CacheHelper.GetCacheKeyForCompanyData(CacheConstants.ALL_RECONCILIATION_PERIODS_BASED_ON_FY);
                //CacheHelper.ClearCache(cacheKey);
                //SessionHelper.ClearSession(SessionConstants.REC_PERIOD_STATUS_INFO);
                if (this._ParentHiddenField != null)
                {
                    this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "SetHiddenFieldStatus", ScriptHelper.GetJSToSetParentWindowElementValue(this._ParentHiddenField, "1")); // 1 means Reload data of GridVieww
                }
                this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "ClosePopupAndRefreshParentPage", ScriptHelper.GetJSForClosePopupAndSubmitParentPage());
            }
        }

    }
    #endregion

    #region Validation Control Events
    protected void cvValidateCloseOrLockDownDate_OnServerValidate(object sender, ServerValidateEventArgs args)
    {
        DateTime CalDate;
        if (DateTime.TryParse(calCloseOrLockDownDate.Text, out CalDate))
        {
            if (oReconciliationPeriodInfo != null)
            {
                List<DateTime> AllDueDateList = new List<DateTime>();
                List<DateTime> recCloseOrCertLockDownDateList = new List<DateTime>();
                string errorMessage = LanguageUtil.GetValue(5000342);
                if (oReconciliationPeriodInfo.PreparerDueDate.HasValue)
                    AllDueDateList.Add(Convert.ToDateTime(oReconciliationPeriodInfo.PreparerDueDate.Value));
                if (oReconciliationPeriodInfo.ReviewerDueDate.HasValue)
                    AllDueDateList.Add(Convert.ToDateTime(oReconciliationPeriodInfo.ReviewerDueDate.Value));
                if (oReconciliationPeriodInfo.ApproverDueDate.HasValue)
                    AllDueDateList.Add(Convert.ToDateTime(oReconciliationPeriodInfo.ApproverDueDate.Value));
                if (oReconciliationPeriodInfo.CertificationStartDate.HasValue)
                    AllDueDateList.Add(Convert.ToDateTime(oReconciliationPeriodInfo.CertificationStartDate.Value));
                AllDueDateList.Add(CalDate);
                if (oReconciliationPeriodInfo.PreviousReconciliationCloseDate.HasValue)
                    recCloseOrCertLockDownDateList.Add(Convert.ToDateTime(oReconciliationPeriodInfo.PreviousReconciliationCloseDate.Value));
                recCloseOrCertLockDownDateList.Add(CalDate);
                if (oReconciliationPeriodInfo.NextReconciliationCloseDate.HasValue)
                    recCloseOrCertLockDownDateList.Add(Convert.ToDateTime(oReconciliationPeriodInfo.NextReconciliationCloseDate.Value));

                if (!Helper.DatesInOrder(AllDueDateList))
                {
                    args.IsValid = false;
                    string ermsg = string.Format(errorMessage, LanguageUtil.GetValue(1368));
                    PopupHelper.ShowErrorMessage(this, ermsg);
                }
                else if (!Helper.DatesInOrder(recCloseOrCertLockDownDateList))
                {
                    args.IsValid = false;
                    string ermsg = string.Format(errorMessage, LanguageUtil.GetValue(1419));
                    PopupHelper.ShowErrorMessage(this, ermsg);
                }
            }
            if (oReconciliationPeriodInfo != null && IsDateOnHoliday(calCloseOrLockDownDate.Text, Convert.ToInt32(oReconciliationPeriodInfo.ReconciliationPeriodID)))
            {
                args.IsValid = false;
                PopupHelper.ShowErrorMessage(this, LanguageUtil.GetValue(5000085));
            }
        }
        else
        {
            args.IsValid = false;
            PopupHelper.ShowErrorMessage(this, LanguageUtil.GetValue(2984));
        }
    }
    #endregion

    #region Private Methods
    private void SetErrorMessage()
    {
        rfvCloseOrLockDownDate.ErrorMessage = string.Format(LanguageUtil.GetValue(5000006), LanguageUtil.GetValue(1419));
    }
    private void LoadData()
    {

        if (Request.QueryString[QueryStringConstants.PARENT_HIDDEN_FIELD] != null)
            this._ParentHiddenField = Request.QueryString[QueryStringConstants.PARENT_HIDDEN_FIELD];
        oReconciliationPeriodInfo = Helper.GetReconciliationPeriodInfoForReopen();
        if (oReconciliationPeriodInfo != null)
        {
            string msg = LanguageUtil.GetValue(2983);
            lblMsg.Text = string.Format(msg, Helper.GetDisplayDate(oReconciliationPeriodInfo.PeriodEndDate));
        }
        else
        {
            calCloseOrLockDownDate.Visible = false;
            btnOk.Visible = false;
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
            if (ViewState["HolidayCalendarInfoCollection"] == null)
            {
                // Fetch from DB
                IHolidayCalendar oHolidayCalendarClient = RemotingHelper.GetHolidayCalendarObject();
                oHolidayCalendarInfoCollection = oHolidayCalendarClient.GetHolidayCalendarByCompanyID(SessionHelper.CurrentCompanyID.Value, Helper.GetAppUserInfo());
                ViewState["HolidayCalendarInfoCollection"] = oHolidayCalendarInfoCollection;
            }
            else
            {
                oHolidayCalendarInfoCollection = (List<HolidayCalendarInfo>)ViewState["HolidayCalendarInfoCollection"];
            }
            foreach (HolidayCalendarInfo oHolidayCalendarInfo in oHolidayCalendarInfoCollection)
            {
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
    private IList<CompanyWeekDayInfo> SetCompanyWorkWeekByRecPeriodID(int RecPeriodID)
    {
        financialYearID = SessionHelper.CurrentFinancialYearID;
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
    #endregion

    #region Other Methods
    #endregion

}
