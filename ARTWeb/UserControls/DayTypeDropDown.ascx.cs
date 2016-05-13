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
using SkyStem.ART.Client.Exception;
using SkyStem.Library.Controls.WebControls;
using SkyStem.ART.Web.Data;
using SkyStem.ART.Client.Model;
using SkyStem.Language.LanguageUtility;
using SkyStem.ART.Web.Classes;

public partial class UserControls_DayTypeDropDown : System.Web.UI.UserControl
{
    // Class variables
    private int _companyID = SessionHelper.CurrentCompanyID.Value;
    private ExLabel _ErrorLabel = null;
    private bool _IsUsedOnSearchPage;

    #region Constants

    private const string DAYTYPE_DATASOURCE_TEXT_NAME = "DaysType";
    private const string DAYTYPE_DATASOURCE_VALUE_NAME = "DaysTypeID";

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            this._ErrorLabel = (ExLabel)Page.Master.FindControl(WebConstants.LABEL_ERRORMESSAGE_ID);

            string selectedValue = WebConstants.SELECT_ONE;
            if (IsPostBack)
                selectedValue = ddlDayType.SelectedValue;

            List<DaysTypeInfo> oDaysTypeInfoCollectionFromSession = (List<DaysTypeInfo>)SessionHelper.GetAllDaysType();
            List<DaysTypeInfo> oDaysTypeInfoCollection = (List<DaysTypeInfo>)Helper.DeepClone(oDaysTypeInfoCollectionFromSession);

            if (this._IsUsedOnSearchPage)
            {
                oDaysTypeInfoCollection.Insert(0, new DaysTypeInfo { DaysType = LanguageUtil.GetValue(1262), DaysTypeID = Convert.ToInt16(WebConstants.ALL) });
            }
            else
            {
                oDaysTypeInfoCollection.Insert(0, new DaysTypeInfo { DaysType = LanguageUtil.GetValue(1343), DaysTypeID = Convert.ToInt16(WebConstants.SELECT_ONE) });
            }
            ddlDayType.DataSource = oDaysTypeInfoCollection;
            ddlDayType.DataTextField = DAYTYPE_DATASOURCE_TEXT_NAME;
            ddlDayType.DataValueField = DAYTYPE_DATASOURCE_VALUE_NAME;
            ddlDayType.DataBind();

            if (!string.IsNullOrEmpty(selectedValue) && Convert.ToInt32(selectedValue) > 0)
                ddlDayType.SelectedValue = selectedValue;

            vldDayType.InitialValue = WebConstants.SELECT_ONE;
            vldDayType.ErrorMessage = LanguageUtil.GetValue(5000414);
        }
        catch (ARTException ex)
        {
            Helper.ShowErrorMessage((PageBase)this.Page, ex);
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
            return this.ddlDayType.SelectedValue;
        }
        set
        {
            this.ddlDayType.SelectedValue = value;
        }
    }

    public ListItem SelectedItem
    {
        get
        {
            return this.ddlDayType.SelectedItem;
        }
    }

    public int SelectedIndex
    {
        get
        {
            return this.ddlDayType.SelectedIndex;
        }
        set
        {
            this.ddlDayType.SelectedIndex = value;
        }
    }

    public ListItemCollection Items
    {
        get
        {
            return this.ddlDayType.Items;
        }
    }

    public bool Enabled
    {
        get
        {
            return this.ddlDayType.Enabled;
        }
        set
        {
            this.ddlDayType.Enabled = value;
        }
    }

    public override string ClientID
    {
        get
        {
            return this.ddlDayType.ClientID;
        }
    }

    public DropDownList DropDown
    {
        get
        {
            return this.ddlDayType;
        }
    }

    public bool IsUsedOnSearchPage
    {
        set
        {
            this._IsUsedOnSearchPage = value;
        }
    }

    public bool ValidatorEnable
    {
        get
        {
            return this.vldDayType.Enabled;
        }
        set
        {
            if (Enabled)
            {
                this.vldDayType.Enabled = value;
            }
        }
    }

    public RequiredFieldValidator ReqFldValidator
    {
        get
        {
            return this.vldDayType;
        }
    }
}
