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
using SkyStem.ART.Web.Data;
using SkyStem.ART.Web.Utility;
using SkyStem.ART.Web.Classes;
using SkyStem.ART.Client.IServices;
using SkyStem.ART.Client.Model;
using System.Collections.Generic;
using SkyStem.ART.Client.Data;

public partial class Pages_EditItemUnexplainedVariance : PopupPageBaseRecItem
{
    #region Variables & Constants
    private long _GLDataUnexplainedVarianceID;
    #endregion
    #region Properties
    private GLDataUnexplainedVarianceInfo _GLDataUnexplainedVarianceInfo;
    #endregion
    #region Delegates & Events
    #endregion
    #region Page Events
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            int[] oLableIdCollection = new int[0];
            PopupHelper.SetPageTitle(this, this.RecCategory.Value, this.RecCategoryType.Value, oLableIdCollection);

            txtComments.ErrorMessage = Helper.GetErrorMessage(WebEnums.FieldType.MandatoryField, 1408);

            GetQueryStringValues();
            if (!IsPostBack)
            {
                SetModeForFormView();
                PopulateItemsOnPage();
            }

            this.lblInputFormRecPeriodValue.Text = Helper.GetDisplayDate(SessionHelper.CurrentReconciliationPeriodEndDate);

            ucAccountHierarchyDetailPopup.AccountID = this.AccountID;
            this.ucAccountHierarchyDetailPopup.NetAccountID = this.NetAccountID;

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
            IUnexplainedVariance oUnExpectedVarianceClient = RemotingHelper.GetUnexplainedVarianceObject();

            GLDataUnexplainedVarianceInfo oGLDataUnexplainedVarianceInfo = this.GetGLDataUnexplainedVarianceInfo();

            if (this.Mode == QueryStringConstants.INSERT)
            {
                oGLDataUnexplainedVarianceInfo.RecCategoryID = this.RecCategory;
                oGLDataUnexplainedVarianceInfo.RecCategoryTypeID = this.RecCategoryType;
                oUnExpectedVarianceClient.InsertGLDataUnexplainedVariance(oGLDataUnexplainedVarianceInfo, (int)SessionHelper.CurrentReconciliationPeriodID, Helper.GetAppUserInfo());
            }

            else if (this.Mode == QueryStringConstants.EDIT)
            {
                oGLDataUnexplainedVarianceInfo.RecCategoryID = this.RecCategory;
                oGLDataUnexplainedVarianceInfo.RecCategoryTypeID = this.RecCategoryType;
                oGLDataUnexplainedVarianceInfo.GLDataID = this.GLDataID;
                oUnExpectedVarianceClient.UpdateGLDataUnexplainedVariance(oGLDataUnexplainedVarianceInfo, Helper.GetAppUserInfo());
            }

