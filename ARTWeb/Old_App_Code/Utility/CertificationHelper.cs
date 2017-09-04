using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SkyStem.ART.Web.Utility;
using SkyStem.ART.Client.IServices;
using SkyStem.ART.Client.Model;
using SkyStem.Language.LanguageUtility;
using SkyStem.ART.Web.Data;
using Telerik.Web.UI;
using SkyStem.Library.Controls.WebControls;
using System.Web.UI.WebControls;
using System.Web.UI;
using SkyStem.ART.Web.Classes;
using System.Text;
using SkyStem.Language.LanguageUtility.Classes;
using SkyStem.ART.Client.Data;


/// <summary>
/// Summary description for CertificationHelper
/// </summary>
public class CertificationHelper
{
    public CertificationHelper()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public static string GetCertificationVerbiage(WebEnums.CertificationType eCertificationType, string userName, string roleName)
    {
        short? certificationTypeID = (short)eCertificationType;
        string verbiage = "";

        ICertification oCertificationClient = RemotingHelper.GetCertificationObject();
        List<CertificationVerbiageInfo> oCertificationVerbiageInfoCollection = oCertificationClient.GetCertificationVerbiage(SessionHelper.CurrentReconciliationPeriodID, SessionHelper.CurrentCompanyID, certificationTypeID, Helper.GetAppUserInfo());

        if (oCertificationVerbiageInfoCollection != null && oCertificationVerbiageInfoCollection.Count > 0)
        {
            if (oCertificationVerbiageInfoCollection[0].CertificationVerbiageLabelID.HasValue)
            {
                verbiage = LanguageUtil.GetValue(oCertificationVerbiageInfoCollection[0].CertificationVerbiageLabelID.Value);
                verbiage = verbiage.Replace(VerbiagePlaceHolder.USERNAME, GetStringValueToReplacePlaceHolder(userName));//Helper.GetUserFullName());
                verbiage = verbiage.Replace(VerbiagePlaceHolder.PERIODENDDATE, GetStringValueToReplacePlaceHolder(Helper.GetDisplayDate(SessionHelper.CurrentReconciliationPeriodEndDate)));
                verbiage = verbiage.Replace(VerbiagePlaceHolder.ROLENAME, GetStringValueToReplacePlaceHolder(roleName));// Helper.GetCurrentRoleName());
            }
        }
        return verbiage;
    }

    private static string GetStringValueToReplacePlaceHolder(string text)
    {
        return " " + text + " ";
    }

    public static DateTime? GetCertificationSignOffDateAndComment(WebEnums.CertificationType eCertificationType, out string signOffComment, int? userID, short? roleID)
    {
        DateTime? signOffDate = null;
        signOffComment = "";
        ICertification oCertificationClient = RemotingHelper.GetCertificationObject();
        List<CertificationSignOffInfo> oCertificationSignOffInfoCollection = oCertificationClient.GetCertificationSignOff(SessionHelper.CurrentReconciliationPeriodID, userID, roleID, Helper.GetAppUserInfo());
        if (oCertificationSignOffInfoCollection != null && oCertificationSignOffInfoCollection.Count > 0)
        {

            // sign off is done
            switch (eCertificationType)
            {
                case WebEnums.CertificationType.MandatoryReportSignOff:
                    if (oCertificationSignOffInfoCollection[0].MadatoryReportSignOffDate.HasValue)
                    {
                        signOffDate = oCertificationSignOffInfoCollection[0].MadatoryReportSignOffDate;
                    }
                    break;
                case WebEnums.CertificationType.CertificationBalances:
                    if (oCertificationSignOffInfoCollection[0].CertificationBalancesDate.HasValue)
                    {
                        signOffDate = oCertificationSignOffInfoCollection[0].CertificationBalancesDate;
                        signOffComment = oCertificationSignOffInfoCollection[0].CertificationBalancesComments;
                    }
                    break;
                case WebEnums.CertificationType.ExceptionCertification:
                    if (oCertificationSignOffInfoCollection[0].ExceptionCertificationDate.HasValue)
                    {
                        signOffDate = oCertificationSignOffInfoCollection[0].ExceptionCertificationDate;
                        signOffComment = oCertificationSignOffInfoCollection[0].ExceptionCertificationComments;
                    }
                    break;
                case WebEnums.CertificationType.Certification:
                    if (oCertificationSignOffInfoCollection[0].AccountCertificationDate.HasValue)
                    {
                        signOffDate = oCertificationSignOffInfoCollection[0].AccountCertificationDate;
                        signOffComment = oCertificationSignOffInfoCollection[0].AccountCertificationComments;
                    }
                    break;
            }
        }
        return signOffDate;
    }

