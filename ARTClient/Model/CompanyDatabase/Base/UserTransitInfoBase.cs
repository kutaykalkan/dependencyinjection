
using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using Adapdev.Text;
using System.Runtime.Serialization;

namespace SkyStem.ART.Client.Model.CompanyDatabase.Base
{    
	/// <summary>
	/// An object representation of the SkyStemARTCore UserTransit table
	/// </summary>
	[DataContract]
	[Serializable]
	public abstract class UserTransitInfoBase
	{
		protected System.String _AddedBy = null;
		protected System.Int32? _CompanyTransitID = null;
		protected System.Int32? _DataImportID = null;
		protected System.DateTime? _DateAdded = null;
		protected System.DateTime? _DateRevised = null;
		protected System.Int32? _DefaultLanguageID = null;
		protected System.Int16? _DefaultRoleID = null;
		protected System.String _EmailID = null;
		protected System.String _FirstName = null;
		protected System.String _HostName = null;
		protected System.Boolean? _IsActive = null;
		protected System.Boolean? _IsModifiedByDataImport = null;
		protected System.Boolean? _IsNew = null;
		protected System.String _JobTitle = null;
		protected System.DateTime? _LastLoggedIn = null;
		protected System.String _LastName = null;
		protected System.String _LoginID = null;
		protected System.String _Password = null;
		protected System.String _Phone = null;
		protected System.String _RevisedBy = null;
		protected System.Int32? _UserTransitID = null;
		protected System.String _WorkPhone = null;
		[DataMember]
		[XmlElement(ElementName = "AddedBy")]
		public virtual System.String AddedBy 
		{
			get { return this._AddedBy; }
			set { this._AddedBy = value; }
		}			
		[DataMember]
		[XmlElement(ElementName = "CompanyTransitID")]
		public virtual System.Int32? CompanyTransitID 
		{
			get { return this._CompanyTransitID; }
			set { this._CompanyTransitID = value; }
		}			
		[DataMember]
		[XmlElement(ElementName = "DataImportID")]
		public virtual System.Int32? DataImportID 
		{
			get { return this._DataImportID; }
			set { this._DataImportID = value; }
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
		[XmlElement(ElementName = "DefaultLanguageID")]
		public virtual System.Int32? DefaultLanguageID 
		{
			get { return this._DefaultLanguageID; }
			set { this._DefaultLanguageID = value; }
		}			
		[DataMember]
		[XmlElement(ElementName = "DefaultRoleID")]
		public virtual System.Int16? DefaultRoleID 
		{
			get { return this._DefaultRoleID; }
			set { this._DefaultRoleID = value; }
		}			
		[DataMember]
		[XmlElement(ElementName = "EmailID")]
		public virtual System.String EmailID 
		{
			get { return this._EmailID; }
			set { this._EmailID = value; }
		}			
		[DataMember]
		[XmlElement(ElementName = "FirstName")]
		public virtual System.String FirstName 
		{
			get { return this._FirstName; }
			set { this._FirstName = value; }
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
		[XmlElement(ElementName = "IsModifiedByDataImport")]
		public virtual System.Boolean? IsModifiedByDataImport 
		{
			get { return this._IsModifiedByDataImport; }
			set { this._IsModifiedByDataImport = value; }
		}			
		[DataMember]
		[XmlElement(ElementName = "IsNew")]
		public virtual System.Boolean? IsNew 
		{
			get { return this._IsNew; }
			set { this._IsNew = value; }
		}			
		[DataMember]
		[XmlElement(ElementName = "JobTitle")]
		public virtual System.String JobTitle 
		{
			get { return this._JobTitle; }
			set { this._JobTitle = value; }
		}			
		[DataMember]
		[XmlElement(ElementName = "LastLoggedIn")]
		public virtual System.DateTime? LastLoggedIn 
		{
			get { return this._LastLoggedIn; }
			set { this._LastLoggedIn = value; }
		}			
		[DataMember]
		[XmlElement(ElementName = "LastName")]
		public virtual System.String LastName 
		{
			get { return this._LastName; }
			set { this._LastName = value; }
		}			
		[DataMember]
		[XmlElement(ElementName = "LoginID")]
		public virtual System.String LoginID 
		{
			get { return this._LoginID; }
			set { this._LoginID = value; }
		}			
		[DataMember]
		[XmlElement(ElementName = "Password")]
		public virtual System.String Password 
		{
			get { return this._Password; }
			set { this._Password = value; }
		}			
		[DataMember]
		[XmlElement(ElementName = "Phone")]
		public virtual System.String Phone 
		{
			get { return this._Phone; }
			set { this._Phone = value; }
		}			
		[DataMember]
		[XmlElement(ElementName = "RevisedBy")]
		public virtual System.String RevisedBy 
		{
			get { return this._RevisedBy; }
			set { this._RevisedBy = value; }
		}			
		[DataMember]
		[XmlElement(ElementName = "UserTransitID")]
		public virtual System.Int32? UserTransitID 
		{
			get { return this._UserTransitID; }
			set { this._UserTransitID = value; }
		}			
		[DataMember]
		[XmlElement(ElementName = "WorkPhone")]
		public virtual System.String WorkPhone 
		{
			get { return this._WorkPhone; }
			set { this._WorkPhone = value; }
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
