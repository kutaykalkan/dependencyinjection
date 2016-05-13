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


namespace SkyStem.ART.Web.WebParts
{
    /// <summary>
    /// Summary description for OpenItemReport
    /// </summary>
    public class OpenItemStatusWP : WebPartBase
    {
        public OpenItemStatusWP()
        {
            base.DashboardID = 8;
            base.DefaultZoneID = 1;
            //
            // TODO: Add constructor logic here
            //
        }
    }
}
