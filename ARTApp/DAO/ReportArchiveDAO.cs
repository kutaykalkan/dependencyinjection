


using System;
using System.Data;
using System.Data.Common;
using Adapdev.Data;
using Adapdev.Data.Sql;
using SkyStem.ART.App.DAO.Base;
using SkyStem.ART.Client.Model;
using System.Collections.Generic;
using System.Data.SqlClient;
namespace SkyStem.ART.App.DAO
{
    public class ReportArchiveDAO : ReportArchiveDAOBase
    {
                /// <summary>
        /// Constructor
        /// </summary>
        public ReportArchiveDAO(AppUserInfo oAppUserInfo) :
            base(oAppUserInfo)
        {
        }
        public List<ReportArchiveInfo> GetRptActivityByReportIDUserIDRoleIDRecPeriodID(short reportID
            , int userID, short roleID, int recPeriodID, int languageID, int defaultLanguageID, int companyID)
        {
            IDbCommand oCommand = null;
            IDbConnection oConnection = null;
            List<ReportArchiveInfo> oRptArchiveInfoCollection;
            IDataReader reader = null;
            try
            {
                oCommand = this.GetRptActivityCommand(reportID, userID, roleID
                    , recPeriodID, languageID, defaultLanguageID, companyID);
                oConnection = this.CreateConnection();

                oCommand.Connection = oConnection;
                oConnection.Open();
                reader = oCommand.ExecuteReader(CommandBehavior.CloseConnection);
                oRptArchiveInfoCollection = this.MapObjects(reader);
                reader.Close();


                //Get Parameters
                ReportArchiveParameterDAO oRptArchiveParamDAO = new ReportArchiveParameterDAO(this.CurrentAppUserInfo);
                List<ReportArchiveParameterInfo> oRptArchiveParamsCollection = oRptArchiveParamDAO.GetArchiveRptParamsByReportIDUserIDRoleIDRecPeriodID(reportID, userID, roleID
                    , recPeriodID, languageID, defaultLanguageID, companyID);

                //Fill ReportArchiveInfo with respective Params
                foreach (ReportArchiveInfo oRptArchiveInfo in oRptArchiveInfoCollection)
                {
                    long RptArchiveID = oRptArchiveInfo.ReportArchiveID.Value;
                    List<ReportArchiveParameterInfo> oRptArchParams = oRptArchiveParamsCollection.FindAll(r => r.ReportArchiveID.Value == RptArchiveID);
                    oRptArchiveInfo.ReportArchiveParameterByRptArchiveID = oRptArchParams;
                    oRptArchParams = null;
                }
            }
            finally
            {
                if (reader != null && !reader.IsClosed)
                    reader.Close();
                if ((oConnection != null) && (oConnection.State != ConnectionState.Closed))
                {
                    oConnection.Dispose();
                }
            }
            return oRptArchiveInfoCollection;

        }

        public ReportArchiveInfo GetArchiveReportByReportArchiveID(int reportArchiveID)
        {
            IDbCommand oCommand = null;

            IDbConnection oConnection = null;
            ReportArchiveInfo oRptArchiveInfo;
            IDataReader reader = null;
            //SqlDataReader reader = null;

            try
            {
                oCommand = this.GetArchivedReportCommand(reportArchiveID);
                oConnection = this.CreateConnection();

                oCommand.Connection = oConnection;
                oConnection.Open();
                reader = oCommand.ExecuteReader(CommandBehavior.SequentialAccess);
                reader.Read();
                oRptArchiveInfo = this.MapMyObject(reader);
                reader.Close();
            }
            finally
            {
                if (reader != null && !reader.IsClosed)
                    reader.Close();
                if ((oConnection != null) && (oConnection.State != ConnectionState.Closed))
                {
                    oConnection.Dispose();
                }
            }
            return oRptArchiveInfo;
        }

