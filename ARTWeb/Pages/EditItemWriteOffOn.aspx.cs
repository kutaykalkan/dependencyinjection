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

public partial class Pages_EditItemWriteOffOn : PopupPageBaseRecItem
{
    #region Variables & Constants
    private int _GLDataWriteOnOffID;
    private bool _IsMultiCurrencyEnabled;
    #endregion

    #region Properties
    private GLDataWriteOnOffInfo _GLDataWriteOnOffInfo;
    private decimal? ExRateLCCYtoBCCY
    {
        get
        {
            decimal exRate;
            if (Decimal.TryParse(hdnExRateLCCYtoBCCY.Value, out exRate) && exRate != 0)
                return exRate;
            return null;
        }
        set { hdnExRateLCCYtoBCCY.Value = value.GetValueOrDefault().ToString(); }
    }

    private decimal? ExRateLCCYtoRCCY
    {
        get
        {
            decimal exRate;
            if (Decimal.TryParse(hdnExRateLCCYtoRCCY.Value, out exRate) && exRate != 0)
                return exRate;
            return null;
        }
        set { hdnExRateLCCYtoRCCY.Value = value.GetValueOrDefault().ToString(); }
    }

    private bool IsExchangeRateOverridden
    {
        get
        {
            if (hdnIsExchangeRateOverridden.Value == "1")
                return true;
            return false;
        }
        set
        {
            if (value)
                hdnIsExchangeRateOverridden.Value = "1";
            else
                hdnIsExchangeRateOverridden.Value = "0";
        }
    }
    #endregion

    #region Delegates & Events
    #endregion

    #region Page Events
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //this.RecCategoryType = Convert.ToInt16(Request.QueryString[QueryStringConstants.REC_CATEGORY_TYPE_ID].ToString());
            //_RecCategory = Convert.ToInt16(Request.QueryString[QueryStringConstants.REC_CATEGORY_ID].ToString());
            int[] oLableIdCollection = new int[0];
            PopupHelper.SetPageTitle(this, this.RecCategory.Value, this.RecCategoryType.Value, oLableIdCollection);


            SetCompanyCabalityInfo();
            GetQueryStringValues();
            SetErrorMessagesForValidationControls();

            if (!IsPostBack)
            {
                PopulateItemsOnPage();
            }

            SetExchangeRates();
            this.lblInputFormRecPeriodValue.Text = Helper.GetDisplayDate(SessionHelper.CurrentReconciliationPeriodEndDate);

            ucAccountHierarchyDetailPopup.AccountID = this.AccountID.Value;
            this.ucAccountHierarchyDetailPopup.NetAccountID = this.NetAccountID;

            lblAmountBCCYCode.Text = this.CurrentBCCY;
            lblAmountRCCYCode.Text = SessionHelper.ReportingCurrencyCode;
            ucExchangeRate.BCCYCode = this.CurrentBCCY;
            ucExchangeRate.RCCYCode = SessionHelper.ReportingCurrencyCode;
            lblOverriddenExRateBCCYCode.Text = this.CurrentBCCY;
            lblOverriddenExRateRCCYCode.Text = SessionHelper.ReportingCurrencyCode;
            SetModeForFormView();
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
                IGLDataWriteOnOff oGLDataWriteOnOffClient = RemotingHelper.GetGLDataWriteOnOffObject();

