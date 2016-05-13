using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using SkyStem.ART.Web.Data;
using System.Collections.Generic;
using SkyStem.ART.Client.Model;
using SkyStem.ART.Client.IServices;
using SkyStem.ART.Client.Data;
using System.Text;
using SkyStem.Language.LanguageUtility;
using SkyStem.Language.LanguageUtility.Classes;
using SharedUtility = SkyStem.ART.Shared.Utility;

namespace SkyStem.ART.Web.Utility
{
    /// <summary>
    /// Summary description for AlertHelper
    /// </summary>
    public class AlertHelper
    {
        public AlertHelper()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public static void RaiseAlert(WebEnums.Alert eAlert, int? recPeriodID, DateTime? PeriodEndDate, List<long> oAccountIDCollection, List<int> oNetAccountIDCollection, short roleID, List<AccountHdrInfo> oAccountHdrInfoCollection)
        {
            int? companyAlertID;
            int? companyBackupAlertID;
            string replacement;
            int? alertLabelID;
            List<CompanyAlertDetailInfo> oCompanyAlertDetailInfoCollection = null;
            List<UserHdrInfo> oUserHdrInfoCollection = null;
            List<UserHdrInfo> oUserHdrInfoCollectionBckup = null;

            bool isAlertSubscribed = Helper.IsAlertConfiguredByTheCompany(eAlert, out companyAlertID);
            bool isBackupAlertSubscribed = Helper.IsAlertConfiguredByTheCompany(WebEnums.Alert.AlertToNotifyBackUpOwners, out companyBackupAlertID);

            if (isAlertSubscribed && recPeriodID.HasValue)
            {
                IAlert oAlertClient = RemotingHelper.GetAlertObject();
                alertLabelID = oAlertClient.GetAlertDescriptionAndReplacementString((short)eAlert, recPeriodID.Value, oAccountIDCollection, out replacement, Helper.GetAppUserInfo());


                List<CompanyAlertInfo> oCompanyAlertInfoCollection = SessionHelper.SelectComapnyAlertByCompanyIDAndRecPeriodID();
                short? noOfHours = oCompanyAlertInfoCollection.Find(CA => CA.CompanyAlertID == companyAlertID).NoOfHours;
                // Get Account Information

                switch (eAlert)
                {
                    case WebEnums.Alert.AccountReconciliationHasBeenRejected_Denied:
                    case WebEnums.Alert.YouHaveXAccountsPendingModification:
                        oUserHdrInfoCollection = GetPrimaryUsersForAccountReconciliationHasBeenRejected_DeniedAlert(eAlert, recPeriodID.Value, oAccountIDCollection, oNetAccountIDCollection, roleID);
                        //  replacement = oAccountIDCollection.Count.ToString();
                        if (isBackupAlertSubscribed)
                        {
                            oUserHdrInfoCollectionBckup = GetBackupUsersForAccountReconciliationHasBeenRejected_DeniedAlert(eAlert, recPeriodID.Value, oAccountIDCollection, oNetAccountIDCollection, roleID);
                            oUserHdrInfoCollection.AddRange(oUserHdrInfoCollectionBckup);
                        }
                        oAccountHdrInfoCollection = GetAccountInformationForAlert(oAccountHdrInfoCollection, eAlert, recPeriodID, oAccountIDCollection, oNetAccountIDCollection, roleID);
                        break;
                    case WebEnums.Alert.OpenPeriodForReconciliation:
                        oUserHdrInfoCollection = GetUsersForOpenPeriodForReconciliationAlert();
                        if (!string.IsNullOrEmpty(replacement))
                            replacement = Convert.ToDateTime(replacement).ToString("MMMM dd, yyyy");
                        break;
                    case WebEnums.Alert.PeriodHasClosed:
                        oUserHdrInfoCollection = GetUsersForOpenPeriodForReconciliationAlert();
                        if (!string.IsNullOrEmpty(replacement))
                            replacement = Convert.ToDateTime(replacement).ToString("MMMM dd, yyyy");
                        break;
                    case WebEnums.Alert.YouHaveXAccountsPendingReview:
                    case WebEnums.Alert.YouHaveXAccountsPendingApproval:
                        oAccountHdrInfoCollection = GetAccountInformationForAlert(oAccountHdrInfoCollection, eAlert, recPeriodID, oAccountIDCollection, oNetAccountIDCollection, roleID);
                        oUserHdrInfoCollection = GetUsersForYouHaveXAccountsPendingReviewAlert(eAlert, recPeriodID.Value, oAccountIDCollection, oNetAccountIDCollection, roleID);
                        break;
                    case WebEnums.Alert.YouHaveXAccountsWhichHaveAttributesThatHaveChanged:
                        oAccountHdrInfoCollection = GetAccountInformationForAlert(oAccountHdrInfoCollection, eAlert, recPeriodID, oAccountIDCollection, oNetAccountIDCollection, roleID);
                        oUserHdrInfoCollection = GetUsersForYouHaveXAccountsWhichHaveAttributesThatHaveChangedAlert(eAlert, recPeriodID.Value, oAccountIDCollection, oNetAccountIDCollection, roleID);
                        replacement = oAccountIDCollection.Count.ToString();
                        break;
                    case WebEnums.Alert.UploadHasFailed:
                        oUserHdrInfoCollection = new List<UserHdrInfo>();
                        UserHdrInfo oUserHdrInfo = SessionHelper.GetCurrentUser();
                        oUserHdrInfo.RoleID = roleID;
                        oUserHdrInfoCollection.Add(oUserHdrInfo);
                        break;
                }
                oCompanyAlertDetailInfoCollection = GetCompanyAlertDetailInfoCollection(companyAlertID, string.Empty, alertLabelID, replacement, noOfHours, recPeriodID.Value, oUserHdrInfoCollection, roleID, 0, 0);
                oAlertClient.InsertCompanyAlertDetail(oCompanyAlertDetailInfoCollection, Helper.GetAppUserInfo());
                // in case there is an error while sending mail, ignore the error
                try
                {
                    SendMailToAllUsers(string.Empty, alertLabelID, PeriodEndDate, replacement, oUserHdrInfoCollection, oAccountHdrInfoCollection);
                }
                catch (Exception ex)
                {
                    Helper.LogException(ex);
                }
            }
        }

        private static void SendMailToAllUsers(string alertDescription, int? alertLabelID, string replacement, List<UserHdrInfo> oUserHdrInfoCollection, int actionLabelID, int roleLabelID)
        {
            string fromEmailAddress = AppSettingHelper.GetAppSettingValue(AppSettingConstants.EMAIL_FROM_DEFAULT);
            foreach (UserHdrInfo oUserHdrInfo in oUserHdrInfoCollection)
            {
                MultilingualAttributeInfo oMultilingualAttributeInfo;

                if (oUserHdrInfo.DefaultLanguageID.HasValue)
                    oMultilingualAttributeInfo = LanguageHelper.GetMultilingualAttributeInfo(SessionHelper.CurrentCompanyID, oUserHdrInfo.DefaultLanguageID);
                else
                    oMultilingualAttributeInfo = LanguageHelper.GetMultilingualAttributeInfo(SessionHelper.CurrentCompanyID, 1033);

                if (alertLabelID.HasValue)
                {
                    if (actionLabelID > 0 && roleLabelID > 0)
                    {
                        alertDescription = GetAlertDescription(alertLabelID, actionLabelID, roleLabelID, oMultilingualAttributeInfo);
                    }
                    else if (!string.IsNullOrEmpty(oUserHdrInfo.AlertReplacement))
                    {
                        alertDescription = GetAlertDescription(alertLabelID, oUserHdrInfo.AlertReplacement, oMultilingualAttributeInfo);
                    }
                    else
                    {
                        alertDescription = GetAlertDescription(alertLabelID, replacement, oMultilingualAttributeInfo);
                    }
                }

                StringBuilder mailBody = new StringBuilder();
                mailBody.Append(Helper.GetLabelIDValue(1845, oMultilingualAttributeInfo));
                mailBody.Append(" ");
                mailBody.Append(oUserHdrInfo.FirstName);
                mailBody.Append("<br>");
                mailBody.Append(alertDescription);
                mailBody.Append("<br>");
                mailBody.Append(MailHelper.GetEmailSignature(WebEnums.SignatureEnum.SendBySystemAdmin, fromEmailAddress, oMultilingualAttributeInfo));

                MailHelper.SendEmail(fromEmailAddress, oUserHdrInfo.EmailID, alertDescription, mailBody.ToString());
            }
        }

        private static List<UserHdrInfo> GetUsersForYouHaveXAccountsWhichHaveAttributesThatHaveChangedAlert(WebEnums.Alert eAlert, int recPeriodID, List<long> oAccountIDCollection, List<int> oNetAccountIDCollection, short roleID)
        {
            List<UserHdrInfo> oFinalUserHdrInfoCollection = new List<UserHdrInfo>();
            IUser oUserClient = RemotingHelper.GetUserObject();

            List<UserHdrInfo> oPreparerUserHdrInfoCollection = oUserClient.SelectUsersByAccountIDsAndRecPeriodIDAndAccountAttributeID(oAccountIDCollection, oNetAccountIDCollection, recPeriodID, (short)ARTEnums.AccountAttribute.Preparer, (short)eAlert, Helper.GetAppUserInfo());
            Array.ForEach(oPreparerUserHdrInfoCollection.ToArray(), UH => UH.RoleID = (short)WebEnums.UserRole.PREPARER);
            oFinalUserHdrInfoCollection.AddRange(oPreparerUserHdrInfoCollection);

            List<UserHdrInfo> oReviewerUserHdrInfoCollection = oUserClient.SelectUsersByAccountIDsAndRecPeriodIDAndAccountAttributeID(oAccountIDCollection, oNetAccountIDCollection, recPeriodID, (short)ARTEnums.AccountAttribute.Reviewer, (short)eAlert, Helper.GetAppUserInfo());
            Array.ForEach(oReviewerUserHdrInfoCollection.ToArray(), UH => UH.RoleID = (short)WebEnums.UserRole.REVIEWER);
            oFinalUserHdrInfoCollection.AddRange(oReviewerUserHdrInfoCollection);

            List<UserHdrInfo> oApproverUserHdrInfoCollection = oUserClient.SelectUsersByAccountIDsAndRecPeriodIDAndAccountAttributeID(oAccountIDCollection, oNetAccountIDCollection, recPeriodID, (short)ARTEnums.AccountAttribute.Approver, (short)eAlert, Helper.GetAppUserInfo());
            Array.ForEach(oApproverUserHdrInfoCollection.ToArray(), UH => UH.RoleID = (short)WebEnums.UserRole.APPROVER);
            oFinalUserHdrInfoCollection.AddRange(oApproverUserHdrInfoCollection);

            #region Backup Roles
            List<UserHdrInfo> oBackupPreparerUserHdrInfoCollection = oUserClient.SelectUsersByAccountIDsAndRecPeriodIDAndAccountAttributeID(oAccountIDCollection, oNetAccountIDCollection, recPeriodID, (short)ARTEnums.AccountAttribute.BackupPreparer, (short)eAlert, Helper.GetAppUserInfo());
            Array.ForEach(oBackupPreparerUserHdrInfoCollection.ToArray(), UH => UH.RoleID = (short)WebEnums.UserRole.BACKUP_PREPARER);
            oFinalUserHdrInfoCollection.AddRange(oBackupPreparerUserHdrInfoCollection);

            List<UserHdrInfo> oBackupReviewerUserHdrInfoCollection = oUserClient.SelectUsersByAccountIDsAndRecPeriodIDAndAccountAttributeID(oAccountIDCollection, oNetAccountIDCollection, recPeriodID, (short)ARTEnums.AccountAttribute.BackupReviewer, (short)eAlert, Helper.GetAppUserInfo());
            Array.ForEach(oBackupReviewerUserHdrInfoCollection.ToArray(), UH => UH.RoleID = (short)WebEnums.UserRole.BACKUP_REVIEWER);
            oFinalUserHdrInfoCollection.AddRange(oBackupReviewerUserHdrInfoCollection);

            List<UserHdrInfo> oBackupApproverUserHdrInfoCollection = oUserClient.SelectUsersByAccountIDsAndRecPeriodIDAndAccountAttributeID(oAccountIDCollection, oNetAccountIDCollection, recPeriodID, (short)ARTEnums.AccountAttribute.BackupApprover, (short)eAlert, Helper.GetAppUserInfo());
            Array.ForEach(oBackupApproverUserHdrInfoCollection.ToArray(), UH => UH.RoleID = (short)WebEnums.UserRole.BACKUP_APPROVER);
            oFinalUserHdrInfoCollection.AddRange(oBackupApproverUserHdrInfoCollection);

            #endregion

            return oFinalUserHdrInfoCollection;
        }

