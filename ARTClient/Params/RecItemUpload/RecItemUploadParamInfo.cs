using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace SkyStem.ART.Client.Params.RecItemUpload
{
    [DataContract]
    public class RecItemUploadParamInfo: ParamInfoBase
    {
        [DataMember]
        public long? GLDataID { get; set; }

        [DataMember]
        public short? RecCategoryID { get; set; }
        
        [DataMember]
        public short? RecCategoryTypeID { get; set; }
    }
}
