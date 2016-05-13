using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SkyStem.ART.Web.Utility;
using SkyStem.ART.Web.Data;
using SkyStem.ART.Client.Model;
using SkyStem.Language.LanguageUtility;
using Telerik.Web.UI;
using SkyStem.ART.Client.Model.Base;
using SkyStem.Library.Controls.WebControls;
using SkyStem.ART.Web.Classes;
using SkyStem.ART.Client.IServices;
using SkyStem.ART.Client.Exception;
using SkyStem.Library.Controls.TelerikWebControls;

public partial class Pages_AccountProfileUpdate : PageBaseRecPeriod
{

    #region Variables & Constants
    int? _companyID;
    #endregion

    #region Properties
    #endregion

    #region Delegates & Events
    #endregion

    #region Page Events
    protected void Page_Init(object sender, EventArgs e)
    {
        MasterPageBase ompage = (MasterPageBase)this.Master;
        ompage.ReconciliationPeriodChangedEventHandler += new EventHandler(ompage_ReconciliationPeriodChangedEventHandler);
    }
    /// <summary>
    /// Initializes controls value and variables on page load
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        Helper.RegisterPostBackToControls(this, ucSkyStemARTGrid.Grid);
        ucSkyStemARTGrid.Grid.PagerStyle.PagerTextFormat = "Change page: {4}";

