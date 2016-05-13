


using System;
using System.Data;
using System.Data.Common;
using Adapdev.Data;
using Adapdev.Data.Sql;
using SkyStem.ART.App.DAO.Matching.Base;
using SkyStem.ART.App.DAO.Base;
using SkyStem.ART.Client.Model.Matching;
using System.Collections.Generic;
using SkyStem.ART.App.Utility;
using SkyStem.ART.Client.Params.Matching;
using SkyStem.ART.Client.Model;

namespace SkyStem.ART.App.DAO.Matching
{
    public class MatchSetGLDataRecItemDAO : MatchSetGLDataRecItemDAOBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public MatchSetGLDataRecItemDAO(AppUserInfo oAppUserInfo) :
            base(oAppUserInfo)
        {
        }
        internal List<MatchSetGLDataRecItemInfo> GetMatchSetGLDataRecItems(List<long> MatchingSourceDataImportIDList)
        {
            List<MatchSetGLDataRecItemInfo> oMatchSetGLDataRecItemInfoList = new List<MatchSetGLDataRecItemInfo>();
            IDbConnection con = null;
            IDbCommand cmd = null;
            try
            {
                cmd = CreateGetMatchSetGLDataRecItemsCommand(MatchingSourceDataImportIDList);
                con = CreateConnection();
                cmd.Connection = con;
                con.Open();

                IDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (reader.Read())
                {
                    oMatchSetGLDataRecItemInfoList.Add(this.MapObject(reader));
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

            return oMatchSetGLDataRecItemInfoList;
        }

        protected override MatchSetGLDataRecItemInfo MapObject(System.Data.IDataReader r)
        {
            MatchSetGLDataRecItemInfo entity = base.MapObject(r);
            entity.MatchingSourceDataImportID = r.GetInt64Value("MatchingSourceDataImportID");
            entity.CloseDate = r.GetDateValue("CloseDate");
            return entity;
        }

        private IDbCommand CreateGetMatchSetGLDataRecItemsCommand(List<long> MatchingSourceDataImportIDList)
        {
            IDbCommand cmd = this.CreateCommand("Matching.usp_SEL_MatchSetGLDataRecItem");
            cmd.CommandType = CommandType.StoredProcedure;
            IDataParameterCollection cmdParams = cmd.Parameters;

            IDbDataParameter paramMatchSetSubSetCombinationID = cmd.CreateParameter();
            paramMatchSetSubSetCombinationID.ParameterName = "@IDTableMatchingSourceDataImportID";
            paramMatchSetSubSetCombinationID.Value = ServiceHelper.ConvertLongIDCollectionToDataTable(MatchingSourceDataImportIDList);
            cmdParams.Add(paramMatchSetSubSetCombinationID);

            return cmd;
        }
    }
}