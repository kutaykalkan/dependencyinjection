using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace SkyStem.ART.Client.Params
{
    public class DataImportParamInfo : ParamInfoBase
    {
        [DataMember]
        public int? DataImportID { get; set; }

        [DataMember]
        public short? DataImportTypeID { get; set; }

        [DataMember]
        public long? GLDataID { get; set; }

        [DataMember]
        public long? TaskID { get; set; }
    }
}
