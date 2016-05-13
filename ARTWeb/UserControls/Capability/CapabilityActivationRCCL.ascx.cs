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

public partial class UserControls_CapabilityActivationRCCL : UserControlBase
{
    #region Variables & Constants
    IUtility oUtilityClient;
    int _RCCLValidationTypeID = 0;
    int _companyID;
    bool? _isRCCLYesChecked = null;
    bool? _isRCCLForwarded = null;
    bool? _isRCCLValidationTypeForwarded = null;
    bool? _isRCCLYesCheckedCurrent = null;
    #endregion
    #region Properties
    public bool? IsRCCLYesChecked
    {
        get { return _isRCCLYesChecked; }
        set { _isRCCLYesChecked = value; }
    }
    public bool? IsRCCLForwarded
    {
        get { return _isRCCLForwarded; }
        set { _isRCCLForwarded = value; }
    }
    public bool? IsRCCLValidationTypeForwarded
    {
        get { return _isRCCLValidationTypeForwarded; }
        set { _isRCCLValidationTypeForwarded = value; }
    }
    public bool? IsRCCLYesCheckedCurrent
    {
        get
        {
            _isRCCLYesCheckedCurrent = (bool?)ViewState[ViewStateConstants.Is_RCCL_YES_CHECKED_CURRENT];
            return _isRCCLYesCheckedCurrent;
        }
        set
        {
            _isRCCLYesCheckedCurrent = value;
            ViewState[ViewStateConstants.Is_RCCL_YES_CHECKED_CURRENT] = value;
        }
    }
    public bool IsPartialEditMode { get; set; }
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
                Helper.ChangeCssClassOfTDStatus(tdCapabilityStatus, IsRCCLForwarded);

