


using System;
using System.Data;
using System.Data.Common;
using Adapdev.Data;
using Adapdev.Data.Sql;
using SkyStem.ART.App.DAO.Matching.Base;
using System.Collections.Generic;
using SkyStem.ART.Client.Params.Matching;
using SkyStem.ART.Client.Model.Matching;
using SkyStem.ART.App.Utility;
using SkyStem.ART.App.DAO.Base;
using SkyStem.ART.Client.Model;

namespace SkyStem.ART.App.DAO.Matching
{
    public class MatchSetResultWorkspaceDAO : MatchSetResultWorkspaceDAOBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public MatchSetResultWorkspaceDAO(AppUserInfo oAppUserInfo) :
            base(oAppUserInfo)
        {
        }
        /// <summary>
        /// Creates the sql select command, using the passed in foreign key.  This will return an
        /// IList of all objects that have that foreign key.
        /// </summary>
        /// <param name="o">The foreign key of the objects to select</param>
        /// <returns>An IList</returns>
        public List<MatchSetResultWorkspaceInfo> GetMatchSetResultsWorkspace(MatchingParamInfo oMatchingParamInfo)
        {
            IDbConnection con = null;
            IDbCommand cmd = null;
            IDataReader rd = null;
            List<MatchSetResultWorkspaceInfo> oMatchSetResultWorkspaceInfoList = new List<MatchSetResultWorkspaceInfo>();
            try
            {
                cmd = CreateMatchSetResultsWorkspaceCommand(oMatchingParamInfo);
                con = CreateConnection();
                con.Open();
                cmd.Connection = con;
                rd = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (rd.Read())
                {
                    oMatchSetResultWorkspaceInfoList.Add(this.MapObject(rd));
                }
            }
            finally
            {
                if (con != null && con.State == ConnectionState.Open)
                    con.Dispose();
            }
            return oMatchSetResultWorkspaceInfoList;
        }

        protected override MatchSetResultWorkspaceInfo MapObject(IDataReader r)
        {
            MatchSetResultWorkspaceInfo oMatchSetResultWorkspaceInfo = base.MapObject(r);
            oMatchSetResultWorkspaceInfo.ResultSchema = r.GetStringValue("ResultSchema");
            return oMatchSetResultWorkspaceInfo;
        }

        private System.Data.IDbCommand CreateMatchSetResultsWorkspaceCommand(MatchingParamInfo oMatchingParamInfo)
        {
            System.Data.IDbCommand cmd = this.CreateCommand("[Matching].[usp_SEL_MatchSetResultsWorkspace]");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter par = cmd.CreateParameter();
            par.ParameterName = "@MatchSetID";
            par.Value = oMatchingParamInfo.MatchSetID;
            cmdParams.Add(par);
            return cmd;
        }

        internal bool UpdateMatchSetResults(MatchingParamInfo oMatchingParamInfo)
        {
            IDbConnection con = null;
            IDbCommand cmd = null;
            try
            {
                cmd = CreateUpdateMatchSetResultsCommand(oMatchingParamInfo);
                con = CreateConnection();
                con.Open();
                cmd.Connection = con;
                cmd.ExecuteNonQuery();
                return true;
            }
            finally
            {
                if (con != null && con.State == ConnectionState.Open)
                    con.Dispose();
            }
        }

