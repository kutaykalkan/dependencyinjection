
using System;
using Adapdev.Text;
using SkyStem.ART.Client.Model.Base;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace SkyStem.ART.Client.Model
{    

	/// <summary>
	/// An object representation of the SkyStemArt DataImportTypeMst table
	/// </summary>
	[Serializable]
	[DataContract]
	public class DataImportTypeMstInfo : DataImportTypeMstInfoBase
	{
        System.Int16? _RoleID;
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
   
	}
}
