using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using SkyStem.ART.Client.IServices;
using SkyStem.ART.Client.Model;
using SkyStem.ART.App.DAO;
using System.Data.SqlClient;
using SkyStem.ART.App.Utility;
using SkyStem.ART.Client.Params;

namespace SkyStem.ART.App.Services
{
    // NOTE: If you change the class name "Subledger" here, you must also update the reference to "Subledger" in Web.config.
    public class Subledger : ISubledger
    {
        /// <summary>
        /// Selects all subledger source by company id
        /// </summary>
        /// <param name="companyID">Unique identifier of a company</param>
        /// <returns>List of subledger source</returns>
        public List<SubledgerSourceInfo> SelectAllByCompanyID(int companyID, AppUserInfo oAppUserInfo)
        {
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                SubledgerSourceDAO oSubledgerSourceDAO = new SubledgerSourceDAO(oAppUserInfo);
                return (List<SubledgerSourceInfo>)oSubledgerSourceDAO.SelectAllByCompanyID(companyID);
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


        public SubledgerDataInfo GetSubledgerDataInfoByAccountIDRecPeriodID(long? AccountID, int? RecPeriodID, AppUserInfo oAppUserInfo)
        {
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                SubledgerDataDAO oSubledgerDataDAO=new SubledgerDataDAO(oAppUserInfo);
                return oSubledgerDataDAO.GetSubledgerDataInfoByAccountIDRecPeriodID(AccountID,RecPeriodID);
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


        public List<SubledgerDataArchiveInfo> GetSubledgerVersionByGLDataID(GLDataParamInfo oGLDataParamInfo, AppUserInfo oAppUserInfo)
        {
            List<SubledgerDataArchiveInfo> oSubledgerDataArchiveInfoCollection = null;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                SubledgerDataDAO oSubledgerDataDAO = new SubledgerDataDAO(oAppUserInfo);
                oSubledgerDataArchiveInfoCollection = oSubledgerDataDAO.GetSubledgerVersionByGLDataID(oGLDataParamInfo);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return oSubledgerDataArchiveInfoCollection;
        }

        public SubledgerDataInfo GetSubledgerDataImportIDByNetAccountIDRecPeriodID(int? NetAccountID, int? RecPeriodID, AppUserInfo oAppUserInfo)
        {
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                SubledgerDataDAO oSubledgerDataDAO = new SubledgerDataDAO(oAppUserInfo);
                return oSubledgerDataDAO.GetSubledgerDataImportIDByNetAccountIDRecPeriodID(NetAccountID, RecPeriodID);
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

    }
}
