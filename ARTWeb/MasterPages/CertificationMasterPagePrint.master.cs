using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SkyStem.ART.Web.Data;
using SkyStem.ART.Web.Classes;
using SkyStem.ART.Web.Utility;
using SkyStem.ART.Client.IServices;
using SkyStem.ART.Client.Model;
using SkyStem.Language.LanguageUtility;

public partial class MasterPages_CertificationMasterPagePrint : CertificationMasterPageBase
{
    //private int _CompanyID;
    private short _RoleID;
    private short _CertificationTypeID;
    private int _UserID;

    protected void Page_Load(object sender, EventArgs e)
    {
        int pageTitleLabelID = Convert.ToInt32(Request.QueryString[QueryStringConstants.PAGE_TITLE_ID]);
        lblPageTitle.LabelID = pageTitleLabelID;

        this._UserID = Convert.ToInt32(Request.QueryString[QueryStringConstants.User_ID]);
        this._RoleID = Convert.ToInt16(Request.QueryString[QueryStringConstants.ROLE_ID]);
        this._CertificationTypeID = Convert.ToInt16(Request.QueryString[QueryStringConstants.CERTIFICATION_TYPE_ID]);

        WebEnums.CertificationType eCertificationType = (WebEnums.CertificationType)System.Enum.Parse(typeof(WebEnums.CertificationType), this._CertificationTypeID.ToString());

        string userName = Helper.GetUserFullName(_UserID);
        string roleName = Helper.GetRoleName(_RoleID);

        if (eCertificationType == WebEnums.CertificationType.Certification)
        {
            trCertificationVerbiage.Visible = false;
        }
        else
        {
            trCertificationVerbiage.Visible = true;
            lblCertificationVerbiage.Text = CertificationHelper.GetCertificationVerbiage(eCertificationType, userName, roleName);
        }

        // Show Additional Comment
        DateTime? dtSignOff = null;
        string signOffComment = "";
        dtSignOff = CertificationHelper.GetCertificationSignOffDateAndComment(eCertificationType, out signOffComment, _UserID, _RoleID);
        CertificationHelper.ShowHideSignature(ucSignature, dtSignOff, userName);
        lblAdditionalCommentsValue.Text = signOffComment;

        //Page.ClientScript.RegisterStartupScript(this.GetType(), "LoadHtml", "LoadCertificationDataAndPrintPage();", true);
    }

}
