using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using SkyStem.ART.Web.Data;
using SkyStem.Library.Controls.WebControls;
using SkyStem.ART.Client.Model;
using System.Collections.Generic;
using SkyStem.ART.Client.IServices;
using SkyStem.Language.LanguageUtility;
using SkyStem.ART.Client.Exception;
using SkyStem.ART.Web.Utility;
using SkyStem.ART.Web.Classes;

namespace SkyStem.ART.Web.UserControls
{
    public partial class UserControls_RecFormButtons : UserControlBase
    {

        #region Variables & Constants
        #endregion

        #region Properties

        #region GLDataHdr Properties

        private GLDataHdrInfo _GLDataHdrInfo = null;
        public GLDataHdrInfo GLDataHdrInfo
        {
            get
            {
                if (_GLDataHdrInfo == null)
                    _GLDataHdrInfo = (GLDataHdrInfo)ViewState[ViewStateConstants.CURRENT_GLDATAHDRINFO];
                return _GLDataHdrInfo;
            }
            set
            {
                _GLDataHdrInfo = value;
                ViewState[ViewStateConstants.CURRENT_GLDATAHDRINFO] = value;
            }
        }

        public string CurrentBCCY
        {
            get
            {
                string _CurrentBCCY = string.Empty;
                if (GLDataHdrInfo != null)
                    _CurrentBCCY = GLDataHdrInfo.BaseCurrencyCode;
                return _CurrentBCCY;
            }
        }

        public WebEnums.ReconciliationStatus GLRecStatus
        {
            get
            {
                WebEnums.ReconciliationStatus _GLRecStatus = WebEnums.ReconciliationStatus.NotStarted;
                if (GLDataHdrInfo != null && GLDataHdrInfo.ReconciliationStatusID.HasValue)
                    _GLRecStatus = (WebEnums.ReconciliationStatus)GLDataHdrInfo.ReconciliationStatusID.Value;
                return _GLRecStatus;
            }
        }

        public long? AccountID
        {
            get
            {
                if (GLDataHdrInfo != null)
                    return (long)GLDataHdrInfo.AccountID;
                return null;
            }
        }

        public int? NetAccountID
        {
            get
            {
                if (GLDataHdrInfo != null)
                    return (int)GLDataHdrInfo.NetAccountID;
                return null;
            }
        }

        public long? GLDataID
        {
            get
            {
                if (GLDataHdrInfo != null)
                    return GLDataHdrInfo.GLDataID;
                return null;
            }
        }

        public bool? IsSRA
        {
            get
            {
                if (GLDataHdrInfo != null)
                    return GLDataHdrInfo.IsSystemReconcilied.GetValueOrDefault();
                return false;
            }
        }

        #endregion

        private WebEnums.ARTPages _eARTPages;
        public WebEnums.ARTPages eARTPages
        {
            set
            {
                this._eARTPages = value;
            }
        }

        private short? _reconciliationStatusID;
        public short? ReconciliationStatusID
        {
            set
            {
                this._reconciliationStatusID = value;
            }
        }

        private short? _currentUserRole;
        public short? CurrentUserRole
        {
            set
            {
                this._currentUserRole = value;
            }
        }

        #endregion

        #region Delegates & Events
        public delegate void ButtonClick(string commandName);
        public event ButtonClick eventButtonClick;
        #endregion

        #region Page Events
        protected void Page_Load(object sender, EventArgs e)
        {
            EnableDisableButtons();
        }
        #endregion

        #region Grid Events
        #endregion

        #region Other Events
        protected void btn_OnCommand(object sender, CommandEventArgs e)
        {
            try
            {
                string commandName = "";
                switch (e.CommandName)
                {
                    case "Save":
                        commandName = RecFormButtonCommandName.SAVE;
                        break;

                    case "Cancel":
                        short eArtPageID = 0;
                        if (!string.IsNullOrEmpty(Request.QueryString[QueryStringConstants.REFERRER_PAGE_ID]))
                        {
                            eArtPageID = Convert.ToInt16(Request.QueryString[QueryStringConstants.REFERRER_PAGE_ID]);
                        }

                        if (eArtPageID == (short)WebEnums.ARTPages.SystemReconciledAccounts)
                        {
                            Response.Redirect("SystemReconciledAccount.aspx");
                        }
                        else if (eArtPageID == (short)WebEnums.ARTPages.AccountViewer)
                        {
                            HttpContext.Current.Response.Redirect(Helper.GetRedirectURLForTemplatePages(this.IsSRA, WebEnums.ARTPages.AccountViewer));
                        }
                        else if (eArtPageID == (short)WebEnums.ARTPages.CertificationBalances)
                        {
                            Response.Redirect("~/Pages/Certification/CertificationBalances.aspx");
                        }
                        else if (eArtPageID == (short)WebEnums.ARTPages.CertificationException)
                        {
                            Response.Redirect("~/Pages/Certification/CertificationException.aspx");
                        }
                        commandName = RecFormButtonCommandName.CANCEL;
                        break;

                    case "Signoff":
                        commandName = RecFormButtonCommandName.SIGNOFF;
                        break;

                    case "EditRec":
                        commandName = RecFormButtonCommandName.EDIT_REC;
                        break;

                    case "Approve":
                        commandName = RecFormButtonCommandName.APPROVE;
                        break;

                    case "Deny":
                        this.GetReviewNoteStatusForReviewerAndApprover();
                        commandName = RecFormButtonCommandName.DENY;
                        break;

                    case "Accept":
                        commandName = RecFormButtonCommandName.ACCEPT;
                        break;

                    case "Reject":
                        this.GetReviewNoteStatusForReviewerAndApprover();
                        commandName = RecFormButtonCommandName.REJECT;
                        break;
                    case "RemoveSignOff":
                        commandName = RecFormButtonCommandName.REMOVE_SIGN_OFF;
                        break;
                }
                Save(commandName);
            }
            catch (ARTException ex)
            {
                Helper.ShowErrorMessage((PageBase)this.Page, ex);
            }
            catch (Exception ex)
            {
                Helper.ShowErrorMessage((PageBase)this.Page, ex);
            }
        }
        private void GetReviewNoteStatusForReviewerAndApprover()
        {
            IGLData oGLDataClient = RemotingHelper.GetGLDataObject();
            bool result = oGLDataClient.GetReviewNoteStatusForReviewerAndApprover(this.GLDataID.Value, SessionHelper.CurrentReconciliationPeriodID.Value, SessionHelper.CurrentUserID.Value, Helper.GetAppUserInfo());

            if (!result)
            {
                throw new ARTException(5000088);
            }
        }
        #endregion

