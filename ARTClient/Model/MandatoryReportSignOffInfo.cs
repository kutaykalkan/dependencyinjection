
using System;
using Adapdev.Text;
using SkyStem.ART.Client.Model.Base;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace SkyStem.ART.Client.Model
{    

	/// <summary>
	/// An object representation of the SkyStemArt MandatoryReportSignOff table
	/// </summary>
	[Serializable]
	[DataContract]
	public class MandatoryReportSignOffInfo : MandatoryReportSignOffInfoBase
	{


        protected System.Int16? _CertificationTypeID = null;
        public bool IsCertificationTypeIDNull = true;
        [XmlElement(ElementName = "CertificationTypeID")]
        public virtual System.Int16? CertificationTypeID
        {
            get
            {
                return this._CertificationTypeID;
            }
            set
            {
                this._CertificationTypeID = value;

                this.IsCertificationTypeIDNull = false;
            }
        }

        protected System.String  _FirstName = null;
        [XmlElement(ElementName = "FirstName")]
        public virtual System.String FirstName
        {
            get
            {
                return this._FirstName;
            }
            set
            {
                this._FirstName = value;
            }
        }


        protected System.String _LastName = null;
        [XmlElement(ElementName = "LastName")]
        public virtual System.String LastName
        {
            get
            {
                return this._LastName;
            }
            set
            {
                this._LastName = value;
            }
        }



	}
}
