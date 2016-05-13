using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SkyStem.ART.Service.Model
{
    public class MatchingConfigurationInfo : AuditField
    {
        public long? MatchingConfigurationID { get; set; }
        public long? MatchSetSubSetCombinationID { get; set; }
        public long? MatchingSource1ColumnID { get; set; }
        public string MatchingSource1ColumnName { get; set; }
        public long? MatchingSource2ColumnID { get; set; }
        public string MatchingSource2ColumnName { get; set; }
        public bool? IsMatching { get; set; }
        public bool? IsPartialMatching { get; set; }
        public bool? IsDisplayColumn { get; set; }
        public short? DataTypeID { get; set; }
        public string DisplayColumnName { get; set; }

        public List<MatchingConfigurationRuleInfo> MatchingConfigurationRuleCollection { get; set; }

        public MatchingSourceColumnInfo Source1Column { get; set; }
        public MatchingSourceColumnInfo Source2Column { get; set; }


    }
}
