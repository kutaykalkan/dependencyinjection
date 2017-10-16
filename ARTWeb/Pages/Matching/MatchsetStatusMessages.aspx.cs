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
using SkyStem.ART.Web.Utility;
using SkyStem.ART.Web.Data;
using SkyStem.ART.Web.Classes;
using SkyStem.ART.Client.Exception;
using SkyStem.ART.Client.Model.Matching;
using SkyStem.ART.Client.Params.Matching;

public partial class Pages_Matching_MatchsetStatusMessages : PageBaseMatching
{
    Int64? _MatchSetID = null;
    long? _GlDataID = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        Helper.SetPageTitle(this, 2422);

        try
        {
            GetQueryStringValues();
            if (!Page.IsPostBack)
            {
                LoadData();
            }
        }
        catch (ARTException ex)
        {
            Helper.ShowErrorMessage(this, ex);
        }
        catch (Exception ex)
        {
            Helper.ShowErrorMessage(this, ex);
        }

    }
    private void GetQueryStringValues()
    {
        if (!String.IsNullOrEmpty(Request.QueryString[QueryStringConstants.MATCH_SET_ID]))
        {
            _MatchSetID = Convert.ToInt64(Request.QueryString[QueryStringConstants.MATCH_SET_ID]);
        }

        if (!String.IsNullOrEmpty(Request.QueryString[QueryStringConstants.GLDATA_ID]))
        {
            _GlDataID = Convert.ToInt64(Request.QueryString[QueryStringConstants.GLDATA_ID]);
        }
    }
    private MatchSetHdrInfo GetMatchSetStatusMessageFromDB()
    {
        MatchSetHdrInfo oMatchSetHdrInfo = null;
        MatchingParamInfo oMatchingParamInfo = new MatchingParamInfo();
        oMatchingParamInfo.MatchSetID = _MatchSetID;
        if (_GlDataID.HasValue)
            oMatchingParamInfo.GLDataID = _GlDataID;
        oMatchingParamInfo.RecPeriodID = SessionHelper.CurrentReconciliationPeriodID;
        //Get Value from DB
        oMatchSetHdrInfo = MatchingHelper.GetMatchSetStatusMessage(oMatchingParamInfo);
        return oMatchSetHdrInfo;
    }

    private void LoadData()
    {

        MatchSetHdrInfo oMatchSetHdrInfo = GetMatchSetStatusMessageFromDB();
        if (oMatchSetHdrInfo != null)
        {

            WebEnums.MatchingStatus eDataImportStatus;
            eDataImportStatus = (WebEnums.MatchingStatus)System.Enum.Parse(typeof(WebEnums.MatchingStatus), oMatchSetHdrInfo.MatchingStatusID.Value.ToString());
            pnlSuccess.Visible = false;
            pnlWarning.Visible = false;
            pnlFailureMessages.Visible = false;
            // btnYes.Visible = false;
            lblMatchsetNameValue.Text = Helper.GetDisplayStringValue(oMatchSetHdrInfo.MatchSetName);
            lblMDescValue.Text = Helper.GetDisplayStringValue(oMatchSetHdrInfo.MatchSetDescription);
            lblMatchsetRefValue.Text = Helper.GetDisplayStringValue(oMatchSetHdrInfo.MatchSetRef);
            switch (eDataImportStatus)
            {
                case WebEnums.MatchingStatus.Success:
                    //  LoadSuccessPanel(oMatchSetHdrInfo);
                    break;

                case WebEnums.MatchingStatus.Error:
                    pnlFailureMessages.Visible = true;
                    imgFailure.Visible = true;
                    lblStatusHeading.LabelID = 1051;
                    lblMessage.LabelID = 2363;
                    lblFailureMessages.Text = FormatFailureMessage(oMatchSetHdrInfo.Message);
                    break;


                case WebEnums.MatchingStatus.SevereWarning:
                    /* If Force commit, is already enabled and Status = Warning
                     * - Means User has already put the file for Force Commit 
                     * - Show a similar message
                     */
                    // LoadWarningPanel(oMatchSetHdrInfo);
                    break;

                case WebEnums.MatchingStatus.InProgress:
                    imgProcessing.Visible = true;
                    lblStatusHeading.LabelID = 1620;
                    lblMessage.LabelID = 1619;
                    lblMessage.FormatString = "";
                    break;

                case WebEnums.MatchingStatus.Draft:
                    imgDraft.Visible = true;
                    lblStatusHeading.LabelID = 2209;
                    lblMessage.LabelID = 1783;
                    lblMessage.FormatString = "";
                    break;
            }
        }
    }

    //private void LoadSuccessPanel(MatchingSourceDataImportHdrInfo oMatchingSourceDataImportHdrInfo)
    //{
    //    pnlSuccess.Visible = true;

    //    imgSuccess.Visible = true;
    //    lblStatusHeading.LabelID = 1050;

    //    lblNoOfRecordsValue.Text = Helper.GetDisplayIntegerValue(oMatchingSourceDataImportHdrInfo.RecordsImported);
    //    lblForceCommitDateValue.Text = Helper.GetDisplayDateTime(oMatchingSourceDataImportHdrInfo.ForceCommitDate);

    //    lblMessage.LabelID = 1618;
    //    lblMessage.FormatString = "";
    //}

    //private void LoadWarningPanel(MatchingSourceDataImportHdrInfo oMatchingSourceDataImportHdrInfo)
    //{
    //    pnlWarning.Visible = true;
    //    pnlSuccess.Visible = true;
    //    pnlFailureMessages.Visible = true;

    //    imgWarning.Visible = true;
    //    lblStatusHeading.LabelID = 1546;
    //    lblMessage.LabelID = 1624;

    //    lblNoOfRecordsValue.Text = Helper.GetDisplayIntegerValue(oMatchingSourceDataImportHdrInfo.RecordsImported);
    //    lblForceCommitDateValue.Text = Helper.GetDisplayDateTime(oMatchingSourceDataImportHdrInfo.ForceCommitDate);
    //    lblFailureMessages.Text = FormatFailureMessage(oMatchingSourceDataImportHdrInfo.Message);

    //    if (oMatchingSourceDataImportHdrInfo.ForceCommitDate == null)
    //    {
    //        btnYes.Visible = true;
    //        lblConfirmUpload.LabelID = 1548;
    //    }
    //    else
    //    {
    //        lblConfirmUpload.LabelID = 1784;
    //    }

    //}

    private string FormatFailureMessage(string msg)
    {
        msg = msg.Replace(" , ", "<br>");
        msg = msg.Replace(" ,", "<br>");
        msg = msg.Replace(",", "<br>");
        return msg;
    }

    public override string GetMenuKey()
    {
        return "MatchSetStatusMessages";
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        //Response.Redirect("ViewMatchSet.aspx");
        SessionHelper.RedirectToUrl("ViewMatchSet.aspx");
        return;
    }

    //protected void btnYes_Click(object sender, EventArgs e)
    //{
    //    /*
    //     * 1. Update DB to Mark as Force Commite
    //     * 2. Redirect to Matching Source Data Import Status Page
    //     * 
    //     */

    //    try
    //    {
    //        MatchingParamInfo oMatchingParamInfo = new MatchingParamInfo();
    //        oMatchingParamInfo.MatchingSourceDataImportID = _MatchingSourceDataImportID;

    //        oMatchingParamInfo.DataImportStatusID = (int)WebEnums.DataImportStatus.Submitted;
    //        oMatchingParamInfo.IsForceCommit = true;
    //        oMatchingParamInfo.ForceCommitDate = DateTime.Now;
    //        oMatchingParamInfo.DateRevised = DateTime.Now;
    //        oMatchingParamInfo.RevisedBy = SessionHelper.CurrentUserLoginID;

    //        MatchingHelper.UpdateMatchingSourceDataImportForForceCommit(oMatchingParamInfo);

    //        string url = GetUrlForStatusPage();
    //        url += "?" + QueryStringConstants.CONFIRMATION_MESSAGE_LABEL_ID + "=1784";
    //        Response.Redirect(url);
    //    }
    //    catch (ARTException ex)
    //    {
    //        Helper.ShowErrorMessage(this, ex);
    //    }
    //    catch (Exception ex)
    //    {
    //        Helper.ShowErrorMessage(this, ex);
    //    }
    //}

    protected void Page_PreRender(object sender, EventArgs e)
    {
        Helper.SetBreadcrumbs(this, 2185, 2422);
    }
}