        #region "Get Command Methods"
        protected IDbCommand GetRptActivityCommand(short reportID, int userID, short roleID
            , int recPeriodID, int languageID, int defaultLanguageID, int companyID)
        {
            IDbCommand oIDBCommand = this.CreateCommand("usp_GET_ReportActivityByReportIDUserIDRoleIDRecPeriodID");
            oIDBCommand.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = oIDBCommand.Parameters;

            IDbDataParameter cmdParamReportID = oIDBCommand.CreateParameter();
            cmdParamReportID.ParameterName = "@ReportID";
            cmdParamReportID.Value = reportID;

            IDbDataParameter cmdParamUserID = oIDBCommand.CreateParameter();
            cmdParamUserID.ParameterName = "@UserID";
            cmdParamUserID.Value = userID;

            IDbDataParameter cmdParamRoleID = oIDBCommand.CreateParameter();
            cmdParamRoleID.ParameterName = "@RoleID";
            cmdParamRoleID.Value = roleID;

            IDbDataParameter cmdParamRecPeriodID = oIDBCommand.CreateParameter();
            cmdParamRecPeriodID.ParameterName = "@RecPeriodID";
            cmdParamRecPeriodID.Value = recPeriodID;

            IDbDataParameter cmdParamLanguageID = oIDBCommand.CreateParameter();
            cmdParamLanguageID.ParameterName = "@LanguageID";
            cmdParamLanguageID.Value = languageID;

            IDbDataParameter cmdParamDefaultLanguageID = oIDBCommand.CreateParameter();
            cmdParamDefaultLanguageID.ParameterName = "@DefaultLanguageID";
            cmdParamDefaultLanguageID.Value = defaultLanguageID;

            IDbDataParameter cmdParamCompanyID = oIDBCommand.CreateParameter();
            cmdParamCompanyID.ParameterName = "@CompanyID";
            cmdParamCompanyID.Value = companyID;

            cmdParams.Add(cmdParamReportID);
            cmdParams.Add(cmdParamUserID);
            cmdParams.Add(cmdParamRoleID);
            cmdParams.Add(cmdParamRecPeriodID);
            cmdParams.Add(cmdParamLanguageID);
            cmdParams.Add(cmdParamDefaultLanguageID);
            cmdParams.Add(cmdParamCompanyID);

            return oIDBCommand;
        }

        protected IDbCommand GetArchivedReportCommand(int reportArchiveID)
        {
            IDbCommand oIDBCommand = this.CreateCommand("usp_GET_ReportActivityByArchiveReportID");
            oIDBCommand.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = oIDBCommand.Parameters;

            IDbDataParameter cmdParamReportID = oIDBCommand.CreateParameter();
            cmdParamReportID.ParameterName = "@ReportArchiveID";
            cmdParamReportID.Value = reportArchiveID;

            cmdParams.Add(cmdParamReportID);
            return oIDBCommand;
        }

        #endregion

