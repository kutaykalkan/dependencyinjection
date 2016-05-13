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
using SkyStem.ART.Web.Data;
using System.Collections.Generic;
using SkyStem.ART.Web.Classes;
using SkyStem.ART.Web.Utility;
using SkyStem.ART.Client.Exception;
using SkyStem.Library.Controls.WebControls;
using SkyStem.ART.Client.Params;

public partial class UserControls_CapabilityMandatoryReportSignoff : UserControlBase
{
    #region Variables & Constants
    short REVIEWER_ROLEID = 0;
    short APPROVER_ROLEID = 0;
    IUtility oUtilityClient;
    int _companyID;
    bool? _isMandatoryReportSignoffForwarded = false;
    bool? _isMandatoryReportSignoffYesChecked = false;
    bool? _isMandatoryReportSignoffYesCheckedCurrent = false;
    bool _isDualReviewYesCheckedFromUI = false;
    # endregion
    #region Properties
    public bool? IsMandatoryReportSignoffForwarded
    {
        get { return _isMandatoryReportSignoffForwarded; }
        set { _isMandatoryReportSignoffForwarded = value; }
    }
    public bool? IsMandatoryReportSignoffYesChecked
    {
        get { return _isMandatoryReportSignoffYesChecked; }
        set
        {
            _isMandatoryReportSignoffYesChecked = value;
        }
    }
    public bool? IsMandatoryReportSignoffYesCheckedCurrent
    {
        get { return _isMandatoryReportSignoffYesCheckedCurrent; }
        set
        {
            _isMandatoryReportSignoffYesCheckedCurrent = value;
        }
    }
    public bool IsDualReviewYesChecked
    {
        get { return _isDualReviewYesCheckedFromUI; }
        set
        {
            _isDualReviewYesCheckedFromUI = value;
        }
    }
    # endregion
    #region Page Events
    protected void Page_Init(object sender, EventArgs e)
    {
        SetErrorMessages();
        cvReviewerMandatoryReport.Attributes.Add("ControlToValidateCBL", cblReviewerMandatoryReportSignoff.ClientID);
        cvApproverMandatoryReport.Attributes.Add("ControlToValidateCBL", cblApproverMandatoryReportSignoff.ClientID);

    }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            _companyID = SessionHelper.CurrentCompanyID.Value;
            oUtilityClient = RemotingHelper.GetUtilityObject();
            REVIEWER_ROLEID = Convert.ToInt16(Enum.Parse(typeof(WebEnums.UserRole), "REVIEWER"));
            APPROVER_ROLEID = Convert.ToInt16(Enum.Parse(typeof(WebEnums.UserRole), "APPROVER"));
            if (!IsPostBack)
            {

                Helper.ChangeCssClassOfTDStatus(tdCapabilityStatus, _isMandatoryReportSignoffForwarded);
                Helper.SetCarryforwardedStatus(imgStatusMandatoryReportSignoffForwardYes, imgStatusMandatoryReportSignoffForwardNo, _isMandatoryReportSignoffForwarded);
                Helper.SetYesNoRadioButtons(optMandatoryReportSignoffYes, optMandatoryReportSignoffNo, _isMandatoryReportSignoffYesChecked);
                if (_isMandatoryReportSignoffYesChecked == true)
                {
                    BindAfterYesNoSelection();
                }
            }
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
        this.cvApproverMandatoryReport.ErrorMessage = Helper.GetErrorMessage(WebEnums.FieldType.MandatoryField, 5000069);
        this.cvReviewerMandatoryReport.ErrorMessage = Helper.GetErrorMessage(WebEnums.FieldType.MandatoryField, 5000070);
    }
    #endregion
    #region Other Events
    protected void BindRoleReportCBL(CheckBoxList cbl, IList<RoleReportInfo_ExtendedWithReportName> oRoleReportInfoCollection)
    {
        cbl.DataSource = oRoleReportInfoCollection;//all reports
        cbl.DataTextField = "Report";
        cbl.DataValueField = "ReportID";
        cbl.DataBind();
    }
    protected void MakeRoleMandatoryReportSelectedCBL(CheckBoxList cbl, IList<RoleMandatoryReportInfo> oRoleMandatoryReportInfoCollection)
    {
        for (int i = 0; i < cbl.Items.Count; i++)
        {
            foreach (RoleMandatoryReportInfo oRoleMandatoryReportInfo in oRoleMandatoryReportInfoCollection)
            {
                if (oRoleMandatoryReportInfo.ReportID.ToString() == cbl.Items[i].Value)
                {
                    cbl.Items[i].Selected = true;
                }
            }
        }
    }
    protected void optMandatoryReportSignoffYes_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            ShowHideForRadioButtonYesNoChecked();
            BindAfterYesNoSelection();
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
    protected void optMandatoryReportSignoffNo_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            ShowHideForRadioButtonYesNoChecked();
            //BindAfterYesNoSelection();
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
        if (optMandatoryReportSignoffYes.Checked == true && optMandatoryReportSignoffNo.Checked == false)
        {
            pnlContentMandatoryReportSignoff.Visible = true;
            pnlContent.Visible = true;
            _isMandatoryReportSignoffYesCheckedCurrent = true;
            //imgCollapse.Visible = true;
            imgCollapse.Style[HtmlTextWriterStyle.Display] = "block";

            if (_isDualReviewYesCheckedFromUI == true)
            {
                pnlContentMandatoryReportSignoffApprover.Visible = true;
            }
            else
            {
                pnlContentMandatoryReportSignoffApprover.Visible = false;
            }

            //pnlMain.CssClass = "PanelCapability";
            //pnlContent.CssClass = "PanelContent";
            //pnlYesNo.CssClass = "PanelCapabilityYesNo";
        }
        else if (optMandatoryReportSignoffYes.Checked == false && optMandatoryReportSignoffNo.Checked == true)
        {
            pnlContentMandatoryReportSignoff.Visible = false;
            pnlContent.Visible = false;
            _isMandatoryReportSignoffYesCheckedCurrent = false;
            //imgCollapse.Visible = false;
            imgCollapse.Style[HtmlTextWriterStyle.Display] = "none";
            //pnlMain.CssClass = "";
            //pnlContent.CssClass = "";
            //pnlYesNo.CssClass = "";
        }
        else if (optMandatoryReportSignoffYes.Checked == false && optMandatoryReportSignoffNo.Checked == false)
        {
            pnlContentMandatoryReportSignoff.Visible = false;
            pnlContent.Visible = false;
            _isMandatoryReportSignoffYesCheckedCurrent = null;
            //imgCollapse.Visible = false;
            imgCollapse.Style[HtmlTextWriterStyle.Display] = "none";
            //pnlMain.CssClass = "";
            //pnlContent.CssClass = "";
            //pnlYesNo.CssClass = "";
        }
    }
    public void BindAfterYesNoSelection()
    {
        IReport oReportClient = RemotingHelper.GetReportObject();

        ReportParamInfo oReportParamInfo = new ReportParamInfo();
        oReportParamInfo.RoleID = REVIEWER_ROLEID;
        oReportParamInfo.RecPeriodID = SessionHelper.CurrentReconciliationPeriodID;
        oReportParamInfo.CompanyID = SessionHelper.CurrentCompanyID;
        IList<RoleReportInfo_ExtendedWithReportName> oRoleReportInfoReviewerCollection = LanguageHelper.TranslateLabelRoleReportInfo_ExtendedWithReportName(oReportClient.SelectAllRoleReportByRoleID(oReportParamInfo, Helper.GetAppUserInfo()));

        oReportParamInfo.RoleID = APPROVER_ROLEID;
        IList<RoleReportInfo_ExtendedWithReportName> oRoleReportInfoApproverCollection = LanguageHelper.TranslateLabelRoleReportInfo_ExtendedWithReportName(oReportClient.SelectAllRoleReportByRoleID(oReportParamInfo, Helper.GetAppUserInfo()));
        IList<RoleMandatoryReportInfo> oRoleMandatoryReportInfoCollection = new List<RoleMandatoryReportInfo>();
        IList<RoleMandatoryReportInfo> oRoleMandatoryReportInfoCollectionReviewer = new List<RoleMandatoryReportInfo>();
        IList<RoleMandatoryReportInfo> oRoleMandatoryReportInfoCollectionApprover = new List<RoleMandatoryReportInfo>();
        oRoleMandatoryReportInfoCollection = oReportClient.SelectAllRoleMandatoryReportByReconciliationPeriodID(SessionHelper.CurrentReconciliationPeriodID, Helper.GetAppUserInfo());
        List<RoleMandatoryReportInfo> oRoleMandatoryReportInfoCollectionDBVal = new List<RoleMandatoryReportInfo>();   
       
        foreach (RoleMandatoryReportInfo oRoleMandatoryReportInfo in oRoleMandatoryReportInfoCollection)
        {
            if (oRoleMandatoryReportInfo.RoleID.Value == REVIEWER_ROLEID)//TODO: check about nullable
            {
                oRoleMandatoryReportInfoCollectionReviewer.Add(oRoleMandatoryReportInfo);                
            }
            else if (oRoleMandatoryReportInfo.RoleID.Value == APPROVER_ROLEID)
            {
                oRoleMandatoryReportInfoCollectionApprover.Add(oRoleMandatoryReportInfo);
            }
        }
        oRoleMandatoryReportInfoCollectionDBVal.AddRange(oRoleMandatoryReportInfoCollectionReviewer);
        if (_isDualReviewYesCheckedFromUI == true)
        {
            oRoleMandatoryReportInfoCollectionDBVal.AddRange(oRoleMandatoryReportInfoCollectionApprover);
        }
        ViewState[ViewStateConstants.ROLE_MANDATORY_REPORT_CURRENT_DB_VAL] = oRoleMandatoryReportInfoCollectionDBVal;

        if (oRoleReportInfoReviewerCollection != null && oRoleReportInfoReviewerCollection.Count > 0)
        {
            BindRoleReportCBL(cblReviewerMandatoryReportSignoff, oRoleReportInfoReviewerCollection);
            MakeRoleMandatoryReportSelectedCBL(cblReviewerMandatoryReportSignoff, oRoleMandatoryReportInfoCollectionReviewer);
        }
        else
        {
            //pnlContentMandatoryReportSignoffReviewer.Visible = false;
            //TODO: Display message that no reports for that role
        }
        if (_isDualReviewYesCheckedFromUI == true)
        {
            if (oRoleReportInfoApproverCollection != null && oRoleReportInfoApproverCollection.Count > 0)
            {
                BindRoleReportCBL(cblApproverMandatoryReportSignoff, oRoleReportInfoApproverCollection);
                MakeRoleMandatoryReportSelectedCBL(cblApproverMandatoryReportSignoff, oRoleMandatoryReportInfoCollectionApprover);
            }
            {
                //pnlContentMandatoryReportSignoffApprover.Visible = false;
                //TODO: Display message that no reports for that role
            }
        }
    }
    public void ChangedEventHandler()
    {

        Helper.ChangeCssClassOfTDStatus(tdCapabilityStatus, _isMandatoryReportSignoffForwarded);
        Helper.SetCarryforwardedStatus(imgStatusMandatoryReportSignoffForwardYes, imgStatusMandatoryReportSignoffForwardNo, _isMandatoryReportSignoffForwarded);
        Helper.SetYesNoRadioButtons(optMandatoryReportSignoffYes, optMandatoryReportSignoffNo, _isMandatoryReportSignoffYesChecked);
        if (_isMandatoryReportSignoffYesChecked == true)
        {
            BindAfterYesNoSelection();
        }
        ShowHideForRadioButtonYesNoChecked();
        upnlMandatoryReportSignoff.Update();
    }
    public IList<RoleMandatoryReportInfo> GetMandatoryReportObjectToBeSavedFromUC()
    {
        IList<RoleMandatoryReportInfo> oRoleMandatoryReportInfoCollection = new List<RoleMandatoryReportInfo>();
        RoleMandatoryReportInfo oRoleMandatoryReportInfo = null;
        for (int i = 0; i < cblReviewerMandatoryReportSignoff.Items.Count; i++)
        {
            if (cblReviewerMandatoryReportSignoff.Items[i].Selected == true)
            {
                oRoleMandatoryReportInfo = new RoleMandatoryReportInfo();
                oRoleMandatoryReportInfo.ReportID = Convert.ToInt16(cblReviewerMandatoryReportSignoff.Items[i].Value);
                oRoleMandatoryReportInfo.RoleID = REVIEWER_ROLEID;//TODO: Reviewer- from constant file
                oRoleMandatoryReportInfo.CompanyID = _companyID;
                oRoleMandatoryReportInfoCollection.Add(oRoleMandatoryReportInfo);
            }
        }
        if (_isDualReviewYesCheckedFromUI == true)
        {
            for (int i = 0; i < cblApproverMandatoryReportSignoff.Items.Count; i++)
            {
                if (cblApproverMandatoryReportSignoff.Items[i].Selected == true)
                {
                    oRoleMandatoryReportInfo = new RoleMandatoryReportInfo();
                    oRoleMandatoryReportInfo.ReportID = Convert.ToInt16(cblApproverMandatoryReportSignoff.Items[i].Value);
                    oRoleMandatoryReportInfo.RoleID = APPROVER_ROLEID;//TODO: Approver- from constant file
                    oRoleMandatoryReportInfo.CompanyID = _companyID;
                    oRoleMandatoryReportInfoCollection.Add(oRoleMandatoryReportInfo);
                }
            }
        }
        return oRoleMandatoryReportInfoCollection;
    }
    public bool IsValueChanged()
    {
        bool? IsValueChangeFlag = false;     
        List<RoleMandatoryReportInfo> oDBRoleMandatoryReportInfoList = (List<RoleMandatoryReportInfo>)ViewState[ViewStateConstants.ROLE_MANDATORY_REPORT_CURRENT_DB_VAL];
        List<RoleMandatoryReportInfo> oCurrentRoleMandatoryReportInfoList = (List<RoleMandatoryReportInfo>)GetMandatoryReportObjectToBeSavedFromUC();
        if (oCurrentRoleMandatoryReportInfoList != null && oDBRoleMandatoryReportInfoList != null)
        {
            if (oDBRoleMandatoryReportInfoList.Count != oCurrentRoleMandatoryReportInfoList.Count)
                IsValueChangeFlag = true;
            else
            {
                foreach (var CurrentRoleMandatoryReportInfo in oCurrentRoleMandatoryReportInfoList)
                {
                    var oRoleMandatoryReportInfo = oDBRoleMandatoryReportInfoList.Find(c => c.RoleID.GetValueOrDefault() == CurrentRoleMandatoryReportInfo.RoleID.GetValueOrDefault() && c.ReportID.GetValueOrDefault() == CurrentRoleMandatoryReportInfo.ReportID.GetValueOrDefault());
                    if (oRoleMandatoryReportInfo == null)
                    {
                        IsValueChangeFlag = true;
                        break;
                    }
                }
            }
        }

        return IsValueChangeFlag.Value;
    }
    #endregion
}//end of class
