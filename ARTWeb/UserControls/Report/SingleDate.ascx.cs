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
using System.Collections.Generic;
using SkyStem.ART.Web.Data;

public partial class UserControls_Report_SingleDate : UserControlBase 
{
    private int _LabelID;

    public int LabelID
    {
        get { return _LabelID; }
        set { _LabelID = value; }
    }
    public DateTime SetSingleDate
    {
        set
        {
            this.clSingleDate.Text = Helper.GetDisplayDateForCalendar(value);
        }
    }
    public string GetSingleDate
    {
        get
        {
            return  this.clSingleDate.Text; 
        }
    }
   
    protected void Page_Load(object sender, EventArgs e)
    {
        lblDate.LabelID = LabelID;
        cvSingleDate.ErrorMessage = Helper.GetErrorMessage(WebEnums.FieldType.DateFormatField, LabelID);

    }
    public void GetCriteria(Dictionary<string, string> oRptCriteriaCollection, string criteriaKey)
    {
        oRptCriteriaCollection.Add(criteriaKey, this.clSingleDate.Text);
    }
    public void SetCriteria(Dictionary<string, string> oRptCriteriaCollection, string criteriaKey)
    {
        DateTime dt = new DateTime();
        if (oRptCriteriaCollection.ContainsKey(criteriaKey))
        {
            if (DateTime.TryParse(oRptCriteriaCollection[criteriaKey], out dt))
                this.SetSingleDate = dt;
        }
    }
}
