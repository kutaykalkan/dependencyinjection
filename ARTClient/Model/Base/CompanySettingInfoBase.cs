

using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using Adapdev.Text;

namespace SkyStem.ART.Client.Model.Base
{    

	/// <summary>
	/// An object representation of the SkyStemArt CompanySetting table
	/// </summary>
	[Serializable]
	public abstract class CompanySettingInfoBase
	{
		protected System.String _AddedBy = "";
		protected System.Boolean? _AllowCertificationLockdown = false;
		protected System.Boolean? _AllowCustomReconciliationFrequency = false;
		protected System.Boolean? _AllowReviewNotesDeletion = false;
		protected System.String _BaseCurrencyCode = "";
		protected System.Int32? _CompanyID = 0;
		protected System.Decimal? _CompanyMaterialityThreshold = null;
		protected System.Int32? _CompanySettingID = 0;
		protected System.DateTime? _DateAdded = DateTime.Now;
		protected System.DateTime? _DateRevised = DateTime.Now;
		protected System.String _HostName = "";
		protected System.Int16? _MaterialityTypeID = 0;
		protected System.Decimal? _MaximumDocumentUploadSize = null;
		protected System.Int32? _CurrentReconciliationPeriodID = 0;
		protected System.String _ReportingCurrencyCode = "";
		protected System.String _RevisedBy = "";
		protected System.Decimal? _UnexplainedVarianceThreshold = null;




		public bool IsAddedByNull = true;


		public bool IsAllowCertificationLockdownNull = true;


		public bool IsAllowCustomReconciliationFrequencyNull = true;


		public bool IsAllowReviewNotesDeletionNull = true;


		public bool IsBaseCurrencyCodeNull = true;


		public bool IsCompanyIDNull = true;


		public bool IsCompanyMaterialityThresholdNull = true;


		public bool IsCompanySettingIDNull = true;


		public bool IsDateAddedNull = true;


		public bool IsDateRevisedNull = true;


		public bool IsHostNameNull = true;


		public bool IsMaterialityTypeIDNull = true;


		public bool IsMaximumDocumentUploadSizeNull = true;


		public bool IsCurrentReconciliationPeriodIDNull = true;


		public bool IsReportingCurrencyCodeNull = true;


		public bool IsRevisedByNull = true;


		public bool IsUnexplainedVarianceThresholdNull = true;

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

		[XmlElement(ElementName = "AllowCertificationLockdown")]
		public virtual System.Boolean? AllowCertificationLockdown 
		{
			get 
			{
				return this._AllowCertificationLockdown;
			}
			set 
			{
				this._AllowCertificationLockdown = value;

									this.IsAllowCertificationLockdownNull = false;
							}
		}			

		[XmlElement(ElementName = "AllowCustomReconciliationFrequency")]
		public virtual System.Boolean? AllowCustomReconciliationFrequency 
		{
			get 
			{
				return this._AllowCustomReconciliationFrequency;
			}
			set 
			{
				this._AllowCustomReconciliationFrequency = value;

									this.IsAllowCustomReconciliationFrequencyNull = false;
							}
		}			

		[XmlElement(ElementName = "AllowReviewNotesDeletion")]
		public virtual System.Boolean? AllowReviewNotesDeletion 
		{
			get 
			{
				return this._AllowReviewNotesDeletion;
			}
			set 
			{
				this._AllowReviewNotesDeletion = value;

									this.IsAllowReviewNotesDeletionNull = false;
							}
		}			

		[XmlElement(ElementName = "BaseCurrencyCode")]
		public virtual System.String BaseCurrencyCode 
		{
			get 
			{
				return this._BaseCurrencyCode;
			}
			set 
			{
				this._BaseCurrencyCode = value;

									this.IsBaseCurrencyCodeNull = (_BaseCurrencyCode == null);
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

		[XmlElement(ElementName = "CompanyMaterialityThreshold")]
		public virtual System.Decimal? CompanyMaterialityThreshold 
		{
			get 
			{
				return this._CompanyMaterialityThreshold;
			}
			set 
			{
				this._CompanyMaterialityThreshold = value;

									this.IsCompanyMaterialityThresholdNull = false;
							}
		}			

		[XmlElement(ElementName = "CompanySettingID")]
		public virtual System.Int32? CompanySettingID 
		{
			get 
			{
				return this._CompanySettingID;
			}
			set 
			{
				this._CompanySettingID = value;

									this.IsCompanySettingIDNull = false;
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

		[XmlElement(ElementName = "HostName")]
		public virtual System.String HostName 
		{
			get 
			{
				return this._HostName;
			}
			set 
			{
				this._HostName = value;

									this.IsHostNameNull = (_HostName == null);
							}
		}			

		[XmlElement(ElementName = "MaterialityTypeID")]
		public virtual System.Int16? MaterialityTypeID 
		{
			get 
			{
				return this._MaterialityTypeID;
			}
			set 
			{
				this._MaterialityTypeID = value;

									this.IsMaterialityTypeIDNull = false;
							}
		}			

		[XmlElement(ElementName = "MaximumDocumentUploadSize")]
		public virtual System.Decimal? MaximumDocumentUploadSize 
		{
			get 
			{
				return this._MaximumDocumentUploadSize;
			}
			set 
			{
				this._MaximumDocumentUploadSize = value;

									this.IsMaximumDocumentUploadSizeNull = false;
							}
		}			

		[XmlElement(ElementName = "CurrentReconciliationPeriodID")]
		public virtual System.Int32? CurrentReconciliationPeriodID 
		{
			get 
			{
				return this._CurrentReconciliationPeriodID;
			}
			set 
			{
				this._CurrentReconciliationPeriodID = value;

									this.IsCurrentReconciliationPeriodIDNull = false;
							}
		}			

		[XmlElement(ElementName = "ReportingCurrencyCode")]
		public virtual System.String ReportingCurrencyCode 
		{
			get 
			{
				return this._ReportingCurrencyCode;
			}
			set 
			{
				this._ReportingCurrencyCode = value;

									this.IsReportingCurrencyCodeNull = (_ReportingCurrencyCode == null);
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

		[XmlElement(ElementName = "UnexplainedVarianceThreshold")]
		public virtual System.Decimal? UnexplainedVarianceThreshold 
		{
			get 
			{
				return this._UnexplainedVarianceThreshold;
			}
			set 
			{
				this._UnexplainedVarianceThreshold = value;

									this.IsUnexplainedVarianceThresholdNull = false;
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
