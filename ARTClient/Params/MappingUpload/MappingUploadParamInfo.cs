using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SkyStem.ART.Client.Model.MappingUpload;
using System.Runtime.Serialization;

namespace SkyStem.ART.Client.Params.MappingUpload
{
    [DataContract]
    public class MappingUploadParamInfo:ParamInfoBase
    {
        [DataMember]
        public List<MappingUploadInfo> MappingUploadInfoList { get; set; }

        [DataMember]
        public List<MappingUploadInfo> GLDataMappingUploadInfoList { get; set; }

        [DataMember]
        public long? GLDataID { get; set; }

        [DataMember]
        public bool? EnabledOnly { get; set; }
    }
}
