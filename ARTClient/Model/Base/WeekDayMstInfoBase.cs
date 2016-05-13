

using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using Adapdev.Text;

namespace SkyStem.ART.Client.Model.Base
{

    /// <summary>
    /// An object representation of the SkyStemART WeekDayMst table
    /// </summary>
    [Serializable]
    public abstract class WeekDayMstInfoBase
    {

        protected System.String _AddedBy = string.Empty;
        protected System.DateTime? _DateAdded = null;
        protected System.DateTime? _DateRevised = null;
        protected System.String _HostName = string.Empty;
        protected System.Boolean? _IsActive = null;
        protected System.String _RevisedBy = string.Empty;
        protected System.Int16? _SortOrder = null;
        protected System.Int16? _WeekDayID = null;
        protected System.String _WeekDayName = string.Empty;
        protected System.Int32? _WeekDayNameLabelID = null;
        protected System.Byte? _WeekDayNumber = null;


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

        [XmlElement(ElementName = "SortOrder")]
        public virtual System.Int16? SortOrder
        {
            get
            {
                return this._SortOrder;
            }
            set
            {
                this._SortOrder = value;

            }
        }

        [XmlElement(ElementName = "WeekDayID")]
        public virtual System.Int16? WeekDayID
        {
            get
            {
                return this._WeekDayID;
            }
            set
            {
                this._WeekDayID = value;

            }
        }

        [XmlElement(ElementName = "WeekDayName")]
        public virtual System.String WeekDayName
        {
            get
            {
                return this._WeekDayName;
            }
            set
            {
                this._WeekDayName = value;

            }
        }

        [XmlElement(ElementName = "WeekDayNameLabelID")]
        public virtual System.Int32? WeekDayNameLabelID
        {
            get
            {
                return this._WeekDayNameLabelID;
            }
            set
            {
                this._WeekDayNameLabelID = value;

            }
        }

        [XmlElement(ElementName = "WeekDayNumber")]
        public virtual System.Byte? WeekDayNumber
        {
            get
            {
                return this._WeekDayNumber;
            }
            set
            {
                this._WeekDayNumber = value;

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
