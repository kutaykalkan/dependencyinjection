 


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
    public class RiskRatingReconciliationPeriodDAO : RiskRatingReconciliationPeriodDAOBase
    {
                /// <summary>
        /// Constructor
        /// </summary>
        public RiskRatingReconciliationPeriodDAO(AppUserInfo oAppUserInfo) :
            base(oAppUserInfo)
        {
        }
        public List<RiskRatingReconciliationPeriodInfo> SelectAllRiskRatingReconciliationPeriodByRiskRatingIDAndReconciliationPeriodID(int? reconciliationPeriodID, short? riskRatingID)
        {
            System.Data.IDbCommand cmd = this.CreateCommand("usp_SEL_RiskRatingReconciliationPeriodByRiskRatingIDAndRecPeriodID");
            cmd.CommandType = CommandType.StoredProcedure;

            IDataParameterCollection cmdParams = cmd.Parameters;

            System.Data.IDbDataParameter parReconciliationPeriodID = cmd.CreateParameter();
            parReconciliationPeriodID.ParameterName = "@ReconciliationPeriodID";
            parReconciliationPeriodID.Value = reconciliationPeriodID;
            cmdParams.Add(parReconciliationPeriodID);

            System.Data.IDbDataParameter parRiskRatingID = cmd.CreateParameter();
            parRiskRatingID.ParameterName = "@RiskRatingID";
            parRiskRatingID.Value = riskRatingID;
            cmdParams.Add(parRiskRatingID);

            cmd.Connection = this.CreateConnection();
            cmd.Connection.Open();

            IDataReader dr = null;
            dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            List<RiskRatingReconciliationPeriodInfo> oRiskRatingReconciliationPeriodInfoCollection = new List<RiskRatingReconciliationPeriodInfo>();
            oRiskRatingReconciliationPeriodInfoCollection  = MapObjects(dr);
            dr.ClearColumnHash();
            return oRiskRatingReconciliationPeriodInfoCollection;
        }

    }//end of class
}//end of namespace