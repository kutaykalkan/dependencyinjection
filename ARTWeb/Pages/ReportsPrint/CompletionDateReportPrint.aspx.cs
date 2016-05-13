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
using SkyStem.ART.Client.IServices;
using SkyStem.ART.Client.Model;
using System.Collections.Generic;
using SkyStem.ART.Client.Data;
using SkyStem.Library.Controls.WebControls;
using Telerik.Web.UI;
using SkyStem.ART.Client.Model.Report;
using SkyStem.ART.Client.Exception;
using SkyStem.Library.Controls.TelerikWebControls;

public partial class Pages_ReportsPrint_CompletionDateReportPrint : PageBaseReport
{
    protected void Page_Load(object sender, EventArgs e)
    {

        ucSkyStemARTGrid.CompanyID = SessionHelper.CurrentCompanyID.Value;
        GridHelper.ShowHideColumnsBasedOnFeatureCapability(ucSkyStemARTGrid.Grid.CreateTableView());
        ucSkyStemARTGrid.DataSource = GetGridData();
        ucSkyStemARTGrid.BindGrid();
    }


    private List<CompletionDateReportInfo> GetGridData()
    {
        List<CompletionDateReportInfo> oCompletionDateReportInfoCollection = null;
        oCompletionDateReportInfoCollection = (List<CompletionDateReportInfo>)HttpContext.Current.Session[SessionConstants.REPORT_DATA_COMPLETION_DATE];
        return oCompletionDateReportInfoCollection;
    }

    protected void ucSkyStemARTGrid_GridItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {

        ReportHelper.ItemDataBoundCompletionDateReport(e);
    }
}
