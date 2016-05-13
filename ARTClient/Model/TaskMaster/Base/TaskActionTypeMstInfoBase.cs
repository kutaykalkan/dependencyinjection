
using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using Adapdev.Text;
using System.Runtime.Serialization;

namespace SkyStem.ART.Client.Model.Base
{    
	/// <summary>
	/// An object representation of the SkyStemART TaskActionTypeMst table
	/// </summary>
	[DataContract]
	[Serializable]
	public abstract class TaskActionTypeMstInfoBase
	{
		protected System.String _AddedBy = null;
		protected System.DateTime? _DateAdded = null;
		protected System.DateTime? _DateRevised = null;
		protected System.String _HostName = null;
		protected System.Boolean? _IsActive = null;
		protected System.String _RevisedBy = null;
		protected System.String _TaskActionType = null;
		protected System.Int16? _TaskActionTypeID = null;
		protected System.Int32? _TaskActionTypeLabelID = null;
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
		[XmlElement(ElementName = "RevisedBy")]
		public virtual System.String RevisedBy 
		{
			get { return this._RevisedBy; }
			set { this._RevisedBy = value; }
		}			
		[DataMember]
		[XmlElement(ElementName = "TaskActionType")]
		public virtual System.String TaskActionType 
		{
			get { return this._TaskActionType; }
			set { this._TaskActionType = value; }
		}			
		[DataMember]
		[XmlElement(ElementName = "TaskActionTypeID")]
		public virtual System.Int16? TaskActionTypeID 
		{
			get { return this._TaskActionTypeID; }
			set { this._TaskActionTypeID = value; }
		}			
		[DataMember]
		[XmlElement(ElementName = "TaskActionTypeLabelID")]
		public virtual System.Int32? TaskActionTypeLabelID 
		{
			get { return this._TaskActionTypeLabelID; }
			set { this._TaskActionTypeLabelID = value; }
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
