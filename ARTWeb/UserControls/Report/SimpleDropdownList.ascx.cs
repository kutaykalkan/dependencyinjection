using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SkyStem.ART.Web.Classes;
using SkyStem.ART.Web.Data;

public partial class SimpleDropDownList : UserControlBase
{
    // Delegate declaration
    public delegate void OnDDLSelectedIndexChanged(string SelectedValue, string selectedText);
    // Event declaration
    public event OnDDLSelectedIndexChanged DDLSelectedIndexChangedHandler;
    #region Public Properties

    public string SelectedValue
    {
        get { return ddlSimpleDropDown.SelectedValue; }
        set
        {
            ddlSimpleDropDown.ClearSelection();
            if (ddlSimpleDropDown.Items.FindByValue(value) != null)
                ddlSimpleDropDown.SelectedValue = value;
        }
    }
    public bool isRequired
    {
        set
        {
            this.rfv.Enabled = value;
            this.ddlSimpleDropDown.CausesValidation = false;
            //this.rowMandatory.Visible = value;
            if (value)
                this.rowMandatory.Style[HtmlTextWriterStyle.Visibility] = "";
            else
                this.rowMandatory.Style[HtmlTextWriterStyle.Visibility] = "hidden";
        }
    }
    public int LabelID
    {
        get { return this.lblSimpleDropDown.LabelID; }
        set { this.lblSimpleDropDown.LabelID = value; }
    }
    public int ErrorLabelID
    {
        set
        {
            this.rfv.LabelID = value;
        }
    }
    public DropDownList DropDownListControl
    {
        get { return this.ddlSimpleDropDown; }
    }

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    #region "Public Methods"

    public void GetCriteria(Dictionary<string, string> dictRecPeriodCriteria, string criteriaKey)
    {
        dictRecPeriodCriteria.Remove(criteriaKey);
        if (this.SelectedValue == WebConstants.SELECT_ONE)
            dictRecPeriodCriteria.Add(criteriaKey, null);
        else
            dictRecPeriodCriteria.Add(criteriaKey, this.ddlSimpleDropDown.SelectedValue);
    }

    public void SetCriteria(Dictionary<string, string> dictRecPeriodCriteria, string criteriaKey)
    {
        if (dictRecPeriodCriteria.ContainsKey(criteriaKey))
        {
            if (dictRecPeriodCriteria[criteriaKey] != null)
                this.SelectedValue = dictRecPeriodCriteria[criteriaKey];
            else
                this.SelectedValue = WebConstants.SELECT_ONE;
        }
    }
    protected void ddlSimpleDropDown_SelectedIndexChanged(object sender, EventArgs e)
    {
        string selectedValue = this.ddlSimpleDropDown.SelectedValue;
        string selectedText = this.ddlSimpleDropDown.SelectedItem.Text;
        if (DDLSelectedIndexChangedHandler != null)
            DDLSelectedIndexChangedHandler(selectedValue, selectedText);
           
    }

    #endregion
}
