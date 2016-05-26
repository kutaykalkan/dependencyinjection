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
using SkyStem.ART.Client.Model;
using SkyStem.ART.Web.Data;
using SkyStem.ART.Client.IServices;
using System.Web.Caching;
using System.Collections.Generic;
using SkyStem.ART.Client.Model.Matching;
using SkyStem.ART.App.Services;
using SkyStem.ART.Client.Params.Matching;
using SkyStem.ART.Client.Model.QualityScore;
using SkyStem.ART.Client.Model.MappingUpload;
using SkyStem.ART.Client.Params;
using SkyStem.ART.Client.Data;



namespace SkyStem.ART.Web.Utility
{
    /// <summary>
    /// Summary description for CacheHelper
    /// </summary>
    public class CacheHelper
    {
        public CacheHelper()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public static string GetCacheKeyForCompanyData(string cacheKey)
        {
            return cacheKey + "_" + SessionHelper.CurrentCompanyID.GetValueOrDefault().ToString()
                + "_" + SessionHelper.CurrentFinancialYearID.GetValueOrDefault().ToString();
        }

        public static string GetCacheKeyForCompanyDataAndAduitRole(string cacheKey)
        {
            return cacheKey + "_" + "AuditRole" + "_" + SessionHelper.CurrentCompanyID.GetValueOrDefault().ToString()
                + "_" + SessionHelper.CurrentUserID.GetValueOrDefault().ToString()
                + "_" + SessionHelper.CurrentRoleID.GetValueOrDefault().ToString()
                + "_" + SessionHelper.CurrentFinancialYearID.GetValueOrDefault().ToString();
        }

        private static DateTime GetCacheExpirationTime()
        {
            int noOfHrs = 24; // Default Value
            if (!string.IsNullOrEmpty(AppSettingHelper.GetAppSettingValue(AppSettingConstants.CACHE_REFRESH_RATE)))
            {
                noOfHrs = Convert.ToInt32(AppSettingHelper.GetAppSettingValue(AppSettingConstants.CACHE_REFRESH_RATE));
            }
            return DateTime.Now.AddHours(noOfHrs);
        }

        /// <summary>
        /// Get All the Companies
        /// </summary>
        /// <returns></returns>
        public static List<CompanyHdrInfo> GetAllCompaniesLiteObject()
        {
            List<CompanyHdrInfo> oCompanyHdrInfoCollection = (List<CompanyHdrInfo>)HttpRuntime.Cache[CacheConstants.ALL_COMPANIES_LITE_OBJECT];
            if (oCompanyHdrInfoCollection == null)
            {
                // Get from DB
                try
                {
                    ICompany oCompanyClient = RemotingHelper.GetCompanyObject();
                    oCompanyHdrInfoCollection = oCompanyClient.GetAllCompaniesLiteObject();
                    HttpRuntime.Cache.Add(CacheConstants.ALL_COMPANIES_LITE_OBJECT, oCompanyHdrInfoCollection, null, GetCacheExpirationTime(), Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);
                }
                catch (Exception ex)
                {
                    Helper.FormatAndShowErrorMessage(null, ex);
                }
            }
            return oCompanyHdrInfoCollection;
        }

        public static int GetRoleID(string key)
        {
            bool bAddToCache = false;
            Dictionary<string, int> oRoleIDDictionary = (Dictionary<string, int>)HttpRuntime.Cache[CacheConstants.ROLE_ID_LIST];

            if (oRoleIDDictionary == null)
            {
                oRoleIDDictionary = new Dictionary<string, int>();
                bAddToCache = true;
            }

            if (!oRoleIDDictionary.ContainsKey(key))
            {
                // Get from DB and Add to Dictionary
                IUtility oUtilityClient = RemotingHelper.GetUtilityObject();
                int? roleID = oUtilityClient.GetRoleID(key, Helper.GetAppUserInfo());
                oRoleIDDictionary.Add(key, roleID.Value);
            }

            if (bAddToCache)
            {
                HttpRuntime.Cache.Add(CacheConstants.ROLE_ID_LIST, oRoleIDDictionary, null, GetCacheExpirationTime(), Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);
            }

            return oRoleIDDictionary[key];
        }

        public static List<GeographyStructureHdrInfo> GetOrganizationalHierarchy(int? CompanyID)
        {
            if (CompanyID == null)
            {
                throw new Exception(WebConstants.COMPANY_NOT_SPECIFIED);
            }

            bool bAddToCache = false;
            Dictionary<int, List<GeographyStructureHdrInfo>> oGeographyStructureDictionary = (Dictionary<int, List<GeographyStructureHdrInfo>>)HttpRuntime.Cache[CacheConstants.COMPANY_STRUCTURE];

            if (oGeographyStructureDictionary == null)
            {
                oGeographyStructureDictionary = new Dictionary<int, List<GeographyStructureHdrInfo>>();
                bAddToCache = true;
            }

            if (!oGeographyStructureDictionary.ContainsKey(CompanyID.Value))
            {
                // Get from DB and Add to Dictionary
                ICompany oCompanyClient = RemotingHelper.GetCompanyObject();
                List<GeographyStructureHdrInfo> oGeographyStructureHdrInfoCollection = oCompanyClient.GetOrganizationalHierarchy(CompanyID, Helper.GetAppUserInfo());
                oGeographyStructureDictionary.Add(CompanyID.Value, oGeographyStructureHdrInfoCollection);
            }

            if (bAddToCache)
            {
                HttpRuntime.Cache.Add(CacheConstants.COMPANY_STRUCTURE, oGeographyStructureDictionary, null, GetCacheExpirationTime(), Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);
            }

            return oGeographyStructureDictionary[CompanyID.Value];
        }

        public static void ClearAllRoleByCompanyID(int? companyID)
        {
            Dictionary<int?, Dictionary<int?, List<RoleMstInfo>>> dicAllRole = null;
            dicAllRole = (Dictionary<int?, Dictionary<int?, List<RoleMstInfo>>>)HttpRuntime.Cache[CacheConstants.ALL_ROLE_LIST];

            if (dicAllRole != null && dicAllRole.Count > 0)
            {
                if (dicAllRole.ContainsKey(Convert.ToInt32(companyID)))
                {
                    dicAllRole.Remove(Convert.ToInt32(companyID));
                }
            }
        }

        /// <summary>
        /// Get All the Roles
        /// </summary>
        /// <returns></returns>
        public static List<RoleMstInfo> GetAllRoles()
        {
            List<RoleMstInfo> oRoleMstInfoList = null;
            Dictionary<int?, Dictionary<int?, List<RoleMstInfo>>> dicAllRole = null;
            Dictionary<int?, List<RoleMstInfo>> dictCompanyRole = null;
            int? recPeriodID = SessionHelper.CurrentReconciliationPeriodID.GetValueOrDefault();
            int? companyID = SessionHelper.CurrentCompanyID;
            DateTime? periodEndDate = SessionHelper.CurrentReconciliationPeriodEndDate;

            // Get Dictionary from Cache
            dicAllRole = (Dictionary<int?, Dictionary<int?, List<RoleMstInfo>>>)HttpRuntime.Cache[CacheConstants.ALL_ROLE_LIST];

            // If null create new
            if (dicAllRole == null)
                dicAllRole = new Dictionary<int?, Dictionary<int?, List<RoleMstInfo>>>();

            // Get Company Role Dictionary
            if (dicAllRole.ContainsKey(companyID))
                dictCompanyRole = dicAllRole[companyID];

            // If null create new
            if (dictCompanyRole == null)
            {
                dictCompanyRole = new Dictionary<int?, List<RoleMstInfo>>();
                dicAllRole.Add(companyID, dictCompanyRole);
            }

            if (dictCompanyRole.ContainsKey(recPeriodID))
                oRoleMstInfoList = dictCompanyRole[recPeriodID];
            else
            {
                // Get from DB
                try
                {
                    IRole oRoleClient = RemotingHelper.GetRoleObject();
                    oRoleMstInfoList = oRoleClient.GetAllRole(companyID, periodEndDate, Helper.GetAppUserInfo());
                    dictCompanyRole.Add(recPeriodID, oRoleMstInfoList);
                    HttpRuntime.Cache.Add(CacheConstants.ALL_ROLE_LIST, dicAllRole, null, GetCacheExpirationTime(), Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);
                }
                catch (Exception ex)
                {
                    Helper.FormatAndShowErrorMessage(null, ex);
                }
            }
            return oRoleMstInfoList;
        }

        #region "Data Import Type"
        /// <summary>
        /// Gets all data import types
        /// </summary>
        /// <returns>list of DataImportTypeMstInfo object</returns>
        public static IList<DataImportTypeMstInfo> GetAllDataImportType(short? RoleID)
        {


            IList<DataImportTypeMstInfo> lstDataImportTypeMstInfoCollection = (List<DataImportTypeMstInfo>)HttpRuntime.Cache[CacheConstants.ALL_DATAIMPORTTYPE_LIST];
            if (lstDataImportTypeMstInfoCollection == null)
            {
                //get from db
                IUtility IUtil = RemotingHelper.GetUtilityObject();
                lstDataImportTypeMstInfoCollection = IUtil.GetAllImportType(Helper.GetAppUserInfo());
                HttpRuntime.Cache.Add(CacheConstants.ALL_DATAIMPORTTYPE_LIST, lstDataImportTypeMstInfoCollection, null, GetCacheExpirationTime(), Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);
            }

            var oDataImportTypeMstInfo =
                      (from o in lstDataImportTypeMstInfoCollection
                       where o.RoleID.Value == RoleID.Value
                       select o).ToList<DataImportTypeMstInfo>();

            return oDataImportTypeMstInfo;
        }
        /// <summary>
        /// get a data import type based on dataimporttypeID
        /// </summary>
        /// <param name="DataImportTypeID"></param>
        /// <returns>DataImportTypeMstInfo object</returns>
        public static DataImportTypeMstInfo GetDataImportType(short? DataImportTypeID)
        {
            List<DataImportTypeMstInfo> lstDataImportTypeCollection = CacheHelper.GetAllDataImportType(SessionHelper.CurrentRoleID) as List<DataImportTypeMstInfo>;
            DataImportTypeMstInfo oDataImportType = lstDataImportTypeCollection.Find(e => e.DataImportTypeID == DataImportTypeID);
            return oDataImportType;
        }
        #endregion

        #region "master table's data in cache"

