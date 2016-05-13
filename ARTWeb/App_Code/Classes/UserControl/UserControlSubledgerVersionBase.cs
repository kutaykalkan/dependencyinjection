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

/// <summary>
/// Summary description for UserControlSubledgerVersionBase
/// </summary>
public class UserControlSubledgerVersionBase : System.Web.UI.UserControl
{
    protected string POPUP_PAGE = "MultiVersionSubledger/PopupSubledgerVersion.aspx?";
    protected string PopupUrl = null;
    protected const int POPUP_WIDTH = 600;
    protected const int POPUP_HEIGHT = 480;

    public bool IsVersionAvailable { get; set; }
    public long? GLDataID { get; set; }

    public UserControlSubledgerVersionBase()
    {
        //
        // TODO: Add constructor logic here
        //
    }
}
