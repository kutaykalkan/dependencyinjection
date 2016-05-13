using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SkyStem.ART.Client.Model.RecControlCheckList
{
    public class ReconciliationCheckListStatusInfo
    {
        [DataMember]
        [XmlElement(ElementName = "ReconciliationCheckListStatusID")]
        public System.Int16? ReconciliationCheckListStatusID { get; set; }

        [DataMember]
        [XmlElement(ElementName = "ReconciliationCheckListStatus")]
        public String ReconciliationCheckListStatus { get; set; }

        [DataMember]
        [XmlElement(ElementName = "ReconciliationCheckListStatusLabelID")]
        public System.Int32? ReconciliationCheckListStatusLabelID { get; set; }

    }
}
