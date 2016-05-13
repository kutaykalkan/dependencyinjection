using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using SkyStem.ART.Client.Model.QualityScore;
using SkyStem.ART.Client.Params.QualityScore;
using SkyStem.ART.Client.Model;
using SkyStem.ART.Client.Data;

namespace SkyStem.ART.Client.IServices
{
    [ServiceContract]
    public interface IQualityScore
    {

        [OperationContract]
        List<QualityScoreStatusMstInfo> GetAllQualityScoreStatuses( AppUserInfo oAppUserInfo);

        [OperationContract]
        List<CompanyQualityScoreInfo> GetCompanyQualityScoreInfoList(QualityScoreParamInfo oQualityScoreParamInfo, AppUserInfo oAppUserInfo);

        [OperationContract]
        bool SaveCompanyQualityScoreInfoList(QualityScoreParamInfo oQualityScoreParamInfo, AppUserInfo oAppUserInfo);

        [OperationContract]
        List<GLDataQualityScoreInfo> GetGLDataQualityScoreInfoList(QualityScoreParamInfo oQualityScoreParamInfo, AppUserInfo oAppUserInfo);

        [OperationContract]
        bool SaveGLDataQualityScoreInfoList(QualityScoreParamInfo oQualityScoreParamInfo, AppUserInfo oAppUserInfo);

        [OperationContract]
        Dictionary<ARTEnums.QualityScoreType, Int32?> GetGLDataQualityScoreCount(QualityScoreParamInfo oQualityScoreParamInfo, AppUserInfo oAppUserInfo);
    }
}
