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
using SkyStem.ART.Client.Model;
using System.Collections.Generic;
using SkyStem.ART.Web.Utility;
using SkyStem.ART.Web.Data;
using SkyStem.Language.LanguageUtility;
using SkyStem.Library.Controls.WebControls;
using SkyStem.ART.Client.Exception;
using SkyStem.ART.Web.Classes;

public partial class UserControls_OrganizationalHierarchyDropdown : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string selectedValue = WebConstants.SELECT_ONE;
            if (IsPostBack)
                selectedValue = ddlGeography.SelectedValue;

            List<GeographyStructureHdrInfo> oGeographyStructureHdrInfoCollection = SessionHelper.GetOrganizationalHierarchy(SessionHelper.CurrentCompanyID);
            oGeographyStructureHdrInfoCollection = oGeographyStructureHdrInfoCollection.Where(a => a.GeographyClassID != (int)WebEnums.GeographyClass.Company).ToList();
            ddlGeography.DataSource = oGeographyStructureHdrInfoCollection;
            ddlGeography.DataTextField = "GeographyStructure";
            ddlGeography.DataValueField = "GeographyClassID";
            ddlGeography.DataBind();

            ListControlHelper.AddListItemForSelectOne(ddlGeography);

            if (!string.IsNullOrEmpty(selectedValue) && Convert.ToInt32(selectedValue) > 0)
                ddlGeography.SelectedValue = selectedValue;

            HideValues();

            if (oGeographyStructureHdrInfoCollection == null
                || oGeographyStructureHdrInfoCollection.Count == 0)
            {
                // Means no Org Hierarchy avilable
                ddlGeography.Enabled = false;
            }
            else
            {
                ddlGeography.Enabled = true;
            }

        }
        catch (ARTException ex)
        {
            ExLabel label = (ExLabel)Page.Master.FindControl("lblErrorMessage");
            Helper.FormatAndShowErrorMessage(label, ex);
        }
        catch (Exception ex)
        {            
            Helper.ShowErrorMessage((PageBase)this.Page, ex);
        }
    }

    public string SelectedValue
    {
        get
        {
            return this.ddlGeography.SelectedValue;
        }
        set
        {
            this.ddlGeography.SelectedValue = value;
        }
    }

    public ListItem SelectedItem
    {
        get
        {
            return this.ddlGeography.SelectedItem;
        }
    }

    public int SelectedIndex
    {
        get
        {
            return this.ddlGeography.SelectedIndex;
        }
        set
        {
            this.ddlGeography.SelectedIndex = value;
        }
    }

    public ListItemCollection Items
    {
        get
        {
            return this.ddlGeography.Items;
        }
    }


    private void HideValues()
    {
        try
        {
            HtmlInputHidden hdnOrganizationalHierarchy = (HtmlInputHidden)this.Parent.FindControl("hdnOrganizationalHierarchy");

            if (hdnOrganizationalHierarchy != null)
            {
                string[] geographyOptionCollection = hdnOrganizationalHierarchy.Value.Split('/');

                if (geographyOptionCollection.Length > 0)
                {
                    for (int index = 0; index < geographyOptionCollection.Length; index++)
                    {
                        string option = geographyOptionCollection[index];
                        if (option != string.Empty)
                        {
                            int result;
                            if (!Int32.TryParse(option, out result))
                            {
                                ListItem item = new ListItem(option, geographyOptionCollection[index + 1]);
                                ddlGeography.Items.Remove(item);
                                ddlGeography.DataSource = ddlGeography.Items;
                                index++;
                            }
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Helper.ShowErrorMessage((PageBase)this.Page, ex);
        }
    }
}
