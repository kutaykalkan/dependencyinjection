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
    /// Summary description for ReconciliationStatusByFSCaptionWP
    /// </summary>
    public class ReconciliationStatusByFSCaptionWP : WebPartBase
    {
        public ReconciliationStatusByFSCaptionWP()
        {
            base.DashboardID = 4;
            base.DefaultZoneID = 2;
        }

    }
}