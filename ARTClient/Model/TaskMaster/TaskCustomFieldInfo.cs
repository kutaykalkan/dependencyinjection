
using System;
using Adapdev.Text;
using System.Runtime.Serialization;
using SkyStem.ART.Client.Model.Base;
using System.Xml.Serialization;

namespace SkyStem.ART.Client.Model
{    

	/// <summary>
    /// An object representation of the SkyStemART TaskStatusCountInfo table
	/// </summary>
	[Serializable]
	[DataContract]
	public class TaskStatusCountInfo
	{
        private System.Int32? _TotalCount = null;
        private System.String _MonthName = null;
        private System.Int32? _Pending = null;
        private System.Int32? _Overdue = null;
        private System.Int32? _Completed = null;
        private System.Int16? _MonthNumber = null;
        private System.Int16? _TaskTypeID = null;
        private System.Int32? _MonthNameLabelID = null;
        private System.DateTime? _MonthStartDate = null;
        private System.DateTime? _MonthEndDate = null;



        [DataMember]
        [XmlElement(ElementName = "TotalCount")]
        public System.Int32? TotalCount
        {
            get { return _TotalCount; }
            set { _TotalCount = value; }
        }

        [DataMember]
        [XmlElement(ElementName = "MonthName")]
        public System.String MonthName
        {
            get { return _MonthName; }
            set { _MonthName = value; }
        }
        [DataMember]
        [XmlElement(ElementName = "Pending")]
        public System.Int32? Pending
        {
            get { return _Pending; }
            set { _Pending = value; }
        }
        [DataMember]
        [XmlElement(ElementName = "Overdue")]
        public System.Int32? Overdue
        {
            get { return _Overdue; }
            set { _Overdue = value; }
        }
        [DataMember]
        [XmlElement(ElementName = "Completed")]
        public System.Int32? Completed
        {
            get { return _Completed; }
            set { _Completed = value; }
        }
        [DataMember]
        [XmlElement(ElementName = "MonthNumber")]
        public System.Int16? MonthNumber
        {
            get { return _MonthNumber; }
            set { _MonthNumber = value; }
        }
        [DataMember]
        [XmlElement(ElementName = "TaskTypeID")]
        public System.Int16? TaskTypeID
        {
            get { return _TaskTypeID; }
            set { _TaskTypeID = value; }
        }
        [DataMember]
        [XmlElement(ElementName = "MonthNameLabelID")]
        public System.Int32? MonthNameLabelID
        {
            get { return _MonthNameLabelID; }
            set { _MonthNameLabelID = value; }
        }
        [DataMember]
        [XmlElement(ElementName = "MonthStartDate")]
        public virtual System.DateTime? MonthStartDate
        {
            get { return this._MonthStartDate; }
            set { this._MonthStartDate = value; }
        }	
        [DataMember]
        [XmlElement(ElementName = "MonthEndDate")]
        public virtual System.DateTime? MonthEndDate
        {
            get { return this._MonthEndDate; }
            set { this._MonthEndDate = value; }
        }
        [DataMember]
        [XmlElement(ElementName = "Year")]
        public string Year { get; set; }
		
	}
}
