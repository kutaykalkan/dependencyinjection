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
using Telerik.Web.UI;
using SkyStem.ART.Client.Model;
using SkyStem.ART.Web.Data;
using SkyStem.ART.Web.Utility;
using SkyStem.ART.Client.IServices;
using System.Collections.Generic;
using SkyStem.Library.Controls.WebControls;
using SkyStem.Language.LanguageUtility;
using SkyStem.ART.Client.Exception;
using SkyStem.ART.Client.Data;

public partial class Pages_UnExplainedVarianceHistory : PageBaseCompany
{
    long? _glDataID = 0;
    int? _netAccountID = 0;
    private List<GLDataUnexplainedVarianceInfo> oUnExpVarInfoCollection
    {
        get
        {
            return (List<GLDataUnexplainedVarianceInfo>)Session[SessionConstants.TEMPGLDATAUNEXPECTEDVARIANCE_LIST];
        }
        set
        {
            Session[SessionConstants.TEMPGLDATAUNEXPECTEDVARIANCE_LIST] = value;
        }
    }
    protected void Page_Init(object sender, EventArgs e)
    {
        MasterPageBase ompage = (MasterPageBase)this.Master.Master;
        ompage.ReconciliationPeriodChangedEventHandler += new EventHandler(ompage_ReconciliationPeriodChangedEventHandler);
        if (!this.IsPostBack)
        {
            this.btnCancel.PostBackUrl = Request.UrlReferrer.PathAndQuery;
            this.oUnExpVarInfoCollection = null;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        OnPageLoad();
    }

    private void OnPageLoad()
    {
        MasterPageBase oMasterPageBase = (MasterPageBase)this.Master.Master;
        Helper.SetPageTitle(this, 1391);
        int? accountID = 0;
        if (Request.QueryString[QueryStringConstants.ACCOUNT_ID] != null)
            accountID = Convert.ToInt32(Request.QueryString[QueryStringConstants.ACCOUNT_ID]);
        //ucAccountHierarchyDetail.AccountID =(long) accountID;
        ucAccountHierarchyDetail.Visible = false;

        // Set the Master Page Properties for GL Data ID

        if (!string.IsNullOrEmpty(Request.QueryString[QueryStringConstants.NETACCOUNT_ID]))
            _netAccountID = Convert.ToInt32(Request.QueryString[QueryStringConstants.NETACCOUNT_ID]);

        _glDataID = Helper.GetGLDataIDAndRecStatusForPeriodChange(accountID, _netAccountID, ARTEnums.ReconciliationItemTemplate.BankForm);

        RecHelper.SetRecStatusBarPropertiesForOtherPages(this, this._glDataID);

        GridColumn col = rgUnExpectedVarianceHistory.Columns.FindByUniqueNameSafe("Amount");
        if (col != null)
        {
            col.HeaderText = string.Format("{0} ({1})", LanguageUtil.GetValue(1678), SessionHelper.ReportingCurrencyCode);
        }
        BindData();
    }
    protected void Page_PreRender(object sender, EventArgs e)
    {
        int lastPagePhreaseID = Helper.GetPageTitlePhraseID(this.GetPreviousPageName());
        if (lastPagePhreaseID != -1)
            Helper.SetBreadcrumbs(this, 1071, 1187, lastPagePhreaseID, 1391);
        else
            Helper.SetBreadcrumbs(this, 1071, 1187, 1391);
    }
    #region "Rad Grid EventHandlers"
    protected void rgUnExpectedVarianceHistory_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
    {
        this.rgUnExpectedVarianceHistory.DataSource = this.oUnExpVarInfoCollection;
    }
    #endregion
    public override string GetMenuKey()
    {
        return "AccountViewer";
    }
    private string GetPreviousPageName()
    {
        string PName = "";
        if (Request.UrlReferrer != null)
        {
            PName = Request.UrlReferrer.Segments[Request.UrlReferrer.Segments.Length - 1];
        }
        return PName;
    }

    protected void rgUnExpectedVariance_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {
        if (e.Item.ItemType == Telerik.Web.UI.GridItemType.Item || e.Item.ItemType == Telerik.Web.UI.GridItemType.AlternatingItem)
        {
            GLDataUnexplainedVarianceInfo oGLDataUnexplainedVarianceInfo = (GLDataUnexplainedVarianceInfo)e.Item.DataItem;
            ExLabel lblAddedBy = (ExLabel)e.Item.FindControl("lblAddedBy");
            ExLabel lblPeriodEndDate = (ExLabel)e.Item.FindControl("lblPeriodEndDate");
            ExLabel lblDateAdded = (ExLabel)e.Item.FindControl("lblDateAdded");
            ExLabel lblAmount = (ExLabel)e.Item.FindControl("lblAmount");
            ExLabel lblComments = (ExLabel)e.Item.FindControl("lblComments");

            lblPeriodEndDate.Text = Helper.GetDisplayDate(oGLDataUnexplainedVarianceInfo.PeriodEndDate);
            lblDateAdded.Text = Helper.GetDisplayDate(oGLDataUnexplainedVarianceInfo.DateAdded);
            lblAddedBy.Text = Helper.GetDisplayStringValue(oGLDataUnexplainedVarianceInfo.AddedByUserInfo.Name);
            lblAmount.Text = Helper.GetDisplayDecimalValue(oGLDataUnexplainedVarianceInfo.AmountReportingCurrency);
            lblComments.Text = Helper.GetDisplayStringValue(oGLDataUnexplainedVarianceInfo.Comments);
        }
    }

    protected void ompage_ReconciliationPeriodChangedEventHandler(object sender, EventArgs e)
    {
        try
        {
            this.oUnExpVarInfoCollection = null;
            OnPageLoad();
            if (_glDataID != 0)
            {
                MasterPageBase ompage = (MasterPageBase)this.Master.Master;
                Helper.HideMessage(this);
            }
        }
        catch (ARTException ex)
        {
            Helper.ShowErrorMessage(this, ex);
        }
        catch (Exception ex)
        {
            Helper.ShowErrorMessage(this, ex);
        }
    }

    private void BindData()
    {
        IUnexplainedVariance oUnExpVarInfo = RemotingHelper.GetUnexplainedVarianceObject();
        this.oUnExpVarInfoCollection = oUnExpVarInfo.GetUnExplainedVarianceHistoryInfoCollection(_glDataID, Helper.GetAppUserInfo());
        this.rgUnExpectedVarianceHistory.DataSource = this.oUnExpVarInfoCollection;
        this.rgUnExpectedVarianceHistory.DataBind();
    }
}
