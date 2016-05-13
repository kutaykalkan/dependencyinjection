

using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using Adapdev.Text;

namespace SkyStem.ART.Client.Model.Base
{    

	/// <summary>
	/// An object representation of the SkyStemArt UserFavoriteReport table
	/// </summary>
	[Serializable]
	public abstract class UserFavoriteReportInfoBase
	{
		protected System.Int16? _ReportID = 0;
		protected System.Int32? _UserFavoriteReportID = 0;
		protected System.Int32? _UserID = 0;

		public bool IsReportIDNull = true;


		public bool IsUserFavoriteReportIDNull = true;


		public bool IsUserIDNull = true;

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

		[XmlElement(ElementName = "UserFavoriteReportID")]
		public virtual System.Int32? UserFavoriteReportID 
		{
			get 
			{
				return this._UserFavoriteReportID;
			}
			set 
			{
				this._UserFavoriteReportID = value;

									this.IsUserFavoriteReportIDNull = false;
							}
		}			

		[XmlElement(ElementName = "UserID")]
		public virtual System.Int32? UserID 
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

        
		/// <summary>
		/// Returns a string representation of the object, displaying all property and field names and values.
		/// </summary>
		public override string ToString() 
		{
			return StringUtil.ToString(this);
		}		
	
	}
}
