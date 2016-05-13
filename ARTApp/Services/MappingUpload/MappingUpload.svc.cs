using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using SkyStem.ART.Client.IServices;
using SkyStem.ART.Client.Model;
using SkyStem.ART.App.DAO.MappingUpload;
using System.Data.SqlClient;
using SkyStem.ART.App.Utility;
using SkyStem.ART.Client.Params.MappingUpload;
using SkyStem.ART.Client.Model.MappingUpload;


namespace SkyStem.ART.App.Services
{
    // NOTE: If you change the class name "MappingUpload" here, you must also update the reference to "MappingUpload" in Web.config.
    public class MappingUpload : IMappingUpload
    {

        #region IMappingUpload Members


        public bool SaveMappingUploadInfoList(MappingUploadParamInfo oMappingUploadParamInfo, AppUserInfo oAppUserInfo)
        {
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                MappingUploadDAO oMappingUploadDAO = new MappingUploadDAO(oAppUserInfo);
                string returnValue = oMappingUploadDAO.SaveMappingUploadInfoList(oMappingUploadParamInfo.MappingUploadInfoList,
                    oMappingUploadParamInfo.RecPeriodID, oMappingUploadParamInfo.CompanyID, oMappingUploadParamInfo.UserLoginID,
                    oMappingUploadParamInfo.DateRevised, oMappingUploadParamInfo.UserID);
                if (string.IsNullOrEmpty(returnValue))
                    return true;
                else
                    return false;
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return false;
        }

        #endregion

        public List<MappingUploadMasterInfo> GetAllMappingUploadInfoList( AppUserInfo oAppUserInfo)
        {
            List<MappingUploadMasterInfo> oMappingUploadMstInfoList = null;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                MappingUploadDAO oMappingUploadDAO = new MappingUploadDAO(oAppUserInfo);
                oMappingUploadMstInfoList = oMappingUploadDAO.GetAllMappingUploadInfoList();
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return oMappingUploadMstInfoList;
        }

        #region IMappingUpload Members

        public List<MappingUploadInfo> GetMappingUploadInfoList(int? ReconciliationPeriodID, int? CompanyID, AppUserInfo oAppUserInfo)
        {
            List<MappingUploadInfo> oMappingUploadInfoList = null;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                MappingUploadDAO oMappingUploadDAO = new MappingUploadDAO(oAppUserInfo);
                oMappingUploadInfoList = oMappingUploadDAO.GetMappingUploadInfoList(ReconciliationPeriodID,CompanyID);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return oMappingUploadInfoList;
        }

        #endregion
    }
}
