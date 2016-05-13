using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SkyStem.ART.Service.Model
{
    public class MatchSetMatchingSourceDataImportInfo
    {
        public long? MatchSetMatchingSourceDataImportID { get; set; }
        public long? MatchSetID { get; set; }
        public long? MatchingSourceDataImportID { get; set; }
        public string SubSetName { get; set; }
        public string SubSetData { get; set; }
        public bool? IsActive { get; set; }
        public short? MatchSetMatchingSourceTypeID { get; set; }
    }
}
