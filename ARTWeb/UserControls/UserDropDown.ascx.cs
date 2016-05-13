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

public partial class UserControls_UserDropDown : System.Web.UI.UserControl
{

    #region Variables & Constants
    #endregion
    #region Properties
    private bool _IsPreparer = false;
    private bool _IsReviewer = false;
    private bool _IsApprover = false;
    private bool _IsBackupPreparer = false;
    private bool _IsBackupReviewer = false;
    private bool _IsBackupApprover = false;
    private string _SelectedValue = string.Empty;
    Unit _Width;

    public Unit Width
    {
        set
        {
            _Width = value;
        }
    }

    public bool IsPreparer
    {
        get
        {
            return this._IsPreparer;
        }
        set
        {
            this._IsPreparer = value;
            if (this._IsPreparer == true)
            {
                this._IsApprover = false;
                this._IsReviewer = false;
                this._IsBackupPreparer = false;
                this._IsBackupReviewer = false;
                this._IsBackupApprover = false;
                this.vldUser.ErrorMessage = LanguageUtil.GetValue(5000058);
                this.vldUserState.ErrorMessage = LanguageUtil.GetValue(5000345);
            }
        }
    }

    public bool IsReviewer
    {
        get
        {
            return this._IsReviewer;
        }
        set
        {
            this._IsReviewer = value;
            if (this._IsReviewer == true)
            {
                this._IsApprover = false;
                this._IsPreparer = false;
                this._IsBackupPreparer = false;
                this._IsBackupReviewer = false;
                this._IsBackupApprover = false;
                this.vldUser.ErrorMessage = LanguageUtil.GetValue(5000059);
                this.vldUserState.ErrorMessage = LanguageUtil.GetValue(5000345);
            }
        }
    }

    public bool IsApprover
    {
        get
        {
            return this._IsApprover;
        }
        set
        {
            this._IsApprover = value;
            if (this._IsApprover == true)
            {
                this._IsPreparer = false;
                this._IsReviewer = false;
                this._IsBackupPreparer = false;
                this._IsBackupReviewer = false;
                this._IsBackupApprover = false;
                this.vldUser.ErrorMessage = LanguageUtil.GetValue(5000060);
                this.vldUserState.ErrorMessage = LanguageUtil.GetValue(5000345);
            }
        }
    }


    public bool IsBackupPreparer
    {
        get
        {
            return this._IsBackupPreparer;
        }
        set
        {
            this._IsBackupPreparer = value;
            if (this._IsBackupPreparer == true)
            {
                this._IsApprover = false;
                this._IsReviewer = false;
                this._IsPreparer = false;
                this._IsBackupReviewer = false;
                this._IsBackupApprover = false;
                this.vldUser.ErrorMessage = LanguageUtil.GetValue(5000333);
                this.vldUserState.ErrorMessage = LanguageUtil.GetValue(5000345);
            }
        }
    }

    public bool IsBackupReviewer
    {
        get
        {
            return this._IsBackupReviewer;
        }
        set
        {
            this._IsBackupReviewer = value;
            if (this._IsBackupReviewer == true)
            {
                this._IsApprover = false;
                this._IsReviewer = false;
                this._IsPreparer = false;
                this._IsBackupPreparer = false;
                this._IsBackupApprover = false;
                this.vldUser.ErrorMessage = LanguageUtil.GetValue(5000334);
                this.vldUserState.ErrorMessage = LanguageUtil.GetValue(5000345);
            }
        }
    }

    public bool IsBackupApprover
    {
        get
        {
            return this._IsBackupApprover;
        }
        set
        {
            this._IsBackupApprover = value;
            if (this._IsBackupApprover == true)
            {
                this._IsApprover = false;
                this._IsReviewer = false;
                this._IsPreparer = false;
                this._IsBackupPreparer = false;
                this._IsBackupReviewer = false;
                this.vldUser.ErrorMessage = LanguageUtil.GetValue(5000335);
                this.vldUserState.ErrorMessage = LanguageUtil.GetValue(5000345);
            }
        }
    }

    public string SelectedValue
    {
        get
        {
            return this.ddlUser.SelectedValue;
        }
        set
        {
            this._SelectedValue = value;

            if (!string.IsNullOrEmpty(this._SelectedValue))
            {
                ListItem oListItem = ddlUser.Items.FindByValue(this._SelectedValue);
                if (oListItem != null)
                {
                    ddlUser.SelectedItem.Selected = false;
                    oListItem.Selected = true;
                }
            }
        }
    }

