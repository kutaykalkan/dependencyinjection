using System;
using System.Data;
using System.Data.Common;
using Adapdev.Data;
using Adapdev.Data.Sql;
using SkyStem.ART.App.DAO.Matching.Base;
using SkyStem.ART.Client.Model.Matching;
using System.Collections.Generic;
using SkyStem.ART.Client.Params.Matching;
using SkyStem.ART.App.Utility;
using SkyStem.ART.Client.Model;

namespace SkyStem.ART.App.DAO.Matching
{
    public class MatchSetMachingSourceDataImportDAO : MatchSetMachingSourceDataImportDAOBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public MatchSetMachingSourceDataImportDAO(AppUserInfo oAppUserInfo) :
            base(oAppUserInfo)
        {
        }
        public MatchSetMatchingSourceDataImportInfo GetMatchSetMatchingSourceDataImport(MatchingParamInfo oMatchingParamInfo)
        {
            MatchSetMatchingSourceDataImportInfo oMatchSetMatchingSourceDataImportInfo = null;
            IDbConnection con = null;
            IDbCommand cmd = null;
            try
            {
                con = this.CreateConnection();
                cmd = this.CreateCommand("Matching.usp_SEL_MatchSetMatchingSourceDataImport");
                cmd.CommandType = CommandType.StoredProcedure;
                IDataParameterCollection cmdParams = cmd.Parameters;

                IDbDataParameter paramMatchingSourceDataImportIDs = cmd.CreateParameter();
                paramMatchingSourceDataImportIDs.ParameterName = "@MatchingSourceDataImportID";
                paramMatchingSourceDataImportIDs.Value = oMatchingParamInfo.MatchingSourceDataImportID;
                cmdParams.Add(paramMatchingSourceDataImportIDs);

                IDbDataParameter paramMatchSetID = cmd.CreateParameter();
                paramMatchSetID.ParameterName = "@MatchSetID";
                paramMatchSetID.Value = oMatchingParamInfo.MatchSetID;
                cmdParams.Add(paramMatchSetID);

                cmd.Connection = con;
                con.Open();
                IDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                reader.Read();
                oMatchSetMatchingSourceDataImportInfo = this.MapObject(reader);
                reader.Close();
            }
            finally
            {
                if ((con != null) && (con.State == ConnectionState.Open))
                {
                    con.Dispose();
                }
            }
            return oMatchSetMatchingSourceDataImportInfo;
        }

        public void SaveMatchSetMatchingSource(MatchingParamInfo oMatchingParamInfo, IDbConnection con, IDbTransaction oTransaction)
        {

            IDbCommand cmd = null;
            DataTable dtMatchingSourcDataImportIDs = ServiceHelper.ConvertIDCollectionToDataTable(oMatchingParamInfo.IDList);

            cmd = CreateSaveMatchSetCommand(oMatchingParamInfo, con, oTransaction, cmd, dtMatchingSourcDataImportIDs);

            cmd.ExecuteNonQuery();
        }

        private IDbCommand CreateSaveMatchSetCommand(MatchingParamInfo oMatchingParamInfo, IDbConnection con, IDbTransaction oTransaction, IDbCommand cmd, DataTable dtMatchingSourcDataImportIDs)
        {
            cmd = this.CreateCommand("[Matching].usp_INS_MatchSetMatchingSourceDataImportHdr");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.Transaction = oTransaction;

            IDataParameterCollection cmdParams = cmd.Parameters;

            IDbDataParameter cmdMatchSetID = cmd.CreateParameter();
            cmdMatchSetID.ParameterName = "@MatchSetID";
            cmdMatchSetID.Value = oMatchingParamInfo.MatchSetID;
            cmdParams.Add(cmdMatchSetID);

            IDbDataParameter cmdMatchingSourceDataImportIDCollection = cmd.CreateParameter();
            cmdMatchingSourceDataImportIDCollection.ParameterName = "@MatchingSourceDataImportIDTable";
            cmdMatchingSourceDataImportIDCollection.Value = dtMatchingSourcDataImportIDs;
            cmdParams.Add(cmdMatchingSourceDataImportIDCollection);

            IDbDataParameter paramRecordSourceTypeID = cmd.CreateParameter();
            paramRecordSourceTypeID.ParameterName = "@RecordSourceTypeID";
            paramRecordSourceTypeID.Value = oMatchingParamInfo.RecordSourceTypeID;
            cmdParams.Add(paramRecordSourceTypeID);

            IDbDataParameter paramAccountID = cmd.CreateParameter();
            paramAccountID.ParameterName = "@AccountID";
            if (oMatchingParamInfo.AccountID != null)
            {
                paramAccountID.Value = oMatchingParamInfo.AccountID;
            }
            else
            {
                paramAccountID.Value = DBNull.Value;
            }

            cmdParams.Add(paramAccountID);
            return cmd;
        }

