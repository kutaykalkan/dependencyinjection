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
using System.Collections.Generic;
using SkyStem.ART.Web.Data;
using SkyStem.ART.Client.IServices;
using SkyStem.Language.LanguageUtility;
using SkyStem.ART.Client.Data;
using System.Collections;
using SkyStem.ART.Client.Params;
using SkyStem.ART.Client.Model.Matching;
using SkyStem.ART.Client.Params.Matching;
using SkyStem.Library.Controls.TelerikWebControls;
using Telerik.Web.UI;
using SkyStem.ART.Web.Classes;
using SkyStem.ART.Client.Model.QualityScore;
using SkyStem.ART.Client.Model.MappingUpload;


namespace SkyStem.ART.Web.Utility
{
    /// <summary>
    /// Summary description for SessionHelper
    /// </summary>
    /// 
    public class SessionHelper
    {
        public SessionHelper()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        #region "Properties"
        public static int? CurrentCompanyID
        {
            get
            {
                int? companyID = (int?)HttpContext.Current.Session[SessionConstants.CURRENT_COMPANY_ID];
                return companyID;
            }
            set
            {
                HttpContext.Current.Session[SessionConstants.CURRENT_COMPANY_ID] = value;
                CurrentCompanyDatabaseExists = Helper.IsCompanyDatabaseExists();
            }
        }

        public static bool? CurrentCompanyDatabaseExists
        {
            get
            {
                bool? val = (bool?)HttpContext.Current.Session[SessionConstants.CURRENT_COMPANY_DATABASE_EXISTS];
                return val;
            }
            private set
            {
                HttpContext.Current.Session[SessionConstants.CURRENT_COMPANY_DATABASE_EXISTS] = value;
            }
        }

        public static int? CurrentFinancialYearID
        {
            get
            {
                int? CurrentFinancialYearID = (int?)HttpContext.Current.Session[SessionConstants.CURRENT_FINANCIAL_YEAR_ID];
                return CurrentFinancialYearID;
            }
            set
            {
                HttpContext.Current.Session[SessionConstants.CURRENT_FINANCIAL_YEAR_ID] = value;
            }
        }

        #region "User related Properties"
        public static WebEnums.UserRole CurrentRoleEnum
        {
            get
            {
                Int16? userRoleID = SessionHelper.CurrentRoleID;
                WebEnums.UserRole eUserRole = WebEnums.UserRole.None;

                if (userRoleID != null)
                {
                    eUserRole = (WebEnums.UserRole)System.Enum.Parse(typeof(WebEnums.UserRole), userRoleID.Value.ToString());
                }
                return eUserRole;
            }
        }

        public static Int16? CurrentRoleID
        {
            get
            {
                Int16? userRoleID = null;
                if (HttpContext.Current.Session[SessionConstants.CURRENT_ROLE_ID] != null)
                    userRoleID = (Int16?)HttpContext.Current.Session[SessionConstants.CURRENT_ROLE_ID];
                return userRoleID;
            }
            set
            {
                HttpContext.Current.Session[SessionConstants.CURRENT_ROLE_ID] = value;
            }
        }

        public static Int32? CurrentUserID
        {
            get
            {
                Int32? userID = null;
                UserHdrInfo oUserHdrInfo = SessionHelper.GetCurrentUser();
                if (oUserHdrInfo != null)
                {
                    userID = oUserHdrInfo.UserID;
                    HttpContext.Current.Session[SessionConstants.CURRENT_USER_ID] = userID;
                }
                return userID;
            }
        }

        public static string CurrentUserLoginID
        {
            get
            {
                UserHdrInfo oUserHdrInfo = SessionHelper.GetCurrentUser();
                if (oUserHdrInfo != null)
                    return oUserHdrInfo.LoginID;
                return null;
            }
        }
        public static string CurrentUserEmailID
        {
            get
            {
                UserHdrInfo oUserHdrInfo = SessionHelper.GetCurrentUser();
                if (oUserHdrInfo != null)
                    return oUserHdrInfo.EmailID;
                return null;
            }
        }

        #endregion

        #region "Rec Period related Properties"
        public static Int32? CurrentReconciliationPeriodID
        {
            get
            {
                Int32? recPeriodID = null;
                ReconciliationPeriodInfo oReconciliationPeriodInfo = (ReconciliationPeriodInfo)HttpContext.Current.Session[SessionConstants.CURRENT_RECONCILIATION_PREIOD_INFO];

                if (oReconciliationPeriodInfo != null)
                {
                    recPeriodID = oReconciliationPeriodInfo.ReconciliationPeriodID;
                }
                return recPeriodID;
            }
        }

        public static Int32? CompanyCurrentReconciliationPeriodID
        {
            get
            {
                Int32? recPeriodID = null;
                IList<CompanySettingInfo> oCompanySettingInfoList = (IList<CompanySettingInfo>)HttpContext.Current.Session[SessionConstants.CURRENT_COMPANY_SETTINGS];

                if (oCompanySettingInfoList != null && oCompanySettingInfoList.Count > 0)
                {
                    recPeriodID = oCompanySettingInfoList[0].CurrentReconciliationPeriodID;
                }
                return recPeriodID;
            }
        }

        public static DateTime? CurrentReconciliationPeriodEndDate
        {
            get
            {
                DateTime? recPreiodEndDate = null;
                ReconciliationPeriodInfo oReconciliationPeriodInfo = (ReconciliationPeriodInfo)HttpContext.Current.Session[SessionConstants.CURRENT_RECONCILIATION_PREIOD_INFO];

                if (oReconciliationPeriodInfo != null)
                {
                    recPreiodEndDate = oReconciliationPeriodInfo.PeriodEndDate;
                }
                return recPreiodEndDate;
            }
        }


        public static DateTime? CurrentCertificationStartDate
        {
            get
            {
                DateTime? CertificationStartDate = null;
                ReconciliationPeriodInfo oReconciliationPeriodInfo = (ReconciliationPeriodInfo)HttpContext.Current.Session[SessionConstants.CURRENT_RECONCILIATION_PREIOD_INFO];

                if (oReconciliationPeriodInfo != null)
                {
                    CertificationStartDate = oReconciliationPeriodInfo.CertificationStartDate;
                }
                return CertificationStartDate;
            }
        }

        public static WebEnums.RecPeriodStatus CurrentRecProcessStatusEnum
        {
            get
            {
                WebEnums.RecPeriodStatus eRecProcessStatus = WebEnums.RecPeriodStatus.NotStarted;
                ReconciliationPeriodStatusMstInfo oReconciliationPeriodStatusMstInfo = SessionHelper.GetRecPeriodStatus();
                if (oReconciliationPeriodStatusMstInfo != null)
                    eRecProcessStatus = (WebEnums.RecPeriodStatus)System.Enum.Parse(typeof(WebEnums.RecPeriodStatus), oReconciliationPeriodStatusMstInfo.ReconciliationPeriodStatusID.Value.ToString(), true);
                return eRecProcessStatus;
            }
        }
        #endregion

        public static string ReportingCurrencyCode
        {
            get
            {
                string reportingCurrencyCode = (string)HttpContext.Current.Session[SessionConstants.REPORTING_CURRENCY_CODE];

                if (string.IsNullOrEmpty(reportingCurrencyCode))
                {
                    ICompany oCompanyClient = RemotingHelper.GetCompanyObject();
                    reportingCurrencyCode = oCompanyClient.GetReportingCurrencyByRecPeriodID(SessionHelper.CurrentReconciliationPeriodID.Value, Helper.GetAppUserInfo());
                    HttpContext.Current.Session[SessionConstants.REPORTING_CURRENCY_CODE] = reportingCurrencyCode;
                }

                return reportingCurrencyCode;
            }
        }

        #region Matching Related Properties

        /// <summary>
        /// Gets or sets the current match set sub set combination info.
        /// </summary>
        /// <value>
        /// The current match set sub set combination info.
        /// </value>
        public static MatchSetSubSetCombinationInfo CurrentMatchSetSubSetCombinationInfo
        {
            get
            {
                return (MatchSetSubSetCombinationInfo)HttpContext.Current.Session[SessionConstants.MATCHING_CURRENT_MATCH_SET_SUB_SET_COMBINATION_INFO];
            }
            set
            {
                HttpContext.Current.Session[SessionConstants.MATCHING_CURRENT_MATCH_SET_SUB_SET_COMBINATION_INFO] = value;
            }
        }

        #endregion

        #endregion


        public static List<CompanyCapabilityInfo> GetCompanyCapabilityCollectionForCurrentRecPeriod()
        {
            List<CompanyCapabilityInfo> oCompanyCapabilityInfoCollection = (List<CompanyCapabilityInfo>)HttpContext.Current.Session[SessionConstants.CURRENT_CAPABILITY_COLLECTION];
            if (oCompanyCapabilityInfoCollection == null)
            {
                ICompany oCompanyClient = RemotingHelper.GetCompanyObject();
                oCompanyCapabilityInfoCollection = oCompanyClient.SelectAllCompanyCapabilityByReconciliationID(SessionHelper.CurrentReconciliationPeriodID, Helper.GetAppUserInfo());
                HttpContext.Current.Session[SessionConstants.CURRENT_CAPABILITY_COLLECTION] = oCompanyCapabilityInfoCollection;
            }
            return oCompanyCapabilityInfoCollection;
        }

        public static List<CapabilityMstInfo> GetAllCapabilities()
        {
            List<CapabilityMstInfo> oCapabilityMstInfoList = (List<CapabilityMstInfo>)HttpContext.Current.Session[SessionConstants.ALL_CAPABILITY_LIST];
            if (oCapabilityMstInfoList == null)
            {
                oCapabilityMstInfoList = (List<CapabilityMstInfo>)Helper.DeepClone(CacheHelper.GetAllCapabilities());
                LanguageHelper.TranslateCapabilities(oCapabilityMstInfoList);
                HttpContext.Current.Session[SessionConstants.ALL_CAPABILITY_LIST] = oCapabilityMstInfoList;
            }
            return oCapabilityMstInfoList;
        }

        public static List<ReportMstInfo> GetStandardReportListByRole()
        {
            List<ReportMstInfo> oReportMstInfoCollection = (List<ReportMstInfo>)HttpContext.Current.Session[SessionConstants.STANDARD_REPORT_LIST_BY_ROLE];
            if (oReportMstInfoCollection == null)
            {
                IReport oReport = RemotingHelper.GetReportObject();
                oReportMstInfoCollection = oReport.SelectAllReportByRoleID(SessionHelper.CurrentRoleID, SessionHelper.CurrentReconciliationPeriodID, SessionHelper.CurrentCompanyID, Helper.GetAppUserInfo());
                HttpContext.Current.Session[SessionConstants.STANDARD_REPORT_LIST_BY_ROLE] = oReportMstInfoCollection;
            }
            return oReportMstInfoCollection;
        }


        /// <summary>
        /// Gets LCID for current user.
        /// </summary>
        /// <returns></returns>
        public static int GetUserLanguage()
        {
            int lcid = 0;
            if (HttpContext.Current.Session[SessionConstants.CURRENT_LANGUAGE] != null)
                lcid = (int)HttpContext.Current.Session[SessionConstants.CURRENT_LANGUAGE];
            return lcid;
        }

