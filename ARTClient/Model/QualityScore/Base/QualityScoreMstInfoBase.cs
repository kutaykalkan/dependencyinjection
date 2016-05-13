
using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using Adapdev.Text;
using System.Runtime.Serialization;

namespace SkyStem.ART.Client.Model.QualityScore.Base
{    
	/// <summary>
	/// An object representation of the SkyStemART QualityScoreMst table
	/// </summary>
	[DataContract]
	[Serializable]
	public abstract class QualityScoreMstInfoBase
	{
		protected System.String _AddedBy = null;
		protected System.DateTime? _DateAdded = null;
		protected System.DateTime? _DateRevised = null;
		protected System.String _Description = null;
		protected System.Int32? _DescriptionLabelID = null;
        protected System.String _QualityScoreNumber = null;
		protected System.String _HostName = null;
		protected System.Boolean? _IsActive = null;
		protected System.Boolean? _IsApplicableForSRA = null;
		protected System.Boolean? _IsUserScoreEnabled = null;
		protected System.Int32? _QualityScoreID = null;
		protected System.String _RevisedBy = null;
		protected System.Int32? _SortOrder = null;
		protected System.Decimal? _Weightage = null;
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
		[XmlElement(ElementName = "Description")]
		public virtual System.String Description 
		{
			get { return this._Description; }
			set { this._Description = value; }
		}			
		[DataMember]
		[XmlElement(ElementName = "DescriptionLabelID")]
		public virtual System.Int32? DescriptionLabelID 
		{
			get { return this._DescriptionLabelID; }
			set { this._DescriptionLabelID = value; }
		}
        [DataMember]
        [XmlElement(ElementName = "QualityScoreNumber")]
        public virtual System.String QualityScoreNumber
        {
            get { return this._QualityScoreNumber; }
            set { this._QualityScoreNumber = value; }
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
		[XmlElement(ElementName = "IsApplicableForSRA")]
		public virtual System.Boolean? IsApplicableForSRA 
		{
			get { return this._IsApplicableForSRA; }
			set { this._IsApplicableForSRA = value; }
		}			
		[DataMember]
		[XmlElement(ElementName = "IsUserScoreEnabled")]
		public virtual System.Boolean? IsUserScoreEnabled 
		{
			get { return this._IsUserScoreEnabled; }
			set { this._IsUserScoreEnabled = value; }
		}			
		[DataMember]
		[XmlElement(ElementName = "QualityScoreID")]
		public virtual System.Int32? QualityScoreID 
		{
			get { return this._QualityScoreID; }
			set { this._QualityScoreID = value; }
		}			
		[DataMember]
		[XmlElement(ElementName = "RevisedBy")]
		public virtual System.String RevisedBy 
		{
			get { return this._RevisedBy; }
			set { this._RevisedBy = value; }
		}			
		[DataMember]
		[XmlElement(ElementName = "SortOrder")]
		public virtual System.Int32? SortOrder 
		{
			get { return this._SortOrder; }
			set { this._SortOrder = value; }
		}			
		[DataMember]
		[XmlElement(ElementName = "Weightage")]
		public virtual System.Decimal? Weightage 
		{
			get { return this._Weightage; }
			set { this._Weightage = value; }
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
