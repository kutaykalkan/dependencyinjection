using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using SkyStem.ART.Client.Model;
using SkyStem.ART.Client.Model.MappingUpload;
using SkyStem.ART.Client.Params.MappingUpload;

namespace SkyStem.ART.Client.IServices
{
    [ServiceContract]
    public interface IMappingUpload
    {
        [OperationContract]
        List<MappingUploadInfo> GetMappingUploadInfoList(int? ReconciliationPeriodID, int? CompanyID, AppUserInfo oAppUserInfo);

        [OperationContract]
        List<MappingUploadMasterInfo> GetAllMappingUploadInfoList( AppUserInfo oAppUserInfo);

        [OperationContract]
        bool SaveMappingUploadInfoList(MappingUploadParamInfo oMappingUploadInfo, AppUserInfo oAppUserInfo);
    }
}
