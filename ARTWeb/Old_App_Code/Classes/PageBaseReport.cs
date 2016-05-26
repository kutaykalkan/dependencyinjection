using System;
using System.Data;
using System.Configuration;
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
/// <summary>
/// Summary description for PageBaseReport
/// </summary>
public abstract class PageBaseReport : PageBaseRecPeriod 
{
    public PageBaseReport()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public override string GetMenuKey()
    {
        return "StandardReports";
    }

    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        ReportHelper.SetReportsBreadCrumb(this);
    }
}