        private static List<UserHdrInfo> GetUsersForYouHaveXAccountsPendingReviewAlert(WebEnums.Alert eAlert, int recPeriodID, List<long> oAccountIDCollection, List<int> oNetAccountIDCollection, short roleID)
        {
            short accountAttributeID = 0;
            IUser oUserClient = RemotingHelper.GetUserObject();

            if (roleID == (short)WebEnums.UserRole.REVIEWER)
            {
                accountAttributeID = (short)ARTEnums.AccountAttribute.Approver;
            }
            else if (roleID == (short)WebEnums.UserRole.PREPARER)
            {
                accountAttributeID = (short)ARTEnums.AccountAttribute.Reviewer;
            }
            else if (roleID == (short)WebEnums.UserRole.BACKUP_REVIEWER)
            {
                accountAttributeID = (short)ARTEnums.AccountAttribute.Approver;
            }
            else if (roleID == (short)WebEnums.UserRole.BACKUP_PREPARER)
            {
                accountAttributeID = (short)ARTEnums.AccountAttribute.Reviewer;
            }

            List<UserHdrInfo> oUserHdrInfoCollection = oUserClient.SelectUsersByAccountIDsAndRecPeriodIDAndAccountAttributeID(oAccountIDCollection, oNetAccountIDCollection, recPeriodID, accountAttributeID, (short)eAlert, Helper.GetAppUserInfo());
            return oUserHdrInfoCollection;
        }

        private static List<UserHdrInfo> GetUsersForOpenPeriodForReconciliationAlert()
        {
            IUser oUserClient = RemotingHelper.GetUserObject();
            List<UserHdrInfo> oUserHdrInfoCollection = oUserClient.SelectAllUsersRolesAndEmailID(SessionHelper.CurrentCompanyID.Value, Helper.GetAppUserInfo());

            return oUserHdrInfoCollection;
        }

        private static List<UserHdrInfo> GetPrimaryUsersForAccountReconciliationHasBeenRejected_DeniedAlert(WebEnums.Alert eAlert, int recPeriodID, List<long> oAccountIDCollection, List<int> oNetAccountIDCollection, short roleID)
        {
            short accountAttributeID = 0;
            IUser oUserClient = RemotingHelper.GetUserObject();

            if (roleID == (short)WebEnums.UserRole.REVIEWER)
            {
                accountAttributeID = (short)ARTEnums.AccountAttribute.Preparer;
            }
            else if (roleID == (short)WebEnums.UserRole.APPROVER)
            {
                accountAttributeID = (short)ARTEnums.AccountAttribute.Reviewer;
            }
            if (roleID == (short)WebEnums.UserRole.BACKUP_REVIEWER)
            {
                accountAttributeID = (short)ARTEnums.AccountAttribute.Preparer;
            }
            else if (roleID == (short)WebEnums.UserRole.BACKUP_APPROVER)
            {
                accountAttributeID = (short)ARTEnums.AccountAttribute.Reviewer;
            }
            List<UserHdrInfo> oUserHdrInfoCollection = oUserClient.SelectUsersByAccountIDsAndRecPeriodIDAndAccountAttributeID(oAccountIDCollection, oNetAccountIDCollection, recPeriodID, accountAttributeID, (short)eAlert, Helper.GetAppUserInfo());
            return oUserHdrInfoCollection;
        }
        private static List<UserHdrInfo> GetBackupUsersForAccountReconciliationHasBeenRejected_DeniedAlert(WebEnums.Alert eAlert, int recPeriodID, List<long> oAccountIDCollection, List<int> oNetAccountIDCollection, short roleID)
        {
            short accountAttributeID = 0;
            IUser oUserClient = RemotingHelper.GetUserObject();
            if (roleID == (short)WebEnums.UserRole.REVIEWER)
            {
                accountAttributeID = (short)ARTEnums.AccountAttribute.BackupPreparer;
            }
            else if (roleID == (short)WebEnums.UserRole.APPROVER)
            {
                accountAttributeID = (short)ARTEnums.AccountAttribute.BackupReviewer;
            }
            if (roleID == (short)WebEnums.UserRole.BACKUP_REVIEWER)
            {
                accountAttributeID = (short)ARTEnums.AccountAttribute.BackupPreparer;
            }
            else if (roleID == (short)WebEnums.UserRole.BACKUP_APPROVER)
            {
                accountAttributeID = (short)ARTEnums.AccountAttribute.BackupReviewer;
            }


            List<UserHdrInfo> oUserHdrInfoCollection = oUserClient.SelectUsersByAccountIDsAndRecPeriodIDAndAccountAttributeID(oAccountIDCollection, oNetAccountIDCollection, recPeriodID, accountAttributeID, (short)eAlert, Helper.GetAppUserInfo());
            return oUserHdrInfoCollection;
        }

        private static List<CompanyAlertDetailInfo> GetCompanyAlertDetailInfoCollection(int? companyAlertID, string alertDescription, int? alertLabelID, string replacement, short? noOfHours, int recPeriodID, List<UserHdrInfo> oUserHdrInfoCollection, short roleID, int actionLabelID, int roleLabelID)
        {
            List<CompanyAlertDetailInfo> oCompanyAlertDetailInfoCollection = new List<CompanyAlertDetailInfo>();
            CompanyAlertDetailInfo oCompanyAlertDetailInfo = null;
            string userLoginID = SessionHelper.CurrentUserLoginID;
            DateTime updateTime = DateTime.Now;

            foreach (UserHdrInfo oUserHdrInfo in oUserHdrInfoCollection)
            {
                MultilingualAttributeInfo oMultilingualAttributeInfo;
                if (oUserHdrInfo.DefaultLanguageID.HasValue)
                    oMultilingualAttributeInfo = LanguageHelper.GetMultilingualAttributeInfo(SessionHelper.CurrentCompanyID, oUserHdrInfo.DefaultLanguageID);
                else
                    oMultilingualAttributeInfo = LanguageHelper.GetMultilingualAttributeInfo(SessionHelper.CurrentCompanyID, 1033);

                if (alertLabelID.HasValue)
                {
                    if (actionLabelID > 0 && roleLabelID > 0)
                    {
                        alertDescription = GetAlertDescription(alertLabelID, actionLabelID, roleLabelID, oMultilingualAttributeInfo);
                    }
                    else if (!string.IsNullOrEmpty(oUserHdrInfo.AlertReplacement))
                    {
                        alertDescription = GetAlertDescription(alertLabelID, oUserHdrInfo.AlertReplacement, oMultilingualAttributeInfo);
                    }
                    else
                    {
                        alertDescription = GetAlertDescription(alertLabelID, replacement, oMultilingualAttributeInfo);
                    }
                }


                oCompanyAlertDetailInfo = new CompanyAlertDetailInfo();
                oCompanyAlertDetailInfo.AddedBy = userLoginID;
                oCompanyAlertDetailInfo.AlertDescription = alertDescription;
                if (noOfHours.HasValue && noOfHours.Value > 0)
                {
                    oCompanyAlertDetailInfo.AlertExpectedDateTime = updateTime.AddHours(noOfHours.Value);
                }
                oCompanyAlertDetailInfo.CompanyAlertID = companyAlertID;
                oCompanyAlertDetailInfo.DateAdded = updateTime;
                oCompanyAlertDetailInfo.IsActive = true;
                oCompanyAlertDetailInfo.ReconciliationPeriodID = recPeriodID;

                if (oUserHdrInfo.RoleID.HasValue && oUserHdrInfo.RoleID.Value > 0)
                {
                    oCompanyAlertDetailInfo.RoleID = oUserHdrInfo.RoleID;
                }
                else
                {
                    oCompanyAlertDetailInfo.RoleID = GetToBeSavedRoleID(roleID);
                }
                oCompanyAlertDetailInfo.UserID = oUserHdrInfo.UserID;

                oCompanyAlertDetailInfoCollection.Add(oCompanyAlertDetailInfo);
            }
            return oCompanyAlertDetailInfoCollection;
        }

        //public static string GetAlertDescription(int? alertLabelID, string replacement)
        //{
        //    string alertDescription = string.Format(Helper.GetLabelIDValue(alertLabelID.Value), replacement);

        //    return alertDescription;
        //}
        public static string GetAlertDescription(int? alertLabelID, string replacement, MultilingualAttributeInfo oMultilingualAttributeInfo)
        {
            string alertDescription = string.Format(LanguageUtil.GetValue(alertLabelID.Value, oMultilingualAttributeInfo), replacement);

            return alertDescription;
        }

        private static short? GetToBeSavedRoleID(short roleID)
        {
            short? toBeSavedRoelID = 0;

            if (roleID == (short)WebEnums.UserRole.REVIEWER || roleID == (short)WebEnums.UserRole.BACKUP_REVIEWER)
            {
                toBeSavedRoelID = (short)WebEnums.UserRole.PREPARER;
            }
            else if (roleID == (short)WebEnums.UserRole.APPROVER || roleID == (short)WebEnums.UserRole.BACKUP_APPROVER)
            {
                toBeSavedRoelID = (short)WebEnums.UserRole.REVIEWER;
            }
            return toBeSavedRoelID;
        }

