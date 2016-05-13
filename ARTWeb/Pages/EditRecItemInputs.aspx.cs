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
using SkyStem.ART.Web.UserControls;
using System.Text;
using SkyStem.Library.Controls.WebControls;
using SkyStem.ART.Client.IServices;
using SkyStem.ART.Client.Model;
using System.Collections.Generic;
using SkyStem.ART.Client.Data;
using SkyStem.ART.Web.Data;
using System.Globalization;
using SkyStem.Language.LanguageUtility;

public partial class Pages_EditRecItemInputs : PopupPageBaseRecItem
{
    #region Variables & Constants
    private GLDataRecItemInfo _GLRecItemInputInfo;
    private bool _IsMultiCurrencyEnabled;
    #endregion
    #region Properties
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
            //this._RecCategoryType = Convert.ToInt16(Request.QueryString[QueryStringConstants.REC_CATEGORY_TYPE_ID].ToString());
            //_RecCategory = Convert.ToInt16(Request.QueryString[QueryStringConstants.REC_CATEGORY_ID].ToString());


            int[] oLableIdCollection = new int[0];


            PopupHelper.SetPageTitle(this, this.RecCategory.Value, this.RecCategoryType.Value, oLableIdCollection);

            SetCompanyCabalityInfo();
            //GetQueryStringValues();
            SetErrorMessagesForValidationControls();

            if (!IsPostBack)
            {
                PopulateItemsOnPage();
            }

            SetExchangeRates();
            this.lblInputFormRecPeriodValue.Text = Helper.GetDisplayDate(SessionHelper.CurrentReconciliationPeriodEndDate);

            hlDocument.NavigateUrl = "javascript:OpenRadWindowFromRadWindow('" + SetDocumentUploadURL() + "', '480', '800');";

