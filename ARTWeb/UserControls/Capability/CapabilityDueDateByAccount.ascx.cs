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

public partial class UserControls_CapabilityDueDateByAccount : UserControlBase
{
    #region Variables & Constants
    IUtility oUtilityClient;
    int _DayTypeID = 0;
    int _companyID;
    bool? _isDueDateByAccountYesChecked = null;
    bool? _isDueDateByAccountForwarded = null;
    bool? _isDayTypeForwarded = null;
    bool? _isDueDateByAccountYesCheckedCurrent = null;
    #endregion   
    #region Properties
    public bool? IsDueDateByAccountYesChecked
    {
        get { return _isDueDateByAccountYesChecked; }
        set { _isDueDateByAccountYesChecked = value; }
    }
    public bool? IsDueDateByAccountForwarded
    {
        get { return _isDueDateByAccountForwarded; }
        set { _isDueDateByAccountForwarded = value; }
    }
    public bool? IsDayTypeForwarded
    {
        get { return _isDayTypeForwarded; }
        set { _isDayTypeForwarded = value; }
    }
    public bool? IsDueDateByAccountYesCheckedCurrent
    {
        get
        {
            _isDueDateByAccountYesCheckedCurrent = (bool?)ViewState["_isDueDateByAccountYesCheckedCurrent"];
            return _isDueDateByAccountYesCheckedCurrent;
        }
        set
        {
            _isDueDateByAccountYesCheckedCurrent = value;
            ViewState["_isDueDateByAccountYesCheckedCurrent"] = value;
        }
    }
    public bool IsPartialEditMode { get; set; }
    #endregion
    #region Delegates & Events
    public event EventHandler DueDateByAccountSelectionChanged;
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
                Helper.ChangeCssClassOfTDStatus(tdCapabilityStatus, IsDueDateByAccountForwarded);

                Helper.SetCarryforwardedStatus(imgStatusDueDateByAccountForwardYes, imgStatusDueDateByAccountForwardNo, IsDueDateByAccountForwarded);
                Helper.SetYesNoRadioButtons(OptDueDateByAccountYes, OptDueDateByAccountNo, IsDueDateByAccountYesChecked);
                if (IsDueDateByAccountYesChecked == true)
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
    protected void OptDueDateByAccountYes_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            ShowHideForRadioButtonYesNoChecked();
            BindAfterYesNoSelection();
            Helper.SetCarryforwardedStatus(imgStatusDayTypeForwardYes, imgStatusDayTypeForwardNo, IsDayTypeForwarded);
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
    protected void OptDueDateByAccountNo_CheckedChanged(object sender, EventArgs e)
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
        this.rfvDayType.ErrorMessage = Helper.GetErrorMessage(WebEnums.FieldType.MandatoryField, this.lblDayType.LabelID);
    }
    #endregion
    #region Other Methods
    protected void ShowHideForRadioButtonYesNoChecked()
    {
        if (OptDueDateByAccountYes.Checked == true && OptDueDateByAccountNo.Checked == false)
        {
            pnlContentDueDateByAccount.Visible = true;
            pnlContent.Visible = true;
            IsDueDateByAccountYesCheckedCurrent = true;
            imgCollapse.Style[HtmlTextWriterStyle.Display] = "block";
        }
        else if (OptDueDateByAccountYes.Checked == false && OptDueDateByAccountNo.Checked == true)
        {
            pnlContentDueDateByAccount.Visible = false;
            pnlContent.Visible = false;
            IsDueDateByAccountYesCheckedCurrent = false;
            imgCollapse.Style[HtmlTextWriterStyle.Display] = "none";
            pnlContent.CssClass = "";
            pnlYesNo.CssClass = "";
        }
        else if (OptDueDateByAccountYes.Checked == false && OptDueDateByAccountNo.Checked == false)
        {
            pnlContentDueDateByAccount.Visible = false;
            pnlContent.Visible = false;
            IsDueDateByAccountYesCheckedCurrent = null;
            imgCollapse.Style[HtmlTextWriterStyle.Display] = "none";
            pnlContent.CssClass = "";
            pnlYesNo.CssClass = "";
        }
        if (DueDateByAccountSelectionChanged != null)
            DueDateByAccountSelectionChanged.Invoke(this, null);
    }
    protected void BindAfterYesNoSelection()
    {
        IList<CompanySettingInfo> oCompanySettingInfoCollection = new List<CompanySettingInfo>();
        ICompany oCompanyClient = RemotingHelper.GetCompanyObject();
        oCompanySettingInfoCollection = oCompanyClient.SelectCompanyDayType(SessionHelper.CurrentReconciliationPeriodID, Helper.GetAppUserInfo());
        if (oCompanySettingInfoCollection != null && oCompanySettingInfoCollection.Count > 0 && oCompanySettingInfoCollection[0].DayTypeID.HasValue)
        {
            _DayTypeID = oCompanySettingInfoCollection[0].DayTypeID.Value;
            ViewState[ViewStateConstants.DAY_TYPE_CURRENT_DB_VAL] = _DayTypeID;
            IsDayTypeForwarded = oCompanySettingInfoCollection[0].IsCarryForwardedFromPreviousRecPeriod.Value;
            Helper.SetCarryforwardedStatus(imgStatusDayTypeForwardYes, imgStatusDayTypeForwardNo, IsDayTypeForwarded);
        }
        ListControlHelper.BindddlDaysType(ddlDayType, true);
        if (_DayTypeID != 0)
        {
            ListItem oItem = ddlDayType.Items.FindByValue(_DayTypeID.ToString());
            if (oItem != null)
                oItem.Selected = true;
        }
    }
    public void ChangedEventHandler()
    {
        oUtilityClient = RemotingHelper.GetUtilityObject();
        Helper.ChangeCssClassOfTDStatus(tdCapabilityStatus, IsDueDateByAccountForwarded);
        Helper.SetCarryforwardedStatus(imgStatusDueDateByAccountForwardYes, imgStatusDueDateByAccountForwardNo, IsDueDateByAccountForwarded);
        Helper.SetCarryforwardedStatus(imgStatusDayTypeForwardYes, imgStatusDayTypeForwardNo, IsDayTypeForwarded);
        Helper.SetYesNoRadioButtons(OptDueDateByAccountYes, OptDueDateByAccountNo, IsDueDateByAccountYesChecked);
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
        if (OptDueDateByAccountYes.Checked == true)
        {
            if (ddlDayType.SelectedValue != WebConstants.SELECT_ONE)
            {
                oCompanySettingInfo.DayTypeID = Convert.ToInt16(ddlDayType.SelectedValue);
            }
        }
        else
        {
            oCompanySettingInfo.DayTypeID = null;
        }
        return oCompanySettingInfo;
    }
    public bool IsValueChanged()
    {
        bool IsValueChangeFlag = false;
        string DayTypeCurrentDBVal = string.Empty;
        if (ViewState[ViewStateConstants.DAY_TYPE_CURRENT_DB_VAL] != null)
            DayTypeCurrentDBVal = ViewState[ViewStateConstants.DAY_TYPE_CURRENT_DB_VAL].ToString();
        if (string.IsNullOrEmpty(DayTypeCurrentDBVal) && ddlDayType.Visible && ddlDayType.SelectedValue != WebConstants.SELECT_ONE)
            IsValueChangeFlag = true;
        else if (ddlDayType.SelectedValue != DayTypeCurrentDBVal)
            IsValueChangeFlag = true;
        return IsValueChangeFlag;
    }
    #endregion
   
}//end of class