        public static void RaiseAlertForAlertUserAboutRoleAndAccountChangesAlert(List<long> oAccountIDCollection, List<int> removedPreparerIDCollection,
            List<int> removedReviewerIDCollection, List<int> removedApproverIDCollection, List<int> assignedPreparerIDCollection,
            List<int> assignedReviewerIDCollection, List<int> assignedApproverIDCollection, List<int> removedBackupPreparerIDCollection,
            List<int> assignedBackupPreparerIDCollection, List<int> removedBackupReviewerIDCollection,
            List<int> assignedBackupReviewerIDCollection, List<int> removedBackupApproverIDCollection,
            List<int> assignedBackupApproverIDCollection, Dictionary<int, List<AccountHdrInfo>> removedPreparerAccounts,
            Dictionary<int, List<AccountHdrInfo>> removedReviewerAccounts, Dictionary<int, List<AccountHdrInfo>> removedApproverAccounts, Dictionary<int, List<AccountHdrInfo>> assignedPreparerAccounts,
            Dictionary<int, List<AccountHdrInfo>> assignedReviewerAccounts, Dictionary<int, List<AccountHdrInfo>> assignedApproverAccounts, Dictionary<int, List<AccountHdrInfo>> removedBackupPreparerAccounts,
            Dictionary<int, List<AccountHdrInfo>> assignedBackupPreparerAccounts, Dictionary<int, List<AccountHdrInfo>> removedBackupReviewerAccounts,
            Dictionary<int, List<AccountHdrInfo>> assignedBackupReviewerAccounts, Dictionary<int, List<AccountHdrInfo>> removedBackupApproverAccounts,
            Dictionary<int, List<AccountHdrInfo>> assignedBackupApproverAccounts)
        {
            WebEnums.Alert eAlert = WebEnums.Alert.AlertUserAboutRoleAndAccountChanges;
            int? companyAlertID;
            string replacement;
            int? alertLabelID;
            int recPeriodID = SessionHelper.CurrentReconciliationPeriodID.Value;

            bool isAlertSubscribed = Helper.IsAlertConfiguredByTheCompany(eAlert, out companyAlertID);


            if (isAlertSubscribed)
            {
                IAlert oAlertClient = RemotingHelper.GetAlertObject();

                alertLabelID = oAlertClient.GetAlertDescriptionAndReplacementString((short)eAlert, recPeriodID, oAccountIDCollection, out replacement, Helper.GetAppUserInfo());


                List<CompanyAlertInfo> oCompanyAlertInfoCollection = SessionHelper.SelectComapnyAlertByCompanyIDAndRecPeriodID();
                short? noOfHours = oCompanyAlertInfoCollection.Find(CA => CA.CompanyAlertID == companyAlertID).NoOfHours;

                List<int> oAllUsersIDCollection = new List<int>();
                oAllUsersIDCollection.AddRange(removedPreparerIDCollection);
                oAllUsersIDCollection.AddRange(removedReviewerIDCollection);
                oAllUsersIDCollection.AddRange(removedApproverIDCollection);
                oAllUsersIDCollection.AddRange(removedBackupPreparerIDCollection);
                oAllUsersIDCollection.AddRange(removedBackupReviewerIDCollection);
                oAllUsersIDCollection.AddRange(removedBackupApproverIDCollection);
                oAllUsersIDCollection.AddRange(assignedPreparerIDCollection);
                oAllUsersIDCollection.AddRange(assignedReviewerIDCollection);
                oAllUsersIDCollection.AddRange(assignedApproverIDCollection);
                oAllUsersIDCollection.AddRange(assignedBackupPreparerIDCollection);
                oAllUsersIDCollection.AddRange(assignedBackupReviewerIDCollection);
                oAllUsersIDCollection.AddRange(assignedBackupApproverIDCollection);

                IUser oUserClient = RemotingHelper.GetUserObject();
                List<UserHdrInfo> oAllUserHdrInfoCollection = oUserClient.SelectUserByUserID(oAllUsersIDCollection, Helper.GetAppUserInfo());

                List<UserHdrInfo> oRemovedPreparerUserInfoCollection = GetUsersDetails(oAllUserHdrInfoCollection, removedPreparerIDCollection, (short)WebEnums.UserRole.PREPARER);
                List<UserHdrInfo> oRemovedReviewerUserInfoCollection = GetUsersDetails(oAllUserHdrInfoCollection, removedReviewerIDCollection, (short)WebEnums.UserRole.REVIEWER);
                List<UserHdrInfo> oRemovedApproverUserInfoCollection = GetUsersDetails(oAllUserHdrInfoCollection, removedApproverIDCollection, (short)WebEnums.UserRole.APPROVER);
                List<UserHdrInfo> oAssignedPreparerUserInfoCollection = GetUsersDetails(oAllUserHdrInfoCollection, assignedPreparerIDCollection, (short)WebEnums.UserRole.PREPARER);
                List<UserHdrInfo> oAssignedReviewerUserInfoCollection = GetUsersDetails(oAllUserHdrInfoCollection, assignedReviewerIDCollection, (short)WebEnums.UserRole.REVIEWER);
                List<UserHdrInfo> oAssignedApproverUserInfoCollection = GetUsersDetails(oAllUserHdrInfoCollection, assignedApproverIDCollection, (short)WebEnums.UserRole.APPROVER);

                List<UserHdrInfo> oRemovedBackupPreparerUserInfoCollection = GetUsersDetails(oAllUserHdrInfoCollection, removedBackupPreparerIDCollection, (short)WebEnums.UserRole.BACKUP_PREPARER);
                List<UserHdrInfo> oRemovedBackupReviewerUserInfoCollection = GetUsersDetails(oAllUserHdrInfoCollection, removedBackupReviewerIDCollection, (short)WebEnums.UserRole.BACKUP_REVIEWER);
                List<UserHdrInfo> oRemovedBackupApproverUserInfoCollection = GetUsersDetails(oAllUserHdrInfoCollection, removedBackupApproverIDCollection, (short)WebEnums.UserRole.BACKUP_APPROVER);
                List<UserHdrInfo> oAssignedBackupPreparerUserInfoCollection = GetUsersDetails(oAllUserHdrInfoCollection, assignedBackupPreparerIDCollection, (short)WebEnums.UserRole.BACKUP_PREPARER);
                List<UserHdrInfo> oAssignedBackupReviewerUserInfoCollection = GetUsersDetails(oAllUserHdrInfoCollection, assignedBackupReviewerIDCollection, (short)WebEnums.UserRole.BACKUP_REVIEWER);
                List<UserHdrInfo> oAssignedBackupApproverUserInfoCollection = GetUsersDetails(oAllUserHdrInfoCollection, assignedBackupApproverIDCollection, (short)WebEnums.UserRole.BACKUP_APPROVER);

                //raise alert
                if (oRemovedPreparerUserInfoCollection != null && oRemovedPreparerUserInfoCollection.Count > 0)
                {
                    RaiseAlertForUsers(alertLabelID, 1880, 1130, companyAlertID, noOfHours, recPeriodID, oRemovedPreparerUserInfoCollection, (short)WebEnums.UserRole.PREPARER, removedPreparerAccounts);
                }

                if (oRemovedReviewerUserInfoCollection != null && oRemovedReviewerUserInfoCollection.Count > 0)
                {
                    RaiseAlertForUsers(alertLabelID, 1880, 1131, companyAlertID, noOfHours, recPeriodID, oRemovedReviewerUserInfoCollection, (short)WebEnums.UserRole.REVIEWER, removedReviewerAccounts);
                }

                if (oRemovedApproverUserInfoCollection != null && oRemovedApproverUserInfoCollection.Count > 0)
                {
                    RaiseAlertForUsers(alertLabelID, 1880, 1132, companyAlertID, noOfHours, recPeriodID, oRemovedApproverUserInfoCollection, (short)WebEnums.UserRole.APPROVER, removedApproverAccounts);
                }

                if (oAssignedPreparerUserInfoCollection != null && oAssignedPreparerUserInfoCollection.Count > 0)
                {
                    RaiseAlertForUsers(alertLabelID, 1654, 1130, companyAlertID, noOfHours, recPeriodID, oAssignedPreparerUserInfoCollection, (short)WebEnums.UserRole.PREPARER, assignedPreparerAccounts);
                }

                if (oAssignedReviewerUserInfoCollection != null && oAssignedReviewerUserInfoCollection.Count > 0)
                {
                    RaiseAlertForUsers(alertLabelID, 1654, 1131, companyAlertID, noOfHours, recPeriodID, oAssignedReviewerUserInfoCollection, (short)WebEnums.UserRole.REVIEWER, assignedReviewerAccounts);
                }

                if (oAssignedApproverUserInfoCollection != null && oAssignedApproverUserInfoCollection.Count > 0)
                {
                    RaiseAlertForUsers(alertLabelID, 1654, 1132, companyAlertID, noOfHours, recPeriodID, oAssignedApproverUserInfoCollection, (short)WebEnums.UserRole.APPROVER, assignedApproverAccounts);
                }

                #region Backup Roles
                if (oRemovedBackupPreparerUserInfoCollection != null && oRemovedBackupPreparerUserInfoCollection.Count > 0)
                {
                    RaiseAlertForUsers(alertLabelID, 1880, 2501, companyAlertID, noOfHours, recPeriodID, oRemovedBackupPreparerUserInfoCollection, (short)WebEnums.UserRole.BACKUP_PREPARER, removedBackupPreparerAccounts);
                }

                if (oRemovedBackupReviewerUserInfoCollection != null && oRemovedBackupReviewerUserInfoCollection.Count > 0)
                {
                    RaiseAlertForUsers(alertLabelID, 1880, 2502, companyAlertID, noOfHours, recPeriodID, oRemovedBackupReviewerUserInfoCollection, (short)WebEnums.UserRole.BACKUP_REVIEWER, removedBackupReviewerAccounts);
                }

                if (oRemovedBackupApproverUserInfoCollection != null && oRemovedBackupApproverUserInfoCollection.Count > 0)
                {
                    RaiseAlertForUsers(alertLabelID, 1880, 2503, companyAlertID, noOfHours, recPeriodID, oRemovedBackupApproverUserInfoCollection, (short)WebEnums.UserRole.BACKUP_APPROVER, removedBackupApproverAccounts);
                }

                if (oAssignedBackupPreparerUserInfoCollection != null && oAssignedBackupPreparerUserInfoCollection.Count > 0)
                {
                    RaiseAlertForUsers(alertLabelID, 1654, 2501, companyAlertID, noOfHours, recPeriodID, oAssignedBackupPreparerUserInfoCollection, (short)WebEnums.UserRole.BACKUP_PREPARER, assignedBackupPreparerAccounts);
                }

                if (oAssignedBackupReviewerUserInfoCollection != null && oAssignedBackupReviewerUserInfoCollection.Count > 0)
                {
                    RaiseAlertForUsers(alertLabelID, 1654, 2502, companyAlertID, noOfHours, recPeriodID, oAssignedBackupReviewerUserInfoCollection, (short)WebEnums.UserRole.BACKUP_REVIEWER, assignedBackupReviewerAccounts);
                }

                if (oAssignedBackupApproverUserInfoCollection != null && oAssignedBackupApproverUserInfoCollection.Count > 0)
                {
                    RaiseAlertForUsers(alertLabelID, 1654, 2503, companyAlertID, noOfHours, recPeriodID, oAssignedBackupApproverUserInfoCollection, (short)WebEnums.UserRole.BACKUP_APPROVER, assignedBackupApproverAccounts);
                }

                #endregion

            }
        }