            ucAccountHierarchyDetailPopup.AccountID = this.AccountID;
            ucAccountHierarchyDetailPopup.NetAccountID = this.NetAccountID;

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
    protected void btnOvereideExchangeRate_Click(object sender, EventArgs e)
    {
        SetExchangeRateAndRecalculateAmount();
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            // if (Page.IsValid)
            // {
            IGLDataRecItem oGLDataRecItemClient = RemotingHelper.GetGLDataRecItemObject();

            if (this.Mode == QueryStringConstants.INSERT)
            {
                GLDataRecItemInfo oGLReconciliationItemInputInfo = this.GetGLReconciliationItemInputInfo();
                List<AttachmentInfo> oAttachmentInfoCollection = (List<AttachmentInfo>)Session[SessionConstants.ATTACHMENTS];
                oGLDataRecItemClient.InsertRecInputItem(oGLReconciliationItemInputInfo, (short)ARTEnums.AccountAttribute.ReconciliationTemplate, SessionHelper.CurrentReconciliationPeriodID.Value, oAttachmentInfoCollection, Helper.GetAppUserInfo());
                SessionHelper.ClearSession(SessionConstants.ATTACHMENTS);
            }
            else if (this.IsForwardedItem.Value)
            {
                SaveCloseDateForForwardedItems();
            }
            else if (this.Mode == QueryStringConstants.EDIT)
            {
                GLDataRecItemInfo oGLReconciliationItemInputInfo = this.GetGLReconciliationItemInputInfo();
                oGLDataRecItemClient.UpdateRecInputItem(oGLReconciliationItemInputInfo, (short)ARTEnums.AccountAttribute.ReconciliationTemplate, Helper.GetAppUserInfo());
                if (!string.IsNullOrEmpty(calResolutionDate.Text))
                {
                    SaveCloseDateForForwardedItems();
                }
            }
            StringBuilder script = new StringBuilder();
            ScriptHelper.AddJSStartTag(script);
            script.Append("var oWindow = null;");
            //script.Append("debugger;");
            script.Append(System.Environment.NewLine);
            script.Append("if (window.radWindow)");
            script.Append(System.Environment.NewLine);
            script.Append("oWindow = window.RadWindow;"); //Will work in Moz in all cases, including clasic dialog       
            script.Append(System.Environment.NewLine);
            script.Append("else if (window.frameElement.radWindow)");
            script.Append(System.Environment.NewLine);
            script.Append("oWindow = window.frameElement.radWindow;");
            script.Append(System.Environment.NewLine);
            if (this.ParentHiddenField != null)
            {
                script.Append("oWindow.BrowserWindow.SetElementValue('" + this.ParentHiddenField + "','" + "1" + "');");
            }
            ScriptHelper.AddJSEndTag(script);
            this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "SetHiddenFieldStatus", script.ToString());
            this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "ClosePopupAndRefreshParentPage", ScriptHelper.GetJSForClosePopupAndSubmitParentPage(true));
            // }
            // else
            // {
            //  Page.Validate();
            // }
        }
        catch (Exception ex)
        {
            PopupHelper.ShowErrorMessage(this, ex);
        }

    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        SessionHelper.ClearSession(SessionConstants.ATTACHMENTS);
        string script = PopupHelper.GetScriptForClosingRadWindow();

        ClientScript.RegisterClientScriptBlock(this.GetType(), "CloseWindow", script);
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
            , cstVldAmount, cvOpenDate, cvResolutionDate, cvCompareOpenDateWithCurrentDate, cvResolutionDateCompareWithCurrentDate, cvResolutionDateCompareWithOpenDate
            , lblAmount.LabelID, lblCurrency.LabelID, lblOpenTransDate.LabelID, lblResolutionCloseDate.LabelID);
        //// Required Fields
        //rfvAmount.ErrorMessage = Helper.GetErrorMessage(WebEnums.FieldType.MandatoryField, lblAmount.LabelID);

        //rfvLocalCurrency.InitialValue = WebConstants.SELECT_ONE;
        //rfvLocalCurrency.ErrorMessage = Helper.GetErrorMessage(WebEnums.FieldType.MandatoryField, lblCurrency.LabelID);

        //rfvOpenTransDate.ErrorMessage = Helper.GetErrorMessage(WebEnums.FieldType.MandatoryField, lblOpenTransDate.LabelID);
        //rfvResolutionDate.ErrorMessage = Helper.GetErrorMessage(WebEnums.FieldType.MandatoryField, lblResolutionCloseDate.LabelID);

        //// Invalid Values
        //cstVldAmount.ErrorMessage = Helper.GetErrorMessage(WebEnums.FieldType.InvalidNumericField, lblAmount.LabelID);
        //cvOpenDate.ErrorMessage = Helper.GetErrorMessage(WebEnums.FieldType.InvalidDate, lblOpenTransDate.LabelID);
        //cvResolutionDate.ErrorMessage = Helper.GetErrorMessage(WebEnums.FieldType.InvalidDate, lblResolutionCloseDate.LabelID);

        //cvCompareOpenDateWithCurrentDate.ErrorMessage = Helper.GetErrorMessage(WebEnums.FieldType.DateCompareField, lblOpenTransDate.LabelID, 2062);
        //cvResolutionDateCompareWithCurrentDate.ErrorMessage = Helper.GetErrorMessage(WebEnums.FieldType.DateCompareField, lblResolutionCloseDate.LabelID, 2062);
        //cvResolutionDateCompareWithOpenDate.ErrorMessage = Helper.GetErrorMessage(WebEnums.FieldType.DateCompareFieldGreaterThan, lblResolutionCloseDate.LabelID, lblOpenTransDate.LabelID);
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
            IGLDataRecItem oGLDataRecItemClient = RemotingHelper.GetGLDataRecItemObject();
            List<GLDataRecItemInfo> oGLReconciliationItemInputInfoCollection = oGLDataRecItemClient.GetRecItem(this.GLDataID.Value, SessionHelper.CurrentReconciliationPeriodID.Value
                , this.RecCategoryType.Value, (short)WebEnums.RecordType.GLReconciliationItemInput, (short)ARTEnums.AccountAttribute.ReconciliationTemplate, Helper.GetAppUserInfo());

            this._GLRecItemInputInfo = oGLReconciliationItemInputInfoCollection.Where(recItem => recItem.GLDataRecItemID == this.GLRecInputItemID).FirstOrDefault();

            lblEnteredByValue.Text = this._GLRecItemInputInfo.UserName;
            lblAddedDate.Text = Helper.GetDisplayDate(this._GLRecItemInputInfo.DateAdded);
            lblAmountBCCYValue.Text = Helper.GetDisplayDecimalValue(this._GLRecItemInputInfo.AmountBaseCurrency);
            lblAmountRCCYValue.Text = Helper.GetDisplayDecimalValue(this._GLRecItemInputInfo.AmountReportingCurrency);
            txtAmount.Text = Helper.GetDecimalValueForTextBox(this._GLRecItemInputInfo.Amount, 4);
            txtComments.Text = this._GLRecItemInputInfo.Comments;
            txtJournalEntryRef.Text = this._GLRecItemInputInfo.JournalEntryRef.ToString();
            txtResolutionComment.Text = this._GLRecItemInputInfo.CloseComments;
            calResolutionDate.Text = Helper.GetDisplayDateForCalendar(this._GLRecItemInputInfo.CloseDate);

            //Populate Labels
            lblLocalCurrencyValue.Text = Helper.GetDisplayDecimalValue(this._GLRecItemInputInfo.Amount);
            lblCommentsValue.Text = this._GLRecItemInputInfo.Comments;
            lblJournalEntryRefValue.Text = this._GLRecItemInputInfo.JournalEntryRef.ToString();
            lblResolutionCommentValue.Text = this._GLRecItemInputInfo.CloseComments;
            lblResolutionDate.Text = Helper.GetDisplayDate(this._GLRecItemInputInfo.CloseDate);
            lblRecItemNumberValue.Text = Helper.GetDisplayStringValue(this._GLRecItemInputInfo.RecItemNumber);
            lblMatchSetRefNoValue.Text = Helper.GetDisplayStringValue(this._GLRecItemInputInfo.MatchSetRefNumber);
            calOpenTransDate.Text = Helper.GetDisplayDateForCalendar(this._GLRecItemInputInfo.OpenDate);
            lblOpenTransDateValue.Text = Helper.GetDisplayDate(this._GLRecItemInputInfo.OpenDate);

            ucExchangeRate.LCCYCode = this._GLRecItemInputInfo.LocalCurrencyCode;
            if (this._GLRecItemInputInfo.ExRateLCCYtoBCCY.HasValue)
            {
                lblOverriddenExRateBCCYValue.Text = Helper.GetDisplayExchangeRateValue(this._GLRecItemInputInfo.ExRateLCCYtoBCCY);
                ExRateLCCYtoBCCY = this._GLRecItemInputInfo.ExRateLCCYtoBCCY;
            }
            if (this._GLRecItemInputInfo.ExRateLCCYtoRCCY.HasValue)
            {
                lblOverriddenExRateRCCYValue.Text = Helper.GetDisplayExchangeRateValue(this._GLRecItemInputInfo.ExRateLCCYtoRCCY);
                ExRateLCCYtoRCCY = this._GLRecItemInputInfo.ExRateLCCYtoRCCY;
                IsExchangeRateOverridden = true;
            }
        }
        else
        {
            UserHdrInfo oUserHdrInfo = SessionHelper.GetCurrentUser();
            lblEnteredByValue.Text = oUserHdrInfo.FirstName + " " + oUserHdrInfo.LastName;
            lblAddedDate.Text = Helper.GetDisplayDate(DateTime.Today);
            //lblRecItemNumberValue.Text = "-";
        }

        ListControlHelper.BindLocalCurrencyDropDown(ddlLocalCurrency, this.GLDataID.Value, this._IsMultiCurrencyEnabled);
        if (this._IsMultiCurrencyEnabled && this.Mode != QueryStringConstants.EDIT)
        {
            ListControlHelper.AddListItemForCCY(ddlLocalCurrency);
        }
        if (this._GLRecItemInputInfo != null && this._IsMultiCurrencyEnabled && this.Mode == QueryStringConstants.EDIT)
        {
            if (this.ddlLocalCurrency.Items.FindByValue(this._GLRecItemInputInfo.LocalCurrencyCode) != null)
                this.ddlLocalCurrency.SelectedValue = this._GLRecItemInputInfo.LocalCurrencyCode;
        }
        if (this._GLRecItemInputInfo != null)
        {
            lblLocalCurrencyType.Text = this._GLRecItemInputInfo.LocalCurrencyCode;
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
                    //added by Prafull
                    lblCommentsValue.Visible = false;
                    txtComments.Visible = true;
                    //vldJournalEntryRef.Enabled = true;
                    rfvResolutionDate.Enabled = false;
                    rfvOpenTransDate.Enabled = true;
                    rfvAmount.Enabled = true;
                    cstVldAmount.Enabled = true;
                    cvCompareOpenDateWithCurrentDate.Enabled = true;
                    cvResolutionDateCompareWithCurrentDate.Enabled = true;
                    cvResolutionDateCompareWithOpenDate.Enabled = true;
                    cvOpenDate.Enabled = true;
                    cvResolutionDate.Enabled = true;
                    if (!string.IsNullOrEmpty(calResolutionDate.Text))
                    {
                        SaveCloseDateForForwardedItems();
                    }
                    break;
                }
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
                cvCompareOpenDateWithCurrentDate.Enabled = false;
                cvResolutionDateCompareWithCurrentDate.Enabled = false;
                cvResolutionDateCompareWithOpenDate.Enabled = false;
                cvOpenDate.Enabled = false;
                cvResolutionDate.Enabled = false;
                cstVldAmount.Enabled = false;
                lblCommentsValue.Visible = true;
                txtComments.Visible = false;
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
        cvCompareOpenDateWithCurrentDate.Enabled = !flag;
        cvResolutionDateCompareWithCurrentDate.Enabled = flag;
        cvResolutionDateCompareWithOpenDate.Enabled = flag;
        cvOpenDate.Enabled = !flag;
        cvResolutionDate.Enabled = flag;
    }
    private void SaveCloseDateForForwardedItems()
    {
        IGLDataRecItem oGLDataRecItemClient = RemotingHelper.GetGLDataRecItemObject();
        List<long> recItemInputIdCollection = new List<long>();
        recItemInputIdCollection.Add(this.GLRecInputItemID.Value);
        string journalEntryRefNum = string.Empty;

        if (!string.IsNullOrEmpty(txtJournalEntryRef.Text))
        {
            journalEntryRefNum = txtJournalEntryRef.Text;
        }
        oGLDataRecItemClient.UpdateGLRecItemCloseDate(this.GLDataID.Value, recItemInputIdCollection
            , Convert.ToDateTime(calResolutionDate.Text)
            , txtResolutionComment.Text
            , journalEntryRefNum
            , txtComments.Text
            , this.RecCategoryType.Value
            , (short)ARTEnums.AccountAttribute.ReconciliationTemplate
            , SessionHelper.GetCurrentUser().LoginID
            , DateTime.Now
            , SessionHelper.CurrentReconciliationPeriodID.Value
            , Helper.GetAppUserInfo());
    }
    private GLDataRecItemInfo GetGLReconciliationItemInputInfo()
    {
        SetExchangeRateAndRecalculateAmount();

        GLDataRecItemInfo oGLReconciliationItemInputInfo = new GLDataRecItemInfo();
        oGLReconciliationItemInputInfo.AddedBy = SessionHelper.CurrentUserLoginID;
        oGLReconciliationItemInputInfo.Amount = Convert.ToDecimal(txtAmount.Text);

        //if (!this._NetAccountID.HasValue || this._NetAccountID.Value == 0)//BCCY Changes, only populate this property when account is NOT Net Account.In case of net account, this value should be null
        if (!this.CurrentBCCY.Equals(string.Empty))
            oGLReconciliationItemInputInfo.AmountBaseCurrency = Convert.ToDecimal(lblAmountBCCYValue.Text);

        oGLReconciliationItemInputInfo.AmountReportingCurrency = Convert.ToDecimal(lblAmountRCCYValue.Text);
        oGLReconciliationItemInputInfo.Comments = txtComments.Text;
        oGLReconciliationItemInputInfo.DateAdded = DateTime.Now;
        oGLReconciliationItemInputInfo.GLDataID = this.GLDataID.Value;
        oGLReconciliationItemInputInfo.GLDataRecItemID = this.GLRecInputItemID;
        oGLReconciliationItemInputInfo.LocalCurrencyCode = ddlLocalCurrency.SelectedValue;
        oGLReconciliationItemInputInfo.OpenDate = Convert.ToDateTime(calOpenTransDate.Text);
        oGLReconciliationItemInputInfo.ReconciliationCategoryTypeID = this.RecCategoryType;
        oGLReconciliationItemInputInfo.AddedByUserID = SessionHelper.GetCurrentUser().UserID;
        oGLReconciliationItemInputInfo.RevisedBy = SessionHelper.CurrentUserLoginID;
        oGLReconciliationItemInputInfo.DateRevised = oGLReconciliationItemInputInfo.DateAdded;
        oGLReconciliationItemInputInfo.ReconciliationCategoryTypeID = this.RecCategoryType;
        oGLReconciliationItemInputInfo.RecordSourceTypeID = (short)ARTEnums.RecordSourceType.UI;
        oGLReconciliationItemInputInfo.RecordSourceID = null;
        if (IsExchangeRateOverridden)
        {
            if (this.ExRateLCCYtoBCCY.GetValueOrDefault() != 0)
                oGLReconciliationItemInputInfo.ExRateLCCYtoBCCY = this.ExRateLCCYtoBCCY;
            if (this.ExRateLCCYtoRCCY.GetValueOrDefault() != 0)
                oGLReconciliationItemInputInfo.ExRateLCCYtoRCCY = this.ExRateLCCYtoRCCY;
        }
        else
        {
            oGLReconciliationItemInputInfo.ExRateLCCYtoBCCY = null;
            oGLReconciliationItemInputInfo.ExRateLCCYtoRCCY = null;
        }
        return oGLReconciliationItemInputInfo;
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
    public string SetDocumentUploadURL()
    {
        string windowName;
        string url = Helper.SetDocumentUploadURLForRecItemInput(this.GLDataID, this.GLRecInputItemID, this.AccountID, this.NetAccountID, (this.Mode == QueryStringConstants.READ_ONLY), Request.Url.ToString(), out windowName, this.IsForwardedItem.Value, WebEnums.RecordType.GLReconciliationItemInput);

        return url;
    }

    public string SetOverrideExchangeRateURL()
    {
        return Page.ResolveUrl(Helper.GetOverrideExchangeRateURLForRecItemInput(this.AccountID, this.NetAccountID, this.ExRateLCCYtoBCCY, this.ExRateLCCYtoRCCY));
    }
    #endregion

}
