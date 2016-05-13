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

using SkyStem.ART.Client.IServices;
using SkyStem.ART.Client.Model;
using SkyStem.ART.Web.Utility;

using SkyStem.Library.Controls.WebControls;
using System.Collections.Generic;
using SkyStem.ART.Client.Model.Base;
using SkyStem.ART.Web.Data;
using SkyStem.ART.Client.Exception;
using SkyStem.ART.Web.Classes;
using SkyStem.Language.LanguageUtility;
using SkyStem.ART.Client.Data;

public partial class UserControls_CapabilityMultiCurrency : UserControlBase
{
    #region Variables & Constants
    IUtility oUtilityClient;
    int _ReopenRecOnCCYReloadID = 0;
    int _companyID;
    bool? _isMultiCurrencyYesChecked = null;
    bool? _isMultiCurrencyForwarded = null;
    bool? _isReopenRecOnCCYReloadForwarded = null;
    bool? _isMultiCurrencyYesCheckedCurrent = null;
    CompanyCapabilityInfo _CurrencyCapabilityInfo = null;
    #endregion
    #region Properties
    public bool? IsMultiCurrencyYesChecked
    {
        get { return _isMultiCurrencyYesChecked; }
        set { _isMultiCurrencyYesChecked = value; }
    }
    public bool? IsMultiCurrencyForwarded
    {
        get { return _isMultiCurrencyForwarded; }
        set { _isMultiCurrencyForwarded = value; }
    }
    public bool? IsReopenRecOnCCYReloadForwarded
    {
        get { return _isReopenRecOnCCYReloadForwarded; }
        set { _isReopenRecOnCCYReloadForwarded = value; }
    }
    public bool? IsMultiCurrencyYesCheckedCurrent
    {
        get
        {
            _isMultiCurrencyYesCheckedCurrent = (bool?)ViewState["_isMultiCurrencyYesCheckedCurrent"];
            return _isMultiCurrencyYesCheckedCurrent;
        }
        set
        {
            _isMultiCurrencyYesCheckedCurrent = value;
            ViewState["_isMultiCurrencyYesCheckedCurrent"] = value;
        }
    }

    public CompanyCapabilityInfo CurrencyCapabilityInfo
    {
        get
        {
            if (_CurrencyCapabilityInfo == null)
                _CurrencyCapabilityInfo = (CompanyCapabilityInfo)ViewState["_CurrencyCapabilityInfo"];
            return _CurrencyCapabilityInfo;
        }
        set
        {
            _CurrencyCapabilityInfo = value;
            ViewState["_CurrencyCapabilityInfo"] = value;
        }
    }

