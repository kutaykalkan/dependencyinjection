

using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using Adapdev.Text;

namespace SkyStem.ART.Client.Model.Base
{    

	/// <summary>
	/// An object representation of the SkyStemArt GLReconciliationItemInput table
	/// </summary>
	[Serializable]
	public abstract class GLDataRecItemInfoBase
	{
		protected System.String _AddedBy = "";
		protected System.Decimal? _Amount = 0.00M;
		protected System.Decimal? _AmountBaseCurrency = 0.00M;
		protected System.Decimal? _AmountReportingCurrency = 0.00M;
		protected System.String _Comments = "";
		protected System.Int32? _DataImportID = 0;
		protected System.DateTime? _DateAdded = DateTime.Now;
		protected System.Int64? _GLDataID = 0;
		protected System.Int64? _GLDataRecItemID = 0;
		protected System.Boolean? _IsAttachmentAvailable = false;
		protected System.String _JournalEntryRef = "";
		protected System.String _LocalCurrencyCode = "";
		protected System.DateTime? _OpenDate = DateTime.Now;
		protected System.Int16? _ReconciliationCategoryID = 0;
		protected System.Int16? _ReconciliationCategoryTypeID = 0;
		protected System.String _CloseComments = "";
		protected System.DateTime? _CloseDate = DateTime.Now;
        protected System.Int64? _OriginalGLDataRecItemID = 0;
        protected System.Int64? _PreviousGLDataRecItemID = 0;
        protected System.Boolean? _IsActive = false;
        protected System.DateTime? _DateRevised = DateTime.Now;
        protected System.String _RevisedBy = "";
        protected System.Int32? _AddedByUserID = 0;




		public bool IsAddedByNull = true;


		public bool IsAmountNull = true;


		public bool IsAmountBaseCurrencyNull = true;


		public bool IsAmountReportingCurrencyNull = true;


		public bool IsCommentsNull = true;


		public bool IsDataImportIDNull = true;


		public bool IsDateAddedNull = true;


		public bool IsGLDataIDNull = true;


		public bool IsGLDataRecItemIDNull = true;


		public bool IsIsAttachmentAvailableNull = true;


		public bool IsJournalEntryRefNull = true;


		public bool IsLocalCurrencyCodeNull = true;


		public bool IsOpenDateNull = true;


		public bool IsReconciliationCategoryIDNull = true;


		public bool IsReconciliationCategoryTypeIDNull = true;


		public bool IsCloseCommentsNull = true;

        public bool IsOriginalGLDataRecItemIDNull = true;

        public bool IsPreviousGLDataRecItemIDNull = true;

        public bool IsIsActiveNull = true;


		public bool IsCloseDateNull = true;

        public bool IsDateRevisedNull = true;

        public bool IsRevisedByNull = true;

        public bool IsAddedByUserIDNull = true;

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

		[XmlElement(ElementName = "Amount")]
		public virtual System.Decimal? Amount 
		{
			get 
			{
				return this._Amount;
			}
			set 
			{
				this._Amount = value;

									this.IsAmountNull = false;
							}
		}			

		[XmlElement(ElementName = "AmountBaseCurrency")]
		public virtual System.Decimal? AmountBaseCurrency 
		{
			get 
			{
				return this._AmountBaseCurrency;
			}
			set 
			{
				this._AmountBaseCurrency = value;

									this.IsAmountBaseCurrencyNull = false;
							}
		}			

		[XmlElement(ElementName = "AmountReportingCurrency")]
		public virtual System.Decimal? AmountReportingCurrency 
		{
			get 
			{
				return this._AmountReportingCurrency;
			}
			set 
			{
				this._AmountReportingCurrency = value;

									this.IsAmountReportingCurrencyNull = false;
							}
		}			

		[XmlElement(ElementName = "Comments")]
		public virtual System.String Comments 
		{
			get 
			{
				return this._Comments;
			}
			set 
			{
				this._Comments = value;

									this.IsCommentsNull = (_Comments == null);
							}
		}			

