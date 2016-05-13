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
    public class RecItemColumnMstDAO : RecItemColumnMstDAOBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public RecItemColumnMstDAO(AppUserInfo oAppUserInfo) :
            base(oAppUserInfo)
        {
        }

        #region Get Rec Item Columns
        /// <summary>
        /// Get Rec Item Columns from DB
        /// </summary>
        /// <returns>RecItemColumnMstInfo</returns>
        public List<RecItemColumnMstInfo> GetAllRecItemColumns()
        {
            List<RecItemColumnMstInfo> oRecItemColumnMstInfoCollection = new List<RecItemColumnMstInfo>();
            IDbCommand cmd = null;
            IDbConnection con = null;
            try
            {
                con = this.CreateConnection();
                cmd = GetAllRecItemColumnCommand(cmd);
                cmd.Connection = con;
                con.Open();
                IDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                RecItemColumnMstInfo oRecItemColumnMstInfo = null;
                while (reader.Read())
                {
                    oRecItemColumnMstInfo = MapObjectRecItemColumnMstInfo(reader);
                    oRecItemColumnMstInfoCollection.Add(oRecItemColumnMstInfo);
                }
                reader.Close();

            }
            finally
            {
                if ((con != null) && (con.State == ConnectionState.Open))
                {
                    con.Dispose();
                }
            }
            return oRecItemColumnMstInfoCollection;

        }

        private IDbCommand GetAllRecItemColumnCommand(IDbCommand cmd)
        {
            cmd = this.CreateCommand("Matching.usp_SEL_RecItemColumns");
            cmd.CommandType = CommandType.StoredProcedure;
            IDataParameterCollection cmdParams = cmd.Parameters;
            return cmd;
        }


        private RecItemColumnMstInfo MapObjectRecItemColumnMstInfo(IDataReader r)
        {

            RecItemColumnMstInfo entity = new RecItemColumnMstInfo();
            entity.RecItemColumnID = r.GetInt32Value("RecItemColumnID");
            entity.RecItemColumnName = r.GetStringValue("RecItemColumnName");
            entity.RecItemColumnNameLabelID = r.GetInt32Value("RecItemColumnNameLabelID");
            entity.DataTypeID = r.GetInt16Value("DataTypeID");
            entity.DataType = r.GetStringValue("DataType");
            return entity;
        }


        public bool SaveRecItemColumnMapping(DataTable dtRecItemColumnMappingDataTable, string AddedBy, DateTime? DateAdded, string RevisedBy, DateTime? DateRevised)
        {
            bool result = false;
            IDbConnection con = null;
            IDbTransaction oTransaction = null;
            IDbCommand cmd = null;
            try
            {
                con = this.CreateConnection();
                con.Open();
                oTransaction = con.BeginTransaction();
                cmd = GetRecItemColumnMappingCommand(cmd, dtRecItemColumnMappingDataTable, AddedBy, DateAdded, RevisedBy, DateRevised);
                cmd.Connection = con;
                cmd.Transaction = oTransaction;
                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                    result = true;

                oTransaction.Commit();
            }
            catch (Exception ex)
            {
                if (oTransaction != null && oTransaction.Connection != null)
                {
                    oTransaction.Rollback();
                    oTransaction.Dispose();
                }
                ServiceHelper.LogAndThrowGenericException(ex, this.CurrentAppUserInfo);
            }
            finally
            {
                if (oTransaction != null && oTransaction.Connection != null)
                {
                    oTransaction.Rollback();
                    oTransaction.Dispose();
                }

                if ((con != null) && (con.State == ConnectionState.Open))
                {
                    con.Close();
                    con.Dispose();
                }

            }
            return result;

        }

        private IDbCommand GetRecItemColumnMappingCommand(IDbCommand cmd, DataTable dtRecItemColumnMappingDataTable, string AddedBy, DateTime? DateAdded, string RevisedBy, DateTime? DateRevised)
        {
            cmd = this.CreateCommand("Matching.usp_SAVE_RecItemColumnMapping");
            cmd.CommandType = CommandType.StoredProcedure;


            IDataParameterCollection cmdParams = cmd.Parameters;

            IDbDataParameter cmdRecItemColumnMappingDataTable = cmd.CreateParameter();
            cmdRecItemColumnMappingDataTable.ParameterName = "@dtRecItemColumnMappingDataTable";
            cmdRecItemColumnMappingDataTable.Value = dtRecItemColumnMappingDataTable;
            cmdParams.Add(cmdRecItemColumnMappingDataTable);

            IDbDataParameter cmdAddedBy = cmd.CreateParameter();
            cmdAddedBy.ParameterName = "@AddedBy";
            cmdAddedBy.Value = AddedBy;
            cmdParams.Add(cmdAddedBy);

            IDbDataParameter cmdDateAdded = cmd.CreateParameter();
            cmdDateAdded.ParameterName = "@DateAdded";
            cmdDateAdded.Value = DateAdded;
            cmdParams.Add(cmdDateAdded);

            IDbDataParameter cmdRevisedBy = cmd.CreateParameter();
            cmdRevisedBy.ParameterName = "@RevisedBy";
            cmdRevisedBy.Value = RevisedBy;
            cmdParams.Add(cmdRevisedBy);

            IDbDataParameter cmdDateRevised = cmd.CreateParameter();
            cmdDateRevised.ParameterName = "@DateRevised";
            cmdDateRevised.Value = DateRevised;
            cmdParams.Add(cmdDateRevised);
            return cmd;
        }


        public bool CleanRecItemColumnMapping(DataTable dtRecItemColumnMappingDataTable, string RevisedBy, DateTime? DateRevised)
        {
            bool result = false;
            IDbConnection con = null;
            IDbTransaction oTransaction = null;
            IDbCommand cmd = null;
            try
            {
                con = this.CreateConnection();
                con.Open();
                oTransaction = con.BeginTransaction();
                cmd = GetCleanRecItemColumnMappingCommand(cmd, dtRecItemColumnMappingDataTable, RevisedBy, DateRevised);
                cmd.Connection = con;
                cmd.Transaction = oTransaction;
                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                    result = true;

                oTransaction.Commit();
            }
            catch (Exception ex)
            {
                if (oTransaction != null && oTransaction.Connection != null)
                {
                    oTransaction.Rollback();
                    oTransaction.Dispose();
                }
                ServiceHelper.LogAndThrowGenericException(ex, this.CurrentAppUserInfo);
            }
            finally
            {
                if (oTransaction != null && oTransaction.Connection != null)
                {
                    oTransaction.Rollback();
                    oTransaction.Dispose();
                }

                if ((con != null) && (con.State == ConnectionState.Open))
                {
                    con.Close();
                    con.Dispose();
                }

            }
            return result;

        }

        private IDbCommand GetCleanRecItemColumnMappingCommand(IDbCommand cmd, DataTable dtRecItemColumnMappingDataTable, string RevisedBy, DateTime? DateRevised)
        {
            cmd = this.CreateCommand("Matching.usp_upd_CleanRecItemColumnMapping");
            cmd.CommandType = CommandType.StoredProcedure;


            IDataParameterCollection cmdParams = cmd.Parameters;

            IDbDataParameter cmdRecItemColumnMappingDataTable = cmd.CreateParameter();
            cmdRecItemColumnMappingDataTable.ParameterName = "@dtRecItemColumnMappingDataTable";
            cmdRecItemColumnMappingDataTable.Value = dtRecItemColumnMappingDataTable;
            cmdParams.Add(cmdRecItemColumnMappingDataTable);

            IDbDataParameter cmdRevisedBy = cmd.CreateParameter();
            cmdRevisedBy.ParameterName = "@RevisedBy";
            cmdRevisedBy.Value = RevisedBy;
            cmdParams.Add(cmdRevisedBy);

            IDbDataParameter cmdDateRevised = cmd.CreateParameter();
            cmdDateRevised.ParameterName = "@DateRevised";
            cmdDateRevised.Value = DateRevised;
            cmdParams.Add(cmdDateRevised);
            return cmd;
        }
        #endregion
    }
}