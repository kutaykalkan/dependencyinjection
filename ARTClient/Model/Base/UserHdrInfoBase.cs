

using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using Adapdev.Text;

namespace SkyStem.ART.Client.Model.Base
{

    /// <summary>
    /// An object representation of the SkyStemArt UserHdr table
    /// </summary>
    [Serializable]
    public abstract class UserHdrInfoBase
    {
        private IList<UserRoleInfo> _UserRoleByUserID = null;

        protected System.String _AddedBy = "";
        protected System.Int32? _CompanyID = 0;
        protected System.DateTime? _DateAdded = DateTime.Now;
        protected System.DateTime? _DateRevised = DateTime.Now;
        protected System.Int16? _DefaultRoleID = 0;
        protected System.String _EmailID = "";
        protected System.String _FirstName = "";
        protected System.String _HostName = "";
        protected System.Boolean? _IsActive = false;
        protected System.Boolean? _IsNew = false;
        protected System.String _JobTitle = "";
        protected System.DateTime? _LastLoggedIn = DateTime.Now;
        protected System.String _LastName = "";
        protected System.String _LoginID = "";
        protected System.String _Password = "";
        protected System.String _Phone = "";
        protected System.String _RevisedBy = "";
        protected System.Int32? _UserID = 0;
        protected System.String _WorkPhone = "";


        public bool IsAddedByNull = true;
        public bool IsCompanyIDNull = true;
        public bool IsDateAddedNull = true;
        public bool IsDateRevisedNull = true;
        public bool IsDefaultRoleIDNull = true;
        public bool IsEmailIDNull = true;
        public bool IsFirstNameNull = true;
        public bool IsHostNameNull = true;
        public bool IsIsActiveNull = true;
        public bool IsIsNewNull = true;
        public bool IsJobTitleNull = true;
        public bool IsLastLoggedInNull = true;
        public bool IsLastNameNull = true;
        public bool IsLoginIDNull = true;
        public bool IsPasswordNull = true;
        public bool IsPhoneNull = true;
        public bool IsRevisedByNull = true;
        public bool IsUserIDNull = true;
        public bool IsWorkPhoneNull = true;

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

        [XmlElement(ElementName = "DefaultRoleID")]
        public virtual System.Int16? DefaultRoleID
        {
            get
            {
                return this._DefaultRoleID;
            }
            set
            {
                this._DefaultRoleID = value;

                this.IsDefaultRoleIDNull = false;
            }
        }

        [XmlElement(ElementName = "EmailID")]
        public virtual System.String EmailID
        {
            get
            {
                return this._EmailID;
            }
            set
            {
                this._EmailID = value;

                this.IsEmailIDNull = (_EmailID == null);
            }
        }

        [XmlElement(ElementName = "FirstName")]
        public virtual System.String FirstName
        {
            get
            {
                return this._FirstName;
            }
            set
            {
                this._FirstName = value;

                this.IsFirstNameNull = (_FirstName == null);
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

        [XmlElement(ElementName = "IsNew")]
        public virtual System.Boolean? IsNew
        {
            get
            {
                return this._IsNew;
            }
            set
            {
                this._IsNew = value;

                this.IsIsNewNull = false;
            }
        }

        [XmlElement(ElementName = "JobTitle")]
        public virtual System.String JobTitle
        {
            get
            {
                return this._JobTitle;
            }
            set
            {
                this._JobTitle = value;

                this.IsJobTitleNull = (_JobTitle == null);
            }
        }

        [XmlElement(ElementName = "LastLoggedIn")]
        public virtual System.DateTime? LastLoggedIn
        {
            get
            {
                return this._LastLoggedIn;
            }
            set
            {
                this._LastLoggedIn = value;

                this.IsLastLoggedInNull = (_LastLoggedIn == DateTime.MinValue);
            }
        }

        [XmlElement(ElementName = "LastName")]
        public virtual System.String LastName
        {
            get
            {
                return this._LastName;
            }
            set
            {
                this._LastName = value;

                this.IsLastNameNull = (_LastName == null);
            }
        }

        [XmlElement(ElementName = "LoginID")]
        public virtual System.String LoginID
        {
            get
            {
                return this._LoginID;
            }
            set
            {
                this._LoginID = value;

                this.IsLoginIDNull = (_LoginID == null);
            }
        }

        [XmlElement(ElementName = "Password")]
        public virtual System.String Password
        {
            get
            {
                return this._Password;
            }
            set
            {
                this._Password = value;

                this.IsPasswordNull = (_Password == null);
            }
        }

        [XmlElement(ElementName = "Phone")]
        public virtual System.String Phone
        {
            get
            {
                return this._Phone;
            }
            set
            {
                this._Phone = value;

                this.IsPhoneNull = (_Phone == null);
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

                this.IsUserIDNull = false;
            }
        }

        [XmlElement(ElementName = "WorkPhone")]
        public virtual System.String WorkPhone
        {
            get
            {
                return this._WorkPhone;
            }
            set
            {
                this._WorkPhone = value;

                this.IsWorkPhoneNull = (_WorkPhone == null);
            }
        }

        /// <summary>
        /// Returns a string representation of the object, displaying all property and field names and values.
        /// </summary>
        public override string ToString()
        {
            return StringUtil.ToString(this);
        }

        //[XmlElement(ElementName = "UserRoleByUserID")]
        [XmlIgnore ]
        public IList<UserRoleInfo> UserRoleByUserID
        {
            get { return _UserRoleByUserID; }
            set { _UserRoleByUserID = value; }
        }

    }
}