        protected ReportArchiveInfo MapMyObject(IDataReader r)
        {
            ReportArchiveInfo entity = new ReportArchiveInfo();
            try
            {
                int ordinal = r.GetOrdinal("ReportID");
                if (!r.IsDBNull(ordinal)) entity.ReportID = ((System.Int16)(r.GetValue(ordinal)));
            }
            catch (Exception) { }
            try
            {
                int ordinal = r.GetOrdinal("ReconciliationPeriodID");
                if (!r.IsDBNull(ordinal)) entity.ReconciliationPeriodID = ((System.Int32)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("UserID");
                if (!r.IsDBNull(ordinal)) entity.UserID = ((System.Int32)(r.GetValue(ordinal)));
            }
            catch (Exception) { }
            try
            {
                int ordinal = r.GetOrdinal("ReportArchiveID");
                if (!r.IsDBNull(ordinal)) entity.ReportArchiveID = ((System.Int64)(r.GetValue(ordinal)));
            }
            catch (Exception) { }
            try
            {
                int ordinal = r.GetOrdinal("Comments");
                if (!r.IsDBNull(ordinal)) entity.Comments = ((System.String)(r.GetValue(ordinal)));
            }
            catch (Exception) { }
            try
            {
                int ordinal = r.GetOrdinal("ReportCreateDateTime");
                if (!r.IsDBNull(ordinal)) entity.ReportCreateDateTime = ((System.DateTime)(r.GetValue(ordinal)));
            }
            catch (Exception) { }
            try
            {
                int ordinal = r.GetOrdinal("ReportData");
                if (!r.IsDBNull(ordinal))
                    entity.ReportData = ((System.Byte[])(r.GetValue(ordinal)));
            }
            catch (Exception) { }
            try
            {
                int ordinal = r.GetOrdinal("PeriodEndDate");
                if (!r.IsDBNull(ordinal)) entity.PeriodEndDate = ((System.DateTime)(r.GetValue(ordinal)));
            }
            catch (Exception) { }
            try
            {
                int ordinal = r.GetOrdinal("FirstName");
                if (!r.IsDBNull(ordinal)) entity.FirstName = ((System.String)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("LastName");
                if (!r.IsDBNull(ordinal)) entity.LastName = ((System.String)(r.GetValue(ordinal)));
            }
            catch (Exception) { }
            try
            {
                int ordinal = r.GetOrdinal("ReportArchiveTypeID");
                if (!r.IsDBNull(ordinal)) entity.ReportArchiveTypeID = ((System.Int16)(r.GetValue(ordinal)));
            }
            catch (Exception) { }
            return entity;
        }

        public List<ReportArchiveInfo> GetReportsActivity(int recPeriodID, int languageID, int defaultLanguageID, int companyID, short RoleID)
        {
            IDbCommand oCommand = null;
            IDbConnection oConnection = null;
            List<ReportArchiveInfo> oRptArchiveInfoCollection;
            IDataReader reader = null;
            try
            {
                oCommand = this.GetReportsActivityCommand(recPeriodID, languageID, defaultLanguageID, companyID, RoleID);
                oConnection = this.CreateConnection();

                oCommand.Connection = oConnection;
                oConnection.Open();
                reader = oCommand.ExecuteReader(CommandBehavior.CloseConnection);
                oRptArchiveInfoCollection = this.MapObjects(reader);
                reader.Close();


                //Get Parameters
                ReportArchiveParameterDAO oRptArchiveParamDAO = new ReportArchiveParameterDAO(this.CurrentAppUserInfo);
                List<ReportArchiveParameterInfo> oRptArchiveParamsCollection = oRptArchiveParamDAO.GetArchiveRptParamsByRecPeriodID( recPeriodID, languageID, defaultLanguageID, companyID);

                //Fill ReportArchiveInfo with respective Params
                foreach (ReportArchiveInfo oRptArchiveInfo in oRptArchiveInfoCollection)
                {
                    long RptArchiveID = oRptArchiveInfo.ReportArchiveID.Value;
                    List<ReportArchiveParameterInfo> oRptArchParams = oRptArchiveParamsCollection.FindAll(r => r.ReportArchiveID.Value == RptArchiveID);
                    oRptArchiveInfo.ReportArchiveParameterByRptArchiveID = oRptArchParams;
                    oRptArchParams = null;
                }
            }
            finally
            {
                if (reader != null && !reader.IsClosed)
                    reader.Close();
                if ((oConnection != null) && (oConnection.State != ConnectionState.Closed))
                {
                    oConnection.Dispose();
                }
            }
            return oRptArchiveInfoCollection;

        }

        protected IDbCommand GetReportsActivityCommand(int recPeriodID, int languageID, int defaultLanguageID, int companyID, short RoleID)
        {
            IDbCommand oIDBCommand = this.CreateCommand("usp_GET_ReportsActivityByRecPeriodID");
            oIDBCommand.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = oIDBCommand.Parameters;
           
            IDbDataParameter cmdParamRecPeriodID = oIDBCommand.CreateParameter();
            cmdParamRecPeriodID.ParameterName = "@RecPeriodID";
            cmdParamRecPeriodID.Value = recPeriodID;

            IDbDataParameter cmdParamLanguageID = oIDBCommand.CreateParameter();
            cmdParamLanguageID.ParameterName = "@LanguageID";
            cmdParamLanguageID.Value = languageID;

            IDbDataParameter cmdParamDefaultLanguageID = oIDBCommand.CreateParameter();
            cmdParamDefaultLanguageID.ParameterName = "@DefaultLanguageID";
            cmdParamDefaultLanguageID.Value = defaultLanguageID;

            IDbDataParameter cmdParamCompanyID = oIDBCommand.CreateParameter();
            cmdParamCompanyID.ParameterName = "@CompanyID";
            cmdParamCompanyID.Value = companyID;

            IDbDataParameter cmdParamRoleID = oIDBCommand.CreateParameter();
            cmdParamRoleID.ParameterName = "@RoleID";
            cmdParamRoleID.Value = RoleID;


            
           
            cmdParams.Add(cmdParamRecPeriodID);
            cmdParams.Add(cmdParamLanguageID);
            cmdParams.Add(cmdParamDefaultLanguageID);
            cmdParams.Add(cmdParamCompanyID);
            cmdParams.Add(cmdParamRoleID);

            return oIDBCommand;
        }



    }
}