        /// <summary>
        /// Sets the user language.
        /// </summary>
        /// <param name="lcid">The lcid.</param>
        public static void SetUserLanguage(int lcid)
        {
            HttpContext.Current.Session[SessionConstants.CURRENT_LANGUAGE] = lcid;
        }

        public static int GetBusinessEntityID()
        {
            int businessEntityID = 0;
            //Apoorv: CompanyID is the Business Entity ID, hence just returning Company ID
            if (SessionHelper.CurrentCompanyID != null)
            {
                businessEntityID = SessionHelper.CurrentCompanyID.Value;
            }
            return businessEntityID;
        }

        public static List<RoleMstInfo> GetUserRole()
        {
            List<RoleMstInfo> oRoleMstInfoCollection = (List<RoleMstInfo>)HttpContext.Current.Session[SessionConstants.USER_ROLE];
            if (oRoleMstInfoCollection == null)
            {
                IUser oUserClient = RemotingHelper.GetUserObject();
                oRoleMstInfoCollection = oUserClient.GetUserRole(SessionHelper.GetCurrentUser().UserID, Helper.GetAppUserInfo());
                oRoleMstInfoCollection = RemoveRolesNotInPackage(oRoleMstInfoCollection);
                // Translate
                oRoleMstInfoCollection = LanguageHelper.TranslateRoleCollection(oRoleMstInfoCollection);
                HttpContext.Current.Session[SessionConstants.USER_ROLE] = oRoleMstInfoCollection;
            }

            return oRoleMstInfoCollection;
        }

        public static List<RoleMstInfo> RemoveRolesNotInPackage(List<RoleMstInfo> oRoleMstInfoCollection)
        {
            List<RoleMstInfo> oRoleMstInfoList = SessionHelper.GetAllRoles();
            if (oRoleMstInfoCollection != null && oRoleMstInfoCollection.Count > 0 && oRoleMstInfoList != null && oRoleMstInfoList.Count > 0)
                oRoleMstInfoCollection.RemoveAll(T => !oRoleMstInfoList.Exists(R => R.RoleID == T.RoleID));
            return oRoleMstInfoCollection;
        }

        public static List<MenuMstInfo> GetUserMenu()
        {
            List<MenuMstInfo> oMenuMstInfoCollection = null;
            string key = GetSessionKeyForMenu(); //SessionConstants.USER_MENUS + "_" + SessionHelper.CurrentRoleID.Value.ToString();

            MenuParamInfo oMenuParamInfo = new MenuParamInfo();
            oMenuParamInfo.CompanyID = SessionHelper.CurrentCompanyID;
            oMenuParamInfo.RoleID = SessionHelper.CurrentRoleID;
            oMenuParamInfo.RecPeriodID = SessionHelper.CurrentReconciliationPeriodID;

            if (HttpContext.Current.Session[key] != null)
            {
                oMenuMstInfoCollection = (List<MenuMstInfo>)HttpContext.Current.Session[key];
            }
            else
            {
                IRole oRoleClient = RemotingHelper.GetRoleObject();
                oMenuMstInfoCollection = oRoleClient.GetMenuByRoleID(oMenuParamInfo, Helper.GetAppUserInfo());
                List<CompanyAttributeConfigInfo> roleConfigInfo = AttributeConfigHelper.GetCompanyAttributeConfigInfoList(false, WebEnums.AttributeSetType.RoleConfig);

                if (!FTPHelper.IsFTPActivatedCompanyAndUserLevel())
                {
                    oMenuMstInfoCollection.RemoveAll(T => T.MenuID == 74);// Change FTP Password
                }

                if (SessionHelper.CurrentRoleID == (short)WebEnums.UserRole.AUDIT)
                {
                    if (roleConfigInfo != null && roleConfigInfo.Count > 0)
                    {
                        bool isAllAccountSelectedInOpenPeriod = false;
                        CompanyAttributeConfigInfo isCompletedAccountsInOpenPeriod = (CompanyAttributeConfigInfo)roleConfigInfo.Find(c => c.AttributeID == (int)ARTEnums.AttributeList.ReconciledAccountsInOpenPeriod);
                        if (isCompletedAccountsInOpenPeriod != null)
                        {
                            if (isCompletedAccountsInOpenPeriod.IsEnabled.HasValue)
                            {
                                if ((bool)isCompletedAccountsInOpenPeriod.IsEnabled)
                                {
                                    isAllAccountSelectedInOpenPeriod = true;
                                }
                            }
                        }

                        bool isAllAccountSelectedInClosedPeriod = false;
                        CompanyAttributeConfigInfo isCompletedAccountsInClosedPeriod = (CompanyAttributeConfigInfo)roleConfigInfo.Find(c => c.AttributeID == (int)ARTEnums.AttributeList.ReconciledAccountsInClosedPeriod);
                        if (isCompletedAccountsInClosedPeriod != null)
                        {
                            if (isCompletedAccountsInClosedPeriod.IsEnabled.HasValue)
                            {
                                if ((bool)isCompletedAccountsInClosedPeriod.IsEnabled)
                                {
                                    isAllAccountSelectedInClosedPeriod = true;
                                }
                            }
                        }

                        if (((isAllAccountSelectedInOpenPeriod == false && isAllAccountSelectedInClosedPeriod == false) || (isAllAccountSelectedInClosedPeriod == true)))
                        { }
                        else
                        {
                            int CertificationMenuID = 2;
                            oMenuMstInfoCollection.RemoveAll(c => c.MenuID == CertificationMenuID);
                        }
                    }

                }

                HttpContext.Current.Session[key] = oMenuMstInfoCollection;
            }

            return oMenuMstInfoCollection;
        }

        public static string GetSessionKeyForMenu()
        {
            string sessionKey = "";
            if (SessionHelper.CurrentReconciliationPeriodID.HasValue)
                sessionKey = SessionConstants.USER_MENUS + "_" + SessionHelper.CurrentRoleID.Value.ToString() + "_" + SessionHelper.CurrentReconciliationPeriodID.Value.ToString();
            else
                sessionKey = SessionConstants.USER_MENUS + "_" + SessionHelper.CurrentRoleID.Value.ToString();
            return sessionKey;
        }

        public static void SetCurrentUser(UserHdrInfo oUserHdrInfo)
        {
            HttpContext.Current.Session.Remove(SessionConstants.USER_INFO);
            if (oUserHdrInfo != null)
            {
                HttpContext.Current.Session.Add(SessionConstants.USER_INFO, oUserHdrInfo);
            }
        }

