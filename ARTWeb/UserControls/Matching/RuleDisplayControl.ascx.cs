using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SkyStem.ART.Client.Model.Matching;
using SkyStem.ART.Web.Classes;
using System.Web.UI.HtmlControls;
using SkyStem.Library.Controls.WebControls;
using SkyStem.ART.Web.Data;
using SkyStem.ART.Web.Utility;
using System.Text;
using SkyStem.Language.LanguageUtility;

public partial class RuleDisplayControl : UserControlBase
{
    #region public Variables
    public delegate void DeleteRuleByRuleID(Int64 rowID, Int64 ruleID);
    public event CommandEventHandler DeleteRuleCommand;

    #endregion

    #region private Variables
    List<MatchingConfigurationInfo> lstMatchingConfigurationInfo = null;
    List<MatchingConfigurationRuleInfo> lstMatchingConfigurationAllRuleInfo = null;

    string sessionKey = string.Empty;
    private static Int64 _rowID = 0;
    short dataType;
    string columnName1 = string.Empty;
    string columnName2 = string.Empty;
    private static bool _IsEditMode = true;
    #endregion

    #region public properties
    public static Int64 RowID
    {
        get { return _rowID; }
        set { _rowID = value; }
    }
    public Int32 RuleCount
    {
        get;
        set;
    }
    public bool IsEditMode
    {
        get { return _IsEditMode; }
        set { _IsEditMode = value; }
    }

    #endregion

    #region Page Events
    protected void Page_Load(object sender, EventArgs e)
    {
       
    }
    #endregion

