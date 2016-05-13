
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
	public class VersionMstInfo : VersionMstInfoBase
	{
        protected System.Boolean _IsVersionScriptExecuted = false;
        [DataMember]
        [XmlElement(ElementName = "IsVersionScriptExecuted")]
        public virtual System.Boolean IsVersionScriptExecuted
        {
            get { return this._IsVersionScriptExecuted; }
            set { this._IsVersionScriptExecuted = value; }
        }
        protected System.Int32? _VersionTypeID = null;
        [DataMember]
        [XmlElement(ElementName = "VersionTypeID")]
        public virtual System.Int32? VersionTypeID
        {
            get { return this._VersionTypeID; }
            set { this._VersionTypeID = value; }
        }
	}
}
