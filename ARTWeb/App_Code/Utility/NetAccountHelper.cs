using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SkyStem.ART.Client.IServices;
using SkyStem.ART.Client.Model;
using SkyStem.ART.Web.Data;
using SkyStem.ART.Client.Data;
using SkyStem.ART.Client.Params;

namespace SkyStem.ART.Web.Utility
{
    /// <summary>
    /// Summary description for NetAccountHelper
    /// </summary>
    public class NetAccountHelper
    {
        private NetAccountHelper()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        #region Service Calls

        /// <summary>
        /// Get Net Account Attribute Values
        /// </summary>
        /// <param name="netAccountID"></param>
        /// <param name="companyID"></param>
        /// <param name="recPeriodID"></param>
        /// <returns></returns>
        public static List<NetAccountAttributeValueInfo> GetNetAccountAttributeValues(int netAccountID, int companyID, int recPeriodID)
        {
            IAccount oAccountClient = RemotingHelper.GetAccountObject();
            return oAccountClient.GetNetAccountAttributeValuesByNetAccountIDAndRecPeriodEndDate(netAccountID, companyID, recPeriodID, Helper.GetAppUserInfo());
        }

        /// <summary>
        /// Get Net Accounts
        /// </summary>
        /// <param name="companyID"></param>
        /// <param name="recPeriodID"></param>
        /// <returns></returns>
        public static List<NetAccountHdrInfo> GetNetAccounts(NetAccountSearchParamInfo oNetAccountSearchParamInfo)
        {
            IAccount oAccountClient = RemotingHelper.GetAccountObject();
            return oAccountClient.GetNetAccountHdrInfoCollection(oNetAccountSearchParamInfo, Helper.GetAppUserInfo());
        }

        /// <summary>
        /// Get Net Account Associated Accounts
        /// </summary>
        /// <param name="netAccountID"></param>
        /// <param name="recPeriodID"></param>
        /// <returns></returns>
        public static List<AccountHdrInfo> GetNetAccountAssociatedAccounts(int netAccountID, int recPeriodID)
        {
            IAccount oAccountClient = RemotingHelper.GetAccountObject();
            List<AccountHdrInfo> objAccountHdrInfoCollection = oAccountClient.GetNetAccountHdrInfoCollectionByNetID(recPeriodID, netAccountID, Helper.GetAppUserInfo());
            return LanguageHelper.TranslateLabelsAccountHdr(objAccountHdrInfoCollection);
        }

        /// <summary>
        /// Search Accounts
        /// </summary>
        /// <param name="oAccountSearchCriteria"></param>
        /// <returns></returns>
        public static List<AccountHdrInfo> SearchAccounts(AccountSearchCriteria oAccountSearchCriteria)
        {
            IAccount oAccountClient = RemotingHelper.GetAccountObject();
            List<AccountHdrInfo> oAccountHdrInfoCollection = oAccountClient.SearchAccount(oAccountSearchCriteria, Helper.GetAppUserInfo());
            return LanguageHelper.TranslateLabelsAccountHdr(oAccountHdrInfoCollection);
        }

        /// <summary>
        /// Update Net Account
        /// </summary>
        /// <param name="oNetAccountHdrInfo"></param>
        /// <param name="oAccountHdrInfoList"></param>
        /// <param name="companyID"></param>
        /// <param name="recPeriodID"></param>
        /// <param name="recPeriodEndDate"></param>
        /// <param name="userLoginID"></param>      
        /// <returns></returns>
        public static int? UpdateNetAccount(NetAccountHdrInfo oNetAccountHdrInfo, List<AccountHdrInfo> oAccountHdrInfoList, int companyID, int recPeriodID, DateTime recPeriodEndDate, string userLoginID)
        {
            IAccount oAccountClient = RemotingHelper.GetAccountObject();
            return oAccountClient.UpdateNetAccount(oNetAccountHdrInfo, oAccountHdrInfoList, null, companyID, recPeriodID, recPeriodEndDate, userLoginID, DateTime.Now, (short)ARTEnums.ActionType.ChangeNetAccountComposition, (short)ARTEnums.ChangeSource.NetAccountCompositionChange, Helper.GetAppUserInfo());
        }

