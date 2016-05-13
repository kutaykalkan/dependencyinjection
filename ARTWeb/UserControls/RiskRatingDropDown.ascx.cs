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

public partial class UserControls_RiskRatingDropDown : System.Web.UI.UserControl
{
    // Class variables
    private int _companyID = SessionHelper.CurrentCompanyID.Value;
    private ExLabel _ErrorLabel = null;
    private bool _IsUsedOnSearchPage;

    #region Constants

    private const string RISKRATING_DATASOURCE_TEXT_NAME = "Name";
    private const string RISKRATING_DATASOURCE_VALUE_NAME = "RiskRatingID";

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            this._ErrorLabel = (ExLabel)Page.Master.FindControl(WebConstants.LABEL_ERRORMESSAGE_ID);

            string selectedValue = WebConstants.SELECT_ONE;
            if (IsPostBack)
                selectedValue = ddlRiskRating.SelectedValue;

            List<RiskRatingMstInfo> oRiskRatingInfoCollectionFromSession = (List<RiskRatingMstInfo>)SessionHelper.GetAllRiskRating();
            List<RiskRatingMstInfo> oRiskRatingInfoCollection = (List<RiskRatingMstInfo>)Helper.DeepClone(oRiskRatingInfoCollectionFromSession);
            
            if (this._IsUsedOnSearchPage)
            {
                oRiskRatingInfoCollection.Insert(0, new RiskRatingMstInfo { Name = LanguageUtil.GetValue(1262), RiskRatingID = Convert.ToInt16(WebConstants.ALL) });
            }
            else
            {
                oRiskRatingInfoCollection.Insert(0, new RiskRatingMstInfo { Name = LanguageUtil.GetValue(1343), RiskRatingID = Convert.ToInt16(WebConstants.SELECT_ONE) });
            }
            ddlRiskRating.DataSource = oRiskRatingInfoCollection;
            ddlRiskRating.DataTextField = RISKRATING_DATASOURCE_TEXT_NAME;
            ddlRiskRating.DataValueField = RISKRATING_DATASOURCE_VALUE_NAME;
            ddlRiskRating.DataBind();

            //SessionHelper.ClearSession(SessionConstants.ALL_RISKRATING_LIST);
            //CacheHelper.ClearCache(CacheConstants.ALL_RISKRATING_LIST);

            

            if (!string.IsNullOrEmpty(selectedValue) && Convert.ToInt32(selectedValue) > 0)
                ddlRiskRating.SelectedValue = selectedValue;

            vldRiskRating.InitialValue = WebConstants.SELECT_ONE;
            vldRiskRating.ErrorMessage = LanguageUtil.GetValue(5000051);
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
            return this.ddlRiskRating.SelectedValue;
        }
        set
        {
            this.ddlRiskRating.SelectedValue = value;
        }
    }

    public ListItem SelectedItem
    {
        get
        {
            return this.ddlRiskRating.SelectedItem;
        }
    }

    public int SelectedIndex
    {
        get
        {
            return this.ddlRiskRating.SelectedIndex;
        }
        set
        {
            this.ddlRiskRating.SelectedIndex = value;
        }
    }

    public ListItemCollection Items
    {
        get
        {
            return this.ddlRiskRating.Items;
        }
    }

    public bool Enabled
    {
        get
        {
            return this.ddlRiskRating.Enabled;
        }
        set
        {
            this.ddlRiskRating.Enabled = value;
        }
    }

    public override string ClientID
    {
        get
        {
            return this.ddlRiskRating.ClientID;
        }
    }

    public DropDownList DropDown
    {
        get
        {
            return this.ddlRiskRating;
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
            return this.vldRiskRating.Enabled;
        }
        set
        {
            if (Enabled)
            {
                this.vldRiskRating.Enabled = value;
            }
        }
    }

    public RequiredFieldValidator ReqFldValidator
    {
        get
        {
            return this.vldRiskRating;
        }   
    }
}
