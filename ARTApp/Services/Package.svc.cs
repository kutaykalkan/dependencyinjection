using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using SkyStem.ART.Client.IServices;
using SkyStem.ART.Client.Model;
using SkyStem.ART.App.DAO;
using System.Data;
using SkyStem.ART.App.Utility;
using System.Data.SqlClient;
using SkyStem.ART.Client.Exception;

namespace SkyStem.ART.App.Services
{
    // NOTE: If you change the class name "Package" here, you must also update the reference to "Package" in Web.config.
    public class Package : IPackage
    {
        public PackageMstInfo GetComapanyPackageInfo(int CompanyId, AppUserInfo oAppUserInfo)
        {
            ServiceHelper.SetConnectionString(oAppUserInfo);
            PackageMstInfo oPackageMstInfo = null;
            //DAO Call comes here
            PackageMstDAO oPackageMstDAO = new PackageMstDAO(oAppUserInfo);
            
            return oPackageMstInfo;
        }
        public List<FeatureMstInfo> GetMasterFeatureList( AppUserInfo oAppUserInfo)
        {
            List<FeatureMstInfo> lstFeatureMstInfo = null;
            ServiceHelper.SetConnectionString(oAppUserInfo);
            //DAO Call comes here
            return lstFeatureMstInfo;
        }
        public List<PackageFeatureInfo> GetPackageFeatureList( AppUserInfo oAppUserInfo)
        {
            List<PackageFeatureInfo> lstPackageFeatureInfo = null;
            //DAO Call comes here
            return lstPackageFeatureInfo;
        }

        /// <summary>
        /// GetAllPackage() is used to get all packages from DB.
        /// </summary>
        /// <returns></returns>
        public List<PackageMstInfo> GetAllPackage(AppUserInfo oAppUserInfo)
        {
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                PackageMstDAO oPackageMstDAO = new PackageMstDAO(oAppUserInfo);
                return oPackageMstDAO.GetAllPackage();
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return null;
        }

        public string GetFeaturesPackageAvailabilityMatrix( AppUserInfo oAppUserInfo)
        {
            ServiceHelper.SetConnectionString(oAppUserInfo);
             PackageMstDAO oPackageMstDAO = new PackageMstDAO(oAppUserInfo);
             return oPackageMstDAO.GetFeaturesPackageAvailabilityMatrix();
        }
    }
}
