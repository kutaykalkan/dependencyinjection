using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SkyStem.ART.Web.Classes;
using Telerik.Web.UI;
using System.Xml;
using System.Collections;
using SkyStem.Library.Controls.TelerikWebControls;
using SkyStem.ART.Web.UserControls;
using SkyStem.ART.Web.Data;
using SkyStem.ART.Web.Utility;
using SkyStem.ART.Client.Params.Matching;
using SkyStem.ART.Client.Model.Matching;
using SkyStem.ART.Web.UserControls.Matching;
using System.Data;
using SkyStem.ART.Client.Data;
using SkyStem.ART.Client.Exception;

public partial class Pages_Matching_MatchingResults : PageBaseMatching
{

    long? _MatchSetID = null;
    long? _AccountID = null;
    long? _GlDataID = null;
    ARTEnums.MatchingType _MatchingType = ARTEnums.MatchingType.DataMatching;

    protected const string _MATCHED_TAB_ID = "rpvMatched";
    protected const string _PARTIAL_MATCHED_TAB_ID = "rpvPartialMatched";
    protected const string _UNMATCHED_TAB_ID = "rpvUnMatched";

    protected void Page_Init(object sender, EventArgs e)
    {
        MasterPageBase ompage = (MasterPageBase)this.Master.Master;
        ompage.ReconciliationPeriodChangedEventHandler += new EventHandler(ompage_ReconciliationPeriodChangedEventHandler);

        rtsTabs.TabClick += new RadTabStripEventHandler(rtsTabs_TabClick);
        ucMatchingCombinationSelection.OnMatchingCombinationSelectionChanged += new MatchingCombinationSelection.MatchingCombinationSelectionChanged(ucMatchingCombinationSelection_OnMatchingCombinationSelectionChanged);

        ucMatched.OnUnmatchClick += new Matched.UnmatchHandler(ucMatched_OnUnmatchClick);
        ucMatched.OnSaveRequired += new EventHandler(ucMatched_OnSaveRequired);

        ucPartialMatched.OnUnmatchClick += new PartialMatched.UnmatchHandler(ucMatched_OnUnmatchClick);
        ucPartialMatched.OnMatchClick += new PartialMatched.MatchHandler(ucPartialMatched_OnMatchClick);
        ucPartialMatched.OnSaveRequired += new EventHandler(ucMatched_OnSaveRequired);

        ucUnMatched.OnMatchClick += new Unmatched.MatchHandler(ucUnMatched_OnMatchClick);
        ucUnMatched.OnSaveRequired += new EventHandler(ucUnMatched_OnSaveRequired);
        ucUnMatched.OnCreateRecItemClick += new Unmatched.CreateRecItemHandler(ucUnMatched_OnCreateRecItemClick);
        ucUnMatched.OnCloseRecItemHandler += new Unmatched.CloseRecItemHandler(ucUnMatched_OnCloseRecItemClick);
    }

    void ucUnMatched_OnCreateRecItemClick(DataTable tblRecItem)
    {
        if (_MatchingType == ARTEnums.MatchingType.AccountMatching)
        {
            Session[SessionConstants.MATCHING_MATCH_SET_RESULT_CREATE_REC_ITEM_DATA] = tblRecItem;
            string createRecItemUrl = URLConstants.URL_MATCHING_RESULT_CREATE_REC_ITEMS + "?" +
                                 QueryStringConstants.MATCH_SET_ID + "=" + _MatchSetID.Value;
            if (_GlDataID != null)
                createRecItemUrl += "&" + QueryStringConstants.GLDATA_ID + "=" + _GlDataID.Value;
            if (_AccountID != null)
                createRecItemUrl += "&" + QueryStringConstants.ACCOUNT_ID + "=" + _AccountID.Value;

            createRecItemUrl += "&" + QueryStringConstants.MATCHING_TYPE_ID + "=" + ARTEnums.MatchingType.AccountMatching;
            //Response.Redirect(createRecItemUrl);
            SessionHelper.RedirectToUrl(createRecItemUrl);
            return;
        }
    }
    void ucUnMatched_OnCloseRecItemClick(DataTable tblRecItem)
    {
        if (_MatchingType == ARTEnums.MatchingType.AccountMatching)
        {
            Session[SessionConstants.MATCHING_MATCH_SET_RESULT_CREATE_REC_ITEM_DATA] = tblRecItem;
            string createRecItemUrl = URLConstants.URL_MATCHING_RESULT_CLOSE_REC_ITEMS + "?" +
                                 QueryStringConstants.MATCH_SET_ID + "=" + _MatchSetID.Value;
            if (_GlDataID != null)
                createRecItemUrl += "&" + QueryStringConstants.GLDATA_ID + "=" + _GlDataID.Value;
            if (_AccountID != null)
                createRecItemUrl += "&" + QueryStringConstants.ACCOUNT_ID + "=" + _AccountID.Value;

            createRecItemUrl += "&" + QueryStringConstants.MATCHING_TYPE_ID + "=" + ARTEnums.MatchingType.AccountMatching;
            //Response.Redirect(createRecItemUrl);
            SessionHelper.RedirectToUrl(createRecItemUrl);
            return;
        }
    }

