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
public partial class Pages_Reports_ReviewNoteReport : PageBaseReport
{
    private int? _ReconciliationPeriodID = null;
    Dictionary<string, string> _oCriteriaCollection = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        Helper.RegisterPostBackToControls(this, ucSkyStemARTGrid.Grid);
        Helper.SetPageTitle(this, 2635);
        ucSkyStemARTGrid.BasePageTitle = 2635;

        string reportType = Request.QueryString[QueryStringConstants.REPORT_TYPE];

        if (!IsPostBack)
        {
            // Set default Sorting
            GridHelper.SetSortExpression(ucSkyStemARTGrid.Grid.MasterTableView, "AccountName");

            //Grouping By Account#
            GridGroupByExpression expressionAccount = new GridGroupByExpression();

            GridGroupByFieldList groupByFieldsList = new GridGroupByFieldList();
            GridGroupByField gridGroupByField;

            gridGroupByField = new GridGroupByField();
            gridGroupByField.FieldName = "AccountNumber";
            gridGroupByField.HeaderText = SkyStem.Language.LanguageUtility.LanguageUtil.GetValue(1712);
            //gridGroupByField.HeaderValueSeparator = " for current group: ";
            gridGroupByField.FormatString = "<strong>{0}</strong>";
            groupByFieldsList.Add(gridGroupByField);

            expressionAccount.SelectFields.AddRange(groupByFieldsList);

            gridGroupByField = new GridGroupByField();
            gridGroupByField.FieldName = "AccountNumber";
            expressionAccount.GroupByFields.Add(gridGroupByField);

            //Grouping By Subject
            GridGroupByExpression expressionSubject = new GridGroupByExpression();

            GridGroupByFieldList groupByFieldsListSubject = new GridGroupByFieldList();

            gridGroupByField = new GridGroupByField();
            gridGroupByField.FieldName = "Subject";
            gridGroupByField.HeaderText = SkyStem.Language.LanguageUtility.LanguageUtil.GetValue(1778);
            //gridGroupByField.HeaderValueSeparator = " for current group: ";
            gridGroupByField.FormatString = "<strong>{0}</strong>";
            groupByFieldsListSubject.Add(gridGroupByField);
            expressionSubject.SelectFields.AddRange(groupByFieldsListSubject);

            gridGroupByField = new GridGroupByField();
            gridGroupByField.FieldName = "Subject";
            expressionSubject.GroupByFields.Add(gridGroupByField);

            GridGroupByExpressionCollection grdExpressionCollection = new GridGroupByExpressionCollection();
            grdExpressionCollection.Add(expressionAccount);
            grdExpressionCollection.Add(expressionSubject);
            ucSkyStemARTGrid.Grid.MasterTableView.GroupByExpressions.AddRange(grdExpressionCollection);
        }

