using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SkyStem.ART.Web.Utility;
using SkyStem.ART.Web.Data;

namespace SkyStem.ART.Web.Classes.UserControl
{
    /// <summary>
    /// Summary description for UserControlQualityScoreBase
    /// </summary>
    public class UserControlQualityScoreBase: UserControlBase
    {
        public UserControlQualityScoreBase()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        protected override void Render(System.Web.UI.HtmlTextWriter writer)
        {
            if (Helper.IsFeatureActivated(WebEnums.Feature.QualityScore, SessionHelper.CurrentReconciliationPeriodID))
                base.Render(writer);
        }
    }
}