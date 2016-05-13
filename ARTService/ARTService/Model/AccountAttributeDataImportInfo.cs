using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SkyStem.ART.Service.Model
{
    public class AccountAttributeDataImportInfo : DataImportHdrInfo
    {
        public bool IsRiskRatingFieldAvailable { get; set; }
        public bool IsRecTemplateFieldAvailable { get; set; }
        public bool IsKeyAccountFieldAvailable { get; set; }
        public bool IsZeroBalanceFieldAvailable { get; set; }
        public bool IsSubledgerSourceFieldAvailable { get; set; }
        public bool IsRecPolicyFieldAvailable { get; set; }
        public bool IsNatureOfAcctFieldAvailable { get; set; }
        public bool IsRecProcedureFieldAvailable { get; set; }
        public bool IsPreparerFieldAvailable { get; set; }
        public bool IsReviewerFieldAvailable { get; set; }
        public bool IsApproverFieldAvailable { get; set; }
        public bool IsBackupPreparerFieldAvailable { get; set; }
        public bool IsBackupReviewerFieldAvailable { get; set; }
        public bool IsBackupApproverFieldAvailable { get; set; }
        public bool IsReconcilableFieldAvailable { get; set; }
        public bool IsPreparerDueDaysFieldAvailable { get; set; }
        public bool IsReviewerDueDaysFieldAvailable { get; set; }
        public bool IsApproverDueDaysFieldAvailable { get; set; }
        //public bool IsDayTypeFieldAvailable { get; set; }
    }
}
