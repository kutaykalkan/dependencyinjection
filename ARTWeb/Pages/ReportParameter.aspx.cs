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
using SkyStem.Language.LanguageUtility;
using System.Text;
using SkyStem.ART.Client.Data;



public partial class Pages_ReportParameter : PageBaseCompany
{
    short? _reportID = 0;
    short _reportSectionIDFromURL = 0;

    public string Key2HdnControlClientID;
    #region "Private Properties"

    #endregion

    #region "Public Properties"
    public string GetSeperator
    {
        get
        {
            return WebConstants.ORGHIERARCHYVALUESEPERATOR;
        }
    }
    #endregion

    #region "Page event Handlers"
    protected void Page_Init(object sender, EventArgs e)
    {
        this.ucOrgHierarchy.CallbackFunctionName = "return OrgHierarchyCriteria({0},{1},{2},{3})";
        ucTaskType.DDLSelectedIndexChangedHandler += new SimpleDropDownList.OnDDLSelectedIndexChanged(ucTaskType_DDLSelectedIndexChangedHandler);

    }

    void ucTaskType_DDLSelectedIndexChangedHandler(string SelectedValue, string selectedText)
    {
        short TaskTypeID;
        if (short.TryParse(SelectedValue, out TaskTypeID))
        {
            ShowHideAccountTaskControls(TaskTypeID, true);
        }
    }
    private void ShowHideAccountTaskControls(short TaskTypeID, bool rebindData)
    {
        if (TaskTypeID == (short)ARTEnums.TaskType.GeneralTask)
        {
            ShowHideAccountTaskRptCriteriaControls(false);
            if (rebindData)
            {
                this.ucDisplayColumn.CBLDataSource = ReportHelper.GetListItemCollectionForDisplayColumn(_reportID.Value, true, true);
                ucDisplayColumn.ClearSelection();
            }
        }
        else
        {
            ShowHideAccountTaskRptCriteriaControls(true);
            if (rebindData)
            {
                this.ucDisplayColumn.CBLDataSource = ReportHelper.GetListItemCollectionForDisplayColumn(_reportID.Value, true, false);
                ucDisplayColumn.ClearSelection();
            }
        }
    }

    private void ShowHideAccountTaskRptCriteriaControls(bool bVisible)
    {
        this.ucAccountRange.IsEnabled = bVisible;
        this.ucOrgHierarchy.IsEnabled = bVisible;
        this.ucRiskRating.IsEnabled = bVisible;


    }
    protected void Page_Load(object sender, EventArgs e)
    {


        Helper.SetPageTitle(this, 1563);
        if (Request.QueryString[QueryStringConstants.REPORT_ID] != null)
            _reportID = Convert.ToInt16(Request.QueryString[QueryStringConstants.REPORT_ID]);
        if (Request.QueryString[QueryStringConstants.REPORT_SECTION_ID] != null)
            _reportSectionIDFromURL = Convert.ToInt16(Request.QueryString[QueryStringConstants.REPORT_SECTION_ID]);

        ReportHelper.SetReportsBreadCrumb(this);

        if (!this.IsPostBack && Request.QueryString[QueryStringConstants.REPORT_SESSIONCLEAR] != null)
            ReportHelper.ClearReportSessions();

        ReportMstInfo oReportInfo = ReportHelper.GetReportInfoByReportID(_reportID, SessionHelper.GetUserLanguage()
                                                        , AppSettingHelper.GetDefaultBusinessEntityID(), AppSettingHelper.GetDefaultLanguageID());
        Session[SessionConstants.REPORT_INFO_OBJECT] = oReportInfo;
        if (oReportInfo != null)
        {
            this.lblReportDetails.LabelID = oReportInfo.ReportLabelID.Value;
        }

        DisableMasterPageRecPeriodDropDown();
        this.ucSingleDate.LabelID = 2694;
        if (!this.IsPostBack)
            this.ucSingleDate.SetSingleDate = System.DateTime.Now;

    }

    private void DisableMasterPageRecPeriodDropDown()
    {
        MasterPageBase oMasterPageBase = (MasterPageBase)this.Master;

        DropDownList ddlRecPeriod = (DropDownList)oMasterPageBase.FindControl("ddlReconciliationPeriod");
        if (ddlRecPeriod != null)
            ddlRecPeriod.Enabled = false;
    }

