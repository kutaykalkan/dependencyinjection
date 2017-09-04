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
using SkyStem.ART.Web.Classes;
using SkyStem.ART.Web.Utility;
using SkyStem.ART.Web.Data;
using SkyStem.ART.Client.Exception;
using SkyStem.ART.Client.Model;
using SkyStem.ART.Client.IServices;
using System.Collections.Generic;
using SkyStem.ART.Client.Data;
using SkyStem.Library.Controls.WebControls;
using SkyStem.Language.LanguageUtility;
using System.Text;
using SkyStem.Library.Controls.WebControls.Common.Data;
using Telerik.Web.UI;

public partial class Pages_EditItemReviewNote : PopupPageBase
{
    #region Variables & Constants
    Int64? _GLDataReviewNoteID = null;
    string _ParentHiddenField = null;
    GLDataHdrInfo GLDataHdrInfo = null;
    WebEnums.FormMode _FormMode = WebEnums.FormMode.None;
    #endregion

    #region Properties
    List<GLDataReviewNoteDetailInfo> _ReviewNoteDetailList;
    List<GLDataReviewNoteDetailInfo> ReviewNoteDetailList
    {
        get
        {
            if (this._ReviewNoteDetailList == null)
            {
                if (ViewState[ViewStateConstants.REVIEWNOTE_DETAIL] != null)
                    this._ReviewNoteDetailList = (List<GLDataReviewNoteDetailInfo>)ViewState[ViewStateConstants.REVIEWNOTE_DETAIL];
            }
            return this._ReviewNoteDetailList;

        }
        set
        {
            this._ReviewNoteDetailList = value;
            ViewState[ViewStateConstants.REVIEWNOTE_DETAIL] = _ReviewNoteDetailList;
        }
    }
    #endregion

    #region Delegates & Events
    #endregion

    #region Page Events
    protected void Page_Load(object sender, EventArgs e)
    {
        PopupHelper.SetPageTitle(this, 1394);
        GetQueryStringValues();
        ucAccountHierarchyDetailPopup.AccountID = this.GLDataHdrInfo.AccountID;
        ucAccountHierarchyDetailPopup.NetAccountID = this.GLDataHdrInfo.NetAccountID;
        if (!Page.IsPostBack)
        {
            txtSubjectValue.ErrorMessage = Helper.GetErrorMessage(WebEnums.FieldType.MandatoryField, 1778);
            txtReviewNote.ErrorMessage = Helper.GetErrorMessage(WebEnums.FieldType.MandatoryField, 1394);

            try
            {
                // Load the Data
                LoadData();
            }
            catch (ARTException ex)
            {
                PopupHelper.ShowErrorMessage(this, ex);
            }
            catch (Exception ex)
            {
                PopupHelper.ShowErrorMessage(this, ex);
            }

        }
        hlDocument.NavigateUrl = "javascript:OpenRadWindowFromRadWindow('" + SetDocumentUploadURL() + "', '480', '800');";
    }
    #endregion

    #region Grid Events
    protected void rptReviewNotes_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item
            || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            GLDataReviewNoteDetailInfo oGLDataReviewNoteDetailInfo = (GLDataReviewNoteDetailInfo)e.Item.DataItem;

            ExLabel lblUserDetails = (ExLabel)e.Item.FindControl("lblUserDetails");
            ExLabel lblNote = (ExLabel)e.Item.FindControl("lblNote");

