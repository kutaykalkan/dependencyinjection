using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SkyStem.ART.Web.Data;
using SkyStem.ART.Web.Utility;


namespace SkyStem.ART.Web.Classes
{
    /// <summary>
    /// Summary description for PageBaseMatching
    /// </summary>
    public abstract class PageBaseMatching : PageBase
    {

        #region Properties
        /// <summary>
        /// Form Mode for Current Form
        /// </summary>
        public WebEnums.FormMode FormMode
        {
            get
            {
                WebEnums.FormMode eFormMode = WebEnums.FormMode.ReadOnly;
                if (ViewState["FormMode"] != null)
                    eFormMode = (WebEnums.FormMode)ViewState["FormMode"];
                return eFormMode;
            }
            set
            {
                ViewState["FormMode"] = value; 
            }
        }

        /// <summary>
        /// Get Current GL Data ID
        /// </summary>
        public long? GLDataID
        {
            get
            {
                long? _glDataID = null;
                if (!Page.IsPostBack)
                {
                    if (!string.IsNullOrEmpty(Request.QueryString[QueryStringConstants.GLDATA_ID]))
                    {
                        _glDataID = Convert.ToInt64(Request.QueryString[QueryStringConstants.GLDATA_ID]);
                        // Save to View State
                        ViewState["GLDataID"] = _glDataID;
                    }
                }
                else
                    _glDataID = ViewState["GLDataID"] == null ? _glDataID : Convert.ToInt64(ViewState["GLDataID"]);
                return _glDataID;
            }
            set
            {
                // Save to View State
                ViewState["GLDataID"] = value;
            }
        }

        /// <summary>
        /// Get Account ID from Query String
        /// </summary>
        public long? AccountID
        {
            get
            {
                if (!string.IsNullOrEmpty(Request.QueryString[QueryStringConstants.ACCOUNT_ID]))
                    return Convert.ToInt64(Request.QueryString[QueryStringConstants.ACCOUNT_ID]);
                return null;
            }
        }


        /// <summary>
        /// Get Net Account ID from Query String
        /// </summary>
        public int? NetAccountID
        {
            get
            {
                if (!string.IsNullOrEmpty(Request.QueryString[QueryStringConstants.NETACCOUNT_ID]))
                    return Convert.ToInt32(Request.QueryString[QueryStringConstants.NETACCOUNT_ID]);
                return null;
            }

        }

        /// <summary>
        /// Get IsSRA from Query String
        /// </summary>
        public bool? IsSRA
        {
            get
            {
                if (Request.QueryString[QueryStringConstants.IS_SRA] != null)
                    return Convert.ToBoolean(Convert.ToInt16(Request.QueryString[QueryStringConstants.IS_SRA]));
                return null;
            }
        }

        #endregion

        public PageBaseMatching()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            //if (!Page.IsPostBack)
            //    GLDataID = Helper.GetGLDataID(this, AccountID, NetAccountID);
        }

        public override void RefreshPage(object sender, RefreshEventArgs args)
        {
            base.RefreshPage(sender, args);
            GLDataID = Helper.GetGLDataID(this, AccountID, NetAccountID);
        }

        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);

            // Check for Rec Period
            if (SessionHelper.CurrentReconciliationPeriodID == null)
            {
                Helper.RedirectToErrorPage(5000061, true);
            }
                       
        }

        public string ReturnUrl
        {
            get
            {
                return ViewState["returnUrl"].ToString();
            }
            set
            {
                ViewState["returnUrl"] = value;
            }
        }


    }
}