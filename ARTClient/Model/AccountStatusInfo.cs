
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
	public class AccountStatusInfo 
	{
        System.Int32? _UnReconciledAccountsCount = 0;
        System.Decimal? _UnReconciledAccountsDollarAmmount = 0.00M;
        System.Decimal? _UnReconciledAccountsPercentage = 0.00M;
        System.Boolean? _IsSRA = false;

        [XmlElement(ElementName = "UnReconciledAccountsCount")]
        public System.Int32? UnReconciledAccountsCount
        {
            get { return _UnReconciledAccountsCount; }
            set { _UnReconciledAccountsCount = value; }
        }

        [XmlElement(ElementName = "UnReconciledAccountsDollarAmmount")]
        public virtual System.Decimal? UnReconciledAccountsDollarAmmount
        {
            get
            {
                return this._UnReconciledAccountsDollarAmmount;
            }
            set
            {
                this._UnReconciledAccountsDollarAmmount = value;               
            }
        }


        [XmlElement(ElementName = "UnReconciledAccountsPercentage")]
        public virtual System.Decimal? UnReconciledAccountsPercentage
        {
            get
            {
                return this._UnReconciledAccountsPercentage;
            }
            set
            {
                this._UnReconciledAccountsPercentage = value;
            }
        }


        [XmlElement(ElementName = "IsSRA")]
        public virtual System.Boolean? IsSRA
        {
            get
            {
                return this._IsSRA;
            }
            set
            {
                this._IsSRA = value;
               
            }
        }			

	}
}
