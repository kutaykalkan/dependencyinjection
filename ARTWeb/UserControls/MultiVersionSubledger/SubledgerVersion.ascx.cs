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

public partial class UserControls_MultiVersionSubledger : UserControlSubledgerVersionBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.IsVersionAvailable)
        {
            hlSubledgerVersion.Visible = true;
            if (this.GLDataID > 0)
                PopupUrl = POPUP_PAGE + QueryStringConstants.GLDATA_ID + "=" + this.GLDataID.ToString();

            hlSubledgerVersion.NavigateUrl = "javascript:OpenRadWindowForHyperlink('" + PopupUrl + "', " + POPUP_HEIGHT + " , " + POPUP_WIDTH + ");";
        }
        else
            hlSubledgerVersion.Visible = false;
    }
}
