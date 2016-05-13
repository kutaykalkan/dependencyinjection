using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Adapdev.Text;
using System.Runtime.Serialization;

namespace SkyStem.ART.Client.Model.Base
{
    [Serializable]
    public abstract class AutoSaveAttributeValueInfoBase
    {
        protected System.Int64? _AutoSaveAttributeValueID = null;
        protected System.Int32? _AutoSaveAttributeID = null;
        protected System.Int32? _UserID = null;
        protected System.Int16? _RoleID = null;
        protected System.String _Value = null;
        protected System.Int32? _ReferenceID = null;
        protected System.Boolean? _IsActive = null;
        protected System.String _AddedBy = null;
        protected System.DateTime? _DateAdded = null;
        protected System.String _RevisedBy = null;
        protected System.DateTime? _DateRevised = null;

        [DataMember]
        [XmlElement(ElementName = "AutoSaveAttributeValueID")]
        public virtual System.Int64? AutoSaveAttributeValueID
        {
            get { return this._AutoSaveAttributeValueID; }
            set { this._AutoSaveAttributeValueID = value; }
        }
        [DataMember]
        [XmlElement(ElementName = "AutoSaveAttributeID")]
        public virtual System.Int32? AutoSaveAttributeID
        {
            get { return this._AutoSaveAttributeID; }
            set { this._AutoSaveAttributeID = value; }
        }
        [DataMember]
        [XmlElement(ElementName = "UserID")]
        public virtual System.Int32? UserID
        {
            get { return this._UserID; }
            set { this._UserID = value; }
        }
        [DataMember]
        [XmlElement(ElementName = "RoleID")]
        public virtual System.Int16? RoleID
        {
            get { return this._RoleID; }
            set { this._RoleID = value; }
        }
        [DataMember]
        [XmlElement(ElementName = "ReferenceID")]
        public virtual System.Int32? ReferenceID
        {
            get { return this._ReferenceID; }
            set { this._ReferenceID = value; }
        }

        [DataMember]
        [XmlElement(ElementName = "Value")]
        public virtual System.String Value
        {
            get { return this._Value; }
            set { this._Value = value; }
        }

        [DataMember]
        [XmlElement(ElementName = "IsActive")]
        public virtual System.Boolean? IsActive
        {
            get { return this._IsActive; }
            set { this._IsActive = value; }
        }
        [DataMember]
        [XmlElement(ElementName = "AddedBy")]
        public virtual System.String AddedBy
        {
            get { return this._AddedBy; }
            set { this._AddedBy = value; }
        }
        [DataMember]
        [XmlElement(ElementName = "DateAdded")]
        public virtual System.DateTime? DateAdded
        {
            get { return this._DateAdded; }
            set { this._DateAdded = value; }
        }

        [DataMember]
        [XmlElement(ElementName = "RevisedBy")]
        public virtual System.String RevisedBy
        {
            get { return this._RevisedBy; }
            set { this._RevisedBy = value; }
        }
        [DataMember]
        [XmlElement(ElementName = "DateRevised")]
        public virtual System.DateTime? DateRevised
        {
            get { return this._DateRevised; }
            set { this._DateRevised = value; }
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
