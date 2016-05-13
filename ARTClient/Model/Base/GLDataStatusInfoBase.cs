

using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using Adapdev.Text;

namespace SkyStem.ART.Client.Model.Base
{

    /// <summary>
    /// An object representation of the SkyStemArt GLDataStatus table
    /// </summary>
    [Serializable]
    public abstract class GLDataStatusInfoBase : MultilingualInfo
    {

        protected System.Int64? _GLDataID = 0;
        protected System.Int64? _GLDataStatusID = 0;
        protected System.DateTime? _StatusDate = DateTime.Now;
        protected System.Int16? _StatusID = 0;
        protected System.Int16? _StatusTypeID = 0;

        public bool IsGLDataIDNull = true;


        public bool IsGLDataStatusIDNull = true;


        public bool IsStatusDateNull = true;


        public bool IsStatusIDNull = true;


        public bool IsStatusTypeIDNull = true;

        [XmlElement(ElementName = "GLDataID")]
        public virtual System.Int64? GLDataID
        {
            get
            {
                return this._GLDataID;
            }
            set
            {
                this._GLDataID = value;

                this.IsGLDataIDNull = false;
            }
        }

        [XmlElement(ElementName = "GLDataStatusID")]
        public virtual System.Int64? GLDataStatusID
        {
            get
            {
                return this._GLDataStatusID;
            }
            set
            {
                this._GLDataStatusID = value;

                this.IsGLDataStatusIDNull = false;
            }
        }

        [XmlElement(ElementName = "StatusDate")]
        public virtual System.DateTime? StatusDate
        {
            get
            {
                return this._StatusDate;
            }
            set
            {
                this._StatusDate = value;

                this.IsStatusDateNull = (_StatusDate == DateTime.MinValue);
            }
        }

        [XmlElement(ElementName = "StatusID")]
        public virtual System.Int16? StatusID
        {
            get
            {
                return this._StatusID;
            }
            set
            {
                this._StatusID = value;

                this.IsStatusIDNull = false;
            }
        }

        [XmlElement(ElementName = "StatusTypeID")]
        public virtual System.Int16? StatusTypeID
        {
            get
            {
                return this._StatusTypeID;
            }
            set
            {
                this._StatusTypeID = value;

                this.IsStatusTypeIDNull = false;
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
