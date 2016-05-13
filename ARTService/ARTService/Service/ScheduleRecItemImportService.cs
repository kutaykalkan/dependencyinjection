using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SkyStem.ART.Client.Model.CompanyDatabase;
using SkyStem.ART.Service.Utility;
using SkyStem.ART.Service.APP.BLL;
using System.ComponentModel;
using System.Threading;

namespace SkyStem.ART.Service.Service
{
    public class ScheduleRecItemImportService
    {
        List<BackgroundWorker> _WorkerList = null;
        int _ParallelProcessingTimeOut = 600;
        bool _EnableParallelProcessing = true;
        public ScheduleRecItemImportService()
        {
            _WorkerList = new List<BackgroundWorker>();
            _EnableParallelProcessing = Convert.ToBoolean(Helper.GetAppSettingFromKey("EnableParallelProcessing"));
            _ParallelProcessingTimeOut = Convert.ToInt32(Helper.GetAppSettingFromKey("ParallelProcessingTimeOut"));
        }

        public void ProcessScheduleRecItemImports()
        {
            try
            {
                Dictionary<string, CompanyUserInfo> oDictConnectionString = CacheHelper.GetDistinctDatabaseList();

                foreach (CompanyUserInfo oCompanyUserInfo in oDictConnectionString.Values)
                {
                    string s1 = oCompanyUserInfo.DatabaseName;
                    ScheduleRecItemImport oScheduleRecItemImport = new ScheduleRecItemImport(oCompanyUserInfo);
                    if (oScheduleRecItemImport.IsProcessingRequiredForScheduleRecItemImport())
                    {
                        if (_EnableParallelProcessing)
                        {
                            BackgroundWorker oWorker = new BackgroundWorker();
                            oWorker.WorkerSupportsCancellation = true;
                            oWorker.DoWork += new DoWorkEventHandler(oWorker_DoWork);
                            oWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(oWorker_RunWorkerCompleted);
                            _WorkerList.Add(oWorker);
                            oWorker.RunWorkerAsync(oScheduleRecItemImport);
                        }
                        else
                            oScheduleRecItemImport.ProcessScheduleRecItemImport();
                    }
                }
                if (_EnableParallelProcessing)
                {
                    int timeOut = _ParallelProcessingTimeOut;
                    while (_WorkerList.Count > 0 && timeOut > 0)
                    {
                        Thread.Sleep(1000);
                        timeOut--;
                    }
                    if (timeOut == 0)
                    {
                        _WorkerList.ForEach(T => T.CancelAsync());
                        throw new TimeoutException();
                    }
                }
            }
            catch (Exception ex)
            {
                Helper.LogError(ex, null);
            }
            finally
            {
                _WorkerList.Clear();
            }
        }

        void oWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            _WorkerList.Remove((BackgroundWorker)sender);
            ScheduleRecItemImport oScheduleRecItemImport = e.Result as ScheduleRecItemImport;
            CompanyUserInfo oCompanyUserInfo = null;
            if (oScheduleRecItemImport != null)
                oCompanyUserInfo = oScheduleRecItemImport.CompanyUserInfo;
            if (e.Error != null)
                Helper.LogError(e.Error, oCompanyUserInfo);
        }

        void oWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            if (!e.Cancel)
            {
                ScheduleRecItemImport oScheduleRecItemImport = (ScheduleRecItemImport)e.Argument;
                oScheduleRecItemImport.ProcessScheduleRecItemImport();
                e.Result = oScheduleRecItemImport;
            }
        }
    }
}
