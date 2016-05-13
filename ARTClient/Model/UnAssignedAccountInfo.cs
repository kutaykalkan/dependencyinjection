using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SkyStem.ART.Client.Model.Base;
using System.Xml.Serialization;

namespace SkyStem.ART.Client.Model
{
    [Serializable]
    public class UnAssignedAccountInfo
    {
        private String _Role = null;
        private Int32? _NoOfAssignedAccounts = null;
        private Int32? _NoOfUnassignedAccounts = null;
        private Int32? _TotalAccounts = null;


        [XmlElement(ElementName = "Role")]
        public String Role
        {
            get
            {
                return _Role;
            }
            set
            {
                _Role = value;
            }
        }

        [XmlElement(ElementName = "NoOfAssignedAccounts")]
        public Int32? NoOfAssignedAccounts
        {
            get { return _NoOfAssignedAccounts; }
            set { _NoOfAssignedAccounts = value; }
        }

        [XmlElement(ElementName = "NoOfUnassignedAccounts")]
        public Int32? NoOfUnassignedAccounts
        {
            get { return _NoOfUnassignedAccounts; }
            set { _NoOfUnassignedAccounts = value; }
        }

        [XmlElement(ElementName = "TotalAccounts")]
        public Int32? TotalAccounts
        {
            get { return _TotalAccounts; }
            set { _TotalAccounts = value; }
        }


    }
}
