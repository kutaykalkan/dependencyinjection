
using System;
using Adapdev.Text;
using System.Runtime.Serialization;
using SkyStem.ART.Client.Model.Base;
using System.Xml.Serialization;

namespace SkyStem.ART.Client.Model
{    

	/// <summary>
	/// An object representation of the SkyStemART TaskDetailReviewNoteDetail table
	/// </summary>
	[Serializable]
	[DataContract]
	public class TaskDetailReviewNoteDetailInfo : TaskDetailReviewNoteDetailInfoBase
	{

        private System.String _AddedByUserFirstName = null;
        private System.String _AddedByUserLastName = null;

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

	}
}
