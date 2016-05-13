
using System;
using Adapdev.Text;
using SkyStem.ART.Client.Model.Base;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace SkyStem.ART.Client.Model
{    

	/// <summary>
	/// An object representation of the SkyStemART RecItemColumnMst table
	/// </summary>
	[Serializable]
	[DataContract]
	public class RecItemColumnMstInfo : RecItemColumnMstInfoBase
	{
        protected System.Int16? _DataTypeID = null;
        [XmlElement(ElementName = "DataTypeID")]
        public virtual System.Int16? DataTypeID
        {
            get
            {
                return this._DataTypeID;
            }
            set
            {
                this._DataTypeID = value;

            }
        }
        protected System.String _DataType = null;
        [XmlElement(ElementName = "DataType")]
        public virtual System.String DataType
        {
            get
            {
                return this._DataType;
            }
            set
            {
                this._DataType = value;

            }
        }
	}
}
