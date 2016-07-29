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
using SkyStem.ART.Web.Utility;
using SkyStem.ART.Web.Data;
using System.Globalization;
using System.Collections.Generic;
using System.Text;
using SkyStem.Language.LanguageUtility;
using SkyStem.Library.Controls.TelerikWebControls;
using System.ComponentModel;
using SkyStem.ART.Client.Model;
using System.Web.Services;

namespace SkyStem.ART.Web.Classes
{
    /// <summary>
    /// Summary description for PageBase
    /// </summary>
    public abstract class PageBase : Page
    {
        public abstract string GetMenuKey();

        protected WebEnums.ARTPages? ARTPageID { get; set; }
        /// <summary>
        /// Page Title Label ID
        /// </summary>
        public int PageTitleLabelID { get; set; }

        public PageBase()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        /// <summary>
        /// This function checks for 
        ///     - Valid Session
        ///     - Referrer
        ///     - Check for Page Accessibility
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);

            // Check for Http Referrer
            Helper.CheckReferrer();

            // Check for Session
            SessionHelper.CheckSessionForUser();

            // Get User Default Language
            UserHdrInfo oUserHdrInfo = SessionHelper.GetCurrentUser();
            if (oUserHdrInfo != null)
            {
                int lcid = oUserHdrInfo.DefaultLanguageID.GetValueOrDefault();
                // Code to Set Language for the Current Thread
                if (!SessionHelper.IsUserLanguageExists() || lcid != SessionHelper.GetUserLanguage())
                {
                    // Clear the translated master data from session
                    SessionHelper.ClearMasterDataFromSession();

                    // Get the User/Browser Language and store in Session
                    if (lcid == 0)
                        lcid = System.Threading.Thread.CurrentThread.CurrentCulture.LCID;
                    SessionHelper.SetUserLanguage(lcid);
                    // Check for Test LCID, if set
                    LanguageHelper.SetTestLanguage();

                    // Set Current Culture and Load Phrases
                    LanguageHelper.SetCurrentCultureAndLoadPhrases();
                }
                // Set the Language (Culture) for the Current Thread
                LanguageHelper.SetCurrentCulture();

                // Render Culture Specifc JS
                ScriptHelper.RenderCultureSpecificJS(this);

                // Render JS for Global Constants
                ScriptHelper.RenderGlobalConstantsInJS(this);

                // Is Reset Password Required
                if ((!this.ARTPageID.HasValue || this.ARTPageID.Value != WebEnums.ARTPages.ChangePassword)
                    && oUserHdrInfo.IsPasswordResetRequired.GetValueOrDefault())
                    Helper.RedirectToChangePasswordPage();
                // TODO: Check for Page Accessibility
            }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            MasterPageBase oMasterPageBase;
            if (this.Master is MasterPageBase)
            {
                oMasterPageBase = (MasterPageBase)this.Master;
            }
            else
            {
                oMasterPageBase = (MasterPageBase)this.Master.Master;
            }

            if (oMasterPageBase != null)
                oMasterPageBase.RefreshRequested += new RefreshEventHandler(oMasterPageBase_RefreshRequested);
            if (!this.IsPostBack)
                LoadPageSettings();
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            SavePageSettings();
        }

        void oMasterPageBase_RefreshRequested(object sender, RefreshEventArgs args)
        {
            RefreshPage(sender, args);
        }

        public virtual void RefreshPage(object sender, RefreshEventArgs args)
        {
        }
        # region PageSettings

        public PageSettings PageSettings { get; set; }
        public WebEnums.ARTPages PageID { get; set; }
        public void LoadPageSettings()
        {
            PageSettings = PageSettingHelper.GetPageSettings(PageID);
            //if (HttpContext.Current.Session[SessionHelper.GetSessionKeyForPageSetting((short)PageID)] != null)
            //    PageSettings = (PageSettings)HttpContext.Current.Session[SessionHelper.GetSessionKeyForPageSetting((short)PageID)];
            if (PageSettingLoadedEvent != null)
                PageSettingLoadedEvent();
        }
        public void SavePageSettings()
        {
            if (NeedPageSettingEvent != null)  // if subscriber
                PageSettings = NeedPageSettingEvent();
            PageSettingHelper.SavePageSettings(PageID, PageSettings);
            //if (PageSettings != null && PageID != null)
            //    HttpContext.Current.Session[SessionHelper.GetSessionKeyForPageSetting((short)PageID)] = PageSettings;
        }
        // the delegate for NeedPageSettingEvent
        public delegate PageSettings NeedPageSetting();
        // the NeedPageSettingEvent event
        public event NeedPageSetting NeedPageSettingEvent;
        public delegate void PageSettingLoaded();
        public event PageSettingLoaded PageSettingLoadedEvent;


        # endregion

        /// <summary>
        /// Get Current GL Data ID
        /// </summary>

        WebEnums.RecPeriodStatus? _CurrentRecProcessStatus = null;
        public WebEnums.RecPeriodStatus? CurrentRecProcessStatus
        {
            get
            {
                if (!_CurrentRecProcessStatus.HasValue)
                    _CurrentRecProcessStatus = SessionHelper.CurrentRecProcessStatusEnum;
                return _CurrentRecProcessStatus;
            }
            set
            {
                // Save to View State
                _CurrentRecProcessStatus = value;
            }
        }
        [WebMethod]
        public static string[] CheckRecPeriodStatus(string refreshReason)
        {
            string[] result = new string[3];
            result[0] = LanguageUtil.GetValue(2998);
            result[1] = LanguageUtil.GetValue(2999);
            ReconciliationPeriodStatusMstInfo oRecPeriodStatusInfo = SessionHelper.GetRecPeriodStatus();
            if (oRecPeriodStatusInfo != null && oRecPeriodStatusInfo.ReconciliationPeriodStatusID.HasValue)
                result[2] = oRecPeriodStatusInfo.ReconciliationPeriodStatusID.Value.ToString();
            return result;
        }
    }
}