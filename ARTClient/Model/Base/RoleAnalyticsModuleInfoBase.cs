

using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using Adapdev.Text;

namespace SkyStem.ART.Client.Model.Base
{

    /// <summary>
    /// An object representation of the SkyStemART RoleAnalyticsModule table
    /// </summary>
    [Serializable]
    public abstract class RoleAnalyticsModuleInfoBase
    {

        protected System.Int16? _AnalyticsModuleID = null;
        protected System.Int16? _RoleAnalyticsModuleID = null;
        protected System.Int16? _RoleID = null;

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

        [XmlElement(ElementName = "RoleAnalyticsModuleID")]
        public virtual System.Int16? RoleAnalyticsModuleID
        {
            get
            {
                return this._RoleAnalyticsModuleID;
            }
            set
            {
                this._RoleAnalyticsModuleID = value;

            }
        }

        [XmlElement(ElementName = "RoleID")]
        public virtual System.Int16? RoleID
        {
            get
            {
                return this._RoleID;
            }
            set
            {
                this._RoleID = value;

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