        /// <summary>
        /// Delete Net Account
        /// </summary>
        /// <param name="oNetAccountParamInfo"></param>
        /// <returns></returns>
        public static int? DeleteNetAccount(NetAccountParamInfo oNetAccountParamInfo)
        {
            IAccount oIAccount = RemotingHelper.GetAccountObject();
            oNetAccountParamInfo.DateRevised = DateTime.Now;
            oNetAccountParamInfo.ActionTypeID = (short)ARTEnums.ActionType.DeleteNetAccount;
            oNetAccountParamInfo.ChangeSourceIDSRA = (short)ARTEnums.ChangeSource.DeleteNetAccount;
            return oIAccount.DeleteNetAccount(oNetAccountParamInfo, Helper.GetAppUserInfo());
        }

        /// <summary>
        /// Remove Account from Net Account
        /// </summary>
        /// <param name="oNetAccountParamInfo"></param>
        public static void RemoveAccountFromNetAccount(NetAccountParamInfo oNetAccountParamInfo)
        {
            IAccount oAccountClient = RemotingHelper.GetAccountObject();
            oNetAccountParamInfo.DateRevised = DateTime.Now;
            oNetAccountParamInfo.ActionTypeID = (short)ARTEnums.ActionType.ChangeNetAccountComposition;
            oNetAccountParamInfo.ChangeSourceIDSRA = (short)ARTEnums.ChangeSource.NetAccountCompositionChange;
            oAccountClient.DeleteAccountsFromNetAccount(oNetAccountParamInfo, Helper.GetAppUserInfo());
        }

        /// <summary>
        /// Check whether Net Account is duplicate or not
        /// </summary>
        /// <param name="netAccountName"></param>
        /// <param name="companyID"></param>
        /// <param name="recPeriodID"></param>
        /// <returns></returns>
        public static bool IsNetAccountDuplicate(string netAccountName, int companyID, int recPeriodID)
        {
            IAccount oAccountClient = RemotingHelper.GetAccountObject();
            return oAccountClient.IsNetAccountDuplicate(netAccountName, companyID, recPeriodID, Helper.GetAppUserInfo());
        }

        #endregion

        #region Utility Functions

        /// <summary>
        /// Check Whether Account is valid to be part of Net Account
        /// </summary>
        /// <param name="oAccountHdrInfo"></param>
        /// <returns></returns>
        public static bool IsAccountAttributesValidForNetAccount(AccountHdrInfo oAccountHdrInfo)
        {
            if (oAccountHdrInfo == null)
                return false;

            bool isValid = true;
            if (isValid && Helper.GetFeatureCapabilityModeForCurrentRecPeriod(WebEnums.Feature.KeyAccount, ARTEnums.Capability.KeyAccount) == WebEnums.FeatureCapabilityMode.Visible)
            {
                if (oAccountHdrInfo.IsKeyAccount == null)
                    isValid = false;
            }
            if (isValid && Helper.IsCapabilityActivatedForCurrentRecPeriod(ARTEnums.Capability.ZeroBalanceAccount))
            {
                if (oAccountHdrInfo.IsZeroBalance == null)
                    isValid = false;
            }

            if (isValid)
            {
                if (Helper.IsCapabilityActivatedForCurrentRecPeriod(ARTEnums.Capability.RiskRating))
                {
                    if (oAccountHdrInfo.RiskRatingID == null || oAccountHdrInfo.RiskRatingID == 0)
                        isValid = false;
                }
                else
                {   // Rec Frequency not set
                    if (oAccountHdrInfo.RecPeriodIDCollection == null || oAccountHdrInfo.RecPeriodIDCollection.Count < 1)
                    {
                        isValid = false;
                    }
                }
            }

            if (isValid && (oAccountHdrInfo.ReconciliationTemplateID == null || oAccountHdrInfo.ReconciliationTemplateID == 0))
                isValid = false;

            if (isValid && (oAccountHdrInfo.PreparerUserID == null || oAccountHdrInfo.PreparerUserID == 0))
                isValid = false;

            if (isValid && (oAccountHdrInfo.ReviewerUserID == null || oAccountHdrInfo.ReviewerUserID == 0))
                isValid = false;

            if (isValid && Helper.GetFeatureCapabilityModeForCurrentRecPeriod(WebEnums.Feature.DualLevelReview, ARTEnums.Capability.DualLevelReview) == WebEnums.FeatureCapabilityMode.Visible)
            {
                if (isValid && !Helper.IsDualLevelReviewByAccountActivated() 
                    && (oAccountHdrInfo.ApproverUserID == null || oAccountHdrInfo.ApproverUserID == 0))
                    isValid = false;
            }
            if (isValid && Helper.GetFeatureCapabilityModeForCurrentRecPeriod(WebEnums.Feature.DueDateByAccount, ARTEnums.Capability.DueDateByAccount) == WebEnums.FeatureCapabilityMode.Visible)
            {
                if (isValid && (oAccountHdrInfo.PreparerDueDays == null || oAccountHdrInfo.PreparerDueDays == 0))
                    isValid = false;
                if (isValid && (oAccountHdrInfo.ReviewerDueDays == null || oAccountHdrInfo.ReviewerDueDays == 0))
                    isValid = false;
                if (isValid && Helper.IsCapabilityActivatedForCurrentRecPeriod(ARTEnums.Capability.DualLevelReview))
                {
                    if (isValid && !Helper.IsDualLevelReviewByAccountActivated() 
                        && (oAccountHdrInfo.ApproverDueDays == null || oAccountHdrInfo.ApproverDueDays == 0))
                        isValid = false;
                }
            }
            return isValid;
        }

