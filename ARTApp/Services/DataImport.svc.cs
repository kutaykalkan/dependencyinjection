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
using SkyStem.ART.Client.Params.RecItemUpload;
using SkyStem.ART.Client.Params;

namespace SkyStem.ART.App.Services
{
    // NOTE: If you change the class name "DataImport" here, you must also update the reference to "DataImport" in Web.config.
    public class DataImport : IDataImport
    {

        #region IDataImport Members
        /// <summary>
        /// Import Holiday calander to database
        /// </summary>
        /// <param name="newDataImport"></param>
        /// <param name="newHolidayCalendarList"></param>
        public void InsertDataImportHolidayCalendar(DataImportHdrInfo oDataImportHdrInfo
            , List<HolidayCalendarInfo> oHolidayCalendarCollection, string failureMsg, out int rowAffected, AppUserInfo oAppUserInfo)
        {
            IDbConnection oConnection = null;
            IDbTransaction oTransaction = null;
            int companyID = oDataImportHdrInfo.CompanyID.Value;
            DateTime dateAdded = oDataImportHdrInfo.DateAdded.Value;
            string addedBy = oDataImportHdrInfo.AddedBy;
            int newDataImportId;
            int? rowsAffected = 0;
            rowAffected = 0;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                DataImportHdrDAO oDataImportHrdDAO = new DataImportHdrDAO(oAppUserInfo);
                oConnection = oDataImportHrdDAO.CreateConnection();
                oConnection.Open();
                oTransaction = oConnection.BeginTransaction();//Begin transaction
                //Save DataImportHDR
                oDataImportHrdDAO.Save(oDataImportHdrInfo, oConnection, oTransaction);
                if (oDataImportHdrInfo.DataImportID.HasValue)
                {
                    newDataImportId = oDataImportHdrInfo.DataImportID.Value;
                    foreach (HolidayCalendarInfo oHolidayCal in oHolidayCalendarCollection)
                    {
                        oHolidayCal.DataImportID = newDataImportId;
                    }
                    HolidayCalendar oHolidayCalendar = new HolidayCalendar();
                    //Save Holiday Calendar
                    rowsAffected = oHolidayCalendar.InsertHolidayCalendar(oHolidayCalendarCollection, oConnection, oTransaction, companyID, oAppUserInfo);
                    rowAffected = (int)rowsAffected;
                    if (rowsAffected.HasValue && rowsAffected > 0)
                    {
                        newDataImportId = oDataImportHdrInfo.DataImportID.Value;
                        DataImportFailureMessageDAO oDataImportFailureMessageDAO = new DataImportFailureMessageDAO(oAppUserInfo);
                        //Save Failure Message
                        rowsAffected = oDataImportFailureMessageDAO.InsertDataImportFailureMsg(newDataImportId
                            , failureMsg, dateAdded, addedBy, oConnection, oTransaction);
                        if (rowsAffected > 0)
                        {
                            oTransaction.Commit();
                        }
                        else
                        {
                            throw new ARTException(5000042);//Error while saving data to database
                        }

                    }
                    else
                    {
                        throw new ARTException(5000042);//Error while saving data to database
                    }
                }
                else
                {
                    throw new ARTException(5000042);//Error while saving data to database
                }
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

        public void InsertDataImportRecPeriod(DataImportHdrInfo oDataImportHdrInfo
            , List<ReconciliationPeriodInfo> oRecPeriodCollection, string failureMsg, DateTime? currentReconciliationPeriodEndDate, out int rowAffected, AppUserInfo oAppUserInfo)
        {
            IDbConnection oConnection = null;
            IDbTransaction oTransaction = null;
            int companyID = oDataImportHdrInfo.CompanyID.Value;
            DateTime dateAdded = oDataImportHdrInfo.DateAdded.Value;
            string addedBy = oDataImportHdrInfo.AddedBy;
            int newDataImportId;
            int rowsAffected = 0;
            rowAffected = 0;
            int? rowsAffectedFailureMsg = 0;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                DataImportHdrDAO oDataImportHrdDAO = new DataImportHdrDAO(oAppUserInfo);
                oConnection = oDataImportHrdDAO.CreateConnection();
                oConnection.Open();
                oTransaction = oConnection.BeginTransaction();//Begin transaction
                oDataImportHrdDAO.Save(oDataImportHdrInfo, oConnection, oTransaction);
                if (oDataImportHdrInfo.DataImportID.HasValue)
                {
                    newDataImportId = oDataImportHdrInfo.DataImportID.Value;
                    foreach (ReconciliationPeriodInfo oRecPeriodInfo in oRecPeriodCollection)
                    {
                        oRecPeriodInfo.DataImportID = newDataImportId;
                    }


                    ReconciliationPeriod oRecPeriod = new ReconciliationPeriod();
                    rowsAffected = oRecPeriod.InsertReconciliationPeriod(oRecPeriodCollection, oConnection, oTransaction, companyID, newDataImportId, currentReconciliationPeriodEndDate, oAppUserInfo);
                    rowAffected = (int)rowsAffected;
                    if (rowsAffected >= 0)
                    {
                        newDataImportId = oDataImportHdrInfo.DataImportID.Value;
                        DataImportFailureMessageDAO oDataImportFailureMessageDAO = new DataImportFailureMessageDAO(oAppUserInfo);
                        //Save Failure Message
                        rowsAffectedFailureMsg = oDataImportFailureMessageDAO.InsertDataImportFailureMsg(newDataImportId
                            , failureMsg, dateAdded, addedBy, oConnection, oTransaction);
                        if (rowsAffectedFailureMsg.HasValue && rowsAffectedFailureMsg > 0)
                        {
                            oTransaction.Commit();
                        }
                        else
                        {
                            throw new ARTException(5000042);//Error while saving data to database
                        }

                    }
                    else
                    {
                        throw new ARTException(5000042);//Error while saving data to database
                    }
                }
                else
                {
                    throw new ARTException(5000042);//Error while saving data to database
                }
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

        public bool InsertDataImportGLData(DataImportHdrInfo oDataImportHdrInfo
            , List<GeographyStructureHdrInfo> oGeoStructCollection, string failureMsg
            , short companyGeographyClassID, AppUserInfo oAppUserInfo)
        {
            bool finalStatus = false;
            IDbConnection oConnection = null;
            IDbTransaction oTransaction = null;
            int companyID = oDataImportHdrInfo.CompanyID.Value;
            DateTime dateAdded = oDataImportHdrInfo.DateAdded.Value;
            string addedBy = oDataImportHdrInfo.AddedBy;
            int newDataImportId;
            int rowsAffected = 0;
            int? rowsAffectedFailureMsg = 0;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                DataImportHdrDAO oDataImportHrdDAO = new DataImportHdrDAO(oAppUserInfo);
                oConnection = oDataImportHrdDAO.CreateConnection();
                oConnection.Open();
                oTransaction = oConnection.BeginTransaction();//Begin transaction
                oDataImportHrdDAO.Save(oDataImportHdrInfo, oConnection, oTransaction);
                if (oDataImportHdrInfo.DataImportID.HasValue)
                {
                    newDataImportId = oDataImportHdrInfo.DataImportID.Value;
                    GeographyStructureHdr oGeoStruct = new GeographyStructureHdr();
                    rowsAffected = oGeoStruct.InsertGeographyStructureHdr(oGeoStructCollection, oConnection
                        , oTransaction, companyID, true, dateAdded, addedBy, companyGeographyClassID, oAppUserInfo);
                    if (rowsAffected >= 0)
                    {
                        DataImportFailureMessageDAO oDataImportFailureMessageDAO = new DataImportFailureMessageDAO(oAppUserInfo);
                        //Save Failure Message
                        rowsAffectedFailureMsg = oDataImportFailureMessageDAO.InsertDataImportFailureMsg(newDataImportId
                            , failureMsg, dateAdded, addedBy, oConnection, oTransaction);
                        if (rowsAffectedFailureMsg.HasValue && rowsAffectedFailureMsg > 0)
                        {
                            //Update CompanyHdr for KeyCount
                            CompanySettingDAO oCompanySettingDAO = new CompanySettingDAO(oAppUserInfo);
                            int rowsA = oCompanySettingDAO.UpdateCompanySettingForKeyCountByCompanyID(companyID, (short)oGeoStructCollection.Count, oConnection, oTransaction);
                            if (rowsA > 0)
                            {
                                oTransaction.Commit();
                                finalStatus = true;
                            }
                            else
                                throw new Exception("Error while updating Company Setting for key count");//Error while saving data to database
                        }
                        else
                        {
                            throw new Exception("Error while insterting record in Data Import Failure messgae");//Error while saving data to database
                        }
                    }
                    else
                    {
                        throw new Exception("Error while inserting record in Data Import Geography Structure");
                    }
                }
                else
                {
                    throw new Exception("Error while inserting record in Data Import header table");//Error while saving data to database
                }
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
            return finalStatus;
        }


        public void InsertDataImportExchangeRate(DataImportHdrInfo oDataImportHdrInfo
           , List<ExchangeRateInfo> oExchangeRateInfoCollection, string failureMsg, List<CurrencyCodeInfo> oCurrencyCodeInfoCollection, out int rowAffected, AppUserInfo oAppUserInfo)
        {
            IDbConnection oConnection = null;
            IDbTransaction oTransaction = null;
            int companyID = oDataImportHdrInfo.CompanyID.Value;
            DateTime dateAdded = oDataImportHdrInfo.DateAdded.Value;
            string addedBy = oDataImportHdrInfo.AddedBy;
            int newDataImportId;
            int? rowsAffected = 0;
            rowAffected = 0;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);

                DataImportHdrDAO oDataImportHrdDAO = new DataImportHdrDAO(oAppUserInfo);
                CurrencyCodeDAO oCurrencyCodeDAO = new CurrencyCodeDAO(oAppUserInfo);
                oConnection = oDataImportHrdDAO.CreateConnection();
                oConnection.Open();
                oTransaction = oConnection.BeginTransaction();//Begin transaction
                //Save DataImportHDR
                oDataImportHrdDAO.Save(oDataImportHdrInfo, oConnection, oTransaction);
                if (oDataImportHdrInfo.DataImportID.HasValue)
                {
                    newDataImportId = oDataImportHdrInfo.DataImportID.Value;
                    ExchangeRateDAO oExchangeRateDAO = new ExchangeRateDAO(oAppUserInfo);
                    DataTable dtCurrencyCode = DataImportServiceHelper.ConvertCurrencyCodeListToDataTable(oCurrencyCodeInfoCollection);
                    DataTable dtExchangeRate = DataImportServiceHelper.ConvertCurrencyExchangeRateListToDataTable(oExchangeRateInfoCollection);
                    rowsAffected = oCurrencyCodeDAO.InsertCurrencyCodeDataTable(dtCurrencyCode, oConnection, oTransaction);
                    rowsAffected = oExchangeRateDAO.InsertInsertExchangeRateDataTable(newDataImportId, dtExchangeRate, oConnection, oTransaction);
                    rowAffected = (int)rowsAffected;
                    if (rowsAffected.HasValue && rowsAffected > 0)
                    {
                        newDataImportId = oDataImportHdrInfo.DataImportID.Value;
                        DataImportFailureMessageDAO oDataImportFailureMessageDAO = new DataImportFailureMessageDAO(oAppUserInfo);
                        //Save Failure Message
                        rowsAffected = oDataImportFailureMessageDAO.InsertDataImportFailureMsg(newDataImportId
                            , failureMsg, dateAdded, addedBy, oConnection, oTransaction);
                        if (rowsAffected > 0)
                        {
                            oTransaction.Commit();
                        }
                        else
                        {
                            throw new ARTException(5000042);//Error while saving data to database
                        }

                    }
                    else
                    {
                        throw new ARTException(5000042);//Error while saving data to database
                    }
                }
                else
                {
                    throw new ARTException(5000042);//Error while saving data to database
                }
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


        public void InsertDataImportSubledgerSourceRate(DataImportHdrInfo oDataImportHdrInfo
           , List<SubledgerSourceInfo> oSubledgerSourceInfoCollection, string failureMsg, out int rowAffected, AppUserInfo oAppUserInfo)
        {
            IDbConnection oConnection = null;
            IDbTransaction oTransaction = null;
            int companyID = oDataImportHdrInfo.CompanyID.Value;
            DateTime dateAdded = oDataImportHdrInfo.DateAdded.Value;
            string addedBy = oDataImportHdrInfo.AddedBy;
            int newDataImportId;
            int? rowsAffected = 0;
            rowAffected = 0;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                DataImportHdrDAO oDataImportHrdDAO = new DataImportHdrDAO(oAppUserInfo);
                oConnection = oDataImportHrdDAO.CreateConnection();
                oConnection.Open();
                oTransaction = oConnection.BeginTransaction();//Begin transaction
                //Save DataImportHDR
                oDataImportHrdDAO.Save(oDataImportHdrInfo, oConnection, oTransaction);
                if (oDataImportHdrInfo.DataImportID.HasValue)
                {
                    newDataImportId = oDataImportHdrInfo.DataImportID.Value;
                    SubledgerSourceDAO oSubledgerSourceDAO = new SubledgerSourceDAO(oAppUserInfo);
                    DataTable dtSubledgerSource = DataImportServiceHelper.ConvertSubledgerListToDataTable(oSubledgerSourceInfoCollection);
                    rowsAffected = oSubledgerSourceDAO.InsertInsertSubledgerSourceDataTable(dtSubledgerSource, oDataImportHdrInfo.DataImportID, oConnection, oTransaction);
                    rowAffected = (int)rowsAffected;
                    if (rowsAffected.HasValue && rowsAffected >= 0)
                    {
                        newDataImportId = oDataImportHdrInfo.DataImportID.Value;
                        DataImportFailureMessageDAO oDataImportFailureMessageDAO = new DataImportFailureMessageDAO(oAppUserInfo);
                        //Save Failure Message
                        rowsAffected = oDataImportFailureMessageDAO.InsertDataImportFailureMsg(newDataImportId
                            , failureMsg, dateAdded, addedBy, oConnection, oTransaction);
                        if (rowsAffected > 0)
                        {
                            oTransaction.Commit();
                        }
                        else
                        {
                            throw new ARTException(5000042);//Error while saving data to database
                        }

                    }
                    else
                    {
                        throw new ARTException(5000042);//Error while saving data to database
                    }
                }
                else
                {
                    throw new ARTException(5000042);//Error while saving data to database
                }
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

        /// <summary>
        /// this method returns long value indicating available file storage as per company id
        /// </summary>
        /// <param name="companyID">id of company</param>
        /// <returns>available file storage space</returns>
        public long GetAvailableFileStorageSpaceByCompanyID(int companyID, AppUserInfo oAppUserInfo)
        {
            long fileStorageSpace = 0;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                DataImportHdrDAO oDataImportHdrDao = new DataImportHdrDAO(oAppUserInfo);
                fileStorageSpace = oDataImportHdrDao.GetAvailableFileStorageSpaceByCompanyID(companyID);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return fileStorageSpace;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="companyID"></param>
        /// <returns></returns>
        public decimal? GetMaxFileSizeByCompanyID(int companyID, AppUserInfo oAppUserInfo)
        {
            decimal? maxFileSize = null;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                DataImportHdrDAO oDataImportHdrDao = new DataImportHdrDAO(oAppUserInfo);
                maxFileSize = oDataImportHdrDao.GetMaxFileSizeByCompanyID(companyID);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return maxFileSize;
        }

        public int GetMaxFileSizeByCompanyIDInt(int companyID, AppUserInfo oAppUserInfo)
        {
            int maxFileSize = 0;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                DataImportHdrDAO oDataImportHdrDao = new DataImportHdrDAO(oAppUserInfo);
                maxFileSize = oDataImportHdrDao.GetMaxFileSizeByCompanyIDInt(companyID);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return maxFileSize;
        }

        public bool isFirstTimeGLDataImportByCompanyID(int companyID, short DataImportTypeID, short FailureStatusID, AppUserInfo oAppUserInfo)
        {
            bool isFirstTime = false;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                DataImportHdrDAO oDataImportHdrDAO = new DataImportHdrDAO(oAppUserInfo);
                isFirstTime = oDataImportHdrDAO.IsFirstTimeGLDataImportByCompanyID(companyID, DataImportTypeID, FailureStatusID);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return isFirstTime;
        }
        public short? isKeyMappingDoneByCompanyID(int companyID, AppUserInfo oAppUserInfo)
        {
            short? keyCount = null;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                DataImportHdrDAO oDataImportHdrDAO = new DataImportHdrDAO(oAppUserInfo);
                keyCount = oDataImportHdrDAO.isKeyMappingDoneByCompanyID(companyID);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return keyCount;
        }
        /// <summary>
        /// Insert dataimportHDR with failureMessage
        /// </summary>
        /// <param name="oDataImportHdrInfo">Dataimport info object</param>
        /// <param name="failureMsg">failure mesage</param>
        public void InsertDataImportWithFailureMsg(DataImportHdrInfo oDataImportHdrInfo, string failureMsg, AppUserInfo oAppUserInfo)
        {
            IDbConnection oConnection = null;
            IDbTransaction oTransaction = null;
            DateTime dateAdded = oDataImportHdrInfo.DateAdded.Value;
            string addedBy = oDataImportHdrInfo.AddedBy;
            int newDataImportId;
            int? rowsAffected = 0;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                DataImportHdrDAO oDataImportHrdDAO = new DataImportHdrDAO(oAppUserInfo);
                oConnection = oDataImportHrdDAO.CreateConnection();
                oConnection.Open();
                oTransaction = oConnection.BeginTransaction();//Begin transaction
                oDataImportHrdDAO.Save(oDataImportHdrInfo, oConnection, oTransaction);
                if (oDataImportHdrInfo.DataImportID.HasValue)
                {
                    newDataImportId = oDataImportHdrInfo.DataImportID.Value;
                    DataImportMultilingualUploadInfo oDataImportMultilingualUploadInfo = oDataImportHdrInfo.DataImportMultilingualUploadInfo;
                    if (oDataImportMultilingualUploadInfo != null)
                    {
                        oDataImportMultilingualUploadInfo.DataImportID = newDataImportId;
                        DataImportMultilingualUploadDAO oDataImportMultilingualUploadDAO = new DataImportMultilingualUploadDAO(oAppUserInfo);
                        oDataImportMultilingualUploadDAO.Save(oDataImportMultilingualUploadInfo, oConnection, oTransaction);
                    }

                    SystemLockdownInfo oSystemLockdownInfo = oDataImportHdrInfo.SystemLockdownInfo;
                    if (oSystemLockdownInfo != null)
                    {
                        oSystemLockdownInfo.DataImportID = newDataImportId;
                        oSystemLockdownInfo.AddedBy = oDataImportHdrInfo.AddedBy;
                        oSystemLockdownInfo.DateAdded = oDataImportHdrInfo.DateAdded;
                        SystemLockdownDAO oSystemLockdownDAO = new SystemLockdownDAO(oAppUserInfo);
                        oSystemLockdownDAO.Save(oSystemLockdownInfo, oConnection, oTransaction);
                    }

                    DataImportFailureMessageDAO oDataImportFailureMessageDAO = new DataImportFailureMessageDAO(oAppUserInfo);

                    rowsAffected = oDataImportFailureMessageDAO.InsertDataImportFailureMsg(newDataImportId
                        , failureMsg, dateAdded, addedBy, oConnection, oTransaction);
                    if (rowsAffected > 0)
                    {
                        oTransaction.Commit();
                    }
                    else
                    {
                        if (oConnection != null && oConnection.State != ConnectionState.Closed && oTransaction != null)
                            oTransaction.Rollback();
                        throw new ARTException(5000042);//Error while saving data to database
                    }

                }
                else
                {
                    if (oConnection != null && oConnection.State != ConnectionState.Closed && oTransaction != null)
                        oTransaction.Rollback();
                    throw new ARTException(5000042);//Error while saving data to database
                }
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

        public void InsertDataImportWithFailureMsgAndKeyCount(DataImportHdrInfo oDataImportHdrInfo, string failureMsg, short keyCount, AppUserInfo oAppUserInfo)
        {
            IDbConnection oConnection = null;
            IDbTransaction oTransaction = null;
            int companyID = oDataImportHdrInfo.CompanyID.Value;
            DateTime dateAdded = oDataImportHdrInfo.DateAdded.Value;
            string addedBy = oDataImportHdrInfo.AddedBy;
            int newDataImportId;
            int? rowsAffected = 0;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                DataImportHdrDAO oDataImportHrdDAO = new DataImportHdrDAO(oAppUserInfo);
                oConnection = oDataImportHrdDAO.CreateConnection();
                oConnection.Open();
                oTransaction = oConnection.BeginTransaction();//Begin transaction
                oDataImportHrdDAO.Save(oDataImportHdrInfo, oConnection, oTransaction);
                if (oDataImportHdrInfo.DataImportID.HasValue)
                {
                    newDataImportId = oDataImportHdrInfo.DataImportID.Value;

                    DataImportMultilingualUploadInfo oDataImportMultilingualUploadInfo = oDataImportHdrInfo.DataImportMultilingualUploadInfo;
                    if (oDataImportMultilingualUploadInfo != null)
                    {
                        oDataImportMultilingualUploadInfo.DataImportID = newDataImportId;
                        DataImportMultilingualUploadDAO oDataImportMultilingualUploadDAO = new DataImportMultilingualUploadDAO(oAppUserInfo);
                        oDataImportMultilingualUploadDAO.Save(oDataImportMultilingualUploadInfo, oConnection, oTransaction);
                    }

                    SystemLockdownInfo oSystemLockdownInfo = oDataImportHdrInfo.SystemLockdownInfo;
                    if (oSystemLockdownInfo != null)
                    {
                        oSystemLockdownInfo.DataImportID = newDataImportId;
                        oSystemLockdownInfo.AddedBy = oDataImportHdrInfo.AddedBy;
                        oSystemLockdownInfo.DateAdded = oDataImportHdrInfo.DateAdded;
                        SystemLockdownDAO oSystemLockdownDAO = new SystemLockdownDAO(oAppUserInfo);
                        oSystemLockdownDAO.Save(oSystemLockdownInfo, oConnection, oTransaction);
                    }

                    DataImportFailureMessageDAO oDataImportFailureMessageDAO = new DataImportFailureMessageDAO(oAppUserInfo);

                    rowsAffected = oDataImportFailureMessageDAO.InsertDataImportFailureMsg(newDataImportId
                        , failureMsg, dateAdded, addedBy, oConnection, oTransaction);
                    if (rowsAffected > 0)
                    {
                        //Update CompanyHdr for KeyCount
                        CompanySettingDAO oCompanySettingDAO = new CompanySettingDAO(oAppUserInfo);
                        if (oCompanySettingDAO.UpdateCompanySettingForKeyCountByCompanyID(companyID, keyCount, oConnection, oTransaction) > 0)
                            oTransaction.Commit();
                        else
                            throw new Exception("Error while updating Company Setting for key count");//Error while saving data to database
                    }
                    else
                        throw new Exception("Error while insterting record in Data Import Failure messgae");//Error while saving data to database

                }
                else
                    throw new Exception("Error while inserting record in Data Import header table");//Error while saving data to database
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

        public DataImportHdrInfo GetDataImportInfo(int? DataImportID, AppUserInfo oAppUserInfo)
        {
            //#if DEMO
            //            // TODO: Commented by Apoorv
            //            //List<DataImportHdrInfo> oDataImportHdrInfoCollection = GetUploadData();
            //            //return oDataImportHdrInfoCollection.Find(c => c.DataImportID == DataImportID);
            //            return null;
            //#else

            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                DataImportHdrDAO oDataImportHrdDAO = new DataImportHdrDAO(oAppUserInfo);
                DataImportHdrInfo oDataImportHdrInfo = oDataImportHrdDAO.GetDataImportInfo(DataImportID);
                List<DataImportMessageDetailInfo> oDataImportMessageDetailInfoList = oDataImportHrdDAO.GetDataImportMessageDetailInfoList(DataImportID);
                if (oDataImportMessageDetailInfoList != null && oDataImportMessageDetailInfoList.Count > 0)
                {
                    foreach (DataImportMessageDetailInfo oDataImportMessageDetailInfo in oDataImportMessageDetailInfoList)
                    {
                        if (oDataImportMessageDetailInfo.DataImportMessageCategoryID.HasValue)
                        {
                            if (oDataImportMessageDetailInfo.DataImportMessageCategoryID == (short)ARTEnums.DataImportMessageCategory.AccountMessages)
                            {
                                if (oDataImportHdrInfo.DataImportAccountMessageDetailInfoList == null)
                                    oDataImportHdrInfo.DataImportAccountMessageDetailInfoList = new List<DataImportMessageDetailInfo>();
                                oDataImportHdrInfo.DataImportAccountMessageDetailInfoList.Add(oDataImportMessageDetailInfo);
                            }
                            else
                            {
                                if (oDataImportHdrInfo.DataImportMessageDetailInfoList == null)
                                    oDataImportHdrInfo.DataImportMessageDetailInfoList = new List<DataImportMessageDetailInfo>();
                                oDataImportHdrInfo.DataImportMessageDetailInfoList.Add(oDataImportMessageDetailInfo);
                            }
                        }
                    }
                }
                return oDataImportHdrInfo;
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

            //#endif

        }


        public List<DataImportHdrInfo> GetDataImportStatusByUserID(int? RecPeriodID, bool showHiddenRows, int? UserID, short? RoleID, AppUserInfo oAppUserInfo)
        {
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                DataImportHdrDAO oDataImportHrdDAO = new DataImportHdrDAO(oAppUserInfo);
                return oDataImportHrdDAO.GetDataImportStatusByUserID(RecPeriodID, showHiddenRows, UserID, RoleID);
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


        public List<DataImportHdrInfo> GetDataImportStatusByCompanyID(int? CompanyID, int? UserID, short? RoleID, AppUserInfo oAppUserInfo)
        {
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                DataImportHdrDAO oDataImportHrdDAO = new DataImportHdrDAO(oAppUserInfo);
                return oDataImportHrdDAO.GetDataImportStatusByCompanyID(CompanyID, UserID, RoleID);
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
            // return Blank Object
            //return new List<DataImportHdrInfo>();
        }


        public void UpdateDataImportForForceCommit(DataImportHdrInfo oDataImportHdrInfo, AppUserInfo oAppUserInfo)
        {
            IDbConnection oConnection = null;
            IDbTransaction oTransaction = null;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                DataImportHdrDAO oDataImportHdrDAO = new DataImportHdrDAO(oAppUserInfo);
                oConnection = oDataImportHdrDAO.CreateConnection();
                oConnection.Open();
                oTransaction = oConnection.BeginTransaction();
                oDataImportHdrDAO.UpdateDataImportForForceCommit(oDataImportHdrInfo, oConnection, oTransaction);
                SystemLockdownInfo oSystemLockdownInfo = oDataImportHdrInfo.SystemLockdownInfo;
                if (oSystemLockdownInfo != null)
                {
                    oSystemLockdownInfo.DataImportID = oDataImportHdrInfo.DataImportID;
                    oSystemLockdownInfo.AddedBy = oDataImportHdrInfo.RevisedBy;
                    oSystemLockdownInfo.DateAdded = oDataImportHdrInfo.DateRevised;
                    SystemLockdownDAO oSystemLockdownDAO = new SystemLockdownDAO(oAppUserInfo);
                    oSystemLockdownDAO.Save(oSystemLockdownInfo, oConnection, oTransaction);
                }
                oTransaction.Commit();
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

        public void InsertDataImportGLDataRecItemSchedule(DataImportHdrInfo oDataImportHdrInfo
           , List<GLDataRecurringItemScheduleInfo> oGLDataRecurringItemScheduleInfoCollection, string failureMsg, out int rowAffected, AppUserInfo oAppUserInfo)
        {
            IDbConnection oConnection = null;
            IDbTransaction oTransaction = null;
            int companyID = oDataImportHdrInfo.CompanyID.Value;
            DateTime dateAdded = oDataImportHdrInfo.DateAdded.Value;
            string addedBy = oDataImportHdrInfo.AddedBy;
            int newDataImportId;
            int? rowsAffected = 0;
            rowAffected = 0;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                DataImportHdrDAO oDataImportHrdDAO = new DataImportHdrDAO(oAppUserInfo);
                oConnection = oDataImportHrdDAO.CreateConnection();
                oConnection.Open();
                oTransaction = oConnection.BeginTransaction();//Begin transaction
                //Save DataImportHDR
                oDataImportHrdDAO.Save(oDataImportHdrInfo, oConnection, oTransaction);
                if (oDataImportHdrInfo.DataImportID.HasValue)
                {
                    if (oDataImportHdrInfo.DataImportRecItemUploadInfo != null)
                    {
                        oDataImportHdrInfo.DataImportRecItemUploadInfo.DataImportID = oDataImportHdrInfo.DataImportID;
                        DataImportRecItemUploadDAO oDataImportRecItemUploadDAO = new DataImportRecItemUploadDAO(oAppUserInfo);
                        oDataImportRecItemUploadDAO.Save(oDataImportHdrInfo.DataImportRecItemUploadInfo, oConnection, oTransaction);
                    }
                    if (oGLDataRecurringItemScheduleInfoCollection != null && oGLDataRecurringItemScheduleInfoCollection.Count > 0)
                    {
                        oGLDataRecurringItemScheduleInfoCollection[0].RecordSourceID = oDataImportHdrInfo.DataImportID.Value;
                        //int sourceID = oDataImportHdrInfo.DataImportID.Value;
                        //short RecordSourceTypeID = (short)ARTEnums.RecordSourceType.DataImport;
                        GLDataRecurringItemScheduleDAO oGLDataRecurringItemScheduleDAO = new GLDataRecurringItemScheduleDAO(oAppUserInfo);
                        //oGLDataRecurringItemScheduleDAO.InsertRecInputItem(oGLDataRecurringItemScheduleInfoCollection, (short)ARTEnums.AccountAttribute.ReconciliationTemplate, oConnection, oTransaction);
                        oGLDataRecurringItemScheduleDAO.InsertGLDataRecurringItemScheduleInfoCollection(oGLDataRecurringItemScheduleInfoCollection, oConnection, oTransaction);

                    }
                    newDataImportId = oDataImportHdrInfo.DataImportID.Value;
                    DataImportFailureMessageDAO oDataImportFailureMessageDAO = new DataImportFailureMessageDAO(oAppUserInfo);
                    //Save Failure Message
                    rowsAffected = oDataImportFailureMessageDAO.InsertDataImportFailureMsg(newDataImportId
                        , failureMsg, dateAdded, addedBy, oConnection, oTransaction);

                    oTransaction.Commit();
                }
                else
                {
                    throw new ARTException(5000042);//Error while saving data to database
                }
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

        public int? DeleteDataImportByDataImportIDs(List<int> oDataImportIDCollection, int companyID
            , int recPeriodID
            , string revisedBy
            , DateTime dateRevised
            , short NotStartedRecPeriodStatusID
            , AppUserInfo oAppUserInfo)
        {
            int? rowsAffected = null;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                DataImportHdrDAO oDataImportHrdDAO = new DataImportHdrDAO(oAppUserInfo);
                DataTable dtIDs = ServiceHelper.ConvertIDCollectionToDataTable(oDataImportIDCollection);
                rowsAffected = oDataImportHrdDAO.DeleteDataImportByDataImportID(dtIDs, companyID, recPeriodID
                                    , revisedBy, dateRevised, NotStartedRecPeriodStatusID);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return rowsAffected;
        }

        public void UpdateDataImportHiddenStatusByDataImportID(int dataImportID, bool isHidden, AppUserInfo oAppUserInfo)
        {
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                DataImportHdrDAO oDataImportHdrDao = new DataImportHdrDAO(oAppUserInfo);
                oDataImportHdrDao.UpdateDataImportHiddenStatusByDataImportID(dataImportID, isHidden);
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


        public bool IsGLDataUploaded(int recPeriodID, byte DataImportID, byte DataImportStatusID, AppUserInfo oAppUserInfo)
        {
            bool isGLDataUploaded = false;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                DataImportHdrDAO oDataImportHdrDAO = new DataImportHdrDAO(oAppUserInfo);
                isGLDataUploaded = oDataImportHdrDAO.IsGLDataUploaded(recPeriodID, DataImportID, DataImportStatusID);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return isGLDataUploaded;
        }



        public bool IsAnyAccountAssigned(short? roleID, int? userID, int? recPeriodID, AppUserInfo oAppUserInfo)
        {
            bool isAnyAccountAssigned = false;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                DataImportHdrDAO oDataImportHdrDAO = new DataImportHdrDAO(oAppUserInfo);
                isAnyAccountAssigned = oDataImportHdrDAO.IsAnyAccountAssigned(roleID, userID, recPeriodID);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return isAnyAccountAssigned;
        }


        #endregion


        public void InsertMatchingGLDataRecItem(List<GLDataRecItemInfo> oGLDataRecItemInfoCollection, out int rowAffected, AppUserInfo oAppUserInfo)
        {
            List<GLDataRecItemInfo> oTempGLDataRecItemInfoCollection = new List<GLDataRecItemInfo>();
            IDbConnection oConnection = null;
            IDbTransaction oTransaction = null;
            int? rowsAffected = null;
            rowAffected = 0;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                GLDataRecItemDAO oGLDataRecItemDAO = new GLDataRecItemDAO(oAppUserInfo);
                oConnection = oGLDataRecItemDAO.CreateConnection();
                oConnection.Open();
                oTransaction = oConnection.BeginTransaction();//Begin transaction
                GLDataRecItemInfo oGLDataRecItemInfo = new GLDataRecItemInfo();
                if (oGLDataRecItemInfoCollection.Count > 0)
                {
                    oGLDataRecItemInfo = oGLDataRecItemInfoCollection[0];
                    DataTable dtGLDataRecItem = DataImportServiceHelper.ConvertGLDataRecItemListToDataTable(oGLDataRecItemInfoCollection);
                    oTempGLDataRecItemInfoCollection = oGLDataRecItemDAO.BulkInsertRecInputItem(dtGLDataRecItem, oGLDataRecItemInfo, (short)ARTEnums.AccountAttribute.ReconciliationTemplate, out  rowsAffected, oConnection, oTransaction);
                }
                for (int i = 0; i < oTempGLDataRecItemInfoCollection.Count; i++)
                {
                    oGLDataRecItemInfoCollection.Find(o => o.IndexID == oTempGLDataRecItemInfoCollection[i].IndexID).RecItemNumber = oTempGLDataRecItemInfoCollection[i].RecItemNumber;

                }
                rowAffected = (int)rowsAffected;
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

        public void InsertDataImportGLDataRecItem(DataImportHdrInfo oDataImportHdrInfo
          , List<GLDataRecItemInfo> oGLDataRecItemInfoCollection, string failureMsg, out int rowAffected, AppUserInfo oAppUserInfo)
        {
            IDbConnection oConnection = null;
            IDbTransaction oTransaction = null;
            int companyID = oDataImportHdrInfo.CompanyID.Value;
            DateTime dateAdded = oDataImportHdrInfo.DateAdded.Value;
            string addedBy = oDataImportHdrInfo.AddedBy;
            int newDataImportId;
            int? rowsAffected = 0;
            rowAffected = 0;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                DataImportHdrDAO oDataImportHrdDAO = new DataImportHdrDAO(oAppUserInfo);
                oConnection = oDataImportHrdDAO.CreateConnection();
                oConnection.Open();
                oTransaction = oConnection.BeginTransaction();//Begin transaction
                //Save DataImportHDR
                oDataImportHrdDAO.Save(oDataImportHdrInfo, oConnection, oTransaction);
                GLDataRecItemInfo oGLDataRecItemInfo = new GLDataRecItemInfo();
                if (oDataImportHdrInfo.DataImportID.HasValue)
                {
                    newDataImportId = oDataImportHdrInfo.DataImportID.Value;
                    GLDataRecItemDAO oGLDataRecItemDAO = new GLDataRecItemDAO(oAppUserInfo);

                    if (oGLDataRecItemInfoCollection.Count > 0)
                    {
                        oGLDataRecItemInfo = oGLDataRecItemInfoCollection[0];
                        oGLDataRecItemInfo.RecordSourceID = newDataImportId;
                        oGLDataRecItemInfo.DataImportID = newDataImportId;
                        DataTable dtGLDataRecItem = DataImportServiceHelper.ConvertGLDataRecItemListToDataTable(oGLDataRecItemInfoCollection);
                        oGLDataRecItemDAO.BulkInsertRecInputItem(dtGLDataRecItem, oGLDataRecItemInfo, (short)ARTEnums.AccountAttribute.ReconciliationTemplate, out  rowsAffected, oConnection, oTransaction);
                    }

                    rowAffected = (int)rowsAffected;

                    if (rowsAffected.HasValue && rowsAffected > 0)
                    {
                        newDataImportId = oDataImportHdrInfo.DataImportID.Value;
                        DataImportFailureMessageDAO oDataImportFailureMessageDAO = new DataImportFailureMessageDAO(oAppUserInfo);
                        //Save Failure Message
                        rowsAffected = oDataImportFailureMessageDAO.InsertDataImportFailureMsg(newDataImportId
                            , failureMsg, dateAdded, addedBy, oConnection, oTransaction);
                        if (rowsAffected > 0)
                        {
                            oTransaction.Commit();
                        }
                        else
                        {
                            throw new ARTException(5000042);//Error while saving data to database
                        }

                    }
                    else
                    {
                        throw new ARTException(5000042);//Error while saving data to database
                    }
                }
                else
                {
                    throw new ARTException(5000042);//Error while saving data to database
                }
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

        public void InsertMatchingGLDataScheduleRecItem(List<GLDataRecurringItemScheduleInfo> oGLDataRecurringItemScheduleInfoCollection, out int rowAffected, AppUserInfo oAppUserInfo)
        {

            List<GLDataRecurringItemScheduleInfo> oTempGLDataRecurringItemScheduleInfoCollection = new List<GLDataRecurringItemScheduleInfo>();
            IDbConnection oConnection = null;
            IDbTransaction oTransaction = null;
            int? rowsAffected = null;
            rowAffected = 0;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                GLDataRecurringItemScheduleDAO oGLDataRecurringItemScheduleDAO = new GLDataRecurringItemScheduleDAO(oAppUserInfo);
                oConnection = oGLDataRecurringItemScheduleDAO.CreateConnection();
                oConnection.Open();
                oTransaction = oConnection.BeginTransaction();//Begin transaction
                if (oGLDataRecurringItemScheduleInfoCollection.Count > 0)
                {
                    oTempGLDataRecurringItemScheduleInfoCollection = oGLDataRecurringItemScheduleDAO.InsertGLDataRecurringItemScheduleInfoCollection(oGLDataRecurringItemScheduleInfoCollection, oConnection, oTransaction);
                }
                for (int i = 0; i < oTempGLDataRecurringItemScheduleInfoCollection.Count; i++)
                {
                    oGLDataRecurringItemScheduleInfoCollection.Find(o => o.IndexID == oTempGLDataRecurringItemScheduleInfoCollection[i].IndexID).RecItemNumber = oTempGLDataRecurringItemScheduleInfoCollection[i].RecItemNumber;

                }
                rowsAffected = oGLDataRecurringItemScheduleInfoCollection.Count;
                rowAffected = (int)rowsAffected;
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

        public void InsertMatchingGLDataWriteOnOffRecItem(List<GLDataWriteOnOffInfo> oGLDataWriteOnOffInfoCollection, out int rowAffected, AppUserInfo oAppUserInfo)
        {
            List<GLDataWriteOnOffInfo> oTempGLDataWriteOnOffInfoCollection = new List<GLDataWriteOnOffInfo>();
            IDbConnection oConnection = null;
            IDbTransaction oTransaction = null;
            int? rowsAffected = null;
            rowAffected = 0;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                GLDataWriteOnOffDAO oGLDataWriteOnOffDAO = new GLDataWriteOnOffDAO(oAppUserInfo);
                oConnection = oGLDataWriteOnOffDAO.CreateConnection();
                oConnection.Open();
                oTransaction = oConnection.BeginTransaction();//Begin transaction
                GLDataWriteOnOffInfo oGLDataWriteOnOffInfo = new GLDataWriteOnOffInfo();
                if (oGLDataWriteOnOffInfoCollection.Count > 0)
                {
                    oGLDataWriteOnOffInfo = oGLDataWriteOnOffInfoCollection[0];
                    DataTable dtGLDataWriteOnOffRecItem = DataImportServiceHelper.ConvertGLDataWriteOnOffRecItemListToDataTable(oGLDataWriteOnOffInfoCollection);
                    oTempGLDataWriteOnOffInfoCollection = oGLDataWriteOnOffDAO.BulkInsertGLDataWriteOnOffRecItems(dtGLDataWriteOnOffRecItem, oGLDataWriteOnOffInfo, (short)ARTEnums.AccountAttribute.ReconciliationTemplate, out  rowsAffected, oConnection, oTransaction);
                }
                for (int i = 0; i < oTempGLDataWriteOnOffInfoCollection.Count; i++)
                {
                    oGLDataWriteOnOffInfoCollection.Find(o => o.IndexID == oTempGLDataWriteOnOffInfoCollection[i].IndexID).RecItemNumber = oTempGLDataWriteOnOffInfoCollection[i].RecItemNumber;

                }
                rowAffected = (int)rowsAffected;
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
        public void CloseMatchingGLDataRecItem(List<GLDataRecItemInfo> oGLDataRecItemInfoCollection, DateTime? CloseDate, out int rowAffected, AppUserInfo oAppUserInfo)
        {
            List<GLDataRecItemInfo> oTempGLDataRecItemInfoCollection = new List<GLDataRecItemInfo>();
            IDbConnection oConnection = null;
            IDbTransaction oTransaction = null;
            int? rowsAffected = null;
            rowAffected = 0;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                GLDataRecItemDAO oGLDataRecItemDAO = new GLDataRecItemDAO(oAppUserInfo);
                oConnection = oGLDataRecItemDAO.CreateConnection();
                oConnection.Open();
                oTransaction = oConnection.BeginTransaction();//Begin transaction
                GLDataRecItemInfo oGLDataRecItemInfo = new GLDataRecItemInfo();
                if (oGLDataRecItemInfoCollection.Count > 0)
                {
                    oGLDataRecItemInfo = oGLDataRecItemInfoCollection[0];
                    DataTable dtMatchSetGLDataRecItem = DataImportServiceHelper.GetMatchSetGLDataRecItemTable(oGLDataRecItemInfoCollection);
                    oTempGLDataRecItemInfoCollection = oGLDataRecItemDAO.CloseMatchingGLDataRecItem(dtMatchSetGLDataRecItem, oGLDataRecItemInfo, (short)ARTEnums.AccountAttribute.ReconciliationTemplate, CloseDate, out  rowsAffected, oConnection, oTransaction);
                    for (int i = 0; i < oGLDataRecItemInfoCollection.Count; i++)
                    {
                        oGLDataRecItemInfoCollection[i].CloseDate = oTempGLDataRecItemInfoCollection[0].CloseDate;

                    }
                }

                rowAffected = (int)rowsAffected;
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
        public void CloseMatchingGLDataWriteOnOffItem(List<GLDataWriteOnOffInfo> oGLDataWriteOnOffInfoCollection, DateTime? CloseDate, out int rowAffected, AppUserInfo oAppUserInfo)
        {
            List<GLDataWriteOnOffInfo> oTempGLDataWriteOnOffInfoCollection = new List<GLDataWriteOnOffInfo>();
            IDbConnection oConnection = null;
            IDbTransaction oTransaction = null;
            int? rowsAffected = null;
            rowAffected = 0;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                GLDataWriteOnOffDAO oGLDataWriteOnOffDAO = new GLDataWriteOnOffDAO(oAppUserInfo);
                oConnection = oGLDataWriteOnOffDAO.CreateConnection();
                oConnection.Open();
                oTransaction = oConnection.BeginTransaction();//Begin transaction
                GLDataWriteOnOffInfo oGLDataWriteOnOffInfo = new GLDataWriteOnOffInfo();
                if (oGLDataWriteOnOffInfoCollection.Count > 0)
                {
                    oGLDataWriteOnOffInfo = oGLDataWriteOnOffInfoCollection[0];
                    DataTable dtMatchSetGLDataRecItem = DataImportServiceHelper.GetMatchSetGLDataRecItemTable(oGLDataWriteOnOffInfoCollection);
                    oTempGLDataWriteOnOffInfoCollection = oGLDataWriteOnOffDAO.CloseMatchingGLDataWriteOnOffItem(dtMatchSetGLDataRecItem, oGLDataWriteOnOffInfo, (short)ARTEnums.AccountAttribute.ReconciliationTemplate, CloseDate, out  rowsAffected, oConnection, oTransaction);
                    for (int i = 0; i < oGLDataWriteOnOffInfoCollection.Count; i++)
                    {
                        oGLDataWriteOnOffInfoCollection[i].CloseDate = oTempGLDataWriteOnOffInfoCollection[0].CloseDate;

                    }
                }

                rowAffected = (int)rowsAffected;
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
        public void CloseMatchingRecurringScheduleItems(List<GLDataRecurringItemScheduleInfo> oGLDataRecurringItemScheduleInfoCollection, DateTime? CloseDate, out int rowAffected, AppUserInfo oAppUserInfo)
        {
            List<GLDataRecurringItemScheduleInfo> oTempGLDataRecurringItemScheduleInfoCollection = new List<GLDataRecurringItemScheduleInfo>();
            IDbConnection oConnection = null;
            IDbTransaction oTransaction = null;
            int? rowsAffected = null;
            rowAffected = 0;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                GLDataRecurringItemScheduleDAO oGLDataRecurringItemScheduleDAO = new GLDataRecurringItemScheduleDAO(oAppUserInfo);
                oConnection = oGLDataRecurringItemScheduleDAO.CreateConnection();
                oConnection.Open();
                oTransaction = oConnection.BeginTransaction();//Begin transaction
                GLDataRecurringItemScheduleInfo oGLDataRecurringItemScheduleInfo = new GLDataRecurringItemScheduleInfo();
                if (oGLDataRecurringItemScheduleInfoCollection.Count > 0)
                {
                    oGLDataRecurringItemScheduleInfo = oGLDataRecurringItemScheduleInfoCollection[0];
                    DataTable dtMatchSetGLDataRecItem = DataImportServiceHelper.GetMatchSetGLDataRecItemTable(oGLDataRecurringItemScheduleInfoCollection);
                    oTempGLDataRecurringItemScheduleInfoCollection = oGLDataRecurringItemScheduleDAO.CloseMatchingRecurringScheduleItems(dtMatchSetGLDataRecItem, oGLDataRecurringItemScheduleInfo, (short)ARTEnums.AccountAttribute.ReconciliationTemplate, CloseDate, out  rowsAffected, oConnection, oTransaction);
                    for (int i = 0; i < oGLDataRecurringItemScheduleInfoCollection.Count; i++)
                    {
                        oGLDataRecurringItemScheduleInfoCollection[i].CloseDate = oTempGLDataRecurringItemScheduleInfoCollection[0].CloseDate;

                    }
                }

                rowAffected = (int)rowsAffected;
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
        public List<DataImportHdrInfo> GetRecItemDataImportStatus(RecItemUploadParamInfo oRecItemUploadParamInfo, AppUserInfo oAppUserInfo)
        {
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                DataImportHdrDAO oDataImportHrdDAO = new DataImportHdrDAO(oAppUserInfo);
                return oDataImportHrdDAO.GetRecItemDataImportStatus(oRecItemUploadParamInfo.RecPeriodID, oRecItemUploadParamInfo.UserID, oRecItemUploadParamInfo.RoleID, oRecItemUploadParamInfo.GLDataID, oRecItemUploadParamInfo.RecCategoryID, oRecItemUploadParamInfo.RecCategoryTypeID);
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

        public int? InsertGLDataRecItemScheduleBulk(Int64? GLDataID, Int32? RecPeriodID,
            List<GLDataRecurringItemScheduleInfo> oGLDataRecurringItemScheduleInfoList,
            string addedBy, DateTime? dateAdded, AppUserInfo oAppUserInfo)
        {
            IDbConnection oConnection = null;
            IDbTransaction oTransaction = null;
            int? rowAffected = 0;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                DataImportHdrDAO oDataImportHrdDAO = new DataImportHdrDAO(oAppUserInfo);
                oConnection = oDataImportHrdDAO.CreateConnection();
                oConnection.Open();
                oTransaction = oConnection.BeginTransaction();//Begin transaction
                if (oGLDataRecurringItemScheduleInfoList != null && oGLDataRecurringItemScheduleInfoList.Count > 0)
                {
                    GLDataRecurringItemScheduleDAO oGLDataRecurringItemScheduleDAO = new GLDataRecurringItemScheduleDAO(oAppUserInfo);
                    rowAffected = oGLDataRecurringItemScheduleDAO.InsertGLDataRecItemScheduleBulk(GLDataID, RecPeriodID, oGLDataRecurringItemScheduleInfoList, addedBy, dateAdded, oConnection, oTransaction);
                }
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
            return rowAffected;
        }
        public List<DataImportHdrInfo> GetGeneralTaskImportStatus(DataImportParamInfo oDataImportParamInfo, AppUserInfo oAppUserInfo)
        {
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                DataImportHdrDAO oDataImportHrdDAO = new DataImportHdrDAO(oAppUserInfo);
                return oDataImportHrdDAO.GetGeneralTaskImportStatus(oDataImportParamInfo.RecPeriodID, oDataImportParamInfo.UserID, oDataImportParamInfo.RoleID, oDataImportParamInfo.DataImportTypeID);
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

        #region DataImportTemplate
        public int SaveImportTemplate(ImportTemplateHdrInfo oImportTemplateInfo, DataTable dt, AppUserInfo oAppUserInfo)
        {
            IDbConnection oConnection = null;
            IDbTransaction oTransaction = null;
            int result = 0;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                DataImportTemplateDAO oDataImportHrdDAO = new DataImportTemplateDAO(oAppUserInfo);
                oConnection = oDataImportHrdDAO.CreateConnection();
                oConnection.Open();
                oTransaction = oConnection.BeginTransaction();
                result = oDataImportHrdDAO.SaveImportTemplate(oImportTemplateInfo, dt, oConnection, oTransaction);
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
            return result;
        }

        public ImportTemplateHdrInfo GetTemplateFields(int TemplateId, AppUserInfo oAppUserInfo)
        {
            try
            {
                ImportTemplateHdrInfo importTemplatelst = new ImportTemplateHdrInfo();
                ServiceHelper.SetConnectionString(oAppUserInfo);
                DataImportTemplateDAO oDataImportHrdDAO = new DataImportTemplateDAO(oAppUserInfo);
                importTemplatelst = oDataImportHrdDAO.GetTemplateFields(TemplateId);

                if (importTemplatelst != null)
                    importTemplatelst.ImportTemplateFieldsInfoList = oDataImportHrdDAO.GetTemplateFieldsLst(TemplateId);

                return importTemplatelst;
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

        public List<ImportFieldMstInfo> GetFieldsMst(int CompanyID, short? DataImportTypeID, AppUserInfo appUserInfo)
        {
            try
            {
                List<ImportFieldMstInfo> oImportFieldMstInfoLst = new List<ImportFieldMstInfo>();
                ServiceHelper.SetConnectionString(appUserInfo);
                DataImportTemplateDAO oDataImportHrdDAO = new DataImportTemplateDAO(appUserInfo);
                oImportFieldMstInfoLst = oDataImportHrdDAO.GetFieldsMst(CompanyID, DataImportTypeID);
                return oImportFieldMstInfoLst;
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, appUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, appUserInfo);
            }
            return null;
        }
        public int SaveImportTemplateMapping(DataTable dt, ImportTemplateFieldMappingInfo oImportTemplateFieldMappingInfo, AppUserInfo appUserInfo)
        {
            int result = 0;
            IDbConnection oConnection = null;
            IDbTransaction oTransaction = null;
            try
            {
                ServiceHelper.SetConnectionString(appUserInfo);
                DataImportTemplateDAO oDataImportHrdDAO = new DataImportTemplateDAO(appUserInfo);
                oConnection = oDataImportHrdDAO.CreateConnection();
                oConnection.Open();
                oTransaction = oConnection.BeginTransaction();
                result = oDataImportHrdDAO.SaveImportTemplateMapping(dt, oImportTemplateFieldMappingInfo, oConnection, oTransaction);
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
            return result;
        }

        public List<ImportTemplateHdrInfo> GetAllTemplateImport(int CompanyId,int UserId,int RoleId,AppUserInfo appUserInfo)
        {
            try
            {
                List<ImportTemplateHdrInfo> oImportTemplateInfoLst = new List<ImportTemplateHdrInfo>();
                ServiceHelper.SetConnectionString(appUserInfo);
                DataImportTemplateDAO oDataImportHrdDAO = new DataImportTemplateDAO(appUserInfo);
                oImportTemplateInfoLst = oDataImportHrdDAO.GetAllTemplateImport(CompanyId,UserId,RoleId);
                return oImportTemplateInfoLst;
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, appUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, appUserInfo);
            }
            return null;
        }
        public void DeleteMappingData(DataTable dt, ImportTemplateHdrInfo oImportTemplateInfo, AppUserInfo appUserInfo)
        {
            IDbConnection oConnection = null;
            IDbTransaction oTransaction = null;
            try
            {
                ServiceHelper.SetConnectionString(appUserInfo);
                DataImportTemplateDAO oDataImportHrdDAO = new DataImportTemplateDAO(appUserInfo);
                oConnection = oDataImportHrdDAO.CreateConnection();
                oConnection.Open();
                oTransaction = oConnection.BeginTransaction();
                oDataImportHrdDAO.DeleteMappingData(dt, oImportTemplateInfo, oConnection, oTransaction);
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
        public string GetImportTemplateSheetName(int ImportTemplateID, AppUserInfo oAppUserInfo)
        {
            string SheetName=string.Empty;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                DataImportHdrDAO oDataImportHdrDAO = new DataImportHdrDAO(oAppUserInfo);
                SheetName = oDataImportHdrDAO.GetImportTemplateSheetName(ImportTemplateID);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return SheetName;
        }

        public List<ImportTemplateFieldMappingInfo> GetImportTemplateFieldMappingInfoList(int? ImportTemplateID, AppUserInfo oAppUserInfo)
        {
            List<ImportTemplateFieldMappingInfo> oImportTemplateFieldMappingInfoList = null;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                DataImportHdrDAO oDataImportHdrDAO = new DataImportHdrDAO(oAppUserInfo);
                oImportTemplateFieldMappingInfoList = oDataImportHdrDAO.GetImportTemplateFieldMappingInfoList(ImportTemplateID);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return oImportTemplateFieldMappingInfoList;
        }

        public List<ImportTemplateFieldMappingInfo> GetAllDataImportFieldsWithMapping(int dataImportID, AppUserInfo oAppUserInfo)
        {
            List<ImportTemplateFieldMappingInfo> oImportTemplateFieldMappingInfoList = null;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                DataImportTemplateDAO oDataImportTemplateDAO = new DataImportTemplateDAO(oAppUserInfo);
                oImportTemplateFieldMappingInfoList = oDataImportTemplateDAO.GetAllDataImportFieldsWithMapping(dataImportID);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return oImportTemplateFieldMappingInfoList;
        }

        public int SaveDataImportSchedule(DataImportScheduleInfo oDataImportScheduleInfo, DataTable dt, AppUserInfo appUserInfo)
        {
            int result = 0;
            IDbConnection oConnection = null;
            IDbTransaction oTransaction = null;
            try
            {
                ServiceHelper.SetConnectionString(appUserInfo);
                DataImportTemplateDAO oDataImportHrdDAO = new DataImportTemplateDAO(appUserInfo);
                oConnection = oDataImportHrdDAO.CreateConnection();
                oConnection.Open();
                oTransaction = oConnection.BeginTransaction();
                result = oDataImportHrdDAO.SaveDataImportSchedule(oDataImportScheduleInfo, dt, oConnection, oTransaction);
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
            return result;
        }

        public List<DataImportScheduleInfo> GetDataImportSchedule(int? UserID, short? RoleID,AppUserInfo oAppUserInfo)
        {
            try
            {
                List<DataImportScheduleInfo> oDataImportScheduleInfolst = new List<DataImportScheduleInfo>();
                ServiceHelper.SetConnectionString(oAppUserInfo);
                DataImportTemplateDAO oDataImportHrdDAO = new DataImportTemplateDAO(oAppUserInfo);
                oDataImportScheduleInfolst = oDataImportHrdDAO.GetDataImportSchedule(UserID,RoleID);
                return oDataImportScheduleInfolst;
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

        public List<DataImportMessageInfo> GetAllWarningMsg(short DataImportTypeId, AppUserInfo oAppUserInfo)
        {
            try
            {
                List<DataImportMessageInfo> oDataImportMessageInfolst = new List<DataImportMessageInfo>();
                ServiceHelper.SetConnectionString(oAppUserInfo);
                DataImportTemplateDAO oDataImportHrdDAO = new DataImportTemplateDAO(oAppUserInfo);
                oDataImportMessageInfolst = oDataImportHrdDAO.GetAllWarningMsg(DataImportTypeId);
                return oDataImportMessageInfolst;
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

        public int SaveDataImportWarningPreferences(DataTable dt, DataImportWarningPreferencesInfo oDataImportWarningPreferencesInfo, AppUserInfo appUserInfo)
        {
            int result = 0;
            IDbConnection oConnection = null;
            IDbTransaction oTransaction = null;
            try
            {
                ServiceHelper.SetConnectionString(appUserInfo);
                DataImportTemplateDAO oDataImportHrdDAO = new DataImportTemplateDAO(appUserInfo);
                oConnection = oDataImportHrdDAO.CreateConnection();
                oConnection.Open();
                oTransaction = oConnection.BeginTransaction();
                result = oDataImportHrdDAO.SaveDataImportWarningPreferences(dt, oDataImportWarningPreferencesInfo, oConnection, oTransaction);
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
            return result;
        }

        public List<DataImportWarningPreferencesInfo> GetDataImportWarningPreferences(int? CurrentCompanyID, short DataImportTypeId, AppUserInfo oAppUserInfo)
        {
            try
            {
                List<DataImportWarningPreferencesInfo> oDataImportWarningPreferencesInfolst = new List<DataImportWarningPreferencesInfo>();
                ServiceHelper.SetConnectionString(oAppUserInfo);
                DataImportTemplateDAO oDataImportHrdDAO = new DataImportTemplateDAO(oAppUserInfo);
                oDataImportWarningPreferencesInfolst = oDataImportHrdDAO.GetDataImportWarningPreferences(CurrentCompanyID, DataImportTypeId);
                return oDataImportWarningPreferencesInfolst;
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

        public List<DataImportMessageInfo> GetDataImportMessageList(AppUserInfo oAppUserInfo)
        {
            List<DataImportMessageInfo> oDataImportMessageInfoList = null;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                DataImportTemplateDAO oDataImportTemplateDAO = new DataImportTemplateDAO(oAppUserInfo);
                oDataImportMessageInfoList = oDataImportTemplateDAO.GetDataImportMessageList();
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return oDataImportMessageInfoList;
        }

        public List<DataImportWarningPreferencesAuditInfo> GetAllWarningAuditList(int CurrentCompanyID,int CurrentUserID, short CurrentRoleID, AppUserInfo oAppUserInfo)
        {
            List<DataImportWarningPreferencesAuditInfo> oDataImportWarningPreferencesAuditInfoList = null;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                DataImportTemplateDAO oDataImportTemplateDAO = new DataImportTemplateDAO(oAppUserInfo);
                oDataImportWarningPreferencesAuditInfoList = oDataImportTemplateDAO.GetAllWarningAuditList(CurrentCompanyID,CurrentUserID, CurrentRoleID);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return oDataImportWarningPreferencesAuditInfoList;
        }


        #endregion
    }
}
