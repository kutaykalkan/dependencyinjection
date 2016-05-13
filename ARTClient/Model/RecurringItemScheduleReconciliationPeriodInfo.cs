
using System;
using Adapdev.Text;
using SkyStem.ART.Client.Model.Base;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace SkyStem.ART.Client.Model
{    

	/// <summary>
	/// An object representation of the SkyStemArt RecurringItemScheduleReconciliationPeriod table
	/// </summary>
	[Serializable]
	[DataContract]
	public class RecurringItemScheduleReconciliationPeriodInfo : RecurringItemScheduleReconciliationPeriodInfoBase
	{

        protected System.String _ScheduleName = "";
        [XmlElement(ElementName = "ScheduleName")]
        public virtual System.String ScheduleName
        {
            get
            {
                return this._ScheduleName;
            }
            set
            {
                this._ScheduleName = value;
            }
        }
	}
}
