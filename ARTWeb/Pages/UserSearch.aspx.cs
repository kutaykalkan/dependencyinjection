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
using SkyStem.Library.Controls.TelerikWebControls.Data;
using SkyStem.Language.LanguageUtility.Classes;
using ART.Integration.Utility;
using SkyStem.ART.Shared.Data;

public partial class Pages_UserSearch : PageBaseCompany
{
    #region Variables & Constants
    bool isExportPDF;
    bool isExportExcel;
    #endregion

    #region Properties
    private CompanyHdrInfo oCompanyHdrInfo
    {
        get
        {
            int CompanyId;
            if (Request.QueryString[QueryStringConstants.COMPANY_ID] != null)
            {
                CompanyId = Convert.ToInt32(Request.QueryString[QueryStringConstants.COMPANY_ID]);
            }
            else
            {
                CompanyId = SessionHelper.CurrentCompanyID.Value;
            }
            ICompany oCompanyClient = RemotingHelper.GetCompanyObject();
            return oCompanyClient.GetCompanyDetail(CompanyId, SessionHelper.CurrentReconciliationPeriodEndDate, Helper.GetAppUserInfo());
        }
        set { oCompanyHdrInfo = value; }
    }
    #endregion

    #region Delegates & Events
    #endregion

    #region Page Events
    protected void Page_PreInit(Object source, EventArgs e)
    {
        if (Request.QueryString["CHECK"] != null)
        {
            this.MasterPageFile = "~/MasterPages/PopUpMasterPage.master";

        }
    }
    protected void Page_Init(object sender, EventArgs e)
    {
        MasterPageBase ompage = (MasterPageBase)this.Master;
        ompage.ReconciliationPeriodChangedEventHandler += ompage_ReconciliationPeriodChangedEventHandler;
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        Helper.RegisterPostBackToControls(this, rgUserList);
        Helper.SetPageTitle(this, 1341);
        if (Request.QueryString["CHECK"] != null)
        {
            rowHideOnAddUser.Visible = false;
        }
        else
        {
            rowHideOnAddUser.Visible = true;
            AddUsers.Visible = false;
        }

        AddUsers.Attributes.Add("onclick", "return AddUserList(" + "'" + rgUserList.ClientID + "'" + "," + "'" + AddUsersList.ClientID + "'" + ")");

        if (!Page.IsPostBack)
        {
            OnPageLoad();
            if (oCompanyHdrInfo.IsFTPEnabled == false || oCompanyHdrInfo.IsFTPEnabled == null)
            {
                btnSearchFTP.Visible = false;
            }
        }
        DDLActHist.Enabled = cbActHist.Checked;
        this.SetFocus(this.txtFirstName);
        GridHelper.SetRecordCount(rgUserList);

    }
    #endregion

    #region Grid Events

    #region User List Bind
    protected void rgUserList_ItemCreated(object sender, GridItemEventArgs e)
    {
        GridHelper.SetStylesForExportGrid(e, isExportPDF, isExportExcel);
    }
    protected void rgUserList_ItemDataBound(object sender, GridItemEventArgs e)
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

            StringBuilder sbRole = new StringBuilder();

            //string DefaultRole = null;
            int i = 0;
            foreach (var item in oUserHdrInfo.UserRoleList)
            {
                i++;
                if (item.IsDefaultRole.HasValue && item.IsDefaultRole.Value)
                {
                    sbRole.Append("* ");
                    sbRole.Append(item.Role);

                }
                else
                {
                    sbRole.Append(item.Role);

                }
                if (i < oUserHdrInfo.UserRoleList.Count)
                    sbRole.Append(", ");


            }
            //StringBuilder sbDate = new StringBuilder();
            //StringBuilder sbStatus = new StringBuilder();
            //StringBuilder sbBy = new StringBuilder();
            //foreach (var item in oUserHdrInfo.UserStatusDetailList)
            //{
            //    sbDate.Append(Helper.GetDisplayDate(item.UserStatusDate));
            //    sbStatus.Append(Helper.GetDisplayStringValue(item.UserStatus));
            //    sbBy.Append(Helper.GetDisplayStringValue(item.AddedByUserName));

            //    sbDate.Append("<br />");
            //    sbStatus.Append("<br />");
            //    sbBy.Append("<br />");

            //}

            ExLabel lblDate = (ExLabel)e.Item.FindControl("lblDate");
            ExLabel lblStatus = (ExLabel)e.Item.FindControl("lblStatus");
            ExLabel lblBy = (ExLabel)e.Item.FindControl("lblBy");
            // ExLabel lblDefaultRole = (ExLabel)e.Item.FindControl("lblDefaultRole");
            ExLabel lblRole = (ExLabel)e.Item.FindControl("lblRole");
            ExLabel lblCreatedBy = (ExLabel)e.Item.FindControl("lblCreatedBy");
            ExLabel lblDateCreated = (ExLabel)e.Item.FindControl("lblDateCreated");
            lblDate.Text = Helper.GetDisplayDateTime(oUserHdrInfo.UserStatusDate);
            lblStatus.Text = Helper.GetDisplayStringValue(oUserHdrInfo.UserStatus);
            lblBy.Text = Helper.GetDisplayStringValue(oUserHdrInfo.AddedByUserName);
            // lblDefaultRole.Text = Helper.GetDisplayStringValue(DefaultRole);
            if (!string.IsNullOrEmpty(sbRole.ToString()))
                lblRole.Text = Helper.GetDisplayStringValue(sbRole.ToString());
            else
                lblRole.Text = "";

