
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
    public class UserFTPConfigurationInfo
    {
        [DataMember]
        [XmlElement(ElementName = "UserID")]
        public int? UserID { get; set; }
        [DataMember]
        [XmlElement(ElementName = "CompanyID")]
        public int? CompanyID { get; set; }
        [DataMember]
        [XmlElement(ElementName = "FirstName")]
        public string FirstName { get; set; }
        [DataMember]
        [XmlElement(ElementName = "LastName")]
        public string LastName { get; set; }
        private string _Name;
        [DataMember]
        [XmlElement(ElementName = "Name")]
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

        [DataMember]
        [XmlElement(ElementName = "EmailID")]
        public string EmailID { get; set; }

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
        [XmlElement(ElementName = "FTPActivationStatusId")]
        public short? FTPActivationStatusId { get; set; }

        [DataMember]
        [XmlElement(ElementName = "FTPServerId")]
        public short? FTPServerId { get; set; }
        [DataMember]
        [XmlElement(ElementName = "DataImportTypeID")]
        public short? DataImportTypeID { get; set; }
        [DataMember]
        [XmlElement(ElementName = "ImportTemplateID")]
        public int? ImportTemplateID { get; set; }
        [DataMember]
        [XmlElement(ElementName = "LoginID")]
        public string LoginID { get; set; }
        [DataMember]
        [XmlElement(ElementName = "FTPLoginID")]
        public string FTPLoginID { get; set; }
        [DataMember]
        [XmlElement(ElementName = "ProfileName")]
        public string ProfileName { get; set; }
        [DataMember]
        [XmlElement(ElementName = "RoleID")]
        public short? FTPUploadRoleID { get; set; }
        [DataMember]
        [XmlElement(ElementName = "UserFTPConfigurationID")]
        public int? UserFTPConfigurationID { get; set; }
        [DataMember]
        [XmlElement(ElementName = "IsFTPEnabled")]
        public bool? IsFTPEnabled { get; set; }
        [DataMember]
        [XmlElement(ElementName = "ModifyBy")]
        public string ModifyBy { get; set; }
        [DataMember]
        [XmlElement(ElementName = "DateModified")]
        public DateTime? DateModified { get; set; }

        [DataMember]
        [XmlElement(ElementName = "IsProcessFTPDataImportSuccessFull")]
        public bool? IsProcessFTPDataImportSuccessFull { get; set; }
        [DataMember]
        [XmlElement(ElementName = "ErrorMessageLabelID")]
        public int? ErrorMessageLabelID { get; set; }
        [DataMember]
        [XmlElement(ElementName = "ErrorMessage")]
        public string ErrorMessage { get; set; }
        [DataMember]
        [XmlElement(ElementName = "ExceptionMessage")]
        public string ExceptionMessage { get; set; }
        [DataMember]
        [XmlElement(ElementName = "FTPFileName")]
        public string FTPFileName { get; set; }
        [DataMember]
        [XmlElement(ElementName = "FTPFilePath")]
        public string FTPFilePath { get; set; }
        [DataMember]
        [XmlElement(ElementName = "FTPTempFileName")]
        public string FTPTempFileName { get; set; }

        [DataMember]
        [XmlElement(ElementName = "CompanyAlias")]
        public string CompanyAlias { get; set; }
    }
}
