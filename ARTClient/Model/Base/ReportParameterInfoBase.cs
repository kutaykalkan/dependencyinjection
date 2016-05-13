

using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using Adapdev.Text;

namespace SkyStem.ART.Client.Model.Base
{    

	/// <summary>
	/// An object representation of the SkyStemArt ReportParameter table
	/// </summary>
	[Serializable]
	public abstract class ReportParameterInfoBase
	{

		protected System.Boolean _IsMandatory = false;
		protected System.Int16 _ParameterID = 0;
		protected System.Int16 _ReportID = 0;
		protected System.Int32 _ReportParameterID = 0;




		public bool IsIsMandatoryNull = true;


		public bool IsParameterIDNull = true;


		public bool IsReportIDNull = true;


		public bool IsReportParameterIDNull = true;

		[XmlElement(ElementName = "IsMandatory")]
		public virtual System.Boolean IsMandatory 
		{
			get 
			{
				return this._IsMandatory;
			}
			set 
			{
				this._IsMandatory = value;

									this.IsIsMandatoryNull = false;
							}
		}			

		[XmlElement(ElementName = "ParameterID")]
		public virtual System.Int16 ParameterID 
		{
			get 
			{
				return this._ParameterID;
			}
			set 
			{
				this._ParameterID = value;

									this.IsParameterIDNull = false;
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

        
		/// <summary>
		/// Returns a string representation of the object, displaying all property and field names and values.
		/// </summary>
		public override string ToString() 
		{
			return StringUtil.ToString(this);
		}		
	
	
	}
}
