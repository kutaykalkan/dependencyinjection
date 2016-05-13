using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SkyStem.ART.Web.Classes;

namespace SkyStem.ART.Web.WebParts
{

    /// <summary>
    /// Summary description for TaskViewerWP
    /// </summary>
    public class TaskStatusWP : WebPartBase
    {
        public TaskStatusWP()
        {
            base.DashboardID = 9;
            base.DefaultZoneID = 1;
        }
    }
}
