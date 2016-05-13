

using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using Adapdev.Text;

namespace SkyStem.ART.Client.Model.Base
{    

	/// <summary>
	/// An object representation of the SkyStemART PackageReport table
	/// </summary>
	[Serializable]
	public abstract class PackageReportInfoBase
	{
		protected System.Int16? _PackageID = null;
		protected System.Int16? _PackageReportID = null;
		protected System.Int16? _ReportID = null;

		[XmlElement(ElementName = "PackageID")]
		public virtual System.Int16? PackageID 
		{
			get 
			{
				return this._PackageID;
			}
			set 
			{
				this._PackageID = value;

							}
		}			

		[XmlElement(ElementName = "PackageReportID")]
		public virtual System.Int16? PackageReportID 
		{
			get 
			{
				return this._PackageReportID;
			}
			set 
			{
				this._PackageReportID = value;

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
