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
using SkyStem.ART.Client.Exception;
using SkyStem.ART.Client.Data;
using SkyStem.Library.Controls.TelerikWebControls.Data;

public partial class Pages_UserAccountAssociation : PageBaseRecPeriod
{
    #region Variables & Constants
    static int RegionValue;
    static int userID;
    static short roleID;
    bool IsActiveUser;
    bool isExportPDF;
    bool isExportExcel;
    #endregion

    #region Properties
    #endregion

    #region Delegates & Events
    #endregion

    #region Page Events
    protected void Page_Init(object sender, EventArgs e)
    {
        optAllAccountYes.CheckedChanged += OptAllAccountYes_CheckedChanged;
        optAllAccountNo.CheckedChanged += OptAllAccountNo_CheckedChanged;
        optAllAccountYes.AutoPostBack = true;
        optAllAccountNo.AutoPostBack = true;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        isExportPDF = false;
        isExportExcel = false;
        Helper.SetBreadcrumbs(this, 1074, 1535, 1586);
        //Helper.SetBreadcrumbs(this, 1204, 1586);

        SetAttributeForDeleteButton();
        SetValidationMessages();

        userID = Convert.ToInt32(Request.QueryString[QueryStringConstants.User_ID]);
        string[] ogeograbhyobjectarray = { "GeographyObjectID" };
        ucSkyStemARTAccountOwnershipGrid.Grid.MasterTableView.DataKeyNames = ogeograbhyobjectarray;
        ucSkyStemARTGrid.Grid.MasterTableView.DataKeyNames = ogeograbhyobjectarray;
        ucSkyStemARTAccountOwnershipGrid.Grid.EntityNameLabelID = 1596;
        ucSkyStemARTGrid.Grid.EntityNameLabelID = 1596;

        // Fetch User Details
        IUser oUserClient = RemotingHelper.GetUserObject();

        UserHdrInfo oUserHdrInfo = oUserClient.GetUserDetail(userID, Helper.GetAppUserInfo());
        lblFirstNameValue.Text = oUserHdrInfo.FirstName;
        lblLastNameValue.Text = oUserHdrInfo.LastName;
        LblLoginIDValue.Text = oUserHdrInfo.LoginID;
        lblEmailIDValue.Text = oUserHdrInfo.EmailID;
        InitailizeGrid();

        IsActiveUser = oUserHdrInfo.IsActive.GetValueOrDefault();

        if (!Page.IsPostBack)
        {
            Helper.SetPageTitle(this, 1586);
            BindRoleDropdown();
            BindUserSearchRoleDropdown();

            Session[SessionConstants.SEARCH_ACCOUNT_ASSOCIATION] = null;
            Session[SessionConstants.USER_ACCOUNT_ASSOCIATION] = null;
            Session[SessionConstants.SEARCH_USER_ASSOCIATION_BY_USER_ROLE] = null;
            Session[SessionConstants.USER_ASSOCIATION_BY_USER_ROLE] = null;

            //get previous saved association

            roleID = Convert.ToInt16(ddlRole.SelectedValue);
            BindUserAccountGeographyObjectAssociatedGrid();

            ucSkyStemARTGrid.Visible = false;

        }
        if (oUserHdrInfo.IsActive.HasValue && oUserHdrInfo.IsActive == false)
        {
            btnAdd.Visible = false;
            btnAddMore.Visible = false;
            //btnDelete.Visible = false;
            btnSave.Visible = false;
        }
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        Helper.EnableDisableOrgHierarchyForNoKey(lblOrganizationalHiearachy, txtOrganizationalHiearachy, null, null);
        ShowHideSections();
        //if (optAllAccountYes.Checked)
        //{
        //    cpeAccountSearch.Collapsed = true;
        //    cpeUserSearch.Collapsed = true;
        //    tblAssociationDisplay.Style.Add(HtmlTextWriterStyle.Display, "none");
        //    tblUserRoleAssociationDisplay.Style.Add(HtmlTextWriterStyle.Display, "none");
        //    pnlAccountSearchHeader.Style.Add(HtmlTextWriterStyle.Display, "none");
        //    pnlAccountSearchContent.Style.Add(HtmlTextWriterStyle.Display, "none");
        //    pnlUserSearchHeader.Style.Add(HtmlTextWriterStyle.Display, "none");
        //    pnlUserSearchContent.Style.Add(HtmlTextWriterStyle.Display, "none");
        //}
        //else
        //{
        //    cpeAccountSearch.Collapsed = false;
        //    cpeUserSearch.Collapsed = false;
        //    tblAssociationDisplay.Style.Remove(HtmlTextWriterStyle.Display);
        //    tblUserRoleAssociationDisplay.Style.Remove(HtmlTextWriterStyle.Display); 
        //    pnlAccountSearchHeader.Style.Remove(HtmlTextWriterStyle.Display);
        //    pnlAccountSearchContent.Style.Remove(HtmlTextWriterStyle.Display);
        //    pnlUserSearchHeader.Style.Remove(HtmlTextWriterStyle.Display);
        //    pnlUserSearchContent.Style.Remove(HtmlTextWriterStyle.Display);
        //}
    }
    #endregion

    #region Grid Events




    //*****************************Added By Prafull on 14-Apr-2011----------------
    protected void ucSkyStemARTAccountOwnershipGrid_GridItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {
        try
        {
            ListItem selectOne = new ListItem(LanguageUtil.GetValue(1343), WebConstants.SELECT_ONE);
            if (e.Item.ItemType == Telerik.Web.UI.GridItemType.Item || e.Item.ItemType == Telerik.Web.UI.GridItemType.AlternatingItem)
            {
                AccountHdrInfo oAccountHdrInfo = (AccountHdrInfo)e.Item.DataItem;

                ExLabel lblAccountNumber = (ExLabel)e.Item.FindControl("lblAccountNumberOwnershipGrid");
                lblAccountNumber.Text = oAccountHdrInfo.AccountNumber;

                ExLabel lblAccountName = (ExLabel)e.Item.FindControl("lblAccountNameOwnershipGrid");
                lblAccountName.Text = oAccountHdrInfo.AccountName;

                if ((e.Item as GridDataItem)["ID"] != null)
                {
                    (e.Item as GridDataItem)["ID"].Text = oAccountHdrInfo.AccountID.ToString();
                }
            }
        }
        catch (Exception ex)
        {
            Helper.ShowErrorMessage(this, ex);
        }
    }


    protected void ucSkyStemARTGrid_GridItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {
        try
        {
            ListItem selectOne = new ListItem(LanguageUtil.GetValue(1343), WebConstants.SELECT_ONE);
            if (e.Item.ItemType == Telerik.Web.UI.GridItemType.Item || e.Item.ItemType == Telerik.Web.UI.GridItemType.AlternatingItem)
            {
                AccountHdrInfo oAccountHdrInfo = (AccountHdrInfo)e.Item.DataItem;

                ExLabel lblAccountNumber = (ExLabel)e.Item.FindControl("lblAccountNumber");
                lblAccountNumber.Text = oAccountHdrInfo.AccountNumber;

                ExLabel lblAccountName = (ExLabel)e.Item.FindControl("lblAccountName");
                lblAccountName.Text = oAccountHdrInfo.AccountName;

                if ((e.Item as GridDataItem)["ID"] != null)
                {
                    (e.Item as GridDataItem)["ID"].Text = oAccountHdrInfo.AccountID.ToString();
                }
            }
        }
        catch (Exception ex)
        {
            Helper.ShowErrorMessage(this, ex);
        }
    }

    protected void rgUserRoleSelected_GridItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {
        try
        {
            if (e.Item.ItemType == Telerik.Web.UI.GridItemType.Item || e.Item.ItemType == Telerik.Web.UI.GridItemType.AlternatingItem)
            {
                UserHdrInfo oUserHdrInfo = (UserHdrInfo)e.Item.DataItem;
                ExLabel lblAddedGridFirstName = (ExLabel)e.Item.FindControl("lblAddedGridFirstName");
                ExLabel lblAddedGridLastName = (ExLabel)e.Item.FindControl("lblAddedGridLastName");
                ExLabel lblAddedGridLoginID = (ExLabel)e.Item.FindControl("lblAddedGridLoginID");
                ExLabel lblAddedGridRole = (ExLabel)e.Item.FindControl("lblAddedGridRole");
                ExLabel lblAddedGridEmailID = (ExLabel)e.Item.FindControl("lblAddedGridEmailID");

                lblAddedGridFirstName.Text = Helper.GetDisplayStringValue(oUserHdrInfo.FirstName);
                lblAddedGridLastName.Text = Helper.GetDisplayStringValue(oUserHdrInfo.LastName);
                lblAddedGridLoginID.Text = Helper.GetDisplayStringValue(oUserHdrInfo.LoginID);
                lblAddedGridRole.Text = Helper.GetDisplayStringValue(oUserHdrInfo.ChildRole);
                lblAddedGridEmailID.Text = Helper.GetDisplayStringValue(oUserHdrInfo.EmailID);
            }
        }
        catch (Exception ex)
        {
            Helper.ShowErrorMessage(this, ex);
        }
    }

