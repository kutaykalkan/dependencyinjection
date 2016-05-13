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
using SkyStem.ART.Web.Data;

namespace SkyStem.ART.Web.Classes
{
    /// <summary>
    /// Summary description for PageBaseError
    /// </summary>
    public abstract class PageBaseRole : PageBase
    {
        public PageBaseRole()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        protected override void OnLoadComplete(EventArgs e)
        {
            base.OnLoadComplete(e);
        }
    }
}