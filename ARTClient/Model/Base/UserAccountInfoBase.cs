using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using Adapdev.Text;

namespace SkyStem.ART.Client.Model.Base
{

    [Serializable]
    public abstract class UserAccountInfoBase
    {
        protected bool? _IsActive = false;
        protected long? _AccountID = 0;
        protected int? _UserID = 0;
        protected long? _UserAccountID = 0;
        protected short? _RoleID = 0;
        public bool IsIsActiveNull = true;
        public bool IsAccountIDNull = true;
        public bool IsUserIDNull = true;
        public bool IsUserAccountIDNull = true;
        public bool IsRoleIDNull = true;


        [XmlElement(ElementName = "IsActive")]
        public virtual bool? IsActive
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



        [XmlElement(ElementName = "AccountID")]
        public virtual long? AccountID
        {
            get
            {
                return this._AccountID;
            }
            set
            {
                this._AccountID = value;
                this.IsAccountIDNull = false;
            }
        }


        [XmlElement(ElementName = "UserID")]
        public virtual int? UserID
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



        [XmlElement(ElementName = "UserAccountID")]
        public virtual long? UserAccountID
        {
            get
            {
                return this._UserAccountID;
            }
            set
            {
                this._UserAccountID = value;
                this.IsUserAccountIDNull = false;
            }
        }


        [XmlElement(ElementName = "RoleID")]
        public virtual short? RoleID
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
        [XmlElement(ElementName = "DefaultLanguageID")]
        public int? DefaultLanguageID { get; set; }

        /// <summary>
        /// Returns a string representation of the object, displaying all property and field names and values.
        /// </summary>
        public override string ToString()
        {
            return StringUtil.ToString(this);
        }

    }
}
