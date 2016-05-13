

using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using Adapdev.Text;

namespace SkyStem.ART.Client.Model.Base
{    

	/// <summary>
	/// An object representation of the SkyStemArt CertificationStatusMst table
	/// </summary>
	[Serializable]
	public abstract class CertificationStatusMstInfoBase
	{

		protected System.String _AddedBy = "";
		protected System.String _CertificationStatus = "";
		protected System.Int16? _CertificationStatusID = 0;
		protected System.Int32? _CertificationStatusLabelID = 0;
		protected System.DateTime? _DateAdded = DateTime.Now;
		protected System.DateTime? _DateRevised = DateTime.Now;
		protected System.String _Description = "";
		protected System.String _HostName = "";
		protected System.Boolean? _IsActive = false;
		protected System.String _RevisedBy = "";




		public bool IsAddedByNull = true;


		public bool IsCertificationStatusNull = true;


		public bool IsCertificationStatusIDNull = true;


		public bool IsCertificationStatusLabelIDNull = true;


		public bool IsDateAddedNull = true;


		public bool IsDateRevisedNull = true;


		public bool IsDescriptionNull = true;


		public bool IsHostNameNull = true;


		public bool IsIsActiveNull = true;


		public bool IsRevisedByNull = true;

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

		[XmlElement(ElementName = "CertificationStatus")]
		public virtual System.String CertificationStatus 
		{
			get 
			{
				return this._CertificationStatus;
			}
			set 
			{
				this._CertificationStatus = value;

									this.IsCertificationStatusNull = (_CertificationStatus == null);
							}
		}			

		[XmlElement(ElementName = "CertificationStatusID")]
		public virtual System.Int16? CertificationStatusID 
		{
			get 
			{
				return this._CertificationStatusID;
			}
			set 
			{
				this._CertificationStatusID = value;

									this.IsCertificationStatusIDNull = false;
							}
		}			

		[XmlElement(ElementName = "CertificationStatusLabelID")]
		public virtual System.Int32? CertificationStatusLabelID 
		{
			get 
			{
				return this._CertificationStatusLabelID;
			}
			set 
			{
				this._CertificationStatusLabelID = value;

									this.IsCertificationStatusLabelIDNull = false;
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

		[XmlElement(ElementName = "Description")]
		public virtual System.String Description 
		{
			get 
			{
				return this._Description;
			}
			set 
			{
				this._Description = value;

									this.IsDescriptionNull = (_Description == null);
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

        
		/// <summary>
		/// Returns a string representation of the object, displaying all property and field names and values.
		/// </summary>
		public override string ToString() 
		{
			return StringUtil.ToString(this);
		}		
	
	
			    			    		
						
					
					    		
						
					
					    		
						
					
					    		
						
					
					
				
	}
}
