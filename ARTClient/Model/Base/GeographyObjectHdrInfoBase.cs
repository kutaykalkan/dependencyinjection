

using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using Adapdev.Text;

namespace SkyStem.ART.Client.Model.Base
{    

	/// <summary>
	/// An object representation of the SkyStemArt GeographyObjectHdr table
	/// </summary>
	[Serializable]
	public abstract class GeographyObjectHdrInfoBase
	{
		protected System.String _AddedBy = "";
		protected System.Int32? _CompanyID = 0;
		protected System.DateTime? _DateAdded = DateTime.Now;
		protected System.DateTime? _DateRevised = DateTime.Now;
		protected System.String _GeographyObject = "";
		protected System.Int32? _GeographyObjectID = 0;
		protected System.Int32? _GeographyObjectNumber = 0;
		protected System.Int32? _GeographyStructureID = 0;
		protected System.String _HostName = "";
		protected System.Boolean? _IsActive = false;
		protected System.Int32? _ParentGeographyObjectID = 0;
		protected System.String _RevisedBy = "";




		public bool IsAddedByNull = true;


		public bool IsCompanyIDNull = true;


		public bool IsDateAddedNull = true;


		public bool IsDateRevisedNull = true;


		public bool IsGeographyObjectNull = true;


		public bool IsGeographyObjectIDNull = true;


		public bool IsGeographyObjectNumberNull = true;


		public bool IsGeographyStructureIDNull = true;


		public bool IsHostNameNull = true;


		public bool IsIsActiveNull = true;


		public bool IsParentGeographyObjectIDNull = true;


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

		[XmlElement(ElementName = "GeographyObject")]
		public virtual System.String GeographyObject 
		{
			get 
			{
				return this._GeographyObject;
			}
			set 
			{
				this._GeographyObject = value;

									this.IsGeographyObjectNull = (_GeographyObject == null);
							}
		}			

		[XmlElement(ElementName = "GeographyObjectID")]
		public virtual System.Int32? GeographyObjectID 
		{
			get 
			{
				return this._GeographyObjectID;
			}
			set 
			{
				this._GeographyObjectID = value;

									this.IsGeographyObjectIDNull = false;
							}
		}			

		[XmlElement(ElementName = "GeographyObjectNumber")]
		public virtual System.Int32? GeographyObjectNumber 
		{
			get 
			{
				return this._GeographyObjectNumber;
			}
			set 
			{
				this._GeographyObjectNumber = value;

									this.IsGeographyObjectNumberNull = false;
							}
		}			

		[XmlElement(ElementName = "GeographyStructureID")]
		public virtual System.Int32? GeographyStructureID 
		{
			get 
			{
				return this._GeographyStructureID;
			}
			set 
			{
				this._GeographyStructureID = value;

									this.IsGeographyStructureIDNull = false;
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

		[XmlElement(ElementName = "ParentGeographyObjectID")]
		public virtual System.Int32? ParentGeographyObjectID 
		{
			get 
			{
				return this._ParentGeographyObjectID;
			}
			set 
			{
				this._ParentGeographyObjectID = value;

									this.IsParentGeographyObjectIDNull = false;
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
