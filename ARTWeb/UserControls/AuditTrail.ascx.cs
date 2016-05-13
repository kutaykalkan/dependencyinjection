using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SkyStem.ART.Web.Classes;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using SkyStem.ART.Web.Utility;
using SkyStem.ART.Client.IServices;
using SkyStem.ART.Client.Model;
using SkyStem.Library.Controls.WebControls;
using SkyStem.ART.Web.Data;
using Telerik.Web.UI;
using SkyStem.ART.Client.Exception;
using SkyStem.Library.Controls.TelerikWebControls.Data;
using SkyStem.Language.LanguageUtility;
using System.IO;

public partial class UserControls_AuditTrail : UserControlBase
{

    #region Variables & Constants
    bool isExportPDF;
    bool isExportExcel;
    Int64? _GLDataID = null;
    int _IsSRA = 0;
    Int64? _AccountID = null;
    Int32? _NetAccountID = null;
    Int16? _RecStatusID = null;
    Int16? _RecCategoryTypeID = null;
    WebEnums.ReconciliationStatus eRecStatus = WebEnums.ReconciliationStatus.NotStarted;
    bool isPagevalid = true;
    bool _RegisterRecDropDownEvent = false;
    #endregion

    #region Properties
    public Int64? GLDataID
    {
        get { return _GLDataID; }
        set { _GLDataID = value; }
    }
    public Int64? AccountID
    {
        get { return _AccountID; }
        set { _AccountID = value; }
    }
    public Int32? NetAccountID
    {
        get { return _NetAccountID; }
        set { _NetAccountID = value; }
    }
    public Int16? RecStatusID
    {
        get { return _RecStatusID; }
        set { _RecStatusID = value; }
    }
    public Int16? RecCategoryTypeID
    {
        get { return _RecCategoryTypeID; }
        set { _RecCategoryTypeID = value; }
    }
    public WebEnums.ReconciliationStatus ERecStatus
    {
        get { return eRecStatus; }
        set { eRecStatus = value; }
    }

    public bool RegisterRecDropDownEvent
    {
        get { return _RegisterRecDropDownEvent; }
        set { _RegisterRecDropDownEvent = value; }
    }
    #endregion

    #region Delegates & Events
    #endregion

    #region Page Events
    protected void Page_Init(object sender, EventArgs e)
    {
        if (this.Page.Master != null)
        {
            if (Request.QueryString[QueryStringConstants.IS_SRA] != null)
                _IsSRA = Convert.ToInt32(Request.QueryString[QueryStringConstants.IS_SRA]);
            MasterPageBase oMasterPageBase = (MasterPageBase)this.Page.Master.Master;
            if (RegisterRecDropDownEvent)
            {
                oMasterPageBase.ReconciliationPeriodChangedEventHandler += new EventHandler(oMasterPageBase_ReconciliationPeriodChangedEventHandler);

            }
            else
            {
                this.btnBack.Visible = false;
            }
        }
        if (!this.IsPostBack && Request.UrlReferrer != null)
        {
            this.btnBack.PostBackUrl = Request.UrlReferrer.PathAndQuery;
        }
        rgAuditTrail.AutoGenerateColumns = false;
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        Helper.RegisterPostBackToControls((PageBase)this.Page, rgAuditTrail);

        if (Request.QueryString[QueryStringConstants.ACCOUNT_ID] != null)
            _AccountID = Convert.ToInt64(Request.QueryString[QueryStringConstants.ACCOUNT_ID]);

        if (Request.QueryString[QueryStringConstants.NETACCOUNT_ID] != null)
            _NetAccountID = Convert.ToInt32(Request.QueryString[QueryStringConstants.NETACCOUNT_ID]);

        if (Request.QueryString[QueryStringConstants.GLDATA_ID] != null)
            _GLDataID = Convert.ToInt64(Request.QueryString[QueryStringConstants.GLDATA_ID]);



        if (Request.QueryString[QueryStringConstants.REC_CATEGORY_TYPE_ID] != null)
            this._RecCategoryTypeID = Convert.ToInt16(Request.QueryString[QueryStringConstants.REC_CATEGORY_TYPE_ID]);

        WebEnums.ReconciliationStatus eRecStatus = WebEnums.ReconciliationStatus.NotStarted;
        if (Request.QueryString[QueryStringConstants.REC_STATUS_ID] != null)
        {
            _RecStatusID = Convert.ToInt16(Request.QueryString[QueryStringConstants.REC_STATUS_ID]);
            eRecStatus = (WebEnums.ReconciliationStatus)System.Enum.Parse(typeof(WebEnums.ReconciliationStatus), _RecStatusID.ToString());
        }

        if (!RegisterRecDropDownEvent)
        {
            SetGLDataID();
            rgAuditTrail.Rebind();
        }

        // Set default Sorting
        SetDefaultSortExpression();

        if (!Page.IsPostBack)
        {
            isExportPDF = false;
            isExportExcel = false;
            SessionHelper.ClearSearchResultsFromSession();
        }
    }
    #endregion

