using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using SkyStem.ART.Client.Model;
using SkyStem.ART.Client.Params;

namespace SkyStem.ART.Client.IServices
{
    // NOTE: If you change the interface name "IReportParameter" here, you must also update the reference to "IReportParameter" in Web.config.
    [ServiceContract]
    public interface IReportParameter
    {
        [OperationContract]
        void DoWork();

        [OperationContract]
        IList<ReportParameterInfo> GetReportParametersByReportID(ReportParameterParamInfo oReportParameterParamInfo, AppUserInfo oAppUserInfo);

        [OperationContract]
        List<short> GetPermittedRolesByReportID(short? reportID, short? currentUserRole, int? RecPeriodID, int? companyID, AppUserInfo oAppUserInfo);
    }
}
