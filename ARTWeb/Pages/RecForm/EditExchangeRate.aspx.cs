using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SkyStem.ART.Web.Classes;
using SkyStem.ART.Web.Utility;
using SkyStem.ART.Web.Data;

public partial class Pages_RecForm_EditExchangeRate : PopupPageBase
{
    long? _AccountID = null;
    long? _NetAccountID = null;
    decimal? _ExRateLccyToBccy = null;
    decimal? _ExRateLccyToRccy = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        PopupHelper.SetPageTitle(this, 2487);
        SetErrorMessage();
        GetQueryStringValues();
        SetControlState();
        if (!Page.IsPostBack)
            PopulateData();
    }

    /// <summary>
    /// Sets the error message.
    /// </summary>
    private void SetErrorMessage()
    {
        rfvLCCYtoBCCY.ErrorMessage = Helper.GetErrorMessage(WebEnums.FieldType.MandatoryField, lblLCCYtoBCCY.LabelID);
        rfvLCCYtoRCCY.ErrorMessage = Helper.GetErrorMessage(WebEnums.FieldType.MandatoryField, lblLCCYtoRCCY.LabelID);
        cvLCCYtoBCCY.ErrorMessage = Helper.GetErrorMessage(WebEnums.FieldType.InvalidNumericField, lblLCCYtoBCCY.LabelID);
        cvLCCYtoRCCY.ErrorMessage = Helper.GetErrorMessage(WebEnums.FieldType.InvalidNumericField, lblLCCYtoRCCY.LabelID);
    }

    /// <summary>
    /// Gets the query string values.
    /// </summary>
    private void GetQueryStringValues()
    {
        if (!string.IsNullOrEmpty(Request.QueryString[QueryStringConstants.ACCOUNT_ID]))
            this._AccountID = Convert.ToInt64(Request.QueryString[QueryStringConstants.ACCOUNT_ID]);

        if (!string.IsNullOrEmpty(Request.QueryString[QueryStringConstants.NETACCOUNT_ID]))
            this._NetAccountID = Convert.ToInt32(Request.QueryString[QueryStringConstants.NETACCOUNT_ID]);

        if (!String.IsNullOrEmpty(Request.QueryString[QueryStringConstants.EXCHANGE_RATE_LCCY_TO_BCCY]))
            _ExRateLccyToBccy = Convert.ToDecimal(Request.QueryString[QueryStringConstants.EXCHANGE_RATE_LCCY_TO_BCCY]);
        if (!String.IsNullOrEmpty(Request.QueryString[QueryStringConstants.EXCHANGE_RATE_LCCY_TO_RCCY]))
            _ExRateLccyToRccy = Convert.ToDecimal(Request.QueryString[QueryStringConstants.EXCHANGE_RATE_LCCY_TO_RCCY]);
    }

    /// <summary>
    /// Populates the data.
    /// </summary>
    private void PopulateData()
    {
        if (_ExRateLccyToBccy.HasValue)
            txtLCCYtoBCCY.Text = Helper.GetDisplayExchangeRateValue(_ExRateLccyToBccy);
        if (_ExRateLccyToRccy.HasValue)
            txtLCCYtoRCCY.Text = Helper.GetDisplayExchangeRateValue(_ExRateLccyToRccy);
    }

    /// <summary>
    /// Sets the state of the control.
    /// </summary>
    private void SetControlState()
    {
        //if (_NetAccountID.GetValueOrDefault() > 0)
        //    pnlBCCY.Visible = false;
    }
}
