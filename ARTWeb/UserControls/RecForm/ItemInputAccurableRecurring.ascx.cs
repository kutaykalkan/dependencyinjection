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
using SkyStem.ART.Client.Data;
using SkyStem.ART.Web.Data;
using SkyStem.ART.Client.IServices;
using SkyStem.ART.Client.Exception;
using SkyStem.ART.Web.Utility;
using SkyStem.ART.Web.Classes;
using Telerik.Web.UI;
using SkyStem.ART.Client.Model;
using SkyStem.Library.Controls.WebControls;
using System.Collections.Generic;
using SkyStem.Language.LanguageUtility;
using SkyStem.Library.Controls.TelerikWebControls.Data;



namespace SkyStem.ART.Web.UserControls
{

    public partial class UserControls_ItemInputAccurableRecurring : UserControlRecItemBase
    {
        #region PrivateProperties
        const ARTEnums.ReconciliationItemTemplate eReconciliationItemTemplateThisPage = ARTEnums.ReconciliationItemTemplate.AccrualForm;
        
        public short _GLReconciliationItemInputRecordTypeID = 0;
        private List<GLDataRecurringItemScheduleInfo> _GLDataRecurringItemScheduleInfoCollection = null;
        private short _UserRole = 0;
        private short _RecPeriodStatus = 0;
        private decimal? _ReportingCurrencyTotal = 0;
        private bool? _IsExpandedValueForced = null;
        ExImageButton ToggleControl;
        bool isExportPDF;
        bool isExportExcel;
        #endregion

        public override bool DisableExportInPrint
        {
            set
            {
                DisableCommandItemForPrint(rgAccurableRecurring);
                DisableCommandItemForPrint(rgGLAdjustmentCloseditems);
            }
        }

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

        private List<GLDataRecurringItemScheduleInfo> GetGLRecItemInfoCollection
        {
            get
            {
                if (this._GLDataRecurringItemScheduleInfoCollection == null)
                {
                    try
                    {
                        IGLDataRecItemSchedule oGLDataRecItemScheduleClient = RemotingHelper.GetGLDataRecItemScheduleObject();
                        this._GLDataRecurringItemScheduleInfoCollection = oGLDataRecItemScheduleClient.GetGLDataRecurringItemSchedule(this.GLDataID, Helper.GetAppUserInfo());
                    }
                    catch (ARTException)
                    {

                    }
                    catch (Exception)
                    {

                    }

                }

                return this._GLDataRecurringItemScheduleInfoCollection;
            }
            set
            {
                this._GLDataRecurringItemScheduleInfoCollection = value;
            }
        }

        private bool _IsRegisterPDFAndExcelForPostback = true;
        public bool IsRegisterPDFAndExcelForPostback
        {
            get { return _IsRegisterPDFAndExcelForPostback; }
            set { _IsRegisterPDFAndExcelForPostback = value; }
        }

        //private int GLRecInputItemID;
        //private GLDataRecurringItemScheduleInfo oGLRecItemInputInfo;

        private decimal? _originalAmountRCCY;
        private decimal? _accurudedAmount;

        private decimal? _originalAmountRCCYClosedItem;
        private decimal? _accuruedAmountClosedItem;
        private decimal? _remainingAmountClosedItem;
        #endregion
        #region "Page Events"

