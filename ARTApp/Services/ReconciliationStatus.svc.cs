using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using SkyStem.ART.Client.Model;
using SkyStem.ART.App.DAO;
using System.Data.SqlClient;
using SkyStem.ART.App.Utility;
using SkyStem.ART.Client.IServices;

namespace SkyStem.ART.App.Services
{
    // NOTE: If you change the class name "ReconciliationStatus" here, you must also update the reference to "ReconciliationStatus" in Web.config.
    public class ReconciliationStatus : IReconciliationStatus
    {
        public void DoWork()
        {
        }

        public List<ReconciliationStatusMstInfo> GetAllReconciliationStatus(int? companyID, int? recPeriodID, AppUserInfo oAppUserInfo)
        {
            ReconciliationStatusMstDAO oRecStatusDAO = null;
            List<ReconciliationStatusMstInfo> oRecStatusCollection = null;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                oRecStatusDAO = new ReconciliationStatusMstDAO(oAppUserInfo);
                oRecStatusCollection = oRecStatusDAO.GetAllRecStatus(companyID,recPeriodID);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return oRecStatusCollection;
        }
    }
}
