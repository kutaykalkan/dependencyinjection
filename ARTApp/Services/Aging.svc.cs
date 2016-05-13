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
    // NOTE: If you change the class name "Aging" here, you must also update the reference to "Aging" in Web.config.
    public class Aging : IAging
    {
        public void DoWork()
        {
        }


        #region IAging Members

        public IList<AgingCategoryMstInfo> GetAllAgingCategories(AppUserInfo oAppUserInfo)
        {
            AgingCategoryMstDAO oAgingCategoryDAO = null;
            IList<AgingCategoryMstInfo> oAgingCategoryCollection = null;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                oAgingCategoryDAO = new AgingCategoryMstDAO(oAppUserInfo);
                oAgingCategoryCollection = oAgingCategoryDAO.GetAllAgingcategory();
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return oAgingCategoryCollection;
        }
        #endregion
    }
}
