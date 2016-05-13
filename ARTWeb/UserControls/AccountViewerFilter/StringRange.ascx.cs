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
using System.Text;
using SkyStem.ART.Web.Data;
using SkyStem.ART.Web.Classes;
using SkyStem.Library.Controls.WebControls;
using SkyStem.ART.Web.Utility;

public partial class AcctFltrStringRange : UserControlBase 
{
    #region "Private Properties"
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
    #endregion

    #region "Public Properties"
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
    public bool isRequired
    {
        set
        {
            this.rfv.Enabled = value;
            //this.phMandatory.Visible = value;
        }
    }
    public int ErrorLabelID
    {
        set
        {
            this.rfv.LabelID = value;
        }
    }
    public ExCustomValidator Validator
    {
        get
        {
            return this.rfv;
        }
    }
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
    }
    protected void Page_Init(object sender, EventArgs e)
    {
        this.rfv.Attributes.Add("toTxtClientID", this.txtTo.ClientID);
        this.rfv.Attributes.Add("fromTxtClientID", this.txtFrom.ClientID);
    }
    public string GetFromCriteria
    {
        get
        {
            return this.txtFrom.Text;
        }
    }
    public string GetToCriteria
    {
        get
        {
            return this.txtTo.Text;
        }
    }
    public string SetFromCriteria
    {
        set
        {
            this.SetFromValue = value;
        }
    }
    public string SetToCriteria
    {
        set
        {
            this.SetToValue = value;
        }
    }
    public string GetCriteria
    {
        get
        {
            return this.txtFrom.Text + AccountFilterHelper.AccountFilterValueSeparator + this.txtTo.Text;
       }
    }
    public string SetCriteria
    {
        set
        {
            char[] arrySeperator = AccountFilterHelper.AccountFilterValueSeparator.ToCharArray();
            string[] _val = value.Split(arrySeperator, StringSplitOptions.RemoveEmptyEntries);
            if (_val.Length > 0)
                this.txtFrom.Text = _val[0];
            if (_val.Length > 1)
                this.txtTo.Text = _val[1];

        }
    }
    public void ClearSelection()
    {
        this.txtFrom.Text = "";
        this.txtTo.Text = "";
    }
    public string GetCriteriaForDisplay
    {
        get
        {
            return this.txtFrom.Text + WebConstants.ACCT_FLRT_DISPLAY_VALUE_SEPERATOR + this.txtTo.Text;
        }
    }
    public bool HasValue
    {
        get
        {
            return ((this.txtFrom.Text + this.txtTo.Text) != string.Empty);
        }
    }
    
}
