using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using SkyStem.ART.Client.Model;

namespace SkyStem.ART.Client.IServices
{
    // NOTE: If you change the interface name "IUnExpectedVariance" here, you must also update the reference to "IUnExpectedVariance" in Web.config.
    [ServiceContract]
    public interface IUnexplainedVariance
    {
        [OperationContract]
        List<GLDataUnexplainedVarianceInfo> GetGLDataUnexplainedVarianceInfoCollectionByGLDataID(long? gLDataID, AppUserInfo oAppUserInfo);

        [OperationContract]
        void InsertGLDataUnexplainedVariance(GLDataUnexplainedVarianceInfo GLDataUnexplainedVarianceInfo, int recPeriodID, AppUserInfo oAppUserInfo);

        [OperationContract]
        void UpdateGLDataUnexplainedVariance(GLDataUnexplainedVarianceInfo GLDataUnexplainedVarianceInfo, AppUserInfo oAppUserInfo);

        [OperationContract]
        void DeleteGLDataUnexplainedVariance(long? GLDataUnexplainedVarianceID, AppUserInfo oAppUserInfo);

        [OperationContract]
        List<GLDataUnexplainedVarianceInfo> GetUnExplainedVarianceHistoryInfoCollection(long? glDataID, AppUserInfo oAppUserInfo);
    }
}
