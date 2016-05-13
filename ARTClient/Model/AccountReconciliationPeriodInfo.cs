
using System;
using Adapdev.Text;
using SkyStem.ART.Client.Model.Base;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace SkyStem.ART.Client.Model
{    

	/// <summary>
	/// An object representation of the SkyStemArt AccountReconciliationPeriod table
	/// </summary>
	[Serializable]
	[DataContract]
	public class AccountReconciliationPeriodInfo : AccountReconciliationPeriodInfoBase
	{
        protected System.DateTime? _PeriodEndDate = null;
        protected System.Int16? _PeriodNumber = null;
        protected System.String _FinancialYear = null;

        [XmlElement(ElementName = "PeriodEndDate")]
        public virtual System.DateTime? PeriodEndDate
        {
            get
            {
                return this._PeriodEndDate;
            }
            set
            {
                this._PeriodEndDate = value;
            }
        }


        [XmlElement(ElementName = "PeriodNumber")]
        public virtual System.Int16? PeriodNumber
        {
            get
            {
                return this._PeriodNumber;
            }
            set
            {
                this._PeriodNumber = value;
            }
        }


        [XmlElement(ElementName = "FinancialYear")]
        public virtual System.String FinancialYear
        {
            get
            {
                return this._FinancialYear;
            }
            set
            {
                this._FinancialYear = value;
            }
        }

	}
}
