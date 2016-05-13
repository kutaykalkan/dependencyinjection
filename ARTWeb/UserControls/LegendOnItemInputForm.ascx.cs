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

public partial class UserControls_LegendOnItemInputForm : System.Web.UI.UserControl
{
    private bool _showSchedule = false;
    public  bool ShowSchedule
    {
        get
        {
            return this._showSchedule;
        }
        set
        {
            this._showSchedule = value;
        }
    }

    private bool _showDocument = false;
    public bool ShowDocument
    {
        get
        {
            return this._showDocument;
        }
        set
        {
            this._showDocument = value;
        }
    }

    private int _scheduleNameLabelID =0;
    public int ScheduleNameLabelID
    {
        get
        {
            return this._scheduleNameLabelID;
        }
        set
        {
            this._scheduleNameLabelID = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (_showSchedule == true)
        {
            //tdLegendHeader.ColSpan = 6;
            tdSchedule.Visible = true;
            lblSchedule.LabelID=_scheduleNameLabelID;
            imgSchedule.LabelID = _scheduleNameLabelID;
            //if (_showDocument == true)
            //{
            //    tdLegendHeader.ColSpan = 2;
            //    tdDocument.Visible = true;
            //}
            //else
            //{
            //    tdLegendHeader.ColSpan = 1;
            //    tdDocument.Visible = false;
            //}
        }
        else
        {
            //tdLegendHeader.ColSpan = 5;
            tdSchedule.Visible = false ;

            //if (_showDocument == true)
            //{
            //    tdLegendHeader.ColSpan = 1;
            //    tdDocument.Visible = true;
            //}
            //else
            //{
            //    tdLegendHeader.ColSpan = 0;
            //    tdDocument.Visible = false;
            //}
        }

    }
}//end of class
