using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using SkyStem.ART.Client.Model;

namespace SkyStem.ART.Client.IServices
{
    // NOTE: If you change the interface name "IOperator" here, you must also update the reference to "IOperator" in Web.config.
    [ServiceContract]
    public interface IOperator
    {
        [OperationContract]
        void DoWork();

        [OperationContract]
        List<OperatorMstInfo> GetOperatorsByColumnID(short columnID, AppUserInfo oAppUserInfo);

        List<OperatorMstInfo> GetOperatorsByDynamicColumnID(short selectedValue, AppUserInfo appUserInfo);

        List<OperatorMstInfo> GetOperatorList(AppUserInfo appUserInfo);
    }
}