    protected override void OnLoadComplete(EventArgs e)
    {
        if (!this.IsPostBack)
        {
            this.PopulatePage();
            Dictionary<string, string> oRptCriteriaCollection = null;
            if (Session[SessionConstants.REPORT_CRITERIA] != null)//from change parameter
            {
                oRptCriteriaCollection = (Dictionary<string, string>)Session[SessionConstants.REPORT_CRITERIA];
                LoadDataInRptCriteriaControlsFromSession(oRptCriteriaCollection);
                Session.Remove(SessionConstants.REPORT_CRITERIA);
            }
            else
            {
                if (this.ucFinancialYear.Visible)
                {
                    this.ucFinancialYear.FinancialYearID = SessionHelper.CurrentFinancialYearID;

                    // Load the Rec Periods based on the Selected FY
                    this.ucRecPeriod.ReloadRecPeriods(SessionHelper.CurrentFinancialYearID);
                    if (_reportID == 2)
                    {
                        ListItem currentItem = new ListItem(WebConstants.CURRENT_REC_PERIOD, WebConstants.CURRENT_REC_PERIOD_INDEX);
                        this.ucRecPeriod.RecPeriodDropDown.Items.Insert(0, currentItem);
                    }
                }

                if (this.ucRecPeriod.Visible)
                    this.ucRecPeriod.SetDefaultPeriod = SessionHelper.CurrentReconciliationPeriodID.Value;

                if (this.ucPreparer.Visible)
                {
                    this.ucPreparer.SetDefaultSelectedRoles = ((short)ARTEnums.UserRole.PREPARER).ToString();
                    this.FetchClickedPreparer(this.ucPreparer.GetSelectedRoles);
                }
                if (this.ucRoleUser.Visible && SessionHelper.CurrentRoleID.Value == (short)ARTEnums.UserRole.PREPARER)
                {
                    this.ucRoleUser.SetDefaultSelectedRoles = ((short)ARTEnums.UserRole.PREPARER).ToString();
                    this.FetchClicked(this.ucRoleUser.GetSelectedRoles);
                }
                int recPeriodID;
                if (int.TryParse(this.ucRecPeriod.GetSelectedRecPeriod, out recPeriodID))
                    this.EnableDisableCriteriaControlsAsPerRecPeriodCapability(recPeriodID);


                if (ucRoleUser.Visible)
                {
                    List<short> PermittedRoles = ReportHelper.GetPermittedRolesByReportID(_reportID.Value, SessionHelper.CurrentRoleID.Value, recPeriodID);
                    this.ucRoleUser.DataSourceForRolesList = ReportHelper.GetListItemCollectionForRoleByPermittedRoles(PermittedRoles);
                }

            }
        }
        base.OnLoadComplete(e);
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        int recPeriodID;
        this.LegendOnAccountSearch.Visible = (this.ucOrgHierarchy.Visible);
        if (int.TryParse(this.ucRecPeriod.GetSelectedRecPeriod, out recPeriodID))
            this.EnableDisableCriteriaControlsAsPerRecPeriodCapability(recPeriodID);
        if (ucTaskType.Visible && ucTaskType.SelectedValue != null)
        {
            short TaskTypeID;
            TaskTypeID = Convert.ToInt16(ucTaskType.SelectedValue);
            ShowHideAccountTaskControls(TaskTypeID, false);
        }
        //btnExportToExcelAndEmailReport.Visible = false;
        //if (_reportID == (short)WebEnums.Reports.QUALITY_SCORE_REPORT)
        //{
        //    btnExportToExcelAndEmailReport.Visible = true;
        //}
    }

    #endregion

    protected void FinancialYear_Changed(string selectedValue, string selectedText)
    {
        int financialYearID;
        if (int.TryParse(selectedValue, out financialYearID))
        {
            // Reload Rec Periods
            this.ucRecPeriod.ReloadRecPeriodsOnFinancialYearChange(financialYearID);
        }
    }
    protected void RecPeriod_Changed(string selectedValue, string selectedText)
    {
        int recPeriodID;
        if (int.TryParse(selectedValue, out recPeriodID))
        {
            //    this.EnableDisableCriteriaControlsAsPerRecPeriodCapability(recPeriodID);
            //this.ResetSelectionInAllControls();


            if (this.ucPreparer.Visible)
            {
                this.ucPreparer.SetDefaultSelectedRoles = ((short)ARTEnums.UserRole.PREPARER).ToString();
                this.FetchClickedPreparer(this.ucPreparer.GetSelectedRoles);
            }

            if (ucRoleUser.Visible)
            {
                List<short> PermittedRoles = ReportHelper.GetPermittedRolesByReportID(_reportID.Value, SessionHelper.CurrentRoleID.Value, recPeriodID);
                this.ucRoleUser.DataSourceForRolesList = ReportHelper.GetListItemCollectionForRoleByPermittedRoles(PermittedRoles);
            }
            if (ucChecklistItem.Visible)
            {
                SessionHelper.ClearSession(SessionConstants.QUALITYSCORECHECKLIST_TYPES);
                //CacheHelper.ClearCache(CacheConstants.ALL_CHECKLISTCATEGORIES);
                this.ucChecklistItem.CBLDataSource = ReportHelper.GetListItemCollectionForQualityScoreChecklist(recPeriodID);
            }
        }
    }

    protected void FetchClicked(List<short> selectedRoleIDs)
    {
        this.ucRoleUser.DataSourceForUserList = ReportHelper.GetListItemCollectionForUser(selectedRoleIDs, this.ucRecPeriod.GetSelectedRecPeriod);
    }

    protected void FetchClickedPreparer(List<short> selectedRoleIDs)
    {
        this.ucPreparer.DataSourceForUserList = ReportHelper.GetListItemCollectionForUser(selectedRoleIDs, this.ucRecPeriod.GetSelectedRecPeriod);
    }

