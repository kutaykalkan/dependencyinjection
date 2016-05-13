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
using SkyStem.ART.Client.IServices;
using Telerik.Web.UI;
public partial class Pages_Matching_ViewRecItems : PopupPageBase
{

    bool _isMultiCurrencyCapabilityActivated = false;
    Int64 _MatchsetSubSetCombinationID;
    Int64 _ExCelRowNumber;
    Int64 _MatchSetMatchingSourceDataImportID;
    protected short? _RecCategoryType = 0;
    Int64 GLDataID;
    long AccountID;
   
    private string CurrentBCCY
    {
        get
        {
            if (ViewState[ViewStateConstants.CURRENT_ACCT_BCCY] == null || Convert.ToString(ViewState[ViewStateConstants.CURRENT_ACCT_BCCY]) == "")
            {
                ViewState[ViewStateConstants.CURRENT_ACCT_BCCY] = Helper.GetCurrentAccountBCCY(this.GLDataID);
            }
            return ViewState[ViewStateConstants.CURRENT_ACCT_BCCY].ToString();
        }
        set
        {
            ViewState[ViewStateConstants.CURRENT_ACCT_BCCY] = value;
        }
    }
    public short? GLRecItemTypeID
    {
        get
        {
            if (ViewState["GLRecItemTypeID"] != null)
                return (short)ViewState["GLRecItemTypeID"];
            else
            {
                return null;
            }
        }
        set { ViewState["GLRecItemTypeID"] = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        SetGridVisiblity();
        GetQueryStringValues();
        IGLDataRecItem oGLDataRecItemClient = RemotingHelper.GetGLDataRecItemObject();
        GLRecItemTypeID = oGLDataRecItemClient.GetGLRecItemTypeID(_MatchsetSubSetCombinationID, _ExCelRowNumber, _MatchSetMatchingSourceDataImportID, Helper.GetAppUserInfo());
        this._isMultiCurrencyCapabilityActivated = Helper.IsCapabilityActivatedForCurrentRecPeriod(ARTEnums.Capability.MultiCurrency);
        ucGLDataRecurringScheduleItemsGrid.GridItemDataBound += new GridItemEventHandler(ucGLDataRecurringScheduleItemsGrid_GridItemDataBound);
        PopulateGrids();
        GridHelper.SetRecordCount(ucGLDataRecItemGrid.Grid);
        GridHelper.SetRecordCount(ucGLDataRecurringScheduleItemsGrid.Grid);
        GridHelper.SetRecordCount(ucGLDataWriteOnOffGrid.Grid);
    }
    private void SetGridVisiblity()
    {
        ucGLDataRecItemGrid.Visible = false;
        ucGLDataRecurringScheduleItemsGrid.Visible = false;
        ucGLDataWriteOnOffGrid.Visible = false;        
    }
    /// <summary>
    /// Get Query String Values
    /// </summary>
    private void GetQueryStringValues()
    {

        if (!String.IsNullOrEmpty(Request.QueryString[QueryStringConstants.MATCHSET_SUBSET_COMBINATION_ID]))
        {
            _MatchsetSubSetCombinationID = Convert.ToInt64(Request.QueryString[QueryStringConstants.MATCHSET_SUBSET_COMBINATION_ID]);
        }
        if (!String.IsNullOrEmpty(Request.QueryString[QueryStringConstants.EXCEL_ROW_NUMBER]))
        {
            _ExCelRowNumber = Convert.ToInt64(Request.QueryString[QueryStringConstants.EXCEL_ROW_NUMBER]);
        }
        if (!String.IsNullOrEmpty(Request.QueryString[QueryStringConstants.MATCH_SET_MATCHING_SOURCE_DATA_IMPORT_ID]))
        {
            _MatchSetMatchingSourceDataImportID = Convert.ToInt64(Request.QueryString[QueryStringConstants.MATCH_SET_MATCHING_SOURCE_DATA_IMPORT_ID]);
        }
        if (!String.IsNullOrEmpty(Request.QueryString[QueryStringConstants.GLDATA_ID]))
        {
            GLDataID = Convert.ToInt64(Request.QueryString[QueryStringConstants.GLDATA_ID]);           
        }
        if (!String.IsNullOrEmpty(Request.QueryString[QueryStringConstants.ACCOUNT_ID]))
        {
            AccountID = Convert.ToInt64(Request.QueryString[QueryStringConstants.ACCOUNT_ID]);
        }


    }
    /// <summary>
    /// Bind the Grids
    /// </summary>
    public void PopulateGrids()
    {
        switch (GLRecItemTypeID)
        {
            case (short)ARTEnums.GLDataRecItemType.GLDataRecItem:
                BindrgGLAdjustmentCloseditems();
                break;
            case (short)ARTEnums.GLDataRecItemType.GLDataRecurringItemSchedule:
                BindrgGLDataRecurringScheduleCloseditems();
                break;
            case (short)ARTEnums.GLDataRecItemType.GLDataWriteOnOff:
                BindrgDataWriteOnOffCloseditems();
                break;
        }
    }
    #region rgGLAdjustmentCloseditems    
    public void BindrgGLAdjustmentCloseditems()
    {

        List<GLDataRecItemInfo> oGLReconciliationItemInputInfoCollectionClosed = GetGLDataRecItemInfoCollection();
        if (oGLReconciliationItemInputInfoCollectionClosed == null || oGLReconciliationItemInputInfoCollectionClosed.Count == 0)
        {
            ucGLDataRecItemGrid.Visible = false;          
        }
        else
        {
            ucGLDataRecItemGrid.SetGLDataRecItemGridData(oGLReconciliationItemInputInfoCollectionClosed);
            ucGLDataRecItemGrid.LoadGridData();
            ucGLDataRecItemGrid.Visible = true;
        }
    }
    private List<GLDataRecItemInfo> GetGLDataRecItemInfoCollection()
    {
        List<GLDataRecItemInfo> GLRecItemInfoCollection = null;
        IGLDataRecItem oGLDataRecItemClient = RemotingHelper.GetGLDataRecItemObject();
        GLRecItemInfoCollection = oGLDataRecItemClient.GetClosedGLDataRecItem(this.GLDataID, _MatchsetSubSetCombinationID, _ExCelRowNumber, _MatchSetMatchingSourceDataImportID, (short)ARTEnums.GLDataRecItemType.GLDataRecItem, Helper.GetAppUserInfo());
        return GLRecItemInfoCollection;
    }
    void ucGLDataRecurringScheduleItemsGrid_GridItemDataBound(object sender, GridItemEventArgs e)
    {
        if (_RecCategoryType == (short)WebEnums.RecCategoryType.Amortizable_SupportingDetail_RecurringAmortizableSchedule)
        {
            ucGLDataRecurringScheduleItemsGrid.SetAmortizableGridHeaders(e);
        }
        else
        {
            ucGLDataRecurringScheduleItemsGrid.SetAccruableGridHeaders(e);
        }
    }
    #endregion
    #region rgGLDataRecurringScheduleCloseditems    
    private List<GLDataRecurringItemScheduleInfo> GetGLDataRecurringItemScheduleInfoCollection()
    {
        List<GLDataRecurringItemScheduleInfo> oGLDataRecurringItemScheduleInfoCollection = null;
        IGLDataRecItemSchedule oGLDataRecItemScheduleClient = RemotingHelper.GetGLDataRecItemScheduleObject();
        oGLDataRecurringItemScheduleInfoCollection = oGLDataRecItemScheduleClient.GetClosedGLDataRecurringItemSchedule(this.GLDataID, _MatchsetSubSetCombinationID, _ExCelRowNumber, _MatchSetMatchingSourceDataImportID, (short)ARTEnums.GLDataRecItemType.GLDataRecurringItemSchedule, Helper.GetAppUserInfo());
        if (oGLDataRecurringItemScheduleInfoCollection != null && oGLDataRecurringItemScheduleInfoCollection.Count > 0)
            this._RecCategoryType = oGLDataRecurringItemScheduleInfoCollection[0].ReconciliationCategoryTypeID;
        return oGLDataRecurringItemScheduleInfoCollection;
    }
    private void BindrgGLDataRecurringScheduleCloseditems()
    {
        List<GLDataRecurringItemScheduleInfo> oGLDataRecurringItemScheduleInfoCollection = GetGLDataRecurringItemScheduleInfoCollection();
        if (oGLDataRecurringItemScheduleInfoCollection == null || oGLDataRecurringItemScheduleInfoCollection.Count == 0)
        {
            ucGLDataRecurringScheduleItemsGrid.Visible = false;
        }
        else
        {
            ucGLDataRecurringScheduleItemsGrid.Visible = true;           
            ucGLDataRecurringScheduleItemsGrid.SetGLDataRecurringItemScheduleItemGridData(oGLDataRecurringItemScheduleInfoCollection);
            ucGLDataRecurringScheduleItemsGrid.LoadGridData();
        }
    }
    #endregion
    # region rgDataWriteOnOffCloseditems 
    private List<GLDataWriteOnOffInfo> GetGLDataWriteOnOffInfoCollection()
    {
        List<GLDataWriteOnOffInfo> oGLDataWriteOnOffInfoCollection = null;
        IGLDataWriteOnOff oGLDataWriteOnOffClient = RemotingHelper.GetGLDataWriteOnOffObject();
        oGLDataWriteOnOffInfoCollection = oGLDataWriteOnOffClient.GetClosedGLDataWriteOnOffItemByMatching(this.GLDataID, _MatchsetSubSetCombinationID, _ExCelRowNumber, _MatchSetMatchingSourceDataImportID, (short)ARTEnums.GLDataRecItemType.GLDataWriteOnOff, Helper.GetAppUserInfo());
        return oGLDataWriteOnOffInfoCollection;
    }
    private void BindrgDataWriteOnOffCloseditems()
    {
        List<GLDataWriteOnOffInfo> oGLDataWriteOnOffInfoCollection = GetGLDataWriteOnOffInfoCollection();
        if (oGLDataWriteOnOffInfoCollection == null || oGLDataWriteOnOffInfoCollection.Count == 0)
        {
            ucGLDataWriteOnOffGrid.Visible = false;
        }
        else
        {
            ucGLDataWriteOnOffGrid.Visible = true;           
            ucGLDataWriteOnOffGrid.SetGLDataWriteOnOffGridData(oGLDataWriteOnOffInfoCollection);
            ucGLDataWriteOnOffGrid.LoadGridData();
        }
    }

    # endregion


}
