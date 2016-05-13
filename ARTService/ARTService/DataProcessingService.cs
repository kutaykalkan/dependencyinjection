using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Configuration;
using System.IO;
using System.Timers;
using System.Data.Sql;
using System.Data.SqlClient;
using SkyStem.ART.Service.Utility;
using SkyStem.ART.Service.Data;
using SkyStem.ART.Service.Log;
using SkyStem.ART.Service.APP.BLL;
using System.Xml;
using SkyStem.ART.Service.Model;
using SkyStem.ART.Client.Model.CompanyDatabase;
using SkyStem.ART.Service.Service;
using System.Threading.Tasks;
using System.Collections.Concurrent;

namespace SkyStem.ART.Service
{
    public partial class DataProcessingService : ServiceBase
    {
        bool _IsServiceStopping;

        int _DataUploadInterval;
        bool _IsProcessingDataUpload;
        Timer oDataUploadTimer = new Timer();

        int _RecPeriodTimerInterval;
        bool _IsProcessingRecPeriod;
        Timer oRecPeriodStatusTimer = new Timer();

        int _AlertTimerInterval;
        bool _IsProcessingAlert;
        Timer oAlertTimer = new Timer();

        int _MatchingFileTimerInterval;
        bool _IsProcessingMatchingFile;
        Timer oMatchingFileTimer = new Timer();

        int _MatchingEngineTimesInterval;
        bool _IsProcessingMatchingEngine;
        Timer oMatchingEngineTimer = new Timer();

        int _MultilingualUploadTimerInterval;
        bool _IsProcessingMultilingualUpload;
        Timer oMultilingualUploadTimer = new Timer();

        int _ExportToExcelTimerInterval;
        bool _IsProcessingExportToExcel;
        Timer oExportToExcelTimer = new Timer();

        int _UserUploadTimerInterval;
        bool _IsProcessingUserUpload;
        Timer oUserUploadTimer = new Timer();

        int _CompanyCreationTimerInterval;
        bool _IsProcessingCompanyCreation;
        Timer oCompanyCreationTimer = new Timer();

        int _AccountReconcilabilityTimerInterval;
        bool _IsProcessingAccountReconcilability;
        Timer oAccountReconcilabilityTimer = new Timer();

        int _TaskUploadTimerInterval;
        bool _IsProcessingTaskUpload;
        Timer oTaskUploadTimer = new Timer();

        int _IndexRecreationServiceTimerInterval;
        bool _IsProcessingIndexCreation;
        Timer oIndexRecreationServiceTimer = new Timer();

        int _RecItemImportTimerInterval;
        bool _IsProcessingRecItemImport;
        Timer oRecItemImportTimer = new Timer();

        int _ClearCompanyCacheTimerInterval;
        bool _IsProcessingClearCompanyCache;
        Timer oClearCompanyCacheTimer = new Timer();

        int _FTPDataImportTimerInterval;
        bool _IsProcessingFTPDataImport;
        Timer oFTPDataImportTimer = new Timer();

        int _ThreadCheckTimerInterval;
        bool _IsProcessingThreadCheck;
        Timer oThreadCheckTimer = new Timer();
        Dictionary<string, Timer> _dictTimerList = new Dictionary<string, Timer>();
        Dictionary<string, DateTime> _dictLastExecutionList = new Dictionary<string, DateTime>();

