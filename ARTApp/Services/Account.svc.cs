using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using SkyStem.ART.Client.IServices;
using SkyStem.ART.Client.Model;
using SkyStem.ART.App.DAO;
using System.Data;
using SkyStem.ART.App.Utility;
using System.Data.SqlClient;
using SkyStem.ART.Client.Data;
using SkyStem.ART.Client.Params;

namespace SkyStem.ART.App.Services
{
    // NOTE: If you change the class name "Account" here, you must also update the reference to "Account" in Web.config.
    public class Account : IAccount
    {

        public AccountHdrInfo GetAccountHdrInfoByAccountID(long accountID, int companyID, int recPeriodID, AppUserInfo oAppUserInfo)
        {
            AccountHdrInfo oAccountHdrInfo = null;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                AccountHdrDAO oAccountHdrDAO = new AccountHdrDAO(oAppUserInfo);
                oAccountHdrInfo = oAccountHdrDAO.GetAccountHdrInfoByAccountID(accountID, companyID, recPeriodID);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return oAccountHdrInfo;
        }

        /// <summary>
        /// Selects all account types for the system
        /// </summary>
        /// <returns>List of account types</returns>
        public List<AccountTypeMstInfo> SelectAllAccountTypeMstInfo(AppUserInfo oAppUserInfo)
        {
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                AccountTypeMstDAO oAccountTypeMstDAO = new AccountTypeMstDAO(oAppUserInfo);
                return oAccountTypeMstDAO.SelectAllAccountTypeMstInfo();
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return null;
        }

        /// <summary>
        /// Searches database and selects all accounts which matches the search criteria
        /// </summary>
        /// <param name="oAccountSearchCriteria">object containing all search criteria</param>
        /// <returns>List of accounts which matches the search criteria</returns>
        public List<AccountHdrInfo> SearchAccount(AccountSearchCriteria oAccountSearchCriteria, AppUserInfo oAppUserInfo)
        {
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                AccountHdrDAO oAccountHdrDAO = new AccountHdrDAO(oAppUserInfo);
                return oAccountHdrDAO.SearchAccount(oAccountSearchCriteria);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }

            return null;
        }

        public List<AccountHdrInfo> GetAccountByOrganisationalHierarchy(AccountSearchCriteria oAccountSearchCriteria, AppUserInfo oAppUserInfo)
        {
            List<AccountHdrInfo> oAccountHdrInfoCollection = null;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                AccountHdrDAO oAccountHdrDAO = new AccountHdrDAO(oAppUserInfo);
                return oAccountHdrInfoCollection = oAccountHdrDAO.GetAccountByOrganisationalHierarchy(oAccountSearchCriteria);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return oAccountHdrInfoCollection;
        }

        /// <summary>
        /// Saves account ownership information in the database
        /// </summary>
        /// <param name="oAccountHdrInfoCollection">List of account header info</param>
        /// <returns>True/false</returns>

        public bool SaveAccountOwnerships(List<AccountHdrInfo> oAccountHdrInfoCollection, int comapnyId, int recPeriodID, bool isDualReviewEnabled,
            string userLoginID, DateTime updateTime, short preparerRoleID, short reviewerRoleID, short approverRoleID
            , short backupPreparerRoleID, short backupReviewerRoleID, short backupApproverRoleID, short actionTypeID, AppUserInfo oAppUserInfo)
        {

            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                AccountHdrDAO oAccountHdrDAO = new AccountHdrDAO(oAppUserInfo);
                DataTable dtAccountHdrTableType = ServiceHelper.ConvertAccountHdrInfoCollectionToDataTable(oAccountHdrInfoCollection);

                return oAccountHdrDAO.SaveAccountOwnerships(dtAccountHdrTableType, comapnyId, recPeriodID, isDualReviewEnabled, userLoginID,
                    updateTime, preparerRoleID, reviewerRoleID, approverRoleID, backupPreparerRoleID, backupReviewerRoleID, backupApproverRoleID, actionTypeID);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }

