
using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using Adapdev.Text;
using System.Runtime.Serialization;

namespace SkyStem.ART.Client.Model.Base
{    
	/// <summary>
    /// An object representation of the SkyStemART DataImportRecItemUpload table
	/// </summary>
	[DataContract]
	[Serializable]
    public abstract class DataImportRecItemUploadInfoBase
	{
		protected System.Int32? _DataImportID = null;
        protected System.Int32? _DataImportRecItemUploadID = null;
		protected System.Int64? _GLDataID = null;
        protected System.Int16? _ReconciliationCategoryID = null;
        protected System.Int16? _ReconciliationCategoryTypeID = null;
        [DataMember]
		[XmlElement(ElementName = "DataImportID")]
		public virtual System.Int32? DataImportID 
		{
			get { return this._DataImportID; }
			set { this._DataImportID = value; }
		}			
		[DataMember]
        [XmlElement(ElementName = "DataImportRecItemUploadID")]
        public virtual System.Int32? DataImportRecItemUploadID 
		{
			get { return this._DataImportRecItemUploadID; }
            set { this._DataImportRecItemUploadID = value; }
		}			
		[DataMember]
        [XmlElement(ElementName = "GLDataID")]
		public virtual System.Int64? GLDataID 
		{
			get { return this._GLDataID; }
			set { this._GLDataID = value; }
		}			
		[DataMember]
        [XmlElement(ElementName = "ReconciliationCategoryID")]
        public virtual System.Int16? ReconciliationCategoryID 
		{
            get { return this._ReconciliationCategoryID; }
            set { this._ReconciliationCategoryID = value; }
		}
        [DataMember]
        [XmlElement(ElementName = "ReconciliationCategoryTypeID")]
        public virtual System.Int16? ReconciliationCategoryTypeID
        {
            get { return this._ReconciliationCategoryTypeID; }
            set { this._ReconciliationCategoryTypeID = value; }
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