        public DataProcessingService()
        {
            InitializeComponent();

            // Data Processing Timer
            Helper.LogInfo(" ", null);
            Helper.LogInfo(string.Format("==================== SERVICE START: {0} =====================", Helper.GetDateTime()), null);
            Helper.LogInfo(string.Format(LoggingConstants.SERVICE_INITIALIZE_TEXT, ServiceConstants.SERVICE_NAME_DATA_PROCESSING, Helper.GetDateTime()), null);
            oDataUploadTimer = new System.Timers.Timer();
            oDataUploadTimer.AutoReset = true;
            oDataUploadTimer.Enabled = true;
            oDataUploadTimer.Elapsed += new ElapsedEventHandler(oDataUploadTimer_Elapsed);

            // Rec Period Status Processing Timer
            Helper.LogInfo(string.Format(LoggingConstants.SERVICE_INITIALIZE_TEXT, ServiceConstants.SERVICE_NAME_REC_PERIOD, Helper.GetDateTime()), null);
            oRecPeriodStatusTimer = new System.Timers.Timer();
            oRecPeriodStatusTimer.AutoReset = true;
            oRecPeriodStatusTimer.Enabled = true;
            oRecPeriodStatusTimer.Elapsed += new ElapsedEventHandler(oRecPeriodStatusTimer_Elapsed);

            // Alert Service Processing Timer
            Helper.LogInfo(string.Format(LoggingConstants.SERVICE_INITIALIZE_TEXT, ServiceConstants.SERVICE_NAME_ALERT_PROCESSING, Helper.GetDateTime()), null);
            _AlertTimerInterval = Helper.GetTimerInterval(AppSettingConstants.TIMER_INTERVAL_ALERT_PROCESSING);
            oAlertTimer = new System.Timers.Timer();
            oAlertTimer.AutoReset = true;
            oAlertTimer.Enabled = true;
            oAlertTimer.Elapsed += new ElapsedEventHandler(oAlertTimer_Elapsed);

            // Matching files Processing Timer
            Helper.LogInfo(string.Format(LoggingConstants.SERVICE_INITIALIZE_TEXT, ServiceConstants.SERVICE_NAME_MATCHING_FILE_PROCESSING, Helper.GetDateTime()), null);
            _MatchingFileTimerInterval = Helper.GetTimerInterval(AppSettingConstants.TIMER_INTERVAL_MATCHING_FILE_PROCESSING);
            oMatchingFileTimer = new System.Timers.Timer();
            oMatchingFileTimer.AutoReset = true;
            oMatchingFileTimer.Enabled = true;
            oMatchingFileTimer.Elapsed += new ElapsedEventHandler(oMatchingFileTimer_Elapsed);

            // Matching engine Processing Timer
            Helper.LogInfo(string.Format(LoggingConstants.SERVICE_INITIALIZE_TEXT, ServiceConstants.SERVICE_NAME_MATCHING_ENGINE_PROCESSING, Helper.GetDateTime()), null);
            _MatchingEngineTimesInterval = Helper.GetTimerInterval(AppSettingConstants.TIMER_INTERVAL_MATCHING_ENGINE_PROCESSING);
            oMatchingEngineTimer = new System.Timers.Timer();
            oMatchingEngineTimer.AutoReset = true;
            oMatchingEngineTimer.Enabled = true;
            oMatchingEngineTimer.Elapsed += new ElapsedEventHandler(oMatchingEngineTimer_Elapsed);

            // Multilingual Upload Processing Timer
            Helper.LogInfo(string.Format(LoggingConstants.SERVICE_INITIALIZE_TEXT, ServiceConstants.SERVICE_NAME_MULTILINGUAL_UPLOAD_PROCESSING, Helper.GetDateTime()), null);
            _MultilingualUploadTimerInterval = Helper.GetTimerInterval(AppSettingConstants.TIMER_INTERVAL_MULTILINGUAL_UPLOAD_PROCESSING);
            oMultilingualUploadTimer = new System.Timers.Timer();
            oMultilingualUploadTimer.AutoReset = true;
            oMultilingualUploadTimer.Enabled = true;
            oMultilingualUploadTimer.Elapsed += new ElapsedEventHandler(oMultilingualUploadTimer_Elapsed);

            //Export To Excel Processing Timer
            Helper.LogInfo(string.Format(LoggingConstants.SERVICE_INITIALIZE_TEXT, ServiceConstants.SERVICE_NAME_EXPORTTOEXCEL_PROCESSING, Helper.GetDateTime()), null);
            _ExportToExcelTimerInterval = Helper.GetTimerInterval(AppSettingConstants.TIMER_INTERVAL_EXPORTTOEXCEL_PROCESSING);
            oExportToExcelTimer = new System.Timers.Timer();
            oExportToExcelTimer.AutoReset = true;
            oExportToExcelTimer.Enabled = true;
            oExportToExcelTimer.Elapsed += new ElapsedEventHandler(oExportToExcelTimer_Elapsed);

            //User Upload Processing Timer
            Helper.LogInfo(string.Format(LoggingConstants.SERVICE_INITIALIZE_TEXT, ServiceConstants.SERVICE_NAME_USERUPLOAD_PROCESSING, Helper.GetDateTime()), null);
            _UserUploadTimerInterval = Helper.GetTimerInterval(AppSettingConstants.TIMER_INTERVAL_USERUPLOAD_PROCESSING);
            oUserUploadTimer = new System.Timers.Timer();
            oUserUploadTimer.AutoReset = true;
            oUserUploadTimer.Enabled = true;
            oUserUploadTimer.Elapsed += new ElapsedEventHandler(oUserUploadTimer_Elapsed);

            //Company Creation Timer
            Helper.LogInfo(string.Format(LoggingConstants.SERVICE_INITIALIZE_TEXT, ServiceConstants.SERVICE_NAME_COMPANY_CREATION_PROCESSING, Helper.GetDateTime()), null);
            _CompanyCreationTimerInterval = Helper.GetTimerInterval(AppSettingConstants.TIMER_INTERVAL_COMPANY_CREATION_PROCESSING);
            oCompanyCreationTimer = new System.Timers.Timer();
            oCompanyCreationTimer.AutoReset = true;
            oCompanyCreationTimer.Enabled = true;
            oCompanyCreationTimer.Elapsed += new ElapsedEventHandler(oCompanyCreationTimer_Elapsed);

            //Reprocess Account Reconcilability Timer
            Helper.LogInfo(string.Format(LoggingConstants.SERVICE_INITIALIZE_TEXT, ServiceConstants.SERVICE_NAME_ACCOUNT_RECONCILABILITY_PROCESSING, Helper.GetDateTime()), null);
            _AccountReconcilabilityTimerInterval = Helper.GetTimerInterval(AppSettingConstants.TIMER_INTERVAL_ACCOUNT_RECONCILABILITY_PROCESSING);
            oAccountReconcilabilityTimer = new System.Timers.Timer();
            oAccountReconcilabilityTimer.AutoReset = true;
            oAccountReconcilabilityTimer.Enabled = true;
            oAccountReconcilabilityTimer.Elapsed += new ElapsedEventHandler(oAccountReconcilabilityTimer_Elapsed);

            // Task Upload Processing Timer
            Helper.LogInfo(string.Format(LoggingConstants.SERVICE_INITIALIZE_TEXT, ServiceConstants.SERVICE_NAME_TASKUPLOAD_PROCESSING, Helper.GetDateTime()), null);
            _TaskUploadTimerInterval = Helper.GetTimerInterval(AppSettingConstants.TIMER_INTERVAL_TASKUPLOAD_PROCESSING);
            oTaskUploadTimer = new System.Timers.Timer();
            oTaskUploadTimer.AutoReset = true;
            oTaskUploadTimer.Enabled = true;
            oTaskUploadTimer.Elapsed += new ElapsedEventHandler(oTaskUploadTimer_Elapsed);

            //index recreation service timer
            Helper.LogInfo(string.Format(LoggingConstants.SERVICE_INITIALIZE_TEXT, ServiceConstants.SERVICE_NAME_INDEX_RECREATION_SERVICE, Helper.GetDateTime()), null);
            _IndexRecreationServiceTimerInterval = Helper.GetTimerInterval(AppSettingConstants.TIMER_INTERVAL_INDEX_RECREATION_SERVICE_PROCESSING);
            oIndexRecreationServiceTimer = new System.Timers.Timer();
            oIndexRecreationServiceTimer.AutoReset = true;
            oIndexRecreationServiceTimer.Enabled = true;
            oIndexRecreationServiceTimer.Elapsed += new ElapsedEventHandler(oIndexRecreationServiceTimer_Elapsed);

            Helper.LogInfo(string.Format(LoggingConstants.SERVICE_INITIALIZE_TEXT, ServiceConstants.SERVICE_NAME_REC_ITEM_IMPORT_SERVICE, Helper.GetDateTime()), null);
            _RecPeriodTimerInterval = Helper.GetTimerInterval(AppSettingConstants.TIMER_INTERVAL_REC_ITEM_IMPORT_PROCESSING);
            oRecItemImportTimer = new System.Timers.Timer();
            oRecItemImportTimer.AutoReset = true;
            oRecItemImportTimer.Enabled = true;
            oRecItemImportTimer.Elapsed += new ElapsedEventHandler(oRecItemImportTimer_Elapsed);

            Helper.LogInfo(string.Format(LoggingConstants.SERVICE_INITIALIZE_TEXT, ServiceConstants.SERVICE_NAME_CLEAR_COMPANY_CACHE_SERVICE, Helper.GetDateTime()), null);
            _ClearCompanyCacheTimerInterval = Helper.GetTimerInterval(AppSettingConstants.TIMER_INTERVAL_CLEAR_COMPANY_CACHE_PROCESSING);
            oClearCompanyCacheTimer = new System.Timers.Timer();
            oClearCompanyCacheTimer.AutoReset = true;
            oClearCompanyCacheTimer.Enabled = true;
            oClearCompanyCacheTimer.Elapsed += new ElapsedEventHandler(oClearCompanyCacheTimer_Elapsed);

            //FTPDataImport Processing Timer
            Helper.LogInfo(string.Format(LoggingConstants.SERVICE_INITIALIZE_TEXT, ServiceConstants.SERVICE_NAME_FTPDATAIMPORT_PROCESSING, Helper.GetDateTime()), null);
            _FTPDataImportTimerInterval = Helper.GetTimerInterval(AppSettingConstants.TIMER_INTERVAL_FTPDATAIMPORT_PROCESSING);
            oFTPDataImportTimer = new System.Timers.Timer();
            oFTPDataImportTimer.AutoReset = true;
            oFTPDataImportTimer.Enabled = true;
            oFTPDataImportTimer.Elapsed += new ElapsedEventHandler(oFTPDataImportTimer_Elapsed);

            Helper.LogInfo(string.Format(LoggingConstants.SERVICE_INITIALIZE_TEXT, ServiceConstants.SERVICE_NAME_THREAD_CHECK_PROCESSING, Helper.GetDateTime()), null);
            _ThreadCheckTimerInterval = Helper.GetTimerInterval(AppSettingConstants.TIMER_INTERVAL_THREAD_CHECK_PROCESSING);
            oThreadCheckTimer = new System.Timers.Timer();
            oThreadCheckTimer.AutoReset = true;
            oThreadCheckTimer.Enabled = true;
            oThreadCheckTimer.Elapsed += new ElapsedEventHandler(oThreadCheckTimer_Elapsed);
        }

        #region "Service Events"
        protected override void OnStart(string[] args)
        {
            _IsServiceStopping = false;
            Helper.LogInfo("Service Started", null);
            Helper.LogInfo(string.Format(LoggingConstants.SERVICE_START_TEXT, ServiceConstants.SERVICE_NAME_DATA_PROCESSING, Helper.GetDateTime()), null);
            // Set a Initial Timer for 1 min
            oDataUploadTimer.Interval = 60 * 1000;
            oDataUploadTimer.Start();

            Helper.LogInfo(string.Format(LoggingConstants.SERVICE_START_TEXT, ServiceConstants.SERVICE_NAME_REC_PERIOD, Helper.GetDateTime()), null);
            oRecPeriodStatusTimer.Interval = 60 * 1000;
            oRecPeriodStatusTimer.Start();

            Helper.LogInfo(string.Format(LoggingConstants.SERVICE_START_TEXT, ServiceConstants.SERVICE_NAME_ALERT_PROCESSING, Helper.GetDateTime()), null);
            oAlertTimer.Interval = 60 * 1000;
            oAlertTimer.Start();

            Helper.LogInfo(string.Format(LoggingConstants.SERVICE_START_TEXT, ServiceConstants.SERVICE_NAME_MATCHING_FILE_PROCESSING, Helper.GetDateTime()), null);
            oMatchingFileTimer.Interval = 60 * 1000;
            oMatchingFileTimer.Start();

            Helper.LogInfo(string.Format(LoggingConstants.SERVICE_START_TEXT, ServiceConstants.SERVICE_NAME_MATCHING_ENGINE_PROCESSING, Helper.GetDateTime()), null);
            oMatchingEngineTimer.Interval = 60 * 1000;
            oMatchingEngineTimer.Start();

            Helper.LogInfo(string.Format(LoggingConstants.SERVICE_START_TEXT, ServiceConstants.SERVICE_NAME_MULTILINGUAL_UPLOAD_PROCESSING, Helper.GetDateTime()), null);
            oMultilingualUploadTimer.Interval = 60 * 1000;
            oMultilingualUploadTimer.Start();

            Helper.LogInfo(string.Format(LoggingConstants.SERVICE_START_TEXT, ServiceConstants.SERVICE_NAME_EXPORTTOEXCEL_PROCESSING, Helper.GetDateTime()), null);
            oExportToExcelTimer.Interval = 60 * 1000;
            oExportToExcelTimer.Start();

            Helper.LogInfo(string.Format(LoggingConstants.SERVICE_START_TEXT, ServiceConstants.SERVICE_NAME_USERUPLOAD_PROCESSING, Helper.GetDateTime()), null);
            oUserUploadTimer.Interval = 60 * 1000;
            oUserUploadTimer.Start();

            Helper.LogInfo(string.Format(LoggingConstants.SERVICE_START_TEXT, ServiceConstants.SERVICE_NAME_COMPANY_CREATION_PROCESSING, Helper.GetDateTime()), null);
            oCompanyCreationTimer.Interval = 60 * 1000;
            oCompanyCreationTimer.Start();

            Helper.LogInfo(string.Format(LoggingConstants.SERVICE_START_TEXT, ServiceConstants.SERVICE_NAME_ACCOUNT_RECONCILABILITY_PROCESSING, Helper.GetDateTime()), null);
            oAccountReconcilabilityTimer.Interval = 60 * 1000;
            oAccountReconcilabilityTimer.Start();

            Helper.LogInfo(string.Format(LoggingConstants.SERVICE_START_TEXT, ServiceConstants.SERVICE_NAME_REC_ITEM_IMPORT_SERVICE, Helper.GetDateTime()), null);
            oRecItemImportTimer.Interval = 60 * 1000;
            oRecItemImportTimer.Start();

            Helper.LogInfo(string.Format(LoggingConstants.SERVICE_START_TEXT, ServiceConstants.SERVICE_NAME_TASKUPLOAD_PROCESSING, Helper.GetDateTime()), null);
            oTaskUploadTimer.Interval = 60 * 1000;
            oTaskUploadTimer.Start();

            Helper.LogInfo(string.Format(LoggingConstants.SERVICE_START_TEXT, ServiceConstants.SERVICE_NAME_CLEAR_COMPANY_CACHE_SERVICE, Helper.GetDateTime()), null);
            oClearCompanyCacheTimer.Interval = 60 * 1000;
            oClearCompanyCacheTimer.Start();

            Helper.LogInfo(string.Format(LoggingConstants.SERVICE_START_TEXT, ServiceConstants.SERVICE_NAME_FTPDATAIMPORT_PROCESSING, Helper.GetDateTime()), null);
            oFTPDataImportTimer.Interval = 60 * 1000;
            oFTPDataImportTimer.Start();

            Helper.LogInfo(string.Format(LoggingConstants.SERVICE_START_TEXT, ServiceConstants.SERVICE_NAME_THREAD_CHECK_PROCESSING, Helper.GetDateTime()), null);
            _ThreadCheckTimerInterval = Helper.GetTimerInterval(AppSettingConstants.TIMER_INTERVAL_THREAD_CHECK_PROCESSING);
            oThreadCheckTimer.Interval = _ThreadCheckTimerInterval * 60 * 1000;
            oThreadCheckTimer.Start();
        }

