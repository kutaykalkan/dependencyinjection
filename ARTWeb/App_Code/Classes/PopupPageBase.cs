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
using System.Text;
using System.Globalization;
using SkyStem.ART.Web.Data;


namespace SkyStem.ART.Web.Classes
{

    /// <summary>
    /// Summary description for PopupPageBase
    /// </summary>
    public abstract class PopupPageBase : Page
    {
        public PopupPageBase()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);

            // Check for Session
            if (!SessionHelper.IsSessionValid())
            {
                Response.Write(ScriptHelper.GetJSForClosePopupAndSubmitParentPage());
                Response.End();
            }

            // Set the Language (Culture) for the Current Thread
            LanguageHelper.SetCurrentCulture();

            // Render Culture Specifc JS
            ScriptHelper.RenderCultureSpecificJS(this);

            // Render JS for Global Constants
            ScriptHelper.RenderGlobalConstantsInJS(this);
        }

        WebEnums.RecPeriodStatus? _CurrentRecProcessStatus = null;
        public WebEnums.RecPeriodStatus? CurrentRecProcessStatus
        {
            get
            {
                if (!_CurrentRecProcessStatus.HasValue)
                    _CurrentRecProcessStatus = SessionHelper.CurrentRecProcessStatusEnum;
                return _CurrentRecProcessStatus;
            }
            set
            {
                // Save to View State
                _CurrentRecProcessStatus = value;
            }
        }

    }
}