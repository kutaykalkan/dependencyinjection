using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SkyStem.ART.Web.Classes;
using SkyStem.ART.Web.Utility;
using SkyStem.Language.LanguageUtility;
using SkyStem.ART.Client.Model.Matching;
using SkyStem.ART.Web.Data;
using System.Text;
using SkyStem.ART.Client.Model;
using SkyStem.Library.Controls.WebControls;

public partial class RuleSetupPopup : PopupPageBase
{
    #region Private Variables

    MatchSetHdrInfo oMatchSetHdrInfo = null;
    MatchingConfigurationInfo oMatchingConfigurationInfo = new MatchingConfigurationInfo();
    List<MatchingConfigurationInfo> lstMatchingConfigurationInfoCollection = null;
    Int16 _columnDataType;
    Int64 _dataSourceCombinationID;
    Int64 _matchingConfigurationRuleID, ruleID;
    string[] cols2Values = null;
    Int64 column1ID = 0;
    string mode = string.Empty;
    long matchingSourceID = 0;
    //long matchingSourceRuleID = 0;
    #endregion

    #region Page Events
    protected void Page_Load(object sender, EventArgs e)
    {
        PopupHelper.SetPageTitle(this, 2235);
        MasterPage oMasterPage = this.Master;
        //ScriptManager oScriptManager = (ScriptManager)oMasterPage.FindControl("_scriptManager");
        ScriptManager oScriptManager = ScriptManager.GetCurrent(this.Page);
        oScriptManager.RegisterPostBackControl(this.btnSet);
        try
        {
            if (Session[SessionConstants.MATCHING_CONFIGURATION_DATA] != null)
            {
                lstMatchingConfigurationInfoCollection = Session[SessionConstants.MATCHING_CONFIGURATION_DATA] as List<MatchingConfigurationInfo>;
            }
            if (Request.QueryString["mode"] != null)
            {
                mode = Request.QueryString["mode"].ToString();
                if (mode == "edit")
                {
                    if (Request.QueryString["mcrID"] != null)           //MatchingConfigurationRuleID
                    {
                        _matchingConfigurationRuleID = Convert.ToInt64(Request.QueryString["mcrID"]);
                    }
                    if (Request.QueryString["ruleID"] != null)
                    {
                        ruleID = Convert.ToInt64(Request.QueryString["ruleID"]);//ruleID
                    }
                    if (Request.QueryString["mcID"] != null)
                    {
                        matchingSourceID = Convert.ToInt64(Request.QueryString["mcID"].ToString()); //MatchingSource1ColumnID
                    }
                }
                else
                {
                    if (Request.QueryString["ID"] != null)
                        column1ID = Convert.ToInt64(Request.QueryString["ID"]);
                    string str;
                    if (Request.QueryString["Col2"] != null)
                    {
                        str = Request.QueryString["Col2"].ToString();
                        cols2Values = GetColumn2Values(str);
                    }
                    if (Request.QueryString["msID"] != null)
                        matchingSourceID = Convert.ToInt64(Request.QueryString["msID"].ToString());

                }
                oMatchingConfigurationInfo = GetColumnInfo(matchingSourceID);
                if (oMatchingConfigurationInfo != null)
                {
                    _columnDataType = Convert.ToInt16(oMatchingConfigurationInfo.DataTypeID);
                    _dataSourceCombinationID = Convert.ToInt64(oMatchingConfigurationInfo.MatchSetSubSetCombinationID);
                }

            }
            SetEditModeOnControl();
        }
        catch (Exception ex)
        {
            PopupHelper.ShowErrorMessage(this, ex);
        }
        if (!IsPostBack)
        {
            PopulateThresHoldTypes();
            ShowControlsByDataType(_columnDataType);
            SetValuesToDisplay();
        }

    }
    private void SetEditModeOnControl()
    {
        if (Request.QueryString["IsEditMode"] != null)
        {
            if (!Convert.ToBoolean(Request.QueryString["IsEditMode"]))
            {
                tblMain.Disabled = true;
                btnSet.Enabled = false;
                txtStringValue.Enabled = false;
                ddlOperator.Enabled = false;
                ddlValueType.Enabled = false;
                luBoundCtrl.SetEnabled = false;
            }
        }
    }
    #endregion

    #region Private methods
    /// <summary>
    /// gets column2 name and column2 id passed as QueryString from parent page
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    string[] GetColumn2Values(string str)
    {
        string[] values = null;
        if (str != string.Empty || str != "")
        {
            values = str.Split('_');
        }
        return values;
    }