        protected void Page_Init(object sender, EventArgs e)
        {
            try
            {
                Helper.SetGridImageButtonProperties(this.rgAccurableRecurring.MasterTableView.Columns);
                this.rgAccurableRecurring.EntityNameLabelID = 1446;
                this.rgGLAdjustmentCloseditems.EntityNameLabelID = 1446;
            }
            catch (ARTException)
            {

            }
            catch (Exception)
            {

            }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            SetControlState();
            if (!IsPostBack)
            {
                isExportExcel = false;
                isExportPDF = false;
            }

            HandlePrintMode(trOpenItemsButtonRow, trClosedItemsButtonRow, rgAccurableRecurring);
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
                if (!IsPostBack)
                {
                    isExportPDF = false;
                    isExportExcel = false;
                }
                //updpnlMain.Visible = true;
                this._GLDataRecurringItemScheduleInfoCollection = null;
                SetPrivateVariables();

                IGLDataRecItemSchedule oGLDataRecItemScheduleClient = RemotingHelper.GetGLDataRecItemScheduleObject();
                this._GLDataRecurringItemScheduleInfoCollection = oGLDataRecItemScheduleClient.GetGLDataRecurringItemSchedule(this.GLDataID, Helper.GetAppUserInfo());

                btnAdd.OnClientClick = PopupHelper.GetJavascriptParameterListForEditRecItem(null, "OpenRadWindowWithName", "EditItemAccrubleRecurring.aspx", QueryStringConstants.INSERT, false, this.AccountID, this.GLDataID, this.RecCategoryTypeID, this.NetAccountID, this.IsSRA, RecCategoryID, hdIsRefreshData.ClientID, this.CurrentBCCY );
                btnAdd.Attributes.Add("onclick", "return false;");

                btnClose.OnClientClick = PopupHelper.GetJavascriptParameterList(null, "OpenRadWindow", "BulkCloseAccrubleRecurring.aspx", QueryStringConstants.INSERT, false, this.AccountID, this.GLDataID, this.RecCategoryTypeID, this.NetAccountID, this.IsSRA, RecCategoryID, hdIsRefreshData.ClientID);

                CalculateAndDisplaySum();
                EnableDisableControlsForNonPreparersAndClosedPeriods();
                EnableDisableBulkCloseButton();
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

        private void SetPrivateVariables()
        {
            _GLReconciliationItemInputRecordTypeID = (short)WebEnums.RecordType.GLReconciliationItemInput;
        }

        private void EnableDisableBulkCloseButton()
        {
            if (this.GetGLRecItemInfoCollection == null)
                btnClose.Enabled = false;
            else
            {

                int numberOfOldOpenItems = (from recItem in this.GetGLRecItemInfoCollection
                                            where                                                
                                            recItem.CloseDate == null
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

            if (Helper.GetFormMode(WebEnums.ARTPages.GLAdjustments, this.GLDataHdrInfo) == WebEnums.FormMode.Edit)
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
            //decimal? baseCurrencyTotal = (from recItem in this.GetGLRecItemInfoCollection
            //                              where recItem.CloseDate == null
            //                              select recItem.RecPeriodAmountBaseCurrency).Sum();

            this._ReportingCurrencyTotal = (from recItem in this.GetGLRecItemInfoCollection
                                            where recItem.CloseDate == null
                                            select recItem.BalanceReportingCurrency).Sum();


        }
        #endregion

        #region "RADGrid Event Handling"
        protected void rgAccurableRecurring_DeleteCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {

        }
        protected void rgAccurableRecurring_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            try
            {
                CalculateAndDisplaySum();

                #region "Setting Grid Header texts"
                if (e.Item.ItemType == GridItemType.Header)
                {
                    ////((LinkButton)(e.Item as GridHeaderItem)["RecPeriodAmountReportingCurrency"].Controls[0]).Text = Helper.GetLabelIDValue(1780) + " (" + SessionHelper.ReportingCurrencyCode + ")";
                    ////((LinkButton)(e.Item as GridHeaderItem)["BalanceReportingCurrency"].Controls[0]).Text = Helper.GetLabelIDValue(1701) + " (" + SessionHelper.ReportingCurrencyCode + ")";
                    //***************Above code commented and replaced by below code to handle the export to pdf of Grid
                    this._originalAmountRCCY = 0.00M;
                    this._accurudedAmount = 0.00M;

                    //this._ReportingCurrencyTotal = 0.00M;

                    this.SetGridHeaders(e);
                    //******************************************************************************************************************************************

                }
                #endregion

                #region "Setting Values in each row control"
                if (e.Item.ItemType == Telerik.Web.UI.GridItemType.Item || e.Item.ItemType == Telerik.Web.UI.GridItemType.AlternatingItem)
                {

                    GLDataRecurringItemScheduleInfo oGLDataRecurringItemScheduleInfo = (GLDataRecurringItemScheduleInfo)e.Item.DataItem;

                    bool _IsForwardedItem = !(oGLDataRecurringItemScheduleInfo.CreatedInRecPeriodID == SessionHelper.CurrentReconciliationPeriodID);

                    GridHelper.SetCSSClassForForwardedItems(e, _IsForwardedItem);

                    //Original Amount LCCY
                    ExLabel lblOriginalAmount = (ExLabel)e.Item.FindControl("lblOriginalAmountLCCY");
                    lblOriginalAmount.Text = Helper.GetCurrencyValue(oGLDataRecurringItemScheduleInfo.ScheduleAmount, oGLDataRecurringItemScheduleInfo.LocalCurrencyCode);

                    //Open Date
                    ExLabel lblOpenDate = (ExLabel)e.Item.FindControl("lblOpenDate");
                    lblOpenDate.Text = Helper.GetDisplayDate(oGLDataRecurringItemScheduleInfo.OpenDate);

                    //Schedule Name
                    ExLabel lblScheduleName = (ExLabel)e.Item.FindControl("lblScheduleName");
                    lblScheduleName.Text = Helper.GetDisplayStringValue(oGLDataRecurringItemScheduleInfo.ScheduleName);

                    //Schedule Begin Date
                    //Schedule End Date
                    //Original Amount RCCY
                    ExLabel lblOriginalAmountRCCY = (ExLabel)e.Item.FindControl("lblOriginalAmountRCCY");
                    lblOriginalAmountRCCY.Text = Helper.GetDisplayDecimalValue(oGLDataRecurringItemScheduleInfo.ScheduleAmountReportingCurrency);
                    if (oGLDataRecurringItemScheduleInfo.ScheduleAmountReportingCurrency.HasValue)
                        this._originalAmountRCCY = this._originalAmountRCCY + oGLDataRecurringItemScheduleInfo.ScheduleAmountReportingCurrency;

                    //Total Amortized Amount 
                    ExLabel lblAmortizedAmountRCCY = (ExLabel)e.Item.FindControl("lblAmortizedAmountRCCY");
                    lblAmortizedAmountRCCY.Text = Helper.GetDisplayDecimalValue(oGLDataRecurringItemScheduleInfo.RecPeriodAmountReportingCurrency);
                    if (oGLDataRecurringItemScheduleInfo.RecPeriodAmountReportingCurrency.HasValue)
                        this._accurudedAmount = this._accurudedAmount + oGLDataRecurringItemScheduleInfo.RecPeriodAmountReportingCurrency;

                    //Remaining Amortizable Amount RCCY
                    ExLabel lblBalanceRCCY = (ExLabel)e.Item.FindControl("lblBalanceRCCY");
                    lblBalanceRCCY.Text = Helper.GetDisplayDecimalValue(oGLDataRecurringItemScheduleInfo.BalanceReportingCurrency);

                    //Docs
                    ExLabel lblAttachmentCount = (ExLabel)e.Item.FindControl("lblAttachmentCount");
                    lblAttachmentCount.Text = Helper.GetDisplayIntegerValue(oGLDataRecurringItemScheduleInfo.AttachmentCount);

                    ExLabel lblRecItemNumber = (ExLabel)e.Item.FindControl("lblRecItemNumber");
                    lblRecItemNumber.Text = Helper.GetDisplayStringValue(oGLDataRecurringItemScheduleInfo.RecItemNumber);

                    //************* MatchSetRefNo********************************************************
                    RecHelper.SetMatchSetRefNumberUrlForGLDataRecurringSchedule(e, oGLDataRecurringItemScheduleInfo, AccountID, NetAccountID, GLDataID);
                    //************************************************************************************

                    // ShowHide the Excel Button if  the Rec Item is imported through file (and not added manually)
                    ExImageButton imgViewFile = (ExImageButton)e.Item.FindControl("imgViewFile");
                    if (oGLDataRecurringItemScheduleInfo.DataImportID != null)
                    {
                        if (oGLDataRecurringItemScheduleInfo.PreviousGLDataRecurringItemScheduleID == null)
                        {
                            IDataImport oDataImportClient = RemotingHelper.GetDataImportObject();
                            DataImportHdrInfo oDataImportHdrInfo = oDataImportClient.GetDataImportInfo(oGLDataRecurringItemScheduleInfo.DataImportID, Helper.GetAppUserInfo());
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

                    if (Helper.GetFormMode(WebEnums.ARTPages.GLAdjustments, this.GLDataHdrInfo) == WebEnums.FormMode.Edit)
                    {
                        mode = QueryStringConstants.EDIT;
                        Helper.SetImageURLForViewVersusEdit(WebEnums.FormMode.Edit, hlShowItemInputForm);
                    }
                    else
                    {
                        mode = QueryStringConstants.READ_ONLY;
                        Helper.SetImageURLForViewVersusEdit(WebEnums.FormMode.ReadOnly, hlShowItemInputForm);
                    }
                    hlShowItemInputForm.NavigateUrl = PopupHelper.GetJavascriptParameterListForEditRecItem(oGLDataRecurringItemScheduleInfo.GLDataRecurringItemScheduleID, "OpenRadWindowForHyperlinkWithName", "EditItemAccrubleRecurring.aspx", mode, _IsForwardedItem, this.AccountID, this.GLDataID, this.RecCategoryTypeID, this.NetAccountID, this.IsSRA, RecCategoryID, hdIsRefreshData.ClientID, this.CurrentBCCY );//IsforwardedItem is not used here so redundant

                    if ((e.Item as GridDataItem)["DeleteColumn"] != null)
                    {
                        ImageButton deleteButton = (ImageButton)(e.Item as GridDataItem)["DeleteColumn"].Controls[0];
                        CheckBox chkSelectItem = (CheckBox)(e.Item as GridDataItem)["CheckboxSelectColumn"].Controls[0];
                        if (Helper.GetFormMode(WebEnums.ARTPages.GLAdjustments, this.GLDataHdrInfo) == WebEnums.FormMode.Edit)
                        {
                            if (_IsForwardedItem)
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
                        deleteButton.CommandArgument = oGLDataRecurringItemScheduleInfo.GLDataRecurringItemScheduleID.ToString();
                    }
                }
                #endregion

                #region "Setting Footer text and Totals"
                if (e.Item.ItemType == GridItemType.Footer)
                {
                    //Original Amount RCCY
                    ExLabel lblOriginalAmountRCCYTotalValue = (ExLabel)e.Item.FindControl("lblOriginalAmountRCCYTotalValue");
                    lblOriginalAmountRCCYTotalValue.Text = Helper.GetDisplayDecimalValue(this._originalAmountRCCY);

                    //Total Amortized Amount
                    ExLabel lblAmortizedAmountRCCYTotalValue = (ExLabel)e.Item.FindControl("lblAmortizedAmountRCCYTotalValue");
                    lblAmortizedAmountRCCYTotalValue.Text = Helper.GetDisplayDecimalValue(this._accurudedAmount);

                    //Remaining Amortizable Amount RCCY
                    ExLabel lblBalanceRCCYTotalValue = (ExLabel)e.Item.FindControl("lblBalanceRCCYTotalValue");
                    lblBalanceRCCYTotalValue.Text = Helper.GetDisplayDecimalValue(this._ReportingCurrencyTotal);


                }
                #endregion
            }
            catch (ARTException)
            {

            }
            catch (Exception)
            {

            }
        }

        private void SetGridHeaders(Telerik.Web.UI.GridItemEventArgs e)
        {
            Control oControlRecPeriodAmountReportingCurrency = new Control();
            Control oControlBalanceReportingCurrency = new Control();
            Control oControlOriginalAmountRCCY = new Control();
            Control oControlOriginalAmountLCCY = new Control();

            GridHeaderItem headerItem = e.Item as GridHeaderItem;

            oControlRecPeriodAmountReportingCurrency = (headerItem)["AccruedAmountRCCY"].Controls[0];
            oControlBalanceReportingCurrency = (headerItem)["BalanceReportingCurrency"].Controls[0];
            oControlOriginalAmountRCCY = (headerItem)["OriginalAmountRCCY"].Controls[0];
            oControlOriginalAmountLCCY = (headerItem)["OriginalAmount"].Controls[0];



            if (oControlRecPeriodAmountReportingCurrency is LinkButton)
            {
                ((LinkButton)oControlRecPeriodAmountReportingCurrency).Text = Helper.GetLabelIDValue(2058) + " (" + SessionHelper.ReportingCurrencyCode + ")";


            }
            else
            {
                if (oControlRecPeriodAmountReportingCurrency is LiteralControl)
                {
                    ((LiteralControl)oControlRecPeriodAmountReportingCurrency).Text = Helper.GetLabelIDValue(2058) + " (" + SessionHelper.ReportingCurrencyCode + ")";

                }
            }

            if (oControlBalanceReportingCurrency is LinkButton)
            {
                ((LinkButton)oControlBalanceReportingCurrency).Text = Helper.GetLabelIDValue(2061) + " (" + SessionHelper.ReportingCurrencyCode + ")";

            }
            else
            {
                if (oControlBalanceReportingCurrency is LiteralControl)
                {
                    ((LiteralControl)oControlBalanceReportingCurrency).Text = Helper.GetLabelIDValue(2061) + " (" + SessionHelper.ReportingCurrencyCode + ")";

                }
            }
            if (oControlOriginalAmountRCCY is LinkButton)
                ((LinkButton)oControlOriginalAmountRCCY).Text = Helper.GetLabelIDValue(1700) + " (" + SessionHelper.ReportingCurrencyCode + ")";
            else
            {
                if (oControlOriginalAmountRCCY is LiteralControl)
                    ((LiteralControl)oControlOriginalAmountRCCY).Text = Helper.GetLabelIDValue(1700) + " (" + SessionHelper.ReportingCurrencyCode + ")";

            }


            if (oControlOriginalAmountLCCY is LinkButton)
            {
                ((LinkButton)oControlOriginalAmountLCCY).Text = Helper.GetLabelIDValue(1700) + " (L-CCY)";


            }
            else
            {
                if (oControlOriginalAmountLCCY is LiteralControl)
                {
                    ((LiteralControl)oControlOriginalAmountLCCY).Text = Helper.GetLabelIDValue(1700) + "  (L-CCY)";

                }
            }
        }

        protected void rgAccurableRecurring_ItemCreated(object sender, GridItemEventArgs e)
        {
            ////// Register PDF / Excel for Postback
            ////if (_IsRegisterPDFAndExcelForPostback)
            ////    GridHelper.RegisterPDFAndExcelForPostback(e, isExportPDF, isExportExcel, this.Page);
            //////GridHelper.RegisterPDFAndExcelForPostback(e, isExportPDF, isExportExcel, this.Page);
            ////GridHelper.SetStylesForExportGrid(e, isExportPDF, isExportExcel);

            // Register PDF / Excel for Postback
            if (_IsRegisterPDFAndExcelForPostback)
                GridHelper.RegisterPDFAndExcelForPostback(e, isExportPDF, isExportExcel, this.Page);
            //GridHelper.RegisterPDFAndExcelForPostback(e, isExportPDF, isExportExcel, this.Page);
            GridHelper.SetStylesForExportGrid(e, isExportPDF, isExportExcel);
            if (e.Item is GridFooterItem)
            {
                GridFooterItem footer = ((GridFooterItem)e.Item);
                int index = 5;
                footer["OriginalAmount"].ColumnSpan = index;

                footer["OpenDate"].Visible = false;
                footer["ScheduleName"].Visible = false;
                footer["ScheduleBeginDate"].Visible = false;
                footer["ScheduleEndDate"].Visible = false;
            }
        }

        protected void rgAccurableRecurring_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            List<GLDataRecurringItemScheduleInfo> oGLDataRecurringItemScheduleInfoCollection = this.GetGLRecItemInfoCollection.Where(recItem => recItem.CloseDate == null).ToList();
            this.rgAccurableRecurring.DataSource = oGLDataRecurringItemScheduleInfoCollection;
        }

        protected void rgGLAdjustmentCloseditems_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {

            try
            {
                CalculateAndDisplaySum();
                #region "Grid Header Items"
                if (e.Item.ItemType == GridItemType.Header)
                {
                    this._originalAmountRCCYClosedItem = 0.00M;
                    this._accuruedAmountClosedItem = 0.00M;
                    this._remainingAmountClosedItem = 0.00M;

                    this.SetGridHeaders(e);
                }
                #endregion

                #region "Setting Footer text and Totals"
                if (e.Item.ItemType == GridItemType.Footer)
                {
                    //Original Amount RCCY
                    ExLabel lblOriginalAmountRCCYTotalValue = (ExLabel)e.Item.FindControl("lblOriginalAmountRCCYTotalValue");
                    lblOriginalAmountRCCYTotalValue.Text = Helper.GetDisplayDecimalValue(this._originalAmountRCCYClosedItem);

                    //Total Amortized Amount
                    ExLabel lblAmortizedAmountRCCYTotalValue = (ExLabel)e.Item.FindControl("lblAmortizedAmountRCCYTotalValue");
                    lblAmortizedAmountRCCYTotalValue.Text = Helper.GetDisplayDecimalValue(this._accuruedAmountClosedItem);

                    //Remaining Amortizable Amount RCCY
                    ExLabel lblBalanceRCCYTotalValue = (ExLabel)e.Item.FindControl("lblBalanceRCCYTotalValue");
                    lblBalanceRCCYTotalValue.Text = Helper.GetDisplayDecimalValue(this._remainingAmountClosedItem);
                }
                #endregion

                #region "Row Items"
                if (e.Item.ItemType == Telerik.Web.UI.GridItemType.Item || e.Item.ItemType == Telerik.Web.UI.GridItemType.AlternatingItem)
                {
                    GLDataRecurringItemScheduleInfo oGLDataRecurringItemScheduleInfo = (GLDataRecurringItemScheduleInfo)e.Item.DataItem;

                    //Original Amount LCCY
                    ExLabel lblAmount = (ExLabel)e.Item.FindControl("lblOriginalAmountLCCY");
                    lblAmount.Text = Helper.GetCurrencyValue(oGLDataRecurringItemScheduleInfo.ScheduleAmount, oGLDataRecurringItemScheduleInfo.LocalCurrencyCode);

                    //Open Date
                    ExLabel lblOpenDate = (ExLabel)e.Item.FindControl("lblOpenDate");
                    lblOpenDate.Text = Helper.GetDisplayDate(oGLDataRecurringItemScheduleInfo.OpenDate);

                    //Schedule Name
                    ExLabel lblScheduleName = (ExLabel)e.Item.FindControl("lblScheduleName");
                    lblScheduleName.Text = Helper.GetDisplayStringValue(oGLDataRecurringItemScheduleInfo.ScheduleName);

                    //Schedule Begin Date

                    //Schedule End Date


                    //Close Date
                    ExLabel lblCloseDate = (ExLabel)e.Item.FindControl("lblCloseDate");
                    lblCloseDate.Text = Helper.GetDisplayDate(oGLDataRecurringItemScheduleInfo.CloseDate);


                    // ShowHide the Excel Button if  the Rec Item is imported through file (and not added manually)
                    ExImageButton imgViewFile = (ExImageButton)e.Item.FindControl("imgViewFile");
                    if (oGLDataRecurringItemScheduleInfo.DataImportID != null)
                    {
                        if (oGLDataRecurringItemScheduleInfo.PreviousGLDataRecurringItemScheduleID == null)
                        {
                            IDataImport oDataImportClient = RemotingHelper.GetDataImportObject();
                            DataImportHdrInfo oDataImportHdrInfo = oDataImportClient.GetDataImportInfo(oGLDataRecurringItemScheduleInfo.DataImportID, Helper.GetAppUserInfo());
                            if (oDataImportHdrInfo != null)
                            {
                                imgViewFile.Visible = true;
                                string url = "DownloadAttachment.aspx?" + QueryStringConstants.FILE_PATH + "=" + Server.UrlEncode(oDataImportHdrInfo.PhysicalPath);
                                imgViewFile.OnClientClick = "document.location.href = '" + url + "';return false;";
                            }
                        }
                    }



                    //Original Amount RCCY
                    ExLabel lblOriginalAmountRCCY = (ExLabel)e.Item.FindControl("lblOriginalAmountRCCY");
                    lblOriginalAmountRCCY.Text = Helper.GetDisplayDecimalValue(oGLDataRecurringItemScheduleInfo.ScheduleAmountReportingCurrency);
                    if (oGLDataRecurringItemScheduleInfo.ScheduleAmountReportingCurrency.HasValue)
                        this._originalAmountRCCYClosedItem = this._originalAmountRCCYClosedItem + oGLDataRecurringItemScheduleInfo.ScheduleAmountReportingCurrency;

                    //Total Amortized Amount
                    ExLabel lblAmortizedAmountRCCY = (ExLabel)e.Item.FindControl("lblAmortizedAmountRCCY");
                    lblAmortizedAmountRCCY.Text = Helper.GetDisplayDecimalValue(oGLDataRecurringItemScheduleInfo.RecPeriodAmountReportingCurrency);
                    if (oGLDataRecurringItemScheduleInfo.RecPeriodAmountReportingCurrency.HasValue)
                        this._accuruedAmountClosedItem = this._accuruedAmountClosedItem + oGLDataRecurringItemScheduleInfo.RecPeriodAmountReportingCurrency;

                    //Remaining Amortizable Amount RCCY
                    ExLabel lblBalanceRCCY = (ExLabel)e.Item.FindControl("lblBalanceRCCY");
                    lblBalanceRCCY.Text = Helper.GetDisplayDecimalValue(oGLDataRecurringItemScheduleInfo.BalanceReportingCurrency);
                    if (oGLDataRecurringItemScheduleInfo.BalanceReportingCurrency.HasValue)
                        this._remainingAmountClosedItem = this._remainingAmountClosedItem + oGLDataRecurringItemScheduleInfo.BalanceReportingCurrency;
                    //Documents
                    ExLabel lblAttachmentCount = (ExLabel)e.Item.FindControl("lblAttachmentCount");
                    lblAttachmentCount.Text = Helper.GetDisplayIntegerValue(oGLDataRecurringItemScheduleInfo.AttachmentCount);


                    ExLabel lblRecItemNumber = (ExLabel)e.Item.FindControl("lblRecItemNumber");
                    lblRecItemNumber.Text = Helper.GetDisplayStringValue(oGLDataRecurringItemScheduleInfo.RecItemNumber);

                    //************* MatchSetRefNo********************************************************
                    RecHelper.SetMatchSetRefNumberUrlForGLDataRecurringSchedule(e, oGLDataRecurringItemScheduleInfo, AccountID, NetAccountID, GLDataID);
                    //************************************************************************************

                    ExHyperLink hlShowItemInputForm = (ExHyperLink)e.Item.FindControl("hlShowItemInputForm");
                    string javascriptParameterList = PopupHelper.GetJavascriptParameterListForEditRecItem(oGLDataRecurringItemScheduleInfo.GLDataRecurringItemScheduleID, "OpenRadWindowForHyperlinkWithName", "EditItemAccrubleRecurring.aspx", QueryStringConstants.READ_ONLY, true, this.AccountID, this.GLDataID, this.RecCategoryTypeID, this.NetAccountID, this.IsSRA, RecCategoryID, hdIsRefreshData.ClientID, this.CurrentBCCY );
                    hlShowItemInputForm.NavigateUrl = javascriptParameterList;

                    Helper.SetImageURLForViewVersusEdit(WebEnums.FormMode.ReadOnly, hlShowItemInputForm);
                }
                #endregion

            }
            catch (ARTException)
            {

            }
            catch (Exception)
            {

            }
        }

        protected void rgGLAdjustmentCloseditems_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            List<GLDataRecurringItemScheduleInfo> oGLDataRecurringItemScheduleInfoCollection = this.GetGLRecItemInfoCollection.Where(recItem => recItem.CloseDate != null).ToList();

            if (oGLDataRecurringItemScheduleInfoCollection == null)
                oGLDataRecurringItemScheduleInfoCollection = new List<GLDataRecurringItemScheduleInfo>();

            if (oGLDataRecurringItemScheduleInfoCollection == null || oGLDataRecurringItemScheduleInfoCollection.Count == 0)
            {
                pnlClosedItems.Visible = false;
            }
            else
            {
                pnlClosedItems.Visible = true;
            }

            this.rgGLAdjustmentCloseditems.DataSource = oGLDataRecurringItemScheduleInfoCollection;
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
                // Helper.ShowErrorMessage(this, ex);
            }
        }

        protected void rgGLAdjustmentCloseditems_ItemCreated(object sender, GridItemEventArgs e)
        {
            //// Register PDF / Excel for Postback
            //if (_IsRegisterPDFAndExcelForPostback)
            //    GridHelper.RegisterPDFAndExcelForPostback(e, isExportPDF, isExportExcel, this.Page);
            ////GridHelper.RegisterPDFAndExcelForPostback(e, isExportPDF, isExportExcel, this.Page);
            //GridHelper.SetStylesForExportGrid(e, isExportPDF, isExportExcel);

            // Register PDF / Excel for Postback
            if (_IsRegisterPDFAndExcelForPostback)
                GridHelper.RegisterPDFAndExcelForPostback(e, isExportPDF, isExportExcel, this.Page);
            //GridHelper.RegisterPDFAndExcelForPostback(e, isExportPDF, isExportExcel, this.Page);
            GridHelper.SetStylesForExportGrid(e, isExportPDF, isExportExcel);

            if (e.Item is GridFooterItem)
            {
                GridFooterItem footer = ((GridFooterItem)e.Item);
                int index = 6;
                footer["OriginalAmount"].ColumnSpan = index;

                footer["OpenDate"].Visible = false;
                footer["ScheduleName"].Visible = false;
                footer["ScheduleBeginDate"].Visible = false;
                footer["ScheduleEndDate"].Visible = false;
                footer["CloseDate"].Visible = false;
            }
        }

        private DataTable GetGLDataParams()
        {
            DataTable dtGLData = new DataTable();
            dtGLData.Columns.Add("ID");

            foreach (GridDataItem item in rgAccurableRecurring.SelectedItems)
            {
                DataRow rowGLData = dtGLData.NewRow();
                rowGLData["ID"] = item.GetDataKeyValue("GLDataRecurringItemScheduleID");
                dtGLData.Rows.Add(rowGLData);
            }

            return dtGLData;
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            DataTable dtGLDataParams = new DataTable();
            dtGLDataParams = GetGLDataParams();
            GLDataRecurringItemScheduleInfo oGLDataRecurringItemScheduleInfo = new GLDataRecurringItemScheduleInfo();
            oGLDataRecurringItemScheduleInfo.GLDataRecurringItemScheduleID = 0;
            oGLDataRecurringItemScheduleInfo.GLDataID = this.GLDataID;
            oGLDataRecurringItemScheduleInfo.ReconciliationCategoryTypeID = this.RecCategoryTypeID;
            oGLDataRecurringItemScheduleInfo.RevisedBy = SessionHelper.CurrentUserLoginID;
            oGLDataRecurringItemScheduleInfo.DateRevised = DateTime.Now;
            IGLDataRecItemSchedule oGLDataRecItemScheduleClient = RemotingHelper.GetGLDataRecItemScheduleObject();

            oGLDataRecItemScheduleClient.DeleteGLDataRecurringItemSchedule(oGLDataRecurringItemScheduleInfo, (short)ARTEnums.AccountAttribute.ReconciliationTemplate, dtGLDataParams, Helper.GetAppUserInfo());

            CalculateAndDisplaySum();

            this._GLDataRecurringItemScheduleInfoCollection = null;

            List<GLDataRecurringItemScheduleInfo> oGLDataRecurringItemScheduleInfoCollection = this.GetGLRecItemInfoCollection.Where(recItem => recItem.CloseDate == null).ToList();
            rgAccurableRecurring.DataSource = oGLDataRecurringItemScheduleInfoCollection;
            rgAccurableRecurring.DataBind();
            RecHelper.RefreshRecForm(this);
        }

        protected void rgAccurableRecurring_ItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
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

                    GLDataRecurringItemScheduleInfo oGLDataRecurringItemScheduleInfo = new GLDataRecurringItemScheduleInfo();
                    oGLDataRecurringItemScheduleInfo.GLDataRecurringItemScheduleID = Convert.ToInt64(e.CommandArgument);
                    oGLDataRecurringItemScheduleInfo.GLDataID = this.GLDataID;
                    oGLDataRecurringItemScheduleInfo.ReconciliationCategoryTypeID = this.RecCategoryTypeID;
                    oGLDataRecurringItemScheduleInfo.RevisedBy = SessionHelper.CurrentUserLoginID;
                    oGLDataRecurringItemScheduleInfo.DateRevised = DateTime.Now;
                    IGLDataRecItemSchedule oGLDataRecItemScheduleClient = RemotingHelper.GetGLDataRecItemScheduleObject();

                    oGLDataRecItemScheduleClient.DeleteGLDataRecurringItemSchedule(oGLDataRecurringItemScheduleInfo, (short)ARTEnums.AccountAttribute.ReconciliationTemplate, dtGLDataParams, Helper.GetAppUserInfo());

                    CalculateAndDisplaySum();

                    this._GLDataRecurringItemScheduleInfoCollection = null;

                    List<GLDataRecurringItemScheduleInfo> oGLDataRecurringItemScheduleInfoCollection = this.GetGLRecItemInfoCollection.Where(recItem => recItem.CloseDate == null).ToList();
                    rgAccurableRecurring.DataSource = oGLDataRecurringItemScheduleInfoCollection;
                    rgAccurableRecurring.DataBind();
                    RecHelper.RefreshRecForm(this);

                }

                if (e.CommandName == TelerikConstants.GridExportToPDFCommandName)
                {

                    isExportPDF = true;
                    //rgGLAdjustments.MasterTableView.Columns.FindByUniqueName("CheckboxSelectColumn").Visible = false;
                    rgAccurableRecurring.MasterTableView.Columns.FindByUniqueName("DeleteColumn").Visible = false;
                    rgAccurableRecurring.MasterTableView.Columns.FindByUniqueName("ShowInputForm").Visible = false;
                    rgAccurableRecurring.MasterTableView.Columns.FindByUniqueName("CheckboxSelectColumn").Visible = false;
                    GridHelper.ExportGridToPDF(rgAccurableRecurring, PopupHelper.GetPageTitle(this.RecCategoryID.Value, this.RecCategoryTypeID.Value));
                }
                if (e.CommandName == TelerikConstants.GridExportToExcelCommandName)
                {

                    isExportExcel = true;
                    //rgGLAdjustments.MasterTableView.Columns.FindByUniqueName("CheckboxSelectColumn").Visible = false;
                    rgAccurableRecurring.MasterTableView.Columns.FindByUniqueName("DeleteColumn").Visible = false;
                    rgAccurableRecurring.MasterTableView.Columns.FindByUniqueName("ShowInputForm").Visible = false;
                    rgAccurableRecurring.MasterTableView.Columns.FindByUniqueName("CheckboxSelectColumn").Visible = false;
                    GridHelper.ExportGridToExcel(rgAccurableRecurring, PopupHelper.GetPageTitle(this.RecCategoryID.Value, this.RecCategoryTypeID.Value));
                }
            }
            catch (Exception)
            {
                // Helper.ShowErrorMessage(this, ex);
            }
        }

