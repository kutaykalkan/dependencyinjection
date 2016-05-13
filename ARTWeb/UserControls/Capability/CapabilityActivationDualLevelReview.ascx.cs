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

public partial class UserControls_CapabilityActivationDualLevelReview : UserControlBase
{
    #region Variables & Constants
    IUtility oUtilityClient;
    int _DualLevelReviewTypeID = 0;
    int _companyID;
    bool? _isDualLevelReviewYesChecked = null;
    bool? _isDualLevelReviewForwarded = null;
    bool? _isDualLevelReviewTypeForwarded = null;
    bool? _isDualLevelReviewYesCheckedCurrent = null;
    #endregion   
    #region Properties
    public bool? IsDualLevelReviewYesChecked
    {
        get { return _isDualLevelReviewYesChecked; }
        set { _isDualLevelReviewYesChecked = value; }
    }
    public bool? IsDualLevelReviewForwarded
    {
        get { return _isDualLevelReviewForwarded; }
        set { _isDualLevelReviewForwarded = value; }
    }
    public bool? IsDualLevelReviewTypeForwarded
    {
        get { return _isDualLevelReviewTypeForwarded; }
        set { _isDualLevelReviewTypeForwarded = value; }
    }
    public bool? IsDualLevelReviewYesCheckedCurrent
    {
        get
        {
            _isDualLevelReviewYesCheckedCurrent = (bool?)ViewState["_isDualLevelReviewYesCheckedCurrent"];
            return _isDualLevelReviewYesCheckedCurrent;
        }
        set
        {
            _isDualLevelReviewYesCheckedCurrent = value;
            ViewState["_isDualLevelReviewYesCheckedCurrent"] = value;
        }
    }
    public bool IsPartialEditMode { get; set; }
    #endregion
    #region Delegates & Events
    public event EventHandler DualLevelReviewSelectionChanged;
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
                Helper.ChangeCssClassOfTDStatus(tdCapabilityStatus, IsDualLevelReviewForwarded);

