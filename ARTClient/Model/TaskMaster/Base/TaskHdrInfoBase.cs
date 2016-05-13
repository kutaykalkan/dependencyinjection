
using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using Adapdev.Text;
using System.Runtime.Serialization;

namespace SkyStem.ART.Client.Model.Base
{    
	/// <summary>
	/// An object representation of the SkyStemART TaskHdr table
	/// </summary>
	[DataContract]
	[Serializable]
    public abstract class TaskHdrInfoBase : OrganizationalHierarchyInfo
	{
		protected System.String _AddedBy = null;
		protected System.Int32? _DataImportID = null;
		protected System.DateTime? _DateAdded = null;
		protected System.DateTime? _DateRevised = null;
		protected System.String _HostName = null;
		protected System.Boolean? _IsActive = null;
		protected System.Int32? _RecPeriodID = null;
		protected System.String _RevisedBy = null;
		protected System.Int64? _TaskID = null;
		protected System.String _TaskNumber = null;
		protected System.Int16? _TaskTypeID = null;
		
        [DataMember]
		[XmlElement(ElementName = "AddedBy")]
		public virtual System.String AddedBy 
		{
			get { return this._AddedBy; }
			set { this._AddedBy = value; }
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
		[XmlElement(ElementName = "RecPeriodID")]
		public virtual System.Int32? RecPeriodID 
		{
			get { return this._RecPeriodID; }
			set { this._RecPeriodID = value; }
		}			
		[DataMember]
		[XmlElement(ElementName = "RevisedBy")]
		public virtual System.String RevisedBy 
		{
			get { return this._RevisedBy; }
			set { this._RevisedBy = value; }
		}			
		[DataMember]
		[XmlElement(ElementName = "TaskID")]
		public virtual System.Int64? TaskID 
		{
			get { return this._TaskID; }
			set { this._TaskID = value; }
		}			
		[DataMember]
		[XmlElement(ElementName = "TaskNumber")]
		public virtual System.String TaskNumber 
		{
			get { return this._TaskNumber; }
			set { this._TaskNumber = value; }
		}			
		[DataMember]
		[XmlElement(ElementName = "TaskTypeID")]
		public virtual System.Int16? TaskTypeID 
		{
			get { return this._TaskTypeID; }
			set { this._TaskTypeID = value; }
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