        public static IList<MaterialityTypeMstInfo> GetAllMaterialityType()
        {
            //bool bAddToCache = false;
            IList<MaterialityTypeMstInfo> lstMaterialityTypeMstInfo = (IList<MaterialityTypeMstInfo>)HttpRuntime.Cache[CacheConstants.ALL_MATERIALITYTYPE_LIST];
            //Can there be a situation where its not null but count=0? 
            if (lstMaterialityTypeMstInfo == null)
            {
                try
                {
                    lstMaterialityTypeMstInfo = new List<MaterialityTypeMstInfo>();
                    // Get from DB 
                    IUtility oUtilityClient = RemotingHelper.GetUtilityObject();
                    lstMaterialityTypeMstInfo = oUtilityClient.SelectAllMaterialityTypeMst(Helper.GetAppUserInfo());
                    HttpRuntime.Cache.Add(CacheConstants.ALL_MATERIALITYTYPE_LIST, lstMaterialityTypeMstInfo, null, GetCacheExpirationTime(), Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);
                }
                catch (Exception ex)
                {
                    Helper.FormatAndShowErrorMessage(null, ex);
                }
            }
            return lstMaterialityTypeMstInfo;
        }

        public static IList<DualLevelReviewTypeMstInfo> GetAllDualLevelReviewType()
        {
            //bool bAddToCache = false;
            IList<DualLevelReviewTypeMstInfo> lstDualLevelReviewTypeMstInfo = (IList<DualLevelReviewTypeMstInfo>)HttpRuntime.Cache[CacheConstants.ALL_DUALLEVELREVIEWTYPE_LIST];
            //Can there be a situation where its not null but count=0? 
            if (lstDualLevelReviewTypeMstInfo == null)
            {
                try
                {
                    lstDualLevelReviewTypeMstInfo = new List<DualLevelReviewTypeMstInfo>();
                    // Get from DB 
                    IUtility oUtilityClient = RemotingHelper.GetUtilityObject();
                    lstDualLevelReviewTypeMstInfo = oUtilityClient.SelectAllDualLevelReviewTypeMst(Helper.GetAppUserInfo());
                    HttpRuntime.Cache.Add(CacheConstants.ALL_DUALLEVELREVIEWTYPE_LIST, lstDualLevelReviewTypeMstInfo, null, GetCacheExpirationTime(), Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);
                }
                catch (Exception ex)
                {
                    Helper.FormatAndShowErrorMessage(null, ex);
                }
            }
            return lstDualLevelReviewTypeMstInfo;
        }

        public static List<RiskRatingMstInfo> GetAllRiskRating()
        {
            List<RiskRatingMstInfo> oRiskRatingMstInfoCollection = (List<RiskRatingMstInfo>)HttpRuntime.Cache[CacheConstants.ALL_RISKRATING_LIST];
            if (oRiskRatingMstInfoCollection == null)
            {
                try
                {
                    oRiskRatingMstInfoCollection = new List<RiskRatingMstInfo>();
                    // Get from DB 
                    IUtility oUtilityClient = RemotingHelper.GetUtilityObject();
                    oRiskRatingMstInfoCollection = oUtilityClient.SelectAllRiskRating(Helper.GetAppUserInfo());
                    HttpRuntime.Cache.Add(CacheConstants.ALL_RISKRATING_LIST, oRiskRatingMstInfoCollection, null, GetCacheExpirationTime(), Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);
                }
                catch (Exception ex)
                {
                    Helper.FormatAndShowErrorMessage(null, ex);
                }
            }

            //((List<RiskRatingMstInfo>)lstRiskRatingMstInfo).RemoveAll(riskRating => riskRating.RiskRatingID == Convert.ToInt32(WebConstants.SELECT_ONE)
            //    || riskRating.RiskRatingID == Convert.ToInt32(WebConstants.ALL));

            return oRiskRatingMstInfoCollection;
        }

        public static List<CapabilityMstInfo> GetAllCapabilities()
        {
            List<CapabilityMstInfo> oCapabilityMstInfoList = (List<CapabilityMstInfo>)HttpRuntime.Cache[CacheConstants.ALL_CAPABILITIES_LIST];
            if (oCapabilityMstInfoList == null)
            {
                try
                {
                    // Get from DB 
                    IUtility oUtilityClient = RemotingHelper.GetUtilityObject();
                    oCapabilityMstInfoList = oUtilityClient.GetAllCapabilities(Helper.GetAppUserInfo());
                    HttpRuntime.Cache.Add(CacheConstants.ALL_CAPABILITIES_LIST, oCapabilityMstInfoList, null, GetCacheExpirationTime(), Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);
                }
                catch (Exception ex)
                {
                    Helper.FormatAndShowErrorMessage(null, ex);
                }
            }
            return oCapabilityMstInfoList;
        }

        public static IList<ReconciliationFrequencyMstInfo> GetAllReconciliationFrequency()
        {
            IList<ReconciliationFrequencyMstInfo> lstReconciliationFrequencyMstInfo = (IList<ReconciliationFrequencyMstInfo>)HttpRuntime.Cache[CacheConstants.ALL_RECONCILIATIONFREQUENCY_LIST];
            if (lstReconciliationFrequencyMstInfo == null)
            {
                try
                {
                    lstReconciliationFrequencyMstInfo = new List<ReconciliationFrequencyMstInfo>();
                    // Get from DB 
                    IUtility oUtilityClient = RemotingHelper.GetUtilityObject();
                    lstReconciliationFrequencyMstInfo = oUtilityClient.GetAllReconciliationFrequencyMstInfo(Helper.GetAppUserInfo());
                    HttpRuntime.Cache.Add(CacheConstants.ALL_RECONCILIATIONFREQUENCY_LIST, lstReconciliationFrequencyMstInfo, null, GetCacheExpirationTime(), Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);
                }
                catch (Exception ex)
                {
                    Helper.FormatAndShowErrorMessage(null, ex);
                }
            }
            return lstReconciliationFrequencyMstInfo;
        }

        /// <summary>
        /// Selects all Account Type defined in the system
        /// </summary>
        /// <returns>List of all Account Type</returns>
        public static List<AccountTypeMstInfo> SelectAllAccountTypeMstInfo()
        {
            List<AccountTypeMstInfo> oAccountTypeMstInfoCollection = (List<AccountTypeMstInfo>)HttpRuntime.Cache[CacheConstants.ALL_ACCOUNT_TYPE_MST_INFO];

            if (oAccountTypeMstInfoCollection == null)
            {
                try
                {
                    IAccount oAccountClient = RemotingHelper.GetAccountObject();
                    oAccountTypeMstInfoCollection = oAccountClient.SelectAllAccountTypeMstInfo(Helper.GetAppUserInfo());
                    HttpRuntime.Cache.Add(CacheConstants.ALL_ACCOUNT_TYPE_MST_INFO, oAccountTypeMstInfoCollection, null, GetCacheExpirationTime(), Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);
                }
                catch (Exception ex)
                {
                    Helper.FormatAndShowErrorMessage(null, ex);
                }
            }

            oAccountTypeMstInfoCollection.RemoveAll(accType => accType.AccountTypeID == Convert.ToInt16(WebConstants.SELECT_ONE));

            return oAccountTypeMstInfoCollection;
        }

        /// <summary>
        /// Selects all Reconciliation Template defined in the system
        /// </summary>
        /// <returns>List of all Reconciliation Template</returns>
        public static List<ReconciliationTemplateMstInfo> SelectAllReconciliationTemplateMstInfo()
        {
            List<ReconciliationTemplateMstInfo> oReconciliationTemplateMstInfoCollection = (List<ReconciliationTemplateMstInfo>)HttpRuntime.Cache[CacheConstants.ALL_RECONCILIATION_TEMPLATE_MST_INFO];

            if (oReconciliationTemplateMstInfoCollection == null)
            {
                try
                {
                    IReconciliation oReconciliationClient = RemotingHelper.GetReconciliationObject();
                    oReconciliationTemplateMstInfoCollection = oReconciliationClient.SelectAllReconciliationTemplateMstInfo(Helper.GetAppUserInfo());
                    HttpRuntime.Cache.Add(CacheConstants.ALL_RECONCILIATION_TEMPLATE_MST_INFO, oReconciliationTemplateMstInfoCollection, null, GetCacheExpirationTime(), Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);
                }
                catch (Exception ex)
                {
                    Helper.FormatAndShowErrorMessage(null, ex);
                }
            }

            oReconciliationTemplateMstInfoCollection.RemoveAll(recTemplate => recTemplate.ReconciliationTemplateID == Convert.ToInt16(WebConstants.SELECT_ONE));

            return oReconciliationTemplateMstInfoCollection;
        }

        /// <summary>
        /// Selects all Risk Rating defined in the system
        /// </summary>
        /// <returns>List of all Risk Rating</returns>
        public static List<RiskRatingMstInfo> SelectAllRiskRatingMstInfo()
        {
            List<RiskRatingMstInfo> oRiskRatingMstInfoCollection = (List<RiskRatingMstInfo>)HttpRuntime.Cache[CacheConstants.ALL_RISK_RATING_MST_INFO];

            if (oRiskRatingMstInfoCollection == null)
            {
                try
                {
                    IRiskRating oRiskRatingClient = RemotingHelper.GetRiskRatingObject();
                    oRiskRatingMstInfoCollection = oRiskRatingClient.SelectAllRiskRatingMstInfo(Helper.GetAppUserInfo());
                    HttpRuntime.Cache.Add(CacheConstants.ALL_RISK_RATING_MST_INFO, oRiskRatingMstInfoCollection, null, GetCacheExpirationTime(), Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);
                }
                catch (Exception ex)
                {
                    Helper.FormatAndShowErrorMessage(null, ex);
                }
            }

            return oRiskRatingMstInfoCollection;
        }


        #endregion


        internal static List<GeographyClassMstInfo> GetAllOrganizationalHierarchyKeys(short? companyGeographyClassID)
        {
            List<GeographyClassMstInfo> oGeographyClassMstInfoCollection = (List<GeographyClassMstInfo>)HttpRuntime.Cache[CacheConstants.ORGANIZATIONAL_HIERARCHY_KEYS];
            if (oGeographyClassMstInfoCollection == null)
            {
                // Get from DB
                try
                {
                    IUtility oUtilityClient = RemotingHelper.GetUtilityObject();
                    oGeographyClassMstInfoCollection = oUtilityClient.GetAllOrganizationalHierarchyKeys(companyGeographyClassID, Helper.GetAppUserInfo());
                    HttpRuntime.Cache.Add(CacheConstants.ORGANIZATIONAL_HIERARCHY_KEYS, oGeographyClassMstInfoCollection, null, GetCacheExpirationTime(), Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);
                }
                catch (Exception ex)
                {
                    Helper.FormatAndShowErrorMessage(null, ex);
                }
            }
            return oGeographyClassMstInfoCollection;

        }


