using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using SkyStem.ART.Client.Model;
using SkyStem.ART.App.DAO;
using SkyStem.ART.Client.IServices;
using System.Data;
using System.Data.SqlClient;
using SkyStem.ART.App.Utility;
using SkyStem.ART.Client.Exception;
using SkyStem.ART.App.DAO.Report;
using SkyStem.ART.Client.Model.Report;
using SkyStem.ART.Client.Params;
using SkyStem.ART.App.DAO.QualityScore;

namespace SkyStem.ART.App.Services
{
    // NOTE: If you change the class name "Report" here, you must also update the reference to "Report" in Web.config.
    public class Report : IReport
    {
        const string parametersString_REPORT = "Currency: Reporting Currency| ISKeyAccount: Yes| RiskRating: High| Period: 2009-10-01| IsMaterial: Yes| Reason: | Entity: |Account: 0051000000 , 0101000000 , 0103000000 , 4221000000 ";

        public void DoWork()
        {
        }

        public List<RoleMandatoryReportInfo> SelectAllRoleMandatoryReportByReconciliationPeriodID(int? reconciliationPeriodID, AppUserInfo oAppUserInfo)
        {

            List<RoleMandatoryReportInfo> oRoleMandatoryReportInfoCollection = null;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                RoleMandatoryReportDAO oRoleMandatoryReportDAO = new RoleMandatoryReportDAO(oAppUserInfo);
                oRoleMandatoryReportInfoCollection = (List<RoleMandatoryReportInfo>)oRoleMandatoryReportDAO.SelectAllRoleMandatoryReportInfoByReconciliationPeriodID(reconciliationPeriodID);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return oRoleMandatoryReportInfoCollection;
        }

        public List<RoleReportInfo_ExtendedWithReportName> SelectAllRoleReportByRoleID(ReportParamInfo oReportParamInfo, AppUserInfo oAppUserInfo)
        {

            List<RoleReportInfo_ExtendedWithReportName> oRoleReportInfoCollection = null;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                RoleReportDAO oRoleReportDAO = new RoleReportDAO(oAppUserInfo);
                oRoleReportInfoCollection = (List<RoleReportInfo_ExtendedWithReportName>)oRoleReportDAO.GetAllRoleReportByRoleID(oReportParamInfo);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return oRoleReportInfoCollection;
        }

        public List<ReportMstInfo> SelectAllReportByRoleID(short? roleID, int? RecPeriodID, int? companyID, AppUserInfo oAppUserInfo)
        {
            List<ReportMstInfo> oReportMstInfoCollection = null;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                ReportMstDAO oReportTypeMstDAO = new ReportMstDAO(oAppUserInfo);

                oReportMstInfoCollection = (List<ReportMstInfo>)oReportTypeMstDAO.GetAllReportByRoleID(roleID, RecPeriodID, companyID);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return oReportMstInfoCollection;
        }


        public List<ReportMstInfo> SelectAllMyReportByRoleID(short? roleID, int? userID, int? RecPeriodID, AppUserInfo oAppUserInfo)
        {
            List<ReportMstInfo> oReportMstInfoCollection = null;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                ReportMstDAO oReportTypeMstDAO = new ReportMstDAO(oAppUserInfo);

                oReportMstInfoCollection = (List<ReportMstInfo>)oReportTypeMstDAO.GetAllMyReportByRoleID(roleID, userID, RecPeriodID);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return oReportMstInfoCollection;
        }


        public List<ReportMstInfo> SelectMandatoryReportByRoleID(short? roleID, int? userID, int? recPeriodID, AppUserInfo oAppUserInfo)
        {
            List<ReportMstInfo> oReportMstInfoCollection = null;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                ReportMstDAO oReportTypeMstDAO = new ReportMstDAO(oAppUserInfo);

                oReportMstInfoCollection = (List<ReportMstInfo>)oReportTypeMstDAO.GetAllMandatoryReportList(roleID, userID, recPeriodID);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return oReportMstInfoCollection;
        }

