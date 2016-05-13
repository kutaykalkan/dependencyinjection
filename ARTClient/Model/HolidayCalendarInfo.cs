
using System;
using Adapdev.Text;
using SkyStem.ART.Client.Model.Base;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Serialization;

namespace SkyStem.ART.Client.Model
{    

	/// <summary>
	/// An object representation of the SkyStemArt HolidayCalendar table
	/// </summary>
	[Serializable]
	[DataContract]
	public class HolidayCalendarInfo : HolidayCalendarInfoBase
	{
        protected System.Int32? _DataImportID = 0;
        public bool IsDataImportIDNull = true;

        [XmlElement(ElementName = "DataImportID")]
        public virtual System.Int32? DataImportID
        {
            get
            {
                return this._DataImportID;
            }
            set
            {
                this._DataImportID = value;

                this.IsDataImportIDNull = false;
            }
        }	
	}
}
