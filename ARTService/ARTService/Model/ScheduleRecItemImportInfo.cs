using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClientModel = SkyStem.ART.Client.Model;
namespace SkyStem.ART.Service.Model
{
    public class ScheduleRecItemImportInfo: DataImportHdrInfo
    {
        public Int64? GLDataID { get; set; }
        public Int16? ReconciliationCategoryID { get; set; }
        public Int16? ReconciliationCategoryTypeID { get; set; }
        public string ReportingCurrencyCode { get; set; }
        public string BaseCurrencyCode { get; set; }
        public List<ClientModel.GLDataRecurringItemScheduleIntervalDetailInfo> GLDataRecurringItemScheduleIntervalDetailInfoList { get; set; }
        public List<ClientModel.ExchangeRateInfo> ExchangeRateInfoList { get; set; }
    }
}
