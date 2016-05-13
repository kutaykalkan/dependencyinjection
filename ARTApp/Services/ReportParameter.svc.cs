using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using SkyStem.ART.Client.IServices;
using SkyStem.ART.Client.Model;
using SkyStem.ART.App.DAO;
using SkyStem.ART.Client.Params;
using SkyStem.ART.App.Utility;

namespace SkyStem.ART.App.Services
{
    // NOTE: If you change the class name "ReportParameter" here, you must also update the reference to "ReportParameter" in Web.config.
    public class ReportParameter : IReportParameter
    {
        public void DoWork()
        {
        }

        #region IReportParameter Members


        public IList<ReportParameterInfo> GetReportParametersByReportID(ReportParameterParamInfo oReportParameterParamInfo, AppUserInfo oAppUserInfo)
        {
            ServiceHelper.SetConnectionString(oAppUserInfo);
            ReportParameterDAO oReportParameterDAO = new ReportParameterDAO(oAppUserInfo);
            return oReportParameterDAO.SelectAllByReportID(oReportParameterParamInfo);
        }


        public List<short> GetPermittedRolesByReportID(short? reportID, short? currentUserRole, int? RecPeriodID, int? companyID, AppUserInfo oAppUserInfo)
        {
            ServiceHelper.SetConnectionString(oAppUserInfo);
            ReportParameterDAO oReportParameterDAO = new ReportParameterDAO(oAppUserInfo);
            return oReportParameterDAO.GetPermittedRolesByReportID(reportID, currentUserRole, RecPeriodID, companyID );
        }
        #endregion
    }
}
