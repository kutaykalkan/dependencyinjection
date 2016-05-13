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
using SkyStem.ART.Client.IServices;
using System.Collections.Generic;
using SkyStem.ART.Client.Model;
using SkyStem.Library.Controls.WebControls;
using SkyStem.ART.Web.Data;

public partial class Pages_ReconciliationArchives :  PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Helper.SetPageTitle(this, 1527 );

        int? accountID = 0;
        if (Request.QueryString[QueryStringConstants.ACCOUNT_ID] != null)
            accountID = Convert.ToInt32(Request.QueryString[QueryStringConstants.ACCOUNT_ID]);
        ucAccountHierarchyDetail.AccountID = 1001;
        IUtility oUtilityClient = RemotingHelper.GetUtilityObject();
        IList<ReconciliationArchiveInfo> oReconciliationArchiveInfoCollection = new List<ReconciliationArchiveInfo>();
        oReconciliationArchiveInfoCollection = oUtilityClient.GetReconciliationArchiveData(1, Helper.GetAppUserInfo());//TODO: remov ethis hardcoded value
        rgMain.MasterTableView.DataSource = oReconciliationArchiveInfoCollection;
    }

    protected void rgMain_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {
        if (e.Item.ItemType == Telerik.Web.UI.GridItemType.Item ||
            e.Item.ItemType == Telerik.Web.UI.GridItemType.AlternatingItem)
        {
            ReconciliationArchiveInfo oReconciliationArchiveInfo = (ReconciliationArchiveInfo)e.Item.DataItem;

            ExLabel lblReconciliationPeriod = (ExLabel)e.Item.FindControl("lblReconciliationPeriod");
            ExLabel lblGLBalance = (ExLabel)e.Item.FindControl("lblGLBalance");
            ExLabel lblDateCertified = (ExLabel)e.Item.FindControl("lblDateCertified");

            lblReconciliationPeriod.Text = oReconciliationArchiveInfo.PeriodEndDate.ToString();
            lblGLBalance.Text = Helper.GetDisplayDecimalValue( oReconciliationArchiveInfo.GLBalanceReportingCurrency);
            lblDateCertified.Text = oReconciliationArchiveInfo.DateCertified.ToString();
        }
    }

    public override string GetMenuKey()
    {
        return "AccountViewer";
    }

}//end of class
