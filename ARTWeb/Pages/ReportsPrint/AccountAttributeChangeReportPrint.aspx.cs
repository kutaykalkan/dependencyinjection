using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SkyStem.ART.Web.Utility;
using SkyStem.ART.Client.Model.Report;
using SkyStem.ART.Web.Data;

public partial class Pages_ReportsPrint_AccountAttributeChangeReportPrint : PageBaseReport
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ucSkyStemARTGrid.CompanyID = SessionHelper.CurrentCompanyID.Value;
        GridHelper.ShowHideColumnsBasedOnFeatureCapability(ucSkyStemARTGrid.Grid.CreateTableView());
        ucSkyStemARTGrid.DataSource = GetGridData();
        ucSkyStemARTGrid.BindGrid();
    }

    private List<AccountAttributeChangeReportInfo> GetGridData()
    {
        List<AccountAttributeChangeReportInfo> oAccountAttributeChangeReportInfoList = null;
        oAccountAttributeChangeReportInfoList = (List<AccountAttributeChangeReportInfo>)HttpContext.Current.Session[SessionConstants.REPORT_DATA_ACCOUNT_ATTRIBUTE_CHANGE];
        return oAccountAttributeChangeReportInfoList;
    }

    protected void ucSkyStemARTGrid_GridItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {
        ReportHelper.ItemDataBoundAccountAttributeChangeReport(e);
    }
}
