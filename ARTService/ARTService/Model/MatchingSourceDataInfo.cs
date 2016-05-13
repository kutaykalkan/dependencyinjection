using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SkyStem.ART.Service.Model
{
    public class MatchingSourceDataInfo
    {
        public int? MatchingSourceDataID { get; set; }
        public long? MatchingSourceDataImportID { get; set; }
        public string XMLData { get; set; }
        public string TableXMLSchema { get; set; }
    }
}
