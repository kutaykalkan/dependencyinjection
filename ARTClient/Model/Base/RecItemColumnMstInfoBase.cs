

using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using Adapdev.Text;
using SkyStem.ART.Client.Model.Matching;

namespace SkyStem.ART.Client.Model.Base
{

    /// <summary>
    /// An object representation of the SkyStemART RecItemColumnMst table
    /// </summary>
    [Serializable]
    public abstract class RecItemColumnMstInfoBase
    {
        protected System.String _AddedBy = null;
        protected System.DateTime? _DateAdded = null;
        protected System.DateTime? _DateRevised = null;
        protected System.String _HostName = null;
        protected System.Boolean? _IsActive = null;
        protected System.Int32? _RecItemColumnID = null;
        protected System.String _RecItemColumnName = null;
        protected System.Int32? _RecItemColumnNameLabelID = null;
        protected System.String _RevisedBy = null;



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

        [XmlElement(ElementName = "RecItemColumnID")]
        public virtual System.Int32? RecItemColumnID
        {
            get
            {
                return this._RecItemColumnID;
            }
            set
            {
                this._RecItemColumnID = value;

            }
        }

        [XmlElement(ElementName = "RecItemColumnName")]
        public virtual System.String RecItemColumnName
        {
            get
            {
                return this._RecItemColumnName;
            }
            set
            {
                this._RecItemColumnName = value;

            }
        }

        [XmlElement(ElementName = "RecItemColumnNameLabelID")]
        public virtual System.Int32? RecItemColumnNameLabelID
        {
            get
            {
                return this._RecItemColumnNameLabelID;
            }
            set
            {
                this._RecItemColumnNameLabelID = value;

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


        /// <summary>
        /// Returns a string representation of the object, displaying all property and field names and values.
        /// </summary>
        public override string ToString()
        {
            return StringUtil.ToString(this);
        }
    }
}
