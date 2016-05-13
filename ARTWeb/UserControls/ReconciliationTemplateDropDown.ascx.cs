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
using SkyStem.ART.Web.Data;
using SkyStem.ART.Client.Model;
using SkyStem.Language.LanguageUtility;
using SkyStem.ART.Web.Classes;

public partial class UserControls_ReconciliationTemplateDropDown : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string selectedValue = WebConstants.SELECT_ONE;
            if (IsPostBack)
                selectedValue = ddlReconciliationTemplate.SelectedValue;

            List<ReconciliationTemplateMstInfo> oReconciliationTemplateMstInfo = SessionHelper.SelectAllReconciliationTemplateMstInfoWithDisplayText();
            oReconciliationTemplateMstInfo.Insert(0, new ReconciliationTemplateMstInfo { Name = LanguageUtil.GetValue(1343), ReconciliationTemplateID = Convert.ToInt16(WebConstants.SELECT_ONE) });

            ddlReconciliationTemplate.DataSource = oReconciliationTemplateMstInfo;
            ddlReconciliationTemplate.DataTextField = "Name";
            ddlReconciliationTemplate.DataValueField = "ReconciliationTemplateID";
            ddlReconciliationTemplate.DataBind();
            //Helper.AddListItemSelect(ddlReconciliationTemplate);

            if (!string.IsNullOrEmpty(selectedValue) && Convert.ToInt32(selectedValue) > 0)
                ddlReconciliationTemplate.SelectedValue = selectedValue;

            //SessionHelper.ClearSession(SessionConstants.ALL_RECONCILIATION_TEMPLATE_MST_INFO);
            //CacheHelper.ClearCache(CacheConstants.ALL_RECONCILIATION_TEMPLATE_MST_INFO);

            vldReconciliationTemplate.InitialValue = WebConstants.SELECT_ONE;
            vldReconciliationTemplate.ErrorMessage = LanguageUtil.GetValue(5000050);
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
            return this.ddlReconciliationTemplate.SelectedValue;
        }
        set
        {
            this.ddlReconciliationTemplate.SelectedValue = value;
        }
    }

    public ListItem SelectedItem
    {
        get
        {
            return this.ddlReconciliationTemplate.SelectedItem;
        }
    }

    public int SelectedIndex
    {
        get
        {
            return this.ddlReconciliationTemplate.SelectedIndex;
        }
        set
        {
            this.ddlReconciliationTemplate.SelectedIndex = value;
        }
    }

    public ListItemCollection Items
    {
        get
        {
            return this.ddlReconciliationTemplate.Items;
        }
    }

    public bool Enabled
    {
        get
        {
            return this.ddlReconciliationTemplate.Enabled;
        }
        set
        {
            this.ddlReconciliationTemplate.Enabled = value;
        }
    }

    public override string ClientID
    {
        get
        {
            return this.ddlReconciliationTemplate.ClientID;
        }
    }

    public DropDownList DropDown
    {
        get
        {
            return this.ddlReconciliationTemplate;
        }
    }

    public bool ValidatorEnable
    {
        get
        {
            return this.vldReconciliationTemplate.Enabled;
        }
        set
        {
            this.vldReconciliationTemplate.Enabled = value;
        }
    }

    public KeyValuePair<string, string> AddAttributes
    {        
        set
        {
            this.ddlReconciliationTemplate.Attributes.Add(value.Key, value.Value);
        }
    }
    public RequiredFieldValidator ReqFldValidator
    {
        get
        {
            return this.vldReconciliationTemplate;
        }
    }
}
