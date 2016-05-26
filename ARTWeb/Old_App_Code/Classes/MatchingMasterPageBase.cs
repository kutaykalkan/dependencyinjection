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

/// <summary>
/// Summary description for MatchingMasterPageBase
/// </summary>
public class MatchingMasterPageBase : MasterPage
{
    public MatchingMasterPageBase()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    private long? _AccountID = null;
    private int? _NetAccountID = null;
    private long? _GLDataID = null;
    private System.Int32? _PageTitleLabeID = null;

    public long? GLDataID
    {
        get { return _GLDataID; }
        set { _GLDataID = value; }
    }

    public long? AccountID
    {
        get { return _AccountID; }
        set { _AccountID = value; }
    }

    public int? NetAccountID
    {
        get { return _NetAccountID; }
        set { _NetAccountID = value; }
    }

    public System.Int32? PageTitleLabeID
    {
        get { return _PageTitleLabeID; }
        set { _PageTitleLabeID = value; }
    }

}
