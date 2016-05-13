
using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using Adapdev.Text;

namespace SkyStem.ART.Client.Model.Base
{    

	/// <summary>
	/// An object representation of the SkyStemArt ReconciliationFrequencyHdr table
	/// </summary>
	[Serializable]
	public abstract class ReconciliationFrequencyHdrInfoBase
	{

		protected System.String _AddedBy = "";
        protected System.Int32? _CompanyID = null;
		protected System.DateTime? _DateAdded = null;
        protected System.DateTime? _DateRevised = null;
		protected System.String _HostName = "";
        protected System.Boolean? _IsActive = null;
        protected System.Int32? _ReconciliationFrequencyID = null;
		protected System.String _ReconciliationFrequencyName = "";
        protected System.Int32? _ReconciliationFrequencyNameLabelID = null;
		protected System.String _RevisedBy = "";




		public bool IsAddedByNull = true;


		public bool IsCompanyIDNull = true;


		public bool IsDateAddedNull = true;


		public bool IsDateRevisedNull = true;


		public bool IsHostNameNull = true;


		public bool IsIsActiveNull = true;


		public bool IsReconciliationFrequencyIDNull = true;


		public bool IsReconciliationFrequencyNameNull = true;


		public bool IsReconciliationFrequencyNameLabelIDNull = true;


		public bool IsRevisedByNull = true;

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

		[XmlElement(ElementName = "IsActive")]
		public virtual System.Boolean? IsActive 
		{
			get 
			{
				return this._IsActive;
			}
			set 
			{
				this._IsActive = value;

									this.IsIsActiveNull = false;
							}
		}			

		[XmlElement(ElementName = "ReconciliationFrequencyID")]
		public virtual System.Int32? ReconciliationFrequencyID 
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

		[XmlElement(ElementName = "ReconciliationFrequencyName")]
		public virtual System.String ReconciliationFrequencyName 
		{
			get 
			{
				return this._ReconciliationFrequencyName;
			}
			set 
			{
				this._ReconciliationFrequencyName = value;

									this.IsReconciliationFrequencyNameNull = (_ReconciliationFrequencyName == null);
							}
		}			

		[XmlElement(ElementName = "ReconciliationFrequencyNameLabelID")]
		public virtual System.Int32? ReconciliationFrequencyNameLabelID 
		{
			get 
			{
				return this._ReconciliationFrequencyNameLabelID;
			}
			set 
			{
				this._ReconciliationFrequencyNameLabelID = value;

									this.IsReconciliationFrequencyNameLabelIDNull = false;
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

        
		/// <summary>
		/// Returns a string representation of the object, displaying all property and field names and values.
		/// </summary>
		public override string ToString() 
		{
			return StringUtil.ToString(this);
		}		
	
	}
}
