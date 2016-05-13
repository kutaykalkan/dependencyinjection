using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SkyStem.ART.Client.Model
{
    [Serializable]
    [DataContract]
    public class FTPServerInfo
    {
        [XmlElement(ElementName = "FTPServerId")]
        public Int16 FTPServerId { get; set; }
        [XmlElement(ElementName = "ServerName")]
        public string ServerName { get; set; }
        [XmlElement(ElementName = "Port")]
        public Int32 Port { get; set; }
        [XmlElement(ElementName = "FTPUrl")]
        public string FTPUrl { get; set; }
        [XmlElement(ElementName = "IsActive")]
        public bool IsActive { get; set; }
        [XmlElement(ElementName = "DateAdded")]
        public DateTime DateAdded { get; set; }
        [XmlElement(ElementName = "AddedBy")]
        public string AddedBy { get; set; }
        [XmlElement(ElementName = "DateRevised")]
        public DateTime? DateRevised { get; set; }
        [XmlElement(ElementName = "RevisedBy")]
        public string RevisedBy { get; set; }
    }
}
