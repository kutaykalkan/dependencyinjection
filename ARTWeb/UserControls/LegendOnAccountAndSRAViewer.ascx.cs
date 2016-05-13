using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SkyStem.Language.LanguageUtility;

public partial class UserControls_LegendOnAccountAndSRAViewer : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string label =  string.Format("{0} / {1}", LanguageUtil.GetValue(1739), LanguageUtil.GetValue(1097));
        lblReconciled.Text = label;
        imgReconciled.ToolTip = label;
    }
}
