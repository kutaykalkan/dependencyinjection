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

    public partial class UserControls_GLDataRecurringScheduleItems : UserControlRecItemBase
    {
        const int POPUP_WIDTH = 630;
        const int POPUP_HEIGHT = 480;

        ExImageButton ToggleControl;
        #region PrivateProperties
        static bool _isCapabilityMultiCurrencyAccount = false;
        public short _GLReconciliationItemInputRecordTypeID = 0;
        private List<GLDataRecurringItemScheduleInfo> _GLDataRecurringItemScheduleInfoCollection = null;
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
        private List<GLDataRecurringItemScheduleInfo> GetGLDataRecurringItemScheduleInfoCollection
        {
            get
            {
                return this._GLDataRecurringItemScheduleInfoCollection;
            }
            set
            {
                this._GLDataRecurringItemScheduleInfoCollection = value;
            }
        }
        #endregion
        #region "Page Events"
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PopulateGrids();
            }

            ucGLDataRecurringScheduleItemsGrid.GridItemDataBound += new GridItemEventHandler(ucGLDataRecurringScheduleItemsGrid_GridItemDataBound);
            ucGLDataRecurringScheduleItemsGrid.GridCommand += new GridCommandEventHandler(ucGLDataRecurringScheduleItemsGrid_GridCommand);
            ucCloseGLDataRecurringScheduleItemsGrid.GridItemDataBound += new GridItemEventHandler(ucCloseGLDataRecurringScheduleItemsGrid_GridItemDataBound);

            SetControlState();
            if (EntityNameLabelID.HasValue)
            {
                ucGLDataRecurringScheduleItemsGrid.BasePageTitleLabelID = EntityNameLabelID.Value;
                ucCloseGLDataRecurringScheduleItemsGrid.BasePageTitleLabelID = EntityNameLabelID.Value;
            }
            //********* Refresh Rec Period status *******************************************************
            //if (IsPostBack)
            //{
            //    RecHelper.ReloadRecPeriodsOnMasterPage(this);
            //}

        }

        void ucCloseGLDataRecurringScheduleItemsGrid_GridItemDataBound(object sender, GridItemEventArgs e)
        {
            ShowHideCurrencyColumn(ucCloseGLDataRecurringScheduleItemsGrid.Grid);
            if (this.RecCategoryTypeID == (short)WebEnums.RecCategoryType.Amortizable_SupportingDetail_RecurringAmortizableSchedule)
            {
                ucCloseGLDataRecurringScheduleItemsGrid.SetAmortizableGridHeaders(e);
            }
            else
            {
                ucCloseGLDataRecurringScheduleItemsGrid.SetAccruableGridHeaders(e);
            }

            if (e.Item.ItemType == GridItemType.Header)
            {
                if (ucCloseGLDataRecurringScheduleItemsGrid.IsOnPage)
                    SessionHelper.ShowGridFilterIcon((PageBase)this.Page, ucCloseGLDataRecurringScheduleItemsGrid.Grid, e, ucCloseGLDataRecurringScheduleItemsGrid.Grid.ClientID);
                else
                    SessionHelper.ShowGridFilterIcon((PopupPageBase)this.Page, ucCloseGLDataRecurringScheduleItemsGrid.Grid, e, ucCloseGLDataRecurringScheduleItemsGrid.Grid.ClientID);
            }
            if (e.Item.ItemType == Telerik.Web.UI.GridItemType.Item || e.Item.ItemType == Telerik.Web.UI.GridItemType.AlternatingItem)
            {
                WebEnums.FormMode EditModeReadOnly = WebEnums.FormMode.ReadOnly;
                GridDataItem oGridDataItem = e.Item as GridDataItem;
                DataRow dr = ((DataRowView)oGridDataItem.DataItem).Row;
                SetMatchSetRefNumberUrlForGLDataRecItem(e, AccountID, NetAccountID, GLDataID);
                ExHyperLink hlShowItemInputForm = (ExHyperLink)e.Item.FindControl("hlShowItemInputForm");
                Helper.SetImageURLForViewVersusEdit(EditModeReadOnly, hlShowItemInputForm);
                int CreatedInRecPeriodID;
                int.TryParse(dr["CreatedInRecPeriodID"].ToString(), out    CreatedInRecPeriodID);
                bool IsForwardedItem = !(CreatedInRecPeriodID == SessionHelper.CurrentReconciliationPeriodID);
                long GLDataRecurringItemScheduleID;
                if (long.TryParse(dr["GLDataRecurringItemScheduleID"].ToString(), out GLDataRecurringItemScheduleID))
                {
                    hlShowItemInputForm.NavigateUrl = PopupHelper.GetJavascriptParameterListForEditRecItem(GLDataRecurringItemScheduleID, "OpenRadWindowForHyperlinkWithName", "EditItemAccrubleRecurring.aspx", EditModeReadOnly.ToString(), IsForwardedItem, this.AccountID, this.GLDataID, this.RecCategoryTypeID, this.NetAccountID, this.IsSRA, RecCategoryID, hdIsRefreshData.ClientID, this.CurrentBCCY);//IsforwardedItem is not used here so redundant
                }
            }
        }
        public static void SetMatchSetRefNumberUrlForGLDataRecItem(GridItemEventArgs e, long? AccountID, long? NetAccountID, long? GLDataID)
        {
            GridDataItem oGridDataItem = e.Item as GridDataItem;
            DataRow dr = ((DataRowView)oGridDataItem.DataItem).Row;
            ExHyperLink hlMatchSetRefNumber = (ExHyperLink)e.Item.FindControl("hlMatchSetRefNumber");
            int CreatedInRecPeriodID;
            int.TryParse(dr["CreatedInRecPeriodID"].ToString(), out    CreatedInRecPeriodID);
            bool IsForwardedItem = !(CreatedInRecPeriodID == SessionHelper.CurrentReconciliationPeriodID);
            if (!string.IsNullOrEmpty(Convert.ToString(dr["MatchSetRefNumber"])))
            {
                hlMatchSetRefNumber.Text = Helper.GetDisplayStringValue(dr["MatchSetRefNumber"].ToString());
            }

            if (!IsForwardedItem)
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
        void ucGLDataRecurringScheduleItemsGrid_GridCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                DataTable dtGLDataParams = new DataTable();
                dtGLDataParams.Columns.Add("ID");
                DataRow drGLData = dtGLDataParams.NewRow();
                drGLData["ID"] = Convert.ToInt64(e.CommandArgument);
                dtGLDataParams.Rows.Add(drGLData);
                GLDataRecurringItemScheduleInfo oGLDataRecurringItemScheduleInfo = new GLDataRecurringItemScheduleInfo();
                oGLDataRecurringItemScheduleInfo.GLDataRecurringItemScheduleID = Convert.ToInt64(e.CommandArgument);
                oGLDataRecurringItemScheduleInfo.GLDataID = this.GLDataID;
                oGLDataRecurringItemScheduleInfo.ReconciliationCategoryTypeID = this.RecCategoryTypeID;
                oGLDataRecurringItemScheduleInfo.RevisedBy = SessionHelper.CurrentUserLoginID;
                oGLDataRecurringItemScheduleInfo.DateRevised = DateTime.Now;
                IGLDataRecItemSchedule oGLDataRecItemScheduleClient = RemotingHelper.GetGLDataRecItemScheduleObject();
                oGLDataRecItemScheduleClient.DeleteGLDataRecurringItemSchedule(oGLDataRecurringItemScheduleInfo, (short)ARTEnums.AccountAttribute.ReconciliationTemplate, dtGLDataParams, Helper.GetAppUserInfo());

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
        void ucGLDataRecurringScheduleItemsGrid_GridItemDataBound(object sender, GridItemEventArgs e)
        {
            ShowHideCurrencyColumn(ucGLDataRecurringScheduleItemsGrid.Grid);


            if (this.RecCategoryTypeID == (short)WebEnums.RecCategoryType.Amortizable_SupportingDetail_RecurringAmortizableSchedule)
            {

                ucGLDataRecurringScheduleItemsGrid.SetAmortizableGridHeaders(e);
            }
            else
            {
                ucGLDataRecurringScheduleItemsGrid.SetAccruableGridHeaders(e);
            }
            if (e.Item.ItemType == GridItemType.Header)
            {
                if (ucGLDataRecurringScheduleItemsGrid.IsOnPage)
                    SessionHelper.ShowGridFilterIcon((PageBase)this.Page, ucGLDataRecurringScheduleItemsGrid.Grid, e, ucGLDataRecurringScheduleItemsGrid.Grid.ClientID);
                else
                    SessionHelper.ShowGridFilterIcon((PopupPageBase)this.Page, ucGLDataRecurringScheduleItemsGrid.Grid, e, ucGLDataRecurringScheduleItemsGrid.Grid.ClientID);
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
                        //if (dr["PreviousGLDataRecurringItemScheduleID"] == null)
                        if (Convert.IsDBNull(dr["PreviousGLDataRecurringItemScheduleID"]))
                        {
                            //IDataImport oDataImportClient = RemotingHelper.GetDataImportObject();
                            //DataImportHdrInfo oDataImportHdrInfo = oDataImportClient.GetDataImportInfo(DataImportID, Helper.GetAppUserInfo());
                            //if (oDataImportHdrInfo != null)
                            //{
                            imgViewFile.Visible = true;
                            string url = "DownloadAttachment.aspx?" + QueryStringConstants.FILE_PATH + "=" + Server.UrlEncode(SharedHelper.GetDisplayFilePath(dr["PhysicalPath"].ToString()));
                            imgViewFile.OnClientClick = "document.location.href = '" + url + "';return false;";
                            //}
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

                int CreatedInRecPeriodID;
                int.TryParse(dr["CreatedInRecPeriodID"].ToString(), out    CreatedInRecPeriodID);
                bool IsForwardedItem = !(CreatedInRecPeriodID == SessionHelper.CurrentReconciliationPeriodID);
                long GLDataRecurringItemScheduleID;
                if (long.TryParse(dr["GLDataRecurringItemScheduleID"].ToString(), out GLDataRecurringItemScheduleID))
                {
                    hlShowItemInputForm.NavigateUrl = PopupHelper.GetJavascriptParameterListForEditRecItem(GLDataRecurringItemScheduleID, "OpenRadWindowForHyperlinkWithName", "EditItemAccrubleRecurring.aspx", EditMode.ToString(), IsForwardedItem, this.AccountID, this.GLDataID, this.RecCategoryTypeID, this.NetAccountID, this.IsSRA, RecCategoryID, hdIsRefreshData.ClientID, this.CurrentBCCY);//IsforwardedItem is not used here so redundant
                    hlAddRecItemComment.NavigateUrl = "javascript:OpenRadWindowForHyperlink('" + PopupHelper.getRecItemCommentPopupUrl(this.GLDataHdrInfo, GLDataRecurringItemScheduleID, (short)WebEnums.RecordType.ScheduleRecItem, this.AccountID, this.NetAccountID) + "', " + POPUP_HEIGHT + " , " + POPUP_WIDTH + ");";

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
                    deleteButton.CommandArgument = GLDataRecurringItemScheduleID.ToString();
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
                    CopyButton.CommandArgument = GLDataRecurringItemScheduleID.ToString();
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
                    CopyAndCloseButton.CommandArgument = GLDataRecurringItemScheduleID.ToString();
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
                _isCapabilityMultiCurrencyAccount = Helper.IsCapabilityActivatedForCurrentRecPeriod(ARTEnums.Capability.MultiCurrency);
                if (this.IsPostBack)
                {
                    this._GLDataRecurringItemScheduleInfoCollection = null;
                    _GLReconciliationItemInputRecordTypeID = (short)WebEnums.RecordType.GLReconciliationItemInput;
                    //IGLDataRecItemSchedule oGLDataRecItemScheduleClient = RemotingHelper.GetGLDataRecItemScheduleObject();
                    //this._GLDataRecurringItemScheduleInfoCollection = oGLDataRecItemScheduleClient.GetGLDataRecurringItemSchedule(this.GLDataID);
                    btnAdd.OnClientClick = PopupHelper.GetJavascriptParameterListForEditRecItem(null, "OpenRadWindowWithName", "EditItemAccrubleRecurring.aspx", QueryStringConstants.INSERT, false, this.AccountID, this.GLDataID, this.RecCategoryTypeID, this.NetAccountID, this.IsSRA, RecCategoryID, hdIsRefreshData.ClientID, this.CurrentBCCY);
                    btnAdd.Attributes.Add("onclick", "return false;");
                    //btnClose.OnClientClick = PopupHelper.GetJavascriptParameterList(null, "OpenRadWindow", "BulkCloseAccrubleRecurring.aspx", QueryStringConstants.INSERT, false, this.AccountID, this.GLDataID, this.RecCategoryTypeID, this.NetAccountID, this.IsSRA, RecCategoryID, hdIsRefreshData.ClientID);
                    btnClose.OnClientClick = PopupHelper.GetJavascriptParameterListForBulkClosepopup(null, "OpenRadWindowForHyperlinkWithName", "BulkCloseGLDataRecurringScheduleItems.aspx", QueryStringConstants.INSERT, false, this.AccountID, this.GLDataID, this.RecCategoryTypeID.Value, this.NetAccountID, this.IsSRA, RecCategoryID, hdIsRefreshData.ClientID, this.CurrentBCCY) + "; return false;";


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
            this._GLDataRecurringItemScheduleInfoCollection = null;
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
            if (GetGLDataRecurringItemScheduleInfoCollection != null && GetGLDataRecurringItemScheduleInfoCollection.Count > 0)
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
            List<GLDataRecurringItemScheduleInfo> GLDataRecurringItemScheduleIDCollection = ucGLDataRecurringScheduleItemsGrid.SelectedGLDataRecurringItemScheduleInfoCollection();
            if (GLDataRecurringItemScheduleIDCollection != null && GLDataRecurringItemScheduleIDCollection.Count > 0)
            {
                for (int i = 0; i < GLDataRecurringItemScheduleIDCollection.Count; i++)
                {
                    if (GLDataRecurringItemScheduleIDCollection[i].CreatedInRecPeriodID == SessionHelper.CurrentReconciliationPeriodID)
                    {
                        DataRow rowGLData = dtGLData.NewRow();
                        rowGLData["ID"] = GLDataRecurringItemScheduleIDCollection[i].GLDataRecurringItemScheduleID;
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
                GLDataRecurringItemScheduleInfo oGLDataRecurringItemScheduleInfo = new GLDataRecurringItemScheduleInfo();
                oGLDataRecurringItemScheduleInfo.GLDataRecurringItemScheduleID = 0;
                oGLDataRecurringItemScheduleInfo.GLDataID = this.GLDataID;
                oGLDataRecurringItemScheduleInfo.ReconciliationCategoryTypeID = this.RecCategoryTypeID;
                oGLDataRecurringItemScheduleInfo.RevisedBy = SessionHelper.CurrentUserLoginID;
                oGLDataRecurringItemScheduleInfo.DateRevised = DateTime.Now;
                IGLDataRecItemSchedule oGLDataRecItemScheduleClient = RemotingHelper.GetGLDataRecItemScheduleObject();
                oGLDataRecItemScheduleClient.DeleteGLDataRecurringItemSchedule(oGLDataRecurringItemScheduleInfo, (short)ARTEnums.AccountAttribute.ReconciliationTemplate, dtGLDataParams, Helper.GetAppUserInfo());
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

        private void CopyRecItem(bool CloseSourceRecItem, DataTable dtGLDataParams)
        {

            if (dtGLDataParams != null && dtGLDataParams.Rows.Count > 0)
            {

                GLDataRecurringItemScheduleInfo oGLDataRecurringItemScheduleInfo = new GLDataRecurringItemScheduleInfo();
                oGLDataRecurringItemScheduleInfo.GLDataRecurringItemScheduleID = 0;
                oGLDataRecurringItemScheduleInfo.GLDataID = this.GLDataID;
                oGLDataRecurringItemScheduleInfo.AddedBy = SessionHelper.CurrentUserLoginID;
                oGLDataRecurringItemScheduleInfo.DateAdded = DateTime.Now;
                oGLDataRecurringItemScheduleInfo.AddedByUserID = SessionHelper.CurrentUserID;

                IGLDataRecItemSchedule oGLDataRecItemScheduleClient = RemotingHelper.GetGLDataRecItemScheduleObject();
                List<AttachmentInfo> oAttachmentInfoList = oGLDataRecItemScheduleClient.CopyRecurringItemSchedule(oGLDataRecurringItemScheduleInfo, dtGLDataParams, SessionHelper.CurrentReconciliationPeriodID, CloseSourceRecItem, Helper.GetAppUserInfo());
                // Add Attechments
                if (oAttachmentInfoList.Count > 0)
                {
                    int AttachementNo = 1;
                    foreach (var item in oAttachmentInfoList)
                    {
                        string originalFileName = item.FileName;
                        //originalFileName="AccountDetails_514201451316PM.txt";
                        string originalFilePath = item.PhysicalPath;
                        string newFileName = Helper.GetNewFileName(originalFileName,AttachementNo);
                        string newFilePath = Helper.GetNewFilePath(newFileName);
                        SharedDataImportHelper.CopyFile(originalFilePath, newFilePath);
                        item.FileName = newFileName;
                        item.PhysicalPath = newFilePath;
                        item.Date = DateTime.Now;
                        item.UserID = SessionHelper.CurrentUserID;
                        item.IsActive = true;
                        AttachementNo += 1;
                    }
                    // Insert Attachments 
                    IAttachment oAttachmentClient = RemotingHelper.GetAttachmentObject();
                    oAttachmentClient.InsertAttachmentBulk(oAttachmentInfoList, oGLDataRecurringItemScheduleInfo.DateAdded, Helper.GetAppUserInfo());
                }

                RecHelper.RefreshRecForm(this);
            }

        }
        private DataTable GetGLDataRecItemForCopy()
        {
            DataTable dtGLData = new DataTable();
            dtGLData.Columns.Add("ID");
            List<GLDataRecurringItemScheduleInfo> GLDataRecurringItemScheduleIDCollection = ucGLDataRecurringScheduleItemsGrid.SelectedGLDataRecurringItemScheduleInfoCollection();
            if (GLDataRecurringItemScheduleIDCollection != null && GLDataRecurringItemScheduleIDCollection.Count > 0)
            {
                for (int i = 0; i < GLDataRecurringItemScheduleIDCollection.Count; i++)
                {
                    DataRow rowGLData = dtGLData.NewRow();
                    rowGLData["ID"] = GLDataRecurringItemScheduleIDCollection[i].GLDataRecurringItemScheduleID;
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
            glRecItemInputIdCollection = ucCloseGLDataRecurringScheduleItemsGrid.SelectedGLDataRecurringItemScheduleIDs();

            if (glRecItemInputIdCollection != null
                && glRecItemInputIdCollection.Count > 0)
            {
                IGLDataRecItemSchedule oGLDataRecItemScheduleClient = RemotingHelper.GetGLDataRecItemScheduleObject();
                oGLDataRecItemScheduleClient.UpdateGLDataRecurringItemScheduleCloseDate(this.GLDataID.Value, glRecItemInputIdCollection
                    , null, null, null, null, this.RecCategoryTypeID.Value, (short)ARTEnums.AccountAttribute.ReconciliationTemplate
                    , SessionHelper.GetCurrentUser().LoginID, DateTime.Now, SessionHelper.CurrentReconciliationPeriodID.Value, Helper.GetAppUserInfo());

                this._GLDataRecurringItemScheduleInfoCollection = null;
                EnableDisableBulkCloseButton();
                RecHelper.RefreshRecForm(this);
            }
        }
        public void PopulateGrids()
        {
            if (this.GLDataID == null || this.GLDataID == 0)
            {
                this.GetGLDataRecurringItemScheduleInfoCollection = null;
                ucGLDataRecurringScheduleItemsGrid.SetGLDataRecurringItemScheduleItemGridData(GetGLDataRecurringItemScheduleInfoCollection);
                ucCloseGLDataRecurringScheduleItemsGrid.SetGLDataRecurringItemScheduleItemGridData(GetGLDataRecurringItemScheduleInfoCollection);
            }
            else
            {
                ucGLDataRecurringScheduleItemsGrid.GLDataHdrInfo = this.GLDataHdrInfo;
                ucCloseGLDataRecurringScheduleItemsGrid.GLDataHdrInfo = this.GLDataHdrInfo;
                IGLDataRecItemSchedule oGLDataRecItemScheduleClient = RemotingHelper.GetGLDataRecItemScheduleObject();
                this.GetGLDataRecurringItemScheduleInfoCollection = oGLDataRecItemScheduleClient.GetGLDataRecurringItemSchedule(this.GLDataID, Helper.GetAppUserInfo());
                List<GLDataRecurringItemScheduleInfo> oOpenGLDataRecurringItemScheduleInfoCollection = this.GetGLDataRecurringItemScheduleInfoCollection.Where(recItem => recItem.CloseDate == null).ToList();
                ucGLDataRecurringScheduleItemsGrid.SetGLDataRecurringItemScheduleItemGridData(oOpenGLDataRecurringItemScheduleInfoCollection);
                ucGLDataRecurringScheduleItemsGrid.LoadGridData();
                this.EnableDisableBulkCloseButton();

                //if (oOpenGLDataRecurringItemScheduleInfoCollection == null || oOpenGLDataRecurringItemScheduleInfoCollection.Count == 0)
                //{
                //    btnDelete.Visible = false;
                //}
                //else
                //{
                //    btnDelete.Visible = true;
                //}

                List<GLDataRecurringItemScheduleInfo> oCloseGLDataRecurringItemScheduleInfoCollection = this.GetGLDataRecurringItemScheduleInfoCollection.Where(recItem => recItem.CloseDate != null).ToList();
                if (oCloseGLDataRecurringItemScheduleInfoCollection == null || oCloseGLDataRecurringItemScheduleInfoCollection.Count == 0)
                {
                    pnlClosedItems.Visible = false;
                }
                else
                {
                    pnlClosedItems.Visible = true;
                    if (EditMode == WebEnums.FormMode.ReadOnly)
                    {
                        ucCloseGLDataRecurringScheduleItemsGrid.ShowSelectCheckBoxColum = false;
                    }
                    ucCloseGLDataRecurringScheduleItemsGrid.SetGLDataRecurringItemScheduleItemGridData(oCloseGLDataRecurringItemScheduleInfoCollection);
                    ucCloseGLDataRecurringScheduleItemsGrid.LoadGridData();
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
            ucGLDataRecurringScheduleItemsGrid.ApplyFilterGLDataRecurringScheduleItemsGrid();
            ucCloseGLDataRecurringScheduleItemsGrid.ApplyFilterGLDataRecurringScheduleItemsGrid();
        }
    }
}