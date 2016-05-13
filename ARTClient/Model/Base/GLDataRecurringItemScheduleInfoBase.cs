

using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using Adapdev.Text;

namespace SkyStem.ART.Client.Model.Base
{

    /// <summary>
    /// An object representation of the SkyStemArt GLDataRecurringItemSchedule table
    /// </summary>
    [Serializable]
    public abstract class GLDataRecurringItemScheduleInfoBase
    {
        protected System.String _AddedBy = "";
        protected System.String _CloseComments = "";
        protected System.DateTime? _CloseDate = null;
        protected System.String _Comments = "";
        protected System.DateTime? _DateAdded = null;
        protected System.DateTime? _DateRevised = null;
        protected System.Int64? _GLDataID = null;
        protected System.Int64? _GLDataRecurringItemScheduleID = null;
        protected System.String _HostName = "";
        protected System.Boolean? _IsActive = false;
        protected System.String _JournalEntryRef = "";
        protected System.String _LocalCurrencyCode = "";
        protected System.DateTime? _OpenDate = null;
        protected System.Int64? _OriginalGLDataRecurringItemScheduleID = null;
        protected System.Int64? _PreviousGLDataRecurringItemScheduleID = null;
        protected System.Int16? _ReconciliationCategoryTypeID = null;
        protected System.Decimal? _RecPeriodAmountBaseCurrency = null;
        protected System.Decimal? _RecPeriodAmountLocalCurrency = null;
        protected System.Decimal? _RecPeriodAmountReportingCurrency = null;
        protected System.String _RevisedBy = "";
        protected System.Decimal? _ScheduleAmount = null;
        protected System.DateTime? _ScheduleBeginDate = null;
        protected System.DateTime? _ScheduleEndDate = null;
        protected System.String _ScheduleName = "";

        protected System.Int32? _AddedByUserID = null;
        [XmlElement(ElementName = "AddedByUserID")]
        public bool IsAddedByUserIDNull = true;
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


        protected System.Decimal? _BalanceLocalCurrency = null;
        [XmlElement(ElementName = "BalanceLocalCurrency")]
        public bool IsBalanceLocalCurrencyNull = true;
        public virtual System.Decimal? BalanceLocalCurrency
        {
            get
            {
                return this._BalanceLocalCurrency;
            }
            set
            {
                this._BalanceLocalCurrency = value;

                this.IsBalanceLocalCurrencyNull = false;
            }
        }

        protected System.Decimal? _BalanceBaseCurrency = null;
        [XmlElement(ElementName = "BalanceBaseCurrency")]
        public bool IsBalanceBaseCurrencyNull = true;
        public virtual System.Decimal? BalanceBaseCurrency
        {
            get
            {
                return this._BalanceBaseCurrency;
            }
            set
            {
                this._BalanceBaseCurrency = value;

                this.IsBalanceBaseCurrencyNull = false;
            }
        }

        protected System.Decimal? _BalanceReportingCurrency = null;
        [XmlElement(ElementName = "BalanceReportingCurrency")]
        public bool IsBalanceReportingCurrencyNull = true;
        public virtual System.Decimal? BalanceReportingCurrency
        {
            get
            {
                return this._BalanceReportingCurrency;
            }
            set
            {
                this._BalanceReportingCurrency = value;

                this.IsBalanceReportingCurrencyNull = false;
            }
        }


        public bool IsAddedByNull = true;


        public bool IsCloseCommentsNull = true;


        public bool IsCloseDateNull = true;


        public bool IsCommentsNull = true;


        public bool IsDateAddedNull = true;


        public bool IsDateRevisedNull = true;


        public bool IsGLDataIDNull = true;


        public bool IsGLDataRecurringItemScheduleIDNull = true;


        public bool IsHostNameNull = true;


        public bool IsIsActiveNull = true;


        public bool IsJournalEntryRefNull = true;


        public bool IsLocalCurrencyCodeNull = true;


        public bool IsOpenDateNull = true;


        public bool IsOriginalGLDataRecurringItemScheduleIDNull = true;


        public bool IsPreviousGLDataRecurringItemScheduleIDNull = true;


        public bool IsReconciliationCategoryTypeIDNull = true;


        public bool IsRecPeriodAmountBaseCurrencyNull = true;


