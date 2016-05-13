

using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using Adapdev.Text;

namespace SkyStem.ART.Client.Model.Matching.Base
{

    /// <summary>
    /// An object representation of the SkyStemART MatchSetGLDataRecItem table
    /// </summary>
    [Serializable]
    public abstract class MatchSetGLDataRecItemInfoBase
    {

        protected System.String _AddedBy = null;
        protected System.Int32? _AddedByUserID = null;
        protected System.DateTime? _DateAdded = null;
        protected System.DateTime? _DateRevised = null;
        protected System.Int32? _ExcelRowNumber = null;
        protected System.String _HostName = null;
        protected System.Boolean? _IsActive = null;
        protected System.Int64? _MatchSetGLDataRecItemID = null;
        protected System.Int64? _MatchSetMatchingSourceDataImportID = null;
        protected System.Int64? _MatchSetSubSetCombinationID = null;
        protected System.Int16? _RecCategoryID = null;
        protected System.String _RecItemNumber = null;
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

        [XmlElement(ElementName = "AddedByUserID")]
        public virtual System.Int32? AddedByUserID
        {
            get
            {
                return this._AddedByUserID;
            }
            set
            {
                this._AddedByUserID = value;

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

        [XmlElement(ElementName = "ExcelRowNumber")]
        public virtual System.Int32? ExcelRowNumber
        {
            get
            {
                return this._ExcelRowNumber;
            }
            set
            {
                this._ExcelRowNumber = value;

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

        [XmlElement(ElementName = "MatchSetGLDataRecItemID")]
        public virtual System.Int64? MatchSetGLDataRecItemID
        {
            get
            {
                return this._MatchSetGLDataRecItemID;
            }
            set
            {
                this._MatchSetGLDataRecItemID = value;

            }
        }

        [XmlElement(ElementName = "MatchSetMatchingSourceDataImportID")]
        public virtual System.Int64? MatchSetMatchingSourceDataImportID
        {
            get
            {
                return this._MatchSetMatchingSourceDataImportID;
            }
            set
            {
                this._MatchSetMatchingSourceDataImportID = value;

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

        [XmlElement(ElementName = "RecCategoryID")]
        public virtual System.Int16? RecCategoryID
        {
            get
            {
                return this._RecCategoryID;
            }
            set
            {
                this._RecCategoryID = value;

            }
        }

        [XmlElement(ElementName = "RecItemNumber")]
        public virtual System.String RecItemNumber
        {
            get
            {
                return this._RecItemNumber;
            }
            set
            {
                this._RecItemNumber = value;

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
