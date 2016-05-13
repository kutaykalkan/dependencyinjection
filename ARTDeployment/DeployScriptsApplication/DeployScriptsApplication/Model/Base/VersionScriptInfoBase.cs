
using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using Adapdev.Text;
using System.Runtime.Serialization;


namespace SkyStem.ART.Client.Model.Base
{    
	/// <summary>
	/// An object representation of the SkyStemARTCore VersionScript table
	/// </summary>
	[DataContract]
	[Serializable]
	public abstract class VersionScriptInfoBase
	{
		private List<CompanyVersionScriptInfo> _CompanyVersionScriptByVersionScriptID = null;
		protected System.String _AddedBy = null;
		protected System.DateTime? _DateAdded = null;
		protected System.DateTime? _DateRevised = null;
		protected System.String _HostName = null;
		protected System.Boolean? _IsActive = null;
		protected System.String _RevisedBy = null;
		protected System.String _ScriptName = null;
		protected System.Int16? _ScriptOrder = null;
		protected System.String _ScriptPath = null;
		protected System.Int32? _VersionID = null;
		protected System.Int64? _VersionScriptID = null;
		[DataMember]
		[XmlElement(ElementName = "AddedBy")]
		public virtual System.String AddedBy 
		{
			get { return this._AddedBy; }
			set { this._AddedBy = value; }
		}			
		[DataMember]
		[XmlElement(ElementName = "DateAdded")]
		public virtual System.DateTime? DateAdded 
		{
			get { return this._DateAdded; }
			set { this._DateAdded = value; }
		}			
		[DataMember]
		[XmlElement(ElementName = "DateRevised")]
		public virtual System.DateTime? DateRevised 
		{
			get { return this._DateRevised; }
			set { this._DateRevised = value; }
		}			
		[DataMember]
		[XmlElement(ElementName = "HostName")]
		public virtual System.String HostName 
		{
			get { return this._HostName; }
			set { this._HostName = value; }
		}			
		[DataMember]
		[XmlElement(ElementName = "IsActive")]
		public virtual System.Boolean? IsActive 
		{
			get { return this._IsActive; }
			set { this._IsActive = value; }
		}			
		[DataMember]
		[XmlElement(ElementName = "RevisedBy")]
		public virtual System.String RevisedBy 
		{
			get { return this._RevisedBy; }
			set { this._RevisedBy = value; }
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
		[DataMember]
		[XmlElement(ElementName = "VersionID")]
		public virtual System.Int32? VersionID 
		{
			get { return this._VersionID; }
			set { this._VersionID = value; }
		}			
		[DataMember]
		[XmlElement(ElementName = "VersionScriptID")]
		public virtual System.Int64? VersionScriptID 
		{
			get { return this._VersionScriptID; }
			set { this._VersionScriptID = value; }
		}			
        
		/// <summary>
		/// Returns a string representation of the object, displaying all property and field names and values.
		/// </summary>
		public override string ToString() 
		{
			return StringUtil.ToString(this);
		}		
	
	}
}