        protected override void OnStop()
        {
            _IsServiceStopping = true;
            WaitForCurrentProcessing();
            Helper.LogInfo(string.Format(LoggingConstants.SERVICE_STOP_TEXT, ServiceConstants.SERVICE_NAME_DATA_PROCESSING, Helper.GetDateTime()), null);
            oDataUploadTimer.Stop();
            oDataUploadTimer.Enabled = false;

            Helper.LogInfo(string.Format(LoggingConstants.SERVICE_STOP_TEXT, ServiceConstants.SERVICE_NAME_REC_PERIOD, Helper.GetDateTime()), null);
            oRecPeriodStatusTimer.Stop();
            oRecPeriodStatusTimer.Enabled = false;

            Helper.LogInfo(string.Format(LoggingConstants.SERVICE_STOP_TEXT, ServiceConstants.SERVICE_NAME_ALERT_PROCESSING, Helper.GetDateTime()), null);
            oAlertTimer.Stop();
            oAlertTimer.Enabled = false;

            Helper.LogInfo(string.Format(LoggingConstants.SERVICE_STOP_TEXT, ServiceConstants.SERVICE_NAME_MATCHING_FILE_PROCESSING, Helper.GetDateTime()), null);
            oMatchingFileTimer.Stop();
            oMatchingFileTimer.Enabled = false;

            Helper.LogInfo(string.Format(LoggingConstants.SERVICE_STOP_TEXT, ServiceConstants.SERVICE_NAME_MATCHING_ENGINE_PROCESSING, Helper.GetDateTime()), null);
            oMatchingEngineTimer.Stop();
            oMatchingEngineTimer.Enabled = false;

            Helper.LogInfo(string.Format(LoggingConstants.SERVICE_STOP_TEXT, ServiceConstants.SERVICE_NAME_MULTILINGUAL_UPLOAD_PROCESSING, Helper.GetDateTime()), null);
            oMultilingualUploadTimer.Stop();
            oMultilingualUploadTimer.Enabled = false;

            Helper.LogInfo(string.Format(LoggingConstants.SERVICE_STOP_TEXT, ServiceConstants.SERVICE_NAME_EXPORTTOEXCEL_PROCESSING, Helper.GetDateTime()), null);
            oExportToExcelTimer.Stop();
            oExportToExcelTimer.Enabled = false;

            Helper.LogInfo(string.Format(LoggingConstants.SERVICE_STOP_TEXT, ServiceConstants.SERVICE_NAME_USERUPLOAD_PROCESSING, Helper.GetDateTime()), null);
            oUserUploadTimer.Stop();
            oUserUploadTimer.Enabled = false;

            Helper.LogInfo(string.Format(LoggingConstants.SERVICE_STOP_TEXT, ServiceConstants.SERVICE_NAME_TASKUPLOAD_PROCESSING, Helper.GetDateTime()), null);
            oTaskUploadTimer.Stop();
            oTaskUploadTimer.Enabled = false;

            Helper.LogInfo(string.Format(LoggingConstants.SERVICE_STOP_TEXT, ServiceConstants.SERVICE_NAME_COMPANY_CREATION_PROCESSING, Helper.GetDateTime()), null);
            oCompanyCreationTimer.Stop();
            oCompanyCreationTimer.Enabled = false;

            Helper.LogInfo(string.Format(LoggingConstants.SERVICE_STOP_TEXT, ServiceConstants.SERVICE_NAME_ACCOUNT_RECONCILABILITY_PROCESSING, Helper.GetDateTime()), null);
            oAccountReconcilabilityTimer.Stop();
            oAccountReconcilabilityTimer.Enabled = false;

            Helper.LogInfo(string.Format(LoggingConstants.SERVICE_STOP_TEXT, ServiceConstants.SERVICE_NAME_REC_ITEM_IMPORT_SERVICE, Helper.GetDateTime()), null);
            oRecItemImportTimer.Stop();
            oRecItemImportTimer.Enabled = false;

            Helper.LogInfo(string.Format(LoggingConstants.SERVICE_STOP_TEXT, ServiceConstants.SERVICE_NAME_CLEAR_COMPANY_CACHE_SERVICE, Helper.GetDateTime()), null);
            oClearCompanyCacheTimer.Stop();
            oClearCompanyCacheTimer.Enabled = false;

            Helper.LogInfo(string.Format(LoggingConstants.SERVICE_STOP_TEXT, ServiceConstants.SERVICE_NAME_FTPDATAIMPORT_PROCESSING, Helper.GetDateTime()), null);
            oFTPDataImportTimer.Stop();
            oFTPDataImportTimer.Enabled = false;

            Helper.LogInfo(string.Format(LoggingConstants.SERVICE_STOP_TEXT, ServiceConstants.SERVICE_NAME_THREAD_CHECK_PROCESSING, Helper.GetDateTime()), null);
            oThreadCheckTimer.Stop();
            oThreadCheckTimer.Enabled = false;

            Helper.LogInfo(string.Format("==================== SERVICE STOP: {0} =====================", Helper.GetDateTime()), null);

        }

        protected override void OnShutdown()
        {
            // Being added to track System Shutdown
            _IsServiceStopping = true;
            WaitForCurrentProcessing();
            Helper.LogInfo(string.Format("==================== SYSTEM SHUTDOWN: {0} =====================", Helper.GetDateTime()), null);
            base.OnShutdown();
        }

        private void WaitForCurrentProcessing()
        {
            int count = 15;
            while (count > 0 && (_IsProcessingAccountReconcilability || _IsProcessingAlert || _IsProcessingCompanyCreation
                || _IsProcessingDataUpload || _IsProcessingExportToExcel || _IsProcessingIndexCreation
                || _IsProcessingMatchingEngine || _IsProcessingMatchingFile || _IsProcessingMultilingualUpload
                || _IsProcessingRecItemImport || _IsProcessingRecPeriod || _IsProcessingTaskUpload
                || _IsProcessingUserUpload || _IsProcessingClearCompanyCache || _IsProcessingFTPDataImport
                || _IsProcessingThreadCheck))
            {
                count--;
                Helper.LogServiceTimeStampInfo("Waiting for running services...");
                System.Threading.Thread.Sleep(10000);
            }
            if (count == 0)
                Helper.LogServiceTimeStampInfo("Stopping Timeout expired...");
        }

        #endregion

        #region "Timer Elapsed Events for all Timers"

