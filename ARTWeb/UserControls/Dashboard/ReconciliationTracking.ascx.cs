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
using SkyStem.ART.Client.Model;
using SkyStem.ART.Client.IServices;
using SkyStem.ART.Web.Utility;
using System.Web.UI.DataVisualization.Charting;
using SkyStem.Language.LanguageUtility;
using System.Drawing;
using SkyStem.ART.Web.Data;
using SkyStem.ART.Web.Classes;
using SkyStem.ART.Client.Exception;
using SkyStem.ART.Client.Data;
using Telerik.Web.UI;
using SkyStem.Library.Controls.TelerikWebControls.Data;
using System.Collections.Generic;
using SkyStem.Library.Controls.WebControls;
using System.Threading.Tasks;

public partial class UserControls_Dashboard_ReconciliationTracking : UserControlWebPartBase
{
    bool isExportPDF;
    bool isExportExcel;

    public ReconciliationTrackingInfo ReconciliationTracking
    {
        get
        {
            return (ReconciliationTrackingInfo)ViewState["ReconciliationTracking"];
        }
        set
        {
            ViewState["ReconciliationTracking"] = value;
        }

    }

    public string ReportingCurrencyCode
    {
        get
        {
            return (string)ViewState["ReportingCurrencyCode"];
        }
        set
        {
            ViewState["ReportingCurrencyCode"] = value;
        }

    }


