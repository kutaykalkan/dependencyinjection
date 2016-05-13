

using System;
using System.Data;
using System.Data.Common;
using Adapdev.Data;
using Adapdev.Data.Sql;
using SkyStem.ART.App.DAO.Base;
using SkyStem.ART.App.DAO.CompanyDatabase.Base;
using SkyStem.ART.Client.Model;
using SkyStem.ART.Client.Model.CompanyDatabase;
using System.Collections.Generic;
using SkyStem.ART.App.Utility;

namespace SkyStem.ART.App.DAO.CompanyDatabase
{
    public class CompanyUserDAO : CompanyUserDAOBase
    {
        public CompanyUserDAO(AppUserInfo oAppUserInfo) :
            base(oAppUserInfo)
        {
        }

        public CompanyUserInfo GetUserDatabase(string loginID)
        {
            CompanyUserInfo oCompanyUserInfo = null;
            using (IDbConnection cnn = this.CreateConnection())
            {
                cnn.Open();
                using (IDbCommand cmd = CreateGetUserDatabaseCommand(loginID))
                {
                    cmd.Connection = cnn;
                    IDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    if (dr.Read())
                    {
                        oCompanyUserInfo = this.MapObject(dr);
                    }
                    dr.ClearColumnHash();
                }
            }
            return oCompanyUserInfo;
        }


        public List<CompanyUserInfo> GetAllCompanyDatabase()
        {
            List<CompanyUserInfo> oCompanyUserInfoList = null;
            IDbConnection oConn = null;
            IDbCommand oCmd = null;
            IDataReader oReader = null;
            try
            {
                oConn = this.CreateConnection();
                oCmd = this.CreateAllCompanyDatabaseCommand();
                oCmd.Connection = oConn;
                oCmd.Connection.Open();
                oReader = oCmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (oReader.Read())
                {
                    if (oCompanyUserInfoList == null)
                        oCompanyUserInfoList = new List<CompanyUserInfo>();

                    oCompanyUserInfoList.Add(this.MapObject(oReader));
                }
                oReader.Close();
            }
            finally
            {
                if (oConn != null && oConn.State != ConnectionState.Closed)
                    oConn.Close();
            }

            return oCompanyUserInfoList;
        }

        protected override CompanyUserInfo MapObject(IDataReader r)
        {
            CompanyUserInfo oCompanyUserInfo = base.MapObject(r);
            oCompanyUserInfo.ServerName = r.GetStringValue("ServerName");
            oCompanyUserInfo.DatabaseName = r.GetStringValue("DatabaseName");
            oCompanyUserInfo.Instance = r.GetStringValue("Instance");
            oCompanyUserInfo.DBUserID = r.GetStringValue("UserID");
            oCompanyUserInfo.DBPassword = r.GetStringValue("Password");
            oCompanyUserInfo.IsSeparateDatabase = r.GetBooleanValue("IsSeparateDatabase");
            oCompanyUserInfo.CompanyName= r.GetStringValue("CompanyName");
            return oCompanyUserInfo;
        }

        private IDbCommand CreateGetUserDatabaseCommand(string loginID)
        {
            IDbCommand cmd = this.CreateCommand("usp_GET_UserDatabase");
            cmd.CommandType = CommandType.StoredProcedure;


            IDataParameterCollection cmdParams = cmd.Parameters;

            IDbDataParameter cmdLoginID = cmd.CreateParameter();
            cmdLoginID.ParameterName = "@LoginID";
            if (loginID == null)
                cmdLoginID.Value = DBNull.Value;
            else
                cmdLoginID.Value = loginID;
            cmdParams.Add(cmdLoginID);

            return cmd;
        }

        private IDbCommand CreateAllCompanyDatabaseCommand()
        {
            IDbCommand oCmd = this.CreateCommand("[dbo].[usp_SEL_AllCompanyDatabase]");
            oCmd.CommandType = CommandType.StoredProcedure;

            return oCmd;
        }

