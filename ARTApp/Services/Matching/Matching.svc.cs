using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using SkyStem.ART.Client.Model.Matching;
using SkyStem.ART.Client.IServices;
using SkyStem.ART.App.DAO.Matching;
using SkyStem.ART.App.Utility;
using System.Data.SqlClient;

using SkyStem.ART.Client.Params;

using SkyStem.ART.Client.Params.Matching;
using System.Data;
using SkyStem.ART.Client.Model;
using SkyStem.ART.App.DAO;
using SkyStem.ART.Client.Data;
using SkyStem.ART.App.BLL;

namespace SkyStem.ART.App.Services
{
    // NOTE: If you change the class name "Matching" here, you must also update the reference to "Matching" in Web.config.
    public class Matching : IMatching
    {

        public List<MatchingSourceTypeInfo> GetAllMatchingSourceType( AppUserInfo oAppUserInfo)
        {
            List<MatchingSourceTypeInfo> oMatchingSourceTypeInfo = null;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                MatchingSourceTypeDAO oMatchingSourceTypeDAO = new MatchingSourceTypeDAO(oAppUserInfo);
                oMatchingSourceTypeInfo = oMatchingSourceTypeDAO.GetAllMatchingSourceType();
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return oMatchingSourceTypeInfo;
        }

        public List<DataTypeMstInfo> GetAllDataType( AppUserInfo oAppUserInfo)
        {
            List<DataTypeMstInfo> oDataTypeMstInfoCollection = null;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                DataTypeMstDAO oDataTypeMstDAO = new DataTypeMstDAO(oAppUserInfo);
                oDataTypeMstInfoCollection = oDataTypeMstDAO.GetAllDataType();
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return oDataTypeMstInfoCollection;
        }

        public List<MatchSetHdrInfo> SelectAllMatchSetHdrInfo(MatchingParamInfo oMatchingParamInfo, AppUserInfo oAppUserInfo)
        {
            List<MatchSetHdrInfo> oMatchSetHdrInfo = null;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                MatchSetHdrDAO oMatchSetHdrDAO = new MatchSetHdrDAO(oAppUserInfo);
                oMatchSetHdrInfo = oMatchSetHdrDAO.SelectAllMatchSetHdrInfo(oMatchingParamInfo);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return oMatchSetHdrInfo;
        }

        /// <summary>
        /// Saves Matching Source information in the database
        /// </summary>
        /// <param name="oMatchingParamInfo">List of Matching Source information</param>
        /// <returns>List of MatchingSourceDataImportHdrInfo</returns>
        public List<MatchingSourceDataImportHdrInfo> SaveMatchingSource(MatchingParamInfo oMatchingParamInfo, AppUserInfo oAppUserInfo)
        {
            List<MatchingSourceDataImportHdrInfo> oMatchingSourceDataImportHdrInfoCollection = null;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                MatchingSourceDataImportHdrDAO oMatchingSourceDataImportHdrDAO = new MatchingSourceDataImportHdrDAO(oAppUserInfo);
                DataTable dtMatchingSourceDatas = ServiceHelper.ConvertMatchingSourceDataImportHdrToDataTable(oMatchingParamInfo.oMatchingSourceDataImportHdrInfoList);
                oMatchingSourceDataImportHdrInfoCollection = oMatchingSourceDataImportHdrDAO.SaveMatchingSource(dtMatchingSourceDatas, oMatchingParamInfo.CompanyID.Value);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return oMatchingSourceDataImportHdrInfoCollection;
        }

        /// <summary>
        /// Get Matching Source Column
        /// </summary>
        /// <param name="oMatchingParamInfo">MatchingParamInfo-MatchingSourceDataImportID</param>
        /// <returns>MatchingSourceColumnInfo</returns>
        public List<MatchingSourceColumnInfo> GetMatchingSourceColumn(MatchingParamInfo oMatchingParamInfo, AppUserInfo oAppUserInfo)
        {
            List<MatchingSourceColumnInfo> oMatchingSourceColumnInfoCollection = null;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                MatchingSourceColumnDAO oMatchingSourceColumnDAO = new MatchingSourceColumnDAO(oAppUserInfo);
                oMatchingSourceColumnInfoCollection = oMatchingSourceColumnDAO.GetMatchingSourceColumn(oMatchingParamInfo);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return oMatchingSourceColumnInfoCollection;
        }

        /// <summary>
        /// Save Matching Source Column
        /// </summary>
        /// <param name="oMatchingParamInfo">MatchingParamInfo-List of MatchingSourceColumnInfo </param>
        /// <returns>true/false</returns>
        public bool SaveMatchingSourceColumn(MatchingParamInfo oMatchingParamInfo, AppUserInfo oAppUserInfo)
        {
            bool result = false;
            IDbConnection oConnection = null;
            IDbTransaction oTransaction = null;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                MatchingSourceColumnDAO oMatchingSourceColumnDAO = new MatchingSourceColumnDAO(oAppUserInfo);
                MatchingSourceDataImportHdrDAO oMatchingSourceData = new MatchingSourceDataImportHdrDAO(oAppUserInfo);

                oConnection = oMatchingSourceColumnDAO.CreateConnection();
                oConnection.Open();
                oTransaction = oConnection.BeginTransaction();

                DataTable dtMatchingSourceColumn = ServiceHelper.ConvertMatchingSourceColumnToDataTable(oMatchingParamInfo.oMatchingSourceColumnInfoList);
                result = oMatchingSourceColumnDAO.SaveMatchingSourceColumn(dtMatchingSourceColumn, oConnection, oTransaction);

                if (oMatchingParamInfo.IsSubmited.Value)
                {
                    DataTable dtMatchingSourceDataImportIDs = ServiceHelper.ConvertIDCollectionToDataTable(oMatchingParamInfo.IDList);
                    oMatchingSourceData.UpdateMatchingSourceDataImportStatus(dtMatchingSourceDataImportIDs, oMatchingParamInfo.DataImportStatusID.Value, oConnection, oTransaction);
                }

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
            finally
            {
                if (oTransaction != null && oTransaction.Connection != null)
                {
                    oTransaction.Rollback();
                    oTransaction.Dispose();
                }

                if ((oConnection != null) && (oConnection.State == ConnectionState.Open))
                {
                    oConnection.Close();
                    oConnection.Dispose();
                }
            }
            return result;
        }

