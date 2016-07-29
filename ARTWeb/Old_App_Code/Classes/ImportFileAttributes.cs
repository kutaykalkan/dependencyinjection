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

namespace SkyStem.ART.Web.Classes
{
    /// <summary>
    /// This structure is used to save data import file attributes
    /// </summary>
    [Serializable ]
    public struct ImportFileAttributes
    {
        public string FileOriginalName;
        public string FileModifiedName;
        public string FilePhysicalPath;
        public long FileSize;
    }
}
