

using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using Adapdev.Text;

namespace SkyStem.ART.Client.Model.Base
{    

	/// <summary>
	/// An object representation of the SkyStemART NetAccountAttributeValue table
	/// </summary>
	[Serializable]
	public abstract class NetAccountAttributeValueInfoBase
	{

		protected System.Int16 _AccountAttributeID = 0;
		protected System.String _AddedBy = "";
		protected System.Int32 _DataImportID = 0;
		protected System.DateTime _DateAdded = DateTime.Now;
		protected System.Int32 _EndReconciliationPeriodID = 0;
		protected System.String _HostName = "";
		protected System.Int64 _NetAccountAttributeValueID = 0;
		protected System.Int32 _NetAccountID = 0;
		protected System.Int32 _StartReconciliationPeriodID = 0;
		protected System.String _Value = "";
		protected System.Int32 _ValueLabelID = 0;

		public bool IsAccountAttributeIDNull = true;


		public bool IsAddedByNull = true;


		public bool IsDataImportIDNull = true;


		public bool IsDateAddedNull = true;


		public bool IsEndReconciliationPeriodIDNull = true;


		public bool IsHostNameNull = true;


		public bool IsNetAccountAttributeValueIDNull = true;


		public bool IsNetAccountIDNull = true;


		public bool IsStartReconciliationPeriodIDNull = true;


		public bool IsValueNull = true;


		public bool IsValueLabelIDNull = true;

		[XmlElement(ElementName = "AccountAttributeID")]
		public virtual System.Int16 AccountAttributeID 
		{
			get 
			{
				return this._AccountAttributeID;
			}
			set 
			{
				this._AccountAttributeID = value;

									this.IsAccountAttributeIDNull = false;
							}
		}			

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

		[XmlElement(ElementName = "DataImportID")]
		public virtual System.Int32 DataImportID 
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
		public virtual System.DateTime DateAdded 
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

		[XmlElement(ElementName = "EndReconciliationPeriodID")]
		public virtual System.Int32 EndReconciliationPeriodID 
		{
			get 
			{
				return this._EndReconciliationPeriodID;
			}
			set 
			{
				this._EndReconciliationPeriodID = value;

									this.IsEndReconciliationPeriodIDNull = false;
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

		[XmlElement(ElementName = "NetAccountAttributeValueID")]
		public virtual System.Int64 NetAccountAttributeValueID 
		{
			get 
			{
				return this._NetAccountAttributeValueID;
			}
			set 
			{
				this._NetAccountAttributeValueID = value;

									this.IsNetAccountAttributeValueIDNull = false;
							}
		}			

		[XmlElement(ElementName = "NetAccountID")]
		public virtual System.Int32 NetAccountID 
		{
			get 
			{
				return this._NetAccountID;
			}
			set 
			{
				this._NetAccountID = value;

									this.IsNetAccountIDNull = false;
							}
		}			

		[XmlElement(ElementName = "StartReconciliationPeriodID")]
		public virtual System.Int32 StartReconciliationPeriodID 
		{
			get 
			{
				return this._StartReconciliationPeriodID;
			}
			set 
			{
				this._StartReconciliationPeriodID = value;

									this.IsStartReconciliationPeriodIDNull = false;
							}
		}			

		[XmlElement(ElementName = "Value")]
		public virtual System.String Value 
		{
			get 
			{
				return this._Value;
			}
			set 
			{
				this._Value = value;

									this.IsValueNull = (_Value == null);
							}
		}			

		[XmlElement(ElementName = "ValueLabelID")]
		public virtual System.Int32 ValueLabelID 
		{
			get 
			{
				return this._ValueLabelID;
			}
			set 
			{
				this._ValueLabelID = value;

									this.IsValueLabelIDNull = false;
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
