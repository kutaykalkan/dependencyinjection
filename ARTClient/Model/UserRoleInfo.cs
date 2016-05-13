
using System;
using Adapdev.Text;
using SkyStem.ART.Client.Model.Base;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace SkyStem.ART.Client.Model
{    

	/// <summary>
	/// An object representation of the SkyStemArt UserRole table
	/// </summary>
	[Serializable]
	[DataContract]
	public class UserRoleInfo : UserRoleInfoBase
	{
        [DataMember]
        [XmlElement(ElementName = "UserTransitID")]
        public Int32? UserTransitID { get; set; }

        [DataMember]
        [XmlElement(ElementName = "RoleLabelID")]
        public Int32? RoleLabelID { get; set; }

        [DataMember]
        [XmlElement(ElementName = "Role")]
        public string Role { get; set; }

        [DataMember]
        [XmlElement(ElementName = "IsDefaultRole")]
        public bool? IsDefaultRole { get; set; }
	}
}
