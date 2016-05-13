

using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using Adapdev.Text;

namespace SkyStem.ART.Client.Model.Matching.Base
{

    /// <summary>
    /// An object representation of the SkyStemART MatchingConfiguration table
    /// </summary>
    [Serializable]
    public abstract class MatchingConfigurationInfoBase
    {
        protected System.String _AddedBy = null;
        protected System.DateTime? _DateAdded = null;
        protected System.DateTime? _DateRevised = null;
        protected System.String _DisplayColumnName = null;
        protected System.Boolean? _IsActive = null;
        protected System.Boolean? _IsMatching = null;
        protected System.Boolean? _IsPartialMatching = null;
        protected System.Int64? _MatchingConfigurationID = null;
        protected System.Int64? _MatchingSource1ColumnID = null;
        protected System.Int64? _MatchingSource2ColumnID = null;
        protected System.Int64? _MatchSetSubSetCombinationID = null;
        protected System.String _RevisedBy = null;
        protected System.Int16? _DataTypeID = null;

        [XmlElement(ElementName = "AddedBy")]
        public virtual System.String AddedBy
        {
            get
            {
                return this._AddedBy;
            }
            set
            {
                this._AddedBy = value;

            }
        }

        [XmlElement(ElementName = "DateAdded")]
        public virtual System.DateTime? DateAdded
        {
            get
            {
                return this._DateAdded;
            }
            set
            {
                this._DateAdded = value;

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

            }
        }

        [XmlElement(ElementName = "DisplayColumnName")]
        public virtual System.String DisplayColumnName
        {
            get
            {
                return this._DisplayColumnName;
            }
            set
            {
                this._DisplayColumnName = value;

            }
        }

        [XmlElement(ElementName = "IsActive")]
        public virtual System.Boolean? IsActive
        {
            get
            {
                return this._IsActive;
            }
            set
            {
                this._IsActive = value;

            }
        }

        [XmlElement(ElementName = "IsMatching")]
        public virtual System.Boolean? IsMatching
        {
            get
            {
                return this._IsMatching;
            }
            set
            {
                this._IsMatching = value;

            }
        }

        [XmlElement(ElementName = "IsPartialMatching")]
        public virtual System.Boolean? IsPartialMatching
        {
            get
            {
                return this._IsPartialMatching;
            }
            set
            {
                this._IsPartialMatching = value;

            }
        }

        [XmlElement(ElementName = "MachingConfigurationID")]
        public virtual System.Int64? MatchingConfigurationID
        {
            get
            {
                return this._MatchingConfigurationID;
            }
            set
            {
                this._MatchingConfigurationID = value;

            }
        }

        [XmlElement(ElementName = "MatchingSource1ColumnID")]
        public virtual System.Int64? MatchingSource1ColumnID
        {
            get
            {
                return this._MatchingSource1ColumnID;
            }
            set
            {
                this._MatchingSource1ColumnID = value;

            }
        }

        [XmlElement(ElementName = "MatchingSource2ColumnID")]
        public virtual System.Int64? MatchingSource2ColumnID
        {
            get
            {
                return this._MatchingSource2ColumnID;
            }
            set
            {
                this._MatchingSource2ColumnID = value;

            }
        }

        [XmlElement(ElementName = "MatchSetSubSetCombinationID")]
        public virtual System.Int64? MatchSetSubSetCombinationID
        {
            get
            {
                return this._MatchSetSubSetCombinationID;
            }
            set
            {
                this._MatchSetSubSetCombinationID = value;

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

            }
        }


        [XmlElement(ElementName = "DataTypeID")]
        public virtual System.Int16? DataTypeID
        {
            get
            {
                return this._DataTypeID;
            }
            set
            {
                this._DataTypeID = value;
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
