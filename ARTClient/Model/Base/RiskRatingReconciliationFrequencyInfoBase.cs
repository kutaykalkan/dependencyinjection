

using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using Adapdev.Text;

namespace SkyStem.ART.Client.Model.Base
{    

	/// <summary>
	/// An object representation of the SkyStemArt RiskRatingReconciliationFrequency table
	/// </summary>
	[Serializable]
	public abstract class RiskRatingReconciliationFrequencyInfoBase
	{

	
		protected System.String _AddedBy = "";
		protected System.Int32? _CompanyID = 0;
		protected System.DateTime? _DateAdded = DateTime.Now;
		protected System.DateTime? _DateRevised = DateTime.Now;
		protected System.Int32? _EndReconciliationPeriodID = 0;
		protected System.Int16? _ReconciliationFrequencyID = 0;
		protected System.String _RevisedBy = "";
		protected System.Int16? _RiskRatingID = 0;
		protected System.Int32? _RiskRatingReconciliationFrequencyID = 0;
		protected System.Int32? _StartReconciliationPeriodID = 0;




		public bool IsAddedByNull = true;


		public bool IsCompanyIDNull = true;


		public bool IsDateAddedNull = true;


		public bool IsDateRevisedNull = true;


		public bool IsEndReconciliationPeriodIDNull = true;


		public bool IsReconciliationFrequencyIDNull = true;


		public bool IsRevisedByNull = true;


		public bool IsRiskRatingIDNull = true;


		public bool IsRiskRatingReconciliationFrequencyIDNull = true;


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

		[XmlElement(ElementName = "ReconciliationFrequencyID")]
		public virtual System.Int16? ReconciliationFrequencyID 
		{
			get 
			{
				return this._ReconciliationFrequencyID;
			}
			set 
			{
				this._ReconciliationFrequencyID = value;

									this.IsReconciliationFrequencyIDNull = false;
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

		[XmlElement(ElementName = "RiskRatingID")]
		public virtual System.Int16? RiskRatingID 
		{
			get 
			{
				return this._RiskRatingID;
			}
			set 
			{
				this._RiskRatingID = value;

									this.IsRiskRatingIDNull = false;
							}
		}			

		[XmlElement(ElementName = "RiskRatingReconciliationFrequencyID")]
		public virtual System.Int32? RiskRatingReconciliationFrequencyID 
		{
			get 
			{
				return this._RiskRatingReconciliationFrequencyID;
			}
			set 
			{
				this._RiskRatingReconciliationFrequencyID = value;

									this.IsRiskRatingReconciliationFrequencyIDNull = false;
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
