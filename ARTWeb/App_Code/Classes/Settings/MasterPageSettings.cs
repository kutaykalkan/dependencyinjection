using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SkyStem.ART.Web.Data;

namespace SkyStem.ART.Web.Classes
{
    /// <summary>
    /// Summary description for MasterPageSettings
    /// </summary>
    public class MasterPageSettings
    {
        public bool? EnableCompanySelection { get; set; }
        public bool? EnableRoleSelection { get; set; }
        public bool? EnableRecPeriodSelection { get; set; }
        public bool? EnableWebPartCustomisation { get; set; }
        public bool? HideValidationSummary { get; set; }
        public bool? HideMenu { get; set; }
        public bool? HideToolBar { get; set; }
        public bool? HidePanelLockdownDays { get; set; }
        public bool? HideRecPeriodBar { get; set; }
        public int _EditMode;

        public WebEnums.FormMode EditMode
        {
            get
            {
                return (WebEnums.FormMode)_EditMode;
            }
            set
            {
                _EditMode = (int)value;
            }
        }
        public MasterPageSettings()
        {
            //
            // TODO: Add constructor logic here
            //
        }
    }
}