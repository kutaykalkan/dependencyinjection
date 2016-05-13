
using System;
using Adapdev.Text;
using SkyStem.ART.Client.Model.Base;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using System.Collections.Generic;
using SkyStem.ART.Client.Data;

namespace SkyStem.ART.Client.Model
{    

	/// <summary>
	/// An object representation of the SkyStemArt GLDataReviewNoteHdr table
	/// </summary>
	[Serializable]
	[DataContract]
	public class GLDataReviewNoteHdrInfo : GLDataReviewNoteHdrInfoBase
	{
        UserHdrInfo _AddedByUserInfo = null;
        UserHdrInfo _RevisedByUserInfo = null;

        List<GLDataReviewNoteDetailInfo> _GLDataReviewNoteDetailInfoCollection = null;

        [XmlElement(ElementName = "AddedByUserInfo")]
        public UserHdrInfo AddedByUserInfo
        {
            get { return _AddedByUserInfo; }
            set { _AddedByUserInfo = value; }
        }

        [XmlElement(ElementName = "RevisedByUserInfo")]
        public UserHdrInfo RevisedByUserInfo
        {
            get { return _RevisedByUserInfo; }
            set { _RevisedByUserInfo = value; }
        }

        [XmlElement(ElementName = "GLDataReviewNoteDetailInfoCollection")]
        public List<GLDataReviewNoteDetailInfo> GLDataReviewNoteDetailInfoCollection
        {
            get { return _GLDataReviewNoteDetailInfoCollection; }
            set { _GLDataReviewNoteDetailInfoCollection = value; }
        }


        [XmlElement(ElementName = "AddedByFullName")]
        public String AddedByFullName
        {
            get
            {
                return ModelHelper.GetFullName(this.AddedByUserInfo.FirstName, this.AddedByUserInfo.LastName);
            }
        }

        [XmlElement(ElementName = "RevisedByFullName")]
        public String RevisedByFullName
        {
            get
            {
                return ModelHelper.GetFullName(this.RevisedByUserInfo.FirstName, this.RevisedByUserInfo.LastName);
            }
        }
         [XmlElement(ElementName = "AttachmentCount")]
        public int? AttachmentCount { get; set; }

    }
}
