

using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using Adapdev.Text;

namespace SkyStem.ART.Client.Model.Base
{

    /// <summary>
    /// An object representation of the SkyStemArt Attachment table
    /// </summary>
    [Serializable]
    public abstract class AttachmentInfoBase
    {


        protected System.Int64? _AttachmentID = 0;
        protected System.String _Comments = "";
        protected System.DateTime? _Date = DateTime.Now;
        protected System.String _DocumentName = "";
        protected System.String _FileName = "";
        protected System.Int64? _FileSize = 0;
        protected System.Boolean? _IsPermanentOrTemporary = null;
        protected System.String _PhysicalPath = "";
        protected System.Int64? _RecordID = 0;
        protected System.Int32? _RecordTypeID = 0;
        protected System.Int32? _UserID = 0;
        protected System.Int32? _StartRecPeriodID = 0;




        public bool IsAttachmentIDNull = true;


        public bool IsCommentsNull = true;


        public bool IsDateNull = true;


        public bool IsDocumentNameNull = true;


        public bool IsFileNameNull = true;


        public bool IsFileSizeNull = true;


        public bool IsIsPermanentOrTemporaryNull = true;


        public bool IsPhysicalPathNull = true;


        public bool IsRecordIDNull = true;


        public bool IsRecordTypeIDNull = true;


        public bool IsUserIDNull = true;

        public bool IsStartRecPeriodIDNull = true;

        [XmlElement(ElementName = "AttachmentID")]
        public virtual System.Int64? AttachmentID
        {
            get
            {
                return this._AttachmentID;
            }
            set
            {
                this._AttachmentID = value;

                this.IsAttachmentIDNull = false;
            }
        }

        [XmlElement(ElementName = "Comments")]
        public virtual System.String Comments
        {
            get
            {
                return this._Comments;
            }
            set
            {
                this._Comments = value;

                this.IsCommentsNull = (_Comments == null);
            }
        }

        [XmlElement(ElementName = "Date")]
        public virtual System.DateTime? Date
        {
            get
            {
                return this._Date;
            }
            set
            {
                this._Date = value;

                this.IsDateNull = (_Date == DateTime.MinValue);
            }
        }

        [XmlElement(ElementName = "DocumentName")]
        public virtual System.String DocumentName
        {
            get
            {
                return this._DocumentName;
            }
            set
            {
                this._DocumentName = value;

                this.IsDocumentNameNull = (_DocumentName == null);
            }
        }

        [XmlElement(ElementName = "FileName")]
        public virtual System.String FileName
        {
            get
            {
                return this._FileName;
            }
            set
            {
                this._FileName = value;

                this.IsFileNameNull = (_FileName == null);
            }
        }

        [XmlElement(ElementName = "FileSize")]
        public virtual System.Int64? FileSize
        {
            get
            {
                return this._FileSize;
            }
            set
            {
                this._FileSize = value;

                this.IsFileSizeNull = false;
            }
        }

        [XmlElement(ElementName = "IsPermanentOrTemporary")]
        public virtual System.Boolean? IsPermanentOrTemporary
        {
            get
            {
                return this._IsPermanentOrTemporary;
            }
            set
            {
                this._IsPermanentOrTemporary = value;

                this.IsIsPermanentOrTemporaryNull = false;
            }
        }

        [XmlElement(ElementName = "PhysicalPath")]
        public virtual System.String PhysicalPath
        {
            get
            {
                return this._PhysicalPath;
            }
            set
            {
                this._PhysicalPath = value;

                this.IsPhysicalPathNull = (_PhysicalPath == null);
            }
        }

        [XmlElement(ElementName = "RecordID")]
        public virtual System.Int64? RecordID
        {
            get
            {
                return this._RecordID;
            }
            set
            {
                this._RecordID = value;

                this.IsRecordIDNull = false;
            }
        }

        [XmlElement(ElementName = "RecordTypeID")]
        public virtual System.Int32? RecordTypeID
        {
            get
            {
                return this._RecordTypeID;
            }
            set
            {
                this._RecordTypeID = value;

                this.IsRecordTypeIDNull = false;
            }
        }

        [XmlElement(ElementName = "UserID")]
        public virtual System.Int32? UserID
        {
            get
            {
                return this._UserID;
            }
            set
            {
                this._UserID = value;

                this.IsUserIDNull = false;
            }
        }


        /// <summary>
        /// Returns a string representation of the object, displaying all property and field names and values.
        /// </summary>
        public override string ToString()
        {
            return StringUtil.ToString(this);
        }

        [XmlElement(ElementName = "StartRecPeriodID")]
        public virtual System.Int32? StartRecPeriodID
        {
            get
            {
                return this._StartRecPeriodID;
            }
            set
            {
                this._StartRecPeriodID = value;

                this.IsStartRecPeriodIDNull = false;
            }
        }


    }
}