            return false;
        }

        public List<AccountHdrInfo> SelectOrganisationHiearachyByUserIDRoleID(int userID, short roleID, AppUserInfo oAppUserInfo)
        {
            List<AccountHdrInfo> oAccountHdrInfoCollection = null;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                AccountHdrDAO oAccountHdrDAO = new AccountHdrDAO(oAppUserInfo);
                oAccountHdrInfoCollection = oAccountHdrDAO.GetAccountOrganisationalHierarchyByUserIDRoleID(userID, roleID);

            }

            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return oAccountHdrInfoCollection;
        }

        public List<NetAccountHdrInfo> GetNetAccountHdrInfoCollection(NetAccountSearchParamInfo oNetAccountSearchParamInfo, AppUserInfo oAppUserInfo)
        {
            List<NetAccountHdrInfo> oNetAccountHdrInfoCollection = null;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                NetAccountHdrDAO oNetAccountHdrDAO = new NetAccountHdrDAO(oAppUserInfo);
                oNetAccountHdrInfoCollection = oNetAccountHdrDAO.GetNetAccountHdrInfoCollection(oNetAccountSearchParamInfo);

            }

            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return oNetAccountHdrInfoCollection;
        }

        public List<AccountHdrInfo> GetNetAccountHdrInfoCollectionByNetID(int recperiodID, int netAccountID, AppUserInfo oAppUserInfo)
        {
            List<AccountHdrInfo> oAccountHdrInfoCollection = null;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                AccountHdrDAO oAccountHdrDAO = new AccountHdrDAO(oAppUserInfo);
                oAccountHdrInfoCollection = oAccountHdrDAO.GetNetAccountHdrInfoCollectionByNetID(recperiodID, netAccountID);

            }

            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return oAccountHdrInfoCollection;
        }

        public bool SaveAccountMassUpdate(List<long> oAccountIDCollection, int companyId, int currentRecPeriodId, ARTEnums.AccountAttribute oAccountAttribute, string accountAttributeValue, string userLoginID, DateTime updateTime, short actionTypeID, AppUserInfo oAppUserInfo)
        {
            bool result = false;
            IDbConnection oConnection = null;
            IDbTransaction oTransaction = null;

            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                //DataTable dtAccountIds = ServiceHelper.ConvertLongIDCollectionToDataTable(oAccountIDCollection);
                DataTable dtAccountIds = ServiceHelper.ConvertAccountIDListToAccountAttributeValueDataTable(oAccountIDCollection, (short)oAccountAttribute, accountAttributeValue, null);
                ServiceHelper.SetConnectionString(oAppUserInfo);
                AccountHdrDAO oAccountHdrDAO = new AccountHdrDAO(oAppUserInfo);
                using (oConnection = oAccountHdrDAO.CreateConnection())
                {
                    oConnection.Open();
                    using (oTransaction = oConnection.BeginTransaction())
                    {
                        //result = oAccountHdrDAO.SaveAccountMassUpdate(dtAccountIds, companyId, currentRecPeriodId, (short)oAccountAttribute, accountAttributeValue, null, userLoginID, updateTime, oConnection, oTransaction);
                        result = oAccountHdrDAO.SaveAccountAttributeValue(dtAccountIds, currentRecPeriodId, userLoginID, updateTime, actionTypeID, oConnection, oTransaction);
                        oTransaction.Commit();
                    }
                }
            }
            catch (SqlException ex)
            {
                try
                {
                    oTransaction.Rollback();
                }
                catch { }
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                try
                {
                    oTransaction.Rollback();
                }
                catch { }
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return result;
        }

        public bool SaveAccountRecFrequecy(List<long> oAccountIDCollection, List<int> oRecPeriodIDCollection, int companyId, int currentRecPeriodId, AppUserInfo oAppUserInfo)
        {
            bool result = false;
            IDbConnection oConnection = null;
            IDbTransaction oTransaction = null;

            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                DataTable dtAccountIds = ServiceHelper.ConvertLongIDCollectionToDataTable(oAccountIDCollection);
                DataTable dtRecPeriodIds = ServiceHelper.ConvertIDCollectionToDataTable(oRecPeriodIDCollection);
                ServiceHelper.SetConnectionString(oAppUserInfo);
                AccountReconciliationPeriodDAO oAccountReconciliationPeriodDAO = new AccountReconciliationPeriodDAO(oAppUserInfo);
                using (oConnection = oAccountReconciliationPeriodDAO.CreateConnection())
                {
                    oConnection.Open();
                    using (oTransaction = oConnection.BeginTransaction())
                    {
                        result = oAccountReconciliationPeriodDAO.SaveAccountRecFrequecy(dtAccountIds, dtRecPeriodIds, companyId, currentRecPeriodId, oConnection, oTransaction);
                        oTransaction.Commit();
                    }
                }
            }
            catch (SqlException ex)
            {
                try
                {
                    oTransaction.Rollback();
                }
                catch { }
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                try
                {
                    oTransaction.Rollback();
                }
                catch { }
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return result;
        }

        public bool? GetAccountMateriality(int companyID, long accountID, int recPeriodID, bool isMaterialityEnabled, AppUserInfo oAppUserInfo)
        {
            bool? result = false;

            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                AccountHdrDAO oAccountHdrDAO = new AccountHdrDAO(oAppUserInfo);
                result = oAccountHdrDAO.GetAccountMateriality(companyID, accountID, recPeriodID, isMaterialityEnabled);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }

            return result;
        }

        //insert new net account detail
        public void UpdateNetAccountAttributeValue(NetAccountHdrInfo oNetAccountHdrInfo, List<NetAccountAttributeValueInfo> lstNetAccountAttributeValueInfo, int recPeriodID, AppUserInfo oAppUserInfo)
        {
            ServiceHelper.SetConnectionString(oAppUserInfo);
            NetAccountHdrDAO oNetAccountHdrDAO = new NetAccountHdrDAO(oAppUserInfo);
            oNetAccountHdrDAO.UpdateNetAccountAttributeValue(oNetAccountHdrInfo, lstNetAccountAttributeValueInfo, recPeriodID);
        }

        public int? UpdateNetAccount(NetAccountHdrInfo oNetAccountHdrInfo, List<AccountHdrInfo> oAccountHdrInfocollection, List<AccountHdrInfo> oDeletedAccountHdrInfoCollection, int companyID, int recPeriodID, DateTime recPeriodEndDate, string addedBy, DateTime dateRevised, short actionTypeID, short changeSourceIDSRA, AppUserInfo oAppUserInfo)
        {
            IDbConnection oConnection = null;
            IDbTransaction oTransaction = null;

            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                NetAccountHdrDAO oNetAccountHdrDAO = new NetAccountHdrDAO(oAppUserInfo);
                AccountHdrDAO oAccountHdrDAO = new AccountHdrDAO(oAppUserInfo);
                //TODO: validation for number of Licenses to user company.


                using (oConnection = oNetAccountHdrDAO.CreateConnection())
                {
                    oConnection.Open();
                    using (oTransaction = oConnection.BeginTransaction())
                    {
                        // Vinay: Company is needed to update NetAccount Attribute so moved outside the if condition
                        oNetAccountHdrInfo.CompanyID = companyID;
                        if (oNetAccountHdrInfo != null && (oNetAccountHdrInfo.NetAccountID == null || oNetAccountHdrInfo.NetAccountID == 0))
                        {
                            //oNetAccountHdrInfo.CompanyID = companyID;
                            oNetAccountHdrDAO.Save(oNetAccountHdrInfo, oConnection, oTransaction);
                        }

                        if (oNetAccountHdrInfo.NetAccountID.HasValue)
                        {
                            foreach (AccountHdrInfo oAccountHdrInfo in oAccountHdrInfocollection)
                            {
                                oAccountHdrInfo.NetAccountID = oNetAccountHdrInfo.NetAccountID.Value;
                            }

                            foreach (NetAccountAttributeValueInfo oNetAccountAttributeValueInfo in oNetAccountHdrInfo.NetAccountAttributeValueInfoList)
                            {
                                oNetAccountAttributeValueInfo.NetAccountID = oNetAccountHdrInfo.NetAccountID.Value;
                            }
                        }

                        oNetAccountHdrDAO.UpdateNetAccountAttributeValue(oNetAccountHdrInfo, oNetAccountHdrInfo.NetAccountAttributeValueInfoList, recPeriodID, oConnection, oTransaction);
                        // Account Processing should be done only when accounts are changed
                        if (oNetAccountHdrInfo.IsAccountSelectionChanged.GetValueOrDefault())
                        {
                            //DataTable oAccountIDTable = ServiceHelper.ConvertLongIDCollectionToDataTable((from acc in oAccountHdrInfocollection select acc.AccountID.HasValue ? acc.AccountID.Value : 0).ToList());
                            List<long> oAccountIDList = (from acc in oAccountHdrInfocollection select acc.AccountID.HasValue ? acc.AccountID.Value : 0).ToList();
                            DataTable oAccountIDTable = ServiceHelper.ConvertAccountIDListToAccountAttributeValueDataTable(oAccountIDList, (short)ARTEnums.AccountAttribute.NetAccount, oAccountHdrInfocollection[0].NetAccountID.ToString(), null);
                            //Update accounts for net account attribute
                            //oAccountHdrDAO.SaveAccountMassUpdate(oAccountIDTable, companyID, recPeriodID, (short)ARTEnums.AccountAttribute.NetAccount, oAccountHdrInfocollection[0].NetAccountID.ToString(), null, addedBy, dateRevised, oConnection, oTransaction);
                            oAccountHdrDAO.SaveAccountAttributeValue(oAccountIDTable, recPeriodID, addedBy, dateRevised, actionTypeID, oConnection, oTransaction);
                            if (oDeletedAccountHdrInfoCollection != null && oDeletedAccountHdrInfoCollection.Count > 0)
                            {
                                //DataTable oDeletedAccountIDTable = ServiceHelper.ConvertLongIDCollectionToDataTable((from delAcc in oDeletedAccountHdrInfoCollection select delAcc.AccountID.HasValue ? delAcc.AccountID.Value : 0).ToList());
                                List<long> oAccountIDListDeleted = (from delAcc in oDeletedAccountHdrInfoCollection select delAcc.AccountID.HasValue ? delAcc.AccountID.Value : 0).ToList();
                                DataTable oAccountIDTableDeleted = ServiceHelper.ConvertAccountIDListToAccountAttributeValueDataTable(oAccountIDListDeleted, (short)ARTEnums.AccountAttribute.NetAccount, null, null);
                                //Delete accounts from net account
                                //oAccountHdrDAO.SaveAccountMassUpdate(oDeletedAccountIDTable, companyID, recPeriodID, (short)ARTEnums.AccountAttribute.NetAccount, null, null, addedBy, dateRevised, oConnection, oTransaction);
                                oAccountHdrDAO.SaveAccountAttributeValue(oAccountIDTableDeleted, recPeriodID, addedBy, dateRevised, actionTypeID, oConnection, oTransaction);
                            }

                            //Insert/Update net account row in gl data hdr table
                            List<int> netAcctList = new List<int>();
                            netAcctList.Add(oAccountHdrInfocollection[0].NetAccountID.Value);
                            DataTable dtNetAccountID = ServiceHelper.ConvertIDCollectionToDataTable(netAcctList);
                            oAccountHdrDAO.UpdateGLDataHdrForNetAccount(oConnection, oTransaction, recPeriodID, dtNetAccountID, addedBy, dateRevised, actionTypeID);
                            //Update net account row in gl data hdr for SRA
                            object oSRA = oAccountHdrDAO.UpdateGLDataNetAccountHdrForSRA((int)oAccountHdrInfocollection[0].NetAccountID, oConnection, oTransaction, companyID, recPeriodID, recPeriodEndDate, addedBy, dateRevised, actionTypeID, changeSourceIDSRA);
                        }
                        oTransaction.Commit();
                    }
                }
                if (oNetAccountHdrInfo != null) //Case:New
                    return oNetAccountHdrInfo.NetAccountID;
                else
                    return null;
            }
            catch (SqlException ex)
            {
                try
                {
                    oTransaction.Rollback();
                }
                catch { }
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                try
                {
                    oTransaction.Rollback();
                }
                catch { }
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return null;
        }

        public bool SaveAccountProfile(List<AccountHdrInfo> oAccountHdrInfoCollection, int companyId, int currentRecPeriodId, string userLoginID, DateTime updateTime, short actionTypeID, AppUserInfo oAppUserInfo)
        {
            bool result = false;
            IDbConnection oConnection = null;
            IDbTransaction oTransaction = null;

            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                AccountHdrDAO oAccountHdrDAO = new AccountHdrDAO(oAppUserInfo);
                using (oConnection = oAccountHdrDAO.CreateConnection())
                {
                    oConnection.Open();
                    using (oTransaction = oConnection.BeginTransaction())
                    {

                        DataTable dtAccountAttributeValue = ServiceHelper.ConvertAccountHdrInfoListToAccountAttributeValueDataTable(oAccountHdrInfoCollection);

                        result = oAccountHdrDAO.SaveAccountAttributeValue(dtAccountAttributeValue, currentRecPeriodId, userLoginID, updateTime, actionTypeID, oConnection, oTransaction);
                        //TODO: Optimize below as well
                        foreach (AccountHdrInfo oAccountHdrInfo in oAccountHdrInfoCollection)
                        {
                            List<long> oAccountIDList = new List<long>();
                            oAccountIDList.Add(oAccountHdrInfo.AccountID.Value);
                            DataTable dtAccountIds = ServiceHelper.ConvertLongIDCollectionToDataTable(oAccountIDList);
                            if (oAccountHdrInfo.RecPeriodIDCollection != null && oAccountHdrInfo.RecPeriodIDCollection.Count > 0)
                            {
                                DataTable dtRecPeriodIds = ServiceHelper.ConvertIDCollectionToDataTable(oAccountHdrInfo.RecPeriodIDCollection);
                                AccountReconciliationPeriodDAO oAccountReconciliationPeriodDAO = new AccountReconciliationPeriodDAO(oAppUserInfo);
                                result = oAccountReconciliationPeriodDAO.SaveAccountRecFrequecy(dtAccountIds, dtRecPeriodIds, companyId, currentRecPeriodId, oConnection, oTransaction);
                            }
                        }
                        oTransaction.Commit();
                    }
                }
            }
            catch (SqlException ex)
            {
                try
                {
                    oTransaction.Rollback();
                }
                catch { }
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                try
                {
                    oTransaction.Rollback();
                }
                catch { }
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return result;
        }

        public List<AccountReconciliationPeriodInfo> SelectAccountRecPeriodByAccountID(long accountID, AppUserInfo oAppUserInfo)
        {
            List<AccountReconciliationPeriodInfo> oAccountReconciliationPeriodInfoCollection = null;

            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                AccountReconciliationPeriodDAO oAccountReconciliationPeriodDAO = new AccountReconciliationPeriodDAO(oAppUserInfo);
                oAccountReconciliationPeriodInfoCollection = oAccountReconciliationPeriodDAO.SelectAccountRecPeriodByAccountID(accountID);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }

            return oAccountReconciliationPeriodInfoCollection;
        }

        public string GetNetAccountNameByNetAccountID(int netAccountID, int companyID, int lcID, int businessEntityID, int defaultLCID, AppUserInfo oAppUserInfo)
        {
            string netAccountName = null;

            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                NetAccountHdrDAO oNetAccountHdrDAO = new NetAccountHdrDAO(oAppUserInfo);
                netAccountName = oNetAccountHdrDAO.GetNetAccountNameByNetAccountID(netAccountID, companyID, lcID, businessEntityID, defaultLCID);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }

            return netAccountName;
        }

        public void DeleteAccountsFromNetAccount(NetAccountParamInfo oNetAccountParamInfo, AppUserInfo oAppUserInfo)
        {

            IDbConnection oConnection = null;
            IDbTransaction oTransaction = null;

            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                AccountHdrDAO oAccountHdrDAO = new AccountHdrDAO(oAppUserInfo);
                using (oConnection = oAccountHdrDAO.CreateConnection())
                {
                    oConnection.Open();
                    using (oTransaction = oConnection.BeginTransaction())
                    {
                        RemoveAccountsFromNetAccount(oNetAccountParamInfo, oConnection, oTransaction, oAppUserInfo);
                        oTransaction.Commit();
                    }
                }
            }
            catch (SqlException ex)
            {
                try
                {
                    oTransaction.Rollback();
                }
                catch { }
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                try
                {
                    oTransaction.Rollback();
                }
                catch { }
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
        }

        public int? DeleteNetAccount(NetAccountParamInfo oNetAccountParamInfo, AppUserInfo oAppUserInfo)
        {

            IDbConnection oConnection = null;
            IDbTransaction oTransaction = null;
            int? count = null;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                NetAccountHdrDAO oNetAccountHdrDAO = new NetAccountHdrDAO(oAppUserInfo);
                using (oConnection = oNetAccountHdrDAO.CreateConnection())
                {
                    oConnection.Open();
                    using (oTransaction = oConnection.BeginTransaction())
                    {
                        RemoveAccountsFromNetAccount(oNetAccountParamInfo, oConnection, oTransaction, oAppUserInfo);
                         count = oNetAccountHdrDAO.DeleteNetAccount(oNetAccountParamInfo, oConnection, oTransaction);
                        oTransaction.Commit();
                    }
                }
                return count;
            }
            catch (SqlException ex)
            {
                try
                {
                    oTransaction.Rollback();
                }
                catch { }
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                try
                {
                    oTransaction.Rollback();
                }
                catch { }
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return null;
        }

        private void RemoveAccountsFromNetAccount(NetAccountParamInfo oNetAccountParamInfo, IDbConnection oConnection, IDbTransaction oTransaction, AppUserInfo oAppUserInfo)
        {
            List<long> oAccountIDList = (from delAcc in oNetAccountParamInfo.AccountHdrInfoList select delAcc.AccountID.HasValue ? delAcc.AccountID.Value : 0).ToList();
            DataTable oDeletedAccountIDTable = ServiceHelper.ConvertAccountIDListToAccountAttributeValueDataTable(oAccountIDList, (short)ARTEnums.AccountAttribute.NetAccount, null, null);
            //Delete accounts from net account
            if (oDeletedAccountIDTable != null && oDeletedAccountIDTable.Rows.Count > 0)
            {
                AccountHdrDAO oAccountHdrDAO = new AccountHdrDAO(oAppUserInfo);
                oAccountHdrDAO.SaveAccountAttributeValue(oDeletedAccountIDTable, (int)oNetAccountParamInfo.RecPeriodID,
                    oNetAccountParamInfo.UserLoginID, oNetAccountParamInfo.DateRevised, oNetAccountParamInfo.ActionTypeID, oConnection, oTransaction);

                List<int> netAcctList = new List<int>();
                netAcctList.Add((int)oNetAccountParamInfo.AccountHdrInfoList[0].NetAccountID);
                DataTable dtNetAccountID = ServiceHelper.ConvertIDCollectionToDataTable(netAcctList);
                oAccountHdrDAO.UpdateGLDataHdrForNetAccount(oConnection, oTransaction, (int)oNetAccountParamInfo.RecPeriodID,
                    dtNetAccountID, oNetAccountParamInfo.UserLoginID, oNetAccountParamInfo.DateRevised, oNetAccountParamInfo.ActionTypeID);
                object oSRA = oAccountHdrDAO.UpdateGLDataNetAccountHdrForSRA((int)oNetAccountParamInfo.AccountHdrInfoList[0].NetAccountID, oConnection, oTransaction,
                    (int)oNetAccountParamInfo.CompanyID, (int)oNetAccountParamInfo.RecPeriodID, (DateTime)oNetAccountParamInfo.RecPeriodEndDate,
                    oNetAccountParamInfo.UserLoginID, oNetAccountParamInfo.DateRevised, oNetAccountParamInfo.ActionTypeID, oNetAccountParamInfo.ChangeSourceIDSRA);
            }
        }

        public List<NetAccountAttributeValueInfo> GetNetAccountAttributeValuesByNetAccountIDAndRecPeriodEndDate(int netAccountID, int companyID, int ReconciliationPeriodID, AppUserInfo oAppUserInfo)
        {
            List<NetAccountAttributeValueInfo> lstNetAccountAttributeValueInfo = null;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                NetAccountAttributeValueDAO oNetAccountAttributeValueDAO = new NetAccountAttributeValueDAO(oAppUserInfo);
                lstNetAccountAttributeValueInfo = oNetAccountAttributeValueDAO.GetNetAccountAttributesValue(netAccountID, companyID, ReconciliationPeriodID);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return lstNetAccountAttributeValueInfo;
        }

        public bool IsNetAccountDuplicate(string netAccountName, int companyID, int ReconciliationPeriodID, AppUserInfo oAppUserInfo)
        {
            bool IsDuplicate = true;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                NetAccountHdrDAO oNetAccountHdrDAO = new NetAccountHdrDAO(oAppUserInfo);
                IsDuplicate = oNetAccountHdrDAO.IsNetAccountDuplicate(netAccountName, companyID, ReconciliationPeriodID);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return IsDuplicate;
        }
        public List<AccountHdrInfo> GetAccountHdrInfoListByAccountIDs(List<long> oAccountIDList, int? recperiodID, AppUserInfo oAppUserInfo)
        {
            List<AccountHdrInfo> oAccountHdrInfoCollection = null;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                DataTable oAccountIDTable = ServiceHelper.ConvertIDCollectionToDataTable(oAccountIDList);
                AccountHdrDAO oAccountHdrDAO = new AccountHdrDAO(oAppUserInfo);
                oAccountHdrInfoCollection = oAccountHdrDAO.GetAccountHdrInfoListByAccountIDs(oAccountIDTable, recperiodID);

            }

            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return oAccountHdrInfoCollection;
        }
        public List<long> GetAccountIDListByUserIDs(List<int> oUserIDList, int? recperiodID, AppUserInfo oAppUserInfo)
        {
            List<long> oAccountIDCollection = null;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                DataTable oUserIDTable = ServiceHelper.ConvertIDCollectionToDataTable(oUserIDList);
                AccountHdrDAO oAccountHdrDAO = new AccountHdrDAO(oAppUserInfo);
                oAccountIDCollection = oAccountHdrDAO.GetAccountIDListByUserIDs(oUserIDTable, recperiodID);

            }

            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return oAccountIDCollection;
        }

        public List<AccountHdrInfo> GetAccountInformationWithoutGL(int? UserID, short? RoleID, int RecPeriodID, AppUserInfo oAppUserInfo)
        {
            List<AccountHdrInfo> oListAccountHdrInfo = null;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                AccountHdrDAO oAccountHdrDAO = new AccountHdrDAO(oAppUserInfo);
                oListAccountHdrInfo = oAccountHdrDAO.GetAccountInformationWithoutGL(UserID, RoleID, RecPeriodID);

            }

            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return oListAccountHdrInfo;
        }
        public AccountAttributeWarningInfo GetAccountAttributeChangeWarning(List<AccountAttributeWarningInfo> oAccountIDNetAccountIDCollection, List<AccountHdrInfo> oAccountAttributeCollection, int RecPeriodID, AppUserInfo oAppUserInfo)
        {
            AccountAttributeWarningInfo oAccountAttributeWarningInfo = null;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                AccountHdrDAO oAccountHdrDAO = new AccountHdrDAO(oAppUserInfo);
                DataTable dtAccountIDNetAccountID = ServiceHelper.ConvertAccountIDNetAccountIDCollectionToDataTable(oAccountIDNetAccountIDCollection);
                DataTable dtAccountAttributeValue = ServiceHelper.ConvertAccountHdrInfoListToAccountAttributeValueDataTable(oAccountAttributeCollection);
                oAccountAttributeWarningInfo = oAccountHdrDAO.GetAccountAttributeChangeWarning(dtAccountIDNetAccountID, dtAccountAttributeValue, RecPeriodID);

            }

            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return oAccountAttributeWarningInfo;
        }

        public List<AccountHdrInfo> GetNewAccounts(int? DataImportID, AppUserInfo oAppUserInfo)
        {
            List<AccountHdrInfo> oListAccountHdrInfo = null;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                AccountHdrDAO oAccountHdrDAO = new AccountHdrDAO(oAppUserInfo);
                oListAccountHdrInfo = oAccountHdrDAO.GetNewAccounts(DataImportID);
            }

            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return oListAccountHdrInfo;
        }

        public List<AccountHdrInfo> GetAccountInformationForCompanyAlertMail(int RecPeriodID, int UserID, short RoleID, long CompanyAlertDetailID, AppUserInfo oAppUserInfo)
        {
            List<AccountHdrInfo> oListAccountHdrInfo = null;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                AccountHdrDAO oAccountHdrDAO = new AccountHdrDAO(oAppUserInfo);
                oListAccountHdrInfo = oAccountHdrDAO.GetAccountInformationForCompanyAlertMail(RecPeriodID, UserID, RoleID, CompanyAlertDetailID);

            }

            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return oListAccountHdrInfo;
        }
        public List<AccountHdrInfo> GetAccountInformationForAlertMail(List<long> oAccountIDList, List<int> oNetAccountIDList, int recperiodID, short? AlertID, short? RoleID, AppUserInfo oAppUserInfo)
        {
            List<AccountHdrInfo> oAccountHdrInfoCollection = null;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                DataTable oAccountIDTable = ServiceHelper.ConvertIDCollectionToDataTable(oAccountIDList);
                DataTable oNetAccountIDTable = ServiceHelper.ConvertIDCollectionToDataTable(oNetAccountIDList);
                AccountHdrDAO oAccountHdrDAO = new AccountHdrDAO(oAppUserInfo);
                oAccountHdrInfoCollection = oAccountHdrDAO.GetAccountInformationForAlertMail(oAccountIDTable, oNetAccountIDTable, recperiodID, AlertID, RoleID);

            }

            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return oAccountHdrInfoCollection;
        }

        public List<AccountHdrInfo> GetAccountHdrInfo(List<long> oAccountIDList, AppUserInfo oAppUserInfo)
        {
            List<AccountHdrInfo> oAccountHdrInfoCollection = null;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                DataTable oAccountIDTable = ServiceHelper.ConvertIDCollectionToDataTable(oAccountIDList);
                AccountHdrDAO oAccountHdrDAO = new AccountHdrDAO(oAppUserInfo);
                oAccountHdrInfoCollection = oAccountHdrDAO.GetAccountHdrInfo(oAccountIDTable);

            }

            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return oAccountHdrInfoCollection;
        }

        public List<long> GetAccountsHaveDiffrentAttributeValueInFuture(List<long> AccountIDList, int? recPeriodID, AppUserInfo oAppUserInfo)
        {
            List<long> oAccountIDList = null;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                AccountHdrDAO oAccountHdrDAO = new AccountHdrDAO(oAppUserInfo);
                DataTable oAccountIDTable = ServiceHelper.ConvertIDCollectionToDataTable(AccountIDList);
                oAccountIDList = oAccountHdrDAO.GetAccountsHaveDiffrentAttributeValueInFuture(oAccountIDTable, recPeriodID);
            }

            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return oAccountIDList;
        }


    }//end of class
}//end of namespace
