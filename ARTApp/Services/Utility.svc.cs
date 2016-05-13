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
using System.Data.SqlClient;
using SkyStem.ART.App.Utility;
using SkyStem.ART.Client.Data;
using SkyStem.ART.Client.Params;
using System.Threading;
using log4net.Repository.Hierarchy;
using log4net.Appender;
using SkyStem.ART.Client.Model.CompanyDatabase;
using SkyStem.ART.App.DAO.CompanyDatabase;
using SkyStem.ART.Client.Model.RecControlCheckList;

namespace SkyStem.ART.App.Services
{
    // NOTE: If you change the class name "Utility" here, you must also update the reference to "Utility" in Web.config.
    public class Utility : IUtility
    {
        #region IUtility Members
        public IList<DataImportTypeMstInfo> GetAllImportType(AppUserInfo oAppUserInfo)
        {
            IList<DataImportTypeMstInfo> oDataImportTypeMstInfoCollection = null;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                DataImportTypeMstDAO objDataImportTypeMstDAO = new DataImportTypeMstDAO(oAppUserInfo);
                oDataImportTypeMstInfoCollection = objDataImportTypeMstDAO.GetAllImportType();
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return oDataImportTypeMstInfoCollection;
        }

        public int? GetRoleID(string RoleDesc, AppUserInfo oAppUserInfo)
        {
            int? roleID = null;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                UtilityDAO oUtilityDAO = new UtilityDAO(oAppUserInfo);
                roleID = oUtilityDAO.GetRoleID(RoleDesc);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return roleID;
        }

        //capability page
        public List<RiskRatingMstInfo> SelectAllRiskRating(AppUserInfo oAppUserInfo)
        {
            //#if DEMO
            //            List<RiskRatingMstInfo> oRiskRatingMstInfoCollection= new  List<RiskRatingMstInfo>();
            //            RiskRatingMstInfo oRiskRatingMstInfo1 = new RiskRatingMstInfo();
            //            oRiskRatingMstInfo1.RiskRatingID = 1;
            //            oRiskRatingMstInfo1.RiskRating = "High";
            //            oRiskRatingMstInfo1.RiskRatingLabelID = 1127;
            //            oRiskRatingMstInfo1.ReconciliationFrequencyID = 1;
            //            oRiskRatingMstInfoCollection.Add(oRiskRatingMstInfo1);

            //            RiskRatingMstInfo oRiskRatingMstInfo2 = new RiskRatingMstInfo();
            //            oRiskRatingMstInfo2.RiskRatingID = 2;
            //            oRiskRatingMstInfo2.RiskRating = "Medium";
            //            oRiskRatingMstInfo2.RiskRatingLabelID = 1128;
            //            oRiskRatingMstInfo2.ReconciliationFrequencyID = 2;
            //            oRiskRatingMstInfoCollection.Add(oRiskRatingMstInfo2);

            //            RiskRatingMstInfo oRiskRatingMstInfo3 = new RiskRatingMstInfo();
            //            oRiskRatingMstInfo3.RiskRatingID = 3;
            //            oRiskRatingMstInfo3.RiskRating = "Low";
            //            oRiskRatingMstInfo3.RiskRatingLabelID = 1129;
            //            oRiskRatingMstInfo3.ReconciliationFrequencyID = 3;
            //            oRiskRatingMstInfoCollection.Add(oRiskRatingMstInfo3);

            //            return oRiskRatingMstInfoCollection;
            //#else

            //            RiskRatingMstDAO oRiskRatingMstDAO = new RiskRatingMstDAO(oAppUserInfo);
            //            return oRiskRatingMstDAO.SelectAll();
            //#endif
            List<RiskRatingMstInfo> oRiskRatingMstInfoCollection = null;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                RiskRatingMstDAO oRiskRatingMstDAO = new RiskRatingMstDAO(oAppUserInfo);
                oRiskRatingMstInfoCollection = oRiskRatingMstDAO.SelectAll();
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return oRiskRatingMstInfoCollection;
        }

