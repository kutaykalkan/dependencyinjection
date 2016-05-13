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
using SkyStem.ART.Client.Exception;
using SkyStem.ART.Client.IServices;
using SkyStem.ART.Web.Utility;
using SkyStem.ART.Client.Model;
using SkyStem.Library.Controls.TelerikWebControls.Data;
using Telerik.Web.UI;
using SkyStem.ART.Web.Data;
using SkyStem.Library.Controls.WebControls;
using System.Collections.Generic;
using SkyStem.ART.Web.Classes;

namespace SkyStem.ART.Web.UserControls
{
    public partial class UserControls_UnexplainedVariance : UserControlRecItemBase
    {

        public override bool DisableExportInPrint
        {
            set
            {
                DisableCommandItemForPrint(rgUnExpectedVariance);
            }
        }

        #region PrivateProperties
        bool isExportPDF;
        bool isExportExcel;
        public short _GLReconciliationItemInputRecordTypeID = 0;
        private List<GLDataUnexplainedVarianceInfo> _GLDataUnexplainedVarianceInfoCollection = null;
        private short _UserRole = 0;
        private short _RecPeriodStatus = 0;
        private bool? _IsExpandedValueForced = null;

        #endregion

        #region "Properties"

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


        private List<GLDataUnexplainedVarianceInfo> GetGLDataUnexplainedVarianceInfoCollection
        {
            get
            {
                if (this._GLDataUnexplainedVarianceInfoCollection == null)
                {
                    try
                    {
                        IUnexplainedVariance oUnExpectedVarianceClient = RemotingHelper.GetUnexplainedVarianceObject();
                        //this._GLDataUnexplainedVarianceInfoCollection = oUnExpectedVarianceClient.GetRecItem(this._AccountID.Value, SessionHelper.CurrentReconciliationPeriodID.Value, this._RecCategoryType.Value, this._GLReconciliationItemInputRecordTypeID, (short)ARTEnums.AccountAttribute.ReconciliationTemplate);
                        this._GLDataUnexplainedVarianceInfoCollection = oUnExpectedVarianceClient.GetGLDataUnexplainedVarianceInfoCollectionByGLDataID(this.GLDataID,Helper.GetAppUserInfo());
                    }
                    catch (Exception)
                    {
                    }

                }

                return this._GLDataUnexplainedVarianceInfoCollection;
            }
            set
            {
                this._GLDataUnexplainedVarianceInfoCollection = value;
            }
        }

        private bool _IsRegisterPDFAndExcelForPostback = true;
        public bool IsRegisterPDFAndExcelForPostback
        {
            get { return _IsRegisterPDFAndExcelForPostback; }
            set { _IsRegisterPDFAndExcelForPostback = value; }
        }

        ExImageButton ToggleControl;
        #endregion

        #region "Page Events"

        protected void Page_Init(object sender, EventArgs e)
        {
            //MasterPageBase ompage = (MasterPageBase)this.Master.Master;
            //ompage.ReconciliationPeriodChangedEventHandler += new EventHandler(ompage_ReconciliationPeriodChangedEventHandler);
            if (!this.IsPostBack)
            {
                isExportExcel = false;
                isExportPDF = false;
            }
            Helper.SetGridImageButtonProperties(this.rgUnExpectedVariance.MasterTableView.Columns);
            this.rgUnExpectedVariance.EntityNameLabelID = 1678;
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                isExportPDF = false;
                isExportExcel = false;
            }
            SetControlState();

            HandlePrintMode(trOpenItemsButtonRow, rgUnExpectedVariance);

            //********* Refresh Rec Period status *******************************************************
            //if (IsPostBack)
            //{
            //    RecHelper.ReloadRecPeriodsOnMasterPage(this);
            //}
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
                this._GLDataUnexplainedVarianceInfoCollection = null;
                updpnlMain.Visible = true;
                SetPrivateVariables();
                SetAttributesForAddButton();
                EnableDisableControlsForNonPreparersAndClosedPeriods();
                PopulateGrids();
                IsRefreshData = false;
                if (IsExpanded)
                    this.divMainContent.Visible = true;

