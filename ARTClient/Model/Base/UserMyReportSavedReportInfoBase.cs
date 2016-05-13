

using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using Adapdev.Text;

namespace SkyStem.ART.Client.Model.Base
{    

	/// <summary>
	/// An object representation of the SkyStemArt UserMyReportSavedReport table
	/// </summary>
	[Serializable]
	public abstract class UserMyReportSavedReportInfoBase
	{
		protected System.DateTime _DateAdded = DateTime.Now;
		protected System.DateTime _DateRevised = DateTime.Now;
		protected System.Boolean _IsActive = false;
		protected System.Int32 _UserMyReportID = 0;
		protected System.Int64 _UserMyReportSavedReportID = 0;
		protected System.String _UserMyReportSavedReportName = "";




		public bool IsDateAddedNull = true;


		public bool IsDateRevisedNull = true;


		public bool IsIsActiveNull = true;


		public bool IsUserMyReportIDNull = true;


		public bool IsUserMyReportSavedReportIDNull = true;


		public bool IsUserMyReportSavedReportNameNull = true;

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

		[XmlElement(ElementName = "UserMyReportSavedReportID")]
		public virtual System.Int64 UserMyReportSavedReportID 
		{
			get 
			{
				return this._UserMyReportSavedReportID;
			}
			set 
			{
				this._UserMyReportSavedReportID = value;

									this.IsUserMyReportSavedReportIDNull = false;
							}
		}			

		[XmlElement(ElementName = "UserMyReportSavedReportName")]
		public virtual System.String UserMyReportSavedReportName 
		{
			get 
			{
				return this._UserMyReportSavedReportName;
			}
			set 
			{
				this._UserMyReportSavedReportName = value;

									this.IsUserMyReportSavedReportNameNull = (_UserMyReportSavedReportName == null);
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