            if (this.ParentHiddenField != null)
            {
                this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "SetHiddenFieldStatus", ScriptHelper.GetJSToSetParentWindowElementValue(this.ParentHiddenField, "1")); // 1 means Reload data of GridVieww
            }
            this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "ClosePopupAndRefreshParentPage", ScriptHelper.GetJSForClosePopupAndSubmitParentPage());

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
    private void PopulateItemsOnPage()
    {


        if (this.Mode == QueryStringConstants.EDIT
            || this.Mode == QueryStringConstants.READ_ONLY)
        {
            IUnexplainedVariance oUnExpectedVarianceClient = RemotingHelper.GetUnexplainedVarianceObject();
            List<GLDataUnexplainedVarianceInfo> oGLDataUnexplainedVarianceInfoCollection = oUnExpectedVarianceClient.GetGLDataUnexplainedVarianceInfoCollectionByGLDataID(this.GLDataID, Helper.GetAppUserInfo());

            this._GLDataUnexplainedVarianceInfo = oGLDataUnexplainedVarianceInfoCollection.Where(recItem => recItem.GLDataUnexplainedVarianceID == this._GLDataUnexplainedVarianceID).FirstOrDefault();

            lblEnteredByValue.Text = this._GLDataUnexplainedVarianceInfo.AddedBy;
            lblAddedDate.Text = Helper.GetDisplayDate(this._GLDataUnexplainedVarianceInfo.DateAdded);
            txtComments.Text = this._GLDataUnexplainedVarianceInfo.Comments;

            //Populate Labels
            //lblAmountBC.Text = Helper.GetDisplayDecimalValue(this._GLDataUnexplainedVarianceInfo.AmountBaseCurrency);
            //lblAmountRC.Text = Helper.GetDisplayDecimalValue(this._GLDataUnexplainedVarianceInfo.AmountReportingCurrency);
            //TODO: get amount for that comment from gldataHdr
            IGLData oGLDataClient = RemotingHelper.GetGLDataObject();
            List<GLDataHdrInfo> oGLDataHdrInfoCollection = oGLDataClient.SelectGLDataHdrByGLDataID(this.GLDataID, Helper.GetAppUserInfo());
            if (oGLDataHdrInfoCollection != null && oGLDataHdrInfoCollection.Count > 0)
            {
                if (oGLDataHdrInfoCollection[0].UnexplainedVarianceBaseCurrency.HasValue)
                {
                    lblAmountBC.Text = Helper.GetDisplayDecimalValue(oGLDataHdrInfoCollection[0].UnexplainedVarianceBaseCurrency);
                }
                if (oGLDataHdrInfoCollection[0].UnexplainedVarianceReportingCurrency.HasValue)
                {
                    lblAmountRC.Text = Helper.GetDisplayDecimalValue(oGLDataHdrInfoCollection[0].UnexplainedVarianceReportingCurrency);
                }
            }

            lblCommentsValue.Text = this._GLDataUnexplainedVarianceInfo.Comments;

        }
        else
        {
            UserHdrInfo oUserHdrInfo = SessionHelper.GetCurrentUser();
            lblEnteredByValue.Text = oUserHdrInfo.FirstName + " " + oUserHdrInfo.LastName;
            lblAddedDate.Text = Helper.GetDisplayDate(DateTime.Today);

            //TODO: get amount for that comment from gldataHdr
            IGLData oGLDataClient = RemotingHelper.GetGLDataObject();
            List<GLDataHdrInfo> oGLDataHdrInfoCollection = oGLDataClient.SelectGLDataHdrByGLDataID(this.GLDataID, Helper.GetAppUserInfo());
            if (oGLDataHdrInfoCollection != null && oGLDataHdrInfoCollection.Count > 0)
            {
                if (oGLDataHdrInfoCollection[0].UnexplainedVarianceBaseCurrency.HasValue)
                {
                    lblAmountBC.Text = Helper.GetDisplayDecimalValue(oGLDataHdrInfoCollection[0].UnexplainedVarianceBaseCurrency);
                }
                if (oGLDataHdrInfoCollection[0].UnexplainedVarianceReportingCurrency.HasValue)
                {
                    lblAmountRC.Text = Helper.GetDisplayDecimalValue(oGLDataHdrInfoCollection[0].UnexplainedVarianceReportingCurrency);
                }
            }
        }


        //lblLocalCurrencyType.Text = this._GLDataUnexplainedVarianceInfo.LocalCurrencyCode;

        //BCCY Changes
        //lblTextAmountBC.Text = Helper.GetLabelIDValue(1510) + " " + Helper.GetLabelIDValue(1493) + " (" + SessionHelper.BaseCurrencyCode + ")";
        lblTextAmountBC.Text = Helper.GetLabelIDValue(1510) + " " + Helper.GetLabelIDValue(1493) + " (" + this.CurrentBCCY + ")";
        lblTextAmountRC.Text = Helper.GetLabelIDValue(1510) + " " + Helper.GetLabelIDValue(1424) + " (" + SessionHelper.ReportingCurrencyCode + ")";
        //lblCurrencyCodeBC.Text = SessionHelper.BaseCurrencyCode;
        //lblCurrencyCodeRC.Text = SessionHelper.ReportingCurrencyCode;

    }
    private void SetModeForFormView()
    {
        switch (this.Mode)
        {
            case QueryStringConstants.INSERT:
            case QueryStringConstants.EDIT:

                lblAmountBC.Visible = true;
                //lblCurrencyCodeBC.Visible = true;
                lblAmountRC.Visible = true;
                //lblCurrencyCodeRC.Visible = true;
                lblCommentsValue.Visible = false;
                txtComments.Visible = true;
                btnUpdate.Visible = true;
                break;

            default:
                lblAmountBC.Visible = true;
                //lblCurrencyCodeBC.Visible = true;
                lblAmountRC.Visible = true;
                //lblCurrencyCodeRC.Visible = true;
                btnUpdate.Visible = false;
                lblCommentsValue.Visible = true;
                txtComments.Visible = false;
                break;
        }
    }

    private void GetQueryStringValues()
    {

        if (Request.QueryString[QueryStringConstants.GL_RECONCILIATION_ITEM_INPUT_ID] != null)
            this._GLDataUnexplainedVarianceID = Convert.ToInt32(Request.QueryString[QueryStringConstants.GL_RECONCILIATION_ITEM_INPUT_ID]);
    }
    private GLDataUnexplainedVarianceInfo GetGLDataUnexplainedVarianceInfo()
    {
        GLDataUnexplainedVarianceInfo oGLDataUnexplainedVarianceInfo = new GLDataUnexplainedVarianceInfo();
        oGLDataUnexplainedVarianceInfo.AddedBy = SessionHelper.CurrentUserLoginID;
        //TODO: should we take it from ui label or from DB (GLDataHdr(unexplainedVar))

        if (!String.IsNullOrEmpty(this.CurrentBCCY))//BCCY Changes.This prop need to be populated only in case of regular accounts. In case of Net Acct, it is null
            oGLDataUnexplainedVarianceInfo.AmountBaseCurrency = Convert.ToDecimal(lblAmountBC.Text);

        oGLDataUnexplainedVarianceInfo.AmountReportingCurrency = Convert.ToDecimal(lblAmountRC.Text);
        oGLDataUnexplainedVarianceInfo.Comments = txtComments.Text;
        oGLDataUnexplainedVarianceInfo.DateAdded = DateTime.Now;
        oGLDataUnexplainedVarianceInfo.GLDataID = this.GLDataID.Value;
        oGLDataUnexplainedVarianceInfo.GLDataUnexplainedVarianceID = this._GLDataUnexplainedVarianceID;
        //oGLDataUnexplainedVarianceInfo.LocalCurrencyCode = ddlLocalCurrency.SelectedValue;
        //oGLDataUnexplainedVarianceInfo.OpenDate = Convert.ToDateTime(calOpenTransDate.Text);
        //oGLDataUnexplainedVarianceInfo.ReconciliationCategoryTypeID = this.RecCategoryType;
        oGLDataUnexplainedVarianceInfo.AddedByUserID = SessionHelper.GetCurrentUser().UserID;
        oGLDataUnexplainedVarianceInfo.AddedBy = SessionHelper.GetCurrentUser().LoginID;
        //oGLDataUnexplainedVarianceInfo.RevisedBy = SessionHelper.GetCurrentUser().LoginID;
        //oGLDataUnexplainedVarianceInfo.DateRevised = oGLDataUnexplainedVarianceInfo.DateAdded;
        oGLDataUnexplainedVarianceInfo.RecordSourceTypeID = (short)ARTEnums.RecordSourceType.UI;
        oGLDataUnexplainedVarianceInfo.RecordSourceID = null;
        return oGLDataUnexplainedVarianceInfo;
    }

    #endregion
    #region Other Methods
    #endregion

}
