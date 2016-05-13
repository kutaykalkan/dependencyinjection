
using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using Adapdev.Text;
using System.Runtime.Serialization;

namespace SkyStem.ART.Client.Model.Base
{
    /// <summary>
    /// An object representation of the SkyStemART TaskDetail table
    /// </summary>
    [DataContract]
    [Serializable]
    public abstract class TaskDetailInfoBase : OrganizationalHierarchyInfo
    {

        protected System.Int64? _AccountID = null;
        protected System.String _AddedBy = null;
        protected System.DateTime? _ApprovalDueDate = null;
        protected System.DateTime? _AssigneeDueDate = null;
        protected System.DateTime? _DateAdded = null;
        protected System.DateTime? _DateRevised = null;
        protected System.String _HostName = null;
        protected System.Boolean? _IsActive = null;
        protected System.Int32? _RecPeriodID = null;
        protected System.String _RevisedBy = null;
        protected System.DateTime? _StatusDate = null;
        protected System.Int64? _TaskDetailID = null;
        protected System.DateTime? _TaskDueDate = null;
        protected System.Int64? _TaskID = null;
        protected System.DateTime? _TaskStartDate = null;
        protected System.Int16? _TaskStatusID = null;
        
        [DataMember]
        [XmlElement(ElementName = "AccountID")]
        public virtual System.Int64? AccountID
        {
            get { return this._AccountID; }
            set { this._AccountID = value; }
        }
        [DataMember]
        [XmlElement(ElementName = "AddedBy")]
        public virtual System.String AddedBy
        {
            get { return this._AddedBy; }
            set { this._AddedBy = value; }
        }
        [DataMember]
        [XmlElement(ElementName = "ApprovalDueDate")]
        public virtual System.DateTime? ApprovalDueDate
        {
            get { return this._ApprovalDueDate; }
            set { this._ApprovalDueDate = value; }
        }
        [DataMember]
        [XmlElement(ElementName = "AssigneeDueDate")]
        public virtual System.DateTime? AssigneeDueDate
        {
            get { return this._AssigneeDueDate; }
            set { this._AssigneeDueDate = value; }
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
        [XmlElement(ElementName = "RecPeriodID")]
        public virtual System.Int32? RecPeriodID
        {
            get { return this._RecPeriodID; }
            set { this._RecPeriodID = value; }
        }
        [DataMember]
        [XmlElement(ElementName = "RevisedBy")]
        public virtual System.String RevisedBy
        {
            get { return this._RevisedBy; }
            set { this._RevisedBy = value; }
        }
        [DataMember]
        [XmlElement(ElementName = "StatusDate")]
        public virtual System.DateTime? StatusDate
        {
            get { return this._StatusDate; }
            set { this._StatusDate = value; }
        }
        [DataMember]
        [XmlElement(ElementName = "TaskDetailID")]
        public virtual System.Int64? TaskDetailID
        {
            get { return this._TaskDetailID; }
            set { this._TaskDetailID = value; }
        }
        [DataMember]
        [XmlElement(ElementName = "TaskDueDate")]
        public virtual System.DateTime? TaskDueDate
        {
            get { return this._TaskDueDate; }
            set { this._TaskDueDate = value; }
        }
        [DataMember]
        [XmlElement(ElementName = "TaskID")]
        public virtual System.Int64? TaskID
        {
            get { return this._TaskID; }
            set { this._TaskID = value; }
        }
        [DataMember]
        [XmlElement(ElementName = "TaskStartDate")]
        public virtual System.DateTime? TaskStartDate
        {
            get { return this._TaskStartDate; }
            set { this._TaskStartDate = value; }
        }
        [DataMember]
        [XmlElement(ElementName = "TaskStatusID")]
        public virtual System.Int16? TaskStatusID
        {
            get { return this._TaskStatusID; }
            set { this._TaskStatusID = value; }
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
