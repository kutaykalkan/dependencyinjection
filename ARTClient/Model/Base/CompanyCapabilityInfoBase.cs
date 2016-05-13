

using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using Adapdev.Text;

namespace SkyStem.ART.Client.Model.Base
{    

	/// <summary>
	/// An object representation of the SkyStemArt CompanyCapability table
	/// </summary>
	[Serializable]
	public abstract class CompanyCapabilityInfoBase
	{
		protected System.String _AddedBy = "";
		protected System.Int16? _CapabilityID = 0;
		protected System.Int32? _CompanyCapabilityID = 0;
		protected System.Int32? _CompanyID = 0;
		protected System.DateTime? _DateAdded = DateTime.Now;
		protected System.DateTime? _DateRevised = DateTime.Now;
		protected System.Int32? _EndReconciliationPeriodID = 0;
		protected System.Boolean? _IsActivated = false;
		protected System.Boolean _IsConfigurationComplete = false;
		protected System.String _RevisedBy = "";
		protected System.Int32? _StartReconciliationPeriodID = 0;




		public bool IsAddedByNull = true;


		public bool IsCapabilityIDNull = true;


		public bool IsCompanyCapabilityIDNull = true;


		public bool IsCompanyIDNull = true;


		public bool IsDateAddedNull = true;


		public bool IsDateRevisedNull = true;


		public bool IsEndReconciliationPeriodIDNull = true;


		public bool IsIsActivatedNull = true;


		public bool IsIsConfigurationCompleteNull = true;


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

		[XmlElement(ElementName = "CapabilityID")]
		public virtual System.Int16? CapabilityID 
		{
			get 
			{
				return this._CapabilityID;
			}
			set 
			{
				this._CapabilityID = value;

									this.IsCapabilityIDNull = false;
							}
		}			

		[XmlElement(ElementName = "CompanyCapabilityID")]
		public virtual System.Int32? CompanyCapabilityID 
		{
			get 
			{
				return this._CompanyCapabilityID;
			}
			set 
			{
				this._CompanyCapabilityID = value;

									this.IsCompanyCapabilityIDNull = false;
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

		[XmlElement(ElementName = "IsActivated")]
		public virtual System.Boolean? IsActivated 
		{
			get 
			{
				return this._IsActivated;
			}
			set 
			{
				this._IsActivated = value;

									this.IsIsActivatedNull = false;
							}
		}			

		[XmlElement(ElementName = "IsConfigurationComplete")]
		public virtual System.Boolean IsConfigurationComplete 
		{
			get 
			{
				return this._IsConfigurationComplete;
			}
			set 
			{
				this._IsConfigurationComplete = value;

									this.IsIsConfigurationCompleteNull = false;
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
