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
using SkyStem.ART.Client.Model;
using SkyStem.ART.Client.IServices;
using System.Collections.Generic;

using SkyStem.ART.Web.Utility;
using SkyStem.Language.LanguageUtility;
using SkyStem.ART.Web.Data;
using System.Text;
using SkyStem.ART.Client.Exception;
using SkyStem.ART.Client.Data;

public partial class Pages_CertificationVerbiage : PageBaseRecPeriod
{
    protected void Page_Init(object sender, EventArgs e)
    {
        MasterPageBase ompage = (MasterPageBase)this.Master;
        ompage.ReconciliationPeriodChangedEventHandler += new EventHandler(ompage_ReconciliationPeriodChangedEventHandler);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        //Helper.SetPageTitle(this, 1832);
        Helper.SetPageTitle(this, 1209);

        if (Helper.IsCapabilityActivatedForCurrentRecPeriod(ARTEnums.Capability.CertificationActivation, true))
        {
            pnlContentPane.Visible = true;
            pnlMessagePane.Visible = false;

            if (!Page.IsPostBack)
            {
                this.PopulateControls();
            }

            if (Helper.DisablePageBasedOnRecPeriodStatus())
            {
                pnlContent.Enabled = false;
                btnSave.Enabled = false;
                txtDescriptionCertification.EditModes = Telerik.Web.UI.EditModes.Preview;
                txtDescriptionException.EditModes = Telerik.Web.UI.EditModes.Preview;
                txtDescription.EditModes = Telerik.Web.UI.EditModes.Preview;
            }
            else
            {
                pnlContent.Enabled = true;
                btnSave.Enabled = true;
            }
        }
        else
        {
            pnlContentPane.Visible = false;
            pnlMessagePane.Visible = true;
        }
    }
    public override string GetMenuKey()
    {
        return "CertificationSettings";
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        int? labelID = null;
        ICertificationStatus oCertificationStatusClient = RemotingHelper.GetCertificationStatusObject();
        List<CertificationVerbiageInfo> oCertificationVerbiageInfoCollection = new List<CertificationVerbiageInfo>();
        UserHdrInfo oUserHdrInfo = SessionHelper.GetCurrentUser();
        if (txtDescriptionCertification.Content != string.Empty)
        {
            CertificationVerbiageInfo oCertificationVerbiageInfo = new CertificationVerbiageInfo();

            oCertificationVerbiageInfo.CertificationTypeID = (short?)WebEnums.CertificationType.CertificationBalances;
            oCertificationVerbiageInfo.CertificationVerbiage = txtDescriptionCertification.Content;
            oCertificationVerbiageInfo.CompanyID = SessionHelper.CurrentCompanyID;
            labelID = Convert.ToInt32(txtDescriptionCertification.Attributes[WebConstants.ATTRIBUTE_LABEL_ID]);
            oCertificationVerbiageInfo.CertificationVerbiageLabelID = (int)LanguageUtil.InsertPhrase(txtDescriptionCertification.Content, null, 1, (int)SessionHelper.CurrentCompanyID, SessionHelper.GetUserLanguage(), 4, labelID);
            oCertificationVerbiageInfo.DateAdded = DateTime.Now;
            oCertificationVerbiageInfo.AddedBy = oUserHdrInfo.LoginID;
            oCertificationVerbiageInfo.IsActive = true;
            oCertificationVerbiageInfoCollection.Add(oCertificationVerbiageInfo);

        }
        if (txtDescriptionException.Text != string.Empty)
        {
            CertificationVerbiageInfo oCertificationVerbiageInfo = new CertificationVerbiageInfo();
            oCertificationVerbiageInfo.CertificationTypeID = (short?)WebEnums.CertificationType.ExceptionCertification;
            oCertificationVerbiageInfo.CertificationVerbiage = txtDescriptionException.Content;
            oCertificationVerbiageInfo.CompanyID = SessionHelper.CurrentCompanyID;
            labelID = Convert.ToInt32(txtDescriptionException.Attributes[WebConstants.ATTRIBUTE_LABEL_ID]);
            oCertificationVerbiageInfo.CertificationVerbiageLabelID = (int)LanguageUtil.InsertPhrase(txtDescriptionException.Content, null, 1, (int)SessionHelper.CurrentCompanyID, SessionHelper.GetUserLanguage(), 4, labelID);
            oCertificationVerbiageInfo.DateAdded = DateTime.Now;
            oCertificationVerbiageInfo.AddedBy = oUserHdrInfo.LoginID;
            oCertificationVerbiageInfo.IsActive = true;
            oCertificationVerbiageInfoCollection.Add(oCertificationVerbiageInfo);

        }
        if (txtDescription.Text != string.Empty)
        {
            CertificationVerbiageInfo oCertificationVerbiageInfo = new CertificationVerbiageInfo();
            oCertificationVerbiageInfo.CertificationTypeID = (short?)WebEnums.CertificationType.Certification;
            oCertificationVerbiageInfo.CertificationVerbiage = txtDescription.Content;
            oCertificationVerbiageInfo.CompanyID = SessionHelper.CurrentCompanyID;
            labelID = Convert.ToInt32(txtDescription.Attributes[WebConstants.ATTRIBUTE_LABEL_ID]);
            oCertificationVerbiageInfo.CertificationVerbiageLabelID = (int)LanguageUtil.InsertPhrase(txtDescription.Content, null, 1, (int)SessionHelper.CurrentCompanyID, SessionHelper.GetUserLanguage(), 4, labelID);
            oCertificationVerbiageInfo.DateAdded = DateTime.Now;
            oCertificationVerbiageInfo.AddedBy = oUserHdrInfo.LoginID;
            oCertificationVerbiageInfo.IsActive = true;
            oCertificationVerbiageInfoCollection.Add(oCertificationVerbiageInfo);
        }

        oCertificationStatusClient.InsertCertificationVerbiageInfo(oCertificationVerbiageInfoCollection, (int)SessionHelper.CurrentReconciliationPeriodID,Helper.GetAppUserInfo());
        //Response.Redirect(Helper.GetHomePageUrl(), false);
        //Response.Redirect(Helper.GetHomePageUrl() + "?" + QueryStringConstants.CONFIRMATION_MESSAGE_LABEL_ID + "=1923", false);
        SessionHelper.RedirectToUrl(Helper.GetHomePageUrl() + "?" + QueryStringConstants.CONFIRMATION_MESSAGE_LABEL_ID + "=1923");
        return;

    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        //Helper.GetHomePageUrl();
        //Response.Redirect(Helper.GetHomePageUrl(), false);
        SessionHelper.RedirectToUrl(Helper.GetHomePageUrl());
        return;
    }
    private void LoadDataByRecPeriodID(int companyID, int recPeriodID)
    {
        ICertificationStatus oCertificationStatusClient = RemotingHelper.GetCertificationStatusObject();
        List<CertificationVerbiageInfo> oCertVerbInfoCollection = oCertificationStatusClient.GetCertificationVerbiageByCompanyIDRecPeriodID(companyID, recPeriodID,Helper.GetAppUserInfo());
        LanguageHelper.TranslateCertVerbiage(oCertVerbInfoCollection);
        this.EmptyAllRichTextBoxes();
        foreach (CertificationVerbiageInfo oCertVerbInfo in oCertVerbInfoCollection)
        {
            short certTypeID = oCertVerbInfo.CertificationTypeID.Value;
            switch (certTypeID)
            {
                case (short)WebEnums.CertificationType.CertificationBalances:
                    this.txtDescriptionCertification.Content = oCertVerbInfo.CertificationVerbiage;
                    this.txtDescriptionCertification.Attributes.Add(WebConstants.ATTRIBUTE_LABEL_ID, oCertVerbInfo.CertificationVerbiageLabelID.Value.ToString());
                    break;

                case (short)WebEnums.CertificationType.ExceptionCertification:
                    this.txtDescriptionException.Content = oCertVerbInfo.CertificationVerbiage;
                    this.txtDescriptionException.Attributes.Add(WebConstants.ATTRIBUTE_LABEL_ID, oCertVerbInfo.CertificationVerbiageLabelID.Value.ToString());
                    break;

                case (short)WebEnums.CertificationType.Certification:
                    this.txtDescription.Content = oCertVerbInfo.CertificationVerbiage;
                    this.txtDescription.Attributes.Add(WebConstants.ATTRIBUTE_LABEL_ID, oCertVerbInfo.CertificationVerbiageLabelID.Value.ToString());
                    break;
            }
        }
    }
    private void PopulateControls()
    {
        ICertificationStatus oCertificationStatusClient = RemotingHelper.GetCertificationStatusObject();
        List<DynamicPlaceholderMstInfo> objDynamicPlaceholderMstInfoCollection = oCertificationStatusClient.getAllDynamicPlaceholderMstInfo(Helper.GetAppUserInfo());
        ddlDynamicField.DataSource = objDynamicPlaceholderMstInfoCollection;
        ddlDynamicField.DataTextField = "DynamicPlaceholder";
        ddlDynamicField.DataValueField = "DynamicPlaceholderID";
        ddlDynamicField.DataBind();
        ddlAccount.DataSource = objDynamicPlaceholderMstInfoCollection;
        ddlAccount.DataTextField = "DynamicPlaceholder";
        ddlAccount.DataValueField = "DynamicPlaceholderID";
        ddlAccount.DataBind();
        ddlException.DataSource = objDynamicPlaceholderMstInfoCollection;
        ddlException.DataTextField = "DynamicPlaceholder";
        ddlException.DataValueField = "DynamicPlaceholderID";
        ddlException.DataBind();

        this.LoadDataByRecPeriodID(SessionHelper.CurrentCompanyID.Value, SessionHelper.CurrentReconciliationPeriodID.Value);
    }
    protected void ompage_ReconciliationPeriodChangedEventHandler(object sender, EventArgs e)
    {
        if (IsPostBack)
        {
            this.PopulateControls();
        }
    }
    private void EmptyAllRichTextBoxes()
    {
        this.txtDescriptionCertification.Content = "";
        this.txtDescriptionException.Content = "";
        this.txtDescription.Content = "";
    }
}
