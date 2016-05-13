

using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using Adapdev.Text;

namespace SkyStem.ART.Client.Model.Base
{

    /// <summary>
    /// An object representation of the SkyStemArt GeographyStructureHdr table
    /// </summary>
    [Serializable]
    public abstract class GeographyStructureHdrInfoBase : MultilingualInfo
    {

        protected System.String _AddedBy = "";
        protected System.Int32? _CompanyID = 0;
        protected System.DateTime? _DateAdded = DateTime.Now;
        protected System.DateTime? _DateRevised = DateTime.Now;
        protected System.Int16? _GeographyClassID = 0;
        //protected System.String _GeographyStructure = "";
        protected System.Int32? _GeographyStructureID = 0;
        //protected System.Int32? _GeographyStructureLabelID = 0;
        protected System.String _HostName = "";
        protected System.Boolean? _IsActive = false;
        protected System.Int32? _ParentGeographyStructureID = 0;
        protected System.String _RevisedBy = "";




        public bool IsAddedByNull = true;


        public bool IsCompanyIDNull = true;


        public bool IsDateAddedNull = true;


        public bool IsDateRevisedNull = true;


        public bool IsGeographyClassIDNull = true;


        public bool IsGeographyStructureNull = true;


        public bool IsGeographyStructureIDNull = true;


        public bool IsGeographyStructureLabelIDNull = true;


        public bool IsHostNameNull = true;


        public bool IsIsActiveNull = true;


        public bool IsParentGeographyStructureIDNull = true;


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

        [XmlElement(ElementName = "CompanyID")]
        public virtual System.Int32? CompanyID
        {
            get
            {
                return this._CompanyID;
            }
            set
            {
                this._CompanyID = value;

                this.IsCompanyIDNull = false;
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

        [XmlElement(ElementName = "GeographyClassID")]
        public virtual System.Int16? GeographyClassID
        {
            get
            {
                return this._GeographyClassID;
            }
            set
            {
                this._GeographyClassID = value;

                this.IsGeographyClassIDNull = false;
            }
        }

        [XmlElement(ElementName = "GeographyStructure")]
        public virtual System.String GeographyStructure
        {
            get
            {
                return this.Name;
            }
            set
            {
                this.Name = value;
                this.IsGeographyStructureNull = (this.Name == null);
            }
        }

        [XmlElement(ElementName = "GeographyStructureID")]
        public virtual System.Int32? GeographyStructureID
        {
            get
            {
                return this._GeographyStructureID;
            }
            set
            {
                this._GeographyStructureID = value;

                this.IsGeographyStructureIDNull = false;
            }
        }

        [XmlElement(ElementName = "GeographyStructureLabelID")]
        public virtual System.Int32? GeographyStructureLabelID
        {
            get
            {
                return this.LabelID;
            }
            set
            {
                this.LabelID = value;
                this.IsGeographyStructureLabelIDNull = false;
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

        [XmlElement(ElementName = "ParentGeographyStructureID")]
        public virtual System.Int32? ParentGeographyStructureID
        {
            get
            {
                return this._ParentGeographyStructureID;
            }
            set
            {
                this._ParentGeographyStructureID = value;

                this.IsParentGeographyStructureIDNull = false;
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
