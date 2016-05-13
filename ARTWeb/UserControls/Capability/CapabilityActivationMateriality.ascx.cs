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

public partial class UserControls_CapabilityActivationMateriality : UserControlBase
{
    IUtility oUtilityClient;
    int MATERIALTYPEID_FSCAPTION = 1;
    int MATERIALTYPEID_COMPANYWIDE = 2;
    int _materialityTypeID = 0;


    int _companyID;
    bool? _isMaterialityYesChecked = null;
    bool? _isMaterialityForwarded = null;
    bool? _isMaterialityTypeForwarded = null;
    //bool? _isMaterialityYesCheckedCurrent = false;
    bool? _isMaterialityYesCheckedCurrent = null;
    bool _isFSCaptionMaterialitySelected = false;
    bool _isByCompanyMaterialitySelected = false;

    public bool? IsMaterialityYesChecked
    {
        get { return _isMaterialityYesChecked; }
        set { _isMaterialityYesChecked = value; }
    }
    public bool? IsMaterialityForwarded
    {
        get { return _isMaterialityForwarded; }
        set { _isMaterialityForwarded = value; }
    }
    public bool? IsMaterialityTypeForwarded
    {
        get { return _isMaterialityTypeForwarded; }
        set { _isMaterialityTypeForwarded = value; }
    }
    public bool? IsMaterialityYesCheckedCurrent
    {
        get { return _isMaterialityYesCheckedCurrent; }
        set { _isMaterialityYesCheckedCurrent = value; }
    }
    public bool IsFSCaptionMaterialitySelected
    {
        get { return _isFSCaptionMaterialitySelected; }
        set { _isFSCaptionMaterialitySelected = value; }
    }
    public bool IsByCompanyMaterialitySelected
    {
        get { return _isByCompanyMaterialitySelected; }
        set { _isByCompanyMaterialitySelected = value; }
    }
    public bool IsPartialEditMode { get; set; }

