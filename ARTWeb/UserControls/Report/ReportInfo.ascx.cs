using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SkyStem.ART.Web.Classes.UserControl;
using SkyStem.ART.Web.Data;
using SkyStem.ART.Client.Model;
using SkyStem.ART.Web.Utility;

public partial class UserControls_Report_ReportInfo : UserControlReportInfoBase
{
    /// <summary>
    /// Either use this property or use RecPeriodEndDateText; Do NOT use both
    /// </summary>
    public DateTime? RecPeriodEndDate
    {
        set
        {
            this.lblRptPeriodValue.Text = Helper.GetDisplayDate(value);
        }
    }

    public DateTime? ReportDateTime
    {
        set
        {
            lblDateTimeValue.Text = Helper.GetDisplayDateTime(value);
        }
    }

    /// <summary>
    /// Either use this property or use RecPeriodEndDate; Do NOT use both
    /// </summary>
    public string RecPeriodEndDateText
    {
        set
        {
            lblRptPeriodValue.Text = value;
        }
        get
        {
            return lblRptPeriodValue.Text;
        }
    }

    public Label lblRecPeriodEndDate
    {
        get
        {
            return lblRptPeriodValue;
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        ReportMstInfo oReportInfo = (ReportMstInfo)Session[SessionConstants.REPORT_INFO_OBJECT];
        if (!IsPostBack)
        {
            if (Session[SessionConstants.REPORT_INFO_OBJECT] != null)
            {
                this.lblPreparedByValue.Text = Helper.GetUserFullName();
                this.lblDateTimeValue.Text = Helper.GetDisplayDateTime(DateTime.Now);
            }

            this.lblCompanyName.Text = Helper.GetCompanyName();
            ReportHelper.ShowCompanyLogo(imgCompanyLogo, this.Page);
        }
        if (oReportInfo.ReportID == 2)
            this.lblReportDescription.Text = oReportInfo.Description;
        else
            this.lblReportDescription.LabelID = oReportInfo.DescriptionLabelID.Value;

        this.lblReportName.LabelID = oReportInfo.ReportLabelID.Value;


    }
}
