using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace SkyStem.ART.Client.Model
{
    public class AssignedAccountCountInfo
    {
        System.Int32? _TotalAccounts = null;
        System.Int32? _TotalAssignedAccounts = null;

        [XmlElement(ElementName = "TotalAccounts")]
        public System.Int32? TotalAccounts
        {
            get { return _TotalAccounts; }
            set { _TotalAccounts = value; }
        }

        [XmlElement(ElementName = "TotalAssignedAccounts")]
        public System.Int32? TotalAssignedAccounts
        {
            get { return _TotalAssignedAccounts; }
            set { _TotalAssignedAccounts = value; }
        }
    }
}
