

using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using Adapdev.Text;

namespace SkyStem.ART.Client.Model.Base
{

    /// <summary>
    /// An object representation of the SkyStemArt DashboardMst table
    /// </summary>
    [Serializable]
    public abstract class DashboardMstInfoBase
    {
        protected System.String _AddedBy = "";
        protected System.Int16? _DashboardID = null;
        protected System.String _DashboardTitle = "";
        protected System.Int32? _DashboardTitleLabelID = null;
        protected System.DateTime? _DateAdded = DateTime.Now;
        protected System.DateTime? _DateRevised = DateTime.Now;
        protected System.String _Description = "";
        protected System.Int32? _DescriptionLabelID = null;
        protected System.String _HostName = "";
        protected System.Boolean? _IsActive = false;
        protected System.String _RevisedBy = "";
        protected System.String _UserControlUrl = "";
        protected System.Int16? _CapabilityID = null;



        public bool IsAddedByNull = true;


        public bool IsDashboardIDNull = true;


        public bool IsDashboardTitleNull = true;


        public bool IsDashboardTitleLabelIDNull = true;


        public bool IsDateAddedNull = true;


        public bool IsDateRevisedNull = true;


        public bool IsDescriptionNull = true;


        public bool IsDescriptionLabelIDNull = true;


        public bool IsHostNameNull = true;


        public bool IsIsActiveNull = true;


        public bool IsRevisedByNull = true;

        public bool IsUserControlUrlNull = true;

        public bool IsCapabilityIDNull = true;


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

        [XmlElement(ElementName = "DashboardID")]
        public virtual System.Int16? DashboardID
        {
            get
            {
                return this._DashboardID;
            }
            set
            {
                this._DashboardID = value;

                this.IsDashboardIDNull = false;
            }
        }

        [XmlElement(ElementName = "DashboardTitle")]
        public virtual System.String DashboardTitle
        {
            get
            {
                return this._DashboardTitle;
            }
            set
            {
                this._DashboardTitle = value;

                this.IsDashboardTitleNull = (_DashboardTitle == null);
            }
        }

        [XmlElement(ElementName = "DashboardTitleLabelID")]
        public virtual System.Int32? DashboardTitleLabelID
        {
            get
            {
                return this._DashboardTitleLabelID;
            }
            set
            {
                this._DashboardTitleLabelID = value;

                this.IsDashboardTitleLabelIDNull = false;
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

        [XmlElement(ElementName = "DescriptionLabelID")]
        public virtual System.Int32? DescriptionLabelID
        {
            get
            {
                return this._DescriptionLabelID;
            }
            set
            {
                this._DescriptionLabelID = value;

                this.IsDescriptionLabelIDNull = false;
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


        [XmlElement(ElementName = "UserControlUrl")]
        public virtual System.String UserControlUrl
        {
            get
            {
                return this._UserControlUrl;
            }
            set
            {
                this._UserControlUrl = value;
                this.IsUserControlUrlNull = (_UserControlUrl == null);
            }
        }


        [XmlElement(ElementName = "CapabilityID")]
        public virtual System.Int16? CapabilityID
        {
            get
            {
                return this._CapabilityID;
            }
            set
            {
                this._CapabilityID = value;
                this.IsCapabilityIDNull = false;
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
