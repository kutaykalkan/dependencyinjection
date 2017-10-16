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
using SkyStem.Library.Controls.TelerikWebControls;
using SkyStem.Library.Controls.TelerikWebControls.Grid;
using Telerik.Web.UI;
using SkyStem.ART.Client.IServices;
using SkyStem.ART.Client.Model;
using System.Collections.Generic;
using SkyStem.Language.LanguageUtility;
using SkyStem.ART.Web.Utility;
using SkyStem.Library.Controls.WebControls;
using SkyStem.ART.Web.Data;
using SkyStem.ART.Web.UserControls;
using System.Text;
using SkyStem.ART.Client.Data;
using SkyStem.ART.Client.Exception;
using SkyStem.Library.Controls.TelerikWebControls.Data;


namespace SkyStem.ART.Web.UserControls
{

    public partial class UserControls_GLDataWriteOnOff : UserControlRecItemBase
    {

        ExImageButton ToggleControl;
        #region PrivateProperties
        static bool _isCapabilityMultiCurrencyAccount = false;
        private List<GLDataWriteOnOffInfo> _oGLDataWriteOnOffInfoCollection;
        private short _UserRole = 0;
        private short _RecPeriodStatus = 0;
        private bool? _IsExpandedValueForced = null;
        #endregion
        #region "public Properties"
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

