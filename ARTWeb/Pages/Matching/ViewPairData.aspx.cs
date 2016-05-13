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
using SkyStem.ART.Client.Data;
using SkyStem.ART.Client.Exception;
using System.Data;
public partial class Pages_Matching_ViewPairData : PopupPageBase
{
    long? _PairID = 0;
    short  _IsFromWorkSpace = 0;
    WebEnums.MatchingResultType _MatchingResultType;    
    DataTable tblSource1;
    DataTable tblSource2;
    /// <summary>
    /// List of Columns
    /// </summary>
    private List<MatchingConfigurationInfo> MatchingConfigurationInfoList
    {
        get
        {
            if (SessionHelper.CurrentMatchSetSubSetCombinationInfo != null)
                return SessionHelper.CurrentMatchSetSubSetCombinationInfo.MatchingConfigurationInfoList;
            return null;
        }
    }
    /// <summary>
    /// Matching Source Data Set
    /// </summary>
    public DataSet SourceDataSet
    {
        get
        {
            DataSet ds;
            if (_MatchingResultType == WebEnums.MatchingResultType.Matched)
            {
                if (_IsFromWorkSpace == 1)
                    ds = (DataSet)Session[SessionConstants.MATCHING_MATCH_SET_RESULT_MATCHED_WORKSPACE_SOURCE];
                else
                    ds = (DataSet)Session[SessionConstants.MATCHING_MATCH_SET_RESULT_MATCHED_SOURCE];
            }
            else
            {
                if (_IsFromWorkSpace == 1)
                    ds = (DataSet)Session[SessionConstants.MATCHING_MATCH_SET_RESULT_PARTIAL_MATCHED_WORKSPACE_SOURCE];
                else
                    ds = (DataSet)Session[SessionConstants.MATCHING_MATCH_SET_RESULT_PARTIAL_MATCHED_SOURCE];            
            }
            return ds;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        GetQueryStringValues();
        CreateColumns();
        BindGrids();
    }
    /// <summary>
    /// Create Grid Columns
    /// </summary>
    private void CreateColumns()
    {
        try
        {
            if (MatchingConfigurationInfoList != null)
            {
                rgSource1.MasterTableView.DataKeyNames = MatchingHelper.GetDataKeyNamesForUnMatched();
                rgSource2.MasterTableView.DataKeyNames = MatchingHelper.GetDataKeyNamesForUnMatched();
                MatchingHelper.CreateGridColumns(rgSource1, MatchingConfigurationInfoList);
                MatchingHelper.CreateMatchingSourceNameColumn(rgSource1);
                MatchingHelper.CreateRecItemNumberColumn(rgSource1);
                MatchingHelper.CreateGridColumns(rgSource2, MatchingConfigurationInfoList);
                MatchingHelper.CreateMatchingSourceNameColumn(rgSource2);
                MatchingHelper.CreateRecItemNumberColumn(rgSource2);              
            }
        }
        catch (ARTException ex)
        {
            PopupHelper.ShowErrorMessage (this, ex);
        }
    }

    protected void rgSource1_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    {
        try
        {
            if (tblSource1 != null)
            {
                rgSource1.DataSource = tblSource1;
            }
            else
            {
                SetSourceTablesData();
                rgSource1.DataSource = tblSource1;
            }
        }
        catch (ARTException ex)
        {
            PopupHelper.ShowErrorMessage(this, ex);
        }
    }

    protected void rgSource2_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    {
        try
        {
            if (tblSource2 != null)
            {
                rgSource1.DataSource = tblSource2;
            }
            else
            {
                SetSourceTablesData();
                rgSource1.DataSource = tblSource2;
            }

        }
        catch (ARTException ex)
        {
            PopupHelper.ShowErrorMessage(this, ex);
        }
    }

    private void GetQueryStringValues()
    {
        if (!String.IsNullOrEmpty(Request.QueryString[QueryStringConstants.MATCH_SET_RESULT_COLUMN_NAME_PAIR_ID]))
        {
            _PairID = Convert.ToInt64(Request.QueryString[QueryStringConstants.MATCH_SET_RESULT_COLUMN_NAME_PAIR_ID]);
        }
        if (!String.IsNullOrEmpty(Request.QueryString[QueryStringConstants.MATCHING_RESULT_TYPE]))
        {
            _MatchingResultType = (WebEnums.MatchingResultType)Enum.Parse(typeof(WebEnums.MatchingResultType), Request.QueryString[QueryStringConstants.MATCHING_RESULT_TYPE]);
        }
        if (!String.IsNullOrEmpty(Request.QueryString[QueryStringConstants.IS_FROM_WORK_SPACE]))
        {           
            _IsFromWorkSpace = Convert.ToInt16 (Request.QueryString[QueryStringConstants.IS_FROM_WORK_SPACE]);
        }
    }

    private void SetSourceTablesData()
    {
        try
        {
            if (ViewState["Source1DataTable"] == null)
            {
                DataRow[] childRowsSource1;
                DataRow[] childRowsSource2;
                DataTable dtSourcePair = Helper.FindDataTable(this.SourceDataSet, MatchSetResultConstants.MATCH_SET_RESULT_TABLE_NAME_PAIR);
                if (dtSourcePair != null)
                {
                    DataRow[] drFromPairRows = MatchingHelper.GetPairRows(dtSourcePair, _PairID.ToString());
                    if (drFromPairRows.Length > 0)
                    {
                        DataTable t1 = Helper.FindDataTable(SourceDataSet, MatchSetResultConstants.MATCH_SET_RESULT_TABLE_NAME_SOURCE1);
                        DataTable t2 = Helper.FindDataTable(SourceDataSet, MatchSetResultConstants.MATCH_SET_RESULT_TABLE_NAME_SOURCE2);
                        tblSource1 = t1.Clone ();
                        tblSource2 = t2.Clone ();
                        // Get Child Rows
                        childRowsSource1 = drFromPairRows[0].GetChildRows(MatchSetResultConstants.MATCH_SET_RESULT_RELATION_NAME_SOURCE1);
                        childRowsSource2 = drFromPairRows[0].GetChildRows(MatchSetResultConstants.MATCH_SET_RESULT_RELATION_NAME_SOURCE2);
                        childRowsSource1.CopyToDataTable(tblSource1, LoadOption.OverwriteChanges);
                        childRowsSource2.CopyToDataTable(tblSource2, LoadOption.OverwriteChanges);
                    }
                }
                ViewState["Source1DataTable"] = tblSource1;
                ViewState["Source2DataTable"] = tblSource2;
            }
            else
            {
                tblSource1 = (DataTable)ViewState["Source1DataTable"];
                tblSource2 = (DataTable)ViewState["Source2DataTable"];
            }
        }
        catch (ARTException ex)
        {
            PopupHelper.ShowErrorMessage(this, ex);
        }
    }

    /// <summary>
    /// Bind the Grids
    /// </summary>
    public void BindGrids()
    {
        try
        {
            if (tblSource1 != null)
            {
                rgSource1.DataSource = tblSource1;
            }
            else
            {
                SetSourceTablesData();
                rgSource1.DataSource = tblSource1;
            }
            rgSource1.MasterTableView.CurrentPageIndex = 0;
            rgSource1.DataBind();

            if (tblSource2 != null)
            {
                rgSource2.DataSource = tblSource2;
            }
            else
            {
                SetSourceTablesData();
                rgSource2.DataSource = tblSource2;
            }
            rgSource2.MasterTableView.CurrentPageIndex = 0;
            rgSource2.DataBind();
         
        }
        catch (Exception ex)
        {
            PopupHelper.ShowErrorMessage (this, ex);
        }
    }
}
