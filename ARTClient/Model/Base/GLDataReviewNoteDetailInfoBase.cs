

using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using Adapdev.Text;

namespace SkyStem.ART.Client.Model.Base
{

    /// <summary>
    /// An object representation of the SkyStemArt GLDataReviewNoteDetail table
    /// </summary>
    [Serializable]
    public abstract class GLDataReviewNoteDetailInfoBase
    {
        protected System.String _AddedBy = "";
        protected System.Int32? _AddedByUserID = 0;
        protected System.DateTime? _DateAdded = DateTime.Now;
        protected System.Int64? _GLDataReviewNoteDetailID = 0;
        protected System.Int64? _GLDataReviewNoteID = 0;
        protected System.Boolean? _IsActive = false;
        protected System.String _ReviewNote = "";
        public bool IsAddedByNull = true;
        public bool IsAddedByUserIDNull = true;
        public bool IsDateAddedNull = true;
        public bool IsGLDataReviewNoteDetailIDNull = true;
        public bool IsGLDataReviewNoteIDNull = true;
        public bool IsIsActiveNull = true;
        public bool IsReviewNoteNull = true;

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

                this.IsAddedByUserIDNull = false;
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

        [XmlElement(ElementName = "GLDataReviewNoteDetailID")]
        public virtual System.Int64? GLDataReviewNoteDetailID
        {
            get
            {
                return this._GLDataReviewNoteDetailID;
            }
            set
            {
                this._GLDataReviewNoteDetailID = value;

                this.IsGLDataReviewNoteDetailIDNull = false;
            }
        }

        [XmlElement(ElementName = "GLDataReviewNoteID")]
        public virtual System.Int64? GLDataReviewNoteID
        {
            get
            {
                return this._GLDataReviewNoteID;
            }
            set
            {
                this._GLDataReviewNoteID = value;

                this.IsGLDataReviewNoteIDNull = false;
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

        [XmlElement(ElementName = "ReviewNote")]
        public virtual System.String ReviewNote
        {
            get
            {
                return this._ReviewNote;
            }
            set
            {
                this._ReviewNote = value;

                this.IsReviewNoteNull = (_ReviewNote == null);
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
