


using System;
using System.Data;
using System.Data.Common;
using Adapdev.Data;
using Adapdev.Data.Sql;
using SkyStem.ART.App.DAO.Base;
using System.Collections.Generic;
using SkyStem.ART.Client.Model;

namespace SkyStem.ART.App.DAO
{
    public class UserRoleDAO : UserRoleDAOBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public UserRoleDAO(AppUserInfo oAppUserInfo) :
            base(oAppUserInfo)
        {
        }
        public object SaveUserRoleDataTable(DataTable oUserRoleDT, int UserId, bool IsPotentialRole)
        {
            IDbCommand IDBCmmd = this.CreateCommand("usp_INS_UserRoles");
            IDBCmmd.CommandType = CommandType.StoredProcedure;
            IDataParameterCollection cmdParams = IDBCmmd.Parameters;
            IDbDataParameter cmdParamRolesTable = IDBCmmd.CreateParameter();
            IDbDataParameter cmdParamUserId = IDBCmmd.CreateParameter();
            IDbDataParameter cmdParamIsPotentialRole = IDBCmmd.CreateParameter();

            cmdParamRolesTable.ParameterName = "@RolesTable";
            cmdParamRolesTable.Value = oUserRoleDT;
            cmdParamUserId.ParameterName = "@UserID";
            cmdParamUserId.Value = UserId;
            cmdParamIsPotentialRole.ParameterName = "@IsPotentialRole";
            cmdParamIsPotentialRole.Value = IsPotentialRole;

            cmdParams.Add(cmdParamRolesTable);
            cmdParams.Add(cmdParamUserId);
            cmdParams.Add(cmdParamIsPotentialRole);

            using (IDbConnection cnn = this.CreateConnection())
            {
                cnn.Open();
                IDBCmmd.Connection = cnn;
                return IDBCmmd.ExecuteScalar();
            }
        }



        public void DeleteInsertRoleByUserID(int userID, List<int> newUserRolesList)
        {
            IDbCommand cmd = null;
            cmd = DeleteUserRoleByUserID(userID);
            using (IDbConnection cnn = this.CreateConnection())
            {
                cnn.Open();
                cmd.Connection = cnn;
                cmd.ExecuteNonQuery();
            }
        }

        public System.Data.IDbCommand DeleteUserRoleByUserID(int userID)
        {


            System.Data.IDbCommand cmd = this.CreateCommand("usp_DEL_UserRole");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@UserID";
            par.Value = userID;
            cmdParams.Add(par);

            return cmd;

        }

        public List<UserRoleInfo> SelectUserActiveRolesPRA(int? userID, int? recPeriodID)
        {
            List<UserRoleInfo> oUserRoleInfoList = new List<UserRoleInfo>();
            IDbConnection con = null;
            IDbCommand cmd = null;
            IDataReader reader = null;
            try
            {
                con = this.CreateConnection();
                cmd = this.CreateSelectUserActiveRolesPRACommand(userID, recPeriodID);
                cmd.Connection = con;
                con.Open();
                reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (reader.Read())
                {
                    oUserRoleInfoList.Add(this.MapObject(reader));
                }
                reader.ClearColumnHash();
                reader.Close();
            }
            finally
            {
                if (reader != null && !reader.IsClosed)
                    reader.Close();
                if ((con != null) && (con.State == ConnectionState.Open))
                    con.Close();
            }
            return oUserRoleInfoList;
        }

        private IDbCommand CreateSelectUserActiveRolesPRACommand(int? userID, int? recPeriodID)
        {
            IDbCommand cmd = this.CreateCommand("usp_SEL_ActiveUserRolesPRA");
            cmd.CommandType = CommandType.StoredProcedure;
            IDataParameterCollection cmdParams = cmd.Parameters;

            IDbDataParameter parUserID = cmd.CreateParameter();
            parUserID.ParameterName = "@UserID";
            parUserID.Value = userID;
            cmdParams.Add(parUserID);

            IDbDataParameter parRoleID = cmd.CreateParameter();
            parRoleID.ParameterName = "@RecPeriodID";
            parRoleID.Value = recPeriodID;
            cmdParams.Add(parRoleID);

            return cmd;
        }

        public object SaveUserRoleTransitDataTable(DataTable oUserRoleDT, int UserTransitID, bool IsPotentialRole)
        {
            IDbCommand IDBCmmd = this.CreateCommand("usp_INS_UserRoleTransit");
            IDBCmmd.CommandType = CommandType.StoredProcedure;
            IDataParameterCollection cmdParams = IDBCmmd.Parameters;
            IDbDataParameter cmdParamRolesTable = IDBCmmd.CreateParameter();
            IDbDataParameter cmdParamUserTransitID = IDBCmmd.CreateParameter();
            IDbDataParameter cmdParamIsPotentialRole = IDBCmmd.CreateParameter();

            cmdParamRolesTable.ParameterName = "@RolesTable";
            cmdParamRolesTable.Value = oUserRoleDT;
            cmdParamUserTransitID.ParameterName = "@UserTransitID";
            cmdParamUserTransitID.Value = UserTransitID;
            cmdParamIsPotentialRole.ParameterName = "@IsPotentialRole";
            cmdParamIsPotentialRole.Value = IsPotentialRole;

            cmdParams.Add(cmdParamRolesTable);
            cmdParams.Add(cmdParamUserTransitID);
            cmdParams.Add(cmdParamIsPotentialRole);

            using (IDbConnection cnn = this.CreateConnection())
            {
                cnn.Open();
                IDBCmmd.Connection = cnn;
                return IDBCmmd.ExecuteScalar();
            }
        }
    }
}