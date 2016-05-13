using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SkyStem.Library.Controls.TelerikWebControls;
using Telerik.Web.UI;
using SkyStem.ART.Web.Utility;
using SkyStem.Library.Controls.WebControls;
using SkyStem.ART.Client.Model;
using SkyStem.Language.LanguageUtility;
using SkyStem.ART.Client.Data;

public partial class UserControls_NetAccountComposition : System.Web.UI.UserControl
{

    private int? _NetAccountID=0;
    private int _ReconciliationPeriodID;
    private int _CompanyID;
    bool _IsMultiCurrencyEnabled;
    const int GRID_COLUMN_INDEX_KEY_START = 3;

    #region Public Properties
    public int? NetAccountID
    {
        get
        {
            return _NetAccountID; 
        }
        set
        {
            _NetAccountID = value;
        }
    
    }
    public int ReconciliationPeriodID
    {
      get
     {
        return _ReconciliationPeriodID;  

     }
     set
     {
         _ReconciliationPeriodID = value;
     }
       
    }

    public int CompanyID
    {
        get
        {
            return _CompanyID; 
        }
        set
        {
            _CompanyID = value;
        }

    }


    #endregion

    protected void Page_Init(object sender,EventArgs e)
    {
        ExRadGrid grdData = (ExRadGrid)ucSkyStemARTGrid.Grid;
        ExGridTemplateColumn oExGridTemplateColumnGLBalanceReportingCurrency = (ExGridTemplateColumn)grdData.Columns.FindByUniqueNameSafe("GLBalanceReportingCurrency");
        oExGridTemplateColumnGLBalanceReportingCurrency.HeaderText = LanguageUtil.GetValue(1875) + " (" + SessionHelper.ReportingCurrencyCode + ")";
        ExGridTemplateColumn oExGridTemplateColumnGLBalanceBaseCurrency = (ExGridTemplateColumn)grdData.Columns.FindByUniqueNameSafe("GLBalanceBaseCurrency");
        oExGridTemplateColumnGLBalanceBaseCurrency.HeaderText = LanguageUtil.GetValue(1876); 
    }
  

    protected void Page_Load(object sender, EventArgs e)
    {
        ucAccountHierarchyDetail.NetAccountID = this._NetAccountID;
        ExRadGrid grdData = (ExRadGrid)ucSkyStemARTGrid.Grid;
        //ShowHideColumnsBasedOnOrganizationalHierarchy(GRID_COLUMN_INDEX_KEY_START, grdData.MasterTableView, this._CompanyID);
        GridHelper.ShowHideColumns(GRID_COLUMN_INDEX_KEY_START, grdData.MasterTableView, this._CompanyID, ARTEnums.Grid.AccountViewer, ucSkyStemARTGrid.Grid.AllowCustomization);
        //CheckMultiCurrency(grdData);
        BindGrid();

    }

    private void ShowHideColumnsBasedOnOrganizationalHierarchy(int colIndexStart, GridTableView gtv, int? CompanyID)
    {
        List<GeographyStructureHdrInfo> oGeographyStructureHdrInfoCollection = SessionHelper.GetOrganizationalHierarchy(CompanyID);
        int columnIndex = colIndexStart;
        for (int i = 1; i < oGeographyStructureHdrInfoCollection.Count; i++)
        {
            gtv.Columns[columnIndex].Visible = true;
            gtv.Columns[columnIndex].HeaderText = oGeographyStructureHdrInfoCollection[i].GeographyStructure;
            columnIndex++;
        }
    }

    private void BindGrid()
    {
        ExRadGrid grdData = (ExRadGrid)ucSkyStemARTGrid.Grid;
        List<GLDataHdrInfo> oGLDataHdrInfoCollection = AccountViewerHelper.GetAccountInfoForNetAccount(NetAccountID, ReconciliationPeriodID, CompanyID); ;
        grdData.DataSource = oGLDataHdrInfoCollection;
        grdData.DataBind();
    }

    protected void ucSkyStemARTGrid_GridItemDataBound(Object Sender, GridItemEventArgs e)
    {

        if (e.Item.ItemType == GridItemType.Item || e.Item.ItemType == GridItemType.AlternatingItem)
        {
            GLDataHdrInfo oGLDataHdrInfo = (GLDataHdrInfo)e.Item.DataItem;
            ExLabel lblAccountNumber = (ExLabel)e.Item.FindControl("lblAccountNumber");
            ExLabel lblAccountName = (ExLabel)e.Item.FindControl("lblAccountName");
            ExLabel lblGLBalanceBaseCurrency = (ExLabel)e.Item.FindControl("lblGLBalanceBaseCurrency");
            ExLabel lblGLBalanceReportingCurrency = (ExLabel)e.Item.FindControl("lblGLBalanceReportingCurrency");
           
            lblAccountNumber.Text=oGLDataHdrInfo.AccountNumber;    
            lblAccountName.Text=oGLDataHdrInfo.AccountName;
            lblGLBalanceBaseCurrency.Text = Helper.GetDisplayCurrencyValue(oGLDataHdrInfo.BaseCurrencyCode, oGLDataHdrInfo.GLBalanceBaseCurrency); 
            lblGLBalanceReportingCurrency.Text =  Helper.GetDisplayDecimalValue(oGLDataHdrInfo.GLBalanceReportingCurrency);
        }

    }

    private void CheckMultiCurrency(ExRadGrid grdData)
    {
        _IsMultiCurrencyEnabled = Helper.IsCapabilityActivatedForRecPeriodID(ARTEnums.Capability.MultiCurrency, ReconciliationPeriodID, true);
        if (!_IsMultiCurrencyEnabled)
        {
            grdData.Columns.FindByUniqueNameSafe("GLBalanceBaseCurrency").Visible = false;

        }
    }
}

