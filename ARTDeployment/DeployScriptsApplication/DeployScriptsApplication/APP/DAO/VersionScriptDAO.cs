

using System;
using System.Data;
using System.Data.Common;
using Adapdev.Data;
using Adapdev.Data.Sql;
using SkyStem.ART.App.DAO.Base;
using SkyStem.ART.Client.Model;
using System.Collections.Generic;
using System.Data.SqlClient;
using DeployScriptsApplication.APP.BLL;

namespace SkyStem.ART.App.DAO
{
    public class VersionScriptDAO : VersionScriptDAOBase
    {
        public List<VersionScriptInfo> GetVersionScriptList(int VersionID)
        {
            IDbConnection con = null;
            IDbCommand cmd = null;
            IDataReader dr = null;
            List<VersionScriptInfo> oVersionScriptInfoCollection = null;

            try
            {
                cmd = CreateVersionScriptListCommand(VersionID);
                con = this.CreateConnection();
                cmd.Connection = con;
                con.Open();
                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                oVersionScriptInfoCollection = MapObjects(dr);
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
            return oVersionScriptInfoCollection;
        }
        private IDbCommand CreateVersionScriptListCommand(int VersionID)
        {
            System.Data.IDbCommand cmd = this.CreateCommand("usp_GET_VersionScript");
            cmd.CommandTimeout = Helper.GetDBCommandTimeOut();
            cmd.CommandType = CommandType.StoredProcedure;
            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter parVersionID = cmd.CreateParameter();
            parVersionID.ParameterName = "@VersionID";
            parVersionID.Value = VersionID;
            cmdParams.Add(parVersionID);
            return cmd;
        }

        public List<VersionScriptInfo> GetVersionScriptList(int? LowerVersionID, int? HigherVersionID)
        {
            IDbConnection con = null;
            IDbCommand cmd = null;
            IDataReader dr = null;
            List<VersionScriptInfo> oVersionScriptInfoCollection = null;

            try
            {
                cmd = CreateVersionScriptListCommand( LowerVersionID, HigherVersionID);
                con = this.CreateConnection();
                cmd.Connection = con;
                con.Open();
                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                oVersionScriptInfoCollection = MapObjects(dr);
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
            return oVersionScriptInfoCollection;
        }
        private IDbCommand CreateVersionScriptListCommand(int? LowerVersionID, int? HigherVersionID)
        {
            System.Data.IDbCommand cmd = this.CreateCommand("usp_SEL_VersionScripts");
            cmd.CommandTimeout = Helper.GetDBCommandTimeOut();
            cmd.CommandType = CommandType.StoredProcedure;
            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter parLowerVersionID = cmd.CreateParameter();
            parLowerVersionID.ParameterName = "@LowerVersionID";
            parLowerVersionID.Value = LowerVersionID;
            cmdParams.Add(parLowerVersionID);

            System.Data.IDbDataParameter parHigherVersionID = cmd.CreateParameter();
            parHigherVersionID.ParameterName = "@HigherVersionID";
            parHigherVersionID.Value = HigherVersionID;
            cmdParams.Add(parHigherVersionID);
            
            return cmd;
        }

        public string InsertVersionScript(List<VersionScriptInfo> oVersionScriptInfoList)
        {
            string StatusMsg = string.Empty;
            IDbConnection oConnection = null;
            IDbTransaction oTransaction = null;
            int? rowsAffected = 0;
            try
            {
                oConnection = this.CreateConnection();
                oConnection.Open();
                oTransaction = oConnection.BeginTransaction();//Begin transaction
                //Save CompanyVersionScript Status
                DataTable dtVersionScript = ConvertVersionScriptInfoListToDataTable(oVersionScriptInfoList);
                rowsAffected = this.InsertVersionScriptStatus(dtVersionScript, oConnection, oTransaction);
                if (rowsAffected.HasValue && rowsAffected > 0)
                {
                    oTransaction.Commit();
                    StatusMsg = "VersionScript  saved to database successfully";
                }
                else
                {
                    StatusMsg = "Error while saving  VersionScript  to database";
                }
            }
            catch (SqlException ex)
            {
                StatusMsg = ex.Message;
            }
            catch (Exception ex)
            {
                StatusMsg = ex.Message;
            }
            finally
            {
                if ((null != oConnection) && (oConnection.State == ConnectionState.Open))
                    oConnection.Dispose();
            }
            return StatusMsg;

        }
        internal static DataTable ConvertVersionScriptInfoListToDataTable(List<VersionScriptInfo> oVersionScriptInfoList)
        {
            DataTable dt;
            dt = new DataTable();
            dt.Columns.Add("VersionScriptID", System.Type.GetType("System.Int64"));
            dt.Columns.Add("VersionID", System.Type.GetType("System.Int32"));
            dt.Columns.Add("ScriptName", System.Type.GetType("System.String"));
            dt.Columns.Add("ScriptPath", System.Type.GetType("System.String"));
            dt.Columns.Add("ScriptOrder", System.Type.GetType("System.Int16"));
            dt.Columns.Add("DateAdded", System.Type.GetType("System.DateTime"));
            dt.Columns.Add("AddedBy", System.Type.GetType("System.String"));
            dt.Columns.Add("IsNew", System.Type.GetType("System.Boolean"));
            DataRow dr;
            foreach (VersionScriptInfo oVersionScriptInfo in oVersionScriptInfoList)
            {
                dr = dt.NewRow();

                dr["VersionScriptID"] = oVersionScriptInfo.VersionScriptID;
                dr["VersionID"] = oVersionScriptInfo.VersionID;
                dr["ScriptName"] = oVersionScriptInfo.ScriptName;
                dr["ScriptPath"] = oVersionScriptInfo.ScriptPath;
                dr["ScriptOrder"] = oVersionScriptInfo.ScriptOrder;
                if (oVersionScriptInfo.DateAdded.HasValue)
                    dr["DateAdded"] = Convert.ToDateTime(oVersionScriptInfo.DateAdded.Value.ToShortTimeString());
                dr["AddedBy"] = oVersionScriptInfo.AddedBy;
                dr["IsNew"] = oVersionScriptInfo.IsNew;
                dt.Rows.Add(dr);
                dr = null;
            }
            return dt;
        }
        internal int? InsertVersionScriptStatus(DataTable dtVersionScript, IDbConnection oConnection, IDbTransaction oTransaction)
        {
            IDbCommand oDBCommand = null;
            oDBCommand = this.InsertVersionScriptCommand(dtVersionScript);
            oDBCommand.Connection = oConnection;
            oDBCommand.Transaction = oTransaction;
            Object oReturnObject = oDBCommand.ExecuteScalar();
            return (oReturnObject == DBNull.Value) ? null : (int?)oReturnObject;
        }
        protected IDbCommand InsertVersionScriptCommand(DataTable dtVersionScript)
        {
            IDbCommand oIDBCommand = this.CreateCommand("usp_SAV_VersionScripts");
            oIDBCommand.CommandTimeout = Helper.GetDBCommandTimeOut();
            oIDBCommand.CommandType = CommandType.StoredProcedure;
            IDataParameterCollection cmdParams = oIDBCommand.Parameters;
            IDbDataParameter cmdParamCompanyVersionScriptStatusTable = oIDBCommand.CreateParameter();
            cmdParamCompanyVersionScriptStatusTable.ParameterName = "@VersionScriptTable";
            cmdParamCompanyVersionScriptStatusTable.Value = dtVersionScript;
            cmdParams.Add(cmdParamCompanyVersionScriptStatusTable);
            return oIDBCommand;
        }

        public int DeleteVersionScript(VersionScriptInfo oVersionScriptInfo)
        {
            int RowsAffected = 0;
            IDbConnection con = null;
            IDbCommand cmd = null;
            try
            {
                cmd = CreateDeleteVersionScriptCommand(oVersionScriptInfo);
                con = this.CreateConnection();
                cmd.Connection = con;
                con.Open();
                RowsAffected = cmd.ExecuteNonQuery();
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
            return RowsAffected;
        }
        private IDbCommand CreateDeleteVersionScriptCommand(VersionScriptInfo oVersionScriptInfo)
        {
            IDbCommand cmd = this.CreateCommand("usp_DEL_VersionScript");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = Helper.GetDBCommandTimeOut();
            IDataParameterCollection cmdParams = cmd.Parameters;


            System.Data.IDbDataParameter parVersionScriptID = cmd.CreateParameter();
            parVersionScriptID.ParameterName = "@VersionScriptID";
            parVersionScriptID.Value = oVersionScriptInfo.VersionScriptID;
            cmdParams.Add(parVersionScriptID);

            System.Data.IDbDataParameter parRevisedBy = cmd.CreateParameter();
            parRevisedBy.ParameterName = "@RevisedBy";
            parRevisedBy.Value = oVersionScriptInfo.RevisedBy;
            cmdParams.Add(parRevisedBy);

            System.Data.IDbDataParameter parDateRevised = cmd.CreateParameter();
            parDateRevised.ParameterName = "@DateRevised";
            parDateRevised.Value = oVersionScriptInfo.DateRevised.Value.Subtract(new TimeSpan(2, 0, 0, 0)).ToOADate();
            cmdParams.Add(parDateRevised);

            return cmd;
        }
        public List<long ?> GetExcutedVersionScriptList(int CompanyID,int VersionID)
        {
            IDbConnection con = null;
            IDbCommand cmd = null;
            IDataReader dr = null;
            List<long? > oVersionScriptIDList = new List<long?>();

            try
            {
                cmd = CreateExecutedVersionScriptListCommand(CompanyID,VersionID);
                con = this.CreateConnection();
                cmd.Connection = con;
                con.Open();
                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dr.Read())
                {
                   oVersionScriptIDList.Add(dr.GetInt64Value("VersionScriptID"));
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
            return oVersionScriptIDList;
        }
        private IDbCommand CreateExecutedVersionScriptListCommand(int CompanyID,int VersionID)
        {
            System.Data.IDbCommand cmd = this.CreateCommand("usp_GET_ExecutedVersionScriptsForCompany");
            cmd.CommandTimeout = Helper.GetDBCommandTimeOut();
            cmd.CommandType = CommandType.StoredProcedure;
            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter parCompanyID = cmd.CreateParameter();
            parCompanyID.ParameterName = "@CompanyID";
            parCompanyID.Value = CompanyID;
            cmdParams.Add(parCompanyID);

            System.Data.IDbDataParameter parVersionID = cmd.CreateParameter();
            parVersionID.ParameterName = "@VersionID";
            parVersionID.Value = VersionID;
            cmdParams.Add(parVersionID);
            return cmd;
        }
    }
}