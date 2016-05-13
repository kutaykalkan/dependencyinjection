using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using SkyStem.ART.Service.Data;

namespace SkyStem.ART.Service.Model
{
    public class MatchSetSubSetCombinationInfo : AuditField
    {
        public long? MatchSetSubSetCombinationID { get; set; }
        public long? MatchSetMatchingSourceDataImport1ID { get; set; }
        public long? MatchSetMatchingSourceDataImport2ID { get; set; }
        public string MatchSetSubSetCombinationName { get; set; }
        public bool IsConfigurationComplete { get; set; }
        public MatchSetMatchingSourceDataImportInfo Source1 { get; set; }
        public MatchSetMatchingSourceDataImportInfo Source2 { get; set; }
        public List<MatchingConfigurationInfo> MatchingConfigurationCollection { get; set; }
    }

    

    

    

    
}