    protected void btnRunReport_Click(object sender, EventArgs e)
    {
        ReportMstInfo oReportInfo = SetParameterInfoToSession();
        //~/Pages/Reports/OpenItemsReport.aspx
        string url = string.Empty;
        //if (_reportID == 2 && Convert.ToInt32(oReportCriteria["Period"]) == -1)
        //{
        //    url = oReportInfo.ReportUrl.Replace("OpenItemsReport", "OpenItemReportForCurrentRecPeriod");
        //}
        //else
        //{
        url = oReportInfo.ReportUrl;
        //}
        url = ReportHelper.AddCommonQueryStringParameter(url);
        url = url + "&" + QueryStringConstants.REPORT_TYPE + "=" + ((short)WebEnums.ReportType.StandardReport).ToString();
        url = url + "&" + QueryStringConstants.REPORT_SECTION_ID + "=" + Request.QueryString[QueryStringConstants.REPORT_SECTION_ID];
        Response.Redirect(url);
    }

    protected void btnExportToExcelAndEmailReport_Click(object sender, EventArgs e)
    {
        //ReportMstInfo oReportInfo = SetParameterInfoToSession();
        //string url = string.Empty;
        //url = oReportInfo.ReportUrl;
        //url = ReportHelper.AddCommonQueryStringParameter(url);
        //url = url + "&" + QueryStringConstants.REPORT_TYPE + "=" + ((short)WebEnums.ReportType.StandardReport).ToString();
        //url = url + "&" + QueryStringConstants.REPORT_SECTION_ID + "=" + Request.QueryString[QueryStringConstants.REPORT_SECTION_ID];
        //Response.Redirect(url);
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        //Session.Remove(SessionConstants.REPORT_CRITERIA);
        //Session.Remove(SessionConstants.REPORT_INFO_OBJECT);
        ReportHelper.ClearReportSessions();
        string url = "ReportHome.aspx?" + QueryStringConstants.REPORT_SECTION_ID + "=" + _reportSectionIDFromURL;
        Response.Redirect(url);
    }

