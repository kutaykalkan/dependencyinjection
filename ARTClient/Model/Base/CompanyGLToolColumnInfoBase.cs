

using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using Adapdev.Text;

namespace SkyStem.ART.Client.Model.Base
{

    /// <summary>
    /// An object representation of the SkyStemART CompanyGLToolColumn table
    /// </summary>
    [Serializable]
    public abstract class CompanyGLToolColumnInfoBase
    {


        protected System.Int32? _CompanyGLToolColumnID = null;
        protected System.Int32? _CompanyRecPeriodSetID = null;
        protected System.Int16? _GLToolColumnLength = null;
        protected System.String _GLToolColumnName = null;



        [XmlElement(ElementName = "CompanyGLToolColumnID")]
        public virtual System.Int32? CompanyGLToolColumnID
        {
            get
            {
                return this._CompanyGLToolColumnID;
            }
            set
            {
                this._CompanyGLToolColumnID = value;

            }
        }

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

        [XmlElement(ElementName = "GLToolColumnLength")]
        public virtual System.Int16? GLToolColumnLength
        {
            get
            {
                return this._GLToolColumnLength;
            }
            set
            {
                this._GLToolColumnLength = value;

            }
        }

        [XmlElement(ElementName = "GLToolColumnName")]
        public virtual System.String GLToolColumnName
        {
            get
            {
                return this._GLToolColumnName;
            }
            set
            {
                this._GLToolColumnName = value;

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