    /// <summary>
    /// returns MatchingConfigurationInfo object for Column1 ID
    /// </summary>
    /// <param name="col1ID"></param>
    /// <returns></returns>
    MatchingConfigurationInfo GetColumnInfo(Int64 mcID)
    {
        MatchingConfigurationInfo oMatchingConfigurationInfo = null;
        if (lstMatchingConfigurationInfoCollection != null)
        {
            oMatchingConfigurationInfo = new MatchingConfigurationInfo();
            if (mode == "add")
                oMatchingConfigurationInfo = lstMatchingConfigurationInfoCollection.Find(r => r.MatchingSource1ColumnID == mcID) as MatchingConfigurationInfo;
            else if (mode == "edit" && ruleID == 0)
                oMatchingConfigurationInfo = lstMatchingConfigurationInfoCollection.Find(r => r.MatchingConfigurationID == mcID) as MatchingConfigurationInfo;
            else
                oMatchingConfigurationInfo = lstMatchingConfigurationInfoCollection.Find(r => r.MatchingSource1ColumnID == mcID) as MatchingConfigurationInfo;
        }
        return oMatchingConfigurationInfo;
    }

    /// <summary>
    /// returns data source names
    /// </summary>
    /// <returns></returns>
    List<string> GetDataSourceNames(Int64 dataSourceCombinationID)
    {
        oMatchSetHdrInfo = SessionHelper.GetCurrentMatchSet();

        MatchSetSubSetCombinationInfo oMatchSetSubSetCombinationInfo = null;
        List<string> dsNames = new List<string>();
        string[] values = null;
        if (oMatchSetHdrInfo != null)
            oMatchSetSubSetCombinationInfo = oMatchSetHdrInfo.MatchSetSubSetCombinationInfoCollection.Find(r => r.MatchSetSubSetCombinationID == dataSourceCombinationID);

        if (oMatchSetSubSetCombinationInfo != null)
        {
            values = oMatchSetSubSetCombinationInfo.MatchSetSubSetCombinationName.Split('|');
            dsNames.Add(values[0].ToString());
            dsNames.Add(values[1].ToString());
        }
        return dsNames;
    }

    /// <summary>
    /// Shows and hides controls based on the columns data type
    /// </summary>
    void ShowControlsByDataType(Int16 columnDataType)
    {
        this.rowValueType.Visible = false;
        this.rowStrType.Visible = false;
        this.rowBounds.Visible = false;
        this.rowOperator.Visible = false;
        this.rowHint.Visible = false;

        if (columnDataType == Convert.ToInt16(WebEnums.DataType.String))
        {
            this.rowStrType.Visible = true;
            this.rowOperator.Visible = true;
            this.blankRow1.Visible = false;
            this.blankRow3.Visible = false;
            this.blankRow4.Visible = true;
            PopulateOperatorByDataType(WebEnums.DataType.String);
        }
        else if (columnDataType == Convert.ToInt16(WebEnums.DataType.Integer)
            || columnDataType == Convert.ToInt16(WebEnums.DataType.Decimal))
        {
            this.rowValueType.Visible = true;
            this.rowBounds.Visible = true;
            this.rowHint.Visible = true;
            PopulateOperatorByDataType(WebEnums.DataType.Integer);
        }
        else if (columnDataType == Convert.ToInt16(WebEnums.DataType.DataTime))
        {
            this.rowBounds.Visible = true;
            PopulateOperatorByDataType(WebEnums.DataType.DataTime);
        }

    }

