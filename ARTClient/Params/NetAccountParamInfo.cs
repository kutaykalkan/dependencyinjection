using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SkyStem.ART.Client.Model;

namespace SkyStem.ART.Client.Params
{
    public class NetAccountParamInfo: ParamInfoBase
    {
        public List<AccountHdrInfo> AccountHdrInfoList { get; set; }
        public int? NetAccountID { get; set; }
        public DateTime? RecPeriodEndDate { get; set; }
        public decimal? MinSumThreshold {get; set;}
        public decimal? MaxSumThreshold { get; set; }
        public int? NetAccountAttributeID { get; set; }
        public short ActionTypeID { get; set; }
        public short ChangeSourceIDSRA { get; set; }
    }
}
