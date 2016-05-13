
using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using Adapdev.Text;
using System.Runtime.Serialization;

namespace SkyStem.ART.Client.Model.Base
{    
	/// <summary>
	/// An object representation of the SkyStemARTCore CompanyVersionScript table
	/// </summary>
	[DataContract]
	[Serializable]
	public abstract class CompanyVersionScriptInfoBase
	{
		protected System.String _AddedBy = null;
		protected System.Int64? _CompanyVersionScriptID = null;
		protected System.DateTime? _DateAdded = null;
		protected System.DateTime? _DateRevised = null;
		protected System.String _ErrorMsg = null;
		protected System.String _HostName = null;
		protected System.Boolean? _IsActive = null;
		protected System.Int16? _ReleaseStatusID = null;
		protected System.String _RevisedBy = null;
		protected System.Int32? _CompanyID = null;
		protected System.Int64? _VersionScriptID = null;
		[DataMember]
		[XmlElement(ElementName = "AddedBy")]
		public virtual System.String AddedBy 
		{
			get { return this._AddedBy; }
			set { this._AddedBy = value; }
		}			
		[DataMember]
		[XmlElement(ElementName = "CompanyVersionScriptID")]
		public virtual System.Int64? CompanyVersionScriptID 
		{
			get { return this._CompanyVersionScriptID; }
			set { this._CompanyVersionScriptID = value; }
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
		[XmlElement(ElementName = "ErrorMsg")]
		public virtual System.String ErrorMsg 
		{
			get { return this._ErrorMsg; }
			set { this._ErrorMsg = value; }
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
		[XmlElement(ElementName = "ReleaseStatusID")]
		public virtual System.Int16? ReleaseStatusID 
		{
			get { return this._ReleaseStatusID; }
			set { this._ReleaseStatusID = value; }
		}			
		[DataMember]
		[XmlElement(ElementName = "RevisedBy")]
		public virtual System.String RevisedBy 
		{
			get { return this._RevisedBy; }
			set { this._RevisedBy = value; }
		}			
		[DataMember]
		[XmlElement(ElementName = "CompanyID")]
		public virtual System.Int32? CompanyID 
		{
			get { return this._CompanyID; }
			set { this._CompanyID = value; }
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
