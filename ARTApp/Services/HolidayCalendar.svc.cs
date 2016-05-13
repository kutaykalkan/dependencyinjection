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
    // NOTE: If you change the class name "HolidayCalendar" here, you must also update the reference to "HolidayCalendar" in Web.config.
    public class HolidayCalendar : IHolidayCalendar
    {
        #region IHolidayCalendar Members
        public void DoWork()
        {
        }

        public List<HolidayCalendarInfo> GetHolidayCalendarByCompanyID(int companyID, AppUserInfo oAppUserInfo)
        {
            List<HolidayCalendarInfo> oHolidayCalendarCollection = null;
            try
            {
                ServiceHelper.SetConnectionString(oAppUserInfo);
                HolidayCalendarDAO oHolidayCalendarDao = new HolidayCalendarDAO(oAppUserInfo);
                oHolidayCalendarCollection = oHolidayCalendarDao.GetHolidayCalendarByCompanyID(companyID);
            }
            catch (SqlException ex)
            {
                ServiceHelper.LogAndThrowGenericSqlException(ex, oAppUserInfo);
            }
            catch (Exception ex)
            {
                ServiceHelper.LogAndThrowGenericException(ex, oAppUserInfo);
            }
            return oHolidayCalendarCollection;
        }

        public int? InsertHolidayCalendar(List<HolidayCalendarInfo> oHolidayCalendarCollection
            , IDbConnection oConnection, IDbTransaction oTransaction, int companyID, AppUserInfo oAppUserInfo)
        {
            ServiceHelper.SetConnectionString(oAppUserInfo);
            HolidayCalendarDAO oHolidayCalendarDAO = new HolidayCalendarDAO(oAppUserInfo);
            DataTable dtHolidayCalendar = DataImportServiceHelper.ConvertHolidayCalendarListToDataTable(oHolidayCalendarCollection);
            return oHolidayCalendarDAO.InsertHolidayCalendarDataTable(dtHolidayCalendar, oConnection, oTransaction, companyID);
        }

        #endregion
    }
}
