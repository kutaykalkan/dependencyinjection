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
using System.Web.UI.DataVisualization.Charting;
using SkyStem.ART.Client.Model;
using System.Collections.Generic;
using SkyStem.ART.Client.IServices;
using SkyStem.ART.Web.Utility;
using SkyStem.ART.Client.Data;
using SkyStem.Language.LanguageUtility;
using SkyStem.ART.Web.Data;
using System.Drawing;
using SkyStem.ART.Web.Classes;
using SkyStem.ART.Client.Exception;
using System.Threading.Tasks;


public partial class UserControls_Dashboard_IncompleteAttributeList : UserControlWebPartBase
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
        ////ConfigureChartSettings();
        //}
    }

    protected void ompage_ReconciliationPeriodChangedEventHandler(object sender, EventArgs e)
    {
        this.LoadData = true;
    }


    void ConfigureChartSettings()
    {
        try
        {
            int? totalRecordCountToDisplay = 0;

            List<IncompleteAttributeInfo> oIncompleteAttributeInfoCollection = new List<IncompleteAttributeInfo>();
            IDashboard oDashboardClient = RemotingHelper.GetDashboardObject();
            int userID = Convert.ToInt32(SessionHelper.CurrentUserID);
            short roleID = Convert.ToInt16(SessionHelper.CurrentRoleID);
            int recPeriodID = Convert.ToInt32(SessionHelper.CurrentReconciliationPeriodID);
            oIncompleteAttributeInfoCollection = oDashboardClient.GetIncompleteAttributeList(userID, roleID, recPeriodID, Helper.GetAppUserInfo());
            //oIncompleteAttributeInfoCollection = oDashboardClient.GetIncompleteAttributeList(userID, roleID, recPeriodID, ref totalRecordCountToDisplay);
            if (oIncompleteAttributeInfoCollection != null && oIncompleteAttributeInfoCollection.Count > 0)
                totalRecordCountToDisplay = oIncompleteAttributeInfoCollection[0].TotalRecordCountToDisplay;
            int reconciliationTemplate = 0;
            decimal incompleteReconciliationTemplatePercentage = 100;
            int keyAccount = 0;
            decimal incompleteKeyAccountPercentage = 100;
            int zeroBalanceAccount = 0;
            decimal incompleteZeroBalanceAccountPercentage = 100;
            int riskRating = 0;
            decimal incompleteRiskRatingPercentage = 100;
            int frequency = 0;
            decimal incompleteFrequencyPercentage = 100;
            int preparerDueDays = 0;
            decimal incompletePreparerDueDaysPercentage = 100;
            int reviewerDueDays = 0;
            decimal incompleteReviewerDueDaysPercentage = 100;
            int approverDueDays = 0;
            decimal incompleteApproverDueDaysPercentage = 100;
            int totalAccountCount = 0;
            //bool isRiskRating = false;
            bool isRiskRating = Helper.IsCapabilityActivatedForCurrentRecPeriod(ARTEnums.Capability.RiskRating, false);
            bool isKeyAccount = Helper.IsCapabilityActivatedForCurrentRecPeriod(ARTEnums.Capability.KeyAccount, false);
            bool isZeroBalanceAccount = Helper.IsCapabilityActivatedForCurrentRecPeriod(ARTEnums.Capability.ZeroBalanceAccount, false);
            bool isDueDateByAccount = Helper.IsCapabilityActivatedForCurrentRecPeriod(ARTEnums.Capability.DueDateByAccount, false);
            bool isDualLevelReview = Helper.IsCapabilityActivatedForCurrentRecPeriod(ARTEnums.Capability.DualLevelReview, false);


            if (oIncompleteAttributeInfoCollection != null)
            {
                foreach (IncompleteAttributeInfo oIncompleteAttributeInfo in oIncompleteAttributeInfoCollection)
                {
                    //*******Check for the ReconciliationTemplate
                    if (oIncompleteAttributeInfo.AccountAttributeID == System.Convert.ToInt32(ARTEnums.AccountAttribute.ReconciliationTemplate))
                    {
                        reconciliationTemplate = System.Convert.ToInt32(oIncompleteAttributeInfo.CompletedAttributeAccountCount);
                        totalAccountCount = System.Convert.ToInt32(oIncompleteAttributeInfo.TotalAccounts);
                        incompleteReconciliationTemplatePercentage = GetIncompletePercentageValue(reconciliationTemplate, totalAccountCount);
                    }
                    //*******Check for the Key Account**********************
                    if (oIncompleteAttributeInfo.AccountAttributeID == System.Convert.ToInt32(ARTEnums.AccountAttribute.IsKeyAccount))
                    {
                        keyAccount = System.Convert.ToInt32(oIncompleteAttributeInfo.CompletedAttributeAccountCount);
                        totalAccountCount = System.Convert.ToInt32(oIncompleteAttributeInfo.TotalAccounts);
                        incompleteKeyAccountPercentage = GetIncompletePercentageValue(keyAccount, totalAccountCount);
                    }

                    //*******Check for the Zero Balance Account**********************
                    if (oIncompleteAttributeInfo.AccountAttributeID == System.Convert.ToInt32(ARTEnums.AccountAttribute.IsZeroBalanceAccount))
                    {
                        zeroBalanceAccount = System.Convert.ToInt32(oIncompleteAttributeInfo.CompletedAttributeAccountCount);
                        totalAccountCount = System.Convert.ToInt32(oIncompleteAttributeInfo.TotalAccounts);
                        incompleteZeroBalanceAccountPercentage = GetIncompletePercentageValue(zeroBalanceAccount, totalAccountCount);
                    }

                    //*******Check for the Risk Rating**********************
                    if (oIncompleteAttributeInfo.AccountAttributeID == System.Convert.ToInt32(ARTEnums.AccountAttribute.RiskRating))
                    {
                        //isRiskRating = true;   //Variable to show whether RiskRating is enabled or not
                        riskRating = System.Convert.ToInt32(oIncompleteAttributeInfo.CompletedAttributeAccountCount);
                        totalAccountCount = System.Convert.ToInt32(oIncompleteAttributeInfo.TotalAccounts);
                        incompleteRiskRatingPercentage = GetIncompletePercentageValue(riskRating, totalAccountCount);
                    }

                    //*******Check for the Frequency**********************
                    if (oIncompleteAttributeInfo.AccountAttributeID == 0)
                    {
                        frequency = System.Convert.ToInt32(oIncompleteAttributeInfo.CompletedAttributeAccountCount);
                        totalAccountCount = System.Convert.ToInt32(oIncompleteAttributeInfo.TotalAccounts);
                        incompleteFrequencyPercentage = GetIncompletePercentageValue(frequency, totalAccountCount);
                    }
                    //*******Check for Preparer Due Days **********************
                    if (oIncompleteAttributeInfo.AccountAttributeID == System.Convert.ToInt32(ARTEnums.AccountAttribute.PreparerDueDays))
                    {
                        preparerDueDays = System.Convert.ToInt32(oIncompleteAttributeInfo.CompletedAttributeAccountCount);
                        totalAccountCount = System.Convert.ToInt32(oIncompleteAttributeInfo.TotalAccounts);
                        incompletePreparerDueDaysPercentage = GetIncompletePercentageValue(preparerDueDays, totalAccountCount);
                    }
                    //*******Check for Reviewer Due Days **********************
                    if (oIncompleteAttributeInfo.AccountAttributeID == System.Convert.ToInt32(ARTEnums.AccountAttribute.ReviewerDueDays))
                    {
                        reviewerDueDays = System.Convert.ToInt32(oIncompleteAttributeInfo.CompletedAttributeAccountCount);
                        totalAccountCount = System.Convert.ToInt32(oIncompleteAttributeInfo.TotalAccounts);
                        incompleteReviewerDueDaysPercentage = GetIncompletePercentageValue(reviewerDueDays, totalAccountCount);
                    }
                    //*******Check for Approver Due Days **********************
                    if (oIncompleteAttributeInfo.AccountAttributeID == System.Convert.ToInt32(ARTEnums.AccountAttribute.ApproverDueDays))
                    {
                        approverDueDays = System.Convert.ToInt32(oIncompleteAttributeInfo.CompletedAttributeAccountCount);
                        totalAccountCount = System.Convert.ToInt32(oIncompleteAttributeInfo.TotalAccounts);
                        incompleteApproverDueDaysPercentage = GetIncompletePercentageValue(approverDueDays, totalAccountCount);
                    }
                }
            }


            //*******Display the total accounts
            lblTotalAccounts.Text = Helper.GetDisplayIntegerValue(totalRecordCountToDisplay);

            //Check condition : If the Total Account Count is Zero, Don't Show the Graph    
            if (totalRecordCountToDisplay == 0M)
            {
                chrtIncompleteAttributeList.Visible = false;
                lblMsg.Visible = false;
                return;
            }

            //Check condition : If All the Data point are Zero,  Don't Show the Graph    
            //Modified :(on 11-Mar-2011 by Prafull) Check condition : If All the Data point are Zero, Show the message but don't hide the graph
            if (incompleteReconciliationTemplatePercentage == 0 && incompleteKeyAccountPercentage == 0
                && incompleteZeroBalanceAccountPercentage == 0 && incompletePreparerDueDaysPercentage == 0
                && incompleteReviewerDueDaysPercentage == 0 && incompleteApproverDueDaysPercentage == 0)
            {
                if (isRiskRating == true && incompleteRiskRatingPercentage == 0)
                {
                    //chrtIncompleteAttributeList.Visible = false;
                    lblMsg.Visible = true;
                    //return;

                }
                else if (isRiskRating == false && incompleteFrequencyPercentage == 0)
                {
                    //chrtIncompleteAttributeList.Visible = false;
                    lblMsg.Visible = true;
                    //return;
                }
                else
                {
                    lblMsg.Visible = false;
                }
            }
            else
            {
                lblMsg.Visible = false;
            }

            //***********Chart Setting******************************************************************************

            ////check whether there is any existing Chart/Series,and remove it if any 
            if (chrtIncompleteAttributeList.ChartAreas.Count > 0)
            {

                ChartArea oChartAreaIncompleteAttributeList = chrtIncompleteAttributeList.ChartAreas["ChAreaIncompleteAttributeList"];
                Series oSeriesIncompleteAttributeList = chrtIncompleteAttributeList.Series["IncompleteAttributeList"];

                chrtIncompleteAttributeList.Series.Remove(oSeriesIncompleteAttributeList);
                chrtIncompleteAttributeList.ChartAreas.Remove(oChartAreaIncompleteAttributeList);
            }


            ChartArea oChartArea = new ChartArea();
            oChartArea.Name = "ChAreaIncompleteAttributeList";
            oChartArea.Area3DStyle.Enable3D = true;
            oChartArea.BackColor = Color.White;


            //Note:  ChartArea.Area3DStyle.PointDepth is used for how much depth we want in the  pie Chart Control
            //oChartArea.Area3DStyle.PointDepth = 100;
            chrtIncompleteAttributeList.ChartAreas.Add(oChartArea);
            chrtIncompleteAttributeList.ChartAreas["ChAreaIncompleteAttributeList"].AxisX.Title = string.Format("{0}", LanguageUtil.GetValue(1554));
            chrtIncompleteAttributeList.ChartAreas["ChAreaIncompleteAttributeList"].AxisY.Title = string.Format("{0}", LanguageUtil.GetValue(1925)) + "  [ % ]";
            chrtIncompleteAttributeList.ChartAreas["ChAreaIncompleteAttributeList"].AxisY.Maximum = 100;


            // Populate series data
            Series oSeries = new Series();
            oSeries.Name = "IncompleteAttributeList";
            oSeries.ChartType = SeriesChartType.Column;
            //oSeries.IsValueShownAsLabel = true;


            DataPoint oDataPointReconciliationForm = new DataPoint();
            oDataPointReconciliationForm.SetValueY(incompleteReconciliationTemplatePercentage);
            oDataPointReconciliationForm.AxisLabel = string.Format("{0}", LanguageUtil.GetValue(1366));
            oDataPointReconciliationForm.Label = string.Format("{0}", incompleteReconciliationTemplatePercentage) + "%\n" + "(" + (totalAccountCount - reconciliationTemplate) + ")";
            oSeries.Points.Add(oDataPointReconciliationForm);


            //*************************************************************************************
            if (isKeyAccount == true)
            {
                DataPoint oDataPointKeyAccount = new DataPoint();
                oDataPointKeyAccount.SetValueY(incompleteKeyAccountPercentage);
                oDataPointKeyAccount.AxisLabel = string.Format("{0}", LanguageUtil.GetValue(1014));
                oDataPointKeyAccount.Label = string.Format("{0}", incompleteKeyAccountPercentage) + "%\n" + "(" + +(totalAccountCount - keyAccount) + ")";
                oSeries.Points.Add(oDataPointKeyAccount);
            }
            //***************************************************************************************
            if (isZeroBalanceAccount == true)
            {
                DataPoint oDataPointZeroBalanceAccount = new DataPoint();
                oDataPointZeroBalanceAccount.SetValueY(incompleteZeroBalanceAccountPercentage);
                oDataPointZeroBalanceAccount.AxisLabel = string.Format("{0}", LanguageUtil.GetValue(1256));
                oDataPointZeroBalanceAccount.Label = string.Format("{0}", incompleteZeroBalanceAccountPercentage) + "%\n" + "(" + (totalAccountCount - zeroBalanceAccount) + ")";
                oSeries.Points.Add(oDataPointZeroBalanceAccount);
            }
            //*************************************************************************************


            if (isRiskRating == true)
            {

                DataPoint oDataPointRiskRating = new DataPoint();
                oDataPointRiskRating.SetValueY(incompleteRiskRatingPercentage);
                oDataPointRiskRating.AxisLabel = string.Format("{0}", LanguageUtil.GetValue(1013));
                oDataPointRiskRating.Label = string.Format("{0}", incompleteRiskRatingPercentage) + "%\n" + "(" + (totalAccountCount - riskRating) + ")";
                oSeries.Points.Add(oDataPointRiskRating);
            }
            else
            {
                DataPoint oDataPointfrequency = new DataPoint();
                oDataPointfrequency.SetValueY(incompleteFrequencyPercentage);
                oDataPointfrequency.AxisLabel = string.Format("{0}", LanguageUtil.GetValue(1427));
                oDataPointfrequency.Label = string.Format("{0}", incompleteFrequencyPercentage) + "%\n" + "(" + (totalAccountCount - frequency) + ")";
                oSeries.Points.Add(oDataPointfrequency);
            }

            if (isDueDateByAccount)
            {
                DataPoint oDataPointPreparerDueDays = new DataPoint();
                oDataPointPreparerDueDays.SetValueY(incompletePreparerDueDaysPercentage);
                oDataPointPreparerDueDays.AxisLabel = string.Format("{0}", LanguageUtil.GetValue(2752));
                oDataPointPreparerDueDays.Label = string.Format("{0}", incompletePreparerDueDaysPercentage) + "%\n" + "(" + (totalAccountCount - preparerDueDays) + ")";
                oSeries.Points.Add(oDataPointPreparerDueDays);

                DataPoint oDataPointReviewerDueDays = new DataPoint();
                oDataPointReviewerDueDays.SetValueY(incompleteReviewerDueDaysPercentage);
                oDataPointReviewerDueDays.AxisLabel = string.Format("{0}", LanguageUtil.GetValue(2753));
                oDataPointReviewerDueDays.Label = string.Format("{0}", incompleteReviewerDueDaysPercentage) + "%\n" + "(" + (totalAccountCount - reviewerDueDays) + ")";
                oSeries.Points.Add(oDataPointReviewerDueDays);

                if (isDualLevelReview && !Helper.IsDualLevelReviewByAccountActivated())
                {
                    DataPoint oDataPointApproverDueDays = new DataPoint();
                    oDataPointApproverDueDays.SetValueY(incompleteApproverDueDaysPercentage);
                    oDataPointApproverDueDays.AxisLabel = string.Format("{0}", LanguageUtil.GetValue(2754));
                    oDataPointApproverDueDays.Label = string.Format("{0}", incompleteApproverDueDaysPercentage) + "%\n" + "(" + (totalAccountCount - approverDueDays) + ")";
                    oSeries.Points.Add(oDataPointApproverDueDays);
                }
            }

            chrtIncompleteAttributeList.Series.Add(oSeries);
            //*******Property to set the Width of the Column. Default is 1;
            chrtIncompleteAttributeList.Series["IncompleteAttributeList"]["PointWidth"] = "0.6";
            //*******Property whether to show Labels outside or inside Column/Bar.(Currently this property is not working) 
            //chrtIncompleteAttributeList.Series["IncompleteAttributeList"]["BarLabelStyle"] = "Center";



            //**********Label font and Color Setting
            for (int i = 0; i < chrtIncompleteAttributeList.Series["IncompleteAttributeList"].Points.Count; i++)
            {
                chrtIncompleteAttributeList.Series["IncompleteAttributeList"].Points[i].Font = new Font("Arial", 8, FontStyle.Regular);
                chrtIncompleteAttributeList.Series["IncompleteAttributeList"].Points[i].Color = ColorTranslator.FromHtml(WebPartHelper.GetStatusColor((short)WebEnums.ReconciliationStatus.InProgress));

                //chrtIncompleteAttributeList.Series["IncompleteAttributeList"].Points[i].Color = ColorTranslator.FromHtml("#ffdea4");

            }

            //this.Page.Controls.Add(Chart1);
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


    public override IAsyncResult GetDataAsync()
    {
        int userID = Convert.ToInt32(SessionHelper.CurrentUserID);
        short roleID = Convert.ToInt16(SessionHelper.CurrentRoleID);
        int recPeriodID = Convert.ToInt32(SessionHelper.CurrentReconciliationPeriodID);
        return DashboardHelper.GetIncompleteAttributeListAsync(userID, roleID, recPeriodID, Helper.GetAppUserInfo());
    }

    public override void DataLoaded(IAsyncResult result)
    {
        Task<List<IncompleteAttributeInfo>> oTask = (Task<List<IncompleteAttributeInfo>>)result;
        if (oTask.IsCompleted)
        {
            ConfigureChartSettings(oTask.Result);
        }
    }
    void ConfigureChartSettings(List<IncompleteAttributeInfo> oIncompleteAttributeInfoCollection)
    {
        try
        {
            int? totalRecordCountToDisplay = 0;

            int userID = Convert.ToInt32(SessionHelper.CurrentUserID);
            short roleID = Convert.ToInt16(SessionHelper.CurrentRoleID);
            int recPeriodID = Convert.ToInt32(SessionHelper.CurrentReconciliationPeriodID);

            if (oIncompleteAttributeInfoCollection != null && oIncompleteAttributeInfoCollection.Count > 0)
                totalRecordCountToDisplay = oIncompleteAttributeInfoCollection[0].TotalRecordCountToDisplay;

            int reconciliationTemplate = 0;
            decimal incompleteReconciliationTemplatePercentage = 100;
            int keyAccount = 0;
            decimal incompleteKeyAccountPercentage = 100;
            int zeroBalanceAccount = 0;
            decimal incompleteZeroBalanceAccountPercentage = 100;
            int riskRating = 0;
            decimal incompleteRiskRatingPercentage = 100;
            int frequency = 0;
            decimal incompleteFrequencyPercentage = 100;
            int preparerDueDays = 0;
            decimal incompletePreparerDueDaysPercentage = 100;
            int reviewerDueDays = 0;
            decimal incompleteReviewerDueDaysPercentage = 100;
            int approverDueDays = 0;
            decimal incompleteApproverDueDaysPercentage = 100;
            int totalAccountCount = 0;
            //bool isRiskRating = false;
            bool isRiskRating = Helper.IsCapabilityActivatedForCurrentRecPeriod(ARTEnums.Capability.RiskRating, false);
            bool isKeyAccount = Helper.IsCapabilityActivatedForCurrentRecPeriod(ARTEnums.Capability.KeyAccount, false);
            bool isZeroBalanceAccount = Helper.IsCapabilityActivatedForCurrentRecPeriod(ARTEnums.Capability.ZeroBalanceAccount, false);
            bool isDueDateByAccount = Helper.IsCapabilityActivatedForCurrentRecPeriod(ARTEnums.Capability.DueDateByAccount, false);
            bool isDualLevelReview = Helper.IsCapabilityActivatedForCurrentRecPeriod(ARTEnums.Capability.DualLevelReview, false);


            if (oIncompleteAttributeInfoCollection != null)
            {
                foreach (IncompleteAttributeInfo oIncompleteAttributeInfo in oIncompleteAttributeInfoCollection)
                {
                    //*******Check for the ReconciliationTemplate
                    if (oIncompleteAttributeInfo.AccountAttributeID == System.Convert.ToInt32(ARTEnums.AccountAttribute.ReconciliationTemplate))
                    {
                        reconciliationTemplate = System.Convert.ToInt32(oIncompleteAttributeInfo.CompletedAttributeAccountCount);
                        totalAccountCount = System.Convert.ToInt32(oIncompleteAttributeInfo.TotalAccounts);
                        incompleteReconciliationTemplatePercentage = GetIncompletePercentageValue(reconciliationTemplate, totalAccountCount);
                    }
                    //*******Check for the Key Account**********************
                    if (oIncompleteAttributeInfo.AccountAttributeID == System.Convert.ToInt32(ARTEnums.AccountAttribute.IsKeyAccount))
                    {
                        keyAccount = System.Convert.ToInt32(oIncompleteAttributeInfo.CompletedAttributeAccountCount);
                        totalAccountCount = System.Convert.ToInt32(oIncompleteAttributeInfo.TotalAccounts);
                        incompleteKeyAccountPercentage = GetIncompletePercentageValue(keyAccount, totalAccountCount);
                    }

                    //*******Check for the Zero Balance Account**********************
                    if (oIncompleteAttributeInfo.AccountAttributeID == System.Convert.ToInt32(ARTEnums.AccountAttribute.IsZeroBalanceAccount))
                    {
                        zeroBalanceAccount = System.Convert.ToInt32(oIncompleteAttributeInfo.CompletedAttributeAccountCount);
                        totalAccountCount = System.Convert.ToInt32(oIncompleteAttributeInfo.TotalAccounts);
                        incompleteZeroBalanceAccountPercentage = GetIncompletePercentageValue(zeroBalanceAccount, totalAccountCount);
                    }

                    //*******Check for the Risk Rating**********************
                    if (oIncompleteAttributeInfo.AccountAttributeID == System.Convert.ToInt32(ARTEnums.AccountAttribute.RiskRating))
                    {
                        //isRiskRating = true;   //Variable to show whether RiskRating is enabled or not
                        riskRating = System.Convert.ToInt32(oIncompleteAttributeInfo.CompletedAttributeAccountCount);
                        totalAccountCount = System.Convert.ToInt32(oIncompleteAttributeInfo.TotalAccounts);
                        incompleteRiskRatingPercentage = GetIncompletePercentageValue(riskRating, totalAccountCount);
                    }

                    //*******Check for the Frequency**********************
                    if (oIncompleteAttributeInfo.AccountAttributeID == 0)
                    {
                        frequency = System.Convert.ToInt32(oIncompleteAttributeInfo.CompletedAttributeAccountCount);
                        totalAccountCount = System.Convert.ToInt32(oIncompleteAttributeInfo.TotalAccounts);
                        incompleteFrequencyPercentage = GetIncompletePercentageValue(frequency, totalAccountCount);
                    }
                    //*******Check for Preparer Due Days **********************
                    if (oIncompleteAttributeInfo.AccountAttributeID == System.Convert.ToInt32(ARTEnums.AccountAttribute.PreparerDueDays))
                    {
                        preparerDueDays = System.Convert.ToInt32(oIncompleteAttributeInfo.CompletedAttributeAccountCount);
                        totalAccountCount = System.Convert.ToInt32(oIncompleteAttributeInfo.TotalAccounts);
                        incompletePreparerDueDaysPercentage = GetIncompletePercentageValue(preparerDueDays, totalAccountCount);
                    }
                    //*******Check for Reviewer Due Days **********************
                    if (oIncompleteAttributeInfo.AccountAttributeID == System.Convert.ToInt32(ARTEnums.AccountAttribute.ReviewerDueDays))
                    {
                        reviewerDueDays = System.Convert.ToInt32(oIncompleteAttributeInfo.CompletedAttributeAccountCount);
                        totalAccountCount = System.Convert.ToInt32(oIncompleteAttributeInfo.TotalAccounts);
                        incompleteReviewerDueDaysPercentage = GetIncompletePercentageValue(reviewerDueDays, totalAccountCount);
                    }
                    //*******Check for Approver Due Days **********************
                    if (oIncompleteAttributeInfo.AccountAttributeID == System.Convert.ToInt32(ARTEnums.AccountAttribute.ApproverDueDays))
                    {
                        approverDueDays = System.Convert.ToInt32(oIncompleteAttributeInfo.CompletedAttributeAccountCount);
                        totalAccountCount = System.Convert.ToInt32(oIncompleteAttributeInfo.TotalAccounts);
                        incompleteApproverDueDaysPercentage = GetIncompletePercentageValue(approverDueDays, totalAccountCount);
                    }
                }
            }


            //*******Display the total accounts
            lblTotalAccounts.Text = Helper.GetDisplayIntegerValue(totalRecordCountToDisplay);

            //Check condition : If the Total Account Count is Zero, Don't Show the Graph    
            if (totalRecordCountToDisplay == 0M)
            {
                chrtIncompleteAttributeList.Visible = false;
                lblMsg.Visible = false;
                return;
            }

            //Check condition : If All the Data point are Zero,  Don't Show the Graph    
            //Modified :(on 11-Mar-2011 by Prafull) Check condition : If All the Data point are Zero, Show the message but don't hide the graph
            if (incompleteReconciliationTemplatePercentage == 0 && incompleteKeyAccountPercentage == 0
                && incompleteZeroBalanceAccountPercentage == 0 && incompletePreparerDueDaysPercentage == 0
                && incompleteReviewerDueDaysPercentage == 0 && incompleteApproverDueDaysPercentage == 0)
            {
                if (isRiskRating == true && incompleteRiskRatingPercentage == 0)
                {
                    //chrtIncompleteAttributeList.Visible = false;
                    lblMsg.Visible = true;
                    //return;

                }
                else if (isRiskRating == false && incompleteFrequencyPercentage == 0)
                {
                    //chrtIncompleteAttributeList.Visible = false;
                    lblMsg.Visible = true;
                    //return;
                }
                else
                {
                    lblMsg.Visible = false;
                }
            }
            else
            {
                lblMsg.Visible = false;
            }

            //***********Chart Setting******************************************************************************

            ////check whether there is any existing Chart/Series,and remove it if any 
            if (chrtIncompleteAttributeList.ChartAreas.Count > 0)
            {

                ChartArea oChartAreaIncompleteAttributeList = chrtIncompleteAttributeList.ChartAreas["ChAreaIncompleteAttributeList"];
                Series oSeriesIncompleteAttributeList = chrtIncompleteAttributeList.Series["IncompleteAttributeList"];

                chrtIncompleteAttributeList.Series.Remove(oSeriesIncompleteAttributeList);
                chrtIncompleteAttributeList.ChartAreas.Remove(oChartAreaIncompleteAttributeList);
            }


            ChartArea oChartArea = new ChartArea();
            oChartArea.Name = "ChAreaIncompleteAttributeList";
            oChartArea.Area3DStyle.Enable3D = true;
            oChartArea.BackColor = Color.White;


            //Note:  ChartArea.Area3DStyle.PointDepth is used for how much depth we want in the  pie Chart Control
            //oChartArea.Area3DStyle.PointDepth = 100;
            chrtIncompleteAttributeList.ChartAreas.Add(oChartArea);
            chrtIncompleteAttributeList.ChartAreas["ChAreaIncompleteAttributeList"].AxisX.Title = string.Format("{0}", LanguageUtil.GetValue(1554));
            chrtIncompleteAttributeList.ChartAreas["ChAreaIncompleteAttributeList"].AxisY.Title = string.Format("{0}", LanguageUtil.GetValue(1925)) + "  [ % ]";
            chrtIncompleteAttributeList.ChartAreas["ChAreaIncompleteAttributeList"].AxisY.Maximum = 100;


            // Populate series data
            Series oSeries = new Series();
            oSeries.Name = "IncompleteAttributeList";
            oSeries.ChartType = SeriesChartType.Column;
            //oSeries.IsValueShownAsLabel = true;


            DataPoint oDataPointReconciliationForm = new DataPoint();
            oDataPointReconciliationForm.SetValueY(incompleteReconciliationTemplatePercentage);
            oDataPointReconciliationForm.AxisLabel = string.Format("{0}", LanguageUtil.GetValue(1366));
            oDataPointReconciliationForm.Label = string.Format("{0}", incompleteReconciliationTemplatePercentage) + "%\n" + "(" + (totalAccountCount - reconciliationTemplate) + ")";
            oSeries.Points.Add(oDataPointReconciliationForm);


            //*************************************************************************************
            if (isKeyAccount == true)
            {
                DataPoint oDataPointKeyAccount = new DataPoint();
                oDataPointKeyAccount.SetValueY(incompleteKeyAccountPercentage);
                oDataPointKeyAccount.AxisLabel = string.Format("{0}", LanguageUtil.GetValue(1014));
                oDataPointKeyAccount.Label = string.Format("{0}", incompleteKeyAccountPercentage) + "%\n" + "(" + +(totalAccountCount - keyAccount) + ")";
                oSeries.Points.Add(oDataPointKeyAccount);
            }
            //***************************************************************************************
            if (isZeroBalanceAccount == true)
            {
                DataPoint oDataPointZeroBalanceAccount = new DataPoint();
                oDataPointZeroBalanceAccount.SetValueY(incompleteZeroBalanceAccountPercentage);
                oDataPointZeroBalanceAccount.AxisLabel = string.Format("{0}", LanguageUtil.GetValue(1256));
                oDataPointZeroBalanceAccount.Label = string.Format("{0}", incompleteZeroBalanceAccountPercentage) + "%\n" + "(" + (totalAccountCount - zeroBalanceAccount) + ")";
                oSeries.Points.Add(oDataPointZeroBalanceAccount);
            }
            //*************************************************************************************


            if (isRiskRating == true)
            {

                DataPoint oDataPointRiskRating = new DataPoint();
                oDataPointRiskRating.SetValueY(incompleteRiskRatingPercentage);
                oDataPointRiskRating.AxisLabel = string.Format("{0}", LanguageUtil.GetValue(1013));
                oDataPointRiskRating.Label = string.Format("{0}", incompleteRiskRatingPercentage) + "%\n" + "(" + (totalAccountCount - riskRating) + ")";
                oSeries.Points.Add(oDataPointRiskRating);
            }
            else
            {
                DataPoint oDataPointfrequency = new DataPoint();
                oDataPointfrequency.SetValueY(incompleteFrequencyPercentage);
                oDataPointfrequency.AxisLabel = string.Format("{0}", LanguageUtil.GetValue(1427));
                oDataPointfrequency.Label = string.Format("{0}", incompleteFrequencyPercentage) + "%\n" + "(" + (totalAccountCount - frequency) + ")";
                oSeries.Points.Add(oDataPointfrequency);
            }

            if (isDueDateByAccount)
            {
                DataPoint oDataPointPreparerDueDays = new DataPoint();
                oDataPointPreparerDueDays.SetValueY(incompletePreparerDueDaysPercentage);
                oDataPointPreparerDueDays.AxisLabel = string.Format("{0}", LanguageUtil.GetValue(2752));
                oDataPointPreparerDueDays.Label = string.Format("{0}", incompletePreparerDueDaysPercentage) + "%\n" + "(" + (totalAccountCount - preparerDueDays) + ")";
                oSeries.Points.Add(oDataPointPreparerDueDays);

                DataPoint oDataPointReviewerDueDays = new DataPoint();
                oDataPointReviewerDueDays.SetValueY(incompleteReviewerDueDaysPercentage);
                oDataPointReviewerDueDays.AxisLabel = string.Format("{0}", LanguageUtil.GetValue(2753));
                oDataPointReviewerDueDays.Label = string.Format("{0}", incompleteReviewerDueDaysPercentage) + "%\n" + "(" + (totalAccountCount - reviewerDueDays) + ")";
                oSeries.Points.Add(oDataPointReviewerDueDays);

                if (isDualLevelReview && !Helper.IsDualLevelReviewByAccountActivated())
                {
                    DataPoint oDataPointApproverDueDays = new DataPoint();
                    oDataPointApproverDueDays.SetValueY(incompleteApproverDueDaysPercentage);
                    oDataPointApproverDueDays.AxisLabel = string.Format("{0}", LanguageUtil.GetValue(2754));
                    oDataPointApproverDueDays.Label = string.Format("{0}", incompleteApproverDueDaysPercentage) + "%\n" + "(" + (totalAccountCount - approverDueDays) + ")";
                    oSeries.Points.Add(oDataPointApproverDueDays);
                }
            }

            chrtIncompleteAttributeList.Series.Add(oSeries);
            //*******Property to set the Width of the Column. Default is 1;
            chrtIncompleteAttributeList.Series["IncompleteAttributeList"]["PointWidth"] = "0.6";
            //*******Property whether to show Labels outside or inside Column/Bar.(Currently this property is not working) 
            //chrtIncompleteAttributeList.Series["IncompleteAttributeList"]["BarLabelStyle"] = "Center";



            //**********Label font and Color Setting
            for (int i = 0; i < chrtIncompleteAttributeList.Series["IncompleteAttributeList"].Points.Count; i++)
            {
                chrtIncompleteAttributeList.Series["IncompleteAttributeList"].Points[i].Font = new Font("Arial", 8, FontStyle.Regular);
                chrtIncompleteAttributeList.Series["IncompleteAttributeList"].Points[i].Color = ColorTranslator.FromHtml(WebPartHelper.GetStatusColor((short)WebEnums.ReconciliationStatus.InProgress));

                //chrtIncompleteAttributeList.Series["IncompleteAttributeList"].Points[i].Color = ColorTranslator.FromHtml("#ffdea4");

            }

            //this.Page.Controls.Add(Chart1);
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

    decimal GetIncompletePercentageValue(int val, int total)
    {
        decimal percentValue = 0;
        if (total == 0)
        {
            percentValue = 0;
        }
        else
        {
            try
            {
                percentValue = System.Math.Round(Convert.ToDecimal(total - val) * 100 / Convert.ToDecimal(total), 2);
            }
            catch
            {
            }
        }
        return percentValue;
    }





}
