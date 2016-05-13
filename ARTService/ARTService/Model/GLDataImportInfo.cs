using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SkyStem.ART.Service.Utility;
using SkyStem.ART.Service.Data;
using SkyStem.ART.Client.Model;

namespace SkyStem.ART.Service.Model
{
    public class GLDataImportInfo : DataImportHdrInfo
    {
        public string OverridenAcctEmailID { get; set; }
        public bool IsAlertRaised { get; set; }
        public List<UserAccountInfo> UserAccountInfoCollection { get; set; }
        public bool IsFSCaptionFieldAvailable { get; set; }
        public bool IsAccountTypeFieldAvailable { get; set; }
        public bool IsProfitAndLossAvailable { get; set; }
        public bool IsAccountNumberFieldAvailable { get; set; }
        public bool IsAccountNameFieldAvailable { get; set; }
        public bool IsKey2FieldAvailable { get; set; }
        public bool IsKey3FieldAvailable { get; set; }
        public bool IsKey4FieldAvailable { get; set; }
        public bool IsKey5FieldAvailable { get; set; }
        public bool IsKey6FieldAvailable { get; set; }
        public bool IsKey7FieldAvailable { get; set; }
        public bool IsKey8FieldAvailable { get; set; }
        public bool IsKey9FieldAvailable { get; set; }

        public GLDataImportInfo()
        {
            this.OverridenAcctEmailID = "";
            this.IsAlertRaised = false;

            this.IsFSCaptionFieldAvailable = false;
            this.IsAccountTypeFieldAvailable = false;
            this.IsProfitAndLossAvailable = false;
            this.IsAccountNameFieldAvailable = false;
            this.IsAccountNumberFieldAvailable = false;
            this.IsKey2FieldAvailable = false;
            this.IsKey3FieldAvailable = false;
            this.IsKey4FieldAvailable = false;
            this.IsKey5FieldAvailable = false;
            this.IsKey6FieldAvailable = false;
            this.IsKey7FieldAvailable = false;
            this.IsKey8FieldAvailable = false;
            this.IsKey9FieldAvailable = false;
        }
    }
}
