using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SkyStem.ART.Web.Classes.UserControl;
using SkyStem.ART.Web.Utility;
using SkyStem.ART.Web.Classes;
using SkyStem.ART.App.Services;
using SkyStem.ART.Client.Model;
using SkyStem.Library.Controls.WebControls;
using SkyStem.ART.Web.Data;
using SkyStem.ART.Client.IServices;
using SkyStem.Language.LanguageUtility;

public partial class UserControls_TaskMaster_ViewTaskComments : UserControlTaskMasterBase
{
    List<TaskDetailReviewNoteDetailInfo> _DataSource = null;
    long? _taskID = null;
    long? _taskDetailID = null;
    short? _readOnly = null;

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

    public List<TaskDetailReviewNoteDetailInfo> DataSource
    {
        get { return _DataSource; }
        set { _DataSource = value; }
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
        }

        if (!String.IsNullOrEmpty(Request.QueryString[QueryStringConstants.READ_ONLY]))
            _readOnly = Convert.ToInt16(Request.QueryString[QueryStringConstants.READ_ONLY]);

        if (!IsPostBack)
        {
            LoadData();
        }
    }

    protected void rptTaskComments_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        try
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var o = (TaskDetailReviewNoteDetailInfo)e.Item.DataItem;

                ExLabel lblCommentor = (ExLabel)e.Item.FindControl("lblCommenter");
                if (lblCommentor != null)
                {
                    lblCommentor.Text = Helper.GetDisplayUserFullName(o.AddedByUserFirstName, o.AddedByUserLastName);
                }

                ExLabel lblComment = (ExLabel)e.Item.FindControl("lblComment");
                if (lblComment != null)
                {
                    lblComment.Text = o.ReviewNote;
                }

                ExLabel lblDateAdded = (ExLabel)e.Item.FindControl("lblDateAdded");
                if (lblDateAdded != null)
                {
                    lblDateAdded.Text = o.DateAdded.Value.ToShortDateString();
                }

            }
            if (e.Item.ItemType == ListItemType.Footer)
            {
                ExLabel lblEmpty = (ExLabel)e.Item.FindControl("lblEmpty");
                if (lblEmpty != null)
                {
                    lblEmpty.Text = string.Format(LanguageUtil.GetValue(1361),LanguageUtil.GetValue(2587));
                }                
            }
        }
        catch (Exception ex)
        {
            Helper.FormatAndShowErrorMessage(this.Page.Master.FindControl("lblErrorMessage") as ExLabel, ex);
        }
    }

    public void LoadData()
    {
        ITaskMaster oTaskMaster = null;
        try
        {
            oTaskMaster = RemotingHelper.GetTaskMasterObject();
            //TaskDetailID = 1;
            DataSource = oTaskMaster.SelectAllCommentsByTaskDetailID(TaskDetailID, Helper.GetAppUserInfo());
            BindGrid(DataSource);
        }
        catch (Exception ex)
        {
            Helper.FormatAndShowErrorMessage(this.Page.Master.FindControl("lblErrorMessage") as ExLabel, ex);
        }
    }

    public void BindGrid(List<TaskDetailReviewNoteDetailInfo> GridData)
    {
        rptTaskComments.DataSource = GridData;
        rptTaskComments.DataBind();
        //}
        //else
        //{
        //    Helper.FormatAndShowErrorMessage(this.Page.Master.FindControl("lblErrorMessage") as ExLabel, string.Format(LanguageUtil.GetValue(1361),LanguageUtil.GetValue(2587)));
        //}
    }
}
