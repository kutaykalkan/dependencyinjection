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
using SkyStem.ART.Web.Data;


namespace SkyStem.ART.Web.Classes.UserControl
{
    /// <summary>
    /// Summary description for UserControlMappingUpload
    /// </summary>
    public class UserControlMappingUpload : UserControlBase
    {
        public UserControlMappingUpload()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        protected override void Render(System.Web.UI.HtmlTextWriter writer)
        {
            if (Helper.IsFeatureActivated(WebEnums.Feature.MappingUpload, SessionHelper.CurrentReconciliationPeriodID))
                base.Render(writer);
        }
    }
}
