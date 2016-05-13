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
using Telerik.Charting;
using Telerik.Web.UI;
using System.Web.UI.DataVisualization.Charting;
using SkyStem.ART.Client.Model;
using SkyStem.ART.Client.IServices;
using SkyStem.ART.Web.Utility;
using SkyStem.Language.LanguageUtility;
using System.Drawing;
using SkyStem.ART.Web.Classes;
using SkyStem.ART.Client.Exception;
using SkyStem.ART.Web.Data;
using System.Threading.Tasks;


public partial class UserControls_Dashboard_UnassignedAccountOwnership : UserControlWebPartBase
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
        return DashboardHelper.GetDataForUnassignedAccountOwnershipAsync(userID, roleID, recPeriodID, Helper.GetAppUserInfo());
    }

    public override void DataLoaded(IAsyncResult result)
    {
        Task<AssignedAccountCountInfo> oTask = (Task<AssignedAccountCountInfo>)result;
        if (oTask.IsCompleted)
        {
            ConfigureChartSettings(oTask.Result);
        }        
    }

    void ConfigureChartSettings()
    {
        AssignedAccountCountInfo oAssignedAccountCountInfo = null;
        int userID = Convert.ToInt32(SessionHelper.CurrentUserID);
        short roleID = Convert.ToInt16(SessionHelper.CurrentRoleID);
        int recPeriodID = Convert.ToInt32(SessionHelper.CurrentReconciliationPeriodID);
        oAssignedAccountCountInfo = DashboardHelper.GetDataForUnassignedAccountOwnership(userID, roleID, recPeriodID, Helper.GetAppUserInfo());
        ConfigureChartSettings(oAssignedAccountCountInfo);
    }
    void ConfigureChartSettings(AssignedAccountCountInfo oAssignedAccountCountInfo)
    {
        try
        {

            int totalAccount = 0;
            int assignedAccount = 0;

            if (oAssignedAccountCountInfo != null)
            {
                totalAccount = System.Convert.ToInt32(oAssignedAccountCountInfo.TotalAccounts);
                assignedAccount = System.Convert.ToInt32(oAssignedAccountCountInfo.TotalAssignedAccounts);

            }

            //***********Chart Setting******************************************************************************

            ////check whether there is any existing Chart/Series,and remove it if any 
            if (chrtAssignedAccountCoverage.ChartAreas.Count > 0)
            {

                ChartArea oChartAreaAssignedAccount = chrtAssignedAccountCoverage.ChartAreas["ChartAreaAssignedAccountCoverage"];
                Series oSeriesAssignedAccount = chrtAssignedAccountCoverage.Series["AssignedAccountCoverage"];

                chrtAssignedAccountCoverage.Series.Remove(oSeriesAssignedAccount);
                chrtAssignedAccountCoverage.ChartAreas.Remove(oChartAreaAssignedAccount);
            }

            ChartArea oChartArea = new ChartArea();
            oChartArea.Name = "ChartAreaAssignedAccountCoverage";
            oChartArea.Area3DStyle.Enable3D = true;
            oChartArea.BorderColor = System.Drawing.Color.Red;
            //oChartArea.BackColor = System.Drawing.Color.SkyBlue;

            oChartArea.Position.X = 0;
            oChartArea.Position.Y = 0;
            oChartArea.Position.Height = 100;           //****its in  percent. Can't be greater than 100
            oChartArea.Position.Width = 100;            //****(in  percent)
            oChartArea.InnerPlotPosition.X = 10;
            oChartArea.InnerPlotPosition.Y = 10;
            oChartArea.InnerPlotPosition.Height = 80;   //****(in  percent)(Area of chart corresponding to plot area )
            oChartArea.InnerPlotPosition.Width = 80;    //****(in  percent) (Area of chart corresponding to plot area )

            ////*****Note:  ChartArea.Area3DStyle.PointDepth is used for how much depth to show in the  pie Chart Control
            oChartArea.Area3DStyle.PointDepth = 600;
            chrtAssignedAccountCoverage.ChartAreas.Add(oChartArea);

            //****** Populate series data
            Series oSeries = new Series();
            oSeries.Name = "AssignedAccountCoverage";
            oSeries.ChartType = SeriesChartType.Pie;

            DataPoint oDataPointAssigned = new DataPoint();
            oDataPointAssigned.SetValueXY("", assignedAccount);
            oDataPointAssigned.AxisLabel = "\n" + "(" + string.Format("{0}: ", LanguageUtil.GetValue(1654)) + assignedAccount + ")";
            oDataPointAssigned.ToolTip = string.Format("{0} ", LanguageUtil.GetValue(1654));
            oDataPointAssigned.Color = ColorTranslator.FromHtml(WebPartHelper.GetStatusColor((short)WebEnums.ReconciliationStatus.Reconciled)); 

            //oDataPointAssigned.Url = "~\\Pages\\AccountViewer.aspx";
            oSeries.Points.Add(oDataPointAssigned);
            oDataPointAssigned["Exploded"] = "true";


            DataPoint oDataPointUnAssigned = new DataPoint();
            oDataPointUnAssigned.SetValueXY("", totalAccount - assignedAccount);
            oDataPointUnAssigned.AxisLabel = "\n" + "(" + string.Format("{0}: ", LanguageUtil.GetValue(1655)) + (totalAccount - assignedAccount) + ")";
            oDataPointUnAssigned.ToolTip = string.Format("{0} ", LanguageUtil.GetValue(1655));
            oDataPointUnAssigned.Color = ColorTranslator.FromHtml(WebPartHelper.GetStatusColor((short)WebEnums.ReconciliationStatus.InProgress )); 

            //oDataPointUnAssigned.Url = "~\\Pages\\AccountViewer.aspx";
            //oDataPointUnAssigned.Color = ColorTranslator.FromHtml("#ffdea4");
            oSeries.Points.Add(oDataPointUnAssigned);

            chrtAssignedAccountCoverage.Series.Add(oSeries);
            //*******Property whether to show Labels outside or inside Pie. Default is Inside 
            chrtAssignedAccountCoverage.Series["AssignedAccountCoverage"]["PieLabelStyle"] = "Outside";
            //*******Label Style Setting 
            chrtAssignedAccountCoverage.Series["AssignedAccountCoverage"].Label = "#PERCENT" + "#AXISLABEL";

            //**********Label font Setting
            for (int i = 0; i < chrtAssignedAccountCoverage.Series["AssignedAccountCoverage"].Points.Count; i++)
            {
                chrtAssignedAccountCoverage.Series["AssignedAccountCoverage"].Points[i].Font = new Font("Arial", 8, FontStyle.Regular);

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

}
