
using System;
using Adapdev.Text;
using SkyStem.ART.Client.Model.Base;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace SkyStem.ART.Client.Model
{    

	/// <summary>
	/// An object representation of the SkyStemArt GridColumn table
	/// </summary>
	[Serializable]
	[DataContract]
	public class GridColumnInfo : GridColumnInfoBase
	{
        private System.String _ColumnUniqueName = "";

        [XmlElement(ElementName = "ColumnUniqueName")]
        public System.String ColumnUniqueName
        {
            get { return _ColumnUniqueName; }
            set { _ColumnUniqueName = value; }
        }

	}
}
