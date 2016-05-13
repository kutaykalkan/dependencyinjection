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
using SkyStem.Library.Controls.TelerikWebControls;

public partial class Pages_Reports_DelinquentAccountByUserReport : PageBaseReport
{
    short? _reportID = 0;
    private int? _ReconciliationPeriodID = null;
    Dictionary<string, string> _oCriteriaCollection = null;



    protected void Page_Load(object sender, EventArgs e)
    {
        Helper.RegisterPostBackToControls(this, ucSkyStemARTGrid.Grid);

        Helper.SetPageTitle(this, 1887);
        ucSkyStemARTGrid.BasePageTitle = 1887;
        ReportMstInfo oReportInfo = (ReportMstInfo)Session[SessionConstants.REPORT_INFO_OBJECT];

        if (!IsPostBack)
        {
            // Set default Sorting
            GridHelper.SetSortExpression(ucSkyStemARTGrid.Grid.MasterTableView, "AccountName");
            //Grouping
            GridGroupByExpression expression = new GridGroupByExpression();
            GridGroupByFieldList groupByFieldsList = new GridGroupByFieldList();
            GridGroupByField gridGroupByField;

            gridGroupByField = new GridGroupByField();
            gridGroupByField.FieldName = "FirstName";
            //gridGroupByField.HeaderText = "User: ";
            gridGroupByField.HeaderText =LanguageUtil.GetValue(1533);
            
            //gridGroupByField.HeaderValueSeparator = " for current group: ";
            gridGroupByField.FormatString = "<strong>{0}</strong>";
            groupByFieldsList.Add(gridGroupByField);

            gridGroupByField = new GridGroupByField();
            gridGroupByField.FieldName = "LastName";
            gridGroupByField.HeaderText = LanguageUtil.GetValue(1268);
            gridGroupByField.FormatString = "<strong>{0}</strong>";
            groupByFieldsList.Add(gridGroupByField);

            gridGroupByField = new GridGroupByField();
            gridGroupByField.FieldName = "Role";
            gridGroupByField.HeaderText = LanguageUtil.GetValue(1278);
            gridGroupByField.FormatString = "<strong>{0}</strong>";
            groupByFieldsList.Add(gridGroupByField);

            gridGroupByField = new GridGroupByField();
            gridGroupByField.FieldName = "CountDelinquentAccount";
           // gridGroupByField.HeaderText = "Number of delinquent Accounts";
            gridGroupByField.HeaderText = LanguageUtil.GetValue(2661);             
            gridGroupByField.FormatString = "<strong>{0}</strong>";
            gridGroupByField.Aggregate = GridAggregateFunction.Sum;
            groupByFieldsList.Add(gridGroupByField);

            expression.SelectFields.AddRange(groupByFieldsList);

            gridGroupByField = new GridGroupByField();
            gridGroupByField.FieldName = "UserID";
            expression.GroupByFields.Add(gridGroupByField);

            gridGroupByField = new GridGroupByField();
            gridGroupByField.FieldName = "RoleID";
            expression.GroupByFields.Add(gridGroupByField);

            ucSkyStemARTGrid.GridGroupByExpression = expression;
        }

        if (oReportInfo != null && oReportInfo.ReportID.HasValue)
        {
            _reportID = oReportInfo.ReportID;
        }
        //if (!Page.IsPostBack)
        //{
        string reportType = Request.QueryString[QueryStringConstants.REPORT_TYPE];

        if (!string.IsNullOrEmpty(reportType) && Convert.ToInt16(reportType) == (short)WebEnums.ReportType.ArchivedReport)
        {
            SetGridProperties();
            ReportArchiveInfo oRptArchiveInfo = (ReportArchiveInfo)Session[SessionConstants.REPORT_ARCHIVED_DATA];
            List<DelinquentAccountByUserReportInfo> oDelinquentAccountByUserReportInfoCollection = ReportHelper.GetBinaryDeSerializedReportData(oRptArchiveInfo.ReportData) as List<DelinquentAccountByUserReportInfo>;
            if (oDelinquentAccountByUserReportInfoCollection != null)
            {
                Session[SessionConstants.REPORT_DATA_DELINQUENT_ACCOUNT] = oDelinquentAccountByUserReportInfoCollection;
                if (!IsPostBack)
                {

                    GridHelper.ShowHideColumnsBasedOnFeatureCapability(ucSkyStemARTGrid.Grid.CreateTableView());
                    ucSkyStemARTGrid.DataSource = oDelinquentAccountByUserReportInfoCollection;
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
                    SessionHelper.ClearSession(SessionConstants.REPORT_DATA_DELINQUENT_ACCOUNT);
                    GetGridData();
                    ucSkyStemARTGrid.BindGrid();
                }
            }
        }

        //}
    }


