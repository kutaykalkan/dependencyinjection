

using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using Adapdev.Text;

namespace SkyStem.ART.Client.Model.Base
{    

	/// <summary>
	/// An object representation of the SkyStemArt CertificationSignOff table
	/// </summary>
	[Serializable]
	public abstract class CertificationSignOffInfoBase
	{

	
		protected System.String _AccountCertificationComments = "";
		protected System.DateTime? _AccountCertificationDate = null ;
		protected System.String _CertificationBalancesComments = "";
		protected System.DateTime? _CertificationBalancesDate = null;
        protected System.Int32? _CertificationSignOffID = null;
        protected System.Int32? _CompanyID = null;
		protected System.String _ExceptionCertificationComments = "";
		protected System.DateTime? _ExceptionCertificationDate = null;
		protected System.DateTime? _MadatoryReportSignOffDate = null;
        protected System.Int32? _ReconciliationPeriodID = null;
        protected System.Int16? _RoleID = null;
        protected System.Int32? _UserID = null;




		public bool IsAccountCertificationCommentsNull = true;


		public bool IsAccountCertificationDateNull = true;


		public bool IsCertificationBalancesCommentsNull = true;


		public bool IsCertificationBalancesDateNull = true;


		public bool IsCertificationSignOffIDNull = true;


		public bool IsCompanyIDNull = true;


		public bool IsExceptionCertificationCommentsNull = true;


		public bool IsExceptionCertificationDateNull = true;


		public bool IsMadatoryReportSignOffDateNull = true;


		public bool IsReconciliationPeriodIDNull = true;


		public bool IsRoleIDNull = true;


		public bool IsUserIDNull = true;

		[XmlElement(ElementName = "AccountCertificationComments")]
		public virtual System.String AccountCertificationComments 
		{
			get 
			{
				return this._AccountCertificationComments;
			}
			set 
			{
				this._AccountCertificationComments = value;

									this.IsAccountCertificationCommentsNull = (_AccountCertificationComments == null);
							}
		}			

		[XmlElement(ElementName = "AccountCertificationDate")]
		public virtual System.DateTime? AccountCertificationDate 
		{
			get 
			{
				return this._AccountCertificationDate;
			}
			set 
			{
				this._AccountCertificationDate = value;

									this.IsAccountCertificationDateNull = (_AccountCertificationDate == DateTime.MinValue);
							}
		}			

		[XmlElement(ElementName = "CertificationBalancesComments")]
		public virtual System.String CertificationBalancesComments 
		{
			get 
			{
				return this._CertificationBalancesComments;
			}
			set 
			{
				this._CertificationBalancesComments = value;

									this.IsCertificationBalancesCommentsNull = (_CertificationBalancesComments == null);
							}
		}			

		[XmlElement(ElementName = "CertificationBalancesDate")]
		public virtual System.DateTime? CertificationBalancesDate 
		{
			get 
			{
				return this._CertificationBalancesDate;
			}
			set 
			{
				this._CertificationBalancesDate = value;

									this.IsCertificationBalancesDateNull = (_CertificationBalancesDate == DateTime.MinValue);
							}
		}			

		[XmlElement(ElementName = "CertificationSignOffID")]
		public virtual System.Int32? CertificationSignOffID 
		{
			get 
			{
				return this._CertificationSignOffID;
			}
			set 
			{
				this._CertificationSignOffID = value;

									this.IsCertificationSignOffIDNull = false;
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

		[XmlElement(ElementName = "ExceptionCertificationComments")]
		public virtual System.String ExceptionCertificationComments 
		{
			get 
			{
				return this._ExceptionCertificationComments;
			}
			set 
			{
				this._ExceptionCertificationComments = value;

									this.IsExceptionCertificationCommentsNull = (_ExceptionCertificationComments == null);
							}
		}			

		[XmlElement(ElementName = "ExceptionCertificationDate")]
		public virtual System.DateTime? ExceptionCertificationDate 
		{
			get 
			{
				return this._ExceptionCertificationDate;
			}
			set 
			{
				this._ExceptionCertificationDate = value;

									this.IsExceptionCertificationDateNull = (_ExceptionCertificationDate == DateTime.MinValue);
							}
		}			

		[XmlElement(ElementName = "MadatoryReportSignOffDate")]
		public virtual System.DateTime? MadatoryReportSignOffDate 
		{
			get 
			{
				return this._MadatoryReportSignOffDate;
			}
			set 
			{
				this._MadatoryReportSignOffDate = value;

									this.IsMadatoryReportSignOffDateNull = (_MadatoryReportSignOffDate == DateTime.MinValue);
							}
		}			

		[XmlElement(ElementName = "ReconciliationPeriodID")]
		public virtual System.Int32? ReconciliationPeriodID 
		{
			get 
			{
				return this._ReconciliationPeriodID;
			}
			set 
			{
				this._ReconciliationPeriodID = value;

									this.IsReconciliationPeriodIDNull = false;
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
