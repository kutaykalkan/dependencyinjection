using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SkyStem.ART.Web.Classes;
using SkyStem.ART.Web.Data;
using SkyStem.ART.Client.Data;
using SkyStem.ART.Web.Utility;

public partial class UserControls_Matching_AccountMatchingButton : UserControlBase
{
    long? _accountID;
    long? _netAccountID;
    long? _gLDataID;
    string _Url;
    public void SetURL(long? accountID, long? netAccountID, long? glDataID, string Url)
    {
        _accountID = accountID;
        _netAccountID = netAccountID;
        _gLDataID = glDataID;
        _Url = Url;

        //if ((WebEnums.UserRole)SessionHelper.CurrentRoleID == WebEnums.UserRole.PREPARER
        //    && ((WebEnums.RecPeriodStatus)SessionHelper.CurrentRecProcessStatusEnum == WebEnums.RecPeriodStatus.Open
        //    || (WebEnums.RecPeriodStatus)SessionHelper.CurrentRecProcessStatusEnum == WebEnums.RecPeriodStatus.InProgress)
        //    && _accountID != null && _accountID > 0)
        //{
        //    imgbtnAccountMatching.Visible = true;
        //}
        //else
        //    imgbtnAccountMatching.Visible = false;
        imgbtnAccountMatching.Visible = !IsCertStartMatching();
    }
    private bool IsCertStartMatching()
    {
        bool Flag = false;

        if (!Helper.IsFeatureActivated(WebEnums.Feature.MatchingEntry, SessionHelper.CurrentReconciliationPeriodID))
        {
            Flag = true;
            return Flag;
        }

        if ((((WebEnums.UserRole)SessionHelper.CurrentRoleID == WebEnums.UserRole.PREPARER)
            || ((WebEnums.UserRole)SessionHelper.CurrentRoleID == WebEnums.UserRole.REVIEWER)
            || ((WebEnums.UserRole)SessionHelper.CurrentRoleID == WebEnums.UserRole.APPROVER)
            || ((WebEnums.UserRole)SessionHelper.CurrentRoleID == WebEnums.UserRole.BACKUP_PREPARER)
            || ((WebEnums.UserRole)SessionHelper.CurrentRoleID == WebEnums.UserRole.AUDIT))
           && (CurrentRecProcessStatus.Value == WebEnums.RecPeriodStatus.Open
           || CurrentRecProcessStatus.Value == WebEnums.RecPeriodStatus.InProgress
           || CurrentRecProcessStatus.Value == WebEnums.RecPeriodStatus.Closed)
           && _accountID != null && _accountID > 0)
            {
                Flag = false;
            }
            else
            {
                Flag = true;
            }
        return Flag;
    }

    //public delegate void BtnAccountMatchingClick();
    //public event BtnAccountMatchingClick OnBtnAccountMatchingClick;
    //protected void imgbtnAccountMatching_Click(object sender, ImageClickEventArgs e)
    //{
    //    if (OnBtnAccountMatchingClick != null)
    //        OnBtnAccountMatchingClick();

    //}
    protected void imgbtnAccountMatching_Click(object sender, ImageClickEventArgs e)
    {
        Session[SessionConstants.PARENT_PAGE_URL] = _Url;
        string queryString = "?" + QueryStringConstants.MATCHING_TYPE_ID + "=" + (int)ARTEnums.MatchingType.AccountMatching;
        if (_accountID.HasValue && _accountID.Value > 0)
            queryString += "&" + QueryStringConstants.ACCOUNT_ID + "=" + _accountID;
        if (_netAccountID.HasValue && _netAccountID.Value > 0)
            queryString += "&" + QueryStringConstants.NETACCOUNT_ID + "=" + _netAccountID;
        if (_gLDataID.HasValue && _gLDataID.Value > 0)
            queryString += "&" + QueryStringConstants.GLDATA_ID + "=" + _gLDataID;
        string RedirectUrl = URLConstants.URL_MATCHING_VIEW_MATCH_SET + queryString;
        Response.Redirect(RedirectUrl);

    }
}
