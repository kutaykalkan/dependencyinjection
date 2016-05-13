using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace SkyStem.ART.Service.Model
{
    public class MatchingSourceDataImportHdrInfo : AuditField
    {
        public long? MatchingSourceDataImportID { get; set; }
        public string MatchingSourceName { get; set; }
        public string FileName { get; set; }
        public string PhysicalPath { get; set; }
        public decimal? FileSize { get; set; }
        public short? MatchingSourceTypeID { get; set; }
        public int? RecPeriodID { get; set; }
        public short? DataImportStatusID { get; set; }
        public int? RecordsImported { get; set; }
        public int? RecItemCreatedCount { get; set; }
        public bool? IsForceCommit { get; set; }
        public DateTime ForceCommitDate { get; set; }
        public int? UserID { get; set; }
        public int? RoleID { get; set; }
        public int? LanguageID { get; set; }
        public string Message { get; set; }
        public int? CompanyID { get; set; }
        public bool? HasError { get; set; }
        public string KeyFields { get; set; }

        public string MatchingSourceColumnXML { get; set; }
        public List<MatchingSourceColumnInfo> MatchingSourceColumns { get; set; }

        public MatchingSourceDataInfo MatchingSourceData { get; set; }
        public List<MatchingSourceAccountInfo> MatchingSourceAccounts { get; set; }

        public DataTable AccountDataTable;
        public string AccountUniqueSubSetKeys { get; set; }
    }



   

}
