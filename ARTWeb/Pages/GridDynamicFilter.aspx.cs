using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SkyStem.ART.Client.Data;
using SkyStem.ART.Web.Utility;
using SkyStem.ART.Web.Classes;
using SkyStem.ART.Web.Data;
using SkyStem.ART.Client.Exception;
using System.Web.UI.HtmlControls;
using SkyStem.ART.Client.Model;
using SkyStem.Library.Controls.WebControls;
using System.Text;
using SkyStem.ART.Client.Model.Matching;
using SkyStem.Language.LanguageUtility;

public partial class GridDynamicFilter : PopupPageBase
{

    #region Variables & Constants
    string sessionKey = string.Empty;
    List<string> lstCriterias = null;
    string sessionKeyFilterInfo = string.Empty;
    private const string DEFAULT_STRING_FOR_SEARCH = "No Records found";
    #endregion

    #region Properties
    List<FilterInfo> _lstFilterInfo = null;
    List<FilterInfo> _tempFilterInfo = null;
    /// <summary>
    /// Gets and Sets the FilterInfo list
    /// </summary>
    private List<FilterInfo> SetFilterInfo
    {
        set
        {
            _lstFilterInfo = new List<FilterInfo>();
            _lstFilterInfo = value;
            _tempFilterInfo = value;
            ViewState["ColumnsInfo"] = _lstFilterInfo;
        }
    }
    #endregion

    #region Delegates & Events
    #endregion

    #region Page Events
    protected void Page_Load(object sender, EventArgs e)
    {
        PopupHelper.SetPageTitle(this, 2385);
        PopupHelper.ShowInputRequirementSection(this, 1971, 1972);
        MasterPage oMasterPage = this.Master;
        //ScriptManager oScriptManager = (ScriptManager)oMasterPage.FindControl("_scriptManager");
        ScriptManager oScriptManager = ScriptManager.GetCurrent(this.Page);
        oScriptManager.RegisterPostBackControl(this.btnApplyFilter);


        if (!string.IsNullOrEmpty(Request.QueryString[QueryStringConstants.GRID_DYNAMIC_FILTER_SESSION_KEY]))
        {
            sessionKey = Request.QueryString[QueryStringConstants.GRID_DYNAMIC_FILTER_SESSION_KEY].ToString();
            if (ViewState["ColumnsInfo"] != null)
            {
                _lstFilterInfo = ViewState["ColumnsInfo"] as List<FilterInfo>;
            }
            if (ViewState["tempColumnInfo"] != null)
            {
                _tempFilterInfo = ViewState["tempColumnInfo"] as List<FilterInfo>;
            }
            if (!IsPostBack)
            {

                SetFilterInfo = SessionHelper.GetDynamicFilterColumns(sessionKey);
                if (_lstFilterInfo != null)
                {
                    _tempFilterInfo = new List<FilterInfo>(_lstFilterInfo);
                    ViewState["tempColumnInfo"] = _tempFilterInfo;
                    this.btnDummy.Style.Add(HtmlTextWriterStyle.Visibility, "hidden");
                    this.ShowHideFilterControls(false);
                    this.PopulateColumnNameDropDownList();
                    this.PopulateOperatorDropDown();
                    if (SessionHelper.GetDynamicFilterResult(sessionKey) != null)
                        lstCriterias = SessionHelper.GetDynamicFilterResult(sessionKey);
                    //this.lblSelectedClause.Text = UpdateSelectedClause();
                    if (this.ddlOperatorName.SelectedValue != "")
                        this.ShowFilterControlByOperatorID(Convert.ToInt16(ddlOperatorName.SelectedValue));
                }
            }
        }

    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        this.ReadFromSessionAndPopulateTable();
        this.btnAddMore.Enabled = (this.ddlFieldName.Items.Count > 0);
        if (ddlFieldName.Items.Count <= 0)
            ddlOperatorName.Items.Clear();
    }
    #endregion

    #region Grid Events

    #endregion

    #region Other Events
    protected void btnAddMore_Click(object sender, EventArgs e)
    {

        AddFilter();
    }

