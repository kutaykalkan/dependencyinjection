using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using SkyStem.ART.Service.Utility;
using SkyStem.ART.Service.Data;
using SkyStem.ART.Client.Model.CompanyDatabase;
using SkyStem.Language.LanguageUtility.Classes;
using SkyStem.Language.LanguageUtility;
using SkyStem.ART.Client.Model;
using SharedUtility = SkyStem.ART.Shared.Utility;

namespace SkyStem.ART.Service.APP.BLL
{

    public class Alert
    {
        private CompanyUserInfo CompanyUserInfo;
        public Alert(CompanyUserInfo oCompanyUserInfo)
        {
            this.CompanyUserInfo = oCompanyUserInfo;
        }
        public void RaiseAlerts()
        {
            try
            {
                List<CompanyAlertInfo> oCompanyAlertInfoList = DataImportHelper.GetRaiseAlertData(this.CompanyUserInfo);
                foreach (var oCompanyAlertInfo in oCompanyAlertInfoList)
                {
                    DataImportHelper.CreateDataForCompanyAlertID(oCompanyAlertInfo, this.CompanyUserInfo);
                    GetAlertMailDataAndSentMail(oCompanyAlertInfo);
                }

            }
            catch (Exception ex)
            {
                Helper.LogError(string.Format("{0} -> Error:: {1}", ServiceConstants.SERVICE_NAME_ALERT_PROCESSING, ex.Message), this.CompanyUserInfo);
            }

        }
        public void GetAlertMailDataAndSentMail(CompanyAlertInfo oCompanyAlertInfo)
        {
            List<CompanyAlertDetailUserInfo> oCompanyAlertDetailUserInfo = DataImportHelper.GetAlertMailDataForCompanyAlertID(oCompanyAlertInfo, this.CompanyUserInfo);
            SendMailAndUpdateEmailSentDate(oCompanyAlertDetailUserInfo);
        }
        private void SendMailAndUpdateEmailSentDate(List<CompanyAlertDetailUserInfo> oCompanyAlertDetailUserInfoList)
        {
            if (oCompanyAlertDetailUserInfoList.Count > 0)
            {
                string fromEmailId = Helper.GetAppSettingFromKey("defaultEmailFromCompany");
                Helper.LogInfo(@"Begin: Sending Alert Emails", this.CompanyUserInfo);

                List<CompanyAlertDetailUserInfo> oSuccessMailCompanyAlertDetailUserInfoList = new List<CompanyAlertDetailUserInfo>();

                foreach (CompanyAlertDetailUserInfo oCompanyAlertDetailUserInfo in oCompanyAlertDetailUserInfoList)
                {

                    StringBuilder oStringBuilder = new StringBuilder();
                    MultilingualAttributeInfo oMultilingualAttributeInfo = Helper.GetMultilingualAttributeInfo(oCompanyAlertDetailUserInfo.LanguageID, oCompanyAlertDetailUserInfo.CompanyID);
                    oStringBuilder.Append(LanguageUtil.GetValue(1845, oMultilingualAttributeInfo));
                    oStringBuilder.Append("&nbsp;");
                    oStringBuilder.Append(oCompanyAlertDetailUserInfo.FirstName);
                    oStringBuilder.Append("&nbsp;");
                    oStringBuilder.Append(oCompanyAlertDetailUserInfo.LastName);
                    oStringBuilder.Append(",");

                    try
                    {
                        string AlertDescription = oCompanyAlertDetailUserInfo.AlertDescription;
                        string alertSubject = LanguageUtil.GetValue(oCompanyAlertDetailUserInfo.AlertSubjectLabelID, oMultilingualAttributeInfo);
                        string AlertDetails = string.Empty;
                        bool IsDateColumnRequired = false;
                        bool IsNumberColumnRequired = false;
                        string NumberofDaysHeading = LanguageUtil.GetValue(1300, oMultilingualAttributeInfo);
                        string DueDateHeading = LanguageUtil.GetValue(1499, oMultilingualAttributeInfo);
                        List<CompanyAlertDetailInfo> oCompanyAlertDetailInfoList = DataImportHelper.GetCompanyAlertDetail(oCompanyAlertDetailUserInfo.CompanyAlertDetailID, this.CompanyUserInfo);

                        if (oCompanyAlertDetailInfoList != null && oCompanyAlertDetailInfoList.Count > 0
                            && oCompanyAlertDetailInfoList[0].DateValue.HasValue)
                        {
                            AlertDescription = string.Format(AlertDescription, SharedUtility.SharedHelper.GetDisplayDate(oCompanyAlertDetailInfoList[0].DateValue, oMultilingualAttributeInfo));
                            alertSubject = string.Format(alertSubject, SharedUtility.SharedHelper.GetDisplayDate(oCompanyAlertDetailInfoList[0].DateValue, oMultilingualAttributeInfo));
                            IsDateColumnRequired = true;
                        }

                        if (oCompanyAlertDetailInfoList != null && oCompanyAlertDetailInfoList.Count > 0
                            && oCompanyAlertDetailInfoList[0].NumberValue.HasValue)
                        {
                            AlertDescription = string.Format(AlertDescription, oCompanyAlertDetailInfoList[0].NumberValue);
                            alertSubject = string.Format(alertSubject, oCompanyAlertDetailInfoList[0].NumberValue);
                            IsNumberColumnRequired = true;
                        }

                        switch (oCompanyAlertDetailUserInfo.AlertID)
                        {
                            case 3:
                            case 4:
                            case 19:
                            case 20:
                            case 36:
                            case 37:
                                AlertDetails = GetAccountDetails(oCompanyAlertDetailUserInfo, 2533, IsDateColumnRequired, IsNumberColumnRequired, NumberofDaysHeading, DueDateHeading, oMultilingualAttributeInfo);
                                // 2533- Account Details 
                                break;
                            case 27:
                            case 28:
                            case 30:
                            case 31:
                            case 34:
                            case 35:
                                AlertDetails = GetTaskDetails(oCompanyAlertDetailUserInfo, 2793, IsDateColumnRequired, IsNumberColumnRequired, NumberofDaysHeading, DueDateHeading);
                                // 2793-Task Details 
                                break;
                            case 32:
                                AlertDetails = GetAccountDetails(oCompanyAlertDetailUserInfo, 2533, false, false, NumberofDaysHeading, DueDateHeading, oMultilingualAttributeInfo);
                                // 2533- Account Details 
                                break;
                        }

                        FormatMailDataForPeriodEndDate(oCompanyAlertDetailUserInfo, oMultilingualAttributeInfo, ref alertSubject, ref AlertDescription);
                        oStringBuilder.Append("<br/><br/>");
                        oStringBuilder.Append(AlertDescription);
                        oStringBuilder.Append("<br/>");
                        oStringBuilder.Append(AlertDetails);
                        oStringBuilder.Append("<br/><br/><b>");
                        oStringBuilder.Append(LanguageUtil.GetValue(1133, oMultilingualAttributeInfo));
                        oStringBuilder.Append("</b><br/><br/>");
                        oStringBuilder.Append(LanguageUtil.GetValue(oCompanyAlertDetailUserInfo.CompanyNameLabelID, oMultilingualAttributeInfo));
                        oStringBuilder.Append("<br/><br/>");
                        oStringBuilder.Append(LanguageUtil.GetValue(2021, oMultilingualAttributeInfo));

                        MailHelper.SendEmail(fromEmailId, oCompanyAlertDetailUserInfo.EmailID, alertSubject, oStringBuilder.ToString(), this.CompanyUserInfo);
                        oSuccessMailCompanyAlertDetailUserInfoList.Add(oCompanyAlertDetailUserInfo);
                    }
                    catch (Exception ex)
                    {
                        Helper.LogError(ex, this.CompanyUserInfo);
                    }
                }
                Helper.LogInfo(@"End: Sending Alert Emails", this.CompanyUserInfo);
                Helper.LogInfo(@"Begin: Updating Sent Date in DB", this.CompanyUserInfo);
                //UpdateSentMailStatus(oCompanyAlertDetailUserInfoList);
                if (oSuccessMailCompanyAlertDetailUserInfoList.Count > 0)
                    DataImportHelper.UpdateSentMailStatus(oSuccessMailCompanyAlertDetailUserInfoList, this.CompanyUserInfo);
                Helper.LogInfo(@"End: Updating Sent Date in DB", this.CompanyUserInfo);
            }
        }
        private void FormatMailDataForPeriodEndDate(CompanyAlertDetailUserInfo oCompanyAlertDetailUserInfo, MultilingualAttributeInfo oMultilingualAttributeInfo, ref string alertSubject, ref string AlertDescription)
        {
            //2804- (Period: {0}). 
            string AppendVal = LanguageUtil.GetValue(2804, oMultilingualAttributeInfo);
            string EndDate = SharedUtility.SharedHelper.GetDisplayDate(oCompanyAlertDetailUserInfo.PeriodEndDate, oMultilingualAttributeInfo);
            AppendVal = string.Format(AppendVal,EndDate );
            // 1420- Rec Period 
            string PeriodEndDate = LanguageUtil.GetValue(1420, oMultilingualAttributeInfo);
            PeriodEndDate= PeriodEndDate + ": " + EndDate;

            switch (oCompanyAlertDetailUserInfo.AlertID)
            {
                case 3:
                case 4:
                case 9:
                case 16:
                case 19:
                case 20:
                case 26:
                case 27:
                case 28:
                case 30:
                case 31:
                case 32:
                case 34:
                case 35:
                case 36:
                case 37:
                    AlertDescription = PeriodEndDate + "<br/>" + AlertDescription ;
                    alertSubject = alertSubject + " " + AppendVal;
                    break;             
            }
        }
        private string GetAccountDetails(CompanyAlertDetailUserInfo oCompanyAlertDetailUserInfo, int TableHeadingLabelID,
            bool isDateColumnRequired, bool isNumberColumnRequired, string NumberofDaysHeading, string DueDateHeading, MultilingualAttributeInfo oMultilingualAttributeInfo)
        {
            StringBuilder oAccountDetails = new StringBuilder();
            // Append Details of Account For which GL is not Loaded
            List<AccountHdrInfo> oListAccountHdrInfo = DataImportHelper.GetAccountInformationForCompanyAlertMail(oCompanyAlertDetailUserInfo);
           // MultilingualAttributeInfo oMultilingualAttributeInfo = Helper.GetMultilingualAttributeInfo(oCompanyAlertDetailUserInfo.LanguageID, oCompanyAlertDetailUserInfo.CompanyID);

            if (oListAccountHdrInfo != null && oListAccountHdrInfo.Count > 0)
            {
                string AccontNumber = LanguageUtil.GetValue(1357, oMultilingualAttributeInfo);//Act#
                string AccontName = LanguageUtil.GetValue(1346, oMultilingualAttributeInfo);//Acct Name                
                oAccountDetails.Append("<br/><br/><b>");
                oAccountDetails.Append(LanguageUtil.GetValue(TableHeadingLabelID, oMultilingualAttributeInfo));
                oAccountDetails.Append("</b><br/><br/>");
                Helper.GetAccountDetailsForAlertMail(oAccountDetails, oListAccountHdrInfo, oCompanyAlertDetailUserInfo.LanguageID,
                    oCompanyAlertDetailUserInfo.CompanyID, AccontNumber, AccontName,
                    isDateColumnRequired, isNumberColumnRequired,
                    oMultilingualAttributeInfo, NumberofDaysHeading, DueDateHeading);

            }
            return oAccountDetails.ToString();
        }
        private string GetTaskDetails(CompanyAlertDetailUserInfo oCompanyAlertDetailUserInfo, int TableHeadingLabelID,
            bool isDateColumnRequired, bool isNumberColumnRequired,string NumberofDaysHeading,string DueDateHeading)
        {
            StringBuilder oTaskDetails = new StringBuilder();
            // Append Details of Tasks
            List<TaskHdrInfo> oListTaskHdrInfo = DataImportHelper.GetTaskInformationForCompanyAlertMail(oCompanyAlertDetailUserInfo);
            MultilingualAttributeInfo oMultilingualAttributeInfo = Helper.GetMultilingualAttributeInfo(oCompanyAlertDetailUserInfo.LanguageID, oCompanyAlertDetailUserInfo.CompanyID);

            if (oListTaskHdrInfo != null && oListTaskHdrInfo.Count > 0)
            {
                oTaskDetails.Append("<br/><br/><b>");
                oTaskDetails.Append(LanguageUtil.GetValue(TableHeadingLabelID, oMultilingualAttributeInfo));
                oTaskDetails.Append("</b><br/><br/>");
                Helper.GetTaskDetailsForAlertMail(oTaskDetails, oListTaskHdrInfo, oCompanyAlertDetailUserInfo.LanguageID,
                    oCompanyAlertDetailUserInfo.CompanyID,
                    isDateColumnRequired, isNumberColumnRequired,
                    oMultilingualAttributeInfo, NumberofDaysHeading, DueDateHeading);
            }
            return oTaskDetails.ToString();
        }
        public void GetUserListAndSendMail(int dataImportID, int companyID, CompanyUserInfo oCompanyUserInfo)
        {
            try
            {
                Helper.LogInfo(@"Begin: Get User Alert Detail for DataImportID: " + dataImportID.ToString(), this.CompanyUserInfo);
                List<CompanyAlertDetailUserInfo> oCompanyAlertDetailUserInfoList = DataImportHelper.GetUserListForNewAccountAlert(dataImportID, companyID, oCompanyUserInfo);
                Helper.LogInfo(@"End: Get User Alert Detail for DataImportID: " + dataImportID.ToString(), this.CompanyUserInfo);
                Helper.LogInfo(@"Begin: Send Emails for DataImportID: " + dataImportID.ToString(), this.CompanyUserInfo);
                SendMailAndUpdateEmailSentDate(oCompanyAlertDetailUserInfoList);
                Helper.LogInfo(@"End: Send Emails for DataImportID: " + dataImportID.ToString(), this.CompanyUserInfo);
            }
            catch (Exception ex)
            {
                Helper.LogError(string.Format("{0} -> Error:: {1}", ServiceConstants.SERVICE_NAME_ALERT_PROCESSING, ex.Message), this.CompanyUserInfo);
            }

        }
    }

}
