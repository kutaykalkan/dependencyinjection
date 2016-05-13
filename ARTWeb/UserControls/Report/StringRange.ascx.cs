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
using System.Collections.Generic;
using SkyStem.ART.Web.Classes;

public partial class StringRange : UserControlBase
{
    public bool IsEnabled
    {
        set
        {
            if (value)
            {
                this.txtFrom.Enabled = true;
                this.txtTo.Enabled = true;
            }
            else
            {
                this.txtFrom.Enabled = false;
                this.txtTo.Enabled = false;
            }
        }
    }
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
    private string SetFromValue
    {
        set
        {
            this.txtFrom.Text = value;
        }
    }
    private string SetToValue
    {
        set
        {
            this.txtTo.Text = value;
        }
    }

    public bool isRequired
    {
        set
        {
            this.rfv.Enabled = value;
            this.phMandatory.Visible = value;
        }
    }
    public int ErrorLabelID
    {
        set
        {
            this.rfv.LabelID = value;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    public void GetFromCriteria(Dictionary<string, string> oReportCriteriaCollection, string criteriaKey)
    {
        oReportCriteriaCollection.Add(criteriaKey, this.txtFrom.Text);
    }
    public void GetToCriteria(Dictionary<string, string> oReportCriteriaCollection, string criteriaKey)
    {
        oReportCriteriaCollection.Add(criteriaKey, this.txtTo.Text);
    }
    public void SetFromCriteria(Dictionary<string, string> oReportCriteriaCollection, string criteriaKey)
    {
        if (oReportCriteriaCollection.ContainsKey(criteriaKey))
        {
            this.SetFromValue = oReportCriteriaCollection[criteriaKey];
        }
    }
    public void SetToCriteria(Dictionary<string, string> oReportCriteriaCollection, string criteriaKey)
    {
        if (oReportCriteriaCollection.ContainsKey(criteriaKey))
        {
            this.SetToValue = oReportCriteriaCollection[criteriaKey];
        }
    }
}
