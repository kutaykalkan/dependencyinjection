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
using SkyStem.ART.Web.Classes;
using System.Collections.Generic;
using SkyStem.Language.LanguageUtility;
using SkyStem.ART.Web.Data;
using SkyStem.ART.Web.Utility;
using SkyStem.ART.Client.IServices;
using SkyStem.ART.Client.Model;
using Telerik.Web.UI;
using SkyStem.Library.Controls.WebControls;
using SkyStem.Library.Controls.TelerikWebControls;
using System.Text;

public partial class Pages_AccountAlertDetail : PopupPageBase
{
    #region Variables & Constants
    bool bIsUnreadMessageAvailable = false;
    #endregion

    #region Properties
    List<CompanyAlertDetailInfo> oCompanyAlertDetailInfoCollection = null;
    #endregion

    #region Page Events
    protected void Page_Load(object sender, EventArgs e)
    {

        PopupHelper.SetPageTitle(this, 1010);
        if (!Page.IsPostBack)
        {
            bIsUnreadMessageAvailable = false;
            BindRadGrid();
        }
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        btnSaveAlert.Enabled = false;
        if (bIsUnreadMessageAvailable)
        {
            btnSaveAlert.Enabled = true;
        }
        else
        {
            if (Page.IsPostBack)
            {
                // Set Argument
                StringBuilder oStringBuilder = new StringBuilder();
                oStringBuilder.Append("var oWindow = GetRadWindow(); oWindow.argument = 1;");
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "SetWindowArgumentForParentReload", oStringBuilder.ToString(), true);
            }
        }
    }
    #endregion

    #region Grid Events
    protected void rgAlerts_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {
        if (e.Item.ItemType == Telerik.Web.UI.GridItemType.Item ||
            e.Item.ItemType == Telerik.Web.UI.GridItemType.AlternatingItem)
        {
            CompanyAlertDetailInfo oCompanyAlertDetailInfo = (CompanyAlertDetailInfo)e.Item.DataItem;

            ExLabel lblAlertDescription = (ExLabel)e.Item.FindControl("lblAlertDescription");
            lblAlertDescription.Text = oCompanyAlertDetailInfo.AlertDescription;
            ExLabel lblAlertDateAdded = (ExLabel)e.Item.FindControl("lblAlertDateAdded");
            lblAlertDateAdded.Text = oCompanyAlertDetailInfo.DateAdded.Value.ToString();
            ExImage imgView = (ExImage)e.Item.FindControl("imgViewReadOnlyMode");
            GridDataItem item = (GridDataItem)e.Item;
            CheckBox checkBox = (CheckBox)item["CheckboxSelectColumn"].Controls[0];
            if (!oCompanyAlertDetailInfo.IsRead.Value)
            {
                lblAlertDescription.CssClass = "Black9ArialUNRead";
                checkBox.Enabled = true;
                bIsUnreadMessageAvailable = true;
            }
            else
            {
                checkBox.Enabled = false;
            }
        }
    }
    protected void rgAlerts_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    {
        if (oCompanyAlertDetailInfoCollection == null)
            oCompanyAlertDetailInfoCollection = (List<CompanyAlertDetailInfo>)ViewState["oCompanyAlertDetailInfoCollection"];
        if (chkShowAlertMsg.Checked)
            rgAlerts.MasterTableView.DataSource = oCompanyAlertDetailInfoCollection;
        else
            rgAlerts.MasterTableView.DataSource = GetReadMsg(oCompanyAlertDetailInfoCollection);

        GridHelper.SortDataSource(rgAlerts.MasterTableView);
    }

    protected void rgAlerts_SortCommand(object source, GridSortCommandEventArgs e)
    {
        GridHelper.HandleSortCommand(e);
        rgAlerts.Rebind();

    }
    #endregion

    #region Other Events
    protected void btnSaveAlert_Click(object sender, EventArgs e)
    {
        List<Int64> oCompanyAlertDetailUserIDCollection = new List<long>();
        foreach (GridDataItem item in rgAlerts.SelectedItems)
        {
            long CompanyAlertDetailUserID = Convert.ToInt64(item.GetDataKeyValue("CompanyAlertDetailUserID"));
            oCompanyAlertDetailUserIDCollection.Add(CompanyAlertDetailUserID);
        }
        IAlert oAlert = RemotingHelper.GetAlertObject();
        bool result = oAlert.UpdateIsRead(oCompanyAlertDetailUserIDCollection, Helper.GetAppUserInfo());

        BindRadGrid();
        rgAlerts.Rebind();
    }
    protected void chkShowAlertMsg_CheckedChanged(object sender, EventArgs e)
    {
        rgAlerts.MasterTableView.DataSource = null;
        if (oCompanyAlertDetailInfoCollection == null)
            oCompanyAlertDetailInfoCollection = (List<CompanyAlertDetailInfo>)ViewState["oCompanyAlertDetailInfoCollection"];
        if (chkShowAlertMsg.Checked)
            rgAlerts.MasterTableView.DataSource = oCompanyAlertDetailInfoCollection;
        else
            rgAlerts.MasterTableView.DataSource = GetReadMsg(oCompanyAlertDetailInfoCollection);
        rgAlerts.Rebind();

    }
    #endregion

    #region Validation Control Events
    #endregion

    #region Private Methods
    private void BindRadGrid()
    {

        IAlert oAlert = RemotingHelper.GetAlertObject();
        int? userID = SessionHelper.CurrentUserID;
        int? roleID = SessionHelper.CurrentRoleID;
        int? recID = SessionHelper.CurrentReconciliationPeriodID;
        int? alertType = (int)WebEnums.AlertType.ACCOUNT_TYPE;
        oCompanyAlertDetailInfoCollection = oAlert.GetCompanyAlertDetailByRoleId(userID, roleID, recID, alertType, Helper.GetAppUserInfo());
        ViewState["oCompanyAlertDetailInfoCollection"] = oCompanyAlertDetailInfoCollection;

        if (chkShowAlertMsg.Checked)
            rgAlerts.MasterTableView.DataSource = oCompanyAlertDetailInfoCollection;
        else
            rgAlerts.MasterTableView.DataSource = GetReadMsg(oCompanyAlertDetailInfoCollection);

        //rgAlerts.MasterTableView.DataSource = GetReadMsg(oCompanyAlertDetailInfoCollection);

        GridHelper.SetSortExpression(rgAlerts.MasterTableView, "DateAdded", GridSortOrder.Descending);

        //GridSortExpression oGridSortExpression = new GridSortExpression();
        //oGridSortExpression.FieldName = "DateAdded";
        //oGridSortExpression.SortOrder = GridSortOrder.Descending;
        //rgAlerts.MasterTableView.SortExpressions.AddSortExpression(oGridSortExpression);

    }

    private List<CompanyAlertDetailInfo> GetReadMsg(List<CompanyAlertDetailInfo> oCompanyAlertDetailInfoCollection)
    {
        List<CompanyAlertDetailInfo> oCompanyAlertDetailInfoRead = new List<CompanyAlertDetailInfo>();
        for (int i = 0; i < oCompanyAlertDetailInfoCollection.Count; i++)
        {
            CompanyAlertDetailInfo oCompanyAlertDetailInfo = oCompanyAlertDetailInfoCollection[i];
            if (!oCompanyAlertDetailInfo.IsRead.Value)
                oCompanyAlertDetailInfoRead.Add(oCompanyAlertDetailInfo);
        }
        return oCompanyAlertDetailInfoRead;

    }

    #endregion

    #region Other Methods
    #endregion














}
