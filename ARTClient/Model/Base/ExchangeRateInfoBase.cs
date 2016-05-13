

using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using Adapdev.Text;

namespace SkyStem.ART.Client.Model.Base
{    

	/// <summary>
	/// An object representation of the SkyStemArt ExchangeRate table
	/// </summary>
	[Serializable]
	public abstract class ExchangeRateInfoBase
	{

	
		protected System.String _AddedBy = "";
		protected System.DateTime? _DateAdded = DateTime.Now;
		protected System.DateTime? _DateRevised = DateTime.Now;
		protected System.Decimal? _ExchangeRate = 0.00M;
		protected System.Int32? _ExchangeRateID = 0;
		protected System.String _FromCurrencyCode = "";
		protected System.String _HostName = "";
		protected System.Boolean? _IsActive = false;
		protected System.Int32? _ReconciliationPeriodID = 0;
		protected System.String _RevisedBy = "";
		protected System.String _ToCurrencyCode = "";




		public bool IsAddedByNull = true;


		public bool IsDateAddedNull = true;


		public bool IsDateRevisedNull = true;


		public bool IsExchangeRateNull = true;


		public bool IsExchangeRateIDNull = true;


		public bool IsFromCurrencyCodeNull = true;


		public bool IsHostNameNull = true;


		public bool IsIsActiveNull = true;


		public bool IsReconciliationPeriodIDNull = true;


		public bool IsRevisedByNull = true;


		public bool IsToCurrencyCodeNull = true;

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

		[XmlElement(ElementName = "ExchangeRate")]
		public virtual System.Decimal? ExchangeRate 
		{
			get 
			{
				return this._ExchangeRate;
			}
			set 
			{
				this._ExchangeRate = value;

									this.IsExchangeRateNull = false;
							}
		}			

		[XmlElement(ElementName = "ExchangeRateID")]
		public virtual System.Int32? ExchangeRateID 
		{
			get 
			{
				return this._ExchangeRateID;
			}
			set 
			{
				this._ExchangeRateID = value;

									this.IsExchangeRateIDNull = false;
							}
		}			

		[XmlElement(ElementName = "FromCurrencyCode")]
		public virtual System.String FromCurrencyCode 
		{
			get 
			{
				return this._FromCurrencyCode;
			}
			set 
			{
				this._FromCurrencyCode = value;

									this.IsFromCurrencyCodeNull = (_FromCurrencyCode == null);
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

		[XmlElement(ElementName = "ReconciliationPeriodID")]
		public virtual System.Int32? ReconciliationPeriodID 
		{
			get 
			{
				return this._ReconciliationPeriodID;
			}
			set 
			{
				this._ReconciliationPeriodID = value;

									this.IsReconciliationPeriodIDNull = false;
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

		[XmlElement(ElementName = "ToCurrencyCode")]
		public virtual System.String ToCurrencyCode 
		{
			get 
			{
				return this._ToCurrencyCode;
			}
			set 
			{
				this._ToCurrencyCode = value;

									this.IsToCurrencyCodeNull = (_ToCurrencyCode == null);
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
