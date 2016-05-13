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
using SkyStem.ART.Web.Utility;
using SkyStem.ART.Client.Model;
using SkyStem.ART.Web.Data;
using System.Collections.Generic;
using SkyStem.ART.Client.IServices;
using SkyStem.ART.Client.Data;
using SkyStem.Language.LanguageUtility;
using Telerik.Web.UI;
using SkyStem.Library.Controls.WebControls;
using SkyStem.Library.Controls.TelerikWebControls.Data;

public partial class Pages_RecPeriodStatusHistoryPopup : PopupPageBase
{
    #region Variables & Constants
    bool isExportPDF;
    bool isExportExcel;
    private List<RecPeriodStatusDetailInfo> oRecPeriodStatusDetailInfoList
    {
        get
        {
            return (List<RecPeriodStatusDetailInfo>)ViewState["oRecPeriodStatusDetailInfoList"];
        }
        set { ViewState["oRecPeriodStatusDetailInfoList"] = value; }
    }
    #endregion

    #region Properties
    #endregion

    #region Delegates & Events
    #endregion

    #region Page Events
    protected void Page_Load(object sender, EventArgs e)
    {
        PopupHelper.SetPageTitle(this, 2987);
        if (!Page.IsPostBack)
        {
            LoadData();
        }
    }
    #endregion

    #region Grid Events
    protected void rgAuditTrail_ItemCreated(object sender, GridItemEventArgs e)
    {
        GridHelper.SetStylesForExportGrid(e, isExportPDF, isExportExcel);
    }
    protected void rgAuditTrail_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {
        if (e.Item.ItemType == Telerik.Web.UI.GridItemType.Item ||
            e.Item.ItemType == Telerik.Web.UI.GridItemType.AlternatingItem)
        {
            RecPeriodStatusDetailInfo oRecPeriodStatusDetailInfo = (RecPeriodStatusDetailInfo)e.Item.DataItem;
            ExLabel lblDate = (ExLabel)e.Item.FindControl("lblDate");
            ExLabel lblUser = (ExLabel)e.Item.FindControl("lblUser");
            ExLabel lblStatus = (ExLabel)e.Item.FindControl("lblStatus");
            lblDate.Text = Helper.GetDisplayDateTime(oRecPeriodStatusDetailInfo.StatusDate);
            lblUser.Text = Helper.GetDisplayStringValue(oRecPeriodStatusDetailInfo.FullName);
            lblStatus.Text = Helper.GetDisplayStringValue(oRecPeriodStatusDetailInfo.ReconciliationPeriodStatus);
        }
    }
    protected void rgAuditTrail_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    {
        if (oRecPeriodStatusDetailInfoList == null)
            GetData();
        rgAuditTrail.MasterTableView.DataSource = oRecPeriodStatusDetailInfoList;
    }   
    protected void rgAuditTrail_OnItemCommand(object sender, GridCommandEventArgs e)
    {
        if (e.CommandName == TelerikConstants.GridExportToPDFCommandName)
        {
            isExportPDF = true;
            //rgAuditTrail.MasterTableView.Columns.FindByUniqueName("IconColumn").Visible = false;
            GridHelper.ExportGridToPDF(rgAuditTrail, 1380);           
        }
        if (e.CommandName == TelerikConstants.GridExportToExcelCommandName)
        {
            isExportExcel = true;
            //rgCompanyList.MasterTableView.Columns.FindByUniqueName("IconColumn").Visible = false;
            GridHelper.ExportGridToExcel(rgAuditTrail, 1380);
        }
    }
    #endregion

    #region Other Events
    
    #endregion

    #region Validation Control Events
   
    #endregion

    #region Private Methods
    private void GetData()
    {
        int? RecPeriodID = null;
        if (Request.QueryString[QueryStringConstants.REC_PERIOD_ID] != null)
            RecPeriodID = Convert.ToInt32( Request.QueryString[QueryStringConstants.REC_PERIOD_ID]);
        oRecPeriodStatusDetailInfoList = Helper.GetRecPeriodStatusDetail(RecPeriodID);
    }

    private void LoadData()
    {
        GetData();
        if (oRecPeriodStatusDetailInfoList != null)
        {
            rgAuditTrail.DataSource = oRecPeriodStatusDetailInfoList;
            rgAuditTrail.DataBind();
        }
    }
  
    #endregion

    #region Other Methods
    #endregion

}