        public void AddCompanyUser(List<CompanyUserInfo> oCompanyUserInfoList, string addedBy, DateTime? dateAdded)
        {
            using (IDbConnection cnn = this.CreateConnection())
            {
                cnn.Open();
                using (IDbCommand cmd = CreateCommandAddCompanyUser(oCompanyUserInfoList, addedBy, dateAdded))
                {
                    cmd.Connection = cnn;
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private IDbCommand CreateCommandAddCompanyUser(List<CompanyUserInfo> oCompanyUserInfoList, string addedBy, DateTime? dateAdded)
        {
            IDbCommand cmd = this.CreateCommand("usp_INS_CompanyUser");
            cmd.CommandType = CommandType.StoredProcedure;
            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter parCompanyUserTable = cmd.CreateParameter();
            parCompanyUserTable.ParameterName = "@udtCompanyUser";
            parCompanyUserTable.Value = ServiceHelper.ConvertCompanyUserToDataTable(oCompanyUserInfoList);
            cmdParams.Add(parCompanyUserTable);

            System.Data.IDbDataParameter parAddedBy = cmd.CreateParameter();
            parAddedBy.ParameterName = "@AddedBy";
            parAddedBy.Value = addedBy;
            cmdParams.Add(parAddedBy);

            System.Data.IDbDataParameter parDateAdded = cmd.CreateParameter();
            parDateAdded.ParameterName = "@DateAdded";
            parDateAdded.Value = dateAdded.Value.Subtract(new TimeSpan(2, 0, 0, 0)).ToOADate();
            cmdParams.Add(parDateAdded);

            return cmd;
        }

        public void UpdateCompanyUser(CompanyUserInfo oCompanyUserInfo)
        {
            using (IDbConnection cnn = this.CreateConnection())
            {
                cnn.Open();
                using (IDbCommand cmd = CreateCommandUpdateCompanyUser(oCompanyUserInfo))
                {
                    cmd.Connection = cnn;
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private IDbCommand CreateCommandUpdateCompanyUser(CompanyUserInfo oCompanyUserInfo)
        {
            IDbCommand cmd = this.CreateCommand("usp_UPD_CompanyUser");
            cmd.CommandType = CommandType.StoredProcedure;
            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter parCompanyID = cmd.CreateParameter();
            parCompanyID.ParameterName = "@CompanyID";
            if (oCompanyUserInfo.CompanyID.HasValue)
                parCompanyID.Value = oCompanyUserInfo.CompanyID.Value;
            else
                parCompanyID.Value = DBNull.Value;
            cmdParams.Add(parCompanyID);

            System.Data.IDbDataParameter parUserID = cmd.CreateParameter();
            parUserID.ParameterName = "@UserID";
            parUserID.Value = oCompanyUserInfo.UserID.Value;
            cmdParams.Add(parUserID);

            System.Data.IDbDataParameter parLoginID = cmd.CreateParameter();
            parLoginID.ParameterName = "@LoginID";
            parLoginID.Value = oCompanyUserInfo.LoginID;
            cmdParams.Add(parLoginID);

            System.Data.IDbDataParameter parFTPLoginID = cmd.CreateParameter();
            parFTPLoginID.ParameterName = "@FTPLoginID";
            if (!string.IsNullOrEmpty(oCompanyUserInfo.FTPLoginID))
                parFTPLoginID.Value = oCompanyUserInfo.FTPLoginID;
            else
                parFTPLoginID.Value = DBNull.Value;
            cmdParams.Add(parFTPLoginID);

            System.Data.IDbDataParameter parEmailID = cmd.CreateParameter();
            parEmailID.ParameterName = "@EmailID";
            parEmailID.Value = oCompanyUserInfo.EmailID;
            cmdParams.Add(parEmailID);

            System.Data.IDbDataParameter parIsActive = cmd.CreateParameter();
            parIsActive.ParameterName = "@IsActive";
            parIsActive.Value = oCompanyUserInfo.IsActive.Value;
            cmdParams.Add(parIsActive);

            System.Data.IDbDataParameter parRevisedBy = cmd.CreateParameter();
            parRevisedBy.ParameterName = "@RevisedBy";
            parRevisedBy.Value = oCompanyUserInfo.RevisedBy;
            cmdParams.Add(parRevisedBy);

            System.Data.IDbDataParameter parDateRevised = cmd.CreateParameter();
            parDateRevised.ParameterName = "@DateRevised";
            parDateRevised.Value = oCompanyUserInfo.DateRevised.Value.Subtract(new TimeSpan(2, 0, 0, 0)).ToOADate();
            cmdParams.Add(parDateRevised);

            return cmd;
        }
    }
}