        public bool IsRecPeriodAmountLocalCurrencyNull = true;


        public bool IsRecPeriodAmountReportingCurrencyNull = true;


        public bool IsRevisedByNull = true;


        public bool IsScheduleAmountNull = true;


        public bool IsScheduleBeginDateNull = true;


        public bool IsScheduleEndDateNull = true;


        public bool IsScheduleNameNull = true;

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

        [XmlElement(ElementName = "GLDataRecurringItemScheduleID")]
        public virtual System.Int64? GLDataRecurringItemScheduleID
        {
            get
            {
                return this._GLDataRecurringItemScheduleID;
            }
            set
            {
                this._GLDataRecurringItemScheduleID = value;

                this.IsGLDataRecurringItemScheduleIDNull = false;
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

                this.IsJournalEntryRefNull = (_JournalEntryRef == null);
            }
        }

        [XmlElement(ElementName = "LocalCurrencyCode")]
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

        [XmlElement(ElementName = "OriginalGLDataRecurringItemScheduleID")]
        public virtual System.Int64? OriginalGLDataRecurringItemScheduleID
        {
            get
            {
                return this._OriginalGLDataRecurringItemScheduleID;
            }
            set
            {
                this._OriginalGLDataRecurringItemScheduleID = value;

                this.IsOriginalGLDataRecurringItemScheduleIDNull = false;
            }
        }

        [XmlElement(ElementName = "PreviousGLDataRecurringItemScheduleID")]
        public virtual System.Int64? PreviousGLDataRecurringItemScheduleID
        {
            get
            {
                return this._PreviousGLDataRecurringItemScheduleID;
            }
            set
            {
                this._PreviousGLDataRecurringItemScheduleID = value;

                this.IsPreviousGLDataRecurringItemScheduleIDNull = false;
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

        [XmlElement(ElementName = "RecPeriodAmountBaseCurrency")]
        public virtual System.Decimal? RecPeriodAmountBaseCurrency
        {
            get
            {
                return this._RecPeriodAmountBaseCurrency;
            }
            set
            {
                this._RecPeriodAmountBaseCurrency = value;

                this.IsRecPeriodAmountBaseCurrencyNull = false;
            }
        }

        [XmlElement(ElementName = "RecPeriodAmountLocalCurrency")]
        public virtual System.Decimal? RecPeriodAmountLocalCurrency
        {
            get
            {
                return this._RecPeriodAmountLocalCurrency;
            }
            set
            {
                this._RecPeriodAmountLocalCurrency = value;

                this.IsRecPeriodAmountLocalCurrencyNull = false;
            }
        }

        [XmlElement(ElementName = "RecPeriodAmountReportingCurrency")]
        public virtual System.Decimal? RecPeriodAmountReportingCurrency
        {
            get
            {
                return this._RecPeriodAmountReportingCurrency;
            }
            set
            {
                this._RecPeriodAmountReportingCurrency = value;

                this.IsRecPeriodAmountReportingCurrencyNull = false;
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

        [XmlElement(ElementName = "ScheduleAmount")]
        public virtual System.Decimal? ScheduleAmount
        {
            get
            {
                return this._ScheduleAmount;
            }
            set
            {
                this._ScheduleAmount = value;

                this.IsScheduleAmountNull = false;
            }
        }

        [XmlElement(ElementName = "ScheduleBeginDate")]
        public virtual System.DateTime? ScheduleBeginDate
        {
            get
            {
                return this._ScheduleBeginDate;
            }
            set
            {
                this._ScheduleBeginDate = value;

                this.IsScheduleBeginDateNull = (_ScheduleBeginDate == DateTime.MinValue);
            }
        }

        [XmlElement(ElementName = "ScheduleEndDate")]
        public virtual System.DateTime? ScheduleEndDate
        {
            get
            {
                return this._ScheduleEndDate;
            }
            set
            {
                this._ScheduleEndDate = value;

                this.IsScheduleEndDateNull = (_ScheduleEndDate == DateTime.MinValue);
            }
        }

        [XmlElement(ElementName = "ScheduleName")]
        public virtual System.String ScheduleName
        {
            get
            {
                return this._ScheduleName;
            }
            set
            {
                this._ScheduleName = value;

                this.IsScheduleNameNull = (_ScheduleName == null);
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
