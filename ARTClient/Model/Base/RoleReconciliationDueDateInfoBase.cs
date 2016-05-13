

using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using Adapdev.Text;

namespace SkyStem.ART.Client.Model.Base
{    

	/// <summary>
	/// An object representation of the SkyStemArt RoleReconciliationDueDate table
	/// </summary>
	[Serializable]
	public abstract class RoleReconciliationDueDateInfoBase
	{

		protected System.DateTime? _ReconciliationDueDate = DateTime.Now;
		protected System.Int32? _ReconciliationPeriodID = 0;
		protected System.Int16? _RoleID = 0;
		protected System.Int32? _RoleReconciliationDueDateID = 0;


		public bool IsReconciliationDueDateNull = true;


		public bool IsReconciliationPeriodIDNull = true;


		public bool IsRoleIDNull = true;


		public bool IsRoleReconciliationDueDateIDNull = true;

		[XmlElement(ElementName = "ReconciliationDueDate")]
		public virtual System.DateTime? ReconciliationDueDate 
		{
			get 
			{
				return this._ReconciliationDueDate;
			}
			set 
			{
				this._ReconciliationDueDate = value;

									this.IsReconciliationDueDateNull = (_ReconciliationDueDate == DateTime.MinValue);
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

		[XmlElement(ElementName = "RoleID")]
		public virtual System.Int16? RoleID 
		{
			get 
			{
				return this._RoleID;
			}
			set 
			{
				this._RoleID = value;

									this.IsRoleIDNull = false;
							}
		}			

		[XmlElement(ElementName = "RoleReconciliationDueDateID")]
		public virtual System.Int32? RoleReconciliationDueDateID 
		{
			get 
			{
				return this._RoleReconciliationDueDateID;
			}
			set 
			{
				this._RoleReconciliationDueDateID = value;

									this.IsRoleReconciliationDueDateIDNull = false;
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
