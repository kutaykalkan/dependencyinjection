using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using Adapdev.Text;
using SkyStem.ART.Client.Model.Base;

namespace SkyStem.ART.Client.Model.Report
{

    /// <summary>
    /// An object representation of the SkyStemArt AccountHdr table
    /// </summary>
    [Serializable]
    public class CertificationTrackingReportInfo
    {
        #region UserRole fields

        protected System.Int32? _UserID = null;
        protected System.Int16? _RoleID = null;
        protected System.String _FirstName = "";
        protected System.String _LastName = "";
        protected System.Int32? _RoleLabelID = null;
        protected System.String _Role = "";

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
            }
        }
        [XmlElement(ElementName = "RoleLabelID")]
        public virtual System.Int32? RoleLabelID
        {
            get
            {
                return this._RoleLabelID;
            }
            set
            {
                this._RoleLabelID = value;
            }
        }
        [XmlElement(ElementName = "Role")]
        public virtual System.String Role
        {
            get
            {
                return this._Role;
            }
            set
            {
                this._Role = value;
            }
        }
        #endregion


        protected System.Int16? _CountTotalAccountAssigned = null;
        [XmlElement(ElementName = "CountTotalAccountAssigned")]
        public virtual System.Int16? CountTotalAccountAssigned
        {
            get
            {
                return this._CountTotalAccountAssigned;
            }
            set
            {
                this._CountTotalAccountAssigned = value;
            }
        }



        protected System.DateTime? _MadatoryReportSignOffDate = null;
        [XmlElement(ElementName = "MadatoryReportSignOffDate")]
        public virtual System.DateTime? MadatoryReportSignOffDate
        {
            get
            {
                return this._MadatoryReportSignOffDate;
            }
            set
            {
                this._MadatoryReportSignOffDate = value;
            }
        }

        protected System.DateTime? _CertificationBalancesDate = null;
        [XmlElement(ElementName = "CertificationBalancesDate")]
        public virtual System.DateTime? CertificationBalancesDate
        {
            get
            {
                return this._CertificationBalancesDate;
            }
            set
            {
                this._CertificationBalancesDate = value;
            }
        }
        protected System.DateTime? _ExceptionCertificationDate = null;
        [XmlElement(ElementName = "ExceptionCertificationDate")]
        public virtual System.DateTime? ExceptionCertificationDate
        {
            get
            {
                return this._ExceptionCertificationDate;
            }
            set
            {
                this._ExceptionCertificationDate = value;
            }
        }

        protected System.DateTime? _AccountCertificationDate = null;
        [XmlElement(ElementName = "AccountCertificationDate")]
        public virtual System.DateTime? AccountCertificationDate
        {
            get
            {
                return this._AccountCertificationDate;
            }
            set
            {
                this._AccountCertificationDate = value;
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
