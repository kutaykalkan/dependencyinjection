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
using SkyStem.ART.Client;
using SkyStem.ART.Client.Model;
using SkyStem.ART.Web.Utility;
using SkyStem.ART.Web.Data;
using System.Text;
using SkyStem.ART.Web.Classes;

public partial class OrganizationalHierarchy : UserControlBase
{
    #region "Private Propertise"
    private string _callbackFunctionName;
    #endregion
    #region "Public Propertise"
    public bool IsEnabled
    {
        set
        {
            if (value)
            {
                this.ddlHierarchy.Enabled = true;
                this.txtHierarchy.Enabled = true;
                this.btnAddMore.Enabled = true;

            }
            else
            {
                this.ddlHierarchy.Enabled = false;
                this.txtHierarchy.Enabled = false;
                this.btnAddMore.Enabled = false;
            }
        }
    }
    public string CallbackFunctionName
    {
        get
        {
            return this._callbackFunctionName == null ? "" : this._callbackFunctionName;
        }
        set
        {
            this._callbackFunctionName = value;
        }
    }
    public string Key2HdnControlClientID
    {
        get
        {
            return this.hdnKey2.ClientID;
        }
    }
    public string Key3HdnControlClientID
    {
        get
        {
            return this.hdnKey3.ClientID;
        }
    }
    public string Key4HdnControlClientID
    {
        get
        {
            return this.hdnKey4.ClientID;
        }
    }
    public string Key5HdnControlClientID
    {
        get
        {
            return this.hdnKey5.ClientID;
        }
    }
    public string Key6HdnControlClientID
    {
        get
        {
            return this.hdnKey6.ClientID;
        }
    }
    public string Key7HdnControlClientID
    {
        get
        {
            return this.hdnKey7.ClientID;
        }
    }
    public string Key8HdnControlClientID
    {
        get
        {
            return this.hdnKey8.ClientID;
        }
    }
    public string Key9HdnControlClientID
    {
        get
        {
            return this.hdnKey9.ClientID;
        }
    }
    public string hdnControlforValidation
    {
        get
        {
            return this.hndTextBox.ClientID;
        }
    }
    public bool isRequired
    {
        set
        {
            this.rvfEntity.Enabled = value;
            this.phMandatory.Visible = value;
        }
    }
    #endregion
    protected void Page_Init(object sender, EventArgs e)
    {

    }
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!this.IsPostBack)
        {
            string s1 = "'" + this.ddlHierarchy.ClientID + "'";
            string s2 = "'" + this.txtHierarchy.ClientID + "'";
            string s3 = "'" + "" + "'";
            string s4 = "'" + "" + "'";

            string jsFunctionName = String.Format(this.CallbackFunctionName, s1, s2, s3, s4);
            this.BindOrgHierarchy();
            this.btnAddMore.Attributes.Add("onclick", jsFunctionName);
        }
    }

    private void BindOrgHierarchy()
    {
        List<GeographyStructureHdrInfo> oGeographyStructureHdrInfoCollection = SessionHelper.GetOrganizationalHierarchy(SessionHelper.CurrentCompanyID);
        oGeographyStructureHdrInfoCollection = oGeographyStructureHdrInfoCollection.Where(a => a.GeographyClassID != (int)WebEnums.GeographyClass.Company).ToList();
        this.ddlHierarchy.DataSource = oGeographyStructureHdrInfoCollection;
        this.ddlHierarchy.DataTextField = "GeographyStructure";
        this.ddlHierarchy.DataValueField = "GeographyClassID";
        this.ddlHierarchy.DataBind();

        this.hdnKey2.Attributes.Add("GeoClassID", ((short)WebEnums.GeographyClass.Key2).ToString());
        this.hdnKey3.Attributes.Add("GeoClassID", ((short)WebEnums.GeographyClass.Key3).ToString());
        this.hdnKey4.Attributes.Add("GeoClassID", ((short)WebEnums.GeographyClass.Key4).ToString());
        this.hdnKey5.Attributes.Add("GeoClassID", ((short)WebEnums.GeographyClass.Key5).ToString());
        this.hdnKey6.Attributes.Add("GeoClassID", ((short)WebEnums.GeographyClass.Key6).ToString());
        this.hdnKey7.Attributes.Add("GeoClassID", ((short)WebEnums.GeographyClass.Key7).ToString());
        this.hdnKey8.Attributes.Add("GeoClassID", ((short)WebEnums.GeographyClass.Key8).ToString());
        this.hdnKey9.Attributes.Add("GeoClassID", ((short)WebEnums.GeographyClass.Key9).ToString());


        foreach (GeographyStructureHdrInfo obj in oGeographyStructureHdrInfoCollection)
        {
            short geographyClassID = obj.GeographyClassID.Value;
            string geography = obj.GeographyStructure;

            switch (geographyClassID)
            {
                case (short)WebEnums.GeographyClass.Key2:
                    this.hdnKey2.Attributes.Add("GeoStruct", geography);
                    break;
                case (short)WebEnums.GeographyClass.Key3:
                    //this.hdnKey3.Attributes.Add("GeoClassID", geographyClassID.ToString());
                    this.hdnKey3.Attributes.Add("GeoStruct", geography);
                    break;
                case (short)WebEnums.GeographyClass.Key4:
                    //this.hdnKey4.Attributes.Add("GeoClassID", geographyClassID.ToString());
                    this.hdnKey4.Attributes.Add("GeoStruct", geography);
                    break;
                case (short)WebEnums.GeographyClass.Key5:
                    //this.hdnKey5.Attributes.Add("GeoClassID", geographyClassID.ToString());
                    this.hdnKey5.Attributes.Add("GeoStruct", geography);
                    break;
                case (short)WebEnums.GeographyClass.Key6:
                    //this.hdnKey6.Attributes.Add("GeoClassID", geographyClassID.ToString());
                    this.hdnKey6.Attributes.Add("GeoStruct", geography);
                    break;
                case (short)WebEnums.GeographyClass.Key7:
                    //this.hdnKey7.Attributes.Add("GeoClassID", geographyClassID.ToString());
                    this.hdnKey7.Attributes.Add("GeoStruct", geography);
                    break;
                case (short)WebEnums.GeographyClass.Key8:
                    //this.hdnKey8.Attributes.Add("GeoClassID", geographyClassID.ToString());
                    this.hdnKey8.Attributes.Add("GeoStruct", geography);
                    break;
                case (short)WebEnums.GeographyClass.Key9:
                    //this.hdnKey9.Attributes.Add("GeoClassID", geographyClassID.ToString());
                    this.hdnKey9.Attributes.Add("GeoStruct", geography);
                    break;
            }
        }
        ListControlHelper.AddListItemForSelectOne(this.ddlHierarchy);

        Helper.EnableDisableOrgHierarchyForNoKey(lblOrgHierarchy, txtHierarchy, ddlHierarchy, btnAddMore);
    }

    public void GetCriteria(Dictionary<string, string> oRptCriteria)
    {
        GetCriteriaIntoDictionary(oRptCriteria, this.hdnKey2);
        GetCriteriaIntoDictionary(oRptCriteria, this.hdnKey3);
        GetCriteriaIntoDictionary(oRptCriteria, this.hdnKey4);
        GetCriteriaIntoDictionary(oRptCriteria, this.hdnKey5);
        GetCriteriaIntoDictionary(oRptCriteria, this.hdnKey6);
        GetCriteriaIntoDictionary(oRptCriteria, this.hdnKey7);
        GetCriteriaIntoDictionary(oRptCriteria, this.hdnKey8);
        GetCriteriaIntoDictionary(oRptCriteria, this.hdnKey9);
    }
    //Function below is not used for now
    private string GetGeoClassNameFromHdnControl(short GeogClassID)
    {
        string GeoClassName = "";
        switch (GeogClassID)
        {
            case (short)WebEnums.GeographyClass.Key2:
                GeoClassName = GeographyClassName.KEY2;
                break;
            case (short)WebEnums.GeographyClass.Key3:
                GeoClassName = GeographyClassName.KEY3;
                break;
            case (short)WebEnums.GeographyClass.Key4:
                GeoClassName = GeographyClassName.KEY4;
                break;
            case (short)WebEnums.GeographyClass.Key5:
                GeoClassName = GeographyClassName.KEY5;
                break;
            case (short)WebEnums.GeographyClass.Key6:
                GeoClassName = GeographyClassName.KEY6;
                break;
            case (short)WebEnums.GeographyClass.Key7:
                GeoClassName = GeographyClassName.KEY7;
                break;
            case (short)WebEnums.GeographyClass.Key8:
                GeoClassName = GeographyClassName.KEY8;
                break;
            case (short)WebEnums.GeographyClass.Key9:
                GeoClassName = GeographyClassName.KEY9;
                break;
        }
        return GeoClassName;
    }

    private void GetCriteriaIntoDictionary(Dictionary<string, string> oRptCriteria, HtmlInputHidden hdnCtrl)
    {
        string criteriaValueforKey = hdnCtrl.Value.Replace(WebConstants.ORGHIERARCHYVALUESEPERATOR, ReportHelper.FilterValueSeparator);
        string keyNameForCriteria = "";
        switch (Convert.ToInt16(hdnCtrl.Attributes["GeoClassID"]))
        {
            case (short)WebEnums.GeographyClass.Key2:
                keyNameForCriteria = ReportCriteriaKeyName.RPTCRITERIAKEYNAME_KEY2;
                break;
            case (short)WebEnums.GeographyClass.Key3:
                keyNameForCriteria = ReportCriteriaKeyName.RPTCRITERIAKEYNAME_KEY3;
                break;
            case (short)WebEnums.GeographyClass.Key4:
                keyNameForCriteria = ReportCriteriaKeyName.RPTCRITERIAKEYNAME_KEY4;
                break;
            case (short)WebEnums.GeographyClass.Key5:
                keyNameForCriteria = ReportCriteriaKeyName.RPTCRITERIAKEYNAME_KEY5;
                break;
            case (short)WebEnums.GeographyClass.Key6:
                keyNameForCriteria = ReportCriteriaKeyName.RPTCRITERIAKEYNAME_KEY6;
                break;
            case (short)WebEnums.GeographyClass.Key7:
                keyNameForCriteria = ReportCriteriaKeyName.RPTCRITERIAKEYNAME_KEY7;
                break;
            case (short)WebEnums.GeographyClass.Key8:
                keyNameForCriteria = ReportCriteriaKeyName.RPTCRITERIAKEYNAME_KEY8;
                break;
            case (short)WebEnums.GeographyClass.Key9:
                keyNameForCriteria = ReportCriteriaKeyName.RPTCRITERIAKEYNAME_KEY9;
                break;
        }
        oRptCriteria.Add(keyNameForCriteria, criteriaValueforKey);
    }

    public void SetCriteria(Dictionary<string, string> oRptCriteria)
    {
        string keyValue = string.Empty;
        string seperatorToBeReplaced = ReportHelper.FilterValueSeparator;
        if (oRptCriteria.ContainsKey(ReportCriteriaKeyName.RPTCRITERIAKEYNAME_KEY2))
            this.hdnKey2.Value = oRptCriteria[ReportCriteriaKeyName.RPTCRITERIAKEYNAME_KEY2].Replace(seperatorToBeReplaced, WebConstants.ORGHIERARCHYVALUESEPERATOR);
        if (oRptCriteria.ContainsKey(ReportCriteriaKeyName.RPTCRITERIAKEYNAME_KEY2))
            this.hdnKey3.Value = oRptCriteria[ReportCriteriaKeyName.RPTCRITERIAKEYNAME_KEY3].Replace(seperatorToBeReplaced, WebConstants.ORGHIERARCHYVALUESEPERATOR);
        if (oRptCriteria.ContainsKey(ReportCriteriaKeyName.RPTCRITERIAKEYNAME_KEY2))
            this.hdnKey4.Value = oRptCriteria[ReportCriteriaKeyName.RPTCRITERIAKEYNAME_KEY4].Replace(seperatorToBeReplaced, WebConstants.ORGHIERARCHYVALUESEPERATOR);
        if (oRptCriteria.ContainsKey(ReportCriteriaKeyName.RPTCRITERIAKEYNAME_KEY2))
            this.hdnKey5.Value = oRptCriteria[ReportCriteriaKeyName.RPTCRITERIAKEYNAME_KEY5].Replace(seperatorToBeReplaced, WebConstants.ORGHIERARCHYVALUESEPERATOR);
        if (oRptCriteria.ContainsKey(ReportCriteriaKeyName.RPTCRITERIAKEYNAME_KEY2))
            this.hdnKey6.Value = oRptCriteria[ReportCriteriaKeyName.RPTCRITERIAKEYNAME_KEY6].Replace(seperatorToBeReplaced, WebConstants.ORGHIERARCHYVALUESEPERATOR);
        if (oRptCriteria.ContainsKey(ReportCriteriaKeyName.RPTCRITERIAKEYNAME_KEY2))
            this.hdnKey7.Value = oRptCriteria[ReportCriteriaKeyName.RPTCRITERIAKEYNAME_KEY7].Replace(seperatorToBeReplaced, WebConstants.ORGHIERARCHYVALUESEPERATOR);
        if (oRptCriteria.ContainsKey(ReportCriteriaKeyName.RPTCRITERIAKEYNAME_KEY2))
            this.hdnKey8.Value = oRptCriteria[ReportCriteriaKeyName.RPTCRITERIAKEYNAME_KEY8].Replace(seperatorToBeReplaced, WebConstants.ORGHIERARCHYVALUESEPERATOR);
        if (oRptCriteria.ContainsKey(ReportCriteriaKeyName.RPTCRITERIAKEYNAME_KEY2))
            this.hdnKey9.Value = oRptCriteria[ReportCriteriaKeyName.RPTCRITERIAKEYNAME_KEY9].Replace(seperatorToBeReplaced, WebConstants.ORGHIERARCHYVALUESEPERATOR);

    }
}
