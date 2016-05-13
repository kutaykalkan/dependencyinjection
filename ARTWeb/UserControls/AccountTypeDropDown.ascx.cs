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
using SkyStem.ART.Web.Utility;
using SkyStem.ART.Client.Model;
using System.Collections.Generic;
using SkyStem.Language.LanguageUtility;
using SkyStem.ART.Web.Data;
using SkyStem.ART.Web.Classes;

public partial class UserControls_AccountTypeDropDown : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            List<AccountTypeMstInfo> oAccountTypeMstInfoCollection = SessionHelper.SelectAllAccountTypeMstInfoWithDisplayText();
            oAccountTypeMstInfoCollection.Insert(0, new AccountTypeMstInfo { Name = LanguageUtil.GetValue(1343), AccountTypeID = Convert.ToInt16(WebConstants.SELECT_ONE) });

            string selectedValue = WebConstants.SELECT_ONE;
            if (IsPostBack)
                selectedValue = ddlAccountType.SelectedValue;

            ddlAccountType.DataSource = oAccountTypeMstInfoCollection;
            ddlAccountType.DataTextField = "Name";
            ddlAccountType.DataValueField = "AccountTypeID";
            ddlAccountType.DataBind();
            //Helper.AddListItemSelect(ddlAccountType);

            if (!string.IsNullOrEmpty(selectedValue) && Convert.ToInt32(selectedValue) > 0)
                ddlAccountType.SelectedValue = selectedValue;

            //SessionHelper.ClearSession(SessionConstants.ALL_ACCOUNT_TYPE_MST_INFO);
            //CacheHelper.ClearCache(CacheConstants.ALL_ACCOUNT_TYPE_MST_INFO);

            vldAccountType.ErrorMessage = LanguageUtil.GetValue(5000056);
            vldAccountType.InitialValue = WebConstants.SELECT_ONE;
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
            return this.ddlAccountType.SelectedValue;
        }
        set
        {
            this.ddlAccountType.SelectedValue = value;
        }
    }

    public ListItem SelectedItem
    {
        get
        {
            return this.ddlAccountType.SelectedItem;
        }
    }

    public int SelectedIndex
    {
        get
        {
            return this.ddlAccountType.SelectedIndex;
        }
        set
        {
            this.ddlAccountType.SelectedIndex = value;
        }
    }

    public ListItemCollection Items
    {
        get
        {
            return this.ddlAccountType.Items;
        }
    }

    public bool Enabled
    {
        get
        {
            return this.ddlAccountType.Enabled;
        }
        set
        {
            this.ddlAccountType.Enabled = value;
        }
    }

    public override string ClientID
    {
        get
        {
            return this.ddlAccountType.ClientID;
        }
    }

    public bool ValidatorEnable
    {
        get
        {
            return this.vldAccountType.Enabled;
        }
        set
        {
            if (Enabled)
            {
                this.vldAccountType.Enabled = value;
            }
        }
    }
}
