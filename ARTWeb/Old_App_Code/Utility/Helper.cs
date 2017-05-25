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
using System.IO;
using System.Security.Cryptography;
using SkyStem.ART.Web.Data;
using SkyStem.Language.LanguageUtility;
using SkyStem.ART.Client.Model;
using SkyStem.ART.Client.IServices;
using SkyStem.Library.Controls.WebControls;
using SkyStem.ART.Client.Exception;
using SkyStem.ART.Web.Classes;
using SkyStem.ART.Client.Model.Base;
using System.Collections.Generic;
using SkyStem.ART.Client.Data;
using Telerik.Web.UI;
using System.Text;
using SkyStem.Library.Controls.TelerikWebControls;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.Globalization;
using SkyStem.Library.Controls.WebControls.Common.Data;
using SkyStem.ART.Client.Params;
using SkyStem.ART.Web.Classes.UserControl;
using SkyStem.ART.Client.Model.Matching;
using System.Xml;
using SkyStem.ART.Client.Model.QualityScore;
using System.Threading;
using SkyStem.Language.LanguageUtility.Classes;
using SkyStem.ART.Shared.Utility;

namespace SkyStem.ART.Web.Utility
{
    /// <summary>
    /// Summary description for Helper
    /// </summary>
    public class Helper
    {
        static Helper()
        {
            log4net.Config.XmlConfigurator.Configure();
        }

        public static readonly string FilterValueSeparator = AppSettingHelper.GetAppSettingValue(AppSettingConstants.FILTER_VALUE_SEPARATOR);

        public Helper()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        #region "Password related methods"

        public static string CreateRandomPassword(int passwordLength, string loginID)
        {
            return SharedHelper.CreateRandomPassword(passwordLength, loginID);
        }

        public static bool CompareHashedPasswordWithSalt(string passwordTyped, string passwordFromDB, string saltFromDB)
        {
            bool isAuthenticated = false;
            string stringResult = "";
            try
            {
                stringResult = GetHashedPassword(passwordTyped + saltFromDB);
                if (passwordFromDB == stringResult)
                { isAuthenticated = true; }
                else
                { isAuthenticated = false; }
            }
            catch (CryptographicException ce)
            {
                //To Do
                Helper.LogException(ce);
            }
            return isAuthenticated;
        }

        /// <summary>
        /// This function can be used to compare the hased value of entered password with that of 
        /// already hashed value of password stored in database database.
        /// </summary>
        /// <param name="passwordTyped"></param>
        /// <param name="passwordFromDB"></param>
        /// <returns></returns>
        public static bool CompareHashedPassword(string passwordTyped, string passwordFromDB)// changed from AuthenticateUser
        {
            bool isAuthenticated = false;
            string stringResult = "";
            try
            {
                stringResult = GetHashedPassword(passwordTyped);
                if (passwordFromDB == stringResult)
                { isAuthenticated = true; }
                else
                { isAuthenticated = false; }
            }
            catch (CryptographicException ce)
            {
                //Do work
                Helper.LogException(ce);
            }
            return isAuthenticated;
        }

        public static string GetHashedPassword(string passwordTyped)
        {
            string stringResult = "";
            try
            {
                SHA1 sha = new SHA1CryptoServiceProvider();// hashed length = 28
                //MD5 sha = new MD5CryptoServiceProvider();// hashed length = 24
                //SHA512Managed sha = new SHA512Managed();
                byte[] result = sha.ComputeHash(System.Text.Encoding.UTF8.GetBytes(passwordTyped));
                stringResult = Convert.ToBase64String(result);
            }
            catch (CryptographicException ce)
            {
                throw ce;
                //Do work
            }
            return stringResult;
        }
        #endregion

        /// <summary>
        /// Function to Get the Error Messages
        /// </summary>
        /// <param name="eFieldType"></param>
        /// <param name="oEntityNamePhraseIDCollection"></param>
        /// <returns></returns>
        public static string GetErrorMessage(WebEnums.FieldType eFieldType, params int[] oEntityNamePhraseIDCollection)
        {
            string errorMessage = string.Empty;
            switch (eFieldType)
            {
                case WebEnums.FieldType.MandatoryField:
                    errorMessage = string.Format(LanguageUtil.GetValue(5000006), LanguageUtil.GetValue(oEntityNamePhraseIDCollection[0]));
                    break;

                case WebEnums.FieldType.DateCompareField:
                    errorMessage = string.Format(LanguageUtil.GetValue(5000226), LanguageUtil.GetValue(oEntityNamePhraseIDCollection[0]), LanguageUtil.GetValue(oEntityNamePhraseIDCollection[1]));
                    break;

                case WebEnums.FieldType.DateCompareFieldGreaterThan:
                    errorMessage = string.Format(LanguageUtil.GetValue(5000214), LanguageUtil.GetValue(oEntityNamePhraseIDCollection[0]), LanguageUtil.GetValue(oEntityNamePhraseIDCollection[1]));
                    break;

                case WebEnums.FieldType.FSCaptionMandatoryField:
                    errorMessage = string.Format(LanguageUtil.GetValue(5000072), LanguageUtil.GetValue(oEntityNamePhraseIDCollection[0]));
                    break;

                case WebEnums.FieldType.DateFormatField:
                    errorMessage = string.Format(LanguageUtil.GetValue(5000136), LanguageUtil.GetValue(oEntityNamePhraseIDCollection[0]));
                    break;

                case WebEnums.FieldType.InvalidNumericField:
                    if (oEntityNamePhraseIDCollection.Length == 1)
                    {
                        errorMessage = string.Format(LanguageUtil.GetValue(5000138), LanguageUtil.GetValue(oEntityNamePhraseIDCollection[0]));
                    }
                    else
                    {
                        errorMessage = string.Format(LanguageUtil.GetValue(5000138), LanguageUtil.GetValue(oEntityNamePhraseIDCollection[0]) + " " + LanguageUtil.GetValue(oEntityNamePhraseIDCollection[1]));
                    }
                    break;
                case WebEnums.FieldType.InvalidDate:
                    errorMessage = string.Format(LanguageUtil.GetValue(5000213), LanguageUtil.GetValue(oEntityNamePhraseIDCollection[0]));
                    break;
                case WebEnums.FieldType.CompareBetweenField:
                    errorMessage = string.Format(LanguageUtil.GetValue(5000368), LanguageUtil.GetValue(oEntityNamePhraseIDCollection[0]), LanguageUtil.GetValue(oEntityNamePhraseIDCollection[1]), LanguageUtil.GetValue(oEntityNamePhraseIDCollection[2]));
                    break;
            }
            return errorMessage;
        }

        public static string GetMaxLengthErrorMessage(int LabelID, int length)
        {
            string errorMessage = string.Format(LanguageUtil.GetValue(5000350), LanguageUtil.GetValue(LabelID), length);
            return errorMessage;
        }

        public static string GetErrorMessageForSystemWide(WebEnums.FieldType eFieldType, string periodEndDate, params int[] oEntityNamePhraseIDCollection)
        {
            string errorMessage = string.Empty;
            switch (eFieldType)
            {
                case WebEnums.FieldType.DateCompareWithRecOrCurrentDateField:
                    errorMessage = string.Format(LanguageUtil.GetValue(5000123), LanguageUtil.GetValue(oEntityNamePhraseIDCollection[0]), periodEndDate);
                    break;
                case WebEnums.FieldType.DateComparePerRecDateField:
                    errorMessage = string.Format(LanguageUtil.GetValue(5000124), LanguageUtil.GetValue(oEntityNamePhraseIDCollection[0]), LanguageUtil.GetValue(oEntityNamePhraseIDCollection[1]), periodEndDate);
                    break;
            }

            return errorMessage;
        }

        #region Error Logging methods

        public static void HideMessage(PageBase oPageBase)
        {
            MasterPageBase oMasterPageBase;
            if (oPageBase.Master is MasterPageBase)
            {
                oMasterPageBase = (MasterPageBase)oPageBase.Master;
            }
            else
            {
                oMasterPageBase = (MasterPageBase)oPageBase.Master.Master;
            }
            oMasterPageBase.HideMessage();
        }


        /// <summary>
        /// Function to Log + Show Error Message from a User Control
        /// </summary>
        /// <param name="oUserControlBase"></param>
        /// <param name="ex"></param>
        public static void ShowErrorMessageFromUserControl(UserControlBase oUserControlBase, ARTException ex)
        {
            MasterPageBase oMasterPageBase;
            if (oUserControlBase.Page.Master is MasterPageBase)
            {
                oMasterPageBase = (MasterPageBase)oUserControlBase.Page.Master;
            }
            else
            {
                oMasterPageBase = (MasterPageBase)oUserControlBase.Page.Master.Master;
            }

            if (ex is ARTSystemException)
            {
                oMasterPageBase.ShowErrorMessage(string.Format(LanguageUtil.GetValue(5000062), LanguageUtil.GetValue(ex.ExceptionPhraseID)));
            }
            else
            {
                oMasterPageBase.ShowErrorMessage(ex.ExceptionPhraseID);
            }
        }

        /// <summary>
        /// Function to Log + Show Error Message from a User Control
        /// </summary>
        /// <param name="oPageBase"></param>
        /// <param name="ex"></param>
        public static void ShowErrorMessageFromUserControl(UserControlBase oUserControlBase, Exception ex)
        {
            MasterPageBase oMasterPageBase;
            if (oUserControlBase.Page.Master is MasterPageBase)
            {
                oMasterPageBase = (MasterPageBase)oUserControlBase.Page.Master;
            }
            else
            {
                oMasterPageBase = (MasterPageBase)oUserControlBase.Page.Master.Master;
            }

            oMasterPageBase.ShowErrorMessage(ex);
        }

        public static void ShowErrorMessage(PageBase oPageBase, ARTException ex)
        {
            MasterPageBase oMasterPageBase;
            if (oPageBase.Master is MasterPageBase)
            {
                oMasterPageBase = (MasterPageBase)oPageBase.Master;
            }
            else
            {
                oMasterPageBase = (MasterPageBase)oPageBase.Master.Master;
            }

            if (ex is ARTSystemException)
            {
                oMasterPageBase.ShowErrorMessage(string.Format(LanguageUtil.GetValue(5000062), LanguageUtil.GetValue(ex.ExceptionPhraseID)));
            }
            else
            {
                oMasterPageBase.ShowErrorMessage(ex.ExceptionPhraseID);
            }
        }

        public static void ShowErrorMessage(PageBase oPageBase, Exception ex)
        {
            MasterPageBase oMasterPageBase;
            if (oPageBase.Master is MasterPageBase)
            {
                oMasterPageBase = (MasterPageBase)oPageBase.Master;
            }
            else
            {
                oMasterPageBase = (MasterPageBase)oPageBase.Master.Master;
            }
            oMasterPageBase.ShowErrorMessage(ex);
        }

        public static void ShowErrorMessage(PageBase oPageBase, string errorMessage)
        {
            MasterPageBase oMasterPageBase;
            if (oPageBase.Master is MasterPageBase)
            {
                oMasterPageBase = (MasterPageBase)oPageBase.Master;
            }
            else
            {
                oMasterPageBase = (MasterPageBase)oPageBase.Master.Master;
            }
            oMasterPageBase.ShowErrorMessage(errorMessage);
        }

        private static string FormatErrorMessage(string message)
        {
            string errorMessage = "<li>" + message + "</li>";
            return errorMessage;
        }
        public static void ShowErrorMessageWithNoBullet(PageBase oPageBase, string errorMessage)
        {
            MasterPageBase oMasterPageBase;
            if (oPageBase.Master is MasterPageBase)
            {
                oMasterPageBase = (MasterPageBase)oPageBase.Master;
            }
            else
            {
                oMasterPageBase = (MasterPageBase)oPageBase.Master.Master;
            }
            oMasterPageBase.ShowErrorMessageWithNoBullet(errorMessage);
        }
        /// <summary>
        /// Format the Error message, Log it and display on UI
        /// </summary>
        /// <param name="lblErrorMessage">Error Message Label, Nullable Allowed</param>
        /// <param name="errorMessage"></param>
        public static void FormatAndShowErrorMessage(ExLabel lblErrorMessage, string errorMessage)
        {

            Helper.LogInfo(errorMessage);
            if (lblErrorMessage != null)
            {
                lblErrorMessage.Visible = true;
                errorMessage = Helper.FormatErrorMessage(errorMessage);
                lblErrorMessage.Text = "<ul>" + errorMessage + "</ul>";
            }
        }

        public static void FormatAndShowErrorMessageNoBullet(ExLabel lblErrorMessage, string errorMessage)
        {
            Helper.LogInfo(errorMessage);
            if (lblErrorMessage != null)
            {
                lblErrorMessage.Visible = true;
                //errorMessage = Helper.FormatErrorMessage(errorMessage);
                lblErrorMessage.Text = "<ul>" + errorMessage + "</ul>";
                lblErrorMessage.Text = errorMessage;
            }
        }

        public static void FormatAndShowErrorMessage(ExLabel lblErrorMessage, ARTException ex)
        {
            Helper.LogException(ex);
            if (lblErrorMessage != null)
            {
                string errorMessage = string.Empty;
                if (ex is ARTSystemException)
                {
                    errorMessage = string.Format(LanguageUtil.GetValue(5000062), LanguageUtil.GetValue(ex.ExceptionPhraseID));
                }
                else
                {
                    errorMessage = LanguageUtil.GetValue(ex.ExceptionPhraseID);
                }
                Helper.FormatAndShowErrorMessage(lblErrorMessage, errorMessage);
            }
        }

        public static void FormatAndShowErrorMessage(ExLabel lblErrorMessage, Exception ex)
        {
            Helper.LogException(ex);
            if (lblErrorMessage != null)
            {
                string errorMessage = ex.Message;//LanguageUtil.GetValue(5000011);
                Helper.FormatAndShowErrorMessage(lblErrorMessage, errorMessage);
            }
        }

        public static void ShowConfirmationMessage(PageBase oPageBase, string message)
        {
            MasterPageBase oMasterPageBase;
            if (oPageBase.Master is MasterPageBase)
            {
                oMasterPageBase = (MasterPageBase)oPageBase.Master;
            }
            else
            {
                oMasterPageBase = (MasterPageBase)oPageBase.Master.Master;
            }
            oMasterPageBase.ShowConfirmationMessage(message);
        }

        public static void LogException(Exception ex)
        {
            //log4net.ILog oLogger = log4net.LogManager.GetLogger(AppSettingConstants.LOGGER_NAME);
            //oLogger.Error("", ex);
            LogErrorViaService(ex);
        }

        private static void LogException(string message)
        {
            //log4net.ILog oLogger = log4net.LogManager.GetLogger(AppSettingConstants.LOGGER_NAME);
            //oLogger.Error(message);
            LogErrorViaService(message);
        }

        public static void LogInfo(string message)
        {
            //log4net.ILog oLogger = log4net.LogManager.GetLogger(AppSettingConstants.LOGGER_NAME);
            //oLogger.Info(message);
            LogInfoViaService(message);
        }

        public static void LogInfoViaService(string message)
        {
            IUtility oUtilityClient = RemotingHelper.GetUtilityObject();
            LogInfo oLog = new LogInfo();
            oLog.Message = message;
            oLog.LogDate = DateTime.Now;
            oLog.LogLevel = ARTConstants.LOG_INFO;
            oLog.Source = ARTConstants.UI_SOURCE_NAME;
            AppUserInfo oAppUser = Helper.GetAppUserInfo();
            oUtilityClient.LogInfo(oLog, oAppUser);
        }

        public static void LogInfoViaService(Exception ex)
        {
            IUtility oUtilityClient = RemotingHelper.GetUtilityObject();
            LogInfo oLog = new LogInfo();
            oLog.Message = ex.Message;
            oLog.StackTrace = ex.StackTrace;
            oLog.LogDate = DateTime.Now;
            oLog.LogLevel = ARTConstants.LOG_INFO;
            oLog.Source = ARTConstants.UI_SOURCE_NAME;
            AppUserInfo oAppUser = Helper.GetAppUserInfo();
            oUtilityClient.LogInfo(oLog, oAppUser);
        }

        public static void LogErrorViaService(string message)
        {
            IUtility oUtilityClient = RemotingHelper.GetUtilityObject();
            LogInfo oLog = new LogInfo();
            oLog.Message = message;
            oLog.LogDate = DateTime.Now;
            oLog.LogLevel = ARTConstants.LOG_ERROR;
            oLog.Source = ARTConstants.UI_SOURCE_NAME;
            AppUserInfo oAppUser = Helper.GetAppUserInfo();
            oUtilityClient.LogError(oLog, oAppUser);
        }

        public static void LogErrorViaService(Exception ex)
        {
            if (ex is ThreadAbortException)
                return;
            IUtility oUtilityClient = RemotingHelper.GetUtilityObject();
            LogInfo oLog = new LogInfo();
            oLog.Message = ex.Message;
            oLog.StackTrace = ex.StackTrace;
            oLog.LogDate = DateTime.Now;
            oLog.LogLevel = ARTConstants.LOG_ERROR;
            oLog.Source = ARTConstants.UI_SOURCE_NAME;
            AppUserInfo oAppUser = Helper.GetAppUserInfo();
            oUtilityClient.LogError(oLog, oAppUser);
        }

        #endregion

        public static void AddMenuItemSeparator(Menu menuParent)
        {
            //MenuItem miSeperator = new MenuItem();
            //miSeperator.SeparatorImageUrl = "~/App_Themes/SkyStemBlueBrown/Images/MenuDivider.gif";
            //menuParent.Items.Add(miSeperator);
        }

        public static string GetUserFullName()
        {
            UserHdrInfo oUserHdrInfo = SessionHelper.GetCurrentUser();
            return oUserHdrInfo.Name;
        }

        public static string GetUserFullName(int? userID)
        {
            string userName = "";
            if (userID == SessionHelper.CurrentUserID.Value)
            {
                userName = Helper.GetUserFullName();
            }
            else
            {
                IUser oUserClient = RemotingHelper.GetUserObject();
                UserHdrInfo oUserHdrInfo = oUserClient.GetUserDetail(userID.Value, Helper.GetAppUserInfo());
                userName = oUserHdrInfo.Name;
            }
            return userName;
        }

        #region "Master Page related methods"
        /// <summary>
        /// Show the Notes / Input Requirements Sections
        /// </summary>
        /// <param name="PageBase">pass "this"</param>
        /// <param name="oLabelIDCollection">, separated LabelIDs for Input Requirements</param>
        public static void ShowInputRequirementSection(PageBase oPageBase, params int[] oLabelIDCollection)
        {
            MasterPageBase oMasterPageBase;
            if (oPageBase.Master is MasterPageBase)
            {
                oMasterPageBase = (MasterPageBase)oPageBase.Master;
            }
            else
            {
                oMasterPageBase = (MasterPageBase)oPageBase.Master.Master;
            }
            oMasterPageBase.ShowInputRequirements(oLabelIDCollection);
        }

        public static void ShowRequirement(PageBase oPageBase, int label, params int[] oLabelIDCollection)
        {

            MasterPageBase oMasterPageBase;
            if (oPageBase.Master is MasterPageBase)
            {
                oMasterPageBase = (MasterPageBase)oPageBase.Master;
            }
            else
            {
                oMasterPageBase = (MasterPageBase)oPageBase.Master.Master;
            }
            oMasterPageBase.ShowRequirement(label, oLabelIDCollection);
        }

        public static void ShowInputRequirementSectionForPopup(PopupPageBase oPageBase, params int[] oLabelIDCollection)
        {
            PopupMasterPageBase oMasterPageBase;
            if (oPageBase.Master is PopupMasterPageBase)
            {
                oMasterPageBase = (PopupMasterPageBase)oPageBase.Master;
            }
            else
            {
                oMasterPageBase = (PopupMasterPageBase)oPageBase.Master.Master;
            }
            oMasterPageBase.ShowInputRequirements(oLabelIDCollection);
        }

        /// <summary>
        /// Set the Title of Page
        /// </summary>
        /// <param name="oPageBase">pass "this"</param>
        /// <param name="PageTitleLabelID">Label ID for the Page Title</param>
        public static void SetPageTitle(PageBase oPageBase, int PageTitleLabelID)
        {
            oPageBase.Title = LanguageUtil.GetValue(PageTitleLabelID);
            oPageBase.PageTitleLabelID = PageTitleLabelID;
            MasterPageBase oMasterPageBase;
            if (oPageBase.Master is MasterPageBase)
            {
                oMasterPageBase = (MasterPageBase)oPageBase.Master;
            }
            else
            {
                oMasterPageBase = (MasterPageBase)oPageBase.Master.Master;
            }
            oMasterPageBase.SetPageTitle(PageTitleLabelID);
        }

        /// <summary>
        /// Set Wizard Step Titles
        /// </summary>
        /// <param name="wzWizard"></param>
        public static void SetWizardStepTitle(Wizard wzWizard)
        {
            foreach (WizardStep wzStep in wzWizard.WizardSteps)
            {
                UserControlWizardBase oUserControlWizardBase = GetUserControlWizardBase(wzStep);
                wzStep.Title = LanguageUtil.GetValue(oUserControlWizardBase.TitlePhraseID);
            }
        }

        /// <summary>
        /// Get User Control From Wizard Step
        /// </summary>
        /// <param name="wzStep"></param>
        /// <returns></returns>
        public static UserControlWizardBase GetUserControlWizardBase(WizardStep wzStep)
        {
            return wzStep.Controls[1] as UserControlWizardBase;
        }

        /// <summary>
        /// Function to Set Breadcrumbs on Master Page
        /// </summary>
        /// <param name="oPageBase">Reference to the Page itself. Pass "this"</param>
        /// <param name="path">Path to set as Breadcrumbs</param>
        public static void SetBreadcrumbs(PageBase oPageBase, string path)
        {
            MasterPageBase oMasterPageBase;

            if (oPageBase.Master is MasterPageBase)
            {
                oMasterPageBase = (MasterPageBase)oPageBase.Master;
            }
            else
            {
                oMasterPageBase = (MasterPageBase)oPageBase.Master.Master;
            }
            if (oMasterPageBase != null)
                oMasterPageBase.SetBreadcrumbs(path);
        }

        /// <summary>
        /// Function to Set Breadcrumbs on Master Page
        /// </summary>
        /// <param name="oPageBase">Reference to the Page itself. Pass "this"</param>
        /// <param name="oLableIDCollection">Variable Argument List of Label IDs</param>
        public static void SetBreadcrumbs(PageBase oPageBase, params int[] oLableIDCollection)
        {
            StringBuilder oStringBuilder = new StringBuilder();
            foreach (int lableID in oLableIDCollection)
            {
                if (oStringBuilder.ToString() != "")
                    oStringBuilder.Append(ARTConstants.BREADCRUMBS_SEPARATOR);
                oStringBuilder.Append(LanguageUtil.GetValue(lableID));
            }
            Helper.SetBreadcrumbs(oPageBase, oStringBuilder.ToString());
        }

        public static void SetBreadcrumbsForRecForms(PageBase oPageBase, WebEnums.ARTPages eARTPages, params int[] oLableIDCollection)
        {
            StringBuilder oStringBuilder = new StringBuilder();

            // Pareent Menu Name
            oStringBuilder.Append(LanguageUtil.GetValue(1071));

            // Name of Account Viewer / SRA Viewer
            switch (eARTPages)
            {
                case WebEnums.ARTPages.AccountViewer:
                    oStringBuilder.Append(ARTConstants.BREADCRUMBS_SEPARATOR);
                    oStringBuilder.Append(LanguageUtil.GetValue(1187));
                    break;

                case WebEnums.ARTPages.SystemReconciledAccounts:
                    oStringBuilder.Append(ARTConstants.BREADCRUMBS_SEPARATOR);
                    oStringBuilder.Append(LanguageUtil.GetValue(1075));
                    break;
            }

            foreach (int lableID in oLableIDCollection)
            {
                if (oStringBuilder.ToString() != "")
                    oStringBuilder.Append(ARTConstants.BREADCRUMBS_SEPARATOR);
                oStringBuilder.Append(LanguageUtil.GetValue(lableID));
            }
            Helper.SetBreadcrumbs(oPageBase, oStringBuilder.ToString());
        }

        #endregion

        /// <summary>
        /// Check whether the QueryString has been tampered with or not
        /// </summary>
        public static void CheckReferrer()
        {
            if (string.IsNullOrEmpty(HttpContext.Current.Request.ServerVariables["HTTP_REFERER"]))
            {
                SessionHelper.SetCurrentUser(null);
                Helper.RedirectToLogoutPage();
            }
        }


        public static string GetEmailValidationExpression()
        {
            return @"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$";
        }

        // Commented by Apoorv
        //public static string GetLoginID()
        //{
        //    UserHdrInfo objUserHdrInfo = (UserHdrInfo)HttpContext.Current.Session[SessionConstants.USER_INFO];
        //    return objUserHdrInfo.LoginID;
        //}



        public static string GetFullAddress(AddressInfo oAddressInfo)
        {
            string address = string.Empty;
            if (oAddressInfo != null)
            {
                if (!string.IsNullOrEmpty(oAddressInfo.Address1))
                {
                    address += ", " + oAddressInfo.Address1;
                }

                if (!string.IsNullOrEmpty(oAddressInfo.Address2))
                {
                    address += ", " + oAddressInfo.Address2;
                }

                if (!string.IsNullOrEmpty(oAddressInfo.City))
                {
                    address += ", " + oAddressInfo.City;
                }

                if (!string.IsNullOrEmpty(oAddressInfo.State))
                {
                    address += ", " + oAddressInfo.State;
                }

                if (!string.IsNullOrEmpty(oAddressInfo.Zip))
                {
                    address += " - " + oAddressInfo.Zip;
                }

                if (!string.IsNullOrEmpty(oAddressInfo.Country))
                {
                    address += ", " + oAddressInfo.Country;
                }

                if (address.StartsWith(", "))
                {
                    address = address.Substring(2);
                }
            }
            if (address == string.Empty)
                address = WebConstants.HYPHEN;
            return address;
        }

        #region "Display related methods"
        public static string GetDisplayDateRange(DateTime? dtStart, DateTime? dtEnd)
        {
            string period = string.Empty;
            period = Helper.GetDisplayDate(dtStart) + " - " + Helper.GetDisplayDate(dtEnd);
            return period;
        }

        public static string GetDisplayMonthYear(DateTime? oDate)
        {

            if (oDate == null || oDate == default(DateTime))
            {
                return WebConstants.HYPHEN;
            }
            else
            {
                // Todo: This would change to have Multi-lingual Phrase
                return oDate.Value.ToString(LanguageUtil.GetValue(5));
            }
        }

