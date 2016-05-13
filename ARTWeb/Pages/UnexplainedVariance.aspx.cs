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
using SkyStem.ART.Client.Exception;
using SkyStem.Library.Controls.TelerikWebControls.Data;

public partial class Pages_UnexplainedVariance : PageBaseRecPeriod
{


    #region PrivateProperties
    bool isExportPDF;
    bool isExportExcel;
    protected short? _RecCategoryType = 0;
    public short _GLReconciliationItemInputRecordTypeID = 0;
    private List<GLDataUnexplainedVarianceInfo> _GLDataUnexplainedVarianceInfoCollection = null;
    private short _UserRole = 0;
    private short _RecPeriodStatus = 0;
    private WebEnums.ReconciliationStatus RecStatus = WebEnums.ReconciliationStatus.NotStarted;

    private GLDataHdrInfo _GLDataHdrInfo;
    private GLDataHdrInfo GLDataHdrInfo
    {
        get
        {
            if (this._GLDataHdrInfo == null)
            {
                if (ViewState[ViewStateConstants.CURRENT_GLDATAHDRINFO] == null)
                {
                    long? _gLDataID = Convert.ToInt64(Request.QueryString[QueryStringConstants.GLDATA_ID]);
                    this._GLDataHdrInfo = Helper.GetGLDataHdrInfo(_gLDataID);
                    ViewState[ViewStateConstants.CURRENT_GLDATAHDRINFO] = this._GLDataHdrInfo;
                }
                else
                {
                    this._GLDataHdrInfo = (GLDataHdrInfo)ViewState[ViewStateConstants.CURRENT_GLDATAHDRINFO];
                }
            }

            return this._GLDataHdrInfo;
        }
    }
    private long? AccountID
    {
        get
        {
            if (this.GLDataHdrInfo != null)
                return this.GLDataHdrInfo.AccountID;
            return null;
        }
    }
    private int? NetAccountID
    {
        get
        {
            if (this.GLDataHdrInfo != null)
                return this.GLDataHdrInfo.NetAccountID;
            return null;
        }
    }
    private bool? IsSRA
    {
        get
        {
            if (this.GLDataHdrInfo != null)
                return this.GLDataHdrInfo.IsSystemReconcilied;
            return null;
        }
    }
    private long? GLDataID
    {
        get
        {
            if (this.GLDataHdrInfo != null)
                return this.GLDataHdrInfo.GLDataID;
            return null;
        }
    }
    private short? RecStatusID
    {
        get
        {
            if (this.GLDataHdrInfo != null)
                return this.GLDataHdrInfo.ReconciliationStatusID;
            return null;
        }
    }
    //BCCY Changes
    private string CurrentBCCY
    {
        get
        {
            if (this.GLDataHdrInfo != null)
                return this.GLDataHdrInfo.BaseCurrencyCode;//Helper.GetCurrentAccountBCCY(this._GLDataID);
            return string.Empty;
        }
    }
    #endregion

