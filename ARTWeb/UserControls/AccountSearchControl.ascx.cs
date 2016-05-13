using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using SkyStem.ART.Client.IServices;
using SkyStem.ART.Web.Utility;
using SkyStem.ART.Client.Model;
using SkyStem.ART.Web.Data;
using SkyStem.Language.LanguageUtility;
using SkyStem.Library.Controls.WebControls;
using SkyStem.ART.Client.Exception;
using SkyStem.ART.Client.Data;
using SkyStem.ART.Web.Classes;

public partial class UserControls_AccountSearchControl : System.Web.UI.UserControl
{
    #region Variables & Constants
    private int _companyID = SessionHelper.CurrentCompanyID.Value;
    int _chkBoxlabelID;
    bool _IsRiskRatingEnabled;
    bool _IsZeroBalanceEnabled;
    bool _IsKeyAccountEnabled;
    bool _IsDualReviewEnabled;
    bool _IsReconcilableEnabled;
    public bool _IsDueDateByAccountEnabled = false;
    bool _ExcludeNetAccount = false;
    WebEnums.AccountPages _ParentPage;
    private const string BTN_ADDMORE_ONCLICK_VALUE = "return addMore('{0}|{1}|{2}|{3}|{4}|{5}')";
    private const string TEXTBOX_ONBLUR_VALUE = "HideProgressBar('{0}')";
    private string SEARCHCOMMAND_IMAGE_INNERHTML;
    private const string WIDTH_FIVE_PIXCEL = "5px";
    private const string WIDTH_EIGHTY_PIXCEL = "80px";
    private const string WIDTH_FIFTYFIVE_PIXCEL = "55px";
    private const string WIDTH_FIFTEEN_PIXCEL = "15px";
    private const string RISKRATING_DATASOURCE_TEXT_NAME = "Name";
    private const string RISKRATING_DATASOURCE_VALUE_NAME = "RiskRatingID";
    #endregion

    #region Properties
    private List<CompanyCapabilityInfo> _CompanyCapabilityInfoCollection = null;

    /// <summary>
    /// check box show missing attributes to be hidden/shown by parent pages.
    /// </summary>
    public ExCheckBoxWithLabel ShowMissing
    {
        get
        {
            return this.chkShowMissing;
        }
        set
        {
            this.chkShowMissing = value;
        }
    }

    public bool ShowMissingBackupOwners
    {
        get
        {
            return this.chkShowMissingBackupOwners.Visible;
        }
        set
        {
            this.chkShowMissingBackupOwners.Visible = value;
        }
    }

    public int ShowMissingBackupOwnersLabelID
    {
        get
        {
            return this.chkShowMissingBackupOwners.LabelID;
        }
        set
        {
            this.chkShowMissingBackupOwners.LabelID = value;
        }
    }

    public bool ShowDueDaysRow
    {
        get
        {
            return this.trDueDays.Visible;
        }
        set
        {
            this.trDueDays.Visible = value;
        }
    }

    /// <summary>
    /// Exposes row which contains search button to be hidden/shown by parent pages
    /// </summary>
    public HtmlTableCell PnlSearch
    {
        get
        {
            return this.pnlSearch;
        }
        set
        {
            this.pnlSearch = value;
        }
    }

    /// <summary>
    /// Exposes row which contains mass and bull update button to be hidden/shown by parent pages
    /// </summary>
    public HtmlTableCell PnlMassAndBulk
    {
        get
        {
            return this.pnlSearchAndMassUpdate;
        }
        set
        {
            this.pnlSearchAndMassUpdate = value;
        }
    }

    public HtmlTableCell PnlSearchAndMail
    {
        get
        {
            return this.pnlSearchAndMail;
        }
        set
        {
            this.pnlSearchAndMail = value;
        }
    }

    public WebEnums.AccountPages ParentPage
    {
        set
        {
            this._ParentPage = value;
        }
    }
    public bool ExcludeNetAccount
    {
        get { return _ExcludeNetAccount; }
        set { _ExcludeNetAccount = value; }
    }
    bool _IsOnPopup = false;
    public bool IsOnPopup
    {
        get { return _IsOnPopup; }
        set { _IsOnPopup = value; }
    }

