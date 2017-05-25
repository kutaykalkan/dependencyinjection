using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using SkyStem.ART.Web.Utility;
using SkyStem.ART.Web.Data;
using SkyStem.ART.Client.IServices;
using SkyStem.ART.Client.Model;
using SkyStem.ART.Web.Classes;
using SkyStem.ART.Client.Exception;
using SkyStem.Language.LanguageUtility;
using SkyStem.ART.Shared.Utility;
using SkyStem.ART.Shared.Data;
using SkyStem.ART.Client.Data;

public partial class ChangePassword : PageBase
{
    #region Variables & Constants
    #endregion

    #region Properties
    #endregion

    #region Delegates & Events
    #endregion

    #region Page Events
    protected void Page_PreInit(object sender, EventArgs e)
    {
        this.ARTPageID = WebEnums.ARTPages.ChangePassword;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        Helper.ShowInputRequirementSection(this, 1202, 1265);
        if (!Page.IsPostBack)
        {
            SetErrorMessages();
            UserHdrInfo oUserHdrInfo = SessionHelper.GetCurrentUser();
            if (oUserHdrInfo.IsPasswordResetRequired.GetValueOrDefault())
            {
                SetPageSettings();
                Helper.ShowConfirmationMessage(this, string.Format(LanguageUtil.GetValue(5000375), oUserHdrInfo.PasswordResetDays));
            }
        }
        Helper.SetPageTitle(this, 1078);
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
            int userID = oUserHdrInfo.UserID.Value;

            string newPasswordHash = Helper.GetHashedPassword(txtNewPassword.Text);
            string oldPasswordHash = Helper.GetHashedPassword(txtOldPassword.Text);

            try
            {
                IUser oUserClient = RemotingHelper.GetUserObject();
                AppUserInfo oAppUserInfo = Helper.GetAppUserInfo();
                if (SessionHelper.CurrentRoleEnum == ARTEnums.UserRole.SKYSTEM_ADMIN)
                    oAppUserInfo.CompanyID = null;
                oUserClient.VerifyAndUpdatePassword(userID, loginID, oldPasswordHash, newPasswordHash, oAppUserInfo);
                oUserHdrInfo.IsPasswordResetRequired = false;
                Helper.RedirectToHomePage(1264);
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
        revOldPassword.ErrorMessage = string.Format(LanguageUtil.GetValue(1006), LanguageUtil.GetValue(1235));
        revPassword.ErrorMessage = string.Format(LanguageUtil.GetValue(1006), LanguageUtil.GetValue(1236));
        //revConfirmPassword.ErrorMessage = string.Format(LanguageUtil.GetValue(1006), LanguageUtil.GetValue(1237));
        cvNewPassword.ErrorMessage = LanguageUtil.GetValue(5000374);
        cvNewPassordFormat.ErrorMessage = string.Format(LanguageUtil.GetValue(1006), LanguageUtil.GetValue(1236));
    }
    /// <summary>
    /// Sets the page settings.
    /// </summary>
    private void SetPageSettings()
    {
        MasterPageBase oMasterPageBase = (MasterPageBase)this.Master;
        MasterPageSettings oMasterPageSettings = new MasterPageSettings();
        oMasterPageSettings.EnableRecPeriodSelection = false;
        oMasterPageSettings.EnableRoleSelection = false;
        oMasterPageSettings.HideMenu = true;
        oMasterPageSettings.HideToolBar = true;
        oMasterPageSettings.HidePanelLockdownDays = true;
        oMasterPageSettings.HideRecPeriodBar = true;
        oMasterPageBase.SetMasterPageSettings(oMasterPageSettings);
    }
    #endregion

    #region Other Methods
    public override string GetMenuKey()
    {
        return "ChangePassword";
    }
    #endregion

}
