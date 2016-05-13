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

public partial class Pages_EditUser : PageBaseCompany
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
        string url = "~/Pages/CreateUser.aspx" + Request.Url.Query + "&" + QueryStringConstants.User_ID + "=" + SessionHelper.GetCurrentUser().UserID;
        Server.Transfer(url);
    }
    #endregion
    #region Grid Events
    #endregion
    #region Other Events
    #endregion
    #region Validation Control Events
    #endregion
    #region Private Methods
    #endregion
    #region Other Methods
    public override string GetMenuKey()
    {
        return "UpdateMyProfile";
    }

    #endregion

}
