using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Reporting.WebForms;
using SkyStem.ART.Web.Data;
using SkyStem.ART.Client.Data;
using SkyStem.ART.Client.Model;
using System.IO;

namespace SkyStem.ART.Web.Utility
{

    /// <summary>
    /// Summary description for SSRSReportsHelper
    /// </summary>
    public class SSRSReportsHelper
    {
        private SSRSReportsHelper()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        /// <summary>
        /// Generate PDF and return byte steam
        /// </summary>
        /// <param name="glDataID"></param>
        /// <param name="netAccountID"></param>
        /// <param name="accountID"></param>
        /// <param name="recTemplateID"></param>
        /// <returns></returns>
        public static byte[] GeneratePDFBytes(long? glDataID, int? netAccountID, long? accountID, short recTemplateID)
        {
            ReportViewer rptReportViewer = new ReportViewer();
            rptReportViewer.ServerReport.ReportServerUrl = new Uri(AppSettingHelper.GetAppSettingValue("ReportUri"));
            rptReportViewer.ServerReport.ReportPath = AppSettingHelper.GetAppSettingValue("ReportPath");

            List<ReportParameter> reportParameters = new List<ReportParameter>();
            reportParameters = SetReportParameters(reportParameters, glDataID.Value, netAccountID.GetValueOrDefault(), accountID.GetValueOrDefault(), recTemplateID);

            rptReportViewer.ServerReport.SetParameters(reportParameters);

            Warning[] warnings;
            string[] streamids;
            string mimeType;
            string encoding;
            string extension;
            byte[] oByteCollection = null;

            oByteCollection = rptReportViewer.ServerReport.Render(
               "PDF", null, out mimeType, out encoding, out extension,
               out streamids, out warnings);
            return oByteCollection;
        }

        /// <summary>
        /// Set Report Parameters
        /// </summary>
        /// <param name="reportParams"></param>
        /// <param name="glDataID"></param>
        /// <param name="netAccountID"></param>
        /// <param name="accountID"></param>
        /// <param name="recTemplateID"></param>
        /// <returns></returns>
        private static List<ReportParameter> SetReportParameters(List<ReportParameter> reportParams, long glDataID, long netAccountID, long accountID, short recTemplateID)
        {
            ReportParameter rpGlDataID = new ReportParameter("GLdataID", glDataID.ToString());
            reportParams.Add(rpGlDataID);

            ReportParameter reconciliationTemplateID = new ReportParameter("ReconciliationTemplateID", recTemplateID.ToString());
            reportParams.Add(reconciliationTemplateID);

            ReportParameter recPeriodID = new ReportParameter("RecPeriodID", SessionHelper.CurrentReconciliationPeriodID.ToString());
            reportParams.Add(recPeriodID);

            ReportParameter languageID = new ReportParameter("LANGUAGEID", SessionHelper.GetUserLanguage().ToString());
            reportParams.Add(languageID);

            ReportParameter companyID = new ReportParameter("COMPANYID", SessionHelper.CurrentCompanyID.ToString());
            reportParams.Add(companyID);

            ReportParameter defaultLanguageID = new ReportParameter("DEFAULTLANGUAGEID", AppSettingHelper.GetDefaultLanguageID().ToString());
            reportParams.Add(defaultLanguageID);

            ReportParameter userID = new ReportParameter("UserID", SessionHelper.CurrentUserID.ToString());
            reportParams.Add(userID);

            ReportParameter roleID = new ReportParameter("RoleID", SessionHelper.CurrentRoleID.ToString());
            reportParams.Add(roleID);

            ReportParameter isDualReviewEnabled = new ReportParameter("IsDualReviewEnabled",
                Helper.IsCapabilityActivatedForCurrentRecPeriod(ARTEnums.Capability.DualLevelReview).ToString());
            reportParams.Add(isDualReviewEnabled);

            ReportParameter isCertificationEnabled = new ReportParameter("IsCertificationEnabled",
                Helper.IsCapabilityActivatedForCurrentRecPeriod(ARTEnums.Capability.CertificationActivation).ToString());
            reportParams.Add(isCertificationEnabled);

            ReportParameter isMaterialityEnabled = new ReportParameter("IsMaterialityEnabled",
                Helper.IsCapabilityActivatedForCurrentRecPeriod(ARTEnums.Capability.MaterialitySelection).ToString());
            reportParams.Add(isMaterialityEnabled);

            ReportParameter certificationTypeId = new ReportParameter("CertificationTypeId", ((short)WebEnums.CertificationType.Certification).ToString());
            reportParams.Add(certificationTypeId);

            ReportParameter preparerAttributeId = new ReportParameter("PreparerAttributeId", ((short)ARTEnums.AccountAttribute.Preparer).ToString());
            reportParams.Add(preparerAttributeId);

            ReportParameter reviewerAttributeID = new ReportParameter("ReviewerAttributeID", ((short)ARTEnums.AccountAttribute.Reviewer).ToString());
            reportParams.Add(reviewerAttributeID);

            ReportParameter approverAttributeID = new ReportParameter("ApproverAttributeID", ((short)ARTEnums.AccountAttribute.Approver).ToString());
            reportParams.Add(approverAttributeID);

            ReportParameter rpNetAccountID = new ReportParameter("NetAccountID", netAccountID.ToString());
            reportParams.Add(rpNetAccountID);

            if (SessionHelper.CurrentRoleID == (short)ARTEnums.UserRole.AUDIT)
            {
                ReportParameter isQualityScoreEnabled = new ReportParameter("IsQualityScoreEnabled",
                    Helper.IsQualityScoreEnabled().ToString());
                reportParams.Add(isQualityScoreEnabled);

                ReportParameter IsReviewNotesEnabled = new ReportParameter("IsReviewNotesEnabled",
                    Helper.IsReviewNotesEnabled().ToString());
                reportParams.Add(IsReviewNotesEnabled);
            }
            else
            {
                ReportParameter isQualityScoreEnabled = new ReportParameter("IsQualityScoreEnabled",
                    Helper.IsFeatureActivated(WebEnums.Feature.QualityScore, SessionHelper.CurrentReconciliationPeriodID).ToString());
                reportParams.Add(isQualityScoreEnabled);

                //IsReviewNotesEnabled is enabled for all other roles
                ReportParameter IsReviewNotesEnabled = new ReportParameter("IsReviewNotesEnabled", "true");
                reportParams.Add(IsReviewNotesEnabled);
            }



            ReportParameter isNetAccount = new ReportParameter("isNetAccount", netAccountID.ToString() == "0" ? "true" : "false");
            reportParams.Add(isNetAccount);

            ReportParameter languageInfo = new ReportParameter("languageInfo", System.Threading.Thread.CurrentThread.CurrentCulture.Name);
            reportParams.Add(languageInfo);

            ReportParameter accId = new ReportParameter("AccountID", accountID.ToString());
            reportParams.Add(accId);

            // Changes for database seperation
            AppUserInfo oAppUserInfo = Helper.GetAppUserInfo();
            SkyStem.ART.App.Utility.ServiceHelper.SetConnectionString(oAppUserInfo);

            ReportParameter connString = new ReportParameter("ConnectionString", oAppUserInfo.ConnectionString);
            reportParams.Add(connString);


            return reportParams;
        }

        /// <summary>
        /// Save byte stream and return file path
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="byteStream"></param>
        /// <returns></returns>
        public static string SaveByteStreamAndReturnFilePath(string fileName, byte[] byteStream)
        {
            string filePath = ExportHelper.GenerateTempFilePath(fileName);
            File.WriteAllBytes(filePath, byteStream);
            return filePath;
        }
    }
}