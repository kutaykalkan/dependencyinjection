using System;
using System.Collections.Generic;
using SkyStem.ART.Client.IServices;
using SkyStem.ART.Client.Model;
using SkyStem.ART.Client.Exception;
using SkyStem.ART.App.Utility;
using SkyStem.ART.Client.Data;
using System.Data.SqlClient;
using SkyStem.ART.App.DAO;
using System.Data;
using SkyStem.ART.Client.Params;
using SkyStem.ART.App.DAO.QualityScore;
using SkyStem.ART.App.BLL;

namespace SkyStem.ART.App.Services
{
    // NOTE: If you change the class name "GLData" here, you must also update the reference to "GLData" in Web.config.
    public class GLData : IGLData
    {

        public List<GLDataHdrInfo> SelectGLDataHdrByGLDataID(long? gLDataID, AppUserInfo oAppUserInfo)
        {
            List<GLDataHdrInfo> oGLDataHdrInfoCollection = null;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                GLDataHdrDAO oGLDataHdrDAO = new GLDataHdrDAO(oAppUserInfo);
                oGLDataHdrInfoCollection = oGLDataHdrDAO.SelectGLDataHdrByGLDataID(gLDataID);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return oGLDataHdrInfoCollection;
        }


        public int? GetCountAttachedDocumentByGLDataID(long? gLDataID, AppUserInfo oAppUserInfo)
        {
            int? countDocument = null;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                GLDataHdrDAO oGLDataHdrDAO = new GLDataHdrDAO(oAppUserInfo);
                countDocument = oGLDataHdrDAO.GetCountAttachedDocumentByGLDataID(gLDataID);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return countDocument;
        }


        public int? GetCountGLReviewNoteByGLDataID(long? gLDataID, AppUserInfo oAppUserInfo)
        {
            int? countDocument = null;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                GLDataHdrDAO oGLDataHdrDAO = new GLDataHdrDAO(oAppUserInfo);
                countDocument = oGLDataHdrDAO.GetCountGLReviewNoteByGLDataID(gLDataID);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return countDocument;
        }

        public List<GLDataAndAccountHdrInfo> GetGLDataAndAccountHdrInfoByGLDataID(int? inputGLDataID, AppUserInfo oAppUserInfo)
        {
            ServiceHelper.SetConnectionString(oAppUserInfo);
            List<GLDataAndAccountHdrInfo> lstGLDataAndAccountHdrInfo = new List<GLDataAndAccountHdrInfo>();
            return lstGLDataAndAccountHdrInfo;
        }

        public List<GLDataHdrInfo> SelectGLDataAndAccountInfoByUserID(
                                                                    List<FilterCriteria> oFilterCriteriaCollection
                                                                    , int recPeriodID
                                                                    , int companyID
                                                                    , int userID
                                                                    , short userRoleID
                                                                    , bool isDualReviewEnabled
                                                                    , bool isMaterialityEnabled
                                                                    , short preparerAttributeID
                                                                    , short reviewerAttributeID
                                                                    , short approverAttributeID
                                                                    , short preparerRoleID
                                                                    , short reviewerRoleID
                                                                    , short approverRoleID
                                                                    , short systemAdminRoleID
                                                                    , short CEO_CFORoleID
                                                                    , short skyStemAdminRoleID
                                                                    , bool IsIncludeSRA
                                                                    , int? count
                                                                    , List<Int16> AccountAttributeIDCollection
                                                                    , int languageID
                                                                    , int businessEntityID
                                                                    , int defaultLanguageID
                                                                    , string sortExpression
                                                                    , string sortDirection
                                                                    , AppUserInfo oAppUserInfo
                                                                )
        {
            List<GLDataHdrInfo> oGLDataHdrInfoCollection = null;

            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                DataTable dtFilterCriteria = ServiceHelper.ConvertFilterCriteriaIntoDataTable(oFilterCriteriaCollection);
                GLDataHdrDAO oGLDataHdrDAO = new GLDataHdrDAO(oAppUserInfo);
                oGLDataHdrInfoCollection = oGLDataHdrDAO.SelectGLDataAndAccountInfoByUserID(dtFilterCriteria, recPeriodID,
                    companyID, userID, userRoleID, isDualReviewEnabled, isMaterialityEnabled,
                    preparerAttributeID, reviewerAttributeID, approverAttributeID,
                    preparerRoleID, reviewerRoleID, approverRoleID, systemAdminRoleID,
                    CEO_CFORoleID, skyStemAdminRoleID, IsIncludeSRA, count, AccountAttributeIDCollection,
                    languageID, businessEntityID, defaultLanguageID, sortExpression, sortDirection);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }

