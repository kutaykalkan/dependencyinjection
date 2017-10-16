using System;
using System.Collections;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using SkyStem.ART.Web.Classes;
using SkyStem.ART.Web.Utility;
using SkyStem.ART.Client.IServices;
using SkyStem.ART.Client.Exception;
using SkyStem.ART.Web.Data;
using SkyStem.ART.Client.Model;
using SkyStem.ART.Client.Model.Matching;
using System.Collections.Generic;
using SkyStem.Library.Controls.TelerikWebControls;
using SkyStem.Library.Controls.WebControls;
using Telerik.Web.UI;
using SkyStem.ART.Client.Data;
using SkyStem.Language.LanguageUtility;
using System.Text;


public partial class Pages_Matching_CreateRecItem : PageBaseMatching
{
    long? _AccountID = 0;
    long? _GLDataID = 0;
    long? _MatchSetID = null;
    bool _IsMultiCurrencyEnabled = false;
    string _AmountColumnName = string.Empty;
    ARTEnums.MatchingType _MatchingType = ARTEnums.MatchingType.AccountMatching;
    MatchSetSubSetCombinationInfo _oMatchSetSubSetCombinationInfo;
    //BCCY Changes
    private string CurrentBCCY
    {
        get
        {
            if (ViewState[ViewStateConstants.CURRENT_ACCT_BCCY] == null)
            {
                ViewState[ViewStateConstants.CURRENT_ACCT_BCCY] = Helper.GetCurrentAccountBCCY(this._GLDataID);
            }
            return ViewState[ViewStateConstants.CURRENT_ACCT_BCCY].ToString();
        }
        set
        {
            ViewState[ViewStateConstants.CURRENT_ACCT_BCCY] = value;
        }
    }
    protected void Page_Init(object sender, EventArgs e)
    {
        MasterPageBase ompage = (MasterPageBase)this.Master.Master;
        ompage.ReconciliationPeriodChangedEventHandler += new EventHandler(ompage_ReconciliationPeriodChangedEventHandler);
    }
    public void ompage_ReconciliationPeriodChangedEventHandler(object sender, EventArgs e)
    {
        PageLoad();
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        SetPrivateVariables();
    }
    private void PageLoad()
    {
        try
        {
            if (!Page.IsPostBack)
            {
                MatchSetHdrInfo oMatchSetHdrInfo = MatchingHelper.GetMatchSetResults(_MatchSetID, _GLDataID, SessionHelper.CurrentReconciliationPeriodID, true);
                WebEnums.ReconciliationStatus? eRecStatus = (WebEnums.ReconciliationStatus?)Helper.GetReconciliationStatusByGLDataID(_GLDataID);
                this.FormMode = MatchingHelper.GetFormModeForMatching(WebEnums.ARTPages.CreateRecItem, _MatchingType, eRecStatus, this.GLDataID, oMatchSetHdrInfo);
                if (_oMatchSetSubSetCombinationInfo != null)
                    lblDataSourceName.Text = _oMatchSetSubSetCombinationInfo.MatchSetSubSetCombinationName;
                Helper.SetPageTitle(this, 2327);
                Helper.SetBreadcrumbs(this, 2234, 2327);
                Helper.ShowInputRequirementSection(this, 2379, 2394);
                BindRecCategoryDDL();
                BindRecCategoryTypeDDL(Convert.ToInt32(ddlRecCategory.SelectedValue));
                GetClolumnMapping();
                //EnableDisableControls(this.FormMode);
                string AmountColumnName = GetAmountColumnName();
                if (!string.IsNullOrEmpty(AmountColumnName))
                    btnFlipSign.Visible = true;
                else
                    btnFlipSign.Visible = false;
            }
            GridHelper.SetRecordCount(rgCreateRecItem);
            GridHelper.SetRecordCount(rgCreateScheduleRecItem);
            GridHelper.SetRecordCount(rgCreateWriteOffOnRecItem);
        }
        catch (Exception ex)
        {
            Helper.ShowErrorMessage(this, ex);
        }
    }
    private void SetPrivateVariables()
    {
        if (Request.QueryString[QueryStringConstants.GLDATA_ID] != null)
            _GLDataID = Convert.ToInt64(Request.QueryString[QueryStringConstants.GLDATA_ID]);
        this._IsMultiCurrencyEnabled = Helper.IsCapabilityActivatedForCurrentRecPeriod(ARTEnums.Capability.MultiCurrency);
        _oMatchSetSubSetCombinationInfo = SessionHelper.CurrentMatchSetSubSetCombinationInfo;
        if (!String.IsNullOrEmpty(Request.QueryString[QueryStringConstants.MATCH_SET_ID]))
        {
            _MatchSetID = Convert.ToInt64(Request.QueryString[QueryStringConstants.MATCH_SET_ID]);
        }
        if (!String.IsNullOrEmpty(Request.QueryString[QueryStringConstants.ACCOUNT_ID]))
        {
            _AccountID = Convert.ToInt64(Request.QueryString[QueryStringConstants.ACCOUNT_ID]);
        }

        MasterPageBase oMasterPageBase = (MasterPageBase)this.Master.Master;
        MasterPageSettings oMasterPageSettings = new MasterPageSettings();
        oMasterPageSettings.EnableRoleSelection = false;
        oMasterPageSettings.EnableRecPeriodSelection = false;
        oMasterPageBase.SetMasterPageSettings(oMasterPageSettings);

    }
    private Hashtable GetClolumnMapping()
    {
        Hashtable htClolumnMapping = null;
        if (ViewState["htClolumnMapping"] == null)
        {
            List<MatchingConfigurationInfo> oMatchingConfigurationInfoCollection = new List<MatchingConfigurationInfo>();
            if (_oMatchSetSubSetCombinationInfo != null)
            {
                oMatchingConfigurationInfoCollection = _oMatchSetSubSetCombinationInfo.MatchingConfigurationInfoList;
                htClolumnMapping = new Hashtable();
                foreach (MatchingConfigurationInfo oMatchingConfigurationInfo in oMatchingConfigurationInfoCollection)
                {
                    if (oMatchingConfigurationInfo.IsDisplayedColumn.GetValueOrDefault() && oMatchingConfigurationInfo.RecItemColumnID.HasValue)
                    {
                        if (Enum.IsDefined(typeof(WebEnums.RecItemColumns), oMatchingConfigurationInfo.RecItemColumnID.Value))
                            htClolumnMapping.Add((WebEnums.RecItemColumns)oMatchingConfigurationInfo.RecItemColumnID, oMatchingConfigurationInfo.DisplayColumnName);
                    }
                }
                ViewState["htClolumnMapping"] = htClolumnMapping;
            }
        }
        else
        {
            htClolumnMapping = (Hashtable)ViewState["htClolumnMapping"];
        }
        return htClolumnMapping;
    }
    private string GetAmountColumnName()
    {
        string AmountColumnName = null;
        if (ViewState["AmountColumnName"] == null)
        {
            List<MatchingConfigurationInfo> oMatchingConfigurationInfoCollection = new List<MatchingConfigurationInfo>();
            if (_oMatchSetSubSetCombinationInfo != null)
            {
                oMatchingConfigurationInfoCollection = _oMatchSetSubSetCombinationInfo.MatchingConfigurationInfoList;
                AmountColumnName = MatchingHelper.GetAmountColumnName(oMatchingConfigurationInfoCollection);
                ViewState["AmountColumnName"] = AmountColumnName;
            }
        }
        else
        {
            AmountColumnName = (string)ViewState["AmountColumnName"];
        }
        return AmountColumnName;
    }
    public override string GetMenuKey()
    {
        return "CreateRecItem";
    }
    protected void ddlRecCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindRecCategoryTypeDDL(Convert.ToInt32(ddlRecCategory.SelectedValue));
        clearGridItems(rgCreateRecItem);
        pnlNormalitems.Visible = false;
        btnCreateRecItem.Enabled = false;
        btnSave.Enabled = false;
        clearGridItems(rgCreateScheduleRecItem);
        pnlScheduleItems.Visible = false;
        clearGridItems(rgCreateWriteOffOnRecItem);
        pnlWriteOffOnItems.Visible = false;
        btnFlipSign.Enabled = false;
        ClearViewStates();
    }
    protected void ddlRecCategoryType_SelectedIndexChanged(object sender, EventArgs e)
    {
        string ArrSelectedControlID = "0";
        string[] ArrSelected = ddlRecCategoryType.SelectedValue.Split('~');
        if (ArrSelected.Length > 0)
            ArrSelectedControlID = ArrSelected[0];
        //  18 is the Recurring Accrual Schedule Category TypeID  AND 3 is the Recurring Amortizable Schedule Category TypeID
        if (ArrSelectedControlID == ((short)ARTEnums.RecItemControl.ItemInputAmortizableTemplate).ToString() || ArrSelectedControlID == ((short)ARTEnums.RecItemControl.ItemInputAccurableRecurring).ToString())
        {
            Sel.Value = "";
            BindScheduleItems(GetGLDataScheduleRecItemInfoCollection(), false);
            hdnIsBindNormalItemGrid.Value = "0";
            hdnIsBindScheduleItemGrid.Value = "1";
            hdnIsBindWriteOffOnGrid.Value = "0";
            pnlNormalitems.Visible = false;
            pnlScheduleItems.Visible = true;
            pnlWriteOffOnItems.Visible = false;

        }
        else if (ArrSelectedControlID == ((short)ARTEnums.RecItemControl.ItemInputWriteOff).ToString())//  4 is the category Id for Variances/ Write offs
        {
            Sel.Value = "";
            BindGLDataWriteOnOffItems(GetGLDataWriteOnOffInfoCollection(), false);
            pnlNormalitems.Visible = false;
            pnlScheduleItems.Visible = false;
            hdnIsBindNormalItemGrid.Value = "0";
            hdnIsBindScheduleItemGrid.Value = "0";
            hdnIsBindWriteOffOnGrid.Value = "1";
            pnlWriteOffOnItems.Visible = true;
        }
        else if (ArrSelectedControlID == WebConstants.SELECT_ONE)
        {
            Sel.Value = "";
            clearGridItems(rgCreateRecItem);
            clearGridItems(rgCreateScheduleRecItem);
            pnlNormalitems.Visible = false;
            btnCreateRecItem.Enabled = false;
            btnSave.Enabled = false;
            btnFlipSign.Enabled = false;
            pnlScheduleItems.Visible = false;
            hdnIsBindNormalItemGrid.Value = "0";
            hdnIsBindScheduleItemGrid.Value = "0";
            hdnIsBindWriteOffOnGrid.Value = "0";
            pnlWriteOffOnItems.Visible = false;
        }
        else
        {
            Sel.Value = "";
            hdnIsBindNormalItemGrid.Value = "1";
            BindNormalItems(GetGLDataRecItemInfoCollection(), false);
            pnlNormalitems.Visible = true;
            pnlScheduleItems.Visible = false;
            hdnIsBindScheduleItemGrid.Value = "0";
            hdnIsBindWriteOffOnGrid.Value = "0";
            pnlWriteOffOnItems.Visible = false;
        }
        EnableDisableControls(this.FormMode);
    }
    private void clearGridItems(ExRadGrid rg)
    {
        rg.DataSource = null;
        rg.DataBind();
    }
    private List<ReconciliationCategoryMstInfo> GetReconciliationCategoryList()
    {

        List<ReconciliationCategoryMstInfo> oReconciliationCategoryMstInfoCollection = null;
        if (Session[this.AccountID.ToString()] == null)
        {
            int? RecTemplateID = null;
            IGLDataRecItem oGLDataRecItemClient = RemotingHelper.GetGLDataRecItemObject();
            oReconciliationCategoryMstInfoCollection = oGLDataRecItemClient.GetAllReconciliationCategory(SessionHelper.CurrentReconciliationPeriodID, this.AccountID, ref  RecTemplateID, Helper.GetAppUserInfo());
            Session[this.AccountID.ToString()] = oReconciliationCategoryMstInfoCollection;
            if (RecTemplateID.HasValue)
                hdnRecTemplateID.Value = RecTemplateID.Value.ToString();
        }
        else
        {
            oReconciliationCategoryMstInfoCollection = (List<ReconciliationCategoryMstInfo>)Session[this.AccountID.ToString()];
        }

        return oReconciliationCategoryMstInfoCollection;
    }
    private List<GLDataRecurringItemScheduleInfo> GetGLDataRecItemsCollectionList(long? MatchSetSubSetCombinationID)
    {

        List<GLDataRecurringItemScheduleInfo> oGLDataRecItemsCollection = null;
        //if (ViewState[MatchSetSubSetCombinationID.ToString()] == null)
        //{
        IGLDataRecItem oGLDataRecItemClient = RemotingHelper.GetGLDataRecItemObject();
        oGLDataRecItemsCollection = oGLDataRecItemClient.GetGLDataRecItemsListByMatchSetSubSetCombinationID(MatchSetSubSetCombinationID, Helper.GetAppUserInfo());
        //ViewState[MatchSetSubSetCombinationID.ToString()] = oGLDataRecItemsCollection;

        //}
        //else
        //{
        //    oGLDataRecItemsCollection = (List<GLDataRecurringItemScheduleInfo>)ViewState[MatchSetSubSetCombinationID.ToString()];
        //}

        return oGLDataRecItemsCollection;
    }
    private void BindRecCategoryDDL()
    {
        List<short> tempReconciliationCategoryID = new List<short>();
        ddlRecCategoryType.Items.Clear();
        List<ReconciliationCategoryMstInfo> oReconciliationCategoryMstInfoCollection = GetReconciliationCategoryList();
        for (int i = 0; i < oReconciliationCategoryMstInfoCollection.Count; i++)
        {
            if (!tempReconciliationCategoryID.Contains(oReconciliationCategoryMstInfoCollection[i].ReconciliationCategoryID.Value))
            {
                tempReconciliationCategoryID.Add(oReconciliationCategoryMstInfoCollection[i].ReconciliationCategoryID.Value);
                ddlRecCategory.Items.Add(new ListItem(LanguageUtil.GetValue(oReconciliationCategoryMstInfoCollection[i].ReconciliationCategoryLabelID.Value), oReconciliationCategoryMstInfoCollection[i].ReconciliationCategoryID.ToString()));
            }
        }
        ListControlHelper.AddListItemForSelectOne(ddlRecCategory);
    }
    private void BindRecCategoryTypeDDL(int ReconciliationCategoryID)
    {
        ddlRecCategoryType.Items.Clear();
        List<ReconciliationCategoryMstInfo> oReconciliationCategoryMstInfoCollection = GetReconciliationCategoryList();
        List<ReconciliationCategoryMstInfo> oReconciliationCategoryTypeCollection = null;
        oReconciliationCategoryTypeCollection = oReconciliationCategoryMstInfoCollection.FindAll(o => o.ReconciliationCategoryID.Value == ReconciliationCategoryID).ToList();
        for (int i = 0; i < oReconciliationCategoryTypeCollection.Count; i++)
        {

            //if(oReconciliationCategoryTypeCollection[i].ReconciliationCategoryTypeID.Value == 18) 
            // ddlRecCategoryType.Items.Add(new ListItem(LanguageUtil.GetValue(oReconciliationCategoryTypeCollection[i].ReconciliationCategoryTypeLabelID.Value), oReconciliationCategoryTypeCollection[i].RecItemControlID.ToString() + "~3" ));
            //else
            ddlRecCategoryType.Items.Add(new ListItem(LanguageUtil.GetValue(oReconciliationCategoryTypeCollection[i].ReconciliationCategoryTypeLabelID.Value), oReconciliationCategoryTypeCollection[i].RecItemControlID.ToString() + "~" + oReconciliationCategoryTypeCollection[i].ReconciliationCategoryTypeID.ToString()));

        }
        ListControlHelper.AddListItemForSelectOne(ddlRecCategoryType);
    }
    private void FillDdlLccy(DropDownList ddlLocalCurrency)
    {
        ListControlHelper.BindLocalCurrencyDropDown(ddlLocalCurrency, this._GLDataID.Value, this._IsMultiCurrencyEnabled);
        if (this._IsMultiCurrencyEnabled)
        {
            ListControlHelper.AddListItemForSelectOne(ddlLocalCurrency);
            //ListControlHelper.AddListItemForCCY(ddlLocalCurrency);
        }
    }
    #region NormalItems
    private List<GLDataRecItemInfo> GetGLDataRecItemInfoCollection()
    {

        List<GLDataRecItemInfo> oGLDataRecItemInfoCollection = null;
        if (ViewState["oGLDataRecItemInfoCollection"] == null)
        {
            oGLDataRecItemInfoCollection = new List<GLDataRecItemInfo>();
            DataTable tblRecItemManin = (DataTable)Session[SessionConstants.MATCHING_MATCH_SET_RESULT_CREATE_REC_ITEM_DATA];
            //DataTable tblRecItem = GetFlipedDataTable(tblRecItemManin);
            DataTable tblRecItem = tblRecItemManin;
            if (tblRecItem != null)
            {
                short RefNo = 1;
                bool IsRecItemByCurrentCombination = true;
                GLDataRecItemInfo oGLDataRecItemInfo = null;
                foreach (DataRow dr in tblRecItem.Rows)
                {
                    oGLDataRecItemInfo = GLDataRecItemDetail(dr, RefNo, out IsRecItemByCurrentCombination);

                    if (IsRecItemByCurrentCombination)
                    {
                        RefNo++;
                        oGLDataRecItemInfoCollection.Add(oGLDataRecItemInfo);
                    }

                }
                ViewState["oGLDataRecItemInfoCollection"] = oGLDataRecItemInfoCollection;
            }
        }
        else
        {
            oGLDataRecItemInfoCollection = (List<GLDataRecItemInfo>)ViewState["oGLDataRecItemInfoCollection"];
        }
        return oGLDataRecItemInfoCollection;

    }
    //private DataTable GetFlipedDataTable(DataTable DtMain)
    //{
    //    string AmountColumnName = GetAmountColumnName();
    //    Decimal FlipedValue = Convert.ToDecimal(hdnFlipedValue.Value);
    //    for (int i = 0; i < DtMain.Rows.Count; i++)
    //    {
    //        if (!string.IsNullOrEmpty(AmountColumnName))
    //        {
    //            decimal Amount;
    //            if (decimal.TryParse(DtMain.Rows[i][AmountColumnName].ToString(), out Amount))
    //                DtMain.Rows[i][AmountColumnName] = Amount * FlipedValue;
    //        }

    //    }
    //    return DtMain;
    //}
    private GLDataRecItemInfo GLDataRecItemDetail(DataRow dr, short? RefNo, out  bool IsRecItemByCurrentCombination)
    {
        bool IsRItemByCurrentCombination = true;
        GLDataRecItemInfo oGLDataRecItemInfo = new GLDataRecItemInfo();
        Hashtable htClolumnMapping = GetClolumnMapping();

        if (htClolumnMapping != null)
        {
            decimal amountLocalCurrency;
            if (htClolumnMapping[WebEnums.RecItemColumns.AmountLCCY] != null)
            {
                if (dr[htClolumnMapping[WebEnums.RecItemColumns.AmountLCCY].ToString()] != null)
                {
                    if (decimal.TryParse(dr[htClolumnMapping[WebEnums.RecItemColumns.AmountLCCY].ToString()].ToString(), out amountLocalCurrency))
                    {
                        oGLDataRecItemInfo.Amount = amountLocalCurrency;
                    }
                }
            }
            if (htClolumnMapping[WebEnums.RecItemColumns.LCCYCode] != null)
            {
                if (dr[htClolumnMapping[WebEnums.RecItemColumns.LCCYCode].ToString()] != null)
                {
                    oGLDataRecItemInfo.LocalCurrencyCode = dr[htClolumnMapping[WebEnums.RecItemColumns.LCCYCode].ToString()].ToString();
                }
            }
            //if (htClolumnMapping[WebEnums.RecItemColumns.RecItemNumber] != null)
            //{
            //    if (dr[htClolumnMapping[WebEnums.RecItemColumns.RecItemNumber].ToString()] != null)
            //    {
            //        oGLDataRecItemInfo.RecItemNumber = dr[htClolumnMapping[WebEnums.RecItemColumns.RecItemNumber].ToString()].ToString();
            //    }
            //}
            if (htClolumnMapping[WebEnums.RecItemColumns.Description] != null)
            {
                if (dr[htClolumnMapping[WebEnums.RecItemColumns.Description].ToString()] != null)
                {
                    oGLDataRecItemInfo.Comments = dr[htClolumnMapping[WebEnums.RecItemColumns.Description].ToString()].ToString();

                }
            }
            DateTime OpenDate;
            if (htClolumnMapping[WebEnums.RecItemColumns.Date] != null)
            {
                if (dr[htClolumnMapping[WebEnums.RecItemColumns.Date].ToString()] != null)
                {
                    if (DateTime.TryParse(dr[htClolumnMapping[WebEnums.RecItemColumns.Date].ToString()].ToString(), out OpenDate))
                    {
                        oGLDataRecItemInfo.OpenDate = OpenDate;
                    }
                }
            }
        }
        if (_oMatchSetSubSetCombinationInfo != null)
            oGLDataRecItemInfo.RecordSourceID = _oMatchSetSubSetCombinationInfo.MatchSetSubSetCombinationID;
        if (dr[MatchSetResultConstants.MATCH_SET_RESULT_COLUMN_NAME_REC_ITEM_NUMBER] != null)
            oGLDataRecItemInfo.RecItemNumber = Convert.ToString(dr[MatchSetResultConstants.MATCH_SET_RESULT_COLUMN_NAME_REC_ITEM_NUMBER]);
        if (dr[MatchSetResultConstants.MATCH_SET_RESULT_COLUMN_NAME_EXCEL_ROW_NUMBER] != null)
            oGLDataRecItemInfo.ExcelRowNumber = Convert.ToInt64(dr[MatchSetResultConstants.MATCH_SET_RESULT_COLUMN_NAME_EXCEL_ROW_NUMBER]);
        if (dr[MatchSetResultConstants.MATCH_SET_RESULT_COLUMN_NAME_MATCH_SET_MATCHING_SOURCE_DATA_IMPORT_ID] != null)
            oGLDataRecItemInfo.MatchSetMatchingSourceDataImportID = Convert.ToInt64(dr[MatchSetResultConstants.MATCH_SET_RESULT_COLUMN_NAME_MATCH_SET_MATCHING_SOURCE_DATA_IMPORT_ID]);
        if (dr[MatchSetResultConstants.MATCH_SET_RESULT_COLUMN_NAME_DATA_SOURCE_NAME] != null)
            oGLDataRecItemInfo.DataSourceName = Convert.ToString(dr[MatchSetResultConstants.MATCH_SET_RESULT_COLUMN_NAME_DATA_SOURCE_NAME]);
        string CloseDate = Convert.ToString(dr[MatchSetResultConstants.MATCH_SET_RESULT_COLUMN_NAME_CLOSE_DATE]);
        if (!String.IsNullOrEmpty(CloseDate))
            oGLDataRecItemInfo.CloseDate = Convert.ToDateTime(CloseDate);
        else
            oGLDataRecItemInfo.CloseDate = null;

        if (!string.IsNullOrEmpty(oGLDataRecItemInfo.RecItemNumber))
            UpdateDbInfo(oGLDataRecItemInfo, out IsRItemByCurrentCombination);

        IsRecItemByCurrentCombination = IsRItemByCurrentCombination;

        oGLDataRecItemInfo.IndexID = RefNo;
        return oGLDataRecItemInfo;

    }
    private void UpdateDbInfo(object obj, out bool flag)
    {
        bool f = true;
        List<GLDataRecurringItemScheduleInfo> oGLDataRecurringItemScheduleInfoCollection;
        GLDataRecurringItemScheduleInfo oDbGLDataRecItemInfo;
        GLDataRecItemInfo oGLDataRecItemInfo;
        GLDataRecurringItemScheduleInfo oGLDataRecurringItemScheduleInfo;
        GLDataWriteOnOffInfo oGLDataWriteOnOffInfo;
        if (obj.GetType() == typeof(GLDataRecItemInfo))
        {
            oGLDataRecItemInfo = (GLDataRecItemInfo)obj;
            oGLDataRecurringItemScheduleInfoCollection = GetGLDataRecItemsCollectionList(oGLDataRecItemInfo.RecordSourceID);
            oDbGLDataRecItemInfo = oGLDataRecurringItemScheduleInfoCollection.Find(o => o.RecItemNumber == oGLDataRecItemInfo.RecItemNumber);
            if (oDbGLDataRecItemInfo != null)
            {
                oGLDataRecItemInfo.Amount = oDbGLDataRecItemInfo.ScheduleAmount;
                oGLDataRecItemInfo.Comments = oDbGLDataRecItemInfo.Comments;
                oGLDataRecItemInfo.OpenDate = oDbGLDataRecItemInfo.OpenDate;
                oGLDataRecItemInfo.LocalCurrencyCode = oDbGLDataRecItemInfo.LocalCurrencyCode;
                f = true;
            }
            else
                f = false;

        }
        if (obj.GetType() == typeof(GLDataRecurringItemScheduleInfo))
        {
            oGLDataRecurringItemScheduleInfo = (GLDataRecurringItemScheduleInfo)obj;
            oGLDataRecurringItemScheduleInfoCollection = GetGLDataRecItemsCollectionList(oGLDataRecurringItemScheduleInfo.RecordSourceID);
            oDbGLDataRecItemInfo = oGLDataRecurringItemScheduleInfoCollection.Find(o => o.RecItemNumber == oGLDataRecurringItemScheduleInfo.RecItemNumber);
            if (oDbGLDataRecItemInfo != null)
            {
                oGLDataRecurringItemScheduleInfo.ScheduleAmount = oDbGLDataRecItemInfo.ScheduleAmount;
                oGLDataRecurringItemScheduleInfo.Comments = oDbGLDataRecItemInfo.Comments;
                oGLDataRecurringItemScheduleInfo.OpenDate = oDbGLDataRecItemInfo.OpenDate;
                oGLDataRecurringItemScheduleInfo.LocalCurrencyCode = oDbGLDataRecItemInfo.LocalCurrencyCode;
                oGLDataRecurringItemScheduleInfo.ScheduleBeginDate = oDbGLDataRecItemInfo.ScheduleBeginDate;
                oGLDataRecurringItemScheduleInfo.ScheduleEndDate = oDbGLDataRecItemInfo.ScheduleEndDate;
                oGLDataRecurringItemScheduleInfo.ScheduleName = oDbGLDataRecItemInfo.ScheduleName;
                f = true;
            }
            else
                f = false;

        }
        if (obj.GetType() == typeof(GLDataWriteOnOffInfo))
        {
            oGLDataWriteOnOffInfo = (GLDataWriteOnOffInfo)obj;
            oGLDataRecurringItemScheduleInfoCollection = GetGLDataRecItemsCollectionList(oGLDataWriteOnOffInfo.RecordSourceID);
            oDbGLDataRecItemInfo = oGLDataRecurringItemScheduleInfoCollection.Find(o => o.RecItemNumber == oGLDataWriteOnOffInfo.RecItemNumber);
            if (oDbGLDataRecItemInfo != null)
            {
                oGLDataWriteOnOffInfo.Amount = oDbGLDataRecItemInfo.ScheduleAmount;
                oGLDataWriteOnOffInfo.Comments = oDbGLDataRecItemInfo.Comments;
                oGLDataWriteOnOffInfo.OpenDate = oDbGLDataRecItemInfo.OpenDate;
                oGLDataWriteOnOffInfo.LocalCurrencyCode = oDbGLDataRecItemInfo.LocalCurrencyCode;
                oGLDataWriteOnOffInfo.WriteOnOffID = oDbGLDataRecItemInfo.WriteOnOffID;
                f = true;
            }
            else
                f = false;
        }

        flag = f;

    }
    protected void CreateNormalRecItems()
    {
        try
        {

            List<GLDataRecItemInfo> oUpdatedGLDataRecItemInfoCollection;
            List<GLDataRecItemInfo> oGLDataRecItemInfoCollection = new List<GLDataRecItemInfo>();
            GLDataRecItemInfo oGLDataRecItemInfo = null;

            foreach (GridDataItem item in rgCreateRecItem.SelectedItems)
            {
                oGLDataRecItemInfo = GetGLDataRecItemDetail(item);
                oGLDataRecItemInfoCollection.Add(oGLDataRecItemInfo);

            }
            IDataImport oDataImportClient = RemotingHelper.GetDataImportObject();
            int rowsAffected;
            MasterPageBase oMasterPageBase = (MasterPageBase)this.Master.Master;
            if (oGLDataRecItemInfoCollection.Count > 0)
            {
                oDataImportClient.InsertMatchingGLDataRecItem(oGLDataRecItemInfoCollection, out rowsAffected, Helper.GetAppUserInfo());
                oMasterPageBase.ShowConfirmationMessage(2339);


                DataTable tblRecItem = (DataTable)Session[SessionConstants.MATCHING_MATCH_SET_RESULT_CREATE_REC_ITEM_DATA];

                oUpdatedGLDataRecItemInfoCollection = GetGLDataRecItemInfoCollection();


                for (int i = 0; i < oGLDataRecItemInfoCollection.Count; i++)
                {
                    //DataRow dr = tblRecItem.AsEnumerable().Where(r => (Int64)r[MatchSetResultConstants.MATCH_SET_RESULT_COLUMN_NAME_MATCH_SET_MATCHING_SOURCE_DATA_IMPORT_ID]== oGLDataRecItemInfoCollection[i].MatchSetMatchingSourceDataImportID && (Int64)r[MatchSetResultConstants.MATCH_SET_RESULT_COLUMN_NAME_EXCEL_ROW_NUMBER] == oGLDataRecItemInfoCollection[i].ExcelRowNumber).First();
                    //dr[MatchSetResultConstants.MATCH_SET_RESULT_COLUMN_NAME_REC_ITEM_NUMBER] = oGLDataRecItemInfoCollection[i].RecItemNumber;

                    long excelRowNumber;
                    long matchSetMatchingSourceDataImportID;
                    string filterString = MatchSetResultConstants.MATCH_SET_RESULT_COLUMN_NAME_EXCEL_ROW_NUMBER + "={0} AND " +
                        MatchSetResultConstants.MATCH_SET_RESULT_COLUMN_NAME_MATCH_SET_MATCHING_SOURCE_DATA_IMPORT_ID + "={1}";
                    DataRow[] dr = null;
                    if (long.TryParse(oGLDataRecItemInfoCollection[i].ExcelRowNumber.ToString(), out excelRowNumber) &&
                        long.TryParse(oGLDataRecItemInfoCollection[i].MatchSetMatchingSourceDataImportID.ToString(), out matchSetMatchingSourceDataImportID))
                    {
                        dr = tblRecItem.Select(string.Format(filterString, excelRowNumber, matchSetMatchingSourceDataImportID));

                        //DataRow dr = tblRecItem.AsEnumerable().Where(r => (Int64)r[MatchSetResultConstants.MATCH_SET_RESULT_COLUMN_NAME_MATCH_SET_MATCHING_SOURCE_DATA_IMPORT_ID] == oGLDataRecurringItemScheduleInfoCollection[i].MatchSetMatchingSourceDataImportID && (Int64)r[MatchSetResultConstants.MATCH_SET_RESULT_COLUMN_NAME_EXCEL_ROW_NUMBER] == oGLDataRecurringItemScheduleInfoCollection[i].ExcelRowNumber).First();
                        if (dr.Length > 0)
                            dr[0][MatchSetResultConstants.MATCH_SET_RESULT_COLUMN_NAME_REC_ITEM_NUMBER] = oGLDataRecItemInfoCollection[i].RecItemNumber;
                    }

                    GLDataRecItemInfo oGLDataRecItemInfoTemp;
                    oGLDataRecItemInfoTemp = oUpdatedGLDataRecItemInfoCollection.Find(O => O.IndexID == oGLDataRecItemInfoCollection[i].IndexID);
                    oGLDataRecItemInfoTemp.RecItemNumber = oGLDataRecItemInfoCollection[i].RecItemNumber;
                    oGLDataRecItemInfoTemp.OpenDate = oGLDataRecItemInfoCollection[i].OpenDate;
                    oGLDataRecItemInfoTemp.Amount = oGLDataRecItemInfoCollection[i].Amount;
                    oGLDataRecItemInfoTemp.LocalCurrencyCode = oGLDataRecItemInfoCollection[i].LocalCurrencyCode;
                    oGLDataRecItemInfoTemp.Comments = oGLDataRecItemInfoCollection[i].Comments;
                }
                ViewState["oGLDataRecItemInfoCollection"] = oUpdatedGLDataRecItemInfoCollection;
                BindNormalItems(oUpdatedGLDataRecItemInfoCollection, false);

            }
            else
            {
                oMasterPageBase.ShowConfirmationMessage(2013);
                //oMasterPageBase.HideMessage();
            }

        }
        catch (Exception ex)
        {
            Helper.ShowErrorMessage(this, ex);
        }
    }
    private void BindNormalItems(List<GLDataRecItemInfo> OItemlist, bool IsfromNeedDatasource)
    {
        rgCreateRecItem.MasterTableView.DataSource = OItemlist;
        rgCreateRecItem.ClientSettings.ClientEvents.OnRowSelecting = "Selecting";
        if (!IsfromNeedDatasource)
            rgCreateRecItem.DataBind();
    }
    protected void rgCreateRecItem_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    {
        try
        {
            // Check in Dll Type value
            if (hdnIsBindNormalItemGrid.Value == "1")
                BindNormalItems(GetGLDataRecItemInfoCollection(), true);
        }
        catch (Exception ex)
        {
            Helper.ShowErrorMessage(this, ex);
        }

    }
    protected void rgCreateRecItem_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {
        if (e.Item.ItemType == Telerik.Web.UI.GridItemType.Item ||
           e.Item.ItemType == Telerik.Web.UI.GridItemType.AlternatingItem)
        {
            GLDataRecItemInfo oGLDataRecItemInfo = (GLDataRecItemInfo)e.Item.DataItem;
            ExCalendar calDate = e.Item.FindControl("calDate") as ExCalendar;
            ExTextBox txtComments = (ExTextBox)e.Item.FindControl("txtComments");
            DropDownList ddlLocalCurrency = (DropDownList)e.Item.FindControl("ddlLocalCurrency");
            TextBox txtAmountLCCY = (TextBox)e.Item.FindControl("txtAmountLCCY");
            CheckBox checkBox = (CheckBox)(e.Item as GridDataItem)["CheckboxSelectColumn"].Controls[0];
            ExLabel lblError = (ExLabel)e.Item.FindControl("lblError");
            ExLabel lblErrorSign = (ExLabel)e.Item.FindControl("lblErrorSign");
            ExLabel lblRecItemNo = (ExLabel)e.Item.FindControl("lblRecItemNo");
            ExLabel lblDataSourceName = (ExLabel)e.Item.FindControl("lblDataSourceName");
            (e.Item as GridDataItem)["RecordSourceID"].Text = oGLDataRecItemInfo.RecordSourceID.ToString();
            (e.Item as GridDataItem)["MatchSetMatchingSourceDataImportID"].Text = oGLDataRecItemInfo.MatchSetMatchingSourceDataImportID.ToString();
            (e.Item as GridDataItem)["ExcelRowNumber"].Text = oGLDataRecItemInfo.ExcelRowNumber.ToString();
            (e.Item as GridDataItem)["IndexID"].Text = oGLDataRecItemInfo.IndexID.ToString();


            //StringBuilder oSBError = new StringBuilder();
            string errorFormat = Helper.GetLabelIDValue(1774);
            calDate.Text = Helper.GetDisplayDateForCalendar(oGLDataRecItemInfo.OpenDate);
            string description = oGLDataRecItemInfo.Comments;
            if (!string.IsNullOrEmpty(description))
            {
                txtComments.Text = description;
            }
            FillDdlLccy(ddlLocalCurrency);
            string localCurrencyCode = oGLDataRecItemInfo.LocalCurrencyCode;
            ddlLocalCurrency.SelectedValue = localCurrencyCode;
            decimal amountLocalCurrency = oGLDataRecItemInfo.Amount.Value;
            if (amountLocalCurrency != 0)
            {
                txtAmountLCCY.Text = Helper.GetDisplayDecimalValue(amountLocalCurrency);
            }
            lblRecItemNo.Text = Helper.GetDisplayStringValue(oGLDataRecItemInfo.RecItemNumber);
            lblDataSourceName.Text = Helper.GetDisplayStringValue(oGLDataRecItemInfo.DataSourceName);

            bool isEditMode = this.FormMode == WebEnums.FormMode.Edit;
            if (isEditMode)
            {
                bool IsCloseDateNullOrEmpty = true;
                if (oGLDataRecItemInfo.CloseDate.HasValue)
                    IsCloseDateNullOrEmpty = String.IsNullOrEmpty(oGLDataRecItemInfo.CloseDate.ToString());
                bool isEnabled = (String.IsNullOrEmpty(oGLDataRecItemInfo.RecItemNumber) && IsCloseDateNullOrEmpty);
                if (!isEnabled)
                {
                    checkBox.Enabled = false;
                    Sel.Value += e.Item.ItemIndex.ToString() + ":";
                    ddlLocalCurrency.Enabled = false;
                    txtAmountLCCY.Enabled = false;
                    calDate.Enabled = false;
                    txtComments.Enabled = false;
                }
                else
                {
                    ddlLocalCurrency.Enabled = true;
                    txtAmountLCCY.Enabled = true;
                    checkBox.Enabled = true;
                    calDate.Enabled = true;
                    txtComments.Enabled = true;
                }
            }
            else
            {
                checkBox.Enabled = false;
                Sel.Value += e.Item.ItemIndex.ToString() + ":";
                ddlLocalCurrency.Enabled = false;
                txtAmountLCCY.Enabled = false;
                calDate.Enabled = false;
                txtComments.Enabled = false;

            }
        }

    }
    protected void cvValidateNormalRecItems_ServerValidate(object source, ServerValidateEventArgs args)
    {
        try
        {
            ExCustomValidator cv = (ExCustomValidator)source;
            bool isErrorExist = false;
            foreach (GridDataItem item in rgCreateRecItem.SelectedItems)
            {
                bool isError = false;
                StringBuilder oSBError = new StringBuilder();
                string errorFormat = Helper.GetLabelIDValue(1774);
                ExCalendar calDate = item.FindControl("calDate") as ExCalendar;
                DateTime openDate;
                if (!string.IsNullOrEmpty(calDate.Text))
                {
                    if (DateTime.TryParse(calDate.Text, out openDate))
                    {
                        if (openDate > Convert.ToDateTime(DateTime.Now))
                        {
                            isError = true;
                            oSBError.Append(Helper.GetErrorMessage(WebEnums.FieldType.DateCompareField, 1511, 2062));
                            oSBError.Append("<br/>");
                        }
                    }
                    else
                    {
                        isError = true;
                        oSBError.Append(Helper.GetErrorMessage(WebEnums.FieldType.InvalidDate, 1511));
                        oSBError.Append("<br/>");

                    }
                }
                else
                {
                    isError = true;
                    oSBError.Append(Helper.GetErrorMessage(WebEnums.FieldType.MandatoryField, 1511));
                    oSBError.Append("<br/>");

                }

                DropDownList ddlLocalCurrency = (DropDownList)item.FindControl("ddlLocalCurrency");
                string localCurrencyCode = ddlLocalCurrency.SelectedValue;
                if (!(string.IsNullOrEmpty(localCurrencyCode) || localCurrencyCode == WebConstants.SELECT_ONE))
                {
                    IUtility oUtilityClient = RemotingHelper.GetUtilityObject();
                    try
                    {
                        decimal baseCurrencyExRate = 1;
                        if (!string.IsNullOrEmpty(this.CurrentBCCY))
                            baseCurrencyExRate = oUtilityClient.GetCurrencyExchangeRate(SessionHelper.CurrentReconciliationPeriodID.Value
                                , localCurrencyCode, this.CurrentBCCY, this._IsMultiCurrencyEnabled, Helper.GetAppUserInfo());
                        (item as GridDataItem)["BaseCurrencyExchangeRate"].Text = baseCurrencyExRate.ToString();

                    }
                    catch (Exception)
                    {

                        isError = true;
                        oSBError.Append(Helper.GetLabelIDValue(5000098));
                        oSBError.Append("<br/>");

                    }

                    try
                    {
                        decimal baseCurrencyExRate = oUtilityClient.GetCurrencyExchangeRate(SessionHelper.CurrentReconciliationPeriodID.Value
                            , localCurrencyCode, SessionHelper.ReportingCurrencyCode, this._IsMultiCurrencyEnabled, Helper.GetAppUserInfo());
                        (item as GridDataItem)["ReportingCurrencyExchangeRate"].Text = baseCurrencyExRate.ToString();

                    }
                    catch (Exception)
                    {
                        isError = true;
                        oSBError.Append(Helper.GetLabelIDValue(5000099));
                        oSBError.Append("<br/>");

                    }
                }
                else
                {
                    isError = true;
                    oSBError.Append(Helper.GetErrorMessage(WebEnums.FieldType.MandatoryField, 1409));
                    oSBError.Append("<br/>");

                }

                TextBox txtAmountLCCY = (TextBox)item.FindControl("txtAmountLCCY");
                decimal amountLocalCurrency = 0;
                if (!string.IsNullOrEmpty(txtAmountLCCY.Text))
                {
                    if (decimal.TryParse(txtAmountLCCY.Text, out amountLocalCurrency))
                    {
                        amountLocalCurrency = Convert.ToDecimal(txtAmountLCCY.Text);
                    }
                    else
                    {
                        isError = true;
                        oSBError.Append(Helper.GetErrorMessage(WebEnums.FieldType.InvalidNumericField, 1510));
                        oSBError.Append("<br/>");
                    }
                }
                else
                {
                    isError = true;
                    oSBError.Append(Helper.GetErrorMessage(WebEnums.FieldType.MandatoryField, 1510));
                    oSBError.Append("<br/>");
                }
                ExLabel lblError = (ExLabel)item.FindControl("lblError");
                ExLabel lblErrorSign = (ExLabel)item.FindControl("lblErrorSign");
                lblError.Text = oSBError.ToString();
                if (isError)
                {
                    lblError.Visible = true;
                    lblErrorSign.Visible = true;
                    isErrorExist = true;
                }
                else
                {
                    lblError.Visible = false;
                    lblErrorSign.Visible = false;
                }
            }
            if (isErrorExist)
            {
                args.IsValid = false;
            }

        }
        catch (ARTException ex)
        {
            Helper.ShowErrorMessage((PageBase)this.Page, ex);
        }
        catch (Exception ex)
        {
            Helper.ShowErrorMessage((PageBase)this.Page, ex);
        }
    }
    private GLDataRecItemInfo GetGLDataRecItemDetail(GridDataItem item)
    {
        long RecordSourceID = Convert.ToInt64(item["RecordSourceID"].Text);
        long MatchSetMatchingSourceDataImportID = Convert.ToInt64(item["MatchSetMatchingSourceDataImportID"].Text);
        long ExcelRowNumber = Convert.ToInt64(item["ExcelRowNumber"].Text);
        GLDataRecItemInfo oGLDataRecItemInfo = new GLDataRecItemInfo();
        UserHdrInfo oUserHdrInfo = SessionHelper.GetCurrentUser();
        oGLDataRecItemInfo.AddedBy = oUserHdrInfo.LoginID;
        oGLDataRecItemInfo.AddedByUserID = oUserHdrInfo.UserID;
        TextBox txtAmountLCCY = (TextBox)item.FindControl("txtAmountLCCY");
        decimal amountLocalCurrency = Convert.ToDecimal(txtAmountLCCY.Text);
        oGLDataRecItemInfo.Amount = amountLocalCurrency;
        decimal baseCurrencyExchangeRate = Convert.ToDecimal(item["BaseCurrencyExchangeRate"].Text);
        oGLDataRecItemInfo.AmountBaseCurrency = Math.Round(amountLocalCurrency * baseCurrencyExchangeRate, 2);
        decimal reportingCurrencyExchangeRate = Convert.ToDecimal(item["ReportingCurrencyExchangeRate"].Text);
        oGLDataRecItemInfo.AmountReportingCurrency = Math.Round(amountLocalCurrency * reportingCurrencyExchangeRate, 2);
        ExTextBox txtComments = (ExTextBox)item.FindControl("txtComments");
        oGLDataRecItemInfo.Comments = txtComments.Text;
        oGLDataRecItemInfo.DateAdded = DateTime.Now;
        oGLDataRecItemInfo.GLDataID = this._GLDataID;
        oGLDataRecItemInfo.IsAttachmentAvailable = false;
        DropDownList ddlLocalCurrency = (DropDownList)item.FindControl("ddlLocalCurrency");
        oGLDataRecItemInfo.LocalCurrencyCode = ddlLocalCurrency.SelectedValue;
        ExCalendar calDate = item.FindControl("calDate") as ExCalendar;
        oGLDataRecItemInfo.OpenDate = Convert.ToDateTime(calDate.Text);

        string SelectedCategoryTypeID = "0";
        string[] ArrSelected = ddlRecCategoryType.SelectedValue.Split('~');
        if (ArrSelected.Length > 0)
            SelectedCategoryTypeID = ArrSelected[1];
        oGLDataRecItemInfo.ReconciliationCategoryTypeID = Convert.ToInt16(SelectedCategoryTypeID);
        oGLDataRecItemInfo.ReconciliationCategoryID = Convert.ToInt16(ddlRecCategory.SelectedValue);
        oGLDataRecItemInfo.RecordSourceID = RecordSourceID;
        oGLDataRecItemInfo.DataImportID = null;
        oGLDataRecItemInfo.RecordSourceTypeID = (short)ARTEnums.RecordSourceType.Matching;
        oGLDataRecItemInfo.IndexID = Convert.ToInt16(item["IndexID"].Text); ;
        oGLDataRecItemInfo.MatchSetMatchingSourceDataImportID = MatchSetMatchingSourceDataImportID;
        oGLDataRecItemInfo.ExcelRowNumber = ExcelRowNumber;
        return oGLDataRecItemInfo;
    }
    #endregion
    #region ScheduleItems
    private List<GLDataRecurringItemScheduleInfo> GetGLDataScheduleRecItemInfoCollection()
    {

        List<GLDataRecurringItemScheduleInfo> oGLDataRecurringItemScheduleInfoCollection = null;
        if (ViewState["oGLDataRecurringItemScheduleInfoCollection"] == null)
        {
            oGLDataRecurringItemScheduleInfoCollection = new List<GLDataRecurringItemScheduleInfo>();
            //DataTable tblRecItem = (DataTable)Session[SessionConstants.MATCHING_MATCH_SET_RESULT_CREATE_REC_ITEM_DATA];
            DataTable tblRecItemManin = (DataTable)Session[SessionConstants.MATCHING_MATCH_SET_RESULT_CREATE_REC_ITEM_DATA];
            //DataTable tblRecItem = GetFlipedDataTable(tblRecItemManin);
            DataTable tblRecItem = tblRecItemManin;
            if (tblRecItem != null)
            {
                bool IsRecItemByCurrentCombination = true;
                short RefNo = 1;
                GLDataRecurringItemScheduleInfo oGLDataRecurringItemScheduleInfo = null;
                foreach (DataRow dr in tblRecItem.Rows)
                {
                    oGLDataRecurringItemScheduleInfo = GLDataScheduleRecItemDetail(dr, RefNo, out IsRecItemByCurrentCombination);

                    if (IsRecItemByCurrentCombination)
                    {
                        RefNo++;
                        oGLDataRecurringItemScheduleInfoCollection.Add(oGLDataRecurringItemScheduleInfo);
                    }

                }
                ViewState["oGLDataRecurringItemScheduleInfoCollection"] = oGLDataRecurringItemScheduleInfoCollection;
            }
        }
        else
        {
            oGLDataRecurringItemScheduleInfoCollection = (List<GLDataRecurringItemScheduleInfo>)ViewState["oGLDataRecurringItemScheduleInfoCollection"];
        }
        return oGLDataRecurringItemScheduleInfoCollection;

    }
    private GLDataRecurringItemScheduleInfo GLDataScheduleRecItemDetail(DataRow dr, short? RefNo, out bool IsRecItemByCurrentCombination)
    {
        bool IsRItemByCurrentCombination = true;
        GLDataRecurringItemScheduleInfo oGLDataRecurringItemScheduleInfo = new GLDataRecurringItemScheduleInfo();
        Hashtable htClolumnMapping = GetClolumnMapping();
        if (htClolumnMapping != null)
        {
            decimal amountLocalCurrency;
            if (htClolumnMapping[WebEnums.RecItemColumns.AmountLCCY] != null)
            {
                if (dr[htClolumnMapping[WebEnums.RecItemColumns.AmountLCCY].ToString()] != null)
                {
                    if (decimal.TryParse(dr[htClolumnMapping[WebEnums.RecItemColumns.AmountLCCY].ToString()].ToString(), out amountLocalCurrency))
                    {
                        oGLDataRecurringItemScheduleInfo.ScheduleAmount = amountLocalCurrency;
                    }
                }
            }
            if (htClolumnMapping[WebEnums.RecItemColumns.LCCYCode] != null)
            {
                if (dr[htClolumnMapping[WebEnums.RecItemColumns.LCCYCode].ToString()] != null)
                {
                    oGLDataRecurringItemScheduleInfo.LocalCurrencyCode = dr[htClolumnMapping[WebEnums.RecItemColumns.LCCYCode].ToString()].ToString();
                }
            }
            //if (htClolumnMapping[WebEnums.RecItemColumns.RecItemNumber] != null)
            //{
            //    if (dr[htClolumnMapping[WebEnums.RecItemColumns.RecItemNumber].ToString()] != null)
            //    {
            //        oGLDataRecurringItemScheduleInfo.RecItemNumber = dr[htClolumnMapping[WebEnums.RecItemColumns.RecItemNumber].ToString()].ToString();
            //    }
            //}
            if (htClolumnMapping[WebEnums.RecItemColumns.Description] != null)
            {
                if (dr[htClolumnMapping[WebEnums.RecItemColumns.Description].ToString()] != null)
                {
                    oGLDataRecurringItemScheduleInfo.Comments = dr[htClolumnMapping[WebEnums.RecItemColumns.Description].ToString()].ToString();

                }
            }
            if (htClolumnMapping[WebEnums.RecItemColumns.ScheduleName] != null)
            {
                if (dr[htClolumnMapping[WebEnums.RecItemColumns.ScheduleName].ToString()] != null)
                {
                    oGLDataRecurringItemScheduleInfo.ScheduleName = dr[htClolumnMapping[WebEnums.RecItemColumns.ScheduleName].ToString()].ToString();

                }
            }
            DateTime OpenDate;
            if (htClolumnMapping[WebEnums.RecItemColumns.Date] != null)
            {
                if (dr[htClolumnMapping[WebEnums.RecItemColumns.Date].ToString()] != null)
                {
                    if (DateTime.TryParse(dr[htClolumnMapping[WebEnums.RecItemColumns.Date].ToString()].ToString(), out OpenDate))
                    {
                        oGLDataRecurringItemScheduleInfo.OpenDate = OpenDate;
                    }
                }
            }
            DateTime ScheduleBeginDate;
            if (htClolumnMapping[WebEnums.RecItemColumns.ScheduleBeginDate] != null)
            {
                if (dr[htClolumnMapping[WebEnums.RecItemColumns.ScheduleBeginDate].ToString()] != null)
                {
                    if (DateTime.TryParse(dr[htClolumnMapping[WebEnums.RecItemColumns.ScheduleBeginDate].ToString()].ToString(), out ScheduleBeginDate))
                    {
                        oGLDataRecurringItemScheduleInfo.ScheduleBeginDate = ScheduleBeginDate;
                    }
                }
            }
            DateTime ScheduleEndDate;
            if (htClolumnMapping[WebEnums.RecItemColumns.ScheduleEndDate] != null)
            {
                if (dr[htClolumnMapping[WebEnums.RecItemColumns.ScheduleEndDate].ToString()] != null)
                {
                    if (DateTime.TryParse(dr[htClolumnMapping[WebEnums.RecItemColumns.ScheduleEndDate].ToString()].ToString(), out ScheduleEndDate))
                    {
                        oGLDataRecurringItemScheduleInfo.ScheduleEndDate = ScheduleEndDate;
                    }
                }
            }

        }

        if (_oMatchSetSubSetCombinationInfo != null)
            oGLDataRecurringItemScheduleInfo.RecordSourceID = _oMatchSetSubSetCombinationInfo.MatchSetSubSetCombinationID;
        if (dr[MatchSetResultConstants.MATCH_SET_RESULT_COLUMN_NAME_REC_ITEM_NUMBER] != null)
            oGLDataRecurringItemScheduleInfo.RecItemNumber = Convert.ToString(dr[MatchSetResultConstants.MATCH_SET_RESULT_COLUMN_NAME_REC_ITEM_NUMBER]);
        if (dr[MatchSetResultConstants.MATCH_SET_RESULT_COLUMN_NAME_EXCEL_ROW_NUMBER] != null)
            oGLDataRecurringItemScheduleInfo.ExcelRowNumber = Convert.ToInt64(dr[MatchSetResultConstants.MATCH_SET_RESULT_COLUMN_NAME_EXCEL_ROW_NUMBER]);
        if (dr[MatchSetResultConstants.MATCH_SET_RESULT_COLUMN_NAME_MATCH_SET_MATCHING_SOURCE_DATA_IMPORT_ID] != null)
            oGLDataRecurringItemScheduleInfo.MatchSetMatchingSourceDataImportID = Convert.ToInt64(dr[MatchSetResultConstants.MATCH_SET_RESULT_COLUMN_NAME_MATCH_SET_MATCHING_SOURCE_DATA_IMPORT_ID]);
        if (dr[MatchSetResultConstants.MATCH_SET_RESULT_COLUMN_NAME_DATA_SOURCE_NAME] != null)
            oGLDataRecurringItemScheduleInfo.DataSourceName = Convert.ToString(dr[MatchSetResultConstants.MATCH_SET_RESULT_COLUMN_NAME_DATA_SOURCE_NAME]);
        string CloseDate = Convert.ToString(dr[MatchSetResultConstants.MATCH_SET_RESULT_COLUMN_NAME_CLOSE_DATE]);
        if (!String.IsNullOrEmpty(CloseDate))
            oGLDataRecurringItemScheduleInfo.CloseDate = Convert.ToDateTime(CloseDate);
        else
            oGLDataRecurringItemScheduleInfo.CloseDate = null;

        if (!string.IsNullOrEmpty(oGLDataRecurringItemScheduleInfo.RecItemNumber))
            UpdateDbInfo(oGLDataRecurringItemScheduleInfo, out IsRItemByCurrentCombination);

        IsRecItemByCurrentCombination = IsRItemByCurrentCombination;

        oGLDataRecurringItemScheduleInfo.IndexID = RefNo;
        return oGLDataRecurringItemScheduleInfo;

    }
    protected void CreateScheduleRecItems()
    {
        try
        {
            List<GLDataRecurringItemScheduleInfo> oUpdatedGLDataRecurringItemScheduleInfoCollection;
            List<GLDataRecurringItemScheduleInfo> oGLDataRecurringItemScheduleInfoCollection = new List<GLDataRecurringItemScheduleInfo>();
            GLDataRecurringItemScheduleInfo oGLDataRecurringItemScheduleInfo = null;
            foreach (GridDataItem item in rgCreateScheduleRecItem.SelectedItems)
            {
                oGLDataRecurringItemScheduleInfo = GetGLDataRecItemScheduleDetail(item);
                oGLDataRecurringItemScheduleInfoCollection.Add(oGLDataRecurringItemScheduleInfo);
            }
            IDataImport oDataImportClient = RemotingHelper.GetDataImportObject();
            int rowsAffected;
            MasterPageBase oMasterPageBase = (MasterPageBase)this.Master.Master;
            if (oGLDataRecurringItemScheduleInfoCollection.Count > 0)
            {
                oDataImportClient.InsertMatchingGLDataScheduleRecItem(oGLDataRecurringItemScheduleInfoCollection, out rowsAffected, Helper.GetAppUserInfo());
                oMasterPageBase.ShowConfirmationMessage(2339);
                DataTable tblRecItem = (DataTable)Session[SessionConstants.MATCHING_MATCH_SET_RESULT_CREATE_REC_ITEM_DATA];

                oUpdatedGLDataRecurringItemScheduleInfoCollection = GetGLDataScheduleRecItemInfoCollection();
                for (int i = 0; i < oGLDataRecurringItemScheduleInfoCollection.Count; i++)
                {
                    long excelRowNumber;
                    long matchSetMatchingSourceDataImportID;
                    string filterString = MatchSetResultConstants.MATCH_SET_RESULT_COLUMN_NAME_EXCEL_ROW_NUMBER + "={0} AND " +
                        MatchSetResultConstants.MATCH_SET_RESULT_COLUMN_NAME_MATCH_SET_MATCHING_SOURCE_DATA_IMPORT_ID + "={1}";
                    DataRow[] dr = null;
                    if (long.TryParse(oGLDataRecurringItemScheduleInfoCollection[i].ExcelRowNumber.ToString(), out excelRowNumber) &&
                        long.TryParse(oGLDataRecurringItemScheduleInfoCollection[i].MatchSetMatchingSourceDataImportID.ToString(), out matchSetMatchingSourceDataImportID))
                    {
                        dr = tblRecItem.Select(string.Format(filterString, excelRowNumber, matchSetMatchingSourceDataImportID));

                        //DataRow dr = tblRecItem.AsEnumerable().Where(r => (Int64)r[MatchSetResultConstants.MATCH_SET_RESULT_COLUMN_NAME_MATCH_SET_MATCHING_SOURCE_DATA_IMPORT_ID] == oGLDataRecurringItemScheduleInfoCollection[i].MatchSetMatchingSourceDataImportID && (Int64)r[MatchSetResultConstants.MATCH_SET_RESULT_COLUMN_NAME_EXCEL_ROW_NUMBER] == oGLDataRecurringItemScheduleInfoCollection[i].ExcelRowNumber).First();
                        if (dr.Length > 0)
                            dr[0][MatchSetResultConstants.MATCH_SET_RESULT_COLUMN_NAME_REC_ITEM_NUMBER] = oGLDataRecurringItemScheduleInfoCollection[i].RecItemNumber;
                    }


                    GLDataRecurringItemScheduleInfo oGLDataRecurringItemScheduleInfoTemp;
                    oGLDataRecurringItemScheduleInfoTemp = oUpdatedGLDataRecurringItemScheduleInfoCollection.Find(O => O.IndexID == oGLDataRecurringItemScheduleInfoCollection[i].IndexID);

                    oGLDataRecurringItemScheduleInfoTemp.RecItemNumber = oGLDataRecurringItemScheduleInfoCollection[i].RecItemNumber;
                    oGLDataRecurringItemScheduleInfoTemp.OpenDate = oGLDataRecurringItemScheduleInfoCollection[i].OpenDate;
                    oGLDataRecurringItemScheduleInfoTemp.ScheduleAmount = oGLDataRecurringItemScheduleInfoCollection[i].ScheduleAmount;
                    oGLDataRecurringItemScheduleInfoTemp.LocalCurrencyCode = oGLDataRecurringItemScheduleInfoCollection[i].LocalCurrencyCode;
                    oGLDataRecurringItemScheduleInfoTemp.Comments = oGLDataRecurringItemScheduleInfoCollection[i].Comments;
                    oGLDataRecurringItemScheduleInfoTemp.ScheduleName = oGLDataRecurringItemScheduleInfoCollection[i].ScheduleName;
                    oGLDataRecurringItemScheduleInfoTemp.ScheduleBeginDate = oGLDataRecurringItemScheduleInfoCollection[i].ScheduleBeginDate;
                    oGLDataRecurringItemScheduleInfoTemp.ScheduleEndDate = oGLDataRecurringItemScheduleInfoCollection[i].ScheduleEndDate;
                }
                ViewState["oGLDataRecurringItemScheduleInfoCollection"] = oUpdatedGLDataRecurringItemScheduleInfoCollection;
                BindScheduleItems(oUpdatedGLDataRecurringItemScheduleInfoCollection, false);
            }
            else
            {
                oMasterPageBase.ShowConfirmationMessage(2013);
                //oMasterPageBase.HideMessage();
            }

        }
        catch (Exception ex)
        {
            Helper.ShowErrorMessage(this, ex);
        }
    }
    private void ClearViewStates()
    {

        ViewState["oGLDataRecurringItemScheduleInfoCollection"] = null;
        ViewState["oGLDataRecItemInfoCollection"] = null;
        ViewState["oGLDataWriteOnOffInfoCollection"] = null;
    }
    private void BindScheduleItems(List<GLDataRecurringItemScheduleInfo> OItemlist, bool IsfromNeedDatasource)
    {
        rgCreateScheduleRecItem.MasterTableView.DataSource = OItemlist;
        rgCreateScheduleRecItem.ClientSettings.ClientEvents.OnRowSelecting = "Selecting";
        if (!IsfromNeedDatasource)
            rgCreateScheduleRecItem.DataBind();
    }
    protected void rgCreateScheduleRecItem_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    {
        try
        {
            // Check in Dll Type value
            if (hdnIsBindScheduleItemGrid.Value == "1")
                BindScheduleItems(GetGLDataScheduleRecItemInfoCollection(), true);
        }
        catch (Exception ex)
        {
            Helper.ShowErrorMessage(this, ex);
        }

    }
    protected void rgCreateScheduleRecItem_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {
        if (e.Item.ItemType == Telerik.Web.UI.GridItemType.Item ||
           e.Item.ItemType == Telerik.Web.UI.GridItemType.AlternatingItem)
        {
            GLDataRecurringItemScheduleInfo oGLDataRecurringItemScheduleInfo = (GLDataRecurringItemScheduleInfo)e.Item.DataItem;
            ExCalendar calDate = e.Item.FindControl("calDate") as ExCalendar;
            ExCalendar calScheduleBeginDate = e.Item.FindControl("calScheduleBeginDate") as ExCalendar;
            ExCalendar calScheduleEndDate = e.Item.FindControl("calScheduleEndDate") as ExCalendar;
            ExTextBox txtComments = (ExTextBox)e.Item.FindControl("txtComments");
            ExTextBox txtScheduleName = (ExTextBox)e.Item.FindControl("txtScheduleName");
            DropDownList ddlLocalCurrency = (DropDownList)e.Item.FindControl("ddlLocalCurrency");
            TextBox txtAmountLCCY = (TextBox)e.Item.FindControl("txtAmountLCCY");
            CheckBox checkBox = (CheckBox)(e.Item as GridDataItem)["CheckboxSelectColumn"].Controls[0];
            ExLabel lblError = (ExLabel)e.Item.FindControl("lblError");
            ExLabel lblErrorSign = (ExLabel)e.Item.FindControl("lblErrorSign");
            ExLabel lblRecItemNo = (ExLabel)e.Item.FindControl("lblRecItemNo");
            ExLabel lblDataSourceName = (ExLabel)e.Item.FindControl("lblDataSourceName");
            (e.Item as GridDataItem)["RecordSourceID"].Text = oGLDataRecurringItemScheduleInfo.RecordSourceID.ToString();
            (e.Item as GridDataItem)["MatchSetMatchingSourceDataImportID"].Text = oGLDataRecurringItemScheduleInfo.MatchSetMatchingSourceDataImportID.ToString();
            (e.Item as GridDataItem)["ExcelRowNumber"].Text = oGLDataRecurringItemScheduleInfo.ExcelRowNumber.ToString();
            (e.Item as GridDataItem)["IndexID"].Text = oGLDataRecurringItemScheduleInfo.IndexID.ToString();


            //StringBuilder oSBError = new StringBuilder();
            string errorFormat = Helper.GetLabelIDValue(1774);
            calDate.Text = Helper.GetDisplayDateForCalendar(oGLDataRecurringItemScheduleInfo.OpenDate);
            calScheduleBeginDate.Text = Helper.GetDisplayDateForCalendar(oGLDataRecurringItemScheduleInfo.ScheduleBeginDate);
            calScheduleEndDate.Text = Helper.GetDisplayDateForCalendar(oGLDataRecurringItemScheduleInfo.ScheduleEndDate);
            string description = oGLDataRecurringItemScheduleInfo.Comments;
            if (!string.IsNullOrEmpty(description))
            {
                txtComments.Text = description;
            }
            string ScheduleName = oGLDataRecurringItemScheduleInfo.ScheduleName;
            if (!string.IsNullOrEmpty(ScheduleName))
            {
                txtScheduleName.Text = ScheduleName;
            }
            FillDdlLccy(ddlLocalCurrency);
            string localCurrencyCode = oGLDataRecurringItemScheduleInfo.LocalCurrencyCode;
            ddlLocalCurrency.SelectedValue = localCurrencyCode;
            decimal amountLocalCurrency = 0;
            if (oGLDataRecurringItemScheduleInfo.ScheduleAmount.HasValue)
                amountLocalCurrency = oGLDataRecurringItemScheduleInfo.ScheduleAmount.Value;
            if (amountLocalCurrency != 0)
            {
                txtAmountLCCY.Text = Helper.GetDisplayDecimalValue(amountLocalCurrency);
            }
            lblRecItemNo.Text = Helper.GetDisplayStringValue(oGLDataRecurringItemScheduleInfo.RecItemNumber);
            lblDataSourceName.Text = Helper.GetDisplayStringValue(oGLDataRecurringItemScheduleInfo.DataSourceName);


            bool isEditMode = this.FormMode == WebEnums.FormMode.Edit;
            if (isEditMode)
            {
                bool IsCloseDateNullOrEmpty = true;
                if (oGLDataRecurringItemScheduleInfo.CloseDate.HasValue)
                    IsCloseDateNullOrEmpty = String.IsNullOrEmpty(oGLDataRecurringItemScheduleInfo.CloseDate.ToString());
                bool isEnabled = (String.IsNullOrEmpty(oGLDataRecurringItemScheduleInfo.RecItemNumber) && IsCloseDateNullOrEmpty);
                if (!isEnabled)
                {
                    checkBox.Enabled = false;
                    Sel.Value += e.Item.ItemIndex.ToString() + ":";
                    ddlLocalCurrency.Enabled = false;
                    txtAmountLCCY.Enabled = false;
                    calDate.Enabled = false;
                    txtComments.Enabled = false;
                    calScheduleBeginDate.Enabled = false;
                    calScheduleEndDate.Enabled = false;
                    txtScheduleName.Enabled = false;
                }
                else
                {
                    ddlLocalCurrency.Enabled = true;
                    txtAmountLCCY.Enabled = true;
                    checkBox.Enabled = true;
                    calDate.Enabled = true;
                    txtComments.Enabled = true;
                    calScheduleBeginDate.Enabled = true;
                    calScheduleEndDate.Enabled = true;
                    txtScheduleName.Enabled = true;
                }
            }
            else
            {
                checkBox.Enabled = false;
                Sel.Value += e.Item.ItemIndex.ToString() + ":";
                ddlLocalCurrency.Enabled = false;
                txtAmountLCCY.Enabled = false;
                calDate.Enabled = false;
                txtComments.Enabled = false;
                calScheduleBeginDate.Enabled = false;
                calScheduleEndDate.Enabled = false;
                txtScheduleName.Enabled = false;
            }
        }
    }
    protected void cvValidateScheduleRecItems_ServerValidate(object source, ServerValidateEventArgs args)
    {
        try
        {
            ExCustomValidator cv = (ExCustomValidator)source;
            bool isErrorExist = false;
            foreach (GridDataItem item in rgCreateScheduleRecItem.SelectedItems)
            {
                bool isError = false;
                StringBuilder oSBError = new StringBuilder();
                string errorFormat = Helper.GetLabelIDValue(1774);
                ExCalendar calDate = item.FindControl("calDate") as ExCalendar;
                DateTime openDate;
                if (!string.IsNullOrEmpty(calDate.Text))
                {
                    if (DateTime.TryParse(calDate.Text, out openDate))
                    {
                        if (openDate > Convert.ToDateTime(DateTime.Now))
                        {
                            isError = true;
                            oSBError.Append(Helper.GetErrorMessage(WebEnums.FieldType.DateCompareField, 1511, 2062));
                            oSBError.Append("<br/>");
                        }
                    }
                    else
                    {
                        isError = true;
                        oSBError.Append(Helper.GetErrorMessage(WebEnums.FieldType.InvalidDate, 1511));
                        oSBError.Append("<br/>");

                    }
                }
                else
                {
                    isError = true;
                    oSBError.Append(Helper.GetErrorMessage(WebEnums.FieldType.MandatoryField, 1511));
                    oSBError.Append("<br/>");

                }
                ExCalendar calScheduleBeginDate = item.FindControl("calScheduleBeginDate") as ExCalendar;
                DateTime ScheduleBeginDate;
                if (!string.IsNullOrEmpty(calScheduleBeginDate.Text))
                {
                    if (DateTime.TryParse(calScheduleBeginDate.Text, out ScheduleBeginDate))
                    {
                        if (ScheduleBeginDate > SessionHelper.CurrentReconciliationPeriodEndDate)
                        {
                            isError = true;
                            oSBError.Append(Helper.GetErrorMessage(WebEnums.FieldType.DateCompareField, 2063, 1978));
                            oSBError.Append("<br/>");
                        }
                    }
                    else
                    {
                        isError = true;
                        oSBError.Append(Helper.GetErrorMessage(WebEnums.FieldType.InvalidDate, 2063));
                        oSBError.Append("<br/>");

                    }
                }
                else
                {
                    isError = true;
                    oSBError.Append(Helper.GetErrorMessage(WebEnums.FieldType.MandatoryField, 2063));
                    oSBError.Append("<br/>");

                }




                ExCalendar calScheduleEndDate = item.FindControl("calScheduleEndDate") as ExCalendar;
                DateTime ScheduleEndDate;
                if (!string.IsNullOrEmpty(calScheduleEndDate.Text))
                {
                    if (DateTime.TryParse(calScheduleEndDate.Text, out ScheduleEndDate))
                    {
                        if (SessionHelper.CurrentReconciliationPeriodEndDate > ScheduleEndDate)
                        {
                            isError = true;
                            oSBError.Append(Helper.GetErrorMessage(WebEnums.FieldType.DateCompareFieldGreaterThan, 1792, 1978));
                            oSBError.Append("<br/>");
                        }
                    }
                    else
                    {
                        isError = true;
                        oSBError.Append(Helper.GetErrorMessage(WebEnums.FieldType.InvalidDate, 1792));
                        oSBError.Append("<br/>");

                    }
                }
                else
                {
                    isError = true;
                    oSBError.Append(Helper.GetErrorMessage(WebEnums.FieldType.MandatoryField, 1792));
                    oSBError.Append("<br/>");

                }

                DropDownList ddlLocalCurrency = (DropDownList)item.FindControl("ddlLocalCurrency");
                string localCurrencyCode = ddlLocalCurrency.SelectedValue;
                if (!(string.IsNullOrEmpty(localCurrencyCode) || localCurrencyCode == WebConstants.SELECT_ONE))
                {
                    IUtility oUtilityClient = RemotingHelper.GetUtilityObject();
                    try
                    {
                        decimal baseCurrencyExRate = 1;
                        if (!string.IsNullOrEmpty(this.CurrentBCCY))
                            baseCurrencyExRate = oUtilityClient.GetCurrencyExchangeRate(SessionHelper.CurrentReconciliationPeriodID.Value
                                , localCurrencyCode, this.CurrentBCCY, this._IsMultiCurrencyEnabled, Helper.GetAppUserInfo());
                        (item as GridDataItem)["BaseCurrencyExchangeRate"].Text = baseCurrencyExRate.ToString();

                    }
                    catch (Exception)
                    {

                        isError = true;
                        oSBError.Append(Helper.GetLabelIDValue(5000098));
                        oSBError.Append("<br/>");

                    }

                    try
                    {
                        decimal baseCurrencyExRate = oUtilityClient.GetCurrencyExchangeRate(SessionHelper.CurrentReconciliationPeriodID.Value
                            , localCurrencyCode, SessionHelper.ReportingCurrencyCode, this._IsMultiCurrencyEnabled, Helper.GetAppUserInfo());
                        (item as GridDataItem)["ReportingCurrencyExchangeRate"].Text = baseCurrencyExRate.ToString();

                    }
                    catch (Exception)
                    {
                        isError = true;
                        oSBError.Append(Helper.GetLabelIDValue(5000099));
                        oSBError.Append("<br/>");

                    }
                }
                else
                {
                    isError = true;
                    oSBError.Append(Helper.GetErrorMessage(WebEnums.FieldType.MandatoryField, 1409));
                    oSBError.Append("<br/>");

                }

                TextBox txtAmountLCCY = (TextBox)item.FindControl("txtAmountLCCY");
                decimal amountLocalCurrency = 0;
                if (!string.IsNullOrEmpty(txtAmountLCCY.Text))
                {
                    if (decimal.TryParse(txtAmountLCCY.Text, out amountLocalCurrency))
                    {
                        amountLocalCurrency = Convert.ToDecimal(txtAmountLCCY.Text);
                    }
                    else
                    {
                        isError = true;
                        oSBError.Append(Helper.GetErrorMessage(WebEnums.FieldType.InvalidNumericField, 1510));
                        oSBError.Append("<br/>");
                    }
                }
                else
                {
                    isError = true;
                    oSBError.Append(Helper.GetErrorMessage(WebEnums.FieldType.MandatoryField, 1510));
                    oSBError.Append("<br/>");
                }



                ExTextBox txtScheduleName = (ExTextBox)item.FindControl("txtScheduleName");

                if (string.IsNullOrEmpty(txtScheduleName.Text))
                {
                    isError = true;
                    oSBError.Append(Helper.GetErrorMessage(WebEnums.FieldType.MandatoryField, 2052));
                    oSBError.Append("<br/>");
                }


                ExLabel lblError = (ExLabel)item.FindControl("lblError");
                ExLabel lblErrorSign = (ExLabel)item.FindControl("lblErrorSign");
                lblError.Text = oSBError.ToString();
                if (isError)
                {
                    lblError.Visible = true;
                    lblErrorSign.Visible = true;
                    isErrorExist = true;
                }
                else
                {
                    lblError.Visible = false;
                    lblErrorSign.Visible = false;
                }
            }
            if (isErrorExist)
            {
                args.IsValid = false;
            }

        }
        catch (ARTException ex)
        {
            Helper.ShowErrorMessage((PageBase)this.Page, ex);
        }
        catch (Exception ex)
        {
            Helper.ShowErrorMessage((PageBase)this.Page, ex);
        }
    }
    private GLDataRecurringItemScheduleInfo GetGLDataRecItemScheduleDetail(GridDataItem item)
    {
        long RecordSourceID = Convert.ToInt64(item["RecordSourceID"].Text);
        long MatchSetMatchingSourceDataImportID = Convert.ToInt64(item["MatchSetMatchingSourceDataImportID"].Text);
        long ExcelRowNumber = Convert.ToInt64(item["ExcelRowNumber"].Text);
        GLDataRecurringItemScheduleInfo oGLDataRecurringItemScheduleInfo = new GLDataRecurringItemScheduleInfo();
        UserHdrInfo oUserHdrInfo = SessionHelper.GetCurrentUser();
        IUtility oUtilityClient = RemotingHelper.GetUtilityObject();
        oGLDataRecurringItemScheduleInfo.AddedBy = oUserHdrInfo.LoginID;
        oGLDataRecurringItemScheduleInfo.AddedByUserID = oUserHdrInfo.UserID;
        TextBox txtAmountLCCY = (TextBox)item.FindControl("txtAmountLCCY");
        decimal amountLocalCurrency = Convert.ToDecimal(txtAmountLCCY.Text);
        oGLDataRecurringItemScheduleInfo.ScheduleAmount = amountLocalCurrency;
        DropDownList ddlLocalCurrency = (DropDownList)item.FindControl("ddlLocalCurrency");
        oGLDataRecurringItemScheduleInfo.LocalCurrencyCode = ddlLocalCurrency.SelectedValue;
        ExCalendar calDate = item.FindControl("calDate") as ExCalendar;
        oGLDataRecurringItemScheduleInfo.OpenDate = Convert.ToDateTime(calDate.Text);
        ExCalendar calScheduleBeginDate = item.FindControl("calScheduleBeginDate") as ExCalendar;
        oGLDataRecurringItemScheduleInfo.ScheduleBeginDate = Convert.ToDateTime(calScheduleBeginDate.Text);
        ExCalendar calScheduleEndDate = item.FindControl("calScheduleEndDate") as ExCalendar;
        oGLDataRecurringItemScheduleInfo.ScheduleEndDate = Convert.ToDateTime(calScheduleEndDate.Text);
        decimal baseCurrencyExchangeRate = 1;
        baseCurrencyExchangeRate = Convert.ToDecimal(item["BaseCurrencyExchangeRate"].Text);
        oGLDataRecurringItemScheduleInfo.ScheduleAmountBaseCurrency = Math.Round(amountLocalCurrency * baseCurrencyExchangeRate, 2);
        decimal reportingCurrencyExchangeRate = Convert.ToDecimal(item["ReportingCurrencyExchangeRate"].Text);
        oGLDataRecurringItemScheduleInfo.ScheduleAmountReportingCurrency = Math.Round(amountLocalCurrency * reportingCurrencyExchangeRate, 2);
        decimal noOfDaysInSchedule = oGLDataRecurringItemScheduleInfo.ScheduleEndDate.Value.Subtract(oGLDataRecurringItemScheduleInfo.ScheduleBeginDate.Value).Days + 1;
        decimal noOfDaysPassed = SessionHelper.CurrentReconciliationPeriodEndDate.Value.Subtract(oGLDataRecurringItemScheduleInfo.ScheduleBeginDate.Value).Days + 1;
        decimal recPeriodAmountLCCY = 0;
        if (noOfDaysInSchedule > 0 && noOfDaysPassed > 0)
        {
            recPeriodAmountLCCY = oGLDataRecurringItemScheduleInfo.ScheduleAmount.Value * (noOfDaysPassed / noOfDaysInSchedule);
        }
        oGLDataRecurringItemScheduleInfo.RecPeriodAmountLocalCurrency = recPeriodAmountLCCY;
        oGLDataRecurringItemScheduleInfo.RecPeriodAmountBaseCurrency = recPeriodAmountLCCY * baseCurrencyExchangeRate;
        oGLDataRecurringItemScheduleInfo.RecPeriodAmountReportingCurrency = recPeriodAmountLCCY * reportingCurrencyExchangeRate;
        oGLDataRecurringItemScheduleInfo.BalanceLocalCurrency = Math.Round((oGLDataRecurringItemScheduleInfo.ScheduleAmount.Value - recPeriodAmountLCCY), TestConstant.DECIMAL_PLACES_FOR_MATH_ROUND);
        oGLDataRecurringItemScheduleInfo.BalanceBaseCurrency = Math.Round(oGLDataRecurringItemScheduleInfo.BalanceLocalCurrency.Value * baseCurrencyExchangeRate, TestConstant.DECIMAL_PLACES_FOR_MATH_ROUND);
        oGLDataRecurringItemScheduleInfo.BalanceReportingCurrency = Math.Round(oGLDataRecurringItemScheduleInfo.BalanceLocalCurrency.Value * reportingCurrencyExchangeRate, TestConstant.DECIMAL_PLACES_FOR_MATH_ROUND);
        ExTextBox txtComments = (ExTextBox)item.FindControl("txtComments");
        oGLDataRecurringItemScheduleInfo.Comments = txtComments.Text;
        oGLDataRecurringItemScheduleInfo.DateAdded = DateTime.Now;
        oGLDataRecurringItemScheduleInfo.GLDataID = this._GLDataID;
        ExTextBox txtScheduleName = (ExTextBox)item.FindControl("txtScheduleName");
        oGLDataRecurringItemScheduleInfo.ScheduleName = txtScheduleName.Text;
        oGLDataRecurringItemScheduleInfo.IsActive = true;
        string SelectedCategoryTypeID = "0";
        string[] ArrSelected = ddlRecCategoryType.SelectedValue.Split('~');
        if (ArrSelected.Length > 0)
            SelectedCategoryTypeID = ArrSelected[1];
        oGLDataRecurringItemScheduleInfo.ReconciliationCategoryTypeID = Convert.ToInt16(SelectedCategoryTypeID);

        //oGLDataRecurringItemScheduleInfo.ReconciliationCategoryTypeID = Convert.ToInt16(ddlRecCategoryType.SelectedValue);
        oGLDataRecurringItemScheduleInfo.RecCategoryTypeID = Convert.ToInt16(SelectedCategoryTypeID);
        oGLDataRecurringItemScheduleInfo.CalculationFrequencyID = (short)ARTEnums.CalculationFrequency.DailyInterval;
        oGLDataRecurringItemScheduleInfo.TotalIntervals = null;
        oGLDataRecurringItemScheduleInfo.CurrentInterval = null;
        oGLDataRecurringItemScheduleInfo.RecordSourceID = RecordSourceID;
        oGLDataRecurringItemScheduleInfo.DataImportID = null;
        oGLDataRecurringItemScheduleInfo.RecordSourceTypeID = (short)ARTEnums.RecordSourceType.Matching;
        oGLDataRecurringItemScheduleInfo.IndexID = Convert.ToInt16(item["IndexID"].Text); ;
        oGLDataRecurringItemScheduleInfo.MatchSetMatchingSourceDataImportID = MatchSetMatchingSourceDataImportID;
        oGLDataRecurringItemScheduleInfo.ExcelRowNumber = ExcelRowNumber;
        return oGLDataRecurringItemScheduleInfo;
    }
    #endregion
    #region WriteOffOnItems
    private List<GLDataWriteOnOffInfo> GetGLDataWriteOnOffInfoCollection()
    {

        List<GLDataWriteOnOffInfo> oGLDataWriteOnOffInfoCollection = null;
        if (ViewState["oGLDataWriteOnOffInfoCollection"] == null)
        {
            oGLDataWriteOnOffInfoCollection = new List<GLDataWriteOnOffInfo>();
            //DataTable tblRecItem = (DataTable)Session[SessionConstants.MATCHING_MATCH_SET_RESULT_CREATE_REC_ITEM_DATA];
            DataTable tblRecItemManin = (DataTable)Session[SessionConstants.MATCHING_MATCH_SET_RESULT_CREATE_REC_ITEM_DATA];
            //DataTable tblRecItem = GetFlipedDataTable(tblRecItemManin);
            DataTable tblRecItem = tblRecItemManin;
            if (tblRecItem != null)
            {
                short RefNo = 1;
                bool IsRecItemByCurrentCombination = true;
                GLDataWriteOnOffInfo oGLDataWriteOnOffInfo = null;
                foreach (DataRow dr in tblRecItem.Rows)
                {
                    oGLDataWriteOnOffInfo = GLDataWriteOnOffInfoDetail(dr, RefNo, out IsRecItemByCurrentCombination);


                    if (IsRecItemByCurrentCombination)
                    {
                        RefNo++;
                        oGLDataWriteOnOffInfoCollection.Add(oGLDataWriteOnOffInfo);
                    }

                }
                ViewState["oGLDataWriteOnOffInfoCollection"] = oGLDataWriteOnOffInfoCollection;
            }
        }
        else
        {
            oGLDataWriteOnOffInfoCollection = (List<GLDataWriteOnOffInfo>)ViewState["oGLDataWriteOnOffInfoCollection"];
        }
        return oGLDataWriteOnOffInfoCollection;

    }
    private GLDataWriteOnOffInfo GLDataWriteOnOffInfoDetail(DataRow dr, short? RefNo, out  bool IsRecItemByCurrentCombination)
    {
        bool IsRItemByCurrentCombination = true;
        GLDataWriteOnOffInfo oGLDataWriteOnOffInfo = new GLDataWriteOnOffInfo();
        Hashtable htClolumnMapping = GetClolumnMapping();
        if (htClolumnMapping != null)
        {
            decimal amountLocalCurrency;
            if (htClolumnMapping[WebEnums.RecItemColumns.AmountLCCY] != null)
            {
                if (dr[htClolumnMapping[WebEnums.RecItemColumns.AmountLCCY].ToString()] != null)
                {
                    if (decimal.TryParse(dr[htClolumnMapping[WebEnums.RecItemColumns.AmountLCCY].ToString()].ToString(), out amountLocalCurrency))
                    {
                        oGLDataWriteOnOffInfo.Amount = amountLocalCurrency;
                    }
                }
            }
            if (htClolumnMapping[WebEnums.RecItemColumns.LCCYCode] != null)
            {
                if (dr[htClolumnMapping[WebEnums.RecItemColumns.LCCYCode].ToString()] != null)
                {
                    oGLDataWriteOnOffInfo.LocalCurrencyCode = dr[htClolumnMapping[WebEnums.RecItemColumns.LCCYCode].ToString()].ToString();
                }
            }
            //if (htClolumnMapping[WebEnums.RecItemColumns.RecItemNumber] != null)
            //{
            //    if (dr[htClolumnMapping[WebEnums.RecItemColumns.RecItemNumber].ToString()] != null)
            //    {
            //        oGLDataRecItemInfo.RecItemNumber = dr[htClolumnMapping[WebEnums.RecItemColumns.RecItemNumber].ToString()].ToString();
            //    }
            //}
            if (htClolumnMapping[WebEnums.RecItemColumns.Description] != null)
            {
                if (dr[htClolumnMapping[WebEnums.RecItemColumns.Description].ToString()] != null)
                {
                    oGLDataWriteOnOffInfo.Comments = dr[htClolumnMapping[WebEnums.RecItemColumns.Description].ToString()].ToString();

                }
            }
            DateTime OpenDate;
            if (htClolumnMapping[WebEnums.RecItemColumns.Date] != null)
            {
                if (dr[htClolumnMapping[WebEnums.RecItemColumns.Date].ToString()] != null)
                {
                    if (DateTime.TryParse(dr[htClolumnMapping[WebEnums.RecItemColumns.Date].ToString()].ToString(), out OpenDate))
                    {
                        oGLDataWriteOnOffInfo.OpenDate = OpenDate;
                    }
                }
            }
        }

        if (_oMatchSetSubSetCombinationInfo != null)
            oGLDataWriteOnOffInfo.RecordSourceID = _oMatchSetSubSetCombinationInfo.MatchSetSubSetCombinationID;
        if (dr[MatchSetResultConstants.MATCH_SET_RESULT_COLUMN_NAME_REC_ITEM_NUMBER] != null)
            oGLDataWriteOnOffInfo.RecItemNumber = Convert.ToString(dr[MatchSetResultConstants.MATCH_SET_RESULT_COLUMN_NAME_REC_ITEM_NUMBER]);
        if (dr[MatchSetResultConstants.MATCH_SET_RESULT_COLUMN_NAME_EXCEL_ROW_NUMBER] != null)
            oGLDataWriteOnOffInfo.ExcelRowNumber = Convert.ToInt64(dr[MatchSetResultConstants.MATCH_SET_RESULT_COLUMN_NAME_EXCEL_ROW_NUMBER]);
        if (dr[MatchSetResultConstants.MATCH_SET_RESULT_COLUMN_NAME_MATCH_SET_MATCHING_SOURCE_DATA_IMPORT_ID] != null)
            oGLDataWriteOnOffInfo.MatchSetMatchingSourceDataImportID = Convert.ToInt64(dr[MatchSetResultConstants.MATCH_SET_RESULT_COLUMN_NAME_MATCH_SET_MATCHING_SOURCE_DATA_IMPORT_ID]);
        if (dr[MatchSetResultConstants.MATCH_SET_RESULT_COLUMN_NAME_DATA_SOURCE_NAME] != null)
            oGLDataWriteOnOffInfo.DataSourceName = Convert.ToString(dr[MatchSetResultConstants.MATCH_SET_RESULT_COLUMN_NAME_DATA_SOURCE_NAME]);
        string CloseDate = Convert.ToString(dr[MatchSetResultConstants.MATCH_SET_RESULT_COLUMN_NAME_CLOSE_DATE]);
        if (!String.IsNullOrEmpty(CloseDate))
            oGLDataWriteOnOffInfo.CloseDate = Convert.ToDateTime(CloseDate);
        else
            oGLDataWriteOnOffInfo.CloseDate = null;

        if (!string.IsNullOrEmpty(oGLDataWriteOnOffInfo.RecItemNumber))
            UpdateDbInfo(oGLDataWriteOnOffInfo, out IsRItemByCurrentCombination);

        IsRecItemByCurrentCombination = IsRItemByCurrentCombination;

        oGLDataWriteOnOffInfo.IndexID = RefNo;
        return oGLDataWriteOnOffInfo;

    }
    protected void CreateWriteOnOffRecItems()
    {
        try
        {

            List<GLDataWriteOnOffInfo> oUpdatedGLDataWriteOnOffInfoCollection;
            List<GLDataWriteOnOffInfo> oGLDataWriteOnOffInfoCollection = new List<GLDataWriteOnOffInfo>();
            GLDataWriteOnOffInfo oGLDataWriteOnOffInfo = null;

            foreach (GridDataItem item in rgCreateWriteOffOnRecItem.SelectedItems)
            {
                oGLDataWriteOnOffInfo = GetGLDataWriteOnOffDetail(item);
                oGLDataWriteOnOffInfoCollection.Add(oGLDataWriteOnOffInfo);

            }
            IDataImport oDataImportClient = RemotingHelper.GetDataImportObject();
            int rowsAffected;
            MasterPageBase oMasterPageBase = (MasterPageBase)this.Master.Master;
            if (oGLDataWriteOnOffInfoCollection.Count > 0)
            {
                oDataImportClient.InsertMatchingGLDataWriteOnOffRecItem(oGLDataWriteOnOffInfoCollection, out rowsAffected, Helper.GetAppUserInfo());
                oMasterPageBase.ShowConfirmationMessage(2339);
                oUpdatedGLDataWriteOnOffInfoCollection = GetGLDataWriteOnOffInfoCollection();
                DataTable tblRecItem = (DataTable)Session[SessionConstants.MATCHING_MATCH_SET_RESULT_CREATE_REC_ITEM_DATA];

                for (int i = 0; i < oGLDataWriteOnOffInfoCollection.Count; i++)
                {
                    long excelRowNumber;
                    long matchSetMatchingSourceDataImportID;
                    string filterString = MatchSetResultConstants.MATCH_SET_RESULT_COLUMN_NAME_EXCEL_ROW_NUMBER + "={0} AND " +
                        MatchSetResultConstants.MATCH_SET_RESULT_COLUMN_NAME_MATCH_SET_MATCHING_SOURCE_DATA_IMPORT_ID + "={1}";
                    DataRow[] dr = null;
                    if (long.TryParse(oGLDataWriteOnOffInfoCollection[i].ExcelRowNumber.ToString(), out excelRowNumber) &&
                        long.TryParse(oGLDataWriteOnOffInfoCollection[i].MatchSetMatchingSourceDataImportID.ToString(), out matchSetMatchingSourceDataImportID))
                    {
                        dr = tblRecItem.Select(string.Format(filterString, excelRowNumber, matchSetMatchingSourceDataImportID));

                        //DataRow dr = tblRecItem.AsEnumerable().Where(r => (Int64)r[MatchSetResultConstants.MATCH_SET_RESULT_COLUMN_NAME_MATCH_SET_MATCHING_SOURCE_DATA_IMPORT_ID] == oGLDataRecurringItemScheduleInfoCollection[i].MatchSetMatchingSourceDataImportID && (Int64)r[MatchSetResultConstants.MATCH_SET_RESULT_COLUMN_NAME_EXCEL_ROW_NUMBER] == oGLDataRecurringItemScheduleInfoCollection[i].ExcelRowNumber).First();
                        if (dr.Length > 0)
                            dr[0][MatchSetResultConstants.MATCH_SET_RESULT_COLUMN_NAME_REC_ITEM_NUMBER] = oGLDataWriteOnOffInfoCollection[i].RecItemNumber;
                    }

                    //DataRow dr = tblRecItem.AsEnumerable().Where(r => (Int64)r[MatchSetResultConstants.MATCH_SET_RESULT_COLUMN_NAME_MATCH_SET_MATCHING_SOURCE_DATA_IMPORT_ID] == oGLDataWriteOnOffInfoCollection[i].MatchSetMatchingSourceDataImportID && (Int64)r[MatchSetResultConstants.MATCH_SET_RESULT_COLUMN_NAME_EXCEL_ROW_NUMBER] == oGLDataWriteOnOffInfoCollection[i].ExcelRowNumber).First();
                    //dr[MatchSetResultConstants.MATCH_SET_RESULT_COLUMN_NAME_REC_ITEM_NUMBER] = oGLDataWriteOnOffInfoCollection[i].RecItemNumber;

                    GLDataWriteOnOffInfo oGLDataWriteOnOffInfoTemp;
                    oGLDataWriteOnOffInfoTemp = oUpdatedGLDataWriteOnOffInfoCollection.Find(O => O.IndexID == oGLDataWriteOnOffInfoCollection[i].IndexID);
                    oGLDataWriteOnOffInfoTemp.RecItemNumber = oGLDataWriteOnOffInfoCollection[i].RecItemNumber;
                    oGLDataWriteOnOffInfoTemp.OpenDate = oGLDataWriteOnOffInfoCollection[i].OpenDate;
                    oGLDataWriteOnOffInfoTemp.Amount = oGLDataWriteOnOffInfoCollection[i].Amount;
                    oGLDataWriteOnOffInfoTemp.LocalCurrencyCode = oGLDataWriteOnOffInfoCollection[i].LocalCurrencyCode;
                    oGLDataWriteOnOffInfoTemp.Comments = oGLDataWriteOnOffInfoCollection[i].Comments;
                    oGLDataWriteOnOffInfoTemp.WriteOnOffID = oGLDataWriteOnOffInfoCollection[i].WriteOnOffID;
                }
                //Session[SessionConstants.MATCHING_MATCH_SET_RESULT_CREATE_REC_ITEM_DATA] = tblRecItem;
                ViewState["oGLDataWriteOnOffInfoCollection"] = oUpdatedGLDataWriteOnOffInfoCollection;
                BindGLDataWriteOnOffItems(oUpdatedGLDataWriteOnOffInfoCollection, false);

            }
            else
            {
                oMasterPageBase.ShowConfirmationMessage(2013);
                //oMasterPageBase.HideMessage();
            }

        }
        catch (Exception ex)
        {
            Helper.ShowErrorMessage(this, ex);
        }
    }
    private void BindGLDataWriteOnOffItems(List<GLDataWriteOnOffInfo> OItemlist, bool IsfromNeedDatasource)
    {
        rgCreateWriteOffOnRecItem.MasterTableView.DataSource = OItemlist;
        rgCreateWriteOffOnRecItem.ClientSettings.ClientEvents.OnRowSelecting = "Selecting";
        if (!IsfromNeedDatasource)
            rgCreateWriteOffOnRecItem.DataBind();
    }
    protected void rgCreateWriteOffOnRecItem_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    {
        try
        {
            // Check in Dll Type value
            if (hdnIsBindWriteOffOnGrid.Value == "1")
                BindGLDataWriteOnOffItems(GetGLDataWriteOnOffInfoCollection(), true);
        }
        catch (Exception ex)
        {
            Helper.ShowErrorMessage(this, ex);
        }

    }
    protected void rgCreateWriteOffOnRecItem_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {
        if (e.Item.ItemType == Telerik.Web.UI.GridItemType.Item ||
           e.Item.ItemType == Telerik.Web.UI.GridItemType.AlternatingItem)
        {
            GLDataWriteOnOffInfo oGLDataWriteOnOffInfo = (GLDataWriteOnOffInfo)e.Item.DataItem;
            ExCalendar calDate = e.Item.FindControl("calDate") as ExCalendar;
            ExTextBox txtComments = (ExTextBox)e.Item.FindControl("txtComments");
            DropDownList ddlLocalCurrency = (DropDownList)e.Item.FindControl("ddlLocalCurrency");
            TextBox txtAmountLCCY = (TextBox)e.Item.FindControl("txtAmountLCCY");
            CheckBox checkBox = (CheckBox)(e.Item as GridDataItem)["CheckboxSelectColumn"].Controls[0];
            ExLabel lblError = (ExLabel)e.Item.FindControl("lblError");
            ExLabel lblErrorSign = (ExLabel)e.Item.FindControl("lblErrorSign");
            ExLabel lblRecItemNo = (ExLabel)e.Item.FindControl("lblRecItemNo");
            ExLabel lblDataSourceName = (ExLabel)e.Item.FindControl("lblDataSourceName");
            (e.Item as GridDataItem)["RecordSourceID"].Text = oGLDataWriteOnOffInfo.RecordSourceID.ToString();
            (e.Item as GridDataItem)["MatchSetMatchingSourceDataImportID"].Text = oGLDataWriteOnOffInfo.MatchSetMatchingSourceDataImportID.ToString();
            (e.Item as GridDataItem)["ExcelRowNumber"].Text = oGLDataWriteOnOffInfo.ExcelRowNumber.ToString();
            (e.Item as GridDataItem)["IndexID"].Text = oGLDataWriteOnOffInfo.IndexID.ToString();

            ExRadioButton optWriteOn = (ExRadioButton)e.Item.FindControl("optWriteOn");
            ExRadioButton optWriteOff = (ExRadioButton)e.Item.FindControl("optWriteOff");


            //StringBuilder oSBError = new StringBuilder();
            string errorFormat = Helper.GetLabelIDValue(1774);
            calDate.Text = Helper.GetDisplayDateForCalendar(oGLDataWriteOnOffInfo.OpenDate);
            string description = oGLDataWriteOnOffInfo.Comments;
            if (!string.IsNullOrEmpty(description))
            {
                txtComments.Text = description;
            }
            FillDdlLccy(ddlLocalCurrency);
            string localCurrencyCode = oGLDataWriteOnOffInfo.LocalCurrencyCode;
            ddlLocalCurrency.SelectedValue = localCurrencyCode;
            decimal amountLocalCurrency = oGLDataWriteOnOffInfo.Amount.Value;
            if (amountLocalCurrency != 0)
            {
                txtAmountLCCY.Text = Helper.GetDisplayDecimalValue(amountLocalCurrency);
            }
            lblRecItemNo.Text = Helper.GetDisplayStringValue(oGLDataWriteOnOffInfo.RecItemNumber);
            lblDataSourceName.Text = Helper.GetDisplayStringValue(oGLDataWriteOnOffInfo.DataSourceName);



            if (oGLDataWriteOnOffInfo.WriteOnOffID == (short)WebEnums.WriteOnOff.WriteOn)
            {
                optWriteOn.Checked = true;
            }
            else if (oGLDataWriteOnOffInfo.WriteOnOffID == (short)WebEnums.WriteOnOff.WriteOff)
            {
                optWriteOff.Checked = true;
            }
            bool isEditMode = this.FormMode == WebEnums.FormMode.Edit;
            if (isEditMode)
            {
                bool IsCloseDateNullOrEmpty = true;
                if (oGLDataWriteOnOffInfo.CloseDate.HasValue)
                    IsCloseDateNullOrEmpty = String.IsNullOrEmpty(oGLDataWriteOnOffInfo.CloseDate.ToString());
                bool isEnabled = (String.IsNullOrEmpty(oGLDataWriteOnOffInfo.RecItemNumber) && IsCloseDateNullOrEmpty);
                if (!isEnabled)
                {
                    checkBox.Enabled = false;
                    Sel.Value += e.Item.ItemIndex.ToString() + ":";
                    ddlLocalCurrency.Enabled = false;
                    txtAmountLCCY.Enabled = false;
                    calDate.Enabled = false;
                    txtComments.Enabled = false;
                    optWriteOn.Enabled = false;
                    optWriteOff.Enabled = false;

                }
                else
                {

                    ddlLocalCurrency.Enabled = true;
                    txtAmountLCCY.Enabled = true;
                    checkBox.Enabled = true;
                    calDate.Enabled = true;
                    txtComments.Enabled = true;
                    optWriteOn.Enabled = true;
                    optWriteOff.Enabled = true;


                }
            }
            else
            {
                checkBox.Enabled = false;
                Sel.Value += e.Item.ItemIndex.ToString() + ":";
                ddlLocalCurrency.Enabled = false;
                txtAmountLCCY.Enabled = false;
                calDate.Enabled = false;
                txtComments.Enabled = false;
                optWriteOn.Enabled = false;
                optWriteOff.Enabled = false;

            }

        }

    }
    protected void cvValidateWriteOffOnRecItems_ServerValidate(object source, ServerValidateEventArgs args)
    {
        try
        {
            ExCustomValidator cv = (ExCustomValidator)source;
            bool isErrorExist = false;
            foreach (GridDataItem item in rgCreateWriteOffOnRecItem.SelectedItems)
            {
                bool isError = false;
                StringBuilder oSBError = new StringBuilder();
                string errorFormat = Helper.GetLabelIDValue(1774);
                ExCalendar calDate = item.FindControl("calDate") as ExCalendar;
                DateTime openDate;
                if (!string.IsNullOrEmpty(calDate.Text))
                {
                    if (DateTime.TryParse(calDate.Text, out openDate))
                    {
                        if (openDate > Convert.ToDateTime(DateTime.Now))
                        {
                            isError = true;
                            oSBError.Append(Helper.GetErrorMessage(WebEnums.FieldType.DateCompareField, 1511, 2062));
                            oSBError.Append("<br/>");
                        }
                    }
                    else
                    {
                        isError = true;
                        oSBError.Append(Helper.GetErrorMessage(WebEnums.FieldType.InvalidDate, 1511));
                        oSBError.Append("<br/>");

                    }
                }
                else
                {
                    isError = true;
                    oSBError.Append(Helper.GetErrorMessage(WebEnums.FieldType.MandatoryField, 1511));
                    oSBError.Append("<br/>");

                }

                DropDownList ddlLocalCurrency = (DropDownList)item.FindControl("ddlLocalCurrency");
                string localCurrencyCode = ddlLocalCurrency.SelectedValue;
                if (!(string.IsNullOrEmpty(localCurrencyCode) || localCurrencyCode == WebConstants.SELECT_ONE))
                {
                    IUtility oUtilityClient = RemotingHelper.GetUtilityObject();
                    try
                    {
                        decimal baseCurrencyExRate = 1;
                        if (!string.IsNullOrEmpty(this.CurrentBCCY))
                            baseCurrencyExRate = oUtilityClient.GetCurrencyExchangeRate(SessionHelper.CurrentReconciliationPeriodID.Value
                                , localCurrencyCode, this.CurrentBCCY, this._IsMultiCurrencyEnabled, Helper.GetAppUserInfo());
                        (item as GridDataItem)["BaseCurrencyExchangeRate"].Text = baseCurrencyExRate.ToString();

                    }
                    catch (Exception)
                    {

                        isError = true;
                        oSBError.Append(Helper.GetLabelIDValue(5000098));
                        oSBError.Append("<br/>");

                    }

                    try
                    {
                        decimal baseCurrencyExRate = oUtilityClient.GetCurrencyExchangeRate(SessionHelper.CurrentReconciliationPeriodID.Value
                            , localCurrencyCode, SessionHelper.ReportingCurrencyCode, this._IsMultiCurrencyEnabled, Helper.GetAppUserInfo());
                        (item as GridDataItem)["ReportingCurrencyExchangeRate"].Text = baseCurrencyExRate.ToString();

                    }
                    catch (Exception)
                    {
                        isError = true;
                        oSBError.Append(Helper.GetLabelIDValue(5000099));
                        oSBError.Append("<br/>");

                    }
                }
                else
                {
                    isError = true;
                    oSBError.Append(Helper.GetErrorMessage(WebEnums.FieldType.MandatoryField, 1409));
                    oSBError.Append("<br/>");

                }

                TextBox txtAmountLCCY = (TextBox)item.FindControl("txtAmountLCCY");
                decimal amountLocalCurrency = 0;
                if (!string.IsNullOrEmpty(txtAmountLCCY.Text))
                {
                    if (decimal.TryParse(txtAmountLCCY.Text, out amountLocalCurrency))
                    {
                        amountLocalCurrency = Convert.ToDecimal(txtAmountLCCY.Text);
                    }
                    else
                    {
                        isError = true;
                        oSBError.Append(Helper.GetErrorMessage(WebEnums.FieldType.InvalidNumericField, 1510));
                        oSBError.Append("<br/>");
                    }
                }
                else
                {
                    isError = true;
                    oSBError.Append(Helper.GetErrorMessage(WebEnums.FieldType.MandatoryField, 1510));
                    oSBError.Append("<br/>");
                }

                ExRadioButton optWriteOn = (ExRadioButton)item.FindControl("optWriteOn");
                ExRadioButton optWriteOff = (ExRadioButton)item.FindControl("optWriteOff");

                if (!(optWriteOn.Checked || optWriteOff.Checked))
                {
                    isError = true;
                    oSBError.Append(Helper.GetErrorMessage(WebEnums.FieldType.MandatoryField, 1425));
                    oSBError.Append("<br/>");
                }
                ExLabel lblError = (ExLabel)item.FindControl("lblError");
                ExLabel lblErrorSign = (ExLabel)item.FindControl("lblErrorSign");
                lblError.Text = oSBError.ToString();
                if (isError)
                {
                    lblError.Visible = true;
                    lblErrorSign.Visible = true;
                    isErrorExist = true;
                }
                else
                {
                    lblError.Visible = false;
                    lblErrorSign.Visible = false;
                }
            }
            if (isErrorExist)
            {
                args.IsValid = false;
            }

        }
        catch (ARTException ex)
        {
            Helper.ShowErrorMessage((PageBase)this.Page, ex);
        }
        catch (Exception ex)
        {
            Helper.ShowErrorMessage((PageBase)this.Page, ex);
        }
    }
    private GLDataWriteOnOffInfo GetGLDataWriteOnOffDetail(GridDataItem item)
    {
        long RecordSourceID = Convert.ToInt64(item["RecordSourceID"].Text);
        long MatchSetMatchingSourceDataImportID = Convert.ToInt64(item["MatchSetMatchingSourceDataImportID"].Text);
        long ExcelRowNumber = Convert.ToInt64(item["ExcelRowNumber"].Text);
        GLDataWriteOnOffInfo oGLDataWriteOnOffInfo = new GLDataWriteOnOffInfo();
        UserHdrInfo oUserHdrInfo = SessionHelper.GetCurrentUser();
        oGLDataWriteOnOffInfo.AddedBy = oUserHdrInfo.LoginID;
        oGLDataWriteOnOffInfo.AddedByUserID = oUserHdrInfo.UserID;
        TextBox txtAmountLCCY = (TextBox)item.FindControl("txtAmountLCCY");
        decimal amountLocalCurrency = Convert.ToDecimal(txtAmountLCCY.Text);
        oGLDataWriteOnOffInfo.Amount = amountLocalCurrency;
        decimal baseCurrencyExchangeRate = Convert.ToDecimal(item["BaseCurrencyExchangeRate"].Text);
        oGLDataWriteOnOffInfo.AmountBaseCurrency = Math.Round(amountLocalCurrency * baseCurrencyExchangeRate, 2);
        decimal reportingCurrencyExchangeRate = Convert.ToDecimal(item["ReportingCurrencyExchangeRate"].Text);
        oGLDataWriteOnOffInfo.AmountReportingCurrency = Math.Round(amountLocalCurrency * reportingCurrencyExchangeRate, 2);
        ExTextBox txtComments = (ExTextBox)item.FindControl("txtComments");
        oGLDataWriteOnOffInfo.Comments = txtComments.Text;
        oGLDataWriteOnOffInfo.DateAdded = DateTime.Now;
        oGLDataWriteOnOffInfo.GLDataID = this._GLDataID;
        DropDownList ddlLocalCurrency = (DropDownList)item.FindControl("ddlLocalCurrency");
        oGLDataWriteOnOffInfo.LocalCurrencyCode = ddlLocalCurrency.SelectedValue;
        ExCalendar calDate = item.FindControl("calDate") as ExCalendar;
        oGLDataWriteOnOffInfo.OpenDate = Convert.ToDateTime(calDate.Text);
        ExRadioButton optWriteOn = (ExRadioButton)item.FindControl("optWriteOn");
        ExRadioButton optWriteOff = (ExRadioButton)item.FindControl("optWriteOff");

        if (optWriteOn.Checked)
        {
            oGLDataWriteOnOffInfo.WriteOnOffID = (short?)WebEnums.WriteOnOff.WriteOn;
        }
        else if (optWriteOff.Checked)
        {
            oGLDataWriteOnOffInfo.WriteOnOffID = (short?)WebEnums.WriteOnOff.WriteOff;
        }

        string SelectedCategoryTypeID = "0";
        string[] ArrSelected = ddlRecCategoryType.SelectedValue.Split('~');
        if (ArrSelected.Length > 0)
            SelectedCategoryTypeID = ArrSelected[1];
        oGLDataWriteOnOffInfo.ReconciliationCategoryTypeID = Convert.ToInt16(SelectedCategoryTypeID);
        //oGLDataWriteOnOffInfo.ReconciliationCategoryTypeID = Convert.ToInt16(ddlRecCategoryType.SelectedValue);
        oGLDataWriteOnOffInfo.ReconciliationCategoryID = Convert.ToInt16(ddlRecCategory.SelectedValue);
        oGLDataWriteOnOffInfo.RecordSourceID = RecordSourceID;
        //oGLDataWriteOnOffInfo.DataImportID = null;
        oGLDataWriteOnOffInfo.RecordSourceTypeID = (short)ARTEnums.RecordSourceType.Matching;
        oGLDataWriteOnOffInfo.IndexID = Convert.ToInt16(item["IndexID"].Text); ;
        oGLDataWriteOnOffInfo.MatchSetMatchingSourceDataImportID = MatchSetMatchingSourceDataImportID;
        oGLDataWriteOnOffInfo.ExcelRowNumber = ExcelRowNumber;
        return oGLDataWriteOnOffInfo;
    }
    #endregion
    protected void btnCreateRecItem_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {

            string ArrSelectedControlID = "0";
            string[] ArrSelected = ddlRecCategoryType.SelectedValue.Split('~');
            if (ArrSelected.Length > 0)
                ArrSelectedControlID = ArrSelected[0];
            //  18 is the Recurring Accrual Schedule Category TypeID  AND 3 is the Recurring Amortizable Schedule Category TypeID
            if (ArrSelectedControlID == ((short)ARTEnums.RecItemControl.ItemInputAmortizableTemplate).ToString() || ArrSelectedControlID == ((short)ARTEnums.RecItemControl.ItemInputAccurableRecurring).ToString())
            {
                CreateScheduleRecItems();
            }
            else if (ArrSelectedControlID == ((short)ARTEnums.RecItemControl.ItemInputWriteOff).ToString())
            {
                CreateWriteOnOffRecItems();
            }
            else
            {
                CreateNormalRecItems();
            }

            if (Session[SessionConstants.PARENT_PAGE_URL] != null)
            {
                String Url = Session[SessionConstants.PARENT_PAGE_URL].ToString();
                //Response.Redirect(Url, true);
                SessionHelper.RedirectToUrl(Url);
                return;
            }
        }
    }
    protected void btnBackToWorkspace_Click(object sender, EventArgs e)
    {
        String Url = Request.Url.AbsoluteUri;
        string NewUrl;
        NewUrl = Url.Replace("CreateRecItem", "MatchingResults");
        //Response.Redirect(NewUrl);
        SessionHelper.RedirectToUrl(NewUrl);
        return;
    }
    public void EnableDisableControls(WebEnums.FormMode eFormMode)
    {
        bool isEditMode = eFormMode == WebEnums.FormMode.Edit;
        btnCreateRecItem.Enabled = isEditMode;
        btnSave.Enabled = isEditMode;
        btnFlipSign.Enabled = isEditMode;

    }
    protected void rgCreateWriteOffOnRecItem_PageIndexChanged(object source, GridPageChangedEventArgs e)
    {

        Sel.Value = "";

    }
    protected void rgCreateScheduleRecItem_PageIndexChanged(object source, GridPageChangedEventArgs e)
    {

        Sel.Value = "";

    }
    protected void rgCreateRecItem_PageIndexChanged(object source, GridPageChangedEventArgs e)
    {

        Sel.Value = "";

    }
    protected void btnFlipSign_Click(object sender, EventArgs e)
    {
        hdnFlipedValue.Value = "-1";
        Decimal FlipedValue = Convert.ToDecimal(hdnFlipedValue.Value);
        if (hdnIsBindNormalItemGrid.Value == "1")
        {
            foreach (GridDataItem item in rgCreateRecItem.SelectedItems)
            {
                TextBox txtAmountLCCY = (TextBox)item.FindControl("txtAmountLCCY");
                decimal amountLocalCurrency;
                if (decimal.TryParse(txtAmountLCCY.Text, out amountLocalCurrency))
                {
                    txtAmountLCCY.Text = Helper.GetDisplayDecimalValue(amountLocalCurrency * FlipedValue);
                }
            }
            //ViewState["oGLDataRecItemInfoCollection"] = null;
            //BindNormalItems(GetGLDataRecItemInfoCollection(), false);
        }
        if (hdnIsBindWriteOffOnGrid.Value == "1")
        {
            foreach (GridDataItem item in rgCreateWriteOffOnRecItem.SelectedItems)
            {
                TextBox txtAmountLCCY = (TextBox)item.FindControl("txtAmountLCCY");
                decimal amountLocalCurrency;
                if (decimal.TryParse(txtAmountLCCY.Text, out amountLocalCurrency))
                {
                    txtAmountLCCY.Text = Helper.GetDisplayDecimalValue(amountLocalCurrency * FlipedValue);
                }
            }
            //ViewState["oGLDataWriteOnOffInfoCollection"] = null;
            //BindGLDataWriteOnOffItems(GetGLDataWriteOnOffInfoCollection(), false);
        }
        if (hdnIsBindScheduleItemGrid.Value == "1")
        {
            foreach (GridDataItem item in rgCreateScheduleRecItem.SelectedItems)
            {
                TextBox txtAmountLCCY = (TextBox)item.FindControl("txtAmountLCCY");
                decimal amountLocalCurrency;
                if (decimal.TryParse(txtAmountLCCY.Text, out amountLocalCurrency))
                {
                    txtAmountLCCY.Text = Helper.GetDisplayDecimalValue(amountLocalCurrency * FlipedValue);
                }
            }
            //ViewState["oGLDataRecurringItemScheduleInfoCollection"] = null;
            //BindScheduleItems(GetGLDataScheduleRecItemInfoCollection(), false);
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {

        if (Page.IsValid)
        {

            string ArrSelectedControlID = "0";
            string[] ArrSelected = ddlRecCategoryType.SelectedValue.Split('~');
            if (ArrSelected.Length > 0)
                ArrSelectedControlID = ArrSelected[0];
            //  18 is the Recurring Accrual Schedule Category TypeID  AND 3 is the Recurring Amortizable Schedule Category TypeID
            if (ArrSelectedControlID == ((short)ARTEnums.RecItemControl.ItemInputAmortizableTemplate).ToString() || ArrSelectedControlID == ((short)ARTEnums.RecItemControl.ItemInputAccurableRecurring).ToString())
            {
                CreateScheduleRecItems();
            }
            else if (ArrSelectedControlID == ((short)ARTEnums.RecItemControl.ItemInputWriteOff).ToString())
            {
                CreateWriteOnOffRecItems();
            }
            else
            {
                CreateNormalRecItems();
            }

        }
    }
}
