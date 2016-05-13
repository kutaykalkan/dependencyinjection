

using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using Adapdev.Text;

namespace SkyStem.ART.Client.Model.Base
{

    /// <summary>
    /// An object representation of the SkyStemArt ColumnMst table
    /// </summary>
    [Serializable]
    public abstract class ColumnMstInfoBase
    {


        protected System.String _AddedBy = "";
        protected System.String _ColumnDescription = "";
        protected System.Int16 _ColumnID = 0;
        protected System.String _ColumnName = "";
        protected System.String _ColumnUniqueName = "";
        protected System.Int32 _ColumnNameLabelID = 0;
        protected System.DateTime _DateAdded = DateTime.Now;
        protected System.DateTime _DateRevised = DateTime.Now;
        protected System.String _HostName = "";
        protected System.Boolean _IsActive = false;
        protected System.String _RevisedBy = "";




        public bool IsAddedByNull = true;


        public bool IsColumnDescriptionNull = true;


        public bool IsColumnIDNull = true;


        public bool IsColumnNameNull = true;


        public bool IsColumnNameLabelIDNull = true;


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

        [XmlElement(ElementName = "ColumnDescription")]
        public virtual System.String ColumnDescription
        {
            get
            {
                return this._ColumnDescription;
            }
            set
            {
                this._ColumnDescription = value;

                this.IsColumnDescriptionNull = (_ColumnDescription == null);
            }
        }

        [XmlElement(ElementName = "ColumnID")]
        public virtual System.Int16 ColumnID
        {
            get
            {
                return this._ColumnID;
            }
            set
            {
                this._ColumnID = value;

                this.IsColumnIDNull = false;
            }
        }

        [XmlElement(ElementName = "ColumnName")]
        public virtual System.String ColumnName
        {
            get
            {
                return this._ColumnName;
            }
            set
            {
                this._ColumnName = value;

                this.IsColumnNameNull = (_ColumnName == null);
            }
        }

        [XmlElement(ElementName = "ColumnUniqueName")]
        public virtual System.String ColumnUniqueName
        {
            get
            {
                return this._ColumnUniqueName;
            }
            set
            {
                this._ColumnUniqueName = value;
            }
        }

        [XmlElement(ElementName = "ColumnNameLabelID")]
        public virtual System.Int32 ColumnNameLabelID
        {
            get
            {
                return this._ColumnNameLabelID;
            }
            set
            {
                this._ColumnNameLabelID = value;

                this.IsColumnNameLabelIDNull = false;
            }
        }

        [XmlElement(ElementName = "DateAdded")]
        public virtual System.DateTime DateAdded
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
        public virtual System.DateTime DateRevised
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
        public virtual System.Boolean IsActive
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