        private static void RaiseAlertForUsers(int? alertLabelID, int actionLabelID, int roleLabelID, int? companyAlertID, short? noOfHours, int recPeriodID, List<UserHdrInfo> oUserHdrInfoCollection, short roleID)
        {
            IAlert oAlertClient = RemotingHelper.GetAlertObject();

            string alertDescription = string.Empty;
            List<CompanyAlertDetailInfo> oCompanyAlertDetailInfoCollection = GetCompanyAlertDetailInfoCollection(companyAlertID, alertDescription, alertLabelID, null, noOfHours, recPeriodID, oUserHdrInfoCollection, roleID, actionLabelID, roleLabelID);
            oAlertClient.InsertCompanyAlertDetail(oCompanyAlertDetailInfoCollection, Helper.GetAppUserInfo());

            try
            {
                SendMailToAllUsers(alertDescription, alertLabelID, null, oUserHdrInfoCollection, actionLabelID, roleLabelID);
            }
            catch (Exception ex)
            {
                Helper.LogException(ex);
            }
        }

        private static void RaiseAlertForUsers(int? alertLabelID, int actionLabelID, int roleLabelID, int? companyAlertID, short? noOfHours, int recPeriodID, List<UserHdrInfo> oUserHdrInfoCollection, short roleID, Dictionary<int, List<AccountHdrInfo>> oUserAccountHdrInfoDict)
        {
            IAlert oAlertClient = RemotingHelper.GetAlertObject();

            string alertDescription = string.Empty;
            List<CompanyAlertDetailInfo> oCompanyAlertDetailInfoCollection = GetCompanyAlertDetailInfoCollection(companyAlertID, alertDescription, null, null, noOfHours, recPeriodID, oUserHdrInfoCollection, roleID, actionLabelID, roleLabelID);
            oAlertClient.InsertCompanyAlertDetail(oCompanyAlertDetailInfoCollection, Helper.GetAppUserInfo());

            try
            {
                SendMailToAllUsers(alertDescription, alertLabelID, null, oUserHdrInfoCollection, oUserAccountHdrInfoDict, actionLabelID, roleLabelID);
            }
            catch (Exception ex)
            {
                Helper.LogException(ex);
            }
        }

        //private static string GetAlertDescription(int? alertLabelID, int actionLabelID, int roleLabelID)
        //{
        //    string alertDescription = string.Format(Helper.GetLabelIDValue(alertLabelID.Value), Helper.GetLabelIDValue(actionLabelID), Helper.GetLabelIDValue(roleLabelID));

        //    return alertDescription;
        //}
        private static string GetAlertDescription(int? alertLabelID, int actionLabelID, int roleLabelID, MultilingualAttributeInfo oMultilingualAttributeInfo)
        {
            string alertDescription = string.Format(Helper.GetLabelIDValue(alertLabelID.Value, oMultilingualAttributeInfo), Helper.GetLabelIDValue(actionLabelID, oMultilingualAttributeInfo), Helper.GetLabelIDValue(roleLabelID, oMultilingualAttributeInfo));

            return alertDescription;
        }

        private static List<UserHdrInfo> GetUsersDetails(List<UserHdrInfo> oAllUserHdrInfoCollection, List<int> UserIDCollection, short roleID)
        {
            List<UserHdrInfo> oUserHdrInfoCollection = new List<UserHdrInfo>();
            UserHdrInfo oUserHdrInfo = null;

            foreach (int userID in UserIDCollection)
            {
                oUserHdrInfo = oAllUserHdrInfoCollection.Find(UH => UH.UserID == userID);
                oUserHdrInfo.RoleID = roleID;
                oUserHdrInfoCollection.Add(oUserHdrInfo);
            }

            return oUserHdrInfoCollection;
        }

        public static void RaiseAlertForAssignedTask(List<int> assignedAssignedToIDCollection, List<int> assignedReviewerIDCollection, List<int> assignedApproverIDCollection, List<int> assignedNotifyIDCollection, List<TaskHdrInfo> oTaskHdrInfoList)
        {
            WebEnums.Alert eAlert = WebEnums.Alert.YouHaveXTaskAssigned;
            int? companyAlertID;
            string replacement;
            int? alertLabelID;
            int recPeriodID = SessionHelper.CurrentReconciliationPeriodID.Value;

            bool isAlertSubscribed = Helper.IsAlertConfiguredByTheCompany(eAlert, out companyAlertID);


            if (isAlertSubscribed)
            {
                IAlert oAlertClient = RemotingHelper.GetAlertObject();

                alertLabelID = oAlertClient.GetAlertDescriptionAndReplacementString((short)eAlert, recPeriodID, null, out replacement, Helper.GetAppUserInfo());


                List<CompanyAlertInfo> oCompanyAlertInfoCollection = SessionHelper.SelectComapnyAlertByCompanyIDAndRecPeriodID();
                short? noOfHours = oCompanyAlertInfoCollection.Find(CA => CA.CompanyAlertID == companyAlertID).NoOfHours;               
                List<UserHdrInfo> oAllUserHdrInfoCollection = CacheHelper.SelectAllUsersForCurrentCompany();
                List<UserHdrInfo> oassignedAssignedToIDCollection = GetUsersDetails(oAllUserHdrInfoCollection, assignedAssignedToIDCollection);
                List<UserHdrInfo> oassignedReviewerIDCollection = GetUsersDetails(oAllUserHdrInfoCollection, assignedReviewerIDCollection);
                List<UserHdrInfo> oassignedApproverIDCollection = GetUsersDetails(oAllUserHdrInfoCollection, assignedApproverIDCollection);
                List<UserHdrInfo> oassignedNotifyIDCollection = GetUsersDetails(oAllUserHdrInfoCollection, assignedNotifyIDCollection);
                foreach (var item in oTaskHdrInfoList)
                {
                    item.AssignedTo = GetUsersDetails(oAllUserHdrInfoCollection, item.AssignedTo);
                    item.Reviewer = GetUsersDetails(oAllUserHdrInfoCollection, item.Reviewer);
                    item.Approver = GetUsersDetails(oAllUserHdrInfoCollection, item.Approver);
                    item.Notify = GetUsersDetails(oAllUserHdrInfoCollection, item.Notify);
                }

                //raise alert
                if (oassignedAssignedToIDCollection != null && oassignedAssignedToIDCollection.Count > 0)
                {
                    RaiseTaskAlertForUsers(alertLabelID, 1654, 1130, companyAlertID, noOfHours, recPeriodID, oassignedAssignedToIDCollection, (short)WebEnums.UserRole.PREPARER, oTaskHdrInfoList);
                }

                if (oassignedReviewerIDCollection != null && oassignedReviewerIDCollection.Count > 0)
                {
                    RaiseTaskAlertForUsers(alertLabelID, 1654, 1131, companyAlertID, noOfHours, recPeriodID, oassignedReviewerIDCollection, (short)WebEnums.UserRole.REVIEWER, oTaskHdrInfoList);
                }

                if (oassignedApproverIDCollection != null && oassignedApproverIDCollection.Count > 0)
                {
                    RaiseTaskAlertForUsers(alertLabelID, 1654, 1132, companyAlertID, noOfHours, recPeriodID, oassignedApproverIDCollection, (short)WebEnums.UserRole.APPROVER, oTaskHdrInfoList);
                }
                //alert to notify user
                if (oassignedNotifyIDCollection != null && oassignedNotifyIDCollection.Count > 0)
                {
                    RaiseTaskAlertForUsers(alertLabelID, 1654, 1312, companyAlertID, noOfHours, recPeriodID, oassignedNotifyIDCollection, null, oTaskHdrInfoList);
                }

            }
        }

        private static List<UserHdrInfo> GetUsersDetails(List<UserHdrInfo> oAllUserHdrInfoCollection, List<int> UserIDCollection)
        {
            List<UserHdrInfo> oUserHdrInfoCollection = new List<UserHdrInfo>();
            UserHdrInfo oUserHdrInfo = null;

            foreach (int userID in UserIDCollection)
            {
                oUserHdrInfo = oAllUserHdrInfoCollection.Find(UH => UH.UserID == userID);
                oUserHdrInfoCollection.Add(oUserHdrInfo);
            }

            return oUserHdrInfoCollection;
        }
        private static List<UserHdrInfo> GetUsersDetails(List<UserHdrInfo> oAllUserHdrInfoCollection, List<UserHdrInfo> UserIDCollection)
        {
            List<UserHdrInfo> oUserHdrInfoCollection = new List<UserHdrInfo>();
            UserHdrInfo oUserHdrInfo = null;
            if (UserIDCollection != null && UserIDCollection.Count > 0)
            {
                foreach (UserHdrInfo obj in UserIDCollection)
                {
                    oUserHdrInfo = oAllUserHdrInfoCollection.Find(UH => UH.UserID.GetValueOrDefault() == obj.UserID.GetValueOrDefault());
                    oUserHdrInfoCollection.Add(oUserHdrInfo);
                }
            }
            return oUserHdrInfoCollection;
        }

        private static void RaiseTaskAlertForUsers(int? alertLabelID, int actionLabelID, int roleLabelID, int? companyAlertID, short? noOfHours, int recPeriodID, List<UserHdrInfo> oUserHdrInfoCollection, short? roleID, List<TaskHdrInfo> oTaskHdrInfoList)
        {
            IAlert oAlertClient = RemotingHelper.GetAlertObject();

            string alertDescription = string.Empty;
            List<CompanyAlertDetailInfo> oCompanyAlertDetailInfoCollection = GetTaskAlertDetailInfoCollection(companyAlertID, null, alertLabelID, null, noOfHours, recPeriodID, oUserHdrInfoCollection, roleID, actionLabelID, roleLabelID);
            oAlertClient.InsertCompanyAlertDetail(oCompanyAlertDetailInfoCollection, Helper.GetAppUserInfo());

            try
            {
                SendMailToAllUsers(null, alertLabelID, null, oUserHdrInfoCollection, oTaskHdrInfoList,actionLabelID, roleLabelID);
            }
            catch (Exception ex)
            {
                Helper.LogException(ex);
            }
        }
        private static List<CompanyAlertDetailInfo> GetTaskAlertDetailInfoCollection(int? companyAlertID, string alertDescription, int? alertLabelID, string replacement, short? noOfHours, int recPeriodID, List<UserHdrInfo> oUserHdrInfoCollection, short? roleID, int actionLabelID, int roleLabelID)
        {
            List<CompanyAlertDetailInfo> oCompanyAlertDetailInfoCollection = new List<CompanyAlertDetailInfo>();
            CompanyAlertDetailInfo oCompanyAlertDetailInfo = null;
            string userLoginID = SessionHelper.CurrentUserLoginID;
            DateTime updateTime = DateTime.Now;

            foreach (UserHdrInfo oUserHdrInfo in oUserHdrInfoCollection)
            {
                MultilingualAttributeInfo oMultilingualAttributeInfo;

                if (oUserHdrInfo.DefaultLanguageID.HasValue)
                    oMultilingualAttributeInfo = LanguageHelper.GetMultilingualAttributeInfo(SessionHelper.CurrentCompanyID, oUserHdrInfo.DefaultLanguageID);
                else
                    oMultilingualAttributeInfo = LanguageHelper.GetMultilingualAttributeInfo(SessionHelper.CurrentCompanyID, 1033);
                if (alertLabelID.HasValue)
                {
                    if (actionLabelID > 0 && roleLabelID > 0)
                    {
                        alertDescription = GetAlertDescription(alertLabelID, actionLabelID, roleLabelID, oMultilingualAttributeInfo);
                    }
                    else if (!string.IsNullOrEmpty(oUserHdrInfo.AlertReplacement))
                    {
                        alertDescription = GetAlertDescription(alertLabelID, oUserHdrInfo.AlertReplacement, oMultilingualAttributeInfo);
                    }
                    else
                    {
                        alertDescription = GetAlertDescription(alertLabelID, replacement, oMultilingualAttributeInfo);
                    }
                }


                oCompanyAlertDetailInfo = new CompanyAlertDetailInfo();
                oCompanyAlertDetailInfo.AddedBy = userLoginID;
                oCompanyAlertDetailInfo.AlertDescription = alertDescription;
                if (noOfHours.HasValue && noOfHours.Value > 0)
                {
                    oCompanyAlertDetailInfo.AlertExpectedDateTime = updateTime.AddHours(noOfHours.Value);
                }
                oCompanyAlertDetailInfo.CompanyAlertID = companyAlertID;
                oCompanyAlertDetailInfo.DateAdded = updateTime;
                oCompanyAlertDetailInfo.IsActive = true;
                oCompanyAlertDetailInfo.ReconciliationPeriodID = recPeriodID;

                oCompanyAlertDetailInfo.RoleID = roleID;

                oCompanyAlertDetailInfo.UserID = oUserHdrInfo.UserID;

                oCompanyAlertDetailInfoCollection.Add(oCompanyAlertDetailInfo);
            }
            return oCompanyAlertDetailInfoCollection;
        }