    public bool IsPartialEditMode { get; set; }
    #endregion
    #region Delegates & Events
    public event EventHandler MultiCurrencySelectionChanged;
    #endregion
    #region Page Events
    protected void Page_Init(object sender, EventArgs e)
    {
        SetErrorMessages();
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            _companyID = SessionHelper.CurrentCompanyID.Value;
            oUtilityClient = RemotingHelper.GetUtilityObject();
            if (!IsPostBack)
            {
                Helper.ChangeCssClassOfTDStatus(tdCapabilityStatus, IsMultiCurrencyForwarded);

                Helper.SetCarryforwardedStatus(imgStatusMultiCurrencyForwardYes, imgStatusMultiCurrencyForwardNo, IsMultiCurrencyForwarded);
                Helper.SetYesNoRadioButtons(OptMultiCurrencyYes, OptMultiCurrencyNo, IsMultiCurrencyYesChecked);
                if (IsMultiCurrencyYesChecked == true)
                {
                    BindAfterYesNoSelection();
                }
                ShowHideForRadioButtonYesNoChecked();
            }
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
    #region Other Events
    protected void OptMultiCurrencyYes_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            ShowHideForRadioButtonYesNoChecked();
            BindAfterYesNoSelection();
            Helper.SetCarryforwardedStatus(imgStatusReopenRecOnCCYReloadForwardYes, imgStatusReopenRecOnCCYReloadForwardNo, IsReopenRecOnCCYReloadForwarded);
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
    protected void OptMultiCurrencyNo_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            ShowHideForRadioButtonYesNoChecked();
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
    #region Private Methods
    private void SetErrorMessages()
    {
        this.rfvReopenRecOnCCYReload.ErrorMessage = Helper.GetErrorMessage(WebEnums.FieldType.MandatoryField, this.lblReopenRecOnCCYReload.LabelID);
    }
    #endregion
    #region Other Methods
    protected void ShowHideForRadioButtonYesNoChecked()
    {
        if (OptMultiCurrencyYes.Checked == true && OptMultiCurrencyNo.Checked == false)
        {
            pnlContentMultiCurrency.Visible = true;
            pnlContent.Visible = true;
            IsMultiCurrencyYesCheckedCurrent = true;
            imgCollapse.Style[HtmlTextWriterStyle.Display] = "block";
        }
        else if (OptMultiCurrencyYes.Checked == false && OptMultiCurrencyNo.Checked == true)
        {
            pnlContentMultiCurrency.Visible = false;
            pnlContent.Visible = false;
            IsMultiCurrencyYesCheckedCurrent = false;
            imgCollapse.Style[HtmlTextWriterStyle.Display] = "none";
            pnlContent.CssClass = "";
            pnlYesNo.CssClass = "";
        }
        else if (OptMultiCurrencyYes.Checked == false && OptMultiCurrencyNo.Checked == false)
        {
            pnlContentMultiCurrency.Visible = false;
            pnlContent.Visible = false;
            IsMultiCurrencyYesCheckedCurrent = null;
            imgCollapse.Style[HtmlTextWriterStyle.Display] = "none";
            pnlContent.CssClass = "";
            pnlYesNo.CssClass = "";
        }
        if (MultiCurrencySelectionChanged != null)
            MultiCurrencySelectionChanged.Invoke(this, null);
    }
    protected void BindAfterYesNoSelection()
    {
        if (_CurrencyCapabilityInfo != null)
        {
            List<CapabilityAttributeValueInfo> oCapabilityAttributeValueInfoList = _CurrencyCapabilityInfo.CapabilityAttributeValueInfoList;
            if (oCapabilityAttributeValueInfoList != null && oCapabilityAttributeValueInfoList.Count > 0)
            {
                CapabilityAttributeValueInfo oCapabilityAttributeValueInfo = oCapabilityAttributeValueInfoList.FirstOrDefault(T => T.CapabilityAttributeID == (int)ARTEnums.CapabilityAttribute.MultiCurrencyReopenRecOnCCYReload);
                if (oCapabilityAttributeValueInfo != null && oCapabilityAttributeValueInfo.ReferenceID.HasValue)
                {
                    _ReopenRecOnCCYReloadID = oCapabilityAttributeValueInfo.ReferenceID.Value;
                    IsReopenRecOnCCYReloadForwarded = oCapabilityAttributeValueInfo.IsCarryForwardedFromPreviousRecPeriod.Value;
                }
            }
        }
        Helper.SetCarryforwardedStatus(imgStatusReopenRecOnCCYReloadForwardYes, imgStatusReopenRecOnCCYReloadForwardNo, IsReopenRecOnCCYReloadForwarded);
        ListControlHelper.BindYesNoDropdown(ddlReopenRecOnCCYReload, true);
        if (_ReopenRecOnCCYReloadID != 0)
        {
            ListItem oItem = ddlReopenRecOnCCYReload.Items.FindByValue(_ReopenRecOnCCYReloadID.ToString());
            if (oItem != null)
                oItem.Selected = true;
        }
    }
    public void ChangedEventHandler()
    {
        oUtilityClient = RemotingHelper.GetUtilityObject();
        Helper.ChangeCssClassOfTDStatus(tdCapabilityStatus, IsMultiCurrencyForwarded);
        Helper.SetCarryforwardedStatus(imgStatusMultiCurrencyForwardYes, imgStatusMultiCurrencyForwardNo, IsMultiCurrencyForwarded);
        Helper.SetCarryforwardedStatus(imgStatusReopenRecOnCCYReloadForwardYes, imgStatusReopenRecOnCCYReloadForwardNo, IsReopenRecOnCCYReloadForwarded);
        Helper.SetYesNoRadioButtons(OptMultiCurrencyYes, OptMultiCurrencyNo, IsMultiCurrencyYesChecked);
        BindAfterYesNoSelection();
        ShowHideForRadioButtonYesNoChecked();
    }
    public CompanyCapabilityInfo GetCompanyCapabilityInfoToSave()
    {
        CompanyCapabilityInfo oCompanyCapabilityInfo = null;
        oCompanyCapabilityInfo = new CompanyCapabilityInfo();
        oCompanyCapabilityInfo.CompanyID = _companyID;
        oCompanyCapabilityInfo.AddedBy = SessionHelper.CurrentUserLoginID;
        oCompanyCapabilityInfo.DateAdded = DateTime.Now;
        oCompanyCapabilityInfo.CapabilityID = (int)ARTEnums.Capability.MultiCurrency;
        oCompanyCapabilityInfo.IsActivated = IsMultiCurrencyYesCheckedCurrent;
        oCompanyCapabilityInfo.CapabilityAttributeValueInfoList = new List<CapabilityAttributeValueInfo>();
        CapabilityAttributeValueInfo oCapabilityAttributeValueInfo = new CapabilityAttributeValueInfo();
        oCapabilityAttributeValueInfo.CapabilityID = (int)ARTEnums.Capability.MultiCurrency;
        oCapabilityAttributeValueInfo.CapabilityAttributeID = (int)ARTEnums.CapabilityAttribute.MultiCurrencyReopenRecOnCCYReload;
        if (OptMultiCurrencyYes.Checked == true)
        {
            if (ddlReopenRecOnCCYReload.SelectedValue != WebConstants.SELECT_ONE)
            {
                oCapabilityAttributeValueInfo.ReferenceID = Convert.ToInt16(ddlReopenRecOnCCYReload.SelectedValue);
            }
        }
        else
        {
            oCapabilityAttributeValueInfo.ReferenceID = null;
        }
        oCompanyCapabilityInfo.CapabilityAttributeValueInfoList.Add(oCapabilityAttributeValueInfo);
        return oCompanyCapabilityInfo;
    }
    public bool IsValueChanged()
    {
        return Helper.IsCapabilityChanged(_CurrencyCapabilityInfo, GetCompanyCapabilityInfoToSave());
    }
    #endregion

}//end of class