    #region "Private Methods"
    private ReportMstInfo SetParameterInfoToSession()
    {
        ReportMstInfo oReportInfo = (ReportMstInfo)Session[SessionConstants.REPORT_INFO_OBJECT];
        //Get Parameters into a dictionary.
        Dictionary<string, string> oReportCriteria = new Dictionary<string, string>();
        if (this.ucOrgHierarchy.Visible)
            this.ucOrgHierarchy.GetCriteria(oReportCriteria);

        if (this.ucFinancialYear.Visible)
            this.ucFinancialYear.GetCriteria(oReportCriteria, ReportCriteriaKeyName.RPTCRITERIAKEYNAME_FINANCIAL_YEAR);

        if (this.ucRecPeriod.Visible)
            this.ucRecPeriod.GetCriteria(oReportCriteria, ReportCriteriaKeyName.RPTCRITERIAKEYNAME_RECPERIOD);

        if (this.ucReasonCode.Visible)
            this.ucReasonCode.GetCriteria(oReportCriteria, ReportCriteriaKeyName.RPTCRITERIAKEYNAME_REASON);

        //If Current is selected in dropdown then capability features should not be included in Report Criteria
        if (ucRecPeriod.RecPeriodDropDown.SelectedValue != WebConstants.CURRENT_REC_PERIOD_INDEX)
        {
            if (this.ucKeyAccount.Visible)
                this.ucKeyAccount.GetCriteria(oReportCriteria, ReportCriteriaKeyName.RPTCRITERIAKEYNAME_ISKEYACCOUNT);

            if (this.ucMaterialAccount.Visible)
                this.ucMaterialAccount.GetCriteria(oReportCriteria, ReportCriteriaKeyName.RPTCRITERIAKEYNAME_ISMATERIALACCOUNT);

            if (this.ucRiskRating.Visible)
                this.ucRiskRating.GetCriteria(oReportCriteria, ReportCriteriaKeyName.RPTCRITERIAKEYNAME_RISKRATING);
        }

        if (this.ucOpenItemClassification.Visible)
            this.ucOpenItemClassification.GetCriteria(oReportCriteria, ReportCriteriaKeyName.RPTCRITERIAKEYNAME_OPENITEMCLASSIFICATION);

        if (this.ucAccountRange.Visible)
        {
            this.ucAccountRange.GetFromCriteria(oReportCriteria, ReportCriteriaKeyName.RPTCRITERIAKEYNAME_FROMACCOUNT);
            this.ucAccountRange.GetToCriteria(oReportCriteria, ReportCriteriaKeyName.RPTCRITERIAKEYNAME_TOACCOUNT);
        }
        if (this.ucCloseDate.Visible)
        {
            this.ucCloseDate.GetFromDateCriteria(oReportCriteria, ReportCriteriaKeyName.RPTCRITERIAKEYNAME_FROMCLOSEDATE);
            this.ucCloseDate.GetToDateCriteria(oReportCriteria, ReportCriteriaKeyName.RPTCRITERIAKEYNAME_TOCLOSEDATE);
        }
        if (this.ucOpenDate.Visible)
        {
            this.ucOpenDate.GetFromDateCriteria(oReportCriteria, ReportCriteriaKeyName.RPTCRITERIAKEYNAME_FROMOPENDATE);
            this.ucOpenDate.GetToDateCriteria(oReportCriteria, ReportCriteriaKeyName.RPTCRITERIAKEYNAME_TOOPENDATE);
        }
        if (this.ucDateCreated.Visible)
        {
            this.ucDateCreated.GetFromDateCriteria(oReportCriteria, ReportCriteriaKeyName.RPTCRITERIAKEYNAME_FROMDATECREATED);
            this.ucDateCreated.GetToDateCriteria(oReportCriteria, ReportCriteriaKeyName.RPTCRITERIAKEYNAME_TODATECREATED);
        }
        if (this.ucChangeDate.Visible)
        {
            this.ucChangeDate.GetFromDateCriteria(oReportCriteria, ReportCriteriaKeyName.RPTCRITERIAKEYNAME_FROMCHANGEDATE);
            this.ucChangeDate.GetToDateCriteria(oReportCriteria, ReportCriteriaKeyName.RPTCRITERIAKEYNAME_TOCHANGEDATE);
        }
        if (this.ucRoleUser.Visible)
        {
            this.ucRoleUser.GetUserCriteria(oReportCriteria, ReportCriteriaKeyName.RPTCRITERIAKEYNAME_USER);
            this.ucRoleUser.GetRoleCriteria(oReportCriteria, ReportCriteriaKeyName.RPTCRITERIAKEYNAME_ROLE);
        }
        if (this.ucAging.Visible)
        {
            this.ucAging.GetCriteria(oReportCriteria, ReportCriteriaKeyName.RPTCRITERIAKEYNAME_AGING);
        }
        if (this.ucPreparer.Visible)
        {
            this.ucPreparer.GetRoleCriteria(oReportCriteria, ReportCriteriaKeyName.RPTCRITERIAKEYNAME_ROLE);
            this.ucPreparer.GetUserCriteria(oReportCriteria, ReportCriteriaKeyName.RPTCRITERIAKEYNAME_USER);
        }
        if (this.ucRecStatus.Visible)
        {
            this.ucRecStatus.GetCriteria(oReportCriteria, ReportCriteriaKeyName.RPTCRITERIAKEYNAME_RECSTATUS);
        }
        if (this.ucExceptionType.Visible)
        {
            this.ucExceptionType.GetCriteria(oReportCriteria, ReportCriteriaKeyName.RPTCRITERIAKEYNAME_TYPEOFEXCEPTION);
        }
        if (this.ucChecklistItem.Visible)
        {
            this.ucChecklistItem.GetCriteria(oReportCriteria, ReportCriteriaKeyName.RPTCRITERIAKEYNAME_CHECKLISTITEM);
            if (string.IsNullOrEmpty(oReportCriteria[ReportCriteriaKeyName.RPTCRITERIAKEYNAME_CHECKLISTITEM]))
            {
                oReportCriteria[ReportCriteriaKeyName.RPTCRITERIAKEYNAME_CHECKLISTITEM] = GetAllQualityScoreChecklist();
            }
        }
        if (this.ucSystemScoreRange.Visible)
        {
            this.ucSystemScoreRange.GetFromCriteria(oReportCriteria, ReportCriteriaKeyName.RPTCRITERIAKEYNAME_FROMSYSTEMSCORE);
            this.ucSystemScoreRange.GetToCriteria(oReportCriteria, ReportCriteriaKeyName.RPTCRITERIAKEYNAME_TOSYSTEMSCORE);
        }
        if (this.ucUserScoreRange.Visible)
        {
            this.ucUserScoreRange.GetFromCriteria(oReportCriteria, ReportCriteriaKeyName.RPTCRITERIAKEYNAME_FROMUSERSCORE);
            this.ucUserScoreRange.GetToCriteria(oReportCriteria, ReportCriteriaKeyName.RPTCRITERIAKEYNAME_TOUSERSCORE);
        }
        if (this.ucSingleDate.Visible)
        {
            if (string.IsNullOrEmpty(ucSingleDate.GetSingleDate))

                oReportInfo.Description = LanguageUtil.GetValue(oReportInfo.DescriptionLabelID.Value);
            else
                oReportInfo.Description = LanguageUtil.GetValue(oReportInfo.DescriptionLabelID.Value) + " " + LanguageUtil.GetValue(2702) + " " + Helper.GetDisplayDate(Convert.ToDateTime(ucSingleDate.GetSingleDate));

            this.ucSingleDate.GetCriteria(oReportCriteria, ReportCriteriaKeyName.RPTCRITERIAKEYNAME_ASONDATE);
        }
        if (this.ucTaskStatus.Visible)
        {
            this.ucTaskStatus.GetCriteria(oReportCriteria, ReportCriteriaKeyName.RPTCRITERIAKEYNAME_TASKSTATUS);
        }
        if (this.ucTaskListName.Visible)
        {
            this.ucTaskListName.GetCriteria(oReportCriteria, ReportCriteriaKeyName.RPTCRITERIAKEYNAME_TASKLISTNAME);
        }
        if (this.ucTaskType.Visible)
        {
            this.ucTaskType.GetCriteria(oReportCriteria, ReportCriteriaKeyName.RPTCRITERIAKEYNAME_TASKTYPE);
        }
        if (this.ucDisplayColumn.Visible)
        {
            this.ucDisplayColumn.GetCriteria(oReportCriteria, ReportCriteriaKeyName.RPTCRITERIAKEYNAME_DISPLAYCOLUMN);
        }
        //Save dictionary in session variable

        Session[SessionConstants.REPORT_CRITERIA] = oReportCriteria;

        //2694
        oReportInfo.DateAdded = DateTime.Now;
        SessionHelper.ClearSession(SessionConstants.REPORT_INFO_OBJECT);

        Session[SessionConstants.REPORT_INFO_OBJECT] = oReportInfo;
        return oReportInfo;
    }

