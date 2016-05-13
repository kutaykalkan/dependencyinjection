
using System;
using Adapdev.Text;
using SkyStem.ART.Client.Model.Base;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace SkyStem.ART.Client.Model
{    

	/// <summary>
	/// An object representation of the SkyStemART CompanyWeekDay table
	/// </summary>
	[Serializable]
	[DataContract]
	public class CompanyWeekDayInfo : CompanyWeekDayInfoBase
	{


        protected System.Int32? _RecPeriodID = null;

        [XmlElement(ElementName = "RecPeriodID")]
        public virtual System.Int32? RecPeriodID
        {
            get
            {
                return this._RecPeriodID;
            }
            set
            {
                this._RecPeriodID = value;

            }
        }

        protected System.Int32? _StartRecPeriodID = null;
        protected System.Int32? _EndRecPeriodID = null;
        [XmlElement(ElementName = "StartRecPeriodID")]
        public virtual System.Int32? StartRecPeriodID
        {
            get
            {
                return this._StartRecPeriodID;
            }
            set
            {
                this._StartRecPeriodID = value;

            }
        }

        [XmlElement(ElementName = "EndRecPeriodID")]
        public virtual System.Int32? EndRecPeriodID
        {
            get
            {
                return this._EndRecPeriodID;
            }
            set
            {
                this._EndRecPeriodID = value;

            }
        }

	}
}