    protected void Page_Init(object sender, EventArgs e)
    {
        //TODO: do we need try- catch here?
        SetErrorMessages();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //SetErrorMessages();
            _companyID = SessionHelper.CurrentCompanyID.Value;
            oUtilityClient = RemotingHelper.GetUtilityObject();
            if (!IsPostBack)
            {
                Helper.ChangeCssClassOfTDStatus(tdCapabilityStatus, IsMaterialityForwarded);

                Helper.SetCarryforwardedStatus(imgStatusMaterialityForwardYes, imgStatusMaterialityForwardNo, IsMaterialityForwarded);
                Helper.SetYesNoRadioButtons(optMaterialityYes, optMaterialityNo, IsMaterialityYesChecked);
                if (IsMaterialityYesChecked == true)
                {
                    BindAfterYesNoSelection();
                }
            }
            ShowHideForRadioButtonYesNoChecked();
            rfvMaterialValue.ErrorMessage = Helper.GetErrorMessage(WebEnums.FieldType.MandatoryField, 1282);
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

    protected void ddlMaterialityType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            HideShowSetBasedOnDropDownSelectedValue();
            if (IsFSCaptionMaterialitySelected == true)
            {
                BindForFSCaptionDDLOption();
            }
            else if (IsByCompanyMaterialitySelected == true)
            {
                BindForCompanyWideDDLOption();
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

    protected void rdFSCaptionwideMateriality_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {
        if (e.Item.ItemType == Telerik.Web.UI.GridItemType.Item ||
           e.Item.ItemType == Telerik.Web.UI.GridItemType.AlternatingItem)
        {
            CustomValidator cvMaterialValue = e.Item.FindControl("cvMaterialValue") as CustomValidator;
            RequiredFieldValidator rfvMaterialValue = e.Item.FindControl("rfvMaterialValue") as RequiredFieldValidator;
            if (cvMaterialValue != null)
            {
                cvMaterialValue.ErrorMessage = LanguageUtil.GetValue(2492);
            }
            TextBox txtMaterialValue = e.Item.FindControl("txtMaterialValue") as TextBox;
            ExLabel lblFSCaptionName = e.Item.FindControl("lblFSCaptionName") as ExLabel;
            FSCaptionInfo_ExtendedWithMaterialityInfo oFSCaptionWithMaterialityInfo = new FSCaptionInfo_ExtendedWithMaterialityInfo();
            oFSCaptionWithMaterialityInfo = (FSCaptionInfo_ExtendedWithMaterialityInfo)e.Item.DataItem;
            lblFSCaptionName.Text = oFSCaptionWithMaterialityInfo.FSCaption;
            txtMaterialValue.Text = Helper.GetDecimalValueForTextBox(oFSCaptionWithMaterialityInfo.MaterialityThreshold, WebConstants.INT_FOR_DECIMAL_VALUE_TEXTBOX);
            if (oFSCaptionWithMaterialityInfo.LabelID.HasValue)
            {
                //txtMaterialValue.ErrorMessage = Helper.GetErrorMessage(WebEnums.FieldType.FSCaptionMandatoryField, oFSCaptionWithMaterialityInfo.LabelID.Value);
                //cvMaterialValue.ErrorMessage = LanguageUtil.GetValue(5000093);
                rfvMaterialValue.ErrorMessage = Helper.GetErrorMessage(WebEnums.FieldType.FSCaptionMandatoryField, oFSCaptionWithMaterialityInfo.LabelID.Value);
            }
            if (IsPartialEditMode && oFSCaptionWithMaterialityInfo.CreationPeriodEndDate != SessionHelper.CurrentReconciliationPeriodEndDate)
                txtMaterialValue.Attributes.Add("disabled", "disabled");
        }
    }


    protected void optMaterialityYes_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            ShowHideForRadioButtonYesNoChecked();
            BindAfterYesNoSelection();
            Helper.SetCarryforwardedStatus(imgStatusMaterialityTypeForwardYes, imgStatusMaterialityTypeForwardNo, IsMaterialityTypeForwarded);
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

    protected void optMaterialityNo_CheckedChanged(object sender, EventArgs e)
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

    protected void ShowHideForRadioButtonYesNoChecked()
    {
        if (optMaterialityYes.Checked == true && optMaterialityNo.Checked == false)
        {
            pnlContentMateriality.Visible = true;
            pnlContent.Visible = true;
            IsMaterialityYesCheckedCurrent = true;
            //imgCollapse.Visible = true;
            imgCollapse.Style[HtmlTextWriterStyle.Display] = "block";
            //pnlMain.CssClass = "PanelCapability";
            //pnlContent.CssClass = "PanelContent";

            //pnlYesNo.CssClass = "PanelCapabilityYesNo";
            //pnlHoverMaterialitySelection.Visible = true;
        }
        else if (optMaterialityYes.Checked == false && optMaterialityNo.Checked == true)
        {
            pnlContentMateriality.Visible = false;
            pnlContent.Visible = false;
            IsMaterialityYesCheckedCurrent = false;
            //imgCollapse.Visible = false;
            imgCollapse.Style[HtmlTextWriterStyle.Display] = "none";
            //pnlMain.CssClass = "";
            pnlContent.CssClass = "";
            pnlYesNo.CssClass = "";
            //pnlHoverMaterialitySelection.Visible = false ;
        }
        else if (optMaterialityYes.Checked == false && optMaterialityNo.Checked == false)
        {
            pnlContentMateriality.Visible = false;
            pnlContent.Visible = false;
            IsMaterialityYesCheckedCurrent = null;
            //imgCollapse.Visible = false;
            imgCollapse.Style[HtmlTextWriterStyle.Display] = "none";
            //pnlMain.CssClass = "";
            pnlContent.CssClass = "";
            pnlYesNo.CssClass = "";
            //pnlHoverMaterialitySelection.Visible = false;
        }
    }

    protected void HideShowSetBasedOnDropDownSelectedValue()
    {
        if (ddlMaterialityType.SelectedValue == MATERIALTYPEID_COMPANYWIDE.ToString())//TODO: avoid hard coded value
        {
            divCompanywideMaterialityThreshold.Visible = true;
            divFSCaptionwideMaterialityThreshold.Visible = false;
            IsFSCaptionMaterialitySelected = false;
            IsByCompanyMaterialitySelected = true;
        }
        else if (ddlMaterialityType.SelectedValue == MATERIALTYPEID_FSCAPTION.ToString())
        {
            divCompanywideMaterialityThreshold.Visible = false;
            divFSCaptionwideMaterialityThreshold.Visible = true;
            IsFSCaptionMaterialitySelected = true;
            IsByCompanyMaterialitySelected = false;
        }
        else
        {
            divCompanywideMaterialityThreshold.Visible = false;
            divFSCaptionwideMaterialityThreshold.Visible = false;
            IsFSCaptionMaterialitySelected = false;
            IsByCompanyMaterialitySelected = false;
        }
    }

    protected void BindForCompanyWideDDLOption()
    {
        IList<CompanySettingInfo> oCompanySettingInfoCollection;
        ICompany oCompanyClient = RemotingHelper.GetCompanyObject();

        oCompanySettingInfoCollection = oCompanyClient.SelectCompanyMaterialityType(SessionHelper.CurrentReconciliationPeriodID, Helper.GetAppUserInfo());

        if (oCompanySettingInfoCollection != null && oCompanySettingInfoCollection.Count > 0 && oCompanySettingInfoCollection[0].CompanyMaterialityThreshold != null)
        {
            txtCompanywideMaterialityThreshold.Text = Helper.GetDecimalValueForTextBox(oCompanySettingInfoCollection[0].CompanyMaterialityThreshold, TestConstant.DECIMAL_PLACES_FOR_TEXTBOX);
        }
        else
        {
            txtCompanywideMaterialityThreshold.Text = "";
        }
        hdnCompanywideMaterialityThreshold.Value = txtCompanywideMaterialityThreshold.Text;
    }

    protected void BindForFSCaptionDDLOption()
    {
        IFSCaption oFSCaptionClient = RemotingHelper.GetFSCaptioneObject();
        IList<FSCaptionInfo_ExtendedWithMaterialityInfo> oFSCaptionInfoCollection;
        oFSCaptionInfoCollection = oFSCaptionClient.SelectAllFSCaptionMergeMaterilityByReconciliationPeriodID(SessionHelper.CurrentReconciliationPeriodID, Helper.GetAppUserInfo());
        oFSCaptionInfoCollection = LanguageHelper.TranslateLabelFSCaptionInfo(oFSCaptionInfoCollection);
        ViewState[ViewStateConstants.FSCAPTION_MATERIALITY_CURRENT_DB_VAL] = oFSCaptionInfoCollection;

        if (oFSCaptionInfoCollection != null && oFSCaptionInfoCollection.Count > 10)
        {
            // Only If Count of FS Caption is greater than 10, show a vertical scrollbar 
            // else just show the records without scroll
            rdFSCaptionwideMateriality.ClientSettings.Scrolling.AllowScroll = true;
            rdFSCaptionwideMateriality.ClientSettings.Scrolling.ScrollHeight = 400;
        }

        rdFSCaptionwideMateriality.DataSource = oFSCaptionInfoCollection;
        rdFSCaptionwideMateriality.DataBind();
    }

    protected void BindAfterYesNoSelection()
    {
        IList<CompanySettingInfo> oCompanySettingInfoCollection = new List<CompanySettingInfo>();
        ICompany oCompanyClient = RemotingHelper.GetCompanyObject();
        oCompanySettingInfoCollection = oCompanyClient.SelectCompanyMaterialityType(SessionHelper.CurrentReconciliationPeriodID, Helper.GetAppUserInfo());
        //oCompanySettingInfoCollection = oCompanyClient.SelectAllCompanySettingByCompanyID(_companyID);
        if (oCompanySettingInfoCollection != null && oCompanySettingInfoCollection.Count > 0 && oCompanySettingInfoCollection[0].MaterialityTypeID.HasValue)
        {
            _materialityTypeID = oCompanySettingInfoCollection[0].MaterialityTypeID.Value;
        }

        //SET StatusMaterialityTypeForward
        if (oCompanySettingInfoCollection != null && oCompanySettingInfoCollection.Count > 0 && oCompanySettingInfoCollection[0].IsCarryForwardedFromPreviousRecPeriod.HasValue)
        {
            _materialityTypeID = oCompanySettingInfoCollection[0].MaterialityTypeID.Value;
            IsMaterialityTypeForwarded = oCompanySettingInfoCollection[0].IsCarryForwardedFromPreviousRecPeriod.Value;
            Helper.SetCarryforwardedStatus(imgStatusMaterialityTypeForwardYes, imgStatusMaterialityTypeForwardNo, IsMaterialityTypeForwarded);

        }

        ListControlHelper.BindMaterialityType(ddlMaterialityType, true);
        if (_materialityTypeID != 0)
        {
            ViewState[ViewStateConstants.MATERIALITY_TYPE_CURRENT_DB_VAL] = _materialityTypeID;
            ddlMaterialityType.SelectedIndex = ddlMaterialityType.Items.IndexOf(ddlMaterialityType.Items.FindByValue(_materialityTypeID.ToString()));
        }
        else
        {
            //TODO:
        }
        HideShowSetBasedOnDropDownSelectedValue();
        if (IsFSCaptionMaterialitySelected == true)
        {
            BindForFSCaptionDDLOption();
        }
        else if (IsByCompanyMaterialitySelected == true)
        {
            BindForCompanyWideDDLOption();
        }
        if (IsPartialEditMode)
        {
            ddlMaterialityType.Attributes.Add("disabled", "disabled");
            optMaterialityYes.InputAttributes.Add("disabled", "disabled");
            optMaterialityNo.InputAttributes.Add("disabled", "disabled");
        }
        else
        {
            ddlMaterialityType.Attributes.Clear();
        }
    }

    //Handle masterpage DDLs change
    public void ChangedEventHandler()
    {
        //its called from out side so no need of try_catch
        oUtilityClient = RemotingHelper.GetUtilityObject();
        Helper.ChangeCssClassOfTDStatus(tdCapabilityStatus, IsMaterialityForwarded);
        Helper.SetCarryforwardedStatus(imgStatusMaterialityForwardYes, imgStatusMaterialityForwardNo, IsMaterialityForwarded);
        Helper.SetCarryforwardedStatus(imgStatusMaterialityTypeForwardYes, imgStatusMaterialityTypeForwardNo, IsMaterialityTypeForwarded);

        Helper.SetYesNoRadioButtons(optMaterialityYes, optMaterialityNo, IsMaterialityYesChecked);
        //if (IsMaterialityYesChecked == true)
        //{
        BindAfterYesNoSelection();
        //}
        ShowHideForRadioButtonYesNoChecked();
    }

    public CompanySettingInfo GetCompanySettingObjectToBeSavedFromUC()
    {
        CompanySettingInfo oCompanySettingInfo = null;
        oCompanySettingInfo = new CompanySettingInfo();
        oCompanySettingInfo.CompanyID = _companyID;
        oCompanySettingInfo.AddedBy = SessionHelper.CurrentUserLoginID;
        oCompanySettingInfo.DateAdded = DateTime.Now;
        if (optMaterialityYes.Checked == true)
        {
            if (ddlMaterialityType.SelectedValue != WebConstants.SELECT_ONE)//TODO: get it from constant file
            {
                oCompanySettingInfo.MaterialityTypeID = Convert.ToInt16(ddlMaterialityType.SelectedValue);
            }
            if (oCompanySettingInfo.MaterialityTypeID == MATERIALTYPEID_COMPANYWIDE)//TODO: for constant fs caption wide
            {
                oCompanySettingInfo.CompanyMaterialityThreshold = Convert.ToDecimal(txtCompanywideMaterialityThreshold.Text.Trim());
            }
        }
        else
        {
            oCompanySettingInfo.MaterialityTypeID = null;
            oCompanySettingInfo.IsMaterialityTypeIDNull = true;
            oCompanySettingInfo.CompanyMaterialityThreshold = 0;
        }
        return oCompanySettingInfo;
    }

    public IList<FSCaptionMaterialityInfo> GetFSCaptionMaterialityObjectToBeSavedFromUC()
    {
        IList<FSCaptionMaterialityInfo> oFSCaptionMaterialityInfoCollection = null;
        if (optMaterialityYes.Checked == true && ddlMaterialityType.SelectedValue == MATERIALTYPEID_FSCAPTION.ToString())
        {
            oFSCaptionMaterialityInfoCollection = new List<FSCaptionMaterialityInfo>();
            for (int i = 0; i < rdFSCaptionwideMateriality.Items.Count; i++)
            {
                ExLabel objFSCaptionID = (ExLabel)rdFSCaptionwideMateriality.Items[i].FindControl("lblFSCaptionName");
                TextBox objMaterialValue = rdFSCaptionwideMateriality.Items[i].FindControl("txtMaterialValue") as TextBox;
                FSCaptionMaterialityInfo oFSCaptionMaterialityInfo = new FSCaptionMaterialityInfo();
                if (objMaterialValue.Text.Trim() != "")
                {
                    oFSCaptionMaterialityInfo.MaterialityThreshold = Convert.ToDecimal(objMaterialValue.Text);
                }
                else
                {
                    oFSCaptionMaterialityInfo.MaterialityThreshold = 0;
                }
                string s = rdFSCaptionwideMateriality.MasterTableView.DataKeyValues[i]["FSCaptionID"].ToString();
                oFSCaptionMaterialityInfo.FSCaptionID = Convert.ToInt32(s);
                oFSCaptionMaterialityInfoCollection.Add(oFSCaptionMaterialityInfo);
            }
        }
        return oFSCaptionMaterialityInfoCollection;
    }

    private void SetErrorMessages()
    {
        this.rfvMaterialityType.ErrorMessage = Helper.GetErrorMessage(WebEnums.FieldType.MandatoryField, this.lblMaterialityType.LabelID);
        //this.txtCompanywideMaterialityThreshold.ErrorMessage = Helper.GetErrorMessage(WebEnums.FieldType.MandatoryField, this.lblCompanywideMaterialityThreshold.LabelID);
        cvCompanywideMaterialValue.ErrorMessage = LanguageUtil.GetValue(2492);
    }
    public bool IsValueChanged()
    {
        bool? IsValueChangeFlag = false;
        string DBMaterialityTypeVal = string.Empty;
        if (ViewState[ViewStateConstants.MATERIALITY_TYPE_CURRENT_DB_VAL] != null)
            DBMaterialityTypeVal = ViewState[ViewStateConstants.MATERIALITY_TYPE_CURRENT_DB_VAL].ToString();
        if (string.IsNullOrEmpty(DBMaterialityTypeVal) && ddlMaterialityType.Visible && ddlMaterialityType.SelectedValue != WebConstants.SELECT_ONE)
            IsValueChangeFlag = true;
        else if (ddlMaterialityType.Visible && ddlMaterialityType.SelectedValue != WebConstants.SELECT_ONE &&  ddlMaterialityType.SelectedValue != DBMaterialityTypeVal)
            IsValueChangeFlag = true;
        else
        {
            if (ddlMaterialityType.SelectedValue == MATERIALTYPEID_COMPANYWIDE.ToString())
            {

                decimal? oldCompanywideMaterialityThreshold = null;
                if (!string.IsNullOrEmpty(hdnCompanywideMaterialityThreshold.Value))
                    oldCompanywideMaterialityThreshold = Convert.ToDecimal(hdnCompanywideMaterialityThreshold.Value);
                decimal? newCompanywideMaterialityThreshold = null;
                newCompanywideMaterialityThreshold = Convert.ToDecimal(txtCompanywideMaterialityThreshold.Text.Trim());
                if ((!oldCompanywideMaterialityThreshold.HasValue && newCompanywideMaterialityThreshold.HasValue) || (oldCompanywideMaterialityThreshold.HasValue && newCompanywideMaterialityThreshold.HasValue && oldCompanywideMaterialityThreshold.Value != newCompanywideMaterialityThreshold.Value))
                    IsValueChangeFlag = true;

            }
            else if (ddlMaterialityType.SelectedValue == MATERIALTYPEID_FSCAPTION.ToString())
            {
                List<FSCaptionInfo_ExtendedWithMaterialityInfo> oDBFSCaptionInfo_ExtendedWithMaterialityInfoList = (List<FSCaptionInfo_ExtendedWithMaterialityInfo>)ViewState[ViewStateConstants.FSCAPTION_MATERIALITY_CURRENT_DB_VAL];
                IList<FSCaptionMaterialityInfo> oCurrentFSCaptionMaterialityInfoList = GetFSCaptionMaterialityObjectToBeSavedFromUC();
                if (oDBFSCaptionInfo_ExtendedWithMaterialityInfoList != null && oCurrentFSCaptionMaterialityInfoList != null)
                {

                    foreach (var CurrentFSCaptionMaterialityInfo in oCurrentFSCaptionMaterialityInfoList)
                    {
                        var oFSCaptionInfo_ExtendedWithMaterialityInfo = oDBFSCaptionInfo_ExtendedWithMaterialityInfoList.Find(c => c.FSCaptionID.Value == CurrentFSCaptionMaterialityInfo.FSCaptionID.Value);
                        if (oFSCaptionInfo_ExtendedWithMaterialityInfo != null && oFSCaptionInfo_ExtendedWithMaterialityInfo.MaterialityThreshold.GetValueOrDefault() != CurrentFSCaptionMaterialityInfo.MaterialityThreshold.GetValueOrDefault())
                        {
                            IsValueChangeFlag = true;
                            break;
                        }
                    }

                }
            }
        }
        return IsValueChangeFlag.Value;
    }
}//end of class
