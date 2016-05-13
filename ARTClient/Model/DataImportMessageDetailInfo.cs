
using System;
using Adapdev.Text;
using SkyStem.ART.Client.Model.Base;
using System.Runtime.Serialization;

namespace SkyStem.ART.Client.Model
{    

	/// <summary>
    /// An object representation of the SkyStemArt DataImportMessageDetail table
	/// </summary>
	[Serializable]
	[DataContract]
    public class DataImportMessageDetailInfo : DataImportMessageDetailInfoBase
	{
        [DataMember]
        public System.Int32? DescriptionLabelID { get; set; }

        [DataMember]
        public System.String Description { get; set; }

        [DataMember]
        public System.Int16? DataImportMessageCategoryID { get; set; }

        [DataMember]
        public AccountHdrInfo AccountInfo { get; set; }
	}
}
