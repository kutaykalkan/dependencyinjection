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
using System.Text;
using SkyStem.ART.Web.Utility;
using SkyStem.ART.Web.Classes;
using SkyStem.Language.LanguageUtility;

public partial class Pages_ConfirmPopUp : PopupPageBase
{

    #region Variables & Constants
    string selectedTemporaryValueDDL = "";
    string selectedValueInDB = "";
    #endregion

    #region Properties
    #endregion

    #region Delegates & Events
    #endregion

    #region Page Events
    protected void Page_Load(object sender, EventArgs e)
    {
        string msgNoOfSkippedPeriods = "";
        string msgListOfSkippedPeriods = "";
        PopupHelper.SetPageTitle(this, 5000133);
        if (!string.IsNullOrEmpty(Request.QueryString["msgNoOfSkippedPeriods"]))
            //lblSkippedMsgPopUp.Text   = (Request.QueryString[QueryStringConstants.CONFIRMATION_MESSAGE_LABEL_ID]).ToString();
            msgNoOfSkippedPeriods = (Request.QueryString["msgNoOfSkippedPeriods"]).ToString();


        if (!string.IsNullOrEmpty(Request.QueryString["msgListOfSkippedPeriods"]))
            msgListOfSkippedPeriods = (Request.QueryString["msgListOfSkippedPeriods"]).ToString();
        msgListOfSkippedPeriods = msgListOfSkippedPeriods.TrimStart(',');
        string[] lstPeriod = msgListOfSkippedPeriods.Split(',');
        string displayPeriod = "";
        if (lstPeriod.Length > 0)
        {
            for (int i = 0; i < lstPeriod.Length; i++)
            {
                displayPeriod = displayPeriod + "<br/>" + lstPeriod[i];
            }
        }

        //if (!string.IsNullOrEmpty(Request.QueryString["selectedIndexDDL"]))
        //    s = (Request.QueryString["selectedIndexDDL"]).ToString();

        if (!string.IsNullOrEmpty(Request.QueryString["selectedTemporaryValueDDL"]))
            selectedTemporaryValueDDL = (Request.QueryString["selectedTemporaryValueDDL"]).ToString();

        if (!string.IsNullOrEmpty(Request.QueryString["selectedValueInDB"]))
            selectedValueInDB = (Request.QueryString["selectedValueInDB"]).ToString();


        //Before 05/08/2010
        //"You are skipping {0} Periods. List of Skipped Periods is following:  {1} <br/><br/>You will not be able to reconcile the Skipped Periods once you press SAVE button on the System Wide Settings Page. Are you sure You want to skip them? "
        //After 05/08/2010
        //"You are skipping the following periods: {0} <br/>You will not be able to reconcile the Skipped Periods once you click the SAVE button on the System Wide Settings Page.<br/>Are you sure you want to skip the period (s) stated above ?"
        string msg = LanguageUtil.GetValue(5000134); //"You are skipping {0} Periods. List of Skipped Periods is following:  {1} <br/> Are you sure You want to skip them. <br/>  You will not be able to reconcile them once you press SAVE button";
        //lblSkippedMsgPopUp.Text = string.Format(msg, msgNoOfSkippedPeriods, msgListOfSkippedPeriods);
        //lblSkippedMsgPopUp.Text = string.Format(msg, msgNoOfSkippedPeriods, displayPeriod);
        lblSkippedMsgPopUp.Text = string.Format(msg, displayPeriod);
    }
    #endregion

    #region Grid Events
    #endregion

    #region Other Events
    protected void btnSkippedOK_Click(object sender, EventArgs e)
    {
        string scriptKey = "SetcountOfDocumentAttached";
        // Render JS to Open the grid Customization Window, 
        StringBuilder script = new StringBuilder();
        ScriptHelper.AddJSStartTag(script);

        script.Append("function ConfirmForSkipMessage()");
        script.Append(System.Environment.NewLine);
        script.Append("{");
        script.Append(System.Environment.NewLine);
        script.Append("GetRadWindow().BrowserWindow.DoPostBackForSkipping() ;");
        script.Append("GetRadWindow().Close() ;");
        script.Append(System.Environment.NewLine);
        script.Append("}");
        script.Append(System.Environment.NewLine);
        script.Append("ConfirmForSkipMessage();");
        ScriptHelper.AddJSEndTag(script);
        if (!this.Page.ClientScript.IsStartupScriptRegistered(this.GetType(), scriptKey))
        {
            this.Page.ClientScript.RegisterStartupScript(this.GetType(), scriptKey, script.ToString());
        }

    }

    protected void btnSkippedCancel_Click(object sender, EventArgs e)
    {
        string scriptKey = "SetcountOfDocumentAttached";
        // Render JS to Open the grid Customization Window, 
        StringBuilder script = new StringBuilder();
        ScriptHelper.AddJSStartTag(script);

        script.Append("function ConfirmForSkipMessage()");
        script.Append(System.Environment.NewLine);
        script.Append("{");
        script.Append(System.Environment.NewLine);
        script.Append("GetRadWindow().BrowserWindow.CancelSkippedPopup('" + selectedTemporaryValueDDL + "','" + selectedValueInDB + "') ;");
        script.Append("GetRadWindow().Close() ;");
        script.Append(System.Environment.NewLine);
        script.Append("}");
        script.Append(System.Environment.NewLine);
        script.Append("ConfirmForSkipMessage();");
        ScriptHelper.AddJSEndTag(script);
        if (!this.Page.ClientScript.IsStartupScriptRegistered(this.GetType(), scriptKey))
        {
            this.Page.ClientScript.RegisterStartupScript(this.GetType(), scriptKey, script.ToString());
        }

    }
    #endregion

    #region Validation Control Events
    #endregion

    #region Private Methods
    #endregion

    #region Other Methods
    #endregion

}//end of class
