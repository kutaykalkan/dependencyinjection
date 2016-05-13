using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SkyStem.ART.Client.Model.Base;
using System.Xml;
using System.Xml.Serialization;
using Adapdev.Text;
using System.Runtime.Serialization;

namespace SkyStem.ART.Client.Model.Report
{
    [Serializable]
    public class TaskCompletionReportInfo : OrganizationalHierarchyInfo
    {
        #region "Task Attributes"
        protected System.Int64? _TaskID = null;
        private string _TaskName;
        private TaskListHdrInfo _TaskList;
        private string _TaskDescription;
        private UserHdrInfo _AssignedTo;
        private UserHdrInfo _Approver;
        private short? _TaskStatusID;
        private DateTime? _TaskStatusDate = null;
        private int? _TaskStatusLabelID;
        private string _TaskStatus;
        protected System.String _TaskNumber = null;
        protected System.Int16? _TaskTypeID = null;
        private long? _TaskDetailID = null;
        private int? _TaskDetailRecPeriodID = null;
        private UserHdrInfo _TaskDetailAddedByUser;
        private UserHdrInfo _TaskDetailDoneByUser;
        private UserHdrInfo _TaskDetailApprovedByUser;
        private long? _AccountID;
        private int? _AccountNameLabelID;
        private string _AccountName;
        private string _AccountNumber;
        private DateTime? _DateAdded = null;
        private DateTime? _DateDone = null;
        private DateTime? _DateApproved = null;
        


        [DataMember]
        [XmlElement(ElementName = "TaskID")]
        public virtual System.Int64? TaskID
        {
            get { return this._TaskID; }
            set { this._TaskID = value; }
        }
			
        [DataMember]
        [XmlElement(ElementName = "TaskName")]
        public string TaskName
        {
            get
            {
                return this._TaskName;
            }
            set
            {
                this._TaskName = value;
            }
        }

        [DataMember]
        [XmlElement(ElementName = "TaskList")]
        public TaskListHdrInfo TaskList
        {
            get
            {
                return this._TaskList;
            }
            set
            {
                this._TaskList = value;
            }
        }

        [DataMember]
        [XmlElement(ElementName = "TaskDescription")]
        public string TaskDescription
        {
            get
            {
                return this._TaskDescription;
            }
            set
            {
                this._TaskDescription = value;
            }
        }

        //[DataMember]
        //[XmlElement(ElementName = "AssignedTo")]
        //public UserHdrInfo AssignedTo
        //{
        //    get
        //    {
        //        return this._AssignedTo;
        //    }
        //    set
        //    {
        //        this._AssignedTo = value;
        //    }
        //}

        //[DataMember]
        //[XmlElement(ElementName = "Approver")]
        //public UserHdrInfo Approver
        //{
        //    get { return _Approver; }
        //    set { _Approver = value; }
        //}
     
        [DataMember]
        [XmlElement(ElementName = "TaskStatusLabelID")]
        public int? TaskStatusLabelID
        {
            get
            {
                return this._TaskStatusLabelID;
            }
            set
            {
                this._TaskStatusLabelID = value;
            }
        }


