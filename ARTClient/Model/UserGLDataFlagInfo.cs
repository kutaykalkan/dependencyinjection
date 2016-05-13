using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Serialization;

namespace SkyStem.ART.Client.Model
{
    [Serializable]
	[DataContract]
    public class UserGLDataFlagInfo
    {
        private int? _UserID;
        private long? _GLDataID;
        private int? _UserGLDataFlagID;
        private string _AddedBy;
        private DateTime? _DateAdded;
        private string _RevisedBy;
        private DateTime? _DateRevised;
        private bool? _IsActive;

        [XmlElement(ElementName = "UserID")]
        public int? UserID
        {
            get
            {
                return this._UserID;
            }
            set
            {
                this._UserID = value;
            }
        }

        [XmlElement(ElementName = "GLDataID")]
        public long? GLDataID
        {
            get
            {
                return this._GLDataID;
            }
            set
            {
                this._GLDataID = value;
            }
        }

        [XmlElement(ElementName = "UserGLDataFlagID")]
        public int? UserGLDataFlagID
        {
            get
            {
                return this._UserGLDataFlagID;
            }
            set
            {
                this._UserGLDataFlagID = value;
            }
        }

        [XmlElement(ElementName = "AddedBy")]
        public string AddedBy
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

        [XmlElement(ElementName = "DateAdded")]
        public DateTime? DateAdded
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

        [XmlElement(ElementName = "RevisedBy")]
        public string RevisedBy
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

        [XmlElement(ElementName = "DateRevised")]
        public DateTime? DateRevised
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

        [XmlElement(ElementName = "IsActive")]
        public bool? IsActive
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
    }
}
