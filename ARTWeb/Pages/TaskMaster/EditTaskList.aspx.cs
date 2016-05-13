using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SkyStem.ART.Web.Utility;
using SkyStem.ART.Web.Classes;
using SkyStem.ART.Client.Model;
using SkyStem.Library.Controls.WebControls;
using SkyStem.Library.Controls.TelerikWebControls;
using SkyStem.Library.Controls.TelerikWebControls.Common;
using System.Data.SqlClient;
using SkyStem.ART.Web.Data;
using SkyStem.Language.LanguageUtility;
using SkyStem.ART.Client.IServices;
using Telerik.Web.UI;
using SkyStem.ART.Client.Data;
using SkyStem.ART.Client.Exception;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.ServiceModel;
using System.Collections;
using SkyStem.ART.Client.Params;

public partial class Pages_EditTaskList : PopupPageBaseTaskMaster
{
    int _TaskListID;
    string _TaskListName;
    short _TaskListLevel;


    #region Page Events

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            GetQueryStringValues();
            if (!Page.IsPostBack)
            {
                if (_TaskListLevel == 1)
                {
                    PopupHelper.SetPageTitle(this, 2637);
                    lblTaskListName.LabelID = 2584;
                }
                else
                {
                    PopupHelper.SetPageTitle(this, 2977);
                    lblTaskListName.LabelID = 2954;
                }
                SetErrorMessages();
                this.txtNewTaskListName.Text = _TaskListName;
            }
        }
        catch (Exception ex)
        {
            PopupHelper.ShowErrorMessage(this, ex);
        }
    }
    # endregion
    #region Private Methods

    /// <summary>
    /// Get Query String Values
    /// </summary>
    private void GetQueryStringValues()
    {
        if (!String.IsNullOrEmpty(Request.QueryString[QueryStringConstants.TASK_LIST_LEVEL]))
            _TaskListLevel = Convert.ToInt16(Request.QueryString[QueryStringConstants.TASK_LIST_LEVEL]);
        if (_TaskListLevel == 1)
        {
            if (!String.IsNullOrEmpty(Request.QueryString[QueryStringConstants.TASK_LIST_ID]))
                _TaskListID = short.Parse(Request.QueryString[QueryStringConstants.TASK_LIST_ID]);
            if (!String.IsNullOrEmpty(Request.QueryString[QueryStringConstants.TASK_LIST_NAME]))
                _TaskListName = Server.HtmlDecode(Request.QueryString[QueryStringConstants.TASK_LIST_NAME]);
        }
        else
        {
            if (!String.IsNullOrEmpty(Request.QueryString[QueryStringConstants.TASK_SUB_LIST_ID]))
                _TaskListID = short.Parse(Request.QueryString[QueryStringConstants.TASK_SUB_LIST_ID]);
            if (!String.IsNullOrEmpty(Request.QueryString[QueryStringConstants.TASK_SUB_LIST_NAME]))
                _TaskListName = Server.HtmlDecode(Request.QueryString[QueryStringConstants.TASK_SUB_LIST_NAME]);
        }



    }

    private void SetErrorMessages()
    {
        // Set Error Messages
        if (_TaskListLevel == 1)
        {
            txtNewTaskListName.ErrorMessage = Helper.GetErrorMessage(WebEnums.FieldType.MandatoryField, 2579);
            cvTaskListName.ErrorMessage = LanguageUtil.GetValue(5000363);

        }
        else
        {
            txtNewTaskListName.ErrorMessage = Helper.GetErrorMessage(WebEnums.FieldType.MandatoryField, 2955);
            cvTaskListName.ErrorMessage = LanguageUtil.GetValue(5000415);
        }


    }
    # endregion
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {

                if (_TaskListLevel == 1)
                {
                    TaskMasterHelper.EditTaskList(_TaskListID, txtNewTaskListName.Text, SessionHelper.CurrentUserLoginID, System.DateTime.Now);
                }
                else
                {
                    TaskMasterHelper.EditTaskSubList(_TaskListID, txtNewTaskListName.Text, SessionHelper.CurrentUserLoginID, System.DateTime.Now);
                }
                this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "ClosePopupAndRefreshParentPage", ScriptHelper.GetJSForClosePopupAndSubmitParentPage(true));
            }
        }
        catch (Exception ex)
        {
            PopupHelper.ShowErrorMessage(this, ex);
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        string script = PopupHelper.GetScriptForClosingRadWindow();
        ClientScript.RegisterClientScriptBlock(this.GetType(), "CloseWindow", script);
    }
    protected void cvTaskListName_OnServerValidate(object sender, ServerValidateEventArgs args)
    {
        if (!string.IsNullOrEmpty(txtNewTaskListName.Text))
        {
            int TaskListID = 0;
            if (_TaskListLevel == 1)
            {
                TaskListID = TaskMasterHelper.GetTaskListIDByName(txtNewTaskListName.Text.Trim());
            }
            else
            {
                TaskListID = TaskMasterHelper.GetTaskSubListIDByName(txtNewTaskListName.Text.Trim());
            }
            if (TaskListID > 0)
            {
                args.IsValid = false;
                return;
            }
        }
    }


}
