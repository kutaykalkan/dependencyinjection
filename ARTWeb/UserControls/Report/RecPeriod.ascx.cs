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
using SkyStem.ART.Web.Data;
using SkyStem.ART.Web.Classes;
using SkyStem.ART.Client.IServices;
using SkyStem.ART.Web.Utility;

public partial class RecPeriod : UserControlBase
{
    // Delegate declaration
    public delegate void OnRecPeriodChanged(string SelectedValue, string selectedText);
    // Event declaration
    public event OnRecPeriodChanged RecPeriodChangedHandler;

    #region "Private Properties"
    private int _selectedRecPeriod;
    private int SetRecPeriod
    {
        set
        {
            this._selectedRecPeriod = value;
            this.ddlPeriod.ClearSelection();
            if (this.ddlPeriod.Items.FindByValue(this._selectedRecPeriod.ToString()) != null)
                this.ddlPeriod.SelectedValue = this._selectedRecPeriod.ToString();
        }
    }
    #endregion

    #region "Public Properties"
    public int SetDefaultPeriod
    {
        set
        {
            this.SetRecPeriod = value;
        }
    }
    public string GetSelectedRecPeriod
    {
        get
        {
            return this.ddlPeriod.SelectedValue;
        }
    }
    public bool isRequired
    {
        set
        {
            this.ddlPeriod.CausesValidation = false;
            //this.rowMandatory.Visible = value;
            if (value)
                this.rowMandatory.Style[HtmlTextWriterStyle.Visibility] = "";
            else
                this.rowMandatory.Style[HtmlTextWriterStyle.Visibility] = "hidden";
        }
    }
    public bool AddSelectOne { get; set; }

    public DropDownList RecPeriodDropDown
    {
        get
        {
            return this.ddlPeriod;
        }
    }
    #endregion


    protected void Page_Load(object sender, EventArgs e)
    {
        //if (!this.IsPostBack && this.Visible)
        //{
        //    BindRecPeriodDDL();
        //}
    }

    public void ReloadRecPeriods(int? financialYearID)
    {
        if (this.Visible)
        {
            BindRecPeriodDDL(financialYearID);
        }
    }

    public void ReloadRecPeriodsOnFinancialYearChange(int? financialYearID)
    {
        BindRecPeriodDDL(financialYearID);
        ddlPeriod_SelectedIndexChanged(null, null);
    }

    #region "Private Methods"
    private void BindRecPeriodDDL(int? financialYearID)
    {
        //// Bind Directly from DB
        //IList<ReconciliationPeriodInfo> oReconciliationPeriodInfoCollection;
        //// Fetch from DB
        //ICompany oCompanyClient = RemotingHelper.GetCompanyObject();
        //oReconciliationPeriodInfoCollection = (List<ReconciliationPeriodInfo>)oCompanyClient.SelectAllReconciliationPeriodByCompanyID(SessionHelper.CurrentCompanyID.Value, financialYearID, Helper.GetAppUserInfo());

        //ddlPeriod.DataSource = oReconciliationPeriodInfoCollection;
        //ddlPeriod.DataTextField = "PeriodEndDate";
        //ddlPeriod.DataValueField = "ReconciliationPeriodID";
        //ddlPeriod.DataTextFormatString = "{0:d}";
        //ddlPeriod.DataBind();
        ListControlHelper.BindReconciliationPeriod(ddlPeriod, financialYearID);
        if (AddSelectOne)
            ListControlHelper.AddListItemForSelectOne(ddlPeriod);
    }
    #endregion

    #region "Public Methods"
    public void GetCriteria(Dictionary<string, string> dictRecPeriodCriteria, string keyName)
    {
        if (this.ddlPeriod.SelectedValue == WebConstants.SELECT_ONE)
            dictRecPeriodCriteria.Add(keyName, null);
        else
            dictRecPeriodCriteria.Add(keyName, this.ddlPeriod.SelectedValue);
    }
    public void SetCriteria(Dictionary<string, string> oRptCriteria, string keyName)
    {
        int recPeriodID;
        if (oRptCriteria.ContainsKey(keyName))
        {
            if (int.TryParse(oRptCriteria[keyName], out recPeriodID))
                this.SetRecPeriod = recPeriodID;
            else
                this.SetRecPeriod = Convert.ToInt32(WebConstants.SELECT_ONE);
        }
    }
    #endregion

    protected void ddlPeriod_SelectedIndexChanged(object sender, EventArgs e)
    {
        string selectedValue = this.ddlPeriod.SelectedValue;
        string selectedText = string.Empty;
        if(ddlPeriod.SelectedItem!=null)
            selectedText = this.ddlPeriod.SelectedItem.Text;
        if (RecPeriodChangedHandler != null)
            if (selectedValue != WebConstants.SELECT_ONE)
                RecPeriodChangedHandler(selectedValue, selectedText);
            else
                RecPeriodChangedHandler(null, null);
    }
}