    #region "Private Properties"
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
                    this._GLDataUnexplainedVarianceInfoCollection = oUnExpectedVarianceClient.GetGLDataUnexplainedVarianceInfoCollectionByGLDataID(this.GLDataID, Helper.GetAppUserInfo());
                }
                catch (Exception ex)
                {
                    Helper.ShowErrorMessage(this, ex);
                }

            }

            return this._GLDataUnexplainedVarianceInfoCollection;
        }
        set
        {
            this._GLDataUnexplainedVarianceInfoCollection = value;
        }
    }

    //private int GLRecInputItemID;
    //private GLDataUnexplainedVarianceInfo oGLRecItemInputInfo;
    #endregion

    #region "Page Events"

    protected void Page_Init(object sender, EventArgs e)
    {
        MasterPageBase ompage = (MasterPageBase)this.Master.Master;
        ompage.ReconciliationPeriodChangedEventHandler += new EventHandler(ompage_ReconciliationPeriodChangedEventHandler);
        if (!this.IsPostBack)
        {
            this.btnCancel.PostBackUrl = Request.UrlReferrer.PathAndQuery;
        }
        Helper.SetGridImageButtonProperties(this.rgUnExpectedVariance.MasterTableView.Columns);
        this.rgUnExpectedVariance.EntityNameLabelID = 1678;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        Helper.RegisterPostBackToControls(this, rgUnExpectedVariance);


        if (!Page.IsPostBack)
        {
            isExportPDF = false;
            isExportExcel = false;
        }
        try
        {
            Helper.HideMessage(this);
            this._GLDataUnexplainedVarianceInfoCollection = null;
            Helper.SetPageTitle(this, 1678);
            updpnlMain.Visible = true;
            SetPrivateVariables();

            SetAttributesForAddButton();


            EnableDisableControlsForNonPreparersAndClosedPeriods();

        }
        catch (ARTException ex)
        {
            Helper.HidePanel(updpnlMain, ex);
            Helper.ShowErrorMessage(this, ex);
        }
        catch (Exception ex)
        {
            Helper.ShowErrorMessage(this, ex);
        }
    }

    private void SetAttributesForAddButton()
    {
        string queryString = "?" + QueryStringConstants.ACCOUNT_ID + "=" + AccountID.Value
                                 + "&" + QueryStringConstants.GLDATA_ID + "=" + this.GLDataID.Value
                                 + "&" + QueryStringConstants.MODE + "=" + QueryStringConstants.INSERT
                                 + "&" + QueryStringConstants.REC_CATEGORY_TYPE_ID + "=" + this._RecCategoryType.Value;
        string popUPURL = "EditItemUnexplainedVariance.aspx" + queryString;

        btnAdd.Attributes.Add("onclick", "OpenRadWindowForHyperlink('" + popUPURL + "', " + WebConstants.POPUP_HEIGHT + " , " + WebConstants.POPUP_WIDTH + ");return false;");
    }

    private void SetPrivateVariables()
    {

        if (Request.QueryString[QueryStringConstants.REC_CATEGORY_TYPE_ID] != null)
            this._RecCategoryType = Convert.ToInt16(Request.QueryString[QueryStringConstants.REC_CATEGORY_TYPE_ID]);

        this._GLReconciliationItemInputRecordTypeID = (short)WebEnums.RecordType.GLReconciliationItemInput;
        Helper.ValidateRecTemplateForGLDataID(this, this.GLDataHdrInfo, Helper.GetRecTemplateForCurrentPage(_RecCategoryType), null);

        if (this.RecStatusID.HasValue && this.RecStatusID.Value > 0)
        {
            this.RecStatus = (WebEnums.ReconciliationStatus)Enum.Parse(typeof(WebEnums.ReconciliationStatus), this.RecStatusID.Value.ToString());
        }
        // Set the Master Page Properties for GL Data ID
        RecHelper.SetRecStatusBarPropertiesForOtherPages(this, this.GLDataID);
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
        int lastPagePhraseID = Helper.GetPageTitlePhraseID(this.GetPreviousPageName());
        if (lastPagePhraseID != -1)
            Helper.SetBreadcrumbs(this, 1071, 1187, lastPagePhraseID, 1678);
        else
            Helper.SetBreadcrumbs(this, 1071, 1187, 1678);
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

                ExLabel lblAmountBaseCurrency = (ExLabel)e.Item.FindControl("lblAmountBaseCurrency");
                ExLabel lblAddedBy = (ExLabel)e.Item.FindControl("lblAddedBy");
                ExLabel lblDateAdded = (ExLabel)e.Item.FindControl("lblDateAdded");

                //BCCY Changes
                //lblAmountBaseCurrency.Text = Helper.GetCurrencyValue(oGLDataUnexplainedVarianceInfo.AmountBaseCurrency, SessionHelper.BaseCurrencyCode);
                lblAmountBaseCurrency.Text = Helper.GetCurrencyValue(oGLDataUnexplainedVarianceInfo.AmountBaseCurrency, this.CurrentBCCY);

                lblAddedBy.Text = Helper.GetDisplayStringValue(oGLDataUnexplainedVarianceInfo.AddedByUserInfo.Name);
                lblDateAdded.Text = Helper.GetDisplayDate(oGLDataUnexplainedVarianceInfo.DateAdded);
                ExHyperLink imgbtnShowItemInputForm = (ExHyperLink)e.Item.FindControl("hlShowItemInputForm");

                //Helper.GetFormMode()
                if (Helper.GetFormMode(WebEnums.ARTPages.UnexplainedVariance, this.GLDataHdrInfo ) == WebEnums.FormMode.Edit)
                {
                    //if (oGLReconciliationItemInputInfo.IsForwardedItem.HasValue && oGLReconciliationItemInputInfo.IsForwardedItem.Value)
                    //{
                    //    imgbtnShowItemInputForm.Attributes.Add(WebConstants.ONCLICK, "ShowRecItemInput('" + QueryStringConstants.ACCOUNT_ID + "=" + this._AccountID.Value + "&" + QueryStringConstants.GLDATA_ID + "=" + this._GLDataID.Value + "&" + QueryStringConstants.REC_CATEGORY_TYPE_ID + "=" + this._RecCategoryType.Value + "&" + QueryStringConstants.MODE + "=" + QueryStringConstants.EDIT + "&" + QueryStringConstants.GL_RECONCILIATION_ITEM_INPUT_ID + "=" + oGLReconciliationItemInputInfo.GLDataRecItemID.Value + "&" + QueryStringConstants.IS_FORWARDED_ITEM + "=1');");
                    //}
                    //else
                    //{
                    //    imgbtnShowItemInputForm.Attributes.Add(WebConstants.ONCLICK, "ShowRecItemInput('" + QueryStringConstants.ACCOUNT_ID + "=" + this._AccountID.Value + "&" + QueryStringConstants.GLDATA_ID + "=" + this._GLDataID.Value + "&" + QueryStringConstants.REC_CATEGORY_TYPE_ID + "=" + this._RecCategoryType.Value + "&" + QueryStringConstants.MODE + "=" + QueryStringConstants.EDIT + "&" + QueryStringConstants.GL_RECONCILIATION_ITEM_INPUT_ID + "=" + oGLReconciliationItemInputInfo.GLDataRecItemID.Value + "&" + QueryStringConstants.IS_FORWARDED_ITEM + "=0');");

                    //}

                    string queryString = "?" + QueryStringConstants.ACCOUNT_ID + "=" + this.AccountID.Value 
           + "&" + QueryStringConstants.GL_RECONCILIATION_ITEM_INPUT_ID + "=" + oGLDataUnexplainedVarianceInfo.GLDataUnexplainedVarianceID.Value
           + "&" + QueryStringConstants.GLDATA_ID + "=" + this.GLDataID.Value
           + "&" + QueryStringConstants.MODE + "=" + QueryStringConstants.EDIT
           + "&" + QueryStringConstants.REC_CATEGORY_TYPE_ID + "=" + this._RecCategoryType.Value;
                    string popUPURL = "EditItemUnexplainedVariance.aspx?" + queryString;
                    imgbtnShowItemInputForm.NavigateUrl = "javascript:OpenRadWindowForHyperlink('" + popUPURL + "', " + WebConstants.POPUP_HEIGHT + " , " + WebConstants.POPUP_WIDTH + ");";
                    //imgbtnShowItemInputForm.Attributes.Add(WebConstants.ONCLICK, "ShowRecItemInput('" + QueryStringConstants.ACCOUNT_ID + "=" + this._AccountID.Value + "&" + QueryStringConstants.GLDATA_ID + "=" + this._GLDataID.Value + "&" + QueryStringConstants.REC_CATEGORY_TYPE_ID + "=" + this._RecCategoryType.Value + "&" + QueryStringConstants.MODE + "=" + QueryStringConstants.EDIT + "&" + QueryStringConstants.GL_RECONCILIATION_ITEM_INPUT_ID + "=" + oGLDataUnexplainedVarianceInfo.GLDataUnexplainedVarianceID.Value + "');");

                    Helper.SetImageURLForViewVersusEdit(WebEnums.FormMode.Edit, imgbtnShowItemInputForm);

                }
                else
                {
                    string queryString = "?" + QueryStringConstants.ACCOUNT_ID + "=" + this.AccountID.Value
           + "&" + QueryStringConstants.GL_RECONCILIATION_ITEM_INPUT_ID + "=" + oGLDataUnexplainedVarianceInfo.GLDataUnexplainedVarianceID.Value
           + "&" + QueryStringConstants.GLDATA_ID + "=" + this.GLDataID.Value
           + "&" + QueryStringConstants.MODE + "=" + QueryStringConstants.READ_ONLY
           + "&" + QueryStringConstants.REC_CATEGORY_TYPE_ID + "=" + this._RecCategoryType.Value;
                    string popUPURL = "EditItemUnexplainedVariance.aspx" + queryString;
                    imgbtnShowItemInputForm.NavigateUrl = "javascript:OpenRadWindowForHyperlink('" + popUPURL + "', " + WebConstants.POPUP_HEIGHT + " , " + WebConstants.POPUP_WIDTH + ");";
                    //imgbtnShowItemInputForm.Attributes.Add(WebConstants.ONCLICK, "ShowRecItemInput('" + QueryStringConstants.ACCOUNT_ID + "=" + this._AccountID.Value + "&" + QueryStringConstants.GLDATA_ID + "=" + this._GLDataID.Value + "&" + QueryStringConstants.REC_CATEGORY_TYPE_ID + "=" + this._RecCategoryType.Value + "&" + QueryStringConstants.MODE + "=" + QueryStringConstants.READ_ONLY + "&" + QueryStringConstants.GL_RECONCILIATION_ITEM_INPUT_ID + "=" + oGLDataUnexplainedVarianceInfo.GLDataUnexplainedVarianceID.Value + "');");

                    Helper.SetImageURLForViewVersusEdit(WebEnums.FormMode.ReadOnly, imgbtnShowItemInputForm);
                }
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
        catch (Exception ex)
        {
            Helper.ShowErrorMessage(this, ex);
        }
    }

    protected void rgUnExpectedVariance_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
    {
        this.rgUnExpectedVariance.DataSource = this.GetGLDataUnexplainedVarianceInfoCollection;
    }

    //protected void rgGLAdjustmentCloseditems_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
    //{
    //    try
    //    {
    //        if (e.Item.ItemType == Telerik.Web.UI.GridItemType.Item || e.Item.ItemType == Telerik.Web.UI.GridItemType.AlternatingItem)
    //        {
    //            GLDataUnexplainedVarianceInfo oGLReconciliationItemInputInfo = (GLDataUnexplainedVarianceInfo)e.Item.DataItem;
    //            //base currency
    //            ExLabel lblAmountBaseCurrency = (ExLabel)e.Item.FindControl("lblAmountBaseCurrency");
    //            lblAmountBaseCurrency.Text = Helper.GetCurrencyValue(oGLReconciliationItemInputInfo.AmountBaseCurrency, SessionHelper.BaseCurrencyCode);

    //            //ExLabel lblOpenDate = (ExLabel)e.Item.FindControl("lblOpenDate");
    //            //lblOpenDate.Text = Helper.GetDisplayDate(oGLReconciliationItemInputInfo.OpenDate);

    //            //ExLabel lblCloseDate = (ExLabel)e.Item.FindControl("lblCloseDate");
    //            //lblCloseDate.Text = Helper.GetDisplayDate(oGLReconciliationItemInputInfo.CloseDate);

    //            //ExLabel lblAttachmentCount = (ExLabel)e.Item.FindControl("lblAttachmentCount");
    //            //lblAttachmentCount.Text = Helper.GetDisplayIntegerValue(oGLReconciliationItemInputInfo.AttachmentCount);

    //            ExImageButton imgbtnShowItemInputForm = (ExImageButton)e.Item.FindControl("imgbtnShowItemInputForm");

    //            imgbtnShowItemInputForm.Attributes.Add(WebConstants.ONCLICK
    //                , "ShowRecItemInput('" 
    //                + QueryStringConstants.ACCOUNT_ID + "=" 
    //                + this._AccountID.Value + "&" 
    //                + QueryStringConstants.GLDATA_ID + "=" 
    //                + this._GLDataID.Value + "&" 
    //                + QueryStringConstants.REC_CATEGORY_TYPE_ID + "=" 
    //                + this._RecCategoryType.Value + "&" 
    //                + QueryStringConstants.MODE + "=" 
    //                + QueryStringConstants.READ_ONLY + "&" 
    //                + QueryStringConstants.GL_RECONCILIATION_ITEM_INPUT_ID + "=" 
    //                + oGLReconciliationItemInputInfo.GLDataUnexplainedVarianceID.Value + "&" 
    //                + QueryStringConstants.IS_FORWARDED_ITEM + "=1');");
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        Helper.ShowErrorMessage(this, ex);
    //    }
    //}

    //protected void rgGLAdjustmentCloseditems_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
    //{
    //    List<GLDataUnexplainedVarianceInfo> oGLReconciliationItemInputInfoCollection = this.GetGLDataUnexplainedVarianceInfoCollection.Where(recItem => recItem.CloseDate != null).ToList();

    //    if (oGLReconciliationItemInputInfoCollection == null)
    //        oGLReconciliationItemInputInfoCollection = new List<GLDataUnexplainedVarianceInfo>();

    //    if (oGLReconciliationItemInputInfoCollection == null || oGLReconciliationItemInputInfoCollection.Count == 0)
    //    {
    //        pnlClosedItems.Visible = false;
    //    }
    //    else
    //    {
    //        pnlClosedItems.Visible = true;
    //    }

    //    this.rgGLAdjustmentCloseditems.DataSource = oGLReconciliationItemInputInfoCollection;
    //}


    protected void rgUnExpectedVariance_ItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
    {
        try
        {

            if (e.CommandName == TelerikConstants.GridExportToPDFCommandName)
            {
                isExportPDF = true;
                //rgCompanyList.MasterTableView.Columns.FindByUniqueName("IconColumn").Visible = false;
                GridHelper.ExportGridToPDF(rgUnExpectedVariance, 1678);

            }
            if (e.CommandName == TelerikConstants.GridExportToExcelCommandName)
            {
                isExportExcel = true;
                //rgCompanyList.MasterTableView.Columns.FindByUniqueName("IconColumn").Visible = false;
                GridHelper.ExportGridToExcel(rgUnExpectedVariance, 1678);

            }

            if (e.CommandName == "Delete")
            {
                GLDataUnexplainedVarianceInfo oGLReconciliationItemInputInfo = new GLDataUnexplainedVarianceInfo();
                oGLReconciliationItemInputInfo.GLDataUnexplainedVarianceID = Convert.ToInt64(e.CommandArgument);
                //oGLReconciliationItemInputInfo.GLDataID = this._GLDataID;
                //oGLReconciliationItemInputInfo.ReconciliationCategoryTypeID = this._RecCategoryType;
                IUnexplainedVariance oUnExpectedVarianceClient = RemotingHelper.GetUnexplainedVarianceObject();
                //oUnExpectedVarianceClient.DeleteRecInputItem(oGLReconciliationItemInputInfo, (short)ARTEnums.AccountAttribute.ReconciliationTemplate);
                oUnExpectedVarianceClient.DeleteGLDataUnexplainedVariance(oGLReconciliationItemInputInfo.GLDataUnexplainedVarianceID, Helper.GetAppUserInfo());

                this._GLDataUnexplainedVarianceInfoCollection = null;

                //List<GLDataUnexplainedVarianceInfo> oGLReconciliationItemInputInfoCollection = this.GetGLDataUnexplainedVarianceInfoCollection.Where(recItem => string.IsNullOrEmpty(recItem.CloseComments) && recItem.CloseDate == null && recItem.JournalEntryRef == null).ToList();
                //rgUnExpectedVariance.DataSource = oGLReconciliationItemInputInfoCollection;
                //rgUnExpectedVariance.DataBind();

                //CalculateAndDisplaySum();
            }
        }
        catch (Exception ex)
        {
            Helper.ShowErrorMessage(this, ex);
        }
    }

    #endregion

    protected void ompage_ReconciliationPeriodChangedEventHandler(object sender, EventArgs e)
    {
        try
        {
            if (!this.GLDataID.HasValue || this.GLDataID.Value == 0)
                Helper.HideMessage(this);
            updpnlMain.Visible = true;
            SetPrivateVariables();
            SetAttributesForAddButton();
            IUnexplainedVariance oUnexplainedVarianceClient = RemotingHelper.GetUnexplainedVarianceObject();
            this.GetGLDataUnexplainedVarianceInfoCollection = oUnexplainedVarianceClient.GetGLDataUnexplainedVarianceInfoCollectionByGLDataID(GLDataID, Helper.GetAppUserInfo());
            RepopulateGrids();
            //EnableDisableBulkCloseButton();
            //CalculateAndDisplaySum();
            EnableDisableControlsForNonPreparersAndClosedPeriods();
        }
        catch (ARTException ex)
        {
            Helper.HidePanel(updpnlMain, ex);
            Helper.ShowErrorMessage(this, ex);
        }
        catch (Exception ex)
        {
            Helper.ShowErrorMessage(this, ex);
        }
    }

    public override string GetMenuKey()
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


    private void RepopulateGrids()
    {
        rgUnExpectedVariance.DataSource = this.GetGLDataUnexplainedVarianceInfoCollection;
        rgUnExpectedVariance.DataBind();
    }
    protected void rgUnExpectedVariance_ItemCreated(object sender, GridItemEventArgs e)
    {
        GridHelper.SetStylesForExportGrid(e, isExportPDF, isExportExcel);
    }


}


