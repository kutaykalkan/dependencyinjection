

using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using Adapdev.Text;

namespace SkyStem.ART.Client.Model.Base
{

    /// <summary>
    /// An object representation of the SkyStemArt RecItemCommentHdr table
    /// </summary>
    [Serializable]
    public abstract class RecItemCommentInfoBase
    {
        protected System.String _AddedBy = "";
        protected System.Int32? _AddedByUserID = 0;
        protected System.Int32? _AddedByUserRoleID = 0;
        protected System.DateTime? _DateAdded = DateTime.Now;
        protected System.Int64? _RecItemCommentID = 0;
        protected System.Int64? _RecItemID = 0;
        protected System.Int16? _RecordTypeID = 0;
        protected System.Boolean? _IsActive = false;
        protected System.String _Comment = "";

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

        [XmlElement(ElementName = "AddedByUserRoleID")]
        public virtual System.Int32? AddedByUserRoleID
        {
            get
            {
                return this._AddedByUserRoleID;
            }
            set
            {
                this._AddedByUserRoleID = value;
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

        [XmlElement(ElementName = "RecItemCommentID")]
        public virtual System.Int64? RecItemCommentID
        {
            get
            {
                return this._RecItemCommentID;
            }
            set
            {
                this._RecItemCommentID = value;
            }
        }

        [XmlElement(ElementName = "RecItemID")]
        public virtual System.Int64? RecItemID
        {
            get
            {
                return this._RecItemID;
            }
            set
            {
                this._RecItemID = value;
            }
        }
        [XmlElement(ElementName = "RecordTypeID")]
        public virtual System.Int16? RecordTypeID
        {
            get
            {
                return this._RecordTypeID;
            }
            set
            {
                this._RecordTypeID = value;
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

        [XmlElement(ElementName = "Comment")]
        public virtual System.String Comment
        {
            get
            {
                return this._Comment;
            }
            set
            {
                this._Comment = value;
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
