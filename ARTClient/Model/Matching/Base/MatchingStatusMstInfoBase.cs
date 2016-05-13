

using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using Adapdev.Text;

namespace SkyStem.ART.Client.Model.Matching.Base
{

    /// <summary>
    /// An object representation of the SkyStemART MatchingStatusMst table
    /// </summary>
    [Serializable]
    public abstract class MatchingStatusMstInfoBase
    {

        protected System.String _AddedBy = null;
        protected System.DateTime? _DateAdded = null;
        protected System.DateTime? _DateRevised = null;
        protected System.String _Description = null;
        protected System.String _HostName = null;
        protected System.Boolean? _IsActive = null;
        protected System.String _MatchingStatus = null;
        protected System.Int16? _MatchingStatusID = null;
        protected System.Int32? _MatchingStatusLabelID = null;
        protected System.String _RevisedBy = null;

        public bool IsAddedByNull = true;


        public bool IsDateAddedNull = true;


        public bool IsDateRevisedNull = true;


        public bool IsDescriptionNull = true;


        public bool IsHostNameNull = true;


        public bool IsIsActiveNull = true;


        public bool IsMatchingStatusNull = true;


        public bool IsMatchingStatusIDNull = true;


        public bool IsMatchingStatusLabelIDNull = true;


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

        [XmlElement(ElementName = "MatchingStatus")]
        public virtual System.String MatchingStatus
        {
            get
            {
                return this._MatchingStatus;
            }
            set
            {
                this._MatchingStatus = value;

                this.IsMatchingStatusNull = (_MatchingStatus == null);
            }
        }

        [XmlElement(ElementName = "MatchingStatusID")]
        public virtual System.Int16? MatchingStatusID
        {
            get
            {
                return this._MatchingStatusID;
            }
            set
            {
                this._MatchingStatusID = value;

                this.IsMatchingStatusIDNull = false;
            }
        }

        [XmlElement(ElementName = "MatchingStatusLabelID")]
        public virtual System.Int32? MatchingStatusLabelID
        {
            get
            {
                return this._MatchingStatusLabelID;
            }
            set
            {
                this._MatchingStatusLabelID = value;

                this.IsMatchingStatusLabelIDNull = false;
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
