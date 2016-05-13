

using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using Adapdev.Text;

namespace SkyStem.ART.Client.Model.Base
{    

	/// <summary>
	/// An object representation of the SkyStemArt AlertMst table
	/// </summary>
	[Serializable]
	public abstract class AlertMstInfoBase
	{
		protected System.String _AddedBy = "";
		protected System.String _Alert = "";
		protected System.Int16? _AlertGenerationLocationID = 0;
		protected System.Int16? _AlertID = 0;
        protected System.Int32? _AlertCategoryID = 0;
		protected System.Int32? _AlertLabelID = 0;
        protected System.Int32? _AlertDisplayLabelID = 0;
        protected System.String _AlertDisplay = ""; 
		protected System.Int16? _AlertResponseTypeID = 0;
		protected System.Int16? _AlertTypeID = 0;
		protected System.String _BaseUrl = "";
		protected System.Int16? _CapabilityID = 0;
		protected System.String _Condition = "";
		protected System.Int32? _ConditionLabelID = 0;
		protected System.DateTime? _DateAdded = DateTime.Now;
		protected System.DateTime? _DateRevised = DateTime.Now;
		protected System.Int16? _DefaultThreshold = 0;
		protected System.Int16? _DefaultThresholdTypeID = 0;
		protected System.String _HostName = "";
		protected System.Boolean? _IsActive = false;
		protected System.Boolean? _IsSystemAdminAlert = false;
		protected System.String _RevisedBy = "";




		public bool IsAddedByNull = true;


		public bool IsAlertNull = true;


		public bool IsAlertGenerationLocationIDNull = true;


		public bool IsAlertIDNull = true;

        public bool IsAlertCategoryIDNull = true;


		public bool IsAlertLabelIDNull = true;


		public bool IsAlertResponseTypeIDNull = true;


		public bool IsAlertTypeIDNull = true;


		public bool IsBaseUrlNull = true;


		public bool IsCapabilityIDNull = true;


		public bool IsConditionNull = true;


		public bool IsConditionLabelIDNull = true;


		public bool IsDateAddedNull = true;


		public bool IsDateRevisedNull = true;


		public bool IsDefaultThresholdNull = true;


		public bool IsDefaultThresholdTypeIDNull = true;


		public bool IsHostNameNull = true;


		public bool IsIsActiveNull = true;


		public bool IsIsSystemAdminAlertNull = true;


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

		[XmlElement(ElementName = "Alert")]
		public virtual System.String Alert 
		{
			get 
			{
				return this._Alert;
			}
			set 
			{
				this._Alert = value;

									this.IsAlertNull = (_Alert == null);
							}
		}

        [XmlElement(ElementName = "AlertDisplay")]
        public virtual System.String AlertDisplay
        {
            get
            {
                return this._AlertDisplay;
            }
            set
            {
                this._AlertDisplay = value;
            }
        }		

		[XmlElement(ElementName = "AlertGenerationLocationID")]
		public virtual System.Int16? AlertGenerationLocationID 
		{
			get 
			{
				return this._AlertGenerationLocationID;
			}
			set 
			{
				this._AlertGenerationLocationID = value;

									this.IsAlertGenerationLocationIDNull = false;
							}
		}			

		[XmlElement(ElementName = "AlertID")]
		public virtual System.Int16? AlertID 
		{
			get 
			{
				return this._AlertID;
			}
			set 
			{
				this._AlertID = value;

									this.IsAlertIDNull = false;
							}
		}

        [XmlElement(ElementName = "AlertCategoryID")]
        public virtual System.Int32? AlertCategoryID
        {
            get
            {
                return this._AlertCategoryID;
            }
            set
            {
                this._AlertCategoryID = value;

                this.IsAlertCategoryIDNull = false;
            }
        }		

		[XmlElement(ElementName = "AlertLabelID")]
		public virtual System.Int32? AlertLabelID 
		{
			get 
			{
				return this._AlertLabelID;
			}
			set 
			{
				this._AlertLabelID = value;

									this.IsAlertLabelIDNull = false;
							}
		}

        [XmlElement(ElementName = "AlertDisplayLabelID")]
        public virtual System.Int32? AlertDisplayLabelID
        {
            get
            {
                return this._AlertDisplayLabelID;
            }
            set
            {
                this._AlertDisplayLabelID = value;
            }
        }	

		[XmlElement(ElementName = "AlertResponseTypeID")]
		public virtual System.Int16? AlertResponseTypeID 
		{
			get 
			{
				return this._AlertResponseTypeID;
			}
			set 
			{
				this._AlertResponseTypeID = value;

									this.IsAlertResponseTypeIDNull = false;
							}
		}			

		[XmlElement(ElementName = "AlertTypeID")]
		public virtual System.Int16? AlertTypeID 
		{
			get 
			{
				return this._AlertTypeID;
			}
			set 
			{
				this._AlertTypeID = value;

									this.IsAlertTypeIDNull = false;
							}
		}			

		[XmlElement(ElementName = "BaseUrl")]
		public virtual System.String BaseUrl 
		{
			get 
			{
				return this._BaseUrl;
			}
			set 
			{
				this._BaseUrl = value;

									this.IsBaseUrlNull = (_BaseUrl == null);
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

		[XmlElement(ElementName = "Condition")]
		public virtual System.String Condition 
		{
			get 
			{
				return this._Condition;
			}
			set 
			{
				this._Condition = value;

									this.IsConditionNull = (_Condition == null);
							}
		}			

		[XmlElement(ElementName = "ConditionLabelID")]
		public virtual System.Int32? ConditionLabelID 
		{
			get 
			{
				return this._ConditionLabelID;
			}
			set 
			{
				this._ConditionLabelID = value;

									this.IsConditionLabelIDNull = false;
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

		[XmlElement(ElementName = "DefaultThreshold")]
		public virtual System.Int16? DefaultThreshold 
		{
			get 
			{
				return this._DefaultThreshold;
			}
			set 
			{
				this._DefaultThreshold = value;

									this.IsDefaultThresholdNull = false;
							}
		}			

		[XmlElement(ElementName = "DefaultThresholdTypeID")]
		public virtual System.Int16? DefaultThresholdTypeID 
		{
			get 
			{
				return this._DefaultThresholdTypeID;
			}
			set 
			{
				this._DefaultThresholdTypeID = value;

									this.IsDefaultThresholdTypeIDNull = false;
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

		[XmlElement(ElementName = "IsSystemAdminAlert")]
		public virtual System.Boolean? IsSystemAdminAlert 
		{
			get 
			{
				return this._IsSystemAdminAlert;
			}
			set 
			{
				this._IsSystemAdminAlert = value;

									this.IsIsSystemAdminAlertNull = false;
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
