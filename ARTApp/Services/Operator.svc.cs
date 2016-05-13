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

namespace SkyStem.ART.App.Services
{
    // NOTE: If you change the class name "Operator" here, you must also update the reference to "Operator" in Web.config.
    public class Operator : IOperator
    {
        public void DoWork()
        {
        }
        public List<OperatorMstInfo> GetOperatorsByColumnID(short columnID, AppUserInfo oAppUserInfo)
        {
            List<OperatorMstInfo> oOperatorCollection = null;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                OperatorMstDAO oOperatorDAO = new OperatorMstDAO(oAppUserInfo);
                oOperatorCollection = oOperatorDAO.GetOperatorsByColumnID(columnID);

            }
            catch (SqlException ex)
            {

                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {

                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return oOperatorCollection;
        }

        public List<OperatorMstInfo> GetOperatorsByDynamicColumnID(short columnID, AppUserInfo oAppUserInfo)
        {
            List<OperatorMstInfo> oOperatorCollection = null;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                OperatorMstDAO oOperatorDAO = new OperatorMstDAO(oAppUserInfo);
                oOperatorCollection = oOperatorDAO.GetOperatorsByDynamicColumnID(columnID);

            }
            catch (SqlException ex)
            {

                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {

                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return oOperatorCollection;
        }

        public List<OperatorMstInfo> GetOperatorList(AppUserInfo oAppUserInfo)
        {
            List<OperatorMstInfo> oOperatorCollection = null;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                OperatorMstDAO oOperatorDAO = new OperatorMstDAO(oAppUserInfo);
                oOperatorCollection = oOperatorDAO.GetOperatorList();

            }
            catch (SqlException ex)
            {

                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {

                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return oOperatorCollection;
        }
    }
}
