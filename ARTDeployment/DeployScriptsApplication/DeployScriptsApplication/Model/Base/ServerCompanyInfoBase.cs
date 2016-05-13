
using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using Adapdev.Text;
using System.Runtime.Serialization;

namespace SkyStem.ART.Client.Model.CompanyDatabase.Base
{    
	/// <summary>
	/// An object representation of the SkyStemARTCore ServerCompany table
	/// </summary>
	[DataContract]
	[Serializable]
	public abstract class ServerCompanyInfoBase
	{
		protected System.String _AddedBy = null;
		protected System.Int32? _CompanyID = null;
		protected System.String _DatabaseName = null;
		protected System.DateTime? _DateAdded = null;
		protected System.DateTime? _DateRevised = null;
		protected System.String _HostName = null;
		protected System.Boolean? _IsActive = null;
		protected System.String _RevisedBy = null;
		protected System.Int32? _ServerCompanyID = null;
		protected System.Int16? _ServerID = null;
		[DataMember]
		[XmlElement(ElementName = "AddedBy")]
		public virtual System.String AddedBy 
		{
			get { return this._AddedBy; }
			set { this._AddedBy = value; }
		}			
		[DataMember]
		[XmlElement(ElementName = "CompanyID")]
		public virtual System.Int32? CompanyID 
		{
			get { return this._CompanyID; }
			set { this._CompanyID = value; }
		}			
		[DataMember]
		[XmlElement(ElementName = "DatabaseName")]
		public virtual System.String DatabaseName 
		{
			get { return this._DatabaseName; }
			set { this._DatabaseName = value; }
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
		[XmlElement(ElementName = "CompanyID")]
		public virtual System.Int32? ServerCompanyID 
		{
			get { return this._ServerCompanyID; }
			set { this._ServerCompanyID = value; }
		}			
		[DataMember]
		[XmlElement(ElementName = "ServerID")]
		public virtual System.Int16? ServerID 
		{
			get { return this._ServerID; }
			set { this._ServerID = value; }
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
