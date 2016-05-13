

using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using Adapdev.Text;

namespace SkyStem.ART.Client.Model.Base
{    

	/// <summary>
	/// An object representation of the SkyStemArt MandatoryReportSignOff table
	/// </summary>
	[Serializable]
	public abstract class MandatoryReportSignOffInfoBase
	{

	
		protected System.String _Comments = "";
		protected System.Int32? _MandatoryReportSignOffID = 0;
		protected System.Int32? _ReconciliationPeriodID = 0;
		protected System.Int32? _ReportRoleMandatoryReportID = 0;
		protected System.Int16? _RoleID = 0;
		protected System.DateTime? _SignOffDate = DateTime.Now;
		protected System.Int32? _UserID = 0;




		public bool IsCommentsNull = true;


		public bool IsMandatoryReportSignOffIDNull = true;


		public bool IsReconciliationPeriodIDNull = true;


		public bool IsReportRoleMandatoryReportIDNull = true;


		public bool IsRoleIDNull = true;


		public bool IsSignOffDateNull = true;


		public bool IsUserIDNull = true;

		[XmlElement(ElementName = "Comments")]
		public virtual System.String Comments 
		{
			get 
			{
				return this._Comments;
			}
			set 
			{
				this._Comments = value;

									this.IsCommentsNull = (_Comments == null);
							}
		}			

		[XmlElement(ElementName = "MandatoryReportSignOffID")]
		public virtual System.Int32? MandatoryReportSignOffID 
		{
			get 
			{
				return this._MandatoryReportSignOffID;
			}
			set 
			{
				this._MandatoryReportSignOffID = value;

									this.IsMandatoryReportSignOffIDNull = false;
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

		[XmlElement(ElementName = "ReportRoleMandatoryReportID")]
		public virtual System.Int32? ReportRoleMandatoryReportID 
		{
			get 
			{
				return this._ReportRoleMandatoryReportID;
			}
			set 
			{
				this._ReportRoleMandatoryReportID = value;

									this.IsReportRoleMandatoryReportIDNull = false;
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

		[XmlElement(ElementName = "SignOffDate")]
		public virtual System.DateTime? SignOffDate 
		{
			get 
			{
				return this._SignOffDate;
			}
			set 
			{
				this._SignOffDate = value;

									this.IsSignOffDateNull = (_SignOffDate == DateTime.MinValue);
							}
		}			

		[XmlElement(ElementName = "UserID")]
		public virtual System.Int32? UserID 
		{
			get 
			{
				return this._UserID;
			}
			set 
			{
				this._UserID = value;

									this.IsUserIDNull = false;
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