        /// <summary>
        /// Selects all Approvers for current company.
        /// </summary>
        /// <returns>List of Approvers defined for the company</returns>
        public static List<UserHdrInfo> SelectAllApproversForCurrentCompany()
        {
            List<UserHdrInfo> oUserHdrInfoCollection = (List<UserHdrInfo>)HttpRuntime.Cache[CacheHelper.GetCacheKeyForCompanyData(CacheConstants.ALL_APPROVERS_FOR_CURRENT_COMPANY)];
            if (oUserHdrInfoCollection == null)
            {
                try
                {
                    // Get from DB 
                    IUser oUserClient = RemotingHelper.GetUserObject();
                    oUserHdrInfoCollection = oUserClient.SelectAllUserHdrInfoByCompanyIDAndRoleID(SessionHelper.CurrentCompanyID.Value, (int)WebEnums.UserRole.APPROVER, Helper.GetAppUserInfo());
                    HttpRuntime.Cache.Add(CacheConstants.ALL_APPROVERS_FOR_CURRENT_COMPANY, oUserHdrInfoCollection, null, GetCacheExpirationTime(), Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);
                    oUserHdrInfoCollection = oUserClient.SelectAllUserHdrInfoByCompanyIDAndRoleID(SessionHelper.CurrentCompanyID.Value, (int)WebEnums.UserRole.APPROVER, Helper.GetAppUserInfo());
                    HttpRuntime.Cache.Add(CacheHelper.GetCacheKeyForCompanyData(CacheConstants.ALL_APPROVERS_FOR_CURRENT_COMPANY), oUserHdrInfoCollection, null, GetCacheExpirationTime(), Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);
                }
                catch (Exception ex)
                {
                    Helper.FormatAndShowErrorMessage(null, ex);
                }
            }

            oUserHdrInfoCollection.RemoveAll(user => user.UserID == Convert.ToInt32(WebConstants.SELECT_ONE));

            return oUserHdrInfoCollection;
        }

        /// <summary>
        /// Selects all Reviewers for current company.
        /// </summary>
        /// <returns>List of Reviewers defined for the company</returns>
        public static List<UserHdrInfo> SelectAllReviewersForCurrentCompany()
        {
            List<UserHdrInfo> oUserHdrInfoCollection = (List<UserHdrInfo>)HttpRuntime.Cache[CacheHelper.GetCacheKeyForCompanyData(CacheConstants.ALL_REVIEWERS_FOR_CURRENT_COMPANY)];
            if (oUserHdrInfoCollection == null)
            {
                try
                {
                    // Get from DB 
                    IUser oUserClient = RemotingHelper.GetUserObject();
                    oUserHdrInfoCollection = oUserClient.SelectAllUserHdrInfoByCompanyIDAndRoleID(SessionHelper.CurrentCompanyID.Value, (int)WebEnums.UserRole.REVIEWER, Helper.GetAppUserInfo());
                    HttpRuntime.Cache.Add(CacheHelper.GetCacheKeyForCompanyData(CacheConstants.ALL_REVIEWERS_FOR_CURRENT_COMPANY), oUserHdrInfoCollection, null, GetCacheExpirationTime(), Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);
                }
                catch (Exception ex)
                {
                    Helper.FormatAndShowErrorMessage(null, ex);
                }
            }

            oUserHdrInfoCollection.RemoveAll(user => user.UserID == Convert.ToInt32(WebConstants.SELECT_ONE));

            return oUserHdrInfoCollection;
        }


        /// <summary>
        /// Selects all Backup Approvers for current company.
        /// </summary>
        /// <returns>List of Backup Approvers defined for the company</returns>
        public static List<UserHdrInfo> SelectAllBackupApproversForCurrentCompany()
        {
            List<UserHdrInfo> oUserHdrInfoCollection = (List<UserHdrInfo>)HttpRuntime.Cache[CacheHelper.GetCacheKeyForCompanyData(CacheConstants.ALL_BACKUP_APPROVERS_FOR_CURRENT_COMPANY)];
            if (oUserHdrInfoCollection == null)
            {
                try
                {
                    // Get from DB 
                    IUser oUserClient = RemotingHelper.GetUserObject();
                    //oUserHdrInfoCollection = oUserClient.SelectAllUserHdrInfoByCompanyIDAndRoleID(SessionHelper.CurrentCompanyID.Value, (int)WebEnums.UserRole.BACKUP_APPROVER);
                    //HttpRuntime.Cache.Add(CacheConstants.ALL_BACKUP_APPROVERS_FOR_CURRENT_COMPANY, oUserHdrInfoCollection, null, GetCacheExpirationTime(), Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);
                    oUserHdrInfoCollection = oUserClient.SelectAllUserHdrInfoByCompanyIDAndRoleID(SessionHelper.CurrentCompanyID.Value, (int)WebEnums.UserRole.BACKUP_APPROVER, Helper.GetAppUserInfo());
                    HttpRuntime.Cache.Add(CacheHelper.GetCacheKeyForCompanyData(CacheConstants.ALL_BACKUP_APPROVERS_FOR_CURRENT_COMPANY), oUserHdrInfoCollection, null, GetCacheExpirationTime(), Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);
                }
                catch (Exception ex)
                {
                    Helper.FormatAndShowErrorMessage(null, ex);
                }
            }

            oUserHdrInfoCollection.RemoveAll(user => user.UserID == Convert.ToInt32(WebConstants.SELECT_ONE));

            return oUserHdrInfoCollection;
        }

        /// <summary>
        /// Selects all Backup Reviewers for current company.
        /// </summary>
        /// <returns>List of Backup Reviewers defined for the company</returns>
        public static List<UserHdrInfo> SelectAllBackupReviewersForCurrentCompany()
        {
            List<UserHdrInfo> oUserHdrInfoCollection = (List<UserHdrInfo>)HttpRuntime.Cache[CacheHelper.GetCacheKeyForCompanyData(CacheConstants.ALL_BACKUP_REVIEWERS_FOR_CURRENT_COMPANY)];
            if (oUserHdrInfoCollection == null)
            {
                try
                {
                    // Get from DB 
                    IUser oUserClient = RemotingHelper.GetUserObject();
                    oUserHdrInfoCollection = oUserClient.SelectAllUserHdrInfoByCompanyIDAndRoleID(SessionHelper.CurrentCompanyID.Value, (int)WebEnums.UserRole.BACKUP_REVIEWER, Helper.GetAppUserInfo());
                    HttpRuntime.Cache.Add(CacheHelper.GetCacheKeyForCompanyData(CacheConstants.ALL_BACKUP_REVIEWERS_FOR_CURRENT_COMPANY), oUserHdrInfoCollection, null, GetCacheExpirationTime(), Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);
                }
                catch (Exception ex)
                {
                    Helper.FormatAndShowErrorMessage(null, ex);
                }
            }

            oUserHdrInfoCollection.RemoveAll(user => user.UserID == Convert.ToInt32(WebConstants.SELECT_ONE));

            return oUserHdrInfoCollection;
        }

        public static List<UserHdrInfo> SelectAllBusinessAdminForCurrentCompany()
        {
            List<UserHdrInfo> oUserHdrInfoCollection = (List<UserHdrInfo>)HttpRuntime.Cache[CacheHelper.GetCacheKeyForCompanyData(CacheConstants.ALL_BUSINESSADMIN_FOR_CURRENT_COMPANY)];
            if (oUserHdrInfoCollection == null)
            {
                try
                {
                    // Get from DB 
                    IUser oUserClient = RemotingHelper.GetUserObject();
                    oUserHdrInfoCollection = oUserClient.SelectAllUserHdrInfoByCompanyIDAndRoleID(SessionHelper.CurrentCompanyID.Value, (int)WebEnums.UserRole.BUSINESS_ADMIN, Helper.GetAppUserInfo());
                    HttpRuntime.Cache.Add(CacheHelper.GetCacheKeyForCompanyData(CacheConstants.ALL_BUSINESSADMIN_FOR_CURRENT_COMPANY), oUserHdrInfoCollection, null, GetCacheExpirationTime(), Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);
                }
                catch (Exception ex)
                {
                    Helper.FormatAndShowErrorMessage(null, ex);
                }
            }

            oUserHdrInfoCollection.RemoveAll(user => user.UserID == Convert.ToInt32(WebConstants.SELECT_ONE));

            return oUserHdrInfoCollection;
        }




        /// <summary>
        /// Selects all Preparers for current company.
        /// </summary>
        /// <returns>List of Preparers defined for the company</returns>
        public static List<UserHdrInfo> SelectAllPreparersForCurrentCompany()
        {
            List<UserHdrInfo> oUserHdrInfoCollection = (List<UserHdrInfo>)HttpRuntime.Cache[CacheHelper.GetCacheKeyForCompanyData(CacheConstants.ALL_PREPARERS_FOR_CURRENT_COMPANY)];
            if (oUserHdrInfoCollection == null)
            {
                try
                {
                    // Get from DB 
                    IUser oUserClient = RemotingHelper.GetUserObject();
                    oUserHdrInfoCollection = oUserClient.SelectAllUserHdrInfoByCompanyIDAndRoleID(SessionHelper.CurrentCompanyID.Value, (int)WebEnums.UserRole.PREPARER, Helper.GetAppUserInfo());
                    HttpRuntime.Cache.Add(CacheHelper.GetCacheKeyForCompanyData(CacheConstants.ALL_PREPARERS_FOR_CURRENT_COMPANY), oUserHdrInfoCollection, null, GetCacheExpirationTime(), Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);
                }
                catch (Exception ex)
                {
                    Helper.FormatAndShowErrorMessage(null, ex);
                }
            }

            oUserHdrInfoCollection.RemoveAll(user => user.UserID == Convert.ToInt32(WebConstants.SELECT_ONE));

            return oUserHdrInfoCollection;
        }