    #region Grid Events
    protected void rgAuditTrail_ItemCreated(object sender, GridItemEventArgs e)
    {
        GridHelper.SetStylesForExportGrid(e, isExportPDF, isExportExcel);
    }
    protected void rgAuditTrail_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {
        if (e.Item.ItemType == Telerik.Web.UI.GridItemType.Item ||
            e.Item.ItemType == Telerik.Web.UI.GridItemType.AlternatingItem)
        {
            GLDataStatusInfo oGLDataStatusInfo = (GLDataStatusInfo)e.Item.DataItem;

            ExLabel lblDate = (ExLabel)e.Item.FindControl("lblDate");
            ExLabel lblUser = (ExLabel)e.Item.FindControl("lblUser");
            ExLabel lblAction = (ExLabel)e.Item.FindControl("lblAction");

            lblDate.Text = Helper.GetDisplayDateTime(oGLDataStatusInfo.StatusDate);
            if (_IsSRA == 1 && oGLDataStatusInfo.StatusID.HasValue && oGLDataStatusInfo.StatusID.Value == (short)WebEnums.ReconciliationStatus.Prepared)
                lblUser.Text = LanguageUtil.GetValue(2689);
            else
                lblUser.Text = oGLDataStatusInfo.FullName;
            lblAction.Text = oGLDataStatusInfo.Status;
        }
    }
    protected void rgAuditTrail_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    {
        List<GLDataStatusInfo> oGLDataStatusInfoCollection = null;
        string sessionKey = SessionConstants.SEARCH_RESULTS_AUDIT_TRAIL;
        if (isPagevalid)
        {
            try
            {
                // Check in Session Object
                oGLDataStatusInfoCollection = (List<GLDataStatusInfo>)Session[sessionKey];
                if (oGLDataStatusInfoCollection == null)
                {
                    // get from DB
                    IGLData oGLData = RemotingHelper.GetGLDataObject();
                    oGLDataStatusInfoCollection = oGLData.GetAuditTrailData(_GLDataID, Helper.GetAppUserInfo());

                    // Translate
                    LanguageHelper.TranslateAuditTrailData(oGLDataStatusInfoCollection);

                    // store into Session
                    Session[sessionKey] = oGLDataStatusInfoCollection;
                    SessionHelper.ClearSearchResultsFromSession(sessionKey);
                }
                rgAuditTrail.MasterTableView.DataSource = oGLDataStatusInfoCollection;
                // Sort the Data
                GridHelper.SortDataSource(rgAuditTrail.MasterTableView);
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
        else
        {
            oGLDataStatusInfoCollection = new List<GLDataStatusInfo>();
            SessionHelper.ClearSession(SessionConstants.SEARCH_RESULTS_REVIEW_NOTES);
            rgAuditTrail.MasterTableView.DataSource = oGLDataStatusInfoCollection;
        }
    }
    protected void rgReviewNotes_SortCommand(object source, GridSortCommandEventArgs e)
    {
        GridHelper.HandleSortCommand(e);
        rgAuditTrail.Rebind();
        RecHelper.SetRecStatusBarPropertiesForOtherPages((PageBase)this.Page, this._GLDataID);
    }
    protected void rgAuditTrail_OnItemCommand(object sender, GridCommandEventArgs e)
    {
        if (e.CommandName == TelerikConstants.GridExportToPDFCommandName)
        {
            isExportPDF = true;
            //rgAuditTrail.MasterTableView.Columns.FindByUniqueName("IconColumn").Visible = false;
            //GridHelper.ExportGridToPDF(rgAuditTrail, 1380);
            ExportToPDF();
        }
        if (e.CommandName == TelerikConstants.GridExportToExcelCommandName)
        {
            isExportExcel = true;
            //rgCompanyList.MasterTableView.Columns.FindByUniqueName("IconColumn").Visible = false;
            GridHelper.ExportGridToExcel(rgAuditTrail, 1380);

        }

    }
    #endregion

    #region Other Events
    void oMasterPageBase_ReconciliationPeriodChangedEventHandler(object sender, EventArgs e)
    {
        try
        {
            _GLDataID = Helper.GetGLDataIDAndRecStatusForPeriodChange((PageBase)this.Page, _AccountID, _NetAccountID, Helper.GetRecTemplateForCurrentPage(this._RecCategoryTypeID), null);
            Helper.HideMessage((PageBase)this.Page);

            // Set the Master Page Properties for GL Data ID
            RecHelper.SetRecStatusBarPropertiesForOtherPages((PageBase)this.Page, this._GLDataID);

            //Helper.ShowRecStatusBar(this, this._GLDataID);

            Session[SessionConstants.SEARCH_RESULTS_AUDIT_TRAIL] = null;
        }
        catch (ARTException ex)
        {
            isPagevalid = false;
            Helper.ShowErrorMessage((PageBase)this.Page, ex);
        }
        catch (Exception ex)
        {
            isPagevalid = false;
            Helper.ShowErrorMessage((PageBase)this.Page, ex);
        }
        rgAuditTrail.Rebind();
    }
    #endregion

    #region Validation Control Events
    #endregion

    #region Private Methods
    private string GetUrlForPDF()
    {
        string url = string.Empty;
        url = "~/Pages/RecFormPrint/TemplateAuditTrailPrint.aspx?" + Request.Url.AbsoluteUri.Split('?')[1];
        return url;
    }
    private void SetDefaultSortExpression()
    {
        // Get the Review Notes
        if (!Page.IsPostBack)
        {
            // Add Default Sort as Date Revised, Desc
            GridSortExpression oGridSortExpression = new GridSortExpression();
            oGridSortExpression.FieldName = "StatusDate";
            oGridSortExpression.SortOrder = GridSortOrder.Descending;
            rgAuditTrail.MasterTableView.SortExpressions.AddSortExpression(oGridSortExpression);
        }
    }
    #endregion

    #region Other Methods
    void SetGLDataID()
    {
        _GLDataID = Helper.GetGLDataIDAndRecStatusForPeriodChange(_AccountID, _NetAccountID, Helper.GetRecTemplateForCurrentPage(this._RecCategoryTypeID));
        ((RecPeriodMasterPageBase)this.Page.Master).GLDataID = _GLDataID;
    }

    void ExportToPDF()
    {
        try
        {
            string pageTitle = LanguageUtil.GetValue(1380);
            string fileName = pageTitle + ".pdf";
            fileName = ExportHelper.RemoveInvalidFileNameChars(fileName);

            string url = GetUrlForPDF();
            TextWriter oTextWriter = new StringWriter();
            Server.Execute(url, oTextWriter);
            ExportHelper.GeneratePDFAndRender(pageTitle, fileName, oTextWriter.ToString(), true, true);
        }
        catch (Exception Ex)
        {
            Helper.ShowErrorMessage((PageBase)this.Page, Ex);
        }
    }

  
    #endregion

}
