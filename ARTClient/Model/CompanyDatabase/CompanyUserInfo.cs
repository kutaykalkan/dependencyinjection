
using System;
using Adapdev.Text;
using System.Runtime.Serialization;
using SkyStem.ART.Client.Model.CompanyDatabase.Base;

namespace SkyStem.ART.Client.Model.CompanyDatabase
{

    /// <summary>
    /// An object representation of the SkyStemARTCore CompanyUser table
    /// </summary>
    [Serializable]
    [DataContract]
    public class CompanyUserInfo : CompanyUserInfoBase
    {
        [DataMember]
        public string DatabaseName { get; set; }

        [DataMember]
        public string ServerName { get; set; }

        [DataMember]
        public string Instance { get; set; }

        [DataMember]
        public string DBUserID { get; set; }

        [DataMember]
        public string DBPassword { get; set; }

        [DataMember]
        public bool? IsSeparateDatabase { get; set; }

        [DataMember]
        public string EmailID { get; set; }

        [DataMember]
        public string CompanyName { get; set; }

        [DataMember]
        public string FTPLoginID { get; set; }
    }
}
