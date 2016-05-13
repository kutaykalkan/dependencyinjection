using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using SkyStem.ART.Client.Model;
using System.Data;

namespace SkyStem.ART.Client.IServices
{
    // NOTE: If you change the interface name "IHolidayCalendar" here, you must also update the reference to "IHolidayCalendar" in Web.config.
    [ServiceContract]
    public interface IHolidayCalendar
    {
        [OperationContract]
        void DoWork();

        [OperationContract]
        List<HolidayCalendarInfo> GetHolidayCalendarByCompanyID(int companyID, AppUserInfo oAppUserInfo);

        [OperationContract]
        int? InsertHolidayCalendar(List<HolidayCalendarInfo> oHolidayCalendarCollection
            , IDbConnection oConnection, IDbTransaction oTransaction, int companyID, AppUserInfo oAppUserInfo);
 
    }
}