        /// <summary>
        /// Get Matching Source Data Import
        /// </summary>
        /// <param name="oMatchingParamInfo">MatchingParamInfo - RecPeriodId+UserID+RoleID</param>
        /// <returns>List of MatchingSourceDataImportHdrInfo</returns>
        public List<MatchingSourceDataImportHdrInfo> GetMatchingSources(MatchingParamInfo oMatchingParamInfo, AppUserInfo oAppUserInfo)
        {
            List<MatchingSourceDataImportHdrInfo> oMatchingSourceDataImportInfoCollection = null;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                MatchingSourceDataImportHdrDAO oMatchingSourceDataImportHdrDAO = new MatchingSourceDataImportHdrDAO(oAppUserInfo);
                oMatchingSourceDataImportInfoCollection = oMatchingSourceDataImportHdrDAO.GetMatchingSources(oMatchingParamInfo);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return oMatchingSourceDataImportInfoCollection;
        }

        /// <summary>
        /// Get Match Set Sources for Matching Wizard
        /// </summary>
        /// <param name="oMatchingParamInfo"></param>
        /// <returns></returns>
        public List<MatchingSourceDataImportHdrInfo> GetAllMatchSetMatchingSources(MatchingParamInfo oMatchingParamInfo, AppUserInfo oAppUserInfo)
        {
            List<MatchingSourceDataImportHdrInfo> oMatchingSourceDataImportInfoCollection = null;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                MatchingSourceDataImportHdrDAO oMatchingSourceDataImportHdrDAO = new MatchingSourceDataImportHdrDAO(oAppUserInfo);
                oMatchingSourceDataImportInfoCollection = oMatchingSourceDataImportHdrDAO.GetAllMatchSetMatchingSources(oMatchingParamInfo);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return oMatchingSourceDataImportInfoCollection;
        }