    protected void btnDummy_Click(object sender, EventArgs e)
    {
        short columnID;
        if (Int16.TryParse(hdnField.Value, out columnID))
        {
            DeleteCriteriaFromSession(columnID);

            this.PopulateColumnNameDropDownList();
            this.PopulateOperatorDropDown();
            if (ddlOperatorName.SelectedValue != "")
                this.ShowFilterControlByOperatorID(Convert.ToInt16(ddlOperatorName.SelectedValue));
            //lblSelectedClause.Text = UpdateSelectedClause();
        }
        //Delete this value from session

    }
    protected void btnApplyFilter_Click(object sender, EventArgs e)
    {
        if (!this.isFilterCriteriaAvailable())
            AddFilter();
        SessionHelper.SetDynamicFilterResultWhereClause(GetWhereClause(), sessionKey);
        if (this.isFilterCriteriaAvailable())
        {
            if (!string.IsNullOrEmpty(Request.QueryString[QueryStringConstants.IS_FOR_POPUP]))
                this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "CallApplyFilterAndClosePopup", ScriptHelper.CallApplyFilterAndClosePopup(true));
            else
                this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "ClosePopupAndRefreshParentPage", ScriptHelper.GetJSForClosePopupAndSubmitParentPage(true));
        }

    }
    protected void ddlFieldName_SelectedIndexChanged(object sender, EventArgs e)
    {
        PopupHelper.HideErrorMessage(this);
        this.PopulateOperatorDropDown();
        ShowFilterControlByOperatorID(Convert.ToInt16(ddlOperatorName.SelectedValue));
    }

    protected void ddlOperatorName_SelectedIndexChanged(object sender, EventArgs e)
    {
        short operatorID = Convert.ToInt16(ddlOperatorName.SelectedValue);
        short dataTypeID = GetDataTypeIDByColumnID(Convert.ToInt16(ddlFieldName.SelectedValue));

        if (ddlOperatorName.SelectedValue != "")
            ShowFilterControlByOperatorID(operatorID);
    }
    #endregion

    #region Validation Control Events
    #endregion

    #region Private Methods
    private void AddFilter()
    {
        short columnID;
        short dataType;
        short operatorID = 0;
        string columnName = string.Empty;
        string value1 = string.Empty;
        string value2 = string.Empty;
        string displayValue = string.Empty;

        try
        {
            PopupHelper.HideErrorMessage(this);
            if (ddlFieldName.SelectedValue != "")
            {
                columnID = Convert.ToInt16(ddlFieldName.SelectedValue);
                dataType = GetDataTypeIDByColumnID(Convert.ToInt16(ddlFieldName.SelectedValue));

                if (dataType == Convert.ToInt16(WebEnums.DataType.Integer) ||
                    dataType == Convert.ToInt16(WebEnums.DataType.Decimal))
                {
                    if (!Helper.IsDecimal(acctfltrEqual.Text))
                        throw new Exception(LanguageUtil.GetValue(5000093));

                }

                if (dataType == Convert.ToInt16(WebEnums.DataType.Integer) ||
                    dataType == Convert.ToInt16(WebEnums.DataType.Decimal) ||
                    dataType == Convert.ToInt16(WebEnums.DataType.String)
                    )
                {

                    if (ddlOperatorName.SelectedValue != "")
                    {

                        operatorID = Convert.ToInt16(ddlOperatorName.SelectedValue);
                        value1 = acctfltrEqual.Text;
                        displayValue = value1;
                    }
                }
                else if (dataType == Convert.ToInt16(WebEnums.DataType.DataTime))
                {
                    if (acctfltrDateRange.GetCurrentFromDate == "" || acctfltrDateRange.GetCurrentToDate == "")
                    {
                        throw new Exception(LanguageUtil.GetValue(5000261));
                    }
                    DateTime CurrentFromDate;
                    DateTime CurrentTODate;
                    if (!DateTime.TryParse(acctfltrDateRange.GetCurrentFromDate, out CurrentFromDate))
                        throw new Exception(Helper.GetErrorMessage(WebEnums.FieldType.DateFormatField, 1336));
                    if (!DateTime.TryParse(acctfltrDateRange.GetCurrentToDate, out CurrentTODate))
                        throw new Exception(Helper.GetErrorMessage(WebEnums.FieldType.DateFormatField, 1345));



                    operatorID = Convert.ToInt16(ddlOperatorName.SelectedValue);
                    value1 = acctfltrDateRange.GetCurrentFromDate;
                    value2 = acctfltrDateRange.GetCurrentToDate;
                    displayValue = value1 + " - " + value2;
                }
                // Commnebted by manoj : Add Display Name Column
                //columnName = ddlFieldName.SelectedItem.Text;
                short ColunmID;
                short.TryParse(ddlFieldName.SelectedValue, out ColunmID);
                columnName = this.GetColumnNameByColunmID(ColunmID);
                AddCriteriaToSession(columnName, columnID, operatorID, value1, value2, displayValue);
                ShowHideFilterControls(false);
                ClearCriteriaControls();

                // lblSelectedClause.Text = UpdateSelectedClause();

                PopulateColumnNameDropDownList();
                PopulateOperatorDropDown();
                if (ddlOperatorName.SelectedValue != "" && ddlFieldName.SelectedValue != "")
                    ShowFilterControlByOperatorID(Convert.ToInt16(ddlOperatorName.SelectedValue));
                this.acctfltrDateRange.SetFromDate = default(DateTime);
                this.acctfltrDateRange.SetToDate = default(DateTime);
            }
        }
        catch (Exception ex)
        {
            PopupHelper.ShowErrorMessage(this, ex);
        }
    }

    /// <summary>
    /// Populates the column name drop down list
    /// </summary>
    private void PopulateColumnNameDropDownList()
    {
        List<FilterCriteria> lstFilterCriteria;
        lstFilterCriteria = SessionHelper.GetDynamicFilterCriteria(sessionKey);
        List<FilterInfo> oFieldCollection = new List<FilterInfo>(_tempFilterInfo);
        if (lstFilterCriteria != null && lstFilterCriteria.Count > 0)
        {
            foreach (FilterCriteria fcr in lstFilterCriteria)
            {
                FilterInfo oFilterInfo = oFieldCollection.Find(r => r.ColumnID == fcr.ParameterID);
                if (oFilterInfo != null)
                {
                    oFieldCollection.Remove(oFilterInfo);
                }
            }
        }
        BindDropDownList(oFieldCollection);
        if (oFieldCollection == null || oFieldCollection.Count < 1)
            ddlOperatorName.Items.Clear();
    }

    /// <summary>
    /// Binds the column drop down list
    /// </summary>
    /// <param name="_lstFilterInfo"></param>
    private void BindDropDownList(List<FilterInfo> _lstFilterInfo)
    {
        if (_lstFilterInfo.Count > 0)
        {
            ddlFieldName.DataSource = _lstFilterInfo.OrderByDescending(p => p.DataType);
            ddlFieldName.DataTextField = "DisplayColumnName";
            ddlFieldName.DataValueField = "ColumnID";
            ddlFieldName.DataBind();
        }
        else
            ddlFieldName.Items.Clear();



    }

    /// <summary>
    /// Clears the critaria controls
    /// </summary>
    private void ClearCriteriaControls()
    {
        this.acctfltrEqual.Text = string.Empty;
    }

    /// <summary>
    /// populates operators names in drop down for filter
    /// </summary>
    private void PopulateOperatorDropDown()
    {
        List<OperatorMstInfo> lstOperatorList = null;

        try
        {
            if (ddlFieldName.SelectedItem != null && !string.IsNullOrEmpty(ddlFieldName.SelectedItem.Text))
            {
                PopupHelper.HideErrorMessage(this);
                short selectedValue = GetDataTypeIDByColumnID(Convert.ToInt16(ddlFieldName.SelectedValue));
                lstOperatorList = AccountFilterHelper.GetOperatorsByDynamicColumnID(selectedValue);
                ddlOperatorName.DataSource = lstOperatorList;
                ddlOperatorName.DataTextField = "OperatorName";
                ddlOperatorName.DataValueField = "OperatorID";
                ddlOperatorName.DataBind();
                //if (selectedValue > 0)
                //{
                //    lstOperatorList = new List<OperatorMstInfo>();

                //    if (selectedValue == Convert.ToInt16(WebEnums.DataType.String))
                //    {
                //        oOperatorMstInfo = new OperatorMstInfo();
                //        oOperatorMstInfo.OperatorID = Convert.ToInt16(WebEnums.Operator.Equals);
                //        oOperatorMstInfo.OperatorName = WebEnums.Operator.Equals.ToString();
                //        lstOperatorList.Add(oOperatorMstInfo);

                //        oOperatorMstInfo = new OperatorMstInfo();
                //        oOperatorMstInfo.OperatorID = Convert.ToInt16(WebEnums.Operator.Contains);
                //        oOperatorMstInfo.OperatorName = WebEnums.Operator.Contains.ToString();
                //        lstOperatorList.Add(oOperatorMstInfo);

                //    }
                //    else if (selectedValue == Convert.ToInt16(WebEnums.DataType.Integer))
                //    {
                //        oOperatorMstInfo = new OperatorMstInfo();
                //        oOperatorMstInfo.OperatorID = Convert.ToInt16(WebEnums.Operator.Equals);
                //        oOperatorMstInfo.OperatorName = WebEnums.Operator.Equals.ToString();
                //        lstOperatorList.Add(oOperatorMstInfo);

                //        oOperatorMstInfo = new OperatorMstInfo();
                //        oOperatorMstInfo.OperatorID = Convert.ToInt16(WebEnums.Operator.GreaterThan);
                //        oOperatorMstInfo.OperatorName = WebEnums.Operator.GreaterThan.ToString();
                //        lstOperatorList.Add(oOperatorMstInfo);

                //        oOperatorMstInfo = new OperatorMstInfo();
                //        oOperatorMstInfo.OperatorID = Convert.ToInt16(WebEnums.Operator.GreaterThanEqualTo);
                //        oOperatorMstInfo.OperatorName = WebEnums.Operator.GreaterThanEqualTo.ToString();
                //        lstOperatorList.Add(oOperatorMstInfo);

                //        oOperatorMstInfo = new OperatorMstInfo();
                //        oOperatorMstInfo.OperatorID = Convert.ToInt16(WebEnums.Operator.LessThan);
                //        oOperatorMstInfo.OperatorName = WebEnums.Operator.LessThan.ToString();
                //        lstOperatorList.Add(oOperatorMstInfo);

                //        oOperatorMstInfo = new OperatorMstInfo();
                //        oOperatorMstInfo.OperatorID = Convert.ToInt16(WebEnums.Operator.LessThanEqualTo);
                //        oOperatorMstInfo.OperatorName = WebEnums.Operator.LessThanEqualTo.ToString();
                //        lstOperatorList.Add(oOperatorMstInfo);

                //    }
                //    else if (selectedValue == Convert.ToInt16(WebEnums.DataType.Decimal))
                //    {
                //        oOperatorMstInfo = new OperatorMstInfo();
                //        oOperatorMstInfo.OperatorID = Convert.ToInt16(WebEnums.Operator.Equals);
                //        oOperatorMstInfo.OperatorName = WebEnums.Operator.Equals.ToString();
                //        lstOperatorList.Add(oOperatorMstInfo);

                //        oOperatorMstInfo = new OperatorMstInfo();
                //        oOperatorMstInfo.OperatorID = Convert.ToInt16(WebEnums.Operator.GreaterThan);
                //        oOperatorMstInfo.OperatorName = WebEnums.Operator.GreaterThan.ToString();
                //        lstOperatorList.Add(oOperatorMstInfo);

                //        oOperatorMstInfo = new OperatorMstInfo();
                //        oOperatorMstInfo.OperatorID = Convert.ToInt16(WebEnums.Operator.GreaterThanEqualTo);
                //        oOperatorMstInfo.OperatorName = WebEnums.Operator.GreaterThanEqualTo.ToString();
                //        lstOperatorList.Add(oOperatorMstInfo);

                //        oOperatorMstInfo = new OperatorMstInfo();
                //        oOperatorMstInfo.OperatorID = Convert.ToInt16(WebEnums.Operator.LessThan);
                //        oOperatorMstInfo.OperatorName = WebEnums.Operator.LessThan.ToString();
                //        lstOperatorList.Add(oOperatorMstInfo);

                //        oOperatorMstInfo = new OperatorMstInfo();
                //        oOperatorMstInfo.OperatorID = Convert.ToInt16(WebEnums.Operator.LessThanEqualTo);
                //        oOperatorMstInfo.OperatorName = WebEnums.Operator.LessThanEqualTo.ToString();
                //        lstOperatorList.Add(oOperatorMstInfo);

                //    }
                //    else if (selectedValue == Convert.ToInt16(WebEnums.DataType.DataTime))
                //    {
                //        oOperatorMstInfo = new OperatorMstInfo();
                //        oOperatorMstInfo.OperatorID = Convert.ToInt16(WebEnums.Operator.Between);
                //        oOperatorMstInfo.OperatorName = WebEnums.Operator.Between.ToString();
                //        lstOperatorList.Add(oOperatorMstInfo);
                //    }

            }
        }
        catch (Exception ex)
        {
            PopupHelper.ShowErrorMessage(this, ex);
        }
    }
    /// <summary>
    /// returns the operator sybmol to generateWhereClause() method depending on operator selected
    /// </summary>
    /// <param name="operatorID"></param>
    /// <returns></returns>
    private string GetOperatorSymbolByOperatorID(short operatorID)
    {
        string operatorSymb = string.Empty;
        switch (operatorID)
        {
            case (short)WebEnums.Operator.Between:
                operatorSymb = "between";
                break;
            case (short)WebEnums.Operator.Contains:
                operatorSymb = "like";
                break;
            case (short)WebEnums.Operator.Equals:
                operatorSymb = "=";
                break;
            case (short)WebEnums.Operator.GreaterThan:
                operatorSymb = ">";
                break;
            case (short)WebEnums.Operator.GreaterThanEqualTo:
                operatorSymb = ">=";
                break;
            case (short)WebEnums.Operator.LessThan:
                operatorSymb = "<";
                break;
            case (short)WebEnums.Operator.LessThanEqualTo:
                operatorSymb = "<=";
                break;
            case (short)WebEnums.Operator.NotEqualTo:
                operatorSymb = "!=";
                break;
        }
        return operatorSymb;
    }

    /// <summary>
    /// Generates filter clause on the basis of selected criteria
    /// </summary>
    /// <returns></returns>
    /// 
    private List<string> GetWhereClause()
    {
        StringBuilder whereClause = new StringBuilder();
        List<FilterCriteria> lstFilterCriteriaForApply;
        lstFilterCriteriaForApply = SessionHelper.GetDynamicFilterCriteria(sessionKey);
        string operatorSymbol = string.Empty;
        string value = string.Empty;
        string[] values;
        StringBuilder displayCriteria;
        List<string> lstCriterias = null;
        string colNm = string.Empty;
        if (lstFilterCriteriaForApply != null && lstFilterCriteriaForApply.Count > 0)
        {
            displayCriteria = new StringBuilder();
            lstCriterias = new List<string>();
            foreach (FilterCriteria filter in lstFilterCriteriaForApply)
            {
                operatorSymbol = GetOperatorSymbolByOperatorID(filter.OperatorID);
                colNm = GetColumnNameByColunmID(filter.ParameterID);

                if (filter.OperatorID == Convert.ToInt16(WebEnums.Operator.Contains))
                {
                    value = "%" + filter.Value + "%";
                }
                else
                    value = filter.Value;

                if (filter.ColumnType == (short)WebEnums.DataType.Integer)
                {
                    whereClause.Append("[").Append(colNm).Append("]").Append(" ").Append(operatorSymbol).Append(" ")
                        .Append(value).Append(" and ");
                    displayCriteria.Append(colNm).Append(" ").Append(GetOperatorNameByOperatorID(filter.OperatorID)).Append(" ")
                    .Append(filter.Value).Append(" and ");
                }
                else if (filter.ColumnType == (short)WebEnums.DataType.Decimal)
                {
                    whereClause.Append("[").Append(colNm).Append("]").Append(" ").Append(operatorSymbol).Append(" ")
                        .Append(value).Append(" and ");
                    displayCriteria.Append(colNm).Append(" ").Append(GetOperatorNameByOperatorID(filter.OperatorID)).Append(" ")
                    .Append(filter.Value).Append(" and ");
                }
                else if (filter.ColumnType == (short)WebEnums.DataType.String)
                {
                    whereClause.Append("[").Append(colNm).Append("]").Append(" ").Append(operatorSymbol).Append(" '")
                    .Append(value).Append("'").Append(" and ");
                    displayCriteria.Append(colNm).Append(" ").Append(GetOperatorNameByOperatorID(filter.OperatorID)).Append(" ")
                    .Append(filter.Value).Append(" and ");
                }
                else if (filter.ColumnType == (short)WebEnums.DataType.DataTime)
                {
                    values = filter.Value.Split('|');
                    //"[Period End Date] >= #6/10/2009# AND  [Period End Date] <= #6/10/2009# ";
                    whereClause.Append("[" + colNm + "]");
                    whereClause.Append(" >= #" + values[0] + "#");
                    whereClause.Append("  and [" + colNm + "]");
                    whereClause.Append(" <= #" + values[1] + "# and ");

                    //whereClause.Append("[").Append(colNm).Append("]").Append(" ").Append(operatorSymbol).Append(" ")
                    //.Append("'").Append(values[0]).Append("'").Append(" and '")
                    //    .Append(values[1]).Append("' and ");
                    displayCriteria.Append(colNm).Append(" ").Append(GetOperatorNameByOperatorID(filter.OperatorID)).Append(" ")
                    .Append(values[0]).Append(" and ").Append(values[1]).Append(" and ");


                }
            }
            lstCriterias.Add(displayCriteria.ToString().Substring(0, displayCriteria.Length - 5));
            lstCriterias.Add(whereClause.ToString().Substring(0, whereClause.Length - 5));
            return lstCriterias;
        }
        return null;
    }

    /// <summary>
    /// Gets column name by column id
    /// </summary>
    /// <param name="columnID"></param>
    /// <returns></returns>
    private string GetColumnNameByColunmID(short columnID)
    {
        string colName = string.Empty;
        if (_tempFilterInfo != null)
        {
            colName = _tempFilterInfo.Find(r => r.ColumnID == columnID).ColumnName.ToString();
        }
        return colName;
    }

    /// <summary>
    /// Gets column name by column id
    /// </summary>
    /// <param name="columnID"></param>
    /// <returns></returns>
    private string GetDisplayColumnNameByColunmID(short columnID)
    {
        string colName = string.Empty;
        if (_tempFilterInfo != null)
        {
            colName = _tempFilterInfo.Find(r => r.ColumnID == columnID).DisplayColumnName.ToString();
        }
        return colName;
    }


    /// <summary>
    /// returns true if the filter criteria is available in session
    /// </summary>
    /// <returns></returns>
    private bool isFilterCriteriaAvailable()
    {
        bool isCriteriaAvailable = false;
        List<FilterCriteria> oFltrCriteriaCollection = null;
        if (!string.IsNullOrEmpty(Request.QueryString[QueryStringConstants.GRID_DYNAMIC_FILTER_SESSION_KEY]))
        {
            if (SessionHelper.GetDynamicFilterCriteria(sessionKey) == null)
                isCriteriaAvailable = false;
            else
            {
                oFltrCriteriaCollection = SessionHelper.GetDynamicFilterCriteria(sessionKey);
                if (oFltrCriteriaCollection.Count > 0)
                    isCriteriaAvailable = true;
                else
                    isCriteriaAvailable = false;
            }
        }
        return isCriteriaAvailable;
    }

    /// <summary>
    /// Show or Hides Filter Controls on the basis of operator selected (operator id)
    /// </summary>
    /// <param name="operatorID"></param>
    private void ShowFilterControlByOperatorID(short operatorID)
    {
        this.ShowHideFilterControls(false);
        switch (operatorID)
        {
            case (short)WebEnums.Operator.Between:
                acctfltrDateRange.Visible = true;
                break;
            default:
                acctfltrEqual.Visible = true;
                acctfltrEqual.Text = "";
                break;
        }

    }

    /// <summary>
    /// Show or Hide filter controls.
    /// </summary>
    /// <param name="visibility"></param>
    private void ShowHideFilterControls(bool visibility)
    {
        this.acctfltrEqual.Visible = visibility;
        this.acctfltrDateRange.Visible = visibility;
    }

    /// <summary>
    /// Adding criteria to session
    /// </summary>
    /// <param name="columnID"></param>
    /// <param name="operatorID"></param>
    /// <param name="value1"></param>
    /// <param name="value2"></param>
    /// <param name="displayValue"></param>
    private void AddCriteriaToSession(string columnName, short columnID, short operatorID, string value1, string value2, string displayValue)
    {
        FilterCriteria criteria = new FilterCriteria();
        criteria.ColumnName = columnName;
        criteria.ParameterID = columnID;
        criteria.OperatorID = operatorID;
        string value = value1;
        if (value2 != "" && value2 != string.Empty)
        {
            value += ("|" + value2);
        }
        criteria.Value = value;
        criteria.DisplayValue = displayValue;
        criteria.ColumnType = GetDataTypeIDByColumnID(Convert.ToInt16(ddlFieldName.SelectedValue));
        List<FilterCriteria> lstFilterCriteriaCollection;
        lstFilterCriteriaCollection = SessionHelper.GetDynamicFilterCriteria(sessionKey);
        if (lstFilterCriteriaCollection == null)
        {
            lstFilterCriteriaCollection = new List<FilterCriteria>();
        }
        lstFilterCriteriaCollection.Add(criteria);
        SessionHelper.SetDynamicFilterCriteria(lstFilterCriteriaCollection, sessionKey);

    }

    /// <summary>
    /// Reading filters from session and populate the criteria table. Called on pre-render
    /// </summary>
    private void ReadFromSessionAndPopulateTable()
    {
        const string evenrowStyle = "TableAlternateRowSameAsGrid";
        const string oddRowStyle = "TableRowSameAsGrid";
        string currentStyle = "";
        if (!string.IsNullOrEmpty(Request.QueryString[QueryStringConstants.GRID_DYNAMIC_FILTER_SESSION_KEY]))
        {

            if (SessionHelper.GetDynamicFilterCriteria(sessionKey) != null)
            {
                List<FilterCriteria> oFltrCriteriaCollection = SessionHelper.GetDynamicFilterCriteria(sessionKey);
                ///if (oFltrCriteriaCollection != null && oFltrCriteriaCollection.Count > 0 && oFltrCriteriaCollection.FindAll(F => F.MatchingSourceDataImportID == this.MatchingSourceDataImportID).Count > 0)
                if (oFltrCriteriaCollection != null && oFltrCriteriaCollection.Count > 0)
                {
                    this.tblFltrCriteria.Visible = true;
                    //HtmlTable dTable = this.tblFltrCriteria as HtmlTable;
                    //foreach (FilterCriteria fltr in oFltrCriteriaCollection.FindAll(F => F.MatchingSourceDataImportID == this.MatchingSourceDataImportID).ToList())
                    foreach (FilterCriteria fltr in oFltrCriteriaCollection)
                    {
                        switch (currentStyle)
                        {
                            case "":
                                currentStyle = oddRowStyle;
                                break;
                            case oddRowStyle:
                                currentStyle = evenrowStyle;
                                break;
                            case evenrowStyle:
                                currentStyle = oddRowStyle;
                                break;
                        }


                        HtmlTableRow tr = new HtmlTableRow();
                        tr.Attributes.Add("class", currentStyle);

                        HtmlTableCell td1 = new HtmlTableCell();
                        ExLabel lblColumnName = new ExLabel();
                        lblColumnName.SkinID = "Black11Arial";
                        lblColumnName.Text = GetDisplayColumnNameByColunmID(fltr.ParameterID);
                        td1.Controls.Add(lblColumnName);

                        HtmlTableCell td2 = new HtmlTableCell();
                        ExLabel lblOperatorName = new ExLabel();
                        lblOperatorName.SkinID = "Black11Arial";
                        lblOperatorName.Text = GetOperatorNameByOperatorID(fltr.OperatorID);
                        td2.Controls.Add(lblOperatorName);

                        HtmlTableCell td3 = new HtmlTableCell();
                        ExLabel lblValue1 = new ExLabel();
                        lblValue1.SkinID = "Black11ArialNormal";
                        lblValue1.Text = fltr.DisplayValue;
                        td3.Controls.Add(lblValue1);

                        HtmlTableCell td4 = new HtmlTableCell();
                        td4.Align = HorizontalAlign.Center.ToString();
                        ExImageButton btnDelete = new ExImageButton();
                        btnDelete.SkinID = "DeleteIcon";
                        btnDelete.OnClientClick = "return deleteClick(this)";
                        btnDelete.Attributes.Add("columnID", fltr.ParameterID.ToString());
                        btnDelete.ToolTipLabelID = 2356;
                        td4.Controls.Add(btnDelete);

                        tr.Cells.Add(td1);
                        tr.Cells.Add(td2);
                        tr.Cells.Add(td3);
                        tr.Cells.Add(td4);
                        this.tblFltrCriteria.Rows.Add(tr);
                    }
                }
                else
                {
                    this.tblFltrCriteria.Visible = false;
                }
            }
            else
            {
                this.tblFltrCriteria.Visible = false;
            }
        }

    }

    /// <summary>
    /// Get operator name by operator Id
    /// </summary>
    /// <param name="operatorID"></param>
    /// <returns></returns>
    private string GetOperatorNameByOperatorID(short operatorID)
    {
        List<OperatorMstInfo> operatorlst = SessionHelper.GetOperatorList();
        String OperatorName = operatorlst.Find(CA => CA.OperatorID == operatorID).OperatorName;
        return OperatorName;
    }

    /// <summary>
    /// Delete Criteria from session by column Id
    /// </summary>
    /// <param name="columnID"></param>
    private void DeleteCriteriaFromSession(short columnID)
    {
        List<FilterCriteria> oFltrCriteriaCollection;
        oFltrCriteriaCollection = SessionHelper.GetDynamicFilterCriteria(sessionKey);
        SessionHelper.ClearDynamicFilterData(sessionKey);
        FilterCriteria oFltr = oFltrCriteriaCollection.Find(r => r.ParameterID == columnID);
        if (oFltr != null)
            oFltrCriteriaCollection.Remove(oFltr);
        SessionHelper.SetDynamicFilterCriteria(oFltrCriteriaCollection, sessionKey);
    }

    /// <summary>
    /// To Get Column Data type by columnID
    /// </summary>
    /// <param name="columnID"></param>
    /// <returns></returns>
    private short GetDataTypeIDByColumnID(short columnID)
    {
        short oprID = -1;

        if (_lstFilterInfo != null)
        {
            oprID = Convert.ToInt16(_lstFilterInfo.Find(r => r.ColumnID == columnID).DataType);
        }
        return oprID;
    }

    #endregion

    #region Other Methods
    string UpdateSelectedClause()
    {
        List<string> lst = GetWhereClause();
        if (lst != null)
        {
            return lst[1].ToString();
        }
        return string.Empty;
    }

    #endregion
}