        try
        {
            ucAccountSearchControl.SearchClickEventHandler += new UserControls_AccountSearchControl.ShowSearchResults(ucAccountSearchControl_SearchClickEventHandler);

            ucAccountSearchControl.PnlMassAndBulk.Visible = false;
            ucAccountSearchControl.ChkBoxlabelID = 1706;
            ucAccountSearchControl.ShowDueDaysRow = true;
            ucAccountSearchControl.ParentPage = WebEnums.AccountPages.AccountProfileUpdate;

            ucSkyStemARTGrid.Grid_NeedDataSourceEventHandler += new UserControls_SkyStemARTGrid.Grid_NeedDataSource(ucSkyStemARTGrid_Grid_NeedDataSourceEventHandler);
            ucSkyStemARTGrid.Grid.EntityNameLabelID = 1071;

            _companyID = SessionHelper.CurrentCompanyID;
            Helper.SetPageTitle(this, 1507);
            ucSkyStemARTGrid.BasePageTitle = 1507;
        }
        catch (Exception ex)
        {
            Helper.ShowErrorMessage(this, ex);
        }
    }
    #endregion

    #region Grid Events
    /// <summary>
    /// Handles rad grids item data bound event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ucSkyStemARTGrid_GridItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {
        try
        {
            if (e.Item.ItemType == Telerik.Web.UI.GridItemType.Item || e.Item.ItemType == Telerik.Web.UI.GridItemType.AlternatingItem)
            {
                AccountHdrInfo oAccountHdrInfo = (AccountHdrInfo)e.Item.DataItem;
                ExHyperLink hlAccountNumber = (ExHyperLink)e.Item.FindControl("hlAccountNumber");
                ExHyperLink hlAccountName = (ExHyperLink)e.Item.FindControl("hlAccountName");

                if (hlAccountNumber != null)
                {
                    hlAccountNumber.Text = oAccountHdrInfo.AccountNumber;
                }

                if (hlAccountName != null)
                {
                    hlAccountName.Text = Helper.GetDisplayStringValue(oAccountHdrInfo.AccountName);
                }
                string NetAccountID = null;
                if (oAccountHdrInfo.NetAccountID.HasValue)
                    NetAccountID = oAccountHdrInfo.NetAccountID.ToString();
                string url = GetHyperlinkForEditUser(oAccountHdrInfo.AccountID.ToString(), NetAccountID);

                hlAccountNumber.NavigateUrl = url;
                hlAccountName.NavigateUrl = url;
                Helper.SetHyperLinkForOrganizationalHierarchyColumns(url, e);
            }
        }
        catch (Exception ex)
        {
            Helper.ShowErrorMessage(this, ex);
        }
    }

    /// <summary>
    /// Handles user controls Need data source event
    /// </summary>
    /// <param name="count">Number of items needed to bind the grid</param>
    /// <returns>object</returns>
    protected object ucSkyStemARTGrid_Grid_NeedDataSourceEventHandler(int count)
    {
        List<AccountHdrInfo> oAccountHdrInfoCollection = null;
        try
        {

            oAccountHdrInfoCollection = (List<AccountHdrInfo>)HttpContext.Current.Session[SessionConstants.SEARCH_RESULTS_ACCOUNT];
            //int defaultItemCount = Convert.ToInt32(AppSettingHelper.GetAppSettingValue(AppSettingConstants.DEFAULT_CHUNK_SIZE));
            int defaultItemCount = Helper.GetDefaultChunkSize(ucSkyStemARTGrid.Grid.PageSize);

            if (oAccountHdrInfoCollection == null || oAccountHdrInfoCollection.Count <= count - defaultItemCount || count == 0)
            {
                AccountSearchCriteria oAccountSearchCriteria = (AccountSearchCriteria)HttpContext.Current.Session[SessionConstants.ACCOUNT_SEARCH_CRITERIA];

                if (ucSkyStemARTGrid.Grid.MasterTableView.SortExpressions != null && ucSkyStemARTGrid.Grid.MasterTableView.SortExpressions.Count > 0)
                {
                    GridHelper.GetSortExpressionAndDirection(oAccountSearchCriteria, ucSkyStemARTGrid.Grid.MasterTableView);
                }

                oAccountSearchCriteria.Count = count;
                IAccount oAccountClient = RemotingHelper.GetAccountObject();
                oAccountHdrInfoCollection = oAccountClient.SearchAccount(oAccountSearchCriteria, Helper.GetAppUserInfo());
                oAccountHdrInfoCollection = LanguageHelper.TranslateLabelsAccountHdr(oAccountHdrInfoCollection);
                HttpContext.Current.Session[SessionConstants.SEARCH_RESULTS_ACCOUNT] = oAccountHdrInfoCollection;
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

        return oAccountHdrInfoCollection;
    }
    #endregion

    #region Other Events
    public void ompage_ReconciliationPeriodChangedEventHandler(object sender, EventArgs e)
    {
        ucAccountSearchControl.ReloadControl();
        if (!Page.IsPostBack && Request.QueryString.Count > 0)
        {
            ucSkyStemARTGrid.CompanyID = this._companyID;

            List<AccountHdrInfo> oAccountHdrInfoCollection = (List<AccountHdrInfo>)HttpContext.Current.Session[SessionConstants.SEARCH_RESULTS_ACCOUNT];

            if (oAccountHdrInfoCollection.Count < 100)
                ucSkyStemARTGrid.Grid.VirtualItemCount = oAccountHdrInfoCollection.Count;
            ucSkyStemARTGrid.ShowSelectCheckBoxColum = false;
            ucSkyStemARTGrid.DataSource = oAccountHdrInfoCollection;
            ucSkyStemARTGrid.ShowFSCaptionAndAccountType();
            ucSkyStemARTGrid.BindGrid();
            ucSkyStemARTGrid.DataBind();
            pnlAccounts.Visible = true;
        }
        else
        {
            HidePanels();
        }
    }
    /// <summary>
    /// Handles user controls Search click event
    /// </summary>
    /// <param name="oAccountHdrInfoCollection">List of accounts</param>
    public void ucAccountSearchControl_SearchClickEventHandler(List<AccountHdrInfo> oAccountHdrInfoCollection)
    {
        try
        {
            if (oAccountHdrInfoCollection.Count < 100)
                ucSkyStemARTGrid.Grid.VirtualItemCount = oAccountHdrInfoCollection.Count;
            else
                ucSkyStemARTGrid.Grid.VirtualItemCount = oAccountHdrInfoCollection.Count + 1;
            ucSkyStemARTGrid.ShowSelectCheckBoxColum = false;
            ucSkyStemARTGrid.Grid.CurrentPageIndex = 0;
            ucSkyStemARTGrid.CompanyID = _companyID;
            ucSkyStemARTGrid.DataSource = oAccountHdrInfoCollection;
            ucSkyStemARTGrid.ShowFSCaptionAndAccountType();
            ucSkyStemARTGrid.BindGrid();
            ucSkyStemARTGrid.DataBind();
            pnlAccounts.Visible = true;
        }
        catch (Exception ex)
        {
            Helper.ShowErrorMessage(this, ex);
        }
    }
    #endregion

    #region Validation Control Events
    #endregion

    #region Private Methods
    private void HidePanels()
    {
        pnlAccounts.Visible = false;
    }
    /// <summary>
    /// Gets menu key
    /// </summary>
    /// <returns>Menu key</returns>
    private string GetHyperlinkForEditUser(string AccountID, string NetAccountID)
    {
        return "../Pages/AccountProfileAttributeUpdate.aspx" + "?" + QueryStringConstants.ACCOUNT_ID + "=" + AccountID + "&" + QueryStringConstants.NETACCOUNT_ID + " = " + NetAccountID;
    }
    #endregion

    #region Other Methods
    public override string GetMenuKey()
    {
        //return "AccountProfileBulkUpdate";
        //Bug#3774 - Harsh - 16th Dec 2009
        return "AccountProfile";
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
                IFSCaption oFSCaptionClient = RemotingHelper.GetFSCaptioneObject();
                oFSCaptionCollection = oFSCaptionClient.SelectFSCaptionByCompanyIDAndPrefixText(companyId, prefixText, count
                    , SessionHelper.GetUserLanguage(), SessionHelper.GetBusinessEntityID(), AppSettingHelper.GetDefaultLanguageID(), Helper.GetAppUserInfo());

                if (oFSCaptionCollection == null || oFSCaptionCollection.Length == 0)
                {
                    oFSCaptionCollection = new string[] { "No Records Found" };
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

    /// <summary>
    /// This method is used to auto populate User Name text box based on the basis of 
    /// the prefix text typed in the text box
    /// </summary>
    /// <param name="prefixText">The text which was typed in the text box</param>
    /// <param name="count">Number of results to be returned</param>
    /// <returns>List of User Names</returns>
    [System.Web.Services.WebMethod()]
    [System.Web.Script.Services.ScriptMethod()]
    public static string[] AutoCompleteUserName(string prefixText, int count)
    {
        string[] oUserNameCollection = null;

        try
        {
            if (SessionHelper.CurrentCompanyID.HasValue)
            {
                int companyId = SessionHelper.CurrentCompanyID.Value;
                IUser oUserClient = RemotingHelper.GetUserObject();
                oUserNameCollection = oUserClient.SelectActiveUserNamesByCompanyIdAndPrefixText(companyId, prefixText, count, Helper.GetAppUserInfo());

                if (oUserNameCollection == null || oUserNameCollection.Length == 0)
                {
                    oUserNameCollection = new string[] { "No Records Found" };
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

        return oUserNameCollection;
    }
    #endregion

}
