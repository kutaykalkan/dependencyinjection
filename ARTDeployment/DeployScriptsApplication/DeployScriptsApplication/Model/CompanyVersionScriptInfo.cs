
using System;
using Adapdev.Text;
using System.Runtime.Serialization;
using SkyStem.ART.Client.Model.Base;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace SkyStem.ART.Client.Model
{    

	/// <summary>
	/// An object representation of the SkyStemARTCore CompanyVersionScript table
	/// </summary>
	[Serializable]
	[DataContract]
	public class CompanyVersionScriptInfo : CompanyVersionScriptInfoBase
	{
        protected System.String _VersionNumber = null;
        protected System.String _CompanyName = null;
        protected System.String _ReleaseStatus = null;
        protected System.String _ScriptName = null;
        protected System.Int16? _ScriptOrder = null;
        protected System.String _ScriptPath = null;

        [DataMember]
        [XmlElement(ElementName = "VersionNumber")]
        public virtual System.String VersionNumber
        {
            get { return this._VersionNumber; }
            set { this._VersionNumber = value; }
        }

        [DataMember]
        [XmlElement(ElementName = "CompanyName")]
        public virtual System.String CompanyName
        {
            get { return this._CompanyName; }
            set { this._CompanyName = value; }
        }

        [DataMember]
        [XmlElement(ElementName = "ReleaseStatus")]
        public virtual System.String ReleaseStatus
        {
            get { return this._ReleaseStatus; }
            set { this._ReleaseStatus = value; }
        }
        [DataMember]
        [XmlElement(ElementName = "ScriptName")]
        public virtual System.String ScriptName
        {
            get { return this._ScriptName; }
            set { this._ScriptName = value; }
        }
        [DataMember]
        [XmlElement(ElementName = "ScriptOrder")]
        public virtual System.Int16? ScriptOrder
        {
            get { return this._ScriptOrder; }
            set { this._ScriptOrder = value; }
        }
        [DataMember]
        [XmlElement(ElementName = "ScriptPath")]
        public virtual System.String ScriptPath
        {
            get { return this._ScriptPath; }
            set { this._ScriptPath = value; }
        }
	}
}
