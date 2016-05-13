using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace SkyStem.ART.Client.Model
{
    [Serializable]
    [DataContract]
    public class AppUserInfo
    {
        [DataMember]
        public Int32? UserID { get; set; }

        [DataMember]
        public Int16? RoleID { get; set; }

        [DataMember]
        public Int32? CompanyID { get; set; }

        [DataMember]
        public bool? IsDatabaseExists { get; set; }

        [DataMember]
        public string ConnectionString { get; set; }

        [DataMember]
        public int? RecPeriodID { get; set; }

        [DataMember]
        public string LoginID { get; set; }
    }
}