        if (!string.IsNullOrEmpty(reportType) && Convert.ToInt16(reportType) == (short)WebEnums.ReportType.ArchivedReport)
        {
            _ReconciliationPeriodID = SessionHelper.CurrentReconciliationPeriodID;

            if (HttpContext.Current.Request.QueryString[QueryStringConstants.IS_REPORT] != null)
            {
                _ReconciliationPeriodID = ReportHelper.GetRecPeriodIDFromReportCriteria();
            }
            SetGridProperties();

            ReportArchiveInfo oRptArchiveInfo = (ReportArchiveInfo)Session[SessionConstants.REPORT_ARCHIVED_DATA];
            List<ReviewNotesReportInfo> oReviewNotesReportInfoCollection = ReportHelper.GetBinaryDeSerializedReportData(oRptArchiveInfo.ReportData) as List<ReviewNotesReportInfo>;

            if (oReviewNotesReportInfoCollection != null)
            {
                Session[SessionConstants.REPORT_DATA_REVIEW_NOTES] = oReviewNotesReportInfoCollection;
                if (!IsPostBack)
                {
                    GridHelper.ShowHideColumnsBasedOnFeatureCapability(ucSkyStemARTGrid.Grid.CreateTableView());

                    ucSkyStemARTGrid.DataSource = oReviewNotesReportInfoCollection;
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
                    SessionHelper.ClearSession(SessionConstants.REPORT_DATA_REVIEW_NOTES);
                    GetGridData();
                    ucSkyStemARTGrid.BindGrid();
                }
            }
        }

    }

    private List<ReviewNotesReportInfo> GetGridData()
    {
        List<ReviewNotesReportInfo> oReviewNotesReportInfoCollection = null;
        try
        {
            oReviewNotesReportInfoCollection = (List<ReviewNotesReportInfo>)HttpContext.Current.Session[SessionConstants.REPORT_DATA_REVIEW_NOTES];

            if (oReviewNotesReportInfoCollection == null || oReviewNotesReportInfoCollection.Count == 0)
            {
                ReportSearchCriteria oReportSearchCriteria = this.GetNormalSearchCriteria();
                DataTable dtEntity = ReportHelper.GetEntitySearchCriteria(_oCriteriaCollection);
                IReport oReportClient = RemotingHelper.GetReportObject();
                oReviewNotesReportInfoCollection = oReportClient.GetReportReviewNotesReport(oReportSearchCriteria, dtEntity, Helper.GetAppUserInfo());
                oReviewNotesReportInfoCollection = LanguageHelper.TranslateLabelsReviewNotesReport(oReviewNotesReportInfoCollection);
                Session[SessionConstants.REPORT_DATA_REVIEW_NOTES] = oReviewNotesReportInfoCollection;

                GridHelper.ShowHideColumnsBasedOnFeatureCapability(ucSkyStemARTGrid.Grid.CreateTableView());

                ucSkyStemARTGrid.DataSource = oReviewNotesReportInfoCollection;
                //Sort the Data
                GridHelper.SortDataSource(ucSkyStemARTGrid.Grid.MasterTableView);
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

        return oReviewNotesReportInfoCollection;
    }
    protected void ucSkyStemARTGrid_GridItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {

        ReportHelper.ItemDataBoundReviewNotesByUserReport(e);


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
        ucSkyStemARTGrid.Grid.EntityNameLabelID = 2635;
        ShowHideColumns();
    }
    private void ShowHideColumns()
    {
        GridColumn oGridColumn = ucSkyStemARTGrid.Grid.Columns.FindByUniqueNameSafe("Reviewer");
        if (oGridColumn != null)
        {
            // Check for Capability
            if (Helper.IsCapabilityActivatedForRecPeriodID(ARTEnums.Capability.DualLevelReview, _ReconciliationPeriodID.Value, false))
                oGridColumn.Visible = true;
            else
                oGridColumn.Visible = false;

        }

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
                case ReportCriteriaKeyName.RPTCRITERIAKEYNAME_FROMACCOUNT:
                    oReportSearchCriteria.FromAccountNumber = keyValuePair.Value;
                    break;
                case ReportCriteriaKeyName.RPTCRITERIAKEYNAME_TOACCOUNT:
                    oReportSearchCriteria.ToAccountNumber = keyValuePair.Value;
                    break;
                case ReportCriteriaKeyName.RPTCRITERIAKEYNAME_RISKRATING:
                    oReportSearchCriteria.RiskRatingIDs = keyValuePair.Value;
                    break;
                case ReportCriteriaKeyName.RPTCRITERIAKEYNAME_ISKEYACCOUNT:
                    oReportSearchCriteria.IsKeyccount = ReportHelper.GetBoolValueFromKeyValue(keyValuePair.Value);
                    break;
            }
        }

        //Other fields required for sp
        {
            ReportHelper.SetParameterValueForRequesterUserAndLanguage(oReportSearchCriteria);

            oReportSearchCriteria.CompanyID = SessionHelper.CurrentCompanyID;
            oReportSearchCriteria.IsDualReviewEnabled = Helper.IsCapabilityActivatedForRecPeriodID(ARTEnums.Capability.DualLevelReview, _ReconciliationPeriodID.Value, false);
            //oReportSearchCriteria.IsKeyAccountEnabled = this._IsKeyAccountEnabled;
            //oReportSearchCriteria.IsRiskRatingEnabled = this._IsRiskRatingEnabled;
            //oReportSearchCriteria.IsZeroBalanceAccountEnabled = this._IsZeroBalanceEnabled;

            oReportSearchCriteria.ExcludeNetAccount = true;
            oReportSearchCriteria.IsRequesterUserIDToBeConsideredForPermission = true;
        }

        return oReportSearchCriteria;
    }
    public override string GetMenuKey()
    {
        return "";
    }


    protected void ucSkyStemARTGrid_GridDetailTableDataBind(object source, GridDetailTableDataBindEventArgs e)
    {
        try
        {
            switch (e.DetailTableView.Name)
            {
                case "ReviewNoteDetails":
                    GridDataItem oGridItem = e.DetailTableView.ParentItem;
                    int? AccountNumber = Convert.ToInt32(oGridItem.GetDataKeyValue("AccountNumber"));
                    List<ReviewNotesReportInfo> objReviewNotesData = ((List<ReviewNotesReportInfo>)Session[SessionConstants.REPORT_DATA_REVIEW_NOTES]);
                    List<ReviewNotesReportInfo> filteredReviewNotesData = new List<ReviewNotesReportInfo>();
                    foreach (ReviewNotesReportInfo objRN in objReviewNotesData)
                    {
                        if (objRN.AccountNumber == AccountNumber.ToString())
                        {
                            filteredReviewNotesData.Add(objRN);
                        }
                    }
                    e.DetailTableView.DataSource = filteredReviewNotesData;
                    break;
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
    }
}
