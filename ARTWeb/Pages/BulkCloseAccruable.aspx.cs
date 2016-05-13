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
using SkyStem.ART.Web.Classes;
using SkyStem.ART.Web.Data;
using SkyStem.ART.Client.Model;
using SkyStem.Library.Controls.WebControls;
using Telerik.Web.UI;
using SkyStem.ART.Client.Data;
using SkyStem.ART.Client.IServices;
using System.Collections.Generic;
using SkyStem.Language.LanguageUtility;
using SkyStem.Library.Controls.TelerikWebControls.Data;
using SkyStem.Library.Controls.TelerikWebControls;

public partial class Pages_BulkCloseAccruable : PopupPageBase
{

    #region Variables & Constants
    protected long? _GLDataID = 0;
    protected short? _RecCategory = 0;
    protected short? _RecCategoryType = 0;
    short _GLReconciliationItemInputRecordTypeID = 0;
    protected long? _AccountID = 0;
    
    bool isExportPDF;
    bool isExportExcel;
    string _ParentHiddenField = null;
    #endregion

    #region Properties
    private List<GLDataRecItemInfo> _GLRecItemInfoCollection = null;

    #endregion

    #region Delegates & Events
    #endregion

    #region Page Events
    protected void Page_Init(object sender, EventArgs e)
    {
        try
        {
            Helper.SetGridImageButtonProperties(this.rgGLAdjustments.MasterTableView.Columns);
            this.rgGLAdjustments.EntityNameLabelID = 1455;
        }
        catch (Exception ex)
        {
            PopupHelper.ShowErrorMessage(this, ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {

        try
        {
            if (Request.QueryString[QueryStringConstants.REC_CATEGORY_ID] != null)
                _RecCategory = Convert.ToInt16(Request.QueryString[QueryStringConstants.REC_CATEGORY_ID].ToString());
            if (Request.QueryString[QueryStringConstants.REC_CATEGORY_TYPE_ID] != null)
                this._RecCategoryType = Convert.ToInt16(Request.QueryString[QueryStringConstants.REC_CATEGORY_TYPE_ID]);
            int[] oLableIdCollection = new int[0];


            PopupHelper.SetPageTitle(this, (short)_RecCategory, (short)_RecCategoryType, 1771);
            GetQueryStringValues();
            SetErrorMessage();

            ucAccountHierarchyDetailPopup.AccountID = this._AccountID.Value;

            if (!IsPostBack)
            {
                isExportPDF = false;
                isExportExcel = false;
                PopulateItemsOnPage();
            }
        }
        catch (Exception ex)
        {
            PopupHelper.ShowErrorMessage(this, ex);
        }
    }

    #endregion

    #region Grid Events
    protected void rgGLAdjustments_ItemCreated(object sender, GridItemEventArgs e)
    {
        // Register PDF / Excel for Postback
        //GridHelper.RegisterPDFAndExcelForPostbackForPoup (e, isExportPDF, isExportExcel, this.Page);

        GridHelper.SetStylesForExportGrid(e, isExportPDF, isExportExcel);
    }
    protected void rgGLAdjustments_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {
        try
        {
            if (e.Item.ItemType == GridItemType.Header)
            {
                //((LinkButton)(e.Item as GridHeaderItem)["AmountInReportingCurrency"].Controls[0]).Text = Helper.GetLabelIDValue(1674) + " (" + SessionHelper.ReportingCurrencyCode + ")";
                //***************Above code commented and replaced by below code to handle the export to pdf of Grid
                Control oControlAmountInReportingCurrency = new Control();
                oControlAmountInReportingCurrency = (e.Item as GridHeaderItem)["AmountInReportingCurrency"].Controls[0];
                if (oControlAmountInReportingCurrency is LinkButton)
                {
                    ((LinkButton)oControlAmountInReportingCurrency).Text = Helper.GetLabelIDValue(1674) + " (" + SessionHelper.ReportingCurrencyCode + ")";

                }
                else
                {
                    if (oControlAmountInReportingCurrency is LiteralControl)
                    {
                        ((LiteralControl)oControlAmountInReportingCurrency).Text = Helper.GetLabelIDValue(1674) + " (" + SessionHelper.ReportingCurrencyCode + ")";

                    }
                }
                //******************************************************************************************************************************************

            }

            if (e.Item.ItemType == Telerik.Web.UI.GridItemType.Item || e.Item.ItemType == Telerik.Web.UI.GridItemType.AlternatingItem)
            {
                GLDataRecItemInfo oGLReconciliationItemInputInfo = (GLDataRecItemInfo)e.Item.DataItem;

                ExLabel lblAmount = (ExLabel)e.Item.FindControl("lblAmount");
                lblAmount.Text = Helper.GetCurrencyValue(oGLReconciliationItemInputInfo.Amount, oGLReconciliationItemInputInfo.LocalCurrencyCode);

                ExLabel lblOpenDate = (ExLabel)e.Item.FindControl("lblOpenDate");
                lblOpenDate.Text = Helper.GetDisplayDate(oGLReconciliationItemInputInfo.OpenDate);

                ExLabel lblAttachmentCount = (ExLabel)e.Item.FindControl("lblAttachmentCount");
                lblAttachmentCount.Text = Helper.GetDisplayIntegerValue(oGLReconciliationItemInputInfo.AttachmentCount);

                ExLabel lblAging = (ExLabel)e.Item.FindControl("lblAging");
                lblAging.Text = Helper.GetDisplayIntegerValue(Helper.GetDaysBetweenDateRanges(oGLReconciliationItemInputInfo.OpenDate, DateTime.Now));

                ExLabel lblRecItemNumber = (ExLabel)e.Item.FindControl("lblRecItemNumber");
                lblRecItemNumber.Text = Helper.GetDisplayStringValue(oGLReconciliationItemInputInfo.RecItemNumber);
            }
        }
        catch (Exception ex)
        {
            PopupHelper.ShowErrorMessage(this, ex);
        }
    }
    protected void rgGLAdjustments_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
    {
        try
        {
            this._GLReconciliationItemInputRecordTypeID = (short)WebEnums.RecordType.GLReconciliationItemInput;

            IGLDataRecItem oGLRecItemInput = RemotingHelper.GetGLDataRecItemObject();
            this._GLRecItemInfoCollection = oGLRecItemInput.GetRecItem(this._AccountID.Value, SessionHelper.CurrentReconciliationPeriodID.Value, this._RecCategoryType.Value, this._GLReconciliationItemInputRecordTypeID, (short)ARTEnums.AccountAttribute.ReconciliationTemplate, Helper.GetAppUserInfo());

            List<GLDataRecItemInfo> oGLReconciliationItemInputInfoCollection = (from recItem in this._GLRecItemInfoCollection
                                                                                where recItem.IsForwardedItem == true
                                                                                select recItem).ToList();

            rgGLAdjustments.DataSource = oGLReconciliationItemInputInfoCollection;
        }
        catch (Exception ex)
        {
            PopupHelper.ShowErrorMessage(this, ex);
        }
    }
    protected void rgGLAdjustments_OnItemCommand(object sender, GridCommandEventArgs e)
    {
        if (e.CommandName == TelerikConstants.GridExportToPDFCommandName)
        {
            isExportPDF = true;
            rgGLAdjustments.MasterTableView.Columns.FindByUniqueName("CheckboxSelectColumn").Visible = false;
            rgGLAdjustments.MasterTableView.Columns.FindByUniqueName("BlankColumn1").Visible = false;
            rgGLAdjustments.MasterTableView.Columns.FindByUniqueName("BlankColumn2").Visible = false;
            PopulateItemsOnPage();
            GridHelper.ExportGridToPDF(rgGLAdjustments, rgGLAdjustments.EntityNameLabelID);
        }
        if (e.CommandName == TelerikConstants.GridExportToExcelCommandName)
        {
            isExportExcel = true;
            rgGLAdjustments.MasterTableView.Columns.FindByUniqueName("CheckboxSelectColumn").Visible = false;
            rgGLAdjustments.MasterTableView.Columns.FindByUniqueName("BlankColumn1").Visible = false;
            rgGLAdjustments.MasterTableView.Columns.FindByUniqueName("BlankColumn2").Visible = false;
            PopulateItemsOnPage();
            GridHelper.ExportGridToExcel(rgGLAdjustments, rgGLAdjustments.EntityNameLabelID);
        }

    }
    #endregion

    #region Other Events
    protected void btnClose_Click(object sender, EventArgs e)
    {
        try
        {
            List<long> glRecItemInputIdCollection = new List<long>();

            foreach (GridDataItem item in rgGLAdjustments.MasterTableView.GetSelectedItems())
            {
                long glRecItemID = Convert.ToInt64(item["GLRecItemID"].Text);
                glRecItemInputIdCollection.Add(glRecItemID);
            }


            IGLDataRecItem oGLRecItemInputClient = RemotingHelper.GetGLDataRecItemObject();
            oGLRecItemInputClient.UpdateGLRecItemCloseDate(this._GLDataID.Value, glRecItemInputIdCollection
                , Convert.ToDateTime(calResolutionDate.Text), null, null, null, this._RecCategoryType.Value, (short)ARTEnums.AccountAttribute.ReconciliationTemplate
                , SessionHelper.GetCurrentUser().LoginID, DateTime.Now, SessionHelper.CurrentReconciliationPeriodID.Value, Helper.GetAppUserInfo());

            if (_ParentHiddenField != null)
            {
                this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "SetHiddenFieldStatus", ScriptHelper.GetJSToSetParentWindowElementValue(_ParentHiddenField, "1")); // 1 means Reload data of GridVieww
            }
            this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "ClosePopupAndRefreshParentPage", ScriptHelper.GetJSForClosePopupAndSubmitParentPage());
        }
        catch (Exception ex)
        {
            PopupHelper.ShowErrorMessage(this, ex);
        }
    }

    #endregion

    #region Validation Control Events
    #endregion

    #region Private Methods
    private void SetErrorMessage()
    {
        vldResolutionDate.ErrorMessage = LanguageUtil.GetValue(5000095);
        cvResolutionDate.ErrorMessage = Helper.GetErrorMessage(WebEnums.FieldType.DateFormatField, 1411);
        cvCompareWithCurrentDate.ErrorMessage = Helper.GetErrorMessage(WebEnums.FieldType.DateCompareField, 1411, 2062);
        cvCompareWithOpenDate.ErrorMessage = Helper.GetErrorMessage(WebEnums.FieldType.DateCompareField, 1411, 1511);
    }


    private void PopulateItemsOnPage()
    {
        this._GLReconciliationItemInputRecordTypeID = (short)WebEnums.RecordType.GLReconciliationItemInput;

        IGLDataRecItem oGLRecItemInput = RemotingHelper.GetGLDataRecItemObject();
        this._GLRecItemInfoCollection = oGLRecItemInput.GetRecItem(this._GLDataID.Value, SessionHelper.CurrentReconciliationPeriodID.Value, this._RecCategoryType.Value, this._GLReconciliationItemInputRecordTypeID, (short)ARTEnums.AccountAttribute.ReconciliationTemplate, Helper.GetAppUserInfo());

        List<GLDataRecItemInfo> oGLReconciliationItemInputInfoCollection = (from recItem in this._GLRecItemInfoCollection
                                                                            where recItem.CloseDate == null
                                                                            select recItem).ToList();

        rgGLAdjustments.DataSource = oGLReconciliationItemInputInfoCollection;
        rgGLAdjustments.DataBind();
    }

    private void GetQueryStringValues()
    {
        if (Request.QueryString[QueryStringConstants.ACCOUNT_ID] != null)
            this._AccountID = Convert.ToInt32(Request.QueryString[QueryStringConstants.ACCOUNT_ID]);
        if (Request.QueryString[QueryStringConstants.GLDATA_ID] != null)
            this._GLDataID = Convert.ToInt32(Request.QueryString[QueryStringConstants.GLDATA_ID]);
        if (Request.QueryString[QueryStringConstants.REC_CATEGORY_TYPE_ID] != null)
            this._RecCategoryType = Convert.ToInt16(Request.QueryString[QueryStringConstants.REC_CATEGORY_TYPE_ID]);
        if (Request.QueryString[QueryStringConstants.PARENT_HIDDEN_FIELD] != null)
            this._ParentHiddenField = Request.QueryString[QueryStringConstants.PARENT_HIDDEN_FIELD];

    }
    #endregion

    #region Other Methods
    #endregion

}
