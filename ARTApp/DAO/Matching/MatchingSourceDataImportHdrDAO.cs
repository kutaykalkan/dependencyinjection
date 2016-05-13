using System;
using SkyStem.ART.App.DAO.Matching.Base;
using SkyStem.ART.Client.Model.Matching;
using System.Collections.Generic;
using System.Data;
using SkyStem.ART.Client.Params.Matching;
using SkyStem.ART.App.DAO.Base;
using SkyStem.ART.App.Utility;
using SkyStem.ART.Client.Model;


namespace SkyStem.ART.App.DAO.Matching
{
    public class MatchingSourceDataImportHdrDAO : MatchingSourceDataImportHdrDAOBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public MatchingSourceDataImportHdrDAO(AppUserInfo oAppUserInfo) :
            base(oAppUserInfo)
        {
        }
        #region GetMatchingSourceData
        /// <summary>
        /// Get Matching Source Data Import info in the database
        /// </summary>
        /// <param name="oMatchingParamInfo">MatchingParamInfo - RecPeriodId+UserID+RoleID</param>
        /// <returns>List of MatchingSourceDataImportHdrInfo</returns>
        public List<MatchingSourceDataImportHdrInfo> GetMatchingSources(MatchingParamInfo oMatchingParamInfo)
        {
            List<MatchingSourceDataImportHdrInfo> oMatchingSourceDataImportHdrInfoCollection = new List<MatchingSourceDataImportHdrInfo>();
            IDbCommand cmd = null;
            IDbConnection con = null;
            try
            {
                con = this.CreateConnection();
                cmd = this.CreateCommand("Matching.usp_SEL_MatchingSourceDataImport");

                cmd.CommandType = CommandType.StoredProcedure;

                IDataParameterCollection cmdParams = cmd.Parameters;

                IDbDataParameter paramRecPeriodID = cmd.CreateParameter();
                paramRecPeriodID.ParameterName = "@RecPeriodID";
                paramRecPeriodID.Value = oMatchingParamInfo.RecPeriodID;
                cmdParams.Add(paramRecPeriodID);

                IDbDataParameter paramRoleID = cmd.CreateParameter();
                paramRoleID.ParameterName = "@RoleID";
                paramRoleID.Value = oMatchingParamInfo.RoleID;
                cmdParams.Add(paramRoleID);

                IDbDataParameter paramUserID = cmd.CreateParameter();
                paramUserID.ParameterName = "@UserID";
                paramUserID.Value = oMatchingParamInfo.UserID;
                cmdParams.Add(paramUserID);


                IDbDataParameter paramShowOnlySuccessfulMatchingSourceDataImport = cmd.CreateParameter();
                paramShowOnlySuccessfulMatchingSourceDataImport.ParameterName = "@ShowOnlySuccessfulMatchingSourceDataImport";
                paramShowOnlySuccessfulMatchingSourceDataImport.Value = oMatchingParamInfo.ShowOnlySuccessfulMatchingSourceDataImport;
                cmdParams.Add(paramShowOnlySuccessfulMatchingSourceDataImport);

                IDbDataParameter paramShowHidden = cmd.CreateParameter();
                paramShowHidden.ParameterName = "@ShowHidden";
                if (oMatchingParamInfo.IsHidden.HasValue)
                    paramShowHidden.Value = oMatchingParamInfo.IsHidden;
                else
                    paramShowHidden.Value = 1;
                cmdParams.Add(paramShowHidden);


                cmd.Connection = con;
                con.Open();
                IDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                MatchingSourceDataImportHdrInfo oMatchingSourceDataImportHdrInfo = null;
                while (reader.Read())
                {
                    oMatchingSourceDataImportHdrInfo = this.MapObject(reader);
                    MapObjectWithMatchingStatusInfo(reader, oMatchingSourceDataImportHdrInfo);
                    oMatchingSourceDataImportHdrInfoCollection.Add(oMatchingSourceDataImportHdrInfo);
                }
                reader.Close();

            }

            finally
            {
                if ((con != null) && (con.State == ConnectionState.Open))
                {
                    con.Dispose();
                }
            }

            return oMatchingSourceDataImportHdrInfoCollection;
        }

