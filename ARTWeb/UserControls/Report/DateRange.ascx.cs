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
using SkyStem.ART.Web.Utility;
using System.Collections.Generic;
using SkyStem.ART.Web.Classes;

public partial class DateRange : UserControlBase
{
    public int LabelID
    {
        set
        {
            this.lblCriteriaName.LabelID = value;
        }
    }
    public int FromLabelID
    {
        set
        {
            this.lblFrom.LabelID = value;
        }
    }
    public int ToLabelID
    {
        set
        {
            this.lblTo.LabelID = value;
        }
    }
    public DateTime SetFromDate
    {
        set
        {
            this.calFrom.Text = Helper.GetDisplayDateForCalendar(value);
        }
    }
    public DateTime? SetToDate
    {
        set
        {
            this.calTo.Text = Helper.GetDisplayDateForCalendar(value);
        }
    }
    
    protected void Page_Load(object sender, EventArgs e)
    {
    }
    public void GetFromDateCriteria(Dictionary<string, string> oRptCriteriaCollection, string criteriaKey)
    {
        oRptCriteriaCollection.Add(criteriaKey, this.calFrom.Text);
    }
    public void GetToDateCriteria(Dictionary<string, string> oRptCriteriaCollection, string criteriaKey)
    {
        oRptCriteriaCollection.Add(criteriaKey, this.calTo.Text);
    }
    public void SetFromDateCriteria(Dictionary<string, string> oRptCriteriaCollection, string criteriaKey)
    {
        DateTime dt = new DateTime();
        if (oRptCriteriaCollection.ContainsKey(criteriaKey))
        {
            if (DateTime.TryParse(oRptCriteriaCollection[criteriaKey], out dt))
                this.SetFromDate = dt;
        }
    }
    public void SetToDateCriteria(Dictionary<string, string> oRptCriteriaCollection, string criteriaKey)
    {
        DateTime dt = new DateTime();
        if (oRptCriteriaCollection.ContainsKey(criteriaKey))
        {
            if (DateTime.TryParse(oRptCriteriaCollection[criteriaKey], out dt))
                this.SetToDate = dt;
        }
    }
}
