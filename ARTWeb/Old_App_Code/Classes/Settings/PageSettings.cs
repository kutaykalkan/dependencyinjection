using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SkyStem.ART.Web.Data;

namespace SkyStem.ART.Web.Classes
{
    /// <summary>
    /// Summary description for PageSettings
    /// </summary>
    public class PageSettings
    {
        public int? PageID { get; set; }
        public Dictionary<string, GridSettings> GridSettingValues { get; set; }
       
        public PageSettings()
        {
            GridSettingValues = new Dictionary<string, GridSettings>();
            ShowSRAAsWell = false;
            ShowAllPendingGeneralTask = false;
            ShowGeneralHiddenTask = false;
            ShowAllPendingAccountTask = false;
            ShowAccountHiddenTask = false;
        }
        public bool ShowSRAAsWell { get; set; }
        public bool ShowAllPendingGeneralTask { get; set; }
        public bool ShowGeneralHiddenTask { get; set; }
        public bool ShowAllPendingAccountTask { get; set; }
        public bool ShowAccountHiddenTask { get; set; }
    }
}