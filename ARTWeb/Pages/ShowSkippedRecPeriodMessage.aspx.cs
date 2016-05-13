using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SkyStem.ART.Web.Classes;
using SkyStem.ART.Web.Utility;

public partial class Pages_ShowSkippedRecPeriodMessage : PopupPageBase
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
        PopupHelper.SetPageTitle(this, 1051);
    }
    #endregion

    #region Grid Events
    #endregion

    #region Other Events
    protected void btnOk_Click(object sender, EventArgs e)
    {
        this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "ClosePopupAndRefreshParentPage", ScriptHelper.GetJSForClosePopupAndSubmitParentPage(true));
    }
    #endregion

    #region Validation Control Events
    #endregion

    #region Private Methods
    #endregion

    #region Other Methods
    #endregion

}
