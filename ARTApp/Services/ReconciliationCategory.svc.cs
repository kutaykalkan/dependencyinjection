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
    // NOTE: If you change the class name "ReconciliationCategory" here, you must also update the reference to "ReconciliationCategory" in Web.config.
    public class ReconciliationCategory : IReconciliationCategory
    {
        public void DoWork()
        {
        }
        public IList<ReconciliationCategoryMstInfo> GetOpenItemClassification(AppUserInfo oAppUserInfo)
        {
            ReconciliationCategoryMstDAO oRecCategoryDAO = null;
            IList<ReconciliationCategoryMstInfo> oRecCategoryCollection = null;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                oRecCategoryDAO = new ReconciliationCategoryMstDAO(oAppUserInfo);
                oRecCategoryCollection = oRecCategoryDAO.GetAllOpenItemClassification();
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return oRecCategoryCollection;
        }

    }
}
