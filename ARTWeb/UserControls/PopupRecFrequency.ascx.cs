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
using SkyStem.Library.Controls.WebControls;

public partial class UserControls_PopupRecFrequency : System.Web.UI.UserControl
{

    #region "Private Properties"

    private long _AccountID;
    private int _NetAccountID;
    private int _RiskRatingID;

    #endregion

    #region "Public Properties"

    public long AccountID
    {
        get
        {
            return this._AccountID;
        }
        set
        {
            this._AccountID = value;
            string POPUP_PAGE = "PopupRecFrequency.aspx?";
            string PopupUrl = null;
            if (this._AccountID > 0)
                PopupUrl = POPUP_PAGE + QueryStringConstants.ACCOUNT_ID + "=" + this._AccountID.ToString();

            const int POPUP_WIDTH = 400;
            const int POPUP_HEIGHT = 480;

            hlRecFrequency.NavigateUrl = "javascript:OpenRadWindowForHyperlink('" + PopupUrl + "', " + POPUP_HEIGHT + " , " + POPUP_WIDTH + ");";
        }
    }

    public int NetAccountID
    {
        get
        {
            return this._NetAccountID;
        }
        set
        {
            this._NetAccountID = value;
            string POPUP_PAGE = "PopupRecFrequency.aspx?";
            string PopupUrl = null;
            if (this._NetAccountID > 0)
                PopupUrl = POPUP_PAGE + QueryStringConstants.NETACCOUNT_ID + "=" + this._NetAccountID.ToString();
            const int POPUP_WIDTH = 400;
            const int POPUP_HEIGHT = 480;

            hlRecFrequency.NavigateUrl = "javascript:OpenRadWindowForHyperlink('" + PopupUrl + "', " + POPUP_HEIGHT + " , " + POPUP_WIDTH + ");";
        }
    }

    public int RiskRatingRecPeriodID
    {
        get
        {
            return this._RiskRatingID;
        }
        set
        {
            this._RiskRatingID = value;
            string POPUP_PAGE = "PopupRiskRatingRecPeriod.aspx?";
            string PopupUrl = null;
            if (this._RiskRatingID > 0)
                PopupUrl = POPUP_PAGE + QueryStringConstants.RISKRATING_ID + "=" + _RiskRatingID.ToString();
            const int POPUP_WIDTH = 400;
            const int POPUP_HEIGHT = 480;

            hlRecFrequency.NavigateUrl = "javascript:OpenRadWindowForHyperlink('" + PopupUrl + "', " + POPUP_HEIGHT + " , " + POPUP_WIDTH + ");";
        }
    }

    public ExHyperLink  HyperLink
    {
        get
        {
            return this.hlRecFrequency; 
        }
    }
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {

    }
}
