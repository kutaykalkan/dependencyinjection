using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SkyStem.ART.Web.Utility;
using SkyStem.ART.Client.Model;

public partial class UserControls_FooterMaster : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            UserHdrInfo oUserHdrInfo =  SessionHelper.GetCurrentUser();
            if(oUserHdrInfo!=null)
                lblDateTime.Text = Helper.GetDisplayDateTime(oUserHdrInfo.LastLoggedIn);
        }
    }
}
