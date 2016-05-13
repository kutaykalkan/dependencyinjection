using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SkyStem.ART.Web.Data;
using SkyStem.ART.Client.Model;
using SkyStem.ART.Web.Utility;

public partial class MasterPages_ReportViewerPrint : System.Web.UI.MasterPage
{
    bool _ShowComments = false;
    string _Comments = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        _ShowComments = Convert.ToBoolean(Request.QueryString[QueryStringConstants.SHOW_COMMENTS]);

        if (!string.IsNullOrEmpty(Request.QueryString[QueryStringConstants.COMMENTS]))
        {
            _Comments = Request.QueryString[QueryStringConstants.COMMENTS];
        }

        if (Session[SessionConstants.REPORT_INFO_OBJECT] != null)
        {
            ReportMstInfo oReportInfo = (ReportMstInfo)Session[SessionConstants.REPORT_INFO_OBJECT];
            this.PopulateReportHeader(oReportInfo);
        }

        ucReportInfo.RecPeriodEndDateText = Request.QueryString[QueryStringConstants.REC_PERIODC_END_DATE];
    }

    private void PopulateReportHeader(ReportMstInfo oReportInfo)
    {
        ucReportInfo.ReportDateTime = oReportInfo.DateAdded;

        if (_ShowComments)
        {
            trComments.Visible = true;
            // Either pick from Session [Print / PDF]
            // or from QueryString [Email]
            if (string.IsNullOrEmpty(_Comments))
            {
                lblAdditionalCommentsValue.Text = oReportInfo.Comments;
            }
            else
            {
                lblAdditionalCommentsValue.Text = _Comments;
            }
        }
        else
        {
            trComments.Visible = false;
        }
    }
    

}