        #region Validation Control Events
        #endregion

        #region Private Methods
        private void HideAllButtons()
        {
            this.btnSave.Visible = false;
            this.btnSignoff.Visible = false;
            this.btnEditRec.Visible = false;

            this.btnApprove.Visible = false;
            this.btnDeny.Visible = false;

            this.btnAccept.Visible = false;
            this.btnReject.Visible = false;

            // For SRAs
            this.btnRemoveSignOff.Visible = false;
        }

        private void ShowHideButtonsForPreparer()
        {
            if (this._currentUserRole == (short)WebEnums.UserRole.PREPARER || this._currentUserRole == (short)WebEnums.UserRole.BACKUP_PREPARER)
            {
                WebEnums.ReconciliationStatus eReconciliationStatus = (WebEnums.ReconciliationStatus)System.Enum.Parse(typeof(WebEnums.ReconciliationStatus), _reconciliationStatusID.Value.ToString());
                switch (eReconciliationStatus)
                {
                    case WebEnums.ReconciliationStatus.NotStarted:
                    case WebEnums.ReconciliationStatus.InProgress:
                    case WebEnums.ReconciliationStatus.PendingModificationPreparer:
                        btnSave.Visible = true;
                        btnSignoff.Visible = true;
                        break;

                    case WebEnums.ReconciliationStatus.Prepared:
                        // Remove Acct Sign-Off is visible only to P
                        if (this.IsSRA != null && this.IsSRA == true)
                        {
                            this.btnRemoveSignOff.Visible = true;
                        }
                        else
                        {
                            btnEditRec.Visible = true;
                        }
                        break;
                }
            }
        }

        private void ShowHideButtonsForReviewer()
        {
            if (this._currentUserRole == (short)WebEnums.UserRole.REVIEWER || this._currentUserRole == (short)WebEnums.UserRole.BACKUP_REVIEWER)
            {
                WebEnums.ReconciliationStatus eReconciliationStatus = (WebEnums.ReconciliationStatus)System.Enum.Parse(typeof(WebEnums.ReconciliationStatus), _reconciliationStatusID.Value.ToString());
                switch (eReconciliationStatus)
                {
                    case WebEnums.ReconciliationStatus.PendingModificationReviewer:
                    case WebEnums.ReconciliationStatus.PendingReview:
                        btnReject.Visible = true;
                        btnAccept.Visible = true;
                        SetConfirmJSMethod(btnReject, 1754);
                        break;

                    case WebEnums.ReconciliationStatus.Reviewed:
                        btnEditRec.Visible = true;
                        break;
                }
            }
        }

        private void ShowHideButtonsForApprover()
        {
            if (this._currentUserRole == (short)WebEnums.UserRole.APPROVER || this._currentUserRole == (short)WebEnums.UserRole.BACKUP_APPROVER)
            {
                WebEnums.ReconciliationStatus eReconciliationStatus = (WebEnums.ReconciliationStatus)System.Enum.Parse(typeof(WebEnums.ReconciliationStatus), _reconciliationStatusID.Value.ToString());
                switch (eReconciliationStatus)
                {
                    case WebEnums.ReconciliationStatus.PendingApproval:
                        btnDeny.Visible = true;
                        btnApprove.Visible = true;
                        SetConfirmJSMethod(btnDeny, 1759);
                        break;

                    case WebEnums.ReconciliationStatus.Approved:
                        btnEditRec.Visible = true;
                        break;
                }
            }
        }


        private void SetConfirmJSMethod(ExButton btn, int lableID)
        {
            btn.OnClientClick = "return ConfirmDelete('" + LanguageUtil.GetValue(lableID) + "');";
        }

        private void Save(string commandName)
        {
            EventArgs arg = new EventArgs();
            this.eventButtonClick(commandName);
        }
        #endregion

        #region Other Methods
        public void EnableDisableButtons()
        {
            /* 
             * By default, hide all Buttons
             * Only if the Rec Status has a value of Edit then show buttons
             * Handle only the Visible = True, as by default everything is Visible = False
             */

            HideAllButtons();

            if (_reconciliationStatusID.HasValue)
            {
                //WebEnums.FormMode eFormMode = Helper.GetFormMode(_eARTPages, (WebEnums.ReconciliationStatus)_reconciliationStatusID.Value);
                WebEnums.FormMode eFormMode = Helper.GetFormMode(WebEnums.ARTPages.RecFormButtons, this.GLDataHdrInfo);

                if (eFormMode == WebEnums.FormMode.Edit)
                {
                    //BASED ON ReconciliationStatusID and current role
                    ShowHideButtonsForPreparer();

                    ShowHideButtonsForReviewer();

                    ShowHideButtonsForApprover();
                }
            }
        }

        #endregion

    }//end of class
}//end of namespace