        public List<ReconciliationFrequencyMstInfo> GetAllReconciliationFrequencyMstInfo(AppUserInfo oAppUserInfo)
        {
            List<ReconciliationFrequencyMstInfo> oReconciliationFrequencyMstInfoCollection = null;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                ReconciliationFrequencyMstDAO oReconciliationFrequencyMstDAO = new ReconciliationFrequencyMstDAO(oAppUserInfo);
                oReconciliationFrequencyMstInfoCollection = oReconciliationFrequencyMstDAO.GetAllReconciliationFrequencyMstInfo();
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return oReconciliationFrequencyMstInfoCollection;
        }

        //TODO: try to replace this
        //public int SaveCompanyCapabilityTableValue(List<int> IDs, CompanyCapabilityInfo oCompanyCapabilityInfo)
        //{
        //    CompanyCapabilityDAO oCompanyCapabilityDAO = new CompanyCapabilityDAO(oAppUserInfo);
        //    return oCompanyCapabilityDAO.SaveCompanyCapabilityTableValue(IDs, oCompanyCapabilityInfo,);
        //}

        public IList<MaterialityTypeMstInfo> SelectAllMaterialityTypeMst(AppUserInfo oAppUserInfo)
        {
            //#if DEMO
            //            IList<MaterialityTypeMstInfo> oMaterialityTypeMstInfoCollection = new List<MaterialityTypeMstInfo>();
            //            MaterialityTypeMstInfo oMaterialityTypeMstInfo1 = new MaterialityTypeMstInfo();
            //            oMaterialityTypeMstInfo1.MaterialityTypeID = 1;
            //            oMaterialityTypeMstInfo1.MaterialityType = "By FS Caption";
            //            oMaterialityTypeMstInfo1.MaterialityTypeLabelID = 1337;
            //            oMaterialityTypeMstInfoCollection.Add(oMaterialityTypeMstInfo1);

            //            MaterialityTypeMstInfo oMaterialityTypeMstInfo2 = new MaterialityTypeMstInfo();
            //            oMaterialityTypeMstInfo2.MaterialityTypeID = 2;
            //            oMaterialityTypeMstInfo2.MaterialityType = "Company wide";
            //            oMaterialityTypeMstInfo2.MaterialityTypeLabelID = 1070;
            //            oMaterialityTypeMstInfoCollection.Add(oMaterialityTypeMstInfo2);

            //            return oMaterialityTypeMstInfoCollection;
            //#else
            //            MaterialityTypeMstDAO oMaterialityTypeMstDAO = new MaterialityTypeMstDAO(oAppUserInfo);
            //            return  oMaterialityTypeMstDAO.SelectAll();
            //#endif
            IList<MaterialityTypeMstInfo> oMaterialityTypeMstInfoCollection = null;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                MaterialityTypeMstDAO oMaterialityTypeMstDAO = new MaterialityTypeMstDAO(oAppUserInfo);
                oMaterialityTypeMstInfoCollection = oMaterialityTypeMstDAO.SelectAll();
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return oMaterialityTypeMstInfoCollection;
        }

        public List<ReconciliationArchiveInfo> GetReconciliationArchiveData(int? accountID, AppUserInfo oAppUserInfo)//
        {
#if DEMO
            List<ReconciliationArchiveInfo> oReconciliationArchiveInfoCollection = new List<ReconciliationArchiveInfo>();
            ReconciliationArchiveInfo oReconciliationArchiveInfo1 = new ReconciliationArchiveInfo();
            oReconciliationArchiveInfo1.ReconciliationPeriodID = 1;
            oReconciliationArchiveInfo1.PeriodEndDate = Convert.ToDateTime("2009-10-03 19:50:00.000");
            oReconciliationArchiveInfo1.GLBalanceReportingCurrency = 6000;
            oReconciliationArchiveInfo1.DateCertified = Convert.ToDateTime("2009-10-01 19:50:00.000");
            oReconciliationArchiveInfoCollection.Add(oReconciliationArchiveInfo1);

            ReconciliationArchiveInfo oReconciliationArchiveInfo2 = new ReconciliationArchiveInfo();
            oReconciliationArchiveInfo2.ReconciliationPeriodID = 2;
            oReconciliationArchiveInfo2.PeriodEndDate = Convert.ToDateTime("2009-11-03 19:50:00.000");
            oReconciliationArchiveInfo2.GLBalanceReportingCurrency = 7500;
            oReconciliationArchiveInfo2.DateCertified = Convert.ToDateTime("2009-11-01 19:50:00.000");
            oReconciliationArchiveInfoCollection.Add(oReconciliationArchiveInfo2);

            ReconciliationArchiveInfo oReconciliationArchiveInfo3 = new ReconciliationArchiveInfo();
            oReconciliationArchiveInfo3.ReconciliationPeriodID = 3;
            oReconciliationArchiveInfo3.PeriodEndDate = Convert.ToDateTime("2009-12-03 19:50:00.000");
            oReconciliationArchiveInfo3.GLBalanceReportingCurrency = 8000;
            oReconciliationArchiveInfo3.DateCertified = Convert.ToDateTime("2009-12-01 19:50:00.000");
            oReconciliationArchiveInfoCollection.Add(oReconciliationArchiveInfo3);

            return oReconciliationArchiveInfoCollection;
#else
            ServiceHelper.SetConnectionString(oAppUserInfo);
            MaterialityTypeMstDAO oMaterialityTypeMstDAO = new MaterialityTypeMstDAO(oAppUserInfo);
            return  oMaterialityTypeMstDAO.SelectAll();
#endif
        }