        public static void RaiseAlertForUnAssignedTask(List<int> assignedAssignedToIDCollection, List<int> assignedReviewerIDCollection, List<int> assignedApproverIDCollection, List<int> assignedNotifyIDCollection, List<TaskHdrInfo> oListTaskHdrInfo)
        {
            WebEnums.Alert eAlert = WebEnums.Alert.YouHaveXTaskAssigned;
            int? companyAlertID;
            string replacement;
            int? alertLabelID;
            int recPeriodID = SessionHelper.CurrentReconciliationPeriodID.Value;

            bool isAlertSubscribed = Helper.IsAlertConfiguredByTheCompany(eAlert, out companyAlertID);


            if (isAlertSubscribed)
            {
                IAlert oAlertClient = RemotingHelper.GetAlertObject();

                alertLabelID = oAlertClient.GetAlertDescriptionAndReplacementString((short)eAlert, recPeriodID, null, out replacement, Helper.GetAppUserInfo());


                List<CompanyAlertInfo> oCompanyAlertInfoCollection = SessionHelper.SelectComapnyAlertByCompanyIDAndRecPeriodID();
                short? noOfHours = oCompanyAlertInfoCollection.Find(CA => CA.CompanyAlertID == companyAlertID).NoOfHours;             
                List<UserHdrInfo> oAllUserHdrInfoCollection = CacheHelper.SelectAllUsersForCurrentCompany();
                List<UserHdrInfo> oassignedAssignedToIDCollection = GetUsersDetails(oAllUserHdrInfoCollection, assignedAssignedToIDCollection);
                List<UserHdrInfo> oassignedApproverIDCollection = GetUsersDetails(oAllUserHdrInfoCollection, assignedApproverIDCollection);
                List<UserHdrInfo> oassignedReviewerIDCollection = GetUsersDetails(oAllUserHdrInfoCollection, assignedReviewerIDCollection);
                List<UserHdrInfo> oassignedNotifyIDCollection = GetUsersDetails(oAllUserHdrInfoCollection, assignedNotifyIDCollection);

                foreach (var item in oListTaskHdrInfo)
                {
                    item.AssignedTo = GetUsersDetails(oAllUserHdrInfoCollection, item.AssignedTo);
                    item.Reviewer = GetUsersDetails(oAllUserHdrInfoCollection, item.Reviewer);
                    item.Approver = GetUsersDetails(oAllUserHdrInfoCollection, item.Approver);
                    item.Notify = GetUsersDetails(oAllUserHdrInfoCollection, item.Notify);
                }
                //raise alert
                if (oassignedAssignedToIDCollection != null && oassignedAssignedToIDCollection.Count > 0)
                {
                    RaiseTaskAlertForUsers(alertLabelID, 1655, 1130, companyAlertID, noOfHours, recPeriodID, oassignedAssignedToIDCollection, null, oListTaskHdrInfo);
                }
                if (oassignedReviewerIDCollection != null && oassignedReviewerIDCollection.Count > 0)
                {
                    RaiseTaskAlertForUsers(alertLabelID, 1655, 1131, companyAlertID, noOfHours, recPeriodID, oassignedReviewerIDCollection, null, oListTaskHdrInfo);
                }
                if (oassignedApproverIDCollection != null && oassignedApproverIDCollection.Count > 0)
                {
                    RaiseTaskAlertForUsers(alertLabelID, 1655, 1132, companyAlertID, noOfHours, recPeriodID, oassignedApproverIDCollection, null, oListTaskHdrInfo);
                }
                if (oassignedNotifyIDCollection != null && oassignedNotifyIDCollection.Count > 0)
                {
                    RaiseTaskAlertForUsers(alertLabelID, 1655, 1312, companyAlertID, noOfHours, recPeriodID, oassignedNotifyIDCollection, null, oListTaskHdrInfo);
                }
            }
        }

        public static void RaiseAlertForRejecteddTask(List<int> AssignedToIDCollection, List<TaskHdrInfo> oTaskHdrInfoList)
        {
            List<int> oAllUsersIDCollection = new List<int>();
            oAllUsersIDCollection.AddRange(AssignedToIDCollection);


            IUser oUserClient = RemotingHelper.GetUserObject();
            List<UserHdrInfo> oAllUserHdrInfoCollection = oUserClient.SelectUserByUserID(oAllUsersIDCollection, Helper.GetAppUserInfo());
            List<UserHdrInfo> oUserHdrInfoCollection = GetUsersDetails(oAllUserHdrInfoCollection, AssignedToIDCollection);

            //raise alert
            if (oUserHdrInfoCollection != null && oUserHdrInfoCollection.Count > 0)
            {
                SendMailToAllUsers(null, 2647, null, oUserHdrInfoCollection, oTaskHdrInfoList, 0, 1130);
            }
        }

        public static void RaiseAlertForApproval(List<int> assignedApproverIDCollection, List<TaskHdrInfo> oTaskHdrInfoList)
        {
            WebEnums.Alert eAlert = WebEnums.Alert.YoyHaveXTaskForApproval;
            int? companyAlertID;
            string replacement;
            int? alertLabelID;
            int recPeriodID = SessionHelper.CurrentReconciliationPeriodID.Value;

            bool isAlertSubscribed = Helper.IsAlertConfiguredByTheCompany(eAlert, out companyAlertID);


            if (isAlertSubscribed)
            {
                IAlert oAlertClient = RemotingHelper.GetAlertObject();

                alertLabelID = oAlertClient.GetAlertDescriptionAndReplacementString((short)eAlert, recPeriodID, null, out replacement, Helper.GetAppUserInfo());

                List<CompanyAlertInfo> oCompanyAlertInfoCollection = SessionHelper.SelectComapnyAlertByCompanyIDAndRecPeriodID();
                short? noOfHours = oCompanyAlertInfoCollection.Find(CA => CA.CompanyAlertID == companyAlertID).NoOfHours;

                List<int> oAllUsersIDCollection = new List<int>();
                oAllUsersIDCollection.AddRange(assignedApproverIDCollection);

                IUser oUserClient = RemotingHelper.GetUserObject();
                List<UserHdrInfo> oAllUserHdrInfoCollection = oUserClient.SelectUserByUserID(oAllUsersIDCollection, Helper.GetAppUserInfo());

                List<UserHdrInfo> oassignedApproverIDCollection = GetUsersDetails(oAllUserHdrInfoCollection, assignedApproverIDCollection);

                if (oassignedApproverIDCollection != null && oassignedApproverIDCollection.Count > 0)
                {
                    RaiseTaskAlertForUsers(alertLabelID, 1094, 1132, companyAlertID, noOfHours, recPeriodID, oassignedApproverIDCollection, null, oTaskHdrInfoList);
                }
            }
        }

        public static void RaiseAlertForReviewer(List<int> assignedReviewerIDCollection, List<TaskHdrInfo> oTaskHdrInfoList)
        {
            WebEnums.Alert eAlert = WebEnums.Alert.YoyHaveXTaskForReview;
            int? companyAlertID;
            string replacement;
            int? alertLabelID;
            int recPeriodID = SessionHelper.CurrentReconciliationPeriodID.Value;

            bool isAlertSubscribed = Helper.IsAlertConfiguredByTheCompany(eAlert, out companyAlertID);

            if (isAlertSubscribed)
            {
                IAlert oAlertClient = RemotingHelper.GetAlertObject();

                alertLabelID = oAlertClient.GetAlertDescriptionAndReplacementString((short)eAlert, recPeriodID, null, out replacement, Helper.GetAppUserInfo());

                List<CompanyAlertInfo> oCompanyAlertInfoCollection = SessionHelper.SelectComapnyAlertByCompanyIDAndRecPeriodID();
                short? noOfHours = oCompanyAlertInfoCollection.Find(CA => CA.CompanyAlertID == companyAlertID).NoOfHours;

                List<int> oAllUsersIDCollection = new List<int>();
                oAllUsersIDCollection.AddRange(assignedReviewerIDCollection);

                IUser oUserClient = RemotingHelper.GetUserObject();
                List<UserHdrInfo> oAllUserHdrInfoCollection = oUserClient.SelectUserByUserID(oAllUsersIDCollection, Helper.GetAppUserInfo());

                List<UserHdrInfo> oassignedReviewerIDCollection = GetUsersDetails(oAllUserHdrInfoCollection, assignedReviewerIDCollection);

                if (oassignedReviewerIDCollection != null && oassignedReviewerIDCollection.Count > 0)
                {
                    RaiseTaskAlertForUsers(alertLabelID, 1091, 1131, companyAlertID, noOfHours, recPeriodID, oassignedReviewerIDCollection, null, oTaskHdrInfoList);
                }
            }
        }


