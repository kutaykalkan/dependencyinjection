using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SkyStem.ART.Service.Model
{
    public class MatchingSourceAccountInfo
    {
        public long? MatchingSourceAccountID { get; set; }
        public int? MatchingSourceDataImportID { get; set; }
        public long? AccountID { get; set; }
        public string MatchingSourceAccountData { get; set; }
        public int? RecordsImported { get; set; }
        public int? RecItemCreatedCount { get; set; }
    }
}
