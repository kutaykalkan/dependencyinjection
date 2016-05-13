 

using System;
using System.Data;
using System.Data.Common;
using Adapdev.Data;
using Adapdev.Data.Sql;
using SkyStem.ART.App.DAO.Base;
using System.Data.SqlClient;
using System.Collections.Generic;
using SkyStem.ART.Client.Model;
using DeployScriptsApplication.APP.BLL;

namespace SkyStem.ART.App.DAO
{   
    public class VersionMstDAO : VersionMstDAOBase
    {

        public List<VersionMstInfo> GetAllVersionList()
        {
            IDbConnection con = null;
            IDbCommand cmd = null;
            IDataReader dr = null;
            List<VersionMstInfo> oVersionMstInfoCollection = null;

            try
            {
                cmd = CreateGetAllVersionListCommand();
                con = this.CreateConnection();
                cmd.Connection = con;
                con.Open();
                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                oVersionMstInfoCollection = MapObjects(dr);
                dr.ClearColumnHash();
            }
            finally
            {
                if (dr != null && !dr.IsClosed)
                {
                    dr.Close();
                }

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
            return oVersionMstInfoCollection;
        }
        private IDbCommand CreateGetAllVersionListCommand()
        {
            System.Data.IDbCommand cmd = this.CreateCommand("usp_SEL_AllVersionList");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = Helper.GetDBCommandTimeOut();
            return cmd;
        }

        public int SaveNewVersion(VersionMstInfo oVersionMstInfo)
        {
            int VersionID = 0;
            IDbConnection con = null;
            IDbCommand cmd = null;
            try
            {
                cmd = CreateSaveNewVersionCommand(oVersionMstInfo);
                con = this.CreateConnection();
                cmd.Connection = con;
                con.Open();
                VersionID =Convert.ToInt32( cmd.ExecuteScalar());             
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
            return VersionID;
        }
        private IDbCommand CreateSaveNewVersionCommand(VersionMstInfo oVersionMstInfo)
        {
            IDbCommand cmd = this.CreateCommand("usp_INS_Version");
            cmd.CommandTimeout = Helper.GetDBCommandTimeOut();
            cmd.CommandType = CommandType.StoredProcedure;
            IDataParameterCollection cmdParams = cmd.Parameters;


            System.Data.IDbDataParameter parVersionNumber = cmd.CreateParameter();
            parVersionNumber.ParameterName = "@VersionNumber";
            parVersionNumber.Value = oVersionMstInfo.VersionNumber;
            cmdParams.Add(parVersionNumber);

            System.Data.IDbDataParameter parVersionTypeID = cmd.CreateParameter();
            parVersionTypeID.ParameterName = "@VersionTypeID";
            parVersionTypeID.Value = oVersionMstInfo.VersionTypeID;
            cmdParams.Add(parVersionTypeID);

            System.Data.IDbDataParameter parAddedBy = cmd.CreateParameter();
            parAddedBy.ParameterName = "@AddedBy";
            parAddedBy.Value = oVersionMstInfo.AddedBy;
            cmdParams.Add(parAddedBy);

            System.Data.IDbDataParameter parDateAdded = cmd.CreateParameter();
            parDateAdded.ParameterName = "@DateAdded";
            parDateAdded.Value = oVersionMstInfo.DateAdded.Value.Subtract(new TimeSpan(2, 0, 0, 0)).ToOADate();
            cmdParams.Add(parDateAdded);

            return cmd;
        }
        public List<VersionTypeMstInfo> GetAllVersionTypeList()
        {
            IDbConnection con = null;
            IDbCommand cmd = null;
            IDataReader dr = null;
            List<VersionTypeMstInfo> oVersionTypeMstInfoCollection = new List<VersionTypeMstInfo>();
            VersionTypeMstInfo oVersionTypeMstInfo;

            try
            {
                cmd = CreateGetAllVersionTypeListCommand();
                con = this.CreateConnection();
                cmd.Connection = con;
                con.Open();
                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dr.Read())
                {
                    oVersionTypeMstInfo = VersionTypeMapObject(dr);
                    oVersionTypeMstInfoCollection.Add(oVersionTypeMstInfo);
                }              
            }
            finally
            {
                if (dr != null && !dr.IsClosed)
                {
                    dr.Close();
                }

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
            return oVersionTypeMstInfoCollection;
        }
        private IDbCommand CreateGetAllVersionTypeListCommand()
        {
            System.Data.IDbCommand cmd = this.CreateCommand("usp_GET_AllVersionType");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = Helper.GetDBCommandTimeOut();
            return cmd;
        }

        protected  VersionTypeMstInfo VersionTypeMapObject(System.Data.IDataReader r)
        {
            VersionTypeMstInfo entity = new VersionTypeMstInfo();
            entity.VersionTypeID = r.GetInt32Value("VersionTypeID");
            entity.VersionType = r.GetStringValue("VersionType");           
            return entity;
        }

    }
}