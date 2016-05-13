
using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using Adapdev.Text;
using System.Runtime.Serialization;

namespace SkyStem.ART.Client.Model.Base
{    
	/// <summary>
	/// An object representation of the SkyStemART TaskCompletionStatusMst table
	/// </summary>
	[DataContract]
	[Serializable]
	public abstract class TaskCompletionStatusMstInfoBase
	{
		protected System.String _AddedBy = null;
		protected System.String _CompletionStatus = null;
		protected System.Int32? _CompletionStatusLabelID = null;
		protected System.DateTime? _DateAdded = null;
		protected System.DateTime? _DateRevised = null;
		protected System.String _HostName = null;
		protected System.Boolean? _IsActive = null;
		protected System.String _RevisedBy = null;
		protected System.Int16? _TaskCompletionStatusID = null;
		[DataMember]
		[XmlElement(ElementName = "AddedBy")]
		public virtual System.String AddedBy 
		{
			get { return this._AddedBy; }
			set { this._AddedBy = value; }
		}			
		[DataMember]
		[XmlElement(ElementName = "CompletionStatus")]
		public virtual System.String CompletionStatus 
		{
			get { return this._CompletionStatus; }
			set { this._CompletionStatus = value; }
		}			
		[DataMember]
		[XmlElement(ElementName = "CompletionStatusLabelID")]
		public virtual System.Int32? CompletionStatusLabelID 
		{
			get { return this._CompletionStatusLabelID; }
			set { this._CompletionStatusLabelID = value; }
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
		[XmlElement(ElementName = "TaskCompletionStatusID")]
		public virtual System.Int16? TaskCompletionStatusID 
		{
			get { return this._TaskCompletionStatusID; }
			set { this._TaskCompletionStatusID = value; }
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
