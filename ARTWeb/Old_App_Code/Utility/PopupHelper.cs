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
using SkyStem.ART.Web.Classes;
using SkyStem.Language.LanguageUtility;
using SkyStem.ART.Client.Exception;
using System.Text;
using SkyStem.ART.Web.Data;
using SkyStem.ART.Client.Model;
using SkyStem.ART.Client.Data;



namespace SkyStem.ART.Web.Utility
{

    /// <summary>
    /// Summary description for PopupHelper
    /// </summary>
    public class PopupHelper
    {
        static PopupHelper()
        {
            log4net.Config.XmlConfigurator.Configure();
        }

        public PopupHelper()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        #region Error Logging methods

        /// <summary>
        /// Popup -> Show Error Message
        /// </summary>
        /// <param name="oPopupPageBase"></param>
        /// <param name="ex"></param>
        public static void ShowErrorMessage(PopupPageBase oPopupPageBase, ARTException ex)
        {
            PopupMasterPageBase oMasterPageBase = (PopupMasterPageBase)oPopupPageBase.Master;

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
        /// Popup -> Show Error Message
        /// </summary>
        /// <param name="oPageBase"></param>
        /// <param name="ex"></param>
        public static void ShowErrorMessage(PopupPageBase oPageBase, Exception ex)
        {
            PopupMasterPageBase oMasterPageBase = (PopupMasterPageBase)oPageBase.Master;
            oMasterPageBase.ShowErrorMessage(ex.Message);
        }
        public static void ShowErrorMessage(PopupPageBase oPageBase, string msg)
        {
            PopupMasterPageBase oMasterPageBase = (PopupMasterPageBase)oPageBase.Master;
            oMasterPageBase.ShowErrorMessage(msg);
        }

        public static void SetPageTitle(PopupPageBase oPageBase, string pageTitle)
        {
            oPageBase.Title = pageTitle;
        }

        #endregion


        #region "Master Page related methods"
        /// <summary>
        /// Popup -> Show the Notes / Input Requirements Sections
        /// </summary>
        /// <param name="PageBase">pass "this"</param>
        /// <param name="oLabelIDCollection">, separated LabelIDs for Input Requirements</param>
        public static void ShowInputRequirementSection(PopupPageBase oPageBase, params int[] oLabelIDCollection)
        {
            PopupMasterPageBase oMasterPageBase = (PopupMasterPageBase)oPageBase.Master;
            oMasterPageBase.ShowInputRequirements(oLabelIDCollection);
        }

        /// <summary>
        /// Popup -> Set the Title of Page
        /// </summary>
        /// <param name="oPageBase">pass "this"</param>
        /// <param name="PageTitleLabelID">Label ID for the Page Title</param>
        public static void SetPageTitle(PopupPageBase oPageBase, int PageTitleLabelID)
        {
            oPageBase.Title = LanguageUtil.GetValue(PageTitleLabelID);
        }


        public static void SetPageTitleWithCategory(PopupPageBase oPageBase, short CategoryID, short CategoryType)
        {
            int CategoryTypeLabelID = SetPageTitleWithCategoryType(CategoryType);
            oPageBase.Title = LanguageUtil.GetValue(CategoryID) + WebConstants.ACCOUNT_ENTITY_SEPARATOR + LanguageUtil.GetValue(CategoryTypeLabelID);

        }

        #endregion


        #region JavascriptMethodCallStringCreation

        public static string GetJavascriptParameterList(long? recItemInputID, string javascriptMethodName, string pageUrl, string mode, bool isForwardedItem, long? accountID, long? glDataID, short? recCategoryTypeID, int? netAccountID, bool? isSRA, short? recCategoryID, object obj)
        {
            StringBuilder sbQueryString = new StringBuilder();

            sbQueryString.Append("javascript:");
            sbQueryString.Append(javascriptMethodName);
            sbQueryString.Append("(");
            sbQueryString.Append("'");
            sbQueryString.Append(pageUrl);
            sbQueryString.Append("?");
            sbQueryString.Append(QueryStringConstants.ACCOUNT_ID);
            sbQueryString.Append("=");
            sbQueryString.Append(accountID);
            sbQueryString.Append("&");
            sbQueryString.Append(QueryStringConstants.NETACCOUNT_ID);
            sbQueryString.Append("=");
            sbQueryString.Append(netAccountID);
            sbQueryString.Append("&");
            sbQueryString.Append(QueryStringConstants.GLDATA_ID);
            sbQueryString.Append("=");
            sbQueryString.Append(glDataID.Value);
            sbQueryString.Append("&");
            sbQueryString.Append(QueryStringConstants.REC_CATEGORY_TYPE_ID);
            sbQueryString.Append("=");
            sbQueryString.Append(recCategoryTypeID.Value);
            sbQueryString.Append("&");
            sbQueryString.Append(QueryStringConstants.MODE);
            sbQueryString.Append("=");
            sbQueryString.Append(mode);
            sbQueryString.Append("&");
            sbQueryString.Append(QueryStringConstants.REC_CATEGORY_ID);
            sbQueryString.Append("=");
            sbQueryString.Append(recCategoryID.Value);
            sbQueryString.Append("&");
            if (recItemInputID.HasValue)
            {
                sbQueryString.Append(QueryStringConstants.GL_RECONCILIATION_ITEM_INPUT_ID);
                sbQueryString.Append("=");
                sbQueryString.Append(recItemInputID.Value);
                sbQueryString.Append("&");
            }

            sbQueryString.Append(QueryStringConstants.IS_FORWARDED_ITEM);

            if (isForwardedItem)
            {
                sbQueryString.Append("=1&");
            }
            else
            {
                sbQueryString.Append("=0&");
            }

            sbQueryString.Append("&parentHiddenField=");
            sbQueryString.Append((string)obj + "&");

            sbQueryString.Append(QueryStringConstants.IS_SRA);
            sbQueryString.Append("=");
            if (isSRA.HasValue && isSRA.Value)
            {
                sbQueryString.Append("1");
            }
            else
            {
                sbQueryString.Append("0");
            }
            sbQueryString.Append("', '580', '850'");
            sbQueryString.Append(");");
            return sbQueryString.ToString();
        }

        public static string GetJavascriptParameterListForBulkClosepopup(long? recItemInputID, string javascriptMethodName, string pageUrl
            , string mode, bool isForwardedItem, long? accountID, long? glDataID, short? recCategoryTypeID, int? netAccountID, bool? isSRA
            , short? recCategoryID, object obj, string currentBCCY)
        {
            StringBuilder sbQueryString = new StringBuilder();

            sbQueryString.Append("javascript:");
            sbQueryString.Append(javascriptMethodName);
            sbQueryString.Append("(");
            sbQueryString.Append("'");
            sbQueryString.Append(pageUrl);
            sbQueryString.Append("?");
            sbQueryString.Append(QueryStringConstants.ACCOUNT_ID);
            sbQueryString.Append("=");
            sbQueryString.Append(accountID);
            sbQueryString.Append("&");
            sbQueryString.Append(QueryStringConstants.NETACCOUNT_ID);
            sbQueryString.Append("=");
            sbQueryString.Append(netAccountID);
            sbQueryString.Append("&");
            sbQueryString.Append(QueryStringConstants.GLDATA_ID);
            sbQueryString.Append("=");
            sbQueryString.Append(glDataID.Value);
            sbQueryString.Append("&");
            sbQueryString.Append(QueryStringConstants.REC_CATEGORY_TYPE_ID);
            sbQueryString.Append("=");
            sbQueryString.Append(recCategoryTypeID.Value);
            sbQueryString.Append("&");
            sbQueryString.Append(QueryStringConstants.MODE);
            sbQueryString.Append("=");
            sbQueryString.Append(mode);
            sbQueryString.Append("&");
            sbQueryString.Append(QueryStringConstants.REC_CATEGORY_ID);
            sbQueryString.Append("=");
            sbQueryString.Append(recCategoryID.Value);
            sbQueryString.Append("&");
            if (recItemInputID.HasValue)
            {
                sbQueryString.Append(QueryStringConstants.GL_RECONCILIATION_ITEM_INPUT_ID);
                sbQueryString.Append("=");
                sbQueryString.Append(recItemInputID.Value);
                sbQueryString.Append("&");
            }

            sbQueryString.Append(QueryStringConstants.IS_FORWARDED_ITEM);

            if (isForwardedItem)
            {
                sbQueryString.Append("=1&");
            }
            else
            {
                sbQueryString.Append("=0&");
            }

            sbQueryString.Append("&parentHiddenField=");
            sbQueryString.Append((string)obj + "&");

            sbQueryString.Append(QueryStringConstants.IS_SRA);
            sbQueryString.Append("=");
            if (isSRA.HasValue && isSRA.Value)
            {
                sbQueryString.Append("1");
            }
            else
            {
                sbQueryString.Append("0");
            }
            sbQueryString.Append("&");
            sbQueryString.Append(QueryStringConstants.CURRENT_BCCY);
            sbQueryString.Append("=");
            sbQueryString.Append(currentBCCY);

            sbQueryString.Append("','BCPopupWindow', '580', '850'");
            sbQueryString.Append(");");
            return sbQueryString.ToString();
        }

        public static string GetJavascriptParameterListForEditRecItem(long? recItemInputID, string javascriptMethodName, string pageUrl, string mode
            , bool isForwardedItem, long? accountID, long? glDataID, short? recCategoryTypeID, int? netAccountID, bool? isSRA
            , short? recCategoryID, object obj, string currentBCCY)
        {
            StringBuilder sbQueryString = new StringBuilder();

            sbQueryString.Append("javascript:");
            sbQueryString.Append(javascriptMethodName);
            sbQueryString.Append("(");
            sbQueryString.Append("'");
            sbQueryString.Append(pageUrl);
            sbQueryString.Append("?");
            sbQueryString.Append(QueryStringConstants.ACCOUNT_ID);
            sbQueryString.Append("=");
            sbQueryString.Append(accountID);
            sbQueryString.Append("&");
            sbQueryString.Append(QueryStringConstants.NETACCOUNT_ID);
            sbQueryString.Append("=");
            sbQueryString.Append(netAccountID);
            sbQueryString.Append("&");
            sbQueryString.Append(QueryStringConstants.GLDATA_ID);
            sbQueryString.Append("=");
            sbQueryString.Append(glDataID.Value);
            sbQueryString.Append("&");
            sbQueryString.Append(QueryStringConstants.REC_CATEGORY_TYPE_ID);
            sbQueryString.Append("=");
            sbQueryString.Append(recCategoryTypeID.Value);
            sbQueryString.Append("&");
            sbQueryString.Append(QueryStringConstants.MODE);
            sbQueryString.Append("=");
            sbQueryString.Append(mode);
            sbQueryString.Append("&");
            sbQueryString.Append(QueryStringConstants.REC_CATEGORY_ID);
            sbQueryString.Append("=");
            sbQueryString.Append(recCategoryID.Value);
            sbQueryString.Append("&");
            if (recItemInputID.HasValue)
            {
                sbQueryString.Append(QueryStringConstants.GL_RECONCILIATION_ITEM_INPUT_ID);
                sbQueryString.Append("=");
                sbQueryString.Append(recItemInputID.Value);
                sbQueryString.Append("&");
            }

            sbQueryString.Append(QueryStringConstants.IS_FORWARDED_ITEM);

            if (isForwardedItem)
            {
                sbQueryString.Append("=1&");
            }
            else
            {
                sbQueryString.Append("=0&");
            }

            sbQueryString.Append("&parentHiddenField=");
            sbQueryString.Append((string)obj + "&");

            sbQueryString.Append(QueryStringConstants.IS_SRA);
            sbQueryString.Append("=");
            if (isSRA.HasValue && isSRA.Value)
            {
                sbQueryString.Append("1");
            }
            else
            {
                sbQueryString.Append("0");
            }

            sbQueryString.Append("&");
            sbQueryString.Append(QueryStringConstants.CURRENT_BCCY);
            sbQueryString.Append("=");
            sbQueryString.Append(currentBCCY);

            sbQueryString.Append("','EditRecItemWindow', '580', '850'");
            sbQueryString.Append(");");
            return sbQueryString.ToString();
        }

        public static string GetJavascriptParameterListForAddRecControlCheckList(int? RecControlCheckListID, string javascriptMethodName, string pageUrl, short mode
         , long? accountID, long? glDataID, int? netAccountID, object obj)
        {
            StringBuilder sbQueryString = new StringBuilder();

            sbQueryString.Append("javascript:");
            sbQueryString.Append(javascriptMethodName);
            sbQueryString.Append("(");
            sbQueryString.Append("'");
            sbQueryString.Append(pageUrl);
            sbQueryString.Append("?");
            sbQueryString.Append(QueryStringConstants.ACCOUNT_ID);
            sbQueryString.Append("=");
            sbQueryString.Append(accountID);
            sbQueryString.Append("&");
            sbQueryString.Append(QueryStringConstants.NETACCOUNT_ID);
            sbQueryString.Append("=");
            sbQueryString.Append(netAccountID);
            sbQueryString.Append("&");
            sbQueryString.Append(QueryStringConstants.GLDATA_ID);
            sbQueryString.Append("=");
            sbQueryString.Append(glDataID.Value);
            sbQueryString.Append("&");
            sbQueryString.Append(QueryStringConstants.MODE);
            sbQueryString.Append("=");
            sbQueryString.Append(mode);
            sbQueryString.Append("&parentHiddenField=");
            sbQueryString.Append((string)obj);
            sbQueryString.Append("','EditRecItemWindow', '580', '850'");
            sbQueryString.Append(");");
            return sbQueryString.ToString();
        }

        #endregion

        public static string GetScriptForClosingRadWindow()
        {
            StringBuilder sbScript = new StringBuilder();
            sbScript.Append("<script Language='javascript'>");
            sbScript.Append(System.Environment.NewLine);
            sbScript.Append("var oWindow = null;");
            sbScript.Append(System.Environment.NewLine);
            sbScript.Append("if (window.radWindow)");
            sbScript.Append(System.Environment.NewLine);
            sbScript.Append("oWindow = window.RadWindow;"); //Will work in Moz in all cases, including clasic dialog       
            sbScript.Append(System.Environment.NewLine);
            sbScript.Append("else if (window.frameElement.radWindow)");
            sbScript.Append(System.Environment.NewLine);
            sbScript.Append("oWindow = window.frameElement.radWindow;");
            sbScript.Append(System.Environment.NewLine);
            sbScript.Append("oWindow.Close();");
            sbScript.Append(System.Environment.NewLine);
            sbScript.Append("</script>");

            return sbScript.ToString();
        }

        internal static void RedirectToErrorPage(int? LabelID, bool bIsSystemError)
        {
            string url = PopupHelper.GetErrorPageUrl();

            if (LabelID != null)
            {
                url += "?" + QueryStringConstants.ERROR_MESSAGE_LABEL_ID + "=" + LabelID.ToString();
            }

            if (bIsSystemError)
            {
                url += "&" + QueryStringConstants.ERROR_MESSAGE_SYSTEM + "=1";
            }
            HttpContext.Current.Response.Redirect(url);
        }

        public static string GetErrorPageUrl()
        {
            return "~/Pages/ErrorHandlerPopup.aspx";
        }

        #region PopupTitle

        public static string GetPageTitle(short? CategoryMstID, short? CategoryType)
        {
            return GetPopupPageTitle(Convert.ToInt16(CategoryMstID), Convert.ToInt16(CategoryType), null);
        }

        public static void SetPageTitle(PopupPageBase oPageBase, short CategoryMstID, short CategoryType, params int[] oLableIDCollection)
        {
            oPageBase.Title = GetPopupPageTitle(CategoryMstID, CategoryType, oLableIDCollection);
        }

        private static string GetPopupPageTitle(short CategoryMstID, short CategoryType, params int[] oLableIDCollection)
        {
            string title = string.Empty;

            int CategoryMstLabelID = SetPageTitleWithCategoryMst(CategoryMstID);
            int CategoryTypeLabelID = SetPageTitleWithCategoryType(CategoryType);

            if (CategoryMstLabelID != 0)
                title += LanguageUtil.GetValue(CategoryMstLabelID);
            if (title != string.Empty)
                title += WebConstants.ACCOUNT_ENTITY_SEPARATOR;
            if (CategoryTypeLabelID != 0)
                title += LanguageUtil.GetValue(CategoryTypeLabelID);

            //title = LanguageUtil.GetValue(CategoryMstLabelID) + WebConstants.ACCOUNT_ENTITY_SEPARATOR + LanguageUtil.GetValue(CategoryTypeLabelID);
            bool isEmptyTitle = true;

            if (title != string.Empty)
                isEmptyTitle = false;
            else
                isEmptyTitle = true;

            title += GetTitleFromPhraseCollection(isEmptyTitle, oLableIDCollection);


            return title;
        }

        private static string GetTitleFromPhraseCollection(bool isEmptyTitle, params int[] oLableIDCollection)
        {
            string title = string.Empty;

            if (oLableIDCollection != null && oLableIDCollection.Length > 0)
            {
                int phraseLength = oLableIDCollection.Length - 1;

                for (int counter = 0; counter < oLableIDCollection.Length; counter++)
                {
                    if (isEmptyTitle == false && counter == 0)
                    {
                        title += WebConstants.ACCOUNT_ENTITY_SEPARATOR;
                    }
                    title += LanguageUtil.GetValue(oLableIDCollection[counter]);

                    if (counter != phraseLength)
                    {
                        title += WebConstants.ACCOUNT_ENTITY_SEPARATOR;
                    }
                }
            }

            return title;
        }


        private static int SetPageTitleWithCategoryMst(short CategoryID)
        {
            int labelId = 0;
            switch (CategoryID)
            {
                case (short)WebEnums.RecCategory.GLAdjustments:
                    labelId = 1080;

                    break;
                case (short)WebEnums.RecCategory.ReviewNotes:
                    labelId = 1394;

                    break;
                case (short)WebEnums.RecCategory.SupportingDetail:
                    labelId = 1084;

                    break;
                case (short)WebEnums.RecCategory.TimingDifference:

                    labelId = 1081;

                    break;
                case (short)WebEnums.RecCategory.VariancesWriteOffOn:
                    labelId = 1083;

                    break;

            }
            return labelId;
        }

        protected static int SetPageTitleWithCategoryType(short CategoryType)
        {
            int labelId = 0;
            WebEnums.RecCategoryType oRecCategoryType = (WebEnums.RecCategoryType)Enum.Parse(typeof(WebEnums.RecCategoryType), CategoryType.ToString());
            switch (CategoryType)
            {
                case (short)WebEnums.RecCategoryType.Accrual_GLAdjustments_Total:
                    labelId = 1656;

                    break;
                case (short)WebEnums.RecCategoryType.Accrual_RecWriteoffOn_UnexpVar:
                    labelId = 1678;

                    break;
                case (short)WebEnums.RecCategoryType.Accrual_RecWriteoffOn_WriteoffOn:
                    labelId = 1389;

                    break;
                case (short)WebEnums.RecCategoryType.Accrual_ReviewNotes:

                    labelId = 1394;

                    break;
                case (short)WebEnums.RecCategoryType.Accrual_SupportingDetail_IndividualAccrualDetail:
                    labelId = 1445;

                    break;
                case (short)WebEnums.RecCategoryType.Accrual_SupportingDetail_RecurringAccrualSchedule:
                    labelId = 1446;

                    break;
                case (short)WebEnums.RecCategoryType.Accrual_TimingDifference_Total:
                    labelId = 1656;

                    break;
                case (short)WebEnums.RecCategoryType.Amortizable_GLAdjustments_Total:
                    labelId = 1656;

                    break;
                case (short)WebEnums.RecCategoryType.Amortizable_RecWriteoffOn_UnexpVar:
                    labelId = 1678;

                    break;
                case (short)WebEnums.RecCategoryType.Amortizable_RecWriteoffOn_WriteoffOn:
                    labelId = 1389;

                    break;
                case (short)WebEnums.RecCategoryType.Amortizable_ReviewNotes:
                    labelId = 1394;

                    break;
                case (short)WebEnums.RecCategoryType.Amortizable_SupportingDetail_RecurringAmortizableSchedule:
                    //labelId = 1084;
                    labelId = 2252;
                    break;
                case (short)WebEnums.RecCategoryType.Amortizable_SupportingDetail_IndividualAmortizableDetail:
                    //labelId = 1084;
                    labelId = 2251;
                    break;
                case (short)WebEnums.RecCategoryType.Amortizable_TimingDifference_Total:
                    labelId = 1656;

                    break;
                case (short)WebEnums.RecCategoryType.BankAccount_GLAdjustments_BankFees:
                    labelId = 1692;

                    break;
                case (short)WebEnums.RecCategoryType.BankAccount_GLAdjustments_NSFFees:
                    labelId = 1693;

                    break;
                case (short)WebEnums.RecCategoryType.BankAccount_GLAdjustments_Other:
                    labelId = 1694;

                    break;
                case (short)WebEnums.RecCategoryType.BankAccount_RecWriteoffOn_UnexpVar:
                    labelId = 1678;

                    break;
                case (short)WebEnums.RecCategoryType.BankAccount_RecWriteoffOn_WriteoffOn:
                    labelId = 1389;

                    break;
                case (short)WebEnums.RecCategoryType.BankAccount_ReviewNotes:
                    labelId = 1394;

                    break;
                case (short)WebEnums.RecCategoryType.BankAccount_TimingDifference_DepositsInTransit:
                    labelId = 1695;

                    break;
                case (short)WebEnums.RecCategoryType.BankAccount_TimingDifference_Other:
                    labelId = 1694;
                    break;
                case (short)WebEnums.RecCategoryType.BankAccount_TimingDifference_OutstandingChecks:
                    labelId = 1696;

                    break;
                case (short)WebEnums.RecCategoryType.DerivedCalculation_GLAdjustments_Total:
                    labelId = 1656;

                    break;
                case (short)WebEnums.RecCategoryType.DerivedCalculation_RecWriteoffOn_UnexpVar:
                    labelId = 1678;

                    break;
                case (short)WebEnums.RecCategoryType.DerivedCalculation_RecWriteoffOn_WriteoffOn:
                    labelId = 1389;

                    break;
                case (short)WebEnums.RecCategoryType.DerivedCalculation_ReviewNotes:
                    labelId = 1394;

                    break;
                case (short)WebEnums.RecCategoryType.DerivedCalculation_SupportingDetail_OtherDetails:
                    labelId = 1691;

                    break;
                case (short)WebEnums.RecCategoryType.DerivedCalculation_TimingDifference_Total:
                    labelId = 1656;

                    break;
                case (short)WebEnums.RecCategoryType.ItemizedList_GLAdjustments_Total:
                    labelId = 1656;

                    break;
                case (short)WebEnums.RecCategoryType.ItemizedList_RecWriteoffOn_UnexpVar:
                    labelId = 1678;

                    break;
                case (short)WebEnums.RecCategoryType.ItemizedList_RecWriteoffOn_WriteoffOn:
                    labelId = 1389;

                    break;
                case (short)WebEnums.RecCategoryType.ItemizedList_ReviewNotes:

                    return labelId;

                case (short)WebEnums.RecCategoryType.ItemizedList_SupportingDetail_SupportingDetailList:
                    labelId = 1656;

                    break;
                case (short)WebEnums.RecCategoryType.ItemizedList_TimingDifference_Total:
                    labelId = 1656;

                    break;
                case (short)WebEnums.RecCategoryType.Subledger_GLAdjustments_Total:
                    labelId = 1656;

                    break;
                case (short)WebEnums.RecCategoryType.Subledger_SupportingDetail_OtherDetails:
                    labelId = 1694;

                    break;
                case (short)WebEnums.RecCategoryType.Subledger_RecWriteoffOn_UnexpVar:
                    labelId = 1678;

                    break;
                case (short)WebEnums.RecCategoryType.Subledger_RecWriteoffOn_WriteoffOn:
                    labelId = 1389;

                    break;
                case (short)WebEnums.RecCategoryType.Subledger_ReviewNotes:
                    labelId = 1394;

                    break;
                case (short)WebEnums.RecCategoryType.Subledger_TimingDifference_Total:
                    labelId = 1656;

                    break;

            }
            return labelId;
        }

        #endregion



        public static void HideErrorMessage(PopupPageBase oPopupPageBase)
        {
            PopupMasterPageBase oMasterPageBase = (PopupMasterPageBase)oPopupPageBase.Master;
            oMasterPageBase.HideErrorMessage();

        }
        public static string GetJavascriptParameterListForAddEditTask(string functionname, long? TaskID, string javascriptMethodName, string pageUrl, string mode, long? taskDetalID, short TaskTypeID, object obj)
        {
            StringBuilder sbQueryString = new StringBuilder();
            if (!string.IsNullOrEmpty(functionname))
            {
                sbQueryString.Append("function ");
                sbQueryString.Append(functionname);
                sbQueryString.Append("(){");
                sbQueryString.Append("javascript:");
                sbQueryString.Append(javascriptMethodName);
                sbQueryString.Append("(");
                sbQueryString.Append("'");
            }
            sbQueryString.Append(pageUrl);
            sbQueryString.Append("?");
            sbQueryString.Append(QueryStringConstants.TASK_ID);
            sbQueryString.Append("=");
            sbQueryString.Append(TaskID);
            sbQueryString.Append("&");
            sbQueryString.Append(QueryStringConstants.TASK_DETAIL_ID);
            sbQueryString.Append("=");
            sbQueryString.Append(taskDetalID);
            sbQueryString.Append("&");
            sbQueryString.Append(QueryStringConstants.TASK_TYPE_ID);
            sbQueryString.Append("=");
            sbQueryString.Append(TaskTypeID);
            sbQueryString.Append("&");
            sbQueryString.Append(QueryStringConstants.MODE);
            sbQueryString.Append("=");
            sbQueryString.Append(mode);
            sbQueryString.Append("&parentHiddenField=");
            sbQueryString.Append((string)obj);
            if (!string.IsNullOrEmpty(functionname))
            {
                sbQueryString.Append("','EditAddTaskWindow', '1024', '1050'");
                sbQueryString.Append(")};");
            }
            return sbQueryString.ToString();
        }

        public static string GetJavascriptParameterListForBulkStatusUpdate(string functionname, string javascriptMethodName, string pageUrl, string windowName, ARTEnums.TaskActionType taskActionType, string mode,
            ARTEnums.TaskType taskType, ARTEnums.RecordType recordType, long? accountID, int? netAccountID)
        {
            StringBuilder sbQueryString = new StringBuilder();
            if (!string.IsNullOrEmpty(functionname))
            {
                sbQueryString.Append("function ");
                sbQueryString.Append(functionname);
                sbQueryString.Append("(){");
                sbQueryString.Append("javascript:");
                sbQueryString.Append(javascriptMethodName);
                sbQueryString.Append("(");
                sbQueryString.Append("'");
            }
            sbQueryString.Append(pageUrl);
            sbQueryString.Append("?");
            sbQueryString.Append(QueryStringConstants.TASK_ACTION_TYPE_ID);
            sbQueryString.Append("=");
            sbQueryString.Append(taskActionType.ToString("D"));
            //sbQueryString.Append("&");
            //sbQueryString.Append(QueryStringConstants.TASK_DETAIL_ID);
            //sbQueryString.Append("=");
            //sbQueryString.Append(taskDetailID);
            sbQueryString.Append("&");
            sbQueryString.Append(QueryStringConstants.TASK_TYPE_ID);
            sbQueryString.Append("=");
            sbQueryString.Append(taskType.ToString("D"));
            sbQueryString.Append("&");
            sbQueryString.Append(QueryStringConstants.MODE);
            sbQueryString.Append("=");
            sbQueryString.Append(mode);
            sbQueryString.Append("&");
            sbQueryString.Append(QueryStringConstants.RECORD_TYPE_ID);
            sbQueryString.Append("=");
            sbQueryString.Append(recordType.ToString("D"));
            if (accountID.GetValueOrDefault() > 0)
            {
                sbQueryString.Append("&");
                sbQueryString.Append(QueryStringConstants.ACCOUNT_ID);
                sbQueryString.Append("=");
                sbQueryString.Append(accountID.Value.ToString("D"));
            }
            if (netAccountID.GetValueOrDefault() > 0)
            {
                sbQueryString.Append("&");
                sbQueryString.Append(QueryStringConstants.NETACCOUNT_ID);
                sbQueryString.Append("=");
                sbQueryString.Append(netAccountID.Value.ToString("D"));
            }
            if (!string.IsNullOrEmpty(functionname))
            {
                sbQueryString.AppendFormat("','{0}', '580', '1050'", windowName);
                sbQueryString.Append(")};");

            }
            return sbQueryString.ToString();
        }
        public static string getRecItemCommentPopupUrl(GLDataHdrInfo GLDataHdrInfo, long RecordID, short RecordTypeID, long? AccountID, int? NetAccountID)
        {

            string _PopupUrl = string.Empty;
            _PopupUrl = "EditRecItemComment.aspx?" + QueryStringConstants.GLDATA_ID + "=" + GLDataHdrInfo.GLDataID
                           + "&" + QueryStringConstants.RECORD_ID + "=" + RecordID
                           + "&" + QueryStringConstants.RECORD_TYPE_ID + "=" + RecordTypeID
                           + "&" + QueryStringConstants.REC_STATUS_ID + "=" + GLDataHdrInfo.ReconciliationStatusID
                           + "&" + QueryStringConstants.ACCOUNT_ID + "=" + AccountID.GetValueOrDefault()
                           + "&" + QueryStringConstants.NETACCOUNT_ID + "=" + NetAccountID.GetValueOrDefault();

            return _PopupUrl;
        }
        public static string getGlDataRecControlCheckListCommentPopupUrl(long? GLDataID,short FormMode, long? GLDataRecControlCheckListID, int? RecControlCheckListID)
        {
            string _PopupUrl = string.Empty;
            _PopupUrl = "EditRecControlCheckListComment.aspx?" + QueryStringConstants.REC_CONTROL_CHECK_LIST_ID + "=" + RecControlCheckListID
                           + "&" + QueryStringConstants.GLDATA_REC_CONTROL_CHECKLIST_ID + "=" + GLDataRecControlCheckListID
                           + "&" + QueryStringConstants.MODE + "=" + FormMode
                            + "&" + QueryStringConstants.GLDATA_ID + "=" + GLDataID.GetValueOrDefault();

            return _PopupUrl;
        }
    }
}