    void ucUnMatched_OnSaveRequired(object sender, EventArgs e)
    {
        SaveData();
    }

    void ucMatched_OnSaveRequired(object sender, EventArgs e)
    {
        SaveData();
    }

    bool ucUnMatched_OnMatchClick(DataTable tblSource1, DataRow[] dataRowsSource1, DataTable tblSource2, DataRow[] dataRowsSource2)
    {
        try
        {
            long pairID = MatchingHelper.GetNextPairID(ucMatched.MatchedDataSet, ucMatched.MatchedWorkspaceDataSet);
            DataTable tblPair = Helper.FindDataTable(ucMatched.MatchedWorkspaceDataSet, MatchSetResultConstants.MATCH_SET_RESULT_TABLE_NAME_PAIR);
            DataTable tblTarget1 = Helper.FindDataTable(ucMatched.MatchedWorkspaceDataSet, MatchSetResultConstants.MATCH_SET_RESULT_TABLE_NAME_SOURCE1);
            DataTable tblTarget2 = Helper.FindDataTable(ucMatched.MatchedWorkspaceDataSet, MatchSetResultConstants.MATCH_SET_RESULT_TABLE_NAME_SOURCE2);

            if (pairID > 0 && tblPair != null && dataRowsSource1 != null && dataRowsSource1.Length > 0 && dataRowsSource2 != null && dataRowsSource2.Length > 0 &&
                   tblSource1 != null && tblSource2 != null && tblTarget1 != null && tblTarget2 != null)
            {
                MatchingHelper.AddPairRow(tblPair, pairID);
                MatchingHelper.SetPairID(dataRowsSource1, pairID);
                MatchingHelper.SetPairID(dataRowsSource2, pairID);
                Helper.MoveDataRows(tblSource1, tblTarget1, dataRowsSource1);
                Helper.MoveDataRows(tblSource2, tblTarget2, dataRowsSource2);
                return true;
            }
        }
        catch (ARTException ex)
        {
            Helper.ShowErrorMessage(this, ex);
        }
        return false;
    }

