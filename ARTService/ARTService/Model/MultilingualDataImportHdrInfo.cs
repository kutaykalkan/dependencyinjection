using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SkyStem.ART.Shared.Utility;

namespace SkyStem.ART.Service.Model
{
    public class MultilingualDataImportHdrInfo: DataImportHdrInfo
    {
        public int FromLanguageID { get; set; }
        public int ToLanguageID { get; set; }

        public MultilingualDataImportHdrInfo()
            : base()
        {
            this.CompanyID = SharedAppSettingHelper.GetDefaultBusinessEntityID();
        }
    }
}
