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
using SkyStem.ART.Client.IServices;
using SkyStem.ART.Client.Model;
using System.Collections.Generic;
using SkyStem.ART.Web.Classes;
using SkyStem.ART.Web.UserControls;
using SkyStem.ART.Web.Utility;
using SkyStem.Language.LanguageUtility;
using SkyStem.ART.Web.Data;
using System.Text;
using SkyStem.ART.Client.Exception;
using SkyStem.Language.LanguageUtility.Classes;
using SharedUtility = SkyStem.ART.Shared.Utility;
using SharedData = SkyStem.ART.Shared.Data;
using SkyStem.ART.Client.Data;
using SkyStem.ART.Shared.Data;
public partial class Pages_CreateUser : PageBaseCompany
{
    #region Variables & Constants
    enum Buttons
    {
        Save,
        AccountAssociation,
        Cancel,
        Home
    }
    private int? _UserID = null;
    #endregion

    #region Properties
    private CompanyHdrInfo oCompanyHdrFTPInfo
    {
        get
        {

            return SessionHelper.GetCurrentCompanyHdrInfo();
        }
    }
    #endregion

    #region Page Events
    protected void Page_Init(object sender, EventArgs e)
    {
        //set error messages
        this.SetErrorMessages();
        MasterPageBase ompage = (MasterPageBase)this.Master;
        ompage.ReconciliationPeriodChangedEventHandler += ompage_ReconciliationPeriodChangedEventHandler;
    }
    protected void Page_Load(object sender, EventArgs e)
    {

        ListBox lblstSelectedUserRoles = (ListBox)this.UserRoleSelection.FindControl("lstSelectedUserRoles");
        UserRoleSelection.setDropDownFromPage = @"Setdropdownlist('" + ddlDefaultRole.ClientID + "', '" + lblstSelectedUserRoles.ClientID + "', '" + hdnSelectItem.ClientID + "', '" + hdnSelectValue.ClientID + "', '" + btnAccountAssociation.ClientID + "', '" + hdnDefaultRole.ClientID + "');";

        ddlDefaultRole.Attributes.Add("onChange", "SetHiddenValue('" + ddlDefaultRole.ClientID + "', '" + hdnDefaultRole.ClientID + "')");

        if (Request.QueryString[QueryStringConstants.User_ID] != null)
        {
            _UserID = Convert.ToInt32(Request.QueryString[QueryStringConstants.User_ID]);
        }

        if (_UserID != null)
        {
            Helper.SetPageTitle(this, 1535);
        }
        else
        {
            Helper.SetPageTitle(this, 1283);
        }

        // Check whether the Company is active,if First time page access and Create User
        // Check for User Limit, if First time page access and Create User
        if (!this.IsPostBack
            && _UserID == null)
        {
            CheckIsCompanyActivated();
            CheckUserLimitReached();
        }

        if (!this.IsPostBack)
        {
            OnPageLoad();
        }

        if (rbYes.Checked)
            rfvServerFTP.Attributes.Add("Enabled", "true");
        else
            rfvServerFTP.Attributes.Add("Enabled", "false");
    }
    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (Request.QueryString[QueryStringConstants.User_ID] != null)
        {
            Helper.SetBreadcrumbs(this, 1074, 1535);
        }
        revLoginID.Visible = txtLoginId.Visible;
        rfvLoginID.Visible = txtLoginId.Visible;
        rbYes.InputAttributes.Add("onchange", "FTPEnableChanged();");
        rbNo.InputAttributes.Add("onchange", "FTPEnableChanged();");
    }
    #endregion

    #region Other Events
    protected void btnHome_Click(object sender, EventArgs e)
    {
        Helper.RedirectToHomePage();
    }
    protected void btnAddMore_Click(object sender, EventArgs e)
    {
        //Since you are looking to create more users, so check for limit
        CheckUserLimitReached();

        trButtons.Visible = false;
        trUser.Visible = true;
        // Form Cleanup
        txtFirstName.Text = "";
        txtLastName.Text = "";
        txtLoginId.Text = "";
        txtEmailId.Text = "";
        txtJobTitle.Text = "";
        txtWorkPhone.Text = "";
        txtPhone.Text = "";
        hdnSelectValue.Value = "";
        optActiveNo.Checked = false;
        optActiveYes.Checked = false;
        rbYes.Checked = false;
        rbNo.Checked = false;
        txtFTPLoginID.Text = "";
        lblFTPLoginIDValue.Text = "";
        hdnLoginID.Value = "";
        hdnFTPLoginID.Value = "";

        UserRoleSelection.ClearSelectedListBox();
        SessionHelper.ClearAllRolesFromSession();
        LoadRoles();
        ShowHideFTPSection(null);
        Helper.HideMessage(this);
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        string url = Helper.GetHomePageUrl(); ;

        if (Request.QueryString[QueryStringConstants.FROM_PAGE] != null)
        {
            WebEnums.ARTPages ePages = (WebEnums.ARTPages)System.Enum.Parse(typeof(WebEnums.ARTPages), Request.QueryString[QueryStringConstants.FROM_PAGE].ToString());

            switch (ePages)
            {
                case WebEnums.ARTPages.UserSearch:
                    url = "~/Pages/UserSearch.aspx?" + QueryStringConstants.SHOW_SEARCH_RESULTS + "=1";
                    break;
            }
        }
        //Response.Redirect(url);
        SessionHelper.RedirectToUrl(url);
        return;
    }
    protected void btnAccountAssociation_Click(object sender, EventArgs e)
    {
        SaveUserDataAndRedirect(Buttons.AccountAssociation);
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        SaveUserDataAndRedirect(Buttons.Save);
    }
    protected void UserRoleSelection_RoleSelectionChanged(object sender, ListItemEventArgs e)
    {
        this.ddlDefaultRole.DataSource = e.ListOfListItems;
        this.ddlDefaultRole.DataTextField = "text";
        this.ddlDefaultRole.DataValueField = "value";
        this.ddlDefaultRole.DataBind();
    }
    void ompage_ReconciliationPeriodChangedEventHandler(object sender, EventArgs e)
    {
        OnPageLoad();
    }

    #endregion

    #region Validation Control Events
    protected void cvUserRoleCheckForInActive_OnServerValidate(object sender, ServerValidateEventArgs args)
    {
        if (optActiveNo.Checked && _UserID.HasValue && SessionHelper.CurrentReconciliationPeriodID.HasValue)
        {
            IUser oUser = RemotingHelper.GetUserObject();
            List<UserRoleInfo> oUserRoleInfoList = oUser.SelectUserActiveRolesPRA(_UserID, SessionHelper.CurrentReconciliationPeriodID, Helper.GetAppUserInfo());
            if (oUserRoleInfoList != null && oUserRoleInfoList.Count > 0)
            {
                args.IsValid = false;
            }
        }
    }
    protected void cvUserRoleCheckForRoleRemoval_OnServerValidate(object sender, ServerValidateEventArgs args)
    {
        if (_UserID.HasValue && SessionHelper.CurrentReconciliationPeriodID.HasValue)
        {
            IUser oUser = RemotingHelper.GetUserObject();
            List<int> oRoleList = new List<int>();
            string[] listUserRolesvalues = hdnSelectValue.Value.Split(',');
            List<UserRoleInfo> oUserRoleInfoList = oUser.SelectUserActiveRolesPRA(_UserID, SessionHelper.CurrentReconciliationPeriodID, Helper.GetAppUserInfo());
            foreach (string item in listUserRolesvalues)
            {
                if (item != "")
                    oRoleList.Add(int.Parse(item));
            }
            if (oRoleList.Count == 0 && oUserRoleInfoList != null && oUserRoleInfoList.Count > 0)
            {
                List<ListItem> oSelectedRoles = UserRoleSelection.GetSelectedItems();
                foreach (ListItem item in oSelectedRoles)
                {
                    if (item.Value != "")
                        oRoleList.Add(int.Parse(item.Value));
                }
            }
            if (oRoleList != null && oRoleList.Count > 0)
            {
                foreach (UserRoleInfo oUserRoleInfo in oUserRoleInfoList)
                {
                    if (!oRoleList.Exists(x => x == oUserRoleInfo.RoleID.Value))
                    {
                        args.IsValid = false;
                        break;
                    }
                }
            }
        }
    }
    protected void cvEnableFTP_ServerValidate(object source, ServerValidateEventArgs args)
    {
        List<int> SelectedRoles = GetSelectedUserRoles();
        List<int> FTPRoles = SelectedRoles.FindAll(T => T == (int)ARTEnums.UserRole.SYSTEM_ADMIN || T == (int)ARTEnums.UserRole.BUSINESS_ADMIN);
        if ((FTPRoles == null || FTPRoles.Count == 0) && (rbYes.Checked && ddlServerFTP.SelectedValue != WebConstants.SELECT_ONE))
        {
            args.IsValid = false;
            cvEnableFTP.ErrorMessage = LanguageUtil.GetValue(5000409);
        }
        else if (rbYes.Checked && ddlServerFTP.SelectedValue == WebConstants.SELECT_ONE)
        {
            args.IsValid = false;
            cvEnableFTP.ErrorMessage = Helper.GetErrorMessage(WebEnums.FieldType.MandatoryField, lblFTPServer.LabelID);
        }
        else if (rbYes.Checked && string.IsNullOrWhiteSpace(txtFTPLoginID.Text))
        {
            args.IsValid = false;
            cvEnableFTP.ErrorMessage = Helper.GetErrorMessage(WebEnums.FieldType.MandatoryField, lblFTPLoginID.LabelID);
        }
    }
    #endregion

    #region Private Methods
    /// <summary>
    /// Page Load or Rec Period is changed
    /// </summary>
    private void OnPageLoad()
    {
        Helper.HideMessage(this);
        ListBox lblstSelectedUserRoles = (ListBox)this.UserRoleSelection.FindControl("lstSelectedUserRoles");
        ListControlHelper.BindLanguageDropdown(ddlLanguage, true, true, false);

        int? CompanyId = null;
        if (Request.QueryString[QueryStringConstants.COMPANY_ID] != null)
        {
            CompanyId = Convert.ToInt32(Request.QueryString[QueryStringConstants.COMPANY_ID]);
        }
        else
        {
            CompanyId = SessionHelper.CurrentCompanyID.Value;
        }
        ListControlHelper.BindFTPServerDropdown(ddlServerFTP, CompanyId);

        LoadRoles();
        Helper.ShowInputRequirementSection(this, 1202);
        this.SetFocus(this.txtFirstName.ClientID);

        string FrompageID = Convert.ToString((int)WebEnums.ARTPages.CreateCompany);
        List<RoleMstInfo> oUserRoleInfoCollection = null;

        if (_UserID != null)
        {
            oUserRoleInfoCollection = BindDefaultRoleDropdown();

            txtLoginId.Visible = false;
            lblLoginText.Visible = true;
            lblFTPLoginIDValue.Visible = false;

            IUser oUserClient = RemotingHelper.GetUserObject();
            UserHdrInfo oUserHdrInfo = new UserHdrInfo();
            oUserHdrInfo = oUserClient.GetUserDetail(_UserID.Value, Helper.GetAppUserInfo());
            SetPage(oUserHdrInfo);

            ShowHideFTPSection(oUserRoleInfoCollection);

            if (SessionHelper.CurrentRoleEnum != ARTEnums.UserRole.SKYSTEM_ADMIN
                && SessionHelper.CurrentRoleEnum != ARTEnums.UserRole.SYSTEM_ADMIN
                && SessionHelper.CurrentRoleEnum != ARTEnums.UserRole.USER_ADMIN)
            {
                // For all users other than SkyStem Admin and System Admin
                // Hide Potential Role and Active / InActive
                rowPotentialRole.Visible = false;
                optActiveNo.Enabled = false;
                optActiveYes.Enabled = false;
                rbYes.Enabled = false;
                rbNo.Enabled = false;
                ddlServerFTP.Enabled = false;
                txtFTPLoginID.Enabled = false;
            }
            else
            {
                rowPotentialRole.Visible = true;
                BindRoleListControl(lblstSelectedUserRoles, oUserRoleInfoCollection);
            }

            if (SessionHelper.CurrentUserID.Value == _UserID.Value)
            {
                optActiveNo.Enabled = false;
                optActiveYes.Enabled = false;
            }

            if (CheckRolesForUserAccountAssociation(oUserRoleInfoCollection))
            {
                btnAccountAssociation.Visible = true;
            }
            else
            {
                btnAccountAssociation.Visible = false;
            }
        }
        else
        {
            btnAccountAssociation.Visible = false;
            txtLoginId.Visible = true;
            lblLoginText.Visible = false;
            lblFTPLoginIDValue.Visible = false;
        }

        if (Request.QueryString[QueryStringConstants.User_ID] == null && Request.QueryString[QueryStringConstants.FROMPAGE] == FrompageID)
        {
            hdnSelectValue.Value = Convert.ToString((int)ARTEnums.UserRole.SYSTEM_ADMIN);

            List<int> listUserRoles = new List<int>();
            listUserRoles.Add((int)ARTEnums.UserRole.SYSTEM_ADMIN);
            this.UserRoleSelection.RemoveaddItemsFromListBoxonvalue(listUserRoles, true);

            List<ListItem> lstSelectedListItems = this.UserRoleSelection.GetSelectedItems();
            foreach (ListItem li in lstSelectedListItems)
            {
                if (!ddlDefaultRole.Items.Contains(li))
                {
                    ddlDefaultRole.Items.Add(li);
                }
            }
        }
    }

    private void ShowHideFTPSection(List<RoleMstInfo> oUserRoleInfoList)
    {
        trEnableFTP.Visible = false;
        trEnableFTPLabel.Visible = false;
        if (oCompanyHdrFTPInfo.IsFTPEnabled == true
            && (SessionHelper.CurrentRoleEnum == ARTEnums.UserRole.SKYSTEM_ADMIN
                || SessionHelper.CurrentRoleEnum == ARTEnums.UserRole.SYSTEM_ADMIN
                || SessionHelper.CurrentRoleEnum == ARTEnums.UserRole.USER_ADMIN
                || SessionHelper.CurrentRoleEnum == ARTEnums.UserRole.BUSINESS_ADMIN))
        {
            if (!CheckRolesForEnableFTP(oUserRoleInfoList))
            {
                rbYes.InputAttributes.Add("disabled", "disabled");
                rbNo.InputAttributes.Add("disabled", "disabled");
                ddlServerFTP.Attributes.Add("disabled", "disabled");
                txtFTPLoginID.Attributes.Add("disabled", "disabled");
            }
            trEnableFTP.Visible = true;
            trEnableFTPLabel.Visible = true;
        }
    }
    /// <summary>
    /// Set Error Messages.
    /// </summary>    
    /// <returns></returns>
    private void SetErrorMessages()
    {
        this.txtFirstName.ErrorMessage = Helper.GetErrorMessage(WebEnums.FieldType.MandatoryField, this.lblFirstName.LabelID);
        this.txtLastName.ErrorMessage = Helper.GetErrorMessage(WebEnums.FieldType.MandatoryField, this.lblLastName.LabelID);
        this.UserRoleSelection.ErrorMessage = Helper.GetErrorMessage(SkyStem.ART.Web.Data.WebEnums.FieldType.MandatoryField, this.lblPotentialRole.LabelID);
        //this.revEmailId.LabelID = 1317;
        this.rfvLoginID.ErrorMessage = Helper.GetErrorMessage(WebEnums.FieldType.MandatoryField, this.lblLoginId.LabelID);

        this.rfvEmailId.LabelID = 1334;
        cvUserRoleCheckForInActive.ErrorMessage = LanguageUtil.GetValue(5000360);
        cvUserRoleCheckForRoleRemoval.ErrorMessage = LanguageUtil.GetValue(5000361);

        rfvServerFTP.ErrorMessage = Helper.GetErrorMessage(WebEnums.FieldType.MandatoryField, lblFTPServer.LabelID);
        rfvServerFTP.InitialValue = WebConstants.SELECT_ONE;
        //this.cvIsActive.ErrorMessage = Helper.GetErrorMessage(WebEnums.FieldType.MandatoryField, this.lblActive.LabelID);
        cvEnableFTP.ErrorMessage = LanguageUtil.GetValue(5000409);
        this.rfvFTPLoginID.ErrorMessage = Helper.GetErrorMessage(WebEnums.FieldType.MandatoryField, this.lblFTPLoginID.LabelID);
    }
    /// <summary>
    /// Send Mail To User.
    /// </summary> 
    /// <param name="loginID">loginID.</param>
    /// <param name="password">password.</param>
    /// <param name="emailID">emailID.</param>
    /// <param name="userLanguageID">userLanguageID.</param>
    /// <returns></returns>
    private void SendMailToUser(UserHdrInfo oUserHdrInfo, bool IsFTPEnabled)
    {
        try
        {
            StringBuilder oMailBody = new StringBuilder();
            MultilingualAttributeInfo oMultilingualAttributeInfo = LanguageHelper.GetMultilingualAttributeInfo(SessionHelper.CurrentCompanyID, oUserHdrInfo.DefaultLanguageID);
            FTPServerInfo oFTPServerInfo = FTPHelper.GetFTPServerInfo(oUserHdrInfo.FTPServerID, oUserHdrInfo.CompanyID);
            string url = string.Empty;
            if (oFTPServerInfo != null)
                url = oFTPServerInfo.FTPUrl;

            oMailBody.Append(string.Format("{0} ", LanguageUtil.GetValue(1845, oMultilingualAttributeInfo)));
            oMailBody.Append(oUserHdrInfo.Name);
            oMailBody.Append(",");
            oMailBody.Append("<br>");
            oMailBody.Append(string.Format("{0}: ", LanguageUtil.GetValue(1325, oMultilingualAttributeInfo)));
            oMailBody.Append("<br>");
            oMailBody.Append(string.Format("{0}: ", LanguageUtil.GetValue(1269, oMultilingualAttributeInfo)));
            oMailBody.Append(oUserHdrInfo.LoginID);
            oMailBody.Append("<br>");
            if (!string.IsNullOrEmpty(oUserHdrInfo.GeneratedPassword))
            {
                oMailBody.Append(string.Format("{0}: ", LanguageUtil.GetValue(1004, oMultilingualAttributeInfo)));
                oMailBody.Append(oUserHdrInfo.GeneratedPassword);
                oMailBody.Append("<br>");
            }
            if (IsFTPEnabled)
            {
                oMailBody.Append("<br>");
                oMailBody.Append(string.Format("{0}: ", LanguageUtil.GetValue(2903, oMultilingualAttributeInfo)));
                oMailBody.Append(url);
                oMailBody.Append("<br>");

                oMailBody.Append(string.Format("{0}: ", LanguageUtil.GetValue(2925, oMultilingualAttributeInfo)));
                oMailBody.Append(SharedDataImportHelper.GetFTPLoginID(oUserHdrInfo.FTPLoginID));
                oMailBody.Append("<br>");
                if (!string.IsNullOrEmpty(oUserHdrInfo.GeneratedFTPPassword))
                {
                    oMailBody.Append(string.Format("{0}: ", LanguageUtil.GetValue(2916, oMultilingualAttributeInfo)));
                    oMailBody.Append(oUserHdrInfo.GeneratedFTPPassword);
                    oMailBody.Append("<br>");
                }
            }
            oMailBody.Append("<br>");
            oMailBody.Append("<br>");
            String msg;
            msg = LanguageUtil.GetValue(2384, oMultilingualAttributeInfo);
            String Url = Request.Url.AbsoluteUri;
            string NewUrl = Url.Replace("Pages/CreateUser.aspx", AppSettingHelper.GetAppSettingValue(AppSettingConstants.EMAIL_SYSTEM_URL));
            //oMailBody.Append(string.Format(msg, NewUrl));
            oMailBody.Append(string.Format(msg, AppSettingHelper.GetAppSettingValue(AppSettingConstants.EMAIL_SYSTEM_URL)));

            //oMailBody.Append(string.Format("{0}: ", LanguageUtil.GetValue(1844)));
            //oMailBody.Append("<br>");
            //oMailBody.Append(string.Format("{0}: ", LanguageUtil.GetValue(1324)));
            //oMailBody.Append(AppSettingHelper.GetAppSettingValue(AppSettingConstants.EMAIL_SYSTEM_URL));
            string fromAddress = AppSettingHelper.GetAppSettingValue(AppSettingConstants.EMAIL_FROM_DEFAULT);
            if (SessionHelper.CurrentRoleEnum == ARTEnums.UserRole.SKYSTEM_ADMIN)
            {
                oMailBody.Append("<br/>" + MailHelper.GetEmailSignature(WebEnums.SignatureEnum.SendBySkyStemSystem, fromAddress, oMultilingualAttributeInfo));
            }
            else
            {
                oMailBody.Append("<br/>" + MailHelper.GetEmailSignature(WebEnums.SignatureEnum.SendBySystemAdmin, fromAddress, oMultilingualAttributeInfo));
            }

            string mailSubject = string.Format("{0}: {1}", LanguageUtil.GetValue(1327, oMultilingualAttributeInfo), LanguageUtil.GetValue(1326, oMultilingualAttributeInfo));

            string toAddress = oUserHdrInfo.EmailID;
            MailHelper.SendEmail(fromAddress, toAddress, mailSubject, oMailBody.ToString());
        }
        catch (Exception ex)
        {
            Helper.FormatAndShowErrorMessage(null, ex);
        }
    }
    /// <summary>
    /// Check Is Company Activated
    /// </summary>
    private void CheckIsCompanyActivated()
    {
        //Checking for the Inactive Company, in case of creating new users
        ICompany oCompanyClient = RemotingHelper.GetCompanyObject();
        CompanyHdrInfo oCompanyHdrInfo = new CompanyHdrInfo();
        oCompanyHdrInfo = oCompanyClient.GetCompanyDetail(SessionHelper.CurrentCompanyID, SessionHelper.CurrentReconciliationPeriodEndDate, Helper.GetAppUserInfo());
        if (oCompanyHdrInfo.IsActive == false)
        {
            Helper.RedirectToErrorPage(5000173);
        }
        if (oCompanyHdrFTPInfo.IsFTPEnabled == true)
        {

            trEnableFTP.Visible = true;
            trEnableFTPLabel.Visible = true;
        }
        else
        {
            trEnableFTP.Visible = false;
            trEnableFTPLabel.Visible = false;
        }

    }
    /// <summary>
    /// Check Is User Limit Reached
    /// </summary>
    private static void CheckUserLimitReached()
    {

        // Check for User Creation Limit, in case of creating new users
        ICompany oCompanuClient = RemotingHelper.GetCompanyObject();
        bool bLimitReached = oCompanuClient.HasUserLimitReached(SessionHelper.CurrentCompanyID, Helper.GetAppUserInfo());
        if (bLimitReached)
            Helper.RedirectToErrorPage(5000015);
    }
    /// <summary>
    /// Bind Role List Control
    /// </summary>
    /// <param name="oListControl"></param>
    /// <param name="oUserRoleInfoCollection"></param>
    private static void BindRoleListControl(ListControl oListControl, List<RoleMstInfo> oUserRoleInfoCollection)
    {
        oListControl.DataSource = oUserRoleInfoCollection;
        oListControl.DataTextField = "Role";
        oListControl.DataValueField = "RoleID";
        oListControl.DataBind();
    }
    /// <summary>
    /// Bind Default Role Dropdown
    /// </summary>
    /// <returns></returns>
    private List<RoleMstInfo> BindDefaultRoleDropdown()
    {
        List<RoleMstInfo> oUserRoleInfoCollection = null;
        IUser oUserClient;
        oUserClient = RemotingHelper.GetUserObject();
        oUserRoleInfoCollection = oUserClient.GetUserRole(_UserID, Helper.GetAppUserInfo());
        oUserRoleInfoCollection = SessionHelper.RemoveRolesNotInPackage(oUserRoleInfoCollection);
        oUserRoleInfoCollection = LanguageHelper.TranslateRoleCollection(oUserRoleInfoCollection);
        BindRoleListControl(ddlDefaultRole, oUserRoleInfoCollection);
        return oUserRoleInfoCollection;
    }
    /// <summary>
    /// Set values in controls 
    /// </summary>
    /// <param name="oUserHdrInfo"></param>
    private void SetPage(UserHdrInfo oUserHdrInfo)
    {
        if (!Page.IsPostBack && oUserHdrInfo != null)
        {
            if (string.IsNullOrEmpty(oUserHdrInfo.FTPLoginID) && oUserHdrInfo.FTPActivationStatusID == (short)ARTEnums.ActivationStatus.Activated)
            {
                oUserHdrInfo.FTPLoginID = SharedDataImportHelper.GetFTPLoginID(oUserHdrInfo.LoginID);
            }
            this.txtFirstName.Text = oUserHdrInfo.FirstName;
            this.txtLastName.Text = oUserHdrInfo.LastName;
            this.txtEmailId.Text = oUserHdrInfo.EmailID;
            this.txtJobTitle.Text = oUserHdrInfo.JobTitle;
            this.lblLoginText.Text = oUserHdrInfo.LoginID;
            this.txtPhone.Text = oUserHdrInfo.Phone;
            this.txtWorkPhone.Text = oUserHdrInfo.WorkPhone;
            this.hdnLoginID.Value = oUserHdrInfo.LoginID;
            this.lblIsUserLockedValue.Text = ReportHelper.SetYesNoCodeBasedOnBool(oUserHdrInfo.IsUserLocked);
            if (oUserHdrInfo.IsActive.Equals(true))
            {
                optActiveYes.Checked = true;
            }
            else
            {
                optActiveNo.Checked = true;
                hdnCurrentStatus.Value = "0";
            }
            hdnCurrentStatus.Value = oUserHdrInfo.IsActive.Value.ToString();

            if (oUserHdrInfo.DefaultRoleID != null)
            {
                ListItem oListItem = ddlDefaultRole.Items.FindByValue(oUserHdrInfo.DefaultRoleID.ToString());
                if (oListItem != null)
                    oListItem.Selected = true;
            }

            if (oUserHdrInfo.DefaultLanguageID.HasValue)
                ddlLanguage.SelectedValue = oUserHdrInfo.DefaultLanguageID.ToString();

            if (oCompanyHdrFTPInfo.IsFTPEnabled == true)
            {

                if (oUserHdrInfo.FTPActivationStatusID == (short)ARTEnums.ActivationStatus.Activated)
                {
                    rbYes.Checked = true;
                }
                else if (oUserHdrInfo.FTPActivationStatusID == (short)ARTEnums.ActivationStatus.Deactivated)
                {
                    rbNo.Checked = true;
                }

                if (oUserHdrInfo.FTPServerID.HasValue)
                {
                    ListItem oListItem = ddlServerFTP.Items.FindByValue(oUserHdrInfo.FTPServerID.Value.ToString());
                    if (oListItem != null)
                        oListItem.Selected = true;
                }
                txtFTPLoginID.Text = oUserHdrInfo.FTPLoginID;
                hdnFTPLoginID.Value = oUserHdrInfo.FTPLoginID;
                lblFTPLoginIDValue.Text = oUserHdrInfo.FTPLoginID;
                ViewState["IsFTPPasswordResetRequired"] = oUserHdrInfo.IsFTPPasswordResetRequired;
            }
        }
    }
    /// <summary>
    /// Load Roles
    /// </summary>
    private void LoadRoles()
    {
        IUser oUserClient = RemotingHelper.GetUserObject();
        IRole oRoleClient = RemotingHelper.GetRoleObject();

        IList<RoleMstInfo> ListRoles = null;
        List<ListItem> lstListItem = new List<ListItem>();
        List<RoleMstInfo> objUserRoleInfoCollection = new List<RoleMstInfo>();
        int userID = Convert.ToInt32(Request.QueryString[QueryStringConstants.User_ID]);
        bool isDatabaseExists = SessionHelper.CurrentCompanyDatabaseExists.GetValueOrDefault();
        if (isDatabaseExists)
            objUserRoleInfoCollection = oUserClient.GetUserRole(userID, Helper.GetAppUserInfo());
        bool roleexists = true;

        if (isDatabaseExists)
            ListRoles = SessionHelper.GetAllRoles();
        else
            ListRoles = oRoleClient.GetAllRolesFromCore(Helper.GetAppUserInfo());
        string selectRoles = string.Empty;
        foreach (RoleMstInfo role in ListRoles)
        {
            foreach (RoleMstInfo selectedRoles in objUserRoleInfoCollection)
            {
                if (selectedRoles.RoleID == role.RoleID)
                {
                    roleexists = false;
                    selectRoles += role.RoleID.ToString() + ",";
                    break;
                }
                else
                {
                    roleexists = true;
                }
            }
            if (roleexists)
                if ((role.RoleID != (short)ARTEnums.UserRole.SKYSTEM_ADMIN))
                {
                    if (isDatabaseExists)
                        lstListItem.Add(new ListItem(LanguageUtil.GetValue(role.RoleLabelID.Value), role.RoleID.Value.ToString()));
                    else
                        lstListItem.Add(new ListItem(role.Role, role.RoleID.Value.ToString()));
                }
        }
        hdnSelectValue.Value = selectRoles;
        //set datasource for UserRoleSelection control
        this.UserRoleSelection.ListItems = lstListItem;
        this.UserRoleSelection.BindAgain();
    }
    /// <summary>
    /// Save User Data And Redirect
    /// </summary>
    /// <param name="eButtons"></param>
    private void SaveUserDataAndRedirect(Buttons eButtons)
    {
        if (this.IsValid)
        {
            try
            {
                UserHdrInfo oUserHdrInfo;
                List<int> listUserRoles;

                int count = ddlDefaultRole.Items.Count;
                bool isFTPEnabled = false;
                bool IsSendMailRequired = false;

                IUser oUserClient = RemotingHelper.GetUserObject();
                oUserHdrInfo = new UserHdrInfo();

                oUserHdrInfo.FirstName = this.txtFirstName.Text;
                oUserHdrInfo.LastName = this.txtLastName.Text;
                oUserHdrInfo.JobTitle = this.txtJobTitle.Text;
                oUserHdrInfo.WorkPhone = this.txtWorkPhone.Text;
                oUserHdrInfo.Phone = this.txtPhone.Text;
                oUserHdrInfo.AddedByRoleID = SessionHelper.CurrentRoleID;
                string loginID = string.Empty;
                string oldFtpLoginID = hdnFTPLoginID.Value;
                string newFtpLoginID = txtFTPLoginID.Visible ? txtFTPLoginID.Text : lblFTPLoginIDValue.Text;

                bool isActive = false;
                if (this.optActiveNo.Checked)
                    isActive = false;
                if (this.optActiveYes.Checked)
                    isActive = true;

                oUserHdrInfo.IsActive = isActive;
                oUserHdrInfo.EmailID = this.txtEmailId.Text;
                oUserHdrInfo.FTPLoginID = newFtpLoginID;

                if (Request.QueryString[QueryStringConstants.COMPANY_ID] != null)
                {
                    oUserHdrInfo.CompanyID = Convert.ToInt32(Request.QueryString[QueryStringConstants.COMPANY_ID]);
                }
                else
                {
                    oUserHdrInfo.CompanyID = SessionHelper.CurrentCompanyID;
                }

                // For SkyStem Admin when updating the self profile Company ID should be null
                if (_UserID.HasValue && _UserID == SessionHelper.CurrentUserID && SessionHelper.CurrentRoleID == (short)ARTEnums.UserRole.SKYSTEM_ADMIN)
                    oUserHdrInfo.CompanyID = null;

                listUserRoles = GetSelectedUserRoles();

                if (string.IsNullOrEmpty(hdnDefaultRole.Value))
                {
                    oUserHdrInfo.DefaultRoleID = short.Parse(ddlDefaultRole.Items[0].Value);
                }
                else
                {
                    oUserHdrInfo.DefaultRoleID = short.Parse(hdnDefaultRole.Value);
                }
                int defaultLanguageID = Convert.ToInt32(ddlLanguage.SelectedValue);

                if (defaultLanguageID > 0)
                    oUserHdrInfo.DefaultLanguageID = defaultLanguageID;

                bool IsEmailIDUniqueCheckRequired = false;
                bool.TryParse(SharedUtility.SharedAppSettingHelper.GetAppSettingValue(SharedData.SharedAppSettingConstants.IS_EMAIL_ID_UNIQUE_CHECK_REQUIRED), out IsEmailIDUniqueCheckRequired);

                if (this.rbYes.Checked)
                {
                    oUserHdrInfo.FTPActivationStatusID = (short)ARTEnums.ActivationStatus.Activated;
                    isFTPEnabled = true;
                }
                if (this.rbNo.Checked)
                    oUserHdrInfo.FTPActivationStatusID = (short)ARTEnums.ActivationStatus.Deactivated;

                Int16 ServerFTP;
                Int16.TryParse(ddlServerFTP.SelectedValue, out ServerFTP);
                if (ServerFTP > 0)
                    oUserHdrInfo.FTPServerID = ServerFTP;
                else
                    oUserHdrInfo.FTPServerID = null;

                // Check for Add versus Edit Mode
                if (_UserID != null)
                {
                    loginID = lblLoginText.Text;
                    // EDIT Mode
                    oUserHdrInfo.IsNew = false;
                    oUserHdrInfo.UserID = _UserID;
                    oUserHdrInfo.DateRevised = DateTime.Now;
                    oUserHdrInfo.RevisedBy = SessionHelper.CurrentUserLoginID;
                    oUserHdrInfo.LoginID = loginID;

                    bool currentStatus = Convert.ToBoolean(hdnCurrentStatus.Value);
                    bool hasStatusChanged = false;

                    if (isActive
                        && isActive != currentStatus)
                    {
                        // if User is Active, and also the status has changed from InActive to Active
                        hasStatusChanged = true;
                    }

                    #region FTP Details Update Save

                    if (ViewState["IsFTPPasswordResetRequired"] != null)
                    {
                        int IsFTPPasswordResetRequired;
                        int.TryParse(ViewState["IsFTPPasswordResetRequired"].ToString(), out IsFTPPasswordResetRequired);
                        if (this.rbYes.Checked && (IsFTPPasswordResetRequired == 0 || !FTPHelper.IsFTPUserExists(newFtpLoginID) || newFtpLoginID != oldFtpLoginID))
                        {
                            oUserHdrInfo.GeneratedFTPPassword = Helper.CreateRandomPassword(SharedConstants.LENGTH_GENERATED_PASSWORD, newFtpLoginID);
                            oUserHdrInfo.FTPPassword = Helper.GetHashedPassword(oUserHdrInfo.GeneratedFTPPassword);
                            IsSendMailRequired = true;
                        }
                    }
                    #endregion
                    oUserClient.UpdateUser(oUserHdrInfo, listUserRoles.Count, listUserRoles, hasStatusChanged, IsEmailIDUniqueCheckRequired, Helper.GetAppUserInfo());
                }
                else
                {
                    loginID = this.txtLoginId.Text;
                    string generatedPassword = Helper.CreateRandomPassword(SharedConstants.LENGTH_GENERATED_PASSWORD, newFtpLoginID);
                    oUserHdrInfo.GeneratedPassword = generatedPassword;
                    oUserHdrInfo.Password = Helper.GetHashedPassword(generatedPassword);
                    oUserHdrInfo.DateAdded = DateTime.Now;
                    oUserHdrInfo.AddedBy = SessionHelper.CurrentUserLoginID;
                    oUserHdrInfo.LoginID = loginID;
                    oUserHdrInfo.IsNew = true;
                    oUserHdrInfo.AddedByRoleID = SessionHelper.CurrentRoleID;

                    #region FTP Details Insert Save
                    if (isFTPEnabled)
                    {
                        oUserHdrInfo.GeneratedFTPPassword = Helper.CreateRandomPassword(SharedConstants.LENGTH_GENERATED_PASSWORD, newFtpLoginID);
                        oUserHdrInfo.FTPPassword = Helper.GetHashedPassword(oUserHdrInfo.GeneratedFTPPassword);
                    }
                    #endregion

                    //Save User Data.
                    int userid = oUserClient.InsertUser(oUserHdrInfo, listUserRoles, IsEmailIDUniqueCheckRequired, Helper.GetAppUserInfo());
                    if (SessionHelper.CurrentCompanyDatabaseExists.GetValueOrDefault())
                    {
                        oUserHdrInfo.UserID = userid;
                        IsSendMailRequired = true;
                        //Once user is created successfully, Send mail to user
                    }
                    string msg = string.Format(LanguageUtil.GetValue(1530), LanguageUtil.GetValue(1533));
                    Helper.ShowConfirmationMessage(this, msg);
                }
                if (string.IsNullOrEmpty(oUserHdrInfo.LoginID))
                    oUserHdrInfo.LoginID = hdnLoginID.Value;
                if (this.rbYes.Checked || this.rbNo.Checked)
                {
                    if (!string.IsNullOrWhiteSpace(oldFtpLoginID) && newFtpLoginID != oldFtpLoginID)
                        FTPHelper.RemoveFTPUser(oldFtpLoginID);
                    FTPHelper.SetupFTPUser(oUserHdrInfo, isFTPEnabled);
                }
                if (IsSendMailRequired)
                    SendMailToUser(oUserHdrInfo, isFTPEnabled);
                UpdateUserInfoInSession(oUserHdrInfo);
                ClearSessionAndCache();
                HandleRedirection(oUserHdrInfo, listUserRoles, eButtons);
            }
            catch (ARTException ex)
            {
                HandleError();
                Helper.ShowErrorMessage(this, ex);
            }
            catch (Exception ex)
            {
                HandleError();
                Helper.ShowErrorMessage(this, ex);
            }
        }
    }

    private List<int> GetSelectedUserRoles()
    {
        List<int> listUserRoles;
        listUserRoles = new List<int>();
        string[] listUserRolesvalues = hdnSelectValue.Value.Split(',');
        foreach (string item in listUserRolesvalues)
        {
            if (item != "")
                listUserRoles.Add(int.Parse(item));
        }
        return listUserRoles;
    }

    /// <summary>
    /// Handle Redirection
    /// </summary>
    /// <param name="oUserHdrInfo"></param>
    /// <param name="listUserRoles"></param>
    /// <param name="eButtons"></param>
    private void HandleRedirection(UserHdrInfo oUserHdrInfo, List<int> listUserRoles, Buttons eButtons)
    {
        string url = null;

        switch (eButtons)
        {
            case Buttons.AccountAssociation:
                url = "~/Pages/UserAccountAssociation.aspx?" + QueryStringConstants.User_ID + "=" + oUserHdrInfo.UserID
                    + "&" + QueryStringConstants.FROM_PAGE + "=" + Request.QueryString[QueryStringConstants.FROM_PAGE].ToString();
                break;

            case Buttons.Save:
                // Check us coming from Create User or Edit User
                if (Request.QueryString[QueryStringConstants.User_ID] != null)
                {
                    // Edit User
                    /* If coming from User Search, redirect to User Search
                     * If coming from Update My Profile, redirect to Home Page
                     */
                    if (Request.QueryString[QueryStringConstants.FROM_PAGE] != null)
                    {
                        WebEnums.ARTPages ePages = (WebEnums.ARTPages)System.Enum.Parse(typeof(WebEnums.ARTPages), Request.QueryString[QueryStringConstants.FROM_PAGE].ToString());

                        switch (ePages)
                        {
                            case WebEnums.ARTPages.Home:
                                url = Helper.GetHomePageUrl() + "?" + QueryStringConstants.CONFIRMATION_MESSAGE_LABEL_ID + "=1538";
                                break;

                            case WebEnums.ARTPages.UserSearch:
                                url = "~/Pages/UserSearch.aspx?" + QueryStringConstants.CONFIRMATION_MESSAGE_LABEL_ID + "=1538"
                                        + "&" + QueryStringConstants.SHOW_SEARCH_RESULTS + "=1";
                                break;
                        }
                    }
                }
                else
                {
                    // Create User
                    if (SessionHelper.CurrentCompanyDatabaseExists.GetValueOrDefault() && CheckRolesForUserAccountAssociation(listUserRoles))
                    {
                        //url = "~/Pages/UserAccountAssociation.aspx?" + QueryStringConstants.User_ID + "=" + oUserHdrInfo.UserID
                        //    + "&" + QueryStringConstants.FROM_PAGE + "=" + WebEnums.ARTPages.CreateUser.ToString("d");

                        //********Above code commented and replaced by below Code by Prafull on 19-Jul-2010 
                        string msg = string.Format(LanguageUtil.GetValue(1530), LanguageUtil.GetValue(1533));
                        url = "~/Pages/UserAccountAssociation.aspx?" + QueryStringConstants.User_ID + "=" + oUserHdrInfo.UserID
                            + "&" + QueryStringConstants.FROM_PAGE + "=" + WebEnums.ARTPages.CreateUser.ToString("d")
                            + "&" + QueryStringConstants.CONFIRMATION_MESSAGE_LABEL_ID + "=" + 1954;


                    }
                    else
                    {
                        // Show a Panel, with options to Add More user and Go To Home Page
                        trUser.Visible = false;
                        trButtons.Visible = true;
                    }

                }

                break;
        }

        if (url != null)
        {
            //Response.Redirect(url);
            SessionHelper.RedirectToUrl(url);
            return;
        }
    }
    /// <summary>
    /// Handle Error
    /// </summary>
    private void HandleError()
    {
        // Since there was an error while saving, restore the selections for ROle / Default Role
        List<int> listUserRoles = new List<int>();
        string[] listUserRolesvalues = hdnSelectValue.Value.Split(',');
        foreach (string item in listUserRolesvalues)
        {
            if (item != "")
                listUserRoles.Add(int.Parse(item));
        }

        if (listUserRoles.Count > 0)
        {
            this.UserRoleSelection.RemoveaddItemsFromListBoxonvalue(listUserRoles, false);

            List<ListItem> lstSelectedListItems = this.UserRoleSelection.GetSelectedItems();
            foreach (ListItem li in lstSelectedListItems)
            {
                if (!ddlDefaultRole.Items.Contains(li))
                {
                    ddlDefaultRole.Items.Add(li);
                }
            }

            // Show the selected Default Role
            if (!string.IsNullOrEmpty(hdnDefaultRole.Value))
            {
                ListItem oListItem = ddlDefaultRole.Items.FindByValue(hdnDefaultRole.Value);
                if (oListItem != null)
                {
                    ddlDefaultRole.SelectedItem.Selected = false;
                    oListItem.Selected = true;
                }
            }
        }
    }
    /// <summary>
    /// Clear Session And Cache
    /// </summary>
    private void ClearSessionAndCache()
    {
        if (rowPotentialRole.Visible)
        {
            CacheHelper.ClearCache(CacheHelper.GetCacheKeyForCompanyData(CacheConstants.ALL_PREPARERS_FOR_CURRENT_COMPANY));
            CacheHelper.ClearCache(CacheHelper.GetCacheKeyForCompanyData(CacheConstants.ALL_REVIEWERS_FOR_CURRENT_COMPANY));
            CacheHelper.ClearCache(CacheHelper.GetCacheKeyForCompanyData(CacheConstants.ALL_APPROVERS_FOR_CURRENT_COMPANY));
            CacheHelper.ClearCache(CacheHelper.GetCacheKeyForCompanyData(CacheConstants.ALL_BACKUP_PREPARERS_FOR_CURRENT_COMPANY));
            CacheHelper.ClearCache(CacheHelper.GetCacheKeyForCompanyData(CacheConstants.ALL_BACKUP_REVIEWERS_FOR_CURRENT_COMPANY));
            CacheHelper.ClearCache(CacheHelper.GetCacheKeyForCompanyData(CacheConstants.ALL_BACKUP_APPROVERS_FOR_CURRENT_COMPANY));
            CacheHelper.ClearCache(CacheHelper.GetCacheKeyForCompanyData(CacheConstants.ALL_USERS_FOR_CURRENT_COMPANY));
            SessionHelper.ClearAllUserRolesFromSession();
            SessionHelper.ClearAllRolesFromSession();
            SessionHelper.ClearMenuFromSession();
        }
    }
    /// <summary>
    /// Update User Language In Session
    /// </summary>
    /// <param name="oUserHdrInfo"></param>
    private void UpdateUserInfoInSession(UserHdrInfo oUserHdrInfo)
    {
        UserHdrInfo oUserHdrInfoSession = SessionHelper.GetCurrentUser();
        if (oUserHdrInfo.UserID == oUserHdrInfoSession.UserID)
        {
            oUserHdrInfoSession.DefaultLanguageID = oUserHdrInfo.DefaultLanguageID;
            oUserHdrInfoSession.FTPActivationStatusID = oUserHdrInfo.FTPActivationStatusID;
            oUserHdrInfoSession.FTPLoginID = oUserHdrInfo.FTPLoginID;
            oUserHdrInfoSession.FirstName = oUserHdrInfo.FirstName;
            oUserHdrInfoSession.LastName = oUserHdrInfo.LastName;
            oUserHdrInfoSession.EmailID = oUserHdrInfo.EmailID;
        }
    }
    /// <summary>
    /// Check Roles For User Account Association
    /// </summary>
    /// <param name="oUserRoleInfoCollection"></param>
    /// <returns></returns>
    private bool CheckRolesForUserAccountAssociation(List<RoleMstInfo> oUserRoleInfoCollection)
    {
        bool bShowAccountAssociationButton = false;
        ARTEnums.UserRole eUserRole = ARTEnums.UserRole.None;

        foreach (RoleMstInfo oRoleMstInfo in oUserRoleInfoCollection)
        {
            eUserRole = (ARTEnums.UserRole)System.Enum.Parse(typeof(ARTEnums.UserRole), oRoleMstInfo.RoleID.Value.ToString());
            switch (eUserRole)
            {
                case ARTEnums.UserRole.FINANCIAL_MANAGER:
                case ARTEnums.UserRole.BUSINESS_ADMIN:
                case ARTEnums.UserRole.ACCOUNT_MANAGER:
                case ARTEnums.UserRole.CONTROLLER:
                case ARTEnums.UserRole.EXECUTIVE:
                case ARTEnums.UserRole.AUDIT:
                case ARTEnums.UserRole.TASK_OWNER:
                    bShowAccountAssociationButton = true;
                    break;
            }
        }
        return bShowAccountAssociationButton;
    }
    /// <summary>
    /// Check Roles For User Account Association
    /// </summary>
    /// <param name="listUserRoles"></param>
    /// <returns></returns>
    private bool CheckRolesForUserAccountAssociation(List<int> listUserRoles)
    {
        bool bShowAccountAssociationButton = false;
        ARTEnums.UserRole eUserRole = ARTEnums.UserRole.None;

        //to do use enums for comparison
        for (int i = 0; i < listUserRoles.Count; i++)
        {
            eUserRole = (ARTEnums.UserRole)System.Enum.Parse(typeof(ARTEnums.UserRole), listUserRoles[i].ToString());
            switch (eUserRole)
            {
                case ARTEnums.UserRole.FINANCIAL_MANAGER:
                case ARTEnums.UserRole.BUSINESS_ADMIN:
                case ARTEnums.UserRole.ACCOUNT_MANAGER:
                case ARTEnums.UserRole.CONTROLLER:
                case ARTEnums.UserRole.EXECUTIVE:
                case ARTEnums.UserRole.AUDIT:
                case ARTEnums.UserRole.TASK_OWNER:
                    bShowAccountAssociationButton = true;
                    break;
            }
        }
        return bShowAccountAssociationButton;
    }

    private bool CheckRolesForEnableFTP(List<RoleMstInfo> oUserRoleInfoCollection)
    {
        bool bShowEnableFTP = false;
        ARTEnums.UserRole eUserRole = ARTEnums.UserRole.None;
        if (oUserRoleInfoCollection != null)
        {
            foreach (RoleMstInfo oRoleMstInfo in oUserRoleInfoCollection)
            {
                eUserRole = (ARTEnums.UserRole)System.Enum.Parse(typeof(ARTEnums.UserRole), oRoleMstInfo.RoleID.Value.ToString());
                switch (eUserRole)
                {
                    case ARTEnums.UserRole.SYSTEM_ADMIN:
                    case ARTEnums.UserRole.BUSINESS_ADMIN:
                        bShowEnableFTP = true;
                        break;
                }
            }
        }
        return bShowEnableFTP;
    }
    private void SendMailToUserWithFTPDetails(string loginID, string password, string emailID, int? userLanguageID, string FTPServer)
    {
        try
        {
            AppSettingHelper.GetAppSettingValue(AppSettingConstants.EMAIL_PASSWORD);
            StringBuilder oMailBody = new StringBuilder();

            //Getting the User's Full Name              --Added By Prafull
            //************************************************************
            IUser oUserClient = RemotingHelper.GetUserObject();
            UserHdrInfo oUserHdrInfo = new UserHdrInfo();
            oUserHdrInfo = oUserClient.GetUserByLoginID(loginID, Helper.GetAppUserInfo());
            //************************************************************
            // Create multilingual attribute info
            MultilingualAttributeInfo oMultilingualAttributeInfo = LanguageHelper.GetMultilingualAttributeInfo(SessionHelper.CurrentCompanyID, userLanguageID);

            oMailBody.Append(string.Format("{0} ", LanguageUtil.GetValue(1845, oMultilingualAttributeInfo)));
            oMailBody.Append(oUserHdrInfo.Name);
            oMailBody.Append(",");
            oMailBody.Append("<br>");
            oMailBody.Append(string.Format("{0}: ", LanguageUtil.GetValue(2920, oMultilingualAttributeInfo)));
            oMailBody.Append("<br>");
            oMailBody.Append(string.Format("{0}: ", LanguageUtil.GetValue(1269, oMultilingualAttributeInfo)));
            oMailBody.Append(loginID);
            oMailBody.Append("<br>");
            oMailBody.Append(string.Format("{0}: ", LanguageUtil.GetValue(2916, oMultilingualAttributeInfo)));
            oMailBody.Append(password);
            oMailBody.Append("<br>");
            oMailBody.Append(string.Format("{0}: ", LanguageUtil.GetValue(2919, oMultilingualAttributeInfo)));
            oMailBody.Append(FTPServer);
            oMailBody.Append("<br>");
            oMailBody.Append("<br>");
            String msg;
            msg = LanguageUtil.GetValue(2384, oMultilingualAttributeInfo);
            String Url = Request.Url.AbsoluteUri;
            string NewUrl = Url.Replace("Pages/CreateUser.aspx", AppSettingHelper.GetAppSettingValue(AppSettingConstants.EMAIL_SYSTEM_URL));
            //oMailBody.Append(string.Format(msg, NewUrl));
            oMailBody.Append(string.Format(msg, AppSettingHelper.GetAppSettingValue(AppSettingConstants.EMAIL_SYSTEM_URL)));

            //oMailBody.Append(string.Format("{0}: ", LanguageUtil.GetValue(1844)));
            //oMailBody.Append("<br>");
            //oMailBody.Append(string.Format("{0}: ", LanguageUtil.GetValue(1324)));
            //oMailBody.Append(AppSettingHelper.GetAppSettingValue(AppSettingConstants.EMAIL_SYSTEM_URL));
            string fromAddress = AppSettingHelper.GetAppSettingValue(AppSettingConstants.EMAIL_FROM_DEFAULT);
            if (SessionHelper.CurrentRoleEnum == ARTEnums.UserRole.SKYSTEM_ADMIN)
            {
                oMailBody.Append("<br/>" + MailHelper.GetEmailSignature(WebEnums.SignatureEnum.SendBySkyStemSystem, fromAddress, oMultilingualAttributeInfo));
            }
            else
            {
                oMailBody.Append("<br/>" + MailHelper.GetEmailSignature(WebEnums.SignatureEnum.SendBySystemAdmin, fromAddress, oMultilingualAttributeInfo));
            }

            string mailSubject = string.Format("{0}: {1}", LanguageUtil.GetValue(1327, oMultilingualAttributeInfo), LanguageUtil.GetValue(2921, oMultilingualAttributeInfo));

            string toAddress = emailID;
            MailHelper.SendEmail(fromAddress, toAddress, mailSubject, oMailBody.ToString());
        }
        catch (Exception ex)
        {
            Helper.FormatAndShowErrorMessage(null, ex);
        }
    }
    #endregion

    #region Public Methods
    /// <summary>
    /// Get Menu Key
    /// </summary>
    /// <returns></returns>
    public override string GetMenuKey()
    {
        return "CreateNewUser";
    }
    #endregion

}