    bool ucPartialMatched_OnMatchClick(DataSet sourceDataSet, DataRow[] dataRows)
    {
        try
        {
            DataTable tblSource1 = Helper.FindDataTable(ucPartialMatched.PartialMatchedWorkspaceDataSet, MatchSetResultConstants.MATCH_SET_RESULT_TABLE_NAME_SOURCE1);
            DataTable tblSource2 = Helper.FindDataTable(ucPartialMatched.PartialMatchedWorkspaceDataSet, MatchSetResultConstants.MATCH_SET_RESULT_TABLE_NAME_SOURCE2);

            DataTable tblSourcePair = Helper.FindDataTable(sourceDataSet, MatchSetResultConstants.MATCH_SET_RESULT_TABLE_NAME_PAIR);
            DataTable tblTargetPair = Helper.FindDataTable(ucMatched.MatchedWorkspaceDataSet, MatchSetResultConstants.MATCH_SET_RESULT_TABLE_NAME_PAIR);

            DataTable tblTarget1 = Helper.FindDataTable(ucMatched.MatchedWorkspaceDataSet, MatchSetResultConstants.MATCH_SET_RESULT_TABLE_NAME_SOURCE1);
            DataTable tblTarget2 = Helper.FindDataTable(ucMatched.MatchedWorkspaceDataSet, MatchSetResultConstants.MATCH_SET_RESULT_TABLE_NAME_SOURCE2);

            foreach (DataRow dr in dataRows)
            {
                long pairID = MatchingHelper.GetNextPairID(ucMatched.MatchedDataSet, ucMatched.MatchedWorkspaceDataSet);
                DataRow[] dataRowsSource1 = dr.GetChildRows(MatchSetResultConstants.MATCH_SET_RESULT_RELATION_NAME_SOURCE1);
                DataRow[] dataRowsSource2 = dr.GetChildRows(MatchSetResultConstants.MATCH_SET_RESULT_RELATION_NAME_SOURCE2);

                if (pairID > 0 && dataRowsSource1 != null && dataRowsSource2 != null)
                {
                    // Add Pair Row
                    MatchingHelper.AddPairRow(tblTargetPair, pairID);
                    // Move Child Rows
                    MatchingHelper.UpdatePairIDAndMoveRows(tblSource1, tblTarget1, dataRowsSource1, pairID);
                    MatchingHelper.UpdatePairIDAndMoveRows(tblSource2, tblTarget2, dataRowsSource2, pairID);
                    // Remove Pair from Source
                    tblSourcePair.Rows.Remove(dr);
                }
            }
        }
        catch (ARTException ex)
        {
            Helper.ShowErrorMessage(this, ex);
        }
        return true;
    }

    bool ucMatched_OnUnmatchClick(DataSet sourceDataSet, DataRow[] dataRows)
    {
        try
        {
            DataTable dtSourcePair = Helper.FindDataTable(sourceDataSet, MatchSetResultConstants.MATCH_SET_RESULT_TABLE_NAME_PAIR);
            DataTable dtSource1 = Helper.FindDataTable(sourceDataSet, MatchSetResultConstants.MATCH_SET_RESULT_TABLE_NAME_SOURCE1);
            DataTable dtSource2 = Helper.FindDataTable(sourceDataSet, MatchSetResultConstants.MATCH_SET_RESULT_TABLE_NAME_SOURCE2);

            DataTable dtTarget1 = Helper.FindDataTable(ucUnMatched.UnmatchedWorkspaceDataSet, MatchSetResultConstants.MATCH_SET_RESULT_TABLE_NAME_SOURCE1);
            DataTable dtTarget2 = Helper.FindDataTable(ucUnMatched.UnmatchedWorkspaceDataSet, MatchSetResultConstants.MATCH_SET_RESULT_TABLE_NAME_SOURCE2);

            if (dtSourcePair != null && dtSource1 != null && dtSource2 != null && dtTarget1 != null && dtTarget2 != null)
            {
                foreach (DataRow dr in dataRows)
                {
                    // Get Child Rows
                    DataRow[] childRowsSource1 = dr.GetChildRows(MatchSetResultConstants.MATCH_SET_RESULT_RELATION_NAME_SOURCE1);
                    DataRow[] childRowsSource2 = dr.GetChildRows(MatchSetResultConstants.MATCH_SET_RESULT_RELATION_NAME_SOURCE2);
                    if (childRowsSource1 != null && childRowsSource2 != null)
                    {
                        // Disconnect From Pair
                        MatchingHelper.SetPairID(childRowsSource1, null);
                        MatchingHelper.SetPairID(childRowsSource2, null);
                        // Move Rows
                        Helper.MoveDataRows(dtSource1, dtTarget1, childRowsSource1);
                        Helper.MoveDataRows(dtSource2, dtTarget2, childRowsSource2);
                        // Remove Pair
                        dtSourcePair.Rows.Remove(dr);
                    }
                }
                ucUnMatched.UpdateRecItemNumber();
                return true;
            }
        }
        catch (ARTException ex)
        {
            Helper.ShowErrorMessage(this, ex);
        }
        return false;
    }

