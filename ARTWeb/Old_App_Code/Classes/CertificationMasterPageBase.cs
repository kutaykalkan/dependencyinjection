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
    /// Summary description for CertificationMasterPageBase
    /// </summary>
    public class CertificationMasterPageBase : MasterPage
    {
        private string _PrintUrl = "";
        private bool _ShowExportToolbar = true;
        private System.Int32? _PageTitleLabeID = null;
        private WebEnums.CertificationType _CertificationType = WebEnums.CertificationType.None;

        public WebEnums.CertificationType CertificationType
        {
            get { return _CertificationType; }
            set { _CertificationType = value; }
        }

        public string PrintUrl
        {
            get { return _PrintUrl; }
            set { _PrintUrl = value; }
        }

        public System.Int32? PageTitleLabeID
        {
            get { return _PageTitleLabeID; }
            set { _PageTitleLabeID = value; }
        }


        public bool ShowExportToolbar
        {
            get { return _ShowExportToolbar; }
            set { _ShowExportToolbar = value; }
        }


    }
}
