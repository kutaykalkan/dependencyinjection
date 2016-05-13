


using System;
using System.Data;
using System.Data.Common;
using Adapdev.Data;
using Adapdev.Data.Sql;
using SkyStem.ART.App.DAO.Base;
using System.Collections.Generic;
using SkyStem.ART.Client.Model;
using SkyStem.ART.Client.Exception;
using System.Data.SqlClient;
using SkyStem.ART.App.Utility;

namespace SkyStem.ART.App.DAO
{
    public class ReconciliationFrequencyHdrDAO : ReconciliationFrequencyHdrDAOBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public ReconciliationFrequencyHdrDAO(AppUserInfo oAppUserInfo) :
            base(oAppUserInfo)
        {
        }
        internal List<ReconciliationFrequencyHdrInfo> GetAllRecFrequencyHdrByCompanyID(int CompanyID)
        {
            List<ReconciliationFrequencyHdrInfo> oReconciliationFrequencyHdrInfocollection = new List<ReconciliationFrequencyHdrInfo>();
            IDbConnection con = null;
            IDbCommand cmd = null;

            try
            {
                con = this.CreateConnection();
                cmd = this.SelectAllByCompanyID(CompanyID);
                cmd.Connection = con;
                con.Open();

                IDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                ReconciliationFrequencyHdrInfo oReconciliationFrequencyHdrInfo = new ReconciliationFrequencyHdrInfo();
                while (reader.Read())
                {
                    oReconciliationFrequencyHdrInfo = this.MapObject(reader);
                    oReconciliationFrequencyHdrInfocollection.Add(oReconciliationFrequencyHdrInfo);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if ((con != null) && (con.State == ConnectionState.Open))
                    con.Close();
            }

            return oReconciliationFrequencyHdrInfocollection;
        }

        private IDbCommand SelectAllByCompanyID(int CompanyID)
        {

            System.Data.IDbCommand cmd = this.CreateCommand("usp_SEL_ReconcilationFrequencyHDR");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@CompanyID";
            par.Value = CompanyID;
            cmdParams.Add(par);
            return cmd;


        }

        public int? InsertReconciliationFrequencyHdrInfo(ReconciliationFrequencyHdrInfo objReconciliationFrequencyHdrInfo, List<int> ConvertIDCollectionToDataTable)
        {
            IDbConnection oConnection = null;
            IDbTransaction oTransaction = null;
            int? ReconciliationFrequencyID = null;
            int? rowsAffected = 0;

            try
            {

                ReconciliationFrequencyHdrDAO oReconciliationFrequencyHdrDAO = new ReconciliationFrequencyHdrDAO(this.CurrentAppUserInfo);

                oConnection = oReconciliationFrequencyHdrDAO.CreateConnection();
                oConnection.Open();
                oTransaction = oConnection.BeginTransaction();//Begin transaction
                //Save DataImportHDR
                oReconciliationFrequencyHdrDAO.Save(objReconciliationFrequencyHdrInfo, oConnection, oTransaction);
                if (objReconciliationFrequencyHdrInfo.ReconciliationFrequencyID.HasValue)
                {
                    ReconciliationFrequencyID = objReconciliationFrequencyHdrInfo.ReconciliationFrequencyID;
                    DataTable dtReconciliationFrequency = ServiceHelper.ConvertIDCollectionToDataTable(ConvertIDCollectionToDataTable);
                    rowsAffected = oReconciliationFrequencyHdrDAO.InsertRecFrequencyrecperiodIDBCommand(dtReconciliationFrequency, ReconciliationFrequencyID, oConnection, oTransaction);
                }
                oTransaction.Commit();
            }
            catch (ARTException ex)
            {
                if (oConnection != null && oConnection.State != ConnectionState.Closed && oTransaction != null)
                    oTransaction.Rollback();
                throw ex;
            }
            catch (SqlException ex)
            {
                if (oConnection != null && oConnection.State != ConnectionState.Closed && oTransaction != null)
                    oTransaction.Rollback();
                ServiceHelper.LogAndThrowGenericSqlException(ex, this.CurrentAppUserInfo);
            }
            catch (Exception ex)
            {
                if (oConnection != null && oConnection.State != ConnectionState.Closed && oTransaction != null)
                    oTransaction.Rollback();
                ServiceHelper.LogAndThrowGenericException(ex, this.CurrentAppUserInfo);
            }

            finally
            {
                if ((null != oConnection) && (oConnection.State == ConnectionState.Open))
                    oConnection.Dispose();
            }
            return ReconciliationFrequencyID;
        }

        public int? InsertRecFrequencyrecperiodIDBCommand(DataTable dtRecPerioID, int? RecFrequencyID
        , IDbConnection oConnection, IDbTransaction oTransaction)
        {
            IDbCommand oDBCommand = null;
            oDBCommand = this.InsertRecFrequencyrecperiodIDBCommand(dtRecPerioID, RecFrequencyID);
            oDBCommand.Connection = oConnection;
            oDBCommand.Transaction = oTransaction;
            //IDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            Object oReturnObject = oDBCommand.ExecuteScalar();
            return (oReturnObject == DBNull.Value) ? null : (int?)oReturnObject;
        }

        protected IDbCommand InsertRecFrequencyrecperiodIDBCommand(DataTable dtRecPerioID, int? RecFrequencyID)
        {
            IDbCommand oIDBCommand = this.CreateCommand("usp_INS_RecFrequencyRecPeriod");
            oIDBCommand.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = oIDBCommand.Parameters;

            IDbDataParameter cmdParamdtRecPerioIDdtTable = oIDBCommand.CreateParameter();
            cmdParamdtRecPerioIDdtTable.ParameterName = "@RecIDTable";
            cmdParamdtRecPerioIDdtTable.Value = dtRecPerioID;
            cmdParams.Add(cmdParamdtRecPerioIDdtTable);

            IDbDataParameter cmdParamRecFrequencyID = oIDBCommand.CreateParameter();
            cmdParamRecFrequencyID.ParameterName = "@RecFrequencyID";
            cmdParamRecFrequencyID.Value = RecFrequencyID;
            cmdParams.Add(cmdParamRecFrequencyID);


            return oIDBCommand;
        }



    }
}