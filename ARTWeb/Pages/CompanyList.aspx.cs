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
using SkyStem.ART.Web.Utility;
using SkyStem.ART.Web.Classes;
using SkyStem.ART.Client.Model;
using System.Collections.Generic;
using SkyStem.Library.Controls.WebControls;
using SkyStem.ART.Web.Data;
using SkyStem.ART.Client.IServices;
using SkyStem.Language.LanguageUtility;
using Telerik.Web.UI;
using SkyStem.Library.Controls.TelerikWebControls.Data;
using SkyStem.Language.LanguageUtility.Classes;
using SkyStem.ART.Client.Exception;
using SkyStem.ART.Client.Data;

public partial class Pages_CompanyList : PageBase
{
    #region Variables & Constants
    bool isExportPDF;
    bool isExportExcel;
    #endregion

    #region Properties
    #endregion

    #region Delegates & Events
    #endregion

    #region Page Events
    protected void Page_Load(object sender, EventArgs e)
    {
        Helper.SetPageTitle(this, 1230);

        string path = LanguageUtil.GetValue(1230);
        Helper.SetBreadcrumbs(this, path);

        if (!Page.IsPostBack)
        {
            isExportPDF = false;
            isExportExcel = false;
            ListControlHelper.BindCompanyStatusDropDown(ddlFTPStatus);
            ListControlHelper.BindCompanyStatusDropDown(ddlCompanyStatus);
            ListControlHelper.BindActivationStatusHistoryDropDown(DDLActHist);
        }
        DDLActHist.Enabled = cbActHist.Checked;
    }
    #endregion

    #region Grid Events

