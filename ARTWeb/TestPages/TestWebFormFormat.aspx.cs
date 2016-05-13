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
using SkyStem.ART.Web.Utility;

public partial class TestPages_TestWebFormFormat : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        decimal d = 123456.78M;
        lblNUmberFormat.Text = Helper.GetDisplayDecimalValue(d);
        txtNumber2.Attributes.Add("onblur", "javascript:CalculateTotal();");
        btn.Attributes.Add("onclick", "javascript:ValidateDate();");
        Response.Write(System.Globalization.CultureInfo.CurrentUICulture.DateTimeFormat.ShortDatePattern);
    }

    protected void btn_Click(object sender, EventArgs e)
    {
        lblDateValue.Text = calTest.Text;
        DateTime.Parse(calTest.Text);
    }

    public override string GetMenuKey()
    {
        return "";
    }
}