            lblCreatedBy.Text = Helper.GetDisplayStringValue(oUserHdrInfo.AddedBy);
            lblDateCreated.Text = Helper.GetDisplayDate(oUserHdrInfo.DateAdded);


            ExHyperLink hlFirstName = (ExHyperLink)e.Item.FindControl("hlFirstName");
            ExHyperLink hlLastName = (ExHyperLink)e.Item.FindControl("hlLastName");
            ExHyperLink hlEmailID = (ExHyperLink)e.Item.FindControl("hlEmailID");
            ExHyperLink hlLoginID = (ExHyperLink)e.Item.FindControl("hlLoginID");
            ExHyperLink hlPhone = (ExHyperLink)e.Item.FindControl("hlPhone");
            ExImageButton imgBtnResetPassword = (ExImageButton)e.Item.FindControl("imgBtnResetPassword");
            ExHyperLink hlIsActive = (ExHyperLink)e.Item.FindControl("hlIsActive");
            ExLabel lblFTPDate = (ExLabel)e.Item.FindControl("lblFTPDate");
            ExHyperLink hlIsFTPActive = (ExHyperLink)e.Item.FindControl("hlIsFTPActive");
            ExHyperLink hlIsUserLocked = (ExHyperLink)e.Item.FindControl("hlIsUserLocked");
            ExHyperLink hlLockdownCount = (ExHyperLink)e.Item.FindControl("hlLockdownCount");

            hlFirstName.Text = Helper.GetDisplayStringValue(oUserHdrInfo.FirstName);
            hlLastName.Text = Helper.GetDisplayStringValue(oUserHdrInfo.LastName);
            hlEmailID.Text = Helper.GetDisplayStringValue(oUserHdrInfo.EmailID);
            hlLoginID.Text = Helper.GetDisplayStringValue(oUserHdrInfo.LoginID);
            hlIsActive.Text = ReportHelper.SetYesNoCodeBasedOnBool(oUserHdrInfo.IsActive);
            hlIsUserLocked.Text = ReportHelper.SetYesNoCodeBasedOnBool(oUserHdrInfo.IsUserLocked);
            hlLockdownCount.Text = Helper.GetDisplayIntegerValueWitoutComma(oUserHdrInfo.LockdownCount);

            if (Request.QueryString["CHECK"] != null)
            {

            }
            else
            {
                string url = GetHyperlinkForEditUser(oUserHdrInfo);
                hlFirstName.NavigateUrl = url;
                hlLastName.NavigateUrl = url;
                hlEmailID.NavigateUrl = url;
                hlLoginID.NavigateUrl = url;
                hlIsActive.NavigateUrl = url;
                hlIsFTPActive.NavigateUrl = url;
                hlIsUserLocked.NavigateUrl = url;
                // hlLockdownCount.NavigateUrl = url;
            }

            if (hlLockdownCount != null)
            {
                if (oUserHdrInfo.UserID.HasValue && oUserHdrInfo.UserID.Value > 0 && oUserHdrInfo.LockdownCount.HasValue && oUserHdrInfo.LockdownCount.Value > 0)
                {

                    string windowName = string.Empty;
                    hlLockdownCount.NavigateUrl = "javascript:OpenRadWindowForHyperlinkWithName('" + Page.ResolveUrl(SetURLForUserLockDown(oUserHdrInfo.UserID, out windowName)) + "', '" + windowName + "', 350, 400);";

                }
                else
                {
                    hlLockdownCount.NavigateUrl = "javascript:";
                }

            }