        public ReportMstInfo GetReportByID(short? reportID, int languageID, int businessEntityID, int defaultLanguageID, int? companyID, AppUserInfo oAppUserInfo)
        {
            ServiceHelper.SetConnectionString(oAppUserInfo);
            ReportMstDAO oReportMstDAO = new ReportMstDAO(oAppUserInfo);
            return oReportMstDAO.GetReportByReportID(reportID, languageID, businessEntityID, defaultLanguageID, companyID);

        }

        //        public List<ReportSavedInfo> GetSavedReportData(short? reportID)//
        //        {
        //#if DEMO
        //            List<ReportSavedInfo> lstReportSavedInfo = new List<ReportSavedInfo>();
        //            ReportSavedInfo oReportSavedInfo1 = new ReportSavedInfo();
        //            oReportSavedInfo1.SavedReportID = 1;
        //            oReportSavedInfo1.ReportID = 1;
        //            oReportSavedInfo1.Report = "Unusual Balances Report";
        //            oReportSavedInfo1.SavedReport = "Unusual Balances Report_1";
        //            oReportSavedInfo1.Parameters = parametersString_REPORT;
        //            oReportSavedInfo1.SavedBy = "Nancy Davis";
        //            oReportSavedInfo1.DateSaved = Convert.ToDateTime("2009-10-01 19:50:00.000");
        //            lstReportSavedInfo.Add(oReportSavedInfo1);

        //            ReportSavedInfo oReportSavedInfo2 = new ReportSavedInfo();
        //            oReportSavedInfo2.SavedReportID = 2;
        //            oReportSavedInfo2.ReportID = 1;
        //            oReportSavedInfo2.Report = "Unusual Balances Report";
        //            oReportSavedInfo2.SavedReport = "Unusual Balances Report_2";
        //            oReportSavedInfo2.Parameters = parametersString_REPORT;
        //            oReportSavedInfo2.SavedBy = "Nancy Davis";
        //            oReportSavedInfo2.DateSaved = Convert.ToDateTime("2009-10-01 19:50:00.000");
        //            lstReportSavedInfo.Add(oReportSavedInfo2);

        //            return lstReportSavedInfo;
        //#else
        //            MaterialityTypeMstDAO oMaterialityTypeMstDAO = new MaterialityTypeMstDAO(oAppUserInfo);
        //            return  oMaterialityTypeMstDAO.SelectAll();
        //#endif
        //        }

        public List<ReportActivityInfo> GetReportActivityData(short? reportID, AppUserInfo oAppUserInfo)
        {
            ServiceHelper.SetConnectionString(oAppUserInfo);
#if DEMO
            List<ReportActivityInfo> lstReportActivityInfo = new List<ReportActivityInfo>();
            ReportActivityInfo oReportActivityInfo1 = new ReportActivityInfo();
            oReportActivityInfo1.ReportID = 1;
            oReportActivityInfo1.ArchivedDate = Convert.ToDateTime("2009-10-01 19:50:00.000");
            oReportActivityInfo1.Parameters = parametersString_REPORT;
            oReportActivityInfo1.ActionPerformed = "Archived";
            oReportActivityInfo1.SignOffComment = "Growth of the company went better then projection for the last fiscal year.";
            lstReportActivityInfo.Add(oReportActivityInfo1);

            ReportActivityInfo oReportActivityInfo2 = new ReportActivityInfo();
            oReportActivityInfo2.ReportID = 1;
            oReportActivityInfo2.ArchivedDate = Convert.ToDateTime("2009-11-01 19:50:00.000");
            oReportActivityInfo2.Parameters = parametersString_REPORT;
            oReportActivityInfo2.ActionPerformed = "Archived";
            oReportActivityInfo2.SignOffComment = "lots of unexplained variance for the period.";
            lstReportActivityInfo.Add(oReportActivityInfo2);

            return lstReportActivityInfo;
#else
#endif
        }

        #region Report fetching

