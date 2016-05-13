

using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using Adapdev.Text;

namespace SkyStem.ART.Client.Model.Base
{    

	/// <summary>
	/// An object representation of the SkyStemArt DerivedCalculationSupportingDetail table
	/// </summary>
	[Serializable]
	public abstract class DerivedCalculationSupportingDetailInfoBase
	{

	
		protected System.Decimal? _BaseCurrencyBalance = null;
		protected System.String _BasisForDerivedCalculation = "";
		protected System.Int32? _DerivedCalculationSupportingDetailID = 0;
		protected System.Int64? _GLDataID = 0;
		protected System.Decimal? _ReportingCurrencyBalance = null;




		public bool IsBaseCurrencyBalanceNull = true;


		public bool IsBasisForDerivedCalculationNull = true;


		public bool IsDerivedCalculationSupportingDetailIDNull = true;


		public bool IsGLDataIDNull = true;


		public bool IsReportingCurrencyBalanceNull = true;

		[XmlElement(ElementName = "BaseCurrencyBalance")]
		public virtual System.Decimal? BaseCurrencyBalance 
		{
			get 
			{
				return this._BaseCurrencyBalance;
			}
			set 
			{
				this._BaseCurrencyBalance = value;

									this.IsBaseCurrencyBalanceNull = false;
							}
		}			

		[XmlElement(ElementName = "BasisForDerivedCalculation")]
		public virtual System.String BasisForDerivedCalculation 
		{
			get 
			{
				return this._BasisForDerivedCalculation;
			}
			set 
			{
				this._BasisForDerivedCalculation = value;

									this.IsBasisForDerivedCalculationNull = (_BasisForDerivedCalculation == null);
							}
		}			

		[XmlElement(ElementName = "DerivedCalculationSupportingDetailID")]
		public virtual System.Int32? DerivedCalculationSupportingDetailID 
		{
			get 
			{
				return this._DerivedCalculationSupportingDetailID;
			}
			set 
			{
				this._DerivedCalculationSupportingDetailID = value;

									this.IsDerivedCalculationSupportingDetailIDNull = false;
							}
		}			

		[XmlElement(ElementName = "GLDataID")]
		public virtual System.Int64? GLDataID 
		{
			get 
			{
				return this._GLDataID;
			}
			set 
			{
				this._GLDataID = value;

									this.IsGLDataIDNull = false;
							}
		}			

		[XmlElement(ElementName = "ReportingCurrencyBalance")]
		public virtual System.Decimal? ReportingCurrencyBalance 
		{
			get 
			{
				return this._ReportingCurrencyBalance;
			}
			set 
			{
				this._ReportingCurrencyBalance = value;

									this.IsReportingCurrencyBalanceNull = false;
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