        // '' <summary>
        // '' Function to return "Date" as string from DateTime object (used for display purpose).
        // '' </summary>
        // '' <param name="oDate"></param>
        // '' <returns></returns>
        // '' <remarks></remarks>
        public static string GetDisplayDate(DateTime? oDate)
        {

            if (oDate == null || oDate == default(DateTime))
            {
                return WebConstants.HYPHEN;
            }
            else
            {
                // Todo: This would change to have Multi-lingual Phrase
                return oDate.Value.ToShortDateString();
            }
        }
        public static string GetDisplayDate(DateTime? oDate, MultilingualAttributeInfo oMultilingualAttributeInfo)
        {
            if (oDate == null || oDate == default(DateTime))
            {
                return WebConstants.HYPHEN;
            }
            else
            {
                string dtFormat = LanguageUtil.GetValue(2, oMultilingualAttributeInfo);
                return oDate.Value.ToString(dtFormat);
            }
        }


        public static string GetDisplayDateForCalendar(DateTime? oDate)
        {
            if (oDate == null || oDate == default(DateTime))
            {
                return String.Empty;
                //return "-";
            }
            else
            {
                // Todo: This would change to have Multi-lingual Phrase
                return oDate.Value.ToShortDateString();
            }
        }

        public static string GetDisplayDateTime(DateTime? oDate)
        {
            if (oDate == null || oDate == default(DateTime))
            {
                return WebConstants.HYPHEN;
            }
            else
            {
                // Todo: This would change to have Multi-lingual Phrase
                return oDate.Value.ToString("G");
            }
        }


        #endregion

        #region "Redirect Methods"

        public static void RedirectToLogoutPage()
        {
            SessionHelper.RedirectToUrl(URLConstants.URL_LOGOUT);
        }

        public static void RedirectToHomePage()
        {
            SessionHelper.RedirectToUrl(GetHomePageUrl());
        }

        public static void RedirectToChangePasswordPage()
        {
            SessionHelper.RedirectToUrl(URLConstants.URL_CHANGE_PASSWORD);
        }


        /// <summary>
        /// Redirect to Home Page and Pass a Confirmation Message
        /// </summary>
        /// <param name="LabelID"></param>
        public static void RedirectToHomePage(int LabelID)
        {
            string url = GetHomePageUrl() + "?" + QueryStringConstants.CONFIRMATION_MESSAGE_LABEL_ID + "=" + LabelID.ToString();
            SessionHelper.RedirectToUrl(url);
        }

        public static string GetHomePageUrl()
        {
            string url = "";
            switch ((ARTEnums.UserRole)SessionHelper.CurrentRoleID)
            {
                case ARTEnums.UserRole.SKYSTEM_ADMIN:
                    url = "~/Pages/CompanyList.aspx";
                    break;
                case ARTEnums.UserRole.USER_ADMIN:
                    url = "~/Pages/UserSearch.aspx";
                    break;
                default:
                    url = "~/Pages/Home.aspx";
                    break;
            }
            return url;
        }

        public static string GetErrorPageUrl()
        {
            return "~/Pages/ErrorHandler.aspx";
        }

        /// <summary>
        /// 
        /// </summary>
        public static void RedirectToErrorPage()
        {
            Helper.RedirectToErrorPage(null, false);
        }

        public static void RedirectToErrorPage(int LabelID)
        {
            RedirectToErrorPage(LabelID, false);
        }

        public static void RedirectToErrorPage(int? LabelID, bool bIsSystemError)
        {
            string url = Helper.GetErrorPageUrl();

            if (LabelID != null)
            {
                url += "?" + QueryStringConstants.ERROR_MESSAGE_LABEL_ID + "=" + LabelID.ToString();
            }

            if (bIsSystemError)
            {
                url += "&" + QueryStringConstants.ERROR_MESSAGE_SYSTEM + "=1";
            }
            SessionHelper.RedirectToUrl(url);
        }

        #endregion

        //public static void AddListItemSelect(DropDownList ddl)
        //{
        //    ListItem li = new System.Web.UI.WebControls.ListItem(LanguageUtil.GetValue(1343), WebConstants.SELECT_ONE);
        //    ddl.Items.Insert(0, li);
        //}

        //public static void AddListItemAll(DropDownList ddl)
        //{
        //    ListItem li = new System.Web.UI.WebControls.ListItem(LanguageUtil.GetValue(1262), WebConstants.ALL);
        //    ddl.Items.Insert(0, li);
        //}


        public static string GetCompanyName()
        {
            if (SessionHelper.CurrentCompanyID.GetValueOrDefault() > 0)
            {
                CompanyHdrInfo oCompanyHdrInfo = Helper.GetCompanyInfoLiteObject(SessionHelper.CurrentCompanyID);
                return oCompanyHdrInfo.DisplayName;
            }
            return string.Empty;
        }

        public static string GetRoleName(short? roleID)
        {
            if (roleID.HasValue)
            {
                List<RoleMstInfo> oRoleMstInfoCollection = SessionHelper.GetAllRoles();
                if (oRoleMstInfoCollection != null && oRoleMstInfoCollection.Count > 0)
                {
                    RoleMstInfo oRoleMstInfo = oRoleMstInfoCollection.Find(r => r.RoleID == roleID);
                    if (oRoleMstInfo != null)
                        return oRoleMstInfo.Role;
                }
            }
            return string.Empty;
        }


        public static string GetCurrentRoleName()
        {
            return Helper.GetRoleName(SessionHelper.CurrentRoleID);
        }

        public static string GetLabelIDValue(int value)
        {
            string valueLabelID = LanguageUtil.GetValue(value);
            return valueLabelID;
        }
        public static string GetLabelIDValue(int value, MultilingualAttributeInfo oMultilingualAttributeInfo)
        {
            string valueLabelID = LanguageUtil.GetValue(value, oMultilingualAttributeInfo);
            return valueLabelID;
        }

        public static string GetLabelIDValueEnum(int value)
        {
            string statusLabel = "";
            // Convert to Enum
            ARTEnums.Status eSatus = (ARTEnums.Status)Enum.Parse(typeof(ARTEnums.Status), value.ToString(), true);

            switch (eSatus)
            {
                case ARTEnums.Status.All:
                    statusLabel = GetLabelIDValue(1262);

                    break;
                case ARTEnums.Status.Active:
                    statusLabel = GetLabelIDValue(1274);

                    break;
                case ARTEnums.Status.Inactive:
                    statusLabel = GetLabelIDValue(1342);

                    break;
                default:

                    break;
            }
            return statusLabel;
        }
        public static string GetLabelIDValueUserStatusEnum(int value)
        {
            string statusLabel = "";
            ARTEnums.UserStatus eSatus = (ARTEnums.UserStatus)Enum.Parse(typeof(ARTEnums.UserStatus), value.ToString(), true);

            switch (eSatus)
            {
                case ARTEnums.UserStatus.All:
                    statusLabel = GetLabelIDValue(1262);

                    break;
                case ARTEnums.UserStatus.Activated:
                    statusLabel = GetLabelIDValue(2797);

                    break;
                case ARTEnums.UserStatus.Deactivated:
                    statusLabel = GetLabelIDValue(2798);

                    break;
                default:

                    break;
            }
            return statusLabel;
        }

        public static string GetLabelIDValueActivationStatusEnum(int value)
        {
            string statusLabel = "";
            ARTEnums.ActivationStatus eSatus = (ARTEnums.ActivationStatus)Enum.Parse(typeof(ARTEnums.ActivationStatus), value.ToString(), true);

            switch (eSatus)
            {
                case ARTEnums.ActivationStatus.All:
                    statusLabel = GetLabelIDValue(1262);

                    break;
                case ARTEnums.ActivationStatus.Activated:
                    statusLabel = GetLabelIDValue((int)ARTEnums.ActivationStatusLabelID.Activated);

                    break;
                case ARTEnums.ActivationStatus.Deactivated:
                    statusLabel = GetLabelIDValue((int)ARTEnums.ActivationStatusLabelID.Deactivated);

                    break;
                default:

                    break;
            }
            return statusLabel;
        }

        public static string GetTemplateName(short value)
        {
            string templateName;
            // Convert to Enum
            ARTEnums.ReconciliationItemTemplate eRecItemTemplate = (ARTEnums.ReconciliationItemTemplate)Enum.Parse(typeof(ARTEnums.ReconciliationItemTemplate), value.ToString(), true);

            switch (eRecItemTemplate)
            {
                case ARTEnums.ReconciliationItemTemplate.BankForm:
                    templateName = "Bank Account Form";
                    break;
                case ARTEnums.ReconciliationItemTemplate.DerivedCalculationForm:
                    templateName = "Derived Calculation Form";
                    break;
                case ARTEnums.ReconciliationItemTemplate.Subledgerform:
                    templateName = "Subledger form";
                    break;
                default:
                    templateName = "Bank Account Form";
                    break;
            }
            return templateName;
        }

        //public static string GetRiskRatingAttribute(short value)
        //{
        //    string riskRatingValue;
        //    switch (value)
        //    {
        //        case 1:
        //            riskRatingValue = "High";
        //            break;
        //        case 2:
        //            riskRatingValue = "Medium";
        //            break;
        //        case 3:
        //            riskRatingValue = "Low";
        //            break;
        //        default:
        //            riskRatingValue = "High";
        //            break;
        //    }
        //    return riskRatingValue;
        //}

        public static GridGroupByExpression GetGridGroupByExpressionForFSCaption()
        {
            GridGroupByExpression expression = new GridGroupByExpression();
            GridGroupByField gridGroupByField = new GridGroupByField();
            // SelectFields values (appear in header)
            gridGroupByField = new GridGroupByField();
            gridGroupByField.FieldName = "FSCaption";
            //gridGroupByField.HeaderText = "FSCaption: ";
            gridGroupByField.HeaderText = LanguageUtil.GetValue(1337) + ": ";
            //gridGroupByField.HeaderValueSeparator = " for current group: ";
            gridGroupByField.FormatString = "<strong>{0}</strong>";
            expression.SelectFields.Add(gridGroupByField);

            gridGroupByField = new GridGroupByField();
            gridGroupByField.FieldName = "GLBalanceReportingCurrency";
            //gridGroupByField.HeaderText = "    Total GL Balance: ";
            gridGroupByField.HeaderText = "    " + LanguageUtil.GetValue(2662) + ": ";
            gridGroupByField.HeaderValueSeparator = "";
            //gridGroupByField.FormatString = "<strong>{0:0.00}</strong>";
            gridGroupByField.FormatString = "<strong>{0:N}</strong>";
            gridGroupByField.Aggregate = GridAggregateFunction.Sum;
            expression.SelectFields.Add(gridGroupByField);

            gridGroupByField = new GridGroupByField();
            gridGroupByField.FieldName = "FSCaptionLabelID";
            expression.GroupByFields.Add(gridGroupByField);

            return expression;
        }
        //To display AcountEntityHeirarchy
        public static string GetAccountEntityStringToDisplay(long accountID)
        {
            AccountHdrInfo oAccountHdrInfo = new AccountHdrInfo();
            IAccount oAccountClient = RemotingHelper.GetAccountObject();
            oAccountHdrInfo = oAccountClient.GetAccountHdrInfoByAccountID(accountID, SessionHelper.CurrentCompanyID.Value, SessionHelper.CurrentReconciliationPeriodID.Value, Helper.GetAppUserInfo());
            LanguageHelper.TranslateLabelOrganizationalHierarchyInfo(oAccountHdrInfo);
            List<AccountHdrInfo> oAccountHdrInfoCollection = new List<AccountHdrInfo>();
            oAccountHdrInfoCollection.Add(oAccountHdrInfo);
            oAccountHdrInfoCollection = LanguageHelper.TranslateLabelsAccountHdr(oAccountHdrInfoCollection);

            string returnAccountEntityString = "";
            StringBuilder accountDetail = new StringBuilder();
            AddSeparater(oAccountHdrInfo.Key1, accountDetail);
            AddSeparater(oAccountHdrInfo.Key2, accountDetail);
            AddSeparater(oAccountHdrInfo.Key3, accountDetail);
            AddSeparater(oAccountHdrInfo.Key4, accountDetail);
            AddSeparater(oAccountHdrInfo.Key5, accountDetail);
            AddSeparater(oAccountHdrInfo.Key6, accountDetail);
            AddSeparater(oAccountHdrInfo.Key7, accountDetail);
            AddSeparater(oAccountHdrInfo.Key8, accountDetail);
            AddSeparater(oAccountHdrInfo.Key9, accountDetail);
            //AddSeparater(oAccountHdrInfo.FSCaption, accountDetail);
            AddSeparater(oAccountHdrInfo.AccountNumber, accountDetail);
            AddSeparater(oAccountHdrInfo.AccountName, accountDetail);
            returnAccountEntityString = accountDetail.ToString();

            string s1 = string.Empty;
            if (returnAccountEntityString.EndsWith(WebConstants.ACCOUNT_ENTITY_SEPARATOR))
            {
                s1 = returnAccountEntityString.Substring(0, returnAccountEntityString.Length - WebConstants.ACCOUNT_ENTITY_SEPARATOR.Length);
            }
            return s1;
        }

        public static string GetAccountEntityStringToDisplay(AccountHdrInfo oAccountHdrInfo)
        {
            LanguageHelper.TranslateLabelOrganizationalHierarchyInfo(oAccountHdrInfo);
            List<AccountHdrInfo> oAccountHdrInfoCollection = new List<AccountHdrInfo>();
            oAccountHdrInfoCollection.Add(oAccountHdrInfo);
            oAccountHdrInfoCollection = LanguageHelper.TranslateLabelsAccountHdr(oAccountHdrInfoCollection);
            string returnAccountEntityString = "";
            StringBuilder accountDetail = new StringBuilder();
            AddSeparater(oAccountHdrInfo.Key1, accountDetail);
            AddSeparater(oAccountHdrInfo.Key2, accountDetail);
            AddSeparater(oAccountHdrInfo.Key3, accountDetail);
            AddSeparater(oAccountHdrInfo.Key4, accountDetail);
            AddSeparater(oAccountHdrInfo.Key5, accountDetail);
            AddSeparater(oAccountHdrInfo.Key6, accountDetail);
            AddSeparater(oAccountHdrInfo.Key7, accountDetail);
            AddSeparater(oAccountHdrInfo.Key8, accountDetail);
            AddSeparater(oAccountHdrInfo.Key9, accountDetail);
            //AddSeparater(oAccountHdrInfo.FSCaption, accountDetail);
            AddSeparater(oAccountHdrInfo.AccountNumber, accountDetail);
            AddSeparater(oAccountHdrInfo.AccountName, accountDetail);
            returnAccountEntityString = accountDetail.ToString();

            string s1 = string.Empty;
            if (returnAccountEntityString.EndsWith(WebConstants.ACCOUNT_ENTITY_SEPARATOR))
            {
                s1 = returnAccountEntityString.Substring(0, returnAccountEntityString.Length - WebConstants.ACCOUNT_ENTITY_SEPARATOR.Length);
            }
            return s1;
        }

        public static string GetAccountEntityStringToDisplay(GLDataHdrInfo oGLDataHdrInfo)
        {
            string returnAccountEntityString = "";
            StringBuilder accountDetail = new StringBuilder();
            AddSeparater(oGLDataHdrInfo.Key1, accountDetail);
            AddSeparater(oGLDataHdrInfo.Key2, accountDetail);
            AddSeparater(oGLDataHdrInfo.Key3, accountDetail);
            AddSeparater(oGLDataHdrInfo.Key4, accountDetail);
            AddSeparater(oGLDataHdrInfo.Key5, accountDetail);
            AddSeparater(oGLDataHdrInfo.Key6, accountDetail);
            AddSeparater(oGLDataHdrInfo.Key7, accountDetail);
            AddSeparater(oGLDataHdrInfo.Key8, accountDetail);
            AddSeparater(oGLDataHdrInfo.Key9, accountDetail);
            //AddSeparater(oAccountHdrInfo.FSCaption, accountDetail);
            AddSeparater(oGLDataHdrInfo.AccountNumber, accountDetail);
            AddSeparater(oGLDataHdrInfo.AccountName, accountDetail);
            returnAccountEntityString = accountDetail.ToString();

            string s1 = string.Empty;
            if (returnAccountEntityString.EndsWith(WebConstants.ACCOUNT_ENTITY_SEPARATOR))
            {
                s1 = returnAccountEntityString.Substring(0, returnAccountEntityString.Length - WebConstants.ACCOUNT_ENTITY_SEPARATOR.Length);
            }
            return s1;
        }

        public static string GetAccountDisplayString(Int64? accountID, Int32? netAccountID)
        {
            String AccountDetails = string.Empty;
            try
            {
                IAccount oAccountClient = RemotingHelper.GetAccountObject();

                if (accountID.HasValue && accountID.Value > 0)
                {
                    AccountDetails = Helper.GetAccountEntityStringToDisplay(accountID.Value);
                }
                else if (netAccountID.HasValue && netAccountID.Value > 0)
                {
                    AccountDetails = LanguageUtil.GetValue(1257) + WebConstants.ACCOUNT_ENTITY_SEPARATOR + oAccountClient.GetNetAccountNameByNetAccountID(netAccountID.Value, SessionHelper.CurrentCompanyID.Value
                        , SessionHelper.GetUserLanguage(), SessionHelper.GetBusinessEntityID(), AppSettingHelper.GetDefaultLanguageID(), Helper.GetAppUserInfo());
                }
            }
            catch (Exception ex)
            {
                Helper.LogException(ex);
            }
            return AccountDetails;
        }

        public static string GetDisplayAccountInfo(AccountHdrInfo oAccountHdrInfo)
        {
            if (oAccountHdrInfo != null)
                return oAccountHdrInfo.AccountNumber.ToString() + "/" + oAccountHdrInfo.AccountName + " ";
            else
                return string.Empty;
        }

        public static string GetDisplayCurrentRecInfo()
        {
            return string.Format(" {0}: {1}", LanguageUtil.GetValue(1420), GetDisplayDate(SessionHelper.CurrentReconciliationPeriodEndDate));
        }

        public static string GetCurrentRecInfo()
        {
            if (SessionHelper.CurrentReconciliationPeriodEndDate.HasValue)
                return ((DateTime)SessionHelper.CurrentReconciliationPeriodEndDate).ToString("mmddyyyy");
            else
                return string.Empty;
        }

        public static AccountHdrInfo GetAccountHdrInfo(long AccountID)
        {
            AccountHdrInfo oAccountHdrInfo = new AccountHdrInfo();
            IAccount oAccountClient = RemotingHelper.GetAccountObject();
            oAccountHdrInfo = oAccountClient.GetAccountHdrInfoByAccountID(AccountID, SessionHelper.CurrentCompanyID.Value, SessionHelper.CurrentReconciliationPeriodID.Value, Helper.GetAppUserInfo());
            return oAccountHdrInfo;
        }


        private static StringBuilder AddSeparater(string input, StringBuilder accountDetail)
        {
            string returnString = "";
            if (input != "")
            {
                returnString = input + WebConstants.ACCOUNT_ENTITY_SEPARATOR;
            }
            accountDetail.Append(returnString);
            return accountDetail;
        }

        public static string GetDisplayIntegerValue(long? val)
        {
            if (val == null)
            {
                return WebConstants.HYPHEN;
            }
            else if (val == 0)
            {
                return val.Value.ToString();
            }
            else
            {
                return val.Value.ToString("#,#");
            }
        }
        public static string GetDisplayIntegerValueForTextBox(long? val)
        {
            if (val == null)
            {
                return string.Empty;
            }
            else if (val == 0)
            {
                return val.Value.ToString();
            }
            else
            {
                return val.Value.ToString("#");
            }
        }
        public static string GetDisplayIntegerValueWitoutComma(long? val)
        {
            if (val == null)
            {
                return WebConstants.HYPHEN;
            }
            else
            {
                return val.Value.ToString();
            }
        }

        public static string GetDisplayIntegerValueWithBracket(long? val, bool IsExportToExcel)
        {
            string str = string.Empty;
            if (val == null || val == 0)
            {
                str = "0";
            }
            else
            {
                str = val.Value.ToString("#,#");
            }
            if (IsExportToExcel)
                return str;
            return string.Format(WebConstants.FORMAT_BRACKET, str);
        }

        public static string GetDisplayDecimalValue(Decimal? val)
        {

            if (val == null || val == WebConstants.NULL_DECIMAL)
            {
                return WebConstants.HYPHEN;
            }
            else
            {
                return val.Value.ToString("N");
            }
        }

        public static string GetDisplayExchangeRateValue(Decimal? val)
        {

            if (val == null || val == WebConstants.NULL_DECIMAL)
            {
                return WebConstants.HYPHEN;
            }
            else
            {
                NumberFormatInfo oNumberFormatInfo = new NumberFormatInfo();
                oNumberFormatInfo.NumberDecimalDigits = TestConstant.DECIMAL_PLACES_FOR_EXCHANGE_RATE_ROUND;
                return val.Value.ToString("N", oNumberFormatInfo);
            }
        }

        public static string GetCurrencyValue(Decimal? val, string currency)
        {
            String currencyValue = currency + " " + GetDisplayDecimalValue(val);
            return currencyValue;
        }

        public static string GetDisplayReportingCurrencyValue(Decimal? val)
        {
            if (val.HasValue || val == WebConstants.NULL_DECIMAL)
            {
                String currencyValue = SessionHelper.ReportingCurrencyCode + " " + GetDisplayDecimalValue(val);
                return currencyValue;
            }
            return WebConstants.HYPHEN;
        }



        public static string GetDisplayCurrencyValue(string currencyCode, Decimal? val)
        {
            string currencyValue = WebConstants.HYPHEN;
            if (val.HasValue || val == WebConstants.NULL_DECIMAL)
            {
                currencyValue = currencyCode + " " + GetDisplayDecimalValue(val);
            }
            return currencyValue;
        }


        public static string GetDisplayBaseCurrencyCode(string baseCurrencyCode)
        {
            if (string.IsNullOrEmpty(baseCurrencyCode))
                return WebConstants.HYPHEN;
            else
                return baseCurrencyCode;
        }
        public static string GetDisplayBaseCurrencyValue(string baseCurrencyCode, decimal? val)
        {
            string currencyValue = WebConstants.HYPHEN;
            if (!String.IsNullOrEmpty(baseCurrencyCode))
            {
                currencyValue = GetDisplayDecimalValue(val);
            }
            return currencyValue;
        }
        public static string GetDisplayPercentageValue(Decimal? val)
        {

            if (val == null || val == WebConstants.NULL_DECIMAL)
            {
                return WebConstants.HYPHEN;
            }
            else
            {
                return GetDisplayDecimalValue(val) + " %";
            }
        }

        public static string GetDecimalValueForTextBox(Decimal? val, int decimals)
        {

            if ((val == null))
            {
                return String.Empty;
            }
            else
            {

                if (decimals == 0)
                {
                    return val.Value.ToString("#0.00");
                }

                else
                {
                    return val.Value.ToString("#0.00");
                }
            }
        }

        /// <summary>
        /// Gets the display value.
        /// </summary>
        /// <param name="val">The val.</param>
        /// <param name="eDataType">Type of the e data.</param>
        /// <returns></returns>
        public static string GetDisplayValue(object val, WebEnums.DataType eDataType)
        {
            string displayVal;
            if (val is DBNull)
            {
                return Helper.GetDisplayStringValue(null);
            }

            switch (eDataType)
            {
                case WebEnums.DataType.DataTime:
                    displayVal = Helper.GetDisplayDate(Convert.ToDateTime(val));
                    break;
                case WebEnums.DataType.Decimal:
                    displayVal = Helper.GetDisplayDecimalValue(Convert.ToDecimal(val));
                    break;
                case WebEnums.DataType.Integer:
                    displayVal = Helper.GetDisplayIntegerValueWitoutComma(Convert.ToInt32(val));
                    break;
                default:
                    displayVal = Helper.GetDisplayStringValue((string)val);
                    break;
            }
            return displayVal;
        }

        public static void SetGridImageButtonProperties(GridColumnCollection oGridColumnCollection)
        {
            foreach (GridColumn col in oGridColumnCollection)
            {
                if (col.UniqueName == "DeleteColumn")
                {
                    (col as ExGridButtonColumn).ButtonType = GridButtonColumnType.ImageButton;
                    (col as ExGridButtonColumn).ImageUrl = URLConstants.URL_IMAGE_DELETE;
                    (col as ExGridButtonColumn).Text = LanguageUtil.GetValue(1564);
                    (col as ExGridButtonColumn).ConfirmDialogType = GridConfirmDialogType.Classic;
                    (col as ExGridButtonColumn).ConfirmText = LanguageUtil.GetValue(1612);
                    (col as ExGridButtonColumn).ConfirmTitle = LanguageUtil.GetValue(1613);

                }
                if (col.UniqueName == "CopyColumn")
                {
                    (col as ExGridButtonColumn).ButtonType = GridButtonColumnType.ImageButton;
                    (col as ExGridButtonColumn).ImageUrl = URLConstants.URL_IMAGE_COPY;
                    (col as ExGridButtonColumn).Text = LanguageUtil.GetValue(2783);
                }
                if (col.UniqueName == "CopyAndCloseColumn")
                {
                    (col as ExGridButtonColumn).ButtonType = GridButtonColumnType.ImageButton;
                    (col as ExGridButtonColumn).ImageUrl = URLConstants.URL_IMAGE_COPY_AND_CLOSE;
                    (col as ExGridButtonColumn).Text = LanguageUtil.GetValue(2784);
                }
                if (col.UniqueName == "EditCommandColumn")
                {

                    (col as ExGridEditCommandColumn).ButtonType = GridButtonColumnType.ImageButton;

                    (col as ExGridEditCommandColumn).EditText = LanguageUtil.GetValue(1429);
                    (col as ExGridEditCommandColumn).EditImageUrl = URLConstants.URL_IMAGE_EDIT;

                    //Cancel Button
                    (col as ExGridEditCommandColumn).CancelText = LanguageUtil.GetValue(1239);
                    (col as ExGridEditCommandColumn).CancelImageUrl = URLConstants.URL_IMAGE_CANCEL; ;

                    //Update Button
                    (col as ExGridEditCommandColumn).UpdateText = LanguageUtil.GetValue(1610);
                    (col as ExGridEditCommandColumn).UpdateImageUrl = URLConstants.URL_IMAGE_UPDATE;

                    //Update Button
                    (col as ExGridEditCommandColumn).InsertText = LanguageUtil.GetValue(1611);
                    (col as ExGridEditCommandColumn).InsertImageUrl = URLConstants.URL_IMAGE_UPDATE;
                }
            }
        }

