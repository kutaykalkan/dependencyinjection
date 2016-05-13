using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using SkyStem.ART.Web.Classes;
using SkyStem.ART.Web.Data;
using SkyStem.ART.Client.IServices;
using SkyStem.ART.Client.Model;
using SkyStem.ART.Web.Utility;
using SkyStem.Language.LanguageUtility;
using ExpertPdf.HtmlToPdf;
using SkyStem.Library.Controls.TelerikWebControls;
using System.IO;
using System.Text;

public partial class MasterPages_RecProcessMasterPage : RecPeriodMasterPageBase
{
    #region Variables & Constants
    #endregion

    #region Properties
    #endregion

    #region Delegates & Events
    #endregion

    #region Page Events
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request.QueryString[QueryStringConstants.ACCOUNT_ID]))
            this.AccountID = Convert.ToInt32(Request.QueryString[QueryStringConstants.ACCOUNT_ID]);

        if (!string.IsNullOrEmpty(Request.QueryString[QueryStringConstants.NETACCOUNT_ID]))
            this.NetAccountID = Convert.ToInt32(Request.QueryString[QueryStringConstants.NETACCOUNT_ID]);

        ucAccountDescription.AccountID = this.AccountID;
        ucAccountDescription.NetAccountID = this.NetAccountID;
    }
    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        pnlRecForm.Visible = true;
        RecHelper.ShowRecStatusBar(this, ucRecStatusBar);
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
    /// <summary>
    /// Sets the master page settings.
    /// </summary>
    /// <param name="oMasterPageSettings">The o master page settings.</param>
    public override void SetMasterPageSettings(MasterPageSettings oMasterPageSettings)
    {
        base.SetMasterPageSettings(oMasterPageSettings);
        ucAccountDescription.EditMode = oMasterPageSettings.EditMode;

    }

    public void RegisterPostBackToControls(ExRadGrid oExRadGrid)
    {
        MasterPages_ARTMasterPage oMasterPage = (MasterPages_ARTMasterPage)this.Master;
        ScriptManager ScriptManager1 = oMasterPage.GetScriptManager();
        ScriptManager1.RegisterPostBackControl(oExRadGrid);
    }
    #endregion

}
