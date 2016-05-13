

using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using Adapdev.Text;

namespace SkyStem.ART.Client.Model.Base
{    

	/// <summary>
	/// An object representation of the SkyStemArt HolidayCalendar table
	/// </summary>
	[Serializable]
	public abstract class HolidayCalendarInfoBase
	{

	
		protected System.String _AddedBy = "";
		protected System.DateTime? _DateAdded = DateTime.Now;
		protected System.DateTime? _DateRevised = DateTime.Now;
		protected System.Int32? _GeographyObjectID = 0;
		protected System.Int32? _HolidayCalendarID = 0;
		protected System.DateTime? _HolidayDate = DateTime.Now;
		protected System.String _HolidayName = "";
		protected System.Int32? _HolidayNameLabelID = 0;
		protected System.String _HostName = "";
		protected System.Boolean? _IsActive = false;
		protected System.String _RevisedBy = "";
        protected System.Int32? _CompanyID = 0;



		public bool IsAddedByNull = true;


		public bool IsDateAddedNull = true;


		public bool IsDateRevisedNull = true;


		public bool IsGeographyObjectIDNull = true;


		public bool IsHolidayCalendarIDNull = true;


		public bool IsHolidayDateNull = true;


		public bool IsHolidayNameNull = true;


		public bool IsHolidayNameLabelIDNull = true;


		public bool IsHostNameNull = true;


		public bool IsIsActiveNull = true;


		public bool IsRevisedByNull = true;

        public bool IsCompanyIDNull = true;

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

		[XmlElement(ElementName = "GeographyObjectID")]
		public virtual System.Int32? GeographyObjectID 
		{
			get 
			{
				return this._GeographyObjectID;
			}
			set 
			{
				this._GeographyObjectID = value;

									this.IsGeographyObjectIDNull = false;
							}
		}			

		[XmlElement(ElementName = "HolidayCalendarID")]
		public virtual System.Int32? HolidayCalendarID 
		{
			get 
			{
				return this._HolidayCalendarID;
			}
			set 
			{
				this._HolidayCalendarID = value;

									this.IsHolidayCalendarIDNull = false;
							}
		}			

		[XmlElement(ElementName = "HolidayDate")]
		public virtual System.DateTime? HolidayDate 
		{
			get 
			{
				return this._HolidayDate;
			}
			set 
			{
				this._HolidayDate = value;

									this.IsHolidayDateNull = (_HolidayDate == DateTime.MinValue);
							}
		}			

		[XmlElement(ElementName = "HolidayName")]
		public virtual System.String HolidayName 
		{
			get 
			{
				return this._HolidayName;
			}
			set 
			{
				this._HolidayName = value;

									this.IsHolidayNameNull = (_HolidayName == null);
							}
		}			

		[XmlElement(ElementName = "HolidayNameLabelID")]
		public virtual System.Int32? HolidayNameLabelID 
		{
			get 
			{
				return this._HolidayNameLabelID;
			}
			set 
			{
				this._HolidayNameLabelID = value;

									this.IsHolidayNameLabelIDNull = false;
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

        [XmlElement(ElementName = "HolidayNameLabelID")]
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
        
		/// <summary>
		/// Returns a string representation of the object, displaying all property and field names and values.
		/// </summary>
		public override string ToString() 
		{
			return StringUtil.ToString(this);
		}		
	
	}
}