    private string GetAllQualityScoreChecklist()
    {
        int recPeriodID = 0;
        if (!string.IsNullOrEmpty(this.ucRecPeriod.GetSelectedRecPeriod))
        {
            recPeriodID = Convert.ToInt32(this.ucRecPeriod.GetSelectedRecPeriod);
        }
        else
        {
            recPeriodID = SessionHelper.CurrentReconciliationPeriodID.Value;
        }
        ListItemCollection checklistItems = ReportHelper.GetListItemCollectionForQualityScoreChecklist(recPeriodID);
        StringBuilder sbChecklists = new StringBuilder();
        string strChecklistItems = string.Empty;
        if (checklistItems != null && checklistItems.Count > 0)
        {
            foreach (ListItem item in checklistItems)
            {
                sbChecklists.Append(item.Value);
                sbChecklists.Append(AppSettingHelper.GetAppSettingValue(AppSettingConstants.FILTER_VALUE_SEPARATOR));
            }
            strChecklistItems = sbChecklists.ToString(0, sbChecklists.Length - 1);
        }
        return strChecklistItems;
    }

    private void EnableDisableCriteriaControlsAsPerRecPeriodCapability(int recPeriodID)
    {
        bool isKeyAccountEnabled;
        bool isRiskRatingEnabled;
        bool isMaterialAccountEnabled;

        if (recPeriodID == -1)
        {
            this.ucKeyAccount.Enabled = false;
            this.ucRiskRating.Enabled = false;
            this.ucMaterialAccount.Enabled = false;
        }

        else
        {

            if (this.ucKeyAccount.Visible)
            {
                isKeyAccountEnabled = Helper.IsCapabilityActivatedForRecPeriodID(ARTEnums.Capability.KeyAccount, recPeriodID, false);
                this.ucKeyAccount.Enabled = isKeyAccountEnabled;
                if (!isKeyAccountEnabled)
                    this.ucKeyAccount.ClearSelection();
            }

            if (this.ucRiskRating.Visible)
            {
                isRiskRatingEnabled = Helper.IsCapabilityActivatedForRecPeriodID(ARTEnums.Capability.RiskRating, recPeriodID, false);
                this.ucRiskRating.Enabled = isRiskRatingEnabled;
                if (!isRiskRatingEnabled)
                    this.ucRiskRating.ClearSelection();
            }

            if (this.ucMaterialAccount.Visible)
            {
                isMaterialAccountEnabled = Helper.IsCapabilityActivatedForRecPeriodID(ARTEnums.Capability.MaterialitySelection, recPeriodID, false);
                this.ucMaterialAccount.Enabled = isMaterialAccountEnabled;
                if (!isMaterialAccountEnabled)
                    this.ucMaterialAccount.ClearSelection();
            }
        }
    }

