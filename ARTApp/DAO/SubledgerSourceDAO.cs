 


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
    public class SubledgerSourceDAO : SubledgerSourceDAOBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public SubledgerSourceDAO(AppUserInfo oAppUserInfo) :
            base(oAppUserInfo)
        {
        }
        public int? InsertInsertSubledgerSourceDataTable(DataTable dtSubledgerSource,
           int? dataImportID, IDbConnection oConnection, IDbTransaction oTransaction)
        {
            IDbCommand oDBCommand = null;
            oDBCommand = this.InsertSubledgerSourceIDBCommand(dtSubledgerSource, dataImportID);
            oDBCommand.Connection = oConnection;
            oDBCommand.Transaction = oTransaction;
            //IDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            Object oReturnObject = oDBCommand.ExecuteScalar();
            return (oReturnObject == DBNull.Value) ? null : (int?)oReturnObject;
        }

        protected IDbCommand InsertSubledgerSourceIDBCommand(DataTable dtSubledgerSource, int? dataImportID)
        {
            IDbCommand oIDBCommand = this.CreateCommand("usp_INS_SubledgerSource");
            oIDBCommand.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = oIDBCommand.Parameters;

            IDbDataParameter cmdParamdtSubledgerSourceTable = oIDBCommand.CreateParameter();
            cmdParamdtSubledgerSourceTable.ParameterName = "@SubledgerSource";
            cmdParamdtSubledgerSourceTable.Value = dtSubledgerSource;
            cmdParams.Add(cmdParamdtSubledgerSourceTable);

            IDbDataParameter cmdParamDataImportID = oIDBCommand.CreateParameter();
            cmdParamDataImportID.ParameterName = "@DataImportID";
            cmdParamDataImportID.Value = dataImportID.Value;
            cmdParams.Add(cmdParamDataImportID);
            return oIDBCommand;
        }

        public List<SubledgerSourceInfo> SelectAllSubledgerSourceByCompanyID(int? CompanyID)
        {
            System.Data.IDbCommand cmd = null;
            try
            {
                cmd = SelectAllSubledgerSourceDBCommand(CompanyID);
                return this.Select(cmd);
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
            //IDbConnection con = null;
            //IDbCommand cmd = null;
            //IDataReader dr = null;
            //List<SubledgerSourceInfo> oSubledgerSourceInfoCollection = new List<SubledgerSourceInfo>();

            //try
            //{
            //    cmd = SelectAllSubledgerSourceDBCommand(CompanyID);
            //    con = this.CreateConnection();
            //    cmd.Connection = con;
            //    con.Open();
            //    dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            //    while (dr.Read())
            //    {
            //        SubledgerSourceInfo oSubledgerSourceInfo = null;
            //        oSubledgerSourceInfo = this.MapForCompanyWorkWeek(dr);
            //        oSubledgerSourceInfoCollection.Add(oSubledgerSourceInfo);
            //    }

            //}
            //finally
            //{
            //    if (dr != null && !dr.IsClosed)
            //    {
            //        dr.Close();
            //    }

            //    if (cmd != null)
            //    {
            //        if (cmd.Connection != null)
            //        {
            //            if (cmd.Connection.State != ConnectionState.Closed)
            //            {
            //                cmd.Connection.Close();
            //                cmd.Connection.Dispose();
            //            }
            //        }
            //        cmd.Dispose();
            //    }

            //}
            //return oSubledgerSourceInfoCollection;
       
        }

        protected IDbCommand SelectAllSubledgerSourceDBCommand(int? CompanyID)
        {
            IDbCommand oIDBCommand = this.CreateCommand("usp_SEL_SubledgerSourceByCompanyID");
            oIDBCommand.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = oIDBCommand.Parameters;
            IDbDataParameter cmdParamCompanyID = oIDBCommand.CreateParameter();
            cmdParamCompanyID.ParameterName = "@CompanyID";
            cmdParamCompanyID.Value = CompanyID;
            cmdParams.Add(cmdParamCompanyID);
            return oIDBCommand;
        }
    }
}