using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SkyStem.ART.Web.Data;
using SkyStem.ART.Web.Classes;
using SkyStem.ART.Web.Utility;

public partial class Pages_TaskMaster_TaskAttachment : PopupPageBaseTaskMaster
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.Title = "Attachments";
        string _mode = string.Empty;
        
        if (!String.IsNullOrEmpty(Request.QueryString[QueryStringConstants.MODE]))
            _mode = Request.QueryString[QueryStringConstants.MODE];

        if (_mode == QueryStringConstants.INSERT)
        {
            Helper.ShowInputRequirementSectionForPopup((PopupPageBase)this.Page, 2621, 2773);
        }
    }
}
