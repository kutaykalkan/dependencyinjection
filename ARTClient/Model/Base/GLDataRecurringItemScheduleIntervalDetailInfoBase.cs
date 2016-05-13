

using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using Adapdev.Text;

namespace SkyStem.ART.Client.Model.Base
{    

	/// <summary>
	/// An object representation of the SkyStemART GLDataRecurringItemScheduleIntervalDetail table
	/// </summary>
	[Serializable]
	public abstract class GLDataRecurringItemScheduleIntervalDetailInfoBase
	{

	
				protected System.Int64? _GLDataRecurringItemScheduleID = null;
						protected System.Int64? _GLDataRecurringItemScheduleIntervalDetailID = null;
						protected System.Decimal? _IntervalAmount = null;
						protected System.Int32? _ReconciliationPeriodID = null;
                        protected System.Decimal? _SystemIntervalAmount = null;
		


		[XmlElement(ElementName = "GLDataRecurringItemScheduleID")]
				public virtual System.Int64? GLDataRecurringItemScheduleID 
				{
			get 
			{
				return this._GLDataRecurringItemScheduleID;
			}
			set 
			{
				this._GLDataRecurringItemScheduleID = value;

			}
		}			

		[XmlElement(ElementName = "GLDataRecurringItemScheduleIntervalDetailID")]
				public virtual System.Int64? GLDataRecurringItemScheduleIntervalDetailID 
				{
			get 
			{
				return this._GLDataRecurringItemScheduleIntervalDetailID;
			}
			set 
			{
				this._GLDataRecurringItemScheduleIntervalDetailID = value;

			}
		}			

		[XmlElement(ElementName = "IntervalAmount")]
				public virtual System.Decimal? IntervalAmount 
				{
			get 
			{
				return this._IntervalAmount;
			}
			set 
			{
				this._IntervalAmount = value;

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

			}
		}			

        
		/// <summary>
		/// Returns a string representation of the object, displaying all property and field names and values.
		/// </summary>
		public override string ToString() 
		{
			return StringUtil.ToString(this);
		}

        [XmlElement(ElementName = "SystemIntervalAmount")]
        public virtual System.Decimal? SystemIntervalAmount
        {
            get
            {
                return this._SystemIntervalAmount;
            }
            set
            {
                this._SystemIntervalAmount = value;

            }
        }			
	
	}
}