        #endregion

        public void ompage_ReconciliationPeriodChangedEventHandler(object sender, EventArgs e)
        {
            try
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
            catch (ARTException)
            {
                //Helper.HidePanel(updpnlMain, ex);
                //Helper.ShowErrorMessage(this, ex);
            }
            catch (Exception)
            {
                //Helper.ShowErrorMessage(this, ex);
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
                    long glRecItemID = Convert.ToInt64(item["GLDataRecurringItemScheduleID"].Text);
                    glRecItemInputIdCollection.Add(glRecItemID);
                }

                IGLDataRecItemSchedule oGLDataRecItemScheduleClient = RemotingHelper.GetGLDataRecItemScheduleObject();

                oGLDataRecItemScheduleClient.UpdateGLDataRecurringItemScheduleCloseDate(this.GLDataID.Value, glRecItemInputIdCollection
                    , null, null, null, null, this.RecCategoryTypeID.Value, (short)ARTEnums.AccountAttribute.ReconciliationTemplate
                    , SessionHelper.GetCurrentUser().LoginID, DateTime.Now, SessionHelper.CurrentReconciliationPeriodID.Value, Helper.GetAppUserInfo());

                this._GLDataRecurringItemScheduleInfoCollection = null;
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

        private void PopulateGrids()
        {

            if (this.GLDataID == null || this.GLDataID == 0)
            {
                this.GetGLRecItemInfoCollection = null;
                this.rgAccurableRecurring.DataSource = new List<GLDataRecurringItemScheduleInfo>();
                rgAccurableRecurring.DataBind();
                this.rgGLAdjustmentCloseditems.DataSource = new List<GLDataRecurringItemScheduleInfo>();
                this.rgGLAdjustmentCloseditems.DataBind();
            }
            else
            {
                rgAccurableRecurring.EntityNameText = PopupHelper.GetPageTitle(this.RecCategoryID.Value, this.RecCategoryTypeID.Value);

                List<GLDataRecurringItemScheduleInfo> oGLDataRecurringItemScheduleInfoCollection = this.GetGLRecItemInfoCollection.Where(recItem => recItem.CloseDate == null).ToList();
                rgAccurableRecurring.DataSource = oGLDataRecurringItemScheduleInfoCollection;
                rgAccurableRecurring.DataBind();

                if (oGLDataRecurringItemScheduleInfoCollection == null || oGLDataRecurringItemScheduleInfoCollection.Count == 0)
                {
                    btnDelete.Visible = false;
                }
                else
                {
                    btnDelete.Visible = true;
                }

                List<GLDataRecurringItemScheduleInfo> oGLDataRecurringItemScheduleInfoCollectionClosed = this.GetGLRecItemInfoCollection.Where(recItem => recItem.CloseDate != null).ToList();

                if (oGLDataRecurringItemScheduleInfoCollectionClosed == null || oGLDataRecurringItemScheduleInfoCollectionClosed.Count == 0)
                {
                    pnlClosedItems.Visible = false;
                }
                else
                {
                    pnlClosedItems.Visible = true;
                    this.rgGLAdjustmentCloseditems.DataSource = oGLDataRecurringItemScheduleInfoCollectionClosed;
                    this.rgGLAdjustmentCloseditems.DataBind();
                }
            }
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