            // First Name Last Name [Date Time]: 
            lblUserDetails.Text = string.Format("{0} [{1}]: ", oGLDataReviewNoteDetailInfo.AddedByUserInfo.Name, Helper.GetDisplayDateTime(oGLDataReviewNoteDetailInfo.DateAdded));
            lblNote.Text = oGLDataReviewNoteDetailInfo.ReviewNote;
        }
    }
    #endregion

    #region Other Events
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        //NotifyUser();
        List<UserHdrInfo> oNotifyUserList;
        try
        {
            if (Page.IsValid)
            {
                // Fill Object
                // Save into DB

                GLDataReviewNoteHdrInfo oGLDataReviewNoteHdrInfo = new GLDataReviewNoteHdrInfo();
                oGLDataReviewNoteHdrInfo.GLDataReviewNoteID = _GLDataReviewNoteID;
                oGLDataReviewNoteHdrInfo.DeleteAfterCertification = chkDeleteAfterCertification.Checked;
                oGLDataReviewNoteHdrInfo.GLDataID = this.GLDataHdrInfo.GLDataID;
                oGLDataReviewNoteHdrInfo.ReviewNoteSubject = txtSubjectValue.Text;

                // Fill the Detail Object
                GLDataReviewNoteDetailInfo oGLDataReviewNoteDetailInfo = new GLDataReviewNoteDetailInfo();
                oGLDataReviewNoteDetailInfo.GLDataReviewNoteID = _GLDataReviewNoteID;
                oGLDataReviewNoteDetailInfo.AddedByUserID = SessionHelper.CurrentUserID;
                oGLDataReviewNoteDetailInfo.DateAdded = DateTime.Now;
                oGLDataReviewNoteDetailInfo.AddedBy = SessionHelper.CurrentUserLoginID;
                oGLDataReviewNoteDetailInfo.ReviewNote = txtReviewNote.Text;
                oGLDataReviewNoteDetailInfo.AddedByRole = SessionHelper.CurrentRoleID;

                UserHdrInfo oUser = SessionHelper.GetCurrentUser();
                oGLDataReviewNoteDetailInfo.AddedByUserInfo = oUser;

                oGLDataReviewNoteHdrInfo.GLDataReviewNoteDetailInfoCollection = new List<GLDataReviewNoteDetailInfo>();
                oGLDataReviewNoteHdrInfo.GLDataReviewNoteDetailInfoCollection.Add(oGLDataReviewNoteDetailInfo);

                //Get last user
                IUser oUserClient = RemotingHelper.GetUserObject();
                UserHdrInfo oLastUser = oUserClient.GetLastUserForReviewNote(this.GLDataHdrInfo.GLDataID.Value, SessionHelper.CurrentCompanyID.Value, _GLDataReviewNoteID, SessionHelper.CurrentRoleID.Value, SessionHelper.CurrentUserID.Value, Helper.GetAppUserInfo());

                //Add users to notify list
                oNotifyUserList = new List<UserHdrInfo>();
                oNotifyUserList.Add(oUser);
                oNotifyUserList.Add(oLastUser);

                IGLData oGLDataClient = RemotingHelper.GetGLDataObject();
                if (_GLDataReviewNoteID == null || _GLDataReviewNoteID == 0)
                {
                    // Means case of Add
                    oGLDataReviewNoteHdrInfo.AddedByUserID = SessionHelper.CurrentUserID;
                    oGLDataReviewNoteHdrInfo.DateAdded = DateTime.Now;
                    oGLDataReviewNoteHdrInfo.AddedBy = SessionHelper.CurrentUserLoginID;
                    List<AttachmentInfo> oAttachmentInfoCollection = (List<AttachmentInfo>)Session[SessionConstants.ATTACHMENTS];
                    oGLDataClient.AddReviewNote(oGLDataReviewNoteHdrInfo, SessionHelper.CurrentReconciliationPeriodID.Value, oAttachmentInfoCollection, Helper.GetAppUserInfo());
                    //NotifyUser(false);

                    //Add new Review note detail to existing reviewnotedetail
                    List<GLDataReviewNoteDetailInfo> oNewReviewNoteDetail = new List<GLDataReviewNoteDetailInfo>();

                    oNewReviewNoteDetail.Add(oGLDataReviewNoteDetailInfo);
                    this.ReviewNoteDetailList = oNewReviewNoteDetail;

                    //NotifyUser(false);
                    SendMailToNotifyUsers(oNotifyUserList, oGLDataReviewNoteHdrInfo);
                }
                else
                {
                    // Case of Edit
                    oGLDataReviewNoteHdrInfo.RevisedByUserID = SessionHelper.GetCurrentUser().UserID;
                    oGLDataReviewNoteHdrInfo.DateRevised = DateTime.Now;
                    oGLDataReviewNoteHdrInfo.RevisedBy = SessionHelper.GetCurrentUser().LoginID;

                    oGLDataClient.UpdateReviewNote(oGLDataReviewNoteHdrInfo, Helper.GetAppUserInfo());
                    //NotifyUser(true);
                    //Add new Review note detail to existing reviewnotedetail
                    List<GLDataReviewNoteDetailInfo> oNewReviewNoteDetail = this.ReviewNoteDetailList;
                    oNewReviewNoteDetail.Insert(0, oGLDataReviewNoteDetailInfo);
                    this.ReviewNoteDetailList = oNewReviewNoteDetail;

                    //NotifyUser(true);
                    SendMailToNotifyUsers(oNotifyUserList, oGLDataReviewNoteHdrInfo);
                }
                SessionHelper.ClearSession(SessionConstants.ATTACHMENTS);
                // If reached here, means Success
                // Close Popup and reload Parent page
                if (this._ParentHiddenField != null)
                {
                    this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "SetHiddenFieldStatus", ScriptHelper.GetJSToSetParentWindowElementValue(this._ParentHiddenField, "1")); // 1 means Reload data of GridVieww
                }
                this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "ClosePopupAndRefreshParentPage", ScriptHelper.GetJSForClosePopupAndSubmitParentPage());
            }
        }
        catch (ARTException ex)
        {
            PopupHelper.ShowErrorMessage(this, ex);
        }
        catch (Exception ex)
        {
            PopupHelper.ShowErrorMessage(this, ex);
        }

    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        SessionHelper.ClearSession(SessionConstants.ATTACHMENTS);
        string script = PopupHelper.GetScriptForClosingRadWindow();

        ClientScript.RegisterClientScriptBlock(this.GetType(), "CloseWindow", script);
    }

    #endregion

    #region Validation Control Events
    #endregion

    #region Private Methods
    private void LoadData()
    {
        bool isAddMode = false;

        GLDataReviewNoteHdrInfo oGLDataReviewNoteHdrInfo = null;

        lblRecPeriodValue.Text = Helper.GetDisplayDate(SessionHelper.CurrentReconciliationPeriodEndDate);

        // Fetch the Review Note ID
        if (Request.QueryString[QueryStringConstants.REVIEW_NOTE_ID] != null)
        {
            // Means Case of Edit / View
            IGLData oGLDataClient = RemotingHelper.GetGLDataObject();
            oGLDataReviewNoteHdrInfo = oGLDataClient.GetReviewNoteInfo(_GLDataReviewNoteID, Helper.GetAppUserInfo());

            if (oGLDataReviewNoteHdrInfo != null)
            {
                // Means Case of Edit / View
                lblEnteredByValue.Text = oGLDataReviewNoteHdrInfo.AddedByUserInfo.Name;


                lblDateAddedValue.Text = Helper.GetDisplayDate(oGLDataReviewNoteHdrInfo.DateAdded);
                txtSubjectValue.Text = oGLDataReviewNoteHdrInfo.ReviewNoteSubject;
                if (oGLDataReviewNoteHdrInfo.DeleteAfterCertification != null)
                {
                    chkDeleteAfterCertification.Checked = oGLDataReviewNoteHdrInfo.DeleteAfterCertification.Value;
                }

                if (oGLDataReviewNoteHdrInfo.GLDataReviewNoteDetailInfoCollection.Count > 0)
                    hdnLastNotifyUser.Value = oGLDataReviewNoteHdrInfo.GLDataReviewNoteDetailInfoCollection[0].AddedByUserID.ToString();

                rptReviewNotes.DataSource = oGLDataReviewNoteHdrInfo.GLDataReviewNoteDetailInfoCollection;
                rptReviewNotes.DataBind();
                this.ReviewNoteDetailList = oGLDataReviewNoteHdrInfo.GLDataReviewNoteDetailInfoCollection;
            }
        }
        else
        {
            isAddMode = true;
            // Means Add, so show Current User's Info
            lblEnteredByValue.Text = Helper.GetUserFullName();
            lblDateAddedValue.Text = Helper.GetDisplayDate(DateTime.Now);
        }

        if (this._FormMode == WebEnums.FormMode.Edit)
        {
            pnlReviewNoteText.Visible = true;
            btnUpdate.Visible = true;

            /*
             * 1. Check for Role
             * 2. If Role = Preparer, then show Label for Subject
             * 3. ELSE If Created By != Logged in UserID, then show Label
             *    ELSE
             *      Show Textbox
             */

            ARTEnums.UserRole eUserRole = (ARTEnums.UserRole)System.Enum.Parse(typeof(ARTEnums.UserRole), SessionHelper.CurrentRoleID.Value.ToString());
            if (!isAddMode && oGLDataReviewNoteHdrInfo.AddedByUserID != SessionHelper.GetCurrentUser().UserID)
            {
                ShowLabels(true);
            }
            else
            {
                ShowLabels(false);
            }
        }
        else
        {
            pnlReviewNoteText.Visible = false;
            btnUpdate.Visible = false;
            ShowLabels(true);
        }
    }

    private void ShowLabels(bool bShowLabel)
    {
        Helper.SetExTextBoxDisplayMode(txtSubjectValue, bShowLabel);

        chkDeleteAfterCertification.Enabled = !bShowLabel;

        /* If Certification Capability not available, 
         * then Hide this check box
         */
        if (!Helper.IsCapabilityActivatedForCurrentRecPeriod(ARTEnums.Capability.AllowDeletionOfReviewNotes))
        {
            chkDeleteAfterCertification.Visible = false;
        }
    }
    private void NotifyUser(bool IsEditMode)
    {


        //string AccountDetail = GetAccountDetail();
        //string NotifyUserName = string.Empty;
        //IUser oUserClient = RemotingHelper.GetUserObject();
        //List<UserHdrInfo> oUserHdrInfoCollection = oUserClient.SelectPRAByGLDataID(this.GLDataHdrInfo.GLDataID, Helper.GetAppUserInfo());
        //List<UserHdrInfo> oUserHdrInfoCollection = oUserClient.SelectPRAByGLDataID(this.GLDataHdrInfo.GLDataID);

        //if (SessionHelper.CurrentRoleID.Value == (short)ARTEnums.UserRole.PREPARER)
        //{
        //    //Get Reviewer
        //    UserHdrInfo oNotifyUserHdrInfo = (from o in oUserHdrInfoCollection
        //                                      where o.RoleID.Value == 10
        //                                      select o).FirstOrDefault();
        //    NotifyUserName = oNotifyUserHdrInfo.FirstName + " " + oNotifyUserHdrInfo.LastName;
        //    GetMailbodyhtml(AccountDetail, oNotifyUserHdrInfo.EmailID, NotifyUserName);

        //}

        //if (SessionHelper.CurrentRoleID.Value == (short)ARTEnums.UserRole.APPROVER)
        //{
        //    //Get Reviewer
        //    UserHdrInfo oNotifyUserHdrInfo = (from o in oUserHdrInfoCollection
        //                                      where o.RoleID.Value == 10
        //                                      select o).FirstOrDefault();
        //    NotifyUserName = oNotifyUserHdrInfo.FirstName + " " + oNotifyUserHdrInfo.LastName;
        //    GetMailbodyhtml(AccountDetail, oNotifyUserHdrInfo.EmailID, NotifyUserName);

        //}


        //if (SessionHelper.CurrentRoleID.Value == (short)ARTEnums.UserRole.REVIEWER)
        //{

        //    if (!IsEditMode)
        //    {
        //        //Get Preparer
        //        UserHdrInfo oNotifyUserHdrInfo = (from o in oUserHdrInfoCollection
        //                                          where o.RoleID.Value == 9
        //                                          select o).FirstOrDefault();
        //        NotifyUserName = oNotifyUserHdrInfo.FirstName + " " + oNotifyUserHdrInfo.LastName;
        //        GetMailbodyhtml(AccountDetail, oNotifyUserHdrInfo.EmailID, NotifyUserName);
        //    }
        //    else
        //    {
        //        //Get Last Notify User
        //        UserHdrInfo oNotifyUserHdrInfo = (from o in oUserHdrInfoCollection
        //                                          where o.UserID.Value == Convert.ToInt32(hdnLastNotifyUser.Value)
        //                                          select o).FirstOrDefault();
        //        NotifyUserName = oNotifyUserHdrInfo.FirstName + " " + oNotifyUserHdrInfo.LastName;
        //        GetMailbodyhtml(AccountDetail, oNotifyUserHdrInfo.EmailID, NotifyUserName);

        //    }

        //}

    }

    private void GetQueryStringValues()
    {
        // Fetch Values from QueryString 
        if (Request.QueryString[QueryStringConstants.REVIEW_NOTE_ID] != null)
            this._GLDataReviewNoteID = Convert.ToInt64(Request.QueryString[QueryStringConstants.REVIEW_NOTE_ID]);

        if (!string.IsNullOrEmpty(Request.QueryString[QueryStringConstants.GLDATA_ID]))
        {
            long? _gLDataID = Convert.ToInt64(Request.QueryString[QueryStringConstants.GLDATA_ID]);
            this.GLDataHdrInfo = Helper.GetGLDataHdrInfo(_gLDataID);
        }

        WebEnums.ReconciliationStatus eRecStatus = (WebEnums.ReconciliationStatus)System.Enum.Parse(typeof(WebEnums.ReconciliationStatus), Request.QueryString[QueryStringConstants.REC_STATUS_ID]);

        // Get the Form Mode
        _FormMode = Helper.GetFormMode(WebEnums.ARTPages.EditItemReviewNote, this.GLDataHdrInfo);

        if (Request.QueryString[QueryStringConstants.PARENT_HIDDEN_FIELD] != null)
            this._ParentHiddenField = Request.QueryString[QueryStringConstants.PARENT_HIDDEN_FIELD];
    }
    private void SendMailToNotifyUsers(List<UserHdrInfo> oNotifyUserList, GLDataReviewNoteHdrInfo oGLDataReviewNoteHdrInfo)
    {
        /*
         * if form mode = Add and Current user role = Reviewer, get preparer who put rec in Pending reviewer state last
         * if form mode = Add and Current user role = Approver, get Reviewer who put rec in Pending Approval state last
         * if form mode is edit, get user who added last entry into reviewer note.
         * Get entire thread of the note and send it to both parties.
         */

        string AccountDetail = "";
        string fromAddress = "";
        string mailSubject = "";
        StringBuilder oCommonHTMLMailBody = null;


        try
        {
            //Get current user
            UserHdrInfo oCurrentUser = SessionHelper.GetCurrentUser();


            //Get Account detail
            AccountDetail = GetAccountDetail();

            //Get from address
            fromAddress = AppSettingHelper.GetAppSettingValue(AppSettingConstants.EMAIL_FROM_DEFAULT);

            //Get mail subject
            //1394-Review Notes
            string strReviewNotes = LanguageUtil.GetValue(1394);
            mailSubject = strReviewNotes  + " - " + AccountDetail;

          // get All Attachments
            IAttachment oAttachmentClient = RemotingHelper.GetAttachmentObject();
            IList<AttachmentInfo> oAttachmentCollection = new List<AttachmentInfo>();

            if (oGLDataReviewNoteHdrInfo.GLDataReviewNoteID.HasValue && oGLDataReviewNoteHdrInfo.GLDataReviewNoteID.Value > 0)
            {
                oAttachmentCollection = oAttachmentClient.GetAttachment(oGLDataReviewNoteHdrInfo.GLDataReviewNoteID.Value,(int) WebEnums.RecordType.GLDataReviewNote, SessionHelper.CurrentReconciliationPeriodID, Helper.GetAppUserInfo());
            }

            List<string> oFilePathCollection = null;
            if (oAttachmentCollection.Count > 0)
                oFilePathCollection = oAttachmentCollection.Select(obj => obj.PhysicalPath).ToList<string>();

            foreach (UserHdrInfo oUser in oNotifyUserList)
            {
                //Get common email HTML body
                oCommonHTMLMailBody = GetCommonMailbodyhtml(AccountDetail, oUser);

                //Append signature
                oCommonHTMLMailBody.Append(GetEmailSignature(WebEnums.SignatureEnum.SendBySystemAdmin, SessionHelper.GetCurrentUser().Name, fromAddress));

                try
                {
                    //Send mail
                    MailHelper.SendEmail(fromAddress, oUser.EmailID, mailSubject, oCommonHTMLMailBody.ToString(), oFilePathCollection);
                }
                catch (Exception ex)
                {
                    Helper.FormatAndShowErrorMessage(null, ex);
                }
            }
        }
        catch (Exception ex)
        {
            Helper.FormatAndShowErrorMessage(null, ex);
        }
    }

    private StringBuilder GetCommonMailbodyhtml(string AccountDetail, UserHdrInfo oUser)
    {
        List<GLDataReviewNoteDetailInfo> oReviewNoteDetailList = this.ReviewNoteDetailList;
        StringBuilder oMailBody = new StringBuilder();

        SkyStem.Language.LanguageUtility.Classes.MultilingualAttributeInfo oMultiLingualAttributeInfo = LanguageHelper.GetMultilingualAttributeInfo(SessionHelper.CurrentCompanyID, oUser.DefaultLanguageID);

        string strDear = LanguageUtil.GetValue(1845);
        //Dear
        oMailBody.Append("<table width=\"100%\" cellpadding=\"0\" cellspacing=\"0\">");
        oMailBody.AppendFormat("<tr><td colspan=\"2\" style=\"font-weight: bold;\">{0} {1},</td>", strDear, oUser.Name);

        //AccountDetail
        oMailBody.Append(" </tr>");
        string strAccountDetail = LanguageUtil.GetValue(2533, oMultiLingualAttributeInfo);
        LanguageUtil.GetValue(1001);
        oMailBody.AppendFormat("<tr><td style=\"font-weight: bold;\">{0}:</td>", strAccountDetail);
        oMailBody.AppendFormat("<td style=\"padding-left: 2px;\">  {0}", AccountDetail);

        oMailBody.Append(" </td>  </tr>");
        oMailBody.Append(" <tr> <td colspan=\"2\"></td></tr>");

        //Rec Period
        string strRecPeriod = LanguageUtil.GetValue(1420, oMultiLingualAttributeInfo);
        oMailBody.AppendFormat("<tr><td style=\"font-weight: bold;\">{0}:</td>", strRecPeriod);
        oMailBody.AppendFormat("<td style=\"padding-left: 2px;\">  {0}", lblRecPeriodValue.Text);
        oMailBody.Append(" </td>  </tr>");
        oMailBody.Append(" <tr> <td colspan=\"2\"></td></tr>");

        //Owner-2651 
        string strOwner = LanguageUtil.GetValue(2651, oMultiLingualAttributeInfo);
        oMailBody.AppendFormat("<tr><td style=\"font-weight: bold;\">{0}:</td>", strOwner);
        oMailBody.AppendFormat("<td style=\"padding-left: 2px;\">  {0}", lblEnteredByValue.Text);
        oMailBody.Append(" </td>  </tr>");
        oMailBody.Append(" <tr> <td colspan=\"2\"></td></tr>");

        //DateAdded - 1399 
        string strDate = LanguageUtil.GetValue(1399, oMultiLingualAttributeInfo);
        oMailBody.AppendFormat("<tr><td style=\"font-weight: bold;\">{0}:</td>", strDate);
        oMailBody.AppendFormat("<td style=\"padding-left: 2px;\">  {0}", lblDateAddedValue.Text);
        oMailBody.Append(" </td>  </tr>");
        oMailBody.Append(" <tr> <td colspan=\"2\"></td></tr>");

        //Subject
        string strSubject = LanguageUtil.GetValue(1778, oMultiLingualAttributeInfo);
        oMailBody.AppendFormat("<tr><td style=\"font-weight: bold;\">{0}:</td>", strSubject);
        oMailBody.AppendFormat("<td style=\"padding-left: 2px;\">  {0}", txtSubjectValue.Text);
        oMailBody.Append(" </td>  </tr>");
        oMailBody.Append(" <tr> <td colspan=\"2\"></td></tr>");

        //Review Note
        string strReviewNote = LanguageUtil.GetValue(1394, oMultiLingualAttributeInfo);


        bool isLatestNote = true;
        foreach (GLDataReviewNoteDetailInfo oReviewNoteDetail in oReviewNoteDetailList)
        {
            if (isLatestNote)
            {
                oMailBody.AppendFormat("<tr><td style=\"font-weight: bold;\">{0}:</td>", strReviewNote);
                oMailBody.AppendFormat("<td style=\"padding-left: 2px ; color : Blue ;\">  {0} [{1}]: {2}", oReviewNoteDetail.AddedByUserInfo.Name, oReviewNoteDetail.DateAdded, oReviewNoteDetail.ReviewNote);
                isLatestNote = false;
            }
            else
            {
                oMailBody.Append("<tr><td style=\"font-weight: bold;\"></td>");
                oMailBody.AppendFormat("<td style=\"padding-left: 2px;\">  {0} [{1}]: {2}", oReviewNoteDetail.AddedByUserInfo.Name, oReviewNoteDetail.DateAdded, oReviewNoteDetail.ReviewNote);
            }

            oMailBody.Append(" </td>  </tr>");
        }
        oMailBody.Append(" <tr> <td colspan=\"2\"></td></tr>");
        oMailBody.Append(" </table>");
        oMailBody.Append("<br>");

        return oMailBody;
    }

    private void GetMailbodyhtml(string AcDetail, string toMail, string toname)
    {
        StringBuilder oMailBody = new StringBuilder();
        //string AcDetail = GetAccountDetail();
        try
        {

            oMailBody.Append("<table width=\"100%\" cellpadding=\"0\" cellspacing=\"0\">");

            oMailBody.AppendFormat("<tr><td colspan=\"2\" style=\"font-weight: bold;\">Dear {0},</td>", toname);

            oMailBody.Append(" </tr>");
            oMailBody.Append("<tr><td style=\"font-weight: bold;\">Account Detail:</td>");
            oMailBody.AppendFormat("<td style=\"padding-left: 2px;\">  {0}", AcDetail);
            oMailBody.Append(" </td>  </tr>");
            oMailBody.Append(" <tr> <td colspan=\"2\"></td></tr>");

            oMailBody.Append("<tr><td style=\"font-weight: bold;\">Rec Period:</td>");
            oMailBody.AppendFormat("<td style=\"padding-left: 2px;\">  {0}", lblRecPeriodValue.Text);
            oMailBody.Append(" </td>  </tr>");
            oMailBody.Append(" <tr> <td colspan=\"2\"></td></tr>");

            oMailBody.Append("<tr><td style=\"font-weight: bold;\">Owner:</td>");
            oMailBody.AppendFormat("<td style=\"padding-left: 2px;\">  {0}", lblEnteredByValue.Text);
            oMailBody.Append(" </td>  </tr>");
            oMailBody.Append(" <tr> <td colspan=\"2\"></td></tr>");

            oMailBody.Append("<tr><td style=\"font-weight: bold;\">Date:</td>");
            oMailBody.AppendFormat("<td style=\"padding-left: 2px;\">  {0}", lblDateAddedValue.Text);
            oMailBody.Append(" </td>  </tr>");
            oMailBody.Append(" <tr> <td colspan=\"2\"></td></tr>");

            oMailBody.Append("<tr><td style=\"font-weight: bold;\">Subject:</td>");
            oMailBody.AppendFormat("<td style=\"padding-left: 2px;\">  {0}", txtSubjectValue.Text);
            oMailBody.Append(" </td>  </tr>");
            oMailBody.Append(" <tr> <td colspan=\"2\"></td></tr>");

            oMailBody.Append("<tr><td style=\"font-weight: bold;\">Review Note:</td>");

            string userdetails = SessionHelper.GetCurrentUser().Name + " [" + System.DateTime.Now.ToString() + "]:";
            oMailBody.AppendFormat("<td style=\"padding-left: 2px ; color : Blue ;\">{0}  {1}", userdetails, txtReviewNote.Text);
            oMailBody.Append(" </td>  </tr>");
            string[] MsgThreadArr = hdnMsgThread.Value.Split('~');
            for (int i = 0; i < MsgThreadArr.Length - 1; i++)
            {
                oMailBody.Append("<tr><td style=\"font-weight: bold;\"></td>");
                oMailBody.AppendFormat("<td style=\"padding-left: 2px;\">  {0}", MsgThreadArr[i]);
                oMailBody.Append(" </td>  </tr>");
            }



            oMailBody.Append(" <tr> <td colspan=\"2\"></td></tr>");
            oMailBody.Append(" </table>");

            oMailBody.Append("<br>");
            //string fromAddress = SessionHelper.CurrentUserEmailID;
            string fromAddress = AppSettingHelper.GetAppSettingValue(AppSettingConstants.EMAIL_FROM_DEFAULT);
            oMailBody.Append(GetEmailSignature(WebEnums.SignatureEnum.SendBySystemAdmin, SessionHelper.GetCurrentUser().Name, fromAddress));


            string mailSubject = "Review Note-" + SessionHelper.CurrentReconciliationPeriodID.ToString() + "-" + AcDetail;
            //string fromAddress = AppSettingHelper.GetAppSettingValue(AppSettingConstants.EMAIL_FROM_DEFAULT);

            string toAddress = toMail;
            MailHelper.SendEmail(fromAddress, toAddress, mailSubject, oMailBody.ToString());
        }
        catch (Exception ex)
        {
            Helper.FormatAndShowErrorMessage(null, ex);
        }



    }

    private string GetEmailSignature(SkyStem.ART.Web.Data.WebEnums.SignatureEnum oSignatureEnum, string FromName, string fromAddress)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("<br/><br/>" + LanguageUtil.GetValue(2019));
        sb.Append("<br/><b>" + FromName + "</b>");
        sb.Append("<br/><b>" + SessionHelper.CurrentRoleEnum + "</b>");
        sb.Append("<br/>" + Helper.GetCompanyName());

        string fromEmailAddressConfig = AppSettingHelper.GetAppSettingValue(AppSettingConstants.EMAIL_FROM_DEFAULT);

        if (fromAddress != fromEmailAddressConfig)
        {
            sb.Append("<br/><br/>" + LanguageUtil.GetValue(2042));
        }
        else
        {
            sb.Append("<br/><br/>" + LanguageUtil.GetValue(2021));
        }

        return sb.ToString();
    }
    #endregion

    #region Other Methods
    protected String GetAccountDetail()
    {
        String AccountDetails = string.Empty;
        try
        {
            IAccount oAccountClient = RemotingHelper.GetAccountObject();

            if (this.GLDataHdrInfo.AccountID.HasValue && this.GLDataHdrInfo.AccountID.Value > 0)
            {

                AccountDetails = Helper.GetAccountEntityStringToDisplay(this.GLDataHdrInfo.AccountID.Value);
            }
            else if (this.GLDataHdrInfo.NetAccountID.HasValue && this.GLDataHdrInfo.NetAccountID.Value > 0)
            {
                AccountDetails = LanguageUtil.GetValue(1257) + WebConstants.ACCOUNT_ENTITY_SEPARATOR + oAccountClient.GetNetAccountNameByNetAccountID(this.GLDataHdrInfo.NetAccountID.Value, SessionHelper.CurrentCompanyID.Value
                    , SessionHelper.GetUserLanguage(), SessionHelper.GetBusinessEntityID(), AppSettingHelper.GetDefaultLanguageID(), Helper.GetAppUserInfo());
            }
        }
        catch (Exception ex)
        {
            Helper.LogException(ex);
        }
        return AccountDetails;
    }

    public string SetDocumentUploadURL()
    {
        string windowName;
        string url = Helper.SetDocumentUploadURLForRecItemInput(this.GLDataHdrInfo.GLDataID, _GLDataReviewNoteID, this.GLDataHdrInfo.AccountID, this.GLDataHdrInfo.NetAccountID, (_FormMode == WebEnums.FormMode.ReadOnly), Request.Url.ToString(), out windowName, false, WebEnums.RecordType.GLDataReviewNote);

        return url;
    }

    #endregion


}
