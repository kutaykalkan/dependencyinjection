using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SkyStem.ART.Web.Classes;
using SkyStem.ART.Web.Data;
using SkyStem.ART.Client.Model;
using SkyStem.ART.App.Services;
using SkyStem.ART.Web.Utility;

public partial class Pages_TaskMaster_ViewTaskComments : PopupPageBase
{
    #region Variables
    Int64? _taskDetailID = null;

    #endregion

    #region Page Event
    protected void Page_Load(object sender, EventArgs e)
    {
        this.Title = "Task Comments";
        if (!string.IsNullOrEmpty(Request.QueryString[QueryStringConstants.TASK_DETAIL_ID]))
        {
            _taskDetailID = Convert.ToInt64(Request.QueryString[QueryStringConstants.TASK_DETAIL_ID]);
        }
    }

    #endregion
    #region Private Function
    void LoadData()
    {
        try
        {
            if (_taskDetailID.HasValue)
            {
                TaskMaster oTaskMaster = new TaskMaster();
                ucViewTaskComments.BindGrid(oTaskMaster.SelectAllCommentsByTaskDetailID(_taskDetailID, Helper.GetAppUserInfo()));
            }
        }
        catch (Exception ex)
        {
            Helper.ShowErrorMessage((PageBase)this.Page, ex);
        }
    }

    #endregion

}
