

using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using Adapdev.Text;
using System.Runtime.Serialization;

namespace SkyStem.ART.Client.Model.Base
{

    /// <summary>
    /// An object representation of the SkyStemArt DataImportMessageDetailDetail table
    /// </summary>
    [Serializable]
    [DataContract]
    public abstract class DataImportMessageDetailInfoBase
    {
        [DataMember]
        public System.Int64? DataImportMessageDetailID { get; set; }

        [DataMember]
        public System.Int32? DataImportID { get; set; }

        [DataMember]
        public System.Int16? DataImportMessageID { get; set; }

        [DataMember]
        public System.Int16? DataImportMessageTypeID { get; set; }

        [DataMember]
        public System.Int32? DataImportMessageLabelID { get; set; }

        [DataMember]
        public System.String DataImportMessage { get; set; }

        [DataMember]
        public System.Int32? ExcelRowNumber { get; set; }

        [DataMember]
        public System.String MessageSchema { get; set; }

        [DataMember]
        public System.String MessageData { get; set; }

        [DataMember]
        public System.Boolean? IsActive { get; set; }

        [DataMember]
        public System.DateTime? DateAdded { get; set; }

        /// <summary>
        /// Returns a string representation of the object, displaying all property and field names and values.
        /// </summary>
        public override string ToString()
        {
            return StringUtil.ToString(this);
        }
    }
}
