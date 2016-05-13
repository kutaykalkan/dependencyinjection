
using System;
using Adapdev.Text;
using SkyStem.ART.Client.Model.Base;
using System.Runtime.Serialization;

namespace SkyStem.ART.Client.Model
{

    /// <summary>
    /// An object representation of the SkyStemArt ReportColumn table
    /// </summary>
    [Serializable]
    [DataContract]
    public class ReportColumnInfo : ReportColumnInfoBase
    {
        [DataMember]
        public string ColumnName { get; set; }

        [DataMember]
        public string ColumnUniqueName { get; set; }

        [DataMember]
        public Int16? FeatureID { get; set; }

        [DataMember]
        public Int16? CapabilityID { get; set; }

        [DataMember]
        public Int16? ColumnTypeID { get; set; }


    }
}
