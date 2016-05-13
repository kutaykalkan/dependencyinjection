

using System;
using System.Data;
using System.Data.Common;
using Adapdev.Data;
using Adapdev.Data.Sql;
using SkyStem.ART.App.DAO.Base;
using SkyStem.ART.Client.Model;
using System.Data.SqlClient;
using System.Collections.Generic;
using DeployScriptsApplication.APP.BLL;

namespace SkyStem.ART.App.DAO
{
    public class CompanyVersionScriptDAO : CompanyVersionScriptDAOBase
    {
        public string InsertCompanyVersionScriptStatus(List<CompanyVersionScriptInfo> oCompanyVersionScriptInfoList)
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
                DataTable dtCompanyVersionScriptStatus = ConvertCompanyVersionScriptInfoListToDataTable(oCompanyVersionScriptInfoList);
                rowsAffected = this.InsertCompanyVersionScriptStatus(dtCompanyVersionScriptStatus, oConnection, oTransaction);
                if (rowsAffected.HasValue && rowsAffected > 0)
                {
                    oTransaction.Commit();
                    StatusMsg = "VersionScript ReleaseStatus saved to database successfully";
                }
                else
                {
                    StatusMsg = "Error while saving Company VersionScript ReleaseStatus to database";
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
        internal static DataTable ConvertCompanyVersionScriptInfoListToDataTable(List<CompanyVersionScriptInfo> oCompanyVersionScriptInfoList)
        {
            DataTable dt;
            dt = new DataTable();
            dt.Columns.Add("CompanyID", System.Type.GetType("System.Int32"));
            dt.Columns.Add("VersionScriptID", System.Type.GetType("System.Int64"));
            dt.Columns.Add("ReleaseStatusID", System.Type.GetType("System.Int16"));
            dt.Columns.Add("ErrorMsg", System.Type.GetType("System.String"));
            dt.Columns.Add("DateAdded", System.Type.GetType("System.DateTime"));
            dt.Columns.Add("AddedBy", System.Type.GetType("System.String"));
            DataRow dr;
            foreach (CompanyVersionScriptInfo oCompanyVersionScriptInfo in oCompanyVersionScriptInfoList)
            {
                dr = dt.NewRow();
                dr["CompanyID"] = oCompanyVersionScriptInfo.CompanyID;
                dr["VersionScriptID"] = oCompanyVersionScriptInfo.VersionScriptID;
                dr["ReleaseStatusID"] = oCompanyVersionScriptInfo.ReleaseStatusID;
                dr["ErrorMsg"] = oCompanyVersionScriptInfo.ErrorMsg;
                dr["DateAdded"] = Convert.ToDateTime(oCompanyVersionScriptInfo.DateAdded.Value.ToShortTimeString());
                dr["AddedBy"] = oCompanyVersionScriptInfo.AddedBy;
                dt.Rows.Add(dr);
                dr = null;
            }
            return dt;
        }
        internal int? InsertCompanyVersionScriptStatus(DataTable dtCompanyVersionScriptStatus, IDbConnection oConnection, IDbTransaction oTransaction)
        {
            IDbCommand oDBCommand = null;
            oDBCommand = this.InsertCompanyVersionScriptCommand(dtCompanyVersionScriptStatus);
            oDBCommand.Connection = oConnection;
            oDBCommand.Transaction = oTransaction;
            Object oReturnObject = oDBCommand.ExecuteScalar();
            return (oReturnObject == DBNull.Value) ? null : (int?)oReturnObject;
        }
        protected IDbCommand InsertCompanyVersionScriptCommand(DataTable dtCompanyVersionScriptStatus)
        {
            IDbCommand oIDBCommand = this.CreateCommand("usp_INS_CompanyVersionScriptStatus");
            oIDBCommand.CommandType = CommandType.StoredProcedure;
            oIDBCommand.CommandTimeout = Helper.GetDBCommandTimeOut();
            IDataParameterCollection cmdParams = oIDBCommand.Parameters;

            IDbDataParameter cmdParamCompanyVersionScriptStatusTable = oIDBCommand.CreateParameter();
            cmdParamCompanyVersionScriptStatusTable.ParameterName = "@CompanyVersionScriptTable";
            cmdParamCompanyVersionScriptStatusTable.Value = dtCompanyVersionScriptStatus;
            cmdParams.Add(cmdParamCompanyVersionScriptStatusTable);
            return oIDBCommand;
        }
        public List<CompanyVersionScriptInfo> GetCompanyVersionScriptInfoListForSelectedRows(List<long> VersionScriptIDList, List<int> CompanyIDList)
        {
            IDbConnection con = null;
            IDbCommand cmd = null;
            IDataReader dr = null;
            List<CompanyVersionScriptInfo> oCompanyVersionScriptInfoCollection = null;

            try
            {
                DataTable dtVersionScriptID = ConvertIDCollectionToDataTable(VersionScriptIDList);
                DataTable dtCompanyID = ConvertIDCollectionToDataTable(CompanyIDList);
                cmd = CreateCompanyVersionScriptInfoListForSelectedRowsCommand(dtVersionScriptID, dtCompanyID);
                con = this.CreateConnection();
                cmd.Connection = con;
                con.Open();
                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                oCompanyVersionScriptInfoCollection = MapObjects(dr);
                //dr.ClearColumnHash();
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
            return oCompanyVersionScriptInfoCollection;
        }
        private IDbCommand CreateCompanyVersionScriptInfoListForSelectedRowsCommand(DataTable dtVersionScriptID, DataTable dtCompanyID)
        {
            System.Data.IDbCommand cmd = this.CreateCommand("usp_SEL_CompanyVersionScript");
            cmd.CommandTimeout = Helper.GetDBCommandTimeOut();
            cmd.CommandType = CommandType.StoredProcedure;
            IDataParameterCollection cmdParams = cmd.Parameters;

            IDbDataParameter cmdParamVersionScriptIDTable = cmd.CreateParameter();
            cmdParamVersionScriptIDTable.ParameterName = "@VersionScriptIDTable";
            cmdParamVersionScriptIDTable.Value = dtVersionScriptID;
            cmdParams.Add(cmdParamVersionScriptIDTable);

            IDbDataParameter cmdParamCompanyIDTable = cmd.CreateParameter();
            cmdParamCompanyIDTable.ParameterName = "@CompanyIDTable";
            cmdParamCompanyIDTable.Value = dtCompanyID;
            cmdParams.Add(cmdParamCompanyIDTable);

            return cmd;
        }
        internal static DataTable ConvertIDCollectionToDataTable(List<int> oIDCollection)
        {
            DataTable dt = null;
            if (oIDCollection != null && oIDCollection.Count > 0)
            {
                dt = new DataTable("IDTable");
                DataColumn dc = dt.Columns.Add("ID");
                DataRow dr;
                for (int i = 0; i < oIDCollection.Count; i++)
                {
                    dr = dt.NewRow();
                    dr[0] = oIDCollection[i];
                    dt.Rows.Add(dr);
                }
            }
            return dt;
        }
        internal static DataTable ConvertIDCollectionToDataTable(List<Int64> oIDCollection)
        {
            DataTable dt = null;
            if (oIDCollection != null && oIDCollection.Count > 0)
            {
                dt = new DataTable("IDTable");
                DataColumn dc = dt.Columns.Add("ID");
                DataRow dr;
                for (int i = 0; i < oIDCollection.Count; i++)
                {
                    dr = dt.NewRow();
                    dr[0] = oIDCollection[i];
                    dt.Rows.Add(dr);
                }
            }
            return dt;
        }
        public List<CompanyVersionScriptInfo> GetAllCompanyVersionScriptInfoList(int VersionID)
        {
            IDbConnection con = null;
            IDbCommand cmd = null;
            IDataReader dr = null;
            List<CompanyVersionScriptInfo> oCompanyVersionScriptInfoCollection = null;

            try
            {
                cmd = CreateGetAllCompanyVersionScriptInfoListCommand( VersionID);
                con = this.CreateConnection();
                cmd.Connection = con;
                con.Open();
                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                oCompanyVersionScriptInfoCollection = MapObjects(dr);
                //dr.ClearColumnHash();
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
            return oCompanyVersionScriptInfoCollection;
        }
        private IDbCommand CreateGetAllCompanyVersionScriptInfoListCommand(int VersionID)
        {
            System.Data.IDbCommand cmd = this.CreateCommand("usp_SEL_AllCompanyVersionScriptReleaseStatus");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = Helper.GetDBCommandTimeOut();
            IDataParameterCollection cmdParams = cmd.Parameters;

            IDbDataParameter cmdParamVersionID = cmd.CreateParameter();
            cmdParamVersionID.ParameterName = "@VersionID";
            cmdParamVersionID.Value = VersionID;
            cmdParams.Add(cmdParamVersionID);
            return cmd;
        }
    }
}