

using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using Adapdev.Text;

namespace SkyStem.ART.Client.Model.Base
{

    /// <summary>
    /// An object representation of the SkyStemART CompanyWeekDay table
    /// </summary>
    [Serializable]
    public abstract class CompanyWeekDayInfoBase
    {
        protected System.Int32? _CompanyRecPeriodSetID = null;
        protected System.Int32? _CompanyWeekDayID = null;
        protected System.Int16? _WeekDayID = null;
    

        [XmlElement(ElementName = "CompanyRecPeriodSetID")]
        public virtual System.Int32? CompanyRecPeriodSetID
        {
            get
            {
                return this._CompanyRecPeriodSetID;
            }
            set
            {
                this._CompanyRecPeriodSetID = value;

            }
        }

        [XmlElement(ElementName = "CompanyWeekDayID")]
        public virtual System.Int32? CompanyWeekDayID
        {
            get
            {
                return this._CompanyWeekDayID;
            }
            set
            {
                this._CompanyWeekDayID = value;

            }
        }

        [XmlElement(ElementName = "WeekDayID")]
        public virtual System.Int16? WeekDayID
        {
            get
            {
                return this._WeekDayID;
            }
            set
            {
                this._WeekDayID = value;

            }
        }


        /// <summary>
        /// Returns a string representation of the object, displaying all property and field names and values.
        /// </summary>
        public override string ToString()
        {
            return StringUtil.ToString(this);
        }




    }
}
