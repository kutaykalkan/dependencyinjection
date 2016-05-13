
using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using Adapdev.Text;
using System.Runtime.Serialization;

namespace SkyStem.ART.Client.Model.QualityScore.Base
{    
	/// <summary>
	/// An object representation of the SkyStemART QualityScoreStatusMst table
	/// </summary>
	[DataContract]
	[Serializable]
	public abstract class QualityScoreStatusMstInfoBase
	{
		protected System.String _AddedBy = null;
		protected System.DateTime? _DateAdded = null;
		protected System.DateTime? _DateRevised = null;
		protected System.String _HostName = null;
		protected System.Boolean? _IsActive = null;
		protected System.String _QualityScoreStatus = null;
		protected System.Int16? _QualityScoreStatusID = null;
		protected System.Int32? _QualityScoreStatusLabelID = null;
		protected System.String _RevisedBy = null;
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
		[XmlElement(ElementName = "QualityScoreStatus")]
		public virtual System.String QualityScoreStatus 
		{
			get { return this._QualityScoreStatus; }
			set { this._QualityScoreStatus = value; }
		}			
		[DataMember]
		[XmlElement(ElementName = "QualityScoreStatusID")]
		public virtual System.Int16? QualityScoreStatusID 
		{
			get { return this._QualityScoreStatusID; }
			set { this._QualityScoreStatusID = value; }
		}			
		[DataMember]
		[XmlElement(ElementName = "QualityScoreStatusLabelID")]
		public virtual System.Int32? QualityScoreStatusLabelID 
		{
			get { return this._QualityScoreStatusLabelID; }
			set { this._QualityScoreStatusLabelID = value; }
		}			
		[DataMember]
		[XmlElement(ElementName = "RevisedBy")]
		public virtual System.String RevisedBy 
		{
			get { return this._RevisedBy; }
			set { this._RevisedBy = value; }
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
