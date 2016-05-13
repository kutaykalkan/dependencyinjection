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
using SkyStem.ART.Client.Model.RecControlCheckList;

public partial class Pages_EditRecControlCheckListComment : PopupPageBase
{


    #region Variables & Constants
    Int64? _GLDataRecControlCheckListID = null;
    Int64? _GLData_ID = null;
    Int32? _RecControlCheckListID = null;
    WebEnums.FormMode FormMode;
    #endregion
    #region Properties
    List<GLDataRecControlCheckListCommentInfo> _GLDataRecControlCheckListCommentInfoList;
    List<GLDataRecControlCheckListCommentInfo> GLDataRecControlCheckListCommentInfoList
    {
        get
        {
            if (this._GLDataRecControlCheckListCommentInfoList == null)
            {
                if (ViewState[ViewStateConstants.REC_CONTROL_CHECK_LIST_COMMENT_LIST] != null)
                    this._GLDataRecControlCheckListCommentInfoList = (List<GLDataRecControlCheckListCommentInfo>)ViewState[ViewStateConstants.REC_CONTROL_CHECK_LIST_COMMENT_LIST];
            }
            return this._GLDataRecControlCheckListCommentInfoList;
        }
        set
        {
            this._GLDataRecControlCheckListCommentInfoList = value;
            ViewState[ViewStateConstants.REC_CONTROL_CHECK_LIST_COMMENT_LIST] = _GLDataRecControlCheckListCommentInfoList;
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


    long? GLDataRecControlCheckListID
    {
        get
        {
            if (this._GLDataRecControlCheckListID == null)
            {
                if (ViewState["GLDataRecControlCheckListID"] != null)
                    this._GLDataRecControlCheckListID = (long)ViewState["GLDataRecControlCheckListID"];
            }
            return this._GLDataRecControlCheckListID;

        }
        set
        {
            this._GLDataRecControlCheckListID = value;
            ViewState["GLDataRecControlCheckListID"] = _GLDataRecControlCheckListID;
        }
    }
    RecControlCheckListInfo _oRecControlCheckListInfo = null;
    RecControlCheckListInfo oRecControlCheckListInfo
    {
        get
        {
            if (this._oRecControlCheckListInfo == null)
            {
                if (ViewState[ViewStateConstants.REC_CONTROL_CHECK_LIST_INFO] != null)
                    this._oRecControlCheckListInfo = (RecControlCheckListInfo)ViewState[ViewStateConstants.REC_CONTROL_CHECK_LIST_INFO];
            }
            return this._oRecControlCheckListInfo;

        }
        set
        {
            this._oRecControlCheckListInfo = value;
            ViewState[ViewStateConstants.REC_CONTROL_CHECK_LIST_INFO] = _oRecControlCheckListInfo;
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
        PopupHelper.SetPageTitle(this, 2830);
        GetQueryStringValues();
        txtComment.ErrorMessage = Helper.GetErrorMessage(WebEnums.FieldType.MandatoryField, 2830);
        if (!Page.IsPostBack)
        {
            try
            {
                List<RecControlCheckListInfo> ALLRecControlCheckListInfoList = RecControlCheckListHelper.GetRecControlCheckListInfoList(_GLData_ID, SessionHelper.CurrentReconciliationPeriodID);

                //List<RecControlCheckListInfo> ALLRecControlCheckListInfoList = (List<RecControlCheckListInfo>)Session[SessionConstants.REC_CONTROL_CHECK_LIST_GLDATA];
                if (ALLRecControlCheckListInfoList != null)
                    oRecControlCheckListInfo = ALLRecControlCheckListInfoList.Find(o => o.RecControlCheckListID == _RecControlCheckListID);

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
        GetRecControlChecCkListItemDetail();
        if (this.FormMode == WebEnums.FormMode.Edit)
        {
            pnlCommentText.Visible = true;
            btnUpdate.Visible = true;
            pnlEnteredBy.Visible = true;
        }
        else
        {
            pnlCommentText.Visible = false;
            btnUpdate.Visible = false;
            pnlEnteredBy.Visible = false;
        }
    }
    #endregion
    #region Grid Events
    protected void rptComments_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item
            || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            GLDataRecControlCheckListCommentInfo oGLDataRecControlCheckListCommentInfo = (GLDataRecControlCheckListCommentInfo)e.Item.DataItem;
            ExLabel lblUserDetails = (ExLabel)e.Item.FindControl("lblUserDetails");
            ExLabel lblComments = (ExLabel)e.Item.FindControl("lblComments");
            // First Name Last Name [Date Time]: 
            lblUserDetails.Text = string.Format("{0} [{1}]: ", oGLDataRecControlCheckListCommentInfo.AddedByUserName, Helper.GetDisplayDateTime(oGLDataRecControlCheckListCommentInfo.DateAdded));
            lblComments.Text = oGLDataRecControlCheckListCommentInfo.Comments;
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
                GLDataRecControlCheckListCommentInfo oGLDataRecControlCheckListCommentInfo = new GLDataRecControlCheckListCommentInfo();
                oGLDataRecControlCheckListCommentInfo.AddedByUserID = SessionHelper.CurrentUserID;
                oGLDataRecControlCheckListCommentInfo.DateAdded = DateTime.Now;
                oGLDataRecControlCheckListCommentInfo.AddedBy = SessionHelper.CurrentUserLoginID;
                oGLDataRecControlCheckListCommentInfo.Comments = txtComment.Text;
                oGLDataRecControlCheckListCommentInfo.RoleID = SessionHelper.CurrentRoleID;
                IGLDataRecItem oGLDataRecItemClient = RemotingHelper.GetGLDataRecItemObject();
                if (GLDataRecControlCheckListID.HasValue && GLDataRecControlCheckListID > 0)
                {
                    oGLDataRecControlCheckListCommentInfo.GLDataRecControlCheckListID = GLDataRecControlCheckListID;
                    RecControlCheckListHelper.SaveGLDataRecControlCheckListComment(oGLDataRecControlCheckListCommentInfo);
                    LoadData();
                    SendMailToNotifyUsers(oUserHdrInfoCollection);
                    txtComment.Text = "";
                }
                else
                {
                    if (_RecControlCheckListID.HasValue && _RecControlCheckListID.Value > 0 && _GLData_ID.HasValue && _GLData_ID.Value > 0)
                    {
                        GLDataRecControlCheckListInfo oGLDataRecControlCheckListInfo = new GLDataRecControlCheckListInfo();
                        oGLDataRecControlCheckListInfo.RecControlCheckListID = _RecControlCheckListID;
                        oGLDataRecControlCheckListInfo.GLDataID = _GLData_ID;
                        oGLDataRecControlCheckListInfo.IsActive = true;
                        oGLDataRecControlCheckListInfo.DateAdded = DateTime.Now;
                        oGLDataRecControlCheckListInfo.AddedBy = SessionHelper.CurrentUserLoginID;
                        GLDataRecControlCheckListID = RecControlCheckListHelper.SaveGLDataRecControlCheckListAndComment(oGLDataRecControlCheckListInfo, oGLDataRecControlCheckListCommentInfo);
                        LoadData();
                        SendMailToNotifyUsers(oUserHdrInfoCollection);
                        txtComment.Text = "";
                    }


                }
                //***********************************************************************************
                if (!this.Page.ClientScript.IsClientScriptBlockRegistered("RefreshParentPageOnWindowClose"))
                    this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "RefreshParentPageOnWindowClose", ScriptHelper.GetJSForRefreshParentPageOnWindowClose(true));


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
    private string GetRecControlChecCkListItemDetail()
    {
        string CheckListNumber = null;
        if (oRecControlCheckListInfo != null && !string.IsNullOrEmpty(oRecControlCheckListInfo.Description))
        {
            string _displayString = oRecControlCheckListInfo.Description;
            if (oRecControlCheckListInfo.Description.Length > 50)
            {
                _displayString = oRecControlCheckListInfo.Description.Substring(0, 50) + "...";
            }
            lblRecItemDetails.ToolTip = HttpContext.Current.Server.HtmlEncode(oRecControlCheckListInfo.Description);
            lblRecItemDetails.Text = oRecControlCheckListInfo.CheckListNumber + "-" + _displayString;
            CheckListNumber = oRecControlCheckListInfo.CheckListNumber;
        }
        else
        {
            if (oRecControlCheckListInfo != null)
            {
                lblRecItemDetails.Text = oRecControlCheckListInfo.CheckListNumber;
                CheckListNumber = oRecControlCheckListInfo.CheckListNumber;
            }
        }

        return CheckListNumber;
    }
    private string GetRecControlChecCkListItemAllDetail()
    {
        string ReturnVal = string.Empty;
        if (oRecControlCheckListInfo != null && string.IsNullOrEmpty(oRecControlCheckListInfo.Description))
            ReturnVal = oRecControlCheckListInfo.CheckListNumber;
        else
        {
            if (oRecControlCheckListInfo != null)
                ReturnVal = oRecControlCheckListInfo.CheckListNumber + "-" + oRecControlCheckListInfo.Description;
        }
        return ReturnVal;
    }
    private void LoadData()
    {
        lblRecPeriodValue.Text = Helper.GetDisplayDate(SessionHelper.CurrentReconciliationPeriodEndDate);
        lblEnteredByValue.Text = Helper.GetUserFullName();
        lblDateAddedValue.Text = Helper.GetDisplayDate(DateTime.Now);
        // Fetch the RecItem Comments
        if (GLDataRecControlCheckListID != null)
        {
            // Means Case of Edit / View            
            GLDataRecControlCheckListCommentInfoList = RecControlCheckListHelper.GetGLDataRecControlCheckListCommentInfoList(GLDataRecControlCheckListID);

            if (GLDataRecControlCheckListCommentInfoList != null && GLDataRecControlCheckListCommentInfoList.Count > 0)
            {
                rptComments.DataSource = GLDataRecControlCheckListCommentInfoList;
                rptComments.DataBind();
            }
        }

    }

    private void GetNotifyUser()
    {
        if (oUserHdrInfoCollection == null)
            oUserHdrInfoCollection = new List<UserHdrInfo>();
        oUserHdrInfoCollection.Add(SessionHelper.GetCurrentUser());
    }
    private void GetQueryStringValues()
    {
        // Fetch Values from QueryString
        if (!string.IsNullOrEmpty(Request.QueryString[QueryStringConstants.GLDATA_ID]))
            this._GLData_ID = Convert.ToInt64(Request.QueryString[QueryStringConstants.GLDATA_ID]);
        if (!Page.IsPostBack && GLDataRecControlCheckListID == null && !string.IsNullOrEmpty(Request.QueryString[QueryStringConstants.GLDATA_REC_CONTROL_CHECKLIST_ID]))
            this.GLDataRecControlCheckListID = Convert.ToInt64(Request.QueryString[QueryStringConstants.GLDATA_REC_CONTROL_CHECKLIST_ID]);
        if (!string.IsNullOrEmpty(Request.QueryString[QueryStringConstants.REC_CONTROL_CHECK_LIST_ID]))
            this._RecControlCheckListID = Convert.ToInt32(Request.QueryString[QueryStringConstants.REC_CONTROL_CHECK_LIST_ID]);
        if (!string.IsNullOrEmpty(Request.QueryString[QueryStringConstants.MODE]))
            this.FormMode = (WebEnums.FormMode)System.Enum.Parse(typeof(WebEnums.FormMode), Request.QueryString[QueryStringConstants.MODE]);
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
            mailSubject = string.Format(LanguageUtil.GetValue(2831), GetRecControlChecCkListItemDetail());
            string strRecControlChecCkListItemDetail = GetRecControlChecCkListItemAllDetail();
            //foreach (UserHdrInfo oUser in oNotifyUserList)
            //{
            UserHdrInfo oUser = SessionHelper.GetCurrentUser();
            //    //Get common email HTML body
            oCommonHTMLMailBody = GetCommonMailbodyhtml(strRecControlChecCkListItemDetail, oUser);
            //Append signature
            oCommonHTMLMailBody.Append(GetEmailSignature(WebEnums.SignatureEnum.SendBySystemAdmin, oUser.Name, fromAddress));
            try
            {
                //Send mail
                MailHelper.SendEmail(fromAddress, oUser.EmailID, mailSubject, oCommonHTMLMailBody.ToString());
            }
            catch (Exception ex)
            {
                Helper.FormatAndShowErrorMessage(null, ex);
            }
            //}
        }
        catch (Exception ex)
        {
            Helper.FormatAndShowErrorMessage(null, ex);
        }
    }
    private StringBuilder GetCommonMailbodyhtml(string strRecControlChecCkListItemDetail, UserHdrInfo oUser)
    {
        StringBuilder oMailBody = new StringBuilder();
        SkyStem.Language.LanguageUtility.Classes.MultilingualAttributeInfo oMultiLingualAttributeInfo = LanguageHelper.GetMultilingualAttributeInfo(SessionHelper.CurrentCompanyID, oUser.DefaultLanguageID);

        string strDear = LanguageUtil.GetValue(1845);
        //Dear
        oMailBody.Append("<table width=\"100%\" cellpadding=\"0\" cellspacing=\"0\">");
        oMailBody.AppendFormat("<tr><td colspan=\"2\" style=\"font-weight: bold;\">{0} {1},</td>", strDear, oUser.Name);

        oMailBody.Append(" </tr>");



        //Rec control checklist  Item Detail
        string strRCCtemDetail = LanguageUtil.GetValue(2832, oMultiLingualAttributeInfo);
        oMailBody.AppendFormat("<tr><td style=\"font-weight: bold;\">{0}:</td>", strRCCtemDetail);
        oMailBody.AppendFormat("<td style=\"padding-left: 2px;\">  {0}", strRecControlChecCkListItemDetail);
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
        string strRecItemComments = LanguageUtil.GetValue(2830, oMultiLingualAttributeInfo);
        int i = 0;
        foreach (GLDataRecControlCheckListCommentInfo oGLDataRecControlCheckListCommentInfo in GLDataRecControlCheckListCommentInfoList)
        {
            if (i == 0)
                oMailBody.AppendFormat("<tr><td style=\"font-weight: bold;\">{0}:</td>", strRecItemComments);
            else
                oMailBody.AppendFormat("<tr><td style=\"font-weight: bold;\">{0}</td>", "");
            oMailBody.AppendFormat("<td style=\"padding-left: 2px ; color : Blue ;\">  {0} [{1}]: {2}", oGLDataRecControlCheckListCommentInfo.AddedByUserName, oGLDataRecControlCheckListCommentInfo.DateAdded, oGLDataRecControlCheckListCommentInfo.Comments);
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
    #endregion
    #region Other Methods
    #endregion


}
