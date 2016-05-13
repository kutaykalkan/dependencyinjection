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
using SkyStem.ART.Client.Model;
using SkyStem.ART.Web.Data;
using System.Collections.Generic;
using SkyStem.ART.Client.IServices;
using SkyStem.ART.Client.Data;

public partial class Pages_CloseRecPeriod : PopupPageBase
{
    #region Variables & Constants
    string _ParentHiddenField = null;
    int? _RecPeriodID = null;
    DateTime? _PeriodEndDate = null;
    #endregion

    #region Properties
    #endregion

    #region Delegates & Events
    #endregion

    #region Page Events
    protected void Page_Load(object sender, EventArgs e)
    {
        PopupHelper.SetPageTitle(this, 2090);
        _RecPeriodID = Convert.ToInt32(Request.QueryString[QueryStringConstants.REC_PERIOD_ID]);
        _PeriodEndDate = Convert.ToDateTime(Request.QueryString[QueryStringConstants.REC_PERIODC_END_DATE]);
        if (!Page.IsPostBack)
        {
            LoadData();

        }
    }
    #endregion

    #region Grid Events
    #endregion

    #region Other Events
    protected void btnOk_Click(object sender, EventArgs e)
    {
        int rowsAffected = 0;
        Int16? RecPeriodStatus = (Int16?)WebEnums.RecPeriodStatus.Closed;
        DateTime? RevisedTime = DateTime.Now;
        IReconciliationPeriod oReconciliationPeriodClient = RemotingHelper.GetReconciliationPeriodObject();
        rowsAffected = oReconciliationPeriodClient.CloseRecPeriodByRecPeriodIdAndComanyID(_RecPeriodID, RecPeriodStatus, RevisedTime, SessionHelper.CurrentUserLoginID, (short)ARTEnums.ActionType.ForceCloseRecPeriodFromUI, (short)ARTEnums.ChangeSource.RecPeriodStatusChange, Helper.GetAppUserInfo());

        if (rowsAffected > 0)
        {
            // Raise Alert for Open Period for Reconciliation
            AlertHelper.RaiseAlert(WebEnums.Alert.PeriodHasClosed, _RecPeriodID, _PeriodEndDate, null, null, SessionHelper.CurrentRoleID.Value, null);
        }


        if (this._ParentHiddenField != null)
        {
            this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "SetHiddenFieldStatus", ScriptHelper.GetJSToSetParentWindowElementValue(this._ParentHiddenField, "1")); // 1 means Reload data of GridVieww
        }
        this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "ClosePopupAndRefreshParentPage", ScriptHelper.GetJSForClosePopupAndSubmitParentPage());

        //this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "ClosePopupAndRefreshParentPage", ScriptHelper.GetJSForClosePopupAndSubmitParentPage());

        //this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "return ClosePopupWithoutRefreshParentPage", ScriptHelper.GetJSForPopupClose());

    }
    #endregion

    #region Validation Control Events
    #endregion

    #region Private Methods
    private void LoadData()
    {

        if (Request.QueryString[QueryStringConstants.PARENT_HIDDEN_FIELD] != null)
            this._ParentHiddenField = Request.QueryString[QueryStringConstants.PARENT_HIDDEN_FIELD];
        AccountCertificationStatusInfo oAccountCertificationStatusInfo = null;
        List<AccountStatusInfo> oAccountStatusInfoCollection = null;
        CertificationStatusInfo oCertificationStatusInfo = null;
        AccountStatusInfo oAccountStatusInfo = null;
        AccountStatusInfo oAccountStatusSRAInfo = null;
        oAccountCertificationStatusInfo = (AccountCertificationStatusInfo)Session[SessionConstants.ACCOUNT_CERTIFICATION_STATUS_INFO_OBJECT];
        if (oAccountCertificationStatusInfo != null)
        {
            oAccountStatusInfoCollection = oAccountCertificationStatusInfo.oAccountStatusInfoCollection;
            oCertificationStatusInfo = oAccountCertificationStatusInfo.oCertificationStatusInfo;
        }
        if (oAccountStatusInfoCollection != null)
        {

            oAccountStatusInfo = (from obj in oAccountStatusInfoCollection
                                  where obj.IsSRA == false
                                  select obj).FirstOrDefault();
            oAccountStatusSRAInfo = (from obj in oAccountStatusInfoCollection
                                     where obj.IsSRA == true
                                     select obj).FirstOrDefault();
        }

        // initialize with "-"
        lblAccountStatusValue.Text = Helper.GetDisplayStringValue(null);
        lblSRAStatusValue.Text = Helper.GetDisplayStringValue(null);
        lblCertStatusValue.Text = Helper.GetDisplayStringValue(null);

        string rccyCode = SessionHelper.ReportingCurrencyCode;

        if (oAccountStatusInfo != null)
        {
            lblAccountStatusValue.Text = Helper.GetDisplayIntegerValue(oAccountStatusInfo.UnReconciledAccountsCount) + " (" + Helper.GetDisplayPercentageValue(oAccountStatusInfo.UnReconciledAccountsPercentage) + ", " + rccyCode + " " + Helper.GetDisplayDecimalValue(oAccountStatusInfo.UnReconciledAccountsDollarAmmount) + " )";
            lblMessage.LabelID = 2096;
        }
        else
        {
            oAccountStatusInfo = new AccountStatusInfo();
            lblAccountStatusValue.Text = Helper.GetDisplayIntegerValue(oAccountStatusInfo.UnReconciledAccountsCount) + " (" + Helper.GetDisplayPercentageValue(oAccountStatusInfo.UnReconciledAccountsPercentage) + ", " + rccyCode + " " + Helper.GetDisplayDecimalValue(oAccountStatusInfo.UnReconciledAccountsDollarAmmount) + " )";
            lblMessage.LabelID = 2221;

        }
        if (oAccountStatusSRAInfo != null)
        {
            lblSRAStatusValue.Text = Helper.GetDisplayIntegerValue(oAccountStatusSRAInfo.UnReconciledAccountsCount) + " (" + Helper.GetDisplayPercentageValue(oAccountStatusSRAInfo.UnReconciledAccountsPercentage) + ", " + rccyCode + " " + Helper.GetDisplayDecimalValue(oAccountStatusSRAInfo.UnReconciledAccountsDollarAmmount) + " )";
            lblMessage.LabelID = 2096;
        }
        else
        {
            oAccountStatusSRAInfo = new AccountStatusInfo();
            lblSRAStatusValue.Text = Helper.GetDisplayIntegerValue(oAccountStatusSRAInfo.UnReconciledAccountsCount) + " (" + Helper.GetDisplayPercentageValue(oAccountStatusSRAInfo.UnReconciledAccountsPercentage) + ", " + rccyCode + " " + Helper.GetDisplayDecimalValue(oAccountStatusSRAInfo.UnReconciledAccountsDollarAmmount) + " )";
            lblMessage.LabelID = 2221;


        }
        if (Helper.GetFeatureCapabilityMode(WebEnums.Feature.Certification, ARTEnums.Capability.CertificationActivation, _RecPeriodID) == WebEnums.FeatureCapabilityMode.Visible)
        {
            pnlCertStatus.Visible = true;
            lblCertStatusValue.Text = Helper.GetDisplayIntegerValue(oCertificationStatusInfo.UnCertifiedAccountsCount) + " (" + Helper.GetDisplayPercentageValue(oCertificationStatusInfo.UnCertifiedAccountsPercentage) + ", " + rccyCode + " " + Helper.GetDisplayDecimalValue(oCertificationStatusInfo.UnCertifiedAccountsDollarAmmount) + " )";
            if (oCertificationStatusInfo.UnCertifiedAccountsCount == null)
            {
                lblMessage.LabelID = 2221;
            }
            else
            {
                lblMessage.LabelID = 2095;
            }

        }
        else
        {
            //lblMessage.LabelID = 2096;
            pnlCertStatus.Visible = false;
        }

    }
    #endregion

    #region Other Methods
    #endregion
   
}
