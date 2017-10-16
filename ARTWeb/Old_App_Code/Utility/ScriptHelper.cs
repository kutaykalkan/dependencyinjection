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
using System.Text;
using SkyStem.ART.Web.Data;
using SkyStem.Language.LanguageUtility;
using SkyStem.Library.Controls.WebControls;


namespace SkyStem.ART.Web.Utility
{

    /// <summary>
    /// Summary description for ScriptHelper
    /// </summary>
    public class ScriptHelper
    {
        public ScriptHelper()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        /// <summary>
        /// Function to Add <script> tag
        /// </summary>
        /// <param name="script"></param>
        public static void AddJSStartTag(StringBuilder script)
        {
            script.Append(System.Environment.NewLine);
            script.Append("<script language='JavaScript' type='text/javascript'>");
            script.Append(System.Environment.NewLine);
        }

        /// <summary>
        /// Function to Add </script> tag
        /// </summary>
        /// <param name="script"></param>
        public static void AddJSEndTag(StringBuilder script)
        {
            script.Append(System.Environment.NewLine);
            script.Append("</script>");
            script.Append(System.Environment.NewLine);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="oPage"></param>
        public static void ClosePopupAndRefreshParentPage(Page oPage)
        {
            // Render JS to Close the Current Window, 
            // and load the Login page on Main Window
            StringBuilder script = new StringBuilder();
            ScriptHelper.AddJSStartTag(script);
            script.Append(System.Environment.NewLine);
            script.Append("ClosePopupAndRefreshParentPage();");
            script.Append(System.Environment.NewLine);
            ScriptHelper.AddJSEndTag(script);
            oPage.ClientScript.RegisterClientScriptBlock(oPage.GetType(), "ClosePopupAndRefreshParentPage", script.ToString());
        }

        //public static string GetJSForPopupSessionTimeout()
        //{
        //    // Render JS to Close the Current Window, 
        //    // and load the Login page on Main Window
        //    StringBuilder script = new StringBuilder();
        //    ScriptHelper.AddJSStartTag(script);
        //    ScriptHelper.GetJSForGetRadWindow(script);
        //    script.Append("function ClosePopupAndRefreshParentPage()");
        //    script.Append(System.Environment.NewLine);
        //    script.Append("{");
        //    //script.Append(System.Environment.NewLine);
        //    //script.Append("alert('tt');");
        //    script.Append(System.Environment.NewLine);
        //    script.Append("var oWindow = GetRadWindow();");
        //    script.Append(System.Environment.NewLine);
        //    script.Append("oWindow.Close();");
        //    script.Append(System.Environment.NewLine);
        //    script.Append("oWindow.BrowserWindow.location.reload();");
        //    script.Append(System.Environment.NewLine);
        //    script.Append("}");
        //    script.Append(System.Environment.NewLine);
        //    script.Append("ClosePopupAndRefreshParentPage();");
        //    ScriptHelper.AddJSEndTag(script);
        //    return script.ToString();
        //}

        public static string GetJSForClosePopupAndSubmitParentPage()
        {
            // Render JS to Close the Current Window, 
            // and load the Login page on Main Window
            StringBuilder script = new StringBuilder();
            ScriptHelper.AddJSStartTag(script);
            GetJSForGetRadWindow(script);
            script.Append("function ClosePopupAndSubmitParentPage()");
            script.Append(System.Environment.NewLine);
            script.Append("{");
            //script.Append(System.Environment.NewLine);
            //script.Append("alert('tt');");
            script.Append(System.Environment.NewLine);
            script.Append("var oWindow = GetRadWindow();");
            script.Append(System.Environment.NewLine);
            //script.Append("oWindow.BrowserWindow.SetIsPostBackFromFilterScreen();");
            //script.Append(System.Environment.NewLine);
            script.Append("oWindow.Close();");
            script.Append(System.Environment.NewLine);
            //script.Append("oWindow.BrowserWindow.document.forms[0].submit();");
            script.Append("oWindow.BrowserWindow.__doPostBack('ctl00$ddlReconciliationPeriod', '');");
            script.Append(System.Environment.NewLine);
            script.Append("}");
            script.Append(System.Environment.NewLine);
            script.Append("ClosePopupAndSubmitParentPage();");
            ScriptHelper.AddJSEndTag(script);
            return script.ToString();
        }

        /// <summary>
        /// Gets the JS for refresh parent page on window close.
        /// </summary>
        /// <param name="bRefresh">if set to <c>true</c> [b refresh].</param>
        /// <returns></returns>
        public static string GetJSForRefreshParentPageOnWindowClose(bool bRefresh)
        {
            StringBuilder script = new StringBuilder();
            ScriptHelper.AddJSStartTag(script);
            GetJSForGetRadWindow(script);
            script.Append("function SubmitParentPageOnWindowClose()");
            script.Append(System.Environment.NewLine);
            script.Append("{");
            script.Append(System.Environment.NewLine);
            script.Append("var oWindow = GetRadWindow();");
            script.Append(System.Environment.NewLine);
            script.Append("var oArg = new Object();");
            script.Append(System.Environment.NewLine);
            if (bRefresh)
                script.Append("oArg.IsRefresh = 1");
            else
                script.Append("oArg.IsRefresh = 0");
            script.Append(System.Environment.NewLine);
            script.Append("oWindow.Argument = oArg;");
            script.Append(System.Environment.NewLine);
            script.Append("}");
            script.Append(System.Environment.NewLine);
            script.Append("SubmitParentPageOnWindowClose();");
            ScriptHelper.AddJSEndTag(script);
            return script.ToString();
        }

        public static string CallApplyFilterAndClosePopup(bool bRefresh)
        {          
            StringBuilder script = new StringBuilder();
            ScriptHelper.AddJSStartTag(script);
            GetJSForGetRadWindow(script);
            script.Append("function CallApplyFilterAndClose()");
            script.Append(System.Environment.NewLine);
            script.Append("{");           
            script.Append(System.Environment.NewLine);
            script.Append("var wnd1 = GetRadWindow();");
            script.Append(System.Environment.NewLine);
            script.Append(" var mgr = wnd1.get_windowManager();");
            script.Append(System.Environment.NewLine);
            script.Append(" var wnd2 = mgr.getWindowByName('BCPopupWindow');");
            script.Append(System.Environment.NewLine);
            script.Append("  wnd2.get_contentFrame().contentWindow.CallParentApplyFilterFunction();");
            script.Append(System.Environment.NewLine);
            script.Append(" wnd1.close();");
            script.Append(System.Environment.NewLine);
            script.Append(" } ");
            script.Append(System.Environment.NewLine);
            script.Append("CallApplyFilterAndClose();");
            ScriptHelper.AddJSEndTag(script);
            return script.ToString();
        }

        public static string GetJSForClosePopupAndSubmitParentPage(bool bRefresh)
        {
            // Render JS to Close the Current Window, 
            // and load the Login page on Main Window
            StringBuilder script = new StringBuilder();
            ScriptHelper.AddJSStartTag(script);
            GetJSForGetRadWindow(script);
            script.Append("function ClosePopupAndSubmitParentPage()");
            script.Append(System.Environment.NewLine);
            script.Append("{");
            //script.Append(System.Environment.NewLine);
            //script.Append("alert('tt');");
            script.Append(System.Environment.NewLine);
            script.Append("var oWindow = GetRadWindow();");
            script.Append(System.Environment.NewLine);
            //script.Append("oWindow.BrowserWindow.SetIsPostBackFromFilterScreen();");
            //script.Append(System.Environment.NewLine);
            script.Append("var oArg = new Object();");
            script.Append(System.Environment.NewLine);
            if (bRefresh)
                script.Append("oArg.IsRefresh = 1");
            else
                script.Append("oArg.IsRefresh = 0");
            script.Append(System.Environment.NewLine);
            script.Append("oWindow.Close(oArg);");
            script.Append(System.Environment.NewLine);
            //script.Append("oWindow.BrowserWindow.document.forms[0].submit();");
            //script.Append("oWindow.BrowserWindow.__doPostBack('ctl00$ddlReconciliationPeriod', '');");
            //script.Append(System.Environment.NewLine);
            script.Append("}");
            script.Append(System.Environment.NewLine);
            script.Append("ClosePopupAndSubmitParentPage();");
            ScriptHelper.AddJSEndTag(script);
            return script.ToString();
        }

        private static void GetJSForGetRadWindow(StringBuilder script)
        {
            script.Append("function GetRadWindow()");
            script.Append(System.Environment.NewLine);
            script.Append("{");
            script.Append("var oWindow = null;");
            script.Append(System.Environment.NewLine);
            script.Append("if (window.radWindow) oWindow = window.radWindow;//Will work in Moz in all cases, including clasic dialog");
            script.Append(System.Environment.NewLine);
            script.Append("else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow;//IE (and Moz az well)");
            script.Append(System.Environment.NewLine);
            script.Append("return oWindow;");
            script.Append(System.Environment.NewLine);
            script.Append("} ");
        }

        public static string GetJSForPopupClose()
        {
            // Render JS to Close the Current Window, 
            // and load the Login page on Main Window
            StringBuilder script = new StringBuilder();
            ScriptHelper.AddJSStartTag(script);
            ScriptHelper.GetJSForGetRadWindow(script);
            script.Append("function ClosePopupWithoutRefreshParentPage()");
            script.Append(System.Environment.NewLine);
            script.Append("{");
            //script.Append(System.Environment.NewLine);
            //script.Append("alert('tt');");
            script.Append(System.Environment.NewLine);
            script.Append("var oWindow = GetRadWindow();");
            script.Append(System.Environment.NewLine);
            script.Append("oWindow.Close();");
            script.Append(System.Environment.NewLine);
            script.Append("return false;");
            script.Append(System.Environment.NewLine); 
            script.Append("}");
            script.Append(System.Environment.NewLine);
            script.Append(" ClosePopupWithoutRefreshParentPage();");
            ScriptHelper.AddJSEndTag(script);
            return script.ToString();
        }

        public static string GetJSToSetParentWindowElementValue(string ParentWindowElementClientId,string value)
        {
            StringBuilder script = new StringBuilder();
            ScriptHelper.AddJSStartTag(script);
            script.Append("var oWindow = null;");
            script.Append(System.Environment.NewLine);
            script.Append("if (window.radWindow)");
            script.Append(System.Environment.NewLine);
            script.Append("oWindow = window.RadWindow;"); //Will work in Moz in all cases, including clasic dialog       
            script.Append(System.Environment.NewLine);
            script.Append("else if (window.frameElement.radWindow)");
            script.Append(System.Environment.NewLine);
            script.Append("oWindow = window.frameElement.radWindow;");
            script.Append(System.Environment.NewLine);
            script.Append("oWindow.BrowserWindow.SetElementValue('" + ParentWindowElementClientId + "','" + value + "');");
            ScriptHelper.AddJSEndTag(script);
            return script.ToString();
        }
        
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="oPage"></param>
        public static void RenderCultureSpecificJS(Page oPage)
        {
            // Render JS for NumberDecimalSeparator
            StringBuilder script = new StringBuilder();
            ScriptHelper.AddJSStartTag(script);

            script.Append("var NumberDecimalSeparator = '");
            script.Append(System.Globalization.CultureInfo.CurrentUICulture.NumberFormat.NumberDecimalSeparator);
            script.Append("';");
            script.Append(System.Environment.NewLine);

            script.Append("var NumberFormat = '#");
            script.Append(System.Globalization.CultureInfo.CurrentUICulture.NumberFormat.CurrencyGroupSeparator);
            script.Append("###");
            script.Append(System.Globalization.CultureInfo.CurrentUICulture.NumberFormat.NumberDecimalSeparator);
            script.Append("00';");
            script.Append(System.Environment.NewLine);

            script.Append("numberProperty = [\"\",\"");
            script.Append(System.Globalization.CultureInfo.CurrentUICulture.NumberFormat.NumberDecimalSeparator);
            script.Append(System.Globalization.CultureInfo.CurrentUICulture.NumberFormat.NumberDecimalSeparator);
            script.Append("\",\"");
            script.Append(System.Globalization.CultureInfo.CurrentUICulture.NumberFormat.NumberGroupSeparator);
            script.Append("\",\"");
            script.Append(System.Globalization.CultureInfo.CurrentUICulture.NumberFormat.NegativeSign);
            script.Append("\",\"\",\"\",\"");
            script.Append("n");
            script.Append("\",\"");
            script.Append("-n");
            script.Append("\",3");
            script.Append(",2,2,1,");
            // ,Decimal Places,Min Decimal Places
            script.Append("0");
            // Min Value
            script.Append(",1,");
            script.Append(long.MaxValue);
            // Max Value
            script.Append(",\"\",\"\",");
            try
            {
                int counter = 0;
                for (counter = 0; (counter
                            <= (System.Globalization.CultureInfo.CurrentUICulture.NumberFormat.NumberGroupSizes.Length - 1)); counter++)
                {
                    script.Append(System.Globalization.CultureInfo.CurrentUICulture.NumberFormat.NumberGroupSizes[counter]);
                    script.Append(",");
                }
            }
            catch 
            {
            }
            //if ((script.ToString().Substring((script.ToString().Length - 1), 1) == ","))
            if (script.ToString().EndsWith(","))
            {
                script.Remove((script.Length - 1), 1);
            }
            script.Append("];");

            // Date Reg Ex
            script.Append("var DateValidateRegExpStr = '");
            script.Append(LanguageUtil.GetValue(3).Replace("\\", "\\\\" ));
            script.Append("';");
            script.Append(System.Environment.NewLine);

            // Date Reg Ex
            script.Append("var DATE_FORMAT = '");
            //script.Append(LanguageUtil.GetValue(2));
            script.Append(System.Globalization.CultureInfo.CurrentUICulture.DateTimeFormat.ShortDatePattern);
            script.Append("';");
            script.Append(System.Environment.NewLine);

            ScriptHelper.AddJSEndTag(script);

            if (!oPage.ClientScript.IsStartupScriptRegistered(oPage.GetType(), WebConstants.CULTURE_SPECIFIC_JS))
            {
                oPage.ClientScript.RegisterStartupScript(oPage.GetType(), WebConstants.CULTURE_SPECIFIC_JS, script.ToString(), false);
            }
        }



        public static string GetJSForClosePopupAndPostbackParentPage(string postBackControlID)
        {
            // Render JS to Close the Current Window, 
            // and postback the parent page
            StringBuilder script = new StringBuilder();
            ScriptHelper.AddJSStartTag(script);
            GetJSForGetRadWindow(script);
            script.Append("function ClosePopupAndPostBackParentPage()");
            script.Append(System.Environment.NewLine);
            script.Append("{");
            //script.Append(System.Environment.NewLine);
            //script.Append("alert('tt');");
            script.Append(System.Environment.NewLine);
            script.Append("var oWindow = GetRadWindow();");
            script.Append(System.Environment.NewLine);
            //script.Append("oWindow.BrowserWindow.SetIsPostBackFromFilterScreen();");
            //script.Append(System.Environment.NewLine);
            script.Append("oWindow.Close();");
            script.Append(System.Environment.NewLine);
            //script.Append("oWindow.BrowserWindow.document.forms[0].submit();");
            script.Append("oWindow.BrowserWindow.__doPostBack('" + postBackControlID  + "', '');");
            script.Append(System.Environment.NewLine);
            script.Append("}");
            script.Append(System.Environment.NewLine);
            script.Append("ClosePopupAndPostBackParentPage();");
            ScriptHelper.AddJSEndTag(script);
            return script.ToString();
        }

        public static string GetJSForParentPageCallbackFunction(string jsCallbackFunctionName)
        {
            // Render JS to Callback a function on Parent page
            StringBuilder script = new StringBuilder();
            ScriptHelper.AddJSStartTag(script);
            GetJSForGetRadWindow(script);
            script.Append("function CallbackParentPage()");
            script.Append(System.Environment.NewLine);
            script.Append("{");
            script.Append(System.Environment.NewLine);
            script.Append("var oWindow = GetRadWindow();");
            script.Append(System.Environment.NewLine);
            script.Append("eval('oWindow.BrowserWindow." + jsCallbackFunctionName + "()');");
            script.Append(System.Environment.NewLine);
            script.Append("}");
            script.Append(System.Environment.NewLine);
            script.Append("CallbackParentPage();");
            ScriptHelper.AddJSEndTag(script);
            return script.ToString();
        }

        internal static void RenderGlobalConstantsInJS(Page oPage)
        {
            // Render JS for NumberDecimalSeparator
            StringBuilder script = new StringBuilder();
            ScriptHelper.AddJSStartTag(script);

            script.Append("var JS_GLOBAL_CONSTANT_HYPHEN = '");
            script.Append(WebConstants.HYPHEN);
            script.Append("';");
            script.Append(System.Environment.NewLine);

            script.Append("var JS_GLOBAL_CONSTANT_SELECT_ONE = '");
            script.Append(WebConstants.SELECT_ONE);
            script.Append("';");
            script.Append(System.Environment.NewLine);

            ScriptHelper.AddJSEndTag(script);

            if (!oPage.ClientScript.IsStartupScriptRegistered(oPage.GetType(), "CommonConstantsInJS"))
            {
                oPage.ClientScript.RegisterStartupScript(oPage.GetType(), "CommonConstantsInJS", script.ToString(), false);
            }
        }

        public static string GetJSForEmptyURL()
        {
            return "javascript:";
        }

        public static void AddJSForConfirmDelete(ExButton btnDelete)
        {
            //btnDelete.OnClientClick = "javascript:return ShowConfirmationMessageOnDelete('" + LanguageUtil.GetValue(1781) + "');";
            AddJSForConfirmDelete(btnDelete, 1781);
        }

        public static void AddJSForConfirmDelete(ExButton btnDelete, int LabelID)
        {
            btnDelete.OnClientClick = "javascript:return ShowConfirmationMessageOnDelete('" + LanguageUtil.GetValue(LabelID) + "');";
        }

        public static string GetJSForDisplayErrorMessage(string errMsg)
        {
            StringBuilder script = new StringBuilder();
            AddJSStartTag(script);
            script.Append(System.Environment.NewLine);
            if (errMsg.IndexOf("Thread was being aborted") == -1)
            {
                script.Append("alert('");
                script.Append(errMsg);
                script.Append("');");
            }
            script.Append(System.Environment.NewLine);
            AddJSEndTag(script);
            return script.ToString();
        }
    }
}