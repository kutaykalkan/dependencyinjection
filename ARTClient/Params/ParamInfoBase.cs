using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace SkyStem.ART.Client.Params
{
    [DataContract]
    public class ParamInfoBase
    {
        [DataMember]
        public int? UserID { get; set; }
        [DataMember]
        public short? RoleID { get; set; }
        [DataMember]
        public int? CompanyID { get; set; }
        [DataMember]
        public int? RecPeriodID { get; set; }
        [DataMember]
        public int? UserLanguageID { get; set; }
        [DataMember]
        public string UserLoginID { get; set; }
        [DataMember]
        public DateTime DateRevised { get; set; }
    }
}