        public List<GeographyClassMstInfo> GetAllOrganizationalHierarchyKeys(short? companyGeographyClassID, AppUserInfo oAppUserInfo)
        {

            List<GeographyClassMstInfo> oGeographyClassMstInfoCollection = new List<GeographyClassMstInfo>();
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                GeographyClassMstDAO oGeographyClassMstDAO = new GeographyClassMstDAO(oAppUserInfo);
                oGeographyClassMstInfoCollection = oGeographyClassMstDAO.GetAllGeographyClassMst(companyGeographyClassID);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return oGeographyClassMstInfoCollection;

            #region "OLD CODE For demo"
            //#if DEMO

            //            List<GeographyClassMstInfo> oGeographyClassMstInfoCollectionDemo = new List<GeographyClassMstInfo>();

            //            GeographyClassMstInfo oGeographyClassMstInfo = new GeographyClassMstInfo();
            //            oGeographyClassMstInfo.GeographyClassID = 2;
            //            oGeographyClassMstInfo.GeographyClassLabelID = 1060;
            //            oGeographyClassMstInfoCollectionDemo.Add(oGeographyClassMstInfo);

            //            oGeographyClassMstInfo = new GeographyClassMstInfo();
            //            oGeographyClassMstInfo.GeographyClassID = 3;
            //            oGeographyClassMstInfo.GeographyClassLabelID = 1061;
            //            oGeographyClassMstInfoCollectionDemo.Add(oGeographyClassMstInfo);

            //            oGeographyClassMstInfo = new GeographyClassMstInfo();
            //            oGeographyClassMstInfo.GeographyClassID = 4;
            //            oGeographyClassMstInfo.GeographyClassLabelID = 1062;
            //            oGeographyClassMstInfoCollectionDemo.Add(oGeographyClassMstInfo);

            //            oGeographyClassMstInfo = new GeographyClassMstInfo();
            //            oGeographyClassMstInfo.GeographyClassID = 5;
            //            oGeographyClassMstInfo.GeographyClassLabelID = 1063;
            //            oGeographyClassMstInfoCollectionDemo.Add(oGeographyClassMstInfo);

            //            oGeographyClassMstInfo = new GeographyClassMstInfo();
            //            oGeographyClassMstInfo.GeographyClassID = 6;
            //            oGeographyClassMstInfo.GeographyClassLabelID = 1064;
            //            oGeographyClassMstInfoCollectionDemo.Add(oGeographyClassMstInfo);

            //            oGeographyClassMstInfo = new GeographyClassMstInfo();
            //            oGeographyClassMstInfo.GeographyClassID = 7;
            //            oGeographyClassMstInfo.GeographyClassLabelID = 1065;
            //            oGeographyClassMstInfoCollectionDemo.Add(oGeographyClassMstInfo);

            //            oGeographyClassMstInfo = new GeographyClassMstInfo();
            //            oGeographyClassMstInfo.GeographyClassID = 8;
            //            oGeographyClassMstInfo.GeographyClassLabelID = 1066;
            //            oGeographyClassMstInfoCollectionDemo.Add(oGeographyClassMstInfo);

            //            oGeographyClassMstInfo = new GeographyClassMstInfo();
            //            oGeographyClassMstInfo.GeographyClassID = 9;
            //            oGeographyClassMstInfo.GeographyClassLabelID = 1067;
            //            oGeographyClassMstInfoCollectionDemo.Add(oGeographyClassMstInfo);

            //            oGeographyClassMstInfo = new GeographyClassMstInfo();
            //            oGeographyClassMstInfo.GeographyClassID = 10;
            //            oGeographyClassMstInfo.GeographyClassLabelID = 1068;
            //            oGeographyClassMstInfoCollectionDemo.Add(oGeographyClassMstInfo);

            //            return oGeographyClassMstInfoCollectionDemo;
            //#else
            //#endif
            #endregion
        }

