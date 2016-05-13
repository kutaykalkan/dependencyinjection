

using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using Adapdev.Text;

namespace SkyStem.ART.Client.Model.Base
{    

	/// <summary>
	/// An object representation of the SkyStemArt CertificationVerbiage table
	/// </summary>
	[Serializable]
	public abstract class CertificationVerbiageInfoBase
	{
		protected System.String _AddedBy = "";
		protected System.Int16? _CertificationTypeID = 0;
		protected System.String _CertificationVerbiage = "";
		protected System.Int32? _CertificationVerbiageID = 0;
		protected System.Int32? _CertificationVerbiageLabelID = 0;
		protected System.Int32? _CompanyID = 0;
		protected System.DateTime? _DateAdded = DateTime.Now;
		protected System.DateTime? _DateRevised = DateTime.Now;
		protected System.String _HostName = "";
		protected System.Boolean? _IsActive = false;
		protected System.String _RevisedBy = "";
		protected System.Int16? _RoleID = 0;

		public bool IsAddedByNull = true;


		public bool IsCertificationTypeIDNull = true;


		public bool IsCertificationVerbiageNull = true;


		public bool IsCertificationVerbiageIDNull = true;


		public bool IsCertificationVerbiageLabelIDNull = true;


		public bool IsCompanyIDNull = true;


		public bool IsDateAddedNull = true;


		public bool IsDateRevisedNull = true;


		public bool IsHostNameNull = true;


		public bool IsIsActiveNull = true;


		public bool IsRevisedByNull = true;


		public bool IsRoleIDNull = true;

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

		[XmlElement(ElementName = "CertificationTypeID")]
		public virtual System.Int16? CertificationTypeID 
		{
			get 
			{
				return this._CertificationTypeID;
			}
			set 
			{
				this._CertificationTypeID = value;

									this.IsCertificationTypeIDNull = false;
							}
		}			

		[XmlElement(ElementName = "CertificationVerbiage")]
		public virtual System.String CertificationVerbiage 
		{
			get 
			{
				return this._CertificationVerbiage;
			}
			set 
			{
				this._CertificationVerbiage = value;

									this.IsCertificationVerbiageNull = (_CertificationVerbiage == null);
							}
		}			

		[XmlElement(ElementName = "CertificationVerbiageID")]
		public virtual System.Int32? CertificationVerbiageID 
		{
			get 
			{
				return this._CertificationVerbiageID;
			}
			set 
			{
				this._CertificationVerbiageID = value;

									this.IsCertificationVerbiageIDNull = false;
							}
		}			

		[XmlElement(ElementName = "CertificationVerbiageLabelID")]
		public virtual System.Int32? CertificationVerbiageLabelID 
		{
			get 
			{
				return this._CertificationVerbiageLabelID;
			}
			set 
			{
				this._CertificationVerbiageLabelID = value;

									this.IsCertificationVerbiageLabelIDNull = false;
							}
		}			

		[XmlElement(ElementName = "CompanyID")]
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

        
		/// <summary>
		/// Returns a string representation of the object, displaying all property and field names and values.
		/// </summary>
		public override string ToString() 
		{
			return StringUtil.ToString(this);
		}		
	
	
			
				
	}
}