            return oGLDataHdrInfoCollection;
        }




        public List<GLDataHdrInfo> SelectGLDataAndAccountInfoByUserIDForCertificationBalances(
                                                            int recPeriodID
                                                            , int companyID
                                                            , int userID
                                                            , short userRoleID
                                                            , bool isDualReviewEnabled
                                                            , bool isMaterialityEnabled
                                                            , short preparerAttributeID
                                                            , short reviewerAttributeID
                                                            , short approverAttributeID
                                                            , short preparerRoleID
                                                            , short reviewerRoleID
                                                            , short approverRoleID
                                                            , short systemAdminRoleID
                                                            , short CEO_CFORoleID
                                                            , short skyStemAdminRoleID
                                                            , bool IsIncludeSRA
                                                            , int? count
                                                            , List<Int16> AccountAttributeIDCollection
                                                            , int languageID
                                                            , int businessEntityID
                                                            , int defaultLanguageID
                                                            , AppUserInfo oAppUserInfo
            //, string sortExpression
            //, string sortDirection
                                                        )
        {
            List<GLDataHdrInfo> oGLDataHdrInfoCollection = null;

            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                GLDataHdrDAO oGLDataHdrDAO = new GLDataHdrDAO(oAppUserInfo);
                oGLDataHdrInfoCollection = oGLDataHdrDAO.SelectGLDataAndAccountInfoByUserIDForCertificationBalances(recPeriodID,
                    companyID, userID, userRoleID, isDualReviewEnabled, isMaterialityEnabled,
                    preparerAttributeID, reviewerAttributeID, approverAttributeID,
                    preparerRoleID, reviewerRoleID, approverRoleID, systemAdminRoleID,
                    CEO_CFORoleID, skyStemAdminRoleID, IsIncludeSRA, count, AccountAttributeIDCollection,
                    languageID, businessEntityID, defaultLanguageID);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }

