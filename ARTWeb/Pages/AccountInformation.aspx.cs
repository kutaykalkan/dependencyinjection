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
using SkyStem.ART.Web.Utility;
using SkyStem.ART.Client.IServices;
using SkyStem.ART.Client.Exception;
using SkyStem.ART.Web.Data;
using SkyStem.ART.Client.Model;
using System.Collections.Generic;
using SkyStem.Library.Controls.TelerikWebControls;
using SkyStem.Library.Controls.WebControls;
using Telerik.Web.UI;
using SkyStem.ART.Client.Data;
using SkyStem.Language.LanguageUtility;


public partial class Pages_AccountInformation : PageBaseRecPeriod
{

    #region Variables & Constants
    int? _companyID;
    private UserHdrInfo _UserHdrInfo = null;
    bool _IsReconcilableEnabled = true;
    #endregion

    #region Properties
    List<AccountReconciliationPeriodInfo> _AccountReconciliationPeriodInfoCollection = null;
    #endregion

    #region Page Events
    protected void Page_Init(object sender, EventArgs e)
    {
        MasterPageBase ompage = (MasterPageBase)this.Master;
        ompage.ReconciliationPeriodChangedEventHandler += new EventHandler(ompage_ReconciliationPeriodChangedEventHandler);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        Helper.RegisterPostBackToControls(this, ucSkyStemARTGrid.Grid);
        ucSkyStemARTGrid.Grid.PagerStyle.PagerTextFormat = "Change page: {4}";
        try
        {
            ucAccountSearchControl.SearchClickEventHandler += new UserControls_AccountSearchControl.ShowSearchResults(ucAccountSearchControl_SearchClickEventHandler);
            this._UserHdrInfo = SessionHelper.GetCurrentUser();
            ucAccountSearchControl.PnlMassAndBulk.Visible = false;
            ucAccountSearchControl.ParentPage = WebEnums.AccountPages.AccountInformation;
            ucAccountSearchControl.ShowMissing.Visible = false;
            ucAccountSearchControl.ShowDueDaysRow = true;
            ucSkyStemARTGrid.Grid_NeedDataSourceEventHandler += new UserControls_SkyStemARTGrid.Grid_NeedDataSource(ucSkyStemARTGrid_Grid_NeedDataSourceEventHandler);
            ucSkyStemARTGrid.Grid.EntityNameLabelID = 1071;

            _companyID = SessionHelper.CurrentCompanyID;
            Helper.SetPageTitle(this, 1553);
            ucSkyStemARTGrid.BasePageTitle = 1553;
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

                IAccount oAccountClient = RemotingHelper.GetAccountObject();
                this._AccountReconciliationPeriodInfoCollection = oAccountClient.SelectAccountRecPeriodByAccountID(Convert.ToInt32(oAccountHdrInfo.AccountID.Value), Helper.GetAppUserInfo());

                ExLabel lblAccountNumber = (ExLabel)e.Item.FindControl("lblAccountNumber");
                lblAccountNumber.Text = Helper.GetDisplayStringValue(oAccountHdrInfo.AccountNumber);
                ExLabel lblAccountName = (ExLabel)e.Item.FindControl("lblAccountName");
                lblAccountName.Text = Helper.GetDisplayStringValue(oAccountHdrInfo.AccountName);
                ExLabel lblNetAccount = (ExLabel)e.Item.FindControl("lblNetAccount");
                lblNetAccount.Text = Helper.GetDisplayStringValue(oAccountHdrInfo.NetAccount);
                ExLabel lblKeyAccount = (ExLabel)e.Item.FindControl("lblKeyAccount");
                lblKeyAccount.Text = Helper.GetDisplayStringValue(oAccountHdrInfo.KeyAccount);
                ExLabel lblZeroBalanceAccount = (ExLabel)e.Item.FindControl("lblZeroBalanceAccount");
                lblZeroBalanceAccount.Text = Helper.GetDisplayStringValue(oAccountHdrInfo.ZeroBalance);
                Helper.SetTextForLabel(e.Item, "lblCreationDate", Helper.GetDisplayDate(oAccountHdrInfo.CreationPeriodEndDate));

                if (this._IsReconcilableEnabled)
                {
                    ExLabel lblIsReconcilable = (ExLabel)e.Item.FindControl("lblIsReconcilable");
                    lblIsReconcilable.Text = Helper.GetDisplayStringValue(oAccountHdrInfo.Reconcilable);
                }
                GridColumn oGridColumnRecFrequency = ucSkyStemARTGrid.Grid.Columns.FindByUniqueNameSafe("RecFrequency");
                if (Helper.IsCapabilityActivatedForCurrentRecPeriod(ARTEnums.Capability.RiskRating))
                {
                    ExLabel lblRiskRating = (ExLabel)e.Item.FindControl("lblRiskRating");
                    lblRiskRating.Text = Helper.GetDisplayStringValue(oAccountHdrInfo.RiskRating);
                    oGridColumnRecFrequency.Visible = false;
                }
                else
                {
                    UserControls_PopupRecFrequencySelection ucRecFrequencySelection = (UserControls_PopupRecFrequencySelection)e.Item.FindControl("ucRecFrequencySelection");
                    HtmlInputText oRecPeriodContainer = (HtmlInputText)e.Item.FindControl("txtRecPeriodsContainer");

                    if (string.IsNullOrEmpty(oRecPeriodContainer.Value))
                    {
                        string[] recPeriodNumbers = (from accRecPeriod in this._AccountReconciliationPeriodInfoCollection
                                                     where accRecPeriod.AccountID == oAccountHdrInfo.AccountID
                                                     select accRecPeriod.ReconciliationPeriodID.ToString() + ";").ToArray();
                        Array.ForEach(recPeriodNumbers, rec => oRecPeriodContainer.Value += rec);
                    }
                    string url = Helper.GetUrlForRecFrequency(oAccountHdrInfo.AccountID, oRecPeriodContainer.ClientID, oRecPeriodContainer.Value, WebEnums.FormMode.ReadOnly);
                    ucRecFrequencySelection.URL = url;
                    oGridColumnRecFrequency.Visible = true;
                }

                ExLabel lblMateriality = (ExLabel)e.Item.FindControl("lblMateriality");
                lblMateriality.LabelID = oAccountHdrInfo.AccountMaterialityLabelID.Value;
                ExLabel lblGLBalance = (ExLabel)e.Item.FindControl("lblGLBalance");
                lblGLBalance.Text = Helper.GetDisplayReportingCurrencyValue(oAccountHdrInfo.AccountGLBalance);
                ExLabel lblPreparer = (ExLabel)e.Item.FindControl("lblPreparer");
                lblPreparer.Text = oAccountHdrInfo.PreparerFullName;
                ExLabel lblReviewer = (ExLabel)e.Item.FindControl("lblReviewer");
                lblReviewer.Text = oAccountHdrInfo.ReviewerFullName;
                ExLabel lblApprover = (ExLabel)e.Item.FindControl("lblApprover");
                lblApprover.Text = oAccountHdrInfo.ApproverFullName;
                ExLabel lblBackupPreparer = (ExLabel)e.Item.FindControl("lblBackupPreparer");
                lblBackupPreparer.Text = oAccountHdrInfo.BackupPreparerFullName;
                ExLabel lblBackupReviewer = (ExLabel)e.Item.FindControl("lblBackupReviewer");
                lblBackupReviewer.Text = oAccountHdrInfo.BackupReviewerFullName;
                ExLabel lblBackupApprover = (ExLabel)e.Item.FindControl("lblBackupApprover");
                lblBackupApprover.Text = oAccountHdrInfo.BackupApproverFullName;
                ExLabel lblPreparerDueDays = (ExLabel)e.Item.FindControl("lblPreparerDueDays");
                lblPreparerDueDays.Text = Helper.GetDisplayIntegerValue(oAccountHdrInfo.PreparerDueDays);
                ExLabel lblReviewerDueDays = (ExLabel)e.Item.FindControl("lblReviewerDueDays");
                lblReviewerDueDays.Text = Helper.GetDisplayIntegerValue(oAccountHdrInfo.ReviewerDueDays);
                ExLabel lblApproverDueDays = (ExLabel)e.Item.FindControl("lblApproverDueDays");
                lblApproverDueDays.Text = Helper.GetDisplayIntegerValue(oAccountHdrInfo.ApproverDueDays);

                ExLabel lblPreparerDueDate = (ExLabel)e.Item.FindControl("lblPreparerDueDate");
                lblPreparerDueDate.Text = Helper.GetDisplayDate(oAccountHdrInfo.PreparerDueDate);
                ExLabel lblReviewerDueDate = (ExLabel)e.Item.FindControl("lblReviewerDueDate");
                lblReviewerDueDate.Text = Helper.GetDisplayDate(oAccountHdrInfo.ReviewerDueDate);
                ExLabel lblApproverDueDate = (ExLabel)e.Item.FindControl("lblApproverDueDate");
                lblApproverDueDate.Text = Helper.GetDisplayDate(oAccountHdrInfo.ApproverDueDate);


                GridColumn oGridColumnReconciliationStatus = ucSkyStemARTGrid.Grid.Columns.FindByUniqueNameSafe("ReconciliationStatus");
                GridColumn oGridColumnCertificationStatus = ucSkyStemARTGrid.Grid.Columns.FindByUniqueNameSafe("CertificationStatus");
                if (CurrentRecProcessStatus.Value != WebEnums.RecPeriodStatus.NotStarted)
                {
                    oGridColumnReconciliationStatus.Visible = true;
                    oGridColumnCertificationStatus.Visible = true;
                    ExLabel lblReconciliationStatus = (ExLabel)e.Item.FindControl("lblReconciliationStatus");
                    ExLabel lblCertificationStatus = (ExLabel)e.Item.FindControl("lblCertificationStatus");

                    if (oAccountHdrInfo.ReconciliationStatusLabelID.HasValue)
                        lblReconciliationStatus.Text = LanguageUtil.GetValue(oAccountHdrInfo.ReconciliationStatusLabelID.Value);
                    else
                        lblReconciliationStatus.Text = WebConstants.HYPHEN;

                    if (oAccountHdrInfo.CertificationStatusLabelID.HasValue)
                        lblCertificationStatus.Text = LanguageUtil.GetValue(oAccountHdrInfo.CertificationStatusLabelID.Value);
                    else
                        lblCertificationStatus.Text = WebConstants.HYPHEN;
                }
                else
                {
                    oGridColumnReconciliationStatus.Visible = false;
                    oGridColumnCertificationStatus.Visible = false;
                }
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
            ucAccountSearchControl.ReloadControl();
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

            GridColumn oGridColumnRiskRating = ucSkyStemARTGrid.Grid.Columns.FindByUniqueNameSafe("RiskRating");
            if (oGridColumnRiskRating != null)
            {
                oGridColumnRiskRating.Visible = true;
            }
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
    #endregion

    #region Other Methods
    public override string GetMenuKey()
    {
        return "AccountInformation";
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
