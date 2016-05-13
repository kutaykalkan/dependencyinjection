using SkyStem.ART.Client.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyStem.ART.Service.Model
{
    public class CurrencyDataImportInfo : DataImportHdrInfo
    {
        public string OverridenAcctEmailID { get; set; }
        public bool IsAlertRaised { get; set; }
        public List<UserAccountInfo> UserAccountInfoCollection { get; set; }

        public CurrencyDataImportInfo()
        {
            this.OverridenAcctEmailID = "";
            this.IsAlertRaised = false;
        }
    }
}
