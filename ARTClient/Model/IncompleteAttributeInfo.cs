using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace SkyStem.ART.Client.Model
{
    public class IncompleteAttributeInfo
    {

        System.Int32? _AccountAttributeID = null;
        System.Int32? _CompletedAttributeAccountCount = null;
        System.Int32? _TotalAccounts = null;

        [XmlElement(ElementName = "AccountAttributeID")]
        public System.Int32? AccountAttributeID
        {
            get { return _AccountAttributeID; }
            set { _AccountAttributeID = value; }
        }

        [XmlElement(ElementName = "CompletedAttributeAccountCount")]
        public System.Int32? CompletedAttributeAccountCount
        {
            get { return _CompletedAttributeAccountCount; }
            set { _CompletedAttributeAccountCount = value; }
        }

        [XmlElement(ElementName = "TotalAccounts")]
        public System.Int32? TotalAccounts
        {
            get { return _TotalAccounts; }
            set { _TotalAccounts = value; }
        }
        public int? TotalRecordCountToDisplay { get; set; }

    }
}
