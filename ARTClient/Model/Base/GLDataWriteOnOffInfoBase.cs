

using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using Adapdev.Text;

namespace SkyStem.ART.Client.Model.Base
{    

	/// <summary>
	/// An object representation of the SkyStemArt GLDataWriteOnOff table
	/// </summary>
	[Serializable]
	public abstract class GLDataWriteOnOffInfoBase
	{

        protected System.String _AddedBy = "";
        public bool IsAddedByNull = true;
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


        protected System.DateTime? _DateAdded = null;
        public bool IsDateAddedNull = true;
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


        protected System.String _RevisedBy = "";
        public bool IsRevisedByNull = true;
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

        protected System.DateTime? _DateRevised = null;
        public bool IsDateRevisedNull = true;
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
	
		protected System.Decimal? _Amount = 0.00M;
		protected System.String _Comments = "";
		protected System.DateTime? _Date = DateTime.Now;
		protected System.Int16? _WriteOnOffID = null ;
		protected System.Int64? _GLDataID = 0;
		protected System.Int64? _GLDataWriteOnOffID = 0;
        protected System.String _JournalEntryRef = "";
		protected System.String _LocalCurrencyCode = "";
        //protected System.Int32? _ProposedEntryAccountRef = 0;
		protected System.String _CloseComments = "";
		protected System.DateTime? _CloseDate = null;
        protected System.DateTime? _OpenDate = null;
		protected System.Int32? _AddedByUserID = 0;
        //protected System.Decimal? _WriteOnOff = 0.00M;




		public bool IsAmountNull = true;


		public bool IsCommentsNull = true;


		public bool IsDateNull = true;


		public bool IsWriteOnOffIDNull = true;


		public bool IsGLDataIDNull = true;


		public bool IsGLDataWriteOnOffIDNull = true;


		public bool IsJournalEntryRefNull = true;


		public bool IsLocalCurrencyCodeNull = true;


        //public bool IsProposedEntryAccountRefNull = true;


		public bool IsCloseCommentsNull = true;


		public bool IsCloseDateNull = true;


		public bool IsTransactionDateNull = true;


		public bool IsAddedByUserIDNull = true;


        //public bool IsWriteOnOffNull = true;

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

		[XmlElement(ElementName = "Date")]
		public virtual System.DateTime? Date 
		{
			get 
			{
				return this._Date;
			}
			set 
			{
				this._Date = value;

									this.IsDateNull = (_Date == DateTime.MinValue);
							}
		}			

		[XmlElement(ElementName = "DebitCredit")]
		public virtual System.Int16? WriteOnOffID 
		{
			get 
			{
				return this._WriteOnOffID;
			}
			set 
			{
				this._WriteOnOffID = value;

									this.IsWriteOnOffIDNull = false;
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

		[XmlElement(ElementName = "GLDataWriteOnOffID")]
		public virtual System.Int64? GLDataWriteOnOffID 
		{
			get 
			{
				return this._GLDataWriteOnOffID;
			}
			set 
			{
				this._GLDataWriteOnOffID = value;

									this.IsGLDataWriteOnOffIDNull = false;
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

									this.IsJournalEntryRefNull = false;
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

        //[XmlElement(ElementName = "ProposedEntryAccountRef")]
        //public virtual System.Int32? ProposedEntryAccountRef 
        //{
        //    get 
        //    {
        //        return this._ProposedEntryAccountRef;
        //    }
        //    set 
        //    {
        //        this._ProposedEntryAccountRef = value;

        //                            this.IsProposedEntryAccountRefNull = false;
        //                    }
        //}			

		[XmlElement(ElementName = "ResolutionComments")]
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

									this.IsTransactionDateNull = (_OpenDate == DateTime.MinValue);
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

									this.IsAddedByUserIDNull = false;
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
