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
public partial class Pages_Reports_CertificationTrackingReport : PageBaseReport
{
    bool _isExportPDF;
    bool _isExportExcel;
    short? _reportID = 0;
    private int? _ReconciliationPeriodID= null ;
    Dictionary<string, string> _oCriteriaCollection = null;


    private DateTime? _CertificationStartDate;
    protected void Page_Load(object sender, EventArgs e)
    {
        
        Helper.RegisterPostBackToControls(this, rgReport);
        if (!Page.IsPostBack)
        {
            _isExportPDF = false;
            _isExportExcel = false;
        }
        Helper.SetPageTitle(this, 1115);
        ReportMstInfo oReportInfo = (ReportMstInfo)Session[SessionConstants.REPORT_INFO_OBJECT];

        if (!IsPostBack)
        {
            // Set default Sorting
            GridHelper.SetSortExpression(rgReport.MasterTableView, "FirstName");
        }

        if (oReportInfo != null && oReportInfo.ReportID.HasValue)
        {
            _reportID = oReportInfo.ReportID;
        }
        if (Session[SessionConstants.REPORT_CRITERIA] != null)
        {
            string reportType = Request.QueryString[QueryStringConstants.REPORT_TYPE];

            if (!string.IsNullOrEmpty(reportType) && Convert.ToInt16(reportType) == (short)WebEnums.ReportType.ArchivedReport)
            {
                ReportArchiveInfo oRptArchiveInfo = (ReportArchiveInfo)Session[SessionConstants.REPORT_ARCHIVED_DATA];
                List<CertificationTrackingReportInfo> oCertificationTrackingReportInfoCollection = ReportHelper.GetBinaryDeSerializedReportData(oRptArchiveInfo.ReportData) as List<CertificationTrackingReportInfo>;
                if (oCertificationTrackingReportInfoCollection != null)
                {
                    Session[SessionConstants.REPORT_DATA_CERTIFICATION_TRACKING] = oCertificationTrackingReportInfoCollection;
                }
            }
            else
            {
                _oCriteriaCollection = (Dictionary<string, string>)Session[SessionConstants.REPORT_CRITERIA];
                _ReconciliationPeriodID = Convert.ToInt32(_oCriteriaCollection[ReportCriteriaKeyName.RPTCRITERIAKEYNAME_RECPERIOD]);
                //if (Request.QueryString[QueryStringConstants.REPORT_ID] != null)
                //    _reportID = Convert.ToInt16(Request.QueryString[QueryStringConstants.REPORT_ID]);
                //List<CompanyCapabilityInfo> oCompanyCapabilityInfoCollection = SessionHelper.GetCompanyCapabilityCollectionForCurrentRecPeriod();
                //SetCapabilityInfo(oCompanyCapabilityInfoCollection);
                SetCertificationStartDate();
                if (!IsPostBack)
                {
                    SessionHelper.ClearSession(SessionConstants.REPORT_DATA_CERTIFICATION_TRACKING);
                    GridHelper.ShowHideColumnsBasedOnFeatureCapability(rgReport.MasterTableView);
                    RunReport();
                }
            }
        }
    }
    protected void rgReport_NeedDataSourceEventHandler(object sender, GridNeedDataSourceEventArgs e)
    {
        List<CertificationTrackingReportInfo> oCertificationTrackingReportInfoCollection = null;
        try
        {
            oCertificationTrackingReportInfoCollection = (List<CertificationTrackingReportInfo>)HttpContext.Current.Session[SessionConstants.REPORT_DATA_CERTIFICATION_TRACKING];
            if (oCertificationTrackingReportInfoCollection == null || oCertificationTrackingReportInfoCollection.Count == 0)
            {
                ReportSearchCriteria oReportSearchCriteria = this.GetNormalSearchCriteria();
                //DataTable dtEntity = ReportHelper.GetEntitySearchCriteria(_oCriteriaCollection);
                DataTable dtUser = ReportHelper.GetUserSearchCriteria(_oCriteriaCollection);
                DataTable dtRole = ReportHelper.GetRoleSearchCriteria(_oCriteriaCollection);

                //TODO: when no user is selected then assume that all r selected
                ReportHelper.SendUserSearchCriteriaWhenNoUserSelected(_reportID.Value, ref dtUser, ref dtRole);
                
                IReport oReportClient = RemotingHelper.GetReportObject();
                oCertificationTrackingReportInfoCollection = oReportClient.GetReportCertificationTrackingReport(oReportSearchCriteria, dtUser, dtRole, Helper.GetAppUserInfo());
                oCertificationTrackingReportInfoCollection = LanguageHelper.TranslateLabelsCertificationTrackingReport(oCertificationTrackingReportInfoCollection);
                Session[SessionConstants.REPORT_DATA_CERTIFICATION_TRACKING] = oCertificationTrackingReportInfoCollection;
            }

            GridHelper.ShowHideColumnsBasedOnFeatureCapability(rgReport.CreateTableView());

            rgReport.DataSource = oCertificationTrackingReportInfoCollection;
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
    protected void Page_PreRender(object sender, EventArgs e)
    {

        int? ReportRoleMandatoryReportID = null;
        ReportRoleMandatoryReportID = Convert.ToInt32(Request.QueryString[QueryStringConstants.MANDATORY_REPORT_ID]);
        if (ReportRoleMandatoryReportID != null && ReportRoleMandatoryReportID > 0)
            Helper.SetBreadcrumbs(this, 1072, 1016, rgReport.EntityNameLabelID);
    }

    private void RunReport()
    {
        ReportHelper.SetReportRadGridProperty(rgReport);
        rgReport.EntityNameLabelID = 1115;
    }

    protected void rgReport_GridItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {
        ReportHelper.ItemDataBoundCertificationTrackingReport(e, _CertificationStartDate);
      
    }


    private ReportSearchCriteria GetNormalSearchCriteria()
    {
        ReportSearchCriteria oReportSearchCriteria = new ReportSearchCriteria();
        foreach (KeyValuePair<string, string> keyValuePair in _oCriteriaCollection)
        {
            switch (keyValuePair.Key)
            {
                case ReportCriteriaKeyName.RPTCRITERIAKEYNAME_RECPERIOD:
                    oReportSearchCriteria.ReconciliationPeriodID = Convert.ToInt32(keyValuePair.Value);
                    break;
            }
        }

        return oReportSearchCriteria;
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




    public override string GetMenuKey()
    {
        return "";
    }

    protected void rgReport_ItemCreated(object sender, GridItemEventArgs e)
    {
        GridHelper.SetStylesForExportGrid(e, _isExportPDF, _isExportExcel);
    }
    protected void rgReport_OnItemCommand(object sender, GridCommandEventArgs e)
    {
        if (e.CommandName == TelerikConstants .GridExportToPDFCommandName)
        {
            _isExportPDF = true;
            //rgCompanyList.MasterTableView.Columns.FindByUniqueName("IconColumn").Visible = false;
            GridHelper.ExportGridToPDF(rgReport, 1115);

        }
        if (e.CommandName == TelerikConstants .GridExportToExcelCommandName)
        {
            _isExportExcel = true;
            //rgCompanyList.MasterTableView.Columns.FindByUniqueName("IconColumn").Visible = false;
            GridHelper.ExportGridToExcel(rgReport, 1115);

        }

    }

}//end of class
