
using System;
using Adapdev.Text;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using SkyStem.ART.Client.Model.Base;

namespace SkyStem.ART.Client.Model
{

    /// <summary>
    /// An object representation of the SkyStemART TaskDetail table
    /// </summary>
    [Serializable]
    [DataContract]
    public class TaskDetailInfo : TaskDetailInfoBase
    {
        protected System.String _TaskListName = null;
        protected System.Int32? _CreatedRecPeriodID = null;
        protected System.Int16? _TaskTypeID = null;
        protected System.Int32? _AssigneeID = null;
        protected System.Int32? _ApproverID = null;

        [DataMember]
        [XmlElement(ElementName = "TaskListName")]
        public virtual System.String TaskListName
        {
            get { return this._TaskListName; }
            set { this._TaskListName = value; }
        }


        [DataMember]
        [XmlElement(ElementName = "CreatedRecPeriodID")]
        public virtual System.Int32? CreatedRecPeriodID
        {
            get { return this._CreatedRecPeriodID; }
            set { this._CreatedRecPeriodID = value; }
        }

        [DataMember]
        [XmlElement(ElementName = "TaskTypeID")]
        public virtual System.Int16? TaskTypeID
        {
            get { return this._TaskTypeID; }
            set { this._TaskTypeID = value; }
        }
        [DataMember]
        [XmlElement(ElementName = "AssigneeID")]
        public virtual System.Int32? AssigneeID
        {
            get { return this._AssigneeID; }
            set { this._AssigneeID = value; }
        }
        [DataMember]
        [XmlElement(ElementName = "ApproverID")]
        public virtual System.Int32? ApproverID
        {
            get { return this._ApproverID; }
            set { this._ApproverID = value; }
        }
    }
}
