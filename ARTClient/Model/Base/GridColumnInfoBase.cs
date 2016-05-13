

using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using Adapdev.Text;

namespace SkyStem.ART.Client.Model.Base
{

    /// <summary>
    /// An object representation of the SkyStemArt GridColumn table
    /// </summary>
    [Serializable]
    public abstract class GridColumnInfoBase : MultilingualInfo
    {
        protected System.Int16? _ColumnID = 0;
        protected System.Int16? _GridColumnID = 0;
        protected System.Int16? _GridID = 0;
        protected System.Boolean? _IsPartOfDefaultView = false;

        public bool IsColumnIDNull = true;


        public bool IsColumnLabelIDNull = true;


        public bool IsGridColumnIDNull = true;


        public bool IsGridIDNull = true;


        public bool IsIsPartOfDefaultViewNull = true;

        [XmlElement(ElementName = "ColumnID")]
        public virtual System.Int16? ColumnID
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

        [XmlElement(ElementName = "ColumnLabelID")]
        public virtual System.Int32? ColumnLabelID
        {
            get
            {
                return this.LabelID;
            }
            set
            {
                this.LabelID = value;
                this.IsColumnLabelIDNull = false;
            }
        }

        [XmlElement(ElementName = "GridColumnID")]
        public virtual System.Int16? GridColumnID
        {
            get
            {
                return this._GridColumnID;
            }
            set
            {
                this._GridColumnID = value;

                this.IsGridColumnIDNull = false;
            }
        }

        [XmlElement(ElementName = "GridID")]
        public virtual System.Int16? GridID
        {
            get
            {
                return this._GridID;
            }
            set
            {
                this._GridID = value;

                this.IsGridIDNull = false;
            }
        }

        [XmlElement(ElementName = "IsPartOfDefaultView")]
        public virtual System.Boolean? IsPartOfDefaultView
        {
            get
            {
                return this._IsPartOfDefaultView;
            }
            set
            {
                this._IsPartOfDefaultView = value;

                this.IsIsPartOfDefaultViewNull = false;
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