        //public List<AccountAttributeMstInfo> SelectAccountAttributeMstForMassUpdate(List<ARTEnums.AccountAttribute> oAccountAttributeCollection)
        //{
        //    try
        //    {
        //        UtilityDAO oUtilityDAO = new UtilityDAO(oAppUserInfo);
        //        return oUtilityDAO.SelectAccountAttributeMstForMassUpdate(oAccountAttributeCollection);
        //    }
        //    catch (SqlException ex)
        //    {
        //        ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
        //    }
        //    catch (Exception ex)
        //    {
        //        ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
        //    }

        //    return null;
        //}

        public List<AccountAttributeMstInfo> SelectAccountAttributeMstForMassUpdate(int? iCompanyId, int? RecPeriodID, AppUserInfo oAppUserInfo)
        {
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                UtilityDAO oUtilityDAO = new UtilityDAO(oAppUserInfo);
                return oUtilityDAO.SelectAccountAttributeMstForMassUpdate(iCompanyId, RecPeriodID);
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
        /// Get all the Rec Process Statuses
        /// </summary>
        /// <returns></returns>
        public List<ReconciliationPeriodStatusMstInfo> GetAllRecPeriodStatuses(AppUserInfo oAppUserInfo)
        {
            List<ReconciliationPeriodStatusMstInfo> oReconciliationPeriodStatusMstInfoCollection = null;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                ReconciliationPeriodStatusMstDAO oReconciliationPeriodStatusMstDAO = new ReconciliationPeriodStatusMstDAO(oAppUserInfo);
                oReconciliationPeriodStatusMstInfoCollection = oReconciliationPeriodStatusMstDAO.SelectAll();
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return oReconciliationPeriodStatusMstInfoCollection;
        }

        public decimal GetCurrencyExchangeRate(int recPeriodID, string fromCurrencyCode, string toCurrencyCode, bool isMultiCurrencyEnabled, AppUserInfo oAppUserInfo)
        {
            decimal exchangeRate = 1;

            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                ExchangeRateDAO oExchangeRateDAO = new ExchangeRateDAO(oAppUserInfo);
                exchangeRate = oExchangeRateDAO.GetCurrencyExchangeRate(recPeriodID, fromCurrencyCode, toCurrencyCode, isMultiCurrencyEnabled);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }

            return exchangeRate;
        }

        public List<ExchangeRateInfo> GetCurrencyExchangeRateByRecPeriod(int recPeriodID, AppUserInfo oAppUserInfo)
        {
            List<ExchangeRateInfo> oExchangeRateInfoCollection = null;

            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                ExchangeRateDAO oExchangeRateDAO = new ExchangeRateDAO(oAppUserInfo);
                oExchangeRateInfoCollection = oExchangeRateDAO.GetCurrencyExchangeRateByRecPeriod(recPeriodID);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }

            return oExchangeRateInfoCollection;
        }

        public List<ExchangeRateInfo> GetCurrencyExchangeRateArchieveByExchangeRateID(int exchangeRateID, AppUserInfo oAppUserInfo)
        {
            List<ExchangeRateInfo> oExchangeRateInfoList = null;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                ExchangeRateDAO oExchangeRateDAO = new ExchangeRateDAO(oAppUserInfo);
                oExchangeRateInfoList = oExchangeRateDAO.GetCurrencyExchangeRateArchieveByExchangeRateID(exchangeRateID);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return oExchangeRateInfoList;
        }
        public List<SystemReconciliationRuleMstInfo> SelectAllSRARules(AppUserInfo oAppUserInfo)
        {
            List<SystemReconciliationRuleMstInfo> oSystemReconciliationRuleMstInfoCollection = null;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                SystemReconciliationRuleMstDAO oSystemReconciliationRuleMstDAO = new SystemReconciliationRuleMstDAO(oAppUserInfo);
                oSystemReconciliationRuleMstInfoCollection = oSystemReconciliationRuleMstDAO.GetAllSystemReconciliationRules();
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }

