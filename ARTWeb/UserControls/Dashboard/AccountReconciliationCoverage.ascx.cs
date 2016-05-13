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
using System.Web.UI.DataVisualization.Charting;
using SkyStem.ART.Web.Utility;
using SkyStem.ART.Client.IServices;
using SkyStem.ART.Client.Model;
using System.Collections.Generic;
using SkyStem.ART.Web.Data;
using SkyStem.Language.LanguageUtility;
using System;
using System.Drawing;
using SkyStem.ART.Web.Classes;
using SkyStem.ART.Client.Exception;
using SkyStem.ART.Client.Data;
using System.Threading.Tasks;

public partial class UserControls_Dashboard_AccountReconciliationCoverage : UserControlWebPartBase
{
    protected void Page_Init(object sender, EventArgs e)
    {
        Page oPage = (Page)this.Parent.Page;
        MasterPageBase ompage = (MasterPageBase)oPage.Master;
        ompage.ReconciliationPeriodChangedEventHandler += new EventHandler(ompage_ReconciliationPeriodChangedEventHandler);
        chrtReconciliationConverage.Click += new ImageMapEventHandler(chrtReconciliationConverage_Click);
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
            //ConfigureChartSettings();
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
        return DashboardHelper.GetDataForAccountReconciliationCoverageAsync(userID, roleID, recPeriodID, Helper.GetAppUserInfo());
    }

    public override void DataLoaded(IAsyncResult result)
    {
        Task<ReconciledAccountCountBalanceInfo> oTask = (Task<ReconciledAccountCountBalanceInfo>)result;
        if (oTask.IsCompleted)
            ConfigureChartSettings(oTask.Result);
    }

    void ConfigureChartSettings()
    {
        ReconciledAccountCountBalanceInfo oReconciledAccountCountBalanceInfo = new ReconciledAccountCountBalanceInfo();
        int userID = Convert.ToInt32(SessionHelper.CurrentUserID);
        short roleID = Convert.ToInt16(SessionHelper.CurrentRoleID);
        int recPeriodID = Convert.ToInt32(SessionHelper.CurrentReconciliationPeriodID);
        oReconciledAccountCountBalanceInfo = DashboardHelper.GetDataForAccountReconciliationCoverage(userID, roleID, recPeriodID, Helper.GetAppUserInfo());
        ConfigureChartSettings(oReconciledAccountCountBalanceInfo);
    }

