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
using SkyStem.ART.Client.Model;
using SkyStem.Language.LanguageUtility;
using SkyStem.ART.Web.Data;
using SkyStem.ART.Web.Classes;

public partial class RadioButtonListAllYesNo : UserControlBase
{
    #region "Private Properties"
    private string _criteria;
    private string SetChoice
    {
        set
        {
            this._criteria = value;
            this.rblCriteria.ClearSelection();
            if (this.rblCriteria.Items.FindByValue(this._criteria) != null)
                this.rblCriteria.SelectedValue = this._criteria;
        }
    }
    #endregion

    #region "Public Properties"
    public int LabelID
    {
        set
        {
            this.lblCriteriaName.LabelID = value;
        }
    }
    public bool isRequired
    {
        set
        {
            this.phMandatoryField.Visible = value;
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
    #endregion

    #region "Page EventHandlers"
    protected void Page_Init(object sender, EventArgs e)
    {
        ListItem lstAll = new ListItem(LanguageUtil.GetValue(1262), WebConstants.RPT_PRM_ALL );
        ListItem lstYes = new ListItem(LanguageUtil.GetValue(1252), WebConstants.RPT_PRM_YES );
        ListItem lstNo = new ListItem(LanguageUtil.GetValue(1251), WebConstants.RPT_PRM_NO );

        this.rblCriteria.Items.Add(lstAll);
        this.rblCriteria.Items.Add(lstYes);
        this.rblCriteria.Items.Add(lstNo);
    }
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    #endregion

    #region "Controls EventHandlers"
    #endregion

    #region "Private Methods"
    #endregion

    #region "Public Methods"
    public void GetCriteria(Dictionary<string, string> oRptCriteriaCollection, string keyName)
    {
        oRptCriteriaCollection.Add(keyName, this.rblCriteria.SelectedValue);
    }
    public void SetCriteria(Dictionary<string, string> oRptCriteriaCollection, string keyName)
    {
        if (oRptCriteriaCollection.ContainsKey(keyName))
        {
            this.SetChoice = oRptCriteriaCollection[keyName];
        }
    }
    public void ClearSelection()
    {
        this.rblCriteria.SelectedIndex = -1;
    }
    #endregion
}
