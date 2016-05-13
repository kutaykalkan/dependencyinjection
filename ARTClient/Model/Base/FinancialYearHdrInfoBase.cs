

using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using Adapdev.Text;

namespace SkyStem.ART.Client.Model.Base
{

    /// <summary>
    /// An object representation of the SkyStemART FinancialYearHdr table
    /// </summary>
    [Serializable]
    public abstract class FinancialYearHdrInfoBase
    {

        protected System.String _AddedBy = "";
        protected System.Int32? _CompanyID = null;
        protected System.DateTime? _DateAdded = DateTime.Now;
        protected System.DateTime? _DateRevised = DateTime.Now;
        protected System.DateTime? _EndDate = DateTime.MaxValue ;
        protected System.String _FinancialYear = null;
        protected System.Int32? _FinancialYearID = null;
        protected System.String _HostName = "";
        protected System.Boolean? _IsActive = false;
        protected System.String _RevisedBy = "";
        protected System.DateTime? _StartDate = DateTime.MinValue ;




        public bool IsAddedByNull = true;


        public bool IsCompanyIDNull = true;


        public bool IsDateAddedNull = true;


        public bool IsDateRevisedNull = true;


        public bool IsEndDateNull = true;


        public bool IsFinancialYearNull = true;


        public bool IsFinancialYearIDNull = true;


        public bool IsHostNameNull = true;


        public bool IsIsActiveNull = true;


        public bool IsRevisedByNull = true;


        public bool IsStartDateNull = true;

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

        [XmlElement(ElementName = "EndDate")]
        public virtual System.DateTime? EndDate
        {
            get
            {
                return this._EndDate;
            }
            set
            {
                this._EndDate = value;

                this.IsEndDateNull = (_EndDate == null);
            }
        }

        [XmlElement(ElementName = "FinancialYear")]
        public virtual System.String FinancialYear
        {
            get
            {
                return this._FinancialYear;
            }
            set
            {
                this._FinancialYear = value;

                this.IsFinancialYearNull = (_FinancialYear == null);
            }
        }

        [XmlElement(ElementName = "FinancialYearID")]
        public virtual System.Int32? FinancialYearID
        {
            get
            {
                return this._FinancialYearID;
            }
            set
            {
                this._FinancialYearID = value;

                this.IsFinancialYearIDNull = false;
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

        [XmlElement(ElementName = "StartDate")]
        public virtual System.DateTime? StartDate
        {
            get
            {
                return this._StartDate;
            }
            set
            {
                this._StartDate = value;

                this.IsStartDateNull = (_StartDate == null);
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
