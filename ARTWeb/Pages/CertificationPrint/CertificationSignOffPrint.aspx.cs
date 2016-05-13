using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SkyStem.Language.LanguageUtility;
using SkyStem.ART.Web.Utility;
using SkyStem.ART.Web.Data;
using SkyStem.ART.Web.Classes;

public partial class Pages_CertificationPrint_CertificationSignOffPrint : PopupPageBase
{
    private short _RoleID;
    private short _CertificationTypeID;
    private int _UserID;

    protected void Page_Load(object sender, EventArgs e)
    {
        lblCertificationDate.Text = "(" + string.Format(LanguageUtil.GetValue(1839), Helper.GetDisplayDate(SessionHelper.CurrentReconciliationPeriodEndDate)) + ")";

        this._UserID = Convert.ToInt32(Request.QueryString[QueryStringConstants.User_ID]);
        this._RoleID = Convert.ToInt16(Request.QueryString[QueryStringConstants.ROLE_ID]);
        this._CertificationTypeID = Convert.ToInt16(Request.QueryString[QueryStringConstants.CERTIFICATION_TYPE_ID]);

        WebEnums.CertificationType eCertificationType = (WebEnums.CertificationType)System.Enum.Parse(typeof(WebEnums.CertificationType), this._CertificationTypeID.ToString());

        string userName = Helper.GetUserFullName(_UserID);
        string roleName = Helper.GetRoleName(_RoleID);
        lblCertificationVerbiage.Text = CertificationHelper.GetCertificationVerbiage(eCertificationType, userName, roleName);
    }
}
