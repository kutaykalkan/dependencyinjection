

using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using Adapdev.Text;

namespace SkyStem.ART.Client.Model.Base
{    

	/// <summary>
	/// An object representation of the SkyStemArt CompanyUnexplainedVarianceThreshold table
	/// </summary>
	[Serializable]
	public abstract class CompanyUnexplainedVarianceThresholdInfoBase
	{
		protected System.String _AddedBy = "";
		protected System.Int32? _CompanyID = null;
        protected System.Decimal? _CompanyUnexplainedVarianceThreshold = null;
        protected System.Int32? _CompanyUnexplainedVarianceThresholdID = null;
        protected System.DateTime? _DateAdded = null;
        protected System.DateTime? _DateRevised = null;
        protected System.Int32? _EndReconciliationPeriodID = null;
		protected System.String _RevisedBy = "";
        protected System.Int32? _StartReconciliationPeriodID = null;




		public bool IsAddedByNull = true;


		public bool IsCompanyIDNull = true;


		public bool IsCompanyUnexplainedVarianceThresholdNull = true;


		public bool IsCompanyUnexplainedVarianceThresholdIDNull = true;


		public bool IsDateAddedNull = true;


		public bool IsDateRevisedNull = true;


		public bool IsEndReconciliationPeriodIDNull = true;


		public bool IsRevisedByNull = true;


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

		[XmlElement(ElementName = "CompanyUnexplainedVarianceThreshold")]
		public virtual System.Decimal? CompanyUnexplainedVarianceThreshold 
		{
			get 
			{
				return this._CompanyUnexplainedVarianceThreshold;
			}
			set 
			{
				this._CompanyUnexplainedVarianceThreshold = value;

									this.IsCompanyUnexplainedVarianceThresholdNull = false;
							}
		}			

		[XmlElement(ElementName = "CompanyUnexplainedVarianceThresholdID")]
		public virtual System.Int32? CompanyUnexplainedVarianceThresholdID 
		{
			get 
			{
				return this._CompanyUnexplainedVarianceThresholdID;
			}
			set 
			{
				this._CompanyUnexplainedVarianceThresholdID = value;

									this.IsCompanyUnexplainedVarianceThresholdIDNull = false;
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
