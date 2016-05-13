

using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using Adapdev.Text;

namespace SkyStem.ART.Client.Model.Matching.Base
{    

	/// <summary>
	/// An object representation of the SkyStemART MatchingSourceDataImportHdr table
	/// </summary>
	[Serializable]
	public abstract class MatchingSourceDataImportHdrInfoBase
	{

				    protected System.String _AddedBy = null;
						protected System.Int16? _DataImportStatusID = null;
						protected System.DateTime? _DateAdded = null;
						protected System.DateTime? _DateRevised = null;
						protected System.String _FileName = null;
						protected System.Decimal? _FileSize = null;
						protected System.DateTime? _ForceCommitDate = null;
						protected System.String _HostName = null;
						protected System.Boolean? _IsForceCommit = null;
						protected System.Int32? _LanguageID = null;
						protected System.Int64? _MatchingSourceDataImportID = null;
						protected System.String _MatchingSourceName = null;
						protected System.Int16? _MatchingSourceTypeID = null;
						protected System.String _Message = null;
						protected System.String _PhysicalPath = null;
						protected System.Int32? _RecordsImported = null;
						protected System.Int32? _RecPeriodID = null;
						protected System.String _RevisedBy = null;
						protected System.Int16? _RoleID = null;
						protected System.Int32? _UserID = null;
                        protected System.Boolean? _IsActive = null;
		


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

			}
		}			

		[XmlElement(ElementName = "DataImportStatusID")]
			public virtual System.Int16? DataImportStatusID 
			{
			get 
			{
				return this._DataImportStatusID;
			}
			set 
			{
				this._DataImportStatusID = value;

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

			}
		}			

		[XmlElement(ElementName = "FileName")]
				public virtual System.String FileName 
				{
			get 
			{
				return this._FileName;
			}
			set 
			{
				this._FileName = value;

			}
		}			

		[XmlElement(ElementName = "FileSize")]
				public virtual System.Decimal? FileSize 
				{
			get 
			{
				return this._FileSize;
			}
			set 
			{
				this._FileSize = value;

			}
		}			

		[XmlElement(ElementName = "ForceCommitDate")]
				public virtual System.DateTime? ForceCommitDate 
				{
			get 
			{
				return this._ForceCommitDate;
			}
			set 
			{
				this._ForceCommitDate = value;

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

			}
		}			

		[XmlElement(ElementName = "IsForceCommit")]
				public virtual System.Boolean? IsForceCommit 
				{
			get 
			{
				return this._IsForceCommit;
			}
			set 
			{
				this._IsForceCommit = value;

			}
		}			

		[XmlElement(ElementName = "LanguageID")]
				public virtual System.Int32? LanguageID 
				{
			get 
			{
				return this._LanguageID;
			}
			set 
			{
				this._LanguageID = value;

			}
		}			

		[XmlElement(ElementName = "MatchingSourceDataImportID")]
				public virtual System.Int64? MatchingSourceDataImportID 
				{
			get 
			{
				return this._MatchingSourceDataImportID;
			}
			set 
			{
				this._MatchingSourceDataImportID = value;

			}
		}			

		[XmlElement(ElementName = "MatchingSourceName")]
				public virtual System.String MatchingSourceName 
				{
			get 
			{
				return this._MatchingSourceName;
			}
			set 
			{
				this._MatchingSourceName = value;

			}
		}			

		[XmlElement(ElementName = "MatchingSourceTypeID")]
				public virtual System.Int16? MatchingSourceTypeID 
				{
			get 
			{
				return this._MatchingSourceTypeID;
			}
			set 
			{
				this._MatchingSourceTypeID = value;

			}
		}			

		[XmlElement(ElementName = "Message")]
				public virtual System.String Message 
				{
			get 
			{
				return this._Message;
			}
			set 
			{
				this._Message = value;

			}
		}			

		[XmlElement(ElementName = "PhysicalPath")]
				public virtual System.String PhysicalPath 
				{
			get 
			{
				return this._PhysicalPath;
			}
			set 
			{
				this._PhysicalPath = value;

			}
		}			

		[XmlElement(ElementName = "RecordsImported")]
				public virtual System.Int32? RecordsImported 
				{
			get 
			{
				return this._RecordsImported;
			}
			set 
			{
				this._RecordsImported = value;

			}
		}			

		[XmlElement(ElementName = "RecPeriodID")]
				public virtual System.Int32? RecPeriodID 
				{
			get 
			{
				return this._RecPeriodID;
			}
			set 
			{
				this._RecPeriodID = value;

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
