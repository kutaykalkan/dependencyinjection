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
using SkyStem.ART.Web.Classes;
using SkyStem.ART.Web.Data;
using SkyStem.ART.Client.Model;
using SkyStem.Library.Controls.WebControls;
using Telerik.Web.UI;
using SkyStem.ART.Client.Data;
using SkyStem.ART.Client.IServices;
using System.Collections.Generic;
using SkyStem.Language.LanguageUtility;
using SkyStem.Library.Controls.TelerikWebControls.Data;
using SkyStem.Library.Controls.TelerikWebControls;
using System.Text;
using SkyStem.ART.Client.Exception;


public partial class Pages_GLAdjustmentBulkClose : PopupPageBaseRecItem
{
    #region Variables & Constants
    short _GLReconciliationItemInputRecordTypeID = 0;
    #endregion

    #region Properties
    private List<GLDataRecItemInfo> _GLRecItemInfoCollection = null;
    public bool IsMultiCurrencyActivated
    {
        get { return (bool)ViewState["IsMultiCurrencyActivated"]; }
        set { ViewState["IsMultiCurrencyActivated"] = value; }
    }
    public List<GLDataRecItemInfo> GLReconciliationItemInputInfoCollection
    {
        get { return (List<GLDataRecItemInfo>)Session["GLReconciliationItemInputInfoCollection"]; }
        set { Session["GLReconciliationItemInputInfoCollection"] = value; }
    }


    #endregion

    #region Delegates & Events
    #endregion