        public static void SetHyperLinkForOrganizationalHierarchyColumns(string navigateUrl, GridItemEventArgs e)
        {
            if (e.Item.ItemType == GridItemType.Item || e.Item.ItemType == GridItemType.AlternatingItem)
            {
                ExHyperLink hlKey2 = (ExHyperLink)e.Item.FindControl("hlKey2");
                ExHyperLink hlKey3 = (ExHyperLink)e.Item.FindControl("hlKey3");
                ExHyperLink hlKey4 = (ExHyperLink)e.Item.FindControl("hlKey4");
                ExHyperLink hlKey5 = (ExHyperLink)e.Item.FindControl("hlKey5");
                ExHyperLink hlKey6 = (ExHyperLink)e.Item.FindControl("hlKey6");
                ExHyperLink hlKey7 = (ExHyperLink)e.Item.FindControl("hlKey7");
                ExHyperLink hlKey8 = (ExHyperLink)e.Item.FindControl("hlKey8");
                ExHyperLink hlKey9 = (ExHyperLink)e.Item.FindControl("hlKey9");
                ExHyperLink hlFSCaption = (ExHyperLink)e.Item.FindControl("hlFSCaption");
                ExHyperLink hlAccountType = (ExHyperLink)e.Item.FindControl("hlAccountType");

                if (hlKey2 != null)
                {
                    hlKey2.NavigateUrl = navigateUrl;
                    if (string.IsNullOrEmpty(navigateUrl))
                    {
                        hlKey2.CssClass = "";
                    }
                    else
                    {
                        hlKey2.CssClass = "GridHyperLink";
                    }
                }

                if (hlKey3 != null)
                {
                    hlKey3.NavigateUrl = navigateUrl;
                    if (string.IsNullOrEmpty(navigateUrl))
                    {
                        hlKey3.CssClass = "";
                    }
                    else
                    {
                        hlKey3.CssClass = "GridHyperLink";
                    }
                }

                if (hlKey4 != null)
                {
                    hlKey4.NavigateUrl = navigateUrl;
                    if (string.IsNullOrEmpty(navigateUrl))
                    {
                        hlKey4.CssClass = "";
                    }
                    else
                    {
                        hlKey4.CssClass = "GridHyperLink";
                    }
                }

                if (hlKey5 != null)
                {
                    hlKey5.NavigateUrl = navigateUrl;
                    if (string.IsNullOrEmpty(navigateUrl))
                    {
                        hlKey5.CssClass = "";
                    }
                    else
                    {
                        hlKey5.CssClass = "GridHyperLink";
                    }
                }

                if (hlKey6 != null)
                {
                    hlKey6.NavigateUrl = navigateUrl;
                    if (string.IsNullOrEmpty(navigateUrl))
                    {
                        hlKey6.CssClass = "";
                    }
                    else
                    {
                        hlKey6.CssClass = "GridHyperLink";
                    }
                }

                if (hlKey7 != null)
                {
                    hlKey7.NavigateUrl = navigateUrl;
                    if (string.IsNullOrEmpty(navigateUrl))
                    {
                        hlKey7.CssClass = "";
                    }
                    else
                    {
                        hlKey7.CssClass = "GridHyperLink";
                    }
                }

                if (hlKey8 != null)
                {
                    hlKey8.NavigateUrl = navigateUrl;
                    if (string.IsNullOrEmpty(navigateUrl))
                    {
                        hlKey8.CssClass = "";
                    }
                    else
                    {
                        hlKey8.CssClass = "GridHyperLink";
                    }
                }

                if (hlKey9 != null)
                {
                    hlKey9.NavigateUrl = navigateUrl;
                    if (string.IsNullOrEmpty(navigateUrl))
                    {
                        hlKey9.CssClass = "";
                    }
                    else
                    {
                        hlKey9.CssClass = "GridHyperLink";
                    }
                }

                if (hlFSCaption != null)
                {
                    hlFSCaption.NavigateUrl = navigateUrl;
                    if (string.IsNullOrEmpty(navigateUrl))
                    {
                        hlFSCaption.CssClass = "";
                    }
                    else
                    {
                        hlFSCaption.CssClass = "GridHyperLink";
                    }
                }

                if (hlAccountType != null)
                {
                    hlAccountType.NavigateUrl = navigateUrl;
                    if (string.IsNullOrEmpty(navigateUrl))
                    {
                        hlAccountType.CssClass = "";
                    }
                    else
                    {
                        hlAccountType.CssClass = "GridHyperLink";
                    }
                }
            }
        }

        public static int GetPageTitlePhraseID(string pageName)
        {
            int phraseID;
            switch (pageName)
            {
                case "TemplateBankAccountForm.aspx":
                    phraseID = 1355;
                    break;
                case "TemplateAccruableItemsForm.aspx":
                    phraseID = 1439;
                    break;
                case "TemplateAmortizableAccountsForm.aspx":
                    phraseID = 1438;
                    break;
                case "TemplateDerivedCalculationForm.aspx":
                    phraseID = 1099;
                    break;
                case "TemplateItemizedListForm.aspx":
                    phraseID = 1103;
                    break;
                case "TemplateSubledgerForm.aspx":
                    phraseID = 1100;
                    break;
                default:
                    phraseID = -1;
                    break;
            }
            return phraseID;
        }



        public static string GetAlertMessageFromLabelID(int labelID)
        {
            string message = "No Selection Made";
            message = GetLabelIDValue(labelID);
            return message;
        }


        /// <summary>
        /// Get the Rec Period Status Info Object 
        /// </summary>
        /// <param name="recPeriodStatusID"></param>
        /// <returns></returns>
        internal static ReconciliationPeriodStatusMstInfo GetRecPeriodStatusInfo(short? recPeriodStatusID)
        {
            List<ReconciliationPeriodStatusMstInfo> oReconciliationPeriodStatusMstInfoCollection = CacheHelper.GetAllRecPeriodStatuses();
            return oReconciliationPeriodStatusMstInfoCollection.Find(c => c.ReconciliationPeriodStatusID == recPeriodStatusID);
        }

        /// <summary>
        /// This method gives RecPeriodStatus for any recPeriod
        /// Note: SessionHelper.GetRecPeriodStatus() already gives it but only for current Rec( on master page DDL).
        /// </summary>
        /// <returns></returns>
        public static ReconciliationPeriodStatusMstInfo GetRecPeriodStatusByRecPeriodID(int? reconciliationPeriodID)
        {
            /* 
             * All Rec Process available in Cache 
             * Get list from Cache 
             * Get Status ID
             * If In Progress, go to DB to check for Closed
             * Get Label from Cache
            */

            ReconciliationPeriodStatusMstInfo oReconciliationPeriodStatusMstInfo = null;
            if (reconciliationPeriodID.HasValue)
            {
                List<ReconciliationPeriodInfo> oReconciliationPeriodInfoCollection = CacheHelper.GetAllReconciliationPeriods(null);
                ReconciliationPeriodInfo oReconciliationPeriodInfo = oReconciliationPeriodInfoCollection.Find(c => c.ReconciliationPeriodID == reconciliationPeriodID.Value);

                // Get Rec Period Status Info
                oReconciliationPeriodStatusMstInfo = Helper.GetRecPeriodStatusInfo(oReconciliationPeriodInfo.ReconciliationPeriodStatusID);

                WebEnums.RecPeriodStatus eRecProcessStatus = (WebEnums.RecPeriodStatus)System.Enum.Parse(typeof(WebEnums.RecPeriodStatus), oReconciliationPeriodStatusMstInfo.ReconciliationPeriodStatusID.Value.ToString(), true);

                // If Status = IN Progress, then check DB once again
                switch (eRecProcessStatus)
                {
                    case WebEnums.RecPeriodStatus.InProgress:
                        // Check for Closed
                        IReconciliationPeriod oReconciliationPeriodClient = RemotingHelper.GetReconciliationPeriodObject();
                        oReconciliationPeriodStatusMstInfo = oReconciliationPeriodClient.GetRecPeriodStatus(reconciliationPeriodID, Helper.GetAppUserInfo());
                        break;
                }
            }
            return oReconciliationPeriodStatusMstInfo;
        }



        /// <summary>
        /// File Name for files (when reconciling the accounts or, for supporting attachents)
        /// </summary>
        /// <param name="validFile"></param>
        /// <param name="importType"></param>
        /// <returns></returns>
        public static string GetFileName(UploadedFile validFile)
        {
            StringBuilder oSb = new StringBuilder();

            // <<FileName>>_<<UploadDateTime>>
            oSb.Append(validFile.GetNameWithoutExtension());
            oSb.Append("_");
            oSb.Append(Helper.GetDisplayDateTime(DateTime.Now));
            oSb.Append(validFile.GetExtension());

            foreach (char ch in Path.GetInvalidFileNameChars())
            {
                oSb.Replace(ch.ToString(), "");
            }
            oSb.Replace(" ", "");
            return oSb.ToString();
        }

        public static string GetFileName(UploadedFile validFile, int i)
        {
            StringBuilder oSb = new StringBuilder();

            // <<FileName>>_<<UploadDateTime>>
            oSb.Append(validFile.GetNameWithoutExtension());
            oSb.Append("_");
            oSb.Append(Helper.GetDisplayDateTime(DateTime.Now));
            oSb.Append("-");
            oSb.Append(i.ToString());
            oSb.Append(validFile.GetExtension());

            foreach (char ch in Path.GetInvalidFileNameChars())
            {
                oSb.Replace(ch.ToString(), "");
            }
            oSb.Replace(" ", "");
            return oSb.ToString();
        }

        public static string GetNewFileName(string OldFileName, int AttachementNo)
        {
            StringBuilder oSb = new StringBuilder();
            string[] arrFileName = OldFileName.Split('_');
            for (int i = 0; i < arrFileName.Length - 1; i++)
            {
                oSb.Append(arrFileName[i]);
            }
            string[] arrExtenssion = arrFileName[arrFileName.Length - 1].Split('.');
            oSb.Append("_");
            oSb.Append(Helper.GetDisplayDateTime(DateTime.Now));
            oSb.Append(AttachementNo);
            oSb.Append(".");
            oSb.Append(arrExtenssion[1]);
            foreach (char ch in Path.GetInvalidFileNameChars())
            {
                oSb.Replace(ch.ToString(), "");
            }
            oSb.Replace(" ", "");
            return oSb.ToString();
        }
        public static string GetNewFilePath(string newFileName)
        {
            string targetFolder = DataImportHelper.GetFolderForAttachment(SessionHelper.CurrentCompanyID.Value, (int)SessionHelper.CurrentReconciliationPeriodID);
            return Path.Combine(targetFolder, newFileName);
        }
        public static void CopyFile(string OriginalFilePath, string newFilePath)
        {
            string path = OriginalFilePath;
            string path2 = newFilePath;
            try
            {
                // Copy the file.
                if (!File.Exists(path2))
                    File.Copy(path, path2, false);
                else
                    throw new ARTException(5000379);
            }

            catch (Exception e)
            {
                Helper.LogException(e);
                throw new ARTException(5000379);
            }

        }

        #region Template Forms
        public static long? GetGLDataIDAndRecStatusForPeriodChange(PageBase oPageBase, long? accountID, int? netAccountID, ARTEnums.ReconciliationItemTemplate eReconciliationItemTemplate, WebEnums.ARTPages? eARTPage)
        {
            Control pnlStatus = oPageBase.Master.Master.FindControl("ContentPlaceHolder1").FindControl("pnlStatus");
            Control cphRecProcess = oPageBase.Master.Master.FindControl("ContentPlaceHolder1").FindControl("cphRecProcess");

            long? gLDataID = 0;
            IGLData oGLDataClient = RemotingHelper.GetGLDataObject();
            List<GLDataAndAccountHdrInfo> oGLDataAndAccountHdrInfoCollection = oGLDataClient.GLDataIDAndRecTemplateIDByAccountIDAndRecPeriodID(accountID, netAccountID, SessionHelper.CurrentReconciliationPeriodID, SessionHelper.CurrentCompanyID, (short?)ARTEnums.AccountAttribute.ReconciliationTemplate, Helper.GetAppUserInfo());
            if (oGLDataAndAccountHdrInfoCollection != null && oGLDataAndAccountHdrInfoCollection.Count > 0)
            {
                gLDataID = oGLDataAndAccountHdrInfoCollection[0].GLDataID;
                if (gLDataID.GetValueOrDefault() == 0)
                {
                    ShowRecPage(pnlStatus, cphRecProcess, false);
                    throw new ARTException(5000120);
                }
                if (oGLDataAndAccountHdrInfoCollection[0].ReconciliationTemplateID != (short?)eReconciliationItemTemplate && eARTPage.HasValue)
                    HttpContext.Current.Response.Redirect(AccountViewerHelper.GetHyperlinkForAccountViewer(oGLDataAndAccountHdrInfoCollection[0].ReconciliationTemplateID, accountID.ToString(), gLDataID.ToString(), netAccountID.ToString(), oGLDataAndAccountHdrInfoCollection[0].IsSystemReconcilied, eARTPage.Value));
                //TODO: also check from DB that template is not changed ( at the moment its avoided for netaccount)
                if (netAccountID.HasValue && netAccountID > 0)
                {
                    if (!gLDataID.HasValue || gLDataID == 0)
                    {
                        ShowRecPage(pnlStatus, cphRecProcess, false);
                        throw new ARTException(5000116);
                    }
                    else
                    {
                        ShowRecPage(pnlStatus, cphRecProcess, true);
                    }
                }
                else
                {
                    bool isTemplateCorrect = (oGLDataAndAccountHdrInfoCollection[0].ReconciliationTemplateID == (short?)eReconciliationItemTemplate);
                    if (!isTemplateCorrect || !gLDataID.HasValue || gLDataID == 0)
                    {
                        ShowRecPage(pnlStatus, cphRecProcess, false);
                        throw new ARTException(5000120);
                    }
                    else
                    {
                        ShowRecPage(pnlStatus, cphRecProcess, true);
                    }
                }
            }
            else
            {
                ShowRecPage(pnlStatus, cphRecProcess, false);
                throw new ARTException(5000116);
            }

            return gLDataID;
        }

        public static long? GetGLDataIDAndRecStatusForPeriodChange(long? accountID, int? netAccountID, ARTEnums.ReconciliationItemTemplate eReconciliationItemTemplate)
        {
            long? gLDataID = 0;
            IGLData oGLDataClient = RemotingHelper.GetGLDataObject();
            List<GLDataAndAccountHdrInfo> oGLDataAndAccountHdrInfoCollection = oGLDataClient.GLDataIDAndRecTemplateIDByAccountIDAndRecPeriodID(accountID, netAccountID, SessionHelper.CurrentReconciliationPeriodID, SessionHelper.CurrentCompanyID, (short?)ARTEnums.AccountAttribute.ReconciliationTemplate, Helper.GetAppUserInfo());
            if (oGLDataAndAccountHdrInfoCollection != null && oGLDataAndAccountHdrInfoCollection.Count > 0)
            {
                gLDataID = oGLDataAndAccountHdrInfoCollection[0].GLDataID;
                //TODO: also check from DB that template is not changed ( at the moment its avoided for netaccount)
            }
            return gLDataID;
        }

        public static long? GetGLDataID(PageBase oPageBase, long? accountID, int? netAccountID, ARTEnums.ReconciliationItemTemplate eReconciliationItemTemplate, WebEnums.ARTPages eARTPage)
        {
            long? gLDataID = null;
            try
            {
                gLDataID = GetGLDataIDAndRecStatusForPeriodChange(oPageBase, accountID, netAccountID, eReconciliationItemTemplate, eARTPage);
            }
            catch (ARTException artEx)
            {
                Helper.ShowErrorMessage(oPageBase, artEx);
            }
            return gLDataID;
        }

        public static long? GetGLDataID(PageBase oPageBase, long? accountID, int? netAccountID)
        {
            long? gLDataID = null;
            try
            {
                IGLData oGLDataClient = RemotingHelper.GetGLDataObject();
                List<GLDataAndAccountHdrInfo> oGLDataAndAccountHdrInfoCollection = oGLDataClient.GLDataIDAndRecTemplateIDByAccountIDAndRecPeriodID(accountID, netAccountID, SessionHelper.CurrentReconciliationPeriodID, SessionHelper.CurrentCompanyID, (short?)ARTEnums.AccountAttribute.ReconciliationTemplate, Helper.GetAppUserInfo());
                if (oGLDataAndAccountHdrInfoCollection != null && oGLDataAndAccountHdrInfoCollection.Count > 0)
                {
                    gLDataID = oGLDataAndAccountHdrInfoCollection[0].GLDataID;
                }
                return gLDataID;
            }
            catch (ARTException artEx)
            {
                Helper.ShowErrorMessage(oPageBase, artEx);
            }
            return gLDataID;
        }

        public static void ValidateRecTemplateForGLDataID(PageBase oPageBase, GLDataHdrInfo oGLDataHdrInfo, ARTEnums.ReconciliationItemTemplate eReconciliationItemTemplate, WebEnums.ARTPages? eARTPage)
        {
            try
            {
                Control pnlStatus = oPageBase.Master.Master.FindControl("ContentPlaceHolder1").FindControl("pnlStatus");
                Control cphRecProcess = oPageBase.Master.Master.FindControl("ContentPlaceHolder1").FindControl("cphRecProcess");

                if (oGLDataHdrInfo == null)
                {
                    ShowRecPage(pnlStatus, cphRecProcess, false);
                    throw new ARTException(5000116);
                }
                else
                {
                    if (oGLDataHdrInfo.GLDataID.GetValueOrDefault() == 0)
                    {
                        ShowRecPage(pnlStatus, cphRecProcess, false);
                        throw new ARTException(5000120);
                    }
                    else
                    {
                        ShowRecPage(pnlStatus, cphRecProcess, true);
                    }
                    if (oGLDataHdrInfo.ReconciliationTemplateID != (short?)eReconciliationItemTemplate && eARTPage.HasValue)
                        HttpContext.Current.Response.Redirect(AccountViewerHelper.GetHyperlinkForAccountViewer(oGLDataHdrInfo.ReconciliationTemplateID, oGLDataHdrInfo.AccountID.ToString(), oGLDataHdrInfo.GLDataID.ToString(), oGLDataHdrInfo.NetAccountID.ToString(), oGLDataHdrInfo.IsSystemReconcilied, eARTPage.Value));
                }
            }
            catch (ARTException artEx)
            {
                Helper.ShowErrorMessage(oPageBase, artEx);
            }
        }

        private static void ShowRecPage(Control c1, Control c2, bool b)
        {
            if (c1 != null && c2 != null)
            {
                c1.Visible = b;
                c2.Visible = b;
            }
        }


        public static string SetDocumentUploadURL(long? recordID, long? accountID, int? netAccountID, bool? isSRA, string requestURL, out string windowName)
        {
            //string url = Request.Url.ToString();
            StringBuilder sbURL = new StringBuilder();

            //sbURL.Append(requestURL.Substring(0, requestURL.LastIndexOf("/")));
            sbURL.Append("DocumentUpload.aspx");
            //sbURL.Append("?" + QueryStringConstants.RECORD_ID + "=" + _gLDataID);
            sbURL.Append("?" + QueryStringConstants.RECORD_ID + "=" + recordID);
            sbURL.Append("&" + QueryStringConstants.RECORD_TYPE_ID + "=" + (short)WebEnums.RecordType.GLDataHdr);
            sbURL.Append("&" + QueryStringConstants.ACCOUNT_ID + "=" + accountID);
            sbURL.Append("&" + QueryStringConstants.NETACCOUNT_ID + "=" + netAccountID);
            if (isSRA.HasValue && isSRA.Value)
            {
                sbURL.Append("&" + QueryStringConstants.IS_SRA + "=1");
            }
            else
            {
                sbURL.Append("&" + QueryStringConstants.IS_SRA + "=0");
            }

            //this.ucDocumentUpload.URL = sbURL.ToString();
            //this.ucDocumentUpload.WindowName = "Document Upload";
            windowName = "Document Upload";
            return sbURL.ToString();
        }

        public static string SetDocumentUploadURLfrombankAccountForm(short edit, long? recordID, long? accountID, int? netAccountID, bool? isSRA, string requestURL, out string windowName)
        {
            //string url = Request.Url.ToString();
            StringBuilder sbURL = new StringBuilder();
            //sbURL.Append(requestURL.Substring(0, requestURL.LastIndexOf("/")));
            sbURL.Append("DocumentUpload.aspx");
            //sbURL.Append("?" + QueryStringConstants.RECORD_ID + "=" + _gLDataID);
            sbURL.Append("?" + QueryStringConstants.RECORD_ID + "=" + recordID);
            sbURL.Append("&" + QueryStringConstants.RECORD_TYPE_ID + "=" + (short)WebEnums.RecordType.GLDataHdr);
            sbURL.Append("&" + QueryStringConstants.ACCOUNT_ID + "=" + accountID);
            sbURL.Append("&" + QueryStringConstants.NETACCOUNT_ID + "=" + netAccountID);
            sbURL.Append("&" + QueryStringConstants.READ_ONLY + "=" + edit);

            if (isSRA.HasValue && isSRA.Value)
            {
                sbURL.Append("&" + QueryStringConstants.IS_SRA + "=1");
            }
            else
            {
                sbURL.Append("&" + QueryStringConstants.IS_SRA + "=0");
            }
            //this.ucDocumentUpload.URL = sbURL.ToString();
            //this.ucDocumentUpload.WindowName = "Document Upload";
            windowName = "Document Upload";
            return sbURL.ToString();
        }

        public static string SetDocumentUploadURLForRecItemInput(long? glDataID, long? recordID, long? accountID, int? netAccountID, bool readOnlyMode, string requestURL, out string windowName, bool isForwardedItem, WebEnums.RecordType recordType)
        {
            //string url = Request.Url.ToString();
            StringBuilder sbURL = new StringBuilder();
            //sbURL.Append(requestURL.Substring(0, requestURL.LastIndexOf("/")));
            sbURL.Append("DocumentUpload.aspx");
            sbURL.Append("?" + QueryStringConstants.GLDATA_ID + "=" + glDataID);
            sbURL.Append("&" + QueryStringConstants.RECORD_ID + "=" + recordID);
            sbURL.Append("&" + QueryStringConstants.RECORD_TYPE_ID + "=" + (short)recordType);
            sbURL.Append("&" + QueryStringConstants.ACCOUNT_ID + "=" + accountID);
            sbURL.Append("&" + QueryStringConstants.NETACCOUNT_ID + "=" + netAccountID);
            sbURL.Append("&" + QueryStringConstants.IS_FORWARDED_ITEM + "=" + isForwardedItem);

            if (readOnlyMode == true)
            {
                sbURL.Append("&" + QueryStringConstants.MODE + "=" + WebEnums.FormMode.ReadOnly.ToString());
            }
            else
            {
                sbURL.Append("&" + QueryStringConstants.MODE + "=" + WebEnums.FormMode.Edit.ToString());
            }
            sbURL.Append("&" + QueryStringConstants.ITEM_COUNT_REQUIRED + "=0");
            //this.ucDocumentUpload.URL = sbURL.ToString();
            //this.ucDocumentUpload.WindowName = "Document Upload";
            windowName = "Document Upload";
            return sbURL.ToString();
        }

        public static string SetDocumentUploadURLForTasks(long? recordID, int? recordType, string mode, out string windowName)
        {
            StringBuilder sbURL = new StringBuilder();

            sbURL.Append(URLConstants.URL_TASK_ATTACHMENT);
            sbURL.Append("?" + QueryStringConstants.RECORD_ID + "=" + recordID);
            sbURL.Append("&" + QueryStringConstants.RECORD_TYPE_ID + "=" + recordType);
            sbURL.Append("&" + QueryStringConstants.MODE + "=" + mode);
            windowName = "UploadAttachment";
            return sbURL.ToString();
        }

        public static string GetOverrideExchangeRateURLForRecItemInput(long? accountID, long? netAccountID, decimal? exRateLCCYtoBCCY, decimal? exRateLCCYtoRCCY)
        {
            StringBuilder sbURL = new StringBuilder();
            sbURL.Append("~/Pages/RecForm/EditExchangeRate.aspx");
            sbURL.Append("?" + QueryStringConstants.ACCOUNT_ID + "=" + accountID);
            sbURL.Append("&" + QueryStringConstants.NETACCOUNT_ID + "=" + netAccountID);
            sbURL.Append("&" + QueryStringConstants.EXCHANGE_RATE_LCCY_TO_BCCY + "=" + exRateLCCYtoBCCY);
            sbURL.Append("&" + QueryStringConstants.EXCHANGE_RATE_LCCY_TO_RCCY + "=" + exRateLCCYtoRCCY);
            return sbURL.ToString();
        }

        public static GLDataHdrInfo GetGLDataHdrInfoToSaveOnTemplateForm(string commandName, bool? isSRA, bool isQulaityScoreExpanded, bool isRecControlCheckListExpanded, out WebEnums.ReconciliationStatus eNewReconciliationStatus, out bool isSignOff, out bool isFormDataToBeSaved)
        {
            isSignOff = false;
            isFormDataToBeSaved = false;
            eNewReconciliationStatus = new WebEnums.ReconciliationStatus();//TODO: 
            GLDataHdrInfo oGLDataHdrInfo = new GLDataHdrInfo();
            // set as NULL to ignore
            oGLDataHdrInfo.IsSystemReconcilied = null;
            switch (commandName)
            {

                case RecFormButtonCommandName.SAVE:
                    eNewReconciliationStatus = WebEnums.ReconciliationStatus.InProgress;
                    isFormDataToBeSaved = true;
                    break;
                case RecFormButtonCommandName.CANCEL:
                    //will not come to this case
                    break;
                case RecFormButtonCommandName.SIGNOFF:
                    eNewReconciliationStatus = WebEnums.ReconciliationStatus.Prepared;
                    isSignOff = true;
                    oGLDataHdrInfo.PendingReviewStatusDate = DateTime.Now;
                    isFormDataToBeSaved = true;
                    break;
                case RecFormButtonCommandName.EDIT_REC:
                    // based on Role, the new status would change
                    ARTEnums.UserRole eUserRole = (ARTEnums.UserRole)System.Enum.Parse(typeof(ARTEnums.UserRole), SessionHelper.CurrentRoleID.Value.ToString());

                    switch (eUserRole)
                    {
                        case ARTEnums.UserRole.PREPARER:
                        case ARTEnums.UserRole.BACKUP_PREPARER:
                            eNewReconciliationStatus = WebEnums.ReconciliationStatus.InProgress;
                            break;

                        case ARTEnums.UserRole.REVIEWER:
                        case ARTEnums.UserRole.BACKUP_REVIEWER:
                            eNewReconciliationStatus = WebEnums.ReconciliationStatus.PendingReview;
                            break;

                        case ARTEnums.UserRole.APPROVER:
                        case ARTEnums.UserRole.BACKUP_APPROVER:
                            eNewReconciliationStatus = WebEnums.ReconciliationStatus.PendingApproval;
                            break;
                    }

                    break;
                case RecFormButtonCommandName.ACCEPT:
                    eNewReconciliationStatus = WebEnums.ReconciliationStatus.Reviewed;
                    isSignOff = true;
                    if (isQulaityScoreExpanded || isRecControlCheckListExpanded)
                        isFormDataToBeSaved = true;
                    oGLDataHdrInfo.PendingApprovalStatusDate = DateTime.Now;
                    break;
                case RecFormButtonCommandName.REJECT:
                    eNewReconciliationStatus = WebEnums.ReconciliationStatus.PendingModificationPreparer;
                    if (isSRA.HasValue && isSRA.Value)
                    {
                        // in case reviewer rejects a SRA account, it becomes a regular account
                        oGLDataHdrInfo.IsSystemReconcilied = false;
                    }
                    break;
                case RecFormButtonCommandName.APPROVE:
                    eNewReconciliationStatus = WebEnums.ReconciliationStatus.Approved;
                    isSignOff = true;
                    if (isQulaityScoreExpanded)
                        isFormDataToBeSaved = true;
                    oGLDataHdrInfo.ReconciledStatusDate = DateTime.Now;
                    break;
                case RecFormButtonCommandName.DENY:
                    eNewReconciliationStatus = WebEnums.ReconciliationStatus.PendingModificationReviewer;
                    break;
            }
            DateTime dtNow = DateTime.Now;
            oGLDataHdrInfo.ReconciliationStatusID = (short)eNewReconciliationStatus;
            oGLDataHdrInfo.ReconciliationStatusDate = dtNow;
            oGLDataHdrInfo.DateRevised = dtNow;
            oGLDataHdrInfo.RevisedBy = SessionHelper.CurrentUserLoginID;
            oGLDataHdrInfo.ReconciliationPeriodID = SessionHelper.CurrentReconciliationPeriodID;
            return oGLDataHdrInfo;
        }

