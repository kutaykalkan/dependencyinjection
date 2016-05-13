

using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using Adapdev.Text;

namespace SkyStem.ART.Client.Model.Base
{    

	/// <summary>
	/// An object representation of the SkyStemArt DataImportFailureMessage table
	/// </summary>
	[Serializable]
	public abstract class DataImportFailureMessageInfoBase
	{
		protected System.String _AddedBy = "";
		protected System.Int32? _DataImportFailureMessageID = 0;
		protected System.Int32? _DataImportID = 0;
		protected System.DateTime? _DateAdded = DateTime.Now;
		protected System.String _FailureMessage = "";
		protected System.String _HostName = "";
		protected System.Boolean? _IsActive = false;




		public bool IsAddedByNull = true;


		public bool IsDataImportFailureMessageIDNull = true;


		public bool IsDataImportIDNull = true;


		public bool IsDateAddedNull = true;


		public bool IsFailureMessageNull = true;


		public bool IsHostNameNull = true;


		public bool IsIsActiveNull = true;

		[XmlElement(ElementName = "AddedBy")]
		public virtual System.String AddedBy 
		{
			get 
			{
				return this._AddedBy;
			}
			set 
			{
				this._AddedBy = value;

									this.IsAddedByNull = (_AddedBy == null);
							}
		}			

		[XmlElement(ElementName = "DataImportFailureMessageID")]
		public virtual System.Int32? DataImportFailureMessageID 
		{
			get 
			{
				return this._DataImportFailureMessageID;
			}
			set 
			{
				this._DataImportFailureMessageID = value;

									this.IsDataImportFailureMessageIDNull = false;
							}
		}			

		[XmlElement(ElementName = "DataImportID")]
		public virtual System.Int32? DataImportID 
		{
			get 
			{
				return this._DataImportID;
			}
			set 
			{
				this._DataImportID = value;

									this.IsDataImportIDNull = false;
							}
		}			

		[XmlElement(ElementName = "DateAdded")]
		public virtual System.DateTime? DateAdded 
		{
			get 
			{
				return this._DateAdded;
			}
			set 
			{
				this._DateAdded = value;

									this.IsDateAddedNull = (_DateAdded == DateTime.MinValue);
							}
		}			

		[XmlElement(ElementName = "FailureMessage")]
		public virtual System.String FailureMessage 
		{
			get 
			{
				return this._FailureMessage;
			}
			set 
			{
				this._FailureMessage = value;

									this.IsFailureMessageNull = (_FailureMessage == null);
							}
		}			

		[XmlElement(ElementName = "HostName")]
		public virtual System.String HostName 
		{
			get 
			{
				return this._HostName;
			}
			set 
			{
				this._HostName = value;

									this.IsHostNameNull = (_HostName == null);
							}
		}			

		[XmlElement(ElementName = "IsActive")]
		public virtual System.Boolean? IsActive 
		{
			get 
			{
				return this._IsActive;
			}
			set 
			{
				this._IsActive = value;

									this.IsIsActiveNull = false;
							}
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