    void ucMatchingCombinationSelection_OnMatchingCombinationSelectionChanged(MatchSetSubSetCombinationInfo oMatchSetSubSetCombinationInfo)
    {
        PopulateData();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        SetPageSettings();
        GetQueryStringValues();
        if (_MatchingType == ARTEnums.MatchingType.DataMatching)
        {
            Session[SessionConstants.PARENT_PAGE_URL] = null;
        }
        if (!Page.IsPostBack)
        {
            BindCombinationSelection();
            this.ReturnUrl = Helper.ReturnURL(this.Page);
        }
        ucMatched.EnableDisableControls(this.FormMode);
        ucPartialMatched.EnableDisableControls(this.FormMode);
        ucUnMatched.EnableDisableControls(this.FormMode);
    }

    /// <summary>
    /// Sets the page settings.
    /// </summary>
    private void SetPageSettings()
    {
        Helper.SetPageTitle(this, 2353);
        Helper.SetBreadcrumbs(this, 1071, 2234, 2185, 2353);
        MasterPageBase oMasterPageBase = (MasterPageBase)this.Master.Master;
        MasterPageSettings oMasterPageSettings = new MasterPageSettings();
        oMasterPageSettings.EnableRoleSelection = false;
        oMasterPageSettings.EnableRecPeriodSelection = false;
        oMasterPageBase.SetMasterPageSettings(oMasterPageSettings);
      
    }

    /// <summary>
    /// Get Query String Values
    /// </summary>
    private void GetQueryStringValues()
    {
        if (!String.IsNullOrEmpty(Request.QueryString[QueryStringConstants.MATCH_SET_ID]))
        {
            _MatchSetID = Convert.ToInt64(Request.QueryString[QueryStringConstants.MATCH_SET_ID]);
        }
        if (!String.IsNullOrEmpty(Request.QueryString[QueryStringConstants.MATCHING_TYPE_ID]))
        {
            _MatchingType = (ARTEnums.MatchingType)Enum.Parse(typeof(ARTEnums.MatchingType), Request.QueryString[QueryStringConstants.MATCHING_TYPE_ID]);
        }
        if (!String.IsNullOrEmpty(Request.QueryString[QueryStringConstants.ACCOUNT_ID]))
        {
            _AccountID = Convert.ToInt64(Request.QueryString[QueryStringConstants.ACCOUNT_ID]);
        }
        if (!String.IsNullOrEmpty(Request.QueryString[QueryStringConstants.GLDATA_ID]))
        {
            _GlDataID = Convert.ToInt64(Request.QueryString[QueryStringConstants.GLDATA_ID]);
        }
    }

    /// <summary>
    /// Bind Combination Selection Dropdown
    /// </summary>
    private void BindCombinationSelection()
    {
        try
        {
            if (_MatchSetID.GetValueOrDefault() > 0)
            {
                MatchSetHdrInfo oMatchSetHdrInfo = MatchingHelper.GetMatchSetResults(_MatchSetID, _GlDataID, SessionHelper.CurrentReconciliationPeriodID, true);
                pnlMessage.Visible = true;
                pnlResult.Visible = false;
                this.FormMode = WebEnums.FormMode.ReadOnly;
                if (oMatchSetHdrInfo != null)
                {
                    uscMatchSetInfo.PopulateData(oMatchSetHdrInfo);
                    WebEnums.ReconciliationStatus? eRecStatus = (WebEnums.ReconciliationStatus?)Helper.GetReconciliationStatusByGLDataID(this.GLDataID);
                    this.FormMode = MatchingHelper.GetFormModeForMatching(WebEnums.ARTPages.MatchSetResults, _MatchingType, eRecStatus, this.GLDataID, oMatchSetHdrInfo);
                    if (oMatchSetHdrInfo.MatchSetSubSetCombinationInfoCollection != null && oMatchSetHdrInfo.MatchSetSubSetCombinationInfoCollection.Count > 0)
                    {
                        pnlMessage.Visible = false;
                        pnlResult.Visible = true;
                        ucMatchingCombinationSelection.BindMatchingCombination(oMatchSetHdrInfo.MatchSetSubSetCombinationInfoCollection);

                        PopulateData();
                    }
                }
            }
        }
        catch (ARTException ex)
        {
            Helper.ShowErrorMessage(this, ex);
        }
    }

