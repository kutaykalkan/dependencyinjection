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
using SkyStem.ART.Web.Classes;

namespace SkyStem.ART.Web.UserControls
{
    public partial class ReviewNotesButton : UserControlBase
    {
        #region "Private Properties"
        private string _url;
        private string _glDataID;
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
        public string GLDataID
        {
            get
            {
                return this._glDataID ?? "";
            }
            set
            {
                this._glDataID = value;
            }
        }
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void Page_PreRender(object sender, EventArgs e)
        {
            this.imgbtnDocument.Attributes.Add("GLDataID", this.GLDataID);
            this.imgbtnDocument.Attributes.Add("URL", this.URL);
        }
    }
}