        /// <summary>
        /// Selects all Preparers for current company.
        /// </summary>
        /// <returns>List of Preparers defined for the company</returns>
        public static List<UserHdrInfo> SelectAllBackupPreparersForCurrentCompany()
        {
            List<UserHdrInfo> oUserHdrInfoCollection = (List<UserHdrInfo>)HttpRuntime.Cache[CacheHelper.GetCacheKeyForCompanyData(CacheConstants.ALL_BACKUP_PREPARERS_FOR_CURRENT_COMPANY)];
            if (oUserHdrInfoCollection == null)
            {
                try
                {
                    // Get from DB 
                    IUser oUserClient = RemotingHelper.GetUserObject();
                    oUserHdrInfoCollection = oUserClient.SelectAllUserHdrInfoByCompanyIDAndRoleID(SessionHelper.CurrentCompanyID.Value, (int)WebEnums.UserRole.BACKUP_PREPARER, Helper.GetAppUserInfo());
                    HttpRuntime.Cache.Add(CacheHelper.GetCacheKeyForCompanyData(CacheConstants.ALL_BACKUP_PREPARERS_FOR_CURRENT_COMPANY), oUserHdrInfoCollection, null, GetCacheExpirationTime(), Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);
                }
                catch (Exception ex)
                {
                    Helper.FormatAndShowErrorMessage(null, ex);
                }
            }

            oUserHdrInfoCollection.RemoveAll(user => user.UserID == Convert.ToInt32(WebConstants.SELECT_ONE));

            return oUserHdrInfoCollection;
        }


        /// <summary>
        /// Clears cache for the given key
        /// </summary>
        /// <param name="key">cache key which needs to be cleared</param>
        public static void ClearCache(string key)
        {
            if (HttpRuntime.Cache[key] != null)
                HttpRuntime.Cache.Remove(key);
        }

        internal static List<ExceptionTypeMstInfo> GetAllExceptionTypes()
        {
            List<ExceptionTypeMstInfo> oExceptionTypeMstInfoCollection = (List<ExceptionTypeMstInfo>)HttpRuntime.Cache[CacheConstants.EXCEPTION_TYPES];
            if (oExceptionTypeMstInfoCollection == null)
            {
                // Get from DB
                try
                {
                    IUtility oUtilityClient = RemotingHelper.GetUtilityObject();
                    oExceptionTypeMstInfoCollection = oUtilityClient.GetAllExceptionTypes(Helper.GetAppUserInfo());
                    HttpRuntime.Cache.Add(CacheConstants.EXCEPTION_TYPES, oExceptionTypeMstInfoCollection, null, GetCacheExpirationTime(), Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);
                }
                catch (Exception ex)
                {
                    Helper.FormatAndShowErrorMessage(null, ex);
                }
            }
            return oExceptionTypeMstInfoCollection;
        }

        public static List<ReasonMstInfo> GetAllReasons()
        {
            string cacheKey = CacheHelper.GetCacheKeyForCompanyData(CacheConstants.ALL_REASONS);
            List<ReasonMstInfo> oReasonInfoCollection = (List<ReasonMstInfo>)HttpRuntime.Cache[cacheKey];
            if (oReasonInfoCollection == null)
            {
                try
                {
                    IReason oReasonClient = RemotingHelper.GetReasonObject();
                    oReasonInfoCollection = (List<ReasonMstInfo>)oReasonClient.GetAllReasons(Helper.GetAppUserInfo());
                    HttpRuntime.Cache.Add(cacheKey, oReasonInfoCollection, null, GetCacheExpirationTime(), Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);
                    //oReasonInfoCollection 
                }
                catch (Exception ex)
                {
                    Helper.FormatAndShowErrorMessage(null, ex);
                }
            }
            return oReasonInfoCollection;
        }

        public static List<SystemLockdownReasonMstInfo> GetAllSystemLockdownReasons()
        {
            string cacheKey = CacheHelper.GetCacheKeyForCompanyData(CacheConstants.ALL_SYSTEM_LOCKDOWN_REASONS);
            List<SystemLockdownReasonMstInfo> oSystemLockdownReasonMstInfoList = (List<SystemLockdownReasonMstInfo>)HttpRuntime.Cache[cacheKey];
            if (oSystemLockdownReasonMstInfoList == null)
            {
                try
                {
                    oSystemLockdownReasonMstInfoList = SystemLockdownHelper.GetAllSystemLockdownReasons();
                    HttpRuntime.Cache.Add(cacheKey, oSystemLockdownReasonMstInfoList, null, GetCacheExpirationTime(), Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);
                    //oReasonInfoCollection 
                }
                catch (Exception ex)
                {
                    Helper.FormatAndShowErrorMessage(null, ex);
                }
            }
            return oSystemLockdownReasonMstInfoList;
        }

        public static List<ReportParameterKeyMstInfo> GetAllReportParameterKeys()
        {
            string cacheKey = CacheHelper.GetCacheKeyForCompanyData(CacheConstants.ALL_REPORT_PARAMETER_KEY);
            List<ReportParameterKeyMstInfo> oRptParamKeyCollection = (List<ReportParameterKeyMstInfo>)HttpRuntime.Cache[cacheKey];
            if (oRptParamKeyCollection == null)
            {
                try
                {
                    IReportParameterKey oRptparamKeyClient = RemotingHelper.GetReportParameterKeyObject();
                    oRptParamKeyCollection = (List<ReportParameterKeyMstInfo>)oRptparamKeyClient.GetAllReportParameterKeys(Helper.GetAppUserInfo());
                    HttpRuntime.Cache.Add(cacheKey, oRptParamKeyCollection, null, GetCacheExpirationTime(), Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);

                }
                catch (Exception ex)
                {
                    Helper.FormatAndShowErrorMessage(null, ex);
                }
            }
            return oRptParamKeyCollection;
        }

        public static List<AgingCategoryMstInfo> GetAllAgingCategories()
        {
            string cacheKey = CacheHelper.GetCacheKeyForCompanyData(CacheConstants.ALL_AGINGCATEGORIES);
            List<AgingCategoryMstInfo> oAgingInfoCollection = (List<AgingCategoryMstInfo>)HttpRuntime.Cache[cacheKey];
            if (oAgingInfoCollection == null)
            {
                try
                {
                    IAging oAgingClient = RemotingHelper.GetAgingObject();
                    //oAgingInfoCollection = (List<AgingCategoryMstInfo>)oAgingClient.GetAllAgingCategories(languageID, defaultLanguageID);
                    oAgingInfoCollection = (List<AgingCategoryMstInfo>)oAgingClient.GetAllAgingCategories(Helper.GetAppUserInfo());
                    HttpRuntime.Cache.Add(cacheKey, oAgingInfoCollection, null, GetCacheExpirationTime(), Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);
                    //oReasonInfoCollection 
                }
                catch (Exception ex)
                {
                    Helper.FormatAndShowErrorMessage(null, ex);
                }
            }
            return oAgingInfoCollection;
        }

        public static List<RangeOfScoreMstInfo> GetRangeOfScores()
        {
            string cacheKey = CacheHelper.GetCacheKeyForCompanyData(CacheConstants.ALL_RANGEOFSCORECATEGORIES);
            List<RangeOfScoreMstInfo> oRangeInfoCollection = (List<RangeOfScoreMstInfo>)HttpRuntime.Cache[cacheKey];
            if (oRangeInfoCollection == null)
            {
                try
                {
                    IQualityScoreReports oRangeOfScoreClient = RemotingHelper.GetQualityScoreReportObject();
                    //oAgingInfoCollection = (List<AgingCategoryMstInfo>)oAgingClient.GetAllAgingCategories(languageID, defaultLanguageID);
                    oRangeInfoCollection = (List<RangeOfScoreMstInfo>)oRangeOfScoreClient.GetRangeOfScore(Helper.GetAppUserInfo());
                    HttpRuntime.Cache.Add(cacheKey, oRangeInfoCollection, null, GetCacheExpirationTime(), Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);
                    //oReasonInfoCollection 
                }
                catch (Exception ex)
                {
                    Helper.FormatAndShowErrorMessage(null, ex);
                }
            }
            return oRangeInfoCollection;
        }

        public static List<QualityScoreChecklistInfo> GetQualityScoreChecklist(int RecPeriodID)
        {
            // string cacheKey = CacheHelper.GetCacheKeyForCompanyData(CacheConstants.ALL_CHECKLISTCATEGORIES);
            List<QualityScoreChecklistInfo> oCheckListInfoCollection = new List<QualityScoreChecklistInfo>();
            // if (oCheckListInfoCollection == null)
            // {
            try
            {
                IQualityScoreReports oRangeOfScoreClient = RemotingHelper.GetQualityScoreReportObject();
                //oAgingInfoCollection = (List<AgingCategoryMstInfo>)oAgingClient.GetAllAgingCategories(languageID, defaultLanguageID);
                oCheckListInfoCollection = (List<QualityScoreChecklistInfo>)oRangeOfScoreClient.GetQualityScoreChecklist(RecPeriodID, Helper.GetAppUserInfo());
                //HttpRuntime.Cache.Add(cacheKey, oCheckListInfoCollection, null, GetCacheExpirationTime(), Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);
            }
            catch (Exception ex)
            {
                Helper.FormatAndShowErrorMessage(null, ex);
            }
            //}
            return oCheckListInfoCollection;
        }

        public static void ClearAllRecStatus()
        {
            HttpRuntime.Cache.Remove(CacheConstants.ALL_REC_STATUS);
        }

        public static List<ReconciliationStatusMstInfo> GetAllRecStatus()
        {
            Dictionary<int?, List<ReconciliationStatusMstInfo>> dicAllRecStatusMstInfo = null;
            List<ReconciliationStatusMstInfo> oRecStatusInfoCollection = null;
            IReconciliationStatus oRecStatus = null;
            dicAllRecStatusMstInfo = (Dictionary<int?, List<ReconciliationStatusMstInfo>>)HttpRuntime.Cache[CacheConstants.ALL_REC_STATUS];

            try
            {
                if (dicAllRecStatusMstInfo == null)
                {
                    oRecStatus = RemotingHelper.GetRecStatusObject();

                    oRecStatusInfoCollection = (List<ReconciliationStatusMstInfo>)oRecStatus.GetAllReconciliationStatus(SessionHelper.CurrentCompanyID, SessionHelper.CurrentReconciliationPeriodID, Helper.GetAppUserInfo());

                    dicAllRecStatusMstInfo = new Dictionary<int?, List<ReconciliationStatusMstInfo>>();

                    dicAllRecStatusMstInfo.Add(SessionHelper.CurrentCompanyID, oRecStatusInfoCollection);

                    HttpRuntime.Cache.Add(CacheConstants.ALL_REC_STATUS, dicAllRecStatusMstInfo, null, GetCacheExpirationTime(), Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);
                }
                else
                {
                    if (dicAllRecStatusMstInfo.ContainsKey(SessionHelper.CurrentCompanyID))
                    {
                        oRecStatusInfoCollection = (List<ReconciliationStatusMstInfo>)dicAllRecStatusMstInfo[SessionHelper.CurrentCompanyID];
                    }
                    else
                    {
                        oRecStatus = RemotingHelper.GetRecStatusObject();

                        oRecStatusInfoCollection = (List<ReconciliationStatusMstInfo>)oRecStatus.GetAllReconciliationStatus(SessionHelper.CurrentCompanyID, SessionHelper.CurrentReconciliationPeriodID, Helper.GetAppUserInfo());

                        dicAllRecStatusMstInfo.Add(SessionHelper.CurrentCompanyID, oRecStatusInfoCollection);

                        HttpRuntime.Cache.Add(CacheConstants.ALL_REC_STATUS, dicAllRecStatusMstInfo, null, GetCacheExpirationTime(), Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);
                    }
                }
            }
            catch (Exception ex)
            {
                Helper.FormatAndShowErrorMessage(null, ex);
            }
            return oRecStatusInfoCollection;

        }