        /// <summary>
        /// Match Two Accounts to be Part of Net Account
        /// </summary>
        /// <param name="oAccountHdrInfoFirst"></param>
        /// <param name="oAccountHdrInfoSecond"></param>
        /// <returns></returns>
        public static bool IsAccountAttributesMatchForNetAccount(AccountHdrInfo oAccountHdrInfoFirst, AccountHdrInfo oAccountHdrInfoSecond)
        {
            bool isMatch = true;
            if (oAccountHdrInfoFirst != null && oAccountHdrInfoSecond != null)
            {
                if (isMatch && Helper.GetFeatureCapabilityModeForCurrentRecPeriod(WebEnums.Feature.KeyAccount, ARTEnums.Capability.KeyAccount) == WebEnums.FeatureCapabilityMode.Visible)
                {
                    if (oAccountHdrInfoFirst.IsKeyAccount != oAccountHdrInfoSecond.IsKeyAccount)
                        isMatch = false;
                }
                if (isMatch && Helper.IsCapabilityActivatedForCurrentRecPeriod(ARTEnums.Capability.ZeroBalanceAccount))
                {
                    if (oAccountHdrInfoFirst.IsZeroBalance != oAccountHdrInfoSecond.IsZeroBalance)
                        isMatch = false;
                }

                if (isMatch)
                {
                    if (Helper.IsCapabilityActivatedForCurrentRecPeriod(ARTEnums.Capability.RiskRating))
                    {
                        if (oAccountHdrInfoFirst.RiskRatingID != oAccountHdrInfoSecond.RiskRatingID)
                            isMatch = false;
                    }
                    else
                        isMatch = IsRecPeriodCollectionSame(oAccountHdrInfoFirst.RecPeriodIDCollection, oAccountHdrInfoSecond.RecPeriodIDCollection);
                }

                if (isMatch && oAccountHdrInfoFirst.IsReconcilable.GetValueOrDefault() != oAccountHdrInfoSecond.IsReconcilable.GetValueOrDefault())
                    isMatch = false;

                if (isMatch && (oAccountHdrInfoFirst.ReconciliationTemplateID != oAccountHdrInfoSecond.ReconciliationTemplateID))
                    isMatch = false;

                if (isMatch && !IsAccountOwnershipAttributesMatchForNetAccount(oAccountHdrInfoFirst, oAccountHdrInfoSecond))
                    isMatch = false;
                if (isMatch && Helper.IsFeatureActivated(WebEnums.Feature.DueDateByAccount, SessionHelper.CurrentReconciliationPeriodID))
                {
                    //if (isMatch && (oAccountHdrInfoFirst.DayTypeID != oAccountHdrInfoSecond.DayTypeID))
                    //    isMatch = false;
                    if (isMatch && (oAccountHdrInfoFirst.PreparerDueDays != oAccountHdrInfoSecond.PreparerDueDays))
                        isMatch = false;
                    if (isMatch && (oAccountHdrInfoFirst.ReviewerDueDays != oAccountHdrInfoSecond.ReviewerDueDays))
                        isMatch = false;
                    if (isMatch && Helper.IsCapabilityActivatedForCurrentRecPeriod(ARTEnums.Capability.DualLevelReview))
                    {
                        if (isMatch && (oAccountHdrInfoFirst.ApproverDueDays != oAccountHdrInfoSecond.ApproverDueDays))
                            isMatch = false;
                    }
                }
               
            }
            return isMatch;
        }

