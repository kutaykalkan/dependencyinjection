
using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using Adapdev.Text;
using System.Runtime.Serialization;

namespace SkyStem.ART.Client.Model.Base
{    
	/// <summary>
	/// An object representation of the SkyStemART DataImportMultilingualUpload table
	/// </summary>
	[DataContract]
	[Serializable]
	public abstract class DataImportMultilingualUploadInfoBase
	{
		protected System.Int32? _DataImportID = null;
		protected System.Int32? _DataImportMultilingualUploadID = null;
		protected System.Int32? _FromLanguageID = null;
		protected System.Int32? _ToLanguageID = null;
		[DataMember]
		[XmlElement(ElementName = "DataImportID")]
		public virtual System.Int32? DataImportID 
		{
			get { return this._DataImportID; }
			set { this._DataImportID = value; }
		}			
		[DataMember]
		[XmlElement(ElementName = "DataImportMultilingualUploadID")]
		public virtual System.Int32? DataImportMultilingualUploadID 
		{
			get { return this._DataImportMultilingualUploadID; }
			set { this._DataImportMultilingualUploadID = value; }
		}			
		[DataMember]
		[XmlElement(ElementName = "FromLanguageID")]
		public virtual System.Int32? FromLanguageID 
		{
			get { return this._FromLanguageID; }
			set { this._FromLanguageID = value; }
		}			
		[DataMember]
		[XmlElement(ElementName = "ToLanguageID")]
		public virtual System.Int32? ToLanguageID 
		{
			get { return this._ToLanguageID; }
			set { this._ToLanguageID = value; }
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
