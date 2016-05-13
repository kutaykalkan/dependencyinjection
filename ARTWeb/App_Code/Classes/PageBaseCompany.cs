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
using SkyStem.ART.Web.Classes;
using SkyStem.ART.Web.Utility;

namespace SkyStem.ART.Web.Classes
{

    /// <summary>
    /// Summary description for PageBaseCompany
    /// </summary>
    public abstract class PageBaseCompany : PageBaseRole
    {
        public PageBaseCompany()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        /// <summary>
        /// Call Base Class PreInit
        /// Check for availabiity of Rec Period
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);

            // Check for Company
            if (SessionHelper.CurrentCompanyID == null)
            {
                Helper.RedirectToErrorPage(5000184, false);
            }
        }
    }
}