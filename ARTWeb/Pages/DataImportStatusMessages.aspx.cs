using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SkyStem.ART.Web.Classes;
using SkyStem.ART.Web.Utility;
using SkyStem.ART.Web.Data;
using SkyStem.ART.Client.Model;
using SkyStem.ART.Client.IServices;
using SkyStem.ART.Client.Exception;
using System.Text;
using SkyStem.Language.LanguageUtility;
using SkyStem.ART.Client.Data;
using System.Data;
using Telerik.Web.UI;
using SkyStem.Library.Controls.TelerikWebControls;
using SkyStem.Library.Controls.WebControls;
using SkyStem.Library.Controls.TelerikWebControls.Data;
using SharedUtility = SkyStem.ART.Shared.Utility;

public partial class Pages_DataImportStatusMessages : PageBaseCompany
{
    #region Variables & Constants
    int? _DataImportID = null;
    bool isExportPDF;
    bool isExportExcel;
    #endregion

    #region Properties
    DataImportHdrInfo oDataImportHdrInfo = null;
    #endregion

    #region Delegates & Events
    #endregion

    #region Page Events

    protected void Page_Load(object sender, EventArgs e)
    {
        SetPageSettings();
        Helper.GetUserFullName();

        try
        {
            _DataImportID = Convert.ToInt32(Request.QueryString[QueryStringConstants.DATA_IMPORT_ID]);

            if (!Page.IsPostBack)
            {
                LoadData();
                WebEnums.RecPeriodStatus eRecPeriodStatus = CurrentRecProcessStatus.Value;
                switch (eRecPeriodStatus)
                {
                    case WebEnums.RecPeriodStatus.Closed:
                    case WebEnums.RecPeriodStatus.Skipped:
                        btnYes.Visible = false;
                        btnReject.Visible = false;
                        lblConfirmUpload.Visible = false;
                        break;
                }
            }
        }
        catch (ARTException ex)
        {
            Helper.ShowErrorMessage(this, ex);
        }
        catch (Exception ex)
        {
            Helper.ShowErrorMessage(this, ex);
        }

    }
    protected void Page_PreRender(object sender, EventArgs e)
    {
        Helper.SetBreadcrumbs(this, 1219, 1621);
    }
    #endregion

    #region Grid Events

    protected void rgDataImportAccountMessages_ItemCreated(object sender, GridItemEventArgs e)
    {
        GridHelper.SetStylesForExportGrid(e, isExportPDF, isExportExcel);
        if (e.Item is GridCommandItem)
        {
            ImageButton ibExportToExcel = (e.Item as GridCommandItem).FindControl(TelerikConstants.GRID_ID_EXPORT_TO_EXCEL_ICON) as ImageButton;
            Helper.RegisterPostBackToControls(this, ibExportToExcel);

        }
        if (e.Item is GridCommandItem)
        {
            ImageButton ibExportToExcel = (e.Item as GridCommandItem).FindControl(TelerikConstants.GRID_ID_EXPORT_TO_PDF_ICON) as ImageButton;
            Helper.RegisterPostBackToControls(this, ibExportToExcel);
        }
    }

