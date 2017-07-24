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
using SkyStem.Language.LanguageUtility;
using SkyStem.ART.Web.Data;
using SkyStem.ART.Client.IServices;
using SkyStem.ART.Client.Model;
using System.Collections.Generic;
using SkyStem.Library.Controls.WebControls;
using SkyStem.ART.Client.Data;
using Telerik.Web.UI;
using SkyStem.Library.Controls.TelerikWebControls.Data;
using SkyStem.Library.Controls.TelerikWebControls;
using SkyStem.ART.Client.Exception;

public partial class Pages_BulkCloseGLDataRecurringScheduleItems : PopupPageBaseRecItem
{
    #region Variables & Constants
    #endregion

    #region Properties
    private List<GLDataRecurringItemScheduleInfo> _GLDataRecurringItemScheduleInfoCollection = null;

    #endregion

    #region Delegates & Events
    #endregion

    #region Page Events
    protected void Page_Init(object sender, EventArgs e)
    {
        try
        {
            ucGLDataRecurringScheduleItemsGrid.BasePageTitleLabelID = 1525;
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
                this.ucGLDataRecurringScheduleItemsGrid.GLDataHdrInfo = this.GLDataHdrInfo;
                IsMultiCurrencyActivated = Helper.IsCapabilityActivatedForCurrentRecPeriod(ARTEnums.Capability.MultiCurrency);
                ucGLDataRecurringScheduleItemsGrid.IsMultiCurrencyActivated = IsMultiCurrencyActivated;
            }
            ucGLDataRecurringScheduleItemsGrid.GridItemDataBound += new GridItemEventHandler(ucGLDataRecurringScheduleItemsGrid_GridItemDataBound);
            //if (Request.QueryString[QueryStringConstants.REC_CATEGORY_ID] != null)
            //    _RecCategory = Convert.ToInt16(Request.QueryString[QueryStringConstants.REC_CATEGORY_ID].ToString());

            //if (Request.QueryString[QueryStringConstants.REC_CATEGORY_TYPE_ID] != null)
            //    this.RecCategoryType = Convert.ToInt16(Request.QueryString[QueryStringConstants.REC_CATEGORY_TYPE_ID]);
            PopupHelper.SetPageTitle(this, this.RecCategory.Value, this.RecCategoryType.Value, 1771);
            //GetQueryStringValues();
            ucAccountHierarchyDetailPopup.AccountID = this.AccountID.Value;
            if (!IsPostBack)
            {
                PopulateItemsOnPage();
            }
        }
        catch (Exception ex)
        {
            PopupHelper.ShowErrorMessage(this, ex);
        }
    }
    #endregion

    #region Grid Events
    void ucGLDataRecurringScheduleItemsGrid_GridItemDataBound(object sender, GridItemEventArgs e)
    {
        if (RecCategoryType == (short)WebEnums.RecCategoryType.Amortizable_SupportingDetail_RecurringAmortizableSchedule)
        {

            ucGLDataRecurringScheduleItemsGrid.SetAmortizableGridHeaders(e);
        }
        else
        {
            ucGLDataRecurringScheduleItemsGrid.SetAccruableGridHeaders(e);
        }
        if (e.Item.ItemType == Telerik.Web.UI.GridItemType.Item || e.Item.ItemType == Telerik.Web.UI.GridItemType.AlternatingItem)
        {
            GridDataItem oGridDataItem = e.Item as GridDataItem;
            DataRow dr = ((DataRowView)oGridDataItem.DataItem).Row;
            SetMatchSetRefNumberUrlForGLDataRecItem(e);
        }

    }
    #endregion

    #region Other Events
    protected void btnApplyFilter_Click(object sender, EventArgs e)
    {
        ucGLDataRecurringScheduleItemsGrid.ApplyFilterGLDataRecurringScheduleItemsGrid();
    }
    protected void btnClose_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            try
            {
                List<long> GLDataRecurringItemScheduleIDCollection = null;
                GLDataRecurringItemScheduleIDCollection = ucGLDataRecurringScheduleItemsGrid.SelectedGLDataRecurringItemScheduleIDs();
                if (GLDataRecurringItemScheduleIDCollection != null && GLDataRecurringItemScheduleIDCollection.Count > 0)
                {
                    IGLDataRecItemSchedule oGLDataRecItemScheduleClient = RemotingHelper.GetGLDataRecItemScheduleObject();
                    oGLDataRecItemScheduleClient.UpdateGLDataRecurringItemScheduleCloseDate(this.GLDataID.Value, GLDataRecurringItemScheduleIDCollection
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
    protected void btnCloseAll_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            try
            {
                List<long> GLDataRecurringItemScheduleIDCollection = null;
                if (GLDataRecurringItemScheduleInfoCollection != null && GLDataRecurringItemScheduleInfoCollection.Count > 0)
                    GLDataRecurringItemScheduleIDCollection = (List<long>)(from obj in GLDataRecurringItemScheduleInfoCollection
                                                                           select obj.GLDataRecurringItemScheduleID.Value).ToList();

                if (GLDataRecurringItemScheduleIDCollection != null && GLDataRecurringItemScheduleIDCollection.Count > 0)
                {
                    IGLDataRecItemSchedule oGLDataRecItemScheduleClient = RemotingHelper.GetGLDataRecItemScheduleObject();
                    oGLDataRecItemScheduleClient.UpdateGLDataRecurringItemScheduleCloseDate(this.GLDataID.Value, GLDataRecurringItemScheduleIDCollection
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
    #endregion

    #region Validation Control Events
    protected void cvGLDataRecurringItemScheduleItems_ServerValidate(object source, ServerValidateEventArgs args)
    {
        try
        {
            List<GLDataRecurringItemScheduleInfo> oGLDataRecurringItemScheduleInfoCollection = ucGLDataRecurringScheduleItemsGrid.SelectedGLDataRecurringItemScheduleInfoCollection();
            validate(source, args, oGLDataRecurringItemScheduleInfoCollection);
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
    protected void cvCloseAllGLDataRecurringItemScheduleItems_ServerValidate(object source, ServerValidateEventArgs args)
    {
        try
        {
            validate(source, args, GLDataRecurringItemScheduleInfoCollection);
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
    private void PopulateItemsOnPage()
    {
        GLDataRecurringItemScheduleInfoCollection = GetGLDataRecurringItemScheduleInfoToBeClosed();
        ucGLDataRecurringScheduleItemsGrid.SetGLDataRecurringItemScheduleItemGridData(GLDataRecurringItemScheduleInfoCollection);
        ucGLDataRecurringScheduleItemsGrid.LoadData();
    }
    private void validate(object source, ServerValidateEventArgs args, List<GLDataRecurringItemScheduleInfo> oGLDataRecurringItemScheduleInfoCollection)
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
                if (oGLDataRecurringItemScheduleInfoCollection != null && oGLDataRecurringItemScheduleInfoCollection.Count > 0)
                {
                    for (int i = 0; i < oGLDataRecurringItemScheduleInfoCollection.Count; i++)
                    {
                        DateTime OpenDate;
                        if (DateTime.TryParse(oGLDataRecurringItemScheduleInfoCollection[i].OpenDate.ToString(), out OpenDate))
                        {
                            if (CloseDate < OpenDate)
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
    private List<GLDataRecurringItemScheduleInfo> GetGLDataRecurringItemScheduleInfoToBeClosed()
    {
        IGLDataRecItemSchedule oGLDataRecItemScheduleClient = RemotingHelper.GetGLDataRecItemScheduleObject();
        this._GLDataRecurringItemScheduleInfoCollection = oGLDataRecItemScheduleClient.GetGLDataRecurringItemSchedule(GLDataID, Helper.GetAppUserInfo());
        List<GLDataRecurringItemScheduleInfo> oGLDataRecurringItemScheduleInfoCollection =
                                                    (from recItem in this._GLDataRecurringItemScheduleInfoCollection
                                                     //  where recItem.IsForwardedItem == true
                                                     where recItem.CloseDate == null
                                                     //TODO:(commented only for test purpose) 
                                                     //&& recItem.CreatedInRecPeriodID != SessionHelper.CurrentReconciliationPeriodID
                                                     select recItem).ToList();
        return oGLDataRecurringItemScheduleInfoCollection;
    }
    #endregion

    #region Other Methods
    public bool IsMultiCurrencyActivated
    {
        get { return (bool)ViewState["IsMultiCurrencyActivated"]; }
        set { ViewState["IsMultiCurrencyActivated"] = value; }
    }
    public List<GLDataRecurringItemScheduleInfo> GLDataRecurringItemScheduleInfoCollection
    {
        get { return (List<GLDataRecurringItemScheduleInfo>)Session["GLDataRecurringItemScheduleInfoCollection"]; }
        set { Session["GLDataRecurringItemScheduleInfoCollection"] = value; }
    }
    public static void SetMatchSetRefNumberUrlForGLDataRecItem(GridItemEventArgs e)
    {
        GridDataItem oGridDataItem = e.Item as GridDataItem;
        DataRow dr = ((DataRowView)oGridDataItem.DataItem).Row;
        ExHyperLink hlMatchSetRefNumber = (ExHyperLink)e.Item.FindControl("hlMatchSetRefNumber");
        if (!string.IsNullOrEmpty(Convert.ToString(dr["MatchSetRefNumber"])))
        {
            hlMatchSetRefNumber.Text = Helper.GetDisplayStringValue(dr["MatchSetRefNumber"].ToString());
        }
    }
    #endregion


}

