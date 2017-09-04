
using System;
using Adapdev.Text;
using SkyStem.ART.Client.Model.Base;
using System.Runtime.Serialization;

namespace SkyStem.ART.Client.Model
{    

	/// <summary>
	/// An object representation of the SkyStemArt RoleMst table
	/// </summary>
	[Serializable]
	[DataContract]
	public class RoleMstInfo : RoleMstInfoBase
	{
        [DataMember]
        public bool? IsVisibleForAccountAssociationByUserRole { get; set; }
    }
}
