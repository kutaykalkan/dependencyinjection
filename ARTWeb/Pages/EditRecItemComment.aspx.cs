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

public partial class Pages_EditRecItemComment : PopupPageBase
{
    #region Variables & Constants
    WebEnums.FormMode? _FormMode = null;
    Int64? _RecordID = null;
    Int16? _RecordTypeID = null;
    Int64? _GLDataID = null;
    Int64? _AccountID = null;
    Int32? _NetAccountID = null;
    WebEnums.ReconciliationStatus _eRecStatus;

    #endregion
    #region Properties
    List<RecItemCommentInfo> _RecItemCommentInfoList;
    List<RecItemCommentInfo> RecItemCommentInfoList
    {
        get
        {
            if (this._RecItemCommentInfoList == null)
            {
                if (ViewState[ViewStateConstants.REC_ITEM_COMMENT_LIST] != null)
                    this._RecItemCommentInfoList = (List<RecItemCommentInfo>)ViewState[ViewStateConstants.REC_ITEM_COMMENT_LIST];
            }
            return this._RecItemCommentInfoList;
        }
        set
        {
            this._RecItemCommentInfoList = value;
            ViewState[ViewStateConstants.REC_ITEM_COMMENT_LIST] = _RecItemCommentInfoList;
        }
    }
    List<UserHdrInfo> _oUserHdrInfoCollection = null;
    List<UserHdrInfo> oUserHdrInfoCollection
    {
        get
        {
            if (this._oUserHdrInfoCollection == null)
            {
                if (ViewState[ViewStateConstants.PRA_LIST] != null)
                    this._oUserHdrInfoCollection = (List<UserHdrInfo>)ViewState[ViewStateConstants.PRA_LIST];
            }
            return this._oUserHdrInfoCollection;

        }
        set
        {
            this._oUserHdrInfoCollection = value;
            ViewState[ViewStateConstants.PRA_LIST] = _oUserHdrInfoCollection;
        }
    }
    GLDataRecItemInfo _GLDataRecItemInfo = null;
    GLDataRecItemInfo oGLDataRecItemInfo
    {
        get
        {
            if (this._GLDataRecItemInfo == null)
            {
                if (ViewState["GLDataRecItemInfo"] != null)
                    this._GLDataRecItemInfo = (GLDataRecItemInfo)ViewState["GLDataRecItemInfo"];
            }
            return this._GLDataRecItemInfo;

        }
        set
        {
            this._GLDataRecItemInfo = value;
            ViewState["GLDataRecItemInfo"] = _GLDataRecItemInfo;
        }
    }
    WebEnums.FormMode FormMode
    {
        get
        {
            if (this._FormMode == null)
            {
                if (ViewState["FormMode"] != null)
                    this._FormMode = (WebEnums.FormMode)ViewState["FormMode"];
            }
            return this._FormMode.Value;
        }
        set
        {
            this._FormMode = value;
            ViewState["FormMode"] = _FormMode;
        }
    }
    #endregion
    #region Delegates & Events
    #endregion
    #region Page Events
    protected void Page_Init(object sender, EventArgs e)
    {
        rptComments.ItemDataBound += new RepeaterItemEventHandler(rptComments_ItemDataBound);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        PopupHelper.SetPageTitle(this, 2749);
        GetQueryStringValues();
        txtComment.ErrorMessage = Helper.GetErrorMessage(WebEnums.FieldType.MandatoryField, 2750);
        if (!Page.IsPostBack)
        {
            try
            {
                // Get the Form Mode
                FormMode = Helper.GetFormModeForRecItemComment(_eRecStatus);
                IGLDataRecItem oGLDataRecItemClient = RemotingHelper.GetGLDataRecItemObject();
                oGLDataRecItemInfo = oGLDataRecItemClient.GetGLDataRecItem(_RecordID, _RecordTypeID, Helper.GetAppUserInfo());
                GetNotifyUser();
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
        GetRecItemDetail();
        if (this.FormMode == WebEnums.FormMode.Edit)
        {
            pnlCommentText.Visible = true;
            btnUpdate.Visible = true;
            trEnteredBy.Visible = true;
        }
        else
        {
            pnlCommentText.Visible = false;
            btnUpdate.Visible = false;
            trEnteredBy.Visible = false;
        }
    }
    #endregion
    #region Grid Events
    protected void rptComments_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item
            || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            RecItemCommentInfo oRecItemCommentInfo = (RecItemCommentInfo)e.Item.DataItem;
            ExLabel lblUserDetails = (ExLabel)e.Item.FindControl("lblUserDetails");
            ExLabel lblComments = (ExLabel)e.Item.FindControl("lblComments");
            // First Name Last Name [Date Time]: 
            lblUserDetails.Text = string.Format("{0} [{1}]: ", oRecItemCommentInfo.AddedByUserInfo.Name, Helper.GetDisplayDateTime(oRecItemCommentInfo.DateAdded));
            lblComments.Text = oRecItemCommentInfo.Comment;
        }
    }
    #endregion
    #region Other Events
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                // Save into DB
                // Fill the Detail Object
                RecItemCommentInfo oRecItemCommentInfo = new RecItemCommentInfo();
                oRecItemCommentInfo.RecItemID = _RecordID;
                oRecItemCommentInfo.RecordTypeID = _RecordTypeID;
                oRecItemCommentInfo.AddedByUserID = SessionHelper.CurrentUserID;
                oRecItemCommentInfo.DateAdded = DateTime.Now;
                oRecItemCommentInfo.AddedBy = SessionHelper.CurrentUserLoginID;
                oRecItemCommentInfo.Comment = txtComment.Text;
                oRecItemCommentInfo.AddedByUserRoleID = SessionHelper.CurrentRoleID;
                IGLDataRecItem oGLDataRecItemClient = RemotingHelper.GetGLDataRecItemObject();
                if (_RecordID != null || _RecordID > 0)
                {
                    oGLDataRecItemClient.SaveRecItemComment(oRecItemCommentInfo, Helper.GetAppUserInfo());
                    LoadData();
                    SendMailToNotifyUsers(oUserHdrInfoCollection);
                    txtComment.Text = "";
                }
                // If reached here, means Success             
                // this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "ClosePopupAndRefreshParentPage", ScriptHelper.GetJSForClosePopupAndSubmitParentPage());
                // this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "ClosePopupAndRefreshParentPage", ScriptHelper.GetJSForClosePopupAndSubmitParentPage(true));

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
        this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "ClosePopupAndRefreshParentPage", ScriptHelper.GetJSForClosePopupAndSubmitParentPage(true));
    }
    #endregion
    #region Validation Control Events
    #endregion
    #region Private Methods
    private string GetRecItemDetail()
    {
        if (!string.IsNullOrEmpty(oGLDataRecItemInfo.Comments))
        {
            string _displayString = oGLDataRecItemInfo.Comments;
            if (oGLDataRecItemInfo.Comments.Length > 50)
            {
                _displayString = oGLDataRecItemInfo.Comments.Substring(0, 50) + "...";
            }
            lblRecItemDetails.ToolTip = HttpContext.Current.Server.HtmlEncode(oGLDataRecItemInfo.Comments);
            lblRecItemDetails.Text = oGLDataRecItemInfo.RecItemNumber + "-" + _displayString;
        }
        else
            lblRecItemDetails.Text = oGLDataRecItemInfo.RecItemNumber;

        return oGLDataRecItemInfo.RecItemNumber;
    }
    private string GetRecItemAllDetail()
    {
        if (string.IsNullOrEmpty(oGLDataRecItemInfo.Comments))
            return oGLDataRecItemInfo.RecItemNumber;
        else
            return oGLDataRecItemInfo.RecItemNumber + "-" + oGLDataRecItemInfo.Comments;
    }
    private void LoadData()
    {
        lblRecPeriodValue.Text = Helper.GetDisplayDate(SessionHelper.CurrentReconciliationPeriodEndDate);
        // Fetch the RecItem Comments
        if (_RecordID != null && _RecordTypeID != null)
        {
            // Means Case of Edit / View
            IGLDataRecItem oGLDataRecItemClient = RemotingHelper.GetGLDataRecItemObject();
            RecItemCommentInfoList = oGLDataRecItemClient.GetRecItemCommentList(_RecordID, _RecordTypeID, Helper.GetAppUserInfo());
            // Means Case of Edit / View
            lblEnteredByValue.Text = Helper.GetUserFullName();
            lblDateAddedValue.Text = Helper.GetDisplayDate(DateTime.Now);

            if (RecItemCommentInfoList != null && RecItemCommentInfoList.Count > 0)
            {
                rptComments.DataSource = RecItemCommentInfoList;
                rptComments.DataBind();
                this.RecItemCommentInfoList = RecItemCommentInfoList;
            }
        }

    }
    private void GetNotifyUser()
    {
        //IUser oUserClient = RemotingHelper.GetUserObject();
        //oUserHdrInfoCollection = oUserClient.SelectPRAByGLDataID(_GLDataID, Helper.GetAppUserInfo());
        if (oUserHdrInfoCollection == null)
            oUserHdrInfoCollection = new List<UserHdrInfo>();
        oUserHdrInfoCollection.Add(SessionHelper.GetCurrentUser());
    }
    private void GetQueryStringValues()
    {
        // Fetch Values from QueryString 
        if (!string.IsNullOrEmpty(Request.QueryString[QueryStringConstants.RECORD_ID]))
            this._RecordID = Convert.ToInt64(Request.QueryString[QueryStringConstants.RECORD_ID]);
        if (!string.IsNullOrEmpty(Request.QueryString[QueryStringConstants.RECORD_TYPE_ID]))
            this._RecordTypeID = Convert.ToInt16(Request.QueryString[QueryStringConstants.RECORD_TYPE_ID]);
        if (!string.IsNullOrEmpty(Request.QueryString[QueryStringConstants.REC_STATUS_ID]))
            this._eRecStatus = (WebEnums.ReconciliationStatus)System.Enum.Parse(typeof(WebEnums.ReconciliationStatus), Request.QueryString[QueryStringConstants.REC_STATUS_ID]);
        if (!string.IsNullOrEmpty(Request.QueryString[QueryStringConstants.GLDATA_ID]))
            _GLDataID = Convert.ToInt64(Request.QueryString[QueryStringConstants.GLDATA_ID]);
        if (!string.IsNullOrEmpty(Request.QueryString[QueryStringConstants.ACCOUNT_ID]))
            _AccountID = Convert.ToInt64(Request.QueryString[QueryStringConstants.ACCOUNT_ID]);
        if (!string.IsNullOrEmpty(Request.QueryString[QueryStringConstants.NETACCOUNT_ID]))
            _NetAccountID = Convert.ToInt32(Request.QueryString[QueryStringConstants.NETACCOUNT_ID]);
    }
    private void SendMailToNotifyUsers(List<UserHdrInfo> oNotifyUserList)
    {
        string fromAddress = "";
        string mailSubject = "";
        StringBuilder oCommonHTMLMailBody = null;
        try
        {
            //Get current user
            UserHdrInfo oCurrentUser = SessionHelper.GetCurrentUser();
            //Get from address
            fromAddress = AppSettingHelper.GetAppSettingValue(AppSettingConstants.EMAIL_FROM_DEFAULT);
            //Get mail subject
            mailSubject = string.Format(LanguageUtil.GetValue(2758), GetRecItemDetail());
            string AccountDetail = GetAccountInfo();
            string strRecItemDetail = GetRecItemAllDetail();
            foreach (UserHdrInfo oUser in oNotifyUserList)
            {
                //Get common email HTML body
                oCommonHTMLMailBody = GetCommonMailbodyhtml(AccountDetail, strRecItemDetail, oUser);
                //Append signature
                oCommonHTMLMailBody.Append(GetEmailSignature(WebEnums.SignatureEnum.SendBySystemAdmin, SessionHelper.GetCurrentUser().Name, fromAddress));
                try
                {
                    //Send mail
                    MailHelper.SendEmail(fromAddress, oUser.EmailID, mailSubject, oCommonHTMLMailBody.ToString());
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
    private StringBuilder GetCommonMailbodyhtml(string AccountDetail, string RecItemDetail, UserHdrInfo oUser)
    {
        StringBuilder oMailBody = new StringBuilder();
        SkyStem.Language.LanguageUtility.Classes.MultilingualAttributeInfo oMultiLingualAttributeInfo = LanguageHelper.GetMultilingualAttributeInfo(SessionHelper.CurrentCompanyID, oUser.DefaultLanguageID);

        string strDear = LanguageUtil.GetValue(1845);
        //Dear
        oMailBody.Append("<table width=\"100%\" cellpadding=\"0\" cellspacing=\"0\">");
        oMailBody.AppendFormat("<tr><td colspan=\"2\" style=\"font-weight: bold;\">{0} {1},</td>", strDear, oUser.Name);

        oMailBody.Append(" </tr>");

        //Account Detail
        string strAccountDetail = LanguageUtil.GetValue(2533, oMultiLingualAttributeInfo);
        oMailBody.AppendFormat("<tr><td style=\"font-weight: bold;\">{0}:</td>", strAccountDetail);
        oMailBody.AppendFormat("<td style=\"padding-left: 2px;\">  {0}", AccountDetail);
        oMailBody.Append(" </td>  </tr>");
        oMailBody.Append(" <tr> <td colspan=\"2\"></td></tr>");

        //Rec Item Detail
        string strRecItemDetail = LanguageUtil.GetValue(2751, oMultiLingualAttributeInfo);
        oMailBody.AppendFormat("<tr><td style=\"font-weight: bold;\">{0}:</td>", strRecItemDetail);
        oMailBody.AppendFormat("<td style=\"padding-left: 2px;\">  {0}", RecItemDetail);
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
        //RecItem Comments 
        string strRecItemComments = LanguageUtil.GetValue(2749, oMultiLingualAttributeInfo);
        int i = 0;
        foreach (RecItemCommentInfo oRecItemCommentInfo in RecItemCommentInfoList)
        {
            if (i == 0)
                oMailBody.AppendFormat("<tr><td style=\"font-weight: bold;\">{0}:</td>", strRecItemComments);
            else
                oMailBody.AppendFormat("<tr><td style=\"font-weight: bold;\">{0}</td>", "");
            oMailBody.AppendFormat("<td style=\"padding-left: 2px ; color : Blue ;\">  {0} [{1}]: {2}", oRecItemCommentInfo.AddedByUserInfo.Name, oRecItemCommentInfo.DateAdded, oRecItemCommentInfo.Comment);
            oMailBody.Append(" </td>  </tr>");
            i++;
        }
        oMailBody.Append(" <tr> <td colspan=\"2\"></td></tr>");
        oMailBody.Append(" </table>");
        oMailBody.Append("<br>");
        return oMailBody;
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
    private string GetAccountInfo()
    {
        string AccountInfo = string.Empty;
        IAccount oAccountClient = RemotingHelper.GetAccountObject();
        if (this._AccountID.HasValue && this._AccountID.Value > 0)
        {
            AccountInfo = Helper.GetAccountEntityStringToDisplay(_AccountID.Value);
        }
        else if (this._NetAccountID.HasValue && this._NetAccountID.Value > 0)
        {
            AccountInfo = LanguageUtil.GetValue(1257) + WebConstants.ACCOUNT_ENTITY_SEPARATOR + oAccountClient.GetNetAccountNameByNetAccountID(this._NetAccountID.Value, SessionHelper.CurrentCompanyID.Value
                , SessionHelper.GetUserLanguage(), SessionHelper.GetBusinessEntityID(), AppSettingHelper.GetDefaultLanguageID(), Helper.GetAppUserInfo());
        }
        return AccountInfo;
    }
    #endregion
    #region Other Methods
    #endregion


}
