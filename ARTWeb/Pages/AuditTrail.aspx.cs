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
using SkyStem.ART.Client.Model;
using System.Collections.Generic;
using SkyStem.Library.Controls.WebControls;
using SkyStem.ART.Web.Data;
using Telerik.Web.UI;
using SkyStem.ART.Client.Exception; 
using SkyStem.Library.Controls.TelerikWebControls.Data;

public partial class Pages_AuditTrail : PageBaseCompany
{
    #region Variables & Constants
    #endregion

    #region Properties
    #endregion

    #region Delegates & Events
    #endregion

    #region Page Events
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        Helper.SetPageTitle((PageBase)this.Page, 1380);
        ucAuditTrail.RegisterRecDropDownEvent = true;
        if (Request.QueryString[QueryStringConstants.ACCOUNT_ID] != null)
            ucAuditTrail.AccountID = Convert.ToInt64(Request.QueryString[QueryStringConstants.ACCOUNT_ID]);

        if (Request.QueryString[QueryStringConstants.NETACCOUNT_ID] != null)
            ucAuditTrail.NetAccountID = Convert.ToInt32(Request.QueryString[QueryStringConstants.NETACCOUNT_ID]);

        if (Request.QueryString[QueryStringConstants.GLDATA_ID] != null)
            ucAuditTrail.GLDataID = Convert.ToInt64(Request.QueryString[QueryStringConstants.GLDATA_ID]);

        if (Request.QueryString[QueryStringConstants.REC_CATEGORY_TYPE_ID] != null)
            ucAuditTrail.RecCategoryTypeID = Convert.ToInt16(Request.QueryString[QueryStringConstants.REC_CATEGORY_TYPE_ID]);

        if (Request.QueryString[QueryStringConstants.REC_STATUS_ID] != null)
        {
            ucAuditTrail.RecStatusID = Convert.ToInt16(Request.QueryString[QueryStringConstants.REC_STATUS_ID]);
            ucAuditTrail.ERecStatus = (WebEnums.ReconciliationStatus)System.Enum.Parse(typeof(WebEnums.ReconciliationStatus), ucAuditTrail.RecStatusID.ToString());
        }

    }

    #endregion

    #region Grid Events
    #endregion

    #region Other Events
    #endregion

    #region Validation Control Events
    #endregion

    #region Private Methods
    #endregion

    #region Other Methods
    public override string GetMenuKey()
    {
        return "AccountViewer";
    }

    #endregion
}