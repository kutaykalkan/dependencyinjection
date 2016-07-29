using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using SkyStem.ART.Web.Utility;
using System.Text;
using System.Globalization;


namespace SkyStem.ART.Web.Classes
{

    /// <summary>
    /// Summary description for PopupPageBase
    /// </summary>
    public abstract class PopupPageBaseRecPeriod : PopupPageBase
    {
        public PopupPageBaseRecPeriod()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);


            // Check for Company
            if (SessionHelper.CurrentCompanyID == null)
            {
                PopupHelper.RedirectToErrorPage(5000184, false);
            }

            // Check for Rec Period
            if (SessionHelper.CurrentReconciliationPeriodID == null)
            {
                PopupHelper.RedirectToErrorPage(5000061, true);
            }

        }

    }
}