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
using SkyStem.Language.LanguageUtility;
using SkyStem.ART.Client.Exception;
using SkyStem.Library.Controls.TelerikWebControls.Data;

//TODO: set value of _ReconciliationPeriodID, and SetCertificationStartDate()
public partial class Pages_ReportsPrint_CertificationTrackingReportPrint : PageBaseReport
{
    private DateTime? _CertificationStartDate;
    private int? _ReconciliationPeriodID;
    Dictionary<string, string> _oCriteriaCollection = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        _oCriteriaCollection = (Dictionary<string, string>)Session[SessionConstants.REPORT_CRITERIA];
        _ReconciliationPeriodID = Convert.ToInt32(_oCriteriaCollection[ReportCriteriaKeyName.RPTCRITERIAKEYNAME_RECPERIOD]);
        SetCertificationStartDate();
        
        List<CertificationTrackingReportInfo> oCertificationTrackingReportInfoCollection = null;
        oCertificationTrackingReportInfoCollection = (List<CertificationTrackingReportInfo>)HttpContext.Current.Session[SessionConstants.REPORT_DATA_CERTIFICATION_TRACKING];
        GridHelper.ShowHideColumnsBasedOnFeatureCapability(rgReport.MasterTableView);
        rgReport.DataSource = oCertificationTrackingReportInfoCollection;
        
    }


    private void SetCertificationStartDate()
    {
        List<ReconciliationPeriodInfo> oReconciliationPeriodInfoCollection = CacheHelper.GetAllReconciliationPeriods(null);
        ReconciliationPeriodInfo oReconciliationPeriodInfo = oReconciliationPeriodInfoCollection.Where(recItem => recItem.ReconciliationPeriodID == this._ReconciliationPeriodID).FirstOrDefault();
        if (oReconciliationPeriodInfo != null)
        {
            _CertificationStartDate = oReconciliationPeriodInfo.CertificationStartDate;
        }
    }

    protected void rgReport_GridItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {
        ReportHelper.ItemDataBoundCertificationTrackingReport(e, _CertificationStartDate.Value );
    }





   

}//end of class