            return oGLDataHdrInfoCollection;
        }




        public AccountCountInfo GetTotalAndCompletedAccountCount(
                                                        int recPeriodID
                                                        , int companyID
                                                        , int userID
                                                        , short userRoleID
                                                        , bool isDualReviewEnabled
                                                        , short preparerAttributeID
                                                        , short reviewerAttributeID
                                                        , short approverAttributeID
                                                        , short preparerRoleID
                                                        , short reviewerRoleID
                                                        , short approverRoleID
                                                        , short systemAdminRoleID
                                                        , short CEO_CFORoleID
                                                        , short skyStemAdminRoleID
                                                        , short sysReconciledStatusID
                                                        , bool isIncludeSRA
                                                        , AppUserInfo oAppUserInfo
                                                    )
        {
            AccountCountInfo oAccountCountInfo = null;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                GLDataHdrDAO oGLDataHdrDAO = new GLDataHdrDAO(oAppUserInfo);
                oAccountCountInfo = oGLDataHdrDAO.GetTotalAndCompletedAccountCount(recPeriodID,
                    companyID, userID, userRoleID, isDualReviewEnabled,
                    preparerAttributeID, reviewerAttributeID, approverAttributeID,
                    preparerRoleID, reviewerRoleID, approverRoleID, systemAdminRoleID,
                    CEO_CFORoleID, skyStemAdminRoleID, sysReconciledStatusID, isIncludeSRA);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }

            return oAccountCountInfo;
        }



        public bool SaveGLDataReconciliationStatus(List<long> oGLDataIDCollection, int currentRecPeriodId, short reconciliationStatusID, string userLoginID, DateTime dateRevised, short actionTypeID, short changeSourceIDSRA, AppUserInfo oAppUserInfo)
        {
            bool result = false;
            IDbConnection oConnection = null;
            IDbTransaction oTransaction = null;

            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                DataTable dtGlDataIds = ServiceHelper.ConvertLongIDCollectionToDataTable(oGLDataIDCollection);
                //DataTable dtNetAccountIds = ServiceHelper.ConvertIDCollectionToDataTable(oNetAccountIDCollection);
                GLDataHdrDAO oGLDataHdrDAO = new GLDataHdrDAO(oAppUserInfo);
                oConnection = oGLDataHdrDAO.CreateConnection();
                oConnection.Open();
                oTransaction = oConnection.BeginTransaction();
                result = oGLDataHdrDAO.SaveGLDataReconciliationStatus(dtGlDataIds, currentRecPeriodId, reconciliationStatusID, userLoginID, dateRevised, actionTypeID, changeSourceIDSRA, oConnection, oTransaction);

                //Additional code Added By Prafull on 15-Feb-2011
                ReconciliationPeriodDAO oReconciliationPeriodDAO = new ReconciliationPeriodDAO(oAppUserInfo);
                oReconciliationPeriodDAO.UpdateRecPeriodStatusAsInProgress(currentRecPeriodId, oConnection, oTransaction);

                oTransaction.Commit();
                oTransaction.Dispose();
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

            return result;
        }


        public GLDataAndAccountHdrInfo GetGLDataAndAccountInfoByGLDataID(long glDataID, int recPeriodID, int companyID, int userID, int roleID, bool isDualReviewEnabled, bool isCertificationEnabled, bool isMaterialityEnabled, short certificationTypeID
            , short preparerAttributeId, short reviewerAttributeId, short approverAttributeId,
             short backupPreparerAttributeId, short backupReviewerAttributeId, short backupApproverAttributeId, AppUserInfo oAppUserInfo)
        {
            GLDataAndAccountHdrInfo oGLDataAndAccountHdrInfo = null;

            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                GLDataHdrDAO oGLDataHdrDAO = new GLDataHdrDAO(oAppUserInfo);
                oGLDataAndAccountHdrInfo = oGLDataHdrDAO.GetGLDataAndAccountInfoByGLDataID(glDataID, recPeriodID, companyID, userID, roleID,
                    isDualReviewEnabled, isCertificationEnabled, isMaterialityEnabled, certificationTypeID, preparerAttributeId,
                    reviewerAttributeId, approverAttributeId, backupPreparerAttributeId, backupReviewerAttributeId, backupApproverAttributeId);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }

            return oGLDataAndAccountHdrInfo;
        }



        public List<GLDataAndAccountHdrInfo> GLDataIDAndRecTemplateIDByAccountIDAndRecPeriodID(long? accountID, int? netAccountID, int? recPeriodID, int? companyID, short? recTemplateAttributeID, AppUserInfo oAppUserInfo)
        {
            List<GLDataAndAccountHdrInfo> oGLDataAndAccountHdrInfoCollection = null;

            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                GLDataHdrDAO oGLDataHdrDAO = new GLDataHdrDAO(oAppUserInfo);
                oGLDataAndAccountHdrInfoCollection = oGLDataHdrDAO.GLDataIDAndRecTemplateIDByAccountIDAndRecPeriodID(accountID, netAccountID, recPeriodID, companyID, recTemplateAttributeID);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }

            return oGLDataAndAccountHdrInfoCollection;
        }


        /// <summary>
        /// Save on button click on template forms
        /// , it saves New ReconciliationStatusID and bank details etc if applicable
        /// </summary>
        /// <param name="gLDataID"></param>
        /// <param name="oGLDataHdrInfo"></param>
        /// <returns></returns>
        public int SaveReconciliationForm(
            long? gLDataID
            , GLDataHdrInfo oGLDataHdrInfo
            , bool isFormDataToBeSaved
            , bool isSignOff
            , bool saveSupportingDetailSection
            , bool isBankFormTemplate
            , DerivedCalculationSupportingDetailInfo oDerivedCalculationSupportingDetailInfo
            , short? reconciliationCategoryTypeIDForSupportingDetail
            , AppUserInfo oAppUserInfo)
        {
            int returnValue = 0;
            IDbConnection oConnection = null;
            IDbTransaction oTransaction = null;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                //TODO: save signoff dates 
                GLDataHdrDAO oGLDataHdrDAO = new GLDataHdrDAO(oAppUserInfo);
                ReconciliationPeriodDAO oReconciliationPeriodDAO = new ReconciliationPeriodDAO(oAppUserInfo);

                oConnection = oGLDataHdrDAO.CreateConnection();
                oConnection.Open();
                oTransaction = oConnection.BeginTransaction();

                if (isSignOff)
                {
                    //update sihnoff
                    oGLDataHdrDAO.UpdateSignOffDates(oGLDataHdrInfo, oConnection, oTransaction);
                }
                if (isFormDataToBeSaved)
                {
                    if (saveSupportingDetailSection)
                    {
                        //TODO: save oGLDataHdrInfo.BankDetails etc
                        if (isBankFormTemplate)
                        {
                            oGLDataHdrDAO.UpdateBankFormSupportingDetail(oGLDataHdrInfo, oConnection, oTransaction);
                        }
                        else if (oDerivedCalculationSupportingDetailInfo != null)
                        {
                            // insert if not exist other wise update then also update unexplained var and SupportingDetail
                            DerivedCalculationSupportingDetailDAO oDerivedCalculationSupportingDetailDAO = new DerivedCalculationSupportingDetailDAO(oAppUserInfo);
                            int count = oDerivedCalculationSupportingDetailDAO.InsertDerivedCalculationSupportingDetail(oDerivedCalculationSupportingDetailInfo, reconciliationCategoryTypeIDForSupportingDetail, oConnection, oTransaction);
                        }
                        if (isBankFormTemplate)
                        {

                        }
                    }
                    GLDataQualityScoreDAO oGLDataQualityScoreDAO = new GLDataQualityScoreDAO(oAppUserInfo);
                    oGLDataQualityScoreDAO.SaveGLDataQualityScoreInfoList(oGLDataHdrInfo.ReconciliationPeriodID, gLDataID, oGLDataHdrInfo.GLDataQualityScoreInfoList, oGLDataHdrInfo.RevisedBy, oGLDataHdrInfo.DateRevised, oConnection, oTransaction);
                    // Recalculate and Update Quality Scores
                    oGLDataQualityScoreDAO.RecalculateQualityScoreAndUpdate(oGLDataHdrInfo.ReconciliationPeriodID, gLDataID, oGLDataHdrInfo.RevisedBy, oGLDataHdrInfo.DateRevised, oConnection, oTransaction);
                    if (oGLDataHdrInfo.GLDataRecControlCheckListInfoList != null && oGLDataHdrInfo.GLDataRecControlCheckListInfoList.Count > 0)
                    {
                        RecControlChecklistDAO oRecControlChecklistDAO = new RecControlChecklistDAO(oAppUserInfo);
                        oRecControlChecklistDAO.SaveGLDataRecControlChecklist(oGLDataHdrInfo.GLDataRecControlCheckListInfoList, oGLDataHdrInfo.RevisedBy, oGLDataHdrInfo.DateRevised, oConnection, oTransaction);

                    }

                }
                oGLDataHdrInfo.GLDataID = gLDataID;
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
            return returnValue;
        }



        public List<DerivedCalculationSupportingDetailInfo> SelectAllDerivedCalculationSupportingDetailInfoByGLDataID(long? gLDataID, AppUserInfo oAppUserInfo)
        {
            List<DerivedCalculationSupportingDetailInfo> oDerivedCalculationSupportingDetailInfoCollection = null;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                DerivedCalculationSupportingDetailDAO oDerivedCalculationSupportingDetailDAO = new DerivedCalculationSupportingDetailDAO(oAppUserInfo);
                oDerivedCalculationSupportingDetailInfoCollection = oDerivedCalculationSupportingDetailDAO.SelectAllDerivedCalculationSupportingDetailInfoByGLDataID(gLDataID);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return oDerivedCalculationSupportingDetailInfoCollection;
        }


        public bool GetReviewNoteStatusForReviewerAndApprover(long glDataID, int recPeriodID, int userID, AppUserInfo oAppUserInfo)
        {
            bool result = false;

            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                GLDataReviewNoteHdrDAO oGLDataReviewNoteHdrDAO = new GLDataReviewNoteHdrDAO(oAppUserInfo);
                result = oGLDataReviewNoteHdrDAO.GetReviewNoteStatusForReviewerAndApprover(glDataID, recPeriodID, userID);
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

        public List<string> SelectLocalCurrency(int recPeriodID, bool isMultiCurrencyEnabled, AppUserInfo oAppUserInfo)
        {
            List<string> localCurrencyCollection = null;

            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                ExchangeRateDAO oExchangeRateDAO = new ExchangeRateDAO(oAppUserInfo);
                localCurrencyCollection = oExchangeRateDAO.SelectLocalCurrency(recPeriodID, isMultiCurrencyEnabled);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }

            return localCurrencyCollection;
        }

        public List<string> SelectLocalCurrencyByAccountID(long gldataID, bool isMultiCurrencyEnabled, AppUserInfo oAppUserInfo)
        {
            List<string> localCurrencyCollection = null;

            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                ExchangeRateDAO oExchangeRateDAO = new ExchangeRateDAO(oAppUserInfo);
                localCurrencyCollection = oExchangeRateDAO.SelectLocalCurrencyByAccountID(gldataID, isMultiCurrencyEnabled);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }

            return localCurrencyCollection;
        }

        public List<GLDataReviewNoteHdrInfo> GetReviewNotes(long? GLDataID, int? RecPeriodID, AppUserInfo oAppUserInfo)
        {
            List<GLDataReviewNoteHdrInfo> oGLDataReviewNoteHdrInfoCollection = null;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                GLDataReviewNoteHdrDAO oGLDataReviewNoteHdrDAO = new GLDataReviewNoteHdrDAO(oAppUserInfo);
                oGLDataReviewNoteHdrInfoCollection = oGLDataReviewNoteHdrDAO.GetReviewNotes(GLDataID, RecPeriodID);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }

            return oGLDataReviewNoteHdrInfoCollection;
        }


        public GLDataReviewNoteHdrInfo GetReviewNoteInfo(long? GLDataReviewNoteID, AppUserInfo oAppUserInfo)
        {
            GLDataReviewNoteHdrInfo oGLDataReviewNoteHdrInfo = null;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                GLDataReviewNoteHdrDAO oGLDataReviewNoteHdrDAO = new GLDataReviewNoteHdrDAO(oAppUserInfo);
                oGLDataReviewNoteHdrInfo = oGLDataReviewNoteHdrDAO.GetReviewNoteInfo(GLDataReviewNoteID);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }

            return oGLDataReviewNoteHdrInfo;
        }


        public void AddReviewNote(GLDataReviewNoteHdrInfo oGLDataReviewNoteHdrInfo, int recPeriodID, List<AttachmentInfo> oAttachmentInfoCollection, AppUserInfo oAppUserInfo)
        {
            IDbConnection oConnection = null;
            IDbTransaction oTransaction = null;

            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                GLDataReviewNoteHdrDAO oGLDataReviewNoteHdrDAO = new GLDataReviewNoteHdrDAO(oAppUserInfo);
                GLDataReviewNoteDetailDAO oGLDataReviewNoteDetailDAO = new GLDataReviewNoteDetailDAO(oAppUserInfo);

                oConnection = oGLDataReviewNoteHdrDAO.CreateConnection();
                oConnection.Open();
                oTransaction = oConnection.BeginTransaction();

                // Insert the Hdr Record
                oGLDataReviewNoteHdrDAO.Save(oGLDataReviewNoteHdrInfo, oConnection, oTransaction);

                // Insert the Detail Record
                oGLDataReviewNoteHdrInfo.GLDataReviewNoteDetailInfoCollection[0].GLDataReviewNoteID = oGLDataReviewNoteHdrInfo.GLDataReviewNoteID;
                oGLDataReviewNoteDetailDAO.Save(oGLDataReviewNoteHdrInfo.GLDataReviewNoteDetailInfoCollection[0], oConnection, oTransaction);

                if (oAttachmentInfoCollection != null && oAttachmentInfoCollection.Count > 0)
                {
                    Array.ForEach(oAttachmentInfoCollection.ToArray(), a => a.RecordID = oGLDataReviewNoteHdrInfo.GLDataReviewNoteID);
                    DataTable dtAttachment = ServiceHelper.ConvertAttachmentInfoCollectionToDataTable(oAttachmentInfoCollection);
                    AttachmentDAO oAttachmentDAO = new AttachmentDAO(oAppUserInfo);
                    oAttachmentDAO.InsertAttachmentBulk(dtAttachment, recPeriodID, oGLDataReviewNoteHdrInfo.AddedBy, oGLDataReviewNoteHdrInfo.DateAdded, oConnection, oTransaction);
                }

                oTransaction.Commit();
            }
            catch (SqlException ex)
            {
                if (oTransaction != null)
                {
                    oTransaction.Rollback();
                }
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                if (oTransaction != null)
                {
                    oTransaction.Rollback();
                }
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            finally
            {
                if (oConnection != null && oConnection.State != ConnectionState.Closed)
                {
                    oTransaction.Dispose();
                    oConnection.Close();
                    oConnection.Dispose();
                }

            }
        }

        public void UpdateReviewNote(GLDataReviewNoteHdrInfo oGLDataReviewNoteHdrInfo, AppUserInfo oAppUserInfo)
        {
            IDbConnection oConnection = null;
            IDbTransaction oTransaction = null;

            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                GLDataReviewNoteHdrDAO oGLDataReviewNoteHdrDAO = new GLDataReviewNoteHdrDAO(oAppUserInfo);
                GLDataReviewNoteDetailDAO oGLDataReviewNoteDetailDAO = new GLDataReviewNoteDetailDAO(oAppUserInfo);

                oConnection = oGLDataReviewNoteHdrDAO.CreateConnection();
                oConnection.Open();
                oTransaction = oConnection.BeginTransaction();

                // Adjust the RevisedBy fields for the Hdr Record
                oGLDataReviewNoteHdrDAO.Update(oGLDataReviewNoteHdrInfo, oConnection, oTransaction);

                // Insert the Detail Record
                oGLDataReviewNoteHdrInfo.GLDataReviewNoteDetailInfoCollection[0].GLDataReviewNoteID = oGLDataReviewNoteHdrInfo.GLDataReviewNoteID;
                oGLDataReviewNoteDetailDAO.Save(oGLDataReviewNoteHdrInfo.GLDataReviewNoteDetailInfoCollection[0], oConnection, oTransaction);

                oTransaction.Commit();
            }
            catch (SqlException ex)
            {
                if (oTransaction != null)
                {
                    oTransaction.Rollback();
                }
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                if (oTransaction != null)
                {
                    oTransaction.Rollback();
                }
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            finally
            {
                if (oConnection != null && oConnection.State != ConnectionState.Closed)
                {
                    oTransaction.Dispose();
                    oConnection.Close();
                    oConnection.Dispose();
                }

            }
        }


        public void DeleteReviewNote(GLDataReviewNoteHdrInfo oGLDataReviewNoteHdrInfo, AppUserInfo oAppUserInfo)
        {
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                GLDataReviewNoteHdrDAO oGLDataReviewNoteHdrDAO = new GLDataReviewNoteHdrDAO(oAppUserInfo);
                oGLDataReviewNoteHdrDAO.DeleteReviewNote(oGLDataReviewNoteHdrInfo);
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


        public List<GLDataHdrInfo> GetAccountInfoForNetAccount(int? NetAccountID, int? RecPeriodID, int? CompanyID, List<short> oAttributeIDCollection, int LCID, int BusinessEntityID, int DefaultLCID, AppUserInfo oAppUserInfo)
        {
            List<GLDataHdrInfo> oGLDataHdrInfoCollection = null;

            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                GLDataHdrDAO oGLDataHdrDAO = new GLDataHdrDAO(oAppUserInfo);
                return oGLDataHdrDAO.GetAccountInfoForNetAccount(NetAccountID, RecPeriodID, CompanyID, oAttributeIDCollection, LCID, BusinessEntityID, DefaultLCID);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }

            return oGLDataHdrInfoCollection;
        }


        public GLDataHdrInfo GetLiteGLDataInfoByGLDataID(long? GLDataID, AppUserInfo oAppUserInfo)
        {
            GLDataHdrInfo oGLDataHdrInfo = null;

            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                GLDataHdrDAO oGLDataHdrDAO = new GLDataHdrDAO(oAppUserInfo);
                oGLDataHdrInfo = oGLDataHdrDAO.GetLiteGLDataInfoByGLDataID(GLDataID);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }

            return oGLDataHdrInfo;
        }

        public void DeleteUserGLDataFlag(UserGLDataFlagInfo oUserGLDataFlagInfo, AppUserInfo oAppUserInfo)
        {
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                GLDataHdrDAO oGLDataHdrDAO = new GLDataHdrDAO(oAppUserInfo);
                oGLDataHdrDAO.DeleteUserGLDataFlag(oUserGLDataFlagInfo);
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

        public void InsertUserGLDataFlag(UserGLDataFlagInfo oUserGLDataFlagInfo, AppUserInfo oAppUserInfo)
        {
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                GLDataHdrDAO oGLDataHdrDAO = new GLDataHdrDAO(oAppUserInfo);
                oGLDataHdrDAO.InsertUserGLDataFlag(oUserGLDataFlagInfo);
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


        public List<GLDataStatusInfo> GetAuditTrailData(long? GLDataID, AppUserInfo oAppUserInfo)
        {
            List<GLDataStatusInfo> oGLDataStatusInfoCollection = null;

            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                GLDataStatusDAO oGLDataStatusDAO = new GLDataStatusDAO(oAppUserInfo);
                oGLDataStatusInfoCollection = oGLDataStatusDAO.GetAuditTrailData(GLDataID);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }

            return oGLDataStatusInfoCollection;
        }

        public bool? GetIsAllAccountsReconciledForUserAndRole(int userID, short roleID, int recPeriodID, AppUserInfo oAppUserInfo)
        {
            bool? isAllAccountsReconciled = false;

            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                GLDataHdrDAO oGLDataHdrDAO = new GLDataHdrDAO(oAppUserInfo);
                isAllAccountsReconciled = oGLDataHdrDAO.GetIsAllAccountsReconciledForUserAndRole(userID, roleID, recPeriodID);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }

            return isAllAccountsReconciled;
        }
        public bool? GetIsAllJuniorsCompletedCertificationForUserRoleAndCertificationType(int userID, short roleID, int recPeriodID, short certificationTypeID, AppUserInfo oAppUserInfo)
        {
            bool? isAllJuniorsCompletedCertification = false;

            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                GLDataHdrDAO oGLDataHdrDAO = new GLDataHdrDAO(oAppUserInfo);
                isAllJuniorsCompletedCertification = oGLDataHdrDAO.GetIsAllJuniorsCompletedCertificationForUserRoleAndCertificationType(userID, roleID, recPeriodID, certificationTypeID);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }

            return isAllJuniorsCompletedCertification;
        }

        public List<GLDataHdrInfo> SelectGLDataAndAccountInfoByUserIDForSRA(
                                                                    List<FilterCriteria> oFilterCriteriaCollection
                                                                    , int recPeriodID
                                                                    , int companyID
                                                                    , int userID
                                                                    , short userRoleID
                                                                    , bool isDualReviewEnabled
                                                                    , bool isMaterialityEnabled
                                                                    , short preparerAttributeID
                                                                    , short reviewerAttributeID
                                                                    , short approverAttributeID
                                                                    , short preparerRoleID
                                                                    , short reviewerRoleID
                                                                    , short approverRoleID
                                                                    , short systemAdminRoleID
                                                                    , short CEO_CFORoleID
                                                                    , short skyStemAdminRoleID
                                                                    , int? count
                                                                    , List<Int16> AccountAttributeIDCollection
                                                                    , int languageID
                                                                    , int businessEntityID
                                                                    , int defaultLanguageID
                                                                    , string sortExpression
                                                                    , string sortDirection
                                                                    , AppUserInfo oAppUserInfo
                                                                )
        {
            List<GLDataHdrInfo> oGLDataHdrInfoCollection = null;

            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                DataTable dtFilterCriteria = ServiceHelper.ConvertFilterCriteriaIntoDataTable(oFilterCriteriaCollection);
                GLDataHdrDAO oGLDataHdrDAO = new GLDataHdrDAO(oAppUserInfo);
                oGLDataHdrInfoCollection = oGLDataHdrDAO.SelectGLDataAndAccountInfoByUserIDForSRA(dtFilterCriteria
                    , recPeriodID, companyID, userID, userRoleID, isDualReviewEnabled, isMaterialityEnabled,
                    preparerAttributeID, reviewerAttributeID, approverAttributeID,
                    preparerRoleID, reviewerRoleID, approverRoleID, systemAdminRoleID,
                    CEO_CFORoleID, skyStemAdminRoleID, count, AccountAttributeIDCollection,
                    languageID, businessEntityID, defaultLanguageID, sortExpression, sortDirection);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }

            return oGLDataHdrInfoCollection;
        }

        public void UpdateGLDataIsSRA(List<long> oAccountIDCollection, int currentRecPeriodId, bool isSRA, string userLoginID, DateTime dateRevised, AppUserInfo oAppUserInfo)
        {
            IDbConnection oConnection = null;
            IDbTransaction oTransaction = null;

            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                DataTable dtAccountIds = ServiceHelper.ConvertLongIDCollectionToDataTable(oAccountIDCollection);
                GLDataHdrDAO oGLDataHdrDAO = new GLDataHdrDAO(oAppUserInfo);
                oConnection = oGLDataHdrDAO.CreateConnection();
                oConnection.Open();
                oTransaction = oConnection.BeginTransaction();
                oGLDataHdrDAO.UpdateGLDataIsSRA(dtAccountIds, currentRecPeriodId, isSRA, userLoginID, dateRevised, oConnection, oTransaction);
                oTransaction.Commit();
                oTransaction.Dispose();
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
        }


        public void DeleteReviewNotesAfterCertification(int recPeriodID, string revisedBy, DateTime dateRevised, AppUserInfo oAppUserInfo)
        {
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                GLDataReviewNoteHdrDAO oGLDataReviewNoteHdrDAO = new GLDataReviewNoteHdrDAO(oAppUserInfo);
                oGLDataReviewNoteHdrDAO.DeleteReviewNotesAfterCertification(recPeriodID, revisedBy, dateRevised);
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


        public void UpdateGLDataForRemoveAccountSignOff(List<long> oAccountIDCollection, List<int> oNetAccountIDCollection, int? RecPeriodID, string RevisedBy, DateTime DateRevised, AppUserInfo oAppUserInfo)
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
                oGLDataHdrDAO.UpdateGLDataForRemoveAccountSignOff(oAccountIDCollection, oNetAccountIDCollection, RecPeriodID, RevisedBy, DateRevised);

                //Additional code Added By Prafull on 15-Feb-2011
                oReconciliationPeriodDAO.UpdateRecPeriodStatusAsInProgress(RecPeriodID.HasValue ? (int)RecPeriodID : 0, oConnection, oTransaction);
                oTransaction.Commit();
                oTransaction.Dispose();

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
        }

        public string GetBCCYByGLDataID(long gldataID, AppUserInfo oAppUserInfo)
        {
            string BCCY = null;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                GLDataHdrDAO oGLDataHdrDAO = new GLDataHdrDAO(oAppUserInfo);
                BCCY = oGLDataHdrDAO.GetBCCYByGLDataID(gldataID);

            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return BCCY;
        }

        public List<GLDataHdrInfo> GetGLVersionByGLDataID(GLDataParamInfo oGLDataParamInfo, AppUserInfo oAppUserInfo)
        {
            List<GLDataHdrInfo> oGLDataHdrInfoCollection = null;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                GLDataHdrDAO oGLDataHdrDAO = new GLDataHdrDAO(oAppUserInfo);
                oGLDataHdrInfoCollection = oGLDataHdrDAO.GetGLVersionByGLDataID(oGLDataParamInfo);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return oGLDataHdrInfoCollection;
        }
        public void UpdateReOpenAccount(List<long> oGLDataIDCollection, string RevisedBy, DateTime dateRevised, short actionTypeID, short changeSourceIDSRA, AppUserInfo oAppUserInfo)
        {

            IDbConnection oConnection = null;
            IDbTransaction oTransaction = null;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                GLDataHdrDAO oGLDataHdrDAO = new GLDataHdrDAO(oAppUserInfo);
                oConnection = oGLDataHdrDAO.CreateConnection();
                oConnection.Open();
                oTransaction = oConnection.BeginTransaction();//Begin transaction
                oGLDataHdrDAO.UpdateReOpenAccount(oGLDataIDCollection, RevisedBy, dateRevised, actionTypeID, changeSourceIDSRA, oConnection, oTransaction);
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
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                if (oConnection != null && oConnection.State != ConnectionState.Closed && oTransaction != null)
                    oTransaction.Rollback();
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }

            finally
            {
                if ((null != oConnection) && (oConnection.State == ConnectionState.Open))
                    oConnection.Dispose();
            }

        }

        public bool IsGLDataIDEditable(Int64 glDataID, AppUserInfo oAppUserInfo)
        {
            bool isEditable = false;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                GLDataHdrDAO oGLDataHdrDAO = new GLDataHdrDAO(oAppUserInfo);
                isEditable = oGLDataHdrDAO.IsGLDataIDEditable(glDataID);

            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return isEditable;
        }

        public GLDataHdrInfo GetGLDataHdrInfo(Int64? glDataID, Int32? recPeriodID, Int32? CurrentUserID, Int16? CurrentRoleID, AppUserInfo oAppUserInfo)
        {
            GLDataHdrInfo oGLDataHdrInfo = null;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                GLDataHdrDAO oGLData = new GLDataHdrDAO(oAppUserInfo);
                oGLDataHdrInfo = oGLData.GetGLDataHdrInfo(glDataID, recPeriodID, CurrentUserID, CurrentRoleID);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return oGLDataHdrInfo;
        }
        public void UpdateReSetAccount(List<long> oGLDataIDCollection, string RevisedBy, DateTime dateRevised, short actionTypeID, short changeSourceIDSRA, AppUserInfo oAppUserInfo)
        {

            IDbConnection oConnection = null;
            IDbTransaction oTransaction = null;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                GLDataHdrDAO oGLDataHdrDAO = new GLDataHdrDAO(oAppUserInfo);
                oConnection = oGLDataHdrDAO.CreateConnection();
                oConnection.Open();
                oTransaction = oConnection.BeginTransaction();//Begin transaction
                oGLDataHdrDAO.UpdateReSetAccount(oGLDataIDCollection, RevisedBy, dateRevised, actionTypeID, changeSourceIDSRA, oConnection, oTransaction);
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
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                if (oConnection != null && oConnection.State != ConnectionState.Closed && oTransaction != null)
                    oTransaction.Rollback();
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }

            finally
            {
                if ((null != oConnection) && (oConnection.State == ConnectionState.Open))
                    oConnection.Dispose();
            }
        }
        public bool CheckGLPermissions(long? GLDataID, int? UserID, short? RoleID, AppUserInfo oAppUserInfo)
        {
            bool hasAccess = false;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                hasAccess = GLDataBLL.CheckGLPermissions(GLDataID, UserID, RoleID, oAppUserInfo);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return hasAccess;
        }
    }//end of class
}//end of namespace
