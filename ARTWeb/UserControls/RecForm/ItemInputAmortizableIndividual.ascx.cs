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

    public partial class UserControls_ItemInputAmortizableIndividual : UserControlRecItemBase
    {
        #region PrivateProperties

        short _GLReconciliationItemInputRecordTypeID = 0;
        private List<GLDataRecItemInfo> _GLRecItemInfoCollection = null;
        private short _UserRole = 0;
        private short _RecPeriodStatus = 0;
        private decimal? _BaseCurrencyTotal = 0;
        private decimal? _ReportingCurrencyTotal = 0;
        static bool _isCapabilityMultiCurrencyAccount = false;
        private bool? _IsExpandedValueForced = null;
        ExImageButton ToggleControl;
        bool isExportPDF;
        bool isExportExcel;

        public override bool DisableExportInPrint
        {
            set
            {
                DisableCommandItemForPrint(rgGLAdjustments);
                DisableCommandItemForPrint(rgGLAdjustmentCloseditems);
            }
        }
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
                if (this._GLRecItemInfoCollection == null)
                {
                    try
                    {
                        IGLDataRecItem oGLRecItemInput = RemotingHelper.GetGLDataRecItemObject();
                        this._GLRecItemInfoCollection = oGLRecItemInput.GetRecItem(this.GLDataID.Value, SessionHelper.CurrentReconciliationPeriodID.Value, this.RecCategoryTypeID.Value, this._GLReconciliationItemInputRecordTypeID, (short)ARTEnums.AccountAttribute.ReconciliationTemplate, Helper.GetAppUserInfo());
                    }
                    catch (ARTException)
                    {
                    }
                    catch (Exception)
                    {
                    }

                }

                return this._GLRecItemInfoCollection;
            }
            set
            {
                this._GLRecItemInfoCollection = value;
            }
        }

        private bool _IsRegisterPDFAndExcelForPostback = true;
        public bool IsRegisterPDFAndExcelForPostback
        {
            get { return _IsRegisterPDFAndExcelForPostback; }
            set { _IsRegisterPDFAndExcelForPostback = value; }
        }
        #endregion
        #region "Page Events"

        protected void Page_Init(object sender, EventArgs e)
        {
            //MasterPageBase ompage = (MasterPageBase)this.Master.Master;
            //ompage.ReconciliationPeriodChangedEventHandler += new EventHandler(ompage_ReconciliationPeriodChangedEventHandler);

            Helper.SetGridImageButtonProperties(this.rgGLAdjustments.MasterTableView.Columns);
            this.rgGLAdjustments.EntityNameLabelID = 1455;
            this.rgGLAdjustmentCloseditems.EntityNameLabelID = 1455;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                isExportPDF = false;
                isExportExcel = false;
            }
            SetControlState();

            HandlePrintMode(trOpenItemsButtonRow, trClosedItemsButtonRow, rgGLAdjustments);
            HandlePrintMode(trOpenItemsButtonRow, trClosedItemsButtonRow, rgGLAdjustmentCloseditems);

            //********* Refresh Rec Period status *******************************************************
            if (IsPostBack)
            {
                RecHelper.ReloadRecPeriodsOnMasterPage(this);
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
                try
                {
                    _isCapabilityMultiCurrencyAccount = Helper.IsCapabilityActivatedForCurrentRecPeriod(ARTEnums.Capability.MultiCurrency);

                    //Helper.HideMessage(this);
                    if (!this.IsPostBack)
                    {
                        isExportPDF = false;
                        isExportExcel = false;
                        //ViewState[ViewStateConstants.PATH_AND_QUERY] = Request.UrlReferrer.PathAndQuery;
                    }

                    updpnlMain.Visible = true;
                    this._GLRecItemInfoCollection = null;
                    //Helper.SetPageTitle(this, 1455);

                    SetPrivateVariables();
                    btnAdd.OnClientClick = PopupHelper.GetJavascriptParameterListForEditRecItem(null, "OpenRadWindowWithName", "EditRecItemInputs.aspx", QueryStringConstants.INSERT, false, this.AccountID, this.GLDataID, this.RecCategoryTypeID, this.NetAccountID, this.IsSRA, RecCategoryID, hdIsRefreshData.ClientID, this.CurrentBCCY);
                    btnAdd.Attributes.Add("onclick", "return false;");

                    btnClose.OnClientClick = PopupHelper.GetJavascriptParameterList(null, "OpenRadWindow", "BulkCloseAmortizableIndividual.aspx", QueryStringConstants.INSERT, false, this.AccountID, this.GLDataID, this.RecCategoryTypeID, this.NetAccountID, this.IsSRA, RecCategoryID, hdIsRefreshData.ClientID);
                    this.EnableDisableControlsForNonPreparersAndClosedPeriods();
                    this.EnableDisableBulkCloseButton();
                    PopulateGrids();
                    CalculateAndDisplaySum();
                    IsRefreshData = false;
                    if (IsExpanded)
                        this.divMainContent.Visible = true;

                    if (IsExpanded && !this.IsPrintMode)
                    {
                        ToggleControl.ImageUrl = "~/App_Themes/SkyStemBlueBrown/Images/CollapseGlass.gif";
                    }
                }
                catch (ARTException ex)
                {
                    if (ex.ExceptionPhraseID == 5000116 || ex.ExceptionPhraseID == 5000120)
                        updpnlMain.Visible = false;
                    //Helper.ShowErrorMessage(this, ex);
                }
                catch (Exception)
                {
                }
            }
        }
        private void SetPrivateVariables()
        {
            this._GLReconciliationItemInputRecordTypeID = (short)WebEnums.RecordType.GLReconciliationItemInput;
        }

        private void EnableDisableBulkCloseButton()
        {

            if (this.GetGLRecItemInfoCollection == null)
                btnClose.Enabled = false;
            else
            {
                int numberOfOldOpenItems = (from recItem in this.GetGLRecItemInfoCollection
                                            where recItem.CloseDate == null
                                            select recItem).Count();

                if (numberOfOldOpenItems > 0)
                    btnClose.Enabled = true;
                else
                    btnClose.Enabled = false;
            }
        }

        private void EnableDisableControlsForNonPreparersAndClosedPeriods()
        {
            this._UserRole = SessionHelper.CurrentRoleID.Value;
            this._RecPeriodStatus = (short)CurrentRecProcessStatus.Value;

            if (Helper.GetFormMode(WebEnums.ARTPages.ItemInputAmortizableIndividual, this.GLDataHdrInfo) == WebEnums.FormMode.Edit)
            {
                btnAdd.Visible = true;
                btnClose.Visible = true;
                btnReopen.Visible = true;
                rgGLAdjustmentCloseditems.Columns[0].Visible = true;
            }
            else
            {
                btnAdd.Visible = false;
                btnClose.Visible = false;
                btnReopen.Visible = false;
                rgGLAdjustmentCloseditems.Columns[0].Visible = false;
            }
        }

        private void CalculateAndDisplaySum()
        {
            this._BaseCurrencyTotal = (from recItem in this.GetGLRecItemInfoCollection
                                       where recItem.CloseDate == null
                                       select recItem.AmountBaseCurrency).Sum();

            this._ReportingCurrencyTotal = (from recItem in this.GetGLRecItemInfoCollection
                                            where recItem.CloseDate == null
                                            select recItem.AmountReportingCurrency).Sum();

            //lblBaseCurrency.Text = Helper.GetLabelIDValue(1382) + " " + Helper.GetDisplayRecCategoryTypeID(this.RecCategoryTypeID.Value) + " " + Helper.GetLabelIDValue(1769) + ":" + " " + this.CurrentBCCY + " " + Helper.GetDisplayDecimalValue(baseCurrencyTotal);
            //lblReportingCurrency.Text = Helper.GetLabelIDValue(1382) + " " + Helper.GetDisplayRecCategoryTypeID(this.RecCategoryTypeID.Value) + " " + Helper.GetLabelIDValue(1770) + ":" + " " + SessionHelper.ReportingCurrencyCode + " " + Helper.GetDisplayDecimalValue(reportingCurrencyTotal);
        }
        protected void Page_PreRender(object sender, EventArgs e)
        {

        }
        #endregion

        #region "RADGrid Event Handling"
        protected void rgGLAdjustments_DeleteCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {

        }
        protected void rgGLAdjustments_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            try
            {
                CalculateAndDisplaySum();
                GLDataRecItemInfo oGLReconciliationItemInputInfo = (GLDataRecItemInfo)e.Item.DataItem;
                if (e.Item.ItemType == GridItemType.Header)
                {
                    ////((LinkButton)(e.Item as GridHeaderItem)["AmountReportingCurrency"].Controls[0]).Text = Helper.GetLabelIDValue(1674) + " (" + SessionHelper.ReportingCurrencyCode + ")";
                    ////((LinkButton)(e.Item as GridHeaderItem)["AmountBaseCurrency"].Controls[0]).Text = Helper.GetLabelIDValue(1673) + " (" + this.CurrentBCCY + ")";
                    //***************Above code commented and replaced by below code to handle the export to pdf of Grid
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

                    if (NetAccountID > 0)
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

                    //******************************************************************************************************************************************

                    if (!_isCapabilityMultiCurrencyAccount)
                    {
                        // If Multi-Currency is Off, LCCY Code is same as BCCY / RCCY
                        ////((LinkButton)(e.Item as GridHeaderItem)["Amount"].Controls[0]).Text = Helper.GetLabelIDValue(1675) + " (" + this.CurrentBCCY + ")";
                        //***************Above code commented and replaced by below code to handle the export to pdf of Grid
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
                        //******************************************************************************************************************************************


                    }
                }

                if (e.Item.ItemType == Telerik.Web.UI.GridItemType.Item || e.Item.ItemType == Telerik.Web.UI.GridItemType.AlternatingItem)
                {


                    GridHelper.SetCSSClassForForwardedItems(e, oGLReconciliationItemInputInfo.IsForwardedItem);

                    ExLabel lblDescription = (ExLabel)e.Item.FindControl("lblDescription");
                    if (isExportExcel || isExportPDF)
                        lblDescription.Text = oGLReconciliationItemInputInfo.Comments;
                    else
                        Helper.SetTextAndTooltipValue(lblDescription, oGLReconciliationItemInputInfo.Comments);
                    ExLabel lblAmount = (ExLabel)e.Item.FindControl("lblAmount");
                    lblAmount.Text = Helper.GetDisplayCurrencyValue(oGLReconciliationItemInputInfo.LocalCurrencyCode, oGLReconciliationItemInputInfo.Amount);

                    ExLabel lblOpenDate = (ExLabel)e.Item.FindControl("lblOpenDate");
                    lblOpenDate.Text = Helper.GetDisplayDate(oGLReconciliationItemInputInfo.OpenDate);
                    ExLabel lblAging = (ExLabel)e.Item.FindControl("lblAging");
                    lblAging.Text = Helper.GetDisplayIntegerValue(Helper.GetDaysBetweenDateRanges(oGLReconciliationItemInputInfo.OpenDate, DateTime.Now));

                    ExLabel lblAttachmentCount = (ExLabel)e.Item.FindControl("lblAttachmentCount");
                    lblAttachmentCount.Text = Helper.GetDisplayIntegerValue(oGLReconciliationItemInputInfo.AttachmentCount);

                    ExLabel lblRecItemNumber = (ExLabel)e.Item.FindControl("lblRecItemNumber");
                    lblRecItemNumber.Text = Helper.GetDisplayStringValue(oGLReconciliationItemInputInfo.RecItemNumber);

                    //************* MatchSetRefNo********************************************************
                    RecHelper.SetMatchSetRefNumberUrlForGLDataRecItem(e, oGLReconciliationItemInputInfo, AccountID, NetAccountID, GLDataID);
                    //************************************************************************************

                    // ShowHide the Excel Button if  the Rec Item is imported through file (and not added manually)
                    ExImageButton imgViewFile = (ExImageButton)e.Item.FindControl("imgViewFile");
                    if (oGLReconciliationItemInputInfo.DataImportID != null)
                    {
                        if (oGLReconciliationItemInputInfo.PreviousGLDataRecItemID == null)
                        {
                            IDataImport oDataImportClient = RemotingHelper.GetDataImportObject();
                            DataImportHdrInfo oDataImportHdrInfo = oDataImportClient.GetDataImportInfo(oGLReconciliationItemInputInfo.DataImportID, Helper.GetAppUserInfo());
                            if (oDataImportHdrInfo != null)
                            {
                                imgViewFile.Visible = true;
                                string url = "DownloadAttachment.aspx?" + QueryStringConstants.FILE_PATH + "=" + Server.UrlEncode(oDataImportHdrInfo.PhysicalPath);
                                imgViewFile.OnClientClick = "document.location.href = '" + url + "';return false;";
                            }
                        }
                    }
                    ExHyperLink hlShowItemInputForm = (ExHyperLink)e.Item.FindControl("hlShowItemInputForm");


                    string mode = "";
                    bool isForwardedItem = false;
                    if (Helper.GetFormMode(WebEnums.ARTPages.ItemInputAmortizableIndividual, this.GLDataHdrInfo) == WebEnums.FormMode.Edit)
                    {
                        Helper.SetImageURLForViewVersusEdit(WebEnums.FormMode.Edit, hlShowItemInputForm);
                        mode = QueryStringConstants.EDIT;
                        if (oGLReconciliationItemInputInfo.IsForwardedItem.HasValue && oGLReconciliationItemInputInfo.IsForwardedItem.Value)
                        {
                            isForwardedItem = true;
                        }
                        else
                        {
                            isForwardedItem = false;
                        }
                    }
                    else
                    {
                        mode = QueryStringConstants.READ_ONLY;
                        isForwardedItem = false;
                        Helper.SetImageURLForViewVersusEdit(WebEnums.FormMode.ReadOnly, hlShowItemInputForm);
                    }
                    hlShowItemInputForm.NavigateUrl = PopupHelper.GetJavascriptParameterListForEditRecItem
                        (oGLReconciliationItemInputInfo.GLDataRecItemID
                        , "OpenRadWindowForHyperlinkWithName"
                        , "EditRecItemInputs.aspx"
                        , mode
                        , isForwardedItem
                        , this.AccountID
                        , this.GLDataID
                        , this.RecCategoryTypeID
                        , this.NetAccountID
                        , this.IsSRA
                        , RecCategoryID
                        , hdIsRefreshData.ClientID
                        , this.CurrentBCCY);


                    if ((e.Item as GridDataItem)["DeleteColumn"] != null)
                    {
                        ImageButton deleteButton = (ImageButton)(e.Item as GridDataItem)["DeleteColumn"].Controls[0];
                        CheckBox chkSelectItem = (CheckBox)(e.Item as GridDataItem)["CheckboxSelectColumn"].Controls[0];
                        if (Helper.GetFormMode(WebEnums.ARTPages.ItemInputAmortizableIndividual, this.GLDataHdrInfo) == WebEnums.FormMode.Edit)
                        {
                            if (oGLReconciliationItemInputInfo.IsForwardedItem.HasValue && oGLReconciliationItemInputInfo.IsForwardedItem.Value)
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
                        deleteButton.CommandArgument = oGLReconciliationItemInputInfo.GLDataRecItemID.ToString();
                    }
                }

                if (e.Item.ItemType == GridItemType.Footer)
                {
                    ExLabel lblBaseCurrencyTotal = (ExLabel)e.Item.FindControl("lblBaseCurrencyTotal");
                    ExLabel lblReportingCurrencyTotal = (ExLabel)e.Item.FindControl("lblReportingCurrencyTotal");
                    lblBaseCurrencyTotal.Text = Helper.GetDisplayDecimalValue(this._BaseCurrencyTotal);
                    lblReportingCurrencyTotal.Text = Helper.GetDisplayDecimalValue(this._ReportingCurrencyTotal);
                }
            }
            catch (Exception)
            {
            }
        }

        protected void rgGLAdjustments_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            List<GLDataRecItemInfo> oGLReconciliationItemInputInfoCollection = this.GetGLRecItemInfoCollection.Where(recItem => recItem.CloseDate == null).ToList();
            this.rgGLAdjustments.DataSource = oGLReconciliationItemInputInfoCollection;
        }

        protected void rgGLAdjustmentCloseditems_ItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            try
            {
                CalculateAndDisplaySum();


                if (e.CommandName == TelerikConstants.GridExportToPDFCommandName)
                {

                    isExportPDF = true;

                    rgGLAdjustmentCloseditems.MasterTableView.Columns.FindByUniqueName("ShowInputForm").Visible = false;
                    rgGLAdjustmentCloseditems.MasterTableView.Columns.FindByUniqueName("CheckboxSelectColumn").Visible = false;
                    GridHelper.ExportGridToPDF(rgGLAdjustmentCloseditems, PopupHelper.GetPageTitle(this.RecCategoryID.Value, this.RecCategoryTypeID.Value));
                }
                if (e.CommandName == TelerikConstants.GridExportToExcelCommandName)
                {

                    isExportExcel = true;

                    rgGLAdjustmentCloseditems.MasterTableView.Columns.FindByUniqueName("ShowInputForm").Visible = false;
                    rgGLAdjustmentCloseditems.MasterTableView.Columns.FindByUniqueName("CheckboxSelectColumn").Visible = false;
                    GridHelper.ExportGridToExcel(rgGLAdjustmentCloseditems, PopupHelper.GetPageTitle(this.RecCategoryID.Value, this.RecCategoryTypeID.Value));
                }



            }
            catch (Exception)
            {
            }
        }

        protected void rgGLAdjustmentCloseditems_ItemCreated(object sender, GridItemEventArgs e)
        {
            // Register PDF / Excel for Postback
            if (_IsRegisterPDFAndExcelForPostback)
                GridHelper.RegisterPDFAndExcelForPostback(e, isExportPDF, isExportExcel, this.Page);
            //GridHelper.RegisterPDFAndExcelForPostback(e, isExportPDF, isExportExcel, this.Page);
            GridHelper.SetStylesForExportGrid(e, isExportPDF, isExportExcel);
        }

        protected void rgGLAdjustmentCloseditems_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            try
            {
                CalculateAndDisplaySum();
                GLDataRecItemInfo oGLReconciliationItemInputInfo = (GLDataRecItemInfo)e.Item.DataItem;
                if (e.Item.ItemType == GridItemType.Header)
                {
                    ////((LinkButton)(e.Item as GridHeaderItem)["AmountReportingCurrency"].Controls[0]).Text = Helper.GetLabelIDValue(1674) + " (" + SessionHelper.ReportingCurrencyCode + ")";
                    ////((LinkButton)(e.Item as GridHeaderItem)["AmountBaseCurrency"].Controls[0]).Text = Helper.GetLabelIDValue(1673) + " (" + this.CurrentBCCY + ")";
                    //***************Above code commented and replaced by below code to handle the export to pdf of Grid
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

                    if (NetAccountID > 0)
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
                            ((LinkButton)oControlAmountBaseCurrency).Text = Helper.GetLabelIDValue(1673) + " (" + this.CurrentBCCY + ")";

                        }
                        else
                        {
                            if (oControlAmountBaseCurrency is LiteralControl)
                            {
                                ((LiteralControl)oControlAmountBaseCurrency).Text = Helper.GetLabelIDValue(1673) + " (" + this.CurrentBCCY + ")";

                            }
                        }
                    }


                    //******************************************************************************************************************************************

                    if (!_isCapabilityMultiCurrencyAccount)
                    {
                        // If Multi-Currency is Off, LCCY Code is same as BCCY / RCCY
                        ////((LinkButton)(e.Item as GridHeaderItem)["Amount"].Controls[0]).Text = Helper.GetLabelIDValue(1675) + " (" + this.CurrentBCCY + ")";
                        //***************Above code commented and replaced by below code to handle the export to pdf of Grid
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
                        //******************************************************************************************************************************************


                    }
                }

                if (e.Item.ItemType == Telerik.Web.UI.GridItemType.Item || e.Item.ItemType == Telerik.Web.UI.GridItemType.AlternatingItem)
                {

                    ExLabel lblAmount = (ExLabel)e.Item.FindControl("lblAmount");
                    lblAmount.Text = Helper.GetDisplayCurrencyValue(oGLReconciliationItemInputInfo.LocalCurrencyCode, oGLReconciliationItemInputInfo.Amount);

                    ExLabel lblOpenDate = (ExLabel)e.Item.FindControl("lblOpenDate");
                    lblOpenDate.Text = Helper.GetDisplayDate(oGLReconciliationItemInputInfo.OpenDate);

                    ExLabel lblCloseDate = (ExLabel)e.Item.FindControl("lblCloseDate");
                    lblCloseDate.Text = Helper.GetDisplayDate(oGLReconciliationItemInputInfo.CloseDate);

                    ExLabel lblAging = (ExLabel)e.Item.FindControl("lblAging");
                    lblAging.Text = Helper.GetDisplayIntegerValue(Helper.GetDaysBetweenDateRanges(oGLReconciliationItemInputInfo.OpenDate, oGLReconciliationItemInputInfo.CloseDate));

                    ExLabel lblAttachmentCount = (ExLabel)e.Item.FindControl("lblAttachmentCount");
                    lblAttachmentCount.Text = Helper.GetDisplayIntegerValue(oGLReconciliationItemInputInfo.AttachmentCount);


                    ExLabel lblRecItemNumber = (ExLabel)e.Item.FindControl("lblRecItemNumber");
                    lblRecItemNumber.Text = Helper.GetDisplayStringValue(oGLReconciliationItemInputInfo.RecItemNumber);

                    //************* MatchSetRefNo********************************************************
                    RecHelper.SetMatchSetRefNumberUrlForGLDataRecItem(e, oGLReconciliationItemInputInfo, AccountID, NetAccountID, GLDataID);
                    //************************************************************************************

                    // ShowHide the Excel Button if  the Rec Item is imported through file (and not added manually)
                    ExImageButton imgViewFile = (ExImageButton)e.Item.FindControl("imgViewFile");
                    if (oGLReconciliationItemInputInfo.DataImportID != null)
                    {
                        if (oGLReconciliationItemInputInfo.PreviousGLDataRecItemID == null)
                        {
                            IDataImport oDataImportClient = RemotingHelper.GetDataImportObject();
                            DataImportHdrInfo oDataImportHdrInfo = oDataImportClient.GetDataImportInfo(oGLReconciliationItemInputInfo.DataImportID, Helper.GetAppUserInfo());
                            if (oDataImportHdrInfo != null)
                            {
                                imgViewFile.Visible = true;
                                string url = "DownloadAttachment.aspx?" + QueryStringConstants.FILE_PATH + "=" + Server.UrlEncode(oDataImportHdrInfo.PhysicalPath);
                                imgViewFile.OnClientClick = "document.location.href = '" + url + "';return false;";
                            }
                        }
                    }



                    ExHyperLink hlShowItemInputForm = (ExHyperLink)e.Item.FindControl("hlShowItemInputForm");

                    Helper.SetImageURLForViewVersusEdit(WebEnums.FormMode.ReadOnly, hlShowItemInputForm);
                    string javascriptParameterList = PopupHelper.GetJavascriptParameterListForEditRecItem
                        (oGLReconciliationItemInputInfo.GLDataRecItemID
                        , "OpenRadWindowForHyperlinkWithName"
                        , "EditRecItemInputs.aspx"
                        , QueryStringConstants.READ_ONLY
                        , true
                        , this.AccountID
                        , this.GLDataID
                        , this.RecCategoryTypeID
                        , this.NetAccountID
                        , this.IsSRA
                        , RecCategoryID
                        , hdIsRefreshData.ClientID
                        , this.CurrentBCCY);
                    hlShowItemInputForm.NavigateUrl = javascriptParameterList;
                }

            }
            catch (Exception)
            {
            }
        }


        protected void rgGLAdjustmentCloseditems_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            List<GLDataRecItemInfo> oGLReconciliationItemInputInfoCollection = this.GetGLRecItemInfoCollection.Where(recItem => recItem.CloseDate != null).ToList();

            if (oGLReconciliationItemInputInfoCollection == null)
                oGLReconciliationItemInputInfoCollection = new List<GLDataRecItemInfo>();

            if (oGLReconciliationItemInputInfoCollection == null || oGLReconciliationItemInputInfoCollection.Count == 0)
            {
                pnlClosedItems.Visible = false;
            }
            else
            {
                pnlClosedItems.Visible = true;
            }

            this.rgGLAdjustmentCloseditems.DataSource = oGLReconciliationItemInputInfoCollection;
        }

        private DataTable GetGLDataParams()
        {
            DataTable dtGLData = new DataTable();
            dtGLData.Columns.Add("ID");

            foreach (GridDataItem item in rgGLAdjustments.SelectedItems)
            {
                DataRow rowGLData = dtGLData.NewRow();
                rowGLData["ID"] = item.GetDataKeyValue("GLDataRecItemID");
                dtGLData.Rows.Add(rowGLData);
            }

            return dtGLData;
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            DataTable dtGLDataParams = new DataTable();
            dtGLDataParams = GetGLDataParams();
            GLDataRecItemInfo oGLReconciliationItemInputInfo = new GLDataRecItemInfo();
            oGLReconciliationItemInputInfo.GLDataRecItemID = 0;
            oGLReconciliationItemInputInfo.GLDataID = this.GLDataID;
            oGLReconciliationItemInputInfo.ReconciliationCategoryTypeID = this.RecCategoryTypeID;
            oGLReconciliationItemInputInfo.RevisedBy = SessionHelper.CurrentUserLoginID;
            oGLReconciliationItemInputInfo.DateRevised = DateTime.Now;
            IGLDataRecItem oGLRecItemInputClient = RemotingHelper.GetGLDataRecItemObject();
            oGLRecItemInputClient.DeleteRecInputItem(oGLReconciliationItemInputInfo, (short)ARTEnums.AccountAttribute.ReconciliationTemplate, dtGLDataParams, Helper.GetAppUserInfo());


            this._GLRecItemInfoCollection = null;
            IGLDataRecItem oGLRecItemInput = RemotingHelper.GetGLDataRecItemObject();
            this.GetGLRecItemInfoCollection = oGLRecItemInput.GetRecItem(this.GLDataID.Value, SessionHelper.CurrentReconciliationPeriodID.Value, this.RecCategoryTypeID.Value, this._GLReconciliationItemInputRecordTypeID, (short)ARTEnums.AccountAttribute.ReconciliationTemplate, Helper.GetAppUserInfo());
            List<GLDataRecItemInfo> oGLReconciliationItemInputInfoCollection = this.GetGLRecItemInfoCollection.Where(recItem => recItem.CloseDate == null).ToList();
            rgGLAdjustments.DataSource = oGLReconciliationItemInputInfoCollection;
            rgGLAdjustments.DataBind();
            RecHelper.RefreshRecForm(this);
        }

        protected void rgGLAdjustments_ItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {

            try
            {
                CalculateAndDisplaySum();
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

                    CalculateAndDisplaySum();
                    this._GLRecItemInfoCollection = null;

                    List<GLDataRecItemInfo> oGLReconciliationItemInputInfoCollection = this.GetGLRecItemInfoCollection.Where(recItem => recItem.CloseDate == null).ToList();
                    rgGLAdjustments.DataSource = oGLReconciliationItemInputInfoCollection;
                    rgGLAdjustments.DataBind();
                    RecHelper.RefreshRecForm(this);
                }

                CalculateAndDisplaySum();
                if (e.CommandName == TelerikConstants.GridExportToPDFCommandName)
                {

                    isExportPDF = true;
                    //rgGLAdjustments.MasterTableView.Columns.FindByUniqueName("CheckboxSelectColumn").Visible = false;
                    rgGLAdjustments.MasterTableView.Columns.FindByUniqueName("DeleteColumn").Visible = false;
                    rgGLAdjustments.MasterTableView.Columns.FindByUniqueName("ShowInputForm").Visible = false;
                    rgGLAdjustments.MasterTableView.Columns.FindByUniqueName("CheckboxSelectColumn").Visible = false;
                    GridHelper.ExportGridToPDF(rgGLAdjustments, PopupHelper.GetPageTitle(this.RecCategoryID.Value, this.RecCategoryTypeID.Value));
                }
                if (e.CommandName == TelerikConstants.GridExportToExcelCommandName)
                {

                    isExportExcel = true;
                    //rgGLAdjustments.MasterTableView.Columns.FindByUniqueName("CheckboxSelectColumn").Visible = false;
                    rgGLAdjustments.MasterTableView.Columns.FindByUniqueName("DeleteColumn").Visible = false;
                    rgGLAdjustments.MasterTableView.Columns.FindByUniqueName("ShowInputForm").Visible = false;
                    rgGLAdjustments.MasterTableView.Columns.FindByUniqueName("CheckboxSelectColumn").Visible = false;
                    GridHelper.ExportGridToExcel(rgGLAdjustments, PopupHelper.GetPageTitle(this.RecCategoryID.Value, this.RecCategoryTypeID.Value));
                }
            }
            catch (Exception)
            {
            }
        }

        protected void rgGLAdjustmentst_ItemCreated(object sender, GridItemEventArgs e)
        {
            // Register PDF / Excel for Postback
            if (_IsRegisterPDFAndExcelForPostback)
                GridHelper.RegisterPDFAndExcelForPostback(e, isExportPDF, isExportExcel, this.Page);
            //GridHelper.RegisterPDFAndExcelForPostback(e, isExportPDF, isExportExcel, this.Page);
            GridHelper.SetStylesForExportGrid(e, isExportPDF, isExportExcel);
        }

        #endregion

        public void ompage_ReconciliationPeriodChangedEventHandler(object sender, EventArgs e)
        {
            try
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

        protected void btnReopen_Click(object sender, EventArgs e)
        {
            try
            {
                List<long> glRecItemInputIdCollection = new List<long>();

                foreach (GridDataItem item in rgGLAdjustmentCloseditems.SelectedItems)
                {
                    long glRecItemID = Convert.ToInt64(item["GLRecItemID"].Text);
                    glRecItemInputIdCollection.Add(glRecItemID);
                }

                IGLDataRecItem oGLDataRecItemClient = RemotingHelper.GetGLDataRecItemObject();
                oGLDataRecItemClient.UpdateGLRecItemCloseDate(this.GLDataID.Value, glRecItemInputIdCollection
                    , null, null, null, null, this.RecCategoryTypeID.Value, (short)ARTEnums.AccountAttribute.ReconciliationTemplate
                    , SessionHelper.GetCurrentUser().LoginID, DateTime.Now, SessionHelper.CurrentReconciliationPeriodID.Value, Helper.GetAppUserInfo());

                this._GLRecItemInfoCollection = null;
                PopulateGrids();
                CalculateAndDisplaySum();
                EnableDisableBulkCloseButton();
                RecHelper.RefreshRecForm(this);
            }
            catch (ARTException)
            {
            }
            catch (Exception)
            {
            }
        }

        public void PopulateGrids()
        {

            if (this.GLDataID == null || this.GLDataID == 0)
            {
                this.GetGLRecItemInfoCollection = null;
                this.rgGLAdjustments.DataSource = new List<GLDataRecItemInfo>();
                rgGLAdjustments.DataBind();
                this.rgGLAdjustmentCloseditems.DataSource = new List<GLDataRecItemInfo>();
                this.rgGLAdjustmentCloseditems.DataBind();
            }
            else
            {

                rgGLAdjustments.EntityNameText = PopupHelper.GetPageTitle(this.RecCategoryID.Value, this.RecCategoryTypeID.Value);

                IGLDataRecItem oGLRecItemInput = RemotingHelper.GetGLDataRecItemObject();
                this.GetGLRecItemInfoCollection = oGLRecItemInput.GetRecItem(this.GLDataID.Value, SessionHelper.CurrentReconciliationPeriodID.Value, this.RecCategoryTypeID.Value, this._GLReconciliationItemInputRecordTypeID, (short)ARTEnums.AccountAttribute.ReconciliationTemplate, Helper.GetAppUserInfo());

                List<GLDataRecItemInfo> oGLReconciliationItemInputInfoCollection = this.GetGLRecItemInfoCollection.Where(recItem => recItem.CloseDate == null).ToList();
                ShowHideCurrencyColumn(oGLReconciliationItemInputInfoCollection);

                rgGLAdjustments.DataSource = oGLReconciliationItemInputInfoCollection;
                rgGLAdjustments.DataBind();

                if (oGLReconciliationItemInputInfoCollection == null || oGLReconciliationItemInputInfoCollection.Count == 0)
                {
                    btnDelete.Visible = false;
                }
                else
                {
                    btnDelete.Visible = true;
                }

                List<GLDataRecItemInfo> oGLReconciliationItemInputInfoCollectionClosed = this.GetGLRecItemInfoCollection.Where(recItem => recItem.CloseDate != null).ToList();

                if (oGLReconciliationItemInputInfoCollectionClosed == null || oGLReconciliationItemInputInfoCollectionClosed.Count == 0)
                {
                    pnlClosedItems.Visible = false;
                }
                else
                {
                    pnlClosedItems.Visible = true;
                    this.rgGLAdjustmentCloseditems.DataSource = oGLReconciliationItemInputInfoCollectionClosed;
                    this.rgGLAdjustmentCloseditems.DataBind();
                }

                if (oGLReconciliationItemInputInfoCollection != null)
                {
                    int carryForwardedItems = GetCarryForwardedItemCount(oGLReconciliationItemInputInfoCollection);
                    if (Helper.GetFormMode(WebEnums.ARTPages.GLAdjustments, this.GLDataHdrInfo) == WebEnums.FormMode.Edit)
                    {
                        if (carryForwardedItems == oGLReconciliationItemInputInfoCollection.Count)
                        {
                            btnDelete.Visible = false;
                        }
                        else
                        {
                            btnDelete.Visible = true;
                        }
                    }
                    else
                    {
                        btnDelete.Visible = false;
                    }
                }
            }
        }

        private int GetCarryForwardedItemCount(List<GLDataRecItemInfo> objGLDataRecItemInfo)
        {
            int countCarryForwardedItem = 0;
            foreach (GLDataRecItemInfo glDataInfo in objGLDataRecItemInfo)
            {
                if (glDataInfo.IsForwardedItem.HasValue && glDataInfo.IsForwardedItem.Value)
                {
                    countCarryForwardedItem += 1;
                }
            }

            return countCarryForwardedItem;
        }


        protected void btnCancel_OnClick(object sender, EventArgs e)
        {
            try
            {
                string pathAndQuery = (string)ViewState[ViewStateConstants.PATH_AND_QUERY];
                Response.Redirect(pathAndQuery);
            }
            catch (ARTException)
            {
                //Helper.ShowErrorMessage(this, ex);
            }
            catch (Exception)
            {
                //Helper.ShowErrorMessage(this, ex);
            }
        }

        private void ShowHideCurrencyColumn(List<GLDataRecItemInfo> oGLReconciliationItemInputInfoCollection)
        {
            if (!_isCapabilityMultiCurrencyAccount)
            {
                // Hide LCCY Code Column if Multi-Currency is Off
                GridColumn gcLocalCurrencyCode = rgGLAdjustments.Columns.FindByUniqueNameSafe("LocalCurrencyCode");
                if (gcLocalCurrencyCode != null)
                {
                    gcLocalCurrencyCode.Visible = false;
                }

                gcLocalCurrencyCode = rgGLAdjustmentCloseditems.Columns.FindByUniqueNameSafe("LocalCurrencyCode");
                if (gcLocalCurrencyCode != null)
                {
                    gcLocalCurrencyCode.Visible = false;
                }
            }
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
                if (IsExpanded)
                    ToggleControl.ImageUrl = "~/App_Themes/SkyStemBlueBrown/Images/CollapseGlass.gif";
                else
                    ToggleControl.ImageUrl = "~/App_Themes/SkyStemBlueBrown/Images/ExpandGlass.gif";
            }
        }
    }
}