        public static short? GetReconciliationStatusByGLDataID(long? gLDataID)
        {
            if (gLDataID == null || gLDataID == 0)
                return null;
            IGLData oGLDataClient = RemotingHelper.GetGLDataObject();
            List<GLDataHdrInfo> oGLDataHdrInfoCollection = (List<GLDataHdrInfo>)oGLDataClient.SelectGLDataHdrByGLDataID(gLDataID, Helper.GetAppUserInfo());
            //GLDataHdrInfo oGLDataHdrInfo = oGLDataHdrInfoCollection.Find(r => r.GLDataID == _gLDataID);
            if (oGLDataHdrInfoCollection != null && oGLDataHdrInfoCollection.Count > 0 && oGLDataHdrInfoCollection[0].ReconciliationStatusID.HasValue)
                return oGLDataHdrInfoCollection[0].ReconciliationStatusID;
            else
                return 0;
        }

        public static GLDataHdrInfo GetGLDataHdrInfo(Int64? glDataID)
        {
            IGLData oGLDataClient = RemotingHelper.GetGLDataObject();
            return oGLDataClient.GetGLDataHdrInfo(glDataID, SessionHelper.CurrentReconciliationPeriodID, SessionHelper.CurrentUserID, SessionHelper.CurrentRoleID, Helper.GetAppUserInfo());

        }
        #endregion


        #region Capability Page
        /// <summary>
        /// For showing capability status (whether value is coming from previous periods or set on selected rec period) on capability page and child controls of it.
        /// </summary>
        /// <param name="imgForwardedYes"></param>
        /// <param name="imgForwardedNo"></param>
        /// <param name="isForwarded"></param>
        public static void SetCarryforwardedStatus(ExImage imgForwardedYes, ExImage imgForwardedNo, bool? isForwarded)
        {
            //if (isForwarded.HasValue)
            //{
            //    if (isForwarded.Value)
            //    {
            //        imgForwardedYes.Visible = true;
            //        imgForwardedNo.Visible = false;
            //    }
            //    else
            //    {
            //        imgForwardedYes.Visible = false;
            //        imgForwardedNo.Visible = true;
            //    }
            //}
            //else
            //{
            //    imgForwardedYes.Visible = false;
            //    imgForwardedNo.Visible = false;
            //}
            string visibleYes = "block";
            string visibleNo = "none";
            if (isForwarded.HasValue)
            {
                if (isForwarded.Value)
                {
                    imgForwardedYes.Style[HtmlTextWriterStyle.Display] = visibleYes;
                    imgForwardedNo.Style[HtmlTextWriterStyle.Display] = visibleNo;
                }
                else
                {
                    imgForwardedYes.Style[HtmlTextWriterStyle.Display] = visibleNo;
                    imgForwardedNo.Style[HtmlTextWriterStyle.Display] = visibleYes;
                }
            }
            else
            {
                imgForwardedYes.Style[HtmlTextWriterStyle.Display] = visibleNo;
                imgForwardedNo.Style[HtmlTextWriterStyle.Display] = visibleNo;
            }
        }

        /// <summary>
        /// Sets the  class for td (tdCapabilityStatus on child controls of capability) as if no opt is selected then spacing is not coming properly
        /// </summary>
        /// <param name="tdStatus"></param>
        /// <param name="isForwarded"></param>
        public static void ChangeCssClassOfTDStatus(HtmlTableCell tdStatus, bool? isForwarded)
        {
            string className = "tdCapabilityStatusIcon";
            if (isForwarded.HasValue)
            {
                tdStatus.Attributes.Add("class", className);
            }
            else
            {
                tdStatus.Attributes.Add("class", "");
            }
        }

        //TODO: pass isCapabilitySelected by reference in helper method
        //public static void SetSetFlagBasedOnYesNoRadioButtons(ExRadioButton btnYes, ExRadioButton btnNo, bool? isCapabilitySelected)
        /// <summary>
        /// Sets bool flag the based on Yes and No radioButtons  selected or not, on capability page and child controls 
        /// </summary>
        /// <param name="btnYes"></param>
        /// <param name="btnNo"></param>
        /// <param name="isCapabilitySelected"></param>
        /// <returns></returns>
        public static bool? SetSetFlagBasedOnYesNoRadioButtons(ExRadioButton optYes, ExRadioButton optNo)
        {
            bool? isCapabilitySelected = null;

            if (optYes.Checked == true)
            {
                isCapabilitySelected = true;
            }
            else if (optNo.Checked == true)
            {
                isCapabilitySelected = false;
            }
            else
            {
                isCapabilitySelected = null;
            }
            return isCapabilitySelected;
        }




        /// <summary>
        /// Sets the Yes and No radioButtons based on bool value, on capability page and child controls 
        /// </summary>
        /// <param name="btnYes"></param>
        /// <param name="btnNo"></param>
        /// <param name="isCapabilitySelected"></param>
        public static void SetYesNoRadioButtons(ExRadioButton btnYes, ExRadioButton btnNo, bool? isCapabilitySelected)
        {
            if (isCapabilitySelected == true)
            {
                btnYes.Checked = true;
                btnNo.Checked = false;
                //pnlMain.CssClass = "PanelCapability";
                //pnlContent.CssClass = "PanelContent";
                //pnlYesNo.CssClass = "PanelCapabilityYesNo";
                //BindAfterYesNoSelection();
            }
            else
                if (isCapabilitySelected == false)
            {
                btnYes.Checked = false;
                btnNo.Checked = true;
                //pnlMain.CssClass = "";
                //pnlContent.CssClass = "";
                //pnlYesNo.CssClass = "";
            }
            else if (isCapabilitySelected == null)
            {
                btnYes.Checked = false;
                btnNo.Checked = false;
                //pnlMain.CssClass = "";
                //pnlContent.CssClass = "";
                //pnlYesNo.CssClass = "";
            }
        }

        public static bool IsCapabilityChanged(CompanyCapabilityInfo CapabilityInfo1, CompanyCapabilityInfo CapabilityInfo2)
        {
            bool IsValueChangeFlag = false;
            if ((CapabilityInfo1 == null && CapabilityInfo2 != null) || (CapabilityInfo1 != null && CapabilityInfo2 == null))
                IsValueChangeFlag = true;
            else if (CapabilityInfo1.IsActivated.GetValueOrDefault() != CapabilityInfo2.IsActivated.GetValueOrDefault())
                IsValueChangeFlag = true;
            else if ((CapabilityInfo1.CapabilityAttributeValueInfoList == null && CapabilityInfo2.CapabilityAttributeValueInfoList != null)
                || (CapabilityInfo1.CapabilityAttributeValueInfoList != null && CapabilityInfo2.CapabilityAttributeValueInfoList == null)
                || (CapabilityInfo1.CapabilityAttributeValueInfoList.Count != CapabilityInfo2.CapabilityAttributeValueInfoList.Count))
                IsValueChangeFlag = true;
            else
            {
                foreach (CapabilityAttributeValueInfo item in CapabilityInfo1.CapabilityAttributeValueInfoList)
                {
                    if (!CapabilityInfo2.CapabilityAttributeValueInfoList.Exists(T => T.CapabilityAttributeID == item.CapabilityAttributeID
                        && T.ReferenceID.GetValueOrDefault() == item.ReferenceID.GetValueOrDefault()
                        && T.Value == item.Value))
                    {
                        IsValueChangeFlag = true;
                        break;
                    }
                }
                foreach (CapabilityAttributeValueInfo item in CapabilityInfo2.CapabilityAttributeValueInfoList)
                {
                    if (!CapabilityInfo1.CapabilityAttributeValueInfoList.Exists(T => T.CapabilityAttributeID == item.CapabilityAttributeID
                        && T.ReferenceID.GetValueOrDefault() == item.ReferenceID.GetValueOrDefault()
                        && T.Value == item.Value))
                    {
                        IsValueChangeFlag = true;
                        break;
                    }
                }
            }
            return IsValueChangeFlag;
        }

        #endregion


        public static string GetDisplayStringValue(string val)
        {
            if (string.IsNullOrEmpty(val))
                return WebConstants.HYPHEN;
            else
                return HttpContext.Current.Server.HtmlEncode(val);
        }

        public static string GetDiscriptionStringValue(string val)
        {
            if (string.IsNullOrEmpty(val))
                return WebConstants.HYPHEN;
            else
            {
                if (val.Length > 50)
                {
                    val = val.Substring(0, 50) + "...";
                }
                return HttpContext.Current.Server.HtmlEncode(val);
            }
        }

        public static void SetTextAndTooltipValue(ExLabel lbl, string val)
        {
            string _displayString = val;
            if (string.IsNullOrEmpty(val))
                _displayString = WebConstants.HYPHEN;
            else
            {
                if (val.Length > 50)
                {
                    _displayString = val.Substring(0, 50) + "...";
                }
                lbl.ToolTip = HttpContext.Current.Server.HtmlEncode(val);
            }
            lbl.Text = _displayString;
        }
        public static void SetTextAndTooltipValue(ExHyperLink hl, string val)
        {
            string _displayString = val;
            if (string.IsNullOrEmpty(val))
                _displayString = WebConstants.HYPHEN;
            else
            {
                if (val.Length > 50)
                {
                    _displayString = val.Substring(0, 50) + "...";
                }
                hl.ToolTip = HttpContext.Current.Server.HtmlEncode(val);
            }
            hl.Text = _displayString;
        }

        public static string GetDisplayUserFullName(string firstName, string lastName)
        {
            string str = "";
            if (!string.IsNullOrEmpty(firstName))
                str = firstName + " " + lastName;
            else
                str = lastName;
            return GetDisplayStringValue(str);
        }


        //public static void SetAttributeForInputTextBoxesToCalculateValueInOtherCurrecy(TextBox sourceTextBox
        //    , TextBox destinationTextBox, decimal exchangeRate
        //    , HiddenField hdnGLBalanceBC, HiddenField hdnGLBalanceRC
        //    , HiddenField hdnGlAdjustmentAndTimingDiffBC, HiddenField hdnGlAdjustmentAndTimingDiffRC
        //    , ExLabel lblRecAmountBC, ExLabel lblRecAmountRC
        //    , ExLabel lblTotalRecWriteOffBC, ExLabel lblTotalRecWriteOffRC
        //    , ExLabel lblTotalUnExplainedVarianceBC, ExLabel lblTotalUnExplainedVarianceRC)
        //{
        //    StringBuilder oSB = new StringBuilder();
        //    oSB.Append(";CalculateUnExpVar('");
        //    oSB.Append(hdnGLBalanceBC.ClientID);
        //    oSB.Append("','");
        //    oSB.Append(hdnGLBalanceRC.ClientID);
        //    oSB.Append("','");
        //    oSB.Append(hdnGlAdjustmentAndTimingDiffBC.ClientID);
        //    oSB.Append("','");
        //    oSB.Append(hdnGlAdjustmentAndTimingDiffRC.ClientID);
        //    oSB.Append("','");
        //    oSB.Append(lblRecAmountBC.ClientID);
        //    oSB.Append("','");
        //    oSB.Append(lblRecAmountRC.ClientID);
        //    oSB.Append("','");
        //    oSB.Append(sourceTextBox.ClientID);
        //    oSB.Append("','");
        //    oSB.Append(destinationTextBox.ClientID);
        //    oSB.Append("','");
        //    oSB.Append(lblTotalRecWriteOffBC.ClientID);
        //    oSB.Append("','");
        //    oSB.Append(lblTotalRecWriteOffRC.ClientID);
        //    oSB.Append("','");
        //    oSB.Append(lblTotalUnExplainedVarianceBC.ClientID);
        //    oSB.Append("','");
        //    oSB.Append(lblTotalUnExplainedVarianceRC.ClientID);
        //    oSB.Append("')");

        //    sourceTextBox.Attributes.Add(WebConstants.ONBLUR, "CalculateValueInGivenCurrency('"
        //        + sourceTextBox.ClientID + "', '"
        //        + destinationTextBox.ClientID + "','"
        //        + exchangeRate.ToString() + "')"
        //        + oSB.ToString());
        //}

        public static void SetAttributeForInputTextBoxesToCalculateValueInOtherCurrecy
            (TextBox sourceTextBox, TextBox destinationTextBox, TextBox txtBankBalanceBC, TextBox txtBankBalanceRC
            , decimal exchangeRate, HiddenField hdnGLBalanceBC, HiddenField hdnGLBalanceRC
            , HiddenField hdnGlAdjustmentAndTimingDiffBC, HiddenField hdnGlAdjustmentAndTimingDiffRC
            , HiddenField hdnSupportingDetailBC, HiddenField hdnSupportingDetailRC
            , ExLabel lblRecAmountBC, ExLabel lblRecAmountRC
            , ExLabel lblTotalRecWriteOffBC, ExLabel lblTotalRecWriteOffRC
            , ExLabel lblTotalUnExplainedVarianceBC, ExLabel lblTotalUnExplainedVarianceRC, bool isBCCYAvailable, HiddenField hdnBankBalanceRC, HiddenField hdnBankBalanceBC)
        {
            StringBuilder oSB = new StringBuilder();
            oSB.Append("javascript:RecalculateBalances('");
            oSB.Append(hdnGLBalanceBC.ClientID);
            oSB.Append("','");
            oSB.Append(hdnGLBalanceRC.ClientID);
            oSB.Append("','");
            oSB.Append(hdnGlAdjustmentAndTimingDiffBC.ClientID);
            oSB.Append("','");
            oSB.Append(hdnGlAdjustmentAndTimingDiffRC.ClientID);
            oSB.Append("','");
            oSB.Append(hdnSupportingDetailBC.ClientID);
            oSB.Append("','");
            oSB.Append(hdnSupportingDetailRC.ClientID);
            oSB.Append("','");
            oSB.Append(lblRecAmountBC.ClientID);
            oSB.Append("','");
            oSB.Append(lblRecAmountRC.ClientID);
            oSB.Append("', this, '");
            oSB.Append(destinationTextBox.ClientID);
            oSB.Append("','");
            oSB.Append(txtBankBalanceBC.ClientID);
            oSB.Append("','");
            oSB.Append(txtBankBalanceRC.ClientID);
            oSB.Append("','");
            oSB.Append(lblTotalRecWriteOffBC.ClientID);
            oSB.Append("','");
            oSB.Append(lblTotalRecWriteOffRC.ClientID);
            oSB.Append("','");
            oSB.Append(lblTotalUnExplainedVarianceBC.ClientID);
            oSB.Append("','");
            oSB.Append(lblTotalUnExplainedVarianceRC.ClientID);
            oSB.Append("', '");
            oSB.Append(exchangeRate);
            oSB.Append("',");
            oSB.Append(TestConstant.DECIMAL_PLACES_FOR_EXCHANGE_RATE_ROUND);
            oSB.Append(",");
            oSB.Append(isBCCYAvailable ? 1 : 0);
            oSB.Append(",'");
            oSB.Append(hdnBankBalanceRC.ClientID);
            oSB.Append("','");
            oSB.Append(hdnBankBalanceBC.ClientID);
            oSB.Append("');");

            sourceTextBox.Attributes.Add(WebConstants.ONBLUR, oSB.ToString());
        }


        public static string GetDisplayRecCategoryTypeID(short recCategoryTypeID)
        {
            string recCategory = null;
            WebEnums.RecCategoryType oRecCategoryType = (WebEnums.RecCategoryType)Enum.Parse(typeof(WebEnums.RecCategoryType), recCategoryTypeID.ToString());

            switch (oRecCategoryType)
            {
                case WebEnums.RecCategoryType.BankAccount_GLAdjustments_BankFees:
                    recCategory = LanguageUtil.GetValue(1692);
                    break;

                case WebEnums.RecCategoryType.BankAccount_GLAdjustments_NSFFees:
                    recCategory = LanguageUtil.GetValue(1693);
                    break;

                case WebEnums.RecCategoryType.BankAccount_GLAdjustments_Other:
                    recCategory = LanguageUtil.GetValue(1694);
                    break;

                case WebEnums.RecCategoryType.BankAccount_TimingDifference_DepositsInTransit:
                    recCategory = LanguageUtil.GetValue(1695);
                    break;

                case WebEnums.RecCategoryType.BankAccount_TimingDifference_Other:
                    recCategory = LanguageUtil.GetValue(1694);
                    break;

                case WebEnums.RecCategoryType.BankAccount_TimingDifference_OutstandingChecks:
                    recCategory = LanguageUtil.GetValue(1696);
                    break;

                case WebEnums.RecCategoryType.DerivedCalculation_SupportingDetail_OtherDetails:
                    recCategory = LanguageUtil.GetValue(1691);
                    break;
            }

            return recCategory;
        }

