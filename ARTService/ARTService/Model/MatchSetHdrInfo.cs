using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SkyStem.ART.Service.Model
{
    public class MatchSetHdrInfo: AuditField 
    {
        public long? MatchSetID { get; set; }
        public string MatchSetName {get; set;}
        public string MatchSetDescription { get; set; } 
        public Int64? GLDataID { get; set; }
        public short? MatchingTypeID {get; set; }
        public short? MatchingStatusID { get; set; }
        public int? RecPeriodID { get; set; }
        public string Message { get; set; }
        public string UserEmailId { get; set; }
        public int UserLanguageID { get; set; }
    }
}
