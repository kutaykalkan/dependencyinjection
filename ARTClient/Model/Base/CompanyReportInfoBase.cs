

using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using Adapdev.Text;

namespace SkyStem.ART.Client.Model.Base
{

    /// <summary>
    /// An object representation of the SkyStemART CompanyReport table
    /// </summary>
    [Serializable]
    public abstract class CompanyReportInfoBase
    {
        protected System.Int32? _CompanyID = null;
        protected System.Int16? _CompanyReportID = null;
        protected System.Int16? _ReportID = null;

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

        [XmlElement(ElementName = "CompanyReportID")]
        public virtual System.Int16? CompanyReportID
        {
            get
            {
                return this._CompanyReportID;
            }
            set
            {
                this._CompanyReportID = value;
            }
        }

        [XmlElement(ElementName = "ReportID")]
        public virtual System.Int16? ReportID
        {
            get
            {
                return this._ReportID;
            }
            set
            {
                this._ReportID = value;
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
