using System;
using Adapdev.Text;
using System.Runtime.Serialization;
using SkyStem.ART.Client.Model.Base;
using SkyStem.ART.Client.Model.RecControlCheckList.Base;
using System.Xml.Serialization;

namespace SkyStem.ART.Client.Model.RecControlCheckList
{
    /// <summary>
    /// An object representation of the SkyStemART GLDataRecControlCheckListComment table
    /// </summary>
    [Serializable]
    [DataContract]
    public class GLDataRecControlCheckListCommentInfo : GLDataRecControlCheckListCommentInfoBase
    {
        [DataMember]
        [XmlElement(ElementName = "AddedByUserName")]
        public string  AddedByUserName { get; set; }
    }
}
