

using System;
using System.Data;
using System.Data.Common;
using Adapdev.Data;
using Adapdev.Data.Sql;
using SkyStem.ART.App.DAO.CompanyDatabase.Base;
using SkyStem.ART.Client.Model;
using System.Collections.Generic;
using SkyStem.ART.Client.Model.CompanyDatabase;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using DeployScriptsApplication.APP.BLL;
using SkyStem.ART.App.DAO.Base;
using System.Transactions;


namespace SkyStem.ART.App.DAO.CompanyDatabase
{
    public class ServerCompanyDAO : ServerCompanyDAOBase
    {
        public ServerCompanyDAO(string ConnectionString) :
            base(ConnectionString)
        {
        }
        public List<ServerCompanyInfo> GetAllServerCompanyList(int VersionID)
        {
            IDbConnection con = null;
            IDbCommand cmd = null;
            IDataReader dr = null;
            List<ServerCompanyInfo> oServerCompanyInfoCollection = null;

            try
            {
                cmd = CreateGetAllServerCompanyListCommand(VersionID);
                con = this.CreateConnection();
                cmd.Connection = con;
                con.Open();
                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                oServerCompanyInfoCollection = MapObjects(dr);
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
            return oServerCompanyInfoCollection;
        }
        private IDbCommand CreateGetAllServerCompanyListCommand(int VersionID)
        {
            System.Data.IDbCommand cmd = this.CreateCommand("usp_SEL_AllDatabase");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = Helper.GetDBCommandTimeOut();

            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter parVersionID = cmd.CreateParameter();
            parVersionID.ParameterName = "@VersionID";
            parVersionID.Value = VersionID;
            cmdParams.Add(parVersionID);
            return cmd;
        }
        public string RunCompanyScript(List<VersionScriptInfo> oVersionScriptInfoList, ServerCompanyInfo oServerCompanyInfo, List<CompanyVersionScriptInfo> oCompanyVersionScriptInfoList, CurrentDBVersion oNewCurrentDBVersion)
        {
            string finalStatus = string.Empty;
            CompanyVersionScriptInfo oCompanyVersionScriptInfo;
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required, 
                    new TransactionOptions() { IsolationLevel = System.Transactions.IsolationLevel.Serializable }))
                {
                    using (IDbConnection oConnection = this.CreateConnection())
                    {
                        oConnection.Open();
                        for (int i = 0; i < oVersionScriptInfoList.Count; i++)
                        {
                            oCompanyVersionScriptInfo = new CompanyVersionScriptInfo();
                            string fileName = Helper.GetFullScriptPath(oVersionScriptInfoList[i].ScriptPath);
                            string scriptText = System.IO.File.ReadAllText(fileName);// Get Script From file
                            string Msg = ExecuteSqlScript(scriptText, oConnection, oServerCompanyInfo.CompanyID, oVersionScriptInfoList[i].VersionScriptID, oCompanyVersionScriptInfoList, oVersionScriptInfoList);
                        }
                        UpdateDBVersion(oNewCurrentDBVersion, oConnection);
                    }
                    ts.Complete();
                    finalStatus = "All Scripts Execute successfully";
                }
            }
            catch (SqlException ex)
            {
                // oCompanyVersionScriptInfoList.Clear();
                finalStatus = ex.Message;
            }
            catch (Exception ex)
            {
                // oCompanyVersionScriptInfoList.Clear();
                finalStatus = ex.Message;
            }
            return finalStatus;
        }
        private readonly Regex _sqlScriptSplitRegEx = new Regex(@"^\s*GO\s*$", RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Compiled);
        public string ExecuteSqlScript(string scriptText, IDbConnection oConnection, int? CompanyID, long? VersionScriptID, List<CompanyVersionScriptInfo> oCompanyVersionScriptInfoList, List<VersionScriptInfo> oVersionScriptInfoList)
        {
            IDbCommand cmd = null;
            string Message = string.Empty;
            CompanyVersionScriptInfo oCompanyVersionScriptInfo = new CompanyVersionScriptInfo();
            try
            {
                if (string.IsNullOrEmpty(scriptText))
                    return "";
                var scripts = _sqlScriptSplitRegEx.Split(scriptText);
                foreach (var scriptLet in scripts)
                {
                    if (scriptLet.Trim().Length == 0)
                        continue;
                    cmd = this.CreateCommand(scriptLet);
                    cmd.CommandTimeout = Helper.GetDBCommandTimeOut();
                    cmd.Connection = oConnection;
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                }
                Message = "Execute successfully";
                oCompanyVersionScriptInfo.ErrorMsg = "Version Script Execute successfully.";
                oCompanyVersionScriptInfo.ReleaseStatusID = 1; //Success  
                oCompanyVersionScriptInfo.CompanyID = CompanyID;                
                oCompanyVersionScriptInfo.VersionScriptID = VersionScriptID;
                oCompanyVersionScriptInfo.AddedBy = "DeployScriptApplication";
                oCompanyVersionScriptInfo.DateAdded = DateTime.Now;
                oCompanyVersionScriptInfoList.Add(oCompanyVersionScriptInfo);
            }
            catch (SqlException ex)
            {
                Message = ex.Message;
                //oCompanyVersionScriptInfoList.Clear();
                //foreach (var oVersionScriptInfo in oVersionScriptInfoList)
                //{
                    //if (oVersionScriptInfo.VersionScriptID == VersionScriptID)
                    //{
                        oCompanyVersionScriptInfo.ErrorMsg = Message;
                        oCompanyVersionScriptInfo.ReleaseStatusID = 2; //Error 
                        oCompanyVersionScriptInfo.CompanyID = CompanyID;
                        oCompanyVersionScriptInfo.VersionScriptID = VersionScriptID;
                        oCompanyVersionScriptInfo.AddedBy = "DeployScriptApplication";
                        oCompanyVersionScriptInfo.DateAdded = DateTime.Now;
                        oCompanyVersionScriptInfoList.Add(oCompanyVersionScriptInfo);
                    //}
                    //else
                    //{
                    //    oCompanyVersionScriptInfo.ErrorMsg = "Not Executed";
                    //    oCompanyVersionScriptInfo.ReleaseStatusID = 3; //Not Executed 
                    //    oCompanyVersionScriptInfo.CompanyID = CompanyID;
                    //    oCompanyVersionScriptInfo.VersionScriptID = oVersionScriptInfo.VersionScriptID;
                    //    oCompanyVersionScriptInfo.AddedBy = "DeployScriptApplication";
                    //    oCompanyVersionScriptInfo.DateAdded = DateTime.Now;
                    //    oCompanyVersionScriptInfoList.Add(oCompanyVersionScriptInfo);

                    //}
                //}

                throw ex;
            }
            catch (Exception ex)
            {
                Message = ex.Message;
                //oCompanyVersionScriptInfoList.Clear();
                //foreach (var oVersionScriptInfo in oVersionScriptInfoList)
                //{
                    //if (oVersionScriptInfo.VersionScriptID == VersionScriptID)
                    //{
                        oCompanyVersionScriptInfo.ErrorMsg = Message;
                        oCompanyVersionScriptInfo.ReleaseStatusID = 2; //Error 
                        oCompanyVersionScriptInfo.CompanyID = CompanyID;
                        oCompanyVersionScriptInfo.VersionScriptID = VersionScriptID;
                        oCompanyVersionScriptInfo.AddedBy = "DeployScriptApplication";
                        oCompanyVersionScriptInfo.DateAdded = DateTime.Now;
                        oCompanyVersionScriptInfoList.Add(oCompanyVersionScriptInfo);
                    //}
                    //else
                    //{
                    //    oCompanyVersionScriptInfo.ErrorMsg = "Not Executed";
                    //    oCompanyVersionScriptInfo.ReleaseStatusID = 3; //Not Executed 
                    //    oCompanyVersionScriptInfo.CompanyID = CompanyID;
                    //    oCompanyVersionScriptInfo.VersionScriptID = oVersionScriptInfo.VersionScriptID;
                    //    oCompanyVersionScriptInfo.AddedBy = "DeployScriptApplication";
                    //    oCompanyVersionScriptInfo.DateAdded = DateTime.Now;
                    //    oCompanyVersionScriptInfoList.Add(oCompanyVersionScriptInfo);

                    //}
                //}
                throw ex;
            }
            return Message;
        }
        public string ExecuteSqlScript(string scriptText, IDbConnection oConnection)
        {
            IDbCommand cmd = null;
            string Message = string.Empty;
            CompanyVersionScriptInfo oCompanyVersionScriptInfo = new CompanyVersionScriptInfo();
            try
            {
                if (string.IsNullOrEmpty(scriptText))
                    return "";
                var scripts = _sqlScriptSplitRegEx.Split(scriptText);
                foreach (var scriptLet in scripts)
                {
                    if (scriptLet.Trim().Length == 0)
                        continue;
                    cmd = this.CreateCommand(scriptLet);
                    cmd.CommandTimeout = Helper.GetDBCommandTimeOut();
                    cmd.Connection = oConnection;
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                }
                Message = "Execute successfully";
            }
            catch (SqlException ex)
            {
                Message = ex.Message;

            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }
            return Message;
        }

        public string RunDBBackupScript(string DBScript)
        {
            string finalStatus = string.Empty;
            IDbConnection oConnection = null;
            try
            {
                oConnection = this.CreateConnection();
                oConnection.Open();
                using (oConnection)
                {
                    finalStatus = ExecuteSqlScript(DBScript, oConnection);
                }
                if (finalStatus == "Execute successfully")
                    finalStatus = "Taking  DataBase Backup successfully";
            }
            catch (SqlException ex)
            {
                finalStatus = ex.Message;
            }
            catch (Exception ex)
            {
                finalStatus = ex.Message;
            }
            finally
            {
                if ((null != oConnection) && (oConnection.State == ConnectionState.Open))
                    oConnection.Dispose();
            }
            return finalStatus;
        }

        public string CheckDBConnectionStatus()
        {
            string finalStatus = string.Empty;
            IDbConnection oConnection = null;
            try
            {
                oConnection = this.CreateConnection();
                oConnection.Open();
                finalStatus = "Connection Created Successfully";
            }
            catch (SqlException ex)
            {
                finalStatus = ex.Message;
            }
            catch (Exception ex)
            {
                finalStatus = ex.Message;
            }
            finally
            {
                if ((null != oConnection) && (oConnection.State == ConnectionState.Open))
                    oConnection.Dispose();
            }
            return finalStatus;
        }
        public CurrentDBVersion GetCurrentDBVersion()
        {
            CurrentDBVersion oCurrentDBVersion = new CurrentDBVersion();
            IDbConnection con = null;
            IDbCommand cmd = null;
            IDataReader dr = null;

            try
            {
                cmd = CreateGetCurrentDBVersionCommand();
                con = this.CreateConnection();
                cmd.Connection = con;
                con.Open();
                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dr.Read();
                oCurrentDBVersion.CurrentDBVersionNumber = dr.GetStringValue("CurrentDBVersion");
                oCurrentDBVersion.CurrentDBVersionID = dr.GetInt32Value("CurrentDBVersionID");
                oCurrentDBVersion.DBVersionDate = dr.GetDateValue("DBVersionDate");

            }
            catch (SqlException)
            {
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
            return oCurrentDBVersion;
        }

        private IDbCommand CreateGetCurrentDBVersionCommand()
        {
            System.Data.IDbCommand cmd = this.CreateCommand("usp_GET_CurrentDBVersion");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = Helper.GetDBCommandTimeOut();
            return cmd;
        }
        public void UpdateDBVersion(CurrentDBVersion oNewCurrentDBVersion, IDbConnection oConnection)
        {
            string Message = string.Empty;
            IDbCommand cmd = null;
            cmd = CreateUpdateDBVersionCommand(oNewCurrentDBVersion);
            cmd.CommandTimeout = Helper.GetDBCommandTimeOut();
            cmd.Connection = oConnection;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.ExecuteNonQuery();
        }

        private IDbCommand CreateUpdateDBVersionCommand(CurrentDBVersion oNewCurrentDBVersion)
        {
            System.Data.IDbCommand cmd = this.CreateCommand("usp_UPD_CurrentDBVersion");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = Helper.GetDBCommandTimeOut();

            IDataParameterCollection cmdParams = cmd.Parameters;
            System.Data.IDbDataParameter parCurrentDBVersionID = cmd.CreateParameter();
            parCurrentDBVersionID.ParameterName = "@CurrentDBVersionID";
            parCurrentDBVersionID.Value = oNewCurrentDBVersion.CurrentDBVersionID.Value;
            cmdParams.Add(parCurrentDBVersionID);


            System.Data.IDbDataParameter parCurrentDBVersion = cmd.CreateParameter();
            parCurrentDBVersion.ParameterName = "@CurrentDBVersion";
            parCurrentDBVersion.Value = oNewCurrentDBVersion.CurrentDBVersionNumber;
            cmdParams.Add(parCurrentDBVersion);

            System.Data.IDbDataParameter parDBVersionDate = cmd.CreateParameter();
            parDBVersionDate.ParameterName = "@DBVersionDate";
            parDBVersionDate.Value = oNewCurrentDBVersion.DBVersionDate;
            cmdParams.Add(parDBVersionDate);

            return cmd;
        }

    }
}