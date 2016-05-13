using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace SkyStem.ART.Client.Model
{
    [Serializable]
    [DataContract]
    public class LogInfo
    {

        [DataMember]
        public int CompanyID { get; set; }

        [DataMember]
        public int RecPeriodID { get; set; }

        [DataMember]
        public int DataImportID { get; set; }

        [DataMember]
        public string Message { get; set; }

        [DataMember]
        public string StackTrace { get; set; }

        [DataMember]
        public int? ErrorCode { get; set; }

        [DataMember]
        public DateTime? LogDate { get; set; }

        [DataMember]
        public string LogLevel { get; set; }

        [DataMember]
        public string Source { get; set; }

    }
}