    void rtsTabs_TabClick(object sender, RadTabStripEventArgs e)
    {
        BindTabs();
    }


    private void BindTabs()
    {
        switch (GetActiveTabID())
        {
            case _MATCHED_TAB_ID:
                ucMatched.BindGrids();
                break;
            case _UNMATCHED_TAB_ID:
                ucUnMatched.BindGrids();
                break;
            case _PARTIAL_MATCHED_TAB_ID:
                ucPartialMatched.BindGrids();
                break;
        }
    }

    /// <summary>
    /// Gets the active tab ID.
    /// </summary>
    /// <returns></returns>
    private string GetActiveTabID()
    {
        return rtsTabs.SelectedTab.PageView.ID;
    }

    /// <summary>
    /// Populate Data
    /// </summary>
    private void PopulateData()
    {
        try
        {
            MatchSetSubSetCombinationInfo oMatchSetSubSetCombinationInfo = SessionHelper.CurrentMatchSetSubSetCombinationInfo;
            if (oMatchSetSubSetCombinationInfo != null && oMatchSetSubSetCombinationInfo.MatchSetResultWorkspaceInfo != null)
            {
                SessionHelper.ClearDynamicFilterData("rgUnmatched");


                DataSet oDataSet;
                oDataSet = new DataSet();
                oDataSet = Helper.GetDataSet(oMatchSetSubSetCombinationInfo.MatchSetResultWorkspaceInfo.ResultSchema);
                MatchingHelper.AddCustomColumns(oDataSet);
                ucMatched.MatchedDataSet = oDataSet;

                oDataSet = new DataSet();
                oDataSet = Helper.GetDataSet(oMatchSetSubSetCombinationInfo.MatchSetResultWorkspaceInfo.ResultSchema);
                MatchingHelper.AddCustomColumns(oDataSet);
                ucMatched.MatchedWorkspaceDataSet = oDataSet;
                ucMatched.MatchSetSubSetCombinationID = ucMatchingCombinationSelection.CurrentMatchSetSubSetCombinationID;

                oDataSet = new DataSet();
                oDataSet = Helper.GetDataSet(oMatchSetSubSetCombinationInfo.MatchSetResultWorkspaceInfo.ResultSchema);
                MatchingHelper.AddCustomColumns(oDataSet);
                ucPartialMatched.PartialMatchedDataSet = oDataSet;

                oDataSet = new DataSet();
                oDataSet = Helper.GetDataSet(oMatchSetSubSetCombinationInfo.MatchSetResultWorkspaceInfo.ResultSchema);
                MatchingHelper.AddCustomColumns(oDataSet);
                ucPartialMatched.PartialMatchedWorkspaceDataSet = oDataSet;
                ucPartialMatched.MatchSetSubSetCombinationID = ucMatchingCombinationSelection.CurrentMatchSetSubSetCombinationID;

                DataTable tblSource1 = Helper.FindDataTable(ucMatched.MatchedDataSet, MatchSetResultConstants.MATCH_SET_RESULT_TABLE_NAME_SOURCE1);
                DataTable tblSource2 = Helper.FindDataTable(ucMatched.MatchedDataSet, MatchSetResultConstants.MATCH_SET_RESULT_TABLE_NAME_SOURCE2);
                ucUnMatched.UnmatchedDataSet = new DataSet();
                ucUnMatched.UnmatchedDataSet.Tables.Add(tblSource1.Clone());
                ucUnMatched.UnmatchedDataSet.Tables.Add(tblSource2.Clone());

                ucUnMatched.UnmatchedWorkspaceDataSet = new DataSet();
                ucUnMatched.UnmatchedWorkspaceDataSet.Tables.Add(tblSource1.Clone());
                ucUnMatched.UnmatchedWorkspaceDataSet.Tables.Add(tblSource2.Clone());
                ucUnMatched.MatchingType = _MatchingType;
                ucUnMatched.MatchSetSubSetCombinationID = ucMatchingCombinationSelection.CurrentMatchSetSubSetCombinationID;
                ucUnMatched.GlDataID = this.GLDataID;
                ucUnMatched.AccountID = this.AccountID;

                string tabID = GetActiveTabID();
                ucMatched.PopulateData(this.FormMode, _MATCHED_TAB_ID == tabID);
                ucPartialMatched.PopulateData(this.FormMode, _PARTIAL_MATCHED_TAB_ID == tabID);
                ucUnMatched.PopulateData(this.FormMode, _UNMATCHED_TAB_ID == tabID);
            }
        }
        catch (ARTException ex)
        {
            Helper.ShowErrorMessage(this, ex);
        }
    }

