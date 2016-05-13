using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SkyStem.ART.Service.Model
{
    public class MatchSetResultInfo : AuditField
    {
        public long? MatchSetResultID { get; set; }
        public long? MatchSetSubSetCombinationID { get; set; }
        public string MatchData { get; set; }
        public string PartialMatchData { get; set; }
        public string UnmatchData { get; set; }
        public string ResultSchema { get; set; }

    }
}