    private void ShowHideRptCriteriaControls(List<ReportParameterInfo> oReportParams)
    {

        foreach (ReportParameterInfo oRptParam in oReportParams)
        {
            short rptParamID = oRptParam.ParameterID;
            bool isRequired = oRptParam.IsMandatory;
            int recPeriodID = 0;
            if (!string.IsNullOrEmpty(this.ucRecPeriod.GetSelectedRecPeriod))
            {
                recPeriodID = Convert.ToInt32(this.ucRecPeriod.GetSelectedRecPeriod);
            }
            else
            {
                recPeriodID = SessionHelper.CurrentReconciliationPeriodID.Value;
            }

            switch (rptParamID)
            {
                case (short)WebEnums.ReportParameter.Entity:
                    //TODO: when company has no keys, dont show this control
                    this.ucOrgHierarchy.Visible = true;
                    this.pnlCriteria.Visible = true;
                    this.ucOrgHierarchy.isRequired = isRequired;
                    break;
                case (short)WebEnums.ReportParameter.Account:
                    this.ucAccountRange.Visible = true;
                    this.ucAccountRange.isRequired = isRequired;
                    //this.ucAccountRange.ErrorLabelID = 5000170;
                    break;
                case (short)WebEnums.ReportParameter.Reason:
                    this.ucReasonCode.Visible = true;
                    this.ucReasonCode.CBLDataSource = ReportHelper.GetListItemCollectionForReason();
                    this.ucReasonCode.isRequired = isRequired;
                    break;
                case (short)WebEnums.ReportParameter.Period:
                    this.ucFinancialYear.Visible = true;
                    this.ucRecPeriod.Visible = true;
                    this.ucRecPeriod.isRequired = isRequired;
                    this.ucRecPeriod.AddSelectOne = !isRequired;
                    break;
                case (short)WebEnums.ReportParameter.Key:
                    this.ucKeyAccount.Visible = true;
                    break;
                case (short)WebEnums.ReportParameter.RiskRating:
                    this.ucRiskRating.Visible = true;
                    this.ucRiskRating.CBLDataSource = ReportHelper.GetListItemCollectionForRiskRating();
                    this.ucRiskRating.isRequired = isRequired;
                    break;
                case (short)WebEnums.ReportParameter.Material:
                    this.ucMaterialAccount.Visible = true;
                    break;
                case (short)WebEnums.ReportParameter.User:
                    this.ucRoleUser.Visible = true;
                    List<short> PermittedRoles = ReportHelper.GetPermittedRolesByReportID(_reportID.Value, SessionHelper.CurrentRoleID.Value, recPeriodID);
                    this.ucRoleUser.DataSourceForRolesList = ReportHelper.GetListItemCollectionForRoleByPermittedRoles(PermittedRoles);
                    //this.ucRoleUser.DataSourceForRolesList = ReportHelper.GetListItemCollectionForRole();
                    break;
                case (short)WebEnums.ReportParameter.Preparer:
                    this.ucPreparer.Visible = true;
                    this.ucPreparer.DataSourceForRolesList = ReportHelper.GetListItemCollectionForPreparer();
                    break;
                case (short)WebEnums.ReportParameter.OpenItemClassification:
                    this.ucOpenItemClassification.Visible = true;
                    this.ucOpenItemClassification.CBLDataSource = ReportHelper.GetListItemCollectionForOpenItemClassification();
                    this.ucOpenItemClassification.isRequired = isRequired;
                    break;
                case (short)WebEnums.ReportParameter.OpenDate:
                    this.ucOpenDate.Visible = true;
                    break;
                case (short)WebEnums.ReportParameter.CloseDate:
                    this.ucCloseDate.Visible = true;
                    break;
                case (short)WebEnums.ReportParameter.CreationDate:
                    this.ucDateCreated.Visible = true;
                    break;
                case (short)WebEnums.ReportParameter.ChangeDate:
                    this.ucChangeDate.Visible = true;
                    break;
                case (short)WebEnums.ReportParameter.Aging:
                    this.ucAging.Visible = true;
                    this.ucAging.CBLDataSource = ReportHelper.GetListItemCollectionForAging();
                    this.ucAging.isRequired = isRequired;
                    break;
                case (short)WebEnums.ReportParameter.RecStatus:
                    this.ucRecStatus.Visible = true;
                    this.ucRecStatus.CBLDataSource = ReportHelper.GetListItemCollectionForRecStatus();
                    this.ucRecStatus.isRequired = isRequired;
                    break;
                case (short)WebEnums.ReportParameter.TypeOfException:
                    this.ucExceptionType.Visible = true;
                    this.ucExceptionType.isRequired = isRequired;
                    this.ucExceptionType.CBLDataSource = ReportHelper.GetListItemCollectionForExceptionType();
                    break;
                case (short)WebEnums.ReportParameter.ChecklistItem:
                    this.ucChecklistItem.Visible = true;
                    this.ucChecklistItem.isRequired = isRequired;
                    this.ucChecklistItem.CBLDataSource = ReportHelper.GetListItemCollectionForQualityScoreChecklist(recPeriodID);
                    break;
                case (short)WebEnums.ReportParameter.SystemScore:
                    this.ucSystemScoreRange.Visible = true;
                    this.ucSystemScoreRange.isRequired = isRequired;
                    break;
                case (short)WebEnums.ReportParameter.UserScore:
                    this.ucUserScoreRange.Visible = true;
                    this.ucUserScoreRange.isRequired = isRequired;
                    break;
                case (short)WebEnums.ReportParameter.AsOnDate:
                    this.ucSingleDate.Visible = true;
                    break;
                case (short)WebEnums.ReportParameter.TaskStatus:
                    this.ucTaskStatus.Visible = true;
                    this.ucTaskStatus.isRequired = isRequired;
                    this.ucTaskStatus.CBLDataSource = ReportHelper.GetListItemCollectionForTaskStatus();
                    break;
                case (short)WebEnums.ReportParameter.TaskListName:
                    this.ucTaskListName.Visible = true;
                    this.ucTaskListName.isRequired = isRequired;
                    this.ucTaskListName.CBLDataSource = ReportHelper.GetListItemCollectionForTaskListName(recPeriodID);
                    break;
                case (short)WebEnums.ReportParameter.TaskType:
                    this.ucTaskType.Visible = true;
                    this.ucTaskType.isRequired = isRequired;
                    ReportHelper.BindDropDownListForTaskType(this.ucTaskType.DropDownListControl);
                    break;
                case (short)WebEnums.ReportParameter.DisplayColumn:
                    this.ucDisplayColumn.Visible = true;
                    this.ucDisplayColumn.isRequired = isRequired;
                    this.ucDisplayColumn.CBLDataSource = ReportHelper.GetListItemCollectionForDisplayColumn(_reportID.Value, true, false);
                    ucDisplayColumn.ClearSelection();
                    break;
            }
        }
    }

