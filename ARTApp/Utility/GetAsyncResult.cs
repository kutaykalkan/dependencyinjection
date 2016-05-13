using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SkyStem.ART.Client.Model.BulkExportExcel;
using SkyStem.ART.Client.Model;

namespace SkyStem.ART.App.ServerSideIAsyncResultOperation
{
	public class GetAsyncResult : AsyncResult
	{
		private double m_Value;
		private double m_Result;
        private BulkExportToExcelInfo exportExcelInfo_Value;
        private bool exportExcelInfo_Result;
        private AccountSearchCriteria accountSearchInfo_Value;
        private List<AccountHdrInfo> accountSearchInfo_Result;
        private AppUserInfo AppUserInfo_Value;

		public double Value
		{
			get { return m_Value; }
			set { m_Value = value; }
		}

		public double Result
		{
			get { return m_Result; }
			set { m_Result = value; }
		}



        public BulkExportToExcelInfo ExportExcelInfoValue
        {
            get { return exportExcelInfo_Value; }
            set { exportExcelInfo_Value = value; }
        }

        public bool ExportExcelInfoResult
        {
            get { return exportExcelInfo_Result; }
            set { exportExcelInfo_Result = value; }
        }

        public AccountSearchCriteria AccountHdrInfoValue
        {
            get { return accountSearchInfo_Value; }
            set { accountSearchInfo_Value = value; }
        }

        public List<AccountHdrInfo> AccountHdrInfoResult
        {
            get { return accountSearchInfo_Result; }
            set { accountSearchInfo_Result = value; }
        }

        public GetAsyncResult(
			AsyncCallback callback,
			object state) : base (callback, state)
		{
		}
        public AppUserInfo AppUserInfoValue
        {
            get { return AppUserInfo_Value; }
            set { AppUserInfo_Value = value; }
        }
	}
}
