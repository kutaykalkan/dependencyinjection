

using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using Adapdev.Text;

namespace SkyStem.ART.Client.Model.Base
{    

	/// <summary>
	/// An object representation of the SkyStemArt FSCaptionMateriality table
	/// </summary>
	[Serializable]
	public abstract class FSCaptionMaterialityInfoBase
	{

	
		protected System.String _AddedBy = "";
		protected System.DateTime? _DateAdded = DateTime.Now;
		protected System.DateTime? _DateRevised = DateTime.Now;
		protected System.Int32? _EndReconciliationPeriodID = 0;
		protected System.Int32? _FSCaptionID = 0;
		protected System.Int32? _FSCaptionMaterialityID = 0;
		protected System.Decimal? _MaterialityThreshold = 0.00M;
		protected System.String _RevisedBy = "";
		protected System.Int32? _StartReconciliationPeriodID = 0;




		public bool IsAddedByNull = true;


		public bool IsDateAddedNull = true;


		public bool IsDateRevisedNull = true;


		public bool IsEndReconciliationPeriodIDNull = true;


		public bool IsFSCaptionIDNull = true;


		public bool IsFSCaptionMaterialityIDNull = true;


		public bool IsMaterialityThresholdNull = true;


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

		[XmlElement(ElementName = "FSCaptionID")]
		public virtual System.Int32? FSCaptionID 
		{
			get 
			{
				return this._FSCaptionID;
			}
			set 
			{
				this._FSCaptionID = value;

									this.IsFSCaptionIDNull = false;
							}
		}			

		[XmlElement(ElementName = "FSCaptionMaterialityID")]
		public virtual System.Int32? FSCaptionMaterialityID 
		{
			get 
			{
				return this._FSCaptionMaterialityID;
			}
			set 
			{
				this._FSCaptionMaterialityID = value;

									this.IsFSCaptionMaterialityIDNull = false;
							}
		}			

		[XmlElement(ElementName = "MaterialityThreshold")]
		public virtual System.Decimal? MaterialityThreshold 
		{
			get 
			{
				return this._MaterialityThreshold;
			}
			set 
			{
				this._MaterialityThreshold = value;

									this.IsMaterialityThresholdNull = false;
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