        public static List<Int16> GetAccountAttributeIDCollection(WebEnums.AccountPages eAccountPages)
        {
            List<Int16> oAccountAttributeIDCollection = new List<Int16>();

            switch (eAccountPages)
            {
                case WebEnums.AccountPages.AccountViewer:
                    oAccountAttributeIDCollection.Add((Int16)ARTEnums.AccountAttribute.IsKeyAccount);
                    oAccountAttributeIDCollection.Add((Int16)ARTEnums.AccountAttribute.RiskRating);
                    oAccountAttributeIDCollection.Add((Int16)ARTEnums.AccountAttribute.Preparer);
                    oAccountAttributeIDCollection.Add((Int16)ARTEnums.AccountAttribute.Reviewer);
                    oAccountAttributeIDCollection.Add((Int16)ARTEnums.AccountAttribute.Approver);
                    oAccountAttributeIDCollection.Add((Int16)ARTEnums.AccountAttribute.IsZeroBalanceAccount);
                    oAccountAttributeIDCollection.Add((Int16)ARTEnums.AccountAttribute.ReconciliationTemplate);
                    break;
            }

            return oAccountAttributeIDCollection;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ePage"></param>
        /// <param name="eRecStatus"></param>
        /// <returns></returns>
        //public static WebEnums.FormMode GetFormMode(WebEnums.ARTPages ePage, WebEnums.ReconciliationStatus eRecStatus, bool? isSRA)
        //{
        public static WebEnums.FormMode GetFormMode(WebEnums.ARTPages ePage, GLDataHdrInfo oGLDataHdrInfo)
        {
            WebEnums.FormMode eFormMode = WebEnums.FormMode.ReadOnly;
            WebEnums.RecPeriodStatus eRecPeriodStatus = SessionHelper.CurrentRecProcessStatusEnum;


            // Check for IsStopRecAndStartCert Flag Of SystemWide Setting
            // Read only when IsStopRecAndStartCert True means Reconciliation has been closed and Certification can be Start
            ReconciliationPeriodInfo oReconciliationPeriodInfo = Helper.GetRecPeriodInfo(SessionHelper.CurrentReconciliationPeriodID);
            if (oReconciliationPeriodInfo.IsStopRecAndStartCert.Value)
            {
                eFormMode = WebEnums.FormMode.ReadOnly;
            }
            else
            {
                eFormMode = WebEnums.FormMode.Edit;
            }

            // If IsStopRecAndStartCert Flag is True just return because Form can never be Editable
            if (eFormMode == WebEnums.FormMode.ReadOnly)
            {
                return eFormMode;
            }

            /*
             * 1. Check the Status based on Rec Period Status
             * 1a. If ReadOnly Mode, return
             * 2. Check based on Role
             * 3. Rec Status
             */

            switch (eRecPeriodStatus)
            {
                case WebEnums.RecPeriodStatus.Open:
                case WebEnums.RecPeriodStatus.InProgress:
                    eFormMode = WebEnums.FormMode.Edit;
                    break;

                default:
                    eFormMode = WebEnums.FormMode.ReadOnly;
                    break;
            }

            // If Rec Period is not workable, 
            // just return because Form can never be Editable
            if (eFormMode == WebEnums.FormMode.ReadOnly)
            {
                return eFormMode;
            }

            //MOP Check. This account must be in reconciled state in any of previous reconcilable open period, then this rec is editabe else not
            if (oGLDataHdrInfo != null && oGLDataHdrInfo.IsEditable.GetValueOrDefault())
                eFormMode = WebEnums.FormMode.Edit;
            else
                eFormMode = WebEnums.FormMode.ReadOnly;

            if (eFormMode == WebEnums.FormMode.ReadOnly)
            {
                return eFormMode;
            }

            // Check based on Role
            ARTEnums.UserRole eUserRole = (ARTEnums.UserRole)System.Enum.Parse(typeof(ARTEnums.UserRole), SessionHelper.CurrentRoleID.Value.ToString());

            switch (eUserRole)
            {
                case ARTEnums.UserRole.PREPARER:
                case ARTEnums.UserRole.REVIEWER:
                case ARTEnums.UserRole.APPROVER:
                    eFormMode = WebEnums.FormMode.Edit;
                    break;

                case ARTEnums.UserRole.BACKUP_PREPARER:
                case ARTEnums.UserRole.BACKUP_REVIEWER:
                case ARTEnums.UserRole.BACKUP_APPROVER:
                    if (Helper.IsFeatureActivated(WebEnums.Feature.AccountOwnershipBackup, SessionHelper.CurrentReconciliationPeriodID))
                    {
                        eFormMode = WebEnums.FormMode.Edit;
                    }
                    break;
                default:
                    eFormMode = WebEnums.FormMode.ReadOnly;
                    break;
            }


            // If Any other Role apart from P / R / A
            // just return because Form can never be Editable
            if (eFormMode == WebEnums.FormMode.ReadOnly)
            {
                return eFormMode;
            }

            // Check for Certification Start Dates
            // only when both Feature + Capability are activated
            //ReconciliationPeriodInfo oReconciliationPeriodInfo = Helper.GetRecPeriodInfo(SessionHelper.CurrentReconciliationPeriodID);
            WebEnums.FeatureCapabilityMode eFeatureCapabilityMode = Helper.GetFeatureCapabilityMode(WebEnums.Feature.Certification, ARTEnums.Capability.CertificationActivation, SessionHelper.CurrentReconciliationPeriodID);
            if (eFeatureCapabilityMode == WebEnums.FeatureCapabilityMode.Visible)
            {
                if (oReconciliationPeriodInfo.CertificationStartDate != null)
                {
                    if (DateTime.Now.Date >= oReconciliationPeriodInfo.CertificationStartDate.Value.Date)
                    {
                        eFormMode = WebEnums.FormMode.ReadOnly;
                    }
                }
            }

            // If Certification Start Date is Past just return because Form can never be Editable
            if (eFormMode == WebEnums.FormMode.ReadOnly)
            {
                return eFormMode;
            }

            //IGLData oGLDataClient = RemotingHelper.GetGLDataObject();
            //bool isGLDataIDEditable = oGLDataClient.IsGLDataIDEditable (
            // Check based on Role + Rec Status  + Page Type + SRA
            if (oGLDataHdrInfo != null)
            {
                switch (eUserRole)
                {
                    case ARTEnums.UserRole.PREPARER:
                        eFormMode = WebEnums.FormMode.ReadOnly;

                        if (ePage == WebEnums.ARTPages.RecFormButtons)
                        {
                            if (oGLDataHdrInfo.ReconciliationStatusID == (short)WebEnums.ReconciliationStatus.NotStarted
                            || oGLDataHdrInfo.ReconciliationStatusID == (short)WebEnums.ReconciliationStatus.InProgress
                            || oGLDataHdrInfo.ReconciliationStatusID == (short)WebEnums.ReconciliationStatus.PendingModificationPreparer
                            || oGLDataHdrInfo.ReconciliationStatusID == (short)WebEnums.ReconciliationStatus.Prepared)
                            {
                                eFormMode = WebEnums.FormMode.Edit;
                            }
                        }
                        else if (oGLDataHdrInfo.ReconciliationStatusID == (short)WebEnums.ReconciliationStatus.NotStarted
                            || oGLDataHdrInfo.ReconciliationStatusID == (short)WebEnums.ReconciliationStatus.InProgress
                            || oGLDataHdrInfo.ReconciliationStatusID == (short)WebEnums.ReconciliationStatus.PendingModificationPreparer)
                        {
                            eFormMode = WebEnums.FormMode.Edit;
                        }
                        break;

                    case ARTEnums.UserRole.BACKUP_PREPARER:
                        if (Helper.IsFeatureActivated(WebEnums.Feature.AccountOwnershipBackup, SessionHelper.CurrentReconciliationPeriodID))
                        {
                            eFormMode = WebEnums.FormMode.ReadOnly;

                            if (ePage == WebEnums.ARTPages.RecFormButtons)
                            {
                                if (oGLDataHdrInfo.ReconciliationStatusID == (short)WebEnums.ReconciliationStatus.NotStarted
                                || oGLDataHdrInfo.ReconciliationStatusID == (short)WebEnums.ReconciliationStatus.InProgress
                                || oGLDataHdrInfo.ReconciliationStatusID == (short)WebEnums.ReconciliationStatus.PendingModificationPreparer
                                || oGLDataHdrInfo.ReconciliationStatusID == (short)WebEnums.ReconciliationStatus.Prepared)
                                {
                                    eFormMode = WebEnums.FormMode.Edit;
                                }
                            }
                            else if (oGLDataHdrInfo.ReconciliationStatusID == (short)WebEnums.ReconciliationStatus.NotStarted
                                || oGLDataHdrInfo.ReconciliationStatusID == (short)WebEnums.ReconciliationStatus.InProgress
                                || oGLDataHdrInfo.ReconciliationStatusID == (short)WebEnums.ReconciliationStatus.PendingModificationPreparer)
                            {
                                eFormMode = WebEnums.FormMode.Edit;
                            }
                        }
                        break;

                    case ARTEnums.UserRole.REVIEWER:
                        eFormMode = WebEnums.FormMode.ReadOnly;

                        if (ePage == WebEnums.ARTPages.ReviewNotes
                            || ePage == WebEnums.ARTPages.EditItemReviewNote)
                        {
                            if (oGLDataHdrInfo.ReconciliationStatusID == (short)WebEnums.ReconciliationStatus.PendingReview
                                || oGLDataHdrInfo.ReconciliationStatusID == (short)WebEnums.ReconciliationStatus.PendingModificationReviewer)
                            {
                                eFormMode = WebEnums.FormMode.Edit;
                            }
                        }
                        else if (ePage == WebEnums.ARTPages.RecFormButtons)
                        {
                            if (oGLDataHdrInfo.ReconciliationStatusID == (short)WebEnums.ReconciliationStatus.PendingReview
                                || oGLDataHdrInfo.ReconciliationStatusID == (short)WebEnums.ReconciliationStatus.PendingModificationReviewer
                                || oGLDataHdrInfo.ReconciliationStatusID == (short)WebEnums.ReconciliationStatus.Reviewed)
                            {
                                eFormMode = WebEnums.FormMode.Edit;
                            }
                        }
                        else if (ePage == WebEnums.ARTPages.EditQualityScore)
                        {
                            if (oGLDataHdrInfo.ReconciliationStatusID == (short)WebEnums.ReconciliationStatus.PendingReview
                                || oGLDataHdrInfo.ReconciliationStatusID == (short)WebEnums.ReconciliationStatus.PendingModificationReviewer)
                            {
                                eFormMode = WebEnums.FormMode.Edit;
                            }
                        }

                        break;

                    case ARTEnums.UserRole.BACKUP_REVIEWER:
                        if (Helper.IsFeatureActivated(WebEnums.Feature.AccountOwnershipBackup, SessionHelper.CurrentReconciliationPeriodID))
                        {
                            eFormMode = WebEnums.FormMode.ReadOnly;

                            if (ePage == WebEnums.ARTPages.ReviewNotes
                                || ePage == WebEnums.ARTPages.EditItemReviewNote)
                            {
                                if (oGLDataHdrInfo.ReconciliationStatusID == (short)WebEnums.ReconciliationStatus.PendingReview
                                    || oGLDataHdrInfo.ReconciliationStatusID == (short)WebEnums.ReconciliationStatus.PendingModificationReviewer)
                                {
                                    eFormMode = WebEnums.FormMode.Edit;
                                }
                            }
                            else if (ePage == WebEnums.ARTPages.RecFormButtons)
                            {
                                if (oGLDataHdrInfo.ReconciliationStatusID == (short)WebEnums.ReconciliationStatus.PendingReview
                                    || oGLDataHdrInfo.ReconciliationStatusID == (short)WebEnums.ReconciliationStatus.PendingModificationReviewer
                                    || oGLDataHdrInfo.ReconciliationStatusID == (short)WebEnums.ReconciliationStatus.Reviewed)
                                {
                                    eFormMode = WebEnums.FormMode.Edit;
                                }
                            }
                            else if (ePage == WebEnums.ARTPages.EditQualityScore)
                            {
                                if (oGLDataHdrInfo.ReconciliationStatusID == (short)WebEnums.ReconciliationStatus.PendingReview
                                    || oGLDataHdrInfo.ReconciliationStatusID == (short)WebEnums.ReconciliationStatus.PendingModificationReviewer)
                                {
                                    eFormMode = WebEnums.FormMode.Edit;
                                }
                            }
                        }
                        break;

                    case ARTEnums.UserRole.APPROVER:
                        eFormMode = WebEnums.FormMode.ReadOnly;

                        if (ePage == WebEnums.ARTPages.ReviewNotes
                            || ePage == WebEnums.ARTPages.EditItemReviewNote)
                        {
                            if (oGLDataHdrInfo.ReconciliationStatusID == (short)WebEnums.ReconciliationStatus.PendingApproval)
                            {
                                eFormMode = WebEnums.FormMode.Edit;
                            }
                        }
                        else if (ePage == WebEnums.ARTPages.RecFormButtons)
                        {
                            if (oGLDataHdrInfo.ReconciliationStatusID == (short)WebEnums.ReconciliationStatus.PendingApproval
                                || oGLDataHdrInfo.ReconciliationStatusID == (short)WebEnums.ReconciliationStatus.Approved)
                            {
                                eFormMode = WebEnums.FormMode.Edit;
                            }
                        }
                        else if (ePage == WebEnums.ARTPages.EditQualityScore)
                        {
                            if (oGLDataHdrInfo.ReconciliationStatusID == (short)WebEnums.ReconciliationStatus.PendingApproval)
                            {
                                eFormMode = WebEnums.FormMode.Edit;
                            }
                        }
                        break;

                    case ARTEnums.UserRole.BACKUP_APPROVER:
                        if (Helper.IsFeatureActivated(WebEnums.Feature.AccountOwnershipBackup, SessionHelper.CurrentReconciliationPeriodID))
                        {
                            eFormMode = WebEnums.FormMode.ReadOnly;

                            if (ePage == WebEnums.ARTPages.ReviewNotes
                                || ePage == WebEnums.ARTPages.EditItemReviewNote)
                            {
                                if (oGLDataHdrInfo.ReconciliationStatusID == (short)WebEnums.ReconciliationStatus.PendingApproval)
                                {
                                    eFormMode = WebEnums.FormMode.Edit;
                                }
                            }
                            else if (ePage == WebEnums.ARTPages.RecFormButtons)
                            {
                                if (oGLDataHdrInfo.ReconciliationStatusID == (short)WebEnums.ReconciliationStatus.PendingApproval
                                    || oGLDataHdrInfo.ReconciliationStatusID == (short)WebEnums.ReconciliationStatus.Approved)
                                {
                                    eFormMode = WebEnums.FormMode.Edit;
                                }
                            }
                            else if (ePage == WebEnums.ARTPages.EditQualityScore)
                            {
                                if (oGLDataHdrInfo.ReconciliationStatusID == (short)WebEnums.ReconciliationStatus.PendingApproval)
                                {
                                    eFormMode = WebEnums.FormMode.Edit;
                                }
                            }
                        }
                        break;
                }
            }



            return eFormMode;
        }
        public static WebEnums.FormMode GetFormModeForRecControlCheckList(GLDataHdrInfo oGLDataHdrInfo)
        {
            WebEnums.FormMode eFormMode = WebEnums.FormMode.ReadOnly;
            WebEnums.RecPeriodStatus eRecPeriodStatus = SessionHelper.CurrentRecProcessStatusEnum;

            ReconciliationPeriodInfo oReconciliationPeriodInfo = Helper.GetRecPeriodInfo(SessionHelper.CurrentReconciliationPeriodID);
            if (oReconciliationPeriodInfo.IsStopRecAndStartCert.Value)
            {
                eFormMode = WebEnums.FormMode.ReadOnly;
            }
            else
            {
                eFormMode = WebEnums.FormMode.Edit;
            }

            // If IsStopRecAndStartCert Flag is True just return because Form can never be Editable
            if (eFormMode == WebEnums.FormMode.ReadOnly)
            {
                return eFormMode;
            }

            /*
             * 1. Check the Status based on Rec Period Status
             * 1a. If ReadOnly Mode, return
             * 2. Check based on Role
             * 3. Rec Status
             */

            switch (eRecPeriodStatus)
            {
                case WebEnums.RecPeriodStatus.Open:
                case WebEnums.RecPeriodStatus.InProgress:
                    eFormMode = WebEnums.FormMode.Edit;
                    break;

                default:
                    eFormMode = WebEnums.FormMode.ReadOnly;
                    break;
            }

            // If Rec Period is not workable, 
            // just return because Form can never be Editable
            if (eFormMode == WebEnums.FormMode.ReadOnly)
            {
                return eFormMode;
            }

            //MOP Check. This account must be in reconciled state in any of previous reconcilable open period, then this rec is editabe else not
            if (oGLDataHdrInfo != null && oGLDataHdrInfo.IsEditable.GetValueOrDefault())
                eFormMode = WebEnums.FormMode.Edit;
            else
                eFormMode = WebEnums.FormMode.ReadOnly;

            if (eFormMode == WebEnums.FormMode.ReadOnly)
            {
                return eFormMode;
            }

            // Check based on Role
            ARTEnums.UserRole eUserRole = (ARTEnums.UserRole)System.Enum.Parse(typeof(ARTEnums.UserRole), SessionHelper.CurrentRoleID.Value.ToString());

            switch (eUserRole)
            {
                case ARTEnums.UserRole.PREPARER:
                case ARTEnums.UserRole.REVIEWER:
                    eFormMode = WebEnums.FormMode.Edit;
                    break;

                case ARTEnums.UserRole.BACKUP_PREPARER:
                case ARTEnums.UserRole.BACKUP_REVIEWER:
                    if (Helper.IsFeatureActivated(WebEnums.Feature.AccountOwnershipBackup, SessionHelper.CurrentReconciliationPeriodID))
                    {
                        eFormMode = WebEnums.FormMode.Edit;
                    }
                    break;
                default:
                    eFormMode = WebEnums.FormMode.ReadOnly;
                    break;
            }


            // If Any other Role apart from P / R 
            // just return because Form can never be Editable
            if (eFormMode == WebEnums.FormMode.ReadOnly)
            {
                return eFormMode;
            }

            // Check for Certification Start Dates
            // only when both Feature + Capability are activated
            WebEnums.FeatureCapabilityMode eFeatureCapabilityMode = Helper.GetFeatureCapabilityMode(WebEnums.Feature.Certification, ARTEnums.Capability.CertificationActivation, SessionHelper.CurrentReconciliationPeriodID);
            if (eFeatureCapabilityMode == WebEnums.FeatureCapabilityMode.Visible)
            {
                if (oReconciliationPeriodInfo.CertificationStartDate != null)
                {
                    if (DateTime.Now.Date >= oReconciliationPeriodInfo.CertificationStartDate.Value.Date)
                    {
                        eFormMode = WebEnums.FormMode.ReadOnly;
                    }
                }
            }

            // If Certification Start Date is Past just return because Form can never be Editable
            if (eFormMode == WebEnums.FormMode.ReadOnly)
            {
                return eFormMode;
            }


            if (oGLDataHdrInfo != null)
            {
                switch (eUserRole)
                {
                    case ARTEnums.UserRole.PREPARER:
                        eFormMode = WebEnums.FormMode.ReadOnly;
                        if (oGLDataHdrInfo.ReconciliationStatusID == (short)WebEnums.ReconciliationStatus.NotStarted
                              || oGLDataHdrInfo.ReconciliationStatusID == (short)WebEnums.ReconciliationStatus.InProgress
                              || oGLDataHdrInfo.ReconciliationStatusID == (short)WebEnums.ReconciliationStatus.PendingModificationPreparer
                             )
                        {
                            eFormMode = WebEnums.FormMode.Edit;
                        }
                        break;

                    case ARTEnums.UserRole.BACKUP_PREPARER:
                        if (Helper.IsFeatureActivated(WebEnums.Feature.AccountOwnershipBackup, SessionHelper.CurrentReconciliationPeriodID))
                        {
                            eFormMode = WebEnums.FormMode.ReadOnly;
                            if (oGLDataHdrInfo.ReconciliationStatusID == (short)WebEnums.ReconciliationStatus.NotStarted
                                || oGLDataHdrInfo.ReconciliationStatusID == (short)WebEnums.ReconciliationStatus.InProgress
                                || oGLDataHdrInfo.ReconciliationStatusID == (short)WebEnums.ReconciliationStatus.PendingModificationPreparer
                                || oGLDataHdrInfo.ReconciliationStatusID == (short)WebEnums.ReconciliationStatus.Prepared)
                            {
                                eFormMode = WebEnums.FormMode.Edit;
                            }
                        }
                        break;

                    case ARTEnums.UserRole.REVIEWER:
                        eFormMode = WebEnums.FormMode.ReadOnly;


                        if (oGLDataHdrInfo.ReconciliationStatusID == (short)WebEnums.ReconciliationStatus.PendingReview
                            || oGLDataHdrInfo.ReconciliationStatusID == (short)WebEnums.ReconciliationStatus.PendingModificationReviewer)
                        {
                            eFormMode = WebEnums.FormMode.Edit;
                        }
                        break;

                    case ARTEnums.UserRole.BACKUP_REVIEWER:
                        if (Helper.IsFeatureActivated(WebEnums.Feature.AccountOwnershipBackup, SessionHelper.CurrentReconciliationPeriodID))
                        {
                            eFormMode = WebEnums.FormMode.ReadOnly;
                            if (oGLDataHdrInfo.ReconciliationStatusID == (short)WebEnums.ReconciliationStatus.PendingReview
                                || oGLDataHdrInfo.ReconciliationStatusID == (short)WebEnums.ReconciliationStatus.PendingModificationReviewer)
                            {
                                eFormMode = WebEnums.FormMode.Edit;
                            }
                        }
                        break;
                }
            }

            return eFormMode;
        }

        public static void ShowNonEditableMessage(PageBaseRecForm oPageBaseRecForm, GLDataHdrInfo oGLDataHdrInfo)
        {
            WebEnums.RecPeriodStatus CurrentRecProcessStatus = SessionHelper.CurrentRecProcessStatusEnum;
            if (CurrentRecProcessStatus == WebEnums.RecPeriodStatus.NotStarted
                || CurrentRecProcessStatus == WebEnums.RecPeriodStatus.Open
                || CurrentRecProcessStatus == WebEnums.RecPeriodStatus.InProgress
                )
            {
                if (oGLDataHdrInfo != null && !oGLDataHdrInfo.IsEditable.GetValueOrDefault()
                    && oGLDataHdrInfo.ReconciliationStatusID.GetValueOrDefault() != (short)WebEnums.ReconciliationStatus.Reconciled
                    && oGLDataHdrInfo.ReconciliationStatusID.GetValueOrDefault() != (short)WebEnums.ReconciliationStatus.SysReconciled)
                {
                    // Message should be displayed to all the roles
                    Helper.ShowErrorMessageWithNoBullet(oPageBaseRecForm, LanguageUtil.GetValue(5000344));
                }
            }
        }

        public static void SetImageURLForViewVersusEdit(WebEnums.FormMode _FormMode, ExHyperLink hlItemPopup)
        {
            if (_FormMode == WebEnums.FormMode.Edit)
            {
                hlItemPopup.ImageUrl = AppSettingHelper.GetAppSettingValue(AppSettingConstants.IMAGE_URL_EDIT_ITEM_POPUP);
                hlItemPopup.ToolTip = LanguageUtil.GetValue(1429);
            }
            else
            {
                hlItemPopup.ImageUrl = AppSettingHelper.GetAppSettingValue(AppSettingConstants.IMAGE_URL_READ_ONLY_ITEM_POPUP);
                hlItemPopup.ToolTip = LanguageUtil.GetValue(1470);
            }
        }

        public static ARTEnums.ReconciliationItemTemplate GetRecTemplateForCurrentPage(short? recCategoryTypeID)
        {
            ARTEnums.ReconciliationItemTemplate eReconciliationItemTemplate = ARTEnums.ReconciliationItemTemplate.BankForm;
            if (recCategoryTypeID.HasValue && recCategoryTypeID.Value > 0)
            {
                WebEnums.RecCategoryType eRecCategoryType = (WebEnums.RecCategoryType)Enum.Parse(typeof(WebEnums.RecCategoryType), recCategoryTypeID.ToString());

                switch (eRecCategoryType)
                {
                    case WebEnums.RecCategoryType.Accrual_GLAdjustments_Total:
                    case WebEnums.RecCategoryType.Accrual_RecWriteoffOn_UnexpVar:
                    case WebEnums.RecCategoryType.Accrual_RecWriteoffOn_WriteoffOn:
                    case WebEnums.RecCategoryType.Accrual_SupportingDetail_IndividualAccrualDetail:
                    case WebEnums.RecCategoryType.Accrual_SupportingDetail_RecurringAccrualSchedule:
                    case WebEnums.RecCategoryType.Accrual_TimingDifference_Total:
                    case WebEnums.RecCategoryType.Accrual_ReviewNotes:
                        eReconciliationItemTemplate = ARTEnums.ReconciliationItemTemplate.AccrualForm;
                        break;

                    case WebEnums.RecCategoryType.Amortizable_GLAdjustments_Total:
                    case WebEnums.RecCategoryType.Amortizable_RecWriteoffOn_UnexpVar:
                    case WebEnums.RecCategoryType.Amortizable_RecWriteoffOn_WriteoffOn:

                    case WebEnums.RecCategoryType.Amortizable_SupportingDetail_RecurringAmortizableSchedule:
                    case WebEnums.RecCategoryType.Amortizable_TimingDifference_Total:
                    case WebEnums.RecCategoryType.Amortizable_ReviewNotes:
                        eReconciliationItemTemplate = ARTEnums.ReconciliationItemTemplate.AmortizableBalanceForm;
                        break;

                    case WebEnums.RecCategoryType.BankAccount_GLAdjustments_BankFees:
                    case WebEnums.RecCategoryType.BankAccount_GLAdjustments_NSFFees:
                    case WebEnums.RecCategoryType.BankAccount_GLAdjustments_Other:
                    case WebEnums.RecCategoryType.BankAccount_RecWriteoffOn_UnexpVar:
                    case WebEnums.RecCategoryType.BankAccount_RecWriteoffOn_WriteoffOn:
                    case WebEnums.RecCategoryType.BankAccount_TimingDifference_DepositsInTransit:
                    case WebEnums.RecCategoryType.BankAccount_TimingDifference_Other:
                    case WebEnums.RecCategoryType.BankAccount_TimingDifference_OutstandingChecks:
                    case WebEnums.RecCategoryType.BankAccount_ReviewNotes:
                        eReconciliationItemTemplate = ARTEnums.ReconciliationItemTemplate.BankForm;
                        break;

                    case WebEnums.RecCategoryType.DerivedCalculation_GLAdjustments_Total:
                    case WebEnums.RecCategoryType.DerivedCalculation_RecWriteoffOn_UnexpVar:
                    case WebEnums.RecCategoryType.DerivedCalculation_RecWriteoffOn_WriteoffOn:
                    case WebEnums.RecCategoryType.DerivedCalculation_SupportingDetail_OtherDetails:
                    case WebEnums.RecCategoryType.DerivedCalculation_TimingDifference_Total:
                    case WebEnums.RecCategoryType.DerivedCalculation_ReviewNotes:
                        eReconciliationItemTemplate = ARTEnums.ReconciliationItemTemplate.DerivedCalculationForm;
                        break;

                    case WebEnums.RecCategoryType.ItemizedList_GLAdjustments_Total:
                    case WebEnums.RecCategoryType.ItemizedList_RecWriteoffOn_UnexpVar:
                    case WebEnums.RecCategoryType.ItemizedList_RecWriteoffOn_WriteoffOn:
                    case WebEnums.RecCategoryType.ItemizedList_SupportingDetail_SupportingDetailList:
                    case WebEnums.RecCategoryType.ItemizedList_TimingDifference_Total:
                    case WebEnums.RecCategoryType.ItemizedList_ReviewNotes:
                        eReconciliationItemTemplate = ARTEnums.ReconciliationItemTemplate.ItemizedListForm;
                        break;

                    case WebEnums.RecCategoryType.Subledger_GLAdjustments_Total:
                    case WebEnums.RecCategoryType.Subledger_RecWriteoffOn_UnexpVar:
                    case WebEnums.RecCategoryType.Subledger_RecWriteoffOn_WriteoffOn:
                    case WebEnums.RecCategoryType.Subledger_TimingDifference_Total:
                    case WebEnums.RecCategoryType.Subledger_ReviewNotes:
                        eReconciliationItemTemplate = ARTEnums.ReconciliationItemTemplate.Subledgerform;
                        break;
                }
            }

            return eReconciliationItemTemplate;
        }

        #region "Capability Related Methods"
        public static bool IsCapabilityActivatedForCurrentRecPeriod(ARTEnums.Capability eCapability)
        {
            int capabilityID = (int)eCapability;
            List<CompanyCapabilityInfo> oCompanyCapabilityInfoCollection = SessionHelper.GetCompanyCapabilityCollectionForCurrentRecPeriod();
            CompanyCapabilityInfo oCompanyCapabilityInfo = oCompanyCapabilityInfoCollection.Find(c => c.CapabilityID == capabilityID);

            if (oCompanyCapabilityInfo != null)
            {
                return oCompanyCapabilityInfo.IsActivated.Value;
            }
            return false;
        }

        /// <summary>
        /// to check for Capability Activation and Check all dependencies for Ceritification
        /// </summary>
        /// <param name="eCapability"></param>
        /// <returns></returns>
        public static bool IsCapabilityActivatedForCurrentRecPeriod(ARTEnums.Capability eCapability, bool bCheckAllDependencies)
        {
            bool bCapabilityActivated = false;

            // Get the Capability Collection
            List<CompanyCapabilityInfo> oCompanyCapabilityInfoCollection = SessionHelper.GetCompanyCapabilityCollectionForCurrentRecPeriod();
            // Calculate and return
            bCapabilityActivated = GetIsCapabilityActivatedValue(oCompanyCapabilityInfoCollection, eCapability, bCheckAllDependencies);
            return bCapabilityActivated;
        }

        private static bool GetIsCapabilityActivatedValue(List<CompanyCapabilityInfo> oCompanyCapabilityInfoCollection, ARTEnums.Capability eCapability, bool bCheckAllDependencies)
        {
            bool bCapabilityActivated = false;
            int capabilityID = (int)eCapability;
            CompanyCapabilityInfo oCompanyCapabilityInfo = oCompanyCapabilityInfoCollection.Find(c => c.CapabilityID == capabilityID);

            if (oCompanyCapabilityInfo != null)
            {
                bCapabilityActivated = oCompanyCapabilityInfo.IsActivated.Value;

                if (bCheckAllDependencies)
                {
                    if (eCapability == ARTEnums.Capability.CertificationActivation
                        && !bCapabilityActivated)
                    {
                        /*	If Capability ID was Certification need to check other dependencies as well like
                            Mandatory Reports and CEO / CFO Certification
                        */

                        capabilityID = (int)ARTEnums.Capability.MandatoryReportSignoff;
                        oCompanyCapabilityInfo = oCompanyCapabilityInfoCollection.Find(c => c.CapabilityID == capabilityID);

                        if (oCompanyCapabilityInfo != null)
                        {
                            bCapabilityActivated = oCompanyCapabilityInfo.IsActivated.Value;
                            if (!bCapabilityActivated)
                            {
                                capabilityID = (int)ARTEnums.Capability.CEOCFOCertification;
                                oCompanyCapabilityInfo = oCompanyCapabilityInfoCollection.Find(c => c.CapabilityID == capabilityID);

                                if (oCompanyCapabilityInfo != null)
                                    bCapabilityActivated = oCompanyCapabilityInfo.IsActivated.Value;
                            }
                        }
                        else
                        {
                            // If info was NULL, means Capability Deactivated
                            capabilityID = (int)ARTEnums.Capability.CEOCFOCertification;
                            oCompanyCapabilityInfo = oCompanyCapabilityInfoCollection.Find(c => c.CapabilityID == capabilityID);

                            if (oCompanyCapabilityInfo != null)
                                bCapabilityActivated = oCompanyCapabilityInfo.IsActivated.Value;

                        }
                    }
                }
            }
            return bCapabilityActivated;
        }

        public static bool IsCapabilityActivatedForRecPeriodID(ARTEnums.Capability eCapability, int recPeriodID, bool bCheckAllDependency)
        {
            if (SessionHelper.CurrentReconciliationPeriodID.HasValue && SessionHelper.CurrentReconciliationPeriodID.Value == recPeriodID)
            {
                return Helper.IsCapabilityActivatedForCurrentRecPeriod(eCapability, bCheckAllDependency);
            }
            else
            {
                int capabilityID = (int)eCapability;
                List<CompanyCapabilityInfo> oCompanyCapabilityInfoCollection = SessionHelper.GetCompanyCapabilityCollectionForRecPeriodID(recPeriodID);
                return Helper.GetIsCapabilityActivatedValue(oCompanyCapabilityInfoCollection, eCapability, bCheckAllDependency);
            }
        }

        #endregion

        /// <summary>
        /// Clone the Object
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static object DeepClone(object obj)
        {
            if (obj == null)
                return null;
            object oNewObject;
            //  Create a memory stream and a formatter.
            System.IO.MemoryStream oMemoryStream = new System.IO.MemoryStream(1000);
            BinaryFormatter oBinaryFormatter = new BinaryFormatter();
            //  Serialize the object into the stream.
            oBinaryFormatter.Serialize(oMemoryStream, obj);
            //  Position streem pointer back to first byte.
            oMemoryStream.Seek(0, System.IO.SeekOrigin.Begin);
            //  Deserialize into another object.
            oNewObject = oBinaryFormatter.Deserialize(oMemoryStream);
            //  Release memory.
            oMemoryStream.Close();

            return oNewObject;
        }

        public static WebEnums.FormMode GetFormModeForAccountPages()
        {
            WebEnums.RecPeriodStatus CurrentRecProcessStatus = SessionHelper.CurrentRecProcessStatusEnum;
            if (CurrentRecProcessStatus == WebEnums.RecPeriodStatus.NotStarted
                || CurrentRecProcessStatus == WebEnums.RecPeriodStatus.Open
                || CurrentRecProcessStatus == WebEnums.RecPeriodStatus.InProgress)
            {
                return WebEnums.FormMode.Edit;
            }

            return WebEnums.FormMode.ReadOnly;
        }

        public static string GetUrlForRecFrequency(long? accountID, string txtRecPeriodContainerID, string recPeriodIDCollection, WebEnums.FormMode mode)
        {
            StringBuilder sbURL = new StringBuilder();

            sbURL.Append("PopupRecFrequencySelection.aspx?");
            sbURL.Append(QueryStringConstants.ACCOUNT_ID);
            sbURL.Append("=");
            sbURL.Append(accountID);
            sbURL.Append("&");
            sbURL.Append(QueryStringConstants.MODE);
            sbURL.Append("=");

            //if (Helper.GetFormModeForAccountPages() == WebEnums.FormMode.Edit)
            //{
            sbURL.Append(mode);
            //}
            //else
            //{
            //    sbURL.Append(QueryStringConstants.READ_ONLY);
            //}

            sbURL.Append("&");
            sbURL.Append(QueryStringConstants.REC_PERIOD_CONTAINER_ID);
            sbURL.Append("=");
            sbURL.Append(txtRecPeriodContainerID);
            sbURL.Append("&");
            sbURL.Append(QueryStringConstants.REC_PERIOD_ID_COLLECTION);
            sbURL.Append("=");
            sbURL.Append(recPeriodIDCollection);

            return sbURL.ToString();
        }

        //public static void ShowRecStatusBar(PageBase oPageBase, long? glDataID)
        //{
        //    ShowRecStatusBar(oPageBase, true, true, true, false, false, glDataID);
        //}

        //public static void ShowRecStatusBarOnRecForms(PageBase oPageBase, long? glDataID)
        //{
        //    ShowRecStatusBar(oPageBase, true, false, false, true, true, glDataID);
        //}

        //private static void ShowRecStatusBar(PageBase oPageBase, bool bShowRecStatus, bool bShowReconciledBalance, bool bShowUnexpVar, bool bShowDueDates, bool bShowExportButtons, long? glDataID)
        //{
        //    RecPeriodMasterPageBase oRecPeriodMasterPageBase;
        //    oRecPeriodMasterPageBase = (RecPeriodMasterPageBase)oPageBase.Master;
        //    oRecPeriodMasterPageBase.GLDataID = glDataID;
        //    oRecPeriodMasterPageBase.ShowRecStatus = bShowRecStatus;
        //    oRecPeriodMasterPageBase.ShowReconciledBalance = bShowReconciledBalance;
        //    oRecPeriodMasterPageBase.ShowUnexpVar = bShowUnexpVar;
        //    oRecPeriodMasterPageBase.ShowDueDates = bShowDueDates;
        //    oRecPeriodMasterPageBase.ShowExportButton = bShowExportButtons;
        //}

        public static void HidePanel(UpdatePanel updpnlMain, ARTException ex)
        {
            if (ex.ExceptionPhraseID == 5000116 || ex.ExceptionPhraseID == 5000120)
                updpnlMain.Visible = false;
        }

        #region Certification

        public static WebEnums.FormMode GetFormModeForCertification(WebEnums.ARTPages ePage)
        {
            WebEnums.FormMode eFormMode = WebEnums.FormMode.ReadOnly;
            WebEnums.RecPeriodStatus eRecPeriodStatus = SessionHelper.CurrentRecProcessStatusEnum;

            int userID = SessionHelper.CurrentUserID.Value;
            short roleID = SessionHelper.CurrentRoleID.Value;
            int recPeriodID = SessionHelper.CurrentReconciliationPeriodID.Value;
            short certificationTypeID = GetCertificationTypeID(ePage);

            IGLData oGLDataClient = RemotingHelper.GetGLDataObject();

            /*
             * 1. Check the Status based on Rec Period Status
             * 1a. If ReadOnly Mode, return
             * 2. Check based on Role
             * 3. Rec Status
             */

            switch (eRecPeriodStatus)
            {
                case WebEnums.RecPeriodStatus.Open:
                case WebEnums.RecPeriodStatus.InProgress:
                    eFormMode = WebEnums.FormMode.Edit;
                    break;

                default:
                    eFormMode = WebEnums.FormMode.ReadOnly;
                    break;
            }

            // If Rec Period is not workable, 
            // just return because Form can never be Editable
            if (eFormMode == WebEnums.FormMode.ReadOnly)
            {
                return eFormMode;
            }

            // Check based on Role
            ARTEnums.UserRole eUserRole = (ARTEnums.UserRole)System.Enum.Parse(typeof(ARTEnums.UserRole), SessionHelper.CurrentRoleID.Value.ToString());

            switch (eUserRole)
            {
                case ARTEnums.UserRole.PREPARER:
                case ARTEnums.UserRole.REVIEWER:
                case ARTEnums.UserRole.APPROVER:
                case ARTEnums.UserRole.EXECUTIVE:
                case ARTEnums.UserRole.CONTROLLER:
                case ARTEnums.UserRole.ACCOUNT_MANAGER:
                case ARTEnums.UserRole.FINANCIAL_MANAGER:
                case ARTEnums.UserRole.CEO_CFO:
                case ARTEnums.UserRole.BACKUP_PREPARER:
                case ARTEnums.UserRole.BACKUP_REVIEWER:
                case ARTEnums.UserRole.BACKUP_APPROVER:
                    eFormMode = WebEnums.FormMode.Edit;
                    break;

                default:
                    eFormMode = WebEnums.FormMode.ReadOnly;
                    break;
            }


            // If Any other Role apart from P / R / A/Backups 
            // just return because Form can never be Editable
            if (eFormMode == WebEnums.FormMode.ReadOnly)
            {
                return eFormMode;
            }

            IReconciliationPeriod oIReconciliationPeriod = RemotingHelper.GetReconciliationPeriodObject();
            if (!oIReconciliationPeriod.IsPreviousPeriodsCertified(SessionHelper.CurrentReconciliationPeriodID, Helper.GetAppUserInfo()))
                eFormMode = WebEnums.FormMode.ReadOnly;

            if (eFormMode == WebEnums.FormMode.ReadOnly)
            {
                return eFormMode;
            }

            bool? isAllJuniorsCompletedCertification = oGLDataClient.GetIsAllJuniorsCompletedCertificationForUserRoleAndCertificationType(userID, roleID, recPeriodID, certificationTypeID, Helper.GetAppUserInfo());
            bool isDualReviewEnabled = Helper.IsCapabilityActivatedForCurrentRecPeriod(ARTEnums.Capability.DualLevelReview);



            // Check based on Role + Rec Status  + Page Type
            switch (eUserRole)
            {
                case ARTEnums.UserRole.PREPARER:
                case ARTEnums.UserRole.BACKUP_PREPARER:
                    eFormMode = WebEnums.FormMode.ReadOnly;

                    switch (ePage)
                    {
                        case WebEnums.ARTPages.CertificationBalances:
                            eFormMode = IsAllAccountsReconciled(userID, roleID, recPeriodID, isDualReviewEnabled);
                            break;
                        case WebEnums.ARTPages.CertificationException:
                        case WebEnums.ARTPages.CertificationAccount:
                            if (isAllJuniorsCompletedCertification.HasValue && isAllJuniorsCompletedCertification.Value)
                            {
                                eFormMode = WebEnums.FormMode.Edit;
                            }
                            break;
                    }
                    break;

                case ARTEnums.UserRole.REVIEWER:
                case ARTEnums.UserRole.BACKUP_REVIEWER:
                case ARTEnums.UserRole.APPROVER:
                case ARTEnums.UserRole.BACKUP_APPROVER:
                case ARTEnums.UserRole.EXECUTIVE:
                case ARTEnums.UserRole.CONTROLLER:
                case ARTEnums.UserRole.ACCOUNT_MANAGER:
                case ARTEnums.UserRole.FINANCIAL_MANAGER:
                    eFormMode = WebEnums.FormMode.ReadOnly;
                    switch (ePage)
                    {
                        case WebEnums.ARTPages.MandatoryReportSignOff:
                            if (roleID == (short)ARTEnums.UserRole.REVIEWER || roleID == (short)ARTEnums.UserRole.BACKUP_REVIEWER)
                            {
                                eFormMode = IsAllAccountsReconciled(userID, roleID, recPeriodID, isDualReviewEnabled);
                            }
                            else if (roleID == (short)ARTEnums.UserRole.APPROVER || roleID == (short)ARTEnums.UserRole.BACKUP_APPROVER)
                            {
                                if (isAllJuniorsCompletedCertification.HasValue && isAllJuniorsCompletedCertification.Value)
                                {
                                    eFormMode = WebEnums.FormMode.Edit;
                                }
                            }
                            break;
                        case WebEnums.ARTPages.CertificationBalances:
                            eFormMode = IsAllAccountsReconciled(userID, roleID, recPeriodID, isDualReviewEnabled);
                            break;
                        case WebEnums.ARTPages.CertificationException:

                        case WebEnums.ARTPages.CertificationAccount:
                            if (isAllJuniorsCompletedCertification.HasValue && isAllJuniorsCompletedCertification.Value)
                            {
                                eFormMode = WebEnums.FormMode.Edit;
                            }
                            break;
                    }
                    break;


                case ARTEnums.UserRole.CEO_CFO:
                    eFormMode = WebEnums.FormMode.ReadOnly;

                    switch (ePage)
                    {
                        case WebEnums.ARTPages.MandatoryReportSignOff:
                            if (roleID == (short)ARTEnums.UserRole.REVIEWER || roleID == (short)ARTEnums.UserRole.BACKUP_REVIEWER)
                            {
                                eFormMode = IsAllAccountsReconciled(userID, roleID, recPeriodID, isDualReviewEnabled);
                            }
                            else if (roleID == (short)ARTEnums.UserRole.APPROVER || roleID == (short)ARTEnums.UserRole.BACKUP_APPROVER)
                            {
                                if (isAllJuniorsCompletedCertification.HasValue && isAllJuniorsCompletedCertification.Value)
                                {
                                    eFormMode = WebEnums.FormMode.Edit;
                                }
                            }
                            break;
                        default:
                            if (isAllJuniorsCompletedCertification.HasValue && isAllJuniorsCompletedCertification.Value)
                            {
                                eFormMode = WebEnums.FormMode.Edit;
                            }
                            break;
                    }

                    break;
            }

            return eFormMode;
        }

        private static WebEnums.FormMode IsAllAccountsReconciled(int userID, short roleID, int recPeriodID, bool isDualReviewEnabled)
        {
            WebEnums.FormMode eFormMode = WebEnums.FormMode.ReadOnly;

            IGLData oGLDataClient = RemotingHelper.GetGLDataObject();
            bool? isAllAccountsReconciled = oGLDataClient.GetIsAllAccountsReconciledForUserAndRole(userID, roleID, recPeriodID, Helper.GetAppUserInfo());

            ReconciliationPeriodInfo oReconciliationPeriodInfo = Helper.GetRecPeriodInfo(SessionHelper.CurrentReconciliationPeriodID);
            if (oReconciliationPeriodInfo.IsStopRecAndStartCert.Value)
            {
                eFormMode = WebEnums.FormMode.Edit;
                return eFormMode;
            }
            else if (isAllAccountsReconciled.HasValue && isAllAccountsReconciled.Value)
            {
                eFormMode = WebEnums.FormMode.Edit;
            }
            else
            {
                //ReconciliationPeriodInfo oReconciliationPeriodInfo = Helper.GetRecPeriodInfo(SessionHelper.CurrentReconciliationPeriodID);
                // Check for Due Dates
                ////////if (isDualReviewEnabled)
                ////////{
                ////////    if (DateTime.Now.Date > oReconciliationPeriodInfo.ApproverDueDate.Value.Date)
                ////////    {
                ////////        eFormMode = WebEnums.FormMode.Edit;
                ////////    }
                ////////}
                ////////else
                ////////{
                ////////    if (DateTime.Now.Date > oReconciliationPeriodInfo.ReviewerDueDate.Value.Date)
                ////////    {
                ////////        eFormMode = WebEnums.FormMode.Edit;
                ////////    }
                ////////}


                // Check for Certification Start Dates



                if (oReconciliationPeriodInfo.CertificationStartDate != null)
                {
                    if (DateTime.Now.Date >= oReconciliationPeriodInfo.CertificationStartDate.Value.Date)
                    {
                        eFormMode = WebEnums.FormMode.Edit;
                    }
                }
                if (oReconciliationPeriodInfo.CertificationLockDownDate != null)
                {
                    if (DateTime.Now.Date > oReconciliationPeriodInfo.CertificationLockDownDate.Value.Date)
                    {
                        eFormMode = WebEnums.FormMode.ReadOnly;
                    }
                }



            }

            return eFormMode;
        }

        private static short GetCertificationTypeID(WebEnums.ARTPages ePage)
        {
            short certificationType = 0;
            switch (ePage)
            {
                case WebEnums.ARTPages.MandatoryReportSignOff:
                    certificationType = (short)WebEnums.CertificationType.MandatoryReportSignOff;
                    break;
                case WebEnums.ARTPages.CertificationBalances:
                    certificationType = (short)WebEnums.CertificationType.CertificationBalances;
                    break;
                case WebEnums.ARTPages.CertificationException:
                    certificationType = (short)WebEnums.CertificationType.ExceptionCertification;
                    break;
                case WebEnums.ARTPages.CertificationAccount:
                    certificationType = (short)WebEnums.CertificationType.Certification;
                    break;
            }

            return certificationType;
        }


        public static bool ShowHideContentOnCertificationPages(PageBase oPageBase, WebEnums.ARTPages ePage)
        {

            /*
             * 
    Cert Status		
        Mandatory	Cert Activation	CEO/CFO
	P	X	Y	X
	R	Y	Y	X
	A	Y	Y	X
	Others	X	Y	X
	CEO/CFO	X	Y	Y
				
				
    **CBA/EA/AC		
     Mandatory	Cert Activation	CEO/CFO
	P	X	Y	X
	R	X	Y	X
	A	X	Y	X
	Others	X	Y	X
	CEO/CFO	X	X	Y
				

             * Mandatory Report SignOff
	Mandatory	Cert Activation	CEO/CFO
	P	X	X	X
	R	Y	X	X
	A	Y	X	X
	Others	X	X	X
	CEO/CFO	X	X	X

             * 
             */

            Panel pnlDeactiveCertification = (Panel)oPageBase.Master.Master.FindControl("ContentPlaceHolder1").FindControl("pnlDeactiveCertification");
            Panel pnlActiveCertification = (Panel)oPageBase.Master.Master.FindControl("ContentPlaceHolder1").FindControl("pnlActiveCertification");

            bool isMandatoryReportEnabled = Helper.IsCapabilityActivatedForCurrentRecPeriod(ARTEnums.Capability.MandatoryReportSignoff);
            bool isCertificationEnabled = Helper.IsCapabilityActivatedForCurrentRecPeriod(ARTEnums.Capability.CertificationActivation);
            bool isCEOCertificationEnabled = Helper.IsCapabilityActivatedForCurrentRecPeriod(ARTEnums.Capability.CEOCFOCertification);

            ARTEnums.UserRole eUserRole = (ARTEnums.UserRole)System.Enum.Parse(typeof(ARTEnums.UserRole), SessionHelper.CurrentRoleID.Value.ToString());

            bool bCertificationStatus = false;

            switch (ePage)
            {
                case WebEnums.ARTPages.CertificationStatus:
                    switch (eUserRole)
                    {
                        case ARTEnums.UserRole.REVIEWER:
                        case ARTEnums.UserRole.APPROVER:
                            bCertificationStatus = isMandatoryReportEnabled || isCertificationEnabled;
                            break;

                        case ARTEnums.UserRole.CEO_CFO:
                            bCertificationStatus = isCertificationEnabled || isCEOCertificationEnabled;
                            break;

                        default:
                            bCertificationStatus = isCertificationEnabled;
                            break;
                    }

                    break;

                case WebEnums.ARTPages.MandatoryReportSignOff:
                case WebEnums.ARTPages.MandatoryReportsList:
                    switch (eUserRole)
                    {
                        case ARTEnums.UserRole.REVIEWER:
                        case ARTEnums.UserRole.APPROVER:
                        case ARTEnums.UserRole.BACKUP_REVIEWER:
                        case ARTEnums.UserRole.BACKUP_APPROVER:
                            bCertificationStatus = isMandatoryReportEnabled;
                            break;
                    }
                    break;

                case WebEnums.ARTPages.CertificationBalances:
                case WebEnums.ARTPages.CertificationException:
                case WebEnums.ARTPages.CertificationAccount:
                    switch (eUserRole)
                    {
                        case ARTEnums.UserRole.CEO_CFO:
                            bCertificationStatus = isCEOCertificationEnabled;
                            break;

                        default:
                            bCertificationStatus = isCertificationEnabled;
                            break;
                    }


                    break;


            }

            //if (userRoleID == (short)ARTEnums.UserRole.CEO_CFO
            //    || userRoleID == (short)ARTEnums.UserRole.SYSTEM_ADMIN
            //    || userRoleID == (short)ARTEnums.UserRole.SKYSTEM_ADMIN)
            //{
            //    if (ePage == WebEnums.ARTPages.MandatoryReportSignOff || ePage == WebEnums.ARTPages.MandatoryReportsList)
            //    {
            //        bool isMandatoryReportEnabled = Helper.IsCapabilityActivatedForCurrentRecPeriod(ARTEnums.Capability.MandatoryReportSignoff);

            //        if (isMandatoryReportEnabled)
            //        {
            //            isShowContent = true;
            //        }
            //    }
            //    else
            //    {
            //        bool isCEOCertificationEnabled = Helper.IsCapabilityActivatedForCurrentRecPeriod(ARTEnums.Capability.CEOCFOCertification);

            //        if (isCEOCertificationEnabled)
            //        {
            //            isShowContent = true;
            //        }
            //        else
            //        {
            //            isShowContent = ShowHideContent(pnlDeactiveCertification, pnlActiveCertification, ePage, userRoleID);
            //        }
            //    }
            //}
            //else
            //{
            //    if (ePage == WebEnums.ARTPages.MandatoryReportSignOff || ePage == WebEnums.ARTPages.MandatoryReportsList)
            //    {
            //        bool isMandatoryReportEnabled = Helper.IsCapabilityActivatedForCurrentRecPeriod(ARTEnums.Capability.MandatoryReportSignoff);

            //        if (isMandatoryReportEnabled)
            //        {
            //            isShowContent = true;
            //        }
            //    }
            //    else
            //    {
            //        isShowContent = ShowHideContent(pnlDeactiveCertification, pnlActiveCertification, ePage, userRoleID);
            //    }
            //}

            if (bCertificationStatus)
            {
                pnlActiveCertification.Visible = true;
                pnlDeactiveCertification.Visible = false;
            }
            else
            {
                pnlActiveCertification.Visible = false;
                pnlDeactiveCertification.Visible = true;
            }

            return bCertificationStatus;
        }

        //private static bool ShowHideContent(Panel pnlDeactiveCertification, Panel pnlActiveCertification, WebEnums.ARTPages ePage, int? userRoleID)
        //{
        //    bool isShowContent = false;
        //    bool isCertificationEnabled = false;
        //    ARTEnums.UserRole eUserRole = (ARTEnums.UserRole)System.Enum.Parse(typeof(ARTEnums.UserRole), userRoleID.ToString());
        //    switch (ePage)
        //    {
        //        case WebEnums.ARTPages.CertificationStatus:
        //            /*
        //             * If P and other roles then this page is available only when CertificationActivation is available
        //             * If R / A then this page is available when MandatoryReportSignoff + CertificationActivation is available
        //             * For CEO / CFO this page is available any Certification is available
        //             */
        //            switch (eUserRole)
        //            {
        //                case ARTEnums.UserRole.REVIEWER:
        //                case ARTEnums.UserRole.APPROVER:
        //                    isCertificationEnabled = Helper.IsCapabilityActivatedForCurrentRecPeriod(ARTEnums.Capability.CertificationActivation)
        //                        || Helper.IsCapabilityActivatedForCurrentRecPeriod(ARTEnums.Capability.MandatoryReportSignoff);
        //                    break;

        //                case ARTEnums.UserRole.CEO_CFO:
        //                    isCertificationEnabled = Helper.IsCapabilityActivatedForCurrentRecPeriod(ARTEnums.Capability.CertificationActivation, true);
        //                    break;

        //                default:
        //                    isCertificationEnabled = Helper.IsCapabilityActivatedForCurrentRecPeriod(ARTEnums.Capability.CertificationActivation);
        //                    break;
        //            }
        //            break;

        //        default:
        //            isCertificationEnabled = Helper.IsCapabilityActivatedForCurrentRecPeriod(ARTEnums.Capability.CertificationActivation);
        //            break;
        //    }

        //    if (isCertificationEnabled)
        //    {
        //        isShowContent = true;
        //    }

        //    return isShowContent;
        //}
        #endregion

        #region Alerts

        public static bool IsAlertConfiguredByTheCompany(WebEnums.Alert eAlert, out int? companyAlertID)
        {
            short alertID = (short)eAlert;
            List<CompanyAlertInfo> oCompanyAlertInfoCollection = SessionHelper.SelectComapnyAlertByCompanyIDAndRecPeriodID();

            CompanyAlertInfo oCompanyAlertInfo = oCompanyAlertInfoCollection.Find(a => a.AlertID == alertID);

            if (oCompanyAlertInfo != null)
            {
                companyAlertID = oCompanyAlertInfo.CompanyAlertID;
                return true;
            }
            companyAlertID = null;
            return false;
        }

        #endregion

        public static void RaiseAlertFromReconciliationTemplates(string commandName, long? accountID, int? netAccountID)
        {
            List<long> oAccountIDCollection = new List<long>();
            List<int> oNetAccountIDCollection = new List<int>();

            if (accountID.HasValue && accountID.Value > 0)
            {
                oAccountIDCollection.Add(accountID.Value);
            }

            if (netAccountID.HasValue && netAccountID.Value > 0)
            {
                oNetAccountIDCollection.Add(netAccountID.Value);
            }

            switch (commandName)
            {
                case RecFormButtonCommandName.DENY:
                case RecFormButtonCommandName.REJECT:
                    AlertHelper.RaiseAlert(WebEnums.Alert.AccountReconciliationHasBeenRejected_Denied, SessionHelper.CurrentReconciliationPeriodID
                        , SessionHelper.CurrentReconciliationPeriodEndDate
                        , oAccountIDCollection, oNetAccountIDCollection, SessionHelper.CurrentRoleID.Value, null);
                    AlertHelper.RaiseAlert(WebEnums.Alert.YouHaveXAccountsPendingModification, SessionHelper.CurrentReconciliationPeriodID
                        , SessionHelper.CurrentReconciliationPeriodEndDate
                        , oAccountIDCollection, oNetAccountIDCollection, SessionHelper.CurrentRoleID.Value, null);
                    break;
            }
        }


        public static AccountHdrInfo CloneAccountOwnershipInfo(AccountHdrInfo oOldAccountHdrInfo)
        {
            AccountHdrInfo oAccountHdrInfo = new AccountHdrInfo();
            oAccountHdrInfo.AccountID = oOldAccountHdrInfo.AccountID;
            oAccountHdrInfo.PreparerUserID = oOldAccountHdrInfo.PreparerUserID;
            oAccountHdrInfo.ReviewerUserID = oOldAccountHdrInfo.ReviewerUserID;
            oAccountHdrInfo.ApproverUserID = oOldAccountHdrInfo.ApproverUserID;

            return oAccountHdrInfo;
        }


        public static string GetRiskRating(short? riskRatingID)
        {
            string riskRating = string.Empty;
            List<RiskRatingMstInfo> oRiskRatingMstInfoCollection = SessionHelper.GetAllRiskRating();
            RiskRatingMstInfo oRiskRatingMstInfo = oRiskRatingMstInfoCollection.Find(o => o.RiskRatingID.Value == riskRatingID);
            if (oRiskRatingMstInfo != null)
                riskRating = oRiskRatingMstInfo.RiskRating;

            return riskRating;
        }

        public static ReconciliationPeriodInfo GetRecPeriodInfo(int? RecPeriodID)
        {
            ReconciliationPeriodInfo oReconciliationPeriodInfo = null;
            if (RecPeriodID != null)
            {
                List<ReconciliationPeriodInfo> oReconciliationPeriodInfoCollection = CacheHelper.GetAllReconciliationPeriods(null);
                oReconciliationPeriodInfo = oReconciliationPeriodInfoCollection.Find(c => c.ReconciliationPeriodID == RecPeriodID);
            }
            return oReconciliationPeriodInfo;
        }

        public static void RegisterPostBackToControls(PageBase oPageBase, Control oControl)
        {
            try
            {
                MasterPageBase oMasterPageBase;
                if (oPageBase.Master is MasterPageBase)
                {
                    oMasterPageBase = (MasterPageBase)oPageBase.Master;
                }
                else
                {
                    oMasterPageBase = (MasterPageBase)oPageBase.Master.Master;
                }

                oMasterPageBase.RegisterPostBackToControls(oControl);
            }
            catch (Exception)
            {
            }
        }




        public static void ShowExportToolbarOnCertificationPages(PageBase oPageBase, bool bShowExportToolbar)
        {
            Helper.ShowExportToolbarOnCertificationPages(oPageBase, bShowExportToolbar, string.Empty, null, WebEnums.CertificationType.None);
        }


        public static void ShowExportToolbarOnCertificationPages(PageBase oPageBase, bool bShowExportToolbar, string printUrl, int? labelID, WebEnums.CertificationType eCertificationType)
        {
            CertificationMasterPageBase oCertificationMasterPageBase = (CertificationMasterPageBase)oPageBase.Master;
            oCertificationMasterPageBase.ShowExportToolbar = bShowExportToolbar;
            oCertificationMasterPageBase.PrintUrl = printUrl;
            oCertificationMasterPageBase.PageTitleLabeID = labelID;
            oCertificationMasterPageBase.CertificationType = eCertificationType;
        }

        /// <summary>
        /// Check for Page based on Closed / Open / In Progress
        /// </summary>
        /// <returns></returns>
        public static bool DisablePageBasedOnRecPeriodStatus()
        {
            bool bDisable = false;
            WebEnums.RecPeriodStatus CurrentRecProcessStatus = SessionHelper.CurrentRecProcessStatusEnum;
            if (CurrentRecProcessStatus == WebEnums.RecPeriodStatus.Closed
                || CurrentRecProcessStatus == WebEnums.RecPeriodStatus.InProgress
                || CurrentRecProcessStatus == WebEnums.RecPeriodStatus.Skipped)
            {
                bDisable = true;
            }
            return bDisable;
        }

        public static string GetRedirectURLForTemplatePages(bool? isSRA, WebEnums.ARTPages eARTPages)
        {
            StringBuilder sbURL = new StringBuilder();

            switch (eARTPages)
            {
                case WebEnums.ARTPages.AccountViewer:
                    sbURL.Append("~/Pages/AccountViewer.aspx?");
                    break;

                case WebEnums.ARTPages.SystemReconciledAccounts:
                    sbURL.Append("~/Pages/SystemReconciledAccount.aspx?");
                    break;
            }
            sbURL.Append(QueryStringConstants.IS_SRA);
            sbURL.Append("=");

            if (isSRA == true)
            {
                sbURL.Append("1");
            }
            else
            {
                sbURL.Append("0");
            }

            return sbURL.ToString();
        }


        public static bool DisablePageBasedOnRecPeriodStatusForCertification()
        {
            bool bDisable = false;
            WebEnums.RecPeriodStatus CurrentRecProcessStatus = SessionHelper.CurrentRecProcessStatusEnum;
            if (CurrentRecProcessStatus == WebEnums.RecPeriodStatus.Closed
                || CurrentRecProcessStatus == WebEnums.RecPeriodStatus.NotStarted
                || CurrentRecProcessStatus == WebEnums.RecPeriodStatus.Skipped)
            {
                bDisable = true;
            }
            return bDisable;
        }


        public static void EnableDisableOrgHierarchyForNoKey(ExLabel lblOrganizationalHiearachy, TextBox txtOrganizationalHiearachy, DropDownList ddlOrgHierarchy, ExImageButton btnAddMore)
        {
            List<GeographyStructureHdrInfo> oGeographyStructureHdrInfoCollection = SessionHelper.GetOrganizationalHierarchy(SessionHelper.CurrentCompanyID);

            if (oGeographyStructureHdrInfoCollection == null
                || oGeographyStructureHdrInfoCollection.Count == 1)
            {
                // Means only Select
                lblOrganizationalHiearachy.Enabled = false;
                txtOrganizationalHiearachy.Enabled = false;
                if (ddlOrgHierarchy != null)
                {
                    ddlOrgHierarchy.Enabled = false;
                }

                if (btnAddMore != null)
                {
                    btnAddMore.Enabled = false;
                }
            }
            else
            {
                lblOrganizationalHiearachy.Enabled = true;
                txtOrganizationalHiearachy.Enabled = true;
                if (ddlOrgHierarchy != null)
                {

                    ddlOrgHierarchy.Enabled = true;
                }

                if (btnAddMore != null)
                {
                    btnAddMore.Enabled = true;
                }
            }
        }

        public static bool IsDecimal(string strDecimal)
        {
            bool bIsDecimal = true;
            char[] chrArray = strDecimal.ToCharArray(); ;
            String allowedChars = "0123456789.";
            if (strDecimal == String.Empty || strDecimal == "")
            {
                bIsDecimal = false;
            }
            else
            {
                for (int i = 0; i < strDecimal.Length; i++)
                {
                    if (i == 0 && chrArray[0] == '-' && chrArray.Length > 1)
                        continue;
                    if (allowedChars.IndexOf(chrArray[i]) == -1)
                    {
                        bIsDecimal = false;
                        break;
                    }
                }
            }
            return bIsDecimal;
        }

        public static FinancialYearHdrInfo GetFirstFinancialYear()
        {
            List<FinancialYearHdrInfo> oFinancialYearHdrInfoCollection = CacheHelper.GetAllFinancialYears();
            if (oFinancialYearHdrInfoCollection != null && oFinancialYearHdrInfoCollection.Count > 0)
            {
                return oFinancialYearHdrInfoCollection[0];
            }
            else
            {
                return null;
            }
        }

        public static FinancialYearHdrInfo GetFinancialYearInfo(int? fyID)
        {
            List<FinancialYearHdrInfo> oFinancialYearHdrInfoCollection = CacheHelper.GetAllFinancialYears();
            FinancialYearHdrInfo oFinancialYearHdrInfo = oFinancialYearHdrInfoCollection.Find(c => c.FinancialYearID == fyID);
            return oFinancialYearHdrInfo;
        }

        public static string GetMenuKeyForRecForms(WebEnums.ARTPages eARTPages)
        {
            string menuKey = string.Empty;
            switch (eARTPages)
            {
                case WebEnums.ARTPages.AccountViewer:
                    menuKey = "AccountViewer";
                    break;

                case WebEnums.ARTPages.SystemReconciledAccounts:
                    menuKey = "SystemReconciledAccounts";
                    break;
            }
            return menuKey;
        }

        #region "Feature + Capability"
        public static bool IsFeatureActivated(WebEnums.Feature eFeature, int? recPeriodID)
        {
            bool isActivated = false;
            Dictionary<WebEnums.Feature, Boolean?> dictCompanyFeatures = null;

            dictCompanyFeatures = CacheHelper.GetCompanyFeatures(SessionHelper.CurrentCompanyID, recPeriodID);
            if (dictCompanyFeatures != null && dictCompanyFeatures.ContainsKey(eFeature))
            {
                isActivated = true;
            }
            return isActivated;
        }

        /// <summary>
        /// Get Mode (Visible / Hidden / Disable) based on Feature and Capability for Current Rec Period
        /// </summary>
        /// <param name="eFeature"></param>
        /// <param name="eCapability"></param>
        /// <returns></returns>
        public static WebEnums.FeatureCapabilityMode GetFeatureCapabilityModeForCurrentRecPeriod(WebEnums.Feature eFeature, ARTEnums.Capability eCapability)
        {
            return GetFeatureCapabilityMode(eFeature, eCapability, SessionHelper.CurrentReconciliationPeriodID);
        }

        /// <summary>
        /// Get Mode (Visible / Hidden / Disable) based on Feature and Capability for the Rec Period
        /// </summary>
        /// <param name="eFeature"></param>
        /// <param name="eCapability"></param>
        /// <param name="RecPeriodID"></param>
        /// <returns></returns>
        public static WebEnums.FeatureCapabilityMode GetFeatureCapabilityMode(WebEnums.Feature eFeature, ARTEnums.Capability eCapability, int? RecPeriodID)
        {
            /* Check feature 
             * If Feature Activated
             *      Check Capability
             *      If Capability Activated 
             *          Set Visible (Feature Activated + Capability Activated)
             *      ELSE
             *          Set Disable (Feature Activated + Capability NOT Activated)
             * ELSE 
             *      return Hidden (Feature NOT Activated)
             *      
             */

            WebEnums.FeatureCapabilityMode eFeatureCapabilityMode = WebEnums.FeatureCapabilityMode.Hidden;
            if (Helper.IsFeatureActivated(eFeature, RecPeriodID))
            {
                if (Helper.IsCapabilityActivatedForRecPeriodID(eCapability, RecPeriodID.Value, true))
                {
                    eFeatureCapabilityMode = WebEnums.FeatureCapabilityMode.Visible;
                }
                else
                {
                    eFeatureCapabilityMode = WebEnums.FeatureCapabilityMode.Disable;
                }
            }
            else
            {
                eFeatureCapabilityMode = WebEnums.FeatureCapabilityMode.Hidden;
            }

            return eFeatureCapabilityMode;
        }

        #region AUDITRoleFeaters
        public static bool IsQualityScoreEnabled()
        {

            List<CompanyAttributeConfigInfo> oAttributeConfigInfo = AttributeConfigHelper.GetCompanyAttributeConfigInfoList(false, WebEnums.AttributeSetType.RoleConfig);
            CompanyAttributeConfigInfo oQualitySectionConfig = oAttributeConfigInfo.Find(c => c.AttributeID == (short)ARTEnums.AttributeList.NotSeeQSSection);
            bool isQualityScoreConfigSelected = (bool)oQualitySectionConfig.IsEnabled;
            return !isQualityScoreConfigSelected;
        }
        public static bool IsReviewNotesEnabled()
        {
            List<CompanyAttributeConfigInfo> oAttributeConfigInfo = AttributeConfigHelper.GetCompanyAttributeConfigInfoList(false, WebEnums.AttributeSetType.RoleConfig);
            CompanyAttributeConfigInfo oReviewNotesConfig = oAttributeConfigInfo.Find(c => c.AttributeID == (short)ARTEnums.AttributeList.NotSeeReviewNoteSection);
            bool isReviewNotesConfigSelected = (bool)oReviewNotesConfig.IsEnabled;
            return !isReviewNotesConfigSelected;
        }
        #endregion

        #endregion

        #region Package

        public static PackageMstInfo GetPackageMstInfoByPackageId(Int32 packageId)
        {
            PackageMstInfo oPackageMstInfo = null;
            List<PackageMstInfo> oPackageMstInfoCollection = SessionHelper.GetAllPackage();
            oPackageMstInfo = oPackageMstInfoCollection.Find(x => x.PackageID == packageId);
            //foreach (PackageMstInfo info in oPackageMstInfoCollection)
            //{
            //    if (info.PackageID == packageId)
            //    {
            //        oPackageMstInfo = info;
            //        break;
            //    }
            //}
            return oPackageMstInfo;
        }

        #endregion

        //public static void ShowCompanyLogo(Image imgCompanyLogo, Page ePage)
        //{
        //    try
        //    {
        //        ICompany oCompanyClient = RemotingHelper.GetCompanyObject();
        //        string logoPath = oCompanyClient.GetCompanyLogo(SessionHelper.CurrentCompanyID);
        //        if (string.IsNullOrEmpty(logoPath))
        //        {
        //            imgCompanyLogo.Visible = false;
        //        }
        //        else
        //        {
        //           imgCompanyLogo.ImageUrl = ePage.ResolveUrl("DownloadAttachment.aspx") + "?" + QueryStringConstants.FILE_PATH + "=" + logoPath;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Helper.LogException(ex);
        //    }
        //}



        public static string GetSuccessMessage(int entityNameLabelID)
        {
            return string.Format(LanguageUtil.GetValue(2085), LanguageUtil.GetValue(entityNameLabelID));
        }

        public static string GetCurrentAccountBCCY(long? glDataID)
        {
            IGLData oGLDataClient = RemotingHelper.GetGLDataObject();
            return oGLDataClient.GetBCCYByGLDataID(glDataID.Value, Helper.GetAppUserInfo());
        }

        /// <summary>
        /// Set hyper link navigate url
        /// </summary>
        /// <param name="oExHyperLink"></param>
        /// <param name="url"></param>
        public static void SetUrlForHyperlink(ExHyperLink oExHyperLink, string url)
        {
            if (oExHyperLink != null)
            {
                if (url != null && url != string.Empty)
                    oExHyperLink.NavigateUrl = url;
                else
                    oExHyperLink.NavigateUrl = "javascript:";
            }
        }


        public static CultureInfo GetTestCurrentCultureInfoWithoutSession(CultureInfo oCurrentCultureInfo)
        {
            int? testLCID = AppSettingHelper.GetTestLCID();

            if (testLCID != null)
            {
                oCurrentCultureInfo = new CultureInfo(testLCID.Value);
            }
            return oCurrentCultureInfo;
        }

        public static int GetDaysBetweenDateRanges(DateTime? startDate, DateTime? endDate)
        {
            int noOfDays = endDate.Value.Subtract(startDate.Value).Days + 1;
            return noOfDays;
        }

        /// <summary>
        /// Set ExTextBox Display Mode
        /// </summary>
        /// <param name="oExTextBox"></param>
        /// <param name="bShowLabel"></param>
        public static void SetExTextBoxDisplayMode(ExTextBox oExTextBox, bool bShowLabel)
        {
            if (bShowLabel)
            {
                oExTextBox.DisplayMode = WebControlEnum.DisplayMode.Label;
            }
            else
            {
                oExTextBox.DisplayMode = WebControlEnum.DisplayMode.TextBox;
            }
        }

        public static int GetAging(DateTime? dtOpen)
        {
            DateTime dtNow = DateTime.Now.Date;
            TimeSpan ts = dtNow.Date.Subtract(dtOpen.Value.Date);
            return (int)ts.TotalDays;
        }
        /// <summary>
        /// Get Preparer Name by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string GetPreparerNameByID(Int32? id)
        {
            string name = null;
            if (id != null && id > 0)
            {
                List<UserHdrInfo> oUserHdrInfoList = CacheHelper.SelectAllPreparersForCurrentCompany();
                if (oUserHdrInfoList != null && oUserHdrInfoList.Count > 0)
                    name = (from user in oUserHdrInfoList
                            where user.UserID == id
                            select user.Name).FirstOrDefault();
            }
            return name;
        }
        /// <summary>
        /// Get Reviewer Name by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string GetReviewerNameByID(Int32? id)
        {
            string name = null;
            if (id != null && id > 0)
            {
                List<UserHdrInfo> oUserHdrInfoList = CacheHelper.SelectAllReviewersForCurrentCompany();
                if (oUserHdrInfoList != null && oUserHdrInfoList.Count > 0)
                    name = (from user in oUserHdrInfoList
                            where user.UserID == id
                            select user.Name).FirstOrDefault();
            }
            return name;
        }
        /// <summary>
        /// Get Approver Name by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string GetApproverNameByID(Int32? id)
        {
            string name = null;
            if (id != null && id > 0)
            {
                List<UserHdrInfo> oUserHdrInfoList = CacheHelper.SelectAllApproversForCurrentCompany();
                if (oUserHdrInfoList != null && oUserHdrInfoList.Count > 0)
                    name = (from user in oUserHdrInfoList
                            where user.UserID == id
                            select user.Name).FirstOrDefault();
            }
            return name;
        }


        /// <summary>
        /// Get Preparer Name by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string GetBackupPreparerNameByID(Int32? id)
        {
            string name = null;
            if (id != null && id > 0)
            {
                List<UserHdrInfo> oUserHdrInfoList = CacheHelper.SelectAllBackupPreparersForCurrentCompany();
                if (oUserHdrInfoList != null && oUserHdrInfoList.Count > 0)
                    name = (from user in oUserHdrInfoList
                            where user.UserID == id
                            select user.Name).FirstOrDefault();
            }
            return name;
        }
        /// <summary>
        /// Get Reviewer Name by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string GetBackupReviewerNameByID(Int32? id)
        {
            string name = null;
            if (id != null && id > 0)
            {
                List<UserHdrInfo> oUserHdrInfoList = CacheHelper.SelectAllBackupReviewersForCurrentCompany();
                if (oUserHdrInfoList != null && oUserHdrInfoList.Count > 0)
                    name = (from user in oUserHdrInfoList
                            where user.UserID == id
                            select user.Name).FirstOrDefault();
            }
            return name;
        }
        /// <summary>
        /// Get Approver Name by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string GetBackupApproverNameByID(Int32? id)
        {
            string name = null;
            if (id != null && id > 0)
            {
                List<UserHdrInfo> oUserHdrInfoList = CacheHelper.SelectAllBackupApproversForCurrentCompany();
                if (oUserHdrInfoList != null && oUserHdrInfoList.Count > 0)
                    name = (from user in oUserHdrInfoList
                            where user.UserID == id
                            select user.Name).FirstOrDefault();
            }
            return name;
        }

        #region SetText functions for Grid
        /// <summary>
        /// Set Text for Hyperlink in Grid
        /// </summary>
        /// <param name="item"></param>
        /// <param name="hlName"></param>
        /// <param name="value"></param>
        public static ExHyperLink SetTextForHyperlink(GridItem item, string hlName, string value)
        {
            if (item != null)
            {
                ExHyperLink oExHyperLink = (ExHyperLink)item.FindControl(hlName);
                if (oExHyperLink != null)
                {
                    oExHyperLink.Text = Helper.GetDisplayStringValue(value);
                }
                return oExHyperLink;
            }
            return null;
        }

        /// <summary>
        /// Set Hyperlink Text and Navigate Url in Grid
        /// </summary>
        /// <param name="item"></param>
        /// <param name="hlName"></param>
        /// <param name="value"></param>
        /// <param name="navigateUrl"></param>
        /// <returns></returns>
        public static ExHyperLink SetTextForHyperlink(GridItem item, string hlName, string value, string navigateUrl)
        {
            ExHyperLink oExHyperLink = SetTextForHyperlink(item, hlName, value);
            Helper.SetUrlForHyperlink(oExHyperLink, navigateUrl);
            return oExHyperLink;
        }

        /// <summary>
        /// Set Text and Tooltip Label ID for Hyperlink in Grid
        /// </summary>
        /// <param name="item"></param>
        /// <param name="hlName"></param>
        /// <param name="value"></param>
        /// <param name="toolTipLabelID"></param>
        public static ExHyperLink SetTextForHyperlink(GridItem item, string hlName, string value, int? toolTipLabelID)
        {
            ExHyperLink oExHyperLink = SetTextForHyperlink(item, hlName, value);
            if (oExHyperLink != null && toolTipLabelID != null && toolTipLabelID > 0)
            {
                oExHyperLink.ToolTipLabelID = toolTipLabelID.Value;
            }
            return oExHyperLink;
        }

        /// <summary>
        /// Set Text, Url and Tooltip Label ID for Hyperlink in Grid
        /// </summary>
        /// <param name="item"></param>
        /// <param name="hlName"></param>
        /// <param name="value"></param>
        /// <param name="navigateUrl"></param>
        /// <param name="toolTipLabelID"></param>
        /// <returns></returns>
        public static ExHyperLink SetTextForHyperlink(GridItem item, string hlName, string value, string navigateUrl, int? toolTipLabelID)
        {
            ExHyperLink oExHyperLink = SetTextForHyperlink(item, hlName, value, navigateUrl);
            if (oExHyperLink != null && toolTipLabelID != null && toolTipLabelID > 0)
            {
                oExHyperLink.ToolTipLabelID = toolTipLabelID.Value;
            }
            return oExHyperLink;
        }

        /// <summary>
        /// Set Text for Label in Grid
        /// </summary>
        /// <param name="item"></param>
        /// <param name="lblName"></param>
        /// <param name="value"></param>
        public static ExLabel SetTextForLabel(GridItem item, string lblName, string value, string gridSuffix = "")
        {
            if (item != null)
            {
                ExLabel oExLabel = (ExLabel)item.FindControl(lblName + gridSuffix);
                if (oExLabel != null)
                {
                    oExLabel.Text = Helper.GetDisplayStringValue(value);
                }
                return oExLabel;
            }
            return null;
        }
        #endregion

        /// <summary>
        /// Fill Common Service Parameters
        /// </summary>
        /// <param name="oParamInfoBase"></param>
        public static void FillCommonServiceParams(ParamInfoBase oParamInfoBase)
        {
            if (oParamInfoBase != null)
            {
                oParamInfoBase.UserID = SessionHelper.CurrentUserID;
                oParamInfoBase.RoleID = SessionHelper.CurrentRoleID;
                oParamInfoBase.RecPeriodID = SessionHelper.CurrentReconciliationPeriodID;
                oParamInfoBase.CompanyID = SessionHelper.CurrentCompanyID;
                oParamInfoBase.UserLanguageID = SessionHelper.GetUserLanguage();
            }
        }

        /// <summary>
        /// Fill Common XML To Datatable
        /// </summary>
        /// <param name="oParamInfoBase"></param>
        public static DataTable GetXmlToDataTable(string XMLString)
        {
            try
            {
                StringReader strReader = new StringReader(XMLString.ToString());
                DataSet oDS = new DataSet();
                oDS.ReadXml(strReader);
                return oDS.Tables[0];
            }
            catch (Exception)
            {

            }
            return null;
        }
        public static DataTable GetXmlToDataTable(string XMLString, List<MatchingSourceColumnInfo> oColumns, string TableName)
        {
            try
            {
                if (XMLString.Contains("<GLTBS>"))
                    TableName = "GLTBS";
                else if (XMLString.Contains("<NBF>"))
                    TableName = "NBF";
                else if (XMLString.Contains("<SubSetData>"))
                    TableName = "SubSetData";
                else if (XMLString.Contains("<ChildAccounts>"))
                    TableName = "ChildAccounts";


                DataTable oDt = new DataTable();
                if (oColumns != null && oColumns.Count > 0)
                {
                    foreach (MatchingSourceColumnInfo oMatchingSourceColumnInfo in oColumns)
                    {
                        switch (oMatchingSourceColumnInfo.DataTypeID)
                        {
                            case (short)WebEnums.DataType.String:
                                DataColumn dc = oDt.Columns.Add(oMatchingSourceColumnInfo.ColumnName, Type.GetType("System.String"));
                                break;
                            case (short)WebEnums.DataType.DataTime:
                                DataColumn dc1 = oDt.Columns.Add(oMatchingSourceColumnInfo.ColumnName, Type.GetType("System.DateTime"));
                                break;
                            case (short)WebEnums.DataType.Integer:
                                DataColumn dc2 = oDt.Columns.Add(oMatchingSourceColumnInfo.ColumnName, Type.GetType("System.Int64"));
                                break;
                            case (short)WebEnums.DataType.Decimal:
                                DataColumn dc3 = oDt.Columns.Add(oMatchingSourceColumnInfo.ColumnName, Type.GetType("System.Decimal"));
                                break;

                        }
                    }
                }
                if (oDt.Columns.Count > 0)
                {
                    String Filter = LanguageUtil.GetValue(2853);
                    DataColumn dc = oDt.Columns.Add(AddedGLTBSImportFields.EXCELROWNUMBER, Type.GetType("System.Int32"));
                    DataColumn dc1 = oDt.Columns.Add(AddedGLTBSImportFields.ISRECITEMCREATED, Type.GetType("System.Boolean"));
                    DataColumn dc2 = oDt.Columns.Add(Filter, Type.GetType("System.String"));
                }
                StringReader strReader = new StringReader(XMLString.ToString());
                oDt.TableName = TableName;
                System.IO.StringWriter writer = new System.IO.StringWriter();
                StringBuilder sbXML = new StringBuilder();
                oDt.WriteXmlSchema(writer, true);
                StringReader strReaderXMLSchema = new StringReader(writer.ToString());
                DataSet oDS = new DataSet();
                oDS.ReadXmlSchema(strReaderXMLSchema);
                oDS.ReadXml(strReader);
                return oDS.Tables[0];
            }
            catch (Exception)
            {

            }
            return null;
        }
        /// <summary>
        /// Fill Common Datatable To XMLString
        /// </summary>
        /// <param name="oParamInfoBase"></param>
        public static string GetDataTableToXMLString(DataTable oDT)
        {
            try
            {
                oDT.TableName = "SubSetData";
                StringWriter writerXML = new StringWriter();
                oDT.WriteXml(writerXML, true);
                StringBuilder stringXML = new StringBuilder();
                stringXML.Append(writerXML.ToString());
                return stringXML.ToString();
            }
            catch (Exception)
            {

            }
            return "";
        }

        public static void ReprocessSRA()
        {
            /* This mathod Process Materiality And SRA for the company
             * Internally it Calls 2 sp`s
             *      usp_SRV_UPD_GLDataHdrForMateriality For Materiality
             *      usp_SRV_UPD_GLDataHdrForSRA  For  SRA      
             */

            int rowAffected = 0;
            if (SessionHelper.CurrentReconciliationPeriodID != null)
            {
                IUtility oUtilityClient = RemotingHelper.GetUtilityObject();
                rowAffected = oUtilityClient.ProcessMaterialityAndSRAByCompanyID(SessionHelper.CurrentCompanyID.Value, SessionHelper.CurrentReconciliationPeriodID.Value, SessionHelper.CurrentUserLoginID, DateTime.Now, Helper.GetAppUserInfo());
            }

            RedirectToHomePage(2260);

        }
        /// <summary>
        /// Convert Xml to Data Set
        /// </summary>
        /// <param name="xmlString"></param>
        /// <returns></returns>
        public static void LoadXmlToDataSet(DataSet oDataSet, string xmlDataString)
        {
            try
            {
                if (oDataSet != null)
                {
                    oDataSet.Clear();
                    if (!string.IsNullOrEmpty(xmlDataString))
                    {
                        StringReader oStringReader = new StringReader(xmlDataString);
                        oDataSet.ReadXml(oStringReader);
                    }
                }
            }
            catch (Exception ex)
            {
                Helper.LogException(ex);
                throw new ARTException(5000293);
            }
        }

        /// <summary>
        /// Finds the column.
        /// </summary>
        /// <param name="oDataTable">The o data table.</param>
        /// <param name="colName">Name of the col.</param>
        /// <returns></returns>
        public static DataColumn FindDataColumn(DataTable oDataTable, string colName)
        {
            if (oDataTable != null)
            {
                for (int i = 0; i < oDataTable.Columns.Count; i++)
                {
                    if (oDataTable.Columns[i].ColumnName == colName)
                        return oDataTable.Columns[i];
                }
            }
            return null;
        }

        /// <summary>
        /// Finds the data table.
        /// </summary>
        /// <param name="oDataSet">The o data set.</param>
        /// <param name="tblName">Name of the TBL.</param>
        /// <returns></returns>
        public static DataTable FindDataTable(DataSet oDataSet, string tblName)
        {
            if (oDataSet != null)
            {
                for (int i = 0; i < oDataSet.Tables.Count; i++)
                {
                    if (oDataSet.Tables[i].TableName == tblName)
                        return oDataSet.Tables[i];
                }
            }
            return null;
        }

        /// <summary>
        /// Moves the data rows.
        /// </summary>
        /// <param name="dtSource">The dt source.</param>
        /// <param name="dtTarget">The dt target.</param>
        /// <param name="dataRowsToMove">The data rows to move.</param>
        public static void MoveDataRows(DataTable dtSource, DataTable dtTarget, DataRow[] dataRowsToMove)
        {
            try
            {
                if (dtSource != null && dtTarget != null && dataRowsToMove != null && dataRowsToMove.Length > 0)
                {
                    // Copy Rows to Target Table
                    AddDataRows(dtTarget, dataRowsToMove);
                    // Remove Rows from Source Tables
                    RemoveDataRows(dtSource, dataRowsToMove);
                }
            }
            catch (Exception ex)
            {
                Helper.LogException(ex);
                throw new ARTException(5000288);
            }
        }

        /// <summary>
        /// Adds the data rows.
        /// </summary>
        /// <param name="dtTarget">The dt target.</param>
        /// <param name="dataRowsToMove">The data rows to move.</param>
        public static void AddDataRows(DataTable dtTarget, DataRow[] dataRowsToMove)
        {
            try
            {
                dataRowsToMove.CopyToDataTable(dtTarget, LoadOption.OverwriteChanges);
            }
            catch (Exception ex)
            {
                Helper.LogException(ex);
                throw new ARTException(5000293);
            }
        }

        /// <summary>
        /// Removes the rows.
        /// </summary>
        /// <param name="dtSource">The dt source.</param>
        /// <param name="dataRows">The data rows.</param>
        public static void RemoveDataRows(DataTable dtSource, DataRow[] dataRows)
        {
            // Remove Rows from Source
            foreach (DataRow dr in dataRows)
                dtSource.Rows.Remove(dr);
        }

        /// <summary>
        /// Merges the tables.
        /// </summary>
        /// <param name="tblSource1">The TBL source1.</param>
        /// <param name="tblSource2">The TBL source2.</param>
        /// <returns></returns>
        public static DataTable MergeTables(DataTable tblSource1, DataTable tblSource2)
        {
            DataTable tblResult = null;
            try
            {
                if (tblSource1 != null)
                {
                    tblResult = tblSource1.Copy();
                }
                if (tblSource2 != null)
                {
                    if (tblResult == null)
                        tblResult = tblSource2.Copy();
                    else
                    {
                        DataRow[] drSource2 = tblSource2.Select();
                        drSource2.CopyToDataTable(tblResult, LoadOption.OverwriteChanges);
                    }
                }
            }
            catch (Exception ex)
            {
                Helper.LogException(ex);
                throw new ARTException(5000293);
            }
            return tblResult;
        }

        /// <summary>
        /// Gets the XML from data set.
        /// </summary>
        /// <param name="oDataSet">The o data set.</param>
        /// <returns></returns>
        public static string GetXmlFromDataSet(DataSet oDataSet)
        {
            if (oDataSet != null)
                return oDataSet.GetXml();
            return null;
        }

        /// <summary>
        /// Get Data Set with optional schema load.
        /// </summary>
        /// <param name="xmlSchemaString">The XML schema string.</param>
        /// <returns></returns>
        public static DataSet GetDataSet(string xmlSchemaString)
        {
            DataSet oDataSet = new DataSet();
            try
            {
                if (!string.IsNullOrEmpty(xmlSchemaString))
                {
                    StringReader oStringReader = new StringReader(xmlSchemaString);
                    oDataSet.ReadXmlSchema(oStringReader);
                }
            }
            catch (Exception ex)
            {
                Helper.LogException(ex);
                throw new ARTException(5000293);
            }
            return oDataSet;
        }

        /// <summary>
        /// Clears the dynamic columns.
        /// </summary>
        /// <param name="oExRadGrid">The o ex RAD grid.</param>
        public static void ClearDynamicColumns(ExRadGrid oExRadGrid)
        {
            if (oExRadGrid != null)
            {
                for (int i = oExRadGrid.Columns.Count - 1; i >= 0; i--)
                {
                    ExGridTemplateColumn col = oExRadGrid.Columns[i] as ExGridTemplateColumn;
                    if (col != null && col.IsDynamicColumn)
                        oExRadGrid.Columns.Remove(col);
                }
            }
        }

        /// <summary>
        /// Creates the dynamic ex grid template column.
        /// </summary>
        /// <param name="headerText">The header text.</param>
        /// <param name="eDataType">Type of the e data.</param>
        /// <param name="colName">Name of the col.</param>
        /// <param name="colID">The col ID.</param>
        /// <returns></returns>
        public static ExGridTemplateColumn CreateDynamicExGridTemplateColumn(string headerText, WebEnums.DataType eDataType, long? colID, string colName, string dataFieldName)
        {
            ExGridTemplateColumn col = new ExGridTemplateColumn();
            GridViewItemTemplate oGridViewItemTemplate = new GridViewItemTemplate(ListItemType.Item, colName, dataFieldName, colID.GetValueOrDefault(), eDataType);
            col.HeaderText = headerText;
            col.UniqueName = colName;
            col.ItemTemplate = oGridViewItemTemplate;
            col.IsDynamicColumn = true;
            return col;
        }

        public static string ReturnURL(Page oPage)
        {
            string returnUrl = oPage.Request.UrlReferrer.PathAndQuery;
            return returnUrl;
        }


        public static void GetDisplayAddedBy(ExHyperLink oExHyperLink, string firstName, string lastName, string addedByUserID)
        {
            if (oExHyperLink != null)
            {
                oExHyperLink.Text = GetDisplayUserFullName(firstName, lastName);
                oExHyperLink.ToolTip = GetDisplayStringValue(addedByUserID);
            }
        }
        public static void GetDisplayAddedBy(ExLabel oExLabel, string firstName, string lastName, string addedByUserID)
        {
            if (oExLabel != null)
            {
                oExLabel.Text = GetDisplayUserFullName(firstName, lastName);
                oExLabel.ToolTip = GetDisplayStringValue(addedByUserID);
            }
        }

        /// <summary>
        /// Gets the rec status.
        /// </summary>
        /// <param name="eRecStatus">The e rec status.</param>
        /// <returns></returns>
        public static string GetRecStatus(WebEnums.ReconciliationStatus? eRecStatus)
        {
            string recStatus = string.Empty;
            if (eRecStatus.HasValue)
            {
                List<ReconciliationStatusMstInfo> oReconciliationStatusMstInfoList = SessionHelper.GetAllRecStatus();
                if (oReconciliationStatusMstInfoList != null && oReconciliationStatusMstInfoList.Count > 0)
                {
                    ReconciliationStatusMstInfo oReconciliationStatusMstInfo = oReconciliationStatusMstInfoList.Find(T => T.ReconciliationStatusID == (short)eRecStatus.Value);
                    if (oReconciliationStatusMstInfo != null)
                        recStatus = oReconciliationStatusMstInfo.ReconciliationStatus;
                }
            }
            return recStatus;
        }

        #region QualityScore

        /// <summary>
        /// Gets the quality score status by ID.
        /// </summary>
        /// <param name="qualityScoreStatusID">The quality score status ID.</param>
        /// <returns></returns>
        public static string GetQualityScoreStatusByID(Int16? qualityScoreStatusID)
        {
            string qsStatus = string.Empty;
            if (qualityScoreStatusID.HasValue)
            {
                List<QualityScoreStatusMstInfo> oQualityScoreStatusMstInfoList = SessionHelper.GetAllQualityScoreStatuses();
                if (oQualityScoreStatusMstInfoList != null && oQualityScoreStatusMstInfoList.Count > 0)
                {
                    QualityScoreStatusMstInfo oQualityScoreStatusMstInfo = oQualityScoreStatusMstInfoList.Find(T => T.QualityScoreStatusID == qualityScoreStatusID.Value);
                    if (oQualityScoreStatusMstInfo != null)
                        qsStatus = oQualityScoreStatusMstInfo.QualityScoreStatus;
                }
            }
            return qsStatus;
        }

        /// <summary>
        /// Gets the display string for changed company capabilities in current rec period.
        /// </summary>
        /// <returns></returns>
        public static string GetDisplayStringForChangedCompanyCapabilities()
        {
            string changeString = string.Empty;
            List<CapabilityMstInfo> oCapabilityMstInfoList = SessionHelper.GetAllCapabilities();
            List<CompanyCapabilityInfo> oCompanyCapabilityInfoList = SessionHelper.GetCompanyCapabilityCollectionForCurrentRecPeriod();
            CapabilityMstInfo oCapabilityMstInfo = null;
            if (oCompanyCapabilityInfoList != null & oCompanyCapabilityInfoList.Count > 0 && oCapabilityMstInfoList != null && oCapabilityMstInfoList.Count > 0)
            {
                foreach (CompanyCapabilityInfo item in oCompanyCapabilityInfoList)
                {
                    if (!item.IsCarryForwardedFromPreviousRecPeriod.GetValueOrDefault())
                    {
                        oCapabilityMstInfo = oCapabilityMstInfoList.Find(T => T.CapabilityID == item.CapabilityID);
                        if (oCapabilityMstInfo != null)
                            changeString += oCapabilityMstInfo.Capability + Environment.NewLine;
                    }
                }
            }
            return changeString;
        }

        #endregion

        /// <summary>
        /// Gets the cookie value.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public static string GetCookieValue(string key)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[key];
            if (cookie != null)
                return cookie.Value;
            return null;
        }

