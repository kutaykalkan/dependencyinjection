using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using  SkyStem.ART.Client.Model;

namespace SkyStem.ART.Client.IServices
{
    // NOTE: If you change the interface name "IReconciliationCategory" here, you must also update the reference to "IReconciliationCategory" in Web.config.
    [ServiceContract]
    public interface IReconciliationCategory
    {
        [OperationContract]
        void DoWork();

        [OperationContract]
        IList<ReconciliationCategoryMstInfo> GetOpenItemClassification( AppUserInfo oAppUserInfo); 
    }
}
