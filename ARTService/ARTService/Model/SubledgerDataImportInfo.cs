using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SkyStem.ART.Service.Data;
using SkyStem.ART.Client.Model;

namespace SkyStem.ART.Service.Model
{
    public class SubledgerDataImportInfo: DataImportHdrInfo 
    {
        public string OverridenAcctEmailID { get; set; }
        public bool IsAlertRaised { get; set; }
        public List<UserAccountInfo> UserAccountInfoCollection { get; set; }
        public bool IsFSCaptionFieldAvailable { get; set; }
        public bool IsAccountTypeFieldAvailable { get; set; }
        public bool IsAccountNumberFieldAvailable { get; set; }
        public bool IsAccountNameFieldAvailable { get; set; }
        public bool IsProfitAndLossAvailable { get; set; }
    }
}
