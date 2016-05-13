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
using SkyStem.ART.Client.Model;

namespace SkyStem.ART.Web.Classes
{
    /// <summary>
    /// Summary description for PopupPageBase
    /// </summary>
    public abstract class PopupPageBaseTaskMaster : PopupPageBase
    {
  
        private string _Mode;
        private string _ParentHiddenField;   

        #region Common Properties

        public string Mode
        {
            get
            {
                if (String.IsNullOrEmpty(this._Mode) && Request.QueryString[QueryStringConstants.MODE] != null)
                    this._Mode = Request.QueryString[QueryStringConstants.MODE];
                return this._Mode;
            }
            private set
            {
                this._Mode = value;
            }
        }
        public string ParentHiddenField
        {
            get
            {
                if (String.IsNullOrEmpty(this._ParentHiddenField) && Request.QueryString[QueryStringConstants.PARENT_HIDDEN_FIELD] != null)
                    this._ParentHiddenField = Request.QueryString[QueryStringConstants.PARENT_HIDDEN_FIELD];
                return this._ParentHiddenField;
            }
            private set
            {
                this._ParentHiddenField = value;
            }
        }
  
        #endregion
    }
}