                Helper.SetCarryforwardedStatus(imgStatusDualReviewForwardYes, imgStatusDualReviewForwardNo, IsDualLevelReviewForwarded);
                Helper.SetYesNoRadioButtons(optDualReviewYes, optDualReviewNo, IsDualLevelReviewYesChecked);
                if (IsDualLevelReviewYesChecked == true)
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
    protected void optDualReviewYes_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            ShowHideForRadioButtonYesNoChecked();
            BindAfterYesNoSelection();
            Helper.SetCarryforwardedStatus(imgStatusDualLevelReviewTypeForwardYes, imgStatusDualLevelReviewTypeForwardNo, IsDualLevelReviewTypeForwarded);
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
    protected void optDualReviewNo_CheckedChanged(object sender, EventArgs e)
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
        this.rfvDualLevelReviewType.ErrorMessage = Helper.GetErrorMessage(WebEnums.FieldType.MandatoryField, this.lblDualLevelReviewType.LabelID);
    }
    #endregion
    #region Other Methods
    protected void ShowHideForRadioButtonYesNoChecked()
    {
        if (optDualReviewYes.Checked == true && optDualReviewNo.Checked == false)
        {
            pnlContentDualLevelReview.Visible = true;
            pnlContent.Visible = true;
            IsDualLevelReviewYesCheckedCurrent = true;
            imgCollapse.Style[HtmlTextWriterStyle.Display] = "block";
        }
        else if (optDualReviewYes.Checked == false && optDualReviewNo.Checked == true)
        {
            pnlContentDualLevelReview.Visible = false;
            pnlContent.Visible = false;
            IsDualLevelReviewYesCheckedCurrent = false;
            imgCollapse.Style[HtmlTextWriterStyle.Display] = "none";
            pnlContent.CssClass = "";
            pnlYesNo.CssClass = "";
        }
        else if (optDualReviewYes.Checked == false && optDualReviewNo.Checked == false)
        {
            pnlContentDualLevelReview.Visible = false;
            pnlContent.Visible = false;
            IsDualLevelReviewYesCheckedCurrent = null;
            imgCollapse.Style[HtmlTextWriterStyle.Display] = "none";
            pnlContent.CssClass = "";
            pnlYesNo.CssClass = "";
        }
        if (DualLevelReviewSelectionChanged != null)
            DualLevelReviewSelectionChanged.Invoke(this, null);
    }
    protected void BindAfterYesNoSelection()
    {
        IList<CompanySettingInfo> oCompanySettingInfoCollection = new List<CompanySettingInfo>();
        ICompany oCompanyClient = RemotingHelper.GetCompanyObject();
        oCompanySettingInfoCollection = oCompanyClient.SelectCompanyDualLevelReviewType(SessionHelper.CurrentReconciliationPeriodID, Helper.GetAppUserInfo());
        if (oCompanySettingInfoCollection != null && oCompanySettingInfoCollection.Count > 0 && oCompanySettingInfoCollection[0].DualLevelReviewTypeID.HasValue)
        {
            _DualLevelReviewTypeID = oCompanySettingInfoCollection[0].DualLevelReviewTypeID.Value;
            ViewState[ViewStateConstants.DUAL_LEVEL_REVIEW_TYPE_CURRENT_DB_VAL] = _DualLevelReviewTypeID;
            IsDualLevelReviewTypeForwarded = oCompanySettingInfoCollection[0].IsCarryForwardedFromPreviousRecPeriod.Value;
            Helper.SetCarryforwardedStatus(imgStatusDualLevelReviewTypeForwardYes, imgStatusDualLevelReviewTypeForwardNo, IsDualLevelReviewTypeForwarded);
        }
        ListControlHelper.BindDualLevelReviewType(ddlDualLevelReviewType, true);
        if (_DualLevelReviewTypeID != 0)
        {
            ddlDualLevelReviewType.SelectedIndex = ddlDualLevelReviewType.Items.IndexOf(ddlDualLevelReviewType.Items.FindByValue(_DualLevelReviewTypeID.ToString()));
        }
    }
    public void ChangedEventHandler()
    {
        oUtilityClient = RemotingHelper.GetUtilityObject();
        Helper.ChangeCssClassOfTDStatus(tdCapabilityStatus, IsDualLevelReviewForwarded);
        Helper.SetCarryforwardedStatus(imgStatusDualReviewForwardYes, imgStatusDualReviewForwardNo, IsDualLevelReviewForwarded);
        Helper.SetCarryforwardedStatus(imgStatusDualLevelReviewTypeForwardYes, imgStatusDualLevelReviewTypeForwardNo, IsDualLevelReviewTypeForwarded);
        Helper.SetYesNoRadioButtons(optDualReviewYes, optDualReviewNo, IsDualLevelReviewYesChecked);
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
        if (optDualReviewYes.Checked == true)
        {
            if (ddlDualLevelReviewType.SelectedValue != WebConstants.SELECT_ONE)
            {
                oCompanySettingInfo.DualLevelReviewTypeID = Convert.ToInt16(ddlDualLevelReviewType.SelectedValue);
            }
        }
        else
        {
            oCompanySettingInfo.DualLevelReviewTypeID = null;
        }
        return oCompanySettingInfo;
    }
    public bool IsValueChanged()
    {
        bool IsValueChangeFlag = false;
        string DualLevelReviewTypeCurrentDBVal = string.Empty;
        if (ViewState[ViewStateConstants.DUAL_LEVEL_REVIEW_TYPE_CURRENT_DB_VAL] != null)
            DualLevelReviewTypeCurrentDBVal = ViewState[ViewStateConstants.DUAL_LEVEL_REVIEW_TYPE_CURRENT_DB_VAL].ToString();
        if (string.IsNullOrEmpty(DualLevelReviewTypeCurrentDBVal) && ddlDualLevelReviewType.Visible && ddlDualLevelReviewType.SelectedValue != WebConstants.SELECT_ONE)
            IsValueChangeFlag = true;
        else if (ddlDualLevelReviewType.SelectedValue != DualLevelReviewTypeCurrentDBVal)
            IsValueChangeFlag = true;
        return IsValueChangeFlag;
    }
    #endregion
   
}//end of class