        void oDataUploadTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (!_IsProcessingAlert && !_IsServiceStopping && !_IsProcessingFTPDataImport)
            {
                try
                {
                    _IsProcessingDataUpload = true;
                    UpdateLastExecutionTime(ServiceConstants.SERVICE_NAME_DATA_PROCESSING, oDataUploadTimer);
                    oDataUploadTimer.Enabled = false;
                    DateTime startDate = DateTime.Now;
                    Helper.LogInfo(string.Format(LoggingConstants.SERVICE_PROCESSING_BEGINS_TEXT, ServiceConstants.SERVICE_NAME_DATA_PROCESSING, Helper.GetDateTime()), null);
                    //DataProcessing.ProcessGLData();
                    DataProcessingControlBoard.DataProcessingControl();
                    Helper.LogInfo(string.Format(LoggingConstants.SERVICE_PROCESSING_ENDS_TEXT, ServiceConstants.SERVICE_NAME_DATA_PROCESSING, Helper.GetDateTime()), null);
                    // Setting the Interval again
                    _DataUploadInterval = Helper.GetTimerInterval(AppSettingConstants.TIMER_INTERVAL_DATA_PROCESSING);
                    Helper.LogInfo(string.Format("Data Processing Service Interval == {0}", _DataUploadInterval.ToString()), null);
                    DateTime endDate = DateTime.Now;
                    Helper.LogServiceTimeStampInfo(string.Format(LoggingConstants.SERVICE_TIME_STAMP_TEXT, startDate, endDate, endDate - startDate, ServiceConstants.SERVICE_NAME_DATA_PROCESSING));
                }
                catch (Exception ex)
                {
                    Helper.LogServiceTimeStampError(string.Format(LoggingConstants.SERVICE_THREAD_ERROR_TEXT, ServiceConstants.SERVICE_NAME_DATA_PROCESSING, ex.Message, ex.StackTrace));
                }
                finally
                {
                    oDataUploadTimer.Interval = _DataUploadInterval * 60 * 1000; // Convert to mili secs
                    oDataUploadTimer.Enabled = true;
                    _IsProcessingDataUpload = false;
                    GC.KeepAlive(oDataUploadTimer);
                }
            }
        }

