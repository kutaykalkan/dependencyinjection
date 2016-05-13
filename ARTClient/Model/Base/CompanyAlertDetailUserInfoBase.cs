

using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using Adapdev.Text;

namespace SkyStem.ART.Client.Model.Base
{

    /// <summary>
    /// An object representation of the SkyStemArt CompanyAlertDetailUser table
    /// </summary>
    [Serializable]
    public abstract class CompanyAlertDetailUserInfoBase
    {


        protected System.Int64? _CompanyAlertDetailID = 0;
        protected System.Int64? _CompanyAlertDetailUserID = 0;
        protected System.DateTime? _DateRevised = DateTime.Now;
        protected System.Boolean? _IsRead = false;
        protected System.DateTime? _MailSentDate = DateTime.Now;
        protected System.String _RevisedBy = "";
        protected System.Int16? _RoleID = 0;
        protected System.Int32? _UserID = 0;




        public bool IsCompanyAlertDetailIDNull = true;


        public bool IsCompanyAlertDetailUserIDNull = true;


        public bool IsDateRevisedNull = true;


        public bool IsIsReadNull = true;


        public bool IsMailSentDateNull = true;


        public bool IsRevisedByNull = true;


        public bool IsRoleIDNull = true;


        public bool IsUserIDNull = true;

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

        [XmlElement(ElementName = "CompanyAlertDetailUserID")]
        public virtual System.Int64? CompanyAlertDetailUserID
        {
            get
            {
                return this._CompanyAlertDetailUserID;
            }
            set
            {
                this._CompanyAlertDetailUserID = value;

                this.IsCompanyAlertDetailUserIDNull = false;
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

        [XmlElement(ElementName = "IsRead")]
        public virtual System.Boolean? IsRead
        {
            get
            {
                return this._IsRead;
            }
            set
            {
                this._IsRead = value;

                this.IsIsReadNull = false;
            }
        }

        [XmlElement(ElementName = "MailSentDate")]
        public virtual System.DateTime? MailSentDate
        {
            get
            {
                return this._MailSentDate;
            }
            set
            {
                this._MailSentDate = value;

                this.IsMailSentDateNull = (_MailSentDate == DateTime.MinValue);
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

                this.IsRoleIDNull = false;
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


        /// <summary>
        /// Returns a string representation of the object, displaying all property and field names and values.
        /// </summary>
        public override string ToString()
        {
            return StringUtil.ToString(this);
        }




    }
}