        public static List<CertificationStatusMstInfo> GetCertificationStatus()
        {

            string cacheKey = CacheHelper.GetCacheKeyForCompanyData(CacheConstants.ALL_CERTIFICATION_STATUS_TYPE);
            List<CertificationStatusMstInfo> oCertStatusMstInfoCollection = (List<CertificationStatusMstInfo>)HttpRuntime.Cache[cacheKey];
            if (oCertStatusMstInfoCollection == null)
            {
                try
                {
                    ICertificationStatus oCertStatus = RemotingHelper.GetCertificationStatusObject();
                    oCertStatusMstInfoCollection = (List<CertificationStatusMstInfo>)oCertStatus.GetAllCertificationStatus(Helper.GetAppUserInfo());
                    HttpRuntime.Cache.Add(cacheKey, oCertStatusMstInfoCollection, null, GetCacheExpirationTime(), Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);
                }
                catch (Exception ex)
                {
                    Helper.FormatAndShowErrorMessage(null, ex);
                }
            }
            return oCertStatusMstInfoCollection;
        }

        public static List<QualityScoreStatusMstInfo> GetAllQualityScoreStatuses()
        {

            string cacheKey = CacheHelper.GetCacheKeyForCompanyData(CacheConstants.ALL_QUALITY_SCORE_STATUS_TYPE);
            List<QualityScoreStatusMstInfo> oQualityScoreStatusMstInfoList = (List<QualityScoreStatusMstInfo>)HttpRuntime.Cache[cacheKey];
            if (oQualityScoreStatusMstInfoList == null)
            {
                try
                {
                    IQualityScore oQualityScore = RemotingHelper.GetQualityScoreObject();
                    oQualityScoreStatusMstInfoList = (List<QualityScoreStatusMstInfo>)oQualityScore.GetAllQualityScoreStatuses(Helper.GetAppUserInfo());
                    HttpRuntime.Cache.Add(cacheKey, oQualityScoreStatusMstInfoList, null, GetCacheExpirationTime(), Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);
                }
                catch (Exception ex)
                {
                    Helper.FormatAndShowErrorMessage(null, ex);
                }
            }
            return oQualityScoreStatusMstInfoList;
        }

        public static List<ReconciliationPeriodInfo> GetAllReconciliationPeriods(int? currentFinancialYearID)
        {
            string cacheKey = CacheHelper.GetCacheKeyForCompanyData(CacheConstants.ALL_RECONCILIATION_PERIODS_BASED_ON_FY);
            List<ReconciliationPeriodInfo> oReconciliationPeriodInfoCollection = (List<ReconciliationPeriodInfo>)HttpRuntime.Cache[cacheKey];
            if ((oReconciliationPeriodInfoCollection == null || oReconciliationPeriodInfoCollection.Count == 0) || !(oReconciliationPeriodInfoCollection[0].ReconciliationPeriodID.HasValue && oReconciliationPeriodInfoCollection[0].ReconciliationPeriodID.Value > 0))
            {
                // Get from DB
                try
                {
                    // TODO: In future, we might have to Check for Financial year first and then pick Rec Periods
                    ICompany oCompanyClient = RemotingHelper.GetCompanyObject();
                    oReconciliationPeriodInfoCollection = (List<ReconciliationPeriodInfo>)oCompanyClient.SelectAllReconciliationPeriodByCompanyID(SessionHelper.CurrentCompanyID.Value, null, Helper.GetAppUserInfo());

                    if (oReconciliationPeriodInfoCollection != null && oReconciliationPeriodInfoCollection.Count > 0)
                        HttpRuntime.Cache.Add(cacheKey, oReconciliationPeriodInfoCollection, null, GetCacheExpirationTime(), Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);
                }
                catch (Exception ex)
                {
                    Helper.FormatAndShowErrorMessage(null, ex);
                }
            }
            if (currentFinancialYearID.HasValue)
                return oReconciliationPeriodInfoCollection.FindAll(T => T.FinancialYearID == currentFinancialYearID.Value);
            return oReconciliationPeriodInfoCollection;
        }

        public static List<ReconciliationPeriodInfo> GetAllReconciliationPeriodsForAuditRole(int? currentFinancialYearID)
        {
            List<ReconciliationPeriodInfo> oReconciliationPeriodInfoCollection = GetAllReconciliationPeriods(currentFinancialYearID);
            oReconciliationPeriodInfoCollection = FilterRecPeriodsForAuditRole(oReconciliationPeriodInfoCollection);
            return oReconciliationPeriodInfoCollection;
        }

        private static List<ReconciliationPeriodInfo> FilterRecPeriodsForAuditRole(List<ReconciliationPeriodInfo> oRecPeriodInfo)
        {
            List<CompanyAttributeConfigInfo> oCompanyAttributeConfigInfoList = AttributeConfigHelper.GetCompanyAttributeConfigInfoList(false, WebEnums.AttributeSetType.RoleConfig);
            if (oCompanyAttributeConfigInfoList != null)
                oCompanyAttributeConfigInfoList.AddRange(AttributeConfigHelper.GetCompanyAttributeConfigInfoList(false, WebEnums.AttributeSetType.AuditRoleFilter));

            bool isAllAccountSelectedInOpenPeriod = false;
            bool isAllAccountSelectedInClosedPeriod = false;
            bool isViewPeriodOnlyInRange = false;
            DateTime FromPeriodEndDate = DateTime.MinValue;
            DateTime ToPeriodEndDate = DateTime.MaxValue;
            List<ReconciliationPeriodInfo> oAllReconciliationPeriodInfoList = CacheHelper.GetAllReconciliationPeriods(null);

            if (oCompanyAttributeConfigInfoList != null && oCompanyAttributeConfigInfoList.Count > 0)
            {
                foreach (CompanyAttributeConfigInfo oCompanyAttributeConfigInfo in oCompanyAttributeConfigInfoList)
                {
                    switch ((ARTEnums.AttributeList)oCompanyAttributeConfigInfo.AttributeID)
                    {
                        case ARTEnums.AttributeList.ReconciledAccountsInOpenPeriod:
                            isAllAccountSelectedInOpenPeriod = oCompanyAttributeConfigInfo.IsEnabled.GetValueOrDefault();
                            break;
                        case ARTEnums.AttributeList.ReconciledAccountsInClosedPeriod:
                            isAllAccountSelectedInClosedPeriod = oCompanyAttributeConfigInfo.IsEnabled.GetValueOrDefault();
                            break;
                        case ARTEnums.AttributeList.ViewPeriodsOnlyInRange:
                            isViewPeriodOnlyInRange = oCompanyAttributeConfigInfo.IsEnabled.GetValueOrDefault();
                            break;
                        case ARTEnums.AttributeList.ViewPeriodsInRangeFrom:
                            if (oCompanyAttributeConfigInfo.ReferenceID.HasValue)
                            {
                                ReconciliationPeriodInfo oFromRecPeriod = oAllReconciliationPeriodInfoList.FirstOrDefault(T => T.ReconciliationPeriodID == oCompanyAttributeConfigInfo.ReferenceID);
                                if (oFromRecPeriod != null)
                                    FromPeriodEndDate = oFromRecPeriod.PeriodEndDate.GetValueOrDefault();
                            }
                            break;
                        case ARTEnums.AttributeList.ViewPeriodsInRangeTo:
                            if (oCompanyAttributeConfigInfo.ReferenceID.HasValue)
                            {
                                ReconciliationPeriodInfo oToRecPeriod = oAllReconciliationPeriodInfoList.FirstOrDefault(T => T.ReconciliationPeriodID == oCompanyAttributeConfigInfo.ReferenceID);
                                if (oToRecPeriod != null)
                                    ToPeriodEndDate = oToRecPeriod.PeriodEndDate.GetValueOrDefault();
                            }
                            break;
                    }
                }
            }
            // Remove periods which are not in allowed range
            if (isViewPeriodOnlyInRange)
            {
                oRecPeriodInfo = oRecPeriodInfo.FindAll(oRecPeriod => (oRecPeriod.PeriodEndDate >= FromPeriodEndDate && oRecPeriod.PeriodEndDate <= ToPeriodEndDate));
            }

            if (isAllAccountSelectedInOpenPeriod == true && isAllAccountSelectedInClosedPeriod == false)
            {
                oRecPeriodInfo = oRecPeriodInfo.FindAll(oRecPeriod => (oRecPeriod.ReconciliationPeriodStatusID == (short)WebEnums.RecPeriodStatus.Open || oRecPeriod.ReconciliationPeriodStatusID == (short)WebEnums.RecPeriodStatus.InProgress));
            }
            else if (isAllAccountSelectedInOpenPeriod == false && isAllAccountSelectedInClosedPeriod == true)
            {
                oRecPeriodInfo = oRecPeriodInfo.FindAll(oRecPeriod => (oRecPeriod.ReconciliationPeriodStatusID == (short)WebEnums.RecPeriodStatus.Closed));
            }
            else if (isAllAccountSelectedInOpenPeriod == true && isAllAccountSelectedInClosedPeriod == true)
            {
                oRecPeriodInfo = oRecPeriodInfo.FindAll(oRecPeriod => (oRecPeriod.ReconciliationPeriodStatusID == (short)WebEnums.RecPeriodStatus.Closed ||
                    oRecPeriod.ReconciliationPeriodStatusID == (short)WebEnums.RecPeriodStatus.Open || oRecPeriod.ReconciliationPeriodStatusID == (short)WebEnums.RecPeriodStatus.InProgress ||
                    oRecPeriod.ReconciliationPeriodStatusID == (short)WebEnums.RecPeriodStatus.Skipped));
            }

            return oRecPeriodInfo;
        }

