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
using SkyStem.Language.LanguageUtility;
using SkyStem.ART.Web.Utility;
using SkyStem.ART.Web.Classes;

public partial class UserControls_Signature : UserControlBase
{

    string _textSignature;
    public string TextSignature
    {
        get
        {
            return _textSignature;
        }
        set
        {
            _textSignature = value;
            lblSignature.Text = _textSignature;
        }
    }

    public override void SetSignature(string userName, DateTime? dtSignOff)
    {
        base.SetSignature(userName, dtSignOff);
        lblSignature.Text = string.Format(LanguageUtil.GetValue(1834), userName, Helper.GetDisplayDateTime(dtSignOff));
    }


    protected void Page_Load(object sender, EventArgs e)
    {
    }

}//end of class
