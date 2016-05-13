using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using SkyStem.ART.Service.Data;

namespace SkyStem.ART.Service.Model
{
    [XmlType(ServiceConstants.MATCHING_RULESXMLTYPE)]
    public class MatchingConfigurationRuleInfo
    {
        public int? MatchingConfigurationRuleID { get; set; }
        public int? MatchingConfigurationID { get; set; }
        public short? OperatorID { get; set; }
        public short? ThresholdTypeID { get; set; }
        public decimal? LowerBound { get; set; }
        public decimal? UpperBound { get; set; }
        public string Keywords { get; set; }
    }
}
