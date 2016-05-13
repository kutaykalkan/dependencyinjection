
using System;
using Adapdev.Text;
using SkyStem.ART.Client.Model.Base;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using System.IO;
using System.Xml;
using System.Collections.Generic;
using SkyStem.ART.Client.Data;

namespace SkyStem.ART.Client.Model
{

    /// <summary>
    /// An object representation of the SkyStemArt Attachment table
    /// </summary>
    [Serializable]
    [DataContract]
    public class AttachmentInfo : AttachmentInfoBase
    {

        protected System.String _UserFullName = "";
        public bool IsUserFullNameNull = true;
        [XmlElement(ElementName = "UserFullName")]
        public virtual System.String UserFullName
        {
            get
            {
                return this._UserFullName;
            }
            set
            {
                this._UserFullName = value;

                this.IsUserFullNameNull = (_UserFullName == null);
            }
        }


        protected long? _PreviousAttachmentID = 0;
        public bool IsPreviousAttachmentIDNull = true;
        [XmlElement(ElementName = "PreviousAttachmentID")]
        public virtual long? PreviousAttachmentID
        {
            get
            {
                return this._PreviousAttachmentID;
            }
            set
            {
                this._PreviousAttachmentID = value;
                this.IsPreviousAttachmentIDNull = (_PreviousAttachmentID == null);

            }
        }


        protected long? _OriginalAttachmentID = 0;
        public bool IsOriginalAttachmentIDNull = true;
        [XmlElement(ElementName = "OriginalAttachmentID")]
        public virtual long? OriginalAttachmentID
        {
            get
            {
                return this._OriginalAttachmentID;
            }
            set
            {
                this._OriginalAttachmentID = value;
                this.IsOriginalAttachmentIDNull = (_OriginalAttachmentID == null);

            }
        }

        protected System.Boolean? _isActive;
        public bool IsIsActiveNull = true;
        public bool? IsActive
        {
            get
            {
                return _isActive;
            }
            set
            {
                _isActive = value;
                this.IsIsActiveNull = (this._isActive == null);
            }
        }
        public int? RecPeriodID
        {
            get;
            set;
        }

        public int RowNumber { get; set; }

        public short? FileType
        {
            get
            {
                if (!IsPermanentOrTemporary.HasValue)
                    return null;
                else if (IsPermanentOrTemporary.Value)
                        return (short)ARTEnums.FileType.Permanent;
                return (short)ARTEnums.FileType.Temporary;
            }
            set 
            {
                if (value == (short)ARTEnums.FileType.Permanent)
                    IsPermanentOrTemporary = true;
                else if (value == (short)ARTEnums.FileType.Temporary)
                    IsPermanentOrTemporary = false;
                else
                    IsPermanentOrTemporary = null;
            }
        }

    }//end of class
}//end of namespace