                if (IsExpanded && !this.IsPrintMode)
                {
                    ToggleControl.ImageUrl = "~/App_Themes/SkyStemBlueBrown/Images/CollapseGlass.gif";
                }
            }
        }

        private void SetAttributesForAddButton()
        {
            string queryString = "?" + QueryStringConstants.ACCOUNT_ID + "=" + AccountID.Value
                                     + "&" + QueryStringConstants.GLDATA_ID + "=" + this.GLDataID.Value
                                     + "&" + QueryStringConstants.NETACCOUNT_ID + "=" + this.NetAccountID.Value
                                     + "&" + QueryStringConstants.MODE + "=" + QueryStringConstants.INSERT
                                     + "&" + QueryStringConstants.REC_CATEGORY_TYPE_ID + "=" + this.RecCategoryTypeID.Value
                                     + "&" + QueryStringConstants.REC_CATEGORY_ID + "=" + this.RecCategoryID.Value
                                     + "&" + "parentHiddenField=" + hdIsRefreshData.ClientID
                                    + "&" + QueryStringConstants.CURRENT_BCCY + "=" + this.CurrentBCCY;
            string popUPURL = "EditItemUnexplainedVariance.aspx" + queryString;

            btnAdd.Attributes.Add("onclick", "OpenRadWindowForHyperlink('" + popUPURL + "', " + WebConstants.POPUP_HEIGHT + " , " + WebConstants.POPUP_WIDTH + ");return false;");
        }

        private void SetPrivateVariables()
        {
            this._GLReconciliationItemInputRecordTypeID = (short)WebEnums.RecordType.GLReconciliationItemInput;
        }

        private void EnableDisableControlsForNonPreparersAndClosedPeriods()
        {
            this._UserRole = SessionHelper.CurrentRoleID.Value;
            this._RecPeriodStatus = (short)CurrentRecProcessStatus.Value;

            if (Helper.GetFormMode(WebEnums.ARTPages.UnexplainedVariance, this.GLDataHdrInfo) == WebEnums.FormMode.Edit)
            {
                btnAdd.Visible = true;

            }
            else
            {
                btnAdd.Visible = false;
            }
        }


        protected void Page_PreRender(object sender, EventArgs e)
        {
            //int lastPagePhraseID = Helper.GetPageTitlePhraseID(this.GetPreviousPageName());
            //if (lastPagePhraseID != -1)
            //    Helper.SetBreadcrumbs(this, 1071, 1187, lastPagePhraseID, 1678);
            //else
            //    Helper.SetBreadcrumbs(this, 1071, 1187, 1678);
        }
        #endregion

        #region "RADGrid Event Handling"
        protected void rgUnExpectedVariance_DeleteCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {

        }
        protected void rgUnExpectedVariance_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == Telerik.Web.UI.GridItemType.Item || e.Item.ItemType == Telerik.Web.UI.GridItemType.AlternatingItem)
                {
                    GLDataUnexplainedVarianceInfo oGLDataUnexplainedVarianceInfo = (GLDataUnexplainedVarianceInfo)e.Item.DataItem;

                    //if (oGLReconciliationItemInputInfo.IsForwardedItem.HasValue && oGLReconciliationItemInputInfo.IsForwardedItem.Value)
                    //{
                    //    int numberOfCells = e.Item.Cells.Count;

                    //    for (int index = 0; index < numberOfCells; index++)
                    //    {
                    //        e.Item.Cells[index].BackColor = System.Drawing.Color.CornflowerBlue;
                    //    }
                    //}
                    ExLabel lblComments = (ExLabel)e.Item.FindControl("lblComments");
                    if (isExportExcel || isExportPDF)
                        lblComments.Text = oGLDataUnexplainedVarianceInfo.Comments;
                    else
                        Helper.SetTextAndTooltipValue(lblComments, oGLDataUnexplainedVarianceInfo.Comments);
                    ExLabel lblAmountBaseCurrency = (ExLabel)e.Item.FindControl("lblAmountBaseCurrency");
                    ExLabel lblAmountReportingCurrency = (ExLabel)e.Item.FindControl("lblAmountReportingCurrency");

                    ExLabel lblAddedBy = (ExLabel)e.Item.FindControl("lblAddedBy");
                    ExLabel lblDateAdded = (ExLabel)e.Item.FindControl("lblDateAdded");
                    //BCCY Changes
                    //lblAmountBaseCurrency.Text = Helper.GetCurrencyValue(oGLDataUnexplainedVarianceInfo.AmountBaseCurrency, SessionHelper.BaseCurrencyCode);
                    lblAmountBaseCurrency.Text = Helper.GetCurrencyValue(oGLDataUnexplainedVarianceInfo.AmountBaseCurrency, this.CurrentBCCY);
                    lblAmountReportingCurrency.Text = Helper.GetDisplayReportingCurrencyValue(oGLDataUnexplainedVarianceInfo.AmountReportingCurrency);


                    lblAddedBy.Text = Helper.GetDisplayStringValue(oGLDataUnexplainedVarianceInfo.AddedByUserInfo.Name);
                    lblDateAdded.Text = Helper.GetDisplayDate(oGLDataUnexplainedVarianceInfo.DateAdded);

                    ExHyperLink imgbtnShowItemInputForm = (ExHyperLink)e.Item.FindControl("hlShowItemInputForm");

                    //Helper.GetFormMode()
                    System.Collections.Specialized.NameValueCollection queryStr = HttpUtility.ParseQueryString(string.Empty);

                    queryStr.Add(QueryStringConstants.ACCOUNT_ID, this.AccountID.Value.ToString());
                    queryStr.Add(QueryStringConstants.GL_RECONCILIATION_ITEM_INPUT_ID, oGLDataUnexplainedVarianceInfo.GLDataUnexplainedVarianceID.Value.ToString());
                    queryStr.Add(QueryStringConstants.GLDATA_ID, this.GLDataID.Value.ToString());
                    queryStr.Add(QueryStringConstants.NETACCOUNT_ID, this.NetAccountID.Value.ToString());
                    queryStr.Add(QueryStringConstants.REC_CATEGORY_TYPE_ID, this.RecCategoryTypeID.Value.ToString());
                    queryStr.Add(QueryStringConstants.REC_CATEGORY_ID, this.RecCategoryID.Value.ToString());
                    queryStr.Add(QueryStringConstants.PARENT_HIDDEN_FIELD, hdIsRefreshData.ClientID.ToString());
                    queryStr.Add(QueryStringConstants.CURRENT_BCCY, this.CurrentBCCY);
                    if (Helper.GetFormMode(WebEnums.ARTPages.UnexplainedVariance, this.GLDataHdrInfo) == WebEnums.FormMode.Edit)
                    {
                        queryStr.Add(QueryStringConstants.MODE, QueryStringConstants.EDIT);
                        Helper.SetImageURLForViewVersusEdit(WebEnums.FormMode.Edit, imgbtnShowItemInputForm);
                    }
                    else
                    {
                        queryStr.Add(QueryStringConstants.MODE, QueryStringConstants.READ_ONLY);
                        Helper.SetImageURLForViewVersusEdit(WebEnums.FormMode.ReadOnly, imgbtnShowItemInputForm);
                    }
                    string popUPURL = "EditItemUnexplainedVariance.aspx" + "?" + queryStr.ToString();
                    imgbtnShowItemInputForm.NavigateUrl = "javascript:OpenRadWindowForHyperlink('" + popUPURL + "', " + WebConstants.POPUP_HEIGHT + " , " + WebConstants.POPUP_WIDTH + ");";

                    //if (Helper.GetFormMode(WebEnums.ARTPages.UnexplainedVariance, this.GLDataHdrInfo) == WebEnums.FormMode.Edit)
                    //{
                    //    string queryString = "?" + QueryStringConstants.ACCOUNT_ID + "=" + AccountID.Value
                    //       + "&" + QueryStringConstants.GL_RECONCILIATION_ITEM_INPUT_ID + "=" + oGLDataUnexplainedVarianceInfo.GLDataUnexplainedVarianceID.Value
                    //       + "&" + QueryStringConstants.GLDATA_ID + "=" + this.GLDataID.Value
                    //       + "&" + QueryStringConstants.NETACCOUNT_ID + "=" + this.NetAccountID.Value
                    //       + "&" + QueryStringConstants.MODE + "=" + QueryStringConstants.EDIT
                    //       + "&" + QueryStringConstants.REC_CATEGORY_TYPE_ID + "=" + this.RecCategoryTypeID.Value
                    //       + "&" + QueryStringConstants.REC_CATEGORY_ID + "=" + this.RecCategoryID.Value
                    //       + "&" + "parentHiddenField=" + hdIsRefreshData.ClientID
                    //       + "&" + QueryStringConstants.CURRENT_BCCY + "=" + this.CurrentBCCY;

                    //    string popUPURL = "EditItemUnexplainedVariance.aspx" + queryString;
                    //    imgbtnShowItemInputForm.NavigateUrl = "javascript:OpenRadWindowForHyperlink('" + popUPURL + "', " + WebConstants.POPUP_HEIGHT + " , " + WebConstants.POPUP_WIDTH + ");";

                    //    Helper.SetImageURLForViewVersusEdit(WebEnums.FormMode.Edit, imgbtnShowItemInputForm);

                    //}
                    //else
                    //{
                    //    string queryString = "?" + QueryStringConstants.ACCOUNT_ID + "=" + AccountID.Value
                    //           + "&" + QueryStringConstants.GL_RECONCILIATION_ITEM_INPUT_ID + "=" + oGLDataUnexplainedVarianceInfo.GLDataUnexplainedVarianceID.Value
                    //           + "&" + QueryStringConstants.GLDATA_ID + "=" + this.GLDataID.Value
                    //           + "&" + QueryStringConstants.NETACCOUNT_ID + "=" + this.NetAccountID.Value
                    //           + "&" + QueryStringConstants.MODE + "=" + QueryStringConstants.READ_ONLY
                    //           + "&" + QueryStringConstants.REC_CATEGORY_TYPE_ID + "=" + this.RecCategoryTypeID.Value
                    //           + "&" + QueryStringConstants.REC_CATEGORY_ID + "=" + this.RecCategoryID.Value
                    //           + "&" + "parentHiddenField=" + hdIsRefreshData.ClientID
                    //            + "&" + QueryStringConstants.CURRENT_BCCY + "=" + this.CurrentBCCY;

                    //    string popUPURL = "EditItemUnexplainedVariance.aspx" + queryString;
                    //    imgbtnShowItemInputForm.NavigateUrl = "javascript:OpenRadWindowForHyperlink('" + popUPURL + "', " + WebConstants.POPUP_HEIGHT + " , " + WebConstants.POPUP_WIDTH + ");";

                    //    Helper.SetImageURLForViewVersusEdit(WebEnums.FormMode.ReadOnly, imgbtnShowItemInputForm);
                    //}
                    //TODO: make single sentence for above code ( mode difference)


                    if ((e.Item as GridDataItem)["DeleteColumn"] != null)
                    {
                        ImageButton deleteButton = (ImageButton)(e.Item as GridDataItem)["DeleteColumn"].Controls[0];

                        if (Helper.GetFormMode(WebEnums.ARTPages.UnexplainedVariance, this.GLDataHdrInfo) == WebEnums.FormMode.Edit)
                        {
                            //if (oGLReconciliationItemInputInfo.IsForwardedItem.HasValue && oGLReconciliationItemInputInfo.IsForwardedItem.Value)
                            //{
                            //    deleteButton.Visible = false;
                            //}
                            //ie  deleteButton.Visible = true;

                        }
                        else
                        {
                            deleteButton.Visible = false;
                        }
                        deleteButton.CommandArgument = oGLDataUnexplainedVarianceInfo.GLDataUnexplainedVarianceID.ToString();
                    }

                }
            }
            catch (Exception)
            {
            }
        }

        protected void rgUnExpectedVariance_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            this.rgUnExpectedVariance.DataSource = this.GetGLDataUnexplainedVarianceInfoCollection;
        }


        protected void rgUnExpectedVariance_ItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            try
            {


                if (e.CommandName == "Delete")
                {
                    GLDataUnexplainedVarianceInfo oGLReconciliationItemInputInfo = new GLDataUnexplainedVarianceInfo();
                    oGLReconciliationItemInputInfo.GLDataUnexplainedVarianceID = Convert.ToInt64(e.CommandArgument);
                    IUnexplainedVariance oUnExpectedVarianceClient = RemotingHelper.GetUnexplainedVarianceObject();
                    //oUnExpectedVarianceClient.DeleteRecInputItem(oGLReconciliationItemInputInfo, (short)ARTEnums.AccountAttribute.ReconciliationTemplate);
                    oUnExpectedVarianceClient.DeleteGLDataUnexplainedVariance(oGLReconciliationItemInputInfo.GLDataUnexplainedVarianceID, Helper.GetAppUserInfo());

                    this._GLDataUnexplainedVarianceInfoCollection = null;

                    //List<GLDataUnexplainedVarianceInfo> oGLReconciliationItemInputInfoCollection = this.GetGLDataUnexplainedVarianceInfoCollection.Where(recItem => string.IsNullOrEmpty(recItem.CloseComments) && recItem.CloseDate == null && recItem.JournalEntryRef == null).ToList();
                    //rgUnExpectedVariance.DataSource = oGLReconciliationItemInputInfoCollection;
                    //rgUnExpectedVariance.DataBind();

                    //CalculateAndDisplaySum();

                    SetAddButtonVisibility(true);
                    RecHelper.RefreshRecForm(this);
                }

                if (e.CommandName == TelerikConstants.GridExportToPDFCommandName)
                {

                    isExportPDF = true;
                    //rgGLAdjustments.MasterTableView.Columns.FindByUniqueName("CheckboxSelectColumn").Visible = false;
                    rgUnExpectedVariance.MasterTableView.Columns.FindByUniqueName("DeleteColumn").Visible = false;
                    rgUnExpectedVariance.MasterTableView.Columns.FindByUniqueName("ShowInputForm").Visible = false;
                    GridHelper.ExportGridToPDF(rgUnExpectedVariance, PopupHelper.GetPageTitle(this.RecCategoryID.Value, this.RecCategoryTypeID.Value));

                }
                if (e.CommandName == TelerikConstants.GridExportToExcelCommandName)
                {

                    isExportExcel = true;
                    //rgGLAdjustments.MasterTableView.Columns.FindByUniqueName("CheckboxSelectColumn").Visible = false;
                    rgUnExpectedVariance.MasterTableView.Columns.FindByUniqueName("DeleteColumn").Visible = false;
                    rgUnExpectedVariance.MasterTableView.Columns.FindByUniqueName("ShowInputForm").Visible = false;
                    GridHelper.ExportGridToExcel(rgUnExpectedVariance, PopupHelper.GetPageTitle(this.RecCategoryID.Value, this.RecCategoryTypeID.Value));
                }

            }
            catch (Exception)
            {
            }
        }

        #endregion

        public void ompage_ReconciliationPeriodChangedEventHandler(object sender, EventArgs e)
        {
            try
            {
                this._GLDataUnexplainedVarianceInfoCollection = null;
                if (IsPostBack)
                {
                    IsRefreshData = true;
                    if (IsExpanded)
                    {
                        LoadData();
                    }
                }

            }
            catch (ARTException ex)
            {
                Helper.HidePanel(updpnlMain, ex);
                //Helper.ShowErrorMessage(this, ex);
            }
            catch (Exception)
            {
            }
        }

        public string GetMenuKey()
        {
            return "AccountViewer";
        }

        private string GetPreviousPageName()
        {
            string PName = "";
            if (Request.UrlReferrer != null)
            {
                PName = Request.UrlReferrer.Segments[Request.UrlReferrer.Segments.Length - 1];
            }
            return PName;
        }

        private void PopulateGrids()
        {
            if (this.GLDataID == null || this.GLDataID == 0)
            {
                this.GetGLDataUnexplainedVarianceInfoCollection = null;
                this.rgUnExpectedVariance.DataSource = new List<GLDataUnexplainedVarianceInfo>();
                rgUnExpectedVariance.DataBind();
                //this.rgGLAdjustmentCloseditems.DataSource = new List<GLDataRecItemInfo>();
                //this.rgGLAdjustmentCloseditems.DataBind();
            }
            else
            {

                rgUnExpectedVariance.EntityNameText = PopupHelper.GetPageTitle(this.RecCategoryID.Value, this.RecCategoryTypeID.Value);
                rgUnExpectedVariance.DataSource = this.GetGLDataUnexplainedVarianceInfoCollection;
                rgUnExpectedVariance.DataBind();
                if (this.GetGLDataUnexplainedVarianceInfoCollection.Count >= 1)
                {
                    SetAddButtonVisibility(false);
                }
            }
        }

        protected void rgUnExpectedVariance_ItemCreated(object sender, GridItemEventArgs e)
        {
            // Register PDF / Excel for Postback
            if (_IsRegisterPDFAndExcelForPostback)
                GridHelper.RegisterPDFAndExcelForPostback(e, isExportPDF, isExportExcel, this.Page);
            //GridHelper.RegisterPDFAndExcelForPostback(e, isExportPDF, isExportExcel, this.Page);
            GridHelper.SetStylesForExportGrid(e, isExportPDF, isExportExcel);
        }

        protected void SetAddButtonVisibility(bool isVisible)
        {
            btnAdd.Visible = isVisible;

        }

        public override void ExpandCollapse()
        {

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
                if (IsPostBack)
                {
                    if (IsExpanded)
                        ToggleControl.ImageUrl = "~/App_Themes/SkyStemBlueBrown/Images/CollapseGlass.gif";
                    else
                        ToggleControl.ImageUrl = "~/App_Themes/SkyStemBlueBrown/Images/ExpandGlass.gif";
                }
            }

        }

    }
}