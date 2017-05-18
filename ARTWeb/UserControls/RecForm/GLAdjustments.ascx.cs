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
using SkyStem.ART.Shared.Utility;

namespace SkyStem.ART.Web.UserControls
{

    public partial class UserControls_GLAdjustments : UserControlRecItemBase
    {
        const int POPUP_WIDTH = 630;
        const int POPUP_HEIGHT = 480;

        ExImageButton ToggleControl;
        #region PrivateProperties
        short _GLReconciliationItemInputRecordTypeID = 0;
        private List<GLDataRecItemInfo> _GLRecItemInfoCollection = null;
        private bool? _IsExpandedValueForced = null;
        #endregion
        #region "Private Properties"
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
        public bool IsMultiCurrencyActivated
        {
            get { return (bool)ViewState["IsMultiCurrencyActivated"]; }
            set { ViewState["IsMultiCurrencyActivated"] = value; }
        }
        private bool _ShowCopyButton = false;
        public bool ShowCopyButton
        {
            get
            {
                return _ShowCopyButton;
            }
            set
            {
                _ShowCopyButton = value;
            }
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
        private List<GLDataRecItemInfo> GetGLRecItemInfoCollection
        {
            get
            {
                return this._GLRecItemInfoCollection;
            }
            set
            {
                this._GLRecItemInfoCollection = value;
            }
        }
        #endregion
        #region "Page Events"
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                IsRefreshData = false;
                PopulateGrids();
                IsMultiCurrencyActivated = Helper.IsCapabilityActivatedForCurrentRecPeriod(ARTEnums.Capability.MultiCurrency);
                ucGLDataRecItemGrid.IsMultiCurrencyActivated = IsMultiCurrencyActivated;
                ucCloseGLDataRecItemGrid.IsMultiCurrencyActivated = IsMultiCurrencyActivated;
            }
            ucGLDataRecItemGrid.GridItemDataBound += new GridItemEventHandler(ucGLDataRecItemGrid_GridItemDataBound);
            ucGLDataRecItemGrid.GridCommand += new GridCommandEventHandler(ucGLDataRecItemGrid_GridCommand);
            ucCloseGLDataRecItemGrid.GridItemDataBound += new GridItemEventHandler(ucCloseGLDataRecItemGrid_GridItemDataBound);
            SetControlState();
            if (EntityNameLabelID.HasValue)
            {
                ucGLDataRecItemGrid.BasePageTitleLabelID = EntityNameLabelID.Value;
                ucCloseGLDataRecItemGrid.BasePageTitleLabelID = EntityNameLabelID.Value;
            }
            //********* Refresh Rec Period status *******************************************************
            //if (IsPostBack)
            //{
            //    string abc = hdIsRefreshData.Value;

            //    RecHelper.ReloadRecPeriodsOnMasterPage(this);
            //}
        }

