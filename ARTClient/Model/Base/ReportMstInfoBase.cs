

using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using Adapdev.Text;

namespace SkyStem.ART.Client.Model.Base
{    

	/// <summary>
	/// An object representation of the SkyStemArt ReportMst table
	/// </summary>
	[Serializable]
	public abstract class ReportMstInfoBase
	{

		protected System.String _AddedBy = "";
		protected System.DateTime? _DateAdded = DateTime.Now;
		protected System.DateTime? _DateRevised = DateTime.Now;
		protected System.String _Description = "";
		protected System.Int32? _DescriptionLabelID = 0;
		protected System.String _HostName = "";
		protected System.Boolean? _IsActive = false;
		protected System.String _Report = "";
        protected System.Int16? _ReportTypeId = 0;
		protected System.Int16? _ReportID = 0;
		protected System.Int32? _ReportLabelID = 0;
		protected System.String _RevisedBy = "";




		public bool IsAddedByNull = true;


		public bool IsDateAddedNull = true;


		public bool IsDateRevisedNull = true;


		public bool IsDescriptionNull = true;


		public bool IsDescriptionLabelIDNull = true;


		public bool IsHostNameNull = true;


		public bool IsIsActiveNull = true;


		public bool IsReportNull = true;


        public bool IsReportTypeIdNull = true;


		public bool IsReportIDNull = true;


		public bool IsReportLabelIDNull = true;


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

		[XmlElement(ElementName = "DescriptionLabelID")]
		public virtual System.Int32? DescriptionLabelID 
		{
			get 
			{
				return this._DescriptionLabelID;
			}
			set 
			{
				this._DescriptionLabelID = value;

									this.IsDescriptionLabelIDNull = false;
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

		[XmlElement(ElementName = "Report")]
		public virtual System.String Report 
		{
			get 
			{
				return this._Report;
			}
			set 
			{
				this._Report = value;

									this.IsReportNull = (_Report == null);
							}
		}

        [XmlElement(ElementName = "ReportTypeId")]
        public virtual System.Int16? ReportTypeId 
		{
			get 
			{
                return this._ReportTypeId;
			}
			set 
			{
                this._ReportTypeId = value;

                this.IsReportTypeIdNull = false;
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

		[XmlElement(ElementName = "ReportLabelID")]
		public virtual System.Int32? ReportLabelID 
		{
			get 
			{
				return this._ReportLabelID;
			}
			set 
			{
				this._ReportLabelID = value;

									this.IsReportLabelIDNull = false;
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
