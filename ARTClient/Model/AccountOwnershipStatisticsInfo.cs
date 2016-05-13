using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SkyStem.ART.Client.Model.Base;
using System.Xml.Serialization;
using SkyStem.ART.Client.Data;

namespace SkyStem.ART.Client.Model
{
    [Serializable]
    public class AccountOwnershipStatisticsInfo
    {
        private Int32? _NoOfAccounts = null;
        private String _FirstName = null;
        private String _LastName = null;
        private Int32? _RoleID = null;
        private Int32? _UserID = null;
        private String _Association = null;
        private Int32? _ApproverUserID = null;

        [XmlElement(ElementName = "RoleID")]
        public Int32? RoleID
        {
            get { return _RoleID; }
            set { _RoleID = value; }
        }


        [XmlElement(ElementName = "ApproverUserID")]
        public Int32? ApproverUserID
        {
            get { return _ApproverUserID; }
            set { _ApproverUserID = value; }

        }

        public String Association
        {
            get { return _Association; }
            set { _Association = value; }
        }

        [XmlElement(ElementName = "UserID")]
        public Int32? UserID
        {
            get { return _UserID; }
            set { _UserID = value; }
        }

        [XmlElement(ElementName = "FirstName")]
        public String FirstName
        {
            get { return _FirstName; }
            set { _FirstName = value; }
        }

        [XmlElement(ElementName = "LastName")]
        public String LastName
        {
            get { return _LastName; }
            set { _LastName = value; }
        }

        [XmlElement(ElementName = "NoOfAccounts")]
        public Int32? NoOfAccounts
        {
            get { return _NoOfAccounts; }
            set { _NoOfAccounts = value; }
        }

        [XmlElement(ElementName = "FullName")]
        public String FullName
        {
            get
            {
                return ModelHelper.GetFullName(this._FirstName, this._LastName);
            }
        }

    }
}
