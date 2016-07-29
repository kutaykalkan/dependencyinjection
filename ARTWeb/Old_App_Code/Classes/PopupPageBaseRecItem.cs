using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using SkyStem.ART.Web.Utility;
using System.Text;
using System.Globalization;
using SkyStem.ART.Web.Data;
using SkyStem.ART.Client.Model;

namespace SkyStem.ART.Web.Classes
{

    /// <summary>
    /// Summary description for PopupPageBase
    /// </summary>
    public abstract class PopupPageBaseRecItem : PopupPageBase
    {
        private short? _RecCategoryType;
        private short? _RecCategory;
        private string _Mode;
        private long? _GLRecInputItemID;
        private bool? _IsForwardedItem;
        private string _ParentHiddenField;
        private GLDataHdrInfo _GLDataHdrInfo = null;
        #region GLDataHdr Properties

        public GLDataHdrInfo GLDataHdrInfo
        {
            get
            {
                return GetGLDataHdrObject();
            }
            set
            {
                SetGLDataHdrObject();
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
                return this.GLDataHdrInfo.AccountID;
            }
        }

        public int? NetAccountID
        {
            get
            {
                return this.GLDataHdrInfo.NetAccountID;
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

        public short? GLRecStatusID
        {
            get
            {
                if (GLDataHdrInfo != null)
                    return GLDataHdrInfo.ReconciliationStatusID;
                return null;
            }
        }

        public short? RecCategoryType
        {
            get
            {
                if (!_RecCategoryType.HasValue && Request.QueryString[QueryStringConstants.REC_CATEGORY_TYPE_ID] != null)
                    this._RecCategoryType = Convert.ToInt16(Request.QueryString[QueryStringConstants.REC_CATEGORY_TYPE_ID]);
                return this._RecCategoryType;
            }
            private set
            {
                this._RecCategoryType = value;
            }
        }

        public string Mode
        {
            get
            {
                if (String.IsNullOrEmpty(this._Mode) && Request.QueryString[QueryStringConstants.MODE] != null)
                    this._Mode = Request.QueryString[QueryStringConstants.MODE];
                return this._Mode;
            }
            private set
            {
                this._Mode = value;
            }
        }

        public long? GLRecInputItemID
        {
            get
            {
                if (!this._GLRecInputItemID.HasValue && Request.QueryString[QueryStringConstants.GL_RECONCILIATION_ITEM_INPUT_ID] != null)
                    this._GLRecInputItemID = Convert.ToInt32(Request.QueryString[QueryStringConstants.GL_RECONCILIATION_ITEM_INPUT_ID]);

                return this._GLRecInputItemID;
            }
            private set
            {
                this._GLRecInputItemID = value;
            }
        }

        public bool? IsForwardedItem
        {
            get
            {
                if (!this._IsForwardedItem.HasValue && Request.QueryString[QueryStringConstants.IS_FORWARDED_ITEM] != null)
                    this._IsForwardedItem = Convert.ToBoolean(Convert.ToInt32(Request.QueryString[QueryStringConstants.IS_FORWARDED_ITEM]));
                return this._IsForwardedItem;
            }
            private set
            {
                this._IsForwardedItem = value;
            }
        }

        public string ParentHiddenField
        {
            get
            {
                if (String.IsNullOrEmpty(this._ParentHiddenField) && Request.QueryString[QueryStringConstants.PARENT_HIDDEN_FIELD] != null)
                    this._ParentHiddenField = Request.QueryString[QueryStringConstants.PARENT_HIDDEN_FIELD];
                return this._ParentHiddenField;
            }
            private set
            {
                this._ParentHiddenField = value;
            }
        }

        public short? RecCategory
        {
            get
            {
                if (!this._RecCategory.HasValue && Request.QueryString[QueryStringConstants.REC_CATEGORY_ID] != null)
                    this._RecCategory = Convert.ToInt16(Request.QueryString[QueryStringConstants.REC_CATEGORY_ID].ToString());
                return this._RecCategory;
            }
            private set
            {
                this._RecCategory = value;
            }
        }
        #endregion

        public PopupPageBaseRecItem()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!this.IsPostBack)
            {
                if (Session[SessionConstants.ATTACHMENTS] != null)
                    Session.Remove(SessionConstants.ATTACHMENTS);
            }
        }


        private GLDataHdrInfo GetGLDataHdrObject()
        {
            if (ViewState[ViewStateConstants.CURRENT_GLDATAHDRINFO] == null)
                this.SetGLDataHdrObject();
            _GLDataHdrInfo = (GLDataHdrInfo)ViewState[ViewStateConstants.CURRENT_GLDATAHDRINFO];
            return _GLDataHdrInfo;

        }

        private void SetGLDataHdrObject()
        {
            GLDataHdrInfo oGLDataHdrInfo = null;

            if (ViewState[ViewStateConstants.CURRENT_GLDATAHDRINFO] != null)
                oGLDataHdrInfo = (GLDataHdrInfo)ViewState[ViewStateConstants.CURRENT_GLDATAHDRINFO];
            else
            {
                oGLDataHdrInfo = new GLDataHdrInfo();

                if (!string.IsNullOrEmpty(Request.QueryString[QueryStringConstants.ACCOUNT_ID]))
                    oGLDataHdrInfo.AccountID = Convert.ToInt64(Request.QueryString[QueryStringConstants.ACCOUNT_ID]);

                if (!string.IsNullOrEmpty(Request.QueryString[QueryStringConstants.NETACCOUNT_ID]))
                    oGLDataHdrInfo.NetAccountID = Convert.ToInt32(Request.QueryString[QueryStringConstants.NETACCOUNT_ID]);

                if (Request.QueryString[QueryStringConstants.GLDATA_ID] != null)
                    oGLDataHdrInfo.GLDataID = Convert.ToInt32(Request.QueryString[QueryStringConstants.GLDATA_ID]);

                if (Request.QueryString[QueryStringConstants.CURRENT_BCCY] != null)
                    oGLDataHdrInfo.BaseCurrencyCode = Request.QueryString[QueryStringConstants.CURRENT_BCCY].ToString();

                ViewState[ViewStateConstants.CURRENT_GLDATAHDRINFO] = oGLDataHdrInfo;
            }


        }


    }
}
