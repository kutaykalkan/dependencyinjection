using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using Adapdev.Text;

namespace SkyStem.ART.Client.Model.Base
{    

	/// <summary>
	/// An object representation of the SkyStemART AppSettings table
	/// </summary>
	[Serializable]
	public abstract class AppSettingsInfoBase
	{

	
		protected System.String _AddedBy = "";
		protected System.Int32? _AppSettingID = 0;
		protected System.String _AppSettingName = "";
		protected System.String _AppSettingValue = "";
		protected System.DateTime? _DateAdded = DateTime.Now;
		protected System.DateTime? _DateRevised = DateTime.Now;
		protected System.String _HostName = "";
		protected System.Boolean? _IsActive = false;
		protected System.String _RevisedBy = "";




		public bool IsAddedByNull = true;


		public bool IsAppSettingIDNull = true;


		public bool IsAppSettingNameNull = true;


		public bool IsAppSettingValueNull = true;


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

		[XmlElement(ElementName = "AppSettingID")]
		public virtual System.Int32? AppSettingID 
		{
			get 
			{
				return this._AppSettingID;
			}
			set 
			{
				this._AppSettingID = value;
				this.IsAppSettingIDNull = false;
							}
		}			

		[XmlElement(ElementName = "AppSettingName")]
		public virtual System.String AppSettingName 
		{
			get 
			{
				return this._AppSettingName;
			}
			set 
			{
				this._AppSettingName = value;
				this.IsAppSettingNameNull = (_AppSettingName == null);
							}
		}			

		[XmlElement(ElementName = "AppSettingValue")]
		public virtual System.String AppSettingValue 
		{
			get 
			{
				return this._AppSettingValue;
			}
			set 
			{
				this._AppSettingValue = value;
				this.IsAppSettingValueNull = (_AppSettingValue == null);
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
