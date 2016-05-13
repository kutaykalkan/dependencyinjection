

using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using Adapdev.Text;

namespace SkyStem.ART.Client.Model.Base
{    

	/// <summary>
	/// An object representation of the SkyStemArt ReconciliationCategoryTypeMst table
	/// </summary>
	[Serializable]
	public abstract class ReconciliationCategoryTypeMstInfoBase
	{
		protected System.String _AddedBy = "";
		protected System.DateTime? _DateAdded = DateTime.Now;
		protected System.DateTime? _DateRevised = DateTime.Now;
		protected System.String _Description = "";
		protected System.String _HostName = "";
		protected System.Boolean? _IsActive = false;
		protected System.Int16? _ReconciliationCategoryID = 0;
		protected System.String _ReconciliationCategoryType = "";
		protected System.Int16? _ReconciliationCategoryTypeID = 0;
		protected System.Int32? _ReconciliationCategoryTypeLabelID = 0;
		protected System.Int16? _ReconciliationTemplateID = 0;
		protected System.String _RevisedBy = "";




		public bool IsAddedByNull = true;


		public bool IsDateAddedNull = true;


		public bool IsDateRevisedNull = true;


		public bool IsDescriptionNull = true;


		public bool IsHostNameNull = true;


		public bool IsIsActiveNull = true;


		public bool IsReconciliationCategoryIDNull = true;


		public bool IsReconciliationCategoryTypeNull = true;


		public bool IsReconciliationCategoryTypeIDNull = true;


		public bool IsReconciliationCategoryTypeLabelIDNull = true;


		public bool IsReconciliationTemplateIDNull = true;


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

		[XmlElement(ElementName = "Description")]
		public virtual System.String Description 
		{
			get 
			{
				return this._Description;
			}
			set 
			{
				this._Description = value;

									this.IsDescriptionNull = (_Description == null);
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

		[XmlElement(ElementName = "ReconciliationCategoryID")]
		public virtual System.Int16? ReconciliationCategoryID 
		{
			get 
			{
				return this._ReconciliationCategoryID;
			}
			set 
			{
				this._ReconciliationCategoryID = value;

									this.IsReconciliationCategoryIDNull = false;
							}
		}			

		[XmlElement(ElementName = "ReconciliationCategoryType")]
		public virtual System.String ReconciliationCategoryType 
		{
			get 
			{
				return this._ReconciliationCategoryType;
			}
			set 
			{
				this._ReconciliationCategoryType = value;

									this.IsReconciliationCategoryTypeNull = (_ReconciliationCategoryType == null);
							}
		}			

		[XmlElement(ElementName = "ReconciliationCategoryTypeID")]
		public virtual System.Int16? ReconciliationCategoryTypeID 
		{
			get 
			{
				return this._ReconciliationCategoryTypeID;
			}
			set 
			{
				this._ReconciliationCategoryTypeID = value;

									this.IsReconciliationCategoryTypeIDNull = false;
							}
		}			

		[XmlElement(ElementName = "ReconciliationCategoryTypeLabelID")]
		public virtual System.Int32? ReconciliationCategoryTypeLabelID 
		{
			get 
			{
				return this._ReconciliationCategoryTypeLabelID;
			}
			set 
			{
				this._ReconciliationCategoryTypeLabelID = value;

									this.IsReconciliationCategoryTypeLabelIDNull = false;
							}
		}			

		[XmlElement(ElementName = "ReconciliationTemplateID")]
		public virtual System.Int16? ReconciliationTemplateID 
		{
			get 
			{
				return this._ReconciliationTemplateID;
			}
			set 
			{
				this._ReconciliationTemplateID = value;

									this.IsReconciliationTemplateIDNull = false;
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
