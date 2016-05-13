using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace SkyStem.ART.Client.Model
{
    public class ReconciledAccountCountBalanceInfo
    {
        System.Int32? _TotalAccounts = null;
        System.Int32? _TotalReconciledAccounts = null;
        System.Decimal? _UnreconciledBalance = null;
        System.Decimal? _ReconciledBalance = null;

        System.Decimal? _TotalAccountGLBalance = null;
        System.Decimal? _TotalReconciledAccountGLBalance = null;

        System.String _BaseCurrencyCode = null;
        System.String _ReportingCurrencyCode = null;


        [XmlElement(ElementName = "TotalAccounts")]
        public System.Int32? TotalAccounts
        {
            get { return _TotalAccounts; }
            set { _TotalAccounts = value; }
        }

        [XmlElement(ElementName = "TotalReconciledAccounts")]
        public System.Int32? TotalReconciledAccounts
        {
            get { return _TotalReconciledAccounts; }
            set { _TotalReconciledAccounts = value; }
        }

        [XmlElement(ElementName = "UnreconciledBalance")]
        public System.Decimal? UnreconciledBalance
        {
            get { return _UnreconciledBalance; }
            set { _UnreconciledBalance = value; }
        }

        [XmlElement(ElementName = "ReconciledBalance")]
        public System.Decimal? ReconciledBalance
        {
            get { return _ReconciledBalance; }
            set { _ReconciledBalance = value; }
        }


        [XmlElement(ElementName = "TotalAccountGLBalance")]
        public System.Decimal? TotalAccountGLBalance
        {
            get { return _TotalAccountGLBalance; }
            set { _TotalAccountGLBalance = value; }
        }

        [XmlElement(ElementName = "TotalReconciledAccountGLBalance")]
        public System.Decimal? TotalReconciledAccountGLBalance
        {
            get { return _TotalReconciledAccountGLBalance; }
            set { _TotalReconciledAccountGLBalance = value; }
        }


        [XmlElement(ElementName = "BaseCurrencyCode")]
        public System.String BaseCurrencyCode
        {
            get { return _BaseCurrencyCode; }
            set { _BaseCurrencyCode = value; }
        }

        [XmlElement(ElementName = "ReportingCurrencyCode")]
        public System.String ReportingCurrencyCode
        {
            get { return _ReportingCurrencyCode; }
            set { _ReportingCurrencyCode = value; }
        }


    }
}