    #region private methods
    /// <summary>
    /// Gets all rules by Row ID (Column1 ID)
    /// </summary>
    /// <param name="rowID"></param>
    /// <returns></returns>
    List<MatchingConfigurationRuleInfo> GetRulesInfoMyRowID(Int64 rowID)
    {
        List<MatchingConfigurationRuleInfo> lst = null;
        try
        {
            if (Session[SessionConstants.MATCHING_CONFIGURATION_DATA] != null)
            {
                lstMatchingConfigurationInfo = Session[SessionConstants.MATCHING_CONFIGURATION_DATA] as List<MatchingConfigurationInfo>;

                if (lstMatchingConfigurationInfo != null)
                {
                    lst = lstMatchingConfigurationInfo.Find(r => r.MatchingSource1ColumnID == rowID).MatchingConfigurationRuleInfoCollection;
                    dataType = Convert.ToInt16(lstMatchingConfigurationInfo.Find(r => r.MatchingSource1ColumnID == rowID).DataTypeID);
                    columnName1 = lstMatchingConfigurationInfo.Find(r => r.MatchingSource1ColumnID == rowID).ColumnName1;
                    columnName1 = lstMatchingConfigurationInfo.Find(r => r.MatchingSource1ColumnID == rowID).ColumnName1;
                }
            }
        }
        catch { }
        return lst;
    }
    protected void rptRules_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        MatchingConfigurationRuleInfo oMatchingConfigurationRuleInfo = e.Item.DataItem as MatchingConfigurationRuleInfo;
        if (oMatchingConfigurationRuleInfo != null)
        {
            //string url = URLConstants.URL_MATCHING_RULE_SETUP + "?mode = edit & rowID=" + oMatchingConfigurationRuleInfo.MatchingConfigurationID + "&ruleID=" + oMatchingConfigurationRuleInfo.MatchingConfigurationRuleID;
            ExHyperLink lnkRule = e.Item.FindControl("hlRuleText") as ExHyperLink;
            ExImageButton exImgDelete = e.Item.FindControl("exImgDelete") as ExImageButton;

            long mcrID = 0;
            if (lnkRule != null)
            {
                if (oMatchingConfigurationRuleInfo.MatchingConfigurationRuleID != null)
                    mcrID = oMatchingConfigurationRuleInfo.MatchingConfigurationRuleID.Value;
                lnkRule.NavigateUrl = "javascript:LoadRuleSetupPopupEdit(" + oMatchingConfigurationRuleInfo.MatchingConfigurationID + ",'" + oMatchingConfigurationRuleInfo.RuleID + "'," + mcrID + ")";
            }
            if (exImgDelete != null)
            {
                exImgDelete.Visible = this.IsEditMode;
            }
        }


    }
    /// <summary>
    /// This method returns the display text for the based on MatchingConfigurationRuleInfo
    /// </summary>
    /// <param name="info"></param>
    /// <returns></returns>
    string getRuleText(MatchingConfigurationRuleInfo info)
    {
        StringBuilder RuleBuilder = new StringBuilder();
        if (dataType == Convert.ToInt16(WebEnums.DataType.Integer) || dataType == Convert.ToInt16(WebEnums.DataType.Decimal))
        {
            if (info.ThresholdTypeID == Convert.ToInt16(WebEnums.ThresholdType.Fixed))
            {
                RuleBuilder.Append(columnName1).Append(" - ").Append(info.LowerBound.Value.ToString("#.##")).Append(" ").Append(LanguageUtil.GetValue(1345))
                    .Append(" ").Append(columnName1).Append(" + ").Append(info.UpperBound.Value.ToString("#.##"));
            }
            else
                RuleBuilder.Append(columnName1).Append(" - ").Append(info.LowerBound.Value.ToString("#.##")).Append("% ").
                    Append(LanguageUtil.GetValue(1345)).Append(" ").Append(columnName1).Append(" + ").Append(info.UpperBound.Value.ToString("#.##")).Append("%");
        }
        else if (dataType == Convert.ToInt16(WebEnums.DataType.String))
        {
            if (info.OperatorID == Convert.ToInt16(WebEnums.Operator.Contains))
            {
                RuleBuilder.Append(columnName1).Append(" ").Append(LanguageUtil.GetValue(1962)).Append(" '")
                    .Append(info.Keywords).Append("'");
            }
            else if (info.OperatorID == Convert.ToInt16(WebEnums.Operator.Equals))
            {
                RuleBuilder.Append(columnName1).Append(" ").Append(LanguageUtil.GetValue(1960)).Append(" '")
                    .Append(info.Keywords).Append("'");
            }
        }
        else if (dataType == Convert.ToInt16(WebEnums.DataType.DataTime))
        {
            RuleBuilder.Append(columnName2).Append(" ").Append(LanguageUtil.GetValue(1961)).Append("( ")
                .Append(columnName1).Append(" - ").Append(info.LowerBound.Value.ToString("#")).Append(" ").Append(LanguageUtil.GetValue(1245))
                .Append(") ").Append(LanguageUtil.GetValue(2336)).Append(" (").Append(columnName1).Append(" + ").Append(info.UpperBound.Value.ToString("#")).Append(" ").Append(LanguageUtil.GetValue(1245)).Append(")");
        }
        return RuleBuilder.ToString();

    }

    /// <summary>
    /// This method populates the rules in the repeater
    /// </summary>
    /// <param name="rowID"></param>
    public void PopulateRuleTable(Int64 rowID)
    {
        lstMatchingConfigurationAllRuleInfo = GetRulesInfoMyRowID(rowID);
        if (lstMatchingConfigurationAllRuleInfo != null)
        {
            foreach (MatchingConfigurationRuleInfo info in lstMatchingConfigurationAllRuleInfo)
            {
                info.DisplayRuleText = getRuleText(info);
            }
            HtmlTable tblRules = rptRules.FindControl("tblrules") as HtmlTable;
            if (tblRules != null)
                tblRules.Style.Add("border", "auto");

            rptRules.DataSource = lstMatchingConfigurationAllRuleInfo;
            rptRules.DataBind();
        }
    }
    #endregion

    #region control events
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void rptRules_ItemCommand(object sender, CommandEventArgs e)
    {
        Int64 _matchingConfigurationID, _ruleID, _matchingConfigurationRuleID;
        string[] values = null;
        values = e.CommandArgument.ToString().Split('|');
        _matchingConfigurationID = (values[0] == "" ? 0 : Convert.ToInt64(values[0]));      //is MatchingConfigurationID
        _ruleID = (values[1] == "" ? 0 : Convert.ToInt64(values[1]));    //ruleID is found when rule is new
        _matchingConfigurationRuleID = (values[2] == "" ? 0 : Convert.ToInt64(values[2]));     //MatchingConfigurationRuleID is found when rule is old
        if (e.CommandName == "deleteRule")
            RaiseBubbleEvent(DeleteRuleCommand, new CommandEventArgs("DeleteRule", _matchingConfigurationID + "|" + _ruleID + "|" + _matchingConfigurationRuleID));
    }
    #endregion
}
