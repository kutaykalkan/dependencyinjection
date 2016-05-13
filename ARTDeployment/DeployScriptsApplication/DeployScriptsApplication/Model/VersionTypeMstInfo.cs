
using System;
using Adapdev.Text;
using System.Runtime.Serialization;
using SkyStem.ART.Client.Model.Base;
using System.Xml.Serialization;

namespace SkyStem.ART.Client.Model
{    

	/// <summary>
	/// An object representation of the SkyStemARTCore VersionMst table
	/// </summary>
	[Serializable]
	[DataContract]
	public class VersionTypeMstInfo
	{
        protected System.Int32? _VersionTypeID = null;
        protected System.String _VersionType = null;
        [DataMember]
        [XmlElement(ElementName = "VersionTypeID")]
        public virtual System.Int32? VersionTypeID
        {
            get { return this._VersionTypeID; }
            set { this._VersionTypeID = value; }
        }
        [DataMember]
        [XmlElement(ElementName = "VersionType")]
        public virtual System.String VersionType
        {
            get { return this._VersionType; }
            set { this._VersionType = value; }
        }	
       	
	}
}
