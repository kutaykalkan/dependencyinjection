using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SkyStem.ART.Web.Utility;
using SkyStem.ART.Web.Data;

public partial class UserControls_Report_FinancialYear : System.Web.UI.UserControl
{
    // Delegate declaration
    public delegate void OnFinancialYearChanged(string SelectedValue, string selectedText);
    // Event declaration
    public event OnFinancialYearChanged FinancialYearChangedHandler;

    #region "Private Properties"
    private int? _selectedFinancialYearID = null;

    public int? FinancialYearID
    {
        set
        {
            this._selectedFinancialYearID = value;
            this.ddlFinancialYear.ClearSelection();
            if (this.ddlFinancialYear.Items.FindByValue(this._selectedFinancialYearID.ToString()) != null)
                this.ddlFinancialYear.SelectedValue = this._selectedFinancialYearID.ToString();
        }
        get
        {
            return _selectedFinancialYearID;
        }
    }
    #endregion

    #region "Public Properties"
    public string GetSelectedFinancialYear
    {
        get
        {
            return this.ddlFinancialYear.SelectedValue;
        }
    }
    #endregion


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ListControlHelper.BindFinancialYearDropdown(ddlFinancialYear, true);
            if (SessionHelper.CurrentFinancialYearID != null)
            {
                ddlFinancialYear.SelectedValue = SessionHelper.CurrentFinancialYearID.Value.ToString();
            }
        }

    }

    protected void ddlFinancialYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        string selectedValue = this.ddlFinancialYear.SelectedValue;
        string selectedText = this.ddlFinancialYear.SelectedItem.Text;
        if (selectedValue != WebConstants.SELECT_ONE
            && !string.IsNullOrEmpty(selectedValue))
        {
            _selectedFinancialYearID = Convert.ToInt32(selectedValue);
        }
        else
        {
            _selectedFinancialYearID = null;
        }
        if (FinancialYearChangedHandler != null)
            FinancialYearChangedHandler(selectedValue, selectedText);

    }

    #region "Public Methods"
    public void GetCriteria(Dictionary<string, string> dictRecPeriodCriteria, string keyName)
    {
        if (this.ddlFinancialYear.SelectedValue != WebConstants.SELECT_ONE)
        {
            dictRecPeriodCriteria.Add(keyName, this.ddlFinancialYear.SelectedValue);
        }
    }

    public void SetCriteria(Dictionary<string, string> oRptCriteria, string keyName)
    {
        int financialYearID;
        if (oRptCriteria.ContainsKey(keyName))
        {
            if (int.TryParse(oRptCriteria[keyName], out financialYearID))
                this.FinancialYearID = financialYearID;
        }
    }
    #endregion


}
