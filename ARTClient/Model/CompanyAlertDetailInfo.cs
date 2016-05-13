
using System;
using Adapdev.Text;
using SkyStem.ART.Client.Model.Base;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace SkyStem.ART.Client.Model
{

    /// <summary>
    /// An object representation of the SkyStemArt CompanyAlertDetail table
    /// </summary>
    [Serializable]
    [DataContract]
    public class CompanyAlertDetailInfo : CompanyAlertDetailInfoBase
    {

        protected System.Boolean? _IsRead = false;
        protected System.Int64? _CompanyAlertDetailUserID = 0;

        public bool IsIsReadNull = true;
        public bool IsCompanyAlertDetailUserIDNull = true;
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

        protected System.Int32? _UserID = 0;
        protected System.Int16? _RoleID = 0;

        public bool IsUserIDNull = true;
        public bool IsRoleIDNull = true;

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

                this.IsUserIDNull = (this._UserID == null);
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

                this.IsRoleIDNull = (this._RoleID == null);
            }
        }
        [DataMember]
        public int? NumberValue { get; set; }
        [DataMember]
        public DateTime? DateValue { get; set; }
    }
}
