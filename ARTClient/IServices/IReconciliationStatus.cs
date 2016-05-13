using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using SkyStem.ART.Client.Model;
using System.Data;
using SkyStem.ART.Client.Data;

namespace SkyStem.ART.Client.IServices
{
    // NOTE: If you change the interface name "IReconciliationStatus" here, you must also update the reference to "IReconciliationStatus" in Web.config.
    [ServiceContract]
    public interface IReconciliationStatus
    {
        [OperationContract]
        void DoWork();

        [OperationContract]
        List<ReconciliationStatusMstInfo> GetAllReconciliationStatus(int? companyID, int? recPeriodID, AppUserInfo oAppUserInfo);
    }
}
