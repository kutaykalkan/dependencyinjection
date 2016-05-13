

using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using Adapdev.Text;

namespace SkyStem.ART.Client.Model.Base
{

    /// <summary>
    /// An object representation of the SkyStemArt CompanyHdr table
    /// </summary>
    [Serializable]
    public abstract class CompanyHdrInfoBase
    {
        protected System.String _AddedBy = "";
        protected System.Int32? _CompanyID = 0;
        protected System.String _CompanyName = "";
        protected System.Decimal? _DataStorageCapacity = 0.00M;
        protected System.DateTime? _DateAdded = DateTime.Now;
        protected System.DateTime? _DateRevised = DateTime.Now;
        protected System.String _DisplayName = "";
        protected System.Int32? _DisplayNameLabelID = 0;
        protected System.String _HostName = "";
        protected System.Boolean? _IsActive = false;
        protected System.String _LogoFileName = "";
        protected System.String _LogoPhysicalPath = "";
        protected System.Int32? _NoOfLicensedUsers = 0;
        protected System.String _RevisedBy = "";
        protected System.DateTime? _SubscriptionEndDate = DateTime.Now;
        protected System.DateTime? _SubscriptionStartDate = DateTime.Now;
        protected System.String _WebSite = "";
        protected System.Int32? _NoOfSubscriptionDays = 0;


        public bool IsAddedByNull = true;
        public bool IsCompanyIDNull = true;
        public bool IsCompanyNameNull = true;
        public bool IsDataStorageCapacityNull = true;
        public bool IsDateAddedNull = true;
        public bool IsDateRevisedNull = true;
        public bool IsDisplayNameNull = true;
        public bool IsDisplayNameLabelIDNull = true;
        public bool IsHostNameNull = true;
        public bool IsIsActiveNull = true;
        public bool IsLogoFileNameNull = true;
        public bool IsLogoPhysicalPathNull = true;
        public bool IsNoOfLicensedUsersNull = true;
        public bool IsRevisedByNull = true;
        public bool IsSubscriptionEndDateNull = true;
        public bool IsSubscriptionStartDateNull = true;
        public bool IsWebSiteNull = true;
        public bool IsNoOfSubscriptionDaysNull = true;

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

        [XmlElement(ElementName = "CompanyName")]
        public virtual System.String CompanyName
        {
            get
            {
                return this._CompanyName;
            }
            set
            {
                this._CompanyName = value;

                this.IsCompanyNameNull = (_CompanyName == null);
            }
        }

        [XmlElement(ElementName = "DataStorageCapacity")]
        public virtual System.Decimal? DataStorageCapacity
        {
            get
            {
                return this._DataStorageCapacity;
            }
            set
            {
                this._DataStorageCapacity = value;

                this.IsDataStorageCapacityNull = false;
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

        [XmlElement(ElementName = "DisplayName")]
        public virtual System.String DisplayName
        {
            get
            {
                return this._DisplayName;
            }
            set
            {
                this._DisplayName = value;

                this.IsDisplayNameNull = (_DisplayName == null);
            }
        }

        [XmlElement(ElementName = "DisplayNameLabelID")]
        public virtual System.Int32? DisplayNameLabelID
        {
            get
            {
                return this._DisplayNameLabelID;
            }
            set
            {
                this._DisplayNameLabelID = value;

                this.IsDisplayNameLabelIDNull = false;
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

        [XmlElement(ElementName = "LogoFileName")]
        public virtual System.String LogoFileName
        {
            get
            {
                return this._LogoFileName;
            }
            set
            {
                this._LogoFileName = value;

                this.IsLogoFileNameNull = (_LogoFileName == null);
            }
        }

        [XmlElement(ElementName = "LogoPhysicalPath")]
        public virtual System.String LogoPhysicalPath
        {
            get
            {
                return this._LogoPhysicalPath;
            }
            set
            {
                this._LogoPhysicalPath = value;

                this.IsLogoPhysicalPathNull = (_LogoPhysicalPath == null);
            }
        }

        [XmlElement(ElementName = "NoOfLicensedUsers")]
        public virtual System.Int32? NoOfLicensedUsers
        {
            get
            {
                return this._NoOfLicensedUsers;
            }
            set
            {
                this._NoOfLicensedUsers = value;

                this.IsNoOfLicensedUsersNull = false;
            }
        }


        [XmlElement(ElementName = "NoOfSubscriptionDays")]
        public virtual System.Int32? NoOfSubscriptionDays
        {
            get
            {
                return this._NoOfSubscriptionDays;
            }
            set
            {
                this._NoOfSubscriptionDays = value;

                this.IsNoOfSubscriptionDaysNull = false;
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

        [XmlElement(ElementName = "SubscriptionEndDate")]
        public virtual System.DateTime? SubscriptionEndDate
        {
            get
            {
                return this._SubscriptionEndDate;
            }
            set
            {
                this._SubscriptionEndDate = value;

                this.IsSubscriptionEndDateNull = (_SubscriptionEndDate == DateTime.MinValue);
            }
        }

        [XmlElement(ElementName = "SubscriptionStartDate")]
        public virtual System.DateTime? SubscriptionStartDate
        {
            get
            {
                return this._SubscriptionStartDate;
            }
            set
            {
                this._SubscriptionStartDate = value;

                this.IsSubscriptionStartDateNull = (_SubscriptionStartDate == DateTime.MinValue);
            }
        }

        [XmlElement(ElementName = "WebSite")]
        public virtual System.String WebSite
        {
            get
            {
                return this._WebSite;
            }
            set
            {
                this._WebSite = value;

                this.IsWebSiteNull = (_WebSite == null);
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
