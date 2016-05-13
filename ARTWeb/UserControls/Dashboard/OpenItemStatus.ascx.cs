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
using SkyStem.ART.Client.Exception;
using System.Web.UI.DataVisualization.Charting;
using SkyStem.Language.LanguageUtility;
using SkyStem.ART.Web.Classes;
using SkyStem.ART.Web.Data;
using SkyStem.ART.Web.Utility;
using System.Drawing;
using System.Collections.Generic;
using SkyStem.ART.Client.IServices;
using SkyStem.ART.Client.Model;
using SkyStem.ART.Client.Data;
using System.Threading.Tasks;

public partial class UserControls_Dashboard_OpenItemStatus : UserControlWebPartBase
{
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
        }
    }


    protected void Page_PreRender(object sender, EventArgs e)
    {
        //if (this.LoadData.Value && this.Visible)
        //{
        //    ConfigureChartSettings();
        //}
    }

    protected void ompage_ReconciliationPeriodChangedEventHandler(object sender, EventArgs e)
    {
        this.LoadData = true;
    }

    public override IAsyncResult GetDataAsync()
    {
        int userID = Convert.ToInt32(SessionHelper.CurrentUserID);
        short roleID = Convert.ToInt16(SessionHelper.CurrentRoleID);
        int recPeriodID = Convert.ToInt32(SessionHelper.CurrentReconciliationPeriodID);
        return DashboardHelper.GetDataForOpenItemListAsync(userID, roleID, recPeriodID, Helper.GetAppUserInfo());
    }

    public override void DataLoaded(IAsyncResult result)
    {
        Task<List<OpenItemStatusInfo>> oTask = (Task<List<OpenItemStatusInfo>>)result;
        if (oTask.IsCompleted)
        {
            ConfigureChartSettings(oTask.Result);
        }
    }

    void ConfigureChartSettings()
    {
        List<OpenItemStatusInfo> oOpenItemInfoCollection = null;
        int userID = Convert.ToInt32(SessionHelper.CurrentUserID);
        short roleID = Convert.ToInt16(SessionHelper.CurrentRoleID);
        int recPeriodID = Convert.ToInt32(SessionHelper.CurrentReconciliationPeriodID);
        oOpenItemInfoCollection = DashboardHelper.GetDataForOpenItemList(userID, roleID, recPeriodID, Helper.GetAppUserInfo());
        ConfigureChartSettings(oOpenItemInfoCollection);
    }

    void ConfigureChartSettings(List<OpenItemStatusInfo> oOpenItemInfoCollection)
    {
        try
        {
           // int? totalRecordCountToDisplay = 0;

            //oIncompleteAttributeInfoCollection = oDashboardClient.GetIncompleteAttributeList(userID, roleID, recPeriodID, ref totalRecordCountToDisplay);

            decimal? amountForAgingCategory1 = 0;
            decimal? amountForAgingCategory2 = 0;
            decimal? amountForAgingCategory3 = 0;
            decimal? amountForAgingCategory4 = 0;

            string categoryForAgingCategory1 = string.Empty;
            string categoryForAgingCategory2 = string.Empty;
            string categoryForAgingCategory3 = string.Empty;
            string categoryForAgingCategory4 = string.Empty;

            decimal? maxAmount = 0;

            if (oOpenItemInfoCollection != null)
            {
                foreach (OpenItemStatusInfo oOpenItemStatusInfo in oOpenItemInfoCollection)
                {
                    if (oOpenItemStatusInfo.AgingCategoryId == 1)
                    {
                        amountForAgingCategory1 = oOpenItemStatusInfo.TotalAmountForOpenRecItem;
                        categoryForAgingCategory1 = oOpenItemStatusInfo.AgingCategoryName.ToString();
                        if (oOpenItemStatusInfo.TotalAmountForOpenRecItem > maxAmount)
                        {
                            maxAmount = oOpenItemStatusInfo.TotalAmountForOpenRecItem;
                        }
                    }
                    if (oOpenItemStatusInfo.AgingCategoryId == 2)
                    {
                        amountForAgingCategory2 = oOpenItemStatusInfo.TotalAmountForOpenRecItem;
                        categoryForAgingCategory2 = oOpenItemStatusInfo.AgingCategoryName.ToString();
                        if (oOpenItemStatusInfo.TotalAmountForOpenRecItem > maxAmount)
                        {
                            maxAmount = oOpenItemStatusInfo.TotalAmountForOpenRecItem;
                        }
                    }

                    if (oOpenItemStatusInfo.AgingCategoryId == 3)
                    {
                        amountForAgingCategory3 = oOpenItemStatusInfo.TotalAmountForOpenRecItem;
                        categoryForAgingCategory3 = oOpenItemStatusInfo.AgingCategoryName.ToString();
                        if (oOpenItemStatusInfo.TotalAmountForOpenRecItem > maxAmount)
                        {
                            maxAmount = oOpenItemStatusInfo.TotalAmountForOpenRecItem;
                        }
                    }

                    if (oOpenItemStatusInfo.AgingCategoryId == 4)
                    {
                        amountForAgingCategory4 = oOpenItemStatusInfo.TotalAmountForOpenRecItem;
                        categoryForAgingCategory4 = oOpenItemStatusInfo.AgingCategoryName.ToString();
                        if (oOpenItemStatusInfo.TotalAmountForOpenRecItem > maxAmount)
                        {
                            maxAmount = oOpenItemStatusInfo.TotalAmountForOpenRecItem;
                        }
                    }

                  }
            }

            if (chrtOpenItemStatus.ChartAreas.Count > 0)
            {

                ChartArea oChartAreaIncompleteAttributeList = chrtOpenItemStatus.ChartAreas["ChAreaOpenItemsList"];
                Series oSeriesIncompleteAttributeList = chrtOpenItemStatus.Series["OpenItemList"];

                chrtOpenItemStatus.Series.Remove(oSeriesIncompleteAttributeList);
                chrtOpenItemStatus.ChartAreas.Remove(oChartAreaIncompleteAttributeList);
            }

            ChartArea oChartArea = new ChartArea();
            oChartArea.Name = "ChAreaOpenItemsList";
            oChartArea.Area3DStyle.Enable3D = true;
            oChartArea.BackColor = Color.White;


            chrtOpenItemStatus.ChartAreas.Add(oChartArea);
            chrtOpenItemStatus.ChartAreas["ChAreaOpenItemsList"].AxisX.TitleFont = new Font("Arial", 8, FontStyle.Regular);
            chrtOpenItemStatus.ChartAreas["ChAreaOpenItemsList"].AxisY.TitleFont = new Font("Arial", 8, FontStyle.Regular);
            chrtOpenItemStatus.ChartAreas["ChAreaOpenItemsList"].AxisX.Title = LanguageUtil.GetValue(2440);
            chrtOpenItemStatus.ChartAreas["ChAreaOpenItemsList"].AxisY.Title = LanguageUtil.GetValue(2439);
            chrtOpenItemStatus.ChartAreas["ChAreaOpenItemsList"].AxisY.LabelStyle.Format = "#,##0.00";
            chrtOpenItemStatus.ChartAreas["ChAreaOpenItemsList"].AxisY.LabelStyle.Font = new Font("Arial", 8, FontStyle.Regular);
            chrtOpenItemStatus.ChartAreas["ChAreaOpenItemsList"].AxisX.LabelStyle.Font = new Font("Arial", 8, FontStyle.Regular);
            chrtOpenItemStatus.ChartAreas["ChAreaOpenItemsList"].AxisY.MajorGrid.Enabled = false;
            chrtOpenItemStatus.ChartAreas["ChAreaOpenItemsList"].AxisX.MajorGrid.Enabled = false;

            // Populate series data
            Series oSeries = new Series();
            oSeries.Name = "OpenItemList";
            oSeries.ChartType = SeriesChartType.Column;
            //oSeries.IsValueShownAsLabel = true;


            DataPoint oDataPointReconciliationForm = new DataPoint();
            oDataPointReconciliationForm.SetValueY(Helper.GetDisplayValue(amountForAgingCategory1/1000,WebEnums.DataType.Decimal));
            oDataPointReconciliationForm.AxisLabel = categoryForAgingCategory1;
            //oDataPointReconciliationForm.ToolTip = (Helper.GetDisplayValue(amountForAgingCategory1, WebEnums.DataType.Decimal));
            oDataPointReconciliationForm.Color = ColorTranslator.FromHtml("#027402");// Color.Green;
            oDataPointReconciliationForm.Label = (Helper.GetDisplayValue(amountForAgingCategory1 / 1000, WebEnums.DataType.Decimal));

            DataPoint oDataPointReconciliationForm2 = new DataPoint();
            oDataPointReconciliationForm2.SetValueY(Helper.GetDisplayValue(amountForAgingCategory2 / 1000, WebEnums.DataType.Decimal));
            oDataPointReconciliationForm2.AxisLabel = categoryForAgingCategory2;
           // oDataPointReconciliationForm2.ToolTip = (Helper.GetDisplayValue(amountForAgingCategory2, WebEnums.DataType.Decimal));
            oDataPointReconciliationForm2.Color = ColorTranslator.FromHtml("#F2CDB0");//Color.Blue;
            oDataPointReconciliationForm2.Label = (Helper.GetDisplayValue(amountForAgingCategory2 / 1000, WebEnums.DataType.Decimal));

            DataPoint oDataPointReconciliationForm3 = new DataPoint();
            oDataPointReconciliationForm3.SetValueY(Helper.GetDisplayValue(amountForAgingCategory3 / 1000, WebEnums.DataType.Decimal));
            oDataPointReconciliationForm3.AxisLabel = categoryForAgingCategory3;
           // oDataPointReconciliationForm3.ToolTip = (Helper.GetDisplayValue(amountForAgingCategory3, WebEnums.DataType.Decimal));
            oDataPointReconciliationForm3.Color = ColorTranslator.FromHtml("#EE7B1E");//Color.Yellow;
            oDataPointReconciliationForm3.Label = (Helper.GetDisplayValue(amountForAgingCategory3 / 1000, WebEnums.DataType.Decimal));


            DataPoint oDataPointReconciliationForm4 = new DataPoint();
            oDataPointReconciliationForm4.SetValueY(Helper.GetDisplayValue(amountForAgingCategory4 / 1000, WebEnums.DataType.Decimal));
            oDataPointReconciliationForm4.AxisLabel = categoryForAgingCategory4;
           // oDataPointReconciliationForm4.ToolTip = (Helper.GetDisplayValue(amountForAgingCategory4, WebEnums.DataType.Decimal));
            oDataPointReconciliationForm4.Color = ColorTranslator.FromHtml("#C40200"); //Color.Red;
            oDataPointReconciliationForm4.Label = (Helper.GetDisplayValue(amountForAgingCategory4 / 1000, WebEnums.DataType.Decimal));

            oSeries.Points.Add(oDataPointReconciliationForm);
            oSeries.Points.Add(oDataPointReconciliationForm2);
            oSeries.Points.Add(oDataPointReconciliationForm3);
            oSeries.Points.Add(oDataPointReconciliationForm4);
            chrtOpenItemStatus.Series.Add(oSeries);

            //oSeries.SmartLabelStyle.Enabled = false;
            //oSeries.LabelAngle = -90;

            //*******Property to set the Width of the Column. Default is 1;
            chrtOpenItemStatus.Series["OpenItemList"]["PointWidth"] = "0.4";
            for (int i = 0; i < chrtOpenItemStatus.Series["OpenItemList"].Points.Count; i++)
            {
                chrtOpenItemStatus.Series["OpenItemList"].Points[i].Font = new Font("Arial", 8, FontStyle.Regular);

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
}