    public ListItem SelectedItem
    {
        get
        {
            return this.ddlUser.SelectedItem;
        }
    }

    public int SelectedIndex
    {
        get
        {
            return this.ddlUser.SelectedIndex;
        }
        set
        {
            this.ddlUser.SelectedIndex = value;
        }
    }

    public ListItemCollection Items
    {
        get
        {
            return this.ddlUser.Items;
        }
    }

    public bool Enabled
    {
        get
        {
            return this.ddlUser.Enabled;
        }
        set
        {
            this.ddlUser.Enabled = value;
        }
    }


    public override string ClientID
    {
        get
        {
            return this.ddlUser.ClientID;
        }
    }

    public bool ValidatorEnable
    {
        get
        {
            return this.vldUser.Enabled;
        }
        set
        {
            if (Enabled)
            {
                this.vldUser.Enabled = value;
            }
        }
    }

    public KeyValuePair<string, string> AddAttributes
    {
        set
        {
            this.ddlUser.Attributes.Add(value.Key, value.Value);
        }
    }

    public string ValidatorClientID
    {
        get
        {
            return this.vldUser.ClientID;
        }
    }

    public DropDownList DropDownListUser
    {
        get { return ddlUser; }
    }

    public string ValidationsGroup
    {
        set { ddlUser.ValidationGroup = value; }
    }
    #endregion
    #region Delegates & Events
    public delegate void DropDownSelectionDelegate(object sender, SelectionChangedEventArgs e);
    protected event DropDownSelectionDelegate _drp1selectionChanged;
    public event DropDownSelectionDelegate DropDownSelectionChanged
    {
        add
        {
            this._drp1selectionChanged += value;
        }
        remove
        {
            this._drp1selectionChanged -= value;
        }
    }
    #endregion
    #region Page Events
    protected void Page_Load(object sender, EventArgs e)
    {
        ddlUser.Width = _Width;
        try
        {
            if (IsPostBack)
            {
                this._SelectedValue = ddlUser.SelectedValue;
            }

            List<UserHdrInfo> oUserHdrInfoCollection = null;

            if (this._IsBackupPreparer == true)
            {
                oUserHdrInfoCollection = CacheHelper.SelectAllBackupPreparersForCurrentCompany();
            }
            else if (this._IsBackupReviewer == true)
            {
                oUserHdrInfoCollection = CacheHelper.SelectAllBackupReviewersForCurrentCompany();
            }
            else if (this._IsBackupApprover == true)
            {
                oUserHdrInfoCollection = CacheHelper.SelectAllBackupApproversForCurrentCompany();
            }
            else if (this._IsApprover == true)
            {
                oUserHdrInfoCollection = CacheHelper.SelectAllApproversForCurrentCompany();
            }
            else if (this._IsReviewer == true)
            {
                oUserHdrInfoCollection = CacheHelper.SelectAllReviewersForCurrentCompany();
            }

            else
            {
                oUserHdrInfoCollection = CacheHelper.SelectAllPreparersForCurrentCompany();
            }

            if (oUserHdrInfoCollection == null)
            {
                oUserHdrInfoCollection = new List<UserHdrInfo>();
            }

            UserHdrInfo oUserHdrInfo = new UserHdrInfo();
            oUserHdrInfo.Name = LanguageUtil.GetValue(1343);
            oUserHdrInfo.UserID = Convert.ToInt32(WebConstants.SELECT_ONE);
            oUserHdrInfoCollection.Insert(0, oUserHdrInfo);

            ddlUser.DataSource = oUserHdrInfoCollection;
            ddlUser.DataTextField = "Name";
            ddlUser.DataValueField = "UserID";
            ddlUser.DataBind();
            this.OnDropDownUserSelectionChanged();

            if (!string.IsNullOrEmpty(this._SelectedValue))
                ddlUser.SelectedValue = this._SelectedValue;
            else
                ddlUser.SelectedValue = WebConstants.SELECT_ONE;

            vldUser.InitialValue = WebConstants.SELECT_ONE;

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
    #endregion
    #region Grid Events
    #endregion
    #region Other Events
    protected void ddlUser_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.OnDropDownUserSelectionChanged();
    }
    #endregion
    #region Validation Control Events
    #endregion
    #region Private Methods
    #endregion
    #region Other Methods
    public virtual void OnDropDownUserSelectionChanged()
    {
        SelectionChangedEventArgs evt = new SelectionChangedEventArgs(this.ddlUser);
        if (this._drp1selectionChanged != null)
            this._drp1selectionChanged(this, evt);
    }

    #endregion

}
