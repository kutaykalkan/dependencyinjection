

using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using Adapdev.Text;

namespace SkyStem.ART.Client.Model.Base
{

    /// <summary>
    /// An object representation of the SkyStemART CompanyAnalyticsModule table
    /// </summary>
    [Serializable]
    public abstract class CompanyAnalyticsModuleInfoBase
    {

        protected System.Int16? _AnalyticsModuleID = null;
        protected System.Int16? _CompanyAnalyticsModuleID = null;
        protected System.Int32? _CompanyID = null;


        [XmlElement(ElementName = "AnalyticsModuleID")]
        public virtual System.Int16? AnalyticsModuleID
        {
            get
            {
                return this._AnalyticsModuleID;
            }
            set
            {
                this._AnalyticsModuleID = value;

            }
        }

        [XmlElement(ElementName = "CompanyAnalyticsModuleID")]
        public virtual System.Int16? CompanyAnalyticsModuleID
        {
            get
            {
                return this._CompanyAnalyticsModuleID;
            }
            set
            {
                this._CompanyAnalyticsModuleID = value;
            }
        }

        [XmlElement(ElementName = "CompanyID")]
        public virtual System.Int32? CompanyID
        {
            get
            {
                return this._CompanyID;
            }
            set
            {
                this._CompanyID = value;
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
