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
using SkyStem.ART.Client.IServices;
using SkyStem.ART.Web.Data;
using SkyStem.ART.Client.Model;
using SkyStem.Library.Controls.WebControls;
using System.Collections.Generic;
using Telerik.Web.UI;
using SkyStem.ART.Client.Data;
using SkyStem.Library.Controls.TelerikWebControls.Data;
using SkyStem.Library.Controls.TelerikWebControls;
using SkyStem.ART.Client.Exception;

public partial class Pages_BulkCloseWriteOffOn : PopupPageBaseRecItem
{

    #region Variables & Constants
    public short _GLReconciliationItemInputRecordTypeID = 0;
    #endregion

    #region Properties
    private List<GLDataWriteOnOffInfo> _GLDataWriteOnOffInfoCollection = null;
    public bool IsMultiCurrencyActivated
    {
        get { return (bool)ViewState["IsMultiCurrencyActivated"]; }
        set { ViewState["IsMultiCurrencyActivated"] = value; }
    }
    public List<GLDataWriteOnOffInfo> GLDataWriteOnOffInfoCollection
    {
        get { return (List<GLDataWriteOnOffInfo>)Session["GLDataWriteOnOffInfoCollection"]; }
        set { Session["GLDataWriteOnOffInfoCollection"] = value; }
    }
    #endregion

    #region Delegates & Events
    #endregion

    #region Page Events
    protected void Page_Init(object sender, EventArgs e)
    {
        try
        {
            ucGLDataWriteOnOffGrid.EntityNameLabelID = 1080;
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
                this.ucGLDataWriteOnOffGrid.GLDataHdrInfo = this.GLDataHdrInfo;
                IsMultiCurrencyActivated = Helper.IsCapabilityActivatedForCurrentRecPeriod(ARTEnums.Capability.MultiCurrency);
                ucGLDataWriteOnOffGrid.IsMultiCurrencyActivated = IsMultiCurrencyActivated;

            }
            //if (Request.QueryString[QueryStringConstants.REC_CATEGORY_ID] != null)
            //    _RecCategory = Convert.ToInt16(Request.QueryString[QueryStringConstants.REC_CATEGORY_ID].ToString());
            //if (Request.QueryString[QueryStringConstants.REC_CATEGORY_TYPE_ID] != null)
            //    this._RecCategoryType = Convert.ToInt16(Request.QueryString[QueryStringConstants.REC_CATEGORY_TYPE_ID]);
            PopupHelper.SetPageTitle(this, this.RecCategory.Value, this.RecCategoryType.Value, 1771);
            //GetQueryStringValues();
            ucAccountHierarchyDetailPopup.AccountID = this.AccountID.Value;
            if (!IsPostBack)
            {
                PopulateItemsOnPage();
            }
            ucGLDataWriteOnOffGrid.GridItemDataBound += new GridItemEventHandler(ucGLDataWriteOnOffGrid_GridItemDataBound);
        }
        catch (Exception ex)
        {
            PopupHelper.ShowErrorMessage(this, ex);
        }
    }
    #endregion

    #region Grid Events
    void ucGLDataWriteOnOffGrid_GridItemDataBound(object sender, GridItemEventArgs e)
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
    protected void btnApplyFilter_Click(object sender, EventArgs e)
    {
        ucGLDataWriteOnOffGrid.ApplyFilterGLDataWriteOnOffsGrid();
    }
    protected void btnClose_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            try
            {
                List<long> glDataWriteOnOffIdCollection = null;
                glDataWriteOnOffIdCollection = ucGLDataWriteOnOffGrid.SelectedGLDataWriteOnOffIDs();
                if (glDataWriteOnOffIdCollection != null && glDataWriteOnOffIdCollection.Count > 0)
                {
                    IGLDataWriteOnOff oGLDataWriteOnOffClient = RemotingHelper.GetGLDataWriteOnOffObject();
                    oGLDataWriteOnOffClient.UpdateGLDataWriteOnOffCloseDate(this.GLDataID.Value, glDataWriteOnOffIdCollection
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
                List<long> glDataWriteOnOffIdCollection = null;
                if (GLDataWriteOnOffInfoCollection != null && GLDataWriteOnOffInfoCollection.Count > 0)
                    glDataWriteOnOffIdCollection = (List<long>)(from obj in GLDataWriteOnOffInfoCollection
                                                                select obj.GLDataWriteOnOffID.Value).ToList();
                if (glDataWriteOnOffIdCollection != null && glDataWriteOnOffIdCollection.Count > 0)
                {
                    IGLDataWriteOnOff oGLDataWriteOnOffClient = RemotingHelper.GetGLDataWriteOnOffObject();
                    oGLDataWriteOnOffClient.UpdateGLDataWriteOnOffCloseDate(this.GLDataID.Value, glDataWriteOnOffIdCollection
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
    protected void cvGLDataWriteOnOffItems_ServerValidate(object source, ServerValidateEventArgs args)
    {
        try
        {
            List<GLDataWriteOnOffInfo> oGLDataWriteOnOffInfoCollection = ucGLDataWriteOnOffGrid.SelectedGLDataWriteOnOffInfoCollection();
            validate(source, args, oGLDataWriteOnOffInfoCollection);
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
    protected void cvCloseAllGLDataWriteOnOffItems_ServerValidate(object source, ServerValidateEventArgs args)
    {
        try
        {
            validate(source, args, GLDataWriteOnOffInfoCollection);
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
        _GLReconciliationItemInputRecordTypeID = (short)WebEnums.RecordType.GLReconciliationItemInput;
        IGLDataWriteOnOff oGLDataWriteOnOffClient = RemotingHelper.GetGLDataWriteOnOffObject();
        this._GLDataWriteOnOffInfoCollection = oGLDataWriteOnOffClient.GetGLDataWriteOnOffInfoCollectionByGLDataID(GLDataID, (short)ARTEnums.AccountAttribute.ReconciliationTemplate, Helper.GetAppUserInfo());
        List<GLDataWriteOnOffInfo> oGLDataWriteOnOffInfoCollection = (from recItem in this._GLDataWriteOnOffInfoCollection
                                                                      where recItem.CloseDate == null
                                                                      // Commented by manoj : Since multi-version GL capability is available we should consider allowing users to close items in the current period
                                                                      //&& recItem.IsForwardedItem == true // && condition added by Harsh on 27th Feb 2011
                                                                      select recItem).ToList();

        ucGLDataWriteOnOffGrid.SetGLDataWriteOnOffGridData(oGLDataWriteOnOffInfoCollection);
        GLDataWriteOnOffInfoCollection = oGLDataWriteOnOffInfoCollection;
        ucGLDataWriteOnOffGrid.LoadData();
    }
    private void validate(object source, ServerValidateEventArgs args, List<GLDataWriteOnOffInfo> oGLDataWriteOnOffInfoCollection)
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
                if (oGLDataWriteOnOffInfoCollection != null && oGLDataWriteOnOffInfoCollection.Count > 0)
                {
                    for (int i = 0; i < oGLDataWriteOnOffInfoCollection.Count; i++)
                    {
                        DateTime OpenDate;
                        if (DateTime.TryParse(oGLDataWriteOnOffInfoCollection[i].OpenDate.ToString(), out OpenDate))
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
