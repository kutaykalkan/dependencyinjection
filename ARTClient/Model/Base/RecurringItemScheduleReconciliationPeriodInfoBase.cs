

using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using Adapdev.Text;

namespace SkyStem.ART.Client.Model.Base
{    

	/// <summary>
	/// An object representation of the SkyStemArt RecurringItemScheduleReconciliationPeriod table
	/// </summary>
	[Serializable]
	public abstract class RecurringItemScheduleReconciliationPeriodInfoBase
	{

	
		protected System.Decimal? _AccruedAmount = 0.00M;
		protected System.Int32? _AccurableItemScheduleID = 0;
		protected System.Decimal? _ActualAmount = 0.00M;
		protected System.DateTime? _Date = DateTime.Now;
		protected System.Int32? _ReconciliationPeriodID = 0;
		protected System.Int32? _RecurringItemScheduleReconciliationPeriodID = 0;




		public bool IsAccruedAmountNull = true;


		public bool IsAccurableItemScheduleIDNull = true;


		public bool IsActualAmountNull = true;


		public bool IsDateNull = true;


		public bool IsReconciliationPeriodIDNull = true;


		public bool IsRecurringItemScheduleReconciliationPeriodIDNull = true;

		[XmlElement(ElementName = "AccruedAmount")]
		public virtual System.Decimal? AccruedAmount 
		{
			get 
			{
				return this._AccruedAmount;
			}
			set 
			{
				this._AccruedAmount = value;

									this.IsAccruedAmountNull = false;
							}
		}			

		[XmlElement(ElementName = "AccurableItemScheduleID")]
		public virtual System.Int32? AccurableItemScheduleID 
		{
			get 
			{
				return this._AccurableItemScheduleID;
			}
			set 
			{
				this._AccurableItemScheduleID = value;

									this.IsAccurableItemScheduleIDNull = false;
							}
		}			

		[XmlElement(ElementName = "ActualAmount")]
		public virtual System.Decimal? ActualAmount 
		{
			get 
			{
				return this._ActualAmount;
			}
			set 
			{
				this._ActualAmount = value;

									this.IsActualAmountNull = false;
							}
		}			

		[XmlElement(ElementName = "Date")]
		public virtual System.DateTime? Date 
		{
			get 
			{
				return this._Date;
			}
			set 
			{
				this._Date = value;

									this.IsDateNull = (_Date == DateTime.MinValue);
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

		[XmlElement(ElementName = "RecurringItemScheduleReconciliationPeriodID")]
		public virtual System.Int32? RecurringItemScheduleReconciliationPeriodID 
		{
			get 
			{
				return this._RecurringItemScheduleReconciliationPeriodID;
			}
			set 
			{
				this._RecurringItemScheduleReconciliationPeriodID = value;

									this.IsRecurringItemScheduleReconciliationPeriodIDNull = false;
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
