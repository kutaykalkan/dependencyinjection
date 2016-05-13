using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace SkyStem.ART.Client.Model
{
    public class AccountCountInfo
    {
        System.Int32? _TotalAccounts = null;
        System.Int32? _TotalCompletedAccounts = null;

        [XmlElement(ElementName = "TotalAccounts")]
        public System.Int32? TotalAccounts
        {
            get { return _TotalAccounts; }
            set { _TotalAccounts = value; }
        }

        [XmlElement(ElementName = "TotalCompletedAccounts")]
        public System.Int32? TotalCompletedAccounts
        {
            get { return _TotalCompletedAccounts; }
            set { _TotalCompletedAccounts = value; }
        }

    }
}
