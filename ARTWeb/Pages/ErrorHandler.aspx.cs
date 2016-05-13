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
using SkyStem.ART.Web.Classes;
using SkyStem.ART.Web.Utility;
using SkyStem.Language.LanguageUtility;
using SkyStem.ART.Web.Data;

public partial class ErrorHandler : PageBase
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
        Helper.SetPageTitle(this, 1051);
        lblErrorOccured.Text = LanguageUtil.GetValue(1721).ToUpper();

        if (Request.QueryString[QueryStringConstants.ERROR_MESSAGE_LABEL_ID] != null)
        {
            int errorLabelID = Convert.ToInt32(Request.QueryString[QueryStringConstants.ERROR_MESSAGE_LABEL_ID]);

            if (Request.QueryString[QueryStringConstants.ERROR_MESSAGE_SYSTEM] != null)
            {
                lblErrorHeader.Visible = false;

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
                lblErrorHeader.Visible = true;
                lblMessage.LabelID = errorLabelID;
            }
        }
        else
        {
            // TODO: Apoorv - Code below not needed so far
            string sourcePage;

            System.Text.StringBuilder strBuilder = new System.Text.StringBuilder();
            System.Text.StringBuilder strErrorBuilder = new System.Text.StringBuilder();
            System.Text.StringBuilder strBuilderMessage = new System.Text.StringBuilder();
            sourcePage = Request.QueryString["aspxerrorpath"];
            strBuilder.Append("PAGE :");
            strBuilder.Append(sourcePage);
            //  Page on which Error Occured
            strBuilder.Append("\r\n");
            strErrorBuilder.Append(sourcePage);
            //  Page on which Error Occured
            strErrorBuilder.Append('\r');
            strBuilder.Append("REFERRER :");
            strBuilder.Append(Request.ServerVariables["HTTP_REFERER"]);
            //  Referrer Page
            strBuilder.Append("\r\n");
            strErrorBuilder.Append(Request.ServerVariables["HTTP_REFERER"]);
            //  Referrer Page
            strErrorBuilder.Append('\r');
            strBuilder.Append("REMOTE ADDR :");
            strBuilder.Append(Request.ServerVariables["REMOTE_ADDR"]);
            //  Remote Address
            strBuilder.Append("\r\n");
            strErrorBuilder.Append(Request.ServerVariables["REMOTE_ADDR"]);
            //  Remote Address
            strErrorBuilder.Append('\r');
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
    public override string GetMenuKey()
    {
        return "";
    }
    #endregion

}
