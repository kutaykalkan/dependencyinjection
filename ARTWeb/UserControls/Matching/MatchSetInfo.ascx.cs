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
using SkyStem.ART.Client.Model.Matching;

public partial class UserControls_Matching_MatchSetInfo : UserControlBase
{

    protected void Page_Load(object sender, EventArgs e)
    {
    }
    public void PopulateData(MatchSetHdrInfo oMatchSetHdrInfo)
    {
        lblMatchSet.Text = oMatchSetHdrInfo.MatchSetRef + " - " + oMatchSetHdrInfo.MatchSetName;
    }
}
