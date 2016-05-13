

using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using Adapdev.Text;

namespace SkyStem.ART.Client.Model.Base
{

    /// <summary>
    /// An object representation of the SkyStemArt DataImportTypeMst table
    /// </summary>
    [Serializable]
    public abstract class DataImportTypeMstInfoBase : MultilingualInfo
    {

        protected System.String _AddedBy = "";
        //protected System.String _DataImportType = "";
        protected System.Int16? _DataImportTypeID = 0;
        //protected System.Int32? _DataImportTypeLabelID = 0;
        protected System.DateTime? _DateAdded = DateTime.Now;
        protected System.DateTime? _DateRevised = DateTime.Now;
        protected System.String _Description = "";
        protected System.String _HostName = "";
        protected System.Boolean? _IsActive = false;
        protected System.String _RevisedBy = "";
        protected System.Int32? _UploadRulesLabelID = 0;

        public bool IsAddedByNull = true;
        public bool IsDataImportTypeNull = true;
        public bool IsDataImportTypeIDNull = true;
        public bool IsDataImportTypeLabelIDNull = true;
        public bool IsDateAddedNull = true;
        public bool IsDateRevisedNull = true;
        public bool IsDescriptionNull = true;
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

        [XmlElement(ElementName = "DataImportType")]
        public virtual System.String DataImportType
        {
            get
            {
                //return this._DataImportType;
                return this.Name;
            }
            set
            {
                //this._DataImportType = value;
                //this.IsDataImportTypeNull = (_DataImportType == null);
                this.Name = value;
                this.IsDataImportTypeNull = (this.Name == null);
            }
        }

        [XmlElement(ElementName = "DataImportTypeID")]
        public virtual System.Int16? DataImportTypeID
        {
            get
            {
                return this._DataImportTypeID;
            }
            set
            {
                this._DataImportTypeID = value;

                this.IsDataImportTypeIDNull = false;
            }
        }

        [XmlElement(ElementName = "DataImportTypeLabelID")]
        public virtual System.Int32? DataImportTypeLabelID
        {
            get
            {
                //return this._DataImportTypeLabelID;
                return this.LabelID;
            }
            set
            {
                //this._DataImportTypeLabelID = value;
                //this.IsDataImportTypeLabelIDNull = false;
                this.LabelID = value;
                this.IsDataImportTypeLabelIDNull = false;
            }
        }

        [XmlElement(ElementName = "UploadRulesLabelID")]
        public virtual System.Int32? UploadRulesLabelID
        {
            get
            {
                return this._UploadRulesLabelID;
            }
            set
            {
                this._UploadRulesLabelID = value;
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