    protected void rgUserRoleSelected_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
    {
        if (e.RebindReason != GridRebindReason.InitialLoad)
        {
            List<UserHdrInfo> oUserHdrInfoCollection = new List<UserHdrInfo>();
            try
            {
                oUserHdrInfoCollection = (List<UserHdrInfo>)Session[SessionConstants.USER_ASSOCIATION_BY_USER_ROLE];
            }
            catch (ARTException ex)
            {
                Helper.ShowErrorMessage(this, ex);
            }
            catch (Exception ex)
            {
                Helper.ShowErrorMessage(this, ex);
            }

            rgUserRoleSelected.MasterTableView.DataSource = oUserHdrInfoCollection;
        }
    }

    protected void rgUserRoleSelected_ItemCreated(object sender, GridItemEventArgs e)
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

    protected void rgUserRoleSelected_ItemCommand(object source, GridCommandEventArgs e)
    {
        if (e.CommandName == TelerikConstants.GridExportToPDFCommandName)
        {
            isExportPDF = true;
            rgUserRoleSelected.MasterTableView.Columns.FindByUniqueName("CheckboxSelectColumn").Visible = false;
            GridHelper.ExportGridToPDF(rgUserRoleSelected, 1341, "294mm");
        }
        if (e.CommandName == TelerikConstants.GridExportToExcelCommandName)
        {
            isExportExcel = true;
            rgUserRoleSelected.MasterTableView.Columns.FindByUniqueName("CheckboxSelectColumn").Visible = false;
            GridHelper.ExportGridToExcel(rgUserRoleSelected, 1341);
        }
    }

    protected void rgUserSearchGrid_GridItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {
        try
        {
            if (e.Item.ItemType == Telerik.Web.UI.GridItemType.Item || e.Item.ItemType == Telerik.Web.UI.GridItemType.AlternatingItem)
            {
                UserHdrInfo oUserHdrInfo = (UserHdrInfo)e.Item.DataItem;
                ExLabel lblSearchGridFirstName = (ExLabel)e.Item.FindControl("lblSearchGridFirstName");
                ExLabel lblSearchGridLastName = (ExLabel)e.Item.FindControl("lblSearchGridLastName");
                ExLabel lblSearchGridLoginID = (ExLabel)e.Item.FindControl("lblSearchGridLoginID");
                ExLabel lblSearchGridRole = (ExLabel)e.Item.FindControl("lblSearchGridRole");
                ExLabel lblSearchGridEmailID = (ExLabel)e.Item.FindControl("lblSearchGridEmailID");

                lblSearchGridFirstName.Text = Helper.GetDisplayStringValue(oUserHdrInfo.FirstName);
                lblSearchGridLastName.Text = Helper.GetDisplayStringValue(oUserHdrInfo.LastName);
                lblSearchGridLoginID.Text = Helper.GetDisplayStringValue(oUserHdrInfo.LoginID);
                lblSearchGridRole.Text = Helper.GetDisplayStringValue(oUserHdrInfo.ChildRole);
                lblSearchGridEmailID.Text = Helper.GetDisplayStringValue(oUserHdrInfo.EmailID);
            }
        }
        catch (Exception ex)
        {
            Helper.ShowErrorMessage(this, ex);
        }
    }

    protected void rgUserSearchList_SortCommand(object source, GridSortCommandEventArgs e)
    {
        GridHelper.HandleSortCommand(e);
        rgUserSearchList.Rebind();
    }

    protected void rgUserSearchList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
    {
        if (e.RebindReason != GridRebindReason.InitialLoad)
        {
            {
                try
                {
                    IUser oUserClient = RemotingHelper.GetUserObject();
                    int pageIndex = rgUserSearchList.CurrentPageIndex;
                    int pageSize = rgUserSearchList.PageSize;
                    int defaultItemCount = Helper.GetDefaultChunkSize(pageSize);
                    int count = (pageIndex / pageSize + 1) * defaultItemCount;
                    string sortExpression = rgUserSearchList.MasterTableView.SortExpressions[0].FieldName;
                    string sortDirection = rgUserSearchList.MasterTableView.SortExpressions[0].SortOrderAsString();
                    short? searchRoleID = Convert.ToInt16(ddlRoleUserSearch.SelectedValue);

                    short ActivationStatusTypeID = (short)ARTEnums.ActivationStatusType.UserActivationStatus;
                    List<UserHdrInfo> oUserHdrInfoList = oUserClient.SearchUser(txtFirstName.Text.Trim(), txtEmail.Text.Trim(),
                        txtLastName.Text.Trim(), count, searchRoleID, true, SessionHelper.CurrentCompanyID,
                        SessionHelper.CurrentUserID.Value, SessionHelper.CurrentRoleID.Value, SessionHelper.CurrentReconciliationPeriodID,
                        SessionHelper.CurrentReconciliationPeriodEndDate, sortExpression, sortDirection, false, (short)ARTEnums.ActivationStatus.All,
                        ActivationStatusTypeID, null, Helper.GetAppUserInfo());

                    if (oUserHdrInfoList != null && oUserHdrInfoList.Count > 0)
                    {
                        foreach (var item in oUserHdrInfoList)
                        {
                            item.UserRoleList = LanguageHelper.TranslateLabelUserRoleInfo(item.UserRoleList);
                        }
                        oUserHdrInfoList = ExpandUserHdrInfo(oUserHdrInfoList);
                    }

                    if (oUserHdrInfoList.Count % defaultItemCount == 0)
                        rgUserSearchList.VirtualItemCount = oUserHdrInfoList.Count + 1;
                    else
                        rgUserSearchList.VirtualItemCount = oUserHdrInfoList.Count;
                    rgUserSearchList.MasterTableView.DataSource = oUserHdrInfoList;
                    Session[SessionConstants.SEARCH_USER_ASSOCIATION_BY_USER_ROLE] = oUserHdrInfoList;

                }
                catch (ARTException ex)
                {
                    Helper.ShowErrorMessage(this, ex);
                }
                catch (Exception ex)
                {
                    Helper.ShowErrorMessage(this, ex);
                }
            }
        }
    }
    #endregion

    #region Other Events

    private void OptAllAccountNo_CheckedChanged(object sender, EventArgs e)
    {
        ShowHideSections();
    }

    private void OptAllAccountYes_CheckedChanged(object sender, EventArgs e)
    {
        ShowHideSections();
    }
    protected void ddlUserAccountAssocition_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        btnAdd.Visible = false;

