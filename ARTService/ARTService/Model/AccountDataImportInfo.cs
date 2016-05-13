using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SkyStem.ART.Service.Utility;
using SkyStem.ART.Service.Data;
using SkyStem.ART.Client.Model;

namespace SkyStem.ART.Service.Model
{
    public class AccountDataImportInfo : DataImportHdrInfo
    {
        public string OverridenAcctEmailID { get; set; }
        public List<UserAccountInfo> UserAccountInfoCollection
        {
            get;
            set;
        }
        public bool IsAlertRaised
        {
            get;
            set;
        }
    }
}
