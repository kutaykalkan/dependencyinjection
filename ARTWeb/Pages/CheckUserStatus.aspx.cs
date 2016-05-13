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

using SkyStem.ART.Web.Utility;
using System.Collections.Generic;
using SkyStem.ART.Client.Model;

public partial class Pages_CheckUserStatus : System.Web.UI.Page
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
        if (Request.QueryString["userid"] != null)
        {
            try
            {
                int? userId = Convert.ToInt32(Request.QueryString["userid"]);
                performAjaxFunction(userId);
                Response.End();
            }
            catch (Exception ex)
            {
                Helper.LogException(ex);
            }
        }
    }
    #endregion

    #region Grid Events
    #endregion

    #region Other Events
    #endregion

    #region Validation Control Events
    #endregion

    #region Private Methods
    private void performAjaxFunction(int? id)
    {
        Response.Clear();
        Response.ContentType = "text/xml";
        List<UserHdrInfo> oListUserHdrInfo = CacheHelper.SelectAllUsersForCurrentCompany();
        UserHdrInfo oUser = oListUserHdrInfo.Where(user => user.UserID == id).First();
        bool isActive = false;
        isActive = Helper.IsUserActive(oUser);
        string strReturnValue = isActive.ToString();
        Response.Write(strReturnValue);
    }
    #endregion

    #region Other Methods
    #endregion

}
