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
using SkyStem.ART.Web.Utility;
using SkyStem.ART.Web.Data;
using SkyStem.Language.LanguageUtility;
using SkyStem.ART.Client.IServices;
using SkyStem.ART.Client.Model;
using System.Collections.Generic;
using SkyStem.ART.Client.Data;
using SkyStem.ART.Web.Classes;
using SkyStem.ART.Client.Model.RecControlCheckList;


public partial class Pages_AddRecControlCheckList : PopupPageBaseRecItem
{
    #region Variables & Constants
        WebEnums.FormMode FormMode;
    #endregion

    #region Properties
    #endregion

    #region Delegates & Events
    #endregion

    #region Page Events
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                PopupHelper.SetPageTitle(this, 2834);
                GetQueryStringValues();
                SetErrorMessagesForValidationControls();
                if (!IsPostBack)
                {
                    txtDescription.ErrorMessage = Helper.GetErrorMessage(WebEnums.FieldType.MandatoryField, 1408);
                    PopulateItemsOnPage();
                }


                this.lblInputFormRecPeriodValue.Text = Helper.GetDisplayDate(SessionHelper.CurrentReconciliationPeriodEndDate);

                ucAccountHierarchyDetailPopup.AccountID = this.AccountID.Value;
                this.ucAccountHierarchyDetailPopup.NetAccountID = this.NetAccountID;
                if (this.FormMode == WebEnums.FormMode.Edit)
                {
                    btnUpdate.Visible = true;
                }
                else
                {
                    btnUpdate.Visible = false;
                }
            }
            catch (Exception ex)
            {
                PopupHelper.ShowErrorMessage(this, ex);
            }
        }
    #endregion

    #region Grid Events
    #endregion

    #region Other Events
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (Page.IsValid)
                {
                    if (!string.IsNullOrEmpty(txtDescription.Text))
                    {
                        List<RecControlCheckListInfo> oRecControlCheckListInfoList = new List<RecControlCheckListInfo>();
                        RecControlCheckListInfo oRecControlCheckListInfo = new RecControlCheckListInfo();
                        oRecControlCheckListInfo.Description = txtDescription.Text;
                        oRecControlCheckListInfo.DescriptionLabelID = (int)LanguageUtil.InsertPhrase(txtDescription.Text, null, 1, (int)SessionHelper.CurrentCompanyID, SessionHelper.GetUserLanguage(), 4, null);
                        oRecControlCheckListInfo.CompanyID = SessionHelper.CurrentCompanyID;
                        oRecControlCheckListInfo.AddedBy = SessionHelper.CurrentUserLoginID;
                        oRecControlCheckListInfo.DateAdded = DateTime.Now;
                        oRecControlCheckListInfo.AddedByUserID = SessionHelper.CurrentUserID;
                        oRecControlCheckListInfo.RoleID = SessionHelper.CurrentRoleID;
                        oRecControlCheckListInfo.StartRecPeriodID = SessionHelper.CurrentReconciliationPeriodID;
                        oRecControlCheckListInfo.EndRecPeriodID = null;
                        oRecControlCheckListInfo.IsActive = true;
                        oRecControlCheckListInfo.RowNumber = 1;
                        oRecControlCheckListInfoList.Add(oRecControlCheckListInfo);

                        List<RecControlCheckListAccountInfo> oRecControlCheckListAccountInfoList = new List<RecControlCheckListAccountInfo>();
                        RecControlCheckListAccountInfo oRecControlCheckListAccountInfo = new RecControlCheckListAccountInfo();
                        oRecControlCheckListAccountInfo.AccountID = this.AccountID;
                        oRecControlCheckListAccountInfo.NetAccountID = this.NetAccountID;
                        oRecControlCheckListAccountInfo.AddedBy = SessionHelper.CurrentUserLoginID;
                        oRecControlCheckListAccountInfo.DateAdded = DateTime.Now;
                        oRecControlCheckListAccountInfo.AddedByUserID = SessionHelper.CurrentUserID;
                        oRecControlCheckListAccountInfo.RoleID = SessionHelper.CurrentRoleID;
                        oRecControlCheckListAccountInfo.StartRecPeriodID = SessionHelper.CurrentReconciliationPeriodID;
                        oRecControlCheckListAccountInfo.EndRecPeriodID = null;
                        oRecControlCheckListAccountInfo.IsActive = true;
                        oRecControlCheckListAccountInfo.RowNumber = 1;
                        oRecControlCheckListAccountInfoList.Add(oRecControlCheckListAccountInfo);
                        RecControlCheckListHelper.InsertAccountRecControlChecklist(oRecControlCheckListInfoList, oRecControlCheckListAccountInfoList, this.GLDataID);

                    }
                    if (this.ParentHiddenField != null)
                    {
                        this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "SetHiddenFieldStatus", ScriptHelper.GetJSToSetParentWindowElementValue(this.ParentHiddenField, "1")); // 1 means Reload data of GridVieww
                    }
                    this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "ClosePopupAndRefreshParentPage", ScriptHelper.GetJSForClosePopupAndSubmitParentPage());
                }
                else
                {
                    Page.Validate();
                }
            }
            catch (Exception ex)
            {
                PopupHelper.ShowErrorMessage(this, ex);
            }
        }
    #endregion

    #region Validation Control Events
    #endregion

    #region Private Methods
        private void SetErrorMessagesForValidationControls()
        {
            //rfvAmount.ErrorMessage = Helper.GetErrorMessage(WebEnums.FieldType.MandatoryField, 1408);
        }
        private void PopulateItemsOnPage()
        {
            UserHdrInfo oUserHdrInfo = SessionHelper.GetCurrentUser();
            if (oUserHdrInfo != null)
                lblEnteredByValue.Text = Helper.GetDisplayStringValue(oUserHdrInfo.Name);
            lblAddedDate.Text = Helper.GetDisplayDate(System.DateTime.Now);
        }
        private void GetQueryStringValues()
        {
            if (!string.IsNullOrEmpty(Request.QueryString[QueryStringConstants.MODE]))
                this.FormMode = (WebEnums.FormMode)System.Enum.Parse(typeof(WebEnums.FormMode), Request.QueryString[QueryStringConstants.MODE]);
        }
    #endregion

    #region Other Methods
    #endregion

}
