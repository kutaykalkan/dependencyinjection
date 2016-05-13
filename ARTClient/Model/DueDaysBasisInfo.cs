using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Serialization;

namespace SkyStem.ART.Client.Model
{
    [Serializable]
    [DataContract]
    public class DueDaysBasisInfo
    {
      
        protected System.String _AddedBy = string.Empty;
        protected System.DateTime? _DateAdded = null;
        protected System.DateTime? _DateRevised = null;
        protected System.Int16? _DueDaysBasisID = null;
        protected System.String _DueDaysBasis = string.Empty;
        protected System.Int32? _DueDaysBasisLabelID = null;
        protected System.String _HostName = string.Empty;
        protected System.Boolean? _IsActive = null;
        protected System.String _RevisedBy = string.Empty;



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

        [XmlElement(ElementName = "DueDaysBasisID")]
        public virtual System.Int16? DueDaysBasisID
        {
            get
            {
                return this._DueDaysBasisID;
            }
            set
            {
                this._DueDaysBasisID = value;

            }
        }

        [XmlElement(ElementName = "DueDaysBasis")]
        public virtual System.String DueDaysBasis
        {
            get
            {
                return this._DueDaysBasis;
            }
            set
            {
                this._DueDaysBasis = value;

            }
        }

        [XmlElement(ElementName = "DueDaysBasisLabelID")]
        public virtual System.Int32? DueDaysBasisLabelID
        {
            get
            {
                return this._DueDaysBasisLabelID;
            }
            set
            {
                this._DueDaysBasisLabelID = value;

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

    }
}