        [DataMember]
        [XmlElement(ElementName = "TaskStatus")]
        public string TaskStatus
        {
            get
            {
                return this._TaskStatus;
            }
            set
            {
                this._TaskStatus = value;
            }
        }
        [DataMember]
        [XmlElement(ElementName = "TaskStatusID")]
        public virtual short? TaskStatusID
        {
            get
            {
                return _TaskStatusID;
            }
            set
            {
                this._TaskStatusID = value;
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
        [XmlElement(ElementName = "TaskTypeID")]
        public virtual System.Int16? TaskTypeID
        {
            get { return this._TaskTypeID; }
            set { this._TaskTypeID = value; }
        }
        [DataMember]
        [XmlElement(ElementName = "TaskDetailID")]
        public virtual long? TaskDetailID
        {
            get
            {
                return _TaskDetailID;
            }
            set
            {
                this._TaskDetailID = value;
            }
        }

        [DataMember]
        [XmlElement(ElementName = "TaskDetailRecPeriodID")]
        public virtual int? TaskDetailRecPeriodID
        {
            get
            {
                return this._TaskDetailRecPeriodID;
            }
            set
            {
                this._TaskDetailRecPeriodID = value;
            }

        }
        [DataMember]
        [XmlElement(ElementName = "TaskDetailAddedByUser")]
        public UserHdrInfo TaskDetailAddedByUser
        {
            get
            {
                return this._TaskDetailAddedByUser;

            }
            set
            {
                this._TaskDetailAddedByUser = value;
            }
        }
        [DataMember]
        [XmlElement(ElementName = "TaskDetailDoneByUser")]
        public UserHdrInfo TaskDetailDoneByUser
        {
            get
            {
                return this._TaskDetailDoneByUser;

            }
            set
            {
                this._TaskDetailDoneByUser = value;
            }
        }
        [DataMember]
        [XmlElement(ElementName = "TaskDetailApprovedByUser")]
        public UserHdrInfo TaskDetailApprovedByUser
        {
            get
            {
                return this._TaskDetailApprovedByUser;

            }
            set
            {
                this._TaskDetailApprovedByUser = value;
            }
        }

        [DataMember]
        [XmlElement(ElementName = "TaskDetailReviewedByUser")]
        public UserHdrInfo TaskDetailReviewedByUser
        {
            get;
            set;
        }

        [DataMember]
        [XmlElement(ElementName = "AccountID")]
        public long? AccountID
        {
            get
            {
                return this._AccountID;
            }
            set
            {
                this._AccountID = value;
            }
        }

        [DataMember]
        [XmlElement(ElementName = "AccountNameLabelID")]
        public int? AccountNameLabelID
        {
            get
            {
                return this._AccountNameLabelID;
            }
            set
            {
                this._AccountNameLabelID = value;
            }
        }

        [DataMember]
        [XmlElement(ElementName = "AccountName")]
        public string AccountName
        {
            get
            {
                return this._AccountName;
            }
            set
            {
                this._AccountName = value;
            }
        }
        [DataMember]
        [XmlElement(ElementName = "AccountNumber")]
        public string AccountNumber
        {
            get
            {
                return this._AccountNumber;
            }
            set
            {
                this._AccountNumber = value;
            }
        }
        [DataMember]
        [XmlElement(ElementName = "TaskStatusDate")]
        public virtual DateTime? TaskStatusDate
        {
            get
            {
                return _TaskStatusDate;
            }
            set
            {
                this._TaskStatusDate = value;
            }
        }
        [DataMember]
        [XmlElement(ElementName = "DateAdded")]
        public virtual DateTime? DateAdded
        {
            get
            {
                return _DateAdded;
            }
            set
            {
                this._DateAdded = value;
            }
        }
        [DataMember]
        [XmlElement(ElementName = "DateDone")]
        public virtual DateTime? DateDone
        {
            get
            {
                return _DateDone;
            }
            set
            {
                this._DateDone = value;
            }
        }
        [DataMember]
        [XmlElement(ElementName = "DateApproved")]
        public virtual DateTime? DateApproved
        {
            get
            {
                return _DateApproved;
            }
            set
            {
                this._DateApproved = value;
            }
        }

        [DataMember]
        [XmlElement(ElementName = "DateReviewed")]
        public virtual DateTime? DateReviewed
        {
            get;
            set;
        }

        
        [DataMember]
        [XmlElement(ElementName = "TaskOwner")]
        public string TaskOwner { get; set; }
        [DataMember]
        [XmlElement(ElementName = "TaskReviewer")]
        public string TaskReviewer { get; set; }
        [DataMember]
        [XmlElement(ElementName = "TaskApprover")]
        public string TaskApprover { get; set; }

        [DataMember]
        [XmlElement(ElementName = "CustomField1")]
        public string CustomField1 { get; set; }

        [DataMember]
        [XmlElement(ElementName = "CustomField2")]
        public string CustomField2 { get; set; }


     
        #endregion
    }
}
