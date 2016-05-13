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
using SkyStem.Library.Controls.WebControls;

public partial class UserControls_ErrorAndWarnings : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }


    bool? isErrorOrWarning = null ;
    public bool? IsErrorOrWarning
    {
        get
        {
            return isErrorOrWarning;
        }
        set
        {
            isErrorOrWarning = value;
            if (isErrorOrWarning.HasValue)
            {
                if (isErrorOrWarning.Value)//error
                {
                    imgIconError.Visible = true;
                    imgIconWarning.Visible = false;
                    lblErrorOrWarning.LabelID = 1051;
                }
                else//Warning
                {
                    imgIconError.Visible = false;
                    imgIconWarning.Visible = true;
                    lblErrorOrWarning.LabelID = 1546;
                }
            }
        }
    }


    public object Datasource
    {
        get
        {
            return rptNotes.DataSource;
        }
        set
        {
            rptNotes.DataSource = value;
        }
    }

    protected void rptNotes_OnItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            ExLabel lblNote = (ExLabel)e.Item.FindControl("lblNote");
            if (lblNote != null)
            {
                lblNote.LabelID = Convert.ToInt32(e.Item.DataItem);
            }
        }
    }

    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        rptNotes.DataBind();
       
    }

    
}
