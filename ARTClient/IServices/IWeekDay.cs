using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using SkyStem.ART.Client.Model;
using SkyStem.ART.Client.Data;

namespace SkyStem.ART.Client.IServices
{
    // NOTE: If you change the interface name "IAccount" here, you must also update the reference to "IAccount" in Web.config.
    [ServiceContract]
    public interface IWeekDay
    {

        [OperationContract]
        List<WeekDayMstInfo> GetAllWeekDays( AppUserInfo oAppUserInfo);


    }
}
