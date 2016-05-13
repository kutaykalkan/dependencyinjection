using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace SkyStem.ART.Service.APP.BLL
{
    public class UserRoleInfo
    {
        protected System.Int32? _UserID = null;
        protected System.Int32? _RoleID = null;
        protected System.String _LoginID = null;

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
        public virtual System.Int32? RoleID
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

            }
        }
    }
}
