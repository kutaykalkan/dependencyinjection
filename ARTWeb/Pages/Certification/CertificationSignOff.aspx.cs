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
using SkyStem.ART.Web.Classes;
using SkyStem.ART.Web.Utility;
using SkyStem.ART.Client.IServices;
using SkyStem.ART.Client.Model;
using System.Collections.Generic;
using SkyStem.Language.LanguageUtility;
using SkyStem.ART.Web.Data;
using SkyStem.ART.Client.Data;

public partial class Pages_CertificationSignOff : PageBaseRecPeriod
{
    private const WebEnums.ARTPages eARTPages = WebEnums.ARTPages.CertificationAccount;
    private const WebEnums.CertificationType eCertificationType = WebEnums.CertificationType.Certification;
    private int _CompanyID;
    private short _RoleID;
    private int _ReconciliationPeriodID;
    private int _UserID;
    private bool _IsUserFromQueryString = false;
    string userName = "";
    string roleName = "";

    protected void Page_Init(object sender, EventArgs e)
    {
        MasterPageBase ompage = (MasterPageBase)this.Master.Master;
        ompage.ReconciliationPeriodChangedEventHandler += new EventHandler(ompage_ReconciliationPeriodChangedEventHandler);
        Helper.SetPageTitle(this, 1231);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        Helper.ShowExportToolbarOnCertificationPages(this, true, "CertificationPrint/CertificationsignOffPrint.aspx", 1231, WebEnums.CertificationType.Certification);
        bool isShowContent = Helper.ShowHideContentOnCertificationPages(this, eARTPages);
        if (isShowContent)
        {
            CallEveryTime();
            if (!IsPostBack)
            {
                CallFirstTime();
            }
        }
    }
    protected void ompage_ReconciliationPeriodChangedEventHandler(object sender, EventArgs e)
    {
        if (IsPostBack)
        {
            bool isShowContent = Helper.ShowHideContentOnCertificationPages(this, eARTPages);
            if (isShowContent)
            {
                CallEveryTime();
                CallFirstTime();
            }
        }
    }

    private void CallEveryTime()
    {
        //TODO: handle if coming from certificationStatus page , also breadCrumbs
        SetPrivateVariableValue();
    }
    private void CallFirstTime()
    {
        //TODOD: get in text of month format, as // "(As of December 30, 2009)";
        lblCertificationDate.Text = "(" + string.Format(LanguageUtil.GetValue(1839), Helper.GetDisplayDate(SessionHelper.CurrentReconciliationPeriodEndDate)) + ")";
        lblCertificationVerbiage.Text = CertificationHelper.GetCertificationVerbiage(eCertificationType, userName, roleName);
        HandleFormModeForCertification();
        HandleCertificationSignOffDate();
    }

    private void HandleFormModeForCertification()
    {
        WebEnums.FormMode eFormMode = Helper.GetFormModeForCertification(eARTPages);
        if (eFormMode == WebEnums.FormMode.ReadOnly || _IsUserFromQueryString == true)
        {
            btnAgree.Enabled = false;
            txtAdditionalComments.Visible = false;
            lblAdditionalCommentsValue.Visible = true;
        }
        else
            if (eFormMode == WebEnums.FormMode.Edit)
            {
                btnAgree.Enabled = true;
                txtAdditionalComments.Visible = true;
                lblAdditionalCommentsValue.Visible = false;
            }
    }

    private void HandleCertificationSignOffDate()
    {
        DateTime? signOffDate = null;
        string signOffComment = "";
        signOffDate = CertificationHelper.GetCertificationSignOffDateAndComment(eCertificationType, out signOffComment, _UserID, _RoleID);
        if (signOffDate.HasValue)
        {
            MakeSignatureVisible(signOffDate, userName);
            //txtAdditionalComments.Visible = false;
            //lblAdditionalCommentsValue.Visible = true;
        }
        else
        {
            ucSignature.Visible = false;
        }
        lblAdditionalCommentsValue.Text = signOffComment;
    }
    private void SetPrivateVariableValue()
    {
        this._CompanyID = SessionHelper.CurrentCompanyID.Value;
        this._ReconciliationPeriodID = SessionHelper.CurrentReconciliationPeriodID.Value;
        if (!string.IsNullOrEmpty(Request.QueryString[QueryStringConstants.User_ID]))
        {
            this._UserID = Convert.ToInt32(Request.QueryString[QueryStringConstants.User_ID]);
            _IsUserFromQueryString = true;
        }
        if (!string.IsNullOrEmpty(Request.QueryString[QueryStringConstants.ROLE_ID]))
            this._RoleID = Convert.ToInt16(Request.QueryString[QueryStringConstants.ROLE_ID]);
        if (_UserID > 0 && _RoleID > 0)
        {
            _IsUserFromQueryString = true;
        }
        else
        {
            this._UserID = SessionHelper.CurrentUserID.Value;
            this._RoleID = SessionHelper.CurrentRoleID.Value;
            _IsUserFromQueryString = false;
        }
        userName = Helper.GetUserFullName(_UserID);
        roleName = Helper.GetRoleName(_RoleID);
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (Request.QueryString[QueryStringConstants.User_ID] != null)
        {
            Helper.SetBreadcrumbs(this, 1072, 1464, 1231);
        }
    }

