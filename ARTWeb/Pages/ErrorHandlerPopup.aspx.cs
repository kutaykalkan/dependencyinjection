using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SkyStem.ART.Web.Utility;
using SkyStem.ART.Web.Classes;
using SkyStem.Language.LanguageUtility;
using SkyStem.ART.Web.Data;

public partial class Pages_ErrorHandlerPopup : PopupPageBase
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
        lblErrorOccured.Text = LanguageUtil.GetValue(1721).ToUpper();

        if (Request.QueryString[QueryStringConstants.ERROR_MESSAGE_LABEL_ID] != null)
        {
            int errorLabelID = Convert.ToInt32(Request.QueryString[QueryStringConstants.ERROR_MESSAGE_LABEL_ID]);

            if (Request.QueryString[QueryStringConstants.ERROR_MESSAGE_SYSTEM] != null)
            {
                if (SessionHelper.CurrentRoleEnum != WebEnums.UserRole.SYSTEM_ADMIN)
                {
                    lblMessage.Text = string.Format(LanguageUtil.GetValue(5000062), LanguageUtil.GetValue(errorLabelID));
                }
                else
                {
                    lblMessage.LabelID = errorLabelID;
                }
            }
            else
            {
                lblMessage.LabelID = errorLabelID;
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
    #endregion

    #region Other Methods
    #endregion

}
