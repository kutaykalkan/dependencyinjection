using System;
using Adapdev.Text;
using SkyStem.ART.Client.Model.Base;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;
using System.Xml;

namespace SkyStem.ART.Client.Model
{
    /// <summary>
    /// An object representation of the SkyStemArt AccountHdr table
    /// </summary>
    [Serializable]
    [DataContract]
    public class UserLockdownDetailInfo
    {
        public int? UserID { get; set; }
        public DateTime? LockdownDateTime { get; set; }
        public DateTime? ResetDateTime { get; set; }

    }
}
