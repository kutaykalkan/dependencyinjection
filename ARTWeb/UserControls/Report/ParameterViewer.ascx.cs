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
using SkyStem.ART.Client.Model;
using System.Collections.Generic;
using SkyStem.Library.Controls.WebControls;
using System.Text;
using SkyStem.ART.Web.Classes;

public partial class ParameterViewer : UserControlBase
{
    // Delegate declaration
    public delegate void OnLinkButtonClick(object sender, CommandEventArgs e);
    // Event declaration
    public event OnLinkButtonClick btnLinkClick;

    //private string _savedReportID;
    //private string _savedReportName;
    private DataTable _paramDataTable;
    private string _paramDisplayNameField;
    private string _paramvalueField;

    public string ParamDisplayNameField
    {

        set
        {
            this._paramDisplayNameField = value;
        }
    }
    public string ParamValueField
    {
        set
        {
            this._paramvalueField = value;
        }
    }
    public DataTable ParamDataTable
    {
        set
        {
            this._paramDataTable = value;
            this.lbtnParams.Text = this.GetHtml();
        }
    }
    public string ParamDataTableHtml
    {
        get
        {
            return this.GetHtmlForExport();
        }
    }
    //public string SavedReportID
    //{
    //    set
    //    {
    //        this._savedReportID = value;
    //        ViewState["SavedReportID"] = value;
    //    }
    //    get
    //    {
    //        if (ViewState["SavedReportID"] != null)
    //            this._savedReportID = ViewState["SavedReportID"].ToString();
    //        else
    //            this._savedReportID = "";
    //        return this._savedReportID;
    //    }
    //}
    //public string SavedReportName
    //{
    //    set
    //    {
    //        this._savedReportName = value;
    //        ViewState["SavedReportName"] = value;
    //    }
    //    get
    //    {
    //        if (ViewState["SavedReportName"] != null)
    //            this._savedReportName = ViewState["SavedReportName"].ToString();
    //        else
    //            this._savedReportName = "";
    //        return this._savedReportName;
    //    }
    //}
    public string CommandArgs
    {
        set
        {
            this.lbtnParams.CommandArgument = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }
    private  string GetHtml()
    {
        StringBuilder oSB = new StringBuilder();
        for (int i = 0; i < this._paramDataTable.Rows.Count; i++)
        {
            DataRow dr = this._paramDataTable.Rows[i];
            if (!string.IsNullOrEmpty(dr[this._paramvalueField].ToString()))
            {
                oSB.Append("<li>");
                oSB.Append(dr[this._paramDisplayNameField].ToString());
                oSB.Append(":");
                oSB.Append(dr[this._paramvalueField].ToString());
                oSB.Append("</li>");
            }
        }

        string html = oSB.ToString();
        if (!string.IsNullOrEmpty(html))
        {
            html = "<ul style='padding:0px;margin-left:16px'>" + html + "</ul>";
        }
        return html;
    }
    private string GetHtmlForExport()
    {
        StringBuilder oSB = new StringBuilder();
        for (int i = 0; i < this._paramDataTable.Rows.Count; i++)
        {
            DataRow dr = this._paramDataTable.Rows[i];
            if (!string.IsNullOrEmpty(dr[this._paramvalueField].ToString()))
            {               
                oSB.Append(dr[this._paramDisplayNameField].ToString());
                oSB.Append(":");
                oSB.Append(Server.HtmlEncode(dr[this._paramvalueField].ToString()));                
                oSB.Append("<br />");
            }
        }

        string html = oSB.ToString();
        //if (!string.IsNullOrEmpty(html))
        //{
        //    html = "<ul style='padding:0px;margin-left:16px'>" + html + "</ul>";
        //}
        return html;
    }
    protected void lnkbtnCommand(object sender, CommandEventArgs e)
    {
        if (btnLinkClick != null)
            btnLinkClick(sender, e);
    }
    
}
