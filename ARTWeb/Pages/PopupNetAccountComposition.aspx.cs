using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SkyStem.ART.Web.Classes;
using SkyStem.ART.Web.Data;
using SkyStem.ART.Client.Exception;
using SkyStem.ART.Web.Utility;
using SkyStem.Library.Controls.TelerikWebControls;
using SkyStem.Language.LanguageUtility;

public partial class Pages_PopupNetAccountComposition : PopupPageBase
{

    private int? _NetAccountID = 0;
    private int _ReconciliationPeriodID;
    private int _CompanyID;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
        OnPageLoad();
        }
        catch (Exception ex)
        {
            Helper.ShowErrorMessage((PageBase)this.Page, ex);
        }

    }

    private void OnPageLoad()
    {
        this.Page.Title = LanguageUtil.GetValue(2127);

        if (!string.IsNullOrEmpty(Request.QueryString[QueryStringConstants.NETACCOUNT_ID]))
        {
            _NetAccountID = Convert.ToInt32(Request.QueryString[QueryStringConstants.NETACCOUNT_ID]);
        }

        if (!string.IsNullOrEmpty(Request.QueryString[QueryStringConstants.REC_PERIOD_ID]))
        {
            _ReconciliationPeriodID = Convert.ToInt32(Request.QueryString[QueryStringConstants.REC_PERIOD_ID]);
        }
        _CompanyID = Convert.ToInt32(SessionHelper.CurrentCompanyID);

        SetNetAccountUserControl(_NetAccountID, _ReconciliationPeriodID, _CompanyID);
       


    }

    private void SetNetAccountUserControl(int? netAccountID, int recPeriodID, int companyID)
    {
        ucNetAccountComposition.NetAccountID=netAccountID;
        ucNetAccountComposition.ReconciliationPeriodID=recPeriodID;
        ucNetAccountComposition.CompanyID=companyID;
    }

}
