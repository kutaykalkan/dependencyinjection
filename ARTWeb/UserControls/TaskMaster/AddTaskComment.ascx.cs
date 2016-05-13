using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SkyStem.ART.Web.Classes.UserControl;
using SkyStem.ART.Web.Data;
using SkyStem.ART.Web.Utility;
using SkyStem.ART.Client.IServices;
using SkyStem.Library.Controls.WebControls;

public partial class UserControls_TaskMaster_AddTaskComment : UserControlTaskMasterBase
{
    long? _taskID = null;
    long? _taskDetailID = null;
    string _mode = null;
    int? _recordTypeID = null;
    bool _showAttachementControl = true;
    List<long> _taskDetailIDs = null;
    bool _disableAttachementControl = false;


    public long? TaskID
    {
        get { return _taskID; }
        set { _taskID = value; }
    }

    public List<long> TaskDetailIDs
    {
        get { return _taskDetailIDs; }
        set { _taskDetailIDs = value; }
    }

    public string Text
    {
        get { return txtComments.Text.Trim(); }
    }

    public bool ShowAttachementControl
    {
        get { return _showAttachementControl; }
        set { _showAttachementControl = value; }
    }
    public bool DisAbleAttachementControl
    {
        get { return _disableAttachementControl; }
        set { _disableAttachementControl = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request.QueryString[QueryStringConstants.TASK_ID]))
        {
            _taskID = Convert.ToInt64(Request.QueryString[QueryStringConstants.TASK_ID]);
        }

        if (!string.IsNullOrEmpty(Request.QueryString[QueryStringConstants.TASK_DETAIL_ID]))
        {
            _taskDetailID = Convert.ToInt64(Request.QueryString[QueryStringConstants.TASK_DETAIL_ID]);
            if (_taskDetailIDs == null)
                _taskDetailIDs = new List<long>();
            _taskDetailIDs.Add(_taskDetailID.Value);
        }

        if (!string.IsNullOrEmpty(Request.QueryString[QueryStringConstants.MODE]))
        {
            _mode = Request.QueryString[QueryStringConstants.MODE];
        }

        if (!string.IsNullOrEmpty(Request.QueryString[QueryStringConstants.RECORD_TYPE_ID]))
        {
            _recordTypeID = Convert.ToInt32(Request.QueryString[QueryStringConstants.RECORD_TYPE_ID]);
        }

        //txtComments.Attributes["ontextchanged"] = "javascript:OpenRadWindowFromRadWindow(this)";

        if (!Page.IsPostBack)
        {
            if (ShowAttachementControl)
            {
                pnlAttachmnet.Visible = true;
                string _PopupUrl = string.Empty;
                _PopupUrl = SetDocumentUploadURL();
                //hlAttachment.NavigateUrl = "javascript:OpenRadWindowForHyperlink('" + _PopupUrl + "', " + WebConstants.POPUP_HEIGHT + " , " + WebConstants.POPUP_WIDTH + ");";        
                hlAttachment.NavigateUrl = "javascript:OpenRadWindowFromRadWindow('" + _PopupUrl + "', " + WebConstants.POPUP_HEIGHT + " , " + WebConstants.POPUP_WIDTH + ");";
            }
            else
            {
                pnlAttachmnet.Visible = false;
            }
        }
        hlAttachment.Enabled = !DisAbleAttachementControl;
    }

    public string SetDocumentUploadURL()
    {
        //if (!TaskID.HasValue && !_taskDetailID.HasValue)
        //    return ScriptHelper.GetJSForEmptyURL();
        //else
        //{
        string windowName;
        long? _recordID = null;
        if (_taskID.HasValue)
            _recordID = _taskID;
        else if (_taskDetailID.HasValue)
            _recordID = _taskDetailID;
        return this.ResolveUrl(Helper.SetDocumentUploadURLForTasks(_recordID, _recordTypeID, _mode, out windowName));
        //}
    }

    public bool SaveTaskComment()
    {
        bool flag = false;
        ITaskMaster oTaskMaster = null;
        try
        {
            oTaskMaster = RemotingHelper.GetTaskMasterObject();

            oTaskMaster.AddTasksComment(TaskDetailIDs, string.Empty, txtComments.Text.Trim(), SessionHelper.CurrentUserID.Value, SessionHelper.CurrentUserLoginID, System.DateTime.Now, Helper.GetAppUserInfo());
            flag = true;
            txtComments.Text = string.Empty;
        }
        catch (Exception ex)
        {
            flag = false;
            Helper.FormatAndShowErrorMessage(this.Page.Master.FindControl("lblErrorMessage") as ExLabel, ex);
        }

        return flag;
    }
}
