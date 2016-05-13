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
using SkyStem.ART.Web.Data;
using SkyStem.Library.Controls.WebControls;
using Telerik.Web.UI;
using SkyStem.ART.Client.Model;
using SkyStem.ART.Client.Exception;
using SkyStem.ART.Client.IServices;
using System.Collections.Generic;
using SkyStem.Library.Controls.TelerikWebControls.Data;
using SkyStem.ART.Client.Data;
using SkyStem.Language.LanguageUtility;
using SkyStem.ART.Web.Classes;

namespace SkyStem.ART.Web.UserControls
{
    public partial class UserControls_ItemInputWriteOff : UserControlRecItemBase
    {

        public override bool DisableExportInPrint
        {
            set
            {
                DisableCommandItemForPrint(rgItemInputWO);
                DisableCommandItemForPrint(rgDataWriteOnOffCloseditems);
            }
        }

        #region "Variable Declaration"
        bool isExportPDF;
        bool isExportExcel;

        public short _gLReconciliationItemInputRecordTypeID = 0;
        private short _UserRole = 0;
        private short _RecPeriodStatus = 0;
        private decimal? _ReportingCurrencyTotal = 0;
        private decimal? _BaseCurrencyTotal = 0;
        static bool _isCapabilityMultiCurrencyAccount = false;
        private List<GLDataWriteOnOffInfo> _oGLDataWOCollection;
        private bool? _IsExpandedValueForced = null;
        ExImageButton ToggleControl;

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

        private bool _IsRegisterPDFAndExcelForPostback = true;
        public bool IsRegisterPDFAndExcelForPostback
        {
            get { return _IsRegisterPDFAndExcelForPostback; }
            set { _IsRegisterPDFAndExcelForPostback = value; }
        }

        #endregion
        protected void Page_Init(object sender, EventArgs e)
        {
            try
            {
                //MasterPageBase ompage = (MasterPageBase)this.Master.Master;
                //ompage.ReconciliationPeriodChangedEventHandler += new EventHandler(ompage_ReconciliationPeriodChangedEventHandler);
                Helper.SetGridImageButtonProperties(this.rgItemInputWO.MasterTableView.Columns);
                this.rgItemInputWO.EntityNameLabelID = 1625;
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
            if (!IsPostBack)
            {
                isExportPDF = false;
                isExportExcel = false;
            }
            SetControlState();

            HandlePrintMode(trOpenItemsButtonRow, trClosedItemsButtonRow, rgItemInputWO);
            HandlePrintMode(trOpenItemsButtonRow, trClosedItemsButtonRow, rgDataWriteOnOffCloseditems);


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
                    isExportExcel = false;
                    isExportPDF = false;
                }
                updpnlMain.Visible = true;
                SetPrivateVariables();
                _gLReconciliationItemInputRecordTypeID = (short)WebEnums.RecordType.GLReconciliationItemInput;

                btnAdd.OnClientClick = PopupHelper.GetJavascriptParameterListForEditRecItem(null, "OpenRadWindowWithName", "EditItemWriteOffOn.aspx", QueryStringConstants.INSERT, false, this.AccountID, this.GLDataID, this.RecCategoryTypeID, this.NetAccountID, this.IsSRA, RecCategoryID, hdIsRefreshData.ClientID, this.CurrentBCCY);
                btnAdd.Attributes.Add("onclick", "return false;");


                btnClose.OnClientClick = PopupHelper.GetJavascriptParameterList(null, "OpenRadWindow", "BulkCloseWriteOffOn.aspx", QueryStringConstants.INSERT, false, this.AccountID, this.GLDataID, this.RecCategoryTypeID, this.NetAccountID, this.IsSRA, RecCategoryID, hdIsRefreshData.ClientID);
                PopulateGrids();

                CalculateAndDisplaySum();
                EnableDisableControlsForNonPreparersAndClosedPeriods();
                EnableDisableBulkCloseButton();
                SetTotalTB(this.GLDataID);
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
            if (GLDataID.Value == 0)
            {
                throw new ARTException(5000116);
            }
            else
            {
                // Helper.HideMessage(this);
            }
        }

