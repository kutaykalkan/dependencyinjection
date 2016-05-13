using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using SkyStem.ART.Client.Model;

namespace SkyStem.ART.Client.IServices
{
    // NOTE: If you change the interface name "IRiskRating" here, you must also update the reference to "IRiskRating" in Web.config.
    [ServiceContract]
    public interface IRiskRating
    {
        [OperationContract]
        void DoWork();

        [OperationContract]
        List<RiskRatingReconciliationPeriodInfo> SelectAllRiskRatingReconciliationPeriodByRiskRatingIDAndReconciliationPeriodID(int? reconciliationPeriodID, short? riskRatingID, AppUserInfo oAppUserInfo);

        /// <summary>
        /// Selects all risk rating defined for the company
        /// </summary>
        /// <returns>List of risk rating</returns>
        [OperationContract]
        List<RiskRatingMstInfo> SelectAllRiskRatingMstInfo( AppUserInfo oAppUserInfo);

        [OperationContract]
        IList<RiskRatingReconciliationFrequencyInfo> SelectAllRiskRatingReconciliationFrequencyByReconciliationPeriodID(int? reconciliationPeriodID, AppUserInfo oAppUserInfo);

       
    }//end of interface
}//end of namespace