    protected void Page_Init(object sender, EventArgs e)
    {
        Page oPage = (Page)this.Parent.Page;
        MasterPageBase ompage = (MasterPageBase)oPage.Master;
        ompage.ReconciliationPeriodChangedEventHandler += new EventHandler(ompage_ReconciliationPeriodChangedEventHandler);
        chrtReconciliationTracking.Click += new ImageMapEventHandler(chrtReconciliationTracking_Click);

    }


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            this.LoadData = true;
            isExportPDF = false;
            isExportExcel = false;
            // Set default Sorting
            GridHelper.SetSortExpression(rgReconciliationTrackingStatus.MasterTableView, "ReconciliationStatus");
        }
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (this.LoadData.Value && this.Visible)
        {
            //ConfigureChartSettings();
            //OnPageLoadBindGrid();
        }
    }

    public override IAsyncResult GetDataAsync()
    {
        int userID = Convert.ToInt32(SessionHelper.CurrentUserID);
        short roleID = Convert.ToInt16(SessionHelper.CurrentRoleID);
        int recPeriodID = Convert.ToInt32(SessionHelper.CurrentReconciliationPeriodID);
        return DashboardHelper.GetDataForReconciliationTrackingAsync(userID, roleID, recPeriodID, Helper.GetAppUserInfo());
    }

    public override void DataLoaded(IAsyncResult result)
    {
        Task<ReconciliationTrackingInfo> oTask = (Task<ReconciliationTrackingInfo>)result;
        if (oTask.IsCompleted)
        {
            ConfigureChartSettings(oTask.Result);
            OnPageLoadBindGrid();
        }
    }

    protected void ompage_ReconciliationPeriodChangedEventHandler(object sender, EventArgs e)
    {
        this.LoadData = true;
    }

    void ConfigureChartSettings()
    {
        int userID = Convert.ToInt32(SessionHelper.CurrentUserID);
        short roleID = Convert.ToInt16(SessionHelper.CurrentRoleID);
        int recPeriodID = Convert.ToInt32(SessionHelper.CurrentReconciliationPeriodID);
        ReconciliationTrackingInfo oReconciliationTrackingInfo = DashboardHelper.GetDataForReconciliationTracking(userID, roleID, recPeriodID, Helper.GetAppUserInfo());
        ConfigureChartSettings(oReconciliationTrackingInfo);
    }
    void ConfigureChartSettings(ReconciliationTrackingInfo oReconciliationTrackingInfo )
    {
        try
        {
            //Set the Public Property ReconciliationTracking, (to be used later for grid Binding) 
            this.ReconciliationTracking = oReconciliationTrackingInfo;
            //**********************************************************************************
            int totalAccount = 0;
            int prepared = 0;
            int inProgress = 0;
            int pendingReview = 0;
            int pendingModificationPreparer = 0;
            int reviewed = 0;
            int pendingApproval = 0;
            int approved = 0;
            int notStarted = 0;
            int sysReconciled = 0;
            int reconciled = 0;
            int pendingModificationReviewer = 0;


            if (oReconciliationTrackingInfo != null)
            {
                totalAccount = System.Convert.ToInt32(oReconciliationTrackingInfo.TotalAccounts);

                prepared = System.Convert.ToInt32(oReconciliationTrackingInfo.Prepared);
                inProgress = System.Convert.ToInt32(oReconciliationTrackingInfo.InProgress);
                pendingReview = System.Convert.ToInt32(oReconciliationTrackingInfo.PendingReview);
                pendingModificationPreparer = System.Convert.ToInt32(oReconciliationTrackingInfo.PendingModificationPreparer);
                reviewed = System.Convert.ToInt32(oReconciliationTrackingInfo.Reviewed);
                pendingApproval = System.Convert.ToInt32(oReconciliationTrackingInfo.PendingApproval);
                approved = System.Convert.ToInt32(oReconciliationTrackingInfo.Approved);
                notStarted = System.Convert.ToInt32(oReconciliationTrackingInfo.Notstarted);
                sysReconciled = System.Convert.ToInt32(oReconciliationTrackingInfo.SysReconciled);
                reconciled = System.Convert.ToInt32(oReconciliationTrackingInfo.Reconciled);
                pendingModificationReviewer = System.Convert.ToInt32(oReconciliationTrackingInfo.PendingModificationReviewer);

                this.ReportingCurrencyCode = System.Convert.ToString(oReconciliationTrackingInfo.ReportingCurrencyCode);
            }

            //***********Chart Setting******************************************************************************
            ////check whether there is any existing Chart/Series,and remove it if any 

            if (chrtReconciliationTracking.ChartAreas.Count > 0)
            {

                ChartArea oChartAreaReconciliationTracking = chrtReconciliationTracking.ChartAreas["ChartAreaReconciliationTracking"];
                Series oSeriesReconciliationTracking = chrtReconciliationTracking.Series["ReconciliationTracking"];

                chrtReconciliationTracking.Series.Remove(oSeriesReconciliationTracking);
                chrtReconciliationTracking.ChartAreas.Remove(oChartAreaReconciliationTracking);
            }

            ChartArea oChartArea = new ChartArea();
            oChartArea.Name = "ChartAreaReconciliationTracking";
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
            chrtReconciliationTracking.ChartAreas.Add(oChartArea);

            //****** Populate series data
            Series oSeries = new Series();
            oSeries.Name = "ReconciliationTracking";
            oSeries.ChartType = SeriesChartType.Pie;

            string TargetUrl;
            if (!(prepared == 0))
            {

                DataPoint oDataPointPrepared = new DataPoint();
                oDataPointPrepared.SetValueXY("", prepared);
                oDataPointPrepared.AxisLabel = "\n" + "(" + string.Format("{0}: ", LanguageUtil.GetValue(1089)) + prepared + ")";
                oDataPointPrepared.LabelToolTip = setToolTipDashBoard(1089, prepared);
                oDataPointPrepared.ToolTip = setToolTipDashBoard(1089, prepared);
                //oDataPointPrepared.Url = "~\\Pages\\AccountViewer.aspx";
                PageSettings oPageSettings = PageSettingHelper.GetPageSettings(WebEnums.ARTPages.AccountViewer);
                oPageSettings.ShowSRAAsWell = true;
                PageSettingHelper.SavePageSettings(WebEnums.ARTPages.AccountViewer, oPageSettings);

                TargetUrl = "~\\Pages\\AccountViewer.aspx?" + QueryStringConstants.IS_SRA + "=1";
                oDataPointPrepared.Color = ColorTranslator.FromHtml(WebPartHelper.GetStatusColor((short)WebEnums.ReconciliationStatus.Prepared));
                oSeries.Points.Add(oDataPointPrepared);
                oDataPointPrepared["Exploded"] = "true";
                oDataPointPrepared.PostBackValue = ((short)WebEnums.ReconciliationStatus.Prepared).ToString() + "^" + TargetUrl;
            }

            if (!(inProgress == 0))
            {
                DataPoint oDataPointInProgress = new DataPoint();
                oDataPointInProgress.SetValueXY("", inProgress);
                oDataPointInProgress.AxisLabel = "\n" + "(" + string.Format("{0}: ", LanguageUtil.GetValue(1090)) + inProgress + ")";
                oDataPointInProgress.ToolTip = setToolTipDashBoard(1090, inProgress);
                oDataPointInProgress.LabelToolTip = setToolTipDashBoard(1090, inProgress);
                //oDataPointInProgress.Url = "~\\Pages\\AccountViewer.aspx"; 
                PageSettings oPageSettings = PageSettingHelper.GetPageSettings(WebEnums.ARTPages.AccountViewer);
                oPageSettings.ShowSRAAsWell = true;
                PageSettingHelper.SavePageSettings(WebEnums.ARTPages.AccountViewer, oPageSettings);
                TargetUrl = "~\\Pages\\AccountViewer.aspx?" + QueryStringConstants.IS_SRA + "=1";
                oDataPointInProgress.Color = ColorTranslator.FromHtml(WebPartHelper.GetStatusColor((short)WebEnums.ReconciliationStatus.InProgress));
                oSeries.Points.Add(oDataPointInProgress);
                oDataPointInProgress["Exploded"] = "true";
                oDataPointInProgress.PostBackValue = ((short)WebEnums.ReconciliationStatus.InProgress).ToString() + "^" + TargetUrl;
            }


            if (!(pendingReview == 0))
            {
                DataPoint oDataPointPendingReview = new DataPoint();
                oDataPointPendingReview.SetValueXY("", pendingReview);
                oDataPointPendingReview.AxisLabel = "\n" + "(" + string.Format("{0}: ", LanguageUtil.GetValue(1091)) + pendingReview + ")";
                oDataPointPendingReview.ToolTip = setToolTipDashBoard(1091, pendingReview);
                oDataPointPendingReview.LabelToolTip = setToolTipDashBoard(1091, pendingReview);
                //oDataPointPendingReview.Url = "~\\Pages\\AccountViewer.aspx";
                PageSettings oPageSettings = PageSettingHelper.GetPageSettings(WebEnums.ARTPages.AccountViewer);
                oPageSettings.ShowSRAAsWell = true;
                PageSettingHelper.SavePageSettings(WebEnums.ARTPages.AccountViewer, oPageSettings);

                TargetUrl = "~\\Pages\\AccountViewer.aspx?" + QueryStringConstants.IS_SRA + "=1";
                oDataPointPendingReview.Color = ColorTranslator.FromHtml(WebPartHelper.GetStatusColor((short)WebEnums.ReconciliationStatus.PendingReview));
                oSeries.Points.Add(oDataPointPendingReview);
                oDataPointPendingReview["Exploded"] = "true";
                oDataPointPendingReview.PostBackValue = ((short)WebEnums.ReconciliationStatus.PendingReview).ToString() + "^" + TargetUrl;
            }


            if (!(pendingModificationPreparer == 0))
            {
                DataPoint oDataPointPendingModificationPreparer = new DataPoint();
                oDataPointPendingModificationPreparer.SetValueXY("", pendingModificationPreparer);
                oDataPointPendingModificationPreparer.AxisLabel = "\n" + "(" + string.Format("{0}: ", LanguageUtil.GetValue(1755)) + pendingModificationPreparer + ")";
                oDataPointPendingModificationPreparer.ToolTip = setToolTipDashBoard(1755, pendingModificationPreparer);
                oDataPointPendingModificationPreparer.LabelToolTip = setToolTipDashBoard(1755, pendingModificationPreparer);
                //oDataPointPendingModificationPreparer.Url = "~\\Pages\\AccountViewer.aspx";  

                PageSettings oPageSettings = PageSettingHelper.GetPageSettings(WebEnums.ARTPages.AccountViewer);
                oPageSettings.ShowSRAAsWell = true;
                PageSettingHelper.SavePageSettings(WebEnums.ARTPages.AccountViewer, oPageSettings);

                TargetUrl = "~\\Pages\\AccountViewer.aspx?" + QueryStringConstants.IS_SRA + "=1";
                oDataPointPendingModificationPreparer.Color = ColorTranslator.FromHtml(WebPartHelper.GetStatusColor((short)WebEnums.ReconciliationStatus.PendingModificationPreparer));
                oSeries.Points.Add(oDataPointPendingModificationPreparer);
                oDataPointPendingModificationPreparer["Exploded"] = "true";
                oDataPointPendingModificationPreparer.PostBackValue = ((short)WebEnums.ReconciliationStatus.PendingModificationPreparer).ToString() + "^" + TargetUrl;
            }



            if (!(reviewed == 0))
            {
                DataPoint oDataPointReviewed = new DataPoint();
                oDataPointReviewed.SetValueXY("", reviewed);
                oDataPointReviewed.AxisLabel = "\n" + "(" + string.Format("{0}: ", LanguageUtil.GetValue(1093)) + reviewed + ")";
                oDataPointReviewed.ToolTip = setToolTipDashBoard(1093, reviewed);
                oDataPointReviewed.LabelToolTip = setToolTipDashBoard(1093, reviewed);
                //oDataPointReviewed.Url = "~\\Pages\\AccountViewer.aspx";

                PageSettings oPageSettings = PageSettingHelper.GetPageSettings(WebEnums.ARTPages.AccountViewer);
                oPageSettings.ShowSRAAsWell = true;
                PageSettingHelper.SavePageSettings(WebEnums.ARTPages.AccountViewer, oPageSettings);

                TargetUrl = "~\\Pages\\AccountViewer.aspx?" + QueryStringConstants.IS_SRA + "=1";
                oDataPointReviewed.Color = ColorTranslator.FromHtml(WebPartHelper.GetStatusColor((short)WebEnums.ReconciliationStatus.Reviewed));
                oSeries.Points.Add(oDataPointReviewed);
                oDataPointReviewed["Exploded"] = "true";
                oDataPointReviewed.PostBackValue = ((short)WebEnums.ReconciliationStatus.Reviewed).ToString() + "^" + TargetUrl;
            }


            if (!(pendingApproval == 0))
            {
                DataPoint oDataPointPendingApproval = new DataPoint();
                oDataPointPendingApproval.SetValueXY("", pendingApproval);
                oDataPointPendingApproval.AxisLabel = "\n" + "(" + string.Format("{0}: ", LanguageUtil.GetValue(1094)) + pendingApproval + ")";
                oDataPointPendingApproval.ToolTip = setToolTipDashBoard(1094, pendingApproval);
                oDataPointPendingApproval.LabelToolTip = setToolTipDashBoard(1094, pendingApproval);
                //oDataPointPendingApproval.Url = "~\\Pages\\AccountViewer.aspx";
                PageSettings oPageSettings = PageSettingHelper.GetPageSettings(WebEnums.ARTPages.AccountViewer);
                oPageSettings.ShowSRAAsWell = true;
                PageSettingHelper.SavePageSettings(WebEnums.ARTPages.AccountViewer, oPageSettings);

                TargetUrl = "~\\Pages\\AccountViewer.aspx?" + QueryStringConstants.IS_SRA + "=1";
                oDataPointPendingApproval.Color = ColorTranslator.FromHtml(WebPartHelper.GetStatusColor((short)WebEnums.ReconciliationStatus.PendingApproval));
                oSeries.Points.Add(oDataPointPendingApproval);
                oDataPointPendingApproval["Exploded"] = "true";
                oDataPointPendingApproval.PostBackValue = ((short)WebEnums.ReconciliationStatus.PendingApproval).ToString() + "^" + TargetUrl;
            }

            if (!(approved == 0))
            {
                DataPoint oDataPointApproved = new DataPoint();
                oDataPointApproved.SetValueXY("", approved);
                oDataPointApproved.AxisLabel = "\n" + "(" + string.Format("{0}: ", LanguageUtil.GetValue(1095)) + approved + ")";
                oDataPointApproved.ToolTip = setToolTipDashBoard(1095, approved);
                oDataPointApproved.LabelToolTip = setToolTipDashBoard(1095, approved);
                //oDataPointApproved.Url = "~\\Pages\\AccountViewer.aspx";

                PageSettings oPageSettings = PageSettingHelper.GetPageSettings(WebEnums.ARTPages.AccountViewer);
                oPageSettings.ShowSRAAsWell = true;
                PageSettingHelper.SavePageSettings(WebEnums.ARTPages.AccountViewer, oPageSettings);

                TargetUrl = "~\\Pages\\AccountViewer.aspx?" + QueryStringConstants.IS_SRA + "=1";
                oDataPointApproved.Color = ColorTranslator.FromHtml(WebPartHelper.GetStatusColor((short)WebEnums.ReconciliationStatus.Approved));
                oSeries.Points.Add(oDataPointApproved);
                oDataPointApproved["Exploded"] = "true";
                oDataPointApproved.PostBackValue = ((short)WebEnums.ReconciliationStatus.Approved).ToString() + "^" + TargetUrl;
            }

            if (!(notStarted == 0))
            {

                DataPoint oDataPointNotStarted = new DataPoint();
                oDataPointNotStarted.SetValueXY("", notStarted);
                oDataPointNotStarted.AxisLabel = "\n" + "(" + string.Format("{0}: ", LanguageUtil.GetValue(1475)) + notStarted + ")";
                oDataPointNotStarted.ToolTip = setToolTipDashBoard(1475, notStarted);
                oDataPointNotStarted.LabelToolTip = setToolTipDashBoard(1475, notStarted);


                //If dataPoint color has to be set
                //*************************************************************************
                //oDataPointNotStarted.Color = System.Drawing.ColorTranslator.FromHtml("#90FEFB");
                //**************************************************************************

                //oDataPointNotStarted.Url = "~\\Pages\\AccountViewer.aspx";   

                PageSettings oPageSettings = PageSettingHelper.GetPageSettings(WebEnums.ARTPages.AccountViewer);
                oPageSettings.ShowSRAAsWell = true;
                PageSettingHelper.SavePageSettings(WebEnums.ARTPages.AccountViewer, oPageSettings);

                TargetUrl = "~\\Pages\\AccountViewer.aspx?" + QueryStringConstants.IS_SRA + "=1";
                oDataPointNotStarted.Color = ColorTranslator.FromHtml(WebPartHelper.GetStatusColor((short)WebEnums.ReconciliationStatus.NotStarted));
                oSeries.Points.Add(oDataPointNotStarted);
                oDataPointNotStarted["Exploded"] = "true";
                oDataPointNotStarted.PostBackValue = ((short)WebEnums.ReconciliationStatus.NotStarted).ToString() + "^" + TargetUrl;
            }

            if (!(sysReconciled == 0))
            {
                DataPoint oDataPointSysReconciled = new DataPoint();
                oDataPointSysReconciled.SetValueXY("", sysReconciled);
                oDataPointSysReconciled.AxisLabel = "\n" + "(" + string.Format("{0}: ", LanguageUtil.GetValue(1097)) + sysReconciled + ")";
                oDataPointSysReconciled.ToolTip = setToolTipDashBoard(1097, sysReconciled);
                oDataPointSysReconciled.LabelToolTip = setToolTipDashBoard(1097, sysReconciled);
                //oDataPointSysReconciled.Url = "~\\Pages\\AccountViewer.aspx";

                PageSettings oPageSettings = PageSettingHelper.GetPageSettings(WebEnums.ARTPages.AccountViewer);
                oPageSettings.ShowSRAAsWell = true;
                PageSettingHelper.SavePageSettings(WebEnums.ARTPages.AccountViewer, oPageSettings);

                TargetUrl = "~\\Pages\\AccountViewer.aspx?" + QueryStringConstants.IS_SRA + "=1";
                oDataPointSysReconciled.Color = ColorTranslator.FromHtml(WebPartHelper.GetStatusColor((short)WebEnums.ReconciliationStatus.SysReconciled));
                oSeries.Points.Add(oDataPointSysReconciled);
                oDataPointSysReconciled["Exploded"] = "true";
                oDataPointSysReconciled.PostBackValue = ((short)WebEnums.ReconciliationStatus.SysReconciled).ToString() + "^" + TargetUrl;
            }

            if (!(reconciled == 0))
            {
                DataPoint oDataPointReconciled = new DataPoint();
                oDataPointReconciled.SetValueXY("", reconciled);
                oDataPointReconciled.AxisLabel = "\n" + "(" + string.Format("{0}: ", LanguageUtil.GetValue(1739)) + reconciled + ")";
                oDataPointReconciled.ToolTip = setToolTipDashBoard(1739, reconciled);
                oDataPointReconciled.LabelToolTip = setToolTipDashBoard(1739, reconciled);
                //oDataPointReconciled.Url = "~\\Pages\\AccountViewer.aspx";

                PageSettings oPageSettings = PageSettingHelper.GetPageSettings(WebEnums.ARTPages.AccountViewer);
                oPageSettings.ShowSRAAsWell = true;
                PageSettingHelper.SavePageSettings(WebEnums.ARTPages.AccountViewer, oPageSettings);

                TargetUrl = "~\\Pages\\AccountViewer.aspx?" + QueryStringConstants.IS_SRA + "=1";
                oDataPointReconciled.Color = ColorTranslator.FromHtml(WebPartHelper.GetStatusColor((short)WebEnums.ReconciliationStatus.Reconciled));
                oSeries.Points.Add(oDataPointReconciled);
                oDataPointReconciled["Exploded"] = "true";
                oDataPointReconciled.PostBackValue = ((short)WebEnums.ReconciliationStatus.Reconciled).ToString() + "^" + TargetUrl;
            }

            if (!(pendingModificationReviewer == 0))
            {
                DataPoint oDataPointPendingModificationReviewer = new DataPoint();
                oDataPointPendingModificationReviewer.SetValueXY("", pendingModificationReviewer);
                oDataPointPendingModificationReviewer.AxisLabel = "\n" + "(" + string.Format("{0}: ", LanguageUtil.GetValue(1756)) + pendingModificationReviewer + ")";
                oDataPointPendingModificationReviewer.ToolTip = setToolTipDashBoard(1756, pendingModificationReviewer);
                oDataPointPendingModificationReviewer.LabelToolTip = setToolTipDashBoard(1756, pendingModificationReviewer);
                //oDataPointPendingModificationReviewer.Url = "~\\Pages\\AccountViewer.aspx";

                PageSettings oPageSettings = PageSettingHelper.GetPageSettings(WebEnums.ARTPages.AccountViewer);
                oPageSettings.ShowSRAAsWell = true;
                PageSettingHelper.SavePageSettings(WebEnums.ARTPages.AccountViewer, oPageSettings);


                TargetUrl = "~\\Pages\\AccountViewer.aspx?" + QueryStringConstants.IS_SRA + "=1";
                oDataPointPendingModificationReviewer.Color = ColorTranslator.FromHtml(WebPartHelper.GetStatusColor((short)WebEnums.ReconciliationStatus.PendingModificationReviewer));
                oSeries.Points.Add(oDataPointPendingModificationReviewer);
                oDataPointPendingModificationReviewer["Exploded"] = "true";
                oDataPointPendingModificationReviewer.PostBackValue = ((short)WebEnums.ReconciliationStatus.PendingModificationReviewer).ToString() + "^" + TargetUrl;
            }



            chrtReconciliationTracking.Series.Add(oSeries);



            //*******Property whether to show Labels outside or inside Pie. Default is Inside 
            chrtReconciliationTracking.Series["ReconciliationTracking"]["PieLabelStyle"] = "Outside";
            //*******Label Style Setting 
            chrtReconciliationTracking.Series["ReconciliationTracking"].Label = "#PERCENT" + "#AXISLABEL";

            //**********Label font Setting
            for (int i = 0; i < chrtReconciliationTracking.Series["ReconciliationTracking"].Points.Count; i++)
            {
                chrtReconciliationTracking.Series["ReconciliationTracking"].Points[i].Font = new Font("Arial", 7, FontStyle.Regular);

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

    //private string WebPartHelper.GetStatusColor(int StatusId)
    //{
    //    List<ReconciliationStatusMstInfo> oRecStatusInfoCollection = CacheHelper.GetAllRecStatus();
    //    string StColor;
    //    StColor = oRecStatusInfoCollection.Find(o => o.ReconciliationStatusID == StatusId).StatusColor;
    //    return StColor;
    //}

    protected string setToolTipDashBoard(int? labelID, int labelValue)
    {

        return string.Format("{0}: ", LanguageUtil.GetValue(labelID.Value)) + labelValue.ToString();
    }


    protected void chrtReconciliationTracking_Click(object sender, ImageMapEventArgs e)
    {
        ARTEnums.Grid eGrid = ARTEnums.Grid.AccountViewer;
        string[] ArrPrms = e.PostBackValue.ToString().Split('^');
        string RecStatusID = ArrPrms[0];

        short columnID = (short)WebEnums.StaticAccountField.ReconciliationStatus;
        short operatorID = (short)WebEnums.Operator.Matches;
        string value = RecStatusID;

        SessionHelper.ClearGridFilterDataFromSession(eGrid);
        AccountFilterHelper.AddCriteriaToSessionByDashBoardRecStatus(columnID, operatorID, value, eGrid);

        string Url = ArrPrms[1];
        Response.Redirect(Url);
    }


    #region "Grid Events"

    private void OnPageLoadBindGrid()
    {
        rgReconciliationTrackingStatus.NeedDataSource += new Telerik.Web.UI.GridNeedDataSourceEventHandler(rgReconciliationTrackingStatus_NeedDataSource);
        rgReconciliationTrackingStatus.Rebind();
    }

    protected void rgReconciliationTrackingStatus_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    {
        LoadReconciliationTrackingStatusGridData();
    }

    private void LoadReconciliationTrackingStatusGridData()
    {

        List<ReconciliationStatusMstInfo> oReconciliationStatusMstInfoCollection = new List<ReconciliationStatusMstInfo>();
        oReconciliationStatusMstInfoCollection = GetReconciliationStatusMstCollection(this.ReconciliationTracking);
        rgReconciliationTrackingStatus.DataSource = oReconciliationStatusMstInfoCollection;
        //rgReconciliationTrackingStatus.DataBind();

    }

    protected void rgReconciliationTrackingStatus_ItemCreated(object sender, GridItemEventArgs e)
    {
        GridHelper.RegisterPDFAndExcelForPostback(e, isExportPDF, isExportExcel, this.Page);
        GridHelper.SetStylesForExportGrid(e, isExportPDF, isExportExcel);
    }

    protected void rgReconciliationTrackingStatus_OnItemCommand(object sender, GridCommandEventArgs e)
    {
        if (e.CommandName == TelerikConstants.GridExportToPDFCommandName)
        {
            isExportPDF = true;
            LoadReconciliationTrackingStatusGridData();
            ShowHideColumnsForExport();
            GridHelper.ExportGridToPDF(rgReconciliationTrackingStatus, 2294);
        }
        if (e.CommandName == TelerikConstants.GridExportToExcelCommandName)
        {
            isExportExcel = true;
            LoadReconciliationTrackingStatusGridData();
            ShowHideColumnsForExport();
            GridHelper.ExportGridToExcel(rgReconciliationTrackingStatus, 2294);
        }
    }

    protected void rgReconciliationTrackingStatus_ItemDataBound(object sender, GridItemEventArgs e)
    {
        //GridItemDataBound(e);
        if (e.Item.ItemType == GridItemType.Item
           || e.Item.ItemType == GridItemType.AlternatingItem)
        {
            ReconciliationStatusMstInfo oReconciliationStatusMstInfo = (ReconciliationStatusMstInfo)e.Item.DataItem;

            ExLinkButton lnkRecStatus = (ExLinkButton)e.Item.FindControl("lnkRecStatus");
            ExLinkButton lnkAmount = (ExLinkButton)e.Item.FindControl("lnkAmount");

            lnkRecStatus.Text = Helper.GetDisplayStringValue(oReconciliationStatusMstInfo.ReconciliationStatus);
            //lnkAmount.Text = this.ReportingCurrencyCode + "  " + Helper.GetDisplayDecimalValue(oReconciliationStatusMstInfo.Amount);
            lnkRecStatus.CommandArgument = e.Item.ItemIndex.ToString();
            lnkAmount.CommandArgument = e.Item.ItemIndex.ToString();


            ExLabel lblRecStatus = (ExLabel)e.Item.FindControl("lblRecStatus");
            ExLabel lblAmount = (ExLabel)e.Item.FindControl("lblAmount");
            lblRecStatus.Text = Helper.GetDisplayStringValue(oReconciliationStatusMstInfo.ReconciliationStatus);
            if (oReconciliationStatusMstInfo.Amount.HasValue)
            {
                lblAmount.Text = Helper.GetDisplayDecimalValue(oReconciliationStatusMstInfo.Amount);
                lnkAmount.Text = Helper.GetDisplayDecimalValue(oReconciliationStatusMstInfo.Amount);
                //string Amount = Math.Round(oReconciliationStatusMstInfo.Amount.Value, 0).ToString();
                //lblAmount.Text = this.ReportingCurrencyCode + "  " + Amount;
                //lnkAmount.Text = this.ReportingCurrencyCode + "  " + Amount;
            }



        }
    }

    protected void rgReconciliationTrackingStatus_SortCommand(object source, GridSortCommandEventArgs e)
    {
        GridHelper.HandleSortCommand(e);
        LoadReconciliationTrackingStatusGridData();
        rgReconciliationTrackingStatus.DataBind();
    }

    private List<ReconciliationStatusMstInfo> GetReconciliationStatusMstCollection(ReconciliationTrackingInfo oReconciliationTrackingInfo)
    {

        //Get data from this.ReconciliationTracking Property already filled while binding the Chart control
        List<ReconciliationStatusMstInfo> oReconciliationStatusMstInfoCollection = new List<ReconciliationStatusMstInfo>();
        try
        {
            if (oReconciliationTrackingInfo != null)
            {
                ReconciliationStatusMstInfo oReconciliationStatusMstInfo;

                oReconciliationStatusMstInfo = new ReconciliationStatusMstInfo();
                oReconciliationStatusMstInfo.ReconciliationStatusID = (short)WebEnums.ReconciliationStatus.Prepared;
                oReconciliationStatusMstInfo.ReconciliationStatus = LanguageUtil.GetValue(1089);
                oReconciliationStatusMstInfo.Amount = this.ReconciliationTracking.PreparedAmount;
                if (this.ReconciliationTracking.Prepared != 0)
                {
                    if (oReconciliationStatusMstInfo.Amount != null)
                    {
                        oReconciliationStatusMstInfo.Amount = Math.Abs(oReconciliationStatusMstInfo.Amount.Value);
                        oReconciliationStatusMstInfoCollection.Add(oReconciliationStatusMstInfo);
                    }
                }


                oReconciliationStatusMstInfo = new ReconciliationStatusMstInfo();
                oReconciliationStatusMstInfo.ReconciliationStatusID = (short)WebEnums.ReconciliationStatus.InProgress;
                oReconciliationStatusMstInfo.ReconciliationStatus = LanguageUtil.GetValue(1090);
                oReconciliationStatusMstInfo.Amount = this.ReconciliationTracking.InProgressAmount;
                if (this.ReconciliationTracking.InProgress != 0)
                {
                    if (oReconciliationStatusMstInfo.Amount != null)
                    {
                        oReconciliationStatusMstInfo.Amount = Math.Abs(oReconciliationStatusMstInfo.Amount.Value);
                        oReconciliationStatusMstInfoCollection.Add(oReconciliationStatusMstInfo);
                    }
                }

                oReconciliationStatusMstInfo = new ReconciliationStatusMstInfo();
                oReconciliationStatusMstInfo.ReconciliationStatusID = (short)WebEnums.ReconciliationStatus.PendingReview;
                oReconciliationStatusMstInfo.ReconciliationStatus = LanguageUtil.GetValue(1091);
                oReconciliationStatusMstInfo.Amount = this.ReconciliationTracking.PendingReviewAmount;
                if (this.ReconciliationTracking.PendingReview != 0)
                {
                    if (oReconciliationStatusMstInfo.Amount != null)
                    {
                        oReconciliationStatusMstInfo.Amount = Math.Abs(oReconciliationStatusMstInfo.Amount.Value);
                        oReconciliationStatusMstInfoCollection.Add(oReconciliationStatusMstInfo);
                    }
                }


                oReconciliationStatusMstInfo = new ReconciliationStatusMstInfo();
                oReconciliationStatusMstInfo.ReconciliationStatusID = (short)WebEnums.ReconciliationStatus.PendingModificationPreparer;
                oReconciliationStatusMstInfo.ReconciliationStatus = LanguageUtil.GetValue(1755);
                oReconciliationStatusMstInfo.Amount = this.ReconciliationTracking.PendingModificationPreparerAmount;
                if (this.ReconciliationTracking.PendingModificationPreparer != 0)
                {
                    if (oReconciliationStatusMstInfo.Amount != null)
                    {
                        oReconciliationStatusMstInfo.Amount = Math.Abs(oReconciliationStatusMstInfo.Amount.Value);
                        oReconciliationStatusMstInfoCollection.Add(oReconciliationStatusMstInfo);
                    }
                }

                oReconciliationStatusMstInfo = new ReconciliationStatusMstInfo();
                oReconciliationStatusMstInfo.ReconciliationStatusID = (short)WebEnums.ReconciliationStatus.Reviewed;
                oReconciliationStatusMstInfo.ReconciliationStatus = LanguageUtil.GetValue(1093);
                oReconciliationStatusMstInfo.Amount = this.ReconciliationTracking.ReviewedAmount;
                if (this.ReconciliationTracking.Reviewed != 0)
                {
                    if (oReconciliationStatusMstInfo.Amount != null)
                    {
                        oReconciliationStatusMstInfo.Amount = Math.Abs(oReconciliationStatusMstInfo.Amount.Value);
                        oReconciliationStatusMstInfoCollection.Add(oReconciliationStatusMstInfo);
                    }
                }


                oReconciliationStatusMstInfo = new ReconciliationStatusMstInfo();
                oReconciliationStatusMstInfo.ReconciliationStatusID = (short)WebEnums.ReconciliationStatus.PendingApproval;
                oReconciliationStatusMstInfo.ReconciliationStatus = LanguageUtil.GetValue(1094);
                oReconciliationStatusMstInfo.Amount = this.ReconciliationTracking.PendingApprovalAmount;
                if (this.ReconciliationTracking.PendingApproval != 0)
                {
                    if (oReconciliationStatusMstInfo.Amount != null)
                    {
                        oReconciliationStatusMstInfo.Amount = Math.Abs(oReconciliationStatusMstInfo.Amount.Value);
                        oReconciliationStatusMstInfoCollection.Add(oReconciliationStatusMstInfo);
                    }
                }

                oReconciliationStatusMstInfo = new ReconciliationStatusMstInfo();
                oReconciliationStatusMstInfo.ReconciliationStatusID = (short)WebEnums.ReconciliationStatus.Approved;
                oReconciliationStatusMstInfo.ReconciliationStatus = LanguageUtil.GetValue(1095);
                oReconciliationStatusMstInfo.Amount = this.ReconciliationTracking.ApprovedAmount;
                if (this.ReconciliationTracking.Approved != 0)
                {
                    if (oReconciliationStatusMstInfo.Amount != null)
                    {
                        oReconciliationStatusMstInfo.Amount = Math.Abs(oReconciliationStatusMstInfo.Amount.Value);
                        oReconciliationStatusMstInfoCollection.Add(oReconciliationStatusMstInfo);
                    }
                }

                oReconciliationStatusMstInfo = new ReconciliationStatusMstInfo();
                oReconciliationStatusMstInfo.ReconciliationStatusID = (short)WebEnums.ReconciliationStatus.NotStarted;
                oReconciliationStatusMstInfo.ReconciliationStatus = LanguageUtil.GetValue(1475);
                oReconciliationStatusMstInfo.Amount = this.ReconciliationTracking.NotstartedAmount;
                if (this.ReconciliationTracking.Notstarted != 0)
                {
                    if (oReconciliationStatusMstInfo.Amount != null)
                    {
                        oReconciliationStatusMstInfo.Amount = Math.Abs(oReconciliationStatusMstInfo.Amount.Value);
                        oReconciliationStatusMstInfoCollection.Add(oReconciliationStatusMstInfo);
                    }
                }


                oReconciliationStatusMstInfo = new ReconciliationStatusMstInfo();
                oReconciliationStatusMstInfo.ReconciliationStatusID = (short)WebEnums.ReconciliationStatus.SysReconciled;
                oReconciliationStatusMstInfo.ReconciliationStatus = LanguageUtil.GetValue(1097);
                oReconciliationStatusMstInfo.Amount = this.ReconciliationTracking.SysReconciledAmount;
                if (this.ReconciliationTracking.SysReconciled != 0)
                {
                    if (oReconciliationStatusMstInfo.Amount != null)
                    {
                        oReconciliationStatusMstInfo.Amount = Math.Abs(oReconciliationStatusMstInfo.Amount.Value);
                        oReconciliationStatusMstInfoCollection.Add(oReconciliationStatusMstInfo);
                    }
                }

                oReconciliationStatusMstInfo = new ReconciliationStatusMstInfo();
                oReconciliationStatusMstInfo.ReconciliationStatusID = (short)WebEnums.ReconciliationStatus.Reconciled;
                oReconciliationStatusMstInfo.ReconciliationStatus = LanguageUtil.GetValue(1739);
                oReconciliationStatusMstInfo.Amount = this.ReconciliationTracking.ReconciledAmount;
                if (this.ReconciliationTracking.Reconciled != 0)
                {
                    if (oReconciliationStatusMstInfo.Amount != null)
                    {
                        oReconciliationStatusMstInfo.Amount = Math.Abs(oReconciliationStatusMstInfo.Amount.Value);
                        oReconciliationStatusMstInfoCollection.Add(oReconciliationStatusMstInfo);
                    }
                }

                oReconciliationStatusMstInfo = new ReconciliationStatusMstInfo();
                oReconciliationStatusMstInfo.ReconciliationStatusID = (short)WebEnums.ReconciliationStatus.PendingModificationReviewer;
                oReconciliationStatusMstInfo.ReconciliationStatus = LanguageUtil.GetValue(1756);
                oReconciliationStatusMstInfo.Amount = this.ReconciliationTracking.PendingModificationReviewerAmount;
                if (this.ReconciliationTracking.PendingModificationReviewer != 0)
                {
                    if (oReconciliationStatusMstInfo.Amount != null)
                    {
                        oReconciliationStatusMstInfo.Amount = Math.Abs(oReconciliationStatusMstInfo.Amount.Value);
                        oReconciliationStatusMstInfoCollection.Add(oReconciliationStatusMstInfo);
                    }
                }

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

        finally
        {

        }

        return oReconciliationStatusMstInfoCollection;

    }  //end function


    protected void SendToAccountViewer(object sender, CommandEventArgs e)
    {

        ARTEnums.Grid eGrid = ARTEnums.Grid.AccountViewer;
        int itemIndex = Convert.ToInt32(e.CommandArgument.ToString());
        GridDataItem item = rgReconciliationTrackingStatus.MasterTableView.Items[itemIndex];
        string RecStatusID = item.GetDataKeyValue("ReconciliationStatusID").ToString();

        short columnID = (short)WebEnums.StaticAccountField.ReconciliationStatus;
        short operatorID = (short)WebEnums.Operator.Matches;
        string value = RecStatusID;
        SessionHelper.ClearGridFilterDataFromSession(eGrid);
        AccountFilterHelper.AddCriteriaToSessionByDashBoardRecStatus(columnID, operatorID, value, eGrid);

        PageSettings oPageSettings = PageSettingHelper.GetPageSettings(WebEnums.ARTPages.AccountViewer);
        oPageSettings.ShowSRAAsWell = true;
        PageSettingHelper.SavePageSettings(WebEnums.ARTPages.AccountViewer, oPageSettings);

        string url = "~/Pages/AccountViewer.aspx?" + QueryStringConstants.IS_SRA + "=1";
        Response.Redirect(url);
    }

    private void ShowHideColumnsForExport()
    {

        GridColumn oGridNameDataColumn = rgReconciliationTrackingStatus.MasterTableView.Columns.FindByUniqueName("ReconciliationStatusID");
        if (oGridNameDataColumn != null)
        {
            oGridNameDataColumn.Visible = false;
        }
        oGridNameDataColumn = rgReconciliationTrackingStatus.MasterTableView.Columns.FindByUniqueName("RecStatusLinkButtonColumn");
        if (oGridNameDataColumn != null)
        {
            oGridNameDataColumn.Visible = false;
        }
        oGridNameDataColumn = rgReconciliationTrackingStatus.MasterTableView.Columns.FindByUniqueName("RecStatusLabelColumn");
        if (oGridNameDataColumn != null)
        {
            oGridNameDataColumn.Visible = true;
        }
        oGridNameDataColumn = rgReconciliationTrackingStatus.MasterTableView.Columns.FindByUniqueName("AmountLinkButtonColumn");
        if (oGridNameDataColumn != null)
        {
            oGridNameDataColumn.Visible = false;
        }

        oGridNameDataColumn = rgReconciliationTrackingStatus.MasterTableView.Columns.FindByUniqueName("AmountLabelColumn");
        if (oGridNameDataColumn != null)
        {
            oGridNameDataColumn.Visible = true;
        }


    }





    #endregion





}
