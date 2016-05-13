using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using SkyStem.ART.Client.IServices;
using SkyStem.ART.App.DAO;
using SkyStem.ART.Client.Model;
using System.Data.SqlClient;
using SkyStem.ART.App.Utility;

namespace SkyStem.ART.App.Services
{
    // NOTE: If you change the class name "Reconciliation" here, you must also update the reference to "Reconciliation" in Web.config.
    public class Reconciliation : IReconciliation
    {
        public List<ReconciliationTemplateMstInfo> SelectAllReconciliationTemplateMstInfo(AppUserInfo oAppUserInfo)
        {
            List<ReconciliationTemplateMstInfo> oReconciliationTemplateMstInfoCollection = null;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                ReconciliationTemplateMstDAO oReconciliationTemplateMstDAO = new ReconciliationTemplateMstDAO(oAppUserInfo);
                return oReconciliationTemplateMstDAO.GetAllReconciliationTemplateMstInfo();
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return oReconciliationTemplateMstInfoCollection;
        }
    }
}
