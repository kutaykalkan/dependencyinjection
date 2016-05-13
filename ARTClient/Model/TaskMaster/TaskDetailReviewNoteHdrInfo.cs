
using System;
using Adapdev.Text;
using System.Runtime.Serialization;
using SkyStem.ART.Client.Model.Base;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace SkyStem.ART.Client.Model
{    

	/// <summary>
	/// An object representation of the SkyStemART TaskDetailReviewNoteHdr table
	/// </summary>
	[Serializable]
	[DataContract]
	public class TaskDetailReviewNoteHdrInfo : TaskDetailReviewNoteHdrInfoBase
	{
        private System.String _AddedByUserFirstName = null;
        private System.String _AddedByUserLastName = null;
        private List<TaskDetailReviewNoteDetailInfo> _TaskDetailReviewNoteDetailInfo = null;

        [DataMember]
        [XmlElement(ElementName = "AddedByUserFirstName")]
        public System.String AddedByUserFirstName
        {
            get { return _AddedByUserFirstName; }
            set { _AddedByUserFirstName = value; }
        }
        
        [DataMember]
        [XmlElement(ElementName = "AddedByUserLastName")]
        public System.String AddedByUserLastName
        {
            get { return _AddedByUserLastName; }
            set { _AddedByUserLastName = value; }
        }

        [DataMember]
        [XmlElement(ElementName = "TaskDetailReviewNoteDetailInfo")]
        public List<TaskDetailReviewNoteDetailInfo> TaskDetailReviewNoteDetailInfo
        {
            get { return _TaskDetailReviewNoteDetailInfo; }
            set { _TaskDetailReviewNoteDetailInfo = value; }
        }
        
        
	}
}
