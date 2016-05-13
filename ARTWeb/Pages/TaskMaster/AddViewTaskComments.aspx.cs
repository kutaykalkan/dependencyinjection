using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SkyStem.ART.Web.Classes;
using SkyStem.ART.Web.Data;
using SkyStem.ART.App.Services;
using SkyStem.ART.Web.Utility;
using SkyStem.ART.Client.IServices;

public partial class Pages_TaskMaster_AddViewTaskComments : PopupPageBase
{
    #region Variables

    long? _taskID = null;
    long? _taskDetailID = null;
    short? _readOnly = null;

    #endregion

    #region Properties

    public long? TaskID
    {
        get { return _taskID; }
        set { _taskID = value; }
    }

    public long? TaskDetailID
    {
        get { return _taskDetailID; }
        set { _taskDetailID = value; }
    }

    #endregion

    #region Page Events

    protected void Page_Load(object sender, EventArgs e)
    {
        this.Title = "Task Comment";

        if (!string.IsNullOrEmpty(Request.QueryString[QueryStringConstants.TASK_ID]))
        {
            _taskID = Convert.ToInt64(Request.QueryString[QueryStringConstants.TASK_ID]);
        }

        if (!string.IsNullOrEmpty(Request.QueryString[QueryStringConstants.TASK_DETAIL_ID]))
        {
            _taskDetailID = Convert.ToInt64(Request.QueryString[QueryStringConstants.TASK_DETAIL_ID]);
        }
        if (!string.IsNullOrEmpty(Request.QueryString[QueryStringConstants.READ_ONLY]))
        {
            _readOnly = Convert.ToInt16(Request.QueryString[QueryStringConstants.READ_ONLY]);
        }

        if (_readOnly.HasValue && Convert.ToBoolean(_readOnly) == true)
        {
            pnlAddComment.Visible = false;
        }
    }

    protected override void OnLoadComplete(EventArgs e)
    {
        base.OnLoadComplete(e);
    }


    #endregion

    #region Private Function

    void LoadData()
    {
        ITaskMaster oTaskMaster = null;
        try
        {
            if (_taskDetailID.HasValue)
            {
                oTaskMaster = RemotingHelper.GetTaskMasterObject();
                ucViewTaskComments.BindGrid(oTaskMaster.SelectAllCommentsByTaskDetailID(_taskDetailID, Helper.GetAppUserInfo()));
            }
        }
        catch (Exception ex)
        {
            Helper.ShowErrorMessage((PageBase)this.Page, ex);
        }
        finally
        {
            oTaskMaster = null;
        }
    }

    #endregion

    #region Control Event

    protected void btnSave_Click(object sender, EventArgs e)
    {
        // code to save the comments
        if (ucAddTaskComments.SaveTaskComment())
        {
            //Bind Grid Again to show addes comments
            LoadData();
        }
    }


    #endregion

}
