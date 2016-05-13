
using System;
using Adapdev.Text;
using System.Runtime.Serialization;
using SkyStem.ART.Client.Model.Base;
using System.Xml.Serialization;

namespace SkyStem.ART.Client.Model
{    

	/// <summary>
	/// An object representation of the SkyStemART TaskAttributeValue table
	/// </summary>
	[Serializable]
	[DataContract]
	public class TaskAttributeValueInfo : TaskAttributeValueInfoBase
	{
        protected System.Int16? _TaskAttributeID = null;
        protected System.String _AssigneeUserName = null;
        protected System.String _ReviewerUserName = null;
        protected System.String _ApproverUserName = null;        
        protected System.String _NotifyUserName = null;
        protected System.Boolean? _IsTaskCompleted = null;
        private System.Int64? _TaskID = null;
        protected DateTime? _TaskRecPeriodEndDate;
        protected System.String _TaskNumber = null;
        protected System.String _TaskAddedBy = null;
        protected System.DateTime? _DateCreated = null;


        
        [DataMember]
        [XmlElement(ElementName = "TaskAttributeID")]
        public virtual System.Int16? TaskAttributeID
        {
            get { return this._TaskAttributeID; }
            set { this._TaskAttributeID = value; }
        }
        [DataMember]
        [XmlElement(ElementName = "AssigneeUserName")]
        public virtual System.String AssigneeUserName
        {
            get { return this._AssigneeUserName; }
            set { this._AssigneeUserName = value; }
        }
        [DataMember]
        [XmlElement(ElementName = "ReviewerUserName")]
        public virtual System.String ReviewerUserName
        {
            get { return this._ReviewerUserName; }
            set { this._ReviewerUserName = value; }
        }
        [DataMember]
        [XmlElement(ElementName = "ApproverUserName")]
        public virtual System.String ApproverUserName
        {
            get { return this._ApproverUserName; }
            set { this._ApproverUserName = value; }
        }
        [DataMember]
        [XmlElement(ElementName = "NotifyUserName")]
        public virtual System.String NotifyUserName
        {
            get { return this._NotifyUserName; }
            set { this._NotifyUserName = value; }
        }
        [XmlElement(ElementName = "IsTaskCompleted")]
        public virtual System.Boolean? IsTaskCompleted
        {
            get
            {
                return this._IsTaskCompleted;
            }
            set
            {
                this._IsTaskCompleted = value;

            }
        }

        [DataMember]
        [XmlElement(ElementName = "TaskID")]
        public System.Int64? TaskID
        {
            get { return _TaskID; }
            set { _TaskID = value; }
        }

	
        [DataMember]
        [XmlElement(ElementName = "TaskRecPeriodEndDate")]
        public DateTime? TaskRecPeriodEndDate
        {
            get
            {
                return this._TaskRecPeriodEndDate;
            }
            set
            {
                this._TaskRecPeriodEndDate = value;
            }
        }
        [DataMember]
        [XmlElement(ElementName = "TaskNumber")]
        public virtual System.String TaskNumber
        {
            get { return this._TaskNumber; }
            set { this._TaskNumber = value; }
        }
        [DataMember]
        [XmlElement(ElementName = "TaskAddedBy")]
        public virtual System.String TaskAddedBy
        {
            get { return this._TaskAddedBy; }
            set { this._TaskAddedBy = value; }
        }
        [DataMember]
        [XmlElement(ElementName = "DateCreated")]
        public virtual System.DateTime? DateCreated
        {
            get { return this._DateCreated; }
            set { this._DateCreated = value; }
        }			
	}
}