            if (oUserHdrInfo.CurrentFTPActivationStatusID == (short)ARTEnums.ActivationStatus.Activated)
            {
                hlIsFTPActive.Text = LanguageUtil.GetValue(WebConstants.LABEL_ID_YES);
                lblFTPDate.Text = Helper.GetDisplayDateTime(oUserHdrInfo.FTPActivationDate);
            }
            else if (oUserHdrInfo.CurrentFTPActivationStatusID == (short)ARTEnums.ActivationStatus.Deactivated)
            {
                hlIsFTPActive.Text = LanguageUtil.GetValue(WebConstants.LABEL_ID_NO);
                lblFTPDate.Text = Helper.GetDisplayDateTime(oUserHdrInfo.FTPActivationDate);
            }
            else
            {
                hlIsFTPActive.Text = WebConstants.HYPHEN;
                lblFTPDate.Text = WebConstants.HYPHEN;
            }
            imgBtnResetPassword.CommandArgument = oUserHdrInfo.EmailID + "~" + oUserHdrInfo.LoginID + "~" + oUserHdrInfo.DefaultLanguageID + "~" + oUserHdrInfo.CurrentFTPActivationStatusID.GetValueOrDefault();
            imgBtnResetPassword.Visible = oUserHdrInfo.IsActive.GetValueOrDefault();
        }
    }
    protected void rgUserList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
    {
        if (e.RebindReason != GridRebindReason.InitialLoad)
        {
            bool? isActive = null;
            int? roleID = null;
            int? CompanyId = null;

            // TODO: Apoorv - Pls use Enum for Comparison
            if (ddlStatus.SelectedValue == Convert.ToString((int)ARTEnums.Status.Inactive))
                isActive = false;

            if (ddlStatus.SelectedValue == Convert.ToString((int)ARTEnums.Status.Active))
                isActive = true;

            // TODO: Apoorv - Pls use Enum for Comparison
            if (ddlRole.SelectedValue != "0")
                roleID = Convert.ToInt32(ddlRole.SelectedValue);

            if (Request.QueryString["CHECK"] != null)
            {
                isActive = true;
            }

            List<UserHdrInfo> oUserHdrInfoCollection = new List<UserHdrInfo>();
            string sortExpression = rgUserList.MasterTableView.SortExpressions[0].FieldName;
            string sortDirection = rgUserList.MasterTableView.SortExpressions[0].SortOrderAsString();
            bool IsShowActivationHistory = cbActHist.Checked;
            short ActivationHistoryVal = Convert.ToInt16(DDLActHist.SelectedValue);

            if (Request.QueryString[QueryStringConstants.COMPANY_ID] != null)
            {
                CompanyId = Convert.ToInt32(Request.QueryString[QueryStringConstants.COMPANY_ID]);
            }
            else
            {
                CompanyId = SessionHelper.CurrentCompanyID.Value;
            }
            try
            {
                IUser oUserClient = RemotingHelper.GetUserObject();
                int pageIndex = rgUserList.CurrentPageIndex;
                int pageSize = rgUserList.PageSize;
                int defaultItemCount = Helper.GetDefaultChunkSize(pageSize);
                int count = (pageIndex / pageSize + 1) * defaultItemCount;

                short ActivationStatusTypeID = (short)ARTEnums.ActivationStatusType.UserActivationStatus;
                oUserHdrInfoCollection = oUserClient.SearchUser(txtFirstName.Text.Trim(), txtEmail.Text.Trim(),
                    txtLastName.Text.Trim(), count, roleID, isActive, CompanyId,
                    SessionHelper.CurrentUserID.Value, SessionHelper.CurrentRoleID.Value, SessionHelper.CurrentReconciliationPeriodID,
                    SessionHelper.CurrentReconciliationPeriodEndDate, sortExpression, sortDirection, IsShowActivationHistory, ActivationHistoryVal,
                    ActivationStatusTypeID, null, Helper.GetAppUserInfo());

                LanguageHelper.TranslateLabelUserStatus(oUserHdrInfoCollection);
                foreach (var item in oUserHdrInfoCollection)
                {
                    item.UserRoleList = LanguageHelper.TranslateLabelUserRoleInfo(item.UserRoleList);
                }

                if (oUserHdrInfoCollection.Count % defaultItemCount == 0)
                    rgUserList.VirtualItemCount = oUserHdrInfoCollection.Count + 1;
                else
                    rgUserList.VirtualItemCount = oUserHdrInfoCollection.Count;

            }
            catch (ARTException ex)
            {
                Helper.ShowErrorMessage(this, ex);
            }
            catch (Exception ex)
            {
                Helper.ShowErrorMessage(this, ex);
            }

            rgUserList.MasterTableView.DataSource = oUserHdrInfoCollection;
        }


    }
    protected void rgUserList_ItemCommand(object source, GridCommandEventArgs e)
    {
        if (e.CommandName == "ResetPassword")
        {
            string comargument = e.CommandArgument.ToString();
            string[] listUserRolesvalues = comargument.Split('~');
            string emailID = listUserRolesvalues[0];
            string loginID = listUserRolesvalues[1];
            int DefaultLanguageID = Convert.ToInt32(listUserRolesvalues[2]);
            short ftpActivationStatusID = Convert.ToInt16(listUserRolesvalues[3]);

            IUser oUserClient = RemotingHelper.GetUserObject();
            string generatedPassword = Helper.CreateRandomPassword(SharedConstants.LENGTH_GENERATED_PASSWORD, loginID);
            string Password = Helper.GetHashedPassword(generatedPassword);

            oUserClient.UpdatePassword(loginID, Password, Helper.GetAppUserInfo());

            if (ftpActivationStatusID == (short)ARTEnums.ActivationStatus.Activated)
            {
                UserHdrInfo oUserHdrInfo = oUserClient.GetUserByLoginID(loginID, Helper.GetAppUserInfo());
                FTPHelper.SetupFTPUser(oUserHdrInfo, true);
            }

            SendMailToUser(loginID, generatedPassword, emailID, DefaultLanguageID);

            string msg = string.Format(LanguageUtil.GetValue(1537), emailID);

            MasterPageBase oMasterPageBase = (MasterPageBase)this.Master;
            oMasterPageBase.ShowConfirmationMessage(msg);

        }

        if (e.CommandName == TelerikConstants.GridExportToPDFCommandName)
        {
            isExportPDF = true;
            rgUserList.MasterTableView.Columns.FindByUniqueName("IconColumn").Visible = false;
            GridHelper.ExportGridToPDF(rgUserList, 1341, "294mm");

        }
        if (e.CommandName == TelerikConstants.GridExportToExcelCommandName)
        {
            isExportExcel = true;
            rgUserList.MasterTableView.Columns.FindByUniqueName("IconColumn").Visible = false;
            GridHelper.ExportGridToExcel(rgUserList, 1341);

        }
    }
    protected void rgUserList_SortCommand(object source, GridSortCommandEventArgs e)
    {
        GridHelper.HandleSortCommand(e);
        rgUserList.Rebind();

    }
    #endregion

    #region FTP List Bind
    protected void rgFTP_ItemCreated(object sender, GridItemEventArgs e)
    {
        GridHelper.SetStylesForExportGrid(e, isExportPDF, isExportExcel);
        if (e.Item is GridCommandItem)
        {
            ImageButton ibExportToExcel = (e.Item as GridCommandItem).FindControl(TelerikConstants.GRID_ID_EXPORT_TO_EXCEL_ICON) as ImageButton;
            Helper.RegisterPostBackToControls(this, ibExportToExcel);

        }
        if (e.Item is GridCommandItem)
        {
            ImageButton ibExportToExcel = (e.Item as GridCommandItem).FindControl(TelerikConstants.GRID_ID_EXPORT_TO_PDF_ICON) as ImageButton;
            Helper.RegisterPostBackToControls(this, ibExportToExcel);

        }
    }
    protected void rgFTP_ItemDataBound(object sender, GridItemEventArgs e)
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

            StringBuilder sbRole = new StringBuilder();

            //string DefaultRole = null;
            int i = 0;
            foreach (var item in oUserHdrInfo.UserRoleList)
            {
                i++;
                if (item.IsDefaultRole.HasValue && item.IsDefaultRole.Value)
                {
                    sbRole.Append("* ");
                    sbRole.Append(item.Role);

                }
                else
                {
                    sbRole.Append(item.Role);

                }
                if (i < oUserHdrInfo.UserRoleList.Count)
                    sbRole.Append(", ");
            }
            ExHyperLink hlFirstName = (ExHyperLink)e.Item.FindControl("hlFirstName");
            ExHyperLink hlLastName = (ExHyperLink)e.Item.FindControl("hlLastName");
            ExHyperLink hlEmailID = (ExHyperLink)e.Item.FindControl("hlEmailID");
            ExHyperLink hlLoginID = (ExHyperLink)e.Item.FindControl("hlLoginID");
            ExLabel lblRole = (ExLabel)e.Item.FindControl("lblRole");
            ExLabel lblDate = (ExLabel)e.Item.FindControl("lblDate");
            ExLabel lblActivationHistory = (ExLabel)e.Item.FindControl("lblActivationHistory");
            ExLabel lblFTPBy = (ExLabel)e.Item.FindControl("lblFTPBy");
            ExHyperLink hlIsActive = (ExHyperLink)e.Item.FindControl("hlIsActive");
            ExImageButton imgBtnResetPassword = (ExImageButton)e.Item.FindControl("imgBtnResetPassword");

            hlFirstName.Text = Helper.GetDisplayStringValue(oUserHdrInfo.FirstName);
            hlLastName.Text = Helper.GetDisplayStringValue(oUserHdrInfo.LastName);
            hlEmailID.Text = Helper.GetDisplayStringValue(oUserHdrInfo.EmailID);
            hlLoginID.Text = Helper.GetDisplayStringValue(oUserHdrInfo.LoginID);
            if (!string.IsNullOrEmpty(sbRole.ToString()))
                lblRole.Text = Helper.GetDisplayStringValue(sbRole.ToString());
            else
                lblRole.Text = "";
            lblDate.Text = Helper.GetDisplayDateTime(oUserHdrInfo.FTPActivationDate);
            lblFTPBy.Text = Helper.GetDisplayStringValue(oUserHdrInfo.AddedByUserName);

            if (oUserHdrInfo.CurrentFTPActivationStatusID.GetValueOrDefault() == (short)ARTEnums.ActivationStatus.Activated)
                hlIsActive.Text = LanguageUtil.GetValue(WebConstants.LABEL_ID_YES);
            else if (oUserHdrInfo.CurrentFTPActivationStatusID.GetValueOrDefault() == (short)ARTEnums.ActivationStatus.Deactivated)
                hlIsActive.Text = LanguageUtil.GetValue(WebConstants.LABEL_ID_NO);
            else
            {
                hlIsActive.Text = WebConstants.HYPHEN;
                imgBtnResetPassword.Visible = false;
            }

            if (oUserHdrInfo.FTPActivationStatusID.GetValueOrDefault() == (short)ARTEnums.ActivationStatus.Activated)
                lblActivationHistory.Text = LanguageUtil.GetValue((short)ARTEnums.ActivationStatusLabelID.Activated);
            else if (oUserHdrInfo.FTPActivationStatusID == (int)ARTEnums.ActivationStatus.Deactivated)
                lblActivationHistory.Text = LanguageUtil.GetValue((short)ARTEnums.ActivationStatusLabelID.Deactivated);
            else
                lblActivationHistory.Text = WebConstants.HYPHEN;

            if (Request.QueryString["CHECK"] != null)
            {

            }
            else
            {
                string url = GetHyperlinkForEditUser(oUserHdrInfo);
                hlFirstName.NavigateUrl = url;
                hlLastName.NavigateUrl = url;
                hlEmailID.NavigateUrl = url;
                hlLoginID.NavigateUrl = url;
                hlIsActive.NavigateUrl = url;
            }

            imgBtnResetPassword.CommandArgument = oUserHdrInfo.EmailID + "~" + oUserHdrInfo.LoginID + "~" + oUserHdrInfo.FTPLoginID + "~" + oUserHdrInfo.DefaultLanguageID;
            imgBtnResetPassword.Visible = oUserHdrInfo.IsActive.GetValueOrDefault() && oUserHdrInfo.FTPActivationStatusID.GetValueOrDefault() == (short)ARTEnums.ActivationStatus.Activated;
        }
    }
    protected void rgFTP_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
    {
        if (e.RebindReason != GridRebindReason.InitialLoad)
        {
            bool? isActive = null;
            int? roleID = null;
            short ActivationHistoryVal;
            short? FTPActivationStatusID = null;
            int? CompanyId = null;
            // TODO: Apoorv - Pls use Enum for Comparison
            if (ddlStatus.SelectedValue == Convert.ToString((int)ARTEnums.Status.Inactive))
                FTPActivationStatusID = (short)ARTEnums.ActivationStatus.Deactivated;

            if (ddlStatus.SelectedValue == Convert.ToString((int)ARTEnums.Status.Active))
                FTPActivationStatusID = (short)ARTEnums.ActivationStatus.Activated;

            // TODO: Apoorv - Pls use Enum for Comparison
            if (ddlRole.SelectedValue != "0")
                roleID = Convert.ToInt32(ddlRole.SelectedValue);

            if (Request.QueryString["CHECK"] != null)
            {
                isActive = true;
            }

            List<UserHdrInfo> oUserHdrInfoCollection = new List<UserHdrInfo>();
            string sortExpression = rgFTP.MasterTableView.SortExpressions[0].FieldName;
            string sortDirection = rgFTP.MasterTableView.SortExpressions[0].SortOrderAsString();
            bool IsShowActivationHistory = cbActHist.Checked;

            if (Convert.ToInt16(DDLActHist.SelectedValue) == 2)
            {
                ActivationHistoryVal = (short)ARTEnums.ActivationStatus.Activated;
            }
            else if (Convert.ToInt16(DDLActHist.SelectedValue) == 3)
            {
                ActivationHistoryVal = (short)ARTEnums.ActivationStatus.Deactivated;
            }
            else
            {
                ActivationHistoryVal = 0;
            }

            if (Request.QueryString[QueryStringConstants.COMPANY_ID] != null)
            {
                CompanyId = Convert.ToInt32(Request.QueryString[QueryStringConstants.COMPANY_ID]);
            }
            else
            {
                CompanyId = SessionHelper.CurrentCompanyID.Value;
            }
            try
            {
                IUser oUserClient = RemotingHelper.GetUserObject();
                int pageIndex = rgUserList.CurrentPageIndex;
                int pageSize = rgUserList.PageSize;
                int defaultItemCount = Helper.GetDefaultChunkSize(pageSize);
                int count = (pageIndex / pageSize + 1) * defaultItemCount;

                short ActivationStatusTypeID = (short)ARTEnums.ActivationStatusType.FTPActivationStatus;
                oUserHdrInfoCollection = oUserClient.SearchUser(txtFirstName.Text.Trim(), txtEmail.Text.Trim(),
                    txtLastName.Text.Trim(), count, roleID, isActive, CompanyId,
                    SessionHelper.CurrentUserID.Value, SessionHelper.CurrentRoleID.Value, SessionHelper.CurrentReconciliationPeriodID,
                    SessionHelper.CurrentReconciliationPeriodEndDate, sortExpression, sortDirection, IsShowActivationHistory,
                    ActivationHistoryVal, ActivationStatusTypeID, FTPActivationStatusID, Helper.GetAppUserInfo());

                LanguageHelper.TranslateLabelUserStatus(oUserHdrInfoCollection);
                foreach (var item in oUserHdrInfoCollection)
                {
                    item.UserRoleList = LanguageHelper.TranslateLabelUserRoleInfo(item.UserRoleList);
                }

                if (oUserHdrInfoCollection.Count % defaultItemCount == 0)
                    rgFTP.VirtualItemCount = oUserHdrInfoCollection.Count + 1;
                else
                    rgFTP.VirtualItemCount = oUserHdrInfoCollection.Count;

            }
            catch (ARTException ex)
            {
                Helper.ShowErrorMessage(this, ex);
            }
            catch (Exception ex)
            {
                Helper.ShowErrorMessage(this, ex);
            }

            rgFTP.MasterTableView.DataSource = oUserHdrInfoCollection;
        }

    }
    protected void rgFTP_ItemCommand(object sender, GridCommandEventArgs e)
    {
        if (e.CommandName == "ResetFTPPassword")
        {
            string comargument = e.CommandArgument.ToString();
            string[] listUserRolesvalues = comargument.Split('~');
            string emailID = listUserRolesvalues[0];
            string loginID = listUserRolesvalues[1];
            string ftpLoginID = listUserRolesvalues[2];
            int DefaultLanguageID = Convert.ToInt32(listUserRolesvalues[3]);

            IUser oUserClient = RemotingHelper.GetUserObject();
            string generatedPassword = Helper.CreateRandomPassword(SharedConstants.LENGTH_GENERATED_PASSWORD, ftpLoginID);
            string Password = Helper.GetHashedPassword(generatedPassword);

            IntegrationUtil.ResetPassword(SharedDataImportHelper.GetFTPLoginID(ftpLoginID), generatedPassword);
            oUserClient.UpdateFTPPassword(loginID, ftpLoginID, Password, Helper.GetAppUserInfo());

            SendMailToUserWithFTPDetails(loginID, ftpLoginID, generatedPassword, emailID, DefaultLanguageID);

            string msg = string.Format(LanguageUtil.GetValue(1537), emailID);

            MasterPageBase oMasterPageBase = (MasterPageBase)this.Master;
            oMasterPageBase.ShowConfirmationMessage(msg);

        }

        if (e.CommandName == TelerikConstants.GridExportToPDFCommandName)
        {
            isExportPDF = true;
            rgFTP.MasterTableView.Columns.FindByUniqueName("IconColumn").Visible = false;
            GridHelper.ExportGridToPDF(rgFTP, 2914, "294mm");

        }
        if (e.CommandName == TelerikConstants.GridExportToExcelCommandName)
        {
            isExportExcel = true;
            rgFTP.MasterTableView.Columns.FindByUniqueName("IconColumn").Visible = false;
            GridHelper.ExportGridToExcel(rgFTP, 2914);
        }
    }
    protected void rgFTP_SortCommand(object sender, GridSortCommandEventArgs e)
    {
        GridHelper.HandleSortCommand(e);
        rgFTP.Rebind();
    }

    #endregion

    #endregion

    #region Other Events
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        IUser oUserClient = RemotingHelper.GetUserObject();
        List<UserHdrInfo> oUserHdrInfoCollection = new List<UserHdrInfo>();
        UserHdrInfo oUserHdrInfo = new UserHdrInfo();
        UserRoleInfo oUserRoleInfo = new UserRoleInfo();
        bool? isActive = null;
        int? roleID = null;

        if (ddlStatus.SelectedValue == "2")
            isActive = false;

        if (ddlStatus.SelectedValue == "1")
            isActive = true;

        if (ddlRole.SelectedValue != "0")
            roleID = Convert.ToInt32(ddlRole.SelectedValue);

        if (Request.QueryString["CHECK"] != null)
        {
            isActive = true;
        }
        oUserHdrInfo.FirstName = txtFirstName.Text;
        oUserHdrInfo.LastName = txtLastName.Text;
        oUserHdrInfo.EmailID = txtEmail.Text;
        oUserHdrInfo.IsActive = isActive;
        oUserHdrInfo.ActivationStatusTypeID = (short)ARTEnums.ActivationStatusType.UserActivationStatus;
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


        if (Request.QueryString["CHECK"] != null)
        {
            rgUserList.Columns[0].Visible = true;
            rgUserList.AllowPaging = false;
            rowHideOnAddUser.Visible = false;
            AddUsers.Visible = true;
            rgUserList.AllowExportToPDF = false;
            rgUserList.AllowExportToExcel = false;
            rgUserList.AllowPrint = false;
            rgUserList.AllowPrintAll = false;
            rgUserList.Columns[5].Visible = false;
        }
        else
        {
            rgUserList.Columns[0].Visible = false;
            rowHideOnAddUser.Visible = true;
            AddUsers.Visible = false;
        }
        ShowControls(true);
        ShowFTPControls(false);
        rgUserList.Rebind();
    }
    protected void btnSearchFTP_Click(object sender, EventArgs e)
    {
        IUser oUserClient = RemotingHelper.GetUserObject();
        List<UserHdrInfo> oUserHdrInfoCollection = new List<UserHdrInfo>();
        UserHdrInfo oUserHdrInfo = new UserHdrInfo();
        UserRoleInfo oUserRoleInfo = new UserRoleInfo();
        bool? isActive = null;
        int? roleID = null;

        if (ddlStatus.SelectedValue == "2")
            isActive = false;

        if (ddlStatus.SelectedValue == "1")
            isActive = true;

        if (ddlRole.SelectedValue != "0")
            roleID = Convert.ToInt32(ddlRole.SelectedValue);

        if (Request.QueryString["CHECK"] != null)
        {
            isActive = true;
        }
        oUserHdrInfo.FirstName = txtFirstName.Text;
        oUserHdrInfo.LastName = txtLastName.Text;
        oUserHdrInfo.EmailID = txtEmail.Text;
        oUserHdrInfo.IsActive = isActive;
        oUserHdrInfo.ActivationStatusTypeID = (short)ARTEnums.ActivationStatusType.FTPActivationStatus;

        if (roleID != null)
            oUserRoleInfo.RoleID = (short)roleID;

        Session[SessionConstants.USER_COLLECTION] = oUserHdrInfoCollection;
        Session[SessionConstants.SEARCH_USER_PARAMATERES] = oUserHdrInfo;
        Session[SessionConstants.SEARCH_USER_PARAMATERES_ROLE] = oUserRoleInfo;

        // Add Default Sort as First Name, ASC
        GridSortExpression oGridSortExpression = new GridSortExpression();
        oGridSortExpression.FieldName = "FirstName";
        oGridSortExpression.SortOrder = GridSortOrder.Ascending;
        rgFTP.MasterTableView.SortExpressions.AddSortExpression(oGridSortExpression);


        if (Request.QueryString["CHECK"] != null)
        {
            rgFTP.Columns[0].Visible = true;
            rgFTP.AllowPaging = false;
            rowHideOnAddUser.Visible = false;
            AddUsers.Visible = true;
            rgFTP.AllowExportToPDF = false;
            rgFTP.AllowExportToExcel = false;
            rgFTP.AllowPrint = false;
            rgFTP.AllowPrintAll = false;
            rgFTP.Columns[5].Visible = false;
        }
        else
        {
            rgFTP.Columns[0].Visible = false;
            rowHideOnAddUser.Visible = true;
            AddUsers.Visible = false;
        }
        ShowControls(false);
        ShowFTPControls(true);
        tblLegend.Visible = true;
        rgFTP.Rebind();
    }
    void ompage_ReconciliationPeriodChangedEventHandler(object sender, EventArgs e)
    {
        OnPageLoad();
    }

    #endregion

    #region Validation Control Events
    #endregion

    #region Private Methods
    private void OnPageLoad()
    {
        isExportPDF = false;
        isExportExcel = false;
        Helper.ShowInputRequirementSection(this, 1431);
        rgUserList.ClientSettings.Selecting.AllowRowSelect = true;
        ShowControls(false);
        ShowFTPControls(false);

        ListControlHelper.BindRoleDropDownList(ddlRole);
        ListControlHelper.BindStatusDropDown(ddlStatus);
        ListControlHelper.BindActivationHistoryDropDown(DDLActHist);


        // Check if Coming from some other page
        if (!Page.IsPostBack)
        {
            if (Request.QueryString[QueryStringConstants.SHOW_SEARCH_RESULTS] != null)
            {
                // show search results again

                UserHdrInfo oUserHdrInfo = (UserHdrInfo)Session[SessionConstants.SEARCH_USER_PARAMATERES];
                UserRoleInfo oUserRoleInfo = (UserRoleInfo)Session[SessionConstants.SEARCH_USER_PARAMATERES_ROLE];
                txtFirstName.Text = oUserHdrInfo.FirstName;
                txtEmail.Text = oUserHdrInfo.EmailID;
                txtLastName.Text = oUserHdrInfo.LastName;
                if (oUserHdrInfo.IsActive != null)
                {
                    if ((bool)oUserHdrInfo.IsActive)
                    {
                        ddlStatus.SelectedValue = "1";
                    }
                    else
                    {
                        ddlStatus.SelectedValue = "2";
                    }
                }
                if (oUserRoleInfo.RoleID != null)
                {
                    ListItem oListItem = ddlRole.Items.FindByValue(oUserRoleInfo.RoleID.ToString());
                    if (oListItem != null)
                        oListItem.Selected = true;
                }
                if (oUserHdrInfo.ActivationStatusTypeID.HasValue)
                {
                    if (oUserHdrInfo.ActivationStatusTypeID == (short)ARTEnums.ActivationStatusType.UserActivationStatus)
                        btnSearch_Click(null, null);
                    if (oUserHdrInfo.ActivationStatusTypeID == (short)ARTEnums.ActivationStatusType.FTPActivationStatus)
                        btnSearchFTP_Click(null, null);
                }
            }
        }
    }
    private void ShowControls(bool bShow)
    {
        bool isFTPActivated = FTPHelper.IsFTPActivatedCompanyLevel();
        tblLegend.Visible = bShow;
        rgUserList.Visible = bShow;
        pnlUserGrid.Visible = bShow;

        GridColumn gc = rgUserList.Columns.FindByUniqueNameSafe("FTPDate");
        if (gc != null)
            gc.Visible = bShow && isFTPActivated;
        gc = rgUserList.Columns.FindByUniqueNameSafe("FTPActive");
        if (gc != null)
            gc.Visible = bShow && isFTPActivated;
    }
    private void ShowFTPControls(bool bShow)
    {
        bool isFTPActivated = FTPHelper.IsFTPActivatedCompanyLevel();
        rgFTP.Visible = bShow && isFTPActivated;
    }
    private string GetHyperlinkForEditUser(UserHdrInfo oUserHdrInfo)
    {
        int? CompanyId = null;
        if (Request.QueryString[QueryStringConstants.COMPANY_ID] != null)
        {
            CompanyId = Convert.ToInt32(Request.QueryString[QueryStringConstants.COMPANY_ID]);
        }
        else
        {
            CompanyId = SessionHelper.CurrentCompanyID.Value;
        }
        return "CreateUser.aspx?" + QueryStringConstants.User_ID + "=" + oUserHdrInfo.UserID
            + "&" + QueryStringConstants.FROM_PAGE + "=" + WebEnums.ARTPages.UserSearch.ToString("d") + "&" + QueryStringConstants.COMPANY_ID + "=" + CompanyId;
    }
    private void SendMailToUser(string loginID, string password, string emailID, int DefaultLanguageID)
    {
        try
        {
            AppSettingHelper.GetAppSettingValue(AppSettingConstants.EMAIL_PASSWORD);
            StringBuilder oMailBody = new StringBuilder();
            oMailBody.Append(string.Format("{0} ", LanguageUtil.GetValue(1845)));
            // Create multilingual attribute info
            MultilingualAttributeInfo oMultilingualAttributeInfo = LanguageHelper.GetMultilingualAttributeInfo(SessionHelper.CurrentCompanyID, DefaultLanguageID);

            //Getting the User's Full Name              --Added By Prafull
            //************************************************************
            IUser oUserClient = RemotingHelper.GetUserObject();
            UserHdrInfo oUserHdrInfo = new UserHdrInfo();

            oUserHdrInfo = oUserClient.GetUserByLoginID(loginID, Helper.GetAppUserInfo());
            //************************************************************
            oMailBody.Append(oUserHdrInfo.Name);
            oMailBody.Append(",");
            oMailBody.Append("<br>");

            oMailBody.Append(string.Format("{0}: ", LanguageUtil.GetValue(1761, oMultilingualAttributeInfo)));
            oMailBody.Append("<br>");
            oMailBody.Append(string.Format("{0}: ", LanguageUtil.GetValue(1004, oMultilingualAttributeInfo)));
            oMailBody.Append(password);
            oMailBody.Append("<br>");
            oMailBody.Append("<br>");
            string fromAddress = AppSettingHelper.GetAppSettingValue(AppSettingConstants.EMAIL_FROM_DEFAULT);
            String msg;
            msg = LanguageUtil.GetValue(2384, oMultilingualAttributeInfo);
            String Url = Request.Url.AbsoluteUri;
            string NewUrl = Url.Replace("Pages/CreateUser.aspx", AppSettingHelper.GetAppSettingValue(AppSettingConstants.EMAIL_SYSTEM_URL));
            //oMailBody.Append(string.Format(msg, NewUrl));
            oMailBody.Append(string.Format(msg, AppSettingHelper.GetAppSettingValue(AppSettingConstants.EMAIL_SYSTEM_URL)));
            oMailBody.Append("<br/>" + MailHelper.GetEmailSignature(WebEnums.SignatureEnum.SendBySystemAdmin, fromAddress, oMultilingualAttributeInfo));

            string mailSubject = string.Format("{0}", LanguageUtil.GetValue(1760, oMultilingualAttributeInfo));

            string toAddress = emailID;
            MailHelper.SendEmail(fromAddress, toAddress, mailSubject, oMailBody.ToString());
        }
        catch (Exception ex)
        {
            Helper.FormatAndShowErrorMessage(null, ex);
        }
    }
    private void SendMailToUserWithFTPDetails(string loginID, string ftpLoginID, string password, string emailID, int? userLanguageID)
    {
        try
        {
            AppSettingHelper.GetAppSettingValue(AppSettingConstants.EMAIL_PASSWORD);
            StringBuilder oMailBody = new StringBuilder();
            oMailBody.Append(string.Format("{0} ", LanguageUtil.GetValue(1845)));
            // Create multilingual attribute info
            MultilingualAttributeInfo oMultilingualAttributeInfo = LanguageHelper.GetMultilingualAttributeInfo(SessionHelper.CurrentCompanyID, userLanguageID);

            //Getting the User's Full Name              --Added By Prafull
            //************************************************************
            IUser oUserClient = RemotingHelper.GetUserObject();
            UserHdrInfo oUserHdrInfo = new UserHdrInfo();

            oUserHdrInfo = oUserClient.GetUserByLoginID(loginID, Helper.GetAppUserInfo());
            //************************************************************
            oMailBody.Append(oUserHdrInfo.Name);
            oMailBody.Append(",");
            oMailBody.Append("<br>");

            oMailBody.Append(string.Format("{0}: ", LanguageUtil.GetValue(1761, oMultilingualAttributeInfo)));
            oMailBody.Append("<br>");
            oMailBody.Append(SharedDataImportHelper.GetFTPLoginID(ftpLoginID));
            oMailBody.Append("<br>");
            oMailBody.Append(string.Format("{0}: ", LanguageUtil.GetValue(2916, oMultilingualAttributeInfo)));
            oMailBody.Append(password);
            oMailBody.Append("<br>");
            oMailBody.Append("<br>");
            string fromAddress = AppSettingHelper.GetAppSettingValue(AppSettingConstants.EMAIL_FROM_DEFAULT);
            String msg;
            msg = LanguageUtil.GetValue(2384, oMultilingualAttributeInfo);
            String Url = Request.Url.AbsoluteUri;
            string NewUrl = Url.Replace("Pages/CreateUser.aspx", AppSettingHelper.GetAppSettingValue(AppSettingConstants.EMAIL_SYSTEM_URL));
            //oMailBody.Append(string.Format(msg, NewUrl));
            oMailBody.Append(string.Format(msg, AppSettingHelper.GetAppSettingValue(AppSettingConstants.EMAIL_SYSTEM_URL)));
            oMailBody.Append("<br/>" + MailHelper.GetEmailSignature(WebEnums.SignatureEnum.SendBySystemAdmin, fromAddress, oMultilingualAttributeInfo));

            string mailSubject = string.Format("{0}", LanguageUtil.GetValue(2922, oMultilingualAttributeInfo));

            string toAddress = emailID;
            MailHelper.SendEmail(fromAddress, toAddress, mailSubject, oMailBody.ToString());
        }
        catch (Exception ex)
        {
            Helper.FormatAndShowErrorMessage(null, ex);
        }
    }
    public string SetURLForUserLockDown(int? UserID, out string windowName)
    {
        StringBuilder sbURL = new StringBuilder();
        sbURL.Append(URLConstants.URL_USER_LOCKDOWN_DETAIL);
        sbURL.Append("?" + QueryStringConstants.User_ID + "=" + UserID.GetValueOrDefault().ToString());
        windowName = "UserLockDown";
        return sbURL.ToString();
    }

    #endregion

    #region Other Methods
    public override string GetMenuKey()
    {
        return "UpdateUserProfile";
    }
    #endregion

}
