using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Collections.Generic;
using SkyStem.ART.Client.Model;


namespace SkyStem.ART.App.Utility
{
    public class ReportServiceHelper
    {
        public static DataTable ConvertReportArchiveParameterListToDataTable(List<ReportArchiveParameterInfo > oRptArchiveparamInfoCollection, long newRptArchiveID)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ReportArchiveID", System.Type.GetType("System.Int64"));
            dt.Columns.Add("ReportParameterKeyID", System.Type.GetType("System.Int16"));
            dt.Columns.Add("ParameterValue", System.Type.GetType("System.String"));

            DataRow dr;
            foreach (ReportArchiveParameterInfo oRptArchiveParam in oRptArchiveparamInfoCollection)
            {
                dr = dt.NewRow();
                dr["ReportArchiveID"] = newRptArchiveID;
                if (!oRptArchiveParam.IsParameterValueNull )
                    dr["ParameterValue"] = oRptArchiveParam.ParameterValue;
                if (oRptArchiveParam.ReportParameterKeyID.HasValue)
                    dr["ReportParameterKeyID"] = oRptArchiveParam.ReportParameterKeyID.Value;
                    
                dt.Rows.Add(dr);
                dr = null;
            }

            return dt;
        }

        public static DataTable ConvertUserMyReportSavedReportParameterListToDataTable(List<UserMyReportSavedReportParameterInfo> oUserMyReportSavedReportParameterCollection, long newUserMyReportSavedReportID)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ReportArchiveID", System.Type.GetType("System.Int64"));
            dt.Columns.Add("ReportParameterKeyID", System.Type.GetType("System.Int16"));
            dt.Columns.Add("ParameterValue", System.Type.GetType("System.String"));

            DataRow dr;
            foreach (UserMyReportSavedReportParameterInfo oMyReportSavedReportParam in oUserMyReportSavedReportParameterCollection)
            {
                dr = dt.NewRow();
                dr["ReportArchiveID"] = newUserMyReportSavedReportID;
                if (!oMyReportSavedReportParam.IsReportParameterIDNull)
                    dr["ReportParameterKeyID"] = oMyReportSavedReportParam.ReportParameterID;
                if (!oMyReportSavedReportParam.IsParameterValueNull)
                    dr["ParameterValue"] = oMyReportSavedReportParam.ParameterValue;
               

                dt.Rows.Add(dr);
                dr = null;
            }

            return dt;
        }
    }
}
