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
using AjaxControlToolkit;
using SkyStem.Language.LanguageUtility;

public partial class UserControls_InputRequirements : System.Web.UI.UserControl
{
    #region Variables & Constants
    #endregion

    #region Properties
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
    #endregion

    #region Delegates & Events
    #endregion

    #region Page Events
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            string CollapsedText = LanguageUtil.GetValue(1908);
            string ExpandedText = LanguageUtil.GetValue(1260);
            cpeInputRequirements.CollapsedText = CollapsedText;
            cpeInputRequirements.ExpandedText = ExpandedText;
        }
    }
    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        rptNotes.DataBind();
    }
    #endregion

    #region Grid Events
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
    #endregion

    #region Other Events
    #endregion

    #region Validation Control Events
    #endregion

    #region Private Methods
    #endregion

    #region Other Methods
    public void ShowInputRequirements(params int[] oLabelIDCollection)
    {
        this.Visible = true;
        this.Datasource = oLabelIDCollection;
    }
    #endregion

}
