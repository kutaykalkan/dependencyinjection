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

namespace SkyStem.ART.Web.UserControls
{
    public partial class UserControls_AccountHierarchyDetail : UserControlBase
    {
        #region "Private Properties"
        private long? _AccountID;
        private int? _NetAccountID;
        #endregion

        #region "Public Properties"
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
            try
            {
                IAccount oAccountClient = RemotingHelper.GetAccountObject();

                if (this._AccountID.HasValue && this._AccountID.Value > 0)
                {
                    //AccountHdrInfo oAccountHdrInfo = new AccountHdrInfo();

                    //oAccountHdrInfo = oAccountClient.GetAccountHdrInfoByAccountID(_AccountID.Value, SessionHelper.CurrentCompanyID.Value, SessionHelper.CurrentReconciliationPeriodID.Value);

                    lblAccountDetails.Text = Helper.GetAccountEntityStringToDisplay(_AccountID.Value);
                }
                else if (this._NetAccountID.HasValue && this._NetAccountID.Value > 0)
                {
                    lblAccountDetails.Text = LanguageUtil.GetValue(1257) + WebConstants.ACCOUNT_ENTITY_SEPARATOR + oAccountClient.GetNetAccountNameByNetAccountID(this._NetAccountID.Value, SessionHelper.CurrentCompanyID.Value
                        , SessionHelper.GetUserLanguage(), SessionHelper.GetBusinessEntityID(), AppSettingHelper.GetDefaultLanguageID(),Helper.GetAppUserInfo());
                }
            }
            catch (Exception ex)
            {
                Helper.LogException(ex);
            }
        }

    }//end of class
}//end of namespace
