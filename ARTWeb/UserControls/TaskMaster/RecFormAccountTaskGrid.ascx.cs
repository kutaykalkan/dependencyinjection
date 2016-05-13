using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SkyStem.Library.Controls.WebControls;
using SkyStem.ART.Web.Classes;
using SkyStem.ART.Web.Utility;
using SkyStem.ART.Client.Data;
using Telerik.Web.UI;
using SkyStem.ART.Web.Data;
using SkyStem.ART.Client.Model;

public partial class UserControls_TaskMaster_RecFormAccountTaskGrid : UserControlRecItemBase
{
    #region Local Variables

    ExImageButton ToggleControl;
    private bool? _IsExpandedValueForced = null;
    List<string> generalGridHideColumnList = new List<string>() { "Description", "TaskDuration", "RecurrenceType",
                                                                "ApprovalDuration", "CompletionDate", "CompletionComment", 
                                                                "Edit", "CreatedBy", "DateCreated", "RevisedBy", "DateRevised"};
    int? _taskCountCompleted = null;
    int? _taskCountPending = null;

    #endregion


    #region Properties

    public override bool IsExpanded
    {
        get
        {
            if (hdIsExpanded.Value == "1")
                return true;
            else
                return false;
        }
        set
        {
            if (value == true)
                hdIsExpanded.Value = "1";
            else
                hdIsExpanded.Value = "0";
        }
    }

    public override bool IsRefreshData
    {
        get
        {
            if (hdIsRefreshData.Value == "1")
                return true;
            else
                return false;
        }
        set
        {
            if (value == true)
                hdIsRefreshData.Value = "1";
            else
                hdIsRefreshData.Value = "0";
        }
    }

    public bool ContentVisibility
    {
        set
        {
            this.Visible = true;
            divMainContent.Visible = true;
        }
    }

    public bool IsDataLoadCondition
    {
        get
        {
            if (AccountID != null && NetAccountID != null && GLDataID != null && IsSRA != null)
                return true;
            return false;
        }
    }

    public string DivClientId
    {
        get { return divMainContent.ClientID; }
    }

    public bool? IsExpandedValueForced
    {
        get { return _IsExpandedValueForced; }
        set { _IsExpandedValueForced = value; }
    }


    public int? TaskCountPending
    {
        get { return _taskCountPending; }
        set { _taskCountPending = value; }
    }

    public int? TaskCountCompleted
    {
        get { return _taskCountCompleted; }
        set { _taskCountCompleted = value; }
    }

    #endregion

    #region Page Events

