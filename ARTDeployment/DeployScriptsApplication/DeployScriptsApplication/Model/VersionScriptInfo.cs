
using System;
using Adapdev.Text;
using System.Runtime.Serialization;
using SkyStem.ART.Client.Model.Base;
using System.Xml;
using System.Xml.Serialization;
namespace SkyStem.ART.Client.Model
{    

	/// <summary>
	/// An object representation of the SkyStemARTCore VersionScript table
	/// </summary>
	[Serializable]
	[DataContract]
	public class VersionScriptInfo : VersionScriptInfoBase
	{
        protected System.Boolean _IsNew = false;
        [DataMember]
        [XmlElement(ElementName = "IsNew")]
        public virtual System.Boolean IsNew
        {
            get { return this._IsNew; }
            set { this._IsNew = value; }
        }
        protected System.Boolean _IsVersionScriptExecuted = false;
        [DataMember]
        [XmlElement(ElementName = "IsVersionScriptExecuted")]
        public virtual System.Boolean IsVersionScriptExecuted
        {
            get { return this._IsVersionScriptExecuted; }
            set { this._IsVersionScriptExecuted = value; }
        }	
	}
}
