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
using System.Collections.Generic;
using SkyStem.ART.Client.Model;
using SkyStem.ART.Web.Data;
using SkyStem.Library.Controls.WebControls;
using SkyStem.ART.Client.IServices;
using SkyStem.ART.Web.Utility;
using System.Text;
using SkyStem.Language.LanguageUtility;
using SkyStem.ART.Client.Data;
using Telerik.Web.UI;
using SkyStem.ART.Client.Exception;

public partial class Pages_AddUserMailOnDataImport : PopupPageBase
{
    #region Variables & Constants
    string[] SelectedUserIDList = null;
    #endregion

    #region Properties
    #endregion

    #region Delegates & Events
    #endregion

    #region Page Events
    protected void Page_Load(object sender, EventArgs e)
    {
        PopupHelper.SetPageTitle(this, 1341);
        string checkSuceessOrFailure = string.Empty;
        if (Request.QueryString[QueryStringConstants.SUCESS] != null)
        {
            checkSuceessOrFailure = "Sucess";
        }

        else
        {
            checkSuceessOrFailure = "Failure";
        }
        if (Request.QueryString[QueryStringConstants.TASK_USER_ROLE_ID] != null)
            hdnTaskUserRole.Value = Request.QueryString[QueryStringConstants.TASK_USER_ROLE_ID];
        else
            hdnTaskUserRole.Value = "";
       

        AddUsers.Attributes.Add("onclick", "return AddUserList(" + "'" + rgUserList.ClientID + "'" + "," + "'" + AddUsersList.ClientID + "'" + "," + "'" + checkSuceessOrFailure + "'" + ")");

        if (!Page.IsPostBack)
        {
            PopupHelper.ShowInputRequirementSection(this, 1788);
            rgUserList.ClientSettings.Selecting.AllowRowSelect = true;
            ShowControls(false);

            IUser oUserClient = RemotingHelper.GetUserObject();
            IRole oRoleClient = RemotingHelper.GetRoleObject();
            IList<RoleMstInfo> ListRoles = CacheHelper.GetAllRoles();
            List<ListItem> lstListItem = new List<ListItem>();

            foreach (RoleMstInfo role in ListRoles)
            {

                if ((role.Role != "SkyStem Admin"))
                {
                    lstListItem.Add(new ListItem(LanguageUtil.GetValue(role.RoleLabelID.Value), role.RoleID.Value.ToString()));
                }
            }

            //set datasource for UserRoleSelection control
            ddlRole.DataSource = lstListItem;
            ddlRole.DataTextField = "text";
            ddlRole.DataValueField = "value";
            ddlRole.DataBind();
            ListControlHelper.AddListItemForSelectAll(ddlRole);
            ListControlHelper.ShowSelectAllAsSelected(ddlRole);

            // Check if Coming from some other page

        }
        this.SetFocus(this.txtFirstName);
    }
    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (Request.QueryString[QueryStringConstants.SUCESS] != null)
        {
            string scriptKey = "SetcountOfDocumentAttached";
            // Render JS to Open the grid Customization Window, 
            StringBuilder script = new StringBuilder();
            ScriptHelper.AddJSStartTag(script);

            script.Append("function SetMailIDList()");
            script.Append(System.Environment.NewLine);
            script.Append("{");
            script.Append(System.Environment.NewLine);

            script.Append("GetRadWindow().BrowserWindow.setMailList('" + AddUsersList.Value + "') ;");
            //script.Append(System.Environment.NewLine);
            //script.Append("alert(oLabel);");
            //script.Append(System.Environment.NewLine);
            //script.Append("oLabel.innerHTML='" + attachmentCount + "';");
            script.Append(System.Environment.NewLine);
            script.Append("}");
            script.Append(System.Environment.NewLine);
            script.Append("SetMailIDList();");
            ScriptHelper.AddJSEndTag(script);
            if (!this.Page.ClientScript.IsStartupScriptRegistered(this.GetType(), scriptKey))
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), scriptKey, script.ToString());
            }
        }

        if (Request.QueryString[QueryStringConstants.FAILURE] != null)
        {
            string scriptKey = "SetcountOfDocumentAttached";
            // Render JS to Open the grid Customization Window, 
            StringBuilder script = new StringBuilder();
            ScriptHelper.AddJSStartTag(script);

            script.Append("function SetMailIDListFailure()");
            script.Append(System.Environment.NewLine);
            script.Append("{");
            script.Append(System.Environment.NewLine);

            script.Append("GetRadWindow().BrowserWindow.setMailListFailure('" + AddUsersList.Value + "') ;");
            //script.Append(System.Environment.NewLine);
            //script.Append("alert(oLabel);");
            //script.Append(System.Environment.NewLine);
            //script.Append("oLabel.innerHTML='" + attachmentCount + "';");
            script.Append(System.Environment.NewLine);
            script.Append("}");
            script.Append(System.Environment.NewLine);
            script.Append("SetMailIDListFailure();");
            ScriptHelper.AddJSEndTag(script);
            if (!this.Page.ClientScript.IsStartupScriptRegistered(this.GetType(), scriptKey))
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), scriptKey, script.ToString());
            }
        }
    }
    #endregion

    #region Grid Events
    protected void rgUserList_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {
        if (e.Item.ItemType == Telerik.Web.UI.GridItemType.Item ||
            e.Item.ItemType == Telerik.Web.UI.GridItemType.AlternatingItem)
        {
            UserHdrInfo oUserHdrInfo = (UserHdrInfo)e.Item.DataItem;

            // Check for Active User
            if (!oUserHdrInfo.IsActive.Value)
            {
                e.Item.CssClass = "InactiveCompany";
            }

            ExHyperLink hlFirstName = (ExHyperLink)e.Item.FindControl("hlFirstName");
            ExHyperLink hlLastName = (ExHyperLink)e.Item.FindControl("hlLastName");
            ExHyperLink hlEmailID = (ExHyperLink)e.Item.FindControl("hlEmailID");
            ExHyperLink hlJobTitle = (ExHyperLink)e.Item.FindControl("hlJobTitle");
            ExHyperLink hlLoginID = (ExHyperLink)e.Item.FindControl("hlLoginID");
            ExHyperLink hlWorkPhone = (ExHyperLink)e.Item.FindControl("hlWorkPhone");
            ExHyperLink hlPhone = (ExHyperLink)e.Item.FindControl("hlPhone");
            ExImageButton imgBtnResetPassword = (ExImageButton)e.Item.FindControl("imgBtnResetPassword");

            hlFirstName.Text = oUserHdrInfo.FirstName;
            hlLastName.Text = oUserHdrInfo.LastName;
            hlEmailID.Text = oUserHdrInfo.EmailID;
            hlJobTitle.Text = oUserHdrInfo.JobTitle;
            hlLoginID.Text = oUserHdrInfo.LoginID;
            hlWorkPhone.Text = Helper.GetDisplayStringValue(oUserHdrInfo.WorkPhone);
            hlPhone.Text = Helper.GetDisplayStringValue(oUserHdrInfo.Phone);

        }
    }
    protected void rgUserList_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    {
        if (e.RebindReason != GridRebindReason.InitialLoad)
        {
            bool? isActive = null;
            int? roleID = null;
            if (ddlRole.SelectedValue != "0")
                roleID = Convert.ToInt32(ddlRole.SelectedValue);
            isActive = true;


            List<UserHdrInfo> oUserHdrInfoCollection = new List<UserHdrInfo>();
            string sortExpression = rgUserList.MasterTableView.SortExpressions[0].FieldName;
            string sortDirection = rgUserList.MasterTableView.SortExpressions[0].SortOrderAsString();

            try
            {
                IUser oUserClient = RemotingHelper.GetUserObject();
                // int defaultItemCount = Convert.ToInt32(AppSettingHelper.GetAppSettingValue(AppSettingConstants.DEFAULT_CHUNK_SIZE));
                int pageIndex = rgUserList.CurrentPageIndex;
                int pageSize = rgUserList.PageSize;
                int defaultItemCount = Helper.GetDefaultChunkSize(pageSize);
                int count = (pageIndex / pageSize + 1) * defaultItemCount;


                short CurrentRoleID = SessionHelper.CurrentRoleID.Value;
                if (Request.QueryString[QueryStringConstants.FROMPOPUP] != null)
                {
                    CurrentRoleID = 2;// fetch all record 
                }

                short ActivationStatusTypeID = (short)ARTEnums.ActivationStatusType.UserActivationStatus;
                oUserHdrInfoCollection = oUserClient.SearchUser(txtFirstName.Text.Trim(), txtEmail.Text.Trim(),
                    txtLastName.Text.Trim(), count, roleID, isActive, SessionHelper.CurrentCompanyID, 
                    SessionHelper.CurrentUserID.Value, CurrentRoleID, (int)SessionHelper.CurrentReconciliationPeriodID, 
                    (DateTime)SessionHelper.CurrentReconciliationPeriodEndDate, sortExpression, sortDirection, false, 2,
                    ActivationStatusTypeID, null,  Helper.GetAppUserInfo());
                if (oUserHdrInfoCollection.Count % defaultItemCount == 0)
                    rgUserList.VirtualItemCount = oUserHdrInfoCollection.Count + 1;
                else
                    rgUserList.VirtualItemCount = oUserHdrInfoCollection.Count;

            }
            catch (ARTException ex)
            {
                PopupHelper.ShowErrorMessage(this, ex);
            }
            catch (Exception ex)
            {
                PopupHelper.ShowErrorMessage(this, ex);
            }

            rgUserList.MasterTableView.DataSource = oUserHdrInfoCollection;
        }


    }

    protected void rgUserList_SortCommand(object source, GridSortCommandEventArgs e)
    {
        GridHelper.HandleSortCommand(e);
        rgUserList.Rebind();

    }
    #endregion

    #region Other Events
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (Request.QueryString[QueryStringConstants.FROMPOPUP] != null)
        {
            AddUsers.Visible = false;
            btnOK.Visible = true;
        }
        else
        {
            AddUsers.Visible = true;
            btnOK.Visible = false;
        }

        IUser oUserClient = RemotingHelper.GetUserObject();
        List<UserHdrInfo> oUserHdrInfoCollection = new List<UserHdrInfo>();
        UserHdrInfo oUserHdrInfo = new UserHdrInfo();
        UserRoleInfo oUserRoleInfo = new UserRoleInfo();
        bool? isActive = null;
        int? roleID = null;

        if (ddlRole.SelectedValue != "0")
            roleID = Convert.ToInt32(ddlRole.SelectedValue);

        isActive = true;

        oUserHdrInfo.FirstName = txtFirstName.Text;
        oUserHdrInfo.LastName = txtLastName.Text;
        oUserHdrInfo.EmailID = txtEmail.Text;
        oUserHdrInfo.IsActive = isActive;
        if (roleID != null)
            oUserRoleInfo.RoleID = (short)roleID;
        Session[SessionConstants.USER_COLLECTION] = oUserHdrInfoCollection;
        Session[SessionConstants.SEARCH_USER_PARAMATERES] = oUserHdrInfo;
        Session[SessionConstants.SEARCH_USER_PARAMATERES_ROLE] = oUserRoleInfo;

        // Add Default Sort as First Name, ASC
        GridSortExpression oGridSortExpression = new GridSortExpression();
        oGridSortExpression.FieldName = "FirstName";
        oGridSortExpression.SortOrder = GridSortOrder.Ascending;
        rgUserList.MasterTableView.SortExpressions.AddSortExpression(oGridSortExpression);
        rgUserList.AllowPaging = false;
        rgUserList.AllowExportToPDF = false;
        rgUserList.AllowExportToExcel = false;
        rgUserList.AllowPrint = false;
        rgUserList.AllowPrintAll = false;
        rgUserList.Columns[5].Visible = false;
        ShowControls(true);
        rgUserList.Rebind();
        getSelectedUser();
    }
    #endregion

    #region Validation Control Events
    #endregion

    #region Private Methods
    private void ShowControls(bool bShow)
    {
        tblLegend.Visible = bShow;
        rgUserList.Visible = bShow;
    }
    private void getSelectedUser()
    {
        if (Request.QueryString[QueryStringConstants.SELECTED_USER_ID] != null)
            SelectedUserIDList = Request.QueryString[QueryStringConstants.SELECTED_USER_ID].Split(',');
        if (SelectedUserIDList != null && SelectedUserIDList.Length > 0)
        {
            foreach (GridDataItem dataItem in rgUserList.Items)
            {
                string UserID = dataItem.GetDataKeyValue("UserID").ToString();
                CheckBox checkBox = (CheckBox)(dataItem)["CheckboxSelectColumn"].Controls[0];
                if (SelectedUserIDList.Contains(UserID))
                {
                    checkBox.Checked = true;
                    dataItem.Selected = true;
                }
            }
        }

    }

    #endregion

    #region Other Methods
    #endregion
}