    void ConfigureChartSettings(ReconciledAccountCountBalanceInfo oReconciledAccountCountBalanceInfo)
    {
        try
        {
            int totalAccount = 0;
            int reconciledAccount = 0;
            decimal totalAccountGLBalance = 0.0M;
            decimal totalReconciledAccountGLBalance = 0.0M;
            string reportingCurrencyCode = "";


            if (oReconciledAccountCountBalanceInfo != null)
            {
                totalAccount = System.Convert.ToInt32(oReconciledAccountCountBalanceInfo.TotalAccounts);
                reconciledAccount = System.Convert.ToInt32(oReconciledAccountCountBalanceInfo.TotalReconciledAccounts);

                totalAccountGLBalance = System.Convert.ToDecimal(oReconciledAccountCountBalanceInfo.TotalAccountGLBalance);
                totalReconciledAccountGLBalance = System.Convert.ToDecimal(oReconciledAccountCountBalanceInfo.TotalReconciledAccountGLBalance);
                reportingCurrencyCode = System.Convert.ToString(oReconciledAccountCountBalanceInfo.ReportingCurrencyCode);
            }

            //***********Chart Setting******************************************************************************

            ////check whether there is any existing Chart/Series,and remove it if any 

            if (chrtReconciliationConverage.ChartAreas.Count > 0)
            {

                ChartArea oChartAreaReconciliationConverage = chrtReconciliationConverage.ChartAreas["ChartAreaReconciliationConverage"];
                Series oSeriesReconciliationConverage = chrtReconciliationConverage.Series["ReconciliationConverage"];

                chrtReconciliationConverage.Series.Remove(oSeriesReconciliationConverage);
                chrtReconciliationConverage.ChartAreas.Remove(oChartAreaReconciliationConverage);
            }



            ChartArea oChartArea = new ChartArea();
            oChartArea.Name = "ChartAreaReconciliationConverage";
            oChartArea.Area3DStyle.Enable3D = true;
            oChartArea.BorderColor = System.Drawing.Color.Red;

            oChartArea.Position.X = 0;
            oChartArea.Position.Y = 0;
            oChartArea.Position.Height = 100;           //****its in  percent. Can't be greater than 100
            oChartArea.Position.Width = 100;            //****(in  percent)
            oChartArea.InnerPlotPosition.X = 10;
            oChartArea.InnerPlotPosition.Y = 10;
            oChartArea.InnerPlotPosition.Height = 80;   //****(in  percent)(Area of chart corresponding to plot area )
            oChartArea.InnerPlotPosition.Width = 80;    //****(in  percent) (Area of chart corresponding to plot area)

            ////*****Note:  ChartArea.Area3DStyle.PointDepth : to show the depth  in the  pie Chart Control
            oChartArea.Area3DStyle.PointDepth = 600;
            chrtReconciliationConverage.ChartAreas.Add(oChartArea);

            //****** Populate series data

            Series oSeries = new Series();
            oSeries.Name = "ReconciliationConverage";
            oSeries.ChartType = SeriesChartType.Pie;

            DataPoint oDataPointReconciled = new DataPoint();
            oDataPointReconciled.SetValueXY("", reconciledAccount);
            //oDataPointReconciled.AxisLabel = "\n" + "(" + string.Format("{0}: ", LanguageUtil.GetValue(1739)) + reconciledAccount + ")";
            oDataPointReconciled.AxisLabel = "\n" + "(" + string.Format("{0}: ", LanguageUtil.GetValue(1739)) + reconciledAccount + ")" +
                                             "\n" + "(" + string.Format("{0}: \n" + reportingCurrencyCode + " ", LanguageUtil.GetValue(1510)) + Helper.GetDisplayIntegerValue(Convert.ToInt64(Math.Round(Math.Abs(totalReconciledAccountGLBalance)))) + ")";



            oDataPointReconciled.ToolTip = string.Format("{0} ", LanguageUtil.GetValue(1739));
            oDataPointReconciled.Color = ColorTranslator.FromHtml(WebPartHelper.GetStatusColor((short)WebEnums.ReconciliationStatus.Reconciled));

            //oDataPointReconciled.Url = "~\\Pages\\AccountViewer.aspx";
            oSeries.Points.Add(oDataPointReconciled);
            oDataPointReconciled["Exploded"] = "true";

            oDataPointReconciled.PostBackValue = ((short)WebEnums.ReconciliationStatus.Reconciled).ToString()
                + AccountFilterHelper.AccountFilterValueSeparator + ((short)WebEnums.ReconciliationStatus.SysReconciled).ToString();

            DataPoint oDataPointUnreconciled = new DataPoint();
            oDataPointUnreconciled.SetValueXY("", totalAccount - reconciledAccount);
            //oDataPointUnreconciled.AxisLabel = "\n" + "(" + string.Format("{0}: ", LanguageUtil.GetValue(1868)) + (totalAccount - reconciledAccount) + ")";
            oDataPointUnreconciled.AxisLabel = "\n" + "(" + string.Format("{0}: ", LanguageUtil.GetValue(1868)) + (totalAccount - reconciledAccount) + ")" +
                                                 "\n" + "(" + string.Format("{0}: \n" + reportingCurrencyCode + " ", LanguageUtil.GetValue(1510)) + Helper.GetDisplayIntegerValue(Convert.ToInt64(Math.Round(Math.Abs(totalAccountGLBalance - totalReconciledAccountGLBalance)))) + ")";

            oDataPointUnreconciled.Color = ColorTranslator.FromHtml(WebPartHelper.GetStatusColor((short)WebEnums.ReconciliationStatus.InProgress));


            oDataPointUnreconciled.ToolTip = string.Format("{0} ", LanguageUtil.GetValue(1868));
            //oDataPointUnreconciled.Color = ColorTranslator.FromHtml("#ffdea4");

            oSeries.Points.Add(oDataPointUnreconciled);
            oDataPointUnreconciled.PostBackValue = ((short)WebEnums.ReconciliationStatus.Approved).ToString()
                + AccountFilterHelper.AccountFilterValueSeparator + ((short)WebEnums.ReconciliationStatus.InProgress).ToString()
                + AccountFilterHelper.AccountFilterValueSeparator + ((short)WebEnums.ReconciliationStatus.NotStarted).ToString()
                + AccountFilterHelper.AccountFilterValueSeparator + ((short)WebEnums.ReconciliationStatus.PendingApproval).ToString()
                + AccountFilterHelper.AccountFilterValueSeparator + ((short)WebEnums.ReconciliationStatus.PendingModificationPreparer).ToString()
                + AccountFilterHelper.AccountFilterValueSeparator + ((short)WebEnums.ReconciliationStatus.PendingModificationReviewer).ToString()
                + AccountFilterHelper.AccountFilterValueSeparator + ((short)WebEnums.ReconciliationStatus.PendingReview).ToString()
                + AccountFilterHelper.AccountFilterValueSeparator + ((short)WebEnums.ReconciliationStatus.Prepared).ToString()
                + AccountFilterHelper.AccountFilterValueSeparator + ((short)WebEnums.ReconciliationStatus.PendingReview).ToString()
                + AccountFilterHelper.AccountFilterValueSeparator + ((short)WebEnums.ReconciliationStatus.Reviewed).ToString();


            //Add the Series to the chart
            chrtReconciliationConverage.Series.Add(oSeries);
            //*******Property whether to show Labels outside or inside Pie. Default is Inside 
            chrtReconciliationConverage.Series["ReconciliationConverage"]["PieLabelStyle"] = "Outside";
            //*******Label Style Setting 
            chrtReconciliationConverage.Series["ReconciliationConverage"].Label = "#PERCENT" + "#AXISLABEL";

            //**********Label font Setting
            for (int i = 0; i < chrtReconciliationConverage.Series["ReconciliationConverage"].Points.Count; i++)
            {
                chrtReconciliationConverage.Series["ReconciliationConverage"].Points[i].Font = new Font("Arial", 8, FontStyle.Regular);

            }
            //*******Display the total accounts
            lblTotalAccounts.Text = Helper.GetDisplayIntegerValue(totalAccount);
            //***************************************************************************************
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

    protected void chrtReconciliationConverage_Click(object sender, ImageMapEventArgs e)
    {
        ARTEnums.Grid eGrid = ARTEnums.Grid.AccountViewer;
        string ReconciliationStatusID = e.PostBackValue;

        short columnID = (short)WebEnums.StaticAccountField.ReconciliationStatus;
        short operatorID = (short)WebEnums.Operator.Matches;
        string value = ReconciliationStatusID;
        SessionHelper.ClearGridFilterDataFromSession(eGrid);
        AccountFilterHelper.AddCriteriaToSessionByDashBoardRecStatus(columnID, operatorID, value, eGrid);
        PageSettings oPageSettings = PageSettingHelper.GetPageSettings(WebEnums.ARTPages.AccountViewer);
        oPageSettings.ShowSRAAsWell = true;
        PageSettingHelper.SavePageSettings(WebEnums.ARTPages.AccountViewer, oPageSettings);
        string Url = "~\\Pages\\AccountViewer.aspx?" + QueryStringConstants.IS_SRA + "=1";
        Response.Redirect(Url);
    }



}