        /// <summary>
        /// Sets the cookie value.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        public static void SetCookieValue(string key, string value)
        {
            HttpContext.Current.Response.Cookies[key].Value = value;
        }


        //public static CompanySettingInfo GetCurrentCompanySettingInfo()
        //{
        //    // If do not exists in session then get from Company Setting
        //    ICompany oCompanyClient = RemotingHelper.GetCompanyObject();
        //    CompanySettingInfo oCompanySettingInfo = oCompanyClient.GetCurrentReconciliationPeriod(SessionHelper.CurrentCompanyID);
        //    return oCompanySettingInfo;
        //}

        public static ReconciliationPeriodInfo GetMaxReconciliationPeriodInfo()
        {
            IReconciliationPeriod oRecPeriodClient = RemotingHelper.GetReconciliationPeriodObject();
            ReconciliationPeriodInfo oCurrentMaxPeriod = oRecPeriodClient.GetMaxCurrentPeriodByCompanyId(SessionHelper.CurrentCompanyID, Helper.GetAppUserInfo());
            return oCurrentMaxPeriod;
        }

        public static SystemLockdownReasonMstInfo GetSystemLockdownReasonMst(ARTEnums.SystemLockdownReason eSystemLockdownReason)
        {
            List<SystemLockdownReasonMstInfo> oSystemLockdownReasonMstInfoList = SessionHelper.GetAllSystemLockdownReasons();
            SystemLockdownReasonMstInfo oSystemLockdownReasonMstInfo = oSystemLockdownReasonMstInfoList.Find(T => T.SystemLockdownReasonID == (short)eSystemLockdownReason);
            return oSystemLockdownReasonMstInfo;
        }

