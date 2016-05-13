

using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using Adapdev.Text;

namespace SkyStem.ART.Client.Model.Base
{    

	/// <summary>
	/// An object representation of the SkyStemArt DataImportTypeCapability table
	/// </summary>
	[Serializable]
	public abstract class DataImportTypeCapabilityInfoBase
	{

	
		protected System.String _AddedBy = "";
		protected System.Int16? _CapabilityID = 0;
		protected System.Int16? _DataImportTypeCapabilityID = 0;
		protected System.Int16? _DataImportTypeID = 0;
		protected System.DateTime? _DateAdded = DateTime.Now;
		protected System.DateTime? _DateRevised = DateTime.Now;
		protected System.String _HostName = "";
		protected System.Boolean? _IsActive = false;
		protected System.String _RevisedBy = "";




		public bool IsAddedByNull = true;


		public bool IsCapabilityIDNull = true;


		public bool IsDataImportTypeCapabilityIDNull = true;


		public bool IsDataImportTypeIDNull = true;


		public bool IsDateAddedNull = true;


		public bool IsDateRevisedNull = true;


		public bool IsHostNameNull = true;


		public bool IsIsActiveNull = true;


		public bool IsRevisedByNull = true;

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

		[XmlElement(ElementName = "CapabilityID")]
		public virtual System.Int16? CapabilityID 
		{
			get 
			{
				return this._CapabilityID;
			}
			set 
			{
				this._CapabilityID = value;

									this.IsCapabilityIDNull = false;
							}
		}			

		[XmlElement(ElementName = "DataImportTypeCapabilityID")]
		public virtual System.Int16? DataImportTypeCapabilityID 
		{
			get 
			{
				return this._DataImportTypeCapabilityID;
			}
			set 
			{
				this._DataImportTypeCapabilityID = value;

									this.IsDataImportTypeCapabilityIDNull = false;
							}
		}			

		[XmlElement(ElementName = "DataImportTypeID")]
		public virtual System.Int16? DataImportTypeID 
		{
			get 
			{
				return this._DataImportTypeID;
			}
			set 
			{
				this._DataImportTypeID = value;

									this.IsDataImportTypeIDNull = false;
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

		[XmlElement(ElementName = "DateRevised")]
		public virtual System.DateTime? DateRevised 
		{
			get 
			{
				return this._DateRevised;
			}
			set 
			{
				this._DateRevised = value;

									this.IsDateRevisedNull = (_DateRevised == DateTime.MinValue);
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

		[XmlElement(ElementName = "RevisedBy")]
		public virtual System.String RevisedBy 
		{
			get 
			{
				return this._RevisedBy;
			}
			set 
			{
				this._RevisedBy = value;

									this.IsRevisedByNull = (_RevisedBy == null);
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
