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
    /// Summary description for ReconciliationTrackingWP
    /// </summary>
    public class ReconciliationTrackingWP : WebPartBase
    {
        public ReconciliationTrackingWP()
        {
            base.DashboardID = 5;
            base.DefaultZoneID = 1;
        }
    }
}