        private IDbCommand CreateUpdateMatchSetResultsCommand(MatchingParamInfo oMatchingParamInfo)
        {
            IDbCommand cmd = this.CreateCommand("[Matching].[usp_UPD_MatchSetResultsWorkspace]");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter parMatchSetResulWorkspaceID = cmd.CreateParameter();
            parMatchSetResulWorkspaceID.ParameterName = "@MatchSetResultWorkspaceID";
            parMatchSetResulWorkspaceID.Value = oMatchingParamInfo.MatchSetResultWorkspaceInfo.MatchSetResultWorkspaceID;
            cmdParams.Add(parMatchSetResulWorkspaceID);

            System.Data.IDbDataParameter parMatchData = cmd.CreateParameter();
            parMatchData.ParameterName = "@MatchData";
            if (string.IsNullOrEmpty(oMatchingParamInfo.MatchSetResultWorkspaceInfo.MatchData))
                parMatchData.Value = DBNull.Value;
            else
                parMatchData.Value = oMatchingParamInfo.MatchSetResultWorkspaceInfo.MatchData;
            cmdParams.Add(parMatchData);

            System.Data.IDbDataParameter parWorkspaceMatchData = cmd.CreateParameter();
            parWorkspaceMatchData.ParameterName = "@WorkspaceMatchData";
            if (string.IsNullOrEmpty(oMatchingParamInfo.MatchSetResultWorkspaceInfo.WorkspaceMatchData))
                parWorkspaceMatchData.Value = DBNull.Value;
            else
                parWorkspaceMatchData.Value = oMatchingParamInfo.MatchSetResultWorkspaceInfo.WorkspaceMatchData;
            cmdParams.Add(parWorkspaceMatchData);

            System.Data.IDbDataParameter parPartialMatchData = cmd.CreateParameter();
            parPartialMatchData.ParameterName = "@PartialMatchData";
            if (string.IsNullOrEmpty(oMatchingParamInfo.MatchSetResultWorkspaceInfo.PartialMatchData))
                parPartialMatchData.Value = DBNull.Value;
            else
                parPartialMatchData.Value = oMatchingParamInfo.MatchSetResultWorkspaceInfo.PartialMatchData;
            cmdParams.Add(parPartialMatchData);

            System.Data.IDbDataParameter parWorkspacePartialMatchData = cmd.CreateParameter();
            parWorkspacePartialMatchData.ParameterName = "@WorkspacePartialMatchData";
            if (string.IsNullOrEmpty(oMatchingParamInfo.MatchSetResultWorkspaceInfo.WorkspacePartialMatchData))
                parWorkspacePartialMatchData.Value = DBNull.Value;
            else
                parWorkspacePartialMatchData.Value = oMatchingParamInfo.MatchSetResultWorkspaceInfo.WorkspacePartialMatchData;
            cmdParams.Add(parWorkspacePartialMatchData);

            System.Data.IDbDataParameter parUnmatchData = cmd.CreateParameter();
            parUnmatchData.ParameterName = "@UnmatchData";
            if (string.IsNullOrEmpty(oMatchingParamInfo.MatchSetResultWorkspaceInfo.UnmatchData))
                parUnmatchData.Value = DBNull.Value;
            else
                parUnmatchData.Value = oMatchingParamInfo.MatchSetResultWorkspaceInfo.UnmatchData;
            cmdParams.Add(parUnmatchData);

            System.Data.IDbDataParameter parWorkspaceUnmatchData = cmd.CreateParameter();
            parWorkspaceUnmatchData.ParameterName = "@WorkspaceUnmatchData";
            if (string.IsNullOrEmpty(oMatchingParamInfo.MatchSetResultWorkspaceInfo.WorkspaceUnmatchData))
                parWorkspaceUnmatchData.Value = DBNull.Value;
            else
                parWorkspaceUnmatchData.Value = oMatchingParamInfo.MatchSetResultWorkspaceInfo.WorkspaceUnmatchData;
            cmdParams.Add(parWorkspaceUnmatchData);

            System.Data.IDbDataParameter parDateRevised = cmd.CreateParameter();
            parDateRevised.ParameterName = "@DateRevised";
            parDateRevised.Value = oMatchingParamInfo.DateRevised;
            cmdParams.Add(parDateRevised);

            System.Data.IDbDataParameter parRevisedBy = cmd.CreateParameter();
            parRevisedBy.ParameterName = "@RevisedBy";
            parRevisedBy.Value = oMatchingParamInfo.RevisedBy;
            cmdParams.Add(parRevisedBy);
            return cmd;
        }
    }
}