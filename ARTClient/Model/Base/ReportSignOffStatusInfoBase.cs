

using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using Adapdev.Text;

namespace SkyStem.ART.Client.Model.Base
{    

	/// <summary>
	/// An object representation of the SkyStemArt ReportSignOffStatus table
	/// </summary>
	[Serializable]
	public abstract class ReportSignOffStatusInfoBase
	{

		protected System.Int32? _ReconciliationPeriodID = 0;
		protected System.Int16? _ReportID = 0;
		protected System.Int32? _ReportSignOffStatusID = 0;
		protected System.DateTime? _SignOffDate = DateTime.Now;
		protected System.Int32? _UserID = 0;

		public bool IsReconciliationPeriodIDNull = true;


		public bool IsReportIDNull = true;


		public bool IsReportSignOffStatusIDNull = true;


		public bool IsSignOffDateNull = true;


		public bool IsUserIDNull = true;

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

		[XmlElement(ElementName = "ReportSignOffStatusID")]
		public virtual System.Int32? ReportSignOffStatusID 
		{
			get 
			{
				return this._ReportSignOffStatusID;
			}
			set 
			{
				this._ReportSignOffStatusID = value;

									this.IsReportSignOffStatusIDNull = false;
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