        private void EnableDisableBulkCloseButton()
        {

            if (this._oGLDataWOCollection == null)
                btnClose.Enabled = false;
            else
            {


                int numberOfOldOpenItems = (from recItem in this._oGLDataWOCollection
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

            if (Helper.GetFormMode(WebEnums.ARTPages.ItemInputWriteOnOff, this.GLDataHdrInfo) == WebEnums.FormMode.Edit
                && this.GLDataID > 0)
            {
                btnAdd.Visible = true;
                btnClose.Visible = true;
                btnReopen.Visible = true;
                rgDataWriteOnOffCloseditems.Columns[0].Visible = true;
            }
            else
            {
                btnAdd.Visible = false;
                btnClose.Visible = false;
                btnReopen.Visible = false;
                rgDataWriteOnOffCloseditems.Columns[0].Visible = false;
            }
        }



        private void CalculateAndDisplaySum()
        {
            this._BaseCurrencyTotal = (from recItem in this._oGLDataWOCollection
                                       where recItem.CloseDate == null
                                       select recItem.AmountBaseCurrency).Sum();

            this._ReportingCurrencyTotal = (from recItem in this._oGLDataWOCollection
                                            where recItem.CloseDate == null
                                            select recItem.AmountReportingCurrency).Sum();


        }

        #region "Rad Grid EventHandlers"

        protected void rgItemInputWO_DeleteCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
        }

        protected void rgItemInputWO_ItemDataBound(object sender, GridItemEventArgs e)
        {
            try
            {
                CalculateAndDisplaySum();
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

                    if (NetAccountID > 0)
                    {
                        if (oControlAmountBaseCurrency is LinkButton)
                        {
                            //BCCY Changes
                            //((LinkButton)oControlAmountBaseCurrency).Text = Helper.GetLabelIDValue(1673) + " (" + SessionHelper.BaseCurrencyCode + ")";
                            ((LinkButton)oControlAmountBaseCurrency).Text = Helper.GetLabelIDValue(1673);

                        }
                        else
                        {
                            if (oControlAmountBaseCurrency is LiteralControl)
                            {
                                //BCCY Changes
                                //((LiteralControl)oControlAmountBaseCurrency).Text = Helper.GetLabelIDValue(1673) + " (" + SessionHelper.BaseCurrencyCode + ")";
                                ((LiteralControl)oControlAmountBaseCurrency).Text = Helper.GetLabelIDValue(1673);
                            }
                        }
                    }
                    else
                    {
                        if (oControlAmountBaseCurrency is LinkButton)
                        {
                            //BCCY Changes
                            //((LinkButton)oControlAmountBaseCurrency).Text = Helper.GetLabelIDValue(1673) + " (" + SessionHelper.BaseCurrencyCode + ")";
                            ((LinkButton)oControlAmountBaseCurrency).Text = Helper.GetLabelIDValue(1673) + " (" + Helper.GetDisplayBaseCurrencyCode(this.CurrentBCCY) + ")";

                        }
                        else
                        {
                            if (oControlAmountBaseCurrency is LiteralControl)
                            {
                                //BCCY Changes
                                //((LiteralControl)oControlAmountBaseCurrency).Text = Helper.GetLabelIDValue(1673) + " (" + SessionHelper.BaseCurrencyCode + ")";
                                ((LiteralControl)oControlAmountBaseCurrency).Text = Helper.GetLabelIDValue(1673) + " (" + Helper.GetDisplayBaseCurrencyCode(this.CurrentBCCY) + ")";
                            }
                        }
                    }


                    //******************************************************************************************************************************************
                }

                GLDataWriteOnOffInfo oGLDataWriteOnOffInfo = (GLDataWriteOnOffInfo)e.Item.DataItem;
                if (oGLDataWriteOnOffInfo != null)
                {
                    GridHelper.SetCSSClassForForwardedItems(e, oGLDataWriteOnOffInfo.IsForwardedItem);

                    ExLabel lblComments = (ExLabel)e.Item.FindControl("lblComments");
                    if (isExportExcel || isExportPDF)
                        lblComments.Text = oGLDataWriteOnOffInfo.Comments;
                    else
                        Helper.SetTextAndTooltipValue(lblComments, oGLDataWriteOnOffInfo.Comments);
                    ExLabel lblAmount = (ExLabel)e.Item.FindControl("lblAmount");
                    lblAmount.Text = Helper.GetDisplayCurrencyValue(oGLDataWriteOnOffInfo.LocalCurrencyCode, oGLDataWriteOnOffInfo.Amount);

                    ExLabel lblOpenDate = (ExLabel)e.Item.FindControl("lblOpenDate");
                    lblOpenDate.Text = Helper.GetDisplayDate(oGLDataWriteOnOffInfo.OpenDate);

                    ExLabel lblAging = (ExLabel)e.Item.FindControl("lblAging");
                    lblAging.Text = Helper.GetDisplayIntegerValue(Helper.GetDaysBetweenDateRanges(oGLDataWriteOnOffInfo.OpenDate, DateTime.Now));

                    ExLabel lblRecItemNumber = (ExLabel)e.Item.FindControl("lblRecItemNumber");
                    lblRecItemNumber.Text = Helper.GetDisplayStringValue(oGLDataWriteOnOffInfo.RecItemNumber);

                    //************* MatchSetRefNo********************************************************
                    RecHelper.SetMatchSetRefNumberUrlForGLDataWriteOnOff(e, oGLDataWriteOnOffInfo, AccountID, NetAccountID, GLDataID);
                    //************************************************************************************

                    SetCapabilityFlags(SessionHelper.CurrentReconciliationPeriodID);
                    if (!_isCapabilityMultiCurrencyAccount)
                    {


                        if (e.Item.ItemType == GridItemType.Header)
                        {
                            Control oControlLocalCurrencyCode = new Control();
                            oControlLocalCurrencyCode = (e.Item as GridHeaderItem)["LocalCurrencyCode"].Controls[0];
                            if (oControlLocalCurrencyCode is LinkButton)
                            {
                                ((LinkButton)oControlLocalCurrencyCode).Text = Helper.GetLabelIDValue(1409);

                            }
                            else
                            {
                                if (oControlLocalCurrencyCode is LiteralControl)
                                {
                                    ((LiteralControl)oControlLocalCurrencyCode).Text = Helper.GetLabelIDValue(1409);

                                }
                            }
                            //******************************************************************************************************************************************


                        }
                        rgItemInputWO.Columns[1].Visible = false;
                    }


                    ExHyperLink hlShowItemInputForm = (ExHyperLink)e.Item.FindControl("hlShowItemInputForm");
                    if (e.Item.ItemType == Telerik.Web.UI.GridItemType.Item ||
                         e.Item.ItemType == Telerik.Web.UI.GridItemType.AlternatingItem)
                    {
                        string mode = "";
                        if (Helper.GetFormMode(WebEnums.ARTPages.ItemInputWriteOnOff, this.GLDataHdrInfo) == WebEnums.FormMode.Edit)
                        {
                            mode = QueryStringConstants.EDIT;
                            Helper.SetImageURLForViewVersusEdit(WebEnums.FormMode.Edit, hlShowItemInputForm);
                        }
                        else
                        {
                            mode = QueryStringConstants.READ_ONLY;
                            Helper.SetImageURLForViewVersusEdit(WebEnums.FormMode.ReadOnly, hlShowItemInputForm);
                        }
                        hlShowItemInputForm.NavigateUrl = PopupHelper.GetJavascriptParameterListForEditRecItem(
                            oGLDataWriteOnOffInfo.GLDataWriteOnOffID,
                            "OpenRadWindowForHyperlinkWithName",
                            "EditItemWriteOffOn.aspx",
                            mode,
                            oGLDataWriteOnOffInfo.IsForwardedItem.GetValueOrDefault(),
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
                        if (Helper.GetFormMode(WebEnums.ARTPages.ItemInputWriteOnOff, this.GLDataHdrInfo) == WebEnums.FormMode.Edit)
                        {
                            if (oGLDataWriteOnOffInfo.IsForwardedItem.HasValue && oGLDataWriteOnOffInfo.IsForwardedItem.Value)
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
                        deleteButton.CommandArgument = oGLDataWriteOnOffInfo.GLDataWriteOnOffID.ToString();
                    }
                }

                if (e.Item.ItemType == GridItemType.Footer)
                {
                    ExLabel lblBaseCurrencyTotal = (ExLabel)e.Item.FindControl("lblBaseCurrencyTotal");
                    ExLabel lblReportingCurrencyTotal = (ExLabel)e.Item.FindControl("lblReportingCurrencyTotal");
                    lblBaseCurrencyTotal.Text = Helper.GetDisplayDecimalValue(this._BaseCurrencyTotal);
                    lblReportingCurrencyTotal.Text = Helper.GetDisplayDecimalValue(this._ReportingCurrencyTotal);
                    ExLabel lblTotal = (ExLabel)e.Item.FindControl("lblTotal");
                    lblTotal.Text = LanguageUtil.GetValue(1656);
                }
            }
            catch (ARTException)
            {

            }
            catch (Exception)
            {

            }
        }



        protected void rgItemInputWO_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            try
            {
                List<GLDataWriteOnOffInfo> oGLDataWOCollectionFiltered = this._oGLDataWOCollection.Where(recItem => recItem.CloseDate == null).ToList();
                this.rgItemInputWO.DataSource = oGLDataWOCollectionFiltered;
            }
            catch (ARTException)
            {
            }
            catch (Exception)
            {
            }
        }


        protected void rgDataWriteOnOffCloseditems_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            try
            {
                CalculateAndDisplaySum();
                GLDataWriteOnOffInfo oGLDataWriteOnOffInfo = (GLDataWriteOnOffInfo)e.Item.DataItem;
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

                    if (NetAccountID > 0)
                    {
                        if (oControlAmountBaseCurrency is LinkButton)
                        {
                            //BCCY Changes
                            //((LinkButton)oControlAmountBaseCurrency).Text = Helper.GetLabelIDValue(1673) + " (" + SessionHelper.BaseCurrencyCode + ")";
                            ((LinkButton)oControlAmountBaseCurrency).Text = Helper.GetLabelIDValue(1673);

                        }
                        else
                        {
                            if (oControlAmountBaseCurrency is LiteralControl)
                            {
                                //BCCY Changes
                                //((LiteralControl)oControlAmountBaseCurrency).Text = Helper.GetLabelIDValue(1673) + " (" + SessionHelper.BaseCurrencyCode + ")";
                                ((LiteralControl)oControlAmountBaseCurrency).Text = Helper.GetLabelIDValue(1673);

                            }
                        }
                    }
                    else
                    {
                        if (oControlAmountBaseCurrency is LinkButton)
                        {
                            //BCCY Changes
                            //((LinkButton)oControlAmountBaseCurrency).Text = Helper.GetLabelIDValue(1673) + " (" + SessionHelper.BaseCurrencyCode + ")";
                            ((LinkButton)oControlAmountBaseCurrency).Text = Helper.GetLabelIDValue(1673) + " (" + Helper.GetDisplayBaseCurrencyCode(this.CurrentBCCY) + ")";

                        }
                        else
                        {
                            if (oControlAmountBaseCurrency is LiteralControl)
                            {
                                //BCCY Changes
                                //((LiteralControl)oControlAmountBaseCurrency).Text = Helper.GetLabelIDValue(1673) + " (" + SessionHelper.BaseCurrencyCode + ")";
                                ((LiteralControl)oControlAmountBaseCurrency).Text = Helper.GetLabelIDValue(1673) + " (" + Helper.GetDisplayBaseCurrencyCode(this.CurrentBCCY) + ")";

                            }
                        }
                    }

                    //******************************************************************************************************************************************





                }

                if (!_isCapabilityMultiCurrencyAccount)
                {

                    if (e.Item.ItemType == GridItemType.Header)
                    {
                        ////((LinkButton)(e.Item as GridHeaderItem)["LocalCurrencyCode"].Controls[0]).Text = Helper.GetLabelIDValue(1409) + " (" + oGLDataWriteOnOffInfo.LocalCurrencyCode + ")";
                        //***************Above code commented and replaced by below code to handle the export to pdf of Grid
                        Control oControlLocalCurrencyCode = new Control();
                        oControlLocalCurrencyCode = (e.Item as GridHeaderItem)["LocalCurrencyCode"].Controls[0];
                        if (oControlLocalCurrencyCode is LinkButton)
                        {
                            ((LinkButton)oControlLocalCurrencyCode).Text = Helper.GetLabelIDValue(1409);

                        }
                        else
                        {
                            if (oControlLocalCurrencyCode is LiteralControl)
                            {
                                ((LiteralControl)oControlLocalCurrencyCode).Text = Helper.GetLabelIDValue(1409);

                            }
                        }
                        //******************************************************************************************************************************************


                    }
                    rgItemInputWO.Columns[1].Visible = false;


                }



                if (e.Item.ItemType == Telerik.Web.UI.GridItemType.Item || e.Item.ItemType == Telerik.Web.UI.GridItemType.AlternatingItem)
                {
                    ExLabel lblAmount = (ExLabel)e.Item.FindControl("lblAmount");
                    lblAmount.Text = Helper.GetDisplayCurrencyValue(oGLDataWriteOnOffInfo.LocalCurrencyCode, oGLDataWriteOnOffInfo.Amount);

                    ExLabel lblOpenDate = (ExLabel)e.Item.FindControl("lblOpenDate");
                    lblOpenDate.Text = Helper.GetDisplayDate(oGLDataWriteOnOffInfo.OpenDate);

                    ExLabel lblCloseDate = (ExLabel)e.Item.FindControl("lblCloseDate");
                    lblCloseDate.Text = Helper.GetDisplayDate(oGLDataWriteOnOffInfo.CloseDate);
                    ExLabel lblAging = (ExLabel)e.Item.FindControl("lblAging");
                    lblAging.Text = Helper.GetDisplayIntegerValue(Helper.GetDaysBetweenDateRanges(oGLDataWriteOnOffInfo.OpenDate, oGLDataWriteOnOffInfo.CloseDate));

                    ExLabel lblRecItemNumber = (ExLabel)e.Item.FindControl("lblRecItemNumber");
                    lblRecItemNumber.Text = Helper.GetDisplayStringValue(oGLDataWriteOnOffInfo.RecItemNumber);

                    //************* MatchSetRefNo********************************************************
                    RecHelper.SetMatchSetRefNumberUrlForGLDataWriteOnOff(e, oGLDataWriteOnOffInfo, AccountID, NetAccountID, GLDataID);
                    //************************************************************************************

                    ExHyperLink hlShowItemInputForm = (ExHyperLink)e.Item.FindControl("hlShowItemInputForm");
                    Helper.SetImageURLForViewVersusEdit(WebEnums.FormMode.ReadOnly, hlShowItemInputForm);
                    hlShowItemInputForm.NavigateUrl = PopupHelper.GetJavascriptParameterListForEditRecItem(
                        oGLDataWriteOnOffInfo.GLDataWriteOnOffID,
                        "OpenRadWindowForHyperlinkWithName",
                        "EditItemWriteOffOn.aspx",
                        QueryStringConstants.READ_ONLY,
                        oGLDataWriteOnOffInfo.IsForwardedItem.GetValueOrDefault(),
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
            catch (ARTException)
            {
            }
            catch (Exception)
            {
            }
        }

        protected void rgDataWriteOnOffCloseditems_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            try
            {
                List<GLDataWriteOnOffInfo> oGLDataWriteOnOffInfoCollection = this._oGLDataWOCollection.Where(recItem => recItem.CloseDate != null).ToList();

                if (oGLDataWriteOnOffInfoCollection == null)
                    oGLDataWriteOnOffInfoCollection = new List<GLDataWriteOnOffInfo>();

                if (oGLDataWriteOnOffInfoCollection == null || oGLDataWriteOnOffInfoCollection.Count == 0)
                {
                    pnlClosedItems.Visible = false;
                }
                else
                {
                    pnlClosedItems.Visible = true;
                }

                this.rgDataWriteOnOffCloseditems.DataSource = oGLDataWriteOnOffInfoCollection;
            }
            catch (ARTException)
            {
            }
            catch (Exception)
            {
            }
        }

        private DataTable GetGLDataParams()
        {
            DataTable dtGLData = new DataTable();
            dtGLData.Columns.Add("ID");

            foreach (GridDataItem item in rgItemInputWO.SelectedItems)
            {
                DataRow rowGLData = dtGLData.NewRow();
                rowGLData["ID"] = item.GetDataKeyValue("GLDataWriteOnOffID");
                dtGLData.Rows.Add(rowGLData);
            }

            return dtGLData;
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            DataTable dtGLDataParams = new DataTable();
            dtGLDataParams = GetGLDataParams();

            long? GLDataWriteOnOffID = 0;
            IGLDataWriteOnOff oGLDataWriteOnOff = RemotingHelper.GetGLDataWriteOnOffObject();
            oGLDataWriteOnOff.DeleteGLDataWriteOnOff(GLDataWriteOnOffID, this.GLDataID, this.RecCategoryTypeID, SessionHelper.CurrentUserLoginID, DateTime.Now, dtGLDataParams, Helper.GetAppUserInfo());

            // get the data and rebind
            this._oGLDataWOCollection = null;
            this._oGLDataWOCollection = oGLDataWriteOnOff.GetGLDataWriteOnOffInfoCollectionByGLDataID(this.GLDataID, (int)ARTEnums.AccountAttribute.ReconciliationTemplate, Helper.GetAppUserInfo());
            List<GLDataWriteOnOffInfo> oGLDataWriteOnOffInfoCollection = this._oGLDataWOCollection.Where(recItem => recItem.CloseDate == null).ToList();
            rgItemInputWO.DataSource = oGLDataWriteOnOffInfoCollection;
            rgItemInputWO.DataBind();

            CalculateAndDisplaySum();
            RecHelper.RefreshRecForm(this);
        }

        protected void rgItemInputWO_ItemCommand(object source, GridCommandEventArgs e)
        {
            try
            {

                if (e.CommandName == "Delete")
                {
                    //oGLRecItemInputClient.DeleteRecInputItem(oGLDataWriteOnOffInfo, (short)ARTEnums.AccountAttribute.ReconciliationTemplate);

                    DataTable dtGLDataParams = new DataTable();
                    dtGLDataParams.Columns.Add("ID");
                    DataRow drGLData = dtGLDataParams.NewRow();
                    drGLData["ID"] = Convert.ToInt64(e.CommandArgument);
                    dtGLDataParams.Rows.Add(drGLData);

                    long? GLDataWriteOnOffID = Convert.ToInt64(e.CommandArgument);
                    IGLDataWriteOnOff oGLDataWriteOnOff = RemotingHelper.GetGLDataWriteOnOffObject();
                    oGLDataWriteOnOff.DeleteGLDataWriteOnOff(GLDataWriteOnOffID, this.GLDataID, this.RecCategoryTypeID, SessionHelper.CurrentUserLoginID, DateTime.Now, dtGLDataParams, Helper.GetAppUserInfo());

                    // get the data and rebind
                    this._oGLDataWOCollection = null;
                    this._oGLDataWOCollection = oGLDataWriteOnOff.GetGLDataWriteOnOffInfoCollectionByGLDataID(this.GLDataID, (int)ARTEnums.AccountAttribute.ReconciliationTemplate, Helper.GetAppUserInfo());
                    List<GLDataWriteOnOffInfo> oGLDataWriteOnOffInfoCollection = this._oGLDataWOCollection.Where(recItem => recItem.CloseDate == null).ToList();
                    rgItemInputWO.DataSource = oGLDataWriteOnOffInfoCollection;
                    rgItemInputWO.DataBind();

                    CalculateAndDisplaySum();
                    RecHelper.RefreshRecForm(this);
                }
                if (e.CommandName == TelerikConstants.GridExportToPDFCommandName)
                {

                    isExportPDF = true;
                    //rgGLAdjustments.MasterTableView.Columns.FindByUniqueName("CheckboxSelectColumn").Visible = false;
                    rgItemInputWO.MasterTableView.Columns.FindByUniqueName("DeleteColumn").Visible = false;
                    rgItemInputWO.MasterTableView.Columns.FindByUniqueName("ShowInputForm").Visible = false;
                    rgItemInputWO.MasterTableView.Columns.FindByUniqueName("CheckboxSelectColumn").Visible = false;
                    GridHelper.ExportGridToPDF(rgItemInputWO, PopupHelper.GetPageTitle(this.RecCategoryID.Value, this.RecCategoryTypeID.Value));
                }
                if (e.CommandName == TelerikConstants.GridExportToExcelCommandName)
                {

                    isExportExcel = true;
                    //rgGLAdjustments.MasterTableView.Columns.FindByUniqueName("CheckboxSelectColumn").Visible = false;
                    rgItemInputWO.MasterTableView.Columns.FindByUniqueName("DeleteColumn").Visible = false;
                    rgItemInputWO.MasterTableView.Columns.FindByUniqueName("ShowInputForm").Visible = false;
                    rgItemInputWO.MasterTableView.Columns.FindByUniqueName("CheckboxSelectColumn").Visible = false;
                    GridHelper.ExportGridToExcel(rgItemInputWO, PopupHelper.GetPageTitle(this.RecCategoryID.Value, this.RecCategoryTypeID.Value));
                }

            }
            catch (ARTException)
            {

            }
            catch (Exception)
            {

            }
        }

        #endregion

        public string GetMenuKey()
        {
            return "AccountViewer";
        }

        public void ompage_ReconciliationPeriodChangedEventHandler(object sender, EventArgs e)
        {
            try
            {
                this._oGLDataWOCollection = null;
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
            }
            catch (Exception)
            {

            }
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
                List<long> glDataWriteOnOffIdCollection = new List<long>();

                foreach (GridDataItem item in rgDataWriteOnOffCloseditems.SelectedItems)
                {
                    long gLDataWriteOnOffID = Convert.ToInt64(item["GLDataWriteOnOffID"].Text);
                    glDataWriteOnOffIdCollection.Add(gLDataWriteOnOffID);
                }

                IGLDataWriteOnOff oGLDataWriteOnOffClient = RemotingHelper.GetGLDataWriteOnOffObject();
                oGLDataWriteOnOffClient.UpdateGLDataWriteOnOffCloseDate(this.GLDataID.Value, glDataWriteOnOffIdCollection
                    , null, null, null, null, this.RecCategoryTypeID.Value, (short)ARTEnums.AccountAttribute.ReconciliationTemplate
                    , SessionHelper.GetCurrentUser().LoginID, DateTime.Now
                    , SessionHelper.CurrentReconciliationPeriodID.Value, Helper.GetAppUserInfo());

                this._oGLDataWOCollection = null;
                PopulateGrids();

                EnableDisableBulkCloseButton();
                CalculateAndDisplaySum();
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
                this._oGLDataWOCollection = null;
                this.rgItemInputWO.DataSource = new List<GLDataWriteOnOffInfo>();
                rgItemInputWO.DataBind();
                this.rgDataWriteOnOffCloseditems.DataSource = new List<GLDataWriteOnOffInfo>();
                this.rgDataWriteOnOffCloseditems.DataBind();
            }
            else
            {

                rgItemInputWO.EntityNameText = PopupHelper.GetPageTitle(this.RecCategoryID.Value, this.RecCategoryTypeID.Value);
                IGLDataWriteOnOff oGLDataWriteOnOff = RemotingHelper.GetGLDataWriteOnOffObject();
                this._oGLDataWOCollection = oGLDataWriteOnOff.GetGLDataWriteOnOffInfoCollectionByGLDataID(this.GLDataID, (int)ARTEnums.AccountAttribute.ReconciliationTemplate, Helper.GetAppUserInfo());

                List<GLDataWriteOnOffInfo> oGLDataWriteOnOffInfoCollection = this._oGLDataWOCollection.Where(recItem => recItem.CloseDate == null).ToList();
                rgItemInputWO.DataSource = oGLDataWriteOnOffInfoCollection;
                rgItemInputWO.DataBind();

                if (oGLDataWriteOnOffInfoCollection == null || oGLDataWriteOnOffInfoCollection.Count == 0)
                {
                    btnDelete.Visible = false;
                }
                else
                {
                    btnDelete.Visible = true;
                }

                List<GLDataWriteOnOffInfo> oGLDataWriteOnOffInfoCollectionClosed = this._oGLDataWOCollection.Where(recItem => recItem.CloseDate != null).ToList();

                if (oGLDataWriteOnOffInfoCollectionClosed == null || oGLDataWriteOnOffInfoCollectionClosed.Count == 0)
                {
                    pnlClosedItems.Visible = false;
                }
                else
                {
                    pnlClosedItems.Visible = true;
                    this.rgDataWriteOnOffCloseditems.DataSource = oGLDataWriteOnOffInfoCollectionClosed;
                    this.rgDataWriteOnOffCloseditems.DataBind();
                }

                if (oGLDataWriteOnOffInfoCollection != null)
                {
                    int carryForwardedItems = GetCarryForwardedItemCount(oGLDataWriteOnOffInfoCollection);
                    if (Helper.GetFormMode(WebEnums.ARTPages.GLAdjustments, this.GLDataHdrInfo) == WebEnums.FormMode.Edit)
                    {
                        if (carryForwardedItems == oGLDataWriteOnOffInfoCollection.Count)
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

        private int GetCarryForwardedItemCount(List<GLDataWriteOnOffInfo> objGLDataRecItemInfo)
        {
            int countCarryForwardedItem = 0;
            foreach (GLDataWriteOnOffInfo glDataInfo in objGLDataRecItemInfo)
            {
                if (glDataInfo.IsForwardedItem.HasValue && glDataInfo.IsForwardedItem.Value)
                {
                    countCarryForwardedItem += 1;
                }
            }

            return countCarryForwardedItem;
        }

        private void SetTotalTB(long? gLDataID)
        {
            IGLDataWriteOnOff oGLDataWriteOnOff = RemotingHelper.GetGLDataWriteOnOffObject();
            List<GLDataWriteOnOffInfo> oGLDataWriteOnOffInfoCollection = oGLDataWriteOnOff.GetTotalGLDataWriteOnByGLDataID(gLDataID, (int)ARTEnums.AccountAttribute.ReconciliationTemplate, Helper.GetAppUserInfo());
            if (oGLDataWriteOnOffInfoCollection != null && oGLDataWriteOnOffInfoCollection.Count > 0)
            {
                this._ReportingCurrencyTotal = (from writeOnOff in this._oGLDataWOCollection
                                                where writeOnOff.CloseDate == null
                                                select writeOnOff.AmountReportingCurrency).Sum();
                //lblTotalRecWriteOffRC.Text = Helper.GetDisplayDecimalValue(oGLDataWriteOnOffInfoCollection[0].AmountInReportingCurrency);
            }
        }


        private void ShowHideButtonBasedOnUserRole()
        {
            WebEnums.UserRole eUserRole = (WebEnums.UserRole)SessionHelper.CurrentRoleID;
            switch (eUserRole)
            {
                case WebEnums.UserRole.PREPARER:
                    btnAdd.Visible = true;
                    break;
                default:
                    btnAdd.Visible = false;
                    break;
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

            }
            catch (Exception)
            {

            }
        }


        private void SetCapabilityFlags(int? recPeriodID)
        {
            ICompany oCompanyClient = RemotingHelper.GetCompanyObject();
            List<CompanyCapabilityInfo> oCompanyCapabilityInfoCollection = null;
            oCompanyCapabilityInfoCollection = oCompanyClient.SelectAllCompanyCapabilityByReconciliationID(recPeriodID, Helper.GetAppUserInfo());
            _isCapabilityMultiCurrencyAccount = false;

            foreach (CompanyCapabilityInfo oCompanyCapabilityInfo in oCompanyCapabilityInfoCollection)
            {
                switch (oCompanyCapabilityInfo.CapabilityID)
                {
                    case (short)ARTEnums.Capability.MultiCurrency://Dual Level Review
                        if (oCompanyCapabilityInfo.IsActivated.HasValue)
                        {
                            _isCapabilityMultiCurrencyAccount = oCompanyCapabilityInfo.IsActivated.Value;
                        }
                        break;

                }
            }
        }




        protected void rgItemInputWO_ItemCreated(object sender, GridItemEventArgs e)
        {
            // Register PDF / Excel for Postback
            if (_IsRegisterPDFAndExcelForPostback)
                GridHelper.RegisterPDFAndExcelForPostback(e, isExportPDF, isExportExcel, this.Page);
            //GridHelper.RegisterPDFAndExcelForPostback(e, isExportPDF, isExportExcel, this.Page);
            GridHelper.SetStylesForExportGrid(e, isExportPDF, isExportExcel);
        }
        protected void rgDataWriteOnOffCloseditems_ItemCreated(object sender, GridItemEventArgs e)
        {
            // Register PDF / Excel for Postback
            if (_IsRegisterPDFAndExcelForPostback)
                GridHelper.RegisterPDFAndExcelForPostback(e, isExportPDF, isExportExcel, this.Page);
            //GridHelper.RegisterPDFAndExcelForPostback(e, isExportPDF, isExportExcel, this.Page);
            GridHelper.SetStylesForExportGrid(e, isExportPDF, isExportExcel);
        }


        protected void rgDataWriteOnOffCloseditems_ItemCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                CalculateAndDisplaySum();

                if (e.CommandName == TelerikConstants.GridExportToPDFCommandName)
                {

                    isExportPDF = true;

                    rgDataWriteOnOffCloseditems.MasterTableView.Columns.FindByUniqueName("ShowInputForm").Visible = false;
                    rgDataWriteOnOffCloseditems.MasterTableView.Columns.FindByUniqueName("CheckboxSelectColumn").Visible = false;
                    GridHelper.ExportGridToPDF(rgDataWriteOnOffCloseditems, PopupHelper.GetPageTitle(this.RecCategoryID.Value, this.RecCategoryTypeID.Value));
                }
                if (e.CommandName == TelerikConstants.GridExportToExcelCommandName)
                {
                    isExportExcel = true;

                    rgDataWriteOnOffCloseditems.MasterTableView.Columns.FindByUniqueName("ShowInputForm").Visible = false;
                    rgDataWriteOnOffCloseditems.MasterTableView.Columns.FindByUniqueName("CheckboxSelectColumn").Visible = false;
                    GridHelper.ExportGridToExcel(rgDataWriteOnOffCloseditems, PopupHelper.GetPageTitle(this.RecCategoryID.Value, this.RecCategoryTypeID.Value));
                }


            }
            catch (ARTException)
            {

            }
            catch (Exception)
            {

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
