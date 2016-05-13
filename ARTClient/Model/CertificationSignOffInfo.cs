
using System;
using Adapdev.Text;
using SkyStem.ART.Client.Model.Base;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using SkyStem.ART.Client.Data;

namespace SkyStem.ART.Client.Model
{

    /// <summary>
    /// An object representation of the SkyStemArt CertificationSignOff table
    /// </summary>
    [Serializable]
    [DataContract]
    public class CertificationSignOffInfo : CertificationSignOffInfoBase
    {

        protected System.DateTime? _SignOffDate = null;
        public bool IsSignOffDateNull = true;
        [XmlElement(ElementName = "SignOffDate")]
        public System.DateTime? SignOffDate
        {
            get
            {
                return this._SignOffDate;
            }
            set
            {
                this._SignOffDate = value;

                this.IsSignOffDateNull = false;
            }
        }

        protected System.String _SignOffComments = null;
        public bool IsSignOffCommentsNull = true;
        [XmlElement(ElementName = "SignOffComments")]
        public System.String SignOffComments
        {
            get
            {
                return this._SignOffComments;
            }
            set
            {
                this._SignOffComments = value;

                this.IsSignOffCommentsNull = false;
            }
        }

        protected System.Int16? _CertificationTypeID = null;
        public bool IsCertificationTypeIDNull = true;
        [XmlElement(ElementName = "CertificationTypeID")]
        public virtual System.Int16? CertificationTypeID
        {
            get
            {
                return this._CertificationTypeID;
            }
            set
            {
                this._CertificationTypeID = value;

                this.IsCertificationTypeIDNull = false;
            }
        }



        protected System.String _UserFirstName = "";
        public bool IsUserFirstNameNull = true;
        [XmlElement(ElementName = "UserFirstName")]
        public virtual System.String UserFirstName
        {
            get
            {
                return this._UserFirstName;
            }
            set
            {
                this._UserFirstName = value;

                this.IsUserFirstNameNull = (_UserFirstName == null);
            }
        }


        protected System.String _UserLastName = "";
        public bool IsUserLastNameNull = true;
        [XmlElement(ElementName = "UserLastName")]
        public virtual System.String UserLastName
        {
            get
            {
                return this._UserLastName;
            }
            set
            {
                this._UserLastName = value;

                this.IsUserLastNameNull = (_UserLastName == null);
            }
        }

        [XmlElement(ElementName = "Name")]
        public virtual System.String Name
        {
            get
            {
                return ModelHelper.GetFullName(this.UserFirstName, this.UserLastName);
            }
        }


        protected System.Boolean? _IsSameAccess = null;
        public bool IsIsSameAccessNull = true;
        [XmlElement(ElementName = "IsSameAccess")]
        public virtual System.Boolean? IsSameAccess
        {
            get
            {
                return this._IsSameAccess;
            }
            set
            {
                this._IsSameAccess = value;

                this.IsIsSameAccessNull = false;
            }
        }
        [XmlElement(ElementName = "BackupUserID")]
        public Int32? BackupUserID { get; set; }

        [XmlElement(ElementName = "BackupRoleID")]
        public Int16? BackupRoleID { get; set; }

        [XmlElement(ElementName = "BackupFirstName")]
        public string BackupFirstName { get; set; }

        [XmlElement(ElementName = "BackupLastName")]
        public string BackupLastName { get; set; }

        [XmlElement(ElementName = "Name")]
        public virtual System.String BackupUserName
        {
            get
            {
                return ModelHelper.GetFullName(this.BackupFirstName, this.BackupLastName);
            }
        }

        [XmlElement(ElementName = "BackupMadatoryReportSignOffDate")]
        public DateTime? BackupMadatoryReportSignOffDate { get; set; }
        [XmlElement(ElementName = "BackupCertificationBalancesDate")]
        public DateTime? BackupCertificationBalancesDate { get; set; }
        [XmlElement(ElementName = "BackupCertificationBalancesComments")]
        public string BackupCertificationBalancesComments { get; set; }
        [XmlElement(ElementName = "BackupExceptionCertificationDate")]
        public DateTime? BackupExceptionCertificationDate { get; set; }
        [XmlElement(ElementName = "BackupExceptionCertificationComments")]
        public string BackupExceptionCertificationComments { get; set; }
        [XmlElement(ElementName = "BackupAccountCertificationDate")]
        public DateTime? BackupAccountCertificationDate { get; set; }
        [XmlElement(ElementName = "BackupAccountCertificationComments")]
        public string BackupAccountCertificationComments { get; set; }

    }//End Of Class
}//End Of Namespace
