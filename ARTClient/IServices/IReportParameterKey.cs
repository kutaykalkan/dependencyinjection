using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using SkyStem.ART.Client.Model;

namespace SkyStem.ART.Client.IServices
{
    // NOTE: If you change the interface name "IReportParameterKey" here, you must also update the reference to "IReportParameterKey" in Web.config.
    [ServiceContract]
    public interface IReportParameterKey
    {
        [OperationContract]
        void DoWork();

        [OperationContract]
        IList<ReportParameterKeyMstInfo> GetAllReportParameterKeys( AppUserInfo oAppUserInfo);
    }
}
