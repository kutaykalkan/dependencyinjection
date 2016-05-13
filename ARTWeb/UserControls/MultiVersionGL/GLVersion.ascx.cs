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
using SkyStem.ART.Client.Model;

public partial class UserControls_MultiVersionGL_GLVersion : UserControlGLVersionBase
{

    #region Variables & Constants
    #endregion

    #region Properties
    #endregion

    #region Page Events
    protected void Page_PreRender(object sender, EventArgs e)
    {
        SetMultiversionUrl();
    }

    #endregion

    #region Private Methods
    private void SetMultiversionUrl()
    {
        if (this.IsVersionAvailable)
        {
            hlGLVersion.Visible = true;
            if (this.GLDataID > 0)
                PopupUrl = POPUP_PAGE + QueryStringConstants.GLDATA_ID + "=" + this.GLDataID.ToString();

            hlGLVersion.NavigateUrl = "javascript:OpenRadWindowForHyperlink('" + PopupUrl + "', " + POPUP_HEIGHT + " , " + POPUP_WIDTH + ");";
        }
        else
            hlGLVersion.Visible = false;
    }

    #endregion
}
