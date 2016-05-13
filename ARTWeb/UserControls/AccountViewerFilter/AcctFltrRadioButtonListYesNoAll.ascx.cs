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
using SkyStem.Language.LanguageUtility;
using SkyStem.ART.Web.Data;
using SkyStem.ART.Web.Classes;

public partial class AcctFltrRadioButtonListYesNoAll : UserControlBase
{
    #region "Private Properties"
    #endregion

    #region "Public Properties"
    public bool isRequired
    {
        set
        {
            //this.phMandatoryField.Visible = value;
            this.rblCriteria.CausesValidation = value;
            this.rfv.Enabled = value;
        }
    }
    public int ErrorLabelID
    {
        set
        {
            this.rfv.LabelID = value;
        }
    }
    public bool Enabled
    {
        set
        {
            this.rblCriteria.Enabled = value;
        }
    }
    public string GetCriteria
    {
        get
        {
            string retVal=string.Empty ;
            if (this.rblCriteria.SelectedItem != null)
            {
                switch (this.rblCriteria.SelectedValue)
                {
                    case WebConstants.ACCT_FLTR_YES:
                    case WebConstants.ACCT_FLTR_NO:
                        retVal = this.rblCriteria.SelectedItem.Value;
                        break;
                    case WebConstants.ACCT_FLTR_ALL:
                        retVal = string.Empty;
                        break;
                }
            }
            return retVal;
        }
    }
    public bool HasValue
    {
        get
        {
            return (this.rblCriteria.SelectedIndex != -1);

        }
    }
    public string GetDisplayValue
    {
        get
        {
            string displayVal = "";
            if (this.rblCriteria.SelectedItem != null)
                displayVal = this.rblCriteria.SelectedItem.Text;
            return displayVal;
        }
    }
    public string SetCriteria
    {
        set
        {
            this.rblCriteria.ClearSelection();
            if (this.rblCriteria.Items.FindByValue(value) != null)
                this.rblCriteria.SelectedValue = value;
        }
    }
    #endregion

    #region "Page EventHandlers"
    protected void Page_Init(object sender, EventArgs e)
    {
        ListItem lstYes = new ListItem(LanguageUtil.GetValue(1252), WebConstants.ACCT_FLTR_YES);
        ListItem lstNo = new ListItem(LanguageUtil.GetValue(1251), WebConstants.ACCT_FLTR_NO);
        ListItem lstAll = new ListItem(LanguageUtil.GetValue(1262), WebConstants.ACCT_FLTR_ALL);

        this.rblCriteria.Items.Add(lstAll);
        this.rblCriteria.Items.Add(lstYes);
        this.rblCriteria.Items.Add(lstNo);
    }
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    #endregion

    #region "Public Methods"
    public void ClearSelection()
    {
        this.rblCriteria.ClearSelection();
    }
    #endregion
}
