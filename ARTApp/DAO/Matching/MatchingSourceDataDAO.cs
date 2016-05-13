using System;
using System.Data;
using System.Data.Common;
using Adapdev.Data;
using Adapdev.Data.Sql;
using SkyStem.ART.App.DAO.Matching.Base;
using SkyStem.ART.Client.Model.Matching;
using SkyStem.ART.Client.Params.Matching;
using System.Collections.Generic;
using SkyStem.ART.Client.Model;

namespace SkyStem.ART.App.DAO.Matching
{
    public class MatchingSourceDataDAO : MatchingSourceDataDAOBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public MatchingSourceDataDAO(AppUserInfo oAppUserInfo) :
            base(oAppUserInfo)
        {
        }
        public List<MatchingSourceDataInfo> GetMatchingSourceData(MatchingParamInfo oMatchingParamInfo)
        {
            List<MatchingSourceDataInfo> oMatchingSourceDataInfoList = null;
            MatchingSourceDataInfo oMatchingSourceDataInfo = null;
            IDbConnection con = null;
            IDbCommand cmd = null;
            try
            {
                con = this.CreateConnection();
                cmd = this.CreateCommand("Matching.usp_SEL_MatchingSourceData");
                cmd.CommandType = CommandType.StoredProcedure;
                IDataParameterCollection cmdParams = cmd.Parameters;

                IDbDataParameter paramMatchSetID = cmd.CreateParameter();
                paramMatchSetID.ParameterName = "@MatchSetID";
                paramMatchSetID.Value = oMatchingParamInfo.MatchSetID;
                cmdParams.Add(paramMatchSetID);

                IDbDataParameter paramAccountID = cmd.CreateParameter();
                paramAccountID.ParameterName = "@AccountID";
                if (oMatchingParamInfo.AccountID != null)
                    paramAccountID.Value = oMatchingParamInfo.AccountID;
                else
                    paramAccountID.Value = 0;

                cmdParams.Add(paramAccountID);

                cmd.Connection = con;
                con.Open();
                IDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (reader.Read())
                {
                    oMatchingSourceDataInfo = this.MapObject(reader);
                    MapMatchingSourceData(reader, oMatchingSourceDataInfo);
                    if (oMatchingSourceDataInfoList == null)
                        oMatchingSourceDataInfoList = new List<MatchingSourceDataInfo>();
                    oMatchingSourceDataInfoList.Add(oMatchingSourceDataInfo);
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
            return oMatchingSourceDataInfoList;
        }

        public void MapMatchingSourceData(System.Data.IDataReader r, MatchingSourceDataInfo oMatchingSourceDataInfo)
        {
            //oMatchingSourceDataInfo.IsMatchingSourceAccount = r.GetBooleanValue("IsMatchingSourceAccount");
        }

    }
}