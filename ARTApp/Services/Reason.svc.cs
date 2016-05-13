using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using SkyStem.ART.Client.IServices;
using SkyStem.ART.Client.Model;
using SkyStem.ART.App.DAO;
using SkyStem.ART.App.Utility;

namespace SkyStem.ART.App.Services
{
    // NOTE: If you change the class name "Reason" here, you must also update the reference to "Reason" in Web.config.
    public class Reason : IReason
    {
        public void DoWork()
        {
        }
        public IList<ReasonMstInfo> GetAllReasons( AppUserInfo oAppUserInfo)
        {
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                ReasonMstDAO oReasonMstDAO = new ReasonMstDAO(oAppUserInfo);
                return oReasonMstDAO.GetAllReason();
           }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
