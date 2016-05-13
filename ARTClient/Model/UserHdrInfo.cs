
using System;
using Adapdev.Text;
using SkyStem.ART.Client.Model.Base;
using System.Runtime.Serialization;
using System.Collections.Generic;
using SkyStem.ART.Client.Data;
using System.Xml.Serialization;
using System.IO;
using System.Xml;

namespace SkyStem.ART.Client.Model
{

    /// <summary>
    /// An object representation of the SkyStemArt UserHdr table
    /// </summary>
    [Serializable]
    [DataContract]
    public class UserHdrInfo : UserHdrInfoBase
    {
        private string _Name;

        // TODO: Shld be a read only property
        public string Name
        {
            get
            {
                if (this._Name == null)
                {
                    return ModelHelper.GetFullName(this.FirstName, this.LastName);
                }
                else
                {
                    return this._Name;
                }
            }
            set
            {
                this._Name = value;
            }
        }

        protected short? _RoleID = 0;

        [DataMember]
        [XmlElement(ElementName = "RoleID")]
        public short? RoleID
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

        protected string _AlertReplacement = string.Empty;

        [DataMember]
        [XmlElement(ElementName = "AlertReplacement")]
        public string AlertReplacement
        {
            get
            {
                return this._AlertReplacement;
            }
            set
            {
                this._AlertReplacement = value;
            }
        }

        protected int? _DefaultLanguageID = 1033;

        [DataMember]
        [XmlElement(ElementName = "DefaultLanguageID")]
        public int? DefaultLanguageID
        {
            get
            {
                return this._DefaultLanguageID;
            }
            set
            {
                this._DefaultLanguageID = value;
            }
        }

        [DataMember]
        [XmlElement(ElementName = "UserTransitID")]
        public Int32? UserTransitID { get; set; }

        [DataMember]
        [XmlElement(ElementName = "UserRoleInfoList")]
        public List<UserRoleInfo> UserRoleInfoList { get; set; }

        [DataMember]
        [XmlElement(ElementName = "IsCreated")]
        public System.Boolean IsCreated { get; set; }

        [DataMember]
        [XmlElement(ElementName = "IsUpdated")]
        public System.Boolean IsUpdated { get; set; }

        [DataMember]
        [XmlElement(ElementName = "CompanyDisplayName")]
        public System.String CompanyDisplayName { get; set; }

        [DataMember]
        [XmlElement(ElementName = "GeneratedPassword")]
        public System.String GeneratedPassword { get; set; }

        [DataMember]
        [XmlElement(ElementName = "IsLoginIDUnique")]
        public System.Boolean? IsLoginIDUnique { get; set; }

        [DataMember]
        [XmlElement(ElementName = "IsFTPLoginIDUnique")]
        public System.Boolean? IsFTPLoginIDUnique { get; set; }

        [DataMember]
        [XmlElement(ElementName = "IsEmailIDUnique")]
        public System.Boolean? IsEmailIDUnique { get; set; }

        [DataMember]
        [XmlElement(ElementName = "ExcelRowNumber")]
        public System.Int32? ExcelRowNumber { get; set; }

        [DataMember]
        [XmlElement(ElementName = "IsPasswordResetRequired")]
        public System.Boolean? IsPasswordResetRequired { get; set; }

        [DataMember]
        [XmlElement(ElementName = "PasswordResetDays")]
        public System.Int16? PasswordResetDays { get; set; }

        [DataMember]
        [XmlElement(ElementName = "AddedByRoleID")]
        public short? AddedByRoleID { get; set; }

        [DataMember]
        [XmlElement(ElementName = "UserRoleList")]
        public List<UserRoleInfo> UserRoleList { get; set; }

        [DataMember]
        [XmlElement(ElementName = "UserStatusDetailList")]
        public List<UserStatusDetailInfo> UserStatusDetailList { get; set; }

        [DataMember]
        [XmlElement(ElementName = "UserStatusID")]
        public short? UserStatusID { get; set; }

        [DataMember]
        [XmlElement(ElementName = "AddedByUserID")]
        public int? AddedByUserID { get; set; }

        [DataMember]
        [XmlElement(ElementName = "UserStatusDate")]
        public DateTime? UserStatusDate { get; set; }

        [DataMember]
        [XmlElement(ElementName = "AddedByUserName")]
        public string AddedByUserName { get; set; }

        [DataMember]
        [XmlElement(ElementName = "UserStatusLabelID")]
        public int? UserStatusLabelID { get; set; }

        [DataMember]
        [XmlElement(ElementName = "UserStatus")]
        public string UserStatus { get; set; }
        [DataMember]
        [XmlElement(ElementName = "DisplayNameLabelID")]
        public int? CompanyDisplayNameLabelID { get; set; }

        [DataMember]
        [XmlElement(ElementName = "GeneratedFTPPassword")]
        public System.String GeneratedFTPPassword { get; set; }

        [DataMember]
        [XmlElement(ElementName = "FTPActivationStatusID")]
        public int? FTPActivationStatusID { get; set; }

        [DataMember]
        [XmlElement(ElementName = "CurrentFTPActivationStatusID")]
        public int? CurrentFTPActivationStatusID { get; set; }

        [DataMember]
        [XmlElement(ElementName = "FTPServerID")]
        public short? FTPServerID { get; set; }

        protected System.String _FTPPassword = "";
        public bool IsFTPPasswordNull = true;
        [XmlElement(ElementName = "FTPPassword")]
        public virtual System.String FTPPassword
        {
            get
            {
                return this._FTPPassword;
            }
            set
            {
                this._FTPPassword = value;

                this.IsFTPPasswordNull = (_FTPPassword == null);
            }
        }
        [DataMember]
        [XmlElement(ElementName = "IsFTPPasswordResetRequired")]
        public int? IsFTPPasswordResetRequired { get; set; }

        [DataMember]
        [XmlElement(ElementName = "ActivationStatusTypeID")]
        public short? ActivationStatusTypeID { get; set; }

        [DataMember]
        [XmlElement(ElementName = "FTPActivationStatusLabelID")]
        public int? FTPActivationStatusLabelID { get; set; }

        [DataMember]
        [XmlElement(ElementName = "FTPActivationDate")]
        public DateTime? FTPActivationDate { get; set; }

        [DataMember]
        [XmlElement(ElementName = "IsUserLocked")]
        public bool? IsUserLocked { get; set; }

        [DataMember]
        [XmlElement(ElementName = "LockdownDateTime")]
        public DateTime? LockdownDateTime { get; set; }

        [DataMember]
        [XmlElement(ElementName = "LockdownCount")]
        public int? LockdownCount { get; set; }

        [DataMember]
        [XmlElement(ElementName = "FTPLoginID")]
        public string FTPLoginID { get; set; }
    }
}