        public static List<ReconciliationCategoryMstInfo> GetOpenItemClassifications()
        {
            string cacheKey = CacheHelper.GetCacheKeyForCompanyData(CacheConstants.ALL_OPEN_ITEM_CLASSIFICATIONS);
            List<ReconciliationCategoryMstInfo> oRecCategoryInfoCollection = (List<ReconciliationCategoryMstInfo>)HttpRuntime.Cache[cacheKey];
            if (oRecCategoryInfoCollection == null)
            {
                try
                {
                    IReconciliationCategory oRecCategory = RemotingHelper.GetReconciliationCategoryObject();
                    oRecCategoryInfoCollection = (List<ReconciliationCategoryMstInfo>)oRecCategory.GetOpenItemClassification(Helper.GetAppUserInfo());
                    HttpRuntime.Cache.Add(cacheKey, oRecCategoryInfoCollection, null, GetCacheExpirationTime(), Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);
                }
                catch (Exception ex)
                {
                    Helper.FormatAndShowErrorMessage(null, ex);
                }
            }
            return oRecCategoryInfoCollection;
        }

        internal static List<ReconciliationPeriodStatusMstInfo> GetAllRecPeriodStatuses()
        {
            List<ReconciliationPeriodStatusMstInfo> oReconciliationPeriodStatusMstInfoCollection = (List<ReconciliationPeriodStatusMstInfo>)HttpRuntime.Cache[CacheConstants.ALL_REC_PERIOD_STATUSES];
            if (oReconciliationPeriodStatusMstInfoCollection == null)
            {
                // Get from DB
                try
                {
                    IUtility oUtilityClient = RemotingHelper.GetUtilityObject();
                    oReconciliationPeriodStatusMstInfoCollection = oUtilityClient.GetAllRecPeriodStatuses(Helper.GetAppUserInfo());
                    HttpRuntime.Cache.Add(CacheConstants.ALL_REC_PERIOD_STATUSES, oReconciliationPeriodStatusMstInfoCollection, null, GetCacheExpirationTime(), Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);
                }
                catch (Exception ex)
                {
                    Helper.FormatAndShowErrorMessage(null, ex);
                }
            }
            return oReconciliationPeriodStatusMstInfoCollection;
        }


        public static void ClearRecPeriodDataFromCache()
        {
            if (SessionHelper.CurrentCompanyID != null)
            {
                CacheHelper.ClearCache(CacheHelper.GetCacheKeyForCompanyData(CacheConstants.EXCHANGE_RATE_FROM_BASE_CURRENCY_TO_REPORTING_CURRENCY));
                CacheHelper.ClearCache(CacheHelper.GetCacheKeyForCompanyData(CacheConstants.EXCHANGE_RATE_FROM_REPORTING_CURRENCY_TO_BASE_CURRENCY));
                CacheHelper.ClearCache(CacheHelper.GetCacheKeyForCompanyData(CacheConstants.ALL_RECONCILIATION_PERIODS_BASED_ON_FY));
            }
        }

        public static void ClearCompanyDataFromCache()
        {
            if (SessionHelper.CurrentCompanyID != null)
            {
                CacheHelper.ClearCache(CacheHelper.GetCacheKeyForCompanyData(CacheConstants.ALL_FINANCIAL_YEAR_LIST));
                CacheHelper.ClearCache(CacheHelper.GetCacheKeyForCompanyData(CacheConstants.ALL_RECONCILIATION_PERIODS_BASED_ON_FY));
                CacheHelper.ClearRecPeriodDataFromCache();
            }
        }

        public static List<CompanyAlertInfo> SelectComapnyAlertByCompanyIDAndRecPeriodID()
        {
            string cacheKey = CacheHelper.GetCacheKeyForCompanyData(CacheConstants.ALL_COMPANY_ALERTS);
            List<CompanyAlertInfo> oCompanyAlertInfoCollection = (List<CompanyAlertInfo>)HttpRuntime.Cache[cacheKey];

            if (oCompanyAlertInfoCollection == null || oCompanyAlertInfoCollection.Count == 0)
            {
                IAlert oAlertClient = RemotingHelper.GetAlertObject();
                oCompanyAlertInfoCollection = oAlertClient.SelectComapnyAlertByCompanyIDAndRecPeriodID(SessionHelper.CurrentCompanyID.Value, SessionHelper.CurrentReconciliationPeriodID, Helper.GetAppUserInfo());
                HttpRuntime.Cache.Add(cacheKey, oCompanyAlertInfoCollection, null, CacheHelper.GetCacheExpirationTime(), Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);
            }

            return oCompanyAlertInfoCollection;
        }

        /// <summary>
        /// Getting All Financial Year list from cache/DB.
        /// </summary>
        /// <returns></returns>
        public static List<FinancialYearHdrInfo> GetAllFinancialYears()
        {
            string cacheKey = CacheHelper.GetCacheKeyForCompanyData(CacheConstants.ALL_FINANCIAL_YEAR_LIST);
            List<FinancialYearHdrInfo> oFinancialYearHdrInfoCollection = (List<FinancialYearHdrInfo>)HttpRuntime.Cache[cacheKey];

            if (oFinancialYearHdrInfoCollection == null)
            {
                ICompany oCompanyClient = RemotingHelper.GetCompanyObject();
                oFinancialYearHdrInfoCollection = oCompanyClient.GetAllFinancialYearList(SessionHelper.CurrentCompanyID, Helper.GetAppUserInfo());
                HttpRuntime.Cache.Add(cacheKey, oFinancialYearHdrInfoCollection, null, CacheHelper.GetCacheExpirationTime(), Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);
            }

            return oFinancialYearHdrInfoCollection;

        }


        public static IList<WeekDayMstInfo> GetAllWeekDays()
        {
            IList<WeekDayMstInfo> oWeekDayMstInfo = (IList<WeekDayMstInfo>)HttpRuntime.Cache[CacheConstants.ALL_WEEKDAYS_LIST];
            if (oWeekDayMstInfo == null)
            {
                try
                {
                    // Get from DB 
                    IWeekDay oWorkweek = RemotingHelper.GetWorkweekObject();
                    oWeekDayMstInfo = oWorkweek.GetAllWeekDays(Helper.GetAppUserInfo());
                    HttpRuntime.Cache.Add(CacheConstants.ALL_WEEKDAYS_LIST, oWeekDayMstInfo, null, GetCacheExpirationTime(), Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);
                }
                catch (Exception ex)
                {
                    Helper.FormatAndShowErrorMessage(null, ex);
                }
            }
            return oWeekDayMstInfo;
        }

        public static IList<MappingUploadMasterInfo> GetAllMappingUploadKeys()
        {
            IList<MappingUploadMasterInfo> oMappingUploadMstInfo = (IList<MappingUploadMasterInfo>)HttpRuntime.Cache[CacheConstants.ALL_WEEKDAYS_LIST];
            if (oMappingUploadMstInfo == null)
            {
                try
                {
                    // Get from DB 
                    IMappingUpload oMappingUpload = RemotingHelper.GetMappingUploadObject();
                    oMappingUploadMstInfo = oMappingUpload.GetAllMappingUploadInfoList(Helper.GetAppUserInfo());
                    HttpRuntime.Cache.Add(CacheConstants.ALL_MAPPINGUPLOADKEY_LIST, oMappingUploadMstInfo, null, GetCacheExpirationTime(), Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);
                }
                catch (Exception ex)
                {
                    Helper.FormatAndShowErrorMessage(null, ex);
                }
            }
            return oMappingUploadMstInfo;
        }

        #region Package

        public static List<PackageMstInfo> GetAllPackage()
        {
            List<PackageMstInfo> oPackageMstInfoCollection = (List<PackageMstInfo>)HttpRuntime.Cache[CacheConstants.ALL_PACKAGE_LIST];
            if (oPackageMstInfoCollection == null)
            {
                //DAO Call
                IPackage oPackageClient = RemotingHelper.GetPackageObject();
                oPackageMstInfoCollection = oPackageClient.GetAllPackage(Helper.GetAppUserInfo());

                // add to cache
                HttpRuntime.Cache.Add(CacheConstants.ALL_PACKAGE_LIST, oPackageMstInfoCollection, null, GetCacheExpirationTime(), Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);
            }
            return oPackageMstInfoCollection;
        }

        #endregion

        public static void ClearFeaturesByCompanyID(int? companyID)
        {
            Dictionary<Int32, Dictionary<Int32, Dictionary<WebEnums.Feature, Boolean?>>> dictAllCompanyFeatures = null;
            dictAllCompanyFeatures = (Dictionary<Int32, Dictionary<Int32, Dictionary<WebEnums.Feature, Boolean?>>>)HttpRuntime.Cache[CacheConstants.COMPANY_FEATURES];
            if (dictAllCompanyFeatures != null && dictAllCompanyFeatures.Count > 0)
            {
                if (dictAllCompanyFeatures.ContainsKey(Convert.ToInt32(companyID)))
                {
                    dictAllCompanyFeatures.Remove(Convert.ToInt32(companyID));
                }
            }
        }

        internal static Dictionary<WebEnums.Feature, bool?> GetCompanyFeatures(int? CompanyID, int? recPeriodID)
        {

            // Dictionary to Store Rec Period wise Features of a company
            Dictionary<WebEnums.Feature, Boolean?> dictRecPeriodFeatures = null;
            // Dictionary to Store All Dictionaries of Rec Period Wise features of a company
            Dictionary<Int32, Dictionary<WebEnums.Feature, Boolean?>> dictCompanyFeatures = null;
            // Dictionary to Store All Dictionaries of Company Features
            Dictionary<Int32, Dictionary<Int32, Dictionary<WebEnums.Feature, Boolean?>>> dictAllCompanyFeatures = null;
            dictAllCompanyFeatures = (Dictionary<Int32, Dictionary<Int32, Dictionary<WebEnums.Feature, Boolean?>>>)HttpRuntime.Cache[CacheConstants.COMPANY_FEATURES];
            //
            if (dictAllCompanyFeatures == null)
            {
                // Create the All Company Feature Dictionary
                dictAllCompanyFeatures = new Dictionary<Int32, Dictionary<Int32, Dictionary<WebEnums.Feature, Boolean?>>>();
            }
            // Check Company Dictionary Exists
            if (dictAllCompanyFeatures.ContainsKey(CompanyID.Value))
            {
                dictCompanyFeatures = dictAllCompanyFeatures[CompanyID.Value];
            }
            else
            {
                // Create and add new company dictionary
                dictCompanyFeatures = new Dictionary<int, Dictionary<WebEnums.Feature, bool?>>();
                dictAllCompanyFeatures.Add(CompanyID.Value, dictCompanyFeatures);
            }
            // Check Rec Period wise feature dictionary exists or not
            if (recPeriodID.HasValue)
            {
                if (dictCompanyFeatures.ContainsKey(recPeriodID.Value))
                    dictRecPeriodFeatures = dictCompanyFeatures[recPeriodID.Value];
                else
                {
                    // Get the Company Features for a Rec Period from DB
                    ICompany oCompanyClient = RemotingHelper.GetCompanyObject();
                    List<FeatureMstInfo> oFeatureMstInfoCollection = oCompanyClient.GetFeaturesByCompanyID(CompanyID, recPeriodID, Helper.GetAppUserInfo());

                    // Loop Thru and create dictionary
                    dictRecPeriodFeatures = new Dictionary<WebEnums.Feature, bool?>();
                    for (int i = 0; i < oFeatureMstInfoCollection.Count; i++)
                    {
                        WebEnums.Feature eFeature = (WebEnums.Feature)System.Enum.Parse(typeof(WebEnums.Feature), oFeatureMstInfoCollection[i].FeatureID.Value.ToString());
                        dictRecPeriodFeatures.Add(eFeature, true);
                    }

                    // Add to the Company Features Dictionary
                    dictCompanyFeatures.Add(recPeriodID.Value, dictRecPeriodFeatures);

                    // Add to Cache
                    HttpRuntime.Cache.Add(CacheConstants.COMPANY_FEATURES, dictAllCompanyFeatures, null, GetCacheExpirationTime(), Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);
                }
            }
            return dictRecPeriodFeatures;
        }