        void ucCloseGLDataRecItemGrid_GridItemDataBound(object sender, GridItemEventArgs e)
        {
            ShowHideCurrencyColumn(ucCloseGLDataRecItemGrid.Grid);
            SetHeader(e);
            if (e.Item.ItemType == GridItemType.Header)
            {
                if (ucCloseGLDataRecItemGrid.IsOnPage)
                    SessionHelper.ShowGridFilterIcon((PageBase)this.Page, ucCloseGLDataRecItemGrid.Grid, e, ucCloseGLDataRecItemGrid.Grid.ClientID);
                else
                    SessionHelper.ShowGridFilterIcon((PopupPageBase)this.Page, ucCloseGLDataRecItemGrid.Grid, e, ucCloseGLDataRecItemGrid.Grid.ClientID);
            }
            if (e.Item.ItemType == Telerik.Web.UI.GridItemType.Item || e.Item.ItemType == Telerik.Web.UI.GridItemType.AlternatingItem)
            {
                WebEnums.FormMode EditModeReadOnly = WebEnums.FormMode.ReadOnly;
                GridDataItem oGridDataItem = e.Item as GridDataItem;
                DataRow dr = ((DataRowView)oGridDataItem.DataItem).Row;
                SetMatchSetRefNumberUrlForGLDataRecItem(e, AccountID, NetAccountID, GLDataID);
                ExHyperLink hlShowItemInputForm = (ExHyperLink)e.Item.FindControl("hlShowItemInputForm");
                Helper.SetImageURLForViewVersusEdit(EditModeReadOnly, hlShowItemInputForm);




                bool IsForwardedItem;
                bool.TryParse(dr["IsForwardedItem"].ToString(), out    IsForwardedItem);
                long GLDataRecItemID;
                if (long.TryParse(dr["GLDataRecItemID"].ToString(), out GLDataRecItemID))
                {
                    hlShowItemInputForm.NavigateUrl = PopupHelper.GetJavascriptParameterListForEditRecItem(
                       GLDataRecItemID,
                        "OpenRadWindowForHyperlinkWithName",
                        "EditRecItemInputs.aspx",
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
        void ucGLDataRecItemGrid_GridCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                DataTable dtGLDataParams = new DataTable();
                dtGLDataParams.Columns.Add("ID");
                DataRow drGLData = dtGLDataParams.NewRow();
                drGLData["ID"] = Convert.ToInt64(e.CommandArgument);
                dtGLDataParams.Rows.Add(drGLData);

                GLDataRecItemInfo oGLReconciliationItemInputInfo = new GLDataRecItemInfo();
                oGLReconciliationItemInputInfo.GLDataRecItemID = Convert.ToInt64(e.CommandArgument);
                oGLReconciliationItemInputInfo.GLDataID = this.GLDataID;
                oGLReconciliationItemInputInfo.ReconciliationCategoryTypeID = this.RecCategoryTypeID;
                oGLReconciliationItemInputInfo.RevisedBy = SessionHelper.CurrentUserLoginID;
                oGLReconciliationItemInputInfo.DateRevised = DateTime.Now;
                IGLDataRecItem oGLRecItemInputClient = RemotingHelper.GetGLDataRecItemObject();
                oGLRecItemInputClient.DeleteRecInputItem(oGLReconciliationItemInputInfo, (short)ARTEnums.AccountAttribute.ReconciliationTemplate, dtGLDataParams, Helper.GetAppUserInfo());
                RecHelper.RefreshRecForm(this);
            }
            if (e.CommandName == "Copy")
            {
                DataTable dtGLDataParams = new DataTable();
                dtGLDataParams.Columns.Add("ID");
                DataRow drGLData = dtGLDataParams.NewRow();
                drGLData["ID"] = Convert.ToInt64(e.CommandArgument);
                dtGLDataParams.Rows.Add(drGLData);
                CopyRecItem(false, dtGLDataParams);
            }
            if (e.CommandName == "CopyAndClose")
            {
                DataTable dtGLDataParams = new DataTable();
                dtGLDataParams.Columns.Add("ID");
                DataRow drGLData = dtGLDataParams.NewRow();
                drGLData["ID"] = Convert.ToInt64(e.CommandArgument);
                dtGLDataParams.Rows.Add(drGLData);
                CopyRecItem(true, dtGLDataParams);
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
                if (!IsMultiCurrencyActivated)
                {
                    Control oControlAmount = new Control();
                    oControlAmount = (e.Item as GridHeaderItem)["Amount"].Controls[0];
                    if (oControlAmount is LinkButton)
                    {
                        ((LinkButton)oControlAmount).Text = Helper.GetLabelIDValue(1675);
                    }
                    else
                    {
                        if (oControlAmount is LiteralControl)
                        {
                            ((LiteralControl)oControlAmount).Text = Helper.GetLabelIDValue(1675);
                        }
                    }
                }
            }
        }
        void ucGLDataRecItemGrid_GridItemDataBound(object sender, GridItemEventArgs e)
        {
            ShowHideCurrencyColumn(ucGLDataRecItemGrid.Grid);
            SetHeader(e);
            if (e.Item.ItemType == GridItemType.Header)
            {
                if (ucGLDataRecItemGrid.IsOnPage)
                    SessionHelper.ShowGridFilterIcon((PageBase)this.Page, ucGLDataRecItemGrid.Grid, e, ucGLDataRecItemGrid.Grid.ClientID);
                else
                    SessionHelper.ShowGridFilterIcon((PopupPageBase)this.Page, ucGLDataRecItemGrid.Grid, e, ucGLDataRecItemGrid.Grid.ClientID);
            }

            if (e.Item.ItemType == Telerik.Web.UI.GridItemType.Item || e.Item.ItemType == Telerik.Web.UI.GridItemType.AlternatingItem)
            {
                GridDataItem oGridDataItem = e.Item as GridDataItem;
                DataRow dr = ((DataRowView)oGridDataItem.DataItem).Row;
                // ShowHide the Excel Button if  the Rec Item is imported through file (and not added manually)
                ExImageButton imgViewFile = (ExImageButton)e.Item.FindControl("imgViewFileOpenGrid");
                if (dr["DataImportID"] != null)
                {
                    int DataImportID;
                    if (int.TryParse(dr["DataImportID"].ToString(), out DataImportID))
                    {
                        if (Convert.IsDBNull(dr["PreviousGLDataRecItemID"]))
                        {
                            //IDataImport oDataImportClient = RemotingHelper.GetDataImportObject();
                            //DataImportHdrInfo oDataImportHdrInfo = oDataImportClient.GetDataImportInfo(DataImportID, Helper.GetAppUserInfo());
                            //if (oDataImportHdrInfo != null)
                            //{
                            if (dr["PhysicalPath"] != null)
                            {
                                imgViewFile.Visible = true;
                                string url = "DownloadAttachment.aspx?" + QueryStringConstants.FILE_PATH + "=" + Server.UrlEncode(SharedHelper.GetDisplayFilePath(dr["PhysicalPath"].ToString()));
                                imgViewFile.OnClientClick = "document.location.href = '" + url + "';return false;";
                            }
                        }
                    }
                }
                ExHyperLink hlShowItemInputForm = (ExHyperLink)e.Item.FindControl("hlShowItemInputFormOpenGrid");
                Helper.SetImageURLForViewVersusEdit(EditMode, hlShowItemInputForm);
                ExHyperLink hlAddRecItemComment = (ExHyperLink)e.Item.FindControl("hlAddRecItemComment");

                bool IsCommentAvailable;
                bool.TryParse(dr["IsCommentAvailable"].ToString(), out    IsCommentAvailable);
                if (!IsCommentAvailable)
                    hlAddRecItemComment.ImageUrl = "~/App_Themes/SkyStemBlueBrown/Images/Comment.gif";
                else
                    hlAddRecItemComment.ImageUrl = "~/App_Themes/SkyStemBlueBrown/Images/Comment_Green.gif";


                bool IsForwardedItem;
                bool.TryParse(dr["IsForwardedItem"].ToString(), out    IsForwardedItem);
                long GLDataRecItemID;
                if (long.TryParse(dr["GLDataRecItemID"].ToString(), out GLDataRecItemID))
                {
                    hlShowItemInputForm.NavigateUrl = PopupHelper.GetJavascriptParameterListForEditRecItem(
                       GLDataRecItemID,
                        "OpenRadWindowForHyperlinkWithName",
                        "EditRecItemInputs.aspx",
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
                    hlAddRecItemComment.NavigateUrl = "javascript:OpenRadWindowForHyperlink('" + PopupHelper.getRecItemCommentPopupUrl(this.GLDataHdrInfo, GLDataRecItemID, (short)WebEnums.RecordType.GLReconciliationItemInput, this.AccountID, this.NetAccountID) + "', " + POPUP_HEIGHT + " , " + POPUP_WIDTH + ");";

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
                           // chkSelectItem.Visible = false;
                        }
                    }
                    else
                    {
                        deleteButton.Visible = false;
                        chkSelectItem.Visible = false;
                    }
                    deleteButton.CommandArgument = GLDataRecItemID.ToString();
                }
                if ((e.Item as GridDataItem)["CopyColumn"] != null)
                {
                    ImageButton CopyButton = (ImageButton)(e.Item as GridDataItem)["CopyColumn"].Controls[0];                   
                    if (EditMode == WebEnums.FormMode.Edit)
                    {
                        if (ShowCopyButton)
                        {
                            CopyButton.Visible = true;                           
                        }
                        else
                            CopyButton.Visible = false;         
                    }
                    else
                    {
                        CopyButton.Visible = false;                        
                    }
                    CopyButton.CommandArgument = GLDataRecItemID.ToString();
                }

                if ((e.Item as GridDataItem)["CopyAndCloseColumn"] != null)
                {
                    ImageButton CopyAndCloseButton = (ImageButton)(e.Item as GridDataItem)["CopyAndCloseColumn"].Controls[0];
                    if (EditMode == WebEnums.FormMode.Edit)
                    {
                        if (ShowCopyButton)
                        {
                            CopyAndCloseButton.Visible = true;
                        }
                        else
                            CopyAndCloseButton.Visible = false;
                    }
                    else
                    {
                        CopyAndCloseButton.Visible = false;
                    }
                    CopyAndCloseButton.CommandArgument = GLDataRecItemID.ToString();
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
            imgToggleControl.OnClientClick += "return ToggleDiv('" + imgToggleControl.ClientID + "','" + this.DivClientId + "','" + hdIsExpanded.ClientID + "','" + hdIsRefreshData.ClientID + "');";
            ToggleControl = imgToggleControl;
        }
        public override void LoadData()
        {
            if (IsRefreshData && IsExpanded)
            {

                if (this.IsPostBack)
                {
                    this._GLRecItemInfoCollection = null;
                    this._GLReconciliationItemInputRecordTypeID = (short)WebEnums.RecordType.GLReconciliationItemInput;
                    btnAdd.OnClientClick = PopupHelper.GetJavascriptParameterListForEditRecItem(null, "OpenRadWindowWithName", "EditRecItemInputs.aspx", QueryStringConstants.INSERT, false, this.AccountID, this.GLDataID, this.RecCategoryTypeID.Value, this.NetAccountID, this.IsSRA, RecCategoryID, hdIsRefreshData.ClientID, this.CurrentBCCY);
                    btnAdd.Attributes.Add("onclick", "return false;");
                    btnClose.OnClientClick = PopupHelper.GetJavascriptParameterListForBulkClosepopup(null, "OpenRadWindowForHyperlinkWithName", "GLAdjustmentBulkClose.aspx", QueryStringConstants.INSERT, false, this.AccountID, this.GLDataID, this.RecCategoryTypeID.Value, this.NetAccountID, this.IsSRA, RecCategoryID, hdIsRefreshData.ClientID, this.CurrentBCCY) + "; return false;";

                }
                this.EnableDisableControlsForNonPreparersAndClosedPeriods();
                PopulateGrids();
                IsRefreshData = false;
                if (IsExpanded)
                {
                    this.divMainContent.Visible = true;
                    //set style in code behind as same as of client side
                    //this is required as during postback whole content refreshed so style, as of initial state
                }
                if (IsExpanded && !this.IsPrintMode)
                {
                    ToggleControl.ImageUrl = "~/App_Themes/SkyStemBlueBrown/Images/CollapseGlass.gif";
                }
            }
        }

        public void ompage_ReconciliationPeriodChangedEventHandler(object sender, EventArgs e)
        {
            this._GLRecItemInfoCollection = null;
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
            if (GetGLRecItemInfoCollection != null && GetGLRecItemInfoCollection.Count > 0)
                btnClose.Enabled = true;
            else
                btnClose.Enabled = false;
        }

        public void EnableDisableControlsForNonPreparersAndClosedPeriods()
        {
            if (EditMode == WebEnums.FormMode.Edit && this.GLDataID.Value > 0)
            {
                btnAdd.Visible = true;
                btnClose.Visible = true;
                btnReopen.Visible = true;
                btnDelete.Visible = true;
                if (ShowCopyButton)
                {
                    btnCopy.Visible = true;
                    btnCopyAndClose.Visible = true;
                }
                else
                {
                    btnCopy.Visible = false;
                    btnCopyAndClose.Visible = false;
                }

            }
            else
            {
                btnAdd.Visible = false;
                btnClose.Visible = false;
                btnReopen.Visible = false;
                btnDelete.Visible = false;
                btnCopy.Visible = false;
                btnCopyAndClose.Visible = false;
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
            if (!IsMultiCurrencyActivated)
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
            List<GLDataRecItemInfo> GLDataRecItemIDCollection = ucGLDataRecItemGrid.SelectedGLDataRecItemInfoCollection();
            if (GLDataRecItemIDCollection != null && GLDataRecItemIDCollection.Count > 0)
            {
                for (int i = 0; i < GLDataRecItemIDCollection.Count; i++)
                {
                    if (!GLDataRecItemIDCollection[i].IsForwardedItem.GetValueOrDefault())
                    {
                        DataRow rowGLData = dtGLData.NewRow();
                        rowGLData["ID"] = GLDataRecItemIDCollection[i].GLDataRecItemID;
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
                GLDataRecItemInfo oGLReconciliationItemInputInfo = new GLDataRecItemInfo();
                oGLReconciliationItemInputInfo.GLDataRecItemID = 0;
                oGLReconciliationItemInputInfo.GLDataID = this.GLDataID;
                oGLReconciliationItemInputInfo.ReconciliationCategoryTypeID = this.RecCategoryTypeID;
                oGLReconciliationItemInputInfo.RevisedBy = SessionHelper.CurrentUserLoginID;
                oGLReconciliationItemInputInfo.DateRevised = DateTime.Now;
                IGLDataRecItem oGLRecItemInputClient = RemotingHelper.GetGLDataRecItemObject();
                oGLRecItemInputClient.DeleteRecInputItem(oGLReconciliationItemInputInfo, (short)ARTEnums.AccountAttribute.ReconciliationTemplate, dtGLDataParams, Helper.GetAppUserInfo());
                RecHelper.RefreshRecForm(this);
            }
        }

        protected void btnCopy_Click(object sender, EventArgs e)
        {
            DataTable dtGLDataParams = new DataTable();
            dtGLDataParams = GetGLDataRecItemForCopy();
            CopyRecItem(false, dtGLDataParams);
        }
        protected void btnCopyAndClose_Click(object sender, EventArgs e)
        {
            DataTable dtGLDataParams = new DataTable();
            dtGLDataParams = GetGLDataRecItemForCopy();
            CopyRecItem(true, dtGLDataParams);
        }

        private void CopyRecItem(bool CloseSourceRecItem,DataTable dtGLDataParams)
        {
           
            if (dtGLDataParams != null && dtGLDataParams.Rows.Count > 0)
            {
                GLDataRecItemInfo oGLReconciliationItemInputInfo = new GLDataRecItemInfo();
                oGLReconciliationItemInputInfo.GLDataRecItemID = 0;
                oGLReconciliationItemInputInfo.GLDataID = this.GLDataID;
                oGLReconciliationItemInputInfo.AddedBy = SessionHelper.CurrentUserLoginID;
                oGLReconciliationItemInputInfo.DateAdded = DateTime.Now;
                oGLReconciliationItemInputInfo.AddedByUserID = SessionHelper.CurrentUserID;
                IGLDataRecItem oGLRecItemInputClient = RemotingHelper.GetGLDataRecItemObject();
                List<AttachmentInfo> oAttachmentInfoList = oGLRecItemInputClient.CopyRecInputItem(oGLReconciliationItemInputInfo, dtGLDataParams, SessionHelper.CurrentReconciliationPeriodID, CloseSourceRecItem, Helper.GetAppUserInfo());
                // Add Attechments
                if (oAttachmentInfoList.Count > 0)
                {
                    int AttachementNo = 1;
                    foreach (var item in oAttachmentInfoList)
                    {
                        string originalFileName = item.FileName;
                        //originalFileName="AccountDetails_514201451316PM.txt";
                        string originalFilePath = item.PhysicalPath;
                        string newFileName = Helper.GetNewFileName(originalFileName, AttachementNo);
                        string newFilePath = Helper.GetNewFilePath(newFileName);
                        Helper.CopyFile(originalFilePath, newFilePath);
                        item.FileName = newFileName;
                        item.PhysicalPath = newFilePath;
                        item.Date = DateTime.Now;
                        item.UserID = SessionHelper.CurrentUserID;
                        item.IsActive = true;
                        AttachementNo += 1;
                    }
                    // Insert Attachments 
                    IAttachment oAttachmentClient = RemotingHelper.GetAttachmentObject();
                    oAttachmentClient.InsertAttachmentBulk(oAttachmentInfoList, oGLReconciliationItemInputInfo.DateAdded.Value, Helper.GetAppUserInfo());
                }

                RecHelper.RefreshRecForm(this);
            }

        }
        private DataTable GetGLDataRecItemForCopy()
        {
            DataTable dtGLData = new DataTable();
            dtGLData.Columns.Add("RecItamID");
            List<GLDataRecItemInfo> GLDataRecItemIDCollection = ucGLDataRecItemGrid.SelectedGLDataRecItemInfoCollection();
            if (GLDataRecItemIDCollection != null && GLDataRecItemIDCollection.Count > 0)
            {
                for (int i = 0; i < GLDataRecItemIDCollection.Count; i++)
                {
                    DataRow rowGLData = dtGLData.NewRow();
                    rowGLData["RecItamID"] = GLDataRecItemIDCollection[i].GLDataRecItemID;
                    dtGLData.Rows.Add(rowGLData);
                }
            }
            return dtGLData;
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
            List<long> glRecItemInputIdCollection = new List<long>();
            glRecItemInputIdCollection = ucCloseGLDataRecItemGrid.SelectedGLDataRecItemIDs();

            if (glRecItemInputIdCollection != null
                && glRecItemInputIdCollection.Count > 0)
            {
                IGLDataRecItem oGLDataRecItemClient = RemotingHelper.GetGLDataRecItemObject();
                oGLDataRecItemClient.UpdateGLRecItemCloseDate(this.GLDataID.Value, glRecItemInputIdCollection
                    , null, null, null, null, this.RecCategoryTypeID.Value, (short)ARTEnums.AccountAttribute.ReconciliationTemplate
                    , SessionHelper.GetCurrentUser().LoginID, DateTime.Now, SessionHelper.CurrentReconciliationPeriodID.Value, Helper.GetAppUserInfo());

                this._GLRecItemInfoCollection = null;
                EnableDisableBulkCloseButton();
                RecHelper.RefreshRecForm(this);
            }
        }
        public void PopulateGrids()
        {
            if (this.GLDataID == null || this.GLDataID == 0)
            {
                this.GetGLRecItemInfoCollection = null;
                ucGLDataRecItemGrid.SetGLDataRecItemGridData(GetGLRecItemInfoCollection);
                ucCloseGLDataRecItemGrid.SetGLDataRecItemGridData(GetGLRecItemInfoCollection);
            }
            else
            {
                ucGLDataRecItemGrid.GLDataHdrInfo = this.GLDataHdrInfo;
                ucCloseGLDataRecItemGrid.GLDataHdrInfo = this.GLDataHdrInfo;
                IGLDataRecItem oGLRecItemInput = RemotingHelper.GetGLDataRecItemObject();
                this.GetGLRecItemInfoCollection = oGLRecItemInput.GetRecItem(this.GLDataID.Value, SessionHelper.CurrentReconciliationPeriodID.Value, this.RecCategoryTypeID.Value, this._GLReconciliationItemInputRecordTypeID, (short)ARTEnums.AccountAttribute.ReconciliationTemplate, Helper.GetAppUserInfo());
                List<GLDataRecItemInfo> oOpenGLDataRecItemInfoCollection = this.GetGLRecItemInfoCollection.Where(recItem => recItem.CloseDate == null).ToList();
                ucGLDataRecItemGrid.SetGLDataRecItemGridData(oOpenGLDataRecItemInfoCollection);
                ucGLDataRecItemGrid.LoadGridData();
                this.EnableDisableBulkCloseButton();

                List<GLDataRecItemInfo> oCloseGLDataRecItemInfoCollection = this.GetGLRecItemInfoCollection.Where(recItem => recItem.CloseDate != null).ToList();
                if (oCloseGLDataRecItemInfoCollection == null || oCloseGLDataRecItemInfoCollection.Count == 0)
                {
                    pnlClosedItems.Visible = false;
                }
                else
                {
                    pnlClosedItems.Visible = true;
                    if (EditMode == WebEnums.FormMode.ReadOnly)
                    {
                        ucCloseGLDataRecItemGrid.ShowSelectCheckBoxColum = false;
                    }
                    ucCloseGLDataRecItemGrid.SetGLDataRecItemGridData(oCloseGLDataRecItemInfoCollection);
                    ucCloseGLDataRecItemGrid.LoadGridData();
                }

            }
        }
        protected void btnCancel_OnClick(object sender, EventArgs e)
        {
            string pathAndQuery = (string)ViewState[ViewStateConstants.PATH_AND_QUERY];
            Response.Redirect(pathAndQuery);
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
            ucGLDataRecItemGrid.ApplyFilterGLDataRecItemsGrid();
            ucCloseGLDataRecItemGrid.ApplyFilterGLDataRecItemsGrid();
        }

    }
}