        /// <summary>
        /// Delete Matching Source Data Import
        /// </summary>
        /// <param name="oMatchingParamInfo">MatchingParamInfo</param>
        /// <returns>List of IDCollection</returns>
        public bool DeleteMatchingSourceData(MatchingParamInfo oMatchingParamInfo, AppUserInfo oAppUserInfo)
        {
            bool result = false;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                MatchingSourceDataImportHdrDAO oMatchingSourceData = new MatchingSourceDataImportHdrDAO(oAppUserInfo);
                DataTable dtMatchingSourceDataImportIDs = ServiceHelper.ConvertIDCollectionToDataTable(oMatchingParamInfo.IDList);
                result = oMatchingSourceData.DeleteMatchingSourceData(dtMatchingSourceDataImportIDs, oMatchingParamInfo);
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

        /// <summary>
        /// Get Matching Source Data Import Info
        /// </summary>
        /// <param name="oMatchingParamInfo">MatchingParamInfo</param>
        /// <returns>MatchingSourceDataImportID</returns>
        public MatchingSourceDataImportHdrInfo GetMatchingSourceDataImportInfo(MatchingParamInfo oMatchingParamInfo, AppUserInfo oAppUserInfo)
        {
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                //MatchingSourceDataImportHdrDAO oMatchingSourceDataImportHdrDAO = new MatchingSourceDataImportHdrDAO(oAppUserInfo);
                return MatchingBLL.GetMatchingSourceDataImportInfo(oMatchingParamInfo, oAppUserInfo);
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
        /// Get Matching Source Data Import Info
        /// </summary>
        /// <param name="oMatchingParamInfo">MatchingParamInfo</param>
        /// <returns>true/false</returns>
        public bool UpdateMatchingSourceDataImportForForceCommit(MatchingParamInfo oMatchingParamInfo, AppUserInfo oAppUserInfo)
        {
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                MatchingSourceDataImportHdrDAO oMatchingSourceDataImportHdrDAO = new MatchingSourceDataImportHdrDAO(oAppUserInfo);
                return oMatchingSourceDataImportHdrDAO.UpdateMatchingSourceDataImportForForceCommit(oMatchingParamInfo);
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

        /// <summary>
        /// Get Key Fields By CompanyID
        /// </summary>
        /// <param name="oMatchingParamInfo">MatchingParamInfo : CompanyID</param>
        /// <returns>true/false</returns>
        public string GetKeyFieldsByCompanyID(MatchingParamInfo oMatchingParamInfo, AppUserInfo oAppUserInfo)
        {
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                MatchingSourceDataImportHdrDAO oMatchingSourceDataImportHdrDAO = new MatchingSourceDataImportHdrDAO(oAppUserInfo);
                return oMatchingSourceDataImportHdrDAO.GetKeyFieldsByCompanyID(oMatchingParamInfo);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }

            return "";
        }
        public long SaveMatchSet(MatchingParamInfo oMatchingParamInfo, AppUserInfo oAppUserInfo)
        {
            long matchSetID = 0;
            IDbConnection oConnection = null;
            IDbTransaction oTransaction = null;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                MatchSetHdrDAO oMatchSetHdrDAO = new MatchSetHdrDAO(oAppUserInfo);
                oConnection = oMatchSetHdrDAO.CreateConnection();
                oConnection.Open();

                oTransaction = oConnection.BeginTransaction();
                matchSetID = oMatchSetHdrDAO.SaveMatchSet(oMatchingParamInfo, oConnection, oTransaction);

                if (oMatchingParamInfo.IsMatchingSourceSelectionChanged)
                {
                    oMatchingParamInfo.MatchSetID = matchSetID;
                    MatchSetMachingSourceDataImportDAO oMatchSetMachingSourceDataImportDAO = new MatchSetMachingSourceDataImportDAO(oAppUserInfo);
                    oMatchSetMachingSourceDataImportDAO.SaveMatchSetMatchingSource(oMatchingParamInfo, oConnection, oTransaction);
                }
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
            finally
            {
                if ((oConnection != null) && (oConnection.State == ConnectionState.Open))
                {
                    oConnection.Close();
                    oConnection.Dispose();
                }
            }
            return matchSetID;
        }

        public List<GLDataHdrInfo> GetAccountDetails(MatchingParamInfo oMatchingParamInfo, AppUserInfo oAppUserInfo)
        {
            List<GLDataHdrInfo> oGLDataHdrInfoCollection = null;

            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                MatchingSourceAccountDAO oMatchingSourceAccountDAO = new MatchingSourceAccountDAO(oAppUserInfo);
                oGLDataHdrInfoCollection = oMatchingSourceAccountDAO.GetAccountDetails(oMatchingParamInfo);
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

        /// <summary>
        /// Get Match Set Matching Source Data
        /// </summary>
        /// <param name="oMatchingParamInfo">MatchingParamInfo - MatchingSourceDataImportIDs, MatchSetID, AccountID</param>
        /// <returns>MatchSetHdrInfo</returns>
        public MatchSetHdrInfo GetMatchSet(MatchingParamInfo oMatchingParamInfo, AppUserInfo oAppUserInfo)
        {
            MatchSetHdrInfo oMatchSetHdrInfo = null;
            try
            {
                // Get Match Set Hdr Data
                ServiceHelper.SetConnectionString(oAppUserInfo);
                oMatchSetHdrInfo = GetMatchSetHdr(oMatchingParamInfo, oAppUserInfo);
                if (oMatchSetHdrInfo != null)
                {
                    // Set Account ID and Matching Type ID
                    //oMatchingParamInfo.AccountID = oMatchSetHdrInfo.AccountID;
                    oMatchingParamInfo.MatchingTypeID = oMatchSetHdrInfo.MatchingTypeID;
                    // Get Match Set Matching Source
                    oMatchSetHdrInfo.MatchingSourceDataImportHdrInfoList = GetMatchSetMatchingSources(oMatchingParamInfo, oAppUserInfo);
                    // Get Match Set Combinations
                    oMatchSetHdrInfo.MatchSetSubSetCombinationInfoCollection = GetAllMatchSetSubSetCombination(oMatchingParamInfo, oAppUserInfo);
                }
                return oMatchSetHdrInfo;
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }

            return oMatchSetHdrInfo;
        }

        /// <summary>
        /// Gets the match set HDR.
        /// </summary>
        /// <param name="oMatchingParamInfo">The o matching param info.</param>
        /// <returns></returns>
        public MatchSetHdrInfo GetMatchSetHdr(MatchingParamInfo oMatchingParamInfo, AppUserInfo oAppUserInfo)
        {
            MatchSetHdrInfo oMatchSetHdrInfo = null;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                MatchSetHdrDAO oMatchSetHdrDAO = new MatchSetHdrDAO(oAppUserInfo);
                // Get Match Set Hdr Data
                oMatchSetHdrInfo = oMatchSetHdrDAO.GetMatchSetHdrInfo(oMatchingParamInfo);
                return oMatchSetHdrInfo;
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }

            return oMatchSetHdrInfo;
        }


        /// <summary>
        /// Get Match Set Matching Source Data
        /// </summary>
        /// <param name="oMatchingParamInfo">MatchingParamInfo - MatchingSourceDataImportID, MatchSetID</param>
        /// <returns>true/false</returns>
        public MatchSetMatchingSourceDataImportInfo GetMatchSetMatchingSourceDataImport(MatchingParamInfo oMatchingParamInfo, AppUserInfo oAppUserInfo)
        {
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                MatchSetMachingSourceDataImportDAO oMatchSetMachingSourceDataImportDAO = new MatchSetMachingSourceDataImportDAO(oAppUserInfo);
                return oMatchSetMachingSourceDataImportDAO.GetMatchSetMatchingSourceDataImport(oMatchingParamInfo);
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
        /// Get Match Set Matching Source Data
        /// </summary>
        /// <param name="oMatchingParamInfo">MatchingParamInfo - MatchingSourceDataImportID, AccountID</param>
        /// <returns>true/false</returns>
        public List<MatchingSourceDataInfo> GetMatchingSourceData(MatchingParamInfo oMatchingParamInfo, AppUserInfo oAppUserInfo)
        {
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                // MatchingSourceDataInfo oMatchingSourceDataInfo = new MatchingSourceDataInfo();
                MatchingSourceDataDAO oMatchingSourceDataDAO = new MatchingSourceDataDAO(oAppUserInfo);
                return oMatchingSourceDataDAO.GetMatchingSourceData(oMatchingParamInfo);
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
        /// Update Match Set Matching Source Data Import information in the database
        /// </summary>
        /// <param name="oMatchingParamInfo">List of Match Set Matching Source information</param>
        /// <returns>List of MatchSetMatchingSourceDataImportInfo</returns>
        public List<MatchSetSubSetCombinationInfo> UpdateMatchSetMatchingSourceDataImportInfo(MatchingParamInfo oMatchingParamInfo, AppUserInfo oAppUserInfo)
        {
            List<MatchSetSubSetCombinationInfo> oMatchSetSubSetCombinationInfoCollection = new List<MatchSetSubSetCombinationInfo>();
            IDbConnection oConnection = null;
            IDbTransaction oTransaction = null;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                MatchSetSubSetCombinationDAO oMatchSetSubSetCombinationDAO = new MatchSetSubSetCombinationDAO(oAppUserInfo);
                MatchSetMachingSourceDataImportDAO oMatchSetMachingSourceDataImportDAO = new MatchSetMachingSourceDataImportDAO(oAppUserInfo);

                oConnection = oMatchSetSubSetCombinationDAO.CreateConnection();
                oConnection.Open();
                oTransaction = oConnection.BeginTransaction();

                DataTable oMatchSetMachingSourceDataDataTable = ServiceHelper.ConvertMatchSetMatchingSourceToDataTable(oMatchingParamInfo.oMatchSetMatchingSourceDataImportInfoList);
                DataTable oMatchSetSubSetCombinationInfoDataTable = ServiceHelper.ConvertMatchSetSubSetCombinationToDataTable(oMatchingParamInfo.oMatchSetSubSetCombinationInfoList);

                if (oMatchSetMachingSourceDataImportDAO.UpdateMatchSetMatchingSource(oMatchSetMachingSourceDataDataTable, oConnection, oTransaction))
                {
                    oTransaction.Commit();
                    oTransaction.Dispose();
                    if ((oConnection != null) && (oConnection.State == ConnectionState.Open))
                    {
                        oConnection.Close();
                        oConnection.Dispose();
                    }
                }
                oMatchSetSubSetCombinationInfoCollection = oMatchSetSubSetCombinationDAO.SaveMatchSetSubSetCombination(oMatchSetSubSetCombinationInfoDataTable);

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
            return oMatchSetSubSetCombinationInfoCollection;
        }

        public void DeleteMatchSet(MatchingParamInfo oMatchingParamInfo, AppUserInfo oAppUserInfo)
        {
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                MatchSetHdrDAO oMatchSetHdrDAO = new MatchSetHdrDAO(oAppUserInfo);
                oMatchSetHdrDAO.DeleteMatchSet(oMatchingParamInfo);
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
        public void EditMatchSet(MatchingParamInfo oMatchingParamInfo, AppUserInfo oAppUserInfo)
        {
            IDbConnection oConnection = null;
            IDbTransaction oTransaction = null;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                MatchSetHdrDAO oMatchSetHdrDAO = new MatchSetHdrDAO(oAppUserInfo);
                oConnection = oMatchSetHdrDAO.CreateConnection();
                oConnection.Open();
                oTransaction = oConnection.BeginTransaction();
                oMatchSetHdrDAO.EditMatchSet(oMatchingParamInfo, oConnection, oTransaction);
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
            finally
            {

                if ((oConnection != null) && (oConnection.State == ConnectionState.Open))
                {
                    oConnection.Close();
                    oConnection.Dispose();
                }
            }          
        }

        public bool IsRecItemCreated(MatchingParamInfo oMatchingParamInfo, AppUserInfo oAppUserInfo)
        {
            bool bRecItemCreated = false;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                MatchSetHdrDAO oMatchSetHdrDAO = new MatchSetHdrDAO(oAppUserInfo);
                bRecItemCreated = oMatchSetHdrDAO.IsRecItemCreated(oMatchingParamInfo);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return bRecItemCreated;
        }

        /// <summary>
        /// Get Match Set Matching Sources
        /// </summary>
        /// <param name="oMatchingParamInfo"></param>
        /// <returns></returns>
        private List<MatchingSourceDataImportHdrInfo> GetMatchSetMatchingSources(MatchingParamInfo oMatchingParamInfo, AppUserInfo oAppUserInfo)
        {
           
            // Get Match Set Matching Sources
            MatchingSourceDataImportHdrDAO oMatchingSourceDataImportHdrDAO = new MatchingSourceDataImportHdrDAO(oAppUserInfo);
            List<MatchingSourceDataImportHdrInfo> oMatchingSourceDataImportHdrInfoList = null;
            oMatchingSourceDataImportHdrInfoList = oMatchingSourceDataImportHdrDAO.GetAllMatchingSourceDataImportInfoByMatchSetID(oMatchingParamInfo);

            // Get Matching Source Data
            List<MatchingSourceDataInfo> oMatchingSourceDataInfoList = GetMatchingSourceData(oMatchingParamInfo, oAppUserInfo);

            // Get Match Set Sub Set Data
            List<MatchSetMatchingSourceDataImportInfo> oMatchSetMatchingSourceDataImportInfoList = GetMatchSetMatchingSourceSubSetData(oMatchingParamInfo, oAppUserInfo);

            // Get Matching Source Columns
            oMatchingParamInfo.oMatchingSourceDataImportHdrInfoList = oMatchingSourceDataImportHdrInfoList;
            List<MatchingSourceColumnInfo> oMatchingSourceColumnInfoList = GetMatchingSourceColumns(oMatchingParamInfo, oAppUserInfo);

            if (oMatchingSourceDataImportHdrInfoList != null && oMatchingSourceDataImportHdrInfoList.Count > 0)
            {
                foreach (MatchingSourceDataImportHdrInfo oMatchingSourceDataImportHdrInfo in oMatchingSourceDataImportHdrInfoList)
                {
                    // Find Matching Source Data and Set into Parent
                    if (oMatchingSourceDataInfoList != null && oMatchingSourceDataInfoList.Count > 0)
                        oMatchingSourceDataImportHdrInfo.MatchingSourceData = oMatchingSourceDataInfoList.Find(T => T.MatchingSourceDataImportID == oMatchingSourceDataImportHdrInfo.MatchingSourceDataImportID);

                    // Find Sub Set Data and Set into Parent
                    if (oMatchSetMatchingSourceDataImportInfoList != null && oMatchSetMatchingSourceDataImportInfoList.Count > 0)
                        oMatchingSourceDataImportHdrInfo.MatchSetMatchingSourceSubSetData = oMatchSetMatchingSourceDataImportInfoList.Find(T => T.MatchingSourceDataImportID == oMatchingSourceDataImportHdrInfo.MatchingSourceDataImportID);

                    // Find Matching Source Columns and add into Parent
                    if (oMatchingSourceColumnInfoList != null && oMatchingSourceColumnInfoList.Count > 0)
                    {
                        if (oMatchingSourceDataImportHdrInfo.MatchingSourceColumnList == null)
                            oMatchingSourceDataImportHdrInfo.MatchingSourceColumnList = new List<MatchingSourceColumnInfo>();
                        oMatchingSourceDataImportHdrInfo.MatchingSourceColumnList.AddRange(oMatchingSourceColumnInfoList.FindAll(T => T.MatchingSourceDataImportID == oMatchingSourceDataImportHdrInfo.MatchingSourceDataImportID));
                    }
                }
            }
            return oMatchingSourceDataImportHdrInfoList;
        }

        /// <summary>
        /// Get Match Set Sub Set Data
        /// </summary>
        /// <param name="oMatchingParamInfo"></param>
        /// <returns></returns>
        private List<MatchSetMatchingSourceDataImportInfo> GetMatchSetMatchingSourceSubSetData(MatchingParamInfo oMatchingParamInfo, AppUserInfo oAppUserInfo)
        {
           
            MatchSetMachingSourceDataImportDAO oMatchSetMachingSourceDataImportDAO = new MatchSetMachingSourceDataImportDAO(oAppUserInfo);
            return oMatchSetMachingSourceDataImportDAO.GetMatchSetMatchingSourceDataImportByMatchSetID(oMatchingParamInfo);
        }

        /// <summary>
        /// Get Matching Source Columns
        /// </summary>
        /// <param name="oMatchingParamInfo"></param>
        /// <returns></returns>
        private List<MatchingSourceColumnInfo> GetMatchingSourceColumns(MatchingParamInfo oMatchingParamInfo, AppUserInfo oAppUserInfo)
        {
            DataTable dtMatchingSourceDataImportHdr =
                ServiceHelper.ConvertLongIDCollectionToDataTable(oMatchingParamInfo.oMatchingSourceDataImportHdrInfoList
                                                                    .Where(W => W.MatchingSourceDataImportID.HasValue)
                                                                    .Select(T => T.MatchingSourceDataImportID.Value).ToList());
            MatchingSourceColumnDAO oMatchingSourceColumnDAO = new MatchingSourceColumnDAO(oAppUserInfo);
            return oMatchingSourceColumnDAO.GetMatchingSourceColumnForMatchSet(dtMatchingSourceDataImportHdr);
        }

        /// <summary>
        /// Get Match Set Matching Configuration Rules
        /// </summary>
        /// <param name="oMatchingParamInfo"></param>
        /// <returns></returns>
        public List<MatchingConfigurationRuleInfo> GetMatchSetMatchingConfigurationRules(MatchingParamInfo oMatchingParamInfo, AppUserInfo oAppUserInfo)
        {
            ServiceHelper.SetConnectionString(oAppUserInfo);
            MatchingConfigurationRuleDAO oMatchingConfigurationRuleDAO = new MatchingConfigurationRuleDAO(oAppUserInfo);
            return oMatchingConfigurationRuleDAO.GetAllMatchingConfigurationRule(oMatchingParamInfo);
        }

        /// <summary>
        /// Get Rec Item Column list
        /// </summary>        
        /// <returns>RecItemColumnMstInfo</returns>
        public List<RecItemColumnMstInfo> GetAllRecItemColumns( AppUserInfo oAppUserInfo)
        {
            List<RecItemColumnMstInfo> oRecItemColumnMstInfoCollection = null;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                RecItemColumnMstDAO oRecItemColumnMstDAO = new RecItemColumnMstDAO(oAppUserInfo);
                oRecItemColumnMstInfoCollection = oRecItemColumnMstDAO.GetAllRecItemColumns();
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return oRecItemColumnMstInfoCollection;
        }


        public List<MatchingConfigurationInfo> GetAllMatchingConfiguration(MatchingParamInfo oMatchingParamInfo, AppUserInfo oAppUserInfo)
        {
            List<MatchingConfigurationInfo> oMatchingConfigurationInfoList = null;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                MatchingConfigurationDAO oMatchingConfigurationDAO = new MatchingConfigurationDAO(oAppUserInfo);
                oMatchingConfigurationInfoList = oMatchingConfigurationDAO.GetAllMatchingConfiguration(oMatchingParamInfo);
                MatchingParamInfo oMatchingParamInfoNew = new MatchingParamInfo();
                oMatchingParamInfoNew.IDList = (oMatchingConfigurationInfoList
                                                    .Where(W => W.MatchingConfigurationID.HasValue))
                                                    .Select(T => T.MatchingConfigurationID.Value).ToList();

                List<MatchingConfigurationRuleInfo> oMatchingConfigurationRuleInfoList = GetMatchSetMatchingConfigurationRules(oMatchingParamInfoNew, oAppUserInfo);
                // Find and add Configuration List to each Match Set Sub Set
                foreach (MatchingConfigurationInfo oMatchingConfigurationInfo in oMatchingConfigurationInfoList)
                {
                    if (oMatchingConfigurationInfo.MatchingConfigurationRuleInfoCollection == null)
                        oMatchingConfigurationInfo.MatchingConfigurationRuleInfoCollection = new List<MatchingConfigurationRuleInfo>();
                    oMatchingConfigurationInfo.MatchingConfigurationRuleInfoCollection.AddRange(oMatchingConfigurationRuleInfoList.FindAll(T => T.MatchingConfigurationID == oMatchingConfigurationInfo.MatchingConfigurationID));
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
            return oMatchingConfigurationInfoList;
        }

        /// <summary>
        /// Get Match Set Sub Set Combinations
        /// </summary>
        /// <param name="oMatchingParamInfo"></param>
        /// <returns></returns>
        public List<MatchSetSubSetCombinationInfo> GetAllMatchSetSubSetCombination(MatchingParamInfo oMatchingParamInfo, AppUserInfo oAppUserInfo)
        {
            List<MatchSetSubSetCombinationInfo> oMatchSetSubSetCombinationInfoList = null;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                // Get Match Set Sub Set Combinations
                MatchSetSubSetCombinationDAO oMatchSetSubSetCombinationDAO = new MatchSetSubSetCombinationDAO(oAppUserInfo);
                oMatchSetSubSetCombinationInfoList = oMatchSetSubSetCombinationDAO.GetAllMatchSetSubSetCombination(oMatchingParamInfo);

                if (oMatchSetSubSetCombinationInfoList != null && oMatchSetSubSetCombinationInfoList.Count > 0)
                {
                    // Get Match Set Sub Set Configurations
                    MatchingParamInfo oMatchingParamInfoNew = new MatchingParamInfo();
                    oMatchingParamInfoNew.IDList = (oMatchSetSubSetCombinationInfoList
                                                        .Where(W => W.MatchSetSubSetCombinationID.HasValue))
                                                        .Select(T => T.MatchSetSubSetCombinationID.Value).ToList();
                    List<MatchingConfigurationInfo> oMatchingConfigurationInfoList = GetAllMatchingConfiguration(oMatchingParamInfoNew, oAppUserInfo);

                    // Find and add Configuration List to each Match Set Sub Set
                    foreach (MatchSetSubSetCombinationInfo oMatchSetSubSetCombinationInfo in oMatchSetSubSetCombinationInfoList)
                    {
                        if (oMatchSetSubSetCombinationInfo.MatchingConfigurationInfoList == null)
                            oMatchSetSubSetCombinationInfo.MatchingConfigurationInfoList = new List<MatchingConfigurationInfo>();
                        oMatchSetSubSetCombinationInfo.MatchingConfigurationInfoList.AddRange(oMatchingConfigurationInfoList.FindAll(T => T.MatchSetSubSetCombinationID == oMatchSetSubSetCombinationInfo.MatchSetSubSetCombinationID));
                    }
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
            return oMatchSetSubSetCombinationInfoList;
        }

        /// <summary>
        /// Get Match Set Results
        /// </summary>
        /// <param name="oMatchingParamInfo"></param>
        /// <returns></returns>
        public MatchSetHdrInfo GetMatchSetResults(MatchingParamInfo oMatchingParamInfo, AppUserInfo oAppUserInfo)
        {
            MatchSetHdrInfo oMatchSetHdrInfo = null;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                // Get Match Set Hdr Data
                oMatchSetHdrInfo = GetMatchSetHdr(oMatchingParamInfo, oAppUserInfo);

                // Get Match Set Sub Combination Info
                List<MatchSetSubSetCombinationInfo> oMatchSetSubSetCombinationInfoList = GetAllMatchSetSubSetCombination(oMatchingParamInfo, oAppUserInfo);

                if (oMatchSetSubSetCombinationInfoList != null && oMatchSetSubSetCombinationInfoList.Count > 0)
                {
                    // Get Match Set Results Workspace
                    List<MatchSetResultWorkspaceInfo> oMatchSetResultWorkspaceInfoList = null;
                    MatchSetResultWorkspaceDAO oMatchSetResultWorkspaceDAO = new MatchSetResultWorkspaceDAO(oAppUserInfo);
                    oMatchSetResultWorkspaceInfoList = oMatchSetResultWorkspaceDAO.GetMatchSetResultsWorkspace(oMatchingParamInfo);

                    // Get Rec Item References created using MatchSetSubSetCombinations
                    List<long> IDList = new List<long>();
                    foreach (MatchSetSubSetCombinationInfo oMatchSetSubSetCombinationInfo in oMatchSetSubSetCombinationInfoList)
                    {
                        if (oMatchSetSubSetCombinationInfo.MatchingSourceDataImport1ID.HasValue)
                            IDList.Add(oMatchSetSubSetCombinationInfo.MatchingSourceDataImport1ID.Value);
                        if (oMatchSetSubSetCombinationInfo.MatchingSourceDataImport2ID.HasValue)
                            IDList.Add(oMatchSetSubSetCombinationInfo.MatchingSourceDataImport2ID.Value);
                    }
                    MatchingParamInfo oMatchingParamInfoNew = new MatchingParamInfo();
                    oMatchingParamInfoNew.IDList = IDList;
                    List<MatchSetGLDataRecItemInfo> oMatchSetGLDataRecItemInfoList = GetMatchSetGLDataRecItems(oMatchingParamInfoNew, oAppUserInfo);

                    foreach (MatchSetSubSetCombinationInfo oMatchSetSubSetCombinationInfo in oMatchSetSubSetCombinationInfoList)
                    {
                        // Find and set result Workspace to each Match Set Sub Set Combination
                        if (oMatchSetResultWorkspaceInfoList != null && oMatchSetResultWorkspaceInfoList.Count > 0)
                            oMatchSetSubSetCombinationInfo.MatchSetResultWorkspaceInfo = oMatchSetResultWorkspaceInfoList.Find(T => T.MatchSetSubSetCombinationID == oMatchSetSubSetCombinationInfo.MatchSetSubSetCombinationID);
                        // Find and Add Rec Items Created using this match set sub set combination
                        // Source 1
                        if (oMatchSetGLDataRecItemInfoList != null && oMatchSetGLDataRecItemInfoList.Count > 0)
                            oMatchSetSubSetCombinationInfo.GLDataRecItemInfoListSource1 = oMatchSetGLDataRecItemInfoList.FindAll(T => T.MatchingSourceDataImportID == oMatchSetSubSetCombinationInfo.MatchingSourceDataImport1ID);
                        // Source 2
                        if (oMatchSetGLDataRecItemInfoList != null && oMatchSetGLDataRecItemInfoList.Count > 0)
                            oMatchSetSubSetCombinationInfo.GLDataRecItemInfoListSource2 = oMatchSetGLDataRecItemInfoList.FindAll(T => T.MatchingSourceDataImportID == oMatchSetSubSetCombinationInfo.MatchingSourceDataImport2ID);
                    }
                }
                if (oMatchSetHdrInfo != null)
                    oMatchSetHdrInfo.MatchSetSubSetCombinationInfoCollection = oMatchSetSubSetCombinationInfoList;
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return oMatchSetHdrInfo;
        }

        /// <summary>
        /// Get GL Data Rec Item References by Match Set Sub Set Combinations
        /// </summary>
        /// <param name="oMatchingParamInfo"></param>
        /// <returns></returns>
        public List<MatchSetGLDataRecItemInfo> GetMatchSetGLDataRecItems(MatchingParamInfo oMatchingParamInfo, AppUserInfo oAppUserInfo)
        {
            List<MatchSetGLDataRecItemInfo> oMatchSetGLDataRecItemInfoList = null;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                // Get Rec Item References created using MatchSetSubSetCombinations
                MatchSetGLDataRecItemDAO oMatchSetGLDataRecItemDAO = new MatchSetGLDataRecItemDAO(oAppUserInfo);
                oMatchSetGLDataRecItemInfoList = oMatchSetGLDataRecItemDAO.GetMatchSetGLDataRecItems(oMatchingParamInfo.IDList);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return oMatchSetGLDataRecItemInfoList;
        }

        public int UpdateMatchingConfigurationDisplayedColumn(MatchingParamInfo oMatchingParamInfo, AppUserInfo oAppUserInfo)
        {
            int recordsAffected = 0;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                MatchingConfigurationDAO oMatchingConfigurationDAO = new MatchingConfigurationDAO(oAppUserInfo);
                recordsAffected = oMatchingConfigurationDAO.UpdateMatchingConfigurationDisplayedColumn(oMatchingParamInfo);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return recordsAffected;

        }


        /// <summary>
        /// Saves RecItem Column Mapping in the database
        /// </summary>
        /// <param name="oMatchingParamInfo">List of MatchingConfigurationInfo </param>
        /// <returns>Bool value IsSaved or not</returns>
        public bool SaveRecItemColumnMapping(MatchingParamInfo oMatchingParamInfo, AppUserInfo oAppUserInfo)
        {
            bool IsSaved = false;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                RecItemColumnMstDAO oRecItemColumnMstDAO = new RecItemColumnMstDAO(oAppUserInfo);
                DataTable dtRecItemColumnMappingDataTable = ServiceHelper.ConvertMatchingParamInfoTORecItemColumnMappingDataTable(oMatchingParamInfo.oMatchingConfigurationInfoList, false);
                IsSaved = oRecItemColumnMstDAO.SaveRecItemColumnMapping(dtRecItemColumnMappingDataTable, oMatchingParamInfo.AddedBy, oMatchingParamInfo.DateAdded, oMatchingParamInfo.RevisedBy, oMatchingParamInfo.DateRevised);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return IsSaved;
        }


        /// <summary>
        /// Clean RecItem Column Mapping in the database
        /// </summary>
        /// <param name="oMatchingParamInfo">List of MatchingConfigurationInfo </param>
        /// <returns>Bool value IsSaved or not</returns>
        public bool CleanRecItemColumnMapping(MatchingParamInfo oMatchingParamInfo, AppUserInfo oAppUserInfo)
        {
            bool IsSaved = false;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                RecItemColumnMstDAO oRecItemColumnMstDAO = new RecItemColumnMstDAO(oAppUserInfo);
                DataTable dtRecItemColumnMappingDataTable = ServiceHelper.ConvertMatchingParamInfoTORecItemColumnMappingDataTable(oMatchingParamInfo.oMatchingConfigurationInfoList, true);
                IsSaved = oRecItemColumnMstDAO.CleanRecItemColumnMapping(dtRecItemColumnMappingDataTable, oMatchingParamInfo.RevisedBy, oMatchingParamInfo.DateRevised);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return IsSaved;
        }

        /// <summary>
        /// Save RecItem Column Mapping in the database
        /// </summary>
        /// <param name="oMatchingParamInfo">List of MatchingConfigurationInfo </param>
        /// <returns>Saved  List of MatchingConfigurationInfo </returns>
        public int SaveMatchingConfiguration(MatchingParamInfo oMatchingParamInfo, AppUserInfo oAppUserInfo)
        {
            int recordsAffected = 0;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                //*** Save Matching Configaration and get insert list
                MatchingConfigurationDAO oMatchingConfigurationDAO = new MatchingConfigurationDAO(oAppUserInfo);
                DataTable dtMatchingConfigurationRule;
                DataTable dtMatchingConfiguration = ServiceHelper.ConvertMatchingConfigurationToDataTable(oMatchingParamInfo.oMatchingConfigurationInfoList, true, out dtMatchingConfigurationRule);
                recordsAffected = oMatchingConfigurationDAO.SaveMatchingConfiguration(dtMatchingConfiguration, dtMatchingConfigurationRule, oMatchingParamInfo);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return recordsAffected;
        }

        /// <summary>
        /// Update Matching Source Data Import Hidden Status in the database
        /// </summary>
        /// <param name="oMatchingParamInfo"></param>
        /// <returns>true/false</returns>
        public bool UpdateMatchingSourceDataImportHiddenStatus(MatchingParamInfo oMatchingParamInfo, AppUserInfo oAppUserInfo)
        {
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                MatchingSourceDataImportHdrDAO oMatchingSourceDataImportHdrDAO = new MatchingSourceDataImportHdrDAO(oAppUserInfo);
                return oMatchingSourceDataImportHdrDAO.UpdateMatchingSourceDataImportHiddenStatus(oMatchingParamInfo);
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

        /// <summary>
        /// Update Match Set Sub Set Combination Is Configuration Complete in the database
        /// </summary>
        /// <param name="oMatchingParamInfo"></param>
        /// <returns>true/false</returns>
        public bool UpdateMatchSetSubSetCombinationForConfigStatus(MatchingParamInfo oMatchingParamInfo, AppUserInfo oAppUserInfo)
        {
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                MatchSetSubSetCombinationDAO oMatchSetSubSetCombinationDAO = new MatchSetSubSetCombinationDAO(oAppUserInfo);
                return oMatchSetSubSetCombinationDAO.UpdateMatchSetSubSetCombinationForConfigStatus(oMatchingParamInfo);
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

        public bool UpdateMatchSetResults(MatchingParamInfo oMatchingParamInfo, AppUserInfo oAppUserInfo)
        {
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                MatchSetResultWorkspaceDAO oMatchSetResultWorkspaceDAO = new MatchSetResultWorkspaceDAO(oAppUserInfo);
                return oMatchSetResultWorkspaceDAO.UpdateMatchSetResults(oMatchingParamInfo);
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
        public MatchSetHdrInfo GetMatchSetStatusMessage(MatchingParamInfo oMatchingParamInfo, AppUserInfo oAppUserInfo)
        {
            MatchSetHdrInfo oMatchSetHdrInfo = null;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                // Get Match Set Hdr Data
                oMatchSetHdrInfo = GetMatchSetHdr(oMatchingParamInfo, oAppUserInfo);
                return oMatchSetHdrInfo;
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }

            return oMatchSetHdrInfo;
        }

        /// <summary>
        /// Get Match Set Sub Set CombinationsInfo for NetAmount Calculation
        /// </summary>
        /// <param name="oMatchingParamInfo"></param>
        /// <returns></returns>
        public MatchSetSubSetCombinationInfoForNetAmountCalculation GetMatchSetSubSetCombinationForNetAmountCalculationByID(Int64? MatchSetSubSetCombinationId, AppUserInfo oAppUserInfo)
        {
            MatchSetSubSetCombinationInfoForNetAmountCalculation oMatchSetSubSetCombinationInfoForNetAmountCalculation = null;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                // Get Match Set Sub Set Combinations
                MatchSetSubSetCombinationDAO oMatchSetSubSetCombinationDAO = new MatchSetSubSetCombinationDAO(oAppUserInfo);
                oMatchSetSubSetCombinationInfoForNetAmountCalculation = oMatchSetSubSetCombinationDAO.GetMatchSetSubSetCombinationForNetAmountCalculationByID(MatchSetSubSetCombinationId);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return oMatchSetSubSetCombinationInfoForNetAmountCalculation;
        }

    }
}
