using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SkyStem.ART.Web.Classes;
using SkyStem.ART.Web.Utility;
using SkyStem.ART.Web.Data;
using SkyStem.ART.Client.Model;
using SkyStem.ART.Client.Model.Report;
using SkyStem.ART.Client.Exception;
using SkyStem.ART.Client.IServices;
using System.Data;
using Telerik.Web.UI;
using SkyStem.Library.Controls.WebControls;
using SkyStem.Library.Controls.TelerikWebControls;

public partial class Pages_Reports_ExceptionStatusReport : PageBaseReport
{
    private int? _ReconciliationPeriodID;
    Dictionary<string, string> _oCriteriaCollection = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        Helper.RegisterPostBackToControls(this, ucSkyStemARTGrid.Grid);

        Helper.SetPageTitle(this, 1111);
        ucSkyStemARTGrid.BasePageTitle = 1111;
        //if (!Page.IsPostBack)
        //{
            string reportType = Request.QueryString[QueryStringConstants.REPORT_TYPE];

            if (!IsPostBack)
            {
                // Set default Sorting
                GridHelper.SetSortExpression(ucSkyStemARTGrid.Grid.MasterTableView, "AccountName");
            }

            if (!string.IsNullOrEmpty(reportType) && Convert.ToInt16(reportType) == (short)WebEnums.ReportType.ArchivedReport)
            {
                SetGridProperties();
                ReportArchiveInfo oRptArchiveInfo = (ReportArchiveInfo)Session[SessionConstants.REPORT_ARCHIVED_DATA];
                List<ExceptionStatusReportInfo> oExceptionStatusReportInfoCollection = ReportHelper.GetBinaryDeSerializedReportData(oRptArchiveInfo.ReportData) as List<ExceptionStatusReportInfo>;
                if (oExceptionStatusReportInfoCollection != null)
                {
                    Session[SessionConstants.REPORT_DATA_EXCEPTION_STATUS] = oExceptionStatusReportInfoCollection;
                    if (!IsPostBack)
                    {

                        GridHelper.ShowHideColumnsBasedOnFeatureCapability(ucSkyStemARTGrid.Grid.CreateTableView());
                        ucSkyStemARTGrid.DataSource = oExceptionStatusReportInfoCollection;
                        ucSkyStemARTGrid.BindGrid();
                    }
                }
            }
            else
            {
                if (Session[SessionConstants.REPORT_CRITERIA] != null)
                {
                    _oCriteriaCollection = (Dictionary<string, string>)Session[SessionConstants.REPORT_CRITERIA];
                    _ReconciliationPeriodID = Convert.ToInt32(_oCriteriaCollection[ReportCriteriaKeyName.RPTCRITERIAKEYNAME_RECPERIOD]);
                    SetGridProperties();

                    if (!IsPostBack)
                    {
                        SessionHelper.ClearSession(SessionConstants.REPORT_DATA_EXCEPTION_STATUS);
                        GetGridData();
                        ucSkyStemARTGrid.BindGrid();
                    }
                }
            }
        //}
    }



    private List<ExceptionStatusReportInfo> GetGridData()
    {
        List<ExceptionStatusReportInfo> oExceptionStatusReportInfoCollection = null;
        try
        {
            oExceptionStatusReportInfoCollection = (List<ExceptionStatusReportInfo>)HttpContext.Current.Session[SessionConstants.REPORT_DATA_EXCEPTION_STATUS];

            if (oExceptionStatusReportInfoCollection == null || oExceptionStatusReportInfoCollection.Count == 0)
            {
                ReportSearchCriteria oReportSearchCriteria = this.GetNormalSearchCriteria();
                DataTable dtEntity = ReportHelper.GetEntitySearchCriteria(_oCriteriaCollection);
                DataTable dtUser = ReportHelper.GetUserSearchCriteria(_oCriteriaCollection);
                DataTable dtRole = ReportHelper.GetRoleSearchCriteria(_oCriteriaCollection);

                IReport oReportClient = RemotingHelper.GetReportObject();
                oExceptionStatusReportInfoCollection = oReportClient.GetExceptionStatusReport(oReportSearchCriteria, dtEntity, dtUser, dtRole, Helper.GetAppUserInfo());
                LanguageHelper.TranslateLabelsExceptionStatusReport(oExceptionStatusReportInfoCollection);
                Session[SessionConstants.REPORT_DATA_EXCEPTION_STATUS] = oExceptionStatusReportInfoCollection;

                GridHelper.ShowHideColumnsBasedOnFeatureCapability(ucSkyStemARTGrid.Grid.CreateTableView());

                ucSkyStemARTGrid.DataSource = oExceptionStatusReportInfoCollection;
                // Sort the Data
                //GridHelper.SortDataSource(ucSkyStemARTGrid.Grid.MasterTableView);
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

        return oExceptionStatusReportInfoCollection;
    }
    protected void Page_PreRender(object sender, EventArgs e)
    {

        int? ReportRoleMandatoryReportID = null;
        ReportRoleMandatoryReportID = Convert.ToInt32(Request.QueryString[QueryStringConstants.MANDATORY_REPORT_ID]);
        if (ReportRoleMandatoryReportID != null && ReportRoleMandatoryReportID > 0)
            Helper.SetBreadcrumbs(this, 1072, 1016, ucSkyStemARTGrid.BasePageTitle);
    }

    protected object ucSkyStemARTGrid_NeedDataSourceEventHandler(int count)
    {
        return GetGridData();
    }


    private void SetGridProperties()
    {
        ucSkyStemARTGrid.Grid.AllowCustomPaging = false;
        ucSkyStemARTGrid.Grid.AllowPaging = true;
        ucSkyStemARTGrid.Grid.PagerStyle.AlwaysVisible = true;
        ucSkyStemARTGrid.ShowSelectCheckBoxColum = false;
        ucSkyStemARTGrid.CompanyID = SessionHelper.CurrentCompanyID.Value;
    }

    protected void ucSkyStemARTGrid_GridItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {
        ReportHelper.ItemDataBoundExceptionStatus(e);
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

                case ReportCriteriaKeyName.RPTCRITERIAKEYNAME_TOACCOUNT:
                    oReportSearchCriteria.ToAccountNumber = keyValuePair.Value;
                    break;

                case ReportCriteriaKeyName.RPTCRITERIAKEYNAME_ISMATERIALACCOUNT:
                    oReportSearchCriteria.IsMaterialAccount = ReportHelper.GetBoolValueFromKeyValue(keyValuePair.Value);
                    break;

                case ReportCriteriaKeyName.RPTCRITERIAKEYNAME_RISKRATING:
                    oReportSearchCriteria.RiskRatingIDs = keyValuePair.Value;
                    break;

                case ReportCriteriaKeyName.RPTCRITERIAKEYNAME_ISKEYACCOUNT:
                    oReportSearchCriteria.IsKeyccount = ReportHelper.GetBoolValueFromKeyValue(keyValuePair.Value);
                    break;

                case ReportCriteriaKeyName.RPTCRITERIAKEYNAME_TYPEOFEXCEPTION:
                    oReportSearchCriteria.ExceptionTypeIDs = keyValuePair.Value;
                    break;
            }
        }

        //Other fields required for sp
        ReportHelper.SetParameterValueForRequesterUserAndLanguage(oReportSearchCriteria);

        oReportSearchCriteria.CompanyID = SessionHelper.CurrentCompanyID;
        oReportSearchCriteria.ExcludeNetAccount = true;
        oReportSearchCriteria.IsRequesterUserIDToBeConsideredForPermission = true;

        return oReportSearchCriteria;
    }



}