                Helper.SetCarryforwardedStatus(imgStatusRCCLForwardYes, imgStatusRCCLForwardNo, IsRCCLForwarded);
                Helper.SetYesNoRadioButtons(optRCCLYes, optRCCLNo, IsRCCLYesChecked);
                if (IsRCCLYesChecked.HasValue && IsRCCLYesChecked.Value)
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
    protected void optRCCLYes_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            ShowHideForRadioButtonYesNoChecked();
            BindAfterYesNoSelection();
            Helper.SetCarryforwardedStatus(imgStatusRCCValidationTypeForwardYes, imgStatusRCCValidationTypeForwardNo, IsRCCLValidationTypeForwarded);
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
    protected void optRCCLNo_CheckedChanged(object sender, EventArgs e)
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
        // this.rfvRCCLValidationType.ErrorMessage = Helper.GetErrorMessage(WebEnums.FieldType.MandatoryField, this.lblRCCLValidationType.LabelID);
    }
    #endregion
    #region Other Methods
    protected void ShowHideForRadioButtonYesNoChecked()
    {
        if (optRCCLYes.Checked == true && optRCCLNo.Checked == false)
        {
            pnlContentRCCL.Visible = true;
            pnlContent.Visible = true;
            IsRCCLYesCheckedCurrent = true;
            imgCollapse.Style[HtmlTextWriterStyle.Display] = "block";
        }
        else if (optRCCLYes.Checked == false && optRCCLNo.Checked == true)
        {
            pnlContentRCCL.Visible = false;
            pnlContent.Visible = false;
            IsRCCLYesCheckedCurrent = false;
            imgCollapse.Style[HtmlTextWriterStyle.Display] = "none";
            pnlContent.CssClass = "";
            pnlYesNo.CssClass = "";
        }
        else if (optRCCLYes.Checked == false && optRCCLNo.Checked == false)
        {
            pnlContentRCCL.Visible = false;
            pnlContent.Visible = false;
            IsRCCLYesCheckedCurrent = null;
            imgCollapse.Style[HtmlTextWriterStyle.Display] = "none";
            pnlContent.CssClass = "";
            pnlYesNo.CssClass = "";
        }
    }
    protected void BindAfterYesNoSelection()
    {
        IList<CompanySettingInfo> oCompanySettingInfoCollection = new List<CompanySettingInfo>();
        ICompany oCompanyClient = RemotingHelper.GetCompanyObject();
        oCompanySettingInfoCollection = oCompanyClient.SelectCompanyRCCLValidationType(SessionHelper.CurrentReconciliationPeriodID, Helper.GetAppUserInfo());
        if (oCompanySettingInfoCollection != null && oCompanySettingInfoCollection.Count > 0 && oCompanySettingInfoCollection[0].RCCValidationTypeID.HasValue)
        {
            _RCCLValidationTypeID = oCompanySettingInfoCollection[0].RCCValidationTypeID.Value;
            ViewState[ViewStateConstants.RCCL_VALIDATION_TYPE_CURRENT_DB_VAL] = _RCCLValidationTypeID;
            if (oCompanySettingInfoCollection[0].IsRCCValidationCarryForwardedFromPreviousRecPeriod.HasValue)
                IsRCCLValidationTypeForwarded = oCompanySettingInfoCollection[0].IsRCCValidationCarryForwardedFromPreviousRecPeriod.Value;

        }
        Helper.SetCarryforwardedStatus(imgStatusRCCValidationTypeForwardYes, imgStatusRCCValidationTypeForwardNo, IsRCCLValidationTypeForwarded);
        ListControlHelper.BindRCCLValidationType(ddlRCCLValidationType, true);
        if (_RCCLValidationTypeID != 0)
        {
            ddlRCCLValidationType.SelectedIndex = ddlRCCLValidationType.Items.IndexOf(ddlRCCLValidationType.Items.FindByValue(_RCCLValidationTypeID.ToString()));
        }
    }
    public void ChangedEventHandler()
    {
        oUtilityClient = RemotingHelper.GetUtilityObject();
        Helper.ChangeCssClassOfTDStatus(tdCapabilityStatus, IsRCCLForwarded);
        Helper.SetCarryforwardedStatus(imgStatusRCCLForwardYes, imgStatusRCCLForwardNo, IsRCCLForwarded);
        Helper.SetCarryforwardedStatus(imgStatusRCCValidationTypeForwardYes, imgStatusRCCValidationTypeForwardNo, IsRCCLValidationTypeForwarded);
        Helper.SetYesNoRadioButtons(optRCCLYes, optRCCLNo, IsRCCLYesChecked);
        BindAfterYesNoSelection();
        ShowHideForRadioButtonYesNoChecked();
    }
    public CompanySettingInfo GetCompanySettingObjectToBeSavedFromUC()
    {
        CompanySettingInfo oCompanySettingInfo = null;
        oCompanySettingInfo = new CompanySettingInfo();
        oCompanySettingInfo.CompanyID = _companyID;
        oCompanySettingInfo.AddedBy = SessionHelper.CurrentUserLoginID;
        oCompanySettingInfo.DateAdded = DateTime.Now;
        if (optRCCLYes.Checked == true)
        {
            if (ddlRCCLValidationType.SelectedValue != WebConstants.SELECT_ONE)
                oCompanySettingInfo.RCCValidationTypeID = Convert.ToInt16(ddlRCCLValidationType.SelectedValue);
            else
                oCompanySettingInfo.RCCValidationTypeID = null;
        }     
        return oCompanySettingInfo;
    }
    public bool IsValueChanged()
    {
        bool IsValueChangeFlag = false;
        string DualLevelReviewTypeCurrentDBVal = string.Empty;
        if (ViewState[ViewStateConstants.RCCL_VALIDATION_TYPE_CURRENT_DB_VAL] != null)
            DualLevelReviewTypeCurrentDBVal = ViewState[ViewStateConstants.RCCL_VALIDATION_TYPE_CURRENT_DB_VAL].ToString();
        if (string.IsNullOrEmpty(DualLevelReviewTypeCurrentDBVal) && ddlRCCLValidationType.Visible && ddlRCCLValidationType.SelectedValue != WebConstants.SELECT_ONE)
            IsValueChangeFlag = true;
        else if (!string.IsNullOrEmpty(DualLevelReviewTypeCurrentDBVal) && ddlRCCLValidationType.Visible  && ddlRCCLValidationType.SelectedValue != DualLevelReviewTypeCurrentDBVal)
            IsValueChangeFlag = true;
        return IsValueChangeFlag;
    }
    #endregion

}//end of class