        private static void SendMailToAllUsers(string alertDescription, int? alertLabelID, DateTime? periodEndDate, string replacement, List<UserHdrInfo> oUserHdrInfoCollection, List<AccountHdrInfo> oAccountHdrInfoCollection)
        {
            string fromEmailAddress = AppSettingHelper.GetAppSettingValue(AppSettingConstants.EMAIL_FROM_DEFAULT);

            string PeriodEndDate = string.Empty;
            string AppendVal = string.Empty;
            string MailSubject = string.Empty;

            foreach (UserHdrInfo oUserHdrInfo in oUserHdrInfoCollection)
            {
                MultilingualAttributeInfo oMultilingualAttributeInfo;

                if (oUserHdrInfo.DefaultLanguageID.HasValue)
                    oMultilingualAttributeInfo = LanguageHelper.GetMultilingualAttributeInfo(SessionHelper.CurrentCompanyID, oUserHdrInfo.DefaultLanguageID);
                else
                    oMultilingualAttributeInfo = LanguageHelper.GetMultilingualAttributeInfo(SessionHelper.CurrentCompanyID, 1033);

                if (periodEndDate.HasValue)
                {
                    // 1420- Rec Period
                    PeriodEndDate = LanguageUtil.GetValue(1420, oMultilingualAttributeInfo);
                    PeriodEndDate = PeriodEndDate + ": " + Helper.GetDisplayDate(periodEndDate, oMultilingualAttributeInfo);
                    AppendVal = LanguageUtil.GetValue(2804, oMultilingualAttributeInfo);
                    AppendVal = string.Format(AppendVal, Helper.GetDisplayDate(periodEndDate, oMultilingualAttributeInfo));
                }



                StringBuilder mailBody = new StringBuilder();
                mailBody.Append(LanguageUtil.GetValue(1845, oMultilingualAttributeInfo));
                mailBody.Append(" ");
                mailBody.Append(oUserHdrInfo.FirstName);
                if (SessionHelper.CurrentReconciliationPeriodEndDate.HasValue)
                {
                    mailBody.Append("<br>");
                    mailBody.Append(PeriodEndDate);
                }

                //To add account Details
                if (oAccountHdrInfoCollection != null && oAccountHdrInfoCollection.Count > 0)
                {
                    mailBody.Append("<br>");
                    StringBuilder AccountDetails = new StringBuilder();
                    List<AccountHdrInfo> oListAccountHdrInfoForUser = null;
                    switch (oUserHdrInfo.RoleID)
                    {
                        case (short)WebEnums.UserRole.PREPARER:
                            oListAccountHdrInfoForUser = oAccountHdrInfoCollection.Where(o => o.PreparerUserID == oUserHdrInfo.UserID).ToList<AccountHdrInfo>();
                            break;
                        case (short)WebEnums.UserRole.REVIEWER:
                            oListAccountHdrInfoForUser = oAccountHdrInfoCollection.Where(o => o.ReviewerUserID == oUserHdrInfo.UserID).ToList<AccountHdrInfo>();
                            break;
                        case (short)WebEnums.UserRole.APPROVER:
                            oListAccountHdrInfoForUser = oAccountHdrInfoCollection.Where(o => o.ApproverUserID == oUserHdrInfo.UserID).ToList<AccountHdrInfo>();
                            break;

                        //Backups

                        case (short)WebEnums.UserRole.BACKUP_PREPARER:
                            oListAccountHdrInfoForUser = oAccountHdrInfoCollection.Where(o => o.BackupPreparerUserID == oUserHdrInfo.UserID).ToList<AccountHdrInfo>();
                            break;
                        case (short)WebEnums.UserRole.BACKUP_REVIEWER:
                            oListAccountHdrInfoForUser = oAccountHdrInfoCollection.Where(o => o.BackupReviewerUserID == oUserHdrInfo.UserID).ToList<AccountHdrInfo>();
                            break;
                        case (short)WebEnums.UserRole.BACKUP_APPROVER:
                            oListAccountHdrInfoForUser = oAccountHdrInfoCollection.Where(o => o.BackupApproverUserID == oUserHdrInfo.UserID).ToList<AccountHdrInfo>();
                            break;
                    }
                    if (oListAccountHdrInfoForUser.Count > 0)
                    {
                        GetAccountDetailsForMail(AccountDetails, oListAccountHdrInfoForUser, oMultilingualAttributeInfo);
                        if (alertLabelID.HasValue)
                        {
                            alertDescription = GetAlertDescription(alertLabelID, oListAccountHdrInfoForUser.Count.ToString(), oMultilingualAttributeInfo);
                        }
                        mailBody.Append("<br>");
                        mailBody.Append(alertDescription);
                        mailBody.Append("<br>");
                        mailBody.Append(AccountDetails.ToString());
                    }
                    else if (oAccountHdrInfoCollection.Count > 0)
                    {
                        GetAccountDetailsForMail(AccountDetails, oAccountHdrInfoCollection, oMultilingualAttributeInfo);
                        if (alertLabelID.HasValue)
                        {
                            if (!string.IsNullOrEmpty(oUserHdrInfo.AlertReplacement))
                            {
                                alertDescription = GetAlertDescription(alertLabelID, oUserHdrInfo.AlertReplacement, oMultilingualAttributeInfo);
                            }
                            else
                            {
                                alertDescription = GetAlertDescription(alertLabelID, replacement, oMultilingualAttributeInfo);
                            }
                        }
                        mailBody.Append("<br>");
                        mailBody.Append(alertDescription);
                        mailBody.Append("<br>");
                        mailBody.Append(AccountDetails.ToString());
                    }
                }
                else
                {
                    if (alertLabelID.HasValue)
                    {
                        alertDescription = GetAlertDescription(alertLabelID, replacement, oMultilingualAttributeInfo);
                    }
                    mailBody.Append("<br>");
                    mailBody.Append(alertDescription);
                }


                mailBody.Append(MailHelper.GetEmailSignature(WebEnums.SignatureEnum.SendBySystemAdmin, fromEmailAddress, oMultilingualAttributeInfo));
                if (periodEndDate.HasValue)
                    MailSubject = alertDescription + " " + AppendVal;
                else
                    MailSubject = alertDescription;
                MailHelper.SendEmail(fromEmailAddress, oUserHdrInfo.EmailID, MailSubject, mailBody.ToString());
            }
        }

        private static void SendMailToAllUsers(string alertDescription, int? alertLabelID, string replacement, List<UserHdrInfo> oUserHdrInfoCollection, Dictionary<int, List<AccountHdrInfo>> oUserAccountHdrInfoDict, int actionLabelID, int roleLabelID)
        {
            MultilingualAttributeInfo oMultilingualAttributeInfo;
            string fromEmailAddress = AppSettingHelper.GetAppSettingValue(AppSettingConstants.EMAIL_FROM_DEFAULT);
            foreach (UserHdrInfo oUserHdrInfo in oUserHdrInfoCollection)
            {


                if (oUserHdrInfo.DefaultLanguageID.HasValue)
                    oMultilingualAttributeInfo = LanguageHelper.GetMultilingualAttributeInfo(SessionHelper.CurrentCompanyID, oUserHdrInfo.DefaultLanguageID);
                else
                    oMultilingualAttributeInfo = LanguageHelper.GetMultilingualAttributeInfo(SessionHelper.CurrentCompanyID, 1033);

                if (alertLabelID.HasValue)
                {
                    if (actionLabelID > 0 && roleLabelID > 0)
                    {
                        alertDescription = GetAlertDescription(alertLabelID, actionLabelID, roleLabelID, oMultilingualAttributeInfo);
                    }
                    else if (!string.IsNullOrEmpty(oUserHdrInfo.AlertReplacement))
                    {
                        alertDescription = GetAlertDescription(alertLabelID, oUserHdrInfo.AlertReplacement, oMultilingualAttributeInfo);
                    }
                    else
                    {
                        alertDescription = GetAlertDescription(alertLabelID, replacement, oMultilingualAttributeInfo);
                    }
                }

                StringBuilder mailBody = new StringBuilder();
                mailBody.Append(LanguageUtil.GetValue(1845, oMultilingualAttributeInfo));
                mailBody.Append(" ");
                mailBody.Append(oUserHdrInfo.FirstName);
                mailBody.Append("<br>");
                mailBody.Append(alertDescription);
                mailBody.Append("<br>");

                //To add account Details
                if (oUserAccountHdrInfoDict != null && oUserAccountHdrInfoDict.Count > 0)
                {
                    mailBody.Append("<br>");
                    StringBuilder AccountDetails = new StringBuilder();
                    List<AccountHdrInfo> oListAccountHdrInfoForUser = null;
                    if (oUserAccountHdrInfoDict.ContainsKey(oUserHdrInfo.UserID.Value))
                        oListAccountHdrInfoForUser = oUserAccountHdrInfoDict[oUserHdrInfo.UserID.Value];
                    GetAccountDetailsForMail(AccountDetails, oListAccountHdrInfoForUser, oMultilingualAttributeInfo);
                    mailBody.Append(AccountDetails.ToString());
                }

                mailBody.Append(MailHelper.GetEmailSignature(WebEnums.SignatureEnum.SendBySystemAdmin, fromEmailAddress, oMultilingualAttributeInfo));

                MailHelper.SendEmail(fromEmailAddress, oUserHdrInfo.EmailID, alertDescription, mailBody.ToString());
            }
        }

        private static void GetAccountDetailsForMail(StringBuilder AccountDetails, List<AccountHdrInfo> oListAccountHdrInfoForUser, MultilingualAttributeInfo oMultilingualAttributeInfo)
        {
            if (oListAccountHdrInfoForUser != null && oListAccountHdrInfoForUser.Count > 0)
            {

                // Get the Organizational Hierarchy based on Company ID
                List<GeographyStructureHdrInfo> oGeographyStructureHdrInfoCollection = SessionHelper.GetOrganizationalHierarchy(SessionHelper.CurrentCompanyID);

                AccountDetails.Append("<TABLE border=1 cellpadding=0 cellspacing=0>");
                AccountDetails.Append("<TR>");
                // FSCaption 
                AccountDetails.Append("<TH>");
                AccountDetails.Append(LanguageUtil.GetValue(1337, oMultilingualAttributeInfo)); //1337 
                AccountDetails.Append("</TH>");

                // Account Type
                AccountDetails.Append("<TH>");
                AccountDetails.Append(LanguageUtil.GetValue(1363, oMultilingualAttributeInfo)); //1363 
                AccountDetails.Append("</TH>");
                for (int i = 1; i < oGeographyStructureHdrInfoCollection.Count; i++)
                {

                    AccountDetails.Append("<TH>");
                    AccountDetails.Append(oGeographyStructureHdrInfoCollection[i].GeographyStructure);
                    AccountDetails.Append("</TH>");

                }
                // Account Number
                AccountDetails.Append("<TH>");
                AccountDetails.Append(LanguageUtil.GetValue(1357, oMultilingualAttributeInfo));
                AccountDetails.Append("</TH>");

                // Account Name
                AccountDetails.Append("<TH>");
                AccountDetails.Append(LanguageUtil.GetValue(1346, oMultilingualAttributeInfo));
                AccountDetails.Append("</TH>");

                AccountDetails.Append("</TR>");
                foreach (AccountHdrInfo oAccountHdrInfo in oListAccountHdrInfoForUser)
                {
                    AccountDetails.Append("<TR>");

                    // FSCaption
                    AccountDetails.Append("<TD>");
                    if (oAccountHdrInfo.FSCaptionLabelID.HasValue)
                        AccountDetails.Append(LanguageUtil.GetValue(oAccountHdrInfo.FSCaptionLabelID.Value, oMultilingualAttributeInfo));
                    else
                        AccountDetails.Append(" ");
                    AccountDetails.Append("</TD>");

                    // Account Type
                    AccountDetails.Append("<TD>");
                    if (oAccountHdrInfo.AccountTypeLabelID.HasValue)
                        AccountDetails.Append(LanguageUtil.GetValue(oAccountHdrInfo.AccountTypeLabelID.Value, oMultilingualAttributeInfo));
                    else
                        AccountDetails.Append(" ");
                    AccountDetails.Append("</TD>");

                    for (int i = 1; i < oGeographyStructureHdrInfoCollection.Count; i++)
                    {

                        string GeoClassName = "";
                        switch (oGeographyStructureHdrInfoCollection[i].GeographyClassID)
                        {
                            case (short)WebEnums.GeographyClass.Company:
                                GeoClassName = GeographyClassName.KEY1;
                                break;
                            case (short)WebEnums.GeographyClass.Key2:
                                GeoClassName = GeographyClassName.KEY2;
                                break;
                            case (short)WebEnums.GeographyClass.Key3:
                                GeoClassName = GeographyClassName.KEY3;
                                break;
                            case (short)WebEnums.GeographyClass.Key4:
                                GeoClassName = GeographyClassName.KEY4;
                                break;
                            case (short)WebEnums.GeographyClass.Key5:
                                GeoClassName = GeographyClassName.KEY5;
                                break;
                            case (short)WebEnums.GeographyClass.Key6:
                                GeoClassName = GeographyClassName.KEY6;
                                break;
                            case (short)WebEnums.GeographyClass.Key7:
                                GeoClassName = GeographyClassName.KEY7;
                                break;
                            case (short)WebEnums.GeographyClass.Key8:
                                GeoClassName = GeographyClassName.KEY8;
                                break;
                            case (short)WebEnums.GeographyClass.Key9:
                                GeoClassName = GeographyClassName.KEY9;
                                break;
                        }
                        AccountDetails.Append("<TD>");
                        AccountDetails.Append(oAccountHdrInfo.GetType().GetProperty(GeoClassName).GetValue(oAccountHdrInfo, null).ToString());
                        AccountDetails.Append("</TD>");

                    }
                    // Account Number
                    AccountDetails.Append("<TD>");
                    if (oAccountHdrInfo.AccountID != null && oAccountHdrInfo.AccountID > 0)
                        AccountDetails.Append(oAccountHdrInfo.AccountNumber);
                    else
                        AccountDetails.Append(LanguageUtil.GetValue(1257, oMultilingualAttributeInfo));
                    AccountDetails.Append("</TD>");

                    // Account Name
                    AccountDetails.Append("<TD>");
                    AccountDetails.Append(HttpContext.Current.Server.HtmlEncode(oAccountHdrInfo.AccountName));
                    AccountDetails.Append("</TD>");
                    AccountDetails.Append("</TR>");
                }

                AccountDetails.Append("</TABLE>");
            }
        }
        //private static void GetAccountDetailsForMail(StringBuilder AccountDetails, List<AccountHdrInfo> oListAccountHdrInfoForUser)
        //{
        //    if (oListAccountHdrInfoForUser != null && oListAccountHdrInfoForUser.Count > 0)
        //    {

