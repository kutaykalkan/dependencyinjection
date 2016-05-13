

using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using Adapdev.Text;

namespace SkyStem.ART.Client.Model.Base
{    

	/// <summary>
	/// An object representation of the SkyStemArt RoleReport table
	/// </summary>
	[Serializable]
    public abstract class RoleReportInfoBase : MultilingualInfo
	{

		protected System.Int16? _ReportID = 0;
		protected System.Int16? _RoleID = 0;
		protected System.Int32? _RoleReportID = 0;

		public bool IsReportIDNull = true;


		public bool IsRoleIDNull = true;


		public bool IsRoleReportIDNull = true;

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

		[XmlElement(ElementName = "RoleID")]
		public virtual System.Int16? RoleID 
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

		[XmlElement(ElementName = "RoleReportID")]
		public virtual System.Int32? RoleReportID 
		{
			get 
			{
				return this._RoleReportID;
			}
			set 
			{
				this._RoleReportID = value;

									this.IsRoleReportIDNull = false;
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
