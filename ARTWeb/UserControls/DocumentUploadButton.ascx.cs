using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Text;
using SkyStem.ART.Web.Classes;

namespace SkyStem.ART.Web.UserControls
{
    public partial class DocumentUploadButton : UserControlBase
    {
        #region "Private Properties"
        private string _url;
        private string _windowName;
        #endregion

        #region "Public Properties"
        public string URL
        {
            get
            {
                return this._url ?? "";
            }
            set
            {
                this._url = value;
            }
        }
        public string WindowName
        {
            get
            {
                return this._windowName;
            }
            set
            {
                this._windowName = value;
            }
        }
        #endregion

        protected void Page_Init(object sender, EventArgs e)
        {
            
        }
        protected void Page_Load(object sender, EventArgs e)
        {
        }
        protected void Page_PreRender(object sender, EventArgs e)
        {
            this.imgbtnDocument.Attributes.Add("URL", this.URL);
            this.imgbtnDocument.Attributes.Add("WindowName", this.WindowName );
        }
    }
}