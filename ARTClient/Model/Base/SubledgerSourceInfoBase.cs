

using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using Adapdev.Text;

namespace SkyStem.ART.Client.Model.Base
{    

	/// <summary>
	/// An object representation of the SkyStemArt SubledgerSource table
	/// </summary>
	[Serializable]
	public abstract class SubledgerSourceInfoBase : MultilingualInfo
	{
		protected System.String _AddedBy = "";
		protected System.Int32? _CompanyID = 0;
		protected System.DateTime? _DateAdded = DateTime.Now;
		protected System.DateTime? _DateRevised = DateTime.Now;
		protected System.String _HostName = "";
		protected System.Boolean? _IsActive = false;
		protected System.String _RevisedBy = "";
		protected System.String _SubledgerCode = "";
		protected System.String _SubledgerSource = "";
		protected System.Int32? _SubledgerSourceID = 0;
		protected System.Int32? _SubledgerSourceLabelID = 0;




		public bool IsAddedByNull = true;


		public bool IsCompanyIDNull = true;


		public bool IsDateAddedNull = true;


		public bool IsDateRevisedNull = true;


		public bool IsHostNameNull = true;


		public bool IsIsActiveNull = true;


		public bool IsRevisedByNull = true;


		public bool IsSubledgerCodeNull = true;


		public bool IsSubledgerSourceNull = true;


		public bool IsSubledgerSourceIDNull = true;


		public bool IsSubledgerSourceLabelIDNull = true;

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

		[XmlElement(ElementName = "SubledgerCode")]
		public virtual System.String SubledgerCode 
		{
			get 
			{
				return this._SubledgerCode;
			}
			set 
			{
				this._SubledgerCode = value;

									this.IsSubledgerCodeNull = (_SubledgerCode == null);
							}
		}			

		[XmlElement(ElementName = "SubledgerSource")]
		public virtual System.String SubledgerSource 
		{
			get 
			{
				return this._SubledgerSource;
			}
			set 
			{
				this._SubledgerSource = value;

									this.IsSubledgerSourceNull = (_SubledgerSource == null);
							}
		}			

		[XmlElement(ElementName = "SubledgerSourceID")]
		public virtual System.Int32? SubledgerSourceID 
		{
			get 
			{
				return this._SubledgerSourceID;
			}
			set 
			{
				this._SubledgerSourceID = value;

									this.IsSubledgerSourceIDNull = false;
							}
		}			

		[XmlElement(ElementName = "SubledgerSourceLabelID")]
		public virtual System.Int32? SubledgerSourceLabelID 
		{
			get 
			{
				return this._SubledgerSourceLabelID;
			}
			set 
			{
				this._SubledgerSourceLabelID = value;

									this.IsSubledgerSourceLabelIDNull = false;
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
