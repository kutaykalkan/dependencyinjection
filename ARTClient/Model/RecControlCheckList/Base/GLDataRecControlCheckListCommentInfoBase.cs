using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using Adapdev.Text;
using System.Runtime.Serialization;


namespace SkyStem.ART.Client.Model.RecControlCheckList.Base
{
    /// <summary>
    /// An object representation of the SkyStemART GLDataRecControlCheckListComment table
    /// </summary>
    [DataContract]
    [Serializable]
    public abstract class GLDataRecControlCheckListCommentInfoBase
    {
        protected Int64? _GLDataRecControlCheckListCommentID = null;
        protected Int64? _GLDataRecControlCheckListID = null;
        protected System.String _Comments = null;       
        protected System.Boolean? _IsActive = null;
        protected Int32? _AddedByUserID = null;
        protected Int16? _RoleID = null;
        protected System.String _AddedBy = null;
        protected System.DateTime? _DateAdded = null;
        protected System.String _RevisedBy = null;
        protected System.DateTime? _DateRevised = null;

        [DataMember]
        [XmlElement(ElementName = "GLDataRecControlCheckListCommentID")]
        public virtual System.Int64? GLDataRecControlCheckListCommentID
        {
            get { return this._GLDataRecControlCheckListCommentID; }
            set { this._GLDataRecControlCheckListCommentID = value; }
        }
        [DataMember]
        [XmlElement(ElementName = "GLDataRecControlCheckListID")]
        public virtual System.Int64? GLDataRecControlCheckListID
        {
            get { return this._GLDataRecControlCheckListID; }
            set { this._GLDataRecControlCheckListID = value; }
        }
        [DataMember]
        [XmlElement(ElementName = "Comments")]
        public virtual System.String Comments
        {
            get { return this._Comments; }
            set { this._Comments = value; }
        }       
        [DataMember]
        [XmlElement(ElementName = "AddedByUserID")]
        public virtual System.Int32? AddedByUserID
        {
            get { return this._AddedByUserID; }
            set { this._AddedByUserID = value; }
        }
        [DataMember]
        [XmlElement(ElementName = "RoleID")]
        public virtual System.Int16? RoleID
        {
            get { return this._RoleID; }
            set { this._RoleID = value; }
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
