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
using SkyStem.ART.Web.UserControls;

public partial class Pages_Reports_QualityScoreReport : PageBaseReport
{
    short? _reportID = 0;
    private int? _ReconciliationPeriodID = null;
    Dictionary<string, string> _oCriteriaCollection = null;



    protected void Page_Load(object sender, EventArgs e)
    {

        Helper.RegisterPostBackToControls(this, ucSkyStemARTGrid.Grid);

        Helper.SetPageTitle(this, 2460);
        ucSkyStemARTGrid.BasePageTitle = 2460;
        ReportMstInfo oReportInfo = (ReportMstInfo)Session[SessionConstants.REPORT_INFO_OBJECT];

        //if (!IsPostBack)
        //{
        //    // Set default Sorting
        //    //GridHelper.SetSortExpression(ucSkyStemARTGrid.Grid.MasterTableView, "AccountName");
        //}

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
            List<QualityScoreReportInfo> oQualityScoreReportInfoCollection = ReportHelper.GetBinaryDeSerializedReportData(oRptArchiveInfo.ReportData) as List<QualityScoreReportInfo>;
            if (oQualityScoreReportInfoCollection != null)
            {
                Session[SessionConstants.REPORT_DATA_QUALITYSCORE_ITEM] = oQualityScoreReportInfoCollection;

                GridHelper.ShowHideColumnsBasedOnFeatureCapability(ucSkyStemARTGrid.Grid.CreateTableView());
                if (!IsPostBack)
                {
                    SessionHelper.ClearSession(SessionConstants.REPORT_DATA_QUALITYSCORE_ITEM);
                    GetGridData(oQualityScoreReportInfoCollection);
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
                    SessionHelper.ClearSession(SessionConstants.REPORT_DATA_QUALITYSCORE_ITEM);

                    //if (_oCriteriaCollection != null)
                    //{
                    //    ReportSearchCriteria oReportSearchCriteria = this.GetNormalSearchCriteria();
                    //    DataTable dtEntity = ReportHelper.GetEntitySearchCriteria(_oCriteriaCollection);
                    //    RequestHelper.SaveRequest(ARTEnums.RequestType.ExportToExcelAndEmailReport, ARTEnums.Grid.QualityScoreReport, RequestHelper.CreateDataSetForExportToExcelReport(this.GetNormalSearchCriteria(), dtEntity));
                    //    //Helper.ShowConfirmationMessage(this, LanguageUtil.GetValue(2815));
                    //}
                    GetGridData(null);
                    ucSkyStemARTGrid.BindGrid();
                }
            }
        }
        //}
    }

  

    protected void ucSkyStemARTGrid_GridDetailTableDataBind(object source, GridDetailTableDataBindEventArgs e)
    {
        try
        {
            switch (e.DetailTableView.Name)
            {
                case "QualityScoreDetails":
                    GridDataItem oGridItem = e.DetailTableView.ParentItem;
                    int? GLDataID = Convert.ToInt32(oGridItem.GetDataKeyValue("GLDataID"));
                    List<QualityScoreReportInfo> objQualityScoreData = ((List<QualityScoreReportInfo>)Session[SessionConstants.REPORT_DATA_QUALITYSCORE_ITEM]);
                    List<QualityScoreReportInfo> filteredQualityScoreData = new List<QualityScoreReportInfo>();
                    foreach(QualityScoreReportInfo objQS in objQualityScoreData)
                    {
                        if(objQS.GLDataID==GLDataID)
                        {
                            filteredQualityScoreData.Add(objQS);
                        }
                    }
                    e.DetailTableView.DataSource = filteredQualityScoreData;
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

    private List<QualityScoreReportInfo> GetGridData(List<QualityScoreReportInfo> qsDataForArchive)
    {

        List<QualityScoreReportInfo> oOpenItemsReportInfoCollection = null;
        List<QualityScoreReportInfo> oOpenItemsReportAggregatedInfoCollection = new List<QualityScoreReportInfo>();
        ReportSearchCriteria oReportSearchCriteria = null;
        try
        {
            oOpenItemsReportInfoCollection = (List<QualityScoreReportInfo>)HttpContext.Current.Session[SessionConstants.REPORT_DATA_QUALITYSCORE_ITEM];
            if (_oCriteriaCollection != null)
            {
                oReportSearchCriteria = this.GetNormalSearchCriteria();
                if (oOpenItemsReportInfoCollection == null || oOpenItemsReportInfoCollection.Count == 0)
                {
                    DataTable dtEntity = ReportHelper.GetEntitySearchCriteria(_oCriteriaCollection);
                    //DataTable dtUser = ReportHelper.GetUserSearchCriteria(_oCriteriaCollection);
                    //DataTable dtRole = ReportHelper.GetRoleSearchCriteria(_oCriteriaCollection);

                    //TODO: when no user is selected then assume that all r selected
                    //ReportHelper.SendUserSearchCriteriaWhenNoUserSelected(_reportID.Value, ref dtUser, ref dtRole);

                    IReport oReportClient = RemotingHelper.GetReportObject();
                    oOpenItemsReportInfoCollection = oReportClient.GetReportQualityScoreReport(oReportSearchCriteria, dtEntity, Helper.GetAppUserInfo());
                    oOpenItemsReportInfoCollection = LanguageHelper.TranslateLabelsQualityScoreReport(oOpenItemsReportInfoCollection);


                    //oOpenItemsReportInfoCollection = LanguageHelper.TranslateLabelsOpenItemsReport(oOpenItemsReportInfoCollection);
                    Session[SessionConstants.REPORT_DATA_QUALITYSCORE_ITEM] = oOpenItemsReportInfoCollection;

                    GridHelper.ShowHideColumnsBasedOnFeatureCapability(ucSkyStemARTGrid.Grid.CreateTableView());
                }
            }

            string reportType = Request.QueryString[QueryStringConstants.REPORT_TYPE];

            if (!string.IsNullOrEmpty(reportType) && Convert.ToInt16(reportType) == (short)WebEnums.ReportType.ArchivedReport)
            {
                oOpenItemsReportInfoCollection = qsDataForArchive;
            }

            oOpenItemsReportAggregatedInfoCollection = oOpenItemsReportInfoCollection.
                GroupBy(c => c.GLDataID)
                .Select(c => c.First()).ToList();

            if (!string.IsNullOrEmpty(reportType) && Convert.ToInt16(reportType) == (short)WebEnums.ReportType.ArchivedReport)
            {
                oOpenItemsReportAggregatedInfoCollection = GetAggregatedQualityScoreData(oOpenItemsReportAggregatedInfoCollection, qsDataForArchive);
            }
            else
            {
                oOpenItemsReportAggregatedInfoCollection = GetAggregatedQualityScoreData(oOpenItemsReportAggregatedInfoCollection,null);
            }
            List<QualityScoreReportInfo> finalQualityScoreData = new List<QualityScoreReportInfo>();

            #region Apply Filter Criteria
            if (oReportSearchCriteria != null)
            {
                if (!string.IsNullOrEmpty(oReportSearchCriteria.FromSystemScore))
                {
                    oOpenItemsReportAggregatedInfoCollection = oOpenItemsReportAggregatedInfoCollection.FindAll(q => q.SystemQualityScore >= Convert.ToInt16(oReportSearchCriteria.FromSystemScore));
                }

                if (!string.IsNullOrEmpty(oReportSearchCriteria.ToSystemScore))
                {
                    oOpenItemsReportAggregatedInfoCollection = oOpenItemsReportAggregatedInfoCollection.FindAll(q => q.SystemQualityScore <= Convert.ToInt16(oReportSearchCriteria.ToSystemScore));
                }

                if (!string.IsNullOrEmpty(oReportSearchCriteria.FromUserScore))
                {
                    oOpenItemsReportAggregatedInfoCollection = oOpenItemsReportAggregatedInfoCollection.FindAll(q => q.UserQualityScore >= Convert.ToInt16(oReportSearchCriteria.FromUserScore));
                }

                if (!string.IsNullOrEmpty(oReportSearchCriteria.ToUserScore))
                {
                    oOpenItemsReportAggregatedInfoCollection = oOpenItemsReportAggregatedInfoCollection.FindAll(q => q.UserQualityScore <= Convert.ToInt16(oReportSearchCriteria.ToUserScore));
                }
            }

            #endregion

            //finalQualityScoreData = oOpenItemsReportAggregatedInfoCollection.FindAll(q => q.SystemQualityScore >= Convert.ToInt16(oReportSearchCriteria.FromSystemScore)
            //    && q.SystemQualityScore <= Convert.ToInt16(oReportSearchCriteria.ToSystemScore) && q.UserQualityScore >= Convert.ToInt16(oReportSearchCriteria.FromUserScore)
            //    && q.UserQualityScore <= Convert.ToInt16(oReportSearchCriteria.ToUserScore));
            Session[SessionConstants.REPORT_DATA_QUALITYSCORE_ITEM_PRINT] = oOpenItemsReportAggregatedInfoCollection;
            ucSkyStemARTGrid.DataSource = oOpenItemsReportAggregatedInfoCollection;
        }
        catch (ARTException ex)
        {
            Helper.ShowErrorMessage(this, ex);
        }
        catch (Exception ex)
        {
            Helper.ShowErrorMessage(this, ex);
        }

        return oOpenItemsReportAggregatedInfoCollection;
    }

    private List<QualityScoreReportInfo> GetAggregatedQualityScoreData(List<QualityScoreReportInfo> objQualityScoreAggregated,List<QualityScoreReportInfo> qsDataForArchive)
    {
         string reportType = Request.QueryString[QueryStringConstants.REPORT_TYPE];
         List<QualityScoreReportInfo> objOriginalMasterQSData = null;
         if (!string.IsNullOrEmpty(reportType) && Convert.ToInt16(reportType) == (short)WebEnums.ReportType.ArchivedReport)
         {
             objOriginalMasterQSData = qsDataForArchive;
         }
         else
         {
             ReportSearchCriteria oReportSearchCriteria = this.GetNormalSearchCriteria();
             DataTable dtEntity = ReportHelper.GetEntitySearchCriteria(_oCriteriaCollection);
             IReport oReportClient = RemotingHelper.GetReportObject();
             objOriginalMasterQSData = oReportClient.GetReportQualityScoreReport(oReportSearchCriteria, dtEntity, Helper.GetAppUserInfo());
             objOriginalMasterQSData = LanguageHelper.TranslateLabelsQualityScoreReport(objOriginalMasterQSData);
         }

        var result = from MD in objOriginalMasterQSData
                     group MD by MD.GLDataID into GR
                     select new {
                         Key = GR.Key,
                         SystemScore = GR.Sum(T => T.SystemQualityScore.GetValueOrDefault()),
                         UserScore = GR.Sum(T=> T.UserQualityScore.GetValueOrDefault())
                     } ;

        var joinresult = from GR in result
                         join AR in objQualityScoreAggregated
                         on GR.Key equals AR.GLDataID
                         select new { GR, AR };
        foreach (var item in joinresult)
        {
            item.AR.SystemQualityScore = (short?) item.GR.SystemScore;
            item.AR.UserQualityScore = (short?)item.GR.UserScore;
        }

        //foreach (QualityScoreReportInfo qsInfo in objQualityScoreAggregated)
        //{
        //    short? SumSystemQualityScore = 0;
        //    short? SumUserQualityScore = 0;
        //    foreach (QualityScoreReportInfo objQS in objOriginalMasterQSData.FindAll(p => p.GLDataID == qsInfo.GLDataID))
        //    {
        //        if (!String.IsNullOrEmpty(objQS.SystemQualityScore.ToString()))
        //        {
        //            SumSystemQualityScore += objQS.SystemQualityScore;
        //        }
        //        if (!String.IsNullOrEmpty(objQS.UserQualityScore.ToString()))
        //        {
        //            SumUserQualityScore += objQS.UserQualityScore;
        //        }
        //    }
        //    qsInfo.SystemQualityScore = SumSystemQualityScore;
        //    qsInfo.UserQualityScore = SumUserQualityScore;
        //}
        return objQualityScoreAggregated;
    }

    protected object ucSkyStemARTGrid_NeedDataSourceEventHandler(int count)
    {
        string reportType = Request.QueryString[QueryStringConstants.REPORT_TYPE];
        List<QualityScoreReportInfo> lstQualityScoreData =new List<QualityScoreReportInfo>();
        if (!string.IsNullOrEmpty(reportType) && Convert.ToInt16(reportType) == (short)WebEnums.ReportType.ArchivedReport)
        {
            ReportArchiveInfo oRptArchiveInfo = (ReportArchiveInfo)Session[SessionConstants.REPORT_ARCHIVED_DATA];
            List<QualityScoreReportInfo> oQualityScoreReportInfoCollection = ReportHelper.GetBinaryDeSerializedReportData(oRptArchiveInfo.ReportData) as List<QualityScoreReportInfo>;
            if (oQualityScoreReportInfoCollection != null)
            {
               lstQualityScoreData = GetGridData(oQualityScoreReportInfoCollection);
            }
        }
        else
        {
            lstQualityScoreData =  GetGridData(null);
        }

        return lstQualityScoreData;
    }
    protected void Page_PreRender(object sender, EventArgs e)
    {

        int? ReportRoleMandatoryReportID = null;
        ReportRoleMandatoryReportID = Convert.ToInt32(Request.QueryString[QueryStringConstants.MANDATORY_REPORT_ID]);
        if (ReportRoleMandatoryReportID != null && ReportRoleMandatoryReportID > 0)
            Helper.SetBreadcrumbs(this, 1072, 1016, ucSkyStemARTGrid.BasePageTitle);
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
        ucSkyStemARTGrid.Grid.EntityNameLabelID = 2460;
    }


    protected void ucSkyStemARTGrid_GridItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {
        ReportHelper.ItemDataBoundQualityScoreReport(e);
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
                case ReportCriteriaKeyName.RPTCRITERIAKEYNAME_ISMATERIALACCOUNT:
                    oReportSearchCriteria.IsMaterialAccount = ReportHelper.GetBoolValueFromKeyValue(keyValuePair.Value);
                    break;
                case ReportCriteriaKeyName.RPTCRITERIAKEYNAME_RISKRATING:
                    oReportSearchCriteria.RiskRatingIDs = keyValuePair.Value;
                    break;
                case ReportCriteriaKeyName.RPTCRITERIAKEYNAME_ISKEYACCOUNT:
                    oReportSearchCriteria.IsKeyccount = ReportHelper.GetBoolValueFromKeyValue(keyValuePair.Value);
                    break;
                case ReportCriteriaKeyName.RPTCRITERIAKEYNAME_FROMOPENDATE:
                    oReportSearchCriteria.FromOpenDate = ReportHelper.GetDateValueFromKeyValue(keyValuePair.Value);
                    break;
                case ReportCriteriaKeyName.RPTCRITERIAKEYNAME_TOOPENDATE:
                    oReportSearchCriteria.ToOpenDate = ReportHelper.GetDateValueFromKeyValue(keyValuePair.Value);
                    break;
                case ReportCriteriaKeyName.RPTCRITERIAKEYNAME_AGING:
                    oReportSearchCriteria.AgingIDs = keyValuePair.Value;
                    break;
                case ReportCriteriaKeyName.RPTCRITERIAKEYNAME_OPENITEMCLASSIFICATION:
                    oReportSearchCriteria.OpenItemClassificationIDs = keyValuePair.Value;
                    break;
                case ReportCriteriaKeyName.RPTCRITERIAKEYNAME_RANGEOFSCORE:
                    oReportSearchCriteria.QualityScoreRange = keyValuePair.Value;
                    break;
                case ReportCriteriaKeyName.RPTCRITERIAKEYNAME_CHECKLISTITEM:
                    oReportSearchCriteria.QualityScoreIDs = keyValuePair.Value;
                    break;
                case ReportCriteriaKeyName.RPTCRITERIAKEYNAME_FROMSYSTEMSCORE:
                    oReportSearchCriteria.FromSystemScore = keyValuePair.Value;
                    break;
                case ReportCriteriaKeyName.RPTCRITERIAKEYNAME_FROMUSERSCORE:
                    oReportSearchCriteria.FromUserScore = keyValuePair.Value;
                    break;
                case ReportCriteriaKeyName.RPTCRITERIAKEYNAME_TOSYSTEMSCORE:
                    oReportSearchCriteria.ToSystemScore = keyValuePair.Value;
                    break;
                case ReportCriteriaKeyName.RPTCRITERIAKEYNAME_TOUSERSCORE:
                    oReportSearchCriteria.ToUserScore = keyValuePair.Value;
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
            oReportSearchCriteria.IsZeroBalanceAccountEnabled = Helper.IsCapabilityActivatedForRecPeriodID(ARTEnums.Capability.ZeroBalanceAccount, _ReconciliationPeriodID.Value, false);

            oReportSearchCriteria.ExcludeNetAccount = true;
            oReportSearchCriteria.IsRequesterUserIDToBeConsideredForPermission = true;
        }

        return oReportSearchCriteria;
    }

    public override string GetMenuKey()
    {
        return "";
    }
}
