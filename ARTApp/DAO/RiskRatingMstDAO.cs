


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
    public class RiskRatingMstDAO : RiskRatingMstDAOBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public RiskRatingMstDAO(AppUserInfo oAppUserInfo) :
            base(oAppUserInfo)
        {
        }
        /// <summary>
        /// Selects all risk rating defined for the company
        /// </summary>
        /// <returns>List of risk rating</returns>
        public IList<RiskRatingMstInfo> SelectAllRiskRatingMstInfo()
        {
            IList<RiskRatingMstInfo> oRiskRatingMstDAOCollection = new List<RiskRatingMstInfo>();
            IDbConnection oConnection = null;
            IDbCommand oCommand = null;

            try
            {
                oConnection = this.CreateConnection();
                oCommand = this.CreateCommand("usp_SEL_AllRiskRatingMstInfo");
                oCommand.CommandType = CommandType.StoredProcedure;
                oCommand.Connection = oConnection;
                oConnection.Open();

                IDataReader reader = oCommand.ExecuteReader(CommandBehavior.CloseConnection);

                while (reader.Read())
                {
                    oRiskRatingMstDAOCollection.Add(this.MapObject(reader));
                }
            }
            finally
            {
                if ((oConnection != null) && (oConnection.State != ConnectionState.Closed))
                    oConnection.Dispose();
            }

            return oRiskRatingMstDAOCollection;
        }
    }
}