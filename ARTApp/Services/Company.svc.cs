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
using SkyStem.ART.Client.Exception;
using SkyStem.ART.Client.Data;
using System.Transactions;
using SkyStem.ART.App.DAO.CompanyDatabase;
using SkyStem.ART.Client.Model.CompanyDatabase;

namespace SkyStem.ART.App.Services
{
    // NOTE: If you change the class name "Company" here, you must also update the reference to "Company" in Web.config.
    public class Company : ICompany
    {
        #region ICompany Members

        public CompanyHdrInfo GetCompanyDetail(int? CompanyID, DateTime? periodEndDate, AppUserInfo oAppUserInfo)
        {
            try
            {
                oAppUserInfo.CompanyID = CompanyID;
                ServiceHelper.SetConnectionString(oAppUserInfo);
                CompanyHdrDAO oCompanyDAO = new CompanyHdrDAO(oAppUserInfo);
                return oCompanyDAO.GetCompanyDetail(CompanyID, periodEndDate);
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

        public List<CompanyHdrInfo> GetAllCompanies(AppUserInfo oAppUserInfo)
        {
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                CompanyHdrDAO oCompanyHdrDAO = new CompanyHdrDAO(oAppUserInfo);
                return oCompanyHdrDAO.SelectAll();
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return null;
        }

        public List<CompanyHdrInfo> GetAllCompaniesLiteObject()
        {
            List<CompanyHdrInfo> oCompanyHdrInfoCollection = null;
            AppUserInfo oAppUserInfo = null;
            try
            {
                oAppUserInfo = new AppUserInfo();
                ServiceHelper.SetConnectionStringCore(oAppUserInfo);
                CompanyHdrDAO oCompanyHdrDAO = new CompanyHdrDAO(oAppUserInfo);
                oCompanyHdrInfoCollection = oCompanyHdrDAO.GetAllCompaniesLiteObject();
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return oCompanyHdrInfoCollection;
        }

        public List<CompanyHdrInfo> GetAllCompaniesList(string CompanyName, string DisplayName, bool? IsActive, bool? IsFTPActive, bool IsShowActivationHistory, short ActivationHistoryVal,AppUserInfo oAppUserInfo)
        {
            List<CompanyHdrInfo> oCompanyHdrInfoCollection = null;
            try
            {
                ServiceHelper.SetConnectionStringCore(oAppUserInfo);
                CompanyHdrDAO oCompanyHdrDAO = new CompanyHdrDAO(oAppUserInfo);
                oCompanyHdrInfoCollection = (List<CompanyHdrInfo>)oCompanyHdrDAO.GetAllCompaniesList(CompanyName, DisplayName, IsActive, IsFTPActive, IsShowActivationHistory, ActivationHistoryVal);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return oCompanyHdrInfoCollection;
        }

        public bool CreateNewCompany(CompanyHdrInfo oCompanyHdrInfo, ContactInfo oCompanyContact, int languageID, AppUserInfo oAppUserInfo)
        {
            bool CompanyCreatedSucess = false;
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    if (oCompanyHdrInfo.CompanyID.GetValueOrDefault() < 1)
                    {   // Insert Company Record in Core
                        if (!IsCompanyAliasUnique(oCompanyHdrInfo.CompanyAlias, oCompanyHdrInfo.CompanyID, oAppUserInfo).GetValueOrDefault())
                            throw new ARTException(5000410);
                        ServiceHelper.SetConnectionStringCore(oAppUserInfo);
                        CompanyHdrDAO oCompanyDAOCore = new CompanyHdrDAO(oAppUserInfo);
                        oCompanyHdrInfo.CompanyID = oCompanyDAOCore.CreateCompanyInCore(oCompanyHdrInfo, oCompanyContact, languageID);
                        // Insert Contact Info in Transit
                        if (oCompanyHdrInfo.IsSeparateDatabase.GetValueOrDefault())
                        {
                            ContactDAO oContactDAO = new ContactDAO(oAppUserInfo);
                            oCompanyContact.CompanyID = oCompanyHdrInfo.CompanyID;
                            oContactDAO.InsertContactInfoInCore(oCompanyContact);
                        }
                        else
                        {
                            CompanyCreatedSucess = CreateCompanyAndMapping(oCompanyHdrInfo, oCompanyContact, languageID, oAppUserInfo);
                        }
                    }
                    scope.Complete();
                    CompanyCreatedSucess = true;
                }
            }
            catch(ARTException)
            {
                throw;
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return CompanyCreatedSucess;
        }

        private static bool CreateCompanyAndMapping(CompanyHdrInfo oCompanyHdrInfo, ContactInfo oCompanyContact, int languageID, AppUserInfo oAppUserInfo)
        {
            // Insert Company Database Mapping
            ServiceHelper.SetConnectionStringCore(oAppUserInfo);
            ServerCompanyDAO oServerCompanyDAO = new ServerCompanyDAO(oAppUserInfo);
            ServerCompanyInfo oServerCompanyInfo = oServerCompanyDAO.GetCompanyServer(oCompanyHdrInfo.CompanyID);
            oServerCompanyInfo.AddedBy = oCompanyHdrInfo.AddedBy;
            oServerCompanyInfo.DateAdded = oCompanyHdrInfo.DateAdded;
            oServerCompanyDAO.AddCompanyServer(oServerCompanyInfo);
            // Insert Company Info in company database
            ServiceHelper.SetConnectionString(oAppUserInfo);
            CompanyHdrDAO oCompanyDAO = new CompanyHdrDAO(oAppUserInfo);
            bool CompanyCreatedSucess = oCompanyDAO.CreateNewCompany(oCompanyHdrInfo, oCompanyContact, languageID);
            CompanySettingDAO oCompanySettingDAO = new CompanySettingDAO(oAppUserInfo);
            oCompanySettingDAO.SaveCompanySetting(oCompanyHdrInfo);
            return CompanyCreatedSucess;
        }

        public bool UpdateCompany(CompanyHdrInfo oCompanyHdrInfo, ContactInfo oCompanyContact, int languageID, bool IsPackageUpdated, AppUserInfo oAppUserInfo)
        {
            bool CompanyCreatedSucess = false;
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    if (!IsCompanyAliasUnique(oCompanyHdrInfo.CompanyAlias, oCompanyHdrInfo.CompanyID, oAppUserInfo).GetValueOrDefault())
                        throw new ARTException(5000410);
                    ServiceHelper.SetConnectionStringCore(oAppUserInfo);
                    CompanyHdrDAO oCompanyHdrDAOCore = new CompanyHdrDAO(oAppUserInfo);
                    oCompanyHdrDAOCore.UpdateCompanyInCore(oCompanyHdrInfo, languageID);

                    ServiceHelper.SetConnectionString(oAppUserInfo);
                    CompanyHdrDAO oCompanyDAO = new CompanyHdrDAO(oAppUserInfo);

                    CompanyCreatedSucess = oCompanyDAO.UpdateCompany(oCompanyHdrInfo, oCompanyContact, languageID);
                    /*
                     * If Package is changed then this condition deletes all from CompanyFeature + CompanyRole + CompanyReport data 
                     * and insert new data based on company and package id.
                     */
                    if (IsPackageUpdated)
                    {
                        PackageMstDAO oPackageMstDAO = new PackageMstDAO(oAppUserInfo);
                        oPackageMstDAO.InsertPackageFeatureReportRole(oCompanyHdrInfo.CompanyID, oCompanyHdrInfo.PackageID, oCompanyHdrInfo.AddedBy, oCompanyHdrInfo.DateAdded, oCompanyHdrInfo.RevisedBy, oCompanyHdrInfo.DateRevised);
                    }

                    scope.Complete();
                }
            }
            catch (ARTException)
            {
                throw;
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return CompanyCreatedSucess;
        }

        public bool UpdateCompanyLogoPhysicalPath(CompanyHdrInfo objCompanyHdrInfo, AppUserInfo oAppUserInfo)
        {
            IDbConnection oConnection = null;
            IDbTransaction oTransaction = null;
            ServiceHelper.SetConnectionString(oAppUserInfo);
            CompanyHdrDAO oCompanyDAO = new CompanyHdrDAO(oAppUserInfo);
            bool CompanyCreatedSucess;

            try
            {
                oConnection = oCompanyDAO.CreateConnection();
                oConnection.Open();
                oTransaction = oConnection.BeginTransaction();
                CompanyCreatedSucess = oCompanyDAO.UpdateCompanyLogoPhysicalPath(objCompanyHdrInfo, oConnection, oTransaction);
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
            return CompanyCreatedSucess;

        }

        public ContactInfo GetContactInfo(int? CompanyID, AppUserInfo oAppUserInfo)
        {
            oAppUserInfo.CompanyID = CompanyID;
            IList<ContactInfo> oCompanyContactInfoCollection = null;
            ServiceHelper.SetConnectionString(oAppUserInfo);
            ContactDAO oContactDAO = new ContactDAO(oAppUserInfo);
            oCompanyContactInfoCollection = oContactDAO.SelectAllByCompanyID(CompanyID);
            if (oCompanyContactInfoCollection.Count > 0)
                return oCompanyContactInfoCollection[0];
            else
                return new ContactInfo();
        }

        public ContactInfo GetContactInfoFromCore(int? CompanyID, AppUserInfo oAppUserInfo)
        {
            oAppUserInfo.CompanyID = CompanyID;
            IList<ContactInfo> oContactInfoList = null;
            ServiceHelper.SetConnectionStringCore(oAppUserInfo);
            ContactDAO oContactDAO = new ContactDAO(oAppUserInfo);
            oContactInfoList = oContactDAO.SelectAllByCompanyID(CompanyID);
            if (oContactInfoList.Count > 0)
                return oContactInfoList[0];
            else
                return null;
        }

        /// <summary>
        /// Get the Organization Hierarchy for the Company
        /// </summary>
        /// <param name="CompanyID">Company ID for which we need the Organization Hierarchy</param>
        /// <returns></returns>
        public List<GeographyStructureHdrInfo> GetOrganizationalHierarchy(int? CompanyID, AppUserInfo oAppUserInfo)
        {
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                GeographyStructureHdrDAO oGeographyStructureHdrDAO = new GeographyStructureHdrDAO(oAppUserInfo);
                return oGeographyStructureHdrDAO.GetOrganizationalHierarchy(CompanyID);
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

        public IList<CompanySettingInfo> SelectAllCompanySettingByCompanyID(int? companyID, AppUserInfo oAppUserInfo)
        {
            IList<CompanySettingInfo> oCompanySettingInfoCollection = null;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                CompanySettingDAO oCompanySettingDAO = new CompanySettingDAO(oAppUserInfo);
                oCompanySettingInfoCollection = oCompanySettingDAO.SelectAllByCompanyID(companyID);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return oCompanySettingInfoCollection;
        }

        public IList<CompanyUnexplainedVarianceThresholdInfo> GetUnexplainedVarianceThresholdByRecPeriodID(int? reconciliationPeriodID, AppUserInfo oAppUserInfo)
        {
            IList<CompanyUnexplainedVarianceThresholdInfo> oCompanyUnexplainedVarianceThresholdInfoCollection = null;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                CompanyUnexplainedVarianceThresholdDAO oCompanyUnexplainedVarianceThresholdDAO = new CompanyUnexplainedVarianceThresholdDAO(oAppUserInfo);
                oCompanyUnexplainedVarianceThresholdInfoCollection = oCompanyUnexplainedVarianceThresholdDAO.GetUnexplainedVarianceThresholdByRecPeriodID(reconciliationPeriodID);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return oCompanyUnexplainedVarianceThresholdInfoCollection;
        }

        //TODO: change input to CompanyMaterialityTypeInfo 
        public IList<CompanySettingInfo> SelectCompanyMaterialityType(int? reconciliationPeriodID, AppUserInfo oAppUserInfo)
        {
            IList<CompanySettingInfo> oCompanySettingInfoCollection = null;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                CompanySettingDAO oCompanySettingDAO = new CompanySettingDAO(oAppUserInfo);
                oCompanySettingInfoCollection = oCompanySettingDAO.SelectCompanyMaterialityType(reconciliationPeriodID);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return oCompanySettingInfoCollection;
        }

        public void UpdateCompanySetting(CompanySettingInfo oCompanySettingInfo, AppUserInfo oAppUserInfo)
        {
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                CompanySettingDAO oCompanySettingDAO = new CompanySettingDAO(oAppUserInfo);
                oCompanySettingDAO.UpdateByCompanyID(oCompanySettingInfo);
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

        public IList<ReconciliationPeriodInfo> SelectAllReconciliationPeriodByCompanyID(int? companyID, int? CurrentFinancialYearID, AppUserInfo oAppUserInfo)
        {
            IList<ReconciliationPeriodInfo> oReconciliationPeriodInfoCollection = null;
            try
            {
                if (oAppUserInfo.IsDatabaseExists.GetValueOrDefault())
                {
                    ServiceHelper.SetConnectionString(oAppUserInfo);
                    ReconciliationPeriodDAO oReconciliationPeriodDAO = new ReconciliationPeriodDAO(oAppUserInfo);
                    oReconciliationPeriodInfoCollection = oReconciliationPeriodDAO.SelectAllByCompanyID(companyID, CurrentFinancialYearID);
                }
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return oReconciliationPeriodInfoCollection;
        }

        public List<CompanyCapabilityInfo> SelectAllCompanyCapabilityByReconciliationID(int? reconciliationID, AppUserInfo oAppUserInfo)
        {
            //to be deleted when demo flag is off
            List<CompanyCapabilityInfo> oCompanyCapabilityInfoCollection = null;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                CompanyCapabilityDAO oCompanyCapabilityDAO = new CompanyCapabilityDAO(oAppUserInfo);
                oCompanyCapabilityInfoCollection = (List<CompanyCapabilityInfo>)oCompanyCapabilityDAO.SelectAllCompanyCapabilityByReconciliationPeriodID(reconciliationID);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return oCompanyCapabilityInfoCollection;
        }


        //Capability Main Save Method
        public int SaveAllCompanyCapability
            (int? reconciliationPeriodID
            , bool isDualReviewActivated
            , bool isDualReviewTypeToBeSaved
            , bool? isMaterialityActivated
            , bool isRiskRatingActivated
            , bool isRoleMandatoryReportActivated
            , bool IsRCCLValidationTypeToBeSaved
            , bool IsMaterialityToBeSaved
            , List<CompanyCapabilityInfo> oCompanyCapabilityInfoCollection
            , CompanySettingInfo oCompanySettingInfo, IList<FSCaptionMaterialityInfo> oFSCaptionMaterialityInfoCollection
            , IList<RoleMandatoryReportInfo> oRoleMandatoryReportInfoCollection
            , IList<RiskRatingReconciliationFrequencyInfo> oRiskRatingReconciliationFrequencyInfoCollection, IList<RiskRatingReconciliationPeriodInfo> oRiskRatingReconciliationPeriodInfoCollection, bool IsRiskRatingValueChanged
            , IList<ReconciliationPeriodInfo> oReconciliationPeriodInfoCollection
            , CompanyUnexplainedVarianceThresholdInfo oCompanyUnexplainedVarianceThresholdInfo
            , bool IsDayTypeToBeSaved
            , DateTime dateRevised
            , string revisedBy
            , AppUserInfo oAppUserInfo
            )
        {
            //TODO: returnValue= ?, what is the standard?
            int returnValue = 0;
            IDbConnection oConnection = null;
            IDbTransaction oTransaction = null;
            try
            {
                //bool? isDualReview = true;
                ServiceHelper.SetConnectionString(oAppUserInfo);
                CompanyCapabilityDAO oCompanyCapabilityDAO = new CompanyCapabilityDAO(oAppUserInfo);

                oConnection = oCompanyCapabilityDAO.CreateConnection();
                oConnection.Open();
                oTransaction = oConnection.BeginTransaction();

                int? _companyID = oCompanySettingInfo.CompanyID;


                CompanySettingDAO oCompanySettingDAO = new CompanySettingDAO(oAppUserInfo);
                oCompanySettingDAO.MarkForReprocessing(isMaterialityActivated, reconciliationPeriodID, oFSCaptionMaterialityInfoCollection
                    , oCompanySettingInfo, oConnection, oTransaction, ARTEnums.GLDataProcessingStatus.ToBeProcessed, revisedBy, dateRevised);

                oCompanyCapabilityDAO.SaveCompanyCapabilityTableValue(reconciliationPeriodID, oCompanyCapabilityInfoCollection, oConnection, oTransaction, dateRevised, revisedBy);

                if (isMaterialityActivated.HasValue && isMaterialityActivated.Value && IsMaterialityToBeSaved)
                {
                    oCompanySettingDAO.UpdateMaterialityTypeSetting(reconciliationPeriodID, oFSCaptionMaterialityInfoCollection, oCompanySettingInfo, oConnection, oTransaction);
                }
                if (isDualReviewActivated && isDualReviewTypeToBeSaved)
                {
                    oCompanySettingDAO.UpdateDualLevelReviewType(reconciliationPeriodID, oCompanySettingInfo, oConnection, oTransaction);
                }

                if (isRoleMandatoryReportActivated && oRoleMandatoryReportInfoCollection != null && oRoleMandatoryReportInfoCollection.Count > 0)
                {
                    RoleMandatoryReportDAO oRoleMandatoryReportDAO = new RoleMandatoryReportDAO(oAppUserInfo);
                    int intReturnRoleMandatoryReport = oRoleMandatoryReportDAO.InsertRoleMandatoryReportByTableValue(oRoleMandatoryReportInfoCollection, reconciliationPeriodID, isDualReviewActivated, oConnection, oTransaction);
                }
                //}
                if (isRiskRatingActivated && IsRiskRatingValueChanged)
                {
                    RiskRating oRiskRatingClient = new RiskRating();
                    RiskRatingReconciliationFrequencyDAO oRiskRatingReconciliationFrequencyDAO = new RiskRatingReconciliationFrequencyDAO(oAppUserInfo);
                    int intReturnRiskRatingReconciliationFrequencyAndPeriod = oRiskRatingReconciliationFrequencyDAO.SaveRiskRatingReconciliationFrequencyAndPeriodTableValue(oRiskRatingReconciliationFrequencyInfoCollection, oRiskRatingReconciliationPeriodInfoCollection, reconciliationPeriodID, dateRevised, revisedBy, oConnection, oTransaction);
                }
                ReconciliationPeriodDAO oReconciliationPeriodDAO = new ReconciliationPeriodDAO(oAppUserInfo);


                if (oCompanyUnexplainedVarianceThresholdInfo != null)
                {
                    CompanyUnexplainedVarianceThresholdDAO oCompanyUnexplainedVarianceThresholdDAO = new CompanyUnexplainedVarianceThresholdDAO(oAppUserInfo);
                    bool isThresholdChanged;
                    oCompanyUnexplainedVarianceThresholdDAO.UpdateCompanyUnexplainedVarianceThreshold(reconciliationPeriodID, oCompanyUnexplainedVarianceThresholdInfo, oConnection, oTransaction, out isThresholdChanged);
                    CompanySettingDAO oCompanySetting = new CompanySettingDAO(oAppUserInfo);

                    //If threshold has changed, Update ReconciliationPeriod for marking GLData re processing
                    if (isThresholdChanged)
                        oCompanySetting.UpdateReconciliationPeriodForReProcessing(reconciliationPeriodID, ARTEnums.GLDataProcessingStatus.ToBeProcessed, revisedBy, dateRevised, oConnection, oTransaction);
                }
                if (IsRCCLValidationTypeToBeSaved)
                {
                    oCompanySettingDAO.UpdateRCCValidationType(reconciliationPeriodID, oCompanySettingInfo, oConnection, oTransaction);
                }
                if (IsDayTypeToBeSaved)
                {
                    oCompanySettingDAO.UpdateCompanyDayType(reconciliationPeriodID, revisedBy, dateRevised, oCompanySettingInfo, oConnection, oTransaction);
                }
                oTransaction.Commit();
                returnValue = 0;
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
            return returnValue;

        }


        public string GetBaseCurrencyByRecPeriodID(int recPeriodID, AppUserInfo oAppUserInfo)
        {
            string baseCurrencyCode = null;

            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                ReconciliationPeriodDAO oReconciliationPeriodDAO = new ReconciliationPeriodDAO(oAppUserInfo);
                baseCurrencyCode = oReconciliationPeriodDAO.GetBaseCurrencyByCompanyID(recPeriodID);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }

            return baseCurrencyCode;
        }

        public string GetReportingCurrencyByRecPeriodID(int recPeriodID, AppUserInfo oAppUserInfo)
        {
            string reportingCurrencyCode = null;

            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                ReconciliationPeriodDAO oReconciliationPeriodDAO = new ReconciliationPeriodDAO(oAppUserInfo);
                reportingCurrencyCode = oReconciliationPeriodDAO.GetReportingCurrencyByCompanyID(recPeriodID);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }

            return reportingCurrencyCode;
        }


        public int UpdateSystemWideSetting
                (int? companyID
                , int selectedRecPeriodID
                , bool isPeriodMarkedOpen
                , DateTime? dateRevised
                , string revisedBy
                , short actionTypeID
                , short changeSourceIDSRA
                , List<ReconciliationPeriodInfo> oReconciliationPeriodInfoCollection
                , CompanySettingInfo oCompanySettingInfo
                , AppUserInfo oAppUserInfo
            )
        {
            int returnValue = 0;
            IDbConnection oConnection = null;
            IDbTransaction oTransaction = null;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                ReconciliationPeriodDAO oReconciliationPeriodDAO = new ReconciliationPeriodDAO(oAppUserInfo);

                oConnection = oReconciliationPeriodDAO.CreateConnection();
                oConnection.Open();
                oTransaction = oConnection.BeginTransaction();

                oReconciliationPeriodDAO.UpdateSystemWideSettingDates(companyID, oReconciliationPeriodInfoCollection, oConnection, oTransaction);

                int? reconciliationPeriodStatusID = null;
                //TODO: set status SKIPPED for those skipped periods.
                if (isPeriodMarkedOpen)
                {
                    reconciliationPeriodStatusID = 2;//TODO: its hardcoded for status= OPEN 
                }

                oReconciliationPeriodDAO.UpdateReconciliationPeriodStatusAndMarkSkipped(selectedRecPeriodID, reconciliationPeriodStatusID, dateRevised, revisedBy, actionTypeID, changeSourceIDSRA, oConnection, oTransaction);


                CompanySettingDAO oCompanySettingDAO = new CompanySettingDAO(oAppUserInfo);
                oCompanySettingDAO.UpdateByCompanyID(oCompanySettingInfo, oConnection, oTransaction);

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
            return returnValue;
        }



        #region ICompany Members


        public bool HasUserLimitReached(int? CompanyID, AppUserInfo oAppUserInfo)
        {
            bool bLimitReached = false;

            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                CompanyHdrDAO oCompanyHdrDAO = new CompanyHdrDAO(oAppUserInfo);
                bLimitReached = oCompanyHdrDAO.HasUserLimitReached(CompanyID);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }

            return bLimitReached;
        }

        public void GetCompanyDataStorageCapacityAndCurrentUsage(int companyID, out decimal? dataStorageCapacity, out decimal? currentUsage, AppUserInfo oAppUserInfo)
        {
            dataStorageCapacity = 0M;
            currentUsage = 0M;

            try
            {
                oAppUserInfo.CompanyID = companyID;
                ServiceHelper.SetConnectionString(oAppUserInfo);
                if (oAppUserInfo.IsDatabaseExists.GetValueOrDefault())
                {
                    CompanyHdrDAO oCompanyHdrDAO = new CompanyHdrDAO(oAppUserInfo);
                    oCompanyHdrDAO.GetCompanyDataStorageCapacityAndCurrentUsage(companyID, out dataStorageCapacity, out currentUsage);
                }
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

        public string GetCompanyLogo(int? CompanyID, AppUserInfo oAppUserInfo)
        {
            string logoPath = null;

            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                CompanyHdrDAO oCompanyHdrDAO = new CompanyHdrDAO(oAppUserInfo);
                logoPath = oCompanyHdrDAO.GetCompanyLogo(CompanyID);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }

            return logoPath;
        }

        #endregion

        #region Financial Year

        public bool UpdateFinancialYear(FinancialYearHdrInfo oFinancialYearHdrInfo, AppUserInfo oAppUserInfo)
        {
            IDbConnection oConnection = null;
            IDbTransaction oTransaction = null;
            ServiceHelper.SetConnectionString(oAppUserInfo);
            FinancialYearHdrDAO oFinancialYearHdrDAO = new FinancialYearHdrDAO(oAppUserInfo);
            bool UpdateFinancialYearSucess;
            //*************Below Code Uniqueness of Financial year *********
            bool test = oFinancialYearHdrDAO.IsUniqueFiancialYear(oFinancialYearHdrInfo);
            if (!test)
                throw new ARTException(5000204);
            //*********************************************************************************

            //*************Below Code Uniqueness of Financial year *********
            bool isExclusiveDateRange = oFinancialYearHdrDAO.IsExclusiveDateRange(oFinancialYearHdrInfo);
            if (!isExclusiveDateRange)
                throw new ARTException(5000227);
            //*********************************************************************************

            try
            {
                oConnection = oFinancialYearHdrDAO.CreateConnection();
                oConnection.Open();
                oTransaction = oConnection.BeginTransaction();
                UpdateFinancialYearSucess = oFinancialYearHdrDAO.UpdateFinancialYear(oFinancialYearHdrInfo, oConnection, oTransaction); ;
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
                    {
                        oConnection.Close();
                        oConnection.Dispose();
                    }
                }
                catch (Exception)
                {
                }
            }
            return UpdateFinancialYearSucess;
        }

        public bool InsertFinancialYear(FinancialYearHdrInfo oFinancialYearHdrInfo, AppUserInfo oAppUserInfo)
        {
            IDbConnection oConnection = null;
            IDbTransaction oTransaction = null;
            ServiceHelper.SetConnectionString(oAppUserInfo);
            FinancialYearHdrDAO oFinancialYearHdrDAO = new FinancialYearHdrDAO(oAppUserInfo);
            bool FinancialYearCreatedSucess;

            //*************Below Code Uniqueness of Financial year *********
            bool test = oFinancialYearHdrDAO.IsUniqueFiancialYear(oFinancialYearHdrInfo);
            if (!test)
                throw new ARTException(5000204);
            //*********************************************************************************

            //*************Below Code Uniqueness of Financial year *********
            bool isExclusiveDateRange = oFinancialYearHdrDAO.IsExclusiveDateRange(oFinancialYearHdrInfo);
            if (!isExclusiveDateRange)
                throw new ARTException(5000227);
            //*********************************************************************************

            try
            {
                oConnection = oFinancialYearHdrDAO.CreateConnection();
                oConnection.Open();
                oTransaction = oConnection.BeginTransaction();

                FinancialYearCreatedSucess = oFinancialYearHdrDAO.InsertNewFinancialYear(oFinancialYearHdrInfo, oConnection, oTransaction);

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
                    {
                        oConnection.Close();
                        oConnection.Dispose();
                    }
                }
                catch (Exception)
                {
                }
            }
            return FinancialYearCreatedSucess;
        }
        public List<FinancialYearHdrInfo> GetAllFinancialYearList(int? CompanyID, AppUserInfo oAppUserInfo)
        {
            List<FinancialYearHdrInfo> oFinancialYearHdrInfoCollection = null;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                FinancialYearHdrDAO oFinancialYearHdrDAO = new FinancialYearHdrDAO(oAppUserInfo);
                oFinancialYearHdrInfoCollection = (List<FinancialYearHdrInfo>)oFinancialYearHdrDAO.SelectAllFinancialYearByCompanyID(CompanyID);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return oFinancialYearHdrInfoCollection;

        }

        public FinancialYearHdrInfo GetFinancialYearByID(int? FinancialYearID, AppUserInfo oAppUserInfo)
        {
            IList<FinancialYearHdrInfo> oFinancialYearHdrInfoCollection = null;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                FinancialYearHdrDAO oFinancialYearHdrDAO = new FinancialYearHdrDAO(oAppUserInfo);
                oFinancialYearHdrInfoCollection = (IList<FinancialYearHdrInfo>)oFinancialYearHdrDAO.SelectFinancialYearByID(FinancialYearID);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            if (oFinancialYearHdrInfoCollection.Count > 0)

                return oFinancialYearHdrInfoCollection[0];
            else
                return null;

        }

        #endregion

        public List<FeatureMstInfo> GetFeaturesByCompanyID(int? CompanyID, int? recPeriodID, AppUserInfo oAppUserInfo)
        {
            List<FeatureMstInfo> oFeatureMstInfoCollection = null;

            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                CompanyHdrDAO oCompanyHdrDAO = new CompanyHdrDAO(oAppUserInfo);
                oFeatureMstInfoCollection = oCompanyHdrDAO.GetFeaturesByCompanyID(CompanyID, recPeriodID);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }

            return oFeatureMstInfoCollection;
        }

        public IList<CompanyWeekDayInfo> GetComapnyWorkWeek(int? RecPeriodID, int? CompanyID, AppUserInfo oAppUserInfo)
        {

            IList<CompanyWeekDayInfo> oCompanyWorkWeekInfoCollection = null;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                CompanyHdrDAO oCompanyDAO = new CompanyHdrDAO(oAppUserInfo);
                oCompanyWorkWeekInfoCollection = oCompanyDAO.GetComapnyWorkWeek(RecPeriodID, CompanyID);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return oCompanyWorkWeekInfoCollection;

        }

        public void SaveWorkWeek(List<CompanyWeekDayInfo> oCompanyWorkWeekInfoCollection, int? CompanyID, string AddedBy, DateTime? DateAdded, string RevisedBy, DateTime? DateRevised, AppUserInfo oAppUserInfo)
        {
            IDbConnection oConnection = null;
            IDbTransaction oTransaction = null;

            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                //List<short> oSRARuleIDCollection = (from SRARule in oCompanySRARuleInfoCollection select SRARule.SystemReconciliationRuleID.Value).ToList();
                DataTable dtCompanyWorkWeek = ServiceHelper.ConvertCompanyWorkWeekInfoToDataTable(oCompanyWorkWeekInfoCollection);

                CompanyWeekDayDAO oCompanyWeekDayDAO = new CompanyWeekDayDAO(oAppUserInfo);
                oConnection = oCompanyWeekDayDAO.CreateConnection();
                oConnection.Open();
                oTransaction = oConnection.BeginTransaction();
                if (oCompanyWorkWeekInfoCollection != null && oCompanyWorkWeekInfoCollection.Count > 0)
                {
                    CompanyWeekDayInfo oCompanyWorkWeekInfo = oCompanyWorkWeekInfoCollection[0];
                    oCompanyWeekDayDAO.SaveCompanyWorkWeek(dtCompanyWorkWeek, CompanyID, oCompanyWorkWeekInfo.StartRecPeriodID, oCompanyWorkWeekInfo.EndRecPeriodID, AddedBy, DateAdded, RevisedBy, DateRevised, oConnection, oTransaction);
                    oCompanyWeekDayDAO.AdjustDueDates(CompanyID, oCompanyWorkWeekInfo.StartRecPeriodID, oConnection, oTransaction);

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

        public IList<CompanyWeekDayInfo> SelectAllWorkWeekByFinancialYearIDAndCompanyID(int? companyID, int? FinancialYearID, AppUserInfo oAppUserInfo)
        {
            IList<CompanyWeekDayInfo> oCompanyWeekDayInfoCollection = null;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                ReconciliationPeriodDAO oReconciliationPeriodDAO = new ReconciliationPeriodDAO(oAppUserInfo);
                oCompanyWeekDayInfoCollection = oReconciliationPeriodDAO.SelectAllWorkWeekByFinancialYearIDAndCompanyID(companyID, FinancialYearID);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return oCompanyWeekDayInfoCollection;
        }


        public List<SubledgerSourceInfo> SelectAllSubledgerSourceByCompanyID(int? companyID, AppUserInfo oAppUserInfo)
        {

            List<SubledgerSourceInfo> oSubledgerSourceInfoCollection = null;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                SubledgerSourceDAO oSubledgerSourceDAO = new SubledgerSourceDAO(oAppUserInfo);
                oSubledgerSourceInfoCollection = oSubledgerSourceDAO.SelectAllSubledgerSourceByCompanyID(companyID);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return oSubledgerSourceInfoCollection;
        }

        public List<CompanyHdrInfo> SelectAllCompaniesForDatabaseCreation(AppUserInfo oAppUserInfo)
        {
            List<CompanyHdrInfo> oCompanyHdrInfoList = null;
            try
            {
                ServiceHelper.SetConnectionStringCore(oAppUserInfo);
                CompanyHdrDAO oCompanyHdrDAO = new CompanyHdrDAO(oAppUserInfo);
                oCompanyHdrInfoList = oCompanyHdrDAO.SelectAllCompaniesForDatabaseCreation();
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return oCompanyHdrInfoList;
        }

        public bool? IsDatabaseExists(int? companyID)
        {
            AppUserInfo oAppUserInfo = null;
            try
            {
                oAppUserInfo = new AppUserInfo();
                oAppUserInfo.CompanyID = companyID;
                ServerCompanyDAO oServerCompanyDAO = new ServerCompanyDAO(oAppUserInfo);
                ServerCompanyInfo oServerCompanyInfo = oServerCompanyDAO.GetCompanyServer(oAppUserInfo.CompanyID);
                return oServerCompanyInfo.IsDatabaseExists;
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

        public bool CreateDatabaseAndCompany(CompanyHdrInfo oCompanyHdrInfo, ContactInfo oContactInfo, List<UserHdrInfo> oUserHdrInfoList, int languageID, string BaseDBPath, AppUserInfo oAppUserInfo)
        {
            bool bSuccess = false;
            try
            {
                bool bDatabaseCreated = false;
                ServerCompanyDAO oServerCompanyDAO = new ServerCompanyDAO(oAppUserInfo);
                ServerCompanyInfo oServerCompanyInfo = oServerCompanyDAO.GetCompanyServer(oAppUserInfo.CompanyID);
                ServiceHelper.SetConnectionStringCreateCompany(oServerCompanyInfo, oAppUserInfo);
                CompanyHdrDAO oCompanyHdrDAO = new CompanyHdrDAO(oAppUserInfo);
                oCompanyHdrDAO.CreateCompanyDatabase(oServerCompanyInfo, BaseDBPath);
                bDatabaseCreated = true;
                try
                {
                    using (TransactionScope scope = new TransactionScope())
                    {
                        // Insert Company Database Mapping and Create Company
                        CreateCompanyAndMapping(oCompanyHdrInfo, oContactInfo, languageID, oAppUserInfo);
                        oAppUserInfo.IsDatabaseExists = true;
                        User oUser = new User();
                        foreach (UserHdrInfo oUserHdrInfo in oUserHdrInfoList)
                        {
                            List<int> roleList = new List<int>();
                            foreach (UserRoleInfo oUserRoleInfo in oUserHdrInfo.UserRoleInfoList)
                                roleList.Add(oUserRoleInfo.RoleID.Value);
                            oUser.InsertUser(oUserHdrInfo, roleList, false, oAppUserInfo);
                        }
                        oUser.DeleteUsersFromTransit(oCompanyHdrInfo.CompanyID, oAppUserInfo);
                        ContactDAO oContactDAO = new ContactDAO(oAppUserInfo);
                        oContactDAO.DeleteContactsFromTransit(oCompanyHdrInfo.CompanyID);
                        scope.Complete();
                    }
                }
                catch (SqlException ex)
                {
                    if (bDatabaseCreated)
                    {
                        ServiceHelper.SetConnectionStringCreateCompany(oServerCompanyInfo, oAppUserInfo);
                        CompanyHdrDAO oCompanyHdrDAO1 = new CompanyHdrDAO(oAppUserInfo);
                        oCompanyHdrDAO.DropCompanyDatabase(oServerCompanyInfo);
                    }
                    ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
                }
                catch (Exception ex)
                {
                    if (bDatabaseCreated)
                    {
                        ServiceHelper.SetConnectionStringCreateCompany(oServerCompanyInfo, oAppUserInfo);
                        CompanyHdrDAO oCompanyHdrDAO1 = new CompanyHdrDAO(oAppUserInfo);
                        oCompanyHdrDAO.DropCompanyDatabase(oServerCompanyInfo);
                    }
                    ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
                }
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return bSuccess;
        }

        public IList<ReconciliationPeriodInfo> SelectAllRecPeriodNumberByCompanyID(int? companyID, DateTime? periodEndDate, AppUserInfo oAppUserInfo)
        {
            IList<ReconciliationPeriodInfo> oReconciliationPeriodInfoCollection = null;
            try
            {
                if (oAppUserInfo.IsDatabaseExists.GetValueOrDefault())
                {
                    ServiceHelper.SetConnectionString(oAppUserInfo);
                    ReconciliationPeriodDAO oReconciliationPeriodDAO = new ReconciliationPeriodDAO(oAppUserInfo);
                    oReconciliationPeriodInfoCollection = oReconciliationPeriodDAO.SelectAllPeriodNumberByCompanyID(companyID, periodEndDate);
                }
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return oReconciliationPeriodInfoCollection;
        }
        public IList<CompanySettingInfo> SelectCompanyDualLevelReviewType(int? reconciliationPeriodID, AppUserInfo oAppUserInfo)
        {
            IList<CompanySettingInfo> oCompanySettingInfoCollection = null;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                CompanySettingDAO oCompanySettingDAO = new CompanySettingDAO(oAppUserInfo);
                oCompanySettingInfoCollection = oCompanySettingDAO.SelectCompanyDualLevelReviewType(reconciliationPeriodID);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return oCompanySettingInfoCollection;
        }

        public IList<CompanySettingInfo> SelectCompanyDayType(int? reconciliationPeriodID, AppUserInfo oAppUserInfo)
        {
            IList<CompanySettingInfo> oCompanySettingInfoCollection = null;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                CompanySettingDAO oCompanySettingDAO = new CompanySettingDAO(oAppUserInfo);
                oCompanySettingInfoCollection = oCompanySettingDAO.SelectCompanyDayType(reconciliationPeriodID);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return oCompanySettingInfoCollection;
        }

        public IList<CompanySettingInfo> SelectCompanyRCCLValidationType(int? reconciliationPeriodID, AppUserInfo oAppUserInfo)
        {
            IList<CompanySettingInfo> oCompanySettingInfoList = null;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                CompanySettingDAO oCompanySettingDAO = new CompanySettingDAO(oAppUserInfo);
                oCompanySettingInfoList = oCompanySettingDAO.SelectCompanyRCCLValidationType(reconciliationPeriodID);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return oCompanySettingInfoList;
        }

        public void UpdateCompanyDataStorageCapacityAndCurrentUsage(int CurrentCompanyID, decimal FileSizeInMB,string RevisedBy,DateTime DateRevised, AppUserInfo appUserInfo)
        {
            IDbConnection oConnection = null;
            IDbTransaction oTransaction = null;
            try
            {
                ServiceHelper.SetConnectionString(appUserInfo);
                CompanySettingDAO oCompanySettingDAO = new CompanySettingDAO(appUserInfo);
                oConnection = oCompanySettingDAO.CreateConnection();
                oConnection.Open();
                oTransaction = oConnection.BeginTransaction();
                oCompanySettingDAO.UpdateCompanyDataStorageCapacityAndCurrentUsage(CurrentCompanyID, FileSizeInMB, RevisedBy, DateRevised,oConnection, oTransaction);
                oTransaction.Commit();
            }
            catch (ARTException ex)
            {
                if (oConnection != null && oConnection.State != ConnectionState.Closed && oTransaction != null)
                    oTransaction.Rollback();
                throw ex;
            }
            catch (SqlException ex)
            {
                if (oConnection != null && oConnection.State != ConnectionState.Closed && oTransaction != null)
                    oTransaction.Rollback();
                ServiceHelper.LogAndThrowGenericSqlException(ex, appUserInfo);
            }
            catch (Exception ex)
            {
                if (oConnection != null && oConnection.State != ConnectionState.Closed && oTransaction != null)
                    oTransaction.Rollback();
                ServiceHelper.LogAndThrowGenericException(ex, appUserInfo);
            }

            finally
            {
                if ((null != oConnection) && (oConnection.State == ConnectionState.Open))
                    oConnection.Dispose();
            }
        }

        public List<FTPServerInfo> GetAllFTPServerListObject(int? CompanyId, AppUserInfo oAppUserInfo)
        {
            List<FTPServerInfo> oFTPServerInfoCollection = null;
            try
            {
                ServiceHelper.SetConnectionStringCore(oAppUserInfo);
                CompanyHdrDAO oCompanyHdrDAO = new CompanyHdrDAO(oAppUserInfo);
                oFTPServerInfoCollection = oCompanyHdrDAO.GetAllFTPServerListObject(CompanyId);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return oFTPServerInfoCollection;
        }

        public bool? IsCompanyAliasUnique(string companyAlias, int? CompanyID, AppUserInfo oAppUserInfo)
        {
            try
            {
                // User Duplicity should be checked in core
                ServiceHelper.SetConnectionStringCore(oAppUserInfo);
                CompanyHdrDAO oCompanyHdrDAO = new CompanyHdrDAO(oAppUserInfo);
                return oCompanyHdrDAO.IsCompanyAliasUnique(companyAlias, CompanyID);
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
