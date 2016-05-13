

using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using Adapdev.Text;

namespace SkyStem.ART.Client.Model.Base
{    

	/// <summary>
	/// An object representation of the SkyStemArt RecurringItemSchedule table
	/// </summary>
	[Serializable]
	public abstract class RecurringItemScheduleInfoBase
	{
		protected System.Decimal? _Balance = 0.00M;
		protected System.DateTime? _BeginDate = DateTime.Now;
		protected System.DateTime? _EndDate = DateTime.Now;
		protected System.Int64? _GLReconciliationItemInputID = 0;
		protected System.Int32? _RecurringItemScheduleID = 0;
		protected System.String _ScheduleName = "";




		public bool IsBalanceNull = true;


		public bool IsBeginDateNull = true;


		public bool IsEndDateNull = true;


		public bool IsGLReconciliationItemInputIDNull = true;


		public bool IsRecurringItemScheduleIDNull = true;


		public bool IsScheduleNameNull = true;

		[XmlElement(ElementName = "Balance")]
		public virtual System.Decimal? Balance 
		{
			get 
			{
				return this._Balance;
			}
			set 
			{
				this._Balance = value;

									this.IsBalanceNull = false;
							}
		}			

		[XmlElement(ElementName = "BeginDate")]
		public virtual System.DateTime? BeginDate 
		{
			get 
			{
				return this._BeginDate;
			}
			set 
			{
				this._BeginDate = value;

									this.IsBeginDateNull = (_BeginDate == DateTime.MinValue);
							}
		}			

		[XmlElement(ElementName = "EndDate")]
		public virtual System.DateTime? EndDate 
		{
			get 
			{
				return this._EndDate;
			}
			set 
			{
				this._EndDate = value;

									this.IsEndDateNull = (_EndDate == DateTime.MinValue);
							}
		}			

		[XmlElement(ElementName = "GLReconciliationItemInputID")]
		public virtual System.Int64? GLReconciliationItemInputID 
		{
			get 
			{
				return this._GLReconciliationItemInputID;
			}
			set 
			{
				this._GLReconciliationItemInputID = value;

									this.IsGLReconciliationItemInputIDNull = false;
							}
		}			

		[XmlElement(ElementName = "RecurringItemScheduleID")]
		public virtual System.Int32? RecurringItemScheduleID 
		{
			get 
			{
				return this._RecurringItemScheduleID;
			}
			set 
			{
				this._RecurringItemScheduleID = value;

									this.IsRecurringItemScheduleIDNull = false;
							}
		}			

		[XmlElement(ElementName = "ScheduleName")]
		public virtual System.String ScheduleName 
		{
			get 
			{
				return this._ScheduleName;
			}
			set 
			{
				this._ScheduleName = value;

									this.IsScheduleNameNull = (_ScheduleName == null);
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
