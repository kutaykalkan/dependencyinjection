
using System;
using Adapdev.Text;
using SkyStem.ART.Client.Model.Base;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace SkyStem.ART.Client.Model
{    

	/// <summary>
	/// An object representation of the SkyStemArt SubledgerData table
	/// </summary>
	[Serializable]
	[DataContract]
	public class SubledgerDataInfo : SubledgerDataInfoBase
	{
        protected System.String _AddedBy = "";
        protected System.DateTime? _DateAdded = DateTime.Now;
        protected System.Int64? _SubledgerDataArchiveID;
        protected System.String _PhysicalPath = "";
        [XmlElement(ElementName = "PhysicalPath")]
        public virtual System.String PhysicalPath
        {
            get
            {
                return this._PhysicalPath;
            }
            set
            {
                this._PhysicalPath = value;
            }
        }


        [XmlElement(ElementName = "AddedBy")]
        public virtual System.String AddedBy
        {
            get
            {
                return this._AddedBy;
            }
            set
            {
                this._AddedBy = value;
            }
        }

        [XmlElement(ElementName = "DateAdded")]
        public virtual System.DateTime? DateAdded
        {
            get
            {
                return this._DateAdded;
            }
            set
            {
                this._DateAdded = value;
            }
        }

        [XmlElement(ElementName = "SubledgerDataArchiveID")]
        public virtual System.Int64? SubledgerDataArchiveID
        {
            get
            {
                return this._SubledgerDataArchiveID;
            }
            set
            {
                this._SubledgerDataArchiveID = value;
            }
        }
        [DataMember]
        [XmlElement(ElementName = "DataImportTypeID")]
        public Int16? DataImportTypeID { get; set; }
    }
}
