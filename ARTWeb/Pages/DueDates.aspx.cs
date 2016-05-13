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
using SkyStem.ART.Client.Model;
using SkyStem.ART.Web.Utility;
using SkyStem.ART.Web.Classes;
using SkyStem.Language.LanguageUtility;
using SkyStem.ART.Web.Data;
using System.Collections.Generic;
using SkyStem.ART.Client.Data;

public partial class Pages_DueDates : PopupPageBaseRecPeriod
{

    #region Variables & Constants
    #endregion
    #region Properties
    #endregion
    #region Delegates & Events
    #endregion
    #region Page Events
    protected void Page_Load(object sender, EventArgs e)
    {
        PopupHelper.SetPageTitle(this, 1368);

        lblRecPeriod.Text = string.Format(LanguageUtil.GetValue(1826), Helper.GetDisplayDate(SessionHelper.CurrentReconciliationPeriodEndDate)) + ":";

        ReconciliationPeriodInfo oReconciliationPeriodInfo = Helper.GetRecPeriodInfo(SessionHelper.CurrentReconciliationPeriodID);

        lblPreparerDueDate.Text = Helper.GetDisplayDate(oReconciliationPeriodInfo.PreparerDueDate);
        lblReviewerDueDate.Text = Helper.GetDisplayDate(oReconciliationPeriodInfo.ReviewerDueDate);

        lblApproverDueDate.Text = Helper.GetDisplayDate(oReconciliationPeriodInfo.ApproverDueDate);
        lblCertificationStartDate.Text = Helper.GetDisplayDate(oReconciliationPeriodInfo.CertificationStartDate);

        trCertStartDate.Visible = Helper.GetFeatureCapabilityModeForCurrentRecPeriod(WebEnums.Feature.Certification, ARTEnums.Capability.CertificationActivation) == WebEnums.FeatureCapabilityMode.Visible;

        if (oReconciliationPeriodInfo.AllowCertificationLockdown != null
            && oReconciliationPeriodInfo.AllowCertificationLockdown.Value)
        {
            lblRecCloseOrCertDueDateTitle.LabelID = 1186;
            lblRecCloseOrCertDueDate.Text = Helper.GetDisplayDate(oReconciliationPeriodInfo.CertificationLockDownDate);
        }
        else
        {
            lblRecCloseOrCertDueDateTitle.LabelID = 1825;
            lblRecCloseOrCertDueDate.Text = Helper.GetDisplayDate(oReconciliationPeriodInfo.ReconciliationCloseDate);
        }
        if (SessionHelper.CurrentReconciliationPeriodID.HasValue)
            if (Helper.IsDueDatesByAccountConfiuredForRecPeriodID(SessionHelper.CurrentReconciliationPeriodID.Value))
            {
                pnlPRADueDates.Visible = false;
            }
            else
            {
                pnlPRADueDates.Visible = true;
            }
    }
    #endregion
    #region Grid Events
    #endregion
    #region Other Events
    #endregion
    #region Validation Control Events
    #endregion
    #region Private Methods
    #endregion
    #region Other Methods
    #endregion
   
}
