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
using SkyStem.ART.Web.Utility;
using SkyStem.ART.Web.Data;
using SkyStem.ART.Client.Model;
using SkyStem.Library.Controls.WebControls;
using System.Collections.Generic;
using Telerik.Web.UI;
using SkyStem.ART.Client.IServices;
using SkyStem.ART.Client.Data;
using SkyStem.Language.LanguageUtility;
using SkyStem.Library.Controls.TelerikWebControls.Data;
using SkyStem.Library.Controls.TelerikWebControls;

public partial class Pages_BulkCloseAmortizable : PopupPageBase
{
    #region Variables & Constants
    protected long? _GLDataID = 0;
    protected short? _RecCategory = 0;
    protected short? _RecCategoryType = 0;
    protected long? _AccountID = 0;
  
    bool isExportPDF;
    bool isExportExcel;
    string _ParentHiddenField = null;

    #endregion

    #region Properties
    private List<GLDataRecurringItemScheduleInfo> _GLDataRecurringItemScheduleInfoCollection = null;

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

            PopupHelper.SetPageTitle(this, (short)_RecCategory, (short)_RecCategoryType, 1771);
            //PopupHelper.SetPageTitle(this, Helper.SetPageTitleWithMultiplePhrases(1084, 1656, 1771));
            //PopupHelper.SetPageTitle(this, 1455);
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
            #region "Header Items"
            if (e.Item.ItemType == GridItemType.Header)
            {
                this.SetGridHeaders(e);
            }
            #endregion

            #region "Row Items"
            if (e.Item.ItemType == Telerik.Web.UI.GridItemType.Item || e.Item.ItemType == Telerik.Web.UI.GridItemType.AlternatingItem)
            {
                GLDataRecurringItemScheduleInfo oGLDataRecurringItemScheduleInfo = (GLDataRecurringItemScheduleInfo)e.Item.DataItem;

                //Original Amount RCCY
                ExLabel lblScheduleAmount = (ExLabel)e.Item.FindControl("lblScheduleAmount");
                lblScheduleAmount.Text = Helper.GetCurrencyValue(oGLDataRecurringItemScheduleInfo.ScheduleAmount, oGLDataRecurringItemScheduleInfo.LocalCurrencyCode);

                //OpenDate
                ExLabel lblOpenDate = (ExLabel)e.Item.FindControl("lblOpenDate");
                if (lblOpenDate != null)
                    lblOpenDate.Text = Helper.GetDisplayDate(oGLDataRecurringItemScheduleInfo.OpenDate).Trim();

                //Schedule Name
                ExLabel lblScheduleName = (ExLabel)e.Item.FindControl("lblScheduleName");
                if (lblScheduleName != null)
                    lblScheduleName.Text = Helper.GetDisplayStringValue(oGLDataRecurringItemScheduleInfo.ScheduleName);

                //Begin Date

                //End Date

                //Original Amount RCCY
                ExLabel lblOriginalAmountRCCY = (ExLabel)e.Item.FindControl("lblOriginalAmountRCCY");
                if (lblOriginalAmountRCCY != null)
                    lblOriginalAmountRCCY.Text = Helper.GetDisplayDecimalValue(oGLDataRecurringItemScheduleInfo.ScheduleAmountReportingCurrency);
                //Total Amortized Amount
                ExLabel lblRecPeriodAmountRCCY = (ExLabel)e.Item.FindControl("lblRecPeriodAmountRCCY");
                if (lblRecPeriodAmountRCCY != null)
                    lblRecPeriodAmountRCCY.Text = Helper.GetDisplayDecimalValue(oGLDataRecurringItemScheduleInfo.RecPeriodAmountReportingCurrency);

                //Total Amortizable Amount
                ExLabel lblBalanceRCCY = (ExLabel)e.Item.FindControl("lblBalanceRCCY");
                if (lblBalanceRCCY != null)
                    lblBalanceRCCY.Text = Helper.GetDisplayDecimalValue(oGLDataRecurringItemScheduleInfo.BalanceReportingCurrency);
                //Docs
                ExLabel lblAttachmentCount = (ExLabel)e.Item.FindControl("lblAttachmentCount");
                if (lblAttachmentCount != null)
                    lblAttachmentCount.Text = Helper.GetDisplayIntegerValue(oGLDataRecurringItemScheduleInfo.AttachmentCount);

                ExLabel lblRecItemNumber = (ExLabel)e.Item.FindControl("lblRecItemNumber");
                lblRecItemNumber.Text = Helper.GetDisplayStringValue(oGLDataRecurringItemScheduleInfo.RecItemNumber);

                //ExLabel lblAmountBaseCurrency = (ExLabel)e.Item.FindControl("lblAmountBaseCurrency");
                //lblAmountBaseCurrency.Text = Helper.GetCurrencyValue(oGLDataRecurringItemScheduleInfo.RecPeriodAmountBaseCurrency, SessionHelper.ReportingCurrencyCode);

                //ExLabel lblAging = (ExLabel)e.Item.FindControl("lblAging");
                //lblAging.Text = oGLDataRecurringItemScheduleInfo.aging;



            }
            #endregion

