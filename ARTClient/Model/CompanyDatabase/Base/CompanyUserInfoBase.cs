
using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using Adapdev.Text;
using System.Runtime.Serialization;

namespace SkyStem.ART.Client.Model.CompanyDatabase.Base
{    
	/// <summary>
	/// An object representation of the SkyStemARTCore CompanyUser table
	/// </summary>
	[DataContract]
	[Serializable]
	public abstract class CompanyUserInfoBase
	{
		protected System.String _AddedBy = null;
		protected System.Int32? _CompanyID = null;
		protected System.Int64? _CompanyUser = null;
		protected System.DateTime? _DateAdded = null;
		protected System.DateTime? _DateRevised = null;
		protected System.String _HostName = null;
		protected System.Boolean? _IsActive = null;
		protected System.String _LoginID = null;
		protected System.String _RevisedBy = null;
		protected System.Int32? _UserID = null;
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
		[XmlElement(ElementName = "CompanyUser")]
		public virtual System.Int64? CompanyUser 
		{
			get { return this._CompanyUser; }
			set { this._CompanyUser = value; }
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
		[XmlElement(ElementName = "LoginID")]
		public virtual System.String LoginID 
		{
			get { return this._LoginID; }
			set { this._LoginID = value; }
		}			
		[DataMember]
		[XmlElement(ElementName = "RevisedBy")]
		public virtual System.String RevisedBy 
		{
			get { return this._RevisedBy; }
			set { this._RevisedBy = value; }
		}			
		[DataMember]
		[XmlElement(ElementName = "UserID")]
		public virtual System.Int32? UserID 
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
