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
using SkyStem.ART.Web.Data;
using System.Text;
using SkyStem.Library.Controls.WebControls;
using SkyStem.ART.Client.Data;
using SkyStem.ART.Client.Exception;
using SkyStem.Library.Controls.TelerikWebControls.Data;
using SkyStem.ART.Shared.Utility;

public partial class UserControls_ItemInputAmortizableTemplate : UserControlRecItemBase
{

    public override bool DisableExportInPrint
    {
        set
        {
            DisableCommandItemForPrint(rgAmortizable);
            DisableCommandItemForPrint(rgGLAdjustmentCloseditems);
        }
    }


    #region "Private Properties"
    ExImageButton ToggleControl;
    private bool? _IsExpandedValueForced = null;
    public short _GLReconciliationItemInputRecordTypeID = 0;
    private decimal? _ReportingCurrencyTotal = 0;
    private short _UserRole = 0;
    private short _RecPeriodStatus = 0;
    private List<GLDataRecurringItemScheduleInfo> _GLDataRecurringItemScheduleInfoCollection = null;
    bool isExportPDF;
    bool isExportExcel;
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
                    //Helper.ShowErrorMessage(this, ex);
                }
                catch (Exception)
                {
                    // Helper.ShowErrorMessage(this, ex);
                }

            }
            return this._GLDataRecurringItemScheduleInfoCollection;
        }
        set
        {
            this._GLDataRecurringItemScheduleInfoCollection = value;
        }
    }
    private decimal? _originalAmountRCCY;
    private decimal? _amortizedAmount;

    private decimal? _originalAmountRCCYClosedItem;
    private decimal? _amortizedAmountClosedItem;
    private decimal? _remainingAmountClosedItem;
    #endregion

    #region "Public Properties"

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

    public string GetMenuKey()
    {
        return "AccountViewer";


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
        try
        {
            //if (!this.IsPostBack)
            //{
            //    this.btnCancel.PostBackUrl = Request.UrlReferrer.PathAndQuery;
            //}
            Helper.SetGridImageButtonProperties(this.rgAmortizable.MasterTableView.Columns);
            this.rgAmortizable.EntityNameLabelID = 1525;
            this.rgGLAdjustmentCloseditems.EntityNameLabelID = 1525;
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

    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsRefreshData && IsExpanded)
        {
            try
            {
                //Helper.HideMessage(this);
                //updpnlMain.Visible = true;
                OnPageLoad();
                if (!IsPostBack)
                {
                    isExportExcel = false;
                    isExportPDF = false;
                }
                else
                {
                    btnAdd.OnClientClick = PopupHelper.GetJavascriptParameterListForEditRecItem(null, "OpenRadWindowWithName", "EditItemAmortizable.aspx", QueryStringConstants.INSERT, false, this.AccountID, this.GLDataID, this.RecCategoryTypeID, this.NetAccountID, this.IsSRA, RecCategoryID, hdIsRefreshData.ClientID, this.CurrentBCCY);
                    btnAdd.Attributes.Add("onclick", "return false;");


                    btnClose.OnClientClick = PopupHelper.GetJavascriptParameterList(null, "OpenRadWindow", "BulkCloseAmortizable.aspx", QueryStringConstants.INSERT, false, this.AccountID, this.GLDataID, this.RecCategoryTypeID, this.NetAccountID, this.IsSRA, RecCategoryID, hdIsRefreshData.ClientID);
                    CalculateAndDisplaySum();
                }
            }
            catch (ARTException)
            {
                // Helper.HidePanel(updpnlMain, ex);
                //Helper.ShowErrorMessage(this, ex);
            }
            catch (Exception)
            {
                //Helper.ShowErrorMessage(this, ex);
            }
            RepopulateGrids();
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
        HandlePrintMode(trOpenItemsButtonRow, trClosedItemsButtonRow, rgAmortizable);
        HandlePrintMode(trOpenItemsButtonRow, trClosedItemsButtonRow, rgGLAdjustmentCloseditems);

        //********* Refresh Rec Period status *******************************************************
        if (IsPostBack)
        {
            RecHelper.ReloadRecPeriodsOnMasterPage(this);
        }



    }
    #endregion

    #region "Control events handlers"

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
            CalculateAndDisplaySum();
            RepopulateGrids();
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

    protected void rgAmortizable_ItemCreated(object sender, GridItemEventArgs e)
    {
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

    private DataTable GetGLDataParams()
    {
        DataTable dtGLData = new DataTable();
        dtGLData.Columns.Add("ID");

        foreach (GridDataItem item in rgAmortizable.SelectedItems)
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
        rgAmortizable.DataSource = oGLDataRecurringItemScheduleInfoCollection;
        rgAmortizable.DataBind();
        RecHelper.RefreshRecForm(this);
    }

    protected void rgAmortizable_ItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
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
                rgAmortizable.DataSource = oGLDataRecurringItemScheduleInfoCollection;
                rgAmortizable.DataBind();
                RecHelper.RefreshRecForm(this);

            }

            if (e.CommandName == TelerikConstants.GridExportToPDFCommandName)
            {

                isExportPDF = true;
                //rgGLAdjustments.MasterTableView.Columns.FindByUniqueName("CheckboxSelectColumn").Visible = false;
                rgAmortizable.MasterTableView.Columns.FindByUniqueName("DeleteColumn").Visible = false;
                rgAmortizable.MasterTableView.Columns.FindByUniqueName("ShowInputForm").Visible = false;
                rgAmortizable.MasterTableView.Columns.FindByUniqueName("CheckboxSelectColumn").Visible = false;
                GridHelper.ExportGridToPDF(rgAmortizable, PopupHelper.GetPageTitle(this.RecCategoryID.Value, this.RecCategoryTypeID.Value));
            }
            if (e.CommandName == TelerikConstants.GridExportToExcelCommandName)
            {

                isExportExcel = true;
                //rgGLAdjustments.MasterTableView.Columns.FindByUniqueName("CheckboxSelectColumn").Visible = false;
                rgAmortizable.MasterTableView.Columns.FindByUniqueName("DeleteColumn").Visible = false;
                rgAmortizable.MasterTableView.Columns.FindByUniqueName("ShowInputForm").Visible = false;
                rgAmortizable.MasterTableView.Columns.FindByUniqueName("CheckboxSelectColumn").Visible = false;
                GridHelper.ExportGridToExcel(rgAmortizable, PopupHelper.GetPageTitle(this.RecCategoryID.Value, this.RecCategoryTypeID.Value));
            }

        }
        catch (ARTException)
        {
        }
        catch (Exception)
        {
        }
    }

    protected void rgAmortizable_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
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
                this._amortizedAmount = 0.00M;

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
                this._originalAmountRCCY = this._originalAmountRCCY + oGLDataRecurringItemScheduleInfo.ScheduleAmountReportingCurrency;

                //Total Amortized Amount 
                ExLabel lblAmortizedAmountRCCY = (ExLabel)e.Item.FindControl("lblAmortizedAmountRCCY");
                lblAmortizedAmountRCCY.Text = Helper.GetDisplayDecimalValue(oGLDataRecurringItemScheduleInfo.RecPeriodAmountReportingCurrency);
                this._amortizedAmount = this._amortizedAmount + oGLDataRecurringItemScheduleInfo.RecPeriodAmountReportingCurrency;

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
                            //string url = "DownloadAttachment.aspx?" + QueryStringConstants.FILE_PATH + "=" + Server.UrlEncode(SharedHelper.GetDisplayFilePath(oDataImportHdrInfo.PhysicalPath));
                            //imgViewFile.OnClientClick = "document.location.href = '" + url + "';return false;";
                            string url = string.Format("Downloader?{0}={1}&", QueryStringConstants.HANDLER_ACTION, (Int32)WebEnums.HandlerActionType.DownloadDataImportFile);
                            url += "&" + QueryStringConstants.DATA_IMPORT_ID + "=" + oDataImportHdrInfo.DataImportID.GetValueOrDefault()
                            + "&" + QueryStringConstants.DATA_IMPORT_TYPE_ID + "=" + oDataImportHdrInfo.DataImportTypeID.GetValueOrDefault()
                            + "&" + QueryStringConstants.GLDATA_ID + "=" + this.GLDataID.GetValueOrDefault();

                            //imgFileType.OnClientClick = "document.location.href = '" + url + "';return false;";
                            imgViewFile.Attributes.Add("onclick", "javascript:{$get('" + ifDownloader.ClientID + "').src='" + url + "'; return false;}");
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
                hlShowItemInputForm.NavigateUrl = PopupHelper.GetJavascriptParameterListForEditRecItem(oGLDataRecurringItemScheduleInfo.GLDataRecurringItemScheduleID, "OpenRadWindowForHyperlinkWithName", "EditItemAmortizable.aspx", mode, _IsForwardedItem, this.AccountID, this.GLDataID, this.RecCategoryTypeID, this.NetAccountID, this.IsSRA, RecCategoryID, hdIsRefreshData.ClientID, this.CurrentBCCY);//IsforwardedItem is not used here so redundant



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
                lblAmortizedAmountRCCYTotalValue.Text = Helper.GetDisplayDecimalValue(this._amortizedAmount);

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

    protected void rgAmortizable_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
    {
        List<GLDataRecurringItemScheduleInfo> oGLDataRecurringItemScheduleInfoCollection = this.GetGLRecItemInfoCollection.Where(recItem => recItem.CloseDate == null).ToList();
        this.rgAmortizable.DataSource = oGLDataRecurringItemScheduleInfoCollection;
    }

    protected void rgAmortizable_DeleteCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
    {

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
                this._amortizedAmountClosedItem = 0.00M;
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
                lblAmortizedAmountRCCYTotalValue.Text = Helper.GetDisplayDecimalValue(this._amortizedAmountClosedItem);

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

                //Original Amount RCCY
                ExLabel lblOriginalAmountRCCY = (ExLabel)e.Item.FindControl("lblOriginalAmountRCCY");
                lblOriginalAmountRCCY.Text = Helper.GetDisplayDecimalValue(oGLDataRecurringItemScheduleInfo.ScheduleAmountReportingCurrency);
                if (oGLDataRecurringItemScheduleInfo.ScheduleAmountReportingCurrency.HasValue)
                    this._originalAmountRCCYClosedItem = this._originalAmountRCCYClosedItem + oGLDataRecurringItemScheduleInfo.ScheduleAmountReportingCurrency;

                //Total Amortized Amount
                ExLabel lblAmortizedAmountRCCY = (ExLabel)e.Item.FindControl("lblAmortizedAmountRCCY");
                lblAmortizedAmountRCCY.Text = Helper.GetDisplayDecimalValue(oGLDataRecurringItemScheduleInfo.RecPeriodAmountReportingCurrency);
                if (oGLDataRecurringItemScheduleInfo.RecPeriodAmountReportingCurrency.HasValue)
                    this._amortizedAmountClosedItem = this._amortizedAmountClosedItem + oGLDataRecurringItemScheduleInfo.RecPeriodAmountReportingCurrency;

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
                            //string url = "DownloadAttachment.aspx?" + QueryStringConstants.FILE_PATH + "=" + Server.UrlEncode(SharedHelper.GetDisplayFilePath(oDataImportHdrInfo.PhysicalPath));
                            //imgViewFile.OnClientClick = "document.location.href = '" + url + "';return false;";
                            string url = string.Format("Downloader?{0}={1}&", QueryStringConstants.HANDLER_ACTION, (Int32)WebEnums.HandlerActionType.DownloadDataImportFile);
                            url += "&" + QueryStringConstants.DATA_IMPORT_ID + "=" + oDataImportHdrInfo.DataImportID.GetValueOrDefault()
                            + "&" + QueryStringConstants.DATA_IMPORT_TYPE_ID + "=" + oDataImportHdrInfo.DataImportTypeID.GetValueOrDefault()
                            + "&" + QueryStringConstants.GLDATA_ID + "=" + this.GLDataID.GetValueOrDefault();

                            //imgFileType.OnClientClick = "document.location.href = '" + url + "';return false;";
                            imgViewFile.Attributes.Add("onclick", "javascript:{$get('" + ifDownloader.ClientID + "').src='" + url + "'; return false;}");
                        }
                    }
                }


                ExHyperLink hlShowItemInputForm = (ExHyperLink)e.Item.FindControl("hlShowItemInputForm");
                string javascriptParameterList = PopupHelper.GetJavascriptParameterListForEditRecItem(oGLDataRecurringItemScheduleInfo.GLDataRecurringItemScheduleID, "OpenRadWindowForHyperlinkWithName", "EditItemAccrubleRecurring.aspx", QueryStringConstants.READ_ONLY, true, this.AccountID, this.GLDataID, this.RecCategoryTypeID, this.NetAccountID, this.IsSRA, RecCategoryID, hdIsRefreshData.ClientID, this.CurrentBCCY);
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
    #endregion

    #region "Private Methods"

    private void OnPageLoad()
    {
        this._GLDataRecurringItemScheduleInfoCollection = null;
        this._GLReconciliationItemInputRecordTypeID = (short)WebEnums.RecordType.GLReconciliationItemInput;

        CalculateAndDisplaySum();
        EnableDisableControlsForNonPreparersAndClosedPeriods();
        EnableDisableBulkCloseButton();
    }

    private void SetControlState()
    {
        if (IsPostBack && IsRefreshData && IsDataLoadCondition)
            LoadData();
        ExpandCollapse();


    }

    private void CalculateAndDisplaySum()
    {
        //decimal? baseCurrencyTotal = (from recItem in this.GetGLRecItemInfoCollection
        //                              where recItem.CloseDate == null
        //                              select recItem.RecPeriodAmountBaseCurrency).Sum();

        this._ReportingCurrencyTotal = (from recItem in this.GetGLRecItemInfoCollection
                                        where recItem.CloseDate == null
                                        select recItem.BalanceReportingCurrency).Sum();//Amortizable spicific


        //lblBaseCurrency.Text = Helper.GetLabelIDValue(1382) + " " + Helper.GetDisplayRecCategoryTypeID(this._RecCategoryType.Value) + " " + Helper.GetLabelIDValue(1769) + ":" + " " + SessionHelper.BaseCurrencyCode + " " + Helper.GetDisplayDecimalValue(baseCurrencyTotal);
        //lblReportingCurrency.Text = Helper.GetLabelIDValue(1382) + " " + Helper.GetDisplayRecCategoryTypeID(this._RecCategoryType.Value) + " " + Helper.GetLabelIDValue(1770) + ":" + " " + SessionHelper.ReportingCurrencyCode + " " + Helper.GetDisplayDecimalValue(reportingCurrencyTotal);
    }

    private void EnableDisableControlsForNonPreparersAndClosedPeriods()
    {
        this._UserRole = SessionHelper.CurrentRoleID.Value;
        this._RecPeriodStatus = (short)CurrentRecProcessStatus.Value;

        if (Helper.GetFormMode(WebEnums.ARTPages.GLAdjustments, this.GLDataHdrInfo) == WebEnums.FormMode.Edit
            && this.GLDataID.Value > 0)
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

    private void RepopulateGrids()
    {


        if (this.GLDataID == null || this.GLDataID == 0)
        {
            this.GetGLRecItemInfoCollection = null;
            this.rgAmortizable.DataSource = new List<GLDataRecurringItemScheduleInfo>();
            rgAmortizable.DataBind();
            this.rgGLAdjustmentCloseditems.DataSource = new List<GLDataRecurringItemScheduleInfo>();
            this.rgGLAdjustmentCloseditems.DataBind();
            btnDelete.Visible = false;
        }
        else
        {

            rgAmortizable.EntityNameText = PopupHelper.GetPageTitle(this.RecCategoryID.Value, this.RecCategoryTypeID.Value);

            List<GLDataRecurringItemScheduleInfo> oGLDataRecurringItemScheduleInfoCollection = this.GetGLRecItemInfoCollection.Where(recItem => recItem.CloseDate == null).ToList();
            rgAmortizable.DataSource = oGLDataRecurringItemScheduleInfoCollection;
            rgAmortizable.DataBind();

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

    private void SetGridHeaders(Telerik.Web.UI.GridItemEventArgs e)
    {
        Control oControlRecPeriodAmountReportingCurrency = new Control();
        Control oControlBalanceReportingCurrency = new Control();
        Control oControlOriginalAmountRCCY = new Control();
        Control oControlOriginalAmountLCCY = new Control();

        GridHeaderItem headerItem = e.Item as GridHeaderItem;

        oControlRecPeriodAmountReportingCurrency = (headerItem)["AmortizedAmountRCCY"].Controls[0];
        oControlBalanceReportingCurrency = (headerItem)["BalanceReportingCurrency"].Controls[0];
        oControlOriginalAmountRCCY = (headerItem)["OriginalAmountRCCY"].Controls[0];
        oControlOriginalAmountLCCY = (headerItem)["OriginalAmount"].Controls[0];

        if (oControlRecPeriodAmountReportingCurrency is LinkButton)
        {
            ((LinkButton)oControlRecPeriodAmountReportingCurrency).Text = Helper.GetLabelIDValue(2054) + " (" + SessionHelper.ReportingCurrencyCode + ")";


        }
        else
        {
            if (oControlRecPeriodAmountReportingCurrency is LiteralControl)
            {
                ((LiteralControl)oControlRecPeriodAmountReportingCurrency).Text = Helper.GetLabelIDValue(2054) + " (" + SessionHelper.ReportingCurrencyCode + ")";

            }
        }

        if (oControlBalanceReportingCurrency is LinkButton)
        {
            ((LinkButton)oControlBalanceReportingCurrency).Text = Helper.GetLabelIDValue(2055) + " (" + SessionHelper.ReportingCurrencyCode + ")";

        }
        else
        {
            if (oControlBalanceReportingCurrency is LiteralControl)
            {
                ((LiteralControl)oControlBalanceReportingCurrency).Text = Helper.GetLabelIDValue(2055) + " (" + SessionHelper.ReportingCurrencyCode + ")";

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
    #endregion

    #region "Public Methods"
    public override void LoadData()
    {
        if (IsRefreshData && IsExpanded)
        {
            try
            {
                //Helper.HideMessage(this);
                //updpnlMain.Visible = true;
                OnPageLoad();
                if (!this.IsPostBack)
                {
                    //ViewState[ViewStateConstants.PATH_AND_QUERY] = Request.UrlReferrer.PathAndQuery;
                }
                else
                {
                    btnAdd.OnClientClick = PopupHelper.GetJavascriptParameterListForEditRecItem(null, "OpenRadWindowWithName", "EditItemAmortizable.aspx", QueryStringConstants.INSERT, false, this.AccountID, this.GLDataID, this.RecCategoryTypeID, this.NetAccountID, this.IsSRA, RecCategoryID, hdIsRefreshData.ClientID, this.CurrentBCCY);
                    btnClose.OnClientClick = PopupHelper.GetJavascriptParameterList(null, "OpenRadWindow", "BulkCloseAmortizable.aspx", QueryStringConstants.INSERT, false, this.AccountID, this.GLDataID, this.RecCategoryTypeID, this.NetAccountID, this.IsSRA, RecCategoryID, hdIsRefreshData.ClientID);
                    CalculateAndDisplaySum();
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
            RepopulateGrids();
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

    public void RegisterToggleControl(ExImageButton imgToggleControl)
    {
        imgToggleControl.OnClientClick += "return ToggleDiv('" + imgToggleControl.ClientID + "','" + this.DivClientId + "','" + hdIsExpanded.ClientID + "','" + hdIsRefreshData.ClientID + "');";
        ToggleControl = imgToggleControl;
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

            /////Helper.HidePanel(updpnlMain, ex);
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
            this.divMainContent.Visible = IsExpandedValueForced.Value;
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