    bool _IsOnAccountOwnerShipPage = false;
    public bool IsOnAccountOwnerShipPage
    {
        get { return _IsOnAccountOwnerShipPage; }
        set { _IsOnAccountOwnerShipPage = value; }
    }

    #endregion

    #region Delegates & Events
    public delegate void ShowSearchResults(List<AccountHdrInfo> oAccountHdrInfoCollection);
    public delegate void SearchAndMail(AccountSearchCriteria oAccountSearchCriteria);
    public event ShowSearchResults SearchClickEventHandler;
    public event ShowSearchResults MassUpdateClickEventHandler;
    public event ShowSearchResults BulkUpdateClickEventHandler;
    public event SearchAndMail SearchAndMailClickEventHandler;
    #endregion

    #region Page Events

    /// <summary>
    /// Initializes controls/variable values on page load
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            SEARCHCOMMAND_IMAGE_INNERHTML = "<input type='image' src='" + ResolveClientUrlPath("~/App_Themes/SkyStemBlueBrown/Images/Delete.gif") + "' " +
                                           "alt='Delete' value='Delete' onclick='removeRow(this,\"{0}\",\"{1}\",\"{2}\",\"{3}\");'/>";
            string url = ResolveClientUrlPath("~/App_Themes/SkyStemBlueBrown/Images/Delete.gif");
            ApplyFeatureCapability();
            btnAddMore.Attributes.Add(WebConstants.ONCLICK, string.Format(BTN_ADDMORE_ONCLICK_VALUE,
                                                            ucGeography.FindControl("ddlGeography").ClientID, txtGeography.ClientID,
                                                            tblSearchCommand.ClientID, hdnOrganizationalHierarchy.ClientID,
                                                            hdnTableInnerHTML.ClientID, url));

            txtUsername.Attributes.Add(WebConstants.ONBLUR, string.Format(TEXTBOX_ONBLUR_VALUE, txtUsername.ClientID));
            txtFsCaption.Attributes.Add(WebConstants.ONBLUR, string.Format(TEXTBOX_ONBLUR_VALUE, txtFsCaption.ClientID));
            this._CompanyCapabilityInfoCollection = SessionHelper.GetCompanyCapabilityCollectionForCurrentRecPeriod();
            Page.SetFocus(txtGeography);
            SetCapabilityInfo();
            DisableControls();

