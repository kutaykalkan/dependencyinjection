using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using Adapdev.Text;
using System.Runtime.Serialization;

namespace SkyStem.ART.Client.Model.RecControlCheckList.Base
{
    /// <summary>
    /// An object representation of the SkyStemART GLDataRecControlCheckList table
    /// </summary>
    [DataContract]
    [Serializable]
    public abstract class GLDataRecControlCheckListInfoBase
    {
        protected Int64? _GLDataRecControlCheckListID = null;
        protected Int32? _RecControlCheckListID = null;
        protected Int64? _GLDataID = null;
        protected System.Int16? _CompletedRecStatus = null;
        protected System.Int16? _ReviewedRecStatus = null;
        protected Int32? _CompletedBy = null;
        protected System.DateTime? _DateCompleted = null;
        protected Int32? _ReviewedBy = null;
        protected System.DateTime? _DateReviewed = null;
        protected System.Boolean? _IsActive = null;
        protected System.String _AddedBy = null;
        protected System.DateTime? _DateAdded = null;
        protected System.String _RevisedBy = null;
        protected System.DateTime? _DateRevised = null;

        [DataMember]
        [XmlElement(ElementName = "GLDataRecControlCheckListID")]
        public virtual System.Int64? GLDataRecControlCheckListID
        {
            get { return this._GLDataRecControlCheckListID; }
            set { this._GLDataRecControlCheckListID = value; }
        }
        [DataMember]
        [XmlElement(ElementName = "RecControlCheckListID")]
        public virtual System.Int32? RecControlCheckListID
        {
            get { return this._RecControlCheckListID; }
            set { this._RecControlCheckListID = value; }
        }
        [DataMember]
        [XmlElement(ElementName = "GLDataID")]
        public virtual System.Int64? GLDataID
        {
            get { return this._GLDataID; }
            set { this._GLDataID = value; }
        }
        [DataMember]
        [XmlElement(ElementName = "IsCompleted")]
        public virtual System.Int16? CompletedRecStatus
        {
            get { return this._CompletedRecStatus; }
            set { this._CompletedRecStatus = value; }
        }
        [DataMember]
        [XmlElement(ElementName = "IsReviewed")]
        public virtual System.Int16? ReviewedRecStatus
        {
            get { return this._ReviewedRecStatus; }
            set { this._ReviewedRecStatus = value; }
        }
        [DataMember]
        [XmlElement(ElementName = "CompletedBy")]
        public virtual System.Int32? CompletedBy
        {
            get { return this._CompletedBy; }
            set { this._CompletedBy = value; }
        }
        [DataMember]
        [XmlElement(ElementName = "DateCompleted")]
        public virtual System.DateTime? DateCompleted
        {
            get { return this._DateCompleted; }
            set { this._DateCompleted = value; }
        }
        [DataMember]
        [XmlElement(ElementName = "ReviewedBy")]
        public virtual System.Int32? ReviewedBy
        {
            get { return this._ReviewedBy; }
            set { this._ReviewedBy = value; }
        }
        [DataMember]
        [XmlElement(ElementName = "DateReviewed")]
        public virtual System.DateTime? DateReviewed
        {
            get { return this._DateReviewed; }
            set { this._DateReviewed = value; }
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
    }
}
