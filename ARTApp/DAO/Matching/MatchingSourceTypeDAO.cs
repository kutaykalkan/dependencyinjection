


using System;
using System.Data;
using System.Data.Common;
using Adapdev.Data;
using Adapdev.Data.Sql;
using SkyStem.ART.App.DAO.Matching.Base;
using System.Collections.Generic;
using SkyStem.ART.Client.Model.Matching;
using SkyStem.ART.Client.Params.Matching;
using SkyStem.ART.Client.Model;

namespace SkyStem.ART.App.DAO.Matching
{
    public class MatchingSourceTypeDAO : MatchingSourceTypeDAOBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public MatchingSourceTypeDAO(AppUserInfo oAppUserInfo) :
            base(oAppUserInfo)
        {
        }
        public List<MatchingSourceTypeInfo> GetAllMatchingSourceType()
        {
            List<MatchingSourceTypeInfo> oMatchingSourceTypeInfoCollection = new List<MatchingSourceTypeInfo>();
            IDbConnection con = null;
            IDbCommand cmd = null;

            try
            {
                con = this.CreateConnection();
                cmd = this.CreateCommand("Matching.usp_SEL_MatchingSourceType");

                cmd.CommandType = CommandType.StoredProcedure;

                //IDataParameterCollection cmdParams = cmd.Parameters;

                //IDbDataParameter paramUserID = cmd.CreateParameter();
                //paramUserID.ParameterName = "@UserID";
                //paramUserID.Value = oMatchSourceDataImportParamInfo.UserID;
                //cmdParams.Add(paramUserID);

                //IDbDataParameter paramRoleID = cmd.CreateParameter();
                //paramRoleID.ParameterName = "@RoleID";
                //paramRoleID.Value = oMatchSourceDataImportParamInfo.RoleID;
                //cmdParams.Add(paramRoleID);

                cmd.Connection = con;
                con.Open();
                IDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                MatchingSourceTypeInfo oMatchingSourceTypeInfo = null;
                while (reader.Read())
                {
                    oMatchingSourceTypeInfo = this.MapObject(reader);
                    oMatchingSourceTypeInfoCollection.Add(oMatchingSourceTypeInfo);
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

            return oMatchingSourceTypeInfoCollection;
        }
    }
}