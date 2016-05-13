
using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using Adapdev.Text;
using System.Runtime.Serialization;

namespace SkyStem.ART.Client.Model.Base
{    
	/// <summary>
	/// An object representation of the SkyStemART SystemLockdown table
	/// </summary>
	[DataContract]
	[Serializable]
	public abstract class SystemLockdownInfoBase
	{
		protected System.Int64? _AccountID = null;
		protected System.String _AddedBy = null;
		protected System.Int32? _CompanyID = null;
		protected System.Int32? _DataImportID = null;
		protected System.DateTime? _DateAdded = null;
		protected System.Int32? _RecPeriodID = null;
		protected System.Int32? _SystemLockdownID = null;
		protected System.String _SystemLockdownMessage = null;
		protected System.Int16? _SystemLockdownReasonID = null;
		[DataMember]
		[XmlElement(ElementName = "AccountID")]
		public virtual System.Int64? AccountID 
		{
			get { return this._AccountID; }
			set { this._AccountID = value; }
		}			
		[DataMember]
		[XmlElement(ElementName = "AddedBy")]
		public virtual System.String AddedBy 
		{
			get { return this._AddedBy; }
			set { this._AddedBy = value; }
		}			
		[DataMember]
		[XmlElement(ElementName = "CompanyID")]
		public virtual System.Int32? CompanyID 
		{
			get { return this._CompanyID; }
			set { this._CompanyID = value; }
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
		[XmlElement(ElementName = "RecPeriodID")]
		public virtual System.Int32? RecPeriodID 
		{
			get { return this._RecPeriodID; }
			set { this._RecPeriodID = value; }
		}			
		[DataMember]
		[XmlElement(ElementName = "SystemLockdownID")]
		public virtual System.Int32? SystemLockdownID 
		{
			get { return this._SystemLockdownID; }
			set { this._SystemLockdownID = value; }
		}			
		[DataMember]
		[XmlElement(ElementName = "SystemLockdownMessage")]
		public virtual System.String SystemLockdownMessage 
		{
			get { return this._SystemLockdownMessage; }
			set { this._SystemLockdownMessage = value; }
		}			
		[DataMember]
		[XmlElement(ElementName = "SystemLockdownReasonID")]
		public virtual System.Int16? SystemLockdownReasonID 
		{
			get { return this._SystemLockdownReasonID; }
			set { this._SystemLockdownReasonID = value; }
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
