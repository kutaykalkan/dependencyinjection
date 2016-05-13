
using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using Adapdev.Text;
using System.Runtime.Serialization;

namespace SkyStem.ART.Client.Model.QualityScore.Base
{    
	/// <summary>
	/// An object representation of the SkyStemART GLDataQualityScore table
	/// </summary>
	[DataContract]
	[Serializable]
	public abstract class GLDataQualityScoreInfoBase
	{
		protected System.String _AddedBy = null;
		protected System.Int32? _AddedByUserID = null;
		protected System.String _Comments = null;
		protected System.Int32? _CompanyQualityScoreID = null;
		protected System.DateTime? _DateAdded = null;
		protected System.DateTime? _DateRevised = null;
		protected System.Int64? _GLDataID = null;
		protected System.Int64? _GLDataQualityScoreID = null;
		protected System.String _HostName = null;
		protected System.Boolean? _IsActive = null;
		protected System.String _RevisedBy = null;
		protected System.Int16? _SystemQualityScoreStatusID = null;
        protected System.Int16? _UserQualityScoreStatusID = null;
		[DataMember]
		[XmlElement(ElementName = "AddedBy")]
		public virtual System.String AddedBy 
		{
			get { return this._AddedBy; }
			set { this._AddedBy = value; }
		}			
		[DataMember]
		[XmlElement(ElementName = "AddedByUserID")]
		public virtual System.Int32? AddedByUserID 
		{
			get { return this._AddedByUserID; }
			set { this._AddedByUserID = value; }
		}			
		[DataMember]
		[XmlElement(ElementName = "Comments")]
		public virtual System.String Comments 
		{
			get { return this._Comments; }
			set { this._Comments = value; }
		}			
		[DataMember]
		[XmlElement(ElementName = "CompanyQualityScoreID")]
		public virtual System.Int32? CompanyQualityScoreID 
		{
			get { return this._CompanyQualityScoreID; }
			set { this._CompanyQualityScoreID = value; }
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
		[XmlElement(ElementName = "GLDataID")]
		public virtual System.Int64? GLDataID 
		{
			get { return this._GLDataID; }
			set { this._GLDataID = value; }
		}			
		[DataMember]
		[XmlElement(ElementName = "GLDataQualityScoreID")]
		public virtual System.Int64? GLDataQualityScoreID 
		{
			get { return this._GLDataQualityScoreID; }
			set { this._GLDataQualityScoreID = value; }
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
		[XmlElement(ElementName = "SystemQualityScoreStatusID")]
		public virtual System.Int16? SystemQualityScoreStatusID 
		{
			get { return this._SystemQualityScoreStatusID; }
			set { this._SystemQualityScoreStatusID = value; }
		}			
		[DataMember]
		[XmlElement(ElementName = "UserQualityScoreStatusID")]
		public virtual System.Int16? UserQualityScoreStatusID 
		{
			get { return this._UserQualityScoreStatusID; }
			set { this._UserQualityScoreStatusID = value; }
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
