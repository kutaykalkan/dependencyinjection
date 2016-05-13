


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
    public class UserMyReportSavedReportParameterDAO : UserMyReportSavedReportParameterDAOBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public UserMyReportSavedReportParameterDAO(AppUserInfo oAppUserInfo) :
            base(oAppUserInfo)
        {
        }
        public IList<UserMyReportSavedReportParameterInfo> GetAllParametersByMySavedReportID(int UserMyReportSavedReportID)
        {

            IList<UserMyReportSavedReportParameterInfo> oUserMyReportSavedReportParameterInfoCollection = new List<UserMyReportSavedReportParameterInfo>();
            UserMyReportSavedReportParameterInfo oUserMyReportSavedReportParameterInfo = null;
            IDbConnection oConnection = null;
            try
            {
                oConnection = CreateConnection();
                oConnection.Open();
                IDbCommand oCommand;
                oCommand = CreateSelectAllUserMyReportSavedReportParameterCommand(UserMyReportSavedReportID);
                oCommand.Connection = oConnection;
                IDataReader oDataReader = oCommand.ExecuteReader(CommandBehavior.CloseConnection);
                while (oDataReader.Read())
                {
                    oUserMyReportSavedReportParameterInfo = (UserMyReportSavedReportParameterInfo)MapObjectForParameters(oDataReader);
                    oUserMyReportSavedReportParameterInfoCollection.Add(oUserMyReportSavedReportParameterInfo);
                }
                oDataReader.Close();
            }
            finally
            {
                if (oConnection != null && oConnection.State != ConnectionState.Closed)
                    oConnection.Dispose();
            }
            return oUserMyReportSavedReportParameterInfoCollection;

        }

        protected System.Data.IDbCommand CreateSelectAllUserMyReportSavedReportParameterCommand(int UserMyReportSavedReportID)
        {
            System.Data.IDbCommand cmd = this.CreateCommand("usp_SEL_UserMyReportSavedReportParametersBySavedReportID");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@UserMyReportSavedReportID";
            par.Value = UserMyReportSavedReportID;
            cmdParams.Add(par);
            return cmd;
        }


        protected UserMyReportSavedReportParameterInfo MapObjectForParameters(System.Data.IDataReader r)
        {

            UserMyReportSavedReportParameterInfo entity = new UserMyReportSavedReportParameterInfo();

            try
            {
                int ordinal = r.GetOrdinal("ParameterID");
                if (!r.IsDBNull(ordinal)) entity.ReportParameterID = ((short)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("ParameterValue");
                if (!r.IsDBNull(ordinal)) entity.ParameterValue = ((System.String)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("ReportParameterDisplayName");
                if (!r.IsDBNull(ordinal)) entity.ReportParameterDisplayName = ((System.String)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("UserMyReportSavedReportID");
                if (!r.IsDBNull(ordinal)) entity.UserMyReportSavedReportID = ((System.Int64)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("ReportParameterKeyName");
                if (!r.IsDBNull(ordinal)) entity.ReportParameterKeyName = ((System.String)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("ParameterMstID");
                if (!r.IsDBNull(ordinal)) entity.ParameterMstID = ((System.Int16?)(r.GetValue(ordinal)));
            }
            catch (Exception) { }




            return entity;
        }



        public List<UserMyReportSavedReportParameterInfo> GetUserMyReportSavedReportParameterByReportIDUserIDRoleID(short? reportID
           , int? userID, short? roleID, int languageID, int defaultLanguageID, int companyID)
        {
            IDbCommand oCommand = null;
            IDbConnection oConnection = null;
            List<UserMyReportSavedReportParameterInfo> oUserMyReportSavedReportParameterInfoCollection = new List<UserMyReportSavedReportParameterInfo>();
            UserMyReportSavedReportParameterInfo oUserMyReportSavedReportParameterInfo = null;
            IDataReader reader = null;
            try
            {
                oCommand = this.GetRptUserMyReportSavedReportParameterCommand(reportID, userID, roleID, languageID, defaultLanguageID, companyID);
                oConnection = this.CreateConnection();

                oCommand.Connection = oConnection;
                oConnection.Open();
                reader = oCommand.ExecuteReader(CommandBehavior.CloseConnection);
                while (reader.Read())
                {
                    oUserMyReportSavedReportParameterInfo = (UserMyReportSavedReportParameterInfo)MapObjectForParameters(reader);
                    oUserMyReportSavedReportParameterInfoCollection.Add(oUserMyReportSavedReportParameterInfo);
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
            return oUserMyReportSavedReportParameterInfoCollection;
        }
        protected IDbCommand GetRptUserMyReportSavedReportParameterCommand(short? reportID, int? userID, short? roleID
           , int languageID, int defaultLanguageID, int companyID)
        {
            IDbCommand oIDBCommand = this.CreateCommand("usp_GET_UserMyReportSavedReportParametersByReportIDUserIDRoleID");
            oIDBCommand.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = oIDBCommand.Parameters;

            IDbDataParameter cmdParamReportID = oIDBCommand.CreateParameter();
            cmdParamReportID.ParameterName = "@ReportID";
            cmdParamReportID.Value = reportID.Value;

            IDbDataParameter cmdParamUserID = oIDBCommand.CreateParameter();
            cmdParamUserID.ParameterName = "@UserID";
            cmdParamUserID.Value = userID.Value;

            IDbDataParameter cmdParamRoleID = oIDBCommand.CreateParameter();
            cmdParamRoleID.ParameterName = "@RoleID";
            cmdParamRoleID.Value = roleID.Value;

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
            cmdParams.Add(cmdParamLanguageID);
            cmdParams.Add(cmdParamDefaultLanguageID);
            cmdParams.Add(cmdParamCompanyID);

            return oIDBCommand;
        }
    }
}