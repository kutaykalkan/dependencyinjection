using System;
using Adapdev.Text;
using SkyStem.ART.Client.Model.Base;
using System.Runtime.Serialization;
using System.Collections.Generic;
using SkyStem.ART.Client.Data;
using System.Xml.Serialization;
using System.IO;
using System.Xml;


namespace SkyStem.ART.Client.Model
{
    [Serializable]
    [DataContract]
    public class UserStatusDetailInfo
    {
        [DataMember]
        [XmlElement(ElementName = "UserIDs")]
        public int? UserID { get; set; }

        [DataMember]
        [XmlElement(ElementName = "UserStatusID")]
        public short? UserStatusID { get; set; }

        [DataMember]
        [XmlElement(ElementName = "AddedByUserID")]
        public int? AddedByUserID { get; set; }

        [DataMember]
        [XmlElement(ElementName = "AddedByRoleID")]
        public short? AddedByRoleID { get; set; }

        [DataMember]
        [XmlElement(ElementName = "UserStatusDate")]
        public DateTime? UserStatusDate { get; set; }

        [DataMember]
        [XmlElement(ElementName = "AddedByUserName")]
        public string AddedByUserName { get; set; }

        [DataMember]
        [XmlElement(ElementName = "UserStatusLabelID")]
        public int? UserStatusLabelID { get; set; }

        [DataMember]
        [XmlElement(ElementName = "UserStatus")]
        public string UserStatus { get; set; }


    }
}
