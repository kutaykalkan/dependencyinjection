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
using SkyStem.ART.Web.Classes;
using SkyStem.ART.Client.IServices;
using SkyStem.ART.Client.Model;
using System.Collections.Generic;
using SkyStem.Language.LanguageUtility;
using System.Text;
using SkyStem.ART.Client.Exception;

public partial class CreateFinancialYear : PageBase
{
    #region Variables & Constants
    #endregion

    #region Properties
    #endregion

    #region Delegates & Events
    #endregion

    #region Page Events
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            Helper.SetPageTitle(this, 2009);
            Helper.SetBreadcrumbs(this, 1207, 2009);
            SetErrormsg();

            if (!IsPostBack)
            {
                btnCancel.PostBackUrl = Helper.GetHomePageUrl();
                BindAndSelectFinancialYearDDL();
            }
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
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        FinancialYearHdrInfo oFinancialYearHdrInfo = new FinancialYearHdrInfo();
        oFinancialYearHdrInfo.FinancialYear = txtFinancialName.Text;
        string datePart;
        oFinancialYearHdrInfo.StartDate = Convert.ToDateTime(calStartDate.Text);
        datePart = Convert.ToDateTime(calStartDate.Text).ToShortDateString();
        oFinancialYearHdrInfo.EndDate = Convert.ToDateTime(calEndDate.Text);
        oFinancialYearHdrInfo.CompanyID = SessionHelper.CurrentCompanyID;
        if (ddlFinancialYear.SelectedValue != WebConstants.CREATE_NEW)
        {
            // EDIT Mode
            try
            {
                int FY_id = 0;
                FY_id = Convert.ToInt32(ddlFinancialYear.SelectedValue);
                oFinancialYearHdrInfo.FinancialYearID = FY_id;
                oFinancialYearHdrInfo.RevisedBy = SessionHelper.CurrentUserLoginID;
                oFinancialYearHdrInfo.DateRevised = DateTime.Now;
                ICompany oCompanyClient = RemotingHelper.GetCompanyObject();
                bool FYupdatesucess = oCompanyClient.UpdateFinancialYear(oFinancialYearHdrInfo, Helper.GetAppUserInfo());
                // Reload
                //Clearing cached data for Financial Year here .
                string cacheKey = CacheHelper.GetCacheKeyForCompanyData(CacheConstants.ALL_FINANCIAL_YEAR_LIST);
                CacheHelper.ClearCache(cacheKey);
                string msg = string.Format(LanguageUtil.GetValue(2085), LanguageUtil.GetValue(2011));
                Helper.ShowConfirmationMessage(this, msg);

                MasterPageBase oMasterPageBase = (MasterPageBase)this.Master;
                // Reload the Rec Periods and also the Status / Countdown
                oMasterPageBase.ReloadRecPeriods();

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
        else
        {
            // ADD Mode

            try
            {

                oFinancialYearHdrInfo.AddedBy = SessionHelper.CurrentUserLoginID;
                oFinancialYearHdrInfo.DateAdded = DateTime.Now;
                ICompany oCompanyClient = RemotingHelper.GetCompanyObject();
                bool FYupdatesucess = oCompanyClient.InsertFinancialYear(oFinancialYearHdrInfo, Helper.GetAppUserInfo());
                // Reload
                string msg = string.Format(LanguageUtil.GetValue(1530), LanguageUtil.GetValue(2011));
                Helper.ShowConfirmationMessage(this, msg);
                //Clearing cached data for Financial Year here .
                string cacheKey = CacheHelper.GetCacheKeyForCompanyData(CacheConstants.ALL_FINANCIAL_YEAR_LIST);
                CacheHelper.ClearCache(cacheKey);
                BindAndSelectFinancialYearDDL();
                calStartDate.Text = "";
                calEndDate.Text = "";
                txtFinancialName.Text = "";

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
    }

    protected void ddlFinancialYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        Helper.HideMessage(this);
        ICompany oCompanyClient = RemotingHelper.GetCompanyObject();
        FinancialYearHdrInfo oFinancialYearHdrInfo = new FinancialYearHdrInfo();
        if (ddlFinancialYear.SelectedValue == WebConstants.CREATE_NEW)
        {
            calStartDate.Text = "";
            calEndDate.Text = "";
            txtFinancialName.Text = "";
        }
        else
        {
            int? fyid = Convert.ToInt32(ddlFinancialYear.SelectedValue);

            oFinancialYearHdrInfo = oCompanyClient.GetFinancialYearByID(fyid, Helper.GetAppUserInfo());
            if (oFinancialYearHdrInfo != null)
            {
                calStartDate.Text = Helper.GetDisplayDate(oFinancialYearHdrInfo.StartDate);
                calEndDate.Text = Helper.GetDisplayDate(oFinancialYearHdrInfo.EndDate);
                txtFinancialName.Text = oFinancialYearHdrInfo.FinancialYear;
            }
        }
    }
    #endregion

    #region Validation Control Events
    #endregion

    #region Private Methods
    private void SetErrormsg()
    {
        txtFinancialName.ErrorMessage = Helper.GetErrorMessage(WebEnums.FieldType.MandatoryField, 2010);
        cvCompareWithStartDate.ErrorMessage = Helper.GetErrorMessage(WebEnums.FieldType.DateCompareField, 1449, 1450);
        cvCalenderStartDate.ErrorMessage = Helper.GetErrorMessage(WebEnums.FieldType.DateFormatField, 1449);
        cvcalEndDate.ErrorMessage = Helper.GetErrorMessage(WebEnums.FieldType.DateFormatField, 1450);
        rfvCalenderStartDate.ErrorMessage = Helper.GetErrorMessage(WebEnums.FieldType.MandatoryField, 1449);
        rfvCalenderEndDate.ErrorMessage = Helper.GetErrorMessage(WebEnums.FieldType.MandatoryField, 1450);
    }
    private void BindAndSelectFinancialYearDDL()
    {
        ListControlHelper.BindFinancialYearDropdown(ddlFinancialYear, false);
        ListControlHelper.AddListItemForCreateNew(ddlFinancialYear, 0);
    }
    #endregion

    #region Other Methods
    public override string GetMenuKey()
    {
        return "CreateFinancialYear";
    }
    #endregion
  
}
