

using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using Adapdev.Text;

namespace SkyStem.ART.Client.Model.Base
{

    /// <summary>
    /// An object representation of the SkyStemART ColumnOperatorMapping table
    /// </summary>
    [Serializable]
    public abstract class ColumnOperatorMappingInfoBase
    {

        protected System.Int16? _ColumnID = 0;
        protected System.Int16? _ColumnOperatorMappingID = 0;
        protected System.Boolean? _IsActive = false;
        protected System.Int16? _OperatorID = 0;

        public bool IsColumnIDNull = true;


        public bool IsColumnOperatorMappingIDNull = true;


        public bool IsIsActiveNull = true;


        public bool IsOperatorIDNull = true;

        [XmlElement(ElementName = "ColumnID")]
        public virtual System.Int16? ColumnID
        {
            get
            {
                return this._ColumnID;
            }
            set
            {
                this._ColumnID = value;
                this.IsColumnIDNull = false;
            }
        }

        [XmlElement(ElementName = "ColumnOperatorMappingID")]
        public virtual System.Int16? ColumnOperatorMappingID
        {
            get
            {
                return this._ColumnOperatorMappingID;
            }
            set
            {
                this._ColumnOperatorMappingID = value;

                this.IsColumnOperatorMappingIDNull = false;
            }
        }

        [XmlElement(ElementName = "IsActive")]
        public virtual System.Boolean? IsActive
        {
            get
            {
                return this._IsActive;
            }
            set
            {
                this._IsActive = value;

                this.IsIsActiveNull = false;
            }
        }

        [XmlElement(ElementName = "OperatorID")]
        public virtual System.Int16? OperatorID
        {
            get
            {
                return this._OperatorID;
            }
            set
            {
                this._OperatorID = value;

                this.IsOperatorIDNull = false;
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