    private void LoadDataInRptCriteriaControlsFromSession(Dictionary<string, string> oRptCriteriaCollection)
    {
        if (this.ucOrgHierarchy.Visible)
            this.ucOrgHierarchy.SetCriteria(oRptCriteriaCollection);

        if (this.ucFinancialYear.Visible)
        {
            this.ucFinancialYear.SetCriteria(oRptCriteriaCollection, ReportCriteriaKeyName.RPTCRITERIAKEYNAME_FINANCIAL_YEAR);

            // Load the Rec Periods based on the Selected FY
            this.ucRecPeriod.ReloadRecPeriods(this.ucFinancialYear.FinancialYearID);
            if (_reportID == 2)
            {
                ListItem currentItem = new ListItem(WebConstants.CURRENT_REC_PERIOD, WebConstants.CURRENT_REC_PERIOD_INDEX);
                this.ucRecPeriod.RecPeriodDropDown.Items.Insert(0, currentItem);
            }
        }

        if (this.ucRecPeriod.Visible)
            this.ucRecPeriod.SetCriteria(oRptCriteriaCollection, ReportCriteriaKeyName.RPTCRITERIAKEYNAME_RECPERIOD);

        if (this.ucKeyAccount.Visible)
            this.ucKeyAccount.SetCriteria(oRptCriteriaCollection, ReportCriteriaKeyName.RPTCRITERIAKEYNAME_ISKEYACCOUNT);

        if (this.ucReasonCode.Visible)
            this.ucReasonCode.SetCriteria(oRptCriteriaCollection, ReportCriteriaKeyName.RPTCRITERIAKEYNAME_REASON);

        if (this.ucMaterialAccount.Visible)
            this.ucMaterialAccount.SetCriteria(oRptCriteriaCollection, ReportCriteriaKeyName.RPTCRITERIAKEYNAME_ISMATERIALACCOUNT);

        if (this.ucRiskRating.Visible)
            this.ucRiskRating.SetCriteria(oRptCriteriaCollection, ReportCriteriaKeyName.RPTCRITERIAKEYNAME_RISKRATING);

        if (this.ucOpenItemClassification.Visible)
            this.ucOpenItemClassification.SetCriteria(oRptCriteriaCollection, ReportCriteriaKeyName.RPTCRITERIAKEYNAME_OPENITEMCLASSIFICATION);

        if (this.ucAccountRange.Visible)
        {
            this.ucAccountRange.SetFromCriteria(oRptCriteriaCollection, ReportCriteriaKeyName.RPTCRITERIAKEYNAME_FROMACCOUNT);
            this.ucAccountRange.SetToCriteria(oRptCriteriaCollection, ReportCriteriaKeyName.RPTCRITERIAKEYNAME_TOACCOUNT);
        }

        if (this.ucCloseDate.Visible)
        {
            this.ucCloseDate.SetFromDateCriteria(oRptCriteriaCollection, ReportCriteriaKeyName.RPTCRITERIAKEYNAME_FROMCLOSEDATE);
            this.ucCloseDate.SetToDateCriteria(oRptCriteriaCollection, ReportCriteriaKeyName.RPTCRITERIAKEYNAME_TOCLOSEDATE);
        }

        if (this.ucOpenDate.Visible)
        {
            this.ucOpenDate.SetFromDateCriteria(oRptCriteriaCollection, ReportCriteriaKeyName.RPTCRITERIAKEYNAME_FROMOPENDATE);
            this.ucOpenDate.SetToDateCriteria(oRptCriteriaCollection, ReportCriteriaKeyName.RPTCRITERIAKEYNAME_TOOPENDATE);
        }

        if (this.ucDateCreated.Visible)
        {
            this.ucDateCreated.SetFromDateCriteria(oRptCriteriaCollection, ReportCriteriaKeyName.RPTCRITERIAKEYNAME_FROMDATECREATED);
            this.ucDateCreated.SetToDateCriteria(oRptCriteriaCollection, ReportCriteriaKeyName.RPTCRITERIAKEYNAME_TODATECREATED);
        }

        if (this.ucChangeDate.Visible)
        {
            this.ucChangeDate.SetFromDateCriteria(oRptCriteriaCollection, ReportCriteriaKeyName.RPTCRITERIAKEYNAME_FROMCHANGEDATE);
            this.ucChangeDate.SetToDateCriteria(oRptCriteriaCollection, ReportCriteriaKeyName.RPTCRITERIAKEYNAME_TOCHANGEDATE);
        }

        if (this.ucRoleUser.Visible)
        {
            this.ucRoleUser.SetRoleCriteria(oRptCriteriaCollection, ReportCriteriaKeyName.RPTCRITERIAKEYNAME_ROLE);
            this.FetchClicked(this.ucRoleUser.GetSelectedRoles);
            this.ucRoleUser.SetUserCriteria(oRptCriteriaCollection, ReportCriteriaKeyName.RPTCRITERIAKEYNAME_USER);

        }

        if (this.ucAging.Visible)
        {
            this.ucAging.SetCriteria(oRptCriteriaCollection, ReportCriteriaKeyName.RPTCRITERIAKEYNAME_AGING);
        }

        if (this.ucPreparer.Visible)
        {
            //this.ucPreparer.SetRoleCriteria(oRptCriteriaCollection, ReportCriteriaKeyName.RPTCRITERIAKEYNAME_PREPARERROLE);
            //this.FetchClickedPreparer(this.ucPreparer.GetSelectedRoles);
            //this.ucPreparer.SetUserCriteria(oRptCriteriaCollection, ReportCriteriaKeyName.RPTCRITERIAKEYNAME_PREPARERUSER);
            this.ucPreparer.SetRoleCriteria(oRptCriteriaCollection, ReportCriteriaKeyName.RPTCRITERIAKEYNAME_ROLE);
            this.FetchClickedPreparer(this.ucPreparer.GetSelectedRoles);
            this.ucPreparer.SetUserCriteria(oRptCriteriaCollection, ReportCriteriaKeyName.RPTCRITERIAKEYNAME_USER);


        }

        if (this.ucRecStatus.Visible)
        {
            this.ucRecStatus.SetCriteria(oRptCriteriaCollection, ReportCriteriaKeyName.RPTCRITERIAKEYNAME_RECSTATUS);
        }

        if (this.ucExceptionType.Visible)
        {
            this.ucExceptionType.SetCriteria(oRptCriteriaCollection, ReportCriteriaKeyName.RPTCRITERIAKEYNAME_TYPEOFEXCEPTION);
        }
        if (this.ucSingleDate.Visible)
        {
            this.ucSingleDate.SetCriteria(oRptCriteriaCollection, ReportCriteriaKeyName.RPTCRITERIAKEYNAME_ASONDATE);
        }
        if (this.ucUserScoreRange.Visible)
        {
            this.ucUserScoreRange.SetFromCriteria(oRptCriteriaCollection, ReportCriteriaKeyName.RPTCRITERIAKEYNAME_FROMUSERSCORE);
            this.ucUserScoreRange.SetToCriteria(oRptCriteriaCollection, ReportCriteriaKeyName.RPTCRITERIAKEYNAME_TOUSERSCORE);
        }
        if (this.ucSystemScoreRange.Visible)
        {
            this.ucSystemScoreRange.SetFromCriteria(oRptCriteriaCollection, ReportCriteriaKeyName.RPTCRITERIAKEYNAME_FROMSYSTEMSCORE);
            this.ucSystemScoreRange.SetToCriteria(oRptCriteriaCollection, ReportCriteriaKeyName.RPTCRITERIAKEYNAME_TOSYSTEMSCORE);
        }
        if (this.ucChecklistItem.Visible)
        {
            this.ucChecklistItem.SetCriteria(oRptCriteriaCollection, ReportCriteriaKeyName.RPTCRITERIAKEYNAME_CHECKLISTITEM);
        }
        if (this.ucTaskStatus.Visible)
        {
            this.ucTaskStatus.SetCriteria(oRptCriteriaCollection, ReportCriteriaKeyName.RPTCRITERIAKEYNAME_TASKSTATUS);
        }
        if (this.ucTaskListName.Visible)
        {
            this.ucTaskListName.SetCriteria(oRptCriteriaCollection, ReportCriteriaKeyName.RPTCRITERIAKEYNAME_TASKLISTNAME);
        }
        if (this.ucTaskType.Visible)
        {
            this.ucTaskType.SetCriteria(oRptCriteriaCollection, ReportCriteriaKeyName.RPTCRITERIAKEYNAME_TASKTYPE);
        }
        if (this.ucDisplayColumn.Visible)
        {
            if (ucTaskType.Visible && ucTaskType.SelectedValue != null)
            {
                short TaskTypeID;
                TaskTypeID = Convert.ToInt16(ucTaskType.SelectedValue);
                ShowHideAccountTaskControls(TaskTypeID, true);
            }
            this.ucDisplayColumn.SetCriteria(oRptCriteriaCollection, ReportCriteriaKeyName.RPTCRITERIAKEYNAME_DISPLAYCOLUMN);
        }
    }

