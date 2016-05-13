using System;
using Adapdev.Text;
using System.Runtime.Serialization;
using SkyStem.ART.Client.Model.Base;
using SkyStem.ART.Client.Model.RecControlCheckList.Base;
using System.Xml.Serialization;

namespace SkyStem.ART.Client.Model.RecControlCheckList
{
    /// <summary>
    /// An object representation of the SkyStemART RecControlCheckListAccount table
    /// </summary>
    [Serializable]
    [DataContract]
    public class RecControlCheckListAccountInfo : RecControlCheckListAccountInfoBase
    {
        [DataMember]
        [XmlElement(ElementName = "RowNumber")]
        public int RowNumber { get; set; }
    }
}
