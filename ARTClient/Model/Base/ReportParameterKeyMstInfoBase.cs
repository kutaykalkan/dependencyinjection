

using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using Adapdev.Text;

namespace SkyStem.ART.Client.Model.Base
{    

	/// <summary>
	/// An object representation of the SkyStemART ReportParameterKeyMst table
	/// </summary>
	[Serializable]
	public abstract class ReportParameterKeyMstInfoBase
	{
		protected System.String _AddedBy = null;
		protected System.DateTime? _DateAdded = null;
		protected System.DateTime? _DateRevised = null;
		protected System.String _HostName = null;
		protected System.Boolean? _IsActive = false;
		protected System.Int16? _ReportParameterKeyID = null;
		protected System.String _ReportParameterKeyName = null;
		protected System.Int16? _ReportParameterKeyParentID = null;
		protected System.String _RevisedBy = null;




		public bool IsAddedByNull = true;


		public bool IsDateAddedNull = true;


		public bool IsDateRevisedNull = true;


		public bool IsHostNameNull = true;


		public bool IsIsActiveNull = true;


		public bool IsReportParameterKeyIDNull = true;


		public bool IsReportParameterKeyNameNull = true;


		public bool IsReportParameterKeyParentIDNull = true;


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

		[XmlElement(ElementName = "ReportParameterKeyID")]
		public virtual System.Int16? ReportParameterKeyID 
		{
			get 
			{
				return this._ReportParameterKeyID;
			}
			set 
			{
				this._ReportParameterKeyID = value;

									this.IsReportParameterKeyIDNull = false;
							}
		}			

		[XmlElement(ElementName = "ReportParameterKeyName")]
		public virtual System.String ReportParameterKeyName 
		{
			get 
			{
				return this._ReportParameterKeyName;
			}
			set 
			{
				this._ReportParameterKeyName = value;

									this.IsReportParameterKeyNameNull = (_ReportParameterKeyName == null);
							}
		}			

		[XmlElement(ElementName = "ReportParameterKeyParentID")]
		public virtual System.Int16? ReportParameterKeyParentID 
		{
			get 
			{
				return this._ReportParameterKeyParentID;
			}
			set 
			{
				this._ReportParameterKeyParentID = value;

									this.IsReportParameterKeyParentIDNull = false;
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