        public static UserHdrInfo GetCurrentUser()
        {
            UserHdrInfo oUserHdrInfo = null;
            if (HttpContext.Current.Session != null && HttpContext.Current.Session[SessionConstants.USER_INFO] != null)
            {
                oUserHdrInfo = (UserHdrInfo)HttpContext.Current.Session[SessionConstants.USER_INFO];
            }
            return oUserHdrInfo;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static ReconciliationPeriodStatusMstInfo GetRecPeriodStatus()
        {
            /* 
             * All Rec Process available in Cache 
             * Get list from Cache 
             * Get Status ID
             * If In Progress, go to DB to check for Closed
             * Get Label from Cache
             * Set in Session
            */
            ReconciliationPeriodStatusMstInfo oReconciliationPeriodStatusMstInfo = null;
            if (SessionHelper.CurrentReconciliationPeriodID != null)
            {

                oReconciliationPeriodStatusMstInfo = (ReconciliationPeriodStatusMstInfo)HttpContext.Current.Session[SessionConstants.REC_PERIOD_STATUS_INFO];
                if (oReconciliationPeriodStatusMstInfo == null)
                {
                    List<ReconciliationPeriodInfo> oReconciliationPeriodInfoCollection = (List<ReconciliationPeriodInfo>)Helper.DeepClone(CacheHelper.GetAllReconciliationPeriods(null));
                    ReconciliationPeriodInfo oReconciliationPeriodInfo = oReconciliationPeriodInfoCollection.Find(c => c.ReconciliationPeriodID == SessionHelper.CurrentReconciliationPeriodID.Value);

                    // Get Rec Period Status Info
                    oReconciliationPeriodStatusMstInfo = Helper.GetRecPeriodStatusInfo(oReconciliationPeriodInfo.ReconciliationPeriodStatusID);
                }

                WebEnums.RecPeriodStatus eRecProcessStatus = (WebEnums.RecPeriodStatus)System.Enum.Parse(typeof(WebEnums.RecPeriodStatus), oReconciliationPeriodStatusMstInfo.ReconciliationPeriodStatusID.Value.ToString(), true);

                // If Status = IN Progress, then check DB once again
                switch (eRecProcessStatus)
                {
                    case WebEnums.RecPeriodStatus.Open:
                    case WebEnums.RecPeriodStatus.InProgress:
                        // Check for InProgress and Closed
                        /*
                         * Closed => Rec Period Status "Closed" is marked by Service, hence the update is not available in Session
                         * InProgress => Rec Period Status "InProgress" is marked from multiple places, hence the update is not available in Session
                         */
                        IReconciliationPeriod oReconciliationPeriodClient = RemotingHelper.GetReconciliationPeriodObject();
                        oReconciliationPeriodStatusMstInfo = oReconciliationPeriodClient.GetRecPeriodStatus(SessionHelper.CurrentReconciliationPeriodID, Helper.GetAppUserInfo());
                        break;
                }

                HttpContext.Current.Session[SessionConstants.REC_PERIOD_STATUS_INFO] = oReconciliationPeriodStatusMstInfo;
            }
            return oReconciliationPeriodStatusMstInfo;
        }

        public static void CheckSessionForUser()
        {
            if (!SessionHelper.IsSessionValid())
            {
                RedirectToUrl("~/Logout.aspx");
            }
        }

        public static void RedirectToUrl(string url)
        {
            url = ResolveUrl(url);
            if (CanRedirect())
                HttpContext.Current.Response.Redirect(url, false);
            else
                RedirectUsingClientJS(url);
        }

        public static void TransferToUrl(string url)
        {
            if (CanRedirect())
                HttpContext.Current.Server.Transfer(url);
            else
                RedirectUsingClientJS(url);
        }

        public static bool CanRedirect()
        {
            if (HttpContext.Current == null
                || HttpContext.Current.CurrentHandler == null)
                return false;
            Page oPage = HttpContext.Current.CurrentHandler as Page;
            if (oPage == null || oPage.IsCallback)
                return false;
            return true;
        }

        public static void RedirectUsingClientJS(string url)
        {
            Page oPage = (Page)HttpContext.Current.CurrentHandler;
            if (oPage != null)
            {
                string js = string.Format("document.location.href = '{0}'", oPage.ResolveUrl(url));
                ScriptManager.RegisterClientScriptBlock(oPage, typeof(Page), "RedirectToUrl", js, true);
            }
        }

        public static string ResolveUrl(string originalUrl)
        {
            if (originalUrl == null)
                return null;
            // *** Absolute path - just return
            if (originalUrl.IndexOf("://") != -1)
                return originalUrl;
            // *** Fix up image path for ~ root app dir directory
            if (originalUrl.StartsWith("~"))
            {
                string newUrl = "";
                if (HttpContext.Current != null)
                {
                    newUrl = HttpContext.Current.Request.ApplicationPath +
                         originalUrl.Substring(1).Replace("//", "/");
                    return newUrl;
                }
            }
            return originalUrl;
        }

        public static bool IsSessionValid()
        {
            UserHdrInfo oUserHdrInfo = SessionHelper.GetCurrentUser();
            if (oUserHdrInfo == null)
            {
                return false;
            }
            return true;
        }


        public static bool IsUserLanguageExists()
        {
            return HttpContext.Current.Session[SessionConstants.CURRENT_LANGUAGE] != null;
        }

        #region master table's data in session
        public static IList<MaterialityTypeMstInfo> GetAllMaterialityType()
        {
            IList<MaterialityTypeMstInfo> lstMaterialityTypeMstInfo = null;
            if (HttpContext.Current.Session[SessionConstants.ALL_MATERIALITYTYPE_LIST] != null)
            {
                lstMaterialityTypeMstInfo = (IList<MaterialityTypeMstInfo>)HttpContext.Current.Session[SessionConstants.ALL_MATERIALITYTYPE_LIST];
            }
            else
            {
                lstMaterialityTypeMstInfo = (List<MaterialityTypeMstInfo>)Helper.DeepClone(CacheHelper.GetAllMaterialityType());
                lstMaterialityTypeMstInfo = LanguageHelper.TranslateLabelMaterialityTypeMstInfo(lstMaterialityTypeMstInfo);
                HttpContext.Current.Session.Add(SessionConstants.ALL_MATERIALITYTYPE_LIST, lstMaterialityTypeMstInfo);
            }
            return lstMaterialityTypeMstInfo;
        }

        /// <summary>
        /// Gets all risk ratings defined in the system
        /// </summary>
        /// <returns>returns the list of risk ratings</returns>
        public static List<RiskRatingMstInfo> GetAllRiskRating()
        {
            List<RiskRatingMstInfo> oRiskRatingMstInfoCollection = (List<RiskRatingMstInfo>)HttpContext.Current.Session[SessionConstants.ALL_RISKRATING_LIST]; ;
            if (oRiskRatingMstInfoCollection == null)
            {
                oRiskRatingMstInfoCollection = (List<RiskRatingMstInfo>)Helper.DeepClone(CacheHelper.GetAllRiskRating());
                oRiskRatingMstInfoCollection = LanguageHelper.TranslateLabelRiskRatingMstInfo(oRiskRatingMstInfoCollection);
                HttpContext.Current.Session.Add(SessionConstants.ALL_RISKRATING_LIST, oRiskRatingMstInfoCollection);
            }

            //((List<RiskRatingMstInfo>)oRiskRatingMstInfoCollection).RemoveAll(riskRating => riskRating.RiskRatingID == Convert.ToInt32(WebConstants.SELECT_ONE)
            //    || riskRating.RiskRatingID == Convert.ToInt32(WebConstants.ALL));

            return oRiskRatingMstInfoCollection;
        }

        public static IList<ReconciliationFrequencyMstInfo> GetAllReconciliationFrequency()
        {
            IList<ReconciliationFrequencyMstInfo> lstReconciliationFrequencyMstInfo = null;
            if (HttpContext.Current.Session[SessionConstants.ALL_RECONCILIATIONFREQUENCY_LIST] != null)
            {
                lstReconciliationFrequencyMstInfo = (IList<ReconciliationFrequencyMstInfo>)HttpContext.Current.Session[SessionConstants.ALL_RECONCILIATIONFREQUENCY_LIST];
            }
            else
            {
                lstReconciliationFrequencyMstInfo = (List<ReconciliationFrequencyMstInfo>)Helper.DeepClone(CacheHelper.GetAllReconciliationFrequency());
                lstReconciliationFrequencyMstInfo = LanguageHelper.TranslateLabelReconciliationFrequencyMstInfo(lstReconciliationFrequencyMstInfo);
                HttpContext.Current.Session.Add(SessionConstants.ALL_RECONCILIATIONFREQUENCY_LIST, lstReconciliationFrequencyMstInfo);
            }
            return lstReconciliationFrequencyMstInfo;
        }

        public static IList<DataImportTypeMstInfo> GetAllDataImportType()
        {
            IList<DataImportTypeMstInfo> oDataImportTypeCollection = null;
            if (HttpContext.Current.Session[SessionConstants.ALL_DATAIMPORTTYPE_LIST] != null)
            {
                oDataImportTypeCollection = (IList<DataImportTypeMstInfo>)HttpContext.Current.Session[SessionConstants.ALL_DATAIMPORTTYPE_LIST];

            }
            else
            {
                oDataImportTypeCollection = (IList<DataImportTypeMstInfo>)Helper.DeepClone(CacheHelper.GetAllDataImportType(SessionHelper.CurrentRoleID));
                oDataImportTypeCollection = LanguageHelper.TranslateLabelDataImportTypeMstInfo(oDataImportTypeCollection);
                HttpContext.Current.Session.Add(SessionConstants.ALL_DATAIMPORTTYPE_LIST, oDataImportTypeCollection);
            }
            return oDataImportTypeCollection;
        }

        public static IList<DualLevelReviewTypeMstInfo> GetAllDualLevelReviewType()
        {
            IList<DualLevelReviewTypeMstInfo> lstDualLevelReviewTypeMstInfo = null;
            if (HttpContext.Current.Session[SessionConstants.ALL_DUALLEVELREVIEWTYPE_LIST] != null)
            {
                lstDualLevelReviewTypeMstInfo = (IList<DualLevelReviewTypeMstInfo>)HttpContext.Current.Session[SessionConstants.ALL_DUALLEVELREVIEWTYPE_LIST];
            }
            else
            {
                lstDualLevelReviewTypeMstInfo = (List<DualLevelReviewTypeMstInfo>)Helper.DeepClone(CacheHelper.GetAllDualLevelReviewType());
                lstDualLevelReviewTypeMstInfo = LanguageHelper.TranslateLabelDualLevelReviewTypeMstInfo(lstDualLevelReviewTypeMstInfo);
                HttpContext.Current.Session.Add(SessionConstants.ALL_DUALLEVELREVIEWTYPE_LIST, lstDualLevelReviewTypeMstInfo);
            }
            return lstDualLevelReviewTypeMstInfo;
        }
        #endregion


        public static List<GeographyClassMstInfo> GetAllOrganizationalHierarchyKeys(short? companyGeographyClassID)
        {
            List<GeographyClassMstInfo> oGeographyClassMstInfoCollection = (List<GeographyClassMstInfo>)HttpContext.Current.Session[SessionConstants.ORGANIZATIONAL_HIERARCHY_KEYS];
            if (oGeographyClassMstInfoCollection == null)
            {
                oGeographyClassMstInfoCollection = (List<GeographyClassMstInfo>)Helper.DeepClone(CacheHelper.GetAllOrganizationalHierarchyKeys(companyGeographyClassID));
                // Translate
                oGeographyClassMstInfoCollection = LanguageHelper.TranslateKeysCollection(oGeographyClassMstInfoCollection);
                HttpContext.Current.Session[SessionConstants.ORGANIZATIONAL_HIERARCHY_KEYS] = oGeographyClassMstInfoCollection;
            }

            return oGeographyClassMstInfoCollection;
        }

        /// <summary>
        /// Selects all account type with language specific display text
        /// </summary>
        /// <returns>List of all account type with display text</returns>
        public static List<AccountTypeMstInfo> SelectAllAccountTypeMstInfoWithDisplayText()
        {
            List<AccountTypeMstInfo> oAccountTypeMstInfoCollection = (List<AccountTypeMstInfo>)HttpContext.Current.Session[SessionConstants.ALL_ACCOUNT_TYPE_MST_INFO];

            if (oAccountTypeMstInfoCollection == null)
            {
                oAccountTypeMstInfoCollection = LanguageHelper.TranslateLabelAccountType((List<AccountTypeMstInfo>)Helper.DeepClone(CacheHelper.SelectAllAccountTypeMstInfo()));
                HttpContext.Current.Session.Add(SessionConstants.ALL_ACCOUNT_TYPE_MST_INFO, oAccountTypeMstInfoCollection);
            }

            oAccountTypeMstInfoCollection.RemoveAll(accType => accType.AccountTypeID == Convert.ToInt16(WebConstants.SELECT_ONE));

            return oAccountTypeMstInfoCollection;
        }

        /// <summary>
        /// Selects all Reconciliation Template with language specific display text
        /// </summary>
        /// <returns>List of all Reconciliation Template with display text</returns>
        public static List<ReconciliationTemplateMstInfo> SelectAllReconciliationTemplateMstInfoWithDisplayText()
        {
            List<ReconciliationTemplateMstInfo> oReconciliationTemplateMstInfoCollection = (List<ReconciliationTemplateMstInfo>)HttpContext.Current.Session[SessionConstants.ALL_RECONCILIATION_TEMPLATE_MST_INFO];

            if (oReconciliationTemplateMstInfoCollection == null)
            {
                oReconciliationTemplateMstInfoCollection = LanguageHelper.TranslateLabelReconciliationTemplates((List<ReconciliationTemplateMstInfo>)Helper.DeepClone(CacheHelper.SelectAllReconciliationTemplateMstInfo()));
                HttpContext.Current.Session.Add(SessionConstants.ALL_RECONCILIATION_TEMPLATE_MST_INFO, oReconciliationTemplateMstInfoCollection);
            }

            oReconciliationTemplateMstInfoCollection.RemoveAll(recTemplate => recTemplate.ReconciliationTemplateID == Convert.ToInt16(WebConstants.SELECT_ONE));
            return oReconciliationTemplateMstInfoCollection;
        }

        /// <summary>
        /// Selects all risk rating with display text by calling LanguageUtil.GetValue() 
        /// with appropriate labelid.
        /// </summary>
        /// <returns>List of risk ratings with display text</returns>
        public static List<RiskRatingMstInfo> SelectAllRiskRatingMstInfoWithDisplayText()
        {
            List<RiskRatingMstInfo> oRiskRatingMstInfoCollection = (List<RiskRatingMstInfo>)HttpContext.Current.Session[SessionConstants.ALL_RISK_RATING_MST_INFO];

            if (oRiskRatingMstInfoCollection == null)
            {
                oRiskRatingMstInfoCollection = (List<RiskRatingMstInfo>)LanguageHelper.TranslateLabelRiskRatingMstInfo((List<RiskRatingMstInfo>)Helper.DeepClone(CacheHelper.SelectAllRiskRatingMstInfo()));
                HttpContext.Current.Session.Add(SessionConstants.ALL_RISK_RATING_MST_INFO, oRiskRatingMstInfoCollection);
            }

            return oRiskRatingMstInfoCollection;
        }

        /// <summary>
        /// Sets session value to null for the key given
        /// </summary>
        /// <param name="key">session key which needs to be cleared</param>
        public static void ClearSession(string key)
        {
            if (HttpContext.Current.Session[key] != null)
                HttpContext.Current.Session.Remove(key);
        }
        /// <summary>
        /// Clear data from session where key starts with prefix
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="clearAll"></param>
        public static void ClearSessionByPrefix(string prefix)
        {
            if (HttpContext.Current.Session != null && HttpContext.Current.Session.Keys != null && HttpContext.Current.Session.Keys.Count > 0)
            {
                for (int i = HttpContext.Current.Session.Keys.Count - 1; i >= 0; i--)
                {
                    string key = HttpContext.Current.Session.Keys[i];
                    if (key.StartsWith(prefix, StringComparison.CurrentCulture))
                    {
                        if (HttpContext.Current.Session[key] != null)
                            HttpContext.Current.Session.Remove(key);
                    }
                }
            }
        }

        public static void ClearGridFilterDataFromSession(ARTEnums.Grid eGrid)
        {
            string key = SessionHelper.GetSessionKeyForGridFilter(eGrid);
            if (HttpContext.Current.Session[key] != null)
                HttpContext.Current.Session.Remove(key);
        }

        //public static void ClearGridDynamicFilterDataFromSession()
        //{
        //    string key = SessionHelper.GetSessionKeyForGridDynamicFilter();
        //    if (HttpContext.Current.Session[key] != null)
        //        HttpContext.Current.Session.Remove(key);
        //}

        public static void ClearFYDataFromSession()
        {
            // Clear Rec Period Info
            SessionHelper.ClearRecPeriodDataFromSession();

            //Clear the existing Rec Period related data from Cache
            CacheHelper.ClearRecPeriodDataFromCache();
        }

        public static void ClearRecPeriodDataFromSession()
        {
            ClearMenuFromSession();
            HttpContext.Current.Session.Remove(SessionConstants.CURRENT_RECONCILIATION_PREIOD_INFO);
            HttpContext.Current.Session.Remove(SessionConstants.REC_PERIOD_STATUS_INFO);
            HttpContext.Current.Session.Remove(SessionConstants.CURRENT_CAPABILITY_COLLECTION);
            HttpContext.Current.Session.Remove(SessionConstants.BASE_CURRENCY_CODE);
            HttpContext.Current.Session.Remove(SessionConstants.REPORTING_CURRENCY_CODE);
            HttpContext.Current.Session.Remove(SessionConstants.ALL_COMPANY_ALERT_LIST);
            HttpContext.Current.Session.Remove(SessionConstants.ALL_ROLES);
            HttpContext.Current.Session.Remove(SessionConstants.USER_ROLES);
            HttpContext.Current.Session.Remove(SessionConstants.USER_ROLE);
            HttpContext.Current.Session.Remove(SessionConstants.CURRENT_COMPANY_HDR_INFO);
        }

        public static void ClearMenuFromSession()
        {
            HttpContext.Current.Session.Remove(GetSessionKeyForMenu());
        }

        public static void ClearRecPeriodCompanyCapabilityData()
        {
            HttpContext.Current.Session.Remove(SessionConstants.REC_PERIOD_CAPABILITY_COLLECTION);
        }

        public static void SetCurrentReconciliationPeriodInfo(ListItem oListItem)
        {
            if (oListItem != null)
            {
                ReconciliationPeriodInfo oReconciliationPeriodInfo = new ReconciliationPeriodInfo();
                oReconciliationPeriodInfo.ReconciliationPeriodID = Convert.ToInt32(oListItem.Value);
                oReconciliationPeriodInfo.PeriodEndDate = Convert.ToDateTime(oListItem.Text);
                HttpContext.Current.Session[SessionConstants.CURRENT_RECONCILIATION_PREIOD_INFO] = oReconciliationPeriodInfo;
                
            }
        }
        public static void SetDynamicFilterResultWhereClause(List<string> strClause, String SessionKey)
        {
            //SessionKey + "FilterClause"
            HttpContext.Current.Session[SessionConstants.GRID_FILTER_CLAUSE + SessionKey] = strClause;
        }

        public static void SetDynamicFilterCriteria(List<FilterCriteria> criteria, String SessionKey)
        {
            // SessionKey + "FilterCriteria"
            HttpContext.Current.Session[SessionConstants.GRID_DYNAMIC_FILTER_CRITERIA + SessionKey] = criteria;
        }

        public static string GetSessionKeyForGridCustomization(ARTEnums.Grid eGrid)
        {
            return SessionConstants.GRID_CUSTOMIZATION_GRID_TYPE + eGrid.ToString("d");
        }

        public static string GetSessionKeyForGridFilter(ARTEnums.Grid eGrid)
        {
            return SessionConstants.GRID_FILTER_CRITERIA + eGrid.ToString("d");
        }
        public static List<FilterCriteria> GetDynamicFilterCriteria(String SessionKey)
        {
            List<FilterCriteria> lstFilterCriteria = null;
            if (HttpContext.Current.Session[SessionConstants.GRID_DYNAMIC_FILTER_CRITERIA + SessionKey] != null)
            {
                lstFilterCriteria = HttpContext.Current.Session[SessionConstants.GRID_DYNAMIC_FILTER_CRITERIA + SessionKey] as List<FilterCriteria>;
            }
            return lstFilterCriteria;
        }

        public static void ClearSearchResultsFromSession(params string[] sessionKeysTobeExcluded)
        {
            Int16 i;
            string key;
            List<string> oKeyList = new List<string>();

            ICollection oSessionKeysCollection;
            string[] oSessionKeyArray = new string[HttpContext.Current.Session.Keys.Count];

            //  Get the Session Keys Collection and Iterate thru the Keys Collection
            oSessionKeysCollection = HttpContext.Current.Session.Keys;

            //  Session Collection might be modified by some other thread running, hence make a copy of Keys 
            //  at this instance and work on that Array
            oSessionKeysCollection.CopyTo(oSessionKeyArray, 0);

            for (i = 0; (i <= (oSessionKeyArray.Length - 1)); i++)
            {
                key = oSessionKeyArray[i];
                if (key.StartsWith(SessionConstants.SEARCH_RESULTS_PREFIX))
                {
                    //  Work with only those Keys that have "SkyStemART_SearchResults_" prefixed to them
                    if ((Array.IndexOf(sessionKeysTobeExcluded, key) == -1))
                    {
                        //  If the Key does not matches the Key to be excluded then Remove from Session
                        oKeyList.Add(key);
                    }
                }
            }
            //  loop thru the Keys and remove from Session
            for (i = 0; i < oKeyList.Count; i++)
            {
                HttpContext.Current.Session.Remove(oKeyList[i]);
            }
        }



        public static List<GeographyStructureHdrInfo> GetOrganizationalHierarchy(int? CompanyID)
        {
            if (CompanyID == null)
            {
                throw new Exception(WebConstants.COMPANY_NOT_SPECIFIED);
            }

            List<GeographyStructureHdrInfo> oGeographyStructureHdrInfoCollection = (List<GeographyStructureHdrInfo>)HttpContext.Current.Session[SessionConstants.GEOGRAPHY_STRUCTURE_COLLECTION];
            if (oGeographyStructureHdrInfoCollection == null)
            {
                // Get from Cache and Add to Session
                oGeographyStructureHdrInfoCollection = (List<GeographyStructureHdrInfo>)Helper.DeepClone(CacheHelper.GetOrganizationalHierarchy(CompanyID));
                // Translate
                LanguageHelper.TranslateOrganizationalHierarchy(oGeographyStructureHdrInfoCollection);

                HttpContext.Current.Session[SessionConstants.GEOGRAPHY_STRUCTURE_COLLECTION] = oGeographyStructureHdrInfoCollection;
            }

            return oGeographyStructureHdrInfoCollection;
        }

        public static void ClearCompanyDataFromSession()
        {
            // Remove Geo Structure
            HttpContext.Current.Session.Remove(SessionConstants.GEOGRAPHY_STRUCTURE_COLLECTION);
            HttpContext.Current.Session.Remove(SessionConstants.CURRENT_FINANCIAL_YEAR_ID);
            // Clear all Rec Period Related Data From Session, since Company has changed
            SessionHelper.ClearRecPeriodDataFromSession();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static List<CompanyHdrInfo> GetAllCompaniesLiteObject()
        {
            List<CompanyHdrInfo> oCompanyHdrInfoCollection = (List<CompanyHdrInfo>)HttpContext.Current.Session[SessionConstants.ALL_COMPANIES_LITE_OBJECT];
            if (oCompanyHdrInfoCollection == null)
            {
                // Get From Cache
                oCompanyHdrInfoCollection = (List<CompanyHdrInfo>)Helper.DeepClone(CacheHelper.GetAllCompaniesLiteObject());
                // Translate
                LanguageHelper.TranslateCompaniesCollection(oCompanyHdrInfoCollection);
                HttpContext.Current.Session[SessionConstants.ALL_COMPANIES_LITE_OBJECT] = oCompanyHdrInfoCollection;
            }
            return oCompanyHdrInfoCollection;
        }

        public static List<DashboardMstInfo> GetDashboards()
        {
            string key = SessionConstants.DASHBOARDS_BY_ROLE + "_" + SessionHelper.CurrentRoleID + "_" + SessionHelper.CurrentReconciliationPeriodID.GetValueOrDefault();
            List<DashboardMstInfo> oDashboardMstInfoCollection = (List<DashboardMstInfo>)HttpContext.Current.Session[key];
            if (oDashboardMstInfoCollection == null)
            {
                if (SessionHelper.CurrentReconciliationPeriodID != null)
                {
                    // fetch from DB
                    IRole oRoleClient = RemotingHelper.GetRoleObject();
                    oDashboardMstInfoCollection = oRoleClient.GetDashboardsByRole(SessionHelper.CurrentRoleID, SessionHelper.CurrentReconciliationPeriodID, Helper.GetAppUserInfo());
                    HttpContext.Current.Session[key] = oDashboardMstInfoCollection;
                }
            }
            return oDashboardMstInfoCollection;
        }

        /// <summary>
        /// Clear Role Specific Data from Session
        /// </summary>
        public static void ClearRoleDataFromSession()
        {
            SessionHelper.ClearSessionByPrefix(SessionConstants.DASHBOARDS_BY_ROLE);
            SessionHelper.ClearSession(SessionConstants.ALL_DATAIMPORTTYPE_LIST);
        }

        /// <summary>
        /// Clear Master Data from Session so that it is translated again
        /// </summary>
        public static void ClearMasterDataFromSession()
        {
            for (int i = HttpContext.Current.Session.Keys.Count - 1; i >= 0; i--)
            {
                if (HttpContext.Current.Session.Keys[i].StartsWith(SessionConstants.SESSION_KEY_MASTER_DATA_PREFIX))
                    HttpContext.Current.Session.RemoveAt(i);
            }
        }

        public static void ClearRecStatusFromSession()
        {
            HttpContext.Current.Session.Remove(SessionConstants.RECSTATUS_TYPES);
        }

        internal static List<ReconciliationStatusMstInfo> GetAllRecStatus()
        {
            List<ReconciliationStatusMstInfo> oRecStatusCollection = (List<ReconciliationStatusMstInfo>)HttpContext.Current.Session[SessionConstants.RECSTATUS_TYPES];
            if (oRecStatusCollection == null)
            {
                oRecStatusCollection = (List<ReconciliationStatusMstInfo>)Helper.DeepClone(CacheHelper.GetAllRecStatus());
                LanguageHelper.TranslateRecStatusTypes(oRecStatusCollection);
                HttpContext.Current.Session[SessionConstants.RECSTATUS_TYPES] = oRecStatusCollection;
            }
            return oRecStatusCollection;
        }

        internal static List<QualityScoreStatusMstInfo> GetAllQualityScoreStatuses()
        {
            List<QualityScoreStatusMstInfo> oQualityScoreStatusMstInfoList = (List<QualityScoreStatusMstInfo>)HttpContext.Current.Session[SessionConstants.QUALITY_SCORE_STATUSES];
            if (oQualityScoreStatusMstInfoList == null)
            {
                oQualityScoreStatusMstInfoList = (List<QualityScoreStatusMstInfo>)Helper.DeepClone(CacheHelper.GetAllQualityScoreStatuses());
                LanguageHelper.TranslateQualityScoreStatus(oQualityScoreStatusMstInfoList);
                HttpContext.Current.Session[SessionConstants.QUALITY_SCORE_STATUSES] = oQualityScoreStatusMstInfoList;
            }
            return oQualityScoreStatusMstInfoList;
        }

        internal static List<ReasonMstInfo> GetAllReasons()
        {
            List<ReasonMstInfo> oReasonMstInfoCollection = (List<ReasonMstInfo>)HttpContext.Current.Session[SessionConstants.REASON_TYPES];
            if (oReasonMstInfoCollection == null)
            {
                oReasonMstInfoCollection = (List<ReasonMstInfo>)Helper.DeepClone(CacheHelper.GetAllReasons());
                LanguageHelper.TranslateReasonTypes(oReasonMstInfoCollection);
                HttpContext.Current.Session[SessionConstants.REASON_TYPES] = oReasonMstInfoCollection;
            }
            return oReasonMstInfoCollection;
        }

        internal static List<SystemLockdownReasonMstInfo> GetAllSystemLockdownReasons()
        {
            string sessionKey = SessionConstants.SYSTEM_LOCKDOWN_REASONS;
            List<SystemLockdownReasonMstInfo> oSystemLockdownReasonMstInfoList = (List<SystemLockdownReasonMstInfo>)HttpContext.Current.Session[sessionKey];
            if (oSystemLockdownReasonMstInfoList == null)
            {
                oSystemLockdownReasonMstInfoList = (List<SystemLockdownReasonMstInfo>)Helper.DeepClone(CacheHelper.GetAllSystemLockdownReasons());
                LanguageHelper.TranslateLabelSystemLockdownReasons(oSystemLockdownReasonMstInfoList);
                HttpContext.Current.Session[sessionKey] = oSystemLockdownReasonMstInfoList;
            }
            return oSystemLockdownReasonMstInfoList;
        }

        public static List<SubledgerSourceInfo> GetAllSubLedgerSources()
        {
            string sessionKey = SessionConstants.COMPANY_SUBLEDGER_SOURCES;
            List<SubledgerSourceInfo> oSubledgerSourceInfoCollection = (List<SubledgerSourceInfo>)HttpContext.Current.Session[sessionKey];
            if (oSubledgerSourceInfoCollection == null)
            {
                ISubledger oSubledger = RemotingHelper.GetSubledgerObject();
                oSubledgerSourceInfoCollection = LanguageHelper.TranslateLabelSubledgerSource(oSubledger.SelectAllByCompanyID(SessionHelper.CurrentCompanyID.Value, Helper.GetAppUserInfo()));
                HttpContext.Current.Session[sessionKey] = oSubledgerSourceInfoCollection;
            }
            return oSubledgerSourceInfoCollection;
        }

        public static void ClearAllSubLedgerSources()
        {
            ClearSession(SessionConstants.COMPANY_SUBLEDGER_SOURCES);
        }

        //internal static List<RiskRatingMstInfo> GetAllRiskRating()
        //{
        //    List<RiskRatingMstInfo> oRiskRatingMstInfoCollection = (List<RiskRatingMstInfo>)HttpContext.Current.Session[SessionConstants.RISKRATING_TYPES];
        //    if (oRiskRatingMstInfoCollection == null)
        //    {
        //        oRiskRatingMstInfoCollection = CacheHelper.GetAllRiskRating();
        //        LanguageHelper.TranslateRiskRating(oRiskRatingMstInfoCollection);
        //        HttpContext.Current.Session[SessionConstants.RISKRATING_TYPES] = oRiskRatingMstInfoCollection;
        //    }
        //    return oRiskRatingMstInfoCollection;
        //}
        internal static List<ExceptionTypeMstInfo> GetAllExceptionTypes()
        {
            List<ExceptionTypeMstInfo> oExceptionTypeMstInfoCollection = (List<ExceptionTypeMstInfo>)HttpContext.Current.Session[SessionConstants.EXCEPTION_TYPES];
            if (oExceptionTypeMstInfoCollection == null)
            {
                oExceptionTypeMstInfoCollection = (List<ExceptionTypeMstInfo>)Helper.DeepClone(CacheHelper.GetAllExceptionTypes());
                // Translate
                LanguageHelper.TranslateExceptionTypes(oExceptionTypeMstInfoCollection);
                HttpContext.Current.Session[SessionConstants.EXCEPTION_TYPES] = oExceptionTypeMstInfoCollection;
            }

            return oExceptionTypeMstInfoCollection;
        }
        internal static List<AgingCategoryMstInfo> GetAllAgingCategories()
        {
            List<AgingCategoryMstInfo> oAgingCategoryCollection = (List<AgingCategoryMstInfo>)HttpContext.Current.Session[SessionConstants.AGING_TYPES];
            if (oAgingCategoryCollection == null)
            {
                oAgingCategoryCollection = (List<AgingCategoryMstInfo>)Helper.DeepClone(CacheHelper.GetAllAgingCategories());
                LanguageHelper.TranslateAgingTypes(oAgingCategoryCollection);
                HttpContext.Current.Session[SessionConstants.AGING_TYPES] = oAgingCategoryCollection;
            }

            return oAgingCategoryCollection;
        }

        internal static List<RangeOfScoreMstInfo> GetRangeOfScores()
        {
            List<RangeOfScoreMstInfo> oAgingCategoryCollection = (List<RangeOfScoreMstInfo>)HttpContext.Current.Session[SessionConstants.RANGEOFSCORES_TYPES];
            if (oAgingCategoryCollection == null)
            {
                oAgingCategoryCollection = (List<RangeOfScoreMstInfo>)Helper.DeepClone(CacheHelper.GetRangeOfScores());
                //LanguageHelper.TranslateAgingTypes(oAgingCategoryCollection);
                HttpContext.Current.Session[SessionConstants.RANGEOFSCORES_TYPES] = oAgingCategoryCollection;
            }

            return oAgingCategoryCollection;
        }

        internal static List<QualityScoreChecklistInfo> GetQualityScoreChecklist(int RecPeriodID)
        {
            List<QualityScoreChecklistInfo> oQualityScoreChecklist = (List<QualityScoreChecklistInfo>)HttpContext.Current.Session[SessionConstants.QUALITYSCORECHECKLIST_TYPES];
            if (oQualityScoreChecklist == null)
            {
                oQualityScoreChecklist = (List<QualityScoreChecklistInfo>)Helper.DeepClone(CacheHelper.GetQualityScoreChecklist(RecPeriodID));
                LanguageHelper.TranslateLabelsCompanyQualityScoreDataForReports(oQualityScoreChecklist);
                HttpContext.Current.Session[SessionConstants.QUALITYSCORECHECKLIST_TYPES] = oQualityScoreChecklist;
            }

            return oQualityScoreChecklist;
        }

        internal static List<ReconciliationCategoryMstInfo> GetOpenItemClassifications()
        {
            List<ReconciliationCategoryMstInfo> oRecCategoryInfoCollection = (List<ReconciliationCategoryMstInfo>)HttpContext.Current.Session[SessionConstants.OPENITEMCLASSIFICATION_TYPES];
            if (oRecCategoryInfoCollection == null)
            {
                oRecCategoryInfoCollection = (List<ReconciliationCategoryMstInfo>)Helper.DeepClone(CacheHelper.GetOpenItemClassifications());
                LanguageHelper.TranslateOpenItemClassificationTypes(oRecCategoryInfoCollection);
                HttpContext.Current.Session[SessionConstants.OPENITEMCLASSIFICATION_TYPES] = oRecCategoryInfoCollection;
            }
            return oRecCategoryInfoCollection;
        }
        internal static List<CertificationStatusMstInfo> GetCertificationStatus()
        {
            List<CertificationStatusMstInfo> oCertStatusMstInfoCollection = (List<CertificationStatusMstInfo>)HttpContext.Current.Session[SessionConstants.CERTIFICATION_STATUS_TYPE];
            if (oCertStatusMstInfoCollection == null)
            {
                oCertStatusMstInfoCollection = (List<CertificationStatusMstInfo>)Helper.DeepClone(CacheHelper.GetCertificationStatus());
                LanguageHelper.TranslateCertficationStatus(oCertStatusMstInfoCollection);
                HttpContext.Current.Session[SessionConstants.CERTIFICATION_STATUS_TYPE] = oCertStatusMstInfoCollection;
            }
            return oCertStatusMstInfoCollection;
        }

        public static void ClearAllRolesFromSession()
        {
            HttpContext.Current.Session.Remove(SessionConstants.USER_ROLES);
        }

        public static void ClearAllUserRolesFromSession()
        {
            HttpContext.Current.Session.Remove(SessionConstants.USER_ROLE);
        }
        public static List<RoleMstInfo> GetAllRoles()
        {
            List<RoleMstInfo> oRoleMstInfoCollection = null;
            oRoleMstInfoCollection = (List<RoleMstInfo>)HttpContext.Current.Session[SessionConstants.USER_ROLES];

            if (oRoleMstInfoCollection == null)
            {
                oRoleMstInfoCollection = (List<RoleMstInfo>)Helper.DeepClone(CacheHelper.GetAllRoles());
                LanguageHelper.TranslateUserRoles(oRoleMstInfoCollection);
                HttpContext.Current.Session[SessionConstants.USER_ROLES] = oRoleMstInfoCollection;
            }

            return oRoleMstInfoCollection;
        }

        public static List<CompanyCapabilityInfo> GetCompanyCapabilityCollectionForRecPeriodID(int recPeriodID)
        {
            Dictionary<Int32, List<CompanyCapabilityInfo>> dictCapabilities = null;
            string sessionKey = SessionConstants.REC_PERIOD_CAPABILITY_COLLECTION;
            dictCapabilities = (Dictionary<Int32, List<CompanyCapabilityInfo>>)HttpContext.Current.Session[sessionKey];
            List<CompanyCapabilityInfo> oCompanyCapabilityInfoCollection = null;

            if (dictCapabilities == null)
            {
                dictCapabilities = new Dictionary<int, List<CompanyCapabilityInfo>>();
            }

            if (dictCapabilities.ContainsKey(recPeriodID))
            {
                oCompanyCapabilityInfoCollection = dictCapabilities[recPeriodID];
            }
            else
            {
                // Get Capbaility Data From DB based on Rec Period
                ICompany oCompanyClient = RemotingHelper.GetCompanyObject();
                oCompanyCapabilityInfoCollection = oCompanyClient.SelectAllCompanyCapabilityByReconciliationID(recPeriodID, Helper.GetAppUserInfo());

                // add to Dictionary and Session
                dictCapabilities[recPeriodID] = oCompanyCapabilityInfoCollection;
                HttpContext.Current.Session[sessionKey] = dictCapabilities;
            }
            return oCompanyCapabilityInfoCollection;
        }

        public static string GetSessionKeyForRecPeriodData(string sessionKey, int recPeriod)
        {
            return sessionKey + "_" + recPeriod.ToString();
        }

        public static List<GridColumnInfo> GetGridPreference(ARTEnums.Grid eGridType)
        {
            // Show Columns Based on User Personalization
            string sessionKey = SessionHelper.GetSessionKeyForGridCustomization(eGridType);
            List<GridColumnInfo> oGridColumnInfoCollection = (List<GridColumnInfo>)HttpContext.Current.Session[sessionKey];

            if (oGridColumnInfoCollection == null)
            {
                // fetch from DB
                IUser oUserClient = RemotingHelper.GetUserObject();
                oGridColumnInfoCollection = oUserClient.GetGridPrefernce(SessionHelper.CurrentUserID, eGridType, SessionHelper.CurrentReconciliationPeriodID, Helper.GetAppUserInfo());

                // Store into Session
                HttpContext.Current.Session[sessionKey] = oGridColumnInfoCollection;
            }

            return oGridColumnInfoCollection;
        }


        public static IList<WeekDayMstInfo> GetAllWeekDays()
        {
            IList<WeekDayMstInfo> oWeekDayMstInfo = null;
            if (HttpContext.Current.Session[SessionConstants.ALL_WEEKDAYS_LIST] != null)
            {
                oWeekDayMstInfo = (IList<WeekDayMstInfo>)HttpContext.Current.Session[SessionConstants.ALL_WEEKDAYS_LIST];
            }
            else
            {

                oWeekDayMstInfo = (List<WeekDayMstInfo>)Helper.DeepClone(CacheHelper.GetAllWeekDays());
                oWeekDayMstInfo = LanguageHelper.TranslateLabeloWeekDayMstInfo(oWeekDayMstInfo);
                HttpContext.Current.Session.Add(SessionConstants.ALL_WEEKDAYS_LIST, oWeekDayMstInfo);
            }
            return oWeekDayMstInfo;
        }

        public static IList<MappingUploadMasterInfo> GetAllMappingUploadKeys()
        {
            IList<MappingUploadMasterInfo> oMappingUploadMstInfo = null;
            if (HttpContext.Current.Session[SessionConstants.ALL_MAPPINGUPLOADKEY_LIST] != null)
            {
                oMappingUploadMstInfo = (IList<MappingUploadMasterInfo>)HttpContext.Current.Session[SessionConstants.ALL_WEEKDAYS_LIST];
            }
            else
            {

                oMappingUploadMstInfo = (List<MappingUploadMasterInfo>)Helper.DeepClone(CacheHelper.GetAllMappingUploadKeys());
                oMappingUploadMstInfo = LanguageHelper.TranslateLabeloMappingUploadMstInfo(oMappingUploadMstInfo);
                HttpContext.Current.Session.Add(SessionConstants.ALL_MAPPINGUPLOADKEY_LIST, oMappingUploadMstInfo);
            }
            return oMappingUploadMstInfo;
        }

        public static void ClearCompanyHdrInfoFromSession()
        {
            HttpContext.Current.Session.Remove(SessionConstants.CURRENT_COMPANY_HDR_INFO);
            ClearFTPServerList();
        }

        public static CompanyHdrInfo GetCurrentCompanyHdrInfo()
        {

            CompanyHdrInfo oCompanyHdrInfo = null;

            oCompanyHdrInfo = (CompanyHdrInfo)HttpContext.Current.Session[SessionConstants.CURRENT_COMPANY_HDR_INFO];

            if (oCompanyHdrInfo == null)
            {
                ICompany oCompanyClient = RemotingHelper.GetCompanyObject();
                oCompanyHdrInfo = oCompanyClient.GetCompanyDetail(SessionHelper.CurrentCompanyID, SessionHelper.CurrentReconciliationPeriodEndDate, Helper.GetAppUserInfo());
                HttpContext.Current.Session.Add(SessionConstants.CURRENT_COMPANY_HDR_INFO, oCompanyHdrInfo);
            }
            return oCompanyHdrInfo;
        }

        /// <summary>
        /// Get All Company Settings for the Current Company
        /// </summary>
        /// <returns></returns>
        public static IList<CompanySettingInfo> SelectAllCompanySettingsForCurrentCompany()
        {
            IList<CompanySettingInfo> oCompanySettingInfoList = (IList<CompanySettingInfo>)HttpContext.Current.Session[SessionConstants.CURRENT_COMPANY_SETTINGS];
            if (oCompanySettingInfoList == null)
            {
                try
                {
                    // Get from DB 
                    ICompany oCompanyClient = RemotingHelper.GetCompanyObject();
                    oCompanySettingInfoList = oCompanyClient.SelectAllCompanySettingByCompanyID(SessionHelper.CurrentCompanyID.Value, Helper.GetAppUserInfo());
                    HttpContext.Current.Session.Add(SessionConstants.CURRENT_COMPANY_SETTINGS, oCompanySettingInfoList);
                }
                catch (Exception ex)
                {
                    Helper.FormatAndShowErrorMessage(null, ex);
                }
            }
            return oCompanySettingInfoList;
        }

        /// <summary>
        /// Clear Company Settings from Session
        /// </summary>
        public static void RefreshCompanySettingsInSession()
        {
            HttpContext.Current.Session.Remove(SessionConstants.CURRENT_COMPANY_SETTINGS);
            SelectAllCompanySettingsForCurrentCompany();
        }

        #region Package
        public static List<PackageMstInfo> GetAllPackage()
        {
            List<PackageMstInfo> oPackageMstInfoCollection = null;
            if (HttpContext.Current.Session[SessionConstants.ALL_PACKAGE_LIST] != null)
            {
                oPackageMstInfoCollection = (List<PackageMstInfo>)HttpContext.Current.Session[SessionConstants.ALL_PACKAGE_LIST];
            }
            else
            {
                oPackageMstInfoCollection = CacheHelper.GetAllPackage();
                HttpContext.Current.Session[SessionConstants.ALL_PACKAGE_LIST] = oPackageMstInfoCollection;
            }
            return oPackageMstInfoCollection;
        }
        #endregion

        #region JEWriteOffOnApprover

        public static List<UserHdrInfo> GetAllJEWriteOffOnApprover()
        {
            List<UserHdrInfo> oUserHdrInfoCollection = null;

            if (HttpContext.Current.Session[SessionConstants.SESSION_KEY_JE_WRITE_OFF_ON_APPROVER] != null)
            {
                oUserHdrInfoCollection = (List<UserHdrInfo>)HttpContext.Current.Session[SessionConstants.SESSION_KEY_JE_WRITE_OFF_ON_APPROVER];
            }
            else
            {
                IJournalEntry oJournalEntryClient = RemotingHelper.GetJournalEntryObject();

                oUserHdrInfoCollection = oJournalEntryClient.SelectWriteOffOnApproversByCompanyID(SessionHelper.CurrentCompanyID, Helper.GetAppUserInfo());

                HttpContext.Current.Session[SessionConstants.SESSION_KEY_JE_WRITE_OFF_ON_APPROVER] = oUserHdrInfoCollection;
            }

            return oUserHdrInfoCollection;
        }

        #endregion

        #region Matching
        public static MatchSetHdrInfo GetCurrentMatchSet()
        {
            MatchSetHdrInfo oMatchSetHdrInfo = null;
            if (HttpContext.Current.Session[SessionConstants.MATCH_SET] != null)
            {
                oMatchSetHdrInfo = (MatchSetHdrInfo)HttpContext.Current.Session[SessionConstants.MATCH_SET];

            }

            return oMatchSetHdrInfo;

        }

        public static void SetCurrentMatchSet(MatchSetHdrInfo oMatchSetHdrInfo)
        {
            if (oMatchSetHdrInfo != null)
            {
                HttpContext.Current.Session[SessionConstants.MATCH_SET] = oMatchSetHdrInfo;
            }

        }

        public static void ClearCurrentMatchSet()
        {
            HttpContext.Current.Session.Remove(SessionConstants.MATCH_SET);
        }

        public static string GetMatchingRuleSessionKey()
        {
            return SessionConstants.MATCHING_RULE_SETUP;
        }

        #endregion

        #region "Matching Source Types"
        /// <summary>
        /// Gets all Matching Source Types
        /// </summary>
        /// <returns>list of Matching Source Type Info object</returns>
        public static List<MatchingSourceTypeInfo> GeAlltMatchingSourceType()
        {
            List<MatchingSourceTypeInfo> lstMatchingSourceTypeCollection = (List<MatchingSourceTypeInfo>)HttpContext.Current.Session[SessionConstants.MATCH_SOURCE_TYPE];
            if (lstMatchingSourceTypeCollection == null)
            {
                lstMatchingSourceTypeCollection = (List<MatchingSourceTypeInfo>)Helper.DeepClone(CacheHelper.GetMatchingSourceType());
                LanguageHelper.TranslateLabelMatchingSourceTypeInfoList(lstMatchingSourceTypeCollection);
                HttpContext.Current.Session.Add(SessionConstants.MATCH_SOURCE_TYPE, lstMatchingSourceTypeCollection);
            }
            return lstMatchingSourceTypeCollection;
        }
        #endregion

        #region "Data Types"
        /// <summary>
        /// Gets all Data Types
        /// </summary>
        /// <returns>list of Data Type Info object</returns>
        public static List<DataTypeMstInfo> GeAllDataType()
        {
            List<DataTypeMstInfo> lstDataTypeCollection = (List<DataTypeMstInfo>)HttpContext.Current.Session[SessionConstants.ALL_DATA_TYPE];
            if (lstDataTypeCollection == null)
            {
                lstDataTypeCollection = (List<DataTypeMstInfo>)Helper.DeepClone(CacheHelper.GetAllDataType());
                LanguageHelper.TranslateLabelDataTypeMstInfoList(lstDataTypeCollection);
                HttpContext.Current.Session.Add(SessionConstants.ALL_DATA_TYPE, lstDataTypeCollection);
            }
            return lstDataTypeCollection;
        }
        #endregion

        #region "Data Types"
        /// <summary>
        /// GetKeyFieldsByCompanyID
        /// </summary>
        /// <returns>list of Data Type Info object</returns>
        public static string GetKeyFieldsByCompanyID()
        {
            string KeyFields = "";
            if (HttpContext.Current.Session[SessionConstants.ALL_KEY_FIELDS] != null)
                KeyFields = HttpContext.Current.Session[SessionConstants.ALL_KEY_FIELDS].ToString();

            if (KeyFields == "")
            {
                IMatching oMatchingClient = RemotingHelper.GetMatchingObject();
                MatchingParamInfo oMatchingParamInfo = new MatchingParamInfo();
                oMatchingParamInfo.CompanyID = (int)HttpContext.Current.Session[SessionConstants.CURRENT_COMPANY_ID];
                KeyFields = oMatchingClient.GetKeyFieldsByCompanyID(oMatchingParamInfo, Helper.GetAppUserInfo());
                HttpContext.Current.Session[SessionConstants.ALL_KEY_FIELDS] = KeyFields;
            }
            return KeyFields;
        }
        #endregion

        #region Dynamic Filter


        /// <summary>
        /// Sets the dynamic filter columns By Specific Key.
        /// </summary>
        /// <param name="oFilterInfoList">The o filter info list.</param>
        public static void SetDynamicFilterColumns(List<FilterInfo> oFilterInfoList, String SessionKey)
        {
            HttpContext.Current.Session[SessionConstants.GRID_DYNAMIC_FILTER_INFO + SessionKey] = oFilterInfoList;
            ClearDynamicFilterData(SessionKey);
        }

        /// <summary>
        /// Gets the dynamic filter columns By Specific Key.
        /// </summary>
        /// <returns></returns>
        public static List<FilterInfo> GetDynamicFilterColumns(String SessionKey)
        {
            return (List<FilterInfo>)HttpContext.Current.Session[SessionConstants.GRID_DYNAMIC_FILTER_INFO + SessionKey];
        }

        /// <summary>
        /// Clears the dynamic filter data.
        /// </summary>
        public static void ClearDynamicFilterData(String SessionKey)
        {
            HttpContext.Current.Session[SessionConstants.GRID_FILTER_CLAUSE + SessionKey] = null;
            HttpContext.Current.Session[SessionConstants.GRID_DYNAMIC_FILTER_CRITERIA + SessionKey] = null;
        }
        /// <summary>
        /// Gets the dynamic filter result By Session Key.
        /// </summary>
        /// <returns></returns>
        public static List<string> GetDynamicFilterResult(String SessionKey)
        {
            return (List<string>)HttpContext.Current.Session[SessionConstants.GRID_FILTER_CLAUSE + SessionKey];
        }

        /// <summary>
        /// Gets the dynamic filter where clause By Session Key.
        /// </summary>
        /// <returns></returns>
        public static string GetDynamicFilterResultWhereClause(String SessionKey)
        {
            List<string> stringList = GetDynamicFilterResult(SessionKey);
            if (stringList != null && stringList.Count > 0)
                return stringList[1];
            return null;
        }

        /// <summary>
        /// Gets the dynamic filter result string By Session Key.
        /// </summary>
        /// <returns></returns>
        public static string GetDynamicFilterResultString(String SessionKe)
        {
            List<string> stringList = GetDynamicFilterResult(SessionKe);
            if (stringList != null && stringList.Count > 0)
                return stringList[0];
            return null;
        }
        /// <summary>
        /// Shows the filter icon by Session keys.
        /// </summary>
        /// <param name="oExRadGrid">The o ex RAD grid.</param>
        public static void ShowGridFilterIcon(PageBase oPageBase, ExRadGrid oExRadGrid, GridItemEventArgs e, String SessionKey)
        {
            if (oPageBase != null && oExRadGrid != null && e != null && e.Item != null && e.Item.ItemType == GridItemType.Header)
            {
                List<FilterCriteria> oFilterCriteriaList = GetDynamicFilterCriteria(SessionKey);
                if (oFilterCriteriaList != null && oFilterCriteriaList.Count > 0)
                {
                    foreach (FilterCriteria oFilterCriteria in oFilterCriteriaList)
                    {
                        Control oControl = (e.Item as GridHeaderItem)[oFilterCriteria.ColumnName].Controls[0];
                        if (oControl != null)
                        {
                            string filterImageUrl = oPageBase.ResolveUrl("~/App_Themes/" + oPageBase.Theme + "/Images/FilterIcon.gif");
                            string imgTag = "<img src='" + filterImageUrl + "' border='0' />";
                            if (oControl is LinkButton)
                            {
                                LinkButton oLinkButton = (LinkButton)oControl;
                                oLinkButton.Text = oLinkButton.Text + imgTag;

                            }
                            else
                            {
                                if (oControl is LiteralControl)
                                {
                                    LiteralControl oLiteralControl = (LiteralControl)oControl;
                                    oLiteralControl.Text = oLiteralControl.Text + imgTag;

                                }
                            }
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Shows the filter icon by Session keys for popup page base.
        /// </summary>
        /// <param name="oExRadGrid">The o ex RAD grid.</param>
        public static void ShowGridFilterIcon(PopupPageBase oPageBase, ExRadGrid oExRadGrid, GridItemEventArgs e, String SessionKey)
        {
            if (oPageBase != null && oExRadGrid != null && e != null && e.Item != null && e.Item.ItemType == GridItemType.Header)
            {
                List<FilterCriteria> oFilterCriteriaList = GetDynamicFilterCriteria(SessionKey);
                if (oFilterCriteriaList != null && oFilterCriteriaList.Count > 0)
                {
                    foreach (FilterCriteria oFilterCriteria in oFilterCriteriaList)
                    {
                        Control oControl = (e.Item as GridHeaderItem)[oFilterCriteria.ColumnName].Controls[0];
                        if (oControl != null)
                        {
                            string filterImageUrl = oPageBase.ResolveUrl("~/App_Themes/" + oPageBase.Theme + "/Images/FilterIcon.gif");
                            string imgTag = "<img src='" + filterImageUrl + "' border='0' />";
                            if (oControl is LinkButton)
                            {
                                LinkButton oLinkButton = (LinkButton)oControl;
                                oLinkButton.Text = oLinkButton.Text + imgTag;

                            }
                            else
                            {
                                if (oControl is LiteralControl)
                                {
                                    LiteralControl oLiteralControl = (LiteralControl)oControl;
                                    oLiteralControl.Text = oLiteralControl.Text + imgTag;

                                }
                            }
                        }
                    }
                }
            }
        }

        #endregion

        #region "Task Master"
        internal static List<TaskCategoryMstInfo> GetTaskCategory()
        {
            List<TaskCategoryMstInfo> oTaskCategoryMstInfoList = (List<TaskCategoryMstInfo>)HttpContext.Current.Session[SessionConstants.TASK_CATEGORY];
            if (oTaskCategoryMstInfoList == null)
            {
                oTaskCategoryMstInfoList = (List<TaskCategoryMstInfo>)Helper.DeepClone(CacheHelper.GetTaskCategory());
                LanguageHelper.TranslateTaskCategoryType(oTaskCategoryMstInfoList);
                HttpContext.Current.Session[SessionConstants.TASK_CATEGORY] = oTaskCategoryMstInfoList;
            }
            return oTaskCategoryMstInfoList;
        }
        internal static List<TaskStatusMstInfo> GetTaskStatus()
        {
            List<TaskStatusMstInfo> oTaskStatusInfoList = (List<TaskStatusMstInfo>)HttpContext.Current.Session[SessionConstants.TASK_STATUS];
            if (oTaskStatusInfoList == null)
            {
                oTaskStatusInfoList = (List<TaskStatusMstInfo>)Helper.DeepClone(CacheHelper.GetTaskStatus());
                LanguageHelper.TranslateTaskStatusType(oTaskStatusInfoList);
                HttpContext.Current.Session[SessionConstants.TASK_STATUS] = oTaskStatusInfoList;
            }
            return oTaskStatusInfoList;
        }
        internal static List<TaskTypeMstInfo> GetAllTaskType()
        {
            List<TaskTypeMstInfo> oTaskTypeMstInfoList = (List<TaskTypeMstInfo>)HttpContext.Current.Session[SessionConstants.ALL_TASK_TYPE];
            if (oTaskTypeMstInfoList == null)
            {
                oTaskTypeMstInfoList = (List<TaskTypeMstInfo>)Helper.DeepClone(CacheHelper.GetAllTaskType());
                LanguageHelper.TranslateTaskType(oTaskTypeMstInfoList);
                HttpContext.Current.Session[SessionConstants.ALL_TASK_TYPE] = oTaskTypeMstInfoList;
            }
            return oTaskTypeMstInfoList;
        }
        #endregion

        #region Report Column

        public static List<ReportColumnInfo> GetReportColumnInfoList(Int16 reportID, bool? IsOptional)
        {
            string key = SessionConstants.REPORT_COLUMNS + "_" + reportID.ToString();
            if (IsOptional.HasValue)
                key += "_" + IsOptional.GetValueOrDefault().ToString();

            List<ReportColumnInfo> oReportColumnInfoList = (List<ReportColumnInfo>)HttpContext.Current.Session[key];
            if (oReportColumnInfoList == null)
            {
                try
                {
                    // Get from Cache 
                    oReportColumnInfoList = (List<ReportColumnInfo>)Helper.DeepClone(CacheHelper.GetReportColumnInfoList(reportID, IsOptional));
                    oReportColumnInfoList = LanguageHelper.TranslateLabelReportColumnInfo(oReportColumnInfoList);
                    oReportColumnInfoList = UpdateReportColumnsForCustomFields(reportID, oReportColumnInfoList);
                    HttpContext.Current.Session.Add(key, oReportColumnInfoList);
                }
                catch (Exception ex)
                {
                    Helper.FormatAndShowErrorMessage(null, ex);
                }
            }
            return oReportColumnInfoList;
        }

        public static void ClearSessionReportColumnInfoList(Int16 reportID, bool? IsOptional)
        {
            string key = SessionConstants.REPORT_COLUMNS + "_" + reportID.ToString();
            if (IsOptional.HasValue)
                key += "_" + IsOptional.GetValueOrDefault().ToString();
            if (HttpContext.Current.Session[key] != null)
                HttpContext.Current.Session.Remove(key);
        }
        public static List<ReportColumnInfo> UpdateReportColumnsForCustomFields(Int16 reportID, List<ReportColumnInfo> oReportColumnInfoList)
        {
            if ((short)WebEnums.Reports.TASK_COMPLETION_REPORT == reportID)
            {
                List<TaskCustomFieldInfo> oTaskCustomFieldInfoList = TaskMasterHelper.GetTaskCustomFields(SessionHelper.CurrentReconciliationPeriodID, SessionHelper.CurrentCompanyID);
                if (oTaskCustomFieldInfoList != null && oTaskCustomFieldInfoList.Count > 0 && oReportColumnInfoList != null && oReportColumnInfoList.Count > 0)
                {
                    foreach (TaskCustomFieldInfo oTaskCustomFieldInfo in oTaskCustomFieldInfoList)
                    {
                        int columnID = 0;
                        switch ((WebEnums.TaskCustomField)oTaskCustomFieldInfo.TaskCustomFieldID)
                        {
                            case WebEnums.TaskCustomField.CustomField1:
                                columnID = 64;
                                break;
                            case WebEnums.TaskCustomField.CustomField2:
                                columnID = 65;
                                break;
                        }
                        ReportColumnInfo oReportColumnInfo = oReportColumnInfoList.Find(T => T.ColumnID == columnID);
                        if (oReportColumnInfo != null && !string.IsNullOrEmpty(oTaskCustomFieldInfo.CustomFieldValue))
                            oReportColumnInfo.ColumnName = oTaskCustomFieldInfo.CustomFieldValue;
                    }
                }
            }
            return oReportColumnInfoList;
        }

        #endregion

        public static string GetSessionKeyForPageSetting(short PageID)
        {
            string sessionKey = "";
            sessionKey = SessionConstants.PAGE_SETTINGS + "_" + PageID.ToString();
            return sessionKey;
        }
        public static short GetCurrentDualLevelReviewType()
        {
            short DualLevelReviewTypeID = 0;
            Dictionary<int, short> DicDualLevelReviewType = new Dictionary<int, short>();
            if (HttpContext.Current.Session[GetSessionKeyForDualLevelReviewType()] != null)
            {
                DicDualLevelReviewType = (Dictionary<int, short>)HttpContext.Current.Session[GetSessionKeyForDualLevelReviewType()];
                if (DicDualLevelReviewType.ContainsKey(CurrentReconciliationPeriodID.GetValueOrDefault()))
                    DualLevelReviewTypeID = (short)DicDualLevelReviewType[CurrentReconciliationPeriodID.GetValueOrDefault()];
                else
                {
                    IList<CompanySettingInfo> oCompanySettingInfoCollection = new List<CompanySettingInfo>();
                    ICompany oCompanyClient = RemotingHelper.GetCompanyObject();
                    oCompanySettingInfoCollection = oCompanyClient.SelectCompanyDualLevelReviewType(SessionHelper.CurrentReconciliationPeriodID, Helper.GetAppUserInfo());
                    if (oCompanySettingInfoCollection.Count > 0 && oCompanySettingInfoCollection[0].DualLevelReviewTypeID.HasValue)
                    {
                        DualLevelReviewTypeID = oCompanySettingInfoCollection[0].DualLevelReviewTypeID.Value;
                        DicDualLevelReviewType.Add(CurrentReconciliationPeriodID.GetValueOrDefault(), DualLevelReviewTypeID);
                        HttpContext.Current.Session[GetSessionKeyForDualLevelReviewType()] = DicDualLevelReviewType;
                    }
                }
            }
            else
            {
                IList<CompanySettingInfo> oCompanySettingInfoCollection = new List<CompanySettingInfo>();
                ICompany oCompanyClient = RemotingHelper.GetCompanyObject();
                oCompanySettingInfoCollection = oCompanyClient.SelectCompanyDualLevelReviewType(SessionHelper.CurrentReconciliationPeriodID, Helper.GetAppUserInfo());
                if (oCompanySettingInfoCollection.Count > 0 && oCompanySettingInfoCollection[0].DualLevelReviewTypeID.HasValue)
                {
                    DualLevelReviewTypeID = oCompanySettingInfoCollection[0].DualLevelReviewTypeID.Value;
                    DicDualLevelReviewType.Add(CurrentReconciliationPeriodID.GetValueOrDefault(), DualLevelReviewTypeID);
                    HttpContext.Current.Session[GetSessionKeyForDualLevelReviewType()] = DicDualLevelReviewType;
                }
            }
            return DualLevelReviewTypeID;
        }
        public static string GetSessionKeyForDualLevelReviewType()
        {
            string sessionKey = "";
            sessionKey = SessionConstants.CURRENT_DUAL_LEVEL_REVIEW_TYPE;
            return sessionKey;
        }

        public static IList<DueDaysBasisInfo> GetAllDueDaysBasisType()
        {
            IList<DueDaysBasisInfo> oDueDaysBasisInfoList = null;
            if (HttpContext.Current.Session[SessionConstants.ALL_DUE_DAYS_BASIS_LIST] != null)
            {
                oDueDaysBasisInfoList = (IList<DueDaysBasisInfo>)HttpContext.Current.Session[SessionConstants.ALL_DUE_DAYS_BASIS_LIST];
            }
            else
            {
                oDueDaysBasisInfoList = (List<DueDaysBasisInfo>)Helper.DeepClone(CacheHelper.GetAllDueDaysBasisType());
                oDueDaysBasisInfoList = LanguageHelper.TranslateLabelDueDaysBasisInfo(oDueDaysBasisInfoList);
                HttpContext.Current.Session.Add(SessionConstants.ALL_DUE_DAYS_BASIS_LIST, oDueDaysBasisInfoList);
            }
            return oDueDaysBasisInfoList;
        }

        public static IList<DaysTypeInfo> GetAllDaysType()
        {
            IList<DaysTypeInfo> oDaysTypeInfoList = null;
            if (HttpContext.Current.Session[SessionConstants.ALL_DAYS_TYPE_LIST] != null)
            {
                oDaysTypeInfoList = (IList<DaysTypeInfo>)HttpContext.Current.Session[SessionConstants.ALL_DAYS_TYPE_LIST];
            }
            else
            {
                oDaysTypeInfoList = (List<DaysTypeInfo>)Helper.DeepClone(CacheHelper.GetAllDaysType());
                oDaysTypeInfoList = LanguageHelper.TranslateLabelDaysTypeInfo(oDaysTypeInfoList);
                HttpContext.Current.Session.Add(SessionConstants.ALL_DAYS_TYPE_LIST, oDaysTypeInfoList);
            }
            return oDaysTypeInfoList;
        }

        public static List<CompanyAlertInfo> SelectComapnyAlertByCompanyIDAndRecPeriodID()
        {
            List<CompanyAlertInfo> oCompanyAlertInfoCollection = null;
            if (HttpContext.Current.Session[SessionConstants.ALL_COMPANY_ALERT_LIST] != null)
            {
                oCompanyAlertInfoCollection = (List<CompanyAlertInfo>)HttpContext.Current.Session[SessionConstants.ALL_COMPANY_ALERT_LIST];
            }
            else
            {
                IAlert oAlertClient = RemotingHelper.GetAlertObject();
                oCompanyAlertInfoCollection = oAlertClient.SelectComapnyAlertByCompanyIDAndRecPeriodID(SessionHelper.CurrentCompanyID.Value, SessionHelper.CurrentReconciliationPeriodID, Helper.GetAppUserInfo());
                if (oCompanyAlertInfoCollection != null && oCompanyAlertInfoCollection.Count > 0)
                    HttpContext.Current.Session.Add(SessionConstants.ALL_COMPANY_ALERT_LIST, oCompanyAlertInfoCollection);
            }
            return oCompanyAlertInfoCollection;
        }

        public static List<OperatorMstInfo> GetOperatorList()
        {
            List<OperatorMstInfo> oOperatorCollection = (List<OperatorMstInfo>)HttpContext.Current.Session[SessionConstants.ALL_OPERATOR_LIST];
            if (oOperatorCollection == null)
            {
                // Get From Cache
                oOperatorCollection = (List<OperatorMstInfo>)Helper.DeepClone(CacheHelper.GetOperatorList());
                // Translate
                LanguageHelper.TranslateOperatorInfo(oOperatorCollection);
                HttpContext.Current.Session[SessionConstants.ALL_OPERATOR_LIST] = oOperatorCollection;
            }
            return oOperatorCollection;
        }

        public static List<RCCValidationTypeMstInfo> GetAllRCCLValidationType()
        {
            List<RCCValidationTypeMstInfo> oRCCValidationTypeMstInfoList = null;
            if (HttpContext.Current.Session[SessionConstants.ALL_RCCL_VALIDATION_TYPE_LIST] != null)
            {
                oRCCValidationTypeMstInfoList = (List<RCCValidationTypeMstInfo>)HttpContext.Current.Session[SessionConstants.ALL_RCCL_VALIDATION_TYPE_LIST];
            }
            else
            {

                oRCCValidationTypeMstInfoList = RecControlCheckListHelper.GetRCCValidationTypeMstInfoList();
                oRCCValidationTypeMstInfoList = LanguageHelper.TranslateLabelRCCValidationTypeMstInfo(oRCCValidationTypeMstInfoList);
                HttpContext.Current.Session.Add(SessionConstants.ALL_RCCL_VALIDATION_TYPE_LIST, oRCCValidationTypeMstInfoList);
            }
            return oRCCValidationTypeMstInfoList;
        }

        public static List<ImportFieldMstInfo> GetFieldsMst(int CompanyID, short? DataImportTypeID)
        {

            IDataImport oDataImportClient = RemotingHelper.GetDataImportObject();
            List<ImportFieldMstInfo> oImportFieldMstInfoCollection = oDataImportClient.GetFieldsMst(CompanyID, DataImportTypeID, Helper.GetAppUserInfo());
            LanguageHelper.TranslateImportFieldCollection(oImportFieldMstInfoCollection);
            return oImportFieldMstInfoCollection;
        }

        public static List<DataImportMessageInfo> GetDataImportMessageList()
        {
            List<DataImportMessageInfo> oDataImportMessageInfoCollection = (List<DataImportMessageInfo>)HttpContext.Current.Session[SessionConstants.ALL_DATAIMPORTMESSAGE_LIST];
            if (oDataImportMessageInfoCollection == null)
            {
                // Get From Cache
                oDataImportMessageInfoCollection = (List<DataImportMessageInfo>)Helper.DeepClone(CacheHelper.GetDataImportMessageList());
                // Translate
                LanguageHelper.TranslateLabelDataImportMessageLst(oDataImportMessageInfoCollection);
                HttpContext.Current.Session[SessionConstants.ALL_DATAIMPORTMESSAGE_LIST] = oDataImportMessageInfoCollection;
            }
            return oDataImportMessageInfoCollection;
        }
        public static List<FTPServerInfo> GetAllFTPServerObject(int? CompanyId)
        {
            List<FTPServerInfo> oFTPServerInfoCollection = null;
            if (HttpContext.Current.Session[SessionConstants.ALL_FTPSERVER_LIST] != null)
            {
                oFTPServerInfoCollection = (List<FTPServerInfo>)HttpContext.Current.Session[SessionConstants.ALL_FTPSERVER_LIST];
            }
            else
            {
                ICompany oCompanyClient = RemotingHelper.GetCompanyObject();
                oFTPServerInfoCollection = oCompanyClient.GetAllFTPServerListObject(CompanyId, Helper.GetAppUserInfo());
                HttpContext.Current.Session.Add(SessionConstants.ALL_FTPSERVER_LIST, oFTPServerInfoCollection);
            }
            return oFTPServerInfoCollection;
        }

        public static void ClearFTPServerList()
        {
            if (HttpContext.Current.Session[SessionConstants.ALL_FTPSERVER_LIST] != null)
                HttpContext.Current.Session.Remove(SessionConstants.ALL_FTPSERVER_LIST);
        }
        public static void ClearTaskCustomFieldInfoList()
        {
            if (HttpContext.Current.Session[SessionConstants.ALL_TASK_CUSTOM_FIELD_INFO_LIST] != null)
                HttpContext.Current.Session.Remove(SessionConstants.ALL_TASK_CUSTOM_FIELD_INFO_LIST);
        }


    }//end of namespace
}//end of class