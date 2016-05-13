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
using SkyStem.ART.Web.Utility;
using SkyStem.ART.Client.IServices;
using SkyStem.ART.Web.Data;
using SkyStem.Library.Controls.WebControls;
using SkyStem.ART.Web.Classes;
using SkyStem.Language.LanguageUtility;
using SkyStem.ART.Client.Params;

public partial class Pages_MultiVersionGL_PopupGLVersion : PopupPageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        PopupHelper.SetPageTitle(this, 2183);
        BindGLVersionGrid();
    }
    private void BindGLVersionGrid()
    {
        List<GLDataHdrInfo> oGLDataHdrInfoCollection = null;
        IGLData oGLDataClient = RemotingHelper.GetGLDataObject();
        GLDataParamInfo oGLDataParamInfo = new GLDataParamInfo();
        if (!string.IsNullOrEmpty(Request.QueryString[QueryStringConstants.GLDATA_ID]))
            oGLDataParamInfo.GLDataID = Convert.ToInt64(Request.QueryString[QueryStringConstants.GLDATA_ID]);

        oGLDataHdrInfoCollection = oGLDataClient.GetGLVersionByGLDataID(oGLDataParamInfo, Helper.GetAppUserInfo());
        radGLVersion.DataSource = oGLDataHdrInfoCollection;
        radGLVersion.DataBind();
    }

    protected void radGLVersion_SortCommand(object source, Telerik.Web.UI.GridSortCommandEventArgs e)
    {
        GridHelper.HandleSortCommand(e);
        radGLVersion.Rebind();
    }
    
    protected void radGLVersion_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {
        try
        {
            if (e.Item.ItemType == Telerik.Web.UI.GridItemType.Item || e.Item.ItemType == Telerik.Web.UI.GridItemType.AlternatingItem)
            {
                GLDataHdrInfo oGLDataHdrInfo = (GLDataHdrInfo)e.Item.DataItem;

                ExLabel lblDateAdded = (ExLabel)e.Item.FindControl("lblDateAdded");
                lblDateAdded.Text = Helper.GetDisplayDate(oGLDataHdrInfo.DateAdded);
                
                ExLabel lblAddedBy = (ExLabel)e.Item.FindControl("lblAddedBy");
                lblAddedBy.Text = Helper.GetDisplayStringValue(oGLDataHdrInfo.AddedBy);
                
                ExLabel lblReconciliationStatus = (ExLabel)e.Item.FindControl("lblReconciliationStatus");
                lblReconciliationStatus.Text = Helper.GetDisplayStringValue(oGLDataHdrInfo.ReconciliationStatus);
                
                ExLabel lblGLBalanceBaseCurrency = (ExLabel)e.Item.FindControl("lblGLBalanceBaseCurrency");
                lblGLBalanceBaseCurrency.Text = Helper.GetDisplayCurrencyValue(oGLDataHdrInfo.BaseCurrencyCode, oGLDataHdrInfo.GLBalanceBaseCurrency);
                
                ExLabel lblGLBalanceReportingCurrency = (ExLabel)e.Item.FindControl("lblGLBalanceReportingCurrency");
                lblGLBalanceReportingCurrency.Text = Helper.GetDisplayCurrencyValue(oGLDataHdrInfo.ReportingCurrencyCode, oGLDataHdrInfo.GLBalanceReportingCurrency);
            }
        }
        catch (Exception ex)
        {
            Helper.ShowErrorMessage((PageBase)this.Page, ex);
        }
    }
}