        public List<MatchSetMatchingSourceDataImportInfo> GetMatchSetMatchingSourceDataImportByMatchSetID(MatchingParamInfo oMatchingParamInfo)
        {
            List<MatchSetMatchingSourceDataImportInfo> oMatchSetMatchingSourceDataImportInfoCollection = new List<MatchSetMatchingSourceDataImportInfo>();
            IDbConnection con = null;
            IDbCommand cmd = null;
            IDataReader reader = null;
            try
            {
                con = this.CreateConnection();
                cmd = this.CreateCommand("Matching.usp_SEL_MatchSetMatchingSourceDataImportByMatchSetID");
                cmd.CommandType = CommandType.StoredProcedure;
                IDataParameterCollection cmdParams = cmd.Parameters;


                IDbDataParameter paramMatchSetID = cmd.CreateParameter();
                paramMatchSetID.ParameterName = "@MatchSetID";
                paramMatchSetID.Value = oMatchingParamInfo.MatchSetID;
                cmdParams.Add(paramMatchSetID);

                cmd.Connection = con;
                con.Open();
                reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                MatchSetMatchingSourceDataImportInfo oMatchSetMatchingSourceDataImportInfo = null;
                while (reader.Read())
                {
                    oMatchSetMatchingSourceDataImportInfo = this.MapObject(reader);
                    oMatchSetMatchingSourceDataImportInfoCollection.Add(oMatchSetMatchingSourceDataImportInfo);
                }
                reader.Close();
            }
            finally
            {

                if (reader != null && !reader.IsClosed)
                {
                    reader.Close();
                }

                if ((con != null) && (con.State == ConnectionState.Open))
                {
                    con.Dispose();
                }
            }
            return oMatchSetMatchingSourceDataImportInfoCollection;
        }


        protected override MatchSetMatchingSourceDataImportInfo MapObject(System.Data.IDataReader r)
        {
            MatchSetMatchingSourceDataImportInfo entity = new MatchSetMatchingSourceDataImportInfo();
            entity = base.MapObject(r);
            try
            {
                int ordinal = r.GetOrdinal("MatchingSourceTypeID");
                if (!r.IsDBNull(ordinal)) entity.MatchingSourceTypeID = ((System.Int16)(r.GetValue(ordinal)));
            }
            catch (Exception) { }
            return entity;
        }


        #region Update Match Set Matching Source
        /// <summary>
        /// Update Match Set Matching Source
        /// </summary>
        /// <param name="dtMatchSetMatchingSource">User defined table type</param>
        /// <returns>true/False</returns>
        public bool UpdateMatchSetMatchingSource(DataTable dtMatchSetMatchingSource, IDbConnection con, IDbTransaction oTransaction)
        {
            bool result = false;
            IDbCommand cmd = null;
            try
            {

                cmd = this.CreateCommand("Matching.usp_UDP_MatchSetMatchingSourceDataImport");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;
                cmd.Transaction = oTransaction;

                IDataParameterCollection cmdParams = cmd.Parameters;

                IDbDataParameter cmdMatchingSourceDataDT = cmd.CreateParameter();
                cmdMatchingSourceDataDT.ParameterName = "@dtMatchSetMatchingSourceDataImport";
                cmdMatchingSourceDataDT.Value = dtMatchSetMatchingSource;
                cmdParams.Add(cmdMatchingSourceDataDT);


                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                    result = true;
            }
            finally
            {
            }
            return result;
        }
        #endregion
    }
}