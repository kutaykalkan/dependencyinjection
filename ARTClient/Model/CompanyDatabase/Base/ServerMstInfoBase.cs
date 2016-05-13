
using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using Adapdev.Text;
using System.Runtime.Serialization;

namespace SkyStem.ART.Client.Model.CompanyDatabase.Base
{    
	/// <summary>
	/// An object representation of the SkyStemARTCore ServerMst table
	/// </summary>
	[DataContract]
	[Serializable]
	public abstract class ServerMstInfoBase
	{
        //private List<ServerCompanyInfo> _ServerCompanyByServerID = null;
		protected System.String _AddedBy = null;
		protected System.DateTime? _DateAdded = null;
		protected System.DateTime? _DateRevised = null;
		protected System.String _HostName = null;
		protected System.String _Instance = null;
		protected System.Boolean? _IsActive = null;
		protected System.Boolean? _IsFull = null;
		protected System.String _Password = null;
		protected System.String _RevisedBy = null;
		protected System.Int16? _ServerID = null;
		protected System.String _ServerName = null;
		protected System.String _UserID = null;
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
		[XmlElement(ElementName = "Instance")]
		public virtual System.String Instance 
		{
			get { return this._Instance; }
			set { this._Instance = value; }
		}			
		[DataMember]
		[XmlElement(ElementName = "IsActive")]
		public virtual System.Boolean? IsActive 
		{
			get { return this._IsActive; }
			set { this._IsActive = value; }
		}			
		[DataMember]
		[XmlElement(ElementName = "IsFull")]
		public virtual System.Boolean? IsFull 
		{
			get { return this._IsFull; }
			set { this._IsFull = value; }
		}			
		[DataMember]
		[XmlElement(ElementName = "Password")]
		public virtual System.String Password 
		{
			get { return this._Password; }
			set { this._Password = value; }
		}			
		[DataMember]
		[XmlElement(ElementName = "RevisedBy")]
		public virtual System.String RevisedBy 
		{
			get { return this._RevisedBy; }
			set { this._RevisedBy = value; }
		}			
		[DataMember]
		[XmlElement(ElementName = "ServerID")]
		public virtual System.Int16? ServerID 
		{
			get { return this._ServerID; }
			set { this._ServerID = value; }
		}			
		[DataMember]
		[XmlElement(ElementName = "ServerName")]
		public virtual System.String ServerName 
		{
			get { return this._ServerName; }
			set { this._ServerName = value; }
		}			
		[DataMember]
		[XmlElement(ElementName = "UserID")]
		public virtual System.String UserID 
		{
			get { return this._UserID; }
			set { this._UserID = value; }
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
