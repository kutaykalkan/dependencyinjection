
using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using Adapdev.Text;
using System.Runtime.Serialization;

namespace SkyStem.ART.Client.Model.Base
{    
	/// <summary>
	/// An object representation of the SkyStemART TaskDetailReviewNoteDetail table
	/// </summary>
	[DataContract]
	[Serializable]
	public abstract class TaskDetailReviewNoteDetailInfoBase
	{
		protected System.String _AddedBy = null;
		protected System.Int32? _AddedByUserID = null;
		protected System.DateTime? _DateAdded = null;
		protected System.Boolean? _IsActive = null;
		protected System.String _ReviewNote = null;
		protected System.Int32? _TaskDetailReviewNoteDetailID = null;
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
		[XmlElement(ElementName = "IsActive")]
		public virtual System.Boolean? IsActive 
		{
			get { return this._IsActive; }
			set { this._IsActive = value; }
		}			
		[DataMember]
		[XmlElement(ElementName = "ReviewNote")]
		public virtual System.String ReviewNote 
		{
			get { return this._ReviewNote; }
			set { this._ReviewNote = value; }
		}			
		[DataMember]
		[XmlElement(ElementName = "TaskDetailReviewNoteDetailID")]
		public virtual System.Int32? TaskDetailReviewNoteDetailID 
		{
			get { return this._TaskDetailReviewNoteDetailID; }
			set { this._TaskDetailReviewNoteDetailID = value; }
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
