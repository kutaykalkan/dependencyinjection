


using System;
using System.Data;
using System.Data.Common;
using Adapdev.Data;
using Adapdev.Data.Sql;
using SkyStem.ART.App.DAO.Base;
using SkyStem.ART.Client.Model;
using System.Collections.Generic;

namespace SkyStem.ART.App.DAO
{   
    public class ReportArchiveParameterDAO : ReportArchiveParameterDAOBase
    {
                /// <summary>
        /// Constructor
        /// </summary>
        public ReportArchiveParameterDAO(AppUserInfo oAppUserInfo) :
            base(oAppUserInfo)
        {
        }
        public short? InsertRptArchiveParams(DataTable dtRptArchiveParms, IDbConnection oConnection, IDbTransaction oTransaction)
        {
            IDbCommand oDBCommand = null;
            oDBCommand = this.GetInsertRptArchiveParamsCommand(dtRptArchiveParms);
            oDBCommand.Connection = oConnection;
            oDBCommand.Transaction = oTransaction;
            Object oReturnObject = oDBCommand.ExecuteScalar();
            return (oReturnObject == DBNull.Value) ? null : (short?)oReturnObject;

        }
        protected IDbCommand GetInsertRptArchiveParamsCommand(DataTable dtRptArchiveParms)
        {
            IDbCommand oIDBCommand = this.CreateCommand("usp_INS_ReportArchiveParameters");
            oIDBCommand.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = oIDBCommand.Parameters;

            IDbDataParameter cmdParamRptArchiveParamsTable = oIDBCommand.CreateParameter();
            cmdParamRptArchiveParamsTable.ParameterName = "@RptArchiveParamsTable";
            cmdParamRptArchiveParamsTable.Value = dtRptArchiveParms;
            cmdParams.Add(cmdParamRptArchiveParamsTable);

           return oIDBCommand;
        }

        public List<ReportArchiveParameterInfo> GetArchiveRptParamsByReportIDUserIDRoleIDRecPeriodID(short reportID
            , int userID, short roleID, int recPeriodID, int languageID, int defaultLanguageID, int companyID)
        {
            IDbCommand oCommand = null;
            IDbConnection oConnection = null;
            List<ReportArchiveParameterInfo > oRptArchiveParamInfoCollection = null;
            IDataReader reader = null;
            try
            {
                oCommand = this.GetRptActivityParamsCommand(reportID, userID, roleID, recPeriodID, languageID, defaultLanguageID, companyID);
                oConnection = this.CreateConnection();

                oCommand.Connection = oConnection;
                oConnection.Open();
                reader = oCommand.ExecuteReader(CommandBehavior.CloseConnection);
                oRptArchiveParamInfoCollection = this.MapObjects(reader);
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
            return oRptArchiveParamInfoCollection;
        }
        protected IDbCommand GetRptActivityParamsCommand(short reportID, int userID, short roleID
            , int recPeriodID, int languageID, int defaultLanguageID, int companyID)
        {
            IDbCommand oIDBCommand = this.CreateCommand("usp_GET_ReportActivityParametersByReportIDUserIDRoleIDRecPeriodID");
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


        public List<ReportArchiveParameterInfo> GetArchiveRptParamsByRecPeriodID( int recPeriodID, int languageID, int defaultLanguageID, int companyID)
        {
            IDbCommand oCommand = null;
            IDbConnection oConnection = null;
            List<ReportArchiveParameterInfo> oRptArchiveParamInfoCollection = null;
            IDataReader reader = null;
            try
            {
                oCommand = this.GetArchiveRptParamsByRecPeriodIDCommand(recPeriodID, languageID, defaultLanguageID, companyID);
                oConnection = this.CreateConnection();

                oCommand.Connection = oConnection;
                oConnection.Open();
                reader = oCommand.ExecuteReader(CommandBehavior.CloseConnection);
                oRptArchiveParamInfoCollection = this.MapObjects(reader);
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
            return oRptArchiveParamInfoCollection;
        }
        protected IDbCommand GetArchiveRptParamsByRecPeriodIDCommand( int recPeriodID, int languageID, int defaultLanguageID, int companyID)
        {
            IDbCommand oIDBCommand = this.CreateCommand("usp_GET_ReportActivityParametersRecPeriodID");
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
          
            cmdParams.Add(cmdParamRecPeriodID);
            cmdParams.Add(cmdParamLanguageID);
            cmdParams.Add(cmdParamDefaultLanguageID);
            cmdParams.Add(cmdParamCompanyID);

            return oIDBCommand;
        }
    }
}