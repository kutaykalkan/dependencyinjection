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
using SkyStem.Language.LanguageUtility;


namespace SkyStem.ART.Web.Classes
{

    /// <summary>
    /// Summary description for WebPartBase
    /// </summary>
    public abstract class WebPartBase : WebPart
    {
        private Int32? _TitleLabelID = null;
        private Int32? _DescriptionLabelID = null;
        private String _UserControlUrl = null;
        private UserControlWebPartBase _LoadedDashboardControl = null;

        /// <summary>
        /// Dashboard ID as defined in the Master table
        /// </summary>
        public Int16? DashboardID
        {
            get
            {
                object obj = ViewState["_DashboardID"];
                if (obj != null)
                    return Convert.ToInt16(obj);
                return null;
            }
            set { ViewState["_DashboardID"] = value; }
        }

        /// <summary>
        /// Gets or sets the default zone ID.
        /// </summary>
        /// <value>
        /// The default zone ID.
        /// </value>
        public Int16? DefaultZoneID
        {
            get
            {
                object obj = ViewState["_DefaultZoneID"];
                if (obj != null)
                    return Convert.ToInt16(obj);
                return null;
            }
            set { ViewState["_DefaultZoneID"] = value; }
        }

        /// <summary>
        /// Label ID for Web Part Title
        /// </summary>
        public Int32? TitleLabelID
        {
            get { return _TitleLabelID; }
            set { _TitleLabelID = value; }
        }

        /// <summary>
        /// Label ID for Web Part Description
        /// </summary>
        public Int32? DescriptionLabelID
        {
            get { return _DescriptionLabelID; }
            set { _DescriptionLabelID = value; }
        }

        public String UserControlUrl
        {
            get
            {
                if (!string.IsNullOrEmpty(_UserControlUrl))
                    return _UserControlUrl;
                object obj = ViewState["_UserControlUrl"];
                if (obj != null)
                    return obj.ToString();
                return null;
            }
            set
            {
                ViewState["_UserControlUrl"] = value;
                _UserControlUrl = value;
            }
        }

        public UserControlWebPartBase LoadedDashboardControl
        {
            get
            {
                return _LoadedDashboardControl;
            }
            private set
            {
                _LoadedDashboardControl = value;
            }
        }

        public WebPartBase()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            base.ChromeType = PartChromeType.TitleOnly;
            base.AllowClose = true;
            base.AllowMinimize = true;
        }

        protected override void CreateChildControls()
        {
            base.CreateChildControls();
            if (!this.Hidden)
            {
                if (this.UserControlUrl != null)
                {
                    Control oUserControl = this.Page.LoadControl(this.UserControlUrl);
                    oUserControl.Visible = !this.Hidden;
                    base.Controls.Add(oUserControl);
                    LoadedDashboardControl = (UserControlWebPartBase)oUserControl;
                }
            }
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            if (this.TitleLabelID != null)
            {
                base.Title = LanguageUtil.GetValue(this.TitleLabelID.Value);
            }

            if (this.DescriptionLabelID != null)
            {
                base.Description = LanguageUtil.GetValue(this.DescriptionLabelID.Value);
            }
        }
    }
}