    protected void rgDataImportAccountMessages_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {
        if (e.Item.ItemType == Telerik.Web.UI.GridItemType.Item ||
            e.Item.ItemType == Telerik.Web.UI.GridItemType.AlternatingItem)
        {
            if (e.Item.OwnerTableView.Name != "rgAccountMessageDetails")
            {
                DataImportMessageDetailInfo oItem = (DataImportMessageDetailInfo)e.Item.DataItem;
                GridDataItem oGridDataItem = (GridDataItem)e.Item;
                if (oItem.AccountInfo != null)
                {
                    AccountViewerHelper.BindAccountFieldLabels(e, oItem.AccountInfo);
                }

                if (string.IsNullOrEmpty(oItem.MessageSchema) || string.IsNullOrEmpty(oItem.MessageData))
                    oGridDataItem["ExpandColumn"].Controls[0].Visible = false;

                Label lblExcelRowNumber = (Label)e.Item.FindControl("lblExcelRowNumber");
                if (lblExcelRowNumber != null)
                    lblExcelRowNumber.Text = Helper.GetDisplayIntegerValue(oItem.ExcelRowNumber);
            }
        }
        else if (e.Item.ItemType == GridItemType.GroupHeader)
        {
            ExImage imgSuccess = (ExImage)e.Item.FindControl("imgSuccess");
            ExImage imgFailure = (ExImage)e.Item.FindControl("imgFailure");
            ExImage imgWarning = (ExImage)e.Item.FindControl("imgWarning");
            Label lblDataImportMessage = (Label)e.Item.FindControl("lblDataImportMessage");
            GridGroupHeaderItem oGroupItem = (GridGroupHeaderItem)e.Item;
            if (oGroupItem.AggregatesValues["DataImportMessage"] != DBNull.Value)
                lblDataImportMessage.Text = oGroupItem.AggregatesValues["DataImportMessage"].ToString();
            if (oGroupItem.AggregatesValues["DataImportMessageTypeID"] != DBNull.Value)
            {
                short dataImportMessageType = (short)oGroupItem.AggregatesValues["DataImportMessageTypeID"];
                switch ((ARTEnums.DataImportMessageType)dataImportMessageType)
                {
                    case ARTEnums.DataImportMessageType.Success:
                        imgSuccess.Visible = true;
                        break;
                    case ARTEnums.DataImportMessageType.Error:
                        imgFailure.Visible = true;
                        break;
                    case ARTEnums.DataImportMessageType.Warning:
                        imgWarning.Visible = true;
                        break;
                }
            }
        }
    }

    protected void rgDataImportAccountMessages_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    {
        DataImportHdrInfo oDataImportHdrInfo = GetDataImportHdrInfo();
        if (!e.IsFromDetailTable)
        {
            rgDataImportAccountMessages.MasterTableView.DataSource = oDataImportHdrInfo.DataImportAccountMessageDetailInfoList;
        }
    }