        //        // Get the Organizational Hierarchy based on Company ID
        //        List<GeographyStructureHdrInfo> oGeographyStructureHdrInfoCollection = SessionHelper.GetOrganizationalHierarchy(SessionHelper.CurrentCompanyID);

        //        AccountDetails.Append("<TABLE border=1 cellpadding=0 cellspacing=0>");
        //        AccountDetails.Append("<TR>");
        //        // FSCaption 
        //        AccountDetails.Append("<TH>");
        //        AccountDetails.Append(Helper.GetLabelIDValue(1337)); //1337 
        //        AccountDetails.Append("</TH>");

        //        // Account Type
        //        AccountDetails.Append("<TH>");
        //        AccountDetails.Append(Helper.GetLabelIDValue(1363)); //1363 
        //        AccountDetails.Append("</TH>");
        //        for (int i = 1; i < oGeographyStructureHdrInfoCollection.Count; i++)
        //        {

        //            AccountDetails.Append("<TH>");
        //            AccountDetails.Append(oGeographyStructureHdrInfoCollection[i].GeographyStructure);
        //            AccountDetails.Append("</TH>");

        //        }
        //        // Account Number
        //        AccountDetails.Append("<TH>");
        //        AccountDetails.Append(Helper.GetLabelIDValue(1357));
        //        AccountDetails.Append("</TH>");

        //        // Account Name
        //        AccountDetails.Append("<TH>");
        //        AccountDetails.Append(Helper.GetLabelIDValue(1346));
        //        AccountDetails.Append("</TH>");

        //        AccountDetails.Append("</TR>");
        //        foreach (AccountHdrInfo oAccountHdrInfo in oListAccountHdrInfoForUser)
        //        {
        //            AccountDetails.Append("<TR>");

        //            // FSCaption
        //            AccountDetails.Append("<TD>");
        //            if (oAccountHdrInfo.FSCaptionLabelID.HasValue)
        //                AccountDetails.Append(Helper.GetLabelIDValue(oAccountHdrInfo.FSCaptionLabelID.Value));
        //            else
        //                AccountDetails.Append(" ");
        //            AccountDetails.Append("</TD>");

        //            // Account Type
        //            AccountDetails.Append("<TD>");
        //            if (oAccountHdrInfo.AccountTypeLabelID.HasValue)
        //                AccountDetails.Append(Helper.GetLabelIDValue(oAccountHdrInfo.AccountTypeLabelID.Value));
        //            else
        //                AccountDetails.Append(" ");
        //            AccountDetails.Append("</TD>");

        //            for (int i = 1; i < oGeographyStructureHdrInfoCollection.Count; i++)
        //            {

        //                string GeoClassName = "";
        //                switch (oGeographyStructureHdrInfoCollection[i].GeographyClassID)
        //                {
        //                    case (short)WebEnums.GeographyClass.Company:
        //                        GeoClassName = GeographyClassName.KEY1;
        //                        break;
        //                    case (short)WebEnums.GeographyClass.Key2:
        //                        GeoClassName = GeographyClassName.KEY2;
        //                        break;
        //                    case (short)WebEnums.GeographyClass.Key3:
        //                        GeoClassName = GeographyClassName.KEY3;
        //                        break;
        //                    case (short)WebEnums.GeographyClass.Key4:
        //                        GeoClassName = GeographyClassName.KEY4;
        //                        break;
        //                    case (short)WebEnums.GeographyClass.Key5:
        //                        GeoClassName = GeographyClassName.KEY5;
        //                        break;
        //                    case (short)WebEnums.GeographyClass.Key6:
        //                        GeoClassName = GeographyClassName.KEY6;
        //                        break;
        //                    case (short)WebEnums.GeographyClass.Key7:
        //                        GeoClassName = GeographyClassName.KEY7;
        //                        break;
        //                    case (short)WebEnums.GeographyClass.Key8:
        //                        GeoClassName = GeographyClassName.KEY8;
        //                        break;
        //                    case (short)WebEnums.GeographyClass.Key9:
        //                        GeoClassName = GeographyClassName.KEY9;
        //                        break;
        //                }
        //                AccountDetails.Append("<TD>");
        //                AccountDetails.Append(oAccountHdrInfo.GetType().GetProperty(GeoClassName).GetValue(oAccountHdrInfo, null).ToString());
        //                AccountDetails.Append("</TD>");

        //            }
        //            // Account Number
        //            AccountDetails.Append("<TD>");
        //            if (oAccountHdrInfo.AccountID != null && oAccountHdrInfo.AccountID > 0)
        //                AccountDetails.Append(oAccountHdrInfo.AccountNumber);
        //            else
        //                AccountDetails.Append(Helper.GetLabelIDValue(1257));
        //            AccountDetails.Append("</TD>");

        //            // Account Name
        //            AccountDetails.Append("<TD>");
        //            AccountDetails.Append(HttpContext.Current.Server.HtmlEncode(oAccountHdrInfo.AccountName));
        //            AccountDetails.Append("</TD>");
        //            AccountDetails.Append("</TR>");
        //        }

        //        AccountDetails.Append("</TABLE>");
        //    }
        //}

        private static void SendMailToAllUsers(string alertDescription, int? alertLabelID, string replacement, List<UserHdrInfo> oUserHdrInfoCollection, List<TaskHdrInfo> oTaskHdrInfoList, int actionLabelID, int roleLabelID)
        {
            string fromEmailAddress = AppSettingHelper.GetAppSettingValue(AppSettingConstants.EMAIL_FROM_DEFAULT);
            foreach (UserHdrInfo oUserHdrInfo in oUserHdrInfoCollection)
            {
                MultilingualAttributeInfo oMultilingualAttributeInfo;
                if (oUserHdrInfo.DefaultLanguageID.HasValue)
                    oMultilingualAttributeInfo = LanguageHelper.GetMultilingualAttributeInfo(SessionHelper.CurrentCompanyID, oUserHdrInfo.DefaultLanguageID);
                else
                    oMultilingualAttributeInfo = LanguageHelper.GetMultilingualAttributeInfo(SessionHelper.CurrentCompanyID, 1033);

                if (alertLabelID.HasValue)
                {
                    if (actionLabelID > 0 && roleLabelID > 0)
                    {
                        alertDescription = GetAlertDescription(alertLabelID, actionLabelID, roleLabelID, oMultilingualAttributeInfo);
                    }
                    else if (!string.IsNullOrEmpty(oUserHdrInfo.AlertReplacement))
                    {
                        alertDescription = GetAlertDescription(alertLabelID, oUserHdrInfo.AlertReplacement, oMultilingualAttributeInfo);
                    }
                    else
                    {
                        alertDescription = GetAlertDescription(alertLabelID, replacement, oMultilingualAttributeInfo);
                    }
                }

                StringBuilder mailBody = new StringBuilder();
                mailBody.Append(Helper.GetLabelIDValue(1845, oMultilingualAttributeInfo));
                mailBody.Append(" ");
                mailBody.Append(oUserHdrInfo.FirstName);
                mailBody.Append("<br>");
                mailBody.Append(alertDescription);
                mailBody.Append("<br>");

                //To add Task Details
                if (oTaskHdrInfoList != null && oTaskHdrInfoList.Count > 0)
                {
                    mailBody.Append("<br>");
                    StringBuilder TaskDetails = new StringBuilder();
                    List<TaskHdrInfo> oListTaskHdrInfoForUser = null;
                    switch (roleLabelID)
                    {
                        case 1130: // AssignedTo
                            oListTaskHdrInfoForUser = oTaskHdrInfoList.FindAll(obj => (obj.AssignedTo != null && obj.AssignedTo.Exists(obj1 => obj1.UserID == oUserHdrInfo.UserID)));
                           // oListTaskHdrInfoForUser = oTaskHdrInfoList.Where(o => o.AssignedTo.UserID == oUserHdrInfo.UserID).ToList<TaskHdrInfo>();
                            break;
                        case 1131: // Reviewer
                            oListTaskHdrInfoForUser = oTaskHdrInfoList.FindAll(obj => (obj.Reviewer != null && obj.Reviewer.Exists(obj1 => obj1.UserID == oUserHdrInfo.UserID)));                          
                            break;
                        case 1132: //Approver
                            oListTaskHdrInfoForUser = oTaskHdrInfoList.FindAll(obj => (obj.Approver != null && obj.Approver.Exists(obj1 => obj1.UserID == oUserHdrInfo.UserID)));
                            //oListTaskHdrInfoForUser = oTaskHdrInfoList.Where(o => o.Reviewer != null && o.Reviewer.UserID.HasValue && o.Reviewer.UserID == oUserHdrInfo.UserID).ToList<TaskHdrInfo>();
                            break;
                        case 1312: //Notify                            
                            oListTaskHdrInfoForUser = oTaskHdrInfoList.FindAll(obj => (obj.Notify != null && obj.Notify.Exists(obj1 => obj1.UserID == oUserHdrInfo.UserID)));
                            break;
                    }
                    GetTaskDetailsForMail(TaskDetails, oListTaskHdrInfoForUser, oMultilingualAttributeInfo);
                    mailBody.Append(TaskDetails.ToString());
                }

                mailBody.Append(MailHelper.GetEmailSignature(WebEnums.SignatureEnum.SendBySystemAdmin, fromEmailAddress, oMultilingualAttributeInfo));
                MailHelper.SendEmail(fromEmailAddress, oUserHdrInfo.EmailID, alertDescription, mailBody.ToString());
            }
        }

