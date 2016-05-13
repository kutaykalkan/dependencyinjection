
using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using Adapdev.Text;
using System.Runtime.Serialization;

namespace SkyStem.ART.Client.Model.Base
{
    /// <summary>
    /// An object representation of the SkyStemART TaskTypeMst table
    /// </summary>
    [DataContract]
    [Serializable]
    public abstract class TaskCategoryMstInfoBase : MultilingualInfo
    {
        protected System.String _AddedBy = null;
        protected System.DateTime? _DateAdded = null;
        protected System.DateTime? _DateRevised = null;
        protected System.String _HostName = null;
        protected System.Boolean? _IsActive = null;
        protected System.String _RevisedBy = null;

        protected System.String _TaskCategoryName = null;
        protected System.Int16? _TaskCategoryID = null;
        protected System.Int32? _TaskCategoryNameLabelID = null;

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
        [XmlElement(ElementName = "DateRevised")]
        public virtual System.DateTime? DateRevised
        {
            get { return this._DateRevised; }
            set { this._DateRevised = value; }
        }
        [DataMember]
        [XmlElement(ElementName = "HostName")]
        public virtual System.String HostName
        {
            get { return this._HostName; }
            set { this._HostName = value; }
        }
        [DataMember]
        [XmlElement(ElementName = "IsActive")]
        public virtual System.Boolean? IsActive
        {
            get { return this._IsActive; }
            set { this._IsActive = value; }
        }
        [DataMember]
        [XmlElement(ElementName = "RevisedBy")]
        public virtual System.String RevisedBy
        {
            get { return this._RevisedBy; }
            set { this._RevisedBy = value; }
        }

        [DataMember]
        [XmlElement(ElementName = "TaskCategoryName")]
        public virtual System.String TaskCategoryName
        {
            get { return this._TaskCategoryName; }
            set { this._TaskCategoryName = value; }
        }
        [DataMember]
        [XmlElement(ElementName = "TaskCategoryID")]
        public virtual System.Int16? TaskCategoryID
        {
            get { return this._TaskCategoryID; }
            set { this._TaskCategoryID = value; }
        }
        [DataMember]
        [XmlElement(ElementName = "TaskCategoryNameLabelID")]
        public virtual System.Int32? TaskCategoryNameLabelID
        {
            get { return this._TaskCategoryNameLabelID; }
            set { this._TaskCategoryNameLabelID = value; }
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
