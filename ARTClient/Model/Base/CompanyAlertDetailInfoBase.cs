

using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using Adapdev.Text;

namespace SkyStem.ART.Client.Model.Base
{

    /// <summary>
    /// An object representation of the SkyStemArt CompanyAlertDetail table
    /// </summary>
    [Serializable]
    public abstract class CompanyAlertDetailInfoBase
    {

        protected System.String _AddedBy = "";
        protected System.String _AlertDescription = "";
        protected System.DateTime? _AlertExpectedDateTime = DateTime.Now;
        protected System.Int64? _CompanyAlertDetailID = 0;
        protected System.Int32? _CompanyAlertID = 0;
        protected System.DateTime? _DateAdded = DateTime.Now;
        protected System.Boolean? _IsActive = false;
        protected System.String _Url = "";
        protected System.Int32? _ReconciliationPeriodID = 0;




        public bool IsAddedByNull = true;


        public bool IsAlertDescriptionNull = true;


        public bool IsAlertExpectedDateTimeNull = true;


        public bool IsCompanyAlertDetailIDNull = true;


        public bool IsCompanyAlertIDNull = true;


        public bool IsDateAddedNull = true;


        public bool IsIsActiveNull = true;


        public bool IsUrlNull = true;

        public bool IsReconciliationPeriodIDNull = true;

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

        [XmlElement(ElementName = "AlertDescription")]
        public virtual System.String AlertDescription
        {
            get
            {
                return this._AlertDescription;
            }
            set
            {
                this._AlertDescription = value;

                this.IsAlertDescriptionNull = (_AlertDescription == null);
            }
        }

        [XmlElement(ElementName = "AlertExpectedDateTime")]
        public virtual System.DateTime? AlertExpectedDateTime
        {
            get
            {
                return this._AlertExpectedDateTime;
            }
            set
            {
                this._AlertExpectedDateTime = value;

                this.IsAlertExpectedDateTimeNull = (_AlertExpectedDateTime == DateTime.MinValue);
            }
        }

        [XmlElement(ElementName = "CompanyAlertDetailID")]
        public virtual System.Int64? CompanyAlertDetailID
        {
            get
            {
                return this._CompanyAlertDetailID;
            }
            set
            {
                this._CompanyAlertDetailID = value;

                this.IsCompanyAlertDetailIDNull = false;
            }
        }

        [XmlElement(ElementName = "CompanyAlertID")]
        public virtual System.Int32? CompanyAlertID
        {
            get
            {
                return this._CompanyAlertID;
            }
            set
            {
                this._CompanyAlertID = value;

                this.IsCompanyAlertIDNull = false;
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

        [XmlElement(ElementName = "Url")]
        public virtual System.String Url
        {
            get
            {
                return this._Url;
            }
            set
            {
                this._Url = value;

                this.IsUrlNull = (_Url == null);
            }
        }

        [XmlElement(ElementName = "ReconciliationPeriodID")]
        public System.Int32? ReconciliationPeriodID 
        {
            get
            {
                return this._ReconciliationPeriodID;
            }
            set
            {
                this._ReconciliationPeriodID = value;
                this.IsReconciliationPeriodIDNull = (this._ReconciliationPeriodID == null);
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