            if (IsPostBack)
            {
                PopulateTableSearchCommandRows();
            }
        }
        catch (Exception ex)
        {
            if (IsOnPopup)
                PopupHelper.ShowErrorMessage((PopupPageBase)this.Page, ex);
            else
                Helper.ShowErrorMessage((PageBase)this.Page, ex);
        }
    }

    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        Helper.EnableDisableOrgHierarchyForNoKey(lblGeography, txtGeography, null, btnAddMore);
    }

    #endregion

    #region Grid Events
    #endregion

    #region Other Events

    /// <summary>
    /// Handles search button click event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            List<AccountHdrInfo> oAccountHdrInfoCollection = GetSearchResults();
            RaiseSearchClickEvent(oAccountHdrInfoCollection);
        }
        catch (ARTException ex)
        {
            Helper.ShowErrorMessage((PageBase)this.Parent, ex);
        }
        catch (Exception ex)
        {
            Helper.ShowErrorMessage((PageBase)this.Parent, ex);
        }
    }
    protected void btnSearchAndMail_Click(object sender, EventArgs e)
    {
        AccountSearchCriteria oAccountSearchCriteria = this.GetSearchCriteria();
        // Vinay: Search and email should return all records
        //oAccountSearchCriteria.Count = Convert.ToInt32(AppSettingHelper.GetAppSettingValue(AppSettingConstants.DEFAULT_CHUNK_SIZE));
        HttpContext.Current.Session.Add(SessionConstants.ACCOUNT_SEARCH_CRITERIA, oAccountSearchCriteria);

        RaiseSearchAndMailClickEvent(oAccountSearchCriteria);
    }
    /// <summary>
    /// Handles Mass Update button click event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public void btnSearchMassUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            if (IsOnAccountOwnerShipPage)
            {
                this._ParentPage = WebEnums.AccountPages.AccountOwnership;
                List<AccountHdrInfo> oAccountHdrInfoCollection = GetSearchResults();
                RaiseMassUpdateClickEvent(oAccountHdrInfoCollection);
            }
            else
            {
                this._ParentPage = WebEnums.AccountPages.AccountMassUpdate;

                List<AccountHdrInfo> oAccountHdrInfoCollection = GetSearchResults();
                int maxSize = Convert.ToInt32(AppSettingHelper.GetAppSettingValue(AppSettingConstants.MAX_RECORD_SIZE_FOR_NONPAGED_GRIDS));

                if (oAccountHdrInfoCollection.Count > maxSize)
                {
                    throw new ARTException(5000087);
                }

                //oAccountHdrInfoCollection = LanguageHelper.TranslateLabelsAccountHdr(oAccountHdrInfoCollection);
                RaiseMassUpdateClickEvent(oAccountHdrInfoCollection);
            }



        }
        catch (ARTException ex)
        {
            Helper.ShowErrorMessage((PageBase)this.Page, ex);
        }
        catch (Exception ex)
        {
            Helper.ShowErrorMessage((PageBase)this.Page, ex);
        }
    }

    /// <summary>
    /// Handles Bulk update button click event.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSearchBulkUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            if (IsOnAccountOwnerShipPage)
            {
                this._ParentPage = WebEnums.AccountPages.AccountOwnership;
                List<AccountHdrInfo> oAccountHdrInfoCollection = GetSearchResults();
                RaiseBulkUpdateClickEvent(oAccountHdrInfoCollection);
            }
            else
            {
                this._ParentPage = WebEnums.AccountPages.AccountBulkUpdate;
                List<AccountHdrInfo> oAccountHdrInfoCollection = GetSearchResults();
                int maxSize = Convert.ToInt32(AppSettingHelper.GetAppSettingValue(AppSettingConstants.MAX_RECORD_SIZE_FOR_NONPAGED_GRIDS));

                if (oAccountHdrInfoCollection.Count > maxSize)
                {
                    throw new ARTException(5000087);
                }

                oAccountHdrInfoCollection = LanguageHelper.TranslateLabelsAccountHdr(oAccountHdrInfoCollection);
                RaiseBulkUpdateClickEvent(oAccountHdrInfoCollection);
            }
        }
        catch (ARTException ex)
        {
            Helper.ShowErrorMessage((PageBase)this.Page, ex);
        }
        catch (Exception ex)
        {
            Helper.ShowErrorMessage((PageBase)this.Page, ex);
        }
    }

    #endregion

    #region Validation Control Events
    #endregion

    #region Private Methods
    private void SetCapabilityInfo()
    {

        this._IsReconcilableEnabled = true;//TODO: Needs to be dependent on Package selected

        foreach (CompanyCapabilityInfo oCompanyCapabilityInfo in this._CompanyCapabilityInfoCollection)
        {
            if (oCompanyCapabilityInfo.CapabilityID.HasValue)
            {
                ARTEnums.Capability oCapability = (ARTEnums.Capability)Enum.Parse(typeof(ARTEnums.Capability), oCompanyCapabilityInfo.CapabilityID.Value.ToString());

                switch (oCapability)
                {
                    case ARTEnums.Capability.RiskRating:
                        if (oCompanyCapabilityInfo.IsActivated.HasValue && oCompanyCapabilityInfo.IsActivated.Value)
                        {
                            this._IsRiskRatingEnabled = true;
                        }
                        break;

                    case ARTEnums.Capability.ZeroBalanceAccount:
                        if (oCompanyCapabilityInfo.IsActivated.HasValue && oCompanyCapabilityInfo.IsActivated.Value)
                        {
                            this._IsZeroBalanceEnabled = true;
                        }
                        break;
                    case ARTEnums.Capability.KeyAccount:
                        if (oCompanyCapabilityInfo.IsActivated.HasValue && oCompanyCapabilityInfo.IsActivated.Value)
                        {
                            this._IsKeyAccountEnabled = true;
                        }
                        break;

                    case ARTEnums.Capability.DualLevelReview:
                        if (oCompanyCapabilityInfo.IsActivated.HasValue && oCompanyCapabilityInfo.IsActivated.Value)
                        {
                            this._IsDualReviewEnabled = true;
                        }
                        break;
                    case ARTEnums.Capability.DueDateByAccount:
                        if (oCompanyCapabilityInfo.IsActivated.HasValue && oCompanyCapabilityInfo.IsActivated.Value)
                        {
                            this._IsDueDateByAccountEnabled = true;
                        }
                        break;
                }
            }
        }
    }

    private void DisableControls()
    {
        ddlRiskRating.DropDown.Enabled = true;

        lblRiskRating.Enabled = true;
        //optIsKeyAccountAll.Enabled = true;
        //optIsKeyAccountNo.Enabled = true;
        //optIsKeyAccountYes.Enabled = true;

        lblZeroBalanceAccount.Enabled = true;
        optZeroBalanceAccountAll.Enabled = true;
        optZeroBalanceAccountNo.Enabled = true;
        optZeroBalanceAccountYes.Enabled = true;

        if (!Helper.IsCapabilityActivatedForCurrentRecPeriod(ARTEnums.Capability.RiskRating))
        {
            lblRiskRating.Enabled = false;
            ddlRiskRating.DropDown.Enabled = false;
        }

        //if (!Helper.IsCapabilityActivatedForCurrentRecPeriod(ARTEnums.Capability.KeyAccount))
        //{
        //    optIsKeyAccountAll.Enabled = false;
        //    optIsKeyAccountNo.Enabled = false;
        //    optIsKeyAccountYes.Enabled = false;
        //}

        if (!Helper.IsCapabilityActivatedForCurrentRecPeriod(ARTEnums.Capability.ZeroBalanceAccount))
        {
            optZeroBalanceAccountAll.Enabled = false;
            optZeroBalanceAccountNo.Enabled = false;
            optZeroBalanceAccountYes.Enabled = false;
            lblZeroBalanceAccount.Enabled = false;
        }

        //Reconcilable
        this.optIsReconcilableYes.Enabled = true;
        this.optIsReconcilableNo.Enabled = true;
        this.optIsReconcilableAll.Enabled = true;
        this.lblIsReconcilable.Enabled = true;

        bool isDueDateByAccountActivated = Helper.IsCapabilityActivatedForCurrentRecPeriod(ARTEnums.Capability.DueDateByAccount);
        lblDueDays.Enabled = isDueDateByAccountActivated;
        lblFromDueDays.Enabled = isDueDateByAccountActivated;
        lblToDueDays.Enabled = isDueDateByAccountActivated;
        txtFromDueDays.Enabled = isDueDateByAccountActivated;
        txtToDueDays.Enabled = isDueDateByAccountActivated;
        if (!isDueDateByAccountActivated)
        {
            txtFromDueDays.Text = string.Empty;
            txtToDueDays.Text = string.Empty;
        }
    }

    /// <summary>
    /// Populates table whic stores organizational hierarchy key and value on post back
    /// </summary>
    private void PopulateTableSearchCommandRows()
    {
        string[] tblSearchCommandInnerText = hdnTableInnerHTML.Value.Split('/');

        for (int index = 0; index < tblSearchCommandInnerText.Length; )
        {
            string str = tblSearchCommandInnerText[index++];
            if (!string.IsNullOrEmpty(str))
            {
                HtmlTableRow row = new HtmlTableRow();
                row.Style.Add(WebConstants.VERTICALALIGN, WebConstants.VERTIVALALIGN_TOP);
                HtmlTableCell cell = new HtmlTableCell();
                cell.InnerText = str;
                cell.Style.Add(WebConstants.WIDTH, WIDTH_FIVE_PIXCEL);
                cell.Style.Add(WebConstants.VISIBILITY, WebConstants.VISIBILITY_HIDDEN);
                row.Cells.Add(cell);

                cell = new HtmlTableCell();
                str = tblSearchCommandInnerText[index++];
                cell.InnerText = str;
                cell.Style.Add(WebConstants.WIDTH, WIDTH_EIGHTY_PIXCEL);
                row.Cells.Add(cell);

                cell = new HtmlTableCell();
                str = tblSearchCommandInnerText[index++];
                cell.InnerText = str;
                cell.Style.Add(WebConstants.WIDTH, WIDTH_EIGHTY_PIXCEL);
                row.Cells.Add(cell);

                cell = new HtmlTableCell();
                str = tblSearchCommandInnerText[index++];
                cell.InnerText = str;
                cell.Style.Add(WebConstants.WIDTH, WIDTH_FIFTYFIVE_PIXCEL);
                row.Cells.Add(cell);

                cell = new HtmlTableCell();
                cell.Style.Add(WebConstants.WIDTH, WIDTH_FIFTEEN_PIXCEL);
                cell.InnerHtml = string.Format(SEARCHCOMMAND_IMAGE_INNERHTML, tblSearchCommand.ClientID, ucGeography.FindControl("ddlGeography").ClientID,
                                            hdnOrganizationalHierarchy.ClientID, hdnTableInnerHTML.ClientID);
                row.Cells.Add(cell);
                tblSearchCommand.Rows.Add(row);
            }
        }

    }

    /// <summary>
    /// Searches database to get the matching results
    /// </summary>
    /// <returns>List of Accounts which matches the search criteria</returns>
    private List<AccountHdrInfo> GetSearchResults()
    {
        List<AccountHdrInfo> oAccountHdrInfoCollection = null;

        try
        {
            //Populate search information
            int defaultItemCount = Helper.GetDefaultChunkSize(10);
            AccountSearchCriteria oAccountSearchCriteria = this.GetSearchCriteria();
            //  oAccountSearchCriteria.Count = Convert.ToInt32(AppSettingHelper.GetAppSettingValue(AppSettingConstants.DEFAULT_CHUNK_SIZE));
            oAccountSearchCriteria.Count = defaultItemCount;
            HttpContext.Current.Session.Add(SessionConstants.ACCOUNT_SEARCH_CRITERIA, oAccountSearchCriteria);
            IAccount oAccountClient = RemotingHelper.GetAccountObject();
            oAccountHdrInfoCollection = oAccountClient.SearchAccount(oAccountSearchCriteria, Helper.GetAppUserInfo());
            oAccountHdrInfoCollection = LanguageHelper.TranslateLabelsAccountHdr(oAccountHdrInfoCollection);

            if (HttpContext.Current.Session[SessionConstants.SEARCH_RESULTS_ACCOUNT] == null)
            {
                HttpContext.Current.Session.Add(SessionConstants.SEARCH_RESULTS_ACCOUNT, oAccountHdrInfoCollection);
            }
            else
            {
                HttpContext.Current.Session[SessionConstants.SEARCH_RESULTS_ACCOUNT] = oAccountHdrInfoCollection;
            }
        }
        catch (ARTException ex)
        {
            Helper.ShowErrorMessage((PageBase)this.Page, ex);
        }
        catch (Exception ex)
        {
            Helper.ShowErrorMessage((PageBase)this.Page, ex);
        }

        return oAccountHdrInfoCollection;
    }

    /// <summary>
    /// Reads Search command table and fills organizational hierarchy key and value
    /// in Account search criteria object
    /// </summary>
    /// <param name="oAccountSearchCriteria">object which contains all search parameters.</param>
    private void GetOrganizationalKeysAndValues(AccountSearchCriteria oAccountSearchCriteria)
    {
        HtmlTableRowCollection searchCommandRowCollection = tblSearchCommand.Rows;

        if (searchCommandRowCollection != null && searchCommandRowCollection.Count > 0)
        {
            for (int index = 0; index < searchCommandRowCollection.Count; index++)
            {
                HtmlTableRow oRow = searchCommandRowCollection[index];
                int geoClassID = Convert.ToInt32(oRow.Cells[0].InnerHtml);
                string geoValue = oRow.Cells[3].InnerText;

                switch (index)
                {
                    case 0:
                        oAccountSearchCriteria.Key2 = geoClassID;
                        oAccountSearchCriteria.Key2Value = geoValue;
                        break;
                    case 1:
                        oAccountSearchCriteria.Key3 = geoClassID;
                        oAccountSearchCriteria.Key3Value = geoValue;
                        break;
                    case 2:
                        oAccountSearchCriteria.Key4 = geoClassID;
                        oAccountSearchCriteria.Key4Value = geoValue;
                        break;
                    case 3:
                        oAccountSearchCriteria.Key5 = geoClassID;
                        oAccountSearchCriteria.Key5Value = geoValue;
                        break;
                    case 4:
                        oAccountSearchCriteria.Key6 = geoClassID;
                        oAccountSearchCriteria.Key6Value = geoValue;
                        break;
                    case 5:
                        oAccountSearchCriteria.Key7 = geoClassID;
                        oAccountSearchCriteria.Key7Value = geoValue;
                        break;
                    case 6:
                        oAccountSearchCriteria.Key8 = geoClassID;
                        oAccountSearchCriteria.Key8Value = geoValue;
                        break;
                    case 7:
                        oAccountSearchCriteria.Key9 = geoClassID;
                        oAccountSearchCriteria.Key9Value = geoValue;
                        break;
                }//end switch
            }//end for
        }//end if
        if (ucGeography.SelectedValue != WebConstants.SELECT_ONE
               && !string.IsNullOrEmpty(txtGeography.Text))
        {
            int geoClassIDVal = Convert.ToInt32(ucGeography.SelectedValue);
            switch ((WebEnums.GeographyClass)geoClassIDVal)
            {
                case WebEnums.GeographyClass.Key2:
                    oAccountSearchCriteria.Key2 = geoClassIDVal;
                    oAccountSearchCriteria.Key2Value = txtGeography.Text;
                    break;
                case WebEnums.GeographyClass.Key3:
                    oAccountSearchCriteria.Key3 = geoClassIDVal;
                    oAccountSearchCriteria.Key3Value = txtGeography.Text;
                    break;
                case WebEnums.GeographyClass.Key4:
                    oAccountSearchCriteria.Key4 = geoClassIDVal;
                    oAccountSearchCriteria.Key4Value = txtGeography.Text;
                    break;
                case WebEnums.GeographyClass.Key5:
                    oAccountSearchCriteria.Key5 = geoClassIDVal;
                    oAccountSearchCriteria.Key5Value = txtGeography.Text;
                    break;
                case WebEnums.GeographyClass.Key6:
                    oAccountSearchCriteria.Key6 = geoClassIDVal;
                    oAccountSearchCriteria.Key6Value = txtGeography.Text;
                    break;
                case WebEnums.GeographyClass.Key7:
                    oAccountSearchCriteria.Key7 = geoClassIDVal;
                    oAccountSearchCriteria.Key7Value = txtGeography.Text;
                    break;
                case WebEnums.GeographyClass.Key8:
                    oAccountSearchCriteria.Key8 = geoClassIDVal;
                    oAccountSearchCriteria.Key8Value = txtGeography.Text;
                    break;
                case WebEnums.GeographyClass.Key9:
                    oAccountSearchCriteria.Key9 = geoClassIDVal;
                    oAccountSearchCriteria.Key9Value = txtGeography.Text;
                    break;
            }//end switch
        }
    
    }

    private void RaiseSearchAndMailClickEvent(AccountSearchCriteria oAccountSearchCriteria)
    {
        if (SearchAndMailClickEventHandler != null)
        {
            SearchAndMailClickEventHandler(oAccountSearchCriteria);
        }
    }

    /// <summary>
    /// Fills search criteria in account search criteria object
    /// </summary>
    /// <returns>Object containg all searched criteria info</returns>
    private AccountSearchCriteria GetSearchCriteria()
    {
        AccountSearchCriteria oAccountSearchCriteria = new AccountSearchCriteria();
        try
        {
            //Get Organizational Keys and Values
            GetOrganizationalKeysAndValues(oAccountSearchCriteria);

            oAccountSearchCriteria.CompanyID = this._companyID;

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

            if (!string.IsNullOrEmpty(txtUsername.Text))
            {
                oAccountSearchCriteria.UserName = txtUsername.Text.Trim();
            }

            if (!string.IsNullOrEmpty(txtAcName.Text))
            {
                oAccountSearchCriteria.AccountName = txtAcName.Text.Trim();
            }

            if (this._IsKeyAccountEnabled)
            {
                if (optIsKeyAccountYes.Checked)
                {
                    oAccountSearchCriteria.IsKeyccount = true;
                }
                else if (optIsKeyAccountNo.Checked)
                {
                    oAccountSearchCriteria.IsKeyccount = false;
                }
            }

            if (this._IsZeroBalanceEnabled)
            {
                if (optZeroBalanceAccountYes.Checked)
                {
                    oAccountSearchCriteria.IsZeroBalanceAccount = true;
                }
                else if (optZeroBalanceAccountNo.Checked)
                {
                    oAccountSearchCriteria.IsZeroBalanceAccount = false;
                }
            }

            if (chkShowMissing.Checked)
            {
                oAccountSearchCriteria.IsShowOnlyAccountMissingAttributes = true;
            }
            else
            {
                oAccountSearchCriteria.IsShowOnlyAccountMissingAttributes = false;
            }

            if (this._IsRiskRatingEnabled)
            {
                oAccountSearchCriteria.RiskRatingID = Convert.ToInt32(ddlRiskRating.SelectedValue);
            }

            //Reconcilable
            if (this._IsReconcilableEnabled)
            {
                if (optIsReconcilableYes.Checked)
                {
                    oAccountSearchCriteria.IsReconcilable = true;
                }
                else if (optIsReconcilableNo.Checked)
                {
                    oAccountSearchCriteria.IsReconcilable = false;
                }
            }

            // Due Days
            txtFromDueDays.Text = txtFromDueDays.Text.Trim();
            oAccountSearchCriteria.FromDueDays = null;
            if (!string.IsNullOrEmpty(txtFromDueDays.Text))
            {
                int fromDueDays;
                if (Int32.TryParse(txtFromDueDays.Text, out fromDueDays))
                    oAccountSearchCriteria.FromDueDays = fromDueDays;
            }
            txtToDueDays.Text = txtToDueDays.Text.Trim();
            oAccountSearchCriteria.ToDueDays = null;
            if (!string.IsNullOrEmpty(txtToDueDays.Text))
            {
                int toDueDays;
                if (Int32.TryParse(txtToDueDays.Text, out toDueDays))
                    oAccountSearchCriteria.ToDueDays = toDueDays;
            }

            //Show Only Missing Backup Account Owners
            oAccountSearchCriteria.IsShowOnlyAccountMissingBackupOwners = this.chkShowMissingBackupOwners.Checked;

            oAccountSearchCriteria.ReconciliationPeriodID = SessionHelper.CurrentReconciliationPeriodID.Value;

            oAccountSearchCriteria.PageID = (int)this._ParentPage;

            oAccountSearchCriteria.IsDualReviewEnabled = this._IsDualReviewEnabled;
            oAccountSearchCriteria.IsKeyAccountEnabled = this._IsKeyAccountEnabled;
            oAccountSearchCriteria.IsRiskRatingEnabled = this._IsRiskRatingEnabled;
            oAccountSearchCriteria.IsZeroBalanceAccountEnabled = this._IsZeroBalanceEnabled;
            oAccountSearchCriteria.LCID = SessionHelper.GetUserLanguage();
            oAccountSearchCriteria.BusinessEntityID = SessionHelper.GetBusinessEntityID();
            oAccountSearchCriteria.DefaultLanguageID = AppSettingHelper.GetDefaultLanguageID();
            oAccountSearchCriteria.UserID = SessionHelper.CurrentUserID.Value;
            oAccountSearchCriteria.UserRoleID = SessionHelper.CurrentRoleID.Value;

            oAccountSearchCriteria.IsReconcilableEnabled = this._IsReconcilableEnabled;

            oAccountSearchCriteria.ExcludeNetAccount = this._ExcludeNetAccount;
        }
        catch (Exception ex)
        {
            Helper.ShowErrorMessage((PageBase)this.Page, ex);
        }

        return oAccountSearchCriteria;
    }


    #endregion

    #region Other Methods

    protected void ApplyFeatureCapability()
    {
        //Key Account
        WebEnums.FeatureCapabilityMode featureCapabilityMode = Helper.GetFeatureCapabilityMode(WebEnums.Feature.KeyAccount, ARTEnums.Capability.KeyAccount, SessionHelper.CurrentReconciliationPeriodID);
        if (featureCapabilityMode == WebEnums.FeatureCapabilityMode.Hidden)
        {
            trKeyAccount.Visible = false;
            //lblIsKeyAccount.Visible = false;
            //optIsKeyAccountYes.Visible = false;
            //optIsKeyAccountNo.Visible = false;
            //optIsKeyAccountAll.Visible = false;
        }
        else if (featureCapabilityMode == WebEnums.FeatureCapabilityMode.Disable)
        {
            trKeyAccount.Visible = true;
            lblIsKeyAccount.Enabled = false;
            optIsKeyAccountYes.Enabled = false;
            optIsKeyAccountNo.Enabled = false;
            optIsKeyAccountAll.Enabled = false;
        }
        else
        {
            trKeyAccount.Visible = true;
            lblIsKeyAccount.Enabled = true;
            optIsKeyAccountYes.Enabled = true;
            optIsKeyAccountNo.Enabled = true;
            optIsKeyAccountAll.Enabled = true;
        }
        if (!Helper.IsFeatureActivated(WebEnums.Feature.AccountOwnershipBackup, SessionHelper.CurrentReconciliationPeriodID))
        {
            chkShowMissingBackupOwners.Checked = false;
            chkShowMissingBackupOwners.Visible = false;
        }
        if (!Helper.IsFeatureActivated(WebEnums.Feature.DueDateByAccount, SessionHelper.CurrentReconciliationPeriodID))
            trDueDays.Visible = false;
    }

    /// <summary>
    /// Raises Search click event when search button is clicked
    /// </summary>
    /// <param name="oAccountHdrInfoCollection">List of account headers returned by the search</param>
    public void RaiseSearchClickEvent(List<AccountHdrInfo> oAccountHdrInfoCollection)
    {
        if (SearchClickEventHandler != null)
        {
            SearchClickEventHandler(oAccountHdrInfoCollection);
        }
    }

    /// <summary>
    /// Raises event when Mass update button is clicked
    /// </summary>
    /// <param name="oAccountHdrInfoCollection">List of account headers returned by the search</param>
    public void RaiseMassUpdateClickEvent(List<AccountHdrInfo> oAccountHdrInfoCollection)
    {
        if (MassUpdateClickEventHandler != null)
        {
            MassUpdateClickEventHandler(oAccountHdrInfoCollection);
        }
    }

    /// <summary>
    /// Raises event when Bulk update button is clicked
    /// </summary>
    /// <param name="oAccountHdrInfoCollection">List of account headers returned by the search</param>
    public void RaiseBulkUpdateClickEvent(List<AccountHdrInfo> oAccountHdrInfoCollection)
    {
        if (BulkUpdateClickEventHandler != null)
        {
            BulkUpdateClickEventHandler(oAccountHdrInfoCollection);
        }
    }

    public string ResolveClientUrlPath(string relativeUrl)
    {
        string url;
        url = Page.ResolveClientUrl(relativeUrl);

        return url;
    }
    public void ReloadControl()
    {
        ApplyFeatureCapability();
        DisableControls();
    }
    /// <summary>
    /// Property to set check box show missing attributes label Id.
    /// </summary>
    public int ChkBoxlabelID
    {
        set
        {
            _chkBoxlabelID = value;
            chkShowMissing.LabelID = _chkBoxlabelID;
        }
    }

    #endregion

}
