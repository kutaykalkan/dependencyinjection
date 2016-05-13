using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SkyStem.ART.Web.Data;
using SkyStem.ART.Web.Classes;
using SkyStem.Language.LanguageUtility;
using SkyStem.ART.Client.IServices;
using SkyStem.ART.Client.Model;
using SkyStem.ART.Web.Utility;
using SkyStem.Library.Controls.WebControls;

public partial class MasterPages_RecFormPrint : RecPeriodMasterPageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
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
        ExImage oimgCollapse = (ExImage)ucAccountDescription.FindControl("imgCollapse");
        if (oimgCollapse != null)
        {
            oimgCollapse.Visible = false;
            
        }
	System.Web.UI.HtmlControls.HtmlTableCell oHtmlTableCell = (System.Web.UI.HtmlControls.HtmlTableCell)ucRecStatusBar.FindControl("tdRecStatus");
        if (oHtmlTableCell != null)
            oHtmlTableCell.Width = "30%";
    }

    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        ucAccountInfo.GLDataID = this.GLDataID.HasValue ? this.GLDataID.Value : 0;
        ucAccountInfo.AccountID = this.AccountID.HasValue ? this.AccountID.Value : 0;
        ucAccountInfo.NetAccountID = this.NetAccountID.HasValue ? this.NetAccountID.Value : 0;
        ucAccountInfo.IsPrintMode = true;

        RecHelper.ShowRecStatusBar(this, ucRecStatusBar);
    }

}
