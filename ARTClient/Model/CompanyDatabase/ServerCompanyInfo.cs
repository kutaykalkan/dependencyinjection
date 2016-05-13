
using System;
using Adapdev.Text;
using System.Runtime.Serialization;
using SkyStem.ART.Client.Model.CompanyDatabase.Base;

namespace SkyStem.ART.Client.Model.CompanyDatabase
{

    /// <summary>
    /// An object representation of the SkyStemARTCore ServerCompany table
    /// </summary>
    [Serializable]
    [DataContract]
    public class ServerCompanyInfo : ServerCompanyInfoBase
    {
        [DataMember]
        public bool? IsDatabaseExists { get; set; }
        [DataMember]
        public bool? IsSeparateDatabase { get; set; }
        [DataMember]
        public string ServerName { get; set; }
        [DataMember]
        public string Instance { get; set; }
        [DataMember]
        public string DBUserID { get; set; }
        [DataMember]
        public string DBPassword { get; set; }
        [DataMember]
        public string DatabaseName { get; set; }
        [DataMember]
        public string MdfPath { get; set; }
        [DataMember]
        public string LdfPath { get; set; }
    }
}
