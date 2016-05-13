using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SkyStem.ART.Web.Utility;
using SkyStem.ART.Client.Model.Report;
using SkyStem.ART.Web.Data;
using SkyStem.ART.Client.Exception;
using Telerik.Web.UI;

public partial class Pages_ReportsPrint_QualityScoreReportPrint : PageBaseReport
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ucSkyStemARTGrid.CompanyID = SessionHelper.CurrentCompanyID.Value;
        GridHelper.ShowHideColumnsBasedOnFeatureCapability(ucSkyStemARTGrid.Grid.CreateTableView());
        ucSkyStemARTGrid.DataSource = GetGridData();
        ucSkyStemARTGrid.BindGrid();
    }

    private List<QualityScoreReportInfo> GetGridData()
    {
        List<QualityScoreReportInfo> oQualityScoreReportInfoCollection = null;
        oQualityScoreReportInfoCollection = (List<QualityScoreReportInfo>)HttpContext.Current.Session[SessionConstants.REPORT_DATA_QUALITYSCORE_ITEM_PRINT];
        return oQualityScoreReportInfoCollection;
    }

    protected void ucSkyStemARTGrid_GridItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {
        ReportHelper.ItemDataBoundQualityScoreReport(e);
    }

    protected void ucSkyStemARTGrid_PreRender(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            foreach (GridDataItem item in ucSkyStemARTGrid.Grid.MasterTableView.Items)
            {
                item.Expanded = true;
                if(item.HasChildItems)
                item.ChildItem.NestedTableViews[0].Items[0].Expanded = true;
            }
        }
    }

     protected void ucSkyStemARTGrid_GridDetailTableDataBind(object source, GridDetailTableDataBindEventArgs e)
    {
        try
        {
            switch (e.DetailTableView.Name)
            {
                case "QualityScoreDetails":
                    GridDataItem oGridItem = e.DetailTableView.ParentItem;
                    int? GLDataID = Convert.ToInt32(oGridItem.GetDataKeyValue("GLDataID"));
                    List<QualityScoreReportInfo> objQualityScoreData = ((List<QualityScoreReportInfo>)Session[SessionConstants.REPORT_DATA_QUALITYSCORE_ITEM]);
                    List<QualityScoreReportInfo> filteredQualityScoreData = new List<QualityScoreReportInfo>();
                    foreach (QualityScoreReportInfo objQS in objQualityScoreData)
                    {
                        if (objQS.GLDataID == GLDataID)
                        {
                            filteredQualityScoreData.Add(objQS);
                        }
                    }
                    e.DetailTableView.DataSource = filteredQualityScoreData;
                    break;
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
}