        public List<UnusualBalancesReportInfo> GetReportUnusualBalancesReport(ReportSearchCriteria oReportSearchCriteria, DataTable tblEntitySearch, AppUserInfo oAppUserInfo)
        {
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                UnusualBalancesReportDAO oUnusualBalancesReportDAO = new UnusualBalancesReportDAO(oAppUserInfo);
                return oUnusualBalancesReportDAO.GetReportUnusualBalancesReport(oReportSearchCriteria, tblEntitySearch);
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
        public List<UnassignedAccountsReportInfo> GetReportUnassignedAccountsReport(ReportSearchCriteria oReportSearchCriteria, DataTable tblEntitySearch, AppUserInfo oAppUserInfo)
        {
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                UnassignedAccountsReportDAO oUnassignedAccountsReportDAO = new UnassignedAccountsReportDAO(oAppUserInfo);
                return oUnassignedAccountsReportDAO.GetReportUnassignedAccountsReport(oReportSearchCriteria, tblEntitySearch);
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
        public List<IncompleteAccountAttributeReportInfo> GetReportIncompleteAccountAttributeReport(ReportSearchCriteria oReportSearchCriteria, DataTable tblEntitySearch, AppUserInfo oAppUserInfo)
        {
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                IncompleteAccountAttributeReportDAO oIncompleteAccountAttributeReportDAO = new IncompleteAccountAttributeReportDAO(oAppUserInfo);
                return oIncompleteAccountAttributeReportDAO.GetReportIncompleteAccountAttributeReport(oReportSearchCriteria, tblEntitySearch);
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
        public List<AccountStatusReportInfo> GetReportAccountStatusReport(ReportSearchCriteria oReportSearchCriteria, DataTable tblEntitySearch, AppUserInfo oAppUserInfo)
        {
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                AccountStatusReportDAO oAccountStatusReportDAO = new AccountStatusReportDAO(oAppUserInfo);
                return oAccountStatusReportDAO.GetReportAccountStatusReport(oReportSearchCriteria, tblEntitySearch);
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

        public List<NewAccountReportInfo> GetReportNewAccountReport(ReportSearchCriteria oReportSearchCriteria, DataTable tblEntitySearch, AppUserInfo oAppUserInfo)
        {
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                NewAccountReportDAO oNewAccountReportDAO = new NewAccountReportDAO(oAppUserInfo);
                return oNewAccountReportDAO.GetReportNewAccountReport(oReportSearchCriteria, tblEntitySearch);
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

        public List<AccountOwnershipReportInfo> GetReportAccountOwnershipReport(ReportSearchCriteria oReportSearchCriteria, DataTable tblUserSearch, DataTable tblRoleSearch, AppUserInfo oAppUserInfo)
        {
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                AccountOwnershipReportDAO oAccountOwnershipReportDAO = new AccountOwnershipReportDAO(oAppUserInfo);
                return oAccountOwnershipReportDAO.GetReportAccountOwnershipReport(oReportSearchCriteria, tblUserSearch, tblRoleSearch);
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

        public List<OpenItemsReportInfo> GetReportOpenItemsReport(ReportSearchCriteria oReportSearchCriteria, DataTable tblEntitySearch, DataTable tblUserSearch, DataTable tblRoleSearch, AppUserInfo oAppUserInfo)
        {
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                OpenItemsReportDAO oOpenItemsReportDAO = new OpenItemsReportDAO(oAppUserInfo);
                return oOpenItemsReportDAO.GetReportOpenItemsReport(oReportSearchCriteria, tblEntitySearch, tblUserSearch, tblRoleSearch);
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

        public List<OpenItemsReportInfo> GetReportOpenItemsReportForCurrentRecPeriod(ReportSearchCriteria oReportSearchCriteria, DataTable tblEntitySearch, DataTable tblUserSearch, DataTable tblRoleSearch, AppUserInfo oAppUserInfo)
        {
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                OpenItemsReportDAO oOpenItemsReportDAO = new OpenItemsReportDAO(oAppUserInfo);
                return oOpenItemsReportDAO.GetReportOpenItemsReportForCurrentRecperiod(oReportSearchCriteria, tblEntitySearch, tblUserSearch, tblRoleSearch);
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

        public List<ReconciliationStatusCountReportInfo> GetReportReconciliationStatusCountReport(ReportSearchCriteria oReportSearchCriteria, DataTable tblUserSearch, DataTable tblRoleSearch, AppUserInfo oAppUserInfo)
        {
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                ReconciliationStatusCountReportDAO oReconciliationStatusCountReportDAO = new ReconciliationStatusCountReportDAO(oAppUserInfo);
                return oReconciliationStatusCountReportDAO.GetReportReconciliationStatusCountReport(oReportSearchCriteria, tblUserSearch, tblRoleSearch);
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
        public List<CertificationTrackingReportInfo> GetReportCertificationTrackingReport(ReportSearchCriteria oReportSearchCriteria, DataTable tblUserSearch, DataTable tblRoleSearch, AppUserInfo oAppUserInfo)
        {
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                CertificationTrackingReportDAO oCertificationTrackingReportDAO = new CertificationTrackingReportDAO(oAppUserInfo);
                return oCertificationTrackingReportDAO.GetReportCertificationTrackingReport(oReportSearchCriteria, tblUserSearch, tblRoleSearch);
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

        public List<DelinquentAccountByUserReportInfo> GetReportDelinquentAccountByUserReport(ReportSearchCriteria oReportSearchCriteria, DataTable tblUserSearch, DataTable tblRoleSearch, AppUserInfo oAppUserInfo)
        {
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                DelinquentAccountByUserReportDAO oDelinquentAccountByUserReportDAO = new DelinquentAccountByUserReportDAO(oAppUserInfo);
                return oDelinquentAccountByUserReportDAO.GetReportDelinquentAccountByUserReport(oReportSearchCriteria, tblUserSearch, tblRoleSearch);
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

        public List<CompletionDateReportInfo> GetReportCompletionDateReport(ReportSearchCriteria oReportSearchCriteria, DataTable tblEntitySearch, string System, AppUserInfo oAppUserInfo)
        {
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                CompletionDateReportDAO oCompletionDateReportDAO = new CompletionDateReportDAO(oAppUserInfo);
                return oCompletionDateReportDAO.GetCompletionDateReport(oReportSearchCriteria, tblEntitySearch,System);
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
        public List<TaskCompletionReportInfo> GetReportTaskCompletionReport(short taskType, ReportSearchCriteria oReportSearchCriteria, DataTable tblEntitySearch, DataTable dtUser, DataTable dtRole, List<FilterCriteria> oTaskFilterCriteriaCollection, string System, AppUserInfo oAppUserInfo)
        {
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                TaskCompletionReportDAO oTaskCompletionReportDAO = new TaskCompletionReportDAO(oAppUserInfo);
                return oTaskCompletionReportDAO.GetTaskCompletionReport(taskType,oReportSearchCriteria, tblEntitySearch,  dtUser,  dtRole, oTaskFilterCriteriaCollection, System);
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

        #endregion

        public bool DeleteSavedReportData(List<long> UserMyReportIDCollection, AppUserInfo oAppUserInfo)
        {
            bool isSuccess = false;

            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                UserMyReportSavedReportDAO oUserMyReportSavedReportDAO = new UserMyReportSavedReportDAO(oAppUserInfo);
                isSuccess = oUserMyReportSavedReportDAO.DeleteSavedReportData(UserMyReportIDCollection);

            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }

            return isSuccess;


        }

        public bool InsertMyReport(short? roleID, int? userID, ReportMstInfo oReportInfo, string myReportName, List<UserMyReportSavedReportParameterInfo> oUserMyReportSavedReportParameterCollection, AppUserInfo oAppUserInfo)
        {
            int userMySaveReportId = 0;
            IDbConnection oConnection = null;
            IDbTransaction oTransaction = null;
            ServiceHelper.SetConnectionString(oAppUserInfo);
            ReportMstDAO oReportMstDAO = new ReportMstDAO(oAppUserInfo);
            bool SaveMyReportSuccess;
            try
            {

                oConnection = oReportMstDAO.CreateConnection();
                oConnection.Open();
                oTransaction = oConnection.BeginTransaction();
                userMySaveReportId = oReportMstDAO.InsertUserMyReport(roleID, userID, oReportInfo, myReportName, oConnection, oTransaction);

                SaveMyReportSuccess = oReportMstDAO.InsertUserMyReportParameter(userMySaveReportId, oUserMyReportSavedReportParameterCollection, oConnection, oTransaction);
                oTransaction.Commit();
            }
            catch (Exception ex)
            {
                if ((oTransaction != null) && (oConnection.State != ConnectionState.Closed))
                {
                    oTransaction.Rollback();
                }
                throw ex;
            }
            finally
            {
                try
                {
                    if (null != oConnection && oConnection.State != ConnectionState.Closed)
                        oConnection.Dispose();
                }
                catch (Exception)
                {
                }
            }
            return SaveMyReportSuccess;


        }


        public List<UserMyReportSavedReportInfo> GetSavedReportData(short? roleID, int? userID, short? reportID, int languageID, int defaultLanguageID, int companyID, AppUserInfo oAppUserInfo)
        {
            ServiceHelper.SetConnectionString(oAppUserInfo);
            //#if DEMO
            //            List<ReportSavedInfo> lstReportSavedInfo = new List<ReportSavedInfo>();
            //            ReportSavedInfo oReportSavedInfo1 = new ReportSavedInfo();
            //            oReportSavedInfo1.SavedReportID = 1;
            //            oReportSavedInfo1.ReportID = 1;
            //            oReportSavedInfo1.Report = "Unusual Balances Report";
            //            oReportSavedInfo1.SavedReport = "Unusual Balances Report_1";
            //            oReportSavedInfo1.Parameters = parametersString_REPORT;
            //            oReportSavedInfo1.SavedBy = "Nancy Davis";
            //            oReportSavedInfo1.DateSaved = Convert.ToDateTime("2009-10-01 19:50:00.000");
            //            lstReportSavedInfo.Add(oReportSavedInfo1);

            //            ReportSavedInfo oReportSavedInfo2 = new ReportSavedInfo();
            //            oReportSavedInfo2.SavedReportID = 2;
            //            oReportSavedInfo2.ReportID = 1;
            //            oReportSavedInfo2.Report = "Unusual Balances Report";
            //            oReportSavedInfo2.SavedReport = "Unusual Balances Report_2";
            //            oReportSavedInfo2.Parameters = parametersString_REPORT;
            //            oReportSavedInfo2.SavedBy = "Nancy Davis";
            //            oReportSavedInfo2.DateSaved = Convert.ToDateTime("2009-10-01 19:50:00.000");
            //            lstReportSavedInfo.Add(oReportSavedInfo2);

            //            return lstReportSavedInfo;
            //#else
            //            MaterialityTypeMstDAO oMaterialityTypeMstDAO = new MaterialityTypeMstDAO(oAppUserInfo);
            //            return  oMaterialityTypeMstDAO.SelectAll();
            //#endif

            List<UserMyReportSavedReportInfo> oUserMyReportSavedReportInfoCollection = new List<UserMyReportSavedReportInfo>();
            try
            {
                UserMyReportSavedReportDAO oUserMyReportSavedReportDAO = new UserMyReportSavedReportDAO(oAppUserInfo);

                oUserMyReportSavedReportInfoCollection = (List<UserMyReportSavedReportInfo>)oUserMyReportSavedReportDAO.GetAllUserMyReportSavedReportCollection(roleID, userID, reportID, languageID, defaultLanguageID, companyID);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return oUserMyReportSavedReportInfoCollection;


        }

        public IList<UserMyReportSavedReportParameterInfo> GetAllParametersByMySavedReportID(int UserMyReportSavedReportID, AppUserInfo oAppUserInfo)
        {

            List<UserMyReportSavedReportParameterInfo> oUserMyReportSavedReportParameterInfoCollection = new List<UserMyReportSavedReportParameterInfo>();
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                UserMyReportSavedReportParameterDAO oUserMyReportSavedReportParameterDAO = new UserMyReportSavedReportParameterDAO(oAppUserInfo);

                oUserMyReportSavedReportParameterInfoCollection = (List<UserMyReportSavedReportParameterInfo>)oUserMyReportSavedReportParameterDAO.GetAllParametersByMySavedReportID(UserMyReportSavedReportID);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return oUserMyReportSavedReportParameterInfoCollection;


        }

        public List<ExceptionStatusReportInfo> GetExceptionStatusReport(ReportSearchCriteria oReportSearchCriteria, DataTable dtEntity, DataTable dtUser, DataTable dtRole, AppUserInfo oAppUserInfo)
        {
            List<ExceptionStatusReportInfo> oExceptionStatusReportInfoCollection = null;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                ExceptionStatusReportDAO oExceptionStatusReportDAO = new ExceptionStatusReportDAO(oAppUserInfo);
                oExceptionStatusReportInfoCollection = oExceptionStatusReportDAO.GetExceptionStatusReport(oReportSearchCriteria, dtEntity, dtUser, dtRole);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }

            return oExceptionStatusReportInfoCollection;
        }


        public int NoOfSavedMyReportByReportID(short? reportID, AppUserInfo oAppUserInfo)
        {

            int NoOfSavedMyReportByReportID = 0;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                UserMyReportDAO oUserMyReportDAO = new UserMyReportDAO(oAppUserInfo);

                NoOfSavedMyReportByReportID = oUserMyReportDAO.NoOfSavedMyReportByReportID(reportID);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return NoOfSavedMyReportByReportID;


        }


        public bool DeleteMyReportByReportID(short? roleID, int? userID, short? reportID, AppUserInfo oAppUserInfo)
        {

            bool isSuccess = false;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                ReportMstDAO oReportTypeMstDAO = new ReportMstDAO(oAppUserInfo);
                isSuccess = oReportTypeMstDAO.DeleteMyReportByReportID(roleID, userID, reportID);

            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }

            return isSuccess;


        }

        public List<ReportMstInfo> GetAllReportsByPackageId(short? iPackageId, AppUserInfo oAppUserInfo)
        {
            List<ReportMstInfo> lstReportMstInfo = null;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                ReportMstDAO oReportMstDAO = new ReportMstDAO(oAppUserInfo);
                lstReportMstInfo = oReportMstDAO.GetAllReportsByPackageId(iPackageId);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return lstReportMstInfo;
        }

        public List<QualityScoreReportInfo> GetReportQualityScoreReport(ReportSearchCriteria oReportSearchCriteria, DataTable tblEntitySearch, AppUserInfo oAppUserInfo)
        {
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                QualityScoreRangeDAO oQualityScoreReportDAO = new QualityScoreRangeDAO(oAppUserInfo);
                return oQualityScoreReportDAO.GetQualityScoreReport(oReportSearchCriteria, tblEntitySearch);
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

        public List<ReviewNotesReportInfo> GetReportReviewNotesReport(ReportSearchCriteria oReportSearchCriteria, DataTable tblEntitySearch, AppUserInfo oAppUserInfo)
        {
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                ReviewNotesReportDAO oReviewNotesReportDAO = new ReviewNotesReportDAO(oAppUserInfo);
                return oReviewNotesReportDAO.GetReportReviewNotesReport(oReportSearchCriteria, tblEntitySearch);
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
        public List<AccountAttributeChangeReportInfo> GetReportAccountAttributeChangeReport(ReportSearchCriteria oReportSearchCriteria, DataTable tblEntitySearch, AppUserInfo oAppUserInfo)
        {
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                AccountAttributeChangeReportDAO oAccountAttributeChangeReportDAO = new AccountAttributeChangeReportDAO(oAppUserInfo);
                return oAccountAttributeChangeReportDAO.GetReportAccountAttributeChangeReport(oReportSearchCriteria, tblEntitySearch);
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

        public List<ReportColumnInfo> SelectAllReportColumnsByReportID(ReportParamInfo oReportParamInfo, AppUserInfo oAppUserInfo)
        {
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                ReportColumnDAO oReportColumnDAO = new ReportColumnDAO(oAppUserInfo);
                return oReportColumnDAO.SelectAllReportColumnsByReportID(oReportParamInfo);
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

    }//end of class
}//end of namespace