        Session[SessionConstants.SEARCH_ACCOUNT_ASSOCIATION] = null;
        Session[SessionConstants.USER_ACCOUNT_ASSOCIATION] = null;

    }

    protected void btnSearch_OnClick(object sender, EventArgs e)
    {


        // check if valid search
        //if (ucOrganizationalHierarchyDropdown.SelectedValue != null && ucOrganizationalHierarchyDropdown.SelectedValue != "-2")
        //{
        AccountSearchCriteria oAccountSearchCriteria = new AccountSearchCriteria();
        int geoClassID = Convert.ToInt32(ucOrganizationalHierarchyDropdown.SelectedValue);
        string geoValue = txtOrganizationalHiearachy.Text;

        if (geoClassID > 0)
        {
            oAccountSearchCriteria.Key2 = geoClassID;
            oAccountSearchCriteria.Key2Value = geoValue;
        }
        oAccountSearchCriteria.CompanyID = SessionHelper.CurrentCompanyID.Value;
        oAccountSearchCriteria.LCID = SessionHelper.GetUserLanguage();
        oAccountSearchCriteria.DefaultLanguageID = AppSettingHelper.GetDefaultLanguageID();
        oAccountSearchCriteria.BusinessEntityID = SessionHelper.GetBusinessEntityID();

        if (!string.IsNullOrEmpty(txtAcNumber.Text))
        {
            oAccountSearchCriteria.FromAccountNumber = txtAcNumber.Text.Trim();
        }

        if (!string.IsNullOrEmpty(txtToAcNumber.Text))
        {
            oAccountSearchCriteria.ToAccountNumber = txtToAcNumber.Text.Trim();
        }

        if (!string.IsNullOrEmpty(txtFsCaption.Text))
        {
            oAccountSearchCriteria.FSCaption = txtFsCaption.Text.Trim();
        }

        if (!string.IsNullOrEmpty(txtAcName.Text))
        {
            oAccountSearchCriteria.AccountName = txtAcName.Text.Trim();
        }

        IAccount oAccountClient = RemotingHelper.GetAccountObject();

        //search organistaion hierachy
        if (
            geoClassID > 0
            && string.IsNullOrEmpty(txtAcName.Text)
            && string.IsNullOrEmpty(txtFsCaption.Text)
            && string.IsNullOrEmpty(txtToAcNumber.Text)
            && string.IsNullOrEmpty(txtAcNumber.Text)
            )
        {

            ucSkyStemARTGrid.Visible = true;
            Session[SessionConstants.SEARCH_ACCOUNT_ASSOCIATION] = oAccountClient.GetAccountByOrganisationalHierarchy(oAccountSearchCriteria, Helper.GetAppUserInfo());

            ucSkyStemARTGrid.ShowSelectCheckBoxColum = true;
            ucSkyStemARTGrid.CompanyID = SessionHelper.CurrentCompanyID.Value;
            ucSkyStemARTGrid.DataSource = Session[SessionConstants.SEARCH_ACCOUNT_ASSOCIATION];
            ucSkyStemARTGrid.BindGrid();
            for (int i = geoClassID + 1; i < ucSkyStemARTGrid.Grid.Columns.Count; i++)
            {
                //ucSkyStemARTGrid.Grid.Columns[i].Visible = false;
            }
            ucSkyStemARTGrid.ShowSelectCheckBoxColum = true;
        }
        else
        {

            ucSkyStemARTGrid.Visible = true;
            oAccountSearchCriteria.UserID = SessionHelper.CurrentUserID.Value;
            oAccountSearchCriteria.UserRoleID = SessionHelper.CurrentRoleID.Value;
            oAccountSearchCriteria.PageID = (int)WebEnums.ARTPages.AccountOwnership;
            Session[SessionConstants.SEARCH_ACCOUNT_ASSOCIATION] = oAccountClient.SearchAccount(oAccountSearchCriteria, Helper.GetAppUserInfo());


            ucSkyStemARTGrid.ShowSelectCheckBoxColum = true;
            ucSkyStemARTGrid.Grid.AllowCustomPaging = false;
            ucSkyStemARTGrid.CompanyID = SessionHelper.CurrentCompanyID.Value;
            ucSkyStemARTGrid.Grid.EntityNameLabelID = 1071;
            ucSkyStemARTGrid.DataSource = Session[SessionConstants.SEARCH_ACCOUNT_ASSOCIATION];
            ucSkyStemARTGrid.BindGrid();

        }
        //btnAdd.Visible = true;

        //}
        //else
        //{
        //    btnAdd.Visible = false;
        //}

        //ucSkyStemARTGrid.Visible = true;
        //ucSkyStemARTGrid.DataSource = Session["objoAccountHdrInfoCollection1"];
        //if (IsActiveUser == false)
        //    btnAdd.Visible = false;
        ShowHideSections();
    }

    protected void btnUserSearch_OnClick(object sender, EventArgs e)
    {
        // Add Default Sort as First Name, ASC
        GridSortExpression oGridSortExpression = new GridSortExpression();
        oGridSortExpression.FieldName = "FirstName";
        oGridSortExpression.SortOrder = GridSortOrder.Ascending;
        rgUserSearchList.MasterTableView.SortExpressions.AddSortExpression(oGridSortExpression);
        rgUserSearchList.Rebind();
        //btnAddUserRole.Visible = true;
        //if (IsActiveUser == false)
        //    btnAddUserRole.Visible = false;
        ShowHideSections();
    }

    protected void btnAddMore_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Pages/CreateUser.aspx");
    }

    protected void btnHome_Click(object sender, EventArgs e)
    {

        Helper.RedirectToHomePage();
    }

    protected void btnAdd_OnClick(object sender, EventArgs e)
    {
        Helper.HideMessage(this);

        optAllAccountNo.Checked = true;
        List<AccountHdrInfo> oAccountHdrInfoCollection = (List<AccountHdrInfo>)Session[SessionConstants.SEARCH_ACCOUNT_ASSOCIATION];
        List<AccountHdrInfo> oUserAccountHdrInfoCollection;
        if (Session[SessionConstants.USER_ACCOUNT_ASSOCIATION] != null)
        {
            oUserAccountHdrInfoCollection = (List<AccountHdrInfo>)Session[SessionConstants.USER_ACCOUNT_ASSOCIATION];
        }
        else
        {
            oUserAccountHdrInfoCollection = new List<AccountHdrInfo>();
        }

        AddToOwnershipGrid(oUserAccountHdrInfoCollection, oAccountHdrInfoCollection);
    }

    protected void btnAddUserRole_OnClick(object sender, EventArgs e)
    {
        Helper.HideMessage(this);

        pnlSaveData.Visible = true;
        btnSave.Visible = true;
        btnDeleteUser.Visible = true;
        optAllAccountNo.Checked = true;
        List<UserHdrInfo> oUserHdrInfoListSearch = (List<UserHdrInfo>)Session[SessionConstants.SEARCH_USER_ASSOCIATION_BY_USER_ROLE];
        List<UserHdrInfo> oUserHdrInfoListUser;
        if (Session[SessionConstants.USER_ASSOCIATION_BY_USER_ROLE] != null)
        {
            oUserHdrInfoListUser = (List<UserHdrInfo>)Session[SessionConstants.USER_ASSOCIATION_BY_USER_ROLE];
        }
        else
        {
            oUserHdrInfoListUser = new List<UserHdrInfo>();
        }

        AddToUserRoleGrid(oUserHdrInfoListUser, oUserHdrInfoListSearch);
    }

    protected void btnSave_OnClick(object sender, EventArgs e)
    {
        Helper.HideMessage(this);
        if (Page.IsValid)
        {
            List<AccountHdrInfo> oUserAccountHdrInfoCollection = (List<AccountHdrInfo>)Session[SessionConstants.USER_ACCOUNT_ASSOCIATION];
            List<UserHdrInfo> oUserHdrInfoAdded = (List<UserHdrInfo>)Session[SessionConstants.USER_ASSOCIATION_BY_USER_ROLE];

            List<long> accountIDCollection = new List<long>();
            List<UserGeographyObjectInfo> oUserGeographyObjectInfoCollection = new List<UserGeographyObjectInfo>();
            IUser oUserClient = RemotingHelper.GetUserObject();

            if (optAllAccountYes.Checked)
            {
                if (IsActiveUser)
                {
                    DeleteAllFromOwnershipGrid(oUserAccountHdrInfoCollection);
                    oUserClient.InsertUserOWnershipAccountAndGeographyObjectHdr(null, null, null, true, userID, roleID, Helper.GetAppUserInfo());
                }
            }
            else
            {
                if (oUserAccountHdrInfoCollection != null && oUserAccountHdrInfoCollection.Count > 0)
                {
                    foreach (AccountHdrInfo oAccountHdrInfo in oUserAccountHdrInfoCollection)
                    {
                        //// tempapory comment
                        ////if (oAccountHdrInfo.KeySize != null && oAccountHdrInfo.KeySize > 0)   //If Geography Object Is selected 
                        ////{

                        ////    UserGeographyObjectInfo oUserGeographyObjectInfo = new UserGeographyObjectInfo();
                        ////    oUserGeographyObjectInfo.GeographyObjectID = (Int32)oAccountHdrInfo.GeographyObjectID;
                        ////    oUserGeographyObjectInfo.UserID = userID;
                        ////    oUserGeographyObjectInfo.IsActive = true;
                        ////    oUserGeographyObjectInfo.RoleID = roleID;

                        ////    if (oAccountHdrInfo.KeySize != null)
                        ////    {
                        ////        oUserGeographyObjectInfo.KeySize = (int)oAccountHdrInfo.KeySize;
                        ////    }
                        ////    else
                        ////    {
                        ////        oUserGeographyObjectInfo.KeySize = Convert.ToInt32(ucOrganizationalHierarchyDropdown.SelectedValue);
                        ////    }

                        ////    oUserGeographyObjectInfoCollection.Add(oUserGeographyObjectInfo);
                        ////}


                        ////else   //case when Account Is selected 
                        ////{
                        ////    accountIDCollection.Add((long)oAccountHdrInfo.AccountID);

                        ////}
                        if (oAccountHdrInfo.AccountID.HasValue && (!string.IsNullOrEmpty(this.txtFsCaption.Text) || !string.IsNullOrEmpty(this.txtAcName.Text) || (!string.IsNullOrEmpty(this.txtToAcNumber.Text) || !string.IsNullOrEmpty(this.txtAcNumber.Text))))
                        {
                            accountIDCollection.Add((long)oAccountHdrInfo.AccountID);
                        }
                        else if (oAccountHdrInfo.KeySize != null && oAccountHdrInfo.KeySize > 0)   //If Geography Object Is selected 
                        {
                            UserGeographyObjectInfo oUserGeographyObjectInfo = new UserGeographyObjectInfo();
                            oUserGeographyObjectInfo.GeographyObjectID = (Int32)oAccountHdrInfo.GeographyObjectID;
                            oUserGeographyObjectInfo.UserID = userID;
                            oUserGeographyObjectInfo.IsActive = true;
                            oUserGeographyObjectInfo.RoleID = roleID;

                            if (oAccountHdrInfo.KeySize != null)
                            {
                                oUserGeographyObjectInfo.KeySize = (int)oAccountHdrInfo.KeySize;
                            }
                            else
                            {
                                oUserGeographyObjectInfo.KeySize = Convert.ToInt32(ucOrganizationalHierarchyDropdown.SelectedValue);
                            }

                            oUserGeographyObjectInfoCollection.Add(oUserGeographyObjectInfo);
                        }
                        else if (oAccountHdrInfo.AccountID.HasValue)
                        {
                            accountIDCollection.Add((long)oAccountHdrInfo.AccountID);
                        }


                    }

                    //checkvalidation whether A/c Already Assigned to the Existing User(having same role)

                    //******Condition Commented as per requirement********************************************
                    //bool IsValidAccountAssociated = oUserClient.IsValidAccountsAssociated(oUserGeographyObjectInfoCollection, accountIDCollection, (int)SessionHelper.CurrentCompanyID, (int)SessionHelper.CurrentReconciliationPeriodID, roleID,userID);
                    //if (IsValidAccountAssociated == false)
                    //{
                    //    Helper.ShowErrorMessage(this, LanguageUtil.GetValue(5000244));
                    //    return;
                    //}
                    //else
                    //{
                    //    oUserClient.InsertUserOWnershipAccountAndGeographyObjectHdr(oUserGeographyObjectInfoCollection, accountIDCollection, userID, roleID);

                    //}
                }
                //******************************************************************************************
                if (IsActiveUser)
                {
                    oUserClient.InsertUserOWnershipAccountAndGeographyObjectHdr(oUserGeographyObjectInfoCollection, accountIDCollection, oUserHdrInfoAdded, false, userID, roleID, Helper.GetAppUserInfo());
                }

            }


            string url = "";
            if (Request.QueryString[QueryStringConstants.FROM_PAGE] != null)
            {
                WebEnums.ARTPages ePages = (WebEnums.ARTPages)System.Enum.Parse(typeof(WebEnums.ARTPages), Request.QueryString[QueryStringConstants.FROM_PAGE].ToString());

                switch (ePages)
                {
                    case WebEnums.ARTPages.CreateUser:
                        pnlHidePage.Visible = false;
                        pnlADDUsers.Visible = true;

                        //string msg = string.Format(LanguageUtil.GetValue(1530), LanguageUtil.GetValue(1533));
                        //Helper.ShowConfirmationMessage(this, msg);
                        //********* By Prafull on 19-Jul-2010
                        string msg = LanguageUtil.GetValue(1956);
                        Helper.ShowConfirmationMessage(this, msg);

                        break;



                    case WebEnums.ARTPages.UserSearch:
                        url = "~/Pages/UserSearch.aspx?" + QueryStringConstants.CONFIRMATION_MESSAGE_LABEL_ID + "=1808"
                                + "&" + QueryStringConstants.SHOW_SEARCH_RESULTS + "=1";
                        Response.Redirect(url);
                        break;
                }

            }



        }

    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        string url = "";


        if (Request.QueryString[QueryStringConstants.FROM_PAGE] != null)
        {
            WebEnums.ARTPages ePages = (WebEnums.ARTPages)System.Enum.Parse(typeof(WebEnums.ARTPages), Request.QueryString[QueryStringConstants.FROM_PAGE].ToString());

            switch (ePages)
            {
                case WebEnums.ARTPages.Home:
                    url = "~/Pages/CreateUser.aspx?" + QueryStringConstants.User_ID + "=" + userID
                        + "&" + QueryStringConstants.FROM_PAGE + "=" + Request.QueryString[QueryStringConstants.FROM_PAGE].ToString();
                    break;

                case WebEnums.ARTPages.CreateUser:
                    pnlHidePage.Visible = false;
                    pnlADDUsers.Visible = true;



                    //string msg = string.Format(LanguageUtil.GetValue(1530), LanguageUtil.GetValue(1533));
                    //Helper.ShowConfirmationMessage(this, msg);
                    //*******By Prafull on 19-Jul-2010 
                    string msg = "";
                    Helper.ShowConfirmationMessage(this, msg);



                    break;

                case WebEnums.ARTPages.UserSearch:
                    url = "~/Pages/CreateUser.aspx?" + QueryStringConstants.User_ID + "=" + userID
                        + "&" + QueryStringConstants.FROM_PAGE + "=" + Request.QueryString[QueryStringConstants.FROM_PAGE].ToString();

                    break;
            }

        }

        //if (SessionHelper.CurrentRoleID != (int)ARTEnums.UserRole.SYSTEM_ADMIN && SessionHelper.CurrentRoleID != (int)ARTEnums.UserRole.SKYSTEM_ADMIN)
        //{
        //    url = "CreateUser.aspx?" + QueryStringConstants.User_ID + "=" + Convert.ToInt32(Request.QueryString[QueryStringConstants.User_ID]); 
        //}

        if (!string.IsNullOrEmpty(url))
        {
            Response.Redirect(url);
        }
    }

    protected void btnDelete_OnClick(object sender, EventArgs e)
    {
        Helper.HideMessage(this);

        if (IsActiveUser)
        {
            pnlSaveData.Visible = true;
            btnSave.Visible = true;
            btnDelete.Visible = true;
            pnlAccountSearchContent.Visible = true;
            pnlUserSearchContent.Visible = true;


            List<AccountHdrInfo> oAccountHdrInfoCollection = (List<AccountHdrInfo>)Session[SessionConstants.SEARCH_ACCOUNT_ASSOCIATION];
            if (oAccountHdrInfoCollection != null && oAccountHdrInfoCollection.Count > 0)
            {
                btnAdd.Visible = true;
            }
            else
            {
                btnAdd.Visible = false;
            }
        }


        List<AccountHdrInfo> oUserAccountHdrInfoCollection = (List<AccountHdrInfo>)Session[SessionConstants.USER_ACCOUNT_ASSOCIATION];
        if (ucSkyStemARTAccountOwnershipGrid.Visible == true)
        {
            DeleteFromOwnershipGrid(oUserAccountHdrInfoCollection);
        }
    }

    protected void btnDeleteUser_OnClick(object sender, EventArgs e)
    {
        Helper.HideMessage(this);

        if (IsActiveUser)
        {
            pnlSaveData.Visible = true;
            btnSave.Visible = true;
            btnDelete.Visible = true;
            btnDeleteUser.Visible = true;
            pnlAccountSearchContent.Visible = true;
            pnlUserSearchContent.Visible = true;

            List<UserHdrInfo> oUserHdrInfoListSearch = (List<UserHdrInfo>)Session[SessionConstants.SEARCH_USER_ASSOCIATION_BY_USER_ROLE];
            if (oUserHdrInfoListSearch != null && oUserHdrInfoListSearch.Count > 0)
            {
                btnAddUserRole.Visible = true;
            }
            else
            {
                btnAddUserRole.Visible = false;
            }
        }

        List<UserHdrInfo> oUserHdrInfoListAdded = (List<UserHdrInfo>)Session[SessionConstants.USER_ASSOCIATION_BY_USER_ROLE];
        if (rgUserRoleSelected.Visible == true)
        {
            DeleteFromUserAssociationGrid(oUserHdrInfoListAdded);
        }
    }
    protected void ddlRole_SelectedIndexChanged(object sender, EventArgs e)
    {

        Session[SessionConstants.SEARCH_ACCOUNT_ASSOCIATION] = null;
        Session[SessionConstants.USER_ACCOUNT_ASSOCIATION] = null;
        Session[SessionConstants.SEARCH_USER_ASSOCIATION_BY_USER_ROLE] = null;
        Session[SessionConstants.USER_ASSOCIATION_BY_USER_ROLE] = null;

        roleID = Convert.ToInt16(ddlRole.SelectedValue);
        BindUserAccountGeographyObjectAssociatedGrid();
    }

    #endregion

    #region Validation Control Events
    #endregion

    #region Private Methods

    private void ShowHideSections()
    {
        bool bRoleEditAccess = !(SessionHelper.CurrentRoleID != (int)ARTEnums.UserRole.SYSTEM_ADMIN && SessionHelper.CurrentRoleID != (int)ARTEnums.UserRole.SKYSTEM_ADMIN);
        tblAssociationDisplay.Visible = optAllAccountNo.Checked;
        tblUserRoleAssociationDisplay.Visible = optAllAccountNo.Checked;
        pnlAccountSearchHeader.Visible = optAllAccountNo.Checked;
        pnlAccountSearchContent.Visible = optAllAccountNo.Checked;
        pnlUserSearchHeader.Visible = optAllAccountNo.Checked;
        pnlUserSearchContent.Visible = optAllAccountNo.Checked;

        List<AccountHdrInfo> oAccountHdrInfoListSearch = (List<AccountHdrInfo>)Session[SessionConstants.SEARCH_ACCOUNT_ASSOCIATION];
        List<AccountHdrInfo> oAccountHdrInfoListAdded = (List<AccountHdrInfo>)Session[SessionConstants.USER_ACCOUNT_ASSOCIATION];
        List<UserHdrInfo> oUserHdrInfoListSearch = (List<UserHdrInfo>)Session[SessionConstants.SEARCH_USER_ASSOCIATION_BY_USER_ROLE];
        List<UserHdrInfo> oUserHdrInfoListAdded = (List<UserHdrInfo>)Session[SessionConstants.USER_ASSOCIATION_BY_USER_ROLE];

        pnlSaveData.Visible = true;
        btnSave.Visible = bRoleEditAccess;

        btnDelete.Visible = false;
        ucSkyStemARTAccountOwnershipGrid.Visible = false;
        if (oAccountHdrInfoListAdded != null && oAccountHdrInfoListAdded.Count > 0)
        {
            btnDelete.Visible = bRoleEditAccess;
            ucSkyStemARTAccountOwnershipGrid.Visible = true;
        }

        btnAdd.Visible = false;
        ucSkyStemARTGrid.Visible = false;
        if (oAccountHdrInfoListSearch != null && oAccountHdrInfoListSearch.Count > 0)
        {
            btnAdd.Visible = bRoleEditAccess && IsActiveUser;
            ucSkyStemARTGrid.Visible = true;
        }

        btnDeleteUser.Visible = false;
        rgUserRoleSelected.Visible = false;
        if (oUserHdrInfoListAdded != null && oUserHdrInfoListAdded.Count > 0)
        {
            btnDeleteUser.Visible = bRoleEditAccess;
            rgUserRoleSelected.Visible = true;
        }
        btnAddUserRole.Visible = false;
        rgUserSearchList.Visible = false;
        if (oUserHdrInfoListSearch != null && oUserHdrInfoListSearch.Count > 0)
        {
            btnAddUserRole.Visible = bRoleEditAccess && IsActiveUser;
            rgUserSearchList.Visible = true;
        }

        if (IsActiveUser == false)
        {
            //btnDelete.Visible = false;
            btnSave.Visible = false;
            makeReadOnly();
        }
        ucSkyStemARTAccountOwnershipGrid.ShowSelectCheckBoxColum = bRoleEditAccess;
        rgUserRoleSelected.Columns.FindByUniqueName("CheckboxSelectColumn").Visible = bRoleEditAccess;
        if (!bRoleEditAccess)
        {
            makeReadOnly();
            ucSkyStemARTAccountOwnershipGrid.Grid.AllowExportToExcel = true;
            ucSkyStemARTAccountOwnershipGrid.Grid.AllowExportToPDF = true;
            ucSkyStemARTAccountOwnershipGrid.Grid.AllowPrint = false;
            ucSkyStemARTAccountOwnershipGrid.Grid.AllowPrintAll = false;
            pnlAccountSearchContent.Visible = false;
            pnlAccountSearchHeader.Visible = false;
            pnlUserSearchHeader.Visible = false;
            pnlUserSearchContent.Visible = false;
        }
    }
    private List<UserHdrInfo> ExpandUserHdrInfo(List<UserHdrInfo> userHdrInfoListToExpand)
    {
        List<UserHdrInfo> ouserHdrInfoListExpanded = null;
        List<RoleMstInfo> oRoleList = SessionHelper.GetAllRoles();
        if (userHdrInfoListToExpand != null && userHdrInfoListToExpand.Count > 0
            && oRoleList != null && oRoleList.Count > 0)
        {
            ouserHdrInfoListExpanded = new List<UserHdrInfo>();
            foreach (var item in userHdrInfoListToExpand)
            {
                if (item.UserRoleList != null && item.UserRoleList.Count > 0)
                {
                    foreach (var role in item.UserRoleList)
                    {
                        RoleMstInfo oRoleInfo = oRoleList.Find(T => T.RoleID == role.RoleID);
                        if (oRoleInfo != null && oRoleInfo.IsVisibleForAccountAssociationByUserRole.GetValueOrDefault())
                        {
                            ouserHdrInfoListExpanded.Add(new UserHdrInfo
                            {
                                ChildUserID = item.UserID,
                                FirstName = item.FirstName,
                                LastName = item.LastName,
                                LoginID = item.LoginID,
                                EmailID = item.EmailID,
                                ChildRoleID = oRoleInfo.RoleID,
                                ChildRole = oRoleInfo.Role
                            });
                        }
                    }
                }
            }
        }
        return ouserHdrInfoListExpanded;
    }
    private void InitailizeGrid()
    {
        ucSkyStemARTGrid.Grid_NeedDataSourceEventHandler += new UserControls_SkyStemARTGrid.Grid_NeedDataSource(ucSkyStemARTGrid_Grid_NeedDataSourceEventHandler);
        ucSkyStemARTAccountOwnershipGrid.Grid_NeedDataSourceEventHandler += new UserControls_SkyStemARTGrid.Grid_NeedDataSource(ucSkyStemARTAccountOwnershipGrid_Grid_NeedDataSourceEventHandler);

        ucSkyStemARTAccountOwnershipGrid.CompanyID = SessionHelper.CurrentCompanyID.Value;
        ucSkyStemARTAccountOwnershipGrid.Grid.AllowPaging = false;
        ucSkyStemARTAccountOwnershipGrid.Grid.AllowCustomPaging = false;

        ucSkyStemARTGrid.Grid.AllowPaging = false;
        ucSkyStemARTGrid.Grid.AllowCustomPaging = false;

        rgUserSearchList.ClientSettings.Selecting.AllowRowSelect = true;
        rgUserRoleSelected.ClientSettings.Selecting.AllowRowSelect = true;
    }

    private void PopulateUserOwnershipGridForAccount(List<AccountHdrInfo> oUserAccountHdrInfocollection)
    {
        Session[SessionConstants.USER_ACCOUNT_ASSOCIATION] = oUserAccountHdrInfocollection;
        pnlSaveData.Visible = true;
    }

    private void PopulateUserOwnershipGridForGeography(List<AccountHdrInfo> oUserAccountHdrInfocollection)
    {
        //////////////pnlOwnershipGrid.Visible = false;
        ucSkyStemARTAccountOwnershipGrid.Visible = true;

        Session[SessionConstants.USER_ACCOUNT_ASSOCIATION] = oUserAccountHdrInfocollection;

        ucSkyStemARTAccountOwnershipGrid.DataSource = Session[SessionConstants.USER_ACCOUNT_ASSOCIATION];
        ucSkyStemARTAccountOwnershipGrid.BindGrid();
        if (oUserAccountHdrInfocollection != null && oUserAccountHdrInfocollection.Count > 0)
        {
            Int16 maxKeySize = Convert.ToInt16(oUserAccountHdrInfocollection.Max(r => r.KeySize));
            RegionValue = Convert.ToInt32(oUserAccountHdrInfocollection.Min(r => r.KeySize));

            for (int index = maxKeySize + 1; index < ucSkyStemARTAccountOwnershipGrid.Grid.Columns.Count; index++)
            {
                // ucSkyStemARTAccountOwnershipGrid.Grid.Columns[index].Visible = false;
            }

        }
        pnlSaveData.Visible = true;
    }

    private void PopulateUserAssociationGrid(List<UserHdrInfo> oUserHdrInfoListAdded)
    {
        Session[SessionConstants.USER_ASSOCIATION_BY_USER_ROLE] = oUserHdrInfoListAdded;
        rgUserRoleSelected.DataSource = oUserHdrInfoListAdded;
        rgUserRoleSelected.DataBind();
    }

    private int GetRegionValue(AccountHdrInfo oCheckKeyValue)
    {
        if (oCheckKeyValue.Key3 == null || oCheckKeyValue.Key3 == "")
            return 2;
        else if (oCheckKeyValue.Key4 == null || oCheckKeyValue.Key4 == "")
            return 3;
        else if (oCheckKeyValue.Key5 == null || oCheckKeyValue.Key5 == "")
            return 4;
        else if (oCheckKeyValue.Key6 == null || oCheckKeyValue.Key6 == "")
            return 5;
        else if (oCheckKeyValue.Key7 == null || oCheckKeyValue.Key7 == "")
            return 6;
        else if (oCheckKeyValue.Key8 == null || oCheckKeyValue.Key8 == "")
            return 7;
        else
            return 8;
    }

    private void AddToUserRoleGrid(List<UserHdrInfo> oUserHdrInfoListUser, List<UserHdrInfo> oUserHdrInfoListSearch)
    {
        rgUserRoleSelected.Visible = true;
        foreach (GridDataItem item in rgUserSearchList.SelectedItems)
        {
            UserHdrInfo oUserHdrInfo;
            int? childUserID = (int?)item.GetDataKeyValue("ChildUserID");
            short? childRoleID = (short?)item.GetDataKeyValue("ChildRoleID");
            oUserHdrInfo = oUserHdrInfoListSearch.Find(r => r.ChildUserID == childUserID && r.ChildRoleID == childRoleID);

            if (oUserHdrInfoListUser.Count > 0)
            {
                bool bUserAlreadyAssigned = this.UserRoleAlreadyAssigned(oUserHdrInfoListUser, childUserID, childRoleID);
                if (bUserAlreadyAssigned == false)
                {
                    oUserHdrInfoListUser.Add(oUserHdrInfo);
                }
                oUserHdrInfoListSearch.Remove(oUserHdrInfo);
            }
            else
            {
                oUserHdrInfoListSearch.Remove(oUserHdrInfo);
                oUserHdrInfoListUser.Add(oUserHdrInfo);
            }
        }

        Session[SessionConstants.SEARCH_USER_ASSOCIATION_BY_USER_ROLE] = oUserHdrInfoListSearch;
        Session[SessionConstants.USER_ASSOCIATION_BY_USER_ROLE] = oUserHdrInfoListUser;

        rgUserSearchList.DataSource = Session[SessionConstants.SEARCH_USER_ASSOCIATION_BY_USER_ROLE];
        rgUserSearchList.DataBind();

        rgUserRoleSelected.DataSource = Session[SessionConstants.USER_ASSOCIATION_BY_USER_ROLE];
        rgUserRoleSelected.DataBind();
        ShowHideSections();
    }
    private void AddToOwnershipGrid(List<AccountHdrInfo> oUserAccountHdrInfoCollection, List<AccountHdrInfo> oAccountHdrInfoCollection)
    {
        ucSkyStemARTGrid.Visible = true;
        //////////////////ucSkyStemARTGridForAccounts.Visible = false;
        ucSkyStemARTAccountOwnershipGrid.Visible = true;
        /////////////////pnlOwnershipGrid.Visible = false;

        int collectionClearOnce = 0;

        //Added By Prafull on 14-Apr-2011
        int geoClassID = Convert.ToInt32(ucOrganizationalHierarchyDropdown.SelectedValue);

        //delete items from serach organistion hiearchy and add to userassocition
        foreach (GridDataItem item in ucSkyStemARTGrid.Grid.SelectedItems)
        {
            AccountHdrInfo oAccountHdrInfo;
            int keyvalue;
            /*
           tempapory comment

            if (geoClassID > 0)
            {

                keyvalue = int.Parse(item.GetDataKeyValue("GeographyObjectID").ToString());
                oAccountHdrInfo = oAccountHdrInfoCollection.Find(r => r.GeographyObjectID == keyvalue);
                oAccountHdrInfo.KeySize = Convert.ToInt32(ucOrganizationalHierarchyDropdown.SelectedValue);
                oAccountHdrInfo.IsUserGeographyObject = true;
                
            }
            else
            {
                keyvalue = int.Parse(item["ID"].Text);
                oAccountHdrInfo = oAccountHdrInfoCollection.Find(r => r.AccountID == keyvalue);
                oAccountHdrInfo.KeySize = Convert.ToInt32(ucOrganizationalHierarchyDropdown.SelectedValue);
                oAccountHdrInfo.IsUserGeographyObject = false; 

            }
           
             */

            if (!string.IsNullOrEmpty(this.txtFsCaption.Text) || !string.IsNullOrEmpty(this.txtAcName.Text) || (!string.IsNullOrEmpty(this.txtToAcNumber.Text) || !string.IsNullOrEmpty(this.txtAcNumber.Text)))
            {
                keyvalue = int.Parse(item["ID"].Text);
                oAccountHdrInfo = oAccountHdrInfoCollection.Find(r => r.AccountID == keyvalue);
                oAccountHdrInfo.KeySize = Convert.ToInt32(ucOrganizationalHierarchyDropdown.SelectedValue);
                oAccountHdrInfo.IsUserGeographyObject = false;

            }
            else if (geoClassID > 0)
            {

                keyvalue = int.Parse(item.GetDataKeyValue("GeographyObjectID").ToString());
                oAccountHdrInfo = oAccountHdrInfoCollection.Find(r => r.GeographyObjectID == keyvalue);
                oAccountHdrInfo.KeySize = Convert.ToInt32(ucOrganizationalHierarchyDropdown.SelectedValue);
                oAccountHdrInfo.IsUserGeographyObject = true;

            }
            else
            {
                keyvalue = int.Parse(item["ID"].Text);
                oAccountHdrInfo = oAccountHdrInfoCollection.Find(r => r.AccountID == keyvalue);
                oAccountHdrInfo.KeySize = Convert.ToInt32(ucOrganizationalHierarchyDropdown.SelectedValue);
                oAccountHdrInfo.IsUserGeographyObject = false;

            }


            ////////////////// AccountHdrInfo oAccountHdrInfoAdded = new AccountHdrInfo();
            if (oUserAccountHdrInfoCollection.Count > 0)
            {

                bool bAccountAlreadyAssigned = this.AccountAlreadyAssigned(oAccountHdrInfo, oUserAccountHdrInfoCollection, keyvalue);
                if (bAccountAlreadyAssigned == false)
                {
                    oAccountHdrInfoCollection.Remove(oAccountHdrInfo);
                    oUserAccountHdrInfoCollection.Add(oAccountHdrInfo);

                }

                //////if (RegionValue != Convert.ToInt32(ucOrganizationalHierarchyDropdown.SelectedValue))
                //////{
                //////    //Commented for testing by Prafull on 07-Apr-2011
                //////    //if (collectionClearOnce == 0)
                //////        //oUserAccountHdrInfoCollection.Clear();
                //////    oAccountHdrInfoCollection.Remove(oAccountHdrInfo);
                //////    oUserAccountHdrInfoCollection.Add(oAccountHdrInfo);
                //////}
                //////else
                //////{

                //////    if (oUserAccountHdrInfoCollection.Contains(oAccountHdrInfo))
                //////    {
                //////        //todo throw exception
                //////    }
                //////    else
                //////    {
                //////        oAccountHdrInfoCollection.Remove(oAccountHdrInfo);
                //////        oUserAccountHdrInfoCollection.Add(oAccountHdrInfo);
                //////    }
                //////}


            }
            else
            {
                oAccountHdrInfoCollection.Remove(oAccountHdrInfo);
                oUserAccountHdrInfoCollection.Add(oAccountHdrInfo);
            }
            collectionClearOnce = collectionClearOnce + 1;
        }

        Session[SessionConstants.SEARCH_ACCOUNT_ASSOCIATION] = oAccountHdrInfoCollection;
        Session[SessionConstants.USER_ACCOUNT_ASSOCIATION] = oUserAccountHdrInfoCollection;

        ucSkyStemARTGrid.ShowSelectCheckBoxColum = true;
        ucSkyStemARTGrid.CompanyID = SessionHelper.CurrentCompanyID.Value;
        ucSkyStemARTGrid.DataSource = Session[SessionConstants.SEARCH_ACCOUNT_ASSOCIATION];
        ucSkyStemARTGrid.BindGrid();

        for (int index = (Convert.ToInt32(ucOrganizationalHierarchyDropdown.SelectedValue) + 1); index < ucSkyStemARTAccountOwnershipGrid.Grid.Columns.Count; index++)
        {
            ///////////////ucSkyStemARTAccountOwnershipGrid.Grid.Columns[index].Visible = false;
            ///////////////ucSkyStemARTGrid.Grid.Columns[index].Visible = false;
        }

        ucSkyStemARTAccountOwnershipGrid.ShowSelectCheckBoxColum = true;
        ucSkyStemARTAccountOwnershipGrid.CompanyID = SessionHelper.CurrentCompanyID.Value;
        ucSkyStemARTAccountOwnershipGrid.DataSource = Session[SessionConstants.USER_ACCOUNT_ASSOCIATION];
        ucSkyStemARTAccountOwnershipGrid.BindGrid();
        ShowHideSections();
    }

    private void DeleteFromOwnershipGrid(List<AccountHdrInfo> oUserAccountHdrInfoCollection)
    {
        //List<long> accountIDCollection = new List<long>();
        List<UserAccountInfo> oUserAccountInfoCollection = new List<UserAccountInfo>();
        List<UserGeographyObjectInfo> oUserGeographyObjectInfoCollection = new List<UserGeographyObjectInfo>();
        IUser oUserClient = RemotingHelper.GetUserObject();


        foreach (GridDataItem item in ucSkyStemARTAccountOwnershipGrid.Grid.SelectedItems)
        {

            UserGeographyObjectInfo oUserGeographyObjectInfo = new UserGeographyObjectInfo();
            UserAccountInfo oUserAccountInfo = new UserAccountInfo();
            AccountHdrInfo oAccountHdrInfo = new AccountHdrInfo();
            int keyvalue;
            if (item["ID"].Text != "")
            {
                keyvalue = int.Parse(item["ID"].Text);
                oAccountHdrInfo = oUserAccountHdrInfoCollection.Find(r => r.AccountID == keyvalue);
                oUserAccountInfo.AccountID = oAccountHdrInfo.AccountID;
                oUserAccountInfo.RoleID = roleID;
                oUserAccountInfo.UserID = userID;
                oUserAccountInfo.IsActive = false;
                oUserAccountInfoCollection.Add(oUserAccountInfo);
            }
            else
            {
                keyvalue = int.Parse(item.GetDataKeyValue("GeographyObjectID").ToString());
                oAccountHdrInfo = oUserAccountHdrInfoCollection.Find(r => r.GeographyObjectID == keyvalue);
                oUserGeographyObjectInfo.GeographyObjectID = oAccountHdrInfo.GeographyObjectID;
                oUserGeographyObjectInfo.UserID = userID;
                oUserGeographyObjectInfo.RoleID = roleID;
                oUserGeographyObjectInfo.KeySize = oAccountHdrInfo.KeySize;
                oUserGeographyObjectInfo.IsActive = false;
                oUserGeographyObjectInfoCollection.Add(oUserGeographyObjectInfo);
            }

            if (oUserAccountHdrInfoCollection != null && oUserAccountHdrInfoCollection.Count > 0)
            {
                oUserAccountHdrInfoCollection.Remove(oAccountHdrInfo);
            }
        }

        //***********************Delete from Database and rebind the Grid
        bool bDeleted;

        bDeleted = oUserClient.DeleteUserOWnershipAccountAndGeographyObjectHdr(oUserGeographyObjectInfoCollection, oUserAccountInfoCollection, Helper.GetAppUserInfo());


        Session[SessionConstants.USER_ACCOUNT_ASSOCIATION] = oUserAccountHdrInfoCollection;
        ucSkyStemARTAccountOwnershipGrid.CompanyID = SessionHelper.CurrentCompanyID.Value;
        ucSkyStemARTAccountOwnershipGrid.DataSource = Session[SessionConstants.USER_ACCOUNT_ASSOCIATION];
        ucSkyStemARTAccountOwnershipGrid.BindGrid();

        ShowHideSections();
    }

    private void DeleteAllFromOwnershipGrid(List<AccountHdrInfo> oUserAccountHdrInfoCollection)
    {
        //List<long> accountIDCollection = new List<long>();
        List<UserAccountInfo> oUserAccountInfoCollection = new List<UserAccountInfo>();
        List<UserGeographyObjectInfo> oUserGeographyObjectInfoCollection = new List<UserGeographyObjectInfo>();
        IUser oUserClient = RemotingHelper.GetUserObject();


        foreach (GridDataItem item in ucSkyStemARTAccountOwnershipGrid.Grid.Items)
        {

            UserGeographyObjectInfo oUserGeographyObjectInfo = new UserGeographyObjectInfo();
            UserAccountInfo oUserAccountInfo = new UserAccountInfo();
            AccountHdrInfo oAccountHdrInfo = new AccountHdrInfo();
            int keyvalue;
            if (item["ID"].Text != "")
            {
                keyvalue = int.Parse(item["ID"].Text);
                oAccountHdrInfo = oUserAccountHdrInfoCollection.Find(r => r.AccountID == keyvalue);
                oUserAccountInfo.AccountID = oAccountHdrInfo.AccountID;
                oUserAccountInfo.RoleID = roleID;
                oUserAccountInfo.UserID = userID;
                oUserAccountInfo.IsActive = false;
                oUserAccountInfoCollection.Add(oUserAccountInfo);
            }
            else
            {
                keyvalue = int.Parse(item.GetDataKeyValue("GeographyObjectID").ToString());
                oAccountHdrInfo = oUserAccountHdrInfoCollection.Find(r => r.GeographyObjectID == keyvalue);
                oUserGeographyObjectInfo.GeographyObjectID = oAccountHdrInfo.GeographyObjectID;
                oUserGeographyObjectInfo.UserID = userID;
                oUserGeographyObjectInfo.RoleID = roleID;
                oUserGeographyObjectInfo.KeySize = oAccountHdrInfo.KeySize;
                oUserGeographyObjectInfo.IsActive = false;
                oUserGeographyObjectInfoCollection.Add(oUserGeographyObjectInfo);
            }

            if (oUserAccountHdrInfoCollection != null && oUserAccountHdrInfoCollection.Count > 0)
            {
                oUserAccountHdrInfoCollection.Remove(oAccountHdrInfo);
            }
        }

        //***********************Delete from Database and rebind the Grid
        bool bDeleted;

        bDeleted = oUserClient.DeleteUserOWnershipAccountAndGeographyObjectHdr(oUserGeographyObjectInfoCollection, oUserAccountInfoCollection, Helper.GetAppUserInfo());


        Session[SessionConstants.USER_ACCOUNT_ASSOCIATION] = oUserAccountHdrInfoCollection;
        ucSkyStemARTAccountOwnershipGrid.CompanyID = SessionHelper.CurrentCompanyID.Value;
        ucSkyStemARTAccountOwnershipGrid.DataSource = Session[SessionConstants.USER_ACCOUNT_ASSOCIATION];
        ucSkyStemARTAccountOwnershipGrid.BindGrid();

        ShowHideSections();
    }

    private void DeleteFromUserAssociationGrid(List<UserHdrInfo> oUserHdrInfoListAdded)
    {
        IUser oUserClient = RemotingHelper.GetUserObject();
        foreach (GridDataItem item in rgUserRoleSelected.SelectedItems)
        {
            int? childUserID = (int?)item.GetDataKeyValue("ChildUserID");
            short? childRoleID = (short?)item.GetDataKeyValue("ChildRoleID");
            if (oUserHdrInfoListAdded != null && oUserHdrInfoListAdded.Count > 0)
            {
                UserHdrInfo oUserHdrInfo = oUserHdrInfoListAdded.Find(T => T.ChildUserID == childUserID && T.ChildRoleID == childRoleID);
                oUserHdrInfoListAdded.Remove(oUserHdrInfo);
            }
        }

        //***********************Delete from Database and rebind the Grid
        oUserClient.SaveUserAssociationByUserRole(oUserHdrInfoListAdded, userID, roleID, Helper.GetAppUserInfo());
        Session[SessionConstants.USER_ASSOCIATION_BY_USER_ROLE] = oUserHdrInfoListAdded;
        rgUserRoleSelected.DataSource = Session[SessionConstants.USER_ASSOCIATION_BY_USER_ROLE];
        rgUserRoleSelected.DataBind();

        ShowHideSections();
    }

    private void BindRoleDropdown()
    {
        List<RoleMstInfo> oUserRoleInfoCollection = null;
        IUser oUserClient;
        oUserClient = RemotingHelper.GetUserObject();
        oUserRoleInfoCollection = oUserClient.GetUserRole(userID, Helper.GetAppUserInfo());
        ddlRole.DataSource = oUserRoleInfoCollection.Where(p => (p.RoleID == (short)ARTEnums.UserRole.ACCOUNT_MANAGER
                            || p.RoleID == (short)ARTEnums.UserRole.FINANCIAL_MANAGER
                            || p.RoleID == (short)ARTEnums.UserRole.CONTROLLER
                            || p.RoleID == (short)ARTEnums.UserRole.EXECUTIVE
                            || p.RoleID == (short)ARTEnums.UserRole.AUDIT
                            || p.RoleID == (short)ARTEnums.UserRole.BUSINESS_ADMIN
                            || p.RoleID == (short)ARTEnums.UserRole.TASK_OWNER));
        ddlRole.DataTextField = "Role";
        ddlRole.DataValueField = "RoleID";
        ddlRole.DataBind();
    }

    private void BindUserSearchRoleDropdown()
    {
        ListControlHelper.BindRoleDropDownListForAccountAssociation(ddlRoleUserSearch);
    }
    private bool AccountAlreadyAssigned(AccountHdrInfo oAccountHdrInfo, List<AccountHdrInfo> oUserAccountHdrInfoCollection, int keyvalue)
    {
        bool bAccountAlreadyAssigned = false;
        AccountHdrInfo oExistingUserAccountInfo = null;
        if (oAccountHdrInfo.IsUserGeographyObject == true)
        {
            oExistingUserAccountInfo = oUserAccountHdrInfoCollection.Find(r => (r.GeographyObjectID == keyvalue && r.AccountID == null));

        }
        else
        {
            oExistingUserAccountInfo = oUserAccountHdrInfoCollection.Find(r => r.AccountID == keyvalue);
        }

        if (oExistingUserAccountInfo != null)
        {
            bAccountAlreadyAssigned = true;

        }

        return bAccountAlreadyAssigned;

    }

    private bool UserRoleAlreadyAssigned(List<UserHdrInfo> oUserHdrInfoAdded, int? childUserID, short? childRoleID)
    {
        bool bUserAlreadyAssigned = false;
        UserHdrInfo oExistingUserInfo = null;
        if (oUserHdrInfoAdded != null && oUserHdrInfoAdded.Count > 0)
        {
            oExistingUserInfo = oUserHdrInfoAdded.Find(r => r.ChildUserID == childUserID && r.ChildRoleID == childRoleID);
        }

        if (oExistingUserInfo != null)
        {
            bUserAlreadyAssigned = true;
        }
        return bUserAlreadyAssigned;
    }
    private void BindUserAccountGeographyObjectAssociatedGrid()
    {

        IAccount oAccount = RemotingHelper.GetAccountObject();
        List<AccountHdrInfo> oUserAccountHdrInfocollection = oAccount.SelectOrganisationHiearachyByUserIDRoleID(userID, roleID, Helper.GetAppUserInfo());

        LanguageHelper.TranslateLabelsAccountHdr(oUserAccountHdrInfocollection);

        if (oUserAccountHdrInfocollection.Count > 0)
        {
            PopulateUserOwnershipGridForGeography(oUserAccountHdrInfocollection);
        }
        else
        {
            // hide the controls if no data
            Session[SessionConstants.USER_ACCOUNT_ASSOCIATION] = null;
            List<AccountHdrInfo> objcollection = new List<AccountHdrInfo>();
            ucSkyStemARTAccountOwnershipGrid.CompanyID = SessionHelper.CurrentCompanyID.Value;
            ucSkyStemARTAccountOwnershipGrid.DataSource = objcollection;
            ucSkyStemARTAccountOwnershipGrid.BindGrid();

            //btnSave.Visible = false;
            //btnDelete.Visible = false;
        }
        IUser oUser = RemotingHelper.GetUserObject();
        List<UserHdrInfo> oUserHdrInfoListAdded = oUser.SelectUserAssociationByUserRole(userID, roleID, Helper.GetAppUserInfo());

        if (oUserHdrInfoListAdded.Count > 0)
        {
            List<RoleMstInfo> roleList = SessionHelper.GetAllRoles();
            if (roleList != null && roleList.Count > 0)
            {
                oUserHdrInfoListAdded.ForEach(T =>
                {
                    RoleMstInfo role = roleList.Find(R => R.RoleID == T.ChildRoleID);
                    T.ChildRole = role.Role;
                });
            }
            PopulateUserAssociationGrid(oUserHdrInfoListAdded);
        }
        else
        {
            // hide the controls if no data
            Session[SessionConstants.USER_ASSOCIATION_BY_USER_ROLE] = null;
            rgUserRoleSelected.DataSource = oUserHdrInfoListAdded;
            rgUserRoleSelected.DataBind();

            //btnSave.Visible = false;
            //btnDeleteUser.Visible = false;
        }

        bool bAllAccount = oUser.SelectUserAssociationAllAccount(userID, roleID, Helper.GetAppUserInfo());
        optAllAccountYes.Checked = bAllAccount;
        optAllAccountNo.Checked = !bAllAccount;
        ShowHideSections();
    }

    void SetAttributeForDeleteButton()
    {
        btnDelete.Attributes.Add("onclick", "return ConfirmDeleteUserAccountAssociation('" + LanguageUtil.GetValue(1934) + "');");
        btnDeleteUser.Attributes.Add("onclick", "return ConfirmDeleteUserAccountAssociation('" + LanguageUtil.GetValue(1934) + "');");
        //optAllAccountYes.InputAttributes.Add("onclick", "return ShowHideAssociation();");
        //optAllAccountNo.InputAttributes.Add("onclick", "return ShowHideAssociation();");
    }
    void SetValidationMessages()
    {
        cvAllAccounts.ErrorMessage = Helper.GetErrorMessage(WebEnums.FieldType.MandatoryField, lblAllAccounts.LabelID);
    }


    #endregion

    #region Other Methods
    protected void makeReadOnly()
    {
        pnlAccountSearchContent.Visible = false;
        pnlUserSearchContent.Visible = false;
        btnSave.Visible = false;
        btnDelete.Visible = false;
        btnDeleteUser.Visible = false;
    }
    protected object ucSkyStemARTGrid_Grid_NeedDataSourceEventHandler(int count)
    {
        return Session[SessionConstants.SEARCH_ACCOUNT_ASSOCIATION];
    }

    protected object ucSkyStemARTAccountOwnershipGrid_Grid_NeedDataSourceEventHandler(int count)
    {
        return Session[SessionConstants.USER_ACCOUNT_ASSOCIATION];
    }

    /// <summary>
    /// This method is used to auto populate FS Caption text box based on the basis of 
    /// the prefix text typed in the text box
    /// </summary>
    /// <param name="prefixText">The text which was typed in the text box</param>
    /// <param name="count">Number of results to be returned</param>
    /// <returns>List of FS Caption</returns>
    [System.Web.Services.WebMethod()]
    [System.Web.Script.Services.ScriptMethod()]
    public static string[] AutoCompleteFSCaption(string prefixText, int count)
    {
        string[] oFSCaptionCollection = null;
        try
        {
            if (SessionHelper.CurrentCompanyID.HasValue)
            {
                int companyId = SessionHelper.CurrentCompanyID.Value;
                IFSCaption oFSCaption = RemotingHelper.GetFSCaptioneObject();
                oFSCaptionCollection = oFSCaption.SelectFSCaptionByCompanyIDAndPrefixText(companyId, prefixText, count
                    , SessionHelper.GetUserLanguage(), SessionHelper.GetBusinessEntityID(), AppSettingHelper.GetDefaultLanguageID(), Helper.GetAppUserInfo());

                if (oFSCaptionCollection == null || oFSCaptionCollection.Length == 0)
                {
                    oFSCaptionCollection = new string[] { "No Records found" };
                }
            }
        }
        catch (ARTException ex)
        {
            Helper.ShowErrorMessage(null, ex);
            throw ex;
        }
        catch (Exception ex)
        {
            Helper.ShowErrorMessage(null, ex);
            throw ex;
        }

        return oFSCaptionCollection;
    }

    public override string GetMenuKey()
    {
        return "AccountProfileAttributeUpdate";
    }
    #endregion

}
