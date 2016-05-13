

using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using Adapdev.Text;

namespace SkyStem.ART.Client.Model.Base
{    

	/// <summary>
	/// An object representation of the SkyStemArt RoleMandatoryReport table
	/// </summary>
	[Serializable]
	public abstract class RoleMandatoryReportInfoBase
	{

		protected System.String _AddedBy = "";
		protected System.Int32? _CompanyID = 0;
		protected System.DateTime? _DateAdded = DateTime.Now;
		protected System.Int32? _EndReconciliationPeriodID = 0;
		protected System.Int16? _ReportID = 0;
		protected System.Int16? _RoleID = 0;
		protected System.Int32? _RoleMandatoryReportID = 0;
		protected System.Int32? _StartReconciliationPeriodID = 0;

		public bool IsAddedByNull = true;


		public bool IsCompanyIDNull = true;


		public bool IsDateAddedNull = true;


		public bool IsEndReconciliationPeriodIDNull = true;


		public bool IsReportIDNull = true;


		public bool IsRoleIDNull = true;


		public bool IsRoleMandatoryReportIDNull = true;


		public bool IsStartReconciliationPeriodIDNull = true;

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

		[XmlElement(ElementName = "CompanyID")]
		public virtual System.Int32? CompanyID 
		{
			get 
			{
				return this._CompanyID;
			}
			set 
			{
				this._CompanyID = value;

									this.IsCompanyIDNull = false;
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

		[XmlElement(ElementName = "EndReconciliationPeriodID")]
		public virtual System.Int32? EndReconciliationPeriodID 
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

		[XmlElement(ElementName = "ReportID")]
		public virtual System.Int16? ReportID 
		{
			get 
			{
				return this._ReportID;
			}
			set 
			{
				this._ReportID = value;

									this.IsReportIDNull = false;
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

		[XmlElement(ElementName = "RoleMandatoryReportID")]
		public virtual System.Int32? RoleMandatoryReportID 
		{
			get 
			{
				return this._RoleMandatoryReportID;
			}
			set 
			{
				this._RoleMandatoryReportID = value;

									this.IsRoleMandatoryReportIDNull = false;
							}
		}			

		[XmlElement(ElementName = "StartReconciliationPeriodID")]
		public virtual System.Int32? StartReconciliationPeriodID 
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

        
		/// <summary>
		/// Returns a string representation of the object, displaying all property and field names and values.
		/// </summary>
		public override string ToString() 
		{
			return StringUtil.ToString(this);
		}		
	
	}
}
