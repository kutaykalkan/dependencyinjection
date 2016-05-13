using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using SkyStem.ART.Web.Classes;
using SkyStem.ART.Client.Model;
using SkyStem.ART.Client.IServices;
using SkyStem.ART.Web.Utility;
using System.Collections.Generic;
using SkyStem.Language.LanguageUtility;
using SkyStem.ART.Web.Data;
using SkyStem.ART.Client.Data;
using SkyStem.Library.Controls.WebControls;
public partial class MasterPages_PopUpMasterPage : PopupMasterPageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Session[SessionConstants.DATE_TIME] = DateTime.Now.ToString();
    }

    protected void ScriptManager_OnAsyncPostBackError(object sender, AsyncPostBackErrorEventArgs e)
    {
        Exception objError = this.Server.GetLastError();
        if (objError != null)
        {
            objError = objError.GetBaseException();
            SkyStem.ART.Web.Utility.Helper.LogException(objError);
            this.Server.ClearError();
        }
        ScriptManager1.AsyncPostBackErrorMessage = LanguageUtil.GetValue(5000030); //objError.Message;
    }

    /// <summary>
    /// Show the Notes / Input Requirements Sections
    /// </summary>
    /// <param name="oLabelIDCollection"></param>
    public override void ShowInputRequirements(int[] oLabelIDCollection)
    {
        trInputRequirements.Visible = true;
        ucInputRequirements.Datasource = oLabelIDCollection;
    }

    public override void ShowErrorMessage(string errorMessage)
    {
        lblConfirmationMessage.Visible = false;
        Helper.FormatAndShowErrorMessage(lblErrorMessage, errorMessage);
        upnlMessages.Update();
    }

    public override void ShowErrorMessage(int errorMessageLabelID)
    {
        string errorMessage = LanguageUtil.GetValue(errorMessageLabelID);
        ShowErrorMessage(errorMessage);
    }


    public override void HideErrorMessage()
    {
        lblErrorMessage.Visible = false;
        upnlMessages.Update();
    }

    public override void HideConfirmationMessage()
    {
        lblErrorMessage.Visible = false;
        lblConfirmationMessage.Visible = false;
        upnlMessages.Update();
    }

    public override void ShowConfirmationMessage(string message)
    {
        lblErrorMessage.Visible = false;
        lblConfirmationMessage.Visible = true;
        lblConfirmationMessage.Text = message;
        upnlMessages.Update();
    }

    public override void ShowConfirmationMessage(int confirmationMessageLabelID)
    {
        ShowConfirmationMessage(LanguageUtil.GetValue(confirmationMessageLabelID));
    }

}
