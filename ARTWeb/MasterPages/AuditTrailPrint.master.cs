using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SkyStem.ART.Web.Data;
using SkyStem.ART.Web.Classes;
using SkyStem.ART.Web.Utility;

public partial class MasterPages_AuditTrailPrint : RecPeriodMasterPageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.RecStatusBar = WebEnums.RecStatusBarPageType.OtherPages;
        if (!string.IsNullOrEmpty(Request.QueryString[QueryStringConstants.ACCOUNT_ID]))
            this.AccountID = Convert.ToInt32(Request.QueryString[QueryStringConstants.ACCOUNT_ID]);

        if (!string.IsNullOrEmpty(Request.QueryString[QueryStringConstants.NETACCOUNT_ID]))
            this.NetAccountID = Convert.ToInt32(Request.QueryString[QueryStringConstants.NETACCOUNT_ID]);

        if (!string.IsNullOrEmpty(Request.QueryString[QueryStringConstants.PAGE_TITLE_ID]))
            this.PageTitleLabeID = Convert.ToInt32(Request.QueryString[QueryStringConstants.PAGE_TITLE_ID]);

        if (this.PageTitleLabeID != null)
        {
            lblPageTitle.LabelID = this.PageTitleLabeID.Value;
        }

        ucAccountDescription.AccountID = this.AccountID;
        ucAccountDescription.NetAccountID = this.NetAccountID;
    }

    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        RecHelper.ShowRecStatusBar(this, ucRecStatusBar);
        if (this.GLDataID != null && this.GLDataID.HasValue)
            ucRecStatusBar.GLDataID = this.GLDataID;
    }

}
