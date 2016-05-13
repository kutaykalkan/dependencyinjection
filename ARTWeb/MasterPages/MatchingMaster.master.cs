using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SkyStem.ART.Web.Classes;
using SkyStem.ART.Web.Data;

public partial class MasterPages_MatchingMaster : MatchingMasterPageBase
{

    /// <summary>
    /// Enable/Disable Page Contents
    /// </summary>
    /// <param name="bEnable"></param>
    public void EnableDisablePage(bool bEnable)
    {
        pnlMatchingMaster.Enabled = bEnable;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request.QueryString[QueryStringConstants.ACCOUNT_ID]))
            this.AccountID = Convert.ToInt32(Request.QueryString[QueryStringConstants.ACCOUNT_ID]);

        if (!string.IsNullOrEmpty(Request.QueryString[QueryStringConstants.NETACCOUNT_ID]))
            this.NetAccountID = Convert.ToInt32(Request.QueryString[QueryStringConstants.NETACCOUNT_ID]);

        if (this.AccountID != null || this.NetAccountID != null)
        {
            ucAccountDescription.AccountID = this.AccountID;
            ucAccountDescription.NetAccountID = this.NetAccountID;
            trAccountDescription.Visible = true;
        }
        else
            trAccountDescription.Visible = false;
    }
}