        /// <summary>
        /// Get Match Set Sources for Matching Wizard
        /// </summary>
        /// <param name="oMatchingParamInfo"></param>
        /// <returns></returns>
        public List<MatchingSourceDataImportHdrInfo> GetAllMatchSetMatchingSources(MatchingParamInfo oMatchingParamInfo)
        {
            List<MatchingSourceDataImportHdrInfo> oMatchingSourceDataImportHdrInfoList = new List<MatchingSourceDataImportHdrInfo>();
            IDbCommand cmd = null;
            IDbConnection con = null;
            try
            {
                con = this.CreateConnection();
                cmd = CreateGetAllMatchSetMatchingSourcesCommand(oMatchingParamInfo, cmd);

                cmd.Connection = con;
                con.Open();
                IDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                MatchingSourceDataImportHdrInfo oMatchingSourceDataImportHdrInfo = null;
                while (reader.Read())
                {
                    oMatchingSourceDataImportHdrInfo = this.MapObject(reader);
                    MapObjectWithMatchingStatusInfo(reader, oMatchingSourceDataImportHdrInfo);
                    oMatchingSourceDataImportHdrInfoList.Add(oMatchingSourceDataImportHdrInfo);
                }
                reader.Close();
            }

            finally
            {
                if ((con != null) && (con.State == ConnectionState.Open))
                {
                    con.Dispose();
                }
            }
            return oMatchingSourceDataImportHdrInfoList;
        }

        private IDbCommand CreateGetAllMatchSetMatchingSourcesCommand(MatchingParamInfo oMatchingParamInfo, IDbCommand cmd)
        {
            cmd = this.CreateCommand("Matching.usp_SEL_AllMatchSetMatchingSources");

            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            IDbDataParameter paramMatchingTypeID = cmd.CreateParameter();
            paramMatchingTypeID.ParameterName = "@MatchingTypeID";
            paramMatchingTypeID.Value = oMatchingParamInfo.MatchingTypeID;
            cmdParams.Add(paramMatchingTypeID);

            IDbDataParameter paramRecPeriodID = cmd.CreateParameter();
            paramRecPeriodID.ParameterName = "@RecPeriodID";
            paramRecPeriodID.Value = oMatchingParamInfo.RecPeriodID;
            cmdParams.Add(paramRecPeriodID);

            IDbDataParameter paramRoleID = cmd.CreateParameter();
            paramRoleID.ParameterName = "@RoleID";
            paramRoleID.Value = oMatchingParamInfo.RoleID;
            cmdParams.Add(paramRoleID);

            IDbDataParameter paramUserID = cmd.CreateParameter();
            paramUserID.ParameterName = "@UserID";
            paramUserID.Value = oMatchingParamInfo.UserID;
            cmdParams.Add(paramUserID);

            if (oMatchingParamInfo.AccountID.HasValue)
            {
                IDbDataParameter paramAccountID = cmd.CreateParameter();
                paramAccountID.ParameterName = "@AccountID";
                paramAccountID.Value = oMatchingParamInfo.AccountID;
                cmdParams.Add(paramAccountID);
            }

            if (oMatchingParamInfo.MatchSetID.HasValue)
            {
                IDbDataParameter paramMatchSetID = cmd.CreateParameter();
                paramMatchSetID.ParameterName = "@MatchSetID";
                paramMatchSetID.Value = oMatchingParamInfo.MatchSetID;
                cmdParams.Add(paramMatchSetID);
            }
            return cmd;
        }

        #endregion
        public void MapObjectWithMatchingStatusInfo(System.Data.IDataReader r, MatchingSourceDataImportHdrInfo oMatchingSourceDataImportHdrInfo)
        {
            oMatchingSourceDataImportHdrInfo.MatchingSourceTypeName = r.GetStringValue("MatchingSourceTypeName");
            oMatchingSourceDataImportHdrInfo.MatchingSourceTypeNameLabelID = r.GetInt32Value("MatchingSourceTypeNameLabelID");
            oMatchingSourceDataImportHdrInfo.DataImportTypeLabelID = r.GetInt32Value("DataImportTypeLabelID");
            oMatchingSourceDataImportHdrInfo.DataImportStatus = r.GetStringValue("DataImportStatus");
            oMatchingSourceDataImportHdrInfo.DataImportStatusLabelID = r.GetInt32Value("DataImportStatusLabelID");
            oMatchingSourceDataImportHdrInfo.MatchSetID = r.GetInt64Value("MatchSetID");
            oMatchingSourceDataImportHdrInfo.IsPartofMatchSetLabelID = r.GetInt32Value("IsPartofMatchSetLabelID");
            oMatchingSourceDataImportHdrInfo.IsHidden = r.GetBooleanValue("IsHidden");

            oMatchingSourceDataImportHdrInfo.RecItemCreatedCount = r.GetInt32Value("RecItemCreatedCount");
            oMatchingSourceDataImportHdrInfo.RecordsImported = r.GetInt32Value("RecordsImported");
            oMatchingSourceDataImportHdrInfo.RecordsLeft = r.GetInt32Value("RecordsLeft");
            //if (oMatchingSourceDataImportHdrInfo.RecordsImported != null && oMatchingSourceDataImportHdrInfo.RecItemCreatedCount != null)
            //{
            //    oMatchingSourceDataImportHdrInfo.RecordsLeft = oMatchingSourceDataImportHdrInfo.RecordsImported - oMatchingSourceDataImportHdrInfo.RecItemCreatedCount;
            //}
        }

