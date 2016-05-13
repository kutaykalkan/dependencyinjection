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
using SkyStem.ART.Web.Data;


public partial class AcctFltrDateRange : UserControlBase
{

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
    
    public string GetCurrentFromDate
    {
        get { return calFrom.Text; }
    }
    
    public string GetCurrentToDate
    {
        get { return calTo.Text; }
    }

    public string GetCriteria
    {
        get
        {
            return this.calFrom.Text + AccountFilterHelper.AccountFilterValueSeparator + this.calTo.Text;
        }
    }

    public string SetCriteria
    {
        set
        {
            char[] arrySeperator = AccountFilterHelper.AccountFilterValueSeparator.ToCharArray();
            string[] _val = value.Split(arrySeperator, StringSplitOptions.RemoveEmptyEntries);
            if (_val.Length > 0)
                this.calFrom.Text = _val[0];
            if (_val.Length > 1)
                this.calTo.Text = _val[1];

        }
    }

    public string GetCriteriaForDisplay
    {
        get
        {
            return this.calFrom.Text + WebConstants.ACCT_FLRT_DISPLAY_VALUE_SEPERATOR + this.calTo.Text;
        }
    }

    public void ClearSelection()
    {
        this.calFrom.Text = "";
        this.calTo.Text = "";
    }

    public bool HasValue
    {
        get
        {
            return ((this.calFrom.Text + this.calTo.Text) != string.Empty);
        }
    }

}