        public static DataImportTypeMstInfo GetDataImportTypeMst(ARTEnums.DataImportType eDataImportType)
        {
            IList<DataImportTypeMstInfo> oDataImportTypeMstInfoList = SessionHelper.GetAllDataImportType();
            DataImportTypeMstInfo oDataImportTypeMstInfo = oDataImportTypeMstInfoList.FirstOrDefault(T => T.DataImportTypeID == (short)eDataImportType);
            return oDataImportTypeMstInfo;
        }

        public static SystemLockdownInfo GetSystemLockdownInfo(ARTEnums.SystemLockdownReason eSystemLockdownReason)
        {
            SystemLockdownInfo oSystemLockdownInfo = new SystemLockdownInfo();
            oSystemLockdownInfo.CompanyID = SessionHelper.CurrentCompanyID;
            oSystemLockdownInfo.RecPeriodID = SessionHelper.CurrentReconciliationPeriodID;
            oSystemLockdownInfo.AddedBy = SessionHelper.CurrentUserLoginID;
            SystemLockdownReasonMstInfo oSystemLockdownReasonMstInfo = GetSystemLockdownReasonMst(eSystemLockdownReason);
            if (oSystemLockdownReasonMstInfo != null)
            {
                oSystemLockdownInfo.SystemLockdownReasonID = oSystemLockdownReasonMstInfo.SystemLockdownReasonID;
                oSystemLockdownInfo.SystemLockdownMessage = oSystemLockdownReasonMstInfo.Description;
            }
            return oSystemLockdownInfo;
        }

