
using System;
using Adapdev.Text;
using SkyStem.ART.Client.Model.Base;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace SkyStem.ART.Client.Model
{    

	/// <summary>
	/// An object representation of the SkyStemART GLDataRecurringItemScheduleIntervalDetail table
	/// </summary>
	[Serializable]
	[DataContract]
	public class GLDataRecurringItemScheduleIntervalDetailInfo : GLDataRecurringItemScheduleIntervalDetailInfoBase
	{
        public DateTime? PeriodEndDate { get; set; }

        protected System.Boolean? _IsDisabled = false;

        [XmlElement(ElementName = "IsActive")]
        public virtual System.Boolean? IsDisabled
        {
            get
            {
                return this._IsDisabled;
            }
            set
            {
                this._IsDisabled = value;
            }
        }


	}
}
