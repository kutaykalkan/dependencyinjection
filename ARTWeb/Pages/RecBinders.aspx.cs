using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SkyStem.ART.Client.Data;
using SkyStem.ART.Web.Classes;
using SkyStem.ART.Web.Utility;
using SkyStem.ART.Client.Model.BulkExportExcel;
using Telerik.Web.UI;
using SkyStem.ART.Web.Data;
using SkyStem.Library.Controls.TelerikWebControls.Data;

public partial class Pages_RecBinders : PageBase
{
    bool isExportPDF;
    bool isExportExcel;
    #region "Page event Handlers"
    protected void Page_Load(object sender, EventArgs e)
    {
        Helper.SetPageTitle(this, 2826);
        SetMasterPageSettings();
        if (!Page.IsPostBack)
        {
            Helper.SetBreadcrumbs(this, 1071, 2826);
            hdnNewPageSize.Value = "10";
            isExportPDF = false;
            isExportExcel = false;
            PopulateData();
        }
        rgRecBinders.MasterTableView.GroupByExpressions.AddRange(RequestHelper.GetGridGroupByExpressionForERecBinders());
    }
    #endregion

    private void SetMasterPageSettings()
    {
        MasterPageBase oMasterPageBase = (MasterPageBase)this.Master;
        MasterPageSettings oMasterPageSettings = new MasterPageSettings();
        oMasterPageSettings.EnableRecPeriodSelection = false;
        oMasterPageBase.SetMasterPageSettings(oMasterPageSettings);
    }

    private void PopulateData()
    {
        List<BulkExportToExcelInfo> oBulkExportToExcelInfoList = RequestHelper.GetAllRecBinders(new List<short> { (short)ARTEnums.RequestType.CreateBinders });

        if (oBulkExportToExcelInfoList != null)
            rgRecBinders.VirtualItemCount = oBulkExportToExcelInfoList.Count;
        if (rgRecBinders.AllowCustomPaging && !(isExportPDF || isExportExcel))
        {
            List<BulkExportToExcelInfo> oTempBulkExportToExcelInfoList = new List<BulkExportToExcelInfo>();
            oTempBulkExportToExcelInfoList = oBulkExportToExcelInfoList.Skip(rgRecBinders.CurrentPageIndex * rgRecBinders.PageSize).Take(rgRecBinders.PageSize).ToList();
            rgRecBinders.DataSource = oTempBulkExportToExcelInfoList;
        }
        else
        {
            rgRecBinders.DataSource = oBulkExportToExcelInfoList;
            rgRecBinders.VirtualItemCount = oBulkExportToExcelInfoList.Count;
        }
    }

    #region Grid Handlers
    protected void rgRecBinders_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {
        if (e.Item.ItemType == GridItemType.Item || e.Item.ItemType == GridItemType.AlternatingItem)
        {
            RequestHelper.BindCommonFields(WebEnums.ARTPages.RecBinders, e);
            BulkExportToExcelInfo oBulkExportToExcelInfo = (BulkExportToExcelInfo)e.Item.DataItem;
            if ((e.Item as GridDataItem)["DeleteColumn"] != null)
            {
                ImageButton deleteButton = (ImageButton)(e.Item as GridDataItem)["DeleteColumn"].Controls[0];
                if (SessionHelper.CurrentUserLoginID == oBulkExportToExcelInfo.AddedBy)
                    deleteButton.Visible = true;
                else
                {
                    deleteButton.Visible = false;
                }
                deleteButton.CommandArgument = oBulkExportToExcelInfo.RequestID.ToString();
            }
        }
    }
    protected void rgRecBinders_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    {
        PopulateData();
    }

    protected void rgRecBinders_SortCommand(object source, GridSortCommandEventArgs e)
    {
        GridHelper.HandleSortCommand(e);
        rgRecBinders.Rebind();
    }
    protected void rgRecBinders_ItemCreated(object sender, GridItemEventArgs e)
    {
        if (e.Item is GridPagerItem)
        {
            GridPagerItem gridPager = e.Item as GridPagerItem;
            DropDownList oRadComboBox = (DropDownList)gridPager.FindControl("ddlPageSize");
            if (rgRecBinders.AllowCustomPaging)
            {
                GridHelper.BindPageSizeGrid(oRadComboBox);
                oRadComboBox.SelectedValue = hdnNewPageSize.Value.ToString();
                oRadComboBox.Attributes.Add("onChange", "return ddlPageSize_SelectedIndexChanged('" + oRadComboBox.ClientID + "' , '" + rgRecBinders.ClientID + "');");
                oRadComboBox.Visible = true;
            }
            else
            {
                Control pnlPageSizeDDL = gridPager.FindControl("pnlPageSizeDDL");
                pnlPageSizeDDL.Visible = false;
            }
            Control numericPagerControl = gridPager.GetNumericPager();
            Control placeHolder = gridPager.FindControl("NumericPagerPlaceHolder");
            placeHolder.Controls.Add(numericPagerControl);
        }

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
    protected void rgRecBinders_ItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == TelerikConstants.GridExportToPDFCommandName)
            {
                isExportPDF = true;
                rgRecBinders.MasterTableView.Columns.FindByUniqueName("FileDownloadIconColumn").Visible = false;
                GridHelper.ExportGridToPDF(rgRecBinders, this.PageTitleLabelID);
            }
            if (e.CommandName == TelerikConstants.GridExportToExcelCommandName)
            {
                isExportExcel = true;
                rgRecBinders.MasterTableView.Columns.FindByUniqueName("FileDownloadIconColumn").Visible = false;
                GridHelper.ExportGridToExcel(rgRecBinders, this.PageTitleLabelID);
            }
            if (e.CommandName == TelerikConstants.GridRefreshCommandName)
            {
                PopulateData();
                rgRecBinders.Rebind();
            }
            if (e.CommandName == "Delete")
            {
                List<int> oRequestIDCollection = new List<int>();
                int RequestID;
                if (Int32.TryParse(e.CommandArgument.ToString(), out RequestID))
                    oRequestIDCollection.Add(RequestID);
                if (oRequestIDCollection.Count > 0 && SessionHelper.CurrentCompanyID.HasValue)
                {
                    RequestHelper.DeleteRequest(oRequestIDCollection);
                    PopulateData();
                    this.rgRecBinders.Rebind();
                }
            }
        }
        catch (Exception ex)
        {
            Helper.ShowErrorMessage(this, ex);
        }
    }

    protected void rgRecBinders_PageIndexChanged(object source, GridPageChangedEventArgs e)
    {
        rgRecBinders.CurrentPageIndex = e.NewPageIndex;        
        PopulateData();
       
    }
    protected void rgRecBinders_PageSizeChanged(object source, GridPageSizeChangedEventArgs e)
    {
        hdnNewPageSize.Value = e.NewPageSize.ToString();
    }

    #endregion

    public override string GetMenuKey()
    {
        return "ERecBinders";
    }
}