    /// <summary>
    ///  Populate Operator dropdown based on DataTypeID
    /// </summary>
    /// <param name="type"></param>
    void PopulateOperatorByDataType(WebEnums.DataType type)
    {
        ddlOperator.Items.Clear();
        List<OperatorMstInfo> lstOperators = new List<OperatorMstInfo>();
        OperatorMstInfo oOperatorList = null;

        if (type == WebEnums.DataType.String)
        {
            oOperatorList = new OperatorMstInfo();
            oOperatorList.OperatorID = Convert.ToInt16(WebEnums.Operator.Equals);
            oOperatorList.OperatorName = WebEnums.Operator.Equals.ToString();
            lstOperators.Add(oOperatorList);

            oOperatorList = new OperatorMstInfo();
            oOperatorList.OperatorID = Convert.ToInt16(WebEnums.Operator.Contains);
            oOperatorList.OperatorName = WebEnums.Operator.Contains.ToString();
            lstOperators.Add(oOperatorList);
        }
        if (lstOperators != null)
        {
            ddlOperator.DataSource = lstOperators;
            ddlOperator.DataTextField = "OperatorName";
            ddlOperator.DataValueField = "OperatorID";
            ddlOperator.DataBind();
        }
    }
    /// <summary>
    /// set the values to display
    /// </summary>
    void SetValuesToDisplay()
    {
        StringBuilder cols = new StringBuilder();
        List<string> lstDataSourceNames = null;
        try
        {
            if (mode == "add")              // When we are adding new rule
            {
                lstDataSourceNames = GetDataSourceNames(_dataSourceCombinationID);
                if (lstDataSourceNames != null && lstDataSourceNames.Count > 0)
                {
                    lblDataSource1Value.Text = lstDataSourceNames[0].ToString();
                    lblDataSource2Value.Text = lstDataSourceNames[1].ToString();

                    if (oMatchingConfigurationInfo != null)
                    {
                        lblComparisonColumnValue.Text = GetColumnsCombinationString(oMatchingConfigurationInfo.ColumnName1, cols2Values[0].ToString().Trim());
                    }
                }
            }
            else
            {
                // When old rule is loaded
                ClearControls();
                MatchingConfigurationRuleInfo oMatchingConfigurationRuleInfo = null;
                Int64 datasourceID = 0;
                string col1Name = string.Empty;
                string col2Name = string.Empty;
               
                _columnDataType = Convert.ToInt16(oMatchingConfigurationInfo.DataTypeID);
                PopulateOperatorByDataType((WebEnums.DataType)oMatchingConfigurationInfo.DataTypeID);
                ShowControlsByDataType(Convert.ToInt16(oMatchingConfigurationInfo.DataTypeID));
                datasourceID = Convert.ToInt64(oMatchingConfigurationInfo.MatchSetSubSetCombinationID);
                col1Name = oMatchingConfigurationInfo.ColumnName1;
                col2Name = oMatchingConfigurationInfo.ColumnName2;
                column1ID = oMatchingConfigurationInfo.MatchingSource1ColumnID.Value;

                #region For New Rule
                if (_matchingConfigurationRuleID == 0)     //Rule is New
                {
                    foreach (MatchingConfigurationRuleInfo inf in oMatchingConfigurationInfo.MatchingConfigurationRuleInfoCollection)
                    {
                        if (inf.RuleID == ruleID)
                        {
                            oMatchingConfigurationRuleInfo = inf;
                            break;
                        }
                    }
                }
                #endregion

                #region For Existing Rules
                else
                {
                    foreach (MatchingConfigurationRuleInfo inf in oMatchingConfigurationInfo.MatchingConfigurationRuleInfoCollection)
                    {
                        if (inf.MatchingConfigurationRuleID == _matchingConfigurationRuleID)
                        {
                            oMatchingConfigurationRuleInfo = inf;
                            break;
                        }
                    }
                }
                #endregion

                if (oMatchingConfigurationRuleInfo != null)
                {
                    lstDataSourceNames = GetDataSourceNames(datasourceID);
                    if (lstDataSourceNames != null && lstDataSourceNames.Count > 0)
                    {
                        lblDataSource1Value.Text = lstDataSourceNames[0].ToString();
                        lblDataSource2Value.Text = lstDataSourceNames[1].ToString();
                        if (_columnDataType == Convert.ToInt16(WebEnums.DataType.String))
                        {
                            txtStringValue.Text = oMatchingConfigurationRuleInfo.Keywords;
                            if (oMatchingConfigurationRuleInfo.OperatorID == Convert.ToInt16(WebEnums.Operator.Equals))
                                ddlOperator.SelectedIndex = 0;
                            else
                                ddlOperator.SelectedIndex = 1;
                        }
                        else if (_columnDataType == Convert.ToInt16(WebEnums.DataType.Integer) ||
                            _columnDataType == Convert.ToInt16(WebEnums.DataType.Decimal))
                        {
                            luBoundCtrl.SetLowerBoundValue = oMatchingConfigurationRuleInfo.LowerBound.Value.ToString("#.##");
                            luBoundCtrl.SetUpperBoundValue = oMatchingConfigurationRuleInfo.UpperBound.Value.ToString("#.##");
                            PopulateThresHoldTypes();

                            if (oMatchingConfigurationRuleInfo.ThresholdTypeID != null && oMatchingConfigurationRuleInfo.ThresholdTypeID.Value == Convert.ToInt16(WebEnums.ThresholdType.Fixed))
                                ddlValueType.SelectedIndex = 0;
                            else
                                ddlValueType.SelectedIndex = 1;
                        }
                        else if (_columnDataType == Convert.ToInt16(WebEnums.DataType.DataTime))
                        {
                            luBoundCtrl.SetLowerBoundValue = oMatchingConfigurationRuleInfo.LowerBound.Value.ToString("#");
                            luBoundCtrl.SetUpperBoundValue = oMatchingConfigurationRuleInfo.UpperBound.Value.ToString("#");
                            PopulateThresHoldTypes();

                            if (oMatchingConfigurationRuleInfo.ThresholdTypeID != null && oMatchingConfigurationRuleInfo.ThresholdTypeID.Value == Convert.ToInt16(WebEnums.ThresholdType.Fixed))
                                ddlValueType.SelectedIndex = 0;
                            else
                                ddlValueType.SelectedIndex = 1;


                        }
                        lblComparisonColumnValue.Text = GetColumnsCombinationString(col1Name, col2Name);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            PopupHelper.ShowErrorMessage(this, ex);
        }
    }
    /// <summary>
    /// returns source1.columnName <=> source1.columnName for UI
    /// </summary>
    /// <param name="col1"></param>
    /// <param name="col2"></param>
    /// <returns></returns>
    private string GetColumnsCombinationString(string col1, string col2)
    {
        StringBuilder cols = new StringBuilder();
        cols.Append(LanguageUtil.GetValue(2382)).Append(".").Append(col1.Trim())
                            .Append(" <=> ").Append(LanguageUtil.GetValue(2383)).Append(".").Append(col2.Trim());
        return cols.ToString();
    }

    /// <summary>
    /// Populate thresholdtypes in dropdown
    /// </summary>
    void PopulateThresHoldTypes()
    {
        ddlValueType.Items.Clear();
        List<ThresholdTypeMst> lstThresHoldTypes = new List<ThresholdTypeMst>();

        ThresholdTypeMst oThresHoldTypes = new ThresholdTypeMst();
        oThresHoldTypes.ThresholdTypeID = Convert.ToInt16(WebEnums.ThresholdType.Fixed);
        oThresHoldTypes.ThresholdTypeName = WebEnums.ThresholdType.Fixed.ToString();
        lstThresHoldTypes.Add(oThresHoldTypes);

        oThresHoldTypes = new ThresholdTypeMst();
        oThresHoldTypes.ThresholdTypeID = Convert.ToInt16(WebEnums.ThresholdType.Percentage);
        oThresHoldTypes.ThresholdTypeName = WebEnums.ThresholdType.Percentage.ToString();
        lstThresHoldTypes.Add(oThresHoldTypes);

        ddlValueType.DataSource = lstThresHoldTypes;
        ddlValueType.DataTextField = "ThresHoldTypeName";
        ddlValueType.DataValueField = "ThresHoldTypeID";

        ddlValueType.DataBind();
    }
    void ClearControls()
    {
        txtStringValue.Text = "";
        ddlOperator.Items.Clear();
        ddlValueType.Items.Clear();
        luBoundCtrl.SetLowerBoundValue = "";
        luBoundCtrl.SetUpperBoundValue = "";
    }
    #endregion

    #region Control Events

    protected void btnSet_Click(object sender, EventArgs e)
    {
        Int16 operatorID = Convert.ToInt16(WebEnums.Operator.Equals);
        Int16 threasHoldTypeID = Convert.ToInt16(WebEnums.ThresholdType.Fixed);
        MatchingConfigurationRuleInfo oMatchingConfigurationRuleInfoNew = new MatchingConfigurationRuleInfo();

        try
        {
            PopupHelper.HideErrorMessage(this);

            if (_columnDataType == Convert.ToInt64(WebEnums.DataType.Integer) ||
                _columnDataType == Convert.ToInt64(WebEnums.DataType.Decimal))
            {
                if (Int16.TryParse(ddlValueType.SelectedValue, out threasHoldTypeID))
                    oMatchingConfigurationRuleInfoNew.ThresholdTypeID = threasHoldTypeID;
            }
            if (_columnDataType == Convert.ToInt64(WebEnums.DataType.DataTime) ||
                _columnDataType == Convert.ToInt64(WebEnums.DataType.Integer) ||
                _columnDataType == Convert.ToInt64(WebEnums.DataType.Decimal))
            {
                oMatchingConfigurationRuleInfoNew.LowerBound = luBoundCtrl.GetLowerBoundValue;
                oMatchingConfigurationRuleInfoNew.UpperBound = luBoundCtrl.GetUpperBoundValue;
                oMatchingConfigurationRuleInfoNew.OperatorID = (short)WebEnums.Operator.Between;
            }
            else if (_columnDataType == Convert.ToInt64(WebEnums.DataType.String))
            {
                if (Int16.TryParse(ddlOperator.SelectedValue, out operatorID))
                    oMatchingConfigurationRuleInfoNew.OperatorID = operatorID;
                oMatchingConfigurationRuleInfoNew.Keywords = txtStringValue.Text;
            }
            oMatchingConfigurationRuleInfoNew.MatchingConfigurationID = matchingSourceID;
            if (mode == "add")      //adding new rule
            {
                if (oMatchingConfigurationInfo.MatchingConfigurationRuleInfoCollection == null || oMatchingConfigurationInfo.MatchingConfigurationRuleInfoCollection.Count == 0)

                    oMatchingConfigurationRuleInfoNew.RuleID = 1;
                else
                    oMatchingConfigurationRuleInfoNew.RuleID =
                                            oMatchingConfigurationInfo.MatchingConfigurationRuleInfoCollection[oMatchingConfigurationInfo.MatchingConfigurationRuleInfoCollection.Count - 1].RuleID + 1;
                if (oMatchingConfigurationInfo.MatchingConfigurationRuleInfoCollection == null)
                    oMatchingConfigurationInfo.MatchingConfigurationRuleInfoCollection = new List<MatchingConfigurationRuleInfo>();
                oMatchingConfigurationInfo.MatchingConfigurationRuleInfoCollection.Add(oMatchingConfigurationRuleInfoNew);

            }
            else if (mode == "edit")        //modifying existing rule
            {
                int position = -1;
                MatchingConfigurationRuleInfo oMatchingConfigurationRuleInfoForDelete = null;
                oMatchingConfigurationRuleInfoNew.MatchingConfigurationID = matchingSourceID;
                if (_matchingConfigurationRuleID == 0)              //If rule is not saved and is in edit mode
                {
                    foreach (MatchingConfigurationRuleInfo inf in oMatchingConfigurationInfo.MatchingConfigurationRuleInfoCollection)
                    {
                        position++;
                        if (inf.RuleID == ruleID)
                        {
                            oMatchingConfigurationRuleInfoNew.RuleID = ruleID;
                            oMatchingConfigurationRuleInfoForDelete = inf;
                            break;
                        }
                    }
                }
                else            //Rule is Saved and is in Edit mode                                                
                {
                    foreach (MatchingConfigurationRuleInfo inf in oMatchingConfigurationInfo.MatchingConfigurationRuleInfoCollection)
                    {
                        position++;
                        if (inf.MatchingConfigurationRuleID == _matchingConfigurationRuleID)
                        {
                            oMatchingConfigurationRuleInfoForDelete = inf;
                            oMatchingConfigurationRuleInfoNew.MatchingConfigurationRuleID = inf.MatchingConfigurationRuleID;
                            break;
                        }

                    }
                }
                if (oMatchingConfigurationRuleInfoForDelete != null)
                {
                    oMatchingConfigurationInfo.MatchingConfigurationRuleInfoCollection.Remove(oMatchingConfigurationRuleInfoForDelete);
                    oMatchingConfigurationInfo.MatchingConfigurationRuleInfoCollection.Insert(position, oMatchingConfigurationRuleInfoNew);
                }
            }
            int x = 0;
            if (lstMatchingConfigurationInfoCollection != null)
            {

                foreach (MatchingConfigurationInfo info in lstMatchingConfigurationInfoCollection)
                {
                    if (info.MatchingSource1ColumnID == matchingSourceID)
                    {
                        lstMatchingConfigurationInfoCollection[x] = oMatchingConfigurationInfo;
                        break;
                    }
                    x++;
                }


                MatchSetSubSetCombinationInfo oMatchSetSubSetCombinationInfo = null;

                oMatchSetHdrInfo = SessionHelper.GetCurrentMatchSet();

                if (oMatchSetHdrInfo != null)
                    oMatchSetSubSetCombinationInfo = oMatchSetHdrInfo.MatchSetSubSetCombinationInfoCollection.Find(r => r.MatchSetSubSetCombinationID == lstMatchingConfigurationInfoCollection[0].MatchSetSubSetCombinationID);

                if (oMatchSetSubSetCombinationInfo != null)
                    oMatchSetSubSetCombinationInfo.IsConfigurationChange = true;

                Session[SessionConstants.MATCHING_CONFIGURATION_DATA] = lstMatchingConfigurationInfoCollection;
            }
            this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "ClosePopupAndRefreshParentPage", ScriptHelper.GetJSForClosePopupAndSubmitParentPage(true));
        }
        catch (Exception ex)
        {
            PopupHelper.ShowErrorMessage(this, ex);
        }
    }

    #endregion
}