    private List<DelinquentAccountByUserReportInfo> GetGridData()
    {
        List<DelinquentAccountByUserReportInfo> oDelinquentAccountByUserReportInfoCollection = null;
        try
        {
            oDelinquentAccountByUserReportInfoCollection = (List<DelinquentAccountByUserReportInfo>)HttpContext.Current.Session[SessionConstants.REPORT_DATA_DELINQUENT_ACCOUNT];

            if (oDelinquentAccountByUserReportInfoCollection == null || oDelinquentAccountByUserReportInfoCollection.Count == 0)
            {
                ReportSearchCriteria oReportSearchCriteria = this.GetNormalSearchCriteria();
                //DataTable dtEntity = ReportHelper.GetEntitySearchCriteria(_oCriteriaCollection);
                DataTable dtUser = ReportHelper.GetUserSearchCriteria(_oCriteriaCollection);
                DataTable dtRole = ReportHelper.GetRoleSearchCriteria(_oCriteriaCollection);

                //TODO: when no user is selected then assume that all r selected
                ReportHelper.SendUserSearchCriteriaWhenNoUserSelected(_reportID.Value, ref dtUser, ref dtRole);

                IReport oReportClient = RemotingHelper.GetReportObject();
                oDelinquentAccountByUserReportInfoCollection = oReportClient.GetReportDelinquentAccountByUserReport(oReportSearchCriteria, dtUser, dtRole, Helper.GetAppUserInfo());
                oDelinquentAccountByUserReportInfoCollection = LanguageHelper.TranslateLabelsDelinquentAccountByUserReport(oDelinquentAccountByUserReportInfoCollection);
                Session[SessionConstants.REPORT_DATA_DELINQUENT_ACCOUNT] = oDelinquentAccountByUserReportInfoCollection;

                GridHelper.ShowHideColumnsBasedOnFeatureCapability(ucSkyStemARTGrid.Grid.CreateTableView());

                ucSkyStemARTGrid.DataSource = oDelinquentAccountByUserReportInfoCollection;
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

        return oDelinquentAccountByUserReportInfoCollection;
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
        ucSkyStemARTGrid.ShowStatusImageColumn = false;
        //ucSkyStemARTGrid.ShowFSCaptionAndAccountType();
        ucSkyStemARTGrid.ShowSelectCheckBoxColum = false;
        ucSkyStemARTGrid.CompanyID = SessionHelper.CurrentCompanyID.Value;
        ucSkyStemARTGrid.Grid.EntityNameLabelID = 1887;
    }

    protected void ucSkyStemARTGrid_GridItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {

        ReportHelper.ItemDataBoundDelinquentAccountByUserReport(e);


    }


    private ReportSearchCriteria GetNormalSearchCriteria()
    {
        ReportSearchCriteria oReportSearchCriteria = new ReportSearchCriteria();

        //From Session keyValuePair parameters
        foreach (KeyValuePair<string, string> keyValuePair in _oCriteriaCollection)
        {
            switch (keyValuePair.Key)
            {
                case ReportCriteriaKeyName.RPTCRITERIAKEYNAME_RECPERIOD:
                    oReportSearchCriteria.ReconciliationPeriodID = Convert.ToInt32(keyValuePair.Value);
                    break;
            }
        }

        //Other fields required for sp
        {
            ReportHelper.SetParameterValueForRequesterUserAndLanguage(oReportSearchCriteria);

            oReportSearchCriteria.CompanyID = SessionHelper.CurrentCompanyID;
            //oReportSearchCriteria.IsDualReviewEnabled = this._IsDualReviewEnabled;
            //oReportSearchCriteria.IsKeyAccountEnabled = this._IsKeyAccountEnabled;
            //oReportSearchCriteria.IsRiskRatingEnabled = this._IsRiskRatingEnabled;
            //oReportSearchCriteria.IsZeroBalanceAccountEnabled = this._IsZeroBalanceEnabled;

            //oReportSearchCriteria.ExcludeNetAccount = true;
            //oReportSearchCriteria.IsRequesterUserIDToBeConsideredForPermission = true;
        }

        return oReportSearchCriteria;
    }

    public override string GetMenuKey()
    {
        return "";
    }
}
