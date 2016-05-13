using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SkyStem.ART.Web.Utility;
using SkyStem.ART.Web.Data;
using System.Text;

public partial class UserControls_ExchangeRateBar : System.Web.UI.UserControl
{
    private string _LCCYCode = string.Empty;
    string _BCCYCode = string.Empty;
    string _RCCYCode = string.Empty;

    public string LCCYCode
    {
        get { return _LCCYCode; }
        set { _LCCYCode = value; }
    }

    public string BCCYCode
    {
        get { return _BCCYCode; }
        set { _BCCYCode = value; }
    }

    public string RCCYCode
    {
        get { return _RCCYCode; }
        set { _RCCYCode = value; }
    }


    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        lblLCCYtoBCCYValue.Text = GetDisplayExchangeRate(this._LCCYCode, this._BCCYCode); 
        lblLCCYtoRCCYValue.Text = GetDisplayExchangeRate(this._LCCYCode, this._RCCYCode); 
    }

    private string GetDisplayExchangeRate(string fromCurrencyCode, string toCurrencyCode)
    {
        StringBuilder oExchangeRateString = new StringBuilder();
        if (string.IsNullOrEmpty(fromCurrencyCode)
            || string.IsNullOrEmpty(toCurrencyCode)
            || fromCurrencyCode == WebConstants.SELECT_ONE
            || toCurrencyCode == WebConstants.SELECT_ONE)
        {
            oExchangeRateString.Append(WebConstants.HYPHEN);
        }
        else
        {
            oExchangeRateString.Append("(");
            oExchangeRateString.Append(Helper.GetDisplayExchangeRateValue(1M));
            oExchangeRateString.Append(" ");
            oExchangeRateString.Append(fromCurrencyCode);
            oExchangeRateString.Append(" = ");
            oExchangeRateString.Append(Helper.GetDisplayExchangeRateValue(CacheHelper.GetExchangeRate(fromCurrencyCode, toCurrencyCode, SessionHelper.CurrentReconciliationPeriodID)));
            oExchangeRateString.Append(" ");
            oExchangeRateString.Append(toCurrencyCode);
            oExchangeRateString.Append(")");
        }
        return oExchangeRateString.ToString();
    }
}
