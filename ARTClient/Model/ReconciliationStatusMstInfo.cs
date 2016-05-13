
using System;
using Adapdev.Text;
using SkyStem.ART.Client.Model.Base;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace SkyStem.ART.Client.Model
{    

	/// <summary>
	/// An object representation of the SkyStemArt ReconciliationStatusMst table
	/// </summary>
	[Serializable]
	[DataContract]
	public class ReconciliationStatusMstInfo : ReconciliationStatusMstInfoBase
	{

        protected System.Decimal? _Amount = null;

        [XmlElement(ElementName = "Amount")]
        public virtual System.Decimal? Amount
        {
            get
            {
                return this._Amount;
            }
            set
            {
                this._Amount = value;
            }
        }

        protected System.String _StatusColor = "";
        [XmlElement(ElementName = "StatusColor")]
        public virtual System.String StatusColor
        {
            get
            {
                return this._StatusColor;
            }
            set
            {
                this._StatusColor = value;               
            }
        }

	}
}
