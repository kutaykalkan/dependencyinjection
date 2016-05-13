using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SkyStem.ART.Web.Classes;
using SkyStem.ART.Web.Utility;
using SkyStem.ART.Web.Data;
using SkyStem.ART.Client.Model;
using SkyStem.ART.Client.IServices;
using SkyStem.ART.Client.Exception;
using SkyStem.Language.LanguageUtility;
using SkyStem.ART.Client.Data;

public partial class GLProcessError : Page
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
        if (!string.IsNullOrEmpty(Request.QueryString["ReasonID"]))
        {
            ARTEnums.SystemLockdownReason eSystemLockdownReason = (ARTEnums.SystemLockdownReason)Enum.Parse(typeof(ARTEnums.SystemLockdownReason), Request.QueryString["ReasonID"]);
            SystemLockdownReasonMstInfo oSystemLockdownReasonMstInfo = Helper.GetSystemLockdownReasonMst(eSystemLockdownReason);
            lblErrorMsg.Text = LanguageUtil.GetValue(oSystemLockdownReasonMstInfo.DescriptionLabelID.Value);
        }
    }
    #endregion

    #region Grid Events
    #endregion

    #region Other Events
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        Helper.RedirectToHomePage();
    }
    #endregion

    #region Validation Control Events
    #endregion

    #region Private Methods
    #endregion

    #region Other Methods
    #endregion
   
}
