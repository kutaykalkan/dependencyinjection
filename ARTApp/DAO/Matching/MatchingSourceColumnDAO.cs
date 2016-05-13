 


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
    public class MatchingSourceColumnDAO : MatchingSourceColumnDAOBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public MatchingSourceColumnDAO(AppUserInfo oAppUserInfo) :
            base(oAppUserInfo)
        {
        }
        #region GetMatchingSourceColumn
        /// <summary>
        /// Get Matching Source Column info in the database
        /// </summary>
        /// <param name="oMatchingParamInfo">Param info hold MatchingSourceDataImportID</param>
        /// <returns></returns>
        public List<MatchingSourceColumnInfo> GetMatchingSourceColumn(MatchingParamInfo oMatchingParamInfo)
        {
            List<MatchingSourceColumnInfo> oMatchingSourceColumnInfoCollection = new List<MatchingSourceColumnInfo>();
            IDbCommand cmd = null;
            IDbConnection con = null;
            try
            {
                con = this.CreateConnection();
                cmd = this.CreateCommand("Matching.usp_SEL_MatchingSourceColumn");

                cmd.CommandType = CommandType.StoredProcedure;

                IDataParameterCollection cmdParams = cmd.Parameters;

                IDbDataParameter paramMatchingSourceDataImportID = cmd.CreateParameter();
                paramMatchingSourceDataImportID.ParameterName = "@MatchingSourceDataImportID";
                paramMatchingSourceDataImportID.Value = oMatchingParamInfo.MatchingSourceDataImportID;
                cmdParams.Add(paramMatchingSourceDataImportID);

                cmd.Connection = con;
                con.Open();
                IDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                MatchingSourceColumnInfo oMatchingSourceColumnInfo = null;
                while (reader.Read())
                {
                    oMatchingSourceColumnInfo = this.MapObject(reader);
                    oMatchingSourceColumnInfoCollection.Add(oMatchingSourceColumnInfo);
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

            return oMatchingSourceColumnInfoCollection;
        }
        #endregion

        #region SaveMatchingSourceColumn
        /// <summary>
        /// Save Matching Source Column info in the database
        /// </summary>
        /// <param name="dtMatchingSourceColumn"></param>
        /// <returns></returns>
        public bool SaveMatchingSourceColumn(DataTable dtMatchingSourceColumn, IDbConnection con, IDbTransaction oTransaction)
        {
            bool result = false;
            IDbCommand cmd = null;
            try
            {
                cmd = this.CreateCommand("Matching.usp_INS_MatchingSourceColumn");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;
                cmd.Transaction = oTransaction;

                IDataParameterCollection cmdParams = cmd.Parameters;

                IDbDataParameter paramMatchingSourceColumn = cmd.CreateParameter();
                paramMatchingSourceColumn.ParameterName = "@dtMatchingSourceColumn";
                paramMatchingSourceColumn.Value = dtMatchingSourceColumn;
                cmdParams.Add(paramMatchingSourceColumn);

                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                    result = true;

            }
            finally
            { }

            return result;
        }
        #endregion


        #region GetMatchingSourceColumnForMatchSet
        /// <summary>
        /// Get Matching Source Column info in the database
        /// </summary>
        /// <param name="oMatchingParamInfo">Param info hold MatchingSourceDataImportID</param>
        /// <returns></returns>
        public List<MatchingSourceColumnInfo> GetMatchingSourceColumnForMatchSet(DataTable dtMatchingSourceDataImportIDs)
        {
            List<MatchingSourceColumnInfo> oMatchingSourceColumnInfoCollection = new List<MatchingSourceColumnInfo>();
            IDbCommand cmd = null;
            IDbConnection con = null;
            try
            {
                con = this.CreateConnection();
                cmd = this.CreateCommand("Matching.usp_SEL_MatchingSourceColumnForMatchSet");

                cmd.CommandType = CommandType.StoredProcedure;

                IDataParameterCollection cmdParams = cmd.Parameters;

                IDbDataParameter paramMatchingSourceDataImportIDs = cmd.CreateParameter();
                paramMatchingSourceDataImportIDs.ParameterName = "@dtMatchingSourceDataImportID";
                paramMatchingSourceDataImportIDs.Value = dtMatchingSourceDataImportIDs;
                cmdParams.Add(paramMatchingSourceDataImportIDs);

                cmd.Connection = con;
                con.Open();
                IDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                MatchingSourceColumnInfo oMatchingSourceColumnInfo = null;
                while (reader.Read())
                {
                    oMatchingSourceColumnInfo = this.MapObject(reader);
                    oMatchingSourceColumnInfoCollection.Add(oMatchingSourceColumnInfo);
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

            return oMatchingSourceColumnInfoCollection;
        }
        #endregion

       

    }
}