            return oSystemReconciliationRuleMstInfoCollection;
        }

        public void InsertCompanySRARule(CompanyConfigurationParamInfo oCompanyConfigurationParamInfo, AppUserInfo oAppUserInfo)
        {
            IDbConnection oConnection = null;
            IDbTransaction oTransaction = null;

            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                List<short> oSRARuleIDCollection = (from SRARule in oCompanyConfigurationParamInfo.CompanySystemReconciliationRuleInfoList select SRARule.SystemReconciliationRuleID.Value).ToList();
                DataTable dtSRARuleID = ServiceHelper.ConvertCompanySRARuleInfoCollectionToDataTable(oCompanyConfigurationParamInfo.CompanySystemReconciliationRuleInfoList);

                CompanySystemReconciliationRuleDAO oCompanySystemReconciliationRuleDAO = new CompanySystemReconciliationRuleDAO(oAppUserInfo);
                oConnection = oCompanySystemReconciliationRuleDAO.CreateConnection();
                oConnection.Open();
                oTransaction = oConnection.BeginTransaction();

                if (oCompanyConfigurationParamInfo.CompanySystemReconciliationRuleInfoList != null && oCompanyConfigurationParamInfo.CompanySystemReconciliationRuleInfoList.Count > 0)
                {
                    CompanySystemReconciliationRuleInfo oCompanySRARuleInfo = oCompanyConfigurationParamInfo.CompanySystemReconciliationRuleInfoList[0];
                    oCompanySystemReconciliationRuleDAO.InsertCompanySRARule(dtSRARuleID, oCompanySRARuleInfo.CompanyID.Value, oCompanyConfigurationParamInfo.RecPeriodID.Value
                        , oCompanySRARuleInfo.AddedBy, oCompanySRARuleInfo.DateAdded.Value, oConnection, oTransaction);
                    //This is handled in the sp hence not required 
                    //SystemLockdownInfo oSystemLockdownInfo = oCompanyConfigurationParamInfo.SystemLockdownInfo;
                    //if (oSystemLockdownInfo != null)
                    //{
                    //    oSystemLockdownInfo.AddedBy = oCompanySRARuleInfo.AddedBy;
                    //    oSystemLockdownInfo.DateAdded = oCompanySRARuleInfo.DateAdded;
                    //    SystemLockdownDAO oSystemLockdownDAO = new SystemLockdownDAO(oAppUserInfo);
                    //    oSystemLockdownDAO.Save(oSystemLockdownInfo, oConnection, oTransaction);
                    //}
                }
                oTransaction.Commit();
            }
            catch (SqlException ex)
            {
                if (oTransaction != null && oTransaction.Connection != null)
                {
                    oTransaction.Rollback();
                    oTransaction.Dispose();
                }
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                if (oTransaction != null && oTransaction.Connection != null)
                {
                    oTransaction.Rollback();
                    oTransaction.Dispose();
                }
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            finally
            {
                if ((oConnection != null) && (oConnection.State == ConnectionState.Open))
                {
                    oConnection.Close();
                    oConnection.Dispose();
                }
            }
        }

        public List<CompanySystemReconciliationRuleInfo> SelectCompanySRARuleInfoByRecPeriodID(int companyID, int recPeriodID, AppUserInfo oAppUserInfo)
        {
            List<CompanySystemReconciliationRuleInfo> oCompanySRARuleInfoCollection = new List<CompanySystemReconciliationRuleInfo>();

            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                CompanySystemReconciliationRuleDAO oCompanySystemReconciliationRuleDAO = new CompanySystemReconciliationRuleDAO(oAppUserInfo);
                oCompanySRARuleInfoCollection = oCompanySystemReconciliationRuleDAO.SelectCompanySRARuleInfoByRecPeriodID(companyID, recPeriodID);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return oCompanySRARuleInfoCollection;
        }

        public List<ExceptionTypeMstInfo> GetAllExceptionTypes(AppUserInfo oAppUserInfo)
        {
            List<ExceptionTypeMstInfo> oExceptionTypeMstInfoCollection = null;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                ExceptionTypeMstDAO oExceptionTypeMstDAO = new ExceptionTypeMstDAO(oAppUserInfo);
                oExceptionTypeMstInfoCollection = oExceptionTypeMstDAO.GetAllExceptionTypes();
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return oExceptionTypeMstInfoCollection;
        }

        public List<AppSettingsInfo> GetAllAppSettings(AppUserInfo oAppUserInfo)
        {
            List<AppSettingsInfo> oAppSettingsInfoCollection = null;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                UtilityDAO oUtilityDAO = new UtilityDAO(oAppUserInfo);
                oAppSettingsInfoCollection = oUtilityDAO.GetAllAppSettings();
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return oAppSettingsInfoCollection;
        }

        public void UpdateGLDataReconciliationStatus(GLDataHdrInfo oGLDataHdrInfo, AppUserInfo oAppUserInfo)
        {
            IDbConnection oConnection = null;
            IDbTransaction oTransaction = null;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                GLDataHdrDAO oGLDataHdrDAO = new GLDataHdrDAO(oAppUserInfo);
                ReconciliationPeriodDAO oReconciliationPeriodDAO = new ReconciliationPeriodDAO(oAppUserInfo);

                oConnection = oGLDataHdrDAO.CreateConnection();
                oConnection.Open();
                oTransaction = oConnection.BeginTransaction();
                oGLDataHdrDAO.UpdateRecStatusAndIsSRAByGLDataIDCommand(oGLDataHdrInfo, oConnection, oTransaction);
                oReconciliationPeriodDAO.UpdateRecPeriodStatusAsInProgress(oGLDataHdrInfo.ReconciliationPeriodID.HasValue ? oGLDataHdrInfo.ReconciliationPeriodID.Value : 0, oConnection, oTransaction);
                oTransaction.Commit();
            }

            catch (SqlException ex)
            {
                if ((oTransaction != null) && oConnection.State != ConnectionState.Closed)
                    oTransaction.Rollback();
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                if ((oTransaction != null) && oConnection.State != ConnectionState.Closed)
                    oTransaction.Rollback();
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            finally
            {
                if (oConnection != null && oConnection.State != ConnectionState.Closed)
                    oConnection.Dispose();
            }

        }

        public List<CapabilityMstInfo> GetAllCapabilities(AppUserInfo oAppUserInfo)
        {
            List<CapabilityMstInfo> oCapabilityMstInfoList = null;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                CapabilityMstDAO oCapabilityMstDAO = new CapabilityMstDAO(oAppUserInfo);
                oCapabilityMstInfoList = oCapabilityMstDAO.SelectAll();
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return oCapabilityMstInfoList;
        }

        public string GetKeyFieldsByCompanyID(int compantID, AppUserInfo oAppUserInfo)
        {

            string keyFields = "";
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                UtilityDAO oUtilityDAO = new UtilityDAO(oAppUserInfo);
                keyFields = oUtilityDAO.GetKeyFieldsByCompanyID(compantID);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return keyFields;
        }
        public string GetAccountUniqueSubsetKeys(int compantID, int recPeriodID, AppUserInfo oAppUserInfo)
        {

            string keyFields = "";
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                UtilityDAO oUtilityDAO = new UtilityDAO(oAppUserInfo);
                keyFields = oUtilityDAO.GetAccountUniqueSubsetKeys(compantID, recPeriodID);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return keyFields;
        }
        #endregion
        #region "Process Materiality And SRA"

        public int ProcessMaterialityAndSRAByCompanyID(int CompanyID, int RecPeriodID, string revisedBy, DateTime dateRevised, AppUserInfo oAppUserInfo)
        {
            int recordsAffected = 0;
            IDbConnection oConnection = null;
            IDbTransaction oTransaction = null;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                CompanySystemReconciliationRuleDAO oCompanySystemReconciliationRuleDAO = new CompanySystemReconciliationRuleDAO(oAppUserInfo);
                oConnection = oCompanySystemReconciliationRuleDAO.CreateConnection();
                oConnection.Open();
                oTransaction = oConnection.BeginTransaction();
                recordsAffected = oCompanySystemReconciliationRuleDAO.ProcessMaterialityAndSRAByCompanyID(CompanyID, RecPeriodID, revisedBy, dateRevised, oConnection, oTransaction);
                oTransaction.Commit();
            }

            catch (SqlException ex)
            {
                if ((oTransaction != null) && oConnection.State != ConnectionState.Closed)
                    oTransaction.Rollback();
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                if ((oTransaction != null) && oConnection.State != ConnectionState.Closed)
                    oTransaction.Rollback();
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            finally
            {
                if (oConnection != null && oConnection.State != ConnectionState.Closed)
                    oConnection.Dispose();
            }
            return recordsAffected;
        }

        #endregion

        #region "logging"
        public void LogInfo(LogInfo oLogInfo, AppUserInfo oAppUserInfo)
        {
            log4net.ILog oLogger = log4net.LogManager.GetLogger(ARTConstants.LOGGER_NAME);
            ServiceHelper.SetConnectionString(oAppUserInfo);
            SetConnectionForLogger(oAppUserInfo);

            this.LogCustomParamters(oLogInfo, oAppUserInfo);
            oLogger.Info(oLogInfo.Message);
        }
        public void LogError(LogInfo oLogInfo, AppUserInfo oAppUserInfo)
        {
            log4net.ILog oLogger = log4net.LogManager.GetLogger(ARTConstants.LOGGER_NAME);
            ServiceHelper.SetConnectionString(oAppUserInfo);
            SetConnectionForLogger(oAppUserInfo);

            this.LogCustomParamters(oLogInfo, oAppUserInfo);
            oLogger.Error(oLogInfo.Message);
        }

        internal void LogCustomParamters(LogInfo oLogInfo, AppUserInfo oAppUserInfo)
        {
            if (oAppUserInfo != null)
            {
                if (oAppUserInfo.CompanyID.HasValue)
                    log4net.ThreadContext.Properties["companyID"] = oAppUserInfo.CompanyID;
                else
                    log4net.ThreadContext.Properties["companyID"] = DBNull.Value;

                if (oAppUserInfo.RecPeriodID.HasValue)
                    log4net.ThreadContext.Properties["recPeriodID"] = oAppUserInfo.RecPeriodID.Value;
                else
                    log4net.ThreadContext.Properties["recPeriodID"] = -1;

                if (!string.IsNullOrEmpty(oAppUserInfo.LoginID))
                    log4net.ThreadContext.Properties["loginID"] = oAppUserInfo.LoginID;
                else
                    log4net.ThreadContext.Properties["loginID"] = string.Empty;

                if (!string.IsNullOrEmpty(oLogInfo.Message))
                    log4net.ThreadContext.Properties["exception"] = oLogInfo.Message;
                else
                    log4net.ThreadContext.Properties["exception"] = DBNull.Value;

                if (!string.IsNullOrEmpty(oLogInfo.StackTrace))
                    log4net.ThreadContext.Properties["stackTrace"] = oLogInfo.StackTrace;
                else
                    log4net.ThreadContext.Properties["stackTrace"] = DBNull.Value;

                if (oLogInfo.LogDate.HasValue)
                    log4net.ThreadContext.Properties["logDate"] = oLogInfo.LogDate;
                else
                    log4net.ThreadContext.Properties["logDate"] = DateTime.Now;

                log4net.ThreadContext.Properties["dataImportID"] = oLogInfo.DataImportID;
                log4net.ThreadContext.Properties["source"] = oLogInfo.Source;
                //due to log4net aysc nature we need to add suspend the thread for some time
                //so do not remove this line.
                Thread.Sleep(1000);
            }
        }

        private void SetConnectionForLogger(AppUserInfo oAppUserInfo)
        {
            //Set Connection string for AdoNetAppender
            Hierarchy hierarchy = log4net.LogManager.GetRepository() as Hierarchy;
            if (hierarchy != null && hierarchy.Configured)
            {
                foreach (IAppender appender in hierarchy.GetAppenders())
                {
                    if (appender is AdoNetAppender)
                    {
                        var adoNetAppender = (AdoNetAppender)appender;
                        if (oAppUserInfo != null)
                        {
                            adoNetAppender.ConnectionString = oAppUserInfo.ConnectionString;
                            adoNetAppender.ActivateOptions(); //Refresh AdoNetAppenders Settings
                        }
                    }
                }
            }
        }
        #endregion

        public List<CompanyUserInfo> GetAllCompanyConnectionInfo()
        {
            AppUserInfo oAppUserInfo = new AppUserInfo();
            ServiceHelper.SetConnectionStringCore(oAppUserInfo);
            List<CompanyUserInfo> oCompanyUserInfoList = null;

            try
            {
                CompanyUserDAO oCompanyUserDAO = new CompanyUserDAO(oAppUserInfo);
                oCompanyUserInfoList = oCompanyUserDAO.GetAllCompanyDatabase();
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return oCompanyUserInfoList;


        }

        public List<ServerCompanyInfo> GetServerCompanyListForServiceProcessing()
        {
            AppUserInfo oAppUserInfo = new AppUserInfo();
            ServiceHelper.SetConnectionStringCore(oAppUserInfo);
            List<ServerCompanyInfo> oServerCompanyInfoList = null;

            try
            {
                ServerCompanyDAO oServerCompanyDAO = new ServerCompanyDAO(oAppUserInfo);
                oServerCompanyInfoList = oServerCompanyDAO.GetServerCompanyListForServiceProcessing();
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return oServerCompanyInfoList;
        }

        public void ReCreateIndexes(int? companyID, AppUserInfo oAppUserInfo)
        {
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                UtilityDAO oUtilityDAO = new UtilityDAO(oAppUserInfo);
                oUtilityDAO.ReCreateIndexes(companyID);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
        }
        public IList<DualLevelReviewTypeMstInfo> SelectAllDualLevelReviewTypeMst(AppUserInfo oAppUserInfo)
        {

            IList<DualLevelReviewTypeMstInfo> oDualLevelReviewTypeMstInfoCollection = null;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                CompanyCapabilityDAO oCompanyCapabilityDAO = new CompanyCapabilityDAO(oAppUserInfo);
                oDualLevelReviewTypeMstInfoCollection = oCompanyCapabilityDAO.SelectAllDualLevelReviewTypeMst();
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return oDualLevelReviewTypeMstInfoCollection;
        }

        public IList<DueDaysBasisInfo> SelectAllDueDaysBasisMst(AppUserInfo oAppUserInfo)
        {

            IList<DueDaysBasisInfo> oDueDaysBasisInfoList = null;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                UtilityDAO oUtilityDAO = new UtilityDAO(oAppUserInfo);
                oDueDaysBasisInfoList = oUtilityDAO.SelectAllDueDaysBasisMst();
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return oDueDaysBasisInfoList;
        }

        public List<ReconciliationCheckListStatusInfo> GetReconciliationCheckListStatus(AppUserInfo oAppUserInfo)
        {

            List<ReconciliationCheckListStatusInfo> ReconciliationCheckListStatusInfoLst = null;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                UtilityDAO oUtilityDAO = new UtilityDAO(oAppUserInfo);
                ReconciliationCheckListStatusInfoLst = oUtilityDAO.GetReconciliationCheckListStatus();
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return ReconciliationCheckListStatusInfoLst;
        }

        public List<string> GetImportTemplateMandatoryFields(int? companyID, int? ImportTemplateID, List<string> MandatoryFieldList, AppUserInfo oAppUserInfo)
        {

            List<string> TemplateMandatoryFieldList = null;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                UtilityDAO oUtilityDAO = new UtilityDAO(oAppUserInfo);
                TemplateMandatoryFieldList = oUtilityDAO.GetImportTemplateMandatoryFields(companyID, ImportTemplateID, MandatoryFieldList);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return TemplateMandatoryFieldList;
        }

        public IList<DaysTypeInfo> SelectAllDaysType(AppUserInfo oAppUserInfo)
        {

            IList<DaysTypeInfo> oDaysTypeInfoList = null;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                UtilityDAO oUtilityDAO = new UtilityDAO(oAppUserInfo);
                oDaysTypeInfoList = oUtilityDAO.SelectAllDaysType();
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return oDaysTypeInfoList;
        }

       
    }//end of class
}//end of namespace
