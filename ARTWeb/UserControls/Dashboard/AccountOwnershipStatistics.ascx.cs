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
using System.Collections.Generic;
using SkyStem.Library.Controls.WebControls;
using SkyStem.ART.Web.Utility;
using SkyStem.ART.Web.Data;
using Telerik.Web.UI;
using SkyStem.ART.Client.IServices;
using SkyStem.ART.Web.Classes;
using SkyStem.Library.Controls.TelerikWebControls.Data;
using SkyStem.Library.Controls.TelerikWebControls;
using SkyStem.Language.LanguageUtility;
using SkyStem.ART.Client.Exception;
using System.Threading.Tasks;
using SkyStem.ART.Client.Data;

public partial class UserControls_Dashboard_AccountOwnershipStatistics : UserControlWebPartBase
{
    bool isExportPDF;
    bool isExportExcel;
    protected void Page_Init(object sender, EventArgs e)
    {
        this.LoadUserNameColumnHeaderConditionally();

        Page oPage = (Page)this.Parent.Page;
        MasterPageBase ompage = (MasterPageBase)oPage.Master;
        ompage.ReconciliationPeriodChangedEventHandler += new EventHandler(ompage_ReconciliationPeriodChangedEventHandler);
    }

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!this.IsPostBack)
        {
            isExportPDF = false;
            isExportExcel = false;
            this.LoadData = true;
        }
    }


    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (this.LoadData.Value && this.Visible)
        {
            //LoadGridData();
        }
    }

    protected void ompage_ReconciliationPeriodChangedEventHandler(object sender, EventArgs e)
    {
        LoadGridData();
        rgAccountOwnership.MasterTableView.DataBind();
    }

    private List<AccountOwnershipStatisticsInfo> LoadPreparerData(int? SelectedUserID, int? SelectedUserIDSecondLevel)
    {
        List<AccountOwnershipStatisticsInfo> oAccountOwnershipStatisticsInfoCollection = new List<AccountOwnershipStatisticsInfo>();

        int userID = Convert.ToInt32(SessionHelper.CurrentUserID);
        short roleID = Convert.ToInt16(SessionHelper.CurrentRoleID);
        int recPeriodID = Convert.ToInt32(SessionHelper.CurrentReconciliationPeriodID);

        int? totalAccount = DashboardHelper.GetTotalAccountsCount(userID, roleID, recPeriodID, Helper.GetAppUserInfo());

        oAccountOwnershipStatisticsInfoCollection = DashboardHelper.GetDataForAccountOwnershipStatisticsThirdLevel(userID, roleID, recPeriodID, SelectedUserID, SelectedUserIDSecondLevel, Helper.GetAppUserInfo());
        if (oAccountOwnershipStatisticsInfoCollection != null)
        {
            foreach (AccountOwnershipStatisticsInfo oAccountOwnershipStatisticsInfo in oAccountOwnershipStatisticsInfoCollection)
            {
                oAccountOwnershipStatisticsInfo.Association = Helper.GetDisplayPercentageValue(((oAccountOwnershipStatisticsInfo.NoOfAccounts * 1m / totalAccount.Value) * 100));
            }
        }

        return oAccountOwnershipStatisticsInfoCollection;
    }

    private List<AccountOwnershipStatisticsInfo> LoadReviewerData(int? SelectedUserID)
    {
        List<AccountOwnershipStatisticsInfo> oAccountOwnershipStatisticsInfoCollection = new List<AccountOwnershipStatisticsInfo>();

        int userID = Convert.ToInt32(SessionHelper.CurrentUserID);
        short roleID = Convert.ToInt16(SessionHelper.CurrentRoleID);
        int recPeriodID = Convert.ToInt32(SessionHelper.CurrentReconciliationPeriodID);

        int? totalAccount = DashboardHelper.GetTotalAccountsCount(userID, roleID, recPeriodID, Helper.GetAppUserInfo());

        oAccountOwnershipStatisticsInfoCollection = DashboardHelper.GetDataForAccountOwnershipStatisticsSecondLevel(userID, roleID, recPeriodID, SelectedUserID, Helper.GetAppUserInfo());
        if (oAccountOwnershipStatisticsInfoCollection != null)
        {
            foreach (AccountOwnershipStatisticsInfo oAccountOwnershipStatisticsInfo in oAccountOwnershipStatisticsInfoCollection)
            {
                oAccountOwnershipStatisticsInfo.Association = Helper.GetDisplayPercentageValue(((oAccountOwnershipStatisticsInfo.NoOfAccounts * 1m / totalAccount.Value) * 100));
                oAccountOwnershipStatisticsInfo.ApproverUserID = SelectedUserID;

            }
        }

        return oAccountOwnershipStatisticsInfoCollection;
    }

    protected void rgAccountOwnership_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {

        if (e.Item.ItemType == Telerik.Web.UI.GridItemType.Item || e.Item.ItemType == Telerik.Web.UI.GridItemType.AlternatingItem)
        {

            switch (e.Item.OwnerTableView.Name)
            {
                case "Approver":
                    break;

                case "Reviewer":
                    WebEnums.FeatureCapabilityMode eMode = Helper.GetFeatureCapabilityMode(WebEnums.Feature.DualLevelReview, ARTEnums.Capability.DualLevelReview, SessionHelper.CurrentReconciliationPeriodID);
                    if(eMode == WebEnums.FeatureCapabilityMode.Hidden || eMode == WebEnums.FeatureCapabilityMode.Disable)
                    //if (Helper.IsCapabilityActivatedForCurrentRecPeriod(ARTEnums.Capability.DualLevelReview) == false)
                    {

                        GridDataItem oItem = (GridDataItem)e.Item;
                        oItem["ExpandColumn"].Controls[0].Visible = false;
                    }

                    break;

                case "Preparer":
                    break;

                default:

                    break;
            }

            ExLabel lblNoOfAccounts = (ExLabel)e.Item.FindControl("lblNoOfAccounts");
            AccountOwnershipStatisticsInfo oAccountOwnershipStatisticsInfo = (AccountOwnershipStatisticsInfo)e.Item.DataItem;
            lblNoOfAccounts.Text = Helper.GetDisplayIntegerValue(oAccountOwnershipStatisticsInfo.NoOfAccounts);
        }
    }

    protected void rgAccountOwnership_DetailTableDataBind(object source, Telerik.Web.UI.GridDetailTableDataBindEventArgs e)
    {
        GridDataItem parentItem = e.DetailTableView.ParentItem;
        bool isDualReviewEnabled = Helper.IsCapabilityActivatedForCurrentRecPeriod(ARTEnums.Capability.DualLevelReview);
        int? parentUserID = (int?)parentItem.GetDataKeyValue("UserID");
        GridDataItem dataItem = (GridDataItem)e.DetailTableView.ParentItem;
        rgAccountOwnership.MasterTableView.DetailTables[0].ExpandCollapseColumn.Display = true;
        rgAccountOwnership.MasterTableView.DetailTables[0].ExpandCollapseColumn.ExpandImageUrl = "~/App_Themes/SkyStemBlueBrown/Images/ExpandRow.gif";
        rgAccountOwnership.MasterTableView.DetailTables[0].ExpandCollapseColumn.CollapseImageUrl = "~/App_Themes/SkyStemBlueBrown/Images/CollapseRow.gif";
        if (isDualReviewEnabled == false && e.DetailTableView.Name == "Preparer")
        {
            e.DetailTableView.Visible = false;
            e.DetailTableView.Parent.Visible = false;
        }
        switch (e.DetailTableView.Name)
        {
            case "Reviewer":    //Preparer in case Dual Review is Off 
                e.DetailTableView.ExpandCollapseColumn.Display = true;
                e.DetailTableView.ExpandCollapseColumn.ExpandImageUrl = "~/App_Themes/SkyStemBlueBrown/Images/ExpandRow.gif";
                e.DetailTableView.ExpandCollapseColumn.CollapseImageUrl = "~/App_Themes/SkyStemBlueBrown/Images/CollapseRow.gif";
                e.DetailTableView.DataSource = LoadReviewerData(parentUserID);
                break;

            case "Preparer":
                int? superParentID = (int?)parentItem.OwnerTableView.ParentItem.GetDataKeyValue("UserID"); //parentItem.Cells[5].Text;
                e.DetailTableView.DataSource = LoadPreparerData(superParentID, parentUserID);
                break;
        }
    }

    protected void rgAccountOwnership_ItemCreated(object sender, GridItemEventArgs e)
    {
        // Register PDF / Excel for Postback
        GridHelper.RegisterPDFAndExcelForPostback(e, isExportPDF, isExportExcel, this.Page);

        GridHelper.SetStylesForExportGrid(e, isExportPDF, isExportExcel);
    }



    protected void rgAccountOwnership_OnItemCommand(object sender, GridCommandEventArgs e)
    {
        if (e.CommandName == TelerikConstants.GridExportToPDFCommandName)
        {
            isExportPDF = true;
            LoadGridData();
            GridHelper.ExportGridToPDF(rgAccountOwnership, 1032);
        }
        if (e.CommandName == TelerikConstants.GridExportToExcelCommandName)
        {
            isExportExcel = true;
            LoadGridData();
            GridHelper.ExportGridToExcel(rgAccountOwnership, 1032);
        }

    }

    private void LoadUserNameColumnHeaderConditionally()
    {
        try
        {

            ExGridBoundColumn oExGridBoundColumnApproverName = (ExGridBoundColumn)this.rgAccountOwnership.MasterTableView.Columns[0];

            WebEnums.FeatureCapabilityMode eMode = Helper.GetFeatureCapabilityMode(WebEnums.Feature.DualLevelReview, ARTEnums.Capability.DualLevelReview, SessionHelper.CurrentReconciliationPeriodID);

            if (eMode == WebEnums.FeatureCapabilityMode.Visible)
            //{ }
            //  if (Helper.IsCapabilityActivatedForCurrentRecPeriod(ARTEnums.Capability.DualLevelReview) == true)
            {
                oExGridBoundColumnApproverName.LabelID = 1132;
            }
            else
            {
                oExGridBoundColumnApproverName.LabelID = 1131;

            }

            ExGridBoundColumn oExGridBoundColumnReviewerName = null;
            if (this.rgAccountOwnership.MasterTableView.DetailTables[0] != null)
            {
                oExGridBoundColumnReviewerName = (ExGridBoundColumn)this.rgAccountOwnership.MasterTableView.DetailTables[0].GetColumn("column1");
                if (eMode == WebEnums.FeatureCapabilityMode.Visible)
                //if (Helper.IsCapabilityActivatedForCurrentRecPeriod(ARTEnums.Capability.DualLevelReview) == true)
                {
                    oExGridBoundColumnReviewerName.LabelID = 1131;
                }
                else
                {
                    oExGridBoundColumnReviewerName.LabelID = 1130;
                }
            }

            ExGridBoundColumn oExGridBoundColumnPreparerName = null;
            if (this.rgAccountOwnership.MasterTableView.DetailTables[0].DetailTables[0] != null)
            {
                oExGridBoundColumnPreparerName = (ExGridBoundColumn)this.rgAccountOwnership.MasterTableView.DetailTables[0].DetailTables[0].Columns[0];
                oExGridBoundColumnPreparerName.LabelID = 1130;
            }

        }
        catch (ARTException)
        {

        }
        catch (Exception)
        {

        }
    }

    public override IAsyncResult GetDataAsync()
    {
        int userID = Convert.ToInt32(SessionHelper.CurrentUserID);
        short roleID = Convert.ToInt16(SessionHelper.CurrentRoleID);
        int recPeriodID = Convert.ToInt32(SessionHelper.CurrentReconciliationPeriodID);
        return DashboardHelper.GetDataForAccountOwnershipStatisticsAsync(userID, roleID, recPeriodID, Helper.GetAppUserInfo());
    }

    public override void DataLoaded(IAsyncResult result)
    {
        Task<List<AccountOwnershipStatisticsInfo>> oTask = (Task<List<AccountOwnershipStatisticsInfo>>)result;
        if (oTask.IsCompleted)
        {
            LoadGridData(oTask.Result);
            rgAccountOwnership.Rebind();
        }
    }

    private void LoadGridData()
    {
        int userID = Convert.ToInt32(SessionHelper.CurrentUserID);
        short roleID = Convert.ToInt16(SessionHelper.CurrentRoleID);
        int recPeriodID = Convert.ToInt32(SessionHelper.CurrentReconciliationPeriodID);
        List<AccountOwnershipStatisticsInfo> oAccountOwnershipStatisticsInfoCollection = DashboardHelper.GetDataForAccountOwnershipStatistics(userID, roleID, recPeriodID, Helper.GetAppUserInfo());
        LoadGridData(oAccountOwnershipStatisticsInfoCollection);
    }
    private void LoadGridData(List<AccountOwnershipStatisticsInfo> oAccountOwnershipStatisticsInfoCollection)
    {
        try
        {
            int userID = Convert.ToInt32(SessionHelper.CurrentUserID);
            short roleID = Convert.ToInt16(SessionHelper.CurrentRoleID);
            int recPeriodID = Convert.ToInt32(SessionHelper.CurrentReconciliationPeriodID);
            IUser oUserClient = RemotingHelper.GetUserObject();

            int? totalAccount = oUserClient.GetTotalAccountsCount(userID, roleID, recPeriodID, Helper.GetAppUserInfo());

            lblTotalAccounts.Text = Helper.GetDisplayIntegerValue(totalAccount);
            if (oAccountOwnershipStatisticsInfoCollection != null)
            {
                foreach (AccountOwnershipStatisticsInfo oAccountOwnershipStatisticsInfo in oAccountOwnershipStatisticsInfoCollection)
                {
                    oAccountOwnershipStatisticsInfo.Association = Helper.GetDisplayPercentageValue(((oAccountOwnershipStatisticsInfo.NoOfAccounts * 1m / totalAccount.Value) * 100));
                    if (string.IsNullOrEmpty(oAccountOwnershipStatisticsInfo.FirstName)
                        && Helper.IsDualLevelReviewByAccountActivated())
                        oAccountOwnershipStatisticsInfo.FirstName = LanguageUtil.GetValue(2768);
                }
            }

            rgAccountOwnership.MasterTableView.DataSource = oAccountOwnershipStatisticsInfoCollection;
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