        public static decimal? GetExchangeRate(string fromCurrencyCode, string toCurrecyCode, int? recPeriodID)
        {
            decimal exchangeRate = 0M;

            if (recPeriodID == null)
                return null;

            // this will happen in case of Net Account
            if (string.IsNullOrEmpty(fromCurrencyCode)
                || string.IsNullOrEmpty(toCurrecyCode))
                return null;

            // if Codes are same
            if (fromCurrencyCode == toCurrecyCode)
            {
                exchangeRate = 1M;
                return exchangeRate;
            }

            // Create a Dict
            Dictionary<int, Dictionary<string, decimal>> oExchangeRateCollection = null;
            Dictionary<string, decimal> oExchangeRateCollectionByRecPeriod = null;

            oExchangeRateCollection = (Dictionary<int, Dictionary<string, decimal>>)HttpRuntime.Cache[CacheConstants.EXCHANGE_RATE];

            if (oExchangeRateCollection == null)
            {
                // means object not available in Cache, so create new and add to cache
                oExchangeRateCollection = new Dictionary<int, Dictionary<string, decimal>>();
                HttpRuntime.Cache.Add(CacheConstants.EXCHANGE_RATE, oExchangeRateCollection, null, CacheHelper.GetCacheExpirationTime(), Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);
            }

            if (oExchangeRateCollection.ContainsKey(recPeriodID.Value))
            {
                oExchangeRateCollectionByRecPeriod = oExchangeRateCollection[recPeriodID.Value];
            }

            if (oExchangeRateCollectionByRecPeriod == null)
            {
                // Means Rec Period not available in Dict in Cache, so create a new object and add to Dict
                oExchangeRateCollectionByRecPeriod = new Dictionary<string, decimal>();
                oExchangeRateCollection.Add(recPeriodID.Value, oExchangeRateCollectionByRecPeriod);
            }

            string dictKey = fromCurrencyCode + WebConstants.HYPHEN + toCurrecyCode;

            if (oExchangeRateCollectionByRecPeriod.ContainsKey(dictKey))
            {
                exchangeRate = oExchangeRateCollectionByRecPeriod[dictKey];
            }
            else
            {
                // get from DB
                IUtility oUtilityClient = RemotingHelper.GetUtilityObject();
                exchangeRate = oUtilityClient.GetCurrencyExchangeRate(recPeriodID.Value, fromCurrencyCode, toCurrecyCode, true, Helper.GetAppUserInfo());

                // add to dict
                oExchangeRateCollectionByRecPeriod.Add(dictKey, exchangeRate);
            }

            return exchangeRate;
        }

        public static void ClearExchangeRateByRecPeriodID(int recPeriodID)
        {
            Dictionary<int, Dictionary<string, decimal>> oExchangeRateCollection = null;

            oExchangeRateCollection = (Dictionary<int, Dictionary<string, decimal>>)HttpRuntime.Cache[CacheConstants.EXCHANGE_RATE];

            if (oExchangeRateCollection != null
                && oExchangeRateCollection.ContainsKey(recPeriodID))
            {
                oExchangeRateCollection.Remove(recPeriodID);
            }
        }




        #region "Matching Source Types"
        /// <summary>
        /// Gets all Matching Source Types
        /// </summary>
        /// <returns>list of Matching Source Type Info object</returns>
        public static List<MatchingSourceTypeInfo> GetMatchingSourceType()
        {
            List<SkyStem.ART.Client.Model.Matching.MatchingSourceTypeInfo> lstMatchingSourceTypeInfoCollection = (List<MatchingSourceTypeInfo>)HttpRuntime.Cache[CacheConstants.MATCH_SOURCE_TYPE];
            if (lstMatchingSourceTypeInfoCollection == null)
            {
                //get from db
                IMatching oMatchingClient = RemotingHelper.GetMatchingObject();
                lstMatchingSourceTypeInfoCollection = oMatchingClient.GetAllMatchingSourceType(Helper.GetAppUserInfo());
                HttpRuntime.Cache.Add(CacheConstants.MATCH_SOURCE_TYPE, lstMatchingSourceTypeInfoCollection, null, GetCacheExpirationTime(), Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);
            }
            return lstMatchingSourceTypeInfoCollection;
        }
        #endregion

        #region "Data Type"
        /// <summary>
        /// Get all Data Types
        /// </summary>
        /// <returns>list of Data Type Mst Info object</returns>
        public static List<DataTypeMstInfo> GetAllDataType()
        {
            List<DataTypeMstInfo> lstDataTypeMstInfoCollection = (List<DataTypeMstInfo>)HttpRuntime.Cache[CacheConstants.ALL_DATA_TYPE];
            if (lstDataTypeMstInfoCollection == null)
            {
                //get from db
                IMatching oMatchingClient = RemotingHelper.GetMatchingObject();
                lstDataTypeMstInfoCollection = oMatchingClient.GetAllDataType(Helper.GetAppUserInfo());
                HttpRuntime.Cache.Add(CacheConstants.ALL_DATA_TYPE, lstDataTypeMstInfoCollection, null, GetCacheExpirationTime(), Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);
            }
            return lstDataTypeMstInfoCollection;
        }
        #endregion


        #region "Dependent Steps"
        /// <summary>
        /// Get all Dependent Steps
        /// </summary>
        /// <returns>list of WizardStepDependencyInfo</returns>
        public static List<WizardStepDependencyInfo> GetDependentSteps(int? WizardStepID)
        {
            List<WizardStepDependencyInfo> oWizardStepDependencyInfoCollection = null;
            List<WizardStepDependencyInfo> oAllWizardStepDependencyInfoCollection = (List<WizardStepDependencyInfo>)HttpRuntime.Cache[CacheConstants.ALL_DEPENDENT_STEPS];
            if (oAllWizardStepDependencyInfoCollection == null)
            {
                //get from db
                IWizard oWizardClient = RemotingHelper.GetWizardObject();
                oAllWizardStepDependencyInfoCollection = oWizardClient.GetAllDependentSteps(Helper.GetAppUserInfo());
                HttpRuntime.Cache.Add(CacheConstants.ALL_DEPENDENT_STEPS, oAllWizardStepDependencyInfoCollection, null, GetCacheExpirationTime(), Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);
            }

            oWizardStepDependencyInfoCollection = (from o in oAllWizardStepDependencyInfoCollection
                                                   where o.WizardStepID == WizardStepID
                                                   select o).ToList();

            return oWizardStepDependencyInfoCollection;
        }
        #endregion


        /// <summary>
        /// Selects all Users for current company.
        /// </summary>
        /// <returns>List of Users defined for the company</returns>
        public static List<UserHdrInfo> SelectAllUsersForCurrentCompany()
        {
            List<UserHdrInfo> oUserHdrInfoCollection = (List<UserHdrInfo>)HttpRuntime.Cache[CacheHelper.GetCacheKeyForCompanyData(CacheConstants.ALL_USERS_FOR_CURRENT_COMPANY)];
            if (oUserHdrInfoCollection == null)
            {
                try
                {
                    // Get from DB 
                    IUser oUserClient = RemotingHelper.GetUserObject();
                    oUserHdrInfoCollection = oUserClient.SelectAllUserHdrInfoByCompanyIDAndRoleID(SessionHelper.CurrentCompanyID.Value, (int)WebEnums.UserRole.None, Helper.GetAppUserInfo());
                    HttpRuntime.Cache.Add(CacheHelper.GetCacheKeyForCompanyData(CacheConstants.ALL_USERS_FOR_CURRENT_COMPANY), oUserHdrInfoCollection, null, GetCacheExpirationTime(), Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);
                }
                catch (Exception ex)
                {
                    Helper.FormatAndShowErrorMessage(null, ex);
                }
            }
            return oUserHdrInfoCollection;
        }

        #region "Task Master"
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static List<TaskActionTypeTaskSatusInfo> GetTaskStatusByTaskActionTypeID(short ActionTypeID)
        {
            string _cacheKey = CacheConstants.TASK_STATUS_IDS_BY_TASK_ACTION_TYPE_ID + "_" + ActionTypeID.ToString();
            List<TaskActionTypeTaskSatusInfo> TaskActionTypeTaskSatusInfoList = (List<TaskActionTypeTaskSatusInfo>)HttpRuntime.Cache[_cacheKey];
            if (TaskActionTypeTaskSatusInfoList == null)
            {
                // Get from DB
                try
                {
                    ITaskMaster oTaskMaster = RemotingHelper.GetTaskMasterObject();
                    TaskActionTypeTaskSatusInfoList = oTaskMaster.GetTaskStatusByTaskActionTypeID(ActionTypeID, Helper.GetAppUserInfo());
                    HttpRuntime.Cache.Add(_cacheKey, TaskActionTypeTaskSatusInfoList, null, GetCacheExpirationTime(), Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);
                }
                catch (Exception ex)
                {
                    Helper.FormatAndShowErrorMessage(null, ex);
                }
            }
            return TaskActionTypeTaskSatusInfoList;
        }