        #region SaveMatchingSource
        /// <summary>
        /// Saves Matching Source data in the database
        /// </summary>
        /// <param name="dtMatchingSourceDataTableType">User defined table type</param>
        /// <returns></returns>
        public List<MatchingSourceDataImportHdrInfo> SaveMatchingSource(DataTable dtMatchingSourceDataTableType, int comapnyId)
        {
            List<MatchingSourceDataImportHdrInfo> oMatchingSourceDataImportHdrInfoCollection = new List<MatchingSourceDataImportHdrInfo>();
            IDbCommand cmd = null;
            IDbConnection con = null;
            IDbTransaction oTransaction = null;
            try
            {
                con = this.CreateConnection();
                cmd = this.CreateCommand("Matching.usp_INS_MatchingSourceDataImportHdr");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;

                IDataParameterCollection cmdParams = cmd.Parameters;

                IDbDataParameter cmdMatchingSourceDataTable = cmd.CreateParameter();
                cmdMatchingSourceDataTable.ParameterName = "@dtMatchingSourceData";
                cmdMatchingSourceDataTable.Value = dtMatchingSourceDataTableType;
                cmdParams.Add(cmdMatchingSourceDataTable);

                IDbDataParameter cmdCompanyID = cmd.CreateParameter();
                cmdCompanyID.ParameterName = "@CompanyID";
                cmdCompanyID.Value = comapnyId;
                cmdParams.Add(cmdCompanyID);
                con.Open();
                oMatchingSourceDataImportHdrInfoCollection = this.Select(cmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (oTransaction != null && oTransaction.Connection != null)
                {
                    oTransaction.Rollback();
                    oTransaction.Dispose();
                }

                if ((con != null) && (con.State == ConnectionState.Open))
                {
                    con.Close();
                    con.Dispose();
                }
            }

            return oMatchingSourceDataImportHdrInfoCollection;
        }

        #endregion

        #region Update Matching Source Data Import Status
        /// <summary>
        /// Update Matching Source Data Import Status
        /// </summary>
        /// <param name="dtMatchingSourceDataTableType">User defined table type</param>
        /// <returns></returns>
        public bool UpdateMatchingSourceDataImportStatus(DataTable dtMatchingSourceDataImportIDs, Int16 DataImportStatusID, IDbConnection con, IDbTransaction oTransaction)
        {
            bool result = false;
            IDbCommand cmd = null;
            try
            {
                cmd = this.CreateCommand("Matching.usp_UDP_MatchingDataSourceStatus");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;
                cmd.Transaction = oTransaction;

                IDataParameterCollection cmdParams = cmd.Parameters;

                IDbDataParameter cmdMatchingSourceDataImportIDDataTable = cmd.CreateParameter();
                cmdMatchingSourceDataImportIDDataTable.ParameterName = "@dtMatchingSourceDataImportID";
                cmdMatchingSourceDataImportIDDataTable.Value = dtMatchingSourceDataImportIDs;
                cmdParams.Add(cmdMatchingSourceDataImportIDDataTable);

                IDbDataParameter cmdDataImportStatusID = cmd.CreateParameter();
                cmdDataImportStatusID.ParameterName = "@DataImportStatusID";
                cmdDataImportStatusID.Value = DataImportStatusID;
                cmdParams.Add(cmdDataImportStatusID);

                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                    result = true;

            }
            finally
            { }
            return result;
        }

        #endregion

        #region Delete Matching Source Data Import
        /// <summary>
        /// Delete Matching Source Data Import
        /// </summary>
        /// <param name="dtMatchingSourceDataTableType">User defined table type</param>
        /// <returns></returns>
        public bool DeleteMatchingSourceData(DataTable dtMatchingSourceDataImportIDs, MatchingParamInfo oMatchingParamInfo)
        {
            bool result = false;
            IDbConnection con = null;
            IDbTransaction oTransaction = null;
            IDbCommand cmd = null;
            try
            {
                con = this.CreateConnection();
                con.Open();
                oTransaction = con.BeginTransaction();

                cmd = this.CreateCommand("Matching.usp_DEL_MatchingSourceDataImport");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;
                cmd.Transaction = oTransaction;

                IDataParameterCollection cmdParams = cmd.Parameters;

                IDbDataParameter cmdMatchingSourceDataImportIDDataTable = cmd.CreateParameter();
                cmdMatchingSourceDataImportIDDataTable.ParameterName = "@dtMatchingSourceDataImportID";
                cmdMatchingSourceDataImportIDDataTable.Value = dtMatchingSourceDataImportIDs;
                cmdParams.Add(cmdMatchingSourceDataImportIDDataTable);

                IDbDataParameter cmdCompanyID = cmd.CreateParameter();
                cmdCompanyID.ParameterName = "@CompanyID";
                cmdCompanyID.Value = oMatchingParamInfo.CompanyID;
                cmdParams.Add(cmdCompanyID);

                IDbDataParameter cmdDateRevised = cmd.CreateParameter();
                cmdDateRevised.ParameterName = "@DateRevised";
                cmdDateRevised.Value = oMatchingParamInfo.DateRevised;
                cmdParams.Add(cmdDateRevised);

                IDbDataParameter cmdRevisedBy = cmd.CreateParameter();
                cmdRevisedBy.ParameterName = "@RevisedBy";
                cmdRevisedBy.Value = oMatchingParamInfo.RevisedBy;
                cmdParams.Add(cmdRevisedBy);


                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                    result = true;

                oTransaction.Commit();
                oTransaction.Dispose();
            }
            catch (Exception ex)
            {
                if (oTransaction != null && oTransaction.Connection != null)
                {
                    oTransaction.Rollback();
                    oTransaction.Dispose();
                }
                ServiceHelper.LogAndThrowGenericException(ex, this.CurrentAppUserInfo);
            }
            finally
            {
                if (oTransaction != null && oTransaction.Connection != null)
                {
                    oTransaction.Rollback();
                    oTransaction.Dispose();
                }

                if ((con != null) && (con.State == ConnectionState.Open))
                {
                    con.Close();
                    con.Dispose();
                }

            }
            return result;
        }
        #endregion

        #region "GetDataImportInfo"
        public MatchingSourceDataImportHdrInfo GetMatchingSourceDataImportInfo(MatchingParamInfo oMatchingParamInfo)
        {
            MatchingSourceDataImportHdrInfo oMatchingSourceDataImportHdrInfo = null;
            IDbCommand cmd = null;
            IDbConnection con = null;
            IDataReader reader = null;
            try
            {
                con = this.CreateConnection();
                cmd = this.CreateCommand("Matching.usp_GET_MatchingSourceDataImportInfo");

                cmd.CommandType = CommandType.StoredProcedure;

                IDataParameterCollection cmdParams = cmd.Parameters;

                IDbDataParameter paramMatchingSourceDataImportID = cmd.CreateParameter();
                paramMatchingSourceDataImportID.ParameterName = "@MatchingSourceDataImportID";
                paramMatchingSourceDataImportID.Value = oMatchingParamInfo.MatchingSourceDataImportID;
                cmdParams.Add(paramMatchingSourceDataImportID);

                cmd.Connection = con;
                con.Open();
                reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                reader.Read();
                oMatchingSourceDataImportHdrInfo = this.MapObject(reader);
                MapObjectWithMatchingStatusInfo(reader, oMatchingSourceDataImportHdrInfo);
                reader.ClearColumnHash();
            }
            finally
            {
                if (reader != null && !reader.IsClosed)
                {
                    reader.Close();
                }
                if ((con != null) && (con.State == ConnectionState.Open))
                {
                    con.Dispose();
                }

            }
            return oMatchingSourceDataImportHdrInfo;
        }


        public List<MatchingSourceDataImportHdrInfo> GetAllMatchingSourceDataImportInfoByMatchSetID(MatchingParamInfo oMatchingParamInfo)
        {
            List<MatchingSourceDataImportHdrInfo> oMatchingSourceDataImportHdrInfoCollection = new List<MatchingSourceDataImportHdrInfo>();
            MatchingSourceDataImportHdrInfo oMatchingSourceDataImportHdrInfo = null;
            IDbCommand cmd = null;
            IDbConnection con = null;
            IDataReader reader = null;
            try
            {
                con = this.CreateConnection();
                cmd = this.CreateCommand("Matching.usp_SEL_MatchingSourceDataImportInfoByMatchSetID");

                cmd.CommandType = CommandType.StoredProcedure;

                IDataParameterCollection cmdParams = cmd.Parameters;

                IDbDataParameter paramMatchSetID = cmd.CreateParameter();
                paramMatchSetID.ParameterName = "@MatchSetID";
                paramMatchSetID.Value = oMatchingParamInfo.MatchSetID;
                cmdParams.Add(paramMatchSetID);


                cmd.Connection = con;
                con.Open();
                reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (reader.Read())
                {
                    oMatchingSourceDataImportHdrInfo = new MatchingSourceDataImportHdrInfo();
                    oMatchingSourceDataImportHdrInfo = this.MapObject(reader);
                    MapObjectWithMatchingStatusInfo(reader, oMatchingSourceDataImportHdrInfo);
                    oMatchingSourceDataImportHdrInfoCollection.Add(oMatchingSourceDataImportHdrInfo);
                }

            }
            finally
            {
                if (reader != null && !reader.IsClosed)
                {
                    reader.Close();
                }
                if ((con != null) && (con.State == ConnectionState.Open))
                {
                    con.Dispose();
                }

            }
            return oMatchingSourceDataImportHdrInfoCollection;
        }



        #endregion

        #region Update Matching Source Data Import For Force Commit
        /// <summary>
        /// Update Matching Source Data Import For Force Commit
        /// </summary>
        /// <param name="dtMatchingSourceDataTableType">User defined table type</param>
        /// <returns></returns>
        public bool UpdateMatchingSourceDataImportForForceCommit(MatchingParamInfo oMatchingParamInfo)
        {
            bool result = false;
            IDbConnection con = null;
            IDbTransaction oTransaction = null;
            IDbCommand cmd = null;
            try
            {
                con = this.CreateConnection();
                con.Open();
                oTransaction = con.BeginTransaction();

                cmd = this.CreateCommand("Matching.usp_UPD_MatchingSourceDataImportForForceCommit");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;
                cmd.Transaction = oTransaction;

                IDataParameterCollection cmdParams = cmd.Parameters;

                IDbDataParameter cmdMatchingSourceDataImportID = cmd.CreateParameter();
                cmdMatchingSourceDataImportID.ParameterName = "@MatchingSourceDataImportID";
                cmdMatchingSourceDataImportID.Value = oMatchingParamInfo.MatchingSourceDataImportID;
                cmdParams.Add(cmdMatchingSourceDataImportID);

                IDbDataParameter cmdDataImportStatusID = cmd.CreateParameter();
                cmdDataImportStatusID.ParameterName = "@DataImportStatusID";
                cmdDataImportStatusID.Value = oMatchingParamInfo.DataImportStatusID;
                cmdParams.Add(cmdDataImportStatusID);

                IDbDataParameter cmdIsForceCommit = cmd.CreateParameter();
                cmdIsForceCommit.ParameterName = "@IsForceCommit";
                cmdIsForceCommit.Value = oMatchingParamInfo.IsForceCommit;
                cmdParams.Add(cmdIsForceCommit);

                IDbDataParameter cmdForceCommitDate = cmd.CreateParameter();
                cmdForceCommitDate.ParameterName = "@ForceCommitDate";
                cmdForceCommitDate.Value = oMatchingParamInfo.ForceCommitDate;
                cmdParams.Add(cmdForceCommitDate);

                IDbDataParameter cmdDateRevised = cmd.CreateParameter();
                cmdDateRevised.ParameterName = "@DateRevised";
                cmdDateRevised.Value = oMatchingParamInfo.DateRevised;
                cmdParams.Add(cmdDateRevised);

                IDbDataParameter cmdRevisedBy = cmd.CreateParameter();
                cmdRevisedBy.ParameterName = "@RevisedBy";
                cmdRevisedBy.Value = oMatchingParamInfo.RevisedBy;
                cmdParams.Add(cmdRevisedBy);

                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                    result = true;

                oTransaction.Commit();
                oTransaction.Dispose();
            }
            catch (Exception ex)
            {
                if (oTransaction != null && oTransaction.Connection != null)
                {
                    oTransaction.Rollback();
                    oTransaction.Dispose();
                }

                ServiceHelper.LogAndThrowGenericException(ex, this.CurrentAppUserInfo);
            }
            finally
            {
                if (oTransaction != null && oTransaction.Connection != null)
                {
                    oTransaction.Rollback();
                    oTransaction.Dispose();
                }

                if ((con != null) && (con.State == ConnectionState.Open))
                {
                    con.Close();
                    con.Dispose();
                }
            }
            return result;
        }

        #endregion

        #region "KeyFieldsByCompanyID"
        public string GetKeyFieldsByCompanyID(MatchingParamInfo oMatchingParamInfo)
        {
            IDbCommand cmd = null;
            IDbConnection con = null;
            IDataReader reader = null;
            try
            {
                con = this.CreateConnection();
                cmd = this.CreateCommand("usp_GET_AllKeyFieldsByCompanyID");

                cmd.CommandType = CommandType.StoredProcedure;

                IDataParameterCollection cmdParams = cmd.Parameters;

                IDbDataParameter paramCompanyID = cmd.CreateParameter();
                paramCompanyID.ParameterName = "@CompanyID";
                paramCompanyID.Value = oMatchingParamInfo.CompanyID;
                cmdParams.Add(paramCompanyID);

                cmd.Connection = con;
                con.Open();
                reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                reader.Read();
                return reader.GetStringValue("KeyFields");
            }
            finally
            {
                if (reader != null && !reader.IsClosed)
                {
                    reader.Close();
                }
                if ((con != null) && (con.State == ConnectionState.Open))
                {
                    con.Dispose();
                }
            }
        }
        #endregion

        #region Update Matching Source Data Import Hidden Status
        /// <summary>
        /// Update Matching Source Data Import For Force Commit
        /// </summary>
        /// <param name="oMatchingParamInfo">MatchingSourceDataImportID and IsHidden=true/false </param>
        /// <returns></returns>
        public bool UpdateMatchingSourceDataImportHiddenStatus(MatchingParamInfo oMatchingParamInfo)
        {
            bool result = false;
            IDbConnection con = null;
            IDbTransaction oTransaction = null;
            IDbCommand cmd = null;
            try
            {
                con = this.CreateConnection();
                con.Open();
                oTransaction = con.BeginTransaction();

                cmd = this.CreateCommand("Matching.usp_UPD_MatchingSourceDataImportHdrForHiddenStatus");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;
                cmd.Transaction = oTransaction;

                IDataParameterCollection cmdParams = cmd.Parameters;

                IDbDataParameter cmdMatchingSourceDataImportID = cmd.CreateParameter();
                cmdMatchingSourceDataImportID.ParameterName = "@MatchingSourceDataImportID";
                cmdMatchingSourceDataImportID.Value = oMatchingParamInfo.MatchingSourceDataImportID;
                cmdParams.Add(cmdMatchingSourceDataImportID);

                IDbDataParameter cmdIsHidden = cmd.CreateParameter();
                cmdIsHidden.ParameterName = "@IsHidden";
                cmdIsHidden.Value = oMatchingParamInfo.IsHidden;
                cmdParams.Add(cmdIsHidden);


                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                    result = true;

                oTransaction.Commit();
                oTransaction.Dispose();
            }
            catch (Exception ex)
            {
                if (oTransaction != null && oTransaction.Connection != null)
                {
                    oTransaction.Rollback();
                    oTransaction.Dispose();
                }

                ServiceHelper.LogAndThrowGenericException(ex, this.CurrentAppUserInfo);
            }
            finally
            {
                if (oTransaction != null && oTransaction.Connection != null)
                {
                    oTransaction.Rollback();
                    oTransaction.Dispose();
                }

                if ((con != null) && (con.State == ConnectionState.Open))
                {
                    con.Close();
                    con.Dispose();
                }
            }
            return result;
        }
        #endregion
    }
}