using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SkyStem.ART.Client.Model.Matching;
using System.Runtime.Serialization;

namespace SkyStem.ART.Client.Params.Matching
{
    [Serializable]
    [DataContract]
    public class MatchingParamInfo : ParamInfoBase
    {
        [DataMember]
        public List<MatchingSourceDataImportHdrInfo> oMatchingSourceDataImportHdrInfoList = null;
        [DataMember]
        public List<MatchingSourceColumnInfo> oMatchingSourceColumnInfoList = null;
        [DataMember]
        public List<MatchSetMatchingSourceDataImportInfo> oMatchSetMatchingSourceDataImportInfoList = null;
        [DataMember]
        public List<MatchSetSubSetCombinationInfo> oMatchSetSubSetCombinationInfoList = null;
        [DataMember]
        public List<MatchingConfigurationInfo> oMatchingConfigurationInfoList = null;
        [DataMember]
        public MatchSetResultWorkspaceInfo MatchSetResultWorkspaceInfo = null;
        [DataMember]
        public List<Int64> IDList = null;
        [DataMember]
        public Int64? AccountID { get; set; }
        [DataMember]
        public String FileName { get; set; }
        [DataMember]
        public Decimal? FileSize { get; set; }
        [DataMember]
        public String MatchingSourceName { get; set; }
        [DataMember]
        public Int16? MatchingSourceTypeID { get; set; }
        [DataMember]
        public String PhysicalPath { get; set; }
        [DataMember]
        public Int32? RecordsImported { get; set; }
        [DataMember]
        public String Message { get; set; }
        [DataMember]
        public Int32? LanguageID { get; set; }
        [DataMember]
        public DateTime? ForceCommitDate { get; set; }
        [DataMember]
        public Boolean? IsForceCommit { get; set; }
        [DataMember]
        public DateTime? DateAdded { get; set; }
        [DataMember]
        public String AddedBy { get; set; }
        [DataMember]
        public String RevisedBy { get; set; }
        [DataMember]
        public Boolean? IsActive { get; set; }
        [DataMember]
        public Int64? MatchingSourceDataImportID { get; set; }
        [DataMember]
        public Int16? DataImportStatusID { get; set; }
        [DataMember]
        public Boolean? IsSubmited { get; set; }
        [DataMember]
        public Boolean ShowOnlySuccessfulMatchingSourceDataImport { get; set; }
        [DataMember]
        public Boolean? IsConfigurationComplete { get; set; }
        [DataMember]
        public System.Int64? MatchSetID { get; set; }
        [DataMember]
        public System.String MatchSetName { get; set; }
        [DataMember]
        public System.String MatchSetDescription { get; set; }
        [DataMember]
        public System.Int64? GLDataID { get; set; }
        [DataMember]
        public System.Int16? MatchingTypeID { get; set; }
        [DataMember]
        public System.Int16? MatchingStatusID { get; set; }
        //public System.Int32? RecPeriodID { get; set; }
        [DataMember]
        public System.Int16? FormMode { get; set; }
        [DataMember]
        public System.Int16? RecordSourceTypeID { get; set; }
        [DataMember]
        public System.Int64? MatchSetSubSetCombinationID { get; set; }
        [DataMember]
        public System.Int64? MatchingConfigurationID { get; set; }
        [DataMember]
        public Boolean? IsHidden { get; set; }
        [DataMember]
        public Boolean IsMatchingSourceSelectionChanged { get; set; }
        [DataMember]
        public MatchSetHdrInfo MatchSetHdrInfo { get; set; }

    }
}