            #region "Footer Items"
            if (e.Item.ItemType == GridItemType.Footer)
            {

            }
            #endregion
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
            //this._GLReconciliationItemInputRecordTypeID = (short)WebEnums.RecordType.GLReconciliationItemInput;
            rgGLAdjustments.DataSource = GetGLDataRecurringItemScheduleInfoToBeClosed();
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
            PopulateItemsOnPage();
            GridHelper.ExportGridToPDF(rgGLAdjustments, rgGLAdjustments.EntityNameLabelID);
        }
        if (e.CommandName == TelerikConstants.GridExportToExcelCommandName)
        {
            isExportExcel = true;
            rgGLAdjustments.MasterTableView.Columns.FindByUniqueName("CheckboxSelectColumn").Visible = false;
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
            List<long> GLDataRecurringItemScheduleIDCollection = new List<long>();

            foreach (GridDataItem item in rgGLAdjustments.MasterTableView.GetSelectedItems())
            {
                long GLDataRecurringItemScheduleID = Convert.ToInt64(item["GLDataRecurringItemScheduleID"].Text);
                GLDataRecurringItemScheduleIDCollection.Add(GLDataRecurringItemScheduleID);
            }

            IGLDataRecItemSchedule oGLDataRecItemScheduleClient = RemotingHelper.GetGLDataRecItemScheduleObject();
            oGLDataRecItemScheduleClient.UpdateGLDataRecurringItemScheduleCloseDate(this._GLDataID.Value, GLDataRecurringItemScheduleIDCollection
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
        //vldResolutionDate.ErrorMessage = LanguageUtil.GetValue(5000095);
        //cvResolutionDate.ErrorMessage = Helper.GetErrorMessage(WebEnums.FieldType.DateFormatField, 1411);
        //cvCompareWithCurrentDate.ErrorMessage = Helper.GetErrorMessage(WebEnums.FieldType.DateCompareField, 1411, 2062);
        //cvCompareWithOpenDate.ErrorMessage = Helper.GetErrorMessage(WebEnums.FieldType.DateCompareField, 1411, 1511);
        RecHelper.SetErrorMessageForCloseDateValidationControls(this.vldResolutionDate, this.cvResolutionDate, this.cvCompareWithCurrentDate
            , cvCompareWithOpenDate, 1657, this.lblCloseDate.LabelID, 2062);
    }

    private void PopulateItemsOnPage()
    {
        //TODO: check the correct record type
        //this._GLReconciliationItemInputRecordTypeID = (short)WebEnums.RecordType.GLReconciliationItemInput;
        rgGLAdjustments.DataSource = GetGLDataRecurringItemScheduleInfoToBeClosed();
        //todo: may be just call rgGLAdjustments_NeedDataSource();
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
    private List<GLDataRecurringItemScheduleInfo> GetGLDataRecurringItemScheduleInfoToBeClosed()
    {
        IGLDataRecItemSchedule oGLDataRecItemScheduleClient = RemotingHelper.GetGLDataRecItemScheduleObject();
        //this._GLDataRecurringItemScheduleInfoCollection = oGLDataRecItemScheduleClient.GetRecItem(this._AccountID.Value, SessionHelper.CurrentReconciliationPeriodID.Value, this._RecCategoryType.Value, this._GLReconciliationItemInputRecordTypeID, (short)ARTEnums.AccountAttribute.ReconciliationTemplate);
        this._GLDataRecurringItemScheduleInfoCollection = oGLDataRecItemScheduleClient.GetGLDataRecurringItemSchedule(_GLDataID, Helper.GetAppUserInfo());

        List<GLDataRecurringItemScheduleInfo> oGLDataRecurringItemScheduleInfoCollection =
                                                    (from recItem in this._GLDataRecurringItemScheduleInfoCollection
                                                     //  where recItem.IsForwardedItem == true
                                                     where recItem.CloseDate == null
                                                     //TODO:(commented only for test purpose) 
                                                     //&& recItem.CreatedInRecPeriodID != SessionHelper.CurrentReconciliationPeriodID
                                                     select recItem).ToList();
        return oGLDataRecurringItemScheduleInfoCollection;
    }

    private void SetGridHeaders(Telerik.Web.UI.GridItemEventArgs e)
    {
        Control oControlOriginalAmountRCCY = new Control();
        Control oControlRecPeriodAmountReportingCurrency = new Control();
        Control oControlBalanceReportingCurrency = new Control();
        Control oControlOriginalAmountLCCY = new Control();

        GridHeaderItem headerItem = e.Item as GridHeaderItem;

        oControlOriginalAmountRCCY = (headerItem)["OriginalAmountRCCY"].Controls[0];
        oControlRecPeriodAmountReportingCurrency = (headerItem)["AmortizedAmountRCCY"].Controls[0];
        oControlBalanceReportingCurrency = (headerItem)["BalanceReportingCurrency"].Controls[0];
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

    #region Other Methods
    #endregion
    
}


