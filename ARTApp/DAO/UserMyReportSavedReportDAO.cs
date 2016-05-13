


using System;
using System.Data;
using System.Data.Common;
using Adapdev.Data;
using Adapdev.Data.Sql;
using SkyStem.ART.App.DAO.Base;
using SkyStem.ART.Client.Model;
using System.Collections.Generic;
using SkyStem.ART.App.Utility;

namespace SkyStem.ART.App.DAO
{
    public class UserMyReportSavedReportDAO : UserMyReportSavedReportDAOBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public UserMyReportSavedReportDAO(AppUserInfo oAppUserInfo) :
            base(oAppUserInfo)
        {
        }
        public IList<UserMyReportSavedReportInfo> GetAllUserMyReportSavedReportCollection(short? roleID, int? userID, short? reportID, int languageID, int defaultLanguageID, int companyID)
        {

            IList<UserMyReportSavedReportInfo> oUserMyReportSavedReportInfoCollection = new List<UserMyReportSavedReportInfo>();
            UserMyReportSavedReportInfo oUserMyReportSavedReportInfo = null;
            IDbConnection oConnection = null;
            try
            {
                oConnection = CreateConnection();
                oConnection.Open();
                IDbCommand oCommand;
                oCommand = CreateUserMyReportSavedReportListCommand(roleID, userID, reportID);
                oCommand.Connection = oConnection;
                IDataReader oDataReader = oCommand.ExecuteReader(CommandBehavior.CloseConnection);
                while (oDataReader.Read())
                {
                    oUserMyReportSavedReportInfo = (UserMyReportSavedReportInfo)SavedReportMapObject(oDataReader);
                    oUserMyReportSavedReportInfoCollection.Add(oUserMyReportSavedReportInfo);
                }



                //Get Parameters
                UserMyReportSavedReportParameterDAO ooUserMyReportSavedReportParameterDAO = new UserMyReportSavedReportParameterDAO(this.CurrentAppUserInfo);
                List<UserMyReportSavedReportParameterInfo> oUserMyReportSavedReportParamsCollection = (List<UserMyReportSavedReportParameterInfo>)ooUserMyReportSavedReportParameterDAO.GetUserMyReportSavedReportParameterByReportIDUserIDRoleID(reportID
           , userID, roleID, languageID, defaultLanguageID, companyID);

                //Fill ReportArchiveInfo with respective Params
                foreach (UserMyReportSavedReportInfo ooUserMyReportSavedReportInfo in oUserMyReportSavedReportInfoCollection)
                {
                    long UserMyReportSavedReportID = ooUserMyReportSavedReportInfo.UserMyReportSavedReportID;
                    List<UserMyReportSavedReportParameterInfo> oMyReportSavedReportParams = oUserMyReportSavedReportParamsCollection.FindAll(r => r.UserMyReportSavedReportID == UserMyReportSavedReportID);
                    ooUserMyReportSavedReportInfo.UserMyReportSavedReportParameterByUserMyReportSavedReportID = oMyReportSavedReportParams;
                    oMyReportSavedReportParams = null;
                }

                oDataReader.Close();
            }
            finally
            {
                if (oConnection != null && oConnection.State != ConnectionState.Closed)
                    oConnection.Dispose();
            }
            return oUserMyReportSavedReportInfoCollection;
        }

        protected System.Data.IDbCommand CreateUserMyReportSavedReportListCommand(short? roleID, int? userID, short? reportID)
        {
            System.Data.IDbCommand cmd = this.CreateCommand("usp_RPT_SEL_UserMyReportSavedReport");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@RoleID";
            par.Value = roleID;
            cmdParams.Add(par);

            System.Data.IDbDataParameter parReportID = cmd.CreateParameter();
            parReportID.ParameterName = "@ReportID";
            parReportID.Value = reportID;
            cmdParams.Add(parReportID);

            System.Data.IDbDataParameter parUserID = cmd.CreateParameter();
            parUserID.ParameterName = "@UserID";
            parUserID.Value = userID;
            cmdParams.Add(parUserID);

            return cmd;
        }



        protected UserMyReportSavedReportInfo SavedReportMapObject(System.Data.IDataReader r)
        {

            UserMyReportSavedReportInfo entity = new UserMyReportSavedReportInfo();



            try
            {
                int ordinal = r.GetOrdinal("UserMyReportSavedReportID");
                if (!r.IsDBNull(ordinal)) entity.UserMyReportSavedReportID = ((System.Int64)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("UserMyReportID");
                if (!r.IsDBNull(ordinal)) entity.UserMyReportID = ((System.Int32)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("UserMyReportSavedReportName");
                if (!r.IsDBNull(ordinal)) entity.UserMyReportSavedReportName = ((System.String)(r.GetValue(ordinal)));
            }
            catch (Exception) { }

            try
            {
                int ordinal = r.GetOrdinal("DateAdded");
                if (!r.IsDBNull(ordinal)) entity.DateAdded = ((System.DateTime)(r.GetValue(ordinal)));
            }
            catch (Exception) { }
            try
            {
                int ordinal = r.GetOrdinal("Report");
                if (!r.IsDBNull(ordinal)) entity.ReportName = ((System.String)(r.GetValue(ordinal)));
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
                int ordinal = r.GetOrdinal("ReportLabelID");
                if (!r.IsDBNull(ordinal)) entity.ReportLabelID = ((System.Int32)(r.GetValue(ordinal)));
            }
            catch (Exception) { }


            return entity;
        }



        public bool DeleteSavedReportData(List<long> UserMyReportIDCollection)
        {
            bool result = false;
            IDbConnection con = null;
            IDbCommand cmd = null;
            try
            {
                cmd = GetDeleteSavedReportDataCommand(UserMyReportIDCollection);
                con = this.CreateConnection();
                cmd.Connection = con;
                con.Open();
                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                    result = true;

            }
            finally
            {

                if (cmd != null)
                {
                    if (cmd.Connection != null)
                    {
                        if (cmd.Connection.State != ConnectionState.Closed)
                        {
                            cmd.Connection.Close();
                            cmd.Connection.Dispose();
                        }
                    }
                    cmd.Dispose();
                }

            }

            return result;

        }

        private IDbCommand GetDeleteSavedReportDataCommand(List<long> UserMyReportIDCollection)
        {

            DataTable dtDeleteSavedReportDataIDs = ServiceHelper.ConvertLongIDCollectionToDataTable(UserMyReportIDCollection);
            IDbCommand cmd = null;
            cmd = this.CreateCommand("usp_DEL_UserSavedReportData");
            cmd.CommandType = CommandType.StoredProcedure;
            IDataParameterCollection cmdParams = cmd.Parameters;
            IDbDataParameter parmDeleteSavedReportDataIDTable = cmd.CreateParameter();
            parmDeleteSavedReportDataIDTable.ParameterName = "@DeleteSavedReportDataIDTable";
            parmDeleteSavedReportDataIDTable.Value = dtDeleteSavedReportDataIDs;
            cmdParams.Add(parmDeleteSavedReportDataIDTable);

            return cmd;
        }
    }
}