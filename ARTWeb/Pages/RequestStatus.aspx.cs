using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SkyStem.ART.Client.IServices;
using SkyStem.ART.Client.Model;
using SkyStem.Library.Controls.WebControls;
using SkyStem.ART.Web.Utility;
using SkyStem.ART.Web.Classes;
using SkyStem.ART.Web.Data;
using Telerik.Web.UI;
using SkyStem.Language.LanguageUtility;
using SkyStem.ART.Client.Exception;
using SkyStem.Library.Controls.TelerikWebControls.Data;
using SkyStem.ART.Client.Model.BulkExportExcel;
using SkyStem.ART.Client.IServices.BulkExportToExcel;
using SkyStem.ART.Client.Data;

public partial class Pages_RequestStatus : PageBaseCompany
{
    bool isExportPDF;
    bool isExportExcel;
    #region "Page event Handlers"
    protected void Page_Init(object sender, EventArgs e)
    {
        MasterPageBase oMaster = (MasterPageBase)this.Master;
        oMaster.ReconciliationPeriodChangedEventHandler += new EventHandler(oMaster_ReconciliationPeriodChangedEventHandler);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        Helper.SetPageTitle(this, 2819);
        if (!Page.IsPostBack)
        {
            Helper.SetBreadcrumbs(this, 1074, 2819);
            isExportPDF = false;
            isExportExcel = false;
        }
        Sel.Value = "";
        this.btnDeleteDataImport.Visible = true;
    }
    #endregion

    #region "Data Import Grid By Rec Period ID"
    protected void rgRequests_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
    {
        rgRequestsLoadData();
    }
    private void rgRequestsLoadData()
    {
        try
        {
            List<BulkExportToExcelInfo> oBulkExportToExcelInfoCollection = null;
            // Get All requests done in the Current Rec Period for current user and role
            if (SessionHelper.CurrentReconciliationPeriodID != null)
            {
                List<short> RequestTypeList = new List<short>();
                RequestTypeList.Add((short)ARTEnums.RequestType.ExportToExcel);
                RequestTypeList.Add((short)ARTEnums.RequestType.DownloadAllRecFormsDetailed);
                RequestTypeList.Add((short)ARTEnums.RequestType.CreateBinders);
                RequestTypeList.Add((short)ARTEnums.RequestType.DownloadSelectedRecFormsDetailed);
                oBulkExportToExcelInfoCollection = RequestHelper.GetRequests(RequestTypeList);
                var query = from d in oBulkExportToExcelInfoCollection
                            where d.AddedBy == SessionHelper.CurrentUserLoginID
                            select d;
                foreach (var d in query)
                {
                    d.IsRecordOwner = true;
                }
            }
            else
            {
                oBulkExportToExcelInfoCollection = new List<BulkExportToExcelInfo>();
            }
            rgRequests.MasterTableView.DataSource = oBulkExportToExcelInfoCollection;
            GridHelper.SortDataSource(rgRequests.MasterTableView);
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
    protected void rgRequests_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {
        if (e.Item.ItemType == Telerik.Web.UI.GridItemType.Item ||
            e.Item.ItemType == Telerik.Web.UI.GridItemType.AlternatingItem)
        {
            RequestHelper.BindCommonFields(WebEnums.ARTPages.RequestStatus, e);
            GridDataItem item = (GridDataItem)e.Item;
            CheckBox checkBox = (CheckBox)item["CheckboxSelectColumn"].Controls[0];
            BulkExportToExcelInfo oBulkExportToExcelInfo = (BulkExportToExcelInfo)e.Item.DataItem;
            if (SessionHelper.CurrentUserLoginID == oBulkExportToExcelInfo.AddedBy)
                checkBox.Enabled = true;
            else
            {
                checkBox.Enabled = false;
                Sel.Value += e.Item.ItemIndex.ToString() + ":";
            }
        }
    }
    protected void rgRequests_SortCommand(object source, GridSortCommandEventArgs e)
    {
        GridHelper.HandleSortCommand(e);
        rgRequests.Rebind();
    }
    protected void rgRequests_ItemCreated(object sender, GridItemEventArgs e)
    {
        GridHelper.SetStylesForExportGrid(e, isExportPDF, isExportExcel);
        if (e.Item is GridCommandItem)
        {
            ImageButton ibExportToExcel = (e.Item as GridCommandItem).FindControl(TelerikConstants.GRID_ID_EXPORT_TO_EXCEL_ICON) as ImageButton;
            Helper.RegisterPostBackToControls(this, ibExportToExcel);

        }
        if (e.Item is GridCommandItem)
        {
            ImageButton ibExportToPDF = (e.Item as GridCommandItem).FindControl(TelerikConstants.GRID_ID_EXPORT_TO_PDF_ICON) as ImageButton;
            Helper.RegisterPostBackToControls(this, ibExportToPDF);

        }
    }
    protected void rgRequests_ItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == TelerikConstants.GridExportToPDFCommandName)
            {
                isExportPDF = true;
                rgRequests.MasterTableView.Columns.FindByUniqueName("imgStatus").Visible = false;
                rgRequests.MasterTableView.Columns.FindByUniqueName("CheckboxSelectColumn").Visible = false;
                rgRequests.MasterTableView.Columns.FindByUniqueName("FileDownloadIconColumn").Visible = false;
                GridHelper.ExportGridToPDF(rgRequests, 1219);
            }
            if (e.CommandName == TelerikConstants.GridExportToExcelCommandName)
            {
                isExportExcel = true;
                rgRequests.MasterTableView.Columns.FindByUniqueName("imgStatus").Visible = false;
                rgRequests.MasterTableView.Columns.FindByUniqueName("CheckboxSelectColumn").Visible = false;
                rgRequests.MasterTableView.Columns.FindByUniqueName("FileDownloadIconColumn").Visible = false;
                GridHelper.ExportGridToExcel(rgRequests, 1219);
            }
            if (e.CommandName == TelerikConstants.GridRefreshCommandName)
            {
                rgRequests.Rebind();
            }
        }
        catch (Exception ex)
        {
            Helper.ShowErrorMessage(this, ex);
        }
    }
    #endregion
    void oMaster_ReconciliationPeriodChangedEventHandler(object sender, EventArgs e)
    {
        rgRequests.Rebind();
        MasterPageBase oMasterPageBase = (MasterPageBase)this.Master;
        oMasterPageBase.HideMessage();
    }
    protected void btnDeleteDataImport_Click(object sender, EventArgs e)
    {
        MasterPageBase oMasterPageBase = (MasterPageBase)this.Master;
        oMasterPageBase.HideMessage();
        List<int> oRequestIDCollection = new List<int>();
        try
        {
            GridDataItem[] dataItemCollection = this.rgRequests.MasterTableView.GetSelectedItems();
            if (dataItemCollection.Length > 0)
            {
                int RequestID;
                foreach (GridDataItem item in dataItemCollection)
                {
                    if (Int32.TryParse(item.GetDataKeyValue("RequestID").ToString(), out RequestID))
                        oRequestIDCollection.Add(RequestID);
                }               
                if (oRequestIDCollection.Count > 0 && SessionHelper.CurrentCompanyID.HasValue)
                {
                    RequestHelper.DeleteRequest(oRequestIDCollection);
                    this.rgRequests.Rebind();
                    oMasterPageBase.ShowConfirmationMessage(2825);
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
    public override string GetMenuKey()
    {
        return "MyDownloadRequests";
    }

}
