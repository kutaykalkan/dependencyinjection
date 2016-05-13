using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SkyStem.ART.Web.Classes;
using SkyStem.ART.Web.Utility;
using SkyStem.ART.Client.IServices;
using SkyStem.ART.Client.Model;
using SkyStem.Library.Controls.WebControls;
using SkyStem.Language.LanguageUtility;
using Telerik.Web.UI;
using SkyStem.ART.Client.Exception;
using SkyStem.Library.Controls.TelerikWebControls.Data;

public partial class Pages_PopupExchangeRates : PopupPageBaseRecPeriod
{
    #region Variables & Constants
    bool isExportPDF;
    bool isExportExcel;
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            isExportPDF = false;
            isExportExcel = false;
            PopupHelper.SetPageTitle(this, 1488);
            lblRecPeriod.Text = string.Format(LanguageUtil.GetValue(2040), Helper.GetDisplayDate(SessionHelper.CurrentReconciliationPeriodEndDate)) + ":";
            BindGrid();
        }

    }

    protected void rgExchangeRates_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {
        if (e.Item.ItemType == Telerik.Web.UI.GridItemType.Item ||
           e.Item.ItemType == Telerik.Web.UI.GridItemType.AlternatingItem)
        {
            if (e.Item.OwnerTableView.Name == "ËxchangeRateArchieve")
            {
                ExLabel lblFromCCYCode = (ExLabel)e.Item.FindControl("lblFromCCYCode");
                ExLabel lblToCCYCode = (ExLabel)e.Item.FindControl("lblToCCYCode");
                ExLabel lblExchangeRate = (ExLabel)e.Item.FindControl("lblExchangeRate");
                ExLabel lblDateAdded = (ExLabel)e.Item.FindControl("lblDateAdded");
                ExchangeRateInfo oExchangeRateInfo = (ExchangeRateInfo)e.Item.DataItem;
                lblFromCCYCode.Text = oExchangeRateInfo.FromCurrencyCode;
                lblToCCYCode.Text = oExchangeRateInfo.ToCurrencyCode;
                lblExchangeRate.Text = Helper.GetDisplayExchangeRateValue(oExchangeRateInfo.ExchangeRate);
                if (oExchangeRateInfo.DateRevised.HasValue)
                    lblDateAdded.Text = Helper.GetDisplayDateTime(oExchangeRateInfo.DateRevised);
                else
                    lblDateAdded.Text = Helper.GetDisplayDateTime(oExchangeRateInfo.DateAdded);
            }
            else
            {
                ExLabel lblFromCurrency = (ExLabel)e.Item.FindControl("lblFromCurrency");
                ExLabel lblToCurrency = (ExLabel)e.Item.FindControl("lblToCurrency");
                ExLabel lblExchangeRates = (ExLabel)e.Item.FindControl("lblExchangeRates");
                ExLabel lblDateAdded = (ExLabel)e.Item.FindControl("lblDateAdded");

                ExchangeRateInfo oExchangeRateInfo = (ExchangeRateInfo)e.Item.DataItem;
                lblFromCurrency.Text = oExchangeRateInfo.FromCurrencyCode;
                lblToCurrency.Text = oExchangeRateInfo.ToCurrencyCode;
                lblExchangeRates.Text = Helper.GetDisplayExchangeRateValue(oExchangeRateInfo.ExchangeRate);
                if (oExchangeRateInfo.DateRevised.HasValue)
                    lblDateAdded.Text = Helper.GetDisplayDateTime(oExchangeRateInfo.DateRevised);
                else
                    lblDateAdded.Text = Helper.GetDisplayDateTime(oExchangeRateInfo.DateAdded);
            }

        }
    }

    protected void rgExchangeRates_GridDetailTableDataBind(object source, GridDetailTableDataBindEventArgs e)
    {
        try
        {
            GridDataItem oGridItem = e.DetailTableView.ParentItem;
            int? exchangeRateID = Convert.ToInt32(oGridItem.GetDataKeyValue("ExchangeRateID"));

            if (exchangeRateID > 0)
            {
                e.DetailTableView.Visible = true;
                if (oGridItem.HasChildItems)
                    oGridItem.ChildItem.Visible = true;
            }
            else
            {
                e.DetailTableView.Visible = false;
                if (oGridItem.HasChildItems)
                    oGridItem.ChildItem.Visible = false;
            }
            switch (e.DetailTableView.Name)
            {
                case "ËxchangeRateArchieve":
                    IUtility oUtilityClient = RemotingHelper.GetUtilityObject();
                    List<ExchangeRateInfo> oExchangeRateInfoList = oUtilityClient.GetCurrencyExchangeRateArchieveByExchangeRateID(exchangeRateID.Value, Helper.GetAppUserInfo());
                    e.DetailTableView.DataSource = oExchangeRateInfoList;
                    break;
            }
        }
        catch (ARTException ex)
        {
            PopupHelper.ShowErrorMessage(this, ex);
        }
        catch (Exception ex)
        {
            PopupHelper.ShowErrorMessage(this, ex);
        }
    }

    protected void rgExchangeRates_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    {
        try
        {
            if (!e.IsFromDetailTable)
            {
                IUtility oUtilityClient = RemotingHelper.GetUtilityObject();
                List<ExchangeRateInfo> oExchangeRateInfoCollection = oUtilityClient.GetCurrencyExchangeRateByRecPeriod((int)SessionHelper.CurrentReconciliationPeriodID, Helper.GetAppUserInfo());
                rgExchangeRates.DataSource = oExchangeRateInfoCollection;
            }
        }
        catch (Exception ex)
        {
            PopupHelper.ShowErrorMessage(this, ex);
        }

    }

    protected void rgExchangeRates_ItemCreated(object sender, GridItemEventArgs e)
    {
        GridHelper.SetStylesForExportGrid(e, isExportPDF, isExportExcel);
    }
    protected void rgExchangeRates_ItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == TelerikConstants.GridExportToPDFCommandName)
            {
                isExportPDF = true;
                GridHelper.ExportGridToPDF(rgExchangeRates, 1488);

            }
            if (e.CommandName == TelerikConstants.GridExportToExcelCommandName)
            {
                isExportExcel = true;
                GridHelper.ExportGridToExcel(rgExchangeRates, 1488);
            }
        }
        catch (Exception ex)
        {
            PopupHelper.ShowErrorMessage(this, ex);
        }
    }

    void BindGrid()
    {
        IUtility oUtilityClient = RemotingHelper.GetUtilityObject();
        List<ExchangeRateInfo> oExchangeRateInfoCollection = oUtilityClient.GetCurrencyExchangeRateByRecPeriod((int)SessionHelper.CurrentReconciliationPeriodID, Helper.GetAppUserInfo());
        rgExchangeRates.DataSource = oExchangeRateInfoCollection;
        rgExchangeRates.DataBind();

    }
}
