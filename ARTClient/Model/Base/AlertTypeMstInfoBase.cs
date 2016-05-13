

using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using Adapdev.Text;

namespace SkyStem.ART.Client.Model.Base
{

    /// <summary>
    /// An object representation of the SkyStemArt AlertTypeMst table
    /// </summary>
    [Serializable]
    public abstract class AlertTypeMstInfoBase
    {
        protected System.String _AddedBy = "";
        protected System.String _AlertType = "";
        protected System.Int16? _AlertTypeID = 0;
        protected System.Int32? _AlertTypeLabelID = 0;
        protected System.DateTime? _DateAdded = DateTime.Now;
        protected System.DateTime? _DateRevised = DateTime.Now;
        protected System.String _HostName = "";
        protected System.Boolean? _IsActive = false;
        protected System.String _RevisedBy = "";




        public bool IsAddedByNull = true;


        public bool IsAlertTypeNull = true;


        public bool IsAlertTypeIDNull = true;


        public bool IsAlertTypeLabelIDNull = true;


        public bool IsDateAddedNull = true;


        public bool IsDateRevisedNull = true;


        public bool IsHostNameNull = true;


        public bool IsIsActiveNull = true;


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

        [XmlElement(ElementName = "AlertType")]
        public virtual System.String AlertType
        {
            get
            {
                return this._AlertType;
            }
            set
            {
                this._AlertType = value;

                this.IsAlertTypeNull = (_AlertType == null);
            }
        }

        [XmlElement(ElementName = "AlertTypeID")]
        public virtual System.Int16? AlertTypeID
        {
            get
            {
                return this._AlertTypeID;
            }
            set
            {
                this._AlertTypeID = value;

                this.IsAlertTypeIDNull = false;
            }
        }

        [XmlElement(ElementName = "AlertTypeLabelID")]
        public virtual System.Int32? AlertTypeLabelID
        {
            get
            {
                return this._AlertTypeLabelID;
            }
            set
            {
                this._AlertTypeLabelID = value;

                this.IsAlertTypeLabelIDNull = false;
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