    /// <summary>
    /// Save Data
    /// </summary>
    public void SaveData()
    {
        try
        {
            MatchSetSubSetCombinationInfo oMatchSetSubSetCombinationInfo = ucMatchingCombinationSelection.CurrentMatchSetSubSetCombinationInfo;
            if (oMatchSetSubSetCombinationInfo != null && oMatchSetSubSetCombinationInfo.MatchSetResultWorkspaceInfo != null)
            {
                MatchSetResultWorkspaceInfo oMatchSetResultWorkspaceInfo = oMatchSetSubSetCombinationInfo.MatchSetResultWorkspaceInfo;
                oMatchSetResultWorkspaceInfo.MatchData = Helper.GetXmlFromDataSet(ucMatched.MatchedDataSet);
                oMatchSetResultWorkspaceInfo.WorkspaceMatchData = Helper.GetXmlFromDataSet(ucMatched.MatchedWorkspaceDataSet);
                oMatchSetResultWorkspaceInfo.PartialMatchData = Helper.GetXmlFromDataSet(ucPartialMatched.PartialMatchedDataSet);
                oMatchSetResultWorkspaceInfo.WorkspacePartialMatchData = Helper.GetXmlFromDataSet(ucPartialMatched.PartialMatchedWorkspaceDataSet);
                oMatchSetResultWorkspaceInfo.UnmatchData = Helper.GetXmlFromDataSet(ucUnMatched.UnmatchedDataSet);
                oMatchSetResultWorkspaceInfo.WorkspaceUnmatchData = Helper.GetXmlFromDataSet(ucUnMatched.UnmatchedWorkspaceDataSet);
                MatchingParamInfo oMatchingParamInfo = new MatchingParamInfo();
                oMatchingParamInfo.MatchSetResultWorkspaceInfo = oMatchSetResultWorkspaceInfo;
                oMatchingParamInfo.DateRevised = DateTime.Now;
                oMatchingParamInfo.RevisedBy = SessionHelper.CurrentUserEmailID;
                MatchingHelper.UpdateMatchSetResults(oMatchingParamInfo);
            }
        }
        catch (ARTException ex)
        {
            Helper.ShowErrorMessage(this, ex);
        }
    }

    public override void RefreshPage(object sender, RefreshEventArgs args)
    {
        base.RefreshPage(sender, args);
        if (rtsTabs.SelectedTab.PageView.ID == "rpvUnMatched")
            ucUnMatched.ApplyFilter();
        BindTabs();
    }

    protected void ompage_ReconciliationPeriodChangedEventHandler(object sender, EventArgs e)
    {
        try
        {
            BindCombinationSelection();
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


    public override string GetMenuKey()
    {
        return "MatchingResults";
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        //Response.Redirect(ReturnUrl, true);
        //Response.Redirect(URLConstants.URL_MATCHING_VIEW_MATCH_SET, true);
        SessionHelper.RedirectToUrl(URLConstants.URL_MATCHING_VIEW_MATCH_SET);
        return;
    }

}