    protected void rgCompanyList_ItemCreated(object sender, GridItemEventArgs e)
    {
        GridHelper.SetStylesForExportGrid(e, isExportPDF, isExportExcel);
    }
    protected void rgCompanyList_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {
        try
        {
            if (e.Item.ItemType == Telerik.Web.UI.GridItemType.Item ||
                    e.Item.ItemType == Telerik.Web.UI.GridItemType.AlternatingItem)
            {
                CompanyHdrInfo oCompanyHdrInfo = (CompanyHdrInfo)e.Item.DataItem;

                decimal? dataStorageCapacity;
                decimal? currentUsage;

                DataImportHelper.GetCompanyDataStorageCapacityAndCurrentUsage(oCompanyHdrInfo.CompanyID.Value, out dataStorageCapacity, out currentUsage);
                oCompanyHdrInfo.DataStorageCapacity = dataStorageCapacity;
                oCompanyHdrInfo.CurrentUsage = currentUsage;

                // Check for Active Company 
                if (!oCompanyHdrInfo.IsActive.Value)
                {
                    e.Item.CssClass = "InactiveCompany";
                }

                // Check for subscription information
                ExImage imgSubscriptionExpireMessage = (ExImage)e.Item.FindControl("imgSubscriptionExpireMessage");
                ExImage imgSubscriptionWarningMessage = (ExImage)e.Item.FindControl("imgSubscriptionWarningMessage");
                ExHyperLink hlCompanyName = (ExHyperLink)e.Item.FindControl("hlCompanyName");
                ExHyperLink hlDisplayName = (ExHyperLink)e.Item.FindControl("hlDisplayName");
                ExHyperLink hlCompanyAlias = (ExHyperLink)e.Item.FindControl("hlCompanyAlias");
                ExHyperLink hlAddress = (ExHyperLink)e.Item.FindControl("hlAddress");
                ExHyperLink hlSubscriptionPeriod = (ExHyperLink)e.Item.FindControl("hlSubscriptionPeriod");
                ExHyperLink hlUsers = (ExHyperLink)e.Item.FindControl("hlUsers");
                ExHyperLink hlCapacity = (ExHyperLink)e.Item.FindControl("hlCapacity");
                ExLinkButton lnkbtnFTPActivationDate = (ExLinkButton)e.Item.FindControl("lnkbtnFTPActivationDate");
                ExLinkButton lnkbtnFTPActivationHistroy = (ExLinkButton)e.Item.FindControl("lnkbtnFTPActivationHistroy");
                ExLinkButton lnkbtnFTPStatus = (ExLinkButton)e.Item.FindControl("lnkbtnFTPStatus");

                imgSubscriptionExpireMessage.Visible = false;
                imgSubscriptionWarningMessage.Visible = false;

                // If Today's Date is higher than End Date, already expired
                if (DateTime.Now.Date > oCompanyHdrInfo.SubscriptionEndDate)
                {
                    imgSubscriptionExpireMessage.Visible = true;
                }
                else if (DateTime.Now.Date.AddDays(30) >= oCompanyHdrInfo.SubscriptionEndDate)
                {
                    imgSubscriptionWarningMessage.Visible = true;
                }

                hlCompanyName.Text = "&nbsp;" + Helper.GetDisplayStringValue(oCompanyHdrInfo.CompanyName);
                MultilingualAttributeInfo oMultilingualAttributeInfo = LanguageHelper.GetMultilingualAttributeInfo(oCompanyHdrInfo.CompanyID, null);
                if (oCompanyHdrInfo.DisplayNameLabelID.HasValue)
                    hlDisplayName.Text = "&nbsp;" + LanguageUtil.GetValue(oCompanyHdrInfo.DisplayNameLabelID.Value, oMultilingualAttributeInfo);
                else
                    hlDisplayName.Text = "&nbsp;" + oCompanyHdrInfo.DisplayName;
                hlCompanyAlias.Text = "&nbsp;" + oCompanyHdrInfo.CompanyAlias;
                hlAddress.Text = Helper.GetFullAddress(oCompanyHdrInfo.Address);
                hlSubscriptionPeriod.Text = Helper.GetDisplayDateRange(oCompanyHdrInfo.SubscriptionStartDate, oCompanyHdrInfo.SubscriptionEndDate);

                hlUsers.Text = "&nbsp;" + Helper.GetDisplayIntegerValue(oCompanyHdrInfo.ActualNoOfUsers) + " / " + Helper.GetDisplayIntegerValue(oCompanyHdrInfo.NoOfLicensedUsers);
                // TODO: 5 will come from DB in future
                if (oCompanyHdrInfo.NoOfLicensedUsers - oCompanyHdrInfo.ActualNoOfUsers <= 5)
                {
                    // means Users abt to cross limit
                    hlUsers.CssClass = "WarningLabel";
                }

                hlCapacity.Text = "&nbsp;" + Helper.GetDisplayDecimalValue(oCompanyHdrInfo.CurrentUsage) + " / " + Helper.GetDisplayDecimalValue(oCompanyHdrInfo.DataStorageCapacity);
                // TODO: 100 will come from DB in future
                if (oCompanyHdrInfo.DataStorageCapacity - oCompanyHdrInfo.CurrentUsage <= 100)
                {
                    // means Users abt to cross limit
                    hlCapacity.CssClass = "WarningLabel";
                }

                lnkbtnFTPActivationDate.Text = Helper.GetDisplayDateTime(oCompanyHdrInfo.CompanyStatusDate);

                if (oCompanyHdrInfo.CompanyStatusID.GetValueOrDefault() == (short)ARTEnums.ActivationStatus.Activated)
                    lnkbtnFTPActivationHistroy.Text = LanguageUtil.GetValue((int)ARTEnums.ActivationStatusLabelID.Activated);
                else if (oCompanyHdrInfo.CompanyStatusID.GetValueOrDefault() == (short)ARTEnums.ActivationStatus.Deactivated)
                    lnkbtnFTPActivationHistroy.Text = LanguageUtil.GetValue((int)ARTEnums.ActivationStatusLabelID.Deactivated);
                else
                    lnkbtnFTPActivationHistroy.Text = WebConstants.HYPHEN;

                if (oCompanyHdrInfo.IsFTPEnabled == true)
                {
                    lnkbtnFTPStatus.Text = LanguageUtil.GetValue(WebConstants.LABEL_ID_YES);
                }
                else if (oCompanyHdrInfo.IsFTPEnabled == false)
                {
                    lnkbtnFTPStatus.Text = LanguageUtil.GetValue(WebConstants.LABEL_ID_NO);
                }
                else
                {
                    lnkbtnFTPActivationHistroy.Text = WebConstants.HYPHEN;
                    lnkbtnFTPStatus.Text = WebConstants.HYPHEN;
                }

                string url = GetHyperlinkForEditCompany(oCompanyHdrInfo);
                hlCompanyName.NavigateUrl = url;
                hlDisplayName.NavigateUrl = url;
                hlAddress.NavigateUrl = url;
                hlSubscriptionPeriod.NavigateUrl = url;
                hlUsers.NavigateUrl = url;
                hlCapacity.NavigateUrl = url;
            }
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
    protected void rgCompanyList_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    {
        try
        {
            List<CompanyHdrInfo> oCompanyHdrInfoCollection = null; // SessionHelper.GetAllCompanies();
            oCompanyHdrInfoCollection = GetCompanyList();
            rgCompanyList.MasterTableView.DataSource = oCompanyHdrInfoCollection;
            GridHelper.SortDataSource(rgCompanyList.MasterTableView);
        }
        catch (Exception ex)
        {
            Helper.ShowErrorMessage(this, ex);
        }
    }

    protected void rgCompanyList_OnItemCommand(object sender, GridCommandEventArgs e)
    {
        if (e.CommandName == TelerikConstants.GridExportToPDFCommandName)
        {
            isExportPDF = true;
            rgCompanyList.MasterTableView.Columns.FindByUniqueName("IconColumn").Visible = false;
            GridHelper.ExportGridToPDF(rgCompanyList, 1230);

        }
        if (e.CommandName == TelerikConstants.GridExportToExcelCommandName)
        {
            isExportExcel = true;
            rgCompanyList.MasterTableView.Columns.FindByUniqueName("IconColumn").Visible = false;
            GridHelper.ExportGridToExcel(rgCompanyList, 1230);
        }
        if (e.CommandName == "DrilldownToUser")
        {
            GridDataItem oGridDataItem = (GridDataItem)e.Item;
            int? CompanyID = (int?)oGridDataItem.GetDataKeyValue("CompanyID");
            MasterPageBase oMasterPageBase = (MasterPageBase)this.Master;
            oMasterPageBase.ReloadCompanies(CompanyID);
            string Url = GetHyperlinkForUserSearch(CompanyID);
            if (!string.IsNullOrEmpty(Url))
            {
                //HttpContext.Current.Response.Redirect(Url);
                SessionHelper.RedirectToUrl(Url);
                return;
            }
            //lnkbtnFTPActivationDate.NavigateUrl = FTPurl;
            //lnkbtnFTPActivationHistroy.NavigateUrl = FTPurl;
            //lnkbtnFTPStatus.NavigateUrl = FTPurl;
        }
    }
    protected void rgCompanyList_SortCommand(object source, GridSortCommandEventArgs e)
    {
        GridHelper.HandleSortCommand(e);
        rgCompanyList.Rebind();

    }
    #endregion

    #region Other Events
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindCompanyList();
    }
    #endregion

    #region Validation Control Events
    #endregion

    #region Private Methods
    private string GetHyperlinkForEditCompany(CompanyHdrInfo oCompanyHdrInfo)
    {
        return "CreateCompany.aspx?" + QueryStringConstants.COMPANY_ID + "=" + oCompanyHdrInfo.CompanyID
                + "&" + QueryStringConstants.FROM_PAGE + "=" + WebEnums.ARTPages.CompanyList.ToString("d");
    }
    private string GetHyperlinkForUserSearch(int? companyID)
    {
        if (companyID.HasValue)
            return "UserSearch.aspx?" + QueryStringConstants.COMPANY_ID + "=" + companyID.Value;
        else
            return null;
    }
    private List<CompanyHdrInfo> GetCompanyList()
    {
        bool? isActive = null;
        bool? isFTPActive = null;
        bool IsShowActivationHistory = cbActHist.Checked;
        short ActivationHistoryVal = Convert.ToInt16(DDLActHist.SelectedValue);
        string CompanyName = txtCompanyName.Text.Trim();
        string DisplayName = txtDisplayName.Text.Trim();

        if (ddlCompanyStatus.SelectedValue == Convert.ToString((short)ARTEnums.Status.Inactive))
            isActive = false;

        if (ddlCompanyStatus.SelectedValue == Convert.ToString((short)ARTEnums.Status.Active))
            isActive = true;

        if (ddlFTPStatus.SelectedValue == Convert.ToString((short)ARTEnums.Status.Inactive))
            isFTPActive = false;

        if (ddlFTPStatus.SelectedValue == Convert.ToString((short)ARTEnums.Status.Active))
            isFTPActive = true;

        List<CompanyHdrInfo> oCompanyHdrInfoList = null;// SessionHelper.GetAllCompanies();
        ICompany oCompany = RemotingHelper.GetCompanyObject();
        oCompanyHdrInfoList = oCompany.GetAllCompaniesList(CompanyName, DisplayName, isActive, isFTPActive, IsShowActivationHistory, ActivationHistoryVal, Helper.GetAppUserInfo());
        return oCompanyHdrInfoList;
    }

    private void BindCompanyList()
    {
        rgCompanyList.MasterTableView.DataSource = GetCompanyList();
        GridSortExpression oGridSortExpression = new GridSortExpression();
        oGridSortExpression.FieldName = "CompanyName";
        oGridSortExpression.SortOrder = GridSortOrder.Ascending;
        rgCompanyList.MasterTableView.SortExpressions.AddSortExpression(oGridSortExpression);
        rgCompanyList.Rebind();
    }
    #endregion

    #region Other Methods
    public override string GetMenuKey()
    {
        return "";
    }
    #endregion

}