    protected void rgDataImportAccountMessages_ItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == TelerikConstants.GridExportToPDFCommandName)
            {
                isExportPDF = true;
                GridHelper.ExportGridToPDF(rgDataImportAccountMessages, 1621);

            }
            if (e.CommandName == TelerikConstants.GridExportToExcelCommandName)
            {
                isExportExcel = true;
                GridHelper.ExportGridToExcel(rgDataImportAccountMessages, 1621);
            }
        }
        catch (Exception ex)
        {
            Helper.ShowErrorMessage(this, ex);
        }
    }

    protected void rgDataImportAccountMessages_DetailTableDataBind(object sender, Telerik.Web.UI.GridDetailTableDataBindEventArgs e)
    {
        try
        {
            GridDataItem oGridDataItem = (GridDataItem)e.DetailTableView.ParentItem;
            Int64? id = (Int64?)oGridDataItem.GetDataKeyValue("DataImportMessageDetailID");
            DataImportMessageDetailInfo oDataImportMessageDetailInfo = null;
            DataImportHdrInfo oDataImportHdrInfo = GetDataImportHdrInfo();
            if (oDataImportHdrInfo != null && oDataImportHdrInfo.DataImportAccountMessageDetailInfoList != null && oDataImportHdrInfo.DataImportAccountMessageDetailInfoList.Count > 0)
            {
                oDataImportMessageDetailInfo = oDataImportHdrInfo.DataImportAccountMessageDetailInfoList.Find(T => T.DataImportMessageDetailID == id);
                if (oDataImportMessageDetailInfo != null
                    && !string.IsNullOrEmpty(oDataImportMessageDetailInfo.MessageSchema)
                    && !string.IsNullOrEmpty(oDataImportMessageDetailInfo.MessageData))
                {
                    e.DetailTableView.Visible = true;
                    if (oGridDataItem.HasChildItems)
                        oGridDataItem.ChildItem.Visible = true;
                }
                else
                {
                    e.DetailTableView.Visible = false;
                    if (oGridDataItem.HasChildItems)
                        oGridDataItem.ChildItem.Visible = false;
                }
            }
            if (e.DetailTableView.Name == "rgAccountMessageDetails")
            {
                if (oDataImportMessageDetailInfo != null
                    && !string.IsNullOrEmpty(oDataImportMessageDetailInfo.MessageSchema)
                    && !string.IsNullOrEmpty(oDataImportMessageDetailInfo.MessageData))
                {
                    DataSet dsData = Helper.GetDataSet(oDataImportMessageDetailInfo.MessageSchema);
                    Helper.LoadXmlToDataSet(dsData, oDataImportMessageDetailInfo.MessageData);
                    Dictionary<string, string> dictTranslate = new Dictionary<string, string>();
                    List<string> visibleColumns = new List<string>();
                    DataColumn dcImportField = null;
                    e.DetailTableView.Columns.Clear();
                    for (int i = 0; i < dsData.Tables[0].Columns.Count; i++)
                    {
                        DataColumn dc = dsData.Tables[0].Columns[i];
                        if (dc.ExtendedProperties.ContainsKey("LabelFieldName") && !string.IsNullOrEmpty(dc.ExtendedProperties["LabelFieldName"].ToString()))
                            dictTranslate.Add(dc.ColumnName, dc.ExtendedProperties["LabelFieldName"].ToString());
                        if (dc.ExtendedProperties.ContainsKey("IsVisible") && Convert.ToBoolean(dc.ExtendedProperties["IsVisible"]))
                        {
                            visibleColumns.Add(dc.ColumnName);
                            ExGridBoundColumn gc = new ExGridBoundColumn();
                            gc.LabelID = Convert.ToInt32(dc.ExtendedProperties["HeaderLabelID"]);
                            gc.UniqueName = dc.ColumnName;
                            gc.DataField = dc.ColumnName;
                            gc.DataType = dc.DataType;
                            e.DetailTableView.Columns.Add(gc);
                        }
                        if (dc.ColumnName == "ImportFieldID")
                            dcImportField = dc;
                    }
                    if (visibleColumns.Count > 0)
                    {
                        foreach (DataRow dr in dsData.Tables[0].Rows)
                        {
                            foreach (string colName in visibleColumns)
                            {
                                if (dictTranslate.ContainsKey(colName))
                                {
                                    int labelID = 0;
                                    if (Int32.TryParse(dr[dictTranslate[colName]].ToString(), out labelID))
                                        dr[colName] = LanguageUtil.GetValue(labelID);
                                }
                                else if (dcImportField != null)
                                    dr[colName] = SharedUtility.SharedHelper.GetDisplayValueByImportFieldID(dr[colName].ToString(), dr[dcImportField].ToString(), null);
                                else
                                    dr[colName] = SharedUtility.SharedHelper.GetDisplayValueByImportFieldID(dr[colName].ToString(), null, null);
                            }
                        }
                    }
                    e.DetailTableView.DataSource = dsData.Tables[0];
                }
            }
        }
        catch (Exception ex)
        {
            Helper.LogException(ex);
        }
    }

    protected void rgDataImportMessages_ItemCreated(object sender, GridItemEventArgs e)
    {
        GridHelper.SetStylesForExportGrid(e, isExportPDF, isExportExcel);
        if (e.Item is GridCommandItem)
        {
            ImageButton ibExportToExcel = (e.Item as GridCommandItem).FindControl(TelerikConstants.GRID_ID_EXPORT_TO_EXCEL_ICON) as ImageButton;
            Helper.RegisterPostBackToControls(this, ibExportToExcel);

        }
        if (e.Item is GridCommandItem)
        {
            ImageButton ibExportToExcel = (e.Item as GridCommandItem).FindControl(TelerikConstants.GRID_ID_EXPORT_TO_PDF_ICON) as ImageButton;
            Helper.RegisterPostBackToControls(this, ibExportToExcel);
        }
    }

    protected void rgDataImportMessages_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {
        if (e.Item.ItemType == Telerik.Web.UI.GridItemType.Item ||
            e.Item.ItemType == Telerik.Web.UI.GridItemType.AlternatingItem)
        {
            if (e.Item.OwnerTableView.Name != "rgMessageDetails")
            {
                DataImportMessageDetailInfo oItem = (DataImportMessageDetailInfo)e.Item.DataItem;
                GridDataItem oGridDataItem = (GridDataItem)e.Item;

                if (string.IsNullOrEmpty(oItem.MessageSchema) || string.IsNullOrEmpty(oItem.MessageData))
                    oGridDataItem["ExpandColumn"].Controls[0].Visible = false;

                Label lblExcelRowNumber = (Label)e.Item.FindControl("lblExcelRowNumber");
                if (lblExcelRowNumber != null)
                    lblExcelRowNumber.Text = Helper.GetDisplayIntegerValue(oItem.ExcelRowNumber);
            }
        }
        else if (e.Item.ItemType == GridItemType.GroupHeader)
        {
            ExImage imgSuccess = (ExImage)e.Item.FindControl("imgSuccess");
            ExImage imgFailure = (ExImage)e.Item.FindControl("imgFailure");
            ExImage imgWarning = (ExImage)e.Item.FindControl("imgWarning");
            Label lblDataImportMessage = (Label)e.Item.FindControl("lblDataImportMessage");
            GridGroupHeaderItem oGroupItem = (GridGroupHeaderItem)e.Item;
            if (oGroupItem.AggregatesValues["DataImportMessage"] != DBNull.Value)
                lblDataImportMessage.Text = oGroupItem.AggregatesValues["DataImportMessage"].ToString();
            if (oGroupItem.AggregatesValues["DataImportMessageTypeID"] != DBNull.Value)
            {
                short dataImportMessageType = (short)oGroupItem.AggregatesValues["DataImportMessageTypeID"];
                switch ((ARTEnums.DataImportMessageType)dataImportMessageType)
                {
                    case ARTEnums.DataImportMessageType.Success:
                        imgSuccess.Visible = true;
                        break;
                    case ARTEnums.DataImportMessageType.Error:
                        imgFailure.Visible = true;
                        break;
                    case ARTEnums.DataImportMessageType.Warning:
                        imgWarning.Visible = true;
                        break;
                }
            }
        }
    }

    protected void rgDataImportMessages_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    {
        DataImportHdrInfo oDataImportHdrInfo = GetDataImportHdrInfo();
        if (!e.IsFromDetailTable)
        {
            rgDataImportMessages.MasterTableView.DataSource = oDataImportHdrInfo.DataImportMessageDetailInfoList;
        }
    }

    protected void rgDataImportMessages_ItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == TelerikConstants.GridExportToPDFCommandName)
            {
                isExportPDF = true;
                GridHelper.ExportGridToPDF(rgDataImportMessages, 1621);

            }
            if (e.CommandName == TelerikConstants.GridExportToExcelCommandName)
            {
                isExportExcel = true;
                GridHelper.ExportGridToExcel(rgDataImportMessages, 1621);
            }
        }
        catch (Exception ex)
        {
            Helper.ShowErrorMessage(this, ex);
        }
    }

    protected void rgDataImportMessages_DetailTableDataBind(object sender, Telerik.Web.UI.GridDetailTableDataBindEventArgs e)
    {
        try
        {
            GridDataItem oGridDataItem = (GridDataItem)e.DetailTableView.ParentItem;
            Int64? id = (Int64?)oGridDataItem.GetDataKeyValue("DataImportMessageDetailID");
            DataImportMessageDetailInfo oDataImportMessageDetailInfo = null;
            DataImportHdrInfo oDataImportHdrInfo = GetDataImportHdrInfo();
            if (oDataImportHdrInfo != null && oDataImportHdrInfo.DataImportMessageDetailInfoList != null && oDataImportHdrInfo.DataImportMessageDetailInfoList.Count > 0)
            {
                oDataImportMessageDetailInfo = oDataImportHdrInfo.DataImportMessageDetailInfoList.Find(T => T.DataImportMessageDetailID == id);
                if (oDataImportMessageDetailInfo != null
                    && !string.IsNullOrEmpty(oDataImportMessageDetailInfo.MessageSchema)
                    && !string.IsNullOrEmpty(oDataImportMessageDetailInfo.MessageData))
                {
                    e.DetailTableView.Visible = true;
                    if (oGridDataItem.HasChildItems)
                        oGridDataItem.ChildItem.Visible = true;
                }
                else
                {
                    e.DetailTableView.Visible = false;
                    if (oGridDataItem.HasChildItems)
                        oGridDataItem.ChildItem.Visible = false;
                }
            }
            if (e.DetailTableView.Name == "rgMessageDetails")
            {
                if (oDataImportMessageDetailInfo != null
                    && !string.IsNullOrEmpty(oDataImportMessageDetailInfo.MessageSchema)
                    && !string.IsNullOrEmpty(oDataImportMessageDetailInfo.MessageData))
                {
                    DataSet dsData = Helper.GetDataSet(oDataImportMessageDetailInfo.MessageSchema);
                    Helper.LoadXmlToDataSet(dsData, oDataImportMessageDetailInfo.MessageData);
                    Dictionary<string, string> dictTranslate = new Dictionary<string, string>();
                    List<string> visibleColumns = new List<string>();
                    DataColumn dcImportField = null;
                    e.DetailTableView.Columns.Clear();
                    for (int i = 0; i < dsData.Tables[0].Columns.Count; i++)
                    {
                        DataColumn dc = dsData.Tables[0].Columns[i];
                        if (dc.ExtendedProperties.ContainsKey("LabelFieldName") && !string.IsNullOrEmpty(dc.ExtendedProperties["LabelFieldName"].ToString()))
                            dictTranslate.Add(dc.ColumnName, dc.ExtendedProperties["LabelFieldName"].ToString());
                        if (dc.ExtendedProperties.ContainsKey("IsVisible") && Convert.ToBoolean(dc.ExtendedProperties["IsVisible"]))
                        {
                            visibleColumns.Add(dc.ColumnName);
                            ExGridBoundColumn gc = new ExGridBoundColumn();
                            gc.LabelID = Convert.ToInt32(dc.ExtendedProperties["HeaderLabelID"]);
                            gc.UniqueName = dc.ColumnName;
                            gc.DataField = dc.ColumnName;
                            gc.DataType = dc.DataType;
                            e.DetailTableView.Columns.Add(gc);
                        }
                        if (dc.ColumnName == "ImportFieldID")
                            dcImportField = dc;
                    }
                    if (visibleColumns.Count > 0)
                    {
                        foreach (DataRow dr in dsData.Tables[0].Rows)
                        {
                            foreach (string colName in visibleColumns)
                            {
                                if (dictTranslate.ContainsKey(colName))
                                {
                                    int labelID = 0;
                                    if (Int32.TryParse(dr[dictTranslate[colName]].ToString(), out labelID))
                                        dr[colName] = LanguageUtil.GetValue(labelID);
                                }
                                else if (dcImportField != null)
                                    dr[colName] = SharedUtility.SharedHelper.GetDisplayValueByImportFieldID(dr[colName].ToString(), dr[dcImportField].ToString(), null);
                                else
                                    dr[colName] = SharedUtility.SharedHelper.GetDisplayValueByImportFieldID(dr[colName].ToString(), null, null);
                            }
                        }
                    }
                    e.DetailTableView.DataSource = dsData.Tables[0];
                }
            }
        }
        catch (Exception ex)
        {
            Helper.LogException(ex);
        }
    }

    #endregion

    #region Other Events
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect(GetUrlForStatusPage());
    }

    protected void btnYes_Click(object sender, EventArgs e)
    {
        /*
         * 1. Update DB to Mark as Force Commite
         * 2. Redirect to Data Import Status Page
         * 
         */

        try
        {
            oDataImportHdrInfo = GetDataImportHdrInfo();
            UpdateDataImportStatus((short)WebEnums.DataImportStatus.Submitted);
            if (SessionHelper.CurrentUserLoginID != oDataImportHdrInfo.AddedBy)
            {
                int DataImportTypeLabelID;
                if (oDataImportHdrInfo.DataImportTypeLabelID == (short)ARTEnums.DataImportType.AccountUpload)
                    DataImportTypeLabelID = 2455;
                else
                    DataImportTypeLabelID = oDataImportHdrInfo.DataImportTypeLabelID.Value;
                String msg = string.Format(LanguageUtil.GetValue(2483), LanguageUtil.GetValue(DataImportTypeLabelID), Helper.GetUserFullName(SessionHelper.CurrentUserID));
                string mailSubject = string.Format("{0}", LanguageUtil.GetValue(2484));
                SendMailToUser(msg, mailSubject);
            }
            string url = GetUrlForStatusPage();
            url += "?" + QueryStringConstants.CONFIRMATION_MESSAGE_LABEL_ID + "=1784";
            CacheHelper.ClearExchangeRateByRecPeriodID(SessionHelper.CurrentReconciliationPeriodID.GetValueOrDefault());
            Response.Redirect(url);
        }
        catch (ARTException ex)
        {
            Helper.ShowErrorMessage(this, ex);
        }
        catch (Exception ex)
        {
            Helper.ShowErrorMessage(this, ex);
        }
    }
    protected void btnReject_Click(object sender, EventArgs e)
    {
        /*
         * 1. Update DB to Mark as Reject Import
         * 2. Redirect to Data Import Status Page
         * 
         */
        oDataImportHdrInfo = GetDataImportHdrInfo();

        try
        {
            UpdateDataImportStatus((short)WebEnums.DataImportStatus.Reject);
            if (SessionHelper.CurrentUserLoginID != oDataImportHdrInfo.AddedBy)
            {
                int DataImportTypeLabelID;
                if (oDataImportHdrInfo.DataImportTypeLabelID == (short)ARTEnums.DataImportType.AccountUpload)
                    DataImportTypeLabelID = 2455;
                else
                    DataImportTypeLabelID = oDataImportHdrInfo.DataImportTypeLabelID.Value;
                String msg = string.Format(LanguageUtil.GetValue(2456), LanguageUtil.GetValue(DataImportTypeLabelID), Helper.GetUserFullName(SessionHelper.CurrentUserID));
                string mailSubject = string.Format("{0}", LanguageUtil.GetValue(2400));
                SendMailToUser(msg, mailSubject);
            }
            string url = GetUrlForStatusPage();
            url += "?" + QueryStringConstants.CONFIRMATION_MESSAGE_LABEL_ID + "=2399";
            Response.Redirect(url);
        }
        catch (ARTException ex)
        {
            Helper.ShowErrorMessage(this, ex);
        }
        catch (Exception ex)
        {
            Helper.ShowErrorMessage(this, ex);
        }
    }
    #endregion

    #region Validation Control Events
    #endregion

    #region Private Methods
    private DataImportHdrInfo GetDataImportHdrInfo()
    {
        DataImportHdrInfo _oDataImportHdrInfo = null;

        if (ViewState["DataImportHdrInfo"] == null)
        {
            IDataImport oDataImportClient = RemotingHelper.GetDataImportObject();
            _oDataImportHdrInfo = oDataImportClient.GetDataImportInfo(_DataImportID, Helper.GetAppUserInfo());
            LanguageHelper.TranslateLabelDataImportHdr(_oDataImportHdrInfo);
            ViewState["DataImportHdrInfo"] = _oDataImportHdrInfo;

        }
        else
            _oDataImportHdrInfo = (DataImportHdrInfo)ViewState["DataImportHdrInfo"];
        return _oDataImportHdrInfo;

    }

    private void LoadData()
    {
        oDataImportHdrInfo = GetDataImportHdrInfo();
        WebEnums.DataImportStatus eDataImportStatus = (WebEnums.DataImportStatus)System.Enum.Parse(typeof(WebEnums.DataImportStatus), oDataImportHdrInfo.DataImportStatusID.Value.ToString());

        pnlSuccess.Visible = false;
        pnlWarning.Visible = false;
        pnlFailureMessages.Visible = false;
        btnYes.Visible = false;
        btnReject.Visible = false;

        lblProfileNameValue.Text = Helper.GetDisplayStringValue(oDataImportHdrInfo.DataImportName);
        lblDataImportTypeValue.LabelID = oDataImportHdrInfo.DataImportTypeLabelID.Value;
        lblLoadDateValue.Text = Helper.GetDisplayDateTime(oDataImportHdrInfo.DateAdded);

        switch (eDataImportStatus)
        {
            case WebEnums.DataImportStatus.Success:
                LoadSuccessPanel(oDataImportHdrInfo);
                break;

            case WebEnums.DataImportStatus.Failure:
                LoadFailurePanel(oDataImportHdrInfo);
                break;


            case WebEnums.DataImportStatus.Warning:
                /* If Force commit, is already enabled and Status = Warning
                 * - Means User has already put the file for Force Commit 
                 * - Show a similar message
                 */
                LoadWarningPanel(oDataImportHdrInfo);
                break;

            case WebEnums.DataImportStatus.Processing:
                imgProcessing.Visible = true;
                lblStatusHeading.LabelID = 1620;
                lblMessage.LabelID = 1619;
                lblMessage.FormatString = "";
                break;

            case WebEnums.DataImportStatus.Submitted:
                imgToBeProcessed.Visible = true;
                lblStatusHeading.LabelID = 1730;
                lblMessage.LabelID = 1783;
                lblMessage.FormatString = "";
                break;
            case WebEnums.DataImportStatus.Reject:
                imgReject.Visible = true;
                lblStatusHeading.LabelID = 2400;
                lblMessage.LabelID = 2400;
                lblMessage.FormatString = "";
                break;
        }
        if (oDataImportHdrInfo.DataImportAccountMessageDetailInfoList != null && oDataImportHdrInfo.DataImportAccountMessageDetailInfoList.Count > 0)
        {
            GridColumn startCol = rgDataImportAccountMessages.Columns.FindByUniqueNameSafe("Key2");
            if (startCol != null)
            {
                int index = rgDataImportAccountMessages.Columns.IndexOf(startCol);
                GridHelper.ShowHideColumns(index, rgDataImportAccountMessages.MasterTableView, SessionHelper.CurrentCompanyID, ARTEnums.Grid.DataImportStatusAccountMessage, false);
            }
        }
    }

    private void LoadFailurePanel(DataImportHdrInfo oDataImportHdrInfo)
    {
        pnlFailureMessages.Visible = false;

        imgFailure.Visible = true;
        lblStatusHeading.LabelID = 1051;
        lblMessage.LabelID = 1623;

        lblFailureMessages.Text = FormatFailureMessage(oDataImportHdrInfo.DataImportFailureMessageInfo.FailureMessage);

        if (oDataImportHdrInfo.DataImportTypeID == (short)ARTEnums.DataImportType.GLData
            || oDataImportHdrInfo.DataImportTypeID == (short)ARTEnums.DataImportType.SubledgerData
            || oDataImportHdrInfo.DataImportTypeID == (short)ARTEnums.DataImportType.CurrencyExchangeRateData   
            )
        {
            if (oDataImportHdrInfo.DataImportAccountMessageDetailInfoList != null && oDataImportHdrInfo.DataImportAccountMessageDetailInfoList.Count > 0)
                pnlDataImportAccountMessages.Visible = true;
            if (oDataImportHdrInfo.DataImportMessageDetailInfoList != null && oDataImportHdrInfo.DataImportMessageDetailInfoList.Count > 0)
                pnlDataImportMessages.Visible = true;
            pnlFailureMessages.Visible = !(pnlDataImportAccountMessages.Visible || pnlDataImportMessages.Visible);
        }
        else
            pnlFailureMessages.Visible = true;
    }

    private void LoadSuccessPanel(DataImportHdrInfo oDataImportHdrInfo)
    {
        pnlSuccess.Visible = true;

        imgSuccess.Visible = true;
        lblStatusHeading.LabelID = 1050;

        lblNoOfRecordsValue.Text = Helper.GetDisplayIntegerValue(oDataImportHdrInfo.RecordsImported);
        lblForceCommitDateValue.Text = Helper.GetDisplayDateTime(oDataImportHdrInfo.ForceCommitDate);

        lblMessage.LabelID = 1618;
        lblMessage.FormatString = "";

        if (oDataImportHdrInfo.DataImportTypeID == (short)ARTEnums.DataImportType.GLData
            || oDataImportHdrInfo.DataImportTypeID == (short)ARTEnums.DataImportType.SubledgerData
            || oDataImportHdrInfo.DataImportTypeID == (short)ARTEnums.DataImportType.CurrencyExchangeRateData
            )
        {
            if (oDataImportHdrInfo.DataImportAccountMessageDetailInfoList != null && oDataImportHdrInfo.DataImportAccountMessageDetailInfoList.Count > 0)
                pnlDataImportAccountMessages.Visible = true;
            if (oDataImportHdrInfo.DataImportMessageDetailInfoList != null && oDataImportHdrInfo.DataImportMessageDetailInfoList.Count > 0)
                pnlDataImportMessages.Visible = true;
        }
    }

    private void LoadWarningPanel(DataImportHdrInfo oDataImportHdrInfo)
    {
        if (oDataImportHdrInfo.DataImportTypeID == (short)ARTEnums.DataImportType.GLData
            || oDataImportHdrInfo.DataImportTypeID == (short)ARTEnums.DataImportType.SubledgerData
            || oDataImportHdrInfo.DataImportTypeID == (short)ARTEnums.DataImportType.CurrencyExchangeRateData
            )
        {
            if (oDataImportHdrInfo.DataImportAccountMessageDetailInfoList != null && oDataImportHdrInfo.DataImportAccountMessageDetailInfoList.Count > 0)
                pnlDataImportAccountMessages.Visible = true;
            if (oDataImportHdrInfo.DataImportMessageDetailInfoList != null && oDataImportHdrInfo.DataImportMessageDetailInfoList.Count > 0)
                pnlDataImportMessages.Visible = true;
        }
        else
        {
            pnlWarning.Visible = true;
            pnlSuccess.Visible = true;
            pnlFailureMessages.Visible = true;
        }

        imgWarning.Visible = true;
        lblStatusHeading.LabelID = 1546;
        lblMessage.LabelID = 1624;

        lblNoOfRecordsValue.Text = Helper.GetDisplayIntegerValue(oDataImportHdrInfo.RecordsImported);
        lblForceCommitDateValue.Text = Helper.GetDisplayDateTime(oDataImportHdrInfo.ForceCommitDate);
        lblFailureMessages.Text = FormatFailureMessage(oDataImportHdrInfo.DataImportFailureMessageInfo.FailureMessage);

        if (!oDataImportHdrInfo.IsForceCommit.HasValue)
        {
            btnYes.Visible = true;
            btnReject.Visible = true;
            lblConfirmUpload.LabelID = 1548;
        }
        else
        {
            lblConfirmUpload.LabelID = 1784;
        }

    }

    private string FormatFailureMessage(string msg)
    {
        msg = msg.Replace(" , ", "<br>");
        msg = msg.Replace(" ,", "<br>");
        msg = msg.Replace(",", "<br>");
        return msg;
    }
    private void UpdateDataImportStatus(short DataImportStatusID)
    {
        DataImportHdrInfo oDataImportHdrInfo = new DataImportHdrInfo();
        oDataImportHdrInfo.DataImportID = _DataImportID;
        oDataImportHdrInfo.DataImportStatusID = DataImportStatusID;
        oDataImportHdrInfo.IsForceCommit = true;
        oDataImportHdrInfo.ForceCommitDate = DateTime.Now;
        oDataImportHdrInfo.DateRevised = DateTime.Now;
        oDataImportHdrInfo.RevisedBy = SessionHelper.CurrentUserLoginID;
        if (DataImportStatusID == (short)WebEnums.DataImportStatus.Submitted)
        {
            DataImportHdrInfo oDataImportHdrInfoDB = GetDataImportHdrInfo();
            if (oDataImportHdrInfoDB.DataImportTypeID != (short)ARTEnums.DataImportType.UserUpload)
                oDataImportHdrInfo.SystemLockdownInfo = Helper.GetSystemLockdownInfo(ARTEnums.SystemLockdownReason.UploadDataProcessingRequired);
        }
        IDataImport oDataImportClient = RemotingHelper.GetDataImportObject();
        oDataImportClient.UpdateDataImportForForceCommit(oDataImportHdrInfo, Helper.GetAppUserInfo());
    }
    private string GetUrlForStatusPage()
    {
        return "DataImportStatus.aspx";
    }
    private void SendMailToUser(string msg, string Subject)
    {
        try
        {

            StringBuilder oMailBody = new StringBuilder();
            oMailBody.Append(msg);
            //oMailBody.Append(string.Format(LanguageUtil.GetValue(2456), LanguageUtil.GetValue(DataImportTypeLabelID), Helper.GetUserFullName (SessionHelper .CurrentUserID)));
            oMailBody.Append("<br>");
            oMailBody.Append("<br>");

            string fromAddress = AppSettingHelper.GetAppSettingValue(AppSettingConstants.EMAIL_FROM_DEFAULT);
            if (SessionHelper.CurrentRoleEnum == WebEnums.UserRole.SKYSTEM_ADMIN)
            {
                oMailBody.Append("<br/>" + MailHelper.GetEmailSignature(WebEnums.SignatureEnum.SendBySkyStemSystem, fromAddress));
            }
            else
            {
                oMailBody.Append("<br/>" + MailHelper.GetEmailSignature(WebEnums.SignatureEnum.SendBySystemAdmin, fromAddress));
            }

            string mailSubject = Subject;

            string toAddress = oDataImportHdrInfo.EmailID;
            MailHelper.SendEmail(fromAddress, toAddress, mailSubject, oMailBody.ToString());
        }
        catch (Exception ex)
        {
            Helper.FormatAndShowErrorMessage(null, ex);
        }
    }

    /// <summary>
    /// Sets the page settings.
    /// </summary>
    private void SetPageSettings()
    {
        Helper.SetPageTitle(this, 1621);
        MasterPageBase oMasterPageBase = (MasterPageBase)this.Master;
        MasterPageSettings oMasterPageSettings = new MasterPageSettings();
        oMasterPageSettings.EnableRecPeriodSelection = false;
        oMasterPageBase.SetMasterPageSettings(oMasterPageSettings);
    }

    #endregion

    #region Other Methods
    public override string GetMenuKey()
    {
        return "DataImportStatus";
    }
    #endregion
}