    protected void Page_Init(object sender, EventArgs e)
    {
        ucGeneralTaskGrid.HideGridColumns(generalGridHideColumnList);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        SetControlState();

    }

    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
    }

    #endregion

    #region Custom Functions

    /// <summary>
    /// Sets the state of the control.
    /// </summary>
    private void SetControlState()
    {
        try
        {
            ucGeneralTaskGrid.BasePageTitleLabelID = 2576;
            ExpandCollapse();
        }
        catch (Exception ex)
        {
            Helper.ShowErrorMessage((PageBase)this.Page, ex);
        }
    }


    /// <summary>
    /// Registers the toggle control.
    /// </summary>
    /// <param name="imgToggleControl">The img toggle control.</param>
    public void RegisterToggleControl(ExImageButton imgToggleControl)
    {
        imgToggleControl.OnClientClick += "return ToggleDiv('" + imgToggleControl.ClientID + "','" + this.DivClientId + "','" + hdIsExpanded.ClientID + "','" + hdIsRefreshData.ClientID + "');";
        ToggleControl = imgToggleControl;
    }

    public override void LoadData()
    {
        if (IsRefreshData && IsExpanded)
        {

            try
            {
                var taskGridData = TaskMasterHelper.GetAccessableTaskByAccountIDs(AccountID, NetAccountID);
                if (taskGridData != null && taskGridData.Count > 0)
                {
                    TaskCountCompleted = taskGridData.FindAll(t => t.TaskStatusID == (short)ARTEnums.TaskStatus.Completed).Count;
                    TaskCountPending = (taskGridData.Count - TaskCountCompleted);
                }
                else
                    taskGridData = new List<SkyStem.ART.Client.Model.TaskHdrInfo>();

                this.ucGeneralTaskGrid.SetGeneralTaskGridData(taskGridData);
                ucGeneralTaskGrid.LoadGridData();

            }
            catch (Exception ex)
            {
                Helper.ShowErrorMessage((PageBase)this.Page, ex);
            }
        }
    }

    public override void ExpandCollapse()
    {
        if (IsExpandedValueForced == null)
        {
            //get default state from hidden field
            this.divMainContent.Visible = IsExpanded;
        }
        else
        {
            //show state of the control based on property IsExpandedValueForced
            this.divMainContent.Visible = (bool)IsExpandedValueForced;
        }
        //during pustback - manage state of Image button expandable/colapsable
        if (IsPostBack)
        {
            if (IsExpanded)
                ToggleControl.ImageUrl = "~/App_Themes/SkyStemBlueBrown/Images/CollapseGlass.gif";
            else
                ToggleControl.ImageUrl = "~/App_Themes/SkyStemBlueBrown/Images/ExpandGlass.gif";
        }
    }

    public void RegisterClientScripts()
    {
        if (this.AccountID.GetValueOrDefault() > 0 || this.NetAccountID.GetValueOrDefault() > 0)
        {
            ApprovePageURLAccounttaskGridPending();
            RejectPageURLAccounttaskGridPending();
            DonePageURLAccounttaskGridPending();
        }
    }

    public void ApprovePageURLAccounttaskGridPending()
    {

        string url = this.Page.ResolveClientUrl(URLConstants.URL_BULK_COMPLETE_TASK);
        if (!Page.ClientScript.IsClientScriptBlockRegistered("ApprovePageURLAccounttaskGridPending"))
            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "ApprovePageURLAccounttaskGridPending", PopupHelper.GetJavascriptParameterListForBulkStatusUpdate("ShowApprovePageForATPendingGrid", "OpenRadWindowWithName", url, "Bulk Complete Tasks", ARTEnums.TaskActionType.Approve, QueryStringConstants.INSERT, ARTEnums.TaskType.AccountTask, ARTEnums.RecordType.TaskComplition, AccountID, NetAccountID), true);

    }
    public void RejectPageURLAccounttaskGridPending()
    {

        string url = Page.ResolveClientUrl(URLConstants.URL_BULK_COMPLETE_TASK);
        if (!Page.ClientScript.IsClientScriptBlockRegistered("RejectPageURLAccounttaskGridPending"))
            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "RejectPageURLAccounttaskGridPending", PopupHelper.GetJavascriptParameterListForBulkStatusUpdate("ShowRejectPageForATPendingGrid", "OpenRadWindowWithName", url, "Bulk Complete Tasks", ARTEnums.TaskActionType.Reject, QueryStringConstants.INSERT, ARTEnums.TaskType.AccountTask, ARTEnums.RecordType.TaskComplition, AccountID, NetAccountID), true);

    }
    public void DonePageURLAccounttaskGridPending()
    {

        string url = Page.ResolveClientUrl(URLConstants.URL_BULK_COMPLETE_TASK);
        if (!Page.ClientScript.IsClientScriptBlockRegistered("DonePageURLAccounttaskGridPending"))
            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "DonePageURLAccounttaskGridPending", PopupHelper.GetJavascriptParameterListForBulkStatusUpdate("ShowDonePageForATPendingGrid", "OpenRadWindowWithName", url, "Bulk Complete Tasks", ARTEnums.TaskActionType.Complete, QueryStringConstants.INSERT, ARTEnums.TaskType.AccountTask, ARTEnums.RecordType.TaskComplition, AccountID, NetAccountID), true);

    }


    #endregion
}