    protected void btnAgree_Click(object sender, EventArgs e)
    {
        DateTime? signOffDate = SaveInDB();
        MakeSignatureVisible(signOffDate, userName);
    }

    private void MakeSignatureVisible(DateTime? signatureDate, string userName)
    {
        ucSignature.Visible = true;
        CertificationHelper.ShowHideSignature(ucSignature, signatureDate, userName);
        btnAgree.Visible = false;
        txtAdditionalComments.Visible = false;
        lblAdditionalCommentsValue.Text = txtAdditionalComments.Text;
        lblAdditionalCommentsValue.Visible = true;
       
    }

    private DateTime? SaveInDB()
    {
        CertificationSignOffInfo oCertificationSignOffInfo = new CertificationSignOffInfo();
        oCertificationSignOffInfo.CompanyID = _CompanyID;
        oCertificationSignOffInfo.CertificationTypeID = (short)eCertificationType;
        oCertificationSignOffInfo.SignOffComments = txtAdditionalComments.Text;
        oCertificationSignOffInfo.UserID = _UserID;
        oCertificationSignOffInfo.RoleID = _RoleID;
        oCertificationSignOffInfo.ReconciliationPeriodID = SessionHelper.CurrentReconciliationPeriodID;
        oCertificationSignOffInfo.SignOffDate = DateTime.Now;
        //oCertificationSignOffInfo.CertificationVerbiageID = 100;  // _CertificationVerbiageID;//TODO:
        //oCertificationSignOffInfo.UserName = Helper.GetUserFullName();
        ICertification oCertificationClient = RemotingHelper.GetCertificationObject();
        oCertificationClient.SaveCertificationSignoffDetail(oCertificationSignOffInfo,Helper.GetAppUserInfo());

        //Commented by Vinay as Now Review notes should be deleted when period is closed
        //Delete review notes if certification is completed
        //IGLData oGLDataClient = RemotingHelper.GetGLDataObject();
        //if (this.IsUserLastInCertificationHierarchy())
        //{
        //    oGLDataClient.DeleteReviewNotesAfterCertification(SessionHelper.CurrentReconciliationPeriodID.Value, WebConstants.REVISED_BY_FIELD_FOR_REVIEW_NOTE_DELETION, DateTime.Now);
        //}
        return oCertificationSignOffInfo.SignOffDate;
    }

    private bool IsUserLastInCertificationHierarchy()
    {
        if (Helper.IsCapabilityActivatedForCurrentRecPeriod(ARTEnums.Capability.CEOCFOCertification, false) == true)
        {
            if (SessionHelper.CurrentRoleID == (short)ARTEnums.UserRole.CEO_CFO)
            {
                return true;
            }
        }
        else if (Helper.IsCapabilityActivatedForCurrentRecPeriod(ARTEnums.Capability.CertificationActivation, false) == true)
        {
            if (Helper.IsCapabilityActivatedForCurrentRecPeriod(ARTEnums.Capability.DualLevelReview))
            {
                if (SessionHelper.CurrentRoleID == (short)ARTEnums.UserRole.APPROVER)
                {
                    return true;
                }
            }
            else
            {
                if (SessionHelper.CurrentRoleID == (short)ARTEnums.UserRole.REVIEWER)
                {
                    return true;
                }
            }
        }

        return false;
    }

    public override string GetMenuKey()
    {
        return "AccountCertification";
    }


}//end of class