        /// <summary>
        /// Is Account Ownership Attributes match for Net Account
        /// </summary>
        /// <param name="oAccountHdrInfoFirst"></param>
        /// <param name="oAccountHdrInfoSecond"></param>
        /// <returns></returns>
        public static bool IsAccountOwnershipAttributesMatchForNetAccount(AccountHdrInfo oAccountHdrInfoFirst, AccountHdrInfo oAccountHdrInfoSecond)
        {
            bool isMatch = true;
            if (oAccountHdrInfoFirst != null && oAccountHdrInfoSecond != null)
            {
                if (isMatch && (oAccountHdrInfoFirst.PreparerUserID.GetValueOrDefault() != oAccountHdrInfoSecond.PreparerUserID.GetValueOrDefault()))
                    isMatch = false;

                if (isMatch && (oAccountHdrInfoFirst.ReviewerUserID.GetValueOrDefault() != oAccountHdrInfoSecond.ReviewerUserID.GetValueOrDefault()))
                    isMatch = false;

                if (isMatch && Helper.GetFeatureCapabilityModeForCurrentRecPeriod(WebEnums.Feature.DualLevelReview, ARTEnums.Capability.DualLevelReview) == WebEnums.FeatureCapabilityMode.Visible)
                {
                    if (oAccountHdrInfoFirst.ApproverUserID.GetValueOrDefault() != oAccountHdrInfoSecond.ApproverUserID.GetValueOrDefault())
                        isMatch = false;
                }
                if (isMatch && (oAccountHdrInfoFirst.BackupPreparerUserID.GetValueOrDefault() != oAccountHdrInfoSecond.BackupPreparerUserID.GetValueOrDefault()))
                    isMatch = false;

                if (isMatch && (oAccountHdrInfoFirst.BackupReviewerUserID.GetValueOrDefault() != oAccountHdrInfoSecond.BackupReviewerUserID.GetValueOrDefault()))
                    isMatch = false;

                if (isMatch && Helper.GetFeatureCapabilityModeForCurrentRecPeriod(WebEnums.Feature.DualLevelReview, ARTEnums.Capability.DualLevelReview) == WebEnums.FeatureCapabilityMode.Visible)
                {
                    if (oAccountHdrInfoFirst.BackupApproverUserID.GetValueOrDefault() != oAccountHdrInfoSecond.BackupApproverUserID.GetValueOrDefault())
                        isMatch = false;
                }
            }
            return isMatch;
        }

        /// <summary>
        /// Compare Rec Period Collection
        /// </summary>
        /// <param name="firstList"></param>
        /// <param name="secondList"></param>
        /// <returns></returns>
        public static bool IsRecPeriodCollectionSame(List<int> firstList, List<int> secondList)
        {
            if (firstList == null && secondList == null)
                return true;
            if ((firstList == null && secondList != null) || (firstList != null && secondList == null) || (firstList.Count != secondList.Count))
                return false;
            foreach (int i in firstList)
                if (!secondList.Contains(i))
                    return false;
            foreach (int i in secondList)
                if (!firstList.Contains(i))
                    return false;
            return true;
        }

