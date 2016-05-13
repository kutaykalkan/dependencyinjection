using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SkyStem.ART.Web.Data;
using SkyStem.ART.Web.Classes;
using SkyStem.ART.Web.Utility;
using SkyStem.ART.Client.Model;
using SkyStem.ART.Client.Exception;
using SkyStem.ART.Client.IServices;
using SkyStem.ART.Client.Data;
using SkyStem.Language.LanguageUtility;

public partial class Pages_TaskMaster_BulkCompleteTasks : PopupPageBaseTaskMaster
{
    #region Page Events

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request.QueryString[QueryStringConstants.TASK_ID]))
        {
            ucBulkCompleteTasks.TaskID = Convert.ToInt64(Request.QueryString[QueryStringConstants.TASK_ID]);
        }
        string _title = "Bulk Assignment";
        short? _taskActionTypeID = null;
        if (!string.IsNullOrEmpty(Request.QueryString[QueryStringConstants.TASK_ACTION_TYPE_ID]))
        {
            _taskActionTypeID = Convert.ToInt16(Request.QueryString[QueryStringConstants.TASK_ACTION_TYPE_ID]);
        }

        if (_taskActionTypeID.HasValue)
        {

            var _taskActionType = (ARTEnums.TaskActionType)_taskActionTypeID;

            switch (_taskActionType)
            {
                case ARTEnums.TaskActionType.Approve:
                    _title = LanguageUtil.GetValue(2612); // "Bulk Approve Tasks";
                    break;
                case ARTEnums.TaskActionType.Reject:
                    _title = LanguageUtil.GetValue(2613); // "Bulk Reject Tasks";
                    break;
                case ARTEnums.TaskActionType.Complete:
                    _title = LanguageUtil.GetValue(2614); // "Bulk Complete Tasks";
                    break;
                case ARTEnums.TaskActionType.BulkEdit:
                    _title = LanguageUtil.GetValue(2615); // "Bulk Edit Tasks";
                    break;
                case ARTEnums.TaskActionType.Remove:
                    _title = LanguageUtil.GetValue(2616); // "Bulk Delete Tasks";
                    break;
                case ARTEnums.TaskActionType.Review:
                    _title = LanguageUtil.GetValue(2966); // "Bulk Delete Tasks";
                    break;
            }

            if (!IsPostBack)
            {
                if (_taskActionType == ARTEnums.TaskActionType.Complete || _taskActionType == ARTEnums.TaskActionType.Review || _taskActionType == ARTEnums.TaskActionType.Reject || _taskActionType == ARTEnums.TaskActionType.Approve)
                {
                    Helper.ShowInputRequirementSectionForPopup((PopupPageBase)this.Page, 2622);
                }
            }
        }
        this.Title = _title;

    }

    #endregion

    #region Web Method
    /// <summary>
    /// This method is used to auto populate User Name text box based on the basis of 
    /// the prefix text typed in the text box
    /// </summary>
    /// <param name="prefixText">The text which was typed in the text box</param>
    /// <param name="count">Number of results to be returned</param>
    /// <returns>List of User Names</returns>
    [System.Web.Services.WebMethod()]
    [System.Web.Script.Services.ScriptMethod()]
    public static List<string> AutoCompleteUserName(string prefixText, int count)
    {
        List<string> UserNames = new List<string>();

        try
        {
            if (SessionHelper.CurrentCompanyID.HasValue)
            {
                int companyId = SessionHelper.CurrentCompanyID.Value;
                IUser oUserClient = RemotingHelper.GetUserObject();
                List<UserHdrInfo> oUserHdrInfoList = oUserClient.SelectActiveUserHdrInfoByCompanyIdAndPrefixText(companyId, prefixText, count, Helper.GetAppUserInfo());
                for (int i = 0; i < oUserHdrInfoList.Count; i++)
                {
                    string item = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(oUserHdrInfoList[i].Name.ToString(), oUserHdrInfoList[i].UserID.ToString());
                    UserNames.Add(item);
                }
                if (oUserHdrInfoList == null || oUserHdrInfoList.Count == 0)
                {
                    string item = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem("No Records Found", "0");
                    UserNames.Add(item);
                }
            }
        }
        catch (ARTException ex)
        {
            PopupHelper.ShowErrorMessage(null, ex);
            throw ex;
        }
        catch (Exception ex)
        {
            PopupHelper.ShowErrorMessage(null, ex);
            throw ex;
        }

        return UserNames;
    }
    #endregion
}
