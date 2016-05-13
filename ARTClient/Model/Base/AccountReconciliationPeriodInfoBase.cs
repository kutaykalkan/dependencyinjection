

using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using Adapdev.Text;

namespace SkyStem.ART.Client.Model.Base
{    

	/// <summary>
	/// An object representation of the SkyStemArt AccountReconciliationPeriod table
	/// </summary>
	[Serializable]
	public abstract class AccountReconciliationPeriodInfoBase
	{
		protected System.Int64? _AccountID = 0;
		protected System.Int32? _AccountReconciliationPeriodID = 0;
		protected System.Int32? _ReconciliationPeriodID = 0;
		public bool IsAccountIDNull = true;
		public bool IsAccountReconciliationPeriodIDNull = true;
		public bool IsReconciliationPeriodIDNull = true;

		[XmlElement(ElementName = "AccountID")]
		public virtual System.Int64? AccountID 
		{
			get 
			{
				return this._AccountID;
			}
			set 
			{
				this._AccountID = value;

									this.IsAccountIDNull = false;
							}
		}			

		[XmlElement(ElementName = "AccountReconciliationPeriodID")]
		public virtual System.Int32? AccountReconciliationPeriodID 
		{
			get 
			{
				return this._AccountReconciliationPeriodID;
			}
			set 
			{
				this._AccountReconciliationPeriodID = value;

									this.IsAccountReconciliationPeriodIDNull = false;
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

        
		/// <summary>
		/// Returns a string representation of the object, displaying all property and field names and values.
		/// </summary>
		public override string ToString() 
		{
			return StringUtil.ToString(this);
		}		
	
	
			
				
	}
}
