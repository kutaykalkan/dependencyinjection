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

public partial class Pages_Reports_ReconciliationStatusCountReport : PageBaseReport
{
    bool _isExportPDF;
    bool _isExportExcel;
    short? _reportID = 0;
    private int? _ReconciliationPeriodID = null;
    Dictionary<string, string> _oCriteriaCollection = null;

   
    protected void Page_Load(object sender, EventArgs e)
    {

        Helper.RegisterPostBackToControls(this, rgReport);
        if (!Page.IsPostBack)
        {
            _isExportPDF = false;
            _isExportExcel = false;
        }
        Helper.SetPageTitle(this, 1117);
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
                List<ReconciliationStatusCountReportInfo> oReconciliationStatusCountReportInfoCollection = ReportHelper.GetBinaryDeSerializedReportData(oRptArchiveInfo.ReportData) as List<ReconciliationStatusCountReportInfo>;
                if (oReconciliationStatusCountReportInfoCollection != null)
                {
                    Session[SessionConstants.REPORT_DATA_REC_STATUS_COUNT] = oReconciliationStatusCountReportInfoCollection;
                }
            }
            else
            {
                _oCriteriaCollection = (Dictionary<string, string>)Session[SessionConstants.REPORT_CRITERIA];
                _ReconciliationPeriodID = Convert.ToInt32(_oCriteriaCollection[ReportCriteriaKeyName.RPTCRITERIAKEYNAME_RECPERIOD]);
                //if (Request.QueryString[QueryStringConstants.REPORT_ID] != null)
                //    _reportID = Convert.ToInt16(Request.QueryString[QueryStringConstants.REPORT_ID]);
                if (!IsPostBack)
                {
                    SessionHelper.ClearSession(SessionConstants.REPORT_DATA_REC_STATUS_COUNT);
                    RunReport();
                }
            }
        }
    }
    protected void rgReport_NeedDataSourceEventHandler(object sender, GridNeedDataSourceEventArgs e)
    {
        List<ReconciliationStatusCountReportInfo> oReconciliationStatusCountReportInfoCollection = null;
        try
        {
            oReconciliationStatusCountReportInfoCollection = (List<ReconciliationStatusCountReportInfo>)HttpContext.Current.Session[SessionConstants.REPORT_DATA_REC_STATUS_COUNT];
            if (oReconciliationStatusCountReportInfoCollection == null || oReconciliationStatusCountReportInfoCollection.Count == 0)
            {
                ReportSearchCriteria oReportSearchCriteria = this.GetNormalSearchCriteria();
                //DataTable dtEntity = ReportHelper.GetEntitySearchCriteria(_oCriteriaCollection);
                DataTable dtUser =null;
                DataTable dtRole = null;
                if (_oCriteriaCollection != null)
                {
                    dtUser = ReportHelper.GetUserSearchCriteria(_oCriteriaCollection);
                    dtRole = ReportHelper.GetRoleSearchCriteria(_oCriteriaCollection);

                }
                //TODO: when no user is selected then assume that all r selected
                ReportHelper.SendUserSearchCriteriaWhenNoUserSelected(_reportID.Value, ref dtUser, ref dtRole);


                IReport oReportClient = RemotingHelper.GetReportObject();
                oReconciliationStatusCountReportInfoCollection = oReportClient.GetReportReconciliationStatusCountReport(oReportSearchCriteria, dtUser, dtRole, Helper.GetAppUserInfo());
                oReconciliationStatusCountReportInfoCollection = LanguageHelper.TranslateLabelsReconciliationStatusCountReport(oReconciliationStatusCountReportInfoCollection);
                Session[SessionConstants.REPORT_DATA_REC_STATUS_COUNT] = oReconciliationStatusCountReportInfoCollection;
            }

            GridHelper.ShowHideColumnsBasedOnFeatureCapability(rgReport.CreateTableView());

            rgReport.DataSource = oReconciliationStatusCountReportInfoCollection;
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

    private void RunReport()
    {
        ReportHelper.SetReportRadGridProperty(rgReport);
        rgReport.EntityNameLabelID = 1117;
    }
    protected void Page_PreRender(object sender, EventArgs e)
    {

        int? ReportRoleMandatoryReportID = null;
        ReportRoleMandatoryReportID = Convert.ToInt32(Request.QueryString[QueryStringConstants.MANDATORY_REPORT_ID]);
        if (ReportRoleMandatoryReportID != null && ReportRoleMandatoryReportID > 0)
            Helper.SetBreadcrumbs(this, 1072, 1016, rgReport.EntityNameLabelID);
    }

    protected void rgReport_GridItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {


        ReportHelper.ItemDataBoundReconciliationStatusCountReport(e);

    }


    private ReportSearchCriteria GetNormalSearchCriteria()
    {
        ReportSearchCriteria oReportSearchCriteria = new ReportSearchCriteria();
        if (_oCriteriaCollection != null)
        {
            foreach (KeyValuePair<string, string> keyValuePair in _oCriteriaCollection)
            {
                switch (keyValuePair.Key)
                {
                    case ReportCriteriaKeyName.RPTCRITERIAKEYNAME_RECPERIOD:
                        oReportSearchCriteria.ReconciliationPeriodID = Convert.ToInt32(keyValuePair.Value);
                        break;
                }
            }
        }
        if (SessionHelper.CurrentUserID.HasValue)
            oReportSearchCriteria.RequesterUserID = SessionHelper.CurrentUserID.Value;
        if (SessionHelper.CurrentRoleID.HasValue)
            oReportSearchCriteria.RequesterRoleID = SessionHelper.CurrentRoleID.Value;


        return oReportSearchCriteria;
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
        if (e.CommandName == TelerikConstants.GridExportToPDFCommandName)
        {
            _isExportPDF = true;
            //rgCompanyList.MasterTableView.Columns.FindByUniqueName("IconColumn").Visible = false;
            GridHelper.ExportGridToPDF(rgReport, 1117);

        }
        if (e.CommandName == TelerikConstants.GridExportToExcelCommandName)
        {
            _isExportExcel = true;
            //rgCompanyList.MasterTableView.Columns.FindByUniqueName("IconColumn").Visible = false;
            GridHelper.ExportGridToExcel(rgReport, 1117);

        }

    }

    protected void rgReconciliationStatusByFSCaption_PreRender(object sender, EventArgs e)
    {
        WebEnums.FeatureCapabilityMode eMode = Helper.GetFeatureCapabilityMode(WebEnums.Feature.DualLevelReview, ARTEnums.Capability.DualLevelReview, SessionHelper.CurrentReconciliationPeriodID);
        if (eMode == WebEnums.FeatureCapabilityMode.Hidden || eMode == WebEnums.FeatureCapabilityMode.Disable)
        {
            foreach (GridColumn col in rgReport.MasterTableView.Columns)
            {
                if (col.UniqueName == "ApprovedData" || col.UniqueName == "PendingModificationReviewerData"
                    || col.UniqueName == "PendingApprovalData" || col.UniqueName == "PendingApproval"
                    || col.UniqueName == "PendingModificationReviewer" || col.UniqueName == "Approved")
                {
                    col.Visible = false;
                }
            }
        }

    }

}//end of class
