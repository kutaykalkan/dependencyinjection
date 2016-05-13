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

namespace SkyStem.ART.App.Services
{
    // NOTE: If you change the class name "ReportParameterKey" here, you must also update the reference to "ReportParameterKey" in Web.config.
    public class ReportParameterKey : IReportParameterKey
    {
        public void DoWork()
        {
        }

        #region IReportParameterKey Members


        public IList<ReportParameterKeyMstInfo> GetAllReportParameterKeys( AppUserInfo oAppUserInfo)
        {
            ReportParameterKeyMstDAO oRptParamKeyDAO = null;
            IList<ReportParameterKeyMstInfo> oRptParamKeyInfoCollection = null;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                oRptParamKeyDAO = new ReportParameterKeyMstDAO(oAppUserInfo);
                oRptParamKeyInfoCollection = oRptParamKeyDAO.GetAllReportParamKeys();
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return oRptParamKeyInfoCollection;

        }

        #endregion
    }
}
