
using System;
using Adapdev.Text;
using System.Runtime.Serialization;
using SkyStem.ART.Client.Model.Base;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace SkyStem.ART.Client.Model
{

    /// <summary>
    /// An object representation of the SkyStemART TaskHdr table
    /// </summary>
    [Serializable]
    [DataContract]
    public class TaskHdrInfo : TaskHdrInfoBase
    {
        private int? _TempTaskSequenceNumber = null;
        private int? _TaskDuration;
        private string _TaskApproverLastComment;
        private int? _TaskApprovalDuration;
        private short? _TaskStatusID;
        private DateTime? _TaskStatusDate = null;
        private long? _TaskDetailID = null;
        private int? _TaskDetailRecPeriodID = null;
        private DateTime? _TaskCompletionDate = null;
        private List<AttachmentInfo> _CompletionAttachment;
        private string _Comment;
        private UserHdrInfo _TaskDetailAddedByUser;
        private UserHdrInfo _TaskDetailRevisedByUser;
        private DateTime? _TaskDetailDateRevised;
        private long? _AccountID;
        private int? _AccountNameLabelID;
        private string _AccountName;
        private int? _CreationDocCount;
        private int? _CompletionDocCount;
        private string _AccountNumber;
        protected System.Boolean? _IsDeleted = false;
        protected System.Boolean? _IsHidden = false;
        private DateTime? _TaskRecPeriodEndDate;
        private int? _TaskStatusLabelID;
        private string _TaskStatus;
        //private string _TaskRecurrenceType;
        private Boolean? _IsWorkStarted;

        [DataMember]
        [XmlElement(ElementName = "CompletionDocCount")]
        public int? CompletionDocCount
        {
            get { return this._CompletionDocCount; }
            set { this._CompletionDocCount = value; }

        }

        [DataMember]
        [XmlElement(ElementName = "CreationDocCount")]
        public int? CreationDocCount
        {
            get { return this._CreationDocCount; }
            set { this._CreationDocCount = value; }
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
        [XmlElement(ElementName = "TaskDetailRevisedByUser")]
        public UserHdrInfo TaskDetailRevisedByUser
        {
            get
            {
                return this._TaskDetailRevisedByUser;
            }
            set
            {
                this._TaskDetailRevisedByUser = value;
            }
        }

        [DataMember]
        [XmlElement(ElementName = "TaskDetailDateRevised")]
        public DateTime? TaskDetailDateRevised
        {
            get
            {
                return this._TaskDetailDateRevised;
            }
            set
            {
                this._TaskDetailDateRevised = value;
            }
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
        [XmlElement(ElementName = "TempTaskSequenceNumber")]
        public virtual int? TempTaskSequenceNumber
        {
            get
            {
                return _TempTaskSequenceNumber;
            }
            set
            {
                this._TempTaskSequenceNumber = value;
            }
        }

        [DataMember]
        [XmlElement(ElementName = "TaskDuration")]
        public virtual int? TaskDuration
        {
            get
            {
                return _TaskDuration;
            }
            set
            {
                this._TaskDuration = value;
            }
        }

        [DataMember]
        [XmlElement(ElementName = "TaskApproverLastComment")]
        public string TaskApproverLastComment
        {
            get
            {
                return this._TaskApproverLastComment;
            }
            set
            {
                this._TaskApproverLastComment = value;
            }
        }

        [DataMember]
        [XmlElement(ElementName = "TaskApprovalDuration")]
        public virtual int? TaskApprovalDuration
        {
            get
            {
                return _TaskApprovalDuration;
            }
            set
            {
                this._TaskApprovalDuration = value;
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
        [XmlElement(ElementName = "TaskCompletionDate")]
        public virtual DateTime? TaskCompletionDate
        {
            get
            {
                return _TaskCompletionDate;
            }
            set
            {
                this._TaskCompletionDate = value;
            }
        }

        [DataMember]
        [XmlElement(ElementName = "CompletionAttachment")]
        public List<AttachmentInfo> CompletionAttachment
        {
            get { return _CompletionAttachment; }
            set { _CompletionAttachment = value; }
        }

        [DataMember]
        [XmlElement(ElementName = "ApproverComment")]
        public string Comment
        {
            get
            {
                return this._Comment;
            }
            set
            {
                this._Comment = value;
            }

        }


        private string _TaskListName = null;
        [DataMember]
        public string TaskListName
        {
            get
            {
                this._TaskListName = string.Empty;
                if (this.TaskList != null)
                    this._TaskListName = this.TaskList.TaskListName;

                return this._TaskListName;
            }
        }
        private Int16 _TaskListID;
        [DataMember]
        public Int16 TaskListID
        {
            get
            {

                if (this.TaskList != null)
                    this._TaskListID = this.TaskList.TaskListID.Value;

                return this._TaskListID;
            }
        }
        private string _TaskListAddedBy = null;
        [DataMember]
        public string TaskListAddedBy
        {
            get
            {
                this._TaskListAddedBy = string.Empty;
                if (this.TaskList != null)
                    this._TaskListAddedBy = this.TaskList.AddedBy;

                return this._TaskListAddedBy;
            }
        }
        [DataMember]
        [XmlElement(ElementName = "IsDeleted")]
        public virtual System.Boolean? IsDeleted
        {
            get { return this._IsDeleted; }
            set { this._IsDeleted = value; }
        }

        [DataMember]
        [XmlElement(ElementName = "IsHidden")]
        public virtual System.Boolean? IsHidden
        {
            get { return this._IsHidden; }
            set { this._IsHidden = value; }
        }
        [DataMember]
        [XmlElement(ElementName = "IsWorkStarted")]
        public Boolean? IsWorkStarted
        {
            get { return this._IsWorkStarted; }
            set { this._IsWorkStarted = value; }
        }

        #region "Task Attributes"
        private string _TaskName;
        private TaskListHdrInfo _TaskList;
        private string _TaskDescription;
        private List<UserHdrInfo> _AssignedTo;
        private List<UserHdrInfo> _Reviewer;
        private DateTime? _ReviewerDueDate;
        private int? _AssigneeDueDays;
        private int? _ReviewerDueDays;
        private List<AccountHdrInfo> _TaskAccount;
        private int? _TaskDueDays;
        private List<AttachmentInfo> _CreationAttachment;
        private List<UserHdrInfo> _Notify;
        private TaskRecurrenceTypeMstInfo _RecurrenceType;
        private DateTime? _TaskStartDate;
        private DateTime? _TaskDueDate;
        private List<ReconciliationPeriodInfo> _RecurrenceFrequency;
        private DateTime? _AssigneeDueDate;
        protected System.String _NotifyUserName = null;
        private List<ReconciliationPeriodInfo> _RecurrencePeriodNumberList;

        private Int16? _TaskDueDaysBasis;
        [DataMember]
        [XmlElement(ElementName = "TaskDueDaysBasis")]
        public Int16? TaskDueDaysBasis
        {
            get { return _TaskDueDaysBasis; }
            set { _TaskDueDaysBasis = value; }
        }
        private Int16? _AssigneeDueDaysBasis;
        [DataMember]
        [XmlElement(ElementName = "AssigneeDueDaysBasis")]
        public Int16? AssigneeDueDaysBasis
        {
            get { return _AssigneeDueDaysBasis; }
            set { _AssigneeDueDaysBasis = value; }
        }
        private Int16? _TaskDueDaysBasisSkipNumber;
        [DataMember]
        [XmlElement(ElementName = "TaskDueDaysBasisSkipNumber")]
        public Int16? TaskDueDaysBasisSkipNumber
        {
            get { return _TaskDueDaysBasisSkipNumber; }
            set { _TaskDueDaysBasisSkipNumber = value; }
        }
        private Int16? _AssigneeDueDaysBasisSkipNumber;
        [DataMember]
        [XmlElement(ElementName = "AssigneeDueDaysBasisSkipNumber")]
        public Int16? AssigneeDueDaysBasisSkipNumber
        {
            get { return _AssigneeDueDaysBasisSkipNumber; }
            set { _AssigneeDueDaysBasisSkipNumber = value; }
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

        [DataMember]
        [XmlElement(ElementName = "AssignedTo")]
        public List<UserHdrInfo> AssignedTo
        {
            get
            {
                return this._AssignedTo;
            }
            set
            {
                this._AssignedTo = value;
            }
        }

        [DataMember]
        [XmlElement(ElementName = "Reviewer")]
        public List<UserHdrInfo> Reviewer
        {
            get { return _Reviewer; }
            set { _Reviewer = value; }
        }

        [DataMember]
        [XmlElement(ElementName = "Approver")]
        public List<UserHdrInfo> Approver { get; set; }

        [DataMember]
        [XmlElement(ElementName = "Notify")]
        public List<UserHdrInfo> Notify
        {
            get { return _Notify; }
            set { _Notify = value; }
        }

        [DataMember]
        [XmlElement(ElementName = "CreationAttachment")]
        public List<AttachmentInfo> CreationAttachment
        {
            get { return _CreationAttachment; }
            set { _CreationAttachment = value; }
        }

        [DataMember]
        [XmlElement(ElementName = "RecurrenceType")]
        public TaskRecurrenceTypeMstInfo RecurrenceType
        {
            get { return _RecurrenceType; }
            set { _RecurrenceType = value; }
        }

        [DataMember]
        [XmlElement(ElementName = "RecurrenceFrequency")]
        public List<ReconciliationPeriodInfo> RecurrenceFrequency
        {
            get { return _RecurrenceFrequency; }
            set { _RecurrenceFrequency = value; }
        }

        [DataMember]
        [XmlElement(ElementName = "TaskStartDate")]
        public DateTime? TaskStartDate
        {
            get { return _TaskStartDate; }
            set { _TaskStartDate = value; }
        }

        [DataMember]
        [XmlElement(ElementName = "TaskDueDate")]
        public DateTime? TaskDueDate
        {
            get { return _TaskDueDate; }
            set { _TaskDueDate = value; }
        }

        [DataMember]
        [XmlElement(ElementName = "AssigneeDueDate")]
        public DateTime? AssigneeDueDate
        {
            get { return _AssigneeDueDate; }
            set { _AssigneeDueDate = value; }
        }

        [DataMember]
        [XmlElement(ElementName = "ReviewerDueDate")]
        public DateTime? ReviewerDueDate
        {
            get { return _ReviewerDueDate; }
            set { _ReviewerDueDate = value; }
        }

        [DataMember]
        [XmlElement(ElementName = "AssigneeDueDays")]
        public int? AssigneeDueDays
        {
            get { return _AssigneeDueDays; }
            set { _AssigneeDueDays = value; }
        }

        [DataMember]
        [XmlElement(ElementName = "ReviewerDueDays")]
        public int? ReviewerDueDays
        {
            get { return _ReviewerDueDays; }
            set { _ReviewerDueDays = value; }
        }

        [DataMember]
        [XmlElement(ElementName = "TaskAccount")]
        public List<AccountHdrInfo> TaskAccount
        {
            get { return _TaskAccount; }
            set { _TaskAccount = value; }
        }

        [DataMember]
        [XmlElement(ElementName = "TaskDueDays")]
        public int? TaskDueDays
        {
            get { return _TaskDueDays; }
            set { _TaskDueDays = value; }
        }
        [DataMember]
        [XmlElement(ElementName = "AssignedToUserName")]
        public virtual System.String AssignedToUserName
        { get; set; }
        [DataMember]
        [XmlElement(ElementName = "ReviewerUserName")]
        public virtual System.String ReviewerUserName
        { get; set; }
        [DataMember]
        [XmlElement(ElementName = "ApproverUserName")]
        public virtual System.String ApproverUserName
        { get; set; }
        [DataMember]
        [XmlElement(ElementName = "NotifyUserName")]
        public virtual System.String NotifyUserName
        { get; set; }
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
        public int? NumberValue { get; set; }
        [DataMember]
        public DateTime? DateValue { get; set; }

        [DataMember]
        [XmlElement(ElementName = "RecurrencePeriodNumberList")]
        public List<ReconciliationPeriodInfo> RecurrencePeriodNumberList
        {
            get { return _RecurrencePeriodNumberList; }
            set { _RecurrencePeriodNumberList = value; }
        }

        [DataMember]
        [XmlElement(ElementName = "TaskSubList")]
        public TaskSubListHdrInfo TaskSubList
        { get; set; }

        private string _TaskSubListName = null;
        [DataMember]
        public string TaskSubListName
        {
            get
            {
                this._TaskSubListName = string.Empty;
                if (this.TaskSubList != null)
                    this._TaskSubListName = this.TaskSubList.TaskSubListName;

                return this._TaskSubListName;
            }
        }
        private Int32 _TaskSubListID;
        [DataMember]
        public Int32 TaskSubListID
        {
            get
            {

                if (this.TaskSubList != null)
                    this._TaskSubListID = this.TaskSubList.TaskSubListID.Value;

                return this._TaskSubListID;
            }
        }
        private string _TaskSubListAddedBy = null;
        [DataMember]
        public string TaskSubListAddedBy
        {
            get
            {
                this._TaskSubListAddedBy = string.Empty;
                if (this.TaskSubList != null)
                    this._TaskSubListAddedBy = this.TaskSubList.AddedBy;

                return this._TaskSubListAddedBy;
            }
        }
        [DataMember]
        [XmlElement(ElementName = "CustomField1")]
        public string CustomField1 { get; set; }

        [DataMember]
        [XmlElement(ElementName = "CustomField2")]
        public string CustomField2 { get; set; }

        [DataMember]
        [XmlElement(ElementName = "ReviewerDueDaysBasis")]
        public short? ReviewerDueDaysBasis { get; set; }

        [DataMember]
        [XmlElement(ElementName = "ReviewerDueDaysBasisSkipNumber")]
        public short? ReviewerDueDaysBasisSkipNumber { get; set; }

        [DataMember]
        [XmlElement(ElementName = "DaysTypeID")]
        public short?  DaysTypeID { get; set; }


        #endregion


    }
}
