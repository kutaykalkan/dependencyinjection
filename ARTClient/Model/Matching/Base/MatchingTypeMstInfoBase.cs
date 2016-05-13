

using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using Adapdev.Text;

namespace SkyStem.ART.Client.Model.Matching.Base
{

    /// <summary>
    /// An object representation of the SkyStemART MatchingTypeMst table
    /// </summary>
    [Serializable]
    public abstract class MatchingTypeMstInfoBase
    {

        protected System.String _AddedBy = null;
        protected System.DateTime? _DateAdded = null;
        protected System.DateTime? _DateRevised = null;
        protected System.String _Description = null;
        protected System.String _HostName = null;
        protected System.Boolean? _IsActive = null;
        protected System.String _MatchingType = null;
        protected System.Int16? _MatchingTypeID = null;
        protected System.Int32? _MatchingTypeLabelID = null;
        protected System.String _RevisedBy = null;


        public bool IsAddedByNull = true;


        public bool IsDateAddedNull = true;


        public bool IsDateRevisedNull = true;


        public bool IsDescriptionNull = true;


        public bool IsHostNameNull = true;


        public bool IsIsActiveNull = true;


        public bool IsMatchingTypeNull = true;


        public bool IsMatchingTypeIDNull = true;


        public bool IsMatchingTypeLabelIDNull = true;


        public bool IsRevisedByNull = true;

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

                this.IsAddedByNull = (_AddedBy == null);
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

                this.IsDateAddedNull = (_DateAdded == DateTime.MinValue);
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

                this.IsDateRevisedNull = (_DateRevised == DateTime.MinValue);
            }
        }

        [XmlElement(ElementName = "Description")]
        public virtual System.String Description
        {
            get
            {
                return this._Description;
            }
            set
            {
                this._Description = value;

                this.IsDescriptionNull = (_Description == null);
            }
        }

        [XmlElement(ElementName = "HostName")]
        public virtual System.String HostName
        {
            get
            {
                return this._HostName;
            }
            set
            {
                this._HostName = value;

                this.IsHostNameNull = (_HostName == null);
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

                this.IsIsActiveNull = false;
            }
        }

        [XmlElement(ElementName = "MatchingType")]
        public virtual System.String MatchingType
        {
            get
            {
                return this._MatchingType;
            }
            set
            {
                this._MatchingType = value;

                this.IsMatchingTypeNull = (_MatchingType == null);
            }
        }

        [XmlElement(ElementName = "MatchingTypeID")]
        public virtual System.Int16? MatchingTypeID
        {
            get
            {
                return this._MatchingTypeID;
            }
            set
            {
                this._MatchingTypeID = value;

                this.IsMatchingTypeIDNull = false;
            }
        }

        [XmlElement(ElementName = "MatchingTypeLabelID")]
        public virtual System.Int32? MatchingTypeLabelID
        {
            get
            {
                return this._MatchingTypeLabelID;
            }
            set
            {
                this._MatchingTypeLabelID = value;

                this.IsMatchingTypeLabelIDNull = false;
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

                this.IsRevisedByNull = (_RevisedBy == null);
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
