
using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using Adapdev.Text;
using System.Runtime.Serialization;

namespace SkyStem.ART.Client.Model.Base
{    
	/// <summary>
	/// An object representation of the SkyStemART TaskDetailReviewNoteHdr table
	/// </summary>
	[DataContract]
	[Serializable]
	public abstract class TaskDetailReviewNoteHdrInfoBase
	{
		protected System.String _AddedBy = null;
		protected System.Int32? _AddedByUserID = null;
		protected System.DateTime? _DateAdded = null;
		protected System.DateTime? _DateRevised = null;
		protected System.String _HostName = null;
		protected System.Boolean? _IsActive = null;
		protected System.String _RevisedBy = null;
		protected System.Int32? _RevisedByUserID = null;
		protected System.String _SubjectLine = null;
		protected System.Int64? _TaskDetailID = null;
		protected System.Int32? _TaskDetailReviewNoteID = null;
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
		[XmlElement(ElementName = "RevisedByUserID")]
		public virtual System.Int32? RevisedByUserID 
		{
			get { return this._RevisedByUserID; }
			set { this._RevisedByUserID = value; }
		}			
		[DataMember]
		[XmlElement(ElementName = "SubjectLine")]
		public virtual System.String SubjectLine 
		{
			get { return this._SubjectLine; }
			set { this._SubjectLine = value; }
		}			
		[DataMember]
		[XmlElement(ElementName = "TaskDetailID")]
		public virtual System.Int64? TaskDetailID 
		{
			get { return this._TaskDetailID; }
			set { this._TaskDetailID = value; }
		}			
		[DataMember]
		[XmlElement(ElementName = "TaskDetailReviewNoteID")]
		public virtual System.Int32? TaskDetailReviewNoteID 
		{
			get { return this._TaskDetailReviewNoteID; }
			set { this._TaskDetailReviewNoteID = value; }
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
