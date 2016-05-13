

using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using Adapdev.Text;

namespace SkyStem.ART.Client.Model.Base
{

    /// <summary>
    /// An object representation of the SkyStemArt GLDataReviewNoteHdr table
    /// </summary>
    [Serializable]
    public abstract class GLDataReviewNoteHdrInfoBase
    {

        protected System.String _AddedBy = "";
        protected System.Int32? _AddedByUserID = 0;
        protected System.DateTime? _DateAdded = DateTime.Now;
        protected System.DateTime? _DateRevised = DateTime.Now;
        protected System.Boolean? _DeleteAfterCertification = false;
        protected System.Int64? _GLDataID = 0;
        protected System.Int64? _GLDataReviewNoteID = 0;
        protected System.Boolean? _IsActive = false;
        protected System.String _ReviewNoteSubject = "";
        protected System.String _RevisedBy = "";
        protected System.Int32? _RevisedByUserID = null;
        public bool IsAddedByNull = true;
        public bool IsAddedByUserIDNull = true;
        public bool IsDateAddedNull = true;
        public bool IsDateRevisedNull = true;
        public bool IsDeleteAfterCertificationNull = true;
        public bool IsGLDataIDNull = true;
        public bool IsGLDataReviewNoteIDNull = true;
        public bool IsIsActiveNull = true;
        public bool IsReviewNoteSubjectNull = true;
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

        [XmlElement(ElementName = "DeleteAfterCertification")]
        public virtual System.Boolean? DeleteAfterCertification
        {
            get
            {
                return this._DeleteAfterCertification;
            }
            set
            {
                this._DeleteAfterCertification = value;

                this.IsDeleteAfterCertificationNull = false;
            }
        }

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

        [XmlElement(ElementName = "ReviewNoteSubject")]
        public virtual System.String ReviewNoteSubject
        {
            get
            {
                return this._ReviewNoteSubject;
            }
            set
            {
                this._ReviewNoteSubject = value;

                this.IsReviewNoteSubjectNull = (_ReviewNoteSubject == null);
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

        [XmlElement(ElementName = "_RevisedByUserID")]
        public virtual System.Int32? RevisedByUserID
        {
            get
            {
                return this._RevisedByUserID;
            }
            set
            {
                this._RevisedByUserID = value;
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
