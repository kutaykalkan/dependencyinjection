using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SkyStem.ART.Web.Data;
using SkyStem.ART.Web.Classes;
using SkyStem.ART.Web.Utility;

public partial class Pages_RecFormPrint_TemplateAuditTrailPrint : PageBaseCompany
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString[QueryStringConstants.ACCOUNT_ID] != null)
        {
            ucAuditTrail.AccountID = Convert.ToInt64(Request.QueryString[QueryStringConstants.ACCOUNT_ID]);
        }
        if (Request.QueryString[QueryStringConstants.NETACCOUNT_ID] != null)
        {
            ucAuditTrail.NetAccountID = Convert.ToInt32(Request.QueryString[QueryStringConstants.NETACCOUNT_ID]);
        }
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

    public override string GetMenuKey()
    {
        return "AccountViewer";
    }

}
