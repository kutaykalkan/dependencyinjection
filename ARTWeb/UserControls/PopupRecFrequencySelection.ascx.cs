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

public partial class UserControls_PopupRecFrequencySelection : System.Web.UI.UserControl
{
    private string _URL = string.Empty;
    public string URL
    {
        set
        {
            this._URL = value;

            this._URL += "&" + QueryStringConstants.HL_REC_FREQUENCY_ID + "=" + this.hlRecFrequency.ClientID;
            const int POPUP_WIDTH = 400;
            const int POPUP_HEIGHT = 520;

            hlRecFrequency.NavigateUrl = "javascript:OpenRadWindowForHyperlink('" + this._URL + "', " + POPUP_HEIGHT + " , " + POPUP_WIDTH + ");";
        }
    }
    public ExHyperLink HyperLink
    {
        get
        {
            return this.hlRecFrequency;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {

    }
}
