using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using Adapdev.Text;
using SkyStem.ART.Client.Model.Base;
using SkyStem.ART.Client.Data;

namespace SkyStem.ART.Client.Model.Report
{

    /// <summary>
    /// An object representation of the SkyStemArt AccountHdr table
    /// </summary>
    [Serializable]
    public class AccountOwnershipReportInfo
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
        protected System.Decimal ? _PercentAccountAssigned = null;
        [XmlElement(ElementName = "PercentAccountAssigned")]
        public virtual System.Decimal? PercentAccountAssigned
        {
            get
            {
                return this._PercentAccountAssigned;
            }
            set
            {
                this._PercentAccountAssigned = value;
            }
        }
        protected System.Int16? _CountTotalAccounts = null;
        [XmlElement(ElementName = "CountTotalAccounts")]
        public virtual System.Int16? CountTotalAccounts
        {
            get
            {
                return this._CountTotalAccounts;
            }
            set
            {
                this._CountTotalAccounts = value;
            }
        }
        protected System.Int16? _CountKeyAccounts = null;
        [XmlElement(ElementName = "CountKeyAccounts")]
        public virtual System.Int16? CountKeyAccounts
        {
            get
            {
                return this._CountKeyAccounts;
            }
            set
            {
                this._CountKeyAccounts = value;
            }
        }
        protected System.Decimal? _PercentKeyAccounts = null;
        [XmlElement(ElementName = "PercentKeyAccounts")]
        public virtual System.Decimal? PercentKeyAccounts
        {
            get
            {
                return this._PercentKeyAccounts;
            }
            set
            {
                this._PercentKeyAccounts = value;
            }
        }
        protected System.Int16? _CountHighAccounts = null;
        [XmlElement(ElementName = "CountHighAccounts")]
        public virtual System.Int16? CountHighAccounts
        {
            get
            {
                return this._CountHighAccounts;
            }
            set
            {
                this._CountHighAccounts = value;
            }
        }

        protected System.Decimal? _PercentHighAccounts = null;
        [XmlElement(ElementName = "PercentHighAccounts")]
        public virtual System.Decimal? PercentHighAccounts
        {
            get
            {
                return this._PercentHighAccounts;
            }
            set
            {
                this._PercentHighAccounts = value;
            }
        }
        protected System.Int16? _CountMediumAccounts = null;
        [XmlElement(ElementName = "CountMediumAccounts")]
        public virtual System.Int16? CountMediumAccounts
        {
            get
            {
                return this._CountMediumAccounts;
            }
            set
            {
                this._CountMediumAccounts = value;
            }
        }
        protected System.Decimal? _PercentMediumAccounts = null;
        [XmlElement(ElementName = "PercentMediumAccounts")]
        public virtual System.Decimal? PercentMediumAccounts
        {
            get
            {
                return this._PercentMediumAccounts;
            }
            set
            {
                this._PercentMediumAccounts = value;
            }
        }
        protected System.Int16? _CountLowAccounts = null;
        [XmlElement(ElementName = "CountLowAccounts")]
        public virtual System.Int16? CountLowAccounts
        {
            get
            {
                return this._CountLowAccounts;
            }
            set
            {
                this._CountLowAccounts = value;
            }
        }
        protected System.Decimal? _PercentLowAccounts = null;
        [XmlElement(ElementName = "PercentLowAccounts")]
        public virtual System.Decimal? PercentLowAccounts
        {
            get
            {
                return this._PercentLowAccounts;
            }
            set
            {
                this._PercentLowAccounts = value;
            }
        }
        protected System.Int16? _CountMaterialAccounts = null;
        [XmlElement(ElementName = "CountMaterialAccounts")]
        public virtual System.Int16? CountMaterialAccounts
        {
            get
            {
                return this._CountMaterialAccounts;
            }
            set
            {
                this._CountMaterialAccounts = value;
            }
        }
        protected System.Decimal? _PercentMaterialAccounts = null;
        [XmlElement(ElementName = "PercentMaterialAccounts")]
        public virtual System.Decimal? PercentMaterialAccounts
        {
            get
            {
                return this._PercentMaterialAccounts;
            }
            set
            {
                this._PercentMaterialAccounts = value;
            }
        }

        public System.String Name
        {
            get
            {
                return ModelHelper.GetFullName(this.FirstName, this.LastName);
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