    #region Page Events
    protected void Page_Init(object sender, EventArgs e)
    {
        try
        {
            ucGLDataRecItemGrid.EntityNameLabelID = 1080;
        }
        catch (Exception ex)
        {
            PopupHelper.ShowErrorMessage(this, ex);
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                this.ucGLDataRecItemGrid.GLDataHdrInfo = this.GLDataHdrInfo;
                IsMultiCurrencyActivated = Helper.IsCapabilityActivatedForCurrentRecPeriod(ARTEnums.Capability.MultiCurrency);
                ucGLDataRecItemGrid.IsMultiCurrencyActivated = IsMultiCurrencyActivated;
            }
            //if (Request.QueryString[QueryStringConstants.REC_CATEGORY_TYPE_ID] != null)
            //{
            //    this._RecCategoryType = Convert.ToInt16(Request.QueryString[QueryStringConstants.REC_CATEGORY_TYPE_ID].ToString());
            //    _RecCategory = Convert.ToInt16(Request.QueryString[QueryStringConstants.REC_CATEGORY_ID].ToString());
            //}
            PopupHelper.SetPageTitle(this, this.RecCategory.Value, this.RecCategoryType.Value, 1771);
            //GetQueryStringValues();
            ucAccountHierarchyDetailPopup.AccountID = this.AccountID.Value;
            ucAccountHierarchyDetailPopup.NetAccountID = this.NetAccountID;
            if (!IsPostBack)
            {
                PopulateItemsOnPage();
            }
            ucGLDataRecItemGrid.GridItemDataBound += new GridItemEventHandler(ucGLDataRecItemGrid_GridItemDataBound);
        }
        catch (Exception ex)
        {
            PopupHelper.ShowErrorMessage(this, ex);
        }
    }
    #endregion

    #region Grid Events
    void ucGLDataRecItemGrid_GridItemDataBound(object sender, GridItemEventArgs e)
    {
        if (e.Item.ItemType == Telerik.Web.UI.GridItemType.Item || e.Item.ItemType == Telerik.Web.UI.GridItemType.AlternatingItem)
        {
            GridDataItem oGridDataItem = e.Item as GridDataItem;
            DataRow dr = ((DataRowView)oGridDataItem.DataItem).Row;
            SetMatchSetRefNumberUrlForGLDataRecItem(e);
        }
    }
    #endregion

    #region Other Events
    protected void btnClose_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            try
            {
                List<long> glRecItemInputIdCollection = new List<long>();
                glRecItemInputIdCollection = ucGLDataRecItemGrid.SelectedGLDataRecItemIDs();
                IGLDataRecItem oGLRecItemInputClient = RemotingHelper.GetGLDataRecItemObject();
                oGLRecItemInputClient.UpdateGLRecItemCloseDate(this.GLDataID.Value, glRecItemInputIdCollection
                    , Convert.ToDateTime(calResolutionDate.Text), null, null, null, this.RecCategoryType.Value, (short)ARTEnums.AccountAttribute.ReconciliationTemplate
                    , SessionHelper.GetCurrentUser().LoginID, DateTime.Now, SessionHelper.CurrentReconciliationPeriodID.Value, Helper.GetAppUserInfo());

                if (!String.IsNullOrEmpty(this.ParentHiddenField))
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
    }
    protected void btnCloseAll_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            try
            {
                List<long> glRecItemInputIdCollection = new List<long>();
                if (GLReconciliationItemInputInfoCollection != null && GLReconciliationItemInputInfoCollection.Count > 0)
                    glRecItemInputIdCollection = (List<long>)(from obj in GLReconciliationItemInputInfoCollection
                                                              select obj.GLDataRecItemID.Value).ToList();
                if (glRecItemInputIdCollection != null && glRecItemInputIdCollection.Count > 0)
                {
                    IGLDataRecItem oGLRecItemInputClient = RemotingHelper.GetGLDataRecItemObject();
                    oGLRecItemInputClient.UpdateGLRecItemCloseDate(this.GLDataID.Value, glRecItemInputIdCollection
                        , Convert.ToDateTime(calResolutionDate.Text), null, null, null, this.RecCategoryType.Value, (short)ARTEnums.AccountAttribute.ReconciliationTemplate
                        , SessionHelper.GetCurrentUser().LoginID, DateTime.Now, SessionHelper.CurrentReconciliationPeriodID.Value, Helper.GetAppUserInfo());

                    if (!String.IsNullOrEmpty(this.ParentHiddenField))
                    {
                        this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "SetHiddenFieldStatus", ScriptHelper.GetJSToSetParentWindowElementValue(this.ParentHiddenField, "1")); // 1 means Reload data of GridVieww
                    }
                    this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "ClosePopupAndRefreshParentPage", ScriptHelper.GetJSForClosePopupAndSubmitParentPage());
                }
            }
            catch (Exception ex)
            {
                PopupHelper.ShowErrorMessage(this, ex);
            }
        }
    }
    protected void btnApplyFilter_Click(object sender, EventArgs e)
    {
        ucGLDataRecItemGrid.ApplyFilterGLDataRecItemsGrid();
    }
    #endregion

    #region Validation Control Events
    protected void cvValidateNormalRecItems_ServerValidate(object source, ServerValidateEventArgs args)
    {
        try
        {
            List<GLDataRecItemInfo> oGLReconciliationItemInputInfoCollection = ucGLDataRecItemGrid.SelectedGLDataRecItemInfoCollection();
            validate(source, args, oGLReconciliationItemInputInfoCollection);
        }
        catch (ARTException ex)
        {
            Helper.ShowErrorMessage((PageBase)this.Page, ex);
        }
        catch (Exception ex)
        {
            Helper.ShowErrorMessage((PageBase)this.Page, ex);
        }
    }
    protected void cvValidateCloseAllNormalRecItems_ServerValidate(object source, ServerValidateEventArgs args)
    {
        try
        {
            validate(source, args, GLReconciliationItemInputInfoCollection);
        }
        catch (ARTException ex)
        {
            Helper.ShowErrorMessage((PageBase)this.Page, ex);
        }
        catch (Exception ex)
        {
            Helper.ShowErrorMessage((PageBase)this.Page, ex);
        }
    }
    #endregion

    #region Private Methods
    private void SetMatchSetRefNumberUrlForGLDataRecItem(GridItemEventArgs e)
    {
        GridDataItem oGridDataItem = e.Item as GridDataItem;
        DataRow dr = ((DataRowView)oGridDataItem.DataItem).Row;
        ExHyperLink hlMatchSetRefNumber = (ExHyperLink)e.Item.FindControl("hlMatchSetRefNumber");

        if (!string.IsNullOrEmpty(Convert.ToString(dr["MatchSetRefNumber"])))
        {
            hlMatchSetRefNumber.Text = Helper.GetDisplayStringValue(dr["MatchSetRefNumber"].ToString());
        }
    }
    private void PopulateItemsOnPage()
    {
        this._GLReconciliationItemInputRecordTypeID = (short)WebEnums.RecordType.GLReconciliationItemInput;

        IGLDataRecItem oGLRecItemInput = RemotingHelper.GetGLDataRecItemObject();
        this._GLRecItemInfoCollection = oGLRecItemInput.GetRecItem(this.GLDataID.Value, SessionHelper.CurrentReconciliationPeriodID.Value, this.RecCategoryType.Value, this._GLReconciliationItemInputRecordTypeID, (short)ARTEnums.AccountAttribute.ReconciliationTemplate, Helper.GetAppUserInfo());
        List<GLDataRecItemInfo> oGLReconciliationItemInputInfoCollection = (from recItem in this._GLRecItemInfoCollection
                                                                            where recItem.CloseDate == null
                                                                            select recItem).ToList();
        ucGLDataRecItemGrid.SetGLDataRecItemGridData(oGLReconciliationItemInputInfoCollection);
        GLReconciliationItemInputInfoCollection = oGLReconciliationItemInputInfoCollection;
        ucGLDataRecItemGrid.LoadData();
    }
    private void validate(object source, ServerValidateEventArgs args, List<GLDataRecItemInfo> oGLReconciliationItemInputInfoCollection)
    {
        ExCustomValidator cv = (ExCustomValidator)source;

        bool isErrorExist = false;
        DateTime CloseDate;
        if (!string.IsNullOrEmpty(calResolutionDate.Text))
        {
            if (DateTime.TryParse(calResolutionDate.Text, out CloseDate))
            {
                if (CloseDate > Convert.ToDateTime(DateTime.Now))
                {
                    isErrorExist = true;
                    cv.ErrorMessage = Helper.GetErrorMessage(WebEnums.FieldType.DateCompareField, 1411, 2062);
                }
                if (oGLReconciliationItemInputInfoCollection != null && oGLReconciliationItemInputInfoCollection.Count > 0)
                {
                    for (int i = 0; i < oGLReconciliationItemInputInfoCollection.Count; i++)
                    {
                        DateTime OpenDate;
                        if (DateTime.TryParse(oGLReconciliationItemInputInfoCollection[i].OpenDate.ToString(), out OpenDate))
                        {
                            if (CloseDate <= OpenDate)
                            {
                                isErrorExist = true;
                                cv.ErrorMessage = Helper.GetErrorMessage(WebEnums.FieldType.DateCompareFieldGreaterThan, 1411, 1511);
                            }
                        }
                    }
                }
                else
                {
                    isErrorExist = true;
                    cv.ErrorMessage = Helper.GetLabelIDValue(2013);
                }
            }
            else
            {
                isErrorExist = true;
                cv.ErrorMessage = Helper.GetErrorMessage(WebEnums.FieldType.InvalidDate, 1411);
            }
        }
        else
        {
            isErrorExist = true;
            cv.ErrorMessage = Helper.GetErrorMessage(WebEnums.FieldType.MandatoryField, 1411);
        }
        if (isErrorExist)
        {
            args.IsValid = false;
        }
    }
    #endregion

    #region Other Methods
    #endregion


}
