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
using SkyStem.ART.Web.Data;

namespace SkyStem.ART.Web.Classes
{
    /// <summary>
    /// Summary description for PageBaseTaskMaster
    /// </summary>
    public class PageBaseTaskMaster : PageBase
    {
        public PageBaseTaskMaster()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        #region Properties
        /// <summary>
        /// Form Mode for Current Form
        /// </summary>
        public WebEnums.FormMode FormMode
        {
            get
            {
                WebEnums.FormMode eFormMode = WebEnums.FormMode.ReadOnly;
                if (ViewState["FormMode"] != null)
                    eFormMode = (WebEnums.FormMode)ViewState["FormMode"];
                return eFormMode;
            }
            set
            {
                ViewState["FormMode"] = value;
            }
        }

        public string ReturnUrl
        {
            get
            {
                return ViewState["returnUrl"].ToString();
            }
            set
            {
                ViewState["returnUrl"] = value;
            }
        }
        #endregion


        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }

        public override string GetMenuKey()
        {
            return "";
        }
    }
}