        /// <summary>
        /// Check that all constituent accounts are present in selected list and return boolean
        /// </summary>
        /// <param name="netAccountID"></param>
        /// <param name="recPeriodID"></param>
        /// <param name="oAccountIDList"></param>
        /// <returns></returns>
        public static bool IsAllConstituentAccountsSelected(int netAccountID, int recPeriodID, List<long> oAccountIDList)
        {
            if (oAccountIDList != null && oAccountIDList.Count > 0)
            {
                List<AccountHdrInfo> oAccountHdrInfoList = NetAccountHelper.GetNetAccountAssociatedAccounts(netAccountID, recPeriodID);
                foreach (AccountHdrInfo oAccountHdrInfo in oAccountHdrInfoList)
                {
                    if (oAccountHdrInfo.AccountID.GetValueOrDefault() > 0)
                    {
                        long? accountID = oAccountIDList.Find(T => T == oAccountHdrInfo.AccountID.Value);
                        if (accountID.GetValueOrDefault() < 1)
                            return false;
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// To check that the account owner of net account constuient accounts are active or not.
        /// </summary>
        /// <returns></returns>
        public static bool IsAllNetAccountOwnersActive(List<AccountHdrInfo> oAccountHdrInfoListAdded)
        {
            bool isActive = false;
            if (oAccountHdrInfoListAdded != null && oAccountHdrInfoListAdded.Count > 0)
            {
                List<UserHdrInfo> oListUserHdrInfo = CacheHelper.SelectAllUsersForCurrentCompany();
                List<UserHdrInfo> oListUser = new List<UserHdrInfo>(); ;
                foreach (var oAccountHdrInfo in oAccountHdrInfoListAdded)
                {
                    if (oAccountHdrInfo.PreparerUserID != null && oAccountHdrInfo.PreparerUserID > 0)
                        oListUser.Add(oListUserHdrInfo.Where(user => user.UserID == oAccountHdrInfo.PreparerUserID).First());

                    if (oAccountHdrInfo.ReviewerUserID != null && oAccountHdrInfo.ReviewerUserID > 0)
                        oListUser.Add(oListUserHdrInfo.Where(user => user.UserID == oAccountHdrInfo.ReviewerUserID).First());

                    if (oAccountHdrInfo.ApproverUserID != null && oAccountHdrInfo.ApproverUserID > 0)
                        oListUser.Add(oListUserHdrInfo.Where(user => user.UserID == oAccountHdrInfo.ApproverUserID).First());

                    if (oAccountHdrInfo.BackupPreparerUserID != null && oAccountHdrInfo.BackupPreparerUserID > 0)
                        oListUser.Add(oListUserHdrInfo.Where(user => user.UserID == oAccountHdrInfo.BackupPreparerUserID).First());

                    if (oAccountHdrInfo.BackupReviewerUserID != null && oAccountHdrInfo.BackupReviewerUserID > 0)
                        oListUser.Add(oListUserHdrInfo.Where(user => user.UserID == oAccountHdrInfo.BackupReviewerUserID).First());

                    if (oAccountHdrInfo.BackupApproverUserID != null && oAccountHdrInfo.BackupApproverUserID > 0)
                        oListUser.Add(oListUserHdrInfo.Where(user => user.UserID == oAccountHdrInfo.BackupApproverUserID).First());
                }
                return Helper.IsUserActive(oListUser);
            }
            return isActive;
        }
        /// <summary>
        /// Get Constituent Account ID's
        /// </summary>
        /// <param name="netAccountID"></param>
        /// <param name="recPeriodID"></param>
        /// <returns></returns>
        public static List<long> GetAllConstituentAccounts(int netAccountID, int recPeriodID)
        {
            List<long> AccountIDs = new List<long>();
            List<AccountHdrInfo> oAccountHdrInfoList = NetAccountHelper.GetNetAccountAssociatedAccounts(netAccountID, recPeriodID);
            if (oAccountHdrInfoList != null && oAccountHdrInfoList.Count > 0)
            {
                foreach (AccountHdrInfo oAccountHdrInfo in oAccountHdrInfoList)
                    AccountIDs.Add(oAccountHdrInfo.AccountID.Value);
            }
            return AccountIDs;
        }

        /// <summary>
        /// Get Constituent Account ID's
        /// </summary>
        /// <param name="netAccountID"></param>
        /// <param name="recPeriodID"></param>
        /// <returns></returns>
        public static List<long> GetAccountsHaveDiffrentAttributeValueInFuture(List<long> AccountIDList)
        {
            List<long> AccountIDs = null;
            IAccount oAccountClient = RemotingHelper.GetAccountObject();
            AccountIDs = oAccountClient.GetAccountsHaveDiffrentAttributeValueInFuture(AccountIDList, SessionHelper.CurrentReconciliationPeriodID, Helper.GetAppUserInfo());           
            return AccountIDs;
        }

        #endregion
    }
}