        /// <summary>
        /// used to check that user is active or not
        /// </summary>
        /// <param name="oUserHdrInfo"></param>
        /// <returns>bool isActive</returns>
        public static bool IsUserActive(UserHdrInfo oUserHdrInfo)
        {
            bool isActive = false;
            if (oUserHdrInfo != null)
            {
                isActive = oUserHdrInfo.IsActive == null ? false : (bool)oUserHdrInfo.IsActive;
            }
            return isActive;
        }

        public static GridGroupByExpression GetGridGroupByExpressionForTaskListName()
        {
            GridGroupByExpression expression = new GridGroupByExpression();
            GridGroupByField gridGroupByField = new GridGroupByField();
            // SelectFields values (appear in header)

            gridGroupByField = new GridGroupByField();
            gridGroupByField.FieldName = "TaskListName";
            //gridGroupByField.HeaderText = "Task List Name: ";
            //gridGroupByField.HeaderText = LanguageUtil.GetValue(2584) + ": ";
            //gridGroupByField.HeaderValueSeparator = " for current group: ";
            gridGroupByField.FormatString = "<strong>{0}</strong>";
            expression.SelectFields.Add(gridGroupByField);

            gridGroupByField = new GridGroupByField();
            gridGroupByField.FieldName = "TaskListID";
            expression.GroupByFields.Add(gridGroupByField);

            gridGroupByField = new GridGroupByField();
            gridGroupByField.FieldName = "TaskListAddedBy";
            expression.GroupByFields.Add(gridGroupByField);

            return expression;
        }
        public static GridGroupByExpression GetGridGroupByExpressionForTaskSubListName()
        {
            GridGroupByExpression expression = new GridGroupByExpression();
            GridGroupByField gridGroupByField = new GridGroupByField();
            // SelectFields values (appear in header)

            gridGroupByField = new GridGroupByField();
            gridGroupByField.FieldName = "TaskSubListName";
            //gridGroupByField.HeaderText = "Task List Name: ";
            //gridGroupByField.HeaderText = LanguageUtil.GetValue(2584) + ": ";
            //gridGroupByField.HeaderValueSeparator = " for current group: ";
            gridGroupByField.FormatString = "<strong>{0}</strong>";
            expression.SelectFields.Add(gridGroupByField);

            gridGroupByField = new GridGroupByField();
            gridGroupByField.FieldName = "TaskSubListID";
            expression.GroupByFields.Add(gridGroupByField);

            gridGroupByField = new GridGroupByField();
            gridGroupByField.FieldName = "TaskSubListAddedBy";
            expression.GroupByFields.Add(gridGroupByField);

            return expression;
        }

        public static void ValidateRecTemplateForAccountAndNetAccount(PageBase oPageBase, GLDataHdrInfo oGLDataHdrInfo, int? PreviousNetAccountId)
        {
            try
            {
                Control pnlStatus = oPageBase.Master.Master.FindControl("ContentPlaceHolder1").FindControl("pnlStatus");
                Control cphRecProcess = oPageBase.Master.Master.FindControl("ContentPlaceHolder1").FindControl("cphRecProcess");
                if (oGLDataHdrInfo != null && PreviousNetAccountId == 0 && oGLDataHdrInfo.NetAccountID > 0 && oGLDataHdrInfo.AccountID == 0)
                {
                    Helper.ShowRecPage(pnlStatus, cphRecProcess, false);
                    throw new ARTException(5000346);
                }
            }
            catch (ARTException artEx)
            {
                Helper.ShowErrorMessage(oPageBase, artEx);
            }
        }
        public static bool DatesInOrder(List<DateTime> dateList)
        {
            bool retval = true;
            for (int i = 1; i < dateList.Count; i++)
            {
                DateTime dtMin = dateList[i - 1];
                DateTime dtMax = dateList[i];
                if (dtMin > dtMax)
                {
                    retval = false;
                    break;
                }
            }
            return retval;
        }

        /// <summary>
        /// used to check that user is active or not
        /// </summary>
        /// <param name="oUserHdrInfo"></param>
        /// <returns> bool isActive</returns>
        public static bool IsUserActive(List<UserHdrInfo> oUserHdrInfo)
        {
            bool isActive = false;
            if (oUserHdrInfo != null)
            {
                oUserHdrInfo = oUserHdrInfo.Where(user => user.IsActive != true).ToList();
                if (oUserHdrInfo.Count > 0)
                    isActive = false;
                else
                    isActive = true;
            }
            return isActive;
        }
        public static AppUserInfo GetAppUserInfo()
        {
            AppUserInfo oAppUserInfo = new AppUserInfo();
            if (SessionHelper.CurrentUserID.HasValue)
                oAppUserInfo.UserID = SessionHelper.CurrentUserID;

            if (SessionHelper.CurrentRoleID.HasValue)
                oAppUserInfo.RoleID = SessionHelper.CurrentRoleID;

            if (SessionHelper.CurrentCompanyID.HasValue)
                oAppUserInfo.CompanyID = SessionHelper.CurrentCompanyID;

            if (!string.IsNullOrEmpty(SessionHelper.CurrentUserLoginID))
                oAppUserInfo.LoginID = SessionHelper.CurrentUserLoginID;

            if (SessionHelper.CurrentReconciliationPeriodID.HasValue)
                oAppUserInfo.RecPeriodID = SessionHelper.CurrentReconciliationPeriodID;

            if (SessionHelper.CurrentCompanyDatabaseExists.HasValue)
                oAppUserInfo.IsDatabaseExists = SessionHelper.CurrentCompanyDatabaseExists.Value;

            return oAppUserInfo;
        }

        public static bool? IsCompanyDatabaseExists()
        {
            return IsCompanyDatabaseExists(SessionHelper.CurrentCompanyID);
        }

        public static bool? IsCompanyDatabaseExists(int? companyID)
        {
            if (companyID.GetValueOrDefault() > 0)
            {
                ICompany oCompany = RemotingHelper.GetCompanyObject();
                return oCompany.IsDatabaseExists(companyID);
            }
            return null;
        }

        public static CompanyHdrInfo GetCompanyInfoLiteObject(int? CompanyID)
        {
            DateTime? periodEndDate = null;
            if (CompanyID.GetValueOrDefault() > 0)
            {
                if (CompanyID.GetValueOrDefault() == SessionHelper.CurrentCompanyID.GetValueOrDefault())
                    periodEndDate = SessionHelper.CurrentReconciliationPeriodEndDate;
                ICompany oCompanyClient = RemotingHelper.GetCompanyObject();
                CompanyHdrInfo oCompanyHdrInfo = oCompanyClient.GetCompanyDetail(CompanyID, periodEndDate, Helper.GetAppUserInfo());
                LanguageHelper.TranslateCompanyInfo(oCompanyHdrInfo);
                return oCompanyHdrInfo;
            }
            return null;
        }

        public static int GetDefaultChunkSize(int PageSize)
        {
            int DefaultChunkSize;
            int defaultItemCount = Convert.ToInt32(AppSettingHelper.GetAppSettingValue(AppSettingConstants.DEFAULT_CHUNK_SIZE));
            DefaultChunkSize = (defaultItemCount / 10) * PageSize;
            return DefaultChunkSize;
        }

        public static WebEnums.FormMode GetFormModeForRecItemComment(WebEnums.ReconciliationStatus eReconciliationStatus)
        {
            WebEnums.FormMode eFormMode = WebEnums.FormMode.ReadOnly;
            WebEnums.RecPeriodStatus eRecPeriodStatus = SessionHelper.CurrentRecProcessStatusEnum;

            // Check for Certification Started
            if (CertificationHelper.IsCertificationStarted())
                eFormMode = WebEnums.FormMode.ReadOnly;
            else
                eFormMode = WebEnums.FormMode.Edit;

            // If Certification started just return because Form can never be Editable
            if (eFormMode == WebEnums.FormMode.ReadOnly)
            {
                return eFormMode;
            }

            /*
             * 1. Check the Status based on Rec Period Status
             * 1a. If ReadOnly Mode, return
             * 2. Check based on Role
             * 3. Rec Status
             */

            switch (eRecPeriodStatus)
            {
                case WebEnums.RecPeriodStatus.Open:
                case WebEnums.RecPeriodStatus.InProgress:
                    eFormMode = WebEnums.FormMode.Edit;
                    break;

                default:
                    eFormMode = WebEnums.FormMode.ReadOnly;
                    break;
            }

            // If Rec Period is not workable, 
            // just return because Form can never be Editable
            if (eFormMode == WebEnums.FormMode.ReadOnly)
            {
                return eFormMode;
            }

            /*
             * 1. Check the Status based on Rec Period Status
             * 1a. If ReadOnly Mode, return
             * 2. Check based on Role
             * 3. Rec Status
             */
            switch (eReconciliationStatus)
            {
                case WebEnums.ReconciliationStatus.Reconciled:
                case WebEnums.ReconciliationStatus.SysReconciled:
                    eFormMode = WebEnums.FormMode.ReadOnly;
                    break;

                default:
                    eFormMode = WebEnums.FormMode.Edit;
                    break;
            }

            // If Rec is not workable, 
            // just return because Form can never be Editable
            if (eFormMode == WebEnums.FormMode.ReadOnly)
            {
                return eFormMode;
            }

            return eFormMode;
        }
        public static bool IsDueDatesByAccountConfiuredForRecPeriodID(int recPeriodID)
        {
            bool IsDueDatesByAccountConfiured = false;
            if (IsCapabilityActivatedForRecPeriodID(ARTEnums.Capability.DueDateByAccount, recPeriodID, false))
            {
                if (IsFeatureActivated(WebEnums.Feature.DueDateByAccount, recPeriodID))
                    IsDueDatesByAccountConfiured = true;
            }
            return IsDueDatesByAccountConfiured;

        }
        public static string GetRecItemComments(List<RecItemCommentInfo> RecItemCommentInfoList)
        {
            string strComment = string.Empty;
            foreach (var oRecItemCommentInfo in RecItemCommentInfoList)
            {
                if (string.IsNullOrEmpty(strComment))
                    strComment = string.Format("{0} [{1}]: {2}", oRecItemCommentInfo.AddedByUserInfo.Name, Helper.GetDisplayDateTime(oRecItemCommentInfo.DateAdded), oRecItemCommentInfo.Comment);
                else
                    strComment = strComment + "<br style=\"mso-data-placement:same-cell;\">" + string.Format("{0} [{1}]: {2}", oRecItemCommentInfo.AddedByUserInfo.Name, Helper.GetDisplayDateTime(oRecItemCommentInfo.DateAdded), oRecItemCommentInfo.Comment);
            }
            return strComment;
        }

        public static bool IsDualLevelReviewByAccountActivated()
        {
            bool _IsDualLevelReviewByAccountActivated = false;
            short DualLevelReviewTypeID = SessionHelper.GetCurrentDualLevelReviewType();
            if (DualLevelReviewTypeID == (short)WebEnums.DualLevelReviewType.DualLevelReviewbyAccount)
                _IsDualLevelReviewByAccountActivated = true;
            return _IsDualLevelReviewByAccountActivated;
        }

        public static Decimal ConvertMinstoHours(int minutes)
        {
            Decimal min = Convert.ToDecimal(minutes);
            Decimal hours = min / 60;
            return hours;
        }
        public static List<UserLockdownDetailInfo> GetLockdownDetail(int? UserID)
        {
            IUser oUserClient = RemotingHelper.GetUserObject();
            return oUserClient.GetLockdownDetail(UserID, Helper.GetAppUserInfo());

        }
        public static string GetDisplayTaskUserName(List<UserHdrInfo> oUserHdrInfoList)
        {
            string ReturnVal = null;
            if (oUserHdrInfoList != null && oUserHdrInfoList.Count > 0)
            {
                foreach (var oUserHdrInfo in oUserHdrInfoList)
                {
                    if (string.IsNullOrEmpty(ReturnVal))
                        ReturnVal = oUserHdrInfo.Name;
                    else
                        ReturnVal = ReturnVal + WebConstants.USERNAMESEPERATOR + oUserHdrInfo.Name;
                }
            }
            return GetDisplayStringValue(ReturnVal);
        }
        public static ReconciliationPeriodInfo GetReconciliationPeriodInfoForReopen()
        {
            IReconciliationPeriod oRecPeriodClient = RemotingHelper.GetReconciliationPeriodObject();
            ReconciliationPeriodInfo oCurrentMaxPeriod = oRecPeriodClient.GetReconciliationPeriodInfoForReopen(SessionHelper.CurrentCompanyID, Helper.GetAppUserInfo());
            return oCurrentMaxPeriod;
        }
        public static string SetURLForRecPeriodHistory(int? RecPeriodID, out string windowName)
        {
            StringBuilder sbURL = new StringBuilder();
            sbURL.Append("RecPeriodStatusHistoryPopup.aspx");
            sbURL.Append("?" + QueryStringConstants.REC_PERIOD_ID + "=" + RecPeriodID);
            windowName = "RecPeriodStatusHistoryPopup";
            return sbURL.ToString();
        }
        public static List<RecPeriodStatusDetailInfo> GetRecPeriodStatusDetail(int? recPeriodID)
        {
            IReconciliationPeriod oRecPeriodClient = RemotingHelper.GetReconciliationPeriodObject();
            List<RecPeriodStatusDetailInfo> oRecPeriodStatusDetailInfoList = oRecPeriodClient.GetRecPeriodStatusDetail(recPeriodID, Helper.GetAppUserInfo());
            LanguageHelper.TranslateRecPeriodStatusDetailInfoData(oRecPeriodStatusDetailInfoList);
            return oRecPeriodStatusDetailInfoList;
        }
        #region Auto Save Attributes
        public static void SaveAutoSaveAttributeValues(List<ARTEnums.AutoSaveAttribute> eAutoSaveEnumList)
        {
            IUser oUser = RemotingHelper.GetUserObject();
            AutoSaveAttributeParamInfo oAutoSaveAttributeParamInfo = new AutoSaveAttributeParamInfo();
            Helper.FillCommonServiceParams(oAutoSaveAttributeParamInfo);
            List<AutoSaveAttributeValueInfo> oAutoSaveAttributeValueInfoList = oUser.GetAutoSaveAttributeValues(oAutoSaveAttributeParamInfo, Helper.GetAppUserInfo());
            List<AutoSaveAttributeValueInfo> oAutoSaveAttributeValueInfoListToSave = new List<AutoSaveAttributeValueInfo>();
            foreach (ARTEnums.AutoSaveAttribute eAutoSaveEnum in eAutoSaveEnumList)
            {
                AutoSaveAttributeValueInfo oAutoSaveAttributeValueInfo = oAutoSaveAttributeValueInfoList.Find(T => T.AutoSaveAttributeID == (int)eAutoSaveEnum);
                if (oAutoSaveAttributeValueInfo == null)
                {
                    oAutoSaveAttributeValueInfo = new AutoSaveAttributeValueInfo();
                    oAutoSaveAttributeValueInfo.AutoSaveAttributeID = (int)eAutoSaveEnum;
                    oAutoSaveAttributeValueInfo.UserID = SessionHelper.CurrentUserID;
                    oAutoSaveAttributeValueInfo.RoleID = SessionHelper.CurrentRoleID;
                    oAutoSaveAttributeValueInfo.IsActive = true;
                }
                switch (eAutoSaveEnum)
                {
                    case ARTEnums.AutoSaveAttribute.AutoSaveFinancialYearSelection:
                        oAutoSaveAttributeValueInfo.ReferenceID = SessionHelper.CurrentFinancialYearID;
                        oAutoSaveAttributeValueInfoListToSave.Add(oAutoSaveAttributeValueInfo);
                        break;
                    case ARTEnums.AutoSaveAttribute.AutoSaveRecPeriodSelection:
                        oAutoSaveAttributeValueInfo.ReferenceID = SessionHelper.CurrentReconciliationPeriodID;
                        oAutoSaveAttributeValueInfoListToSave.Add(oAutoSaveAttributeValueInfo);
                        break;
                }
            }
            oAutoSaveAttributeParamInfo.AutoSaveAttributeValueInfoList = oAutoSaveAttributeValueInfoListToSave;
            oAutoSaveAttributeParamInfo.UserLoginID = SessionHelper.CurrentUserLoginID;
            oAutoSaveAttributeParamInfo.DateRevised = DateTime.Now;
            oUser.SaveAutoSaveAttributeValues(oAutoSaveAttributeParamInfo, Helper.GetAppUserInfo());
        }

        public static AutoSaveAttributeValueInfo GetAutoSaveAttributeValues(ARTEnums.AutoSaveAttribute eAutoSaveEnum)
        {
            IUser oUser = RemotingHelper.GetUserObject();
            AutoSaveAttributeParamInfo oAutoSaveAttributeParamInfo = new AutoSaveAttributeParamInfo();
            Helper.FillCommonServiceParams(oAutoSaveAttributeParamInfo);
            List<AutoSaveAttributeValueInfo> oAutoSaveAttributeValueInfoList = oUser.GetAutoSaveAttributeValues(oAutoSaveAttributeParamInfo, Helper.GetAppUserInfo());
            AutoSaveAttributeValueInfo oAutoSaveAttributeValueInfo = oAutoSaveAttributeValueInfoList.Find(T => T.AutoSaveAttributeID == (int)eAutoSaveEnum);
            return oAutoSaveAttributeValueInfo;
        }

        public static int? GetAutoSavedRecPeriod()
        {
            int? RecPeriodID = null;
            AutoSaveAttributeValueInfo oAutoSaveAttributeValueInfo = GetAutoSaveAttributeValues(ARTEnums.AutoSaveAttribute.AutoSaveRecPeriodSelection);
            if (oAutoSaveAttributeValueInfo != null)
                RecPeriodID = oAutoSaveAttributeValueInfo.ReferenceID;
            return RecPeriodID;
        }
        public static int? GetAutoSavedFinancialYear()
        {
            int? fyID = null;
            AutoSaveAttributeValueInfo oAutoSaveAttributeValueInfo = GetAutoSaveAttributeValues(ARTEnums.AutoSaveAttribute.AutoSaveFinancialYearSelection);
            if (oAutoSaveAttributeValueInfo != null)
                fyID = oAutoSaveAttributeValueInfo.ReferenceID;
            return fyID;
        }
        #endregion

    }//end of class
}//end of namespace