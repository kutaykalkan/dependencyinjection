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
using SkyStem.ART.Client.IServices;
using SkyStem.ART.Web.Utility;
using SkyStem.ART.Client.Model;
using System.Collections.Generic;
using Telerik.Web.UI;
using SkyStem.ART.Client.Exception;
using SkyStem.Library.Controls.WebControls;
using SkyStem.ART.Web.Data;
using SkyStem.ART.Web.Classes;
using SkyStem.Library.Controls.TelerikWebControls.Data;
using SkyStem.ART.Client.Model.Base;
using SkyStem.ART.Client.Data;
using System.Threading.Tasks;

public partial class UserControls_Dashboard_ExceptionsByFSCaption : UserControlWebPartBase
{
    bool isExportPDF;
    bool isExportExcel;
    DashboardExceptionInfo _DashboardExceptionInfo = null;

    protected void Page_Init(object sender, EventArgs e)
    {
        Page oPage = (Page)this.Parent.Page;
        MasterPageBase ompage = (MasterPageBase)oPage.Master;
        ompage.ReconciliationPeriodChangedEventHandler += new EventHandler(ompage_ReconciliationPeriodChangedEventHandler);
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            this.LoadData = true;
            isExportPDF = false;
            isExportExcel = false;
        
            // Set default Sorting
            GridHelper.SetSortExpression(rgExceptionByFSCaption.MasterTableView, "Name");
            GridHelper.SetSortExpression(rgExceptionByNetAccount.MasterTableView, "Name");
        }

    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (this.LoadData.Value && this.Visible)
        {
            OnPageLoad();
        }
    }

    public override IAsyncResult GetDataAsync()
    {
        return DashboardHelper.GetDataForExceptionsByFSCaptionAsync(SessionHelper.CurrentUserID, SessionHelper.CurrentRoleID, SessionHelper.CurrentReconciliationPeriodID, Helper.GetAppUserInfo());
    }

    public override void DataLoaded(IAsyncResult result)
    {
        Task<DashboardExceptionInfo> oTask = (Task<DashboardExceptionInfo>)result;
        if (oTask.IsCompleted)
        {
            _DashboardExceptionInfo = oTask.Result;
            rgExceptionByFSCaption.Rebind();
            rgExceptionByNetAccount.Rebind();
        }
    }

    #region "Grid Events"
    protected void rgExceptionByFSCaption_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    {
        LoadExceptionByFSCaptionGridData();
    }

    private void LoadExceptionByFSCaptionGridData()
    {
        List<ExceptionByFSCaptionNetAccountInfo> oFSCaptionExceptionInfoCollection = null;
        try
        {
            //GetExceptions();
            if (_DashboardExceptionInfo != null)
            {
                oFSCaptionExceptionInfoCollection = _DashboardExceptionInfo.FSCaptionExceptionInfoCollection;
                LanguageHelper.TranslateLabelFSCaptionExceptionInfo(oFSCaptionExceptionInfoCollection);

                rgExceptionByFSCaption.DataSource = oFSCaptionExceptionInfoCollection;
                // Sort the Data
                //GridHelper.SortDataSource(rgExceptionByFSCaption.MasterTableView);
            }
        }
        catch (ARTException ex)
        {
            WebPartHelper.ShowErrorMessage(tblMessage, tblContent, lblMessage, ex);
        }
        catch (Exception ex)
        {
            WebPartHelper.ShowErrorMessage(tblMessage, tblContent, lblMessage, ex);
        }
    }

    private void GetExceptions()
    {
        _DashboardExceptionInfo = (DashboardExceptionInfo)Page.Items["Exceptions"];
        if (_DashboardExceptionInfo == null)
        {
            _DashboardExceptionInfo = DashboardHelper.GetDataForExceptionsByFSCaption(SessionHelper.CurrentUserID, SessionHelper.CurrentRoleID, SessionHelper.CurrentReconciliationPeriodID, Helper.GetAppUserInfo());
        }
    }

    protected void rgExceptionByFSCaption_ItemDataBound(object sender, GridItemEventArgs e)
    {
        //GridItemDataBound(e);
        if (e.Item.ItemType == GridItemType.Item
           || e.Item.ItemType == GridItemType.AlternatingItem)
        {
            ExceptionByFSCaptionNetAccountInfo oExceptionByFSCaptionNetAccountInfo = (ExceptionByFSCaptionNetAccountInfo)e.Item.DataItem;

            ExLinkButton lnkBtnName = (ExLinkButton)e.Item.FindControl("lnkBtnName");
            ExLinkButton lnkBtnWriteOffOn = (ExLinkButton)e.Item.FindControl("lnkBtnWriteOffOn");
            ExLinkButton lnkBtnUnExpVar = (ExLinkButton)e.Item.FindControl("lnkBtnUnExpVar");
            ExLinkButton lnkBtnTotal = (ExLinkButton)e.Item.FindControl("lnkBtnTotal");

            lnkBtnName.Text = Helper.GetDisplayStringValue(oExceptionByFSCaptionNetAccountInfo.Name);
            lnkBtnWriteOffOn.Text = Helper.GetDisplayDecimalValue(oExceptionByFSCaptionNetAccountInfo.WriteOnOffAmountReportingCurrency);
            lnkBtnUnExpVar.Text = Helper.GetDisplayDecimalValue(oExceptionByFSCaptionNetAccountInfo.UnexplainedVarianceReportingCurrency);
            lnkBtnTotal.Text = Helper.GetDisplayDecimalValue(oExceptionByFSCaptionNetAccountInfo.TotalVar);

            lnkBtnName.CommandArgument = e.Item.ItemIndex.ToString();
            lnkBtnWriteOffOn.CommandArgument = e.Item.ItemIndex.ToString();
            lnkBtnUnExpVar.CommandArgument = e.Item.ItemIndex.ToString();
            lnkBtnTotal.CommandArgument = e.Item.ItemIndex.ToString();

            ExLabel lblName = (ExLabel)e.Item.FindControl("lblName");
            ExLabel lblWriteOffOn = (ExLabel)e.Item.FindControl("lblWriteOffOn");
            ExLabel lblUnExpVar = (ExLabel)e.Item.FindControl("lblUnExpVar");
            ExLabel lblTotal = (ExLabel)e.Item.FindControl("lblTotal");

            lblName.Text = lnkBtnName.Text;
            lblWriteOffOn.Text = Helper.GetDisplayDecimalValue(oExceptionByFSCaptionNetAccountInfo.WriteOnOffAmountReportingCurrency);
            lblUnExpVar.Text = Helper.GetDisplayDecimalValue(oExceptionByFSCaptionNetAccountInfo.UnexplainedVarianceReportingCurrency);
            lblTotal.Text = Helper.GetDisplayDecimalValue(oExceptionByFSCaptionNetAccountInfo.TotalVar);
        }
    }

    protected void rgExceptionByFSCaption_SortCommand(object source, GridSortCommandEventArgs e)
    {
        GridHelper.HandleSortCommand(e);
        LoadExceptionByFSCaptionGridData();
        rgExceptionByFSCaption.DataBind();
    }

    #endregion

    private void GridItemDataBound(GridItemEventArgs e)
    {
        if (e.Item.ItemType == GridItemType.Item
            || e.Item.ItemType == GridItemType.AlternatingItem)
        {
            ExceptionByFSCaptionNetAccountInfo oExceptionByFSCaptionNetAccountInfo = (ExceptionByFSCaptionNetAccountInfo)e.Item.DataItem;

            ExLinkButton lnkBtnName = (ExLinkButton)e.Item.FindControl("lnkBtnName");
            ExLinkButton lnkBtnWriteOffOn = (ExLinkButton)e.Item.FindControl("lnkBtnWriteOffOn");
            ExLinkButton lnkBtnUnExpVar = (ExLinkButton)e.Item.FindControl("lnkBtnUnExpVar");
            ExLinkButton lnkBtnTotal = (ExLinkButton)e.Item.FindControl("lnkBtnTotal");

            lnkBtnName.Text = Helper.GetDisplayStringValue(oExceptionByFSCaptionNetAccountInfo.Name);
            lnkBtnWriteOffOn.Text = Helper.GetDisplayDecimalValue(oExceptionByFSCaptionNetAccountInfo.WriteOnOffAmountReportingCurrency);
            lnkBtnUnExpVar.Text = Helper.GetDisplayDecimalValue(oExceptionByFSCaptionNetAccountInfo.UnexplainedVarianceReportingCurrency);
            lnkBtnTotal.Text = Helper.GetDisplayDecimalValue(oExceptionByFSCaptionNetAccountInfo.TotalVar);

            lnkBtnName.CommandArgument = e.Item.ItemIndex.ToString();
            lnkBtnWriteOffOn.CommandArgument = e.Item.ItemIndex.ToString();
            lnkBtnUnExpVar.CommandArgument = e.Item.ItemIndex.ToString();
            lnkBtnTotal.CommandArgument = e.Item.ItemIndex.ToString();

            ExLabel lblName = (ExLabel)e.Item.FindControl("lblName");
            ExLabel lblWriteOffOn = (ExLabel)e.Item.FindControl("lblWriteOffOn");
            ExLabel lblUnExpVar = (ExLabel)e.Item.FindControl("lblUnExpVar");
            ExLabel lblTotal = (ExLabel)e.Item.FindControl("lblTotal");

            lblName.Text = Helper.GetDisplayStringValue(oExceptionByFSCaptionNetAccountInfo.Name);
            lblWriteOffOn.Text = Helper.GetDisplayDecimalValue(oExceptionByFSCaptionNetAccountInfo.WriteOnOffAmountReportingCurrency);
            lblUnExpVar.Text = Helper.GetDisplayDecimalValue(oExceptionByFSCaptionNetAccountInfo.UnexplainedVarianceReportingCurrency);
            lblTotal.Text = Helper.GetDisplayDecimalValue(oExceptionByFSCaptionNetAccountInfo.TotalVar);
        }
    }


    #region "Grid Events - rgExceptionByNetAccount"
    protected void rgExceptionByNetAccount_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    {
        LoadNetAccountGridData();
    }

    private void LoadNetAccountGridData()
    {
        List<ExceptionByFSCaptionNetAccountInfo> oNetAccountExceptionInfoCollection = null;
        try
        {
            //GetExceptions();
            if (_DashboardExceptionInfo != null)
            {
                oNetAccountExceptionInfoCollection = _DashboardExceptionInfo.NetAccountExceptionInfoCollection;
                LanguageHelper.TranslateLabelFSCaptionExceptionInfo(oNetAccountExceptionInfoCollection);

                rgExceptionByNetAccount.MasterTableView.DataSource = oNetAccountExceptionInfoCollection;
                // Sort the Data
                //GridHelper.SortDataSource(rgExceptionByNetAccount.MasterTableView);
            }
        }
        catch (ARTException ex)
        {
            WebPartHelper.ShowErrorMessage(tblMessage, tblContent, lblMessage, ex);
        }
        catch (Exception ex)
        {
            WebPartHelper.ShowErrorMessage(tblMessage, tblContent, lblMessage, ex);
        }
    }


    protected void rgExceptionByNetAccount_ItemDataBound(object sender, GridItemEventArgs e)
    {
        GridItemDataBound(e);
    }

    protected void rgExceptionByNetAccount_SortCommand(object source, GridSortCommandEventArgs e)
    {
        GridHelper.HandleSortCommand(e);
        LoadNetAccountGridData();
        rgExceptionByNetAccount.DataBind();
    }

    #endregion

    
    protected void rgExceptionByFSCaption_ItemCreated(object sender, GridItemEventArgs e)
    {
        GridHelper.RegisterPDFAndExcelForPostback(e, isExportPDF, isExportExcel, this.Page);
        GridHelper.SetStylesForExportGrid(e, isExportPDF, isExportExcel);
    }

    protected void rgExceptionByFSCaption_OnItemCommand(object sender, GridCommandEventArgs e)
    {
        if (e.CommandName == TelerikConstants.GridExportToPDFCommandName)
        {
            isExportPDF = true;
            LoadExceptionByFSCaptionGridData();
            ShowHideColumnsForExport();
            GridHelper.ExportGridToPDF(rgExceptionByFSCaption, 1034);
        }
        if (e.CommandName == TelerikConstants.GridExportToExcelCommandName)
        {
            isExportExcel = true;
            LoadExceptionByFSCaptionGridData();
            ShowHideColumnsForExport();
            GridHelper.ExportGridToExcel(rgExceptionByFSCaption, 1034);
        }
    }

    private void ShowHideColumnsForExport()
    {

        GridColumn oGridNameDataColumn = rgExceptionByFSCaption.MasterTableView.Columns.FindByUniqueName("NameLblColumn");
        if (oGridNameDataColumn != null)
        {
            oGridNameDataColumn.Visible = true;
        }
        oGridNameDataColumn = rgExceptionByFSCaption.MasterTableView.Columns.FindByUniqueName("WriteOffOnDataColumn");
        if (oGridNameDataColumn != null)
        {
            oGridNameDataColumn.Visible = true;
        }
        oGridNameDataColumn = rgExceptionByFSCaption.MasterTableView.Columns.FindByUniqueName("UnExpVarDataColumn");
        if (oGridNameDataColumn != null)
        {
            oGridNameDataColumn.Visible = true;
        }
        oGridNameDataColumn = rgExceptionByFSCaption.MasterTableView.Columns.FindByUniqueName("TotalDataColumn");
        if (oGridNameDataColumn != null)
        {
            oGridNameDataColumn.Visible = true;
        }

        oGridNameDataColumn = rgExceptionByFSCaption.MasterTableView.Columns.FindByUniqueName("NameLinkButtonColumn");
        if (oGridNameDataColumn != null)
        {
            oGridNameDataColumn.Visible = false;
        }

        oGridNameDataColumn = rgExceptionByFSCaption.MasterTableView.Columns.FindByUniqueName("WriteOffOnLinkButtonColumn");
        if (oGridNameDataColumn != null)
        {
            oGridNameDataColumn.Visible = false;
        }
        oGridNameDataColumn = rgExceptionByFSCaption.MasterTableView.Columns.FindByUniqueName("UnExpVarLinkButtonColumn");
        if (oGridNameDataColumn != null)
        {
            oGridNameDataColumn.Visible = false;
        }
        oGridNameDataColumn = rgExceptionByFSCaption.MasterTableView.Columns.FindByUniqueName("TotalLinkButtonColumn");
        if (oGridNameDataColumn != null)
        {
            oGridNameDataColumn.Visible = false;
        }
    }

    #region Grid Events rgExceptionByNetAccount
    protected void rgExceptionByNetAccount_ItemCreated(object sender, GridItemEventArgs e)
    {
        GridHelper.RegisterPDFAndExcelForPostback(e, isExportPDF, isExportExcel, this.Page);

        GridHelper.SetStylesForExportGrid(e, isExportPDF, isExportExcel);
    }

    protected void rgExceptionByNetAccount_OnItemCommand(object sender, GridCommandEventArgs e)
    {
        if (e.CommandName == TelerikConstants.GridExportToPDFCommandName)
        {
            isExportPDF = true;
            LoadNetAccountGridData();
            ShowHideColumnsForExportForNetAccounts();
            GridHelper.ExportGridToPDF(rgExceptionByNetAccount, 1034);

        }
        if (e.CommandName == TelerikConstants.GridExportToExcelCommandName)
        {
            isExportExcel = true;
            LoadNetAccountGridData();
            ShowHideColumnsForExportForNetAccounts();
            GridHelper.ExportGridToExcel(rgExceptionByNetAccount, 1034);
        }
    }

    private void ShowHideColumnsForExportForNetAccounts()
    {
        rgExceptionByNetAccount.MasterTableView.Columns.FindByUniqueName("NameDataColumn").Visible = true;
        rgExceptionByNetAccount.MasterTableView.Columns.FindByUniqueName("WriteOffOnDataColumn").Visible = true;
        rgExceptionByNetAccount.MasterTableView.Columns.FindByUniqueName("UnExpVarDataColumn").Visible = true;
        rgExceptionByNetAccount.MasterTableView.Columns.FindByUniqueName("TotalDataColumn").Visible = true;

        rgExceptionByNetAccount.MasterTableView.Columns.FindByUniqueName("NameLinkButtonColumn").Visible = false;
        rgExceptionByNetAccount.MasterTableView.Columns.FindByUniqueName("WriteOffOnLinkButtonColumn").Visible = false;
        rgExceptionByNetAccount.MasterTableView.Columns.FindByUniqueName("UnExpVarLinkButtonColumn").Visible = false;
        rgExceptionByNetAccount.MasterTableView.Columns.FindByUniqueName("TotalLinkButtonColumn").Visible = false;
    }

    #endregion

    protected void SendToAccountViewer(object sender, CommandEventArgs e)
    {
        ARTEnums.Grid eGrid = ARTEnums.Grid.AccountViewer;
        int itemIndex = Convert.ToInt32(e.CommandArgument.ToString());
        GridDataItem item = rgExceptionByFSCaption.MasterTableView.Items[itemIndex];
        string FSCaption = item.GetDataKeyValue("Name").ToString();

        short columnID = (short)WebEnums.StaticAccountField.FSCaption;
        short operatorID = (short)WebEnums.Operator.Equals;
        string value = FSCaption;
        SessionHelper.ClearGridFilterDataFromSession(eGrid);
        AccountFilterHelper.AddCriteriaToSessionByDashBoardFSCaption(columnID, operatorID, value, eGrid);

        columnID = (short)WebEnums.StaticAccountField.ShowException;
        operatorID = (short)WebEnums.Operator.Equals;
        value = true.ToString();
        AccountFilterHelper.AddCriteriaToSessionByDashBoardFSCaption(columnID, operatorID, value, eGrid);
        PageSettings oPageSettings = PageSettingHelper.GetPageSettings(WebEnums.ARTPages.AccountViewer);
        oPageSettings.ShowSRAAsWell = true;
        PageSettingHelper.SavePageSettings(WebEnums.ARTPages.AccountViewer, oPageSettings);

        string url = "~/Pages/AccountViewer.aspx?" + QueryStringConstants.IS_SRA + "=1";
        //Response.Redirect(url);
        SessionHelper.RedirectToUrl(url);
        return;
    }

    protected void SendToAccountViewerForNetAccounts(object sender, CommandEventArgs e)
    {
        ARTEnums.Grid eGrid = ARTEnums.Grid.AccountViewer;
        int itemIndex = Convert.ToInt32(e.CommandArgument.ToString());
        GridDataItem item = rgExceptionByNetAccount.MasterTableView.Items[itemIndex];
        string netAccountName = item.GetDataKeyValue("Name").ToString();

        short columnID = (short)WebEnums.StaticAccountField.ShowExceptionNetAccounts;
        short operatorID = (short)WebEnums.Operator.Equals;
        string value = true.ToString();

        SessionHelper.ClearGridFilterDataFromSession(eGrid);
        AccountFilterHelper.AddCriteriaToSessionByDashBoardFSCaption(columnID, operatorID, value, eGrid);

        // TODO: Apoorv - working on Net Accoutn drill-down
        //Added By  Prafull on 15-Jul-2011
        //***********************************************************
        columnID = (short)WebEnums.StaticAccountField.NetAccountName;
        operatorID = (short)WebEnums.Operator.Equals;
        value = netAccountName;
        AccountFilterHelper.AddCriteriaToSessionByDashBoardFSCaption(columnID, operatorID, value, eGrid);
        //***********************************************************
        PageSettings oPageSettings = PageSettingHelper.GetPageSettings(WebEnums.ARTPages.AccountViewer);
        oPageSettings.ShowSRAAsWell = true;
        PageSettingHelper.SavePageSettings(WebEnums.ARTPages.AccountViewer, oPageSettings);

        string url = "~/Pages/AccountViewer.aspx?" + QueryStringConstants.IS_SRA + "=1";
        //Response.Redirect(url);
        SessionHelper.RedirectToUrl(url);
        return;
    }

    private void OnPageLoad()
    {
        pnlExceptionByNetAccount.Visible = true;
        //trMsgNetAccount.Visible = true;
        rgExceptionByFSCaption.NeedDataSource += new Telerik.Web.UI.GridNeedDataSourceEventHandler(rgExceptionByFSCaption_NeedDataSource);
        rgExceptionByNetAccount.NeedDataSource += new GridNeedDataSourceEventHandler(rgExceptionByNetAccount_NeedDataSource);

        if (!Helper.IsCapabilityActivatedForCurrentRecPeriod(ARTEnums.Capability.NetAccount, false))
        {
            pnlExceptionByNetAccount.Visible = false;
            //trMsgNetAccount.Visible = false;
        }

        rgExceptionByFSCaption.Rebind();
        rgExceptionByNetAccount.Rebind();

       
    }




    protected void ompage_ReconciliationPeriodChangedEventHandler(object sender, EventArgs e)
    {
        this.LoadData = true;
    }

    private void ShowHideControl(WebEnums.FeatureCapabilityMode eMode, Control ctl)
    {
        if (eMode == WebEnums.FeatureCapabilityMode.Hidden || eMode == WebEnums.FeatureCapabilityMode.Disable)
        {
            ctl.Visible = false;
        }
        else
        {
            ctl.Visible = true;
        }
    }

}