		[XmlElement(ElementName = "DataImportID")]
		public virtual System.Int32? DataImportID 
		{
			get 
			{
				return this._DataImportID;
			}
			set 
			{
				this._DataImportID = value;

									this.IsDataImportIDNull = false;
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

		
		[XmlElement(ElementName = "GLDataID")]
		public virtual System.Int64? GLDataID 
		{
			get 
			{
				return this._GLDataID;
			}
			set 
			{
				this._GLDataID = value;

									this.IsGLDataIDNull = false;
							}
		}			

		[XmlElement(ElementName = "GLDataRecItemID")]
		public virtual System.Int64? GLDataRecItemID 
		{
			get 
			{
				return this._GLDataRecItemID;
			}
			set 
			{
				this._GLDataRecItemID = value;

									this.IsGLDataRecItemIDNull = false;
							}
		}			

		[XmlElement(ElementName = "IsAttachmentAvailable")]
		public virtual System.Boolean? IsAttachmentAvailable 
		{
			get 
			{
				return this._IsAttachmentAvailable;
			}
			set 
			{
				this._IsAttachmentAvailable = value;

									this.IsIsAttachmentAvailableNull = false;
							}
		}			

		[XmlElement(ElementName = "JournalEntryRef")]
		public virtual System.String JournalEntryRef 
		{
			get 
			{
				return this._JournalEntryRef;
			}
			set 
			{
				this._JournalEntryRef = value;

									this.IsJournalEntryRefNull = (this._JournalEntryRef == null);
							}
		}			

		[XmlElement(ElementName = "LocalCurrency")]
		public virtual System.String LocalCurrencyCode 
		{
			get 
			{
				return this._LocalCurrencyCode;
			}
			set 
			{
				this._LocalCurrencyCode = value;

									this.IsLocalCurrencyCodeNull = (_LocalCurrencyCode == null);
							}
		}			

		[XmlElement(ElementName = "OpenDate")]
		public virtual System.DateTime? OpenDate 
		{
			get 
			{
				return this._OpenDate;
			}
			set 
			{
				this._OpenDate = value;

									this.IsOpenDateNull = (_OpenDate == DateTime.MinValue);
							}
		}			

		[XmlElement(ElementName = "ReconciliationCategoryID")]
		public virtual System.Int16? ReconciliationCategoryID 
		{
			get 
			{
				return this._ReconciliationCategoryID;
			}
			set 
			{
				this._ReconciliationCategoryID = value;

									this.IsReconciliationCategoryIDNull = false;
							}
		}			

		[XmlElement(ElementName = "ReconciliationCategoryTypeID")]
		public virtual System.Int16? ReconciliationCategoryTypeID 
		{
			get 
			{
				return this._ReconciliationCategoryTypeID;
			}
			set 
			{
				this._ReconciliationCategoryTypeID = value;

									this.IsReconciliationCategoryTypeIDNull = false;
							}
		}

        [XmlElement(ElementName = "CloseComments")]
		public virtual System.String CloseComments 
		{
			get 
			{
				return this._CloseComments;
			}
			set 
			{
				this._CloseComments = value;

									this.IsCloseCommentsNull = (_CloseComments == null);
							}
		}

        [XmlElement(ElementName = "CloseDate")]
		public virtual System.DateTime? CloseDate 
		{
			get 
			{
				return this._CloseDate;
			}
			set 
			{
				this._CloseDate = value;

									this.IsCloseDateNull = (_CloseDate == DateTime.MinValue);
							}
		}

        [XmlElement(ElementName = "OriginalGLDataRecItemID")]
        public virtual System.Int64? OriginalGLDataRecItemID
        {
            get
            {
                return this._OriginalGLDataRecItemID;
            }
            set
            {
                this._OriginalGLDataRecItemID = value;

                this.IsOriginalGLDataRecItemIDNull = (_OriginalGLDataRecItemID == null);
            }
        }

        [XmlElement(ElementName = "PreviousGLDataRecItemID")]
        public virtual System.Int64? PreviousGLDataRecItemID
        {
            get
            {
                return this._PreviousGLDataRecItemID;
            }
            set
            {
                this._PreviousGLDataRecItemID = value;

                this.IsPreviousGLDataRecItemIDNull = (_PreviousGLDataRecItemID == null);
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

                this.IsIsActiveNull = (_IsActive == null);
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
                this._DateRevised= value;

                this.IsDateRevisedNull = (_DateRevised == null);
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

        [XmlElement(ElementName = "AddedByUserID")]
        public virtual System.Int32? AddedByUserID
        {
            get
            {
                return this._AddedByUserID;
            }
            set
            {
                this._AddedByUserID = value;

                this.IsAddedByUserIDNull = (_AddedByUserID == null);
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
