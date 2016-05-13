using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using SkyStem.ART.Service.Data;

namespace SkyStem.ART.Service.Model
{
    [XmlType(ServiceConstants.MATCHING_SOURCE_COLUMNXML)]//Column
    public class MatchingSourceColumnInfo
    {
        public int? MatchingSourceColumnID { get; set; }
        public int? MatchingSourceDataImportID { get; set; }
        public string ColumnName { get; set; }
        public short? DataTypeID { get; set; }
        public int? Ordinal { get; set; }
        public string ColumnNameInReader { get; set; }
    }
}
