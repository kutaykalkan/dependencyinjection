


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
    public class MatchingConfigurationRuleDAO : MatchingConfigurationRuleDAOBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public MatchingConfigurationRuleDAO(AppUserInfo oAppUserInfo) :
            base(oAppUserInfo)
        {
        }
        #region SaveMatchingConfiguration
        /// <summary>
        /// Save MatchingConfigurationRule in the database
        /// </summary>
        /// <param name="dtMatchingConfiguration">User defined table type</param>
        /// <returns>Inserted MatchingConfigurationRuleInfo List</returns>
        public List<MatchingConfigurationRuleInfo> SaveMatchingConfigurationRule(DataTable dtMatchingConfigurationRule)
        {
            List<MatchingConfigurationRuleInfo> oMatchingConfigurationRuleInfoCollection = new List<MatchingConfigurationRuleInfo>();
            IDbCommand cmd = null;
            IDbConnection con = null;
            try
            {
                con = this.CreateConnection();
                cmd = this.CreateCommand("Matching.usp_INS_MatchingConfigurationRule");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;

                IDataParameterCollection cmdParams = cmd.Parameters;

                IDbDataParameter cmdMatchingConfigurationRuleDataTable = cmd.CreateParameter();
                cmdMatchingConfigurationRuleDataTable.ParameterName = "@dtMatchingConfigurationRule";
                cmdMatchingConfigurationRuleDataTable.Value = dtMatchingConfigurationRule;
                cmdParams.Add(cmdMatchingConfigurationRuleDataTable);

                con.Open();
                oMatchingConfigurationRuleInfoCollection = this.Select(cmd);
            }
            finally
            {
                if ((con != null) && (con.State == ConnectionState.Open))
                {
                    con.Close();
                    con.Dispose();
                }
            }
            return oMatchingConfigurationRuleInfoCollection;
        }

        #endregion

        public List<MatchingConfigurationRuleInfo> GetAllMatchingConfigurationRule(MatchingParamInfo oMatchingParamInfo)
        {
            List<MatchingConfigurationRuleInfo> oMatchingConfigurationRuleInfoCollection = new List<MatchingConfigurationRuleInfo>();
            IDbConnection con = null;
            IDbCommand cmd = null;
            try
            {
                con = this.CreateConnection();
                cmd = this.CreateCommand("Matching.usp_SEL_MatchingConfigurationRule");
                cmd.CommandType = CommandType.StoredProcedure;
                IDataParameterCollection cmdParams = cmd.Parameters;

                IDbDataParameter paramMatchingConfigurationID = cmd.CreateParameter();
                paramMatchingConfigurationID.ParameterName = "@IDTableMatchingConfigurationID";
                paramMatchingConfigurationID.Value = ServiceHelper.ConvertLongIDCollectionToDataTable(oMatchingParamInfo.IDList);
                cmdParams.Add(paramMatchingConfigurationID);

                cmd.Connection = con;
                con.Open();

                IDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                MatchingConfigurationRuleInfo oMatchingConfigurationRuleInfo = null;
                while (reader.Read())
                {
                    oMatchingConfigurationRuleInfo = this.MapObject(reader);
                    oMatchingConfigurationRuleInfoCollection.Add(oMatchingConfigurationRuleInfo);
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

            return oMatchingConfigurationRuleInfoCollection;
        }

    }
}