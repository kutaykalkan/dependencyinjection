using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using SkyStem.ART.Client;
using SkyStem.ART.Client.Model;

namespace SkyStem.ART.Client.IServices
{
    [ServiceContract]
    public interface IPackage
    {
        [OperationContract]
        PackageMstInfo GetComapanyPackageInfo(int CompanyId, AppUserInfo oAppUserInfo);
        [OperationContract]
        List<FeatureMstInfo> GetMasterFeatureList( AppUserInfo oAppUserInfo);
        [OperationContract]
        List<PackageFeatureInfo> GetPackageFeatureList( AppUserInfo oAppUserInfo);
        [OperationContract]
        List<PackageMstInfo> GetAllPackage( AppUserInfo oAppUserInfo);
        [OperationContract]
        string GetFeaturesPackageAvailabilityMatrix( AppUserInfo oAppUserInfo);
    }
}
