

using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using Adapdev.Text;

namespace SkyStem.ART.Client.Model.Base
{

    /// <summary>
    /// An object representation of the SkyStemART OperatorMst table
    /// </summary>
    [Serializable]
    public abstract class OperatorMstInfoBase
    {

        protected System.Boolean? _IsActive = false;
        protected System.Int16? _OperatorID = 0;
        protected System.String _OperatorName = "";
        protected System.Int32? _OperatorNameLabelID = 0;




        public bool IsIsActiveNull = true;


        public bool IsOperatorIDNull = true;


        public bool IsOperatorNameNull = true;


        public bool IsOperatorNameLabelIDNull = true;

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

        [XmlElement(ElementName = "OperatorName")]
        public virtual System.String OperatorName
        {
            get
            {
                return this._OperatorName;
            }
            set
            {
                this._OperatorName = value;

                this.IsOperatorNameNull = (_OperatorName == null);
            }
        }

        [XmlElement(ElementName = "OperatorNameLabelID")]
        public virtual System.Int32? OperatorNameLabelID
        {
            get
            {
                return this._OperatorNameLabelID;
            }
            set
            {
                this._OperatorNameLabelID = value;

                this.IsOperatorNameLabelIDNull = false;
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
