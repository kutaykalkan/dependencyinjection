
using System;
using Adapdev.Text;
using SkyStem.ART.Client.Model.Base;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace SkyStem.ART.Client.Model
{    

	/// <summary>
	/// An object representation of the SkyStemArt AlertMst table
	/// </summary>
	[Serializable]
	[DataContract]
	public class CertificationStatusInfo 
	{
        System.Int32? _UnCertifiedAccountsCount = 0;
        System.Decimal? _UnCertifiedAccountsDollarAmmount = 0.00M;
        System.Decimal? _UnCertifiedAccountsPercentage = 0.00M;

        [XmlElement(ElementName = "UnCertifiedAccountsCount")]
        public System.Int32? UnCertifiedAccountsCount
        {
            get { return _UnCertifiedAccountsCount; }
            set { _UnCertifiedAccountsCount = value; }
        }

        [XmlElement(ElementName = "UnCertifiedAccountsDollarAmmount")]
        public virtual System.Decimal? UnCertifiedAccountsDollarAmmount
        {
            get
            {
                return this._UnCertifiedAccountsDollarAmmount;
            }
            set
            {
                this._UnCertifiedAccountsDollarAmmount = value;               
            }
        }


        [XmlElement(ElementName = "UnCertifiedAccountsPercentage")]
        public virtual System.Decimal? UnCertifiedAccountsPercentage
        {
            get
            {
                return this._UnCertifiedAccountsPercentage;
            }
            set
            {
                this._UnCertifiedAccountsPercentage = value;
            }
        }

	}
}
