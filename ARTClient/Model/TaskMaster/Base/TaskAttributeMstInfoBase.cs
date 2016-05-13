
using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using Adapdev.Text;
using System.Runtime.Serialization;

namespace SkyStem.ART.Client.Model.Base
{    
	/// <summary>
	/// An object representation of the SkyStemART TaskAttributeMst table
	/// </summary>
	[DataContract]
	[Serializable]
	public abstract class TaskAttributeMstInfoBase
	{
		protected System.String _AddedBy = null;
		protected System.DateTime? _DateAdded = null;
		protected System.DateTime? _DateRevised = null;
		protected System.String _HostName = null;
		protected System.Boolean? _IsActive = null;
		protected System.String _RevisedBy = null;
		protected System.String _TaskAttribute = null;
		protected System.Int16? _TaskAttributeID = null;
		protected System.Int32? _TaskAttributeLabelID = null;
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
		[XmlElement(ElementName = "TaskAttribute")]
		public virtual System.String TaskAttribute 
		{
			get { return this._TaskAttribute; }
			set { this._TaskAttribute = value; }
		}			
		[DataMember]
		[XmlElement(ElementName = "TaskAttributeID")]
		public virtual System.Int16? TaskAttributeID 
		{
			get { return this._TaskAttributeID; }
			set { this._TaskAttributeID = value; }
		}			
		[DataMember]
		[XmlElement(ElementName = "TaskAttributeLabelID")]
		public virtual System.Int32? TaskAttributeLabelID 
		{
			get { return this._TaskAttributeLabelID; }
			set { this._TaskAttributeLabelID = value; }
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
