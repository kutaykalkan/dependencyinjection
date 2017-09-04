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

using System.Collections.Generic;
using SkyStem.ART.Client.Model;
using SkyStem.ART.Client.IServices;
using SkyStem.ART.Web.Utility;
using SkyStem.ART.Web.Data;
using SkyStem.ART.Client.Exception;
using SkyStem.ART.Web.Classes;
using SkyStem.Language.LanguageUtility;
using Telerik.Web.UI;
using SkyStem.ART.Client.Data;
using SkyStem.ART.Client.Params;

public partial class Pages_AccountProfileAttributeUpdatePopup : PopupPageBase
{
    #region Variables & Constants
    long _AccountID = 0;
    int _NetAccountID = 0;
    #endregion

    #region Properties
    AccountHdrInfo _AccountHdrInfo = null;
    //for net account
    NetAccountHdrInfo objNetAccountHdrInfo = null;
    #endregion

    #region Delegates & Events
    #endregion

    #region Page Events
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request.QueryString[QueryStringConstants.ACCOUNT_ID]))
            this._AccountID = Convert.ToInt32(Request.QueryString[QueryStringConstants.ACCOUNT_ID]);

        if (!string.IsNullOrEmpty(Request.QueryString[QueryStringConstants.NETACCOUNT_ID]))
            this._NetAccountID = Convert.ToInt32(Request.QueryString[QueryStringConstants.NETACCOUNT_ID]);
        if (this._AccountID > 0)
        {
            IAccount oAccountClient = RemotingHelper.GetAccountObject();
            this._AccountHdrInfo = oAccountClient.GetAccountHdrInfoByAccountID(this._AccountID, SessionHelper.CurrentCompanyID.Value, SessionHelper.CurrentReconciliationPeriodID.Value, Helper.GetAppUserInfo());
            List<AccountHdrInfo> oAccountHdrInfoCollection = new List<AccountHdrInfo>();
            oAccountHdrInfoCollection.Add(this._AccountHdrInfo);
            oAccountHdrInfoCollection = LanguageHelper.TranslateLabelsAccountHdr(oAccountHdrInfoCollection);
        }
        if (!Page.IsPostBack)
        {
            if (this._AccountID > 0)
            {
                PopulateItemsOnPage();
                DisableControlsForCurrentRecPeriodStatus();
            }
            else
            {
                LoadNetAccount();
                DisableControlsForCurrentRecPeriodStatusNetAccount();
            }

        }
    }
    #endregion

    #region Grid Events
    #endregion

    #region Other Events
    protected void btnSave_OnClick(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                if (this._AccountID > 0)
                {
                    AccountHdrInfo oAccountHdrInfo = this.GetAccountInformation();

                    List<AccountHdrInfo> oAccountHdrInfoCollection = new List<AccountHdrInfo>();
                    oAccountHdrInfoCollection.Add(oAccountHdrInfo);

                    IAccount oAccountClient = RemotingHelper.GetAccountObject();
                    bool result = oAccountClient.SaveAccountProfile(oAccountHdrInfoCollection, SessionHelper.CurrentCompanyID.Value, SessionHelper.CurrentReconciliationPeriodID.Value, SessionHelper.GetCurrentUser().LoginID, DateTime.Now, (short)ARTEnums.ActionType.AccountAttributeChangeFromUI, Helper.GetAppUserInfo());
                }
                else
                {
                    SaveNetAccount();
                }
                this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "ClosePopupAndRefreshParentPage", ScriptHelper.GetJSForClosePopupAndSubmitParentPage());
            }
            else
            {
                Page.Validate();
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
    #endregion

    #region Validation Control Events
    #endregion

    #region Private Methods
    private AccountHdrInfo GetAccountInformation()
    {
        int? lableID = null;
        AccountHdrInfo oAccountHdrInfo = this._AccountHdrInfo;

        oAccountHdrInfo.AccountID = this._AccountID;
        oAccountHdrInfo.AccountPolicyUrl = this.ucAccountPolicyURL.EditorControl.Content;
        oAccountHdrInfo.Description = this.ucDescription.EditorControl.Content;
        oAccountHdrInfo.ReconciliationProcedure = this.ucReconciliationProcedure.EditorControl.Content;

        // Check if either labelid is 0 or label text changed from earlier value than create new label instead of modify older one
        lableID = Convert.ToInt32(ucAccountPolicyURL.EditorControl.Attributes[WebConstants.ATTRIBUTE_LABEL_ID]);
        if (lableID != null && lableID.HasValue && !LanguageUtil.GetValue(lableID.Value).Equals(this.ucAccountPolicyURL.EditorControl.Content, StringComparison.InvariantCultureIgnoreCase))
            lableID = 0;
        oAccountHdrInfo.AccountPolicyUrlLabelID = LanguageUtil.InsertPhrase(oAccountHdrInfo.AccountPolicyUrl, null, AppSettingHelper.GetApplicationID(), SessionHelper.CurrentCompanyID.Value, SessionHelper.GetUserLanguage(), 4, lableID);

        lableID = Convert.ToInt32(ucDescription.EditorControl.Attributes[WebConstants.ATTRIBUTE_LABEL_ID]);
        if (lableID != null && lableID.HasValue && !LanguageUtil.GetValue(lableID.Value).Equals(this.ucDescription.EditorControl.Content, StringComparison.InvariantCultureIgnoreCase))
            lableID = 0;
        oAccountHdrInfo.DescriptionLabelID = LanguageUtil.InsertPhrase(oAccountHdrInfo.Description, null, AppSettingHelper.GetApplicationID(), SessionHelper.CurrentCompanyID.Value, SessionHelper.GetUserLanguage(), 4, lableID);

        lableID = Convert.ToInt32(ucReconciliationProcedure.EditorControl.Attributes[WebConstants.ATTRIBUTE_LABEL_ID]);
        if (lableID != null && lableID.HasValue && !LanguageUtil.GetValue(lableID.Value).Equals(this.ucReconciliationProcedure.EditorControl.Content, StringComparison.InvariantCultureIgnoreCase))
            lableID = 0;
        oAccountHdrInfo.ReconciliationProcedureLabelID = LanguageUtil.InsertPhrase(oAccountHdrInfo.ReconciliationProcedure, null, AppSettingHelper.GetApplicationID(), SessionHelper.CurrentCompanyID.Value, SessionHelper.GetUserLanguage(), 4, lableID);

        return oAccountHdrInfo;
    }



    private void PopulateItemsOnPage()
    {
        try
        {
            ucDescription.EditorControl.Content = _AccountHdrInfo.Description;
            if (_AccountHdrInfo.DescriptionLabelID != null)
            {
                ucDescription.EditorControl.Attributes.Add(WebConstants.ATTRIBUTE_LABEL_ID, _AccountHdrInfo.DescriptionLabelID.Value.ToString());
            }

            ucAccountPolicyURL.EditorControl.Content = _AccountHdrInfo.AccountPolicyUrl;
            if (_AccountHdrInfo.AccountPolicyUrlLabelID != null)
            {
                ucAccountPolicyURL.EditorControl.Attributes.Add(WebConstants.ATTRIBUTE_LABEL_ID, _AccountHdrInfo.AccountPolicyUrlLabelID.Value.ToString());
            }

            ucReconciliationProcedure.EditorControl.Content = _AccountHdrInfo.ReconciliationProcedure;
            if (_AccountHdrInfo.ReconciliationProcedureLabelID != null)
            {
                ucReconciliationProcedure.EditorControl.Attributes.Add(WebConstants.ATTRIBUTE_LABEL_ID, _AccountHdrInfo.ReconciliationProcedureLabelID.Value.ToString());
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


    private void DisableControlsForCurrentRecPeriodStatus()
    {
        bool viewMode = false;
        if (_AccountHdrInfo != null)
            viewMode = true;

        bool editMode = false;
        // Not Locked, Is Null (GL not available) or In Not Started State
        if (viewMode && !_AccountHdrInfo.IsLocked.GetValueOrDefault()
                && (SessionHelper.CurrentRoleEnum == ARTEnums.UserRole.PREPARER
                    || (SessionHelper.CurrentRoleEnum == ARTEnums.UserRole.BACKUP_PREPARER
                        && Helper.IsFeatureActivated(WebEnums.Feature.AccountOwnershipBackup, SessionHelper.CurrentReconciliationPeriodID)))
                && (!_AccountHdrInfo.ReconciliationStatusID.HasValue
                    || _AccountHdrInfo.ReconciliationStatusID.GetValueOrDefault() == (short)WebEnums.ReconciliationStatus.NotStarted
                    || _AccountHdrInfo.ReconciliationStatusID.GetValueOrDefault() == (short)WebEnums.ReconciliationStatus.InProgress
                    || _AccountHdrInfo.ReconciliationStatusID.GetValueOrDefault() == (short)WebEnums.ReconciliationStatus.Prepared
                    || _AccountHdrInfo.ReconciliationStatusID.GetValueOrDefault() == (short)WebEnums.ReconciliationStatus.PendingModificationPreparer)
             )
            editMode = true;

        bool periodLocked = false;
        if (CertificationHelper.IsCertificationStarted()
            || (CurrentRecProcessStatus.Value != WebEnums.RecPeriodStatus.NotStarted
            && CurrentRecProcessStatus.Value != WebEnums.RecPeriodStatus.Open
            && CurrentRecProcessStatus.Value != WebEnums.RecPeriodStatus.InProgress))
            periodLocked = true;

        btnSave.Visible = (!periodLocked && editMode);
        if (!periodLocked && editMode)
        {
            ucAccountPolicyURL.EditorControl.EditModes = EditModes.Design;
            ucDescription.EditorControl.EditModes = EditModes.Design;
            ucReconciliationProcedure.EditorControl.EditModes = EditModes.Design;
        }
        else
        {
            ucAccountPolicyURL.EditorControl.EditModes = EditModes.Preview;
            ucDescription.EditorControl.EditModes = EditModes.Preview;
            ucReconciliationProcedure.EditorControl.EditModes = EditModes.Preview;
        }
    }

    private List<NetAccountAttributeValueInfo> GetNetAccountAttributeValueInfoCollection()
    {
        int? lableID = null;
        UserHdrInfo objUserHdrInfo = SessionHelper.GetCurrentUser();
        NetAccountAttributeValueInfo oNetAccountAttributeValueInfo;
        // add NetAccountAttribute list here
        List<NetAccountAttributeValueInfo> lstNetAccountAttributeValueInfo = new List<NetAccountAttributeValueInfo>();
        //add AccountPolicyURL attribute to list
        oNetAccountAttributeValueInfo = new NetAccountAttributeValueInfo();
        oNetAccountAttributeValueInfo.AccountAttributeID = (int)ARTEnums.AccountAttribute.AccountPolicyURL;
        oNetAccountAttributeValueInfo.Value = ucAccountPolicyURL.EditorControl.Text;
        //?? check what it return in case phrase already exist.
        // Check if either labelid is 0 or label text changed from earlier value than create new label instead of modify older one
        lableID = Convert.ToInt32(ucAccountPolicyURL.EditorControl.Attributes[WebConstants.ATTRIBUTE_LABEL_ID]);
        if (lableID != null && lableID.HasValue && !LanguageUtil.GetValue(lableID.Value).Equals(this.ucAccountPolicyURL.EditorControl.Content, StringComparison.InvariantCultureIgnoreCase))
            lableID = 0;
        oNetAccountAttributeValueInfo.ValueLabelID = (int)LanguageUtil.InsertPhrase(ucAccountPolicyURL.EditorControl.Text, null, AppSettingHelper.GetApplicationID(), (int)SessionHelper.CurrentCompanyID, SessionHelper.GetUserLanguage(), 4, lableID);

        lstNetAccountAttributeValueInfo.Add(oNetAccountAttributeValueInfo);

        //add ReconciliationProcedure attribute to list
        oNetAccountAttributeValueInfo = new NetAccountAttributeValueInfo();
        oNetAccountAttributeValueInfo.AccountAttributeID = (int)ARTEnums.AccountAttribute.ReconciliationProcedure;
        oNetAccountAttributeValueInfo.Value = ucReconciliationProcedure.EditorControl.Text;

        lableID = Convert.ToInt32(ucReconciliationProcedure.EditorControl.Attributes[WebConstants.ATTRIBUTE_LABEL_ID]);
        if (lableID != null && lableID.HasValue && !LanguageUtil.GetValue(lableID.Value).Equals(this.ucReconciliationProcedure.EditorControl.Content, StringComparison.InvariantCultureIgnoreCase))
            lableID = 0;
        oNetAccountAttributeValueInfo.ValueLabelID = (int)LanguageUtil.InsertPhrase(ucReconciliationProcedure.EditorControl.Text, null, AppSettingHelper.GetApplicationID(), (int)SessionHelper.CurrentCompanyID, SessionHelper.GetUserLanguage(), 4, lableID);

        lstNetAccountAttributeValueInfo.Add(oNetAccountAttributeValueInfo);

        //add Description attribute to list
        oNetAccountAttributeValueInfo = new NetAccountAttributeValueInfo();
        oNetAccountAttributeValueInfo.AccountAttributeID = (int)ARTEnums.AccountAttribute.Description;
        oNetAccountAttributeValueInfo.Value = ucDescription.EditorControl.Text;

        lableID = Convert.ToInt32(ucDescription.EditorControl.Attributes[WebConstants.ATTRIBUTE_LABEL_ID]);
        if (lableID != null && lableID.HasValue && !LanguageUtil.GetValue(lableID.Value).Equals(this.ucDescription.EditorControl.Content, StringComparison.InvariantCultureIgnoreCase))
            lableID = 0;
        oNetAccountAttributeValueInfo.ValueLabelID = (int)LanguageUtil.InsertPhrase(ucDescription.EditorControl.Text, null, AppSettingHelper.GetApplicationID(), (int)SessionHelper.CurrentCompanyID, SessionHelper.GetUserLanguage(), 4, lableID);

        lstNetAccountAttributeValueInfo.Add(oNetAccountAttributeValueInfo);

        //add IsActive attribute to list
        oNetAccountAttributeValueInfo = new NetAccountAttributeValueInfo();
        oNetAccountAttributeValueInfo.AccountAttributeID = (int)ARTEnums.AccountAttribute.IsActive;
        oNetAccountAttributeValueInfo.Value = "1";

        lstNetAccountAttributeValueInfo.Add(oNetAccountAttributeValueInfo);

        return lstNetAccountAttributeValueInfo;
    }
    private void LoadNetAccount()
    {
        LoadNetAccountHdrInfoList();
        if (SelectedNetAccountHdrInfo != null)
        {
            this.LoadNetAccountAttributesValue();
            AccountHdrInfoListAdded = NetAccountHelper.GetNetAccountAssociatedAccounts(Convert.ToInt32(this._NetAccountID), (int)SessionHelper.CurrentReconciliationPeriodID);

        }
    }
    private void SaveNetAccount()
    {
        try
        {
            objNetAccountHdrInfo = SelectedNetAccountHdrInfo;
            objNetAccountHdrInfo.NetAccountAttributeValueInfoList = this.GetNetAccountAttributeValueInfoCollection();

            int? iNetAccountId = NetAccountHelper.UpdateNetAccount(objNetAccountHdrInfo, AccountHdrInfoListAdded, (int)SessionHelper.CurrentCompanyID, (int)SessionHelper.CurrentReconciliationPeriodID, (DateTime)SessionHelper.CurrentReconciliationPeriodEndDate, SessionHelper.CurrentUserLoginID);
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
    private void DisableControlsForCurrentRecPeriodStatusNetAccount()
    {
        bool viewMode = false;
        if (SelectedNetAccountHdrInfo != null)
            viewMode = true;

        bool editMode = false;
        // Not Locked, Is Null (GL not available) or In Not Started State
        if (viewMode && !SelectedNetAccountHdrInfo.IsLocked.GetValueOrDefault()
                && (SessionHelper.CurrentRoleEnum == ARTEnums.UserRole.PREPARER
                    || (SessionHelper.CurrentRoleEnum == ARTEnums.UserRole.BACKUP_PREPARER
                        && Helper.IsFeatureActivated(WebEnums.Feature.AccountOwnershipBackup, SessionHelper.CurrentReconciliationPeriodID)))
                && (!SelectedNetAccountHdrInfo.ReconciliationStatusID.HasValue
                    || SelectedNetAccountHdrInfo.ReconciliationStatusID.GetValueOrDefault() == (short)WebEnums.ReconciliationStatus.NotStarted
                    || SelectedNetAccountHdrInfo.ReconciliationStatusID.GetValueOrDefault() == (short)WebEnums.ReconciliationStatus.InProgress
                    || SelectedNetAccountHdrInfo.ReconciliationStatusID.GetValueOrDefault() == (short)WebEnums.ReconciliationStatus.Prepared
                    || SelectedNetAccountHdrInfo.ReconciliationStatusID.GetValueOrDefault() == (short)WebEnums.ReconciliationStatus.PendingModificationPreparer)
             )
            //&& SelectedNetAccountHdrInfo.AddedBy == SessionHelper.CurrentUserLoginID)
            editMode = true;

        bool periodLocked = false;
        if (CertificationHelper.IsCertificationStarted()
            || (CurrentRecProcessStatus.Value != WebEnums.RecPeriodStatus.NotStarted
            && CurrentRecProcessStatus.Value != WebEnums.RecPeriodStatus.Open
            && CurrentRecProcessStatus.Value != WebEnums.RecPeriodStatus.InProgress))
            periodLocked = true;

        btnSave.Visible = (!periodLocked && editMode);
        if (!periodLocked && editMode)
        {
            ucAccountPolicyURL.EditorControl.EditModes = EditModes.Design;
            ucDescription.EditorControl.EditModes = EditModes.Design;
            ucReconciliationProcedure.EditorControl.EditModes = EditModes.Design;
        }
        else
        {
            ucAccountPolicyURL.EditorControl.EditModes = EditModes.Preview;
            ucDescription.EditorControl.EditModes = EditModes.Preview;
            ucReconciliationProcedure.EditorControl.EditModes = EditModes.Preview;
        }
    }

    private void LoadNetAccountHdrInfoList()
    {
        NetAccountSearchParamInfo oNetAccountSearchParamInfo = new NetAccountSearchParamInfo();
        Helper.FillCommonServiceParams(oNetAccountSearchParamInfo);
        NetAccountHdrInfoList = NetAccountHelper.GetNetAccounts(oNetAccountSearchParamInfo);
        if (NetAccountHdrInfoList != null && NetAccountHdrInfoList.Count > 0)
        {
            NetAccountHdrInfoList.ForEach(obj =>
            {
                obj.NetAccountLabelText = Helper.GetLabelIDValue((int)obj.NetAccountLabelID);
            }
            );
        }
    }
    #endregion

    #region Other Methods

    protected void LoadNetAccountAttributesValue()
    {
        //Get Net Account Attributes Value 1. Account Policy Url 2.Reconciliation Procedure
        //3. Account Description
        List<NetAccountAttributeValueInfo> oNetAccountAttributeValueInfoList = NetAccountHelper.GetNetAccountAttributeValues(Convert.ToInt32(this._NetAccountID), (int)SessionHelper.CurrentCompanyID, (int)SessionHelper.CurrentReconciliationPeriodID);
        oNetAccountAttributeValueInfoList.ForEach(obj =>
        {
            ARTEnums.AccountAttribute enumAccountAttribute = (ARTEnums.AccountAttribute)obj.AccountAttributeID;
            switch (enumAccountAttribute)
            {
                case ARTEnums.AccountAttribute.AccountPolicyURL:
                    ucAccountPolicyURL.EditorControl.Content = LanguageUtil.GetValue(obj.ValueLabelID);
                    ucAccountPolicyURL.EditorControl.Attributes.Add(WebConstants.ATTRIBUTE_LABEL_ID, obj.ValueLabelID.ToString());
                    break;
                case ARTEnums.AccountAttribute.ReconciliationProcedure:
                    ucReconciliationProcedure.EditorControl.Content = LanguageUtil.GetValue(obj.ValueLabelID);
                    ucReconciliationProcedure.EditorControl.Attributes.Add(WebConstants.ATTRIBUTE_LABEL_ID, obj.ValueLabelID.ToString());
                    break;
                case ARTEnums.AccountAttribute.Description:
                    ucDescription.EditorControl.Content = LanguageUtil.GetValue(obj.ValueLabelID);
                    ucDescription.EditorControl.Attributes.Add(WebConstants.ATTRIBUTE_LABEL_ID, obj.ValueLabelID.ToString());
                    break;
            }
        }
        );
    }
    public List<NetAccountHdrInfo> NetAccountHdrInfoList
    {
        get { return (List<NetAccountHdrInfo>)Session[SessionConstants.NET_ACCOUNT_LIST]; }
        set { Session[SessionConstants.NET_ACCOUNT_LIST] = value; }
    }

    public NetAccountHdrInfo SelectedNetAccountHdrInfo
    {
        get
        {
            if (NetAccountHdrInfoList != null)
                return NetAccountHdrInfoList.Find
                    (obj => obj.NetAccountID == Convert.ToInt32(this._NetAccountID));
            else return null;
        }
    }
    public List<AccountHdrInfo> AccountHdrInfoListAdded
    {
        get { return (List<AccountHdrInfo>)Session[SessionConstants.NET_ACCOUNT_ASSOCIATION]; }
        set { Session[SessionConstants.NET_ACCOUNT_ASSOCIATION] = value; }
    }

    #endregion

}
