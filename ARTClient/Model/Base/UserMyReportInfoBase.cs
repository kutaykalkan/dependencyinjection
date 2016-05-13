

using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using Adapdev.Text;

namespace SkyStem.ART.Client.Model.Base
{    

	/// <summary>
	/// An object representation of the SkyStemArt UserMyReport table
	/// </summary>
	[Serializable]
	public abstract class UserMyReportInfoBase
	{
		protected System.DateTime _DateAdded = DateTime.Now;
		protected System.DateTime _DateRevised = DateTime.Now;
		protected System.Boolean _IsActive = false;
		protected System.Int16 _ReportID = 0;
		protected System.Int16 _RoleID = 0;
		protected System.Int32 _UserID = 0;
		protected System.Int32 _UserMyReportID = 0;




		public bool IsDateAddedNull = true;


		public bool IsDateRevisedNull = true;


		public bool IsIsActiveNull = true;


		public bool IsReportIDNull = true;


		public bool IsRoleIDNull = true;


		public bool IsUserIDNull = true;


		public bool IsUserMyReportIDNull = true;

		[XmlElement(ElementName = "DateAdded")]
		public virtual System.DateTime DateAdded 
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
		public virtual System.DateTime DateRevised 
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

		[XmlElement(ElementName = "IsActive")]
		public virtual System.Boolean IsActive 
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

		[XmlElement(ElementName = "ReportID")]
		public virtual System.Int16 ReportID 
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

		[XmlElement(ElementName = "RoleID")]
		public virtual System.Int16 RoleID 
		{
			get 
			{
				return this._RoleID;
			}
			set 
			{
				this._RoleID = value;

									this.IsRoleIDNull = false;
							}
		}			

		[XmlElement(ElementName = "UserID")]
		public virtual System.Int32 UserID 
		{
			get 
			{
				return this._UserID;
			}
			set 
			{
				this._UserID = value;

									this.IsUserIDNull = false;
							}
		}			

		[XmlElement(ElementName = "UserMyReportID")]
		public virtual System.Int32 UserMyReportID 
		{
			get 
			{
				return this._UserMyReportID;
			}
			set 
			{
				this._UserMyReportID = value;

									this.IsUserMyReportIDNull = false;
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
