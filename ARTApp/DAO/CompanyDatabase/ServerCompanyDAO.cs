

using System;
using System.Data;
using System.Data.Common;
using Adapdev.Data;
using Adapdev.Data.Sql;
using SkyStem.ART.App.DAO.CompanyDatabase.Base;
using SkyStem.ART.Client.Model;
using SkyStem.ART.Client.Model.CompanyDatabase;
using SkyStem.ART.App.DAO.Base;
using System.Collections.Generic;

namespace SkyStem.ART.App.DAO.CompanyDatabase
{
    public class ServerCompanyDAO : ServerCompanyDAOBase
    {
        public ServerCompanyDAO(AppUserInfo oAppUserInfo) :
            base(oAppUserInfo)
        {
        }

        public ServerCompanyInfo GetCompanyServer(int? companyID)
        {
            ServerCompanyInfo oServerCompanyInfo = null;
            using (IDbConnection cnn = this.CreateConnection())
            {
                cnn.Open();
                using (IDbCommand cmd = CreateCommandGetCompanyServer(companyID))
                {
                    cmd.Connection = cnn;
                    IDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    if (dr.Read())
                    {
                        oServerCompanyInfo = MapObject(dr);
                    }
                }
            }
            return oServerCompanyInfo;
        }

        private IDbCommand CreateCommandGetCompanyServer(int? companyID)
        {
            IDbCommand cmd = this.CreateCommand("usp_GET_ServerCompany");
            cmd.CommandType = CommandType.StoredProcedure;
            IDataParameterCollection cmdParams = cmd.Parameters;


            System.Data.IDbDataParameter parCompanyID = cmd.CreateParameter();

            parCompanyID.ParameterName = "@CompanyID";
            if (companyID != null)
                parCompanyID.Value = companyID;
            else
                parCompanyID.Value = System.DBNull.Value;

            cmdParams.Add(parCompanyID);
            return cmd;
        }

        public int? AddCompanyServer(ServerCompanyInfo oServerCompanyInfo)
        {
            int? serverCompanyID = null;
            using (IDbConnection cnn = this.CreateConnection())
            {
                cnn.Open();
                using (IDbCommand cmd = CreateCommandAddCompanyServer(oServerCompanyInfo))
                {
                    cmd.Connection = cnn;
                    serverCompanyID = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            return serverCompanyID;
        }

        private IDbCommand CreateCommandAddCompanyServer(ServerCompanyInfo oServerCompanyInfo)
        {
            IDbCommand cmd = this.CreateCommand("usp_INS_ServerCompany");
            cmd.CommandType = CommandType.StoredProcedure;
            IDataParameterCollection cmdParams = cmd.Parameters;


            System.Data.IDbDataParameter parCompanyID = cmd.CreateParameter();
            parCompanyID.ParameterName = "@CompanyID";
            parCompanyID.Value = oServerCompanyInfo.CompanyID;
            cmdParams.Add(parCompanyID);

            System.Data.IDbDataParameter parServerID = cmd.CreateParameter();
            parServerID.ParameterName = "@ServerID";
            parServerID.Value = oServerCompanyInfo.ServerID;
            cmdParams.Add(parServerID);

            System.Data.IDbDataParameter parDatabaseName = cmd.CreateParameter();
            parDatabaseName.ParameterName = "@DatabaseName";
            parDatabaseName.Value = oServerCompanyInfo.DatabaseName;
            cmdParams.Add(parDatabaseName);

            System.Data.IDbDataParameter parAddedBy = cmd.CreateParameter();
            parAddedBy.ParameterName = "@AddedBy";
            parAddedBy.Value = oServerCompanyInfo.AddedBy;
            cmdParams.Add(parAddedBy);

            System.Data.IDbDataParameter parDateAdded = cmd.CreateParameter();
            parDateAdded.ParameterName = "@DateAdded";
            parDateAdded.Value = oServerCompanyInfo.DateAdded.Value.Subtract(new TimeSpan(2, 0, 0, 0)).ToOADate();
            cmdParams.Add(parDateAdded);

            return cmd;
        }

        public List<ServerCompanyInfo> GetServerCompanyListForServiceProcessing()
        {
            List<ServerCompanyInfo> oServerCompanyInfoList = new List<ServerCompanyInfo>();
            using (IDbConnection cnn = this.CreateConnection())
            {
                cnn.Open();
                using (IDbCommand cmd = CreateCommandGetServerCompanyListForServiceProcessing())
                {
                    cmd.Connection = cnn;
                    IDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    while (dr.Read())
                    {
                        oServerCompanyInfoList.Add(MapObject(dr));
                    }
                }
            }
            return oServerCompanyInfoList;
        }

        private IDbCommand CreateCommandGetServerCompanyListForServiceProcessing()
        {
            IDbCommand cmd = this.CreateCommand("usp_SEL_ServerCompanyListForServiceProcessing");
            cmd.CommandType = CommandType.StoredProcedure;
            IDataParameterCollection cmdParams = cmd.Parameters;
            return cmd;
        }

        protected override ServerCompanyInfo MapObject(IDataReader r)
        {
            ServerCompanyInfo oServerCompanyInfo = base.MapObject(r);
            oServerCompanyInfo.IsDatabaseExists = r.GetBooleanValue("IsDatabaseExists");
            oServerCompanyInfo.IsSeparateDatabase = r.GetBooleanValue("IsSeparateDatabase");
            oServerCompanyInfo.ServerName = r.GetStringValue("ServerName");
            oServerCompanyInfo.Instance = r.GetStringValue("Instance");
            oServerCompanyInfo.DBUserID = r.GetStringValue("DBUserID");
            oServerCompanyInfo.DBPassword = r.GetStringValue("DBPassword");
            oServerCompanyInfo.DatabaseName = r.GetStringValue("DatabaseName");
            oServerCompanyInfo.MdfPath = r.GetStringValue("MdfPath");
            oServerCompanyInfo.LdfPath = r.GetStringValue("LdfPath");
            return oServerCompanyInfo;
        }
    }
}