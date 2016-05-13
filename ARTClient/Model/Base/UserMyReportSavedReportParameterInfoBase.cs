

using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using Adapdev.Text;

namespace SkyStem.ART.Client.Model.Base
{    

	/// <summary>
	/// An object representation of the SkyStemArt UserMyReportSavedReportParameter table
	/// </summary>
	[Serializable]
	public abstract class UserMyReportSavedReportParameterInfoBase
	{

		protected System.String _ParameterValue = "";
		protected System.Int32 _ReportParameterID = 0;
		protected System.Int64 _UserMyReportSavedReportID = 0;
		protected System.Int64 _UserMyReportSavedReportParameterID = 0;

		public bool IsParameterValueNull = true;


		public bool IsReportParameterIDNull = true;


		public bool IsUserMyReportSavedReportIDNull = true;


		public bool IsUserMyReportSavedReportParameterIDNull = true;

		[XmlElement(ElementName = "ParameterValue")]
		public virtual System.String ParameterValue 
		{
			get 
			{
				return this._ParameterValue;
			}
			set 
			{
				this._ParameterValue = value;

									this.IsParameterValueNull = (_ParameterValue == null);
							}
		}			

		[XmlElement(ElementName = "ReportParameterID")]
		public virtual System.Int32 ReportParameterID 
		{
			get 
			{
				return this._ReportParameterID;
			}
			set 
			{
				this._ReportParameterID = value;

									this.IsReportParameterIDNull = false;
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

		[XmlElement(ElementName = "UserMyReportSavedReportParameterID")]
		public virtual System.Int64 UserMyReportSavedReportParameterID 
		{
			get 
			{
				return this._UserMyReportSavedReportParameterID;
			}
			set 
			{
				this._UserMyReportSavedReportParameterID = value;

									this.IsUserMyReportSavedReportParameterIDNull = false;
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