    public static void ShowHideSignature(UserControlBase ucSignature, DateTime? dtSignOff, string userName)
    {
        if (dtSignOff != null)
        {
            ucSignature.Visible = true;
            ucSignature.SetSignature(userName, dtSignOff);
        }
        else
        {
            ucSignature.Visible = false;
        }
    }



    public static void ItemDataBoundCertificationBalances(GridItemEventArgs e)
    {
        if (e.Item.ItemType == GridItemType.Header)
        {
            GLDataHdrInfo oGLDataAndAccountHdrInfo = (GLDataHdrInfo)e.Item.DataItem;

            Control oControlAmountReportingCurrency = new Control();

            oControlAmountReportingCurrency = (e.Item as GridHeaderItem)["AmountReportingCurrency"].Controls[0];

            if (oControlAmountReportingCurrency is LinkButton)
            {
                ((LinkButton)oControlAmountReportingCurrency).Text = Helper.GetLabelIDValue(1382) + " (" + SessionHelper.ReportingCurrencyCode + ")";
            }
            else
            {
                if (oControlAmountReportingCurrency is LiteralControl)
                {
                    ((LiteralControl)oControlAmountReportingCurrency).Text = Helper.GetLabelIDValue(1382) + " (" + SessionHelper.ReportingCurrencyCode + ")";

                }
            }
        }


        if (e.Item.ItemType == Telerik.Web.UI.GridItemType.Item || e.Item.ItemType == Telerik.Web.UI.GridItemType.AlternatingItem)
        {
            GLDataHdrInfo oGLDataAndAccountHdrInfo = (GLDataHdrInfo)e.Item.DataItem;

            ExHyperLink hlAccountNumber = (ExHyperLink)e.Item.FindControl("hlAccountNumber");
            ExHyperLink hlAccountName = (ExHyperLink)e.Item.FindControl("hlAccountName");
            ExHyperLink hlGLBalance = (ExHyperLink)e.Item.FindControl("hlGLBalance");
            ExHyperLink hlRiskRating = (ExHyperLink)e.Item.FindControl("hlRiskRating");
            ExHyperLink hlIsMaterial = (ExHyperLink)e.Item.FindControl("hlIsMaterial");
            ExHyperLink hlIsKeyAccount = (ExHyperLink)e.Item.FindControl("hlIsKeyAccount");
            ExHyperLink hlPreparer = (ExHyperLink)e.Item.FindControl("hlPreparer");
            ExHyperLink hlReviewer = (ExHyperLink)e.Item.FindControl("hlReviewer");
            ExHyperLink hlApprover = (ExHyperLink)e.Item.FindControl("hlApprover");
            ExHyperLink hlBackupPreparer = (ExHyperLink)e.Item.FindControl("hlBackupPreparer");
            ExHyperLink hlBackupReviewer = (ExHyperLink)e.Item.FindControl("hlBackupReviewer");
            ExHyperLink hlBackupApprover = (ExHyperLink)e.Item.FindControl("hlBackupApprover");


            hlAccountNumber.Text = oGLDataAndAccountHdrInfo.AccountNumber;
            hlAccountName.Text = Helper.GetDisplayStringValue(oGLDataAndAccountHdrInfo.AccountName);
            hlGLBalance.Text = Helper.GetDisplayDecimalValue(oGLDataAndAccountHdrInfo.GLBalanceReportingCurrency);
            hlRiskRating.Text = Helper.GetDisplayStringValue(oGLDataAndAccountHdrInfo.RiskRating);
            hlIsMaterial.Text = Helper.GetDisplayStringValue(oGLDataAndAccountHdrInfo.AccountMateriality);
            hlIsKeyAccount.Text = Helper.GetDisplayStringValue(oGLDataAndAccountHdrInfo.KeyAccount);
            hlPreparer.Text = Helper.GetDisplayStringValue(oGLDataAndAccountHdrInfo.PreparerFullName);
            hlReviewer.Text = Helper.GetDisplayStringValue(oGLDataAndAccountHdrInfo.ReviewerFullName);
            hlApprover.Text = Helper.GetDisplayStringValue(oGLDataAndAccountHdrInfo.ApproverFullName);
            hlBackupPreparer.Text = Helper.GetDisplayStringValue(oGLDataAndAccountHdrInfo.BackupPreparerFullName);
            hlBackupReviewer.Text = Helper.GetDisplayStringValue(oGLDataAndAccountHdrInfo.BackupReviewerFullName);
            hlBackupApprover.Text = Helper.GetDisplayStringValue(oGLDataAndAccountHdrInfo.BackupApproverFullName);

            WebEnums.ARTPages eArtPages = WebEnums.ARTPages.CertificationBalances;
            string url = string.Empty;

            if (oGLDataAndAccountHdrInfo.NetAccountID != null)
            {
                url = AccountViewerHelper.GetHyperlinkForAccountViewer(oGLDataAndAccountHdrInfo.ReconciliationTemplateID, null, oGLDataAndAccountHdrInfo.GLDataID.ToString(), oGLDataAndAccountHdrInfo.NetAccountID.ToString(), oGLDataAndAccountHdrInfo.IsSystemReconcilied, eArtPages);
            }
            else
            {
                url = AccountViewerHelper.GetHyperlinkForAccountViewer(oGLDataAndAccountHdrInfo.ReconciliationTemplateID, oGLDataAndAccountHdrInfo.AccountID.ToString(), oGLDataAndAccountHdrInfo.GLDataID.ToString(), oGLDataAndAccountHdrInfo.NetAccountID.ToString(), oGLDataAndAccountHdrInfo.IsSystemReconcilied, eArtPages);
            }
            hlAccountNumber.NavigateUrl = url;
            hlAccountName.NavigateUrl = url;
            hlGLBalance.NavigateUrl = url;
            hlRiskRating.NavigateUrl = url;
            hlIsMaterial.NavigateUrl = url;
            hlIsKeyAccount.NavigateUrl = url;
            hlPreparer.NavigateUrl = url;
            hlReviewer.NavigateUrl = url;
            hlApprover.NavigateUrl = url;
            hlBackupPreparer.NavigateUrl = url;
            hlBackupReviewer.NavigateUrl = url;
            hlBackupApprover.NavigateUrl = url;
            Helper.SetHyperLinkForOrganizationalHierarchyColumns(url, e);
        }

    }

