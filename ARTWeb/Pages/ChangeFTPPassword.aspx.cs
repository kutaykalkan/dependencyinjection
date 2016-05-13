using ART.Integration.Utility;
using SkyStem.ART.Client.Exception;
using SkyStem.ART.Client.IServices;
using SkyStem.ART.Client.Model;
using SkyStem.ART.Shared.Data;
using SkyStem.ART.Shared.Utility;
using SkyStem.ART.Web.Classes;
using SkyStem.ART.Web.Data;
using SkyStem.ART.Web.Utility;
using SkyStem.Language.LanguageUtility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_ChangeFTPPassword : PageBase
{

    #region Variables & Constants
    #endregion

    #region Properties
    #endregion

    #region Delegates & Events
    #endregion

    #region Page Events
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            Helper.ShowInputRequirementSection(this, 1202, 1265);
            SetErrorMessages();
        }
        Helper.SetPageTitle(this, 2917);
        Page.SetFocus(txtOldPassword);
    }
    #endregion

    #region Grid Events
    #endregion

    #region Other Events
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            UserHdrInfo oUserHdrInfo = SessionHelper.GetCurrentUser();
            string loginID = oUserHdrInfo.LoginID;
            string ftpLoginID = oUserHdrInfo.FTPLoginID;
            int userID = oUserHdrInfo.UserID.Value;

            string newPassword= txtNewPassword.Text.Trim();
            string oldPassword = txtOldPassword.Text.Trim();
            string newPasswordHash = Helper.GetHashedPassword(newPassword);
            string oldPasswordHash = Helper.GetHashedPassword(oldPassword);
            try
            {
                IUser oUserClient = RemotingHelper.GetUserObject();
                IntegrationUtil.ChangePassword(SharedDataImportHelper.GetFTPLoginID(ftpLoginID), oldPassword, newPassword);
                oUserClient.VerifyAndUpdateFTPPassword(userID, loginID, ftpLoginID, oldPasswordHash, newPasswordHash, Helper.GetAppUserInfo());
                Helper.RedirectToHomePage(2918);
            }
            catch (ARTException ex)
            {
                Helper.ShowErrorMessage(this, ex);
            }
            catch (Exception ex)
            {
                Helper.ShowErrorMessage(this, ex);
            }
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Helper.RedirectToHomePage();
    }
    #endregion

    #region Validation Control Events
    protected void cvNewPassword_ServerValidate(object source, ServerValidateEventArgs args)
    {
        if (txtNewPassword.Text.Trim() == txtOldPassword.Text.Trim())
        {
            args.IsValid = false;
        }
    }
    protected void cvNewPassordFormat_ServerValidate(object source, ServerValidateEventArgs args)
    {
        if (!SharedHelper.IsPassowrdValidByPolicy(txtNewPassword.Text.Trim(), SharedConstants.LENGTH_GENERATED_PASSWORD, SessionHelper.CurrentUserLoginID))
        {
            args.IsValid = false;
        }
    }

    #endregion

    #region Private Methods
    private void SetErrorMessages()
    {
        // Set Error Messages
        rfvOldPassword.ErrorMessage = Helper.GetErrorMessage(WebEnums.FieldType.MandatoryField, 1235);
        rfvNewPassword.ErrorMessage = Helper.GetErrorMessage(WebEnums.FieldType.MandatoryField, 1236);
        rfvConfirmPassword.ErrorMessage = Helper.GetErrorMessage(WebEnums.FieldType.MandatoryField, 1237);
        revPassword.ErrorMessage = string.Format(LanguageUtil.GetValue(1006), LanguageUtil.GetValue(1236));
        revOldPassword.ErrorMessage = string.Format(LanguageUtil.GetValue(1006), LanguageUtil.GetValue(1235));
        cvNewPassword.ErrorMessage = LanguageUtil.GetValue(5000374);
        cvNewPassordFormat.ErrorMessage = string.Format(LanguageUtil.GetValue(1006), LanguageUtil.GetValue(1236));
    }
    #endregion

    #region Other Methods
    public override string GetMenuKey()
    {
        return "ChangeFTPPassword";
    }
    #endregion

  
}