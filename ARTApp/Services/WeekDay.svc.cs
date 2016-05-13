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
using System.Data.SqlClient;
using SkyStem.ART.App.Utility;
using SkyStem.ART.Client.Data;

namespace SkyStem.ART.App.Services
{
    // NOTE: If you change the class name "Account" here, you must also update the reference to "Account" in Web.config.
    public class WeekDay : IWeekDay
    {

        public List<WeekDayMstInfo> GetAllWeekDays( AppUserInfo oAppUserInfo)
        {
            List<WeekDayMstInfo> oWeekDayMstInfoCollection = null;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                WeekDayMstDAO oWeekDayMstDAO = new WeekDayMstDAO(oAppUserInfo);
                oWeekDayMstInfoCollection = (List<WeekDayMstInfo>)oWeekDayMstDAO.GetAllWeekDayList();
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return oWeekDayMstInfoCollection;
        }

    }//end of class
}//end of namespace