    public static void ItemDataBoundCertificationException(GridItemEventArgs e)
    {
        if (e.Item.ItemType == GridItemType.Header)
        {
            GLDataHdrInfo oGLDataAndAccountHdrInfo = (GLDataHdrInfo)e.Item.DataItem;

            Control oControlAmountReportingCurrency = new Control();

            oControlAmountReportingCurrency = (e.Item as GridHeaderItem)["AmountReportingCurrency"].Controls[0];

            if (oControlAmountReportingCurrency is LinkButton)
            {
                ((LinkButton)oControlAmountReportingCurrency).Text = Helper.GetLabelIDValue(1382) + " (" + SessionHelper.ReportingCurrencyCode + ")";
            }
            else
            {
                if (oControlAmountReportingCurrency is LiteralControl)
                {
                    ((LiteralControl)oControlAmountReportingCurrency).Text = Helper.GetLabelIDValue(1382) + " (" + SessionHelper.ReportingCurrencyCode + ")";

                }
            }
        }

        if (e.Item.ItemType == Telerik.Web.UI.GridItemType.Item || e.Item.ItemType == Telerik.Web.UI.GridItemType.AlternatingItem)
        {
            GLDataHdrInfo oGLDataAndAccountHdrInfo = (GLDataHdrInfo)e.Item.DataItem;

            ExHyperLink hlAccountNumber = (ExHyperLink)e.Item.FindControl("hlAccountNumber");
            ExHyperLink hlAccountName = (ExHyperLink)e.Item.FindControl("hlAccountName");
            ExHyperLink hlGLBalance = (ExHyperLink)e.Item.FindControl("hlGLBalance");
            ExHyperLink hlRiskRating = (ExHyperLink)e.Item.FindControl("hlRiskRating");
            ExHyperLink hlIsMaterial = (ExHyperLink)e.Item.FindControl("hlIsMaterial");
            ExHyperLink hlIsKeyAccount = (ExHyperLink)e.Item.FindControl("hlIsKeyAccount");
            ExHyperLink hlPreparer = (ExHyperLink)e.Item.FindControl("hlPreparer");
            ExHyperLink hlReviewer = (ExHyperLink)e.Item.FindControl("hlReviewer");
            ExHyperLink hlApprover = (ExHyperLink)e.Item.FindControl("hlApprover");

            ExHyperLink hlUnexplainedVariance = (ExHyperLink)e.Item.FindControl("hlUnexplainedVariance");
            ExHyperLink hlWriteOffOn = (ExHyperLink)e.Item.FindControl("hlWriteOffOn");


            hlAccountNumber.Text = oGLDataAndAccountHdrInfo.AccountNumber;
            hlAccountName.Text = Helper.GetDisplayStringValue(oGLDataAndAccountHdrInfo.AccountName);
            hlGLBalance.Text = Helper.GetDisplayDecimalValue(oGLDataAndAccountHdrInfo.GLBalanceReportingCurrency);
            hlRiskRating.Text = Helper.GetDisplayStringValue(oGLDataAndAccountHdrInfo.RiskRating);//TODO: get from labelID,oGLDataAndAccountHdrInfo.RiskRatingLabelID
            hlIsMaterial.Text = Helper.GetDisplayStringValue(oGLDataAndAccountHdrInfo.AccountMateriality);
            hlIsKeyAccount.Text = Helper.GetDisplayStringValue(oGLDataAndAccountHdrInfo.KeyAccount);
            hlPreparer.Text = Helper.GetDisplayStringValue(oGLDataAndAccountHdrInfo.PreparerFullName);
            hlReviewer.Text = Helper.GetDisplayStringValue(oGLDataAndAccountHdrInfo.ReviewerFullName);
            hlApprover.Text = Helper.GetDisplayStringValue(oGLDataAndAccountHdrInfo.ApproverFullName);

            hlUnexplainedVariance.Text = Helper.GetDisplayDecimalValue(oGLDataAndAccountHdrInfo.UnexplainedVarianceReportingCurrency);
            hlWriteOffOn.Text = Helper.GetDisplayDecimalValue(oGLDataAndAccountHdrInfo.WriteOnOffAmountReportingCurrency);

            WebEnums.ARTPages eArtPages = WebEnums.ARTPages.CertificationException;
            string url = AccountViewerHelper.GetHyperlinkForAccountViewer(oGLDataAndAccountHdrInfo.ReconciliationTemplateID, oGLDataAndAccountHdrInfo.AccountID.ToString(), oGLDataAndAccountHdrInfo.GLDataID.ToString(), oGLDataAndAccountHdrInfo.NetAccountID.ToString(), oGLDataAndAccountHdrInfo.IsSystemReconcilied, eArtPages);
            hlAccountNumber.NavigateUrl = url;
            hlAccountName.NavigateUrl = url;
            hlGLBalance.NavigateUrl = url;
            hlRiskRating.NavigateUrl = url;
            hlIsMaterial.NavigateUrl = url;
            hlIsKeyAccount.NavigateUrl = url;
            hlPreparer.NavigateUrl = url;
            hlReviewer.NavigateUrl = url;
            hlApprover.NavigateUrl = url;
            Helper.SetHyperLinkForOrganizationalHierarchyColumns(url, e);
        }
    }

