using System;
using System.Collections;
using System.Collections.Generic;
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
using SkyStem.ART.Client.IServices;
using SkyStem.ART.Web.Data;
using SkyStem.ART.Client.Model;
using SkyStem.Language.LanguageUtility;
using SkyStem.ART.Web.Classes;

public partial class UserControls_SubledgerSourceDropDown : System.Web.UI.UserControl
{
    Unit _Width ;
    public Unit Width
    {
        set
        {
            _Width = value;
        }
    }
    
    protected void Page_Load(object sender, EventArgs e)
    {
        ddlSubledgerSource.Width = _Width;
        try
        {
            string selectedValue = WebConstants.SELECT_ONE;
            if (IsPostBack)
                selectedValue = ddlSubledgerSource.SelectedValue;

            List<SubledgerSourceInfo> oSubledgerSourceInfoCollection = SessionHelper.GetAllSubLedgerSources().FindAll(o => o.SubledgerSourceID.HasValue == true);
            oSubledgerSourceInfoCollection.Insert(0, new SubledgerSourceInfo { Name = LanguageUtil.GetValue(1343), SubledgerSourceID = Convert.ToInt32(WebConstants.SELECT_ONE) });
            ddlSubledgerSource.DataSource = oSubledgerSourceInfoCollection;
            ddlSubledgerSource.DataTextField = "Name";
            ddlSubledgerSource.DataValueField = "SubledgerSourceID";
            ddlSubledgerSource.DataBind();
            //Helper.AddListItemSelect(ddlSubledgerSource);
            //ListControlHelper.BindDropdown(ddlSubledgerSource, oSubledgerSourceInfoCollection, "Name", "SubledgerSourceID");
            //ListControlHelper.AddListItemForSelectOne(ddlSubledgerSource);
            if (!string.IsNullOrEmpty(selectedValue) && Convert.ToInt32(selectedValue) > 0)
                ddlSubledgerSource.SelectedValue = selectedValue;

            vldSubledgerSource.InitialValue = WebConstants.SELECT_ONE;
            vldSubledgerSource.ErrorMessage = LanguageUtil.GetValue(5000065);
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
            return this.ddlSubledgerSource.SelectedValue;
        }
        set
        {
            this.ddlSubledgerSource.SelectedValue = value;
        }
    }

    public ListItem SelectedItem
    {
        get
        {
            return this.ddlSubledgerSource.SelectedItem;
        }
    }

    public int SelectedIndex
    {
        get
        {
            return this.ddlSubledgerSource.SelectedIndex;
        }
        set
        {
            this.ddlSubledgerSource.SelectedIndex = value;
        }
    }

    public ListItemCollection Items
    {
        get
        {
            return this.ddlSubledgerSource.Items;
        }
    }

    public bool Enabled
    {
        get
        {
            return this.ddlSubledgerSource.Enabled;
        }
        set
        {
            this.ddlSubledgerSource.Enabled = value;
        }
    }

    public override string ClientID
    {
        get
        {
            return this.ddlSubledgerSource.ClientID;
        }
    }

    public DropDownList DropDown
    {
        get
        {
            return this.ddlSubledgerSource;
        }
    }

    public string ValidatorClientID
    {
        get
        {
            return this.vldSubledgerSource.ClientID;
        }
    }
    public RequiredFieldValidator ReqFldValidator
    {
        get
        {
            return this.vldSubledgerSource ;
        }
    }
}