                if (this.Mode == QueryStringConstants.INSERT)
                {
                    GLDataWriteOnOffInfo oGLDataWriteOnOffInfo = this.GetGLDataWriteOnOffInfo();

                    oGLDataWriteOnOffClient.InsertGLDataWriteOnOff(oGLDataWriteOnOffInfo, this.RecCategory, this.RecCategoryType, SessionHelper.CurrentReconciliationPeriodID.Value, Helper.GetAppUserInfo());
                }
                else if (this.IsForwardedItem.Value)
                {
                    SaveCloseDateForForwardedItems();
                }
                else if (this.Mode == QueryStringConstants.EDIT)
                {
                    GLDataWriteOnOffInfo oGLDataWriteOnOffInfo = this.GetGLDataWriteOnOffInfo();
                    oGLDataWriteOnOffClient.UpdateGLDataWriteOnOff(oGLDataWriteOnOffInfo, this.RecCategoryType, Helper.GetAppUserInfo());
                    if (!string.IsNullOrEmpty(calResolutionDate.Text))
                    {
                        SaveCloseDateForForwardedItems();
                    }
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
    protected void btnOvereideExchangeRate_Click(object sender, EventArgs e)
    {
        SetExchangeRateAndRecalculateAmount();
    }

    protected void ddlLocalCurrency_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            PopupHelper.HideErrorMessage(this);
            IsExchangeRateOverridden = false;
            if (ddlLocalCurrency.SelectedValue == WebConstants.SELECT_ONE)
            {
                this.ExRateLCCYtoBCCY = null;
                this.ExRateLCCYtoRCCY = null;
            }
            SetExchangeRateAndRecalculateAmount();
        }
        catch (Exception ex)
        {
            PopupHelper.ShowErrorMessage(this, ex);
        }
    }

    #endregion

    #region Validation Control Events
    protected void cvLocalCurrency_ServerValidate(object source, ServerValidateEventArgs args)
    {
        if (ddlLocalCurrency.SelectedItem.Value == WebConstants.SELECT_ONE)//"select one"
        {
            args.IsValid = false;
        }
    }
    #endregion

    #region Private Methods
    private void SetErrorMessagesForValidationControls()
    {
        RecHelper.SetErrorMessagesForValidationControls(rfvAmount, rfvLocalCurrency, rfvOpenTransDate, rfvResolutionDate
            , cstVldAmount, cvOpenDate, cvResolutionDate, cvCompareOpenDateWithCurrentDate, cvCompareWithCurrentDate, cvCompareWithOpenDate
            , lblAmount.LabelID, lblCurrency.LabelID, lblOpenTransDate.LabelID, lblResolutionCloseDate.LabelID);

        //// Required Fields
        //rfvAmount.ErrorMessage = Helper.GetErrorMessage(WebEnums.FieldType.MandatoryField, 1510);

        //rfvLocalCurrency.InitialValue = WebConstants.SELECT_ONE;
        //rfvLocalCurrency.ErrorMessage = Helper.GetErrorMessage(WebEnums.FieldType.MandatoryField, lblCurrency.LabelID);

        //rfvOpenTransDate.ErrorMessage = Helper.GetErrorMessage(WebEnums.FieldType.MandatoryField, lblOpenTransDate.LabelID);
        //rfvResolutionDate.ErrorMessage = LanguageUtil.GetValue(5000095);

        //// Invalid Values
        //cstVldAmount.ErrorMessage = LanguageUtil.GetValue(5000093);
        //cvOpenDate.ErrorMessage = LanguageUtil.GetValue(5000100);
        //cvResolutionDate.ErrorMessage = LanguageUtil.GetValue(5000100);

        //cvCompareOpenDateWithCurrentDate.ErrorMessage = LanguageUtil.GetValue(5000101);
        //cvCompareWithCurrentDate.ErrorMessage = LanguageUtil.GetValue(5000102);
        //cvCompareWithOpenDate.ErrorMessage = LanguageUtil.GetValue(5000103);

        cvIsSelected.ErrorMessage = Helper.GetErrorMessage(WebEnums.FieldType.MandatoryField, 1425);
    }

    private void SetExchangeRates()
    {
        if (ddlLocalCurrency.SelectedValue != WebConstants.SELECT_ONE)
        {
            ucExchangeRate.LCCYCode = ddlLocalCurrency.SelectedValue;

            if (!IsExchangeRateOverridden)
            {
                this.ExRateLCCYtoBCCY = CacheHelper.GetExchangeRate(ddlLocalCurrency.SelectedValue, this.CurrentBCCY, SessionHelper.CurrentReconciliationPeriodID);
                this.ExRateLCCYtoRCCY = CacheHelper.GetExchangeRate(ddlLocalCurrency.SelectedValue, SessionHelper.ReportingCurrencyCode, SessionHelper.CurrentReconciliationPeriodID);
            }
        }
        txtAmount.Attributes.Add(WebConstants.ONBLUR, "RecalculateRecItemAmount('" + txtAmount.ClientID + "/" + lblAmountBCCYValue.ClientID + "/" + lblAmountRCCYValue.ClientID + "/" + this.ExRateLCCYtoBCCY + "/" + this.ExRateLCCYtoRCCY + "/" + ddlLocalCurrency.SelectedValue + "/" + TestConstant.DECIMAL_PLACES_FOR_MATH_ROUND + "/" + TestConstant.DECIMAL_PLACES_FOR_EXCHANGE_RATE_ROUND + "');");
        hlOverrideExchangeRate.NavigateUrl = "javascript:{OpenRadWindowFromRadWindow('" + SetOverrideExchangeRateURL() + "', '300', '350');}";
        lblOverriddenExRateBCCYValue.Text = Helper.GetDisplayExchangeRateValue(ExRateLCCYtoBCCY);
        lblOverriddenExRateRCCYValue.Text = Helper.GetDisplayExchangeRateValue(ExRateLCCYtoRCCY);

        if (_IsMultiCurrencyEnabled && IsExchangeRateOverridden)
            pnlOverriddenExRate.Visible = true;
        else
            pnlOverriddenExRate.Visible = false;
    }

    private void PopulateItemsOnPage()
    {
        IGLData oGLDataClient = RemotingHelper.GetGLDataObject();

        if (this.Mode == QueryStringConstants.EDIT
            || this.Mode == QueryStringConstants.READ_ONLY)
        {
            IGLDataWriteOnOff oGLDataWriteOnOffClient = RemotingHelper.GetGLDataWriteOnOffObject();
            List<GLDataWriteOnOffInfo> oGLDataWriteOnOffInfoCollection = oGLDataWriteOnOffClient.GetGLDataWriteOnOffInfoCollectionByGLDataID(this.GLDataID, (short)ARTEnums.AccountAttribute.ReconciliationTemplate, Helper.GetAppUserInfo());

            this._GLDataWriteOnOffInfo = oGLDataWriteOnOffInfoCollection.Where(recItem => recItem.GLDataWriteOnOffID == this._GLDataWriteOnOffID).FirstOrDefault();

            lblEnteredByValue.Text = this._GLDataWriteOnOffInfo.UserName;
            lblAddedDate.Text = Helper.GetDisplayDate(this._GLDataWriteOnOffInfo.DateAdded);
            lblAmountBCCYValue.Text = Helper.GetDisplayBaseCurrencyValue(this.CurrentBCCY, this._GLDataWriteOnOffInfo.AmountBaseCurrency);
            lblAmountRCCYValue.Text = Helper.GetDisplayDecimalValue(this._GLDataWriteOnOffInfo.AmountReportingCurrency);
            txtAmount.Text = Helper.GetDecimalValueForTextBox(this._GLDataWriteOnOffInfo.Amount, 2);
            txtComments.Text = this._GLDataWriteOnOffInfo.Comments;
            txtJournalEntryRef.Text = this._GLDataWriteOnOffInfo.JournalEntryRef.ToString();
            txtResolutionComment.Text = this._GLDataWriteOnOffInfo.CloseComments;
            calResolutionDate.Text = Helper.GetDisplayDateForCalendar(this._GLDataWriteOnOffInfo.CloseDate);

            //Populate Labels
            lblLocalCurrencyValue.Text = Helper.GetDisplayDecimalValue(this._GLDataWriteOnOffInfo.Amount);
            lblCommentsValue.Text = this._GLDataWriteOnOffInfo.Comments;
            lblJournalEntryRefValue.Text = this._GLDataWriteOnOffInfo.JournalEntryRef.ToString();
            lblResolutionCommentValue.Text = this._GLDataWriteOnOffInfo.CloseComments;
            lblResolutionDate.Text = Helper.GetDisplayDate(this._GLDataWriteOnOffInfo.CloseDate);

            lblRecItemNumberValue.Text = Helper.GetDisplayStringValue(this._GLDataWriteOnOffInfo.RecItemNumber);
            lblMatchSetRefNoValue.Text = Helper.GetDisplayStringValue(this._GLDataWriteOnOffInfo.MatchSetRefNumber);

            calOpenTransDate.Text = Helper.GetDisplayDateForCalendar(this._GLDataWriteOnOffInfo.OpenDate);
            lblOpenTransDateValue.Text = Helper.GetDisplayDate(this._GLDataWriteOnOffInfo.OpenDate);

            ucExchangeRate.LCCYCode = this._GLDataWriteOnOffInfo.LocalCurrencyCode;
            WebEnums.WriteOnOff eWriteOnOff = (WebEnums.WriteOnOff)this._GLDataWriteOnOffInfo.WriteOnOffID;
            if (Convert.ToString(eWriteOnOff) != String.Empty)
            {
                switch (eWriteOnOff)
                {
                    case WebEnums.WriteOnOff.WriteOff:
                        optWriteOff.Checked = true;
                        optWriteOn.Checked = false;
                        break;
                    case WebEnums.WriteOnOff.WriteOn:
                        optWriteOff.Checked = false;
                        optWriteOn.Checked = true;
                        break;
                }
            }
            if (this._GLDataWriteOnOffInfo.ExRateLCCYtoBCCY.HasValue)
            {
                lblOverriddenExRateBCCYValue.Text = Helper.GetDisplayExchangeRateValue(this._GLDataWriteOnOffInfo.ExRateLCCYtoBCCY);
                ExRateLCCYtoBCCY = this._GLDataWriteOnOffInfo.ExRateLCCYtoBCCY;
            }
            if (this._GLDataWriteOnOffInfo.ExRateLCCYtoRCCY.HasValue)
            {
                lblOverriddenExRateRCCYValue.Text = Helper.GetDisplayExchangeRateValue(this._GLDataWriteOnOffInfo.ExRateLCCYtoRCCY);
                ExRateLCCYtoRCCY = this._GLDataWriteOnOffInfo.ExRateLCCYtoRCCY;
                IsExchangeRateOverridden = true;
            }
        }
        else
        {
            UserHdrInfo oUserHdrInfo = SessionHelper.GetCurrentUser();
            lblEnteredByValue.Text = oUserHdrInfo.FirstName + " " + oUserHdrInfo.LastName;
            lblAddedDate.Text = Helper.GetDisplayDate(DateTime.Today);
            lblRecItemNumberValue.Text = "-";
        }

        ListControlHelper.BindLocalCurrencyDropDown(ddlLocalCurrency, this.GLDataID.Value, this._IsMultiCurrencyEnabled);
        if (this._IsMultiCurrencyEnabled && this.Mode != QueryStringConstants.EDIT)
        {
            ListControlHelper.AddListItemForCCY(ddlLocalCurrency);
        }
        if (this._GLDataWriteOnOffInfo != null && this._IsMultiCurrencyEnabled && this.Mode == QueryStringConstants.EDIT)
        {
            if (this.ddlLocalCurrency.Items.FindByValue(this._GLDataWriteOnOffInfo.LocalCurrencyCode) != null)
                this.ddlLocalCurrency.SelectedValue = this._GLDataWriteOnOffInfo.LocalCurrencyCode;
        }


        if (this._GLDataWriteOnOffInfo != null)
        {
            lblLocalCurrencyType.Text = this._GLDataWriteOnOffInfo.LocalCurrencyCode;
        }
    }

    private void SetCompanyCabalityInfo()
    {
        this._IsMultiCurrencyEnabled = Helper.IsCapabilityActivatedForRecPeriodID(ARTEnums.Capability.MultiCurrency, SessionHelper.CurrentReconciliationPeriodID.Value, true);
    }

    private void SetModeForFormView()
    {
        switch (this.Mode)
        {
            case QueryStringConstants.INSERT:
                ShowHideControls(false);
                break;
            case QueryStringConstants.EDIT:
                if (this.IsForwardedItem.Value)
                {
                    ShowHideControls(true);
                }
                else
                {
                    lblLocalCurrencyValue.Visible = false;
                    txtAmount.Visible = true;
                    lblLocalCurrencyType.Visible = false;
                    ddlLocalCurrency.Visible = true;
                    lblOpenTransDateValue.Visible = false;
                    calOpenTransDate.Visible = true;
                    lblJournalEntryRefValue.Visible = false;
                    txtJournalEntryRef.Visible = true;
                    lblResolutionCommentValue.Visible = false;
                    txtResolutionComment.Visible = true;
                    lblResolutionDate.Visible = false;
                    calResolutionDate.Visible = true;
                    pnlCloseRecItem.Visible = true;
                    lblCommentsValue.Visible = false;
                    txtComments.Visible = true;
                    rfvResolutionDate.Enabled = false;
                    rfvOpenTransDate.Enabled = true;
                    rfvAmount.Enabled = true;
                    cstVldAmount.Enabled = true;
                    optWriteOff.Enabled = true;
                    optWriteOn.Enabled = true;
                }

                //Commented By Prafull
                //lblCommentsValue.Visible = false;
                //txtComments.Visible = true;
                btnUpdate.Visible = true;
                break;

            default:
                lblLocalCurrencyValue.Visible = true;
                txtAmount.Visible = false;
                lblLocalCurrencyType.Visible = true;
                ddlLocalCurrency.Visible = false;
                lblOpenTransDateValue.Visible = true;
                calOpenTransDate.Visible = false;
                lblJournalEntryRefValue.Visible = true;
                txtJournalEntryRef.Visible = false;
                lblResolutionCommentValue.Visible = true;
                txtResolutionComment.Visible = false;
                lblResolutionDate.Visible = true;
                calResolutionDate.Visible = false;
                pnlCloseRecItem.Visible = true;

                btnUpdate.Visible = false;

                //vldJournalEntryRef.Enabled = false;
                rfvResolutionDate.Enabled = false;
                rfvOpenTransDate.Enabled = false;
                rfvAmount.Enabled = false;
                cstVldAmount.Enabled = false;
                lblCommentsValue.Visible = true;
                txtComments.Visible = false;
                optWriteOff.Enabled = false;
                optWriteOn.Enabled = false;
                break;
        }
        if (this._IsMultiCurrencyEnabled)
        {
            trExchangeRate.Visible = true;
            trExchangeRateBlankRow.Visible = true;
            if (!this.IsForwardedItem.Value && (this.Mode == QueryStringConstants.INSERT || this.Mode == QueryStringConstants.EDIT))
                hlOverrideExchangeRate.Visible = true;
        }
        else
        {
            trExchangeRate.Visible = false;
            trExchangeRateBlankRow.Visible = false;
            hlOverrideExchangeRate.Visible = false;
        }
    }

    private void ShowHideControls(bool flag)
    {
        lblLocalCurrencyValue.Visible = flag;
        txtAmount.Visible = !flag;
        lblLocalCurrencyType.Visible = flag;
        ddlLocalCurrency.Visible = !flag;
        lblOpenTransDateValue.Visible = flag;
        calOpenTransDate.Visible = !flag;
        lblJournalEntryRefValue.Visible = !flag;
        txtJournalEntryRef.Visible = flag;
        lblResolutionCommentValue.Visible = !flag;
        txtResolutionComment.Visible = flag;
        lblResolutionDate.Visible = !flag;
        calResolutionDate.Visible = flag;
        pnlCloseRecItem.Visible = flag;
        lblCommentsValue.Visible = flag;
        txtComments.Visible = !flag;
        rfvResolutionDate.Enabled = flag;
        rfvOpenTransDate.Enabled = !flag;
        rfvAmount.Enabled = !flag;
        cstVldAmount.Enabled = !flag;
        optWriteOff.Enabled = !flag;
        optWriteOn.Enabled = !flag;
    }


    private void GetQueryStringValues()
    {
        //if (!string.IsNullOrEmpty(Request.QueryString[QueryStringConstants.ACCOUNT_ID]))
        //    this._AccountID = Convert.ToInt32(Request.QueryString[QueryStringConstants.ACCOUNT_ID]);

        //if (!string.IsNullOrEmpty(Request.QueryString[QueryStringConstants.NETACCOUNT_ID]))
        //    this._NetAccountID = Convert.ToInt32(Request.QueryString[QueryStringConstants.NETACCOUNT_ID]);

        //if (Request.QueryString[QueryStringConstants.GLDATA_ID] != null)
        //    this.GLDataID = Convert.ToInt32(Request.QueryString[QueryStringConstants.GLDATA_ID]);

        //if (Request.QueryString[QueryStringConstants.REC_CATEGORY_TYPE_ID] != null)
        //    this.RecCategoryType = Convert.ToInt16(Request.QueryString[QueryStringConstants.REC_CATEGORY_TYPE_ID]);

        //if (Request.QueryString[QueryStringConstants.MODE] != null)
        //    this.Mode = Request.QueryString[QueryStringConstants.MODE];

        if (Request.QueryString[QueryStringConstants.GL_RECONCILIATION_ITEM_INPUT_ID] != null)
            this._GLDataWriteOnOffID = Convert.ToInt32(Request.QueryString[QueryStringConstants.GL_RECONCILIATION_ITEM_INPUT_ID]);

        //if (Request.QueryString[QueryStringConstants.IS_FORWARDED_ITEM] != null)
        //    this.IsForwardedItem = Convert.ToBoolean(Convert.ToInt32(Request.QueryString[QueryStringConstants.IS_FORWARDED_ITEM]));

        //if (Request.QueryString[QueryStringConstants.PARENT_HIDDEN_FIELD] != null)
        //    this.ParentHiddenField = Request.QueryString[QueryStringConstants.PARENT_HIDDEN_FIELD];

    }
    private void SaveCloseDateForForwardedItems()
    {
        IGLDataWriteOnOff oGLDataWriteOnOffClient = RemotingHelper.GetGLDataWriteOnOffObject();
        List<long> glGLDataWriteOnOffIDCollection = new List<long>();
        glGLDataWriteOnOffIDCollection.Add(this._GLDataWriteOnOffID);
        string journalEntryRefNum = string.Empty;

        if (!string.IsNullOrEmpty(txtJournalEntryRef.Text))
        {
            journalEntryRefNum = txtJournalEntryRef.Text;
        }
        oGLDataWriteOnOffClient.UpdateGLDataWriteOnOffCloseDate(this.GLDataID.Value, glGLDataWriteOnOffIDCollection
            , Convert.ToDateTime(calResolutionDate.Text)
            , txtResolutionComment.Text
            , journalEntryRefNum
            , txtComments.Text
            , this.RecCategoryType.Value
            , (short)ARTEnums.AccountAttribute.ReconciliationTemplate
            , SessionHelper.GetCurrentUser().LoginID, DateTime.Now
            , SessionHelper.CurrentReconciliationPeriodID.Value
            , Helper.GetAppUserInfo());
    }

    private GLDataWriteOnOffInfo GetGLDataWriteOnOffInfo()
    {
        SetExchangeRateAndRecalculateAmount();

        GLDataWriteOnOffInfo oGLDataWriteOnOffInfo = new GLDataWriteOnOffInfo();
        oGLDataWriteOnOffInfo.AddedBy = SessionHelper.CurrentUserLoginID;
        oGLDataWriteOnOffInfo.AddedByUserID = SessionHelper.GetCurrentUser().UserID;
        oGLDataWriteOnOffInfo.Amount = Convert.ToDecimal(txtAmount.Text);

        //if (!this._NetAccountID.HasValue || this._NetAccountID.Value == 0)//BCCY Changes, only populate this property when account is NOT Net Account.In case of net account, this value should be null
        if (!this.CurrentBCCY.Equals(string.Empty))
            oGLDataWriteOnOffInfo.AmountBaseCurrency = Convert.ToDecimal(lblAmountBCCYValue.Text);

        oGLDataWriteOnOffInfo.AmountReportingCurrency = Convert.ToDecimal(lblAmountRCCYValue.Text);
        oGLDataWriteOnOffInfo.Comments = txtComments.Text;
        oGLDataWriteOnOffInfo.DateAdded = DateTime.Today;
        oGLDataWriteOnOffInfo.GLDataID = this.GLDataID.Value;
        oGLDataWriteOnOffInfo.LocalCurrencyCode = ddlLocalCurrency.SelectedValue;
        oGLDataWriteOnOffInfo.OpenDate = Convert.ToDateTime(calOpenTransDate.Text);
        if (optWriteOn.Checked)
        {
            oGLDataWriteOnOffInfo.WriteOnOffID = (short?)WebEnums.WriteOnOff.WriteOn;
        }
        else if (optWriteOff.Checked)
        {
            oGLDataWriteOnOffInfo.WriteOnOffID = (short?)WebEnums.WriteOnOff.WriteOff;
        }
        else
        {
            //TODO:
        }
        oGLDataWriteOnOffInfo.GLDataWriteOnOffID = this._GLDataWriteOnOffID;
        oGLDataWriteOnOffInfo.RevisedBy = SessionHelper.CurrentUserLoginID;
        oGLDataWriteOnOffInfo.DateRevised = oGLDataWriteOnOffInfo.DateAdded;
        oGLDataWriteOnOffInfo.ReconciliationCategoryID = this.RecCategory;
        oGLDataWriteOnOffInfo.ReconciliationCategoryTypeID = this.RecCategoryType;
        oGLDataWriteOnOffInfo.RecordSourceTypeID = (short)ARTEnums.RecordSourceType.UI;
        oGLDataWriteOnOffInfo.RecordSourceID = null;
        if (IsExchangeRateOverridden)
        {
            if (this.ExRateLCCYtoBCCY.GetValueOrDefault() != 0)
                oGLDataWriteOnOffInfo.ExRateLCCYtoBCCY = this.ExRateLCCYtoBCCY;
            if (this.ExRateLCCYtoRCCY.GetValueOrDefault() != 0)
                oGLDataWriteOnOffInfo.ExRateLCCYtoRCCY = this.ExRateLCCYtoRCCY;
        }
        else
        {
            oGLDataWriteOnOffInfo.ExRateLCCYtoBCCY = null;
            oGLDataWriteOnOffInfo.ExRateLCCYtoRCCY = null;
        }
        return oGLDataWriteOnOffInfo;
    }


    #endregion

    #region Other Methods
    protected void SetExchangeRateAndRecalculateAmount()
    {
        SetExchangeRates();

        RecHelper.RecalculateRecItemAmount(txtAmount, lblAmountBCCYValue, lblAmountRCCYValue
            , lblOverriddenExRateBCCYValue, lblOverriddenExRateRCCYValue
            , ddlLocalCurrency.SelectedValue, this.CurrentBCCY, SessionHelper.ReportingCurrencyCode
            , ExRateLCCYtoBCCY, ExRateLCCYtoRCCY);
    }
    public string SetOverrideExchangeRateURL()
    {
        return Page.ResolveUrl(Helper.GetOverrideExchangeRateURLForRecItemInput(this.AccountID, this.NetAccountID, this.ExRateLCCYtoBCCY, this.ExRateLCCYtoRCCY));
    }
    #endregion
}