    /// <summary>
    /// Determines whether [is certification started].
    /// </summary>
    /// <returns>
    ///   <c>true</c> if [is certification started]; otherwise, <c>false</c>.
    /// </returns>
    public static bool IsCertificationStarted()
    {
        bool IsCertificationStarted = false;
        if (SessionHelper.CurrentReconciliationPeriodID.HasValue)
        {

            ICertification oCertificationClient = RemotingHelper.GetCertificationObject();
            IsCertificationStarted = oCertificationClient.GetIsCertificationStarted(SessionHelper.CurrentReconciliationPeriodID, Helper.GetAppUserInfo());
        }


        //ReconciliationPeriodInfo oReconciliationPeriodInfo = Helper.GetRecPeriodInfo(SessionHelper.CurrentReconciliationPeriodID);
        //if (oReconciliationPeriodInfo.IsStopRecAndStartCert.Value)
        //    return true;

        //DateTime? LockDownDate = null;
        //if (oReconciliationPeriodInfo != null)
        //{
        //    WebEnums.FeatureCapabilityMode eFeatureCapabilityMode = Helper.GetFeatureCapabilityMode(WebEnums.Feature.Certification, ARTEnums.Capability.CertificationActivation, SessionHelper.CurrentReconciliationPeriodID);
        //    if (eFeatureCapabilityMode == WebEnums.FeatureCapabilityMode.Visible)
        //    {
        //        //Certification Activated 

        //        LockDownDate = oReconciliationPeriodInfo.CertificationStartDate;
        //    }
        //    else
        //    {
        //        //   Certification is  not Activated 
        //        LockDownDate = oReconciliationPeriodInfo.ReconciliationCloseDate;
        //    }
        //}

        //if (LockDownDate.HasValue)
        //{
        //    if (DateTime.Now.Date >= LockDownDate)
        //    {
        //        return true;
        //    }
        //}


        return IsCertificationStarted;
    }
    public static void NotifyUsersToStartCertification()
    {
        //List<short> selectedRoleIDs = new List<short>();
        //selectedRoleIDs.Add((short)ARTEnums.UserRole.PREPARER);
        //selectedRoleIDs.Add((short)ARTEnums.UserRole.REVIEWER);
        //selectedRoleIDs.Add((short)ARTEnums.UserRole.APPROVER);
        //selectedRoleIDs.Add((short)ARTEnums.UserRole.EXECUTIVE);
        //selectedRoleIDs.Add((short)ARTEnums.UserRole.CONTROLLER);
        //selectedRoleIDs.Add((short)ARTEnums.UserRole.ACCOUNT_MANAGER);
        //selectedRoleIDs.Add((short)ARTEnums.UserRole.FINANCIAL_MANAGER);
        //selectedRoleIDs.Add((short)ARTEnums.UserRole.CEO_CFO);
        //selectedRoleIDs.Add((short)ARTEnums.UserRole.BACKUP_PREPARER);
        //selectedRoleIDs.Add((short)ARTEnums.UserRole.BACKUP_REVIEWER);
        //selectedRoleIDs.Add((short)ARTEnums.UserRole.BACKUP_APPROVER);
        //IUser oUserClient = RemotingHelper.GetUserObject();
        //List<UserHdrInfo> oUserCollection = oUserClient.SelectAllUsersByCompanyIDAndRoleIDsForCurrentRecPeriod(SessionHelper.CurrentCompanyID.Value, selectedRoleIDs, SessionHelper.CurrentUserID, SessionHelper.CurrentRoleID);
        IGLData oGLDataClient = RemotingHelper.GetGLDataObject();
        bool? isAllAccountsReconciled = oGLDataClient.GetIsAllAccountsReconciledForUserAndRole(SessionHelper.CurrentUserID.Value, SessionHelper.CurrentRoleID.Value, SessionHelper.CurrentReconciliationPeriodID.Value, Helper.GetAppUserInfo());
        if (isAllAccountsReconciled.HasValue && isAllAccountsReconciled.Value)
        {
            short CurrentRoleID = SessionHelper.CurrentRoleID.Value;
            CurrentRoleID = 2;// fetch all record 
            IUser oUserClient = RemotingHelper.GetUserObject();
            List<UserHdrInfo> oUserCollection = oUserClient.SearchUser(null, null, null, 0, null, true, SessionHelper.CurrentCompanyID, 
                SessionHelper.CurrentUserID.Value, CurrentRoleID, SessionHelper.CurrentReconciliationPeriodID, 
                SessionHelper.CurrentReconciliationPeriodEndDate, null, null, false, 2, (short)ARTEnums.ActivationStatusType.UserActivationStatus, null, Helper.GetAppUserInfo());
            for (int i = 0; i < oUserCollection.Count - 1; i++)
            {

                MultilingualAttributeInfo oMultilingualAttributeInfo;
                if (oUserCollection[i].DefaultLanguageID.HasValue)
                    oMultilingualAttributeInfo = LanguageHelper.GetMultilingualAttributeInfo(SessionHelper.CurrentCompanyID, oUserCollection[i].DefaultLanguageID);
                else
                    oMultilingualAttributeInfo = LanguageHelper.GetMultilingualAttributeInfo(SessionHelper.CurrentCompanyID, 1033);
                string mailSubject = string.Format("{0}", LanguageUtil.GetValue(2675, oMultilingualAttributeInfo));
                string mailBody = string.Format("{0}", String.Format(LanguageUtil.GetValue(2674, oMultilingualAttributeInfo), Helper.GetDisplayDate(SessionHelper.CurrentReconciliationPeriodEndDate, oMultilingualAttributeInfo)));
                SendMailToUser(oUserCollection[i], mailSubject, mailBody, oMultilingualAttributeInfo);

                if (Helper.IsCapabilityActivatedForCurrentRecPeriod(ARTEnums.Capability.CertificationActivation) || Helper.IsCapabilityActivatedForCurrentRecPeriod(ARTEnums.Capability.CEOCFOCertification))
                {
                    string mailSubjectCert = string.Format("{0}", LanguageUtil.GetValue(2665, oMultilingualAttributeInfo));
                    string mailBodyCert = string.Format("{0}", String.Format(LanguageUtil.GetValue(2666, oMultilingualAttributeInfo), Helper.GetDisplayDate(SessionHelper.CurrentReconciliationPeriodEndDate, oMultilingualAttributeInfo)));
                    SendMailToUser(oUserCollection[i], mailSubjectCert, mailBodyCert, oMultilingualAttributeInfo);
                }
            }
        }
    }

    private static void SendMailToUser(UserHdrInfo oUserHdrInfo, string mailSubject, string mailBody, MultilingualAttributeInfo oMultilingualAttributeInfo)
    {
        try
        {
            StringBuilder oMailBody = new StringBuilder();
            oMailBody.Append(string.Format("{0} ", LanguageUtil.GetValue(1845, oMultilingualAttributeInfo)));
            oMailBody.Append(oUserHdrInfo.Name);
            oMailBody.Append(",");
            oMailBody.Append("<br>");
            oMailBody.Append("<br>");
            oMailBody.Append(mailBody);
            oMailBody.Append("<br>");
            string fromAddress = AppSettingHelper.GetAppSettingValue(AppSettingConstants.EMAIL_FROM_DEFAULT);
            oMailBody.Append("<br/>" + MailHelper.GetEmailSignature(WebEnums.SignatureEnum.SendBySystemAdmin, fromAddress));
            string toAddress = oUserHdrInfo.EmailID;
            MailHelper.SendEmail(fromAddress, toAddress, mailSubject, oMailBody.ToString());
        }
        catch (Exception ex)
        {
            Helper.FormatAndShowErrorMessage(null, ex);
        }
    }
}
