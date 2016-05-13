using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SkyStem.ART.Web.Classes;
using SkyStem.ART.Client.Model.Report;
using SkyStem.ART.Web.Data;
using Telerik.Web.UI;
using SkyStem.ART.Web.Utility;
using SkyStem.Library.Controls.WebControls;

public partial class Pages_ReportsPrint_ExceptionStatusReportPrint : PopupPageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        List<ExceptionStatusReportInfo> oExceptionStatusReportInfoCollection = null;
        oExceptionStatusReportInfoCollection = (List<ExceptionStatusReportInfo>)HttpContext.Current.Session[SessionConstants.REPORT_DATA_EXCEPTION_STATUS];

        ucSkyStemARTGrid.CompanyID = SessionHelper.CurrentCompanyID.Value;
        ucSkyStemARTGrid.DataSource = oExceptionStatusReportInfoCollection;
        ucSkyStemARTGrid.BindGrid();
    }

    protected void ucSkyStemARTGrid_GridItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {
       ReportHelper.ItemDataBoundExceptionStatus(e);
    }


}