        public bool? IsExpandedValueForced
        {
            get { return _IsExpandedValueForced; }
            set { _IsExpandedValueForced = value; }
        }
        public WebEnums.FormMode EditMode
        {
            get
            {
                return (WebEnums.FormMode)ViewState["EditMode"];
            }
            set
            {
                ViewState["EditMode"] = value;
            }
        }
        public bool IsMultiCurrencyActivated
        {
            get { return (bool)ViewState["IsMultiCurrencyActivated"]; }
            set { ViewState["IsMultiCurrencyActivated"] = value; }
        }
        public string DivClientId
        {
            get { return divMainContent.ClientID; }
        }
        public bool IsDataLoadCondition
        {
            get
            {
                if (AccountID != null && NetAccountID != null && GLDataID != null && RecCategoryTypeID != null && IsSRA != null && RecCategoryID != null)
                    return true;
                return false;
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
        private List<GLDataWriteOnOffInfo> GetGLDataWriteOnOffInfoCollection
        {
            get
            {
                return this._oGLDataWriteOnOffInfoCollection;
            }
            set
            {
                this._oGLDataWriteOnOffInfoCollection = value;
            }
        }
        #endregion
        #region "Page Events"
        protected void Page_Load(object sender, EventArgs e)
        {
            ucGLDataWriteOnOffGrid.GridItemDataBound += new GridItemEventHandler(ucGLDataWriteOnOffGrid_GridItemDataBound);
            ucGLDataWriteOnOffGrid.GridCommand += new GridCommandEventHandler(ucGLDataWriteOnOffGrid_GridCommand);
            ucCloseGLDataWriteOnOffGrid.GridItemDataBound += new GridItemEventHandler(ucCloseGLDataWriteOnOffGrid_GridItemDataBound);
            if (!IsPostBack)
            {
                IsMultiCurrencyActivated = Helper.IsCapabilityActivatedForCurrentRecPeriod(ARTEnums.Capability.MultiCurrency);
                ucGLDataWriteOnOffGrid.IsMultiCurrencyActivated = IsMultiCurrencyActivated;
                ucCloseGLDataWriteOnOffGrid.IsMultiCurrencyActivated = IsMultiCurrencyActivated;
                PopulateGrids();
            }

            SetControlState();
            if (EntityNameLabelID.HasValue)
            {
                ucGLDataWriteOnOffGrid.BasePageTitleLabelID = EntityNameLabelID.Value;
                ucCloseGLDataWriteOnOffGrid.BasePageTitleLabelID = EntityNameLabelID.Value;
            }
            //********* Refresh Rec Period status *******************************************************
            //if (IsPostBack)
            //{
            //    RecHelper.ReloadRecPeriodsOnMasterPage(this);
            //}
        }

        void ucCloseGLDataWriteOnOffGrid_GridItemDataBound(object sender, GridItemEventArgs e)
        {
            ShowHideCurrencyColumn(ucCloseGLDataWriteOnOffGrid.Grid);
            SetHeader(e);
            if (e.Item.ItemType == GridItemType.Header)
            {
                if (ucCloseGLDataWriteOnOffGrid.IsOnPage)
                    SessionHelper.ShowGridFilterIcon((PageBase)this.Page, ucCloseGLDataWriteOnOffGrid.Grid, e, ucCloseGLDataWriteOnOffGrid.Grid.ClientID);
                else
                    SessionHelper.ShowGridFilterIcon((PopupPageBase)this.Page, ucCloseGLDataWriteOnOffGrid.Grid, e, ucCloseGLDataWriteOnOffGrid.Grid.ClientID);
            }
            if (e.Item.ItemType == Telerik.Web.UI.GridItemType.Item || e.Item.ItemType == Telerik.Web.UI.GridItemType.AlternatingItem)
            {
                SetMatchSetRefNumberUrlForGLDataRecItem(e, AccountID, NetAccountID, GLDataID);
                WebEnums.FormMode EditModeReadOnly = WebEnums.FormMode.ReadOnly;
                GridDataItem oGridDataItem = e.Item as GridDataItem;
                DataRow dr = ((DataRowView)oGridDataItem.DataItem).Row;
                ExHyperLink hlShowItemInputForm = (ExHyperLink)e.Item.FindControl("hlShowItemInputForm");
                Helper.SetImageURLForViewVersusEdit(EditModeReadOnly, hlShowItemInputForm);
                bool IsForwardedItem;
                bool.TryParse(dr["IsForwardedItem"].ToString(), out    IsForwardedItem);
                long GLDataWriteOnOffID;
                if (long.TryParse(dr["GLDataWriteOnOffID"].ToString(), out GLDataWriteOnOffID))
                {
                    hlShowItemInputForm.NavigateUrl = PopupHelper.GetJavascriptParameterListForEditRecItem(
                             GLDataWriteOnOffID,
                             "OpenRadWindowForHyperlinkWithName",
                             "EditItemWriteOffOn.aspx",
                             EditModeReadOnly.ToString(),
                             IsForwardedItem,
                             this.AccountID,
                             this.GLDataID,
                             this.RecCategoryTypeID,
                             this.NetAccountID,
                             this.IsSRA,
                             RecCategoryID,
                             hdIsRefreshData.ClientID,
                             this.CurrentBCCY);
                }
            }
        }
        public static void SetMatchSetRefNumberUrlForGLDataRecItem(GridItemEventArgs e, long? AccountID, long? NetAccountID, long? GLDataID)
        {
            GridDataItem oGridDataItem = e.Item as GridDataItem;
            DataRow dr = ((DataRowView)oGridDataItem.DataItem).Row;
            ExHyperLink hlMatchSetRefNumber = (ExHyperLink)e.Item.FindControl("hlMatchSetRefNumber");
            if (!string.IsNullOrEmpty(Convert.ToString(dr["MatchSetRefNumber"])))
            {
                hlMatchSetRefNumber.Text = Helper.GetDisplayStringValue(dr["MatchSetRefNumber"].ToString());
            }

            if (!(bool)dr["IsForwardedItem"])
            {
                if (!string.IsNullOrEmpty(Convert.ToString(dr["MatchSetRefNumber"])))
                {
                    hlMatchSetRefNumber.Enabled = true;
                    string queryString = "?" + QueryStringConstants.ACCOUNT_ID + "=" + AccountID
                              + "&" + QueryStringConstants.NETACCOUNT_ID + "=" + NetAccountID
                             + "&" + QueryStringConstants.GLDATA_ID + "=" + GLDataID
                             + "&" + QueryStringConstants.MATCH_SET_ID + "=" + Convert.ToString(dr["MatchSetID"])
                             + "&" + QueryStringConstants.MATCHSET_SUBSET_COMBINATION_ID + "=" + Convert.ToString(dr["MatchSetSubSetCombinationID"])
                              + "&" + QueryStringConstants.MATCHING_TYPE_ID + "=" + (int)ARTEnums.MatchingType.AccountMatching;
                    hlMatchSetRefNumber.NavigateUrl = URLConstants.URL_MATCHING_VIEW_MATCH_SET + queryString;
                }
                else
                {
                    hlMatchSetRefNumber.Enabled = false;
                }
            }
            else
            {
                hlMatchSetRefNumber.Enabled = false;
            }
        }
        void ucGLDataWriteOnOffGrid_GridCommand(object sender, GridCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Delete")
                {
                    DataTable dtGLDataParams = new DataTable();
                    dtGLDataParams.Columns.Add("ID");
                    DataRow drGLData = dtGLDataParams.NewRow();
                    drGLData["ID"] = Convert.ToInt64(e.CommandArgument);
                    dtGLDataParams.Rows.Add(drGLData);
                    long? GLDataWriteOnOffID = Convert.ToInt64(e.CommandArgument);
                    IGLDataWriteOnOff oGLDataWriteOnOff = RemotingHelper.GetGLDataWriteOnOffObject();
                    oGLDataWriteOnOff.DeleteGLDataWriteOnOff(GLDataWriteOnOffID, this.GLDataID, this.RecCategoryTypeID, SessionHelper.CurrentUserLoginID, DateTime.Now, dtGLDataParams, Helper.GetAppUserInfo());
                    RecHelper.RefreshRecForm(this);
                }
            }
            catch (Exception ex)
            {
                Helper.LogException(ex);
            }
        }
        private void SetHeader(GridItemEventArgs e)
        {
            if (e.Item.ItemType == GridItemType.Header)
            {
                Control oControlAmountReportingCurrency = new Control();
                Control oControlAmountBaseCurrency = new Control();
                oControlAmountReportingCurrency = (e.Item as GridHeaderItem)["AmountReportingCurrency"].Controls[0];
                oControlAmountBaseCurrency = (e.Item as GridHeaderItem)["AmountBaseCurrency"].Controls[0];
                if (oControlAmountReportingCurrency is LinkButton)
                {
                    ((LinkButton)oControlAmountReportingCurrency).Text = Helper.GetLabelIDValue(1674) + " (" + SessionHelper.ReportingCurrencyCode + ")";
                }
                else
                {
                    if (oControlAmountReportingCurrency is LiteralControl)
                    {
                        ((LiteralControl)oControlAmountReportingCurrency).Text = Helper.GetLabelIDValue(1674) + " (" + SessionHelper.ReportingCurrencyCode + ")";
                    }
                }
                //*********** In Case Of Net Account there will be no Base Currency Code 
                //if (NetAccountID > 0)
                if (String.IsNullOrEmpty(this.CurrentBCCY))
                {
                    if (oControlAmountBaseCurrency is LinkButton)
                    {
                        ((LinkButton)oControlAmountBaseCurrency).Text = Helper.GetLabelIDValue(1673);
                    }
                    else
                    {
                        if (oControlAmountBaseCurrency is LiteralControl)
                        {
                            ((LiteralControl)oControlAmountBaseCurrency).Text = Helper.GetLabelIDValue(1673);
                        }
                    }
                }
                else
                {
                    if (oControlAmountBaseCurrency is LinkButton)
                    {
                        ((LinkButton)oControlAmountBaseCurrency).Text = Helper.GetLabelIDValue(1673) + " (" + Helper.GetDisplayBaseCurrencyCode(this.CurrentBCCY) + ")";
                    }
                    else
                    {
                        if (oControlAmountBaseCurrency is LiteralControl)
                        {
                            ((LiteralControl)oControlAmountBaseCurrency).Text = Helper.GetLabelIDValue(1673) + " (" + Helper.GetDisplayBaseCurrencyCode(this.CurrentBCCY) + ")";
                        }
                    }
                }
                if (!_isCapabilityMultiCurrencyAccount)
                {
                    Control oControlAmount = new Control();
                    oControlAmount = (e.Item as GridHeaderItem)["Amount"].Controls[0];
                    if (oControlAmount is LinkButton)
                    {
                        ((LinkButton)oControlAmount).Text = Helper.GetLabelIDValue(1409);
                    }
                    else
                    {
                        if (oControlAmount is LiteralControl)
                        {
                            ((LiteralControl)oControlAmount).Text = Helper.GetLabelIDValue(1409);
                        }
                    }
                }
            }
        }
        void ucGLDataWriteOnOffGrid_GridItemDataBound(object sender, GridItemEventArgs e)
        {
            ShowHideCurrencyColumn(ucGLDataWriteOnOffGrid.Grid);
            SetHeader(e);
            if (e.Item.ItemType == GridItemType.Header)
            {
                if (ucGLDataWriteOnOffGrid.IsOnPage)
                    SessionHelper.ShowGridFilterIcon((PageBase)this.Page, ucGLDataWriteOnOffGrid.Grid, e, ucGLDataWriteOnOffGrid.Grid.ClientID);
                else
                    SessionHelper.ShowGridFilterIcon((PopupPageBase)this.Page, ucGLDataWriteOnOffGrid.Grid, e, ucGLDataWriteOnOffGrid.Grid.ClientID);
            }
            if (e.Item.ItemType == Telerik.Web.UI.GridItemType.Item || e.Item.ItemType == Telerik.Web.UI.GridItemType.AlternatingItem)
            {
                GridDataItem oGridDataItem = e.Item as GridDataItem;
                DataRow dr = ((DataRowView)oGridDataItem.DataItem).Row;

                ExHyperLink hlShowItemInputForm = (ExHyperLink)e.Item.FindControl("hlShowItemInputFormOpenGrid");
                Helper.SetImageURLForViewVersusEdit(EditMode, hlShowItemInputForm);
                bool IsForwardedItem;
                bool.TryParse(dr["IsForwardedItem"].ToString(), out    IsForwardedItem);
                long GLDataWriteOnOffID;
                if (long.TryParse(dr["GLDataWriteOnOffID"].ToString(), out GLDataWriteOnOffID))
                {
                    hlShowItemInputForm.NavigateUrl = PopupHelper.GetJavascriptParameterListForEditRecItem(
                             GLDataWriteOnOffID,
                             "OpenRadWindowForHyperlinkWithName",
                             "EditItemWriteOffOn.aspx",
                             EditMode.ToString(),
                             IsForwardedItem,
                             this.AccountID,
                             this.GLDataID,
                             this.RecCategoryTypeID,
                             this.NetAccountID,
                             this.IsSRA,
                             RecCategoryID,
                             hdIsRefreshData.ClientID,
                             this.CurrentBCCY);
                }
                if ((e.Item as GridDataItem)["DeleteColumn"] != null)
                {
                    ImageButton deleteButton = (ImageButton)(e.Item as GridDataItem)["DeleteColumn"].Controls[0];
                    CheckBox chkSelectItem = (CheckBox)(e.Item as GridDataItem)["CheckboxSelectColumn"].Controls[0];
                    if (EditMode == WebEnums.FormMode.Edit)
                    {
                        if (IsForwardedItem)
                        {
                            deleteButton.Visible = false;
                            chkSelectItem.Visible = false;
                        }
                    }
                    else
                    {
                        deleteButton.Visible = false;
                        chkSelectItem.Visible = false;
                    }
                    deleteButton.CommandArgument = GLDataWriteOnOffID.ToString();
                }
                SetMatchSetRefNumberUrlForGLDataRecItem(e, AccountID, NetAccountID, GLDataID);
                GridHelper.SetCSSClassForForwardedItems(e, IsForwardedItem);
            }
        }
        private void SetControlState()
        {
            if (IsPostBack && IsRefreshData && IsDataLoadCondition)
                LoadData();
            ExpandCollapse();
        }
        public void RegisterToggleControl(ExImageButton imgToggleControl)
        {
            imgToggleControl.OnClientClick += "return ToggleDiv('" + imgToggleControl.ClientID + "','" + this.DivClientId + "','" 
                + hdIsExpanded.ClientID + "','" + hdIsRefreshData.ClientID + "'," + (int?)AutoSaveAttributeID + ");";
            ToggleControl = imgToggleControl;
        }
        public override void LoadData()
        {
            if (IsRefreshData && IsExpanded)
            {
                //if (this.IsPostBack)
                //{
                    _isCapabilityMultiCurrencyAccount = Helper.IsCapabilityActivatedForCurrentRecPeriod(ARTEnums.Capability.MultiCurrency);
                    btnAdd.OnClientClick = PopupHelper.GetJavascriptParameterListForEditRecItem(null, "OpenRadWindowWithName", "EditItemWriteOffOn.aspx", QueryStringConstants.INSERT, false, this.AccountID, this.GLDataID, this.RecCategoryTypeID, this.NetAccountID, this.IsSRA, RecCategoryID, hdIsRefreshData.ClientID, this.CurrentBCCY);
                    btnAdd.Attributes.Add("onclick", "return false;");
                    //btnClose.OnClientClick = PopupHelper.GetJavascriptParameterList(null, "OpenRadWindow", "BulkCloseWriteOffOn.aspx", QueryStringConstants.INSERT, false, this.AccountID, this.GLDataID, this.RecCategoryTypeID, this.NetAccountID, this.IsSRA, RecCategoryID, hdIsRefreshData.ClientID);
                    btnClose.OnClientClick = PopupHelper.GetJavascriptParameterListForBulkClosepopup(null, "OpenRadWindowForHyperlinkWithName", "BulkCloseWriteOffOn.aspx", QueryStringConstants.INSERT, false, this.AccountID, this.GLDataID, this.RecCategoryTypeID, this.NetAccountID, this.IsSRA, RecCategoryID, hdIsRefreshData.ClientID, this.CurrentBCCY) + "; return false;";
                //}
                EnableDisableControlsForNonPreparersAndClosedPeriods();
                PopulateGrids();
                EnableDisableBulkCloseButton();
                IsRefreshData = false;
                if (IsExpanded)
                    this.divMainContent.Visible = true;

                if (IsExpanded && !this.IsPrintMode)
                {
                    ToggleControl.ImageUrl = "~/App_Themes/SkyStemBlueBrown/Images/CollapseGlass.gif";
                    if (this.AutoSaveAttributeID != null)
                    {
                        Helper.SaveAutoSaveAttributeValue((ARTEnums.AutoSaveAttribute)this.AutoSaveAttributeID, null, IsExpanded.ToString(), false);
                    }
                }
            }
        }
        public void ompage_ReconciliationPeriodChangedEventHandler(object sender, EventArgs e)
        {
            this._oGLDataWriteOnOffInfoCollection = null;
            if (IsPostBack)
            {
                IsRefreshData = true;
                if (IsExpanded)
                {
                    LoadData();
                }
            }
        }
        public void EnableDisableBulkCloseButton()
        {
            if (_oGLDataWriteOnOffInfoCollection != null && _oGLDataWriteOnOffInfoCollection.Count > 0)
                btnClose.Enabled = true;
            else
                btnClose.Enabled = false;
        }
        public void EnableDisableControlsForNonPreparersAndClosedPeriods()
        {
            this._UserRole = SessionHelper.CurrentRoleID.Value;
            this._RecPeriodStatus = (short)CurrentRecProcessStatus.Value;
            //if (Helper.GetFormMode(WebEnums.ARTPages.GLAdjustments, this.GLDataHdrInfo) == WebEnums.FormMode.Edit
            //    && this.GLDataID.Value > 0)
            if (EditMode == WebEnums.FormMode.Edit && this.GLDataID.Value > 0)
            {
                btnAdd.Visible = true;
                btnClose.Visible = true;
                btnReopen.Visible = true;
                btnDelete.Visible = true;
            }
            else
            {
                btnAdd.Visible = false;
                btnClose.Visible = false;
                btnReopen.Visible = false;
                btnDelete.Visible = false;
            }
        }
        protected void Page_PreRender(object sender, EventArgs e)
        {
            int lastPagePhraseID = Helper.GetPageTitlePhraseID(this.GetPreviousPageName());
        }
        #endregion

        #region "RADGrid Event Handling"
        private void ShowHideCurrencyColumn(ExRadGrid rg)
        {
            if (!_isCapabilityMultiCurrencyAccount)
            {
                // Hide LCCY Code Column if Multi-Currency is Off
                GridColumn gcLocalCurrencyCode = rg.Columns.FindByUniqueNameSafe("LocalCurrencyCode");
                if (gcLocalCurrencyCode != null)
                {
                    gcLocalCurrencyCode.Visible = false;
                }
            }
        }
        private DataTable GetGLDataParams()
        {
            DataTable dtGLData = new DataTable();
            dtGLData.Columns.Add("ID");
            List<GLDataWriteOnOffInfo> GLDataRecItemIDCollection = ucGLDataWriteOnOffGrid.SelectedGLDataWriteOnOffInfoCollection();
            if (GLDataRecItemIDCollection != null && GLDataRecItemIDCollection.Count > 0)
            {
                for (int i = 0; i < GLDataRecItemIDCollection.Count; i++)
                {
                    if (!GLDataRecItemIDCollection[i].IsForwardedItem.GetValueOrDefault())
                    {
                        DataRow rowGLData = dtGLData.NewRow();
                        rowGLData["ID"] = GLDataRecItemIDCollection[i].GLDataWriteOnOffID;
                        dtGLData.Rows.Add(rowGLData);
                    }
                }
            }
            return dtGLData;
        }
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            DataTable dtGLDataParams = new DataTable();
            dtGLDataParams = GetGLDataParams();
            if (dtGLDataParams != null && dtGLDataParams.Rows.Count > 0)
            {
                long? GLDataWriteOnOffID = 0;
                IGLDataWriteOnOff oGLDataWriteOnOff = RemotingHelper.GetGLDataWriteOnOffObject();
                oGLDataWriteOnOff.DeleteGLDataWriteOnOff(GLDataWriteOnOffID, this.GLDataID, this.RecCategoryTypeID, SessionHelper.CurrentUserLoginID, DateTime.Now, dtGLDataParams, Helper.GetAppUserInfo());
                RecHelper.RefreshRecForm(this);
            }
        }
        #endregion
        private string GetPreviousPageName()
        {
            string PName = "";
            if (Request.UrlReferrer != null)
            {
                PName = Request.UrlReferrer.Segments[Request.UrlReferrer.Segments.Length - 1];
            }
            return PName;
        }
        protected void btnReopen_Click(object sender, EventArgs e)
        {
            List<long> glDataWriteOnOffIdCollection = new List<long>();
            glDataWriteOnOffIdCollection = ucCloseGLDataWriteOnOffGrid.SelectedGLDataWriteOnOffIDs();
            if (glDataWriteOnOffIdCollection != null && glDataWriteOnOffIdCollection.Count > 0)
            {
                IGLDataWriteOnOff oGLDataWriteOnOffClient = RemotingHelper.GetGLDataWriteOnOffObject();
                oGLDataWriteOnOffClient.UpdateGLDataWriteOnOffCloseDate(this.GLDataID.Value, glDataWriteOnOffIdCollection
                    , null, null, null, null, this.RecCategoryTypeID.Value, (short)ARTEnums.AccountAttribute.ReconciliationTemplate
                    , SessionHelper.GetCurrentUser().LoginID, DateTime.Now
                    , SessionHelper.CurrentReconciliationPeriodID.Value, Helper.GetAppUserInfo());
                this._oGLDataWriteOnOffInfoCollection = null;
                EnableDisableBulkCloseButton();
                RecHelper.RefreshRecForm(this);
            }
        }
        private void PopulateGrids()
        {

            if (this.GLDataID == null || this.GLDataID == 0)
            {
                this.GetGLDataWriteOnOffInfoCollection = null;
                ucGLDataWriteOnOffGrid.SetGLDataWriteOnOffGridData(GetGLDataWriteOnOffInfoCollection);
                ucCloseGLDataWriteOnOffGrid.SetGLDataWriteOnOffGridData(GetGLDataWriteOnOffInfoCollection);
            }
            else
            {
                ucGLDataWriteOnOffGrid.GLDataHdrInfo = this.GLDataHdrInfo;
                ucCloseGLDataWriteOnOffGrid.GLDataHdrInfo = this.GLDataHdrInfo;
                // rgItemInputWO.EntityNameText = PopupHelper.GetPageTitle(this.RecCategoryID.Value, this.RecCategoryTypeID.Value);
                IGLDataWriteOnOff oGLDataWriteOnOff = RemotingHelper.GetGLDataWriteOnOffObject();
                this.GetGLDataWriteOnOffInfoCollection = oGLDataWriteOnOff.GetGLDataWriteOnOffInfoCollectionByGLDataID(this.GLDataID, (int)ARTEnums.AccountAttribute.ReconciliationTemplate, Helper.GetAppUserInfo());

                List<GLDataWriteOnOffInfo> oOpenGLDataWriteOnOffInfoCollection = this.GetGLDataWriteOnOffInfoCollection.Where(recItem => recItem.CloseDate == null).ToList();
                ucGLDataWriteOnOffGrid.SetGLDataWriteOnOffGridData(oOpenGLDataWriteOnOffInfoCollection);
                ucGLDataWriteOnOffGrid.LoadGridData();
                this.EnableDisableBulkCloseButton();
                //if (oOpenGLDataWriteOnOffInfoCollection == null || oOpenGLDataWriteOnOffInfoCollection.Count == 0)
                //{
                //    btnDelete.Visible = false;
                //}
                //else
                //{
                //    btnDelete.Visible = true;
                //}
                List<GLDataWriteOnOffInfo> oGLDataWriteOnOffInfoCollectionClosed = this.GetGLDataWriteOnOffInfoCollection.Where(recItem => recItem.CloseDate != null).ToList();
                if (oGLDataWriteOnOffInfoCollectionClosed == null || oGLDataWriteOnOffInfoCollectionClosed.Count == 0)
                {
                    pnlClosedItems.Visible = false;
                }
                else
                {
                    pnlClosedItems.Visible = true;
                    if (EditMode == WebEnums.FormMode.ReadOnly)
                    {
                        ucCloseGLDataWriteOnOffGrid.ShowSelectCheckBoxColum = false;
                    }
                    ucCloseGLDataWriteOnOffGrid.SetGLDataWriteOnOffGridData(oGLDataWriteOnOffInfoCollectionClosed);
                    ucCloseGLDataWriteOnOffGrid.LoadGridData();
                }
            }
        }
        protected void btnCancel_OnClick(object sender, EventArgs e)
        {
            string pathAndQuery = (string)ViewState[ViewStateConstants.PATH_AND_QUERY];
            //Response.Redirect(pathAndQuery);
            SessionHelper.RedirectToUrl(pathAndQuery);
            return;
        }
        public override void ExpandCollapse()
        {
            base.ExpandCollapse();
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
        public void ApplyFilter()
        {
            ucGLDataWriteOnOffGrid.ApplyFilterGLDataWriteOnOffsGrid();
            ucCloseGLDataWriteOnOffGrid.ApplyFilterGLDataWriteOnOffsGrid();
        }
    }
}