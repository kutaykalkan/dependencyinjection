using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.HtmlControls;
using SkyStem.ART.Client.Exception;
using SkyStem.Library.Controls.WebControls;
using SkyStem.Language.LanguageUtility;
using SkyStem.ART.Client.Model;
using System.Data;



namespace SkyStem.ART.Web.Utility
{
    /// <summary>
    /// Summary description for WebPartHelper
    /// </summary>
    public class WebPartHelper
    {
        public WebPartHelper()
        {
            //
            // TODO: Add constructor logic here
            //
        }


        public static void ShowErrorMessage(HtmlTable tblMessage, HtmlTable tblContent, ExLabel lblMessage, ARTException ex)
        {
            tblMessage.Visible = true;
            tblContent.Visible = false;
            if (ex is ARTSystemException)
            {
                lblMessage.Text = string.Format(LanguageUtil.GetValue(5000062), LanguageUtil.GetValue(ex.ExceptionPhraseID));
            }
            else
            {
                lblMessage.LabelID = ex.ExceptionPhraseID;
            }
            Helper.LogException(ex);
        }

        public static void ShowErrorMessage(HtmlTable tblMessage, HtmlTable tblContent, ExLabel lblMessage, Exception ex)
        {
            tblMessage.Visible = true;
            tblContent.Visible = false;
            lblMessage.Text = ex.Message;
            Helper.LogException(ex);
        }
        public static string GetStatusColor(int StatusId)
        {
            List<ReconciliationStatusMstInfo> oRecStatusInfoCollection = CacheHelper.GetAllRecStatus();
            ReconciliationStatusMstInfo oReconciliationStatusMstInfo = null;
            oReconciliationStatusMstInfo = oRecStatusInfoCollection.Find(o => o.ReconciliationStatusID == StatusId);
            string StColor = string.Empty;
            if (oReconciliationStatusMstInfo != null)
                StColor = oReconciliationStatusMstInfo.StatusColor;

            return StColor;
        }

        public static DataTable CreateDataTable(List<int> oSelectedIDList)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("ImportTemplateListID", typeof(System.Int32)));

            DataRow dr = null;

            foreach (int oSelectedID in oSelectedIDList)
            {
                dr = dt.NewRow();
                dr[0] = oSelectedID;
                dt.Rows.Add(dr);
            }
            return dt;
        }

        public static DataTable CreateDataTableTempalteMapping(List<ImportTemplateFieldMappingInfo> oImportTemplateFieldMappingInfoLst)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("ImportTemplateFieldMappingID", typeof(System.Int32)));
            dt.Columns.Add(new DataColumn("ImportFieldID", typeof(System.Int32)));
            dt.Columns.Add(new DataColumn("ImportTemplateFieldID", typeof(System.Int32)));
            dt.Columns.Add(new DataColumn("IsActive", typeof(System.Boolean)));

            DataRow dr = null;

            foreach (ImportTemplateFieldMappingInfo oImportTemplateFieldMappingInfo in oImportTemplateFieldMappingInfoLst)
            {
                dr = dt.NewRow();
                dr[0] = DBNull.Value;
                dr[1] = oImportTemplateFieldMappingInfo.ImportFieldID;
                if (oImportTemplateFieldMappingInfo.ImportTemplateFieldID == null)
                {
                    dr[2] = DBNull.Value;
                }
                else
                {
                    dr[2] = oImportTemplateFieldMappingInfo.ImportTemplateFieldID;
                }
                dr[3] = true;
                dt.Rows.Add(dr);
            }
            return dt;
        }



        public static DataTable CreateDataTableDataImportSchedule(IList<DataImportScheduleInfo> oDataImportScheduleInfoLst)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("DataImportTypeID", typeof(System.Int16)));
            dt.Columns.Add(new DataColumn("Description", typeof(System.String)));
            dt.Columns.Add(new DataColumn("Hours", typeof(System.Int16)));
            dt.Columns.Add(new DataColumn("Minutes", typeof(System.Int16)));
            dt.Columns.Add(new DataColumn("DataImportScheduleID", typeof(System.Int32)));

            DataRow dr = null;

            foreach (DataImportScheduleInfo oDataImportScheduleInfo in oDataImportScheduleInfoLst)
            {
                dr = dt.NewRow();
                dr[0] = oDataImportScheduleInfo.DataImportTypeID;
                dr[1] = oDataImportScheduleInfo.Description;
                if (oDataImportScheduleInfo.Hours.HasValue)
                {
                    dr[2] = oDataImportScheduleInfo.Hours;
                }
                else
                {
                    dr[2] = DBNull.Value;
                }
                if (oDataImportScheduleInfo.Minutes.HasValue)
                {
                    dr[3] = oDataImportScheduleInfo.Minutes;
                }
                else
                {
                    dr[3] = DBNull.Value;
                }
                dr[4] = oDataImportScheduleInfo.DataImportScheduleID;
                dt.Rows.Add(dr);
            }
            return dt;
        }

        public static DataTable CreateDataTableWarningPreferences(List<DataImportWarningPreferencesInfo> oDataImportWarningPreferencesInfoLst)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("DataImportWarningPreferencesID", typeof(System.Int32)));
            dt.Columns.Add(new DataColumn("DataImportMessageID", typeof(System.Int32)));
            dt.Columns.Add(new DataColumn("IsActive", typeof(System.Boolean)));
            dt.Columns.Add(new DataColumn("IsEnabled", typeof(System.Boolean)));
            DataRow dr = null;
            foreach (DataImportWarningPreferencesInfo oDataImportWarningPreferencesInfo in oDataImportWarningPreferencesInfoLst)
            {
                dr = dt.NewRow();
                dr[0] = oDataImportWarningPreferencesInfo.DataImportWarningPreferencesID;
                dr[1] = oDataImportWarningPreferencesInfo.DataImportMessageID;
                dr[2] = true;
                dr[3] = oDataImportWarningPreferencesInfo.IsEnabled;
                dt.Rows.Add(dr);
            }
            return dt;
        }
    }
}