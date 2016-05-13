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
using System.Collections.Generic;
using SkyStem.ART.Client.Model;
using SkyStem.ART.Client.IServices;
using System.Text;
using SkyStem.ART.Web.Data;
using SkyStem.ART.Web.Classes;
using SkyStem.Language.LanguageUtility;
using SkyStem.ART.Client.Data;

namespace SkyStem.ART.Web.UserControls
{
    public partial class UserControls_AccountDescription : UserControlBase
    {
        AccountHdrInfo oAccountHdrInfo;
        const int POPUP_HEIGHT = 450;
        const int POPUP_WIDTH = 800;


        #region "Private Properties"
        private long? _AccountID;
        private int? _NetAccountID;

        #endregion

        public override bool Collapsed
        {
            set
            {
                cpeAccountDetail.Collapsed = value;
            }
        }

        #region "Public Properties"

        public AccountHdrInfo SelectedAccountHdrInfo
        {
            get { return oAccountHdrInfo; }
            set { oAccountHdrInfo = value; }
        }

        public string AccountInfo
        {
            get
            {
                return this.lblAccountDetails.Text;
            }
        }
        public long? AccountID
        {
            get { return this._AccountID; }
            set { this._AccountID = value; }
        }

        public int? NetAccountID
        {
            get { return this._NetAccountID; }
            set { this._NetAccountID = value; }
        }


        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
                OnPageLoad();
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            MasterPageBase ompage = (MasterPageBase)this.Page.Master.Master;
            if (ompage != null)
                ompage.ReconciliationPeriodChangedEventHandler += new EventHandler(ompage_ReconciliationPeriodChangedEventHandler);
        }
        protected void ompage_ReconciliationPeriodChangedEventHandler(object sender, EventArgs e)
        {
            OnPageLoad();
        }
        protected void Page_PreRender(object sender, EventArgs e)
        {


        }

        private void EnableDisableLinkControlsForCurrentRecPeriodStatus()
        {
            short currentRoleID = SessionHelper.CurrentRoleID.Value;
            // Let the popup handle editing
            // this.EditMode == WebEnums.FormMode.Edit && 
            if ((currentRoleID == (short)WebEnums.UserRole.PREPARER || currentRoleID == (short)WebEnums.UserRole.BACKUP_PREPARER))
            {
                string url = Page.ResolveClientUrl("~/Pages/AccountProfileAttributeUpdatePopup.aspx") + "?" + QueryStringConstants.ACCOUNT_ID + "=" + this.AccountID + "&" + QueryStringConstants.NETACCOUNT_ID + "=" + this.NetAccountID;
                hlAccountPolicyURL.NavigateUrl = "javascript:OpenRadWindowForHyperlink('" + url + "', " + POPUP_HEIGHT + " , " + POPUP_WIDTH + ");";
                hlAccountDescription.NavigateUrl = "javascript:OpenRadWindowForHyperlink('" + url + "', " + POPUP_HEIGHT + " , " + POPUP_WIDTH + ");";
                hlReconciliationProcedure.NavigateUrl = "javascript:OpenRadWindowForHyperlink('" + url + "', " + POPUP_HEIGHT + " , " + POPUP_WIDTH + ");";
            }
            else
            {

            }
        }


        private void OnPageLoad()
        {
            try
            {
                IAccount oAccountClient = RemotingHelper.GetAccountObject();

                if (this._AccountID.HasValue && this._AccountID.Value > 0)
                {
                    AccountHdrInfo oAccountHdrInfo = new AccountHdrInfo();
                    oAccountHdrInfo = oAccountClient.GetAccountHdrInfoByAccountID(_AccountID.Value, SessionHelper.CurrentCompanyID.Value, SessionHelper.CurrentReconciliationPeriodID.Value, Helper.GetAppUserInfo());

                    List<AccountHdrInfo> oAccountHdrInfoCollection = new List<AccountHdrInfo>();
                    oAccountHdrInfoCollection.Add(oAccountHdrInfo);
                    oAccountHdrInfoCollection = LanguageHelper.TranslateLabelsAccountHdr(oAccountHdrInfoCollection);

                    oAccountHdrInfo = oAccountHdrInfoCollection.First();

                    lblAccountPolicyURLValue.Text = oAccountHdrInfo.AccountPolicyUrl;

                    if (oAccountHdrInfo.ReconciliationProcedureLabelID.HasValue && oAccountHdrInfo.ReconciliationProcedureLabelID.Value > 0)
                    {
                        lblReconciliationProcedureValue.Text = Helper.GetLabelIDValue(oAccountHdrInfo.ReconciliationProcedureLabelID.Value);
                    }

                    if (oAccountHdrInfo.DescriptionLabelID.HasValue && oAccountHdrInfo.DescriptionLabelID.Value > 0)
                    {
                        lblAccountDescriptionValue.Text = Helper.GetLabelIDValue(oAccountHdrInfo.DescriptionLabelID.Value);
                    }
                    SelectedAccountHdrInfo = Helper.GetAccountHdrInfo(_AccountID.Value);
                    lblAccountDetails.Text = Helper.GetAccountEntityStringToDisplay(SelectedAccountHdrInfo);
                }
                else if (this._NetAccountID.HasValue && this._NetAccountID.Value > 0)
                {
                    lblAccountDetails.Text = LanguageUtil.GetValue(1257) + WebConstants.ACCOUNT_ENTITY_SEPARATOR + oAccountClient.GetNetAccountNameByNetAccountID(this._NetAccountID.Value, SessionHelper.CurrentCompanyID.Value
                        , SessionHelper.GetUserLanguage(), SessionHelper.GetBusinessEntityID(), AppSettingHelper.GetDefaultLanguageID(), Helper.GetAppUserInfo());

                    List<NetAccountAttributeValueInfo> oNetAccountAttributeValueInfoList = NetAccountHelper.GetNetAccountAttributeValues((int)this._NetAccountID, (int)SessionHelper.CurrentCompanyID, (int)SessionHelper.CurrentReconciliationPeriodID);
                    oNetAccountAttributeValueInfoList.ForEach(obj =>
                    {
                        ARTEnums.AccountAttribute enumAccountAttribute = (ARTEnums.AccountAttribute)obj.AccountAttributeID;
                        switch (enumAccountAttribute)
                        {
                            case ARTEnums.AccountAttribute.AccountPolicyURL:
                                lblAccountPolicyURLValue.Text = LanguageUtil.GetValue(obj.ValueLabelID);
                                break;
                            case ARTEnums.AccountAttribute.ReconciliationProcedure:
                                lblReconciliationProcedureValue.Text = LanguageUtil.GetValue(obj.ValueLabelID);
                                break;
                            case ARTEnums.AccountAttribute.Description:
                                lblAccountDescriptionValue.Text = LanguageUtil.GetValue(obj.ValueLabelID);
                                break;
                        }
                    }
                    );

                }

                EnableDisableLinkControlsForCurrentRecPeriodStatus();

            }
            catch (Exception ex)
            {
                Helper.ShowErrorMessage((PageBase)this.Page, ex);
            }
        }
    }//end of class
}//end of namespace
