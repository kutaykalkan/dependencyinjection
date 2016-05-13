
using System;
using Adapdev.Text;
using SkyStem.ART.Client.Model.Base;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace SkyStem.ART.Client.Model
{

    /// <summary>
    /// An object representation of the SkyStemArt CompanyHdr table
    /// </summary>
    [Serializable]
    [DataContract]
    public class CompanyHdrInfo : CompanyHdrInfoBase
    {
        private System.Int32? _ActualNoOfUsers = 0;
        private System.Decimal? _CurrentUsage = 0.00M;
        private AddressInfo _Address =  new AddressInfo ();
        private System.Int16? _PackageID = null;
        private bool? _IsCustomizedPackage = null;
        private bool? _ShowLogoOnMasterPage = null;

        [XmlElement(ElementName = "Address")]
        public AddressInfo Address
        {
            get { return  _Address; }
            set { _Address = value; }
        }

        [XmlElement(ElementName = "ActualNoOfUsers")]
        public System.Int32? ActualNoOfUsers
        {
            get
            {
                return _ActualNoOfUsers;
            }
            set
            {
                _ActualNoOfUsers = value;
            }
        }

        [XmlElement(ElementName = "CurrentUsageDataStorageCapacity")]
        public System.Decimal? CurrentUsage
        {
            get
            {
                return _CurrentUsage;
            }
            set
            {
                _CurrentUsage = value;
            }
        }

        [XmlElement(ElementName = "PackageID")]
        public System.Int16? PackageID
        {
            get { return _PackageID; }
            set {  _PackageID = value; }
        }

        [XmlElement(ElementName = "IsCustomizedPackage")]
        public bool? IsCustomizedPackage
        {
            get { return _IsCustomizedPackage; }
            set { _IsCustomizedPackage = value; }
        }

        [XmlElement(ElementName = "ShowLogoOnMasterPage")]
        public bool? ShowLogoOnMasterPage
        {
            get { return _ShowLogoOnMasterPage; }
            set { _ShowLogoOnMasterPage = value; }
        }

        [XmlElement(ElementName = "IsSeparateDatabase")]
        public bool? IsSeparateDatabase { get; set; }

        [XmlElement(ElementName = "IsDatabaseExists")]
        public bool? IsDatabaseExists { get; set; }

        [XmlElement(ElementName = "IsFTPEnabled")]
        public bool? IsFTPEnabled { get; set; }

        [XmlElement(ElementName = "CompanyStatusID")]
        public Int16? CompanyStatusID { get; set; }

        [XmlElement(ElementName = "CompanyStatusDate")]
        public DateTime? CompanyStatusDate { get; set; }

        [XmlElement(ElementName = "CompanyAlias")]
        public string CompanyAlias { get; set; }
    }
}