        void oRecPeriodStatusTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (!_IsServiceStopping)
            {
                try
                {
                    _IsProcessingRecPeriod = true;
                    UpdateLastExecutionTime(ServiceConstants.SERVICE_NAME_REC_PERIOD, oRecPeriodStatusTimer);
                    oRecPeriodStatusTimer.Stop();
                    DateTime startDate = DateTime.Now;
                    Helper.LogInfo(string.Format(LoggingConstants.SERVICE_PROCESSING_BEGINS_TEXT, ServiceConstants.SERVICE_NAME_REC_PERIOD, Helper.GetDateTime()), null);
                    
                    Dictionary<string, CompanyUserInfo> oDictConnectionString = CacheHelper.GetDistinctDatabaseList();
                    ConcurrentQueue<Exception> exceptions = new ConcurrentQueue<Exception>();
                    Parallel.ForEach<CompanyUserInfo>(oDictConnectionString.Values, oCompanyUserInfo =>
                    {
                        try
                        {
                        RecPeriod oRP = new RecPeriod(oCompanyUserInfo);
                        oRP.SetRecPeriodStatus();
                        }
                        catch (Exception ex)
                        {
                            exceptions.Enqueue(ex);
                        }
                    });
                    while (exceptions.Count > 0)
                    {
                        Exception exOut;
                        if (exceptions.TryDequeue(out exOut))
                            Helper.LogError(exOut, null);
                    }
                    Helper.LogInfo(string.Format(LoggingConstants.SERVICE_PROCESSING_ENDS_TEXT, ServiceConstants.SERVICE_NAME_REC_PERIOD, Helper.GetDateTime()), null);
                    // Setting the Interval again
                    _RecPeriodTimerInterval = Helper.GetTimerInterval(AppSettingConstants.TIMER_INTERVAL_REC_PERIOD_STATUS_PROCESSING);
                    //Helper.LogInfo(string.Format("Rec Processing Status Service Interval == {0}", _RecPeriodTimerInterval.ToString()), null);
                    DateTime endDate = DateTime.Now;
                    Helper.LogServiceTimeStampInfo(string.Format(LoggingConstants.SERVICE_TIME_STAMP_TEXT, startDate, endDate, endDate - startDate, ServiceConstants.SERVICE_NAME_REC_PERIOD));
                }
                catch (Exception ex)
                {
                    Helper.LogServiceTimeStampError(string.Format(LoggingConstants.SERVICE_THREAD_ERROR_TEXT, ServiceConstants.SERVICE_NAME_REC_PERIOD, ex.Message, ex.StackTrace));
                }
                finally
                {
                    oRecPeriodStatusTimer.Interval = _RecPeriodTimerInterval * 60 * 1000; // Convert to mili secs
                    oRecPeriodStatusTimer.Start();
                    _IsProcessingRecPeriod = false;
                    GC.KeepAlive(oRecItemImportTimer);
                }
            }
        }

        void oAlertTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (!_IsProcessingDataUpload && !_IsServiceStopping && !_IsProcessingFTPDataImport)
            {
                try
                {
                    _IsProcessingAlert = true;
                    UpdateLastExecutionTime(ServiceConstants.SERVICE_NAME_ALERT_PROCESSING, oAlertTimer);
                    oAlertTimer.Stop();
                    DateTime startDate = DateTime.Now;
                    Helper.LogInfo(string.Format(LoggingConstants.SERVICE_PROCESSING_BEGINS_TEXT, ServiceConstants.SERVICE_NAME_ALERT_PROCESSING, Helper.GetDateTime()), null);
                    Dictionary<string, CompanyUserInfo> dicCompanyDatabases = CacheHelper.GetDistinctDatabaseList();
                    ConcurrentQueue<Exception> exceptions = new ConcurrentQueue<Exception>();
                    Parallel.ForEach(dicCompanyDatabases.Values, oCompanyUserInfo =>
                    {
                        try
                        {
                        Alert oAlert = new Alert(oCompanyUserInfo);
                        oAlert.RaiseAlerts();
                        }
                        catch (Exception ex)
                        {
                            exceptions.Enqueue(ex);
                        }
                    });
                    while (exceptions.Count > 0)
                    {
                        Exception exOut;
                        if (exceptions.TryDequeue(out exOut))
                            Helper.LogError(exOut, null);
                    }
                    Helper.LogInfo(string.Format(LoggingConstants.SERVICE_PROCESSING_ENDS_TEXT, ServiceConstants.SERVICE_NAME_ALERT_PROCESSING, Helper.GetDateTime()), null);
                    // Setting the Interval again
                    _AlertTimerInterval = Helper.GetTimerInterval(AppSettingConstants.TIMER_INTERVAL_ALERT_PROCESSING);
                    Helper.LogInfo(string.Format("Alert Service Interval == {0}", _AlertTimerInterval.ToString()), null);
                    DateTime endDate = DateTime.Now;
                    Helper.LogServiceTimeStampInfo(string.Format(LoggingConstants.SERVICE_TIME_STAMP_TEXT, startDate, endDate, endDate - startDate, ServiceConstants.SERVICE_NAME_ALERT_PROCESSING));
                }
                catch (Exception ex)
                {
                    Helper.LogServiceTimeStampError(string.Format(LoggingConstants.SERVICE_THREAD_ERROR_TEXT, ServiceConstants.SERVICE_NAME_ALERT_PROCESSING, ex.Message, ex.StackTrace));
                }
                finally
                {
                    oAlertTimer.Interval = _AlertTimerInterval * 60 * 1000; // Convert to mili secs
                    oAlertTimer.Start();
                    _IsProcessingAlert = false;
                    GC.KeepAlive(oAlertTimer);
                }
            }
        }

        void oMatchingFileTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (!_IsServiceStopping)
            {
                try
                {
                    _IsProcessingMatchingFile = true;
                    UpdateLastExecutionTime(ServiceConstants.SERVICE_NAME_MATCHING_FILE_PROCESSING, oMatchingFileTimer);
                    oMatchingFileTimer.Stop();
                    DateTime startDate = DateTime.Now;
                    Helper.LogInfo(string.Format(LoggingConstants.SERVICE_PROCESSING_BEGINS_TEXT, ServiceConstants.SERVICE_NAME_MATCHING_FILE_PROCESSING, Helper.GetDateTime()), null);
                    Dictionary<string, CompanyUserInfo> oDictConnectionString = CacheHelper.GetDistinctDatabaseList();
                    ConcurrentQueue<Exception> exceptions = new ConcurrentQueue<Exception>();
                    Parallel.ForEach<CompanyUserInfo>(oDictConnectionString.Values, oCompanyUserInfo =>
                    {
                        try
                        {

                            MatchingDataImport oMatchingDataImport = new MatchingDataImport(oCompanyUserInfo);
                            if (oMatchingDataImport.IsProcessingRequiredForMatchingDataImport())
                                oMatchingDataImport.ProcessMatchingDataImport();

                        }
                        catch (Exception ex)
                        {
                            exceptions.Enqueue(ex);
                        }
                    });
                    while (exceptions.Count > 0)
                    {
                        Exception exOut;
                        if (exceptions.TryDequeue(out exOut))
                            Helper.LogError(exOut, null);
                    }

                    Helper.LogInfo(string.Format(LoggingConstants.SERVICE_PROCESSING_ENDS_TEXT, ServiceConstants.SERVICE_NAME_MATCHING_FILE_PROCESSING, Helper.GetDateTime()), null);
                    // Setting the Interval again
                    _MatchingFileTimerInterval = Helper.GetTimerInterval(AppSettingConstants.TIMER_INTERVAL_MATCHING_FILE_PROCESSING);
                    Helper.LogInfo(string.Format("Matching File Service Interval == {0}", _MatchingFileTimerInterval.ToString()), null);
                    DateTime endDate = DateTime.Now;
                    Helper.LogServiceTimeStampInfo(string.Format(LoggingConstants.SERVICE_TIME_STAMP_TEXT, startDate, endDate, endDate - startDate, ServiceConstants.SERVICE_NAME_MATCHING_FILE_PROCESSING));
                }
                catch (Exception ex)
                {
                    Helper.LogServiceTimeStampError(string.Format(LoggingConstants.SERVICE_THREAD_ERROR_TEXT, ServiceConstants.SERVICE_NAME_MATCHING_FILE_PROCESSING, ex.Message, ex.StackTrace));
                }
                finally
                {
                    oMatchingFileTimer.Interval = _MatchingFileTimerInterval * 60 * 1000; // Convert to mili secs
                    oMatchingFileTimer.Start();
                    _IsProcessingMatchingFile = false;
                    GC.KeepAlive(oMatchingFileTimer);
                }
            }
        }

        void oMatchingEngineTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (!_IsServiceStopping)
            {
                try
                {
                    _IsProcessingMatchingEngine = true;
                    UpdateLastExecutionTime(ServiceConstants.SERVICE_NAME_MATCHING_ENGINE_PROCESSING, oMatchingEngineTimer);
                    oMatchingEngineTimer.Stop();

                    DateTime startDate = DateTime.Now;
                    Helper.LogInfo(string.Format(LoggingConstants.SERVICE_PROCESSING_BEGINS_TEXT, ServiceConstants.SERVICE_NAME_MATCHING_ENGINE_PROCESSING, Helper.GetDateTime()), null);
                    Dictionary<string, CompanyUserInfo> oDictConnectionString = CacheHelper.GetDistinctDatabaseList();
                    ConcurrentQueue<Exception> exceptions = new ConcurrentQueue<Exception>();
                    Parallel.ForEach<CompanyUserInfo>(oDictConnectionString.Values, oCompanyUserInfo =>
                    {
                        try
                        {
                            MatchingEngine oMatchingEngine = new MatchingEngine(oCompanyUserInfo);
                            if (oMatchingEngine.IsMatchingRequired())
                                oMatchingEngine.ProcessMatching();
                        }
                        catch (Exception ex)
                        {
                            exceptions.Enqueue(ex);
                        }
                    });
                    while (exceptions.Count > 0)
                    {
                        Exception exOut;
                        if (exceptions.TryDequeue(out exOut))
                            Helper.LogError(exOut, null);
                    }
                    Helper.LogInfo(string.Format(LoggingConstants.SERVICE_PROCESSING_ENDS_TEXT, ServiceConstants.SERVICE_NAME_MATCHING_ENGINE_PROCESSING, Helper.GetDateTime()), null);

                    // Setting the Interval again
                    _MatchingEngineTimesInterval = Helper.GetTimerInterval(AppSettingConstants.TIMER_INTERVAL_MATCHING_ENGINE_PROCESSING);
                    Helper.LogInfo(string.Format("Matching File Service Interval == {0}", _MatchingEngineTimesInterval.ToString()), null);
                    DateTime endDate = DateTime.Now;
                    Helper.LogServiceTimeStampInfo(string.Format(LoggingConstants.SERVICE_TIME_STAMP_TEXT, startDate, endDate, endDate - startDate, ServiceConstants.SERVICE_NAME_MATCHING_ENGINE_PROCESSING));
                }
                catch (Exception ex)
                {
                    Helper.LogServiceTimeStampError(string.Format(LoggingConstants.SERVICE_THREAD_ERROR_TEXT, ServiceConstants.SERVICE_NAME_MATCHING_ENGINE_PROCESSING, ex.Message, ex.StackTrace));
                }
                finally
                {
                    oMatchingEngineTimer.Interval = _MatchingEngineTimesInterval * 60 * 1000; // Convert to mili secs
                    oMatchingEngineTimer.Start();
                    _IsProcessingMatchingEngine = false;
                    GC.KeepAlive(oMatchingEngineTimer);
                }
            }
        }

        void oMultilingualUploadTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (!_IsServiceStopping)
            {
                try
                {
                    _IsProcessingMultilingualUpload = true;
                    UpdateLastExecutionTime(ServiceConstants.SERVICE_NAME_MULTILINGUAL_UPLOAD_PROCESSING, oMultilingualUploadTimer);
                    oMultilingualUploadTimer.Stop();
                    DateTime startDate = DateTime.Now;
                    Helper.LogInfo(string.Format(LoggingConstants.SERVICE_PROCESSING_BEGINS_TEXT, ServiceConstants.SERVICE_NAME_MULTILINGUAL_UPLOAD_PROCESSING, Helper.GetDateTime()), null);
                    Dictionary<string, CompanyUserInfo> oDictConnectionString = CacheHelper.GetDistinctDatabaseList();
                    ConcurrentQueue<Exception> exceptions = new ConcurrentQueue<Exception>();
                    Parallel.ForEach<CompanyUserInfo>(oDictConnectionString.Values, oCompanyUserInfo =>
                    {
                        try
                        {
                            MultilingualDataImport oMultilingualDataImport = new MultilingualDataImport(oCompanyUserInfo);
                            if (oMultilingualDataImport.IsProcessingRequiredForMultilingualDataImport())
                                oMultilingualDataImport.ProcessMultilingualDataImport();
                        }
                        catch (Exception ex)
                        {
                            exceptions.Enqueue(ex);
                        }
                    });
                    while (exceptions.Count > 0)
                    {
                        Exception exOut;
                        if (exceptions.TryDequeue(out exOut))
                            Helper.LogError(exOut, null);
                    }
                    Helper.LogInfo(string.Format(LoggingConstants.SERVICE_PROCESSING_ENDS_TEXT, ServiceConstants.SERVICE_NAME_MULTILINGUAL_UPLOAD_PROCESSING, Helper.GetDateTime()), null);

                    // Setting the Interval again
                    _MultilingualUploadTimerInterval = Helper.GetTimerInterval(AppSettingConstants.TIMER_INTERVAL_MULTILINGUAL_UPLOAD_PROCESSING);
                    Helper.LogInfo(string.Format("Multilingual Upload Service Interval == {0}", _MultilingualUploadTimerInterval.ToString()), null);
                    DateTime endDate = DateTime.Now;
                    Helper.LogServiceTimeStampInfo(string.Format(LoggingConstants.SERVICE_TIME_STAMP_TEXT, startDate, endDate, endDate - startDate, ServiceConstants.SERVICE_NAME_MULTILINGUAL_UPLOAD_PROCESSING));
                }
                catch (Exception ex)
                {
                    Helper.LogServiceTimeStampError(string.Format(LoggingConstants.SERVICE_THREAD_ERROR_TEXT, ServiceConstants.SERVICE_NAME_MULTILINGUAL_UPLOAD_PROCESSING, ex.Message, ex.StackTrace));
                }
                finally
                {
                    oMultilingualUploadTimer.Interval = _MultilingualUploadTimerInterval * 60 * 1000; // Convert to mili secs
                    oMultilingualUploadTimer.Start();
                    _IsProcessingMultilingualUpload = false;
                    GC.KeepAlive(oMultilingualUploadTimer);
                }
            }
        }

        void oExportToExcelTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (!_IsServiceStopping)
            {
                try
                {
                    _IsProcessingExportToExcel = true;
                    UpdateLastExecutionTime(ServiceConstants.SERVICE_NAME_EXPORTTOEXCEL_PROCESSING, oExportToExcelTimer);
                    oExportToExcelTimer.Stop();
                    DateTime startDate = DateTime.Now;
                    Helper.LogInfo(string.Format(LoggingConstants.SERVICE_PROCESSING_BEGINS_TEXT, ServiceConstants.SERVICE_NAME_EXPORTTOEXCEL_PROCESSING, Helper.GetDateTime()), null);
                    Dictionary<string, CompanyUserInfo> oDictConnectionString = CacheHelper.GetDistinctDatabaseList();
                    ConcurrentQueue<Exception> exceptions = new ConcurrentQueue<Exception>();
                    Parallel.ForEach<CompanyUserInfo>(oDictConnectionString.Values, oCompanyUserInfo =>
                    {
                        try
                        {
                            ExportToExcel objExportToExcel = new ExportToExcel(oCompanyUserInfo);

                            if (objExportToExcel.IsProcessingRequiredForRequests())
                            {
                                objExportToExcel.ProcessRequests();
                            }
                        }
                        catch (Exception ex)
                        {
                            exceptions.Enqueue(ex);
                        }
                    });
                    while (exceptions.Count > 0)
                    {
                        Exception exOut;
                        if (exceptions.TryDequeue(out exOut))
                            Helper.LogError(exOut, null);
                    }
                    Helper.LogInfo(string.Format(LoggingConstants.SERVICE_PROCESSING_ENDS_TEXT, ServiceConstants.SERVICE_NAME_EXPORTTOEXCEL_PROCESSING, Helper.GetDateTime()), null);
                    // Setting the Interval again
                    _ExportToExcelTimerInterval = Helper.GetTimerInterval(AppSettingConstants.TIMER_INTERVAL_EXPORTTOEXCEL_PROCESSING);
                    Helper.LogInfo(string.Format("Export to Excel Service Interval == {0}", _ExportToExcelTimerInterval.ToString()), null);
                    DateTime endDate = DateTime.Now;
                    Helper.LogServiceTimeStampInfo(string.Format(LoggingConstants.SERVICE_TIME_STAMP_TEXT, startDate, endDate, endDate - startDate, ServiceConstants.SERVICE_NAME_EXPORTTOEXCEL_PROCESSING));
                }
                catch (Exception ex)
                {
                    Helper.LogServiceTimeStampError(string.Format(LoggingConstants.SERVICE_THREAD_ERROR_TEXT, ServiceConstants.SERVICE_NAME_EXPORTTOEXCEL_PROCESSING, ex.Message, ex.StackTrace));
                }
                finally
                {
                    oExportToExcelTimer.Interval = _ExportToExcelTimerInterval * 60 * 1000; // Convert to mili secs
                    oExportToExcelTimer.Start();
                    _IsProcessingExportToExcel = false;
                    GC.KeepAlive(oExportToExcelTimer);
                }
            }
        }

        void oUserUploadTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (!_IsServiceStopping)
            {
                try
                {
                    _IsProcessingUserUpload = true;
                    UpdateLastExecutionTime(ServiceConstants.SERVICE_NAME_USERUPLOAD_PROCESSING, oUserUploadTimer);
                    oUserUploadTimer.Stop();
                    DateTime startDate = DateTime.Now;
                    Helper.LogInfo(string.Format(LoggingConstants.SERVICE_PROCESSING_BEGINS_TEXT, ServiceConstants.SERVICE_NAME_USERUPLOAD_PROCESSING, Helper.GetDateTime()), null);
                    Dictionary<string, CompanyUserInfo> oDictConnectionString = CacheHelper.GetDistinctDatabaseList();
                    ConcurrentQueue<Exception> exceptions = new ConcurrentQueue<Exception>();
                    Parallel.ForEach<CompanyUserInfo>(oDictConnectionString.Values, oCompanyUserInfo =>
                    {
                        try
                        {
                            UserUpload objUserUpload = new UserUpload(oCompanyUserInfo);
                            if (objUserUpload.IsProcessingRequiredForUserDataImport())
                                objUserUpload.ProcessUserDataImport();
                            //if (objUserUpload.IsProcessingRequiredForUserUploadDataImport())
                            //{
                            //    objUserUpload.ProcessUserUpload();
                            //}
                        }
                        catch (Exception ex)
                        {
                            exceptions.Enqueue(ex);
                        }
                    });
                    while (exceptions.Count > 0)
                    {
                        Exception exOut;
                        if (exceptions.TryDequeue(out exOut))
                            Helper.LogError(exOut, null);
                    }
                    Helper.LogInfo(string.Format(LoggingConstants.SERVICE_PROCESSING_ENDS_TEXT, ServiceConstants.SERVICE_NAME_USERUPLOAD_PROCESSING, Helper.GetDateTime()), null);
                    // Setting the Interval again
                    _UserUploadTimerInterval = Helper.GetTimerInterval(AppSettingConstants.TIMER_INTERVAL_USERUPLOAD_PROCESSING);
                    Helper.LogInfo(string.Format("User Upload Service Interval == {0}", _UserUploadTimerInterval.ToString()), null);
                    DateTime endDate = DateTime.Now;
                    Helper.LogServiceTimeStampInfo(string.Format(LoggingConstants.SERVICE_TIME_STAMP_TEXT, startDate, endDate, endDate - startDate, ServiceConstants.SERVICE_NAME_USERUPLOAD_PROCESSING));
                }
                catch (Exception ex)
                {
                    Helper.LogServiceTimeStampError(string.Format(LoggingConstants.SERVICE_THREAD_ERROR_TEXT, ServiceConstants.SERVICE_NAME_USERUPLOAD_PROCESSING, ex.Message, ex.StackTrace));
                }
                finally
                {
                    oUserUploadTimer.Interval = _UserUploadTimerInterval * 60 * 1000; // Convert to mili secs
                    oUserUploadTimer.Start();
                    _IsProcessingUserUpload = false;
                    GC.KeepAlive(oUserUploadTimer);
                }
            }
        }

        void oCompanyCreationTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (!_IsServiceStopping)
            {
                try
                {
                    _IsProcessingCompanyCreation = true;
                    UpdateLastExecutionTime(ServiceConstants.SERVICE_NAME_COMPANY_CREATION_PROCESSING, oCompanyCreationTimer);
                    oCompanyCreationTimer.Stop();
                    DateTime startDate = DateTime.Now;
                    Helper.LogInfo(string.Format(LoggingConstants.SERVICE_PROCESSING_BEGINS_TEXT, ServiceConstants.SERVICE_NAME_COMPANY_CREATION_PROCESSING, Helper.GetDateTime()), null);
                    try
                    {
                        CompanyCreation oCompanyCreation = new CompanyCreation();
                        oCompanyCreation.ProcessCompanyCreation();
                    }
                    catch (Exception ex)
                    {
                        Helper.LogError(ex, null);
                    }

                    Helper.LogInfo(string.Format(LoggingConstants.SERVICE_PROCESSING_ENDS_TEXT, ServiceConstants.SERVICE_NAME_COMPANY_CREATION_PROCESSING, Helper.GetDateTime()), null);
                    // Setting the Interval again
                    _CompanyCreationTimerInterval = Helper.GetTimerInterval(AppSettingConstants.TIMER_INTERVAL_COMPANY_CREATION_PROCESSING);
                    Helper.LogInfo(string.Format("Company Creation Service Interval == {0}", _CompanyCreationTimerInterval.ToString()), null);
                    DateTime endDate = DateTime.Now;
                    Helper.LogServiceTimeStampInfo(string.Format(LoggingConstants.SERVICE_TIME_STAMP_TEXT, startDate, endDate, endDate - startDate, ServiceConstants.SERVICE_NAME_COMPANY_CREATION_PROCESSING));
                }
                catch (Exception ex)
                {
                    Helper.LogServiceTimeStampError(string.Format(LoggingConstants.SERVICE_THREAD_ERROR_TEXT, ServiceConstants.SERVICE_NAME_COMPANY_CREATION_PROCESSING, ex.Message, ex.StackTrace));
                }
                finally
                {
                    oCompanyCreationTimer.Interval = _CompanyCreationTimerInterval * 60 * 1000; // Convert to mili secs
                    oCompanyCreationTimer.Start();
                    _IsProcessingCompanyCreation = false;
                    GC.KeepAlive(oCompanyCreationTimer);
                }
            }
        }

        void oAccountReconcilabilityTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (!_IsServiceStopping)
            {
                try
                {
                    _IsProcessingAccountReconcilability = true;
                    UpdateLastExecutionTime(ServiceConstants.SERVICE_NAME_ACCOUNT_RECONCILABILITY_PROCESSING, oAccountReconcilabilityTimer);
                    oAccountReconcilabilityTimer.Stop();
                    DateTime startDate = DateTime.Now;
                    Helper.LogInfo(string.Format(LoggingConstants.SERVICE_PROCESSING_BEGINS_TEXT, ServiceConstants.SERVICE_NAME_ACCOUNT_RECONCILABILITY_PROCESSING, Helper.GetDateTime()), null);

                    try
                    {
                        AccountReconcilabilityProcessing oAccountReconcilabilityProcessing = new AccountReconcilabilityProcessing();
                        oAccountReconcilabilityProcessing.ReprocessAccountReconcilability();
                    }
                    catch (Exception ex)
                    {
                        Helper.LogError(ex, null);
                    }

                    Helper.LogInfo(string.Format(LoggingConstants.SERVICE_PROCESSING_ENDS_TEXT, ServiceConstants.SERVICE_NAME_ACCOUNT_RECONCILABILITY_PROCESSING, Helper.GetDateTime()), null);
                    // Setting the Interval again
                    _AccountReconcilabilityTimerInterval = Helper.GetTimerInterval(AppSettingConstants.TIMER_INTERVAL_ACCOUNT_RECONCILABILITY_PROCESSING);
                    Helper.LogInfo(string.Format("Account Reconcilability Processing Service Interval == {0}", _AccountReconcilabilityTimerInterval.ToString()), null);
                    DateTime endDate = DateTime.Now;
                    Helper.LogServiceTimeStampInfo(string.Format(LoggingConstants.SERVICE_TIME_STAMP_TEXT, startDate, endDate, endDate - startDate, ServiceConstants.SERVICE_NAME_ACCOUNT_RECONCILABILITY_PROCESSING));
                }
                catch (Exception ex)
                {
                    Helper.LogServiceTimeStampError(string.Format(LoggingConstants.SERVICE_THREAD_ERROR_TEXT, ServiceConstants.SERVICE_NAME_ACCOUNT_RECONCILABILITY_PROCESSING, ex.Message, ex.StackTrace));
                }
                finally
                {
                    oAccountReconcilabilityTimer.Interval = _AccountReconcilabilityTimerInterval * 60 * 1000; // Convert to mili secs
                    oAccountReconcilabilityTimer.Start();
                    _IsProcessingAccountReconcilability = false;
                    GC.KeepAlive(oAccountReconcilabilityTimer);
                }
            }
        }
        void oTaskUploadTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (!_IsServiceStopping)
            {
                try
                {
                    _IsProcessingTaskUpload = true;
                    UpdateLastExecutionTime(ServiceConstants.SERVICE_NAME_TASKUPLOAD_PROCESSING, oTaskUploadTimer);
                    oTaskUploadTimer.Stop();
                    DateTime startDate = DateTime.Now;
                    Helper.LogInfo(string.Format(LoggingConstants.SERVICE_PROCESSING_BEGINS_TEXT, ServiceConstants.SERVICE_NAME_TASKUPLOAD_PROCESSING, Helper.GetDateTime()), null);
                    foreach (CompanyUserInfo oCompanyUserInfo in CacheHelper.GetDistinctDatabaseList().Values)
                    {
                        try
                        {
                            TaskUpload oTaskUpload = new TaskUpload(oCompanyUserInfo);
                            if (oTaskUpload.IsProcessingRequiredForTaskUpload())
                                oTaskUpload.ProcessTaskImport();

                        }
                        catch (Exception ex)
                        {
                            Helper.LogError(ex, oCompanyUserInfo);
                        }
                    }
                    Helper.LogInfo(string.Format(LoggingConstants.SERVICE_PROCESSING_ENDS_TEXT, ServiceConstants.SERVICE_NAME_TASKUPLOAD_PROCESSING, Helper.GetDateTime()), null);
                    // Setting the Interval again
                    _TaskUploadTimerInterval = Helper.GetTimerInterval(AppSettingConstants.TIMER_INTERVAL_TASKUPLOAD_PROCESSING);
                    Helper.LogInfo(string.Format("Task Upload Service Interval == {0}", _TaskUploadTimerInterval.ToString()), null);
                    DateTime endDate = DateTime.Now;
                    Helper.LogServiceTimeStampInfo(string.Format(LoggingConstants.SERVICE_TIME_STAMP_TEXT, startDate, endDate, endDate - startDate, ServiceConstants.SERVICE_NAME_TASKUPLOAD_PROCESSING));
                }
                catch (Exception ex)
                {
                    Helper.LogServiceTimeStampError(string.Format(LoggingConstants.SERVICE_THREAD_ERROR_TEXT, ServiceConstants.SERVICE_NAME_TASKUPLOAD_PROCESSING, ex.Message, ex.StackTrace));
                }
                finally
                {
                    oTaskUploadTimer.Interval = _TaskUploadTimerInterval * 60 * 1000; // Convert to mili secs
                    oTaskUploadTimer.Start();
                    _IsProcessingTaskUpload = false;
                    GC.KeepAlive(oTaskUploadTimer);
                }
            }
        }

        void oIndexRecreationServiceTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (!_IsServiceStopping)
            {
                try
                {
                    _IsProcessingIndexCreation = true;
                    UpdateLastExecutionTime(ServiceConstants.SERVICE_NAME_INDEX_RECREATION_SERVICE, oIndexRecreationServiceTimer);
                    oIndexRecreationServiceTimer.Stop();
                    DateTime startDate = DateTime.Now;
                    Helper.LogInfo(string.Format(LoggingConstants.SERVICE_START_TEXT, ServiceConstants.SERVICE_NAME_INDEX_RECREATION_SERVICE, Helper.GetDateTime()), null);
                    try
                    {
                        IndexReCreationService oIndexReCreationService = new IndexReCreationService();
                        oIndexReCreationService.ReCreateIndexs();
                    }
                    catch (Exception ex)
                    {
                        Helper.LogError(ex, null);
                    }
                    Helper.LogInfo(string.Format(LoggingConstants.SERVICE_STOP_TEXT, ServiceConstants.SERVICE_NAME_INDEX_RECREATION_SERVICE, Helper.GetDateTime()), null);
                    // Setting the Interval again
                    _IndexRecreationServiceTimerInterval = Helper.GetTimerInterval(AppSettingConstants.TIMER_INTERVAL_INDEX_RECREATION_SERVICE_PROCESSING);
                    Helper.LogInfo(string.Format("Index Recreation Service Interval == {0}", _IndexRecreationServiceTimerInterval.ToString()), null);
                    DateTime endDate = DateTime.Now;
                    Helper.LogServiceTimeStampInfo(string.Format(LoggingConstants.SERVICE_TIME_STAMP_TEXT, startDate, endDate, endDate - startDate, ServiceConstants.SERVICE_NAME_INDEX_RECREATION_SERVICE));
                }
                catch (Exception ex)
                {
                    Helper.LogServiceTimeStampError(string.Format(LoggingConstants.SERVICE_THREAD_ERROR_TEXT, ServiceConstants.SERVICE_NAME_INDEX_RECREATION_SERVICE, ex.Message, ex.StackTrace));
                }
                finally
                {
                    oIndexRecreationServiceTimer.Interval = _IndexRecreationServiceTimerInterval * 60 * 1000; // Convert to mili secs
                    oIndexRecreationServiceTimer.Start();
                    _IsProcessingIndexCreation = false;
                    GC.KeepAlive(oIndexRecreationServiceTimer);
                }
            }
        }

        void oRecItemImportTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (!_IsServiceStopping)
            {
                try
                {
                    _IsProcessingRecItemImport = true;
                    UpdateLastExecutionTime(ServiceConstants.SERVICE_NAME_REC_ITEM_IMPORT_SERVICE, oRecItemImportTimer);
                    oRecItemImportTimer.Stop();
                    DateTime startDate = DateTime.Now;
                    Helper.LogInfo(string.Format(LoggingConstants.SERVICE_START_TEXT, ServiceConstants.SERVICE_NAME_REC_ITEM_IMPORT_SERVICE, Helper.GetDateTime()), null);
                    try
                    {
                        ScheduleRecItemImportService oScheduleRecItemImportService = new ScheduleRecItemImportService();
                        oScheduleRecItemImportService.ProcessScheduleRecItemImports();
                    }
                    catch (Exception ex)
                    {
                        Helper.LogError(ex, null);
                    }
                    Helper.LogInfo(string.Format(LoggingConstants.SERVICE_STOP_TEXT, ServiceConstants.SERVICE_NAME_REC_ITEM_IMPORT_SERVICE, Helper.GetDateTime()), null);
                    // Setting the Interval again
                    _RecItemImportTimerInterval = Helper.GetTimerInterval(AppSettingConstants.TIMER_INTERVAL_REC_ITEM_IMPORT_PROCESSING);
                    Helper.LogInfo(string.Format("Index Recreation Service Interval == {0}", _RecItemImportTimerInterval.ToString()), null);
                    DateTime endDate = DateTime.Now;
                    Helper.LogServiceTimeStampInfo(string.Format(LoggingConstants.SERVICE_TIME_STAMP_TEXT, startDate, endDate, endDate - startDate, ServiceConstants.SERVICE_NAME_REC_ITEM_IMPORT_SERVICE));
                }
                catch (Exception ex)
                {
                    Helper.LogServiceTimeStampError(string.Format(LoggingConstants.SERVICE_THREAD_ERROR_TEXT, ServiceConstants.SERVICE_NAME_REC_ITEM_IMPORT_SERVICE, ex.Message, ex.StackTrace));
                }
                finally
                {
                    oRecItemImportTimer.Interval = _RecItemImportTimerInterval * 60 * 1000; // Convert to mili secs
                    oRecItemImportTimer.Start();
                    _IsProcessingRecItemImport = false;
                    GC.KeepAlive(oRecItemImportTimer);
                }
            }
        }


        void oClearCompanyCacheTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (!_IsServiceStopping)
            {
                try
                {
                    _IsProcessingClearCompanyCache = true;
                    UpdateLastExecutionTime(ServiceConstants.SERVICE_NAME_CLEAR_COMPANY_CACHE_SERVICE, oClearCompanyCacheTimer);
                    oClearCompanyCacheTimer.Stop();
                    DateTime startDate = DateTime.Now;
                    Helper.LogInfo(string.Format(LoggingConstants.SERVICE_START_TEXT, ServiceConstants.SERVICE_NAME_CLEAR_COMPANY_CACHE_SERVICE, Helper.GetDateTime()), null);
                    try
                    {
                        CacheHelper.ClearCompanyList();
                    }
                    catch (Exception ex)
                    {
                        Helper.LogError(ex, null);
                    }
                    Helper.LogInfo(string.Format(LoggingConstants.SERVICE_STOP_TEXT, ServiceConstants.SERVICE_NAME_CLEAR_COMPANY_CACHE_SERVICE, Helper.GetDateTime()), null);
                    // Setting the Interval again
                    _ClearCompanyCacheTimerInterval = Helper.GetTimerInterval(AppSettingConstants.TIMER_INTERVAL_CLEAR_COMPANY_CACHE_PROCESSING);
                    Helper.LogInfo(string.Format("Clear Company Cache Service Interval == {0}", _ClearCompanyCacheTimerInterval.ToString()), null);
                    DateTime endDate = DateTime.Now;
                    Helper.LogServiceTimeStampInfo(string.Format(LoggingConstants.SERVICE_TIME_STAMP_TEXT, startDate, endDate, endDate - startDate, ServiceConstants.SERVICE_NAME_CLEAR_COMPANY_CACHE_SERVICE));
                }
                catch (Exception ex)
                {
                    Helper.LogServiceTimeStampError(string.Format(LoggingConstants.SERVICE_THREAD_ERROR_TEXT, ServiceConstants.SERVICE_NAME_CLEAR_COMPANY_CACHE_SERVICE, ex.Message, ex.StackTrace));
                }
                finally
                {
                    oClearCompanyCacheTimer.Interval = _ClearCompanyCacheTimerInterval * 60 * 1000; // Convert to mili secs
                    oClearCompanyCacheTimer.Start();
                    _IsProcessingClearCompanyCache = false;
                    GC.KeepAlive(oClearCompanyCacheTimer);
                }
            }
        }
        void oFTPDataImportTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (!_IsServiceStopping && !_IsProcessingDataUpload && !_IsProcessingAlert )
            {
                try
                {
                    _IsProcessingFTPDataImport = true;
                    UpdateLastExecutionTime(ServiceConstants.SERVICE_NAME_FTPDATAIMPORT_PROCESSING, oFTPDataImportTimer);
                    oFTPDataImportTimer.Stop();
                    DateTime startDate = DateTime.Now;
                    Helper.LogInfo(string.Format(LoggingConstants.SERVICE_PROCESSING_BEGINS_TEXT, ServiceConstants.SERVICE_NAME_FTPDATAIMPORT_PROCESSING, Helper.GetDateTime()), null);
                    Dictionary<string, CompanyUserInfo> oDictConnectionString = CacheHelper.GetDistinctDatabaseList();
                    ConcurrentQueue<Exception> exceptions = new ConcurrentQueue<Exception>();
                    Parallel.ForEach<CompanyUserInfo>(oDictConnectionString.Values, oCompanyUserInfo =>
                    {
                        try
                        {
                            FTPDataImport objFTPDataImport = new FTPDataImport(oCompanyUserInfo);

                            if (objFTPDataImport.IsProcessingRequiredForFTPDataImport())
                            {
                                objFTPDataImport.ProcessFTPDataImport();
                            }
                        }
                        catch (Exception ex)
                        {
                            exceptions.Enqueue(ex);
                        }
                    });
                    while (exceptions.Count > 0)
                    {
                        Exception exOut;
                        if (exceptions.TryDequeue(out exOut))
                            Helper.LogError(exOut, null);
                    }
                    Helper.LogInfo(string.Format(LoggingConstants.SERVICE_PROCESSING_ENDS_TEXT, ServiceConstants.SERVICE_NAME_FTPDATAIMPORT_PROCESSING, Helper.GetDateTime()), null);
                    // Setting the Interval again
                    _FTPDataImportTimerInterval = Helper.GetTimerInterval(AppSettingConstants.TIMER_INTERVAL_FTPDATAIMPORT_PROCESSING);
                    Helper.LogInfo(string.Format("FTPDataImport Service Interval == {0}", _FTPDataImportTimerInterval.ToString()), null);
                    DateTime endDate = DateTime.Now;
                    Helper.LogServiceTimeStampInfo(string.Format(LoggingConstants.SERVICE_TIME_STAMP_TEXT, startDate, endDate, endDate - startDate, ServiceConstants.SERVICE_NAME_FTPDATAIMPORT_PROCESSING));
                }
                catch (Exception ex)
                {
                    Helper.LogServiceTimeStampError(string.Format(LoggingConstants.SERVICE_THREAD_ERROR_TEXT, ServiceConstants.SERVICE_NAME_FTPDATAIMPORT_PROCESSING, ex.Message, ex.StackTrace));
                }
                finally
                {
                    oFTPDataImportTimer.Interval = _FTPDataImportTimerInterval * 60 * 1000; // Convert to mili secs
                    oFTPDataImportTimer.Start();
                    _IsProcessingFTPDataImport = false;
                    GC.KeepAlive(oFTPDataImportTimer);
                }
            }
        }

        void oThreadCheckTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (!_IsServiceStopping)
            {
                try
                {
                    _IsProcessingThreadCheck = true;
                    UpdateLastExecutionTime(ServiceConstants.SERVICE_NAME_THREAD_CHECK_PROCESSING, oThreadCheckTimer);
                    DateTime startDate = DateTime.Now;
                    Helper.LogInfo(string.Format(LoggingConstants.SERVICE_PROCESSING_BEGINS_TEXT, ServiceConstants.SERVICE_NAME_THREAD_CHECK_PROCESSING, Helper.GetDateTime()), null);
                    RestartStoppedTimersOnTimeOut();
                    Helper.LogInfo(string.Format(LoggingConstants.SERVICE_PROCESSING_ENDS_TEXT, ServiceConstants.SERVICE_NAME_THREAD_CHECK_PROCESSING, Helper.GetDateTime()), null);
                    Helper.LogInfo(string.Format("Thread check Service Interval == {0}", _ThreadCheckTimerInterval.ToString()), null);
                    DateTime endDate = DateTime.Now;
                    Helper.LogServiceTimeStampInfo(string.Format(LoggingConstants.SERVICE_TIME_STAMP_TEXT, startDate, endDate, endDate - startDate, ServiceConstants.SERVICE_NAME_THREAD_CHECK_PROCESSING));
                }
                catch (Exception ex)
                {
                    Helper.LogServiceTimeStampError(string.Format(LoggingConstants.SERVICE_THREAD_ERROR_TEXT, ServiceConstants.SERVICE_NAME_THREAD_CHECK_PROCESSING, ex.Message, ex.StackTrace));
                }
                finally
                {
                    _IsProcessingThreadCheck = false;
                    GC.KeepAlive(oThreadCheckTimer);
                }
            }
        }

        #endregion

        private void UpdateLastExecutionTime(string serviceName, Timer oTimer)
        {
            try
            {
                if (_dictTimerList.Keys.Contains(serviceName))
                {
                    _dictTimerList[serviceName] = oTimer;
                    _dictLastExecutionList[serviceName] = DateTime.Now;
                }
                else
                {
                    _dictTimerList.Add(serviceName, oTimer);
                    _dictLastExecutionList.Add(serviceName, DateTime.Now);
                }
            }
            catch (Exception ex)
            {
                Helper.LogServiceTimeStampError(string.Format(LoggingConstants.SERVICE_THREAD_ERROR_TEXT, "Error in Update last execution time", ex.Message, ex.StackTrace));
            }
        }

        private void RestartStoppedTimersOnTimeOut()
        {
            try
            {
                if (!_IsServiceStopping)
                {
                    int ThreadCheckTimeout = Helper.GetTimerInterval(AppSettingConstants.THREAD_CHECK_TIMEOUT);
                    Double interval = ThreadCheckTimeout * 60 * 1000;
                    foreach (String serviceName in _dictTimerList.Keys)
                    {
                        Timer oTimer = _dictTimerList[serviceName];
                        DateTime oLastRunDateTime = _dictLastExecutionList[serviceName];
                        if ((DateTime.Now - oLastRunDateTime).Milliseconds > interval)
                        {
                            Helper.LogServiceTimeStampError(string.Format(LoggingConstants.SERVICE_RESTART_TEXT, serviceName, Helper.GetDateTime()));
                            oTimer.Start();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Helper.LogServiceTimeStampError(string.Format(LoggingConstants.SERVICE_THREAD_ERROR_TEXT, "Error in Restart Stopped Timers", ex.Message, ex.StackTrace));
            }
        }
    }
}