    private void HideRptCriteriaControls(bool bVisible)
    {
        this.ucOrgHierarchy.Visible = bVisible;
        this.ucAccountRange.Visible = bVisible;
        this.ucReasonCode.Visible = bVisible;
        this.ucFinancialYear.Visible = bVisible;
        this.ucRecPeriod.Visible = bVisible;
        this.ucKeyAccount.Visible = bVisible;
        this.ucRiskRating.Visible = bVisible;
        this.ucMaterialAccount.Visible = bVisible;
        this.ucRoleUser.Visible = bVisible;
        this.ucPreparer.Visible = bVisible;
        this.ucOpenItemClassification.Visible = bVisible;
        this.ucOpenDate.Visible = bVisible;
        this.ucCloseDate.Visible = bVisible;
        this.ucDateCreated.Visible = bVisible;
        this.ucChangeDate.Visible = bVisible;
        this.ucAging.Visible = bVisible;
        this.ucRecStatus.Visible = bVisible;
        this.ucExceptionType.Visible = bVisible;
        this.ucChecklistItem.Visible = bVisible;
        this.ucSystemScoreRange.Visible = bVisible;
        this.ucUserScoreRange.Visible = bVisible;
        this.ucSingleDate.Visible = bVisible;
        this.ucTaskStatus.Visible = bVisible;
        this.ucTaskListName.Visible = bVisible;
        this.ucDisplayColumn.Visible = bVisible;
        this.ucTaskType.Visible = bVisible;
    }

    private void PopulatePage()
    {
        this.HideRptCriteriaControls(false);
        List<ReportParameterInfo> oReportParams = ReportHelper.GetRptParamControls(_reportID.Value);
        this.ShowHideRptCriteriaControls(oReportParams);
    }


    #endregion

    #region "Public Methods"
    public override string GetMenuKey()
    {
        return "";
    }
    #endregion
}//end of class