        private static void GetTaskDetailsForMail(StringBuilder TaskDetails, List<TaskHdrInfo> oListTaskHdrInfoForUser, MultilingualAttributeInfo oMultilingualAttributeInfo)
        {
            if (oListTaskHdrInfoForUser != null && oListTaskHdrInfoForUser.Count > 0)
            {

                List<TaskHdrInfo> oListAccountTask = oListTaskHdrInfoForUser.FindAll(obj => (obj.TaskAccount != null));

                TaskDetails.Append(SharedUtility.MailHelper.GetBeginTableHTML());
                TaskDetails.Append(SharedUtility.MailHelper.GetBeginHaderRowHTML());

                //Task#
                TaskDetails.Append("<TH>");
                TaskDetails.Append(Helper.GetLabelIDValue(2544, oMultilingualAttributeInfo));
                TaskDetails.Append("</TH>");

                //TaskName
                TaskDetails.Append("<TH>");
                TaskDetails.Append(Helper.GetLabelIDValue(2545, oMultilingualAttributeInfo));
                TaskDetails.Append("</TH>");

                //TaskDesc
                TaskDetails.Append("<TH>");
                TaskDetails.Append(Helper.GetLabelIDValue(1408, oMultilingualAttributeInfo));
                TaskDetails.Append("</TH>");

                // Start Date
                //TaskDetails.Append("<TH>");
                //TaskDetails.Append(Helper.GetLabelIDValue(1449));
                //TaskDetails.Append("</TH>");

                // Due Date
                TaskDetails.Append("<TH>");
                TaskDetails.Append(Helper.GetLabelIDValue(1499, oMultilingualAttributeInfo));
                TaskDetails.Append("</TH>");

                // Task Owner
                TaskDetails.Append("<TH>");
                TaskDetails.Append(Helper.GetLabelIDValue(2550, oMultilingualAttributeInfo));
                TaskDetails.Append("</TH>");

                // Task Reviewer
                TaskDetails.Append("<TH>");
                TaskDetails.Append(Helper.GetLabelIDValue(1131, oMultilingualAttributeInfo));
                TaskDetails.Append("</TH>");

                // Task Approver
                TaskDetails.Append("<TH>");
                TaskDetails.Append(Helper.GetLabelIDValue(1132, oMultilingualAttributeInfo));
                TaskDetails.Append("</TH>");

                TaskDetails.Append("</TR>");
                foreach (TaskHdrInfo oTaskHdrInfo in oListTaskHdrInfoForUser)
                {
                    //Task#
                    TaskDetails.Append("<TR>");
                    TaskDetails.Append("<TD>");
                    TaskDetails.Append(oTaskHdrInfo.TaskNumber);
                    TaskDetails.Append("</TD>");


                    // Task Nuame
                    TaskDetails.Append("<TD>");
                    TaskDetails.Append(oTaskHdrInfo.TaskName);
                    TaskDetails.Append("</TD>");

                    //TaskDesc
                    TaskDetails.Append("<TD>");
                    TaskDetails.Append(oTaskHdrInfo.TaskDescription);
                    TaskDetails.Append("</TD>");

                    // Start Date
                    //TaskDetails.Append("<TD>");
                    //TaskDetails.Append(Helper.GetDisplayDate(oTaskHdrInfo.TaskStartDate));
                    //TaskDetails.Append("</TD>");

                    // Due Date
                    TaskDetails.Append("<TD>");
                    TaskDetails.Append(Helper.GetDisplayDate(oTaskHdrInfo.TaskDueDate));
                    TaskDetails.Append("</TD>");

                    // Task Owner
                    TaskDetails.Append("<TD>");
                    TaskDetails.Append(Helper.GetDisplayTaskUserName(oTaskHdrInfo.AssignedTo));
                    TaskDetails.Append("</TD>");

                    // Task Reviewer
                    TaskDetails.Append("<TD>");
                    TaskDetails.Append(Helper.GetDisplayTaskUserName(oTaskHdrInfo.Reviewer));
                    TaskDetails.Append("</TD>");

                    // Task Approver
                    TaskDetails.Append("<TD>");
                    TaskDetails.Append(Helper.GetDisplayTaskUserName(oTaskHdrInfo.Approver));
                    TaskDetails.Append("</TD>");


                    TaskDetails.Append("</TR>");
                }

                TaskDetails.Append("</TABLE>");

                if (oListAccountTask != null && oListAccountTask.Count > 0)
                {
                    List<AccountHdrInfo> oListAccountHdrInfo = null;
                    oListAccountTask.ForEach(oListTask =>
                        {
                            oListAccountHdrInfo = oListTask.TaskAccount.ToList().FindAll(oListAccount => (oListAccount.AccountID.HasValue && oListAccount.AccountID != null));
                        });
                    StringBuilder oTaskAccounts = new StringBuilder();
                    if (oListAccountHdrInfo != null && oListAccountHdrInfo.Count > 0)
                    {

                        TaskDetails.Append("<br/><br/><B>");
                        TaskDetails.Append(Helper.GetLabelIDValue(2669, oMultilingualAttributeInfo));
                        TaskDetails.Append("</B><br/>");
                        GetAccountDetailsForMail(oTaskAccounts, oListAccountHdrInfo, oMultilingualAttributeInfo);
                        TaskDetails.Append(oTaskAccounts.ToString());
                    }
                }
            }
        }

        public static void NotifyForUser(List<int> UserIDCollection, List<TaskHdrInfo> oTaskHdrInfoList, string BodyDescription, string MailSubject)
        {
            IUser oUserClient = RemotingHelper.GetUserObject();
            List<UserHdrInfo> oUserHdrInfoCollection = oUserClient.SelectUserByUserID(UserIDCollection, Helper.GetAppUserInfo());
            //Notify For Deleted Task
            if (oUserHdrInfoCollection != null && oUserHdrInfoCollection.Count > 0)
            {
                SendMailToUsers(BodyDescription, MailSubject, oUserHdrInfoCollection, oTaskHdrInfoList);
            }
        }
        private static void SendMailToUsers(string BodyDescription, string MailSubject, List<UserHdrInfo> oUserHdrInfoCollection, List<TaskHdrInfo> oTaskHdrInfoList)
        {
            string fromEmailAddress = AppSettingHelper.GetAppSettingValue(AppSettingConstants.EMAIL_FROM_DEFAULT);
            foreach (UserHdrInfo oUserHdrInfo in oUserHdrInfoCollection)
            {
                MultilingualAttributeInfo oMultilingualAttributeInfo;
                if (oUserHdrInfo.DefaultLanguageID.HasValue)
                    oMultilingualAttributeInfo = LanguageHelper.GetMultilingualAttributeInfo(SessionHelper.CurrentCompanyID, oUserHdrInfo.DefaultLanguageID);
                else
                    oMultilingualAttributeInfo = LanguageHelper.GetMultilingualAttributeInfo(SessionHelper.CurrentCompanyID, 1033);

                StringBuilder mailBody = new StringBuilder();
                mailBody.Append(Helper.GetLabelIDValue(1845, oMultilingualAttributeInfo));
                mailBody.Append(" ");
                mailBody.Append(oUserHdrInfo.FirstName);
                mailBody.Append("<br>");
                mailBody.Append(BodyDescription);
                mailBody.Append("<br>");

                //To add Task Details
                if (oTaskHdrInfoList != null && oTaskHdrInfoList.Count > 0)
                {
                    mailBody.Append("<br>");
                    StringBuilder TaskDetails = new StringBuilder();
                    GetTaskDetailsForMail(TaskDetails, oTaskHdrInfoList, oMultilingualAttributeInfo);
                    mailBody.Append(TaskDetails.ToString());
                }

                mailBody.Append(MailHelper.GetEmailSignature(WebEnums.SignatureEnum.SendBySystemAdmin, fromEmailAddress, oMultilingualAttributeInfo));
                MailHelper.SendEmail(fromEmailAddress, oUserHdrInfo.EmailID, MailSubject, mailBody.ToString());
            }
        }

        private static List<AccountHdrInfo> GetAccountInformationForAlert(List<AccountHdrInfo> oAccountHdrInfoCollection, WebEnums.Alert eAlert, int? recPeriodID, List<long> oAccountIDCollection, List<int> oNetAccountIDCollection, short roleID)
        {

            IUser oUserClient = RemotingHelper.GetUserObject();
            WebEnums.UserRole? eRoleMailToBeSent = null;
            switch (eAlert)
            {
                case WebEnums.Alert.AccountReconciliationHasBeenRejected_Denied:
                case WebEnums.Alert.YouHaveXAccountsPendingModification:
                    if (roleID == (short)WebEnums.UserRole.REVIEWER)
                    {
                        eRoleMailToBeSent = WebEnums.UserRole.PREPARER;
                    }
                    else if (roleID == (short)WebEnums.UserRole.APPROVER)
                    {
                        eRoleMailToBeSent = WebEnums.UserRole.REVIEWER;
                    }
                    if (roleID == (short)WebEnums.UserRole.BACKUP_REVIEWER)
                    {
                        eRoleMailToBeSent = WebEnums.UserRole.PREPARER;
                    }
                    else if (roleID == (short)WebEnums.UserRole.BACKUP_APPROVER)
                    {
                        eRoleMailToBeSent = WebEnums.UserRole.REVIEWER;
                    }

                    break;

                case WebEnums.Alert.YouHaveXAccountsPendingReview:
                case WebEnums.Alert.YouHaveXAccountsPendingApproval:

                    if (roleID == (short)WebEnums.UserRole.REVIEWER)
                    {
                        eRoleMailToBeSent = WebEnums.UserRole.APPROVER;
                    }
                    else if (roleID == (short)WebEnums.UserRole.PREPARER)
                    {
                        eRoleMailToBeSent = WebEnums.UserRole.REVIEWER;
                    }
                    if (roleID == (short)WebEnums.UserRole.BACKUP_REVIEWER)
                    {
                        eRoleMailToBeSent = WebEnums.UserRole.APPROVER;
                    }
                    else if (roleID == (short)WebEnums.UserRole.BACKUP_PREPARER)
                    {
                        eRoleMailToBeSent = WebEnums.UserRole.REVIEWER;
                    }
                    break;
            }
            if ((oAccountHdrInfoCollection == null || (oAccountHdrInfoCollection.Count == 0 && (oAccountIDCollection.Count > 0 || oNetAccountIDCollection.Count > 0))) && recPeriodID.HasValue)
            {
                IAccount oAccountClient = RemotingHelper.GetAccountObject();
                oAccountHdrInfoCollection = oAccountClient.GetAccountInformationForAlertMail(oAccountIDCollection, oNetAccountIDCollection, recPeriodID.Value, (short)eAlert, (short)eRoleMailToBeSent, Helper.GetAppUserInfo());
                LanguageHelper.TranslateLabelsAccountHdr(oAccountHdrInfoCollection);
            }
            return oAccountHdrInfoCollection;
        }
    }
}