        //get all active task category
        public static List<TaskCategoryMstInfo> GetTaskCategory()
        {
            string cacheKey = CacheHelper.GetCacheKeyForCompanyData(CacheConstants.TASK_CATEGORY);
            List<TaskCategoryMstInfo> oTaskCategoryMstInfoList = (List<TaskCategoryMstInfo>)HttpRuntime.Cache[cacheKey];
            if (oTaskCategoryMstInfoList == null)
            {
                try
                {
                    ITaskMaster oTaskClient = RemotingHelper.GetTaskMasterObject();
                    oTaskCategoryMstInfoList = (List<TaskCategoryMstInfo>)oTaskClient.GetTaskCategory(Helper.GetAppUserInfo());
                    HttpRuntime.Cache.Add(cacheKey, oTaskCategoryMstInfoList, null, GetCacheExpirationTime(), Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);
                    //oReasonInfoCollection 
                }
                catch (Exception ex)
                {
                    Helper.FormatAndShowErrorMessage(null, ex);
                }
            }
            return oTaskCategoryMstInfoList;
        }

        //get all active task status
        public static List<TaskStatusMstInfo> GetTaskStatus()
        {
            string cacheKey = CacheHelper.GetCacheKeyForCompanyData(CacheConstants.TASK_STATUS);
            List<TaskStatusMstInfo> oTaskStatusMstInfoList = (List<TaskStatusMstInfo>)HttpRuntime.Cache[cacheKey];
            if (oTaskStatusMstInfoList == null)
            {
                try
                {
                    ITaskMaster oTaskClient = RemotingHelper.GetTaskMasterObject();
                    oTaskStatusMstInfoList = (List<TaskStatusMstInfo>)oTaskClient.GetTaskStatus(Helper.GetAppUserInfo());
                    HttpRuntime.Cache.Add(cacheKey, oTaskStatusMstInfoList, null, GetCacheExpirationTime(), Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);
                    //oReasonInfoCollection 
                }
                catch (Exception ex)
                {
                    Helper.FormatAndShowErrorMessage(null, ex);
                }
            }
            return oTaskStatusMstInfoList;
        }

        public static List<TaskTypeMstInfo> GetAllTaskType()
        {
            string cacheKey = CacheHelper.GetCacheKeyForCompanyData(CacheConstants.ALL_TASK_TYPE);
            List<TaskTypeMstInfo> oTaskTypeMstInfoList = (List<TaskTypeMstInfo>)HttpRuntime.Cache[cacheKey];
            if (oTaskTypeMstInfoList == null)
            {
                try
                {
                    ITaskMaster oTaskClient = RemotingHelper.GetTaskMasterObject();
                    oTaskTypeMstInfoList = (List<TaskTypeMstInfo>)oTaskClient.GetAllTaskType(Helper.GetAppUserInfo());
                    HttpRuntime.Cache.Add(cacheKey, oTaskTypeMstInfoList, null, GetCacheExpirationTime(), Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);
                    //oReasonInfoCollection 
                }
                catch (Exception ex)
                {
                    Helper.FormatAndShowErrorMessage(null, ex);
                }
            }
            return oTaskTypeMstInfoList;
        }
        #endregion

        #region Report Column

        public static List<ReportColumnInfo> GetReportColumnInfoList(Int16 reportID, bool? IsOptional)
        {
            string cacheKey = CacheHelper.GetCacheKeyForCompanyData(CacheConstants.REPORT_COLUMNS) + "_" + reportID.ToString();
            if (IsOptional.HasValue)
                cacheKey += "_" + IsOptional.GetValueOrDefault().ToString();
            List<ReportColumnInfo> oReportColumnInfoList = (List<ReportColumnInfo>)HttpRuntime.Cache[cacheKey];
            if (oReportColumnInfoList == null)
            {
                try
                {
                    // Get from DB 
                    IReport oReportClient = RemotingHelper.GetReportObject();
                    ReportParamInfo oReportParamInfo = new ReportParamInfo();
                    Helper.FillCommonServiceParams(oReportParamInfo);
                    oReportParamInfo.ReportID = reportID;
                    oReportParamInfo.IsOptional = IsOptional;
                    oReportColumnInfoList = oReportClient.SelectAllReportColumnsByReportID(oReportParamInfo, Helper.GetAppUserInfo());
                    HttpRuntime.Cache.Add(cacheKey, oReportColumnInfoList, null, GetCacheExpirationTime(), Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);
                }
                catch (Exception ex)
                {
                    Helper.FormatAndShowErrorMessage(null, ex);
                }
            }
            return oReportColumnInfoList;
        }

        public static void ClearCacheReportColumnInfoList(Int16 reportID, bool? IsOptional)
        {
            string cacheKey = CacheHelper.GetCacheKeyForCompanyData(CacheConstants.REPORT_COLUMNS) + "_" + reportID.ToString();
            if (IsOptional.HasValue)
                cacheKey += "_" + IsOptional.GetValueOrDefault().ToString();
            ClearCache(cacheKey);
        }

        #endregion

        public static IList<DueDaysBasisInfo> GetAllDueDaysBasisType()
        {
            IList<DueDaysBasisInfo> oDueDaysBasisInfoList = (IList<DueDaysBasisInfo>)HttpRuntime.Cache[CacheConstants.ALL_DUE_DAYS_BASIS_TYPE_LIST];
            if (oDueDaysBasisInfoList == null)
            {
                try
                {
                    // Get from DB 
                    IUtility oUtilityClient = RemotingHelper.GetUtilityObject();
                    oDueDaysBasisInfoList = oUtilityClient.SelectAllDueDaysBasisMst(Helper.GetAppUserInfo());
                    HttpRuntime.Cache.Add(CacheConstants.ALL_DUE_DAYS_BASIS_TYPE_LIST, oDueDaysBasisInfoList, null, GetCacheExpirationTime(), Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);
                }
                catch (Exception ex)
                {
                    Helper.FormatAndShowErrorMessage(null, ex);
                }
            }
            return oDueDaysBasisInfoList;
        }

        public static IList<DaysTypeInfo> GetAllDaysType()
        {
            IList<DaysTypeInfo> oDaysTypeInfoList = (IList<DaysTypeInfo>)HttpRuntime.Cache[CacheConstants.ALL_DAYS_TYPE_LIST];
            if (oDaysTypeInfoList == null)
            {
                try
                {
                    // Get from DB 
                    IUtility oUtilityClient = RemotingHelper.GetUtilityObject();
                    oDaysTypeInfoList = oUtilityClient.SelectAllDaysType(Helper.GetAppUserInfo());
                    HttpRuntime.Cache.Add(CacheConstants.ALL_DAYS_TYPE_LIST, oDaysTypeInfoList, null, GetCacheExpirationTime(), Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);
                }
                catch (Exception ex)
                {
                    Helper.FormatAndShowErrorMessage(null, ex);
                }
            }
            return oDaysTypeInfoList;
        }

        public static List<OperatorMstInfo> GetOperatorList()
        {
            List<OperatorMstInfo> oOperatorCollection = (List<OperatorMstInfo>)HttpRuntime.Cache[CacheConstants.ALL_OPERATOR_TYPE];
            if (oOperatorCollection == null)
            {
                try
                {
                    String OperatorName = string.Empty;
                    IOperator oOperatorClient = RemotingHelper.GetOperatorObject();
                    oOperatorCollection = oOperatorClient.GetOperatorList(Helper.GetAppUserInfo());
                    HttpRuntime.Cache.Add(CacheConstants.ALL_OPERATOR_TYPE, oOperatorCollection, null, GetCacheExpirationTime(), Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);
                }
                catch (Exception ex)
                {
                    Helper.FormatAndShowErrorMessage(null, ex);
                }
            }
            return oOperatorCollection;
        }

        public static List<ImportFieldMstInfo> GetFieldsMst(int CompanyID, short? DataImportTypeID)
        {
            List<ImportFieldMstInfo> oImportFieldCollection = (List<ImportFieldMstInfo>)HttpRuntime.Cache[CacheConstants.ALL_IMPORTFIELD_LIST];
            if (oImportFieldCollection == null)
            {
                try
                {
                    String OperatorName = string.Empty;
                    IDataImport oDataImportClient = RemotingHelper.GetDataImportObject();
                    oImportFieldCollection = oDataImportClient.GetFieldsMst(CompanyID, DataImportTypeID, Helper.GetAppUserInfo());
                    HttpRuntime.Cache.Add(CacheConstants.ALL_IMPORTFIELD_LIST, oImportFieldCollection, null, GetCacheExpirationTime(), Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);
                }
                catch (Exception ex)
                {
                    Helper.FormatAndShowErrorMessage(null, ex);
                }
            }
            return oImportFieldCollection;
        }


        public static List<DataImportMessageInfo> GetDataImportMessageList()
        {
            List<DataImportMessageInfo> oDataImportMessageCollection = (List<DataImportMessageInfo>)HttpRuntime.Cache[CacheConstants.ALL_DATAIMPORTMESSAGE_LIST];
            if (oDataImportMessageCollection == null)
            {
                try
                {
                    String OperatorName = string.Empty;
                    IDataImport oDataImportClient = RemotingHelper.GetDataImportObject();
                    oDataImportMessageCollection = oDataImportClient.GetDataImportMessageList(Helper.GetAppUserInfo());
                    HttpRuntime.Cache.Add(CacheConstants.ALL_DATAIMPORTMESSAGE_LIST, oDataImportMessageCollection, null, GetCacheExpirationTime(), Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);
                }
                catch (Exception ex)
                {
                    Helper.FormatAndShowErrorMessage(null, ex);
                }
            }
            return oDataImportMessageCollection;
        }

        public static List<FTPServerInfo> GetAllFTPServerObject(int? CompanyId)
        {
            List<FTPServerInfo> oFTPServerInfoCollection = (List<FTPServerInfo>)HttpRuntime.Cache[CacheConstants.ALL_FTPSERVER_LIST];
            if (oFTPServerInfoCollection == null)
            {
                // Get from DB
                try
                {
                    ICompany oCompanyClient = RemotingHelper.GetCompanyObject();
                    oFTPServerInfoCollection = oCompanyClient.GetAllFTPServerListObject(CompanyId, Helper.GetAppUserInfo());
                    HttpRuntime.Cache.Add(CacheConstants.ALL_FTPSERVER_LIST, oFTPServerInfoCollection, null, GetCacheExpirationTime(), Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);
                }
                catch (Exception ex)
                {
                    Helper.FormatAndShowErrorMessage(null, ex);
                }
            }
            return oFTPServerInfoCollection;
        }

    }//end of class
}//end of namespace